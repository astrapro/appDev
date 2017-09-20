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



namespace AstraAccess.ADOC
{
    public partial class UC_CAD : UserControl
    {
        public vdPropertyGrid.vdPropertyGrid PropertyGrid;
        public UC_CAD()
        {
            InitializeComponent();

            vdScrollableControl1.BaseControl.SelectionsAddItem += BaseControl_SelectionsAddItem;

            this.vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            this.vdScrollableControl1.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);

            this.vdScrollableControl1.BaseControl.MouseMove += BaseControl_MouseMove;
            this.vdScrollableControl1.BaseControl.Progress += BaseControl_Progress;

            //vdScrollableControl1.BaseControl.Selections += BaseControl_SelectionsAddItem;

        }

        void BaseControl_MouseMove(object sender, MouseEventArgs e)
        {
            gPoint ccspt = VDoc.CCS_CursorPos();

            double x = ccspt.x;
            double y = ccspt.y;
            double z = ccspt.z;

            tsb_coordinate.Text = string.Format("{0:f4}, {1:f4}, {2:f4}", x, y, z);
        }


        private void BaseControl_Progress(object sender, long percent, string jobDescription)
        {
            //if (percent == 0 || percent == 100) parent.status.Text = "";
            //else parent.status.Text = jobDescription;
            tspb_progress.Value = (int)percent;
        }
        void BaseControl_vdKeyUp(KeyEventArgs e, ref bool cancel)
        {
            Selected_Objects();

            //VDoc.

            if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.UndoEx(VDoc);
            }
            else if (e.KeyCode == Keys.Y && e.Modifiers == Keys.Control)
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.RedoEx(VDoc);
            }
            else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.MoveEx(VDoc);
            }
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.CopyEx(VDoc);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.EraseEx(VDoc);
            }
        }

        private void BaseControl_vdMouseUp(MouseEventArgs e, ref bool cancel)
        {
            //throw new NotImplementedException();
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
                
            //}
            Selected_Objects();
        }

        private void Selected_Objects()
        {
            if (vdScrollableControl1.BaseControl.ActiveDocument.Selections.Count > 0)
            {
                try
                {
                    if (PropertyGrid != null)
                    {
                        //pdg.SelectedObject = vdScrollableControl1.BaseControl.ActiveDocument.Selections.Last[0];

                        if (vdScrollableControl1.BaseControl.ActiveDocument.Selections.Last.Count == 0)
                            PropertyGrid.SelectedObject = vdScrollableControl1.BaseControl;
                        else
                            PropertyGrid.SelectedObject = vdScrollableControl1.BaseControl.ActiveDocument.Selections.Last;
                    }
                }
                catch (Exception exx) { }
            }
            else
            {

            }
        }

        void BaseControl_SelectionsAddItem(vdSelections sender, vdSelection item, ref bool cancel)
        {
            //if (pdg != null)
            //    pdg.SelectedObject =  item;
            vdScrollableControl1.Focus();
        }
        public IApplication iApp;


        public event EventHandler Open_Drawing;
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
            vdDocument VD = VDoc;
            bool f = false;

            VDoc.Prompt("command:");



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
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire2d(VD);
            else if (tsb.Name == tsb_Dist.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.GetDistanceEx(VD);

            else if (tsb.Name == tsb_Layers.Name)
                VectorDraw.Professional.Dialogs.LayersDialog.Show(VD);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.Layer(VD);

            else if (tsb.Name == tsb_Rotate.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.RotateEx(VD);

            else if (tsb.Name == tsb_Undo.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.UndoEx(VD);

            else if (tsb.Name == tsb_Redo.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.RedoEx(VD);

            else if (tsb.Name == tsb_copy.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.CopyEx(VD);

            else if (tsb.Name == tsb_Erase.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.EraseEx(VD);

            else if (tsb.Name == tsb_Move.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.MoveEx(VD);


            else if (tsb.Name == tsb_Explode.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ExplodeEx(VD);

            else if (tsb.Name == tsb_Mirror.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.MirrorEx(VD);

            else if (tsb.Name == tsb_Scale.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ScaleEx(VD);

            else if (tsb.Name == tsb_Break.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.BreakEx(VD);

            else if (tsb.Name == tsb_Trim.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.TrimEx(VD);

            else if (tsb.Name == tsb_Extend.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ExtendEx(VD);

            else if (tsb.Name == tsb_Insert.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.InsertEx(VD);

            else if (tsb.Name == tsb_Regen.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.RegenEx(VD);


            else if (tsb.Name.EndsWith("OsnapEnd"))
                VD.osnapMode = OsnapMode.END;
            else if (tsb.Name.EndsWith("OsnapCen"))
                VD.osnapMode = OsnapMode.CEN;
            else if (tsb.Name.EndsWith("OsnapIns"))
                VD.osnapMode = OsnapMode.INS;
            else if (tsb.Name.EndsWith("OsnapMid"))
                VD.osnapMode = OsnapMode.MID;
            else if (tsb.Name.EndsWith("OsnapPer"))
                VD.osnapMode = OsnapMode.PER;
            else if (tsb.Name.EndsWith("OsnapNea"))
                VD.osnapMode = OsnapMode.NEA;
            else if (tsb.Name.EndsWith("OsnapInters"))
                VD.osnapMode = OsnapMode.INTERS;
            else if (tsb.Name.EndsWith("OsnapNode"))
                VD.osnapMode = OsnapMode.NODE;
            else if (tsb.Name.EndsWith("OsnapTang"))
                VD.osnapMode = OsnapMode.TANG;
            else if (tsb.Name.EndsWith("OsnapApparentInt"))
                VD.osnapMode = OsnapMode.APPARENTINT;
            else if (tsb.Name.EndsWith("OsnapNone"))
                VD.osnapMode = OsnapMode.NONE;
            else if (tsb.Name.EndsWith("OsnapCancel"))
                VD.osnapMode = OsnapMode.DISABLE;


            else if (tsb.Name == tsb_Line.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.LineEx(VD);

            else if (tsb.Name == tsb_Polyline.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PolylineEx(VD);

            else if (tsb.Name == tsb_Circle.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.CircleEx(VD);

            else if (tsb.Name == tsb_arc.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ArcEx(VD);

            else if (tsb.Name == tsb_Rect.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.RectEx(VD);


            else if (tsb.Name == tsb_Text.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.TextEx(VD);

            else if (tsb.Name == tsb_Ortho.Name)
                VD.OrthoMode = tsb_Ortho.Checked;


            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name == tsb_Save.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";

                    if (File.Exists(VD.FileName))
                    {
                        sfd.InitialDirectory = Path.GetDirectoryName(VD.FileName);
                        sfd.FileName = Path.GetFileNameWithoutExtension(VD.FileName);
                        //sfd.DefaultExt = Path.GetExtension(VD.FileName);
                    }
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
            else if (tsb.Name == tsb_Open.Name)
            {
                using (OpenFileDialog sfd = new OpenFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        //if (iApp.IsDemo)
                        //{
                        //    MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //}
                        //else
                        {
                            if (VD.Open(sfd.FileName))
                            {
                                if (Open_Drawing != null) Open_Drawing(sender, e);
                                MessageBox.Show("File Opened successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }

        }

        public bool View_Buttons
        {
            get
            {
                return tsb_VBack.Visible;
            }
           set
            {
                tsb_VBack.Visible = value;
                tsb_VBot.Visible = value;
                tsb_VFront.Visible = value;
                tsb_VLeft.Visible = value;
                tsb_VNE.Visible = value;
                tsb_VNW.Visible = value;
                tsb_VRight.Visible = value;
                tsb_VSE.Visible = value;
                tsb_VSW.Visible = value;
                tsb_VTop.Visible = value;
            }
        }
        public vdDocument VDoc
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
