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
    public partial class frm_StructuralDesign : Form
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

        public vdDocument VDoc
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }
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

        public frm_StructuralDesign(string input_file)
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

        public frm_StructuralDesign(string drawing_file, bool IsDrawingFile)
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


        public frm_StructuralDesign(IApplication iapp)
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

        public void SetGridWithNode()
        {
            string kStr = "";
            int indx = -1;
            dgv_joints.Rows.Clear();

            for (int i = 0; i < AST_DOC.Joints.Count; i++)
            {
                indx = AST_DOC.Supports.IndexOf(AST_DOC.Joints[i].NodeNo);
                kStr = "";
                if (indx != -1)
                {
                    kStr = AST_DOC.Supports[indx].Option.ToString();
                }

                dgv_joints.Rows.Add(AST_DOC.Joints[i].NodeNo,
                    AST_DOC.Joints[i].Point.x.ToString("f4"),
                    AST_DOC.Joints[i].Point.y.ToString("f4"),
                    AST_DOC.Joints[i].Point.z.ToString("f4"), kStr);
            }

            //ACad.Create_Data
        }
        private void SetGridWithMember()
        {
            dgv_members.Rows.Clear();

            foreach (var item in AST_DOC.Members)
            {
                try
                {
                    if (item.Property == null)
                        item.Property = new MemberProperty();

                    dgv_members.Rows.Add(item.MemberNo, item.MemberType,
                        item.StartNode.NodeNo,
                        item.EndNode.NodeNo,
                        item.Property.YD,
                        item.Property.ZD,
                        item.Property.Area,
                        item.Property.IX,
                        item.Property.IY,
                        item.Property.IZ,
                        item.Property.E,
                        item.Property.DEN,
                        item.Property.PR,
                        item.Length.ToString("f3")
                        );
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void SetGridWithElement()
        {
            dgv_elements.Rows.Clear();
            foreach (var item in AST_DOC.Elements)
            {
                try
                {
                    dgv_elements.Rows.Add(item.ElementNo,
                        item.Node1.NodeNo,
                        item.Node2.NodeNo,
                        item.Node3.NodeNo,
                        item.Node4.NodeNo,
                        item.ThickNess.Replace("TH: ", "")
                        );
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Tab_Selection()
        {
            sc2.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);
            sc1.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);

            if (tc_prp_main.SelectedTab == tab_geom)
                Show_Panel(tab1_geom);
            else if (tc_prp_main.SelectedTab == tab_props)
                Show_Panel(tab1_props);
            else if (tc_prp_main.SelectedTab == tab_constants)
                Show_Panel(tab1_const);
            else if (tc_prp_main.SelectedTab == tab_supports)
                Show_Panel(tab1_supports);
            else if (tc_prp_main.SelectedTab == tab_loads)
                Show_Panel(tab1_loads);
            else if (tc_prp_main.SelectedTab == tab_mov_load)
                Show_Panel(tab1_moving);
            else if (tc_prp_main.SelectedTab == tab_dynamic)
                Show_Panel(tab1_dynamic);
            else if (tc_prp_main.SelectedTab == tab_ana_spec)
                Show_Panel(tab1_ana_spec);
        }
        public void Show_Panel(TabPage tp)
        {
            tc_prp_panel.TabPages.Clear();
            tc_prp_panel.TabPages.Add(tp);
        }

        public void ShowMemberOnGrid(vdLine vLine)
        {
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                if (AST_DOC.Members[i].StartNode.Point == vLine.StartPoint &&
                    AST_DOC.Members[i].EndNode.Point == vLine.EndPoint)
                {

                    DataGridView dgv = dgv_members;

                    vdDocument VD = ActiveDoc;

                    //if (VD == VDoc) dgv = dgv_members;
                    if (VD == maxDoc) dgv = dgv_max_frc;


                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        int memNo = (int)dgv[0, j].Value;

                        if (memNo == AST_DOC.Members[i].MemberNo)
                        {
                            ClearSelect(dgv);
                            dgv.Rows[j].Selected = true;
                            dgv.FirstDisplayedScrollingRowIndex = j;
                            AST_DOC.Members.ShowMember(j, VD, 0.03d);
                            tc3.SelectedTab = tab2_members;
                            return;
                        }
                    }
                }
            }
        }
        public void ShowNodeOnGrid(vdText vtxt)
        {

            if (!vtxt.Layer.Name.ToUpper().StartsWith("NODE")) return;
            int jNo = MyStrings.StringToInt(vtxt.TextString, -1);
            if (jNo == -1) return;

            int jntNo = 0;


            for (int j = 0; j < dgv_joints.Rows.Count; j++)
            {
                jntNo = (int)dgv_joints[0, j].Value;

                if (jntNo == jNo)
                {
                    ClearSelect(dgv_joints);
                    dgv_joints.Rows[j].Selected = true;
                    dgv_joints.FirstDisplayedScrollingRowIndex = j;
                    //ShowJ(j, VDoc, 0.03d);
                    //vtxt.
                    tc3.SelectedTab = tab2_joints;
                    return;
                }
            }
        }
        public void ShowNodeOnGrid(gPoint gp)
        {

            int jNo = 0;
            int jntNo = 0;

            for (int i = 0; i < AST_DOC.Joints.Count; i++)
            {
                if (AST_DOC.Joints[i].Point == gp)
                    jNo = AST_DOC.Joints[i].NodeNo;
            }

            for (int j = 0; j < dgv_joints.Rows.Count; j++)
            {
                jntNo = (int)dgv_joints[0, j].Value;

                if (jntNo == jNo)
                {
                    ClearSelect(dgv_joints);
                    dgv_joints.Rows[j].Selected = true;
                    dgv_joints.FirstDisplayedScrollingRowIndex = j;
                    //ShowJ(j, VDoc, 0.03d);
                    //vtxt.
                    tc3.SelectedTab = tab2_joints;
                    return;
                }
            }
        }

        public void ShowMemberOnGrid(vdText vtxt)
        {
            if (vtxt.Layer.Name.ToUpper().StartsWith("NODE"))
                ShowNodeOnGrid(vtxt);

            if (!vtxt.Layer.Name.ToUpper().StartsWith("MEMBER")) return;
            int mNo = MyStrings.StringToInt(vtxt.TextString, -1);
            if (mNo == -1) return;


            DataGridView dgv = dgv_members;

            vdDocument VD = ActiveDoc;


            for (int j = 0; j < dgv.Rows.Count; j++)
            {
                int memNo = (int)dgv[0, j].Value;

                if (memNo == mNo)
                {
                    ClearSelect(dgv);
                    dgv.Rows[j].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = j;
                    AST_DOC.Members.ShowMember(j, VD, 0.03d);
                    //vtxt.
                    tc3.SelectedTab = tab2_members;
                    return;
                }
            }
        }

        public void ShowElementOnGrid(vd3DFace _3dFace)
        {
            int elmtNo = 0;
            string ss = "";

            for (int i = 0; i < AST_DOC.Elements.Count; i++)
            {
                if (AST_DOC.Elements[i].Node1.Point == _3dFace.VertexList[0] &&
                    AST_DOC.Elements[i].Node2.Point == _3dFace.VertexList[1] &&
                    AST_DOC.Elements[i].Node3.Point == _3dFace.VertexList[2] &&
                    AST_DOC.Elements[i].Node4.Point == _3dFace.VertexList[3])
                {
                    for (int j = 0; j < dgv_members.RowCount; j++)
                    {
                        elmtNo = (int)dgv_members[0, j].Value;
                        ss = dgv_members[1, j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == AST_DOC.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            //ClearSelect();

                            dgv_members.Rows[j].Selected = true;
                            dgv_members.FirstDisplayedScrollingRowIndex = j;
                            return;
                        }

                    }
                }
            }
        }

        public void ShowElementOnGrid(vdPolyline _pline)
        {
            int elmtNo = 0;
            string ss = "";

            for (int i = 0; i < AST_DOC.Elements.Count; i++)
            {
                if (AST_DOC.Elements[i].Node1.Point == _pline.VertexList[0] &&
                    AST_DOC.Elements[i].Node2.Point == _pline.VertexList[1] &&
                    AST_DOC.Elements[i].Node3.Point == _pline.VertexList[2] &&
                    AST_DOC.Elements[i].Node4.Point == _pline.VertexList[3])
                {
                    for (int j = 0; j < dgv_members.RowCount; j++)
                    {
                        elmtNo = (int)dgv_elements[0, j].Value;
                        ss = dgv_elements[1, j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == AST_DOC.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            ClearSelect(dgv_elements);
                            tc3.SelectedTab = tab2_elements;

                            dgv_elements.Rows[j].Selected = true;
                            dgv_elements.FirstDisplayedScrollingRowIndex = j;
                            AST_DOC.Elements.ShowElement(j, VDoc, 0.09d);
                            return;
                        }

                    }
                }
            }

        }

        public void ClearSelect(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                dgv.Rows[i].Selected = false;
            }
        }
        public void ClearSelect()
        {
            ClearSelect(dgv_members);
        }
        public void SetTextSize(double txtSize)
        {
            vdDocument VD = ActiveDoc;

            if (VD == null) return;
            foreach (var item in VD.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = txtSize;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = txtSize;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    vdCircle t = item as vdCircle;
                    t.Radius = txtSize * 0.3;
                    t.Update();
                }

            }
            VD.Redraw(true);

            //for (int i = 0; i < VD.ActiveLayOut.Entities.Count; i++)
            //{
            //    vdText txt = VD.ActiveLayOut.Entities[i] as vdText;
            //    if (txt != null)
            //    {
            //        txt.Height = txtSize / 10.0d;
            //        txt.Update();
            //    }
            //}
            //VD.Redraw(true);
        }


        private void frm_ASTRA_Inputs_Load(object sender, EventArgs e)
        {
            uC_SteelSections_Beam.SetIApplication(iApp);
            uC_SteelSections_Column.SetIApplication(iApp);



            grb_slab_individual.Visible = false;
            grb_beam_individual.Visible = false;
            grb_column_individual.Visible = false;
            grb_footing_individual.Visible = false;
            grb_pile_individual.Visible = false;
            //cmb_stage.SelectedIndex = 0;

            grb_stage.Visible = (StageAnalysisForm != null);
            Set_Project_Name();

            IsCreateData = true;



            IsSavedData = true;
            tc_prp_main.TabPages.Remove(tab_mov_load);
            tc_pp_main.TabPages.Remove(tab_envelop);
            tc_pp_main.TabPages.Remove(tab_moving_load);
            //Base_Control_MouseEvent();

            sc1.Panel2Collapsed = true;
            //MyList
            for (int i = 1; i < 100; i++)
            {
                cmb_text_size.Items.Add(i.ToString());
                cmb_pp_text_size.Items.Add(i.ToString());
                cmb1_text_size.Items.Add(i.ToString());
                cmb2_text_size.Items.Add(i.ToString());
                cmb3_text_size.Items.Add(i.ToString());
                cmb4_text_size.Items.Add(i.ToString());
            }
            //MyList

            VDoc.Palette.Background = Color.White;
            chk_selfweight.Checked = false;
            cmb_Self_Weight.SelectedIndex = 1;

            cmb_structure_type.SelectedIndex = 0;
            cmb_Base_LUnit.SelectedIndex = 0;
            cmb_Base_MUnit.SelectedIndex = 1;
            cmb_Const_LUnit.SelectedIndex = 0;
            cmb_Const_MUnit.SelectedIndex = 1;
            cmb_Load_LUnit.SelectedIndex = 0;
            cmb_Load_MUnit.SelectedIndex = 1;

            cmb_Prop_LUnit.SelectedIndex = 0;


            for (int i = 0; i < tc_prp_panel.TabPages.Count; i++)
            {
                tc_prp_panel.TabPages[i].Text = "";
            }


            ACad.Document = VDoc;



            if (IsDrawingFileOpen)
            {
                if (File.Exists(Drawing_File))
                {
                    Open_Drawing_File();
                    tc_prp_main.SelectedTab = tab_geom;
                }
                else
                    sc2.Panel1Collapsed = true;
            }
            else
            {
                if (File.Exists(File_Name))
                {
                    Open_Data_File(File_Name);
                    //VDRAW.vdCommandAction.View3D_VTop(VDoc);
                }
                else
                    sc2.Panel1Collapsed = true;
                tc_prp_main.SelectedTab = tab_geom;
                tc_prp_main.SelectedTab = tab_file_open;
            }


            Button_Enable_Disable();

            Count_Geometry();

            timer1.Start();

            cmb_text_size.SelectedIndex = 0;
            cmb_pp_text_size.SelectedIndex = 0;


            cmb_slab_fck.SelectedIndex = 1;
            cmb_slab_fy.SelectedIndex = 1;
            cmb_slab_type.SelectedIndex = 1;

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

        private void Open_Drawing_File()
        {

            if (File.Exists(Drawing_File))
            {
                if (!VDoc.Open(Drawing_File))
                    MessageBox.Show(Drawing_File + " could not be opened.");

                //VDoc.Palette.Background = Color.White;


                VDoc.Palette.Background = Color.White;

                ACad.Document = VDoc;
                ACad.StructuralGeometry(VDoc);

                Clear_All();


                //Draw_Joints();
                //Draw_Members();

                SetGridWithNode();
                SetGridWithMember();

                Tab_Selection();


                cmb_structure_type.SelectedIndex = 0;
                cmb_Base_MUnit.SelectedIndex = 1;
                cmb_Base_LUnit.SelectedIndex = 0;

                cmb_Prop_LUnit.SelectedIndex = 0;


                cmb_Const_MUnit.SelectedIndex = 1;
                cmb_Const_LUnit.SelectedIndex = 0;

                cmb_Load_MUnit.SelectedIndex = 1;
                cmb_Load_LUnit.SelectedIndex = 0;






                txtUserTitle.Text = "Analysis Input Data File";

                Button_Enable_Disable();


                Draw_Joints();
                Draw_Members();
                VDRAW.vdCommandAction.View3D_VTop(VDoc);

                tc_prp_main.SelectedTab = tab_geom;

            }
        }

        private void Clear_All()
        {

            //VDoc.ActiveLayOut.Entities.EraseAll();
            VDoc.ActiveLayOut.Entities.RemoveAll();




            dgv_elements.Rows.Clear();
            dgv_members.Rows.Clear();
            dgv_joints.Rows.Clear();

            tv_mem_props.Nodes.Clear();
            tv_elem_props.Nodes.Clear();
            tv_mem_spec_truss.Nodes.Clear();
            tv_mem_spec_cable.Nodes.Clear();
            tv_mem_grps.Nodes.Clear();
            tv_mem_release.Nodes.Clear();
            tv_constants.Nodes.Clear();
            tv_supports.Nodes.Clear();
            tv_loads.Nodes.Clear();
            tv_mov_def_load.Nodes.Clear();
            tv_mov_loads.Nodes.Clear();
            tv_max_frc.Nodes.Clear();



            ld = null;
            dgv_node_disp.Rows.Clear();

            dgv_beam_frcs.Rows.Clear();
            dgv_max_frc.Rows.Clear();

            beamsNos.Clear();

            try
            {
                //if (beamDoc != null) beamDoc.ActiveLayOut.Entities.EraseAll();
                //if (slabDoc != null) slabDoc.ActiveLayOut.Entities.EraseAll();
                //if (columnDoc != null) columnDoc.ActiveLayOut.Entities.EraseAll();


                if (beamDoc != null) beamDoc.ActiveLayOut.Entities.RemoveAll();
                if (slabDoc != null) slabDoc.ActiveLayOut.Entities.RemoveAll();
                if (columnDoc != null) columnDoc.ActiveLayOut.Entities.RemoveAll();
            }
            catch (Exception exx) { }


        }
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
            vdDocument VD = VDoc;

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
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name == tsb_Save.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (!iApp.Is_StructureDemo)
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
        }
        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_Selection();
        }

        #region CAD Methods
        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
            {
                //gripset = GetGripSelection(false);
                vdSelection gripset = GetGripSelection(false);

                if (gripset == null) return;
                gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
                //activeDragDropControl = this.vdScrollableControl1;
                int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
                //activeDragDropControl = null;
                return;
            }
            if (e.Button != MouseButtons.Right) return;
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0) return;
            if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count != 1) return;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
            {
                //gripset = GetGripSelection(false);
                vdSelection gripset = GetGripSelection(false);
                if (gripset == null) return;
                gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
                //activeDragDropControl = this.vdScrollableControl1;
                int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
                //activeDragDropControl = null;
            }
            else
            {
                //if (BaseAction.PressedKeyCode != Keys.None) return; //6011 correction
                //MainForm parent = this.MdiParent as MainForm;
                //parent.commandLine.PostExecuteCommand("");
            }
        }
        private vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;

            vdDocument VD = ActiveDoc;



            string selsetname = "VDGRIPSET_" + VD.ActiveLayOut.Handle.ToStringValue() + (VD.ActiveLayOut.ActiveViewPort != null ? VD.ActiveLayOut.ActiveViewPort.Handle.ToStringValue() : "");
            gripset = VD.ActiveLayOut.Document.Selections.FindName(selsetname);
            if (Create)
            {
                if (gripset == null)
                {
                    gripset = VD.ActiveLayOut.Document.Selections.Add(selsetname);
                }
            }
            //vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname).RemoveAll();
            return gripset;
        }
        void BaseControl_vdMouseUp(MouseEventArgs e, ref bool cancel)
        {
            try
            {

                if (tc_parrent.SelectedTab == tab_pre_process)
                {
                    if (tc_prp_main.SelectedTab != tab_props || e.Button == MouseButtons.Right)
                    {
                        Delete_Layer_Items("Selection");
                        //return;
                    }

                    if (tc_prp_main.SelectedTab != tab_geom)
                    {
                        for (int i = 0; i < VDoc.ActiveLayOut.Entities.Count; i++)
                        {
                            if (VDoc.ActiveLayOut.Entities[i] is vdCircle)
                            {
                                if (VDoc.ActiveLayOut.Entities[i].Layer.Name != "Selection")
                                {
                                    VDoc.ActiveLayOut.Entities.RemoveAt(i); i = -1;
                                }
                            }
                        }
                        VDoc.Redraw(true);
                        return;
                    }
                    else
                    {
                        if (tc3.SelectedTab == tab2_elements)
                        {
                            return;
                        }
                    }
                }


                vdSelection gripset = GetGripSelection(false);
                vd3DFace dFace;
                vdLine ln;

                vdDocument VD = ActiveDoc;
                foreach (vdFigure fig in gripset)
                {
                    if (fig is vdText)
                    {
                        ShowMemberOnGrid(fig as vdText);
                        VD.Redraw(true);
                        break;
                    }
                    if (fig is vdPolyline)
                    {
                        ShowElementOnGrid(fig as vdPolyline);
                        VD.Redraw(true);
                        break;
                    }

                    if (fig is ASTRASupportFixed)
                    {
                        ASTRASupportFixed asf = fig as ASTRASupportFixed;

                        ShowNodeOnGrid(asf.Origin);
                        VD.Redraw(true);
                        break;
                    }

                    if (fig is ASTRASupportPinned)
                    {
                        ASTRASupportPinned asp = fig as ASTRASupportPinned;

                        ShowNodeOnGrid(asp.Origin);
                        VD.Redraw(true);
                        break;
                    }


                    ln = fig as vdLine;

                    if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
                    {
                    }
                    if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Left)
                    {
                    }
                    if (ln == null)
                    {
                        dFace = fig as vd3DFace;
                        ShowElementOnGrid(dFace);
                        VD.Redraw(true);
                    }
                    else
                    {
                        ShowMemberOnGrid(ln);
                        VD.Redraw(true);
                    }
                }

                gripset.RemoveAll();
            }
            catch (Exception exx) { }
        }
        #endregion Cad Methods



        private void dgvElementGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AST_DOC.Elements.ShowElement(e.RowIndex, VDoc, 0.093d);
            }
            catch (Exception ex) { }
        }
        private void dgvMemberGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AST_DOC.Members.ShowMember(e.RowIndex, VDoc);
            }
            catch (Exception ex) { }
        }
        private void dgv_joints_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            gPoint joint = new gPoint();

            try
            {
                //joint.x = double.Parse(dgv_joints[1, e.RowIndex].Value.ToString());
                //joint.y = double.Parse(dgv_joints[2, e.RowIndex].Value.ToString());
                //joint.z = double.Parse(dgv_joints[3, e.RowIndex].Value.ToString());

                //VectorDraw.Professional.vdPrimaries.vdFigure fg = null;
                //if (lastId != -1)
                //{
                //    for (int i = VDoc.ActiveLayOut.Entities.Count - 1; i >= 0; i--)
                //    {
                //        fg = VDoc.ActiveLayOut.Entities[i];
                //        if (fg.Id == lastId)
                //        {
                //            VDoc.ActiveLayOut.Entities.RemoveAt(i);
                //            break;
                //        }
                //    }
                //}
                //VDoc.CommandAction.CmdSphere(joint, 0.039, 10, 10);

                //lastId = VDoc.ActiveLayOut.Entities.Count - 1;
                //fg = VDoc.ActiveLayOut.Entities[lastId];
                //lastId = fg.Id;
                //fg.PenColor = new vdColor(Color.DarkViolet);

                //VDoc.Redraw(true);
            }
            catch (Exception ex) { }
        }

        private void btn_props_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_props_add.Name)
            {
                frmSectionProperties fs = new frmSectionProperties(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_props;

                fs.DGV_Joints = dgv_joints;
                fs.DGV_Members = dgv_members;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_props_del.Name)
            {
                tv_mem_props.Nodes.Remove(tv_mem_props.SelectedNode);
            }
            else if (btn.Name == btn_props_edit.Name)
            {
                frmSectionProperties fs = new frmSectionProperties(ACad);
                fs.Owner = this;
                fs.DGV_Joints = dgv_joints;
                fs.DGV_Members = dgv_members;
                fs.MGC = Groups;
                fs.TRV = tv_mem_props;
                fs.ASTRA_Data.Add(tv_mem_props.SelectedNode.Text);
                fs.ShowDialog();
            }
            if (tv_mem_props.SelectedNode != null)
                ShowMember(tv_mem_props.SelectedNode.Text, Text_Size * 0.3);
            else
                Delete_Layer_Items("Selection");

        }

        private void btn_grps_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_grps_add.Name)
            {
                frmMemberGroups fs = new frmMemberGroups(ACad);
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.MGroup != null)
                {
                    Groups.Add(fs.MGroup);
                    tv_mem_grps.Nodes.Add(fs.ASTRA_Data);
                }

            }
            else if (btn.Name == btn_grps_del.Name)
            {
                try
                {
                    Groups.RemoveAt(tv_mem_grps.SelectedNode.Index);

                    tv_mem_grps.Nodes.Remove(tv_mem_grps.SelectedNode);
                }
                catch (Exception ex) { }

            }
            else if (btn.Name == btn_grps_edit.Name)
            {
                try
                {
                    frmMemberGroups fs = new frmMemberGroups(ACad);
                    fs.Owner = this;
                    fs.MGroup = Groups[tv_mem_grps.SelectedNode.Index];
                    fs.ShowDialog();
                    Groups[tv_mem_grps.SelectedNode.Index] = fs.MGroup;
                    tv_mem_grps.Nodes[tv_mem_grps.SelectedNode.Index].Text = fs.ASTRA_Data;

                }
                catch (Exception ex) { }

            }
        }

        private void btn_spec_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_spec_add_cable.Name)
            {
                frmMemberTruss fs = new frmMemberTruss(ACad, true);
                fs.Owner = this;

                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_cable;
                fs.ShowDialog();
                //tv_mem_spec_cable.Nodes.Add(fs.ASTRA_Data);
            }
            else if (btn.Name == btn_spec_del_cable.Name)
            {
                tv_mem_spec_cable.Nodes.Remove(tv_mem_spec_cable.SelectedNode);
            }
            else if (btn.Name == btn_spec_edit_cable.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_cable;
                fs.ASTRA_Data = (tv_mem_spec_cable.SelectedNode.Text);
                fs.ShowDialog();
            }

            else if (btn.Name == btn_spec_add_truss.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_truss;
                fs.ShowDialog();
                //tv_mem_spec_truss.Nodes.Add(fs.ASTRA_Data);
            }
            else if (btn.Name == btn_spec_del_truss.Name)
            {
                tv_mem_spec_truss.Nodes.Remove(tv_mem_spec_truss.SelectedNode);
            }
            else if (btn.Name == btn_spec_edit_truss.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_truss;
                fs.ASTRA_Data = (tv_mem_spec_truss.SelectedNode.Text);
                fs.ShowDialog();
            }

        }

        private void btn_release_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Name == btn_release_add.Name)
            {
                frmMemberRelease fs = new frmMemberRelease(ACad);
                fs.Owner = this;
                fs.TRV = tv_mem_release;
                fs.ShowDialog();
            }
            else if (btn.Name == btn_release_del.Name)
            {
                tv_mem_release.Nodes.Remove(tv_mem_release.SelectedNode);
            }
            else if (btn.Name == btn_release_edit.Name)
            {

                frmMemberRelease fs = new frmMemberRelease(ACad);
                fs.Owner = this;
                fs.TRV = tv_mem_release;
                fs.ASTRA_Data = tv_mem_release.SelectedNode.Text;
                fs.ShowDialog();
            }
        }

        private void btn_constant_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Name == btn_cons_add.Name)
            {
                frmMaterialProperties fs = new frmMaterialProperties(ACad);
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.ASTRA_Data.Count > 0)
                {

                    TreeNode tn = new TreeNode("MATERIAL_" + (tv_constants.Nodes.Count + 1));
                    foreach (var item in fs.ASTRA_Data)
                    {
                        tn.Nodes.Add(item);
                    }
                    tv_constants.Nodes.Add(tn);

                    Materials.Add(fs.Material);

                }
            }
            else if (btn.Name == btn_cons_edit.Name)
            {
                TreeNode tn = tv_constants.SelectedNode;

                while (tn.Parent != null) tn = tn.Parent;

                frmMaterialProperties fs = new frmMaterialProperties(ACad);
                fs.Owner = this;
                fs.Material = Materials[tn.Index];

                fs.ShowDialog();

                if (fs.ASTRA_Data.Count > 0)
                {

                    //tn.Text = new TreeNode("MATERIAL_" + (tv_constants.Nodes.Count + 1));
                    tn.Nodes.Clear();
                    foreach (var item in fs.ASTRA_Data)
                    {
                        tn.Nodes.Add(item);
                    }
                    //tv_constants.Nodes.Add(tn);

                    Materials[tn.Index] = (fs.Material);

                }
            }
            else if (btn.Name == btn_cons_del.Name)
            {

                TreeNode tn = tv_constants.SelectedNode;
                while (tn.Parent != null) tn = tn.Parent;

                Materials.RemoveAt(tn.Index);

                tv_constants.Nodes.RemoveAt(tn.Index);
                //tv_constants.Nodes.RemoveAt(tv_constants.SelectedNode);
                //tv_constants.s
            }
        }

        private void btn_supp_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_supp_add.Name)
            {
                frmSupport fs = new frmSupport(ACad);
                fs.Owner = this;
                fs.TRV = tv_supports;
                fs.ShowDialog();
            }
            else if (btn.Name == btn_supp_del.Name)
            {
                tv_supports.Nodes.Remove(tv_supports.SelectedNode);
                //tv_constants.s
            }
            else if (btn.Name == btn_supp_edit.Name)
            {
                frmSupport fs = new frmSupport(ACad);
                fs.Owner = this;
                fs.TRV = tv_supports;
                fs.ASTRA_Data = tv_supports.SelectedNode.Text;
                fs.ShowDialog();
            }
            Draw_Supports();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TreeNode tn = tv_loads.SelectedNode;

            bool flag = false;
            if (tn != null)
            {
                try
                {
                    while (tn.Parent != null) tn = tn.Parent;
                    CurrentLoadIndex = tn.Index;
                    //tn.ExpandAll();
                }
                catch (Exception ex) { }
            }
            else
            {
                if (tv_loads.Nodes.Count > 0)
                    tn = tv_loads.Nodes[0];
            }

            int r = 0;
            #region Add Load
            if (btn.Name == btn_ldc_add.Name)
            {
                frmLoadCase flc = new frmLoadCase(ACad);
                flc.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                flc.Owner = this;
                flc.ShowDialog();

                if (flc.ASTRA_Data != "")
                {
                    tv_loads.Nodes.Add(flc.ASTRA_Data);
                    LoadCases.Add(flc.Ld);
                }
            }
            else if (btn.Name == btn_aload_add.Name)
            {
                switch (cmb_def_loads.SelectedIndex)
                {
                    #region All Loads

                    case 0: // Joint Load
                        #region Joint Load


                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("JOINT LO"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }
                        tn.ExpandAll();

                        if (!flag)
                        {
                            tn.Nodes.Add("JOINT LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                        }


                        frmJointLoad fjl = new frmJointLoad(ACad);
                        if (tn != null)
                            fjl.Node = tn;
                        fjl.Owner = this;
                        fjl.ShowDialog();

                        foreach (var item in fjl.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].JointLoads.Add(item);
                        }
                        #endregion
                        break;
                    case 1: // Joint Weight
                        #region Joint Weight

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("JOINT WEI"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }
                        tn.ExpandAll();

                        if (!flag)
                        {
                            tn.Nodes.Add("JOINT WEIGHT");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                        }

                        frmJointWeight fjw = new frmJointWeight(ACad);
                        if (tn != null)
                            fjw.Node = tn;
                        fjw.Owner = this;
                        fjw.ShowDialog();


                        if (fjw.ASTRA_Data.Count > 0)
                            LoadCases[CurrentLoadIndex].JointWeights.Clear();
                        foreach (var item in fjw.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].JointWeights.Add(item);
                        }
                        #endregion
                        break;
                    case 2: // Support Displacement Load
                        #region Support Displacement Load

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("SUPPORT"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }
                        tn.ExpandAll();

                        if (!flag)
                        {
                            tn.Nodes.Add("SUPPORT DISPLACEMENT LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                        }


                        frmSupportDisplacements fsd = new frmSupportDisplacements(ACad);
                        if (tn != null)
                            fsd.Node = tn;
                        fsd.Owner = this;
                        fsd.ShowDialog();

                        foreach (var item in fsd.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].SupportDisplacementLoads.Add(item);
                        }

                        #endregion
                        break;
                    case 3: // Member Load
                        #region  Member Load

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("MEMBER"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                        {

                            tn.Nodes.Add("MEMBER LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                            tn.ExpandAll();


                        }



                        frmMemberLoad fml = new frmMemberLoad(ACad);

                        if (tn != null)
                            fml.Node = tn;
                        fml.Owner = this;
                        fml.ShowDialog();

                        foreach (var item in fml.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].MemberLoads.Add(item);
                            //tn.Nodes.Add(item);
                        }
                        #endregion
                        break;
                    case 4: //  Element Load
                        #region  Element Load

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("ELEMENT"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                        {
                            tn.Nodes.Add("ELEMENT LOAD");
                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                            tn.ExpandAll();
                        }

                        frmElementLoad fel = new frmElementLoad(ACad);

                        if (tn != null)
                            fel.Node = tn;
                        fel.Owner = this;
                        fel.ShowDialog();

                        foreach (var item in fel.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].ElementLoads.Add(item);
                            //tn.Nodes.Add(item);
                        }
                        #endregion
                        break;
                    case 5: //  Repeat Load
                        #region  Repeat Load

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("REPEAT"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                        {
                            tn.Nodes.Add("REPEAT LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                            tn.ExpandAll();
                        }


                        frmLoadCombination frl = new frmLoadCombination(ACad, LoadCases);
                        frl.IsRepeatLoad = true;
                        frl.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
                        frl.Owner = this;
                        frl.LDC.LoadNo = Current_LoadCase.LoadNo;
                        frl.LDC.Name = Current_LoadCase.Title;
                        frl.ShowDialog();

                        if (frl.LDC.Count > 0)
                        {
                            LoadCaseDefinition Ld = new LoadCaseDefinition();
                            Ld.LoadNo = frl.LDC.LoadNo;
                            Ld.Title = frl.LDC.Name;
                            Ld.RepeatLoads = frl.LDC;
                            LoadCases[CurrentLoadIndex].RepeatLoads = frl.LDC;

                            Ld.RepeatLoads.Set_Combination();
                            for (int i = 0; i < Ld.RepeatLoads.Count; i++)
                            {
                                tn.Nodes.Add(Ld.RepeatLoads[i]);
                            }
                        }

                        #endregion
                        break;
                    case 6: // Area Load
                        #region Area Load

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("AREA"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                        {
                            tn.Nodes.Add("AREA LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                        }


                        frmAreaLoad fal = new frmAreaLoad(ACad);
                        if (tn != null)
                            fal.Node = tn;
                        fal.Owner = this;
                        fal.ShowDialog();


                        foreach (var item in fal.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].AreaLoads.Add(item);
                        }
                        #endregion

                        break;
                    case 7: // Floor Load
                        #region Floor Load

                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("FLOOR"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                        {
                            tn.Nodes.Add("FLOOR LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                        }


                        frmFloorLoad ffl = new frmFloorLoad(ACad);
                        if (tn != null)
                            ffl.Node = tn;
                        ffl.Owner = this;
                        ffl.ShowDialog();


                        foreach (var item in ffl.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].FloorLoads.Add(item);
                        }

                        #endregion
                        break;
                    case 8: //  Temperature Load
                        #region Temperature Load
                        for (int i = 0; i < tn.Nodes.Count; i++)
                        {
                            if (tn.Nodes[i].Text.StartsWith("TEMP"))
                            {
                                tn = tn.Nodes[i];
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            tn.Nodes.Add("TEMP LOAD");

                            tn.ExpandAll();
                            tn = tn.Nodes[tn.Nodes.Count - 1];
                        }


                        frmTempLoad ftl = new frmTempLoad(ACad);
                        if (tn != null)
                            ftl.Node = tn;
                        ftl.Owner = this;
                        ftl.ShowDialog();

                        foreach (var item in ftl.ASTRA_Data)
                        {
                            LoadCases[CurrentLoadIndex].TemperatureLoads.Add(item);
                            //tn.Nodes.Add(item);
                        }
                        #endregion

                        break;
                    case 9: // Combination
                        #region Combination
                        frmLoadCombination fcl = new frmLoadCombination(ACad, LoadCases);
                        fcl.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
                        fcl.Owner = this;
                        fcl.ShowDialog();

                        if (fcl.LDC.Count > 0)
                        {
                            LoadCaseDefinition Ld = new LoadCaseDefinition();
                            Ld.LoadNo = fcl.LDC.LoadNo;
                            Ld.Title = fcl.LDC.Name;
                            Ld.Comb_Loads = fcl.LDC;

                            LoadCases.Add(Ld);

                            tv_loads.Nodes.Add(Ld.LoadNo + " : " + Ld.Title);

                            Ld.Comb_Loads.Set_Combination();
                            for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                            {
                                tv_loads.Nodes[tv_loads.Nodes.Count - 1].Nodes.Add(Ld.Comb_Loads[i]);
                            }
                        }

                        #endregion

                        break;


                    case 10: // Seismic Load
                        #region Seismic Load

                        for (int i = 0; i < tv_loads.Nodes.Count; i++)
                        {
                            if (tv_loads.Nodes[i].Text.StartsWith("SEISMIC LO"))
                            {
                                tn = tv_loads.Nodes[i];
                                flag = true;
                                break;
                            }
                        }
                        tn.ExpandAll();

                        if (!flag)
                        {
                            //tn.Text
                            //while (tn.Parent != null)
                            //    tn = tn.Parent;
                            tv_loads.Nodes.Add("SEISMIC LOAD");

                            tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];
                            tn.ExpandAll();
                        }


                        frmSeismicLoad fsl = new frmSeismicLoad(ACad);
                        if (tn != null)
                            fsl.Node = tn;
                        fsl.Owner = this;
                        fsl.ShowDialog();
                        SeismicLoads = new List<string>();
                        //foreach (var item in fsl.ASTRA_Data)
                        //{
                        //    SeismicLoads.Add(item);
                        //}
                        if (true)
                        {
                            LoadCaseDefinition lds = new LoadCaseDefinition();
                            LoadCases.Add(lds);
                            lds.Title = "SEISMIC LOAD";
                            lds.LoadNo = LoadCases.Count;
                            foreach (var item in fsl.ASTRA_Data)
                            {
                                SeismicLoads.Add(item);
                                lds.JointLoads.Add(new SLoad(item));
                            }
                        }
                        #endregion Seismic

                        break;


                    case 11: // Seismic Load
                        #region Seismic Load

                        for (int i = 0; i < tv_loads.Nodes.Count; i++)
                        {
                            if (tv_loads.Nodes[i].Text.StartsWith("WIND LO"))
                            {
                                tn = tv_loads.Nodes[i];
                                flag = true;
                                break;
                            }
                        }
                        tn.ExpandAll();

                        if (!flag)
                        {
                            //tn.Text
                            //while (tn.Parent != null)
                            //    tn = tn.Parent;
                            tv_loads.Nodes.Add("WIND LOAD");

                            tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];
                            tn.ExpandAll();
                        }


                        frmWindLoad fwl = new frmWindLoad(ACad);
                        if (tn != null)
                            fwl.Node = tn;
                        fwl.Owner = this;
                        fwl.ShowDialog();
                        //SeismicLoads = new List<string>();
                        //foreach (var item in fsl.ASTRA_Data)
                        //{
                        //    SeismicLoads.Add(item);
                        //}
                        if (true)
                        {
                            LoadCaseDefinition lds = new LoadCaseDefinition();
                            LoadCases.Add(lds);
                            lds.Title = "WIND LOAD";
                            lds.LoadNo = LoadCases.Count;
                            foreach (var item in fwl.ASTRA_Data)
                            {
                                lds.JointLoads.Add(new SLoad(item));
                            }
                        }
                        #endregion Seismic

                        break;
                    #endregion All Loads

                }
            }
            #endregion Add Load

            #region Edit Load

            else if (btn.Name == btn_ldc_edit.Name)
            {

                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ASTRA_Data = tv_loads.Nodes[CurrentLoadIndex].Text;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "" && fs.Ld != null)
                {
                    tv_loads.Nodes[CurrentLoadIndex].Text = fs.ASTRA_Data;
                    Current_LoadCase.LoadNo = fs.Ld.LoadNo;
                    Current_LoadCase.Title = fs.Ld.Title;
                }
            }
            else if (btn.Name == btn_ldc_del.Name)
            {
                try
                {
                    LoadCases.RemoveAt(CurrentLoadIndex);
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            else if (btn.Name == btn_aload_edit.Name)
            {
                switch (cmb_def_loads.SelectedIndex)
                {
                    #region All Loads

                    case 0: // Joint Load
                        #region Joint Load

                        r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmJointLoad fjl = new frmJointLoad(ACad);
                        if (tn != null)
                            fjl.Node = tv_loads.SelectedNode;
                        fjl.Owner = this;
                        fjl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fjl.ShowDialog();


                        if (r != -1 && fjl.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].JointLoads[r] = fjl.ASTRA_Data[0];
                        }

                        #endregion
                        break;
                    case 1: // Joint Weight
                        #region Joint Weight

                        r = LoadCases[CurrentLoadIndex].JointWeights.IndexOf(tv_loads.SelectedNode.Text);
                        frmJointWeight fjw = new frmJointWeight(ACad);
                        if (tn != null)
                            fjw.Node = tv_loads.SelectedNode;
                        fjw.Owner = this;
                        fjw.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fjw.ShowDialog();


                        if (r != -1 && fjw.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].JointWeights[r] = fjw.ASTRA_Data[0];
                        }

                        #endregion

                        break;
                    case 2: // Support Displacement Load
                        #region Support Displacement Load

                        r = LoadCases[CurrentLoadIndex].SupportDisplacementLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmSupportDisplacements fsd = new frmSupportDisplacements(ACad);
                        if (tn != null)
                            fsd.Node = tv_loads.SelectedNode;
                        fsd.Owner = this;
                        fsd.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fsd.ShowDialog();


                        if (r != -1 && fsd.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fsd.ASTRA_Data[0];
                        }
                        #endregion

                        break;
                    case 3: // Member Load
                        #region  Member Load

                        r = LoadCases[CurrentLoadIndex].MemberLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmMemberLoad fml = new frmMemberLoad(ACad);
                        if (tn != null)
                            fml.Node = tv_loads.SelectedNode;
                        fml.Owner = this;
                        fml.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fml.ShowDialog();


                        if (r != -1 && fml.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].MemberLoads[r] = fml.ASTRA_Data[0];
                        }

                        #endregion

                        break;
                    case 4: //  Element Load
                        #region  Element Load

                        r = LoadCases[CurrentLoadIndex].ElementLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmElementLoad fel = new frmElementLoad(ACad);
                        if (tn != null)
                            fel.Node = tv_loads.SelectedNode;
                        fel.Owner = this;
                        fel.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fel.ShowDialog();


                        if (r != -1 && fel.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].ElementLoads[r] = fel.ASTRA_Data[0];
                        }

                        #endregion

                        break;
                    case 5: //  Repeat Load
                        #region  Repeat Load

                        frmLoadCombination frl = new frmLoadCombination(ACad, LoadCases);
                        //frl.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                        frl.Owner = this;
                        frl.IsRepeatLoad = true;
                        frl.LDC = Current_LoadCase.RepeatLoads;
                        frl.ShowDialog();

                        if (frl.LDC.Count > 0)
                        {

                            tn = tv_loads.SelectedNode.Parent;


                            Current_LoadCase.RepeatLoads = frl.LDC;

                            tn.Nodes.Clear();

                            frl.LDC.Set_Combination();
                            for (int i = 0; i < frl.LDC.Count; i++)
                            {
                                tn.Nodes.Add(frl.LDC[i]);
                            }
                        }
                        #endregion

                        break;
                    case 6: // Area Load
                        #region Area Load


                        r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmAreaLoad fal = new frmAreaLoad(ACad);
                        if (tn != null)
                            fal.Node = tv_loads.SelectedNode;
                        fal.Owner = this;
                        fal.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fal.ShowDialog();

                        if (r != -1 && fal.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].JointLoads[r] = fal.ASTRA_Data[0];
                        }


                        #endregion

                        break;
                    case 7: // Floor Load
                        #region Floor Load

                        r = LoadCases[CurrentLoadIndex].FloorLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmFloorLoad ffl = new frmFloorLoad(ACad);
                        if (tn != null)
                            ffl.Node = tv_loads.SelectedNode;
                        ffl.Owner = this;
                        ffl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        ffl.ShowDialog();

                        if (r != -1 && ffl.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].FloorLoads[r] = ffl.ASTRA_Data[0];
                        }

                        #endregion

                        break;
                    case 8: //  Temperature Load
                        #region Temperature Load

                        r = LoadCases[CurrentLoadIndex].TemperatureLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmTempLoad ftl = new frmTempLoad(ACad);
                        if (tn != null)
                            ftl.Node = tv_loads.SelectedNode;
                        ftl.Owner = this;
                        ftl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        ftl.ShowDialog();

                        if (r != -1 && ftl.ASTRA_Data.Count > 0)
                        {
                            LoadCases[CurrentLoadIndex].TemperatureLoads[r] = ftl.ASTRA_Data[0];
                        }
                        #endregion

                        break;
                    case 9: // Combination
                        #region Combination

                        frmLoadCombination fcl = new frmLoadCombination(ACad, LoadCases);
                        //fcl.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                        fcl.Owner = this;
                        fcl.LDC = Current_LoadCase.Comb_Loads;
                        fcl.ShowDialog();

                        if (fcl.LDC.Count > 0)
                        {
                            LoadCaseDefinition Ld = new LoadCaseDefinition();
                            Ld.LoadNo = fcl.LDC.LoadNo;
                            Ld.Title = fcl.LDC.Name;
                            Ld.Comb_Loads = fcl.LDC;

                            Current_LoadCase = (Ld);



                            tv_loads.Nodes[CurrentLoadIndex].Text = (Ld.LoadNo + " : " + Ld.Title);

                            tv_loads.Nodes[CurrentLoadIndex].Nodes.Clear();
                            Ld.Comb_Loads.Set_Combination();
                            for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                            {
                                tv_loads.Nodes[CurrentLoadIndex].Nodes.Add(Ld.Comb_Loads[i]);
                            }
                        }
                        #endregion
                        break;


                    case 10: // Seismic Load
                        #region Seismic Load

                        r = SeismicLoads.IndexOf(tv_loads.SelectedNode.Text);
                        frmSeismicLoad fsl = new frmSeismicLoad(ACad);
                        if (tn != null)
                            fsl.Node = tv_loads.SelectedNode;
                        fsl.Owner = this;
                        fsl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fsl.ShowDialog();


                        if (r != -1 && fsl.ASTRA_Data.Count > 0)
                        {
                            SeismicLoads.Clear();
                            //SeismicLoads.Add(fsl.ASTRA_Data[0]);
                            //LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fsl.ASTRA_Data[0];

                            foreach (var item in fsl.ASTRA_Data)
                            {
                                SeismicLoads.Add(item);
                            }
                        }

                        #endregion Seismic
                        break;

                    case 11: // Wind Load
                        #region Wind Load

                        frmWindLoad fwl = new frmWindLoad(ACad);
                        if (tn != null)
                            fwl.Node = tv_loads.SelectedNode;
                        fwl.Owner = this;
                        fwl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        fwl.ShowDialog();

                        //if (r != -1 && fsl.ASTRA_Data.Count > 0)
                        //{
                        //    foreach (var item in fsl.ASTRA_Data)
                        //    {
                        //        SeismicLoads.Add(item);
                        //    }
                        //}

                        #endregion Seismic
                        break;
                    #endregion All Loads
                }
            }
            #endregion Edit Load

            #region Delete Load
            else if (btn.Name == btn_aload_del.Name)
            {
                switch (cmb_def_loads.SelectedIndex)
                {
                    #region All Loads

                    case 5: //  Repeat Load
                    case 9: // Combination
                        try
                        {
                            tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                            LoadCases.RemoveAt(CurrentLoadIndex);
                        }
                        catch (Exception ex) { }
                        break;
                    default:
                        tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
                        break;

                    #endregion All Loads
                }
            }
            #endregion Delete Load



            grb_def_loads.Enabled = (tv_loads.Nodes.Count > 0);

            Button_Enable_Disable();

        }

        private void btn_load_Click_2015_03_20(object sender, EventArgs e)
        {
            //Button btn = sender as Button;
            //TreeNode tn = tv_loads.SelectedNode;

            //bool flag = false;
            //if (tn != null)
            //{
            //    try
            //    {
            //        while (tn.Parent != null) tn = tn.Parent;
            //        CurrentLoadIndex = tn.Index;
            //        tn.ExpandAll();
            //    }
            //    catch (Exception ex) { }
            //}
            //else
            //{
            //    if (tv_loads.Nodes.Count > 0)
            //        tn = tv_loads.Nodes[0];
            //}
            //#region Define Load Case
            //if (btn.Name == btn_ldc_add.Name)
            //{
            //    frmLoadCase fs = new frmLoadCase(ACad);
            //    fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    if (fs.ASTRA_Data != "")
            //    {
            //        tv_loads.Nodes.Add(fs.ASTRA_Data);
            //        LoadCases.Add(fs.Ld);
            //    }
            //}
            //else if (btn.Name == btn_ldc_edit.Name)
            //{

            //    frmLoadCase fs = new frmLoadCase(ACad);
            //    fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
            //    fs.Owner = this;
            //    fs.ASTRA_Data = tv_loads.Nodes[CurrentLoadIndex].Text;
            //    fs.ShowDialog();

            //    if (fs.ASTRA_Data != "")
            //    {
            //        tv_loads.Nodes[CurrentLoadIndex].Text = fs.ASTRA_Data;
            //        Current_LoadCase.LoadNo = fs.Ld.LoadNo;
            //        Current_LoadCase.Title = fs.Ld.Title;
            //    }
            //}
            //else if (btn.Name == btn_ldc_del.Name)
            //{
            //    try
            //    {
            //        tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
            //        LoadCases.RemoveAt(CurrentLoadIndex);
            //    }
            //    catch (Exception ex) { }
            //}
            //#endregion Define Load Case

            //#region Define Joint Load
            //else if (btn.Name == btn_jload_add.Name)
            //{

            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("JOINT"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }
            //    tn.ExpandAll();

            //    if (!flag)
            //    {
            //        tn.Nodes.Add("JOINT LOAD");

            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //    }


            //    frmJointLoad fs = new frmJointLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tn;
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    foreach (var item in fs.ASTRA_Data)
            //    {
            //        LoadCases[CurrentLoadIndex].JointLoads.Add(item);
            //    }
            //}
            //else if (btn.Name == btn_jload_edit.Name)
            //{

            //    int r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loads.SelectedNode.Text);
            //    frmJointLoad fs = new frmJointLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tv_loads.SelectedNode;
            //    fs.Owner = this;
            //    fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
            //    fs.ShowDialog();


            //    if (r != -1 && fs.ASTRA_Data.Count > 0)
            //    {
            //        LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
            //    }

            //}
            //else if (btn.Name == btn_jload_del.Name)
            //{
            //    //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
            //    tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            //}
            //#endregion Define Joint Load

            //#region Define SINK Load
            //else if (btn.Name == btn_sdload_add.Name)
            //{

            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("SUPPORT"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }
            //    tn.ExpandAll();

            //    if (!flag)
            //    {
            //        tn.Nodes.Add("SUPPORT DISPLACEMENT LOAD");

            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //    }


            //    frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
            //    if (tn != null)
            //        fs.Node = tn;
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    foreach (var item in fs.ASTRA_Data)
            //    {
            //        LoadCases[CurrentLoadIndex].SupportDisplacementLoads.Add(item);
            //    }
            //}
            //else if (btn.Name == btn_sdload_edit.Name)
            //{

            //    int r = LoadCases[CurrentLoadIndex].SupportDisplacementLoads.IndexOf(tv_loads.SelectedNode.Text);
            //    frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
            //    if (tn != null)
            //        fs.Node = tv_loads.SelectedNode;
            //    fs.Owner = this;
            //    fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
            //    fs.ShowDialog();


            //    if (r != -1 && fs.ASTRA_Data.Count > 0)
            //    {
            //        LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fs.ASTRA_Data[0];
            //    }

            //}
            //else if (btn.Name == btn_sdload_del.Name)
            //{
            //    //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
            //    tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            //}
            //#endregion Define SINK Load


            //#region Define Member Load
            //else if (btn.Name == btn_mload_add.Name)
            //{

            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("MEMBER"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag)
            //    {

            //        tn.Nodes.Add("MEMBER LOAD");

            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //        tn.ExpandAll();


            //    }



            //    frmMemberLoad fs = new frmMemberLoad(ACad);

            //    if (tn != null)
            //        fs.Node = tn;
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    foreach (var item in fs.ASTRA_Data)
            //    {
            //        LoadCases[CurrentLoadIndex].MemberLoads.Add(item);
            //        //tn.Nodes.Add(item);
            //    }
            //}
            //else if (btn.Name == btn_mload_edit.Name)
            //{

            //    int r = LoadCases[CurrentLoadIndex].MemberLoads.IndexOf(tv_loads.SelectedNode.Text);
            //    frmMemberLoad fs = new frmMemberLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tv_loads.SelectedNode;
            //    fs.Owner = this;
            //    fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
            //    fs.ShowDialog();


            //    if (r != -1 && fs.ASTRA_Data.Count > 0)
            //    {
            //        LoadCases[CurrentLoadIndex].MemberLoads[r] = fs.ASTRA_Data[0];
            //    }
            //}
            //else if (btn.Name == btn_mload_del.Name)
            //{
            //    tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            //}
            //#endregion Define Member Load


            //#region Define Element Load
            //else if (btn.Name == btn_eload_add.Name)
            //{

            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("ELEMENT"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag)
            //    {
            //        tn.Nodes.Add("ELEMENT LOAD");
            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //        tn.ExpandAll();
            //    }

            //    frmElementLoad fs = new frmElementLoad(ACad);

            //    if (tn != null)
            //        fs.Node = tn;
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    foreach (var item in fs.ASTRA_Data)
            //    {
            //        LoadCases[CurrentLoadIndex].ElementLoads.Add(item);
            //        //tn.Nodes.Add(item);
            //    }
            //}
            //else if (btn.Name == btn_eload_edit.Name)
            //{

            //    int r = LoadCases[CurrentLoadIndex].ElementLoads.IndexOf(tv_loads.SelectedNode.Text);
            //    frmElementLoad fs = new frmElementLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tv_loads.SelectedNode;
            //    fs.Owner = this;
            //    fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
            //    fs.ShowDialog();


            //    if (r != -1 && fs.ASTRA_Data.Count > 0)
            //    {
            //        LoadCases[CurrentLoadIndex].ElementLoads[r] = fs.ASTRA_Data[0];
            //    }
            //}
            //else if (btn.Name == btn_eload_del.Name)
            //{
            //    tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            //}
            //#endregion Define Member Load

            //#region Define Area Load
            //else if (btn.Name == btn_aload_add.Name)
            //{
            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("AREA"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag)
            //    {
            //        tn.Nodes.Add("AREA LOAD");

            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //    }


            //    frmAreaLoad fs = new frmAreaLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tn;
            //    fs.Owner = this;
            //    fs.ShowDialog();


            //    foreach (var item in fs.ASTRA_Data)
            //    {
            //        LoadCases[CurrentLoadIndex].AreaLoads.Add(item);
            //    }
            //}
            //else if (btn.Name == btn_aload_edit.Name)
            //{

            //    int r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loads.SelectedNode.Text);
            //    frmAreaLoad fs = new frmAreaLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tv_loads.SelectedNode;
            //    fs.Owner = this;
            //    fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
            //    fs.ShowDialog();

            //    if (r != -1 && fs.ASTRA_Data.Count > 0)
            //    {
            //        LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
            //    }
            //}
            //else if (btn.Name == btn_aload_del.Name)
            //{
            //    tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            //}
            //#endregion Define Area Load

            //#region Define Temperature Load
            //else if (btn.Name == btn_tload_add.Name)
            //{
            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("TEMP"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag)
            //    {
            //        tn.Nodes.Add("TEMP LOAD");

            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //    }


            //    frmTempLoad fs = new frmTempLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tn;
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    foreach (var item in fs.ASTRA_Data)
            //    {
            //        LoadCases[CurrentLoadIndex].TemperatureLoads.Add(item);
            //        //tn.Nodes.Add(item);
            //    }

            //}
            //else if (btn.Name == btn_tload_edit.Name)
            //{

            //    int r = LoadCases[CurrentLoadIndex].TemperatureLoads.IndexOf(tv_loads.SelectedNode.Text);
            //    frmTempLoad fs = new frmTempLoad(ACad);
            //    if (tn != null)
            //        fs.Node = tv_loads.SelectedNode;
            //    fs.Owner = this;
            //    fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
            //    fs.ShowDialog();

            //    if (r != -1 && fs.ASTRA_Data.Count > 0)
            //    {
            //        LoadCases[CurrentLoadIndex].TemperatureLoads[r] = fs.ASTRA_Data[0];
            //    }
            //}
            //else if (btn.Name == btn_tload_del.Name)
            //{
            //    tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            //}
            //#endregion Define Temperature Load

            //#region Define Repeat Load
            //else if (btn.Name == btn_rload_add.Name)
            //{
            //    for (int i = 0; i < tn.Nodes.Count; i++)
            //    {
            //        if (tn.Nodes[i].Text.StartsWith("REPEAT"))
            //        {
            //            tn = tn.Nodes[i];
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (!flag)
            //    {
            //        tn.Nodes.Add("REPEAT LOAD");

            //        tn.ExpandAll();
            //        tn = tn.Nodes[tn.Nodes.Count - 1];
            //        tn.ExpandAll();
            //    }


            //    frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
            //    fs.IsRepeatLoad = true;
            //    fs.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
            //    fs.Owner = this;
            //    fs.LDC.LoadNo = Current_LoadCase.LoadNo;
            //    fs.LDC.Name = Current_LoadCase.Title;
            //    fs.ShowDialog();

            //    if (fs.LDC.Count > 0)
            //    {
            //        LoadCaseDefinition Ld = new LoadCaseDefinition();
            //        Ld.LoadNo = fs.LDC.LoadNo;
            //        Ld.Title = fs.LDC.Name;
            //        Ld.RepeatLoads = fs.LDC;
            //        LoadCases[CurrentLoadIndex].RepeatLoads = fs.LDC;

            //        Ld.RepeatLoads.Set_Combination();
            //        for (int i = 0; i < Ld.RepeatLoads.Count; i++)
            //        {
            //            tn.Nodes.Add(Ld.RepeatLoads[i]);
            //        }
            //    }

            //}
            //else if (btn.Name == btn_rload_edit.Name)
            //{

            //    frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
            //    //fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
            //    fs.Owner = this;
            //    fs.IsRepeatLoad = true;
            //    fs.LDC = Current_LoadCase.RepeatLoads;
            //    fs.ShowDialog();

            //    if (fs.LDC.Count > 0)
            //    {

            //        tn = tv_loads.SelectedNode.Parent;


            //        Current_LoadCase.RepeatLoads = fs.LDC;

            //        tn.Nodes.Clear();

            //        fs.LDC.Set_Combination();
            //        for (int i = 0; i < fs.LDC.Count; i++)
            //        {
            //            tn.Nodes.Add(fs.LDC[i]);
            //        }
            //    }
            //}
            //else if (btn.Name == btn_rload_del.Name)
            //{
            //    try
            //    {
            //        tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
            //        LoadCases.RemoveAt(CurrentLoadIndex);
            //    }
            //    catch (Exception ex) { }
            //}
            //#endregion Define Repeat Load

            //#region Define Combination Load
            //else if (btn.Name == btn_cload_add.Name)
            //{

            //    frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
            //    fs.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
            //    fs.Owner = this;
            //    fs.ShowDialog();

            //    if (fs.LDC.Count > 0)
            //    {
            //        LoadCaseDefinition Ld = new LoadCaseDefinition();
            //        Ld.LoadNo = fs.LDC.LoadNo;
            //        Ld.Title = fs.LDC.Name;
            //        Ld.Comb_Loads = fs.LDC;

            //        LoadCases.Add(Ld);

            //        tv_loads.Nodes.Add(Ld.LoadNo + " : " + Ld.Title);

            //        Ld.Comb_Loads.Set_Combination();
            //        for (int i = 0; i < Ld.Comb_Loads.Count; i++)
            //        {
            //            tv_loads.Nodes[tv_loads.Nodes.Count - 1].Nodes.Add(Ld.Comb_Loads[i]);
            //        }
            //    }

            //}
            //else if (btn.Name == btn_cload_edit.Name)
            //{

            //    frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
            //    //fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
            //    fs.Owner = this;
            //    fs.LDC = Current_LoadCase.Comb_Loads;
            //    fs.ShowDialog();

            //    if (fs.LDC.Count > 0)
            //    {
            //        LoadCaseDefinition Ld = new LoadCaseDefinition();
            //        Ld.LoadNo = fs.LDC.LoadNo;
            //        Ld.Title = fs.LDC.Name;
            //        Ld.Comb_Loads = fs.LDC;

            //        Current_LoadCase = (Ld);



            //        tv_loads.Nodes[CurrentLoadIndex].Text = (Ld.LoadNo + " : " + Ld.Title);

            //        tv_loads.Nodes[CurrentLoadIndex].Nodes.Clear();
            //        Ld.Comb_Loads.Set_Combination();
            //        for (int i = 0; i < Ld.Comb_Loads.Count; i++)
            //        {
            //            tv_loads.Nodes[CurrentLoadIndex].Nodes.Add(Ld.Comb_Loads[i]);
            //        }
            //    }
            //}
            //else if (btn.Name == btn_cload_del.Name)
            //{
            //    try
            //    {
            //        tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
            //        LoadCases.RemoveAt(CurrentLoadIndex);
            //    }
            //    catch (Exception ex) { }
            //}
            //#endregion Define Combination Load

            //grb_cload.Enabled = (tv_loads.Nodes.Count > 0);

            //grb_jload.Enabled = (tv_loads.Nodes.Count > 0);
            //grb_mload.Enabled = (tv_loads.Nodes.Count > 0);
            //grb_def_loads.Enabled = (tv_loads.Nodes.Count > 0);
            //grb_tload.Enabled = (tv_loads.Nodes.Count > 0);
            //grb_rload.Enabled = (tv_loads.Nodes.Count > 0);

            //Button_Enable_Disable();

        }

        private void rbtn_perform_eigen_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            txt_frequencies.Enabled = rbtn_perform_eigen.Checked;
            grb_dynamic.Enabled = !rbtn_perform_eigen.Checked;
        }

        private void chk_print_max_force_CheckedChanged(object sender, EventArgs e)
        {
            grb_max_frc.Enabled = chk_print_max_force.Checked;
        }

        private void btn_dyn_add_Click(object sender, EventArgs e)
        {

            if (rbtn_perform_time_history.Checked)
            {
                frmTimeHistory fs = new frmTimeHistory(ACad, true);
                fs.Owner = this;
                fs.ShowDialog();

                txt_dynamic.Lines = fs.ASTRA_Data.ToArray();
            }
            else if (rbtn_response_spectrum.Checked)
            {
                frmResponse fs = new frmResponse(ACad);
                fs.Owner = this;
                //fs.ShowDialog();
                if (fs.ShowDialog() != DialogResult.Cancel)
                    txt_dynamic.Lines = fs.ASTRA_Data.ToArray();
            }
        }

        private void btn_dyn_del_Click(object sender, EventArgs e)
        {
            txt_dynamic.Text = "";
        }
        private void btn_file_open_Click(object sender, EventArgs e)
        {

        }

        private void btn_file_save_Click(object sender, EventArgs e)
        {
            if (File.Exists(Drawing_File))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    //sfd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    sfd.Filter = "VDML Files (*.vdml)|*.vdml|DXF Files (*.dxf)|*.dxf|DWG Files (*.dwg)|*.dwg";

                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (VDoc.SaveAs(sfd.FileName))
                        {
                            MessageBox.Show("File saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void chk_dynamic_analysis_CheckedChanged(object sender, EventArgs e)
        {
            grb_dyna_analysis.Enabled = chk_dynamic_analysis.Checked;

            //Mem
        }

        private void btn_mov_load_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            TreeNode tn = tv_mov_loads.SelectedNode;


            int type = 0;
            int indx = -1;
            MovingLoadData MLD = null;
            if (tn != null)
            {
                while (tn.Parent != null) tn = tn.Parent;


                MyStrings mlist = new MyStrings(tn.Text, ' ');


                type = mlist.GetInt(1);


                for (int i = 0; i < MovingLoads.Count; i++)
                {
                    if (MovingLoads[i].Type == type)
                    {
                        indx = i;
                        MLD = MovingLoads[i];
                        break;
                    }
                }
            }


            ACad.MassUnit = (EMassUnits)cmb_Base_MUnit.SelectedIndex;
            #region Define Load Case
            if (btn.Name == btn_mov_load_add.Name)
            {
                frmDefineMovingLoad fs = new frmDefineMovingLoad(ACad);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.txt_type.Text = (tv_mov_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.MLD != null)
                {
                    MovingLoads.Add(fs.MLD);
                    tn = new TreeNode(fs.MLD.ToString());

                    tv_mov_loads.Nodes.Add(tn);
                    tn.Nodes.Add("LOADS : " + fs.MLD.Loads);
                    tn.Nodes.Add("DISTANCES : " + fs.MLD.Distances);
                    tn.Nodes.Add("LOAD WIDTH : " + fs.MLD.LoadWidth);
                }
            }
            else if (btn.Name == btn_mov_load_edit.Name)
            {


                frmDefineMovingLoad fs = new frmDefineMovingLoad(ACad);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                //fs.txt_type.Text = (tv_mov_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.MLD = MLD;
                fs.ShowDialog();

                if (fs.MLD != null)
                {
                    MovingLoads[indx] = (fs.MLD);

                    tv_mov_loads.Nodes[indx].Text = fs.MLD.ToString();
                    tv_mov_loads.Nodes[indx].Nodes.Clear();
                    tv_mov_loads.Nodes[indx].Nodes.Add("LOADS : " + fs.MLD.Loads);
                    tv_mov_loads.Nodes[indx].Nodes.Add("DISTANCES : " + fs.MLD.Distances);
                    tv_mov_loads.Nodes[indx].Nodes.Add("LOAD WIDTH : " + fs.MLD.LoadWidth);
                }

            }
            else if (btn.Name == btn_mov_load_del.Name)
            {
                try
                {

                    MovingLoads.RemoveAt(indx);

                    tv_mov_loads.Nodes.Remove(tn);
                }
                catch (Exception ex) { }
            }
            #endregion Define Load Case
        }
        string Load_Type = "";


        private void tv_loadings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Load_Selection(e.Node);
            Draw_Loadings();
        }

        private void Load_Selection(TreeNode tn)
        {
            //tv_loadings.SelectedNode
            Load_Type = "";


            if (tn.Nodes.Count == 0)
            {


                if (tn.Parent != null)
                {
                    Load_Type = tn.Parent.Text;

                    if (tn.Parent.Text.StartsWith("JOINT LO"))
                        cmb_def_loads.SelectedIndex = 0;
                    if (tn.Parent.Text.StartsWith("JOINT WE"))
                        cmb_def_loads.SelectedIndex = 1;
                    if (tn.Parent.Text.StartsWith("SUPPORT"))
                        cmb_def_loads.SelectedIndex = 2;
                    if (tn.Parent.Text.StartsWith("MEMB"))
                        cmb_def_loads.SelectedIndex = 3;
                    if (tn.Parent.Text.StartsWith("ELE"))
                        cmb_def_loads.SelectedIndex = 4;
                    if (tn.Parent.Text.StartsWith("REP"))
                        cmb_def_loads.SelectedIndex = 5;
                    if (tn.Parent.Text.StartsWith("AREA"))
                        cmb_def_loads.SelectedIndex = 6;
                    if (tn.Parent.Text.StartsWith("FLOOR"))
                        cmb_def_loads.SelectedIndex = 7;
                    if (tn.Parent.Text.StartsWith("TEMP"))
                        cmb_def_loads.SelectedIndex = 8;
                    if (tn.Parent.Text.StartsWith("COMB"))
                        cmb_def_loads.SelectedIndex = 9;
                    if (tn.Parent.Text.StartsWith("SEIS"))
                        cmb_def_loads.SelectedIndex = 10;
                }

            }
            else
            {
                if (tn != null)
                {
                    //Load_Type = tn.Text;
                    if (tn.Text.StartsWith("JOINT LO"))
                        cmb_def_loads.SelectedIndex = 0;
                    else if (tn.Text.StartsWith("JOINT WE"))
                        cmb_def_loads.SelectedIndex = 1;
                    else if (tn.Text.StartsWith("SUPPORT"))
                        cmb_def_loads.SelectedIndex = 2;
                    else if (tn.Text.StartsWith("MEMB"))
                        cmb_def_loads.SelectedIndex = 3;
                    else if (tn.Text.StartsWith("ELE"))
                        cmb_def_loads.SelectedIndex = 4;
                    else if (tn.Text.StartsWith("REP"))
                        cmb_def_loads.SelectedIndex = 5;
                    else if (tn.Text.StartsWith("AREA"))
                        cmb_def_loads.SelectedIndex = 6;
                    else if (tn.Text.StartsWith("FLOOR"))
                        cmb_def_loads.SelectedIndex = 7;
                    else if (tn.Text.StartsWith("FLOOR"))
                        cmb_def_loads.SelectedIndex = 8;
                    else if (tn.Text.StartsWith("COMB"))
                        cmb_def_loads.SelectedIndex = 9;
                    else if (tn.Text.StartsWith("SEIS"))
                        cmb_def_loads.SelectedIndex = 10;
                    else
                    {
                        MyList ml = new MyList(tn.Text, ':');
                        //cmb_def_loads.SelectedIndex = 9;

                        for (int i = 0; i < LoadCases.Count; i++)
                        {
                            if(ml.StringList.Count > 1)
                            if (ml.StringList[1].Contains(":"))
                                cmb_def_loads.SelectedIndex = -1;
                            else
                            {
                                if (LoadCases[i].LoadNo == ml.GetInt(0))
                                {
                                    if (LoadCases[i].Is_Load_Combination)
                                        cmb_def_loads.SelectedIndex = 9;
                                    else
                                        cmb_def_loads.SelectedIndex = 5;
                                    break;
                                }
                            }
                        }
                    }
                }
            }


            JointLoadCollection lds = AST_DOC.JointLoads;
            lds.Clear();
            for (int i = 0; i < LoadCases.Count; i++)
            {
                foreach (var item in LoadCases[i].JointLoads)
                {
                    lds.AddTXT(item.Value, i+1);
                }



                foreach (var item in lds)
                {
                    item.Joint = AST_DOC.Joints[item.Joint.NodeNo - 1];
                }
            }
            if (tn.Parent == null)
            {
                grb_def_loads.Enabled = true;
            }
            else
            {
                if (tn.Parent.Text.StartsWith("JOINT") ||
                    tn.Parent.Text.StartsWith("MEMBER") ||
                    tn.Parent.Text.StartsWith("ELEMENT") ||
                    tn.Parent.Text.StartsWith("AREA") ||
                    tn.Parent.Text.StartsWith("FLOOR") ||
                    tn.Parent.Text.StartsWith("TEMP") ||
                    tn.Parent.Text.StartsWith("SUPPORT") ||
                    tn.Parent.Text.StartsWith("REPEAT"))
                    tn = tn.Parent;

                if (tn.Text != "")
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    grb_def_loads.Enabled = true;
                    //grb_tload.Enabled = false;
                }

            }


            while (tn.Parent != null)
            {
                tn = tn.Parent;
            }

            MyStrings mlist = new MyStrings(tn.Text, ':');

            if (mlist.Count > 0)
            {
                //CurrentLoadIndex = mlist.GetInt(0) - 1;
                CurrentLoadIndex = tn.Index;
            }


            if (Load_Type == "")
            {
                btn_aload_edit.Enabled = false;
                btn_aload_del.Enabled = false;
                btn_aload_add.Enabled = true;
            }

            if (Load_Type != "")
            {

                btn_aload_edit.Enabled = true;
                btn_aload_del.Enabled = true;
                grb_def_loads.Enabled = true;
            }
        }
        private void Load_Selection_2015_03_20(TreeNode tn)
        {
            //tv_loadings.SelectedNode
            //Load_Type = "";
            //if (tn.Nodes.Count == 0)
            //{
            //    if (tn.Parent != null)
            //    {
            //        if (tn.Parent.Text.StartsWith("JOINT"))
            //            Load_Type = "JOINT";
            //        if (tn.Parent.Text.StartsWith("MEMBER"))
            //            Load_Type = "MEMBER";
            //        if (tn.Parent.Text.StartsWith("AREA"))
            //            Load_Type = "AREA";
            //        if (tn.Parent.Text.StartsWith("TEMP"))
            //            Load_Type = "TEMP";
            //        if (tn.Parent.Text.StartsWith("REPEAT"))
            //            Load_Type = "REPEAT";
            //        if (tn.Parent.Text.StartsWith("ELEM"))
            //            Load_Type = "ELEMENT";
            //        if (tn.Parent.Text.StartsWith("SUPPORT"))
            //            Load_Type = "SUPPORT";
            //    }

            //}

            //if (tn.Parent == null)
            //{
            //    grb_jload.Enabled = true;
            //    grb_mload.Enabled = true;
            //    grb_eload.Enabled = true;
            //    grb_def_loads.Enabled = true;
            //    grb_tload.Enabled = true;
            //    grb_cload.Enabled = true;
            //    grb_sdload.Enabled = true;


            //}
            //else
            //{
            //    if (tn.Parent.Text.StartsWith("JOINT") ||
            //        tn.Parent.Text.StartsWith("MEMBER") ||
            //        tn.Parent.Text.StartsWith("ELEMENT") ||
            //        tn.Parent.Text.StartsWith("AREA") ||
            //        tn.Parent.Text.StartsWith("TEMP") ||
            //        tn.Parent.Text.StartsWith("SUPPORT") ||
            //        tn.Parent.Text.StartsWith("REPEAT"))
            //        tn = tn.Parent;


            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = false;

            //    if (tn.Text.StartsWith("JOINT"))
            //    {
            //        grb_jload.Enabled = true;
            //        //grb_mload.Enabled = false;
            //        //grb_aload.Enabled = false;
            //        //grb_tload.Enabled = false;
            //    }
            //    else if (tn.Text.StartsWith("MEMBER"))
            //    {
            //        //grb_jload.Enabled = false;
            //        grb_mload.Enabled = true;
            //        //grb_aload.Enabled = false;
            //        //grb_tload.Enabled = false;
            //    }
            //    else if (tn.Text.StartsWith("SUPPORT"))
            //    {
            //        //grb_jload.Enabled = false;
            //        grb_sdload.Enabled = true;
            //        //grb_aload.Enabled = false;
            //        //grb_tload.Enabled = false;
            //    }
            //    else if (tn.Text.StartsWith("ELEMENT"))
            //    {
            //        //grb_jload.Enabled = false;
            //        grb_eload.Enabled = true;
            //        //grb_aload.Enabled = false;
            //        //grb_tload.Enabled = false;
            //    }
            //    else if (tn.Text.StartsWith("AREA"))
            //    {
            //        //grb_jload.Enabled = false;
            //        //grb_mload.Enabled = false;
            //        grb_def_loads.Enabled = true;
            //        //grb_tload.Enabled = false;
            //    }
            //    else if (tn.Text.StartsWith("TEMP"))
            //    {
            //        //grb_jload.Enabled = false;
            //        //grb_mload.Enabled = false;
            //        //grb_aload.Enabled = false;
            //        grb_tload.Enabled = true;
            //    }
            //    else if (tn.Text.StartsWith("REP"))
            //    {
            //        //grb_jload.Enabled = false;
            //        //grb_mload.Enabled = false;
            //        //grb_aload.Enabled = false;
            //        grb_rload.Enabled = true;
            //    }

            //}


            //while (tn.Parent != null)
            //{
            //    tn = tn.Parent;
            //}

            //MyStrings mlist = new MyStrings(tn.Text, ':');

            //if (mlist.Count > 0)
            //{
            //    //CurrentLoadIndex = mlist.GetInt(0) - 1;
            //    CurrentLoadIndex = tn.Index;
            //}


            //grb_jload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            //grb_mload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            //grb_eload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            //grb_def_loads.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            //grb_tload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            //grb_rload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            //grb_sdload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;


            ////btn_jload_edit.Enabled = (Current_LoadCase.JointLoads.Count > 0);
            ////btn_jload_del.Enabled = (Current_LoadCase.JointLoads.Count > 0);

            ////btn_mload_edit.Enabled = (Current_LoadCase.MemberLoads.Count > 0);
            ////btn_mload_del.Enabled = (Current_LoadCase.MemberLoads.Count > 0);

            ////btn_aload_edit.Enabled = (Current_LoadCase.AreaLoads.Count > 0);
            ////btn_aload_del.Enabled = (Current_LoadCase.AreaLoads.Count > 0);

            ////btn_tload_edit.Enabled = (Current_LoadCase.TemperatureLoads.Count > 0);
            ////btn_tload_del.Enabled = (Current_LoadCase.TemperatureLoads.Count > 0);


            //btn_cload_edit.Enabled = (Current_LoadCase.Comb_Loads.Count > 0);
            //btn_cload_del.Enabled = (Current_LoadCase.Comb_Loads.Count > 0);

            //if (Load_Type == "")
            //{
            //    btn_jload_edit.Enabled = false;
            //    btn_jload_del.Enabled = false;
            //    btn_mload_edit.Enabled = false;
            //    btn_mload_del.Enabled = false;
            //    btn_eload_edit.Enabled = false;
            //    btn_eload_del.Enabled = false;
            //    btn_aload_edit.Enabled = false;
            //    btn_aload_del.Enabled = false;
            //    btn_tload_edit.Enabled = false;
            //    btn_tload_del.Enabled = false;

            //    btn_rload_edit.Enabled = false;
            //    btn_rload_del.Enabled = false;

            //    btn_sdload_edit.Enabled = false;
            //    btn_sdload_del.Enabled = false;



            //    btn_jload_add.Enabled = true;
            //    btn_mload_add.Enabled = true;
            //    btn_eload_add.Enabled = true;
            //    btn_aload_add.Enabled = true;
            //    btn_tload_add.Enabled = true;
            //    btn_cload_add.Enabled = true;
            //    btn_rload_add.Enabled = true;

            //    btn_sdload_add.Enabled = true;



            //}

            //if (Load_Type == "JOINT")
            //{

            //    btn_jload_edit.Enabled = true;
            //    btn_jload_del.Enabled = true;

            //    grb_jload.Enabled = true;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = false;
            //}
            //else if (Load_Type == "MEMBER")
            //{

            //    btn_mload_edit.Enabled = true;
            //    btn_mload_del.Enabled = true;

            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = true;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = false;
            //}
            //else if (Load_Type == "ELEMENT")
            //{

            //    btn_eload_edit.Enabled = true;
            //    btn_eload_del.Enabled = true;

            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = true;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = false;
            //}
            //else if (Load_Type == "AREA")
            //{

            //    btn_aload_edit.Enabled = true;
            //    btn_aload_del.Enabled = true;

            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = true;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = false;
            //}
            //else if (Load_Type == "TEMP")
            //{

            //    btn_tload_edit.Enabled = true;
            //    btn_tload_del.Enabled = true;

            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = true;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = false;
            //}
            //else if (Load_Type == "REPEAT")
            //{

            //    btn_rload_edit.Enabled = true;
            //    btn_rload_del.Enabled = true;

            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = true;
            //    grb_sdload.Enabled = false;
            //}
            //else if (Load_Type == "SUPPORT")
            //{

            //    btn_sdload_edit.Enabled = true;
            //    btn_sdload_del.Enabled = true;

            //    grb_jload.Enabled = false;
            //    grb_mload.Enabled = false;
            //    grb_eload.Enabled = false;
            //    grb_def_loads.Enabled = false;
            //    grb_tload.Enabled = false;
            //    grb_rload.Enabled = false;
            //    grb_sdload.Enabled = true;
            //}
        }

        private void frm_DrawingToData_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btn_mov_def_load_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            TreeNode tn = tv_mov_def_load.SelectedNode;


            int type = 0;
            int indx = -1;
            LiveLoad LL = null;

            if (tn != null)
            {
                while (tn.Parent != null) tn = tn.Parent;
                LL = LL_Definition[tn.Index];
                indx = tn.Index;
            }







            #region Define Load Case
            if (btn.Name == btn_mov_def_load_add.Name)
            {
                frmLiveLoad fs = new frmLiveLoad(ACad, MovingLoads);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                //fs.txt_type.Text = (tv_mov_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LLD != null)
                {
                    tn = new TreeNode(fs.LLD.ToString());

                    LL_Definition.Add(fs.LLD);
                    tv_mov_def_load.Nodes.Add(tn);
                    tn.Nodes.Add("X-Distance : " + fs.LLD.X_Distance);
                    tn.Nodes.Add("Y-Distance : " + fs.LLD.Y_Distance);
                    tn.Nodes.Add("Z-Distance : " + fs.LLD.Z_Distance);
                    if (fs.LLD.X_Increment != 0.0)
                        tn.Nodes.Add("X_Increment : " + fs.LLD.X_Increment);
                    if (fs.LLD.Y_Increment != 0.0)
                        tn.Nodes.Add("Y_Increment : " + fs.LLD.Y_Increment);
                    if (fs.LLD.Z_Increment != 0.0)
                        tn.Nodes.Add("Z_Increment : " + fs.LLD.Z_Increment);
                    tn.Nodes.Add("Impact_Factor : " + fs.LLD.Impact_Factor);
                }
            }
            else if (btn.Name == btn_mov_def_load_edit.Name)
            {

                frmLiveLoad fs = new frmLiveLoad(ACad, MovingLoads);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.LLD = LL;
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LLD != null)
                {
                    tn = tv_mov_def_load.Nodes[indx];

                    LL_Definition[indx] = (fs.LLD);
                    tn.Text = fs.LLD.ToString();

                    tn.Nodes.Clear();

                    tn.Nodes.Add("X-Distance : " + fs.LLD.X_Distance);
                    tn.Nodes.Add("Y-Distance : " + fs.LLD.Y_Distance);
                    tn.Nodes.Add("Z-Distance : " + fs.LLD.Z_Distance);
                    if (fs.LLD.X_Increment != 0.0)
                        tn.Nodes.Add("X_Increment : " + fs.LLD.X_Increment);
                    if (fs.LLD.Y_Increment != 0.0)
                        tn.Nodes.Add("Y_Increment : " + fs.LLD.Y_Increment);
                    if (fs.LLD.Z_Increment != 0.0)
                        tn.Nodes.Add("Z_Increment : " + fs.LLD.Z_Increment);
                    tn.Nodes.Add("Impact_Factor : " + fs.LLD.Impact_Factor);
                }
            }
            else if (btn.Name == btn_mov_def_load_del.Name)
            {
                if (indx != -1)
                    tv_mov_def_load.Nodes.RemoveAt(indx);
            }
            #endregion Define Load Case
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Save_Data();
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
                user_path = Path.GetDirectoryName(VDoc.FileName);
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

            Save_Data(File_Name);
        }

        private void Save_Data(string fname)
        {

            //string fname = Path.Combine(Path.GetDirectoryName(txt_file_name.Text),
            //               Path.GetFileNameWithoutExtension(txt_file_name.Text) + ".TXT");
            //string fname = "";

            //if (IsDrawingFileOpen)


            //if (!File.Exists(File_Name))
            //{
            //    using (SaveFileDialog sfd = new SaveFileDialog())
            //    {
            //        sfd.Filter = "TEXT Files (*.txt)|*.txt";
            //        if (sfd.ShowDialog() != DialogResult.Cancel)
            //            File_Name = sfd.FileName;
            //        else
            //            return;
            //    }
            //}

            //fname = File_Name;



            //using (SaveFileDialog sfd = new SaveFileDialog())
            //{
            //    sfd.Filter = "TEXT Files (*.txt)|*.txt";
            //    if (sfd.ShowDialog() != DialogResult.Cancel)
            //    {
            //        fname = sfd.FileName;
            //    }
            //    else
            //        return;
            //}


            List<string> list = new List<string>();
            string kStr = "";

            int i = 0;
            list.Add(string.Format("ASTRA {0} {1}", cmb_structure_type.Text, txtUserTitle.Text));
            list.Add(string.Format("UNIT {0} {1}", cmb_Base_MUnit.Text, cmb_Base_LUnit.Text));

            list.Add(string.Format("JOINT COORDINATES"));

            for (i = 0; i < dgv_joints.RowCount; i++)
            {
                list.Add(string.Format("{0,-5} {1,10} {2,10} {3,10}", dgv_joints[0, i].Value,
                    dgv_joints[1, i].Value, dgv_joints[2, i].Value, dgv_joints[3, i].Value));

            }
            list.Add(string.Format("MEMBER INCIDENCES"));
            for (i = 0; i < dgv_members.RowCount; i++)
            {
                list.Add(string.Format("{0,-5} {1,10} {2,10}",
                    dgv_members[0, i].Value,
                    dgv_members[2, i].Value,
                    dgv_members[3, i].Value));

            }

            if (dgv_elements.RowCount > 1)
            {
                list.Add(string.Format("ELEMENT CONNECTIVITY"));
                for (i = 0; i < dgv_elements.RowCount; i++)
                {
                    list.Add(string.Format("{0,-5} {1,4} {2,4} {3,4} {4,4}",
                        dgv_elements[0, i].Value,
                        dgv_elements[1, i].Value,
                        dgv_elements[2, i].Value,
                        dgv_elements[3, i].Value,
                        dgv_elements[4, i].Value));
                }
            }
            if (tv_mem_grps.Nodes.Count > 0)
            {
                list.Add(string.Format("START GROUP DEFINITION"));
                for (i = 0; i < tv_mem_grps.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_mem_grps.Nodes[i].Text));

                }
                list.Add(string.Format("END GROUP DEFINITION"));
            }
            if (tv_mem_props.Nodes.Count > 0)
            {

                if (cmb_Prop_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex)
                {
                    list.Add(string.Format("UNIT {0}", cmb_Prop_LUnit.Text));
                }

                list.Add(string.Format("MEMBER PROPERTY"));
                for (i = 0; i < tv_mem_props.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_mem_props.Nodes[i].Text));
                }
            }
            if (tv_elem_props.Nodes.Count > 0)
            {
                list.Add(string.Format("ELEMENT PROPERTY"));
                for (i = 0; i < tv_elem_props.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_elem_props.Nodes[i].Text));
                }
            }
            if (tv_mem_spec_truss.Nodes.Count > 0)
            {
                for (i = 0; i < tv_mem_spec_truss.Nodes.Count; i++)
                {
                    list.Add(string.Format("MEMBER TRUSS"));
                    list.Add(string.Format("{0}", tv_mem_spec_truss.Nodes[i].Text));
                }
            }
            if (tv_mem_spec_cable.Nodes.Count > 0)
            {
                for (i = 0; i < tv_mem_spec_cable.Nodes.Count; i++)
                {
                    list.Add(string.Format("MEMBER CABLE"));
                    list.Add(string.Format("{0}", tv_mem_spec_cable.Nodes[i].Text));
                }
            }
            if (tv_mem_release.Nodes.Count > 0)
            {
                list.Add(string.Format("MEMBER RELEASE"));
                for (i = 0; i < tv_mem_release.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_mem_release.Nodes[i].Text));
                }
            }
            if (tv_constants.Nodes.Count > 0)
            {
                if (cmb_Const_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex ||
                  cmb_Const_MUnit.SelectedIndex != cmb_Base_MUnit.SelectedIndex)
                {
                    string s1 = "";
                    string s2 = "";

                    if (cmb_Const_MUnit.SelectedIndex != cmb_Base_MUnit.SelectedIndex)
                        s1 = cmb_Const_MUnit.Text;
                    if (cmb_Const_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex)
                        s1 = cmb_Const_LUnit.Text;

                    if (s1 != "" || s2 != "")
                        list.Add(string.Format("UNIT {0} {1}", s1, s2));
                }

                list.Add(string.Format("MATERIAL CONSTANT"));
                for (i = 0; i < tv_constants.Nodes.Count; i++)
                {
                    for (int j = 0; j < tv_constants.Nodes[i].Nodes.Count; j++)
                    {
                        list.Add(string.Format("{0}", tv_constants.Nodes[i].Nodes[j].Text));
                    }
                }
            }
            if (tv_supports.Nodes.Count > 0)
            {
                list.Add(string.Format("SUPPORTS"));
                for (i = 0; i < tv_supports.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_supports.Nodes[i].Text));
                }
            }
            if (chk_selfweight.Checked)
            {
                list.Add(string.Format("SELFWEIGHT {0} {1}", cmb_Self_Weight.Text, txt_seft_wt_Value.Text));
            }
            if (tv_loads.Nodes.Count > 0)
            {
                if (cmb_Load_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex ||
                    cmb_Load_MUnit.SelectedIndex != cmb_Base_MUnit.SelectedIndex)
                {
                    list.Add(string.Format("UNIT {0} {1}", cmb_Load_MUnit.Text, cmb_Load_LUnit.Text));
                }


                for (i = 0; i < LoadCases.Count; i++)
                {

                    if (LoadCases[i].Comb_Loads.Count > 0)
                    {
                        list.Add(string.Format("LOAD COMB {0} {1}", LoadCases[i].Comb_Loads.LoadNo, LoadCases[i].Comb_Loads.Name));
                        LoadCases[i].Comb_Loads.Set_Combination();
                        list.Add(string.Format("{0}", LoadCases[i].Comb_Loads.Data));
                        continue;
                    }
                    else
                        list.Add(string.Format("LOAD {0} {1}", LoadCases[i].LoadNo, LoadCases[i].Title));


                    if (LoadCases[i].RepeatLoads.Count > 0)
                    {
                        LoadCases[i].RepeatLoads.Set_Combination();
                        list.Add(string.Format("REPEAT LOAD"));
                        list.Add(string.Format("{0}", LoadCases[i].RepeatLoads.Data));
                        //foreach (var item in LoadCases[i].RepeatLoads)
                        //{

                        //     ;
                        //    list.Add(string.Format("{0}", LoadCases[i].RepeatLoads.Data));

                        //}
                    }

                    //for (int v = 0; v < LoadCases[i].RepeatLoads.Count;v++)
                    //{

                    //    list.Add(string.Format("{0}", LoadCases[i].RepeatLoads.G

                    //}


                    if (LoadCases[i].JointWeights.Count > 0)
                        list.Add(string.Format("JOINT WEIGHT"));
                    foreach (var item in LoadCases[i].JointWeights)
                    {
                        list.Add(string.Format("{0}", item));

                    }

                    if (LoadCases[i].JointLoads.Count > 0)
                        list.Add(string.Format("JOINT LOAD"));
                    foreach (var item in LoadCases[i].JointLoads)
                    {
                        list.Add(string.Format("{0}", item.Value));

                    }

                    if (LoadCases[i].MemberLoads.Count > 0)
                        list.Add(string.Format("MEMBER LOAD"));
                    foreach (var item in LoadCases[i].MemberLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }
                    if (LoadCases[i].ElementLoads.Count > 0)
                        list.Add(string.Format("ELEMENT LOAD"));
                    foreach (var item in LoadCases[i].ElementLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }
                    if (LoadCases[i].AreaLoads.Count > 0)
                        list.Add(string.Format("AREA LOAD"));
                    foreach (var item in LoadCases[i].AreaLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }
                    if (LoadCases[i].TemperatureLoads.Count > 0)
                        list.Add(string.Format("TEMP LOAD"));
                    foreach (var item in LoadCases[i].TemperatureLoads)
                    {
                        list.Add(string.Format("{0}", item));
                    }

                    if (LoadCases[i].SupportDisplacementLoads.Count > 0)
                        list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
                    foreach (var item in LoadCases[i].SupportDisplacementLoads)
                    {
                        list.Add(string.Format("{0}", item));
                    }

                    if (LoadCases[i].FloorLoads.Count > 0)
                        list.Add(string.Format("FLOOR LOAD"));
                    foreach (var item in LoadCases[i].FloorLoads)
                    {
                        if (item.Contains("ONE"))
                            list.Add(string.Format("ONEWAY LOAD", item));
                        list.Add(string.Format("{0}", item));
                    }


                }

                list.AddRange(Seismic_Load.ToArray());
                list.AddRange(Seismic_Combinations.ToArray());

                if (Seismic_Coeeficient != 0)
                {
                    list.Add(string.Format("SEISMIC COEEFICIENT {0:f3}", Seismic_Coeeficient));
                    //list.Add(string.Format("SEISMIC LOAD"));
                    //list.Add(string.Format("{0}", SeismicLoads[0]));
                }

                //for (i = 0; i < Combinations.Count; i++)
                //{
                //    list.Add(string.Format("LOAD COMB {0} {1}", Combinations[i].LoadNo, Combinations[i].Name));

                //    foreach (var item in Combinations)
                //    {
                //        item.Set_Combination();
                //        list.Add(string.Format("{0}", item.Data));
                //    }
                //}

            }

            if (chk_dynamic_analysis.Checked)
            {
                if (rbtn_perform_eigen.Checked)
                {
                    list.Add(string.Format("PERFORM EIGEN VALUES ANALYSIS"));
                    list.Add(string.Format("FREQUENCIES {0}", txt_frequencies.Text));
                }
                else
                {
                    list.AddRange(txt_dynamic.Lines);
                }
                //list.Add(string.Format(""));
            }
            else
            {

                if (tv_mov_def_load.Nodes.Count > 0)
                {
                    List<string> ll = new List<string>();
                    ll.Add("");
                    ll.Add("FILE LL.TXT");
                    ll.Add("");
                    for (i = 0; i < MovingLoads.Count; i++)
                    {
                        ll.Add("");
                        ll.Add(MovingLoads[i].ToString());
                        ll.Add(MovingLoads[i].Loads.Replace(",", " "));
                        ll.Add(MovingLoads[i].Distances.Replace(",", " "));
                        ll.Add(MovingLoads[i].LoadWidth.ToString("f3"));
                        ll.Add("");
                        ll.Add("");

                    }
                    File.WriteAllLines(Path.Combine(Path.GetDirectoryName(fname), "LL.TXT"), ll.ToArray());

                    rtb_ll_txt.Lines = ll.ToArray();

                    list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                    for (i = 0; i < LL_Definition.Count; i++)
                    {
                        list.Add(string.Format("TYPE {0} {1} {2:f3}", LL_Definition[i].Type,
                            Get_Load_Name(LL_Definition[i].Type),
                            LL_Definition[i].Impact_Factor));
                    }
                    list.Add(string.Format("LOAD GENERATION {0}", txt_load_gen.Text));
                    for (i = 0; i < LL_Definition.Count; i++)
                    {
                        list.Add(string.Format("{0}", LL_Definition[i].ToString()));
                    }
                }
            }
            if (chk_perform_ana.Checked)
                list.Add(string.Format(chk_perform_ana.Text));
            if (chk_print_supp_reac.Checked)
                list.Add(string.Format(chk_print_supp_reac.Text));
            if (chk_print_static_check.Checked)
                list.Add(string.Format(chk_print_static_check.Text));
            if (chk_print_load_data.Checked)
                list.Add(string.Format(chk_print_load_data.Text));
            if (chk_print_ana_all.Checked)
                list.Add(string.Format(chk_print_ana_all.Text));
            if (chk_print_max_force.Checked)
            {
                for (i = 0; i < tv_max_frc.Nodes.Count; i++)
                {
                    list.Add(string.Format("PRINT MAX FORCE ENVELOPE LIST {0}", tv_max_frc.Nodes[i].Text));
                }
            }

            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            rtb_input_file.Lines = list.ToArray();

            AST_DOC = new ASTRADoc(fname);
            File.WriteAllLines(fname, list.ToArray());

            IsSavedData = true;
            Write_All_Data();

            //if (MessageBox.Show("ASTRA Data file created as \n\r" + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
            if (MessageBox.Show(this, "Data file Saved as " + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start(fname);
            }
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

        private void chk_selfweight_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Self_Weight.Enabled = chk_selfweight.Checked;
            txt_seft_wt_Value.Enabled = chk_selfweight.Checked;
        }

        private void tv_mem_grps_MouseMove(object sender, MouseEventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (tv.Name == tv_mem_grps.Name)
            {
                btn_grps_edit.Enabled = (tv_mem_grps.SelectedNode != null);
                btn_grps_del.Enabled = (tv_mem_grps.SelectedNode != null);
            }
            else if (tv.Name == tv_elem_props.Name)
            {
                btn_elem_props_edit.Enabled = (tv_elem_props.SelectedNode != null);
                btn_elem_props_del.Enabled = (tv_elem_props.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_props.Name)
            {
                btn_props_edit.Enabled = (tv_mem_props.SelectedNode != null);
                btn_props_del.Enabled = (tv_mem_props.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_spec_truss.Name)
            {
                btn_spec_edit_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);
                btn_spec_del_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_spec_cable.Name)
            {
                btn_spec_edit_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);
                btn_spec_del_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_release.Name)
            {
                btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
                btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_release.Name)
            {
                btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
                btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);
            }
            else if (tv.Name == tv_constants.Name)
            {
                btn_cons_edit.Enabled = (tv_constants.SelectedNode != null);
                btn_cons_del.Enabled = (tv_constants.SelectedNode != null);
            }
            else if (tv.Name == tv_supports.Name)
            {
                btn_supp_edit.Enabled = (tv_supports.SelectedNode != null);
                btn_supp_del.Enabled = (tv_supports.SelectedNode != null);
            }
            else if (tv.Name == tv_loads.Name)
            {
                btn_ldc_edit.Enabled = (tv_loads.SelectedNode != null);
                btn_ldc_del.Enabled = (tv_loads.SelectedNode != null);
            }
            else if (tv.Name == tv_mov_loads.Name)
            {
                btn_mov_load_edit.Enabled = (tv_mov_loads.SelectedNode != null);
                btn_mov_load_del.Enabled = (tv_mov_loads.SelectedNode != null);
            }
            else if (tv.Name == tv_mov_def_load.Name)
            {
                btn_mov_def_load_edit.Enabled = (tv_mov_def_load.SelectedNode != null);
                btn_mov_def_load_del.Enabled = (tv_mov_def_load.SelectedNode != null);
            }
        }

        private void Button_Enable_Disable()
        {

            btn_grps_edit.Enabled = (tv_mem_grps.SelectedNode != null);
            btn_grps_del.Enabled = (tv_mem_grps.SelectedNode != null);

            btn_props_edit.Enabled = (tv_mem_props.SelectedNode != null);
            btn_props_del.Enabled = (tv_mem_props.SelectedNode != null);

            btn_elem_props_edit.Enabled = (tv_elem_props.SelectedNode != null);
            btn_elem_props_del.Enabled = (tv_elem_props.SelectedNode != null);


            btn_spec_edit_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);
            btn_spec_del_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);

            btn_spec_edit_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);
            btn_spec_del_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);

            btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
            btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);

            btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
            btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);

            btn_cons_edit.Enabled = (tv_constants.SelectedNode != null);
            btn_cons_del.Enabled = (tv_constants.SelectedNode != null);

            btn_supp_edit.Enabled = (tv_supports.SelectedNode != null);
            btn_supp_del.Enabled = (tv_supports.SelectedNode != null);

            btn_ldc_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_ldc_del.Enabled = (tv_loads.SelectedNode != null);

            if (tv_loads.SelectedNode == null)
            {
                //btn_jload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_jload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_jload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_mload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_mload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_mload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_aload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_aload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_aload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_tload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_tload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_tload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_rload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_rload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_rload_del.Enabled = (tv_loadings.SelectedNode != null);
            }
            else
                Load_Selection(tv_loads.SelectedNode);

            btn_mov_load_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_mov_load_del.Enabled = (tv_loads.SelectedNode != null);

            btn_mov_def_load_edit.Enabled = (tv_mov_def_load.SelectedNode != null);
            btn_mov_def_load_del.Enabled = (tv_mov_def_load.SelectedNode != null);



        }

        private void btn_max_frc_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_max_frc_add.Name)
            {
                if (txt_max_frc_list.Text != "")
                    tv_max_frc.Nodes.Add(txt_max_frc_list.Text);
            }
            else if (btn.Name == btn_max_frc_del.Name)
            {
                if (tv_max_frc.SelectedNode != null)
                    tv_max_frc.Nodes.Remove(tv_max_frc.SelectedNode);
            }
        }

        private void tsmi_data_open_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            ToolStripItem tsmi = sender as ToolStripItem;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                #region Drawing Open
                ofd.InitialDirectory = work_fold;
                if (tsmi.Name == tsmi_file_open.Name || tsmi.Name == tsb_file_open.Name)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {

                        Drawing_File = ofd.FileName;

                        IsDrawingFileOpen = true;
                        this.Text = "Building Frame Analysis Design [" + MyStrings.Get_Modified_Path(Drawing_File) + "]";

                        File_Name = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                        if (File.Exists(File_Name))
                        {
                            Open_Data_File(File_Name);
                        }
                        else
                            Open_Drawing_File();

                    }
                }
                #endregion Drawing Open

                #region Data Open
                else if (tsmi.Name == tsmi_data_open.Name || tsmi.Name == tsb_open_data.Name)
                {
                    ofd.Filter = "ASTRA Data Files (*.txt)|*.txt";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        File_Name = ofd.FileName;
                        Drawing_File = "";
                        IsDrawingFileOpen = false;
                        this.Text = "Building Frame Analysis Design [" + MyStrings.Get_Modified_Path(File_Name) + "]";
                        Open_Data_File(File_Name);
                    }
                }
                #endregion Data Open

                #region Drawing Open
                else if (tsmi.Name == tsb_open_data.Name)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg|ASTRA Data Files (*.txt)|*.txt";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {

                        if (Path.GetExtension(ofd.FileName).ToUpper() != ".TXT")
                        {
                            Drawing_File = ofd.FileName;
                            IsDrawingFileOpen = true;
                            File_Name = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");
                            this.Text = "Building Frame Analysis Design [" + MyStrings.Get_Modified_Path(ofd.FileName) + "]";


                            if (File.Exists(File_Name))
                            {
                                Open_Data_File(File_Name);
                            }
                            else
                                Open_Drawing_File();
                        }
                        else
                        {
                            File_Name = ofd.FileName;
                            Drawing_File = "";
                            IsDrawingFileOpen = false;
                            this.Text = "Building Frame Analysis Design [" + MyStrings.Get_Modified_Path(File_Name) + "]";
                            Open_Data_File(File_Name);
                        }

                    }
                }
                #endregion Drawing Open

                IsSavedData = false;
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

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




            //AST_DOC.Members.DrawMember(VDoc);
            AST_DOC.Joints.DrawJointsText(VDoc, Text_Size);
            AST_DOC.Members.DrawMember(VDoc, Text_Size);
            AST_DOC.Elements.DrawElements(VDoc);
            AST_DOC.Supports.DrawSupport(VDoc);

            chk_dynamic_analysis.Checked = false;
            rbtn_perform_eigen.Checked = true;
            txt_dynamic.Text = "";

            cmb_structure_type.SelectedItem = AST_DOC.StructureType;
            txtUserTitle.Text = AST_DOC.UserTitle;


            cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;
            cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;





            cmb_Const_MUnit.SelectedIndex = (int)AST_DOC.Const_MassUnit;
            cmb_Const_LUnit.SelectedIndex = (int)AST_DOC.Const_LengthUnit;


            cmb_Prop_LUnit.SelectedIndex = (int)AST_DOC.Prop_LengthUnit;
            cmb_Load_MUnit.SelectedIndex = (int)AST_DOC.Load_MassUnit;
            cmb_Load_LUnit.SelectedIndex = (int)AST_DOC.Load_LengthUnit;




            SetGridWithNode();
            SetGridWithMember();
            SetGridWithElement();

            Groups.Clear();
            tv_mem_grps.Nodes.Clear();
            foreach (var item in AST_DOC.MemberGroups)
            {
                tv_mem_grps.Nodes.Add(item);

                MemberGroup mg = new MemberGroup();
                MyStrings ml = new MyStrings(item, ' ');

                mg.GroupName = ml.StringList[0];
                mg.MemberNosText = ml.GetString(1);

                Groups.Add(mg);
            }

            tv_mem_props.Nodes.Clear();
            foreach (var item in AST_DOC.MemberProps)
            {
                tv_mem_props.Nodes.Add(item);
            }
            tv_elem_props.Nodes.Clear();
            foreach (var item in AST_DOC.ElementProps)
            {
                tv_elem_props.Nodes.Add(item);
            }
            tv_mem_spec_cable.Nodes.Clear();
            foreach (var item in AST_DOC.MemberCables)
            {
                tv_mem_spec_cable.Nodes.Add(item);
            }
            tv_mem_spec_truss.Nodes.Clear();
            foreach (var item in AST_DOC.MemberTrusses)
            {
                tv_mem_spec_truss.Nodes.Add(item);
            }

            tv_constants.Nodes.Clear();
            Materials.Clear();
            for (int i = 0; i < AST_DOC.Constants.Count; i++)
            {
                tv_constants.Nodes.Add("MATERIAL_" + (i + 1));
                foreach (var item in AST_DOC.Constants[i].ASTRA_Data)
                {
                    tv_constants.Nodes[i].Nodes.Add(item);
                }
                Materials.Add(AST_DOC.Constants[i]);
            }

            tv_supports.Nodes.Clear();
            foreach (var item in AST_DOC.JointSupports)
            {
                tv_supports.Nodes.Add(item);
            }

            if (AST_DOC.SelfWeight != "")
            {
                chk_selfweight.Checked = true;
                cmb_Self_Weight.SelectedItem = AST_DOC.SelfWeight_Direction;
                txt_seft_wt_Value.Text = AST_DOC.SelfWeight;
            }
            tv_loads.Nodes.Clear();
            LoadCases.Clear();
            foreach (var item in AST_DOC.LoadDefines)
            {
                LoadCaseDefinition lcd = new LoadCaseDefinition();
                lcd.LoadNo = item.LoadCase;
                lcd.Title = item.LoadTitle;
                lcd.JointWeights = item.JointWeightList;
                lcd.JointLoads = item.JointLoadList;
                lcd.MemberLoads = item.MemberLoadList;
                lcd.ElementLoads = item.ElementLoadList;
                lcd.FloorLoads = item.FloorLoadList;
                lcd.AreaLoads = item.AreaLoadList;
                lcd.TemperatureLoads = item.TempLoadList;
                lcd.SupportDisplacementLoads = item.SupportDisplacements;


                if (item.CominationLoadList.Count > 0)
                {
                    item.Set_Combination();
                    lcd.Comb_Loads.LoadNo = item.LoadCase;
                    lcd.Comb_Loads.Name = item.LoadTitle;
                    foreach (var cm in item.Combinations)
                    {
                        lcd.Comb_Loads.LoadCases.Add(cm.Load_No);
                        lcd.Comb_Loads.Factors.Add(cm.Load_Factor);
                    }
                    lcd.Comb_Loads.Set_Combination();
                }

                if (item.RepeatLoadList.Count > 0)
                {
                    item.Set_Combination();
                    lcd.RepeatLoads.LoadNo = item.LoadCase;
                    lcd.RepeatLoads.Name = item.LoadTitle;

                    foreach (var cm in item.Combinations)
                    {
                        lcd.RepeatLoads.LoadCases.Add(cm.Load_No);
                        lcd.RepeatLoads.Factors.Add(cm.Load_Factor);
                    }
                    lcd.RepeatLoads.Set_Combination();
                }
                //lcd.Comb_Loads = item.CominationLoadList;
                //lcd.RepeatLoads = item.RepeatLoadList;

                LoadCases.Add(lcd);

            }
            #region   LoadCases
            if (LoadCases.Count > 0)
            {
                TreeNode tn = null;
                TreeNode tjn = null;
                tv_loads.Nodes.Clear();
                foreach (var item in LoadCases)
                {
                    tv_loads.Nodes.Add(item.ToString());

                    tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];

                    if (item.JointWeights.Count > 0)
                    {

                        tn.Nodes.Add("JOINT WEIGHT");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.JointWeights)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }

                    if (item.RepeatLoads.Count > 0)
                    {
                        //item.Set_Combination();
                        tn.Nodes.Add("REPEAT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.RepeatLoads)
                        {
                            tn.Nodes.Add(item1.ToString());
                        }
                        tn = tn.Parent;
                    }
                    if (item.MemberLoads.Count > 0)
                    {
                        tn.Nodes.Add("MEMBER LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.MemberLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;

                    }
                    if (item.ElementLoads.Count > 0)
                    {
                        tn.Nodes.Add("ELEMENT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.ElementLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;

                    }
                    if (item.JointLoads.Count > 0)
                    {

                        tn.Nodes.Add("JOINT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.JointLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.SupportDisplacementLoads.Count > 0)
                    {

                        tn.Nodes.Add("SUPPORT DISPLACEMENT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.SupportDisplacementLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.AreaLoads.Count > 0)
                    {

                        tn.Nodes.Add("AREA LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.AreaLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.FloorLoads.Count > 0)
                    {

                        tn.Nodes.Add("FLOOR LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.FloorLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.TemperatureLoads.Count > 0)
                    {

                        tn.Nodes.Add("TEMP LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.TemperatureLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.Comb_Loads.Count > 0)
                    {
                        //item.Set_Combination();
                        //tn.Nodes.Add("COMBINATION");
                        //if (tn.Nodes.Count > 0)
                        //    tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.Comb_Loads)
                        {
                            tn.Nodes.Add(item1.ToString());
                        }
                        //tn = tn.Parent;
                    }
                    //if (item.ElementLoad.Count > 0)
                    //{
                    //    tn.Nodes.Add("ELEMENT LOAD");
                    //    tn = tn.Nodes[0];
                    //    foreach (var item1 in item.ElementLoadList)
                    //    {
                    //        //tn.Nodes.Add(item1 + " " + astDoc.MassUnit + "/" + astDoc.LengthUnit);
                    //        tn.Nodes.Add(item1);
                    //    }
                    //    tn = tn.Parent;
                    //}
                }
                SeismicLoads = AST_DOC.SeismicLoads;
                if (SeismicLoads.Count > 0)
                {
                    tv_loads.Nodes.Add("SEISMIC LOAD");
                    tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];
                    tn.Nodes.Add(SeismicLoads[0]);
                }
            }
            #endregion

            #region Moving Load
            if (AST_DOC.MovingLoads.Count > 0)
            {
                TreeNode tn;
                tv_mov_loads.Nodes.Clear();
                MovingLoads.Clear();

                foreach (var item in AST_DOC.MovingLoads)
                {
                    MovingLoads.Add(item);
                    tn = new TreeNode(item.ToString());
                    tv_mov_loads.Nodes.Add(tn);
                    tn.Nodes.Add("LOADS : " + item.Loads);
                    tn.Nodes.Add("DISTANCES : " + item.Distances);
                    tn.Nodes.Add("LOAD WIDTH : " + item.LoadWidth);
                }
            }
            if (AST_DOC.LL_Definition.Count > 0)
            {


                TreeNode tn;
                tv_mov_def_load.Nodes.Clear();
                LL_Definition.Clear();

                foreach (var item in AST_DOC.LL_Definition)
                {

                    LL_Definition.Add(item);

                    tn = new TreeNode(item.ToString());

                    tv_mov_def_load.Nodes.Add(tn);
                    tn.Nodes.Add("X-Distance : " + item.X_Distance);
                    tn.Nodes.Add("Y-Distance : " + item.Y_Distance);
                    tn.Nodes.Add("Z-Distance : " + item.Z_Distance);
                    if (item.X_Increment != 0.0)
                        tn.Nodes.Add("X_Increment : " + item.X_Increment);
                    if (item.Y_Increment != 0.0)
                        tn.Nodes.Add("Y_Increment : " + item.Y_Increment);
                    if (item.Z_Increment != 0.0)
                        tn.Nodes.Add("Z_Increment : " + item.Z_Increment);
                    tn.Nodes.Add("Impact_Factor : " + item.Impact_Factor);

                }
            }
            #endregion Moving Load

            chk_seismic_coeeficient.Checked = (AST_DOC.Seismic_Coeeficient != 0.0);
            txt_seismic_coeeficient.Text = AST_DOC.Seismic_Coeeficient.ToString("f3");
            chk_perform_ana.Checked = AST_DOC.Analysis_Specification.Perform_Analysis;
            chk_print_ana_all.Checked = AST_DOC.Analysis_Specification.Print_Analysis_All;
            chk_print_load_data.Checked = AST_DOC.Analysis_Specification.Print_Load_Data;
            chk_print_static_check.Checked = AST_DOC.Analysis_Specification.Print_Static_Check;
            chk_print_supp_reac.Checked = AST_DOC.Analysis_Specification.Print_Support_Reaction;
            chk_print_max_force.Checked = AST_DOC.Analysis_Specification.Print_Max_Force;
            txt_max_frc_list.Enabled = true;
            tv_max_frc.Nodes.Clear();
            foreach (var item in AST_DOC.Analysis_Specification.List_Maxforce)
            {
                tv_max_frc.Nodes.Add(item);
            }

            if (AST_DOC.DynamicAnalysis.Count > 0)
            {
                chk_dynamic_analysis.Checked = true;

                if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM EIGEN"))
                {
                    rbtn_perform_eigen.Checked = true;
                    txt_frequencies.Text = AST_DOC.FREQUENCIES;
                }
                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM TIME HISTORY"))
                {
                    rbtn_perform_time_history.Checked = true;
                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                }
                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM RESPON"))
                {
                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                    rbtn_response_spectrum.Checked = true;
                }
            }
            //AST_DOC.DynamicAnalysis
            //return astdoc;
            Count_Geometry();

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

        private void tsmi_file_save_Click(object sender, EventArgs e)
        {
            ToolStripItem tsmi = sender as ToolStripItem;

            if (tsmi.Name == tsmi_file_save_as.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Text Data File(*.txt)|*.txt";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        DataFileName = sfd.FileName;
                        File.WriteAllLines(DataFileName, rtb_input_file.Lines);
                    }
                    else
                        return;
                }
            }
            Save_Data();
        }

        private void tsmi_file_print_Click(object sender, EventArgs e)
        {
            if (iApp.Is_StructureDemo)
            {
                MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.VDoc != null)
                {
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.PrintEx(VDoc);
                }
            }
        }

        private void tsmi_show_data_Click(object sender, EventArgs e)
        {
            if (File.Exists(File_Name))
            {
                System.Diagnostics.Process.Start(File_Name);
            }
        }

        private void tsmi_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_elem_props_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_elem_props_add.Name)
            {
                frmElementProp fs = new frmElementProp(ACad);
                fs.Owner = this;
                fs.TRV = tv_elem_props;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_elem_props_del.Name)
            {
                tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);
            }
            else if (btn.Name == btn_elem_props_edit.Name)
            {
                frmElementProp fs = new frmElementProp(ACad);
                fs.Owner = this;
                fs.TRV = tv_elem_props;
                fs.ASTRA_Data.Add(tv_elem_props.SelectedNode.Text);
                fs.ShowDialog();
            }

        }

        private void btn_mbr_add_Click(object sender, EventArgs e)
        {
            Is_TextEdit = false;

            Button btn = sender as Button;

            if (btn.Name == btn_mbr_add.Name)
            {
                frmMembers fs = new frmMembers(ACad);
                fs.Owner = this;
                fs.DGV = dgv_members;
                fs.ShowDialog();

                //Draw_Members();
            }
            else if (btn.Name == btn_mbr_delete.Name)
            {
                if (dgv_members.SelectedCells.Count > 0)
                    dgv_members.Rows.RemoveAt(dgv_members.SelectedCells[0].RowIndex);
                else if (dgv_members.CurrentCell.RowIndex > -1)
                    dgv_members.Rows.RemoveAt(dgv_members.CurrentCell.RowIndex);

                //Draw_Members();
                //tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);
            }
            else if (btn.Name == btn_mbr_edit.Name)
            {
                frmMembers fs = new frmMembers(ACad);
                fs.Owner = this;
                fs.DGV = dgv_members;
                if (dgv_members.SelectedCells.Count > 0)
                    fs.ASTRA_Data.Add(dgv_members.SelectedCells[0].RowIndex.ToString());
                else
                    fs.ASTRA_Data.Add(dgv_members.CurrentCell.RowIndex.ToString());
                fs.ShowDialog();
            }
            else if (btn.Name == btn_mbr_insert.Name)
            {
                frmMembers fs = new frmMembers(ACad);
                fs.Owner = this;
                fs.DGV = dgv_members;
                fs.ASTRA_Data.Add("INSERT");
                fs.ShowDialog();
            }
            Draw_Members();

        }

        private void btn_jnt_add_Click(object sender, EventArgs e)
        {
            Is_TextEdit = false;
            Button btn = sender as Button;

            if (btn.Name == btn_jnt_add.Name)
            {
                frmJoints fs = new frmJoints(ACad);
                fs.Owner = this;
                fs.DGV = dgv_joints;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_jnt_delete.Name)
            {
                //tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);

                if (dgv_joints.SelectedCells.Count > 0)
                    dgv_joints.Rows.RemoveAt(dgv_joints.SelectedCells[0].RowIndex);
                else if (dgv_joints.CurrentCell.RowIndex > -1)
                    dgv_joints.Rows.RemoveAt(dgv_joints.CurrentCell.RowIndex);

            }
            else if (btn.Name == btn_jnt_edit.Name)
            {
                frmJoints fs = new frmJoints(ACad);
                fs.Owner = this;
                fs.DGV = dgv_joints;
                if (dgv_joints.SelectedCells.Count > 0)
                    fs.ASTRA_Data.Add(dgv_joints.SelectedCells[0].RowIndex.ToString());
                else
                    fs.ASTRA_Data.Add(dgv_joints.CurrentCell.RowIndex.ToString());
                fs.ShowDialog();
            }
            else if (btn.Name == btn_jnt_insert.Name)
            {
                frmJoints fs = new frmJoints(ACad);
                fs.Owner = this;
                fs.DGV = dgv_joints;
                fs.ASTRA_Data.Add("INSERT");
                fs.ShowDialog();
            }
            Draw_Joints();
            Draw_Members();
            Draw_Supports();

            IsSavedData = false;
        }

        public void Draw_Joints()
        {
            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(ACad.AstraDocument);
            astdoc = (ACad.AstraDocument);


            astdoc.Joints.Clear();

            for (int i = 0; i < dgv_joints.Rows.Count; i++)
            {
                JointCoordinate jn = new JointCoordinate();

                dgv_joints[0, i].Value = i + 1;

                jn.NodeNo = MyStrings.StringToInt(dgv_joints[0, i].Value.ToString(), 0);
                jn.Point.x = MyStrings.StringToDouble(dgv_joints[1, i].Value.ToString(), 0.0);
                jn.Point.y = MyStrings.StringToDouble(dgv_joints[2, i].Value.ToString(), 0.0);
                jn.Point.z = MyStrings.StringToDouble(dgv_joints[3, i].Value.ToString(), 0.0);
                astdoc.Joints.Add(jn);
            }

            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Nodes")
                    {
                        vf.Deleted = true;
                    }
                }

            }
            VDoc.Redraw(true);
            astdoc.Joints.DrawJointsText(VDoc, Text_Size);
            Count_Geometry();
        }
        public void Draw_Supports()
        {

            #region Draw_Support
            AST_DOC.Supports.Clear();

            if (tv_supports.Nodes.Count > 0)
            {
                for (int i = 0; i < tv_supports.Nodes.Count; i++)
                {
                    AST_DOC.Supports.Add(tv_supports.Nodes[i].Text);
                }
            }

            AST_DOC.Supports.CopyFromCoordinateCollection(AST_DOC.Joints);
            AST_DOC.Supports.DrawSupport(VDoc);
            VDoc.Redraw(true);
            #endregion DrawSupport
            Count_Geometry();
        }
        public void Draw_Members()
        {


            ClearSelect();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(ACad.AstraDocument);
            astdoc = (ACad.AstraDocument);


            astdoc.Members.Clear();

            for (int i = 0; i < dgv_members.Rows.Count; i++)
            {
                MemberIncidence mbr = new MemberIncidence();
                dgv_members[0, i].Value = i + 1;

                mbr.MemberNo = MyStrings.StringToInt(dgv_members[0, i].Value.ToString(), 0);

                if (dgv_members[1, i].Value.ToString().StartsWith("C"))
                    mbr.MemberType = MembType.CABLE;
                else if (dgv_members[1, i].Value.ToString().StartsWith("T"))
                    mbr.MemberType = MembType.TRUSS;
                else
                    mbr.MemberType = MembType.BEAM;

                mbr.StartNode.NodeNo = MyStrings.StringToInt(dgv_members[2, i].Value.ToString(), 0);



                mbr.EndNode.NodeNo = MyStrings.StringToInt(dgv_members[3, i].Value.ToString(), 0);


                astdoc.Members.Add(mbr);
            }
            astdoc.Members.CopyJointCoordinates(astdoc.Joints);



            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Members" || vf.Layer.Name == "0")
                    {
                        vf.Deleted = true;
                    }
                }

            }
            VDoc.Redraw(true);

            astdoc.Members.DrawMember(VDoc, Text_Size);
            astdoc.MemberProperties.CopyMemberIncidence(astdoc.Members);
            astdoc.Members.DrawMember(VDoc, Text_Size);





            Count_Geometry();

            IsSavedData = false;
        }
        private void Draw_Loadings()
        {
            string kStr = "";

            TreeNode tn = tv_loads.SelectedNode;

            bool IsJointLoad = false;

            if (tn.Nodes.Count == 0)
            {
                if (tn.Parent != null)
                    IsJointLoad = (tn.Parent.Text.StartsWith("JOINT"));
                kStr = tn.Text;
            }

            JointLoad jn = new JointLoad();


           

            AST_DOC.JointLoads.Delete_ASTRAArrowLine(VDoc);
            AST_DOC.MemberLoads.Delete_ASTRAMemberLoad(VDoc);

            if (tn.Parent != null)
            {
                tn = tn.Parent;
                if (tv_loads.SelectedNode.Text.StartsWith("JOINT"))
                {
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                else if (tv_loads.SelectedNode.Text.StartsWith("MEMBER"))
                {
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("COMB"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                }
                if (tn.Text.StartsWith("REPEAT"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3));
                    goto _100;
                }
            }
            else
            {
                AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1);
                AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1);
                goto _100;
            }

            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;

            if (IsJointLoad)
            {
                JointLoadCollection jlc = new JointLoadCollection();
                jlc.AddTXT(kStr, tn.Index + 1);
                for (int i = 0; i < jlc.Count; i++)
                {
                    try
                    {
                        jlc[i].Joint = AST_DOC.Joints[jlc[i].Joint.NodeNo - 1];
                    }
                    catch (Exception exx)
                    {
                        jlc.RemoveAt(i); i--;
                    }
                }
                jlc.DrawJointLoads(VDoc, tn.Index + 1);
            }
            else
            {
                MemberLoadCollection mlc = new MemberLoadCollection();
                mlc.AddTxt(kStr, tn.Index + 1);

                for (int i = 0; i < mlc.Count; i++)
                {
                    try
                    {
                        mlc[i].Member = AST_DOC.Members[mlc[i].Member.MemberNo - 1];
                    }
                    catch (Exception exx)
                    {
                        mlc.RemoveAt(i); i--;
                    }
                }
                mlc.DrawMemberLoad(VDoc, tn.Index + 1);
            }
        _100:
            VDoc.Redraw(true);
        }
        public void Draw_Elements()
        {


            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(iACad.AstraDocument);
            astdoc = (ACad.AstraDocument);


            astdoc.Elements.Clear();

            DataGridView DGV = dgv_elements;

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                Element elm = new Element();
                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                elm.ElementNo = MyStrings.StringToInt(DGV[0, i].Value.ToString(), 0);
                elm.Node1.NodeNo = MyStrings.StringToInt(DGV[1, i].Value.ToString(), 0);
                elm.Node2.NodeNo = MyStrings.StringToInt(DGV[2, i].Value.ToString(), 0);
                elm.Node3.NodeNo = MyStrings.StringToInt(DGV[3, i].Value.ToString(), 0);
                elm.Node4.NodeNo = MyStrings.StringToInt(DGV[4, i].Value.ToString(), 0);

                astdoc.Elements.Add(elm);
            }
            //iACad.Document.ActiveLayOut.Entities.EraseAll();

            foreach (var item in ACad.Document.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Elements")
                    {
                        vf.Deleted = true;
                    }
                }

            }
            ACad.Document.Redraw(true);

            astdoc.Elements.CopyCoordinates(astdoc.Joints);
            astdoc.Elements.DrawElements(ACad.Document);
            Count_Geometry();

        }
        public void Count_Geometry()
        {
            txt_total_jnts.Text = AST_DOC.Joints.Count.ToString();
            txt_total_mbrs.Text = AST_DOC.Members.Count.ToString();
            txt_total_elmts.Text = AST_DOC.Elements.Count.ToString();
            txt_total_supps.Text = AST_DOC.Supports.Count.ToString();
        }

        public void Delete_Layer_Items(string LayerName)
        {
            foreach (var item in ACad.Document.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == LayerName)
                    {
                        vf.Deleted = true;
                    }
                }
            }
            ACad.Document.Redraw(true);
        }
        public double Text_Size
        {
            get
            {
                return (cmb_text_size.SelectedIndex + 1) * 0.01;
            }
        }

        private void cmb_size_Click(object sender, EventArgs e)
        {
            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = Text_Size;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = Text_Size;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    //vdCircle t = item as vdCircle;
                    //t.Radius = Text_Size * 0.3;
                    //t.Update();
                }

            }
            VDoc.Redraw(true);
        }
        private void chk_joints_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;

                //VDoc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;


                vdDocument VD = VDoc;

                if (tc_parrent.SelectedTab == tab_post_procees)
                    VD = ActiveDoc;

                if (tc_structure.SelectedTab != tab_Analysis)
                    VD = Active_Design_Doc;



                //if (tc4.SelectedIndex == 1)
                //    VD = defDoc;


                for (int i = 0; i < VD.Layers.Count; i++)
                {
                    if (VD.Layers[i].Name.ToLower().StartsWith(chk.Text.Substring(0, 4).ToLower()))
                    {
                        VD.Layers[i].On = chk.Checked;
                        if (chk.Text.StartsWith("E"))
                        {
                            if (chk.Checked)
                                VDRAW.vdCommandAction.View3D_ShadeOn(VD);
                            else
                                VDRAW.vdCommandAction.View3D_Wire2d(VD);
                        }

                        //VD.Layers[i].Frozen = !chk.Checked;
                    }
                    //if (VD.Layers[i].Name.StartsWith("Ele"))
                    //{
                    //    VD.Layers[i].Frozen = !chk_mems.Checked;
                    //}
                }
                VD.Redraw(true);
            }
            catch (Exception ex)
            {
            }

        }
        private void cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {

            vdDocument VD = ActiveDoc;

            double txt_size = PP_Text_Size;

            if (tc_structure.SelectedTab == tab_slab)
            {
                txt_size = (cmb1_text_size.SelectedIndex + 1) * 0.01;
            }
            else if (tc_structure.SelectedTab == tab_beam)
            {
                txt_size = (cmb2_text_size.SelectedIndex + 1) * 0.01;
            }
            else if (tc_structure.SelectedTab == tab_columns)
            {
                txt_size = (cmb3_text_size.SelectedIndex + 1) * 0.01;
            }
            else if (tc_structure.SelectedTab == tab_foundation)
            {
                txt_size = (cmb4_text_size.SelectedIndex + 1) * 0.01;
            }


            foreach (var item in VD.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = txt_size;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = txt_size;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    vdCircle t = item as vdCircle;
                    t.Radius = txt_size * 0.3;
                    t.Update();
                }
            }
            VD.Redraw(true);
        }
        private void btn_elmt_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_elmt_add.Name)
            {
                frmElements fs = new frmElements(ACad);
                fs.Owner = this;
                fs.DGV = dgv_elements;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_elmt_delete.Name)
            {
                //tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);

                if (dgv_elements.SelectedCells.Count > 0)
                    dgv_elements.Rows.RemoveAt(dgv_elements.SelectedCells[0].RowIndex);
                else if (dgv_elements.CurrentCell.RowIndex > -1)
                    dgv_elements.Rows.RemoveAt(dgv_elements.CurrentCell.RowIndex);

                Draw_Joints();

            }
            else if (btn.Name == btn_elmt_edit.Name)
            {
                frmElements fs = new frmElements(ACad);
                fs.Owner = this;
                fs.DGV = dgv_elements;
                if (dgv_elements.SelectedCells.Count > 0)
                    fs.ASTRA_Data.Add(dgv_elements.SelectedCells[0].RowIndex.ToString());
                else
                    fs.ASTRA_Data.Add(dgv_elements.CurrentCell.RowIndex.ToString());
                fs.ShowDialog();
            }
            else if (btn.Name == btn_elmt_insert.Name)
            {
                frmElements fs = new frmElements(ACad);
                fs.Owner = this;
                fs.DGV = dgv_elements;
                fs.ASTRA_Data.Add("INSERT");
                fs.ShowDialog();
            }
            Draw_Elements();
        }

        public void ShowMember(string memText, double cirRadius)
        {

            List<int> mbrs = new List<int>();

            MyStrings MS = new MyStrings(memText, ';');

            foreach (var item in MS.StringList)
            {
                mbrs.AddRange(MyStrings.Get_Array_Intiger(item).ToArray());
            }


            if (mbrs.Count == 0)
            {
                MyStrings ml = new MyStrings(memText, ' ');
                string mm = "";
                for (int i = 0; i < tv_mem_grps.Nodes.Count; i++)
                {
                    mm = tv_mem_grps.Nodes[i].Text;

                    if (mm.StartsWith(ml.StringList[0]))
                    {
                        mm = mm.Replace(ml.StringList[0], "");
                        mbrs = MyStrings.Get_Array_Intiger(mm);
                    }
                }
            }





            AST_DOC.Members.SetLayers(VDoc);




            foreach (vdFigure item in VDoc.ActiveLayOut.Entities)
            {
                item.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                if (item is vdLine)
                {
                    if (item.ToolTip.StartsWith("Member"))
                    {
                        MyList ml = new MyList(item.ToolTip, ' ');
                        if (mbrs.Contains(ml.GetInt(3)))
                            item.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_140;

                    }
                }
                item.Update();
            }
            VDoc.Redraw(true);
            return;


            Delete_Layer_Items("Selection");

            MemberIncidence minc = new MemberIncidence();

            foreach (var item in mbrs)
            {
                try
                {
                    minc = AST_DOC.Members.Get_Member(item);
                    if (minc != null)
                    {
                        vdCircle cirMember = new vdCircle();
                        cirMember.SetUnRegisterDocument(VDoc);
                        cirMember.setDocumentDefaults();

                        cirMember.Layer = VDoc.Layers.FindName("Selection");

                        cirMember.PenColor = new vdColor(Color.LightCoral);
                        cirMember.PenColor = new vdColor(Color.IndianRed);
                        cirMember.Center = minc.StartNode.Point;
                        cirMember.Radius = cirRadius;
                        cirMember.Thickness = gPoint.Distance3D(minc.StartNode.Point,
                            minc.EndNode.Point);

                        cirMember.ExtrusionVector = Vector.CreateExtrusion(minc.StartNode.Point,
                            minc.EndNode.Point);
                        VDoc.ActiveLayOut.Entities.AddItem(cirMember);
                    }

                }
                catch (Exception exx) { return; }


                //cirMember.Update();
                VDoc.Redraw(true);
            }
        }

        private void tsmi_openAnalysisTXTDataFile_Click(object sender, EventArgs e)
        {
            //if (Working_Folder == "" || Working_Folder == null)
            //{
            //    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            //    {
            //        if (fbd.ShowDialog() != DialogResult.Cancel)
            //        {
            //            Working_Folder = fbd.SelectedPath;
            //        }
            //    }
            //}

            Load_Example_Project_Data();
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

        private void tv_mem_props_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (AST_DOC.MemberGroups.Count > 0)
            //{
            //    //MyList mli = new MyList(tv_mem_props.SelectedNode.Text, ' ');

            //    //AST_DOC.Members.Groups.Table[
            //}

            ShowMember(e.Node.Text, Text_Size * 0.3);
        }

        private void tsmi_saveDXFDrawingFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                //sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf";
                sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (iApp.Is_StructureDemo)
                    {
                        MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (VDoc.SaveAs(sfd.FileName))
                        {
                            MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        //bool FirstOpen = false;
        private void tv_mem_props_Click(object sender, EventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (tv != null)
            {
                if (tv.SelectedNode != null)
                    ShowMember(tv.SelectedNode.Text, Text_Size * 0.3);
            }
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
            //MessageBox.Show(HeadsUtils.Constants.BuildType.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (HASP_Lock.IsDemoVersion())
            //if (HASP_Lock.IsDemoVersion())
            if (!iApp.Is_StructureDemo)
            {
                if (!Check_Coordinate(dgv_joints.RowCount, (dgv_members.RowCount)))
                {
                    //Chiranjit [2012 11 13]
                    string str = "This Facility is restricted in the Demo Version of ASTRA Pro.\n\nASTRA Pro USB Dongle not found at any port.\n\n";

                    str += "This Version can Process the Analysis Input Data from the Example Only. User has to select the input data file by using the menu option\n";
                    str += "\n \"File>>Open Analysis Example Data File\", and next can Process the Analysis file by using the menu option ";
                    str += "\"Process Analysis>>Analysis by Example Data File\" for Text Data, SAP Data and Drawings Files.\n\n";


                    str += "For Professional Version of ASTRA Pro please contact : \n\n";
                    str += "Email at : techsoft@consultant.com, dataflow@mail.com\n\n";
                    str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                    str += "Tel. No  : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                    str += "\nTechSOFT Engineering Services\n\n";
                    //MessageBox.Show(this, str, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show(str, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Chiranjit [2014 12 10]
                    //frm_Demo_Version.DemoDialog();
                    //frm_Demo_StructureVersion.DemoDialog();

                    //if (MessageBox.Show(this, "Do you want to Load Example Data?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
                    //MessageBox.Show(this, "Do you want to Load Example Data?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return true;
                }
                else
                {

                    //txt_seismic_coeeficient.Text = "0.12";

                    if (Seismic_Coeeficient != 0.12 && Seismic_Coeeficient != 0.0)
                    {
                        //string str = "This Facility is restricted in the Demo Version of ASTRA Pro.\n\nASTRA Pro USB Dongle not found at any port.\n\n";

                        //str += "This Version can Process the Analysis Input Data from the Example Only. User has to select the input data file by using the menu option\n";
                        //str += "\n \"File>>Open Analysis Example Data File\", and next can Process the Analysis file by using the menu option ";
                        //str += "\"Process Analysis>>Analysis by Example Data File\" for Text Data, SAP Data and Drawings Files.\n\n";


                        //str += "For Professional Version of ASTRA Pro please contact : \n\n";
                        //str += "Email at : techsoft@consultant.com, dataflow@mail.com\n\n";
                        //str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                        //str += "Tel. No  : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                        //str += "\nTechSOFT Engineering Services\n\n";

                        string str = "";

                        str = "This is a unauthorize version, Seismic Coefficient default value 0.12 can not be changed in this version.";



                        MessageBox.Show(this, str,"ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show(str, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txt_seismic_coeeficient.Text = "0.12";
                    }
                }
            }

            return false;
        }

        public double Seismic_Coeeficient 
        {
            get 
            {
                return MyList.StringToDouble(txt_seismic_coeeficient.Text, 0.12);
            }
        }
        //public event EventHandler StageAnalysisEvent;

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
            Seismic_Load = LoadData;
            Seismic_Combinations = Load_Comb;
            //File_Name = file_name;
            Save_Data(file_name);
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
            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);

            //if (tc4.TabPages.Count > 2)
            //{
            //    tc4.TabPages.Remove(tab_evlp_doc);
            //}

            //if (tc_parrent.SelectedIndex != 2) return;

            //if (tc_parrent.SelectedTab == tab_design_standard)
            //{
            //    tc_parrent.SelectedTab = tab_pre_process;
            //}

            if (tc_parrent.SelectedTab != tab_post_procees) return;


            if (!File.Exists(Analysis_File_Name))
            {
                MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (File.Exists(File_Name))
                    tc_parrent.SelectedTab = tab_procees;
                else
                    tc_parrent.SelectedTab = tab_pre_process;
                return;
            }


            tc1_pp.TabPages.Clear();


            if (tc_pp_main.SelectedTab == tab_forces)
            {
                PP_Show_Panel(tab1_forces);
                if (frcsDoc != null)
                {
                    //frcsDoc.ActiveLayOut.Entities.RemoveAll();

                    if (frcsDoc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Joints.DrawJointsText(frcsDoc, PP_Text_Size);
                            AST_DOC_ORG.Members.DrawMember(frcsDoc, PP_Text_Size);
                            AST_DOC_ORG.Elements.DrawElements(frcsDoc);
                            AST_DOC_ORG.Supports.DrawSupport(frcsDoc);
                        }
                        else
                        {
                            AST_DOC.Joints.DrawJointsText(frcsDoc, PP_Text_Size);
                            AST_DOC.Members.DrawMember(frcsDoc, PP_Text_Size);
                            AST_DOC.Elements.DrawElements(frcsDoc);
                            AST_DOC.Supports.DrawSupport(frcsDoc);
                        }
                        ActiveDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(frcsDoc);
                    }
                }
            }
            else if (tc_pp_main.SelectedTab == tab_load_deflection)
            {
                PP_Show_Panel(tab1_load_deflection);
            }
            else if (tc_pp_main.SelectedTab == tab_max_force)
            {
                //tc_docs.SelectedTab = tab_max_doc;
                PP_Show_Panel(tab1_max_force);

                if (maxDoc != null)
                {
                    //maxDoc.ActiveLayOut.Entities.RemoveAll();

                    if (maxDoc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Joints.DrawJointsText(maxDoc, PP_Text_Size);
                            AST_DOC_ORG.Members.DrawMember(maxDoc, PP_Text_Size);
                            AST_DOC_ORG.Elements.DrawElements(maxDoc);
                            AST_DOC_ORG.Supports.DrawSupport(maxDoc);
                        }
                        else
                        {
                            AST_DOC.Joints.DrawJointsText(maxDoc, PP_Text_Size);
                            AST_DOC.Members.DrawMember(maxDoc, PP_Text_Size);
                            AST_DOC.Elements.DrawElements(maxDoc);
                            AST_DOC.Supports.DrawSupport(maxDoc);

                        }

                        maxDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(maxDoc);
                    }
                }

            }
            else if (tc_pp_main.SelectedTab == tab_moving_load)
            {
                PP_Show_Panel(tab1_moving_load);
            }
            else if (tc_pp_main.SelectedTab == tab_envelop)
            {
                PP_Show_Panel(tab1_truss_env);
            }
            else if (tc_pp_main.SelectedTab == tab_diagram)
            {
                PP_Show_Panel(tab1_diag);
                //Load_Forces_Diagram();
                
            }

            //SetTextSize((cmb_text_size.SelectedIndex + 2) * 0.50);
            SetTextSize(PP_Text_Size);

            //return;
            try
            {
                //tc2.SelectedIndex = tc1.SelectedIndex;
                PP_Tab_Selection();
            }
            catch (Exception ex) { }
        }

        #endregion Process

        #region Post Process

        string file_name = "";
        ASTRADoc astDoc;
        StructureMemberAnalysis StructureAnalysis { get; set; }
        //public ASTRADoc AST_DOC
        //{

        //    get
        //    {
        //        if (tc4.SelectedIndex == 0 && AST_DOC_ORG != null)
        //            return AST_DOC_ORG;
        //        return astDoc;
        //    }
        //    set
        //    {
        //        astDoc = value;
        //    }
        //}

        public ASTRADoc AST_DOC_ORG { get; set; }

        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;

        //int lastId = -1;

        //public vdDocument VDoc
        //{
        //    get
        //    {
        //        return vdScrollableControl1.BaseControl.ActiveDocument;
        //    }
        //}

        public vdDocument frcsDoc
        {
            get
            {
                return vdSC_frcs.BaseControl.ActiveDocument;
            }
        }

        public vdDocument defDoc
        {
            get
            {
                return vdSC_ldef.BaseControl.ActiveDocument;
            }
        }
        public vdDocument envDoc
        {
            get
            {
                return vdSC_env.BaseControl.ActiveDocument;
            }
        }
        public vdDocument maxDoc
        {
            get
            {
                return vdSC_maxf.BaseControl.ActiveDocument;
            }
        }


        public vdDocument movDoc
        {
            get
            {
                return vdSC_mov.BaseControl.ActiveDocument;
            }
        }

        public vdDocument diagDoc
        {
            get
            {
                return vdSC_diag.BaseControl.ActiveDocument;
            }
        }

        public vdDocument slabDoc
        {
            get
            {
                return vdSC_slab.BaseControl.ActiveDocument;
            }
        }

        public vdDocument beamDoc
        {
            get
            {
                return vdSC_beam.BaseControl.ActiveDocument;
            }
        }
        public vdDocument columnDoc
        {
            get
            {
                return vdSC_columns.BaseControl.ActiveDocument;
            }
        }

        public vdDocument raftDoc
        {
            get
            {
                return vdRaft.BaseControl.ActiveDocument;
            }
        }


        public vdDocument ActiveDoc
        {
            get
            {

                if (tc_structure.SelectedTab != tab_Analysis)
                    return Active_Design_Doc;

                if (tc_parrent.SelectedTab == tab_pre_process)
                    return VDoc;
                if (tc_docs.SelectedTab == tab_frcs_doc)
                    return frcsDoc;
                else if (tc_docs.SelectedTab == tab_defl_doc)
                    return defDoc;
                else if (tc_docs.SelectedTab == tab_max_doc)
                    return maxDoc;
                else if (tc_docs.SelectedTab == tab_mov_doc)
                    return movDoc;
                else if (tc_docs.SelectedTab == tab_evlp_doc)
                    return envDoc;
                else if (tc_docs.SelectedTab == tab_diag_doc)
                    return diagDoc;
                else if (tc_docs.SelectedTab == tab_foundation)
                {
                    if (tc_footing.SelectedTab == tab_raft_foundation)
                        return raftDoc;
                }

                return VDoc;

                //if(
            }
        }


        public vdDocument Active_Design_Doc
        {
            get
            {

                if (tc_structure.SelectedTab == tab_slab)
                    return slabDoc;
                else if (tc_structure.SelectedTab == tab_beam)
                    return beamDoc;
                else if (tc_structure.SelectedTab == tab_columns)
                    return columnDoc;
                else if (tc_structure.SelectedTab == tab_drawings)
                    return vdDwg.ActiveDocument;
                else if (tc_structure.SelectedTab == tab_foundation)
                {
                    if (tc_footing.SelectedTab == tab_raft_foundation)
                        return raftDoc;
                }

                return VDoc;

                //if(
            }
        }

        public vdDocument doc
        {
            get
            {
                return defDoc;
            }
        }
        public vdDocument MainDoc
        {
            get
            {
                return VDoc;
            }
        }

        int iLoadCase;

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

            vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdScrollableControl1.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);







            vdSC_frcs.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_frcs.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_ldef.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_ldef.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_mov.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_mov.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_maxf.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_maxf.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);


            vdRaft.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdRaft.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(Raft_BaseControl_vdMouseUp);
            vdRaft.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);

        }

        void BaseControl_vdKeyUp(KeyEventArgs e, ref bool cancel)
        {
            if (e.KeyCode == Keys.Escape)
            {

                foreach (vdFigure item in VDoc.ActiveLayOut.Entities)
                {
                    item.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                    //if (item is vdLine)
                    //{
                    //    if (item.ToolTip.StartsWith("Member"))
                    //    {
                    //        MyList ml = new MyList(item.ToolTip, ' ');
                    //        if (mbrs.Contains(ml.GetInt(3)))
                    //            item.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_140;

                    //    }
                    //}
                    item.Update();
                }
            }
            VDoc.Redraw(true);
        }

        private void frm_ASTRA_Analysis_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < tc1_pp.TabPages.Count; i++)
            {
                tc1_pp.TabPages[i].Text = "";
            }

            if (AST_DOC != null)
            {

                File_Name = AST_DOC.FileName;

                if (File.Exists(File_Name))
                    rtb_input_file.Lines = File.ReadAllLines(File_Name);
                tc1_pp.Visible = true;
                StructureAnalysis = null;
                ld = null;
                if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                    rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                if (File.Exists(Analysis_File_Name))
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);

                Load_ASTRA_Data();

                cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                txtUserTitle.Text = ml.GetString(2).Trim();
                cmb_structure_type.SelectedItem = ml.StringList[1];
            }
        }

        private void Load_ASTRA_Data()
        {

            if (frcsDoc != null)
            {
                frcsDoc.ActiveLayOut.Entities.RemoveAll();
                frcsDoc.Redraw(true);
            }
            if (defDoc != null)
            {
                defDoc.ActiveLayOut.Entities.RemoveAll();
                defDoc.Redraw(true);
            }
            if (envDoc != null)
            {
                envDoc.ActiveLayOut.Entities.RemoveAll();
                envDoc.Redraw(true);
            }
            if (maxDoc != null)
            {
                maxDoc.ActiveLayOut.Entities.RemoveAll();
                maxDoc.Redraw(true);
            }
            if (movDoc != null)
            {
                movDoc.ActiveLayOut.Entities.RemoveAll();
                movDoc.Redraw(true);
            }
            if (diagDoc != null)
            {
                diagDoc.ActiveLayOut.Entities.RemoveAll();
                diagDoc.Redraw(true);
            }
            if (AST_DOC.Joints.Count == 0 ||
                AST_DOC.Members.Count == 0)
            {
                //MessageBox.Show("This file is not in correct format. Please select another file.", "ASTRA", MessageBoxButtons.OK);
                //this.Close();
                return;
            }



            VDoc.Palette.Background = Color.White;
            //DefDoc.Palette.Background = Color.White;   +
            astDoc = AST_DOC;
            AST_DOC.MemberProperties.CopyMemberIncidence(astDoc.Members);
            AST_DOC.Members.DrawMember(VDoc, Text_Size);
            //AST_DOC.Members.DrawMember(VDoc);
            AST_DOC.Elements.DrawElements(VDoc);
            AST_DOC.Supports.DrawSupport(VDoc);
            VDoc.Redraw(true);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
            SetGridWithNode();
            SetGridWithMember();
            SetGridWithElement();
            Set_Loadings();
            chk_joints.Checked = true;
            chk_mems.Checked = true;
            chk_elems.Checked = true;
            chk_suprts.Checked = true;

            if (AST_DOC.Elements.Count > 0)
                chk_elems.Checked = true;

            //for (int i = 1; i < 100; i++) cmb_text_size.Items.Add(i);

            //cmb_text_size.SelectedIndex = 1;
            timer1.Start();
        }

        private void PP_toolStripButtons_Click(object sender, EventArgs e)
        {

            vdDocument VD = ActiveDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //else if (tc4.SelectedIndex == 2) VD = envDoc;


            VD = ActiveDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name == tsb_pp_3D_rotate.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name == tsb_pp_VTop.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name == tsb_pp_VBot.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBottom(VD);
            else if (tsb.Name == tsb_pp_VLeft.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VLeft(VD);
            else if (tsb.Name == tsb_pp_VRight.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VRight(VD);
            else if (tsb.Name == tsb_pp_VFront.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(VD);
            else if (tsb.Name == tsb_pp_VBack.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VD);
            else if (tsb.Name == tsb_pp_VNE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINE(VD);
            else if (tsb.Name == tsb_pp_VNW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINW(VD);
            else if (tsb.Name == tsb_pp_VSE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISE(VD);
            else if (tsb.Name == tsb_pp_VSW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISW(VD);
            else if (tsb.Name == tsb_pp_ZoomA.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomP.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name == tsb_pp_ZoomW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name == tsb_pp_ZoomIn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomOut.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name == tsb_pp_Pan.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name == tsb_pp_Save.Name)
            {
                SaveDrawing(VD);
            }
            else if (tsb.Name == tsb_pp_ShadeOn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name == tsb_pp_Wire.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);

        }

        private void SaveDrawing(vdDocument VD)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (iApp.Is_StructureDemo)
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




        #region Loading
        private void Set_Loadings()
        {

            TreeNode tn = null;
            TreeNode tjn = null;

            //TreeView tv_loads = tv_loads;
            tv_loads.Nodes.Clear();
            foreach (var item in astDoc.LoadDefines)
            {
                tv_loads.Nodes.Add("LOAD " + item.LoadCase + " : " + item.LoadTitle);

                tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];


                if (item.Selfweight != "")
                {
                    tn.Nodes.Add(item.Selfweight);
                    //tn = tn.Nodes[tn.Nodes.Count - 1];
                    //foreach (var item1 in item.Combinations)
                    //{
                    //    tn.Nodes.Add(item1.ToString());
                    //}
                    //tn = tn.Parent;
                }
                if (item.SupportDisplacements.Count > 0)
                {
                    tn.Nodes.Add("SUPPORT DISPACEMENT LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.SupportDisplacements)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                    tn = tn.Parent;
                }
                if (item.RepeatLoadList.Count > 0)
                {
                    item.Set_Combination();
                    tn.Nodes.Add("REPEAT LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.Combinations)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                    tn = tn.Parent;
                }
                if (item.MemberLoadList.Count > 0)
                {
                    tn.Nodes.Add("MEMBER LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.MemberLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit + "/" + astDoc.Base_LengthUnit);
                    }
                    tn = tn.Parent;

                }
                if (item.JointLoadList.Count > 0)
                {

                    tn.Nodes.Add("JOINT LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.JointLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit);
                    }
                    tn = tn.Parent;
                }
                if (item.AreaLoadList.Count > 0)
                {

                    tn.Nodes.Add("AREA LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.AreaLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit + "/Sq." + astDoc.Base_LengthUnit);
                    }
                    tn = tn.Parent;
                }
                if (item.TempLoadList.Count > 0)
                {

                    tn.Nodes.Add("TEMP LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.TempLoadList)
                    {
                        tn.Nodes.Add(item1);
                    }
                    tn = tn.Parent;
                }
                if (item.CominationLoadList.Count > 0)
                {
                    item.Set_Combination();
                    tn.Nodes.Add("COMBINATION");
                    if (tn.Nodes.Count > 0)
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.Combinations)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                    tn = tn.Parent;
                }
                if (item.ElementLoadList.Count > 0)
                {
                    tn.Nodes.Add("ELEMENT LOAD");
                    tn = tn.Nodes[0];
                    foreach (var item1 in item.ElementLoadList)
                    {
                        //tn.Nodes.Add(item1 + " " + astDoc.MassUnit + "/" + astDoc.LengthUnit);
                        tn.Nodes.Add(item1);
                    }
                    tn = tn.Parent;
                }
            }
            //End
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string kStr = "";

            TreeNode tn = tv_loads.SelectedNode;

            bool IsJointLoad = false;

            if (tn.Nodes.Count == 0)
            {
                IsJointLoad = (tn.Parent.Text.StartsWith("JOINT"));
                kStr = tn.Text;
            }

            AST_DOC.JointLoads.Delete_ASTRAArrowLine(VDoc);
            AST_DOC.MemberLoads.Delete_ASTRAMemberLoad(VDoc);

            if (tn.Parent != null)
            {
                tn = tn.Parent;
                if (tv_loads.SelectedNode.Text.StartsWith("JOINT"))
                {
                    astDoc.JointLoads.DrawJointLoads(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                else if (tv_loads.SelectedNode.Text.StartsWith("MEMBER"))
                {
                    astDoc.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("COMB"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                    //astDoc.MemberLoads.DrawC(Maindoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("REPEAT"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                    //astDoc.MemberLoads.DrawC(Maindoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
            }
            else
            {
                AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1);
                AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1);
                goto _100;
            }

            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;

            //int MemNo = MemberNo;
            //int mIndx = astDoc.Members.IndexOf(MemNo);

            //astDoc.Supports.DrawSupport(Maindoc);




            if (IsJointLoad)
            {
                //AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1, kStr);
                JointLoadCollection jlc = new JointLoadCollection();
                jlc.AddTXT(kStr, tn.Index + 1);


                for (int i = 0; i < jlc.Count; i++)
                {

                    try
                    {
                        jlc[i].Joint = AST_DOC.Joints[jlc[i].Joint.NodeNo - 1];
                    }
                    catch (Exception exx)
                    {
                        jlc.RemoveAt(i); i--;
                    }
                }

                jlc.DrawJointLoads(VDoc, tn.Index + 1);
            }
            else
            {
                MemberLoadCollection mlc = new MemberLoadCollection();
                mlc.AddTxt(kStr, tn.Index + 1);

                for (int i = 0; i < mlc.Count; i++)
                {
                    try
                    {
                        mlc[i].Member = AST_DOC.Members[mlc[i].Member.MemberNo - 1];
                    }
                    catch (Exception exx)
                    {
                        mlc.RemoveAt(i); i--;
                    }
                }
                //AST_DOC.LoadDefines
                mlc.DrawMemberLoad(VDoc, tn.Index + 1);




                //AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1, kStr);
            }


        _100:
            if (tn.Index < cmbLoadCase.Items.Count)
                cmbLoadCase.SelectedIndex = tn.Index;
            VDoc.Redraw(true);
        }

        #endregion


        private void btn_open_ana_rep_Click(object sender, EventArgs e)
        {
            string open_file = "";

            Button btn = sender as Button;

            //if (btn.Name == btn_open_ana_rep.Name)
            //{
            //    open_file = Path.Combine(Path.GetDirectoryName(AST_DOC.FileName), "ANALYSIS_REP.TXT");
            //}

            if (File.Exists(open_file))
                System.Diagnostics.Process.Start(open_file);
        }

        private void PP_chk_joints_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;

                //VDoc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;

                vdDocument VD = VDoc;

                if (tc_docs.SelectedIndex == 1)
                    VD = defDoc;


                for (int i = 0; i < VD.Layers.Count; i++)
                {
                    if (VD.Layers[i].Name.ToLower().StartsWith(chk.Text.Substring(0, 4).ToLower()))
                    {
                        VD.Layers[i].Frozen = !chk.Checked;
                    }
                    //if (VD.Layers[i].Name.StartsWith("Ele"))
                    //{
                    //    VD.Layers[i].Frozen = !chk_mems.Checked;
                    //}
                }
                VD.Redraw(true);
            }
            catch (Exception ex)
            {
            }

        }
        public double PP_Text_Size
        {
            get
            {
                return (cmb_pp_text_size.SelectedIndex + 1) * 0.01;
            }
        }

        private void PP_cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //double d = double.Parse(cmb_text_size.Text);
                SetTextSize(Text_Size);
            }
            catch (Exception ex)
            { }
        }

        void PP_Show_Panel(TabPage tp)
        {
            tc_docs.TabPages.Clear();
            if (tp == tab1_forces)
            {
                tc_docs.TabPages.Add(tab_frcs_doc);
            }
            else if (tp == tab1_load_deflection)
            {
                tc_docs.TabPages.Add(tab_defl_doc);
            }
            else if (tp == tab1_max_force)
            {
                tc_docs.TabPages.Add(tab_max_doc);
            }
            else if (tp == tab1_moving_load)
            {
                tc_docs.TabPages.Add(tab_mov_doc);
            }
            else if (tp == tab1_truss_env)
            {
                tc_docs.TabPages.Add(tab_evlp_doc);
            }
            else if (tp == tab1_diag)
            {
                tc_docs.TabPages.Add(tab_diag_doc);
            }

            tc1_pp.TabPages.Clear();
            tc1_pp.TabPages.Add(tp);
        }


        private void PP_tc1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);

            if (tc_docs.TabPages.Count > 2)
            {
                tc_docs.TabPages.Remove(tab_evlp_doc);
            }


            tc_prp_panel.TabPages.Clear();
            if (tc_prp_main.SelectedTab == tab_forces)
                Show_Panel(tab1_forces);
            else if (tc_prp_main.SelectedTab == tab_load_deflection)
            {
                Show_Panel(tab1_load_deflection);
                tc_docs.SelectedTab = tab_defl_doc;
            }
            else if (tc_prp_main.SelectedTab == tab_max_force)
            {

                tc_docs.SelectedTab = tab_max_doc;
                Show_Panel(tab1_max_force);

                if (maxDoc != null)
                {
                    maxDoc.ActiveLayOut.Entities.RemoveAll();

                    if (maxDoc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Members.DrawMember(maxDoc);
                            AST_DOC_ORG.Elements.DrawElements(maxDoc);
                            AST_DOC_ORG.Supports.DrawSupport(maxDoc);
                        }
                        else
                        {
                            AST_DOC.Members.DrawMember(maxDoc);
                            AST_DOC.Elements.DrawElements(maxDoc);
                            AST_DOC.Supports.DrawSupport(maxDoc);

                        }
                    }

                    maxDoc.Redraw(true);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(maxDoc);
                }

            }
            else if (tc_prp_main.SelectedTab == tab_moving_load)
            {
                Show_Panel(tab1_moving_load);
                tc_docs.SelectedTab = tab_mov_doc;
            }
            else if (tc_prp_main.SelectedTab == tab_envelop)
            {
                Show_Panel(tab1_truss_env);
                tc_docs.TabPages.Add(tab_evlp_doc);
                tc_docs.SelectedTab = tab_evlp_doc;
            }

            //SetTextSize((cmb_text_size.SelectedIndex + 2) * 0.50);
            SetTextSize(Text_Size);

            //return;
            try
            {
                //tc2.SelectedIndex = tc1.SelectedIndex;
                Tab_Selection();
            }
            catch (Exception ex) { }
        }

        private void PP_Tab_Selection()
        {

            if (tc_pp_main.SelectedTab == tab_load_deflection)
            {
                if (ld == null || dgv_node_disp.RowCount == 0)
                {
                    AstrA_LOAD_Deflection();
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(defDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(defDoc);
                }
                PP_Show_Panel(tab1_load_deflection);

            }
            else if (tc_pp_main.SelectedTab == tab_moving_load)
            {
                if (AST_DOC.IsMovingLoad)
                {
                    if (StructureAnalysis == null)
                    {
                        StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    //tc_docs.SelectedTab = tab_mov_doc;
                    Set_Moving_Load_Init();
                    PP_Show_Panel(tab1_moving_load);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(movDoc);
                }
                else
                {
                    tc_pp_main.SelectedTab = tab_forces;
                    MessageBox.Show("Moving Load Data not found in the input file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (tc_pp_main.SelectedTab == tab_forces ||
                tc_pp_main.SelectedTab == tab_max_force)
            {

                if (StructureAnalysis == null || dgv_beam_frcs.RowCount == 0 || dgv_max_frc.RowCount == 0)
                {
                    string ana_seis = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);
                    if(File.Exists(ana_seis))
                        StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(ana_seis);
                    else
                        StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);

                    dgv_joint_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_joints)
                    {
                        dgv_joint_frcs.Rows.Add(item.NodeNo, item.LoadNo, item.FX, item.FY, item.FZ, item.MX, item.MY, item.MZ);

                    }
                    dgv_beam_frcs.Rows.Clear();

                    cmb_diag_mem_no.Items.Clear();
                    cmb_diag_ld_no.Items.Clear();
                    foreach (var item in StructureAnalysis.list_beams)
                    {
                        if (!cmb_diag_ld_no.Items.Contains(item.LoadNo))
                            cmb_diag_ld_no.Items.Add(item.LoadNo);
                        if (!cmb_diag_mem_no.Items.Contains(item.BeamNo))
                            cmb_diag_mem_no.Items.Add(item.BeamNo);

                        dgv_beam_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.BeamNo, MovingLoadAnalysis.eAstraMemberType.BEAM), item.LoadNo,
                            item.StartNodeForce.JointNo,
                            item.StartNodeForce.R1_Axial,
                            item.StartNodeForce.R2_Shear,
                            item.StartNodeForce.R3_Shear,
                            item.StartNodeForce.M1_Torsion,
                            item.StartNodeForce.M2_Bending,
                            item.StartNodeForce.M3_Bending);

                        dgv_beam_frcs.Rows.Add("", "",
                            item.EndNodeForce.JointNo,
                            item.EndNodeForce.R1_Axial,
                            item.EndNodeForce.R2_Shear,
                            item.EndNodeForce.R3_Shear,
                            item.EndNodeForce.M1_Torsion,
                            item.EndNodeForce.M2_Bending,
                            item.EndNodeForce.M3_Bending);
                    }
                    dgv_truss_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_trusses)
                    {
                        dgv_truss_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.TrussMemberNo, MovingLoadAnalysis.eAstraMemberType.TRUSS), item.LoadNo,
                            item.Stress,
                            item.Force);
                    }
                    foreach (var item in StructureAnalysis.list_cables)
                    {
                        dgv_truss_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.TrussMemberNo, MovingLoadAnalysis.eAstraMemberType.CABLE), item.LoadNo,
                            item.Stress,
                            item.Force);
                    }

                    dgv_max_frc.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_analysis)
                    {
                        dgv_max_frc.Rows.Add(item.UserMemberNo, item.AstraMemberType,
                                    item.CompressionForce,
                                    item.TensileForce,
                            item.MaxAxialForce,
                            item.MaxTorsion,
                            item.MaxBendingMoment,
                            item.MaxShearForce);
                    }

                    dgv_plate_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_plates)
                    {
                        dgv_plate_frcs.Rows.Add(item.PlateNo, item.LoadNo,
                            item.SXX.ToString("f4"),
                            item.SYY.ToString("f4"),
                            item.SXY.ToString("f4"),
                            item.MXX.ToString("f4"),
                            item.MXY.ToString("f4"),
                            item.MXY.ToString("f4"));
                    }

                }
                if (tc_pp_main.SelectedTab == tab_max_force)
                    PP_Show_Panel(tab1_max_force);
                else
                    PP_Show_Panel(tab1_forces);


            }
            else if (tc_pp_main.SelectedTab == tab_envelop)
            {
                if (StructureAnalysis == null)
                    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                //tc4.SelectedIndex = 2;
            }
            else if (tc_pp_main.SelectedTab == tab_diagram)
            {
                //if (StructureAnalysis == null)
                //    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);


                //if (cmb_diag_ld_no.Items.Count > 0)
                //{
                //    if (cmb_diag_ld_no.SelectedIndex == -1)
                //        cmb_diag_ld_no.SelectedIndex = 0;
                //}
                //if (cmb_diag_mem_no.Items.Count > 0)
                //{
                //    if (cmb_diag_mem_no.SelectedIndex == -1)
                //        cmb_diag_mem_no.SelectedIndex = 0;
                //}
                //Select_Diagram();

                Load_Forces_Diagram();
            }
            else
                tc_docs.SelectedIndex = 0;
            this.Refresh();
        }

        private void tc2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tc2.SelectedTab == tab1_moving_load)
            //{
            //    if (!AST_DOC.IsMovingLoad)
            //    {
            //        tc2.SelectedIndex = 0;
            //        tc1.SelectedIndex = 0;
            //        //MessageBox.Show("Moving Load Data not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        tc2.SelectedIndex = 0;
            //        tc1.SelectedIndex = 0;
            //    }
            //    else
            //        tc1.SelectedIndex = tc2.SelectedIndex;
            //}
            //else
            //if(tc2.SelectedTab != null)
            //tc2.SelectedTab.Text = "";
        }

        private void chkGlobal_CheckedChanged(object sender, EventArgs e)
        {
            ShowGlobalLocal();
        }
        private void ShowGlobalLocal()
        {
            vdDocument VD = VDoc;

            if (tc_docs.SelectedIndex == 1)
                VD = defDoc;

            try
            {
                //VD.Layers.FindName("MemberLoadGlobal").Frozen = !chkGlobal.Checked;
                //VD.Layers.FindName("MemberLoadLocal").Frozen = !chkLocal.Checked;
            }
            catch (Exception exx) { }
            {
            }

            try
            {

                //VD.Layers.FindName("ConcentrateLocal").Frozen = !chkLocal.Checked;
                //VD.Layers.FindName("ConcentrateGlobal").Frozen = !chkGlobal.Checked;
            }
            catch (Exception ex2) { }
            {
            }
            try
            {
                //VD.Layers.FindName("JointLoad").Frozen = !chkJoint.Checked;
            }
            catch (Exception exx1) { }
            {
            }
            VD.Redraw(true);
        }

        private void PP_timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
            //defDoc.Palette.Background = Color.White;
        }

        public void AstrA_LOAD_Deflection()
        {

            string analysisFileName = "";
            //if (astDoc == null)
            astDoc = AST_DOC;
            analysisFileName = Path.GetDirectoryName(astDoc.FileName);
            analysisFileName = astDoc.AnalysisFileName;

            if (!File.Exists(analysisFileName)) return;

            Set_Init();
            if (AST_DOC_ORG.IsDynamicLoad)
            {
                //this.Text = "EIGEN VALUE ANALYSIS";
                //lblLoadCase.Text = "EIGEN VALUE";
                lblLoadCase.Text = "Frequencies : Mode Shapes";
            }
            else
            {
                //this.Text = "LOAD DEFLECTION ANALYSIS";
                lblLoadCase.Text = "LOAD CASE";
            }
            cmbInterval.SelectedIndex = 9;
            //if (AST_DOC_ORG.IsMovingLoad)
            //    AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, 1);

            cmbLoadCase.SelectedIndex = -1;
            cmbLoadCase.SelectedIndex = 0;

            string temp_file = "";

            temp_file = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "rep.tmp");

            StreamWriter sw = new StreamWriter(new FileStream(temp_file, FileMode.Create));
            StreamReader sr = new StreamReader(new FileStream(analysisFileName, FileMode.Open));

            string str = "";
            string kstr = "";
            bool flag = false;

            bool truss_title = false;
            bool beam_title = false;
            bool cable_title = false;
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Taken from file ANALYSIS_REP.TXT");
                //sw.WriteLine("       Member   Load     Node    AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                //sw.WriteLine("       No:      Case     Nos:       R1          R2          R3          M1          M2          M3");
                sw.WriteLine();
                sw.WriteLine();
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().ToUpper();
                    kstr = MovingLoadAnalysis.MyList.RemoveAllSpaces(str);

                    //str = str.Replace("\t\t", "\t    ");
                    if (str.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!truss_title)
                        {
                            sw.WriteLine("TRUSS MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            truss_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("BEAM FORCES AND MOMENTS"))
                    {
                        flag = true;
                        if (!beam_title)
                        {
                            sw.WriteLine("BEAM FORCES AND MOMENTS");
                            sw.WriteLine();
                            sw.WriteLine(" BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                            sw.WriteLine("  NO.  NO.        R1          R2          R3          M1          M2          M3");
                            sw.WriteLine();
                            beam_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("CABLE MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!cable_title)
                        {
                            sw.WriteLine("CABLE MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            cable_title = true;
                        }
                        continue;
                    }
                    if (flag && !str.Contains("*") && str.Length > 9)
                    {
                        if ((kstr != "MEMBER LOAD STRESS FORCE") ||
                            (kstr != ".....BEAM FORCES AND MOMENTS"))
                            sw.WriteLine(str);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sr.Close();
            }

            return;
        }

        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_mode_shape.Text = "";
            if (cmbLoadCase.SelectedIndex == 0)
            {
                lbl_mode_shape.Text = "NO LOAD CASE";

            }
            else
            {
                lbl_mode_shape.Text = AST_DOC.LoadDefines[cmbLoadCase.SelectedIndex - 1].LoadTitle;

             
                //ToolTip tp = new ToolTip();
                //tp.SetToolTip(lbl_mode_shape, lbl_mode_shape.Text);

                if (AST_DOC.LoadDefines[cmbLoadCase.SelectedIndex - 1].CominationLoadList.Count > 0)
                {

                    lbl_mode_shape.Text = AST_DOC.LoadDefines[cmbLoadCase.SelectedIndex - 1].CominationLoadList[0];

                    MyList ml = new MyList(lbl_mode_shape.Text, ' ');

                    lbl_mode_shape.Text = "";
                    for (int i = 0; i < ml.Count; i++)
                    {

                        lbl_mode_shape.Text = lbl_mode_shape.Text + "Load " + ml.StringList[i] + " X " + ml.StringList[i + 1];

                        i++;

                        if (i < ml.Count-1)
                            lbl_mode_shape.Text = lbl_mode_shape.Text + ", ";

                    }

                    //lbl_mode_shape.Text = AST_DOC.LoadDefines[cmbLoadCase.SelectedIndex - 1].CominationLoadList[0] + " " + lbl_mode_shape.Text;
                    //lbl_mode_shape.Text = "Combination : " + AST_DOC.LoadDefines[cmbLoadCase.SelectedIndex - 1].CominationLoadList[0];
                }

            }
            
            lbl_mode_shape.Visible = true;
            

            try
            {
                iLoadCase = int.Parse(cmbLoadCase.Text);

                ShowData();

                if (AST_DOC.IsDynamicLoad)
                {
                    iLoadCase = 1;
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

                    VectorDraw.Professional.Memory.vdMemory.Collect();
                    GC.Collect();
                    VDoc.UndoHistory.PushEnable(false);
                    VDoc.CommandAction.Undo("BEGIN");

                    //mainDoc.UndoHistory.PushEnable(false);
                    //mainDoc.CommandAction.Undo("BEGIN");

                    doc.Redraw(true);
                    MainDoc.Redraw(true);

                    return;
                }
                //SetTextSize(Text_Size);
            }
            catch (Exception ex)
            {
                //ex = ex;
            }

            //if (AST_DOC_ORG.IsMovingLoad)
            //{
            //    try
            //    {
            //        GC.Collect();
            //        VectorDraw.Professional.Memory.vdMemory.Collect();


            //        MainDoc.UndoHistory.PushEnable(false);
            //        //doc.CommandAction.Undo("BEGIN");

            //        doc.UndoHistory.PushEnable(false);
            //        //mainDoc.CommandAction.Undo("BEGIN");

            //        AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, iLoadCase);
            //        AST_DOC.Load_Geneartion.Draw_Moving_Wheel(doc, 0.2, LoadCase, max_x, AST_DOC_ORG.Joints.BoundingBox.Bottom);
            //        AST_DOC.Load_Geneartion.Draw_Plan_Moving_Wheel(MainDoc, 0.2, LoadCase, max_x);
            //        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
            //    }
            //    catch (Exception ex) { }
            //}
            //else
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);

            doc.Redraw(true);
            MainDoc.Redraw(true);
        }
        public double Factor
        {
            get
            {
                return MyStrings.StringToDouble(txtDefFactor.Text, 1);
            }
        }
        private void ShowData()
        {

            VectorDraw.Professional.Memory.vdMemory.Collect();

            string sLoad = "LOAD_" + cmbLoadCase.Text;
            AST_DOC = (ASTRADoc)ld.HashDeflection[sLoad];
            //if (AST_DOC_ORG.IsMovingLoad == false)
            AST_DOC = ld.Get_ASTRADoc(LoadCase, Factor);

            if (AST_DOC != null)
            {

                defDoc.ActiveLayOut.Entities.EraseAll();
                AST_DOC.Joints.DrawJointsText(defDoc, Text_Size);
                AST_DOC.Members.DrawMember(defDoc, Text_Size);
                AST_DOC.Elements.DrawElements(defDoc);
                AST_DOC.Supports.DrawSupport(defDoc);

                //if (AST_DOC_ORG.IsMovingLoad == false)
                //    AST_DOC.Members.DrawMember(defDoc);
                if (AST_DOC.IsMovingLoad)
                {
                    //AST_DOC.JointLoads.DrawJointLoads_MovingLoad(doc, LoadCase);
                }
                else
                {

                    AST_DOC_ORG.MemberLoads.Delete_ASTRAMemberLoad(MainDoc);
                    AST_DOC_ORG.MemberLoads.DrawMemberLoad(MainDoc, LoadCase);

                    AST_DOC_ORG.JointLoads.Delete_ASTRAArrowLine(MainDoc);
                    AST_DOC_ORG.JointLoads.DrawJointLoads(MainDoc, LoadCase);
                }
                AST_DOC_ORG.JointLoads.CopyCoordinates(AST_DOC_ORG.Joints);
            }
            VectorDraw.Professional.Memory.vdMemory.Collect();
        }
        List<int> list_joint_index = new List<int>();
        private void Draw_ASTRA_Joint_Load()
        {
            ASTRAArrowLine aline = null;
            for (int i = 0; i < AST_DOC.Joints.Count; i++)
            {
                aline = new ASTRAArrowLine();
                aline.SetUnRegisterDocument(doc);
                aline.setDocumentDefaults();

                list_joint_index.Add(doc.ActiveLayOut.Entities.Count);
                doc.ActiveLayOut.Entities.AddItem(aline);

                aline.EndPoint = AST_DOC.Joints[i].Point;

                aline.StartPoint.x = aline.EndPoint.x;
                aline.StartPoint.y = aline.EndPoint.y + 2.0d;
                aline.StartPoint.z = aline.EndPoint.z;


                if (i % 2 == 0)
                {
                    aline.visibility = vdFigure.VisibilityEnum.Invisible;
                }
                else
                {
                    aline.visibility = vdFigure.VisibilityEnum.Visible;
                }
            }
            doc.Redraw(true);
        }
        public void Moving_Line()
        {
        }

        public int LoadCase
        {
            get
            {
                try
                {
                    iLoadCase = int.Parse(cmbLoadCase.Text);
                }
                catch (Exception exx)
                {
                }
                return iLoadCase;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            SetLoadIndex(-1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SetLoadIndex(+1);
        }

        private void SetLoadIndex(int indx)
        {
            int selIndex = cmbLoadCase.SelectedIndex;
            selIndex += indx;

            if (selIndex == cmbLoadCase.Items.Count)
            {
                selIndex = 0;
            }
            else if (selIndex == -1)
            {
                selIndex = cmbLoadCase.Items.Count - 1;
            }
            try
            {
                cmbLoadCase.SelectedIndex = selIndex;
            }
            catch (Exception ex) { }
            if (cmbLoadCase.SelectedIndex == -1)
            {
                AST_DOC_ORG.Members.DrawMember(doc);
            }
            CleanMemory();
        }

        private void ASTRALoadDeflections_Load()
        {
            //pbLoadDeflection.Visible = true;

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            VectorDraw.Professional.Memory.vdMemory.Collect();
            GC.Collect();
            tmrLoadDeflection.Stop();
            StartTimer(false);
            //SLAB01 slb01 = new SLAB01();
        }

        private void btnNextAuto_Click(object sender, EventArgs e)
        {
            bIsNext = true;
            StartTimer(true);
        }

        private void btnAutoPrev_Click(object sender, EventArgs e)
        {
            bIsNext = false;
            StartTimer(true);
            CleanMemory();
        }

        public void Set_Init()
        {
            if (chk_seismic_coeeficient.Checked && File.Exists(File_Seismic_Load))
            {
                AST_DOC_ORG = new ASTRADoc(File_Seismic_Load);
            }
            else
                AST_DOC_ORG = new ASTRADoc(AST_DOC.FileName);
            
            if (ld == null)
                ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);



            NodeDisplacement nd = null;
            dgv_node_disp.Rows.Clear();

            double max_def = 0.0;
            int load_no = 0;
            int node_no = 0;

            for (int i = 0; i < ld.Node_Displacements.Count; i++)
            {
                nd = ld.Node_Displacements[i];
                dgv_node_disp.Rows.Add(nd.Node.NodeNo, nd.LoadCase, nd.Tx, nd.Ty, nd.Tz, nd.Rx, nd.Ry, nd.Rz);

                if (Math.Abs(nd.Ty) > Math.Abs(max_def))
                {
                    max_def = nd.Ty;
                    load_no = nd.LoadCase;
                    node_no = nd.Node.NodeNo;
                }
            }
            txt_max_deflection.Text = max_def.ToString();
            txt_max_deflection_load.Text = load_no.ToString();
            txt_max_deflection_node.Text = node_no.ToString();


            cmbLoadCase.Items.Clear();
            for (int j = 0; j <= ld.MaxLoadCase; j++)
            {
                cmbLoadCase.Items.Add(j);
            }

            SetLoadIndex(1);


            if (AST_DOC_ORG.IsDynamicLoad)
            {
                AST_DOC_ORG.DrawMemberDetails(MainDoc, 1);
            }
        }

        private void StartTimer(bool flag)
        {
            int interval = 1000;
            try
            {
                double dd = double.Parse(cmbInterval.Text.Replace("Sec", " ").Trim().TrimEnd().TrimStart());
                dd = dd * 1000.0d;
                interval = int.Parse(dd.ToString());
            }
            catch (Exception xex)
            {
            }
            tmrLoadDeflection.Interval = interval;

            tmrLoadDeflection.Enabled = flag;
            btnPause.Enabled = flag;
            btnStop.Enabled = flag;
            cmbInterval.Enabled = !flag;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CleanMemory();
            tmrLoadDeflection.Stop();
            StartTimer(false);
            cmbLoadCase.SelectedIndex = 0;
            //System.GC.Collect();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                cmbLoadCase.SelectedIndex = cmbLoadCase.Items.Count - 1;
                CleanMemory();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                cmbLoadCase.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }

        private void pbLoadDeflection_VisibleChanged(object sender, EventArgs e)
        {
            grb_manual.Visible = !pbLoadDeflection.Visible;
            grb_Auto.Visible = !pbLoadDeflection.Visible;
            lblPleaseWait.Visible = pbLoadDeflection.Visible;

            //lblFactor.Enabled = !AST_DOC_ORG.IsMovingLoad;

            txtDefFactor.Visible = !pbLoadDeflection.Visible;
            lblFactor.Visible = !pbLoadDeflection.Visible;
            try
            {
                if (ld.Factor < 1)
                    txtDefFactor.Text = ld.Factor.ToString("0.000");
                else
                    txtDefFactor.Text = ld.Factor.ToString("0");
                cmbLoadCase.SelectedIndex = -1;
                cmbLoadCase.SelectedIndex = 0;
                //txtDefFactor.Enabled = !AST_DOC_ORG.IsMovingLoad;
            }
            catch (Exception ex)
            {
            }
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

        #region  Chiranjit [2014 04 10] New Moving load Animation
        List<MLoadData> load_data = null;
        List<VehicleLoad> list_vehicle = null;
        public void MovingLoad_Initialize(vdDocument vDoc, string file_name)
        {
            //InitializeComponent();
            if (movDoc != null)
            {
                AST_DOC_ORG.Members.DrawMember(movDoc);
                AST_DOC_ORG.Supports.DrawSupport(movDoc);
            }
            //if (MainDoc != null)
            //    AST_DOC_ORG.Members.DrawMember(MainDoc);

            //doc = vDoc;

            //AST_DOC_ORG.Members.DrawMember(MainDoc);
            //AST_DOC_ORG.Joints.DrawJointsText(MainDoc, 0.1);


            string load_file = Path.Combine(Path.GetDirectoryName(file_name), "LL.fil");
            if (!File.Exists(load_file)) load_file = Path.Combine(Path.GetDirectoryName(file_name), "LL.txt");

            if (File.Exists(load_file))
                load_data = MLoadData.GetLiveLoads(load_file);
            else
            {
                MessageBox.Show(this, load_file + " not found.");
                return;
            }

            VehicleLoad vl1 = null;

            for (int i = 0; i < AST_DOC_ORG.Load_Geneartion.Count; i++)
            {
                for (int j = 0; j < load_data.Count; j++)
                {
                    if (AST_DOC_ORG.Load_Geneartion[i].TypeNo == load_data[j].TypeNo)
                    {
                        try
                        {
                            //vl1 = new VehicleLoad(doc, MainDoc, load_data[j].Distances.Text);
                            vl1 = new VehicleLoad(movDoc, movDoc, load_data[j]);
                            vl1.TotalLength = AST_DOC_ORG.AnalysisData.Joints.MaxX;
                            vl1.XINC = AST_DOC_ORG.Load_Geneartion[i].XINC;
                            vl1.X = AST_DOC_ORG.Load_Geneartion[i].X;
                            vl1.Y = AST_DOC_ORG.Load_Geneartion[i].Y;
                            vl1.Z = AST_DOC_ORG.Load_Geneartion[i].Z;
                            if (list_vehicle == null)
                                list_vehicle = new List<VehicleLoad>();
                            list_vehicle.Add(vl1);
                        }
                        catch (Exception ex) { }
                    }
                }
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
        }
        void RunMovingLoad()
        {
            pbLoadDeflection.Visible = false;


            foreach (var item in list_vehicle)
            {
                item.Update(bIsNext);
            }
        }
        public void Set_Moving_Load_Init()
        {
            AST_DOC_ORG = new ASTRADoc(AST_DOC.FileName);

            if (ld == null)
                ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);

            txtDefFactor.Text = ld.Factor.ToString("0");

            cmb_mov_loadcase.Items.Clear();
            for (int j = 0; j <= ld.MaxLoadCase; j++)
            {
                cmb_mov_loadcase.Items.Add(j);
            }
            //SetLoadIndex(1);


            //if (AST_DOC_ORG.IsDynamicLoad)
            //{
            //    AST_DOC_ORG.DrawMemberDetails(defDoc, 1);
            //}
            if (movDoc != null && AST_DOC_ORG.IsMovingLoad)
                MovingLoad_Initialize(movDoc, AST_DOC_ORG.FileName);

            List<int> lst_def_types = new List<int>();
            tv_ml_def.Nodes.Clear();
            for (int i = 0; i < AST_DOC_ORG.Load_Geneartion.Count; i++)
            {
                if (!lst_def_types.Contains(AST_DOC_ORG.Load_Geneartion[i].TypeNo))
                    lst_def_types.Add(AST_DOC_ORG.Load_Geneartion[i].TypeNo);
                tv_ml_def.Nodes.Add("TYPE " + AST_DOC_ORG.Load_Geneartion[i].TypeNo);
                tv_ml_def.Nodes[i].Nodes.Add("X : " + AST_DOC_ORG.Load_Geneartion[i].X.ToString("f3"));
                tv_ml_def.Nodes[i].Nodes.Add("Y : " + AST_DOC_ORG.Load_Geneartion[i].Y.ToString("f3"));
                tv_ml_def.Nodes[i].Nodes.Add("Z : " + AST_DOC_ORG.Load_Geneartion[i].Z.ToString("f3"));
                tv_ml_def.Nodes[i].Nodes.Add("XINC : " + AST_DOC_ORG.Load_Geneartion[i].XINC.ToString("f3"));
            }

            tv_ml_loads.Nodes.Clear();
            for (int i = 0; i < load_data.Count; i++)
            {
                if (lst_def_types.Contains(load_data[i].TypeNo))
                {
                    tv_ml_loads.Nodes.Add(load_data[i].TypeNoText + " : " + load_data[i].Code);
                    tv_ml_loads.Nodes[tv_ml_loads.Nodes.Count - 1].Nodes.Add("AXLE LOADS : " + load_data[i].LoadValues.ToString());
                    tv_ml_loads.Nodes[tv_ml_loads.Nodes.Count - 1].Nodes.Add("AXLE SPACINGS : " + load_data[i].Distances.ToString());
                    tv_ml_loads.Nodes[tv_ml_loads.Nodes.Count - 1].Nodes.Add("AXLE WIDTH : " + load_data[i].LoadWidth.ToString("f3"));
                }
            }

            if (AST_DOC_ORG.IsDynamicLoad)
            {
                //this.Text = "EIGEN VALUE ANALYSIS";
            }
            else
            {
                //this.Text = "MOVING LOAD DISPLAY";
            }
            cmb_mov_time.SelectedIndex = 0;

            cmb_mov_loadcase.SelectedIndex = 1;

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);


        }
        public void Moving_Stop()
        {
            tmr_moving_load.Stop();
            for (int i = 0; i < list_vehicle.Count; i++)
            {
                list_vehicle[i].Set_Wheel_Distance();
            }
            cmb_mov_loadcase.SelectedIndex = 1;
        }
        private void Start_Moving_Timer(bool flag)
        {

            int interval = 100;
            try
            {

                MyStrings m = new MyStrings(cmbInterval.Text, ' ');

                double dd = m.GetDouble(0);
                if (dd < 0.3)
                    dd = dd * 100.0d;
                else
                    dd = dd * 1000.0d;

                if (dd == 0.0)
                    dd = 100.0;
                //else
                //    dd = dd * 100.0d;

                interval = int.Parse(dd.ToString());

            }
            catch (Exception xex)
            {
                interval = 100;
            }
            tmr_moving_load.Interval = interval;

            tmr_moving_load.Enabled = flag;
            btn_mov_pause.Enabled = flag;
            btn_mov_stop.Enabled = flag;
            cmb_mov_time.Enabled = !flag;
        }
        private void Set_Moving_LoadIndex(int indx)
        {
            int selIndex = cmb_mov_loadcase.SelectedIndex;
            selIndex += indx;

            if (selIndex == cmb_mov_loadcase.Items.Count)
            {
                selIndex = 0;
            }
            else if (selIndex == -1)
            {
                selIndex = cmb_mov_loadcase.Items.Count - 1;
            }
            try
            {
                cmb_mov_loadcase.SelectedIndex = selIndex;
            }
            catch (Exception ex) { }
            if (cmb_mov_loadcase.SelectedIndex == -1)
            {
                if (AST_DOC_ORG.IsMovingLoad)
                    AST_DOC_ORG.Members.DrawMember(movDoc);
                else
                    AST_DOC_ORG.Members.DrawMember(movDoc);
            }
            CleanMemory();
        }

        private void cmb_mov_loadcase_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Chiranjit [2011 09 24]   New Moving Load Analysis
            try
            {
                iLoadCase = int.Parse(cmb_mov_loadcase.Text);


                if (AST_DOC_ORG.IsMovingLoad)
                {
                    txt_dist.Text = (AST_DOC_ORG.Load_Geneartion[0].XINC * iLoadCase).ToString();
                    RunMovingLoad();
                }
                else
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(defDoc);


            }
            catch (Exception ex) { }
            #endregion Chiranjit [2011 09 24]   Commented for New Moving Load Analysis
        }
        private void btn_mov_run_Click(object sender, EventArgs e)
        {

            bIsNext = true;
            if (cmb_mov_loadcase.SelectedIndex == cmb_mov_loadcase.Items.Count - 1)
            {
                Moving_Stop();
            }
            if (cmb_mov_loadcase.SelectedIndex == 0)
            {
                Moving_Stop();
            }
            Start_Moving_Timer(true);
        }
        private void btn_mov_pause_Click(object sender, EventArgs e)
        {

            VectorDraw.Professional.Memory.vdMemory.Collect();
            GC.Collect();
            tmr_moving_load.Stop();
            Start_Moving_Timer(false);
        }
        private void btn_mov_stop_Click(object sender, EventArgs e)
        {
            Moving_Stop();
        }
        #endregion  Chiranjit [2014 04 10] New Moving load Animation

        private void tc5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_parrent.SelectedTab == tab_pre_process)
            {
                sc3.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);
            }
            else
            {
                if (!File.Exists(File_Name))
                {
                    MessageBox.Show("Please select a file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tc_parrent.SelectedTab = tab_pre_process;
                    return;
                }
                //if (tc_parrent.SelectedTab == pa)
                //{
                //    sc3.Panel2Collapsed = true;
                //}
                else if (tc_parrent.SelectedTab == tab_post_procees)
                {
                    if (!File.Exists(Analysis_File_Name))
                    {
                        MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (File.Exists(File_Name))
                            tc_parrent.SelectedTab = tab_procees;
                        else
                            tc_parrent.SelectedTab = tab_pre_process;
                        return;
                    }
                    Tab_Selection();
                    //if (tc1.SelectedTab == tab_forces)
                    //    Show_Panel(tab1_forces);
                    //else
                    //    Show_Panel(tab1_max_force);
                    sc3.Panel2Collapsed = false;
                }
            }

        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {

        }

        private void tc6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tc2.TabPages.Clear();
            //if (tc6.SelectedTab == tab_geometry)
            //    tc2.TabPages.Add(tab1_geometry);
            //else if (tc6.SelectedTab == tab_loading)
            //{
            //    tc2.TabPages.Add(tab1_loading);
            //    tc4.SelectedTab = tab_org_doc;
            //}
            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);
        }

        private void txtDefFactor_EnabledChanged(object sender, EventArgs e)
        {
            txtDefFactor.Enabled = true;
        }

        private void btn_env_show_Click(object sender, EventArgs e)
        {
            //List<int> mem_list = MyList.Get_Array_Intiger(txt_env_mnos.Text);
            List<int> mem_list = new List<int>();

            List<AnalysisData> data = new List<AnalysisData>();
            List<double> tensile_forces = new List<double>();
            List<double> compressive_forces = new List<double>();




            List<int> top_chords = new List<int>();
            List<int> bottom_chords = new List<int>();

            foreach (var item in AST_DOC.AnalysisData.Members.Members)
            {
                if (item.StartNode.Z == 0.0 &&
                    item.EndNode.Z == 0.0 &&
                    item.EndNode.Y == AST_DOC.Joints.Max_Y_Positive &&
                    item.StartNode.Y == AST_DOC.Joints.Max_Y_Positive)
                    top_chords.Add(item.MemberNo);

                else if (item.StartNode.Z == 0.0 &&
                    item.EndNode.Z == 0.0 &&
                    item.EndNode.Y == AST_DOC.Joints.Max_Y_Negative &&
                    item.StartNode.Y == AST_DOC.Joints.Max_Y_Negative)
                    bottom_chords.Add(item.MemberNo);
            }

            AnalysisData ad = null;



            if (rbtn_env_top.Checked)
                mem_list = top_chords;
            if (rbtn_env_bottom.Checked)
                mem_list = bottom_chords;

            for (int i = 0; i < mem_list.Count; i++)
            {
                ad = (AnalysisData)StructureAnalysis.MemberAnalysis[mem_list[i]];
                if (ad != null)
                {
                    data.Add(ad);
                    if (ad.TensileForce != 0.0)
                        tensile_forces.Add(-ad.TensileForce);
                    else if (ad.CompressionForce != 0.0)
                        tensile_forces.Add(ad.CompressionForce);
                }
            }


            try
            {
                Envelope env = new Envelope(tensile_forces, AST_DOC);
                env.MembersNos = mem_list;

                envDoc.ActiveLayOut.Entities.RemoveAll();
                env.DrawEnvelop(envDoc);

                envDoc.Redraw(true);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(envDoc);
            }
            catch (Exception ex)
            { }


        }

        private void PP_tsmi_data_open_Click(object sender, EventArgs e)
        {
            string menu_name = "";


            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi != null) menu_name = tsmi.Name;
            else
            {
                ToolStripButton tsb = sender as ToolStripButton;

                if (tsb != null) menu_name = tsb.Name;
            }

            if (menu_name == tsmi_data_open.Name || menu_name == tsb_open_data.Name)
            {

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text Files (*.txt)|*.txt|ASTRA Files (*.ast)|*.ast";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        File_Name = ofd.FileName;
                        rtb_input_file.Lines = File.ReadAllLines(File_Name);
                        tc_prp_panel.Visible = true;
                        sc3.Panel2Collapsed = true;
                        astDoc = new ASTRADoc(File_Name);
                        StructureAnalysis = null;
                        ld = null;
                        if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                            rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                        if (File.Exists(Analysis_File_Name))
                            rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                        Load_ASTRA_Data();

                        cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                        cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                        MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                        txtUserTitle.Text = ml.GetString(2).Trim();
                        cmb_structure_type.SelectedItem = ml.StringList[1];
                    }
                }
            }
            else if (menu_name == tsmi_file_save.Name || menu_name == tsb_save_data.Name)
            {
                if (File.Exists(File_Name))
                    File.WriteAllLines(File_Name, rtb_input_file.Lines);
                if (File.Exists(LL_TXT))
                    File.WriteAllLines(LL_TXT, rtb_ll_txt.Lines);


                //File_Name = ofd.FileName;
                //rtb_input_file.Lines = File.ReadAllLines(File_Name);
                //tc2.Visible = true;
                //sc3.Panel2Collapsed = true;
                astDoc = new ASTRADoc(File_Name);
                StructureAnalysis = null;
                ld = null;
                //if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                //    rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                //if (File.Exists(Analysis_File_Name))
                //    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);


                Load_ASTRA_Data();

                MessageBox.Show("Data Saved Sucessfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



                cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                txtUserTitle.Text = ml.GetString(2).Trim();
                cmb_structure_type.SelectedItem = ml.StringList[1];

            }
            //else if (menu_name == tsmi_dwg_save.Name || menu_name == tsb_save_drawing.Name)
            //{
            //    SaveDrawing(ActiveDoc);
            //}
            //else if (menu_name == tsmi_file_print.Name || menu_name == tsb_print_drawing.Name)
            //{
            //    if (ActiveDoc != null)
            //    {
            //        VectorDraw.Professional.ActionUtilities.vdCommandAction.PrintEx(ActiveDoc);
            //    }
            //}
            //else if (menu_name == tsmi_close.Name || menu_name == tsb_close.Name)
            //{
            //    this.Close();
            //}
        }

        private void dgv_max_frc_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_max_frc_SelectionChanged(object sender, EventArgs e)
        {
            Set_Max_Force_Column();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void tmrLoadDeflection_Tick(object sender, EventArgs e)
        {
            if (bIsNext)
                SetLoadIndex(+1);
            else
                SetLoadIndex(-1);
        }

        private void PP_tmr_moving_load_Tick(object sender, EventArgs e)
        {
            if (bIsNext)
                Set_Moving_LoadIndex(+1);
            else
                Set_Moving_LoadIndex(-1);

            //Chiranjit [2011 12 14]
            //When Full load case shown timer should be stop
            if (cmb_mov_loadcase.SelectedIndex == cmb_mov_loadcase.Items.Count - 1)
                Start_Moving_Timer(false);
        }

        private void cmb_diag_mem_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Diagram();



        }
        public void Select_Diagram()
        {

            int memNo = MyStrings.StringToInt(cmb_diag_mem_no.Text, 0);
            int loadNo = MyStrings.StringToInt(cmb_diag_ld_no.Text, 0);

            BeamForceMoment bfm = new BeamForceMoment();
            BeamForceMomentCollection bfc = new BeamForceMomentCollection();
            diagDoc.Palette.Background = Color.White;
            //bfc.

            if (StructureAnalysis != null)
            {

                AstraBeamMember bm = null;
                if (memNo != 0 && loadNo != 0)
                {
                    bm = StructureAnalysis.Get_Beam_Forces(memNo, loadNo);

                    bfm.Member.MemberNo = bm.BeamNo;
                    bfm.LoadCase = loadNo;

                    bfm.StartForceMoment.R1 = bm.StartNodeForce.R1_Axial;
                    bfm.StartForceMoment.R2 = bm.StartNodeForce.R2_Shear;
                    bfm.StartForceMoment.R3 = bm.StartNodeForce.R3_Shear;


                    bfm.StartForceMoment.M1 = bm.StartNodeForce.M1_Torsion;
                    bfm.StartForceMoment.M2 = bm.StartNodeForce.M2_Bending;
                    bfm.StartForceMoment.M3 = bm.StartNodeForce.M3_Bending;


                    bfm.EndForceMoment.R1 = bm.EndNodeForce.R1_Axial;
                    bfm.EndForceMoment.R2 = bm.EndNodeForce.R2_Shear;
                    bfm.EndForceMoment.R3 = bm.EndNodeForce.R3_Shear;


                    bfm.EndForceMoment.M1 = bm.EndNodeForce.M1_Torsion;
                    bfm.EndForceMoment.M2 = bm.EndNodeForce.M2_Bending;
                    bfm.EndForceMoment.M3 = bm.EndNodeForce.M3_Bending;

                    bfc.Add(bfm);

                    grb_diag_start.Text = "START NODE : " + bm.StartNodeForce.JointNo;
                    txt_diag_start_FX.Text = bm.StartNodeForce.R1_Axial.ToString("f3");
                    txt_diag_start_FY.Text = bm.StartNodeForce.R2_Shear.ToString("f3");
                    txt_diag_start_FZ.Text = bm.StartNodeForce.R3_Shear.ToString("f3");
                    txt_diag_start_MX.Text = bm.StartNodeForce.M1_Torsion.ToString("f3");
                    txt_diag_start_MY.Text = bm.StartNodeForce.M2_Bending.ToString("f3");
                    txt_diag_start_MZ.Text = bm.StartNodeForce.M3_Bending.ToString("f3");



                    grb_diag_end.Text = "END NODE : " + bm.EndNodeForce.JointNo;
                    txt_diag_end_FX.Text = bm.EndNodeForce.R1_Axial.ToString("f3");
                    txt_diag_end_FY.Text = bm.EndNodeForce.R2_Shear.ToString("f3");
                    txt_diag_end_FZ.Text = bm.EndNodeForce.R3_Shear.ToString("f3");
                    txt_diag_end_MX.Text = bm.EndNodeForce.M1_Torsion.ToString("f3");
                    txt_diag_end_MY.Text = bm.EndNodeForce.M2_Bending.ToString("f3");
                    txt_diag_end_MZ.Text = bm.EndNodeForce.M3_Bending.ToString("f3");


                    bfc.CopyMembers(AST_DOC.Members);


                    BeamForceMomentCollection.eForce fc = BeamForceMomentCollection.eForce.M1;
                    if (rbtn_diag_MX.Checked)
                    {
                        //bfc.DrawBeamForceMoment_M1(PP_ActiveDoc, 0);
                        //fc = BeamForceMomentCollection.eForce.R1;
                        fc = BeamForceMomentCollection.eForce.M1;
                    }
                    if (rbtn_diag_MY.Checked)
                    {
                        //bfc.DrawBeamForceMoment_M2(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.M2;
                    }
                    if (rbtn_diag_MZ.Checked)
                    {
                        //bfc.DrawBeamForceMoment_M3(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.M3;
                    }

                    if (rbtn_diag_FX.Checked)
                    {
                        //bfc.DrawBeamForceMoment_R1(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.R1;
                    }
                    if (rbtn_diag_FY.Checked)
                    {
                        //bfc.DrawBeamForceMoment_R2(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.R2;
                    }
                    if (rbtn_diag_FZ.Checked)
                    {
                        //bfc.DrawBeamForceMoment_R3(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.R3;
                    }

                    //bfc.DrawBeamForceMoment_Bending(PP_ActiveDoc, memNo, loadNo, BeamForceMomentCollection.eForce.M1, )

                    bfc.DrawBeamForceMoment(diagDoc, memNo, fc, loadNo);
                }

            }
        }

        private void dgv_max_frc_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            Set_Max_Force_Column();
        }
        private void Set_Max_Force_Column()
        {
            try
            {
                bool flag = (dgv_max_frc[1, dgv_max_frc.SelectedCells[0].RowIndex].Value.ToString() == "BEAM");

                if (flag)
                {
                    col_cmf.Width = 0;
                    col_tf.Width = 0;
                    col_tor.Width = 99;
                    col_af.Width = 99;
                    col_bm.Width = 99;
                    col_sf.Width = 99;
                }
                else
                {
                    col_cmf.Width = 99;
                    col_tf.Width = 99;
                    col_tor.Width = 0;
                    col_af.Width = 0;
                    col_bm.Width = 0;
                    col_sf.Width = 0;
                }
            }
            catch (Exception ex) { }
        }

        private void tv_mem_grps_MouseMove(object sender, EventArgs e)
        {

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
                        VDoc.SaveAs(sfd.FileName);
                        Drawing_File = sfd.FileName;

                        System.Environment.SetEnvironmentVariable("OPENFILE", Drawing_File);
                        //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                        System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                    }
                }
            }
        }

        private void lblLoadCase_TextChanged(object sender, EventArgs e)
        {
            lbl_mode_shape.Visible = lblLoadCase.Text.ToLower().StartsWith("e");
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



            sb.Lx = MyList.StringToDouble(txt_slab_Lx.Text, 0.0);
            sb.Ly = MyList.StringToDouble(txt_slab_Ly.Text, 0.0);
            sb.Floor_Level = MyList.StringToDouble(txt_slab_flv_lvl.Text, 0.0);

            sb.case_index = cmb_slab_type.SelectedIndex;

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

        private void Program_Slab()
        {

            frm_Slab_BOQ fsboq = new frm_Slab_BOQ(AST_DOC);
            fsboq.ShowDialog();

            txt_slab_Lx.Text = fsboq.BeamNos;



            MyList ml = new MyList(txt_slab_Lx.Text, ' ');



            MemberIncidenceCollection mic = new MemberIncidenceCollection();

            //int beam1 = MyList.StringToInt(txt_slab_beam1.Text, 0);
            //int beam2 = MyList.StringToInt(txt_slab_beam2.Text, 0);
            //int beam3 = MyList.StringToInt(txt_slab_beam3.Text, 0);
            //int beam4 = MyList.StringToInt(txt_slab_beam4.Text, 0);

            int beam1, beam2, beam3, beam4;

            try
            {
                beam1 = ml.GetInt(0);
                beam2 = ml.GetInt(1);
                beam3 = ml.GetInt(2);
                beam4 = ml.GetInt(3);
            }
            catch (Exception eeexx)
            {
                MessageBox.Show(this, "Wrong Input Data.");
                return;
            }






            #region  Beam Check
            MemberIncidence mi = AST_DOC.Members.Get_Member(beam1);


            if (mi == null)
            {
                MessageBox.Show("Beam 1 data not accepted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MemberIncidence mi2 = AST_DOC.Members.Get_Member(beam2);

            if (mi2 == null)
            {
                MessageBox.Show("Beam 2 data not accepted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MemberIncidence mi3 = AST_DOC.Members.Get_Member(beam3);
            if (mi3 == null)
            {
                MessageBox.Show("Beam 3 data not accepted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MemberIncidence mi4 = AST_DOC.Members.Get_Member(beam4);

            if (mi4 == null)
            {
                MessageBox.Show("Beam 4 data not accepted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            mic.Add(mi);
            mic.Add(mi2);
            mic.Add(mi3);
            mic.Add(mi4);


            string msg = "";
            bool flag = false;

            int coor_flag = 0;


            if ((mi.EndNode.NodeNo == mi2.StartNode.NodeNo) ||
                (mi.EndNode.NodeNo == mi2.EndNode.NodeNo))
            {
                flag = true;
                coor_flag = 0;
            }
            else if ((mi.StartNode.NodeNo == mi2.StartNode.NodeNo) ||
                (mi.StartNode.NodeNo == mi2.EndNode.NodeNo))
            {
                flag = true;
                coor_flag = 1;
            }
            else
            {
                flag = false;

                msg = string.Format("Beam 1 : {0} [{1} {2}], Beam 2 : {3} [{4} {5}] Joint Mismatch....\n\n",
                    mi.MemberNo, mi.StartNode.NodeNo, mi.EndNode.NodeNo,
                    mi2.MemberNo, mi2.StartNode.NodeNo, mi2.EndNode.NodeNo);
            }
            if (flag)
            {
                if ((mi2.EndNode.NodeNo == mi3.StartNode.NodeNo) ||
                     (mi2.EndNode.NodeNo == mi3.EndNode.NodeNo) ||
                     (mi2.StartNode.NodeNo == mi3.StartNode.NodeNo) ||
                     (mi2.StartNode.NodeNo == mi3.EndNode.NodeNo))
                {
                    flag = true;
                }
                else
                {
                    flag = false;

                    msg = string.Format("Beam 2 : {0} [{1} {2}], Beam 3 : {3} [{4} {5}] Joint Mismatch....\n\n",
                           mi2.MemberNo, mi2.StartNode.NodeNo, mi2.EndNode.NodeNo,
                        mi3.MemberNo, mi3.StartNode.NodeNo, mi3.EndNode.NodeNo);
                }
            }
            if (flag)
            {
                if ((mi3.StartNode.NodeNo == mi4.StartNode.NodeNo) ||
                     (mi3.StartNode.NodeNo == mi4.EndNode.NodeNo) ||
                     (mi3.EndNode.NodeNo == mi4.StartNode.NodeNo) ||
                     (mi3.EndNode.NodeNo == mi4.EndNode.NodeNo))
                {
                    flag = true;
                }
                else
                {
                    msg = string.Format("Beam 3 : {0} [{1} {2}], Beam 4 : {3} [{4} {5}] Joint Mismatch....\n\n",
                          mi3.MemberNo, mi3.StartNode.NodeNo, mi3.EndNode.NodeNo,
                          mi4.MemberNo, mi4.StartNode.NodeNo, mi4.EndNode.NodeNo);

                    flag = false;
                }
            }
            if (flag)
            {

                if ((mi4.StartNode.NodeNo == mi.StartNode.NodeNo) ||
                     (mi4.EndNode.NodeNo == mi.StartNode.NodeNo))
                {
                    flag = true;
                }
                else if ((mi4.StartNode.NodeNo == mi.EndNode.NodeNo) ||
                     (mi4.EndNode.NodeNo == mi.EndNode.NodeNo))
                {
                    flag = true;
                }
                else
                {
                    flag = false;

                    msg = string.Format("Beam 4 : {0} [{1} {2}], Beam 1 : {3} [{4} {5}] Joint Mismatch....\n\n",
                        //msg = string.Format("Beam 4 [{0} {1}], Beam 1 [{2} {3}] Joint Mismatch....\n\n",
                          mi4.MemberNo, mi4.StartNode.NodeNo, mi4.EndNode.NodeNo,
                          mi.MemberNo, mi.StartNode.NodeNo, mi.EndNode.NodeNo);

                }
            }
            if (!flag)
            {
                MessageBox.Show(msg + "Data not accepted.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            #endregion  Beam Check

            List<string> list = new List<string>();
            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*          ASTRA Pro Release 18.0            *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*            DESIGN OF RCC SLAB              *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion


            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("  Beam#   Member#   Joint1        X         Y           Z      Joint2         X         Y           Z       Length"));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------"));
            for (int i = 0; i < mic.Count; i++)
            {
                list.Add(string.Format("{0,5} {1,9} {2,8} {3,12:f3} {4,10:f3} {5,10:f3} {6,8} {7,12:f3} {8,10:f3} {9,10:f3} {10,10:f3}", (i + 1),
                    mic[i].MemberNo,
                    mic[i].StartNode.NodeNo,
                    mic[i].StartNode.Point.x,
                    mic[i].StartNode.Point.y,
                    mic[i].StartNode.Point.z,
                    mic[i].EndNode.NodeNo,
                    mic[i].EndNode.Point.x,
                    mic[i].EndNode.Point.y,
                    mic[i].EndNode.Point.z,
                    mic[i].Length));
            }
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------"));



            #region User Input Data
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------"));
            list.Add(string.Format("USER INPUT DATA"));
            list.Add(string.Format("----------------"));
            list.Add(string.Format(""));
            list.Add("Position & Case Type of Slab");
            list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));




            //list.Add("Case 4");
            list.Add(string.Format("Case {0} {1}", cmb_slab_type.SelectedIndex + 1, cmb_slab_type.Text));


            double Lx = Math.Min(mic[0].Length, mic[1].Length);
            double Ly = Math.Max(mic[0].Length, mic[1].Length);

            list.Add(string.Format("Lx = {0:f3} m,     Ly = {1:f3} m", Lx, Ly));


            double D, fck, gamma, fy, wll, d1, d2, d3, d4, dlf, llf;

            D = MyList.StringToDouble(txt_slab_D.Text, 0.0);
            fck = MyList.StringToDouble(cmb_slab_fck.Text.Replace("M", ""), 0.0);
            gamma = MyList.StringToDouble(txt_slab_gamma.Text, 0.0);
            fy = MyList.StringToDouble(cmb_slab_fy.Text.Replace("Fe", ""), 0.0);
            wll = MyList.StringToDouble(txt_slab_wll.Text, 0.0);
            d1 = MyList.StringToDouble(txt_slab_d1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_slab_d2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_slab_d3.Text, 0.0);
            d4 = MyList.StringToDouble(txt_slab_c.Text, 0.0);
            dlf = MyList.StringToDouble(txt_slab_DLF.Text, 0.0);
            llf = MyList.StringToDouble(txt_slab_LLF.Text, 0.0);



            list.Add(string.Format("Thickness of Slab = D = {0} mm", D));
            list.Add(string.Format("Concrete Grade = fck = M{0}", fck));
            list.Add(string.Format("Unit Weight = γ = {0} kN/Cu.m", fck));
            list.Add(string.Format("Live Load = WLL = {0} kN/Sq.m", wll));
            list.Add(string.Format("Diameter of Main Reinforcement Bars = d1 = {0} mm", d1));
            list.Add(string.Format("Diameter of Main Reinforcement Bars in Edge Strip at Bottom = d2 = {0} mm", d2));
            list.Add(string.Format("Diameter of Main Reinforcement Bars in Corners at Top & Bottom = d3 = {0} mm", d3));
            list.Add(string.Format("Cover for Reinforcement = d4 = {0} mm", d4));
            list.Add(string.Format("Dead Load Factor = DLF = {0}", dlf));
            list.Add(string.Format("Live Load Factor = LLF = {0}", llf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion User Input Data

            #region  STEP 1 : Effective Depth

            list.Add(string.Format("STEP 1 : Effective Depth"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));

            double d = D - (d1 / 2) - d4;
            list.Add(string.Format("Effective Depth = d = D - (d1 / 2) - d4"));
            list.Add(string.Format("                    = {0} - ({1} / 2) - {2}", D, d1, d4));
            list.Add(string.Format("                    = {0:f3} mm", d));
            list.Add(string.Format(""));

            #endregion STEP 1 : Effective Depth

            #region STEP 2 : Design Load

            list.Add(string.Format("STEP 2 : Design Load"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Define Dead Loads"));
            list.Add(string.Format("------------------"));

            double WDL = D / 1000 * gamma;
            //list.Add(string.Format("Dead Load = WDL = D x 1.0 x γ = ({0}/1000) x 1.0 x {1} = {2:f3} kN/Sq.m ", D, gamma, WDL));

            double dl_sw = WDL;
            list.Add(string.Format("Self Weight = D x 1.0 x γ = ({0}/1000) x 1.0 x {1} = {2:f3} kN/Sq.m ", D, gamma, WDL));
            list.Add(string.Format(""));
            for (int i = 0; i < dgv_dls.RowCount - 1; i++)
            {
                try
                {
                    list.Add(string.Format("{0}   = {1:f3} kN/Sq.m", dgv_dls[0, i].Value, dgv_dls[1, i].Value));
                    WDL += MyList.StringToDouble(dgv_dls[1, i].Value.ToString(), 0.0);
                }
                catch (Exception exx) { }
            }
            list.Add(string.Format(""));
            list.Add(string.Format("Total Dead Load = WDL = {0:f3} kN/Sq.m", WDL));
            list.Add(string.Format(""));




            list.Add(string.Format("Live Load = WLL = {0} kN/Sq.m", llf));
            list.Add(string.Format(""));

            double w = WDL * dlf + wll * llf;
            list.Add(string.Format("Design Load = w = WDL x DLF + WLL x LLF"));
            list.Add(string.Format("                = {0:f3} x {1} + {2:f3} x {3}", WDL, dlf, wll, llf));
            list.Add(string.Format("                = {0:f3} kN/Sq.m", w));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            #endregion STEP 2 : Design Load

            #region STEP 3 : Type of Slab

            list.Add(string.Format("STEP 3 : Type of Slab"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            double LY_by_LX = Ly / Lx;
            list.Add(string.Format("Ly = {0:f3} m,    Lx = {1:f3} m,   Ly/Lx = {0:f3}/{1:f3} = {2:f3}", Lx, Ly, LY_by_LX));
            list.Add(string.Format(""));

            if (LY_by_LX < 2.0)
                list.Add(string.Format("This is Two Way Slab Type"));
            else
                list.Add(string.Format("This is One Way Slab Type"));
            list.Add(string.Format(""));

            //To add more cases
            //list.Add(string.Format("Two Adjacent Edges are Discontinuous - Case 4"));
            list.Add(string.Format("{0} - Case {1}", cmb_slab_type.Text, cmb_slab_type.SelectedIndex + 1));
            list.Add(string.Format(""));


            double beta_x = 0.084;
            double beta_y = 0.063;
            double alpha_x = 0.047;
            double alpha_y = 0.035;

            Get_Bending_Moment_Coefficients(out beta_x, out  beta_y, out  alpha_x, out  alpha_y, LY_by_LX);

            beta_x = Math.Pow(LY_by_LX, 4.0) / (8 * (1 + Math.Pow(LY_by_LX, 4.0)));
            beta_y = Math.Pow(LY_by_LX, 2.0) / (8 * (1 + Math.Pow(LY_by_LX, 4.0)));


            list.Add(string.Format("βx = {0:f4},   βy = {1:f4},   ", beta_x, beta_y));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment Coefficients for Simply Supported Two-Way Slabs"));
            list.Add(string.Format(""));
            list.Add(string.Format("   βx = (Ly/Lx)^4 / (8 x (1 + (Ly/Lx)^4))"));
            list.Add(string.Format("      = ({0:f3})^4 / (8 x (1 + ({0:f3})^4))", LY_by_LX));
            list.Add(string.Format("      = {0:f3}", beta_x));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("   βy = (Ly/Lx)^2 / (8 x (1 + (Ly/Lx)^4))"));
            list.Add(string.Format("      = ({0:f3})^2 / (8 x (1 + ({0:f3})^4))", LY_by_LX));
            list.Add(string.Format("      = {0:f3}", beta_y));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("  αx = {0:f4},   αy = {1:f4},   (from TABLE 1)", alpha_x, alpha_y));
            #endregion STEP 3 : Type of Slab

            #region STEP 4 : Check for Shear


            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4 : Check for Shear"));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format(""));

            double gamma_x1 = 0.57;
            double gamma_x2 = 0.38;
            double gamma_x3 = 0.57;
            double gamma_x4 = 0.38;


            Get_Shear_Force_Coefficients(out gamma_x1, out  gamma_x2, out  gamma_x3, out  gamma_x4, LY_by_LX);


            double gamma_x_max = Math.Max(gamma_x1, gamma_x2);

            list.Add(string.Format(""));
            list.Add(string.Format("Case {0} ,  γx = {1:f4} and {2:f4},   γx_max = {3:f4}   (from Table 2)", cmb_slab_type.SelectedIndex + 1, gamma_x1, gamma_x2, gamma_x_max));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Shear Force = V = γx_max  x  w  x Lx "));

            double V = gamma_x_max * w * Lx;
            list.Add(string.Format("                        = {0:f4}  x {1:f3} x {2:f3} ", gamma_x_max, w, Lx));
            list.Add(string.Format("                        = {0:f3} kN", V));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double v = (V * 1000) / (1000 * d);
            list.Add(string.Format("Shear Stress = v = (V x 1000) / (1000 x d)"));
            list.Add(string.Format("                 = ({0:f3} x 1000) / (1000 x {1:f3})", V, d));
            list.Add(string.Format("                 = {0:f3} N/Sq.mm", v));
            list.Add(string.Format(""));






            //Bar Diameter = 12 mm

            double Abar = Math.PI * d1 * d1 / 4.0;


            double p = (Abar * 100.0) / (D * d);
            //sw.WriteLine("Percentage of Steel = p = ({0} * 100) / ({1} * {2})", bar_ast, (b * 1000), d);
            //sw.WriteLine("                        = {0:f2}%", p);
            //sw.WriteLine();




            //double tab_value = 0.29; // from Table 3
            string ref_str = "";
            double tab_value = Permissible_Shear_Stress(p, (int)fck, ref ref_str);
            if (tab_value > v)
                list.Add(string.Format("Minimum Shear Strength for M{0} Grade Concrete = {1:f3} (TABLE 3) > {2:f3} , OK", fck, tab_value, v));
            else
                list.Add(string.Format("Minimum Shear Strength for M{0} Grade Concrete = {1:f3} (TABLE 3) < {2:f3} , NOT OK", fck, tab_value, v));
            list.Add(string.Format(""));


            #endregion STEP 4 : Check for Shear

            #region STEP 5 : Bending Moment

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5 : Bending Moment"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5.1 : Short Direction"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Negative Bending Moment on Continuous Edge"));
            list.Add(string.Format(""));


            double M1 = beta_x * w * Lx * Lx;
            list.Add(string.Format("   M1 = βx  x  w  x  Lx^2"));
            list.Add(string.Format("      = {0:f4}  x  {1:f3}  x  {2:f3}^2", beta_x, w, Lx));
            list.Add(string.Format("      = {0:f4} kN-m", M1));

            list.Add(string.Format(""));
            list.Add(string.Format("Positive Bending Moment at Mid Span"));
            list.Add(string.Format(""));
            double M2 = beta_y * w * Lx * Lx;
            list.Add(string.Format("   M2 = βy  x  w  x  Lx^2"));
            list.Add(string.Format("      = {0:f4}  x  {1:f3}  x  {2:f3}^2", beta_x, w, Lx));
            list.Add(string.Format("      = {0:f4} kN-m", M1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Negative Bending Moment on Discontinuous Edge"));
            list.Add(string.Format(""));

            double M3 = M2 / 2.0;
            list.Add(string.Format("   M3 =  (Positive BM at Mid Span) / 2 = M2 / 2 = {0:f3}/2 = {1:f3} kN-m", M2, M3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5.2 : Long Direction"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double alpha_min = Math.Min(alpha_x, alpha_y);
            list.Add(string.Format("αx = {0:f4},   αy = {1:f4},    α_min = {2:f4}", alpha_x, alpha_y, alpha_min));
            list.Add(string.Format(""));
            list.Add(string.Format("Positive Bending Moment at Mid Span"));
            list.Add(string.Format(""));


            double M4 = alpha_min * w * Lx * Lx;
            list.Add(string.Format("   M4 = α_min x w x Lx^2"));
            list.Add(string.Format("      = {0:f4} x {1:f3} x  {2:f3}^2", alpha_min, w, Lx));
            list.Add(string.Format("      = {0:f3} kN-m", M4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Negative Bending Moment on Continuous Edge"));
            list.Add(string.Format(""));

            double M5 = (4.0 / 3.0) * M4;
            list.Add(string.Format("   M5 = (4 / 3) x M4 = (4 / 3) x {0:f3} = {1:f3} kN-m", M4, M5));
            list.Add(string.Format(""));
            list.Add(string.Format("Negative Bending Moment on Discontinuous Edge"));
            list.Add(string.Format(""));
            double M6 = (1.0 / 2.0) * M4;
            list.Add(string.Format("   M6 = (1 / 2) x M4 = (1 / 2) x {0:f3} = {1:f3} kN-m", M4, M6));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion STEP 5 : Bending Moment

            #region STEP 6 : Reinforcement Steel

            list.Add(string.Format("STEP 6 : Reinforcement Steel"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Diameter = {0} mm", d1));
            list.Add(string.Format(""));
            Abar = Math.PI * d1 * d1 / 4.0;
            list.Add(string.Format("Area of Bar = Abar = 3.1416 x {0}^2 / 4 = {1:f3} Sq.mm.", d1, Abar));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double M = M1;

            double j = 0.5 + Math.Pow(Math.Abs(0.25 - (M * 1000000) / (0.87 * fck * 1000 * d * d)), 0.5);
            //double j = 0.5 + Math.Pow((0.25 - ((M * 1000000) / (0.87 * fck * 1000 * d1 * d1))), 0.5);
            double Ast = (M * 1000000) / (0.87 * fy * d * j);
            double Spacing = (1000 * Abar) / (Ast);

            List<double> list_j = new List<double>();
            List<double> list_Ast = new List<double>();
            List<double> list_spacing = new List<double>();


            list_j.Add(j);
            list_Ast.Add(Ast);
            list_spacing.Add(Spacing);


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Short Direction (Continuous Edge)"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment = M1 = -{0:f3} kN-m", M));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = 0.5 + ((0.25 - (M x 10^6) / (0.87 x fck x 1000 x d^2))^0.5"));
            list.Add(string.Format("                     = 0.5 + ((0.25 - ({0:f3} x 10^6) / (0.87 x {1} x 1000 x {2}^2))^0.5", M, fck, d));
            list.Add(string.Format("                     = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Steel = Ast = (M x 10^6) / (0.87 x fy x d x j)"));
            list.Add(string.Format("                    = ({0:f3} x 10^6) / (0.87 x {1} x {2} x {3:f3})", M, fy, d, j));
            list.Add(string.Format("                    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));



            #region   Short Direction (Mid Span)

            M = M2;
            j = 0.5 + Math.Pow(Math.Abs(0.25 - (M * 1000000) / (0.87 * fck * 1000 * d * d)), 0.5);
            Ast = (M * 1000000) / (0.87 * fy * d * j);
            Spacing = (1000 * Abar) / (Ast);

            list_j.Add(j);
            list_Ast.Add(Ast);
            list_spacing.Add(Spacing);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Short Direction (Mid Span)"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment = M2 = {0:f3} kN-m", M));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = 0.5 + ((0.25 - (M2 x 10^6) / (0.87 x fck x 1000 x d^2))^0.5"));
            list.Add(string.Format("                     = 0.5 + ((0.25 - ({0:f3} x 10^6) / (0.87 x {1} x 1000 x {2}^2))^0.5", M, fck, d));
            list.Add(string.Format("                     = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Steel = Ast = (M x 10^6) / (0.87 x fy x d x j)"));
            list.Add(string.Format("                    = ({0:f3} x 10^6) / (0.87 x {1} x {2} x {3:f3})", M, fy, d, j));
            list.Add(string.Format("                    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));
            #endregion   Short Direction (Mid Span)

            #region   Short Direction (Discontinuous Edge)

            M = M3;
            j = 0.5 + Math.Pow(Math.Abs(0.25 - (M * 1000000) / (0.87 * fck * 1000 * d * d)), 0.5);
            Ast = (M * 1000000) / (0.87 * fy * d * j);
            Spacing = (1000 * Abar) / (Ast);

            list_j.Add(j);
            list_Ast.Add(Ast);
            list_spacing.Add(Spacing);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Short Direction (Discontinuous Edge)"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment = M3 = -{0} kN-m", M));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = 0.5 + ((0.25 - (M x 10^6) / (0.87 x fck x 1000 x d^2))^0.5"));
            list.Add(string.Format("                     = 0.5 + ((0.25 - ({0:f3} x 10^6) / (0.87 x {1} x 1000 x {2}^2))^0.5", M, fck, d));
            list.Add(string.Format("                     = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Steel = Ast = (M x 10^6) / (0.87 x fy x d x j)"));
            list.Add(string.Format("                    = ({0:f3} x 10^6) / (0.87 x {1} x {2} x {3:f3})", M, fy, d, j));
            list.Add(string.Format("                    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));
            #endregion   Short Direction (Discontinuous Edge)

            #region   Long Direction (Mid Span)

            M = M4;
            j = 0.5 + Math.Pow(Math.Abs(0.25 - (M * 1000000) / (0.87 * fck * 1000 * d * d)), 0.5);
            Ast = (M * 1000000) / (0.87 * fy * d * j);
            Spacing = (1000 * Abar) / (Ast);

            list_j.Add(j);
            list_Ast.Add(Ast);
            list_spacing.Add(Spacing);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Long Direction (Mid Span)"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment = M2 = {0} kN-m", M));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = 0.5 + ((0.25 - (M x 10^6) / (0.87 x fck x 1000 x d^2))^0.5"));
            list.Add(string.Format("                     = 0.5 + ((0.25 - ({0:f3} x 10^6) / (0.87 x {1} x 1000 x {2}^2))^0.5", M, fck, d));
            list.Add(string.Format("                     = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Steel = Ast = (M x 10^6) / (0.87 x fy x d1 x j)"));
            list.Add(string.Format("                    = ({0:f3} x 10^6) / (0.87 x {1} x {2} x {3:f3})", M, fy, d, j));
            list.Add(string.Format("                    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));
            #endregion   Long Direction (Mid Span)

            #region   Long Direction (Continuous Edge)

            M = M5;
            j = 0.5 + Math.Pow(Math.Abs(0.25 - (M * 1000000) / (0.87 * fck * 1000 * d * d)), 0.5);
            Ast = (M * 1000000) / (0.87 * fy * d * j);
            Spacing = (1000 * Abar) / (Ast);

            list_j.Add(j);
            list_Ast.Add(Ast);
            list_spacing.Add(Spacing);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Long Direction (Continuous Edge)"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment = M5 = -{0} kN-m", M));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = 0.5 + ((0.25 - (M x 10^6) / (0.87 x fck x 1000 x d^2))^0.5"));
            list.Add(string.Format("                     = 0.5 + ((0.25 - ({0:f3} x 10^6) / (0.87 x {1} x 1000 x {2}^2))^0.5", M, fck, d));
            list.Add(string.Format("                     = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Steel = Ast = (M x 10^6) / (0.87 x fy x d x j)"));
            list.Add(string.Format("                    = ({0:f3} x 10^6) / (0.87 x {1} x {2} x {3:f3})", M, fy, d, j));
            list.Add(string.Format("                    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));
            #endregion   Long Direction (Continuous Edge)

            #region   Long Direction (Discontinuous Edge)

            M = M6;
            j = 0.5 + Math.Pow(Math.Abs(0.25 - (M * 1000000) / (0.87 * fck * 1000 * d * d)), 0.5);
            Ast = (M * 1000000) / (0.87 * fy * d * j);
            Spacing = (1000 * Abar) / (Ast);

            list_j.Add(j);
            list_Ast.Add(Ast);
            list_spacing.Add(Spacing);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Short Direction (Discontinuous Edge)"));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment = M6 = -{0} kN-m", M));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever Arm Factor = j = 0.5 + ((0.25 - (M x 10^6) / (0.87 x fck x 1000 x d1^2))^0.5"));
            list.Add(string.Format("                     = 0.5 + ((0.25 - ({0:f3} x 10^6) / (0.87 x {1} x 1000 x {2}^2))^0.5", M, fck, d));
            list.Add(string.Format("                     = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Steel = Ast = (M x 10^6) / (0.87 x fy x d1 x j)"));
            list.Add(string.Format("                    = ({0:f3} x 10^6) / (0.87 x {1} x {2} x {3:f3})", M, fy, d, j));
            list.Add(string.Format("                    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));

            #endregion   Long Direction (Discontinuous Edge)




            list.Add(string.Format(""));

            double dis_reinf = (0.12 / 100) * 1000 * d;
            list.Add(string.Format(""));

            double max_sp_steel = list_Ast[1];
            list.Add(string.Format("Maximum Span Steel = {0:f3} Sq.mm", max_sp_steel));
            list.Add(string.Format(""));
            list.Add(string.Format("Distribution Reinforcement = (0.12 / 100) x 1000 x d "));
            list.Add(string.Format("                           = (0.0012) x 1000 x {0:f3} ", d));
            list.Add(string.Format("                           = {0:f3} ", dis_reinf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion STEP 6 : Reinforcement Steel

            #region STEP 7 : Corner Reinforcements (Top & Bottom)


            list.Add(string.Format("STEP 7 : Corner Reinforcements (Top & Bottom)"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("  As = Area of Steel >= (3/8) x Maximum Span Steel"));
            list.Add(string.Format("                      = (3/8) x {0:f3}", max_sp_steel));

            Ast = (3.0 / 8.0) * max_sp_steel;
            list.Add(string.Format("                      = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));
            list.Add(string.Format("Diameter of Reinforcement Bar = d4 = {0} mm", d4));
            list.Add(string.Format(""));
            Abar = Math.PI * d4 * d4 / 4.0;
            list.Add(string.Format("Area of Bar = Abar = 3.1416 x {0}^2 / 4 = {1:f3} Sq.mm.", d4, Abar));

            list.Add(string.Format(""));
            Spacing = (Abar * 1000) / Ast;
            list.Add(string.Format("Bar Spacing = (1000 * Abar) / (As) = (1000 * {0:f3}) / ({1:f3}) = {2:f3}", Abar, Ast, Spacing));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion STEP 7 : Corner Reinforcements (Top & Bottom)

            #region STEP 8 : Reinforcement for Edge Strip

            list.Add(string.Format("STEP 8 : Reinforcement for Edge Strip"));
            list.Add(string.Format("-------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("   Steel >= Distribution Steel"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 9 : Reinforcement for Edge Strip"));
            list.Add(string.Format("-------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel = 0.5 x Span Reinforcement"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion STEP 8 : Reinforcement for Edge Strip


            #region STEP 10 : Bill Of Quantity

            list.Add(string.Format("STEP 10 : Bill Of Quantity"));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: SLAB"));
            list.Add(string.Format(""));
            list.Add(string.Format("SLAB Concrete"));
            list.Add(string.Format(""));

            //Ly
            string beams = string.Format("{0},{1},{2},{3}", beam1, beam2, beam3, beam4);
            list.Add(string.Format("S.No.      Beam Nos.                           Section       Floor              Slab        Floor         Quantity"));
            list.Add(string.Format("                                                             Elevation        Thickness     Area        Area x Thickness  "));
            list.Add(string.Format("                                                                                (mm)        (Sq.m)           (Cu.m)   "));
            list.Add(string.Format(""));
            //list.Add(string.Format(" 1. {0,20} {1:f3} {2:f3}  {3:f3}   {4:f3}  {5:f3}  {6:f3}                              3.8                 160              104.0             104.0 x 0.160"));
            list.Add(string.Format(" 1. {0,20} {1:f3}x{2:f3}  {3:f3}   {4:f3}  {5:f3}  {6:f3}", beams, Ly, Lx, mi.StartNode.Z, D, Lx * Ly, Lx * Ly * D / 1000));
            //list.Add(string.Format(" 2."));
            list.Add(string.Format("                                                                      --------------------------------------------"));
            list.Add(string.Format("                                                                      Total Concrete (Cu.m)"));
            list.Add(string.Format(""));
            list.Add(string.Format("SLAB Steel Reinforcement"));
            list.Add(string.Format(""));
            list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
            list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            list.Add(string.Format("1.     SAst1      200     12    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            list.Add(string.Format(""));
            list.Add(string.Format("2."));
            list.Add(string.Format("                                                                      ------------------------------------"));
            list.Add(string.Format("                                                                      Total Steel (M.TON)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion STEP 8 : Reinforcement for Edge Strip


            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("TABLE 1 : BENDING MOMENT COEFFICIENTS"));
            list.Add(string.Format("------------------------------------"));
            list.AddRange(Get_File_Bending_Moment_Coefficients().ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("TABLE 2 : SHEAR FORCE COEFFICIENTS"));
            list.Add(string.Format("------------------------------------"));
            list.AddRange(Get_File_Shear_Force_Coefficients().ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format("TABLE 3 : PERMISSIBLE SHEAR STRESS"));
            list.Add(string.Format("------------------------------------"));
            list.AddRange(Get_File_Permissible_Shear_Stress().ToArray());
            list.Add(string.Format(""));
            #region End of Report
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            #endregion


            //rtb_BD_res.Lines = list.ToArray();
            string rep_file = Slab_Design_Report;

            File.WriteAllLines(rep_file, list.ToArray());

            MessageBox.Show("Report file created in file " + rep_file);
            frmASTRAReport.OpenReport(rep_file, this);
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


        public void Get_Bending_Moment_Coefficients(out double beta_x, out double beta_y, out double alpha_x, out double alpha_y, double Ly_by_Lx)
        {

            beta_x = 0.084;
            beta_y = 0.063;
            alpha_x = 0.047;
            alpha_y = 0.035;


            List<double> list = new List<double>();

            List<List<double>> list_1 = new List<List<double>>();

            List<double> list_2 = new List<double>();





            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Bending_Moment_Coefficients.txt");

            if (File.Exists(tab_file))
            {
                List<string> file_content = new List<string>(File.ReadAllLines(tab_file));

                string kStr = "";
                MyList mlist = null;

                int flag = 0;
                double dd = 0.0;

                int i = 0;
                for (i = 0; i < file_content.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(file_content[i].ToUpper().TrimEnd().TrimStart().Trim());

                    if (kStr.StartsWith("------"))
                    {
                        flag++;
                        continue;
                    }

                    if (flag == 1)
                    {
                        mlist = new MyList(kStr, ' ');

                        for (int j = 0; j < mlist.Count; j++)
                        {
                            try
                            {
                                list_2.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { }
                        }
                    }
                    else if (flag == 2)
                    {
                        mlist = new MyList(kStr, ' ');
                        for (int j = mlist.Count - 1; j > 0; j--)
                        {
                            try
                            {
                                list.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { break; }
                        }

                        if (list.Count > 0)
                        {
                            list.Reverse();
                            list_1.Add(list);
                            list = new List<double>();
                        }
                    }
                }

                //Ly_by_Lx = 1.75;
                for (i = 1; i < list_2.Count; i++)
                {
                    if (Ly_by_Lx >= list_2[i - 1] && Ly_by_Lx <= list_2[i])
                    {
                        int indx = cmb_slab_type.SelectedIndex;

                        indx = indx * 2;

                        beta_x = list_1[indx][i];
                        beta_y = list_1[indx + 1][i];

                        alpha_x = list_1[indx][list_1[indx].Count - 1];
                        alpha_y = list_1[indx + 1][list_1[indx].Count - 1];
                        break;
                    }
                }
            }
        }



        public void Get_Shear_Force_Coefficients(out double gamma_x1, out double gamma_x2, out double gamma_x3, out double gamma_x4, double Ly_by_Lx)
        {

            gamma_x1 = 0.57;
            gamma_x2 = 0.38;

            gamma_x3 = 0.57;
            gamma_x4 = 0.38;


            List<double> list = new List<double>();

            List<List<double>> list_1 = new List<List<double>>();

            List<double> list_2 = new List<double>();





            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Shear_Force_Coefficients.txt");

            if (File.Exists(tab_file))
            {
                List<string> file_content = new List<string>(File.ReadAllLines(tab_file));

                string kStr = "";
                MyList mlist = null;

                int flag = 0;
                double dd = 0.0;

                int i = 0;
                for (i = 0; i < file_content.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(file_content[i].ToUpper().TrimEnd().TrimStart().Trim());

                    if (kStr.StartsWith("------"))
                    {
                        flag++;
                        continue;
                    }

                    if (flag == 1)
                    {
                        mlist = new MyList(kStr, ' ');

                        for (int j = 0; j < mlist.Count; j++)
                        {
                            try
                            {
                                list_2.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { }
                        }
                    }
                    else if (flag == 2)
                    {
                        mlist = new MyList(kStr, ' ');
                        for (int j = mlist.Count - 1; j > 0; j--)
                        {
                            try
                            {
                                list.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { break; }
                        }

                        if (list.Count > 2)
                        {
                            list.Reverse();
                            list_1.Add(list);
                        }
                        list = new List<double>();
                    }
                }

                //Ly_by_Lx = 1.75;
                for (i = 1; i < list_2.Count; i++)
                {
                    if (Ly_by_Lx >= list_2[i - 1] && Ly_by_Lx <= list_2[i])
                    {
                        int indx = cmb_slab_type.SelectedIndex;
                        if (indx == 8 || indx == 0 || indx == 1)
                            indx = 0;
                        else
                            indx = indx - 1;


                        indx = indx * 2;

                        gamma_x1 = list_1[indx][i];
                        gamma_x2 = list_1[indx + 1][i];

                        gamma_x3 = list_1[indx][list_1[indx].Count - 1];
                        gamma_x4 = list_1[indx + 1][list_1[indx].Count - 1];

                        break;
                    }
                }
            }
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

        private void toolStrip_deign_Click(object sender, EventArgs e)
        {
            vdDocument VD = Active_Design_Doc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 1) VD = defDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name.EndsWith("3D_rotate"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name.EndsWith("VTop"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name.EndsWith("VBot"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBottom(VD);
            else if (tsb.Name.EndsWith("VLeft"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VLeft(VD);
            else if (tsb.Name.EndsWith("VRight"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VRight(VD);
            else if (tsb.Name.EndsWith("VFront"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(VD);
            else if (tsb.Name.EndsWith("VBack"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VD);
            else if (tsb.Name.EndsWith("VNE"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINE(VD);
            else if (tsb.Name.EndsWith("VNW"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINW(VD);
            else if (tsb.Name.EndsWith("VSE"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISE(VD);
            else if (tsb.Name.EndsWith("VSW"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISW(VD);
            else if (tsb.Name.EndsWith("ZoomA"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name.EndsWith("ZoomE"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name.EndsWith("ZoomP"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name.EndsWith("ZoomW"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name.EndsWith("ZoomIn"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name.EndsWith("ZoomOut"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name.EndsWith("Pan"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name.EndsWith("ShadeOn"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name.EndsWith("Wire"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name.EndsWith("Save"))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (iApp.Is_StructureDemo)
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
        }

        private void tab_structure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_structure.SelectedTab != tab_Analysis)
            {
                //if (tab_design.SelectedTab == tab_slab)
                //{
                //    if (AST_DOC != null)
                //    {
                //        AST_DOC.Joints.DrawJointsText(Active_Design_Doc, Text_Size);
                //        AST_DOC.Members.DrawMember(Active_Design_Doc, Text_Size);
                //        AST_DOC.Supports.DrawSupport(Active_Design_Doc);
                //        VDRAW.vdCommandAction.View3D_VTop(Active_Design_Doc);
                //    }
                //}
                //if (tab_design.SelectedTab == tab_beam)
                //{
                //    if (AST_DOC != null)
                //    {
                //        AST_DOC.Joints.DrawJointsText(Active_Design_Doc, Text_Size);
                //        AST_DOC.Members.DrawMember(Active_Design_Doc, Text_Size);
                //        AST_DOC.Supports.DrawSupport(Active_Design_Doc);
                //    }
                //}

                if (tc_structure.SelectedTab == tab_foundation)
                {
                    if (tc_footing.SelectedTab == tab_raft_foundation)
                    {
                        if (AST_DOC.Joints.Count > 0)
                            Draw_Floor_Layout();
                    }
                }
                if (tc_structure.SelectedTab == tab_beam)
                {
                    if (tc_Beam.SelectedTab == tab_steel_beam)
                    {
                        if (AST_DOC.Joints.Count > 0)
                        {
                            //chk_beam_steel_individual.Checked = true;
                            sc_beam.Panel2Collapsed = true;
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
                            //chk_column_steel_individual.Checked = true;

                            //sc_beam.Panel2Collapsed = chk_beam_individual.Checked;
                            sc_column.Panel2Collapsed = true;

                            Select_ColumnMembers();
                        }
                    }
                }
                if (tc_structure.SelectedTab == tab_BOQ)
                {
                    BOQ_Button_Enable();
                    //if (tc_footing.SelectedTab == tab_raft_foundation)
                    //{
                    //    if (AST_DOC.Joints.Count > 0)
                    //        Draw_Floor_Layout();
                    //}
                }
                else
                {
                    //Active_Design_Doc.Redraw(true);
                    if (Active_Design_Doc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Joints.DrawJointsText(Active_Design_Doc, Text_Size);
                            AST_DOC_ORG.Members.DrawMember(Active_Design_Doc, Text_Size);
                            AST_DOC_ORG.Supports.DrawSupport(Active_Design_Doc);
                        }
                        else if (AST_DOC != null)
                        {
                            AST_DOC.Joints.DrawJointsText(Active_Design_Doc, Text_Size);
                            AST_DOC.Members.DrawMember(Active_Design_Doc, Text_Size);
                            AST_DOC.Supports.DrawSupport(Active_Design_Doc);
                        }
                        VDRAW.vdCommandAction.View3D_VTop(Active_Design_Doc);
                    }
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
                fboq.TRV = tv_mem_props;
                fboq.ShowDialog();
            }
            //if (fboq.ShowDialog() == DialogResult.Cancel)
            //{
            //    return;
            //}




        }

        private void btn_selected_beams_Click(object sender, EventArgs e)
        {

            vdSelection gripset = GetGripSelection(false);
            //vd3DFace dFace;
            //vdLine ln;

            vdDocument VD = ActiveDoc;

            List<int> members = new List<int>();

            foreach (vdFigure fig in gripset)
            {
                MyList ml = new MyList(fig.ToolTip.ToUpper(), ' ');
                if (ml.StringList[0].StartsWith("MEMBER"))
                {
                    if (!members.Contains(ml.GetInt(3)))
                        members.Add(ml.GetInt(3));
                }
            }

            Button btn = sender as Button;
            //if (btn.Name == btn_column_selected_beams.Name)
            //{
            //    txt_column_H.Text = MyList.Get_Array_Text(members);

            //    if (!txt_column_H.Text.Contains("TO"))
            //        txt_column_H.Text = txt_column_H.Text.Replace(" ", ",");
            //}

        }

        public void Get_Slab_Type()
        {

            MyList ml = new MyList(txt_slab_Lx.Text, ',');
            //MemberIncidence b1 = AST_DOC.Members.Get_Member(MyList.StringToInt(txt_slab_beam1.Text, 0));
            //MemberIncidence b2 = AST_DOC.Members.Get_Member(MyList.StringToInt(txt_slab_beam2.Text, 0));
            //MemberIncidence b3 = AST_DOC.Members.Get_Member(MyList.StringToInt(txt_slab_beam3.Text, 0));
            //MemberIncidence b4 = AST_DOC.Members.Get_Member(MyList.StringToInt(txt_slab_beam4.Text, 0));

            MemberIncidence b1 = AST_DOC.Members.Get_Member(ml.GetInt(0));
            MemberIncidence b2 = AST_DOC.Members.Get_Member(ml.GetInt(1));
            MemberIncidence b3 = AST_DOC.Members.Get_Member(ml.GetInt(2));
            MemberIncidence b4 = AST_DOC.Members.Get_Member(ml.GetInt(3));




            if (b1 == null || b2 == null || b3 == null || b4 == null) return;

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();

            jcc.Add(b1.StartNode);
            //jcc.Add(b1.EndNode);

            if (!jcc.Contains(b2.StartNode))
                jcc.Add(b2.StartNode);
            if (!jcc.Contains(b2.EndNode))
                jcc.Add(b2.EndNode);
            if (!jcc.Contains(b3.StartNode))
                jcc.Add(b3.StartNode);
            if (!jcc.Contains(b3.EndNode))
                jcc.Add(b3.EndNode);
            if (!jcc.Contains(b4.StartNode))
                jcc.Add(b4.StartNode);
            if (!jcc.Contains(b4.EndNode))
                jcc.Add(b4.EndNode);



            List<bool> flags = new List<bool>();

            list.Add(b1);
            list.Add(b2);
            list.Add(b3);
            list.Add(b4);



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
                if (item.EndNode.NodeNo == jcc[2].NodeNo || item.StartNode.NodeNo == jcc[2].NodeNo)
                    mic3.Add(item);
                if (item.EndNode.NodeNo == jcc[3].NodeNo || item.StartNode.NodeNo == jcc[3].NodeNo)
                    mic4.Add(item);

            }

            DirecctionCollection dc1 = new DirecctionCollection();

            Axis_Direction ad = new Axis_Direction();

            int index = 0;
            ad = new Axis_Direction();
            foreach (var item in mic1)
            {
                //ad = new Axis_Direction();

                JointCoordinate jc = item.StartNode;
                if (item.StartNode.NodeNo == jcc[index].NodeNo)
                {
                    jc = item.EndNode;
                }
                ad.JointNo = jcc[index].NodeNo;
                if ((jcc[index].Point.x < jc.Point.x))
                {
                    ad.X_Positive_Member = item;
                    ad.X_Positive = true;
                }
                if ((jcc[index].Point.x > jc.Point.x))
                {
                    ad.X_Negative = true;
                    ad.X_Negative_Member = item;
                }
                if ((jcc[index].Point.y < jc.Point.y))
                {
                    ad.Y_Positive_Member = item;
                    ad.Y_Positive = true;
                }
                if ((jcc[index].Point.y > jc.Point.y))
                {
                    ad.Y_Negative_Member = item;
                    ad.Y_Negative = true;
                }
                if ((jcc[index].Point.z < jc.Point.z))
                {
                    ad.Z_Positive_Member = item;
                    ad.Z_Positive = true;
                }
                if ((jcc[index].Point.z > jc.Point.z))
                {

                    ad.Z_Negative_Member = item;
                    ad.Z_Negative = true;
                }
            }
            dc1.Add(ad);

            index = 1;
            ad = new Axis_Direction();
            foreach (var item in mic2)
            {
                JointCoordinate jc = item.StartNode;
                if (item.StartNode.NodeNo == jcc[index].NodeNo)
                {
                    jc = item.EndNode;
                }
                ad.JointNo = jcc[index].NodeNo;
                if ((jcc[index].Point.x < jc.Point.x)) ad.X_Positive = true;
                if ((jcc[index].Point.x > jc.Point.x)) ad.X_Negative = true;
                if ((jcc[index].Point.y < jc.Point.y)) ad.Y_Positive = true;
                if ((jcc[index].Point.y > jc.Point.y)) ad.Y_Negative = true;
                if ((jcc[index].Point.z < jc.Point.z)) ad.Z_Positive = true;
                if ((jcc[index].Point.z > jc.Point.z)) ad.Z_Negative = true;

            }
            dc1.Add(ad);

            index = 2;
            ad = new Axis_Direction();
            foreach (var item in mic3)
            {
                JointCoordinate jc = item.StartNode;
                if (item.StartNode.NodeNo == jcc[index].NodeNo)
                {
                    jc = item.EndNode;
                }
                ad.JointNo = jcc[index].NodeNo;
                if ((jcc[index].Point.x < jc.Point.x)) ad.X_Positive = true;
                if ((jcc[index].Point.x > jc.Point.x)) ad.X_Negative = true;
                if ((jcc[index].Point.y < jc.Point.y)) ad.Y_Positive = true;
                if ((jcc[index].Point.y > jc.Point.y)) ad.Y_Negative = true;
                if ((jcc[index].Point.z < jc.Point.z)) ad.Z_Positive = true;
                if ((jcc[index].Point.z > jc.Point.z)) ad.Z_Negative = true;

            }
            dc1.Add(ad);


            index = 3;
            ad = new Axis_Direction();
            foreach (var item in mic4)
            {
                JointCoordinate jc = item.StartNode;
                if (item.StartNode.NodeNo == jcc[index].NodeNo)
                {
                    jc = item.EndNode;
                }
                ad.JointNo = jcc[index].NodeNo;
                if ((jcc[index].Point.x < jc.Point.x)) ad.X_Positive = true;
                if ((jcc[index].Point.x > jc.Point.x)) ad.X_Negative = true;
                if ((jcc[index].Point.y < jc.Point.y)) ad.Y_Positive = true;
                if ((jcc[index].Point.y > jc.Point.y)) ad.Y_Negative = true;
                if ((jcc[index].Point.z < jc.Point.z)) ad.Z_Positive = true;
                if ((jcc[index].Point.z > jc.Point.z)) ad.Z_Negative = true;

            }
            dc1.Add(ad);





            bool f1 = false;
            bool f2 = false;
            bool f3 = false;
            bool f4 = false;

            string s1 = "";
            string s2 = "";
            string s3 = "";
            string s4 = "";

            int side = 0;
            index = 0;

            int long_side = 0;
            int short_side = 0;
            if (((!dc1[index].X_Positive && !dc1[index + 1].X_Positive) ||
                (!dc1[index].X_Negative && !dc1[index + 1].X_Negative)) ||

             //((!dc1[index].Y_Positive && !dc1[index + 1].Y_Positive) ||
                //   (!dc1[index].Y_Negative && !dc1[index + 1].Y_Negative)) ||

            ((!dc1[index].Z_Positive && !dc1[index + 1].Z_Positive) ||
                (!dc1[index].Z_Negative && !dc1[index + 1].Z_Negative)))
            {
                side++;
                f1 = true;

                if (b1.Length > b2.Length) long_side++;
                else short_side++;
            }


            index = 1;
            if (((!dc1[index].X_Positive && !dc1[index + 1].X_Positive) ||
                (!dc1[index].X_Negative && !dc1[index + 1].X_Negative)) ||

             //((!dc1[index].Y_Positive && !dc1[index + 1].Y_Positive) ||
                //   (!dc1[index].Y_Negative && !dc1[index + 1].Y_Negative)) ||

            ((!dc1[index].Z_Positive && !dc1[index + 1].Z_Positive) ||
                (!dc1[index].Z_Negative && !dc1[index + 1].Z_Negative)))
            {
                side++;
                f2 = true;

                if (b2.Length > b3.Length) long_side++;
                else short_side++;
            }


            index = 2;
            if (((!dc1[index].X_Positive && !dc1[index + 1].X_Positive) ||
                (!dc1[index].X_Negative && !dc1[index + 1].X_Negative)) ||

             //((!dc1[index].Y_Positive && !dc1[index + 1].Y_Positive) ||
                //   (!dc1[index].Y_Negative && !dc1[index + 1].Y_Negative)) ||

            ((!dc1[index].Z_Positive && !dc1[index + 1].Z_Positive) ||
                (!dc1[index].Z_Negative && !dc1[index + 1].Z_Negative)))
            {
                side++;
                f3 = true;

                if (b3.Length > b4.Length) long_side++;
                else short_side++;
            }

            index = 3;
            if (((!dc1[index].X_Positive && !dc1[0].X_Positive) ||
                (!dc1[index].X_Negative && !dc1[0].X_Negative)) ||

             //((!dc1[index].Y_Positive && !dc1[0].Y_Positive) ||
                //   (!dc1[index].Y_Negative && !dc1[0].Y_Negative)) ||

            ((!dc1[index].Z_Positive && !dc1[0].Z_Positive) ||
                (!dc1[index].Z_Negative && !dc1[0].Z_Negative)))
            {
                side++;
                f4 = true;


                if (b4.Length > b1.Length) long_side++;
                else short_side++;
            }

            switch (side)
            {
                case 0:
                    index = 0;
                    break;
                case 1:
                    if (long_side < short_side) index = 1;
                    else index = 2;
                    break;
                case 2:

                    if (long_side == short_side) index = 3;
                    if (long_side < short_side) index = 4;
                    else index = 5;
                    break;
                case 3:
                    if (long_side < short_side) index = 6;
                    else index = 7;
                    break;
                case 4:
                    index = 8;
                    break;

            }

            cmb_slab_type.SelectedIndex = index;

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
                fcboq.TRV = tv_mem_props;
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
                fcboq.TRV = tv_mem_props;
                fcboq.Add_Staircase_BOQ += new sAdd_Staircase_BOQ(Add_Staircase_BOQ);

                fcboq.col_design = sc;
                //fcboq.ShowDialog();
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
            if (File.Exists(flNm))
            {
                vdDwg.ActiveDocument.Open(flNm);

                VDRAW.vdCommandAction.View3D_VTop(vdDwg.ActiveDocument);
            }

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

            if (File.Exists(flNm))
            {
                vdDwg.ActiveDocument.Open(flNm);

                VDRAW.vdCommandAction.View3D_VTop(vdDwg.ActiveDocument);
                vdDwg.ActiveDocument.ShowUCSAxis = false;
            }
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

            grb_slab_individual.Visible = chk_slab_individual.Checked;
            grb_beam_individual.Visible = chk_beam_individual.Checked;
            grb_column_individual.Visible = chk_column_individual.Checked;
            grb_footing_individual.Visible = chk_footing_individual.Checked;
            grb_pile_individual.Visible = chk_pile_individual.Checked;


            sc_slab.Panel2Collapsed = chk_slab_individual.Checked;
            sc_beam.Panel2Collapsed = chk_beam_individual.Checked;
            sc_column.Panel2Collapsed = chk_column_individual.Checked;
            sc_raft.Panel2Collapsed = chk_raft_individual.Checked;

            this.Refresh();

        }



        //Chiranjit [2015 05 03]
        public void Create_Drawings()
        {

            SlabDesign SD = new SlabDesign();
            BeamDesign BD = new BeamDesign();
            ColumnDesign CD = new ColumnDesign();
            StaircaseDesign SCD = new StaircaseDesign();
            IsolatedFoundationDesign IFD = new IsolatedFoundationDesign();
            PileFoundation PFD = new PileFoundation();
            RaftFoundationDesign RFD = new RaftFoundationDesign();




            SD.Report_File = Slab_Design_Report;
            BD.Report_File = Beam_Design_Report;
            CD.Report_File = Column_Design_Report;
            SCD.Report_File = Staircase_Design_Report;
            IFD.Report_File = Isolate_Footing_BOQ_Report;
            PFD.Report_File = Pile_Design_Report;
            RFD.Report_File = Raft_Design_Report;




            List<double> flv_lvls = AST_DOC.Joints.Get_Floors();

            List<string> beams = new List<string>();

            double flr_level = 0.0;
            string flNm = "";

            StrDwg = new StructureDrawing(AST_DOC, vdDwg.ActiveDocument);

            try
            {
                #region Create Floor Layout Drawings


                if (file_name != "")
                {
                    if (StructureAnalysis == null)
                    {
                        string ana_f = MyStrings.Get_Analysis_Report_File(File_Seismic_Load);

                        if(chk_seismic_coeeficient.Checked && File.Exists(ana_f))
                            StructureAnalysis = new StructureMemberAnalysis(ana_f);
                        else
                            StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }



                    StrDwg.StructureAnalysis = StructureAnalysis;

                    flNm = Path.GetDirectoryName(BD.Get_Report_File(1));

                    flNm = Path.Combine(flNm, "BEAM_DATA.TXT");

                    if (File.Exists(flNm))
                    {
                        StrDwg.Load_All_Beam_Data(flNm);
                    }

                    flNm = Path.GetDirectoryName(CD.Get_Report_File(1));

                    flNm = Path.Combine(flNm, "COLUMN_DATA.TXT");
                    if (File.Exists(flNm))
                    {
                        StrDwg.Load_All_Column_Data(flNm);
                    }


                    StrDwg.Footing_level = flv_lvls[0];

                    for (int i = 0; i < flv_lvls.Count; i++)
                    {

                        flr_level = flv_lvls[i];

                        if (i == 0)
                            flNm = Path.Combine(Drawing_Path_Floor_Layout, "FLOOR_LAYOUT_0+" + flr_level.ToString("f3") + "_M.vdml");
                        else
                            flNm = Path.Combine(Drawing_Path_Floor_Layout, "FLOOR_LAYOUT_" + flr_level.ToString("f3") + "_M.vdml");

                        StrDwg.Footing_B = MyStrings.StringToDouble(txt_ifoot_b.Text, 2.8);
                        StrDwg.Footing_D = MyStrings.StringToDouble(txt_ifoot_l.Text, 2.8);
                        StrDwg.Draw_Floor_Layout(flr_level, flNm);

                    }
                }


                #endregion Create Floor Layout Drawings
            }
            catch (Exception ex1) { }

            try
            {
                #region Member Force Diagram

                foreach (var item in flv_lvls)
                {
                    flr_level = item;
                    beams = StrDwg.Get_BeamNos(flr_level);

                    for (int i = 0; i < beams.Count; i++)
                    {
                        flNm = Path.Combine(Drawing_Path_Force_Diagram(flr_level), "FORCE_DIAGRAM_" + beams[i] +
                            "_LEV_" + flr_level.ToString() + "_M.vdml");

                        StrDwg.Draw_Beam_Moment_Diagram(flr_level, beams[i], flNm);
                        StrDwg.Draw_Beam_Force_Diagram(flr_level, beams[i], flNm, new gPoint(0, -10));
                    }
                }

                #endregion Member Force Diagram
            }
            catch (Exception ex2) { }

            try
            {
                #region Structural Construction Table Drawings

                //BeamDesign BD = new BeamDesign();
                //BD.Report_File = Beam_Design_Report;

                //flNm = Path.Combine(Path.GetDirectoryName(Beam_Design_Report), "temp");

                //flNm = Path.Combine(flNm, "BEAM_DESIGN_SUMMARY.TXT");

                List<string> list = new List<string>();
                string src = Path.GetDirectoryName(BD.Get_Design_Summary_File(1));


                foreach (var item in Directory.GetFiles(src))
                {
                    if (Path.GetFileNameWithoutExtension(item).StartsWith("DESIGN_SUMMARY"))
                    {
                        list.AddRange(File.ReadAllLines(item));
                    }
                }

                File.WriteAllLines(Beam_Design_Summary, list.ToArray());




                flNm = Beam_Design_Summary;
                if (File.Exists(flNm))
                {
                    StrDwg.Load_All_Beam_Construction_Details(flNm);
                }

                StrDwg.Draw_Beam_Constuction_Table(flr_level, Drawing_Beam_Construction_Details);


                list.Clear();
                src = Path.GetDirectoryName(CD.Get_Design_Summary_File(1));


                foreach (var item in Directory.GetFiles(src))
                {
                    if (Path.GetFileNameWithoutExtension(item).StartsWith("DESIGN_SUMMARY"))
                    {
                        list.AddRange(File.ReadAllLines(item));
                    }
                }

                File.WriteAllLines(Column_Design_Summary, list.ToArray());

                flNm = Column_Design_Summary;
                if (File.Exists(flNm))
                {
                    StrDwg.Load_All_Column_Construction_Details(flNm);
                }
                StrDwg.Draw_Column_Constuction_Table(flr_level, Drawing_Column_Construction_Details);


                list.Clear();
                src = Path.GetDirectoryName(IFD.Get_Design_Summary_File(1));

                foreach (var item in Directory.GetFiles(src))
                {
                    if (Path.GetFileNameWithoutExtension(item).StartsWith("DESIGN_SUMMARY"))
                    {
                        list.AddRange(File.ReadAllLines(item));
                    }
                }

                File.WriteAllLines(Isolate_Footing_Design_Summary, list.ToArray());
                //flNm = Path.Combine(flNm, "FOUNDATION_DESIGN_SUMMARY.TXT");
                flNm = Isolate_Footing_Design_Summary;
                if (File.Exists(flNm))
                {
                    StrDwg.Load_All_Foundation_Construction_Details(flNm);
                }
                StrDwg.Draw_Foundation_Constuction_Table(flr_level, Drawing_Foundation_Construction_Details);


                list.Clear();
                //src = Path.GetDirectoryName(SD.Get_Design_Summary_File();

                foreach (var item in flv_lvls)
                {
                    src = SD.Get_Design_Summary_File("", item.ToString("f3"));
                    src = Path.GetDirectoryName(src);

                    foreach (var fn in Directory.GetFiles(src))
                    {
                        if (Path.GetFileNameWithoutExtension(fn).ToUpper().StartsWith("DESIGN_SUMMARY"))
                        {
                            list.Add("");
                            list.Add("");
                            list.Add("");
                            list.AddRange(File.ReadAllLines(fn));
                        }

                    }
                }

                File.WriteAllLines(Slab_Design_Summary, list.ToArray());


                flNm = Slab_Design_Summary;

                double f = MyList.StringToDouble(cmb_dwg_flr_lvl.Text, 0.0);
                //foreach (var item in flv_lvls)
                //{
                //    flr_level = item;
                //    flNm = Path.Combine(flNm, "FLOOR_LEVEL_" + item.ToString("f3").Replace(".", "_"));
                //    flNm = Path.Combine(flNm, "SLAB_DESIGN_SUMMARY.TXT");
                //    if (File.Exists(flNm))
                //    {
                //        StrDwg.Load_All_Slab_Construction_Details(flNm);
                //    }
                //    StrDwg.Draw_SLab_Constuction_Table(flr_level, Drawing_Slab_Construction_Details);

                //}

                //flNm = Path.Combine(flNm, "SLAB_DESIGN_SUMMARY.TXT");
                if (File.Exists(flNm))
                {
                    StrDwg.Load_All_Slab_Construction_Details(flNm);
                }
                StrDwg.Draw_SLab_Constuction_Table(flr_level, Drawing_Slab_Construction_Details);
                if (flv_lvls.Count == 0)
                {
                    flNm = Path.Combine(flNm, "SLAB_DESIGN_SUMMARY.TXT");
                    if (File.Exists(flNm))
                    {
                        StrDwg.Load_All_Slab_Construction_Details(flNm);
                    }
                    StrDwg.Draw_SLab_Constuction_Table(flr_level, Drawing_Slab_Construction_Details);
                }

                #endregion Structural Construction Table Drawings
            }
            catch (Exception ex3) { }

            try
            {
                #region Typical Structural Detail Drawings

                string Dwg_Folder = Drawing_Path_Typical_Slab_Structural_Details;

                string src_fld = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Typical Structural Detail Drawings");

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
            }
            catch (Exception ex4) { }

            vdDwg.ActiveDocument.ActiveLayOut.Entities.RemoveAll();

            vdDwg.ActiveDocument.Redraw(true);
            MessageBox.Show(this, "All Drawings are created in folder \"" + Drawing_Path + "\"", "ASTRA", MessageBoxButtons.OK);
        }

        private void btn_drawing_create_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_drawing_create.Name)
            {
                grb_dwg_open.Enabled = true;

                Create_Drawings();
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


            GetGripSelection(false).RemoveAll();

        }

        void Raft_BaseControl_vdMouseUp(MouseEventArgs e, ref bool cancel)
        {
            try
            {
                vdSelection gripset = GetGripSelection(false);
                vd3DFace dFace;
                vdLine ln;

                vdDocument VD = ActiveDoc;
                string kStr = "";
                foreach (vdFigure fig in gripset)
                {
                    if (fig is vdMText)
                    {
                        if (fig.ToolTip.StartsWith("C"))
                        {
                            kStr += fig.ToolTip + ",";
                        }
                    }
                }
                txt_raft_cols.Text = kStr;
                //gripset.RemoveAll();
            }
            catch (Exception exx) { }
        }

        public void Draw_Floor_Layout()
        {
            if (file_name == "") return;

            double flr_lvl = AST_DOC.Joints.Get_Floors()[0];

            //, string file_name



            MemberIncidenceCollection beams = new MemberIncidenceCollection();
            MemberIncidenceCollection columns = new MemberIncidenceCollection();

            if (astDoc == null)
                astDoc = AST_DOC;
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = astDoc.Members[i];

                //if (item.StartNode.Y == flr_lvl || item.EndNode.Y == flr_lvl)
                if (item.EndNode.Y == flr_lvl)
                {
                    if (item.StartNode.Y == item.EndNode.Y)
                    {
                        beams.Add(item);
                    }
                    else
                    {
                        columns.Add(item);
                    }
                }
            }


            vdLayer concLay = raftDoc.Layers.FindName("Concrete");
            if (concLay == null)
            {
                concLay = new vdLayer(raftDoc, "Concrete");
                //concLay.setDocumentDefaults(raftDoc);
                //concLay.SetUnRegisterDocument();
                concLay.PenColor = new vdColor(Color.Cyan);

                raftDoc.Layers.Add(concLay);
            }



            vdLine ln = new vdLine();

            raftDoc.ActiveLayOut.Entities.RemoveAll();


            vdMText vtxt = null;
            vdCircle vcle = null;
            vdLine ln2 = null;
            vdLine conln = null;


            double max_z = astDoc.Joints.Max_Z_Positive;
            foreach (var item in beams)
            {
                ln = new vdLine();

                ln.SetUnRegisterDocument(raftDoc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = item.StartNode.X;
                ln.StartPoint.y = max_z - item.StartNode.Z;

                ln.EndPoint.x = item.EndNode.X;
                ln.EndPoint.y = max_z - item.EndNode.Z;
                ln.ToolTip = "";
                raftDoc.ActiveLayOut.Entities.Add(ln);

                #region Chiranjit [2015 05 08]

                //#region Draw Concrete Line 1

                //conln = new vdLine();

                //conln.SetUnRegisterDocument(raftDoc);
                //conln.setDocumentDefaults();


                //conln.StartPoint.x = ln.StartPoint.x;
                //conln.StartPoint.y = ln.StartPoint.y;


                //conln.EndPoint.x = ln.EndPoint.x;
                //conln.EndPoint.y = ln.EndPoint.y;


                //if (item.Direction == eDirection.X_Direction)
                //{
                //    conln.StartPoint.y = ln.StartPoint.y + item.Property.YD / 2;
                //    conln.EndPoint.y = ln.EndPoint.y + item.Property.YD / 2;

                //}
                //else if (item.Direction == eDirection.Z_Direction)
                //{
                //    conln.StartPoint.x = ln.StartPoint.x + item.Property.YD / 2;
                //    conln.EndPoint.x = ln.EndPoint.x + item.Property.YD / 2;

                //}
                //conln.Layer = concLay;
                //raftDoc.ActiveLayOut.Entities.Add(conln);

                //#endregion Draw Concrete Line


                //#region Draw Concrete Line 2

                //conln = new vdLine();

                //conln.SetUnRegisterDocument(raftDoc);
                //conln.setDocumentDefaults();


                //conln.StartPoint.x = ln.StartPoint.x;
                //conln.StartPoint.y = ln.StartPoint.y;


                //conln.EndPoint.x = ln.EndPoint.x;
                //conln.EndPoint.y = ln.EndPoint.y;


                //if (item.Direction == eDirection.X_Direction)
                //{
                //    //conln.StartPoint.y = ln.StartPoint.y - item.Property.ZD / 2;
                //    //conln.EndPoint.y = ln.EndPoint.y - item.Property.ZD / 2;

                //    conln.StartPoint.y = ln.StartPoint.y - item.Property.YD / 2;
                //    conln.EndPoint.y = ln.EndPoint.y - item.Property.YD / 2;

                //}
                //else if (item.Direction == eDirection.Z_Direction)
                //{
                //    //conln.StartPoint.x = ln.StartPoint.x - item.Property.ZD / 2;
                //    //conln.EndPoint.x = ln.EndPoint.x - item.Property.ZD / 2;

                //    conln.StartPoint.x = ln.StartPoint.x - item.Property.YD / 2;
                //    conln.EndPoint.x = ln.EndPoint.x - item.Property.YD / 2;

                //}

                //conln.Layer = concLay;
                //raftDoc.ActiveLayOut.Entities.Add(conln);

                //#endregion Draw Concrete Line 2

                #endregion Chiranjit [2015 05 08]

                vtxt = new vdMText();

                vtxt.SetUnRegisterDocument(raftDoc);
                vtxt.setDocumentDefaults();

                vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                vtxt.InsertionPoint = (ln.StartPoint + ln.EndPoint) / 2;

                if (item.Direction == eDirection.X_Direction)
                {
                    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                }
                else
                {
                    vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorLeft;
                    //vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                    vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + 0.5;
                }

                vtxt.Height = 0.2;


                vtxt.TextString = item.Length.ToString("f3");
                //vtxt.TextString = Get_Beam_Data(item.MemberNo);



                //ln.ToolTip = vtxt.TextString;

                raftDoc.ActiveLayOut.Entities.Add(vtxt);



                vcle = new vdCircle();

                vcle.SetUnRegisterDocument(raftDoc);
                vcle.setDocumentDefaults();

                vcle.Center = vtxt.InsertionPoint;
                vcle.Radius = 0.2;

                //raftDoc.ActiveLayOut.Entities.Add(vcle);



                ln2 = new vdLine();

                ln2.SetUnRegisterDocument(raftDoc);
                ln2.setDocumentDefaults();


                ln2.StartPoint = (ln.StartPoint + ln.EndPoint) / 2;
                ln2.EndPoint = vtxt.InsertionPoint;
                //ln2.StartPoint = (ln.StartPoint + ln.EndPoint) / 2;

                if (item.Direction == eDirection.X_Direction)
                    ln2.EndPoint.y = ln2.EndPoint.y - vcle.Radius;

                else
                    ln2.EndPoint.x = ln2.EndPoint.x - vcle.Radius;


                raftDoc.ActiveLayOut.Entities.Add(ln2);



            }

            vdPolyline pln = null;

            foreach (var item in columns)
            {
                pln = new vdPolyline();

                pln.SetUnRegisterDocument(raftDoc);
                pln.setDocumentDefaults();

                pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, max_z - (item.StartNode.Z + item.Property.ZD / 2)));
                pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 2, max_z - (item.StartNode.Z + item.Property.ZD / 2)));
                pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 2, max_z - (item.StartNode.Z - item.Property.ZD / 2)));
                pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, max_z - (item.StartNode.Z - item.Property.ZD / 2)));
                pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, max_z - (item.StartNode.Z + item.Property.ZD / 2)));

                raftDoc.ActiveLayOut.Entities.Add(pln);


                vtxt = new vdMText();

                vtxt.SetUnRegisterDocument(raftDoc);
                vtxt.setDocumentDefaults();

                //vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                //vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;

                //vtxt.InsertionPoint = new gPoint(item.StartNode.X, max_z - item.StartNode.Z);
                vtxt.InsertionPoint = pln.VertexList[1];


                //if (item.Direction == eDirection.X_Direction)
                //    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                //else
                //vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + item.Property.YD / 2;

                //vtxt.Height = 0.15;
                vtxt.Height = item.Property.YD / 2;


                vtxt.TextString = "C" + item.MemberNo;
                vtxt.ToolTip = vtxt.TextString;
                //vtxt.TextString = Get_Column_Data(item.MemberNo);

                raftDoc.ActiveLayOut.Entities.Add(vtxt);
            }

            raftDoc.Palette.Background = Color.Black;
            raftDoc.Redraw(true);

            raftDoc.ShowUCSAxis = false;
            VDRAW.vdCommandAction.View3D_VTop(raftDoc);

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
            Draw_Floor_Layout();

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


            if (chk_slab_individual.Checked)
            {
                floors.Clear();
                floors.Add(MyList.StringToDouble(txt_slab_flv_lvl.Text, 0.0));
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

        private void chk_seismic_coeeficient_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_seismic_coeeficient.Checked)
            {
                txt_seismic_coeeficient.Enabled = true;
                txt_seismic_coeeficient.Text = "0.12";
            }
            else
            {
                txt_seismic_coeeficient.Enabled = false;
                txt_seismic_coeeficient.Text = "0.0";
            }
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

            if (StrDwg == null)
                StrDwg = new StructureDrawing(AST_DOC, diagDoc);



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

            cmb_diag_flr_lvl.Items.Clear();

            foreach (var item in beamsLvls)
            {
                cmb_diag_flr_lvl.Items.Add(item);
            }
            if (cmb_diag_flr_lvl.Items.Count > 0)
                cmb_diag_flr_lvl.SelectedIndex = 0;
            if (cmb_diag_beam_no.Items.Count > 0)
                cmb_diag_beam_no.SelectedIndex = 0;



        }
        private void cmb_diag_beam_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            double flv = MyList.StringToDouble(cmb_diag_flr_lvl.Text, 0.0);
            StrDwg.doc = diagDoc;
            StrDwg.StructureAnalysis = StructureAnalysis;

            foreach (var item in beamsNos)
            {
                if (item.FloorLavel == flv && item.BeamNo == cmb_diag_beam_no.Text)
                {
                    StrDwg.All_Beam_Data = beamsNos;
                    StrDwg.Draw_Beam_Moment_Diagram(flv, item.BeamNo, "");
                    StrDwg.Draw_Beam_Force_Diagram(flv, item.BeamNo, "", new gPoint(0, -10));
                    break;
                }
            }
            diagDoc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(diagDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(diagDoc);
        }

        private void cmb_diag_flr_lvl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndx = cmb_diag_beam_no.SelectedIndex;


            double d = MyList.StringToDouble(cmb_diag_flr_lvl.Text, 0.0);

            cmb_diag_beam_no.Items.Clear();
            foreach (var item in beamsNos)
            {
                if (item.FloorLavel == d)
                    cmb_diag_beam_no.Items.Add(item.BeamNo);
            }
            if (cmb_diag_beam_no.Items.Count > selIndx)
                cmb_diag_beam_no.SelectedIndex = selIndx;


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
                fboq.TRV = tv_mem_props;
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
                fcboq.TRV = tv_mem_props;
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

    }



    public class SteelColumnDesign
    {
        IApplication iApp;
        public double l, a, P, M, V, e, Pms, fy, fs, fb, Pcs, Ps;

        public double n, tb, Dr, Nr;
        public double A, h, Bf, tw, Ixx, Iyy, rxx, ryy, rxx_comp;
        public string Section_Name = "ISMB 300";
        public string ColumnNo = "C1";
        public SteelColumnDesign(IApplication iapp)
        {
            iApp = iapp;


            l = a = P = M = V = e = Pms = fy = fs = fb = Pcs = Ps = 0.0;

            l = 4.0;
            a = 300;
            P = 1350;
            M = 50;
            V = 40;
            e = 100;
            Pms = 165;
            fy = 250;
            fs = 100;
            Pcs = 120;
            Ps = 150;

            n = 2;
            tb = 6.0;
            Dr = 20.0;
            Nr = 6.0;


            A = 5626;
            h = 300;
            Bf = 140;
            tw = 8.8;
            Ixx = 8603.6;
            Iyy = 453.9;
            rxx = 123.7;
            ryy = 28.4;
        }
        public List<string> Design_Individual_Program()
        {
            List<string> list = new List<string>();
            IS_DESIGN_OK = true;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Steel Column"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User’s Input data:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Column = l = {0:f2} m.", l));
            list.Add(string.Format("Bearing at each end = a = {0:f2} mm.", a));
            list.Add(string.Format("Maximum Compressive Load = P = {0:f3} kN", P));
            list.Add(string.Format("(The load acts on the y-y axis)"));
            list.Add(string.Format("Maximum Bending Moment  = M = {0:f3} kN-m", M));
            list.Add(string.Format("Maximum Shear Force = V = {0:f3} kN", V));
            list.Add(string.Format("Eccentricity of Vertical Load = e = {0:f2} mm", e));
            list.Add(string.Format("Permissible Bending Stress = Pms = {0:f2} N/Sq.mm", Pms));
            list.Add(string.Format("Permissible Stress = fy = {0} N/Sq.mm", fy));
            list.Add(string.Format("Permissible stress of a rivet in shear  = fs = {0} N/Sq.mm", fs));
            list.Add(string.Format("Permissible stress of a rivet in bearing  = fb = {0} N/Sq.mm", fb));
            list.Add(string.Format("Permissible Compressive Stress = Pcs = {0} N/Sq.mm", Pcs));
            list.Add(string.Format("Safe compressive stress for splice plate = Ps = {0} N/Sq.mm", Ps));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Steel Section = n = {0}", n));
            list.Add(string.Format("Minimum thickness of Batten Plates = tb = {0} mm thick", tb));
            list.Add(string.Format("Diameter of Rivets = Dr = {0} mm", Dr));
            list.Add(string.Format("Number of rivets for connecting Battens to each flange = Nr = {0}", Nr));
            list.Add(string.Format(""));
            //list.Add(string.Format("Selected Steel Section = ISMB 300 (Selected from Combo) "));
            list.Add(string.Format("Selected Steel Section = {0} ", Section_Name));
            list.Add(string.Format(""));


            list.Add(string.Format("A = {0} Sq.mm", A));
            list.Add(string.Format("h = {0} mm", h));
            list.Add(string.Format("Bf = {0} mm", Bf));
            list.Add(string.Format("tw = {0} mm", tw));
            list.Add(string.Format("Ixx = {0} x 10000 Sq. Sq. mm,", Ixx / 10000));
            list.Add(string.Format("Iyy = {0} x 10000 Sq. Sq. mm,", Iyy / 10000));
            list.Add(string.Format("rxx = {0} mm, ryy = {1} mm,", rxx, ryy));
            list.Add(string.Format("for combined section rxx = {0} mm", rxx));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Design Calculations:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Section Area"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double req_val = 0.0;
            double prv_val = 0.0;

            req_val = P * 1000 / Pcs;
            prv_val = A * n;

            list.Add(string.Format("Required Section Area = P / Pcs = {0:f2} x 1000 / {1:f2} = {2:f2} Sq.mm", P, Pcs, req_val));
            list.Add(string.Format(""));
            if (prv_val > req_val)
                list.Add(string.Format("Provided Section Area = A x n = {0:f2} x {1} = {2:f2} Sq.mm > {3:f2} Sq.mm Hence OK.", A, n, prv_val, req_val));
            else if (prv_val < req_val)
            {
                IS_DESIGN_OK = false;
                list.Add(string.Format("Provided Section Area = A x n = {0:f2} x {1} = {2:f2} Sq.mm < {3:f2} Sq.mm Hence NOT OK.", A, n, prv_val, req_val));
            }
            else
                list.Add(string.Format("Provided Section Area = A x n = {0:f2} x {1} = {2:f2} Sq.mm = {3:f2} Sq.mm Hence OK.", A, n, prv_val, req_val));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : Slenderness Ratio"));
            list.Add(string.Format("--------------------------"));
            list.Add(string.Format(""));

            double le = 0.8 * l * 1000;
            list.Add(string.Format("Effective Length of Column = le = 0.8 x l x 1000 = 0.8 x {0:f3} x 1000 = {1:f3} mm", l, le));


            double le10 = le * 1.10;
            list.Add(string.Format("For battened column the effective length is increased by 10% = le = 1.10 x {0:f3} = {1:f3} mm.", le, le10));
            list.Add(string.Format(""));

            double lamda = le10 / rxx;

            list.Add(string.Format("Slenderness Ratio based on rxx = lamda = le / rxx = {0:f3} / {1:f3} = {2:f3}", le10, rxx, lamda));


            string kStr = "";
            //double Sigma_ac = iApp.Tables.Permissible_Bending_Compressive_Stresses(h / tw, lamda, ref kStr);
            double Sigma_ac = iApp.Tables.Permissible_Bending_Compressive_Stresses(h / tw, lamda, ref kStr);
            //double Sigma_ac = 146;

            list.Add(string.Format("Allowable Compressive Stress corresponding to this Slenderness Ratio = Sigma_ac = {0:f3} N/Sq.mm", Sigma_ac));
            list.Add(string.Format(""));

            double comp_strs = (P * 1000 / (2 * A));
            list.Add(string.Format("Actual Compressive stress induced = P x 1000 / (2 x A)"));
            list.Add(string.Format("                                  = {0:f3} x 1000 / (2 x  {1:f3}) ", P, A));

            if (Sigma_ac > comp_strs)
                list.Add(string.Format("                                  = {0:f0} N/Sq. mm < {1:f0} N/Sq.mm, Hence, OK.", comp_strs, Sigma_ac));
            else
            {
                IS_DESIGN_OK = false;
                list.Add(string.Format("                                  = {0:f0} N/Sq. mm > {1:f0} N/Sq.mm, Hence, NOT OK.", comp_strs, Sigma_ac));
            }
          
            list.Add(string.Format(""));


            list.Add(string.Format("The spacing between the two sections of the column is so chosen,"));
            list.Add(string.Format("that the radii of gyration about X & Y axes are equal."));
            list.Add(string.Format(""));

            double spacing = 2 * Math.Sqrt(Math.Pow(rxx, 2) - Math.Pow(ryy, 2));
            list.Add(string.Format("Therefore, spacing = 2 x SQRT(rxx^2 - ryy^2) = 2 x SQRT({0:f3}^2 - {1:f3}^2) = {2:f3} sq.mm", rxx, ryy, spacing));
            list.Add(string.Format(""));

            //double s = Math.Round(spacing, 0, MidpointRounding.AwayFromZero);

            double s = (int)spacing;

            //s = 245;
            //double ss = 
            //list.Add(string.Format("Let us provide spacing of 245 mm between the centres of the two sections,"));

            list.Add(string.Format("Let us provide spacing of {0} mm between the centres of the two sections,", s));

            list.Add(string.Format("Figure 1 shows the position of the two I-sections."));
            //list.Add(string.Format("Therefore, for the composite section, therefore, ‘s’ = 245 mm."));
            list.Add(string.Format("Therefore, for the composite section, therefore, ‘s’ = {0} mm.", s));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Figure 1 (4.29) shown the position of the two I-sections."));
            list.Add(string.Format(""));
            list.Add(string.Format("For the composite section, "));

            double Iyy_dash = (n * (Iyy + A * rxx * rxx));
            list.Add(string.Format(" Iyy’ = {0} x (Iyy + A x rxx^2)", n));
            //list.Add(string.Format("      = 2 x (453.9 x 10^4 + 5,626 x 123.7^2)"));
            //list.Add(string.Format("      = 17,792.8 x 10^4 mm4"));
            list.Add(string.Format("      = {0} x ({1} x 10^4 + {2} x {3}^2)", n, Iyy/10000, A, rxx));
            list.Add(string.Format("      = {0:f0} x 10^4 mm^4", Iyy_dash/10000));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Ixx_dash = (n * Ixx);

            list.Add(string.Format("But   Ixx’ = {0} x Ixx", n));


            list.Add(string.Format("           = {0} x {1:f0} x 10^4", n, Ixx / 10000));
            //list.Add(string.Format("           = 17,207.2 x 10^4 mm4"));
            list.Add(string.Format("           = {0:f0} x 10^4 mm4", Ixx_dash / 10000));
            list.Add(string.Format(""));

            if (Ixx_dash < Iyy_dash)
            {
                list.Add(string.Format("So, for combined section   Ixx’ < Iyy’"));
                list.Add(string.Format(""));
                list.Add(string.Format("Therefore, rxx’ < ryy’ Hence, the slenderness ratio adopted is satisfactory."));
            }
            else
            {
                list.Add(string.Format("So, for combined section   Ixx’ > Iyy’"));
                list.Add(string.Format(""));
                list.Add(string.Format("Therefore, rxx’ > ryy’ Hence, the slenderness ratio adopted is satisfactory."));
       
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("STEP 3:  Design of Battens"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));

            //list.Add(string.Format("Figure 2 (4.30)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Spacing of Battens:"));
            list.Add(string.Format(""));



            //double slender_ratio = d / ryy
            list.Add(string.Format("Let the centre to centre spacing of battens be 'd' mm"));
            list.Add(string.Format("Slenderness ratio of one I- section = d / ryy    of one I section = d / {0:f3}", ryy));



            list.Add(string.Format("The ratio of one I- section between centres of battens shall not exceed the following:"));
            list.Add(string.Format("The slenderness ratio of one I- section between centres of battens shall not exceed the following:"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i) 50"));
            list.Add(string.Format("(ii) 0.7 x lamda (the slenderness ratio of the member as a whole about the X-X axis)."));
            list.Add(string.Format(""));

            double d_1 = 50 * ryy;
            double d_2 = 0.7 * lamda * ryy;

            list.Add(string.Format("From (i)  d / {0:f3} = 50                 d = {1:f3} mm", ryy, d_1));
            list.Add(string.Format("From (ii) d / {0:f3} = 0.7 x {1:f3}         d = {2:f3} mm", ryy, lamda, d_2));
            list.Add(string.Format(""));

            double d = Math.Min(d_1, d_2);

            d = Math.Round(d, 1);

            //list.Add(string.Format("Taking the lower value, provide a spacing         ‘d’ = 560 mm"));
            list.Add(string.Format("Taking the lower value, provide a spacing      ‘d’ = {0:f3} mm", d));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Depth of the Battens:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));

            double d1_1 = s;
            double d1_2 = 2 * Bf;
            list.Add(string.Format("(a)  End Battens. The effective depth of the end battens   ‘d1’   shall be "));
            list.Add(string.Format("(i)  Not less than the spacing of the two I - sections i.e. not less than   ‘s’ = {0} mm", d1_1));
            list.Add(string.Format("(ii) Not less than twice the width of one I - section i.e. not less than 2 x (Bf = {0}) = {1} mm.", Bf, d1_2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double d1 = Math.Max(d1_1, d1_2);
            list.Add(string.Format("The effective depth is the distance between the extreme rivets in the vertical row."));
            list.Add(string.Format("By taking the higher value, effective depth = {0} mm. ", d1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _d = 20;
            double _c = 30;
            list.Add(string.Format("By using   ‘d’ = {0} mm diameter rivets and providing an effective margin of   ‘c’ = {1} mm. ", _d, _c));
            list.Add(string.Format(""));
            double Db = d1 + 2 * _c;
            //list.Add(string.Format("The total depth of the battens = ‘Db’ = 280 + 2 x 30 = 340 mm"));
            list.Add(string.Format("The total depth of the battens = ‘Db’ = {0} + 2 x {1} = {2} mm", d1, _c, Db));
            list.Add(string.Format(""));
            list.Add(string.Format("(b)  Intermediate battens. The effective depth of an intermediate batten  ‘d1’  shall be "));
            list.Add(string.Format("(i)  Not less than 3/4 x s (spacing of the I - sections) "));

            d1_1 = (3.0 / 4.0) * s;
            //d1_2 = (3.0 / 4.0) * 245;
            //list.Add(string.Format("i.e. Not less than = 3/4 x 245 = 183.75 mm"));
            list.Add(string.Format("i.e. Not less than = 3/4 x {0} = {1:f2} mm", s, d1_1));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Not less than 2 x Bf (the width of one I - section) "));
            list.Add(string.Format("i.e. Not less than = 2 x {0} = {1} mm", Bf, d1_2));

            double eff_dep = Math.Max(d1_1, d1_2);
            list.Add(string.Format("Hence, for the intermediate battens, by taking the higher value, effective depth = {0} mm.", eff_dep));
            list.Add(string.Format(""));

            list.Add(string.Format("Width of Battens :"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));

            double B = s + Bf;
            list.Add(string.Format("This is made equal to the total width of the whole column along the plane of battens,"));
            //list.Add(string.Format("                        = B = 245 + 2 x 70  = 385 mm"));
            list.Add(string.Format("                        = B = {0} + 2 x {1}  = {2} mm", s, Bf, B));
            list.Add(string.Format(""));

            list.Add(string.Format("Thickness of Battens :"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));

            double Vh = 2.5 * P / 100.0;
            list.Add(string.Format("Horizontal shear = Vh = 2.5% of P (the column load)"));
            //list.Add(string.Format("                      = 2.5 x 1350 /100 = 33.75 kN"));
            list.Add(string.Format("                      = 2.5 x {0} /100 = {1:f3} kN", P, Vh));
            list.Add(string.Format(""));


            double allow_shr_strss = 100.0;
            //double allow_shr_strss = iApp.Tables.all;
            //list.Add(string.Format("Allowing a shear stress of 100 N/ Sq. mm"));
            list.Add(string.Format("Allowing a shear stress of {0} N/ Sq. mm", allow_shr_strss));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Va = (Vh / 2) * 1000 / 100.0;
            list.Add(string.Format("Horizontal shear area per plate = Va"));
            //list.Add(string.Format("                                = (Vh / 2) x 1,000 / 100 Sq. mm"));
            //list.Add(string.Format("                                = 16.875 x 1,000 / 100 Sq. mm"));
            //list.Add(string.Format("                                = 168.75  Sq. mm"));
            list.Add(string.Format("                                = (Vh / 2) x 1,000 / 100 Sq. mm"));
            list.Add(string.Format("                                = {0:f3} x 1,000 / 100 Sq. mm", Vh));
            list.Add(string.Format("                                = {0:f3}  Sq. mm", Va));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            tb = Va / B;
            //list.Add(string.Format("Thickness of battens plate = tb = Va / B = 168.75 / 385 = 0.44 mm"));
            list.Add(string.Format("Thickness of battens plate = tb = Va / B = {0:f3} / {1:f3} = {2:f3} mm", Va, B, tb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Minimum thickness of the batten plate"));
            list.Add(string.Format(""));
            list.Add(string.Format("                         = 1 / 50 x distance between innermost line of rivets"));

            double tb_min = 1.0 / 50.0 * (s - _c - _c);
            list.Add(string.Format(" = 1 / 50 x [{0} - {1} - {1}] = {2} mm", s, _c, tb_min));
            list.Add(string.Format(""));

            //double = 
            if (tb_min < 6)
            {
                list.Add(string.Format("Thickness of the batten plate shall not be less than = tb_min = 6 mm"));
                tb_min = 6;
            }
            list.Add(string.Format(""));
            tb = tb_min;
            list.Add(string.Format("Provide batten plates of thickness = tb = tb_min = {0} mm.", tb));
            list.Add(string.Format(""));
            list.Add(string.Format("Next, we shall check the batten plates for longitudinal shear and bending moment."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check for longitudinal shear : "));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double F1 = Vh * 1000 * d / (n * s);
            list.Add(string.Format("Longitudinal shear per batten plate = F1 = S x d / (n x s)"));
            list.Add(string.Format("                                         = Vh x 1000 x d / (n x s)"));
            //list.Add(string.Format("                                         = 33.75 x 1,000 x 560 / (2 x 245)"));
            list.Add(string.Format("                                         = {0:f3} x 1,000 x {1} / ({2} x {3})", Vh, d, n, s));
            list.Add(string.Format("                                         = {0:f3} N", F1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Vstrs = F1 / (Db * tb);
            list.Add(string.Format("Vertical shear stress induced = F1 / (Db x tb) "));
            list.Add(string.Format("                              = {0:f3} / ({1}  x {2} )", F1, Db, tb));
            if (Vstrs < allow_shr_strss)
                list.Add(string.Format("                              = {0:f3} N / Sq. mm (less than {1} N/mm^2)", Vstrs, allow_shr_strss));
            else
                list.Add(string.Format("                              = {0:f3} N / Sq. mm (greater than {1} N/mm^2)", Vstrs, allow_shr_strss));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check  for bending moment :"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));

           double  _M = Vh * 1000 * d / (2 * n);
            list.Add(string.Format("B.M. per batten plate = M = S x d / (2 x n)"));
            list.Add(string.Format("                          = Vh x 1000 x d / (2 x n) "));
            list.Add(string.Format("                          = {0:f3} x 1,000 x {1} / 2 x {2}  Nmm", Vh, d, n));
            list.Add(string.Format("                          = {0:f3} N-mm", _M));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Let  ‘f’ be the bending stress induced"));
            list.Add(string.Format("Equating the moment of resistance to the bending moment,"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("M’ = (1 / 6) x f x tb x Db^2 = (1 / 6) x f x 6 x 340^2"));



            list.Add(string.Format(""));
            //list.Add(string.Format("Therefore, M’ = M,    i.e, (1 / 6) x f x 6 x 340^2 = 47,25,000"));
            list.Add(string.Format("Therefore, M’ = M,    i.e, (1 / 6) x f x {0} x {1}^2 = {2}", tb, Db, _M));
            list.Add(string.Format(""));

            double f = _M * 6 / (tb * Db * Db);


            //double M_dash = (1./6.0)*

            //list.Add(string.Format("f  = 47,25,000 x 6 / (6 x 340^2) = 40.9 N / Sq.mm (less than 165 N / mm)"));

            if (f < Pms)
            {
                list.Add(string.Format("f  = {0} x 6 / ({1} x {2}^2) = {3:f3} N / Sq.mm (less than {4} N / mm)", _M, tb, Db, f, Pms));
            }
            else
            {
                list.Add(string.Format("f  = {0} x 6 / ({1} x {2}^2) = {3:f3} N / Sq.mm (greater than {4} N / mm)", _M, tb, Db, f, Pms));
            }
            list.Add(string.Format(""));


            list.Add(string.Format("Connection of the battens to the I - sections"));
            list.Add(string.Format(""));

            //list.Add(string.Format("Longitudinal shear = F1 = 38,571 N "));
            //list.Add(string.Format("            Moment = M = 47,25,000 Nmm"));
            list.Add(string.Format("Longitudinal shear = F1 = {0:f3} N ", F1));
            //list.Add(string.Format("            Moment = M = 47,25,000 Nmm", M));
            list.Add(string.Format("            Moment = M = {0:f3} N-mm", _M));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            //list.Add(string.Format("Provide Nr = 6 rivets for connecting to each flange.", Nr));
            list.Add(string.Format("Provide Nr = {0} rivets for connecting to each flange.", Nr));
            list.Add(string.Format(""));


            double thk1 = 40;
            //Ss = Nr * (40 * 40) + 4 * (140 * 140);
            double Ss = Nr * (thk1 * thk1) + 4 * (Bf * Bf);
            //list.Add(string.Format("Σx^2  + Σy^2 with respect to G,  Ss = 6 x 40^2 + 4 x 140^2 = 88,000 mm^2"));
            list.Add(string.Format("Σx^2  + Σy^2 with respect to G,  Ss = {0} x {1}^2 + 4 x {2}^2 = {3:f2} mm^2", Nr, thk1, Bf, Ss));
            list.Add(string.Format(""));
            //list.Add(string.Format(" K = M / Ss = 47,25,000 / 88,000 N/mm"));
            //list.Add(string.Format("   = 53.69 N/mm"));
            double K = _M / Ss;
            list.Add(string.Format(" K = M / Ss = {0:f3} / {1:f3} N/mm", _M, Ss));
            list.Add(string.Format("            = {0:f3} N/mm", K));


            list.Add(string.Format(""));

            list.Add(string.Format("Consider the rivet marked A,"));

            double Rt = F1 / Nr;
            //list.Add(string.Format("Resistance against translation = Rt = F1 / Nr = 38,571 / 6 = 6,428.5 N"));
            list.Add(string.Format("Resistance against translation = Rt = F1 / Nr = {0:f3} / {1} = {2:f3} N", F1, Nr, Rt));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Resistance against rotation (torsional shear) = Sa "));
            list.Add(string.Format("                                              = K x ra"));
            list.Add(string.Format("                                              = {0:f3} x ra", K));
            list.Add(string.Format(""));

            list.Add(string.Format("Total Vertical component = V = Rt + K x (ra x sin θ)"));
            //list.Add(string.Format("                             = 6,428.5 + 53.69 x (ra x sin θ)"));
            list.Add(string.Format("                             = {0:f3} + {1:f3} x (ra x sin θ)", Rt, K));
            list.Add(string.Format(""));
            //V = Rt + K * (ra * sin θ)
            double  _V = Rt + K * thk1;
            //list.Add(string.Format("                             = {0:f3} + 53.69 x 40 = 8,576.1 N"));
            list.Add(string.Format("                             = {0:f3} + {1:f3} x {2:f0} = {3:f3} N", Rt, K, thk1, _V));
            list.Add(string.Format(""));
            //H = K * (ra * cos θ)
            double H = (K * Bf);
            list.Add(string.Format("Horizontal component = H = K x (ra x cos θ)"));
            list.Add(string.Format("                         = {0:f3} x (ra x cos θ)", K));
            list.Add(string.Format("                         = {0:f3} x {1:f0}", K, Bf));
            list.Add(string.Format("                         = {0:f3} N", H));
            list.Add(string.Format(""));


            A = Math.Sqrt(_V * _V + H * H);
            list.Add(string.Format("Resultant resistance offered by the rivet A, = SQRT(V^2 + H^2)"));
            list.Add(string.Format("                                             = SQRT ({0:f3}^2 + {1:f3}^2)", _V, H));
            list.Add(string.Format("                                             = {0:f3} N", A));

            double Dr1 = Dr + 1.5;

            //list.Add(string.Format("Diameter of rivets = Dr = 20 mm,  Therefore, Dr1 = 20 + 1.5 = 21.5 mm"));
            list.Add(string.Format("Diameter of rivets = Dr = {0} mm,  Therefore, Dr1 = {0} + 1.5 = {1} mm", Dr, Dr1));

            double stngth_single = fs * (Math.PI * Dr1 * Dr1 / 4);


            //list.Add(string.Format("Strength of a rivet in single shear = fs x 3.1415 x (d^2 / 4) = 100 x 3.1415 x 21.5^2 / 4 = 36,305 N"));
            list.Add(string.Format("Strength of a rivet in single shear = fs x 3.1416 x (d^2 / 4)"));
            list.Add(string.Format("                                    = {0} x 3.1415 x {1}^2 / 4", fs, Dr1));
            list.Add(string.Format("                                    = {0:f3} N", stngth_single));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double brng_strn = fb * Dr1 * tb;
            list.Add(string.Format("Strength of a rivet in bearing = fb x Dr1 x tb"));
            //list.Add(string.Format("                               = 300 x 21.5 x 6"));
            list.Add(string.Format("                               = {0} x {1} x {2}", fb, Dr1, tb));
            list.Add(string.Format("                               = {0:f3} N", brng_strn));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double sf_ld = Math.Min(stngth_single, brng_strn);


            //list.Add(string.Format("Safe load per rivet = Minimum of the above two values = 36,305 N > 11,404 N,  Hence OK."));
            if (sf_ld >= A) list.Add(string.Format("Safe load per rivet = Minimum of the above two values = {0:f3} N > {1:f3} N,  Hence OK.", sf_ld, A));
            else
            {
                IS_DESIGN_OK = false;
                list.Add(string.Format("Safe load per rivet = Minimum of the above two values = {0:f3} N < {1:f3} N,  Hence NOT OK.", sf_ld, A));
            }



            double no_face = ((int)(l * 1000 / d) + 1);
            list.Add(string.Format("Total number of battens per face = Length of the column / Spacing of battens + 1"));
            list.Add(string.Format("                                 = (l x 1000 / d) + 1"));
            //list.Add(string.Format("                                 = 4,000 / 560 + 1 = 9"));
            list.Add(string.Format("                                 = {0} * 1000 / {1} + 1", l, d));
            list.Add(string.Format("                                 = {0}", no_face));


            double tot_bat = 2 * no_face;

            list.Add(string.Format("Hence, provide {0} battens in each side face, therefore, total 2 x {0} = {1} battens in two side faces.", no_face, tot_bat));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4:  Design of flange splices at two joining sections"));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Design 91. An ISHB 250 x 54.7 kg/m column has to be spliced at a section."));
            list.Add(string.Format("An ISHB 250 x 54.7 kg/m column has to be spliced at a section."));
            list.Add(string.Format("The axial load on the column is 500 kN, the horizontal shear at the splice "));
            list.Add(string.Format("section is 40 kN, and the bending moment at the splice section is 50 kN-m."));
            list.Add(string.Format("Assume that the column ends are faced for complete bearing. Design the column splice."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Maximum Compressive Load  = P = 1350 kN (500)"));
            list.Add(string.Format("Maximum Compressive Load  = P = {0} kN (500)", P));
            //list.Add(string.Format("Maximum Bending Moment = M = 50 kN-m "));
            list.Add(string.Format("Maximum Bending Moment = M = {0} kN-m ", M));
            //list.Add(string.Format("Maximum Shear Force = V = 40 kN "));
            list.Add(string.Format("Maximum Shear Force = V = {0} kN ", V));
            list.Add(string.Format(""));

            //P = 1350;
            //M = 50;
            //V = 40;

            list.Add(string.Format("Solution. In this case we will provide flange and web splices."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of the flange splices"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Load transmitted to flange splices from column"));
            list.Add(string.Format(""));

            double P1 = 0.5 * P;
            //list.Add(string.Format("P1 = 50% of the column load = (50/100) x 1350 = 675 kN"));
            list.Add(string.Format("P1 = 50% of the column load = (50/100) x {0} = {1} kN", P, P1));
            list.Add(string.Format(""));

            double P2 = P1 / 2;
            //list.Add(string.Format("Load on one splice plate = P2 = 675 / 2 = 337.5 kN"));
            list.Add(string.Format("Load on one splice plate = P2 = {0} / 2 = {1:f2} kN", P1, P2));
            list.Add(string.Format(""));

            double P3 = M / (h / 1000);
            list.Add(string.Format("Load transmitted to one splice plate due to moment,"));
            list.Add(string.Format("P3 = M/(h/1000) "));
            //list.Add(string.Format("   = 50 / (300/1000) = 50 / 0.30 = 166.67 kN"));
            list.Add(string.Format("   = {0} / ({1:f2}/1000)", M, h));
            list.Add(string.Format("   = {0:f2} kN", P3));
            list.Add(string.Format(""));

            double P4 = P2 + P3;
            list.Add(string.Format("Maximum load on one splice plate"));
            list.Add(string.Format(" P4 = P2 + P3"));
            //list.Add(string.Format("    = 337.5 + 166.67 = 504.17 kN"));
            list.Add(string.Format("    = {0:f3} + {1:f3}", P2, P3));
            list.Add(string.Format("    = {0:f3} kN", P4));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("Safe compressive stress for splice plate = Ps = 150 N/mm^2", ps));
            list.Add(string.Format("Safe compressive stress for splice plate = Ps = {0} N/mm^2", Ps));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double area = P4 * 1000 / Ps;
            list.Add(string.Format("Area of one splice plate = P4 x 1000 / Ps = {0:f2} x 1000 / {1} = {2:f2} Sq.mm", P4, Ps, area));
            list.Add(string.Format(""));

            double wd1 = 2 * Bf;
            list.Add(string.Format("Width of the splice plate = width of flange of the column = 2 x Bf = 2 x {0} = {1} mm.", Bf, wd1));
            list.Add(string.Format(""));

            double t2 = (int)(area / wd1);
            //list.Add(string.Format("Thickness of the splice plate = 3361.13 / 280 = 12 mm"));
            list.Add(string.Format("Thickness of the splice plate = {0:f3} / {1} = {2} mm", area, wd1, t2));
            list.Add(string.Format(""));


            //list.Add(string.Format("Provide {0} mm thick splice plates, t2 = {1} mm.", wd1, t2));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide {0} mm thick splice plates, t2 = {0} mm.", t2));


            double Dr2 = Dr + 1.5;
            //list.Add(string.Format("Provide 20 mm diameter rivets, with 1.5 mm clearance, diameter = Dr2 = 20 + 1.5 = 21.5 mm."));
            list.Add(string.Format("Provide {0} mm diameter rivets, with 1.5 mm clearance, diameter = Dr2 = {0} + 1.5 = {1} mm.", Dr, Dr2));
            list.Add(string.Format(""));


            double V1 = fs * Math.PI * Dr2 * Dr2 / 4;
            list.Add(string.Format("Strength of a rivet in single shear   = fs x 3.1416  x Dr2^2 / 4 "));
            //list.Add(string.Format("                                      = 100 x 3.1416 x 21.5^2/4 = 36,356 N"));
            list.Add(string.Format("                                      = {0} x 3.1416 x {1}^2/4", fs, Dr2));
            list.Add(string.Format("                                      = {0:f0} N", V1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double V2 = fb * Dr2 * t2;
            list.Add(string.Format("Strength of a rivet in bearing  = fb x Dr2 x t2 "));
            list.Add(string.Format("                                = {0} x {1} x {2}", fb, Dr2, t2));
            list.Add(string.Format("                                = {0:f3} N", V2));
            list.Add(string.Format(""));

            double Pr = Math.Min(V1, V2);

            //list.Add(string.Format("Safe load per rivet = Pr = 36,356 N"));
            list.Add(string.Format("Safe load per rivet = Pr = {0:f3} N", Pr));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of rivets required on each side of the splice section = P4 x 1000 / Pr"));
            //list.Add(string.Format("                                                             = 504.17 x 103 / 36,356 = 13.8 say 16 rivets"));

            double rvt_nos = ((int)(P4 * 1000 / Pr) + 1);

            if (rvt_nos % 2 != 0.0) rvt_nos = rvt_nos + 1;

            list.Add(string.Format("                                                             = {0:f3} x 10^3 / {1:f3}", P4, Pr, rvt_nos));

            list.Add(string.Format("                                                             = {0:f3} say {1} rivets", (P4 * 1000 / Pr), rvt_nos));
            list.Add(string.Format(""));


            list.Add(string.Format("Provide {0} rivets arranged in 4 horizontal rows with 4 (fixed) rivets per row, so 4 vertical rows.", rvt_nos));
            //list.Add(string.Format("(In case of {0} rivets arrange in 2 horizontal rows with 4 (fixed) rivets per row, so 4 vertical rows.)", (rvt_nos / 2)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Web Splice."));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("Two splice plates are provided one on either side of the web."));
            //list.Add(string.Format("Horizontal shear transmitted to splice plates = 40 kN"));
            list.Add(string.Format("Horizontal shear transmitted to splice plates = {0} kN", V));
            //list.Add(string.Format("Safe average shear stress = 100 N/Sq.mm"));


            double AVG_Shear_Stress = allow_shr_strss;

            list.Add(string.Format("Safe average shear stress = {0} N/Sq.mm", allow_shr_strss));

            double area_shr_plt = V * 1000 / AVG_Shear_Stress;
            list.Add(string.Format("Area of shear plates {0} x 1,000 / {1} = {2} Sq.mm", V, AVG_Shear_Stress, area_shr_plt));

            double area_each_shr_plt = area_shr_plt / 2.0;
            list.Add(string.Format("Area of each shear plate = {0} Sq.mm", area_each_shr_plt));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("Provide 4 vertical rows of rivets"));

            double sp_row = 40;
            double Prv_sp_row = 35;
            //list.Add(string.Format("Let, the spacing between the two rows be 40 mm"));
            //list.Add(string.Format("Providing an effective margin of 35 mm."));
            //list.Add(string.Format("Width of the splice plates = 40 x (4-1) + 35 + 35 = 120 +35 + 35 = 190 mm"));

            list.Add(string.Format("Let, the spacing between the two rows be {0} mm", sp_row));
            list.Add(string.Format("Providing an effective margin of {0} mm.", Prv_sp_row));

            double wd_sp_plt = sp_row * (4 - 1) + Prv_sp_row + Prv_sp_row;
            list.Add(string.Format("Width of the splice plates = {0} x (4-1) + {1} + {1} = {2} mm", sp_row, Prv_sp_row, wd_sp_plt));

            double thk_shr_plt = area_each_shr_plt / wd_sp_plt;

            //list.Add(string.Format("Thickness of shear plates = 200 / 190 = 1.05 mm"));
            list.Add(string.Format("Thickness of shear plates = {0:f3} / {1:f3} = {2:f3} mm", area_each_shr_plt, wd_sp_plt, thk_shr_plt));

            if (thk_shr_plt < 6)
            {
                thk_shr_plt = 6.0;
            }
            list.Add(string.Format("Provide a thickness of {0} mm for the Splice Plates from shear criterion. ", thk_shr_plt));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of the connection "));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double rvt1_nos = 4;
            double rvt1_ecc = 20;

            double rvt2_nos = 4;
            double rvt2_ecc = 60;

            list.Add(string.Format("Let us provide all 16 rivets with 8 rivets on each side of the splice. "));
            list.Add(string.Format("Figure 4  shows the proposed arrangement. Consider the rivets on first "));
            list.Add(string.Format("row of 4 rivets at an eccentricity of 20 mm and second row of 4 rivets at an "));
            list.Add(string.Format("eccentricity of 60 mm on upper and lower halves of the joint."));
            Rt = V * 1000 / 8;

            //list.Add(string.Format("Resistance against translation = Rt = 40 x 10^3 / 8 = 5,000 N"));
            list.Add(string.Format("Resistance against translation = Rt = {0} x 10^3 / 8 = {1:f3} N", V, Rt));
            list.Add(string.Format(""));
            list.Add(string.Format("Resistance against rotation (Torsional shear) = K x ra"));
            list.Add(string.Format("Where,    K = P.e / Σx^2 + Σy^2"));
            list.Add(string.Format(""));

            double area_horz = (rvt1_nos * (rvt1_ecc * rvt1_ecc + rvt1_ecc * rvt1_ecc)
                                + rvt2_nos * (rvt2_ecc * rvt2_ecc + rvt1_ecc * rvt1_ecc)
                                + rvt1_nos * (rvt1_ecc * rvt1_ecc + rvt2_ecc * rvt2_ecc)
                                + rvt2_nos * (rvt2_ecc * rvt2_ecc + rvt2_ecc * rvt2_ecc))/4;


            list.Add(string.Format("Measuring horizontal (x) and vertical (y) distances from the joint, to the rivets, above and below the joint,"));
            //list.Add(string.Format("        Σx^2 + Σy^2 = 4 x (20^2 + 20^2) + 4 x (60^2 + 20^2) + 4 x (20^2 + 60^2) + 4 x (60^2 + 60^2) = 16,000 Sq. mm"));
            list.Add(string.Format("        Σx^2 + Σy^2 = ({0} x ({1}^2 + {1}^2) + {2} x ({3}^2 + {1}^2) + {1} x ({0}^2 + {3}^2) + {2} x ({3}^2 + {3}^2))/4",
                rvt1_nos, rvt1_ecc, rvt2_nos, rvt2_ecc));
            //list.Add(string.Format("                    = 16,000 Sq. mm"));
            list.Add(string.Format("                    = {0:f3} Sq. mm", area_horz));
            list.Add(string.Format(""));


            K = V * 1000 * V / area_horz;
            //list.Add(string.Format("K = 40 x 10^3 x 40 /16,000 = 100 N/mm"));
            list.Add(string.Format("K = {0} x 10^3 x {0} / {1:f3} = {2:f3} N/mm", V, area_horz, K));
            list.Add(string.Format(""));

            list.Add(string.Format("Resistance against rotation = 100 x ra"));
            list.Add(string.Format(""));

            thk1 = 40;
            H = (Rt + K * thk1);//??
            list.Add(string.Format("Total horizontal component = H = Rt + K x (r x  cos θ)"));
            //list.Add(string.Format("                               = 5000 + 100 x 40 = 9000 N"));
            list.Add(string.Format("                               = {0} + {1} x {2}", Rt, K, thk1));
            list.Add(string.Format("                               = {0:f3} N", H));
            list.Add(string.Format(""));


            //V = = K x (r x sin θ)

            V1 = K * thk1;
            list.Add(string.Format("Vertical component = V = K x (r x sin θ)"));
            //list.Add(string.Format("                       = 100 x 40 = 4000 N"));
            list.Add(string.Format("                       = {0} x {1}", K, thk1));
            list.Add(string.Format("                       = {0:f3} N", V1));
            list.Add(string.Format(""));


            double R = Math.Sqrt(H * H + V1 * V1);
            //list.Add(string.Format("Resultant resistance = √(9000^2 + 4000^2) = 9848.86 N"));
            list.Add(string.Format("Resultant resistance = √({0:f3}^2 + {1:f3}^2) = {2:f3} N", H, V1, R));
            list.Add(string.Format(""));

            double bar_dia = 16.0;
            double Dr3 = bar_dia + 1.5;
            //list.Add(string.Format("Provide 16 mm diameter rivets, allowing 1.5 mm clearance, Dr3 = 16+1.5 = 17.5 mm,"));
            list.Add(string.Format("Provide {0} mm diameter rivets, allowing 1.5 mm clearance, Dr3 = {0} + 1.5 = {1} mm,", bar_dia, Dr3));
            list.Add(string.Format(""));


            double Rvt_dbl_shr = 2 * fs * Math.PI * Dr3 * Dr3 / 4;
            list.Add(string.Format("Rivet value in double shear = 2 x fs x π x Dr3^2 / 4"));
            //list.Add(string.Format("                            = 2 x 100 x  3.1416 x 17.5^2 / 4"));
            list.Add(string.Format("                            = 2 x {0} x  3.1416 x {1}^2 / 4", fs, Dr3));
            list.Add(string.Format("                            = {0:f3} N", Rvt_dbl_shr));
            list.Add(string.Format(""));


            double Rvt_Brg = fb * Dr3 * tw;
            list.Add(string.Format("Rivet value in bearing = fb x Dr3 x tw"));
            //list.Add(string.Format("                       = 300 x 17.5 x 8.8 (Thickness of web = tw = 8.8 mm)"));
            //list.Add(string.Format("                       = 46,200 N"));
            list.Add(string.Format("                       = {0} x {1} x {2} (Thickness of web = tw = {2} mm)", fb, Dr3, tw));
            list.Add(string.Format("                       = {0:f3} N", Rvt_Brg));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double safe_load = Math.Min(Rvt_dbl_shr, Rvt_Brg);
            list.Add(string.Format("Safe load per rivet = {0} N (The lower of the above two values)", safe_load));

            if (safe_load >= R) list.Add(string.Format("Hence, the design in safe, OK"));
            else list.Add(string.Format("Hence, the design in unsafe, NOT OK"));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5:  Design of the base plate for the column"));
            list.Add(string.Format("-------------------------------------------------"));
            list.Add(string.Format(""));

            double W = 600.0;

            //D 
            list.Add(string.Format(""));
            //list.Add(string.Format("Column load = W = 600 kN"));
            //list.Add(string.Format("Eccentricity = e = 100 mm"));
            //list.Add(string.Format("Depth of the column section = 300 mm"));

            //list.Add(string.Format("Providing a cantilever projection of 200 mm beyond the flanges, length of the base plate"));
            //list.Add(string.Format("      = I = 300 + 200 + 200 = 700 mm"));
            //list.Add(string.Format("      = e / I = 100 / 700 = 0.14"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        This is not in between 1/6 = 0.167 and 1/3 = 0.333"));
            //list.Add(string.Format("        Distance from the load line to the extreme compression edge"));
            //list.Add(string.Format("                                = I / 2 - e = 700 / 2 - 100 = 250 mm"));
            //list.Add(string.Format("Length of the pressure diagram = X = 3 x 250 = 750 mm"));
            //list.Add(string.Format("Let the maximum compressive pressure reach 4 N/mm2."));
            //list.Add(string.Format("Let the width of the base plate be b mm."));
            //list.Add(string.Format("Equating the upward reaction to the downward load."));
            //list.Add(string.Format("                1/ 2 x 750 x 4 x b = 1350 x 1,000"));
            //list.Add(string.Format("b = 900 mm"));
            //list.Add(string.Format("Provide a width of 900 mm."));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Let the actual maximum upward pressure intensity be p N / mm2"));
            //list.Add(string.Format("1 / 2 x 750 x p x 900 = 1350 x 1,000                                                 "));
            //list.Add(string.Format("P = 4  N/mm2"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Figure 5 (5.13) shows the pressure distribution diagram below the base plate."));
            //list.Add(string.Format(" "));
            //list.Add(string.Format("Figure 5 (5.13)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Provide 700 mm x 900 mm x 35 mm Base Plate."));

            double D = h;
            list.Add(string.Format("Column load = W = {0} kN", W));
            list.Add(string.Format("Eccentricity = e = {0} mm", e));
            list.Add(string.Format("Depth of the column section = {0} mm", D));
            list.Add(string.Format(""));

            double cant_proj = 200.0;
            list.Add(string.Format("Providing a cantilever projection of 200 mm beyond the flanges, length of the base plate"));

            double I = D + cant_proj + cant_proj;
            list.Add(string.Format("      = I = {0} + {1} + {1} = {2} mm", D, cant_proj, I));
            list.Add(string.Format(""));

            double e_by_I = e / I;
            list.Add(string.Format("      = e / I = {0} / {1} = {2:f3}", e, I, e_by_I));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("This is not in between 1/6 = 0.167 and 1/3 = 0.333"));
            list.Add(string.Format("Distance from the load line to the extreme compression edge"));
            list.Add(string.Format(""));

            double ext_cmp_ed = I / 2 - e;
            list.Add(string.Format("     = I / 2 - e = {0} / 2 - {1} = {2} mm", I, e, ext_cmp_ed));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double X = 3 * ext_cmp_ed;
            list.Add(string.Format("Length of the pressure diagram = X = 3 x {0} = {1} mm", ext_cmp_ed, X));
            list.Add(string.Format(""));

            double max_comp_prs = 4;

            list.Add(string.Format("Let the maximum compressive pressure reach {0} N/Sq.mm.", max_comp_prs));
            list.Add(string.Format(""));
            list.Add(string.Format("Let the width of the base plate be 'b' mm."));
            list.Add(string.Format("Equating the upward reaction to the downward load."));
            list.Add(string.Format(""));
            //list.Add(string.Format("    1/ 2 x 750 x 4 x b = 1350 x 1,000"));

            double b = 2 * (P * 1000) / (X * max_comp_prs);
            list.Add(string.Format("    1/ 2 x X x 4 x b = P x 1,000"));
            list.Add(string.Format(""));
            list.Add(string.Format("                     b = 2 x ({0} x 1000)/({1} x {2})", P, X, max_comp_prs));
            list.Add(string.Format("                     b = {0:f0} mm", b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide a width of {0:f0} mm.", b));
            list.Add(string.Format(""));
            list.Add(string.Format("Let the actual maximum upward pressure intensity be 'p' N / mm2"));
            list.Add(string.Format(""));
            list.Add(string.Format("1 / 2 x 750 x p x 900 = 1350 x 1,000"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("P = 4 N/Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Figure 5 (5.13) shows the pressure distribution diagram below the base plate."));
            list.Add(string.Format(" "));
            //list.Add(string.Format("Figure 5 (5.13)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide 700 mm x 900 mm x 35 mm Base Plate."));


            //File.WriteAllLines(Report_File, list.ToArray());

            return list;

        }

        public void Calculate_Program()
        {
            //Design_Individual_Program
            File.WriteAllLines(Get_Report_File(), Design_Individual_Program().ToArray());

        }
        public string Report_File { get; set; }

        public bool IS_DESIGN_OK { get; set; }

        public string Get_Report_File()
        {
            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            //tmp_file = Path.Combine(tmp_file, "DESIGN_STEEL_COLUMN_" + step.ToString("000") + ".TXT");
            tmp_file = Path.Combine(tmp_file, "DESIGN_STEEL_COLUMN_" + ColumnNo + ".TXT");
            return tmp_file;
        }
        public string Get_BOQ_File(int step)
        {
            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "BOQ");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "BOQ_C" + step.ToString("000") + ".TXT");

            return tmp_file;
        }

        public string Get_Design_Summary_File(int step)
        {
            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "DESIGN SUMMARY");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "DESIGN_SUMMARY_C" + step.ToString("000") + ".TXT");

            return tmp_file;
        }

        public string File_Design_Summary
        {
            get
            {
                string des_sum = Path.Combine(Path.GetDirectoryName(Get_Report_File()), "COLUMN_DESIGN_SUMMARY.TXT");
                return des_sum;
            }
        }


        public object bar_nos { get; set; }
    }

    public class SteelBeamDesign
    {
        IApplication iApp;

        public double l, a, M, V, Pms, Pss, Pbs, h, tw, Ixx, h1, h2, w, Z;
        public string sectionName = "ISLB 400";

        public SteelBeamDesign(IApplication iapp)
        {
            iapp = iApp;

            l = 8.0;
            a = 300;
            M = 151.13;
            V = 70.2;
            Pms = 165;
            Pss = 100;
            Pbs = 187.5;

            h = 400; tw = 8; Ixx = 19306.3; h1 = 306.2; h2 = 31.9; w = 56.9; Z = 965.3;
        }
        public void Calculate_Program()
        {
            List<string> list = new List<string>();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Steel Beam "));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User’s Input data :"));
            list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));

            //double l, a, M, V, Pms, Pss, Pbs, h, tw, Ixx, h1, h2, w, Z;

            IS_DESIGN_OK = true;

            list.Add(string.Format("Span = l = {0} m.", l));
            list.Add(string.Format("Bearing at each end = a = {0} mm.", a));
            list.Add(string.Format("Maximum Bending Moment  = M = {0} kN-m", M));
            list.Add(string.Format("Maximum Shear Force = V = {0} kN", V));
            list.Add(string.Format("Permissible Bending Stress = Pms = {0} N/Sq.mm", Pms));
            list.Add(string.Format("Permissible Shear Stress = Pss = {0} N/Sq.mm", Pss));
            list.Add(string.Format("Permissible Bearing Stress = Pbs = {0} N/Sq.mm", Pbs));
            list.Add(string.Format("Selected Steel Section = {0}", sectionName));
            list.Add(string.Format("(Overall depth = h = {0} mm, tw = {1} mm, Ixx = {2} x 10000 Sq. Sq. mm,", h, tw, Ixx/10000));
            list.Add(string.Format(" h1 = {0} mm, h2 = {1} mm, w = {2} kg/m, Z = {3} x 1000 cu.mm)", h1, h2, w, Z/1000));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Calculations :"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Check for Section Modulus"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));

            double req_mod1 = M * 1000000 / Pms;
            //list.Add(string.Format("Required Section Modulus = M x 1000000 / Pms = 151.13 x 1000000 / 165 = 915.94 x 1000 cu.mm"));
            list.Add(string.Format("Required Section Modulus = M x 10^6 / Pms = {0} x 10^6 / {1} = {2:f2} x 1000 cu.mm", M, Pms, req_mod1 / 1000));
            if (Z >= req_mod1)
            {
                //list.Add(string.Format("Provided Section Modulus = 965.3 x 1000 cu.mm > 915.94 x 1000         Hence OK."));
                list.Add(string.Format("Provided Section Modulus = {0:f2} x 1000 cu.mm > {1:f2} x 1000         Hence OK.", Z / 1000, req_mod1 / 1000));
            }
            else
            {
                list.Add(string.Format("Provided Section Modulus = {0:f2} x 1000 cu.mm < {1:f2} x 1000         Hence NOT OK.", Z / 1000, req_mod1 / 1000));
                IS_DESIGN_OK = false;
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : Check for Shear Stress"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));

            double ISS = V * 1000 / (h * tw);
            //list.Add(string.Format("Induced Shear Stress = V  x 1000 / (h x tw) = 70.2 x 1000 / (400 x 8) = 21.93 N/Sq.mm "));
            list.Add(string.Format("Induced Shear Stress = V  x 1000 / (h x tw) = {0:f2} x 1000 / ({1} x {2}) = {3:f2} N/Sq.mm ", V, h, tw, ISS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("Permissible Shear Stress = 100 N/Sq.mm  > 21.93 Sq.mm  Hence OK."));
            if (Pss > ISS)
            {
                list.Add(string.Format("Permissible Shear Stress = {0} N/Sq.mm  > {1} Sq.mm  Hence OK.", Pss, ISS));
            }
            else
            {
                list.Add(string.Format("Permissible Shear Stress = {0:f2} N/Sq.mm  < {1} Sq.mm  Hence NOT OK.", Pss, ISS));
                IS_DESIGN_OK = false;
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3 : Check for Web Crippling at Support "));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double IBS = V * 1000 / (tw * (a + h2 * Math.Sqrt(3)));
            list.Add(string.Format("Induced Bearing Stress = V x 1000 / [tw x (a + h2 x sqrt (3))]"));
            //list.Add(string.Format("                       = 70.2 x 1000 / [8 x (300 + 31.9 x sqrt (3))]"));
            list.Add(string.Format("                       = {0} x 1000 / [{1} x ({2} + {3} x sqrt(3))]", V, tw, a, h2));
            //list.Add(string.Format("                       = 24.70 N/Sq.mm  "));
            list.Add(string.Format("                       = {0:f3} N/Sq.mm", IBS));
            list.Add(string.Format(""));
            if (Pbs >= IBS) list.Add(string.Format("Permissible Bearing Stress = {0:f3} N/Sq.mm  > {1:f3} Sq.mm,   Hence OK.", Pbs, IBS));
            else list.Add(string.Format("Permissible Bearing Stress = {0:f3} N/Sq.mm  < {1:f3} Sq.mm,   Hence NOT OK.", Pbs, IBS));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4 : Check for Buckling of the Web"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Check for Buckling of the Web:"));
            list.Add(string.Format(""));


            double SRWB = h1 * Math.Sqrt(3) / l;
            list.Add(string.Format("Slenderness Ratio for Web Buckling = h1 x sqrt(3) / l"));
            //list.Add(string.Format("                                   = 336.2 x sqrt(3) / 8"));
            list.Add(string.Format("                                   = {0:f3} x {1:f3} / {2}", h1, Math.Sqrt(3), l));
            list.Add(string.Format("                                   = {0:f3}", SRWB));
            list.Add(string.Format(""));

            double Sigma_ac = 110;
            //list.Add(string.Format("Corresponding Safe Compressive Stress  = Sigma_ac = 110 N/Sq.mm"));
            list.Add(string.Format("Corresponding Safe Compressive Stress  = Sigma_ac = {0} N/Sq.mm", Sigma_ac));
            list.Add(string.Format(""));
            list.Add(string.Format("(Refer to Table for “Permissible_Bending_Compressive_Stresses”)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double B = a + h / 2;
            //list.Add(string.Format("B = a + h/2 = 300 + 400/2 = 500 mm."));
            list.Add(string.Format("B = a + h/2 = {0} + {1}/2 = {2} mm.", a, h, B));
            list.Add(string.Format(""));

            double LBCW = (Sigma_ac * tw * B) / 1000;
            list.Add(string.Format("Load Bearing Capacity of Web = Sigma_ac x tw x B"));
            //list.Add(string.Format("                             = 110 x 8 x 500 "));
            list.Add(string.Format("                             = {0} x {1} x {2} ", Sigma_ac, tw, B));
            //list.Add(string.Format("                             = 4,40,000 N"));
            list.Add(string.Format("                             = {0:f2} x 1000 N", LBCW));

            if (LBCW >= V)
            {
                list.Add(string.Format("                             = {0:f2} kN > V = {1:f2} kN   Hence OK.", LBCW, V));
            }
            else
            {
                list.Add(string.Format("                             = {0:f2} kN < V = {1:f2} kN   Hence NOT OK.", LBCW, V));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5 : Check for Deflection"));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Deflection = (5/384) x (w x l^4/EI)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Where, w = load per metre length, l = span, E = Elastic Modulus and I = Moment of Inertia of Beam Section."));
            list.Add(string.Format(""));

            double max_defl = 4.85;
            list.Add(string.Format("Maximum Deflection Obtained from Analysis Result (for Beams with 8 m span) = {0:f5} mm.", max_defl));
            list.Add(string.Format(""));

            double eff_span = l + 2 * a / (2 * 1000);
            //list.Add(string.Format("Effective Span = span + 2 x a / (2 x 1000) = 8.0 + 0.300 = 8.3 m."));
            list.Add(string.Format("Effective Span = span + 2 x a / (2 x 1000) = {0} + {1:f3} = {2:f3} m.", l, 2 * a / (2 * 1000), eff_span));
            list.Add(string.Format(""));
            list.Add(string.Format("Permissible Deflection  = Effective Span x 1000 / 325 "));

            double PD = eff_span * 1000 / 325;
            //list.Add(string.Format("                        = 8.3 x 1000 / 325"));
            list.Add(string.Format("                        = {0:f3} x 1000 / 325", eff_span));
            //list.Add(string.Format("                        = 25.5 mm > Maximum Deflection = 8.3 mm    Hence OK"));

            if (PD >= max_defl)
                list.Add(string.Format("                        = {0:f3} mm > Maximum Deflection = {1:f3} mm    Hence OK", PD, max_defl));
            else
            {
                list.Add(string.Format("                        = {0:f3} mm < Maximum Deflection = {1:f3} mm    Hence NOT OK", PD, max_defl));
                IS_DESIGN_OK = false;
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            //File.WriteAllLines(Report_File, list.ToArray());
            File.WriteAllLines(Get_Report_File(), list.ToArray());
        }

        public string Report_File { get; set; }

        public bool IS_DESIGN_OK { get; set; }

        #region IBeamDesign Members

        public List<BeamData> All_Beam_Data
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ASTRADoc AST_DOC
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Beam_Nos
        {
            get;
            set;
        }

        public string Beam_Title
        {
            get;
            set;
        }

        public Beam_BOQ BOQ
        {
            get;
            set;
        }



        public void Get_Continuous_Beams(ref JointCoordinateCollection cont_jcc,
            ref MemberIncidenceCollection mbr_coll, ref DirecctionCollection dc1)
        {

            MemberIncidence b1 = AST_DOC.Members.Get_Member(MyList.StringToInt(Beam_Nos, 0));

            if (b1 == null) return;

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();

            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();

            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            List<bool> flags = new List<bool>();



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


            if (jcc[0].NodeNo < jcc[1].NodeNo)
            {
                if (!cont_jcc.Contains(jcc[1]))
                {
                    cont_jcc.Add(jcc[1]);
                }
            }
            cont_jcc.Add(jcc[0]);

            mbr_coll.Add(b1);

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
                        else if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
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



            //DirecctionCollection dc1 = new DirecctionCollection();

            Axis_Direction ad = new Axis_Direction();
            int index = 0;

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
        }

        public List<int> Get_Continuous_Beams(MemberIncidence b1)
        {

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
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

        public List<int> Get_Continuous_Beams(MemberIncidence b1,
            ref JointCoordinateCollection cont_jcc,
            ref MemberIncidenceCollection mbr_coll,
            ref DirecctionCollection dc1)
        {

            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            //MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

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
                                    if (!mbr_coll.Contains(item))
                                        mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    if (!mbr_coll.Contains(item))
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


            Axis_Direction ad = new Axis_Direction();
            int index = 0;

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

            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }



        //Chiranjit [2015 03 06]



        public static List<string> Get_Banner()
        {
            List<string> list = new List<string>();


            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*                 ASTRA Pro                  *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*       DESIGN OF RCC FLANGED BEAM           *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add("");
            list.Add("");

            #endregion


            return list;

        }

         
        //Chiranjit [2015 03 06]
        public List<string> Design_Program_Loop(int step)
        {
            //Store Bar Nos
            return new List<string>();
        }


        public double Floor_Level = 0.0;

        public List<string> Design_Program_Individual()
        {
            //Store Bar Nos

            List<string> list = new List<string>();

            return list;
        }

        public string Get_Report_File()
        {

            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
            tmp_file = Path.Combine(tmp_file, "DESIGN_STEEL_BEAM_FL_" + Floor_Level.ToString("f3").Replace(".","_") + "_NO_" + Beam_Nos  +".TXT");
            return tmp_file;
        }


        public string Get_BOQ_File(int step)
        {

            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            tmp_file = Path.Combine(tmp_file, "BOQ");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
            tmp_file = Path.Combine(tmp_file, "BOQ_B" + step.ToString("000") + ".TXT");
            return tmp_file;
        }

        public string Get_Design_Summary_File(int step)
        {

            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            tmp_file = Path.Combine(tmp_file, "DESIGN SUMMARY");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
            tmp_file = Path.Combine(tmp_file, "DESIGN_SUMMARY_B" + step.ToString("000") + ".TXT");
            return tmp_file;
        }
        public string File_Design_Summary
        {
            get
            {
                string des_sum = Path.Combine(Path.GetDirectoryName(Get_Report_File()), "BEAM_DESIGN_SUMMARY.TXT");
                return des_sum;
            }
        }
        #endregion
    }

    interface IBeamDesign
    {
        System.Collections.Generic.List<BeamData> All_Beam_Data { get; set; }
        HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc AST_DOC { get; set; }
        string Beam_Nos { get; set; }
        string Beam_Title { get; set; }
        Beam_BOQ BOQ { get; set; }
        void Calculate_Program();
        void Calculate_Program_Loop();
        System.Collections.Generic.List<string> Design_Program_Individual();
        System.Collections.Generic.List<string> Design_Program_Loop(int step);
        System.Collections.Generic.List<string> Design_Summary { get; set; }
        eDesignStandard DesignStandard { get; set; }
        string File_Design_Summary { get; }
        void Get_All_Forces(HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref double AM1, ref double AM2, ref double AM3, ref double AM4, ref double AV1, ref double AV2, ref double AV3);
        string Get_BOQ_File(int step);
        System.Collections.Generic.List<int> Get_Continuous_Beams(HEADSNeed.ASTRA.ASTRAClasses.MemberIncidence b1, ref HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref HEADSNeed.ASTRA.ASTRAClasses.MemberIncidenceCollection mbr_coll, ref HEADSNeed.ASTRA.ASTRAClasses.DirecctionCollection dc1);
        System.Collections.Generic.List<int> Get_Continuous_Beams(HEADSNeed.ASTRA.ASTRAClasses.MemberIncidence b1);
        void Get_Continuous_Beams(ref HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref HEADSNeed.ASTRA.ASTRAClasses.MemberIncidenceCollection mbr_coll, ref HEADSNeed.ASTRA.ASTRAClasses.DirecctionCollection dc1);
        string Get_Design_Summary_File(int step);
        string Get_Report_File(int step);
        bool IS_DESIGN_OK { get; set; }
        double L { get; set; }
        double Lx1 { get; set; }
        double Lx2 { get; set; }
        double Ly1 { get; set; }
        double Ly2 { get; set; }
        string Report_File { get; set; }
    }



    interface IColumnDesign
    {
        System.Collections.Generic.List<ColumnData> All_Column_Data { get; set; }
        HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc AST_DOC { get; set; }
        string Beam_Nos { get; set; }
        Column_BOQ BOQ { get; set; }
        System.Collections.Generic.List<string> BOQ_Summary { get; set; }
        void Calculate_Program();
        void Calculate_Program_Loop();
        ColumnData Col_Data { get; set; }
        System.Collections.Generic.List<string> Design_Program_Individual(int step);
        System.Collections.Generic.List<string> Design_Program_Individual2(int step);
        System.Collections.Generic.List<string> Design_Program_Loop(int step);
        System.Collections.Generic.List<string> Design_Summary { get; set; }
        string File_Design_Summary { get; }
        string Get_BOQ_File(int step);
        System.Collections.Generic.List<int> Get_Continuous_Members(HEADSNeed.ASTRA.ASTRAClasses.MemberIncidence b1, ref HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref HEADSNeed.ASTRA.ASTRAClasses.MemberIncidenceCollection mbr_coll);
        string Get_Design_Summary_File(int step);
        string Get_Report_File(int step);
        bool IS_DESIGN_OK { get; set; }
        string Report_File { get; set; }
        void Write_All_Data();
    }


}
