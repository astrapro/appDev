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
    public partial class UCPostProcess : UserControl
    {

        public IApplication iApp;

        public string FilePath { get; set; }

        public string INPUT_FILE { get; set; }

        public UCPostProcess()
        {
            InitializeComponent();

            for (int i = 1; i < 30; i++)
            {
                cmb_pp_text_size.Items.Add(i);
            }
            tc_pp_main.TabPages.Remove(tab_envelop);
        }


        #region Post Process

        string file_name = "";
        ASTRADoc astDoc;
        MCLSS.StructureMemberAnalysis StructureAnalysis;
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

        ASTRADoc AST_DOC_ORG;

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
                //if (tc_parrent.SelectedTab == tab_pre_process_sap)
                //    return UC_SAP.vdoc
                if (tc_docs.SelectedTab == tab_defl_doc)
                    return defDoc;
                else if (tc_docs.SelectedTab == tab_max_doc)
                    return maxDoc;
                else if (tc_docs.SelectedTab == tab_mov_doc)
                    return movDoc;
                else if (tc_docs.SelectedTab == tab_evlp_doc)
                    return envDoc;
                else if (tc_docs.SelectedTab == tab_diagram)
                    return diagDoc;

                return frcsDoc;
                //if(
            }
        }
        public vdDocument VDoc
        {
            get
            {
                return ActiveDoc;
            }
        }
        public vdDocument doc
        {
            get
            {
                return ActiveDoc;
                //return defDoc;
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
                file_name = value;
            }
        }

        bool IsMovingLoad = true;

        ASTRADoc AST_DOC;

        public bool IsFlag = false;
        public void Load_Initials(string file_name)
        {
            TabPage tb = tc_pp_main.SelectedTab;
            File_Name = file_name;

            StructureAnalysis = null;
            ld = null;
            AST_DOC = new ASTRADoc(file_name);
            AST_DOC_ORG = new ASTRADoc(file_name);

            PP_Tab_Selection();

            if(cmb_pp_text_size.SelectedIndex == -1)
            {
                if (cmb_pp_text_size.Items.Count > 0)
                    cmb_pp_text_size.SelectedIndex = 0;
            }
            tc_pp_main.SelectedTab = tb;
            tc_pp_main_SelectedIndexChanged(null, null);
        }
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


        #region CAD Methods
        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {

            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
            //{
            //    //gripset = GetGripSelection(false);
            //    vdSelection gripset = GetGripSelection(false);

            //    if (gripset == null) return;
            //    gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            //    //activeDragDropControl = this.vdScrollableControl1;
            //    int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
            //    //activeDragDropControl = null;
            //    return;
            //}
            if (e.Button != MouseButtons.Right) return;
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0) return;
            //if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count != 1) return;

            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
            //{
            //    //gripset = GetGripSelection(false);
            //    vdSelection gripset = GetGripSelection(false);
            //    if (gripset == null) return;
            //    gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            //    //activeDragDropControl = this.vdScrollableControl1;
            //    int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
            //    //activeDragDropControl = null;
            //}
            //else
            //{
            //    //if (BaseAction.PressedKeyCode != Keys.None) return; //6011 correction
            //    //MainForm parent = this.MdiParent as MainForm;
            //    //parent.commandLine.PostExecuteCommand("");
            //}
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

                //vdSelection gripset = GetGripSelection(false);
                //vd3DFace dFace;
                //vdLine ln;

                //vdDocument VD = ActiveDoc;
                //foreach (vdFigure fig in gripset)
                //{
                //    if (fig is vdText)
                //    {
                //        ShowMemberOnGrid(fig as vdText);
                //        VD.Redraw(true);
                //        break;
                //    }
                //    if (fig is vdPolyline)
                //    {
                //        ShowElementOnGrid(fig as vdPolyline);
                //        VD.Redraw(true);
                //        break;
                //    }

                //    if (fig is ASTRASupportFixed)
                //    {
                //        ASTRASupportFixed asf = fig as ASTRASupportFixed;

                //        ShowNodeOnGrid(asf.Origin);
                //        VD.Redraw(true);
                //        break;
                //    }

                //    if (fig is ASTRASupportPinned)
                //    {
                //        ASTRASupportPinned asp = fig as ASTRASupportPinned;

                //        ShowNodeOnGrid(asp.Origin);
                //        VD.Redraw(true);
                //        break;
                //    }


                //    ln = fig as vdLine;

                //    if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
                //    {
                //    }
                //    if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Left)
                //    {
                //    }
                //    if (ln == null)
                //    {
                //        dFace = fig as vd3DFace;
                //        ShowElementOnGrid(dFace);
                //        VD.Redraw(true);
                //    }
                //    else
                //    {
                //        ShowMemberOnGrid(ln);
                //        VD.Redraw(true);
                //    }
                //}

                //gripset.RemoveAll();
            }
            catch (Exception exx) { }
        }
        #endregion Cad Methods


        private void Base_Control_MouseEvent()
        {

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



            //VDoc.Palette.Background = Color.White;
            ////DefDoc.Palette.Background = Color.White;   +
            //astDoc = AST_DOC;
            //AST_DOC.MemberProperties.CopyMemberIncidence(astDoc.Members);
            //AST_DOC.Members.DrawMember(VDoc, Text_Size);
            ////AST_DOC.Members.DrawMember(VDoc);
            //AST_DOC.Elements.DrawElements(VDoc);
            //AST_DOC.Supports.DrawSupport(VDoc);
            //VDoc.Redraw(true);

            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
            //SetGridWithNode();
            //SetGridWithMember();
            //SetGridWithElement();
            //Set_Loadings();
            //chk_joints.Checked = true;
            //chk_mems.Checked = true;
            //chk_elems.Checked = true;
            //chk_suprts.Checked = true;

            //if (AST_DOC.Elements.Count > 0)
            //    chk_elems.Checked = true;

            //for (int i = 1; i < 100; i++) cmb_text_size.Items.Add(i);

            //cmb_text_size.SelectedIndex = 1;
            //timer1.Start();
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
            else if (tsb.Name == tsb_pp_Edit.Name)
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
                            System.Environment.SetEnvironmentVariable("OPENFILE", sfd.FileName);
                            //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                            System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                        }
                    }
                }
            }
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
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
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
                //SetTextSize(Text_Size);
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
        }

        public void PP_Tab_Selection()
        {
            #region STRC
            #endregion STRC
            if (StructureAnalysis == null)
            {
                StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(Analysis_File_Name);

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
                if (StructureAnalysis.list_plates == null)
                    StructureAnalysis.list_plates = new List<MCLSS.AstraPlateMember>();
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


             
            if (tc_pp_main.SelectedTab == tab_load_deflection)
            {
                //if (StructureAnalysis == null)
                //{
                //    //StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                //    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(Analysis_File_Name);
                //}
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
                    //if (StructureAnalysis == null)
                    //{
                    //    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    //}
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

                



                if (tc_pp_main.SelectedTab == tab_max_force)
                    PP_Show_Panel(tab1_max_force);
                else
                    PP_Show_Panel(tab1_forces);


            }
            else if (tc_pp_main.SelectedTab == tab_envelop)
            {
                //if (StructureAnalysis == null)
                //{
                //    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                //}
                //tc4.SelectedIndex = 2;
            }
            else if (tc_pp_main.SelectedTab == tab_diagram)
            {
                //if (StructureAnalysis == null)
                //{
                //    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                //    //StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(Analysis_File_Name);
                //}

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
            //timer1.Stop();
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
            if (doc != null)
            {
                doc.Redraw(true);
                MainDoc.Redraw(true);
            }
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
                AST_DOC.Joints.DrawJointsText(defDoc, PP_Text_Size);
                AST_DOC.Members.DrawMember(defDoc, PP_Text_Size);
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

        public double Curve_Radius = 0.0;

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
                            vl1 = new VehicleLoad(movDoc, movDoc, load_data[j], Curve_Radius);

                            //vl1.TotalLength = MyList.StringToDouble(txt_Ana_L.Text, 0.0);

                            //vl1.Radius = Curve_Radius; //Add Radius for curve bridge

                            vl1.Set_Wheel_Distance();

                            vl1.TotalLength = AST_DOC_ORG.AnalysisData.Joints.MaxX;
                            vl1.XINC = AST_DOC_ORG.Load_Geneartion[i].XINC;
                            vl1.X = AST_DOC_ORG.Load_Geneartion[i].X;
                            vl1.Y = AST_DOC_ORG.Load_Geneartion[i].Y;
                            vl1.Z = AST_DOC_ORG.Load_Geneartion[i].Z;

                            foreach (var item in vl1.WheelAxis)
                            {
                                item.Z = vl1.Z;
                            }

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

            cmb_pp_text_size.SelectedIndex = 0;
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

            SetTextSize(PP_Text_Size);

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
            SetTextSize(PP_Text_Size);
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
            //Tab_Selection();
        }

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
            //this.Close();
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
        }

        private void btn_update_file_Click(object sender, EventArgs e)
        {
            
        }

        #endregion Post Process

        private void tc_pp_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!File.Exists(Analysis_File_Name))
            {
                MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    t.Radius = txtSize * 2.5;
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
            if (VD == null) return;
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
                    t.Radius = PP_Text_Size * 2;
                    t.Update();
                }

            }
            VD.Redraw(true);
        }
        public void DisposeDoc(vdDocument dc)
        {
            try
            {
                dc.Dispose();
            }
            catch (Exception ex) { }
        }

    }
}
