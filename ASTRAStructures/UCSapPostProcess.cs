using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
//using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRADrawingTools;
//using HEADSNeed.ASTRA.ASTRAForms;
using AstraAccess.SAP_Forms;

using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;

using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraAccess.SAP_Classes;


namespace ASTRAStructures
{
    public partial class UCSapPostProcess : UserControl
    {
        //public UCSapPostProcess()
        //{
        //    InitializeComponent();
        //}
        IApplication iApp;
        public string DataFileName { get; set; }

        public string ReportFileName
        {
            get
            {
                if (File.Exists(DataFileName))
                {
                    return Path.Combine(Path.GetDirectoryName(DataFileName), "SAP_ANALYSIS_REP.TXT");
                }
                return "";
            }
        }

        Hashtable Load_Deflections { get; set; }

        AstraInterface.DataStructure.BridgeMemberAnalysis StructureAnalysis { get; set; }

        public ASTRADoc AST_DOC_ORG { get; set; }

        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;
        int iLoadCase;

        int lastId = -1;


        public UCSapPostProcess(IApplication app)
        {
            //UCSapPostProcess();
            InitializeComponent();
            iApp = app;
            DataFileName = iApp.SAP_File;
            SAP_DOC = new SAP_Document();
        }
        public UCSapPostProcess(IApplication app, string file_name)
        {
            //UCSapPostProcess();
            InitializeComponent();
            iApp = app;
            DataFileName = file_name;
            SAP_DOC = new SAP_Document();
        }

        public UCSapPostProcess(IApplication app, string menu_name, bool flag)
        {

            try
            {

                //Project_Type = eASTRADesignType.Structural_Analysis_SAP;



                InitializeComponent();
                iApp = app;
                //DataFileName = file_name;
                SAP_DOC = new SAP_Document();

                MyList ml = new MyList(menu_name, ':');
            }
            catch (Exception ex) { }

        }


        #region CAD Methods
        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {

            ////if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
            ////{
            ////    //gripset = GetGripSelection(false);
            ////    vdSelection gripset = GetGripSelection(false);

            ////    if (gripset == null) return;
            ////    gPoint pt = VDoc.CCS_CursorPos();
            ////    //activeDragDropControl = this.vdScrollableControl1;
            ////    int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
            ////    //activeDragDropControl = null;
            ////    return;
            ////}
            ////if (e.Button != MouseButtons.Right) return;
            ////if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0) return;
            ////if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count != 1) return;

            ////if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
            ////{
            ////    //gripset = GetGripSelection(false);
            ////    vdSelection gripset = GetGripSelection(false);
            ////    if (gripset == null) return;
            ////    gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            ////    //activeDragDropControl = this.vdScrollableControl1;
            ////    int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
            ////    //activeDragDropControl = null;
            ////}
            ////else
            ////{
            ////    //if (BaseAction.PressedKeyCode != Keys.None) return; //6011 correction
            ////    //MainForm parent = this.MdiParent as MainForm;
            ////    //parent.commandLine.PostExecuteCommand("");
            ////}
        }
        private vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;

            vdDocument VD = VDoc;

                VD = PP_ActiveDoc;

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


                vdSelection gripset = GetGripSelection(false);

                vdDocument VD = VDoc;


                Deselect_All_Element(VD);

                foreach (vdFigure fig in gripset)
                {

                    fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                    if (fig is vdText)
                    {
                        vdText obj = fig as vdText;
                        if (obj.ToolTip.StartsWith("Node"))
                        {

                            MyList ms = obj.TextString;
                            int nd_no = ms.GetInt(0);
                        }
                    }
                    //if (fig is vdPolyline)
                    //{
                    //    ShowElementOnGrid(fig as vdPolyline);
                    //    VD.Redraw(true);
                    //    break;
                    //}

                    //if (fig is ASTRASupportFixed)
                    //{
                    //    ASTRASupportFixed asf = fig as ASTRASupportFixed;

                    //    ShowNodeOnGrid(asf.Origin);
                    //    VD.Redraw(true);
                    //    break;
                    //}

                    //if (fig is ASTRASupportPinned)
                    //{
                    //    ASTRASupportPinned asp = fig as ASTRASupportPinned;

                    //    ShowNodeOnGrid(asp.Origin);
                    //    VD.Redraw(true);
                    //    break;
                    //}


                    //ln = fig as vdLine;

                    //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
                    //{
                    //}
                    //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Left)
                    //{
                    //}
                    //if (ln == null)
                    //{
                    //    dFace = fig as vd3DFace;
                    //    ShowElementOnGrid(dFace);
                    //    VD.Redraw(true);
                    //}
                    //else
                    //{
                    //    ShowMemberOnGrid(ln);
                    //    VD.Redraw(true);
                    //}
                }
                VD.Update();
                VD.Redraw(true);
                //gripset.RemoveAll();
            }
            catch (Exception exx) { }
        }

        public List<int> Get_Selected_Nodes(vdDocument doc)
        {
            List<int> nodes = new List<int>();

            vdSelection gripset = GetGripSelection(false);

            //vdDocument VD = VDoc;


            //Deselect_All_Element(VD);

            foreach (vdFigure fig in gripset)
            {

                if (fig.Layer.Name == nodesLay.Name)
                {
                    if (fig.ToolTip.StartsWith("Node"))
                    {
                        MyList mll = fig.ToolTip;

                        nodes.Add(mll.GetInt(3));
                    }
                }

            }
            //VD.Update();
            //VD.Redraw(true);

            return nodes;
        }
        void BaseControl_vdKeyUp(KeyEventArgs e, ref bool cancel)
        {
            try
            {

                //if (tcParrent.SelectedTab == tab_pre_process)
                //{
                //    if (tc_prp_main.SelectedTab != tab_props || e.Button == MouseButtons.Right)
                //    {
                //        Delete_Layer_Items("Selection");
                //        //return;
                //    }

                //    if (tc_prp_main.SelectedTab != tab_geom)
                //    {
                //        for (int i = 0; i < VDoc.ActiveLayOut.Entities.Count; i++)
                //        {
                //            if (VDoc.ActiveLayOut.Entities[i] is vdCircle)
                //            {
                //                if (VDoc.ActiveLayOut.Entities[i].Layer.Name != "Selection")
                //                {
                //                    VDoc.ActiveLayOut.Entities.RemoveAt(i); i = -1;
                //                }
                //            }
                //        }
                //        VDoc.Redraw(true);
                //        return;
                //    }
                //    else
                //    {

                //        if (tc3.SelectedTab == tab2_elements)
                //        {
                //            return;
                //        }
                //    }
                //}


                vdSelection gripset = GetGripSelection(false);
                //vd3DFace dFace;
                //vdLine ln;

                vdDocument VD = VDoc;

                    VD = PP_ActiveDoc;

                Deselect_All_Element(VD);

                foreach (vdFigure fig in gripset)
                {

                    fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                    //if (fig is vdText)
                    //{
                    //    ShowMemberOnGrid(fig as vdText);
                    //    VD.Redraw(true);
                    //    break;
                    //}
                    //if (fig is vdPolyline)
                    //{
                    //    ShowElementOnGrid(fig as vdPolyline);
                    //    VD.Redraw(true);
                    //    break;
                    //}

                    //if (fig is ASTRASupportFixed)
                    //{
                    //    ASTRASupportFixed asf = fig as ASTRASupportFixed;

                    //    ShowNodeOnGrid(asf.Origin);
                    //    VD.Redraw(true);
                    //    break;
                    //}

                    //if (fig is ASTRASupportPinned)
                    //{
                    //    ASTRASupportPinned asp = fig as ASTRASupportPinned;

                    //    ShowNodeOnGrid(asp.Origin);
                    //    VD.Redraw(true);
                    //    break;
                    //}


                    //ln = fig as vdLine;

                    //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
                    //{
                    //}
                    //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Left)
                    //{
                    //}
                    //if (ln == null)
                    //{
                    //    dFace = fig as vd3DFace;
                    //    ShowElementOnGrid(dFace);
                    //    VD.Redraw(true);
                    //}
                    //else
                    //{
                    //    ShowMemberOnGrid(ln);
                    //    VD.Redraw(true);
                    //}
                }
                VD.Update();
                VD.Redraw(true);
                //gripset.RemoveAll();
            }
            catch (Exception exx) { }
        }

        #endregion Cad Methods

        vdDocument VDoc
        {
            get
            {
                //return vdScrollableControl1.BaseControl.ActiveDocument;
                return PP_ActiveDoc;
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
        public vdDocument diagDoc
        {
            get
            {
                return vdSC_diag.BaseControl.ActiveDocument;
            }
        }
        public vdDocument maxDoc
        {
            get
            {
                return vdSC_mfrc.BaseControl.ActiveDocument;
            }
        }


        public vdDocument PP_ActiveDoc
        {
            get
            {
                if (tc4.SelectedTab == tab_defl_doc)
                    return defDoc;
                else if (tc4.SelectedTab == tab_max_doc)
                    return maxDoc;
                else if (tc4.SelectedTab == tab_evlp_doc)
                    return envDoc;
                else if (tc4.SelectedTab == tab_diag_doc)
                    return diagDoc;

                return defDoc;
            }
        }

        vdLayer selectLay { get; set; }
        vdLayer nodesLay { get; set; }
        vdLayer membersLay { get; set; }
        vdLayer beamsLay { get; set; }
        vdLayer trussesLay { get; set; }
        vdLayer solidsLay { get; set; }

        public ASTRADoc AST_DOC;
        public SAP_Document SAP_DOC;


        public UCSapPostProcess()
        {
            InitializeComponent();
            DataFileName = "";
            SAP_DOC = new SAP_Document();
        }

        public void UCSapPostProcess_Load(object sender, EventArgs e)
        {
            //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);

            if (vdSC_ldef != null) return;

            vdSC_ldef.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdSC_ldef.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);


            vdSC_mfrc.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdSC_mfrc.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);



            Load_Deflections = new Hashtable();

            SetLayers(VDoc);

            for (int i = 1; i < 100; i++)
            {
                cmb_pp_text_size.Items.Add(i.ToString());
            }


            cmb_pp_text_size.SelectedIndex = 5;


            Open_Data_File(DataFileName);

            cmbInterval.SelectedIndex = 9;
        }

        void Delete_Layer_Objects(vdLayer lay, vdDocument doc)
        {
            Delete_Layer_Objects(lay.Name, doc);
        }
        void Delete_Layer_Objects(string layer_name, vdDocument doc)
        {

            foreach (var item in doc.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == layer_name)
                    {
                        vf.Deleted = true;
                    }
                }

            }
        }
        public void SetLayers(vdDocument doc)
        {
            if (doc == null) return;
            //Chiranjit [2014 10 31]
            selectLay = doc.Layers.FindName("Selection");
            if (selectLay == null)
            {
                selectLay = new vdLayer();
                selectLay.Name = "Selection";
                selectLay.SetUnRegisterDocument(doc);
                selectLay.setDocumentDefaults();
                selectLay.PenColor = new vdColor(Color.Red);
                doc.Layers.AddItem(selectLay);
            }


            nodesLay = doc.Layers.FindName("Nodes");
            if (nodesLay == null)
            {
                nodesLay = new vdLayer();
                nodesLay.Name = "Nodes";
                nodesLay.SetUnRegisterDocument(doc);
                nodesLay.setDocumentDefaults();
                nodesLay.PenColor = new vdColor(Color.Magenta);
                doc.Layers.AddItem(nodesLay);
            }


            membersLay = doc.Layers.FindName("Members");
            if (membersLay == null)
            {
                membersLay = new vdLayer();
                membersLay.Name = "Members";
                membersLay.SetUnRegisterDocument(doc);
                membersLay.setDocumentDefaults();
                doc.Layers.AddItem(membersLay);
            }

            beamsLay = doc.Layers.FindName("Beams");
            if (beamsLay == null)
            {
                beamsLay = new vdLayer();
                beamsLay.Name = "Beams";
                beamsLay.SetUnRegisterDocument(doc);
                beamsLay.setDocumentDefaults();
                doc.Layers.AddItem(beamsLay);
            }


            trussesLay = doc.Layers.FindName("Trusses");
            if (trussesLay == null)
            {
                trussesLay = new vdLayer();
                trussesLay.Name = "Trusses";
                trussesLay.SetUnRegisterDocument(doc);
                trussesLay.setDocumentDefaults();
                doc.Layers.AddItem(trussesLay);
            }

            solidsLay = doc.Layers.FindName("Solids");
            if (solidsLay == null)
            {
                solidsLay = new vdLayer();
                solidsLay.Name = "Solids";
                solidsLay.SetUnRegisterDocument(doc);
                solidsLay.setDocumentDefaults();
                doc.Layers.AddItem(solidsLay);
            }


            VectorDraw.Professional.Memory.vdMemory.Collect();
            System.GC.Collect();

        }

        private void Open_Data_File(string file_name)
        {
            DataFileName = file_name;
        }

        public void Draw_Joints(DataGridView DGV)
        {
            Draw_Joints(VDoc);
        }


        public void SetIApplication(IApplication iapp, UCSapPreProcess ucsap)
        {
            iApp = iapp;
            dgv_joints = ucsap.dgv_joints;
            dgv_plate_elements = ucsap.dgv_plate_elements;
            dgv_solid_elements = ucsap.dgv_solid_elements;
            dgv_truss_elements = ucsap.dgv_truss_elements;
            dgv_beam_elements = ucsap.dgv_beam_elements;
            //dgv_boundary_elements = ucsap.dgv_boundary_elements;

            AST_DOC = new ASTRADoc();
        }
       DataGridView dgv_joints;


        public void Draw_Joints(vdDocument doc)
        {

            if (dgv_joints == null) return;

            DataGridView DGV = dgv_joints;
            //Clear_All();
            //ASTRADoc AST_DOC = new ASTRADoc();

            
            AST_DOC.Joints.Clear();

            SupportCollection list_supports = new SupportCollection();

            Support sp = new Support();

            string sup = "";

            bool FX, FY, FZ, MX, MY, MZ;

            //sp.Option = Support.SupportOption.PINNED;
            int c = 0;

            //tv_supports.Nodes.Clear();
            for (int i = 0; i < DGV.Rows.Count; i++)
            {

                c = 0;
                sup = "";
                JointCoordinate jn = new JointCoordinate();
                //DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                jn.NodeNo = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                jn.Point.x = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                jn.Point.y = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                jn.Point.z = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                AST_DOC.Joints.Add(jn);

                FX = ((bool)DGV[c++, i].Value);
                FY = ((bool)DGV[c++, i].Value);
                FZ = ((bool)DGV[c++, i].Value);


                MX = ((bool)DGV[c++, i].Value);
                MY = ((bool)DGV[c++, i].Value);
                MZ = ((bool)DGV[c++, i].Value);

                sp = new Support();
                sp.FX = FX;
                sp.FY = FY;
                sp.FZ = FZ;
                sp.MX = MX;
                sp.MY = MY;
                sp.MZ = MZ;

                if (FX && FY && FZ && MX && MY && MZ)
                {
                    sp.Option = Support.SupportOption.FIXED;
                    sp.Node = jn;
                    list_supports.Add(sp);

                    sup = "FIXED";
                }
                else if (FX && FY && FZ && !MX && !MY && !MZ)
                {
                    sp.Option = Support.SupportOption.PINNED;
                    sp.Node = jn;
                    list_supports.Add(sp);
                    sup = "PINNED";
                }
                else if ((FX && !FY && FZ && !MX && MY && !MZ))
                {
                    //if (cmb_structure_type.SelectedIndex != 1)
                    //{
                    //    sp = new Support();
                    //    sp.Option = Support.SupportOption.FIXED;
                    //    sp.Node = jn;
                    //    list_supports.Add(sp);

                    //    sup = "FIXED BUT";

                    //    if (FX) sup += " FX";
                    //    if (FY) sup += " FY";
                    //    if (FZ) sup += " FZ";
                    //    if (MX) sup += " MX";
                    //    if (MY) sup += " MY";
                    //    if (MZ) sup += " MZ";
                    //}
                }
                else if (FX || FY || FZ || MX || MY || MZ)
                {
                    sp.Option = Support.SupportOption.FIXED;
                    sp.Node = jn;
                    list_supports.Add(sp);

                    sup = "FIXED BUT";

                    if (!FX) sup += " FX";
                    if (!FY) sup += " FY";
                    if (!FZ) sup += " FZ";
                    if (!MX) sup += " MX";
                    if (!MY) sup += " MY";
                    if (!MZ) sup += " MZ";
                }
                if (sup != "")
                {
                    //tv_supports.Nodes.Add(jn.NodeNo + " " + sup);
                }

            }

            Draw_Joints(AST_DOC.Joints, doc);
            Draw_Members(AST_DOC.Joints, doc);
            list_supports.DrawSupport(doc);

            doc.Redraw(true);
        }


        public void Draw_Joints(JointCoordinateCollection list, vdDocument doc)
        {
            //= VDoc;
            doc.Palette.Background = Color.White;
            double txtSize = Text_Size;
            Delete_Layer_Objects(nodesLay, doc);


            for (int i = 0; i < list.Count; i++)
            {

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = list[i].Point;
                vTxt.Height = txtSize;
                //vTxt.HorJustify = VdConstHorJust.VdTextHorCenter;
                //vTxt.VerJustify = VdConstVerJust.VdTextVerCen;
                vTxt.TextString = list[i].NodeNo.ToString();

                vTxt.ToolTip = string.Format("Node No : {0} [X:{1:f4}, Y:{2:f4}, Z:{3:f4}",
                    list[i].NodeNo, list[i].Point.x, list[i].Point.y, list[i].Point.z);

                vTxt.Layer = nodesLay;
                doc.ActiveLayOut.Entities.AddItem(vTxt);


                //vdCircle vcir = new vdCircle();
                //vcir.SetUnRegisterDocument(doc);
                //vcir.setDocumentDefaults();
                //vcir.Center = list[i].Point;
                //vcir.Radius = txtSize;
                //vcir.Layer = nodesLay;
                //doc.ActiveLayOut.Entities.AddItem(vcir);
            }
            doc.Update();
            doc.Redraw(true);

        }
        public void Draw_Beams(DataGridView DGV)
        {


            //Clear_All();
            //ASTRADoc AST_DOC = new ASTRADoc();


            AST_DOC.Members.Clear();

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                MemberIncidence mi = new MemberIncidence();

                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.MemberNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.StartNode.NodeNo = MyList.StringToInt(DGV[1, i].Value.ToString(), 0);
                mi.EndNode.NodeNo = MyList.StringToInt(DGV[2, i].Value.ToString(), 0);


                AST_DOC.Members.Add(mi);
            }
            AST_DOC.Members.CopyJointCoordinates(AST_DOC.Joints);

            //AST_DOC.Members.DrawMember(VDoc, 0.3);

            foreach (var item in AST_DOC.Members)
            {
                Draw_Beam(item, VDoc, Text_Size);
            }

            VDoc.Redraw(true);
        }

        public void Draw_Beams(vdDocument doc)
        {
            Draw_Beams(AST_DOC.Joints, doc);
        }

        DataGridView dgv_beam_elements;

        public void Draw_Beams(JointCoordinateCollection jcols, vdDocument doc)
        {
            if (dgv_beam_elements == null) return;

            DataGridView DGV = dgv_beam_elements;
            //Clear_All();
            //ASTRADoc AST_DOC = new ASTRADoc();


            AST_DOC.Members.Clear();

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                MemberIncidence mi = new MemberIncidence();

                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.MemberNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.StartNode.NodeNo = MyList.StringToInt(DGV[1, i].Value.ToString(), 0);
                mi.EndNode.NodeNo = MyList.StringToInt(DGV[2, i].Value.ToString(), 0);


                AST_DOC.Members.Add(mi);
            }
            AST_DOC.Members.CopyJointCoordinates(jcols);

            //AST_DOC.Members.DrawMember(VDoc, 0.3);

            foreach (var item in AST_DOC.Members)
            {
                Draw_Beam(item, doc, Text_Size);
            }

            doc.Redraw(true);
        }


        public void Draw_Beam(MemberIncidence mi, vdDocument doc, double txtSize)
        {

            double length, factor;


            doc.Palette.Background = Color.White;
            vdLine line = new vdLine();
            vdText vtxtMemberNo = new vdText();

            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            vtxtMemberNo.SetUnRegisterDocument(doc);
            vtxtMemberNo.setDocumentDefaults();


            line.PenColor = new vdColor(Color.Black);

            line.Layer = doc.Layers[0];
            doc.Layers[0].Frozen = false;

            line.StartPoint = mi.StartNode.Point;
            line.EndPoint = mi.EndNode.Point;


            line.ToolTip = string.Format("Beam No : {0} [Nodes ({1}, {2})]",
                 mi.MemberNo,
                 mi.StartNode.NodeNo,
                 mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(line);




            length = line.Length();
            //factor = 22.2222d;
            //factor = length / 0.9d;

            //txtSize = length / factor;

            //txtSize = GetTextSize();


            vtxtMemberNo.TextString = mi.MemberNo.ToString();
            vtxtMemberNo.Layer = beamsLay;
            vtxtMemberNo.Height = txtSize;
            vtxtMemberNo.PenColor = new vdColor(Color.Blue);
            vtxtMemberNo.InsertionPoint = (mi.StartNode.Point + mi.EndNode.Point) / 2;
            vtxtMemberNo.Layer = beamsLay;
            vtxtMemberNo.ToolTip = string.Format("Beam No : {0} [Nodes ({1}, {2})]",
                mi.MemberNo,
                mi.StartNode.NodeNo,
                mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(vtxtMemberNo);

            //doc.ZoomAll();
            //doc.Redraw(true);
        }

        public void Draw_Truss(MemberIncidence mi, vdDocument doc)
        {
            double txtSize = Text_Size;

            double length, factor;


            doc.Palette.Background = Color.White;
            vdLine line = new vdLine();
            vdText vtxtMemberNo = new vdText();

            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            vtxtMemberNo.SetUnRegisterDocument(doc);
            vtxtMemberNo.setDocumentDefaults();


            line.PenColor = new vdColor(Color.Black);

            line.Layer = doc.Layers[0];
            doc.Layers[0].Frozen = false;

            line.StartPoint = mi.StartNode.Point;
            line.EndPoint = mi.EndNode.Point;


            line.ToolTip = string.Format("Truss No : {0} [Nodes ({1}, {2})]",
                 mi.MemberNo,
                 mi.StartNode.NodeNo,
                 mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(line);

            length = line.Length();


            vtxtMemberNo.TextString = mi.MemberNo.ToString();
            vtxtMemberNo.Layer = trussesLay;
            vtxtMemberNo.Height = txtSize;
            vtxtMemberNo.PenColor = new vdColor(Color.Red);
            vtxtMemberNo.InsertionPoint = (mi.StartNode.Point + mi.EndNode.Point) / 2;
            //vtxtMemberNo.Layer = beamsLay;
            vtxtMemberNo.ToolTip = string.Format("Truss No : {0} [Nodes ({1}, {2})]",
                mi.MemberNo,
                mi.StartNode.NodeNo,
                mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(vtxtMemberNo);

        }

        public void Draw_Trusses(DataGridView DGV)
        {


            //Clear_All();
            //ASTRADoc AST_DOC = new ASTRADoc();


            AST_DOC.Members.Clear();

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                MemberIncidence mi = new MemberIncidence();

                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.MemberNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.StartNode.NodeNo = MyList.StringToInt(DGV[1, i].Value.ToString(), 0);
                mi.EndNode.NodeNo = MyList.StringToInt(DGV[2, i].Value.ToString(), 0);


                AST_DOC.Members.Add(mi);
            }

            //iACad.Document.ActiveLayOut.Entities.EraseAll();

            //foreach (var item in VDoc.ActionLayout.Entities)
            //{
            //    if (item is vdFigure)
            //    {
            //        vdFigure vf = item as vdFigure;
            //        if (vf.Layer.Name == "Trusses")
            //        {
            //            vf.Deleted = true;
            //        }
            //    }
            //}

            Delete_Layer_Objects(trussesLay, VDoc);


            AST_DOC.Members.CopyJointCoordinates(AST_DOC.Joints);

            //AST_DOC.Members.DrawMember(VDoc, 0.3);

            foreach (var item in AST_DOC.Members)
            {
                Draw_Truss(item, VDoc);
            }

            VDoc.Redraw(true);
        }
        public void Draw_Trusses(vdDocument doc)
        {
            Draw_Trusses(AST_DOC.Joints, doc);
        }

        DataGridView dgv_truss_elements;

        public void Draw_Trusses(JointCoordinateCollection jcols, vdDocument doc)
        {

            DataGridView DGV = dgv_truss_elements;
            //Clear_All();
            //ASTRADoc AST_DOC = new ASTRADoc();


            AST_DOC.Members.Clear();

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                MemberIncidence mi = new MemberIncidence();

                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.MemberNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.StartNode.NodeNo = MyList.StringToInt(DGV[1, i].Value.ToString(), 0);
                mi.EndNode.NodeNo = MyList.StringToInt(DGV[2, i].Value.ToString(), 0);


                AST_DOC.Members.Add(mi);
            }

            Delete_Layer_Objects(trussesLay, doc);


            AST_DOC.Members.CopyJointCoordinates(jcols);

            //AST_DOC.Members.DrawMember(VDoc, 0.3);

            foreach (var item in AST_DOC.Members)
            {
                Draw_Truss(item, doc);
            }

            doc.Redraw(true);
        }

        public void Draw_Members()
        {
            Draw_Members(AST_DOC.Joints, VDoc);
        }
        public void Draw_Members(JointCoordinateCollection jcols, vdDocument doc)
        {


            Delete_Layer_Objects("0", doc);
            Delete_Layer_Objects(trussesLay, doc);
            Delete_Layer_Objects(beamsLay, doc);
            Draw_Trusses(jcols, doc);
            Draw_Beams(jcols, doc);

            VDoc.Redraw(true);
        }

        public void Draw_Plates()
        {
            Draw_Plates(VDoc);
        }

        public void Draw_Plates(vdDocument doc)
        {
            if (AST_DOC != null)
            {
                Draw_Plates(AST_DOC.Joints, doc);
            }
        }

        DataGridView dgv_plate_elements;

        public void Draw_Plates(JointCoordinateCollection jcols, vdDocument doc)
        {

            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(iACad.AstraDocument);
            astdoc = AST_DOC;


            astdoc.Elements.Clear();

            DataGridView DGV = dgv_plate_elements;

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

            Delete_Layer_Objects("Elements", doc);


            if (astdoc.Elements.Count != 0)
            {
                astdoc.Elements.CopyCoordinates(jcols);
                astdoc.Elements.DrawElements(doc);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(doc);
            }
            doc.Redraw(true);

        }

        void Deselect_All_Element(vdDocument doc)
        {
            foreach (var item in doc.ActiveLayOut.Entities)
            {
                vdFigure vpf = item as vdFigure;
                vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;

                //if (item is vdPolyface)
                //{
                //    vdPolyface vpf = item as vdPolyface;
                //    kString st = vpf.ToolTip;
                //    vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                //    MyList mll = vpf.ToolTip;
                //    if (vpf.ToolTip.StartsWith("Solid No"))
                //    {

                //        int el = mll.GetInt(2);
                //        if (el == elment_no)
                //        {
                //            vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                //        }

                //    }
                //}
            }

            doc.Update();
            doc.Redraw(true);
        }

        void Show_Solid_Element(vdDocument doc, int elment_no)
        {
            foreach (var item in doc.ActiveLayOut.Entities)
            {
                if (item is vdPolyface)
                {
                    vdPolyface vpf = item as vdPolyface;
                    kString st = vpf.ToolTip;
                    vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                    MyList mll = vpf.ToolTip;
                    if (mll[0].StartsWith("Solid"))
                    {

                        int el = mll.GetInt(2);
                        if (el == elment_no)
                        {
                            vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                        }

                    }
                }
            }

            doc.Update();
            doc.Redraw(true);
        }
        void Show_Plate_Element(vdDocument doc, int elment_no)
        {
            foreach (var item in doc.ActiveLayOut.Entities)
            {
                if (item is vdPolyface)
                {
                    vd3DFace vpf = item as vd3DFace;
                    kString st = vpf.ToolTip;
                    vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                    MyList mll = vpf.ToolTip;
                    if (mll[0].StartsWith("Plate"))
                    {

                        int el = mll.GetInt(2);
                        if (el == elment_no)
                        {
                            vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                        }

                    }
                }
            }

            doc.Update();
            doc.Redraw(true);
        }

        void Show_Beam_Element(vdDocument doc, int elment_no)
        {
            foreach (var item in doc.ActiveLayOut.Entities)
            {
                if (item is vdLine)
                {
                    vdLine vpf = item as vdLine;
                    kString st = vpf.ToolTip;
                    vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                    MyList mll = vpf.ToolTip;
                    if (mll[0].StartsWith("Beam"))
                    {

                        int el = mll.GetInt(3);
                        if (el == elment_no)
                        {
                            vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                        }

                    }
                }
            }

            doc.Update();
            doc.Redraw(true);
        }

        void Show_Truss_Element(vdDocument doc, int elment_no)
        {
            foreach (var item in doc.ActiveLayOut.Entities)
            {
                if (item is vdLine)
                {
                    vdLine vpf = item as vdLine;
                    kString st = vpf.ToolTip;
                    vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                    MyList mll = vpf.ToolTip;
                    if (mll[0].StartsWith("Truss"))
                    {

                        int el = mll.GetInt(3);
                        if (el == elment_no)
                        {
                            vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                        }

                    }
                }
            }

            doc.Update();
            doc.Redraw(true);
        }

        public void Draw_Solids(vdDocument doc)
        {
            Delete_Layer_Objects(solidsLay, doc);
            foreach (var item in SAP_DOC.Solids)
            {
                Draw_Solids(AST_DOC.Joints, doc, item);
            }
            //if (SAP_DOC.Solids.Count != 0)
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(doc);
            //else 
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(doc);

        }


        DataGridView dgv_solid_elements;

        public void Draw_Solids()
        {

            SAP_DOC.Solids.Clear();
            DataGridView DGV = dgv_solid_elements;
            int c = 0;
            for (int i = 0; i < DGV.RowCount; i++)
            {
                Solid_Element se = new Solid_Element();
                c = 0;
                se.Element_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_1 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_2 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_3 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_4 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_5 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_6 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_7 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Node_8 = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Integration_Order = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Material_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Generation_Parameter = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.LSA = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.LSB = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.LSC = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.LSD = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Face_Number = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                se.Stress_free_Temperature = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);

                if (se.Element_No != 0)
                    SAP_DOC.Solids.Add(se);
            }


            Draw_Solids(VDoc);
            //DataGridView DGV = dgv_solid_elements;

            //foreach (var item in SAP_DOC.Solids)
            //{
            //    Draw_Solids(AST_DOC.Joints, doc, item);
            //}


            //if (SAP_DOC.Solids.Count == 0)
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(doc);

        }

        public void Draw_Solids(JointCoordinateCollection jcols, vdDocument doc)
        {
            foreach (var item in SAP_DOC.Solids)
            {
                Draw_Solids(jcols, doc, item);
            }


            //if (SAP_DOC.Solids.Count == 0)
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(doc);
            //else
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(doc);

            doc.Redraw(true);

        }
        public void Draw_Solids(JointCoordinateCollection jcols, vdDocument doc, Solid_Element elmt)
        {

            //solidsLay
            //vdLayer elementLay = new vdLayer();
            //elementLay.Name = "Elements";
            //elementLay.SetUnRegisterDocument(doc);
            //elementLay.setDocumentDefaults();
            ////elementLay.PenColor = new vdColor(Color.Gray);
            //elementLay.PenColor = new vdColor(Color.DarkGreen);
            //doc.Layers.AddItem(elementLay);
            //solidsLay.PenColor = new vdColor(Color.Gray);
            //solidsLay.PenColor = new vdColor(Color.OrangeRed);
            //solidsLay.PenColor = new vdColor(Color.FromArgb(128, 128, 128));
            solidsLay.PenColor = new vdColor(Color.FromArgb(0, 204, 204));

            int indx = -1;



            //foreach (Element elmt in list)
            //{

            //We will create a vdPolyface object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            //vdPolyface poly = new vd3DFace();


            vdPolyface one3dface = new vdPolyface();
            //vd3DFace one3dface = new vd3DFace();
            //VectorDraw.Professional.vdFigures.vdPolyline one3dface = new vdPolyline();


            //We set the document where the polyface is going to be added.This is important for the vdPolyface in order to obtain initial properties with setDocumentDefaults.
            one3dface.SetUnRegisterDocument(doc);
            one3dface.setDocumentDefaults();



            VectorDraw.Geometry.gPoints gpts = new VectorDraw.Geometry.gPoints();

            one3dface.VertexList.RemoveAll();

            indx = jcols.IndexOf(elmt.Node_1);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);
            indx = jcols.IndexOf(elmt.Node_2);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);
            indx = jcols.IndexOf(elmt.Node_3);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);

            indx = jcols.IndexOf(elmt.Node_4);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);


            indx = jcols.IndexOf(elmt.Node_5);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);

            indx = jcols.IndexOf(elmt.Node_6);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);

            indx = jcols.IndexOf(elmt.Node_7);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);

            indx = jcols.IndexOf(elmt.Node_8);
            if (indx != -1) one3dface.VertexList.Add(jcols[indx].Point);




            //one3dface.VertexList.Add(elmt.Node1.Point);
            //one3dface.VertexList.Add(elmt.Node2.Point);
            //one3dface.VertexList.Add(elmt.Node3.Point);
            //one3dface.VertexList.Add(elmt.Node4.Point);
            //one3dface.VertexList.Add(elmt.Node1.Point);




            //vdHatchProperties Properties = new vdHatchProperties();

            //Properties.SetUnRegisterDocument(doc);




            //Properties.FillMode = VdConstFill.VdFillModeSolid;
            //Properties.FillBkColor = new vdColor(Color.DarkGreen);
            //Properties.FillColor = new vdColor(Color.DarkGreen);


            //one3dface.HatchProperties = Properties;


            //one3dface.VertexList = gpts;
            one3dface.Layer = solidsLay;
            //one3dface.visibility
            #region Define Face
            one3dface.FaceList.Add(1);
            one3dface.FaceList.Add(2);
            one3dface.FaceList.Add(3);
            one3dface.FaceList.Add(4);
            one3dface.FaceList.Add(-1);
            one3dface.FaceList.Add(5);
            one3dface.FaceList.Add(6);
            one3dface.FaceList.Add(7);
            one3dface.FaceList.Add(8);
            one3dface.FaceList.Add(-1);
            one3dface.FaceList.Add(1);
            one3dface.FaceList.Add(2);
            one3dface.FaceList.Add(6);
            one3dface.FaceList.Add(5);
            one3dface.FaceList.Add(-1);
            one3dface.FaceList.Add(2);
            one3dface.FaceList.Add(3);
            one3dface.FaceList.Add(7);
            one3dface.FaceList.Add(6);
            one3dface.FaceList.Add(-1);
            one3dface.FaceList.Add(3);
            one3dface.FaceList.Add(4);
            one3dface.FaceList.Add(8);
            one3dface.FaceList.Add(7);
            one3dface.FaceList.Add(-1);
            one3dface.FaceList.Add(4);
            one3dface.FaceList.Add(1);
            one3dface.FaceList.Add(5);
            one3dface.FaceList.Add(8);
            one3dface.FaceList.Add(-1);
            #endregion Define Face

            one3dface.Update();

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            doc.ActiveLayOut.Entities.AddItem(one3dface);

            //View3DShadeOn
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(doc);
            one3dface.ToolTip = "Solid No: " + elmt.Element_No + " [" + elmt.Node_1 + "," +

                elmt.Node_2 + "," + elmt.Node_3 + "," + elmt.Node_4 + "," + elmt.Node_5
                 + "," + elmt.Node_6 + "," + elmt.Node_7 + "," + elmt.Node_8 + "]";
            //}
        }

        public void Tab_Selection()
        {
            #region Post Process Tabs


            tab_pp_1.TabPages.Clear();
            tc4.TabPages.Clear();

            if (tc_pp_main.SelectedTab == tab_forces)
            {
                tab_pp_1.TabPages.Add(tab1_forces);
                tc4.TabPages.Add(tab_max_doc);
            }
            else if (tc_pp_main.SelectedTab == tab_load_deflection)
            {
                tab_pp_1.TabPages.Add(tab1_load_deflection);
                tc4.TabPages.Add(tab_defl_doc);

            }
            else if (tc_pp_main.SelectedTab == tab_max_force)
            {
                tab_pp_1.TabPages.Add(tab1_max_force);
                tc4.TabPages.Add(tab_max_doc);
            }
            else if (tc_pp_main.SelectedTab == tab_envelop)
            {
                tab_pp_1.TabPages.Add(tab1_truss_env);
                //tc4.SelectedTab = tab_evlp_doc;
                tc4.TabPages.Add(tab_evlp_doc);
            }
            else if (tc_pp_main.SelectedTab == tab_diagram)
            {
                tab_pp_1.TabPages.Add(tab1_diagram);
                //tc4.SelectedTab = tab_evlp_doc;
                tc4.TabPages.Add(tab_diag_doc);
            }

            #endregion Post Process Tabs


        }
        public void Show_Panel(TabPage tp)
        {
           
        }

        List<SAP_Joint> Get_Joints()
        {
            List<SAP_Joint> list = new List<SAP_Joint>();
            SAP_Joint jnt = null;
            DataGridView DGV = dgv_joints;

            int cnt = 0;
            for (int i = 0; i < DGV.RowCount; i++)
            {
                cnt = 0;
                jnt = new SAP_Joint();

                jnt.NodeNo = MyList.StringToInt(DGV[cnt++, i].Value.ToString(), 0);
                jnt.X = MyList.StringToDouble(DGV[cnt++, i].Value.ToString(), 0);
                jnt.Y = MyList.StringToDouble(DGV[cnt++, i].Value.ToString(), 0);
                jnt.Z = MyList.StringToDouble(DGV[cnt++, i].Value.ToString(), 0);
                jnt.Tx = (bool)(DGV[cnt++, i].Value);
                jnt.Ty = (bool)(DGV[cnt++, i].Value);
                jnt.Tz = (bool)(DGV[cnt++, i].Value);
                jnt.Rx = (bool)(DGV[cnt++, i].Value);
                jnt.Ry = (bool)(DGV[cnt++, i].Value);
                jnt.Rz = (bool)(DGV[cnt++, i].Value);

                list.Add(jnt);
            }
            return list;
        }
        private void Save_Data()
        {

        }

        public void RunAnalysis()
        {
        }
        public double Text_Size
        {
            get
            {
                return (cmb_pp_text_size.SelectedIndex + 1) * 0.01;
            }
        }
        private void tsmi_data_open_Click(object sender, EventArgs e)
        {
            ToolStripItem tsmi = sender as ToolStripItem;

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

        }

        private void rtb_inputs_TextChanged(object sender, EventArgs e)
        {

        }

        Timer timer1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
        }
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
            vdDocument VD = VDoc;
        }


        private void tc1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Tab_Selection();
        }

        public bool Check_Coordinate(int JointNo, int MemberNo)
        {
            return false;
        }
        private void btn_process_analysis_Click(object sender, EventArgs e)
        {

            RunAnalysis();

            if (maxDoc != null) maxDoc.ActiveLayOut.Entities.RemoveAll();
            if (defDoc != null) defDoc.ActiveLayOut.Entities.RemoveAll();
            if (envDoc != null) envDoc.ActiveLayOut.Entities.RemoveAll();
            if (diagDoc != null) diagDoc.ActiveLayOut.Entities.RemoveAll();

            //Tab_Post_Selection();
        }
      



        private void cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            //if(cmb.Name == cmb_pp_text_size.Name)
            //    cmb_text_size.SelectedIndex = cmb_pp_text_size.SelectedIndex;
            //else
            //cmb_pp_text_size.SelectedIndex = cmb_text_size.SelectedIndex;

            if (cmb.Name == cmb_pp_text_size.Name)
                SetTextSize(PP_ActiveDoc);
            else
                SetTextSize(VDoc);
        }

        private void SetTextSize(vdDocument doc)
        {
            if (doc == null) return;

            foreach (var item in doc.ActionLayout.Entities)
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
            doc.Redraw(true);
        }

        private void dgv_joints_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Draw_Joints(dgv_joints);
        }

        private void tc_pp_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_Selection();


            if (cmb_pp_text_size.Items.Count == 0)
            {
                for (int i = 1; i < 101; i++)
                {
                    cmb_pp_text_size.Items.Add(i);
                }
            }
            if (cmb_pp_text_size.SelectedIndex == -1)
                cmb_pp_text_size.SelectedIndex = 5;

            if (tc_pp_main.SelectedTab != tab_diag_doc)
                Tab_Post_Selection();


            if (PP_ActiveDoc != null)
            {
                //if (PP_ActiveDoc.ActiveLayOut.Entities.Count == 0)
                //{

                if (tc_pp_main.SelectedTab == tab_load_deflection)
                {
                    Set_Load_Deflection_Init();
                }
                else if (tc_pp_main.SelectedTab == tab_envelop)
                {
                    //Set_Load_Deflection_Init();

                }
                else if (tc_pp_main.SelectedTab == tab_diagram)
                {
                    //Set_Load_Deflection_Init();
                }
                else
                {
                    SetLayers(PP_ActiveDoc);
                    Draw_Joints(PP_ActiveDoc);
                    Draw_Plates(PP_ActiveDoc);
                    Draw_Solids(PP_ActiveDoc);
                }
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(PP_ActiveDoc);

            }
        }

        public void Tab_Post_Selection()
        {
            if (!File.Exists(ReportFileName))
            {
                dgv_beam_frcs.Rows.Clear();
                dgv_truss_frcs.Rows.Clear();
                dgv_plate_frcs.Rows.Clear();
                dgv_solid_frcs.Rows.Clear();
                dgv_max_frc.Rows.Clear();
                dgv_node_disp.Rows.Clear();
                return;
            }

            if (StructureAnalysis == null)
            {
                StructureAnalysis = new AstraInterface.DataStructure.BridgeMemberAnalysis(iApp, ReportFileName);
                if (PP_ActiveDoc != null)
                    PP_ActiveDoc.ActiveLayOut.Entities.EraseAll();
            }



            dgv_plate_frcs.Rows.Clear();
            dgv_beam_frcs.Rows.Clear();
            dgv_solid_frcs.Rows.Clear();
            cmb_diag_mem_no.Items.Clear();
            cmb_diag_ld_no.Items.Clear();
            foreach (var item in StructureAnalysis.Beams_Forces)
            {
                if (!cmb_diag_ld_no.Items.Contains(item.LoadNo))
                {
                    cmb_diag_ld_no.Items.Add(item.LoadNo);
                    //cmb_diag_mem_no.Items.Add(item.BeamNo);
                }
                if (!cmb_diag_mem_no.Items.Contains(item.BeamNo))
                {
                    //cmb_diag_ld_no.Items.Add(item.LoadNo);
                    cmb_diag_mem_no.Items.Add(item.BeamNo);
                }
                dgv_beam_frcs.Rows.Add(item.BeamNo, item.LoadNo,
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
            foreach (var item in StructureAnalysis.Trusses_Forces)
            {
                dgv_truss_frcs.Rows.Add(item.TrussMemberNo, item.LoadNo,
                    item.Stress,
                    item.Force);
            }
            foreach (var item in StructureAnalysis.Cables_Forces)
            {
                dgv_truss_frcs.Rows.Add(item.TrussMemberNo, item.LoadNo,
                    item.Stress,
                    item.Force);
            }

            dgv_plate_frcs.Rows.Clear();
            foreach (var item in StructureAnalysis.Plates_Forces)
            {
                dgv_plate_frcs.Rows.Add(item.PlateNo, item.LoadNo,
                    item.SXX.ToString("f4"),
                    item.SYY.ToString("f4"),
                    item.SXY.ToString("f4"),
                    item.MXX.ToString("f4"),
                    item.MXY.ToString("f4"),
                    item.MXY.ToString("f4"));
            }

            dgv_solid_frcs.Rows.Clear();
            foreach (var item in StructureAnalysis.Solids_Forces)
            {
                //dgv_solid_frcs.Rows.Add(item.ElementNo, item.LoadNo, item.Face,
                //    item.SIG_XX.ToString("f4"),
                //    item.SIG_YY.ToString("f4"),
                //    item.SIG_ZZ.ToString("f4"),
                //    item.SIG_XY.ToString("f4"),
                //    item.SIG_YZ.ToString("f4"),
                //    item.SIG_ZX.ToString("f4"),
                //    item.SIG_MAX.ToString("f4"),
                //    item.SIG_MIN.ToString("f4"),
                //    item.S2_Angle.ToString("f4"));
                dgv_solid_frcs.Rows.Add(item.ElementNo, item.LoadNo, item.Face,
                    item.SIG_XX,
                    item.SIG_YY,
                    item.SIG_ZZ,
                    item.SIG_XY,
                    item.SIG_YZ,
                    item.SIG_ZX,
                    item.SIG_MAX,
                    item.SIG_MIN,
                    item.S2_Angle);
            }
            //foreach (var item in StructureAnalysis.Cables_Forces)
            //{
            //    dgv_joint_frcs.Rows.Add(item.TrussMemberNo, item.LoadNo,
            //        item.Stress,
            //        item.Force);
            //}

            dgv_max_frc.Rows.Clear();

            if (StructureAnalysis.Analysis_Forces == null)
            {
                StructureAnalysis = new AstraInterface.DataStructure.BridgeMemberAnalysis(iApp, ReportFileName);
            }


            if (StructureAnalysis.Analysis_Forces != null)
            {
                foreach (var item in StructureAnalysis.Analysis_Forces)
                {
                    dgv_max_frc.Rows.Add(item.AstraMemberNo, item.AstraMemberType,
                                item.CompressionForce,
                                item.TensileForce,
                        item.MaxAxialForce,
                        item.MaxTorsion,
                        item.MaxBendingMoment,
                        item.MaxShearForce);
                }
            }


            double max_def = 0.0;
            int load_no = 0;
            int node_no = 0;


            dgv_node_disp.Rows.Clear();
            if (StructureAnalysis.Node_Displacements != null)
            {
                foreach (var item in StructureAnalysis.Node_Displacements)
                {
                    //dgv_node_disp.Rows.Add(nd.Node.NodeNo, nd.LoadCase, nd.Tx, nd.Ty, nd.Tz, nd.Rx, nd.Ry, nd.Rz);
                    dgv_node_disp.Rows.Add(item.NodeNo, item.LoadCase, item.X_Translation, item.Y_Translation
                        , item.Z_Translation, item.X_Rotation, item.Y_Rotation, item.Z_Rotation);


                    if (Math.Abs(item.Y_Translation) > Math.Abs(max_def))
                    {
                        max_def = item.Y_Translation;
                        load_no = item.LoadCase;
                        node_no = item.NodeNo;
                    }

                }
                txt_max_deflection.Text = max_def.ToString();
                txt_max_deflection_load.Text = load_no.ToString();
                txt_max_deflection_node.Text = node_no.ToString();

                txtDefFactor.Text = Math.Abs(1 / max_def).ToString("f3");
                if (SAP_DOC.DynamicAnalysis.FREQUENCIES > 0)
                {
                    lblLoadCase.Text = "Frequencies : Mode Shapes";
                }
                else
                {
                    lblLoadCase.Text = "LOAD CASE";
                }

            }

        }

        public void Set_Load_Deflection_Init()
        {
            //AST_DOC_ORG = new ASTRADoc(AST_DOC);
            //if (ld == null)
            //    ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);


            List<AstraInterface.DataStructure.NodeResults> NodeResults = new List<AstraInterface.DataStructure.NodeResults>();

            if (StructureAnalysis == null) return;
            if (StructureAnalysis.Node_Displacements == null) return;
            List<int> LoadCases = StructureAnalysis.Node_Displacements.Get_LoadCases();


            //if (cmbLoadCase.Items.Count > 0) return;

            cmbLoadCase.Items.Clear();

            if (Load_Deflections == null) Load_Deflections = new Hashtable();
            Load_Deflections.Clear();
            cmbLoadCase.Items.Add(0);

            foreach (var item in LoadCases)
            {
                Load_Deflections.Add(item, StructureAnalysis.Node_Displacements.Get_NodeResults(item));
                //NodeResults.Add();
                cmbLoadCase.Items.Add(item);
            }

            cmbLoadCase.SelectedIndex = 0;

            return;




            //NodeDisplacement nd = null;
            //dgv_node_disp.Rows.Clear();

            double max_def = 0.0;
            int load_no = 0;
            int node_no = 0;



            ASTRADoc adoc;
            Load_Deflections.Clear();
            JointCoordinateCollection jcols;
            foreach (var item in NodeResults)
            {
                item.Reverse();

                JointCoordinate jn;
                jcols = new JointCoordinateCollection();

                for (int i = 0; i < item.Count; i++)
                {
                    if (i == 0)
                    {
                        Load_Deflections.Add(item[i].LoadCase, jcols);
                    }
                    jn = new JointCoordinate();

                    jn.NodeNo = item[i].NodeNo;
                    //jn.Point.x = AST_DOC.Joints[i].Point.x + item[i].X_Translation * fact;
                    //jn.Point.y = AST_DOC.Joints[i].Point.y + item[i].Y_Translation * fact;
                    //jn.Point.z = AST_DOC.Joints[i].Point.z + item[i].Z_Translation * fact;

                    jn.Point.x = item[i].X_Translation;
                    jn.Point.y = item[i].Y_Translation;
                    jn.Point.z = item[i].Z_Translation;


                    jcols.Add(jn);

                    //dgv_node_disp.Rows.Add(item[i].NodeNo,
                    //    item[i].LoadCase,
                    //    item[i].X_Translation,
                    //    item[i].Y_Translation,
                    //    item[i].Z_Translation,
                    //    item[i].X_Rotation,
                    //    item[i].Y_Rotation,
                    //    item[i].Z_Rotation);

                    //if (Math.Abs(item[i].Y_Translation) > Math.Abs(max_def))
                    //{
                    //    max_def = item[i].Y_Translation;
                    //    load_no = item[i].LoadCase;
                    //    node_no = item[i].NodeNo;
                    //}
                }
            }
            txt_max_deflection.Text = max_def.ToString();
            txt_max_deflection_load.Text = load_no.ToString();
            txt_max_deflection_node.Text = node_no.ToString();


            cmbLoadCase.SelectedIndex = 0;

            //SetLoadIndex(1);


            //if (AST_DOC_ORG.IsDynamicLoad)
            //{
            //    AST_DOC_ORG.DrawMemberDetails(MainDoc, 1);
            //}
        }


        public JointCoordinateCollection Get_Deflected_Joints(int loadcase)
        {
            double fact = MyList.StringToDouble(txtDefFactor.Text, 1.0);

            AstraInterface.DataStructure.NodeResults NodeResult = Load_Deflections[loadcase] as AstraInterface.DataStructure.NodeResults;

            JointCoordinateCollection jcols = new JointCoordinateCollection();


            JointCoordinate jn = new JointCoordinate();
            if (jcols != null)
            {
                for (int i = 0; i < AST_DOC.Joints.Count; i++)
                {
                    jn = new JointCoordinate();

                    jn.NodeNo = AST_DOC.Joints[i].NodeNo;

                    //try
                    //{

                    //    jn.Point.x = AST_DOC.Joints[i].Point.x + NodeResult[i].X_Translation * fact;
                    //    jn.Point.y = AST_DOC.Joints[i].Point.y + NodeResult[i].Y_Translation * fact;
                    //    jn.Point.z = AST_DOC.Joints[i].Point.z + NodeResult[i].Z_Translation * fact;
                    //}
                    //catch(Exception exx)
                    //{

                    //    jn.Point.x = AST_DOC.Joints[i].Point.x;
                    //    jn.Point.y = AST_DOC.Joints[i].Point.y;
                    //    jn.Point.z = AST_DOC.Joints[i].Point.z;
                    //}
                    if (NodeResult == null || NodeResult.Count != AST_DOC.Joints.Count)
                    {
                        jn.Point.x = AST_DOC.Joints[i].Point.x;
                        jn.Point.y = AST_DOC.Joints[i].Point.y;
                        jn.Point.z = AST_DOC.Joints[i].Point.z;

                    }
                    else
                    {
                        jn.Point.x = AST_DOC.Joints[i].Point.x + NodeResult[i].X_Translation * fact;
                        jn.Point.y = AST_DOC.Joints[i].Point.y + NodeResult[i].Y_Translation * fact;
                        jn.Point.z = AST_DOC.Joints[i].Point.z + NodeResult[i].Z_Translation * fact;
                    }
                    jcols.Add(jn);
                }


            }



            return jcols;
        }

        private void dgv_max_frc_CellEnter(object sender, DataGridViewCellEventArgs e)
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

        private void tsb_pp_rot3D_Click(object sender, EventArgs e)
        {

            vdDocument VD = PP_ActiveDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 1) VD = defDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name == tsb_pp_rot3D.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name == tsb_pp_VTop.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name == tsb_pp_VBottom.Name)
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
            else if (tsb.Name == tsb_pp_ZoomAll.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name == tsb_pp_ZomExtent.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name == tsb_pp_ZomPrev.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name == tsb_pp_ZomWindow.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name == tsb_pp_ZoomIn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomOut.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name == tsb_pp_Pan.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name == tsb_pp_ShadeOn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name == tsb_pp_Wire2D.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(VD);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name == tsb_pp_Save.Name)
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

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            JointCoordinateCollection jcol = Get_Deflected_Joints(cmbLoadCase.SelectedIndex);

            if (jcol == null)
                jcol = AST_DOC.Joints;

            if (jcol != null)
            {
                PP_ActiveDoc.ActionLayout.Entities.EraseAll();
                Draw_Joints(jcol, PP_ActiveDoc);
                Draw_Members(jcol, PP_ActiveDoc);
                Draw_Plates(jcol, PP_ActiveDoc);
                Draw_Solids(jcol, PP_ActiveDoc);


            }
        }


        void Set_Load_Index(int loadcase)
        {

            //if (bIsNext)
            //{
            //iLoadCase++;
            if (iLoadCase > cmbLoadCase.Items.Count - 1)
                iLoadCase = 0;
            //}
            //else
            //{
            //iLoadCase--;
            if (iLoadCase < 0)
                iLoadCase = cmbLoadCase.Items.Count - 1;
            //}

            cmbLoadCase.SelectedIndex = iLoadCase;
        }

        Timer tmr_ldef;
        private void tmr_ldef_Tick(object sender, EventArgs e)
        {
            tmr_ldef.Interval = (int)(MyList.StringToDouble(cmbInterval.Text.Replace(" Sec", ""), 1.0) * 1000);
            if (tmr_ldef.Interval == 0)
                tmr_ldef.Interval = 1000;


            if (bIsNext)
            {
                iLoadCase++;
            }
            else
            {
                iLoadCase--;
            }
            Set_Load_Index(iLoadCase);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            int tm = (int)(MyList.StringToDouble(cmbInterval.Text.Replace(" Sec", ""), 1.0) * 1000);
            if (tm == 0)
                tm = 1000;
            if (tmr_ldef == null)
            {
                tmr_ldef = new Timer();
                tmr_ldef.Tick += tmr_ldef_Tick;
            }

            tmr_ldef.Interval = tm;
            if (btn.Name == btnAutoNext.Name)
            {
                bIsNext = true;
                tmr_ldef.Start();
            }
            else if (btn.Name == btnAutoPrev.Name)
            {
                bIsNext = false;
                tmr_ldef.Start();

            }
            else if (btn.Name == btnPrev.Name)
            {
                iLoadCase--;
                Set_Load_Index(iLoadCase);

            }
            else if (btn.Name == btnNext.Name)
            {

                iLoadCase++;
                Set_Load_Index(iLoadCase);
            }
            else if (btn.Name == btnStop.Name)
            {
                tmr_ldef.Stop();
                cmbLoadCase.SelectedIndex = 0;
            }
            else if (btn.Name == btnPause.Name)
            {
                tmr_ldef.Stop();

            }
            //else if (btn.Name == btn.Name)
            //{

            //}

        }

        private void chk_joints_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                CheckBox chk = sender as CheckBox;

                //VDoc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;

                vdDocument VD = VDoc;


                string lay_name = chk.Text.Substring(0, 4).ToLower();
                //if (tc4.SelectedIndex == 1)
                //    VD = defDoc;
                if (lay_name.StartsWith("plat"))
                {
                    lay_name = "elem";
                    if (chk.Checked)
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
                    else
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(VD);

                }
                //if (lay_name.StartsWith("soli"))
                //{
                //    lay_name = "soli";
                //    if (chk.Checked)
                //        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
                //    else
                //        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(VD);

                //}

                for (int i = 0; i < VD.Layers.Count; i++)
                {
                    if (VD.Layers[i].Name.ToLower().StartsWith(lay_name))
                    {
                        VD.Layers[i].Frozen = !chk.Checked;
                        VD.Layers[i].On = chk.Checked;
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

        private void cmb_diag_mem_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Diagram();
        }
        public void Select_Diagram()
        {

            int memNo = MyList.StringToInt(cmb_diag_mem_no.Text, 0);
            int loadNo = MyList.StringToInt(cmb_diag_ld_no.Text, 0);

            BeamForceMoment bfm = new BeamForceMoment();
            BeamForceMomentCollection bfc = new BeamForceMomentCollection();

            PP_ActiveDoc.Palette.Background = Color.White;
            if (StructureAnalysis != null)
            {
                AstraInterface.DataStructure.AstraBeamMember bm = null;
                if (memNo != 0 && loadNo != 0)
                {
                    bm = StructureAnalysis.Get_BeamMember_Force(memNo, loadNo);

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

                    bfc.DrawBeamForceMoment(PP_ActiveDoc, memNo, fc, loadNo);



                }

            }
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

            AST_DOC.Members.Clear();

            for (int i = 0; i < dgv_truss_elements.Rows.Count; i++)
            {
                MemberIncidence mi = new MemberIncidence();

                dgv_truss_elements[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                mi.MemberNo = MyList.StringToInt(dgv_truss_elements[0, i].Value.ToString(), 0);
                mi.StartNode.NodeNo = MyList.StringToInt(dgv_truss_elements[1, i].Value.ToString(), 0);
                mi.EndNode.NodeNo = MyList.StringToInt(dgv_truss_elements[2, i].Value.ToString(), 0);


                AST_DOC.Members.Add(mi);
            }
            AST_DOC.Members.CopyJointCoordinates(AST_DOC.Joints);


            foreach (var item in StructureAnalysis.Analysis.Members)
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
                        tensile_forces.Add(-ad.CompressionForce);
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

        private void cmb_structure_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw_Joints(VDoc);
        }

        private void tsb_Edit_Click(object sender, EventArgs e)
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
                        vdDocument VD = VDoc;


                        VD.SaveAs(sfd.FileName);
                        //Drawing_File = sfd.FileName;
                        System.Environment.SetEnvironmentVariable("OPENFILE", sfd.FileName);
                        //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                        System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                    }
                }
            }
        }

        private void btn_view_analysis_Click(object sender, EventArgs e)
        {
        }


        private void sc2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.Refresh();
        }

    }
}
