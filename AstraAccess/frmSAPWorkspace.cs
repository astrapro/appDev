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


namespace AstraAccess
{
    public partial class frmSAPWorkspace : Form
    {
        IApplication iApp;
        public string DataFileName { get; set; }

        public string ReportFileName
        {
            get
            {
                if(File.Exists(DataFileName))
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


        public frmSAPWorkspace(IApplication app)
        {
            //frmSAPWorkspace();
            InitializeComponent();
            iApp = app;
            DataFileName = iApp.SAP_File;
            SAP_DOC = new SAP_Document();
        }
        public frmSAPWorkspace(IApplication app, string file_name)
        {
            //frmSAPWorkspace();
            InitializeComponent();
            iApp = app;
            DataFileName = file_name;
            SAP_DOC = new SAP_Document();
        }

        public frmSAPWorkspace(IApplication app, string menu_name, bool flag)
        {
         
            try
            {

                //Project_Type = eASTRADesignType.Structural_Analysis_SAP;



                InitializeComponent();
                iApp = app;
                //DataFileName = file_name;
                SAP_DOC = new SAP_Document();

                MyList ml = new MyList(menu_name, ':');

                lbl_Title.Text = ml.StringList[1].ToUpper();

                //analysis_type = ml.StringList[0].ToUpper();


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

            if (tcParrent.SelectedTab == tab_post_process)
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

                if (tcParrent.SelectedTab == tab_post_process)
                    VD = PP_ActiveDoc;

                Deselect_All_Element(VD);

                foreach (vdFigure fig in gripset)
                {

                    fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_100;
                    if (fig is vdText)
                    {
                        vdText obj = fig as vdText;
                        if(obj.ToolTip.StartsWith("Node"))
                        {
                            if (tcParrent.SelectedTab != tab_pre_process) continue;

                            MyList ms = obj.TextString;
                            int nd_no = ms.GetInt(0);

                            for (int i = 0; i < dgv_joints.RowCount; i++)
                            {
                                if(MyList.StringToInt(dgv_joints[0,i].Value.ToString(), -1) == nd_no)
                                {
                                    dgv_joints[0, i].Selected = true;
                                    dgv_joints.FirstDisplayedScrollingRowIndex = i;
                                    break;
                                }
                            }
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

                if (tcParrent.SelectedTab == tab_post_process)
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
                return vdScrollableControl1.BaseControl.ActiveDocument;
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

        public ASTRADoc AST_DOC { set; get; }
        public SAP_Document SAP_DOC { set; get; }
        public frmSAPWorkspace()
        {
            InitializeComponent();
            DataFileName = "";
            SAP_DOC = new SAP_Document();
        }
        
        private void frmSAPWorkspace_Load(object sender, EventArgs e)
        {
            //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdScrollableControl1.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);


            vdSC_ldef.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdSC_ldef.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);


            vdSC_mfrc.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdSC_mfrc.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);   
        

            tc1.TabPages.Remove(tab_mov_load);
            tc1.TabPages.Remove(tab_constants);
            tc1.TabPages.Remove(tab_ana_spec);



            Load_Deflections = new Hashtable();

            SetLayers(VDoc);
            tc1.SelectedTab = tab_geom;

            for (int i = 1; i < 100; i++)
            {
                cmb_text_size.Items.Add(i.ToString());
                cmb_pp_text_size.Items.Add(i.ToString());
            }


            cmb_text_size.SelectedIndex = 5;
            cmb_pp_text_size.SelectedIndex = 5;


            Open_Data_File(DataFileName);

            tc1.SelectedTab = tab_file_open;
            cmbInterval.SelectedIndex = 9;
            timer1.Start();
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

            this.Text = "SAP ANALYSIS [" + MyList.Get_Modified_Path(file_name) + "]";
            if (AST_DOC == null)
                AST_DOC = new ASTRADoc();

            //VDoc.Palette.Background = Color.White;

            if (File.Exists(DataFileName))
            {
                SAP_DOC.Read_SAP_Data(DataFileName);

                MyList ml = SAP_DOC.HED;
                if (ml != "")
                {
                    if (ml.Count > 1)
                    {
                        cmb_structure_type.SelectedItem = ml.StringList[1];
                        txtUserTitle.Text = ml.GetString(2);
                    }
                }
                rtb_inputs.Lines = File.ReadAllLines(DataFileName);
            }
            else
                return;

            VDoc.ActiveLayOut.Entities.EraseAll();
            VDoc.ActiveLayOut.Entities.RemoveAll();


            VDoc.Redraw(true);

            if (maxDoc != null) maxDoc.ActiveLayOut.Entities.EraseAll();
            if (defDoc != null) defDoc.ActiveLayOut.Entities.EraseAll();
            if (envDoc != null) envDoc.ActiveLayOut.Entities.EraseAll();
            rtb_results.Text = "";
            lsv_steps.Items.Clear();

            //Tab_Post_Selection();


            if(SAP_DOC.MasterControl.NF >= 1)
            {
                SAP_DOC.DynamicAnalysis.AnalysisType =(eSAP_AnalysisType) (int) (SAP_DOC.MasterControl.NYDN);
                SAP_DOC.DynamicAnalysis.FREQUENCIES = (SAP_DOC.MasterControl.NF);

                chk_dynamic_analysis.Checked = true;
                if (SAP_DOC.MasterControl.NYDN == 1)
                    rbtn_perform_eigen.Checked = true;
                else if (SAP_DOC.MasterControl.NYDN == 2)
                    rbtn_perform_time_history.Checked = true;
                else if (SAP_DOC.MasterControl.NYDN == 3)
                    rbtn_response_spectrum.Checked = true;

                txt_frequencies.Text = SAP_DOC.DynamicAnalysis.FREQUENCIES.ToString();
                if (SAP_DOC.DynamicAnalysis.AnalysisType == eSAP_AnalysisType.ResponceSpectrum)
                {
                    txtCutOffFrequencies.Text = SAP_DOC.DynamicAnalysis.Response_spectrum.CUTOFF_FREQUENCY.ToString();
                    txtX.Text = SAP_DOC.DynamicAnalysis.Response_spectrum.X_DIRECTION_FACTORS.ToString();
                    txtY.Text = SAP_DOC.DynamicAnalysis.Response_spectrum.Y_DIRECTION_FACTORS.ToString();
                    txtZ.Text = SAP_DOC.DynamicAnalysis.Response_spectrum.Z_DIRECTION_FACTORS.ToString();
                    txtScaleFactor.Text = SAP_DOC.DynamicAnalysis.Response_spectrum.SCALE_FACTOR.ToString();

                    dgv_acceleration.Rows.Clear();
                    for(int i = 0; i < SAP_DOC.DynamicAnalysis.Response_spectrum.Periods.Count; i++)
                    {
                        dgv_acceleration.Rows.Add(SAP_DOC.DynamicAnalysis.Response_spectrum.Periods[i],
                            SAP_DOC.DynamicAnalysis.Response_spectrum.Values[i]);
                    }

                    if (SAP_DOC.DynamicAnalysis.Response_spectrum.IS_DISPLACEMENT)
                    {
                        rbtnDisplacement.Checked = true;
                    }
                    else
                    {
                        rbtnAcceleration.Checked = true;
                    }

                }
                else if (SAP_DOC.DynamicAnalysis.AnalysisType == eSAP_AnalysisType.TimeHistory)
                {

                    txt_time_steps.Text = SAP_DOC.DynamicAnalysis.Time_History.TIME_STEPS.ToString();
                    txt_time_print_interval.Text = SAP_DOC.DynamicAnalysis.Time_History.PRINT_INTERVAL.ToString();
                    txt_time_step_interval.Text = SAP_DOC.DynamicAnalysis.Time_History.STEP_INTERVAL.ToString();
                    txt_time_damp_fact.Text = SAP_DOC.DynamicAnalysis.Time_History.DAMPING_FACTOR.ToString();
                    txt_time_x_divs.Text = SAP_DOC.DynamicAnalysis.Time_History.X_DIVISION.ToString();
                    txt_time_scale_fact.Text = SAP_DOC.DynamicAnalysis.Time_History.SCALE_FACTOR.ToString();


                    rbtn_time_ground_TX.Checked = SAP_DOC.DynamicAnalysis.Time_History.GROUND_MOTION.Tx;
                    rbtn_time_ground_TY.Checked = SAP_DOC.DynamicAnalysis.Time_History.GROUND_MOTION.Ty;
                    rbtn_time_ground_TZ.Checked = SAP_DOC.DynamicAnalysis.Time_History.GROUND_MOTION.Tz;
                    rbtn_time_ground_RX.Checked = SAP_DOC.DynamicAnalysis.Time_History.GROUND_MOTION.Rx;
                    rbtn_time_ground_RY.Checked = SAP_DOC.DynamicAnalysis.Time_History.GROUND_MOTION.Ry;
                    rbtn_time_ground_RZ.Checked = SAP_DOC.DynamicAnalysis.Time_History.GROUND_MOTION.Rz;

                    dgv_time_function.Rows.Clear();
                    for (int i = 0; i < SAP_DOC.DynamicAnalysis.Time_History.TIME_VALUES.Count; i++)
                    {
                        dgv_time_function.Rows.Add(SAP_DOC.DynamicAnalysis.Time_History.TIME_VALUES[i],
                            SAP_DOC.DynamicAnalysis.Time_History.TIME_FUNCTION[i]);
                    }


                    dgv_time_nodal.Rows.Clear();
                    foreach (var item in SAP_DOC.DynamicAnalysis.Time_History.NODAL_CONSTRAINTS)
                    {
                        dgv_time_nodal.Rows.Add(item.NodeNo, item.Tx, item.Ty, item.Tz, item.Rx, item.Ry, item.Rz);
                    }
                    dgv_time_mbr_strs.Rows.Clear();
                    foreach (var item in SAP_DOC.DynamicAnalysis.Time_History.STRESS_COMPONENTS)
                    {
                        dgv_time_mbr_strs.Rows.Add(item.NodeNo, item.TEXT);
                    }

                  
                }
            }

            dgv_joints.Rows.Clear();
            foreach (var item in SAP_DOC.Joints)
            {
                dgv_joints.Rows.Add(item.NodeNo, item.X.ToString("f4"), item.Y.ToString("f4"), item.Z.ToString("f4")
                    , item.Tx, item.Ty, item.Tz, item.Rx, item.Ry, item.Rz);
            }
            dgv_truss_elements.Rows.Clear();
            foreach (var item in SAP_DOC.Trusses)
            {
                dgv_truss_elements.Rows.Add(item.Element_No,
                    item.Node_I,
                    item.Node_J,
                    item.Material_Property_No);
            }
            dgv_beam_elements.Rows.Clear();
            foreach (var item in SAP_DOC.Beams)
            {
                dgv_beam_elements.Rows.Add(item.Element_No,
                    item.Node_I,
                    item.Node_J,
                    item.Node_K,
                    item.Material_Property_No,
                    item.Element_Property_No,
                    item.End_Force_No,
                    item.NODE_I_Release_Code,
                    item.NODE_J_Release_Code);
            }


            dgv_solid_elements.Rows.Clear();
            foreach (var item in SAP_DOC.Solids)
            {
                dgv_solid_elements.Rows.Add(item.Element_No,
                    item.Node_1,
                    item.Node_2,
                    item.Node_3,
                    item.Node_4,
                    item.Node_5,
                    item.Node_6,
                    item.Node_7,
                    item.Node_8,
                    item.Integration_Order,
                    item.Material_No,
                    item.Generation_Parameter,
                    item.LSA,
                    item.LSB,
                    item.LSC,
                    item.LSD,
                    item.Face_Number,
                    item.Stress_free_Temperature);
            }









            dgv_plate_elements.Rows.Clear();
            foreach (var item in SAP_DOC.Plates)
            {
                dgv_plate_elements.Rows.Add(item.Element_No,
                    item.Node_I,
                    item.Node_J,
                    item.Node_K,
                    item.Node_L,
                    item.Node_O,
                    item.Material_Property_No,
                    item.Element_Data_Generator,
                    item.Element_Thickness,
                    item.Element_Pressure,
                    item.Mean_Temperature_Variation,
                    item.Mean_Temperature_Gradient);
            }


            dgv_boundary_elements.Rows.Clear();
            foreach (var item in SAP_DOC.Boundaries)
            {
                dgv_boundary_elements.Rows.Add(item.Node_N,
                    item.Node_I,
                    item.Node_J,
                    item.Node_K,
                    item.Node_L,
                    item.Displacement_Code,
                    item.Rotation_Code,
                    item.Data_Generator,
                    item.Displacement,
                    item.Roration,
                    item.Spring_Stiffness);
            }


            dgv_trss_mat_props.Rows.Clear();

            foreach (var item in SAP_DOC.Truss_Mat_Properties)
            {
                dgv_trss_mat_props.Rows.Add(item.Material_No,
                    item.Modulus_of_Elasticity,
                    item.Thermal_Coefficient,
                    item.Mass_Density,
                    item.Cross_Sectional_Area,
                    item.Weight_Density);
            }

            dgv_beams_mat_props.Rows.Clear();

            foreach (var item in SAP_DOC.Beam_Mat_Properties)
            {
                dgv_beams_mat_props.Rows.Add(item.Material_No,
                    item.Youngs_Modulus,
                    item.Poisson_Ratio,
                    item.Mass_Density,
                    item.Weight_Density);
            }

            dgv_beams_sect_props.Rows.Clear();

            foreach (var item in SAP_DOC.Beam_Sect_Properties)
            {
                dgv_beams_sect_props.Rows.Add(item.Property_No,
                    item.AX,
                    item.AY,
                    item.AZ,
                    item.IX,
                    item.IY,
                    item.IZ);
            }



            dgv_solids_mat_props.Rows.Clear();

            foreach (var item in SAP_DOC.Solid_Mat_Properties)
            {
                dgv_solids_mat_props.Rows.Add(item.Material_No,
                    item.Modulus_of_Elasticity,
                    item.Poisson_Ratio,
                    item.Weight_Density,
                    item.Thermal_Coefficient);
            }


            dgv_plates_mat_props.Rows.Clear();

            foreach (var item in SAP_DOC.Plate_Mat_Properties)
            {
                dgv_plates_mat_props.Rows.Add(item.Material_No,
                    item.Mass_Density,
                    item.Thermal_Coefficient_Ax,
                    item.Thermal_Coefficient_Ay,
                    item.Thermal_Coefficient_Axy,
                    item.Elasticity.Cxx,
                    item.Elasticity.Cxy,
                    item.Elasticity.Cxs,
                    item.Elasticity.Cyy,
                    item.Elasticity.Cys,
                    item.Elasticity.Gxy );
            }

            dgv_joint_loads.Rows.Clear();

            foreach (var item in SAP_DOC.Joint_Loads)
            {
                dgv_joint_loads.Rows.Add(item.Joint_No,
                    item.Load_No,
                    item.FX.ToString("f3"),
                    item.FY.ToString("f3"),
                    item.FZ.ToString("f3"),
                    item.MX.ToString("f3"),
                    item.MY.ToString("f3"),
                    item.MZ.ToString("f3"));
            }



            dgv_solid_loads.Rows.Clear();

            foreach (var item in SAP_DOC.Solid_Distributed_Loads)
            {
                dgv_solid_loads.Rows.Add(item.Load_No,
                    item.LT,
                    item.P,
                    item.Y,
                    item.Element_Face_No);
            }


            Draw_Joints(dgv_joints);
            Draw_Members();
            Draw_Plates();
            Draw_Solids();

            cmb_text_size.SelectedIndex = 5;
        }
        
        public void Draw_Joints(DataGridView DGV)
        {
            Draw_Joints(VDoc);
        }


        public void Draw_Joints(vdDocument doc)
        {
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

            tv_supports.Nodes.Clear();
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

                if(FX && FY && FZ && MX && MY && MZ)
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
                    if (cmb_structure_type.SelectedIndex != 1)
                    {
                        sp = new Support();
                        sp.Option = Support.SupportOption.FIXED;
                        sp.Node = jn;
                        list_supports.Add(sp);

                        sup = "FIXED BUT";

                        if (FX) sup += " FX";
                        if (FY) sup += " FY";
                        if (FZ) sup += " FZ";
                        if (MX) sup += " MX";
                        if (MY) sup += " MY";
                        if (MZ) sup += " MZ";
                    }
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
                    tv_supports.Nodes.Add(jn.NodeNo + " " + sup);
                }

            }

            Draw_Joints(AST_DOC.Joints, doc);
            Draw_Members(AST_DOC.Joints, doc);
            list_supports.DrawSupport(doc);

            doc.Redraw(true);
        }


        public void Draw_Joints(JointCoordinateCollection list,  vdDocument doc)
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

        public void Draw_Beams(JointCoordinateCollection jcols, vdDocument doc)
        {

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

            Delete_Layer_Objects( trussesLay, doc);


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

            Draw_Plates(AST_DOC.Joints, doc);
        }
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
                if(item is vdPolyface)
                {
                    vdPolyface vpf = item as vdPolyface;
                    kString st = vpf.ToolTip;
                    vpf.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                    MyList mll = vpf.ToolTip;
                    if (mll[0].StartsWith("Solid"))
                    {

                        int el = mll.GetInt(2);
                        if(el == elment_no)
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

        private void Tab_Selection()
        {
            //sc2.Panel2Collapsed = true;
            sc2.Panel2Collapsed = (tc1.SelectedTab == tab_file_open);


            sc1.Panel2Collapsed = (tc1.SelectedTab == tab_file_open);

            //if ((tc1.SelectedTab == tab_file_open))
            //{
            //    sc1.Panel2Collapsed = true;
            //}
            //else
            //    sc1.Panel2.Si = true;





            //if (tc1.SelectedTab == tab_file_open)
            //    Show_Panel(tab1_inputs);


            if (tc1.SelectedTab == tab_geom)
                Show_Panel(tab1_geom);
            else if (tc1.SelectedTab == tab_props)
                Show_Panel(tab1_props);
            else if (tc1.SelectedTab == tab_elements)
                Show_Panel(tab1_elements);
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
            tc2.TabPages.Clear();
            tc2.TabPages.Add(tp);
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



            DataGridView DGV = dgv_joints;

            SAP_DOC.ClearAll();

            int i = 0;
            int c = 0;
            SAP_DOC.HED = "ASTRA " + cmb_structure_type.Text + " " + txtUserTitle.Text;
            for (i = 0; i < DGV.RowCount; i++)
            {
                SAP_Joint sp = new SAP_Joint();
                c = 0;
                sp.NodeNo = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                sp.X = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                sp.Y = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                sp.Z = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                sp.Tx = (bool)DGV[c++, i].Value;
                sp.Ty = (bool)DGV[c++, i].Value;
                sp.Tz = (bool)DGV[c++, i].Value;
                sp.Rx = (bool)DGV[c++, i].Value;
                sp.Ry = (bool)DGV[c++, i].Value;
                sp.Rz = (bool)DGV[c++, i].Value;
                SAP_DOC.Joints.Add(sp);
            }

            DGV = dgv_trss_mat_props;

            #region Truss Material Properties
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Truss_Material_Property tmp = new Truss_Material_Property();

                tmp.Material_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Modulus_of_Elasticity = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                tmp.Thermal_Coefficient = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                tmp.Mass_Density = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                tmp.Cross_Sectional_Area = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                tmp.Weight_Density = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);

                SAP_DOC.Truss_Mat_Properties.Add(tmp);
            }
            #endregion Truss Material Properties

            DGV = dgv_truss_elements;
            #region Truss Elements
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Truss_Element tmp = new Truss_Element();

                tmp.Element_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_I = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);
                tmp.Node_J = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);
                tmp.Material_Property_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);
                tmp.Ref_Temp = "  0.0    0       0.0    0               000000000000";

                SAP_DOC.Trusses.Add(tmp);
            }
            #endregion Truss Material Properties


            DGV = dgv_beams_mat_props;
            #region Beam  Material Properties
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Beam_Material_Property tmp = new Beam_Material_Property();

                tmp.Material_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Youngs_Modulus = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Poisson_Ratio = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Mass_Density = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Weight_Density = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);


                SAP_DOC.Beam_Mat_Properties.Add(tmp);
            }
            #endregion Beam Material Properties

            DGV = dgv_beams_sect_props;
            #region Beam  Section Properties
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Beam_Section_Property tmp = new Beam_Section_Property();

                tmp.Property_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.AX = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.AY = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.AZ = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.IX = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.IY = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.IZ = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);

                SAP_DOC.Beam_Sect_Properties.Add(tmp);
            }
            #endregion Beam Section Properties

            DGV = dgv_beam_elements;
            #region Beam  Elements
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Beam_Element tmp = new Beam_Element();

                tmp.Element_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_I = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_J = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_K = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Material_Property_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Element_Property_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.End_Force_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.NODE_I_Release_Code = DGV[c++, i].Value.ToString();
                tmp.NODE_J_Release_Code = DGV[c++, i].Value.ToString();
                SAP_DOC.Beams.Add(tmp);
            }
            #endregion Beam Elements



            DGV = dgv_solids_mat_props;
            #region Solid Material Properties
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Solid_Material_Property tmp = new Solid_Material_Property();

                tmp.Material_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Modulus_of_Elasticity = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Poisson_Ratio = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Weight_Density = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Thermal_Coefficient = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);


                SAP_DOC.Solid_Mat_Properties.Add(tmp);
            }
            #endregion Solid Material Properties

            DGV = dgv_solid_loads;
            #region  Solid Surface Loads
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Solid_Surface_Loads tmp = new Solid_Surface_Loads();

                tmp.Load_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.LT = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);
                tmp.P = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);
                tmp.Y = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);
                tmp.Element_Face_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 1);

                SAP_DOC.Solid_Distributed_Loads.Add(tmp);
            }
            #endregion Solid Surface Loads

            SAP_DOC.Solid_Acceleration = MyList.StringToDouble(txt_solid_acc_gvt.Text, 0.11);

            DGV = dgv_solid_elements;
            #region Solid  Elements

            
            for (i = 0; i < DGV.RowCount; i++)
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

            #endregion Solid Elements

            DGV = dgv_plates_mat_props;
            #region Plate  Material Properties
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Plate_Material_Property tmp = new Plate_Material_Property();

                tmp.Material_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Mass_Density = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Thermal_Coefficient_Ax = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Thermal_Coefficient_Ay = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Thermal_Coefficient_Axy = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Elasticity.Cxx = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Elasticity.Cxy = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Elasticity.Cxs = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Elasticity.Cyy = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Elasticity.Cys = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);
                tmp.Elasticity.Gxy = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 1);


                SAP_DOC.Plate_Mat_Properties.Add(tmp);
            }
            #endregion Plate Material Properties

            DGV = dgv_plate_elements;
            #region Plate Elements
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Plate_Element tmp = new Plate_Element();

                tmp.Element_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_I = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_J = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_K = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_L = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_O = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Material_Property_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Element_Data_Generator = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Element_Thickness = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.Element_Pressure = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.Mean_Temperature_Variation = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.Mean_Temperature_Gradient = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                SAP_DOC.Plates.Add(tmp);
            }
            #endregion Plate Elements


            DGV = dgv_boundary_elements;
            #region Boundary Elements
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                Boundary_Element tmp = new Boundary_Element();

                tmp.Node_N = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_I = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_J = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_K = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Node_L = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Displacement_Code = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Rotation_Code = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Data_Generator = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Displacement = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                tmp.Roration = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                tmp.Spring_Stiffness = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0.0);
                SAP_DOC.Boundaries.Add(tmp);
            }
            #endregion Boundary Elements


            DGV = dgv_joint_loads;
            #region Joint  Loads
            for (i = 0; i < DGV.RowCount; i++)
            {
                c = 0;

                SAP_Joint_Load tmp = new SAP_Joint_Load();

                tmp.Joint_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.Load_No = MyList.StringToInt(DGV[c++, i].Value.ToString(), 0);
                tmp.FX = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.FY = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.FZ = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.MX = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.MY = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);
                tmp.MZ = MyList.StringToDouble(DGV[c++, i].Value.ToString(), 0);

                SAP_DOC.Joint_Loads.Add(tmp);
            }
            #endregion Beam Elements

            #region Dynamic Analysis
            if (chk_dynamic_analysis.Checked)
            {
                if (rbtn_perform_eigen.Checked)
                {
                    SAP_DOC.DynamicAnalysis.AnalysisType = eSAP_AnalysisType.EigenValue;
                }
                else if (rbtn_perform_time_history.Checked)
                {
                    SAP_DOC.DynamicAnalysis.AnalysisType = eSAP_AnalysisType.TimeHistory;


                    Time_History_Function thf = SAP_DOC.DynamicAnalysis.Time_History;

                    thf.TIME_STEPS = MyList.StringToDouble(txt_time_steps.Text, 0.0);
                    thf.PRINT_INTERVAL = MyList.StringToDouble(txt_time_print_interval.Text, 0.0);
                    thf.STEP_INTERVAL = MyList.StringToDouble(txt_time_step_interval.Text, 0.0);
                    thf.DAMPING_FACTOR = MyList.StringToDouble(txt_time_damp_fact.Text, 0.0);

                    thf.GROUND_MOTION.Tx = rbtn_time_ground_TX.Checked;
                    thf.GROUND_MOTION.Ty = rbtn_time_ground_TY.Checked;
                    thf.GROUND_MOTION.Tz = rbtn_time_ground_TZ.Checked;
                    thf.GROUND_MOTION.Rx = rbtn_time_ground_RX.Checked;
                    thf.GROUND_MOTION.Ry = rbtn_time_ground_RY.Checked;
                    thf.GROUND_MOTION.Rz = rbtn_time_ground_RZ.Checked;

                    thf.X_DIVISION = MyList.StringToDouble(txt_time_x_divs.Text, 0.0);
                    thf.SCALE_FACTOR = MyList.StringToDouble(txt_time_scale_fact.Text, 0.0);

                    DGV = dgv_time_function;
                    for (i = 0; i < DGV.RowCount - 1; i++)
                    {
                        thf.TIME_VALUES.Add(MyList.StringToDouble(DGV[0, i].Value.ToString(), 0.0));
                        thf.TIME_FUNCTION.Add(MyList.StringToDouble(DGV[1, i].Value.ToString(), 0.0));
                    }


                    DGV = dgv_time_nodal;
                    for (i = 0; i < DGV.RowCount - 1; i++)
                    {
                        NODAL_CONSTRAINT_DOF nc = new NODAL_CONSTRAINT_DOF();


                        nc.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                        if (DGV[1, i].Value != null)
                            nc.Tx = (bool)DGV[1, i].Value;
                        else
                            nc.Tx = false;

                        if (DGV[2, i].Value != null)
                            nc.Ty = (bool)DGV[2, i].Value;
                        else
                            nc.Ty = false;
                        if (DGV[3, i].Value != null)
                            nc.Tz = (bool)DGV[3, i].Value;
                        else
                            nc.Tz = false;
                        if (DGV[4, i].Value != null)
                            nc.Rx = (bool)DGV[4, i].Value;
                        else
                            nc.Rx = false;
                        if (DGV[5, i].Value != null)
                            nc.Ry = (bool)DGV[5, i].Value;
                        else
                            nc.Ry = false;
                        if (DGV[5, i].Value != null)
                            nc.Rz = (bool)DGV[6, i].Value;
                        else
                            nc.Rz = false;

                        if (nc.NodeNo != 0)
                            thf.NODAL_CONSTRAINTS.Add(nc);
                    }

                    DGV = dgv_time_mbr_strs;
                    for (i = 0; i < DGV.RowCount; i++)
                    {
                        MEMBER_STRESS_COMPONENT msc = new MEMBER_STRESS_COMPONENT();
                        msc.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                        msc.IS_START = (DGV[1, i].Value.ToString().StartsWith("S"));
                        if (msc.NodeNo != 0)
                            thf.STRESS_COMPONENTS.Add(msc);
                    }



                }
                else if (rbtn_response_spectrum.Checked)
                {
                    SAP_DOC.DynamicAnalysis.AnalysisType = eSAP_AnalysisType.ResponceSpectrum;


                    Response_Spectrum_Analysis rs = SAP_DOC.DynamicAnalysis.Response_spectrum;
                    rs.CUTOFF_FREQUENCY = MyList.StringToDouble(txtCutOffFrequencies.Text, 0.0);
                    rs.IS_DISPLACEMENT = rbtnDisplacement.Checked;
                    rs.SCALE_FACTOR = MyList.StringToDouble(txtScaleFactor.Text, 0.0);
                    rs.X_DIRECTION_FACTORS = MyList.StringToDouble(txtX.Text, 0.0);
                    rs.Y_DIRECTION_FACTORS = MyList.StringToDouble(txtY.Text, 0.0);
                    rs.Z_DIRECTION_FACTORS = MyList.StringToDouble(txtZ.Text, 0.0);
                    DGV = dgv_acceleration;
                    for (i = 0; i < DGV.RowCount - 1; i++)
                    {
                        rs.Periods.Add(MyList.StringToDouble(DGV[0, i].Value.ToString(), 0.0));
                        rs.Values.Add(MyList.StringToDouble(DGV[0, i].Value.ToString(), 0.0));
                    }

                }
                SAP_DOC.DynamicAnalysis.FREQUENCIES = MyList.StringToInt(txt_frequencies.Text, 3);
            }
            else
            {
                SAP_DOC.DynamicAnalysis.AnalysisType = eSAP_AnalysisType.StaticAnalysis;
                SAP_DOC.DynamicAnalysis.FREQUENCIES = 0;
            }
            #endregion Dynamic Analysis

            rtb_inputs.Lines = SAP_DOC.Get_SAP_Data().ToArray();

            File.WriteAllLines(DataFileName, rtb_inputs.Lines);
        }

        public void RunAnalysis()
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
                File.WriteAllLines(flPath, rtb_inputs.Lines);
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
        public double Text_Size
        {
            get
            {
                if(tcParrent.SelectedTab == tab_post_process)
                    return (cmb_pp_text_size.SelectedIndex + 1) * 0.01;
                return (cmb_text_size.SelectedIndex + 1) * 0.01;
            }
        }
        private void tsmi_data_open_Click(object sender, EventArgs e)
        {
            ToolStripItem tsmi = sender as ToolStripItem;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                #region Data Open
                if (tsmi.Name == tsmi_data_open.Name || tsmi.Name == tsb_open_data.Name)
                {
                    ofd.Filter = "SAP Data Files (*.txt;*.sap)|*.txt;*.sap";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        Open_Data_File(ofd.FileName);
                    }
                }
                #endregion Data Open
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

        }

        private void rtb_inputs_TextChanged(object sender, EventArgs e)
        {

        }

     
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
        }
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
            vdDocument VD = VDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 1) VD = defDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name == tsb_3D_rotate.Name )
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
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(VD);
                //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
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

        private void btn_jnt_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_jnt_add.Name)
            {
                frm_SAP_Joints frm = new frm_SAP_Joints(dgv_joints, false);
                frm.DrawJoints = new deleDrawJoints(Draw_Joints);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_jnt_edit.Name)
            {
                frm_SAP_Joints frm = new frm_SAP_Joints(dgv_joints, true);
                frm.DrawJoints = new deleDrawJoints(Draw_Joints);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_jnt_delete.Name)
            {
                if (dgv_joints.SelectedCells.Count > 0)
                    dgv_joints.Rows.RemoveAt(dgv_joints.SelectedCells[0].RowIndex);
            }
            else if (btn.Name == btn_jnt_insert.Name)
            {
                frm_SAP_Joints frm = new frm_SAP_Joints(dgv_joints, false);
                frm.IsInsert = true;
                frm.DrawJoints = new deleDrawJoints(Draw_Joints);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_support_add.Name)
            {
                frm_Supports frm = new frm_Supports(dgv_joints, false);
                frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                frm.DrawJoints = new deleDrawJoints(Draw_Joints);
                frm.DGV_Boundary = dgv_boundary_elements;
                frm.ShowDialog();
            }
            Draw_Joints(dgv_joints);
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
            if(iApp.IsDemo)
            {
                if (!iApp.Check_Coordinate(dgv_joints.RowCount, (dgv_beam_elements.RowCount + dgv_truss_elements.RowCount)))
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

                    iApp.Check_Demo_Version(true);
                    return;
                }
            }

            RunAnalysis();

            string res_file = Path.Combine(Path.GetDirectoryName(DataFileName), "RES001.tmp");
            if(File.Exists(res_file))
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
                list.Add(string.Format("                                *           ASTRA Pro Release 15 Version 00         *"));
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


                rtb_results.Lines = file_cont.ToArray();

                res_file = Path.Combine(Path.GetDirectoryName(res_file), "SAP_ANALYSIS_REP.TXT");
                File.WriteAllLines(res_file, rtb_results.Lines);
                iApp.Delete_Temporary_Files(DataFileName);
                MessageBox.Show(this, "Analysis Results are written in file 'SAP_ANALYSIS_REP.TXT'.\n\nFile Path:\n\n" + res_file, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show(this, "Analysis Results are written in file 'SAP_ANALYSIS_REP.TXT'.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //if (MessageBox.Show(this, "Analysis Results are written in file 'SAP_ANALYSIS_REP.TXT'.\n\n Do you want to open the File ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    //iApp.View_Result(res_file);

                Select_Steps();
                //StructureAnalysis = new AstraInterface.DataStructure.BridgeMemberAnalysis(iApp, ReportFileName);
                StructureAnalysis = null;


                if (maxDoc != null) maxDoc.ActiveLayOut.Entities.RemoveAll();
                if (defDoc != null) defDoc.ActiveLayOut.Entities.RemoveAll();
                if (envDoc != null) envDoc.ActiveLayOut.Entities.RemoveAll();
                if (diagDoc != null) diagDoc.ActiveLayOut.Entities.RemoveAll();

                //Tab_Post_Selection();

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
            analysis_list.Add(string.Format("  .....8-NODE SOLID ELEMENT STRESSES"));
         
            analysis_list.Add(string.Format(" THIN  PLATE/SHELL  ELEMENT DATA"));
            analysis_list.Add(string.Format(" T H I N   P L A T E / S H E L L   E L E M E N T S"));
            analysis_list.Add(string.Format("S T A T I C   S O L U T I O N   T I M E   L O G"));
            analysis_list.Add(string.Format("O V E R A L L   T I M E   L O G"));
            analysis_list.Add(string.Format("SUMMARY OF MAXIMUM SUPPORT FORCES"));



            analysis_list.Add(string.Format("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD"));
            analysis_list.Add(string.Format("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD"));

            analysis_list.Add(string.Format("T I M E   H I S T O R Y   R E S P O N S E"));
            analysis_list.Add(string.Format("D I S P L A C E M E N T   T I M E   H I S T O R Y"));
            analysis_list.Add(string.Format("F O R C E D   R E S P O N S E   A N A L Y S I S"));
            analysis_list.Add(string.Format("E I G E N V A L U E   A N A L Y S I S"));

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


            List<string> list = new List<string>(rtb_results.Lines);
            //list.Sort();
            int indx = 0;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
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
                RichTextBox rtbData = rtb_results;
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

        private void tsb_print_Click(object sender, EventArgs e)
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

        private void tsmi_file_save_Click(object sender, EventArgs e)
        {

            ToolStripItem tsi = sender as ToolStripItem;

            if (tsi.Name == tsmi_file_save_as.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "TEXT Files (*.txt)|*.txt";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {

                        DataFileName = sfd.FileName;

                        if(File.Exists(DataFileName) == false)
                        {
                            File.WriteAllText(DataFileName, "");
                        }
                    }
                    else
                        return;
                }
            }

            Save_Data();
            MessageBox.Show("Data successfully Saved.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            //if(cmb.Name == cmb_pp_text_size.Name)
            //    cmb_text_size.SelectedIndex = cmb_pp_text_size.SelectedIndex;
            //else
            //cmb_pp_text_size.SelectedIndex = cmb_text_size.SelectedIndex;

            if(cmb.Name == cmb_pp_text_size.Name)
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

        private void btn_jload_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DataGridView DGV = dgv_joint_loads;

            if (btn.Name == btn_jload_add.Name)
            {
                frm_Joint_Loads frm = new frm_Joint_Loads(DGV, false);
                frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_jload_edit.Name)
            {
                frm_Joint_Loads frm = new frm_Joint_Loads(DGV, true);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_jload_del.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void btn_trss_mat_props_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DataGridView DGV = dgv_trss_mat_props;

            if (btn.Name == btn_trss_mat_props_add.Name)
            {
                frm_Truss_Mat_Props frm = new frm_Truss_Mat_Props(DGV, false);
                frm.ShowDialog();
            }
            if (btn.Name == btn_trss_mat_props_edit.Name)
            {
                frm_Truss_Mat_Props frm = new frm_Truss_Mat_Props(DGV, true);
                frm.ShowDialog();
            }
            if (btn.Name == btn_trss_mat_props_del.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
            //Draw_Joints(dgv_joints);
        }

        private void btn_beam_mat_props_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_beams_mat_props;

            if (btn.Name == btn_beam_mat_props_add.Name)
            {
                frm_Beam_Mat_Props frm = new frm_Beam_Mat_Props(DGV, false);
                frm.ShowDialog();
            }
            if (btn.Name == btn_beam_mat_props_edit.Name)
            {
                frm_Beam_Mat_Props frm = new frm_Beam_Mat_Props(DGV, true);
                frm.ShowDialog();
            }
            if (btn.Name == btn_beam_mat_props_del.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void btn_beam_sec_props_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_beams_sect_props;

            if (btn.Name == btn_beam_sec_props_add.Name)
            {
                frm_Beam_Sect_Props frm = new frm_Beam_Sect_Props(DGV, false);
                frm.ShowDialog();
            }
            if (btn.Name == btn_beam_sec_props_edit.Name)
            {
                frm_Beam_Sect_Props frm = new frm_Beam_Sect_Props(DGV, true);
                frm.ShowDialog();
            }
            if (btn.Name == btn_beam_sec_props_del.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void btn_trss_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DataGridView DGV = dgv_truss_elements;

            if (btn.Name == btn_trss_add.Name)
            {
                frm_Truss_Element frm = new frm_Truss_Element(DGV, false);
                frm.DrawTrusses = new deleDrawTrusses(Draw_Trusses);
                frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_trss_edit.Name)
            {
                frm_Truss_Element frm = new frm_Truss_Element(DGV, true);
                frm.DrawTrusses = new deleDrawTrusses(Draw_Trusses);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_trss_del.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
            else if (btn.Name == btn_trss_insert.Name)
            {
                frm_Truss_Element frm = new frm_Truss_Element(DGV, false);
                frm.IsInsert = true;
                frm.DrawTrusses = new deleDrawTrusses(Draw_Trusses);
                frm.ShowDialog();
            }
            Draw_Members();
        }

        private void btn_mbr_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DataGridView DGV = dgv_beam_elements;

            if (btn.Name == btn_mbr_add.Name)
            {
                frm_Beam_Elements frm = new frm_Beam_Elements(DGV, false);
                frm.DrawBeams = new deleDrawMembers(Draw_Members);
                frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_mbr_edit.Name)
            {
                frm_Beam_Elements frm = new frm_Beam_Elements(DGV, true);
                frm.DrawBeams = new deleDrawMembers(Draw_Members);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_mbr_delete.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
            else if (btn.Name == btn_mbr_insert.Name)
            {
                frm_Beam_Elements frm = new frm_Beam_Elements(DGV, false);
                frm.DrawBeams = new deleDrawMembers(Draw_Members);
                frm.IsInsert = true;
                frm.ShowDialog();
            }
            else if (btn.Name == btn_mbr_release.Name)
            {

                frm_Member_Release frm = new frm_Member_Release(DGV, true);
                frm.ShowDialog();
            }

            Draw_Members();
        }

        private void rtb_inputs_SelectionChanged(object sender, EventArgs e)
        {
            int index = rtb_inputs.SelectionStart;
            int line = rtb_inputs.GetLineFromCharIndex(index);

            // Get the column.
            int firstChar = rtb_inputs.GetFirstCharIndexFromLine(line);
            int column = index - firstChar;
            //rtb_inputs.
            statusBar.Text = string.Format("Line : {0}, Column : {1}", line + 1, column + 1);
        }

        private void dgv_joints_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Draw_Joints(dgv_joints);
        }

        private void btn_elmt_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_plate_elements;

            if (btn.Name == btn_elmt_add.Name)
            {
                frm_Plate_Elements frm = new frm_Plate_Elements(DGV, false);
                frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                frm.DrawPlates = new deleDrawPlates(Draw_Plates);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_elmt_edit.Name)
            {
                frm_Plate_Elements frm = new frm_Plate_Elements(DGV, true);
                frm.DrawPlates = new deleDrawPlates(Draw_Plates);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_elmt_insert.Name)
            {
                frm_Plate_Elements frm = new frm_Plate_Elements(DGV, false);
                frm.IsInsert = true;
                frm.DrawPlates = new deleDrawPlates(Draw_Plates);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_elmt_delete.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }

            Draw_Plates();
        }

        private void btn_bound_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_boundary_elements;

            if (btn.Name == btn_bound_add.Name)
            {
                frm_Boundary_Elements frm = new frm_Boundary_Elements(DGV, false);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_bound_edit.Name)
            {
                frm_Boundary_Elements frm = new frm_Boundary_Elements(DGV, true);
                frm.IsInsert = true;
                frm.ShowDialog();
            }
            else if (btn.Name == btn_bound_insert.Name)
            {
                frm_Boundary_Elements frm = new frm_Boundary_Elements(DGV, false);
                //frm.Isin= ;
                frm.ShowDialog();
            }
            if (btn.Name == btn_bound_delete.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void btn_plt_prop_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_plates_mat_props;

            if (btn.Name == btn_plt_prop_add.Name)
            {
                frm_Plate_Mat_Props frm = new frm_Plate_Mat_Props(DGV, false);
                frm.ShowDialog();
            }
            if (btn.Name == btn_plt_prop_edit.Name)
            {
                frm_Plate_Mat_Props frm = new frm_Plate_Mat_Props(DGV, true);
                frm.ShowDialog();
            }
            if (btn.Name == btn_plt_prop_del.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void chk_dynamic_analysis_CheckedChanged(object sender, EventArgs e)
        {
            grb_dyna_analysis.Enabled = chk_dynamic_analysis.Checked;

            tab_dyn_ana.Visible = chk_dynamic_analysis.Checked;

            tab_dyn_ana.TabPages.Clear();
            if (rbtn_response_spectrum.Checked)
                tab_dyn_ana.TabPages.Add(tab_dyn_respns);
            if (rbtn_perform_time_history.Checked)
                tab_dyn_ana.TabPages.Add(tab_dyn_time);
        }

        private void rbtn_perform_eigen_CheckedChanged(object sender, EventArgs e)
        {
            //txt_frequencies.Enabled = rbtn_perform_eigen.Checked;

            grb_dynamic.Enabled = true;
            tab_dyn_ana.Visible = true;

            tab_dyn_ana.TabPages.Clear();
            if (rbtn_response_spectrum.Checked)
                tab_dyn_ana.TabPages.Add(tab_dyn_respns);
            if (rbtn_perform_time_history.Checked)
                tab_dyn_ana.TabPages.Add(tab_dyn_time);
        }

        private void btn_dyn_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int r = -1;

            DataGridView DGV = dgv_acceleration;

            if (DGV.SelectedCells.Count > 0)
                r = DGV.SelectedCells[0].RowIndex;

            if (r != -1)
                DGV.Rows.Insert(r, "", "");
            else
                DGV.Rows.Add("", "");


        }

        private void tc_pp_main_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tcParrent.SelectedTab != tab_post_process) return;

            if (!File.Exists(ReportFileName))
            {
                MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (File.Exists(DataFileName))
                    tcParrent.SelectedTab = tab_process;
                else
                    tcParrent.SelectedTab = tab_pre_process;
                return;
            }

            Tab_Selection();

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

        private void Tab_Post_Selection()
        {
            if (!File.Exists(ReportFileName))
            {
                rtb_results.Text = "";
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
                    if(i == 0)
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
            if(jcols != null)
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


            int tm = (int) (MyList.StringToDouble(cmbInterval.Text.Replace(" Sec", ""), 1.0) * 1000);
            if (tm == 0)
                tm = 1000;
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

                if (tcParrent.SelectedTab == tab_post_process)
                    VD = PP_ActiveDoc;

                string lay_name = chk.Text.Substring(0, 4).ToLower();
                //if (tc4.SelectedIndex == 1)
                //    VD = defDoc;
                if (lay_name.StartsWith("plat"))
                {
                    lay_name = "elem";
                    if(chk.Checked)
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

                        if (tcParrent.SelectedTab == tab_post_process)
                            VD = PP_ActiveDoc;

                        VD.SaveAs(sfd.FileName);
                        //Drawing_File = sfd.FileName;
                        System.Environment.SetEnvironmentVariable("OPENFILE", sfd.FileName);
                        //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                        System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                    }
                }
            }
        }

        private void btn_update_file_Click(object sender, EventArgs e)
        {
            if (File.Exists(DataFileName))
            {
                File.WriteAllLines(DataFileName, rtb_inputs.Lines);
                Open_Data_File(DataFileName);
                MessageBox.Show("Data File Updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chk_show_steps_CheckedChanged(object sender, EventArgs e)
        {
            spc_results.Panel1Collapsed = !chk_show_steps.Checked;
        }

        private void btn_sload_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DataGridView DGV = dgv_solid_loads;

            if (btn.Name == btn_sload_add.Name)
            {
                frm_Solid_Loads frm = new frm_Solid_Loads(DGV, false);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_sload_edit.Name)
            {
                frm_Solid_Loads frm = new frm_Solid_Loads(DGV, true);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_sload_delete.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void btn_view_analysis_Click(object sender, EventArgs e)
        {
            if(File.Exists(ReportFileName))
            {
                System.Diagnostics.Process.Start(ReportFileName);
            }
            else
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_sld_prop_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_solids_mat_props;

            if (btn.Name == btn_sld_prop_add.Name)
            {
                frm_Solid_Mat_Props frm = new frm_Solid_Mat_Props(DGV, false);
                frm.ShowDialog();
            }
            if (btn.Name == btn_sld_prop_edit.Name)
            {
                frm_Solid_Mat_Props frm = new frm_Solid_Mat_Props(DGV, true);
                frm.ShowDialog();
            }
            if (btn.Name == btn_sld_prop_delete.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
        }

        private void btn_solid_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            DataGridView DGV = dgv_solid_elements;

            if (btn.Name == btn_solid_add.Name)
            {
                frm_Solid_Elements frm = new frm_Solid_Elements(DGV, false);
                frm.DrawSolids = new deleDrawMembers(Draw_Solids);
                frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_solid_edit.Name)
            {
                frm_Solid_Elements frm = new frm_Solid_Elements(DGV, true);
                frm.DrawSolids = new deleDrawMembers(Draw_Solids);
                frm.ShowDialog();
            }
            else if (btn.Name == btn_solid_delete.Name)
            {
                if (DGV.SelectedCells.Count > 0)
                    DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
            }
            else if (btn.Name == btn_solid_insert.Name)
            {
                frm_Solid_Elements frm = new frm_Solid_Elements(DGV, false);
                frm.DrawSolids = new deleDrawMembers(Draw_Solids);
                frm.IsInsert = true;
                frm.ShowDialog();
            }

            Draw_Solids();
        }

        private void dgv_solid_elements_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView DGV = sender as DataGridView;

            int elment_no = MyList.StringToInt(DGV[0, e.RowIndex].Value.ToString(), 1);


            if (DGV.Name == dgv_solid_elements.Name)
                Show_Solid_Element(VDoc, elment_no);
            else if (DGV.Name == dgv_plate_elements.Name)
                Show_Plate_Element(VDoc, elment_no);
            else if (DGV.Name == dgv_beam_elements.Name)
                Show_Beam_Element(VDoc, elment_no);
            else if (DGV.Name == dgv_truss_elements.Name)
                Show_Truss_Element(VDoc, elment_no);
        }

        private void btn_time_func_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DataGridView DGV;
            int r = -1;

            try
            {

                if (btn.Name == btn_time_func_add.Name)
                {

                    DGV = dgv_time_function;
                    if (DGV.SelectedCells.Count > 0)
                        r = DGV.SelectedCells[0].RowIndex;

                    if (r == -1)
                        DGV.Rows.Add("", "");
                    else
                    {
                        DGV.Rows.Insert(r, "0.0", "0.0");
                    }

                }
                else if (btn.Name == btn_time_func_del.Name)
                {
                    DGV = dgv_time_function;
                    if (DGV.SelectedCells.Count > 0)
                        DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
                }
                else if (btn.Name == btn_time_nodal_add.Name)
                {

                    //DGV = dgv_time_nodal;
                    //if (DGV.SelectedCells.Count > 0)
                    //    r = DGV.SelectedCells[0].RowIndex;

                    //if (r == -1)
                    //    DGV.Rows.Add("", false, false, false, false, false, false);
                    //else
                    //{
                    //    DGV.Rows.Insert(r, "", false, false, false, false, false, false);
                    //}


                    frm_Time_Nodal frm = new frm_Time_Nodal(dgv_time_nodal, false);
                    frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                    frm.ShowDialog();

                }
                else if (btn.Name == btn_time_nodal_edit.Name)
                {
                    frm_Time_Nodal frm = new frm_Time_Nodal(dgv_time_nodal, true);
                    frm.ShowDialog();
                }
                else if (btn.Name == btn_time_nodal_del.Name)
                {
                    DGV = dgv_time_nodal;
                    if (DGV.SelectedCells.Count > 0)
                        DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
                }
                else if (btn.Name == btn_time_mbr_strs_add.Name)
                {
                    DGV = dgv_time_mbr_strs;
                    //if (DGV.SelectedCells.Count > 0)
                    //    r = DGV.SelectedCells[0].RowIndex;

                    //if (r == -1)
                    //    DGV.Rows.Add("", "START");
                    //else
                    //{
                    //    DGV.Rows.Insert(r, "", "START");
                    //}


                    frm_Time_Member_Stress frm = new frm_Time_Member_Stress(DGV, false);
                    //frm.Selected_Nodes = Get_Selected_Nodes(VDoc);
                    frm.ShowDialog();
                }
                else if (btn.Name == btn_time_mbr_strs_edit.Name)
                {

                    frm_Time_Member_Stress frm = new frm_Time_Member_Stress(dgv_time_mbr_strs, true);
                    frm.ShowDialog();
                }
                else if (btn.Name == btn_time_mbr_strs_del.Name)
                {
                    DGV = dgv_time_mbr_strs;
                    if (DGV.SelectedCells.Count > 0)
                        DGV.Rows.RemoveAt(DGV.SelectedCells[0].RowIndex);
                }

            }
            catch (Exception ex) { }
        }

        private void btn_solid_note_Click(object sender, EventArgs e)
        {
            //string msg = "";

            //msg = "Note:\n\n" +
            //    "In the Axis system:" + "\n" +
            //    "Y-Axis is Vertical and 'Y' coordinates are increasing in upward direction," + "\n" +
            //    "X-Axis is horizontal and 'X' coordinates are increasing in right-ward direction," + "\n" +
            //    "Z-Axis is horizontal and 'Z' coordinates are increasing in user-ward direction." + "\n\n" +

            //    "The solid elements are to be defined by starting with minimum Y-Coordinate values," + "\n" +
            //    "At every Y-Level, solid elements are to be defined by starting with maximum Z-Coordinate values," + "\n" +
            //    "At every Z-Level, solid elements are to be defined by starting with minimum X-Coordinate values," + "\n" +
            //    "and proceed towards maximum X-Coordinate values.";

            //MessageBox.Show(this, msg, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            frm_Solid_Note fn = new frm_Solid_Note();
            fn.Owner = this;
            fn.ShowDialog();
        }

        private void sc2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.Refresh();
        }

    }

}
