using System;
using System.Collections.Generic;
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
using HEADSNeed.ASTRA.CadToAstra;

using VDRAW = VectorDraw.Professional.ActionUtilities;

//using MovingLoadAnalysis;
//using MovingLoadAnalysis.DataStructure;


namespace AstraAccess
{
    public partial class frm_DrawingToData : Form
    {
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
        private vdScrollableControl.vdScrollableControl vdScrollableControl1;

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
        public List<LoadCaseDefinition> LoadCases { get; set; }


        public List<MovingLoadData> MovingLoads { get; set; }
        public List<LiveLoad> LL_Definition { get; set; }
        public List<MaterialProperty> Materials { get; set; }
        MemberGroupCollection Groups { get; set; }


        string Drawing_File { get; set; }
        string DataFileName { get; set; }

        public frm_DrawingToData(string input_file)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;
            
            try
            {
                InitializeComponent();
                vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

                Drawing_File = "";
                DataFileName = input_file;


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

        public frm_DrawingToData(string drawing_file, bool IsDrawingFile)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
                InitializeComponent();
                vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

                IsDrawingFileOpen = IsDrawingFile;

                if (IsDrawingFile)
                {
                    Drawing_File = drawing_file;
                    DataFileName = "";
                }
                else
                {
                    Drawing_File = "";
                    DataFileName = drawing_file;
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


        public frm_DrawingToData()
        {
            //this.Show
            try
            {
                InitializeComponent();
                vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

                Drawing_File = "";
                DataFileName = "";
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
            //sc2.Panel2Collapsed = true;
            sc2.Panel2Collapsed = (tc1.SelectedTab == tab_file_open);


            sc1.Panel2Collapsed = (tc1.SelectedTab == tab_file_open);

            //if (tc1.SelectedTab == tab_file_open)
            //    Show_Panel(tab1_inputs);


            if (tc1.SelectedTab == tab_geom)
                Show_Panel(tab1_geom);
            else if (tc1.SelectedTab == tab_props)
                Show_Panel(tab1_props);
            else if (tc1.SelectedTab == tab_constants)
                Show_Panel(tab1_const);
            else if (tc1.SelectedTab == tab_supports)
                Show_Panel(tab1_supports);
            else if (tc1.SelectedTab == tab_loads)
                Show_Panel(tab1_loads);
            else if (tc1.SelectedTab == tab_mov_load)
                Show_Panel(tab1_moving);
            else if (tc1.SelectedTab == tab_dynamic)
                Show_Panel(tab1_dynamic);
            else if (tc1.SelectedTab == tab_ana_spec)
                Show_Panel(tab1_ana_spec);
        }
        public void Show_Panel(TabPage tp)
        {
            tc2.TabPages.Clear();
            tc2.TabPages.Add(tp);
        }

        public void ShowMemberOnGrid(vdLine vLine)
        {
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                if (AST_DOC.Members[i].StartNode.Point == vLine.StartPoint &&
                    AST_DOC.Members[i].EndNode.Point == vLine.EndPoint)
                {

                    DataGridView dgv = dgv_members;
                   
                    vdDocument VD = VDoc;
                
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
            int jNo = MyList.StringToInt(vtxt.TextString, -1);
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
            int mNo = MyList.StringToInt(vtxt.TextString, -1);
            if (mNo == -1) return;


            DataGridView dgv = dgv_members;
           
            vdDocument VD = VDoc;
          

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
            vdDocument VD = VDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;

            for (int i = 0; i < VD.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = VD.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    txt.Height = txtSize / 10.0d;
                    txt.Update();
                }
            }
            VD.Redraw(true);
        }


        private void frm_DrawingToData_Load(object sender, EventArgs e)
        {
            //MyList
            for (int i = 1; i < 100; i++)
            {
                cmb_text_size.Items.Add(i.ToString());
            }


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


            for (int i = 0; i < tc2.TabPages.Count; i++)
            {
                tc2.TabPages[i].Text = "";
            }


            ACad.Document = VDoc;

           

            if (IsDrawingFileOpen)
            {
                if (File.Exists(Drawing_File))
                {
                    Open_Drawing_File();
                    tc1.SelectedTab = tab_geom;
                }
                else
                    sc2.Panel1Collapsed = true;
            }
            else
            {
                if (File.Exists(DataFileName))
                {
                    Open_Data_File(DataFileName);
                    //VDRAW.vdCommandAction.View3D_VTop(VDoc);

                }
                else
                    sc2.Panel1Collapsed = true;
                tc1.SelectedTab = tab_geom;
                tc1.SelectedTab = tab_file_open;
            }


            Button_Enable_Disable();

            Count_Geometry();

            timer1.Start();

            cmb_text_size.SelectedIndex = 0;
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

                tc1.SelectedTab = tab_geom;

            }
        }

        private void Clear_All()
        {

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
            tv_loadings.Nodes.Clear();
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
                        if (VD.SaveAs(sfd.FileName))
                        {
                            MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            vdDocument VD = VDoc;

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


                if (tc1.SelectedTab != tab_props || e.Button == MouseButtons.Right)
                {
                    Delete_Layer_Items("Selection");
                    //return;
                }
                
                if (tc1.SelectedTab != tab_geom)
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


                vdSelection gripset = GetGripSelection(false);
                vd3DFace dFace;
                vdLine ln;
                foreach (vdFigure fig in gripset)
                {
                    if (fig is vdText)
                    {
                        ShowMemberOnGrid(fig as vdText);
                        this.VDoc.Redraw(true);
                        break;
                    }
                    if (fig is vdPolyline)
                    {
                        ShowElementOnGrid(fig as vdPolyline);
                        this.VDoc.Redraw(true);
                        break;
                    }

                    if (fig is ASTRASupportFixed)
                    {
                        ASTRASupportFixed asf = fig as ASTRASupportFixed;

                        ShowNodeOnGrid(asf.Origin);
                        this.VDoc.Redraw(true);
                        break;
                    }

                    if (fig is ASTRASupportPinned)
                    {
                        ASTRASupportPinned asp = fig as ASTRASupportPinned;

                        ShowNodeOnGrid(asp.Origin);
                        this.VDoc.Redraw(true);
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
                        this.VDoc.Redraw(true);
                    }
                    else
                    {
                        ShowMemberOnGrid(ln);
                        this.VDoc.Redraw(true);
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
            TreeNode tn = tv_loadings.SelectedNode;

           
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
                if (tv_loadings.Nodes.Count > 0)
                    tn = tv_loadings.Nodes[0];
            }
            #region Define Load Case
            if (btn.Name == btn_ldc_add.Name)
            {
                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
                {
                    tv_loadings.Nodes.Add(fs.ASTRA_Data);
                    LoadCases.Add(fs.Ld);
                }
            }
            else if (btn.Name == btn_ldc_edit.Name)
            {

                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ASTRA_Data = tv_loadings.Nodes[CurrentLoadIndex].Text;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
                {
                    tv_loadings.Nodes[CurrentLoadIndex].Text = fs.ASTRA_Data;
                    Current_LoadCase.LoadNo = fs.Ld.LoadNo;
                    Current_LoadCase.Title = fs.Ld.Title;
                }
            }
            else if (btn.Name == btn_ldc_del.Name)
            {
                try
                {
                    tv_loadings.Nodes.RemoveAt(CurrentLoadIndex);
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

                int r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loadings.SelectedNode.Text);
                frmJointLoad fs = new frmJointLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loadings.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loadings.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }
               
            }
            else if (btn.Name == btn_jload_del.Name)
            {
                //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
                tv_loadings.SelectedNode.Parent.Nodes.Remove(tv_loadings.SelectedNode);
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

                int r = LoadCases[CurrentLoadIndex].SupportDisplacementLoads.IndexOf(tv_loadings.SelectedNode.Text);
                frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
                if (tn != null)
                    fs.Node = tv_loadings.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loadings.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fs.ASTRA_Data[0];
                }

            }
            else if (btn.Name == btn_sdload_del.Name)
            {
                //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
                tv_loadings.SelectedNode.Parent.Nodes.Remove(tv_loadings.SelectedNode);
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

                int r = LoadCases[CurrentLoadIndex].MemberLoads.IndexOf(tv_loadings.SelectedNode.Text);
                frmMemberLoad fs = new frmMemberLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loadings.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loadings.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].MemberLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_mload_del.Name)
            {
                tv_loadings.SelectedNode.Parent.Nodes.Remove(tv_loadings.SelectedNode);
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

                int r = LoadCases[CurrentLoadIndex].ElementLoads.IndexOf(tv_loadings.SelectedNode.Text);
                frmElementLoad fs = new frmElementLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loadings.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loadings.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].ElementLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_eload_del.Name)
            {
                tv_loadings.SelectedNode.Parent.Nodes.Remove(tv_loadings.SelectedNode);
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

                int r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loadings.SelectedNode.Text);
                frmAreaLoad fs = new frmAreaLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loadings.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loadings.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_aload_del.Name)
            {
                tv_loadings.SelectedNode.Parent.Nodes.Remove(tv_loadings.SelectedNode);
            }
            #endregion Define Area Load

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

                int r = LoadCases[CurrentLoadIndex].TemperatureLoads.IndexOf(tv_loadings.SelectedNode.Text);
                frmTempLoad fs = new frmTempLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loadings.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loadings.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].TemperatureLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_tload_del.Name)
            {
                tv_loadings.SelectedNode.Parent.Nodes.Remove(tv_loadings.SelectedNode);
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
                fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
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

                    tn = tv_loadings.SelectedNode.Parent;


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
                    tv_loadings.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Repeat Load

            #region Define Combination Load
            else if (btn.Name == btn_cload_add.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.Comb_Loads = fs.LDC;

                    LoadCases.Add(Ld);

                    tv_loadings.Nodes.Add(Ld.LoadNo + " : " + Ld.Title);

                    Ld.Comb_Loads.Set_Combination();
                    for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                    {
                        tv_loadings.Nodes[tv_loadings.Nodes.Count - 1].Nodes.Add(Ld.Comb_Loads[i]);
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



                    tv_loadings.Nodes[CurrentLoadIndex].Text = (Ld.LoadNo + " : " + Ld.Title);

                    tv_loadings.Nodes[CurrentLoadIndex].Nodes.Clear();
                    Ld.Comb_Loads.Set_Combination();
                    for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                    {
                        tv_loadings.Nodes[CurrentLoadIndex].Nodes.Add(Ld.Comb_Loads[i]);
                    }
                }
            }
            else if (btn.Name == btn_cload_del.Name)
            {
                try
                {
                    tv_loadings.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Combination Load

            grb_cload.Enabled = (tv_loadings.Nodes.Count > 0);

            grb_jload.Enabled = (tv_loadings.Nodes.Count > 0);
            grb_mload.Enabled = (tv_loadings.Nodes.Count > 0);
            grb_aload.Enabled = (tv_loadings.Nodes.Count > 0);
            grb_tload.Enabled = (tv_loadings.Nodes.Count > 0);
            grb_rload.Enabled = (tv_loadings.Nodes.Count > 0);

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
                if (btn.Name == btn_file_open.Name)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        txt_file_name.Text = ofd.FileName;

                        Drawing_File = txt_file_name.Text;

                        string fn = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                        ASTRADoc astdoc;
                        if (File.Exists(fn))
                        {
                            astdoc = new ASTRADoc(fn);
                            ACad.AstraDocument = astdoc;

                            AST_DOC.Members.DrawMember(VDoc);
                            SetGridWithNode();
                            SetGridWithMember();
                        }
                        else
                            Open_Drawing_File();

                    }
                }
                #endregion Drawing Open
                #region Data Open
                else if (btn.Name == btn_data_open.Name)
                {
                    ofd.Filter = "ASTRA Data Files (*.txt)|*.txt";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        txt_file_name.Text = ofd.FileName;

                        Drawing_File = txt_file_name.Text;

                        string fn = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                        ASTRADoc astdoc;
                        if (File.Exists(fn))
                        {
                            astdoc = new ASTRADoc(fn);
                            ACad.AstraDocument = astdoc;

                            AST_DOC.Members.DrawMember(VDoc);
                            AST_DOC.Elements.DrawElements(VDoc);
                            AST_DOC.Supports.DrawSupport(VDoc);

                            chk_dynamic_analysis.Checked = false;
                            rbtn_perform_eigen.Checked = true;
                            txt_dynamic.Text = "";

                            cmb_structure_type.SelectedItem = AST_DOC.StructureType;
                            txtUserTitle.Text = AST_DOC.UserTitle;


                            cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;
                            cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;


                            cmb_Prop_LUnit.SelectedIndex = (int)AST_DOC.Prop_LengthUnit;
                            cmb_Load_MUnit.SelectedIndex = (int)AST_DOC.Load_MassUnit;
                            cmb_Load_LUnit.SelectedIndex = (int)AST_DOC.Load_LengthUnit;




                            SetGridWithNode();
                            SetGridWithMember();
                            //SetGridWithElement();

                            Groups.Clear();
                            tv_mem_grps.Nodes.Clear();
                            foreach (var item in AST_DOC.MemberGroups)
                            {
                                tv_mem_grps.Nodes.Add(item);
                                
                                MemberGroup mg = new MemberGroup();
                                MyList ml = new MyList(item, ' ');

                                mg.GroupName = ml.StringList[0];
                                mg.MemberNosText = ml.GetString(1);

                                Groups.Add(mg);
                            }

                            tv_mem_props.Nodes.Clear();
                            foreach (var item in AST_DOC.MemberProps)
                            {
                                tv_mem_props.Nodes.Add(item);
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
                            tv_loadings.Nodes.Clear();
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
                                lcd.TemperatureLoads = item.TempLoadList;


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
                                tv_loadings.Nodes.Clear();
                                foreach (var item in LoadCases)
                                {
                                    tv_loadings.Nodes.Add(item.ToString());

                                    tn = tv_loadings.Nodes[tv_loadings.Nodes.Count - 1];


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
                        }
                    }
                }
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


                MyList mlist = new MyList(tn.Text, ' ');


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
                    if (tn.Parent.Text.StartsWith("JOINT"))
                        Load_Type = "JOINT";
                    if (tn.Parent.Text.StartsWith("MEMBER"))
                        Load_Type = "MEMBER";
                    if (tn.Parent.Text.StartsWith("AREA"))
                        Load_Type = "AREA";
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
                    tn.Parent.Text.StartsWith("TEMP") ||
                    tn.Parent.Text.StartsWith("SUPPORT") ||
                    tn.Parent.Text.StartsWith("REPEAT"))
                    tn = tn.Parent;


                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
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

            MyList mlist = new MyList(tn.Text, ':');

            if (mlist.Count > 0)
            {
                //CurrentLoadIndex = mlist.GetInt(0) - 1;
                CurrentLoadIndex = tn.Index;
            }


            grb_jload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_mload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_eload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_aload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
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
                grb_aload.Enabled = true;
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
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = true;
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


            if (!File.Exists(DataFileName))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "TEXT Files (*.txt)|*.txt";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                        DataFileName = sfd.FileName;
                    else
                        return;
                }
            }

            fname = DataFileName;



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
            if (tv_loadings.Nodes.Count > 0)
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
                        list.Add(string.Format("REPEAT LOAD"));
                    foreach (var item in LoadCases[i].RepeatLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }

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

            rtb_inputs.Lines = list.ToArray();


            File.WriteAllLines(fname, list.ToArray());

            if (MessageBox.Show("ASTRA Data file created as \n\r" + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
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
            else if (tv.Name == tv_loadings.Name)
            {
                btn_ldc_edit.Enabled = (tv_loadings.SelectedNode != null);
                btn_ldc_del.Enabled = (tv_loadings.SelectedNode != null);
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

            btn_ldc_edit.Enabled = (tv_loadings.SelectedNode != null);
            btn_ldc_del.Enabled = (tv_loadings.SelectedNode != null);

            if (tv_loadings.SelectedNode == null)
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
                Load_Selection(tv_loadings.SelectedNode);

            btn_cload_edit.Enabled = (tv_loadings.SelectedNode != null);
            btn_cload_del.Enabled = (tv_loadings.SelectedNode != null);


            btn_mov_load_edit.Enabled = (tv_loadings.SelectedNode != null);
            btn_mov_load_del.Enabled = (tv_loadings.SelectedNode != null);

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
                if(tv_max_frc.SelectedNode != null)
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
                        this.Text = "Analysis Input Data File [" + MyList.Get_Modified_Path(Drawing_File) + "]";

                        DataFileName = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                        if (File.Exists(DataFileName))
                        {
                            Open_Data_File(DataFileName);
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
                        DataFileName = ofd.FileName;
                        Drawing_File = "";
                        IsDrawingFileOpen = false;
                        this.Text = "Drawing To ASTRA Data [" + MyList.Get_Modified_Path(DataFileName) + "]";
                        Open_Data_File(DataFileName);
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
                            DataFileName = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");
                            this.Text = "Drawing To ASTRA Data [" + MyList.Get_Modified_Path(ofd.FileName) + "]";


                            if (File.Exists(DataFileName))
                            {
                                Open_Data_File(DataFileName);
                            }
                            else
                                Open_Drawing_File();
                        }
                        else
                        {
                            DataFileName = ofd.FileName;
                            Drawing_File = "";
                            IsDrawingFileOpen = false;
                            this.Text = "Drawing To ASTRA Data [" + MyList.Get_Modified_Path(DataFileName) + "]";
                            Open_Data_File(DataFileName);
                        }

                    }
                }
                #endregion Drawing Open


            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            
        }

        private void Open_Data_File(string fn)
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

                MemberGroup mg = new MemberGroup();
                MyList ml = new MyList(item, ' ');

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
            tv_loadings.Nodes.Clear();
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
                tv_loadings.Nodes.Clear();
                foreach (var item in LoadCases)
                {
                    tv_loadings.Nodes.Add(item.ToString());

                    tn = tv_loadings.Nodes[tv_loadings.Nodes.Count - 1];


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

            this.Text = "Analysis Input Data File [ " + MyList.Get_Modified_Path(fn) + " ]";

            rtb_inputs.Lines = File.ReadAllLines(fn);

            if (File.Exists(MyList.Get_LL_TXT_File(fn)))
            {
                rtb_ll_txt.Lines = File.ReadAllLines(MyList.Get_LL_TXT_File(fn));
            }

            Tab_Selection();
        }

        private void tsmi_file_save_Click(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void tsmi_file_print_Click(object sender, EventArgs e)
        {

            if (HeadsUtils.Constants.BuildType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
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
            if (File.Exists(DataFileName))
            {
                System.Diagnostics.Process.Start(DataFileName);
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
                if(dgv_joints.SelectedCells.Count>0)
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

                jn.NodeNo = MyList.StringToInt(dgv_joints[0, i].Value.ToString(), 0);
                jn.Point.x = MyList.StringToDouble(dgv_joints[1, i].Value.ToString(), 0.0);
                jn.Point.y = MyList.StringToDouble(dgv_joints[2, i].Value.ToString(), 0.0);
                jn.Point.z = MyList.StringToDouble(dgv_joints[3, i].Value.ToString(), 0.0);
                astdoc.Joints.Add(jn);
            }

            foreach (var item in  VDoc.ActionLayout.Entities)
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

                mbr.MemberNo = MyList.StringToInt(dgv_members[0, i].Value.ToString(), 0);
                
                if (dgv_members[1, i].Value.ToString().StartsWith("C"))
                    mbr.MemberType = MembType.CABLE;
                else if (dgv_members[1, i].Value.ToString().StartsWith("T"))
                    mbr.MemberType = MembType.TRUSS;
                else 
                    mbr.MemberType = MembType.BEAM;

                mbr.StartNode.NodeNo = MyList.StringToInt(dgv_members[2, i].Value.ToString(), 0);



                mbr.EndNode.NodeNo = MyList.StringToInt(dgv_members[3, i].Value.ToString(), 0);
              

                astdoc.Members.Add(mbr);
            }
            astdoc.Members.CopyJointCoordinates(astdoc.Joints);



            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Members" || vf.Layer.Name == "0" )
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

            TreeNode tn = tv_loadings.SelectedNode;

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
                if (tv_loadings.SelectedNode.Text.StartsWith("JOINT"))
                {
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                else if (tv_loadings.SelectedNode.Text.StartsWith("MEMBER"))
                {
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("COMB"))
                {
                    MyList mlist = new MyList(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                }
                if (tn.Text.StartsWith("REPEAT"))
                {
                    MyList mlist = new MyList(kStr, ' ');
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
                elm.ElementNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                elm.Node1.NodeNo = MyList.StringToInt(DGV[1, i].Value.ToString(), 0);
                elm.Node2.NodeNo = MyList.StringToInt(DGV[2, i].Value.ToString(), 0);
                elm.Node3.NodeNo = MyList.StringToInt(DGV[3, i].Value.ToString(), 0);
                elm.Node4.NodeNo = MyList.StringToInt(DGV[4, i].Value.ToString(), 0);

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
                return (cmb_text_size.SelectedIndex + 1) * 0.1;
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
                    vdCircle t = item as vdCircle;
                    t.Radius = Text_Size * 0.3;
                    t.Update();
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

                //if (tc4.SelectedIndex == 1)
                //    VD = defDoc;


                for (int i = 0; i < VD.Layers.Count; i++)
                {
                    if (VD.Layers[i].Name.ToLower().StartsWith(chk.Text.Substring(0, 4).ToLower()))
                    {
                        VD.Layers[i].On = chk.Checked;
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
            try
            {
                double d = double.Parse(cmb_text_size.Text);
                SetTextSize(d * 0.50);
            }
            catch (Exception ex)
            { }
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

            MyList MS = new MyList(memText,';');

            foreach (var item in MS.StringList)
            {
                mbrs.AddRange(MyList.Get_Array_Intiger(item).ToArray());
            }


            if (mbrs.Count == 0)
            {
                MyList ml = new MyList(memText, ' ');
                string mm = "";
                for (int i = 0; i < tv_mem_grps.Nodes.Count; i++)
                {
                    mm = tv_mem_grps.Nodes[i].Text;

                    if (mm.StartsWith(ml.StringList[0]))
                    {
                        mm = mm.Replace(ml.StringList[0], "");
                        mbrs = MyList.Get_Array_Intiger(mm);
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
                    if (VDoc.SaveAs(sfd.FileName))
                    {
                        MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        bool FirstOpen = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            VDRAW.vdCommandAction.View3D_VTop(VDoc);
            VDRAW.vdCommandAction.View3D_Vrot(VDoc);
        }
        private void tv_mem_props_Click(object sender, EventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (tv != null)
            {
                if (tv.SelectedNode != null)
                    ShowMember(tv.SelectedNode.Text, Text_Size * 0.3);
            }
        }

        private void InitializeComponent()
        {
            this.vdScrollableControl1 = new vdScrollableControl.vdScrollableControl();
            this.SuspendLayout();
            // 
            // vdScrollableControl1
            // 
            this.vdScrollableControl1.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl1.Location = new System.Drawing.Point(16, 24);
            this.vdScrollableControl1.Name = "vdScrollableControl1";
            this.vdScrollableControl1.ShowLayoutPopupMenu = true;
            this.vdScrollableControl1.Size = new System.Drawing.Size(449, 402);
            this.vdScrollableControl1.TabIndex = 0;
            // 
            // frm_DrawingToData
            // 
            this.ClientSize = new System.Drawing.Size(477, 426);
            this.Controls.Add(this.vdScrollableControl1);
            this.Name = "frm_DrawingToData";
            this.ResumeLayout(false);

        }

    }
}
