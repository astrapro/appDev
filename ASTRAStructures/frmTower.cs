

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
    public partial class frmTower : Form
    {

        #region Processing
        IApplication iApp;
        BridgeMemberAnalysis StructureAnalysis = null;

        public string FilePath { get; set; }

        public string INPUT_FILE { get; set; }

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
            if (iApp.IsDemo)
            {
            }

            return false;
        }

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;


            RunAnalysis();


            Open_Analysis_Report();

        }
        public void Write_Seismic_Data(List<string> LoadData, List<string> Load_Comb, string file_name)
        {
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

                string ana_file = MyStrings.Get_Analysis_Report_File(fName);
                if (File.Exists(ana_file))
                {
                    rtb_ana_rep.Lines = File.ReadAllLines(ana_file);
                    StructureAnalysis = null;
                    Select_Steps();
                }
            }
            catch (Exception exx)
            {
            }

            return true;
        }

        public void Run_Seismic_Analysis()
        {
            //if (MessageBox.Show(this, "Do you want to Proceed for Seismic Load Analysis ?",
            //     "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

            //frm_Seismic_Suspension fsa = new frm_Seismic_Suspension(ACad, LoadCases);

            //fsa.Write_Seismic_Data += new dWrite_Seismic_Data(Write_Seismic_Data);
            //fsa.File_Seismic_Load = File_Seismic_Load;
            //fsa.RunAnalysis += new dRunAnalysis(RunAnalysis);

            //fsa.SC = Seismic_Coeeficient;
            //fsa.ShowDialog();
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
            //Delete_FIL_Files(Path.GetDirectoryName(fName));
            Delete_Temporary_Files(fName);

            System.Environment.SetEnvironmentVariable("SURVEY", fName);
            System.Environment.SetEnvironmentVariable("ASTRA", fName);

            File.WriteAllText(patFile, Path.GetDirectoryName(fName) + "\\");

            string runExe = "", ext = "";
            ext = Path.GetExtension(fName).ToLower();
            if (ext == ".txt")
            {
                //runExe = Path.Combine(Application.StartupPath, "AST001.exe");
                runExe = Path.Combine(Application.StartupPath, "AST006.exe");

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


        private void tc_pp_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);

            //if (tc4.TabPages.Count > 2)
            //{
            //    tc4.TabPages.Remove(tab_evlp_doc);
            //}

            //if (tc_parrent.SelectedIndex != 2) return;

            if (tc_parrent.SelectedTab == tab_load_calculations)
            {
                if(!File.Exists(uC_CAD_Loading.VDoc.FileName))
                {
                    string ff = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Transmission Tower\INPUT for Cable Loading.dwg");
                    if (File.Exists(ff))
                    {
                        uC_CAD_Loading.VDoc.Open(ff);
                        uC_CAD_Loading.VDoc.Redraw(true);
                        VDRAW.vdCommandAction.View3D_VTop(uC_CAD_Loading.VDoc);
                    }
                }
            }
        }

        #endregion Process

        private void tsmi_viewer_Click(object sender, EventArgs e)
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
                        //VDoc.SaveAs(sfd.FileName);
                        //Drawing_File = sfd.FileName;

                        //System.Environment.SetEnvironmentVariable("OPENFILE", Drawing_File);
                        //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                        //System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                    }
                }
            }
        }

        private void btn_view_analysis_Click(object sender, EventArgs e)
        {

            if (File.Exists(Analysis_File_Name))
            {
                System.Diagnostics.Process.Start(Analysis_File_Name);
            }
            else
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_update_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btn_view_pre_process)
            {
                iApp.View_PreProcess(File_Name);
            }
            else if (btn == btn_view_post_process)
            {
                iApp.View_PostProcess(File_Name);
            }
        }
        string File_Name = "";
        private void btn_open_data_Click(object sender, EventArgs e)
        {
            if (File.Exists(File_Name))
            {
                System.Diagnostics.Process.Start(File_Name);
            }
            else
            {
                MessageBox.Show("Analysis Input Data file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Processing

        #region Load_calculation
        private void cmb_no_levels_SelectedIndexChanged(object sender, EventArgs e)
        {
            //return;
            string drw_path = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Transmission Tower");


            string drw_file = Path.Combine(drw_path, @"INPUT for Tower Structure [LEVEL1].dwg");
            if (cmb_no_levels.SelectedIndex == 0)
            {
                drw_file = Path.Combine(drw_path, @"INPUT for Tower Structure [LEVEL1].dwg");

                grp_design_Level2.Enabled = false;
                grp_design_Level3.Enabled = false;
            }
            else if (cmb_no_levels.SelectedIndex == 1)
            {
                drw_file = Path.Combine(drw_path, @"INPUT for Tower Structure [LEVEL2].dwg");

                grp_design_Level2.Enabled = true;
                grp_design_Level3.Enabled = false;
            }
            else if (cmb_no_levels.SelectedIndex == 2)
            {
                drw_file = Path.Combine(drw_path, @"INPUT for Tower Structure [LEVEL3].dwg");
                grp_design_Level2.Enabled = true;
                grp_design_Level3.Enabled = true;
            }

            if (File.Exists(drw_file))
            {
                //uC_CAD1.VDoc.Open(drw_file);
                //uC_CAD1.VDoc.Redraw(true);
                //VDRAW.vdCommandAction.View3D_VTop(uC_CAD1.VDoc);
            }

        }
        List<TowerLoadCase> Loadcases = new List<TowerLoadCase>();
        private void txt_loadcase_end1_load_TextChanged(object sender, EventArgs e)
        {
            CalculateLoads();
        }

        private void CalculateLoads()
        {
            TowerLoadCase tower_load = new TowerLoadCase();
            CalculateLoads(ref tower_load);
        }


        private void CalculateLoads(ref TowerLoadCase tower_load)
        {
            //tower_load.LoadCase = cmb_loadcases.Text;
            tower_load.LoadCase = "";
            #region END 1
            TowerLoad tl = new TowerLoad();


            tower_load.ISApply_End1 = true;
            //if (chk_loadcase_end1.Checked)
            //{
            tl.Load = MyList.StringToDouble(txt_loadcase_end1_load.Text, 0.0);
            tl.Vertical_Angle = MyList.StringToDouble(txt_loadcase_end1_vert_angle.Text, 0.0);
            tl.Horizontal_Angle = MyList.StringToDouble(txt_loadcase_end1_hor_angle.Text, 0.0);



            txt_loadcase_end1_FY.Text = tl.FY.ToString("f2");
            lbl_loadcase_end1_FY.Text = tl.FY_TEXT;

            txt_loadcase_end1_FH.Text = tl.FH.ToString("f2");
            lbl_loadcase_end1_FH.Text = tl.FH_TEXT;

            txt_loadcase_end1_FX.Text = tl.FX.ToString("f2");
            lbl_loadcase_end1_FX.Text = tl.FX_TEXT;

            txt_loadcase_end1_FZ.Text = tl.FZ.ToString("f2");
            lbl_loadcase_end1_FZ.Text = tl.FZ_TEXT;


            tower_load.End1 = tl;


            //}

            #endregion END 1

            #region END 2


            //tower_load.ISApply_End2 = chk_loadcase_end2.Checked;
            tower_load.ISApply_End2 = true;
            if (tower_load.ISApply_End2)
            {
                tl = new TowerLoad();

                tl.Load = MyList.StringToDouble(txt_loadcase_end2_load.Text, 0.0);
                tl.Vertical_Angle = MyList.StringToDouble(txt_loadcase_end2_vert_angle.Text, 0.0);
                tl.Horizontal_Angle = MyList.StringToDouble(txt_loadcase_end2_hor_angle.Text, 0.0);


                txt_loadcase_end2_FY.Text = tl.FY.ToString("f2");
                lbl_loadcase_end2_FY.Text = tl.FY_TEXT;


                txt_loadcase_end2_FH.Text = tl.FH.ToString("f2");
                lbl_loadcase_end2_FH.Text = tl.FH_TEXT;

                txt_loadcase_end2_FX.Text = tl.FX.ToString("f2");
                lbl_loadcase_end2_FX.Text = tl.FX_TEXT;

                txt_loadcase_end2_FZ.Text = tl.FZ.ToString("f2");
                lbl_loadcase_end2_FZ.Text = tl.FZ_TEXT;


                tower_load.End2 = tl;
            }

            #endregion END 2

            #region END 3



            //tower_load.ISApply_End3 = chk_loadcase_end3.Checked;
            tower_load.ISApply_End3 = true;
            if (tower_load.ISApply_End3)
            {
                tl = new TowerLoad();

                tl.Load = MyList.StringToDouble(txt_loadcase_end3_load.Text, 0.0);
                tl.Vertical_Angle = MyList.StringToDouble(txt_loadcase_end3_vert_angle.Text, 0.0);
                tl.Horizontal_Angle = MyList.StringToDouble(txt_loadcase_end3_hor_angle.Text, 0.0);




                txt_loadcase_end3_FY.Text = tl.FY.ToString("f2");
                lbl_loadcase_end3_FY.Text = tl.FY_TEXT;

                txt_loadcase_end3_FH.Text = tl.FH.ToString("f2");
                lbl_loadcase_end3_FH.Text = tl.FH_TEXT;



                txt_loadcase_end3_FX.Text = tl.FX.ToString("f2");
                lbl_loadcase_end3_FX.Text = tl.FX_TEXT;


                txt_loadcase_end3_FZ.Text = tl.FZ.ToString("f2");
                lbl_loadcase_end3_FZ.Text = tl.FZ_TEXT;


                tower_load.End3 = tl;
            }
            #endregion END 3

            #region END 4

            tower_load.ISApply_End4 = true;
            if (tower_load.ISApply_End4)
            {
                tl = new TowerLoad();

                tl.Load = MyList.StringToDouble(txt_loadcase_end4_load.Text, 0.0);
                tl.Vertical_Angle = MyList.StringToDouble(txt_loadcase_end4_vert_angle.Text, 0.0);
                tl.Horizontal_Angle = MyList.StringToDouble(txt_loadcase_end4_hor_angle.Text, 0.0);

                txt_loadcase_end4_FY.Text = tl.FY.ToString("f2");
                lbl_loadcase_end4_FY.Text = tl.FY_TEXT;

                txt_loadcase_end4_FH.Text = tl.FH.ToString("f2");
                lbl_loadcase_end4_FH.Text = tl.FH_TEXT;

                txt_loadcase_end4_FX.Text = tl.FX.ToString("f2");
                lbl_loadcase_end4_FX.Text = tl.FX_TEXT;

                txt_loadcase_end4_FZ.Text = tl.FZ.ToString("f2");
                lbl_loadcase_end4_FZ.Text = tl.FZ_TEXT;

                tower_load.End4 = tl;
            }
            #endregion END 4


            #region Wind Force

            tower_load.ISApply_Wind_Force = true;
            if (tower_load.ISApply_Wind_Force)
            {
                WindLoad wn = new WindLoad();
                wn.Wind_Velocity = MyList.StringToDouble(txt_loadcase_wind_velocity.Text, 0.0);
                wn.Effective_Area_Factor = MyList.StringToDouble(txt_loadcase_wind_area_factor.Text, 0.0);

                string WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 214 231 247 248 263 305 308 310 313 328 329 331 344 375 377 403 ";
                string WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 212 231 232 247 248 305 TO 307 313 TO 315 328 331 375 376 403 ";

                if (cmb_no_levels.SelectedIndex == 0)
                {
                    WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 223 ";
                    WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 223 ";

                }
                else if (cmb_no_levels.SelectedIndex == 1)
                {
                    WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 214 231 247 248 263 305 308 310 337 ";
                    WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 212 231 232 247 248 305 TO 307 337 ";
                }

                wn.Total_Nodes = MyList.Get_Array_Intiger(WIND_FX).Count;


                wn.Wind_Rectangle_Width = MyList.StringToDouble(txt_des_width.Text, 0.0);
                wn.Wind_Rectangle_Height = MyList.StringToDouble(txt_des_Height.Text, 0.0);


                tower_load.Wind = wn;

                SeismicLoad sl = new SeismicLoad();

                sl.Ah =  MyList.StringToDouble(txt_loadcase_seismic.Text, 0.0);

                if (complete_design != null)
                {
                    sl.W = complete_design.TotalSteelWeight * 10;

                    for (int i = 0; i < complete_design.Members.Count; i++)
                    {
                        var mm = complete_design.Members[i];

                        if (mm.Group.GroupName.StartsWith("_LEV1"))
                        {
                            sl.W1 = sl.W1 + complete_design.Members[i].Weight;
                        }
                        else if (mm.Group.GroupName.StartsWith("_LEV2"))
                        {
                            sl.W2 = sl.W2 + complete_design.Members[i].Weight;
                        }
                        else if (mm.Group.GroupName.StartsWith("_LEV3"))
                        {
                            sl.W3 = sl.W3 + complete_design.Members[i].Weight;
                        }
                        else if (mm.Group.GroupName.StartsWith("_CRW"))
                        {
                            sl.W4 = sl.W4 + complete_design.Members[i].Weight;
                        }
                    }
                    double Hgt = wn.Wind_Rectangle_Height;

                    sl.H1 = Get_Y_Coordinates(13.31, Hgt);
                    sl.H2 = Get_Y_Coordinates(17.31, Hgt);
                    sl.H3 = Get_Y_Coordinates(20.31, Hgt);
                    sl.H4 = Get_Y_Coordinates(21.86, Hgt);
                   
                    tower_load.Seismic = sl;


                    string Seismic_File = Path.Combine(user_path, "SEISMIC_CALCULATION.TXT");


                    File.WriteAllLines(Seismic_File, sl.Get_Calculations().ToArray());

                }

            }
            #endregion Seismic Force

        }
        private void SetLoadcases(ref TowerLoadCase tower_load)
        {
            //txt_loadcases.Text  = tower_load.LoadCase;
            #region END 1
            TowerLoad tl = tower_load.End1;

            txt_loadcase_end1_load.Text = tl.Load.ToString("f2");
            txt_loadcase_end1_vert_angle.Text = tl.Vertical_Angle.ToString("f2");
            txt_loadcase_end1_hor_angle.Text = tl.Horizontal_Angle.ToString("f2");

            #endregion END 1

            #region END 2

            tl = tower_load.End2;

            txt_loadcase_end2_load.Text = tl.Load.ToString("f2");
            txt_loadcase_end2_vert_angle.Text = tl.Vertical_Angle.ToString("f2");
            txt_loadcase_end2_hor_angle.Text = tl.Horizontal_Angle.ToString("f2");

            #endregion END 2

            #region END 3

            tl = tower_load.End3;

            txt_loadcase_end3_load.Text = tl.Load.ToString("f2");
            txt_loadcase_end3_vert_angle.Text = tl.Vertical_Angle.ToString("f2");
            txt_loadcase_end3_hor_angle.Text = tl.Horizontal_Angle.ToString("f2");

            #endregion END 3

            #region END 4

            tl = tower_load.End4;

            txt_loadcase_end4_load.Text = tl.Load.ToString("f2");
            txt_loadcase_end4_vert_angle.Text = tl.Vertical_Angle.ToString("f2");
            txt_loadcase_end4_hor_angle.Text = tl.Horizontal_Angle.ToString("f2");

            #endregion END 4


            #region Wind Load


            txt_loadcase_wind_velocity.Text = tower_load.Wind.Wind_Velocity.ToString("f2");
            txt_loadcase_wind_area_factor.Text = tower_load.Wind.Effective_Area_Factor.ToString("f2");

            #endregion Wind Load


            txt_loadcase_seismic.Text = tower_load.Seismic.Ah.ToString("f2");

        }

        TowerLoadCase twr_loadcase;

        private void btn_loadcase_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            twr_loadcase = new TowerLoadCase();


            if (btn == btn_loadcase_add)
            {
                CalculateLoads(ref twr_loadcase);
                //Loadcases.Add(twr_loadcase);

                Update_Section_Properties();
                //MessageBox.Show("Loadcase " + cmb_loadcases.Text + " added.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (btn == btn_loadcase_update)
            {
                //for (int i = 0; i < Loadcases.Count; i++)
                //{
                //    twr_loadcase = Loadcases[i];
                //    if(twr_loadcase.LoadCase == cmb_loadcases.Text)
                //    {
                //        CalculateLoads(ref twr_loadcase);
                //        MessageBox.Show("Loadcase " + cmb_loadcases.Text + " Updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        break;
                //    }
                //}
            }
            else if (btn == btn_loadcase_delete)
            {
                //for (int i = 0; i < Loadcases.Count; i++)
                //{
                //    twr_loadcase = Loadcases[i];
                //    if (twr_loadcase.LoadCase == cmb_loadcases.Text)
                //    {
                //        Loadcases.RemoveAt(i);
                //        cmb_loadcases.Items.RemoveAt(i);
                //        MessageBox.Show("Loadcase " + cmb_loadcases.Text + " Deleted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        break;
                //    }
                //}
            }
            else if (btn == btn_Load_help)
            {
                string help_file = Path.Combine(Application.StartupPath, @"ASTRAHelp\TransmissionTower_Load_Calculations.pdf");

                if(File.Exists(help_file))
                {
                    iApp.RunExe(help_file);
                }
            }

            
        }

        private void cmb_loadcases_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TowerLoadCase tlc = new TowerLoadCase();
            //if (cmb_loadcases.SelectedIndex != -1)
            //{
            //    tlc = Loadcases[cmb_loadcases.SelectedIndex];
            //    SetLoadcases(ref tlc);
            //}
        }
        #endregion Load_calculation

        #region Create Data


        eASTRADesignType Project_Type;
        public string Title
        {
            get
            {
                //return "STEEL_TRUSS_OPEN_WEB_COMPLETE_DESIGN";


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    if (Project_Type == eASTRADesignType.Transmission_Tower) return "DESIGN OF TRANSMISSION TOWER [BS]";
                    else if (Project_Type == eASTRADesignType.Microwave_Tower) return "DESIGN OF MICROWAVE TOWER [BS]";
                    else if (Project_Type == eASTRADesignType.CableCar_Tower) return "DESIGN OF CABLE CAR TOWER [BS]";
                }
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                {
                    if (Project_Type == eASTRADesignType.Transmission_Tower) return "DESIGN OF TRANSMISSION TOWER [LRFD]";
                    else if (Project_Type == eASTRADesignType.Microwave_Tower) return "DESIGN OF MICROWAVE TOWER [LRFD]";
                    else if (Project_Type == eASTRADesignType.CableCar_Tower) return "DESIGN OF CABLE CAR TOWER [LRFD]";
                }
                else
                {
                    if (Project_Type == eASTRADesignType.Transmission_Tower) return "DESIGN OF TRANSMISSION TOWER [IRC]";
                    else if (Project_Type == eASTRADesignType.Microwave_Tower) return "DESIGN OF MICROWAVE TOWER [IRC]";
                    else if (Project_Type == eASTRADesignType.CableCar_Tower) return "DESIGN OF CABLE CAR TOWER [IRC]";

                    return "DESIGN OF TRANSMISSION TOWER [IRC]";
                }
                return "DESIGN OF TRANSMISSION TOWER";
            }
        }

        public List<string> Get_TowerData()
        {
            string file_name = Path.Combine(Application.StartupPath, @"DESIGN\Transmission Tower\Model Input Data.txt");



            if (cmb_no_levels.SelectedIndex == 0)
            {
                file_name = Path.Combine(Application.StartupPath, @"DESIGN\Transmission Tower\Transmission Tower 1 wing.txt");
            }
            else if (cmb_no_levels.SelectedIndex == 1)
            {
                file_name = Path.Combine(Application.StartupPath, @"DESIGN\Transmission Tower\Transmission Tower 2 wings.txt");
            }

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

            #region Create Model by Code
            if (false)
            //if (true)
            {
                TowerStructure tower_ss = new TowerStructure();

                tower_ss.Wing_Nos = MyList.StringToInt(cmb_no_levels.Text, 3);


                tower_ss.CWN_HGT = MyList.StringToDouble(txt_des_CWN_top_height.Text, 0.0);
                tower_ss.CWN_BOT_WD = MyList.StringToDouble(txt_des_CWN_bottom_width.Text, 0.0);

                tower_ss.BASE_WD = MyList.StringToDouble(txt_des_width.Text, 0.0);
                tower_ss.LEV1_HGT = MyList.StringToDouble(txt_des_LEV1_height.Text, 0.0);
                tower_ss.LEV2_HGT = MyList.StringToDouble(txt_des_LEV2_height.Text, 0.0);
                tower_ss.LEV3_HGT = MyList.StringToDouble(txt_des_LEV3_height.Text, 0.0);

                tower_ss.LEV1_WING_HGT = MyList.StringToDouble(txt_des_LEV1_wing_height.Text, 0.0);
                tower_ss.LEV2_WING_HGT = MyList.StringToDouble(txt_des_LEV2_wing_height.Text, 0.0);
                tower_ss.LEV3_WING_HGT = MyList.StringToDouble(txt_des_LEV3_wing_height.Text, 0.0);

                tower_ss.LEV1_WING_BOT_WD = MyList.StringToDouble(txt_des_LEV1_bottom_width.Text, 0.0);
                tower_ss.LEV2_WING_BOT_WD = MyList.StringToDouble(txt_des_LEV2_bottom_width.Text, 0.0);
                tower_ss.LEV3_WING_BOT_WD = MyList.StringToDouble(txt_des_LEV3_bottom_width.Text, 0.0);


                tower_ss.LEV1_WING_TOP_WD = MyList.StringToDouble(txt_des_LEV1_top_width.Text, 0.0);
                tower_ss.LEV2_WING_TOP_WD = MyList.StringToDouble(txt_des_LEV2_top_width.Text, 0.0);
                tower_ss.LEV3_WING_TOP_WD = MyList.StringToDouble(txt_des_LEV3_top_width.Text, 0.0);


                tower_ss.LEV1_WING_SPAN = MyList.StringToDouble(txt_des_LEV1_wing_span.Text, 0.0);
                tower_ss.LEV2_WING_SPAN = MyList.StringToDouble(txt_des_LEV2_wing_span.Text, 0.0);
                tower_ss.LEV3_WING_SPAN = MyList.StringToDouble(txt_des_LEV3_wing_span.Text, 0.0);

                return;

            }
            #endregion Create Model by Code


            //return;
            CBridgeStructure ca = new CBridgeStructure(Get_TowerData());


            TransmissionTower tt = new TransmissionTower();

            //double Len = MyList.StringToDouble(txt_Length.Text, 11.844);
            //double Height = MyList.StringToDouble(txt_Height.Text, 26.81);

            //double Len = MyList.StringToDouble(txt_des_CWN_bottom_width.Text, 11.844);
            //double Height = MyList.StringToDouble(txt_des_CWN_top_height.Text, 26.81);

            double Len = 11.844;
            double Height = 26.81;



            Len = MyList.StringToDouble(txt_des_width.Text, 11.844);
            Height = MyList.StringToDouble(txt_des_Height.Text, 23.584); ;





            //Hashtable ht = new Hashtable();

            //object obj = null;
            double min_X = 0.0;

            for (int i = 0; i < ca.Joints.Count; i++)
            {

                //ca.Joints[i].X = tt.Get_X_Coordinates(ca.Joints[i].X, 11.8440);
                //ca.Joints[i].Y = tt.Get_Y_Coordinates(ca.Joints[i].Y, 26.81);
                //ca.Joints[i].Z = tt.Get_Z_Coordinates(ca.Joints[i].Y, 11.8440);



                ca.Joints[i].X = Get_X_Coordinates(ca.Joints[i].X, Len);
                ca.Joints[i].Y = Get_Y_Coordinates(ca.Joints[i].Y, Height);
                ca.Joints[i].Z = Get_Z_Coordinates(ca.Joints[i].Z, Len);


                if (min_X > ca.Joints[i].X) min_X = ca.Joints[i].X;
            }


            foreach (var item in ca.Joints)
            {
                item.X += Math.Abs(min_X);
            }


            List<string> list = new List<string>();
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            foreach (var item in ca.Joints)
            {
                list.Add(item.ToString());
            }
            list.Add(string.Format("MEMBER INCIDENCES"));
            foreach (var item in ca.Members)
            {
                list.Add(item.ToString());
            }



            list.Add(string.Format("START GROUP DEFINITION"));

            foreach (var item in ca.MemberGroups.GroupCollection)
            {
                item.GroupName = Get_Tower_Member_Short(item.GroupName);
                list.Add(item.ToString());

            }
            list.Add(string.Format("END GROUP DEFINITION"));

            #region 3 wings
            //list.Add(string.Format("START GROUP DEFINITION"));
            //list.Add(string.Format("_CRW_MAINPOST 997 1000 1001 1004 1005 1007 1010 1012 1013 1016 1017 1020 1021 1023 1026 1028 1029 1032 1033 1036 1037 1039 1042 1044 TO 1048"));
            //list.Add(string.Format("_CRW_BRACINGS 998 999 1002 1003 1006 1008 1009 1011 1014 1015 1018 1019 1022 1024 1025 1027 1030 1031 1034 1035 1038 1040 1041 1043"));
            //list.Add(string.Format("_LEV1_MAINPOST 173 176 179 182 209 212 215 218 253 256 259 262 297 300 303 306 323 326 329 332 337 340 343 346 363 368 377 382 383 390 403 410 411 415 425 426 431 439 532 538"));
            //list.Add(string.Format("_LEV1_LOWER_CROSS_BRACINGS 171 172 174 175 177 178 180 181 184 187 190 193 196 199 202 205 220 223 226 229 232 235 238 241 263 TO 270 307 TO 322"));
            //list.Add(string.Format("_LEV1_UPPER_CROSS_BRACINGS 324 325 327 328 330 331 333 334 348 349 352 354 355 357 360 361 365 366 370 372 373 375 379 380 386 387 395 TO 398 406 407 412 413 416 TO 418 420 422 424"));
            //list.Add(string.Format("_LEV1_INTERNAL_LOWERBRACINGS 183 185 TO 189 191 192 194 195 197 198 200 201 203 TO 208 210 211 213 214 216 217 219 221 222 224 225 227 228 230 231 233 234 236 237 239 240 242 TO 252 254 255 257 258 260 261 271 TO 296 298 299 301 302 304 305"));
            //list.Add(string.Format("_LEV1_INTERNALUPPERBRACINGS 3 TO 16 49 TO 62 335 336 338 339 341 342 344 345 347 350 351 353 356 358 359 362 364 367 369 371 374 376 378 381 384 385 388 389 391 TO 394 399 TO 402 404 405 408 409 414 419 421 423"));
            //list.Add(string.Format("_LEV1_UPPER 17 TO 24 63 64 77 78 91 TO 94"));
            //list.Add(string.Format("_LEV1_WING_BRACINGS 428 429 432 433 436 437 488 TO 491 529 530 533 TO 536 557 TO 567 568 TO 572"));
            //list.Add(string.Format("_LEV1_LEFT_WING_TOP 486 487 539 543 545 549 551 555 573 577 579 583 585 589"));
            //list.Add(string.Format("_LEV1_LEFT_WING_BOTTOM 427 440 448 456 464 472 480 485 495 499 505 511 517 523"));
            //list.Add(string.Format("_LEV1_LEFT_WING_SIDEBRACINGS 430 442 443 450 451 458 459 466 467 474 475 482 496 500 501 506 507 512 513 518 519 524 525 531"));
            //list.Add(string.Format("_LEV1_LEFT_WING_TOPBRACINGS 79 81 83 85 87 89 542 548 554 576 582 588"));
            //list.Add(string.Format("_LEV1_LEFTWINGBOTTOMBRACINGS 65 67 69 71 73 75 441 449 457 465 473 481"));
            //list.Add(string.Format("_LEV1_RIGHTWINGTOP 493 494 540 544 546 550 552 556 574 578 580 584 586 590"));
            //list.Add(string.Format("_LEV1_RIGHTWINGBOTTOM 434 444 452 460 468 476 483 492 497 502 508 514 520 526"));
            //list.Add(string.Format("_LEV1_RIGHTWINGSIDEBRACINGS 438 446 447 454 455 462 463 470 471 478 479 484 498 503 504 509 510 515 516 521 522 527 528 537"));
            //list.Add(string.Format("_LEV1_RIGHTWINGTOPBRACINGS 80 82 84 86 88 90 541 547 553 575 581 587"));
            //list.Add(string.Format("_LEV1RIGHTWINGBOTTOMBRACINGS 66 68 70 72 74 76 435 445 453 461 469 477"));
            //list.Add(string.Format("_LEV2_MAINPOST 592 595 598 602 610 611 621 626 628 633 640 646 652 653 659 662 664 671 774 779 805 810 819 824"));
            //list.Add(string.Format("_LEV2_LOWERCROSSBRACINGS 591 593 594 596 597 599 TO 601 605 607 613 615 617 619 623 624 630 631 635 TO 638 642 643 648 650 654 655 657 658 660 661"));
            //list.Add(string.Format("_LEV2_UPPERCROSSBRACINGS 665 666 670 672 773 775 777 778 807 808 812 814 815 817 821 822"));
            //list.Add(string.Format("_LEV2_LOWERINTERNALBRACINGS 25 TO 32 95 TO 102 603 604 606 608 609 612 614 616 618 620 622 625 627 629 632 634 639 641 644 645 647 649 651 656"));
            //list.Add(string.Format("_LEV2_UPPERINTERNALBRACINGS 35 TO 38 127 128 129 130 806 809 811 813 816 818 820 823"));
            //list.Add(string.Format("_LEV2_LEFTWINGSIDEBRACINGS 667 676 677 684 685 692 693 700 701 708 709 716 717 724 734 738 739 744 745 750 751 756 757 762 763 768 769 776"));
            //list.Add(string.Format("_LEV2_LEFTWINGTOPBRACINGS 119 121 123 125 131 133 135 784 790 796 802 828 834 840"));
            //list.Add(string.Format("_LEV2_LEFTWINGBOTTOMBRACINGS 105 107 109 111 113 115 117 675 683 691 699 707 715 723"));
            //list.Add(string.Format("_LEV2_RIGHTWINGSIDEBRACINGS 673 680 681 688 689 696 697 704 705 712 713 720 721 726 736 741 742 747 748 753 754 759 760 765 766 771 772 780"));
            //list.Add(string.Format("_LEV2_WINGTOP 728 729 781 785 787 791 793 797 799 803 825 829 831 835 837 841"));
            //list.Add(string.Format("_LEV2_BOX 33 34 39 TO 42 103 104 137 TO 140"));
            //list.Add(string.Format("_LEV2_LEFTWINGBOTTOM 663 674 682 690 698 706 714 722 727 733 737 743 749 755 761 767"));
            //list.Add(string.Format("_LEV2_RIGHTWINGTOP 731 732 782 786 788 792 794 798 800 804 826 830 832 836 838 842"));
            //list.Add(string.Format("_LEV2_RIGHTWINGBOTTOM 668 678 686 694 702 710 718 725 730 735 740 746 752 758 764 770"));
            //list.Add(string.Format("_LEV2_RIGHTWINGTOPBRACINGS 120 122 124 126 132 134 136 783 789 795 801 827 833 839"));
            //list.Add(string.Format("_LEV2RIGHTWINGBOTTOMBRACINGS 106 108 110 112 114 116 118 669 679 687 695 703 711 719"));
            //list.Add(string.Format("_LEV3_MAINPOST 843 TO 846 851 857 947 952"));
            //list.Add(string.Format("_LEV3_UPPER_BRACINGS 1 2 848 849 852 854 855 944 945 948 TO 950 971 TO 978"));
            //list.Add(string.Format("_LEV3_BOX 43 TO 48 141 142 155 156 169 170"));
            //list.Add(string.Format("_LEV3RIGHTWINGBOTTOMBRACINGS 144 146 148 150 152 154 863 871 879 887 895 902"));
            //list.Add(string.Format("_LEV3_RIGHTWINGTOPBRACINGS 158 160 162 164 166 168 955 963 969 983 989 995"));
            //list.Add(string.Format("_LEV3_RIGHTWINGSIDEBRACINGS 856 864 865 872 873 880 881 888 889 896 897 903 913 918 919 924 925 930 931 936 937 942 943 951"));
            //list.Add(string.Format("_LEV3_RIGHTWINGTOP 908 909 954 958 960 964 966 970 980 984 986 990 992 996"));
            //list.Add(string.Format("_LEV3_RIGHTWINGBOTTOM 853 862 870 878 886 894 901 907 912 917 923 929 935 941"));
            //list.Add(string.Format("_LEV3LEFTWINGSIDEBRACINGS 850 860 861 868 869 876 877 884 885 892 893 900 911 915 916 921 922 927 928 933 934 939 940 946"));
            //list.Add(string.Format("_LEV3LEFTWINGTOPBRACINGS 157 159 161 163 165 167 956 961 967 981 987 993"));
            //list.Add(string.Format("_LEV3LEFTWINGBOTTOMBRACINGS 143 145 147 149 151 153 859 867 875 883 891 899"));
            //list.Add(string.Format("_LEV3LEFTWINGTOP 905 906 953 957 959 962 965 968 979 982 985 988 991 994"));
            //list.Add(string.Format("_LEV3LEFTWINGBOTTOM 847 858 866 874 882 890 898 904 910 914 920 926 932 938"));
            //list.Add(string.Format("END GROUP DEFINITION"));
            list.Add(string.Format("SECTION PROPERTIES"));
            list.Add(string.Format("1 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("2 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("3 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("4 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("5 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("6 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("7 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("8 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("9 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("10 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("11 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("12 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("13 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("14 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("15 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("16 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("17 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("18 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("19 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("20 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("21 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("22 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("23 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("24 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("25 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("26 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("27 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("28 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("29 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("30 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("31 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("32 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("33 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("34 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("35 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("36 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("37 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("38 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("39 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("40 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("41 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("42 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("43 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("44 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("45 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("46 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("47 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("48 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("49 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("50 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("51 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("52 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("53 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("54 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("55 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("56 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("57 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("58 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("59 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("60 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("61 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("62 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("63 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("64 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("65 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("66 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("67 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("68 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("69 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("70 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("71 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("72 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("73 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("74 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("75 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("76 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("77 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("78 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("79 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("80 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("81 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("82 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("83 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("84 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("85 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("86 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("87 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("88 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("89 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("90 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("91 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("92 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("93 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("94 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("95 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("96 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("97 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("98 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("99 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("100 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("101 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("102 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("103 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("104 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("105 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("106 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("107 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("108 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("109 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("110 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("111 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("112 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("113 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("114 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("115 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("116 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("117 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("118 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("119 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("120 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("121 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("122 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("123 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("124 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("125 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("126 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("127 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("128 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("129 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("130 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("131 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("132 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("133 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("134 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("135 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("136 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("137 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("138 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("139 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("140 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("141 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("142 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("143 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("144 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("145 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("146 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("147 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("148 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("149 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("150 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("151 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("152 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("153 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("154 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("155 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("156 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("157 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("158 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("159 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("160 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("161 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("162 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("163 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("164 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("165 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("166 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("167 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("168 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("169 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("170 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("171 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("172 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("173 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("174 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("175 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("176 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("177 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("178 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("179 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("180 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("181 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("182 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("183 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("184 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("185 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("186 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("187 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("188 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("189 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("190 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("191 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("192 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("193 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("194 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("195 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("196 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("197 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("198 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("199 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("200 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("201 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("202 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("203 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("204 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("205 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("206 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("207 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("208 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("209 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("210 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("211 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("212 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("213 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("214 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("215 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("216 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("217 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("218 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("219 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("220 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("221 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("222 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("223 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("224 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("225 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("226 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("227 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("228 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("229 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("230 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("231 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("232 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("233 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("234 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("235 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("236 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("237 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("238 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("239 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("240 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("241 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("242 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("243 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("244 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("245 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("246 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("247 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("248 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("249 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("250 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("251 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("252 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("253 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("254 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("255 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("256 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("257 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("258 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("259 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("260 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("261 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("262 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("263 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("264 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("265 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("266 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("267 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("268 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("269 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("270 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("271 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("272 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("273 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("274 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("275 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("276 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("277 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("278 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("279 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("280 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("281 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("282 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("283 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("284 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("285 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("286 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("287 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("288 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("289 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("290 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("291 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("292 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("293 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("294 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("295 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("296 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("297 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("298 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("299 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("300 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("301 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("302 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("303 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("304 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("305 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("306 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("307 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("308 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("309 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("310 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("311 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("312 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("313 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("314 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("315 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("316 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("317 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("318 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("319 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("320 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("321 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("322 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("323 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("324 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("325 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("326 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("327 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("328 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("329 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("330 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("331 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("332 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("333 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("334 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("335 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("336 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("337 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("338 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("339 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("340 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("341 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("342 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("343 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("344 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("345 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("346 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("347 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("348 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("349 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("350 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("351 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("352 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("353 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("354 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("355 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("356 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("357 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("358 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("359 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("360 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("361 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("362 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("363 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("364 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("365 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("366 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("367 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("368 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("369 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("370 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("371 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("372 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("373 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("374 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("375 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("376 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("377 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("378 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("379 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("380 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("381 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("382 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("383 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("384 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("385 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("386 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("387 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("388 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("389 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("390 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("391 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("392 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("393 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("394 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("395 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("396 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("397 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("398 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("399 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("400 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("401 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("402 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("403 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("404 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("405 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("406 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("407 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("408 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("409 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("410 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("411 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("412 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("413 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("414 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("415 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("416 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("417 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("418 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("419 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("420 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("421 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("422 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("423 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("424 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("425 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("426 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("427 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("428 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("429 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("430 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("431 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("432 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("433 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("434 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("435 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("436 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("437 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("438 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("439 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("440 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("441 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("442 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("443 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("444 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("445 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("446 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("447 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("448 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("449 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("450 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("451 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("452 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("453 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("454 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("455 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("456 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("457 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("458 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("459 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("460 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("461 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("462 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("463 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("464 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("465 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("466 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("467 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("468 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("469 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("470 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("471 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("472 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("473 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("474 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("475 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("476 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("477 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("478 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("479 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("480 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("481 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("482 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("483 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("484 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("485 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("486 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("487 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("488 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("489 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("490 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("491 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("492 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("493 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("494 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("495 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("496 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("497 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("498 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("499 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("500 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("501 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("502 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("503 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("504 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("505 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("506 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("507 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("508 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("509 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("510 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("511 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("512 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("513 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("514 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("515 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("516 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("517 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("518 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("519 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("520 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("521 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("522 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("523 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("524 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("525 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("526 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("527 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("528 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("529 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("530 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("531 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("532 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("533 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("534 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("535 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("536 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("537 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("538 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("539 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("540 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("541 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("542 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("543 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("544 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("545 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("546 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("547 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("548 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("549 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("550 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("551 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("552 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("553 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("554 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("555 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("556 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("557 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("558 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("559 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("560 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("561 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("562 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("563 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("564 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("565 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("566 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("567 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("568 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("569 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("570 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("571 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("572 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("573 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("574 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("575 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("576 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("577 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("578 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("579 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("580 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("581 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("582 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("583 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("584 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("585 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("586 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("587 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("588 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("589 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("590 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("591 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("592 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("593 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("594 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("595 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("596 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("597 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("598 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("599 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("600 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("601 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("602 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("603 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("604 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("605 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("606 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("607 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("608 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("609 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("610 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("611 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("612 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("613 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("614 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("615 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("616 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("617 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("618 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("619 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("620 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("621 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("622 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("623 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("624 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("625 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("626 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("627 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("628 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("629 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("630 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("631 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("632 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("633 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("634 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("635 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("636 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("637 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("638 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("639 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("640 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("641 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("642 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("643 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("644 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("645 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("646 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("647 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("648 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("649 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("650 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("651 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("652 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("653 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("654 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("655 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("656 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("657 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("658 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("659 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("660 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("661 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("662 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("663 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("664 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("665 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("666 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("667 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("668 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("669 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("670 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("671 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("672 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("673 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("674 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("675 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("676 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("677 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("678 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("679 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("680 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("681 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("682 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("683 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("684 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("685 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("686 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("687 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("688 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("689 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("690 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("691 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("692 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("693 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("694 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("695 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("696 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("697 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("698 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("699 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("700 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("701 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("702 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("703 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("704 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("705 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("706 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("707 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("708 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("709 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("710 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("711 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("712 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("713 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("714 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("715 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("716 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("717 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("718 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("719 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("720 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("721 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("722 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("723 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("724 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("725 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("726 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("727 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("728 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("729 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("730 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("731 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("732 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("733 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("734 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("735 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("736 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("737 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("738 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("739 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("740 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("741 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("742 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("743 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("744 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("745 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("746 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("747 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("748 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("749 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("750 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("751 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("752 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("753 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("754 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("755 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("756 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("757 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("758 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("759 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("760 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("761 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("762 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("763 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("764 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("765 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("766 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("767 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("768 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("769 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("770 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("771 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("772 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("773 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("774 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("775 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("776 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("777 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("778 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("779 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("780 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("781 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("782 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("783 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("784 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("785 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("786 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("787 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("788 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("789 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("790 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("791 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("792 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("793 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("794 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("795 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("796 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("797 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("798 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("799 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("800 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("801 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("802 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("803 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("804 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("805 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("806 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("807 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("808 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("809 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("810 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("811 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("812 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("813 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("814 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("815 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("816 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("817 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("818 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("819 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("820 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("821 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("822 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("823 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("824 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("825 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("826 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("827 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("828 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("829 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("830 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("831 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("832 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("833 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("834 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("835 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("836 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("837 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("838 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("839 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("840 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("841 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("842 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("843 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("844 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("845 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("846 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("847 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("848 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("849 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("850 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("851 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("852 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("853 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("854 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("855 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("856 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("857 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("858 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("859 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("860 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("861 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("862 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("863 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("864 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("865 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("866 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("867 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("868 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("869 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("870 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("871 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("872 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("873 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("874 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("875 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("876 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("877 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("878 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("879 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("880 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("881 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("882 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("883 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("884 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("885 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("886 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("887 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("888 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("889 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("890 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("891 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("892 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("893 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("894 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("895 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("896 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("897 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("898 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("899 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("900 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("901 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("902 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("903 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("904 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("905 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("906 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("907 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("908 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("909 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("910 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("911 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("912 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("913 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("914 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("915 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("916 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("917 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("918 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("919 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("920 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("921 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("922 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("923 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("924 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("925 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("926 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("927 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("928 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("929 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("930 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("931 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("932 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("933 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("934 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("935 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("936 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("937 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("938 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("939 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("940 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("941 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("942 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("943 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("944 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("945 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("946 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("947 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("948 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("949 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("950 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("951 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("952 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("953 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("954 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("955 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("956 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("957 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("958 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("959 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("960 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("961 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("962 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("963 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("964 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("965 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("966 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("967 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("968 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("969 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("970 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("971 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("972 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("973 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("974 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("975 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("976 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("977 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("978 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("979 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("980 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("981 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("982 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("983 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("984 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("985 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("986 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("987 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("988 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("989 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("990 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("991 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("992 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("993 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("994 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("995 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("996 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("997 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("998 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("999 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1000 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1001 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1002 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1003 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1004 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1005 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1006 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1007 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1008 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1009 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1010 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1011 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1012 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1013 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1014 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1015 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1016 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1017 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1018 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1019 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1020 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1021 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1022 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1023 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1024 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1025 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1026 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1027 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1028 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1029 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1030 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1031 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1032 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1033 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1034 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1035 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1036 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1037 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1038 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1039 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1040 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1041 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1042 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1043 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1044 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1045 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1046 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1047 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("1048 PRISMATIC AX 0.326 IX 0.0012 IY 0.236"));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E STEEL ALL"));
            list.Add(string.Format("DEN STEEL ALL"));
            list.Add(string.Format("POISSON STEEL ALL"));
            list.Add(string.Format("SUPPORTS"));
            list.Add(string.Format("1 TO 4 PINNED"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("LOAD 1 LOAD 1"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("140 143 246 248 250 266 268 327 328 331 333 348 350 FY -10.000"));
            list.Add(string.Format("140 143 247 248 328 331 FY -10.000"));
            //list.Add(string.Format("FINISH"));
            list.Add(string.Format("FINISH"));

            #endregion 3 wings


            string fileName = Path.Combine(iApp.LastDesignWorkingFolder, "TRANSMISSION TOWER");



            //fileName = Path.Combine(user_path, "TRANSMISSION TOWER");

            //Directory.CreateDirectory(fileName);

            fileName = Path.Combine(user_path, "INPUT_DATA.TXT");
            //fileName = Path.Combine(user_path, "TRANSMISSION_TOWER_ANALYSIS.TXT");

            File.WriteAllLines(fileName, list.ToArray());



            File_Name = fileName;
            //iApp.View_Input_File(fileName);
            complete_design = new TowerDesign();


            OpenAnalysisFile(File_Name);



            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                Data_Convert_and_Update_IS_TO_BS();
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                Data_Convert_and_Update_IS_TO_AISC();


            Update_Section_Properties();
            Show_Total_Weight();
            //Open_Data_File(File_Name);

            //  MessageBox.Show(this, "Analysis Input data is created as INPUT_DATA.TXT inside the Project Folder. \n\r\n\rNote :\n\r" +
            //"User has to observe the data displayed in the Tabs " +
            //"'Steel Strsucture Load [DL]', 'Super Imposed Dead Load [SIDL]' and 'Moving Load [LL]'." +
            //" User may modify the data if so desired." +
            //" Next, User has to open the tab 'Analysis + Design' and Process the buttons" +
            //"'Save Section + Load Data', 'Process Analysis' and 'Process Design' in sequence." +
            //" This will complete the Design Process.", Title);

            Write_All_Data();
            MessageBox.Show(this, "Analysis Input data is created as INPUT_DATA.TXT inside the Project Folder", "ASTRA");

        }


        public double Get_Y_Coordinates(double val, double MaxHeight)
        {
            double y = 0.0;
            double maxVal = 26.81;

            if (cmb_no_levels.SelectedIndex == 0)
            {
                maxVal = 23.584;
            }
            else if (cmb_no_levels.SelectedIndex == 1)
            {
                maxVal = 25.097;
            }


            y = MaxHeight * val / maxVal;
            return y;

        }
        public double Get_Z_Coordinates(double val, double MaxWidth)
        {
            double z = 0.0;

            double maxVal = 11.844;

            z = MaxWidth * val / maxVal;

            return z;
        }
        public double Get_X_Coordinates(double val, double MaxLen)
        {
            double x = 0.0;
            //double maxVal = 12.638;
            double maxVal = 11.8440;





            x = MaxLen * val / maxVal;

            return x;
        }

        #endregion Create Data

        public frmTower(IApplication app, eASTRADesignType prts)
        {
            //this.Show
            try
            {

                iApp = app;
                Project_Type = prts;
                InitializeComponent();
                File_Name = "";
            }
            catch (Exception ex) { }

        }

        TowerDesign complete_design = null;
        BridgeMemberAnalysis Truss_Analysis = null;

        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelBeams;

                else if (cmb_select_standard.SelectedIndex == 2)
                    return iApp.Tables.AISC_SteelBeams;


                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelChannels tbl_rolledSteelChannels
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelChannels;

                else if (cmb_select_standard.SelectedIndex == 2)
                    return iApp.Tables.AISC_SteelChannels;

                return iApp.Tables.IS_SteelChannels;
            }
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelAngles;
                else if (cmb_select_standard.SelectedIndex == 2)
                    return iApp.Tables.AISC_SteelAngles;
                return iApp.Tables.IS_SteelAngles;
            }
        }

        public void SetComboSections()
        {
            //string tab_path = Path.Combine(Application.StartupPath, "Tables");
            int i = 0;
            string sec_name, code1, code2, code3;
            try
            {
                cmb_section_name.Items.Clear();
                switch (cmb_sections_define.SelectedIndex)
                {
                    case 0:
                    case 4:
                    case 5:
                        for (i = 0; i < tbl_rolledSteelBeams.List_Table.Count; i++)
                        {
                            sec_name = tbl_rolledSteelBeams.List_Table[i].SectionName;
                            if (!cmb_section_name.Items.Contains(sec_name) && sec_name != "")
                            {
                                cmb_section_name.Items.Add(sec_name);
                            }
                        }
                        break;
                    case 1:
                    case 9:
                        for (i = 0; i < tbl_rolledSteelChannels.List_Table.Count; i++)
                        {
                            sec_name = tbl_rolledSteelChannels.List_Table[i].SectionName;
                            if (!cmb_section_name.Items.Contains(sec_name) && sec_name != "")
                            {
                                cmb_section_name.Items.Add(sec_name);
                            }
                        }
                        break;
                    case 2:
                    case 3:
                    case 6:
                    case 7:
                    case 8:
                    case 10:
                    case 11:
                    case 12:
                        for (i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                        {
                            sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                            if (!cmb_section_name.Items.Contains(sec_name))
                            {
                                cmb_section_name.Items.Add(sec_name);
                            }
                        }
                        break;
                }
                if (cmb_section_name.Items.Count > 0) cmb_section_name.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }
        private void frmTower_Load(object sender, EventArgs e)
        {
            try
            {

                this.Text = Title;
                Set_Project_Name();

                sc_design.SplitterDistance = 517;
                splitContainer2.SplitterDistance = 505;
                CalculateLoads();

                cmb_no_levels.SelectedIndex = 2;




                txt_fck.SelectedIndex = 2;
                //complete_design = new CompleteDesign_LS();
                cmb_Shr_Con_Section_name.Items.Clear();
                cmb_Shr_Con_Section_name.Items.AddRange(tbl_rolledSteelChannels.Get_Channels().ToArray());
                if (cmb_Shr_Con_Section_name.Items.Count > 0)
                {
                    cmb_Shr_Con_Section_name.SelectedItem = "ISMC";
                    cmb_Shr_Con_Section_Code.SelectedItem = "150";

                    if (cmb_Shr_Con_Section_name.SelectedItem == null)
                        cmb_Shr_Con_Section_name.SelectedIndex = 0;
                }
                cmb_lac.SelectedIndex = 0;



            }
            catch (Exception exx) { }
        }

        private void cmb_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
            grb_bottom_plate.Enabled = true;
            grb_Top_plate.Enabled = true;
            grb_vertical_stiffener_plate.Enabled = true;

            if (cmb_sections_define.SelectedIndex == 12 || cmb_sections_define.SelectedIndex == 11)
            {
                grb_side_plate.Text = "WEB PLATE";
                grb_bottom_plate.Enabled = false;
                grb_Top_plate.Enabled = false;
                grb_vertical_stiffener_plate.Enabled = false;
            }
            else if (cmb_sections_define.SelectedIndex == 13)
            {
                grb_side_plate.Text = "WEB PLATE";
            }
            else
            {
                grb_side_plate.Text = "SIDE PLATE";

            }
            SetComboSections();

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section1;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();

                    //cmb_section_name.Items.Add("ISLB");
                    //cmb_section_name.Items.Add("ISJB");
                    //cmb_section_name.Items.Add("ISMB");
                    //cmb_section_name.Items.Add("ISWB");
                    //cmb_section_name.Items.Add("ISHB");

                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    cmb_code1.Text = "600";
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "500";
                    txt_tp_thk.Text = "22";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 1:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section2;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "400";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISJC");
                    //cmb_section_name.Items.Add("ISLC");
                    //cmb_section_name.Items.Add("ISMC");


                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);


                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "320";
                    txt_sp_thk.Text = "10";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 2:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section3;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");

                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "350";
                    txt_tp_thk.Text = "25";
                    txt_sp_wd.Text = "420";
                    txt_sp_thk.Text = "16";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 3:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section4;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    txt_tp_width.Text = "350";
                    txt_tp_thk.Text = "25";
                    txt_sp_wd.Text = "420";
                    txt_sp_thk.Text = "16";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_vsp_wd.Text = "120";
                    txt_vsp_thk.Text = "25";
                    break;
                case 4:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section5;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "600";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISLB");
                    //cmb_section_name.Items.Add("ISJB");
                    //cmb_section_name.Items.Add("ISMB");
                    //cmb_section_name.Items.Add("ISWB");
                    //cmb_section_name.Items.Add("ISHB");
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);


                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "1";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "125";
                    txt_bp_thk.Text = "10";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 5:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section6;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISLB");
                    //cmb_section_name.Items.Add("ISJB");
                    //cmb_section_name.Items.Add("ISMB");
                    //cmb_section_name.Items.Add("ISWB");
                    //cmb_section_name.Items.Add("ISHB");
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "1";


                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "490";
                    txt_vsp_thk.Text = "12";
                    break;
                case 6:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section7;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Top_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";


                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 7:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section8;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "12";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "1";


                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 8:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section9;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section9;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "150";
                    cmb_sec_thk.Text = "10";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 9:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section10;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section10;
                    cmb_sec_thk.Visible = false;
                    cmb_code1.Text = "400";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISJC");
                    //cmb_section_name.Items.Add("ISLC");
                    //cmb_section_name.Items.Add("ISMC");
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "320";
                    txt_sp_thk.Text = "10";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 10:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section11;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section111;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "110";
                    cmb_sec_thk.Text = "10";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                case 11:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section12;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section12;
                    cmb_sec_thk.Visible = true;
                    cmb_code1.Text = "110";
                    cmb_sec_thk.Text = "10";
                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "2";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                //Chiranjit [2011 05 17]
                case 12:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section13;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section13;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.Text = "12";
                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "30";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                //Chiranjit [2011 05 18]
                case 13:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section14;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section14;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISA");
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.Text = "12";
                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "320";
                    txt_tp_thk.Text = "16";
                    txt_bp_wd.Text = "320";
                    txt_bp_thk.Text = "16";
                    txt_sp_wd.Text = "700";
                    txt_sp_thk.Text = "16";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
                //Chiranjit [2011 05 18]
                case 14:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section15;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
                    cmb_sec_thk.Visible = true;

                    cmb_section_name.Items.Clear();
                    //cmb_section_name.Items.Add("ISMC");
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    cmb_code1.SelectedItem = "300";
                    cmb_sec_thk.Text = "0";
                    txt_no_ele.Text = "4";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";
                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    break;
            }
            cmb_code1.Width = cmb_sec_thk.Visible ? 93 : 144;

            //SetComboSections();
        }
        private void cmb_select_standard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region Convert Data
                //SectionData secData = null;
                //if (cmb_convert_standard.SelectedIndex == 0)
                //{
                //    for (int i = 0; i < complete_design.Members.Count; i++)
                //    {
                //        secData = complete_design.Members[i].SectionDetails;

                //        if (secData.SectionName.StartsWith("IS"))
                //        {
                //            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref secData);
                //        }
                //        //AddMemberRow(complete_design.Members[i]);
                //    }
                //}
                //else if (cmb_convert_standard.SelectedIndex == 1)
                //{
                //    for (int i = 0; i < complete_design.Members.Count; i++)
                //    {
                //        secData = complete_design.Members[i].SectionDetails;

                //        if (secData.SectionName.StartsWith("UK"))
                //        {
                //            iApp.Tables.Steel_Convert.Convert_BS_to_IS(ref secData);
                //        }
                //        //AddMemberRow(complete_design.Members[i]);
                //    }
                //}
                #endregion Convert Data
                Set_Sections_Standard();
            }
            catch (Exception ex) { }


        }
        private void Set_Sections_Standard()
        {
            grb_bottom_plate.Enabled = true;
            grb_Top_plate.Enabled = true;
            grb_vertical_stiffener_plate.Enabled = true;

            if (cmb_sections_define.SelectedIndex == 12 || cmb_sections_define.SelectedIndex == 13)
            {
                grb_side_plate.Text = "WEB PLATE";
                grb_bottom_plate.Enabled = false;
                grb_Top_plate.Enabled = false;
                grb_vertical_stiffener_plate.Enabled = false;
            }
            else
            {
                grb_side_plate.Text = "SIDE PLATE";

            }
            SetComboSections();

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section1;
                    cmb_sec_thk.Visible = false;
                    cmb_section_name.Items.Clear();
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    break;
                case 1:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section2;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section21;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    break;
                case 2:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section3;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section31;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 3:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section4;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section41;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 4:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section5;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Stringer_Beam;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 5:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section6;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cross_Girder;
                    cmb_sec_thk.Visible = false;
                    tbl_rolledSteelBeams.Read_Beam_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 6:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section7;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Top_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                case 7:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section8;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "1";
                    break;
                case 8:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section9;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section9;
                    cmb_sec_thk.Visible = true;
                    cmb_section_name.Items.Clear();
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    break;
                case 9:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section10;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section10;
                    cmb_sec_thk.Visible = false;

                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";
                    break;
                case 10:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section11;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section111;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "2";
                    break;
                case 11:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section12;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section12;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "2";
                    break;
                //Chiranjit [2011 05 17]
                case 12:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section13;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section13;
                    cmb_sec_thk.Visible = true;

                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;

                    txt_no_ele.Text = "4";
                    break;
                //Chiranjit [2011 05 18]
                case 13:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section14;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section14;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelAngles.Read_Angle_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
                //Chiranjit [2011 05 18]
                case 14:
                    pb_sections.BackgroundImage = global::AstraFunctionOne.ImageCollection.Truss_Section15;
                    //pb_sections.BackgroundImage = global::AstraFunctionOne.Properties.Resources.section15;
                    cmb_sec_thk.Visible = true;
                    tbl_rolledSteelChannels.Read_Channel_Sections(ref cmb_section_name);
                    cmb_section_name.SelectedIndex = cmb_section_name.Items.Count > 0 ? 0 : -1;
                    txt_no_ele.Text = "4";
                    break;
            }

            cmb_code1.Width = cmb_sec_thk.Visible ? 93 : (cmb_select_standard.SelectedIndex == 0 ? 144 : 93);
        }

        private void cmb_sec_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
            cmb_code1.Items.Clear();
            cmb_sec_thk.Items.Clear();
            string sec_name, sec_code;

            switch (cmb_sections_define.SelectedIndex)
            {
                case 0:
                case 4:
                case 5:
                    for (int i = 0; i < tbl_rolledSteelBeams.List_Table.Count; i++)
                    {
                        sec_code = tbl_rolledSteelBeams.List_Table[i].SectionCode;
                        sec_name = tbl_rolledSteelBeams.List_Table[i].SectionName;
                        if (sec_name == cmb_section_name.Text && sec_name != "")
                        {
                            if (cmb_code1.Items.Contains(sec_code) == false)
                            {
                                cmb_code1.Items.Add(sec_code);
                            }
                        }
                    }
                    break;
                case 1:
                case 9:
                    for (int i = 0; i < tbl_rolledSteelChannels.List_Table.Count; i++)
                    {
                        sec_code = tbl_rolledSteelChannels.List_Table[i].SectionCode;
                        sec_name = tbl_rolledSteelChannels.List_Table[i].SectionName;
                        if (sec_name == cmb_section_name.Text && sec_name != "")
                        {
                            if (cmb_code1.Items.Contains(sec_code) == false)
                            {
                                cmb_code1.Items.Add(sec_code);
                            }
                        }
                    }
                    break;
                case 2:
                case 3:
                case 6:
                case 7:
                case 8:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                    {
                        sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                        sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                        if (sec_name == cmb_section_name.Text && sec_name != "")
                        {
                            if (cmb_code1.Items.Contains(sec_code) == false)
                            {
                                cmb_code1.Items.Add(sec_code);
                            }
                        }
                    }
                    break;
            }
            if (cmb_code1.Items.Count > 0) cmb_code1.SelectedIndex = 0;

        }

        private void cmb_code1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
            string sec_code, sec_name, code2;
            double thk = 0.0;
            cmb_sec_thk.Items.Clear();
            for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
            {
                sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                thk = tbl_rolledSteelAngles.List_Table[i].Thickness;
                if (sec_name == cmb_section_name.Text && sec_code == cmb_code1.Text)
                {
                    if (cmb_sec_thk.Items.Contains(thk) == false)
                    {
                        cmb_sec_thk.Items.Add(thk);
                    }
                }
            }
            if (cmb_sec_thk.Items.Count > 0) cmb_sec_thk.SelectedIndex = 0;


            if (cmb_section_name.Text == "ISMC")
            {
                int le = MyList.StringToInt(cmb_code1.Text, 0);
                switch (le)
                {
                    case 100:
                        txt_sp_wd.Text = "" + (le - 18 * 2);
                        break;
                    case 125:
                        txt_sp_wd.Text = "" + (le - 20 * 2);
                        break;
                    case 150:
                        txt_sp_wd.Text = "" + (le - 22 * 2);
                        break;
                    case 175:
                        txt_sp_wd.Text = "" + (le - 24 * 2);
                        break;
                    case 200:
                        txt_sp_wd.Text = "" + (le - 25 * 2);
                        break;
                    case 225:
                        txt_sp_wd.Text = "" + (le - 28 * 2);
                        break;
                    case 250:
                        txt_sp_wd.Text = "" + (le - 30 * 2);
                        break;
                    case 300:
                        txt_sp_wd.Text = "" + (le - 30 * 2);
                        break;
                    case 350:
                        txt_sp_wd.Text = "" + (le - 32 * 2);
                        break;
                    case 400:
                        txt_sp_wd.Text = "" + (le - 35 * 2);
                        break;
                }
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





            if (File.Exists(analysis_file))
            {

                FilePath = Path.GetDirectoryName(file_name);
                string rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
                if (File.Exists(rep_file) && !isCreateData)
                {

                    iApp.Progress_Works.Add("Reading Analysis Data from Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");
                    Truss_Analysis = new BridgeMemberAnalysis(iApp, rep_file, GetForceType());
                    iApp.Progress_Works.Clear();

                    //Truss_Analysis = new BridgeMemberAnalysis(iApp, rep_file);
                }
                else
                    Truss_Analysis = new BridgeMemberAnalysis(iApp, analysis_file);

                if (Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count == 0)
                {
                    MessageBox.Show(this, "Member Groups are not found in data file.\nPlease define Group Defination in data file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Process.Start(analysis_file);
                    return;
                }
                if (cmb_mem_group.Items.Count > 0)
                    cmb_mem_group.SelectedIndex = 0;
                //SetCompleteDesign(Path.Combine(Path.GetDirectoryName(analysis_file), "MEMBER_LOAD_DATA.txt"));
               
                if (isCreateData) dgv_mem_details.Rows.Clear();

                if (dgv_mem_details.RowCount == 0)
                {
                    FillMemberGroup();
                }

                //string kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
                string kFile = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");
                if (!File.Exists(kFile))
                {
                    kFile = Path.Combine(system_path, "MEMBER_LOAD_REP.txt");
                }

                if (!File.Exists(kFile))
                {
                    kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
                }
                if (File.Exists(kFile))
                {
                    if (!isCreateData)
                        SetCompleteDesign(kFile);
                }
                if (!isCreateData)
                {

                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(this, "Analysis Input Data file opened successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
        }
        void SetCompleteDesign(string file_name)
        {
            try
            {

                //complete_design = new CompleteDesign();
                //complete_design.DeadLoads.ReadFromFile(file_name);
                if (File.Exists(file_name))
                {
                    complete_design = new TowerDesign();
                     
                    complete_design.ReadFromFile(file_name);
                    SetMemberDetails();
                    dgv_mem_details.Rows.Clear();
                    if (complete_design.Members.Count > 0)
                    {
                        foreach (CMember mem in complete_design.Members)
                        {
                            AddMemberRow(mem);
                        }
                    }
                   

                    List<string> lst = new List<string>(File.ReadAllLines(file_name));
                    string kStr = "";
                    MyList mlist = null;
                    int j = 0;
                    string entity_name = "";
                    double dd = 0.0;
                    bool flag = false;
                    bool flag2 = false;

                    for (int i = 0; i < lst.Count; i++)
                    {
                        kStr = MyList.RemoveAllSpaces(lst[i]).ToUpper();
                        mlist = new MyList(kStr, ' ');
                        if (kStr.StartsWith("NAME"))
                        {
                            flag2 = true; i++;
                            continue;
                        }
                        if (flag2)
                        {
                            if (kStr.Contains("------------------------------------------------"))
                            {
                                flag = true;
                                flag2 = false;
                                continue;
                            }
                        }

                        if (flag)
                        {
                            if (kStr.Contains("------------------------------------------------"))
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
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
        public void Set_Sections(string group_name)
        {

            switch (group_name)
            {
                case "_L0L1":
                case "_L1L2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";

                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2L3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "300";
                    txt_vsp_thk.Text = "10";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";

                    break;
                case "_L3L4":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "300";
                    txt_vsp_thk.Text = "25";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";
                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4L5":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_vsp_wd.Text = "300";
                    txt_vsp_thk.Text = "30";
                    txt_sp_wd.Text = "600";
                    txt_sp_thk.Text = "10";
                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";
                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U1U2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "420";
                    txt_tp_thk.Text = "16";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U2U3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "420";
                    txt_tp_thk.Text = "16";

                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "16";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U3U4":

                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "400";
                    txt_tp_thk.Text = "16";

                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "25";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_U4U5":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 12.0d;

                    txt_tp_width.Text = "420";
                    txt_tp_thk.Text = "16";

                    txt_sp_wd.Text = "350";
                    txt_sp_thk.Text = "30";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L1U1":

                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "200";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "400";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U4":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "240";
                    txt_bp_thk.Text = "10";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L5U5":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "150";
                    //cmb_sec_thk.SelectedItem = "12";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_ER":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    cmb_code1.SelectedItem = "150150";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "430";
                    txt_sp_thk.Text = "12";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U1":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "400";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "320";
                    txt_sp_thk.Text = "10";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U2":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "220";
                    txt_sp_thk.Text = "10";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U3":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_L5U4":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    cmb_code1.SelectedItem = "300";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "400";
                    break;
                case "_TCS_ST":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section7;
                    cmb_code1.SelectedItem = "9090";
                    cmb_sec_thk.SelectedItem = 8.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "180";
                    break;
                case "_TCS_DIA":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section7;
                    cmb_code1.SelectedItem = "9090";
                    cmb_sec_thk.SelectedItem = 8.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "180";
                    break;
                case "_BCB":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    cmb_code1.SelectedItem = "9090";
                    cmb_sec_thk.SelectedItem = 10.0d;

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "0";
                    txt_bp_thk.Text = "0";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_STRINGER":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section5;
                    cmb_code1.SelectedItem = "450";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "150";
                    txt_bp_thk.Text = "40";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    cmb_code1.SelectedItem = "600";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "350";
                    txt_bp_thk.Text = "32";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_IN":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    cmb_code1.SelectedItem = "600";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "0";
                    txt_vsp_thk.Text = "0";

                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_END":
                    cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    cmb_code1.SelectedItem = "600";
                    //cmb_sec_thk.SelectedItem = "10";

                    txt_tp_width.Text = "0";
                    txt_tp_thk.Text = "0";

                    txt_sp_wd.Text = "0";
                    txt_sp_thk.Text = "0";

                    txt_vsp_wd.Text = "490";
                    txt_vsp_thk.Text = "12";

                    txt_bp_wd.Text = "300";
                    txt_bp_thk.Text = "20";
                    txt_sec_lat_spac.Text = "0";
                    break;
            }
        }
        void FillMemberGroup()
        {
            if (Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count == 0) return;

            string kStr = "";
            cmb_mem_group.Items.Clear();
            int i = 0;
            CMember member = null;
            for (i = 0; i < Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count; i++)
            {
                kStr = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].GroupName;

                member = new CMember(iApp);
                member.Group = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i];
                //member.Group.GroupName = kStr;
                //member.Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].MemberNosText;
                member.Group.SetMemNos();
                member.MemberType = Get_MemberType(kStr);
                member.WeightPerMetre = GetWeightPerMetre();

                member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);

                member.MemLengths = Truss_Analysis.Analysis.Members.Get_Member_Lengths(member.Group.MemberNosText);

                member.Force = Truss_Analysis.GetForce(ref member, true);

                Add_SectionsData(ref member);
                member.SectionDetails.TopPlate.Length = member.Length;
                member.SectionDetails.BottomPlate.Length = member.Length;
                member.SectionDetails.SidePlate.Length = member.Length;
                member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;




                complete_design.Members.Add(member);
                AddMemberRow(member);

                if (!cmb_mem_group.Items.Contains(kStr))
                    cmb_mem_group.Items.Add(kStr);

            }

            if (cmb_mem_group.Items.Count > 0) cmb_mem_group.SelectedIndex = 0; //Chiranjit [2013 06 07] Kolkata

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


        void SetMemberLength()
        {
            if (Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count == 0) return;

            string kStr = "";
            cmb_mem_group.Items.Clear();
            int i = 0;
            CMember member = null;
            for (i = 0; i < Truss_Analysis.Analysis.MemberGroups.GroupCollection.Count; i++)
            {
                kStr = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].GroupName;

                member = new CMember(iApp);
                member.Group.GroupName = kStr;
                member.Group.MemberNosText = Truss_Analysis.Analysis.MemberGroups.GroupCollection[i].MemberNosText;
                member.Group.SetMemNos();
                member.MemberType = Get_MemberType(kStr);
                member.WeightPerMetre = GetWeightPerMetre();


                member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);

                member.Force = Truss_Analysis.GetForce(ref member);

                //Add_SectionsData(ref member);
                member.SectionDetails.TopPlate.Length = member.Length;
                member.SectionDetails.BottomPlate.Length = member.Length;
                member.SectionDetails.SidePlate.Length = member.Length;
                member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;


                complete_design.Members.Add(member);
                AddMemberRow(member);

                if (!cmb_mem_group.Items.Contains(kStr))
                    cmb_mem_group.Items.Add(kStr);

            }
        }
        public void AddMemberRow(CMember member)
        {
            if (member.Group.GroupName == "") return;
            string kStr = "";
            try
            {
                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    member.Force = Truss_Analysis.GetForce(ref member, true);
                else
                {
                    member.Force = Truss_Analysis.GetForce(ref member, true);
                }

                member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(member.Group.MemberNosText);
                member.MemLengths = Truss_Analysis.Analysis.Members.Get_Member_Lengths(member.Group.MemberNosText);

                member.Group.SetMemNos();
                member.WeightPerMetre = GetWeightPerMetre(member);
                bool flag = false;
                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    kStr = dgv_mem_details[0, i].Value.ToString();

                    if (kStr == member.Group.GroupName)
                    {
                        dgv_mem_details[0, i].Value = member.Group.GroupName;
                        dgv_mem_details[1, i].Value = member.Group.GroupType = (member.MemberType == eMemberType.CrossGirder || member.MemberType == eMemberType.StringerBeam) ? ASTR.MemberGroup.eMType.BEAM : ASTR.MemberGroup.eMType.TRUSS;
                      
                        //dgv_mem_details[2, i].Value = (member.MemberType == eMemberType.NoSelection) ? "" : member.MemberType.ToString();

                        dgv_mem_details[2, i].Value = Get_Tower_Member(member.Group.GroupName).ToString(); ;

                        dgv_mem_details[3, i].Value = (member.SectionDetails.DefineSection == eDefineSection.NoSelection) ? "" : member.SectionDetails.DefineSection.ToString();
                        dgv_mem_details[4, i].Value = member.NoOfMember.ToString("0");
                        dgv_mem_details[5, i].Value = member.Length.ToString("0.000");
                        dgv_mem_details[6, i].Value = member.WeightPerMetre.ToString("0.0000");
                        dgv_mem_details[7, i].Value = member.Weight.ToString("0.0000");
                        dgv_mem_details[8, i].Value = member.Force;
                        return;
                    }
                }
                dgv_mem_details.Rows.Add(
                               member.Group.GroupName,
                               member.Group.GroupType = (member.MemberType == eMemberType.CrossGirder || member.MemberType == eMemberType.StringerBeam) ? ASTR.MemberGroup.eMType.BEAM : ASTR.MemberGroup.eMType.TRUSS,
                               //(member.MemberType == eMemberType.NoSelection) ? "" : member.MemberType.ToString(),
                               Get_Tower_Member(member.Group.GroupName).ToString(),
                               (member.SectionDetails.DefineSection == eDefineSection.NoSelection) ? "" : member.SectionDetails.DefineSection.ToString(),
                               member.NoOfMember.ToString("0"),
                               member.Length.ToString("0.000"),
                               member.WeightPerMetre.ToString("0.0000"),
                               member.Weight.ToString("0.0000"),
                               member.Force);


                dgv_mem_details.FirstDisplayedScrollingRowIndex = dgv_mem_details.RowCount - 1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(kStr + " :" + ex.Message);
            }


        }



        public eMemberType Get_MemberType(string groupName)
        {
            return eMemberType.TowerMember;

            //return eMemberType.NoSelection;
        }


        public double GetWeightPerMetre(CMember m)
        {
            double wt_p_m = 0.0;

            if (m.SectionDetails.SectionName.EndsWith("B"))
            {
                wt_p_m = iApp.Tables.Get_BeamData_FromTable(m.SectionDetails.SectionName, m.SectionDetails.SectionCode).Weight;
            }
            else if (m.SectionDetails.SectionName.EndsWith("C")) // Channel
            {
                wt_p_m = iApp.Tables.Get_ChannelData_FromTable(m.SectionDetails.SectionName, m.SectionDetails.SectionCode).Weight;
            }
            else if (m.SectionDetails.SectionName.EndsWith("A"))
            {
                wt_p_m = iApp.Tables.Get_AngleData_FromTable(m.SectionDetails.SectionName, m.SectionDetails.SectionCode, m.SectionDetails.AngleThickness).Weight;
            }

            return wt_p_m * 0.001; // Convert Newton to
        }
        public double GetWeightPerMetre()
        {
            double wt_p_m = 0.0;
            if (cmb_sections_define.SelectedIndex == 0 ||
                cmb_sections_define.SelectedIndex == 4 ||
                cmb_sections_define.SelectedIndex == 5) // Beam
            {
                wt_p_m = iApp.Tables.Get_BeamData_FromTable(cmb_section_name.Text, cmb_code1.Text).Weight;
                //wt_p_m = wt_p_m * 0.001;
            }
            else if (cmb_sections_define.SelectedIndex == 1) // Channel
            {
                wt_p_m = iApp.Tables.Get_ChannelData_FromTable(cmb_section_name.Text, cmb_code1.Text).Weight;
                //wt_p_m = wt_p_m * 0.001;
            }
            else if (cmb_sections_define.SelectedIndex == 2 ||
                cmb_sections_define.SelectedIndex == 3 ||
                cmb_sections_define.SelectedIndex == 6 ||
                cmb_sections_define.SelectedIndex == 7)
            {
                wt_p_m = iApp.Tables.Get_AngleData_FromTable(cmb_section_name.Text, cmb_code1.Text, MyList.StringToDouble(cmb_sec_thk.Text, 0.0)).Weight;
                //wt_p_m = wt_p_m * 0.001;
            }
            return wt_p_m * 0.001;
        }
       
        public void Add_SectionsData(ref CMember member)
        {
            member.SectionDetails.TopPlate.TotalPlates = 1;
            member.SectionDetails.BottomPlate.TotalPlates = 1;
            member.SectionDetails.SidePlate.TotalPlates = 2;
            member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;


            //member.SectionDetails.DefineSection = eDefineSection.Section8;
            //member.SectionDetails.SectionName = "ISA";
            //member.SectionDetails.SectionCode = "9090";
            //member.SectionDetails.AngleThickness = 8d;

            //if (false)
            {
                switch (member.Group.GroupName)
                {
                    #region _CRW_MAINPOST
                    case "_CRW_MAINPOST":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "9090";
                        member.SectionDetails.AngleThickness = 8d;

                        break;

                    case "_CRW_BRACINGS":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "7575";
                        member.SectionDetails.AngleThickness = 8d;
                        break;

                    case "_LEV1_MAINPOST":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "9090";
                        member.SectionDetails.AngleThickness = 10d;
                        break;


                    case "_LEV1_LOWER_CROSS_BRACINGS":
                    case "_LEV1_UPPER_CROSS_BRACINGS":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "7575";
                        member.SectionDetails.AngleThickness = 8d;
                        break;

                    case "_LEV1_INTERNAL_LOWERBRACINGS":
                    case "_LEV1_INTERNALUPPERBRACINGS":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "6565";
                        member.SectionDetails.AngleThickness = 6d;
                        break;
                    case "_LEV1_UPPER":
                    case "_LEV1_WING_BRACINGS":
                    case "_LEV1_LEFT_WING_TOP":
                    case "_LEV1_LEFT_WING_BOTTOM":
                    case "_LEV1_LEFT_WING_SIDEBRACINGS":
                    case "_LEV1_LEFT_WING_TOPBRACINGS":
                    case "_LEV1_LEFTWINGBOTTOMBRACINGS":
                    case "_LEV1_RIGHTWINGTOP":
                    case "_LEV1_RIGHTWINGBOTTOM":
                    case "_LEV1_RIGHTWINGSIDEBRACINGS":
                    case "_LEV1_RIGHTWINGTOPBRACINGS":
                    case "_LEV1RIGHTWINGBOTTOMBRACINGS":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "6565";
                        member.SectionDetails.AngleThickness = 6d;
                        break;
                    case "_LEV2_MAINPOST":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "9090";
                        member.SectionDetails.AngleThickness = 10d;
                        break;
                    case "_LEV2_LOWERCROSSBRACINGS":
                    case "_LEV2_UPPERCROSSBRACINGS":
                    case "_LEV2_LOWERINTERNALBRACINGS":
                    case "_LEV2_UPPERINTERNALBRACINGS":
                    case "_LEV2_LEFTWINGSIDEBRACINGS":
                    case "_LEV2_LEFTWINGTOPBRACINGS":
                    case "_LEV2_LEFTWINGBOTTOMBRACINGS":
                    case "_LEV2_RIGHTWINGSIDEBRACINGS":
                    case "_LEV2_WINGTOP":
                    case "_LEV2_BOX":
                    case "_LEV2_LEFTWINGBOTTOM":
                    case "_LEV2_RIGHTWINGTOP":
                    case "_LEV2_RIGHTWINGBOTTOM":
                    case "_LEV2_RIGHTWINGTOPBRACINGS":
                    case "_LEV2RIGHTWINGBOTTOMBRACINGS":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "6565";
                        member.SectionDetails.AngleThickness = 6d;
                        break;
                    case "_LEV3_MAINPOST":
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "9090";
                        member.SectionDetails.AngleThickness = 10d;
                        break;
                    case "_LEV3_UPPER_BRACINGS":
                    case "_LEV3_BOX":
                    case "_LEV3RIGHTWINGBOTTOMBRACINGS":
                    case "_LEV3_RIGHTWINGTOPBRACINGS":
                    case "_LEV3_RIGHTWINGSIDEBRACINGS":
                    case "_LEV3_RIGHTWINGTOP":
                    case "_LEV3_RIGHTWINGBOTTOM":
                    case "_LEV3LEFTWINGSIDEBRACINGS":
                    case "_LEV3LEFTWINGTOPBRACINGS":
                    case "_LEV3LEFTWINGBOTTOMBRACINGS":
                    case "_LEV3LEFTWINGTOP":
                    case "_LEV3LEFTWINGBOTTOM":

                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "6565";
                        member.SectionDetails.AngleThickness = 6d;
                        break;

                    default:
                        member.SectionDetails.DefineSection = eDefineSection.Section8;
                        member.SectionDetails.SectionName = "ISA";
                        member.SectionDetails.SectionCode = "7575";
                        member.SectionDetails.AngleThickness = 8d;
                        break;

                    #endregion _CRW_MAINPOST

                }
            }

            member.SectionDetails.TopPlate.Width = 0.0;
            member.SectionDetails.TopPlate.Thickness = 0.0;


            member.SectionDetails.BottomPlate.Width = 0.0;
            member.SectionDetails.BottomPlate.Thickness = 0.0;

            member.SectionDetails.SidePlate.Width = 0.0;
            member.SectionDetails.SidePlate.Thickness = 0.0;

            member.SectionDetails.VerticalStiffenerPlate.Width = 0.0;
            member.SectionDetails.VerticalStiffenerPlate.Thickness = 0.0;

            member.SectionDetails.LateralSpacing = 0.0;
            member.SectionDetails.NoOfBolts = 2;
            member.SectionDetails.BoltDia = 20;
            member.SectionDetails.NoOfElements = 1.0;

        }

        void ShowMemberNos(string group_name)
        {

            string kStr = "";
            ASTR.MemberGroup mGrp = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(group_name);
            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                
            if (mGrp != null)
            {
                txt_cd_mem_no.Text = mGrp.MemberNosText;
            }
        }

        private void cmb_mem_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string mem_name = cmb_mem_group.SelectedItem.ToString();
                ShowMemberNos(mem_name);
                //cmb_cd_mem_type.SelectedIndex = (int)Get_MemberType(cmb_mem_group.SelectedItem.ToString());
                Set_Sections(mem_name);

                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    dgv_mem_details.Rows[i].Selected = false;
                }

                for (int i = 0; i < dgv_mem_details.RowCount; i++)
                {
                    if (dgv_mem_details[0, i].Value.ToString().ToUpper() == (cmb_mem_group.SelectedItem.ToString().ToUpper()))
                    {
                        dgv_mem_details.Rows[i].Selected = true;
                        //dgv_mem_details.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex) { }

        }

        private void dgv_mem_details_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //timer1.Stop();
            //btn_add_to_list.ForeColor = Color.Black;

            int indx = e.RowIndex;
            //Select_Member_Group(e.RowIndex);
            //Select_Member_Group(dgv_mem_details[e.ColumnIndex, e.RowIndex].Value.ToString());
            //if ((sender as DataGridView).Name == dgv_member_Result.Name)
            //{

            //    for (int i = 0; i < dgv_mem_details.RowCount; i++)
            //    {
            //        if (dgv_mem_details[0, i].Value.ToString().ToUpper() == (dgv_member_Result[0, e.RowIndex].Value.ToString().ToUpper()))
            //        {
            //            dgv_mem_details.Rows[i].Selected = true;
            //            dgv_mem_details.FirstDisplayedScrollingRowIndex = i;

            //            indx = i;
            //            break;
            //        }
            //    }
            //}
            Select_Member_Group(indx);
        }

        void Select_Member_Group(int indx)
        {
            //int indx = e.RowIndex;
            string memNo = "";
            try
            {
                memNo = dgv_mem_details[0, indx].Value.ToString();
                if (memNo != "")
                {
                    indx = Complete_Design.Members.IndexOf(memNo);
                    if (indx == -1) return;

                    cmb_mem_group.SelectedItem = Complete_Design.Members[indx].Group.GroupName;
                    //cmb_cd_mem_type.SelectedIndex = (int)Complete_Design.Members[indx].MemberType;
                    txt_cd_mem_type.Text = Complete_Design.Members[indx].MemberType.ToString();


                    txt_cd_mem_type.Text = Get_Tower_Member(Complete_Design.Members[indx].Group.GroupName).ToString();

                }



                txt_cd_mem_no.Text = Complete_Design.Members[indx].Group.MemberNosText;


                cmb_sections_define.SelectedIndex = ((int)Complete_Design.Members[indx].SectionDetails.DefineSection);

                //cmb_select_standard.SelectedIndex = Complete_Design.Members[indx].SectionDetails.SectionName.StartsWith("UK") ? 0 : 1;



                if (Complete_Design.Members[indx].SectionDetails.SectionName.StartsWith("UK"))
                {
                    cmb_select_standard.SelectedIndex = 0;

                }
                else if (Complete_Design.Members[indx].SectionDetails.SectionName.StartsWith("I"))
                {
                    cmb_select_standard.SelectedIndex = 1;

                }
                else 
                {
                    cmb_select_standard.SelectedIndex = 2;

                }
                cmb_section_name.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionName;



                cmb_code1.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionCode;
                //if (cmb_sec_thk.Visible)
                cmb_sec_thk.SelectedItem = Complete_Design.Members[indx].SectionDetails.AngleThickness;

                txt_no_ele.Text = Complete_Design.Members[indx].SectionDetails.NoOfElements.ToString();


                txt_tp_width.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Width.ToString("0");
                txt_tp_thk.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Thickness.ToString("0");
                txt_bp_wd.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Width.ToString("0");
                txt_bp_thk.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Thickness.ToString("0");

                txt_sp_wd.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Width.ToString("0");
                txt_sp_thk.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Thickness.ToString("0");
                txt_vsp_wd.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Width.ToString("0");
                txt_vsp_thk.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Thickness.ToString("0");

                txt_sec_lat_spac.Text = Complete_Design.Members[indx].SectionDetails.LateralSpacing.ToString();
                txt_sec_bolt_dia.Text = Complete_Design.Members[indx].SectionDetails.BoltDia.ToString();
                txt_sec_nb.Text = Complete_Design.Members[indx].SectionDetails.NoOfBolts.ToString();
            }
            catch (Exception ex) { }
        }
        public TowerDesign Complete_Design
        {
            get
            {
                return complete_design;
            }
        }
        void Select_Member_Group(string member_name)
        {
            int indx = -1;
            string memNo = member_name;
            try
            {
                if (memNo != "")
                {
                    indx = Complete_Design.Members.IndexOf(memNo);
                    if (indx == -1) return;

                    cmb_mem_group.SelectedItem = memNo;
                    //txt_cd_mem_type.Text = (int)Complete_Design.Members[indx].MemberType;
                    txt_cd_mem_type.Text = Complete_Design.Members[indx].MemberType.ToString();
                }

                txt_cd_mem_no.Text = Complete_Design.Members[indx].Group.MemberNosText;

                cmb_sections_define.SelectedIndex = ((int)Complete_Design.Members[indx].SectionDetails.DefineSection);
                cmb_section_name.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionName;
                cmb_code1.SelectedItem = Complete_Design.Members[indx].SectionDetails.SectionCode;
                if (cmb_sec_thk.Visible)
                    cmb_sec_thk.SelectedItem = Complete_Design.Members[indx].SectionDetails.AngleThickness;

                txt_no_ele.Text = Complete_Design.Members[indx].SectionDetails.NoOfElements.ToString();

                txt_tp_width.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Width.ToString("0");
                txt_tp_thk.Text = Complete_Design.Members[indx].SectionDetails.TopPlate.Thickness.ToString("0");
                txt_bp_wd.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Width.ToString("0");
                txt_bp_thk.Text = Complete_Design.Members[indx].SectionDetails.BottomPlate.Thickness.ToString("0");

                txt_sp_wd.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Width.ToString("0");
                txt_sp_thk.Text = Complete_Design.Members[indx].SectionDetails.SidePlate.Thickness.ToString("0");
                txt_vsp_wd.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Width.ToString("0");
                txt_vsp_thk.Text = Complete_Design.Members[indx].SectionDetails.VerticalStiffenerPlate.Thickness.ToString("0");

                txt_sec_lat_spac.Text = Complete_Design.Members[indx].SectionDetails.LateralSpacing.ToString();
                txt_sec_bolt_dia.Text = Complete_Design.Members[indx].SectionDetails.BoltDia.ToString();
                txt_sec_nb.Text = Complete_Design.Members[indx].SectionDetails.NoOfBolts.ToString();

                //dgv_mem_details.FirstDisplayedScrollingRowIndex = dgv_mem_details.RowCount - 1;
            }
            catch (Exception ex) { }
        }



        private void btn_update_Click(object sender, EventArgs e)
        {
            //Data_Convert_and_Update_IS_TO_BS();
            //return;

            try
            {
                #region Convert Data
                SectionData secData = null;
                if (cmb_convert_standard.SelectedIndex == 0)
                {
                    for (int i = 0; i < complete_design.Members.Count; i++)
                    {
                        secData = complete_design.Members[i].SectionDetails;

                        if (secData.SectionName.StartsWith("IS"))
                        {
                            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref secData);
                        }
                        AddMemberRow(complete_design.Members[i]);
                    }
                }
                else if (cmb_convert_standard.SelectedIndex == 1)
                {
                    for (int i = 0; i < complete_design.Members.Count; i++)
                    {
                        secData = complete_design.Members[i].SectionDetails;

                        if (secData.SectionName.StartsWith("UK"))
                        {
                            iApp.Tables.Steel_Convert.Convert_BS_to_IS(ref secData);
                        }
                        AddMemberRow(complete_design.Members[i]);
                    }
                }
                #endregion Convert Data
            }
            catch (Exception ex) { }


            try
            {
                string str = MyList.RemoveAllSpaces(txt_cd_mem_no.Text).ToUpper();
                CMember m = null;
                str = str.Replace(',', ' ');
                str = MyList.RemoveAllSpaces(str);
                MyList mList = new MyList(str, ' ');

                m = GetMemberData();
             
                int indx = complete_design.Members.IndexOf(m);
                if (indx != -1)
                {
                    //dgv_mem_details.Rows.RemoveAt(dgv_mem_details.CurrentRow.Index);
                    //DeleteMember(m.Group.GroupName);
                    complete_design.Members[indx] = m;
                }
                else
                    complete_design.Members.Add(m);

                AddMemberRow(m);
                //Show_Total_Weight();
                //MessageBox.Show(m.Group.GroupName + " updated.", "ASTRA");
                timer1.Stop();
                Update_Section_Properties();
                MessageBox.Show(this, "All Member Sections are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //prss = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Show_Total_Weight();
            }
        }


        public bool SaveMemberLoads(string file_name)
        {
            bool success = false;
            //string title_line1, title_line2;
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                complete_design.ToStream(sw);
                success = true;
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            return success;
        }

        private void Update_Section_Properties()
        {
            //Chiranjit [2012 07 13]

            string file_name = INPUT_FILE;

            if (!File.Exists(file_name))
            {
                MessageBox.Show(this, "The Analysis Input data File is not created. \n\n" +
                                    "In Tab 'Structure Geometry' the button 'Create Analysis Input data File' " +
                                    "is to be used for creating the Analysis Input data\",",
                                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string load_file = Path.Combine(Path.GetDirectoryName(file_name), "MEMBER_LOAD_DATA.txt");

            //if (!SaveMemberLoads(load_file)) return;

            load_file = Path.Combine(user_path, "MEMBER_LOAD_DATA.txt");
            system_path = user_path;

            if (!SaveMemberLoads(load_file)) return;

            //iApp.LiveLoads.Save_LL_TXT(Path.GetDirectoryName(file_name), false);



            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;

            List<string> mem_lst = new List<string>();
            List<string> load_lst = new List<string>();


            List<string> data = new List<string>();

            //cmb_long_open_file.SelectedIndex = 0;



            #region Indian

            //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                for (i = 0; i < inp_file_cont.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                    mlist = new MyList(kStr, ' ');

                    //if (kStr.Contains("LOAD GEN"))
                    //    isMoving_load = true;
                    mem_lst = new List<string>();

                    data.Add(inp_file_cont[i]);

                    if (kStr.StartsWith("MEMBER PROPER") || kStr.StartsWith("SECTION PROPER"))
                    {
                        break;
                      
                    }
                }

                Hashtable hh = new Hashtable();
                List<int> ls_mems = new List<int>();

              
                for (i = 0; i < Complete_Design.Members.Count; i++)
                {

                    Complete_Design.Members[i].iApp = iApp;
                    var item = Complete_Design.Members[i].Group.GroupName;
                    kStr = (string.Format("{0}   PRI  AX  {1:f6}  IX  {2:E6}  IY   {3:E6}  IZ  {4:E6}",
                           item,
                           Complete_Design.Members[i].Area,
                           Complete_Design.Members[i].IXX,
                           Complete_Design.Members[i].IXX,
                          Complete_Design.Members[i].IXX + Complete_Design.Members[i].IXX));

                    data.Add(kStr);


                    //foreach (var item in Complete_Design.Members[i].Group.MemberNos)
                    //{
                    //    if (!ls_mems.Contains(item))
                    //    {
                    //        ls_mems.Add(item);
                    //        kStr = (string.Format("{0}   PRI  AX  {1:f6}  IX  {2:E6}  IY   {3:E6}  IZ  {4:E6}",
                    //       item,
                    //       Complete_Design.Members[i].Area,
                    //       Complete_Design.Members[i].IXX,
                    //       Complete_Design.Members[i].IXX,
                    //      Complete_Design.Members[i].IXX + Complete_Design.Members[i].IXX));

                    //        hh.Add(item, kStr);
                    //    }
                    //}

                    //inp_file_cont[i + j] = mem_lst[j];
                }

                load_lst = new List<string>();

                //ls_mems.Sort();

                //foreach (var item in ls_mems)
                //{
                //    kStr = hh[item].ToString();
                //    data.Add(kStr);
                //}

                data.Add(string.Format("CONSTANTS"));
                data.Add(string.Format("E STEEL ALL"));
                data.Add(string.Format("DEN STEEL ALL"));
                data.Add(string.Format("POISSON STEEL ALL"));
                data.Add(string.Format("SUPPORTS"));
                //data.Add(string.Format("1 TO 4 PINNED"));
                data.Add(string.Format("1 TO 4 FIXED"));
                foreach (var item in Get_Load_Data())
                {
                    data.Add(item);
                }

                
                //data.Add(string.Format("SEISMIC COEFFICIENT {0}" , txt_loadcase_seismic.Text));
                data.Add(string.Format("SEISMIC COEEFICIENT {0}", txt_loadcase_seismic.Text));
                data.Add(string.Format("FINISH"));


                File.WriteAllLines(file_name, data.ToArray());
                try
                {
                    //Open_Data_File(file_name);

                    IsFlag = false;
                }
                catch (Exception exx) { }
            }
            #endregion

            Write_All_Data();
        }


        public List<string> Get_Load_Data()
        {
            List<string> list = new List<string>();



            string END_1, END_2, END_3, END_4;


            TowerLoadCase tlc = new TowerLoadCase();


            CalculateLoads(ref tlc);
            //tlc.Wind.Wind_Load_per_Node = 0.389;

            END_1 = "328 247 140";
            END_2 = "328 247 140";
            END_3 = "331 248 143";
            END_4 = "331 248 143";



            string WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 214 231 247 248 263 305 308 310 313 328 329 331 344 375 377 403 ";
            string WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 212 231 232 247 248 305 TO 307 313 TO 315 328 331 375 376 403 ";


            string SEISMIC_FZ = "1 TO 4 ";

            string SEISMIC_FZ1 = "125 127 156 158";
            string SEISMIC_FZ2 = "231 232 263 264";
            string SEISMIC_FZ3 = "313 315 344 346";
            string SEISMIC_FZ4 = "375 376 377 378";



             //247,  143, 248
            if (cmb_no_levels.SelectedIndex == 0)
            {
                END_1 = "140";
                END_2 = "140";
                END_3 = "143";
                END_4 = "143";


                WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 223 ";
                WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 223 ";



                SEISMIC_FZ1 = "125 127 156 158";
                SEISMIC_FZ2 = "";
                SEISMIC_FZ3 = "";
                SEISMIC_FZ4 = "191 193 196 198";


            }
            else if (cmb_no_levels.SelectedIndex == 1)
            {
                END_1 = "140 247";
                END_2 = "140 247";
                END_3 = "143 248";
                END_4 = "143 248";

                WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 214 231 247 248 263 305 308 310 337 ";
                WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 212 231 232 247 248 305 TO 307 337 ";



                SEISMIC_FZ1 = "125 127 156 158";
                SEISMIC_FZ2 = "231 232 263 264";
                SEISMIC_FZ3 = "";
                SEISMIC_FZ4 = "305 307 310 312";
            }




            string WING_FX_COMMENT = string.Format("*Wind Load in FX in all {0} Nodes", MyList.Get_Array_Intiger(WIND_FX).Count);
            string WING_FZ_COMMENT = string.Format("*Wind Load in FZ in all {0} Nodes", MyList.Get_Array_Intiger(WIND_FZ).Count);



            //list.Add(WING_FZ_COMMENT);


            #region All Load Combinations

            #region Load 1 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 1 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            list.Add(string.Format("JOINT LOAD"));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));


            //list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind_Force));


            #endregion Load 1 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY

            //list.Add(string.Format("Load 2"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2 & 3 on all Wing Tips only"));
            //list.Add(string.Format(""));

            #region Load 2 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 2 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));


            //list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind_Force));


            #endregion Load 2

            //list.Add(string.Format("Load 3"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 3 & 4 on all Wing Tips only"));

            #region LOAD 3 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 3 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            //list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            //list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));


            //list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind_Force));


            #endregion Load 3

            //list.Add(string.Format("Load 4"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1 & 3 on all Wing Tips only"));

            #region LOAD 4 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 4 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));


            //list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind_Force));


            #endregion Load 3

            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 5"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FZ in all 30 Nodes"));


            #region LOAD 5 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY , Wind Load in FZ
            list.Add(string.Format("LOAD 5 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY, Wind Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FZ_COMMENT);
            //list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 3





            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 6"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FZ in all 30 Nodes"));



            #region LOAD 6 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 6 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY, Wind Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FZ_COMMENT);
            list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 3





            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 7"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FZ in all 30 Nodes"));




            #region LOAD 7 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 7 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY, Wind Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            //list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            //list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FZ_COMMENT);
            list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 7

            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 8"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FZ in all 30 Nodes"));


            #region LOAD 8 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 8 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY, Wind Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON ENDS 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FZ_COMMENT);
            list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 8


            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 9"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));



            #region LOAD 9 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 9 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY, Wind Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 9



            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 10"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));


            #region LOAD 10 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 10 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY, Wind Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 10


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 11"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));


            #region LOAD 11 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 11 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY, Wind Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            //list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            //list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 3


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 12"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));


            #region LOAD 12 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 12 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY, Wind Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 3



            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 13"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format(""));


            //tlc.Seismic.Q1 = 31.37;


            #region LOAD 13 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 13 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY, Seismic Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));

            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }

            #endregion Load 3


            //list.Add(string.Format("Load 14"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format(""));



            #region LOAD 14 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 14 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY, Seismic Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            //list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }

            #endregion Load 3



            //list.Add(string.Format("Load 15"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format(""));




            #region LOAD 15 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 15 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY, Seismic Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            //list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            //list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            //list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 15



            //list.Add(string.Format("Load 16"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FZ in all 4 Nodes at Bottom Support"));





            #region LOAD 16 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 16 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY, Seismic Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            //list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 16

            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 17"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));




            #region LOAD 17 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 17 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));


            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 17


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 18"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format(""));


            #region LOAD 18 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 18 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 18



            //list.Add(string.Format("Load 19"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));



            #region LOAD 19 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 19 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }




            #endregion Load 19


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 20"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));



            #region LOAD 20 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY
            list.Add(string.Format("LOAD 20 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 20


            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 21"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));


            #region LOAD 21 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY

            list.Add(string.Format("LOAD 21 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2, 3 & 4 ON ALL WING TIPS ONLY, Wind Load in FX, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));


            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));

            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));



            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 21


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 22"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 2 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));




            #region LOAD 22 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY

            list.Add(string.Format("LOAD 22 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY, Wind Load in FX, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            list.Add(string.Format("*CABLE LOADS IN FX ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 2 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));


            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));

            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));


            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }

            #endregion Load 22




            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 23"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1, 3 & 4 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));





            #region LOAD 23 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY

            list.Add(string.Format("LOAD 23 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 3 & 4 ON ALL WING TIPS ONLY, Wind Load in FX, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 4 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));



             
            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));

            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));




            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 23


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 24"));
            //list.Add(string.Format("Cable Loads in FX, FY, FZ on Ends 1 & 3 on all Wing Tips only"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format(""));


            #region LOAD 24 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY

            list.Add(string.Format("LOAD 24 CABLE LOADS IN FX, FY, FZ ON ENDS 1 & 3 ON ALL WING TIPS ONLY, Wind Load in FX, Seismic Load in FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("*CABLE LOADS IN FX ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_1, tlc.End1.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_1, tlc.End1.FY));
            list.Add(string.Format("*CABLE LOADS IN FZ ON END 1 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_1, tlc.End1.FZ));

            //list.Add(string.Format("{0} FX {1:f3}", END_2, tlc.End2.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_2, tlc.End2.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_2, tlc.End2.FZ));


            list.Add(string.Format("*CABLE LOADS IN FX ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FX -{1:f3}", END_3, tlc.End3.FX));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FY -{1:f3}", END_3, tlc.End3.FY));
            list.Add(string.Format("*CABLE LOADS IN FY ON END 3 ON ALL WING TIPS"));
            list.Add(string.Format("{0} FZ -{1:f3}", END_3, tlc.End3.FZ));


            //list.Add(string.Format("{0} FX {1:f3}", END_4, tlc.End4.FX));
            //list.Add(string.Format("{0} FY -{1:f3}", END_4, tlc.End4.FY));
            //list.Add(string.Format("{0} FZ {1:f3}", END_4, tlc.End4.FZ));




            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));

            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));

            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3 (Wing Bottom)"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }


            #endregion Load 3

            #endregion All Load Combinations

            return list;
        }

        //Chiranjit [2012 02 09]
        private void Data_Convert_and_Update_IS_TO_BS()
        {
            try
            {
                #region Convert Data
                SectionData secData = null;

                for (int i = 0; i < complete_design.Members.Count; i++)
                {
                    secData = complete_design.Members[i].SectionDetails;

                    if (secData.SectionName.StartsWith("IS"))
                    {
                        iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref secData);

                        //Chiranjit [2012 02 09]
                        switch (complete_design.Members[i].Group.GroupName.ToUpper())
                        {
                            case "_STRINGER":
                                {
                                    secData.SectionName = "UKB";
                                    secData.SectionCode = "533X312X150";
                                    secData.BottomPlate.Width = 180.0;
                                    secData.BottomPlate.Thickness = 12.0;
                                    secData.VerticalStiffenerPlate.Width = 490.0;
                                    secData.VerticalStiffenerPlate.Thickness = 12.0;

                                    secData.SidePlate.Width = 0.0;
                                    secData.SidePlate.Thickness = 0.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                }
                                break;
                            case "_U1U2":
                                {
                                    secData.SectionName = "UKA";
                                    secData.SectionCode = " 150X150";
                                    secData.AngleThickness = 12.0;
                                    secData.SidePlate.Width = 220.0;
                                    secData.SidePlate.Thickness = 12.0;


                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                            case "_L3U2":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "300X90X41";
                                    secData.SidePlate.Width = 220.0;
                                    secData.SidePlate.Thickness = 12.0;


                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;

                                }
                                break;
                            case "_L4U3":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "300X90X41";

                                    secData.SidePlate.Width = 0.0;
                                    secData.SidePlate.Thickness = 0.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                            case "_L5U4":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "200X75X23";

                                    secData.SidePlate.Width = 0.0;
                                    secData.SidePlate.Thickness = 0.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 0.0;
                                    secData.BottomPlate.Thickness = 0.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                            case "_L5U5":
                                {
                                    secData.SectionName = "UKPFC";
                                    secData.SectionCode = "200X75X23";

                                    secData.SidePlate.Width = 180.0;
                                    secData.SidePlate.Thickness = 32.0;

                                    secData.TopPlate.Width = 0.0;
                                    secData.TopPlate.Thickness = 0.0;

                                    secData.BottomPlate.Width = 200.0;
                                    secData.BottomPlate.Thickness = 10.0;

                                    secData.VerticalStiffenerPlate.Width = 0.0;
                                    secData.VerticalStiffenerPlate.Thickness = 0.0;
                                }
                                break;
                        }
                    }
                    AddMemberRow(complete_design.Members[i]);
                }

                #endregion Convert Data
            }
            catch (Exception ex) { }


            try
            {
                string str = MyList.RemoveAllSpaces(txt_cd_mem_no.Text).ToUpper();
                CMember m = null;
                str = str.Replace(',', ' ');
                str = MyList.RemoveAllSpaces(str);
                MyList mList = new MyList(str, ' ');

                m = GetMemberData();
                int indx = complete_design.Members.IndexOf(m);
                if (indx != -1)
                {
                    //dgv_mem_details.Rows.RemoveAt(dgv_mem_details.CurrentRow.Index);
                    //DeleteMember(m.Group.GroupName);
                    complete_design.Members[indx] = m;
                }
                else
                    complete_design.Members.Add(m);

                AddMemberRow(m);
                //Show_Total_Weight();
                //MessageBox.Show(m.Group.GroupName + " updated.", "ASTRA");
                timer1.Stop();
                //MessageBox.Show(this, "All Member Sections are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Show_Total_Weight();
            }
        }
        private void Data_Convert_and_Update_IS_TO_AISC()
        {
            try
            {
                #region Convert Data
                SectionData secData = null;

                for (int i = 0; i < complete_design.Members.Count; i++)
                {
                    secData = complete_design.Members[i].SectionDetails;

                    if (secData.SectionName.StartsWith("IS"))
                    {
                        iApp.Tables.Steel_Convert.Convert_IS_to_AISC(ref secData);
                    }
                    AddMemberRow(complete_design.Members[i]);
                }

                #endregion Convert Data
            }
            catch (Exception ex) { }


            try
            {
                string str = MyList.RemoveAllSpaces(txt_cd_mem_no.Text).ToUpper();
                CMember m = null;
                str = str.Replace(',', ' ');
                str = MyList.RemoveAllSpaces(str);
                MyList mList = new MyList(str, ' ');

                m = GetMemberData();
                int indx = complete_design.Members.IndexOf(m);
                if (indx != -1)
                {
                    //dgv_mem_details.Rows.RemoveAt(dgv_mem_details.CurrentRow.Index);
                    //DeleteMember(m.Group.GroupName);
                    //complete_design.Members[indx] = m;
                }
                else
                    complete_design.Members.Add(m);

                AddMemberRow(m);
                //Show_Total_Weight();
                //MessageBox.Show(m.Group.GroupName + " updated.", "ASTRA");
                timer1.Stop();
                //MessageBox.Show(this, "All Member Sections are updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Show_Total_Weight();
            }
        }

        public CMember GetMemberData()
        {
            MyList mList = new MyList(txt_cd_mem_no.Text, ' ');

            CMember member = new CMember(iApp);
            member.Group.GroupName = cmb_mem_group.Text;
            member.Group.MemberNosText = txt_cd_mem_no.Text;
            member.MemberType = eMemberType.TowerMember;
            member.SectionDetails.DefineSection = (eDefineSection)cmb_sections_define.SelectedIndex;
            member.SectionDetails.SectionName = cmb_section_name.Text;
            member.SectionDetails.SectionCode = cmb_code1.Text;
            member.SectionDetails.NoOfElements = MyList.StringToDouble(txt_no_ele.Text, 1);

            member.SectionDetails.AngleThickness = MyList.StringToDouble(cmb_sec_thk.Text, 0.0);


            member.Length = Truss_Analysis.Analysis.Members.Get_Member_Length(txt_cd_mem_no.Text);

            member.Force = Truss_Analysis.GetForce(ref member);
            //double ixx = member.IXX;


            //member.Weight = MyList.StringToDouble(txt_cd_wgt.Text, 0.0);
            member.WeightPerMetre = GetWeightPerMetre(member);
            member.SectionDetails.TopPlate.Width = MyList.StringToDouble(txt_tp_width.Text, 0.0);
            member.SectionDetails.TopPlate.Thickness = MyList.StringToDouble(txt_tp_thk.Text, 0.0);
            member.SectionDetails.TopPlate.Length = member.Length;
            member.SectionDetails.TopPlate.TotalPlates = 1;

            member.SectionDetails.BottomPlate.Width = MyList.StringToDouble(txt_bp_wd.Text, 0.0);
            member.SectionDetails.BottomPlate.Thickness = MyList.StringToDouble(txt_bp_thk.Text, 0.0);
            member.SectionDetails.BottomPlate.Length = member.Length;
            member.SectionDetails.BottomPlate.TotalPlates = 1;

            member.SectionDetails.SidePlate.Width = MyList.StringToDouble(txt_sp_wd.Text, 0.0);
            member.SectionDetails.SidePlate.Thickness = MyList.StringToDouble(txt_sp_thk.Text, 0.0);
            member.SectionDetails.SidePlate.Length = member.Length;

            if (member.SectionDetails.DefineSection == eDefineSection.Section8
               || member.SectionDetails.DefineSection == eDefineSection.Section11
               || member.SectionDetails.DefineSection == eDefineSection.Section12
               || member.SectionDetails.DefineSection == eDefineSection.Section13
               || member.SectionDetails.DefineSection == eDefineSection.Section14
                )
            {
                member.SectionDetails.SidePlate.TotalPlates = 1;
            }
            else
                member.SectionDetails.SidePlate.TotalPlates = 2;

            member.SectionDetails.VerticalStiffenerPlate.Width = MyList.StringToDouble(txt_vsp_wd.Text, 0.0);
            member.SectionDetails.VerticalStiffenerPlate.Thickness = MyList.StringToDouble(txt_vsp_thk.Text, 0.0);
            member.SectionDetails.VerticalStiffenerPlate.Length = member.Length;
            if (member.SectionDetails.DefineSection == eDefineSection.Section8)
                member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 1;
            else
                member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;


            member.SectionDetails.LateralSpacing = MyList.StringToDouble(txt_sec_lat_spac.Text, 0.0);
            member.SectionDetails.BoltDia = MyList.StringToDouble(txt_sec_bolt_dia.Text, 0.0);
            member.SectionDetails.NoOfBolts = MyList.StringToInt(txt_sec_nb.Text, 0);

            return member;
        }

        void Show_Total_Weight()
        {
            Complete_Design.AddWeightPercent = MyList.StringToDouble(txt_cd_force_percent.Text, 0.0);
            txt_steel_structure_weight.Text = (Complete_Design.TotalSteelWeight / 10.0).ToString("0.00");
            txt_total_structure_weight.Text = ((Complete_Design.Members.Weight + Complete_Design.GussetAndLacingWeight) / 10.0).ToString("0.00");
        }

        public eTowerMemberType Get_Tower_Member2(string mem_grp)
        {

            eTowerMemberType tmt = eTowerMemberType.AllMember;



            if (mem_grp.Contains("_CRW_MAINPOST")) tmt = eTowerMemberType.CROWN_MAINPOST;

            else if (mem_grp.Contains("_CRW_BRACINGS")) { tmt = eTowerMemberType.CROWN_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_MAINPOST")) { tmt = eTowerMemberType.LEVEL1_MAINPOST; }

            else if (mem_grp.Contains("_LEV1_LOWER_CROSS_BRACINGS")) { tmt = eTowerMemberType.LEVEL1_LOWER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_UPPER_CROSS_BRACINGS")) { tmt = eTowerMemberType.LEVEL1_UPPER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_INTERNAL_LOWERBRACINGS")) { tmt = eTowerMemberType.LEVEL1_INTERNAL_LOWER_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_INTERNALUPPERBRACINGS")) { tmt = eTowerMemberType.LEVEL1_INTERNAL_UPPER_BRACINGS; }
                                       
            else if (mem_grp.Contains("_LEV1_UPPER")) { tmt = eTowerMemberType.LEVEL1_BOX; }
            else if (mem_grp.Contains("_LEV1_BOX")) { tmt = eTowerMemberType.LEVEL1_BOX; }

            else if (mem_grp.Contains("_LEV1_WING_BRACINGS")) { tmt = eTowerMemberType.LEVEL1_WING_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_LEFT_WING_TOP")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_TOP; }

            else if (mem_grp.Contains("_LEV1_LEFT_WING_BOTTOM")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV1_LEFT_WING_SIDEBRACINGS")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_SIDE_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_LEFT_WING_TOPBRACINGS")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_TOP_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_LEFTWINGBOTTOMBRACINGS")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_BOTTOM_BRACINGS; }


            else if (mem_grp.Contains("_LEV1_RIGHTWINGTOP")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_TOP; }

            else if (mem_grp.Contains("_LEV1_RIGHTWINGBOTTOM")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV1_RIGHTWINGSIDEBRACINGS")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_RIGHTWINGTOPBRACINGS")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_TOP_BRACINGS; }

            else if (mem_grp.Contains("_LEV1RIGHTWINGBOTTOMBRACINGS")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_BOTTOM_BRACINGS; }


            else if (mem_grp.Contains("_LEV2_MAINPOST"))
            {
                tmt = eTowerMemberType.LEVEL2_MAINPOST;
            }
            else if (mem_grp.Contains("_LEV2_LOWERCROSSBRACINGS")) { tmt = eTowerMemberType.LEVEL2_LOWER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_UPPERCROSSBRACINGS")) { tmt = eTowerMemberType.LEVEL2_UPPER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LOWERINTERNALBRACINGS")) { tmt = eTowerMemberType.LEVEL2_INTERNAL_LOWER_BRACINGS; }

            else if (mem_grp.Contains("_LEV2_UPPERINTERNALBRACINGS")) { tmt = eTowerMemberType.LEVEL2_INTERNAL_UPPER_BRACINGS; }

            else if (mem_grp.Contains("_LEV2_LEFTWINGSIDEBRACINGS")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LEFTWINGTOPBRACINGS")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LEFTWINGBOTTOMBRACINGS")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_BOTTOM_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_RIGHTWINGSIDEBRACINGS")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_SIDE_BRACINGS; }


            else if (mem_grp.Contains("_LEV2_WINGTOP")) { tmt = eTowerMemberType.LEVEL2_WING_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LEFT_WING_TOP")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_TOP; }
            else if (mem_grp.Contains("_LEV2_LEFT_WING_BOTTOM")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_BOTTOM; }
                
            else if (mem_grp.Contains("_LEV2_BOX")) { tmt = eTowerMemberType.LEVEL2_BOX; }


            else if (mem_grp.Contains("_LEV2_LEFTWINGBOTTOM")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_BOTTOM; }
            else if (mem_grp.Contains("_LEV2_RIGHTWINGTOP")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP; }
            else if (mem_grp.Contains("_LEV2_RIGHTWINGBOTTOM")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV2_RIGHTWINGTOPBRACINGS")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV2RIGHTWINGBOTTOMBRACINGS")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_BOTTOM_BRACINGS; }


            else if (mem_grp.Contains("_LEV3_MAINPOST")) { tmt = eTowerMemberType.LEVEL3_MAINPOST; }
            else if (mem_grp.Contains("_LEV3_UPPER_BRACINGS")) { tmt = eTowerMemberType.LEVEL3_UPPER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_BOX")) { tmt = eTowerMemberType.LEVEL3_BOX; }
            else if (mem_grp.Contains("_LEV3RIGHTWINGBOTTOMBRACINGS")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_BOTTOM_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_RIGHTWINGTOPBRACINGS")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_RIGHTWINGSIDEBRACINGS")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_RIGHTWINGTOP")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP; }

            else if (mem_grp.Contains("_LEV3_RIGHTWINGBOTTOM")) { tmt = eTowerMemberType.LEVEL3_RIGHT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV3LEFTWINGSIDEBRACINGS")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_SIDE_BRACINGS; }

            else if (mem_grp.Contains("_LEV3LEFTWINGTOPBRACINGS")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_TOP_BRACINGS; }

            else if (mem_grp.Contains("_LEV3LEFTWINGBOTTOMBRACINGS")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_BOTTOM_BRACINGS; }

            else if (mem_grp.Contains("_LEV3LEFTWINGTOP")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_TOP; }

            else if (mem_grp.Contains("_LEV3LEFTWINGBOTTOM")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_BOTTOM; }

            return tmt;
        }

        public eTowerMemberType Get_Tower_Member(string mem_grp)
        {
            eTowerMemberType tmt = eTowerMemberType.AllMember;

            if (mem_grp.Contains("_CRW_MAINPOST")) tmt = eTowerMemberType.CROWN_MAINPOST;

            else if (mem_grp.Contains("_CRW_BRACINGS")) { tmt = eTowerMemberType.CROWN_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_MAINPOST")) { tmt = eTowerMemberType.LEVEL1_MAINPOST; }

            else if (mem_grp.Contains("_LEV1_LCB")) { tmt = eTowerMemberType.LEVEL1_LOWER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_UCB")) { tmt = eTowerMemberType.LEVEL1_UPPER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_ILB")) { tmt = eTowerMemberType.LEVEL1_INTERNAL_LOWER_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_IUB")) { tmt = eTowerMemberType.LEVEL1_INTERNAL_UPPER_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_BOX")) { tmt = eTowerMemberType.LEVEL1_BOX; }

            else if (mem_grp.Contains("_LEV1_WB")) { tmt = eTowerMemberType.LEVEL1_WING_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_LTWTOP")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_TOP; }

            else if (mem_grp.Contains("_LEV1_LTWBOT")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV1_LTWSB")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_SIDE_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_LTWTB")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_TOP_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_LTWBOTB")) { tmt = eTowerMemberType.LEVEL1_LEFT_WING_BOTTOM_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_RTWTOP")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_TOP; }

            else if (mem_grp.Contains("_LEV1_RTWBOT")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV1_RTWSB")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV1_RTWTB")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_TOP_BRACINGS; }

            else if (mem_grp.Contains("_LEV1_RTWBOTB")) { tmt = eTowerMemberType.LEVEL1_RIGHT_WING_BOTTOM_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_MAINPOST")) tmt = eTowerMemberType.LEVEL2_MAINPOST;
            else if (mem_grp.Contains("_LEV2_LCB")) { tmt = eTowerMemberType.LEVEL2_LOWER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_UCB")) { tmt = eTowerMemberType.LEVEL2_UPPER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_ILB")) { tmt = eTowerMemberType.LEVEL2_INTERNAL_LOWER_BRACINGS; }

            else if (mem_grp.Contains("_LEV2_IUB")) { tmt = eTowerMemberType.LEVEL2_INTERNAL_UPPER_BRACINGS; }

            else if (mem_grp.Contains("_LEV2_LTWSB")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LTWTB")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LTWBOTB")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_BOTTOM_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_RTWSB")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_SIDE_BRACINGS; }

            else if (mem_grp.Contains("_LEV2_LTWTOP")) { tmt = eTowerMemberType.LEVEL2_WING_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_LEFT_WING_TOP")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_TOP; }
            else if (mem_grp.Contains("_LEV2_LEFT_WING_BOTTOM")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV2_BOX")) { tmt = eTowerMemberType.LEVEL2_BOX; }

            else if (mem_grp.Contains("_LEV2_LTWBOT")) { tmt = eTowerMemberType.LEVEL2_LEFT_WING_BOTTOM; }
            else if (mem_grp.Contains("_LEV2_RTWTOP")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP; }
            else if (mem_grp.Contains("_LEV2_RTWBOT")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_BOTTOM; }

            else if (mem_grp.Contains("_LEV2_RTWTB")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV2_RTWBOTB")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_BOTTOM_BRACINGS; }

            else if (mem_grp.Contains("_LEV3_MAINPOST")) { tmt = eTowerMemberType.LEVEL3_MAINPOST; }
            else if (mem_grp.Contains("_LEV3_UB")) { tmt = eTowerMemberType.LEVEL3_UPPER_CROSS_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_BOX")) { tmt = eTowerMemberType.LEVEL3_BOX; }
            else if (mem_grp.Contains("_LEV3_RTWBOTB")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_BOTTOM_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_RWTOPB")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_RTWSB")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_RTWTOP")) { tmt = eTowerMemberType.LEVEL2_RIGHT_WING_TOP; }

            else if (mem_grp.Contains("_LEV3_RTWBOT")) { tmt = eTowerMemberType.LEVEL3_RIGHT_WING_BOTTOM; }
            else if (mem_grp.Contains("_LEV3_LTWSB")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_SIDE_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_LTWTB")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_TOP_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_LTWBOTB")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_BOTTOM_BRACINGS; }
            else if (mem_grp.Contains("_LEV3_LTWTOP")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_TOP; }
            else if (mem_grp.Contains("_LEV3_LTWB")) { tmt = eTowerMemberType.LEVEL3_LEFT_WING_BOTTOM; }

            return tmt;
        }

        public string Get_Tower_Member_Short(string mem_grp)
        {

            string tmt = "";

            //_CRW_MAINPOST
            //_CRW_BRACINGS
            //_LEV1_MAINPOST
            //_LEV1_LCB
            //_LEV1_UCB
            //_LEV1_ILB
            //_LEV1_IUB
            //_LEV1_BOX
            //_LEV1_WB
            //_LEV1_LTWTOP
            //_LEV1_LTWBOT
            //_LEV1_LTWSB
            //_LEV1_LTWTB
            //_LEV1_LTWBOTB
            //_LEV1_RTWTOP
            //_LEV1_RTWBOT
            //_LEV1_RTWSB
            //_LEV1_RTWTB
            //_LEV1_RTWBOTB
            //_LEV2_MAINPOST
            //_LEV2_LCB
            //_LEV2_UCB
            //_LEV2_ILB
            //_LEV2_IUB
            //_LEV2_LTWSB
            //_LEV2_LTWTB
            //_LEV2_LTWBOTB
            //_LEV2_RTWSB
            //_LEV2_LTWTOP
            //_LEV2_BOX
            //_LEV2_LTWBOT
            //_LEV2_RTWTOP
            //_LEV2_RTWBOT
            //_LEV2_RTWTB
            //_LEV2_RTWBOTB
            //_LEV3_MAINPOST
            //_LEV3_UB
            //_LEV3_BOX
            //_LEV3_RTWBOTB
            //_LEV3_RWTOPB
            //_LEV3_RTWSB
            //_LEV3_RTWTOP
            //_LEV3_RTWBOT
            //_LEV3_LTWSB
            //_LEV3_LTWTB
            //_LEV3_LTWBOTB
            //_LEV3_LTWTOP
            //_LEV3_LTWB

            if (mem_grp.Contains("_CRW_MAINPOST")) tmt = "_CRW_MAINPOST";

            else if (mem_grp.Contains("_CRW_BRACINGS")) tmt = "_CRW_BRACINGS";

            else if (mem_grp.Contains("_LEV1_MAINPOST")) tmt = "_LEV1_MAINPOST";

            else if (mem_grp.Contains("_LEV1_LOWER_CROSS_BRACINGS")) tmt = "_LEV1_LCB";
            else if (mem_grp.Contains("_LEV1_UPPER_CROSS_BRACINGS")) tmt = "_LEV1_UCB";
            else if (mem_grp.Contains("_LEV1_INTERNAL_LOWERBRACINGS")) tmt = "_LEV1_ILB";
            else if (mem_grp.Contains("_LEV1_INTERNALUPPERBRACINGS")) tmt = "_LEV1_IUB";

            else if (mem_grp.Contains("_LEV1_UPPER")) tmt = "_LEV1_BOX";
            else if (mem_grp.Contains("_LEV1_BOX")) tmt = "_LEV1_BOX";

            else if (mem_grp.Contains("_LEV1_WING_BRACINGS")) tmt = "_LEV1_WB";

            else if (mem_grp.Contains("_LEV1_LEFT_WING_TOP")) tmt = "_LEV1_LTWTOP";

            else if (mem_grp.Contains("_LEV1_LEFT_WING_BOTTOM")) tmt = "_LEV1_LTWBOT";

            else if (mem_grp.Contains("_LEV1_LEFT_WING_SIDEBRACINGS")) tmt = "_LEV1_LTWSB";

            else if (mem_grp.Contains("_LEV1_LEFT_WING_TOPBRACINGS")) tmt = "_LEV1_LTWTB";

            else if (mem_grp.Contains("_LEV1_LEFTWINGBOTTOMBRACINGS")) tmt = "_LEV1_LTWBOTB";


            else if (mem_grp.Contains("_LEV1_RIGHTWINGTOP")) tmt = "_LEV1_RTWTOP";

            else if (mem_grp.Contains("_LEV1_RIGHTWINGBOTTOM")) tmt = "_LEV1_RTWBOT";

            else if (mem_grp.Contains("_LEV1_RIGHTWINGSIDEBRACINGS")) tmt = "_LEV1_RTWSB";
            else if (mem_grp.Contains("_LEV1_RIGHTWINGTOPBRACINGS")) tmt = "_LEV1_RTWTB";

            else if (mem_grp.Contains("_LEV1RIGHTWINGBOTTOMBRACINGS")) tmt = "_LEV1_RTWBOTB";


            else if (mem_grp.Contains("_LEV2_MAINPOST")) tmt = "_LEV2_MAINPOST";
            else if (mem_grp.Contains("_LEV2_LOWERCROSSBRACINGS")) tmt = "_LEV2_LCB";
            else if (mem_grp.Contains("_LEV2_UPPERCROSSBRACINGS")) tmt = "_LEV2_UCB";
            else if (mem_grp.Contains("_LEV2_LOWERINTERNALBRACINGS")) tmt = "_LEV2_ILB";

            else if (mem_grp.Contains("_LEV2_UPPERINTERNALBRACINGS")) tmt = "_LEV2_IUB";

            else if (mem_grp.Contains("_LEV2_LEFTWINGSIDEBRACINGS")) tmt = "_LEV2_LTWSB";
            else if (mem_grp.Contains("_LEV2_LEFTWINGTOPBRACINGS")) tmt = "_LEV2_LTWTB";
            else if (mem_grp.Contains("_LEV2_LEFTWINGBOTTOMBRACINGS")) tmt = "_LEV2_LTWBOTB";
            else if (mem_grp.Contains("_LEV2_RIGHTWINGSIDEBRACINGS")) tmt = "_LEV2_RTWSB";


            else if (mem_grp.Contains("_LEV2_WINGTOP")) tmt = "_LEV2_LTWTOP";
            else if (mem_grp.Contains("_LEV2_LEFT_WING_TOP")) tmt = "_LEV2_LTWTOP";
            else if (mem_grp.Contains("_LEV2_LEFT_WING_BOTTOM")) tmt = "_LEV2_LTWBOT";

            else if (mem_grp.Contains("_LEV2_BOX")) tmt = "_LEV2_BOX";


            else if (mem_grp.Contains("_LEV2_LEFTWINGBOTTOM")) tmt = "_LEV2_LTWBOT";
            else if (mem_grp.Contains("_LEV2_RIGHTWINGTOP")) tmt = "_LEV2_RTWTOP";
            else if (mem_grp.Contains("_LEV2_RIGHTWINGBOTTOM")) tmt = "_LEV2_RTWBOT";

            else if (mem_grp.Contains("_LEV2_RIGHTWINGTOPBRACINGS")) tmt = "_LEV2_RTWTB";
            else if (mem_grp.Contains("_LEV2RIGHTWINGBOTTOMBRACINGS")) tmt = "_LEV2_RTWBOTB";


            else if (mem_grp.Contains("_LEV3_MAINPOST")) tmt = "_LEV3_MAINPOST";
            else if (mem_grp.Contains("_LEV3_UPPER_BRACINGS")) tmt = "_LEV3_UB";
            else if (mem_grp.Contains("_LEV3_BOX")) tmt = "_LEV3_BOX";
            else if (mem_grp.Contains("_LEV3RIGHTWINGBOTTOMBRACINGS")) tmt = "_LEV3_RTWBOTB";
            else if (mem_grp.Contains("_LEV3_RIGHTWINGTOPBRACINGS")) tmt = "_LEV3_RWTOPB";
            else if (mem_grp.Contains("_LEV3_RIGHTWINGSIDEBRACINGS")) tmt = "_LEV3_RTWSB";
            else if (mem_grp.Contains("_LEV3_RIGHTWINGTOP")) tmt = "_LEV3_RTWTOP";

            else if (mem_grp.Contains("_LEV3_RIGHTWINGBOTTOM")) tmt = "_LEV3_RTWBOT";

            else if (mem_grp.Contains("_LEV3LEFTWINGSIDEBRACINGS")) tmt = "_LEV3_LTWSB";

            else if (mem_grp.Contains("_LEV3LEFTWINGTOPBRACINGS")) tmt = "_LEV3_LTWTB";

            else if (mem_grp.Contains("_LEV3LEFTWINGBOTTOMBRACINGS")) tmt = "_LEV3_LTWBOTB";

            else if (mem_grp.Contains("_LEV3LEFTWINGTOP")) tmt = "_LEV3_LTWTOP";

            else if (mem_grp.Contains("_LEV3LEFTWINGBOTTOM")) tmt = "_LEV3_LTWBOT";

            return tmt;
        }

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

                MessageBox.Show("Next, the Program will be reading the analysis results, it may take some times, please wait till the analysis results are displayed.", "ASTRA", MessageBoxButtons.OK);
                //return;
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
                    txt_node_displace.Lines = list_node.ToArray();
                }
                catch (Exception e2x) { }
                iApp.Progress_Works.Clear();

            }


            if (cmb_mem_group.Items.Count > 0)
                cmb_mem_group.SelectedIndex = 0;
            if (dgv_mem_details.RowCount == 0)
            {
                FillMemberGroup();
            }

            string kFile = Path.Combine(system_path, "MEMBER_LOAD_DATA.txt");
            if (File.Exists(kFile))
            {
                //angle thickness not comming
                //SetCompleteDesign(kFile);

                //ReadResult();
            }
            dgv_mem_details.Rows.Clear();


            for (int i = 0; i < Complete_Design.Members.Count; i++)
            {
                try
                {
                    CMember member = Complete_Design.Members[i];
                    if (Truss_Analysis != null)
                    {
                        member.Group = Truss_Analysis.Analysis.MemberGroups.GetMemberGroup(member.Group.GroupName);
                    }
                    member.Force = Truss_Analysis.GetForce(ref member, true);
                    AddMemberRow(member);
                }
                catch (Exception eeex) { }
            }
            FillMemberResult();
            FillAnalysisResults();
            Set_Force_Input_Color();
        }


        private void Set_Force_Input_Color()
        {
            dgv_ana_results.ReadOnly = !chk_edit_forces.Checked;
            if (dgv_ana_results.ReadOnly) return;
            for (int i = 0; i < dgv_ana_results.RowCount; i++)
            {

                if (dgv_ana_results[1, i].Value.ToString().ToUpper().StartsWith("CROSS") ||
                    dgv_ana_results[1, i].Value.ToString().ToUpper().StartsWith("STRING"))
                {
                    dgv_ana_results[3, i].Style.BackColor = Color.White;
                    dgv_ana_results[4, i].Style.BackColor = Color.White;
                    dgv_ana_results[5, i].Style.BackColor = Color.Yellow;
                    dgv_ana_results[6, i].Style.BackColor = Color.Yellow;
                }
                else
                {
                    dgv_ana_results[3, i].Style.BackColor = Color.Yellow;
                    dgv_ana_results[4, i].Style.BackColor = Color.Yellow;
                    dgv_ana_results[5, i].Style.BackColor = Color.White;
                    dgv_ana_results[6, i].Style.BackColor = Color.White;
                }


                if (!dgv_ana_results.ReadOnly)
                {
                    dgv_ana_results[3, i].Style.ForeColor = Color.Red;
                    dgv_ana_results[4, i].Style.ForeColor = Color.Red;
                    dgv_ana_results[5, i].Style.ForeColor = Color.Red;
                    dgv_ana_results[6, i].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv_ana_results[3, i].Style.ForeColor = Color.Black;
                    dgv_ana_results[4, i].Style.ForeColor = Color.Black;
                    dgv_ana_results[5, i].Style.ForeColor = Color.Black;
                    dgv_ana_results[6, i].Style.ForeColor = Color.Black;
                }
            }
        }

        public void FillMemberResult()
        {
            try
            {
                dgv_member_Result.Rows.Clear();

                double tens = 0.0, comp = 0.0;


                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    if (true)
                    {
                        //if ((Complete_Design.Members[i].MemberType == eMemberType.CrossGirder) ||
                        //    (Complete_Design.Members[i].MemberType == eMemberType.StringerBeam))
                        //{
                        //    comp = 0.0;
                        //    tens = 0.0;
                        //}
                        //else
                        //{
                            comp = Complete_Design.Members[i].MaxCompForce.Force;
                            tens = Complete_Design.Members[i].MaxTensionForce.Force;
                        //}

                        //dgv_member_Result.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                        //    Complete_Design.Members[i].MemberType.ToString(),
                        //    comp.ToString("f3"),
                        //    Complete_Design.Members[i].Capacity_CompForce.ToString("f3"),
                        //    tens.ToString("f3"),
                        //    Complete_Design.Members[i].Capacity_TensionForce.ToString("f3"),
                        //    Complete_Design.Members[i].MaxMoment.Force.ToString("f3"),
                        //    Complete_Design.Members[i].Required_SectionModulus.ToString("E3"),
                        //    Complete_Design.Members[i].Capacity_SectionModulus.ToString("E3"),
                        //    Complete_Design.Members[i].MaxShearForce.Force.ToString("f3"),
                        //    Complete_Design.Members[i].Required_ShearStress.ToString("f3"),
                        //    Complete_Design.Members[i].Capacity_ShearStress.ToString("f3"),
                        //    Complete_Design.Members[i].Result);

                        string kStr = "";


                        dgv_member_Result.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                           Get_Tower_Member( Complete_Design.Members[i].Group.GroupName),

                            comp.ToString("f3"),
                            Complete_Design.Members[i].Capacity_CompForce.ToString("f3"),

                           //Math.Abs(Complete_Design.Members[i].MaxCompForce.Stress/1000).ToString("f3"),
                            // Complete_Design.Members[i].Capacity_Compressive_Stress.ToString("f3"),

                            Complete_Design.Members[i].Result_Compressive,

                            tens.ToString("f3"),
                            Complete_Design.Members[i].Capacity_TensionForce.ToString("f3"),


                            //(Complete_Design.Members[i].MaxTensionForce.Stress/1000).ToString("f3"),
                            //Complete_Design.Members[i].Capacity_Tensile_Stress.ToString("f3"),


                            Complete_Design.Members[i].Result_Tensile,


                            Complete_Design.Members[i].MaxBendingMoment.Force.ToString("f3"),
                            Complete_Design.Members[i].Required_SectionModulus.ToString("E3"),
                            Complete_Design.Members[i].Capacity_SectionModulus.ToString("E3"),
                            Complete_Design.Members[i].MaxShearForce.Force.ToString("f3"),
                            Complete_Design.Members[i].Capacity_ShearStress.ToString("f3"),
                            Complete_Design.Members[i].Required_ShearStress.ToString("f3"),
                            Complete_Design.Members[i].Result);
                    }
                }
                FillResultGridWithColor();
            }
            catch (Exception ex) { }


        }

        public void FillAnalysisResults()
        {
            try
            {
                dgv_ana_results.Rows.Clear();

                double tens = 0.0, comp = 0.0;


                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    //if (Complete_Design.Members[i].MemberType || design_member == eMemberType.AllMember)
                    //{
                    if ((Complete_Design.Members[i].MemberType == eMemberType.CrossGirder) ||
                        (Complete_Design.Members[i].MemberType == eMemberType.StringerBeam))
                    {
                        comp = 0.0;
                        tens = 0.0;
                    }
                    else
                    {
                        comp = Complete_Design.Members[i].MaxCompForce.Force;
                        tens = Complete_Design.Members[i].MaxTensionForce.Force;
                    }

                    dgv_ana_results.Rows.Add(Complete_Design.Members[i].Group.GroupName,
                      Get_Tower_Member(Complete_Design.Members[i].Group.GroupName),

                        Complete_Design.Members[i].Group.MemberNosText.ToString(),
                        Complete_Design.Members[i].MaxAxialForce.Force.ToString("f3"),
                        Complete_Design.Members[i].MaxBendingMoment.Force.ToString("f3"),
                        Complete_Design.Members[i].MaxShearForce.Force.ToString("f3")
                        );
                    //}
                }
                for (int i = 0; i < dgv_ana_results.RowCount; i++)
                {
                    for (int j = 0; j < dgv_ana_results.ColumnCount; j++)
                    {
                        if (dgv_ana_results[j, i].Value.ToString() == "0.000" ||
                            dgv_ana_results[j, i].Value.ToString() == "0.000E+000")
                            dgv_ana_results[j, i].Value = "";
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void FillResultGridWithColor()
        {
            try
            {

                for (int i = 0; i < dgv_member_Result.RowCount; i++)
                {
                    for (int j = 0; j < dgv_member_Result.ColumnCount; j++)
                    {
                        if (dgv_member_Result[j, i].Value.ToString() == "0.000" ||
                            dgv_member_Result[j, i].Value.ToString() == "0.000E+000")
                            dgv_member_Result[j, i].Value = "";
                    }

                    if (dgv_member_Result[dgv_member_Result.ColumnCount - 1, i].Value.ToString().ToUpper() == "NOT OK")
                    {
                        SetGroupResultColor(dgv_member_Result[0, i].Value.ToString(), Color.Red);
                        dgv_member_Result.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    if (dgv_member_Result[dgv_member_Result.ColumnCount - 1, i].Value.ToString().ToUpper().Trim().TrimEnd().TrimStart() == "OK")
                    {
                        SetGroupResultColor(dgv_member_Result[0, i].Value.ToString(), Color.Green);
                        dgv_member_Result.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception xe) { }
        }
        void SetGroupResultColor(string mem_grp, Color clr)
        {
            for (int i = 0; i < dgv_mem_details.RowCount; i++)
            {
                if (dgv_mem_details[0, i].Value.ToString().ToUpper() == mem_grp.ToUpper())
                {
                    dgv_mem_details.Rows[i].DefaultCellStyle.ForeColor = clr;
                    return;
                }
                //if (dgv_mem_details[12, i].Value.ToString().ToUpper() == "OK")
                //{
                //    dgv_mem_details.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                //}
            }
        }

        string rep_file_name
        {
            get
            {
                return Path.Combine(user_path, "DESIGN_REP.TXT");
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 07 13]
            //Write_All_Data();
            //Chiranjit [2012 07 06]
            if (!File.Exists(INPUT_FILE))
            {
                MessageBox.Show(this, "The Analysis Input data File is not created. \n\n" +
                                    "In Tab 'Structure Geometry' the button 'Create Analysis Input data File' " +
                                    "is to be used for creating the Analysis Input data\",",
                                    "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //tabControl1.SelectedTab = tab_GD;
                return;
            }

            string mem_grp = "";



            try
            {
                //mem_grp = dgv_member_Result[0, dgv_member_Result.CurrentRow.Index].Value.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, "Analysis Report was not found.", "ASTRA");
                //return;
            }
            if (dgv_mem_details.RowCount == 0)
            {
                MessageBox.Show(this, "No Member found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(this, "This Process might take few minuites.\n Do you want to continue ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                iApp.Progress_Works.Clear();
                iApp.Progress_Works.Add("Member Analysis and Design");

                //Write_User_Input();
                InitializeData();
                //ReadDeadLoadInputs();

                //Chiranjit [2014 03 24]
                if (chk_edit_forces.Checked)
                    Update_Forces();
                Calculate_Program();
                FillMemberResult();
                Set_Force_Input_Color();
                if (File.Exists(rep_file_name))
                {
                    if (MessageBox.Show(this, "Report file written in file " + rep_file_name + "\n\n Do you want to open the report file?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        iApp.View_Result(rep_file_name);
                }
                iApp.Progress_Works.Clear();
            }
            string kStr = "";
            for (int i = 0; i < dgv_member_Result.RowCount; i++)
            {
                kStr = dgv_member_Result[0, i].Value.ToString();
                if (kStr == mem_grp)
                {
                    dgv_member_Result.Rows[i].Selected = true;
                }
            }
        }



        double DL, LL, IL, h, l, fy, fc, ft, d;
        double sigma_b, sigma_c;
        public void InitializeData()
        {
            //DL = 15;
            //LL = 175;
            //IL = 0.15;
            //h = 4.0;
            //l = 24;
            //fy = 250;
            //fc = 110;
            #region USER DATA INPUT
            try
            {
                //L = MyList.StringToDouble(txt_L.Text, 0.0);
                //top_chord_mf, top_chord_c, top_chord_RI, top_chord_phi_1, top_chord_dr;

                //DL = MyList.StringToDouble(txt_DL.Text, 0.0);
                //LL = MyList.StringToDouble(txt_LL.Text, 0.0);
                //IL = MyList.StringToDouble(txt_gd_IL.Text, 0.0);


                //h = MyList.StringToDouble(txt_H.Text, 0.0);
                //l = MyList.StringToDouble(txt_L.Text, 0.0);


                fy = MyList.StringToDouble(txt_gd_fy.Text, 0.0);
                fc = MyList.StringToDouble(txt_fc.Text, 0.0);
                ft = MyList.StringToDouble(txt_gd_ft.Text, 0.0);
                sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);



            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
            //if (cmb_design_member.SelectedIndex == 0)
            //{
            //    design_member = eMemberType.AllMember;
            //}
            //else if (cmb_design_member.SelectedIndex > 0)
            //{
            //    design_member = (eMemberType)(cmb_design_member.SelectedIndex - 1);
            //}
            //else
            //    design_member = eMemberType.NoSelection;
        }
        private void Calculate_Program()
        {
            string file_path = Path.Combine(user_path, "ANALYSIS_REP.TXT");
            //Truss_Analysis = new SteelTrussAnalysis(file_path, pbar);


            if (Truss_Analysis == null)
                Truss_Analysis = new BridgeMemberAnalysis(iApp, file_path);

            //string file_name = Path.Combine(user_path, "MembersDesign.txt");
            //rep_file_name = Truss_Analysis.Analysis.Length.ToString("0") + "m Bridge " + ((cmb_design_member.Text.ToUpper() == "ALL") ? "Complete" : cmb_design_member.Text.Replace("Member", "")) + " Member Design Report.TXT";
            //rep_file_name = Path.Combine(user_path, rep_file_name);



            //rep_file_name = Path.Combine(user_path, "DESIGN_REP.TXT");
            if (File.Exists(rep_file_name))
            {
                if (MessageBox.Show(rep_file_name + " report file is already exist. Do you want to Overwrite this file?", "ASTRA", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }



            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));



            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*                 ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*        DESIGN OF STEEL TRUSS BRIDGE        *");
                sw.WriteLine("\t\t*          COMPLETE MEMBER DESIGN            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                sw.WriteLine();
                sw.WriteLine("-------------------------");
                sw.WriteLine("USER'S GENERAL INPUT DATA ");
                sw.WriteLine("-------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Height of Tower [H] = {0} m", txt_des_Height.Text);
                sw.WriteLine("Width of Tower at Base [B] = {0} m", txt_des_width.Text);
                sw.WriteLine("No of Wing Levels = {0}", cmb_no_levels.Text);
                sw.WriteLine("");


                //sw.WriteLine("Length / Span of Bridge [L] = {0} m", txt_L.Text);
                //sw.WriteLine("Width of Bridge [B] = {0} m", txt_B.Text);
                //sw.WriteLine("Length of each Panel [l] = {0} m", txt_Panel_Length.Text);
                //sw.WriteLine("No of Panels = {0} ", txt_gd_np.Text);
                //sw.WriteLine("Height of Bridge [h] = {0} m", txt_H.Text);

                sw.WriteLine("Steel Yield Stress [Fy] = {0} N/sq.mm", txt_gd_fy.Text);
                //sw.WriteLine("Length of Span [l] = {0} kN/m", txt_L.Text);
                sw.WriteLine("Steel Yield Stress [Fy] = {0} kN/m", txt_gd_fy.Text);
                sw.WriteLine("Permissible stress in Axial comppression [fc] = {0} N/sq.mm", txt_fc.Text);
                sw.WriteLine("Permissible Tensile stress [ft] = {0} N/sq.mm", txt_gd_ft.Text);
                sw.WriteLine("Permissible Bending stress in steel [σ_b] = {0} N/sq.mm", txt_sigma_b.Text);
                sw.WriteLine("Permissible shear stress in steel [σ_c] = {0} N/sq.mm", txt_sigma_c.Text);
                sw.WriteLine();

                //Chiranjit [2013 08 07] Add Weight calculation into Design Report
                Complete_Design.ToStream(sw);

                sw.WriteLine();
                sw.WriteLine();
                int v = 0;
                iApp.Progress_ON("Design Members....");

                int step = 0;

                eMemberType design_member = eMemberType.AllMember;

                for (int i = 0; i < Complete_Design.Members.Count; i++)
                {
                    if (design_member == eMemberType.AllMember)
                    {
                        CMember mem = Complete_Design.Members[i];
                        TotalAnalysis(sw, ref mem, (++step).ToString());
                    }
                    else
                    {
                        if (design_member == Complete_Design.Members[i].MemberType)
                        {
                            CMember mem = Complete_Design.Members[i];
                            TotalAnalysis(sw, ref mem, (++step).ToString());
                            Complete_Design.Members[i] = mem;
                        }
                        else
                            continue;
                    }
                    //if (Complete_Design.Members[i].MemberType == eMemberType.CrossGirder ||
                    //    Complete_Design.Members[i].MemberType == eMemberType.StringerBeam)
                    //{
                        DesignShearConnector(Complete_Design.Members[i], sw);
                    //}
                    iApp.SetProgressValue(i, Complete_Design.Members.Count);
                    if (iApp.Is_Progress_Cancel) break;
                    //v = (int)(((double)(i + 1) / (double)Complete_Design.Members.Count) * 100.0);
                    //pbar.Invoke(spv, pbar, v);
                }




                string file_load_def = Path.Combine(Analysis_Path, "MAX_LOAD_DEFLECTION.TXT");
                //File.WriteAllLines(file_load_def, list_node.ToArray());

                if (File.Exists(file_load_def))
                {
                    List<string> list_node = new List<string>(File.ReadAllLines(file_load_def));

                    #region CHECK FOR LIVE LOAD DEFLECTION

                    sw.WriteLine("");
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : CHECK FOR LIVE LOAD DEFLECTION", ++step);
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                    sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                    sw.WriteLine("");


                    string Node_Displacement_Data_DL = list_node[0];
                    NodeResultData Max_Node_Displacement = NodeResultData.Parse(Node_Displacement_Data_DL);
                    sw.WriteLine("");
                    sw.WriteLine(Max_Node_Displacement.ToString());
                    sw.WriteLine("");

                    sw.WriteLine("");
                    //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                    sw.WriteLine("");
                    double val = Truss_Analysis.Analysis.Length / 800.0;
                    sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", Truss_Analysis.Analysis.Length, val);
                    sw.WriteLine("");
                    if (Max_Node_Displacement.Max_Translation < val)
                        sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. < {1:f5} M.    OK.", Max_Node_Displacement.Max_Translation, val);
                    else
                    {
                        sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M. ", Max_Node_Displacement.Max_Translation, val);
                        //sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                    }

                    #endregion CHECK FOR LIVE LOAD DEFLECTION

                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();

                    #region CHECK FOR DEAD LOAD DEFLECTION

                    string kStr = Truss_Analysis.Analysis.Supports[0].NodeNo + " TO "
                                            + Truss_Analysis.Analysis.Supports[3].NodeNo;


                    List<int> jnts = MyList.Get_Array_Intiger(kStr);


                    List<NodeResultData> dead_load_results = new List<NodeResultData>();

                    sw.WriteLine("");
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : CHECK FOR DEAD LOAD DEFLECTION", ++step);
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-");
                    sw.WriteLine(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION");
                    sw.WriteLine("");


                    for (int i = 1; i < list_node.Count; i++)
                    {
                        dead_load_results.Add(NodeResultData.Parse(list_node[i]));
                        sw.WriteLine(list_node[i]);
                    }


                    sw.WriteLine("");
                    //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    //    sw.WriteLine("Ref. to Cl. 12.4.1  IRC 112 : 2011");
                    sw.WriteLine("");


                    val = Truss_Analysis.Analysis.Length / 800.0;
                    sw.WriteLine("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1:F3} M. ", Truss_Analysis.Analysis.Length, val);
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("-----------------------------------------------------------------------------");
                    sw.WriteLine("MAXIMUM NODE DISPLACEMENTS FOR RIGHT SIDE BOTTOM CHORD");
                    sw.WriteLine("-----------------------------------------------------------------------------");

                    for (int i = 0; i < dead_load_results.Count; i++)
                    {


                        var item = dead_load_results[i];

                        if (i == (dead_load_results.Count / 2))
                        {
                            sw.WriteLine("");
                            sw.WriteLine("");
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("MAXIMUM NODE DISPLACEMENTS FOR LEFT SIDE BOTTOM CHORD");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                        }


                        if (item.Max_Translation < val)
                            sw.WriteLine("DISPLACEMENT AT NODE {0}  = {1:f5} M. < {2:f5} M.    OK.", item.NodeNo, item.Max_Translation, val);
                        else
                        {
                            sw.WriteLine("DISPLACEMENT AT NODE {0}  = {1:f5} M. > {2:f5} M. .", item.NodeNo, item.Max_Translation, val);
                            //sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M. ", Max_Node_Displacement.Max_Translation, val);
                            //sw.WriteLine("MAXIMUM  NODE DISPLACEMENTS = {0:f5} M. > {1:f5} M.    NOT OK.", Max_Node_Displacement.Max_Translation, val);
                        }
                    }

                    sw.WriteLine();


                    #endregion CHECK FOR DEAD LOAD DEFLECTION

                    sw.WriteLine();
                    sw.WriteLine("To be adjusted by providing Longitudinal Camber.");
                    sw.WriteLine();
                    sw.WriteLine();
                }



                iApp.Progress_OFF();

                //WriteTable1(sw);
                //WriteTable2(sw);
                //WriteTable3(sw);
                //WriteTable4(sw);
                //Complete_Design.WriteForcesSummery(sw);
                Complete_Design.WriteForces_Capacity_Summery(sw);
                Complete_Design.WriteGroupSummery(sw);
                string file_ds_frc = "";
                file_ds_frc = Path.Combine(user_path, "DESIGN_SECTION_SUMMARY.TXT");
                Complete_Design.WriteGroupSummery(file_ds_frc);
                file_ds_frc = Path.Combine(user_path, "DESIGN_FORCES_SUMMARY.TXT");



                //Complete_Design.WriteForcesSummery(file_ds_frc);
                Complete_Design.WriteForces_Capacity_Summery(file_ds_frc);




                //Chiranjit [2013 08 16] Kolkata Write Tables at the end of the report
                WriteTable1(sw);
                WriteTable2(sw);
                WriteTable3(sw);
                WriteTable4(sw);
                #region End of Report
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion

            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            //btnReport.Enabled = true;
            //btnDrawing.Enabled = true;


        }
        public void TotalAnalysis(StreamWriter sw, ref CMember mem, string step)
        {
            double Rxx, Cxx, a, Iyy, Ixx, Zxx, Zyy, t, tf, D; // From Table

            double nb, n, S, bolt_dia;

            double tp, Bp, np; // Top Plate
            double tbp, Bbp, nbp; // Bottom Plate
            double ts, Bs, ns; // Side Plate
            double tss, Bss, nss; // Vertical Stiffener Plate
            double Z = 0.0;

            Iyy = Ixx =  Zxx = Zyy = 0.0;


            double Iy, Ix, A, Anet, ry;

            double M, F;
            RolledSteelAnglesRow tabAngle;
            RolledSteelBeamsRow tabBeam;
            RolledSteelChannelsRow tabChannel;


            string kStr = mem.Group.MemberNosText;
            MyList mList = new MyList(kStr, ' ');

            mem.Result = "OK";
            //AnalysisData ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mem.Group.GroupName];
            AnalysisData ana_data = (AnalysisData)Truss_Analysis.MemberAnalysis[mList.GetInt(0)];

            if (ana_data == null)
            {
                ana_data = new AnalysisData();
                if (mem.MemberType != eMemberType.StringerBeam && mem.MemberType != eMemberType.CrossGirder)
                {
                    ana_data.AstraMemberType = eAstraMemberType.TRUSS;
                }
                else
                {
                    ana_data.AstraMemberType = eAstraMemberType.BEAM;
                }
            }
            //M = ana_data.MaxBendingMoment;
            //F = ana_data.MaxShearForce;


            M = mem.MaxBendingMoment.Force;
            F = mem.MaxShearForce.Force;
            A = 0.0;
            Anet = 0.0;
            ry = 0.0;
            double ry_anet = 0.0;
            Iy = 0.0;
            //M = F = 0.0;


            //Chiranjit [2012 02 08]
            bool Check_Tens = false;
            bool Check_Comp = false;

            nb = mem.SectionDetails.NoOfBolts;
            S = mem.SectionDetails.LateralSpacing;
            bolt_dia = mem.SectionDetails.BoltDia;
            n = mem.SectionDetails.NoOfElements;

            Bp = mem.SectionDetails.TopPlate.Width;
            tp = mem.SectionDetails.TopPlate.Thickness;
            np = mem.SectionDetails.TopPlate.TotalPlates;

            Bbp = mem.SectionDetails.BottomPlate.Width;
            tbp = mem.SectionDetails.BottomPlate.Thickness;
            nbp = mem.SectionDetails.BottomPlate.TotalPlates;

            Bs = mem.SectionDetails.SidePlate.Width;
            ts = mem.SectionDetails.SidePlate.Thickness;
            ns = mem.SectionDetails.SidePlate.TotalPlates;

            Bss = mem.SectionDetails.VerticalStiffenerPlate.Width;
            tss = mem.SectionDetails.VerticalStiffenerPlate.Thickness;
            nss = mem.SectionDetails.VerticalStiffenerPlate.TotalPlates;

            mem.DesignReport.Clear();
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            string str = "";


            if (step != "")
                //str = (string.Format("STEP {0} : DESIGN OF {1}, [GROUP : {2}]", step, MemberString.GerMemberString(mem), mem.Group.GroupName));
                str = (string.Format("STEP {0} : DESIGN OF {1}, [GROUP : {2}]", step, Get_Tower_Member(mem.Group.GroupName), mem.Group.GroupName));
            else
                str = (string.Format("DESIGN OF {0}", MemberString.GerMemberString(mem)));
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));
            mem.DesignReport.Add(str);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));

            //if (step != "")
            //    mem.DesignReport.Add(string.Format("STEP {0} : DESIGN OF {1}", step, MemberString.GerMemberString(mem)));
            //else
            //    mem.DesignReport.Add(string.Format("DESIGN OF {0}", MemberString.GerMemberString(mem)));

            mem.DesignReport.Add("");
            //str = string.Format("MEMBER GROUP : {0}", mem.Group.GroupName);
            str = string.Format("MEMBER GROUP : {0}", Get_Tower_Member(mem.Group.GroupName));
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));
            mem.DesignReport.Add(str);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));

            //mem.DesignReport.Add(string.Format("MEMBER GROUP : {0}", mem.Group.GroupName));
            //mem.DesignReport.Add(string.Format("-----------------------"));
            mem.DesignReport.Add(string.Format(""));



            str = string.Format("MEMBER NOS : {0}", mem.Group.MemberNosText);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));
            mem.DesignReport.Add(str);
            mem.DesignReport.Add("".PadLeft(str.Length, '-'));

            //mem.DesignReport.Add(string.Format("MEMBER NOS : {0}", mem.Group.MemberNosText));
            //mem.DesignReport.Add(string.Format("-----------------------"));
            mem.DesignReport.Add(string.Format(""));
            if (mem.MemberType == eMemberType.CrossGirder)
            {
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("NOTE: Forces from Member at Supported Edges are not taken."));
                mem.DesignReport.Add(string.Format(""));
            }

            if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
            {
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("AXIAL FORCE DATA"));
                mem.DesignReport.Add(string.Format("----------------"));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Obtained from Analysis Report File."));
                mem.DesignReport.Add(string.Format(""));
                if (mem.MaxTensionForce.Force != 0.0)
                {
                    if (mem.MaxTensionForce.MemberNo != 0 && mem.MaxTensionForce.MemberNo != 0)
                    {
                        //[MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Force, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                        mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN      [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("CORRESPONDING TENSILE STRESS = {0:f3} kN/sq.m       [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce.Stress, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                        mem.DesignReport.Add(string.Format(""));
                    }
                    else
                    {
                        //[MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Force, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                        mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN", mem.MaxTensionForce));
                        mem.DesignReport.Add(string.Format(""));
                    }
                }
                if (mem.MaxCompForce.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));

                    if (mem.MaxCompForce.MemberNo != 0 && mem.MaxCompForce.MemberNo != 0)
                    {

                        mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN       [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("CORRESPONDING COMPRESSIVE STRESS = {0:f3} kN/sq.m      [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Stress, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                        mem.DesignReport.Add(string.Format(""));
                    }
                    else
                    {

                        mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN", mem.MaxCompForce));
                        mem.DesignReport.Add(string.Format(""));
                    }
                }
                if (mem.MaxStress.Force != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("MAXIMUM STRESS = {0:f3} kN/sq.m", mem.MaxStress));
                }

            }

            mem.DesignReport.Add(string.Format(""));
            if (mem.Length != 0.0)
                mem.DesignReport.Add(string.Format("Length of Member = ly = {0:f3} m", mem.Length));
            if (bolt_dia != 0.0)
                mem.DesignReport.Add(string.Format("Diameter of Bolt = bolt_dia = {0} mm", bolt_dia));
            if (nb != 0.0)
                mem.DesignReport.Add(string.Format("No of Bolt in a Section = nb = {0} ", nb));
            mem.DesignReport.Add(string.Format(""));

            //Chiranjit
            //if (mem.SectionDetails.DefineSection != eDefineSection.Section11 &&
            //    mem.SectionDetails.DefineSection != eDefineSection.Section12 &&
            //    mem.SectionDetails.DefineSection != eDefineSection.Section13 &&
            //    mem.SectionDetails.DefineSection != eDefineSection.Section14)
            //{

            if (true)
            {
                #region Plate Details
                //mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                mem.DesignReport.Add(string.Format("---------------------"));
                mem.DesignReport.Add(string.Format(""));
                //for Ang
                if (mem.SectionDetails.SectionName.Contains("A") || mem.SectionDetails.SectionName.Contains("L"))
                {

                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));
                    mem.DesignReport.Add(string.Format(""));
                    if (S != 0.0)
                        mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                //for Beam
                else if (mem.SectionDetails.SectionName.Contains("B") || mem.SectionDetails.SectionName.Contains("S") || mem.SectionDetails.SectionName.Contains("W"))
                {
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    mem.DesignReport.Add(string.Format(""));
                    if (S != 0.0)
                        mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                //for Channel
                else if (mem.SectionDetails.SectionName.Contains("C"))
                {
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    mem.DesignReport.Add(string.Format(""));
                    if (S != 0.0)
                        mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                }
                if ((Bp * tp) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                }
                if ((Bbp * tbp) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                }
                if ((Bs * ts) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    mem.DesignReport.Add(string.Format("No Of Side Plates = ns = {0}", ns));
                }
                if ((Bss * tss) != 0.0)
                {
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                }
                mem.DesignReport.Add(string.Format(""));
                #endregion Plate Details
            }
            #region Define Sections
            switch (mem.SectionDetails.DefineSection)
            {
                case eDefineSection.Section1:
                    #region Section 1
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    mem.DesignReport.Add(string.Format("---------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));

                    //tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);


                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    //mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));

                    //Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0);
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0)"));
                    //mem.DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + ({4} * {5} * {5} * {5} / 12.0)",
                    //    Iyy, a, S, n, tp, Bp));
                    //mem.DesignReport.Add(string.Format("                  = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));



                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (D / 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (D / 2)^2) * n"));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    // Top Plate
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3}  ", Iyy, a, D, n));
                    Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (D / 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]





                    A = n * a * 100 + (tp * Bp * np);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4})",
                        n, a, tp, Bp, np));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));


                    Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, tf, tp));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm ", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3}/ {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));


                    #endregion Section 1
                    break;
                case eDefineSection.Section2:
                    #region Section 2

                    //tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);

                    a = tabChannel.Area;
                    D = tabChannel.Depth;
                    Iyy = tabChannel.Iyy;
                    t = tabChannel.WebThickness;
                    tf = tabChannel.FlangeThickness;
                    Cxx = tabChannel.CentreOfGravity;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = np = {0}", nbp));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n
                        + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp)
                        + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;




                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]








                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {8}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
                    //    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns));

                    //mem.DesignReport.Add(string.Format("           = {0:E3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss) + (tbp * Bbp * nbp);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * (Math.Pow((S - t - (tp / 2.0)), 2))) * np;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp**3 / 12.0) * (S - t - (tp / 2.0))^2)) * np"));
                    //mem.DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + (({4} * {5}^3 / 12.0) * ({2} - {6} - ({4} / 2.0))^2)) * {7}",
                    //                                                              Iyy, a, S, n, tp, Bp, t, np));
                    //mem.DesignReport.Add(string.Format("                  = {0:f3} sq.sq.mm", Iy));
                    //mem.DesignReport.Add(string.Format(""));

                    //A = n * a * 100 + (tp * Bp * np));
                    //mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np)"));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4})", n, a, tp, Bp, np));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    //mem.DesignReport.Add(string.Format(""));
                    //Anet = A - nb * ((bolt_dia + 1.5) * (t + tp)));
                    //mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + tp))"));
                    //mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))", A, nb, bolt_dia, t, tp));
                    //mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    //mem.DesignReport.Add(string.Format(""));
                    ry_anet = Math.Sqrt(Iy / Anet);
                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / A)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:E3} / {1:f3})", Iy, A));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format("ry_Anet = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:E3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry_anet));

                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 2
                    break;
                case eDefineSection.Section3:
                    #region Section 3
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    //tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0}", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));

                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]




                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns); ;
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 3
                    break;
                case eDefineSection.Section4:
                    #region Section 4
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0}", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Centre of Gravity = Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + (tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    ////Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    //mem.DesignReport.Add(string.Format("Moment of Intertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("          Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns + (tss * Bss * (((S / 2) - t - (tss / 2.0))^2)) * nss"));
                    //mem.DesignReport.Add(string.Format("             = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10} + ({11} * {12} * ((({2} / 2) - {13} - ({11} / 2.0))^2)) * {14}",
                    //                                                                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns, tss, Bss, t, nss));


                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]



                    //mem.DesignReport.Add(string.Format("             = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
                        n, a, tp, Bp, np, ts, Bs, ns, tss, Bss, nss));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));

                    #endregion Section 4
                    break;

                case eDefineSection.Section5: // Stringer
                    #region Section 5
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MOMENT & FORCE  DATA"));
                    mem.DesignReport.Add(string.Format("--------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    if (mem.MaxBendingMoment.MemberNo != 0 && mem.MaxBendingMoment.MemberNo != 0)
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxBendingMoment.MemberNo, mem.MaxBendingMoment.Loadcase));
                        //mem.DesignReport.Add(string.Format(""));
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m ", M));
                        //mem.DesignReport.Add(string.Format(""));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    if (mem.MaxBendingMoment.MemberNo != 0 && mem.MaxBendingMoment.MemberNo != 0)
                    {
                        mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN      [MemberNo = {1}, LoadNo = {2}]", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));

                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN ", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));
                    }
                    //mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m", M));
                    //mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN", F));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));

                    tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    Ixx = tabBeam.Ixx;
                    Zxx = tabBeam.Zxx;

                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = np = {0}", nbp));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm.", Iyy));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0}", tf));


                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));


                    // Chiranjit [2011 10 21] this formula is wrong
                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia

                    mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    // Top Plate
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0} * 10000 ", Ixx));
                    Ix = Ixx * 10000.0;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Ix += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Ix += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Ix += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Ix += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));



                    #endregion Sandiapan Goswami [2011 10 26]

                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + ((Bbp * tbp^3 / 12.0) + (tbp * Bbp) * ((((D / 2) + (tbp / 2))^2))) * nbp "));
                    //mem.DesignReport.Add(string.Format("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({2} / 2))^2))) * {4} ",
                    //                Ixx, Bbp, tbp, D, nbp));
                    //mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    mem.DesignReport.Add(string.Format(""));

                    //Ix = Ixx * 10000 + ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));

                    A = n * a * 100 + (tbp * Bbp * nbp);

                    A = n * a * 100 + (tbp * Bbp * nbp) + (tss * Bss * nss);

                    Z = (M * 10e5) / sigma_b;
                    mem.DesignReport.Add(string.Format("Required Section Modulus = Zr = M/σ_b"));
                    mem.DesignReport.Add(string.Format("                         = {0}*10^6 / {1}", M, sigma_b));
                    mem.DesignReport.Add(string.Format("                         = {0:e3} cu.mm", Z));
                    mem.DesignReport.Add(string.Format(""));

                    double y = (D / 2) + tbp;
                    mem.DesignReport.Add(string.Format("Distance from Center to Bottom most edge of Section = y"));
                    mem.DesignReport.Add(string.Format("                    y = (D/2) + tbp"));
                    mem.DesignReport.Add(string.Format("                      = ({0}/2) + {1}", D, tbp));
                    mem.DesignReport.Add(string.Format("                      = {0:f3} mm", y));
                    mem.DesignReport.Add(string.Format(""));

                    double chk_Z = Ix / y;
                    mem.DesignReport.Add(string.Format("Section Modulus = Z = Ix/y"));
                    mem.DesignReport.Add(string.Format("                    = {0:e3}/{1}", Ix, y));
                    //mem.DesignReport.Add(string.Format("                    = {0:f3} cu.mm", chk_Z));
                    //mem.DesignReport.Add(string.Format(""));
                    mem.Capacity_SectionModulus = chk_Z;
                    mem.Required_SectionModulus = Z;

                    if (chk_Z < Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm < Zr ({1:e3} cu.mm) ,  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    else if (chk_Z > Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm > Zr ({1:e3} cu.mm)  ,  So, OK", chk_Z, Z));
                        //mem.Result = "OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm =  Zr ({1:e3} cu.mm) ,  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    mem.DesignReport.Add(string.Format(""));


                    //double shr_stress = (F * 1000.0) / (t * D);
                    //Chiranjit [2011 10 24] acording to Mr. S.Goswami this formula should be
                    //shr_stress = (F * 1000.0) / (t * D + tbp*bbp)
                    double shr_stress = (F * 1000.0) / (t * D + tbp * Bbp);
                    mem.DesignReport.Add(string.Format("Shear Stress = (F * 1000.0)/(t * D + tbp * Bbp)"));
                    mem.DesignReport.Add(string.Format("             = ({0} * 1000.0)/({1} * {2} + {3} * {4})", F, t, D, tbp, Bbp));
                    mem.Capacity_ShearStress = shr_stress;
                    mem.Required_ShearStress = sigma_c;

                    mem.MaxCompForce.Force = 0.0;
                    mem.MaxTensionForce.Force = 0.0;

                    if (shr_stress < sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm < Permissible Shear Stress (σ_c = {1} N/sq.mm)  , So, OK", shr_stress, sigma_c));
                        //mem.Result = "OK";
                    }
                    else if (shr_stress >= sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm > Permissible Shear Stress (σ_c = {1} N/sq.mm)  , So, NOT OK", shr_stress, sigma_c));
                        mem.Result = "NOT OK";
                    }
                    goto _SWWrite;
                    #endregion Section 5
                    return;
                    break;

                case eDefineSection.Section6: // Cross Girder
                    #region Section 6
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MOMENT & FORCE  DATA"));
                    mem.DesignReport.Add(string.Format("--------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    if (mem.MaxBendingMoment.MemberNo != 0 && mem.MaxBendingMoment.Loadcase != 0)
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxBendingMoment.MemberNo, mem.MaxBendingMoment.Loadcase));
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m ", M));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    if (mem.MaxShearForce.MemberNo != 0 && mem.MaxShearForce.Loadcase != 0)
                        mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN      [MemberNo = {1}, LoadNo = {2}]", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));
                    else
                        mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN", F));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));

                    tabBeam = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    Ixx = tabBeam.Ixx;
                    Zxx = tabBeam.Zxx;

                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Ixx = {0} sq.sq.cm", Ixx));


                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    // Chiranjit [2011 10 21] this formula is wrong
                    //Ix = Ixx * 10000 + ((tbp * Bbp * Bbp * Bbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));
                    Ix = Ixx * 10000 + ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));

                    Ix = Ixx * 10000.0;
                    Ix += ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0)));
                    Ix += (tss * Bss * Bss * Bss / 12.0) * nss;


                    #region Chiranjit [2011 10 26]
                    //According to Mr. Sandipan Goswami
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + ((Bbp * tbp^3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)))"));
                    //mem.DesignReport.Add(string.Format("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({2} / 2))^2))) * {4} + ({5} * ({6} * {7}) * (({8} / 2.0) + ({6} / 2.0))) ",
                    //                                                                                                            Ixx, Bbp, tbp, D, nbp, nss, tss, Bss, t));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    //mem.DesignReport.Add(string.Format(""));
                    #endregion Chiranjit [2011 10 26]
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + (Bbp * tbp^3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    //mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12.0) * nss"));
                    //mem.DesignReport.Add(string.Format("                       = {0} * 10000 + ({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + (tbp / 2))^2)) * {4}", Ixx, Bbp, tbp, D, nbp));
                    //mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    #region Sandiapan Goswami [2011 10 26]   Moment of Inertia

                    mem.DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = {0} * 10000 ", Ixx));
                    Ix = Ixx * 10000.0;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Ix += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Ix += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Ix += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Ix += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));



                    #endregion Sandiapan Goswami [2011 10 26]

                    mem.DesignReport.Add(string.Format(""));

                    Z = (M * 10e5) / sigma_b;
                    mem.DesignReport.Add(string.Format("Required Section Modulus = Zp = M/σ_b"));
                    mem.DesignReport.Add(string.Format("                         = {0}*10^6 / {1}", M, sigma_b));
                    mem.DesignReport.Add(string.Format("                         = {0:e3} cu.mm", Z));
                    mem.DesignReport.Add(string.Format(""));

                    y = (D / 2) + tbp;
                    mem.DesignReport.Add(string.Format("Distance from Center to Bottom most edge of Section = y"));
                    mem.DesignReport.Add(string.Format("                    y = (D/2) + tbp"));
                    mem.DesignReport.Add(string.Format("                      = ({0}/2) + {1}", D, tbp));
                    mem.DesignReport.Add(string.Format("                      = {0:f3} mm", y));
                    mem.DesignReport.Add(string.Format(""));

                    chk_Z = Ix / y;
                    mem.DesignReport.Add(string.Format("Section Modulus = Z = Ix/y"));
                    mem.DesignReport.Add(string.Format("                    = {0:e3}/{1}", Ix, y));
                    mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm", chk_Z));
                    mem.DesignReport.Add(string.Format(""));
                    mem.Required_SectionModulus = Z;
                    mem.Capacity_SectionModulus = chk_Z;

                    if (chk_Z > Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm > Zp ({1:e3} cu.mm),  So, OK", chk_Z, Z));
                        //mem.Result = "OK";
                    }
                    else if (chk_Z < Z)
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm < Zp ({1:e3} cu.mm),  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                    = {0:e3} cu.mm = Zp ({1:e3} cu.mm) ,  So, NOT OK", chk_Z, Z));
                        mem.Result = "NOT OK";
                    }
                    mem.DesignReport.Add(string.Format(""));


                    shr_stress = (F * 1000.0) / (t * D + tbp * Bbp);
                    mem.DesignReport.Add(string.Format("Shear Stress = (F * 1000.0) /(t * D + tbp * Bbp)"));
                    mem.DesignReport.Add(string.Format("             = ({0:f3} * 1000.0) / ({1} * {2} + {3} * {4})", F, t, D, tbp, Bbp));
                    mem.Capacity_ShearStress = shr_stress;
                    mem.Required_ShearStress = sigma_c;

                    mem.MaxCompForce.Force = 0.0;
                    mem.MaxTensionForce.Force = 0.0;
                    if (shr_stress < sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm <  Permissible Shear Stress (σ_c = {1} N/sq.mm) , So, OK", shr_stress, sigma_c));
                        //mem.Result = "OK";
                    }
                    else if (shr_stress >= sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f3} N/sq.mm > Permissible Shear Stress (σ_c = {1} N/sq.mm)  , So, NOT OK", shr_stress, sigma_c));
                        mem.Result = "NOT OK";
                    }
                    goto _SWWrite;
                    #endregion Section 6
                    return;
                    break;
                case eDefineSection.Section7:
                    #region Section 7
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0} mm", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0} mm", ns));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //mem.DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //mem.DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0} mm", nss));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));


                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy "));
                    //mem.DesignReport.Add(string.Format("      Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    //mem.DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
                    //                Iyy, a, S, Cxx, n));





                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]







                    //mem.DesignReport.Add(string.Format("         = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    mem.DesignReport.Add(string.Format("A = n * a * 100"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100",
                        n, a));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * t)"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
                        A, nb, bolt_dia, t));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));

                    #endregion Section 7
                    break;
                case eDefineSection.Section8:
                    #region Section 8
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Ixx = tabAngle.Ixx;
                    Cxx = tabAngle.Cxx;
                    Zxx = tabAngle.Zxx;
                    Zyy = tabAngle.Zyy;

                    //n = 4;
                    t = tabAngle.Thickness;
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area of Section  = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Moment of Inertia  = Ixx = {0} sq.sq.cm", Ixx));
                    //mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format("Section Modulus = Zyy = {0} Cu.cm", Zyy));
                    mem.DesignReport.Add(string.Format("Section Modulus = Zxx = {0} Cu.cm", Zxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("MOMENT & FORCE  DATA"));
                    mem.DesignReport.Add(string.Format("--------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    
                    double P = mem.MaxAxialForce.Force;

                    mem.MaxCompForce = mem.MaxAxialForce;
                    mem.MaxTensionForce = mem.MaxAxialForce;


                    if (mem.MaxAxialForce.MemberNo != 0)
                    {
                        mem.DesignReport.Add(string.Format("Maximum Axial Force (Tension or Compression) = P  = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", P, mem.MaxAxialForce.MemberNo, mem.MaxAxialForce.Loadcase));
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m ", P));
                    }

                    if (mem.MaxBendingMoment.MemberNo != 0 && mem.MaxBendingMoment.MemberNo != 0)
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m   [MemberNo = {1}, LoadNo = {2}]", M, mem.MaxBendingMoment.MemberNo, mem.MaxBendingMoment.Loadcase));
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m ", M));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    if (mem.MaxBendingMoment.MemberNo != 0 && mem.MaxBendingMoment.MemberNo != 0)
                    {
                        mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN      [MemberNo = {1}, LoadNo = {2}]", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));

                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN ", F, mem.MaxShearForce.MemberNo, mem.MaxShearForce.Loadcase));
                    }
                    //mem.DesignReport.Add(string.Format("Maximum Bending Moment = M = {0:f3} kN-m", M));
                    //mem.DesignReport.Add(string.Format("Maximum Shear Force = F = {0:f3} kN", F));
                    //mem.DesignReport.Add(string.Format(""));






                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Design for Flexure and Shear"));
                    mem.DesignReport.Add(string.Format("----------------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Permissible Bending Stress = σ_b = {0} N/Sq.mm", sigma_b));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Permissible Shear Stress   = σ_c = {0} N/sq.mm", sigma_c));
                    mem.DesignReport.Add(string.Format(""));

                    double Zp = M * 1000000 / sigma_b;
                    mem.DesignReport.Add(string.Format("Required Section Modulus = Zp = M/σ_b"));
                    mem.DesignReport.Add(string.Format("                         = {0:f4}*10^6 / {1}", M, sigma_b));
                    mem.DesignReport.Add(string.Format("                         = {0:E3} cu.mm", Zp));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));


                    Z = Zxx * 1000;


                    if (Z < Zp)
                    {
                        mem.DesignReport.Add(string.Format("Provided Section Modulus = Z = {0:E3} Cu.Mm < {1:E3} cu.mm,   Hence NOT OK,", Z, Zp));
                        mem.Result = "NOT OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("Provided Section Modulus = Z = {0:E3} Cu.Mm > {1:E3} cu.mm,   Hence OK,", Z, Zp));
                    }


                    mem.Capacity_SectionModulus = Z;
                    mem.Required_SectionModulus = Zp;



                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = a;
                    shr_stress = (F * 1000.0) / (A * 100.0);
                    mem.DesignReport.Add(string.Format("Shear Stress = (F * 1000.0) / A"));
                    mem.DesignReport.Add(string.Format("             = ({0:f3} * 1000.0) / ({1} x 100)", F, A));



                    mem.Capacity_ShearStress = shr_stress;
                    mem.Required_ShearStress = sigma_c;

                    if (shr_stress > sigma_c)
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f2} N/sq.mm >  Permissible Shear Stress (σ_c = {1} N/sq.mm) , Hence NOT OK.", shr_stress, sigma_c));
                        mem.Result = "NOT OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("             = {0:f2} N/sq.mm <  Permissible Shear Stress (σ_c = {1} N/sq.mm) , Hence OK.", shr_stress, sigma_c));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    
                    #endregion Section 8

                    #region Section 5
                  

                    //goto _SWWrite;
                    #endregion Section 5

                    break;
                case eDefineSection.Section9:
                    #region Section 9
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));
                    //mem.DesignReport.Add(string.Format("           = {0:f3} sq.sq.mm", Iy));




                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]






                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 9
                    break;
                case eDefineSection.Section10:
                    #region Section 10

                    tabChannel = iApp.Tables.Get_ChannelData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);
                    a = tabChannel.Area;
                    D = tabChannel.Depth;
                    Iyy = tabChannel.Iyy;
                    t = tabChannel.WebThickness;
                    tf = tabChannel.FlangeThickness;
                    Cxx = tabChannel.CentreOfGravity;
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //mem.DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //mem.DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //mem.DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {9}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns));

                    //mem.DesignReport.Add(string.Format("           = {0:E3} sq.sq.mm", Iy));







                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    mem.DesignReport.Add(string.Format(""));
                    // Top Plate
                    mem.DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        mem.DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]








                    mem.DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns);
                    mem.DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns)"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
                        n, a, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    mem.DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / Anet);
                    mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));

                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 10
                    break;
                case eDefineSection.Section11:
                    #region Section 11
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = 2 * Iyy * 10000;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = 2 * Iyy *10000"));
                    mem.DesignReport.Add(string.Format("                       = 2 * {0} *10000", Iyy));
                    mem.DesignReport.Add(string.Format("                       = {0} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //mem.DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));

                    //mem.DesignReport.Add(string.Format("           = {0:f3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));

                    A = 2 * a * 100;
                    Anet = A;
                    mem.DesignReport.Add(string.Format("A = 2 * a * 100 = 2 * {0} * 100 = {1} sq.mm", a, A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 11
                    break;
                case eDefineSection.Section12:
                    #region Section 12
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //mem.DesignReport.Add(string.Format("---------------------"));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //mem.DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //mem.DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //mem.DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = 2 * Iyy * 10000;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy = 2 * Iyy * 10000"));
                    mem.DesignReport.Add(string.Format("                       = 2 * {0} * 10000", Iyy));
                    mem.DesignReport.Add(string.Format("                       = {0} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = 2 * a * 100;
                    Anet = A;
                    mem.DesignReport.Add(string.Format("A = 2 * a * 100 = 2 * {0} * 100 = {1} sq.mm", a, A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;

                case eDefineSection.Section13:
                    #region Section 13
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    mem.DesignReport.Add(string.Format("---------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;


                    Bp = Bs;
                    tp = ts;



                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    mem.DesignReport.Add(string.Format("Central Plate Width = Bp = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format("Central Plate Thickness = tp = {0} mm", tp));
                    mem.DesignReport.Add(string.Format("No Of Central Plate = np = {0}", np));
                    //mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //mem.DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //mem.DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} mm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + tp / 2), 2.0)) + tp * (Bp * Bp * Bp / 12.0) * np;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np"));
                    mem.DesignReport.Add(string.Format("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5} * {5} * {5} / 12.0) * {6}", n,
                        Iyy, a, Cxx, tp, Bp, np));
                    mem.DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = a * n * 100 + tp * Bp * np;
                    mem.DesignReport.Add(string.Format("A = {0} * {1} * 100 + {2} * {3} * {4} = {5:f3} sq.mm", a, n, tp, Bp, np, A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));


                    Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp);
                    mem.DesignReport.Add(string.Format("Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp)"));
                    mem.DesignReport.Add(string.Format("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, tp));
                    mem.DesignReport.Add(string.Format("     = {0} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;
                case eDefineSection.Section14:
                    #region Section 14
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    mem.DesignReport.Add(string.Format("---------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, mem.SectionDetails.SectionName, mem.SectionDetails.SectionSize, mem.SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode, mem.SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;


                    ns = 1;
                    mem.DesignReport.Add(string.Format(""));
                    //mem.DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    mem.DesignReport.Add(string.Format("Central Plate Width = Bp = {0} mm", Bs));
                    mem.DesignReport.Add(string.Format("Central Plate Thickness = tp = {0} mm", ts));
                    mem.DesignReport.Add(string.Format("No Of Central Plate = np = {0}", ns));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Top Plate Width = Bs = {0} mm", Bp));
                    mem.DesignReport.Add(string.Format("Top Plate Thickness = ts = {0} mm", tp));
                    mem.DesignReport.Add(string.Format("No Of Top Plate = ns = {0}", np));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Bottom Plate Width = Bs = {0} mm", Bbp));
                    mem.DesignReport.Add(string.Format("Bottom Plate Thickness = ts = {0} mm", tbp));
                    mem.DesignReport.Add(string.Format("No Of Bottom Plate = ns = {0}", nbp));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    mem.DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //mem.DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    mem.DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    mem.DesignReport.Add(string.Format("Cxx = {0} mm", Cxx));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    mem.DesignReport.Add(string.Format("------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("For Combined Section :"));
                    mem.DesignReport.Add(string.Format("----------------------"));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + ts / 2), 2.0)) + ts * (Bs * Bs * Bs / 12.0) * ns + tp * (Bp * Bp * Bp / 12.0) * np + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp;
                    mem.DesignReport.Add(string.Format("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np + ts * (Bs * Bs * Bs / 12.0) * ns + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp"));
                    mem.DesignReport.Add(string.Format("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5}^3 / 12.0) * {6} + {7} * ({8}^3 / 12.0) * {9} + {10} * ({11}^3 / 12.0) * {12}", n,
                        Iyy, a, Cxx, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp));
                    mem.DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns;
                    mem.DesignReport.Add(string.Format("A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns"));
                    mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + {2} * {3} * {4} + {5} * {6} * {7} + {8} * {9} * {10}", a, n, tp, Bp, np, tbp, Bbp, nbp, ts, Bs, ns));
                    mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    //mem.DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //mem.DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));


                    Anet = A - ((bolt_dia + 1.5) * n * (2 * t + ts));
                    mem.DesignReport.Add(string.Format("Anet = A - (bolt_dia + 1.5) * n * (2 * t + ts)"));
                    mem.DesignReport.Add(string.Format("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, ts));
                    mem.DesignReport.Add(string.Format("     = {0} sq.mm", Anet));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    mem.DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    mem.DesignReport.Add(string.Format("   = SQRT({0:E3} / {1})", Iy, Anet));
                    mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    mem.DesignReport.Add(string.Format(""));
                    l = (mem.Length * 1000);

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;
            }
            #endregion Define Section

            //Iy = Iyy * 10000;


            Anet = A * 100;
            Iy = Iyy * 10000;

            if (mem.MaxCompForce.Force == 0.0) mem.Compressive_Stress = 0.0;
            if (mem.MaxTensionForce.Force == 0.0) mem.Tensile_Stress = 0.0;
            if (mem.MaxCompForce.Force != 0.0)
            {
                #region Compression
                //double L = mem.Length;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("-----------------------------------"));
                mem.DesignReport.Add(string.Format("COMPRESSIVE LOAD CARRYING CAPACITY"));
                mem.DesignReport.Add(string.Format("-----------------------------------"));
                mem.DesignReport.Add(string.Format(""));


                mem.DesignReport.Add(string.Format(""));
                ry = Math.Sqrt(Iy / Anet);
                mem.DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                mem.DesignReport.Add(string.Format("   = SQRT({0:f3}/ {1:f3})", Iy, Anet));
                mem.DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                mem.DesignReport.Add(string.Format(""));


                if (mem.MaxCompForce.Force != 0.0)
                {
                    if (mem.MaxCompForce.Stress == 0.0)
                    {

                        mem.MaxCompForce.Stress = mem.MaxCompForce.Force / (Anet / 1000000);
                    }

                    if (mem.MaxCompForce.MemberNo != 0 && mem.MaxCompForce.MemberNo != 0)
                    {

                        mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN    [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Force, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("COMPRESSIVE STRESS = {0:f3} kN/m^2    [MemberNo = {1} , LoadNo = {2}]", mem.MaxCompForce.Stress, mem.MaxCompForce.MemberNo, mem.MaxCompForce.Loadcase));
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("MAXIMUM COMPRESSIVE FORCE = {0:f3} kN", mem.MaxCompForce.Force));
                        mem.DesignReport.Add(string.Format(""));

                        mem.DesignReport.Add(string.Format(""));
                        mem.DesignReport.Add(string.Format("COMPRESSIVE STRESS = {0:f3} kN/m^2", mem.MaxCompForce.Stress));
                    }

                }
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));

                double ly = 0.85 * mem.Length * 1000;
                //Chiranjit [2011 10 20]  
                //if (mem.MemberType == eMemberType.TopChord)
                //{
                //    ly = mem.Length * 1000;
                //    mem.DesignReport.Add(string.Format("Effective Length = ly = {0:f3} mm", ly));
                //}
                //else
                //{
                mem.DesignReport.Add(string.Format("Effective Length = 0.85*ly = 0.85 * {0:f3} = {1:f3} mm", mem.Length * 1000, ly));
                //}
                double lamda = ly / ry;
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("   λ  = ly / ry"));
                mem.DesignReport.Add(string.Format("      = {0:f3} / {1:f3} = {2:f3}", ly, ry, lamda));

                double sigma = Get_Table_1_Sigma_Value(lamda, fy);
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("From Table 4,   σ_ac = {0} N/sq.mm", sigma));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));

                double comp_load_cap = sigma * (Anet / 1000.0);
                //double comp_load_cap = sigma * (A / 1000.0);

                comp_load_cap = MyList.StringToDouble(comp_load_cap.ToString("f3"), 0.0);
                //Chiranjit [2011 07 01] 
                //Store Compressive Stress
                //mem.Compressive_Stress = (mem.MaxCompForce * 1000) / (A);
                mem.Capacity_Compressive_Stress = sigma;


                mem.Capacity_CompForce = comp_load_cap;
                mem.DesignReport.Add(string.Format("Compressive Load Carrying Capacity = σ_ac * A   N"));
                mem.DesignReport.Add(string.Format("                                   = ({0}*{1:f3})/1000   kN", sigma, A));

                if (comp_load_cap > mem.MaxCompForce.Force)
                {

                    //mem.DesignReport.Add(string.Format("                                   = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Compressive Force OK", comp_load_cap, mem.MaxCompForce.Force, mem.Group.MemberNosText));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN > {1:f3} kN,  Compressive Force OK", comp_load_cap, mem.MaxCompForce.Force, mem.Group.MemberNosText));
                    mem.Result = "OK";
                    Check_Comp = true;
                }
                else if (comp_load_cap < mem.MaxCompForce.Force)
                {
                    //mem.DesignReport.Add(string.Format("                                   = {0:f3} kN < {1:f3} kN,  Maximum Group [{2}] Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText));
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN < {1:f3} kN,  Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Comp = false;
                }
                else
                {
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} kN = {1:f3} kN,  Compressive Force NOT OK", comp_load_cap, mem.MaxCompForce, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Comp = false;
                }

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Compressive Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/sq.mm", Math.Abs(mem.MaxCompForce.Stress), Math.Abs(mem.MaxCompForce.Stress / 1000.0)));
                mem.DesignReport.Add(string.Format(""));
                if ((sigma) > Math.Abs(mem.MaxCompForce.Stress / 1000.0))
                {
                    //mem.DesignReport.Add(string.Format("Maximum Compressive Stress = {0:f3} N/sq.mm", mem.MaxStress/1000.0));
                    mem.Compressive_Stress = Math.Abs(mem.MaxCompForce.Stress / 1000.0);
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Compressive Stress OK", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                    //mem.Result = "OK";
                    //Check_Comp = true;
                }
                else
                {
                    //mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress   NOT OK", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                    //mem.Result = "NOT OK";


                    if (!Check_Comp)
                    {
                        Check_Comp = false;
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress   NOT OK", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                        mem.Result = "NOT OK";
                    }
                    else
                    {
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Compressive Stress", Math.Abs(mem.MaxCompForce.Stress / 1000.0), sigma));
                    }
                }

                if (n > 1 || mem.SectionDetails.LateralSpacing > 0)
                    DesignLacing(sw, mem, lamda);
                #endregion Compression
            }
            else
            {
                Check_Comp = true;
            }
            if (mem.MaxTensionForce.Force != 0.0)
            {
                #region Tensile
                //mem.DesignReport.Add(string.Format(""));
                //mem.DesignReport.Add(string.Format("Tensile Load Carrying Capacity = "));
                double tensile_load_cap = (Anet * ft) / 1000.0;
                tensile_load_cap = MyList.StringToDouble(tensile_load_cap.ToString("f3"), 0.0);


                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("------------------------------"));
                mem.DesignReport.Add(string.Format("TENSILE LOAD CARRYING CAPACITY"));
                mem.DesignReport.Add(string.Format("------------------------------"));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.Capacity_TensionForce = tensile_load_cap;

                if (mem.MaxTensionForce.Stress == 0.0)
                {
                    mem.MaxTensionForce.Stress = mem.MaxTensionForce.Force / (Anet / 1000000);
                }
                if (mem.MaxTensionForce.MemberNo != 0 && mem.MaxTensionForce.MemberNo != 0)
                {

                    mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN    [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce.Force, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("TENSILE STRESS = {0:f3} kN/m^2    [MemberNo = {1} , LoadNo = {2}]", mem.MaxTensionForce.Stress, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                }
                else
                {

                    mem.DesignReport.Add(string.Format("MAXIMUM TENSILE FORCE = {0:f3} kN ", mem.MaxTensionForce.Force, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));

                    mem.DesignReport.Add(string.Format(""));
                    mem.DesignReport.Add(string.Format("TENSILE STRESS = {0:f3} kN/m^2 ", mem.MaxTensionForce.Stress, mem.MaxTensionForce.MemberNo, mem.MaxTensionForce.Loadcase));
                    mem.DesignReport.Add(string.Format(""));
                }

                //Chiranjit [2011 07 01] 
                //Store Tensile Stress
                mem.Capacity_Tensile_Stress = ft;
                //mem.Tensile_Stress = (mem.MaxTensionForce*1000) / (Anet);



                mem.DesignReport.Add(string.Format("Tensile Load Carrying Capacity = Anet * ft   N"));
                mem.DesignReport.Add(string.Format("                               = ({0:f3}*{1})/1000   kN", Anet, ft));
                bool flag = tensile_load_cap > mem.MaxTensionForce.Force;
                if (tensile_load_cap > mem.MaxTensionForce.Force)
                {

                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN > {1:f3} kN,  Maximum Group [{2}] Tensile Force OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "OK";
                    Check_Tens = true;
                }
                else if (tensile_load_cap < mem.MaxTensionForce.Force)
                {
                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN < {1:f3} kN, Maximum Group [{2}] Tensile Force, NOT OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Tens = false;
                }
                else
                {
                    mem.DesignReport.Add(string.Format("                               = {0:f3} kN = {1:f3} kN,  Maximum Group [{2}] Tensile Force NOT OK", tensile_load_cap, mem.MaxTensionForce.Force, mem.Group.MemberNosText));
                    mem.Result = "NOT OK";
                    Check_Tens = false;

                }

                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format(""));
                mem.DesignReport.Add(string.Format("Tensile Stress from Analysis  = σ_c = {0:f3} kN/m^2 = {1:f3} N/sq.mm", mem.MaxTensionForce.Stress, (mem.MaxTensionForce.Stress / 1000.0)));
                mem.DesignReport.Add(string.Format(""));
                if ((ft) > (mem.MaxTensionForce.Stress / 1000.0))
                {
                    //mem.DesignReport.Add(string.Format("Maximum Compressive Stress = {0:f3} N/sq.mm", mem.MaxStress/1000.0));
                    mem.Tensile_Stress = (mem.MaxTensionForce.Stress / 1000.0);
                    mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 < {1:f3} N/mm^2, Allowable Tensile Stress  OK", mem.MaxTensionForce.Stress / 1000.0, ft));
                    //mem.Result = "OK";

                    //Check_Tens = true;
                }
                else
                {
                    if (flag)
                    {
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress", mem.MaxTensionForce.Stress / 1000.0, ft));
                    }
                    else
                        mem.DesignReport.Add(string.Format("                                   = {0:f3} N/mm^2 > {1:f3} N/mm^2, Allowable Tensile Stress  NOT OK", mem.MaxTensionForce.Stress / 1000.0, ft));


                    Check_Tens = flag;
                    mem.Result = flag ? "  OK" : "NOT OK";
                }


                //if (!Check_Tens && !Check_Comp)
                //{
                //    mem.Result = "NOT OK";
                //}
                //else
                //    mem.Result = "OK";


                DesignConnection(sw, mem);

                mem.DesignReport.Add(string.Format(""));


                #endregion Tensile
            }
            else
            {
                Check_Tens = true;
            }

            if (Check_Tens && Check_Comp)
            {
                mem.Result = "OK";
            }
            else
                mem.Result = "NOT OK";


            //sw.Write(mem.DesignReport.ToArray());

            _SWWrite:
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
            foreach (string s in mem.DesignReport) sw.WriteLine(s);
            mem.DesignReport.Add(string.Format(""));
            mem.DesignReport.Add(string.Format(""));
        }

        //Chiranjit [2014 03 24] Add new Method for Update user force
        public void Update_Forces()
        {
            string kStr = "";
            CMember mbr;
            for (int i = 0; i < dgv_ana_results.RowCount; i++)
            {
                kStr = dgv_ana_results[0, i].Value.ToString();

                if (null == dgv_ana_results[3, i].Value)
                    dgv_ana_results[3, i].Value = "";
                if (null == dgv_ana_results[4, i].Value)
                    dgv_ana_results[4, i].Value = "";
                if (null == dgv_ana_results[5, i].Value)
                    dgv_ana_results[5, i].Value = "";
                if (null == dgv_ana_results[6, i].Value)
                    dgv_ana_results[5, i].Value = "";

                mbr = Complete_Design.Members.Get_Member(kStr);

                mbr.MaxCompForce = MyList.StringToDouble(dgv_ana_results[3, i].Value.ToString(), 0.0);

                mbr.MaxTensionForce = MyList.StringToDouble(dgv_ana_results[4, i].Value.ToString(), 0.0);

                mbr.MaxBendingMoment = MyList.StringToDouble(dgv_ana_results[5, i].Value.ToString(), 0.0);

                mbr.MaxShearForce = MyList.StringToDouble(dgv_ana_results[6, i].Value.ToString(), 0.0);
            }
        }


        #region Table Functions
        void WriteTable1(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "SteelTruss_Table1.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_EUDL_CDA();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 1 : E.U.D.L., C.D.A. and longitudinal Loads for Modified B.G. Loading");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        void WriteTable2(StreamWriter sw)
        {
            string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            string tab_file = Path.Combine(tab_path, "Truss_Bridge_2.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_Allowable_Working_Stress_Critical();
            //List<string> file_cont = new List<string>(File.ReadAllLines(tab_file));

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 2 : Allowable Working Stress σbc for different Values of Critical Stress");
                sw.WriteLine("-------------------------------------------------------------------------------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        void WriteTable3(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "Truss_Bridge_3.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_Allowable_Average_Shear_Stress();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 3 : Allowable Average Shear Stress in Stiffened Webs of Steel");
                sw.WriteLine("-------------------------------------------------------------------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }
        void WriteTable4(StreamWriter sw)
        {
            //string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //string tab_file = Path.Combine(tab_path, "Truss_Bridge_4.txt");

            List<string> file_cont = iApp.Tables.Get_Tables_Allowable_Working_Stress_Cross_Section();

            try
            {
                sw.WriteLine();
                sw.WriteLine("TABLE 4 : Allowable Working Stress σac in N/mm2 on Effective Cross Section for Axial Compression");
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine();
                foreach (var item in file_cont)
                {
                    sw.WriteLine(item);
                }
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}
        }

        string ref_string = "";
        public double Get_Table_1_Sigma_Value(double lamda, double fy)
        {
            return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, fy, ref ref_string);

            //lamda = Double.Parse(lamda.ToString("0.000"));

            //int indx = 5;

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");

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
            //    kStr = kStr.Replace("Fy = ", "");
            //    //if (kStr.StartsWith("--------------"))
            //    //{
            //    //    find = !find; continue;
            //    //}
            //    //if (find)
            //    //{

            //    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //    try
            //    {
            //        double d = mList.GetDouble(0);
            //        lst_list.Add(mList);

            //    }
            //    catch (Exception ex) { }
            //    //}
            //}
            //indx = lst_list[0].StringList.IndexOf(fy.ToString());
            //a1 = 0.0;
            //a2 = 0.0;
            //b1 = 0.0;
            //b2 = 0.0;
            //int _col_index = -1;
            //for (int i = 1; i < lst_list[0].Count; i++)
            //{
            //    if (fy >= lst_list[0].GetDouble(i - 1) && fy <= lst_list[0].GetDouble(i))
            //    {
            //        a1 = lst_list[0].GetDouble(i - 1);
            //        a2 = lst_list[0].GetDouble(i);
            //        _col_index = i+1;
            //        break;
            //    }
            //}
            //double v1, v2, v3, v4, val1, val2, val3;
            //v1 = 0.0; v2 = 0.0; v3 = 0.0; v4 = 0.0;
            //for (int i = 2; i < lst_list.Count; i++)
            //{
            //    b1 = lst_list[i - 1].GetDouble(0);
            //    b2 = lst_list[i].GetDouble(0);

            //    if (lamda >= b1 && lamda <= b2)
            //    {
            //        v1 = lst_list[i - 1].GetDouble(_col_index - 1);
            //        v2 = lst_list[i - 1].GetDouble(_col_index);

            //        v3 = lst_list[i].GetDouble(_col_index - 1);
            //        v4 = lst_list[i].GetDouble(_col_index);
            //        break;
            //    }
            //}
            //if (v1 == 0.0 && v2 == 0.0 && v3 == 0.0 && v4 == 0.0)
            //{
            //    v1 = lst_list[lst_list.Count - 1].GetDouble(_col_index - 1);
            //    v2 = lst_list[lst_list.Count - 1].GetDouble(_col_index);

            //    v3 = lst_list[lst_list.Count - 1].GetDouble(_col_index - 1);
            //    v4 = lst_list[lst_list.Count - 1].GetDouble(_col_index);
            //}
            ////a1 = 0.0; a2 = 0.0; b1 = 0.0; 
            ////b2 = 0.0; v1 = 0.0; 
            ////v2 = 0.0; v3 = 0.0; v4 = 0.0; 
            //val1 = 0.0; val2 = 0.0; val3 = 0.0;


            //val1 = v1 + ((v2 - v1) / (a2 - a1)) * (fy - a1);
            //val2 = v3 + ((v4 - v3) / (a2 - a1)) * (fy - a1);

            //if (v1 == v3) val1 = v1;
            //if (v2 == v4) val2 = v2;

            //returned_value = val1 + ((val2 - val1) / (b2 - b1)) * (lamda - b1);



            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;



        }
        public double Get_Table_3_Value(double d_by_t, double d_point)
        {
            return iApp.Tables.Allowable_Average_Shear_Stress(d_by_t, d_point, ref ref_string);

            //int indx = -1;

            //if (d_point >= 0.4 && d_point < 0.6)
            //    indx = 1;
            //else if (d_point >= 0.6 && d_point < 0.8)
            //    indx = 2;
            //else if (d_point >= 0.8 && d_point < 1.0)
            //    indx = 3;
            //else if (d_point >= 1.0 && d_point < 1.2)
            //    indx = 4;
            //else if (d_point >= 1.2 && d_point < 1.4)
            //    indx = 5;
            //else if (d_point >= 1.4 && d_point < 1.5)
            //    indx = 6;
            //else if (d_point >= 1.5)
            //    indx = 7;


            //d_by_t = Double.Parse(d_by_t.ToString("0.0"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, returned_value;
            ////double  b1, a2, b2, returned_value;

            ////a1 = 0.0;
            ////b1 = 0.0;
            ////a2 = 0.0;
            ////b2 = 0.0;
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

            //    if (d_by_t < a1)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        #endregion Table Functions


        public void DesignShearConnector(CMember mem, StreamWriter sw)
        {
            try
            {


                //List<string> sw = new List<string>();

                //sw.Add(string.Format(""));
                //sw.Add(string.Format(""));
                double V = mem.MaxShearForce.Force;
                double Ixx, a, D, B, tw, tf;
                double bs, ts, bp, tp, w, fck, m, L;
                //double bs, ts, bp, tp, w, d, fck, m, L;
                //top_plate_thickness


                //D = MyList.StringToDouble(txt_
                RolledSteelBeamsRow tabData = iApp.Tables.Get_BeamData_FromTable(mem.SectionDetails.SectionName, mem.SectionDetails.SectionCode);

                Ixx = tabData.Ixx;
                a = tabData.Area;
                D = tabData.Depth;
                tw = tabData.WebThickness;
                tf = tabData.FlangeThickness;
                B = tabData.FlangeWidth;

                L = mem.Length;

                bs = mem.SectionDetails.SidePlate.Width;
                ts = mem.SectionDetails.SidePlate.Thickness;
                //bp = mem.SectionDetails.TopPlate.Width;
                //tp = mem.SectionDetails.TopPlate.Thickness;
                bp = mem.SectionDetails.BottomPlate.Width;
                tp = mem.SectionDetails.BottomPlate.Thickness;

                //w = MyList.StringToDouble(txt_w.Text, 0.0);
                w = mem.Length;
                //d = MyList.StringToDouble(txt_d.Text, 0.0);
                fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                m = MyList.StringToDouble(txt_m.Text, 0.0);

                sw.WriteLine();
                sw.WriteLine("-------------------------");
                sw.WriteLine("DESIGN OF SHEAR CONNECTOR");
                sw.WriteLine("-------------------------");
                sw.WriteLine();
                sw.WriteLine("INPUT DATA");
                sw.WriteLine("-----------");
                sw.WriteLine();

                sw.WriteLine("Shear Force = V = {0} kN = {0} * 10^3 N", V);
                sw.WriteLine("Moment of Inertia about X-X axis  = Ixx = {0:f3} sq.sq.cm", Ixx);
                if (a != 0.0)
                    sw.WriteLine("Area = a =  {0:f3} sq.cm", a);
                if (D != 0.0)
                    sw.WriteLine("Depth of Girder = D = {0} mm", D);
                if (B != 0.0)
                    sw.WriteLine("Flange Width = B = {0} mm", B);
                if (tw != 0.0)
                    sw.WriteLine("Web Thickness = tw = {0} mm", tw);
                if (tf != 0.0)
                    sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
                if ((bs * ts) != 0.0)
                    sw.WriteLine("Side Plates = bs x ts = {0} x {1}", bs, ts);
                if ((bp * tp) != 0.0)
                    sw.WriteLine("Bottom Plate = bp x tp = {0} x {1}", bp, tp);
                sw.WriteLine();
                if (w != 0.0)
                    sw.WriteLine("Spacing of Cross Girders = w = {0} mm", w);
                if (d != 0.0)
                    sw.WriteLine("Thickness of Deck Slab = d = {0} mm", d);
                if (fck != 0.0)
                    sw.WriteLine("Concrete Grade = fck = M {0}", fck);
                if (m != 0.0)
                    sw.WriteLine("Modular Ratio = m = {0}", m);
                sw.WriteLine();


                sw.WriteLine("Design Calculation");
                sw.WriteLine("------------------");
                sw.WriteLine();
                sw.WriteLine();

                double Ay = (w * d / m) * (D + tp + (d / 2)) + a * 100 * ((D / 2) + tp) + bp * tp * (tp / 2) + 2 * bs * ts * ((D / 2) + tp);

                sw.WriteLine(" Ay = (w * d / m) * (D + tp + (d / 2)) + a * 100 * ((D / 2) + tp) + bp * tp * (tp / 2) + 2 * bs * ts * ((D / 2) + tp)");
                sw.WriteLine("    = ({0} * {1} / {2}) * ({3} + {4} + ({1} / 2)) + {5} * 100 * (({3} / 2) + {4}) + {6} * {4} * ({4} / 2) + 2 * {7} * {8} * (({3} / 2) + {4})",
                                w, d, m, D, tp, a, bp, bs, ts);

                sw.WriteLine("    = {0:f3} ", Ay);
                sw.WriteLine();


                double Ay1 = (w * d / m) + a * 100 + bp * tp + 2 * bs * ts;

                sw.WriteLine("Ay = (w * d / m) + a * 100 + bp * tp + 2 * bs * ts");
                sw.WriteLine("   = ({0} * {1} / {2}) + {3} * 100 + {4} * {5} + 2 * {6} * {7}",
                    w, d, m, a, bp, tp, bs, ts);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} ", Ay1);
                sw.WriteLine();

                double y = Ay / Ay1;
                sw.WriteLine(" y' = {0:f3} / {1:f3} = {2:f3} mm", Ay, Ay1, y);
                sw.WriteLine("   = Distance of Neutral Axis from bottom edge of Composite Section");
                sw.WriteLine();

                double y1 = y - ((D / 2) + tp);

                sw.WriteLine("y  = y' - ((D/2) + tp)");
                sw.WriteLine("   = {0:f3} - (({1}/2) + {2})", y, D, tp);
                sw.WriteLine("   = {0:f3} ", y1);
                sw.WriteLine("   = Distance between NA of Composite Section and C.G. of Cross Girder");
                sw.WriteLine();

                double yd = d + D + tp - y;
                sw.WriteLine("  yd = Distance of NA from top of Composite Section");
                sw.WriteLine("     = d + D + tp - y'");
                sw.WriteLine("     = {0} + {1} + {2} - {3:f3}", d, D, tp, y);
                sw.WriteLine("     = {0:f3} mm", yd);
                sw.WriteLine();

                double Iz = ((w * d) / m) * Math.Pow((yd - (d / 2)), 2) + a * 100 * Math.Pow((y - ((d / 2) + tp)), 2) + bp * tp * Math.Pow((y - (tp / 2)), 2);

                sw.WriteLine(" Iz = ((w * d) / m) * ((yd - (d / 2))^2) + a * 100 * ((y - ((d / 2) + tp))^2) + bp * tp * ((y - (tp / 2))^2)");
                sw.WriteLine();

                sw.WriteLine("    = {0:f3} sq.sq.mm", Iz);
                sw.WriteLine();

                double Ty = (V * Ay * yd) / Iz;
                sw.WriteLine("Shear Stress at top Flange Level = τy = (V x Ay x yd) / Iz");
                sw.WriteLine("                                 = ({0:f3} * {1:f3} * {2:f3}) / {3:f3}", V, Ay, yd, Iz);
                sw.WriteLine("                                 = {0:f3}", Ty);
                sw.WriteLine();

                double lamda = 0.85d;
                RolledSteelChannelsRow tabChn = tbl_rolledSteelChannels.GetDataFromTable(cmb_Shr_Con_Section_name.Text, cmb_Shr_Con_Section_Code.Text);
                tw = tabChn.WebThickness;
                tf = tabChn.FlangeThickness;
                B = tabChn.FlangeWidth;

                L = B - 2 * 10;
                double Qu = 45 * lamda * (tf + 0.5 * tw) * L * Math.Sqrt(fck);

                sw.WriteLine("Shear Capacity of H.T. Channel = Qu = 45 * λ * (tf + 0.5 * tw) * L * √fck");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Try {0} {1} ", cmb_Shr_Con_Section_name.Text, cmb_Shr_Con_Section_Code.Text);
                sw.WriteLine("Web Thickness = tw = {0} ", tw);
                sw.WriteLine("Flange Thickness = tf = {0} mm", tf);
                sw.WriteLine("Flange Width = B = {0} mm", B);

                sw.WriteLine();
                sw.WriteLine("L = B – 2 x 10 = {0} – 20 = {1}, λ = Load Reduction Factor = 0.85", B, L);
                sw.WriteLine();

                sw.WriteLine("   Qu = 45 x 0.85 x ({0} + 0.5 * {1}) x {2} x √{3}", tf, tw, L, fck);
                sw.WriteLine("      = {0:f3} N", Qu);
                sw.WriteLine("      = {0:f3} kN", Qu / 1000.0);
                sw.WriteLine("      = {0:f3} MTon", Qu / 10000.0);
                sw.WriteLine();

                double sr = Qu / Ty;
                sw.WriteLine("Spacing Required = Qu / τy = {0:f3} / {1:f2} = {2:f2} mm", Qu, Ty, sr);
                sw.WriteLine();
            }
            catch (Exception ex) { }

        }
        public void DesignLacing(StreamWriter sw, CMember mbr, double lamda)
        {
            string section_name = mbr.SectionDetails.SectionName;
            string section_code = mbr.SectionDetails.SectionCode;
            double rad = (Math.PI / 180.0);
            double lac_ang, lac_bl, lac_tl, lac_d2, lac_nr, lac_fs, lac_fb, weld_strength;


            //if (cmb_lac.SelectedIndex == 0)
            //{

            lac_ang = MyList.StringToDouble(txt_lac_ang.Text, 0.0);
            lac_bl = MyList.StringToDouble(cmb_lac_bl.Text, 0.0);
            lac_tl = MyList.StringToDouble(cmb_lac_tl.Text, 0.0);
            lac_d2 = MyList.StringToDouble(txt_lac_d2.Text, 0.0);
            lac_nr = MyList.StringToDouble(txt_lac_nr.Text, 0.0);
            lac_fs = MyList.StringToDouble(txt_lac_fs.Text, 0.0);
            lac_fb = MyList.StringToDouble(txt_lac_fb.Text, 0.0);
            weld_strength = MyList.StringToDouble(txt_weld_strength.Text, 108);

            //}
            if (cmb_lac.SelectedIndex == 1)
            {
                string kStr = cmb_lac_bl.Text.Trim();
                string kStr2 = "";
                int cnt = kStr.Length;
                if (cnt % 2 != 0)
                    cnt = cnt / 2 + 1;
                else
                    cnt = cnt / 2;
                kStr = cmb_lac_bl.Text.Trim().Substring(0, cnt);
                kStr2 = cmb_lac_bl.Text.Trim().Substring(cnt, cmb_lac_bl.Text.Trim().Length - cnt);

                lac_ang = MyList.StringToDouble(txt_lac_ang.Text, 0.0);

                lac_bl = (double.Parse(kStr) > double.Parse(kStr2)) ? double.Parse(kStr) : double.Parse(kStr2);

                //lac_bl = MyList.StringToDouble(cmb_lac_bl.Text, 0.0);
                lac_tl = MyList.StringToDouble(cmb_lac_tl.Text, 0.0);
                lac_d2 = MyList.StringToDouble(txt_lac_d2.Text, 0.0);
                lac_nr = MyList.StringToDouble(txt_lac_nr.Text, 0.0);
                lac_fs = MyList.StringToDouble(txt_lac_fs.Text, 0.0);
                lac_fb = MyList.StringToDouble(txt_lac_fb.Text, 0.0);

            }

            sw.WriteLine();
            sw.WriteLine("Design of Lacing for Compressive force");
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            sw.WriteLine("INPUT DATA");
            sw.WriteLine("-----------");
            sw.WriteLine();
            //double s = MyList.StringToDouble(mbr.SectionDetails.SectionCode, 400);


            //RolledSteelChannelsRow tab = tbl_rolledSteelChannels.GetDataFromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode);
            //double B = tab.FlangeWidth;
            //sw.WriteLine("Clear spacing between two {0} {1} = s = {2} mm.",mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode, s);
            //sw.WriteLine("Flange width of {0} {1} = B = {2} mm", mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode, B);
            //sw.WriteLine("Centre to centre distance between members = s + 2 x B/2");
            //sw.WriteLine("                                          = {0} + 2 x {1}/2", s, B);
            //sw.WriteLine("                                          = {0} mm.", cc_dis);
            //sw.WriteLine();
            sw.WriteLine("Lacing Angle = {0}˚(Degrees)", lac_ang);

            if (cmb_lac.SelectedIndex == 0)
            {
                sw.WriteLine("Lacing Plate width = bl = {0} mm", lac_bl);
                sw.WriteLine("Thickness = tl = {0} mm.", lac_tl);
            }
            else if (cmb_lac.SelectedIndex == 1)
            {
                sw.WriteLine();
                sw.WriteLine("Lacing Angle  1 x ISA {0} x {1}", cmb_lac_bl.Text, cmb_lac_tl.Text);
                sw.WriteLine();
            }
            double weld_size = MyList.StringToDouble(txt_weld_size.Text, lac_tl - 1.6);
            double throat_size = weld_size * 0.7; ;

            if (rbtn_weld.Checked)
            {


                if (weld_size > (lac_tl - 1.5))
                {
                    MessageBox.Show(this, "Weld Size = " + weld_size + " mm     must be Less than = Thickness - 1.5 = " + lac_tl + " - 1.5 = " + (lac_tl - 1.5) + " mm", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception("Weld Size greater than thickness - 1.5");
                }
                sw.WriteLine("WELD size = {0} mm  < {1} (Thickness) - 1.5mm.", weld_size, lac_tl);
                sw.WriteLine("Throat size = 0.7 x {0} = {1} mm", weld_size, throat_size);

                sw.WriteLine();
                sw.WriteLine("Welding Strength = {0} N/sq.mm", weld_strength);


            }
            else
            {
                sw.WriteLine("Bolt/Rivet Diameter = d2 = {0} mm", lac_d2);
                sw.WriteLine("Number per row = nr = {0}", lac_nr);
                sw.WriteLine("Rivet/Bolt    Shear strength = fs = {0} N/Sq. mm ", lac_fs);
                sw.WriteLine("Bearing strength = fb = {0} N/Sq.mm", lac_fb);
            }
            sw.WriteLine();
            //double s = MyList.StringToDouble(mbr.SectionDetails.SectionCode, 0.0);
            double s = mbr.SectionDetails.LateralSpacing;
            sw.WriteLine("Spacing of Member components = s = {0} mm.", s);
            sw.WriteLine();
            //double cc_dis = s + 2 * B / 2.0;

            //sw.WriteLine("Clear spacing between two ISMC 400 = s = 400 mm.");

            double B = 0.0;
            double a = 0.0;
            double cc_dis = 0.0;
            double ryy = 0.0;
            double Iy = lac_bl * lac_tl * lac_tl * lac_tl / 12.0;
            a = lac_tl * lac_bl;

            if (cmb_lac.SelectedIndex == 1)
            {
                RolledSteelAnglesRow tabData1 = iApp.Tables.Get_AngleData_FromTable("ISA", cmb_lac_bl.Text, lac_tl);
                a = tabData1.Area * 100;
                Iy = tabData1.Iyy * 10000;
                sw.WriteLine("Area    a = {0} sq.mm.", a);
                sw.WriteLine();
                sw.WriteLine("       Iy = {0:f3} sq.sq.mm.", Iy);
                sw.WriteLine();
            }
            else
            {
                sw.WriteLine("Area     a = bl * tl = {0} * {1} = {2} sq.mm.", lac_bl, lac_tl, a);
                sw.WriteLine();
                sw.WriteLine("       Iy  = bl * tl**3 / 12.0");
                sw.WriteLine("           = {0} * {1}^3 / 12.0", lac_bl, lac_tl);
                sw.WriteLine("           = {0:f3} sq.sq.mm.", Iy);
                sw.WriteLine();
            }
            if (mbr.SectionDetails.SectionName.EndsWith("A"))
            {
                RolledSteelAnglesRow tabData = iApp.Tables.Get_AngleData_FromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode, mbr.SectionDetails.AngleThickness);

                ryy = Math.Sqrt(tabData.Iyy / tabData.Area);
                B = tabData.Length_1;
                sw.WriteLine("Flange width of {0} {1}  = B = {2} mm.",
                    mbr.SectionDetails.SectionName,
                    mbr.SectionDetails.SectionCode, B);
                //double cc_dis = s + (2.0 * B / 2.0);
                cc_dis = s + (2.0 * B / 2.0);
                //will be opened when sections are back to back
                sw.WriteLine("Centre to centre distance between members = s + 2 * B/2");
                sw.WriteLine("                                          = {0} + 2 * {1}/2", s, B);
                sw.WriteLine("                                          = {0:f3} mm.", cc_dis);
            }
            else if (mbr.SectionDetails.SectionName.EndsWith("C"))
            {
                RolledSteelChannelsRow tabData = iApp.Tables.Get_ChannelData_FromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode);

                B = tabData.FlangeWidth;

                RolledSteelAnglesRow aa;

                ryy = tabData.Ryy;

                //sw.WriteLine("Flange width of {0} {1}  = B = {2} mm.",
                //    mbr.SectionDetails.SectionName,
                //    mbr.SectionDetails.SectionCode, B);
                //double cc_dis = s + (2.0 * B / 2.0);
                //will be opened when sections are back to back
                //sw.WriteLine("Centre to centre distance between members = s + 2 * B/2");
                //sw.WriteLine("                                          = {0} + 2 * {1}/2", s, B);
                //sw.WriteLine("                                          = {0:f3} mm.", cc_dis);
            }
            else if (mbr.SectionDetails.SectionName.EndsWith("B"))
            {
                RolledSteelBeamsRow tabData = iApp.Tables.Get_BeamData_FromTable(mbr.SectionDetails.SectionName, mbr.SectionDetails.SectionCode);

                //B = tabData.FlangeWidth;

                ryy = tabData.Ryy;

                //sw.WriteLine("Flange width of {0} {1}  = B = {2} mm.",
                //    mbr.SectionDetails.SectionName,
                //    mbr.SectionDetails.SectionCode, B);
                ////double cc_dis = s + (2.0 * B / 2.0);

                //will be opened when sections are back to back
                //sw.WriteLine("Centre to centre distance between members = s + 2 * B/2");
                //sw.WriteLine("                                          = {0} + 2 * {1}/2", s, B);
                //sw.WriteLine("                                          = {0:f3} mm.", cc_dis);
            }

            sw.WriteLine("Length of the member between two successive Lacing connections = l1");
            sw.WriteLine();
            //cc_dis = s;

            //double l1 = 2 * cc_dis * Math.Tan((lac_ang / 2.0) * rad);
            double l1 = cc_dis * Math.Tan((lac_ang) * rad);

            sw.WriteLine("  l1  = {0:f3} * tan({1}) = {2:f3} mm", cc_dis, lac_ang, l1);
            sw.WriteLine();



            ryy = Math.Sqrt(Iy / a);
            sw.WriteLine("Radius of gyration = ryy = SQRT(Iy/a)");
            sw.WriteLine("                   = SQRT({0:f3}/{1:f2}) mm.", Iy, a);
            sw.WriteLine("                   = {0:f3} mm.", ryy);
            sw.WriteLine();

            //sw.WriteLine("Slenderness ratio for one {0} {1} between two successive lacing connections", section_name, section_code);
            sw.WriteLine("Slenderness ratio between two successive lacing connections");

            double lamda1 = l1 / ryy;

            sw.WriteLine(" λ1 = l1 / ryy = {0:f3} / {1:f3} = {2:f3}", l1, ryy, lamda1);
            sw.WriteLine();

            sw.WriteLine("Slenderness Ratio of the Member under compressive force ");
            //double lamda = 24.874;
            sw.WriteLine(" = λ  = {0:f3}", lamda);
            sw.WriteLine();
            //Chiranjit [2011 04 13]
            //if (lamda1 <= (lamda * 0.7))
            //if (lamda1 <= (lamda * 0.7))
            //{
            //    sw.WriteLine(" λ1 = {0:f3} < 0.7 * λ = 0.7 * {1:f3} ", lamda1, lamda);
            //    sw.WriteLine("                    = {0:f3} So, Lacing geometry is OK .", (lamda * 0.7));
            //}
            //else
            //{
            //    sw.WriteLine(" λ1 = {0:f3} > 0.7 * λ = 0.7 * {1:f3} ", lamda1, lamda);
            //    sw.WriteLine("                    = {0:f3} So, Lacing geometry is NOT OK .", (lamda * 0.7));
            //}

            if (lamda1 <= 145.0)
            {
                sw.WriteLine(" λ1 = {0:f3} < 145.0  So, Lacing geometry is OK .", lamda1);
            }
            else
            {
                sw.WriteLine(" λ1 = {0:f3} > 145.0 So, Lacing geometry is NOT OK .", lamda1);
            }

            sw.WriteLine();

            sw.WriteLine("Horizontal force for which the Lacings are to be designed");
            sw.WriteLine("    = 2.5% of Compressive Load in the member");
            sw.WriteLine("    = 0.025 * {0:f3} kN", mbr.MaxCompForce.Force);
            double hor_force = 0.025 * mbr.MaxCompForce.Force;
            sw.WriteLine("    = {0:f3} kN", hor_force);
            hor_force = hor_force * 1000;
            sw.WriteLine("    = {0:f0} N", hor_force);
            sw.WriteLine();

            sw.WriteLine("Thrust in each Lacing Plate = T ");
            sw.WriteLine();
            sw.WriteLine(" 2 * T * cos ({0}/2) = {1:f0}", lac_ang, hor_force);
            double T = (hor_force / ((2 * Math.Cos((lac_ang / 2) * rad))));
            sw.WriteLine();
            sw.WriteLine(" T = {0:f0} / (2 * cos ({1}/2) = {2:f3} N", hor_force, lac_ang, T);
            sw.WriteLine();

            double pl_wd, pl_thk, pl_area;
            //pl_wd = 50.0;
            //pl_thk = 12.0;
            //pl_area = pl_wd * pl_thk;
            //sw.WriteLine("Try, {0} mm * {1} mm Plates,", pl_wd, pl_thk);
            //sw.WriteLine("Area of the Plate = a = {0} * {1} = {2} Sq.mm.", pl_wd, pl_thk, pl_area);

            //double I = pl_wd * pl_thk * pl_thk * pl_thk / 12.0;
            //sw.WriteLine("Moment of Inertia = I = {0} x {1}^3 / 12 = {2:f3} Sq.Sq.mm", pl_wd, pl_thk, I);
            //sw.WriteLine();

            //sw.WriteLine("Length of between successive connections = l1 = {0:f3} mm", l1);
            //double r = Math.Sqrt(I / pl_area);
            //sw.WriteLine("Radius of gyration = r = √(I / a) =  √ ({0:f3} / {1}) = {2:f3} mm.", I, pl_area, r);
            sw.WriteLine();
            double lamda2 = l1 / ryy;
            sw.WriteLine("Slenderness ratio = λ2 = l1/r = {0:f3} / {1:f3} = {2:f3}", l1, ryy, lamda2);
            sw.WriteLine();
            double sigma_ac = Get_Table_1_Sigma_Value(lamda2, fy);
            sw.WriteLine("From Table 4,   σ_c = {0}  N / Sq. mm.", sigma_ac);
            sw.WriteLine();
            double app_com = T / a;

            sw.WriteLine("Applied Compressive stress = T / a");
            sw.WriteLine("                           = {0:f3} / {1}", T, a);
            if (app_com <= sigma_ac)
            {
                sw.WriteLine("                           = {0:f3} N/Sq.mm. < {1:f3}(σ_c),    So, OK", app_com, sigma_ac);
            }
            else
            {
                sw.WriteLine("                           = {0:f3} N/Sq.mm. > {1:f3}(σ_c),    So, NOT OK", app_com, sigma_ac);
            }
            sw.WriteLine();
            double rss = 0.0, rvt_val = 0.0, safe_load = 0.0;
            //bool flag = false;
            double[] rvt_dia = new double[] { 16, 18, 20, 22, 24, 25, 26, 30, 32, 36, 38, 40 };
            foreach (var item in rvt_dia)
            {
                lac_d2 = item;
                rss = lac_fs * Math.PI * (1.5 + lac_d2) * (1.5 + lac_d2) / 4.0;
                rvt_val = lac_fb * (1.5 + lac_d2) * lac_tl;
                safe_load = (rss > rvt_val) ? rvt_val : rss;
                if (safe_load >= T)
                {
                    break;
                }
            }
            //while ();


            //double rss = lac_fs * Math.PI * (1.5 + lac_d2) * (1.5 + lac_d2) / 4.0;
            //double rvt_val = lac_fb * (1.5 + lac_d2) * lac_tl;
            //double safe_load = (rss > rvt_val) ? rvt_val : rss;
            double tot_len = T / (throat_size * weld_strength);
            if (rbtn_weld.Checked)
            {
                sw.WriteLine("Thrust  = T = {0:f3} N ", T);
                sw.WriteLine("Welding Strength = ws = {0:f3} N/sq.mm ", weld_strength);
                sw.WriteLine("Throat size = ts = {0:f3} mm ", throat_size);
                sw.WriteLine();

                sw.WriteLine("Total Length of Weld Required = T / (ts * ws)");
                sw.WriteLine("                              = {0:f3} / ({1:f3} * {2:f3}) ", T, throat_size, weld_strength);
                sw.WriteLine("                              = {0:f3} mm", tot_len);
            }
            else
            {

                sw.WriteLine("Using {0} mm. diameter rivets, ", lac_d2);
                sw.WriteLine();

                sw.WriteLine("Rivet value in single shear = (fs * π * (d2+1.5)^2) / 4 ");
                sw.WriteLine("                            = ({0} * π * ({1}+1.5)^2) / 4 ", lac_fs, lac_d2);
                sw.WriteLine("                            = {0:f3} N", rss);


                sw.WriteLine("Rivet value in bearing = fb * d2 * t");
                sw.WriteLine("                       = {0} * ({1}+1.5) * {2}", lac_fb, lac_d2, lac_tl);
                sw.WriteLine("                       = {0} N", rvt_val);
                sw.WriteLine();
                sw.WriteLine("Safe Load in each rivet is the minimum of the above two values");


                if (safe_load >= T)
                {
                    sw.WriteLine("  = {0:f3} N. > Load in the Lacing Plate = T = {1:f3} N   So, OK.", safe_load, T);
                    sw.WriteLine();
                }
                else
                {
                    sw.WriteLine("two values = {0:f3} N. < Load in the Lacing Plate = T = {1:f3} N   So, NOT OK, Larger Diameter Rivet is Required.", safe_load, T);
                    sw.WriteLine();
                }
            }
            sw.WriteLine();

            /**/
        }
        public void DesignConnection(StreamWriter sw, CMember mbr)
        {
            double conn_d, conn_nr, conn_bg, conn_tg, conn_fs, conn_fb, conn_ft;

            conn_d = MyList.StringToDouble(txt_conn_d.Text, 0.0);
            conn_nr = MyList.StringToDouble(txt_conn_nr.Text, 0.0);
            conn_bg = MyList.StringToDouble(txt_conn_bg.Text, 0.0);
            conn_tg = MyList.StringToDouble(txt_conn_tg.Text, 0.0);
            conn_fs = MyList.StringToDouble(txt_conn_fs.Text, 0.0);
            conn_fb = MyList.StringToDouble(txt_conn_fb.Text, 0.0);
            conn_ft = MyList.StringToDouble(txt_conn_ft.Text, 0.0);


            sw.WriteLine();
            sw.WriteLine("Design of Connection for Tensile force");
            sw.WriteLine("--------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("INPUT DATA");
            sw.WriteLine("-----------");
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Bolt/Rivet Diameter = d = {0} mm.", conn_d);
            sw.WriteLine("Minimum Number of rivets/bolts per row = nr = {0}", conn_nr);
            sw.WriteLine();
            sw.WriteLine("Width of Gusset Plate = bg = {0} mm", conn_bg);
            sw.WriteLine("Thickness of Gusset Plate = tg = {0} mm", conn_tg);
            sw.WriteLine("Shear strength = fs = {0} N/Sq.mm", conn_fs);
            sw.WriteLine("Bearing strength = fb = {0} N/Sq.mm", conn_fb);
            sw.WriteLine("Tearing strength = ft = {0} N/Sq.mm.", conn_ft);
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Design Calculation");
            sw.WriteLine("------------------");
            sw.WriteLine();
            sw.WriteLine();
            double rvt_strength_shr = (conn_fs * Math.PI * (conn_d + 1.5) * (conn_d + 1.5) / 4.0);

            sw.WriteLine("Rivet strength in single shear = (fs * π * d * d) / 4 ");
            sw.WriteLine("                               = ({0} * π * ({1}+1.5) * ({1}+1.5)) / 4 ", conn_fs, conn_d);
            sw.WriteLine("                               = {0:f3} N", rvt_strength_shr);
            rvt_strength_shr = rvt_strength_shr / 1000.0;
            sw.WriteLine("                               = {0:f3} kN.", rvt_strength_shr);
            sw.WriteLine();
            double rvt_strength_brn = conn_fb * (conn_d + 1.5) * conn_tg;

            sw.WriteLine("Rivet strength in bearing = fb * d * tg");
            sw.WriteLine("                          = {0} * ({1}+1.5) * {2}", conn_fb, conn_d, conn_tg);
            sw.WriteLine("                          = {0:f3} N.", rvt_strength_brn);
            rvt_strength_brn = rvt_strength_brn / 1000.0;
            sw.WriteLine("                          = {0:f3} kN.", rvt_strength_brn);
            sw.WriteLine();
            double safe_load = (rvt_strength_shr > rvt_strength_brn) ? rvt_strength_brn : rvt_strength_shr;

            sw.WriteLine("Safe Load in each rivet is the minimum of the above two values = {0:f3} kN.", safe_load);
            sw.WriteLine();
            int no_rvt = (int)(mbr.MaxTensionForce.Force / safe_load);
            no_rvt++;

            sw.WriteLine("Number of Rivets/Bolts required ");
            sw.WriteLine("     = Tensile Force / Safe Load in each Rivet/Bolt");
            sw.WriteLine("     = {0} / {1:f3}", mbr.MaxTensionForce, safe_load);
            sw.WriteLine("     = {0} nos.", no_rvt);
            sw.WriteLine();
            sw.WriteLine();

            int no_end_rvt = (int)((double)no_rvt / 2.0);
            no_end_rvt++;

            sw.WriteLine("Use Rivets/Bolts at both sides of both ends of the member = {0} / 2 = {1} = {2} nos.", no_rvt, (no_rvt / 2.0), no_end_rvt);
            sw.WriteLine();
            sw.WriteLine();

            sw.WriteLine("Design of Gusset Plate for Tensile force");
            sw.WriteLine("----------------------------------------");
            sw.WriteLine();
            sw.WriteLine();
            double net_width = conn_bg - conn_nr * (conn_d + 1.5);

            sw.WriteLine("Net width of plate for effective section ");
            sw.WriteLine("    = bg – nr * (d+1.5) ");
            sw.WriteLine("    = {0} – {1} * ({2}+1.5)", conn_bg, conn_nr, conn_d);
            sw.WriteLine("    = {0:f3} mm", net_width);
            sw.WriteLine();

            sw.WriteLine("One Gusset plate is required at both sides and at both ends of each member");
            sw.WriteLine("So, 2 Gusset plates are to support the Tensile load = {0:f3} kN", mbr.MaxTensionForce);
            sw.WriteLine();

            double safe_tens_load = 2 * 150 * conn_bg * 12.0;
            sw.WriteLine("Safe Tensile Load carrying capacity ");
            sw.WriteLine("  = Tensile strength * Net area of effective section of 2 Gusset Plates");
            sw.WriteLine("  = 2 * 150 * bg * 12");
            //sw.WriteLine("  = 4 * 150 * {0} * 12", conn_bg);
            sw.WriteLine();

            double bg = (mbr.MaxTensionForce.Force * 1000.0 / (2d * 150.0 * 12.0));
            sw.WriteLine("Net width of each plate required = bg");
            sw.WriteLine("                                 = ({0:f3} * 1000) / (2 x 150 x 12)", mbr.MaxTensionForce);
            sw.WriteLine("                                 = {0:f3} mm.", bg);
            sw.WriteLine();
            double grs_wd = bg + conn_nr * (conn_d + 1.5);
            sw.WriteLine("Gross width of plate for effective section ");
            sw.WriteLine("        = bg + nr * (d+1.5)");
            sw.WriteLine("        = {0:f3} + {1} * ({2}+1.5)", bg, conn_nr, conn_d);
            sw.WriteLine("        = {0:f3} mm", grs_wd);
            grs_wd = (int)(grs_wd / 10.0);
            grs_wd = (grs_wd + 1) * 10;
            sw.WriteLine("        = {0:f0} mm", grs_wd);
            sw.WriteLine();

            sw.WriteLine("Use Gusset Plates of size {0}mm x 12mm at both sides of both ends of the member.", grs_wd);
            sw.WriteLine();

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
            //Write_All_Data();

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
                BridgeAnalysisDesign.frm_Open_Project frm = new BridgeAnalysisDesign.frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
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



                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);

                    //Write_All_Data();

                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                isCreateData = true;
                Create_Project();
            }
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
            string file_name = Path.Combine(user_path, "INPUT_DATA.TXT");

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
            }
            catch (Exception ex) { }
            Show_Total_Weight();
            FillResultGridWithColor();
            Set_Force_Input_Color();
        }


        public void Write_All_Data()
        {

            iApp.Save_Form_Record(this, user_path); return;

        }
        public void Read_All_Data()
        {
            iApp.Read_Form_Record(this, user_path); return;
        }
        #endregion Chiranjit [2016 09 07]


        private void cmb_lac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_lac.SelectedIndex == 0)
            {
                lbl_lac_wd.Text = "Width [bl]";
                lbl_lac_thk.Text = "Thickness [tl]";
                lbl_ISA.Visible = false;
                cmb_lac_tl.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_lac_bl.DropDownStyle = ComboBoxStyle.DropDown;
                cmb_lac_tl.Items.Clear();
                cmb_lac_bl.Items.Clear();
                cmb_lac_tl.Text = "16";
                cmb_lac_bl.Text = "60";
            }
            else if (cmb_lac.SelectedIndex == 1)
            {

                cmb_lac_tl.Items.Clear();
                cmb_lac_bl.Items.Clear();
                Fill_Angles_in_Combobox(cmb_lac_bl);
                lbl_lac_wd.Text = "Select Angle";
                lbl_lac_thk.Text = "Thickness [tl]";
                lbl_ISA.Visible = true;
                cmb_lac_tl.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb_lac_bl.DropDownStyle = ComboBoxStyle.DropDownList;
                if (cmb_lac_bl.Items.Count > 0) cmb_lac_bl.SelectedIndex = 0;
            }
        }
        void Fill_Angles_in_Combobox(ComboBox cmb)
        {
            string sec_code, sec_name;
            for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
            {
                sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                if (sec_name == "ISA" && sec_name != "")
                {
                    if (cmb.Items.Contains(sec_code) == false)
                    {
                        cmb.Items.Add(sec_code);
                    }
                }
            }
        }

        private void cmb_lac_bl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sec_code, sec_name, code2;
            double thk = 0.0;
            cmb_lac_tl.Items.Clear();
            for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
            {
                sec_code = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                sec_name = tbl_rolledSteelAngles.List_Table[i].SectionName;
                thk = tbl_rolledSteelAngles.List_Table[i].Thickness;
                if (sec_name == "ISA" && sec_code == cmb_lac_bl.Text)
                {
                    if (cmb_lac_tl.Items.Contains(thk) == false)
                    {
                        cmb_lac_tl.Items.Add(thk);
                    }
                }
            }
            if (cmb_lac_tl.Items.Count > 0)
            {
                cmb_lac_tl.SelectedIndex = 0;
            }
        }
        private void cmb_Shr_Con_Section_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_Shr_Con_Section_Code.Items.Clear();

            cmb_Shr_Con_Section_Code.Items.AddRange(tbl_rolledSteelChannels.Get_SectionCodes(cmb_Shr_Con_Section_name.Text).ToArray());
            if (cmb_Shr_Con_Section_Code.Items.Count > 0)
                cmb_Shr_Con_Section_Code.SelectedIndex = 0;
        }
        private void rbtn_bolt_CheckedChanged(object sender, EventArgs e)
        {
            grb_bolt.Enabled = rbtn_bolt.Checked;
            grb_weld.Enabled = rbtn_weld.Checked;
        }
        private void txt_weld_size_TextChanged(object sender, EventArgs e)
        {
            lbl_throat_size.Text = "= 0.7 * " + txt_weld_size.Text;
            txt_throat_size.Text = (0.7 * MyList.StringToDouble(txt_weld_size.Text, 0.0)).ToString("0.00");

        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            string drwg_path = "TRANSMISSION_TOWER1";
            //if (IsWarren2)
            //    drwg_path = "SteelTruss_Warren2";

            //if (Directory.Exists(drwg_path))
            //{

            if (cmb_no_levels.SelectedIndex == 0) drwg_path = "TRANSMISSION_TOWER1";
            if (cmb_no_levels.SelectedIndex == 1) drwg_path = "TRANSMISSION_TOWER2";
            if (cmb_no_levels.SelectedIndex == 2) drwg_path = "TRANSMISSION_TOWER3";

            //iApp.RunViewer(Path.Combine(Drawing_Folder, "Steel Truss Drawings"), drwg_path);
            iApp.RunViewer(Drawing_Folder, drwg_path);
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

        private void btn_open_analysis_input_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_open_analysis_input)
            {
                if (File.Exists(INPUT_FILE))
                    System.Diagnostics.Process.Start(INPUT_FILE);

            }
            else if (btn == btn_open_analysis_report)
            {
                string ana_rep = MyList.Get_Analysis_Report_File(INPUT_FILE);

                if (File.Exists(ana_rep))
                    System.Diagnostics.Process.Start(ana_rep);
            }
            else if (btn == btn_open_design_rep)
            {

                string des_rep = Path.Combine(user_path, "DESIGN_REP.TXT");
                if (File.Exists(des_rep))
                    System.Diagnostics.Process.Start(des_rep);
            }
        }

        private void txt_cd_force_percent_TextChanged(object sender, EventArgs e)
        {
            Show_Total_Weight();
        }

    }
   
    
    public class TowerStructure
    {
        #region Properties

        public double CWN_HGT = 4.95;
        public double CWN_BOT_WD = 2.4;

        public double BASE_WD = 11.844;

        public int Wing_Nos = 3;


        public double LEV1_HGT = 13.31;
        public double LEV1_WING_HGT = 1.55;
        public double LEV1_WING_BOT_WD = 4.37;
        public double LEV1_WING_TOP_WD = 4.23;
        public double LEV1_WING_SPAN = 4.531;



        public double LEV2_HGT = 4.0;
        public double LEV2_WING_HGT = 1.55;
        public double LEV2_WING_BOT_WD = 3.53;
        public double LEV2_WING_TOP_WD = 3.024;
        public double LEV2_WING_SPAN = 3.98;



        public double LEV3_HGT = 3.0;
        public double LEV3_WING_HGT = 1.55;
        public double LEV3_WING_BOT_WD = 2.55;
        public double LEV3_WING_TOP_WD = 2.4;
        public double LEV3_WING_SPAN = 3.784;


        #endregion Properties
        public TowerStructure()
        {

        }
        public void CreateStructures(vdDocument doc)
        {

            double hgt = LEV1_HGT;
            double wd = BASE_WD;



            List<vdLine> lines = new List<vdLine>();

            List<gPoint> gpc = new List<gPoint>();




            gPoint gp = new gPoint();

            #region Base Node

            gp.x = 0.0;
            gp.y = 0.0;
            gp.z = 0.0;
            gpc.Add(gp);

            gp = new gPoint();
            gp.x = wd;
            gp.y = 0.0;
            gp.z = 0.0;
            gpc.Add(gp);

            gp = new gPoint();
            gp.x = wd;
            gp.y = 0.0;
            gp.z = wd;
            gpc.Add(gp);

            gp = new gPoint();
            gp.x = 0.0;
            gp.y = 0.0;
            gp.z = wd;
            gpc.Add(gp);

            #endregion Base Node


            List<gPoint> Lev1_BOT_Pts = new List<gPoint>();

            #region Level 1
            hgt = LEV1_HGT;
            wd = (BASE_WD - LEV1_WING_BOT_WD) / 2.0;



            gp = new gPoint();
            gp.x = wd;
            gp.y = hgt;
            gp.z = wd;
            Lev1_BOT_Pts.Add(gp);

            gp = new gPoint();
            gp.x = BASE_WD - wd;
            gp.y = hgt;
            gp.z = wd;
            Lev1_BOT_Pts.Add(gp);

            gp = new gPoint();
            gp.x = BASE_WD - wd;
            gp.y = hgt;
            gp.z = BASE_WD - wd;
            Lev1_BOT_Pts.Add(gp);

            gp = new gPoint();
            gp.x = wd;
            gp.y = hgt;
            gp.z = BASE_WD - wd;
            Lev1_BOT_Pts.Add(gp);

            #endregion Level 1

            List<gPoint> Lev1_top_Pts = new List<gPoint>();

            #region Level 1
            hgt = LEV1_HGT + LEV1_WING_HGT;
            wd = (BASE_WD - LEV1_WING_TOP_WD) / 2.0;



            gp = new gPoint();
            gp.x = wd;
            gp.y = hgt;
            gp.z = wd;
            Lev1_top_Pts.Add(gp);

            gp = new gPoint();
            gp.x = BASE_WD - wd;
            gp.y = hgt;
            gp.z = wd;
            Lev1_top_Pts.Add(gp);

            gp = new gPoint();
            gp.x = BASE_WD - wd;
            gp.y = hgt;
            gp.z = BASE_WD - wd;
            Lev1_top_Pts.Add(gp);

            gp = new gPoint();
            gp.x = wd;
            gp.y = hgt;
            gp.z = BASE_WD - wd;
            Lev1_top_Pts.Add(gp);

            #endregion Level 1


            //vdDocument doc = uC_CAD1.VDoc;



            vdEntities ents = doc.ActiveLayOut.Entities;


            ents.RemoveAll();

            vdLine ln = new vdLine();


            List<gPoint> Lev1_Mid_Pts = new List<gPoint>();

            int i = 0;

            #region Level 1


            double len =0.0;

            #region Level 1
            for (i = 0; i < gpc.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = gpc[i];
                ln.EndPoint = Lev1_BOT_Pts[i];
                ents.AddItem(ln);

                len = ln.StartPoint.Distance3D(ln.EndPoint);

                Lev1_Mid_Pts.Add(ln.getPointAtDist(len / 2));

            }
            #region Level 1 Bottom Horizontal


            List<vdLine> side1 = new List<vdLine>();
            #region Mid Line 1

            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[0];
            ln.EndPoint = Lev1_Mid_Pts[1];
            ents.AddItem(ln);
            side1.Add(ln);

            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[1];
            ln.EndPoint = Lev1_BOT_Pts[0];
            ents.AddItem(ln);
            side1.Add(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[1];
            ln.EndPoint = Lev1_Mid_Pts[0];
            ents.AddItem(ln);
            side1.Add(ln);



            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[0];
            ln.EndPoint = Lev1_BOT_Pts[1];
            ents.AddItem(ln);
            side1.Add(ln);



            #endregion Mid Line 1


            List<vdLine> side2 = new List<vdLine>();

            #region Mid Line 2

            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[1];
            ln.EndPoint = Lev1_Mid_Pts[2];
            ents.AddItem(ln);
            side2.Add(ln);

            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[2];
            ln.EndPoint = Lev1_BOT_Pts[1];
            ents.AddItem(ln);
            side2.Add(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[2];
            ln.EndPoint = Lev1_Mid_Pts[1];
            ents.AddItem(ln);
            side2.Add(ln);



            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[1];
            ln.EndPoint = Lev1_BOT_Pts[2];
            ents.AddItem(ln);
            side2.Add(ln);



            #endregion Mid Line 2



            List<vdLine> side3 = new List<vdLine>();

            #region Mid Line 3

            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[2];
            ln.EndPoint = Lev1_Mid_Pts[3];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[3];
            ln.EndPoint = Lev1_BOT_Pts[2];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[3];
            ln.EndPoint = Lev1_Mid_Pts[2];
            ents.AddItem(ln);



            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[2];
            ln.EndPoint = Lev1_BOT_Pts[3];
            ents.AddItem(ln);



            #endregion Mid Line 2



            //ln.IntersectWith(ln, VectorDraw.Professional.Constants.VdConstInters.VdIntOnBothOperands, )
            #region Mid Line 4

            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[3];
            ln.EndPoint = Lev1_Mid_Pts[0];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[0];
            ln.EndPoint = Lev1_BOT_Pts[3];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = gpc[0];
            ln.EndPoint = Lev1_Mid_Pts[3];
            ents.AddItem(ln);



            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_Mid_Pts[3];
            ln.EndPoint = Lev1_BOT_Pts[0];
            ents.AddItem(ln);



            #endregion Mid Line 4



            for (i = 1; i < Lev1_BOT_Pts.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev1_BOT_Pts[i - 1];
                ln.EndPoint = Lev1_BOT_Pts[i];
                ents.AddItem(ln);

                if (i == Lev1_BOT_Pts.Count - 1)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev1_BOT_Pts[0];
                    ln.EndPoint = Lev1_BOT_Pts[i];
                    ents.AddItem(ln);
                }
            }
            #endregion Level 1 Bottom Horizontal

            #endregion Level 1


            #region Level 1 Top

            for (i = 0; i < Lev1_BOT_Pts.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev1_BOT_Pts[i];
                ln.EndPoint = Lev1_top_Pts[i];
                ents.AddItem(ln);
            }

            #region Level 1 Bottom Horizontal

            for (i = 1; i < Lev1_top_Pts.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev1_top_Pts[i - 1];
                ln.EndPoint = Lev1_top_Pts[i];
                ents.AddItem(ln);

                if (i == Lev1_top_Pts.Count - 1)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev1_top_Pts[0];
                    ln.EndPoint = Lev1_top_Pts[i];
                    ents.AddItem(ln);
                }
            }
            #endregion Level 1 Bottom Horizontal

            #endregion Level 1  Top


            #region Level 1 Wing

            List<gPoint> Lev1_Wing_Pts = new List<gPoint>();

            gp = new gPoint();

            gp.x = Lev1_BOT_Pts[0].x - LEV1_WING_SPAN;
            gp.y = Lev1_BOT_Pts[0].y;
            gp.z = BASE_WD / 2.0;

            Lev1_Wing_Pts.Add(gp);


            gp = new gPoint();

            gp.x = Lev1_BOT_Pts[1].x + LEV1_WING_SPAN;
            gp.y = Lev1_BOT_Pts[1].y;
            gp.z = BASE_WD / 2.0;

            Lev1_Wing_Pts.Add(gp);



            #region Level 1 Left Wing
            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_BOT_Pts[0];
            ln.EndPoint = Lev1_Wing_Pts[0];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_top_Pts[0];
            ln.EndPoint = Lev1_Wing_Pts[0];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_BOT_Pts[3];
            ln.EndPoint = Lev1_Wing_Pts[0];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_top_Pts[3];
            ln.EndPoint = Lev1_Wing_Pts[0];
            ents.AddItem(ln);
            #endregion Level 1 Left Wing

            #region Level 1 Right Wing
            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_BOT_Pts[1];
            ln.EndPoint = Lev1_Wing_Pts[1];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_top_Pts[1];
            ln.EndPoint = Lev1_Wing_Pts[1];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_BOT_Pts[2];
            ln.EndPoint = Lev1_Wing_Pts[1];
            ents.AddItem(ln);


            ln = new vdLine(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = Lev1_top_Pts[2];
            ln.EndPoint = Lev1_Wing_Pts[1];
            ents.AddItem(ln);
            #endregion Level 1 Right Wing



            #endregion Level 1 Wing

            #endregion Level 1

            List<gPoint> Lev2_Bot_Pts = new List<gPoint>();
            List<gPoint> Lev2_Top_Pts = new List<gPoint>();

            if (Wing_Nos > 1)
            {
                #region Level 2



                #region Level 2 Bottom Points
                hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT - LEV2_WING_HGT;
                wd = (BASE_WD - LEV2_WING_BOT_WD) / 2.0;


                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = wd;
                Lev2_Bot_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = wd;
                Lev2_Bot_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev2_Bot_Pts.Add(gp);

                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev2_Bot_Pts.Add(gp);

                #endregion Level 2 Bottom Points



                #region Level 2 Top Points
                hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT;
                wd = (BASE_WD - LEV2_WING_TOP_WD) / 2.0;


                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = wd;
                Lev2_Top_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = wd;
                Lev2_Top_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev2_Top_Pts.Add(gp);

                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev2_Top_Pts.Add(gp);

                #endregion Level 2 Top Points


                #region Level 2
                for (i = 0; i < Lev2_Bot_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev1_top_Pts[i];
                    ln.EndPoint = Lev2_Bot_Pts[i];
                    ents.AddItem(ln);

                }
                #region Level 2 Bottom Horizontal

                for (i = 1; i < Lev2_Bot_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev2_Bot_Pts[i - 1];
                    ln.EndPoint = Lev2_Bot_Pts[i];
                    ents.AddItem(ln);

                    if (i == Lev2_Bot_Pts.Count - 1)
                    {
                        ln = new vdLine(doc);
                        ln.setDocumentDefaults();
                        ln.StartPoint = Lev2_Bot_Pts[0];
                        ln.EndPoint = Lev2_Bot_Pts[i];
                        ents.AddItem(ln);
                    }
                }
                #endregion Level 2 Bottom Horizontal

                #endregion Level 2



                #region Level 2
                for (i = 0; i < Lev2_Top_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev2_Bot_Pts[i];
                    ln.EndPoint = Lev2_Top_Pts[i];
                    ents.AddItem(ln);

                }
                #region Level 2 Bottom Horizontal

                for (i = 1; i < Lev2_Top_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev2_Top_Pts[i - 1];
                    ln.EndPoint = Lev2_Top_Pts[i];
                    ents.AddItem(ln);

                    if (i == Lev2_Top_Pts.Count - 1)
                    {
                        ln = new vdLine(doc);
                        ln.setDocumentDefaults();
                        ln.StartPoint = Lev2_Top_Pts[0];
                        ln.EndPoint = Lev2_Top_Pts[i];
                        ents.AddItem(ln);
                    }
                }
                #endregion Level 2 Bottom Horizontal

                #endregion Level 2



                #region Level 2 Wing

                List<gPoint> Lev2_Wing_Pts = new List<gPoint>();

                gp = new gPoint();

                gp.x = Lev2_Bot_Pts[0].x - LEV2_WING_SPAN;
                gp.y = Lev2_Bot_Pts[0].y;
                gp.z = BASE_WD / 2.0;

                Lev2_Wing_Pts.Add(gp);


                gp = new gPoint();

                gp.x = Lev2_Bot_Pts[1].x + LEV2_WING_SPAN;
                gp.y = Lev2_Bot_Pts[1].y;
                gp.z = BASE_WD / 2.0;

                Lev2_Wing_Pts.Add(gp);



                #region Level 2 Left Wing
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Bot_Pts[0];
                ln.EndPoint = Lev2_Wing_Pts[0];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Top_Pts[0];
                ln.EndPoint = Lev2_Wing_Pts[0];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Bot_Pts[3];
                ln.EndPoint = Lev2_Wing_Pts[0];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Top_Pts[3];
                ln.EndPoint = Lev2_Wing_Pts[0];
                ents.AddItem(ln);
                #endregion Level 2 Left Wing

                #region Level 2 Right Wing
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Bot_Pts[1];
                ln.EndPoint = Lev2_Wing_Pts[1];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Top_Pts[1];
                ln.EndPoint = Lev2_Wing_Pts[1];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Bot_Pts[2];
                ln.EndPoint = Lev2_Wing_Pts[1];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev2_Top_Pts[2];
                ln.EndPoint = Lev2_Wing_Pts[1];
                ents.AddItem(ln);
                #endregion Level 2 Right Wing



                #endregion Level 2 Wing

                #endregion Level 2
            }


            List<gPoint> Lev3_Bot_Pts = new List<gPoint>();
            List<gPoint> Lev3_Top_Pts = new List<gPoint>();

            if (Wing_Nos > 2)
            {
                #region Level 3

                #region Level 3 Bottom Points
                hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT + LEV3_HGT - LEV3_WING_HGT;
                wd = (BASE_WD - LEV3_WING_BOT_WD) / 2.0;


                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = wd;
                Lev3_Bot_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = wd;
                Lev3_Bot_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev3_Bot_Pts.Add(gp);

                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev3_Bot_Pts.Add(gp);

                #endregion Level 3 Bottom Points



                #region Level 3 Top Points
                hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT + LEV3_HGT;
                wd = (BASE_WD - LEV3_WING_TOP_WD) / 2.0;


                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = wd;
                Lev3_Top_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = wd;
                Lev3_Top_Pts.Add(gp);

                gp = new gPoint();
                gp.x = BASE_WD - wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev3_Top_Pts.Add(gp);

                gp = new gPoint();
                gp.x = wd;
                gp.y = hgt;
                gp.z = BASE_WD - wd;
                Lev3_Top_Pts.Add(gp);

                #endregion Level 3 Top Points


                #region Level 3
                for (i = 0; i < Lev3_Bot_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev2_Top_Pts[i];
                    ln.EndPoint = Lev3_Bot_Pts[i];
                    ents.AddItem(ln);

                }
                #region Level 2 Bottom Horizontal

                for (i = 1; i < Lev3_Bot_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev3_Bot_Pts[i - 1];
                    ln.EndPoint = Lev3_Bot_Pts[i];
                    ents.AddItem(ln);

                    if (i == Lev3_Bot_Pts.Count - 1)
                    {
                        ln = new vdLine(doc);
                        ln.setDocumentDefaults();
                        ln.StartPoint = Lev3_Bot_Pts[0];
                        ln.EndPoint = Lev3_Bot_Pts[i];
                        ents.AddItem(ln);
                    }
                }
                #endregion Level 3 Bottom Horizontal

                #endregion Level 2

                #region Level 3
                for (i = 0; i < Lev3_Top_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev3_Bot_Pts[i];
                    ln.EndPoint = Lev3_Top_Pts[i];
                    ents.AddItem(ln);

                }
                #region Level 3 Bottom Horizontal

                for (i = 1; i < Lev3_Top_Pts.Count; i++)
                {
                    ln = new vdLine(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = Lev3_Top_Pts[i - 1];
                    ln.EndPoint = Lev3_Top_Pts[i];
                    ents.AddItem(ln);

                    if (i == Lev3_Top_Pts.Count - 1)
                    {
                        ln = new vdLine(doc);
                        ln.setDocumentDefaults();
                        ln.StartPoint = Lev3_Top_Pts[0];
                        ln.EndPoint = Lev3_Top_Pts[i];
                        ents.AddItem(ln);
                    }
                }
                #endregion Level 2 Bottom Horizontal

                #endregion Level 3

                #region Level 3 Wing

                List<gPoint> Lev3_Wing_Pts = new List<gPoint>();

                gp = new gPoint();

                gp.x = Lev3_Bot_Pts[0].x - LEV3_WING_SPAN;
                gp.y = Lev3_Bot_Pts[0].y;
                gp.z = BASE_WD / 2.0;

                Lev3_Wing_Pts.Add(gp);


                gp = new gPoint();

                gp.x = Lev3_Bot_Pts[1].x + LEV3_WING_SPAN;
                gp.y = Lev3_Bot_Pts[1].y;
                gp.z = BASE_WD / 2.0;

                Lev3_Wing_Pts.Add(gp);



                #region Level 3 Left Wing
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Bot_Pts[0];
                ln.EndPoint = Lev3_Wing_Pts[0];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Top_Pts[0];
                ln.EndPoint = Lev3_Wing_Pts[0];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Bot_Pts[3];
                ln.EndPoint = Lev3_Wing_Pts[0];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Top_Pts[3];
                ln.EndPoint = Lev3_Wing_Pts[0];
                ents.AddItem(ln);
                #endregion Level 3 Left Wing

                #region Level 3 Right Wing
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Bot_Pts[1];
                ln.EndPoint = Lev3_Wing_Pts[1];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Top_Pts[1];
                ln.EndPoint = Lev3_Wing_Pts[1];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Bot_Pts[2];
                ln.EndPoint = Lev3_Wing_Pts[1];
                ents.AddItem(ln);


                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Top_Pts[2];
                ln.EndPoint = Lev3_Wing_Pts[1];
                ents.AddItem(ln);
                #endregion Level 3 Right Wing



                #endregion Level 2 Wing

                #endregion Level 3
            }



            #region Crown

            gPoint CWN_Top = new gPoint();
            if (Wing_Nos == 3)
            {
                hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT + LEV3_HGT + CWN_HGT;
            }
            else if (Wing_Nos == 2)
            {
                hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT + CWN_HGT;

                Lev3_Top_Pts = Lev2_Top_Pts;

            }
            else if (Wing_Nos == 1)
            {
                hgt = LEV1_HGT + LEV1_WING_HGT + CWN_HGT;
                Lev3_Top_Pts = Lev1_top_Pts;
            }
            gp = new gPoint();
            gp.x = BASE_WD / 2;
            gp.y = hgt;
            gp.z = BASE_WD / 2;




            for (i = 0; i < Lev3_Top_Pts.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Top_Pts[i];
                ln.EndPoint = gp;
                ents.AddItem(ln);
            }

            #endregion Crown


            doc.Redraw(true);
        }


        public List<string> Get_Tower_Crown_Data()
        {
            List<string> list = new List<string>();

            #region DATA
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         4.7220    21.8600     4.7220"));
            list.Add(string.Format("2         7.1220    21.8600     4.7220"));
            list.Add(string.Format("3         4.7220    21.8600     7.1220"));
            list.Add(string.Format("4         7.1220    21.8600     7.1220"));
            list.Add(string.Format("5         4.9540    22.7730     4.9540"));
            list.Add(string.Format("6         6.8900    22.7730     4.9540"));
            list.Add(string.Format("7         4.9540    22.7730     6.8900"));
            list.Add(string.Format("8         6.8900    22.7730     6.8900"));
            list.Add(string.Format("9         5.1860    23.6850     5.1860"));
            list.Add(string.Format("10        6.6580    23.6850     5.1860"));
            list.Add(string.Format("11        5.1860    23.6850     6.6580"));
            list.Add(string.Format("12        6.6580    23.6850     6.6580"));
            list.Add(string.Format("13        5.3280    24.2930     5.3280"));
            list.Add(string.Format("14        6.5160    24.2930     5.3280"));
            list.Add(string.Format("15        5.3280    24.2930     6.5160"));
            list.Add(string.Format("16        6.5160    24.2930     6.5160"));
            list.Add(string.Format("17        5.4700    24.9000     5.4700"));
            list.Add(string.Format("18        6.3740    24.9000     5.4700"));
            list.Add(string.Format("19        5.4700    24.9000     6.3740"));
            list.Add(string.Format("20        6.3740    24.9000     6.3740"));
            list.Add(string.Format("21        5.5800    25.3560     5.5800"));
            list.Add(string.Format("22        6.2640    25.3560     5.5800"));
            list.Add(string.Format("23        5.5800    25.3560     6.2640"));
            list.Add(string.Format("24        6.2640    25.3560     6.2640"));
            list.Add(string.Format("25        5.6900    25.8120     5.6900"));
            list.Add(string.Format("26        6.1540    25.8120     5.6900"));
            list.Add(string.Format("27        5.6900    25.8120     6.1540"));
            list.Add(string.Format("28        6.1540    25.8120     6.1540"));
            list.Add(string.Format("29        5.9220    26.8100     5.9220"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1              1          2"));
            list.Add(string.Format("2              3          4"));
            list.Add(string.Format("3              1          3"));
            list.Add(string.Format("4              2          4"));
            list.Add(string.Format("5              1          5"));
            list.Add(string.Format("6              1          7"));
            list.Add(string.Format("7              2          5"));
            list.Add(string.Format("8              2          6"));
            list.Add(string.Format("9              3          7"));
            list.Add(string.Format("10             3          8"));
            list.Add(string.Format("11             4          6"));
            list.Add(string.Format("12             4          8"));
            list.Add(string.Format("13             5          9"));
            list.Add(string.Format("14             5         10"));
            list.Add(string.Format("15             6         10"));
            list.Add(string.Format("16             6         12"));
            list.Add(string.Format("17             7          9"));
            list.Add(string.Format("18             7         11"));
            list.Add(string.Format("19             8         11"));
            list.Add(string.Format("20             8         12"));
            list.Add(string.Format("21             9         13"));
            list.Add(string.Format("22             9         15"));
            list.Add(string.Format("23            10         13"));
            list.Add(string.Format("24            10         14"));
            list.Add(string.Format("25            11         15"));
            list.Add(string.Format("26            11         16"));
            list.Add(string.Format("27            12         14"));
            list.Add(string.Format("28            12         16"));
            list.Add(string.Format("29            13         17"));
            list.Add(string.Format("30            13         18"));
            list.Add(string.Format("31            14         18"));
            list.Add(string.Format("32            14         20"));
            list.Add(string.Format("33            15         17"));
            list.Add(string.Format("34            15         19"));
            list.Add(string.Format("35            16         19"));
            list.Add(string.Format("36            16         20"));
            list.Add(string.Format("37            17         21"));
            list.Add(string.Format("38            17         23"));
            list.Add(string.Format("39            18         21"));
            list.Add(string.Format("40            18         22"));
            list.Add(string.Format("41            19         23"));
            list.Add(string.Format("42            19         24"));
            list.Add(string.Format("43            20         22"));
            list.Add(string.Format("44            20         24"));
            list.Add(string.Format("45            21         25"));
            list.Add(string.Format("46            21         26"));
            list.Add(string.Format("47            22         26"));
            list.Add(string.Format("48            22         28"));
            list.Add(string.Format("49            23         25"));
            list.Add(string.Format("50            23         27"));
            list.Add(string.Format("51            24         27"));
            list.Add(string.Format("52            24         28"));
            list.Add(string.Format("53            25         29"));
            list.Add(string.Format("54            26         29"));
            list.Add(string.Format("55            27         29"));
            list.Add(string.Format("56            28         29"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion
            return list;
        }


        public List<string> Get_Tower_Level1_Data()
        {
            List<string> list = new List<string>();


            #region DATA
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         0.0000     0.0000     0.0000"));
            list.Add(string.Format("2        11.8440     0.0000     0.0000"));
            list.Add(string.Format("3         0.0000     0.0000    11.8440"));
            list.Add(string.Format("4        11.8440     0.0000    11.8440"));
            list.Add(string.Format("5         1.4810     0.9880     0.2770"));
            list.Add(string.Format("6        10.3640     0.9880     0.2770"));
            list.Add(string.Format("7         0.2770     0.9880     1.4810"));
            list.Add(string.Format("8        11.5670     0.9880     1.4810"));
            list.Add(string.Format("9         0.2770     0.9880    10.3640"));
            list.Add(string.Format("10       11.5670     0.9880    10.3640"));
            list.Add(string.Format("11        1.4810     0.9880    11.5670"));
            list.Add(string.Format("12       10.3640     0.9880    11.5670"));
            list.Add(string.Format("13        0.4670     1.6640     0.4670"));
            list.Add(string.Format("14       11.3770     1.6640     0.4670"));
            list.Add(string.Format("15        0.4670     1.6640    11.3770"));
            list.Add(string.Format("16       11.3770     1.6640    11.3770"));
            list.Add(string.Format("17        2.9610     1.9750     0.5540"));
            list.Add(string.Format("18        8.8830     1.9750     0.5540"));
            list.Add(string.Format("19        0.5540     1.9750     2.9610"));
            list.Add(string.Format("20       11.2900     1.9750     2.9610"));
            list.Add(string.Format("21        0.5540     1.9750     8.8830"));
            list.Add(string.Format("22       11.2900     1.9750     8.8830"));
            list.Add(string.Format("23        2.9610     1.9750    11.2900"));
            list.Add(string.Format("24        8.8830     1.9750    11.2900"));
            list.Add(string.Format("25        1.9480     2.6510     0.7440"));
            list.Add(string.Format("26        9.8970     2.6510     0.7440"));
            list.Add(string.Format("27        0.7440     2.6510     1.9480"));
            list.Add(string.Format("28       11.1000     2.6510     1.9480"));
            list.Add(string.Format("29        0.7440     2.6510     9.8970"));
            list.Add(string.Format("30       11.1000     2.6510     9.8970"));
            list.Add(string.Format("31        1.9480     2.6510    11.1000"));
            list.Add(string.Format("32        9.8970     2.6510    11.1000"));
            list.Add(string.Format("33        0.9340     3.3280     0.9340"));
            list.Add(string.Format("34       10.9100     3.3280     0.9340"));
            list.Add(string.Format("35        0.9340     3.3280    10.9100"));
            list.Add(string.Format("36       10.9100     3.3280    10.9100"));
            list.Add(string.Format("37        5.9220     3.9510     1.1090"));
            list.Add(string.Format("38        1.1090     3.9510     5.9220"));
            list.Add(string.Format("39       10.7350     3.9510     5.9220"));
            list.Add(string.Format("40        5.9220     3.9510    10.7350"));
            list.Add(string.Format("41        2.4150     4.3150     1.2110"));
            list.Add(string.Format("42        9.4300     4.3150     1.2110"));
            list.Add(string.Format("43        1.2110     4.3150     2.4150"));
            list.Add(string.Format("44       10.6330     4.3150     2.4150"));
            list.Add(string.Format("45        1.2110     4.3150     9.4300"));
            list.Add(string.Format("46       10.6330     4.3150     9.4300"));
            list.Add(string.Format("47        2.4150     4.3150    10.6330"));
            list.Add(string.Format("48        9.4300     4.3150    10.6330"));
            list.Add(string.Format("49        1.4010     4.9910     1.4010"));
            list.Add(string.Format("50       10.4430     4.9910     1.4010"));
            list.Add(string.Format("51        1.4010     4.9910    10.4430"));
            list.Add(string.Format("52       10.4430     4.9910    10.4430"));
            list.Add(string.Format("53        3.8950     5.3030     1.4880"));
            list.Add(string.Format("54        7.9490     5.3030     1.4880"));
            list.Add(string.Format("55        1.4880     5.3030     3.8950"));
            list.Add(string.Format("56       10.3560     5.3030     3.8950"));
            list.Add(string.Format("57        1.4880     5.3030     7.9490"));
            list.Add(string.Format("58       10.3560     5.3030     7.9490"));
            list.Add(string.Format("59        3.8950     5.3030    10.3560"));
            list.Add(string.Format("60        7.9490     5.3030    10.3560"));
            list.Add(string.Format("61        2.8820     5.9790     1.6780"));
            list.Add(string.Format("62        8.9630     5.9790     1.6780"));
            list.Add(string.Format("63        1.6780     5.9790     2.8820"));
            list.Add(string.Format("64       10.1660     5.9790     2.8820"));
            list.Add(string.Format("65        1.6780     5.9790     8.9630"));
            list.Add(string.Format("66       10.1660     5.9790     8.9630"));
            list.Add(string.Format("67        2.8820     5.9790    10.1660"));
            list.Add(string.Format("68        8.9630     5.9790    10.1660"));
            list.Add(string.Format("69        1.8680     6.6550     1.8680"));
            list.Add(string.Format("70        9.9760     6.6550     1.8680"));
            list.Add(string.Format("71        1.8680     6.6550     9.9760"));
            list.Add(string.Format("72        9.9760     6.6550     9.9760"));
            list.Add(string.Format("73        2.2150     7.8900     2.2150"));
            list.Add(string.Format("74        9.6290     7.8900     2.2150"));
            list.Add(string.Format("75        2.2150     7.8900     9.6290"));
            list.Add(string.Format("76        9.6290     7.8900     9.6290"));
            list.Add(string.Format("77        3.2310     8.1090     2.2770"));
            list.Add(string.Format("78        8.6130     8.1090     2.2770"));
            list.Add(string.Format("79        2.2770     8.1090     3.2310"));
            list.Add(string.Format("80        9.5680     8.1090     3.2310"));
            list.Add(string.Format("81        2.2770     8.1090     8.6130"));
            list.Add(string.Format("82        9.5680     8.1090     8.6130"));
            list.Add(string.Format("83        3.2310     8.1090     9.5680"));
            list.Add(string.Format("84        8.6130     8.1090     9.5680"));
            list.Add(string.Format("85        2.6850     9.5620     2.6850"));
            list.Add(string.Format("86        4.5940     9.5620     2.6850"));
            list.Add(string.Format("87        7.2500     9.5620     2.6850"));
            list.Add(string.Format("88        9.1590     9.5620     2.6850"));
            list.Add(string.Format("89        2.6850     9.5620     4.5940"));
            list.Add(string.Format("90        9.1590     9.5620     4.5940"));
            list.Add(string.Format("91        2.6850     9.5620     7.2500"));
            list.Add(string.Format("92        9.1590     9.5620     7.2500"));
            list.Add(string.Format("93        2.6850     9.5620     9.1590"));
            list.Add(string.Format("94        4.5940     9.5620     9.1590"));
            list.Add(string.Format("95        7.2500     9.5620     9.1590"));
            list.Add(string.Format("96        9.1590     9.5620     9.1590"));
            list.Add(string.Format("97        3.0820    10.9790     3.0820"));
            list.Add(string.Format("98        4.5020    10.9790     3.0820"));
            list.Add(string.Format("99        5.9220    10.9790     3.0820"));
            list.Add(string.Format("100       7.3420    10.9790     3.0820"));
            list.Add(string.Format("101       8.7620    10.9790     3.0820"));
            list.Add(string.Format("102       3.0820    10.9790     4.5020"));
            list.Add(string.Format("103       8.7620    10.9790     4.5020"));
            list.Add(string.Format("104       3.0820    10.9790     5.9220"));
            list.Add(string.Format("105       8.7620    10.9790     5.9220"));
            list.Add(string.Format("106       3.0820    10.9790     7.3420"));
            list.Add(string.Format("107       8.7620    10.9790     7.3420"));
            list.Add(string.Format("108       3.0820    10.9790     8.7620"));
            list.Add(string.Format("109       4.5020    10.9790     8.7620"));
            list.Add(string.Format("110       5.9220    10.9790     8.7620"));
            list.Add(string.Format("111       7.3420    10.9790     8.7620"));
            list.Add(string.Format("112       8.7620    10.9790     8.7620"));
            list.Add(string.Format("113       3.4100    12.1450     3.4100"));
            list.Add(string.Format("114       4.8300    12.1450     3.4100"));
            list.Add(string.Format("115       7.0150    12.1450     3.4100"));
            list.Add(string.Format("116       8.4350    12.1450     3.4100"));
            list.Add(string.Format("117       3.4100    12.1450     4.8300"));
            list.Add(string.Format("118       8.4340    12.1450     4.8300"));
            list.Add(string.Format("119       3.4100    12.1450     7.0150"));
            list.Add(string.Format("120       8.4340    12.1450     7.0150"));
            list.Add(string.Format("121       4.8300    12.1450     8.4340"));
            list.Add(string.Format("122       7.0150    12.1450     8.4340"));
            list.Add(string.Format("123       3.4100    12.1450     8.4350"));
            list.Add(string.Format("124       8.4350    12.1450     8.4350"));
            list.Add(string.Format("125       3.7370    13.3100     3.7370"));
            list.Add(string.Format("126       5.9220    13.3100     3.7370"));
            list.Add(string.Format("127       8.1070    13.3100     3.7370"));
            list.Add(string.Format("128       3.7370    13.3100     5.9220"));
            list.Add(string.Format("129       8.1070    13.3100     5.9220"));
            list.Add(string.Format("130       3.7370    13.3100     8.1070"));
            list.Add(string.Format("131       5.9220    13.3100     8.1070"));
            list.Add(string.Format("132       8.1070    13.3100     8.1070"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1             85         86"));
            list.Add(string.Format("2             87         88"));
            list.Add(string.Format("3             93         94"));
            list.Add(string.Format("4             95         96"));
            list.Add(string.Format("5             97         98"));
            list.Add(string.Format("6             98         99"));
            list.Add(string.Format("7             99        100"));
            list.Add(string.Format("8            100        101"));
            list.Add(string.Format("9            108        109"));
            list.Add(string.Format("10           109        110"));
            list.Add(string.Format("11           110        111"));
            list.Add(string.Format("12           111        112"));
            list.Add(string.Format("13           113        114"));
            list.Add(string.Format("14           115        116"));
            list.Add(string.Format("15           125        126"));
            list.Add(string.Format("16           126        127"));
            list.Add(string.Format("17           130        131"));
            list.Add(string.Format("18           131        132"));
            list.Add(string.Format("19            85         89"));
            list.Add(string.Format("20            88         90"));
            list.Add(string.Format("21            91         93"));
            list.Add(string.Format("22            92         96"));
            list.Add(string.Format("23            97        102"));
            list.Add(string.Format("24           101        103"));
            list.Add(string.Format("25           102        104"));
            list.Add(string.Format("26           103        105"));
            list.Add(string.Format("27           104        106"));
            list.Add(string.Format("28           105        107"));
            list.Add(string.Format("29           106        108"));
            list.Add(string.Format("30           107        112"));
            list.Add(string.Format("31           113        117"));
            list.Add(string.Format("32           119        123"));
            list.Add(string.Format("33           125        128"));
            list.Add(string.Format("34           127        129"));
            list.Add(string.Format("35           128        130"));
            list.Add(string.Format("36           129        132"));
            list.Add(string.Format("37             1          5"));
            list.Add(string.Format("38             1          7"));
            list.Add(string.Format("39             1         13"));
            list.Add(string.Format("40             2          6"));
            list.Add(string.Format("41             2          8"));
            list.Add(string.Format("42             2         14"));
            list.Add(string.Format("43             3          9"));
            list.Add(string.Format("44             3         11"));
            list.Add(string.Format("45             3         15"));
            list.Add(string.Format("46             4         10"));
            list.Add(string.Format("47             4         12"));
            list.Add(string.Format("48             4         16"));
            list.Add(string.Format("49             5         13"));
            list.Add(string.Format("50             5         17"));
            list.Add(string.Format("51             5         25"));
            list.Add(string.Format("52             6         14"));
            list.Add(string.Format("53             6         18"));
            list.Add(string.Format("54             6         26"));
            list.Add(string.Format("55             7         13"));
            list.Add(string.Format("56             7         19"));
            list.Add(string.Format("57             7         27"));
            list.Add(string.Format("58             8         14"));
            list.Add(string.Format("59             8         20"));
            list.Add(string.Format("60             8         28"));
            list.Add(string.Format("61             9         15"));
            list.Add(string.Format("62             9         21"));
            list.Add(string.Format("63             9         29"));
            list.Add(string.Format("64            10         16"));
            list.Add(string.Format("65            10         22"));
            list.Add(string.Format("66            10         30"));
            list.Add(string.Format("67            11         15"));
            list.Add(string.Format("68            11         23"));
            list.Add(string.Format("69            11         31"));
            list.Add(string.Format("70            12         16"));
            list.Add(string.Format("71            12         24"));
            list.Add(string.Format("72            12         32"));
            list.Add(string.Format("73            13         25"));
            list.Add(string.Format("74            13         27"));
            list.Add(string.Format("75            13         33"));
            list.Add(string.Format("76            14         26"));
            list.Add(string.Format("77            14         28"));
            list.Add(string.Format("78            14         34"));
            list.Add(string.Format("79            15         29"));
            list.Add(string.Format("80            15         31"));
            list.Add(string.Format("81            15         35"));
            list.Add(string.Format("82            16         30"));
            list.Add(string.Format("83            16         32"));
            list.Add(string.Format("84            16         36"));
            list.Add(string.Format("85            17         25"));
            list.Add(string.Format("86            17         37"));
            list.Add(string.Format("87            17         53"));
            list.Add(string.Format("88            18         26"));
            list.Add(string.Format("89            18         37"));
            list.Add(string.Format("90            18         54"));
            list.Add(string.Format("91            19         27"));
            list.Add(string.Format("92            19         38"));
            list.Add(string.Format("93            19         55"));
            list.Add(string.Format("94            20         28"));
            list.Add(string.Format("95            20         39"));
            list.Add(string.Format("96            20         56"));
            list.Add(string.Format("97            21         29"));
            list.Add(string.Format("98            21         38"));
            list.Add(string.Format("99            21         57"));
            list.Add(string.Format("100           22         30"));
            list.Add(string.Format("101           22         39"));
            list.Add(string.Format("102           22         58"));
            list.Add(string.Format("103           23         31"));
            list.Add(string.Format("104           23         40"));
            list.Add(string.Format("105           23         59"));
            list.Add(string.Format("106           24         32"));
            list.Add(string.Format("107           24         40"));
            list.Add(string.Format("108           24         60"));
            list.Add(string.Format("109           25         33"));
            list.Add(string.Format("110           26         34"));
            list.Add(string.Format("111           27         33"));
            list.Add(string.Format("112           28         34"));
            list.Add(string.Format("113           29         35"));
            list.Add(string.Format("114           30         36"));
            list.Add(string.Format("115           31         35"));
            list.Add(string.Format("116           32         36"));
            list.Add(string.Format("117           33         41"));
            list.Add(string.Format("118           33         43"));
            list.Add(string.Format("119           33         49"));
            list.Add(string.Format("120           34         42"));
            list.Add(string.Format("121           34         44"));
            list.Add(string.Format("122           34         50"));
            list.Add(string.Format("123           35         45"));
            list.Add(string.Format("124           35         47"));
            list.Add(string.Format("125           35         51"));
            list.Add(string.Format("126           36         46"));
            list.Add(string.Format("127           36         48"));
            list.Add(string.Format("128           36         52"));
            list.Add(string.Format("129           37         53"));
            list.Add(string.Format("130           37         54"));
            list.Add(string.Format("131           38         55"));
            list.Add(string.Format("132           38         57"));
            list.Add(string.Format("133           39         56"));
            list.Add(string.Format("134           39         58"));
            list.Add(string.Format("135           40         59"));
            list.Add(string.Format("136           40         60"));
            list.Add(string.Format("137           41         49"));
            list.Add(string.Format("138           41         53"));
            list.Add(string.Format("139           41         61"));
            list.Add(string.Format("140           42         50"));
            list.Add(string.Format("141           42         54"));
            list.Add(string.Format("142           42         62"));
            list.Add(string.Format("143           43         49"));
            list.Add(string.Format("144           43         55"));
            list.Add(string.Format("145           43         63"));
            list.Add(string.Format("146           44         50"));
            list.Add(string.Format("147           44         56"));
            list.Add(string.Format("148           44         64"));
            list.Add(string.Format("149           45         51"));
            list.Add(string.Format("150           45         57"));
            list.Add(string.Format("151           45         65"));
            list.Add(string.Format("152           46         52"));
            list.Add(string.Format("153           46         58"));
            list.Add(string.Format("154           46         66"));
            list.Add(string.Format("155           47         51"));
            list.Add(string.Format("156           47         59"));
            list.Add(string.Format("157           47         67"));
            list.Add(string.Format("158           48         52"));
            list.Add(string.Format("159           48         60"));
            list.Add(string.Format("160           48         68"));
            list.Add(string.Format("161           49         61"));
            list.Add(string.Format("162           49         63"));
            list.Add(string.Format("163           49         69"));
            list.Add(string.Format("164           50         62"));
            list.Add(string.Format("165           50         64"));
            list.Add(string.Format("166           50         70"));
            list.Add(string.Format("167           51         65"));
            list.Add(string.Format("168           51         67"));
            list.Add(string.Format("169           51         71"));
            list.Add(string.Format("170           52         66"));
            list.Add(string.Format("171           52         68"));
            list.Add(string.Format("172           52         72"));
            list.Add(string.Format("173           53         61"));
            list.Add(string.Format("174           54         62"));
            list.Add(string.Format("175           55         63"));
            list.Add(string.Format("176           56         64"));
            list.Add(string.Format("177           57         65"));
            list.Add(string.Format("178           58         66"));
            list.Add(string.Format("179           59         67"));
            list.Add(string.Format("180           60         68"));
            list.Add(string.Format("181           61         69"));
            list.Add(string.Format("182           62         70"));
            list.Add(string.Format("183           63         69"));
            list.Add(string.Format("184           64         70"));
            list.Add(string.Format("185           65         71"));
            list.Add(string.Format("186           66         72"));
            list.Add(string.Format("187           67         71"));
            list.Add(string.Format("188           68         72"));
            list.Add(string.Format("189           69         73"));
            list.Add(string.Format("190           69         77"));
            list.Add(string.Format("191           69         79"));
            list.Add(string.Format("192           70         74"));
            list.Add(string.Format("193           70         78"));
            list.Add(string.Format("194           70         80"));
            list.Add(string.Format("195           71         75"));
            list.Add(string.Format("196           71         81"));
            list.Add(string.Format("197           71         83"));
            list.Add(string.Format("198           72         76"));
            list.Add(string.Format("199           72         82"));
            list.Add(string.Format("200           72         84"));
            list.Add(string.Format("201           73         77"));
            list.Add(string.Format("202           73         79"));
            list.Add(string.Format("203           73         85"));
            list.Add(string.Format("204           74         78"));
            list.Add(string.Format("205           74         80"));
            list.Add(string.Format("206           74         88"));
            list.Add(string.Format("207           75         81"));
            list.Add(string.Format("208           75         83"));
            list.Add(string.Format("209           75         93"));
            list.Add(string.Format("210           76         82"));
            list.Add(string.Format("211           76         84"));
            list.Add(string.Format("212           76         96"));
            list.Add(string.Format("213           77         85"));
            list.Add(string.Format("214           77         86"));
            list.Add(string.Format("215           78         87"));
            list.Add(string.Format("216           78         88"));
            list.Add(string.Format("217           79         85"));
            list.Add(string.Format("218           79         89"));
            list.Add(string.Format("219           80         88"));
            list.Add(string.Format("220           80         90"));
            list.Add(string.Format("221           81         91"));
            list.Add(string.Format("222           81         93"));
            list.Add(string.Format("223           82         92"));
            list.Add(string.Format("224           82         96"));
            list.Add(string.Format("225           83         93"));
            list.Add(string.Format("226           83         94"));
            list.Add(string.Format("227           84         95"));
            list.Add(string.Format("228           84         96"));
            list.Add(string.Format("229           85         97"));
            list.Add(string.Format("230           86         97"));
            list.Add(string.Format("231           86         99"));
            list.Add(string.Format("232           87         99"));
            list.Add(string.Format("233           87        101"));
            list.Add(string.Format("234           88        101"));
            list.Add(string.Format("235           89         97"));
            list.Add(string.Format("236           89        104"));
            list.Add(string.Format("237           90        101"));
            list.Add(string.Format("238           90        105"));
            list.Add(string.Format("239           91        104"));
            list.Add(string.Format("240           91        108"));
            list.Add(string.Format("241           92        105"));
            list.Add(string.Format("242           92        112"));
            list.Add(string.Format("243           93        108"));
            list.Add(string.Format("244           94        108"));
            list.Add(string.Format("245           94        110"));
            list.Add(string.Format("246           95        110"));
            list.Add(string.Format("247           95        112"));
            list.Add(string.Format("248           96        112"));
            list.Add(string.Format("249           97        113"));
            list.Add(string.Format("250           98        113"));
            list.Add(string.Format("251           98        114"));
            list.Add(string.Format("252           99        114"));
            list.Add(string.Format("253           99        115"));
            list.Add(string.Format("254          100        115"));
            list.Add(string.Format("255          100        116"));
            list.Add(string.Format("256          101        116"));
            list.Add(string.Format("257          102        113"));
            list.Add(string.Format("258          102        117"));
            list.Add(string.Format("259          103        116"));
            list.Add(string.Format("260          103        118"));
            list.Add(string.Format("261          104        117"));
            list.Add(string.Format("262          104        119"));
            list.Add(string.Format("263          105        118"));
            list.Add(string.Format("264          105        120"));
            list.Add(string.Format("265          106        119"));
            list.Add(string.Format("266          106        123"));
            list.Add(string.Format("267          107        120"));
            list.Add(string.Format("268          107        124"));
            list.Add(string.Format("269          108        123"));
            list.Add(string.Format("270          109        121"));
            list.Add(string.Format("271          109        123"));
            list.Add(string.Format("272          110        121"));
            list.Add(string.Format("273          110        122"));
            list.Add(string.Format("274          111        122"));
            list.Add(string.Format("275          111        124"));
            list.Add(string.Format("276          112        124"));
            list.Add(string.Format("277          113        125"));
            list.Add(string.Format("278          114        125"));
            list.Add(string.Format("279          115        127"));
            list.Add(string.Format("280          116        118"));
            list.Add(string.Format("281          116        127"));
            list.Add(string.Format("282          117        125"));
            list.Add(string.Format("283          118        127"));
            list.Add(string.Format("284          119        130"));
            list.Add(string.Format("285          120        124"));
            list.Add(string.Format("286          120        132"));
            list.Add(string.Format("287          121        123"));
            list.Add(string.Format("288          121        130"));
            list.Add(string.Format("289          122        124"));
            list.Add(string.Format("290          122        132"));
            list.Add(string.Format("291          123        130"));
            list.Add(string.Format("292          124        132"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion DATA

            return list;
        }

        public List<string> Get_Tower_Level1_Wing_Data()
        {
            List<string> list = new List<string>();


            #region DATA


            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         3.7370    13.3100     3.7370"));
            list.Add(string.Format("2         5.9220    13.3100     3.7370"));
            list.Add(string.Format("3         8.1070    13.3100     3.7370"));
            list.Add(string.Format("4         3.1100    13.3100     4.0390"));
            list.Add(string.Format("5         8.7340    13.3100     4.0390"));
            list.Add(string.Format("6         2.4550    13.3100     4.3550"));
            list.Add(string.Format("7         9.3890    13.3100     4.3550"));
            list.Add(string.Format("8         1.8230    13.3100     4.6600"));
            list.Add(string.Format("9        10.0210    13.3100     4.6600"));
            list.Add(string.Format("10        1.2570    13.3100     4.9330"));
            list.Add(string.Format("11       10.5870    13.3100     4.9330"));
            list.Add(string.Format("12        0.7340    13.3100     5.1850"));
            list.Add(string.Format("13       11.1100    13.3100     5.1850"));
            list.Add(string.Format("14        0.1900    13.3100     5.4480"));
            list.Add(string.Format("15       11.6540    13.3100     5.4480"));
            list.Add(string.Format("16       -0.7940    13.3100     5.9220"));
            list.Add(string.Format("17        3.7370    13.3100     5.9220"));
            list.Add(string.Format("18        8.1070    13.3100     5.9220"));
            list.Add(string.Format("19       12.6380    13.3100     5.9220"));
            list.Add(string.Format("20        0.1900    13.3100     6.3960"));
            list.Add(string.Format("21       11.6540    13.3100     6.3960"));
            list.Add(string.Format("22        0.7340    13.3100     6.6590"));
            list.Add(string.Format("23       11.1100    13.3100     6.6590"));
            list.Add(string.Format("24        1.2570    13.3100     6.9110"));
            list.Add(string.Format("25       10.5870    13.3100     6.9110"));
            list.Add(string.Format("26        1.8230    13.3100     7.1840"));
            list.Add(string.Format("27       10.0210    13.3100     7.1840"));
            list.Add(string.Format("28        2.4550    13.3100     7.4890"));
            list.Add(string.Format("29        9.3890    13.3100     7.4890"));
            list.Add(string.Format("30        3.1100    13.3100     7.8050"));
            list.Add(string.Format("31        8.7340    13.3100     7.8050"));
            list.Add(string.Format("32        3.7370    13.3100     8.1070"));
            list.Add(string.Format("33        5.9220    13.3100     8.1070"));
            list.Add(string.Format("34        8.1070    13.3100     8.1070"));
            list.Add(string.Format("35        0.1940    13.6430     5.4680"));
            list.Add(string.Format("36       11.6500    13.6430     5.4680"));
            list.Add(string.Format("37        0.1940    13.6430     6.3760"));
            list.Add(string.Format("38       11.6500    13.6430     6.3760"));
            list.Add(string.Format("39        0.7490    13.8300     5.2130"));
            list.Add(string.Format("40       11.0950    13.8300     5.2130"));
            list.Add(string.Format("41        0.7490    13.8300     6.6310"));
            list.Add(string.Format("42       11.0950    13.8300     6.6310"));
            list.Add(string.Format("43        1.2580    14.0010     4.9790"));
            list.Add(string.Format("44       10.5860    14.0010     4.9790"));
            list.Add(string.Format("45        1.2580    14.0010     6.8650"));
            list.Add(string.Format("46       10.5860    14.0010     6.8650"));
            list.Add(string.Format("47        4.8470    14.0980     3.7730"));
            list.Add(string.Format("48        6.9970    14.0980     3.7730"));
            list.Add(string.Format("49        3.7730    14.0980     4.8470"));
            list.Add(string.Format("50        8.0710    14.0980     4.8470"));
            list.Add(string.Format("51        3.7730    14.0980     6.9970"));
            list.Add(string.Format("52        8.0710    14.0980     6.9970"));
            list.Add(string.Format("53        4.8470    14.0980     8.0710"));
            list.Add(string.Format("54        6.9970    14.0980     8.0710"));
            list.Add(string.Format("55        1.8280    14.1930     4.7170"));
            list.Add(string.Format("56       10.0160    14.1930     4.7170"));
            list.Add(string.Format("57        1.8280    14.1930     7.1270"));
            list.Add(string.Format("58       10.0160    14.1930     7.1270"));
            list.Add(string.Format("59        2.4610    14.4070     4.4260"));
            list.Add(string.Format("60        9.3830    14.4070     4.4260"));
            list.Add(string.Format("61        2.4610    14.4070     7.4180"));
            list.Add(string.Format("62        9.3830    14.4070     7.4180"));
            list.Add(string.Format("63        3.1240    14.6300     4.1210"));
            list.Add(string.Format("64        8.7200    14.6300     4.1210"));
            list.Add(string.Format("65        3.1240    14.6300     7.7230"));
            list.Add(string.Format("66        8.7200    14.6300     7.7230"));
            list.Add(string.Format("67        3.8070    14.8600     3.8070"));
            list.Add(string.Format("68        5.9220    14.8600     3.8070"));
            list.Add(string.Format("69        8.0370    14.8600     3.8070"));
            list.Add(string.Format("70        3.8070    14.8600     5.9220"));
            list.Add(string.Format("71        8.0370    14.8600     5.9220"));
            list.Add(string.Format("72        3.8070    14.8600     8.0370"));
            list.Add(string.Format("73        5.9220    14.8600     8.0370"));
            list.Add(string.Format("74        8.0370    14.8600     8.0370"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1              1          2"));
            list.Add(string.Format("2              2          3"));
            list.Add(string.Format("3             32         33"));
            list.Add(string.Format("4             33         34"));
            list.Add(string.Format("5             67         68"));
            list.Add(string.Format("6             68         69"));
            list.Add(string.Format("7             72         73"));
            list.Add(string.Format("8             73         74"));
            list.Add(string.Format("9              1         17"));
            list.Add(string.Format("10             3         18"));
            list.Add(string.Format("11            17         32"));
            list.Add(string.Format("12            18         34"));
            list.Add(string.Format("13            67         70"));
            list.Add(string.Format("14            69         71"));
            list.Add(string.Format("15            70         72"));
            list.Add(string.Format("16            71         74"));
            list.Add(string.Format("17             1          4"));
            list.Add(string.Format("18             1         47"));
            list.Add(string.Format("19             1         49"));
            list.Add(string.Format("20             1         63"));
            list.Add(string.Format("21             1         67"));
            list.Add(string.Format("22             2         47"));
            list.Add(string.Format("23             2         48"));
            list.Add(string.Format("24             3          5"));
            list.Add(string.Format("25             3         48"));
            list.Add(string.Format("26             3         50"));
            list.Add(string.Format("27             3         64"));
            list.Add(string.Format("28             3         69"));
            list.Add(string.Format("29             4          6"));
            list.Add(string.Format("30             4         59"));
            list.Add(string.Format("31             4         63"));
            list.Add(string.Format("32             5          7"));
            list.Add(string.Format("33             5         60"));
            list.Add(string.Format("34             5         64"));
            list.Add(string.Format("35             6          8"));
            list.Add(string.Format("36             6         55"));
            list.Add(string.Format("37             6         59"));
            list.Add(string.Format("38             7          9"));
            list.Add(string.Format("39             7         56"));
            list.Add(string.Format("40             7         60"));
            list.Add(string.Format("41             8         10"));
            list.Add(string.Format("42             8         43"));
            list.Add(string.Format("43             8         55"));
            list.Add(string.Format("44             9         11"));
            list.Add(string.Format("45             9         44"));
            list.Add(string.Format("46             9         56"));
            list.Add(string.Format("47            10         12"));
            list.Add(string.Format("48            10         39"));
            list.Add(string.Format("49            10         43"));
            list.Add(string.Format("50            11         13"));
            list.Add(string.Format("51            11         40"));
            list.Add(string.Format("52            11         44"));
            list.Add(string.Format("53            12         14"));
            list.Add(string.Format("54            12         35"));
            list.Add(string.Format("55            12         39"));
            list.Add(string.Format("56            13         15"));
            list.Add(string.Format("57            13         36"));
            list.Add(string.Format("58            13         40"));
            list.Add(string.Format("59            14         16"));
            list.Add(string.Format("60            14         35"));
            list.Add(string.Format("61            15         19"));
            list.Add(string.Format("62            15         36"));
            list.Add(string.Format("63            16         20"));
            list.Add(string.Format("64            16         35"));
            list.Add(string.Format("65            16         37"));
            list.Add(string.Format("66            17         49"));
            list.Add(string.Format("67            17         51"));
            list.Add(string.Format("68            18         50"));
            list.Add(string.Format("69            18         52"));
            list.Add(string.Format("70            19         21"));
            list.Add(string.Format("71            19         36"));
            list.Add(string.Format("72            19         38"));
            list.Add(string.Format("73            20         22"));
            list.Add(string.Format("74            20         37"));
            list.Add(string.Format("75            21         23"));
            list.Add(string.Format("76            21         38"));
            list.Add(string.Format("77            22         24"));
            list.Add(string.Format("78            22         37"));
            list.Add(string.Format("79            22         41"));
            list.Add(string.Format("80            23         25"));
            list.Add(string.Format("81            23         38"));
            list.Add(string.Format("82            23         42"));
            list.Add(string.Format("83            24         26"));
            list.Add(string.Format("84            24         41"));
            list.Add(string.Format("85            24         45"));
            list.Add(string.Format("86            25         27"));
            list.Add(string.Format("87            25         42"));
            list.Add(string.Format("88            25         46"));
            list.Add(string.Format("89            26         28"));
            list.Add(string.Format("90            26         45"));
            list.Add(string.Format("91            26         57"));
            list.Add(string.Format("92            27         29"));
            list.Add(string.Format("93            27         46"));
            list.Add(string.Format("94            27         58"));
            list.Add(string.Format("95            28         30"));
            list.Add(string.Format("96            28         57"));
            list.Add(string.Format("97            28         61"));
            list.Add(string.Format("98            29         31"));
            list.Add(string.Format("99            29         58"));
            list.Add(string.Format("100           29         62"));
            list.Add(string.Format("101           30         32"));
            list.Add(string.Format("102           30         61"));
            list.Add(string.Format("103           30         65"));
            list.Add(string.Format("104           31         34"));
            list.Add(string.Format("105           31         62"));
            list.Add(string.Format("106           31         66"));
            list.Add(string.Format("107           32         51"));
            list.Add(string.Format("108           32         53"));
            list.Add(string.Format("109           32         65"));
            list.Add(string.Format("110           32         72"));
            list.Add(string.Format("111           33         53"));
            list.Add(string.Format("112           33         54"));
            list.Add(string.Format("113           34         52"));
            list.Add(string.Format("114           34         54"));
            list.Add(string.Format("115           34         66"));
            list.Add(string.Format("116           34         74"));
            list.Add(string.Format("117           35         39"));
            list.Add(string.Format("118           36         40"));
            list.Add(string.Format("119           37         41"));
            list.Add(string.Format("120           38         42"));
            list.Add(string.Format("121           39         43"));
            list.Add(string.Format("122           40         44"));
            list.Add(string.Format("123           41         45"));
            list.Add(string.Format("124           42         46"));
            list.Add(string.Format("125           43         55"));
            list.Add(string.Format("126           44         56"));
            list.Add(string.Format("127           45         57"));
            list.Add(string.Format("128           46         58"));
            list.Add(string.Format("129           47         67"));
            list.Add(string.Format("130           47         68"));
            list.Add(string.Format("131           48         68"));
            list.Add(string.Format("132           48         69"));
            list.Add(string.Format("133           49         67"));
            list.Add(string.Format("134           49         70"));
            list.Add(string.Format("135           50         69"));
            list.Add(string.Format("136           50         71"));
            list.Add(string.Format("137           51         70"));
            list.Add(string.Format("138           51         72"));
            list.Add(string.Format("139           52         71"));
            list.Add(string.Format("140           52         74"));
            list.Add(string.Format("141           53         72"));
            list.Add(string.Format("142           53         73"));
            list.Add(string.Format("143           54         73"));
            list.Add(string.Format("144           54         74"));
            list.Add(string.Format("145           55         59"));
            list.Add(string.Format("146           56         60"));
            list.Add(string.Format("147           57         61"));
            list.Add(string.Format("148           58         62"));
            list.Add(string.Format("149           59         63"));
            list.Add(string.Format("150           60         64"));
            list.Add(string.Format("151           61         65"));
            list.Add(string.Format("152           62         66"));
            list.Add(string.Format("153           63         67"));
            list.Add(string.Format("154           64         69"));
            list.Add(string.Format("155           65         72"));
            list.Add(string.Format("156           66         74"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion DATA

            return list;
        }

        public List<string> Get_Tower_Level2_Data()
        {
            List<string> list = new List<string>();
            #region DATA
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         3.8070    14.8600     3.8070"));
            list.Add(string.Format("2         5.9220    14.8600     3.8070"));
            list.Add(string.Format("3         8.0370    14.8600     3.8070"));
            list.Add(string.Format("4         3.8070    14.8600     5.9220"));
            list.Add(string.Format("5         8.0370    14.8600     5.9220"));
            list.Add(string.Format("6         3.8070    14.8600     8.0370"));
            list.Add(string.Format("7         5.9220    14.8600     8.0370"));
            list.Add(string.Format("8         8.0370    14.8600     8.0370"));
            list.Add(string.Format("9         4.8650    15.5280     3.9020"));
            list.Add(string.Format("10        6.9800    15.5280     3.9020"));
            list.Add(string.Format("11        3.9030    15.5280     3.9030"));
            list.Add(string.Format("12        7.9420    15.5280     3.9030"));
            list.Add(string.Format("13        3.9020    15.5280     4.8650"));
            list.Add(string.Format("14        7.9420    15.5280     4.8650"));
            list.Add(string.Format("15        3.9020    15.5280     6.9800"));
            list.Add(string.Format("16        7.9420    15.5280     6.9800"));
            list.Add(string.Format("17        3.9030    15.5280     7.9420"));
            list.Add(string.Format("18        4.8650    15.5280     7.9420"));
            list.Add(string.Format("19        6.9800    15.5280     7.9420"));
            list.Add(string.Format("20        7.9420    15.5280     7.9420"));
            list.Add(string.Format("21        3.9980    16.1960     3.9980"));
            list.Add(string.Format("22        5.9220    16.1960     3.9980"));
            list.Add(string.Format("23        7.8460    16.1960     3.9980"));
            list.Add(string.Format("24        3.9980    16.1960     5.9220"));
            list.Add(string.Format("25        7.8460    16.1960     5.9220"));
            list.Add(string.Format("26        3.9980    16.1960     7.8460"));
            list.Add(string.Format("27        5.9220    16.1960     7.8460"));
            list.Add(string.Format("28        7.8460    16.1960     7.8460"));
            list.Add(string.Format("29        5.0400    16.7530     4.0770"));
            list.Add(string.Format("30        6.8050    16.7530     4.0770"));
            list.Add(string.Format("31        4.0780    16.7530     4.0780"));
            list.Add(string.Format("32        7.7670    16.7530     4.0780"));
            list.Add(string.Format("33        4.0770    16.7530     5.0400"));
            list.Add(string.Format("34        7.7670    16.7530     5.0400"));
            list.Add(string.Format("35        4.0770    16.7530     6.8050"));
            list.Add(string.Format("36        7.7670    16.7530     6.8050"));
            list.Add(string.Format("37        4.0780    16.7530     7.7670"));
            list.Add(string.Format("38        5.0400    16.7530     7.7670"));
            list.Add(string.Format("39        6.8050    16.7530     7.7670"));
            list.Add(string.Format("40        7.7670    16.7530     7.7670"));
            list.Add(string.Format("41        4.1570    17.3100     4.1570"));
            list.Add(string.Format("42        7.6870    17.3100     4.1570"));
            list.Add(string.Format("43        4.1570    17.3100     7.6870"));
            list.Add(string.Format("44        7.6870    17.3100     7.6870"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1              1          2"));
            list.Add(string.Format("2              2          3"));
            list.Add(string.Format("3              6          7"));
            list.Add(string.Format("4              7          8"));
            list.Add(string.Format("5             17         18"));
            list.Add(string.Format("6             19         20"));
            list.Add(string.Format("7             21         22"));
            list.Add(string.Format("8             22         23"));
            list.Add(string.Format("9             26         27"));
            list.Add(string.Format("10            27         28"));
            list.Add(string.Format("11            37         38"));
            list.Add(string.Format("12            39         40"));
            list.Add(string.Format("13            41         42"));
            list.Add(string.Format("14            43         44"));
            list.Add(string.Format("15             1          4"));
            list.Add(string.Format("16             3          5"));
            list.Add(string.Format("17             4          6"));
            list.Add(string.Format("18             5          8"));
            list.Add(string.Format("19            12         14"));
            list.Add(string.Format("20            16         20"));
            list.Add(string.Format("21            21         24"));
            list.Add(string.Format("22            23         25"));
            list.Add(string.Format("23            24         26"));
            list.Add(string.Format("24            25         28"));
            list.Add(string.Format("25            32         34"));
            list.Add(string.Format("26            36         40"));
            list.Add(string.Format("27            41         43"));
            list.Add(string.Format("28            42         44"));
            list.Add(string.Format("29             1          9"));
            list.Add(string.Format("30             1         11"));
            list.Add(string.Format("31             1         13"));
            list.Add(string.Format("32             3         10"));
            list.Add(string.Format("33             3         12"));
            list.Add(string.Format("34             3         14"));
            list.Add(string.Format("35             6         15"));
            list.Add(string.Format("36             6         17"));
            list.Add(string.Format("37             6         18"));
            list.Add(string.Format("38             8         16"));
            list.Add(string.Format("39             8         19"));
            list.Add(string.Format("40             8         20"));
            list.Add(string.Format("41             9         11"));
            list.Add(string.Format("42             9         21"));
            list.Add(string.Format("43             9         22"));
            list.Add(string.Format("44            10         12"));
            list.Add(string.Format("45            10         22"));
            list.Add(string.Format("46            10         23"));
            list.Add(string.Format("47            11         13"));
            list.Add(string.Format("48            11         21"));
            list.Add(string.Format("49            12         23"));
            list.Add(string.Format("50            13         21"));
            list.Add(string.Format("51            13         24"));
            list.Add(string.Format("52            14         23"));
            list.Add(string.Format("53            14         25"));
            list.Add(string.Format("54            15         17"));
            list.Add(string.Format("55            15         24"));
            list.Add(string.Format("56            15         26"));
            list.Add(string.Format("57            16         25"));
            list.Add(string.Format("58            16         28"));
            list.Add(string.Format("59            17         26"));
            list.Add(string.Format("60            18         26"));
            list.Add(string.Format("61            18         27"));
            list.Add(string.Format("62            19         27"));
            list.Add(string.Format("63            19         28"));
            list.Add(string.Format("64            20         28"));
            list.Add(string.Format("65            21         29"));
            list.Add(string.Format("66            21         31"));
            list.Add(string.Format("67            21         33"));
            list.Add(string.Format("68            22         29"));
            list.Add(string.Format("69            22         30"));
            list.Add(string.Format("70            23         30"));
            list.Add(string.Format("71            23         32"));
            list.Add(string.Format("72            23         34"));
            list.Add(string.Format("73            24         33"));
            list.Add(string.Format("74            24         35"));
            list.Add(string.Format("75            25         34"));
            list.Add(string.Format("76            25         36"));
            list.Add(string.Format("77            26         35"));
            list.Add(string.Format("78            26         37"));
            list.Add(string.Format("79            26         38"));
            list.Add(string.Format("80            27         38"));
            list.Add(string.Format("81            27         39"));
            list.Add(string.Format("82            28         36"));
            list.Add(string.Format("83            28         39"));
            list.Add(string.Format("84            28         40"));
            list.Add(string.Format("85            29         31"));
            list.Add(string.Format("86            29         41"));
            list.Add(string.Format("87            30         32"));
            list.Add(string.Format("88            30         42"));
            list.Add(string.Format("89            31         33"));
            list.Add(string.Format("90            31         41"));
            list.Add(string.Format("91            32         42"));
            list.Add(string.Format("92            33         41"));
            list.Add(string.Format("93            34         42"));
            list.Add(string.Format("94            35         37"));
            list.Add(string.Format("95            35         43"));
            list.Add(string.Format("96            36         44"));
            list.Add(string.Format("97            37         43"));
            list.Add(string.Format("98            38         43"));
            list.Add(string.Format("99            39         44"));
            list.Add(string.Format("100           40         44"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion DATA

            return list;
        }

        public List<string> Get_Tower_Level2_Wing_Data()
        {
            List<string> list = new List<string>();
            #region DATA
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         4.1570    17.3100     4.1570"));
            list.Add(string.Format("2         7.6870    17.3100     4.1570"));
            list.Add(string.Format("3         3.5900    17.3100     4.4080"));
            list.Add(string.Format("4         8.2540    17.3100     4.4080"));
            list.Add(string.Format("5         2.9440    17.3100     4.6940"));
            list.Add(string.Format("6         8.9000    17.3100     4.6940"));
            list.Add(string.Format("7         2.4210    17.3100     4.9260"));
            list.Add(string.Format("8         9.4230    17.3100     4.9260"));
            list.Add(string.Format("9         2.0150    17.3100     5.1060"));
            list.Add(string.Format("10        9.8290    17.3100     5.1060"));
            list.Add(string.Format("11        1.6440    17.3100     5.2700"));
            list.Add(string.Format("12       10.2000    17.3100     5.2700"));
            list.Add(string.Format("13        1.2760    17.3100     5.4330"));
            list.Add(string.Format("14       10.5680    17.3100     5.4330"));
            list.Add(string.Format("15        0.9120    17.3100     5.5950"));
            list.Add(string.Format("16       10.9320    17.3100     5.5950"));
            list.Add(string.Format("17        0.1770    17.3100     5.9200"));
            list.Add(string.Format("18       11.6670    17.3100     5.9200"));
            list.Add(string.Format("19        0.9120    17.3100     6.2490"));
            list.Add(string.Format("20       10.9320    17.3100     6.2490"));
            list.Add(string.Format("21        1.2760    17.3100     6.4110"));
            list.Add(string.Format("22       10.5680    17.3100     6.4110"));
            list.Add(string.Format("23        1.6440    17.3100     6.5740"));
            list.Add(string.Format("24       10.2000    17.3100     6.5740"));
            list.Add(string.Format("25        2.0150    17.3100     6.7380"));
            list.Add(string.Format("26        9.8290    17.3100     6.7380"));
            list.Add(string.Format("27        2.4210    17.3100     6.9180"));
            list.Add(string.Format("28        9.4230    17.3100     6.9180"));
            list.Add(string.Format("29        2.9440    17.3100     7.1500"));
            list.Add(string.Format("30        8.9000    17.3100     7.1500"));
            list.Add(string.Format("31        3.5900    17.3100     7.4360"));
            list.Add(string.Format("32        8.2540    17.3100     7.4360"));
            list.Add(string.Format("33        4.1570    17.3100     7.6870"));
            list.Add(string.Format("34        7.6870    17.3100     7.6870"));
            list.Add(string.Format("35        0.9100    17.5780     5.6590"));
            list.Add(string.Format("36       10.9340    17.5780     5.6590"));
            list.Add(string.Format("37        0.9100    17.5780     6.1850"));
            list.Add(string.Format("38       10.9340    17.5780     6.1850"));
            list.Add(string.Format("39        1.2720    17.7110     5.5290"));
            list.Add(string.Format("40       10.5720    17.7110     5.5290"));
            list.Add(string.Format("41        1.2720    17.7110     6.3150"));
            list.Add(string.Format("42       10.5720    17.7110     6.3150"));
            list.Add(string.Format("43        1.6390    17.8450     5.3980"));
            list.Add(string.Format("44       10.2050    17.8450     5.3980"));
            list.Add(string.Format("45        1.6390    17.8450     6.4460"));
            list.Add(string.Format("46       10.2050    17.8450     6.4460"));
            list.Add(string.Format("47        2.0070    17.9800     5.2670"));
            list.Add(string.Format("48        9.8370    17.9800     5.2670"));
            list.Add(string.Format("49        2.0070    17.9800     6.5770"));
            list.Add(string.Format("50        9.8370    17.9800     6.5770"));
            list.Add(string.Format("51        4.2880    18.1100     4.2880"));
            list.Add(string.Format("52        5.0680    18.1100     4.2880"));
            list.Add(string.Format("53        6.7760    18.1100     4.2880"));
            list.Add(string.Format("54        7.5560    18.1100     4.2880"));
            list.Add(string.Format("55        4.2880    18.1100     5.0680"));
            list.Add(string.Format("56        7.5560    18.1100     5.0680"));
            list.Add(string.Format("57        4.2880    18.1100     6.7760"));
            list.Add(string.Format("58        7.5560    18.1100     6.7760"));
            list.Add(string.Format("59        4.2880    18.1100     7.5560"));
            list.Add(string.Format("60        5.0680    18.1100     7.5560"));
            list.Add(string.Format("61        6.7760    18.1100     7.5560"));
            list.Add(string.Format("62        7.5560    18.1100     7.5560"));
            list.Add(string.Format("63        2.4190    18.1310     5.1200"));
            list.Add(string.Format("64        9.4250    18.1310     5.1200"));
            list.Add(string.Format("65        2.4190    18.1310     6.7240"));
            list.Add(string.Format("66        9.4250    18.1310     6.7240"));
            list.Add(string.Format("67        2.9350    18.3200     4.9360"));
            list.Add(string.Format("68        8.9090    18.3200     4.9360"));
            list.Add(string.Format("69        2.9350    18.3200     6.9080"));
            list.Add(string.Format("70        8.9090    18.3200     6.9080"));
            list.Add(string.Format("71        3.5860    18.5580     4.7040"));
            list.Add(string.Format("72        8.2580    18.5580     4.7040"));
            list.Add(string.Format("73        3.5860    18.5580     7.1400"));
            list.Add(string.Format("74        8.2580    18.5580     7.1400"));
            list.Add(string.Format("75        4.4100    18.8600     4.4100"));
            list.Add(string.Format("76        5.9220    18.8600     4.4100"));
            list.Add(string.Format("77        7.4340    18.8600     4.4100"));
            list.Add(string.Format("78        4.4100    18.8600     5.9220"));
            list.Add(string.Format("79        7.4340    18.8600     5.9220"));
            list.Add(string.Format("80        4.4100    18.8600     7.4340"));
            list.Add(string.Format("81        5.9220    18.8600     7.4340"));
            list.Add(string.Format("82        7.4340    18.8600     7.4340"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1              1          2"));
            list.Add(string.Format("2             33         34"));
            list.Add(string.Format("3             51         52"));
            list.Add(string.Format("4             53         54"));
            list.Add(string.Format("5             59         60"));
            list.Add(string.Format("6             61         62"));
            list.Add(string.Format("7             75         76"));
            list.Add(string.Format("8             76         77"));
            list.Add(string.Format("9             80         81"));
            list.Add(string.Format("10            81         82"));
            list.Add(string.Format("11             1         33"));
            list.Add(string.Format("12             2         34"));
            list.Add(string.Format("13            51         55"));
            list.Add(string.Format("14            54         56"));
            list.Add(string.Format("15            57         59"));
            list.Add(string.Format("16            58         62"));
            list.Add(string.Format("17            75         78"));
            list.Add(string.Format("18            77         79"));
            list.Add(string.Format("19            78         80"));
            list.Add(string.Format("20            79         82"));
            list.Add(string.Format("21             1          3"));
            list.Add(string.Format("22             1         51"));
            list.Add(string.Format("23             1         52"));
            list.Add(string.Format("24             1         55"));
            list.Add(string.Format("25             1         71"));
            list.Add(string.Format("26             2          4"));
            list.Add(string.Format("27             2         53"));
            list.Add(string.Format("28             2         54"));
            list.Add(string.Format("29             2         56"));
            list.Add(string.Format("30             2         72"));
            list.Add(string.Format("31             3          5"));
            list.Add(string.Format("32             3         67"));
            list.Add(string.Format("33             3         71"));
            list.Add(string.Format("34             4          6"));
            list.Add(string.Format("35             4         68"));
            list.Add(string.Format("36             4         72"));
            list.Add(string.Format("37             5          7"));
            list.Add(string.Format("38             5         63"));
            list.Add(string.Format("39             5         67"));
            list.Add(string.Format("40             6          8"));
            list.Add(string.Format("41             6         64"));
            list.Add(string.Format("42             6         68"));
            list.Add(string.Format("43             7          9"));
            list.Add(string.Format("44             7         47"));
            list.Add(string.Format("45             7         63"));
            list.Add(string.Format("46             8         10"));
            list.Add(string.Format("47             8         48"));
            list.Add(string.Format("48             8         64"));
            list.Add(string.Format("49             9         11"));
            list.Add(string.Format("50             9         43"));
            list.Add(string.Format("51             9         47"));
            list.Add(string.Format("52            10         12"));
            list.Add(string.Format("53            10         44"));
            list.Add(string.Format("54            10         48"));
            list.Add(string.Format("55            11         13"));
            list.Add(string.Format("56            11         39"));
            list.Add(string.Format("57            11         43"));
            list.Add(string.Format("58            12         14"));
            list.Add(string.Format("59            12         40"));
            list.Add(string.Format("60            12         44"));
            list.Add(string.Format("61            13         15"));
            list.Add(string.Format("62            13         35"));
            list.Add(string.Format("63            13         39"));
            list.Add(string.Format("64            14         16"));
            list.Add(string.Format("65            14         36"));
            list.Add(string.Format("66            14         40"));
            list.Add(string.Format("67            15         17"));
            list.Add(string.Format("68            15         35"));
            list.Add(string.Format("69            16         18"));
            list.Add(string.Format("70            16         36"));
            list.Add(string.Format("71            17         19"));
            list.Add(string.Format("72            17         35"));
            list.Add(string.Format("73            17         37"));
            list.Add(string.Format("74            18         20"));
            list.Add(string.Format("75            18         36"));
            list.Add(string.Format("76            18         38"));
            list.Add(string.Format("77            19         21"));
            list.Add(string.Format("78            19         37"));
            list.Add(string.Format("79            20         22"));
            list.Add(string.Format("80            20         38"));
            list.Add(string.Format("81            21         23"));
            list.Add(string.Format("82            21         37"));
            list.Add(string.Format("83            21         41"));
            list.Add(string.Format("84            22         24"));
            list.Add(string.Format("85            22         38"));
            list.Add(string.Format("86            22         42"));
            list.Add(string.Format("87            23         25"));
            list.Add(string.Format("88            23         41"));
            list.Add(string.Format("89            23         45"));
            list.Add(string.Format("90            24         26"));
            list.Add(string.Format("91            24         42"));
            list.Add(string.Format("92            24         46"));
            list.Add(string.Format("93            25         27"));
            list.Add(string.Format("94            25         45"));
            list.Add(string.Format("95            25         49"));
            list.Add(string.Format("96            26         28"));
            list.Add(string.Format("97            26         46"));
            list.Add(string.Format("98            26         50"));
            list.Add(string.Format("99            27         29"));
            list.Add(string.Format("100           27         49"));
            list.Add(string.Format("101           27         65"));
            list.Add(string.Format("102           28         30"));
            list.Add(string.Format("103           28         50"));
            list.Add(string.Format("104           28         66"));
            list.Add(string.Format("105           29         31"));
            list.Add(string.Format("106           29         65"));
            list.Add(string.Format("107           29         69"));
            list.Add(string.Format("108           30         32"));
            list.Add(string.Format("109           30         66"));
            list.Add(string.Format("110           30         70"));
            list.Add(string.Format("111           31         33"));
            list.Add(string.Format("112           31         69"));
            list.Add(string.Format("113           31         73"));
            list.Add(string.Format("114           32         34"));
            list.Add(string.Format("115           32         70"));
            list.Add(string.Format("116           32         74"));
            list.Add(string.Format("117           33         57"));
            list.Add(string.Format("118           33         59"));
            list.Add(string.Format("119           33         60"));
            list.Add(string.Format("120           33         73"));
            list.Add(string.Format("121           34         58"));
            list.Add(string.Format("122           34         61"));
            list.Add(string.Format("123           34         62"));
            list.Add(string.Format("124           34         74"));
            list.Add(string.Format("125           35         39"));
            list.Add(string.Format("126           36         40"));
            list.Add(string.Format("127           37         41"));
            list.Add(string.Format("128           38         42"));
            list.Add(string.Format("129           39         43"));
            list.Add(string.Format("130           40         44"));
            list.Add(string.Format("131           41         45"));
            list.Add(string.Format("132           42         46"));
            list.Add(string.Format("133           43         47"));
            list.Add(string.Format("134           44         48"));
            list.Add(string.Format("135           45         49"));
            list.Add(string.Format("136           46         50"));
            list.Add(string.Format("137           47         63"));
            list.Add(string.Format("138           48         64"));
            list.Add(string.Format("139           49         65"));
            list.Add(string.Format("140           50         66"));
            list.Add(string.Format("141           51         75"));
            list.Add(string.Format("142           52         75"));
            list.Add(string.Format("143           52         76"));
            list.Add(string.Format("144           53         76"));
            list.Add(string.Format("145           53         77"));
            list.Add(string.Format("146           54         77"));
            list.Add(string.Format("147           55         75"));
            list.Add(string.Format("148           55         78"));
            list.Add(string.Format("149           56         77"));
            list.Add(string.Format("150           56         79"));
            list.Add(string.Format("151           57         78"));
            list.Add(string.Format("152           57         80"));
            list.Add(string.Format("153           58         79"));
            list.Add(string.Format("154           58         82"));
            list.Add(string.Format("155           59         80"));
            list.Add(string.Format("156           60         80"));
            list.Add(string.Format("157           60         81"));
            list.Add(string.Format("158           61         81"));
            list.Add(string.Format("159           61         82"));
            list.Add(string.Format("160           62         82"));
            list.Add(string.Format("161           63         67"));
            list.Add(string.Format("162           64         68"));
            list.Add(string.Format("163           65         69"));
            list.Add(string.Format("164           66         70"));
            list.Add(string.Format("165           67         71"));
            list.Add(string.Format("166           68         72"));
            list.Add(string.Format("167           69         73"));
            list.Add(string.Format("168           70         74"));
            list.Add(string.Format("169           71         75"));
            list.Add(string.Format("170           72         77"));
            list.Add(string.Format("171           73         80"));
            list.Add(string.Format("172           74         82"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion DATA

            return list;
        }

        public List<string> Get_Tower_Level3_Data()
        {
            List<string> list = new List<string>();
            #region DATA
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         4.4100    18.8600     4.4100"));
            list.Add(string.Format("2         5.9220    18.8600     4.4100"));
            list.Add(string.Format("3         7.4340    18.8600     4.4100"));
            list.Add(string.Format("4         4.4100    18.8600     5.9220"));
            list.Add(string.Format("5         7.4340    18.8600     5.9220"));
            list.Add(string.Format("6         4.4100    18.8600     7.4340"));
            list.Add(string.Format("7         5.9220    18.8600     7.4340"));
            list.Add(string.Format("8         7.4340    18.8600     7.4340"));
            list.Add(string.Format("9         4.6470    20.3100     4.6470"));
            list.Add(string.Format("10        5.9220    20.3100     4.6470"));
            list.Add(string.Format("11        7.1970    20.3100     4.6470"));
            list.Add(string.Format("12        4.6470    20.3100     5.9220"));
            list.Add(string.Format("13        7.1970    20.3100     5.9220"));
            list.Add(string.Format("14        4.6470    20.3100     7.1970"));
            list.Add(string.Format("15        5.9220    20.3100     7.1970"));
            list.Add(string.Format("16        7.1970    20.3100     7.1970"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1              1          2"));
            list.Add(string.Format("2              2          3"));
            list.Add(string.Format("3              6          7"));
            list.Add(string.Format("4              7          8"));
            list.Add(string.Format("5              9         10"));
            list.Add(string.Format("6             10         11"));
            list.Add(string.Format("7             14         15"));
            list.Add(string.Format("8             15         16"));
            list.Add(string.Format("9              1          4"));
            list.Add(string.Format("10             3          5"));
            list.Add(string.Format("11             4          6"));
            list.Add(string.Format("12             5          8"));
            list.Add(string.Format("13             9         12"));
            list.Add(string.Format("14            11         13"));
            list.Add(string.Format("15            12         14"));
            list.Add(string.Format("16            13         16"));
            list.Add(string.Format("17             1          9"));
            list.Add(string.Format("18             3         11"));
            list.Add(string.Format("19             6         14"));
            list.Add(string.Format("20             8         16"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion DATA

            return list;
        }

        public List<string> Get_Tower_Level3_Wing_Data()
        {
            List<string> list = new List<string>();
            #region DATA
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         4.6470    20.3100     4.6470"));
            list.Add(string.Format("2         5.9220    20.3100     4.6470"));
            list.Add(string.Format("3         7.1970    20.3100     4.6470"));
            list.Add(string.Format("4         4.1260    20.3100     4.8220"));
            list.Add(string.Format("5         7.7180    20.3100     4.8220"));
            list.Add(string.Format("6         3.5840    20.3100     5.0050"));
            list.Add(string.Format("7         8.2600    20.3100     5.0050"));
            list.Add(string.Format("8         3.0860    20.3100     5.1730"));
            list.Add(string.Format("9         8.7580    20.3100     5.1730"));
            list.Add(string.Format("10        2.5570    20.3100     5.3510"));
            list.Add(string.Format("11        9.2870    20.3100     5.3510"));
            list.Add(string.Format("12        2.1700    20.3100     5.4820"));
            list.Add(string.Format("13        9.6740    20.3100     5.4820"));
            list.Add(string.Format("14        1.7310    20.3100     5.6300"));
            list.Add(string.Format("15       10.1130    20.3100     5.6300"));
            list.Add(string.Format("16        0.8630    20.3100     5.9220"));
            list.Add(string.Format("17        4.6470    20.3100     5.9220"));
            list.Add(string.Format("18        7.1970    20.3100     5.9220"));
            list.Add(string.Format("19       10.9810    20.3100     5.9220"));
            list.Add(string.Format("20        1.7310    20.3100     6.2140"));
            list.Add(string.Format("21       10.1130    20.3100     6.2140"));
            list.Add(string.Format("22        2.1700    20.3100     6.3620"));
            list.Add(string.Format("23        9.6740    20.3100     6.3620"));
            list.Add(string.Format("24        2.5570    20.3100     6.4930"));
            list.Add(string.Format("25        9.2870    20.3100     6.4930"));
            list.Add(string.Format("26        3.0860    20.3100     6.6710"));
            list.Add(string.Format("27        8.7580    20.3100     6.6710"));
            list.Add(string.Format("28        3.5840    20.3100     6.8390"));
            list.Add(string.Format("29        8.2600    20.3100     6.8390"));
            list.Add(string.Format("30        4.1260    20.3100     7.0220"));
            list.Add(string.Format("31        7.7180    20.3100     7.0220"));
            list.Add(string.Format("32        4.6470    20.3100     7.1970"));
            list.Add(string.Format("33        5.9220    20.3100     7.1970"));
            list.Add(string.Format("34        7.1970    20.3100     7.1970"));
            list.Add(string.Format("35        1.7230    20.6550     5.6550"));
            list.Add(string.Format("36       10.1210    20.6550     5.6550"));
            list.Add(string.Format("37        1.7230    20.6550     6.1890"));
            list.Add(string.Format("38       10.1210    20.6550     6.1890"));
            list.Add(string.Format("39        2.1930    20.8440     5.5080"));
            list.Add(string.Format("40        9.6510    20.8440     5.5080"));
            list.Add(string.Format("41        2.1930    20.8440     6.3360"));
            list.Add(string.Format("42        9.6510    20.8440     6.3360"));
            list.Add(string.Format("43        2.5690    20.9950     5.3910"));
            list.Add(string.Format("44        9.2750    20.9950     5.3910"));
            list.Add(string.Format("45        2.5690    20.9950     6.4530"));
            list.Add(string.Format("46        9.2750    20.9950     6.4530"));
            list.Add(string.Format("47        5.9220    21.1080     4.6860"));
            list.Add(string.Format("48        4.6860    21.1080     5.9220"));
            list.Add(string.Format("49        7.1580    21.1080     5.9220"));
            list.Add(string.Format("50        5.9220    21.1080     7.1580"));
            list.Add(string.Format("51        3.0920    21.2050     5.2290"));
            list.Add(string.Format("52        8.7520    21.2050     5.2290"));
            list.Add(string.Format("53        3.0920    21.2050     6.6150"));
            list.Add(string.Format("54        8.7520    21.2050     6.6150"));
            list.Add(string.Format("55        3.6300    21.4220     5.0610"));
            list.Add(string.Format("56        8.2140    21.4220     5.0610"));
            list.Add(string.Format("57        3.6300    21.4220     6.7830"));
            list.Add(string.Format("58        8.2140    21.4220     6.7830"));
            list.Add(string.Format("59        4.1380    21.6260     4.9040"));
            list.Add(string.Format("60        7.7060    21.6260     4.9040"));
            list.Add(string.Format("61        4.1380    21.6260     6.9400"));
            list.Add(string.Format("62        7.7060    21.6260     6.9400"));
            list.Add(string.Format("63        4.7220    21.8600     4.7220"));
            list.Add(string.Format("64        7.1220    21.8600     4.7220"));
            list.Add(string.Format("65        4.7220    21.8600     7.1220"));
            list.Add(string.Format("66        7.1220    21.8600     7.1220"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1             17         48"));
            list.Add(string.Format("2             18         49"));
            list.Add(string.Format("3              1          2"));
            list.Add(string.Format("4              2          3"));
            list.Add(string.Format("5             32         33"));
            list.Add(string.Format("6             33         34"));
            list.Add(string.Format("7             63         64"));
            list.Add(string.Format("8             65         66"));
            list.Add(string.Format("9              1         17"));
            list.Add(string.Format("10             3         18"));
            list.Add(string.Format("11            17         32"));
            list.Add(string.Format("12            18         34"));
            list.Add(string.Format("13            63         65"));
            list.Add(string.Format("14            64         66"));
            list.Add(string.Format("15             1          4"));
            list.Add(string.Format("16             1         47"));
            list.Add(string.Format("17             1         48"));
            list.Add(string.Format("18             1         59"));
            list.Add(string.Format("19             1         63"));
            list.Add(string.Format("20             2         47"));
            list.Add(string.Format("21             3          5"));
            list.Add(string.Format("22             3         47"));
            list.Add(string.Format("23             3         49"));
            list.Add(string.Format("24             3         60"));
            list.Add(string.Format("25             3         64"));
            list.Add(string.Format("26             4          6"));
            list.Add(string.Format("27             4         55"));
            list.Add(string.Format("28             4         59"));
            list.Add(string.Format("29             5          7"));
            list.Add(string.Format("30             5         56"));
            list.Add(string.Format("31             5         60"));
            list.Add(string.Format("32             6          8"));
            list.Add(string.Format("33             6         51"));
            list.Add(string.Format("34             6         55"));
            list.Add(string.Format("35             7          9"));
            list.Add(string.Format("36             7         52"));
            list.Add(string.Format("37             7         56"));
            list.Add(string.Format("38             8         10"));
            list.Add(string.Format("39             8         43"));
            list.Add(string.Format("40             8         51"));
            list.Add(string.Format("41             9         11"));
            list.Add(string.Format("42             9         44"));
            list.Add(string.Format("43             9         52"));
            list.Add(string.Format("44            10         12"));
            list.Add(string.Format("45            10         39"));
            list.Add(string.Format("46            10         43"));
            list.Add(string.Format("47            11         13"));
            list.Add(string.Format("48            11         40"));
            list.Add(string.Format("49            11         44"));
            list.Add(string.Format("50            12         14"));
            list.Add(string.Format("51            12         35"));
            list.Add(string.Format("52            12         39"));
            list.Add(string.Format("53            13         15"));
            list.Add(string.Format("54            13         36"));
            list.Add(string.Format("55            13         40"));
            list.Add(string.Format("56            14         16"));
            list.Add(string.Format("57            14         35"));
            list.Add(string.Format("58            15         19"));
            list.Add(string.Format("59            15         36"));
            list.Add(string.Format("60            16         20"));
            list.Add(string.Format("61            16         35"));
            list.Add(string.Format("62            16         37"));
            list.Add(string.Format("63            19         21"));
            list.Add(string.Format("64            19         36"));
            list.Add(string.Format("65            19         38"));
            list.Add(string.Format("66            20         22"));
            list.Add(string.Format("67            20         37"));
            list.Add(string.Format("68            21         23"));
            list.Add(string.Format("69            21         38"));
            list.Add(string.Format("70            22         24"));
            list.Add(string.Format("71            22         37"));
            list.Add(string.Format("72            22         41"));
            list.Add(string.Format("73            23         25"));
            list.Add(string.Format("74            23         38"));
            list.Add(string.Format("75            23         42"));
            list.Add(string.Format("76            24         26"));
            list.Add(string.Format("77            24         41"));
            list.Add(string.Format("78            24         45"));
            list.Add(string.Format("79            25         27"));
            list.Add(string.Format("80            25         42"));
            list.Add(string.Format("81            25         46"));
            list.Add(string.Format("82            26         28"));
            list.Add(string.Format("83            26         45"));
            list.Add(string.Format("84            26         53"));
            list.Add(string.Format("85            27         29"));
            list.Add(string.Format("86            27         46"));
            list.Add(string.Format("87            27         54"));
            list.Add(string.Format("88            28         30"));
            list.Add(string.Format("89            28         53"));
            list.Add(string.Format("90            28         57"));
            list.Add(string.Format("91            29         31"));
            list.Add(string.Format("92            29         54"));
            list.Add(string.Format("93            29         58"));
            list.Add(string.Format("94            30         32"));
            list.Add(string.Format("95            30         57"));
            list.Add(string.Format("96            30         61"));
            list.Add(string.Format("97            31         34"));
            list.Add(string.Format("98            31         58"));
            list.Add(string.Format("99            31         62"));
            list.Add(string.Format("100           32         48"));
            list.Add(string.Format("101           32         50"));
            list.Add(string.Format("102           32         61"));
            list.Add(string.Format("103           32         65"));
            list.Add(string.Format("104           33         50"));
            list.Add(string.Format("105           34         49"));
            list.Add(string.Format("106           34         50"));
            list.Add(string.Format("107           34         62"));
            list.Add(string.Format("108           34         66"));
            list.Add(string.Format("109           35         39"));
            list.Add(string.Format("110           36         40"));
            list.Add(string.Format("111           37         41"));
            list.Add(string.Format("112           38         42"));
            list.Add(string.Format("113           39         43"));
            list.Add(string.Format("114           40         44"));
            list.Add(string.Format("115           41         45"));
            list.Add(string.Format("116           42         46"));
            list.Add(string.Format("117           43         51"));
            list.Add(string.Format("118           44         52"));
            list.Add(string.Format("119           45         53"));
            list.Add(string.Format("120           46         54"));
            list.Add(string.Format("121           47         63"));
            list.Add(string.Format("122           47         64"));
            list.Add(string.Format("123           48         63"));
            list.Add(string.Format("124           48         65"));
            list.Add(string.Format("125           49         64"));
            list.Add(string.Format("126           49         66"));
            list.Add(string.Format("127           50         65"));
            list.Add(string.Format("128           50         66"));
            list.Add(string.Format("129           51         55"));
            list.Add(string.Format("130           52         56"));
            list.Add(string.Format("131           53         57"));
            list.Add(string.Format("132           54         58"));
            list.Add(string.Format("133           55         59"));
            list.Add(string.Format("134           56         60"));
            list.Add(string.Format("135           57         61"));
            list.Add(string.Format("136           58         62"));
            list.Add(string.Format("137           59         63"));
            list.Add(string.Format("138           60         64"));
            list.Add(string.Format("139           61         65"));
            list.Add(string.Format("140           62         66"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            #endregion DATA

            return list;
        }
    }

    public class TowerLoadCase
    {

        public string LoadCase { get; set; }

        public bool ISApply_End1 { get; set; }
        public bool ISApply_End2 { get; set; }
        public bool ISApply_End3 { get; set; }
        public bool ISApply_End4 { get; set; }
        public bool ISApply_Wind_Force { get; set; }
        public bool ISApply_Seismic { get; set; }
        public TowerLoadCase()
        {
            LoadCase = "";
            End1 = new TowerLoad();
            End2 = new TowerLoad();
            End3 = new TowerLoad();
            End4 = new TowerLoad();

            ISApply_End1 = true;
            ISApply_End2 = true;
            ISApply_End3 = true;
            ISApply_End4 = true;

            ISApply_Wind_Force = false ;
            ISApply_Seismic = false;

            //Wind_Velocity = 0.0;
            //Wind_Effective_Area_Factor = 0.0;
            //Seismic_Coefficient = 0.0;

            //Seismic_Q1 = 0.0;
            //Seismic_Q2 = 0.0;
            //Seismic_Q3 = 0.0;
            //Seismic_Q4 = 0.0;

            Wind = new WindLoad();
            Seismic = new SeismicLoad();

        }

        public TowerLoad End1 { get; set; }
        public TowerLoad End2 { get; set; }
        public TowerLoad End3 { get; set; }
        public TowerLoad End4 { get; set; }

        public WindLoad Wind { get; set; }
        public SeismicLoad Seismic { get; set; }
        
        //public double Wind_Velocity { get; set; }
        //public double Wind_Effective_Area_Factor { get; set; }
        //public double Seismic_Coefficient { get; set; }
        //public double Seismic_Q1 { get; set; }
        //public double Seismic_Q2 { get; set; }
        //public double Seismic_Q3 { get; set; }
        //public double Seismic_Q4 { get; set; }

    }

    public class TowerLoad
    {
        //End3
        //Load 12 Tons
        //Vertical angle = 36 degrees
        //Horizontal Angle = 28 degrees

        //FZ = cos (36) x 12 = 9.71 Tons
        //FZ = cos (28) x 9.71 = 6.18 Tons = (-) 8.57 Tons

        //FY = sin (36) x 12 = 7.05 Tons = (-) 7.05 Tons

        public TowerLoad()
        {
            Load = 0.0;
            Vertical_Angle = 0.0;
            Horizontal_Angle = 0.0;
        }
        public double Load { get; set; }
        public double Vertical_Angle { get; set; }
        public double Horizontal_Angle { get; set; }


        public double FY
        {
            get
            {
                //return Math.Sin(MyList.Convert_Degree_To_Radian(Horizontal_Angle)) * Load;
                return Math.Cos(MyList.Convert_Degree_To_Radian(Vertical_Angle)) * Load;

            }
        }
        public double FH
        {
            get
            {
                //return Math.Sin(MyList.Convert_Degree_To_Radian(Horizontal_Angle)) * Load;
                return Math.Sin(MyList.Convert_Degree_To_Radian(Vertical_Angle)) * Load;

            }
        }

        public double FX
        {
            get
            {
                return Math.Cos(MyList.Convert_Degree_To_Radian(Horizontal_Angle)) * FH;

            }
        }
        public double FZ
        {
            get
            {
                return Math.Sin(MyList.Convert_Degree_To_Radian(Horizontal_Angle)) * FH;

            }
        }


        public string FY_TEXT
        {
            get
            {
                //return string.Format("FZ = cos (36) x 12 = 9.71 Tons");
                return string.Format("FY = cos({0}) x {1:f2} ", Vertical_Angle, Load);
            }
        }
        public string FH_TEXT
        {
            get
            {
                //return string.Format("FZ = cos (36) x 12 = 9.71 Tons");
                return string.Format("FH = sin({0}) x {1:f2} ", Vertical_Angle, Load);
            }
        }

        public string FX_TEXT
        {
            get
            {
                //return string.Format("FZ = cos (36) x 12 = 9.71 Tons");
                return string.Format("FX = cos({0}) x {1:f2}", Horizontal_Angle, FH);
            }
        }

        public string FZ_TEXT
        {
            get
            {
                //return string.Format("FZ = cos (36) x 12 = 9.71 Tons");
                return string.Format("FZ = sin({0}) x {1:f2}", Horizontal_Angle, FH);
            }
        }
    }

    public class WindLoad
    {
        public double Wind_Velocity { get; set; }
        public double Wind_Pressure { get { return Get_Pressure(Wind_Velocity); } }
        public double Wind_Rectangle_Width { get; set; }
        public double Wind_Rectangle_Height { get; set; }
        public double Effective_Area_Factor { get; set; }
        public double Wind_Rectangle_Effective_Area { get { return Wind_Rectangle_Width * Wind_Rectangle_Height * Effective_Area_Factor; } }
        public double Total_Wind_Force
        {
            get
            {
                return Wind_Rectangle_Effective_Area * Wind_Pressure / 1000;
            }
        }
        public double Total_Nodes { get; set; }

        public double Wind_Load_per_Node
        {
            get
            {
                return Total_Wind_Force / Total_Nodes;
            }
        }

        public WindLoad()
        {
            Wind_Velocity = 30;
            //Wind_Pressure = 540;
            Wind_Rectangle_Width = 13.432;
            Wind_Rectangle_Height = 26.810;
            Effective_Area_Factor = 0.60;

            //Wind Force to be calculated as:
            //Wind Velocity = 30 metres/sec
            //Wind Pressure = 540 N/Sq.mm (From Table)

            //Wind Rectangle Width = 13.432 m
            //Wind Rectangle Height = 26.810 m
            //Effective Area Factor = 0.60 (User Input)
            //Wind Rectangle Effective Area = 13.432 x 26.810 x 0.60 =216.067 Sq.m

            //Total Wind Force = 216.067 x 540 N. = 116676 N = 116.676 kN
            //Total Wind Force is to be applied on 30 Nodes as shown
            //Wind Force per Node = 116.676 / 30 = 3.89 kN in FX or FY directions.
        }


        public List<string> Get_Calculations()
        {
            List<string> list = new List<string>();
            list.Add(string.Format("Wind Force to be calculated as:"));
            list.Add(string.Format("Wind Velocity = {0} metres/sec", Wind_Velocity));
            list.Add(string.Format("Wind Pressure = {1} N/Sq.mm (From Table)", Wind_Pressure));
            list.Add(string.Format(""));
            list.Add(string.Format("Wind Rectangle Width = {0:f3} m", Wind_Rectangle_Width));
            list.Add(string.Format("Wind Rectangle Height = {0:f3} m", Wind_Rectangle_Height));
            //list.Add(string.Format("Effective Area Factor = 0.60 (User Input)", Effective_Area_Factor));
            list.Add(string.Format("Effective Area Factor = {0:f2}", Effective_Area_Factor));
            list.Add(string.Format("Wind Rectangle Effective Area = {0:f3} x {1:f3} x {2:f2} = {3:f3} Sq.m", 
                Wind_Rectangle_Width, Wind_Rectangle_Height, Effective_Area_Factor, Wind_Rectangle_Effective_Area));
            list.Add(string.Format(""));
            //list.Add(string.Format("Total Wind Force = 216.067 x 540 N. = 116676 N = 116.676 kN"));
            list.Add(string.Format("Total Wind Force = {0:f3} x {1} /1000 = {2:f3} kN", Wind_Rectangle_Effective_Area, Wind_Pressure, Total_Wind_Force));
            //list.Add(string.Format("Total Wind Force is to be applied on 30 Nodes as shown"));
            list.Add(string.Format("Total Wind Force is to be applied on {0} Nodes", Total_Nodes));
            list.Add(string.Format("Wind Force per Node = {0:f3} / {1} = {2:f3} kN in FX or FZ directions.", Total_Wind_Force, Total_Nodes, Wind_Load_per_Node));
            list.Add(string.Format(""));

            return list;
        }

        public double Get_Pressure(double velocity)
        {
            List<string> list = new List<string>();

            #region Table
            list.Add(string.Format("1 0.6"));
            list.Add(string.Format("2 2.4"));
            list.Add(string.Format("3 5.4"));
            list.Add(string.Format("4 9.6"));
            list.Add(string.Format("5 15"));
            list.Add(string.Format("6 22"));
            list.Add(string.Format("7 29"));
            list.Add(string.Format("8 38"));
            list.Add(string.Format("9 49"));
            list.Add(string.Format("10 60"));
            list.Add(string.Format("11 73"));
            list.Add(string.Format("12 86"));
            list.Add(string.Format("13 101"));
            list.Add(string.Format("14 118"));
            list.Add(string.Format("15 135"));
            list.Add(string.Format("16 154"));
            list.Add(string.Format("17 173"));
            list.Add(string.Format("18 194"));
            list.Add(string.Format("19 217"));
            list.Add(string.Format("20 240"));
            list.Add(string.Format("21 265"));
            list.Add(string.Format("22 290"));
            list.Add(string.Format("23 317"));
            list.Add(string.Format("24 346"));
            list.Add(string.Format("25 375"));
            list.Add(string.Format("26 406"));
            list.Add(string.Format("27 437"));
            list.Add(string.Format("28 470"));
            list.Add(string.Format("29 505"));
            list.Add(string.Format("30 540"));
            list.Add(string.Format("31 577"));
            list.Add(string.Format("32 614"));
            list.Add(string.Format("33 653"));
            list.Add(string.Format("34 694"));
            list.Add(string.Format("35 735"));
            list.Add(string.Format("36 778"));
            list.Add(string.Format("37 821"));
            list.Add(string.Format("38 866"));
            list.Add(string.Format("39 913"));
            list.Add(string.Format("40 960"));
            list.Add(string.Format("41 1009"));
            list.Add(string.Format("42 1058"));
            list.Add(string.Format("43 1109"));
            list.Add(string.Format("44 1162"));
            list.Add(string.Format("45 1215"));
            list.Add(string.Format("46 1270"));
            list.Add(string.Format("47 1325"));
            list.Add(string.Format("48 1382"));
            list.Add(string.Format("49 1441"));
            list.Add(string.Format("50 1500"));
            #endregion Table

            MyList ml;
            foreach (var item in list)
            {
                try
                {
                    ml = new MyList(item, ' ');
                    if (ml.GetDouble(0) >= velocity)
                    {
                        return ml.GetDouble(1);
                    }
                }
                catch (Exception ex) { }
            }

            return 0.0;
        }

    }

    public class SeismicLoad
    {

        public double W, W1, W2, W3, W4, Ah;

        public double H1, H2, H3, H4;
        public double Q1, Q2, Q3, Q4;

        public SeismicLoad()
        {


            //W = 1000.0;
            //W1 = 40.0;
            //W2 = 30.0;
            //W3 = 10.0;
            //W4 = 5.0;
            //Ah = 0.1;


            //H1 = 13.31;
            //H2 = 17.31;
            //H3 = 20.31;
            //H4 = 21.86;


            W = 0.0;
            W1 = 0.0;
            W2 = 0.0;
            W3 = 0.0;
            W4 = 0.0;
            Ah = 0.1;


            H1 = 0.0;
            H2 = 0.0;
            H3 = 0.0;
            H4 = 0.0;

            Q1 = Q2 = Q3 = Q4 = 0.0;

        }
        public List<string> Get_Calculations()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("W = Total Weight of Structure = {0:f2} kN ", W));
            list.Add(string.Format("W1 = Weight of Structure between Level1 and Level2 = {0:f2} kN", W1));
            list.Add(string.Format("W2 = Weight of Structure between Level2 and Level3 = {0:f2} kN", W2));
            list.Add(string.Format("W3 = Weight of Structure between Level3 and Crown Base = {0:f2} kN", W3));
            list.Add(string.Format("W4 = Weight at base Level of Crown = {0:f2} kN", W4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("Ah = Seismic Coefficient = {0:f2} (User Input)", Ah));
            list.Add(string.Format("Ah = Seismic Coefficient = {0:f2}", Ah));

            double Vb = W * Ah;
            list.Add(string.Format("Vb = Base Shear = W x Ah = {0:f3} x {1:f3} = {2:f3} kN", W, Ah, Vb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("H1 = Tower Height at Wing Level1 = {0:f3} m.", H1));
            list.Add(string.Format("W1 = Weight of Structure between Level1 and Level2 = {0:f3} kN", W1));

            double Fs1 = W1 * H1 * H1;
            list.Add(string.Format("Fs1 = Lateral Force = {0:f3} x {1:f3}^2 = {2:f3} kN", W1, H1, Fs1));
            list.Add(string.Format(""));
            list.Add(string.Format("H2 = Tower Height at Wing Level2 = {0:f3} m.", H2));
            list.Add(string.Format("W2 = Weight of Structure between Level2 and Level3 = {0:f3} kN", W2));
            double Fs2 = W2 * H2 * H2;
            list.Add(string.Format("Fs2 = Lateral Force = {0:f3} x {1:f3}^2 = {2:f3} kN", W2, H2, Fs2));
            list.Add(string.Format(""));
            list.Add(string.Format("H3 = Tower Height at Wing Level3 = {0:f3} m.", H3));
            list.Add(string.Format("W3 = Weight of Structure between Level3 and Crown Base = {0:f3} kN", W3));
            double Fs3 = W3 * H3 * H3;
            list.Add(string.Format("Fs3 = Lateral Force = {0:f3} x {1:f3}^2 = {2:f3} kN", W3, H3, Fs3));
            list.Add(string.Format(""));
            list.Add(string.Format("H4 = Tower Height at base Level of Crown = {0:f3} m.", H4));
            list.Add(string.Format("W4 = Weight at base Level of Crown = {0:f3} kN", W4));
            double Fs4 = W4 * H4 * H4;
            list.Add(string.Format("Fs4 = Lateral Force = {0:f3} x {1:f3}^2 = {2:f3} kN", W4, H4, Fs4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Fs = Fs1 + Fs2 + Fs3 + Fs4;

            list.Add(string.Format("Fs = Total Lateral Force"));
            list.Add(string.Format("   = Fs1 + Fs2 + Fs3 + Fs4 "));
            list.Add(string.Format("   = {0:f3} + {1:f3} + {2:f3} + {3:f3} ", Fs1, Fs2, Fs3, Fs4));
            list.Add(string.Format("   = {0:f3} kN", Fs));
            list.Add(string.Format(""));



            Q1 = Vb * Fs1 / Fs;
            Q2 = Vb * Fs2 / Fs;
            Q3 = Vb * Fs3 / Fs;
            Q4 = Vb * Fs4 / Fs;

            list.Add(string.Format("Q1 = Vb x Fs1 / Fs = {0:f2} x {1:f3} / {2:f3} = {3:f3} kN        FX or FZ to be applied at Level1 (Wing Bottom)", Vb, Fs1, Fs, Q1));
            list.Add(string.Format("Q2 = Vb x Fs2 / Fs = {0:f2} x {1:f3} / {2:f3} = {3:f3} kN        FX or FZ to be applied at Level2 (Wing Bottom)", Vb, Fs2, Fs, Q2));
            list.Add(string.Format("Q3 = Vb x Fs3 / Fs = {0:f2} x {1:f3} / {2:f3} = {3:f3} kN        FX or FZ to be applied at Level3 (Wing Bottom)", Vb, Fs3, Fs, Q3));
            list.Add(string.Format("Q4 = Vb x Fs4 / Fs = {0:f2} x {1:f3} / {2:f3} = {3:f3} kN        FX or FZ to be applied at Level4 (Crown Bottom)", Vb, Fs4, Fs, Q4));
            list.Add(string.Format(""));
            return list;
        }
    }
    [Serializable]
    public enum eTowerMemberType
    {

        NoSelection = -1,

        CROWN_MAINPOST = 0,
        CROWN_BRACINGS = 1,
        LEVEL1_MAINPOST = 2,
        LEVEL1_LOWER_CROSS_BRACINGS = 3,
        LEVEL1_UPPER_CROSS_BRACINGS = 4,
        LEVEL1_INTERNAL_LOWER_BRACINGS = 5,
        LEVEL1_INTERNAL_UPPER_BRACINGS = 6,
        LEVEL1_BOX = 7,
        LEVEL1_WING_BRACINGS = 8,
        LEVEL1_LEFT_WING_TOP = 9,
        LEVEL1_LEFT_WING_BOTTOM = 10,
        LEVEL1_LEFT_WING_SIDE_BRACINGS = 11,
        LEVEL1_LEFT_WING_TOP_BRACINGS = 12,
        LEVEL1_LEFT_WING_BOTTOM_BRACINGS = 13,

        LEVEL1_RIGHT_WING_TOP = 14,
        LEVEL1_RIGHT_WING_BOTTOM = 15,
        LEVEL1_RIGHT_WING_SIDE_BRACINGS = 16,
        LEVEL1_RIGHT_WING_TOP_BRACINGS = 17,
        LEVEL1_RIGHT_WING_BOTTOM_BRACINGS = 18,

        LEVEL2_MAINPOST = 19,
        LEVEL2_LOWER_CROSS_BRACINGS = 20,
        LEVEL2_UPPER_CROSS_BRACINGS = 21,
        LEVEL2_INTERNAL_LOWER_BRACINGS = 22,
        LEVEL2_INTERNAL_UPPER_BRACINGS = 23,
        LEVEL2_BOX = 24,
        LEVEL2_WING_BRACINGS = 25,

        LEVEL2_LEFT_WING_TOP = 26,
        LEVEL2_LEFT_WING_BOTTOM = 27,
        LEVEL2_LEFT_WING_SIDE_BRACINGS = 28,
        LEVEL2_LEFT_WING_TOP_BRACINGS = 29,
        LEVEL2_LEFT_WING_BOTTOM_BRACINGS = 30,

        LEVEL2_RIGHT_WING_TOP = 31,
        LEVEL2_RIGHT_WING_BOTTOM = 32,
        LEVEL2_RIGHT_WING_SIDE_BRACINGS = 33,
        LEVEL2_RIGHT_WING_TOP_BRACINGS = 34,
        LEVEL2_RIGHT_WING_BOTTOM_BRACINGS = 35,



        LEVEL3_MAINPOST = 36,
        LEVEL3_LOWER_CROSS_BRACINGS = 37,
        LEVEL3_UPPER_CROSS_BRACINGS = 38,
        LEVEL3_INTERNAL_LOWER_BRACINGS = 39,
        LEVEL3_INTERNAL_UPPER_BRACINGS = 40,
        LEVEL3_BOX = 41,
        LEVEL3_WING_BRACINGS = 42,

        LEVEL3_LEFT_WING_TOP = 43,
        LEVEL3_LEFT_WING_BOTTOM = 44,
        LEVEL3_LEFT_WING_SIDE_BRACINGS = 45,
        LEVEL3_LEFT_WING_TOP_BRACINGS = 46,
        LEVEL3_LEFT_WING_BOTTOM_BRACINGS = 47,

        LEVEL3_RIGHT_WING_TOP = 48,
        LEVEL3_RIGHT_WING_BOTTOM = 49,
        LEVEL3_RIGHT_WING_SIDE_BRACINGS = 50,
        LEVEL3_RIGHT_WING_TOP_BRACINGS = 51,
        LEVEL3_RIGHT_WING_BOTTOM_BRACINGS = 52,


        //_CRW_MAINPOST
        //_CRW_BRACINGS
        //_LEV1_MAINPOST
        //_LEV1_LOWER_CROSS_BRACINGS
        //_LEV1_UPPER_CROSS_BRACINGS
        //_LEV1_INTERNAL_LOWERBRACINGS
        //_LEV1_INTERNALUPPERBRACINGS
        //_LEV1_UPPER
        //_LEV1_WING_BRACINGS
        //_LEV1_LEFT_WING_TOP
        //_LEV1_LEFT_WING_BOTTOM
        //_LEV1_LEFT_WING_SIDEBRACINGS
        //_LEV1_LEFT_WING_TOPBRACINGS
        //_LEV1_LEFTWINGBOTTOMBRACINGS
        //_LEV1_RIGHTWINGTOP
        //_LEV1_RIGHTWINGBOTTOM
        //_LEV1_RIGHTWINGSIDEBRACINGS
        //_LEV1_RIGHTWINGTOPBRACINGS
        //_LEV1RIGHTWINGBOTTOMBRACINGS

        //_LEV2_MAINPOST
        //_LEV2_LOWERCROSSBRACINGS
        //_LEV2_UPPERCROSSBRACINGS
        //_LEV2_LOWERINTERNALBRACINGS
        //_LEV2_UPPERINTERNALBRACINGS
        //_LEV2_LEFTWINGSIDEBRACINGS
        //_LEV2_LEFTWINGTOPBRACINGS
        //_LEV2_LEFTWINGBOTTOMBRACINGS
        //_LEV2_RIGHTWINGSIDEBRACINGS
        //_LEV2_WINGTOP
        //_LEV2_BOX
        //_LEV2_LEFTWINGBOTTOM
        //_LEV2_RIGHTWINGTOP
        //_LEV2_RIGHTWINGBOTTOM
        //_LEV2_RIGHTWINGTOPBRACINGS
        //_LEV2RIGHTWINGBOTTOMBRACINGS


        //_LEV3_MAINPOST
        //_LEV3_UPPER_BRACINGS
        //_LEV3_BOX
        //_LEV3RIGHTWINGBOTTOMBRACINGS
        //_LEV3_RIGHTWINGTOPBRACINGS
        //_LEV3_RIGHTWINGSIDEBRACINGS
        //_LEV3_RIGHTWINGTOP
        //_LEV3_RIGHTWINGBOTTOM
        //_LEV3LEFTWINGSIDEBRACINGS
        //_LEV3LEFTWINGTOPBRACINGS
        //_LEV3LEFTWINGBOTTOMBRACINGS
        //_LEV3LEFTWINGTOP
        //_LEV3LEFTWINGBOTTOM
        AllMember = 53,

    }

    public class TowerDesign
    {
        public TowerDesign()
        {
            Members = new TowerMembersDesign();
            DeadLoads = new TotalDeadLoad();
            //TotalSteelWeight = 0.0;
            AddWeightPercent = 24.42;
            NoOfJointsAtTrussFloor = 0;
            NoOfJointsAtTrussFloor = 0;
            IsRailBridge = false;
            //NoOfEndJointsOnBothSideAtBottomChord = 0;
            //ForceEachInsideJoints = 0.0;
            //ForceEachEndJoint = 0.0;
            Is_Live_Load = Is_Super_Imposed_Dead_Load = Is_Dead_Load = false;

        }
        public TowerMembersDesign Members { get; set; }
        public TotalDeadLoad DeadLoads { get; set; }
        public double TotalSteelWeight
        {
            get
            {
                return Members.Weight;
            }
        }
        public double AddWeightPercent { get; set; }
        public double TotalBridgeWeight
        {
            get
            {
                //return TotalSteelWeight + GussetAndLacingWeight + DeadLoads.Weight;



                //Chiranjit [2011 07 05]
                //Calculate Total Bridge weight
                double tot_wgt = 0;

                if (Is_Dead_Load)
                {
                    tot_wgt += TotalSteelWeight + GussetAndLacingWeight;
                }
                if (Is_Super_Imposed_Dead_Load)
                {
                    tot_wgt += DeadLoads.Weight;
                }
                return tot_wgt;
            }
        }
        public double GussetAndLacingWeight
        {
            get
            {
                return (TotalSteelWeight * AddWeightPercent / 100.0);
            }
        }
        //public int NoOfJointsAtTrussFloor { get; set; }
        public int NoOfInsideJointsOnBothSideAtBottomChord
        {
            get
            {
                return (NoOfJointsAtTrussFloor - NoOfEndJointsOnBothSideAtBottomChord);
            }
        }
        public int NoOfJointsAtTrussFloor { get; set; }
        public int NoOfEndJointsOnBothSideAtBottomChord
        {

            get
            {
                return 4;
            }
        }

        public double ForceEachInsideJoints
        {
            get
            {
                //return (TotalBridgeWeight / (NoOfJointsAtTrussFloor + 2.0));
                return (TotalBridgeWeight / (NoOfInsideJointsOnBothSideAtBottomChord + 2.0));
            }
        }

        public double ForceEachEndJoint
        {
            get
            {
                return ForceEachInsideJoints / 2.0;
            }

        }
        public bool IsRailBridge { get; set; }
        //Chiranjit[2011 07 05] 
        //For Defferent Loads like LL, SIDL, DL
        public bool Is_Live_Load { get; set; }
        public bool Is_Super_Imposed_Dead_Load { get; set; }
        public bool Is_Dead_Load { get; set; }

        public void ToStream(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                //if(DeadLoads
                //if (DeadLoads.Load_List.Count > 0 && DeadLoads.IsRailLoad == false)
                //{
                //    sw.WriteLine();
                //    sw.WriteLine("---------------------------------------------");
                //    sw.WriteLine("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD");
                //    sw.WriteLine("---------------------------------------------");
                //    sw.WriteLine();
                //    DeadLoads.ToStream(sw);
                //}
                //if (DeadLoads.IsRailLoad)
                //{
                //    DeadLoads.ToStream(sw);
                //}
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------");
                sw.WriteLine("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD");
                sw.WriteLine("---------------------------------------------");
                sw.WriteLine();
                Members.ToStream(sw);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Weight of Structural Steel  = {0:f3} kN = {1:f3} MTon",
                    TotalSteelWeight,
                    (TotalSteelWeight / 10.0),
                    (TotalSteelWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc  = {1:f3} kN = {2:f3} MTon",
                    AddWeightPercent,
                    GussetAndLacingWeight,
                    (GussetAndLacingWeight / 10.0),
                    (GussetAndLacingWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine("Total Weight  = {0:f3} + {1:f3} = {2:f3} MTon",
                    (TotalSteelWeight / 10.0), (GussetAndLacingWeight / 10.0),
                    ((TotalSteelWeight / 10.0) + (GussetAndLacingWeight / 10.0)));

                sw.WriteLine();
                //sw.WriteLine("Weight of Deck Slab + S.I.D.L  = {0:f3} kN = {1:f3} MTon",
                //    DeadLoads.Weight,
                //    (DeadLoads.Weight / 10.0),
                //    (DeadLoads.Weight / 0.00981));
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN = {4:f3} MTon",
                //    (Is_Dead_Load) ? TotalSteelWeight : 0.0,
                //    (Is_Dead_Load) ? GussetAndLacingWeight : 0.0,
                //    (Is_Super_Imposed_Dead_Load) ? DeadLoads.Weight : 0.0,
                //    TotalBridgeWeight,
                //    (TotalBridgeWeight / 10.0),
                //    (TotalBridgeWeight / 0.00981));
                //sw.WriteLine();
                //sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
                //sw.WriteLine();
                //sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
                //sw.WriteLine();
                //sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                //    TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
                //sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);
            }
            catch (Exception ex) { }
        }
        public void WriteGroupSummery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteGroupSummery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void WriteGroupSummery(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("==============================================================");
            sw.WriteLine("                        DESIGN SECTION SUMMARY");
            sw.WriteLine("==============================================================");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            string kStr = "";
            string format = "";
            format = "{0,-9} {1,-21} {2,-8} {3,-8} {4,-8} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:e2} {13,10:e2}  {14,-7}";


            //sw.WriteLine(format, "      ", "      ", "     ", "     ", "      ", " Vertical", "  Member  ", " Capacity  ", " Member", "Capacity", " Member", "Capacity", "Member", "Required", "");
            //sw.WriteLine(format, "Member", "      ", " Top ", "Side ", "Bottom", "Stiffener", "Compressive", "Compressive", "Tensile ", "Tensile ", " Shear  ", " Shear  ", "Section ", "Secion  ", "Remarks");
            //sw.WriteLine(format, "Group", "Section", "Plate", "Plate", "Plate ", "  Plate  ", "  Force    ", "   Force   ", " Force  ", " Force  ", " Stress ", " Stress ", "Modulus ", "Modulus ", "       ");
            //sw.WriteLine(format, "-----", "-------", "-----", "-----", "------", "---------", "-----------", "-----------", "--------", "--------", "--------", "--------", "--------", "--------", "-------");

            //Chiranjit [2013 08 16]  Check Comp force to Comp Stress
            sw.WriteLine(format, "      ", "      ", "     ", "     ", "      ", " Vertical", "  Member  ", " Capacity  ", " Member", "Capacity", " Member", "Capacity", "Member", "Required", "");
            sw.WriteLine(format, "Member", "      ", " Top ", "Side ", "Bottom", "Stiffener", "Compressive", "Compressive", "Tensile ", "Tensile ", " Shear  ", " Shear  ", "Section ", "Secion  ", "Remarks");
            sw.WriteLine(format, "Group", "Section", "Plate", "Plate", "Plate ", "  Plate  ", "  Stress   ", "   Stress  ", " Force  ", " Force  ", " Stress ", " Stress ", "Modulus ", "Modulus ", "       ");
            sw.WriteLine(format, "-----", "-------", "-----", "-----", "------", "---------", "-----------", "-----------", "--------", "--------", "--------", "--------", "--------", "--------", "-------");



            bool flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomChord)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("         ASTRA Pro :    SUMMARY OF BOTTOM CHORDS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce.Force,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce.Force,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomChordBracings)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("            ASTRA Pro :   SUMMARY OF BOTTOM CHORD BRACINGS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.CantileverBrackets)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("          ASTRA Pro :    SUMMARY OF CANTILEVER BRACKETS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                         item.Group.GroupName,
                         item.SectionDetails.ToString(),
                         item.SectionDetails.TopPlate.ToString(),
                         item.SectionDetails.SidePlate.ToString(),
                         item.SectionDetails.BottomPlate.ToString(),
                         item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                         item.MaxTensionForce,
                         item.Capacity_TensionForce,
                         item.Required_ShearStress,
                         item.Capacity_ShearStress,
                         item.Capacity_SectionModulus,
                         item.Required_SectionModulus,
                         item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }

            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.CrossGirder)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("          ASTRA Pro :    SUMMARY OF CROSS GIRDERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }


            #region Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.DiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Diagonal Members

            #region Top Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopDiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF TOP DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Top Diagonal Members


            #region BOTTOM Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomDiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF BOTTOM DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion BOTTOM Diagonal Members


            #region Short Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.ShortDiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF SHORT DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Short Diagonal Members

            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.EndRakers)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("          ASTRA Pro :     SUMMARY OF END RAKERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.StringerBeam)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("        ASTRA Pro :      SUMMARY OF STRINGER BEAMS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopChord)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("        ASTRA Pro :    SUMMARY OF TOP CHORDS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                         item.Group.GroupName,
                         item.SectionDetails.ToString(),
                         item.SectionDetails.TopPlate.ToString(),
                         item.SectionDetails.SidePlate.ToString(),
                         item.SectionDetails.BottomPlate.ToString(),
                         item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                         item.MaxTensionForce,
                         item.Capacity_TensionForce,
                         item.Required_ShearStress,
                         item.Capacity_ShearStress,
                         item.Capacity_SectionModulus,
                         item.Required_SectionModulus,
                         item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopChordBracings)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("         ASTRA Pro :    SUMMARY OF TOP CHORD BRACINGS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.VerticalMember)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("         ASTRA Pro :    SUMMARY OF VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }



            #region TOP VERTICAL Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopVerticalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF TOP VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Top VERTICAL Members


            #region BOTTOM VERTICAL Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomVerticalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF BOTTOM VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion BOTTOM VERTICAL Members



            #region SHORT VERTICAL Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.ShortVerticalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF SHORT VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion SHORT VERTICAL Members



            sw.WriteLine();
            sw.WriteLine("-----------------------------------------------------------------------------");
            sw.WriteLine("            ASTRA Pro :   SUMMARY OF WEIGHTS");
            sw.WriteLine("-----------------------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Total Weight of Structural Steel = {0:f3} kN  = {1:f3} MTon", TotalSteelWeight, (TotalSteelWeight / 10.0));
            sw.WriteLine();
            sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc = {1:f3} kN  = {2:f3} MTon", AddWeightPercent, GussetAndLacingWeight, GussetAndLacingWeight / 10.0);
            sw.WriteLine();
            sw.WriteLine("Weight of Deck Slab + Super Imposed Dead Load = {0:f3} kN  = {1:f3} MTon", DeadLoads.Weight, DeadLoads.Weight / 10.0);
            sw.WriteLine();
            sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN  = {4:f3} MTon",
                TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, TotalBridgeWeight / 10.0);
            sw.WriteLine();
            sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
            sw.WriteLine();
            sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
            sw.WriteLine();
            sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
            sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("==============================================================");
            sw.WriteLine("                      END DESIGN SECTION SUMMARY");
            sw.WriteLine("==============================================================");
            sw.WriteLine();

        }
        public void WriteForcesSummery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteForcesSummery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }
        public void WriteForcesSummery(StreamWriter sw)
        {
            //StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                        DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
                sw.WriteLine();

                string DL_SIDL_LL = "";

                DL_SIDL_LL = Is_Dead_Load ? "DL " : "";
                DL_SIDL_LL += Is_Super_Imposed_Dead_Load ? Is_Dead_Load ? " + SIDL " : "SIDL" : "";
                DL_SIDL_LL += Is_Live_Load ? (Is_Dead_Load || Is_Super_Imposed_Dead_Load) ? " + LL " : "LL" : "";

                sw.WriteLine("Applied force and Check for member section for " + DL_SIDL_LL);
                sw.WriteLine();
                string kStr = "";
                string format = "";

                //format = "{0,-18} {1,-25} {2,-15} {3,-15} {4,-15} {5,-20} {6,20:f2} {7,20:f2} {8,20:f2} {9,20:f2} {10,20:f2} {11,20:f2} {12,20:f2} {13,20:f2} {14,7}";
                //format = "{0,-18} {1,-25} {2,-10} {3,-10} {4,-10} {5,-15} {6,15:f2} {7,15:f2} {8,15:f2} {9,15:f2} {10,10:f2} {11,10:f2} {12,15:f2} {13,15:f2}   {14,-7}";
                //format = "{0,-9} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}   {14,-7}";



                format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,15:f3} {7,15:f3} {8,10:f3}";
                //      0             1                 2                  3                 4           5                6                   7              8
                //MemberGroup    TensionForce    CaompressionForce    TensileStress   CapTensileStress Result     CompressiveStress   CapCompressiveStress Result  
                sw.WriteLine(format,
                    "Member",
                    "Tension",
                    "Compression",
                    "Tensile",
                    "Capacity",
                    "",
                    "Compressive",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Force",
                   "Force",
                   "Stress",
                   "Tensile",
                   "Result",
                   "Stress",
                   "Compression",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN)",
                                    " (kN)",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");

                sw.WriteLine();
                bool flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORDS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                            item.Group.GroupName,
                            item.MaxTensionForce.Force,
                            item.MaxCompForce.Force,
                            item.Tensile_Stress,
                            item.Capacity_Tensile_Stress,
                            item.Result_Tensile_Stress,
                            item.Compressive_Stress,
                            item.Capacity_Compressive_Stress,
                            item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChordBracings)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORD BRACINGS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CantileverBrackets)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CANTILEVER BRACKETS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.DiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF DIAGONAL MEMBERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.EndRakers)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF END RAKERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORDS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChordBracings)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORD BRACINGS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.VerticalMember)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF VERTICAL MEMBERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                format = "{0,-10} {1,10:f3} {2,10:f3} {3,15:e3} {4,15:e3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("                                 BEAM MEMBERS  ");
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine(format,
                    "Member",
                    "Bending",
                    "Shear",
                    "Section",
                    "Capacity",
                    "",
                    "Shear",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Moment",
                   "Force",
                   "Modulas",
                   "Section",
                   "Result",
                   "Stress",
                   "Shear",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN-m)",
                                    " (kN)",
                                    " (cu.mm)",
                                    " (cu.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.StringerBeam)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF STRINGER BEAMS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxBendingMoment.Force,
                           item.MaxShearForce.Force,
                           item.Required_SectionModulus,
                           item.Capacity_SectionModulus,
                           item.Result_Section_Modulas,
                           item.Capacity_ShearStress,
                           item.Required_ShearStress,
                           item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CrossGirder)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CROSS GIRDERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                          item.Group.GroupName,
                          item.MaxBendingMoment.Force,
                          item.MaxShearForce.Force,
                          item.Required_SectionModulus,
                          item.Capacity_SectionModulus,
                          item.Result_Section_Modulas,
                          item.Capacity_ShearStress,
                          item.Required_ShearStress,
                          item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                //sw.WriteLine();
                //sw.WriteLine("-----------------------------------------------------------------------------");
                //sw.WriteLine("                        SUMMARY OF WEIGHTS");
                //sw.WriteLine("-----------------------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Structural Steel = {0:f3} kN  = {1:f3} MTon", TotalSteelWeight, (TotalSteelWeight / 10.0));
                //sw.WriteLine();
                //sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc = {1:f3} kN  = {2:f3} MTon", AddWeightPercent, GussetAndLacingWeight, GussetAndLacingWeight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("Weight of Deck Slab + Super Imposed Dead Load = {0:f3} kN  = {1:f3} MTon", DeadLoads.Weight, DeadLoads.Weight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN  = {4:f3} MTon",
                //    TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, TotalBridgeWeight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
                //sw.WriteLine();
                //sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
                //sw.WriteLine();
                //sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                //    TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
                //sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);

                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                      END DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
            }
            catch (Exception ex) { }
            finally
            {
                //sw.Flush();
                //sw.Close();
            }


        }
        public void WriteForces_Capacity_Summery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteForces_Capacity_Summery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }
        public void WriteForces_Capacity_Summery(StreamWriter sw)
        {
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                        DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
                sw.WriteLine();

                string DL_SIDL_LL = "";

                DL_SIDL_LL = Is_Dead_Load ? "DL " : "";
                DL_SIDL_LL += Is_Super_Imposed_Dead_Load ? Is_Dead_Load ? " + SIDL " : "SIDL" : "";
                DL_SIDL_LL += Is_Live_Load ? (Is_Dead_Load || Is_Super_Imposed_Dead_Load) ? " + LL " : "LL" : "";

                sw.WriteLine("Applied force and Check for member section for " + DL_SIDL_LL);
                sw.WriteLine();
                string kStr = "";
                string format = "";



                //format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,15:f3} {7,15:f3} {8,10:f3} {9,10:f3} {10,10:f3}";
                format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,15:f3} {5,15:f3} {6,5:f3}";

                sw.WriteLine("");
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("Member    Compressive     Compressive     Compressive   Tensile     Tensile       Tensile");
                sw.WriteLine("Group        Force       Force Capacity      Result      Force   Force Capacity    Result");
                sw.WriteLine("             (kN)             (kN)                       (kN)         (kN)      ");
                sw.WriteLine("-------------------------------------------------------------------------------------------");

                bool flag = true;

                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORDS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChordBracings)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORD BRACINGS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CantileverBrackets)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CANTILEVER BRACKETS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                                item.Group.GroupName,


                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.DiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }
                #region Short Diagonal

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.ShortDiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF SHORT DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion Short Diagonal


                #region TOP Diagonal

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopDiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion TOP Diagonal



                #region BOTTOM Diagonal

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomDiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion BOTTOM Diagonal


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.EndRakers)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF END RAKERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORDS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChordBracings)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORD BRACINGS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.VerticalMember)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                       item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }

                #region TOP VERTICAL

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopVerticalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion TOP VERTICAL



                #region BOTTOM VERTICAL

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomVerticalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion BOTTOM VERTICAL

                #region SHORT VERTICAL

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.ShortVerticalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF SHORT VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion SHORT VERTICAL


                format = "{0,-10} {1,10:f3} {2,10:f3} {3,15:e3} {4,15:e3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("                                 BEAM MEMBERS  ");
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine(format,
                    "Member",
                    "Bending",
                    "Shear",
                    "Section",
                    "Capacity",
                    "",
                    "Shear",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Moment",
                   "Force",
                   "Modulas",
                   "Section",
                   "Result",
                   "Stress",
                   "Shear",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN-m)",
                                    " (kN)",
                                    " (cu.mm)",
                                    " (cu.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.StringerBeam)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF STRINGER BEAMS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxBendingMoment.Force,
                           item.MaxShearForce.Force,
                           item.Required_SectionModulus,
                           item.Capacity_SectionModulus,
                           item.Result_Section_Modulas,
                           item.Capacity_ShearStress,
                           item.Required_ShearStress,
                           item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CrossGirder)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CROSS GIRDERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                          item.Group.GroupName,
                          item.MaxBendingMoment.Force,
                          item.MaxShearForce.Force,
                          item.Required_SectionModulus,
                          item.Capacity_SectionModulus,
                          item.Result_Section_Modulas,
                          item.Capacity_ShearStress,
                          item.Required_ShearStress,
                          item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                      END DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
            }
            catch (Exception ex) { }
            finally
            {
                //sw.Flush();
                //sw.Close();
            }

        }

        public void ReadFromFile(string file_name)
        {
            //this.DeadLoads.ReadFromFile(file_name);
            this.Members.ReadFromFile(file_name);
        }
    }

    public class TowerMembersDesign : IList<CMember>
    {
        List<CMember> list = null;

        public TowerMembersDesign()
        {
            list = new List<CMember>();
        }

        #region IList<Member> Members

        public int IndexOf(string MemberNo)
        {


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Group.GroupName == MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(CMember item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Group.GroupName == item.Group.GroupName))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, CMember item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public CMember this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<Member> Members

        public void Add(CMember item)
        {
            try
            {
                if (IndexOf(item) != -1) throw new Exception("Member Group. " + item.Group.GroupName + " already exist.");
                if (item.Group.GroupName == "") throw new Exception("Invalid Member Information");
                list.Add(item);
            }
            catch (Exception ex) { }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CMember item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(CMember[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CMember item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Member> Members

        public IEnumerator<CMember> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        public void ReadFromFile(string file_name)
        {
            List<string> file_con = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyList mLIst = null;
            CMember mem = null;
            int index = 0;
            bool flag = false;
            bool flag2 = false;
            eMemberType memType = eMemberType.NoSelection;

            for (int i = 0; i < file_con.Count; i++)
            {
                kStr = file_con[i];

                if (kStr == "") continue;
                if (kStr.StartsWith("-----")) continue;

                //if (i >= 56)
                //{
                //    kStr = file_con[i];
                //}

                //list[6].Capacity_ShearStress;
                kStr = MyList.RemoveAllSpaces(kStr.ToUpper());


                if (kStr == "") continue;
                mLIst = new MyList(kStr, ' ');
                //1      Section1     ISJB 150                              1.0000                1.0000     1.0000    
                //                    T PL 500 x 22   500        22         0.8628     1         

                //2      Section1     ISJB 150                              1.0000                1.0000     1.0000    
                //                    T PL 500 x 22   500        22         0.8628     1         

                //3      Section4     ISA 2020 x 3                          1.0000                1.0000     1.0000    
                //                    T PL 350 x 25   350        25         0.6864     1         
                //                    S PL 420 x 16   420        16         0.5271     2         
                //                    VS PL 120 x 25  120        25         0.2353     2         
                //                    S PL 320 x 10   320        10         0.2510    

                try
                {
                    if (mLIst.StringList[0].StartsWith("MEMBER"))
                    {
                        flag2 = true;
                        continue;
                    }
                    if (!flag2) continue;
                    if (mLIst.Count == 2 || mLIst.Count == 3)
                    {
                        kStr = kStr.ToUpper();
                        #region Selection Member
                        if (kStr == "TOP CHORD")
                        {
                            memType = eMemberType.TopChord; continue;
                        }
                        else if (kStr == "BOTTOM CHORD")
                        {
                            memType = eMemberType.BottomChord; continue;
                        }
                        else if (kStr == "STRINGER BEAM")
                        {
                            memType = eMemberType.StringerBeam;
                        }
                        else if (kStr == "CROSS GIRDER")
                        {
                            memType = eMemberType.CrossGirder;
                        }
                        else if (kStr == "END RAKERS")
                        {
                            memType = eMemberType.EndRakers;
                        }
                        else if (kStr == "DIAGONAL MEMBER")
                        {
                            memType = eMemberType.DiagonalMember;
                        }
                        else if (kStr == "SHORT DIAGONAL MEMBER")
                        {
                            memType = eMemberType.DiagonalMember;
                        }
                        else if (kStr == "TOP DIAGONAL MEMBER")
                        {
                            memType = eMemberType.TopDiagonalMember;
                        }
                        else if (kStr == "BOTTOM DIAGONAL MEMBER")
                        {
                            memType = eMemberType.BottomDiagonalMember;
                        }
                        else if (kStr == "VERTICAL MEMBER")
                        {
                            memType = eMemberType.VerticalMember;
                        }
                        else if (kStr == "TOP VERTICAL MEMBER")
                        {
                            memType = eMemberType.TopVerticalMember;
                        }
                        else if (kStr == "BOTTOM VERTICAL MEMBER")
                        {
                            memType = eMemberType.BottomVerticalMember;
                        }
                        else if (kStr == "SHORT VERTICAL MEMBER")
                        {
                            memType = eMemberType.ShortVerticalMember;
                        }
                        else if (kStr == "TOP CHORD BRACINGS")
                        {
                            memType = eMemberType.TopChordBracings;
                        }
                        else if (kStr == "BOTTOM CHORD BRACINGS")
                        {
                            memType = eMemberType.BottomChordBracings;
                        }
                        else if (kStr == "CANTILEVER BRACKETS")
                        {
                            memType = eMemberType.CantileverBrackets;
                        }
                        //Chiranjit [2014 01 22]
                        else if (kStr == "ARCH CABLE MEMBERS")
                        {
                            memType = eMemberType.ArchMembers;
                        }
                        else if (kStr == "ARCH MEMBERS")
                        {
                            memType = eMemberType.ArchMembers;
                        }
                        else if (kStr == "SUSPENSION CABLE MEMBERS")
                        {
                            memType = eMemberType.SuspensionCables;
                        }
                        else if (kStr == "TRANSVERS MEMBERS")
                        {
                            memType = eMemberType.TransverseMember;
                        }
                        #endregion Selection Member
                    }
                    else if (mLIst.Count == 13 && (mLIst.StringList[2] == "UKA" || mLIst.StringList[2] == "ISA"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++; index++;
                        mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                        mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                        //mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                        mem.Length = mLIst.GetDouble(index); index++;
                        //mem.Weight = mLIst.GetDouble(index); index++;

                        //mem.MemberType = memType;

                        mem.MemberType = eMemberType.TowerMember;

                        flag = true;
                    }
                }
                catch (Exception ex) { }
            }
            if (flag)
            {
                Add(mem);
                //flag = false;
            }
        }
        public void ToStream(StreamWriter sw)
        {
            string title_line1, title_line2;
            //StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
                title_line1 = string.Format("{0, -36} {1, -12} {2, -15} {3, 0} {4, 0} {5, -10} {6, -10} {7, -10} {8, 0} {9, 6} {10, 10} {11, 10} {12, 10}",
                    "Member", //0
                    "Section",//1
                    "",//2
                    "",//3
                    "",//4
                    "Unit",//5
                    "No Of",//6
                    "No Of",//7
                    "",//8
                    "Bolt",//9
                    "No Of",//10
                    "Member",//11
                    "Member");//12
                sw.WriteLine(title_line1);

                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10} {7, -10} {8, -10} {9, -10}",
                title_line2 = string.Format("{0, -36} {1, -12} {2, -15} {3, 0} {4, 0} {5, -10} {6, -10} {7, -10} {8, 0} {9, 6} {10, 8} {11, 10} {12, 10}",
                    "Group", //0
                    "Type",//1
                    "Element",//2
                    "",//3
                    "",//4
                    "Weight",//5
                    "Elements",//6
                    "Members",//7
                    "",//8
                    "Diameter",//9
                    "Bolt",//10
                    "Length",//11
                    "Weight");//12
                sw.WriteLine(title_line2);

                title_line2 = string.Format("{0, -36} {1, -12} {2, -15} {3, 0} {4, 0} {5, -10} {6, -10} {7, -10} {8, 0} {9, 6} {10, 8} {11, 10} {12, 10}",
                   "", //0
                   "",//1
                   "",//2
                   "",//3
                   "",//4
                   "(kN/m)",//5
                   "",//6
                   "",//7
                   "",//8
                   " (mm) ",//9
                   "",//10
                   " (m) ",//11
                   "(kN)");//12
                sw.WriteLine(title_line2);
                sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine();

                int i = 0;
                double total_weight, weight;

                total_weight = 0.0;
                weight = 0.0;
                bool print_flag = true;

                #region TOWER MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TowerMember)
                    {
                        if (print_flag)
                        {
                            //sw.WriteLine("CANTILEVER BRACKETS");
                            //sw.WriteLine("-------------------");
                            print_flag = false;
                        }
                        list[i].ToStreamTower(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,121}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TOWER MEMBER


                sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}

        }

        public eDefineSection GetDefineSection(string s)
        {
            eDefineSection ds = eDefineSection.NoSelection;
            switch (s.ToUpper())
            {
                case "SECTION1":
                    ds = eDefineSection.Section1;
                    break;
                case "SECTION2":
                    ds = eDefineSection.Section2;
                    break;
                case "SECTION3":
                    ds = eDefineSection.Section3;
                    break;
                case "SECTION4":
                    ds = eDefineSection.Section4;
                    break;
                case "SECTION5":
                    ds = eDefineSection.Section5;
                    break;
                case "SECTION6":
                    ds = eDefineSection.Section6;
                    break;
                case "SECTION7":
                    ds = eDefineSection.Section7;
                    break;
                case "SECTION8":
                    ds = eDefineSection.Section8;
                    break;
                case "SECTION9":
                    ds = eDefineSection.Section9;
                    break;
                case "SECTION10":
                    ds = eDefineSection.Section10;
                    break;
                case "SECTION11":
                    ds = eDefineSection.Section11;
                    break;
                case "SECTION12":
                    ds = eDefineSection.Section12;
                    break;
                case "SECTION13":
                    ds = eDefineSection.Section13;
                    break;
                case "SECTION14":
                    ds = eDefineSection.Section14;
                    break;
                case "SECTION15":
                    ds = eDefineSection.Section15;
                    break;
                case "SECTION16":
                    ds = eDefineSection.Section16;
                    break;
                case "SECTION17":
                    ds = eDefineSection.Section17;
                    break;
                case "SECTION18":
                    //ds = eDefineSection.Section15;
                    break;
            }
            return ds;
        }
        public eMemberType GetMemberType(string s)
        {
            eMemberType ds = eMemberType.NoSelection;
            if (s.ToUpper() == eMemberType.BottomChord.ToString().ToUpper())
                ds = eMemberType.BottomChord;
            else if (s.ToUpper() == eMemberType.BottomChordBracings.ToString().ToUpper())
                ds = eMemberType.BottomChordBracings;
            else if (s.ToUpper() == eMemberType.CantileverBrackets.ToString().ToUpper())
                ds = eMemberType.CantileverBrackets;
            else if (s.ToUpper() == eMemberType.CrossGirder.ToString().ToUpper())
                ds = eMemberType.CrossGirder;
            else if (s.ToUpper() == eMemberType.DiagonalMember.ToString().ToUpper())
                ds = eMemberType.DiagonalMember;
            else if (s.ToUpper() == eMemberType.EndRakers.ToString().ToUpper())
                ds = eMemberType.EndRakers;
            else if (s.ToUpper() == eMemberType.StringerBeam.ToString().ToUpper())
                ds = eMemberType.StringerBeam;
            else if (s.ToUpper() == eMemberType.TopChord.ToString().ToUpper())
                ds = eMemberType.TopChord;
            else if (s.ToUpper() == eMemberType.TopChordBracings.ToString().ToUpper())
                ds = eMemberType.TopChordBracings;
            else if (s.ToUpper() == eMemberType.VerticalMember.ToString().ToUpper())
                ds = eMemberType.VerticalMember;

            return ds;

        }
        //public void Serialise(string file_name)
        //{
        //    Stream stream = new FileStream(file_name, FileMode.Create);
        //    BinaryFormatter bfor = new BinaryFormatter();
        //    bfor.Serialize(stream, this);
        //}
        //public static MembersDesign DeSerialise(string file_name)
        //{
        //    MembersDesign cmp_des = null;

        //    Stream stream = new FileStream(file_name, FileMode.Open);
        //    try
        //    {
        //        BinaryFormatter bfor = new BinaryFormatter();
        //        cmp_des = (MembersDesign)bfor.Deserialize(stream);
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        stream.Close();
        //    }
        //    return cmp_des;
        //}

        public double Weight
        {
            get
            {
                double wgt = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    wgt += list[i].Weight;
                }
                return wgt;
            }
        }

        public CMember Get_Member(string group_name)
        {
            foreach (var item in this)
            {
                if (item.Group.GroupName.ToUpper() == group_name)
                {
                    return item;
                }
            }
            return null;
        }
    }

}
