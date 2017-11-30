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
    public partial class frmMicrowaveTower : Form
    {

        #region Processing
        IApplication iApp;

        public string FilePath { get; set; }

        public string INPUT_FILE { get; set; }
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
        ACLSS.MemberGroupCollection Groups { get; set; }

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

        public frmMicrowaveTower(string input_file)
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
                Groups = new ACLSS.MemberGroupCollection();
                Materials = new List<MaterialProperty>();

                IsDrawingFileOpen = false;
            }
            catch (Exception ex) { }

        }

        public frmMicrowaveTower(string drawing_file, bool IsDrawingFile)
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
                Groups = new ACLSS.MemberGroupCollection();
                Materials = new List<MaterialProperty>();

            }
            catch (Exception ex) { }

        }

        eASTRADesignType Project_Type { get; set; }
        public frmMicrowaveTower(IApplication app, eASTRADesignType prts)
        {
            //this.Show
            try
            {

                iApp = app;

                Project_Type = prts;
                InitializeComponent();
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();
                Drawing_File = "";
                File_Name = "";
                ACad = new ASTRACAD();


                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new ACLSS.MemberGroupCollection();
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
            //sc2.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);

            //sc1.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);

            sc2.Panel2Collapsed = true;

            sc1.Panel2Collapsed = true;

            sc2.Panel2Collapsed = false;

            sc1.Panel2Collapsed = false;

            if (tc_prp_main.SelectedTab == tab_file_open)
                Show_Panel(tab1_data);
            if (tc_prp_main.SelectedTab == tab_geom)
                Show_Panel(tab1_geom);
            else if (tc_prp_main.SelectedTab == tab_props)
                Show_Panel(tab1_props);
            else if (tc_prp_main.SelectedTab == tab_constants)
                Show_Panel(tab1_const);
            else if (tc_prp_main.SelectedTab == tab_supports)
                Show_Panel(tab1_supports);
            else if (tc_prp_main.SelectedTab == tab_loads)
                //Show_Panel(tab1_loads);
                Show_Panel(tab1_loads2);
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
            VD.Redraw(true);
        }


        private void frmMicrowaveTower_Load(object sender, EventArgs e)
        {
            try
            {

                this.Text = Title;
                Set_Project_Name();
                tc_prp_main.TabPages.Remove(tab_mov_load);
                tc_prp_main.TabPages.Remove(tab_dynamic);
                tc_prp_main.TabPages.Remove(tab_ana_spec);

                uC_CAD1.iApp = iApp;

                tc_pp_main.TabPages.Remove(tab_moving_load);
                tc_pp_main.TabPages.Remove(tab_envelop);

                sc_design.SplitterDistance = 517;
                splitContainer2.SplitterDistance = 505;
                CalculateLoads();

                cmb_no_levels.SelectedIndex = 2;

                vdScrollableControl1.BaseControl.KeyUp += new KeyEventHandler(BaseControl_KeyUp);



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


                for (int i = 1; i < 100; i++)
                {
                    cmb_text_size.Items.Add(i.ToString());
                    cmb_pp_text_size.Items.Add(i.ToString());
                }

                cmb_text_size.SelectedIndex = 0;
                cmb_pp_text_size.SelectedIndex = 0;


                Load_Initials();



            }
            catch (Exception exx) { }
        }

        private void Load_Initials()
        {
            //if (tc_parrent.SelectedTab == tab_pre_process)
            //{
               
            //}


            //Base_Control_MouseEvent();

            //sc1.Panel2Collapsed = true;
            //MyList
          
            //MyList
            if (VDoc == null) return;

            if (IsFlag) return;
            IsFlag = true;


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

        }

        void BaseControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                AST_DOC.Members.RemoveCircle(VDoc);
                AST_DOC.JointLoads.Delete_ASTRAArrowLine(VDoc);
                AST_DOC.MemberLoads.Delete_ASTRAMemberLoad(VDoc);
                VDoc.Redraw(true);
            }

        }

        private void Open_Drawing_File()
        {

            if (File.Exists(Drawing_File))
            {

                if (!VDoc.Open(Drawing_File))
                    MessageBox.Show(Drawing_File + " could not be opened.");

                //VDoc.Palette.Background = Color.White;

                //VDoc.ActiveLayOut.Entities
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
            if (VDoc == null) return;
            VDoc.ActiveLayOut.Entities.EraseAll();

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
        }
        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load_Initials();

            if (tc_parrent.SelectedTab == tab_pre_process)
            {
                if (!IsFlag)
                {
                    Load_Initials();
                    IsFlag = true;
                }
            }

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
                fs.DGV_Joints = dgv_joints;
                fs.DGV_Members = dgv_members;
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_props;
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

                Draw_Supports();
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
                    tn.ExpandAll();
                }
                catch (Exception ex) { }
            }
            else
            {
                if (tv_loads.Nodes.Count > 0)
                    tn = tv_loads.Nodes[0];
            }
            #region Define Load Case
            if (btn.Name == btn_ldc_add.Name)
            {
                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
                {
                    tv_loads.Nodes.Add(fs.ASTRA_Data);
                    LoadCases.Add(fs.Ld);
                }
            }
            else if (btn.Name == btn_ldc_edit.Name)
            {

                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ASTRA_Data = tv_loads.Nodes[CurrentLoadIndex].Text;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
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
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Load Case

            #region Define Joint Load
            else if (btn.Name == btn_jload_add.Name)
            {

                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("JOINT"))
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


                frmJointLoad fs = new frmJointLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].JointLoads.Add(item);
                }
            }
            else if (btn.Name == btn_jload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmJointLoad fs = new frmJointLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }

            }
            else if (btn.Name == btn_jload_del.Name)
            {
                //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Joint Load

            #region Define SINK Load
            else if (btn.Name == btn_sdload_add.Name)
            {

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


                frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].SupportDisplacementLoads.Add(item);
                }
            }
            else if (btn.Name == btn_sdload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].SupportDisplacementLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fs.ASTRA_Data[0];
                }

            }
            else if (btn.Name == btn_sdload_del.Name)
            {
                //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define SINK Load


            #region Define Member Load
            else if (btn.Name == btn_mload_add.Name)
            {

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



                frmMemberLoad fs = new frmMemberLoad(ACad);

                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].MemberLoads.Add(item);
                    //tn.Nodes.Add(item);
                }
            }
            else if (btn.Name == btn_mload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].MemberLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmMemberLoad fs = new frmMemberLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].MemberLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_mload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Member Load


            #region Define Element Load
            else if (btn.Name == btn_eload_add.Name)
            {

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

                frmElementLoad fs = new frmElementLoad(ACad);

                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].ElementLoads.Add(item);
                    //tn.Nodes.Add(item);
                }
            }
            else if (btn.Name == btn_eload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].ElementLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmElementLoad fs = new frmElementLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].ElementLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_eload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Member Load

            #region Define Area Load
            else if (btn.Name == btn_aload_add.Name)
            {
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


                frmAreaLoad fs = new frmAreaLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();


                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].AreaLoads.Add(item);
                }
            }
            else if (btn.Name == btn_aload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmAreaLoad fs = new frmAreaLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_aload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Area Load

            #region Define Floor Load
            else if (btn.Name == btn_fload_add.Name)
            {
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


                frmFloorLoad fs = new frmFloorLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();


                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].AreaLoads.Add(item);
                }
            }
            else if (btn.Name == btn_fload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmFloorLoad fs = new frmFloorLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_fload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Floor Load


            #region Define Temperature Load
            else if (btn.Name == btn_tload_add.Name)
            {
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


                frmTempLoad fs = new frmTempLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].TemperatureLoads.Add(item);
                    //tn.Nodes.Add(item);
                }

            }
            else if (btn.Name == btn_tload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].TemperatureLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmTempLoad fs = new frmTempLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].TemperatureLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_tload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Temperature Load

            #region Define Repeat Load
            else if (btn.Name == btn_rload_add.Name)
            {
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


                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                fs.IsRepeatLoad = true;
                fs.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.LDC.LoadNo = Current_LoadCase.LoadNo;
                fs.LDC.Name = Current_LoadCase.Title;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.RepeatLoads = fs.LDC;
                    LoadCases[CurrentLoadIndex].RepeatLoads = fs.LDC;

                    Ld.RepeatLoads.Set_Combination();
                    for (int i = 0; i < Ld.RepeatLoads.Count; i++)
                    {
                        tn.Nodes.Add(Ld.RepeatLoads[i]);
                    }
                }

            }
            else if (btn.Name == btn_rload_edit.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                //fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.IsRepeatLoad = true;
                fs.LDC = Current_LoadCase.RepeatLoads;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {

                    tn = tv_loads.SelectedNode.Parent;


                    Current_LoadCase.RepeatLoads = fs.LDC;

                    tn.Nodes.Clear();

                    fs.LDC.Set_Combination();
                    for (int i = 0; i < fs.LDC.Count; i++)
                    {
                        tn.Nodes.Add(fs.LDC[i]);
                    }
                }
            }
            else if (btn.Name == btn_rload_del.Name)
            {
                try
                {
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Repeat Load

            #region Define Combination Load
            else if (btn.Name == btn_cload_add.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                fs.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.Comb_Loads = fs.LDC;

                    LoadCases.Add(Ld);

                    tv_loads.Nodes.Add(Ld.LoadNo + " : " + Ld.Title);

                    Ld.Comb_Loads.Set_Combination();
                    for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                    {
                        tv_loads.Nodes[tv_loads.Nodes.Count - 1].Nodes.Add(Ld.Comb_Loads[i]);
                    }
                }

            }
            else if (btn.Name == btn_cload_edit.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                //fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.LDC = Current_LoadCase.Comb_Loads;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.Comb_Loads = fs.LDC;

                    Current_LoadCase = (Ld);



                    tv_loads.Nodes[CurrentLoadIndex].Text = (Ld.LoadNo + " : " + Ld.Title);

                    tv_loads.Nodes[CurrentLoadIndex].Nodes.Clear();
                    Ld.Comb_Loads.Set_Combination();
                    for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                    {
                        tv_loads.Nodes[CurrentLoadIndex].Nodes.Add(Ld.Comb_Loads[i]);
                    }
                }
            }
            else if (btn.Name == btn_cload_del.Name)
            {
                try
                {
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Combination Load

            grb_cload.Enabled = (tv_loads.Nodes.Count > 0);

            grb_jload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_mload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_aload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_tload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_rload.Enabled = (tv_loads.Nodes.Count > 0);

            Button_Enable_Disable();

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
            Button btn = sender as Button;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                #region Drawing Open
                //if (btn.Name == btn_file_open.Name)
                //{
                //    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                //    if (ofd.ShowDialog() != DialogResult.Cancel)
                //    {
                //        txt_file_name.Text = ofd.FileName;

                //        Drawing_File = txt_file_name.Text;

                //        string fn = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                //        ASTRADoc astdoc;
                //        if (File.Exists(fn))
                //        {
                //            astdoc = new ASTRADoc(fn);
                //            ACad.AstraDocument = astdoc;

                //            AST_DOC.Members.DrawMember(VDoc);
                //            SetGridWithNode();
                //            SetGridWithMember();
                //        }
                //        else
                //            Open_Drawing_File();

                //    }
                //}
                #endregion Drawing Open
                #region Data Open
                //else if (btn.Name == btn_data_open.Name)
                //{
                //    ofd.Filter = "ASTRA Data Files (*.txt)|*.txt";
                //    if (ofd.ShowDialog() != DialogResult.Cancel)
                //    {
                //        txt_file_name.Text = ofd.FileName;

                //        Drawing_File = txt_file_name.Text;

                //        string fn = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                //        ASTRADoc astdoc;
                //        if (File.Exists(fn))
                //        {
                //            astdoc = new ASTRADoc(fn);
                //            ACad.AstraDocument = astdoc;

                //            AST_DOC.Members.DrawMember(VDoc);
                //            AST_DOC.Elements.DrawElements(VDoc);
                //            AST_DOC.Supports.DrawSupport(VDoc);

                //            chk_dynamic_analysis.Checked = false;
                //            rbtn_perform_eigen.Checked = true;
                //            txt_dynamic.Text = "";

                //            cmb_structure_type.SelectedItem = AST_DOC.StructureType;
                //            txtUserTitle.Text = AST_DOC.UserTitle;


                //            cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;
                //            cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;


                //            cmb_Prop_LUnit.SelectedIndex = (int)AST_DOC.Prop_LengthUnit;
                //            cmb_Load_MUnit.SelectedIndex = (int)AST_DOC.Load_MassUnit;
                //            cmb_Load_LUnit.SelectedIndex = (int)AST_DOC.Load_LengthUnit;




                //            SetGridWithNode();
                //            SetGridWithMember();
                //            //SetGridWithElement();

                //            Groups.Clear();
                //            tv_mem_grps.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberGroups)
                //            {
                //                tv_mem_grps.Nodes.Add(item);

                //                MemberGroup mg = new MemberGroup();
                //                MyStrings ml = new MyStrings(item, ' ');

                //                mg.GroupName = ml.StringList[0];
                //                mg.MemberNosText = ml.GetString(1);

                //                Groups.Add(mg);
                //            }

                //            tv_mem_props.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberProps)
                //            {
                //                tv_mem_props.Nodes.Add(item);
                //            }
                //            tv_mem_spec_cable.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberCables)
                //            {
                //                tv_mem_spec_cable.Nodes.Add(item);
                //            }
                //            tv_mem_spec_truss.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberTrusses)
                //            {
                //                tv_mem_spec_truss.Nodes.Add(item);
                //            }

                //            tv_constants.Nodes.Clear();
                //            Materials.Clear();
                //            for (int i = 0; i < AST_DOC.Constants.Count; i++)
                //            {
                //                tv_constants.Nodes.Add("MATERIAL_" + (i + 1));
                //                foreach (var item in AST_DOC.Constants[i].ASTRA_Data)
                //                {
                //                    tv_constants.Nodes[i].Nodes.Add(item);
                //                }
                //                Materials.Add(AST_DOC.Constants[i]);
                //            }

                //            tv_supports.Nodes.Clear();
                //            foreach (var item in AST_DOC.JointSupports)
                //            {
                //                tv_supports.Nodes.Add(item);
                //            }

                //            if (AST_DOC.SelfWeight != "")
                //            {
                //                chk_selfweight.Checked = true;
                //                cmb_Self_Weight.SelectedItem = AST_DOC.SelfWeight_Direction;
                //                txt_seft_wt_Value.Text = AST_DOC.SelfWeight;
                //            }
                //            tv_loads.Nodes.Clear();
                //            LoadCases.Clear();
                //            foreach (var item in AST_DOC.LoadDefines)
                //            {
                //                LoadCaseDefinition lcd = new LoadCaseDefinition();
                //                lcd.LoadNo = item.LoadCase;
                //                lcd.Title = item.LoadTitle;
                //                lcd.JointLoads = item.JointLoadList;
                //                lcd.MemberLoads = item.MemberLoadList;
                //                lcd.ElementLoads = item.ElementLoadList;
                //                lcd.AreaLoads = item.AreaLoadList;
                //                lcd.TemperatureLoads = item.TempLoadList;


                //                if (item.CominationLoadList.Count > 0)
                //                {
                //                    item.Set_Combination();
                //                    lcd.Comb_Loads.LoadNo = item.LoadCase;
                //                    lcd.Comb_Loads.Name = item.LoadTitle;
                //                    foreach (var cm in item.Combinations)
                //                    {
                //                        lcd.Comb_Loads.LoadCases.Add(cm.Load_No);
                //                        lcd.Comb_Loads.Factors.Add(cm.Load_Factor);
                //                    }
                //                    lcd.Comb_Loads.Set_Combination();
                //                }

                //                if (item.RepeatLoadList.Count > 0)
                //                {
                //                    item.Set_Combination();
                //                    lcd.RepeatLoads.LoadNo = item.LoadCase;
                //                    lcd.RepeatLoads.Name = item.LoadTitle;

                //                    foreach (var cm in item.Combinations)
                //                    {
                //                        lcd.RepeatLoads.LoadCases.Add(cm.Load_No);
                //                        lcd.RepeatLoads.Factors.Add(cm.Load_Factor);
                //                    }
                //                    lcd.RepeatLoads.Set_Combination();
                //                }
                //                //lcd.Comb_Loads = item.CominationLoadList;
                //                //lcd.RepeatLoads = item.RepeatLoadList;



                //                LoadCases.Add(lcd);
                //            }
                //            #region   LoadCases
                //            if (LoadCases.Count > 0)
                //            {
                //                TreeNode tn = null;
                //                TreeNode tjn = null;
                //                tv_loads.Nodes.Clear();
                //                foreach (var item in LoadCases)
                //                {
                //                    tv_loads.Nodes.Add(item.ToString());

                //                    tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];


                //                    if (item.RepeatLoads.Count > 0)
                //                    {
                //                        //item.Set_Combination();
                //                        tn.Nodes.Add("REPEAT LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.RepeatLoads)
                //                        {
                //                            tn.Nodes.Add(item1.ToString());
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.MemberLoads.Count > 0)
                //                    {
                //                        tn.Nodes.Add("MEMBER LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.MemberLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;

                //                    }
                //                    if (item.ElementLoads.Count > 0)
                //                    {
                //                        tn.Nodes.Add("ELEMENT LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.ElementLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;

                //                    }
                //                    if (item.JointLoads.Count > 0)
                //                    {

                //                        tn.Nodes.Add("JOINT LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.JointLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.AreaLoads.Count > 0)
                //                    {

                //                        tn.Nodes.Add("AREA LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.AreaLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.TemperatureLoads.Count > 0)
                //                    {

                //                        tn.Nodes.Add("TEMP LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.TemperatureLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.Comb_Loads.Count > 0)
                //                    {
                //                        //item.Set_Combination();
                //                        //tn.Nodes.Add("COMBINATION");
                //                        //if (tn.Nodes.Count > 0)
                //                        //    tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.Comb_Loads)
                //                        {
                //                            tn.Nodes.Add(item1.ToString());
                //                        }
                //                        //tn = tn.Parent;
                //                    }
                //                    //if (item.ElementLoad.Count > 0)
                //                    //{
                //                    //    tn.Nodes.Add("ELEMENT LOAD");
                //                    //    tn = tn.Nodes[0];
                //                    //    foreach (var item1 in item.ElementLoadList)
                //                    //    {
                //                    //        //tn.Nodes.Add(item1 + " " + astDoc.MassUnit + "/" + astDoc.LengthUnit);
                //                    //        tn.Nodes.Add(item1);
                //                    //    }
                //                    //    tn = tn.Parent;
                //                    //}
                //                }
                //            }
                //            #endregion

                //            #region Moving Load
                //            if (AST_DOC.MovingLoads.Count > 0)
                //            {
                //                TreeNode tn;
                //                tv_mov_loads.Nodes.Clear();
                //                MovingLoads.Clear();

                //                foreach (var item in AST_DOC.MovingLoads)
                //                {
                //                    MovingLoads.Add(item);
                //                    tn = new TreeNode(item.ToString());
                //                    tv_mov_loads.Nodes.Add(tn);
                //                    tn.Nodes.Add("LOADS : " + item.Loads);
                //                    tn.Nodes.Add("DISTANCES : " + item.Distances);
                //                    tn.Nodes.Add("LOAD WIDTH : " + item.LoadWidth);
                //                }


                //            }
                //            if (AST_DOC.LL_Definition.Count > 0)
                //            {


                //                TreeNode tn;
                //                tv_mov_def_load.Nodes.Clear();
                //                LL_Definition.Clear();

                //                foreach (var item in AST_DOC.LL_Definition)
                //                {

                //                    LL_Definition.Add(item);

                //                    tn = new TreeNode(item.ToString());

                //                    tv_mov_def_load.Nodes.Add(tn);
                //                    tn.Nodes.Add("X-Distance : " + item.X_Distance);
                //                    tn.Nodes.Add("Y-Distance : " + item.Y_Distance);
                //                    tn.Nodes.Add("Z-Distance : " + item.Z_Distance);
                //                    if (item.X_Increment != 0.0)
                //                        tn.Nodes.Add("X_Increment : " + item.X_Increment);
                //                    if (item.Y_Increment != 0.0)
                //                        tn.Nodes.Add("Y_Increment : " + item.Y_Increment);
                //                    if (item.Z_Increment != 0.0)
                //                        tn.Nodes.Add("Z_Increment : " + item.Z_Increment);
                //                    tn.Nodes.Add("Impact_Factor : " + item.Impact_Factor);

                //                }
                //            }
                //            #endregion Moving Load
                //            chk_perform_ana.Checked = AST_DOC.Analysis_Specification.Perform_Analysis;
                //            chk_print_ana_all.Checked = AST_DOC.Analysis_Specification.Print_Analysis_All;
                //            chk_print_load_data.Checked = AST_DOC.Analysis_Specification.Print_Load_Data;
                //            chk_print_static_check.Checked = AST_DOC.Analysis_Specification.Print_Static_Check;
                //            chk_print_supp_reac.Checked = AST_DOC.Analysis_Specification.Print_Support_Reaction;
                //            chk_print_max_force.Checked = AST_DOC.Analysis_Specification.Print_Max_Force;
                //            txt_max_frc_list.Enabled = true;
                //            tv_max_frc.Nodes.Clear();
                //            foreach (var item in AST_DOC.Analysis_Specification.List_Maxforce)
                //            {
                //                tv_max_frc.Nodes.Add(item);
                //            }

                //            if (AST_DOC.DynamicAnalysis.Count > 0)
                //            {
                //                chk_dynamic_analysis.Checked = true;

                //                if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM EIGEN"))
                //                {
                //                    rbtn_perform_eigen.Checked = true;
                //                    txt_frequencies.Text = AST_DOC.FREQUENCIES;
                //                }
                //                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM TIME HISTORY"))
                //                {
                //                    rbtn_perform_time_history.Checked = true;
                //                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                //                }
                //                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM RESPON"))
                //                {
                //                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                //                    rbtn_response_spectrum.Checked = true;
                //                }

                //            }
                //            //AST_DOC.DynamicAnalysis
                //        }
                //    }
                //}
                #endregion Data Open
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
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


            ACad.MassUnit = (ACLSS.EMassUnits)cmb_Base_MUnit.SelectedIndex;
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

        private void Load_Selection_2016_04_29(TreeNode tn)
        {
            //tv_loadings.SelectedNode
            Load_Type = "";
            if (tn.Nodes.Count == 0)
            {
                if (tn.Parent != null)
                {
                    if (tn.Parent.Text.StartsWith("JOINT"))
                        Load_Type = "JOINT";
                    if (tn.Parent.Text.StartsWith("MEMBER"))
                        Load_Type = "MEMBER";
                    if (tn.Parent.Text.StartsWith("AREA"))
                        Load_Type = "AREA";
                    if (tn.Parent.Text.StartsWith("FLOOR"))
                        Load_Type = "FLOOR";
                    if (tn.Parent.Text.StartsWith("TEMP"))
                        Load_Type = "TEMP";
                    if (tn.Parent.Text.StartsWith("REPEAT"))
                        Load_Type = "REPEAT";
                    if (tn.Parent.Text.StartsWith("ELEM"))
                        Load_Type = "ELEMENT";
                    if (tn.Parent.Text.StartsWith("SUPPORT"))
                        Load_Type = "SUPPORT";
                }

            }

            if (tn.Parent == null)
            {
                grb_jload.Enabled = true;
                grb_mload.Enabled = true;
                grb_eload.Enabled = true;
                grb_aload.Enabled = true;
                grb_fload.Enabled = true;
                grb_tload.Enabled = true;
                grb_cload.Enabled = true;
                grb_sdload.Enabled = true;


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


                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;

                if (tn.Text.StartsWith("JOINT"))
                {
                    grb_jload.Enabled = true;
                    //grb_mload.Enabled = false;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("MEMBER"))
                {
                    //grb_jload.Enabled = false;
                    grb_mload.Enabled = true;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("SUPPORT"))
                {
                    //grb_jload.Enabled = false;
                    grb_sdload.Enabled = true;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("ELEMENT"))
                {
                    //grb_jload.Enabled = false;
                    grb_eload.Enabled = true;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("AREA"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    grb_aload.Enabled = true;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("FLOOR"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    grb_fload.Enabled = true;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("TEMP"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    //grb_aload.Enabled = false;
                    grb_tload.Enabled = true;
                }
                else if (tn.Text.StartsWith("REP"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    //grb_aload.Enabled = false;
                    grb_rload.Enabled = true;
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


            grb_jload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_mload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_eload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_aload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_fload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_tload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_rload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_sdload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;


            //btn_jload_edit.Enabled = (Current_LoadCase.JointLoads.Count > 0);
            //btn_jload_del.Enabled = (Current_LoadCase.JointLoads.Count > 0);

            //btn_mload_edit.Enabled = (Current_LoadCase.MemberLoads.Count > 0);
            //btn_mload_del.Enabled = (Current_LoadCase.MemberLoads.Count > 0);

            //btn_aload_edit.Enabled = (Current_LoadCase.AreaLoads.Count > 0);
            //btn_aload_del.Enabled = (Current_LoadCase.AreaLoads.Count > 0);

            //btn_tload_edit.Enabled = (Current_LoadCase.TemperatureLoads.Count > 0);
            //btn_tload_del.Enabled = (Current_LoadCase.TemperatureLoads.Count > 0);


            btn_cload_edit.Enabled = (Current_LoadCase.Comb_Loads.Count > 0);
            btn_cload_del.Enabled = (Current_LoadCase.Comb_Loads.Count > 0);

            if (Load_Type == "")
            {
                btn_jload_edit.Enabled = false;
                btn_jload_del.Enabled = false;
                btn_mload_edit.Enabled = false;
                btn_mload_del.Enabled = false;
                btn_eload_edit.Enabled = false;
                btn_eload_del.Enabled = false;
                btn_aload_edit.Enabled = false;
                btn_aload_del.Enabled = false;
                btn_fload_edit.Enabled = false;
                btn_fload_del.Enabled = false;
                btn_tload_edit.Enabled = false;
                btn_tload_del.Enabled = false;

                btn_rload_edit.Enabled = false;
                btn_rload_del.Enabled = false;

                btn_sdload_edit.Enabled = false;
                btn_sdload_del.Enabled = false;



                btn_jload_add.Enabled = true;
                btn_mload_add.Enabled = true;
                btn_eload_add.Enabled = true;
                btn_aload_add.Enabled = true;
                btn_fload_add.Enabled = true;
                btn_tload_add.Enabled = true;
                btn_cload_add.Enabled = true;
                btn_rload_add.Enabled = true;

                btn_sdload_add.Enabled = true;



            }

            if (Load_Type == "JOINT")
            {

                btn_jload_edit.Enabled = true;
                btn_jload_del.Enabled = true;

                grb_jload.Enabled = true;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "MEMBER")
            {

                btn_mload_edit.Enabled = true;
                btn_mload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = true;
                grb_eload.Enabled = false;
                grb_fload.Enabled = false;
                grb_aload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "ELEMENT")
            {

                btn_eload_edit.Enabled = true;
                btn_eload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = true;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "AREA")
            {

                btn_aload_edit.Enabled = true;
                btn_aload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_fload.Enabled = false;
                grb_aload.Enabled = true;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "FLOOR")
            {

                btn_fload_edit.Enabled = true;
                btn_fload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_fload.Enabled = false;
                grb_fload.Enabled = true;
                grb_aload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "TEMP")
            {

                btn_tload_edit.Enabled = true;
                btn_tload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = true;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "REPEAT")
            {

                btn_rload_edit.Enabled = true;
                btn_rload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = true;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "SUPPORT")
            {

                btn_sdload_edit.Enabled = true;
                btn_sdload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = true;
            }
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
                //btn_aload_edit.Enabled = false;
                //btn_aload_del.Enabled = false;
                //btn_aload_add.Enabled = true;

                btn_def_loads_edit.Enabled = false;
                btn_def_loads_del.Enabled = false;
                btn_def_loads_add.Enabled = true;

                btn_lc_add.Enabled = true;
                btn_lc_edit.Enabled = true;
                btn_ldc_del.Enabled = true;

            }

            if (Load_Type != "")
            {

                btn_def_loads_edit.Enabled = true;
                btn_def_loads_del.Enabled = true;
                grb_def_loads.Enabled = true;
            }
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

        private void Save_Data()
        {

            //string fname = Path.Combine(Path.GetDirectoryName(txt_file_name.Text),
            //               Path.GetFileNameWithoutExtension(txt_file_name.Text) + ".TXT");
            string fname = "";

            //if (IsDrawingFileOpen)


            if (!File.Exists(File_Name))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "TEXT Files (*.txt)|*.txt";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                        File_Name = sfd.FileName;
                    else
                        return;
                }
            }

            fname = File_Name;


            #region Chiranjit [2016 04 29]
            Save_Data(fname);

            return;
            #endregion Chiranjit [2016 04 29]

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

                    if(s1 != "" || s2 != "")
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

                    if (LoadCases[i].JointLoads.Count > 0)
                        list.Add(string.Format("JOINT LOAD"));
                    foreach (var item in LoadCases[i].JointLoads)
                    {
                        list.Add(string.Format("{0}", item));

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

            if (MessageBox.Show("ASTRA Data file created as \n\r" + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start(fname);
            }

        }

        public double Seismic_Coeeficient
        {
            get
            {
                return MyList.StringToDouble(txt_seismic_coeeficient.Text, 0.12);
            }
        }
        List<string> Seismic_Load = new List<string>();
        List<string> Seismic_Combinations = new List<string>();
        bool IsSavedData = false;
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
                        s2 = cmb_Const_LUnit.Text;

                    if (s1 != "" || s2 != "")
                        list.Add(string.Format("UNIT {0} {1}", s1, s2));
                }

                for (i = 0; i < tv_constants.Nodes.Count; i++)
                {
                    list.Add(string.Format("MATERIAL CONSTANT"));
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
                if (cmb_Load_LUnit.SelectedIndex != cmb_Const_MUnit.SelectedIndex ||
                    cmb_Load_MUnit.SelectedIndex != cmb_Const_MUnit.SelectedIndex)
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
                        //list.Add(string.Format("{0}", item));


                        if (item.Comment != "")
                        {
                            //list.Add(string.Format("*{0}", item.Comment));
                            if (item.Comment.StartsWith("*"))
                            {
                                list.Add(string.Format("{0}", item.Comment));
                            }
                            else
                            {
                                list.Add(string.Format("*{0}", item.Comment));
                            }


                        }

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
            if (tv == null) return;
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
                //btn_ldc_edit.Enabled = (tv_loads.SelectedNode != null);
                //btn_ldc_del.Enabled = (tv_loads.SelectedNode != null);
                btn_lc_edit.Enabled = (tv_loads.SelectedNode != null);
                btn_lc_del.Enabled = (tv_loads.SelectedNode != null);
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



            btn_lc_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_lc_del.Enabled = (tv_loads.SelectedNode != null);


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

            btn_cload_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_cload_del.Enabled = (tv_loads.SelectedNode != null);


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
                if (tsmi.Name == tsmi_file_open.Name || tsmi.Name == tsb_file_open.Name)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {

                        Drawing_File = ofd.FileName;

                        IsDrawingFileOpen = true;
                        this.Text = "Analysis Input Data File [" + MyStrings.Get_Modified_Path(Drawing_File) + "]";

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
                        this.Text = "Drawing To ASTRA Data [" + MyStrings.Get_Modified_Path(File_Name) + "]";
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
                            this.Text = "Drawing To ASTRA Data [" + MyStrings.Get_Modified_Path(ofd.FileName) + "]";


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
                            this.Text = "Drawing To ASTRA Data [" + MyStrings.Get_Modified_Path(File_Name) + "]";
                            Open_Data_File(File_Name);
                        }

                    }
                }
                #endregion Drawing Open
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

        }

        private void Open_Data_File_2016_04_29(string fn)
        {

            Clear_All();
            ASTRADoc astdoc;
            astdoc = new ASTRADoc(fn);
            ACad.AstraDocument = astdoc;

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

                ACLSS.MemberGroup mg = new ACLSS.MemberGroup();
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
                lcd.JointLoads = item.JointLoadList;
                lcd.MemberLoads = item.MemberLoadList;
                lcd.ElementLoads = item.ElementLoadList;
                lcd.AreaLoads = item.AreaLoadList;
                lcd.FloorLoads = item.FloorLoadList;
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

            this.Text = "Analysis Input Data File [ " + MyStrings.Get_Modified_Path(fn) + " ]";

            rtb_input_file.Lines = File.ReadAllLines(fn);

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
        private void Open_Data_File(string fn)
        {

            Clear_All();
            ASTRADoc astdoc;
            astdoc = new ASTRADoc(fn);
            ACad.AstraDocument = astdoc;


            List<double> floors = astdoc.Joints.Get_Floors();



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

                ACLSS.MemberGroup mg = new ACLSS.MemberGroup();
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

                        //tn.Nodes.Add("JOINT LOAD");
                        //tn = tn.Nodes[tn.Nodes.Count - 1];
                        //foreach (var item1 in item.JointLoads)
                        //{
                        //    tn.Nodes.Add(item1);
                        //}
                        //tn = tn.Parent;


                        tn.Nodes.Add("JOINT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.JointLoads)
                        {
                            if (item1.Comment != "")
                                tn.Nodes.Add(item1 + " " + item1.Comment);
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

            this.Text = "Analysis Input Data File [ " + MyStrings.Get_Modified_Path(fn) + " ]";
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
            if (iApp.IsDemo)
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

            Count_Geometry();
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
                double VV = MyList.StringToDouble(cmb_text_size.Text, 0.2);
                if (VV == 0.0)
                    VV = 0.1;
                return (VV * 0.05);
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

            foreach (var item in VD.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = PP_Text_Size;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = PP_Text_Size;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    vdCircle t = item as vdCircle;
                    t.Radius = PP_Text_Size * 0.3;
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
                    if (iApp.IsDemo)
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
            //MessageBox.Show(HeadsUtils.Constants.BuildType.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (iApp.IsDemo)
            if (iApp.IsDemo)
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
                    //return true;
                    return false;
                }
            }

            return false;
        }

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;


            RunAnalysis();

            AST_DOC = new ASTRADoc(DataFileName);
            AST_DOC_ORG = new ASTRADoc(DataFileName);
            //Load_ASTRA_Data();

            Open_Analysis_Report();
            Open_Data_File(DataFileName);


            if (SeismicLoads == null) SeismicLoads = new List<string>();

            if (Seismic_Coeeficient != 0)
            {
                if (!File.Exists(AST_DOC.AnalysisFileName)) return;
                //Run_Seismic_Analysis();
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

        public void Run_Seismic_Analysis()
        {

            if (MessageBox.Show(this, "Do you want to Proceed for Seismic Load Analysis ?",
                 "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

            //frm_SeismicAnalysis fsa = new frm_SeismicAnalysis(ACad, LoadCases);
            frm_Seismic_Suspension fsa = new frm_Seismic_Suspension(ACad, LoadCases);



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

            tc1_SelectedIndexChanged(sender, e);
            if (tc_parrent.SelectedTab == tab_load_calculations)
            {
                //return;

                if (!File.Exists(uC_CAD_Loading.VDoc.FileName))
                {
                    string ff = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Microwave Tower\Microwave Tower.dwg");
                    if (File.Exists(ff))
                    {
                        uC_CAD_Loading.VDoc.Open(ff);
                        uC_CAD_Loading.VDoc.Redraw(true);
                        VDRAW.vdCommandAction.View3D_VTop(uC_CAD_Loading.VDoc);
                    }
                }
                //splitContainer2.Panel2Collapsed = true;
            }

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
        MCLSS.StructureMemberAnalysis StructureAnalysis { get; set; }
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



        public vdDocument ActiveDoc
        {
            get
            {

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
                else if (tc_docs.SelectedTab == tab_diagram)
                    return diagDoc;

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
                //this.Text = "Analysis Process & Results [" + MyStrings.Get_Modified_Path(value) + "]";
                this.Text = Title + " [" + MyStrings.Get_Modified_Path(value) + "]";
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
            vdSC_frcs.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_frcs.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_ldef.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_ldef.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_mov.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_mov.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_maxf.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_maxf.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
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
                double VV = MyList.StringToDouble(cmb_pp_text_size.Text, 0.0);

                if (VV == 0.0)
                    VV = 0.1;
                return (VV) * 0.05;
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

                if (StructureAnalysis == null)
                {
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
                if (StructureAnalysis == null)
                    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);


                if (cmb_diag_ld_no.Items.Count > 0)
                {
                    if (cmb_diag_ld_no.SelectedIndex == -1)
                        cmb_diag_ld_no.SelectedIndex = 0;
                }
                if (cmb_diag_mem_no.Items.Count > 0)
                {
                    if (cmb_diag_mem_no.SelectedIndex == -1)
                        cmb_diag_mem_no.SelectedIndex = 0;
                }
                Select_Diagram();
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
            if (VDoc == null) return;
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

            lbl_mode_shape.Text = LoadCases[LoadCase - 1].Title;
            lbl_mode_shape.Visible = true;

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

                    //AST_DOC_ORG.MemberLoads.Delete_ASTRAMemberLoad(MainDoc);
                    //AST_DOC_ORG.MemberLoads.DrawMemberLoad(MainDoc, LoadCase);

                    //AST_DOC_ORG.JointLoads.Delete_ASTRAArrowLine(MainDoc);
                    //AST_DOC_ORG.JointLoads.DrawJointLoads(MainDoc, LoadCase);


                    AST_DOC.MemberLoads.Delete_ASTRAMemberLoad(defDoc);
                    AST_DOC.MemberLoads.DrawMemberLoad(defDoc, LoadCase);

                    AST_DOC.JointLoads.Delete_ASTRAArrowLine(defDoc);
                    AST_DOC.JointLoads.DrawJointLoads(defDoc, LoadCase);





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

                MCLSS.AstraBeamMember bm = null;
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
            if (File.Exists(DataFileName))
            {
                File.WriteAllLines(DataFileName, rtb_input_file.Lines);
                Open_Data_File(DataFileName);
                MessageBox.Show("Data File Updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    if (iApp.IsDemo)
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
                System.Diagnostics.Process.Start(Analysis_File_Name);
            }
            else
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region Chiranjit [2016 04 29]
        private void btn_lc_add_Click(object sender, EventArgs e)
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
            if (btn.Name == btn_lc_add.Name)
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
            else if (btn.Name == btn_def_loads_add.Name)
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
                        foreach (var item in fsl.ASTRA_Data)
                        {
                            SeismicLoads.Add(item);
                        }
                        #endregion Seismic

                        break;
                    #endregion All Loads
                }
            }
            #endregion Add Load

            #region Edit Load

            else if (btn.Name == btn_lc_edit.Name)
            {

                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ASTRA_Data = tv_loads.Nodes[CurrentLoadIndex].Text;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
                {
                    tv_loads.Nodes[CurrentLoadIndex].Text = fs.ASTRA_Data;
                    Current_LoadCase.LoadNo = fs.Ld.LoadNo;
                    Current_LoadCase.Title = fs.Ld.Title;
                }
            }
            else if (btn.Name == btn_lc_del.Name)
            {
                try
                {
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            else if (btn.Name == btn_def_loads_edit.Name)
            {
                switch (cmb_def_loads.SelectedIndex)
                {
                    #region All Loads

                    case 0: // Joint Load
                        #region Joint Load

                        //r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loads.SelectedNode.Text);
                        //frmJointLoad fjl = new frmJointLoad(ACad);
                        //if (tn != null)
                        //    fjl.Node = tv_loads.SelectedNode;
                        //fjl.Owner = this;
                        //fjl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        //fjl.ShowDialog();


                        //if (r != -1 && fjl.ASTRA_Data.Count > 0)
                        //{
                        //    LoadCases[CurrentLoadIndex].JointLoads[r] = fjl.ASTRA_Data[0];
                        //}

                        #endregion





                        
                        #region Joint Load

                        MyList ms = new MyList(tv_loads.SelectedNode.Text, '*');

                        for (int i = 0; i < LoadCases[CurrentLoadIndex].JointLoads.Count; i++)
                        {
                            var itm = LoadCases[CurrentLoadIndex].JointLoads[i];

                            if (itm.Value == ms.StringList[0])
                            {
                                r = i;
                                break;
                            }
                        }
                        //r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loads.SelectedNode.Text);


                        frmJointLoad fjl = new frmJointLoad(ACad);
                        if (tn != null)
                            fjl.Node = tv_loads.SelectedNode;
                        fjl.Owner = this;
                        fjl.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                        //fjl.ASTRA_Data.Add(ms.StringList[0]);
                        fjl.ShowDialog();


                        if (r != -1 && fjl.ASTRA_Data.Count > 0)
                        {
                            ms = new MyList(tv_loads.SelectedNode.Text, '*');

                            if (ms.Count > 1)
                            {
                                LoadCases[CurrentLoadIndex].JointLoads[r].Value = ms.StringList[0];
                                LoadCases[CurrentLoadIndex].JointLoads[r].Comment = ms.StringList[1];
                            }
                            else
                            {
                                LoadCases[CurrentLoadIndex].JointLoads[r].Value = ms.StringList[0];
                            }
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
                            SeismicLoads.Add(fsl.ASTRA_Data[0]);
                            //LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fsl.ASTRA_Data[0];
                        }

                        #endregion Seismic
                        break;
                    #endregion All Loads
                }
            }
            #endregion Edit Load

            #region Delete Load
            else if (btn.Name == btn_def_loads_del.Name)
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

        private void btn_update_data_Click(object sender, EventArgs e)
        {
            Save_Data();
        }
        #endregion Chiranjit [2016 04 29]

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
            string drw_path = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Microwave Tower");

            string drw_file = Path.Combine(drw_path, @"Microwave Tower.dwg");

            if (File.Exists(drw_file))
            {
                uC_CAD1.VDoc.Open(drw_file);
                uC_CAD1.VDoc.Redraw(true);
                VDRAW.vdCommandAction.View3D_VTop(uC_CAD1.VDoc);
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


        public string Title
        {
            get
            {
                //return "STEEL_TRUSS_OPEN_WEB_COMPLETE_DESIGN";


                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                {
                    return "DESIGN OF MICROWAVE TOWER [BS]";

                }
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                {
                    return "DESIGN OF MICROWAVE TOWER [LRFD]";

                }
                else
                {
                   return "DESIGN OF MICROWAVE TOWER [IRC]";
                }
                return "DESIGN OF MICROWAVE TOWER [IRC]";
            }
        }

        public List<string> Get_TowerData()
        {
            string file_name = Path.Combine(Application.StartupPath, @"DESIGN\Transmission Tower\Microwave Tower.txt");

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

            //Form1 f = new Form1();

            //f.ShowDialog();

            //return;
            uC_CAD1.View_Buttons = true;
            //CreateStructures();
            //return;

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

                tower_ss.CreateStructures(uC_CAD1.VDoc);

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
        public void CreateStructures()
        {




            double CWN_HGT = 4.95;
            double CWN_BOT_WD = 2.4;

            double BASE_WD = 11.844;


            double LEV1_HGT = 13.31;
            double LEV1_WING_HGT = 1.55;
            double LEV1_WING_BOT_WD = 4.37;
            double LEV1_WING_TOP_WD = 4.23;
            double LEV1_WING_SPAN = 4.531;



            double LEV2_HGT = 4.0;
            double LEV2_WING_HGT = 1.55;
            double LEV2_WING_BOT_WD = 3.53;
            double LEV2_WING_TOP_WD = 3.024;
            double LEV2_WING_SPAN = 3.98;



            double LEV3_HGT = 3.0;
            double LEV3_WING_HGT = 1.55;
            double LEV3_WING_BOT_WD = 2.55;
            double LEV3_WING_TOP_WD = 2.4;
            double LEV3_WING_SPAN = 3.784;



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


            vdDocument doc = uC_CAD1.VDoc;



            vdEntities ents = doc.ActiveLayOut.Entities;


            ents.RemoveAll();

            vdLine ln = new vdLine();

            int i = 0;

            #region Level 1

            #region Level 1
            for (i = 0; i < gpc.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = gpc[i];
                ln.EndPoint = Lev1_BOT_Pts[i];
                ents.AddItem(ln);

               
            }
            #region Level 1 Bottom Horizontal

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


            #region Level 2


            List<gPoint> Lev2_Bot_Pts = new List<gPoint>();

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


            List<gPoint> Lev2_Top_Pts = new List<gPoint>();

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


            #region Level 3


            List<gPoint> Lev3_Bot_Pts = new List<gPoint>();

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


            List<gPoint> Lev3_Top_Pts = new List<gPoint>();

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



            #region Level 2
            for (i = 0; i < Lev3_Top_Pts.Count; i++)
            {
                ln = new vdLine(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = Lev3_Bot_Pts[i];
                ln.EndPoint = Lev3_Top_Pts[i];
                ents.AddItem(ln);

            }
            #region Level 2 Bottom Horizontal

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

            #endregion Level 2



            #region Level 2 Wing

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

            #region Crown

            gPoint CWN_Top = new gPoint();

            hgt = LEV1_HGT + LEV1_WING_HGT + LEV2_HGT + LEV3_HGT + CWN_HGT;

            gp = new gPoint();
            gp.x = BASE_WD / 2;
            gp.y = hgt;
            gp.z = BASE_WD / 2;

 


           for(i = 0; i < Lev3_Top_Pts.Count; i++)
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

        #endregion Create Data

        TowerDesign complete_design = null;
        BridgeMemberAnalysis Truss_Analysis = null;

        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                if (cmb_select_standard.SelectedIndex == 0)
                    return iApp.Tables.BS_SteelBeams;
                if (cmb_select_standard.SelectedIndex == 2)
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
                if (cmb_select_standard.SelectedIndex == 2)
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
            
            Button_Enable_Disable();
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
            else if (m.SectionDetails.SectionName.EndsWith("L"))
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
                if (Complete_Design.Members[indx].SectionDetails.SectionName.StartsWith("UK")) cmb_select_standard.SelectedIndex = 0;
                else if (Complete_Design.Members[indx].SectionDetails.SectionName.StartsWith("I")) cmb_select_standard.SelectedIndex = 1;
                else cmb_select_standard.SelectedIndex = 2;


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



            string WIND_FX = "1 3 38 69 71 97 104 108 125 128 130 141 144 146 161 166 181 183 197 200 202 205 210 217 219 245 ";
            string WIND_FZ = "1 2 37 69 70 97 99 101 125 127 141 143 161 163 181 182 197 199 205 207 217 218 245 ";


            string SEISMIC_FZ1 = "125 127 130 132";
            string SEISMIC_FZ2 = "181 182 183 184";
            string SEISMIC_FZ3 = "205 207 210 212";
            string SEISMIC_FZ4 = "217 218 219 220";



             //247,  143, 248
            if (cmb_no_levels.SelectedIndex == 0)
            {
                END_1 = "140";
                END_2 = "140";
                END_3 = "143";
                END_4 = "143";


                WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 223 ";
                WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 223 ";

            }
            else if (cmb_no_levels.SelectedIndex == 1)
            {
                END_1 = "140 247";
                END_2 = "140 247";
                END_3 = "143 248";
                END_4 = "143 248";

                WIND_FX = "1 3 38 69 71 104 125 140 141 143 156 191 194 196 214 231 247 248 263 305 308 310 337 ";
                WIND_FZ = "1 2 37 69 70 99 125 TO 127 140 143 191 TO 193 212 231 232 247 248 305 TO 307 337 ";
            }




            string WING_FX_COMMENT = string.Format("*Wind Load in FX in all {0} Nodes", MyList.Get_Array_Intiger(WIND_FX).Count);
            string WING_FZ_COMMENT = string.Format("*Wind Load in FZ in all {0} Nodes", MyList.Get_Array_Intiger(WIND_FZ).Count);



            //list.Add(WING_FZ_COMMENT);


            #region All Load Combinations

            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 1"));
            //list.Add(string.Format("Wind Load in FZ in all 30 Nodes"));


            #region LOAD 1 WIND LOAD IN FZ DIRECTIONS


            list.Add(string.Format("LOAD 1 WIND LOAD IN FZ DIRECTIONS"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(WING_FZ_COMMENT);
            list.Add(string.Format("{0} FZ {1:f3}", WIND_FZ, tlc.Wind.Wind_Load_per_Node));


            #endregion Load 5



            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 2"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));



            #region LOAD 2 WIND LOAD IN FX DIRECTION
            list.Add(string.Format("LOAD 2 WIND LOAD IN FX DIRECTION"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));
            #endregion Load 3

            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 13"));
            //list.Add(string.Format("Seismic Load in FZ in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format(""));

            #region LOAD 3 SEISMIC LOAD IN FZ IN ALL 4 NODES AT BOTTOM SUPPORT
            list.Add(string.Format("LOAD 3 Seismic Load in FZ"));
            list.Add(string.Format("JOINT LOAD"));

            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level1 "));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level2"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Level3"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FZ in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FZ {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }

            #endregion Load 3


            //list.Add(string.Format("=============================================================================="));
            //list.Add(string.Format("Load 17"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));

            #region LOAD 4  SEISMIC LOAD IN FX IN ALL 4 NODES AT BOTTOM SUPPORT
            list.Add(string.Format("LOAD 4 SEISMIC LOAD IN FX IN ALL 4 NODES AT BOTTOM SUPPORT"));
            list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Bottom Support"));
            //list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ, tlc.Seismic.Q1));


            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 "));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }
            #endregion Load 17


            //list.Add(string.Format(""));
            //list.Add(string.Format("Load 22"));
            //list.Add(string.Format("Wind Load in FX in all 30 Nodes"));
            //list.Add(string.Format("Seismic Load in FX in all 4 Nodes at Bottom Support"));

            #region LOAD 5 CABLE LOADS IN FX, FY, FZ ON ENDS 1, 2 & 3 ON ALL WING TIPS ONLY

            list.Add(string.Format("LOAD 5 WIND LOAD IN FX & SEISMIC LOAD IN FX"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(WING_FX_COMMENT);
            list.Add(string.Format("{0} FX {1:f3}", WIND_FX, tlc.Wind.Wind_Load_per_Node));

            if (SEISMIC_FZ1 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level1 "));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ1, tlc.Seismic.Q1));
            }
            if (SEISMIC_FZ2 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level2"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ2, tlc.Seismic.Q2));
            }
            if (SEISMIC_FZ3 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Level3"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ3, tlc.Seismic.Q3));
            }
            if (SEISMIC_FZ4 != "")
            {
                list.Add(string.Format("*Seismic Load in FX in all 4 Nodes at Crown Bottom"));
                list.Add(string.Format("{0} FX {1:f3}", SEISMIC_FZ4, tlc.Seismic.Q4));
            }

            #endregion Load 5




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
            else if (mem_grp.Contains("_LEV3_LB")) { tmt = eTowerMemberType.LEVEL3_LOWER_CROSS_BRACINGS; }
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
            else if (mem_grp.Contains("_LEV3_LOWER_BRACINGS")) tmt = "_LEV3_LB";

                
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
            tc_forces.SelectedTab = tab_analysis_results;
            //Write_All_Data();

            Button_Enable_Disable();

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
                tc_forces.SelectedTab = tab_design_results;
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

            //tab_design_results.Focus();
            //tabControl2.SelectedTab = tab_design_results;
            //Write_All_Data();

            Button_Enable_Disable();
            //prss = 4;
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
                sw.WriteLine("\t\t*            ASTRA Pro Release 22            *");
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

                //frm_ViewForces(Truss_Analysis.Analysis.Width, DL_Report_Analysis_File, LL_Report_Analysis_File, (s1 + s2));
                //frm_ViewForces_Load();

                //frm_Pier_ViewDesign_Forces(Total_Report_Analysis_File, s1, s2);
                //frm_ViewDesign_Forces_Load();

            }
            catch (Exception ex) { }
            //Read_All_Data();
            Button_Enable_Disable();
            Show_Total_Weight();
            //Chiranjit [2012 07 13]

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
            string drwg_path = "MICROWAVE_TOWER";
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


    }
   
    
   
    
}
