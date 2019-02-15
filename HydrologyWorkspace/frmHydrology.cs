using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsFunctions1;




using Vdraw = VectorDraw.Professional.ActionUtilities;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;


using HEADS_Project_Mode;
using HEADS_Project_Mode.DataStructure;
using HEADS_Project_Mode.DataStructure.RoadProject;
using HEADS_Project_Mode.DataStructure.SplitCarriageway;
using HEADS_Project_Mode.DataStructure.Tunnel;
using HEADS_Project_Mode.ProcessData;
using HEADS_Project_Mode.DataFroms;
using HEADS_Project_Mode.ProjectForms;
using HEADS_Site_Projects.DisNET;
//using HeadsProgram;


using HEADSNeed.RetrieveLandRecord;

namespace HydrologyWorkspace
{
    
    public partial class frmHydrology : Form, IVersion, IHeadsApplication, IProject, ILand_Record, IDisNetApplication
    {
        HASP_LOCK_CHECK HASP;
        public IProject iProj;
        public eSurveyAppType SurveyType { get; set; }

        public bool IsTutorial { get; set; }

        
        #region Create Project

        //Chiranjit [2016 01 18]
        //public double TextSize { get; set; }
        //public double Interval { get; set; }

        public frmHydrology(eTypeOfProject projectType)
        {
            InitializeComponent();
            //Working_Folder = "";
            Project_Name = "";
            //Project_File = "";
            Survey_File = "";
            Tunnel_Data_File = "";

            HASP = new HASP_LOCK_CHECK();
            ProjectType = eTypeOfProject.StreamHydrology;
            Selected_Line_Type = 1;

            HeadsFunctions = new CHeadsFunctionFactory();
            Is_Tunnel_Design = false;
            iProj = null;
            TextSize = 0.0;
            Interval = 0.0;

            iProj = this;
        }

        public string Open_file_name = "";
        public frmHydrology()
        {
            InitializeComponent();
            //Working_Folder = "";
            Project_Name = "";
            //Project_File = "";
            Survey_File = "";
            Tunnel_Data_File = "";

            HASP = new HASP_LOCK_CHECK();
            ProjectType = eTypeOfProject.StreamHydrology;

            Selected_Line_Type = 1;
            HeadsFunctions = new CHeadsFunctionFactory();
            Is_Tunnel_Design = false;
            iProj = this;
            IsTutorial = false;
        }

        public double Start_Chainage { get; set; }
        public double End_Chainage { get; set; }
        public double Interval_Chainage { get; set; }
        //public string Title { get; set; }




        public string Project_Name
        {
            get
            {
                return txt_Project_Name.Text;
            }
            set
            {
                txt_Project_Name.Text = value;
            }

        }
        public string Project_File
        {
            get
            {
                return Path.Combine(Working_Folder, "Project Data File.TXT");
            }
        }


        public string Survey_File { get; set; }
        public string Tunnel_Data_File { get; set; }

        public string Title
        {
            get
            {
                if (ProjectType == eTypeOfProject.Survey_Applications)
                    return "Survey Application";

                else if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
                    return "Site Leveling and Grading";

                return "Create Project";
            }
        }




        private void btn_Update_Project_Data_Click(object sender, EventArgs e)
        {
            Load_Project_File(Project_File);
            MessageBox.Show("Project Data File Updated.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btn_Refresh_Project_Data_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("All Previous work will be Deleted. Do you want to Proceed ?", "HEADS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                if (File.Exists(Project_File))
                {  //Folder_Delete(Working_Folder, true);
                    Folder_Delete_Index(Working_Folder, true);
                }
                Load_Project_File(Project_File);
                dgv_all_data.Rows.Clear();
                Clear_Project();
            }

        }
        private void btn_Save_Project_Data_Click(object sender, EventArgs e)
        {

            //Project_Name = ProjectType.ToString();
            if (ProjectType == eTypeOfProject.Irrigation_Canal)
            {
                Save_Irrigation_Canal_Project_File();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
            {
                Save_Irrigation_Dyke_Project_File();
            }
            else if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
            {
                Save_Site_Leveling_Grading_Project_File();
            }
            else if (ProjectType == eTypeOfProject.Land_Record)
            {
                Save_Project_File();
            }
            else
            {
                Save_Project_File();
            }
            //else if (ProjectType == eTypeOfProject.Intersection || ProjectType == eTypeOfProject.Interchange)
            //{
            //    Save_INT_Interchange_Project_File();
            //}
            //else if (ProjectType == eTypeOfProject.RacingTrackDesign)
            //{
            //    Save_Racing_Interchange_Project_File();
            //}
            //else if (ProjectType == eTypeOfProject.HillRoad)
            //{
            //    Save_HRP_Uniform_Section_Project_File();
            //}
            //else if (ProjectType == eTypeOfProject.Widening)
            //{
            //    Save_HWP_Project_File();
            //}
            //else if (ProjectType == eTypeOfProject.Tunnel)
            //{
            //    Save_TNL_Tunnel_Project_File();
            //}
        }

        #endregion


        #region Satellite Buttons
        private void btn_run_google_earth_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            string vid_path = Path.GetDirectoryName(Application.StartupPath);

            //vid_path = Path.Combine(vid_path, @"Technical Data\ExternalPrograms");
            vid_path = Path.Combine(vid_path, @"Technical Data\Videos");

            if (btn.Name == btn_run_google_earth.Name)
            {
                ExternalPrograms.GoogleEarth();
            }
            else if (btn.Name == btn_run_global_mapper.Name)
            {
                ExternalPrograms.GlobalMapper();
            }
            else if (btn.Name == btn_run_explorer.Name)
            {
                ExternalPrograms.RunExe("EXPLORER");

            }
            else if (btn.Name == btn_run_viewer.Name || btn.Name == btn_process_ground_data.Name)
            {
                RunExe("VIEWER.EXE", false);
            }
            else if (btn.Name == btn_video_google_earth.Name)
            {
                //vid_path = Path.Combine(vid_path, "01 Google Earth Video.avi");
                vid_path = Path.Combine(vid_path, "01 Google Earth Video.mp4");
                if (File.Exists(vid_path))
                    System.Diagnostics.Process.Start(vid_path);
                else
                {
                    MessageBox.Show(vid_path + " not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (btn.Name == btn_video_global_mapper.Name)
            {
                //vid_path = Path.Combine(vid_path, "02 Global Mapper Video.avi");
                vid_path = Path.Combine(vid_path, "02 Global Mapper Video.mp4");
                if (File.Exists(vid_path))
                    System.Diagnostics.Process.Start(vid_path);
                else
                {
                    MessageBox.Show(vid_path + " not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btn.Name == btn_video_explorer.Name)
            {
                vid_path = Path.Combine(vid_path, "03 Ground Data Formatting.mp4");
                if (File.Exists(vid_path))
                    System.Diagnostics.Process.Start(vid_path);
                else
                {
                    MessageBox.Show(vid_path + " not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btn.Name == btn_video_viewer.Name)
            {
                vid_path = Path.Combine(vid_path, "04 Alignment Design.mp4");
                if (File.Exists(vid_path))
                    System.Diagnostics.Process.Start(vid_path);
                else
                {
                    MessageBox.Show(vid_path + " not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btn.Name == btn_video_ground_data.Name)
            {
                vid_path = Path.Combine(vid_path, "05 Process Survey Data.mp4");
                if (File.Exists(vid_path))
                    System.Diagnostics.Process.Start(vid_path);
                else
                {
                    MessageBox.Show(vid_path + " not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btn.Name == btn_tutor_vids.Name)
            {
                //vid_path = Path.Combine(vid_path, "02 Global Mapper Video.avi");
                if (ProjectType == eTypeOfProject.Interchange) vid_path = Path.Combine(vid_path, "23 Interchange.mp4");
                if (ProjectType == eTypeOfProject.Intersection) vid_path = Path.Combine(vid_path, "24 Intersection.mp4");
                if (ProjectType == eTypeOfProject.RacingTrackDesign) vid_path = Path.Combine(vid_path, "Racing Track Design.mp4");
                if (File.Exists(vid_path))
                    System.Diagnostics.Process.Start(vid_path);
                else
                {
                    MessageBox.Show(vid_path + " not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btn.Name == btn_info_global_mapper.Name)
            {
                string info_path = Application.StartupPath;
                //prog_path = Path.GetDirectoryName(prog_path);
                info_path = Path.GetDirectoryName(info_path);
                info_path = Path.Combine(info_path, "Technical Data");
                info_path = Path.Combine(info_path, "ExternalPrograms");
                info_path = Path.Combine(info_path, "globalMapper.pdf");
                if (File.Exists(info_path))
                {
                    RunExe(info_path, false);
                }
                else
                {
                    MessageBox.Show(this, info_path + " file not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        #endregion Satellite Buttons

        #region Run Exe
        bool RunExe(string file_name)
        {
            return RunExe(file_name, true);
        }

        System.Diagnostics.Process prs;
        bool RunExe(string file_name, bool iswait)
        {
            string ex_file = "";

            if (Path.GetExtension(file_name).ToLower() == ".exe")
            {
                ex_file = Path.Combine(Application.StartupPath, file_name);

                if (!File.Exists(ex_file))
                {

                    ex_file = Path.Combine(@"C:\Program Files (x86)\HEADS Pro Release 20.0\Application", file_name);
                }
            }
            else
                ex_file = file_name;


            if (!File.Exists(ex_file))
            {
                MessageBox.Show(ex_file + " file not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }



            prs = new System.Diagnostics.Process();

            prs.StartInfo.FileName = ex_file;


            prs.Start();
            if (iswait)
                prs.WaitForExit();

            return true;
        }
        void Close_Exe()
        {
            try
            {
                prs.Kill();
                prs.Close();
            }
            catch (Exception ex) { }
        }
        public void SetEnvironment(string inputFile)
        {
            Environment.SetEnvironmentVariable("SURVEY", inputFile);
            Environment.SetEnvironmentVariable("DEM0", inputFile);
        }


        #endregion Run Exe

        #region Run Data

        public void Run_Data(string file_name)
        {
            frm_Process_Data fpd = new frm_Process_Data(file_name, this);
            fpd.VDoc = vdsC_Main.ActiveDocument;
            fpd.ShowDialog();
        }
        public void Run_Data(string file_name, vdDocument doc)
        {
            frm_Process_Data fpd = new frm_Process_Data(file_name, this);
            fpd.VDoc = doc;
            fpd.ShowDialog();
        }
        public void Run_Data(string file_name, bool IsHide)
        {
            frm_Process_Data fpd = new frm_Process_Data(file_name, this);
            fpd.Run_HEADS_DATA();
        }

        #endregion Run Data

        #region Draw Model Strings

        vdText vtxt = new vdText();
        vdLine ln = new vdLine();
        vdPolyline p_ln = new vdPolyline();
        vd3DFace v3D_face = new vd3DFace();
        vdPoint pt = new vdPoint();


        public Color Current_Color { get; set; }
        public void DrawEntity(vdFigure fig, string layer_name)
        {
            if (layers.Count > 0)
            {
                vdLayer lay = (vdLayer)layers[layer_name];
                if (lay != null)
                    DrawEntity(fig, lay);
            }
            else
                DrawEntity(fig);

        }
        public void DrawEntity(vdFigure fig, vdLayer lay)
        {
            Current_Color = Color.Red;
            vdDocument vdoc = VDoc;
            //vdText vtxt = new vdText();
            //vdLine ln = new vdLine();
            //vdPolyline p_ln = new vdPolyline();
            //vd3DFace v3D_face = new vd3DFace();
            //vdPoint pt = new vdPoint();

            //vdLayer lay = vdoc.Layers.FindName(fig.Label);

            //if (lay == null)
            //{
            //    lay = new vdLayer(vdoc, fig.Label);

            //    lay.SetUnRegisterDocument(vdoc);
            //    lay.setDocumentDefaults();
            //    vdoc.Layers.AddItem(lay);
            //}


            //if (Current_Color != null)
            //fig.PenColor = new vdColor(Current_Color);

            //lay.PenColor = new vdColor(Current_Color);


            //fig.Layer = lay;


            if (fig is vdText)
            {
                vtxt.SetUnRegisterDocument(vdoc);
                vtxt.setDocumentDefaults();
                vtxt.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(vtxt);
                vtxt = new vdText();
                return;
            }
            if (fig is vdLine)
            {
                ln.SetUnRegisterDocument(vdoc);
                ln.setDocumentDefaults();
                ln.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(ln);
                ln = new vdLine();
                return;
            }
            if (fig is vdPolyline)
            {
                p_ln.SetUnRegisterDocument(vdoc);
                p_ln.setDocumentDefaults();

                //p_ln.Label = p_ln.Label;
                p_ln.ToolTip = p_ln.Label;
                //if (Current_Color != null)
                //    p_ln.PenColor = new vdColor(Current_Color);
                p_ln.Layer = lay;


                vdoc.ActiveLayOut.Entities.AddItem(p_ln);
                p_ln = new vdPolyline();
                //p_ln.VertexList
                return;
            }
            if (fig is vd3DFace)
            {
                v3D_face.SetUnRegisterDocument(vdoc);
                v3D_face.setDocumentDefaults();

                //if (Current_Color != null)
                //    v3D_face.PenColor = new vdColor(Current_Color);

                v3D_face.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(v3D_face);
                v3D_face = new vd3DFace();
                return;
            }
            if (fig is vdPoint)
            {
                pt.SetUnRegisterDocument(vdoc);
                pt.setDocumentDefaults();
                pt.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(pt);
                pt = new vdPoint();
                return;
            }
        }

        public void DrawEntity(vdFigure fig)
        {
            vdDocument vdoc = VDoc;
            //vdText vtxt = new vdText();
            //vdLine ln = new vdLine();
            //vdPolyline p_ln = new vdPolyline();
            //vd3DFace v3D_face = new vd3DFace();
            //vdPoint pt = new vdPoint();

            //vdLayer lay = vdoc.Layers.FindName(fig.Label);

            //if (lay == null)
            //{
            //    lay = new vdLayer(vdoc, fig.Label);

            //    lay.SetUnRegisterDocument(vdoc);
            //    lay.setDocumentDefaults();
            //    vdoc.Layers.AddItem(lay);
            //}


            //if (Current_Color != null)
            //fig.PenColor = new vdColor(Current_Color);

            //lay.PenColor = new vdColor(Current_Color);


            //fig.Layer = lay;


            if (fig is vdText)
            {
                vtxt.SetUnRegisterDocument(vdoc);
                vtxt.setDocumentDefaults();
                //vtxt.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(vtxt);
                vtxt = new vdText();
                return;
            }
            if (fig is vdLine)
            {
                ln.SetUnRegisterDocument(vdoc);
                ln.setDocumentDefaults();
                //ln.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(ln);
                ln = new vdLine();
                return;
            }
            if (fig is vdPolyline)
            {
                p_ln.SetUnRegisterDocument(vdoc);
                p_ln.setDocumentDefaults();

                //p_ln.Label = p_ln.Label;
                p_ln.ToolTip = p_ln.Label;
                if (Current_Color != null)
                    p_ln.PenColor = new vdColor(Current_Color);


                vdoc.ActiveLayOut.Entities.AddItem(p_ln);
                p_ln = new vdPolyline();
                //p_ln.VertexList
                return;
            }
            if (fig is vd3DFace)
            {
                v3D_face.SetUnRegisterDocument(vdoc);
                v3D_face.setDocumentDefaults();

                if (Current_Color != null)
                {
                    #region Draw VIBGYOR

                    if (v3D_face.Layer.Name == "SURFACE")
                    {

                        double dif = (Max_limit - Min_limit) / 7;

                        //Violet	148, 0, 211	#9400D3	
                        //Indigo	75, 0, 130	#4B0082	
                        //Blue	0, 0, 255	#0000FF	
                        //Green	0, 255, 0	#00FF00	
                        //Yellow	255, 255, 0	#FFFF00	
                        //Orange	255, 127, 0	#FF7F00	
                        //Red	255, 0 , 0	#FF0000



                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FF0000"));//Red
                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FF7F00"));//Orange
                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FFFF00"));//Yellow
                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#00FF00"));//Green
                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#0000FF"));//Blue
                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#4B0082"));//Indigo
                        //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#9400D3"));//Violet

                        if (v3D_face.VertexList[0].z > (Min_limit + 6 * dif))
                        {
                            //v3D_face.PenColor = new vdColor(Color.DarkRed);
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FF0000"));//Red
                        }
                        else if (v3D_face.VertexList[0].z > (Min_limit + 5 * dif))
                        {
                            //v3D_face.PenColor = new vdColor(Color.Orange);
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FF7F00"));//Orange
                        }
                        else if (v3D_face.VertexList[0].z > (Min_limit + 4 * dif))
                        {
                            //v3D_face.PenColor = new vdColor(Color.Yellow);
                            //v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FFFF00"));
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#FFFF00"));//Yellow
                        }
                        else if (v3D_face.VertexList[0].z > (Min_limit + 3 * dif))
                        {
                            //v3D_face.PenColor = new vdColor(Color.DarkGreen);
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#00FF00"));//Green
                        }
                        else if (v3D_face.VertexList[0].z > (Min_limit + 2 * dif))
                        {
                            //v3D_face.PenColor = new vdColor(Color.DarkBlue);
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#0000FF"));//Blue
                        }
                        else if (v3D_face.VertexList[0].z > (Min_limit + 1 * dif))
                        {
                            //v3D_face.PenColor = new vdColor(Color.DarkViolet);
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#4B0082"));//Indigo
                        }
                        else if (v3D_face.VertexList[0].z > (Min_limit))
                        {
                            //v3D_face.PenColor = new vdColor(Color.Violet);
                            v3D_face.PenColor = new vdColor(ColorTranslator.FromHtml("#9400D3"));//Violet
                        }
                        else
                        {
                            v3D_face.PenColor = new vdColor(Color.DarkViolet);
                        }
                    }
                    //else if (v3D_face.VertexList[0].z > 90)
                    //{
                    //    v3D_face.PenColor = new vdColor(Color.Yellow);
                    //}
                    else
                        v3D_face.PenColor = new vdColor(Current_Color);

                    #endregion Draw VIBGYOR
                }




                //v3D_face.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(v3D_face);
                v3D_face = new vd3DFace();
                return;
            }
            if (fig is vdPoint)
            {
                pt.SetUnRegisterDocument(vdoc);
                pt.setDocumentDefaults();
                //pt.Layer = lay;
                vdoc.ActiveLayOut.Entities.AddItem(pt);
                pt = new vdPoint();
                return;
            }
        }


        double Min_limit = -1.0;
        double Max_limit = -1.0;
        private void Set_Max_Min_Elevation(string strSeleModel, List<string> CheckedItems)
        {
            if (Folder_Current == null)
                Folder_Current = Working_Folder;

            //m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            //m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");
            //set the test data path    

            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);

            string strModelName = "";
            string stgLabel = "";
            string _model = "";
            string _string = "";


            //double min_limit = -1.0;
            //double max_limit = -1.0;


            int NumPts = -1;

            bool bIsModel = false;
            bool bIsLabel = false;



            Max_limit = -1.0;
            Min_limit = -1.0;


            try
            {

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    try
                    {
                        CLabtype labtype = CLabtype.FromStream(br);

                        #region Model
                        if (labtype.attr == CLabtype.Type.Model) // Model Name
                        {
                            NumPts = 0;
                            strModelName = ((CModType)labtype.Tag).Name;

                            bIsModel = (strModelName == strSeleModel) ? true : false;
                        }
                        #endregion Model

                        #region String
                        else if (labtype.attr == CLabtype.Type.String)// String Label
                        {
                            stgLabel = ((CStgType)labtype.Tag).label;

                            if (bIsModel)
                            {
                                bIsLabel = CheckedItems.Contains(stgLabel);

                                //p_ln.Label = _model + ":" + _string;
                                if (bIsLabel)
                                {
                                    _model = strModelName;
                                    _string = stgLabel;
                                }
                            }
                        }
                        #endregion String

                        #region Point
                        else if (labtype.attr == CLabtype.Type.Point)
                        {
                            CPTStype pts = (CPTStype)labtype.Tag;
                            try
                            {
                                if (bIsModel && bIsLabel)
                                {
                                    if (pts.mz < -899) continue;


                                    if (Max_limit == -1.0)
                                    {
                                        Max_limit = pts.mz;
                                        Min_limit = pts.mz;
                                    }

                                    if (Max_limit < pts.mz)
                                    {
                                        Max_limit = pts.mz;
                                    }
                                    if (Min_limit > pts.mz)
                                    {
                                        Min_limit = pts.mz;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        #endregion Point
                             
                        #region EndCode

                        else if (labtype.attr == CLabtype.Type.EndCode)
                        {
                            NumPts = 0;
                        }
                        #endregion EndCode
                        //iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);
                    }
                    catch (Exception ex) { }
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();
                VDoc.Redraw(true);
            }
        }

        public int Selected_Line_Type { get; set; }

        string m_strpathlistfile = "";
        string m_strpathfilfile = "";
        private void ExecDrawString(string strSeleModel, List<string> CheckedItems)
        {
            if (Folder_Current == null)
                Folder_Current = Working_Folder;

            //m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            //m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");
            //set the test data path    

            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            List<vdPolyline> list_pLine = new List<vdPolyline>();
            vdPolyline p_line = new vdPolyline();
            try
            {
                ExecDrawString(br, strSeleModel, CheckedItems);
                vtxt.Dispose();
                ln.Dispose();
                p_ln.Dispose();
                v3D_face.Dispose();
                pt.Dispose();

                this.TopMost = true;

                System.Threading.Thread.Sleep(10);
                this.TopMost = false;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();
                VDoc.Redraw(true);
            }
        }
        private void ExecDrawString(List<string> strSeleModel, List<string> CheckedItems)
        {
            if (Folder_Current == null)
                Folder_Current = Working_Folder;

            //m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            //m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");
            //set the test data path    

            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            List<vdPolyline> list_pLine = new List<vdPolyline>();
            vdPolyline p_line = new vdPolyline();
            try
            {

                ExecDrawString(br, strSeleModel, CheckedItems);
                vtxt.Dispose();
                ln.Dispose();
                p_ln.Dispose();
                v3D_face.Dispose();
                pt.Dispose();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();
                VDoc.Redraw(true);
            }
        }
        private void ExecDrawString(BinaryReader br, List<string> strSeleModel, List<string> CheckedItems)
        {

            List<string> list_text = new List<string>();


            if (Folder_Current == null) Folder_Current = Working_Folder;

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");


            //set the test data path    


            //List<string> CheckedItems = new List<string>();

            //CheckedItems.Add(txt_Contour_pri_string.Text);
            //CheckedItems.Add(txt_Contour_sec_string.Text);

            //SetProgressValue s_pbr_val = new SetProgressValue(SetProgressBarValue);
            int iCurProgressInPercent = 0;

            bool bIsModel = false;
            bool bIsLabel = false;

            gPoint ptStart = new gPoint();
            gPoint ptEnd = new gPoint();
            gPoint ptLast = new gPoint();
            gPoint ptFirst3dFac = new gPoint();


            //string strSeleModel = txt_Contour_pri_model.Text;



            CTXTtype txt = new CTXTtype();
            vdLine uline = new vdLine();


            string _model = "";
            string _string = "";

            string strModelName = "";
            int NumPts = 0;
            string str1 = "";
            string stgLabel = "";
            gPoint aPoint = null;
            IHdPolyline3D polyline = null;
            CPTStype pts = new CPTStype();
            List<gPoint> pFArray = new List<gPoint>();

            //this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
            //this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

            //CCfgtype cfg = this.VDoc.ConfigParam;
            CCfgtype cfg = new CCfgtype();

            int iSelectedLineType = 1;

            double dTextHeight = (VDoc.ActiveTextStyle.Height > 0) ? VDoc.ActiveTextStyle.Height : 1.0;

            int p_count = 0;

            string strFilePath = m_strpathfilfile;


            if (File.Exists(strFilePath))
            {
                //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                //set the progress bar max value
                //SetProgressBarMaxValue(100, progressBar_);
                //Chiranjit [2011 08 10]
                //Add Progress Bar and Time
                //AutoProgressBar.frm_ProgressBar.ON("Reading Model file....");

                frm_ProgressBar.ON("Drawing Strings.........");
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    try
                    {

                        CLabtype labtype = CLabtype.FromStream(br);

                        //list_text.Add(string.Format("{0} {1} {2}", labtype.attr, labtype.attrVal, labtype.Tag));

                        //AutoProgressBar.frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                        #region Model
                        if (labtype.attr == CLabtype.Type.Model) // Model Name
                        {
                            NumPts = 0;
                            strModelName = ((CModType)labtype.Tag).Name;


                            //list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, strModelName));
                            //list_text.Add(string.Format("{0}", strModelName));

                            bIsModel = (strSeleModel.Contains(strModelName)) ? true : false;
                        }
                        #endregion Model

                        #region String
                        else if (labtype.attr == CLabtype.Type.String)// String Label
                        {
                            stgLabel = ((CStgType)labtype.Tag).label;

                            list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, stgLabel));

                            if (bIsModel)
                            {
                                bIsLabel = CheckedItems.Contains(stgLabel);

                                //p_ln.Label = _model + ":" + _string;
                                if (bIsLabel)
                                {
                                    _model = strModelName;
                                    _string = stgLabel;
                                    if (p_ln.VertexList.Count > 1)
                                    {
                                        //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                        DrawEntity(p_ln);
                                    }
                                }
                                p_ln.Label = _model + ":" + _string;
                            }
                        }
                        #endregion String

                        #region Point
                        else if (labtype.attr == CLabtype.Type.Point)
                        {
                            pts = (CPTStype)labtype.Tag;
                            try
                            {
                                ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -898.0) ? 0.0 : pts.mz));
                            }
                            catch (Exception ex)
                            {
                                ptEnd = new gPoint();
                            }
                            NumPts++;

                            ptStart = ptLast;
                            ptLast = ptEnd;

                            if (bIsModel && bIsLabel)
                            {
                                uline.StartPoint = new gPoint(ptStart);
                                uline.EndPoint = new gPoint(ptEnd);

                                str1 = stgLabel;
                                if (iSelectedLineType == 5)
                                {
                                    if (true)
                                    {
                                        str1 = "X = " + ptEnd.x.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        //double _bt = vtxt.BoundingBox.Bottom;
                                        DrawEntity(vtxt);

                                        str1 = "Y = " + ptEnd.y.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.InsertionPoint.y -= (1.2 * dTextHeight);
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);


                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);

                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 6)
                                {
                                    if (true)
                                    {
                                        str1 = "Z = " + ptEnd.z.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 7)
                                {
                                    if (true)
                                    {
                                        str1 = stgLabel;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 8)
                                {
                                    if (true)
                                    {
                                        str1 = strModelName;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        str1 = stgLabel;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.InsertionPoint.y -= (dTextHeight * 1.5);
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                        //ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        //ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        //DrawEntity(ln);

                                        //ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        //ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        //DrawEntity(ln);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (str1.ToUpper().StartsWith("P") == true)
                                {
                                    if (str1.ToUpper().StartsWith("P0") == true)
                                    {
                                        //txt.Point = ptEnd;
                                        //double[] res = ptEnd.GetCordArr();

                                        txt.tr = 0.0;

                                        str1 = ptEnd.z.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        DrawEntity(ln);

                                        ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        DrawEntity(ln);



                                        //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, ptEnd.z), str1, dTextHeight);

                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));

                                    }
                                    else
                                    {
                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        DrawEntity(ln);

                                        ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        DrawEntity(ln);

                                        //txt.Point = ptEnd;
                                        //txt.tr = 0.0;
                                        //double[] res = ptEnd.GetCordArr();

                                        //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, dTextHeight), str1, dTextHeight);

                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));
                                    }
                                }
                                else
                                {
                                    if (NumPts == 1)
                                    {
                                        //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        if (iSelectedLineType == 1)
                                        {
                                            p_ln.VertexList.Add(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            //aPoint.Erase();
                                            ptFirst3dFac = new gPoint(ptEnd);
                                            pFArray.Add(ptEnd);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                        }


                                    }
                                    else if (NumPts == 2)
                                    {
                                        //uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                        //uline.elatt = 1;
                                        //uline.laatt = 1;
                                        uline.Label = strModelName;
                                        uline.Label += ":";
                                        uline.Label += stgLabel;
                                        //uline.scatt = 1;

                                        if (iSelectedLineType == 1)
                                        {
                                            //ln.StartPoint = uline.StartPoint;
                                            //ln.EndPoint = uline.EndPoint;
                                            //ln.Label = uline.Label;
                                            //DrawEntity(ln);

                                            p_ln.VertexList.Add(uline.EndPoint);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 2)
                                        {
                                            ln.StartPoint = uline.StartPoint;
                                            ln.EndPoint = uline.EndPoint;
                                            ln.Label = uline.Label;
                                            DrawEntity(ln);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            pFArray.Add(ptEnd);
                                        }

                                        //aPoint.Erase();
                                    }
                                    else if (NumPts > 2)
                                    {
                                        if (iSelectedLineType == 1)
                                        {
                                            p_ln.VertexList.Add(ptEnd);
                                            //polyline.AppendVertex(ptEnd);
                                        }
                                        else if (iSelectedLineType == 2)
                                        {
                                            ln.StartPoint = uline.StartPoint;
                                            ln.EndPoint = uline.EndPoint;
                                            ln.Label = uline.Label;
                                            DrawEntity(ln);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            pFArray.Add(ptEnd);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion Point

                        #region Text

                        else if (labtype.attr == CLabtype.Type.Text)
                        {
                            txt = (CTXTtype)labtype.Tag;

                            ptEnd = new gPoint(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);


                            if (bIsLabel && bIsModel)
                            {
                                str1 = txt.tg;
                                //Display an Elevation
                                //TO DO:

                                vtxt.InsertionPoint = ptEnd;
                                vtxt.TextString = str1;
                                vtxt.Height = dTextHeight;
                                vtxt.Rotation = txt.tr;
                                DrawEntity(vtxt);
                                //IHdText xt = this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                                //xt.Rotation = txt.tr;
                                //xt.Height = txt.ts;
                                //xt.VerJustify = eVerticalJustify.Bottom;
                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                            }
                        }
                        #endregion Text

                        #region EndCode

                        else if (labtype.attr == CLabtype.Type.EndCode)
                        {
                            if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                            {
                                if ((System.Math.Abs(ptEnd.x - ptFirst3dFac.x) > 0.00001) || (System.Math.Abs(ptEnd.y - ptFirst3dFac.y) > 0.00001))
                                {
                                    if (NumPts < 4)
                                    {
                                        pFArray.Add(ptFirst3dFac);	 //close the face
                                    }
                                }

                                if (pFArray.Count > 3)
                                {
                                    //v3D_face.VertexList.Add(pFArray[0]);
                                    //v3D_face.VertexList.Add(pFArray[1]);
                                    //v3D_face.VertexList.Add(pFArray[2]);
                                    //v3D_face.VertexList.Add(pFArray[3]);


                                    v3D_face.VertexList[0] = (pFArray[0]);
                                    v3D_face.VertexList[1] = (pFArray[1]);
                                    v3D_face.VertexList[2] = (pFArray[2]);
                                    v3D_face.VertexList[3] = (pFArray[3]);
                                    pFArray.Clear();

                                }
                                DrawEntity(v3D_face);


                            }
                            if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                            {
                                //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                p_ln.Label = _model + ":" + _string;
                                DrawEntity(p_ln);
                            }
                            NumPts = 0;
                        }
                        #endregion EndCode
                        iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);



                        frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);

                        //progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                        //Thread.Sleep(0);
                    }
                    catch (Exception ex) { }
                }

                frm_ProgressBar.SetValue(br.BaseStream.Length, br.BaseStream.Length);
                frm_ProgressBar.OFF();

                if ((iSelectedLineType == 1))
                {
                    if (p_ln.VertexList.Count > 1)
                    {
                        //Chiranjit [2012 02 02]
                        //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                        //p_ln.Label = strModelName + ":" + stgLabel;
                        DrawEntity(p_ln);
                    }
                }
                //AutoProgressBar.frm_ProgressBar.OFF();
                //br.Close();

                string m_pathlstfile = Path.Combine(Working_Folder, "model.des");

                File.WriteAllLines(m_pathlstfile, list_text.ToArray());

            }
        }
        private void ExecDrawString(BinaryReader br, string strSeleModel, List<string> CheckedItems)
        {

            List<string> list_text = new List<string>();


            if (Folder_Current == null) Folder_Current = Working_Folder;

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");


            //set the test data path    


            //List<string> CheckedItems = new List<string>();

            //CheckedItems.Add(txt_Contour_pri_string.Text);
            //CheckedItems.Add(txt_Contour_sec_string.Text);

            //SetProgressValue s_pbr_val = new SetProgressValue(SetProgressBarValue);
            int iCurProgressInPercent = 0;

            bool bIsModel = false;
            bool bIsLabel = false;

            gPoint ptStart = new gPoint();
            gPoint ptEnd = new gPoint();
            gPoint ptLast = new gPoint();
            gPoint ptFirst3dFac = new gPoint();


            //string strSeleModel = txt_Contour_pri_model.Text;



            CTXTtype txt = new CTXTtype();
            vdLine uline = new vdLine();


            string _model = "";
            string _string = "";

            string strModelName = "";
            int NumPts = 0;
            string str1 = "";
            string stgLabel = "";
            gPoint aPoint = null;
            IHdPolyline3D polyline = null;
            CPTStype pts = new CPTStype();
            List<gPoint> pFArray = new List<gPoint>();

            //this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
            //this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

            //CCfgtype cfg = this.VDoc.ConfigParam;
            CCfgtype cfg = new CCfgtype();

            //int iSelectedLineType = 1;
            int iSelectedLineType = Selected_Line_Type;

            double dTextHeight = (VDoc.ActiveTextStyle.Height > 0) ? VDoc.ActiveTextStyle.Height : 1.0;

            int p_count = 0;

            string strFilePath = m_strpathfilfile;


            if (File.Exists(strFilePath))
            {
                //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                //set the progress bar max value
                //SetProgressBarMaxValue(100, progressBar_);
                //Chiranjit [2011 08 10]
                //Add Progress Bar and Time
                //AutoProgressBar.frm_ProgressBar.ON("Reading Model file....");

                frm_ProgressBar.ON("Draw Strings.........");
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    try
                    {


                        CLabtype labtype = CLabtype.FromStream(br);

                        //list_text.Add(string.Format("{0} {1} {2}", labtype.attr, labtype.attrVal, labtype.Tag));

                        //AutoProgressBar.frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                        #region Model
                        if (labtype.attr == CLabtype.Type.Model) // Model Name
                        {
                            NumPts = 0;
                            strModelName = ((CModType)labtype.Tag).Name;


                            list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, strModelName));
                            //list_text.Add(string.Format("{0}", strModelName));

                            bIsModel = (strModelName == strSeleModel) ? true : false;
                        }
                        #endregion Model





                        #region String
                        else if (labtype.attr == CLabtype.Type.String)// String Label
                        {
                            stgLabel = ((CStgType)labtype.Tag).label;

                            list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, stgLabel));

                            if (bIsModel)
                            {
                                bIsLabel = CheckedItems.Contains(stgLabel);

                                //p_ln.Label = _model + ":" + _string;
                                if (bIsLabel)
                                {
                                    _model = strModelName;
                                    _string = stgLabel;
                                    if (p_ln.VertexList.Count > 1)
                                    {
                                        //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                        DrawEntity(p_ln);
                                    }
                                }
                                p_ln.Label = _model + ":" + _string;
                            }
                        }
                        #endregion String

                        #region Point
                        else if (labtype.attr == CLabtype.Type.Point)
                        {
                            pts = (CPTStype)labtype.Tag;
                            try
                            {
                                ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -898.0) ? 0.0 : pts.mz));
                            }
                            catch (Exception ex)
                            {
                                ptEnd = new gPoint();
                            }
                            NumPts++;

                            ptStart = ptLast;
                            ptLast = ptEnd;

                            if (bIsModel && bIsLabel)
                            {
                                uline.StartPoint = new gPoint(ptStart);
                                uline.EndPoint = new gPoint(ptEnd);

                                str1 = stgLabel;
                                if (iSelectedLineType == 5)
                                {
                                    if (true)
                                    {
                                        str1 = "X = " + ptEnd.x.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        //double _bt = vtxt.BoundingBox.Bottom;
                                        DrawEntity(vtxt);

                                        str1 = "Y = " + ptEnd.y.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.InsertionPoint.y -= (1.2 * dTextHeight);
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);


                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);

                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 6)
                                {
                                    if (true)
                                    {
                                        str1 = "Z = " + ptEnd.z.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 7)
                                {
                                    if (true)
                                    {
                                        str1 = stgLabel;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 8)
                                {
                                    if (true)
                                    {
                                        str1 = strModelName;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        str1 = stgLabel;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.InsertionPoint.y -= (dTextHeight * 1.5);
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                        //ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        //ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        //DrawEntity(ln);

                                        //ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        //ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        //DrawEntity(ln);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (str1.ToUpper().StartsWith("P") == true)
                                {
                                    if (str1.ToUpper().StartsWith("P0") == true)
                                    {
                                        //txt.Point = ptEnd;
                                        //double[] res = ptEnd.GetCordArr();

                                        txt.tr = 0.0;

                                        str1 = ptEnd.z.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        DrawEntity(ln);

                                        ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        DrawEntity(ln);



                                        //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, ptEnd.z), str1, dTextHeight);

                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));

                                    }
                                    else
                                    {
                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        DrawEntity(ln);

                                        ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        DrawEntity(ln);

                                        //txt.Point = ptEnd;
                                        //txt.tr = 0.0;
                                        //double[] res = ptEnd.GetCordArr();

                                        //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, dTextHeight), str1, dTextHeight);

                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));
                                    }
                                }
                                else
                                {
                                    if (NumPts == 1)
                                    {
                                        //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        if (iSelectedLineType == 1)
                                        {
                                            p_ln.VertexList.Add(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            //aPoint.Erase();
                                            ptFirst3dFac = new gPoint(ptEnd);
                                            pFArray.Add(ptEnd);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                        }


                                    }
                                    else if (NumPts == 2)
                                    {
                                        //uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                        //uline.elatt = 1;
                                        //uline.laatt = 1;
                                        uline.Label = strModelName;
                                        uline.Label += ":";
                                        uline.Label += stgLabel;
                                        //uline.scatt = 1;

                                        if (iSelectedLineType == 1)
                                        {
                                            //ln.StartPoint = uline.StartPoint;
                                            //ln.EndPoint = uline.EndPoint;
                                            //ln.Label = uline.Label;
                                            //DrawEntity(ln);

                                            p_ln.VertexList.Add(uline.EndPoint);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 2)
                                        {
                                            ln.StartPoint = uline.StartPoint;
                                            ln.EndPoint = uline.EndPoint;
                                            ln.Label = uline.Label;
                                            DrawEntity(ln);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            pFArray.Add(ptEnd);
                                        }

                                        //aPoint.Erase();
                                    }
                                    else if (NumPts > 2)
                                    {
                                        if (iSelectedLineType == 1)
                                        {
                                            p_ln.VertexList.Add(ptEnd);
                                            //polyline.AppendVertex(ptEnd);
                                        }
                                        else if (iSelectedLineType == 2)
                                        {
                                            ln.StartPoint = uline.StartPoint;
                                            ln.EndPoint = uline.EndPoint;
                                            ln.Label = uline.Label;
                                            DrawEntity(ln);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            pFArray.Add(ptEnd);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion Point

                        #region Text

                        else if (labtype.attr == CLabtype.Type.Text)
                        {
                            txt = (CTXTtype)labtype.Tag;

                            ptEnd = new gPoint(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);


                            if (bIsLabel && bIsModel)
                            {
                                str1 = txt.tg;
                                //Display an Elevation
                                //TO DO:

                                vtxt.InsertionPoint = ptEnd;
                                vtxt.TextString = str1;
                                vtxt.Height = dTextHeight;
                                vtxt.Rotation = txt.tr;
                                DrawEntity(vtxt);
                                //IHdText xt = this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                                //xt.Rotation = txt.tr;
                                //xt.Height = txt.ts;
                                //xt.VerJustify = eVerticalJustify.Bottom;
                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                            }
                        }
                        #endregion Text

                        #region EndCode

                        else if (labtype.attr == CLabtype.Type.EndCode)
                        {
                            if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                            {
                                if ((System.Math.Abs(ptEnd.x - ptFirst3dFac.x) > 0.00001) || (System.Math.Abs(ptEnd.y - ptFirst3dFac.y) > 0.00001))
                                {
                                    if (NumPts < 4)
                                    {
                                        pFArray.Add(ptFirst3dFac);	 //close the face
                                    }
                                }

                                if (pFArray.Count > 3)
                                {
                                    //v3D_face.VertexList.Add(pFArray[0]);
                                    //v3D_face.VertexList.Add(pFArray[1]);
                                    //v3D_face.VertexList.Add(pFArray[2]);
                                    //v3D_face.VertexList.Add(pFArray[3]);

                                    v3D_face.VertexList[0] = (pFArray[0]);
                                    v3D_face.VertexList[1] = (pFArray[1]);
                                    v3D_face.VertexList[2] = (pFArray[2]);
                                    v3D_face.VertexList[3] = (pFArray[3]);
                                    pFArray.Clear();

                                }
                                if (!(v3D_face.VertexList[0] == v3D_face.VertexList[1] &&
                                        v3D_face.VertexList[0] == v3D_face.VertexList[2] &&
                                        v3D_face.VertexList[0] == v3D_face.VertexList[3]))
                                {
                                    DrawEntity(v3D_face);
                                }
                                else
                                {
                                    //DrawEntity(v3D_face);
                                }

                            }
                            if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                            {
                                //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                p_ln.Label = _model + ":" + _string;
                                DrawEntity(p_ln);
                            }
                            NumPts = 0;
                        }
                        #endregion EndCode
                        iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);



                        frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);

                        //progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                        //Thread.Sleep(0);
                    }
                    catch (Exception ex) { }
                }

                frm_ProgressBar.SetValue(br.BaseStream.Length, br.BaseStream.Length);
                frm_ProgressBar.OFF();

                if ((iSelectedLineType == 1))
                {
                    if (p_ln.VertexList.Count > 1)
                    {
                        //Chiranjit [2012 02 02]
                        //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                        //p_ln.Label = strModelName + ":" + stgLabel;
                        DrawEntity(p_ln);
                    }
                }
                //AutoProgressBar.frm_ProgressBar.OFF();
                //br.Close();

                string m_pathlstfile = Path.Combine(Working_Folder, "model.des");

                File.WriteAllLines(m_pathlstfile, list_text.ToArray());

            }
        }

        string Get_Layer_by_Name(string str_name)
        {
            string kStr = "";


            if (str_name.StartsWith("M"))
            {
                kStr = "CentreLine";
            }

            else if (str_name == "CL01" || str_name == "CL02" || str_name == "CL03" ||
                str_name == "CR01" || str_name == "CR02" || str_name == "CR03")
            {
                kStr = "Median";
            }
            else if (str_name == "CL04" || str_name == "CL05" ||
                 str_name == "CR04" || str_name == "CR05")
            {
                kStr = "Carriageway";
            }
            else if (str_name == "CL06" || str_name == "CL07" || str_name == "CR06" || str_name == "CR07")
            {
                kStr = "Shoulder";
            }
            else if (str_name.StartsWith("SL") || str_name.StartsWith("SR") ||
                str_name.StartsWith("NL") || str_name.StartsWith("NR") ||
                str_name.StartsWith("DL") || str_name.StartsWith("DR") ||
                str_name.StartsWith("TL") || str_name.StartsWith("TR"))
            {
                kStr = "ServiceRoad";
            }
            else if (str_name.StartsWith("I0"))
            {
                kStr = "Interface";
            }
            else
            {
                kStr = str_name;
            }

            return kStr;
        }

        vdLayer Create_Layer(string name)
        {

            //string nm_lay = Get_Layer_by_Name(name);
            string nm_lay = name;

            vdLayer vlay = VDoc.Layers.FindName(nm_lay);
            if (vlay == null)
            {
                //vlay = new vdLayer(VDoc, name);
                vlay = new vdLayer(VDoc, nm_lay);
                vlay.SetUnRegisterDocument(VDoc);
                vlay.setDocumentDefaults();
                //vlay.PenColor = new vdColor(Color.Red);
                VDoc.Layers.Add(vlay);
            }

            if (name.Contains("Align"))
            {
                vlay.PenColor = new vdColor(Color.White);
            }
            else if (name.Contains("M001"))
            {
                vlay.PenColor = new vdColor(Color.Red);
            }
            else if (name.StartsWith("M"))
            {
                vlay.PenColor = new vdColor(Color.Red);
            }
            else if (name.Contains("E001"))
            {
                vlay.PenColor = new vdColor(Color.DarkGreen);
            }
            else if (name.StartsWith("E"))
            {
                vlay.PenColor = new vdColor(Color.DarkGreen);
            }
            else if (name.Contains("C001"))
            {
                vlay.PenColor = new vdColor(Color.Gray);
            }
            else if (name.Contains("C005"))
            {
                vlay.PenColor = new vdColor(Color.GreenYellow);
            }
            else if (name.Contains("CL01") || name.Contains("CL02") || name.Contains("CL03") ||
                name.Contains("CR01") || name.Contains("CR02") || name.Contains("CR03"))
            {
                //vlay.PenColor = new vdColor(Color.Pink);
                vlay.PenColor = new vdColor(Color.Blue);
            }
            else if (name.Contains("CL04") || name.Contains("CL05") ||
                name.Contains("CR04") || name.Contains("CR05"))
            {
                vlay.PenColor = new vdColor(Color.Cyan);
            }
            else if (name.Contains("CL06") || name.Contains("CL07") ||
                name.Contains("CR06") || name.Contains("CR07"))
            {
                vlay.PenColor = new vdColor(Color.Orange);
            }
            else if (name.Contains("I0"))
            {
                //vlay.PenColor = new vdColor(Color.Green);
                vlay.PenColor = new vdColor(Color.BlueViolet);
            }
            else if (name.Contains("HALIGN"))
            {
                //vlay.PenColor = new vdColor(Color.Green);
                vlay.PenColor = new vdColor(Color.White);
            }
            else
            {
                //vlay.PenColor = new vdColor(Color.Green);
                vlay.PenColor = new vdColor(Color.Gray);
            }
            try
            {
                VDoc.ActiveLayer = vlay;
            }
            catch (Exception exx) { }
            return vlay;
        }

        vdLayer Create_Layer(CModel cmod)
        {
            if (VDoc == null) return null;

            string name = cmod.ToString();

            vdLayer vlay = VDoc.Layers.FindName(name);
            if (vlay == null)
            {
                //vlay = new vdLayer(VDoc, name);
                vlay = new vdLayer(VDoc, name);
                vlay.SetUnRegisterDocument(VDoc);
                vlay.setDocumentDefaults();
                vlay.PenColor = new vdColor(cmod.LayerColor);
                VDoc.Layers.Add(vlay);
            }
            vlay.PenColor = new vdColor(cmod.LayerColor);
            return vlay;
        }


        Hashtable layers = new Hashtable();

        public void DrawStrings(List<CModel> str_labels)
        {


            CHEADSDoc.ReadModelsFromFile(Working_Folder);

            List<CModel> mld = CHEADSDoc.LstModel;
            bool flag = false;
            foreach (var item in str_labels)
            {
                for (int i = 0; i < mld.Count; i++)
                {
                    if (item.ModelName == mld[i].ModelName && item.StringName == mld[i].StringName)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag) return;

            if (str_labels.Count > 0)
            {
                ExecDrawString(str_labels, str_labels[0].StringName); ;
            }


            //frm_ProgressBar
            VDoc.Redraw(true);
            Vdraw.vdCommandAction.View3D_VTop(VDoc);



        }
        private void ExecDrawString(List<CModel> lst_model_string, string str_title)
        {
            if (Folder_Current == null)
                Folder_Current = Working_Folder;

            //m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            //m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");
            //set the test data path    

            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            List<vdPolyline> list_pLine = new List<vdPolyline>();
            vdPolyline p_line = new vdPolyline();


            List<string> strSeleModels = new List<string>();
            List<string> strSeleStrings = new List<string>();




            #region Refresh Layers
            foreach (var item in lst_model_string)
            {
                for (int i = 0; i < VDoc.ActiveLayOut.Entities.Count; i++)
                {
                    var it = VDoc.ActiveLayOut.Entities[i];

                    if (it.Layer.Name == item.StringName)
                    {
                        it.Deleted = true;
                    }

                }
            }

            VDoc.ClearEraseItems();
            #endregion Refresh Layers






            vdLayer current_layer = new vdLayer();

            layers.Clear();

            string progress_title = "Drawing Strings...";
            foreach (var item in lst_model_string)
            {

                if (!strSeleStrings.Contains(item.StringName) || !strSeleModels.Contains(item.ModelName))
                {
                    strSeleModels.Add(item.ModelName);
                    strSeleStrings.Add(item.StringName);

                    progress_title = "Drawing Strings..." + str_title;
                    //if (ProjectType == eTypeOfProject.Interchange)
                    //    current_layer = Create_Layer(item.ToString());
                    //else
                    current_layer = Create_Layer(item.StringName);
                    if (item.LayerColor != Color.Black)
                        current_layer.PenColor = new vdColor(item.LayerColor);
                    try
                    {
                        layers.Add(item.StringName, current_layer);
                        //layers.Add(item.ToString(), current_layer);
                    }
                    catch (Exception exx) { }
                }
            }

            try
            {
                //foreach (var item in lst_model_string)
                //{
                //    strSeleModels.Clear();
                //    strSeleStrings.Clear();

                //    strSeleModels.Add(item.ModelName);
                //    strSeleStrings.Add(item.StringName);
                //    Create_Layer(item.StringName).PenColor = new vdColor(item.LayerColor);
                //    br.BaseStream.Position = 0;
                //}




                #region ExecDrawString
                List<string> list_text = new List<string>();




                //set the test data path    


                //List<string> CheckedItems = new List<string>();

                //CheckedItems.Add(txt_Contour_pri_string.Text);
                //CheckedItems.Add(txt_Contour_sec_string.Text);

                //SetProgressValue s_pbr_val = new SetProgressValue(SetProgressBarValue);
                int iCurProgressInPercent = 0;

                bool bIsModel = false;
                bool bIsLabel = false;

                gPoint ptStart = new gPoint();
                gPoint ptEnd = new gPoint();
                gPoint ptLast = new gPoint();
                gPoint ptFirst3dFac = new gPoint();


                //string strSeleModel = txt_Contour_pri_model.Text;



                CTXTtype txt = new CTXTtype();
                vdLine uline = new vdLine();


                string _model = "";
                string _string = "";

                string strModelName = "";
                int NumPts = 0;
                string str1 = "";
                string stgLabel = "";
                gPoint aPoint = null;
                IHdPolyline3D polyline = null;
                CPTStype pts = new CPTStype();
                List<gPoint> pFArray = new List<gPoint>();

                //this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
                //this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

                //CCfgtype cfg = this.VDoc.ConfigParam;
                CCfgtype cfg = new CCfgtype();

                int iSelectedLineType = 1;

                double dTextHeight = (VDoc.ActiveTextStyle.Height > 0) ? VDoc.ActiveTextStyle.Height : 1.0;

                int p_count = 0;

                string strFilePath = m_strpathfilfile;


                if (File.Exists(strFilePath))
                {
                    //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                    //set the progress bar max value
                    //SetProgressBarMaxValue(100, progressBar_);
                    //Chiranjit [2011 08 10]
                    //Add Progress Bar and Time
                    //AutoProgressBar.frm_ProgressBar.ON("Reading Model file....");

                    //frm_ProgressBar.ON("Drawing Strings.....[MODEL=" + strSeleModels[0] + ", STRING=" + strSeleStrings[0] + "]");
                    frm_ProgressBar.ON(progress_title);


                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        try
                        {

                            CLabtype labtype = CLabtype.FromStream(br);

                            //list_text.Add(string.Format("{0} {1} {2}", labtype.attr, labtype.attrVal, labtype.Tag));

                            //AutoProgressBar.frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                            #region Model
                            if (labtype.attr == CLabtype.Type.Model) // Model Name
                            {
                                NumPts = 0;
                                strModelName = ((CModType)labtype.Tag).Name;


                                //list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, strModelName));
                                //list_text.Add(string.Format("{0}", strModelName));

                                bIsModel = (strSeleModels.Contains(strModelName)) ? true : false;
                            }
                            #endregion Model

                            #region String
                            else if (labtype.attr == CLabtype.Type.String)// String Label
                            {
                                stgLabel = ((CStgType)labtype.Tag).label;

                                list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, stgLabel));

                                if (bIsModel)
                                {
                                    bIsLabel = strSeleStrings.Contains(stgLabel);

                                    //p_ln.Label = _model + ":" + _string;
                                    if (bIsLabel)
                                    {
                                        _model = strModelName;
                                        _string = stgLabel;
                                        if (p_ln.VertexList.Count > 1)
                                        {
                                            //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                            DrawEntity(p_ln, stgLabel);
                                        }
                                    }
                                    p_ln.Label = _model + ":" + _string;
                                }
                            }
                            #endregion String

                            #region Point
                            else if (labtype.attr == CLabtype.Type.Point)
                            {
                                pts = (CPTStype)labtype.Tag;
                                try
                                {
                                    ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -898.0) ? 0.0 : pts.mz));
                                }
                                catch (Exception ex)
                                {
                                    ptEnd = new gPoint();
                                }
                                NumPts++;

                                ptStart = ptLast;
                                ptLast = ptEnd;

                                if (bIsModel && bIsLabel)
                                {
                                    uline.StartPoint = new gPoint(ptStart);
                                    uline.EndPoint = new gPoint(ptEnd);

                                    str1 = stgLabel;
                                    if (iSelectedLineType == 5)
                                    {
                                        if (true)
                                        {
                                            str1 = "X = " + ptEnd.x.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            //double _bt = vtxt.BoundingBox.Bottom;
                                            DrawEntity(vtxt, stgLabel);

                                            str1 = "Y = " + ptEnd.y.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.InsertionPoint.y -= (1.2 * dTextHeight);
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);


                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);

                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    else if (iSelectedLineType == 6)
                                    {
                                        if (true)
                                        {
                                            str1 = "Z = " + ptEnd.z.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);
                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt, stgLabel);
                                    }
                                    else if (iSelectedLineType == 7)
                                    {
                                        if (true)
                                        {
                                            str1 = stgLabel;

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);
                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    else if (iSelectedLineType == 8)
                                    {
                                        if (true)
                                        {
                                            str1 = strModelName;

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            str1 = stgLabel;

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.InsertionPoint.y -= (dTextHeight * 1.5);
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);
                                            //ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                            //ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                            //DrawEntity(ln);

                                            //ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                            //ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                            //DrawEntity(ln);
                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt, stgLabel);
                                    }
                                    else if (str1.ToUpper().StartsWith("P") == true)
                                    {
                                        if (str1.ToUpper().StartsWith("P0") == true)
                                        {
                                            //txt.Point = ptEnd;
                                            //double[] res = ptEnd.GetCordArr();

                                            txt.tr = 0.0;

                                            str1 = ptEnd.z.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                            DrawEntity(ln, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                            DrawEntity(ln, stgLabel);



                                            //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, ptEnd.z), str1, dTextHeight);

                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));

                                        }
                                        else
                                        {
                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                            DrawEntity(ln, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                            DrawEntity(ln, stgLabel);

                                            //txt.Point = ptEnd;
                                            //txt.tr = 0.0;
                                            //double[] res = ptEnd.GetCordArr();

                                            //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, dTextHeight), str1, dTextHeight);

                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));
                                        }
                                    }
                                    else
                                    {
                                        if (NumPts == 1)
                                        {
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                            if (iSelectedLineType == 1)
                                            {
                                                p_ln.VertexList.Add(ptEnd);
                                            }
                                            else if (iSelectedLineType == 4)
                                            {
                                                //aPoint.Erase();
                                                ptFirst3dFac = new gPoint(ptEnd);
                                                pFArray.Add(ptEnd);
                                            }
                                            else if (iSelectedLineType == 3)
                                            {
                                                pt.InsertionPoint = ptEnd;
                                                DrawEntity(pt);
                                            }


                                        }
                                        else if (NumPts == 2)
                                        {
                                            //uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                            //uline.elatt = 1;
                                            //uline.laatt = 1;
                                            uline.Label = strModelName;
                                            uline.Label += ":";
                                            uline.Label += stgLabel;
                                            //uline.scatt = 1;

                                            if (iSelectedLineType == 1)
                                            {
                                                //ln.StartPoint = uline.StartPoint;
                                                //ln.EndPoint = uline.EndPoint;
                                                //ln.Label = uline.Label;
                                                //DrawEntity(ln);

                                                p_ln.VertexList.Add(uline.EndPoint);
                                                //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                            }
                                            else if (iSelectedLineType == 2)
                                            {
                                                ln.StartPoint = uline.StartPoint;
                                                ln.EndPoint = uline.EndPoint;
                                                ln.Label = uline.Label;
                                                DrawEntity(ln, stgLabel);
                                                //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                            }
                                            else if (iSelectedLineType == 3)
                                            {
                                                pt.InsertionPoint = ptEnd;
                                                DrawEntity(pt, stgLabel);
                                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                            }
                                            else if (iSelectedLineType == 4)
                                            {
                                                pFArray.Add(ptEnd);
                                            }

                                            //aPoint.Erase();
                                        }
                                        else if (NumPts > 2)
                                        {
                                            if (iSelectedLineType == 1)
                                            {
                                                p_ln.VertexList.Add(ptEnd);
                                                //polyline.AppendVertex(ptEnd);
                                            }
                                            else if (iSelectedLineType == 2)
                                            {
                                                ln.StartPoint = uline.StartPoint;
                                                ln.EndPoint = uline.EndPoint;
                                                ln.Label = uline.Label;
                                                DrawEntity(ln, stgLabel);
                                                //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                            }
                                            else if (iSelectedLineType == 3)
                                            {
                                                pt.InsertionPoint = ptEnd;
                                                DrawEntity(pt, stgLabel);
                                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                            }
                                            else if (iSelectedLineType == 4)
                                            {
                                                pFArray.Add(ptEnd);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion Point

                            #region Text

                            else if (labtype.attr == CLabtype.Type.Text)
                            {
                                txt = (CTXTtype)labtype.Tag;

                                ptEnd = new gPoint(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);


                                if (bIsLabel && bIsModel)
                                {
                                    str1 = txt.tg;
                                    //Display an Elevation
                                    //TO DO:

                                    vtxt.InsertionPoint = ptEnd;
                                    vtxt.TextString = str1;
                                    vtxt.Height = dTextHeight;
                                    vtxt.Rotation = txt.tr;
                                    DrawEntity(vtxt, stgLabel);
                                    //IHdText xt = this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                                    //xt.Rotation = txt.tr;
                                    //xt.Height = txt.ts;
                                    //xt.VerJustify = eVerticalJustify.Bottom;
                                    //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                }
                            }
                            #endregion Text

                            #region EndCode

                            else if (labtype.attr == CLabtype.Type.EndCode)
                            {
                                if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                                {
                                    if ((System.Math.Abs(ptEnd.x - ptFirst3dFac.x) > 0.00001) || (System.Math.Abs(ptEnd.y - ptFirst3dFac.y) > 0.00001))
                                    {
                                        if (NumPts < 4)
                                        {
                                            pFArray.Add(ptFirst3dFac);	 //close the face
                                        }
                                    }

                                    if (pFArray.Count > 3)
                                    {
                                        //v3D_face.VertexList.Add(pFArray[0]);
                                        //v3D_face.VertexList.Add(pFArray[1]);
                                        //v3D_face.VertexList.Add(pFArray[2]);
                                        //v3D_face.VertexList.Add(pFArray[3]);


                                        v3D_face.VertexList[0] = (pFArray[0]);
                                        v3D_face.VertexList[1] = (pFArray[1]);
                                        v3D_face.VertexList[2] = (pFArray[2]);
                                        v3D_face.VertexList[3] = (pFArray[3]);
                                        pFArray.Clear();

                                    }
                                    DrawEntity(v3D_face, stgLabel);


                                }
                                if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                                {
                                    //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                    p_ln.Label = _model + ":" + _string;
                                    //DrawEntity(p_ln, p_ln.Label);
                                    DrawEntity(p_ln, stgLabel);
                                }
                                NumPts = 0;
                            }
                            #endregion EndCode
                            iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);



                            frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);

                            //progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                            //Thread.Sleep(0);
                        }
                        catch (Exception ex) { }
                    }

                    frm_ProgressBar.SetValue(br.BaseStream.Length, br.BaseStream.Length);
                    frm_ProgressBar.OFF();

                    if ((iSelectedLineType == 1))
                    {
                        if (p_ln.VertexList.Count > 1)
                        {
                            //Chiranjit [2012 02 02]
                            //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                            //p_ln.Label = strModelName + ":" + stgLabel;
                            DrawEntity(p_ln, stgLabel);
                        }
                    }
                    //AutoProgressBar.frm_ProgressBar.OFF();
                    //br.Close();

                    //string m_pathlstfile = Path.Combine(Working_Folder, "model.des");

                    //File.WriteAllLines(m_pathlstfile, list_text.ToArray());

                }
                #endregion ExecDrawString

                vtxt.Dispose();
                ln.Dispose();
                p_ln.Dispose();
                v3D_face.Dispose();
                pt.Dispose();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();
                VDoc.Redraw(true);
                VDoc.EnableAutoGripOn = true;
                VDoc.EntitySelectMode = VectorDraw.Render.PickEntityMode.Auto;
                VDoc.ZoomAll();
                Vdraw.vdCommandAction.View3D_VTop(VDoc);

            }
        }
        private void ExecDrawString(List<CModel> lst_model_string)
        {
            if (Folder_Current == null)
                Folder_Current = Working_Folder;

            //m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            //m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");


            if (!File.Exists(m_strpathfilfile)) return;
            //set the test data path    
            string str_title = "";


            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            List<vdPolyline> list_pLine = new List<vdPolyline>();
            vdPolyline p_line = new vdPolyline();


            List<string> strSeleModels = new List<string>();
            List<string> strSeleStrings = new List<string>();



            vdLayer current_layer = new vdLayer();

            layers.Clear();

            string progress_title = "Drawing Strings...";
            foreach (var item in lst_model_string)
            {

                if (!strSeleStrings.Contains(item.StringName) || !strSeleModels.Contains(item.ModelName))
                {
                    strSeleModels.Add(item.ModelName);
                    strSeleStrings.Add(item.StringName);

                    progress_title = "Drawing Strings..." + str_title;
                    current_layer = Create_Layer(item);
                    if (item.LayerColor != Color.Black)
                        current_layer.PenColor = new vdColor(item.LayerColor);
                    try
                    {
                        //layers.Add(item.StringName, current_layer);
                        layers.Add(item.ToString(), current_layer);
                    }
                    catch (Exception exx) { }
                }
            }

            try
            {

                #region ExecDrawString
                List<string> list_text = new List<string>();

                int iCurProgressInPercent = 0;

                bool bIsModel = false;
                bool bIsLabel = false;

                gPoint ptStart = new gPoint();
                gPoint ptEnd = new gPoint();
                gPoint ptLast = new gPoint();
                gPoint ptFirst3dFac = new gPoint();


                CTXTtype txt = new CTXTtype();
                vdLine uline = new vdLine();


                string _model = "";
                string _string = "";

                string strModelName = "";
                int NumPts = 0;
                string str1 = "";
                string stgLabel = "";
                gPoint aPoint = null;
                IHdPolyline3D polyline = null;
                CPTStype pts = new CPTStype();
                List<gPoint> pFArray = new List<gPoint>();

                //this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
                //this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

                //CCfgtype cfg = this.VDoc.ConfigParam;
                CCfgtype cfg = new CCfgtype();

                int iSelectedLineType = 1;

                double dTextHeight = (VDoc.ActiveTextStyle.Height > 0) ? VDoc.ActiveTextStyle.Height : 1.0;

                int p_count = 0;

                string strFilePath = m_strpathfilfile;


                if (File.Exists(strFilePath))
                {
                    //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                    //set the progress bar max value
                    //SetProgressBarMaxValue(100, progressBar_);
                    //Chiranjit [2011 08 10]
                    //Add Progress Bar and Time
                    //AutoProgressBar.frm_ProgressBar.ON("Reading Model file....");

                    //frm_ProgressBar.ON("Drawing Strings.....[MODEL=" + strSeleModels[0] + ", STRING=" + strSeleStrings[0] + "]");
                    frm_ProgressBar.ON(progress_title);


                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        try
                        {

                            CLabtype labtype = CLabtype.FromStream(br);

                            //list_text.Add(string.Format("{0} {1} {2}", labtype.attr, labtype.attrVal, labtype.Tag));

                            //AutoProgressBar.frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                            #region Model
                            if (labtype.attr == CLabtype.Type.Model) // Model Name
                            {
                                NumPts = 0;
                                strModelName = ((CModType)labtype.Tag).Name;


                                //list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, strModelName));
                                //list_text.Add(string.Format("{0}", strModelName));

                                bIsModel = (strSeleModels.Contains(strModelName)) ? true : false;
                            }
                            #endregion Model

                            #region String
                            else if (labtype.attr == CLabtype.Type.String)// String Label
                            {
                                stgLabel = ((CStgType)labtype.Tag).label;

                                list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, stgLabel));

                                if (bIsModel)
                                {
                                    bIsLabel = strSeleStrings.Contains(stgLabel);

                                    //p_ln.Label = _model + ":" + _string;
                                    if (bIsLabel)
                                    {
                                        _model = strModelName;
                                        _string = stgLabel;
                                        if (p_ln.VertexList.Count > 1)
                                        {
                                            //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                            DrawEntity(p_ln, stgLabel);
                                        }
                                    }
                                    p_ln.Label = _model + ":" + _string;
                                }
                            }
                            #endregion String

                            #region Point
                            else if (labtype.attr == CLabtype.Type.Point)
                            {
                                pts = (CPTStype)labtype.Tag;
                                try
                                {
                                    ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -898.0) ? 0.0 : pts.mz));
                                }
                                catch (Exception ex)
                                {
                                    ptEnd = new gPoint();
                                }
                                NumPts++;

                                ptStart = ptLast;
                                ptLast = ptEnd;

                                if (bIsModel && bIsLabel)
                                {
                                    uline.StartPoint = new gPoint(ptStart);
                                    uline.EndPoint = new gPoint(ptEnd);

                                    str1 = stgLabel;
                                    if (iSelectedLineType == 5)
                                    {
                                        if (true)
                                        {
                                            str1 = "X = " + ptEnd.x.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            //double _bt = vtxt.BoundingBox.Bottom;
                                            DrawEntity(vtxt, stgLabel);

                                            str1 = "Y = " + ptEnd.y.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.InsertionPoint.y -= (1.2 * dTextHeight);
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);


                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);

                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    else if (iSelectedLineType == 6)
                                    {
                                        if (true)
                                        {
                                            str1 = "Z = " + ptEnd.z.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);
                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt, stgLabel);
                                    }
                                    else if (iSelectedLineType == 7)
                                    {
                                        if (true)
                                        {
                                            str1 = stgLabel;

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);
                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    else if (iSelectedLineType == 8)
                                    {
                                        if (true)
                                        {
                                            str1 = strModelName;

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            str1 = stgLabel;

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.InsertionPoint.y -= (dTextHeight * 1.5);
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt, stgLabel);
                                            //ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                            //ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                            //DrawEntity(ln);

                                            //ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                            //ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                            //DrawEntity(ln);
                                        }
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt, stgLabel);
                                    }
                                    else if (str1.ToUpper().StartsWith("P") == true)
                                    {
                                        if (str1.ToUpper().StartsWith("P0") == true)
                                        {
                                            //txt.Point = ptEnd;
                                            //double[] res = ptEnd.GetCordArr();

                                            txt.tr = 0.0;

                                            str1 = ptEnd.z.ToString("0.000");

                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                            DrawEntity(ln, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                            DrawEntity(ln, stgLabel);



                                            //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, ptEnd.z), str1, dTextHeight);

                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));

                                        }
                                        else
                                        {
                                            vtxt.InsertionPoint = ptEnd;
                                            vtxt.TextString = str1;
                                            vtxt.Height = dTextHeight;
                                            DrawEntity(vtxt, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                            DrawEntity(ln, stgLabel);

                                            ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                            ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                            DrawEntity(ln, stgLabel);

                                            //txt.Point = ptEnd;
                                            //txt.tr = 0.0;
                                            //double[] res = ptEnd.GetCordArr();

                                            //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, dTextHeight), str1, dTextHeight);

                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                            //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));
                                        }
                                    }
                                    else
                                    {
                                        if (NumPts == 1)
                                        {
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                            if (iSelectedLineType == 1)
                                            {
                                                p_ln.VertexList.Add(ptEnd);
                                            }
                                            else if (iSelectedLineType == 4)
                                            {
                                                //aPoint.Erase();
                                                ptFirst3dFac = new gPoint(ptEnd);
                                                pFArray.Add(ptEnd);
                                            }
                                            else if (iSelectedLineType == 3)
                                            {
                                                pt.InsertionPoint = ptEnd;
                                                DrawEntity(pt);
                                            }


                                        }
                                        else if (NumPts == 2)
                                        {
                                            //uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                            //uline.elatt = 1;
                                            //uline.laatt = 1;
                                            uline.Label = strModelName;
                                            uline.Label += ":";
                                            uline.Label += stgLabel;
                                            //uline.scatt = 1;

                                            if (iSelectedLineType == 1)
                                            {
                                                //ln.StartPoint = uline.StartPoint;
                                                //ln.EndPoint = uline.EndPoint;
                                                //ln.Label = uline.Label;
                                                //DrawEntity(ln);

                                                p_ln.VertexList.Add(uline.EndPoint);
                                                //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                            }
                                            else if (iSelectedLineType == 2)
                                            {
                                                ln.StartPoint = uline.StartPoint;
                                                ln.EndPoint = uline.EndPoint;
                                                ln.Label = uline.Label;
                                                DrawEntity(ln, stgLabel);
                                                //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                            }
                                            else if (iSelectedLineType == 3)
                                            {
                                                pt.InsertionPoint = ptEnd;
                                                DrawEntity(pt, stgLabel);
                                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                            }
                                            else if (iSelectedLineType == 4)
                                            {
                                                pFArray.Add(ptEnd);
                                            }

                                            //aPoint.Erase();
                                        }
                                        else if (NumPts > 2)
                                        {
                                            if (iSelectedLineType == 1)
                                            {
                                                p_ln.VertexList.Add(ptEnd);
                                                //polyline.AppendVertex(ptEnd);
                                            }
                                            else if (iSelectedLineType == 2)
                                            {
                                                ln.StartPoint = uline.StartPoint;
                                                ln.EndPoint = uline.EndPoint;
                                                ln.Label = uline.Label;
                                                DrawEntity(ln, stgLabel);
                                                //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                            }
                                            else if (iSelectedLineType == 3)
                                            {
                                                pt.InsertionPoint = ptEnd;
                                                DrawEntity(pt, stgLabel);
                                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                            }
                                            else if (iSelectedLineType == 4)
                                            {
                                                pFArray.Add(ptEnd);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion Point

                            #region Text

                            else if (labtype.attr == CLabtype.Type.Text)
                            {
                                txt = (CTXTtype)labtype.Tag;

                                ptEnd = new gPoint(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);


                                if (bIsLabel && bIsModel)
                                {
                                    str1 = txt.tg;
                                    //Display an Elevation
                                    //TO DO:

                                    vtxt.InsertionPoint = ptEnd;
                                    vtxt.TextString = str1;
                                    vtxt.Height = dTextHeight;
                                    vtxt.Rotation = txt.tr;
                                    DrawEntity(vtxt, stgLabel);
                                    //IHdText xt = this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                                    //xt.Rotation = txt.tr;
                                    //xt.Height = txt.ts;
                                    //xt.VerJustify = eVerticalJustify.Bottom;
                                    //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                }
                            }
                            #endregion Text

                            #region EndCode

                            else if (labtype.attr == CLabtype.Type.EndCode)
                            {
                                if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                                {
                                    if ((System.Math.Abs(ptEnd.x - ptFirst3dFac.x) > 0.00001) || (System.Math.Abs(ptEnd.y - ptFirst3dFac.y) > 0.00001))
                                    {
                                        if (NumPts < 4)
                                        {
                                            pFArray.Add(ptFirst3dFac);	 //close the face
                                        }
                                    }

                                    if (pFArray.Count > 3)
                                    {
                                        //v3D_face.VertexList.Add(pFArray[0]);
                                        //v3D_face.VertexList.Add(pFArray[1]);
                                        //v3D_face.VertexList.Add(pFArray[2]);
                                        //v3D_face.VertexList.Add(pFArray[3]);


                                        v3D_face.VertexList[0] = (pFArray[0]);
                                        v3D_face.VertexList[1] = (pFArray[1]);
                                        v3D_face.VertexList[2] = (pFArray[2]);
                                        v3D_face.VertexList[3] = (pFArray[3]);
                                        pFArray.Clear();

                                    }
                                    DrawEntity(v3D_face, stgLabel);


                                }
                                if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                                {
                                    //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                    p_ln.Label = _model + ":" + _string;
                                    DrawEntity(p_ln, p_ln.Label);
                                    //DrawEntity(p_ln, stgLabel);
                                }
                                NumPts = 0;
                            }
                            #endregion EndCode
                            iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);



                            frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);

                            //progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                            //Thread.Sleep(0);
                        }
                        catch (Exception ex) { }
                    }

                    frm_ProgressBar.SetValue(br.BaseStream.Length, br.BaseStream.Length);
                    frm_ProgressBar.OFF();

                    if ((iSelectedLineType == 1))
                    {
                        if (p_ln.VertexList.Count > 1)
                        {
                            //Chiranjit [2012 02 02]
                            //p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                            //p_ln.Label = strModelName + ":" + stgLabel;
                            DrawEntity(p_ln, stgLabel);
                        }
                    }
                    //AutoProgressBar.frm_ProgressBar.OFF();
                    //br.Close();

                    //string m_pathlstfile = Path.Combine(Working_Folder, "model.des");

                    //File.WriteAllLines(m_pathlstfile, list_text.ToArray());

                }
                #endregion ExecDrawString

                vtxt.Dispose();
                ln.Dispose();
                p_ln.Dispose();
                v3D_face.Dispose();
                pt.Dispose();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();
                VDoc.Redraw(true);
                VDoc.EnableAutoGripOn = true;
                VDoc.EntitySelectMode = VectorDraw.Render.PickEntityMode.Auto;
                VDoc.ZoomAll();
                Vdraw.vdCommandAction.View3D_VTop(VDoc);

            }
        }
        bool Delete_Layer_Items(string name)
        {
            for (int i = 0; i < VDoc.ActiveLayOut.Entities.Count; i++)
            {
                if (VDoc.ActiveLayOut.Entities[i].Layer.Name == name &&
                    VDoc.ActiveLayOut.Entities[i].Deleted == false)
                    VDoc.ActiveLayOut.Entities[i].Deleted = true;
            }
            VDoc.Redraw(true);
            return true;
        }



        public vdDocument VDoc
        {
            get
            {
                //return vdSc_DGM.BaseControl.ActiveDocument;

                if (tc_main.SelectedTab == tab_land)
                {
                    //if (tc_survey.SelectedTab == tab_land)
                        return vdsC_DOC_Land.ActiveDocument;
                }
                if (tc_main.SelectedTab == tab_site_lvl_grd)
                {
                    if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step5)
                    {
                        if (tc_HDP_valign.SelectedTab == tab_HDP_valign_design)
                            return vdsC_HDP_Valign.ActiveDocument;
                    }
                    if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step7)
                    {
                            return vdsC_Volume.ActiveDocument;
                    }
                }

                return vdsC_Main.ActiveDocument;

            }
        }


        #endregion Draw Model Strings

        #region IHeadsApplication

        public IHeadsDocument ActiveDocument
        {
            get
            {
                if (tc_main.SelectedTab == tab_site_lvl_grd)
                {
                    if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step5)
                    {
                        //if (tc_HDP_valign.SelectedTab == tab_HDP_profile_opt) ;
                        //else if (tc_HDP_valign.SelectedTab == tab_HDP_update_valign_data) ;
                        //else if (tc_HDP_valign.SelectedTab == tab_HDP_valign_design)
                        if (tc_HDP_valign.SelectedTab == tab_HDP_valign_design)
                        {
                            return vdsC_HDP_Valign;
                        }
                    }
                    else if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step7)
                    {
                        return vdsC_Volume;
                    }
                }
                return vdsC_Main;
            }
        }

        public string AppDataPath
        {
            get
            {
                return Folder_Current;
            }
            set
            {
                Folder_Current = value;
            }
        }

        public void ZoomExtents()
        {
            //throw new NotImplementedException();
        }

        public void ZoomWindow(CPoint3D ptLowerLeft, CPoint3D ptUpperRight)
        {
            //throw new NotImplementedException();
        }

        public eHEADS_RELEASE_TYPE ReleaseType
        {
            get
            {
                return eHEADS_RELEASE_TYPE.PROFESSIONAL;
            }
        }

        public string ApplicationName
        {
            get { return "HEADS Site Projects"; }
        }

        public string ApplicationTitle
        {
            get { return "HEADS Site Projects"; }
        }

        public IHeadsFunctions HeadsFunctions { get; set; }

        public string SelectedModel { get; set; }

        public List<string> SelectedStrings { get; set; }

        public bool Is_Tunnel_Design { get; set; }

        public double TextSize { get; set; }

        public double Interval { get; set; }

        #endregion IHeadsApplication

        #region IVersion


        public bool IsDemo
        {
            get { return HASP_LOCK_CHECK.IsDemo; }
        }

        public void Set_HEADS_Environment_Variable(string file_path)
        {
            if (!File.Exists(file_path)) return;


            string exePath = Application.StartupPath;
            string WorkingPath = Path.GetDirectoryName(file_path);
            try
            {
                File.WriteAllText(Path.Combine(exePath, "hds.001"), file_path);
                File.WriteAllText(Path.Combine(exePath, "hds.002"), WorkingPath + "\\");
                FileInfo finfo = new FileInfo(file_path);
                if (finfo.IsReadOnly)
                    finfo.IsReadOnly = false;
            }
            catch (Exception exx)
            {
            }
            System.Environment.SetEnvironmentVariable("HDSPATH", exePath + "\\");

            if (IsDemo)
            {
                Environment.SetEnvironmentVariable("HEADSVER", "DEMO");
                System.Environment.SetEnvironmentVariable("DEM0", file_path);
                System.Environment.SetEnvironmentVariable("SURVEY", "");
            }
            else
            {
                Environment.SetEnvironmentVariable("HEADSVER", "PRO");
                System.Environment.SetEnvironmentVariable("SURVEY", file_path);
                System.Environment.SetEnvironmentVariable("DEM0", "");
            }
        }
        #endregion IVersion


        #region IProject

        public string Project_Folder
        {
            get
            {
                return Working_Folder;
            }
            set
            {
                Working_Folder = value;
            }
        }

        public bool Open_Project()
        {

            #region Open Project Folder
            frm_Open_Project fprj = new frm_Open_Project(this);
            if (fprj.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return false;
            #endregion Open Project Folde
            return true;

        }

        public string Get_Project_Name(eTypeOfProject proj)
        {
            string prj_folder = "";
            switch (proj)
            {
                case eTypeOfProject.Survey_Applications:
                    prj_folder = "Survey Applications";
                    break;
                case eTypeOfProject.Site_Leveling_Grading:
                    prj_folder = "Site Leveling and Grading";
                    break;
                case eTypeOfProject.Land_Record:
                    prj_folder = "Land Record Management";
                    break;
                case eTypeOfProject.Irrigation_Canal:
                    prj_folder = "Irrigation Canal Projects";
                    break;
                case eTypeOfProject.Irrigation_RiverDesiltation:
                    //prj_folder = "Irrigation Canal Projects";
                    prj_folder = "Irrigation River Desiltation Projects";
                    break;
                case eTypeOfProject.Irrigation_Dyke:
                    prj_folder = "Irrigation Dyke Projects";
                    break;
                case eTypeOfProject.Irrigation_SSA:
                    prj_folder = "Irrigation SSA Projects";
                    break;
                case eTypeOfProject.Irrigation_Stockpile:
                    prj_folder = "Stockpile Quantity Projects";
                    break;
                case eTypeOfProject.Irrigation_Discharge:
                    prj_folder = "Discharge Quantity Projects";
                    break;
                case eTypeOfProject.Mining_Excavation:
                    prj_folder = "Mining Open Cast Excavation Mesurement";
                    break;
                case eTypeOfProject.Mining_Stockpile:
                    prj_folder = "Mining Stockpile Quantity Mesurement";
                    break;
                case eTypeOfProject.UniformSection:
                    prj_folder = "Highway Design Projects";
                    break;
                case eTypeOfProject.Tunnel:
                    prj_folder = "Tunnel Design Projects";
                    break;
                case eTypeOfProject.Airport:
                    prj_folder = "Tunnel Design Projects";
                    break;
                case eTypeOfProject.Water_Distribution:
                    prj_folder = "Water Distribution Network";
                    break;
                case eTypeOfProject.StreamHydrology:
                    prj_folder = "Stream Hydrology Projects";
                    break;
            }
            return prj_folder;
        }


        public bool Select_Project_Folder()
        {

            #region Open Project Folder
            frm_Project_Folder fprj = new frm_Project_Folder(this);
            if (fprj.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return false;
            #endregion Open Project Folde
            return true;
        }

        public bool Select_Working_Folder()
        {
            return false;
        }

        public eTypeOfProject CurrentProjectType
        {
            get
            {
                return ProjectType;
            }
            set
            {
                ProjectType = value;
            }
        }

        public bool Show_Demo_Message(bool IsExcel)
        {

            frm_Demo_Message fdm = new frm_Demo_Message(IsExcel);
            fdm.Owner = this;
            fdm.ShowDialog();
            return true;
        }


        string _work = "";
        string Survey_Drawing_File { get; set; }
        public string Working_Folder
        {
            get
            {
                //if (Survey_File != "")
                //    _work = Path.GetDirectoryName(Survey_File);
                //else if (Project_File != "")
                //    _work = Path.GetDirectoryName(Project_File);

                return _work;
            }
            set
            {
                _work = value;
                Folder_Current = _work;

                //txt_Working_Folder.Text = value;
                if (value != "")
                    this.Text = Title + "[" + MyList.Get_Modified_Path(value) + "]";
                else
                    this.Text = Title;

                //if (ProjectType == eTypeOfProject.Tunnel)
                //{
                //    if (File.Exists(Survey_File))
                //    {
                //        if (!Directory.Exists(Folder_TNL_Tunnel_Lining))
                //            Directory.CreateDirectory(Folder_TNL_Tunnel_Lining);
                //    }
                //    lininG_DOC1.Working_Folder = Folder_TNL_Tunnel_Lining;
                //}
            }
        }
        public string Folder_HEADSSYS
        {
            get
            {
                return Path.Combine(Working_Folder, "HEADSSYS");
            }
        }

        public string Folder_Current { get; set; }

        public string Folder_Design_Drawings
        {
            get
            {
                return Path.Combine(Working_Folder, "Design Drawings");
            }
        }

        public string Folder_Process_Survey_Data
        {
            get
            {
                //return Path.Combine(Working_Folder, "Process Survey Data");
                //return Path.Combine(Folder_HEADSSYS, "Process Survey Data");
                return Folder_Ground_Modeling;

                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Process Survey Data");
                //return Folder_Current;
            }
        }
        public string Folder_Ground_Modeling
        {
            get
            {
                //return Path.Combine(Working_Folder, "Process Survey Data");
                return Path.Combine(Folder_HEADSSYS, "Ground Modeling");


                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Process Survey Data");
                //return Folder_Current;
            }
        }
        public string Folder_HDP_Halign
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Horizontal Alignment");
            }
        }


        public string Folder_HDP_Ground_Long_Section
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Ground_Long_Section");

                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Ground_Long_Section");

                //return Folder_Current;
            }
        }

        public string Folder_HDP_Vertical_Profile
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Vertical Profile");
                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Horizontal Alignment");
                //return Folder_Current;
            }
        }

        public string Folder_HDP_Cross_Section
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Cross_Section");
                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Horizontal Alignment");
                //return Folder_Current;
            }
        }

        public string Folder_HDP_Pavement_Layers
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Pavement Layers");
                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Horizontal Alignment");
                //return Folder_Current;
            }
        }
        public string Folder_HDP_Volume
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Volume");
                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Horizontal Alignment");
                //return Folder_Current;
            }
        }
        public string Folder_HDP_Profile_Optimization
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Profile Optimization");
                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Horizontal Alignment");
                //return Folder_Current;
            }
        }
        public string Folder_HDP_Drawings
        {
            get
            {
                return Path.Combine(Folder_HEADSSYS, "Project Drawings");
                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Horizontal Alignment");
                //return Folder_Current;
            }
        }



        public string Folder_Contour_Data
        {
            get
            {
                //return Path.Combine(Working_Folder, "Contours");
                //return Folder_Process_Survey_Data;
                return Path.Combine(Folder_HEADSSYS, "Contours");


                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Process Survey Data");
                //return Folder_Current;
            }
        }
        public string Folder_DTM_Data
        {
            get
            {
                //return Path.Combine(Working_Folder, "Contours");
                return Path.Combine(Folder_HEADSSYS, "DTM");


                //Folder_Current = Path.Combine(Folder_Highway_Design_Project, "Process Survey Data");
                //return Folder_Current;
            }
        }



        public eTypeOfProject ProjectType { get; set; }


        #endregion IProject

        private void sc_main_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.Refresh();
        }

        #region New Project

        private void btn_new_project_Click(object sender, EventArgs e)
        {
            Working_Folder = Set_Working_Folder();

            if (Directory.Exists(Working_Folder))
            {
                if (MessageBox.Show(txt_Project_Name.Text + " is already exist. Do you want to overwrite ?",
                    "HEADS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //Folder_Delete(Working_Folder, true);
                    Folder_Delete_Index(Working_Folder, true);
                }
            }



            Directory.CreateDirectory(Working_Folder);


            string fname = Path.Combine(Path.GetDirectoryName(Working_Folder), txt_Project_Name.Text + ".apr");


            int ty = (int)ProjectType;
            File.WriteAllText(fname, ty.ToString());


            btn_survey_browse.Enabled = true;
            txt_survey_data.Text = "";
            txt_Working_Folder.Text = "";

            Survey_File = "";

            MessageBox.Show(txt_Project_Name.Text + " Project is Created.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private string Set_Working_Folder()
        {
            //if (Working_Folder != "") return Working_Folder;


            string prj_folder = Path.Combine(AP_WORK_DIR, iProj.Get_Project_Name(ProjectType));
            if (!Directory.Exists(prj_folder))
                Directory.CreateDirectory(prj_folder);

            prj_folder = Path.Combine(prj_folder, txt_Project_Name.Text);
            //Directory.CreateDirectory(Working_Folder);
            return prj_folder;
        }

        public void Save_Data()
        {

            Save_FormRecord.Write_All_Data(this, Working_Folder);

            string fn = Path.Combine(Folder_HEADSSYS, ProjectType.ToString() + ".txt");
            File.WriteAllLines(fn, txt_Project_Name.Lines);
        }

        public bool Save_Data(bool msg)
        {

            if (txt_survey_data.Text == "") return true;

            System.Windows.Forms.DialogResult erd = System.Windows.Forms.DialogResult.Yes;

            if (msg)
            {
                erd = (MessageBox.Show("Do you want to Save Project Data file ?", "HEADS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

            }
            if (erd == System.Windows.Forms.DialogResult.Cancel) return false;

            Save_Data();
            if (msg)
            {
                if (txt_Working_Folder.Text != "")
                {
                    if (erd == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
                            Save_Site_Leveling_Grading_Project_File();
                        else if (ProjectType == eTypeOfProject.Irrigation_Canal)
                            Save_Irrigation_Canal_Project_File();
                        else if (ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
                            Save_Irrigation_RiverCanal_Project_File();
                        else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
                            Save_Irrigation_Dyke_Project_File();
                        else
                            Save_Project_File();
                        //Save_Data();
                    }

                }
            }
            //MyList.

            return true;
        }


        public bool Folder_Copy(string source_folder, string dest_folder)
        {
            return Folder_Copy(source_folder, dest_folder, false);
        }

        public bool Folder_Copy(string source_folder, string dest_folder, bool force_delete)
        {
            try
            {

                if (!Directory.Exists(source_folder))
                {
                    //MessageBox.Show("Previous Analysis [" + Path.GetFileName(source_folder) + "] not done.");
                    return false;
                }


                if (source_folder.ToLower() == dest_folder.ToLower())
                    return false;


                if (!Directory.Exists(dest_folder))
                    Directory.CreateDirectory(dest_folder);
                else
                {
                    List<string> file_list = new List<string>(Directory.GetFiles(dest_folder));

                    if (file_list.Count > 0)
                    {
                        //if (MessageBox.Show(this, "Previous Processed Data found. Do you want to overwrite with new data ?",
                        //    "HEADS", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        //{
                        foreach (var item in file_list)
                        {
                            try
                            {
                                if (Path.GetExtension(item).ToLower() != ".txt")
                                    File.Delete(item);
                            }
                            catch (Exception ex) { }
                        }
                        //}
                    }

                }


                List<string> file_src = new List<string>(Directory.GetFiles(source_folder));

                foreach (var item in file_src)
                {
                    try
                    {
                        string fl = Path.Combine(dest_folder, Path.GetFileName(item));
                        if (fl.ToLower() != item.ToLower())
                            File.Copy(item, fl, true);
                    }
                    catch (Exception exxx) { }
                    
                }
            }
            catch (Exception exx) { }


            return true;
        }
        public bool Folder_Delete_Index(string source_folder, bool is_folder_delete)
        {
            try
            {
                //if (source_folder.ToLower() == dest_folder.ToLower()) return;
                if (!Directory.Exists(source_folder)) return false;
                else
                {
                    List<string> file_list = new List<string>(Directory.GetFiles(source_folder));

                    if (file_list.Count > 0)
                    {
                        foreach (var item in file_list)
                        {
                            try
                            {
                                if (item.ToLower() != Survey_File.ToLower() &&
                                    item.ToLower() != Project_File.ToLower() &&
                                    item.ToLower() != Tunnel_Data_File.ToLower())
                                    File.Delete(item);
                            }
                            catch (Exception ex) { }
                        }
                    }

                    if (is_folder_delete)
                    {
                        file_list.Clear();
                        file_list.AddRange(Directory.GetDirectories(source_folder));
                        foreach (var item in file_list)
                        {
                            try
                            {
                                Folder_Delete_Index(item, is_folder_delete);
                            }
                            catch (Exception ex) { }
                        }
                        try
                        {
                            if (source_folder.ToLower() != Working_Folder.ToLower())
                                Directory.Delete(source_folder);
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch (Exception exx) { }
            return true;
        }
        public bool Folder_Delete(string folder_name, bool delete_folders)
        {
            if (!File.Exists(Survey_File))
            {
                //MessageBox.Show("Survey file not selected.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            int index = 0;
            try
            {
                List<string> proj_folds = new List<string>();
                proj_folds.Add(Working_Folder); // index 0
                proj_folds.Add(Folder_Process_Survey_Data); // index 1
                proj_folds.Add(Folder_DTM_Data); // index 1
                proj_folds.Add(Folder_Contour_Data); // index 1

                if (ProjectType == eTypeOfProject.MultipleSection ||
                    ProjectType == eTypeOfProject.HillRoad ||
                    ProjectType == eTypeOfProject.UniformSection)
                {
                    //proj_folds.Add(Folder_HDP_Halign); // index 2
                    //proj_folds.Add(Folder_HDP_Profile_Optimization); // index 3
                    //proj_folds.Add(Folder_HDP_Ground_Long_Section); // index 3
                    //proj_folds.Add(Folder_HDP_Vertical_Profile); // index 4
                    //proj_folds.Add(Folder_HDP_Cross_Section); // index 5
                    //proj_folds.Add(Folder_HDP_Pavement_Layers); // index 6
                    //proj_folds.Add(Folder_HDP_Volume);
                    //proj_folds.Add(Folder_HDP_Drawings); // index 7
                    //proj_folds.Add(Folder_Process_Project_Data); // index 8
                }
                else if (ProjectType == eTypeOfProject.Widening)
                {
                    //proj_folds.Add(Folder_HWP_Halign); // index 2
                    //proj_folds.Add(Folder_HWP_Ground_Long_Section); // index 3
                    //proj_folds.Add(Folder_HWP_Profile_Optimization); // index 4
                    //proj_folds.Add(Folder_HWP_Vertical_Profile); // index 5
                    //proj_folds.Add(Folder_HWP_Cross_Section); // index 6
                    //proj_folds.Add(Folder_HWP_Pavement_Layers); // index 7
                    ////proj_folds.Add(Folder_HDP_vol);
                    //proj_folds.Add(Folder_HWP_Drawings); // index 8
                    //proj_folds.Add(Folder_Process_Project_Data); // index 9
                }
                else if (ProjectType == eTypeOfProject.Tunnel)
                {
                    //proj_folds.Add(Folder_TNL_Halign); // index 1
                    //proj_folds.Add(Folder_TNL_Ground_Long_Section); // index 2
                    //proj_folds.Add(Folder_TNL_Vertical_Profile); // index 3
                    //proj_folds.Add(Folder_TNL_Cross_Section); // index 4
                    //proj_folds.Add(Folder_TNL_Drawings); // index 5
                    //proj_folds.Add(Folder_TNL_Coach_Profile); // index 6
                }


                for (index = 0; index < proj_folds.Count; index++)
                {
                    if (proj_folds[index].ToLower() == folder_name.ToLower())
                    {
                        break;
                    }
                }


                if (folder_name.ToLower() == Folder_Ground_Modeling.ToLower() ||
                    folder_name.ToLower() == Folder_DTM_Data.ToLower() ||
                    folder_name.ToLower() == Folder_Contour_Data.ToLower())
                {
                    //txt_HDP_HalignData.Text = "";
                    //txt_HDP_ValignData.Text = "";
                    //volume_HDP.HEADS_Data = new List<string>();
                    //pavement_HDP.HEADS_Data = new List<string>();
                }
                for (int i = index; i < proj_folds.Count; i++)
                {
                    //Folder_Delete_Index(proj_folds[i], delete_folders);
                }

            }
            catch (Exception exx) { }
            return true;
        }

        #endregion New Project


        #region Open Project

        private void btn_open_project_Click(object sender, EventArgs e)
        {
            
            if (this.TopMost) this.TopMost = false;
            iProj.CurrentProjectType = ProjectType;
            //if (iProj.Select_Project_Folder())

            iProj.Working_Folder = AP_WORK_DIR;

            if (Open_Project())
            {

                string prj_fld = Path.Combine(AP_WORK_DIR, Get_Project_Name(ProjectType));
                Working_Folder = Path.Combine(prj_fld, iProj.Project_Folder);

                //Working_Folder = Path.Combine(iProj.Working_Folder, iProj.Project_Folder);

                //Intersection_Project

                txt_Project_Name2.Text = iProj.Project_Folder;
                txt_Working_Folder.Text = Project_File;
                Working_Folder = Path.GetDirectoryName(Project_File);
                //Load_Files();
            }
            else
                return;

            Open_Project(sender, e);

        }

        public void Open_Project(object sender, EventArgs e)
        {
            if (this.TopMost) this.TopMost = false;

            Save_FormRecord.Read_All_Data(this, Working_Folder);

            Survey_File = txt_survey_data.Text;


            if (rbtn_total_station.Checked)
            {
                if (Path.GetExtension(Survey_File).ToLower() == ".txt")
                {
                    utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
                    utM_Conversion1.UTM_Data_Conversion_Load(sender, e);
                }
            }
            if (ProjectType != eTypeOfProject.Land_Record)
            {
                if (File.Exists(Project_File))
                    Load_Project_File(Project_File);
            }
            txt_Project_Name2.Text = iProj.Project_Folder;
            btn_survey_browse.Enabled = true;
            MessageBox.Show(this, "File Opened Successfully.", "HEADS");
        }


        #endregion Open Project

        private void tab_cont_Click(object sender, EventArgs e)
        {

        }

        #region Survey Applications

        #region Survey Buttons
        private void btn_ground_modeling_Click(object sender, EventArgs e)
        {
            tab_gm.Text = "";
            tab_trngu.Text = "";
            tab_cont.Text = "";
            tab_trvrs.Text = "";
            tab_land.Text = "";

            Button btn = sender as Button;
            tc_survey.TabPages.Clear();
            if (btn.Name == btn_ground_modeling.Name)
            {

                tc_survey.TabPages.Add(tab_gm);

            }
            else if (btn.Name == btn_traingulation.Name)
            {
                tc_survey.TabPages.Add(tab_trngu);
            }
            else if (btn.Name == btn_contour_modeling.Name)
            {
                tc_survey.TabPages.Add(tab_cont);
            }
            else if (btn.Name == btn_traverse_survey.Name)
            {
                tc_survey.TabPages.Add(tab_trvrs);
            }
            else if (btn.Name == btn_land_record.Name)
            {
                tc_survey.TabPages.Add(tab_land);
            }
        }

        string AP_WORK_DIR = "";

        private void frmProject1_Load(object sender, EventArgs e)
        {

            //Environment.SetEnvironmentVariable("AP_WORK_DIR", @"G:\Software Testings\ASTRA Pro\WORK");
            try
            {

                AP_WORK_DIR = Environment.GetEnvironmentVariable("AP_WORK_DIR");

                if (AP_WORK_DIR == null)
                {
                    MessageBox.Show("WORKING FOLDER NOT SELECTED", "STREAM HYDROLOGY", MessageBoxButtons.OK);
                    this.Close();
                    return;
                }

                if (iProj.Working_Folder == "")
                    iProj.Working_Folder = AP_WORK_DIR;

                //Working_Folder = Set_Working_Folder();





                ProjectType = eTypeOfProject.StreamHydrology;
                this.Text = Get_Project_Name(ProjectType);
                //tc_survey.TabPages.Clear();
                //tc_survey.TabPages.Add(tab_gm);
                //pcb_logo.BackgroundImage = HEADS_Site_Projects.Properties.Resources.techsoft_logo;

                tc_main_Selected();
                Set_Project_Name();

                tc_survey.TabPages.Remove(tab_auto_level_halign);
                SelectedStrings = new List<string>();
                SelectedModel = "DESIGN";

                uC_LandRecord1.Lan_rec = this;
                //tc_main.TabPages.Add(tab_auto_level_halign);

                tc_HDP_valign.TabPages.Remove(tab_HDP_profile_opt);
                tc_HDP_valign.TabPages.Remove(tab_HDP_update_valign_data);



                plan_Drawing_HDP.ProjectType = ProjectType;
                profile_Drawing_HDP.ProjectType = ProjectType;
                cross_Section_Drawing_HDP.ProjectType = ProjectType;



                plan_Drawing_HDP.frm_Plan_Load();
                profile_Drawing_HDP.frm_Profile_Load();
                cross_Section_Drawing_HDP.frm_Section_Load();


                Project = new HighwayRoadProject("");


                //if (ProjectType != eTypeOfProject.Land_Record)
                //{
                //    tc_main.TabPages.Remove(tab_land);
                //}

                tc_main.TabPages.Remove(tab_disnet);
                tc_main.TabPages.Remove(tab_trvrs);
                tc_survey.TabPages.Remove(tab_alignment);

                if (ProjectType == eTypeOfProject.Survey_Applications)
                {
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    tc_main.TabPages.Remove(tab_hydrology);


                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);
                }
                else if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_hydrology);

                    //tc_main.TabPages.Remove(tab_site_lvl_grd);


                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);

                    sc_SLG_cross_section.Panel1Collapsed = true;
                }
                else if (ProjectType == eTypeOfProject.Irrigation_Canal)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_hydrology);

                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);
                    //tc_main.TabPages.Remove(tab_site_lvl_grd);
                    sc_SLG_cross_section.Panel2Collapsed = true;
                    tab_site_lvl_grd.Text = "Canal Design";
                    uC_Dyke_CrossSection1.Visible = false;

                }
                else if (ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_hydrology);

                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);
                    //tc_main.TabPages.Remove(tab_site_lvl_grd);
                    sc_SLG_cross_section.Panel1Collapsed = true;
                    tc_HDP_drawings.TabPages.Remove(tab_align_sch);
                    sc_offset.SplitterDistance = sc_offset.Height / 2;
                    tab_site_lvl_grd.Text = "River Canal De-Siltation by Dredging";
                    uC_Dyke_CrossSection1.Visible = true;
                    uC_Canal_CrossSection1.Visible = false;

                }
                else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_hydrology);

                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);
                    //tc_main.TabPages.Remove(tab_site_lvl_grd);
                    sc_SLG_cross_section.Panel2Collapsed = true;
                    tab_site_lvl_grd.Text = "Earth Dam Dyke Design";
                    uC_Dyke_CrossSection1.Visible = true;
                    uC_Canal_CrossSection1.Visible = false;
                }
                else if (ProjectType == eTypeOfProject.Irrigation_Stockpile)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_hydrology);

                    tc_survey.TabPages.Remove(tab_discrg_qty);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    sc_SLG_cross_section.Panel2Collapsed = true;
                    tab_strg_qty.Text = "Stockpile Quantity Mesurement";
                    uC_Stockpile.Title = "Stockpile Quantity Mesurement";

                    uC_Stockpile.ProjectType = ProjectType;


                    uC_Dyke_CrossSection1.Visible = true;
                    uC_Canal_CrossSection1.Visible = false;
                }
                else if (ProjectType == eTypeOfProject.Irrigation_Discharge)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_hydrology);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);

                    tc_survey.TabPages.Remove(tab_strg_qty);
                    sc_SLG_cross_section.Panel2Collapsed = true;
                    tab_discrg_qty.Text = "Discharge Quantity Measurement";
                    uC_Discharge.Title = "Discharge Quantity Mesurement";
                    uC_Discharge.ProjectType = ProjectType;

                    uC_Dyke_CrossSection1.Visible = true;
                    uC_Canal_CrossSection1.Visible = false;
                }
                else if (ProjectType == eTypeOfProject.Mining_Excavation)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_main.TabPages.Remove(tab_hydrology);

                    //tc_survey.TabPages.Remove(tab_discrg_qty);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    sc_SLG_cross_section.Panel2Collapsed = true;
                    tab_discrg_qty.Text = "Open Cast Excavation Mesurement";

                    uC_Discharge.Title = "Open Cast Excavation Mesurement";
                    uC_Discharge.ProjectType = ProjectType;



                    uC_Dyke_CrossSection1.Visible = true;
                    uC_Canal_CrossSection1.Visible = false;
                }
                else if (ProjectType == eTypeOfProject.Mining_Stockpile)
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_survey.TabPages.Remove(tab_discrg_qty);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    tc_main.TabPages.Remove(tab_hydrology);
                    sc_SLG_cross_section.Panel2Collapsed = true;
                    tab_strg_qty.Text = "Stockpile Quantity Mesurement";
                    uC_Stockpile.Title = "Stockpile Quantity Mesurement";
                    uC_Stockpile.ProjectType = ProjectType;

                    uC_Dyke_CrossSection1.Visible = true;
                    uC_Canal_CrossSection1.Visible = false;
                }
                else if (ProjectType == eTypeOfProject.Land_Record)
                {
                    tc_main.TabPages.Remove(tab_survey);
                    tc_main.TabPages.Remove(tab_trvrs);
                    //tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    tc_main.TabPages.Remove(tab_hydrology);

                    grb_survey_type.Visible = false;


                    txt_survey_data.Visible = false;
                    btn_survey_browse.Visible = false;
                    lbl_select_survey.Visible = false;
                }
                else if (ProjectType == eTypeOfProject.Water_Distribution)
                {
                    AppDocument = new CDisNetDoc();
                    uC_DisNet1.iApp = this;
                    uC_DisNet1.doc = VDoc;


                    //tc_main.TabPages.Remove(tab_survey);
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    tc_main.TabPages.Remove(tab_hydrology);

                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);

                    tc_main.TabPages.Add(tab_disnet);
                }
                else if (ProjectType == eTypeOfProject.StreamHydrology)
                {
                    AppDocument = new CDisNetDoc();
                    uC_DisNet1.iApp = this;
                    uC_DisNet1.doc = VDoc;

                    uC_StreamHydrology1.SetProjectApplication(this);
                    //tc_main.TabPages.Remove(tab_survey);
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);

                    //tc_main.TabPages.Remove(tab_hydrology);

                    tc_survey.TabPages.Remove(tab_strg_qty);
                    tc_survey.TabPages.Remove(tab_discrg_qty);

                    tc_survey.TabPages.Add(tab_alignment);
                    rbtn_bearing_line.Visible = false;

                }
                else
                {
                    tc_main.TabPages.Remove(tab_trvrs);
                    tc_main.TabPages.Remove(tab_land);
                    tc_main.TabPages.Remove(tab_site_lvl_grd);
                    tc_survey.TabPages.Remove(tab_strg_qty);
                }

                rtb_bowditch_data.Text = "";
                rtb_transit_data.Text = "";
                rtb_edm_data.Text = "";
                rtb_closed_link_data.Text = "";
            }
            catch (Exception exx) {
                MessageBox.Show(exx.ToString());
            }

        }
        public void Set_Project_Name()
        {
            string dir = Path.Combine(iProj.Working_Folder, Get_Project_Name(ProjectType));

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

            txt_Project_Name.Text = prj_name;


            lbl_Title.Text = Get_Project_Name(ProjectType);
        }



        private void grb_survey_type_Enter(object sender, EventArgs e)
        {

        }

        private void rbtn_survey_options_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;

            cmb_transverse.Enabled = rbtn_transverse.Checked;

            if (cmb_transverse.Enabled)
            {
                if (cmb_transverse.SelectedIndex == -1) cmb_transverse.SelectedIndex = 0;
            }


            lbl_survey_data.Text = "SURVEY DATA TYPE : " + rbtn.Text;


            if (rbtn_transverse.Checked)
            {
                if (!tc_main.TabPages.Contains(tab_trvrs)) tc_main.TabPages.Add(tab_trvrs);
            }
            else
            {
                if (tc_main.TabPages.Contains(tab_trvrs)) tc_main.TabPages.Remove(tab_trvrs);
            }
            //if (rbtn_auto_level.Checked)
            //{
            //    if (tc_survey.TabPages.Contains(tab_auto_level_halign) == false)
            //    {
            //        //tc_survey.TabPages.Add(tab_auto_level_halign);
            //        tc_survey.TabPages.Insert(0, tab_auto_level_halign);
            //        tc_survey.SelectedTab = tab_auto_level_halign;
            //    }
            //}
            //else
            //{
            //    if (tc_survey.TabPages.Contains(tab_auto_level_halign))
            //    {
            //        tc_survey.TabPages.Remove(tab_auto_level_halign);
            //    }
            //}
        }
        #endregion Survey Buttons

        #region Create Project
        public void Create_Project()
        {
            Working_Folder = Set_Working_Folder();
            Directory.CreateDirectory(Working_Folder);
            string fname = Path.Combine(Path.GetDirectoryName(Working_Folder), txt_Project_Name.Text + ".apr");
            int ty = (int)ProjectType;
            File.WriteAllText(fname, ty.ToString());
        }
        #endregion  Create Project

        #region Select Survey Data

        private void btn_browse_Click(object sender, EventArgs e)
        {
            if (this.TopMost) this.TopMost = false;

            if (rbtn_survey_drawing.Checked)
            {
                Select_Survey_Drawing();
            }
            else
            {
                Select_Survey_File();
            }
        }

        private void Select_Survey_File()
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a Survey Data File";
                if (rbtn_bearing_line.Checked)
                    ofd.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                else
                    ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                ofd.InitialDirectory = Working_Folder;

                if (Working_Folder == "")
                {
                    ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                }

                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    if (iProj.Working_Folder == null)
                    {
                        iProj.Working_Folder = Path.GetDirectoryName(ofd.FileName);
                    }


                    if (!Directory.Exists(Working_Folder))
                        Directory.CreateDirectory(Working_Folder);

                    string sds = Path.Combine(Working_Folder, Path.GetFileName(ofd.FileName));

                    if (ofd.FileName.ToLower() != sds.ToLower())
                    {
                        File.Copy(ofd.FileName, sds, true);

                    }

                    Survey_File = sds;


                    MessageBox.Show("Survey Data file " + Path.GetFileName(Survey_File) + " is taken inside the Project Folder.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (rbtn_total_station.Checked)
                    {
                        //Working_Folder = Path.Combine(Working_Folder, txt_Project_Name.Text);

                        if (!Directory.Exists(Working_Folder))
                            Directory.CreateDirectory(Working_Folder);

                        Survey_File = Path.Combine(Working_Folder, Path.GetFileName(ofd.FileName));

                        if (ofd.FileName.ToLower() != Survey_File.ToLower())
                            File.Copy(ofd.FileName, Survey_File, true);

                        //txt_survey_data.Text = Path.GetFileName(Survey_File);
                        txt_survey_data.Text = Survey_File;


                    }
                    else if (rbtn_auto_level.Checked)
                    {
                        string fds = Working_Folder;

                        //Working_Folder = Path.Combine(Working_Folder, txt_Project_Name.Text);


                        if (!Directory.Exists(Working_Folder))
                            Directory.CreateDirectory(Working_Folder);

                        Folder_Copy(fds, Working_Folder);

                        Survey_File = Path.Combine(Working_Folder, Path.GetFileName(ofd.FileName));

                        if (ofd.FileName.ToLower() != Survey_File.ToLower())
                            File.Copy(ofd.FileName, Survey_File, true);
                        txt_survey_data.Text = Survey_File;

                        //Interval_Chainage
                        List<string> list = new List<string>(File.ReadAllLines(Survey_File));
                        MyList ml;
                        double d1 = -1.0;
                        bool flag = false;
                        List<double> all_chns = new List<double>();
                        double sc = 0.0;
                        double ec = 0.0;
                        for (int i = 0; i < list.Count; i++)
                        {
                            ml = new MyList(MyList.RemoveAllSpaces(list[i]), ' ');
                            if (ml.Count == 3)
                            {
                                all_chns.Add(ml[0]);
                                if (flag)
                                {
                                    Interval_Chainage = Math.Abs(ml[0] - d1);
                                    flag = false;
                                    //break;
                                }
                                if (d1 == -1.0)
                                {
                                    flag = true;
                                    d1 = ml[0];
                                    sc = d1;
                                    ec = d1;
                                }

                                if (sc > ml[0]) sc = ml[0];
                                if (ec < ml[0]) ec = ml[0];



                            }
                        }

                        Start_Chainage = sc;
                        End_Chainage = ec;
                    }
                    else if (rbtn_bearing_line.Checked)
                    {
                        string fds = Working_Folder;

                        if (!Directory.Exists(Working_Folder))
                            Directory.CreateDirectory(Working_Folder);

                        Folder_Copy(fds, Working_Folder);

                        Survey_File = Path.Combine(Working_Folder, Path.GetFileName(ofd.FileName));
                        if (Path.GetExtension(Survey_File).ToLower().StartsWith(".xls"))
                            MessageBox.Show("Open MS Excel file for Bearing Line Data and \nSave as Text File \"(Tab delimited) (*.txt)\" in the working folder.", "HEADS", MessageBoxButtons.OK);
                        if (ofd.FileName.ToLower() != Survey_File.ToLower())
                        {
                            File.Copy(ofd.FileName, Survey_File, true);
                        }
                        txt_survey_data.Text = Survey_File;
                    }

                    System.Diagnostics.Process.Start(Survey_File);
                    txt_Working_Folder.Text = Project_File;
                }
            }
            //string fld = txt_Working_Folder.Text;
            //if (chk_create_project_directory.Checked)
            //{
            //    fld = Path.Combine(fld, txt_Project_Name.Text);
            //    if (!Directory.Exists(fld))
            //        Directory.CreateDirectory(fld);
            //}

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");

        }
        private void Select_Survey_Drawing()
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a Survey Drawing File";
                ofd.Filter = "Drawing Files (*.dwg;*.dxf;*.vdml)|*.dwg;*.dxf;*.vdml";
                ofd.InitialDirectory = Working_Folder;

                if (Working_Folder == "")
                {
                    ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                }

                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    //Survey_File = ofd.FileName;
                    //Working_Folder = Path.GetDirectoryName(ofd.FileName);
                    //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);


                    if (!Directory.Exists(Working_Folder))
                        Directory.CreateDirectory(Working_Folder);

                    string sds = Path.Combine(Working_Folder, Path.GetFileName(ofd.FileName));

                    if (ofd.FileName.ToLower() != sds.ToLower())
                    {
                        File.Copy(ofd.FileName, sds, true);
                    }
                    //else
                    //    return;

                    Survey_Drawing_File = sds;
                    Survey_File = sds;

                    if (rbtn_survey_drawing.Checked)
                    {
                        VDoc.Open(Survey_Drawing_File);
                        VDoc.Redraw(true);
                        vdsC_Main.View3D_VTOP();

                    }
                    txt_survey_data.Text = Survey_File;
                }
            }

            //string fld = txt_Working_Folder.Text;
            //if (chk_create_project_directory.Checked)
            //{
            //    fld = Path.Combine(fld, txt_Project_Name.Text);
            //    if (!Directory.Exists(fld))
            //        Directory.CreateDirectory(fld);
            //}

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");

            //txt_Working_Folder.Text = Project_File;
        }

        #endregion  Select Survey Data

        #region Open Tutorials
        private void btn_tutorial_example_Click(object sender, EventArgs e)
        {
            Clear_Project();
               
            if (ProjectType == eTypeOfProject.Survey_Applications)
            {
                #region Survey_Applications
                if (rbtn_total_station.Checked)
                {
                    Load_Site_Application_Total_Station();
                }
                else if (rbtn_auto_level.Checked)
                {
                    Load_Site_Application_AutoLevel();
                }
                else if (rbtn_bearing_line.Checked)
                {
                    Load_Site_Application_BearingLine();
                }
                else if (rbtn_survey_drawing.Checked)
                {
                    Load_Site_Application_Drawing();
                }
                else if (rbtn_transverse.Checked)
                {
                    if (cmb_transverse.SelectedIndex == 0) Load_Site_Application_Traverse();
                }
                #endregion Survey_Applications
            }
            else if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
            {
                Load_Project_Tutoial_Site_Leveling_Grading();
            }
            else if (ProjectType == eTypeOfProject.Land_Record)
            {
                Load_Project_Tutoial_Land_Record();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_Canal)
            {
                Load_Project_Tutoial_Irrigation_Canal();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
            {
                Load_Project_Tutoial_Irrigation_RiverDesiltation();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
            {
                Load_Project_Tutoial_Irrigation_Dyke();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_Stockpile)
            {
                Load_Project_Tutoial_Stock_Quantity();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_Discharge)
            {
                Load_Project_Tutoial_Discharge_Quantity();
            }
            else if (ProjectType == eTypeOfProject.Mining_Excavation)
            {
                Load_Project_Tutoial_Mining_Open_Cast_Excavation();
            }
            else if (ProjectType == eTypeOfProject.Mining_Stockpile)
            {
                Load_Project_Tutoial_Stock_Quantity();
            }
            else if (ProjectType == eTypeOfProject.Water_Distribution)
            {
                Load_Project_Tutoial_Water_Distribution();
            }
            else if (ProjectType == eTypeOfProject.StreamHydrology)
            {
                Load_Project_Tutoial_Stream_Hydrology();
            }
            Save_FormRecord.Read_All_Data(this, Working_Folder);
        }

        #region Survey Applications
        public void Load_Site_Application_Total_Station()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Total_Station");


            txt_Project_Name.Text = "Survey with Total Station Data";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_AutoLevel()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Autolevel");


            txt_Project_Name.Text = "Survey with Autolevel Data";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            if (File.Exists(Survey_File))
            {
                #region ProHalign Data
                List<string> list = new List<string>();
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("PROHEADS"));
                list.Add(string.Format("200,PROHALIGN"));
                list.Add(string.Format("201,MODEL=DESIGN,STRING=M001"));
                list.Add(string.Format("210,DATAFILE"));
                list.Add(string.Format("211,SC=31150.000,IN=5.0,XC=0.0,YC=0.0,RA=10.0,V=40.0,e=5.0"));
                list.Add(string.Format("212 256417.51610 3637436.73310   "));
                list.Add(string.Format("212 256498.00000 3637364.00000 75.000  "));
                list.Add(string.Format("212 256673.60640 3637140.99940 40.000  "));
                list.Add(string.Format("212 256446.50280 3637118.84380 200.000  "));
                list.Add(string.Format("212 256331.18450 3637034.07340 60.000  "));
                list.Add(string.Format("212 256042.76480 3637304.54440 600.000  "));
                list.Add(string.Format("212 255780.00000 3637347.00000 75.000  "));
                list.Add(string.Format("212 255417.01810 3637516.55790 300.000  "));
                list.Add(string.Format("212 255006.37050 3637283.04960 30.000  "));
                list.Add(string.Format("212 255355.47130 3637331.98420 15.000  "));
                list.Add(string.Format("212 255294.01160 3637213.39950 75.000  "));
                list.Add(string.Format("212 255254.00000 3637162.00000 20.000  "));
                list.Add(string.Format("212 255248.83160 3637090.89500 15.000  "));
                list.Add(string.Format("212 255285.00000 3637142.00000 15.000  "));
                list.Add(string.Format("212 255321.28080 3637200.01530 75.000  "));
                list.Add(string.Format("212 255471.77510 3637169.24120 75.000  "));
                list.Add(string.Format("212 255589.27700 3637066.76640 75.000  "));
                list.Add(string.Format("212 255594.74250 3637025.55150 25.000  "));
                list.Add(string.Format("212 255594.74250 3637025.55150   "));
                list.Add(string.Format("212 255624.16860 3636992.70850   "));
                list.Add(string.Format("FINISH"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                //txt_HDP_HalignData.Lines = list.ToArray();

                #endregion ProHalign Data
                rtb_auto_level_prohalign.Lines = list.ToArray();

                cmb_auto_halign.SelectedIndex = 3;
            }

            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_BearingLine()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Bearing_Line");


            txt_Project_Name.Text = "Survey with Bearing Line Data";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();


            Survey_File = Path.Combine(Working_Folder, "BL Data File.xls");

            MessageBox.Show("Open MS Excel file for Bearing Line Data and \nSave as Text File \"(Tab delimited) (*.txt)\" in the working folder.", "HEADS", MessageBoxButtons.OK);

            txt_survey_data.Text = Survey_File;

            System.Diagnostics.Process.Start(Survey_File);
            //design_Section_Schedule1.Start_Chainage = 0;
            //design_Section_Schedule1.End_Chainage = 3000.0;

            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;

            Project = new HighwayRoadProject(Project_File);

            txt_survey_data.Text = Survey_File;

            //rtb_auto_level_halign.Lines = File.ReadAllLines(Survey_File);
            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_Drawing()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Drawing");


            txt_Project_Name.Text = "Survey with Drawing File";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_Drawing_File = Path.Combine(Working_Folder, "SURVEY.DWG");
            Survey_File = Survey_Drawing_File;
            if (File.Exists(Survey_Drawing_File))
            {
                VDoc.Open(Survey_Drawing_File);
                VDoc.Redraw(true);
                vdsC_Main.View3D_VTOP();
            }


            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_Traverse()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Traverse");


            txt_Project_Name.Text = "Survey with Traverse Data";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();



            Survey_File = Path.Combine(Working_Folder, "Bowditch.txt");

            if (File.Exists(Survey_File)) rtb_bowditch_data.Lines = File.ReadAllLines(Survey_File);


            Survey_File = Path.Combine(Working_Folder, "Transit.txt");

            if (File.Exists(Survey_File)) rtb_transit_data.Lines = File.ReadAllLines(Survey_File);

            Survey_File = Path.Combine(Working_Folder, "EDM.txt");

            if (File.Exists(Survey_File)) rtb_edm_data.Lines = File.ReadAllLines(Survey_File);


            Survey_File = Path.Combine(Working_Folder, "Closed_Link.txt");

            if (File.Exists(Survey_File)) rtb_closed_link_data.Lines = File.ReadAllLines(Survey_File);


            Survey_File = Path.Combine(Working_Folder, "Bowditch.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_Transverse_Bowditch()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Traverse\Bowditch");


            txt_Project_Name.Text = "Survey with Traverse Bowditch Data";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "Bowditch.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_Transverse_Transit()
        {

            string src = "";

            string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Traverse\Transit");


            txt_Project_Name.Text = "Survey with Traverse Transit Method";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "Transit.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_Transverse_Closed_Link()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Traverse\Closed_Link");


            txt_Project_Name.Text = "Survey with Traverse Closed Link Method";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "Closed_Link.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        public void Load_Site_Application_Transverse_EDM()
        {

            string src = "";

            string dest = "";

            //rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Survey_Data_Traverse\EDM");


            txt_Project_Name.Text = "Survey with Traverse EDM Method";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "EDM.txt");

            //chk_utm_conversion.Checked = true;
            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;
        }
        #endregion Survey Applications

        #region Site Leveling and Grading


        public void Load_Project_Tutoial_Site_Leveling_Grading()
        {
            string src = "";

            string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Site Leveling and Grading");

            txt_Project_Name.Text = "Tutorial Site Leveling and Grading";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());
            //design_Section_Schedule1.cmb_cstype.SelectedIndex = 0;
            //design_Section_Schedule1.Start_Chainage = 0;
            //design_Section_Schedule1.End_Chainage = 3000.0;


            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);

            List<IHEADS> hds_data = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);
            IHEADS sa = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN)[0];


            Project.HalignData = sa.Get_HEADS_Data();

            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;

            txt_HDP_HalignData.Lines = Project.HalignData.ToArray();



            sa = Project.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN)[0];


            Project.ValignData = sa.Get_HEADS_Data();
            txt_HDP_ValignData.Lines = Project.ValignData.ToArray();


            //Project_Example = eProject_Examples.Tutoial_01;

            //pavement_HDP.Load_Project_Example(Project_Example);
            //roadVolume1.Load_Project_Example(Project_Example);
            //plan_Drawing_HDP.Load_Project_Example(Project_Example);
            //profile_Drawing_HDP.Load_Project_Example(Project_Example);
            //cross_Section_Drawing_HDP.Load_Project_Example(Project_Example);


            Load_Project_File(Project_File);

        }

        public void Load_Project_File(string file_name)
        {
            if (!File.Exists(file_name)) return;
            HEADS_Project_Data hpd = new HEADS_Project_Data(file_name);
            List_IHEADS hds_data;


            IHEADS hds = hpd.Find_Option_Data(eHEADS_OPTION._104_DGM_Contour);

            if (hds != null)
            {
                txt_Contour_pri_inc.Text = hds.Data[1].Find_Value("INC");
                //txt_Contour_.Text = hds.Data[1].Find_Value("INC");
            }

            hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._Utilities);

            if (hds_data.Count > 0)
            {
                _Utilities util = (_Utilities)hds_data[0].Get_Object_Data();



                Survey_File = Path.Combine(Working_Folder, util.Data[0].StringList[util.Data[0].StringList.Count - 1]);
            }

            if (!File.Exists(Survey_File))
            {
                if (MessageBox.Show("Survey Data File ('" + Path.GetFileName(Survey_File) + "') not found. Do you want to select Survey Data File ?", "HEADS",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    Select_Survey_File();
                }
            }


            hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._104_DGM_Contour);

            if (hds_data.Count > 0)
            {
                txt_Contour_pri_inc.Text = hds_data[0].Data[1].Find_Value("INC");
            }


            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;

            #region Site_Leveling_Grading
            if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
            {
                //hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._Utilities);

                //if (hds_data.Count > 0)
                //{
                //    _Utilities util = (_Utilities)hds_data[0].Get_Object_Data();
                //    Survey_File = Path.Combine(Working_Folder, util.Data[0].StringList[util.Data[0].StringList.Count - 1]);
                //}
                //if (!File.Exists(Survey_File))
                //{
                //    if (MessageBox.Show("Survey Data File ('" + Path.GetFileName(Survey_File) + "') not found. Do you want to select Survey Data File ?", "HEADS",
                //        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        Select_Survey_File();
                //    }
                //}
                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);

                if (hds_data.Count > 0)
                    txt_HDP_HalignData.Lines = hds_data.Get_HEADS_Data().ToArray();



                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_ProHalign);

                if (hds_data.Count > 0)
                {
                    bool flg = true;
                    foreach (var item in hds_data)
                    {
                        foreach (var item1 in item.Data)
                        {
                            if (item1.ToString().StartsWith("212"))
                            {
                                txt_HDP_HalignData.Lines = item.Get_HEADS_Data().ToArray();
                                flg = false;
                                cmb_SLG_halign.SelectedIndex = 3;
                                break;
                            }
                        }
                        if (!flg) break;
                    }
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN);

                if (hds_data.Count > 0)
                {
                    txt_HDP_ValignData.Lines = hds_data.Get_HEADS_Data().ToArray();
                    cmb_HDP_Valign.SelectedIndex = 2;
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._400_OFFSET);

                #region Read Cross Section

                CS_Data_Collection cs_col = new CS_Data_Collection();

                ChainageCollection chn_col = new ChainageCollection();
                ChainageDetails chn_dtls = new ChainageDetails();

                if (hds_data.Count > 0)
                {
                    //CS_Data data = new CS_Data(

                    string kStr = "";
                    MyList ml = null;
                    string ref_str = "";
                    double HO1, HO2, VO1, VO2;

                    HO1 = 0.0;
                    HO2 = 0.0;
                    VO1 = 0.0;
                    VO2 = 0.0;
                    foreach (var item in hds_data)
                    {
                        for (int i = 0; i < item.Data.Count; i++)
                        {
                            kStr = item.Data[i].ToString();
                            kStr = kStr.Replace(",", " ").Replace("=", " ");
                            kStr = MyList.RemoveAllSpaces(kStr);

                            ml = new MyList(kStr, ' ');
                            if (ml.StringList[0].StartsWith("401"))
                            {
                                ref_str = ml[4].ToString();
                            }
                            else if (ml.StringList[0].StartsWith("402"))
                            {
                                chn_dtls = new ChainageDetails();
                                chn_dtls.Format = ChainageDetails.eFormat._402;
                                chn_dtls.RefString = ref_str;
                                chn_dtls.Model = ml[2].ToString();
                                chn_dtls.String = ml[4].ToString();
                                chn_dtls.CH1 = ml[6].Value;
                                chn_dtls.CH2 = ml[8].Value;
                                chn_dtls.HO1 = ml[10].Value;
                                chn_dtls.HO2 = ml[12].Value;
                                chn_dtls.VO1 = ml[14].Value;
                                chn_dtls.VO2 = ml[16].Value;

                                chn_col.Add(chn_dtls);
                            }
                            else if (ml.StringList[0].StartsWith("403"))
                            {
                                chn_dtls = new ChainageDetails();
                                chn_dtls.Format = ChainageDetails.eFormat._403;
                                chn_dtls.RefString = ref_str;
                                chn_dtls.Model = ml[2].ToString();
                                chn_dtls.String = ml[4].ToString();
                                chn_dtls.CH1 = ml[6].Value;
                                chn_dtls.CH2 = ml[8].Value;
                                chn_dtls.HO1 = ml[10].Value;
                                chn_dtls.HO2 = ml[12].Value;
                                chn_dtls.VO1 = ml[14].Value;
                                chn_dtls.VO2 = ml[16].Value;

                                chn_col.Add(chn_dtls);
                            }
                        }
                    }

                }
                Start_Chainage = chn_col.GetStartChainage();
                End_Chainage = chn_col.GetEndChainage();



                //cs_col.Start_Chainage = Start_Chainage;
                //cs_col.End_Chainage = End_Chainage;
                //cs_col.TypeName = "TCS07";
                //foreach (var item in chn_col)
                //{

                //    CS_Data cs = new CS_Data();
                //    cs.RefStringName = item.RefString;
                //    cs.StringName = item.String;
                //    cs.HO = item.HO1;
                //    cs.VO = item.VO1;
                //    cs_col.Add_Data(cs.ToString());
                //}
                foreach (var item in chn_col)
                {
                    dgv_offset_service.Rows.Add(
                        item.RefString,
                        item.String,
                        item.CH1.ToString("f3"),
                        item.CH2.ToString("f3"),
                        item.HO1.ToString("f3"),
                        item.HO2.ToString("f3"),
                        item.VO1.ToString("f3"),
                        item.VO2.ToString("f3"));
                }

                #endregion Read Cross Section

                Project.All_Cross_Section_Types.Add(cs_col);

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._500_XSECTIONS);
                //pavement_HDP.HEADS_Data = new List<string>();
                if (hds_data.Count > 1)
                {
                    foreach (var item in hds_data)
                    {
                        _500_XSECTIONS pv = (_500_XSECTIONS)item.Get_Object_Data();
                        pv.Set_Option_Title();

                        if (pv.Is_PaveLayers)
                        {
                            //pavement_HDP.HEADS_Data = pv.Get_HEADS_Data();
                            break;
                        }
                    }
                }


                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._600_INTERFACE);

                if (hds_data.Count > 0)
                {
                    interface_HDP.Data = hds_data[0].Get_HEADS_Data();
                    //rtb_HDP_interface.Lines = hds_data[0].Get_HEADS_Data().ToArray();
                    //interface_HDP.Data = new List<string>(rtb_HDP_interface.Lines);
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._700_Volume);
                if (hds_data.Count > 0)
                    Volume_HDP_Data.Lines = hds_data.Get_HEADS_Data().ToArray();

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_ProHalign);
                if (hds_data.Count > 0)
                    halign_Diagram_HDP.Load_ProHALIGN_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._300_ProValign);
                if (hds_data.Count > 0)
                    valign_Diagram_HDP.Load_ProVALIGN_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1100_Plan);
                if (hds_data.Count > 0)
                    plan_Drawing_HDP.Load_Plan_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1200_Profile);
                if (hds_data.Count > 0)
                    profile_Drawing_HDP.Load_Profile_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1300_Section);
                if (hds_data.Count > 0)
                    cross_Section_Drawing_HDP.Load_Section_Data(hds_data[0].Get_HEADS_Data());
            }
            #endregion Site_Leveling_Grading


            #region Irrigation Canal
            else if (ProjectType == eTypeOfProject.Irrigation_Canal || ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)

            {
                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);

                if (hds_data.Count > 0)
                    txt_HDP_HalignData.Lines = hds_data.Get_HEADS_Data().ToArray();

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_ProHalign);

                if (hds_data.Count > 0)
                {
                    bool flg = true;
                    foreach (var item in hds_data)
                    {
                        foreach (var item1 in item.Data)
                        {
                            if (item1.ToString().StartsWith("212"))
                            {
                                txt_HDP_HalignData.Lines = item.Get_HEADS_Data().ToArray();
                                flg = false;
                                cmb_SLG_halign.SelectedIndex = 3;
                                break;
                            }
                        }
                        if (!flg) break;
                    }
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN);

                if (hds_data.Count > 0)
                {
                    txt_HDP_ValignData.Lines = hds_data.Get_HEADS_Data().ToArray();
                    cmb_HDP_Valign.SelectedIndex = 2;
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._400_OFFSET);

                #region Read Cross Section

                CS_Data_Collection cs_col = new CS_Data_Collection();

                ChainageCollection chn_col = new ChainageCollection();
                ChainageDetails chn_dtls = new ChainageDetails();

                if (hds_data.Count > 0)
                {

                    string kStr = "";
                    MyList ml = null;
                    string ref_str = "";
                    double HO1, HO2, VO1, VO2;

                    HO1 = 0.0;
                    HO2 = 0.0;
                    VO1 = 0.0;
                    VO2 = 0.0;
                    foreach (var item in hds_data)
                    {
                        for (int i = 0; i < item.Data.Count; i++)
                        {
                            kStr = item.Data[i].ToString();
                            kStr = kStr.Replace(",", " ").Replace("=", " ");
                            kStr = MyList.RemoveAllSpaces(kStr);

                            ml = new MyList(kStr, ' ');
                            if (ml.StringList[0].StartsWith("401"))
                            {
                                ref_str = ml[4].ToString();
                            }
                            else if (ml.StringList[0].StartsWith("402"))
                            {
                                chn_dtls = new ChainageDetails();
                                chn_dtls.Format = ChainageDetails.eFormat._402;
                                chn_dtls.RefString = ref_str;
                                chn_dtls.Model = ml[2].ToString();
                                chn_dtls.String = ml[4].ToString();
                                chn_dtls.CH1 = ml[6].Value;
                                chn_dtls.CH2 = ml[8].Value;
                                chn_dtls.HO1 = ml[10].Value;
                                chn_dtls.HO2 = ml[12].Value;
                                chn_dtls.VO1 = ml[14].Value;
                                chn_dtls.VO2 = ml[16].Value;

                                chn_col.Add(chn_dtls);
                            }
                            else if (ml.StringList[0].StartsWith("403"))
                            {
                                chn_dtls = new ChainageDetails();
                                chn_dtls.Format = ChainageDetails.eFormat._403;
                                chn_dtls.RefString = ref_str;
                                chn_dtls.Model = ml[2].ToString();
                                chn_dtls.String = ml[4].ToString();
                                chn_dtls.CH1 = ml[6].Value;
                                chn_dtls.CH2 = ml[8].Value;
                                chn_dtls.HO1 = ml[10].Value;
                                chn_dtls.HO2 = ml[12].Value;
                                chn_dtls.VO1 = ml[14].Value;
                                chn_dtls.VO2 = ml[16].Value;

                                chn_col.Add(chn_dtls);
                            }
                        }
                    }

                }
                Start_Chainage = chn_col.GetStartChainage();
                End_Chainage = chn_col.GetEndChainage();

                uC_Canal_CrossSection1.Start_Chainage = Start_Chainage;
                uC_Canal_CrossSection1.End_Chainage = End_Chainage;

                //cs_col.Start_Chainage = Start_Chainage;
                //cs_col.End_Chainage = End_Chainage;
                //cs_col.TypeName = "TCS07";
                //foreach (var item in chn_col)
                //{

                //    CS_Data cs = new CS_Data();
                //    cs.RefStringName = item.RefString;
                //    cs.StringName = item.String;
                //    cs.HO = item.HO1;
                //    cs.VO = item.VO1;
                //    cs_col.Add_Data(cs.ToString());
                //}
                foreach (var item in chn_col)
                {
                    dgv_offset_service.Rows.Add(
                        item.RefString,
                        item.String,
                        item.CH1.ToString("f3"),
                        item.CH2.ToString("f3"),
                        item.HO1.ToString("f3"),
                        item.HO2.ToString("f3"),
                        item.VO1.ToString("f3"),
                        item.VO2.ToString("f3"));
                }

                #endregion Read Cross Section

                Project.All_Cross_Section_Types.Add(cs_col);

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._500_XSECTIONS);
                //pavement_HDP.HEADS_Data = new List<string>();
                if (hds_data.Count > 1)
                {
                    foreach (var item in hds_data)
                    {
                        _500_XSECTIONS pv = (_500_XSECTIONS)item.Get_Object_Data();
                        pv.Set_Option_Title();

                        if (pv.Is_PaveLayers)
                        {
                            //pavement_HDP.HEADS_Data = pv.Get_HEADS_Data();
                            break;
                        }
                    }
                }


                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._600_INTERFACE);

                if (hds_data.Count > 0)
                {
                    interface_HDP.Data = hds_data[0].Get_HEADS_Data();
                    //rtb_HDP_interface.Lines = hds_data[0].Get_HEADS_Data().ToArray();
                    //interface_HDP.Data = new List<string>(rtb_HDP_interface.Lines);
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._700_Volume);
                if (hds_data.Count > 0)
                    Volume_HDP_Data.Lines = hds_data.Get_HEADS_Data().ToArray();

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_ProHalign);
                if (hds_data.Count > 0)
                    halign_Diagram_HDP.Load_ProHALIGN_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._300_ProValign);
                if (hds_data.Count > 0)
                    valign_Diagram_HDP.Load_ProVALIGN_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1100_Plan);
                if (hds_data.Count > 0)
                    plan_Drawing_HDP.Load_Plan_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1200_Profile);
                if (hds_data.Count > 0)
                    profile_Drawing_HDP.Load_Profile_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1300_Section);
                if (hds_data.Count > 0)
                    cross_Section_Drawing_HDP.Load_Section_Data(hds_data[0].Get_HEADS_Data());
            }
            #endregion Irrigation Canal

            #region Irrigation Dyke
            else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
            {
                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);

                if (hds_data.Count > 0)
                    txt_HDP_HalignData.Lines = hds_data.Get_HEADS_Data().ToArray();



                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_ProHalign);

                if (hds_data.Count > 0)
                {
                    bool flg = true;
                    foreach (var item in hds_data)
                    {
                        foreach (var item1 in item.Data)
                        {
                            if (item1.ToString().StartsWith("212"))
                            {
                                txt_HDP_HalignData.Lines = item.Get_HEADS_Data().ToArray();
                                flg = false;
                                cmb_SLG_halign.SelectedIndex = 3;
                                break;
                            }
                        }
                        if (!flg) break;
                    }
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN);

                if (hds_data.Count > 0)
                {
                    txt_HDP_ValignData.Lines = hds_data.Get_HEADS_Data().ToArray();
                    cmb_HDP_Valign.SelectedIndex = 2;
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._400_OFFSET);

                #region Read Cross Section

                CS_Data_Collection cs_col = new CS_Data_Collection();

                ChainageCollection chn_col = new ChainageCollection();
                ChainageDetails chn_dtls = new ChainageDetails();

                if (hds_data.Count > 0)
                {

                    string kStr = "";
                    MyList ml = null;
                    string ref_str = "";
                    double HO1, HO2, VO1, VO2;

                    HO1 = 0.0;
                    HO2 = 0.0;
                    VO1 = 0.0;
                    VO2 = 0.0;
                    foreach (var item in hds_data)
                    {
                        for (int i = 0; i < item.Data.Count; i++)
                        {
                            kStr = item.Data[i].ToString();
                            kStr = kStr.Replace(",", " ").Replace("=", " ");
                            kStr = MyList.RemoveAllSpaces(kStr);

                            ml = new MyList(kStr, ' ');
                            if (ml.StringList[0].StartsWith("401"))
                            {
                                ref_str = ml[4].ToString();
                            }
                            else if (ml.StringList[0].StartsWith("402"))
                            {
                                chn_dtls = new ChainageDetails();
                                chn_dtls.Format = ChainageDetails.eFormat._402;
                                chn_dtls.RefString = ref_str;
                                chn_dtls.Model = ml[2].ToString();
                                chn_dtls.String = ml[4].ToString();
                                chn_dtls.CH1 = ml[6].Value;
                                chn_dtls.CH2 = ml[8].Value;
                                chn_dtls.HO1 = ml[10].Value;
                                chn_dtls.HO2 = ml[12].Value;
                                chn_dtls.VO1 = ml[14].Value;
                                chn_dtls.VO2 = ml[16].Value;

                                chn_col.Add(chn_dtls);
                            }
                            else if (ml.StringList[0].StartsWith("403"))
                            {
                                chn_dtls = new ChainageDetails();
                                chn_dtls.Format = ChainageDetails.eFormat._403;
                                chn_dtls.RefString = ref_str;
                                chn_dtls.Model = ml[2].ToString();
                                chn_dtls.String = ml[4].ToString();
                                chn_dtls.CH1 = ml[6].Value;
                                chn_dtls.CH2 = ml[8].Value;
                                chn_dtls.HO1 = ml[10].Value;
                                chn_dtls.HO2 = ml[12].Value;
                                chn_dtls.VO1 = ml[14].Value;
                                chn_dtls.VO2 = ml[16].Value;

                                chn_col.Add(chn_dtls);
                            }
                        }
                    }

                }
                Start_Chainage = chn_col.GetStartChainage();
                End_Chainage = chn_col.GetEndChainage();

                uC_Dyke_CrossSection1.Start_Chainage = Start_Chainage;
                uC_Dyke_CrossSection1.End_Chainage = End_Chainage;

                //cs_col.Start_Chainage = Start_Chainage;
                //cs_col.End_Chainage = End_Chainage;
                //cs_col.TypeName = "TCS07";
                //foreach (var item in chn_col)
                //{

                //    CS_Data cs = new CS_Data();
                //    cs.RefStringName = item.RefString;
                //    cs.StringName = item.String;
                //    cs.HO = item.HO1;
                //    cs.VO = item.VO1;
                //    cs_col.Add_Data(cs.ToString());
                //}
                foreach (var item in chn_col)
                {
                    dgv_offset_service.Rows.Add(
                        item.RefString,
                        item.String,
                        item.CH1.ToString("f3"),
                        item.CH2.ToString("f3"),
                        item.HO1.ToString("f3"),
                        item.HO2.ToString("f3"),
                        item.VO1.ToString("f3"),
                        item.VO2.ToString("f3"));
                }

                #endregion Read Cross Section

                Project.All_Cross_Section_Types.Add(cs_col);

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._500_XSECTIONS);
                //pavement_HDP.HEADS_Data = new List<string>();
                if (hds_data.Count > 1)
                {
                    foreach (var item in hds_data)
                    {
                        _500_XSECTIONS pv = (_500_XSECTIONS)item.Get_Object_Data();
                        pv.Set_Option_Title();

                        if (pv.Is_PaveLayers)
                        {
                            //pavement_HDP.HEADS_Data = pv.Get_HEADS_Data();
                            break;
                        }
                    }
                }


                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._600_INTERFACE);

                if (hds_data.Count > 0)
                {
                    interface_HDP.Data = hds_data[0].Get_HEADS_Data();
                    //rtb_HDP_interface.Lines = hds_data[0].Get_HEADS_Data().ToArray();
                    //interface_HDP.Data = new List<string>(rtb_HDP_interface.Lines);
                }

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._700_Volume);
                if (hds_data.Count > 0)
                    Volume_HDP_Data.Lines = hds_data.Get_HEADS_Data().ToArray();

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._200_ProHalign);
                if (hds_data.Count > 0)
                    halign_Diagram_HDP.Load_ProHALIGN_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._300_ProValign);
                if (hds_data.Count > 0)
                    valign_Diagram_HDP.Load_ProVALIGN_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1100_Plan);
                if (hds_data.Count > 0)
                    plan_Drawing_HDP.Load_Plan_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1200_Profile);
                if (hds_data.Count > 0)
                    profile_Drawing_HDP.Load_Profile_Data(hds_data[0].Get_HEADS_Data());

                hds_data = hpd.Find_HEADS_Data(eHEADS_OPTION._1300_Section);
                if (hds_data.Count > 0)
                    cross_Section_Drawing_HDP.Load_Section_Data(hds_data[0].Get_HEADS_Data());
            }
            #endregion Irrigation Dyke

            #region Mining Project
            else if (ProjectType == eTypeOfProject.Mining_Excavation)
            {

            }
            #endregion Mining Project

            else if (ProjectType == eTypeOfProject.Water_Distribution)
            {
                Read_Pipe_Network();
            }
            Load_Color_from_Drawing();
        }

        #endregion Site Leveling and Grading

        #region Land Record

        public void Load_Project_Tutoial_Land_Record()
        {
            string src = "";

            string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Land Record Management");

            txt_Project_Name.Text = "Tutorial Land Record Management";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "HLRM.mdb");

            uC_LandRecord1.Entry_Database = Survey_File;
            uC_LandRecord1.Retrive_Database = Survey_File;

            //cmb_SLG_halign.SelectedIndex = 2;
            //cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            Land_Drawing_Folder = Path.Combine(Working_Folder, "Land Plan Drawings");

            Load_Land_Drawing();
            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            //Load_Project_File(Project_File);
        }

        #endregion Land Record

        public void Load_Project_Tutoial_Irrigation_Canal()
        {
            string src = "";

            string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Canal Project Data");

            txt_Project_Name.Text = "Tutorial Canal Design";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());
            //design_Section_Schedule1.cmb_cstype.SelectedIndex = 0;
            //design_Section_Schedule1.Start_Chainage = 0;
            //design_Section_Schedule1.End_Chainage = 3000.0;


            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);

            List<IHEADS> hds_data = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);
            IHEADS sa = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN)[0];


            Project.HalignData = sa.Get_HEADS_Data();

            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;

            txt_HDP_HalignData.Lines = Project.HalignData.ToArray();

            sa = Project.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN)[0];

            Project.ValignData = sa.Get_HEADS_Data();
            txt_HDP_ValignData.Lines = Project.ValignData.ToArray();

            Load_Project_File(Project_File);

        }

        public void Load_Project_Tutoial_Irrigation_RiverDesiltation()
        {
            string src = "";

            string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\River Desiltation Project Data");

            txt_Project_Name.Text = "Tutorial River Desiltation";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                    MessageBox.Show("Example Data Loaded successfully.");
                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());
            //design_Section_Schedule1.cmb_cstype.SelectedIndex = 0;
            //design_Section_Schedule1.Start_Chainage = 0;
            //design_Section_Schedule1.End_Chainage = 3000.0;


            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);

            List<IHEADS> hds_data = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);
            IHEADS sa = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN)[0];


            Project.HalignData = sa.Get_HEADS_Data();

            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;

            txt_HDP_HalignData.Lines = Project.HalignData.ToArray();

            sa = Project.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN)[0];

            Project.ValignData = sa.Get_HEADS_Data();
            txt_HDP_ValignData.Lines = Project.ValignData.ToArray();

            cmb_draw.SelectedIndex = 2;
            Load_Project_File(Project_File);

        }

        public void Load_Project_Tutoial_Irrigation_Dyke()
        {

            string src = "";

            //string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Dyke Project Data");


            //Uniform_Section_Tutorial = true;


            txt_Project_Name.Text = "Tutorial Earth Dam_Dyke Design";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());
          


            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);

            List<IHEADS> hds_data = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN);
            IHEADS sa = Project.Find_HEADS_Data(eHEADS_OPTION._200_HALIGN)[0];


            Project.HalignData = sa.Get_HEADS_Data();

            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


            txt_HDP_HalignData.Lines = Project.HalignData.ToArray();



            sa = Project.Find_HEADS_Data(eHEADS_OPTION._300_VALIGN)[0];


            Project.ValignData = sa.Get_HEADS_Data();
            txt_HDP_ValignData.Lines = Project.ValignData.ToArray();


            //roadVolume1.Load_Project_Example(Project_Example);
            //plan_Drawing_HDP.Load_Project_Example(Project_Example);
            //profile_Drawing_HDP.Load_Project_Example(Project_Example);
            //cross_Section_Drawing_HDP.Load_Project_Example(Project_Example);


            Load_Project_File(Project_File);
            MessageBox.Show("Example Data Loaded successfully.");

        }
        public void Load_Project_Tutoial_Mining_Open_Cast_Excavation()
        {
            string src = "";

            //string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Discharge Quantity");


            //Uniform_Section_Tutorial = true;


            txt_Project_Name.Text = "Tutorial Open Cast Excavation";

            txt_Contour_pri_inc.Text = "0.2";

            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());



            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);






            //roadVolume1.Load_Project_Example(Project_Example);
            //plan_Drawing_HDP.Load_Project_Example(Project_Example);
            //profile_Drawing_HDP.Load_Project_Example(Project_Example);
            //cross_Section_Drawing_HDP.Load_Project_Example(Project_Example);


            Load_Project_File(Project_File);
            MessageBox.Show("Example Data Loaded successfully.");

        }

        public void Load_Project_Tutoial_Stock_Quantity()
        {

            string src = "";

            //string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Stock Quantity");


            //Uniform_Section_Tutorial = true;

            txt_Contour_pri_inc.Text = "0.2";

            txt_Project_Name.Text = "Tutorial Stockpile Quantity";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());



            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);






            //roadVolume1.Load_Project_Example(Project_Example);
            //plan_Drawing_HDP.Load_Project_Example(Project_Example);
            //profile_Drawing_HDP.Load_Project_Example(Project_Example);
            //cross_Section_Drawing_HDP.Load_Project_Example(Project_Example);


            Load_Project_File(Project_File);
            MessageBox.Show("Example Data Loaded successfully.");

        }

        public void Load_Project_Tutoial_Discharge_Quantity()
        {

            string src = "";

            //string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            //src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Stock Quantity");
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Discharge Quantity");
            

            //Uniform_Section_Tutorial = true;


            txt_Project_Name.Text = "Tutorial Discharge Quantity";

            txt_Contour_pri_inc.Text = "0.2";

            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());



            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);






            //roadVolume1.Load_Project_Example(Project_Example);
            //plan_Drawing_HDP.Load_Project_Example(Project_Example);
            //profile_Drawing_HDP.Load_Project_Example(Project_Example);
            //cross_Section_Drawing_HDP.Load_Project_Example(Project_Example);


            Load_Project_File(Project_File);
            MessageBox.Show("Example Data Loaded successfully.");

        }
        public void Load_Project_Tutoial_Water_Distribution()
        {

            string src = "";

            //string dest = "";

            rbtn_total_station.Checked = true;
            src = Path.GetDirectoryName(Application.StartupPath);
            src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Water Distribution Network");


            //Uniform_Section_Tutorial = true;
         

            txt_Project_Name.Text = "Tutorial Water Distribution Network";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                }
                catch (Exception exx) { }
            }

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");


            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());



            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);
            txt_Contour_pri_inc.Text = "0.1";

            Load_Project_File(Project_File);

            MessageBox.Show("Example Data Loaded successfully.");

        }

        public void Load_Project_Tutoial_Stream_Hydrology()
        {

            string src = "";

            //string dest = "";

            rbtn_total_station.Checked = true;
            //src = Path.GetDirectoryName(Application.StartupPath);
            src = Application.StartupPath;
            //src = Path.Combine(src, @"Technical Data\HEADS Site Tutorial Data\Stream Hydrology");
            src = Path.Combine(src, @"DESIGN\Stream Hydrology\Project Data");
            IsTutorial = true;

            //MessageBox.Show(src);
            //Uniform_Section_Tutorial = true;


            txt_Project_Name.Text = "Tutorial Stream Hydrology";


            //Working_Folder = Path.Combine(iProj.Working_Folder, txt_Project_Name.Text);
            Working_Folder = Set_Working_Folder();

            Directory.CreateDirectory(Working_Folder);
            if (Directory.Exists(src))
            {
                try
                {
                    DirectoryCopy(src, Working_Folder, true);

                }
                catch (Exception exx) { }
            }

            //MessageBox.Show(Working_Folder);

            Create_Project();

            Survey_File = Path.Combine(Working_Folder, "SURVEY.txt");

            //MessageBox.Show(Survey_File);

            //chk_utm_conversion.Checked = true;
            //utM_Conversion1.Initialize_UTM_Data_Conversion(Survey_File);
            //utM_Conversion1.UTM_Data_Conversion_Load(btn_survey_browse, new EventArgs());



            cmb_SLG_halign.SelectedIndex = 2;
            cmb_HDP_Valign.SelectedIndex = 2;


            txt_survey_data.Text = Survey_File;

            //Project_File = Path.Combine(Working_Folder, "Project Data File.txt");
            txt_Working_Folder.Text = Project_File;


            Project = new HighwayRoadProject(Project_File);
            txt_Contour_pri_inc.Text = "1.0";

            Load_Project_File(Project_File);

            MessageBox.Show("Example Data Loaded successfully.");

        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            //destDirName = Path.Combine(destDirName, "HEADS Pro Tutorials");
            //destDirName = Path.Combine(destDirName, Path.GetFileName(sourceDirName));
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        #endregion Open Tutorials

        #region Ground Modeling

        public void WriteModel(string file_name)
        {
            ColumnIndexes colIndex = new ColumnIndexes();
            colIndex.Point_Index = 0;
            colIndex.X_Index = 1;
            colIndex.Y_Index = 2;
            colIndex.Z_Index = 3;
            colIndex.Layer_Index = 4;

            dplc = new DrawPolyLineCollection(file_name, colIndex);


            DrawPolyLineCollection dpl_col = dplc;


            List<string> lst_layer = new List<string>(dplc.Labels.ToArray());

            cmb_layer.Items.Clear();
            lst_layer.Sort();
            if (lst_layer.Count > 0)
                cmb_layer.Items.Add("Select All");
            else
                return;
            //lst.Insert(0, "Select All");
            cmb_layer.Items.AddRange(lst_layer.ToArray());


            cmb_layer.SelectedIndex = 0;

            if (ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
            {
                if(cmb_draw.SelectedIndex == -1)
                cmb_draw.SelectedIndex = 0;
            }
            else
                cmb_draw.SelectedIndex = 0;

            Settings setting = new Settings();


            string kStr = "SELECT ALL";
            if (kStr == "SELECT ALL")
            {
                for (int i = 0; i < lst_layer.Count; i++)
                {
                    kStr = lst_layer[i];

                    Setting st = null;

                    if (kStr == "P0")
                    {
                        st = new Setting(kStr, "POINT_ELEVATION", kStr);
                    }
                    else if (kStr == "OGL" || kStr == "GROUND")
                    {
                        st = new Setting(kStr, "POINT", kStr);
                    }
                    else if (kStr == "TEXT")
                    {
                        st = new Setting(kStr, "TEXT", kStr);
                    }
                    else
                    {
                        st = new Setting(kStr, "POLYLINE", kStr);
                    }

                    st.IsDraw = true;
                    st.Layer_Color = Color.White;
                    setting.Add(st);
                }
            }





            string last_str = "";

            CLabtype lab;
            CModType mod;
            CStgType stg;
            CPTStype pts;
            CTXTtype txt;
            ClSTtype lst;

            List<CLabtype> listlabs = new List<CLabtype>();
            List<ClSTtype> listLst = new List<ClSTtype>();

            //string model_name = "GROUND";
            string model_name = Path.GetFileNameWithoutExtension(Survey_File).ToUpper();


            if (model_name.Length > 8)
                model_name = model_name.Substring(0, 8);
            try
            {

                for (int i = 0; i < dpl_col.Count; i++)
                {
                    dpl_col[i].LayerName = dpl_col[i].Label;



                    if ((dpl_col[i].PointNo == 0) ||
                        ((dpl_col[i].X <= -999 || dpl_col[i].X == 0.0) &&
                        (dpl_col[i].Y <= -999 || dpl_col[i].Y == 0.0)))
                    {
                        lab = new CLabtype();
                        lab.attr = CLabtype.Type.EndCode;
                        listlabs.Add(lab);
                    }
                    else
                    {
                        if (dpl_col[i].LayerName != last_str)
                        {
                            last_str = dpl_col[i].LayerName;

                            mod = new CModType();
                            mod.Name = model_name;

                            lab = new CLabtype();
                            lab.attr = CLabtype.Type.Model;
                            lab.Tag = mod;
                            listlabs.Add(lab);

                            stg = new CStgType();
                            stg.label = last_str;

                            lab = new CLabtype();
                            lab.attr = CLabtype.Type.String;
                            lab.Tag = stg;
                            listlabs.Add(lab);


                            lst = new ClSTtype();
                            lst.strMod1 = model_name;
                            lst.strStg = last_str;
                            listLst.Add(lst);
                        }

                        lab = new CLabtype();
                        lab.attr = CLabtype.Type.Point;

                        pts = new CPTStype();

                        pts.mx = dpl_col[i].X;
                        pts.my = dpl_col[i].Y;
                        pts.mz = dpl_col[i].Z;
                        pts.mc = 123.00;
                        lab.Tag = pts;
                        listlabs.Add(lab);
                    }
                }

                file_name = Path.Combine(Path.GetDirectoryName(file_name), "Text_Data.txt");
                lab = new CLabtype();
                lab.attr = CLabtype.Type.EndCode;
                listlabs.Add(lab);
                if (File.Exists(file_name))
                {
                    dpl_col = null;
                    dpl_col = new DrawPolyLineCollection(file_name, colIndex);
                    for (int i = 0; i < dpl_col.Count; i++)
                    {
                        dpl_col[i].LayerName = dpl_col[i].Label;

                        if ((dpl_col[i].PointNo == 0) ||
                            ((dpl_col[i].X <= -999 || dpl_col[i].X == 0.0) &&
                            (dpl_col[i].Y <= -999 || dpl_col[i].Y == 0.0)))
                        {
                            lab = new CLabtype();
                            lab.attr = CLabtype.Type.EndCode;
                            listlabs.Add(lab);
                        }
                        else
                        {
                            if (dpl_col[i].LayerName != last_str)
                            {
                                last_str = dpl_col[i].LayerName;

                                mod = new CModType();
                                mod.Name = model_name;

                                lab = new CLabtype();
                                lab.attr = CLabtype.Type.Model;
                                lab.Tag = mod;
                                listlabs.Add(lab);

                                stg = new CStgType();
                                stg.label = last_str;

                                lab = new CLabtype();
                                lab.attr = CLabtype.Type.String;
                                lab.Tag = stg;
                                listlabs.Add(lab);


                                lst = new ClSTtype();
                                lst.strMod1 = model_name;
                                lst.strStg = last_str;
                                listLst.Add(lst);
                            }

                            lab = new CLabtype();
                            lab.attr = CLabtype.Type.Text;

                            CTXTtype ctxt = new CTXTtype();
                            ctxt.tx = dpl_col[i].X;
                            ctxt.ty = dpl_col[i].Y;
                            ctxt.tz = dpl_col[i].Z;
                            ctxt.tr = dpl_col[i].TextRotation;
                            ctxt.ts = dpl_col[i].TextHeight;
                            ctxt.tg = dpl_col[i].TextString;

                            lab.Tag = ctxt;
                            listlabs.Add(lab);
                        }
                    }
                }

                lab = new CLabtype();
                lab.attr = CLabtype.Type.EndCode;
                listlabs.Add(lab);

                //string pth = Path.Combine(Working_Folder, "MODEL.LST");
                string pth = Path.Combine(Path.GetDirectoryName(file_name), "MODEL.LST");


                if (File.Exists(pth)) File.Delete(pth);


                kStr = "";
                for (int i = 0; i < listLst.Count; i++)
                {
                    kStr = String.Format("{0,-25} :{1,-25}", listLst[i].strMod1,
                        listLst[i].strStg);
                    if (lb_GMT_ModelAndStringName.Items.Contains(kStr) == false)
                        lb_GMT_ModelAndStringName.Items.Add(kStr);

                    lb_GMT_ModelAndStringName.SelectedItem = kStr;
                }

                //MessageBox.Show(listlabs.Count.ToString());
                //MessageBox.Show(listLst.Count.ToString());

                WriteModelLstFile(pth, listLst);
                pth = Path.Combine(Path.GetDirectoryName(file_name), "MODEL.FIL");

                //MessageBox.Show(pth);

                if (File.Exists(pth)) File.Delete(pth);
                WriteModelFilFile(pth, listlabs);

                MessageBox.Show("MODEL.LST and MODEL.FIL files created.", "HEADS");
            }
            catch (Exception ex) { }
            finally
            {

            }
        }


        private void Draw_from_Model()
        {

            //if (!Directory.Exists(Folder_Current))
            Folder_Current = Working_Folder;
            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");
            //set the test data path    

            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);


            dplc = new DrawPolyLineCollection();

            DrawPolyLine dpl = new DrawPolyLine();


            List<string> list_text = new List<string>();
            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");

            gPoint ptStart = new gPoint();
            gPoint ptEnd = new gPoint();
            gPoint ptLast = new gPoint();
            gPoint ptFirst3dFac = new gPoint();
            string strSeleModel = txt_Contour_pri_model.Text;

            CTXTtype txt = new CTXTtype();
            vdLine uline = new vdLine();


            string _model = "";
            string _string = "";

            string strModelName = "";
            int NumPts = 0;
            //string str1 = "";
            string stgLabel = "";
            CPTStype pts = new CPTStype();
            List<gPoint> pFArray = new List<gPoint>();

            CCfgtype cfg = new CCfgtype();


            double dTextHeight = (VDoc.ActiveTextStyle.Height > 0) ? VDoc.ActiveTextStyle.Height : 1.0;

            int p_count = 0;

            string strFilePath = m_strpathfilfile;


            if (File.Exists(strFilePath))
            {

                frm_ProgressBar.ON("Read Model files......");
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    try
                    {

                        CLabtype labtype = CLabtype.FromStream(br);

                        #region Model
                        if (labtype.attr == CLabtype.Type.Model) // Model Name
                        {
                            //NumPts = 0;
                            strModelName = ((CModType)labtype.Tag).Name;
                            if (strModelName != "")
                            {
                                NumPts = 0;

                                dpl = new DrawPolyLine();

                                dpl.Label = _string;
                                dpl.LayerName = _string;
                                dpl.PointNo = NumPts;
                                //dpl.Chainge = txt.Point;
                                dpl.X = 0;
                                dpl.Y = 0;
                                dpl.Z = 0;
                                dplc.Add(dpl);
                            }
                        }
                        #endregion Model

                        #region String
                        else if (labtype.attr == CLabtype.Type.String)// String Label
                        {
                            stgLabel = ((CStgType)labtype.Tag).label;

                            _model = strModelName;
                            _string = stgLabel;
                        }
                        #endregion String

                        #region Point
                        else if (labtype.attr == CLabtype.Type.Point)
                        {

                            if (_model != "" && _string != "")
                            {
                                pts = (CPTStype)labtype.Tag;

                                dpl = new DrawPolyLine();

                                dpl.Label = _string;
                                dpl.LayerName = _string;
                                dpl.PointNo = ++NumPts;
                                dpl.Chainge = pts.mc;
                                dpl.X = pts.mx;
                                dpl.Y = pts.my;
                                dpl.Z = pts.mz;

                                if (pts.mx != 0.0 && pts.my != 0.0 && pts.mz != 0.0)
                                    dplc.Add(dpl);
                            }
                            //dpl.LayerName = Path.
                        }
                        #endregion Point

                        #region Text

                        else if (labtype.attr == CLabtype.Type.Text)
                        {
                            if (_model != "" && _string != "")
                            {
                                txt = (CTXTtype)labtype.Tag;

                                dpl = new DrawPolyLine();

                                dpl.Label = _string;
                                dpl.LayerName = _string;
                                dpl.PointNo = ++NumPts;
                                //dpl.Chainge = txt.Point;
                                dpl.X = txt.tx;
                                dpl.Y = txt.ty;
                                dpl.Z = txt.tz;
                                dpl.TextRotation = txt.tr;
                                dpl.TextString = txt.tg;
                                dpl.TextHeight = txt.ts;

                                dplc.Add(dpl);
                            }
                        }
                        #endregion Text

                        #region EndCode

                        else if (labtype.attr == CLabtype.Type.EndCode)
                        {
                            if (_model != "" && _string != "" && _string != "-999")
                            {
                                NumPts = 0;

                                dpl = new DrawPolyLine();

                                dpl.Label = _string;
                                dpl.LayerName = _string;
                                dpl.PointNo = NumPts;
                                //dpl.Chainge = txt.Point;
                                dpl.X = 0;
                                dpl.Y = 0;
                                dpl.Z = 0;
                                dplc.Add(dpl);
                            }
                        }
                        #endregion EndCode


                        frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                    }
                    catch (Exception ex) { }
                }

                frm_ProgressBar.SetValue(br.BaseStream.Length, br.BaseStream.Length);
                frm_ProgressBar.OFF();
            }
            br.Close();
        }

        void WriteModelLstFile(string strFilePath, List<ClSTtype> listData)
        {

            //MessageBox.Show("WriteModelLstFile_START");
            BinaryWriter bw = new BinaryWriter(new FileStream(strFilePath, FileMode.Append), Encoding.Default);
            foreach (ClSTtype lstdata in listData)
            {
                lstdata.ToStream(bw);
            }
            bw.Close();
            //MessageBox.Show("WriteModelLstFile_END");
        }
        void WriteModelFilFile(string strPath, List<CLabtype> listfil)
        {
            //MessageBox.Show("WriteModelFilFile_START");
            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Append), Encoding.Default);
            for (int i = 0; i < listfil.Count; i++)
            {
                CLabtype lab = listfil[i];
                lab.ToStream(bw);
            }
            bw.Close();
            //MessageBox.Show("WriteModelFilFile_END");
        }



        public bool Check_Autolevel_Model_String()
        {
            string fname = Path.Combine(Working_Folder, "HALIGN.FIL");

            if (!File.Exists(fname))
            {
                return false;
            }
            List<string> list = new List<string>(File.ReadAllLines(fname));
            MyList mlist;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(list[i]), ' ');
                if (mlist.StringList[0] == "DESIGN" && mlist.StringList[1] == "M001") return true;
            }
            return false;
        }
        private void btn_DGM_create_model_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Survey_File))
                Survey_File = txt_survey_data.Text;
            if (!File.Exists(Survey_File))
            {
                MessageBox.Show("Survey file not selected.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (rbtn_total_station.Checked)
                Folder_Delete(Working_Folder, true);

            Button btn = sender as Button;
            string inp = "";

            if (btn.Name == btn_DGM_create_model.Name)
            {
                if (rbtn_total_station.Checked)
                {
                    #region Total Station Data

                    //Load_Survey_Mapping();
                    if (false)
                    {
                        #region Create DGM

                        Set_HEADS_Environment_Variable(Survey_File);
                        RunExe("dgm2.exe", true);

                        inp = Path.Combine(Working_Folder, Path.GetFileNameWithoutExtension(Survey_File) + ".DGM");
                        if (!File.Exists(inp))
                            inp = Path.Combine(Working_Folder, Path.GetFileNameWithoutExtension(Survey_File) + ".GEN");

                        if (File.Exists(inp))
                        {
                            Set_HEADS_Environment_Variable(inp);
                            RunExe("modgen.exe", true);
                        }

                        #endregion Create DGM
                    }
                    //MessageBox.Show(Folder_Current);

                    //inp = Path.Combine(Folder_Process_Survey_Data, Path.GetFileName(Survey_File));
                    Folder_Current = Working_Folder;
                    inp = Survey_File;
                    WriteModel(inp);



                    try
                    {
                        Load_Ground_Model();
                    }
                    catch (Exception exx) { }



                    #endregion Total Station Data
                }
                else if (rbtn_auto_level.Checked)
                {
                    #region Auto Level Data


                    //if (Check_Autolevel_Model_String())
                    if (true)
                    {
                        //Folder_Current = Folder_Process_Survey_Data;
                        Folder_Delete(Folder_Process_Survey_Data, true);

                        #region Create DGM
                        string pth = Path.Combine(Working_Folder, Path.GetFileName(Survey_File));

                        //Set_HEADS_Environment_Variable(pth);
                        //RunExe("dgm4.exe", true);

                        //inp = Path.Combine(Working_Folder, "CROSSECT.DGM");

                        //if (File.Exists(inp))
                        //{
                        //    Set_HEADS_Environment_Variable(inp);
                        //    RunExe("crossgen.exe", true);
                        //}

                        #endregion Create DGM

                        frm_Autolevel_Data fad = new frm_Autolevel_Data();
                        fad.IsDemo = IsDemo;
                        fad.Survey_File = Survey_File;
                        fad.Working_Folder = Working_Folder;
                        if (fad.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                        pth = Path.Combine(Working_Folder, "MODEL.LST");






                        string kStr = "";
                        CHEADSDoc.ReadModelsFromFile(Working_Folder);
                        for (int i = 0; i < CHEADSDoc.LstModel.Count; i++)
                        {
                            if (CHEADSDoc.LstModel[i].ModelName == "GROUND")
                            {
                                kStr = String.Format("{0,-15} :{1,-25}", CHEADSDoc.LstModel[i].ModelName,
                                    CHEADSDoc.LstModel[i].StringName);
                                if (lb_GMT_ModelAndStringName.Items.Contains(kStr) == false)
                                    lb_GMT_ModelAndStringName.Items.Add(kStr);

                                lb_GMT_ModelAndStringName.SelectedItem = kStr;
                            }
                        }

                        Draw_from_Model();



                        //Folder_Copy(Folder_Process_Survey_Data, Folder_HDP_Halign);
                        Settings setting = new Settings();


                        kStr = "SELECT ALL";
                        dplc.Labels.Remove("M001");
                        if (kStr == "SELECT ALL")
                        {
                            for (int i = 0; i < dplc.Labels.Count; i++)
                            {
                                kStr = dplc.Labels[i];
                                if (kStr == "-999") continue;
                                Setting st = null;

                                if (kStr == "P0")
                                {
                                    st = new Setting(kStr, "POINT_ELEVATION", kStr);
                                }
                                else if (kStr == "TEXT")
                                {
                                    st = new Setting(kStr, "TEXT", kStr);
                                }
                                else
                                {
                                    st = new Setting(kStr, "POLYLINE", kStr);
                                }

                                st.IsDraw = true;
                                st.Layer_Color = Color.White;
                                setting.Add(st);
                            }
                        }

                        cmb_layer.Items.Clear();
                        //cmb_layer.Items.Add("Select All");
                        List<string> lst = new List<string>(dplc.Labels.ToArray());
                        lst.Sort();
                        if (lst.Count > 0)
                            lst.Insert(0, "Select All");
                        //lst.Insert(0, "Select All");
                        cmb_layer.Items.AddRange(lst.ToArray());


                        cmb_layer.SelectedIndex = 0;
                        cmb_draw.SelectedIndex = 0;
                    }

                    #endregion Auto Level Data
                }
                else if (rbtn_survey_drawing.Checked)
                {
                    #region survey_drawing

                    //Load_Survey_Mapping();

                    //MessageBox.Show(Folder_Current);

                    //inp = Path.Combine(Folder_Process_Survey_Data, Path.GetFileName(Survey_File));
                    Folder_Current = Working_Folder;
                    inp = Path.Combine(Folder_Current, Path.GetFileNameWithoutExtension(Survey_File) + ".TXT");
                    Survey_File = inp;
                    DrawingToData(VDoc);
                    WriteData(inp);
                    WriteModel(inp);

                    txt_survey_data.Text = Survey_File;

                    Load_Ground_Model();



                    #endregion survey_drawing
                }
                else if (rbtn_bearing_line.Checked)
                {
                    #region Bearing Line Data


                    frm_BearingDataExtract frm = new frm_BearingDataExtract(this);


                    frm.file_name = Path.Combine(Working_Folder, "BL_DATA.TXT");
                    frm.ShowDialog();

                    Survey_File = frm.Survey_File;


                    Folder_Current = Working_Folder;
                    inp = Survey_File;
                    WriteModel(inp);

                    //cmb_HDP_halign.SelectedIndex = 3;



                    try
                    {
                        Load_Ground_Model();

                    }
                    catch (Exception exx) { }



                    #endregion Bearing Line Data

                }
                //Folder_Current = Folder_Process_Survey_Data;
                Folder_Copy(Working_Folder, Folder_Process_Survey_Data);

                dgv_all_data.Rows.Clear();
                Add_To_List();
            }
            else if (btn.Name == btn_DGM_open_survey_data.Name)
            {
                if (File.Exists(Survey_File))
                {
                    System.Diagnostics.Process.Start(Survey_File);
                }
                else
                {
                    MessageBox.Show(Survey_File + " File not found.", "HEADS", MessageBoxButtons.OK);
                }
            }
            else if (btn.Name == btn_DGM_open_gm_data.Name)
            {
                string crs_dgm = Path.Combine(Working_Folder, "CROSSECT.DGM");
                if (File.Exists(crs_dgm))
                {
                    System.Diagnostics.Process.Start(crs_dgm);
                }
                else
                {
                    MessageBox.Show(crs_dgm + " File not found.", "HEADS", MessageBoxButtons.OK);
                }
            }

        }

        public void WriteData(string file_name)
        {

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            string text_file = Path.Combine(Path.GetDirectoryName(file_name), "Text_Map_Data.txt");
            string text_data = Path.Combine(Path.GetDirectoryName(file_name), "TEXT.TXT");
            string point_data = Path.Combine(Path.GetDirectoryName(file_name), "POINTS.TXT");


            StreamWriter sw2 = new StreamWriter(new FileStream(text_file, FileMode.Create));
            StreamWriter sw_txt = new StreamWriter(new FileStream(text_data, FileMode.Create));
            StreamWriter sw_pnt = new StreamWriter(new FileStream(point_data, FileMode.Create));

            bool flag = false;


            string sp_char = ((Path.GetExtension(file_name).ToLower() == ".csv")) ? "," : " ";
            try
            {
                sw.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "Serial", sp_char,
                   "Easting",
                   "Northing",
                   "Elevation",
                   "Feature");
                //sw.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);
                sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "No:", sp_char,
                   "  (Metres)",
                   "  (Metres)",
                   "  (Metres)",
                   "Code");
                sw2.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5,15:f5}  {6,15:f5}  {7,15:f5} {8}",
                "Serial", sp_char,
                "Easting",
                "Northing",
                "Elevation",
                "Feature",
                "Text",
                "Text",
                "Text");
                //sw.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);

                sw2.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5,15:f5}  {6,15:f5}  {7,15:f5} {8}",
                   "No:", sp_char,
                   "(Metres)",
                   "(Metres)",
                   "(Metres)",
                   "Code",
                "Size",
                "Rotation",
                "");


                sw_pnt.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                "Serial", sp_char,
                "Easting",
                "Northing",
                "Elevation",
                "Feature");
                //sw.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);
                sw_pnt.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "No:", sp_char,
                   "(Metres)",
                   "(Metres)",
                   "(Metres)",
                   "Code");



                sw_txt.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5,15}  {6,15:F5}  {7}",
                "Serial", sp_char,
                "Easting",
                "Northing",
                "Elevation",
                "Text",
                "Text",
                "Text");

                sw_txt.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5,15:F5}  {6,15:F5}  {7}",
                "", sp_char,
                "(Metres)",
                "(Metres)",
                "(Metres)",
                "Size",
                "Rotation",
                "");
                //sw.WriteLine("No:{0}   (Mtres){0}  (Metres){0} (Metre){0}   Code", sp_char);
                //sw2.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);
                //sw2.WriteLine("No:{0}   (Mtres){0}  (Metres){0} (Metre){0}   Code", sp_char);


                DrawPolyLineCollection dpl_col = dplc;

                for (int i = 0; i < dpl_col.Count; i++)
                {
                    if (dpl_col[i].DrawFormat == DrawAs.Text)
                    {
                        if (dpl_col[i].PointNo == 0)
                        {
                            if (flag)
                            {
                                sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                        dpl_col[i].PointNo, sp_char,
                                        dpl_col[i].X,
                                        dpl_col[i].Y,
                                        dpl_col[i].Z,
                                        dpl_col[i].LayerName);
                                flag = false;
                            }
                            //,dpl_col[i].Label
                        }
                        else
                        {
                            flag = true;
                            sw2.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5,15:f5}   {6,15:f5} {7,15:f5} {8}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName,
                                    dpl_col[i].TextHeight,
                                    dpl_col[i].TextRotation,
                                    dpl_col[i].TextString);

                            sw_pnt.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName);
                            sw_txt.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5,15:f5}   {6,15:F5}  {7}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].TextHeight,
                                    dpl_col[i].TextRotation,
                                    dpl_col[i].TextString);

                            //sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                            //        dpl_col[i].PointNo, sp_char,
                            //        dpl_col[i].X,
                            //        dpl_col[i].Y,
                            //        dpl_col[i].Z,
                            //        "TEXT");
                        }
                    }
                    else
                    {
                        if (dpl_col[i].PointNo == 0)
                        {
                            if (flag)
                            {
                                sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                    //sw.WriteLine("{0}   {1}  {2:f3} {1} {3:f3}  {1} {4:f3}{1}  {5}",
                                        dpl_col[i].PointNo, sp_char,
                                        dpl_col[i].X,
                                        dpl_col[i].Y,
                                        dpl_col[i].Z,
                                        dpl_col[i].LayerName);

                                flag = false;
                            }
                        }
                        else
                        {
                            //sw.WriteLine("{0}{1}{2:f5}{1}{3:f5}{1}{4:f5}{1}  {5} ",
                            flag = true;
                            sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName);
                        }
                    }
                    //sw.WriteLine("{0}{1}{2:f4}{1}{3:f4}{1}{4:f4}{1}{5:f4}{1}{6:f4}{1}{7}{1}{8}{1}{9:f4}",
                    //    (i + 1), sp_char,
                    //    dpl_col[i].SegmentLength,
                    //    dpl_col[i].Chainge,
                    //    dpl_col[i].X,
                    //    dpl_col[i].Y,
                    //    dpl_col[i].Z,
                    //    dpl_col[i].LayerName,
                    //    dpl_col[i].Label,
                    //    txt_height.Text);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sw2.Flush();
                sw2.Close();
                sw_txt.Flush();
                sw_txt.Close();
                sw_pnt.Flush();
                sw_pnt.Close();
            }
        }

        private void Load_Ground_Model()
        {


            #region Read Model List
            CHEADSDoc.ReadModelsFromFile(Folder_Current);
            lb_GMT_ModelAndStringName.Items.Clear();
            for (int i = 0; i < CHEADSDoc.LstModel.Count; i++)
            {
                if (CHEADSDoc.LstModel[i].ModelName == "" || CHEADSDoc.LstModel[i].StringName == "") continue;

                string kStr = String.Format("{0,-15} :{1,-25}", CHEADSDoc.LstModel[i].ModelName,
                    CHEADSDoc.LstModel[i].StringName);
                if (lb_GMT_ModelAndStringName.Items.Contains(kStr) == false)
                {
                    //this.lb_GMT_ModelAndStringName.SetSelected(i, true);
                    lb_GMT_ModelAndStringName.Items.Add(kStr);
                }
                //this.lb_GMT_ModelAndStringName.SetSelected(i, true);
            }

            for (int iIndex = 0; iIndex < this.lb_GMT_ModelAndStringName.Items.Count; iIndex++)
            {
                this.lb_GMT_ModelAndStringName.SetSelected(iIndex, true);
            }
            #endregion Read Model List

            if (chk_utm_conversion.Checked)
            {
                //utM_Conversion1.TM_to_UTM_Conversion();

                frm_UTM_Data_Conversion ff = new frm_UTM_Data_Conversion(txt_survey_data.Text);
                //ff.Show_Default_Data = !Uniform_Section_Tutorial;
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;


                //utM_Conversion1.In

                //utM_Conversion1.TM1_XC = ff.TM1_XC;
                //utM_Conversion1.TM2_XC = ff.TM2_XC;
                //utM_Conversion1.TM1_YC = ff.TM1_YC;
                //utM_Conversion1.TM2_YC = ff.TM2_YC;

                //utM_Conversion1.TM1_XC = ff.TM1_XC;
                //utM_Conversion1.TM2_XC = ff.TM2_XC;
                //utM_Conversion1.TM1_YC = ff.TM1_YC;
                //utM_Conversion1.TM2_YC = ff.TM2_YC;



                //txt_utm_convert.Lines = ff.HEADS_Data.ToArray();

                string inp = Path.Combine(Folder_Current, "inp.txt");

                File.WriteAllLines(inp, ff.HEADS_Data.ToArray());

                SetEnvironment(inp);

                string mls = Path.Combine(Folder_Current, "MODEL.LST");
                //string mls1 = Path.Combine(Folder_Current, "MODEL.LST1");
                //File.Copy(mls, mls1, true);
                RunExe("modeledit.exe", true);
                //File.Copy(mls1, mls, true);
                Draw_from_Model();
                //File.Delete(mls1);
            }
        }
        private void btn_DGM_data_convert_Click(object sender, EventArgs e)
        {

            try
            {
                //if (chk_utm_conversion.Checked)
                //{
                //utM_Conversion1.TM_to_UTM_Conversion();

                string inp = Path.Combine(Folder_Current, "inp.txt");

                //File.WriteAllLines(inp, utM_Conversion1.HEADS_Data.ToArray());

                SetEnvironment(inp);

                string mls = Path.Combine(Folder_Current, "MODEL.LST");
                string mls1 = Path.Combine(Folder_Current, "MODEL.LST1");
                File.Copy(mls, mls1, true);
                RunExe("modeledit.exe", true);
                File.Copy(mls1, mls, true);
                Draw_from_Model();
                File.Delete(mls1);


                dgv_all_data.Rows.Clear();
                Add_To_List();
                //}
            }
            catch (Exception exx) { }

        }

        private void Load_Survey_Mapping()
        {
            ColumnIndexes cid = new ColumnIndexes();

            cid.Point_Index = 0;
            cid.X_Index = 1;
            cid.Y_Index = 2;
            cid.Z_Index = 3;
            cid.Label_Index = 4;
            dplc = new DrawPolyLineCollection(Survey_File, cid);




            List<string> lst = new List<string>(dplc.Labels.ToArray());



            cmb_layer.Items.Clear();
            lst.Sort();
            if (lst.Count > 0)
                cmb_layer.Items.Add("Select All");
            //lst.Insert(0, "Select All");
            cmb_layer.Items.AddRange(lst.ToArray());


            cmb_layer.SelectedIndex = 0;
            cmb_draw.SelectedIndex = 0;

            Settings setting = new Settings();


            string kStr = "SELECT ALL";
            if (kStr == "SELECT ALL")
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    kStr = lst[i];

                    Setting st = null;

                    if (kStr == "P0")
                    {
                        st = new Setting(kStr, "POINT_ELEVATION", kStr);
                    }
                    else if (kStr == "TEXT")
                    {
                        st = new Setting(kStr, "TEXT", kStr);
                    }
                    else
                    {
                        st = new Setting(kStr, "POLYLINE", kStr);
                    }

                    st.IsDraw = true;
                    st.Layer_Color = Color.White;
                    setting.Add(st);
                }
            }


            DrawPolyLineCollection draw_dplc = dplc;
            vdDocument vdoc = VDoc;
            //Settings setting = new Settings();


            vdoc.ActiveLayOut.Entities.RemoveAll();
            frm_ProgressBar.ON("Creating Model Strings....");
            for (int i = 0; i < setting.Count; i++)
            {

                //if (!setting[i].IsDraw)
                //{

                //    //dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
                //    this.Refresh();
                //    continue;
                //}

                //dgv_all_data.FirstDisplayedScrollingRowIndex = i;
                //dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;


                this.Refresh();
                switch (setting[i].Drawing_Element.ToUpper())
                {
                    case "POLYLINE":
                        draw_dplc.DrawFormat = DrawAs.Polyline;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT":
                        draw_dplc.DrawFormat = DrawAs.Point;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "TEXT":
                        draw_dplc.DrawFormat = DrawAs.Text;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT_ELEVATION":
                        draw_dplc.DrawFormat = DrawAs.Point_Elevation;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT_LABEL":
                        draw_dplc.DrawFormat = DrawAs.Point_Label;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "ELEVATION":
                        draw_dplc.DrawFormat = DrawAs.Elevation;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "LABEL":
                        draw_dplc.DrawFormat = DrawAs.Label;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    default:
                        //draw_dplc.DrawFormat = DrawAs.FromFile;
                        //if (File.Exists(Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element)))
                        //{
                        //    draw_dplc.SelectFromFile(vdoc, Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element), setting[i].Label_Name);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Block Library path is not correct.", "HEADS Viewer", MessageBoxButtons.OK);
                        //}
                        break;
                }

                frm_ProgressBar.SetValue(i + 1, setting.Count);

            }

            frm_ProgressBar.SetValue(setting.Count, setting.Count);

            frm_ProgressBar.OFF();


            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
        }

        void DrawingFromSettingsFile(DrawPolyLineCollection draw_dplc)
        {
            vdDocument vdoc = VDoc;
            Settings setting = new Settings();
            for (int i = 0; i < setting.Count; i++)
            {
                if (!setting[i].IsDraw)
                {

                    //dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.Refresh();
                    continue;
                }

                //dgv_all_data.FirstDisplayedScrollingRowIndex = i;
                //dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;


                this.Refresh();

                switch (setting[i].Drawing_Element.ToUpper())
                {
                    case "POLYLINE":
                        draw_dplc.DrawFormat = DrawAs.Polyline;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT":
                        draw_dplc.DrawFormat = DrawAs.Point;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "TEXT":
                        draw_dplc.DrawFormat = DrawAs.Text;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT_ELEVATION":
                        draw_dplc.DrawFormat = DrawAs.Point_Elevation;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT_LABEL":
                        draw_dplc.DrawFormat = DrawAs.Point_Label;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "ELEVATION":
                        draw_dplc.DrawFormat = DrawAs.Elevation;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "LABEL":
                        draw_dplc.DrawFormat = DrawAs.Label;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    default:
                        //draw_dplc.DrawFormat = DrawAs.FromFile;
                        //if (File.Exists(Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element)))
                        //{
                        //    draw_dplc.SelectFromFile(vdoc, Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element), setting[i].Label_Name);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Block Library path is not correct.", "HEADS Viewer", MessageBoxButtons.OK);
                        //}
                        break;
                }
            }
        }


        #endregion Ground Modeling

        #region Enhanced Mapping


        private void btn_Add_to_list_Click(object sender, EventArgs e)
        {
            Add_To_List();
        }

        public void DrawingToData(vdDocument vdoc)
        {
            MessageBox.Show("Text Data file will be created from selected Survey Base Plan.", "HEADS", MessageBoxButtons.OK);
            vdFigure vFig;
            gPoint gp = new gPoint();

            vdoc.Prompt("Select Entities:");

            //vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            vdSelection vsel = vdoc.ActionUtility.getUserSelection();
            IvdPolyline pLine = null;
            IvdText vdTxt = null;



            dplc = new DrawPolyLineCollection();
            DrawPolyLine dpl = null;
            int pt_no = 0;
            string last_label = "";

            for (int sel = 0; sel < vsel.Count; sel++)
            {
                vFig = vsel[sel];
                //pLine = vFig as vdPolyline;
                if (vFig.Deleted || vFig.Layer.Frozen)
                    continue;
                double last_chainage = 0.0;

                //if (pLine is vdPolyline)
                if (vFig is vdPolyline)
                {
                    #region Polyline
                    pLine = vFig as vdPolyline;
                    last_chainage = 0.0;
                    for (int i = 0; i < pLine.VertexList.Count; i++)
                    {
                        try
                        {
                            dpl = new DrawPolyLine();
                            //dpl.PointNo = i + 1;
                            if (last_label != pLine.Layer.Name)
                            {
                                pt_no = 0;
                            }
                            pt_no++;
                            dpl.PointNo = pt_no;
                            if (i > 0)
                            {
                                dpl.SegmentLength = pLine.VertexList[i - 1].Distance2D(pLine.VertexList[i]);
                                last_chainage += dpl.SegmentLength;
                            }
                            else
                            {
                                dpl.SegmentLength = 0.0;
                            }
                            //dpl.LayerName = txt_layer_name.Text;
                            //dpl.Label = txt_label_name.Text;
                            last_label = pLine.Layer.Name;
                            dpl.LayerName = pLine.Layer.Name;
                            dpl.Label = pLine.Layer.Name;
                            dpl.Chainge = last_chainage;
                            dpl.X = pLine.VertexList[i].x;
                            dpl.Y = pLine.VertexList[i].y;
                            dpl.Z = (pLine.VertexList[i].z == 0.0) ? -999.0 : pLine.VertexList[i].z;
                            dpl.DrawFormat = DrawAs.Polyline;
                            dplc.Add(dpl);
                        }
                        catch (Exception ex) { }
                    }
                    if (pLine.Flag == VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE)
                    {
                        dpl = new DrawPolyLine();
                        dpl.PointNo = ++pt_no;
                        dpl.LayerName = pLine.Layer.Name;
                        dpl.Label = pLine.Layer.Name;
                        dpl.Chainge = last_chainage;
                        dpl.X = pLine.VertexList[0].x;
                        dpl.Y = pLine.VertexList[0].y;
                        dpl.Z = (pLine.VertexList[0].z == 0.0) ? -999.0 : pLine.VertexList[0].z;
                        dpl.DrawFormat = DrawAs.Polyline;
                        dplc.Add(dpl);
                    }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = pLine.Layer.Name;
                    dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dplc[dplc.Count - 1].Z;
                    dplc.Add(dpl);

                    #endregion
                }
                else if (vFig is vdText)
                {
                    #region Text
                    vdTxt = vFig as vdText;
                    last_chainage = 0.0;
                    /**/

                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdTxt.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdTxt.TextString;
                        last_label = vdTxt.Layer.Name;
                        dpl.LayerName = vdTxt.Layer.Name;
                        dpl.Label = vdTxt.Layer.Name;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdTxt.InsertionPoint.x;
                        dpl.Y = vdTxt.InsertionPoint.y;
                        dpl.Z = (vdTxt.InsertionPoint.z == 0.0) ? -999.0 : vdTxt.InsertionPoint.z;

                        dpl.TextString = vdTxt.TextString;
                        dpl.TextHeight = vdTxt.Height;
                        dpl.TextRotation = vdTxt.Rotation;
                        dpl.DrawFormat = DrawAs.Text;
                        dplc.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdTxt.Layer.Name;
                    dpl.Label = vdTxt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    if (dplc.Count > 0)
                    {
                        dpl.Z = dplc[dplc.Count - 1].Z;
                        dplc.Add(dpl);
                    }

                    #endregion
                }
                else if (vFig is vdPoint)
                {
                    //continue;
                    #region Point
                    vdPoint vdpnt = vFig as vdPoint;
                    last_chainage = 0.0;
                    /**/
                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdpnt.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdpnt.TextString;
                        last_label = vdpnt.Layer.Name;
                        //dpl.LayerName = vdpnt.Layer.Name;
                        dpl.LayerName = "P0";
                        dpl.Label = dpl.LayerName;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdpnt.InsertionPoint.x;
                        dpl.Y = vdpnt.InsertionPoint.y;
                        dpl.Z = (vdpnt.InsertionPoint.z == 0.0) ? -999.0 : vdpnt.InsertionPoint.z;
                        dpl.DrawFormat = DrawAs.Point;
                        dplc.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdpnt.Layer.Name;
                    //dpl.Label = txt_label_name.Text;
                    //dpl.Label = vdpnt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    if (dplc.Count > 0)
                    {
                        dpl.Z = dplc[dplc.Count - 1].Z;
                        //dpl.Z = dplc[dplc.Count - 1].Z;
                        dplc.Add(dpl);
                    }

                    #endregion
                }
                else if (vFig is vdInsert)
                {
                    continue;
                    #region Insert
                    vdInsert vdins = vFig as vdInsert;
                    last_chainage = 0.0;
                    /**/
                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdins.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdpnt.TextString;
                        last_label = vdins.Layer.Name;
                        dpl.LayerName = vdins.Layer.Name;
                        dpl.Label = vdins.Layer.Name;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdins.InsertionPoint.x;
                        dpl.Y = vdins.InsertionPoint.y;
                        dpl.Z = (vdins.InsertionPoint.z == 0.0) ? -999.0 : vdins.InsertionPoint.z;
                        dpl.DrawFormat = DrawAs.FromFile;
                        dplc.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdins.Layer.Name;
                    //dpl.Label = txt_label_name.Text;
                    //dpl.Label = vdpnt.TextString;
                    dpl.Label = vdins.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dplc[dplc.Count - 1].Z;
                    dplc.Add(dpl);

                    #endregion
                }
                else if (vFig is vdLine)
                {
                    #region Line
                    vdLine vdpnt = vFig as vdLine;
                    last_chainage = 0.0;
                    /**/
                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdpnt.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdpnt.TextString;
                        last_label = vdpnt.Layer.Name;
                        dpl.LayerName = vdpnt.Layer.Name;
                        //dpl.Label = txt_label_name.Text;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdpnt.StartPoint.x;
                        dpl.Y = vdpnt.StartPoint.y;
                        dpl.Z = (vdpnt.StartPoint.z == 0.0) ? -999.0 : vdpnt.StartPoint.z;
                        dpl.DrawFormat = DrawAs.Line;
                        dplc.Add(dpl);

                        dpl = new DrawPolyLine();

                        pt_no++;
                        dpl.PointNo = pt_no;

                        dpl.LayerName = vdpnt.Layer.Name;
                        //dpl.Label = txt_label_name.Text;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdpnt.EndPoint.x;
                        dpl.Y = vdpnt.EndPoint.y;
                        dpl.Z = (vdpnt.EndPoint.z == 0.0) ? -999.0 : vdpnt.EndPoint.z;
                        dpl.DrawFormat = DrawAs.Line;
                        dplc.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdpnt.Layer.Name;
                    //dpl.Label = txt_label_name.Text;
                    //dpl.Label = vdpnt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dplc[dplc.Count - 1].Z;
                    dplc.Add(dpl);

                    #endregion
                }
            }
        }
        public void SetLayerLabel()
        {
            List<string> list_layer = new List<string>();
            //dgv_layer_label.Rows.Clear();


            DrawPolyLineCollection dpl_col = dplc;
            for (int i = 0; i < dpl_col.Count; i++)
            {
                if (!list_layer.Contains(dpl_col[i].LayerName))
                {
                    list_layer.Add(dpl_col[i].LayerName);
                    //dgv_layer_label.Rows.Add(dpl_col[i].LayerName, dpl_col[i].LayerName);
                }
            }
            //for (int i = 0; i < dpl_col.Labels.Count; i++)
            //{
            //        //list_layer.Add(dpl_col.Labels[i]);
            //        dgv_layer_label.Rows.Add(dpl_col.Labels[i], "");
            //}
            list_layer.Clear();
            list_layer = null;
        }
        void SetLabels()
        {
            Hashtable ht = new Hashtable();
            string layer, label;
            layer = string.Empty;
            label = string.Empty;

            int i = 0;
            //for (i = 0; i < dgv_layer_label.Rows.Count; i++)
            //{

            //    layer = dgv_layer_label[0, i].Value.ToString();
            //    label = dgv_layer_label[1, i].Value.ToString();
            //    if (label != null && layer != null)
            //    {
            //        ht.Add(layer, label);
            //    }
            //}

            for (i = 0; i < dpl_col.Count; i++)
            {
                layer = dpl_col[i].LayerName;
                label = (string)ht[layer];
                if (label != null)
                {
                    dpl_col[i].Label = (label == "") ? layer : label;
                }

            }
            ht.Clear();
            ht = null;
        }

        private void Add_To_List()
        {

            string kStr = cmb_layer.Text.ToUpper();

            List<string> ex_items = new List<string>();
            for (int i = 0; i < dgv_all_data.RowCount; i++)
            {
                if (!ex_items.Contains(dgv_all_data[1, i].Value.ToString()))
                    ex_items.Add(dgv_all_data[1, i].Value.ToString());

            }
            if (kStr == "SELECT ALL")
            {


                for (int i = 0; i < cmb_layer.Items.Count; i++)
                {
                    kStr = cmb_layer.Items[i].ToString();
                    if (kStr.ToUpper() != "SELECT ALL" && !ex_items.Contains(kStr))
                    {
                        if (kStr == "P0")
                            dgv_all_data.Rows.Add(true, kStr, "POINT_ELEVATION", kStr);
                        else if (kStr == "OGL" || kStr == "GROUND")
                            dgv_all_data.Rows.Add(true, kStr, "POINT", kStr);
                        else if ((kStr != "TEXT") || (kStr == "TEXT" && cmb_draw.Text == "TEXT"))
                            dgv_all_data.Rows.Add(true, kStr, cmb_draw.Text, kStr);
                    }
                }
            }
            else
            {

                if (ex_items.Contains(kStr))
                {
                    MessageBox.Show("\"" + cmb_layer.Text + "\"" + " is already added.", "HEADS", MessageBoxButtons.OK);
                    return;
                }
                dgv_all_data.Rows.Add(true, cmb_layer.Text, cmb_draw.Text, cmb_layer.Text);
            }
        }

        DrawPolyLineCollection dplc;
        DrawPolyLineCollection dpl_col
        {
            get
            {
                return dplc;
            }
            set
            {
                dplc = value;
            }
        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {

            vdDocument vdoc = VDoc;
            ReadFromGrid();
            if (dplc == null)
            {
                ColumnIndexes colIndex = new ColumnIndexes();
                colIndex.Point_Index = 0;
                colIndex.X_Index = 1;
                colIndex.Y_Index = 2;
                colIndex.Z_Index = 3;
                colIndex.Layer_Index = 4;
                dplc = new DrawPolyLineCollection(Survey_File, colIndex);
            }

            DrawingFromSettingsFile(dplc, vdoc);
            vdoc.Redraw(true);
            //vdoc.SaveAs(Path.Combine(Working_Folder, "MAPPING.VDML"));
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
        }

        public void ReadFromGrid()
        {
            setting = new Settings();
            Setting sett = null;
            for (int i = 0; i < dgv_all_data.Rows.Count; i++)
            {
                sett = new Setting();
                sett.IsDraw = bool.Parse(dgv_all_data[0, i].Value.ToString());
                sett.Label_Name = dgv_all_data[1, i].Value.ToString();
                sett.Drawing_Element = dgv_all_data[2, i].Value.ToString();
                sett.Layer_Name = dgv_all_data[3, i].Value.ToString();
                if (dgv_all_data[4, i].Style.BackColor.A == 0 &&
                    dgv_all_data[4, i].Style.BackColor.R == 0 &&
                    dgv_all_data[4, i].Style.BackColor.G == 0 &&
                    dgv_all_data[4, i].Style.BackColor.B == 0)
                {
                    sett.Layer_Color = Color.White;
                }
                else
                    sett.Layer_Color = dgv_all_data[4, i].Style.BackColor;

                setting.Add(sett);
            }
        }

        private void btn_browse_lib_Click(object sender, EventArgs e)
        {
            string block_lib_path = Path.Combine(Application.StartupPath, @"Make Ground Survey Drawing\Block Library");
            if (!Directory.Exists(block_lib_path))
                block_lib_path = Path.Combine(Application.StartupPath, "Block Library");

            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.SelectedPath = block_lib_path;

            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                txt_drawing_lib.Text = fbd.SelectedPath;
                LoadDrawingsFromFolder(txt_drawing_lib.Text);
            }
        }

        private void btn_save_settings_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = "UserSettings1.hus";
                sfd.Filter = "HEADS User Settings Files|*.hus";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    //setting.SaveSettings(sfd.FileName);
                    Settings.Serialise(sfd.FileName, setting);
                }
            }
        }

        void DrawingFromSettingsFile(DrawPolyLineCollection draw_dplc, vdDocument vdoc)
        {
            RefreshItems();
            for (int i = 0; i < setting.Count; i++)
            {
                if (!setting[i].IsDraw)
                {

                    dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.Refresh();
                    continue;
                }

                dgv_all_data.FirstDisplayedScrollingRowIndex = i;
                //dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;


                this.Refresh();

                switch (setting[i].Drawing_Element.ToUpper())
                {
                    case "POLYLINE":
                        draw_dplc.DrawFormat = DrawAs.Polyline;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT":
                        draw_dplc.DrawFormat = DrawAs.Point;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "TEXT":
                        draw_dplc.DrawFormat = DrawAs.Text;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT_ELEVATION":
                        draw_dplc.DrawFormat = DrawAs.Point_Elevation;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "POINT_LABEL":
                        draw_dplc.DrawFormat = DrawAs.Point_Label;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "ELEVATION":
                        draw_dplc.DrawFormat = DrawAs.Elevation;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    case "LABEL":
                        draw_dplc.DrawFormat = DrawAs.Label;
                        draw_dplc.DataToDrawing(vdoc, setting[i]);
                        break;
                    default:
                        draw_dplc.DrawFormat = DrawAs.FromFile;
                        if (File.Exists(Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element)))
                        {
                            draw_dplc.SelectFromFile(vdoc, Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element), setting[i].Label_Name);
                        }
                        else
                        {
                            MessageBox.Show("Block Library path is not correct.", "HEADS Viewer", MessageBoxButtons.OK);
                        }
                        break;
                }
            }
        }
        void LoadDrawingsFromFolder(string folder)
        {
            cmb_draw.Items.Clear();
            cmb_draw.Items.Add("POLYLINE");
            cmb_draw.Items.Add("POINT");
            cmb_draw.Items.Add("POINT_ELEVATION");
            cmb_draw.Items.Add("POINT_LABEL");
            cmb_draw.Items.Add("ELEVATION");
            cmb_draw.Items.Add("LABEL");
            cmb_draw.Items.Add("TEXT");
            string ext = "";
            foreach (string nextFile in Directory.GetFiles(folder))
            {
                ext = Path.GetExtension(nextFile).ToLower();
                if (ext == ".vdml" ||
                    ext == ".vdcl" ||
                    ext == ".dxf" ||
                    ext == ".dwg" ||
                    ext == ".jpg" ||
                    ext == ".bmp")
                    cmb_draw.Items.Add(Path.GetFileName(nextFile));
            }
            cmb_draw.SelectedIndex = cmb_draw.Items.Count > 0 ? 0 : -1;
        }

        Settings setting;

        private void btn_browse_settings_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "HEADS User Settings Files|*.hus";
            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    setting = Settings.DeSerialise(ofd.FileName);
                    txt_select_settings.Text = ofd.FileName;
                    dgv_all_data.Rows.Clear();
                    foreach (Setting sett in setting)
                    {
                        dgv_all_data.Rows.Add(sett.IsDraw, sett.Label_Name, sett.Drawing_Element, sett.Layer_Name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File format is not correct.", "HEADS Viewer", MessageBoxButtons.OK);
                }
            }
        }

        private void btn_delete_rows_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("All the ticked items will be deleted. Are you sure ?", "HEADS", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                for (int i = 0; i < dgv_all_data.RowCount; i++)
                {
                    if ((bool)(dgv_all_data[0, i].Value))
                    {
                        dgv_all_data.Rows.RemoveAt(i);
                        i = -1;
                    }
                }
            }
            catch (Exception ex) { }

        }

        private void chk_Draw_All_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_all_data.Rows.Count; i++)
            {
                dgv_all_data[0, i].Value = chk_Draw_All.Checked;
            }
            Layer_On_Off();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {

            VDoc.ActiveLayOut.Entities.RemoveAll();
            VDoc.Redraw(true);

            RefreshItems();


        }
        void RefreshItems()
        {
            for (int i = 0; i < dgv_all_data.Rows.Count; i++)
            {
                dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            this.Refresh();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {

        }


        private void dgv_all_data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                using (frmColorDialog cd = new frmColorDialog())
                {
                    cd.SelectedColor = dgv_all_data[e.ColumnIndex, e.RowIndex].Style.BackColor;
                    if (cd.ShowDialog() != DialogResult.Cancel)
                    {

                        if (cd.IsApply_to_All)
                        {
                            for (int i = 0; i < dgv_all_data.RowCount; i++)
                            {
                                //if (bool.Parse(dgv_all_data[0, i].Value.ToString()))
                                //{
                                dgv_all_data[4, i].Style.BackColor = cd.SelectedColor;

                                vdLayer vlay = VDoc.Layers.FindName(dgv_all_data[3, i].Value.ToString());

                                if (vlay != null)
                                {
                                    vlay.PenColor = new vdColor(cd.SelectedColor);

                                    vlay.Update();
                                }
                                //}
                            }
                        }
                        else
                        {
                            dgv_all_data[e.ColumnIndex, e.RowIndex].Style.ForeColor = cd.SelectedColor;
                            dgv_all_data[e.ColumnIndex, e.RowIndex].Style.BackColor = cd.SelectedColor;
                            //dgv_all_data[e.ColumnIndex, e.RowIndex].Selected = false;
                            vdLayer vlay = VDoc.Layers.FindName(dgv_all_data[3, e.RowIndex].Value.ToString());

                            if (vlay != null)
                            {
                                vlay.PenColor = new vdColor(cd.SelectedColor);

                                vlay.Update();
                            }
                        }

                        VDoc.Redraw(true);
                    }
                    dgv_all_data[e.ColumnIndex, e.RowIndex].Selected = false;
                }
            }
            //if (e.ColumnIndex == 0)
            //{
            Layer_On_Off();
            //}
        }
        void Load_Color_from_Drawing()
        {
            string kStr = "";
            vdLayer ld = null;
            for (int i = 0; i < dgv_all_data.RowCount; i++)
            {
                kStr = dgv_all_data[1, i].Value.ToString();

                ld = VDoc.Layers.FindName(kStr);
                if (ld != null) dgv_all_data[4, i].Style.BackColor = ld.PenColor.AsSystemColor();
                //dgv_all_data[1, i]
            }
        }


        void Layer_On_Off()
        {
            vdLayer vlay = null;
            for (int i = 0; i < dgv_all_data.RowCount; i++)
            {
                try
                {
                    vlay = VDoc.Layers.FindName(dgv_all_data[3, i].Value.ToString());
                    if (vlay != null)
                        vlay.Frozen = !(bool.Parse(dgv_all_data[0, i].Value.ToString()));
                }
                catch (Exception exx) { }
            }
            VDoc.Redraw(true);
        }

        #endregion Enhanced Mapping

        #region Ground Modeling Triangulation

        private void btn_GMT_DeSelectAll_Click(object sender, EventArgs e)
        {
            for (int iIndex = 0; iIndex < this.lb_GMT_ModelAndStringName.Items.Count; iIndex++)
            {
                this.lb_GMT_ModelAndStringName.SetSelected(iIndex, false);
            }
        }

        private void btn_GMT_Select_Click(object sender, EventArgs e)
        {
            string strSerach = this.txt_GMT_Select.Text.Trim();
            if (strSerach != "")
            {
                int iQuesMarkIndex = strSerach.IndexOf('?');
                if (iQuesMarkIndex > 0)
                {
                    strSerach = strSerach.Substring(0, iQuesMarkIndex);
                }

                for (int iIndex = 0; iIndex < this.lb_GMT_ModelAndStringName.Items.Count; iIndex++)
                {
                    string kSS = this.lb_GMT_ModelAndStringName.Items[iIndex].ToString();
                    int idx = kSS.IndexOf(":");


                    kSS = kSS.Substring(idx + 1, kSS.Length - 1 - idx);


                    if (kSS.StartsWith(strSerach))
                    {
                        this.lb_GMT_ModelAndStringName.SetSelected(iIndex, true);
                    }
                }
            }
        }



        private void btn_GMT_SelectAll_Click(object sender, EventArgs e)
        {
            for (int iIndex = 0; iIndex < this.lb_GMT_ModelAndStringName.Items.Count; iIndex++)
            {
                this.lb_GMT_ModelAndStringName.SetSelected(iIndex, true);
            }
        }

        private void btn_GMT_DeSelect_Click(object sender, EventArgs e)
        {

        }

        private void btn_GMT_OK_Click(object sender, EventArgs e)
        {
            //if (!Folder_Copy(Folder_Ground_Modeling, Working_Folder, false)) return;

            Folder_Delete(Folder_DTM_Data, true);

            if (lb_GMT_ModelAndStringName.SelectedIndices.Count <= 0)
            {
                MessageBox.Show("Please select atleast one Model and string for Ground Modelling", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Folder_Current = Working_Folder;
            //Folder_Delete(1); // Delete from HALIGN

            //MessageBox.Show(Folder_Current, "HEADS");

            //StreamWriter sw = new StreamWriter(new FileStream(Path.Combine(Working_Folder, "DGM4DLG.TMP"), FileMode.Create));
            StreamWriter sw = null;

            if (Directory.Exists(Folder_Current))
                sw = new StreamWriter(new FileStream(Path.Combine(Folder_Current, "DGM4DLG.TMP"), FileMode.Create));
            else
                sw = new StreamWriter(new FileStream(Path.Combine(Working_Folder, "DGM4DLG.TMP"), FileMode.Create));



            string kStr = "";
            int indx = 0;
            try
            {
                for (int i = 0; i < lb_GMT_ModelAndStringName.SelectedIndices.Count; i++)
                {
                    indx = lb_GMT_ModelAndStringName.SelectedIndices[i];
                    kStr = lb_GMT_ModelAndStringName.Items[indx].ToString();

                    string[] vals = kStr.Split(new char[] { ':' });
                    //vals[0] = vals[0].Trim();
                    //vals[1] = vals[1].Trim();

                    sw.Write("{0,-25}{1,-15}", vals[0].Trim(), vals[1].Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Proccessing Error!", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }

            Process_Triangulation();
            Folder_Copy(Working_Folder, Folder_DTM_Data);
        }


        private void Process_Triangulation()
        {

            List<string> HEADS_Data = new List<string>();
            //HEADS
            HEADS_Data.Add("HEADS");
            //100,DGM
            HEADS_Data.Add("100,DGM");
            //101,BOU,MODEL=BOUND,STRING=BDRY
            HEADS_Data.Add("101,BOU,MODEL=" + txt_GMT_model.Text + ",STRING=" + txt_GMT_string.Text);
            //FINISH
            HEADS_Data.Add("FINISH");

            Folder_Current = Working_Folder;

            if (!Directory.Exists(Folder_Current))
                Folder_Current = Working_Folder;



            string tmp_file = Path.Combine(Folder_Current, "inp.tmp");
            File.WriteAllLines(tmp_file, HEADS_Data.ToArray());

            Set_HEADS_Environment_Variable(tmp_file);
            SetEnvironment(Path.Combine(Folder_Current, "inp.tmp"));


            //string ffl = Path.Combine(Working_Folder, "HDS000.FIL");
            string ffl = Path.Combine(Folder_Current, "HDS000.FIL");

            if (File.Exists(ffl)) File.Delete(ffl);

            //ffl = Path.Combine(Working_Folder, "HDS001.FIL");
            ffl = Path.Combine(Folder_Current, "HDS001.FIL");
            if (File.Exists(ffl)) File.Delete(ffl);



            if (RunExe("dtm.exe"))
            {
                //try
                //{
                //    ffl = Path.Combine(Folder_Current, "HDS001.FIL");
                //    if (File.Exists(ffl))
                //        rtb_HDS001.Lines = File.ReadAllLines(ffl);
                //    else
                //        rtb_HDS001.Text = "";
                //}
                //catch (Exception exx) { }
            }

            //Run_Data(tmp_file);


        }







        #endregion Ground Modeling Triangulation

        #region Contour Modeling
        public string Project_Drawing_File
        {
            get
            {
                //return Path.Combine(Folder_Process_Survey_Data, "Project_Drawing.VDML");
                if (Folder_Current == null)
                    Folder_Current = Working_Folder;
                return Path.Combine(Folder_Current, "Project_Drawing.VDML");
            }

        }

        private void btn_Contour_Save_Click(object sender, EventArgs e)
        {
            Folder_Delete(Folder_Contour_Data, true);
            Folder_Copy(Folder_DTM_Data, Working_Folder);

            Folder_Current = Working_Folder;


            List<string> HEADS_Data = new List<string>();

            string str = "";
            HEADS_Data.Clear();
            //HEADS
            HEADS_Data.Add("HEADS");
            //100,DGM
            HEADS_Data.Add("100,DGM");
            //104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0
            str = string.Format("104,CON,MODEL={0},STRING={1},INC={2}",
                txt_Contour_pri_model.Text,
                txt_Contour_pri_string.Text,
                txt_Contour_pri_inc.Text);
            HEADS_Data.Add(str);
            //104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0
            str = string.Format("104,CON,MODEL={0},STRING={1},INC={2}",
                txt_Contour_sec_model.Text,
                txt_Contour_sec_string.Text,
                txt_Contour_sec_inc.Text);
            HEADS_Data.Add(str);
            //105,TXT,MODEL=CONTOUR,STRING=ELE2,INC=5.0,TSI=5
            str = string.Format("105,TXT,MODEL={0},STRING={1},INC={2}",
                txt_Contour_ele_model.Text,
                txt_Contour_ele_string.Text,
                txt_Contour_ele_inc.Text);
            HEADS_Data.Add(str);
            //FINISH
            str = "FINISH";
            HEADS_Data.Add(str);


            if (Directory.Exists(Folder_Current) == false)
                Folder_Current = Working_Folder;

            File.WriteAllLines(Path.Combine(Folder_Current, "contour.txt"), HEADS_Data.ToArray());
            SetEnvironment(Path.Combine(Folder_Current, "contour.txt"));



            HEADS_Data.Clear();
            HEADS_Data.Add(string.Format("{0} {1} {2}",
                txt_Contour_pri_model.Text,
                txt_Contour_pri_string.Text,
                txt_Contour_pri_inc.Text

                ));
            HEADS_Data.Add(string.Format("{0} {1} {2}",
                txt_Contour_sec_model.Text,
                txt_Contour_sec_string.Text,
                txt_Contour_sec_inc.Text));
            HEADS_Data.Add(string.Format("{0} {1} {2}", txt_Contour_ele_model.Text, txt_Contour_ele_string.Text, txt_Contour_ele_inc.Text));

            File.WriteAllLines(Path.Combine(Folder_Current, "CONTOUR.FIL"), HEADS_Data.ToArray());

            RunExe("dgcont.exe", true);

            Folder_Copy(Working_Folder, Folder_Contour_Data);

        }

        private void txt_Contour_pri_inc_TextChanged(object sender, EventArgs e)
        {
            //txt_Contour_ele_inc.Text = (MyList.StringToDouble(txt_Contour_pri_inc.Text, 0.0) * 5).ToString("0.00");
            //txt_Contour_sec_inc.Text = (MyList.StringToDouble(txt_Contour_pri_inc.Text, 0.0) * 5).ToString("0.00");

            txt_Contour_ele_inc.Text = (MyList.StringToDouble(txt_Contour_pri_inc.Text, 0.0) * 5).ToString();
            txt_Contour_sec_inc.Text = (MyList.StringToDouble(txt_Contour_pri_inc.Text, 0.0) * 5).ToString();



        }

        private void btn_Contour_draw_model_Click(object sender, EventArgs e)
        {
            //VDoc.ActiveLayOut.Entities.RemoveAll();
            Folder_Current = Working_Folder;
            if (!Directory.Exists(Folder_Current))
            {
                MessageBox.Show("Contour Model is not Created.", "HEADS", MessageBoxButtons.OK);
                return;
            }
            Close_Exe();

            List<CModel> lst_mdl_str = new List<CModel>();

            lst_mdl_str.Add(new CModel("CONTOUR", "C001", Color.Gray));
            lst_mdl_str.Add(new CModel("CONTOUR", "C005", Color.Orange));
            lst_mdl_str.Add(new CModel("CONTOUR", "ELEV", Color.Orange));

            ExecDrawString(lst_mdl_str, "Contour");

            chk_Contour_C001.Checked = true;
            chk_Contour_C005.Checked = true;
            chk_Contour_ELEV.Checked = true;

            Create_Layer("Align");

            VDoc.SaveAs(Project_Drawing_File);

            Vdraw.vdCommandAction.View3D_VTop(VDoc);

            this.TopMost = true;
            System.Threading.Thread.Sleep(100);
            this.TopMost = false;

        }
        private void btn_draw_ground_surface_Click(object sender, EventArgs e)
        {
            Folder_Current = Working_Folder;
            if (!Directory.Exists(Folder_Current))
            {
                MessageBox.Show("Contour Model is not Created.", "HEADS", MessageBoxButtons.OK);
                return;
            }

            //VDoc.ActiveLayOut.Entities.RemoveAll();
            List<string> stgs = new List<string>();
            Selected_Line_Type = 4; //for 3D face
            stgs.Add("DTRA");

            Current_Color = Color.DarkGreen;
            Create_Layer("SURFACE").PenColor = new vdColor(Current_Color);

            Set_Max_Min_Elevation("TRIANGLS", stgs);

            ExecDrawString("TRIANGLS", stgs);

            Vdraw.vdCommandAction.View3D_ShadeOn(VDoc);

            chk_Contour_SURFACE.Checked = true;

            Selected_Line_Type = 1; //for Line

            Create_Layer("Align").PenColor = new vdColor(Color.Red);
            VDoc.SaveAs(Project_Drawing_File);
            Vdraw.vdCommandAction.View3D_VTop(VDoc);
        }
        private void btn_contour_refresh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will remove the last process.\nDo you want to refresh ?", "HEADS", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                VDoc.ActiveLayOut.Entities.RemoveAll();
                VDoc.Redraw(true);
            }
        }

        #endregion Contour Modeling

        #region tc_main_Selected

        private void tc_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = sender as TabControl;
            tc_main_Selected();
        }


        private void tc_main_Selected()
        {
            try
            {
                #region Satellite Project
                if (tc_main.SelectedTab == tab_create_project)
                {
                    sc_main.Panel2Collapsed = false;
                    sc_main.SplitterDistance = 460;
                }
                else if (tc_main.SelectedTab == tab_satellite)
                {
                    sc_main.Panel2Collapsed = true;
                }
                else if (tc_main.SelectedTab == tab_trvrs)
                {
                    sc_main.Panel2Collapsed = true;
                    sc_traverse.Panel2Collapsed = false;
                    sc_traverse.SplitterDistance = 400;
                }
                else if (tc_main.SelectedTab == tab_land)
                {
                    #region Land Record Data
                    sc_main.Panel2Collapsed = true;
                    //sc_main.Panel2Collapsed = false;
                    //sc_main.SplitterDistance = 400;


                    #endregion Land Record Data
                }
                #endregion Satellite Project

                #region Process Survey Data
                else if (tc_main.SelectedTab == tab_survey)
                {
                    //sc_main.Panel2Collapsed = false;
                    //sc_main.SplitterDistance = 500;
                    if (rbtn_auto_level.Checked)
                    {
                        if (tc_survey.TabPages.Contains(tab_auto_level_halign) == false)
                        {
                            //tc_survey.TabPages.Add(tab_auto_level_halign);
                            tc_survey.TabPages.Insert(0, tab_auto_level_halign);
                            tc_survey.SelectedTab = tab_auto_level_halign;
                        }
                    }
                    else
                    {
                        if (tc_survey.TabPages.Contains(tab_auto_level_halign))
                        {
                            tc_survey.TabPages.Remove(tab_auto_level_halign);
                        }
                    }

                    if (tc_survey.SelectedTab == tab_auto_level_halign)
                    {
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;

                        if (tc_auto_level_halign.SelectedTab == tab_auto_level_prohalign_data)
                        {
                            cmb_auto_halign.SelectedIndex = 3;
                        }
                        else if (tc_auto_level_halign.SelectedTab == tab_auto_level_halign_data)
                        {
                            cmb_auto_halign.SelectedIndex = 2;
                        }
                        else if (tc_auto_level_halign.SelectedTab == tab_auto_traverse_data)
                        {
                            cmb_auto_halign.SelectedIndex = 4;
                        }
                    }
                    else if (tc_survey.SelectedTab == tab_gm)
                    {
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;
                    }
                    else if (tc_survey.SelectedTab == tab_trngu)
                    {
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;
                    }
                    else if (tc_survey.SelectedTab == tab_cont)
                    {
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;
                    }
                    else if (tc_survey.SelectedTab == tab_strg_qty)
                    {
                        uC_Stockpile.working_folder = Working_Folder;
                        if (uC_Stockpile.rtb_report.Lines.Length <= 0)
                        {
                            if (File.Exists(uC_Stockpile.Storage_ReportFile))
                            {
                                uC_Stockpile.rtb_report.Lines = File.ReadAllLines(uC_Stockpile.Storage_ReportFile);
                            }
                        }
                    }
                    else if (tc_survey.SelectedTab == tab_alignment)
                    {
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;
                    }
                }
                #endregion Satellite Project

                #region Site Leveling and Grading
                else if (tc_main.SelectedTab == tab_site_lvl_grd)
                {
                    if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step5 ||
                        tc_Site_Leveling_Grading.SelectedTab == tab_site_step8 ||
                        tc_Site_Leveling_Grading.SelectedTab == tab_site_step9
                        )
                    {
                        sc_main.Panel2Collapsed = true;

                        sc_HDP_valign.SplitterDistance = 420;
                    }
                    else
                    {
                        if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step6)
                        {
                            if (ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
                            {
                                string ss = Path.Combine(Application.StartupPath, @"Design\Typical Cross Sections Irrigation\TCS 01.dwg");
                                if (File.Exists(ss))
                                {
                                    if(vdsC_DOC_river_canal.ActiveDocument != null) vdsC_DOC_river_canal.ActiveDocument.Open(ss);
                                }
                                sc_offset.Panel2Collapsed = false;
                            }
                            else if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
                            {
                                sc_offset.Panel2Collapsed = true;
                            }
                        }
                         
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;
                    }

                    if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step4)
                    {
                        if (tc_SLG_halign.SelectedTab == tab_SLG_halign_data)
                        {
                            cmb_SLG_halign.SelectedIndex = 2;
                        }
                        else if (tc_SLG_halign.SelectedTab == tab_SLG_prohalign_data)
                        {
                            cmb_SLG_halign.SelectedIndex = 3;
                        }
                        else if (tc_SLG_halign.SelectedTab == tab_SLG_traverse_data)
                        {
                            cmb_SLG_halign.SelectedIndex = 4;
                        }
                    }
                    else if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step6)
                    {
                        //    if (tc_SLG_cross_secion.SelectedTab == tab_HDP_define_cross_section ||
                        //        tc_SLG_cross_secion.SelectedTab == tab_HDP_interface )
                        //    {
                        //        sc_main.Panel2Collapsed = true;
                        //    }
                        //    else
                        //    {
                        sc_main.Panel2Collapsed = false;
                        sc_main.SplitterDistance = 500;
                        //    }
                    }
                    else if (tc_Site_Leveling_Grading.SelectedTab == tab_site_step7)
                    {
                        sc_main.Panel2Collapsed = true;
                        sc_volume.SplitterDistance = 500;
                        sc_volume_rep.SplitterDistance = 240;
                    }

                }
                #endregion Site Leveling and Grading


            }
            catch (Exception pxx) { }
            finally
            {
                this.Refresh();
            }
        }

        private void btn_auto_halign_process_Click(object sender, EventArgs e)
        {
            RichTextBox txt_HDP_HalignData = rtb_auto_level_halign;



            if (cmb_auto_halign.SelectedIndex == 0)
            {
                #region  New HALIGN Design

                if (Directory.Exists(Folder_Contour_Data))
                    Folder_Copy(Folder_Contour_Data, Working_Folder);
                else
                {
                    if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                }

                //Folder_Delete(Folder_HDP_Halign, true);

                Folder_Current = Working_Folder;
                //if(Directory.Exists(Folder_Contour_Data))
                //    Folder_Copy(Folder_Contour_Data, Folder_Current);
                //else
                //    Folder_Copy(Folder_Process_Survey_Data, Folder_Current);
                VDoc.SaveAs(Project_Drawing_File);
                //Environment.SetEnvironmentVariable("DESIGN_OPTION", "HALIGN$" + Project_Drawing_File);
                System.Environment.SetEnvironmentVariable("HEADSPROJECT", "");
                Environment.SetEnvironmentVariable("DESIGN_OPTION", "HALIGN$HIP$" + Project_Drawing_File);
                //Environment.SetEnvironmentVariable("DESIGN_OPTION", "HALIGN$HIP$" + Folder_Current);
                Environment.SetEnvironmentVariable("WORKING_FOLDER", Folder_Current);
                RunExe("VIEWER.EXE", false);
                #endregion  New HALIGN Design
            }
            else if (cmb_auto_halign.SelectedIndex == 1)
            {
                #region  Automatic HALIGN Design Wizard
                #region New HIP Wizard

                Folder_Delete(Folder_HDP_Halign, true);
                if (Directory.Exists(Folder_Contour_Data))
                    Folder_Copy(Folder_Contour_Data, Working_Folder);
                else
                    Folder_Copy(Folder_DTM_Data, Working_Folder);


                Folder_Current = Working_Folder;
                //Folder_Copy(Folder_Process_Survey_Data, Folder_Current);

                //HeadsFunctions.MakeHalignDialog(this, false);
                SelectedModel = "DESIGN";
                SelectedStrings.Add("M001");
                frm_MakeHalignOption fmho = new frm_MakeHalignOption();

                fmho.ShowDialog();

                MessageBox.Show("Please Select a Polyline....", "HEADS", MessageBoxButtons.OK);
                Form frmBoundary = HeadsFunctions.MakeHalignDialog(this, fmho.IsLinear_Polyline);

                if (frmBoundary != null)
                {
                    frmBoundary.Owner = this;
                    frmBoundary.ShowDialog();
                    Read_Halign_Data();
                }
                Folder_Copy(Working_Folder, Folder_HDP_Halign);

                #endregion New HIP Wizard
                #endregion  Automatic HALIGN Design Wizard
            }
            else if (cmb_auto_halign.SelectedIndex == 2)
            {
                #region  Process HALIGN Design Data

                if (rbtn_auto_level.Checked == false)
                {
                    if (Directory.Exists(Folder_Contour_Data))
                        Folder_Copy(Folder_Contour_Data, Working_Folder);
                    else
                    {
                        if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                    }
                }
                Folder_Current = Working_Folder;




                if ((txt_HDP_HalignData.Lines.Length < 3))
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        //ofd.Filter = "Text File (*.txt)|*.txt|HEADS Report File (*.rep)|*.rep|All Files (*.*)|*.*";
                        ofd.Filter = "HALIGN Data File (*.rep;*.txt)|*.rep;*.txt|All Files (*.*)|*.*";
                        if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                        {
                            Read_Halign_Data(ofd.FileName);
                        }
                    }
                }


                if ((txt_HDP_HalignData.Lines.Length > 3))
                {

                    string inp = Path.Combine(Folder_Current, "Halign_Data.txt");

                    File.WriteAllLines(inp, txt_HDP_HalignData.Lines);

                    Set_HEADS_Environment_Variable(inp);
                    RunExe("HALIGN_Road.EXE", true);
                }

                Folder_Copy(Working_Folder, Folder_HDP_Halign);
                #endregion  Process HALIGN Design Data
            }
            else if (cmb_auto_halign.SelectedIndex == 3)
            {
                #region  HALIGN Coordinate Inputs [Traverse IPs]
                if (Directory.Exists(Folder_Contour_Data))
                    Folder_Copy(Folder_Contour_Data, Working_Folder);
                else
                {
                    //if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                }

                //Read_Halign_Data
                //Folder_Delete(Folder_HDP_Halign, true);

                Folder_Current = Working_Folder;



                frm_HALIGN_Coordinates fd = new frm_HALIGN_Coordinates(ProjectType);
                fd.Run_Data = new dProcess_Data(Procees_HALIGN_Coordinate_Inputs);

                fd.txt_SC.Text = Start_Chainage.ToString("f3");
                fd.txt_IN.Text = Interval_Chainage.ToString("f3");

                txt_HDP_HalignData = rtb_auto_level_prohalign;

                fd.Alignment_Data = new List<string>(txt_HDP_HalignData.Lines);
                fd.Working_Folder = Working_Folder;
                fd.Owner = this;
                fd.Show();
                //if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                #endregion  HALIGN Coordinate Inputs [Traverse IPs]
            }
            else if (cmb_auto_halign.SelectedIndex == 4)
            {
                if (false)
                {
                    #region  Traverse Alignment

                    if (Directory.Exists(Folder_Contour_Data))
                        Folder_Copy(Folder_Contour_Data, Working_Folder);
                    else
                    {
                        //if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                    }

                    //Read_Halign_Data
                    //Folder_Delete(Folder_HDP_Halign, true);

                    Folder_Current = Working_Folder;



                    frm_HALIGN_Coordinates fd = new frm_HALIGN_Coordinates(ProjectType);
                    fd.Run_Data = new dProcess_Data(Procees_HALIGN_Coordinate_Inputs);
                    fd.Traverse_Alignment = true;

                    fd.txt_SC.Text = Start_Chainage.ToString("f3");
                    fd.txt_IN.Text = Interval_Chainage.ToString("f3");
                    fd.Alignment_Data = new List<string>(txt_HDP_HalignData.Lines);
                    fd.Working_Folder = Working_Folder;
                    fd.Owner = this;
                    fd.Show();
                    //if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                    #endregion  Traverse Alignment
                }
                else
                {
                    if (rtb_auto_level_traverse.Lines.Length < 2)
                    {
                        MessageBox.Show("Browse Traverse Data File..", "HEADS",  MessageBoxButtons.OK);
                        using (OpenFileDialog ofd = new OpenFileDialog())
                        {
                            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                            {
                                rtb_auto_level_traverse.Lines = File.ReadAllLines(ofd.FileName);
                            }
                        }
                    }

                    #region Make String Method

                    vdPolyline pl = new vdPolyline(VDoc);
                    pl.setDocumentDefaults();

                    MyList ml = null;
                    foreach (var item in rtb_auto_level_traverse.Lines)
                    {
                        ml = new MyList(MyList.RemoveAllSpaces(item), ' ');

                        try
                        {

                            if(ml.Count == 2)
                                pl.VertexList.Add(new gPoint(ml.GetInt(0), ml.GetInt(1)));
                            else if (ml.Count >= 5)
                                pl.VertexList.Add(new gPoint(ml.GetDouble(1), ml.GetDouble(2), ml.GetDouble(3)));
                        }
                        catch (Exception exx) { }
                    }


                    if (pl.VertexList.Count > 0)
                    {
                        VDoc.ActiveLayOut.Entities.Add(pl);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
                    }

                    //SelectedModel = "DESIGN";
                    //SelectedStrings.Add("M001");
                    SelectedModel = txt_trav_align_model.Text;
                    SelectedStrings.Add(txt_trav_align_string.Text);
                    Start_Chainage = 31150.0;
                    Interval = 10.0;

                    MessageBox.Show("Click on the Polyline, then press right mouse button.", "HEADS", MessageBoxButtons.OK);
                    HeadsFunctions.CreateBoundaryDialog(this, true).ShowDialog();

                    #endregion Make String Method
                }

            }
            //Save_Data();
        }



        void Procees_HALIGN_Coordinate_Inputs(List<string> data)
        {
            RichTextBox txt_HDP_HalignData = rtb_auto_level_halign;

            string fl_name = Path.Combine(Working_Folder, "HALIGN_COORDINATES.TXT");

            File.WriteAllLines(fl_name, data.ToArray());

            List<string> prohal_data = new List<string>(File.ReadAllLines(fl_name));
            Run_Data(fl_name, true);

            fl_name = Path.Combine(Working_Folder, "HALIGNMENT.TXT");

            if (!File.Exists(fl_name)) return;



            List<string> hal_data = new List<string>(File.ReadAllLines(fl_name));
            MyList ml;
            string kStr = "";
            if (hal_data.Count > 0)
            {
                for (int i = 0; i < hal_data.Count; i++)
                {
                    if (hal_data[i].StartsWith("202"))
                    {
                        for (int j = 0; j < prohal_data.Count; j++)
                        {
                            if (prohal_data[j].StartsWith("211"))
                            {
                                kStr = MyList.RemoveAllSpaces(prohal_data[j]);
                                ml = new MyList(kStr, ',');
                                kStr = hal_data[i] + ml.GetString(6);
                                hal_data[i] = kStr;
                                break;
                            }
                        }
                        File.WriteAllLines(fl_name, hal_data.ToArray());
                        break;

                    }

                }
            }

            txt_HDP_HalignData.Lines = File.ReadAllLines(fl_name);

            //tc_auto_level_halign.SelectedTab = tab_auto_level_halign_data;
            cmb_auto_halign.SelectedIndex = 2;
            //VDoc.ActiveLayOut.Entities.RemoveAll();

            //if ((txt_HDP_HalignData.Lines.Length > 0))
            //{
            //    Project.HalignData = new List<string>(txt_HDP_HalignData.Lines);
            //}
            //if (Project.HalignData.Count == 0)
            //{
            //    using (OpenFileDialog ofd = new OpenFileDialog())
            //    {
            //        //ofd.Filter = "Text File (*.txt)|*.txt|HEADS Report File (*.rep)|*.rep|All Files (*.*)|*.*";
            //        ofd.Filter = "HALIGN Data File (*.rep;*.txt)|*.rep;*.txt|All Files (*.*)|*.*";
            //        if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            //        {
            //            Read_Halign_Data(ofd.FileName);
            //        }
            //    }
            //}

            if (Project == null) Project = new HighwayRoadProject(Project_File);
            Project.HalignData = new List<string>(txt_HDP_HalignData.Lines);

            //if (Project.HalignData != null)
            //{
            //    if (Project.HalignData.Count > 0)
            //    {
            //        string inp = Path.Combine(Folder_Current, "Halign_Data.txt");

            //        File.WriteAllLines(inp, Project.HalignData.ToArray());

            //        Set_HEADS_Environment_Variable(inp);
            //        RunExe("HALIGN_Tunnel.EXE", true);
            //    }
            //}

            if (rbtn_bearing_line.Checked)
            {
                RunProjectDataCollection rpcs = new RunProjectDataCollection();

                string an_file = Path.Combine(Working_Folder, "ALIGNMENT.txt");

                if (File.Exists(an_file))
                    rpcs.SetFileList(new List<string>(File.ReadAllLines(an_file)));

                List<string> fl = new List<string>();
                if (rpcs.Count > 2)
                {
                    fl.AddRange(rpcs[1].List_Data);
                    fl.Add("");
                    fl.Add("");
                    fl.Add("");
                    fl.AddRange(rpcs[2].List_Data);
                }
                an_file = Path.Combine(Working_Folder, "TEMP_OFF.txt");
                File.WriteAllLines(an_file, fl.ToArray());
                Run_Data(an_file, true);
            }
            //Read_Start_End_Chainage();

            //design_Profile_Optimization1.Start_Chainage = Start_Chainage;
            //design_Profile_Optimization1.End_Chainage = End_Chainage;

            //design_Section_Schedule1.Start_Chainage = Start_Chainage;
            //design_Section_Schedule1.End_Chainage = End_Chainage;


            //cross_Section_Drawing_HDP.txt_start_chainage.Text = Start_Chainage.ToString("f3");
            //cross_Section_Drawing_HDP.txt_end_chainage.Text = End_Chainage.ToString("f3");

            //profile_Drawing_HDP.txt_start_chainage.Text = Start_Chainage.ToString("f3");
            //profile_Drawing_HDP.txt_end_chainage.Text = End_Chainage.ToString("f3");



            Folder_Copy(Working_Folder, Folder_HDP_Halign);
            Save_Data();
        }


        HighwayRoadProject Project
        {
            get
            {
                return RoadProject.Instance.Project;
            }
            set
            {
                RoadProject.Instance.Project = value;
            }
        }


        #endregion tc_main_Selected

        #region Read HALIGN and VALIGN

        private void Read_Valign_Data()
        {
            string halign_report = "", str = "";
            List<string> HEADS_Data = new List<string>();
            bool flag = false;
            MyList mlist = null;

            #region From File
            halign_report = Path.Combine(Working_Folder, "VALIGNMENT.REP");

            if (!File.Exists(halign_report))
            {
                //MessageBox.Show("VALIGNMENT.REP file not found in the Working Folder.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Read_Valign_Data(halign_report);

            #endregion From File
        }
        private void Read_Valign_Data(string valign_data_file)
        {
            string valign_report = "", str = "";
            List<string> HEADS_Data = new List<string>();
            bool flag = false;
            bool flag2 = false;
            MyList mlist = null;
            #region From File

            valign_report = valign_data_file;
            if (!File.Exists(valign_report))
            {
                return;
            }


            List<string> lst_strs = new List<string>();
            List<string> file_content = new List<string>(File.ReadAllLines(valign_report));
            for (int i = 0; i < file_content.Count; i++)
            {
                str = file_content[i].Trim().TrimEnd().TrimStart().ToUpper();
                str = MyList.RemoveAllSpaces(str).Replace(',', ' ');

                mlist = new MyList(str, ' ');

                if (mlist.StringList.Count == 2)
                {
                    if (mlist.StringList[0] == "300" && mlist.StringList[1].StartsWith("VAL"))
                    {
                        if (lst_strs.Contains(file_content[i + 1]) == false)
                        {
                            flag = true;
                            HEADS_Data.Add("HEADS");
                            HEADS_Data.Add(str);
                            lst_strs.Add(file_content[i + 1]);
                        }
                        continue;
                    }
                }
                if (mlist.StringList.Count == 3)
                {
                    if (mlist.StringList[1] == "300" && mlist.StringList[2].StartsWith("VAL"))
                    {
                        flag = true;

                        HEADS_Data.Add("");
                        HEADS_Data.Add("");
                        HEADS_Data.Add("HEADS");
                        HEADS_Data.Add(mlist.GetString(1).TrimStart());
                        continue;
                        //con
                    }
                }
                else if (flag)
                {
                    if (mlist.StringList.Count == 1)
                    {
                        if (mlist.StringList[0].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                            //break;
                        }
                    }
                    else if (mlist.StringList.Count == 2)
                    {
                        if (mlist.StringList[1].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                            //break;
                        }
                    }
                }
                if (flag)
                {
                    if (mlist.StringList[0].Contains("301") ||
                        mlist.StringList[0].Contains("302") ||
                        mlist.StringList[0].Contains("303"))
                    {
                        HEADS_Data.Add(mlist.GetString(0).TrimStart());
                    }
                    else
                        HEADS_Data.Add(mlist.GetString(1).TrimStart());
                }

            }

            TextBox txt_TNL_Valign_Data = txt_HDP_ValignData;


            if (flag2)
            {
                switch (ProjectType)
                {

                    //case eTypeOfProject.Tunnel:
                    //    Tunnel_Project.Vertical_Alignment = HEADS_Data;
                    //    if (txt_TNL_Valign_Data.Lines.Length == 0)
                    //        txt_TNL_Valign_Data.Lines = HEADS_Data.ToArray();
                    //    break;

                    case eTypeOfProject.Survey_Applications:
                        txt_TNL_Valign_Data.Lines = HEADS_Data.ToArray();
                        break;

                }
                txt_HDP_ValignData.Lines = HEADS_Data.ToArray();
            }
            else
            {
                MessageBox.Show("Valign data is not found in File " + valign_report, "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            #endregion From File
        }
        private List<string> Get_Valign_Data(string valign_data_file)
        {
            string valign_report = "", str = "";
            List<string> HEADS_Data = new List<string>();
            bool flag = false;
            bool flag2 = false;
            MyList mlist = null;
            #region From File
            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "Report Files(*.txt,*.rep)|*.txt;*rep|All Files(*.*)|*.*";
            //    if (ofd.ShowDialog() != DialogResult.Cancel)
            //    {
            valign_report = valign_data_file;
            if (!File.Exists(valign_report))
            {
                return HEADS_Data;
            }


            List<string> file_content = new List<string>(File.ReadAllLines(valign_report));
            for (int i = 0; i < file_content.Count; i++)
            {
                str = file_content[i].Trim().TrimEnd().TrimStart().ToUpper();
                str = MyList.RemoveAllSpaces(str).Replace(',', ' ');

                mlist = new MyList(str, ' ');

                if (mlist.StringList.Count == 2)
                {
                    if (mlist.StringList[0] == "300" && mlist.StringList[1].StartsWith("VAL"))
                    {
                        flag = true;
                        HEADS_Data.Add("HEADS");
                        HEADS_Data.Add(str);
                        continue;
                    }
                }
                if (mlist.StringList.Count == 3)
                {
                    if (mlist.StringList[1] == "300" && mlist.StringList[2].StartsWith("VAL"))
                    {
                        flag = true;

                        HEADS_Data.Add("");
                        HEADS_Data.Add("");
                        HEADS_Data.Add("HEADS");
                        HEADS_Data.Add(mlist.GetString(1).TrimStart());
                        continue;
                    }
                }
                else if (flag)
                {
                    if (mlist.StringList.Count == 1)
                    {
                        if (mlist.StringList[0].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                        }
                    }
                    else if (mlist.StringList.Count == 2)
                    {
                        if (mlist.StringList[1].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                        }
                    }
                }
                if (flag)
                {
                    if (mlist.StringList[0].Contains("301") ||
                        mlist.StringList[0].Contains("302") ||
                        mlist.StringList[0].Contains("303"))
                    {
                        HEADS_Data.Add(mlist.GetString(0).TrimStart());
                    }
                    else
                        HEADS_Data.Add(mlist.GetString(1).TrimStart());
                }

            }
            return HEADS_Data;

            #endregion From File
        }

        private void Read_Halign_Data()
        {
            string halign_report = "", str = "";
            List<string> HEADS_Data = new List<string>();
            bool flag = false;
            MyList mlist = null;
            #region From File
            halign_report = Path.Combine(Working_Folder, "HALIGNMENT.REP");


            if (!File.Exists(halign_report))
            {
                return;
            }

            Read_Halign_Data(halign_report);


            if (ProjectType == eTypeOfProject.Irrigation_Canal)
                uC_Canal_CrossSection1.Chainage_Interval = Interval_Chainage;
            #endregion From File
        }

        private void Read_Halign_Data(string halign_data_file)
        {
            string halign_report = "", str = "";
            List<string> HEADS_Data = new List<string>();
            bool flag = false;
            bool flag2 = false;
            MyList mlist = null;

            #region From File

            halign_report = halign_data_file;

            if (!File.Exists(halign_report))
            {
                return;
            }

            List<string> lst_strs = new List<string>();
            List<string> file_content = new List<string>(File.ReadAllLines(halign_report));
            for (int i = 0; i < file_content.Count; i++)
            {
                str = file_content[i].Trim().TrimEnd().TrimStart().ToUpper();
                str = MyList.RemoveAllSpaces(str).Replace(',', ' ');

                mlist = new MyList(str, ' ');

                if (mlist.StringList.Count == 2)
                {
                    if (mlist.StringList[0] == "200" && mlist.StringList[1].StartsWith("HAL"))
                    {
                        if (lst_strs.Contains(file_content[i + 1]) == false)
                        {
                            flag = true;
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("HEADS");
                            HEADS_Data.Add(str);
                            lst_strs.Add(file_content[i + 1]);
                        }

                        continue;
                    }
                }
                if (mlist.StringList.Count == 3)
                {
                    if (mlist.StringList[1] == "200" && mlist.StringList[2].StartsWith("HAL"))
                    {
                        flag = true;

                        HEADS_Data.Add("");
                        HEADS_Data.Add("HEADS");
                        HEADS_Data.Add(mlist.GetString(1));
                        continue;
                        //con
                    }
                }
                else if (flag)
                {
                    if (mlist.StringList.Count == 1)
                    {
                        if (mlist.StringList[0].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                            //if (ProjectType != eTypeOfProject.Interchange)
                            //    break;
                        }
                    }
                    else if (mlist.StringList.Count == 2)
                    {
                        if (mlist.StringList[1].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                            //if (ProjectType != eTypeOfProject.Interchange)
                            //    break;
                        }
                    }
                }
                if (flag)
                {
                    HEADS_Data.Add(str);
                }

            }
            if (flag2)
            {
                switch (ProjectType)
                {
                    case eTypeOfProject.Survey_Applications:
                        if (rtb_auto_level_halign.Lines.Length == 0)
                            rtb_auto_level_halign.Lines = HEADS_Data.ToArray();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Halign data is not found in File " + halign_report, "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion From File


            //if (ProjectType == eTypeOfProject.RacingTrackDesign)
            {
                MyList_Line ml;
                string kStr = "";
                string sname = "";
                for (int i = 0; i < HEADS_Data.Count; i++)
                {
                    kStr = MyList_Line.RemoveAllSpaces(HEADS_Data[i].Replace("=", " ").Replace(",", " "));
                    //sname = "";
                    if (kStr.StartsWith("201"))
                    {

                        ml = new MyList_Line(kStr, ' ');
                        sname = ml.Find_Value("STR");
                    }
                    else if (kStr.StartsWith("202"))
                    {
                        ml = new MyList_Line(kStr, ' ');
                        //sname = ml.Find_Value("IN");
                        //tab.Add(sname, ml.Find_Value("IN"));
                        Interval_Chainage = ml.Find_Value("IN");
                        break;
                    }
                }
            }

            rtb_SLG_halign.Lines = HEADS_Data.ToArray();

        }

        private List<string> Get_Halign_Data(string halign_data_file)
        {
            string halign_report = "", str = "";
            List<string> HEADS_Data = new List<string>();
            bool flag = false;
            bool flag2 = false;
            MyList mlist = null;
            #region From File

            halign_report = halign_data_file;

            if (!File.Exists(halign_report))
            {
                return HEADS_Data;
            }


            List<string> file_content = new List<string>(File.ReadAllLines(halign_report));
            for (int i = 0; i < file_content.Count; i++)
            {
                str = file_content[i].Trim().TrimEnd().TrimStart().ToUpper();
                str = MyList.RemoveAllSpaces(str).Replace(',', ' ');

                mlist = new MyList(str, ' ');

                if (mlist.StringList.Count == 2)
                {
                    if (mlist.StringList[0] == "200" && mlist.StringList[1].StartsWith("HAL"))
                    {
                        flag = true;
                        HEADS_Data.Add("");
                        HEADS_Data.Add("");
                        HEADS_Data.Add("HEADS");
                        HEADS_Data.Add(str);
                        continue;
                    }
                }
                if (mlist.StringList.Count == 3)
                {
                    if (mlist.StringList[1] == "200" && mlist.StringList[2].StartsWith("HAL"))
                    {
                        flag = true;

                        HEADS_Data.Add("");
                        HEADS_Data.Add("HEADS");
                        HEADS_Data.Add(mlist.GetString(1));
                        continue;
                        //con
                    }
                }
                else if (flag)
                {
                    if (mlist.StringList.Count == 1)
                    {
                        if (mlist.StringList[0].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                            //break;
                        }
                    }
                    else if (mlist.StringList.Count == 2)
                    {
                        if (mlist.StringList[1].StartsWith("FIN"))
                        {
                            HEADS_Data.Add("FINISH");
                            HEADS_Data.Add("");
                            HEADS_Data.Add("");
                            flag = false;
                            flag2 = true;
                            //break;
                        }
                    }
                }
                if (flag)
                {
                    HEADS_Data.Add(str);
                }
            }

            return HEADS_Data;

            #endregion From File
        }
        #endregion Read HALIGN and VALIGN


        #region View Halign
        private void btn_design_horizontal_Click(object sender, EventArgs e)
        {
            if (SelectedStrings == null) SelectedStrings = new List<string>();

            SelectedStrings.Clear();
            SelectedModel = "";


            string file_name = Project_Drawing_File;

            Button btn = sender as Button;

            RichTextBox txt_HDP_HalignData = rtb_auto_level_halign;
            try
            {
                #region HALIGN
                if (btn.Name == btn_halign_view.Name ||
                        btn.Name == btn_SLG_View_Halign.Name)
                {
                    Folder_Current = Working_Folder;
                    //if (cmb_HDP_halign.SelectedIndex == 0 || cmb_HDP_halign.SelectedIndex == 1)
                    Read_Halign_Data();
                    Folder_Copy(Working_Folder, Folder_HDP_Halign);

                    #region Draw Halign

                    Folder_Current = Working_Folder;
                    //Create_Layer("HALIGN");
                    RunProjectDataCollection rpdc = new RunProjectDataCollection();

                    rpdc.SetFileList(new List<string>(txt_HDP_HalignData.Lines));

                    List<CModel> lst_mdl_str = new List<CModel>();

                    //lst_mdl_str.AddRange(rpdc[0].List_Model_String[0].StringName);


                    if (rpdc.Count > 0)
                    {
                        if (rpdc[0].List_Model_String.Count > 0)
                        {
                            lst_mdl_str.Add(new CModel("DESIGN", rpdc[0].List_Model_String[0].StringName, Color.Red));
                            lst_mdl_str.Add(new CModel("DESIGN$", rpdc[0].List_Model_String[0].StringName, Color.White));
                        }
                    }
                    else
                    {
                        lst_mdl_str.Add(new CModel("DESIGN", "M001", Color.Red));
                        lst_mdl_str.Add(new CModel("DESIGN$", "M001", Color.White));
                    }


                    ExecDrawString(lst_mdl_str);

                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign

                }
                else if (btn.Name == btn_halign_chainage_on.Name ||
                        btn.Name == btn_SLG_chn_ON.Name)
                {
                    #region Draw Halign Chainage
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    if (ProjectType != eTypeOfProject.Interchange && ProjectType != eTypeOfProject.Intersection)
                        Create_Layer("Chainage").PenColor = new vdColor(Color.Red);

                    HeadsFunctions.CreateChainageDialog(this).ShowDialog();
                    //HeadsFunctions.CreateDetailsDialog(this).ShowDialog();

                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Chainage
                }
                else if (btn.Name == btn_halign_chainage_off.Name ||
                        btn.Name == btn_SLG_chn_OFF.Name)
                {
                    #region Draw Halign Chainage
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    HeadsFunctions.DeleteChainage(this);
                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Chainage
                }
                else if (btn.Name == btn_halign_details_on.Name ||
                        btn.Name == btn_SLG_dlts_ON.Name)
                {
                    #region Draw Halign Details
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    Create_Layer("Details").PenColor = new vdColor(Color.White);
                    //HeadsFunctions.DeleteDetails(this);
                    HeadsFunctions.CreateDetailsDialog(this).ShowDialog();

                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Details
                }
                else if (btn.Name == btn_halign_details_off.Name ||
                        btn.Name == btn_SLG_dlts_OFF.Name
                    )
                {
                    #region Draw Halign Details
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    HeadsFunctions.DeleteDetails(this);
                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Details
                }
                #endregion HALIGN
            }
            catch (Exception eexx) { }
            Save_Data();

        }

        #endregion View HALIGN

        #region Draw Strings Application
        private void btn_draw_model_strings_Click(object sender, EventArgs e)
        {
            try
            {
                HeadsFunctions.CreateDrawStringDialog(this).ShowDialog();
            }
            catch (Exception exx)
            {
                //throw;
            }
        }

        private void btn_chn_on_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Text.Contains("ON"))
            {
                #region Draw Halign Chainage
                Folder_Current = Working_Folder;
                SelectedStrings.Clear();
                SelectedModel = "DESIGN";
                SelectedStrings.Add("M001");

                //if (ProjectType != eTypeOfProject.Interchange && ProjectType != eTypeOfProject.Intersection)
                //    Create_Layer("Chainage").PenColor = new vdColor(Color.Red);

                HeadsFunctions.CreateChainageDialog(this).ShowDialog();
                //HeadsFunctions.CreateDetailsDialog(this).ShowDialog();

                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
                #endregion Draw Halign Chainage
            }
            else if (btn.Text.Contains("OFF"))
            {
                #region Draw Halign Chainage
                Folder_Current = Working_Folder;
                SelectedModel = "DESIGN";
                SelectedStrings.Add("M001");
                HeadsFunctions.DeleteChainage(this);
                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
                #endregion Draw Halign Chainage
            }
        }

        private void btn_crdn_on_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Text.Contains("ON"))
            {
                #region Draw Halign Chainage
                Folder_Current = Working_Folder;
                SelectedStrings.Clear();

                HeadsFunctions.CreateCoordinateDialog(this).ShowDialog();

                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
                #endregion Draw Halign Chainage
            }
            else if (btn.Text.Contains("OFF"))
            {
                #region Draw Halign Chainage
                Folder_Current = Working_Folder;
                HeadsFunctions.DeleteCoordinates(this);
                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
                #endregion Draw Halign Chainage
            }
        }

        private void btn_dtls_on_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;




            if (btn.Text.Contains("ON"))
            {
                #region Draw Halign Details
                Folder_Current = Working_Folder;
                SelectedModel = "DESIGN";
                SelectedStrings.Add("M001");

                Create_Layer("Details").PenColor = new vdColor(Color.White);
                //HeadsFunctions.DeleteDetails(this);
                HeadsFunctions.CreateDetailsDialog(this).ShowDialog();

                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
                #endregion Draw Halign Details
            }

            else if (btn.Text.Contains("OFF"))
            {
                #region Draw Halign Details
                Folder_Current = Working_Folder;
                SelectedModel = "DESIGN";
                SelectedStrings.Add("M001");

                HeadsFunctions.DeleteDetails(this);
                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
                #endregion Draw Halign Details
            }
        }

        private void rtb_auto_level_halign_TextChanged(object sender, EventArgs e)
        {

        }



        #endregion  Application

        #region Traverse Input Data


        private void btn_transverse_process_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            string fn_nam = Path.Combine(Working_Folder, "Traverse.txt");




            if (btn.Name == btn_transverse_process.Name)
            {
                if (!Directory.Exists(Working_Folder))
                {
                    MessageBox.Show("Project is not created. Please Create a Project.");
                    return;
                }

                int flag = 0;

                if (tc_traverse.SelectedTab == tab_bowditch)
                {
                    flag = 0;
                    fn_nam = Path.Combine(Working_Folder, "Traverse_Bowditch.txt");
                    File.WriteAllLines(fn_nam, rtb_bowditch_data.Lines);
                }
                else if (tc_traverse.SelectedTab == tab_transit)
                {
                    flag = 1;
                    fn_nam = Path.Combine(Working_Folder, "Traverse_Transit.txt");
                    File.WriteAllLines(fn_nam, rtb_transit_data.Lines);
                }
                else if (tc_traverse.SelectedTab == tab_closed_link)
                {
                    flag = 2;
                    fn_nam = Path.Combine(Working_Folder, "Traverse_Closed_Link.txt");
                    File.WriteAllLines(fn_nam, rtb_closed_link_data.Lines);
                }
                else if (tc_traverse.SelectedTab == tab_edm)
                {
                    flag = 3;

                    fn_nam = Path.Combine(Working_Folder, "Traverse_EDM.txt");
                    File.WriteAllLines(fn_nam, rtb_edm_data.Lines);
                }


                Set_HEADS_Environment_Variable(fn_nam);
                //RunExe("Traverse.exe");
                RunExe("Traverse.exe");
                Folder_Current = Working_Folder;

                fn_nam = Path.Combine(Working_Folder, "TRAVERSE.REP");
                if (File.Exists(fn_nam))
                {
                    rtb_transverse_report.Lines = File.ReadAllLines(fn_nam);

                    if (flag == 1) File.WriteAllLines(Path.Combine(Working_Folder, "TRAVERSE_BOWDITCH_REP.TXT"), rtb_transverse_report.Lines);
                    else if (flag == 1) File.WriteAllLines(Path.Combine(Working_Folder, "TRAVERSE_TRANSIT_REP.TXT"), rtb_transverse_report.Lines);
                    else if (flag == 2) File.WriteAllLines(Path.Combine(Working_Folder, "TRAVERSE_CLOSED_LINK_REP.TXT"), rtb_transverse_report.Lines);
                    else if (flag == 3) File.WriteAllLines(Path.Combine(Working_Folder, "TRAVERSE_EDM_REP.TXT"), rtb_transverse_report.Lines);
                    else File.WriteAllLines(Path.Combine(Working_Folder, "TRAVERSE.TXT"), rtb_transverse_report.Lines);
                }
            }
            else if (btn.Name == btn_transverse_draw.Name)
            {
                sc_main.Panel2Collapsed = false;
                sc_traverse.Panel2Collapsed = true;
                sc_traverse.SplitterDistance = 400;


                Folder_Current = Working_Folder;
                //Create_Layer("HALIGN");
                RunProjectDataCollection rpdc = new RunProjectDataCollection();

                rpdc.SetFileList(new List<string>(rtb_bowditch_data.Lines));

                List<CModel> lst_mdl_str = new List<CModel>();

                //lst_mdl_str.AddRange(rpdc[0].List_Model_String[0].StringName);


                if (rpdc.Count > 0)
                {
                    if (rpdc[0].List_Model_String.Count > 0)
                    {
                        lst_mdl_str.Add(new CModel(
                            rpdc[0].List_Model_String[0].ModelName,
                            rpdc[0].List_Model_String[0].StringName,
                            Color.Red));

                        if (rpdc[0].List_Model_String.Count > 1)
                            lst_mdl_str.Add(new CModel(
                            rpdc[0].List_Model_String[1].ModelName,
                            rpdc[0].List_Model_String[1].StringName,
                            Color.Orange));
                    }
                    ExecDrawString(lst_mdl_str);
                }


                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
            }

            else if (btn.Name == btn_transverse_report.Name)
            {
                sc_main.Panel2Collapsed = true;
                sc_traverse.Panel2Collapsed = false;
                sc_traverse.SplitterDistance = 400;
            }
        }




        #endregion Traverse Input Data

        #region Land Record

        string Land_Drawing_Folder 
        {
            get
            {
                return txt_draw_path.Text;
            }
            set
            {
                txt_draw_path.Text = value;
            }
        
        }
        private void btn_Land_drawings_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Working_Folder;
                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    Land_Drawing_Folder = fbd.SelectedPath;
                }
            } 
            Load_Land_Drawing();
        }

        private void Load_Land_Drawing()
        {

            if (Directory.Exists(Land_Drawing_Folder))
            {
                lst_land_drawings.Items.Clear();
                string ext = "";
                foreach (var item in Directory.GetFiles(Land_Drawing_Folder))
                {
                    ext = Path.GetExtension(item.ToLower());
                    if (ext == ".dwg" || ext == ".dxf" || ext == ".vdml")
                    {
                        lst_land_drawings.Items.Add(Path.GetFileName(item));
                    }
                }
            }
        }


        private void lst_land_drawings_SelectedIndexChanged(object sender, EventArgs e)
        {

            string dwg_path = Path.Combine(Land_Drawing_Folder, lst_land_drawings.SelectedItem.ToString());
            
            if (File.Exists(dwg_path)) vdsC_DOC_Land.ActiveDocument.Open(dwg_path);

            vdsC_DOC_Land.View3D_VTOP();
        }
        #endregion Land Record

        #region tc_auto_level_halign
        private void cmb_transverse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.Refresh();
        }

        private void cmb_auto_halign_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_auto_halign.SelectedIndex == 4)
            {
                tc_auto_level_halign.TabPages.Clear();
                tc_auto_level_halign.TabPages.Add(tab_auto_traverse_data);
            }
            else if (cmb_auto_halign.SelectedIndex == 3 || cmb_auto_halign.SelectedIndex == 2)
            {
                tc_auto_level_halign.TabPages.Clear();
                tc_auto_level_halign.TabPages.Add(tab_auto_level_prohalign_data);
                tc_auto_level_halign.TabPages.Add(tab_auto_level_halign_data);
            }
            else
            {
                tc_auto_level_halign.TabPages.Clear();
                tc_auto_level_halign.TabPages.Add(tab_auto_level_halign_data);
            }
            if (cmb_auto_halign.SelectedIndex == 2)
            {
                tc_auto_level_halign.SelectedTab = tab_auto_level_halign_data;
            }
            if (cmb_auto_halign.SelectedIndex == 3)
            {
                tc_auto_level_halign.SelectedTab = tab_auto_level_prohalign_data;
            }
            if (cmb_auto_halign.SelectedIndex == 4)
            {
              
                tc_auto_level_halign.SelectedTab = tab_auto_traverse_data;
            }
        }

        void Clear_Project()
        {
            try
            {
                

                dgv_all_data.Rows.Clear();
                lb_GMT_ModelAndStringName.Items.Clear();

                vdsC_Main.Clear_Screen();
                vdsC_DOC_Land.Clear_Screen();

                uC_DisNet1.Clear();

            }
            catch (Exception exx) { }
        }
        #endregion tc_auto_level_halign

        #endregion Survey Applications



        #region Site Leveling and Grading
        #region HALIGN
        private void btn_SLG_halign_process_Click(object sender, EventArgs e)
        {

            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;

            if (cmb_SLG_halign.SelectedIndex == 0)
            {
                #region  New HALIGN Design

                if (Directory.Exists(Folder_Contour_Data))
                    Folder_Copy(Folder_Contour_Data, Working_Folder);
                else
                {
                    if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                }

                //Folder_Delete(Folder_HDP_Halign, true);

                Folder_Current = Working_Folder;
                //if(Directory.Exists(Folder_Contour_Data))
                //    Folder_Copy(Folder_Contour_Data, Folder_Current);
                //else
                //    Folder_Copy(Folder_Process_Survey_Data, Folder_Current);
                VDoc.SaveAs(Project_Drawing_File);
                //Environment.SetEnvironmentVariable("DESIGN_OPTION", "HALIGN$" + Project_Drawing_File);
                System.Environment.SetEnvironmentVariable("HEADSPROJECT", "");
                Environment.SetEnvironmentVariable("DESIGN_OPTION", "HALIGN$HIP$" + Project_Drawing_File);
                //Environment.SetEnvironmentVariable("DESIGN_OPTION", "HALIGN$HIP$" + Folder_Current);
                Environment.SetEnvironmentVariable("WORKING_FOLDER", Folder_Current);
                RunExe("VIEWER.EXE", false);
                #endregion  New HALIGN Design
            }
            else if (cmb_SLG_halign.SelectedIndex == 1)
            {
                #region  Automatic HALIGN Design Wizard
                #region New HIP Wizard

                Folder_Delete(Folder_HDP_Halign, true);
                if (Directory.Exists(Folder_Contour_Data))
                    Folder_Copy(Folder_Contour_Data, Working_Folder);
                else
                    Folder_Copy(Folder_DTM_Data, Working_Folder);


                Folder_Current = Working_Folder;
                //Folder_Copy(Folder_Process_Survey_Data, Folder_Current);

                //HeadsFunctions.MakeHalignDialog(this, false);
                SelectedModel = "DESIGN";
                SelectedStrings.Add("M001");
                frm_MakeHalignOption fmho = new frm_MakeHalignOption();

                fmho.ShowDialog();

                MessageBox.Show("Please Select a Polyline....", "HEADS", MessageBoxButtons.OK);
                Form frmBoundary = HeadsFunctions.MakeHalignDialog(this, fmho.IsLinear_Polyline);

                if (frmBoundary != null)
                {
                    frmBoundary.Owner = this;
                    frmBoundary.ShowDialog();
                    Read_Halign_Data();
                }
                Folder_Copy(Working_Folder, Folder_HDP_Halign);

                #endregion New HIP Wizard
                #endregion  Automatic HALIGN Design Wizard
            }
            else if (cmb_SLG_halign.SelectedIndex == 2)
            {
                #region  Process HALIGN Design Data

                if (rbtn_auto_level.Checked == false)
                {
                    if (Directory.Exists(Folder_Contour_Data))
                        Folder_Copy(Folder_Contour_Data, Working_Folder);
                    else
                    {
                        if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                    }
                }
                Folder_Current = Working_Folder;




                if ((txt_HDP_HalignData.Lines.Length < 3))
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        //ofd.Filter = "Text File (*.txt)|*.txt|HEADS Report File (*.rep)|*.rep|All Files (*.*)|*.*";
                        ofd.Filter = "HALIGN Data File (*.rep;*.txt)|*.rep;*.txt|All Files (*.*)|*.*";
                        if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                        {
                            Read_Halign_Data(ofd.FileName);
                        }
                    }
                }


                if ((txt_HDP_HalignData.Lines.Length > 3))
                {

                    string inp = Path.Combine(Folder_Current, "Halign_Data.txt");

                    File.WriteAllLines(inp, txt_HDP_HalignData.Lines);

                    Set_HEADS_Environment_Variable(inp);
                    //RunExe("HALIGN_Road.EXE", true);
                    RunExe("HALIGN_Tunnel.EXE", true);
                }

                Folder_Copy(Working_Folder, Folder_HDP_Halign);
                #endregion  Process HALIGN Design Data
            }
            else if (cmb_SLG_halign.SelectedIndex == 3)
            {
                #region  HALIGN Coordinate Inputs [Traverse IPs]
                if (Directory.Exists(Folder_Contour_Data))
                    Folder_Copy(Folder_Contour_Data, Working_Folder);
                else
                {
                    //if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                }

                //Read_Halign_Data
                //Folder_Delete(Folder_HDP_Halign, true);

                Folder_Current = Working_Folder;



                frm_HALIGN_Coordinates fd = new frm_HALIGN_Coordinates(ProjectType);
                fd.Run_Data = new dProcess_Data(Procees_HALIGN_Coordinate_Inputs);

                fd.txt_SC.Text = Start_Chainage.ToString("f3");
                fd.txt_IN.Text = Interval_Chainage.ToString("f3");

                txt_HDP_HalignData = rtb_SLG_prohalign;

                fd.Alignment_Data = new List<string>(txt_HDP_HalignData.Lines);
                fd.Working_Folder = Working_Folder;
                fd.Owner = this;
                fd.Show();
                //if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                #endregion  HALIGN Coordinate Inputs [Traverse IPs]
            }
            else if (cmb_auto_halign.SelectedIndex == 4)
            {
                if (false)
                {
                    #region  Traverse Alignment

                    if (Directory.Exists(Folder_Contour_Data))
                        Folder_Copy(Folder_Contour_Data, Working_Folder);
                    else
                    {
                        //if (!Folder_Copy(Folder_DTM_Data, Working_Folder)) return;
                    }

                    //Read_Halign_Data
                    //Folder_Delete(Folder_HDP_Halign, true);

                    Folder_Current = Working_Folder;



                    frm_HALIGN_Coordinates fd = new frm_HALIGN_Coordinates(ProjectType);
                    fd.Run_Data = new dProcess_Data(Procees_HALIGN_Coordinate_Inputs);
                    fd.Traverse_Alignment = true;

                    fd.txt_SC.Text = Start_Chainage.ToString("f3");
                    fd.txt_IN.Text = Interval_Chainage.ToString("f3");
                    fd.Alignment_Data = new List<string>(txt_HDP_HalignData.Lines);
                    fd.Working_Folder = Working_Folder;
                    fd.Owner = this;
                    fd.Show();
                    //if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                    #endregion  Traverse Alignment
                }
                else
                {
                    if (rtb_SLG_traverse_data.Lines.Length < 2)
                    {
                        MessageBox.Show("Browse Traverse Data File..", "HEADS", MessageBoxButtons.OK);
                        using (OpenFileDialog ofd = new OpenFileDialog())
                        {
                            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                            {
                                rtb_SLG_traverse_data.Lines = File.ReadAllLines(ofd.FileName);
                            }
                        }
                    }

                    #region Make String Method

                    vdPolyline pl = new vdPolyline(VDoc);
                    pl.setDocumentDefaults();

                    MyList ml = null;
                    foreach (var item in rtb_auto_level_traverse.Lines)
                    {
                        ml = new MyList(MyList.RemoveAllSpaces(item), ' ');

                        try
                        {

                            if (ml.Count == 2)
                                pl.VertexList.Add(new gPoint(ml.GetInt(0), ml.GetInt(1)));
                            else if (ml.Count >= 5)
                                pl.VertexList.Add(new gPoint(ml.GetDouble(1), ml.GetDouble(2), ml.GetDouble(3)));
                        }
                        catch (Exception exx) { }
                    }


                    if (pl.VertexList.Count > 0)
                    {
                        VDoc.ActiveLayOut.Entities.Add(pl);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
                    }

                    SelectedModel = "GROUND";
                    SelectedStrings.Add("TRAV");
                    Start_Chainage = 31150.0;
                    Interval = 10.0;

                    MessageBox.Show("Click on the Polyline, then press right mouse button.", "HEADS", MessageBoxButtons.OK);
                    HeadsFunctions.CreateBoundaryDialog(this, true).ShowDialog();

                    #endregion Make String Method
                }

            }
            //Save_Data();
        }
        private void btn_SLG_design_horizontal_Click(object sender, EventArgs e)
        {
            if (SelectedStrings == null) SelectedStrings = new List<string>();

            SelectedStrings.Clear();
            SelectedModel = "";


            string file_name = Project_Drawing_File;

            Button btn = sender as Button;

            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;
            try
            {
                #region HALIGN
                if (btn.Name == btn_halign_view.Name ||
                        btn.Name == btn_SLG_View_Halign.Name)
                {
                    Folder_Current = Working_Folder;
                    //if (cmb_HDP_halign.SelectedIndex == 0 || cmb_HDP_halign.SelectedIndex == 1)
                    Read_Halign_Data();
                    Folder_Copy(Working_Folder, Folder_HDP_Halign);

                    #region Draw Halign
                    Folder_Current = Working_Folder;
                    //Create_Layer("HALIGN");
                    Read_Start_End_Chainage();
                    RunProjectDataCollection rpdc = new RunProjectDataCollection();

                    rpdc.SetFileList(new List<string>(txt_HDP_HalignData.Lines));

                    List<CModel> lst_mdl_str = new List<CModel>();

                    //lst_mdl_str.AddRange(rpdc[0].List_Model_String[0].StringName);


                    if (rpdc.Count > 0)
                    {
                        if (rpdc[0].List_Model_String.Count > 0)
                        {
                            lst_mdl_str.Add(new CModel("DESIGN", rpdc[0].List_Model_String[0].StringName, Color.Red));
                            lst_mdl_str.Add(new CModel("DESIGN$", rpdc[0].List_Model_String[0].StringName, Color.White));
                        }
                    }
                    else
                    {
                        lst_mdl_str.Add(new CModel("DESIGN", "M001", Color.Red));
                        lst_mdl_str.Add(new CModel("DESIGN$", "M001", Color.White));
                    }


                    ExecDrawString(lst_mdl_str);

                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign

                }
                else if (btn.Name == btn_halign_chainage_on.Name ||
                        btn.Name == btn_SLG_chn_ON.Name)
                {
                    #region Draw Halign Chainage
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    if (ProjectType != eTypeOfProject.Interchange && ProjectType != eTypeOfProject.Intersection)
                        Create_Layer("Chainage").PenColor = new vdColor(Color.Red);

                    HeadsFunctions.CreateChainageDialog(this).ShowDialog();
                    //HeadsFunctions.CreateDetailsDialog(this).ShowDialog();

                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Chainage
                }
                else if (btn.Name == btn_halign_chainage_off.Name ||
                        btn.Name == btn_SLG_chn_OFF.Name)
                {
                    #region Draw Halign Chainage
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    HeadsFunctions.DeleteChainage(this);
                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Chainage
                }
                else if (btn.Name == btn_halign_details_on.Name ||
                        btn.Name == btn_SLG_dlts_ON.Name)
                {
                    #region Draw Halign Details
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    Create_Layer("Details").PenColor = new vdColor(Color.White);
                    //HeadsFunctions.DeleteDetails(this);
                    HeadsFunctions.CreateDetailsDialog(this).ShowDialog();

                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Details
                }
                else if (btn.Name == btn_halign_details_off.Name ||
                        btn.Name == btn_SLG_dlts_OFF.Name
                    )
                {
                    #region Draw Halign Details
                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    HeadsFunctions.DeleteDetails(this);
                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign Details
                }
                #endregion HALIGN
            }
            catch (Exception eexx) { }
            Save_Data();

        }
        private void cmb_SLG_halign_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_SLG_halign.SelectedIndex == 4)
            {
                tc_SLG_halign.TabPages.Clear();
                tc_SLG_halign.TabPages.Add(tab_SLG_traverse_data);
            }
            else if (cmb_SLG_halign.SelectedIndex == 3 || cmb_SLG_halign.SelectedIndex == 2)
            {
                tc_SLG_halign.TabPages.Clear();
                tc_SLG_halign.TabPages.Add(tab_SLG_prohalign_data);
                tc_SLG_halign.TabPages.Add(tab_SLG_halign_data);
            }
            else
            {
                tc_SLG_halign.TabPages.Clear();
                tc_SLG_halign.TabPages.Add(tab_SLG_halign_data);
            }


            if (cmb_SLG_halign.SelectedIndex == 2) tc_SLG_halign.SelectedTab = tab_SLG_halign_data;
            if (cmb_SLG_halign.SelectedIndex == 3) tc_SLG_halign.SelectedTab = tab_SLG_prohalign_data;
            if (cmb_SLG_halign.SelectedIndex == 4) tc_SLG_halign.SelectedTab = tab_SLG_traverse_data;

        }


        #endregion HALIGN

        #region Valign

        private void btn_SLG_profile_opt_process_Click(object sender, EventArgs e)
        {

            //Folder_Current = Folder_HDP_Profile_Optimization;
            Folder_Delete(Folder_HDP_Profile_Optimization, true);
            Folder_Copy(Folder_HDP_Halign, Working_Folder);
            Folder_Current = Working_Folder;


            Folder_Copy(Folder_HDP_Halign, Folder_Current);

            string inp = Path.Combine(Folder_Current, "inp.txt");

           

            List<string> list = new List<string>();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            list.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
            list.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));



            list.AddRange(design_Profile_Optimization1.Get_Profile_Optimization_Data().ToArray());

            list.Add(string.Format(""));
            list.Add(string.Format(""));


            File.WriteAllLines(inp, list.ToArray());

            Run_Data(inp, true);


            #region CC


            if (true)
            {

                //design_Profile_Optimization1.Fly_Over_Height = MyList.StringToDouble(txt_Flyover_Height.Text, 0.0);
                //design_Profile_Optimization1.Fly_Over_Start_VCL = MyList.StringToDouble(txt_Flyover_VCL_start.Text, 0.0);
                //design_Profile_Optimization1.Fly_Over_Mid_VCL = MyList.StringToDouble(txt_Flyover_VCL_mid.Text, 0.0);
                //design_Profile_Optimization1.Fly_Over_End_VCL = MyList.StringToDouble(txt_Flyover_VCL_end.Text, 0.0);

                //double slop = MyList.StringToDouble(txt_Flyover_Height.Text, 0.0);


                //design_Profile_Optimization1.Fly_Over_Start_Length = (slop / 2) * 100;
                //design_Profile_Optimization1.Fly_Over_End_Length = (slop / 2) * 100;

                //string file_name = Path.Combine(Folder_Current, "VALIGN_DESIGN_ML01.TXT");
                string fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");

                ValignCollection c_col = new ValignCollection(fname);

                CCrossSection cs = new CCrossSection();

                ChainageDetails chn = new ChainageDetails();



                foreach (var item in Project.All_Cross_Section_Types)
                {
                    if (item.TypeName.StartsWith("TCS05") ||
                        item.TypeName.StartsWith("TCS06"))
                    {
                        chn = new ChainageDetails();
                        chn.CH1 = item.Start_Chainage;
                        chn.CH2 = item.End_Chainage;
                        chn.CSType = eCrossSectionType.FlyOver;
                        cs.LeftMedian.Add(chn);
                    }
                }

                cs.Fly_Over_Height = design_Profile_Optimization1.Fly_Over_Height;
                cs.Fly_Over_End_Length = design_Profile_Optimization1.Fly_Over_End_Length;
                cs.Fly_Over_End_VCL = design_Profile_Optimization1.Fly_Over_End_VCL;
                cs.Fly_Over_Height = design_Profile_Optimization1.Fly_Over_Height;
                cs.Fly_Over_Mid_VCL = design_Profile_Optimization1.Fly_Over_Mid_VCL;
                cs.Fly_Over_Min_Chainage_Distance = design_Profile_Optimization1.Fly_Over_Min_Chainage_Distance;
                cs.Fly_Over_Start_Length = design_Profile_Optimization1.Fly_Over_Start_Length;
                cs.Fly_Over_Start_VCL = design_Profile_Optimization1.Fly_Over_Start_VCL;





                c_col.WriteFlyOver(ref cs);
                fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");
                ValignCollection left_val = c_col;



                Valign val = null;

                int indx = -1;
                //for (int i = 0; i < left_val.Count; i++)
                //{
                //    indx = right_val.IndexOf(left_val[i].Distance);
                //    if (indx != -1 && left_val[i].CurveLength == 150.0)
                //    {
                //        if (Math.Abs(right_val[indx].CurveLength) == Math.Abs(left_val[i].CurveLength))
                //        {
                //            if (Math.Abs(right_val[indx - 1].Height) > Math.Abs(left_val[i - 1].Height))
                //                left_val[i - 1].Height = right_val[indx - 1].Height;
                //            else
                //                right_val[indx - 1].Height = left_val[i - 1].Height;


                //            if (Math.Abs(right_val[indx].Height) > Math.Abs(left_val[i].Height))
                //                left_val[i].Height = right_val[indx].Height;
                //            else
                //                right_val[indx].Height = left_val[i].Height;



                //            if (Math.Abs(right_val[indx + 1].Height) > Math.Abs(left_val[i + 1].Height))
                //                left_val[i + 1].Height = right_val[indx + 1].Height;
                //            else
                //                right_val[indx + 1].Height = left_val[i + 1].Height;

                //        }
                //    }
                //}
                //List<string> l = new List<string>();
                //data.AddRange(left_val.GetHEADSData());
                //data.AddRange(right_val.GetHEADSData());

                txt_HDP_ValignData.Lines = left_val.GetHEADSData().ToArray();

                cmb_HDP_Valign.SelectedIndex = 2;
            }


            #endregion CC


            Folder_Copy(Working_Folder, Folder_HDP_Profile_Optimization);

            Save_Data();
        }


        List<CPTStype> elevated_points { get; set; }

        private List<CPTStype> Get_Model_String_Points(CModel mod_str)
        {
            if (Folder_Current == null)
                Folder_Current = Working_Folder;

            //m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            //m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");

            m_strpathlistfile = Path.Combine(Folder_Current, "model.lst");
            m_strpathfilfile = Path.Combine(Folder_Current, "model.fil");
            //set the test data path    
            string str_title = "";


            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            List<vdPolyline> list_pLine = new List<vdPolyline>();
            vdPolyline p_line = new vdPolyline();


            List<string> strSeleModels = new List<string>();
            List<string> strSeleStrings = new List<string>();

            string progress_title = "Reading Strings...";

            List<CPTStype> cpts = new List<CPTStype>();

            try
            {

                #region ExecDrawString
                List<string> list_text = new List<string>();

                int iCurProgressInPercent = 0;

                bool bIsModel = false;
                bool bIsLabel = false;

                gPoint ptStart = new gPoint();
                gPoint ptEnd = new gPoint();
                gPoint ptLast = new gPoint();
                gPoint ptFirst3dFac = new gPoint();

                CTXTtype txt = new CTXTtype();
                vdLine uline = new vdLine();


                string _model = "";
                string _string = "";

                string strModelName = "";
                int NumPts = 0;
                string str1 = "";
                string stgLabel = "";
                gPoint aPoint = null;
                IHdPolyline3D polyline = null;
                CPTStype pts = new CPTStype();
                List<gPoint> pFArray = new List<gPoint>();

                //this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
                //this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

                //CCfgtype cfg = this.VDoc.ConfigParam;
                CCfgtype cfg = new CCfgtype();

                int iSelectedLineType = 1;

                double dTextHeight = (VDoc.ActiveTextStyle.Height > 0) ? VDoc.ActiveTextStyle.Height : 1.0;

                int p_count = 0;

                string strFilePath = m_strpathfilfile;

                strSeleModels.Add(mod_str.ModelName);
                strSeleStrings.Add(mod_str.StringName);


                if (File.Exists(strFilePath))
                {
                    frm_ProgressBar.ON(progress_title);

                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        try
                        {

                            CLabtype labtype = CLabtype.FromStream(br);


                            #region Model
                            if (labtype.attr == CLabtype.Type.Model) // Model Name
                            {
                                NumPts = 0;
                                strModelName = ((CModType)labtype.Tag).Name;
                                bIsModel = (strSeleModels.Contains(strModelName)) ? true : false;
                            }
                            #endregion Model

                            #region String
                            else if (labtype.attr == CLabtype.Type.String)// String Label
                            {
                                stgLabel = ((CStgType)labtype.Tag).label;

                                list_text.Add(string.Format("{0,12} {1,12} {2,12}", labtype.attr, labtype.attrVal, stgLabel));

                                if (bIsModel)
                                {
                                    bIsLabel = strSeleStrings.Contains(stgLabel);

                                    //p_ln.Label = _model + ":" + _string;
                                    if (bIsLabel)
                                    {
                                        _model = strModelName;
                                        _string = stgLabel;

                                    }
                                }
                            }
                            #endregion String

                            #region Point
                            else if (labtype.attr == CLabtype.Type.Point)
                            {
                                pts = (CPTStype)labtype.Tag;
                                //try
                                //{
                                //    ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -898.0) ? 0.0 : pts.mz));
                                //}
                                //catch (Exception ex)
                                //{
                                //    ptEnd = new gPoint();
                                //}
                                NumPts++;

                                //ptStart = ptLast;
                                //ptLast = ptEnd;

                                if (bIsModel && bIsLabel)
                                {
                                    str1 = stgLabel;
                                    cpts.Add(pts);
                                }
                            }
                            #endregion Point

                            #region Text

                            else if (labtype.attr == CLabtype.Type.Text)
                            {
                                txt = (CTXTtype)labtype.Tag;

                            }
                            #endregion Text

                            #region EndCode

                            else if (labtype.attr == CLabtype.Type.EndCode)
                            {
                                //if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                                //{

                                //}
                                //if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                                //{
                                //    p_ln.Label = _model + ":" + _string;
                                //    //DrawEntity(p_ln, p_ln.Label);
                                //}
                                NumPts = 0;
                            }
                            #endregion EndCode
                            iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);

                            //frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);

                        }
                        catch (Exception ex) { }
                    }

                    frm_ProgressBar.SetValue(br.BaseStream.Length, br.BaseStream.Length);
                    frm_ProgressBar.OFF();

                }
                #endregion ExecDrawString
            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();


            }

            return cpts;

        }

        private void MultiSection_Previous_VALIGN_Data()
        {

            MyList ml = new MyList(cmb_HDP_pro_opt_Bridge_sections.Text, ' ');
            if (elevated_points == null)
            {
                CModel cm = new CModel("DESIGN", cmb_HDP_pro_opt_exist_level_str.Text);
                elevated_points = Get_Model_String_Points(cm);
            }

            List<double> list_chn = new List<double>();

            if (ml.Count > 2)
            {
                dgv_HDP_pro_opt_chns.Rows.Clear();
                for (int i = 0; i < elevated_points.Count; i++)
                {

                    if (elevated_points[i].mc >= ml[0] && elevated_points[i].mc <= ml[2])
                    {
                        if (!list_chn.Contains(elevated_points[i].mc))
                        {
                            list_chn.Add(elevated_points[i].mc);
                            dgv_HDP_pro_opt_chns.Rows.Add(elevated_points[i].mc.ToString("f3"),
                                elevated_points[i].mz.ToString("f3"), "");
                        }
                    }
                }
            }


            #region Previous Data


            List<string> list = new List<string>(txt_HDP_ValignData.Lines);
            dgv_HDP_pro_opt_prev_data.Rows.Clear();
            string kStr = "";
            int indx_1 = -1;
            MyList mlist;
            for (int i = 0; i < list.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(list[i]);
                //kStr = MyList.RemoveAllSpaces(list[i], ' ');
                mlist = new MyList(kStr, ' ');
                if (mlist[0] == "303")
                {
                    if (mlist[1] >= ml[0] && mlist[1] <= ml[2])
                    {
                        if (indx_1 == -1) indx_1 = i;
                        if (indx_1 != -1)
                        {
                            dgv_HDP_pro_opt_prev_data.Rows.Add(mlist[1].ToString(), mlist[2].ToString(), mlist[3].ToString());
                        }
                    }
                    else if (mlist[1] < ml[0])
                    {
                        indx_1 = -1;
                    }
                    else if (mlist[1] > ml[2])
                    {
                        break;
                    }

                }
            }

            #endregion Previous Data
        }
        private void MultiSection_VALIGN_Update(bool Is_Prev_Upd)
        {

            List<string> list = new List<string>(txt_HDP_ValignData.Lines);


            ValignCollection upd = new ValignCollection();

            Valign vl;


            MyList ml;
            string kStr = "";
            if (Is_Prev_Upd)
            {

                for (int i = 0; i < dgv_HDP_pro_opt_prev_data.RowCount - 1; i++)
                {
                    if (dgv_HDP_pro_opt_prev_data[0, i].Value == null) dgv_HDP_pro_opt_prev_data[0, i].Value = "";
                    if (dgv_HDP_pro_opt_prev_data[1, i].Value == null) dgv_HDP_pro_opt_prev_data[1, i].Value = "";
                    if (dgv_HDP_pro_opt_prev_data[2, i].Value == null) dgv_HDP_pro_opt_prev_data[2, i].Value = "";

                    if (dgv_HDP_pro_opt_prev_data[1, i].Value.ToString() != "")
                    {
                        vl = new Valign();

                        vl.Distance = MyList.StringToDouble(dgv_HDP_pro_opt_prev_data[0, i].Value.ToString(), 0.0);
                        vl.Height = MyList.StringToDouble(dgv_HDP_pro_opt_prev_data[1, i].Value.ToString(), 0.0);
                        vl.CurveLength = MyList.StringToDouble(dgv_HDP_pro_opt_prev_data[2, i].Value.ToString(), 150.0);
                        upd.Add(vl);
                    }
                    else if (dgv_HDP_pro_opt_prev_data[1, i].Value.ToString() != "")
                    {
                        vl = new Valign();

                        vl.Distance = MyList.StringToDouble(dgv_HDP_pro_opt_prev_data[0, i].Value.ToString(), 0.0);
                        vl.Height = MyList.StringToDouble(dgv_HDP_pro_opt_prev_data[1, i].Value.ToString(), 0.0);
                        vl.CurveLength = MyList.StringToDouble(dgv_HDP_pro_opt_prev_data[2, i].Value.ToString(), 150.0);
                        upd.Add(vl);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgv_HDP_pro_opt_chns.RowCount - 1; i++)
                {
                    if (dgv_HDP_pro_opt_chns[0, i].Value == null) dgv_HDP_pro_opt_chns[0, i].Value = "";
                    if (dgv_HDP_pro_opt_chns[1, i].Value == null) dgv_HDP_pro_opt_chns[1, i].Value = "";
                    if (dgv_HDP_pro_opt_chns[2, i].Value == null) dgv_HDP_pro_opt_chns[2, i].Value = "";
                    if (dgv_HDP_pro_opt_chns[3, i].Value == null) dgv_HDP_pro_opt_chns[3, i].Value = "";

                    if (dgv_HDP_pro_opt_chns[2, i].Value.ToString() != "")
                    {
                        vl = new Valign();

                        vl.Distance = MyList.StringToDouble(dgv_HDP_pro_opt_chns[0, i].Value.ToString(), 0.0);
                        vl.Height = MyList.StringToDouble(dgv_HDP_pro_opt_chns[2, i].Value.ToString(), 0.0);
                        vl.CurveLength = MyList.StringToDouble(dgv_HDP_pro_opt_chns[3, i].Value.ToString(), 150.0);
                        upd.Add(vl);
                    }
                    else if (dgv_HDP_pro_opt_chns[1, i].Value.ToString() != "")
                    {
                        vl = new Valign();

                        vl.Distance = MyList.StringToDouble(dgv_HDP_pro_opt_chns[0, i].Value.ToString(), 0.0);
                        vl.Height = MyList.StringToDouble(dgv_HDP_pro_opt_chns[1, i].Value.ToString(), 0.0);
                        vl.CurveLength = MyList.StringToDouble(dgv_HDP_pro_opt_chns[3, i].Value.ToString(), 150.0);
                        upd.Add(vl);
                    }
                }
            }


            int indx_1 = -1;
            int indx_2 = -1;
            int cnt = 0;
            List<string> upd_str = new List<string>();
            foreach (var item in upd)
            {
                upd_str.Add(item.ToString());
            }

            for (int i = 0; i < list.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(list[i]);
                //kStr = MyList.RemoveAllSpaces(list[i], ' ');
                ml = new MyList(kStr, ' ');
                if (ml[0] == "303")
                {
                    if (ml[1] >= upd[0].Distance && ml[1] <= upd[upd.Count - 1].Distance)
                    {
                        if (indx_1 == -1) indx_1 = i;
                        if (indx_1 != -1) cnt++;
                    }
                    else if (ml[1] < upd[0].Distance)
                    {
                        indx_1 = -1;
                        indx_2 = -1;
                    }
                    else if (ml[1] > upd[0].Distance)
                    {
                        if (indx_1 != -1)
                        {
                            list.RemoveRange(indx_1, cnt);

                            list.InsertRange(indx_1, upd_str.ToArray());
                            i += upd_str.Count - 1;
                        }
                        indx_1 = -1;
                        indx_2 = -1;
                        cnt = 0;
                    }

                }
            }


            bool find_heads = false;
            cnt = 0;
            indx_1 = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "") continue;
                kStr = MyList.RemoveAllSpaces(list[i]);
                ml = new MyList(kStr, ' ');
                if (ml[0] == "HEADS")
                {
                    find_heads = true; continue;
                }
                else if (ml[0] == "FINISH")
                {
                    find_heads = false;
                    if (indx_1 != -1)
                    {
                        kStr = MyList.RemoveAllSpaces(list[indx_1]);
                        ml = new MyList(kStr, ' ');
                        //list[indx_1] = string.Format("302 VIP 41 SC 304725.00000 EC 308750.00000 IN 5.000");
                        list[indx_1] = string.Format("302 VIP {0} SC {1:f5} EC {2:f5} IN {3:f3}",
                            cnt, ml.GetValue("SC"), ml.GetValue("EC"), ml.GetValue("IN"));
                    }
                    cnt = 0;
                    indx_1 = -1;
                    continue;
                }
                if (find_heads)
                {
                    if (ml[0] == "302")
                    {
                        indx_1 = i;
                    }
                    else if (ml[0] == "303")
                    {
                        cnt++;
                    }
                }
            }

            txt_HDP_ValignData.Lines = list.ToArray();

            MessageBox.Show("VALIGN Data Updated.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_HDP_pro_opt_update_data_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            bool Is_Prev_Upd = false;


            if (btn.Name == btn_HDP_pro_opt_update_data_prev.Name) Is_Prev_Upd = true;
            if (btn.Name == btn_HDP_pro_opt_update_data.Name) Is_Prev_Upd = false;

            MultiSection_VALIGN_Update(Is_Prev_Upd);

        }

        private void btn_HDP_pro_opt_get_exst_lvls_Click(object sender, EventArgs e)
        {

            CModel cm = new CModel("DESIGN", cmb_HDP_pro_opt_exist_level_str.Text);

            elevated_points = Get_Model_String_Points(cm);

            //DesignConfiguration wcg = design_Section_Schedule1.Get_DesignConfiguration_Data();

            //cmb_HDP_pro_opt_Bridge_sections.Items.Clear();
            //for (int i = 0; i < wcg.Schedule_Data.Count; i++)
            //{
            //    if (wcg.Schedule_Data[i][2].ToString().StartsWith("TCS05") || wcg.Schedule_Data[i][2].ToString().StartsWith("TCS06"))
            //    {
            //        cmb_HDP_pro_opt_Bridge_sections.Items.Add(wcg.Schedule_Data[i][0] + " to " + wcg.Schedule_Data[i][1]);
            //    }
            //}

        }

        private void btn_HDP_pro_opt_props_hgt_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_HDP_pro_opt_insert_row.Name)
            {
                int rindx = dgv_HDP_pro_opt_chns.CurrentCell.RowIndex;
                if (rindx != -1) dgv_HDP_pro_opt_chns.Rows.Insert(rindx, "", "", "", "");
            }
            if (btn.Name == btn_HDP_pro_opt_del_row.Name)
            {
                int rindx = dgv_HDP_pro_opt_chns.CurrentCell.RowIndex;
                if (rindx != -1) dgv_HDP_pro_opt_chns.Rows.RemoveAt(rindx);
            }
            else if (btn.Name == btn_HDP_pro_opt_props_hgt.Name)
            {
                double val = 0.0;
                double hgt = MyList.StringToDouble(txt_HDP_pro_opt_props_hgt.Text, 0.0);
                for (int i = 0; i < dgv_HDP_pro_opt_chns.RowCount - 1; i++)
                {
                    val = MyList.StringToDouble(dgv_HDP_pro_opt_chns[1, i].Value.ToString(), 0.0);
                    if (val != 0.0)
                    {
                        dgv_HDP_pro_opt_chns[2, i].Value = (val + hgt).ToString("f3");
                    }
                    val = MyList.StringToDouble(txt_HDP_pro_opt_props_VCL.Text, 0.0);
                    if (val != 0.0)
                    {
                        dgv_HDP_pro_opt_chns[3, i].Value = val.ToString("f3");
                    }
                }
            }
        }

        private void cmb_HDP_pro_opt_Bridge_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiSection_Previous_VALIGN_Data();
        }

        private void btn_HDP_pro_opt_del_row_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int indx = -1;
            try
            {
                if (btn.Name == btn_HDP_pro_opt_insert_row.Name)
                {
                    indx = dgv_HDP_pro_opt_chns.CurrentCell.RowIndex;
                    if (indx != -1)
                    {
                        dgv_HDP_pro_opt_chns.Rows.Insert(indx, "", "", "", "");
                    }
                }
                else if (btn.Name == btn_HDP_pro_opt_insert_row_prev.Name)
                {
                    indx = dgv_HDP_pro_opt_prev_data.CurrentCell.RowIndex;
                    if (indx != -1)
                    {
                        dgv_HDP_pro_opt_prev_data.Rows.Insert(indx, "", "", "");
                    }
                }
                else if (btn.Name == btn_HDP_pro_opt_del_row.Name)
                {
                    indx = dgv_HDP_pro_opt_chns.CurrentCell.RowIndex;
                    if (indx != -1)
                    {
                        dgv_HDP_pro_opt_chns.Rows.RemoveAt(indx);
                    }
                }
                else if (btn.Name == btn_HDP_pro_opt_del_row_prev.Name)
                {
                    indx = dgv_HDP_pro_opt_prev_data.CurrentCell.RowIndex;
                    if (indx != -1)
                    {
                        dgv_HDP_pro_opt_prev_data.Rows.RemoveAt(indx);
                    }
                }
            }
            catch (Exception ex) { }
        }



        private void btn_valign_process_Click(object sender, EventArgs e)
        {

            Folder_Current = Working_Folder;
            //Folder_Copy(Folder_HDP_Ground_Long_Section, Folder_HDP_Vertical_Profile);
            bool is_profile_opt = false;


            //if (Directory.Exists(Folder_HDP_Profile_Optimization))
            //{
            //    //Folder_Copy(Folder_HDP_Profile_Optimization, Folder_Current);
            //    is_profile_opt = true;
            //}
            //else
            //{
            if (rbtn_auto_level.Checked) Folder_Copy(Folder_DTM_Data, Folder_Current);
            else if (Directory.Exists(Folder_HDP_Halign)) Folder_Copy(Folder_HDP_Halign, Folder_Current);
            //}


            Button btn = sender as Button;

            List<string> data = new List<string>();
            string fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");

            if (btn.Name == btn_valign_process.Name)
            {
                #region Create Ground Long Section


               if(ProjectType == eTypeOfProject.Site_Leveling_Grading)
                {

                    if (Directory.Exists(Folder_HDP_Ground_Long_Section))
                        Folder_Copy(Folder_HDP_Ground_Long_Section, Working_Folder);
                    else if (Directory.Exists(Folder_HDP_Profile_Optimization))
                        Folder_Copy(Folder_HDP_Profile_Optimization, Working_Folder);
                    else
                    {
                        if (!Folder_Copy(Folder_HDP_Halign, Working_Folder)) return;
                    }



                    Folder_Delete(Folder_HDP_Vertical_Profile, true);


                    if (txt_HDP_ValignData.Lines.Length == 0)
                        Read_Valign_Data();
                    else
                        Project.ValignData = new List<string>(txt_HDP_ValignData.Lines);

                    if (Project.ValignData != null)
                        if (Project.ValignData.Count > 0)
                            data.AddRange(Project.ValignData.ToArray());

                }

                fname = Path.Combine(Folder_Current, "VALIGN.TXT");

                File.WriteAllLines(fname, data.ToArray());

                Set_HEADS_Environment_Variable(fname);
                RunExe("VALIGN.EXE", true);

                Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);

                return;
                #endregion Create Ground Long Section

            }
            else
            {

                //if(! Folder_Copy(Folder_HDP_Halign, Working_Folder)) return;

                Folder_Delete(Folder_HDP_Ground_Long_Section, true);

                string control_String = "M001";

                fname = Path.Combine(Folder_Current, "GLONGSEC.TXT");


                RunProjectData rpd = new RunProjectData();


                RunProjectDataCollection rpc = new RunProjectDataCollection();

                RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


                rpc.SetFileList(new List<string>(txt_HDP_HalignData.Lines));
                if (rpc.Count > 0)
                {
                    control_String = rpc[0].List_Model_String[0].StringName;
                }



                if (ProjectType == eTypeOfProject.Interchange || ProjectType == eTypeOfProject.Intersection)
                {
                    string EXL_str = "";


                    data.Add(string.Format(""));
                    data.Add(string.Format(""));
                    data.Add(string.Format("HEADS"));
                    data.Add(string.Format("100,DGM"));

                    foreach (var item in rpc)
                    {
                        control_String = item.List_Model_String[0].StringName;
                        EXL_str = control_String.Substring(2);


                        string fst = control_String.Substring(0, 1);

                        int m = 0;

                        if (int.TryParse(EXL_str, out m)) ;

                        if (fst == "M")
                        {
                            EXL_str = "E" + control_String.Substring(1);
                        }
                        else
                        {
                            EXL_str = "E" + fst + m.ToString("00");
                        }


                        data.Add(string.Format("102,DES,MODEL=DESIGN,STRING={0}", control_String));
                        data.Add(string.Format("103,EXL,MODEL=DESIGN,STRING={0}", EXL_str));
                    }

                    //data.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
                    //data.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
                    data.Add(string.Format("FINISH"));
                    data.Add(string.Format(""));
                    data.Add(string.Format(""));

                    File.WriteAllLines(fname, data.ToArray());

                    Set_HEADS_Environment_Variable(fname);
                    RunExe("GLONGSEC.EXE", true);
                }
                else
                {

                    data.Add(string.Format(""));
                    data.Add(string.Format(""));
                    data.Add(string.Format("HEADS"));
                    data.Add(string.Format("100,DGM"));
                    //data.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
                    data.Add(string.Format("102,DES,MODEL=DESIGN,STRING={0}", control_String));
                    data.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
                    data.Add(string.Format("FINISH"));
                    data.Add(string.Format(""));
                    data.Add(string.Format(""));

                    File.WriteAllLines(fname, data.ToArray());

                    Set_HEADS_Environment_Variable(fname);
                    RunExe("GLONGSEC.EXE", true);
                }

                Folder_Copy(Working_Folder, Folder_HDP_Ground_Long_Section);


                return;

            }

            if (data.Count > 0)
            {
                File.WriteAllLines(fname, data.ToArray());
                Run_Data(fname);
            }
            Save_Data();
        }


        private void btn_valign_draw_glongsec_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_HDP_valign_draw_glongsec.Name)
            {
                //VDoc.ActiveLayOut.Entities.RemoveAll();
                #region Draw Valign Profile

                Folder_Current = Working_Folder;
                //if (!Directory.Exists(Folder_Current))
                //    Folder_Copy(Folder_HDP_Profile_Optimization, Folder_Current);

                SelectedModel = "DESIGN";

                SelectedStrings.Clear();
                if (ProjectType != eTypeOfProject.Interchange && ProjectType != eTypeOfProject.Intersection && ProjectType != eTypeOfProject.RacingTrackDesign)
                {
                    SelectedStrings.Add("E001");
                    Create_Layer("E001").PenColor = new vdColor(Color.LightGreen);
                }

                frm_Draw_LSection fdr = new frm_Draw_LSection(this, VDoc);
                fdr.ShowDialog();

                #endregion Draw Valign Profile

                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
            }
            else if (btn.Name == btn_HDP_valign_draw_vertical_profile.Name)
            {

                #region Draw Valign Profile

                Folder_Current = Working_Folder;


                Read_Valign_Data();

                Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);


                SelectedModel = "DESIGN";

                SelectedStrings.Clear();
                if (ProjectType != eTypeOfProject.Interchange && ProjectType != eTypeOfProject.Intersection && ProjectType != eTypeOfProject.RacingTrackDesign)
                {
                    SelectedStrings.Add("M001");
                    Create_Layer("M001").PenColor = new vdColor(Color.Red);
                }
                frm_Draw_LSection fdr = new frm_Draw_LSection(this, VDoc);
                fdr.ShowDialog();


                #endregion Draw Valign Profile

                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
            }

            else if (btn.Name == btn_HDP_valign_draw_selected_profile.Name)
            {
                //VDoc.ActiveLayOut.Entities.RemoveAll();
                #region Draw Valign Profile

                Folder_Current = Working_Folder;

                SelectedModel = "DESIGN";
                SelectedStrings.Clear();


                frm_Draw_LSection fdr = new frm_Draw_LSection(this, VDoc);
                fdr.ShowDialog();

                #endregion Draw Valign Profile

                VDoc.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(VDoc);
            }
        }


        private void btn_HDP_Valign_proceed_Click(object sender, EventArgs e)
        {
            Folder_Current = Working_Folder;
            if (cmb_HDP_Valign.SelectedIndex == 0)
            {
                System.Environment.SetEnvironmentVariable("HEADSPROJECT", "");
                Environment.SetEnvironmentVariable("DESIGN_OPTION", "VALIGN$" + Folder_Current);
                RunExe("VIEWER.EXE", false);
            }
            else if (cmb_HDP_Valign.SelectedIndex == 1)
            {
                Run_HDP_Profile_Optimiation_Data();
            }
            else if (cmb_HDP_Valign.SelectedIndex == 2)
            {
                #region Process VALIGN Data
                //Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);

                if (Directory.Exists(Folder_HDP_Profile_Optimization))
                    Folder_Copy(Folder_HDP_Profile_Optimization, Working_Folder);
                else if (Directory.Exists(Folder_HDP_Ground_Long_Section))
                    Folder_Copy(Folder_HDP_Ground_Long_Section, Working_Folder);
                else
                {
                    if (!Folder_Copy(Folder_HDP_Halign, Working_Folder)) return;
                }



                Folder_Delete(Folder_HDP_Vertical_Profile, true);
                //if (Project.All_Cross_Section_Types.Count == 0)
                //    design_Section_Schedule1.SaveProjectData();

                //cmb_HDP_cs_schedule.Items.Clear();
                //for (int i = 0; i < Project.All_Cross_Section_Types.Count; i++)
                //{
                //    cmb_HDP_cs_schedule.Items.Add(i + 1);
                //}



                if (txt_HDP_ValignData.Lines.Length <= 3)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "VALIGN Data File (*.rep;*.txt)|*.rep;*.txt|All Files (*.*)|*.*";
                        if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                        {
                            Read_Valign_Data(ofd.FileName);
                        }
                    }
                }




                List<string> data = new List<string>();


                string fname = Path.Combine(Folder_Current, "VALIGN_Data.TXT");

                //if (ProjectType == eTypeOfProject.Site_Leveling_Grading ||
                //    ProjectType == eTypeOfProject.Irrigation_Canal
                //    )
                {

                    if (txt_HDP_ValignData.Lines.Length != 0)
                        Project.ValignData = new List<string>(txt_HDP_ValignData.Lines);
                    else
                        return;

                    if (Project.ValignData != null)
                        if (Project.ValignData.Count > 0)
                            data.AddRange(Project.ValignData.ToArray());


                    fname = Path.Combine(Folder_Current, "VALIGN_Data.TXT");

                    if (data.Count == 0)
                    {
                        fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");
                        if (File.Exists(fname))
                        {
                            data.AddRange(File.ReadAllLines(fname));
                        }
                    }

                    if (data.Count == 0)
                    {
                        MessageBox.Show("VALIGN Data not found in the Project File.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    fname = Path.Combine(Folder_Current, "VALIGN_Data.TXT");

                    File.WriteAllLines(fname, data.ToArray());

                    Set_HEADS_Environment_Variable(fname);
                    RunExe("VALIGN.EXE", true);
                }



                Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);

                #endregion Process VALIGN Data
                //Load_Intersection_Schedule();

            }
            else if (cmb_HDP_Valign.SelectedIndex == 3)
            {
                #region VALIGN Profile Input Data
                //Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);

                if (Directory.Exists(Folder_HDP_Profile_Optimization))
                    Folder_Copy(Folder_HDP_Profile_Optimization, Working_Folder);
                else if (Directory.Exists(Folder_HDP_Ground_Long_Section))
                    Folder_Copy(Folder_HDP_Ground_Long_Section, Working_Folder);
                else
                {
                    if (!Folder_Copy(Folder_HDP_Halign, Working_Folder)) return;
                }



                //Folder_Delete(Folder_HDP_Vertical_Profile, true);



                //design_Section_Schedule1.Start_Chainage
                //Start_Chainage = design_Section_Schedule1.Start_Chainage;
                //End_Chainage = design_Section_Schedule1.End_Chainage;
                frm_VALIGN_Profile_Inputs fd = new frm_VALIGN_Profile_Inputs(Start_Chainage, End_Chainage);
                fd.Interval = 25.0;
                fd.Run_VALIGN_Profile_Input_Data = new dProcess_Data(Process_HDP_VALIGN_Profile_Input);
                fd.Owner = this;
                //fd.Parent = this;
                fd.Show();

                #endregion Process VALIGN Data
            }
            Read_Start_End_Chainage();
            Save_Data();
        }
        void Process_HDP_VALIGN_Profile_Input(List<string> data)
        {

            string fl_name = Path.Combine(Working_Folder, "VALIGN_COORDINATES.TXT");

            File.WriteAllLines(fl_name, data.ToArray());

            Run_Data(fl_name, true);

            fl_name = Path.Combine(Working_Folder, "VALIGNMENT.TXT");

            if (!File.Exists(fl_name)) return;
            txt_HDP_ValignData.Lines = File.ReadAllLines(fl_name);

            if (txt_HDP_ValignData.Lines.Length != 0)
                Project.ValignData = new List<string>(txt_HDP_ValignData.Lines);



            //if (Project.ValignData.Count == 0)
            //{
            //    using (OpenFileDialog ofd = new OpenFileDialog())
            //    {
            //        ofd.Filter = "VALIGN Data File (*.rep;*.txt)|*.rep;*.txt|All Files (*.*)|*.*";
            //        if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            //        {
            //            Read_Valign_Data(ofd.FileName);
            //        }
            //    }
            //}



            data = new List<string>();
            if (Project.ValignData != null)
                if (Project.ValignData.Count > 0)
                    data.AddRange(Project.ValignData.ToArray());


            string fname = Path.Combine(Folder_Current, "VALIGN_Data.TXT");

            if (data.Count == 0)
            {
                fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");
                if (File.Exists(fname))
                {
                    data.AddRange(File.ReadAllLines(fname));
                }
            }

            if (data.Count == 0)
            {
                MessageBox.Show("VALIGN Data not found in the Project File.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fname = Path.Combine(Folder_Current, "VALIGN_Data.TXT");

            File.WriteAllLines(fname, data.ToArray());

            Set_HEADS_Environment_Variable(fname);
            RunExe("VALIGN.EXE", true);

            Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);
            Save_Data();

        }

        public void Read_Start_End_Chainage()
        {

            string hal_fil = Path.Combine(Working_Folder, "VALIGN.FIL");


            if (!File.Exists(hal_fil))
                hal_fil = Path.Combine(Working_Folder, "HALIGN.FIL");

            double start_chn = -999.0;
            double end_chn = 0.0;
            if (File.Exists(hal_fil))
            {
                List<string> list = new List<string>(File.ReadAllLines(hal_fil));
                MyList mlist = null;
                foreach (var item in list)
                {
                    mlist = new MyList(MyList.RemoveAllSpaces(item), ' ');

                    if (start_chn == -999.0)
                    {
                        start_chn = mlist.GetDouble(4);
                    }
                    end_chn = mlist.GetDouble(5);
                }
            }
            Start_Chainage = start_chn;
            End_Chainage = end_chn;



            //if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
            //{

                cross_Section_Drawing_HDP.txt_start_chainage.Text = Start_Chainage.ToString("f3");
                cross_Section_Drawing_HDP.txt_end_chainage.Text = End_Chainage.ToString("f3");

                profile_Drawing_HDP.txt_start_chainage.Text = Start_Chainage.ToString("f3");
                profile_Drawing_HDP.txt_end_chainage.Text = End_Chainage.ToString("f3");
                profile_Drawing_HDP.CalculateLength();

            //}


                if (ProjectType == eTypeOfProject.Irrigation_Canal)
                {

                    uC_Canal_CrossSection1.Start_Chainage = Start_Chainage;
                    uC_Canal_CrossSection1.End_Chainage = End_Chainage;

                    uC_Canal_CrossSection1.Chainage_Interval = Interval_Chainage;
                }
                if (ProjectType == eTypeOfProject.Irrigation_Dyke)
                {
                    uC_Dyke_CrossSection1.Start_Chainage = Start_Chainage;
                    uC_Dyke_CrossSection1.End_Chainage = End_Chainage;
                    uC_Dyke_CrossSection1.Chainage_Interval = Interval_Chainage;
                }
        }

        public void Run_HDP_Profile_Optimiation_Data()
        {
            frm_Vertical_Optimization fvo = new frm_Vertical_Optimization();

            if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
            {

                //DesignConfiguration des_cfg = design_Section_Schedule1.Get_DesignConfiguration_Data();
                //double sc = 0.0;
                //double ec = 0.0;
                //if (des_cfg.Schedule_Data.Count > 0)
                //{
                //    sc = des_cfg.Schedule_Data[0][0];
                //    ec = des_cfg.Schedule_Data[0][1];

                //    for (int i = 0; i < des_cfg.Schedule_Data.Count; i++)
                //    {
                //        if (sc > des_cfg.Schedule_Data[i][0])
                //        {
                //            sc = des_cfg.Schedule_Data[i][0];
                //        }
                //        if (ec < des_cfg.Schedule_Data[i][1])
                //        {
                //            ec = des_cfg.Schedule_Data[i][1];
                //        }
                //    }
                //}

                //Start_Chainage = sc;
                //End_Chainage = ec;
            }
            fvo.Start_Chainage = Start_Chainage;
            fvo.End_Chainage = End_Chainage;

            if (fvo.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;


            List<string> data = fvo.Get_Profile_Optimization_Data();

            //Folder_Current = Folder_HDP_Profile_Optimization;
            Folder_Current = Working_Folder;

            //if (!Folder_Copy(Folder_HDP_Halign, Working_Folder)) return;
            Folder_Delete(Folder_HDP_Profile_Optimization, true);


            string inp = Path.Combine(Folder_Current, "Optimised_Data.txt");

            //DesignConfiguration des_confilg = design_Section_Schedule1.Get_DesignConfiguration_Data();
            //design_Section_Schedule1.SaveProjectData();

            //if (ProjectType == eTypeOfProject.HillRoad)
            //{
            //    cmb_HRP_cs_schedule.Items.Clear();
            //    for (int i = 0; i < Project.All_Cross_Section_Types.Count; i++)
            //    {
            //        cmb_HRP_cs_schedule.Items.Add(i + 1);
            //    }
            //}
            //else
            //{
            //    cmb_HDP_cs_schedule.Items.Clear();
            //    for (int i = 0; i < Project.All_Cross_Section_Types.Count; i++)
            //    {
            //        cmb_HDP_cs_schedule.Items.Add(i + 1);
            //    }
            //}

            List<string> list = new List<string>();


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("HEADS"));
            //list.Add(string.Format("100,DGM"));
            //list.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
            //list.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
            //list.Add(string.Format("FINISH"));
            //list.Add(string.Format(""));



            list.AddRange(data.ToArray());

            list.Add(string.Format(""));
            list.Add(string.Format(""));


            File.WriteAllLines(inp, list.ToArray());

            Run_Data(inp, true);


            #region CC


            if (true)
            {
                string fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");

                ValignCollection c_col = new ValignCollection(fname);

                CCrossSection cs = new CCrossSection();

                ChainageDetails chn = new ChainageDetails();



                foreach (var item in Project.All_Cross_Section_Types)
                {
                    if (item.TypeName.StartsWith("TCS05") ||
                        item.TypeName.StartsWith("TCS06"))
                    {
                        chn = new ChainageDetails();
                        chn.CH1 = item.Start_Chainage;
                        chn.CH2 = item.End_Chainage;
                        chn.CSType = eCrossSectionType.FlyOver;
                        cs.LeftMedian.Add(chn);
                    }
                }

                cs.Fly_Over_Height = fvo.Fly_Over_Height;
                cs.Fly_Over_End_Length = fvo.Fly_Over_End_Length;
                cs.Fly_Over_End_VCL = fvo.Fly_Over_End_VCL;
                cs.Fly_Over_Height = fvo.Fly_Over_Height;
                cs.Fly_Over_Mid_VCL = fvo.Fly_Over_Mid_VCL;
                cs.Fly_Over_Min_Chainage_Distance = fvo.Fly_Over_Min_Chainage_Distance;
                cs.Fly_Over_Start_Length = fvo.Fly_Over_Start_Length;
                cs.Fly_Over_Start_VCL = fvo.Fly_Over_Start_VCL;





                c_col.WriteFlyOver(ref cs);
                fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");
                ValignCollection left_val = c_col;



                Valign val = null;

                int indx = -1;

                txt_HDP_ValignData.Lines = left_val.GetHEADSData().ToArray();

                //cmb_HDP_Valign.SelectedIndex = 1;


                //if (data.Count == 0)
                //{
                //    fname = Path.Combine(Folder_Current, "VALIGN_DESIGN_M001.TXT");
                //    if (File.Exists(fname))
                //    {
                //        data.AddRange(File.ReadAllLines(fname));
                //    }
                //}

                //if (data.Count == 0)
                //{
                //    MessageBox.Show("VALIGN Data not found in the Project File.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                fname = Path.Combine(Folder_Current, "VALIGN_Data.TXT");

                File.WriteAllLines(fname, txt_HDP_ValignData.Lines);

                Set_HEADS_Environment_Variable(fname);
                RunExe("VALIGN.EXE", true);

                Folder_Copy(Working_Folder, Folder_HDP_Vertical_Profile);

            }


            #endregion CC


            //Folder_Copy(Working_Folder, Folder_HDP_Profile_Optimization);

            Save_Data();
        }

        private void btn_HDP_Valign_video_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            int indx = -1;
            if (btn.Name == btn_HDP_Valign_video.Name)
            {
                indx = cmb_HDP_Valign.SelectedIndex;
            }
           
            //else if (btn.Name == btn_HWP_valign_video.Name)
            //{
            //    indx = cmb_HWP_Valign.SelectedIndex;
            //}
            string file_name = "";
            switch (indx)
            {
                case 0:
                    file_name = "VALIGN_1.mp4";

                    if (ProjectType == eTypeOfProject.Interchange)
                        file_name = "VALIGN_5.mp4";
                    if (ProjectType == eTypeOfProject.Intersection)
                        file_name = "VALIGN_6.mp4";

                    break;
                case 1:
                    file_name = "VALIGN_2.mp4";
                    break;
                case 2:
                    file_name = "VALIGN_3.mp4";
                    break;
                case 3:
                    file_name = "VALIGN_4.mp4";
                    break;
            }


            string vid_path = Path.GetDirectoryName(Application.StartupPath);

            vid_path = Path.Combine(vid_path, @"Technical Data\Videos\VALIGN Videos");

            string vid_file = Path.Combine(vid_path, file_name);

            if (File.Exists(vid_file)) System.Diagnostics.Process.Start(vid_file);
        }


        private void btn_design_Valign_Click(object sender, EventArgs e)
        {

            SelectedStrings.Clear();
            SelectedModel = "";


            string file_name = Project_Drawing_File;

            Button btn = sender as Button;

            try
            {

                #region VALIGN

                if (btn.Name == btn_HDP_valign_draw_vertical_profile.Name)
                {
                    #region Draw Valign
                    Folder_Delete(Folder_HDP_Vertical_Profile, true);
                    if (Directory.Exists(Folder_HDP_Profile_Optimization))
                        Folder_Copy(Folder_HDP_Profile_Optimization, Working_Folder);
                    else
                        Folder_Copy(Folder_HDP_Halign, Working_Folder);

                    Folder_Current = Working_Folder;

                    IValignFilSelector valignfilselector = HeadsFunctions.CreateValignSelectorDialog(this);

                    if (valignfilselector != null)
                    {
                        Form frmVIPSelector = valignfilselector.DialogInstance;
                        frmVIPSelector.Owner = this;
                        if (frmVIPSelector.ShowDialog() == DialogResult.OK)
                        {
                            Form frmHalignValign = HeadsFunctions.CreateOpenValignmentDialog(this, valignfilselector.SelectedItem);
                            //frmHalignValign.Owner = this;
                            //frmHalignValign.Show();
                        }
                    }


                    Read_Start_End_Chainage();


                    VDoc.Redraw(true);
                    Vdraw.vdCommandAction.View3D_VTop(VDoc);
                    #endregion Draw Halign
                }
                else if (btn.Name == btn_valign_grid_on.Name)
                {
                    #region Draw Valign grid On

                    Folder_Current = Working_Folder;
                    Create_Layer("Grid").PenColor = new vdColor(Color.White);
                    HeadsFunctions.CreateLSecGridDialog(this).ShowDialog();

                    #endregion Draw Valign grid On

                }
                else if (btn.Name == btn_valign_grid_off.Name)
                {
                    #region Draw Valign grid OFF

                    Folder_Current = Working_Folder;
                    HeadsFunctions.DeleteLSecGrid(this);

                    #endregion Draw Valign grid OFF

                }
                else if (btn.Name == btn_valign_details_on.Name)
                {
                    #region Draw Valign details

                    Folder_Current = Working_Folder;
                    SelectedModel = "DESIGN";
                    SelectedStrings.Add("M001");

                    Create_Layer("Details").PenColor = new vdColor(Color.White);
                    HeadsFunctions.CreateVarticalDetailsDialog(this).ShowDialog();

                    #endregion Draw Valign details

                }

                else if (btn.Name == btn_valign_details_off.Name)
                {
                    #region Draw Valign details Off

                    Folder_Current = Working_Folder;
                    //Create_Layer("Details").PenColor = new vdColor(Color.White);
                    HeadsFunctions.DeleteDetails(this);

                    #endregion Draw Valign details Off
                }

                else if (btn.Name == btn_valign_new_vip_design.Name)
                {
                    #region Draw Valign details Off

                    try
                    {
                        Folder_Current = Working_Folder;


                        MessageBox.Show("Select a Polyline for Vertical Alignment Design...");
                        HeadsFunctions.CreateNewValignDialog(this).Show();
                    }
                    catch (Exception exx) { }

                    #endregion Draw Valign details Off
                }
                else if (btn.Name == btn_valign_open_vip_design.Name)
                {
                    Folder_Current = Working_Folder;
                    //Folder_Copy(Folder_HDP_Ground_Long_Section, Folder_Current);
                    Environment.SetEnvironmentVariable("DESIGN_OPTION", "VALIGN$" + Folder_Current);
                    RunExe("VIEWER.EXE", false);
                }
                #endregion HALIGN
            }
            catch (Exception eexx) { }
            Save_Data();

        }

        private void btn_HDP_Elevation_Click(object sender, EventArgs e)
        {

            ValignCollection vc = null;

            if (txt_HDP_ValignData.Lines.Length > 0)
                vc = new ValignCollection(new List<string>(txt_HDP_ValignData.Lines));
            else
                return;


            double elev = MyList.StringToDouble(txt_HDP_Elevation.Text, 0.0);

            if (vc != null)
            {
                for (int i = 1; i < vc.Count - 1; i++)
                {
                    var item = vc[i];
                    item.Height = item.Height + elev;
                }

                txt_HDP_ValignData.Lines = vc.GetHEADSData().ToArray();
            }

        }




        #endregion Valign

        #region Cross Section
        private void btn_cross_section_create_Click(object sender, EventArgs e)
        {
            Folder_Current = Working_Folder;
            string inp = Path.Combine(Folder_Current, "Cross_Section.txt");
            List<string> data = new List<string>();


            ChainageCollection CRS = new ChainageCollection();
            ChainageDetails cdl = new ChainageDetails();

            double start_chainage = 0.0;
            double end_chainage = 0.0;
            if (Directory.Exists(Folder_HDP_Cross_Section))
            {
                if (MessageBox.Show("Delete Previous Data ?", "HEADS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!Folder_Copy(Folder_HDP_Vertical_Profile, Working_Folder))
                    {
                        MessageBox.Show("In STEP 6 Vertical Profile Design was not Processed....cannot Proceed...", "HEADS", MessageBoxButtons.OK);
                        return;
                    }
                }
            }



            Interval_Chainage = 10;




            for (int i = 0; i < txt_HDP_ValignData.Lines.Length; i++)
            {
                string fs = txt_HDP_ValignData.Lines[i].Replace(",", " ").ToUpper();

                fs = MyList_Line.RemoveAllSpaces(fs);

                if (fs == "") continue;

                if (fs.StartsWith("302"))
                {
                    MyList_Line ms = new MyList_Line(fs, ' ');
                    Interval_Chainage = ms.Find_Value("IN");
                    break;
                }
            }


            if (ProjectType == eTypeOfProject.Site_Leveling_Grading ||
                ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
            {

                //data = intersection_Carrigeway1.Get_Offset_Data();
                //data = interchange_CrossSection1.Get_Offset_Data();


                //for
                for (int i = 0; i < dgv_offset_service.RowCount; i++)
                {
                    cdl = new ChainageDetails();
                    cdl.Model = "DESIGN";
                    cdl.RefString = dgv_offset_service[0, i].Value.ToString();
                    cdl.String = dgv_offset_service[1, i].Value.ToString();
                    cdl.CH1 = MyList.StringToDouble(dgv_offset_service[2, i].Value.ToString(), 0.0);
                    cdl.CH2 = MyList.StringToDouble(dgv_offset_service[3, i].Value.ToString(), 0.0);
                    cdl.HO1 = MyList.StringToDouble(dgv_offset_service[4, i].Value.ToString(), 0.0);
                    cdl.HO2 = MyList.StringToDouble(dgv_offset_service[5, i].Value.ToString(), 0.0);
                    cdl.VO1 = MyList.StringToDouble(dgv_offset_service[6, i].Value.ToString(), 0.0);
                    cdl.VO2 = MyList.StringToDouble(dgv_offset_service[7, i].Value.ToString(), 0.0);
                    CRS.Add(cdl);
                    if (i == 0)
                    {
                        start_chainage = cdl.CH1;
                        end_chainage = cdl.CH2;
                    }
                    if (start_chainage > cdl.CH1) start_chainage = cdl.CH1;
                    if (end_chainage < cdl.CH1) end_chainage = cdl.CH2;
                }

                ChainageDetails chn = new ChainageDetails();

                int cc = 0;


                #region Create Median Data

                List<string> left_xsec = new List<string>();
                List<string> right_xsec = new List<string>();
                data = new List<string>();

                data.Add(string.Format(""));
                data.Add(string.Format(""));
                data.Add(string.Format(""));
                data.Add(string.Format("HEADS"));
                data.Add(string.Format("400,OFFSETS"));

                if (CRS.Count > 0)
                {
                    foreach (var item in CRS)
                    {
                        data.AddRange(item.HEADS_Data);

                        if (item.String.Contains("L"))
                        {
                            if (!left_xsec.Contains(item.String))
                                left_xsec.Add(item.String);
                        }
                        else
                        {
                            if (!right_xsec.Contains(item.String))
                                right_xsec.Add(item.String);
                        }
                    }
                }
                data.Add(string.Format("FINISH"));
                data.Add(string.Format(""));
                data.Add(string.Format(""));
                data.Add(string.Format(""));


                #endregion Create Median Data


                data.Add(string.Format(""));
                data.Add(string.Format("//Creating Cross Sections combining all the string serially from Left to Right"));
                data.Add(string.Format(""));

                Start_Chainage = start_chainage;
                End_Chainage = end_chainage;



                data.Add(string.Format(""));
                data.Add(string.Format("HEADS"));
                data.Add(string.Format("500,XSECTIONS,XS01"));
                //list.Add(string.Format("501,MODEL=DESIGN,STRING=M001,CH1={0:f3},CH2=1455.000,INC=10"));
                data.Add(string.Format("501,MODEL=DESIGN,STRING=M001,CH1={0:f3},CH2={1:f3},INC={2:f3}", Start_Chainage, End_Chainage, Interval_Chainage));

                left_xsec.Reverse();

                foreach (var item in left_xsec)
                {
                    data.Add(string.Format("502,LHS,MODEL=DESIGN,STRING={0}", item));
                }
                foreach (var item in right_xsec)
                {
                    data.Add(string.Format("502,RHS,MODEL=DESIGN,STRING={0}", item));
                }
                data.Add(string.Format("504,GRO"));
                data.Add(string.Format("FINISH"));
                data.Add(string.Format(""));
                data.Add(string.Format(""));

            }
            else if (ProjectType == eTypeOfProject.Irrigation_Canal)
            {

                uC_Canal_CrossSection1.Chainage_Interval = Interval_Chainage;
                data = uC_Canal_CrossSection1.Get_Offset_Data();
            }
            else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
            {
                uC_Dyke_CrossSection1.Chainage_Interval = Interval_Chainage;
                data = uC_Dyke_CrossSection1.Get_Offset_Data();
            }
            else
            {
                if (Project.All_Cross_Section_Types.Count == 0)
                {
                    MessageBox.Show("In STEP 5 Schedule Data was not Saved.....cannot Proceed....", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (!Folder_Copy(Folder_HDP_Vertical_Profile, Working_Folder))
                {
                    MessageBox.Show("In STEP 6 Vertical Profile Design was not Processed....cannot Proceed...", "HEADS", MessageBoxButtons.OK);
                    return;
                }
            }

            Folder_Delete(Folder_HDP_Cross_Section, true);

            #region Chiranjit [2015 07 26]
            #endregion Chiranjit [2015 07 26]




            //Read_Cross_Section_Data_HDP();



            #region Define Chainages


            cross_Section_Drawing_HDP.txt_start_chainage.Text = Start_Chainage.ToString("f3");
            cross_Section_Drawing_HDP.txt_end_chainage.Text = End_Chainage.ToString("f3");

            profile_Drawing_HDP.txt_start_chainage.Text = Start_Chainage.ToString("f3");
            profile_Drawing_HDP.txt_end_chainage.Text = End_Chainage.ToString("f3");
            profile_Drawing_HDP.CalculateLength();


            #region Set_Volume_Data

            List<string> list = new List<string>();


            //if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
            {
                if (rbtn_auto_level.Checked)
                {

                    list.Add(string.Format(""));
                    list.Add(string.Format(""));
                    list.Add(string.Format("HEADS"));
                    list.Add(string.Format("700,VOLUME"));
                    list.Add(string.Format("710,EARTHWORK"));
                    list.Add(string.Format("711,INPUT=INTERFACE"));
                    list.Add(string.Format("712,SUB=1000"));
                    list.Add(string.Format("720,MASSHAUL"));
                    //list.Add(string.Format("721,CH1 = 31150.0, CH2 = 34140.0"));
                    list.Add(string.Format("721,CH1={0:f3},CH2={1:f3}", Start_Chainage, End_Chainage));
                    list.Add(string.Format("722,HS = 4000, VS = 60000"));
                    list.Add(string.Format("723,HIN = 500, VIN = 5000"));
                    list.Add(string.Format("724,SL = 841, SW = 594"));
                    list.Add(string.Format("725,LM = 20, RM = 20, TM = 20, BM = 20"));
                    list.Add(string.Format("726,LCO = 4, BCO = 2, TCO = 14, TSI = 6.0"));
                    list.Add(string.Format("FINISH"));
                    list.Add(string.Format(""));
                    list.Add(string.Format(""));
                    list.Add(string.Format(""));

                }
                else if (rbtn_bearing_line.Checked)
                {

                    list.Add(string.Format(""));
                    list.Add(string.Format(""));
                    list.Add(string.Format("HEADS"));
                    list.Add(string.Format("700,VOLUME"));
                    list.Add(string.Format("710,EARTHWORK"));
                    list.Add(string.Format("711,INPUT=INTERFAC"));
                    list.Add(string.Format("712,SUB=100"));
                    list.Add(string.Format("720,MASSHAUL"));
                    //list.Add(string.Format("721,CH1=0.0000,CH2=1864.0000"));
                    list.Add(string.Format("721,CH1={0:f3},CH2={1:f3}", Start_Chainage, End_Chainage));
                    list.Add(string.Format("722,HS=1000,VS=50000"));
                    list.Add(string.Format("723,HIN=500,VIN=1000"));
                    list.Add(string.Format("724,SL=200000,SW=100000"));
                    list.Add(string.Format("725,LM=20,RM=20,TM=20,BM=20"));
                    list.Add(string.Format("726,LCO=1,BCO=251,TCO=7,TSI=6"));
                    list.Add(string.Format("FINISH"));
                    list.Add(string.Format(""));
                    list.Add(string.Format(""));
                    list.Add(string.Format(""));

                }
                else
                {
                    list.Add(string.Format(""));
                    list.Add(string.Format(""));
                    list.Add(string.Format("HEADS"));
                    list.Add(string.Format("700,VOLUME"));
                    list.Add(string.Format("710,EARTHWORK"));
                    list.Add(string.Format("711,INPUT=INTERFAC"));
                    list.Add(string.Format("712,SUB=100"));
                    list.Add(string.Format("720,MASSHAUL"));
                    list.Add(string.Format("721,CH1={0:f3},CH2={1:f3}", Start_Chainage, End_Chainage));
                    list.Add(string.Format("722,HS=1000,VS=50000"));
                    list.Add(string.Format("723,HIN=500,VIN=5000"));
                    list.Add(string.Format("724,SL=200000,SW=100000"));
                    list.Add(string.Format("725,LM=20,RM=20,TM=20,BM=20"));
                    list.Add(string.Format("726,LCO=1,BCO=251,TCO=7,TSI=6"));
                    list.Add(string.Format("FINISH"));
                    list.Add(string.Format(""));
                    list.Add(string.Format(""));

                }

            }

            if (list.Count > 0) Volume_HDP_Data.Lines = list.ToArray();
            #endregion Set_Volume_Data

            data.AddRange(interface_HDP.Get_HEADS_Data().ToArray());
            //Write_Cross_Section_Data_HDP();
            File.WriteAllLines(inp, data.ToArray());
            Run_Data(inp);

            Folder_Copy(Working_Folder, Folder_HDP_Cross_Section);

            RunProjectDataCollection rpdc = new RunProjectDataCollection();


            data.InsertRange(0, txt_HDP_ValignData.Lines);

            data.Add("");

            rpdc.SetFileList(data.ToArray());

            List<CModel> lst = new List<CModel>();

            foreach (var item in rpdc)
            {
                lst.AddRange(item.List_Model_String);
            }
            //}
            //     lst = rpdc[0].List_Model_String;
            //lst.AddRange(rpdc[2].List_Model_String);

            if (lst.Count > 0) plan_Drawing_HDP.Add_Model_String(lst);


            Save_Data();
            return;


            #endregion Chiranjit [2015 07 26]
        }



        Draw_Cross_Sections Cross_Section_Data { get; set; }
        Draw_Pavement_Drawings PavementDrawings { get; set; }

        private void btn_cross_section_draw_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Name == btn_cross_section_create.Name)
            {
                Create_Cross_section_HDP();
                Select_HDP_CS_Drawing();
                return;

            }
            else if (btn.Name == btn_cross_section_draw.Name)
            {
                Folder_Current = Working_Folder;

                //col_data.Draw_Cross_Section(VDoc);


                #region btn_HWP_cross_section_create

                Draw_from_Model();

                dplc.DrawFormat = DrawAs.Polyline;

                Setting st = null;

                List<string> stgs = new List<string>();




                stgs.Add("CL01");
                stgs.Add("CR01");
                stgs.Add("CL02");
                stgs.Add("CR02");
                stgs.Add("CL03");
                stgs.Add("CR03");
                stgs.Add("CL04");
                stgs.Add("CR04");
                stgs.Add("CL05");
                stgs.Add("CR05");
                stgs.Add("CL06");
                stgs.Add("CR06");
                stgs.Add("CL07");
                stgs.Add("CR07");

                foreach (var item in stgs)
                {
                    Delete_Layer_Items(item);
                    st = new Setting(item, "POLYLINE", item);
                    st.Layer_Color = Color.White;
                    dplc.DataToDrawing(vdsC_Main.ActiveDocument, st);
                }



                stgs.Clear();
                stgs.Add("I001");
                stgs.Add("I002");

                foreach (var item in stgs)
                {
                    Delete_Layer_Items(item);
                    st = new Setting(item, "POLYLINE", item);
                    st.Layer_Color = Color.DarkGreen;
                    dplc.DataToDrawing(vdsC_Main.ActiveDocument, st);
                }


                stgs.Clear();
                stgs.Add("LTOE_I001");
                stgs.Add("RTOE_I001");

                foreach (var item in stgs)
                {
                    Delete_Layer_Items(item);
                    st = new Setting(item, "POLYLINE", item);
                    st.Layer_Color = Color.LightGreen;
                    dplc.DataToDrawing(vdsC_Main.ActiveDocument, st);
                }
                #endregion btn_HWP_cross_section_create
            }

            return;










            List<string> list = new List<string>();

            list.Add("M001");
            list.Add("CL01");
            list.Add("CR01");
            list.Add("CL02");
            list.Add("CR02");
            list.Add("CL03");
            list.Add("CR03");
            list.Add("CL04");
            list.Add("CR04");
            list.Add("CL05");
            list.Add("CR05");
            list.Add("CL06");
            list.Add("CR06");
            list.Add("CL07");
            list.Add("CR07");
            list.Add("I001");
            list.Add("I002");
            ExecDrawString("DESIGN", list);
        }

        private void Create_Cross_section_HDP()
        {
            Folder_Current = Working_Folder;

            Cross_Section_Data = new Draw_Cross_Sections(Folder_Current);
            Cross_Section_Data.Read_HDS003_Data();
            Cross_Section_Data.Read_HDS002_Data();

            List<double> chns = Cross_Section_Data.Get_All_Chainages();
            List<string> stgs = Cross_Section_Data.Get_All_Strings();


            string inp_fl = "";


            frm_ProgressBar.ON("Drawings are Creating...");
            int cnt = 0;
            foreach (var st in stgs)
            {
                foreach (var ch in chns)
                {
                    inp_fl = Path.Combine(Folder_Current, "SECTION_DRGS");
                    if (!Directory.Exists(inp_fl)) Directory.CreateDirectory(inp_fl);

                    inp_fl = Path.Combine(inp_fl, "CS_" + st + "_" + ch.ToString() + ".VDML");

                    Cross_Section_Data.Get_Data(ch, st).Draw_Cross_Section(vdsC_HDP_CrossSect.ActiveDocument);
                    vdsC_HDP_CrossSect.ActiveDocument.Redraw(true);
                    vdsC_HDP_CrossSect.ActiveDocument.SaveAs(inp_fl);
                    frm_ProgressBar.SetValue(cnt++, stgs.Count * chns.Count);
                }
            }
            frm_ProgressBar.OFF();

            cmb_HDP_cs_sel_chainage.Items.Clear();
            cmb_HDP_cs_sel_strings.Items.Clear();

            foreach (var item in Cross_Section_Data.Get_All_Chainages())
            {
                cmb_HDP_cs_sel_chainage.Items.Add(item);
            }
            foreach (var item in Cross_Section_Data.Get_All_Strings())
            {
                cmb_HDP_cs_sel_strings.Items.Add(item);
            }

            if (cmb_HDP_cs_sel_chainage.Items.Count > 0)
            {
                cmb_HDP_cs_sel_chainage.SelectedIndex = 0;
            }
            if (cmb_HDP_cs_sel_strings.Items.Count > 0)
            {
                cmb_HDP_cs_sel_strings.SelectedIndex = 0;
            }
        }
        private void Select_HDP_CS_Drawing()
        {

            Folder_Current = Working_Folder;
            string drg_fl = Path.Combine(Folder_Current, @"SECTION_DRGS\CS_" + cmb_HDP_cs_sel_strings.Text + "_" + cmb_HDP_cs_sel_chainage.Text + ".VDML");
            
            if (File.Exists(drg_fl))
            {
                vdsC_HDP_CrossSect.ActiveDocument.Open(drg_fl);

                vdsC_HDP_CrossSect.ActiveDocument.Redraw(true);
                //Vdraw.vdCommandAction.View3D_VTop(VDoc);
                return;
            }
        }




        private void tsmi_HWP_cs_First_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = sender as ToolStripButton;
            //tmr_Cross_section.Interval = (MyList.StringToInt(tscmb_HWP_cs_Time.Text, 1) * 1000);
            if (tsb.Name == tsmi_HDP_cs_backword.Name)
            {
                DrawIncrement = -1;
                //tmr_Cross_section.Start();
            }
            if (tsb.Name == tsmi_HDP_cs_Forward.Name)
            {
                DrawIncrement = 1;
                //tmr_Cross_section.Start();
            }
            if (tsb.Name == tsmi_HDP_cs_First.Name)
            {
                if (cmb_HDP_cs_sel_chainage.Items.Count > 0)
                    cmb_HDP_cs_sel_chainage.SelectedIndex = 0;
                return;
            }
            if (tsb.Name == tsmi_HDP_cs_Last.Name)
            {
                if (cmb_HDP_cs_sel_chainage.Items.Count > 0)
                    cmb_HDP_cs_sel_chainage.SelectedIndex = cmb_HDP_cs_sel_chainage.Items.Count - 1;
                return;
            }
            Cross_section_Display();
        }
        int DrawIncrement = 1;

        private void Cross_section_Display()
        {
            try
            {
                //if (ProjectType == eTypeOfProject.Site_Leveling_Grading ||)
                {
                    if (cmb_HDP_cs_sel_chainage.SelectedIndex + DrawIncrement >= cmb_HDP_cs_sel_chainage.Items.Count)
                        cmb_HDP_cs_sel_chainage.SelectedIndex = 0;
                    if (cmb_HDP_cs_sel_chainage.SelectedIndex + DrawIncrement < 0)
                        cmb_HDP_cs_sel_chainage.SelectedIndex = cmb_HDP_cs_sel_chainage.Items.Count - 1;
                    else
                        cmb_HDP_cs_sel_chainage.SelectedIndex = cmb_HDP_cs_sel_chainage.SelectedIndex + DrawIncrement;
                }
            }
            catch (Exception exx) { }
        }


        private void dgv_offset_service_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }


        private void cmb_HDP_cs_sel_chainage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_HDP_CS_Drawing();
        }

        #endregion Cross Section

        #region Volume

        private void btn_HDP_process_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            bool Is_Volume = false;
            string inp = Path.Combine(Working_Folder, "inp.txt");

            List<string> data = new List<string>();

            if (btn.Name == btn_process_diagram_HDP.Name)
            {
                Folder_Current = Folder_HDP_Drawings;

                inp = Path.Combine(Working_Folder, "diagrams.txt");

                halign_Diagram_HDP.btn_Save_Click();
                valign_Diagram_HDP.btn_Save_Click();
                data.AddRange(halign_Diagram_HDP.HEADS_Data);
                data.Add("");
                data.Add("");
                data.AddRange(valign_Diagram_HDP.HEADS_Data);
                data.Add("");
                data.Add("");

                File.WriteAllLines(inp, data.ToArray());
            }
            else if (btn.Name == btn_HDP_volume_process.Name)
            {
                Is_Volume = true;

                //if (ProjectType == eTypeOfProject.Site_Leveling_Grading)
                {
                    Folder_Current = Folder_HDP_Volume;

                    if (Directory.Exists(Folder_HDP_Pavement_Layers))
                        Folder_Copy(Folder_HDP_Pavement_Layers, Working_Folder);
                    else
                    {
                        if (!Folder_Copy(Folder_HDP_Cross_Section, Working_Folder)) return;
                    }

                    Folder_Delete(Folder_HDP_Volume, true);

                    inp = Path.Combine(Working_Folder, "Volume.txt");

                    File.WriteAllLines(inp, Volume_HDP_Data.Lines);

                }
            }
            else if (btn.Name == btn_HDP_volume_masshaul.Name)
            {
                string p_ma = "";
                Folder_Current = Working_Folder;
                p_ma = Path.Combine(Folder_Current, "MASSHAUL.DRG");

                if (File.Exists(p_ma))
                    HeadsFunctions.ImportFromDrgFile(this, p_ma);
                vdsC_Volume.ActiveDocument.Redraw(true);
                Vdraw.vdCommandAction.View3D_VTop(vdsC_Volume.ActiveDocument); return;

            }

            else if (btn.Name == btn_HDP_plan_process.Name)
            {
                Folder_Current = Folder_HDP_Drawings;

                inp = Path.Combine(Working_Folder, "Plan.txt");

                plan_Drawing_HDP.btn_Save_Click(sender, e);
                data = plan_Drawing_HDP.HEADS_Data;
                File.WriteAllLines(inp, data.ToArray());
            }
            else if (btn.Name == btn_HDP_profile_process.Name)
            {
                Folder_Current = Folder_HDP_Drawings;

                inp = Path.Combine(Working_Folder, "Profile.txt");

                profile_Drawing_HDP.btn_Save_Click(sender, e);
                data = profile_Drawing_HDP.HEADS_Data;
                File.WriteAllLines(inp, data.ToArray());
            }
            else if (btn.Name == btn_HDP_cross_process.Name)
            {
                Folder_Current = Folder_HDP_Drawings;

                inp = Path.Combine(Working_Folder, "Xsection.txt");
                cross_Section_Drawing_HDP.btn_save_Click(sender, e);
                data = cross_Section_Drawing_HDP.HEADS_Data;
                File.WriteAllLines(inp, data.ToArray());

            }
            Run_Data(inp, true);

            Folder_Copy(Working_Folder, Folder_Current);
            Folder_Current = Working_Folder;

            if (Is_Volume)
            {
                string rep_file = Path.Combine(Path.GetDirectoryName(inp), "VOLUME.REP");

                if (File.Exists(rep_file))
                {
                    Volume_HDP_Report.Lines = File.ReadAllLines(rep_file);
                }
            }
            Save_Data();
        }

        #endregion Volume



        #region Save Data
        private void Save_Site_Leveling_Grading_Project_File()
        {

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");
            txt_HDP_file.Text = Project_File;

            #region Chiranjit[2015 05 28]
            #endregion
            List<string> list = new List<string>();

            string str = "";

            #region Chiranjit [2015 06 16]


            if (RoadProject.Instance.Project == null) return;

            RoadProject.Instance.Project.SurveyFile = Survey_File;
            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


            #region Select Survey File
            list.Clear();
            #region Banner

            list.Add("");
            list.Add("");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***                    HEADS SITE                          ***");
            list.Add("\t\t\t***            TECHSOFT ENGINEERING SERVICES               ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***              SITE LEVELING AND GRADING                 ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add(string.Format("\t\t\t***          DATE : {0}/{1}/{2}    TIME : {3}:{4}:{5}          ***",
                DateTime.Now.Day.ToString("00"),
                DateTime.Now.Month.ToString("00"),
                DateTime.Now.Year.ToString("0000"),
                DateTime.Now.Hour.ToString("00"),
                DateTime.Now.Minute.ToString("00"),
                DateTime.Now.Second.ToString("00")));
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("");
            list.Add("");

            #endregion Banner

            if (rbtn_auto_level.Checked)
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                halign_Diagram_HDP.btn_Save_Click();

                //list.Add(string.Format("//Creating Horizontal alignment Diagram to show in Profile Drawing"));
                //list.Add(string.Format(""));
                //list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
                //list.Add(string.Format(""));
                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            #region Select Survey File

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Total_Station_Data;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Formating Data into DGM / GENIO Format"));
                //list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> SURVEY.txt"));
                list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> {0}", Path.GetFileName(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Processing DGM / GENIO Data file"));
                list.Add(string.Format(""));
                //list.Add(string.Format("DGM >> Modeling from DGM File >> SURVEY.GEN"));
                list.Add(string.Format("DGM >> Modeling from DGM File >> {0}.GEN", Path.GetFileNameWithoutExtension(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else if (rbtn_auto_level.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Auto_Level_Data;

                str = "UTILITIES >> FORMAT CROSS SECTION DATA DGM >> " + Path.GetFileName(Survey_File);
                list.Add(str);
                list.Add("");
                list.Add("");
                str = "DGM  >> Modeling from CROSS SECTION DGM File >>  CROSSECT.DGM";
                list.Add(str);
                list.Add("");
                list.Add("");
            }



            //if (chk_utm_conversion.Checked)
            //{
            //    list.Add(string.Format(""));


            //    //list.AddRange(utM_Conversion1.HEADS_Data.ToArray());

            //    list.Add(string.Format("Transform Ground Survey data from TM (Tranverse Markator) to UTM (Unversal Tranverse Markator)"));
            //    utM_Conversion1.TM_to_UTM_Conversion();
            //    Project.TM_to_UTM_Data = utM_Conversion1.HEADS_Data;
            //    list.AddRange(utM_Conversion1.HEADS_Data);
            //}

            #endregion Select Survey File

            #region Creating Ground Modeling Triangulation

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list = new List<string>();
            list.Add(string.Format("//Creating Ground Modeling Triangulation"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            list.Add(string.Format("101,BOU,MODEL={0},STRING={1}", txt_GMT_model.Text, txt_GMT_string.Text));
            list.Add(string.Format("FINISH"));

            Project.Triangulation_Data = list;

            list.Add(string.Format(""));

            #endregion Creating Ground Modeling Triangulation

            #region Creating Contour

            //list = new List<string>();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Contour"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0"));
            //list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING=ELEV,INC=5.0,TSI=5"));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_pri_string.Text, txt_Contour_pri_inc.Text));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_sec_string.Text, txt_Contour_sec_inc.Text));
            list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING={0},INC={1},TSI=5", txt_Contour_ele_string.Text, txt_Contour_ele_inc.Text));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Project.Contour_Data = list;

            #endregion Creating Contour

            //Project.S

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                if (rbtn_bearing_line.Checked)
                {
                    string temp_off = Path.Combine(Working_Folder, "TEMP_OFF.TXT");
                    if (File.Exists(temp_off))
                    {
                        list.AddRange(File.ReadAllLines(temp_off));
                    }
                }


                halign_Diagram_HDP.btn_Save_Click();

                //list.Add(string.Format(""));
                //list.Add(string.Format(""));
                //list.Add(string.Format("//Creating Horizontal alignment Diagram to show in Profile Drawing"));
                //list.Add(string.Format(""));
                //list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
                //list.Add(string.Format(""));
                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
            }




            #region Ground Long Section
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            string fname = Path.Combine(Folder_Current, "GLONGSEC.TXT");
            if (File.Exists(fname))
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section"));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(fname));

            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section as String E001 following Sring M001"));
                list.Add(string.Format(""));
                list.Add(string.Format("HEADS"));
                list.Add(string.Format("100,DGM"));
                list.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
                list.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
                list.Add(string.Format("FINISH"));
            }
            #endregion Ground Long Section
            list.Add(string.Format(""));

            #region Vertical Alignment


            Read_Valign_Data();

            list.Add(string.Format(""));
            list.Add(string.Format("//Design of Vertical Alignment"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Project.ValignData.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            valign_Diagram_HDP.btn_Save_Click();
            list.Add(string.Format(""));
            //list.Add(string.Format("//Creating Vertical Alignment Diagram to show in Profile Drawing"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion Vertical Alignment



            string inp_file = Path.Combine(Folder_HDP_Cross_Section, "Cross_Section.txt");


            if (File.Exists(inp_file))
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(inp_file));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            //pavement_HDP.btn_OK_Click();

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.AddRange(pavement_HDP.HEADS_Data);
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Volume_HDP_Data.Lines);
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            plan_Drawing_HDP.btn_Save_Click();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(plan_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            profile_Drawing_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(profile_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            cross_Section_Drawing_HDP.btn_save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(cross_Section_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion Chiranjit [2015 06 16]

            string proj_file = Project_File;
            txt_HDP_file.Text = Project_File;




            rtb_project_data.Lines = list.ToArray();

            File.WriteAllLines(Project_File, list.ToArray());

            Save_Data();

            MessageBox.Show(this, "Project file created as " + Project_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion Chiranjit[2015 05 28]
        }
        private void Save_Irrigation_Canal_Project_File()
        {

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");
            txt_HDP_file.Text = Project_File;

            #region Chiranjit[2015 05 28]
            #endregion
            List<string> list = new List<string>();

            string str = "";

            #region Chiranjit [2015 06 16]


            if (RoadProject.Instance.Project == null) return;

            RoadProject.Instance.Project.SurveyFile = Survey_File;
            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


            #region Select Survey File
            list.Clear();
            #region Banner

            list.Add("");
            list.Add("");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***                    HEADS SITE                          ***");
            list.Add("\t\t\t***            TECHSOFT ENGINEERING SERVICES               ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***               IRRIGATION CANAL DESIGN                  ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add(string.Format("\t\t\t***          DATE : {0}/{1}/{2}    TIME : {3}:{4}:{5}          ***",
                DateTime.Now.Day.ToString("00"),
                DateTime.Now.Month.ToString("00"),
                DateTime.Now.Year.ToString("0000"),
                DateTime.Now.Hour.ToString("00"),
                DateTime.Now.Minute.ToString("00"),
                DateTime.Now.Second.ToString("00")));
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("");
            list.Add("");

            #endregion Banner

            if (rbtn_auto_level.Checked)
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                halign_Diagram_HDP.btn_Save_Click();

                //list.Add(string.Format("//Creating Horizontal alignment Diagram to show in Profile Drawing"));
                //list.Add(string.Format(""));
                //list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
                //list.Add(string.Format(""));
                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            #region Select Survey File

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Total_Station_Data;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Formating Data into DGM / GENIO Format"));
                //list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> SURVEY.txt"));
                list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> {0}", Path.GetFileName(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Processing DGM / GENIO Data file"));
                list.Add(string.Format(""));
                //list.Add(string.Format("DGM >> Modeling from DGM File >> SURVEY.GEN"));
                list.Add(string.Format("DGM >> Modeling from DGM File >> {0}.GEN", Path.GetFileNameWithoutExtension(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else if (rbtn_auto_level.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Auto_Level_Data;

                str = "UTILITIES >> FORMAT CROSS SECTION DATA DGM >> " + Path.GetFileName(Survey_File);
                list.Add(str);
                list.Add("");
                list.Add("");
                str = "DGM  >> Modeling from CROSS SECTION DGM File >>  CROSSECT.DGM";
                list.Add(str);
                list.Add("");
                list.Add("");
            }



            //if (chk_utm_conversion.Checked)
            //{
            //    list.Add(string.Format(""));


            //    //list.AddRange(utM_Conversion1.HEADS_Data.ToArray());

            //    list.Add(string.Format("Transform Ground Survey data from TM (Tranverse Markator) to UTM (Unversal Tranverse Markator)"));
            //    utM_Conversion1.TM_to_UTM_Conversion();
            //    Project.TM_to_UTM_Data = utM_Conversion1.HEADS_Data;
            //    list.AddRange(utM_Conversion1.HEADS_Data);
            //}

            #endregion Select Survey File

            #region Creating Ground Modeling Triangulation

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list = new List<string>();
            list.Add(string.Format("//Creating Ground Modeling Triangulation"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            list.Add(string.Format("101,BOU,MODEL={0},STRING={1}", txt_GMT_model.Text, txt_GMT_string.Text));
            list.Add(string.Format("FINISH"));

            Project.Triangulation_Data = list;

            list.Add(string.Format(""));

            #endregion Creating Ground Modeling Triangulation

            #region Creating Contour

            //list = new List<string>();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Contour"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0"));
            //list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING=ELEV,INC=5.0,TSI=5"));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_pri_string.Text, txt_Contour_pri_inc.Text));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_sec_string.Text, txt_Contour_sec_inc.Text));
            list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING={0},INC={1},TSI=5", txt_Contour_ele_string.Text, txt_Contour_ele_inc.Text));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Project.Contour_Data = list;

            #endregion Creating Contour

            //Project.S

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                if (rbtn_bearing_line.Checked)
                {
                    string temp_off = Path.Combine(Working_Folder, "TEMP_OFF.TXT");
                    if (File.Exists(temp_off))
                    {
                        list.AddRange(File.ReadAllLines(temp_off));
                    }
                }


                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
            }




            #region Ground Long Section
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            string fname = Path.Combine(Folder_Current, "GLONGSEC.TXT");
            if (File.Exists(fname))
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section"));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(fname));

            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section as String E001 following Sring M001"));
                list.Add(string.Format(""));
                list.Add(string.Format("HEADS"));
                list.Add(string.Format("100,DGM"));
                list.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
                list.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
                list.Add(string.Format("FINISH"));
            }
            #endregion Ground Long Section
            list.Add(string.Format(""));

            #region Vertical Alignment


            Read_Valign_Data();

            list.Add(string.Format(""));
            list.Add(string.Format("//Design of Vertical Alignment"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Project.ValignData.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            valign_Diagram_HDP.btn_Save_Click();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Vertical Alignment Diagram to show in Profile Drawing"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion Vertical Alignment



            string inp_file = Path.Combine(Folder_HDP_Cross_Section, "Cross_Section.txt");


            if (File.Exists(inp_file))
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(inp_file));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            //pavement_HDP.btn_OK_Click();

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.AddRange(pavement_HDP.HEADS_Data);
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Volume_HDP_Data.Lines);
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            halign_Diagram_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//CREATING DRAWINGS FOR DETAILS OF HORIZONTAL ALIGNMENT:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("//CREATING DRAWINGS FOR DETAILS OF VERTICAL ALIGNMENT:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            valign_Diagram_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));




            plan_Drawing_HDP.btn_Save_Click();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(plan_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            profile_Drawing_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(profile_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            cross_Section_Drawing_HDP.btn_save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(cross_Section_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion Chiranjit [2015 06 16]

            string proj_file = Project_File;
            txt_HDP_file.Text = Project_File;




            rtb_project_data.Lines = list.ToArray();

            File.WriteAllLines(Project_File, list.ToArray());

            Save_Data();

            MessageBox.Show(this, "Project file created as " + Project_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion Chiranjit[2015 05 28]
        }

        private void Save_Irrigation_RiverCanal_Project_File()
        {

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");
            txt_HDP_file.Text = Project_File;

            #region Chiranjit[2015 05 28]
            #endregion
            List<string> list = new List<string>();

            string str = "";

            #region Chiranjit [2015 06 16]


            if (RoadProject.Instance.Project == null) return;

            RoadProject.Instance.Project.SurveyFile = Survey_File;
            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


            #region Select Survey File
            list.Clear();
            #region Banner

            list.Add("");
            list.Add("");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***                    HEADS SITE                          ***");
            list.Add("\t\t\t***            TECHSOFT ENGINEERING SERVICES               ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***      IRRIGATION RIVER/CANAL DESILTATION DESIGN         ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add(string.Format("\t\t\t***          DATE : {0}/{1}/{2}    TIME : {3}:{4}:{5}          ***",
                DateTime.Now.Day.ToString("00"),
                DateTime.Now.Month.ToString("00"),
                DateTime.Now.Year.ToString("0000"),
                DateTime.Now.Hour.ToString("00"),
                DateTime.Now.Minute.ToString("00"),
                DateTime.Now.Second.ToString("00")));
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("");
            list.Add("");

            #endregion Banner

            if (rbtn_auto_level.Checked)
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                halign_Diagram_HDP.btn_Save_Click();

                //list.Add(string.Format("//Creating Horizontal alignment Diagram to show in Profile Drawing"));
                //list.Add(string.Format(""));
                //list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
                //list.Add(string.Format(""));
                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            #region Select Survey File

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Total_Station_Data;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Formating Data into DGM / GENIO Format"));
                //list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> SURVEY.txt"));
                list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> {0}", Path.GetFileName(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Processing DGM / GENIO Data file"));
                list.Add(string.Format(""));
                //list.Add(string.Format("DGM >> Modeling from DGM File >> SURVEY.GEN"));
                list.Add(string.Format("DGM >> Modeling from DGM File >> {0}.GEN", Path.GetFileNameWithoutExtension(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else if (rbtn_auto_level.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Auto_Level_Data;

                str = "UTILITIES >> FORMAT CROSS SECTION DATA DGM >> " + Path.GetFileName(Survey_File);
                list.Add(str);
                list.Add("");
                list.Add("");
                str = "DGM  >> Modeling from CROSS SECTION DGM File >>  CROSSECT.DGM";
                list.Add(str);
                list.Add("");
                list.Add("");
            }



            //if (chk_utm_conversion.Checked)
            //{
            //    list.Add(string.Format(""));


            //    //list.AddRange(utM_Conversion1.HEADS_Data.ToArray());

            //    list.Add(string.Format("Transform Ground Survey data from TM (Tranverse Markator) to UTM (Unversal Tranverse Markator)"));
            //    utM_Conversion1.TM_to_UTM_Conversion();
            //    Project.TM_to_UTM_Data = utM_Conversion1.HEADS_Data;
            //    list.AddRange(utM_Conversion1.HEADS_Data);
            //}

            #endregion Select Survey File

            #region Creating Ground Modeling Triangulation

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list = new List<string>();
            list.Add(string.Format("//Creating Ground Modeling Triangulation"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            list.Add(string.Format("101,BOU,MODEL={0},STRING={1}", txt_GMT_model.Text, txt_GMT_string.Text));
            list.Add(string.Format("FINISH"));

            Project.Triangulation_Data = list;

            list.Add(string.Format(""));

            #endregion Creating Ground Modeling Triangulation

            #region Creating Contour

            //list = new List<string>();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Contour"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0"));
            //list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING=ELEV,INC=5.0,TSI=5"));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_pri_string.Text, txt_Contour_pri_inc.Text));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_sec_string.Text, txt_Contour_sec_inc.Text));
            list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING={0},INC={1},TSI=5", txt_Contour_ele_string.Text, txt_Contour_ele_inc.Text));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Project.Contour_Data = list;

            #endregion Creating Contour

            //Project.S

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                if (rbtn_bearing_line.Checked)
                {
                    string temp_off = Path.Combine(Working_Folder, "TEMP_OFF.TXT");
                    if (File.Exists(temp_off))
                    {
                        list.AddRange(File.ReadAllLines(temp_off));
                    }
                }


                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
            }




            #region Ground Long Section
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            string fname = Path.Combine(Folder_Current, "GLONGSEC.TXT");
            if (File.Exists(fname))
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section"));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(fname));

            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section as String E001 following Sring M001"));
                list.Add(string.Format(""));
                list.Add(string.Format("HEADS"));
                list.Add(string.Format("100,DGM"));
                list.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
                list.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
                list.Add(string.Format("FINISH"));
            }
            #endregion Ground Long Section
            list.Add(string.Format(""));

            #region Vertical Alignment


            Read_Valign_Data();

            list.Add(string.Format(""));
            list.Add(string.Format("//Design of Vertical Alignment"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Project.ValignData.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            valign_Diagram_HDP.btn_Save_Click();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Vertical Alignment Diagram to show in Profile Drawing"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion Vertical Alignment



            string inp_file = Path.Combine(Folder_HDP_Cross_Section, "Cross_Section.txt");


            if (File.Exists(inp_file))
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(inp_file));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            //pavement_HDP.btn_OK_Click();

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.AddRange(pavement_HDP.HEADS_Data);
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Volume_HDP_Data.Lines);
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            halign_Diagram_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//CREATING DRAWINGS FOR DETAILS OF HORIZONTAL ALIGNMENT:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("//CREATING DRAWINGS FOR DETAILS OF VERTICAL ALIGNMENT:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            valign_Diagram_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));




            plan_Drawing_HDP.btn_Save_Click();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(plan_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            profile_Drawing_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(profile_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            cross_Section_Drawing_HDP.btn_save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(cross_Section_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion Chiranjit [2015 06 16]

            string proj_file = Project_File;
            txt_HDP_file.Text = Project_File;




            rtb_project_data.Lines = list.ToArray();

            File.WriteAllLines(Project_File, list.ToArray());

            Save_Data();

            MessageBox.Show(this, "Project file created as " + Project_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion Chiranjit[2015 05 28]
        }

        private void Save_Project_File()
        {

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");
            txt_HDP_file.Text = Project_File;

            #region Chiranjit[2015 05 28]
            #endregion
            List<string> list = new List<string>();

            string str = "";

            #region Chiranjit [2015 06 16]
            RoadProject.Instance.Project = new HighwayRoadProject(Project_File);

            if (RoadProject.Instance.Project == null) return;

            RoadProject.Instance.Project.SurveyFile = Survey_File;
            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


            #region Select Survey File
            list.Clear();
            #region Banner

            list.Add("");
            list.Add("");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***                    HEADS SITE                          ***");
            list.Add("\t\t\t***            TECHSOFT ENGINEERING SERVICES               ***");
            list.Add("\t\t\t***                                                        ***");
            //list.Add("\t\t\t***               IRRIGATION CANAL DESIGN                  ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add(string.Format("\t\t\t***          DATE : {0}/{1}/{2}    TIME : {3}:{4}:{5}          ***",
                DateTime.Now.Day.ToString("00"),
                DateTime.Now.Month.ToString("00"),
                DateTime.Now.Year.ToString("0000"),
                DateTime.Now.Hour.ToString("00"),
                DateTime.Now.Minute.ToString("00"),
                DateTime.Now.Second.ToString("00")));
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("");
            list.Add("");

            #endregion Banner

            if (rbtn_auto_level.Checked)
            {
                txt_HDP_HalignData = rtb_auto_level_halign;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));


                halign_Diagram_HDP.btn_Save_Click();

                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
                list.Add(string.Format(""));
            }

            #region Select Survey File

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked || rbtn_survey_drawing.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Total_Station_Data;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Formating Data into DGM / GENIO Format"));
                //list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> SURVEY.txt"));
                list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> {0}", Path.GetFileName(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Processing DGM / GENIO Data file"));
                list.Add(string.Format(""));
                //list.Add(string.Format("DGM >> Modeling from DGM File >> SURVEY.GEN"));
                list.Add(string.Format("DGM >> Modeling from DGM File >> {0}.GEN", Path.GetFileNameWithoutExtension(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else if (rbtn_auto_level.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Auto_Level_Data;

                str = "UTILITIES >> FORMAT CROSS SECTION DATA DGM >> " + Path.GetFileName(Survey_File);
                list.Add(str);
                list.Add("");
                list.Add("");
                str = "DGM  >> Modeling from CROSS SECTION DGM File >>  CROSSECT.DGM";
                list.Add(str);
                list.Add("");
                list.Add("");
            }



            //if (chk_utm_conversion.Checked)
            //{
            //    list.Add(string.Format(""));


            //    //list.AddRange(utM_Conversion1.HEADS_Data.ToArray());

            //    list.Add(string.Format("Transform Ground Survey data from TM (Tranverse Markator) to UTM (Unversal Tranverse Markator)"));
            //    utM_Conversion1.TM_to_UTM_Conversion();
            //    Project.TM_to_UTM_Data = utM_Conversion1.HEADS_Data;
            //    list.AddRange(utM_Conversion1.HEADS_Data);
            //}

            #endregion Select Survey File

            #region Creating Ground Modeling Triangulation

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list = new List<string>();
            list.Add(string.Format("//Creating Ground Modeling Triangulation"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            list.Add(string.Format("101,BOU,MODEL={0},STRING={1}", txt_GMT_model.Text, txt_GMT_string.Text));
            list.Add(string.Format("FINISH"));

            Project.Triangulation_Data = list;

            list.Add(string.Format(""));

            #endregion Creating Ground Modeling Triangulation

            #region Creating Contour

            //list = new List<string>();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Contour"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0"));
            //list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING=ELEV,INC=5.0,TSI=5"));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_pri_string.Text, txt_Contour_pri_inc.Text));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_sec_string.Text, txt_Contour_sec_inc.Text));
            list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING={0},INC={1},TSI=5", txt_Contour_ele_string.Text, txt_Contour_ele_inc.Text));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Project.Contour_Data = list;

            #endregion Creating Contour

          

            string proj_file = Project_File;
            txt_HDP_file.Text = Project_File;




            rtb_project_data.Lines = list.ToArray();

            File.WriteAllLines(Project_File, list.ToArray());

            Save_Data();

            MessageBox.Show(this, "Project file created as " + Project_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion Chiranjit[2015 05 28]
        }

        private void Save_Irrigation_Dyke_Project_File()
        {

            //Project_File = Path.Combine(Working_Folder, "Project Data File.TXT");
            txt_HDP_file.Text = Project_File;

            #region Chiranjit[2015 05 28]
            #endregion
            List<string> list = new List<string>();

            string str = "";

            #region Chiranjit [2015 06 16]


            if (RoadProject.Instance.Project == null) return;

            RoadProject.Instance.Project.SurveyFile = Survey_File;
            RichTextBox txt_HDP_HalignData = rtb_SLG_halign;


            #region Select Survey File
            list.Clear();
            #region Banner

            list.Add("");
            list.Add("");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***                    HEADS SITE                          ***");
            list.Add("\t\t\t***            TECHSOFT ENGINEERING SERVICES               ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t***             IRRIGATION DYKE/BUND DESIGN                ***");
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add(string.Format("\t\t\t***          DATE : {0}/{1}/{2}    TIME : {3}:{4}:{5}          ***",
                DateTime.Now.Day.ToString("00"),
                DateTime.Now.Month.ToString("00"),
                DateTime.Now.Year.ToString("0000"),
                DateTime.Now.Hour.ToString("00"),
                DateTime.Now.Minute.ToString("00"),
                DateTime.Now.Second.ToString("00")));
            list.Add("\t\t\t***                                                        ***");
            list.Add("\t\t\t**************************************************************");
            list.Add("\t\t\t**************************************************************");
            list.Add("");
            list.Add("");

            #endregion Banner

            if (rbtn_auto_level.Checked)
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                halign_Diagram_HDP.btn_Save_Click();

                //list.Add(string.Format("//Creating Horizontal alignment Diagram to show in Profile Drawing"));
                //list.Add(string.Format(""));
                //list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
                //list.Add(string.Format(""));
                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            #region Select Survey File

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Total_Station_Data;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Formating Data into DGM / GENIO Format"));
                //list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> SURVEY.txt"));
                list.Add(string.Format("UTILITIES >> FORMAT DATA AS 2D/3D DGM >> {0}", Path.GetFileName(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("//Processing DGM / GENIO Data file"));
                list.Add(string.Format(""));
                //list.Add(string.Format("DGM >> Modeling from DGM File >> SURVEY.GEN"));
                list.Add(string.Format("DGM >> Modeling from DGM File >> {0}.GEN", Path.GetFileNameWithoutExtension(Survey_File)));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            else if (rbtn_auto_level.Checked)
            {
                RoadProject.Instance.SurveyType = eSurveyType.Auto_Level_Data;

                str = "UTILITIES >> FORMAT CROSS SECTION DATA DGM >> " + Path.GetFileName(Survey_File);
                list.Add(str);
                list.Add("");
                list.Add("");
                str = "DGM  >> Modeling from CROSS SECTION DGM File >>  CROSSECT.DGM";
                list.Add(str);
                list.Add("");
                list.Add("");
            }



            //if (chk_utm_conversion.Checked)
            //{
            //    list.Add(string.Format(""));


            //    //list.AddRange(utM_Conversion1.HEADS_Data.ToArray());

            //    list.Add(string.Format("Transform Ground Survey data from TM (Tranverse Markator) to UTM (Unversal Tranverse Markator)"));
            //    utM_Conversion1.TM_to_UTM_Conversion();
            //    Project.TM_to_UTM_Data = utM_Conversion1.HEADS_Data;
            //    list.AddRange(utM_Conversion1.HEADS_Data);
            //}

            #endregion Select Survey File

            #region Creating Ground Modeling Triangulation

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list = new List<string>();
            list.Add(string.Format("//Creating Ground Modeling Triangulation"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            list.Add(string.Format("101,BOU,MODEL={0},STRING={1}", txt_GMT_model.Text, txt_GMT_string.Text));
            list.Add(string.Format("FINISH"));

            Project.Triangulation_Data = list;

            list.Add(string.Format(""));

            #endregion Creating Ground Modeling Triangulation

            #region Creating Contour

            //list = new List<string>();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Contour"));
            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0"));
            //list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0"));
            //list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING=ELEV,INC=5.0,TSI=5"));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_pri_string.Text, txt_Contour_pri_inc.Text));
            list.Add(string.Format("104,CON,MODEL=CONTOUR,STRING={0},INC={1}", txt_Contour_sec_string.Text, txt_Contour_sec_inc.Text));
            list.Add(string.Format("105,TXT,MODEL=CONTOUR,STRING={0},INC={1},TSI=5", txt_Contour_ele_string.Text, txt_Contour_ele_inc.Text));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Project.Contour_Data = list;

            #endregion Creating Contour

            //Project.S

            if (rbtn_total_station.Checked || rbtn_bearing_line.Checked)
            {
                #region Hrizontal Alignment

                list.Add(string.Format(""));
                list.Add(string.Format("//Design of Horizontal Alignment"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(txt_HDP_HalignData.Lines);
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                if (rbtn_bearing_line.Checked)
                {
                    string temp_off = Path.Combine(Working_Folder, "TEMP_OFF.TXT");
                    if (File.Exists(temp_off))
                    {
                        list.AddRange(File.ReadAllLines(temp_off));
                    }
                }


                list.Add(string.Format(""));
                #endregion Hrizontal Alignment
            }




            #region Ground Long Section
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            string fname = Path.Combine(Folder_Current, "GLONGSEC.TXT");
            if (File.Exists(fname))
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section"));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(fname));

            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("//Creating Ground long Section as String E001 following Sring M001"));
                list.Add(string.Format(""));
                list.Add(string.Format("HEADS"));
                list.Add(string.Format("100,DGM"));
                list.Add(string.Format("102,DES,MODEL=DESIGN,STRING=M001"));
                list.Add(string.Format("103,EXL,MODEL=DESIGN,STRING=E001"));
                list.Add(string.Format("FINISH"));
            }
            #endregion Ground Long Section
            list.Add(string.Format(""));

            #region Vertical Alignment


            Read_Valign_Data();

            list.Add(string.Format(""));
            list.Add(string.Format("//Design of Vertical Alignment"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Project.ValignData.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            valign_Diagram_HDP.btn_Save_Click();
            list.Add(string.Format(""));
            list.Add(string.Format("//Creating Vertical Alignment Diagram to show in Profile Drawing"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion Vertical Alignment



            string inp_file = Path.Combine(Folder_HDP_Cross_Section, "Cross_Section.txt");


            if (File.Exists(inp_file))
            {
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.AddRange(File.ReadAllLines(inp_file));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }

            //pavement_HDP.btn_OK_Click();

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.AddRange(pavement_HDP.HEADS_Data);
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(Volume_HDP_Data.Lines);
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            halign_Diagram_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//CREATING DRAWINGS FOR DETAILS OF HORIZONTAL ALIGNMENT:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(halign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("//CREATING DRAWINGS FOR DETAILS OF VERTICAL ALIGNMENT:"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            valign_Diagram_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(valign_Diagram_HDP.HEADS_Data.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));




            plan_Drawing_HDP.btn_Save_Click();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(plan_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            profile_Drawing_HDP.btn_Save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(profile_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            cross_Section_Drawing_HDP.btn_save_Click();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.AddRange(cross_Section_Drawing_HDP.HEADS_Data);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion Chiranjit [2015 06 16]

            string proj_file = Project_File;
            txt_HDP_file.Text = Project_File;




            rtb_project_data.Lines = list.ToArray();

            File.WriteAllLines(Project_File, list.ToArray());

            Save_Data();

            MessageBox.Show(this, "Project file created as " + Project_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion Chiranjit[2015 05 28]
        }

        private void btn_HDP_create_project_file_Click(object sender, EventArgs e)
        {
            try
            {
                if(ProjectType == eTypeOfProject.Site_Leveling_Grading)
                    Save_Site_Leveling_Grading_Project_File();
                else if (ProjectType == eTypeOfProject.Irrigation_Canal)
                    Save_Irrigation_Canal_Project_File();
                else if (ProjectType == eTypeOfProject.Irrigation_RiverDesiltation)
                    Save_Irrigation_RiverCanal_Project_File();
                else if (ProjectType == eTypeOfProject.Irrigation_Dyke)
                    Save_Irrigation_Dyke_Project_File();


            }
            catch (Exception exx) { }
        }
        #endregion Save Data

        private void btn_HDP_open_project_file_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_HDP_open_project_file.Name)
            {
                if (File.Exists(Project_File))
                    RunExe(Project_File);
            }
            else if (btn.Name == btn_HDP_process_project_file.Name)
            {
                Folder_Current = Working_Folder;


                //if (!File.Exists(txt_HDP_file.Text))
                //    Project_File = txt_HDP_file.Text;

                //string inp = Folder_Current;


                //if (!Directory.Exists(Folder_Current)) Directory.CreateDirectory(Folder_Current);
                //inp = Path.Combine(Working_Folder, "Processed Data");






                //string hill = Path.Combine(Working_Folder, "EW_HILLROAD.TXT");
                //inp = Path.Combine(Folder_Current, "EW_HILLROAD.TXT");
                //if (File.Exists(hill))
                //    File.Copy(hill, inp, true);


                //inp = Path.Combine(Folder_Current, Path.GetFileName(Survey_File));

                //File.Copy(Survey_File, inp, true);

                //inp = Path.Combine(Folder_Current, Path.GetFileName(Project_File));

                //File.Copy(Project_File, inp, true);




                frm_Visual_Process fp = new frm_Visual_Process(Project_File);

                fp.ShowDialog();
            }
            else if (btn.Name == btn_HDP_view_report_file.Name)
            {
                frm_Open_Reports f = new frm_Open_Reports(Working_Folder);
                f.ShowDialog();

            }
            else if (btn.Name == btn_HDP_view_drawings.Name)
            {
                Folder_Current = Working_Folder;

                OpenDesignDrawings(true);
            }

        }
        private void OpenDesignDrawings(bool isDesignDrawings)
        {


            string fp = Path.Combine(Folder_Current, "HALIGN.DRG");
            //if (File.Exists(fp))
            //{
            //    string ss = Path.Combine(Folder_Current, "Design Drawings");
            //    ss = Path.Combine(ss, "HALIGN.DRG");
            //    File.Copy(fp, ss, true);
            //}

            //fp = Path.Combine(Folder_Current, "Superelevation.DRG");
            //if (File.Exists(fp))
            //{
            //    string ss = Path.Combine(Folder_Current, "Design Drawings");
            //    ss = Path.Combine(ss, "Superelevation.DRG");
            //    File.Copy(fp, ss, true);
            //}
            //fp = Path.Combine(Folder_Current, "Superelevation.DRG");
            //if (File.Exists(fp))
            //{
            //    string ss = Path.Combine(Folder_Current, "Design Drawings");
            //    ss = Path.Combine(ss, "Superelevation.DRG");
            //    File.Copy(fp, ss, true);
            //}
            fp = Path.Combine(Folder_Current, "VALIGN.DRG");
            //if (File.Exists(fp))
            //{
            //    string ss = Path.Combine(Folder_Current, "Design Drawings");
            //    ss = Path.Combine(ss, "VALIGN.DRG");
            //    File.Copy(fp, ss, true);
            //}
            if (isDesignDrawings)
            {
                if (!Directory.Exists(Path.Combine(Folder_Current, "Design Drawings")))
                {
                    if (!Directory.Exists(Path.Combine(Folder_Current, "Design Pavements"))) return;
                }
                if (MessageBox.Show("Drawings are created in the folder \"Design Drawings\" inside the working folder.\n\nDo you want to open all the Drawings?", "HEADS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Environment.SetEnvironmentVariable("HEADSPROJECT", Path.Combine(Folder_Current, "Design Drawings"));
                }
                else
                    return;
            }
            else
                System.Environment.SetEnvironmentVariable("HEADSPROJECT", "");

            try
            {
                //System.Diagnostics.Process.Start(Path.Combine(exe_path, "viewer.exe"));
                RunExe("VIEWER.EXE", false);
            }
            catch (Exception ex) { }

        }




        private void btn_HDP_survey_view_Click(object sender, EventArgs e)
        {

            if (File.Exists(txt_survey_data.Text))
                System.Diagnostics.Process.Start(txt_survey_data.Text);
        }
        #endregion Site Leveling and Grading

        #endregion Site Leveling and Grading


        #region Catchment Area, Storage Quantity
        private void uC_QuantityMeasurement1_Proceed_Click(object sender, EventArgs e)
        {
            uC_Stockpile.vdoc = VDoc;
            uC_Stockpile.working_folder = Working_Folder;
        }
        private void uC_QuantityMeasurement2_Proceed_Click(object sender, EventArgs e)
        {
            uC_Discharge.vdoc = VDoc;
            uC_Discharge.working_folder = Working_Folder;
        }
        #endregion Catchment Area, Storage Quantity



        #region Disnet : Water Distribution Network


        private void btn_disnet_create_loop_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 1;

                while (lst_disnet_strings.Items.Contains("LOOP" + i))
                {
                    i++;
                }

                MessageBox.Show("Left Click on the Loop Polyline, then Press Right Click.", "HEADS", MessageBoxButtons.OK);
                SelectedModel = "PIPENET";
                SelectedStrings.Clear();
                SelectedStrings.Add("LOOP" + i);
                HeadsFunctions.CreateBoundaryDialog(this, false).ShowDialog();
            }
            catch (Exception exx) { }

            Read_Pipe_Network();
        }

        private void Read_Pipe_Network()
        {
            #region Read Model List
            CHEADSDoc.ReadModelsFromFile(Folder_Current);
            lst_disnet_strings.Items.Clear();
            for (int i = 0; i < CHEADSDoc.LstModel.Count; i++)
            {
                //if (CHEADSDoc.LstModel[i].ModelName == "" || CHEADSDoc.LstModel[i].StringName == "") continue;

                if (CHEADSDoc.LstModel[i].ModelName != cmb_disnet_pipenet_model.Text) continue;

                //string kStr = String.Format("{0,-15} :{1,-25}", CHEADSDoc.LstModel[i].ModelName, CHEADSDoc.LstModel[i].StringName);
                string kStr = String.Format("{0}", CHEADSDoc.LstModel[i].StringName);
                if (lst_disnet_strings.Items.Contains(kStr) == false)
                {
                    //this.lb_GMT_ModelAndStringName.SetSelected(i, true);
                    lst_disnet_strings.Items.Add(kStr);
                }
                //this.lb_GMT_ModelAndStringName.SetSelected(i, true);
            }

            for (int iIndex = 0; iIndex < this.lst_disnet_strings.Items.Count; iIndex++)
            {
                this.lst_disnet_strings.SetSelected(iIndex, true);
            }
            #endregion Read Model List
        }

        private void cmb_disnet_pipenet_model_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lst_disnet_strings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_disnet_nodal_ground_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();


            list.Add(string.Format(""));
            list.Add(string.Format("HEADS"));
            list.Add(string.Format("100,DGM"));
            MyList ml = null;


            uC_DisNet1.iApp = this;
            uC_DisNet1.strSeleModel = cmb_disnet_pipenet_model.Text;
            uC_DisNet1.lbStringName = new List<string>();

            for (int i = 0; i < lst_disnet_strings.Items.Count; i++)
            {
                if (lst_disnet_strings.GetSelected(i))
                {
                    //ml = new MyList(MyList.RemoveAllSpaces(lst_disnet_strings.Items[i].ToString()), ':');

                    //list.Add(string.Format("102,DES,MOD=PIPENET,STR=LOOP1", ml.StringList[0], ml.StringList[1]));
                    //list.Add(string.Format("103,EXL,MOD=DISNET,STR=LOOP1"));
                    list.Add(string.Format("102,DES,MOD={0},STR={1}", cmb_disnet_pipenet_model.Text.ToString(), lst_disnet_strings.Items[i].ToString()));
                    list.Add(string.Format("103,EXL,MOD=DISNET,STR={0}", lst_disnet_strings.Items[i].ToString()));



                    uC_DisNet1.lbStringName.Add(lst_disnet_strings.Items[i].ToString());

                }
            }
            list.Add(string.Format("FINISH"));

            string fn = Path.Combine(Working_Folder, "NODAL_GROUND.TXT");

            File.WriteAllLines(fn, list.ToArray());
            Run_Data(fn, false);


            

            uC_DisNet1.UC_DisNet_Load();
           

        }

        #endregion Disnet : Water Distribution Network

        private void uC_DisNet1_On_Proceed(object sender, EventArgs e)
        {
            uC_DisNet1.iApp = this;

            uC_DisNet1.doc = VDoc;

            Save_Data();

            //uC_DisNet1.UC_DisNet_Load();

        }


        public IDisNetDocument AppDocument { get; set; }

        public Form AppWindow
        {
            get { return this; }
        }

        public string CurrentDirectory
        {
            get { return  Working_Folder; }
        }

        public string ExePath
        {
            get { return Application.StartupPath; }
        }

        public List<string> LstRecentFiles { get; set; }

        public void RunViewer()
        {
            RunExe("Viewer.exe");
        }

        public bool SerializeDocument(string FileName)
        {
            throw new NotImplementedException();
        }

        public bool DeSerializeDocument(string FileName)
        {
            throw new NotImplementedException();
        }

        public void ModelFileUpdate()
        {

            DisNetModelString ms = new DisNetModelString(this, Path.Combine(this.CurrentDirectory, "MODEL.FIL"));
            ms.ModelUpdate();
            AppDocument.WriteModel(Working_Folder);
        }

        public int PipeNo { get; set; }

        public int JointNo { get; set; }

        public bool Is_Demo
        {
            get { return HASP_LOCK_CHECK.IsDemo; }
        }

        private void chk_Contour_ELEV_CheckedChanged(object sender, EventArgs e)
        {
            vdLayer lay = VDoc.Layers.FindName(txt_Contour_pri_string.Text);

            VDoc.ActiveLayer = VDoc.Layers[0];
            if (lay != null) lay.Frozen = !chk_Contour_C001.Checked;

            lay = VDoc.Layers.FindName(txt_Contour_sec_string.Text);

            if (lay != null) lay.Frozen = !chk_Contour_C005.Checked;

            lay = VDoc.Layers.FindName(txt_Contour_ele_string.Text);

            if (lay != null) lay.Frozen = !chk_Contour_ELEV.Checked;

            lay = VDoc.Layers.FindName(chk_Contour_SURFACE.Text);

            if (lay != null) lay.Frozen = !chk_Contour_SURFACE.Checked;

            VDoc.Redraw(true);
        }

        private void btn_tutor_vids_Click(object sender, EventArgs e)
        {

        }

        private void btn_HRP_halign_video_Click(object sender, EventArgs e)
        {

        }

        private void btn_offset_add_Click(object sender, EventArgs e)
        {
            int r = -1;

            if (dgv_offset_service.SelectedCells.Count > 0)
                r = dgv_offset_service.SelectedCells[0].RowIndex;

            Button btn = sender as Button;

            if (btn == btn_offset_add)
            {
                dgv_offset_service.Rows.Add("", "", "", "", "", "", "", "");
            }
            else if (btn == btn_offset_insert)
            {
                if (r > -1)
                    dgv_offset_service.Rows.Insert(r,

                        dgv_offset_service[0,r].Value.ToString()
                        ,dgv_offset_service[1,r].Value.ToString()
                        ,dgv_offset_service[2,r].Value.ToString()
                        ,dgv_offset_service[3,r].Value.ToString()
                        ,dgv_offset_service[4,r].Value.ToString()
                        , dgv_offset_service[5, r].Value.ToString()
                        , dgv_offset_service[6, r].Value.ToString()
                        , dgv_offset_service[7, r].Value.ToString()
                        );
                else
                    dgv_offset_service.Rows.Add("", "", "", "", "", "", "", "");
            }
            else if (btn == btn_offset_insert)
            {
                if (r > -1)
                    dgv_offset_service.Rows.Insert(r, "", "", "", "", "", "", "", "");
                else
                    dgv_offset_service.Rows.Add("", "", "", "", "", "", "", "");
            }
            else if (btn == btn_offset_delete)
            {
                if (r > -1)
                    dgv_offset_service.Rows.RemoveAt(r);
            }
        }

        private void btn_disnet_refresh_loops_Click(object sender, EventArgs e)
        {
            lst_disnet_strings.Items.Clear();
            Folder_Copy(Folder_Contour_Data, Working_Folder);

            uC_DisNet1.Clear();


            //Delete_Layer_Items("WaterFlow0");
            //Delete_Layer_Items("WaterFlow1");
            //Delete_Layer_Items("WaterFlow2");
            //Delete_Layer_Items("Pipes");
            //Delete_Layer_Items("NodalData");


            List<string> list = new List<string>();
            list.Add(string.Format("WaterFlow0"));
            list.Add(string.Format("WaterFlow1"));
            list.Add(string.Format("WaterFlow2"));
            list.Add(string.Format("NodalData"));
            list.Add(string.Format("PipeData"));
            list.Add(string.Format("Loops"));
            list.Add(string.Format("Pipes"));


            for (int i = 0; i < VDoc.ActiveLayOut.Entities.Count; i++)
            {
                if (list.Contains(VDoc.ActiveLayOut.Entities[i].Layer.Name))
                {
                    VDoc.ActiveLayOut.Entities[i].Deleted = true;
                }
            }
            VDoc.ClearEraseItems();

            VDoc.Redraw(true);
        }

        private void btn_Boundary_Click(object sender, EventArgs e)
        {

            if (IsTutorial)
            {
                string Msg = "We are running the Tutorial Example Data. " +
                    "Here the user need not draw the boundary string by using a closed Polyline. " +
                    "The Boundary string encloses area of the river under consideration larger area " +
                    "enclosed by the boundary string will take longer processing time for Triangulation. " +
                    "The sample boundary string is drawn.";

                MessageBox.Show(Msg, "HEADS");

                if (txt_GMT_model.Tag == null)
                {
                    #region Draw BOUNDARY for Tutorial Data
                    List<string> list = new List<string>();
                    list.Add("339769.54889$1507363.66791");
                    list.Add("329540.92533$1506749.95049");
                    list.Add("324631.18603$1504397.36707");
                    list.Add("316959.71836$1507977.38532");
                    list.Add("309697.39563$1503169.93225");
                    list.Add("305196.80127$1496828.18564");
                    list.Add("303048.79032$1492123.01881");
                    list.Add("295172.75018$1490077.29409");
                    list.Add("295377.32265$1485576.69973");
                    list.Add("306219.66362$1487520.13820");
                    list.Add("310208.82681$1494680.17470");
                    list.Add("314300.27623$1499487.62777");
                    list.Add("317675.72201$1500715.06259");
                    list.Add("324426.61355$1498669.33788");
                    list.Add("330870.64640$1501124.20754");
                    list.Add("340894.69748$1502147.06989");
                    list.Add("339769.54889$1507363.66791");

                    vdPolyline vpl = new vdPolyline(VDoc);
                    vpl.setDocumentDefaults();

                    foreach (var item in list)
                    {
                        try
                        {
                            var ml = new MyList(item, '$');
                            vpl.VertexList.Add(new gPoint(ml.GetDouble(0), ml.GetDouble(1)));
                        }
                        catch (Exception exx) { }
                    }
                    vpl.PenColor = new vdColor(Color.Red);
                    VDoc.ActiveLayOut.Entities.Add(vpl);
                    VDoc.Redraw(true);
                    txt_GMT_model.Tag = vpl;

                    #endregion
                }
            }
            try
            {

                this.SelectedModel = txt_GMT_model.Text;
                this.SelectedStrings.Clear();
                this.SelectedStrings.Add(txt_GMT_string.Text);

                MessageBox.Show("Select a Boundary String drawn by a closed Polyline by clicking the left and right mouse button.", "HEADS");


                HeadsFunctions.CreateBoundaryDialog(this, false).ShowDialog();

                Folder_Copy(Folder_Current, Folder_Ground_Modeling);
            }
            catch (Exception exx) { }

            Save_Data();
        }

        private void btn_ALGN_create_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            try
            {
                if (btn == btn_ALGN_create)
                {
                    MessageBox.Show("Select a Alignment Polyline by clicking the left and right mouse button.", "HEADS");
                    this.SelectedModel = txt_ALGN_Model.Text;
                    this.SelectedStrings.Clear();
                    this.SelectedStrings.Add(txt_ALGN_String.Text);

                    HeadsFunctions.CreateBoundaryDialog(this, true).ShowDialog();
                }
                else if (btn == btn_ALGN_chnOn)
                {
                    HeadsFunctions.CreateChainageDialog(this).ShowDialog();
                }
                else if (btn == btn_ALGN_chnOff)
                {
                    HeadsFunctions.DeleteChainage(this);
                }
                else if (btn == btn_ALGN_dtlsOn)
                {
                    HeadsFunctions.CreateDetailsDialog(this).ShowDialog();
                }
                else if (btn == btn_ALGN_dtlsOff)
                {
                    HeadsFunctions.DeleteDetails(this);
                }
                Folder_Copy(Folder_Current, Folder_Contour_Data);
                Save_Data();
            }
            catch (Exception exx) { }
        }


        public string Layout_Base_Drawing
        {
            get;
            set;
        }
    }
}
