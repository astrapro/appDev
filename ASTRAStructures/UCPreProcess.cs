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
    public partial class UCPreProcess : UserControl
    {

        public UCPreProcess()
        {
            InitializeComponent();

            for (int i = 1; i < 30; i++)
            {
                cmb_text_size.Items.Add(i);
            }
        }

        
        public IApplication iApp;

        public string FilePath { get { return File_Name; } set { File_Name = value; } }

        public string INPUT_FILE { get { return File_Name; } set { File_Name = value; } }

       public bool IsFlag = false;

        #region Pre Process

        public vdDocument VDoc
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }
        

        public vdDocument ActiveDoc
        {
            get
            {

                //if (tc_parrent.SelectedTab == tab_pre_process)
                //    return VDoc;
                ////if (tc_parrent.SelectedTab == tab_pre_process_sap)
                ////    return UC_SAP.vdoc
                //if (tc_docs.SelectedTab == tab_frcs_doc)
                //    return frcsDoc;
                //else if (tc_docs.SelectedTab == tab_defl_doc)
                //    return defDoc;
                //else if (tc_docs.SelectedTab == tab_max_doc)
                //    return maxDoc;
                //else if (tc_docs.SelectedTab == tab_mov_doc)
                //    return movDoc;
                //else if (tc_docs.SelectedTab == tab_evlp_doc)
                //    return envDoc;
                //else if (tc_docs.SelectedTab == tab_diagram)
                //    return diagDoc;

                return VDoc;

                //if(
            }
        }
        public vdDocument doc
        {
            get
            {
                return VDoc;
            }
        }
        ASTRADoc AST_DOC
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

         int iLoadCase;

        //public string File_Name
        

        bool IsMovingLoad = true;


        string File_Name { get; set; }
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

        public void frmAnalysisWorkspace()
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();

                Drawing_File = "";
                //File_Name = input_file;


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

        
        private void Base_Control_MouseEvent()
        {

            vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            //vdSC_frcs.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            //vdSC_frcs.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            //vdSC_ldef.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            //vdSC_ldef.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            //vdSC_mov.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            //vdSC_mov.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            //vdSC_maxf.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            //vdSC_maxf.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
        }

        public void frmAnalysisWorkspace(string drawing_file, bool IsDrawingFile)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
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

        public string analysis_type { get; set; }

        public string analysis_title { get; set; }



        public void frmAnalysisWorkspace(IApplication app, string menu_name)
        {
            //this.Show
            try
            {
                Project_Type = eASTRADesignType.Structural_Analysis;

                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();
                Drawing_File = "";
                File_Name = "";
                ACad = new ASTRACAD();

                iApp = app;

                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new ACLSS.MemberGroupCollection();
                Materials = new List<MaterialProperty>();

                IsDrawingFileOpen = false;


                MyList ml = new MyList(menu_name, ':');

                //lbl_Title.Text = ml.StringList[1].ToUpper();

                analysis_title = ml.StringList[1];

                analysis_type = ml.StringList[0].ToUpper();
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
                    //if (VD == maxDoc) dgv = dgv_max_frc;


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

        

        private void frmAnalysisWorkspace_Load()
        {
            try
            {
                //this.Text = Title;
                //Set_Project_Name();

                //UC_SAP.SetIApplication(iApp);
                //tc_parrent.TabPages.Remove(tab_pre_process_sap);
                //ucSapPostProcess1.SetIApplication(iApp, UC_SAP);
                //tc_parrent.TabPages.Remove(tab_post_procees_sap);

                vdScrollableControl1.BaseControl.KeyUp += new KeyEventHandler(BaseControl_KeyUp);

                for (int i = 1; i < 100; i++)
                {
                    cmb_text_size.Items.Add(i.ToString());
                }
                Load_Initials();


               var ml = new MyList(analysis_type, '_');


                int anaType = ml.GetInt(1);

                cmb_text_size.SelectedIndex = 0;
            }
            catch (Exception exx) { }
        }

        public void Load_Initials()
        {
            TabPage tb = tc_prp_main.SelectedTab;
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
            cmb_Base_MUnit.SelectedIndex = 2;
            cmb_Const_LUnit.SelectedIndex = 0;
            cmb_Const_MUnit.SelectedIndex = 2;
            cmb_Load_LUnit.SelectedIndex = 0;
            cmb_Load_MUnit.SelectedIndex = 2;

            cmb_Prop_LUnit.SelectedIndex = 0;


            for (int i = 0; i < tc_prp_panel.TabPages.Count; i++)
            {
                tc_prp_panel.TabPages[i].Text = "";
            }

            if (ACad == null)
            {
                frmAnalysisWorkspace();
            }
            ACad.Document = VDoc;

            //IsDrawingFileOpen = true;
            //Drawing_File = Path.Combine(user_path, "STRUCTURE.VDML");
            //Drawing_File = Path.Combine(user_path, "");
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

            tc_prp_main.SelectedTab = tb;
            Button_Enable_Disable();

            Count_Geometry();

            Tab_Selection();

            if (cmb_text_size.SelectedIndex == -1)
            {
                if (cmb_text_size.Items.Count > 0)
                    cmb_text_size.SelectedIndex = 0;
            }
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
            if (VDoc == null) return;
            if (File.Exists(Drawing_File))
            {
              
                 VDoc.Open(Drawing_File);

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
            else if (tsb.Name == tsb_Edit.Name)
            {
                tsmi_Edit_Click(sender, e);
            }
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
        private void tsmi_Edit_Click(object sender, EventArgs e)
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
        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load_Initials();

            //if (tc_parrent.SelectedTab == tab_pre_process)
            //{
            if (!IsFlag)
            {
                Load_Initials();
                IsFlag = true;
            }
            //}
            //else if (tc_parrent.SelectedTab == tab_pre_process_sap)
            //{

            //    if (!IsFlag)
            //    {
            //        //UC_SAP.Load_Initials();
            //        //UC_SAP.Load_Initials();
            //        UC_SAP.Load_SAP_Events();


            //        UC_SAP.Open_Data_File(File_Name);
            //        IsFlag = true;
            //    }
            //    UC_SAP.Tab_Selection();
            //}
            //else if (tc_parrent.SelectedTab == tab_post_procees_sap)
            //{

            //    if (!IsFlag)
            //    {
            //        //UC_SAP.Load_Initials();
            //        //UC_SAP.Load_Initials();
            //        //ucSapPostProcess1.Load_SAP_Events();


            //        UC_SAP.Open_Data_File(File_Name);
            //        IsFlag = true;
            //    }
            //    ucSapPostProcess1.DataFileName = File_Name;
            //    ucSapPostProcess1.UCSapPostProcess_Load(sender, e);
            //    ucSapPostProcess1.Tab_Post_Selection();
            //}




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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                    // fs.Owner = this;
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
                // fs.Owner = this;

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
                // fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_cable;
                fs.ASTRA_Data = (tv_mem_spec_cable.SelectedNode.Text);
                fs.ShowDialog();
            }

            else if (btn.Name == btn_spec_add_truss.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
                fs.ShowDialog();

                txt_dynamic.Lines = fs.ASTRA_Data.ToArray();
            }
            else if (rbtn_response_spectrum.Checked)
            {
                frmResponse fs = new frmResponse(ACad);
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                    if (erd == System.Windows.Forms.DialogResult.Yes)
                    {
                        Save_Data();
                    }
                }
            }
            //MyList.
            return true;
        }

        private bool Save_Data()
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
                        return false;
                }
            }

            fname = File_Name;


            #region Write Curve Coordinate
            
            
            Save_Data(fname);

            #endregion Write Curve Coordinate






            return true;

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

            txt_input_file.Lines = list.ToArray();

            AST_DOC = new ASTRADoc(fname);
            File.WriteAllLines(fname, list.ToArray());

            if (MessageBox.Show("ASTRA Data file created as \n\r" + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start(fname);
            }

        }

        private static string Get_Straight_File(string fname)
        {

            string st_file = Path.Combine(Path.GetDirectoryName(fname), "TempAnalysis");
            if (!Directory.Exists(st_file)) Directory.CreateDirectory(st_file);
            st_file = Path.Combine(st_file, "TempAnalysis.txt");
            return st_file;
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
        private bool Save_Data(string fname)
        {
            return Save_Data(fname, false);
        }
        private bool Save_Data(string fname, bool IsCurve)
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

            txt_input_file.Lines = list.ToArray();

            AST_DOC = new ASTRADoc(fname);
            File.WriteAllLines(fname, list.ToArray());

            IsSavedData = true;

            if (!IsCurve)
            {
                //if (MessageBox.Show("ASTRA Data file created as \n\r" + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
                if (MessageBox.Show(this, "Data file Saved as " + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    //System.Diagnostics.Process.Start(fname);
                }
            }
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
                            else
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

            txt_load_gen.Text = astdoc.LOAD_GENERATION.ToString();
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
                txt_input_file.Lines = File.ReadAllLines(fn);
            else
                txt_input_file.Text = "";

            if (File.Exists(MyStrings.Get_LL_TXT_File(fn)))
            {
                rtb_ll_txt.Lines = File.ReadAllLines(MyStrings.Get_LL_TXT_File(fn));
            }


            //if (File.Exists(Analysis_File_Name))
            //{
            //    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
            //    StructureAnalysis = null;
            //    ld = null;
            //    Select_Steps();
            //}

            Tab_Selection();


        }

        private void tsmi_file_save_Click(object sender, EventArgs e)
        {
            ToolStripItem tsmi = sender as ToolStripItem;
            //if (tsmi.Name == tsmi_file_save_as.Name)
            //{
            //    using (SaveFileDialog sfd = new SaveFileDialog())
            //    {
            //        sfd.Filter = "Text Data File(*.txt)|*.txt";
            //        if (sfd.ShowDialog() != DialogResult.Cancel)
            //        {
            //            DataFileName = sfd.FileName;
            //            File.WriteAllLines(DataFileName, txt_input_file.Lines);
            //        }
            //        else
            //            return;
            //    }
            //}
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
            //this.Close();
        }

        private void btn_elem_props_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_elem_props_add.Name)
            {
                frmElementProp fs = new frmElementProp(ACad);
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
        public void Delete_Layer_Items(vdDocument doc, string LayerName)
        {
            foreach (var item in doc.ActionLayout.Entities)
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
            doc.Redraw(true);
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

                //if (tc_parrent.SelectedTab == tab_post_procees)
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
                    t.Radius = Text_Size * 0.7;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                // fs.Owner = this;
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
                //flc.Owner = this;
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
                        //fjl.Owner = this;
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
                        //fjw.Owner = this;
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
                        //fsd.Owner = this;
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
                        //fml.Owner = this;
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
                        //fel.Owner = this;
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
                        //frl.Owner = this;
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
                        //fal.Owner = this;
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
                        //ffl.Owner = this;
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
                        //ftl.Owner = this;
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
                        //fcl.Owner = this;
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
                        //fsl.Owner = this;
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
                //fs.Owner = this;
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
                        //fjl.Owner = this;
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
                        //fjw.Owner = this;
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
                        //fsd.Owner = this;
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
                        //fml.Owner = this;
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
                        //fel.Owner = this;
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
                        //frl.Owner = this;
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
                        //fal.Owner = this;
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
                        //ffl.Owner = this;
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
                        //ftl.Owner = this;
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
                        //fcl.Owner = this;
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
                        //fsl.Owner = this;
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

        public void DisposeDoc(vdDocument dc)
        {
            try
            {
                dc.Dispose();
            }
            catch (Exception ex) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (VDoc == null) return;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
        }
    }
}
