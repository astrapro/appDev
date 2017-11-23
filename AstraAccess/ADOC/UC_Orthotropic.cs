using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Generics;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;



using AstraInterface.DataStructure;
using AstraInterface.Interface;


namespace AstraAccess.ADOC
{
    public partial class UC_Orthotropic : UserControl
    {
        IApplication iApp;
        public UC_Orthotropic()
        {
            InitializeComponent();
        }

        public void SetApplication(IApplication app)
        {
            iApp = app;
            uC_CAD1.iApp = app;
            uC_CAD1.PropertyGrid = vdPropertyGrid1;
            uC_CAD1.iApp = iApp;

        }

        public event EventHandler OnDraw_Click;
        private void btn_draw_Click(object sender, EventArgs e)
        {
            if(OnDraw_Click != null)
            {
                uC_CAD1.VDoc.ActiveLayOut.Entities.RemoveAll();
                OnDraw_Click(sender, e);
            }
            else
            {
                Draw();
            }
        }

        public void Draw()
        {

            double L = 10, B = 3, D = 1.0;


            L = MyList.StringToDouble(txt_L);
            B = MyList.StringToDouble(txt_B);
            D = MyList.StringToDouble(txt_D);



            gPoint insPnt = new gPoint(MyList.StringToDouble(txt_X),
                MyList.StringToDouble(txt_Y),
                MyList.StringToDouble(txt_Z));


            vdDocument vdoc = uC_CAD1.VDoc;


            vdPolyface pf = new vdPolyface(vdoc);
            pf.setDocumentDefaults();





            double Lat_Spacing = MyList.StringToDouble(txt_Lat_Spc);


            if (Lat_Spacing > 0)
            {
                #region Web Section

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Lat_Spacing/2)); // ex 0.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Lat_Spacing / 2));  // ex 10.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + B - Lat_Spacing / 2));  // ex 10.0, 0.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + B - Lat_Spacing / 2));  // ex 0.0, 0.0, 3.0

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z - Lat_Spacing / 2));  // ex 0.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z - Lat_Spacing / 2));  // ex 10.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z + B - Lat_Spacing / 2));  // ex 10.0, 1.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z + B - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0


                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                vdoc.ActiveLayOut.Entities.Add(pf);

                #endregion Web Section

                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.Add(pf);

                #region Web Section

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Lat_Spacing / 2)); // ex 0.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Lat_Spacing / 2));  // ex 10.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + B + Lat_Spacing / 2));  // ex 10.0, 0.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + B + Lat_Spacing / 2));  // ex 0.0, 0.0, 3.0

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z + Lat_Spacing / 2));  // ex 0.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z + Lat_Spacing / 2));  // ex 10.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z + B + Lat_Spacing / 2));  // ex 10.0, 1.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z + B + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0


                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);

                #endregion Web Section
            }
            else
            {
                #region Web Section
                vdoc.ActiveLayOut.Entities.Add(pf);

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z)); // ex 0.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z));  // ex 10.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + B));  // ex 10.0, 0.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + B));  // ex 0.0, 0.0, 3.0

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z));  // ex 0.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z));  // ex 10.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z + B));  // ex 10.0, 1.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z + B));  // ex 0.0, 1.0, 3.0


                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);

                #endregion Web Section
            }
            #region Top Flange Section

            double FL_THK = MyList.StringToDouble(txt_TF_THK);
            double FL_WD = MyList.StringToDouble(txt_TF_WD);

            if (FL_THK != 0.0 && FL_WD != 0.0)
            {
                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();





                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D, insPnt.z + B + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D, insPnt.z + B + FL_WD));  // ex 0.0, 1.0, 3.0+1


                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + D + FL_THK, insPnt.z + B + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + D + FL_THK, insPnt.z + B + FL_WD));  // ex 0.0, 1.0, 3.0+1


                #region Add Face List
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                #endregion Add Face List


                vdoc.ActiveLayOut.Entities.Add(pf);

            }
            #endregion Top Flange Section


            #region Bottom Flange Section

            
             FL_THK = MyList.StringToDouble(txt_BF_THK);
             FL_WD = MyList.StringToDouble(txt_BF_WD);

             if (FL_THK != 0.0 && FL_WD != 0.0)
             {
                 pf = new vdPolyface(vdoc);
                 pf.setDocumentDefaults();

                 //FL_THK = 0.3;
                 //FL_WD = 1.0;


                 pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                 pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                 pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + B + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                 pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + B + FL_WD));  // ex 0.0, 1.0, 3.0+1


                 pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                 pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                 pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y - FL_THK, insPnt.z + B + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                 pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y - FL_THK, insPnt.z + B + FL_WD));  // ex 0.0, 1.0, 3.0+1


                 #region Add Face List
                 pf.FaceList.Add(1);
                 pf.FaceList.Add(2);
                 pf.FaceList.Add(3);
                 pf.FaceList.Add(4);
                 pf.FaceList.Add(-1);
                 pf.FaceList.Add(5);
                 pf.FaceList.Add(6);
                 pf.FaceList.Add(7);
                 pf.FaceList.Add(8);
                 pf.FaceList.Add(-1);
                 pf.FaceList.Add(1);
                 pf.FaceList.Add(2);
                 pf.FaceList.Add(6);
                 pf.FaceList.Add(5);
                 pf.FaceList.Add(-1);
                 pf.FaceList.Add(2);
                 pf.FaceList.Add(3);
                 pf.FaceList.Add(7);
                 pf.FaceList.Add(6);
                 pf.FaceList.Add(-1);
                 pf.FaceList.Add(3);
                 pf.FaceList.Add(4);
                 pf.FaceList.Add(8);
                 pf.FaceList.Add(7);
                 pf.FaceList.Add(-1);
                 pf.FaceList.Add(4);
                 pf.FaceList.Add(1);
                 pf.FaceList.Add(5);
                 pf.FaceList.Add(8);
                 pf.FaceList.Add(-1);
                 #endregion Add Face List


                 vdoc.ActiveLayOut.Entities.Add(pf);
             }
            #endregion Bottom Flange Section

            vdoc.Redraw(true);

        }


        public bool DrawElement(SectionElement elmnt)
        {
            try
            {
                //elmnt.Draw(uC_CAD1.VDoc);
                //elmnt.Draw1(uC_CAD1.VDoc);

                if(elmnt.Curve_Radius > 0)
                    elmnt.Draw_Curve(uC_CAD1.VDoc);
                else
                    elmnt.Draw(uC_CAD1.VDoc);
 
                uC_CAD1.VDoc.ActiveLayer = uC_CAD1.VDoc.Layers[0];
            }
            catch (Exception ex) { }
            return false;
        }

        private void chk_DS_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            vdLayer ly = uC_CAD1.VDoc.Layers.FindName(chk.Text);
            if (ly != null) ly.Frozen = !chk.Checked;

            uC_CAD1.VDoc.Redraw(true);
        }
    }

    public class SectionElement
    {

        #region Properties
        public double L { get; set; }
        public double Web_Thickness { get; set; }
        public double Web_Depth { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public bool Is_Cross_Girder { get; set; }

        public double Lat_Spacing { get; set; }

        /// <summary>
        /// Total Number of Plates
        /// </summary>
        public int Plate_Nos { get; set; }
        /// <summary>
        /// Top Flange Thickness
        /// </summary>
        public double TF_THK { get; set; }
        /// <summary>
        /// Top Flange Width
        /// </summary>
        public double TF_WD { get; set; }
        /// <summary>
        /// Bottom Flange Thickness
        /// </summary>
        public double BF_THK { get; set; }
        /// <summary>
        /// Bottom Flange Width
        /// </summary>
        public double BF_WD { get; set; }



        public double TP_THK { get; set; }
        public double TP_WD { get; set; }


        public double BP_THK { get; set; }
        public double BP_WD { get; set; }
        public double SP_1_THK { get; set; }
        public double SP_1_WD { get; set; }
        public double SP_2_THK { get; set; }
        public double SP_2_WD { get; set; }
        public double SP_3_THK { get; set; }
        public double SP_3_WD { get; set; }
        public double SP_4_THK { get; set; }
        public double SP_4_WD { get; set; }


        public double Curve_Radius { get; set; }

        public Color Color_Web_Plate { get; set; }
        public Color Color_Top_Plate { get; set; }
        public Color Color_Top_Flange { get; set; }
        public Color Color_Bottom_Plate { get; set; }
        public Color Color_Bottom_Flange { get; set; }


        public Color Color_Side_Plate_1 { get; set; }
        public Color Color_Side_Plate_2 { get; set; }
        public Color Color_Side_Plate_3 { get; set; }
        public Color Color_Side_Plate_4 { get; set; }

        #endregion Properties

        public SectionElement()
        {
            L = Web_Thickness = Web_Depth = 0.0;
            X = Y = Z = 0.0;
            TF_THK = TF_WD = BF_THK = BF_WD = 0.0;
            TP_THK = TP_WD = BP_THK = BP_WD = 0.0;
            SP_1_THK = SP_1_WD = SP_2_THK = SP_2_WD = 0.0;
            SP_3_THK = SP_3_WD = SP_4_THK = SP_4_WD = 0.0;
            Lat_Spacing = 0.0;
            Is_Cross_Girder = false;

            Curve_Radius = 0.0;

            Color_Web_Plate = Color.DarkBlue;
            Color_Top_Plate  = Color.DarkGreen;
            Color_Top_Flange  = Color.DarkCyan;

            Color_Bottom_Plate = Color.DarkGreen;
            Color_Bottom_Flange = Color.DarkCyan;
            
            Color_Side_Plate_1  = Color.DarkRed;
            Color_Side_Plate_2 = Color.DarkRed;
            Color_Side_Plate_3 = Color.DarkRed;
            Color_Side_Plate_4 = Color.DarkRed;
        }


        public void Cross_Draw(vdDocument vdoc)
        {
            gPoint insPnt = new gPoint(X, Y, Z);

            vdPolyface pf = new vdPolyface(vdoc);
            pf.setDocumentDefaults();

            #region Web Section
            vdoc.ActiveLayOut.Entities.Add(pf);

            pf.VertexList.Add(new gPoint(insPnt.x - Web_Thickness / 2, insPnt.y, insPnt.z)); // ex 0.0, 0.0, 0.0
            pf.VertexList.Add(new gPoint(insPnt.x - Web_Thickness / 2, insPnt.y, insPnt.z + L));  // ex 10.0, 0.0, 0.0
            pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness / 2, insPnt.y, insPnt.z + L));  // ex 10.0, 0.0, 3.0
            pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness / 2, insPnt.y, insPnt.z));  // ex 0.0, 0.0, 3.0

            pf.VertexList.Add(new gPoint(insPnt.x - Web_Thickness / 2, insPnt.y + Web_Depth, insPnt.z));  // ex 0.0, 1.0, 0.0
            pf.VertexList.Add(new gPoint(insPnt.x - Web_Thickness / 2, insPnt.y + Web_Depth, insPnt.z + L));  // ex 10.0, 1.0, 0.0
            pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness / 2, insPnt.y + Web_Depth, insPnt.z + L));  // ex 10.0, 1.0, 3.0
            pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness / 2, insPnt.y + Web_Depth, insPnt.z));  // ex 0.0, 1.0, 3.0


            pf.FaceList.Add(1);
            pf.FaceList.Add(2);
            pf.FaceList.Add(3);
            pf.FaceList.Add(4);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(5);
            pf.FaceList.Add(6);
            pf.FaceList.Add(7);
            pf.FaceList.Add(8);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(1);
            pf.FaceList.Add(2);
            pf.FaceList.Add(6);
            pf.FaceList.Add(5);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(2);
            pf.FaceList.Add(3);
            pf.FaceList.Add(7);
            pf.FaceList.Add(6);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(3);
            pf.FaceList.Add(4);
            pf.FaceList.Add(8);
            pf.FaceList.Add(7);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(4);
            pf.FaceList.Add(1);
            pf.FaceList.Add(5);
            pf.FaceList.Add(8);
            pf.FaceList.Add(-1);

            #endregion Web Section

            #region Top Flange Section

            double FL_THK = TF_THK;
            double FL_WD = TF_WD / 2 - Web_Thickness / 2;

            if (FL_THK != 0.0 && FL_WD != 0.0)
            {
                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();


                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth, insPnt.z));  // ex 0.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth, insPnt.z + L));  // ex 10.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth, insPnt.z));  // ex 0.0, 1.0, 3.0+1


                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth - FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth - FL_THK, insPnt.z + L));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth - FL_THK, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth - FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1


                #region Add Face List
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                #endregion Add Face List


                vdoc.ActiveLayOut.Entities.Add(pf);

            }
            #endregion Top Flange Section

            #region Top Plate Section

            FL_THK = TP_THK;
            FL_WD = TP_WD / 2 - Web_Thickness / 2;

            if (FL_THK != 0.0 && FL_WD != 0.0)
            {
                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();

                pf.PenColor = new vdColor(Color.LightGreen);


                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth, insPnt.z));  // ex 0.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth, insPnt.z + L));  // ex 10.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth, insPnt.z));  // ex 0.0, 1.0, 3.0+1


                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth + FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + Web_Depth + FL_THK, insPnt.z + L));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth + FL_THK, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + Web_Depth + FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1


                #region Add Face List
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                #endregion Add Face List


                vdoc.ActiveLayOut.Entities.Add(pf);

            }
            #endregion Top Plate Section

            #region Bottom Flange Section


            FL_THK = BF_THK;
            FL_WD = BF_WD / 2 - Web_Thickness / 2;

            if (FL_THK != 0.0 && FL_WD != 0.0)
            {
                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();

                //FL_THK = 0.3;
                //FL_WD = 1.0;

                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y, insPnt.z));  // ex 0.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y, insPnt.z + L));  // ex 10.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y, insPnt.z));  // ex 0.0, 1.0, 3.0+1

                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y + FL_THK, insPnt.z + L));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + FL_THK, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y + FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1

                #region Add Face List
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                #endregion Add Face List

                vdoc.ActiveLayOut.Entities.Add(pf);
            }
            #endregion Bottom Flange Section

            #region Bottom Plate Section


            FL_THK = BP_THK;
            FL_WD = BP_WD / 2 - Web_Thickness / 2;

            if (FL_THK != 0.0 && FL_WD != 0.0)
            {
                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();
                pf.PenColor = new vdColor(Color.LightGreen);

                //FL_THK = 0.3;
                //FL_WD = 1.0;

                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y, insPnt.z));  // ex 0.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y, insPnt.z + L));  // ex 10.0, 1.0, 0.0 - 1.0
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + FL_WD, insPnt.y, insPnt.z + Web_Thickness));  // ex 0.0, 1.0, 3.0+1

                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y - FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x - FL_WD, insPnt.y - FL_THK, insPnt.z + L));  // ex 0.0, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y - FL_THK, insPnt.z + L));  // ex 0.0 + 10, 1.0, 3.0+1
                pf.VertexList.Add(new gPoint(insPnt.x + Web_Thickness + FL_WD, insPnt.y - FL_THK, insPnt.z));  // ex 0.0, 1.0, 3.0+1

                #region Add Face List
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                #endregion Add Face List

                vdoc.ActiveLayOut.Entities.Add(pf);
            }
            #endregion Bottom Flange Section

            vdoc.Redraw(true);


            vdoc.Redraw(true);
        }

        public void Draw_Old(vdDocument vdoc, bool sa)
        {



            if (Is_Cross_Girder)
            {

                Cross_Draw(vdoc);
                return;
            }

            gPoint insPnt = new gPoint(X, Y, Z);

            vdPolyface pf = new vdPolyface(vdoc);
            pf.setDocumentDefaults();



            if (Lat_Spacing > 0)
            {
                #region Web Section 1

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0

                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2));  // ex 10.0, 0.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2));  // ex 0.0, 0.0, 3.0

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2));  // ex 0.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 1.0, 0.0

                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2));  // ex 10.0, 1.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0


                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                vdoc.ActiveLayOut.Entities.Add(pf);

                #endregion Web Section



                #region Side Plate 1


                double _yy = (Web_Depth - SP_1_WD) / 2;

                if (SP_1_THK != 0.0 && SP_1_WD != 0.0)
                {

                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Red);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0

                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Web_Thickness / 2 - Lat_Spacing / 2 - SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Web_Thickness / 2 - Lat_Spacing / 2 - SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - Web_Thickness / 2 - Lat_Spacing / 2 - SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - Web_Thickness / 2 - Lat_Spacing / 2 - SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);

                }
                #endregion Side Plate 1

                #region Side Plate 2


                _yy = (Web_Depth - SP_2_WD) / 2;

                if (SP_1_THK != 0.0 && SP_1_WD != 0.0)
                {

                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Red);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Lat_Spacing / 2 + Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Lat_Spacing / 2 + Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2 + SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2 + SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - Lat_Spacing / 2 + Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - Lat_Spacing / 2 + Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2 + SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 - Lat_Spacing / 2 + SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);

                }
                #endregion Side Plate 2

                pf = new vdPolyface(vdoc);
                pf.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.Add(pf);

                #region Web Section 2

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2));  // ex 10.0, 0.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2));  // ex 0.0, 0.0, 3.0

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2));  // ex 0.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2));  // ex 10.0, 1.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0

                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);



                #endregion Web Section



                #region Side Plate 3


                _yy = (Web_Depth - SP_3_WD) / 2;

                if (SP_1_THK != 0.0 && SP_1_WD != 0.0)
                {

                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Red);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Web_Thickness / 2 + Lat_Spacing / 2 - SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Web_Thickness / 2 + Lat_Spacing / 2 - SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Lat_Spacing / 2 - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - Web_Thickness / 2 + Lat_Spacing / 2 - SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - Web_Thickness / 2 + Lat_Spacing / 2 - SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);

                }
                #endregion Side Plate 1


                #region Side Plate 4


                _yy = (Web_Depth - SP_4_WD) / 2;

                if (SP_4_THK != 0.0 && SP_4_WD != 0.0)
                {

                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Red);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Lat_Spacing / 2 + Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Lat_Spacing / 2 + Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2 + SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2 + SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Lat_Spacing / 2 + Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Lat_Spacing / 2 + Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2 + SP_1_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2 + Lat_Spacing / 2 + SP_1_THK));  // ex 0.0, 0.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);

                }
                #endregion Side Plate 1



                #region Top Flange Section

                double FL_THK = TF_THK;
                double FL_WD = TF_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth - FL_THK, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth - FL_THK, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth - FL_THK, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth - FL_THK, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1


                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List


                    vdoc.ActiveLayOut.Entities.Add(pf);


                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth - FL_THK, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth - FL_THK, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth - FL_THK, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth - FL_THK, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1


                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List


                    vdoc.ActiveLayOut.Entities.Add(pf);

                }
                #endregion Top Flange Section

                #region Top Plate Section

                FL_THK = TP_THK;
                FL_WD = TP_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();

                    pf.PenColor = new vdColor(Color.LightGreen);


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth + FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth + FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1


                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List


                    vdoc.ActiveLayOut.Entities.Add(pf);

                }
                #endregion Top Plate Section

                #region Bottom Flange Section


                FL_THK = BF_THK;
                FL_WD = BF_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();

                    //FL_THK = 0.3;
                    //FL_WD = 1.0;

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + FL_THK, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + FL_THK, insPnt.z - FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + FL_THK, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + FL_THK, insPnt.z + Web_Thickness + FL_WD - Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1

                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List

                    vdoc.ActiveLayOut.Entities.Add(pf);





                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();

                    //FL_THK = 0.3;
                    //FL_WD = 1.0;

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + FL_THK, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + FL_THK, insPnt.z - FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + FL_THK, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + FL_THK, insPnt.z + Web_Thickness + FL_WD + Lat_Spacing / 2));  // ex 0.0, 1.0, 3.0+1

                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List

                    vdoc.ActiveLayOut.Entities.Add(pf);
                }
                #endregion Bottom Flange Section

                #region Bottom Plate Section


                FL_THK = BP_THK;
                FL_WD = BP_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    pf.PenColor = new vdColor(Color.LightGreen);

                    //FL_THK = 0.3;
                    //FL_WD = 1.0;

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y - FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y - FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1

                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List

                    vdoc.ActiveLayOut.Entities.Add(pf);
                }
                #endregion Bottom Flange Section

            }
            else
            {
                #region Web Section
                vdoc.ActiveLayOut.Entities.Add(pf);

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - Web_Thickness / 2)); // ex 0.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - Web_Thickness / 2));  // ex 10.0, 0.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness / 2));  // ex 10.0, 0.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness / 2));  // ex 0.0, 0.0, 3.0

                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - Web_Thickness / 2));  // ex 0.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - Web_Thickness / 2));  // ex 10.0, 1.0, 0.0
                pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2));  // ex 10.0, 1.0, 3.0
                pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness / 2));  // ex 0.0, 1.0, 3.0


                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(6);
                pf.FaceList.Add(7);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(6);
                pf.FaceList.Add(5);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(2);
                pf.FaceList.Add(3);
                pf.FaceList.Add(7);
                pf.FaceList.Add(6);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(3);
                pf.FaceList.Add(4);
                pf.FaceList.Add(8);
                pf.FaceList.Add(7);
                pf.FaceList.Add(-1);
                pf.FaceList.Add(4);
                pf.FaceList.Add(1);
                pf.FaceList.Add(5);
                pf.FaceList.Add(8);
                pf.FaceList.Add(-1);

                #endregion Web Section



                #region Side Plate 1


                double _yy = (Web_Depth - SP_1_WD) / 2;

                if (SP_1_THK != 0.0 && SP_1_WD != 0.0)
                {

                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Red);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK - SP_1_THK)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK - SP_1_THK));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 0.0, 0.0, 3.0

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + SP_1_WD + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK - SP_1_THK));  // ex 0.0, 1.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + SP_1_WD + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK - SP_1_THK));  // ex 10.0, 1.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + SP_1_WD + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 10.0, 1.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + SP_1_WD + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 0.0, 1.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);

                }
                #endregion Side Plate 1


                #region Side Plate 2

                if (SP_2_THK != 0.0 && SP_2_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Cyan);


                    _yy = (Web_Depth - SP_2_WD) / 2;


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z - Web_Thickness / 2));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z - Web_Thickness / 2));  // ex 0.0, 0.0, 3.0

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy + SP_1_WD, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 0.0, 1.0, 0.0

                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy + SP_1_WD, insPnt.z - Web_Thickness / 2 - SP_2_THK));  // ex 10.0, 1.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy + SP_1_WD, insPnt.z - Web_Thickness / 2));  // ex 10.0, 1.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy + SP_1_WD, insPnt.z - Web_Thickness / 2));  // ex 0.0, 1.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                }
                #endregion Web Section



                #region Side Plate 3
                if (SP_3_THK != 0.0 && SP_3_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Cyan);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z + Web_Thickness / 2 + SP_2_THK)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z + Web_Thickness / 2));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z + Web_Thickness / 2));  // ex 0.0, 0.0, 3.0

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 0.0, 1.0, 0.0

                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 10.0, 1.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2));  // ex 10.0, 1.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2));  // ex 0.0, 1.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                }
                #endregion Web Section


                #region Side Plate 4
                if (SP_4_THK != 0.0 && SP_4_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.Add(pf);
                    pf.PenColor = new vdColor(Color.Red);

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z + Web_Thickness / 2 + SP_2_THK + SP_1_THK)); // ex 0.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z + Web_Thickness / 2 + SP_2_THK + SP_1_THK));  // ex 10.0, 0.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 10.0, 0.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 0.0, 0.0, 3.0

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2 + SP_2_THK + SP_1_THK));  // ex 0.0, 1.0, 0.0

                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2 + SP_2_THK + SP_1_THK));  // ex 10.0, 1.0, 0.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 10.0, 1.0, 3.0
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + _yy + SP_1_WD, insPnt.z + Web_Thickness / 2 + SP_2_THK));  // ex 0.0, 1.0, 3.0


                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                }
                #endregion Web Section


                #region Top Flange Section

                double FL_THK = TF_THK;
                double FL_WD = TF_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth - FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth - FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1


                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List


                    vdoc.ActiveLayOut.Entities.Add(pf);

                }
                #endregion Top Flange Section

                #region Top Plate Section

                FL_THK = TP_THK;
                FL_WD = TP_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();

                    pf.PenColor = new vdColor(Color.LightGreen);


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1


                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + Web_Depth + FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + Web_Depth + FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1


                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List


                    vdoc.ActiveLayOut.Entities.Add(pf);

                }
                #endregion Top Plate Section

                #region Bottom Flange Section


                FL_THK = BF_THK;
                FL_WD = BF_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();

                    //FL_THK = 0.3;
                    //FL_WD = 1.0;

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y + FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1

                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List

                    vdoc.ActiveLayOut.Entities.Add(pf);
                }
                #endregion Bottom Flange Section

                #region Bottom Plate Section


                FL_THK = BP_THK;
                FL_WD = BP_WD / 2 - Web_Thickness / 2;

                if (FL_THK != 0.0 && FL_WD != 0.0)
                {
                    pf = new vdPolyface(vdoc);
                    pf.setDocumentDefaults();
                    pf.PenColor = new vdColor(Color.LightGreen);

                    //FL_THK = 0.3;
                    //FL_WD = 1.0;

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - FL_WD));  // ex 0.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z - FL_WD));  // ex 10.0, 1.0, 0.0 - 1.0
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1

                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y - FL_THK, insPnt.z - FL_WD));  // ex 0.0, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x + L, insPnt.y - FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0 + 10, 1.0, 3.0+1
                    pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y - FL_THK, insPnt.z + Web_Thickness + FL_WD));  // ex 0.0, 1.0, 3.0+1

                    #region Add Face List
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(2);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(6);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(3);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(7);
                    pf.FaceList.Add(-1);
                    pf.FaceList.Add(4);
                    pf.FaceList.Add(1);
                    pf.FaceList.Add(5);
                    pf.FaceList.Add(8);
                    pf.FaceList.Add(-1);
                    #endregion Add Face List

                    vdoc.ActiveLayOut.Entities.Add(pf);
                }
                #endregion Bottom Flange Section

            }
            vdoc.Redraw(true);

        }

        #region Draw Curve
        public void Draw_Curve(vdDocument vdoc)
        {
            if (Is_Cross_Girder)
            {
                #region Draw Section

                // Draw Web Section
                if (TP_WD == 0.0 && BP_WD == 0.0 && TF_WD == 0.0 && BF_WD == 0.0)
                    Draw_Curve(vdoc, "Cross Girders", Web_Thickness, Web_Depth, Y, Color_Web_Plate);
                else
                    Draw_Curve(vdoc, "Cross Girders", Web_Thickness, Web_Depth, Y + BP_THK + BF_THK, Color_Web_Plate);

                // Draw Top Plate Section
                Draw_Curve(vdoc, "Cross Girders", TP_WD, TP_THK, Y + Web_Depth + BP_THK + BF_THK + TP_THK, Color_Top_Plate);
                // Draw Top Flange Section
                Draw_Curve(vdoc, "Cross Girders", TF_WD, TF_THK, Y + Web_Depth + BP_THK + BF_THK, Color_Top_Flange);

                // Draw Bottom Plate Section
                Draw_Curve(vdoc, "Cross Girders", BP_WD, BP_THK, Y, Color_Bottom_Plate);
                // Draw Bottom Flange Section
                Draw_Curve(vdoc, "Cross Girders", BF_WD, BF_THK, Y + BP_THK, Color_Bottom_Flange);



                //double hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_1_WD) / 2;
                //// Draw Side Plate 1 Section
                //Draw_Curve(vdoc, "Side Plate 1", SP_1_THK, SP_1_WD, L, Z - Web_Thickness, Y + hgt, Color_Side_Plate_1);


                //hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_2_WD) / 2;
                //// Draw Side Plate 2 Section
                //Draw_Curve(vdoc, "Side Plate 2", SP_2_THK, SP_2_WD, L, Z - Web_Thickness - SP_1_THK, Y + hgt, Color_Side_Plate_2);


                //hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_3_WD) / 2;
                //// Draw Side Plate 3 Section
                //Draw_Curve(vdoc, "Side Plate 3", SP_3_THK, SP_3_WD, L, Z + Web_Thickness, Y + hgt, Color_Side_Plate_3);


                //hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_4_WD) / 2;
                //// Draw Side Plate 4 Section
                //Draw_Curve(vdoc, "Side Plate 4", SP_4_THK, SP_4_WD, L, Z + Web_Thickness + SP_3_THK, Y + hgt, Color_Side_Plate_4);

                #endregion Web Section
            }
            else
            {
                if (Lat_Spacing > 0)
                {
                    #region Draw Section

                    // Draw Web Section
                    if (TP_WD == 0.0 && BP_WD == 0.0 && TF_WD == 0.0 && BF_WD == 0.0)
                        Draw_Curve(vdoc, "Deckslab", Web_Thickness, Web_Depth, Y, Color_Web_Plate);
                    else
                    {
                        Draw_Curve(vdoc, "Web Plate", Web_Thickness, Web_Depth, L, Z - Lat_Spacing / 2, Y + BP_THK + BF_THK, Color_Web_Plate);

                        Draw_Curve(vdoc, "Web Plate", Web_Thickness, Web_Depth, L, Z + Lat_Spacing / 2, Y + BP_THK + BF_THK, Color_Web_Plate);
                    }

                    // Draw Top Plate Section
                    Draw_Curve(vdoc, "Top Plate", TP_WD, TP_THK, Y + Web_Depth + BP_THK + BF_THK + TP_THK, Color_Top_Plate);
                    // Draw Top Flange Section
                    Draw_Curve(vdoc, "Top Flange", TF_WD, TF_THK, L, Z - Lat_Spacing / 2, Y + Web_Depth + BP_THK + BF_THK, Color_Top_Flange);
                    Draw_Curve(vdoc, "Top Flange", TF_WD, TF_THK, L, Z + Lat_Spacing / 2, Y + Web_Depth + BP_THK + BF_THK, Color_Top_Flange);

                    // Draw Bottom Plate Section
                    Draw_Curve(vdoc, "Bottom Plate", BP_WD, BP_THK, Y, Color_Bottom_Plate);
                    // Draw Bottom Flange Section
                    Draw_Curve(vdoc, "Bottom Flange", BF_WD, BF_THK, L, Z - Lat_Spacing / 2, Y + BP_THK, Color_Bottom_Flange);
                    Draw_Curve(vdoc, "Bottom Flange", BF_WD, BF_THK, L, Z + Lat_Spacing / 2, Y + BP_THK, Color_Bottom_Flange);



                    double hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_1_WD) / 2;


                    // Draw Side Plate 1 Section
                    Draw_Curve(vdoc, "Side Plate 1", SP_1_THK, SP_1_WD, L, Z - Web_Thickness - Lat_Spacing / 2, Y + hgt, Color_Side_Plate_1);


                    hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_2_WD) / 2;
                    // Draw Side Plate 2 Section
                    //Draw_Curve(vdoc, "Side Plate 2", SP_2_THK, SP_2_WD, L, Z - Web_Thickness - SP_1_THK - Lat_Spacing / 2, Y + hgt, Color_Side_Plate_2);
                    Draw_Curve(vdoc, "Side Plate 2", SP_1_THK, SP_1_WD, L, Z + Web_Thickness - Lat_Spacing / 2, Y + hgt, Color_Side_Plate_1);


                    hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_3_WD) / 2;
                    // Draw Side Plate 3 Section
                    Draw_Curve(vdoc, "Side Plate 3", SP_3_THK, SP_3_WD, L, Z - Web_Thickness + Lat_Spacing / 2, Y + hgt, Color_Side_Plate_3);


                    hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_4_WD) / 2;
                    // Draw Side Plate 4 Section
                    //Draw_Curve(vdoc, "Side Plate 4", SP_4_THK, SP_4_WD, L, Z + Web_Thickness + SP_3_THK, Y + hgt, Color_Side_Plate_4);
                    Draw_Curve(vdoc, "Side Plate 4", SP_4_THK, SP_4_WD, L, Z + Web_Thickness + Lat_Spacing / 2, Y + hgt, Color_Side_Plate_4);

                    #endregion Web Section
                }
                else
                {
                    #region Draw Section

                    // Draw Web Section
                    if (TP_WD == 0.0 && BP_WD == 0.0 && TF_WD == 0.0 && BF_WD == 0.0)
                        Draw_Curve(vdoc, "Deckslab", Web_Thickness, Web_Depth, Y, Color_Web_Plate);
                    else
                        Draw_Curve(vdoc, "Web Plate", Web_Thickness, Web_Depth, Y + BP_THK + BF_THK, Color_Web_Plate);

                    // Draw Top Plate Section
                    Draw_Curve(vdoc, "Top Plate", TP_WD, TP_THK, Y + Web_Depth + BP_THK + BF_THK + TP_THK, Color_Top_Plate);
                    // Draw Top Flange Section
                    Draw_Curve(vdoc, "Top Flange", TF_WD, TF_THK, Y + Web_Depth + BP_THK + BF_THK, Color_Top_Flange);

                    // Draw Bottom Plate Section
                    Draw_Curve(vdoc, "Bottom Plate", BP_WD, BP_THK, Y, Color_Bottom_Plate);
                    // Draw Bottom Flange Section
                    Draw_Curve(vdoc, "Bottom Flange", BF_WD, BF_THK, Y + BP_THK, Color_Bottom_Flange);



                    double hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_1_WD) / 2;
                    // Draw Side Plate 1 Section
                    Draw_Curve(vdoc, "Side Plate 1", SP_1_THK, SP_1_WD, L, Z - Web_Thickness, Y + hgt, Color_Side_Plate_1);


                    hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_2_WD) / 2;
                    // Draw Side Plate 2 Section
                    Draw_Curve(vdoc, "Side Plate 2", SP_2_THK, SP_2_WD, L, Z - Web_Thickness - SP_1_THK, Y + hgt, Color_Side_Plate_2);


                    hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_3_WD) / 2;
                    // Draw Side Plate 3 Section
                    Draw_Curve(vdoc, "Side Plate 3", SP_3_THK, SP_3_WD, L, Z + Web_Thickness, Y + hgt, Color_Side_Plate_3);


                    hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_4_WD) / 2;
                    // Draw Side Plate 4 Section
                    Draw_Curve(vdoc, "Side Plate 4", SP_4_THK, SP_4_WD, L, Z + Web_Thickness + SP_3_THK, Y + hgt, Color_Side_Plate_4);

                    #endregion Web Section
                }

            }

        
        }
        public void Draw_Curve(vdDocument vdoc, string sectionName, double Thickness, double depth, double hgt)
        {
            Draw_Curve(vdoc, sectionName, Thickness, depth, hgt, Color.White);
        }
        public void Draw_Curve(vdDocument vdoc, string sectionName, double Thickness, double depth, double hgt, Color c)
        {


            Draw_Curve(vdoc, sectionName, Thickness, depth, L, Z, hgt, c); return;



            vdLayer ly = Add_Layer(vdoc, sectionName, c);

            if (Thickness == 0.0 || depth == 0.0) return;

            int _Columns = 100;

            List<double> lst_x = new List<double>();
            List<double> lst_z = new List<double>();


            List<double> lst_x2 = new List<double>();
            List<double> lst_z2 = new List<double>();

            for (int iCols = 0; iCols < _Columns; iCols++)
            {
                var ang_incr = ((L / _Columns) * iCols) / Curve_Radius;

                var _r = Curve_Radius - (Z + Thickness / 2);

                lst_z.Add(Curve_Radius - _r * Math.Cos(ang_incr));
                lst_x.Add(_r * Math.Sin(ang_incr));


                _r = Curve_Radius - (Z - Thickness / 2);

                lst_z2.Add(Curve_Radius - _r * Math.Cos(ang_incr));
                lst_x2.Add(_r * Math.Sin(ang_incr));
            }

            for (int iCols = 1; iCols < lst_x2.Count; iCols++)
            {
                //var pnt1 = new gPoint(lst_x[iCols - 1], Y, lst_z[iCols - 1]);
                //var pnt2 = new gPoint(lst_x[iCols], Y, lst_z[iCols]);

                //var pnt3 = new gPoint(lst_x2[iCols - 1], Y, lst_z2[iCols - 1]);
                //var pnt4 = new gPoint(lst_x2[iCols], Y, lst_z2[iCols]);

                var pnt1 = new gPoint(lst_x[iCols - 1], hgt, lst_z[iCols - 1]);
                var pnt2 = new gPoint(lst_x2[iCols - 1], hgt, lst_z2[iCols - 1]);

                var pnt4 = new gPoint(lst_x[iCols], hgt, lst_z[iCols]);

                var pnt3 = new gPoint(lst_x2[iCols], hgt, lst_z2[iCols]);

                Add_Web(vdoc, pnt1, pnt2, pnt3, pnt4, depth, c);

            }

            #region Web Section


            //Add_Web(insPnt, endPnt, vdoc, Web_Thickness, Web_Depth);


            #endregion Web Section
        }

        public void Draw_Curve(vdDocument vdoc, string sectionName, double Thickness, double depth, double length, double z, double hgt, Color c)
        {


            if(Is_Cross_Girder)
            {
                Draw_Curve_Cross(vdoc, sectionName, Thickness, depth, length, z, hgt, c);
                return;
            }
            vdLayer ly = Add_Layer(vdoc, sectionName, c);

            if (Thickness == 0.0 || depth == 0.0) return;

            //gPoint insPnt = new gPoint(X, Y, Z);
            //gPoint endPnt = new gPoint(X + L, Y, Z);


            int _Columns = 100;

            List<double> lst_x = new List<double>();
            List<double> lst_z = new List<double>();


            List<double> lst_x2 = new List<double>();
            List<double> lst_z2 = new List<double>();


            
            for (int iCols = 0; iCols <= _Columns; iCols++)
            {
                var ang_incr = ((length / _Columns) * iCols) / Curve_Radius;

                var _r = Curve_Radius - (z + Thickness / 2);

                lst_z.Add(Curve_Radius - _r * Math.Cos(ang_incr));
                lst_x.Add(_r * Math.Sin(ang_incr));


                _r = Curve_Radius - (z - Thickness / 2);

                lst_z2.Add(Curve_Radius - _r * Math.Cos(ang_incr));
                lst_x2.Add(_r * Math.Sin(ang_incr));
            }

            for (int iCols = 1; iCols < lst_x2.Count; iCols++)
            {

                //var pnt1 = new gPoint(lst_x[iCols - 1], Y, lst_z[iCols - 1]);
                //var pnt2 = new gPoint(lst_x[iCols], Y, lst_z[iCols]);


                //var pnt3 = new gPoint(lst_x2[iCols - 1], Y, lst_z2[iCols - 1]);
                //var pnt4 = new gPoint(lst_x2[iCols], Y, lst_z2[iCols]);

                var pnt1 = new gPoint(lst_x[iCols - 1], hgt, lst_z[iCols - 1]);
                var pnt2 = new gPoint(lst_x2[iCols - 1], hgt, lst_z2[iCols - 1]);

                var pnt4 = new gPoint(lst_x[iCols], hgt, lst_z[iCols]);

                var pnt3 = new gPoint(lst_x2[iCols], hgt, lst_z2[iCols]);

                Add_Web(vdoc, pnt1, pnt2, pnt3, pnt4, depth, c);

            }

            #region Web Section


            //Add_Web(insPnt, endPnt, vdoc, Web_Thickness, Web_Depth);


            #endregion Web Section
        }



        public void Draw_Curve_Cross(vdDocument vdoc, string sectionName, double Thickness, double depth, double length, double z, double hgt, Color c)
        {
            vdLayer ly = Add_Layer(vdoc, sectionName, c);

            if (Thickness == 0.0 || depth == 0.0) return;

            //gPoint insPnt = new gPoint(X, Y, Z);
            //gPoint endPnt = new gPoint(X + L, Y, Z);


            int _Columns = 12;

            List<double> lst_x = new List<double>();
            List<double> lst_z = new List<double>();


            List<double> lst_x2 = new List<double>();
            List<double> lst_z2 = new List<double>();

            for (int iCols = 0; iCols <= _Columns; iCols++)
            {
                var ang_incr = (X / Curve_Radius);

                var _r = Curve_Radius - (z + Thickness / 2);

                //lst_x.Add(X - Thickness / 2);
                //lst_x2.Add(X + Thickness / 2);

                //lst_z.Add(z + (length / _Columns) * iCols);
                //lst_z2.Add(z + (length / _Columns) * iCols);



                ang_incr = ((X - Thickness / 2) / Curve_Radius);


                _r = Curve_Radius - (z + (length / _Columns) * iCols);

                lst_z.Add(Curve_Radius - _r * Math.Cos(ang_incr));
                lst_x.Add(_r * Math.Sin(ang_incr));






                ang_incr = ((X + Thickness / 2) / Curve_Radius);


                _r = Curve_Radius - (z + (length / _Columns) * iCols);


                lst_z2.Add(Curve_Radius - _r * Math.Cos(ang_incr));
                lst_x2.Add(_r * Math.Sin(ang_incr));


            }

            for (int iCols = 1; iCols < lst_x2.Count; iCols++)
            {

                //var pnt1 = new gPoint(lst_x[iCols - 1], Y, lst_z[iCols - 1]);
                //var pnt2 = new gPoint(lst_x[iCols], Y, lst_z[iCols]);


                //var pnt3 = new gPoint(lst_x2[iCols - 1], Y, lst_z2[iCols - 1]);
                //var pnt4 = new gPoint(lst_x2[iCols], Y, lst_z2[iCols]);

                //var pnt1 = new gPoint(lst_z[iCols - 1], hgt, lst_x[iCols - 1]);
                //var pnt2 = new gPoint(lst_z2[iCols - 1], hgt, lst_x2[iCols - 1]);

                //var pnt4 = new gPoint(lst_z[iCols], hgt, lst_x[iCols]);

                //var pnt3 = new gPoint(lst_z2[iCols], hgt, lst_x2[iCols]);





                var pnt1 = new gPoint(lst_x[iCols - 1], hgt, lst_z[iCols - 1]);
                var pnt2 = new gPoint(lst_x2[iCols - 1], hgt, lst_z2[iCols - 1]);

                var pnt4 = new gPoint(lst_x[iCols], hgt, lst_z[iCols]);

                var pnt3 = new gPoint(lst_x2[iCols], hgt, lst_z2[iCols]);



                Add_Web(vdoc, pnt1, pnt2, pnt3, pnt4, depth, c);

            }

            #region Web Section


            //Add_Web(insPnt, endPnt, vdoc, Web_Thickness, Web_Depth);


            #endregion Web Section
        }

        #endregion Draw Curve


        #region Draw Straight

        public void Draw(vdDocument vdoc)
        {


            if (Is_Cross_Girder)
            {
                //Cross_Draw(vdoc);


                //double hgt = Y + Web_Depth - (BP_THK + BF_THK + TP_THK + TF_THK);
                #region Draw Section

                // Draw Web Section
                Draw(vdoc, "Cross Girders", Web_Thickness, Web_Depth, L, Z, Y + BP_THK + BF_THK, Color_Web_Plate);
                // Draw Top Plate Section
                Draw(vdoc, "Cross Girders", TP_WD, TP_THK, Y + Web_Depth + BP_THK + BF_THK + TP_THK, Color_Top_Plate);
                // Draw Top Flange Section
                Draw(vdoc, "Cross Girders", TF_WD, TF_THK, Y + Web_Depth + BP_THK + BF_THK, Color_Top_Flange);

                // Draw Bottom Plate Section
                Draw(vdoc, "Cross Girders", BP_WD, BP_THK, Y, Color_Bottom_Plate);
                // Draw Bottom Flange Section
                Draw(vdoc, "Cross Girders", BF_WD, BF_THK, Y + BP_THK, Color_Bottom_Flange);

                //double hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_1_WD) / 2;
                //// Draw Side Plate 1 Section
                //Draw(vdoc, "Cross Side Plate 1", SP_1_THK, SP_1_WD, L, Z - Web_Thickness, Y + hgt, Color_Side_Plate_1);


                //hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_2_WD) / 2;
                //// Draw Side Plate 2 Section
                //Draw(vdoc, "Cross Side Plate 2", SP_2_THK, SP_2_WD, L, Z - Web_Thickness - SP_1_THK, Y + hgt, Color_Side_Plate_2);


                //hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_3_WD) / 2;
                //// Draw Side Plate 3 Section
                //Draw(vdoc, "Cross Side Plate 3", SP_3_THK, SP_3_WD, L, Z + Web_Thickness, Y + hgt, Color_Side_Plate_3);


                //hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_4_WD) / 2;
                //// Draw Side Plate 4 Section
                //Draw(vdoc, "Cross Side Plate 4", SP_4_THK, SP_4_WD, L, Z + Web_Thickness + SP_3_THK, Y + hgt, Color_Side_Plate_4);

                #endregion Web Section
            }
            else
            {
                #region Draw Section

                // Draw Web Section
                if (TP_WD == 0.0 && BP_WD == 0.0 && TF_WD == 0.0 && BF_WD == 0.0)
                    Draw(vdoc, "Deckslab", Web_Thickness, Web_Depth, L, Z, Y + BP_THK + BF_THK, Color_Web_Plate);
                else
                    Draw(vdoc, "Web Plate", Web_Thickness, Web_Depth, L, Z, Y + BP_THK + BF_THK, Color_Web_Plate);

                // Draw Top Plate Section
                Draw(vdoc, "Top Plate", TP_WD, TP_THK, Y + Web_Depth + BP_THK + BF_THK + TP_THK, Color_Top_Plate);
                // Draw Top Flange Section
                Draw(vdoc, "Top Flange", TF_WD, TF_THK, Y + Web_Depth + BP_THK + BF_THK, Color_Top_Flange);

                // Draw Bottom Plate Section
                Draw(vdoc, "Bottom Plate", BP_WD, BP_THK, Y, Color_Bottom_Plate);
                // Draw Bottom Flange Section
                Draw(vdoc, "Bottom Flange", BF_WD, BF_THK, Y + BP_THK, Color_Bottom_Flange);



                double hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_1_WD) / 2;
                // Draw Side Plate 1 Section
                Draw(vdoc, "Side Plate 1", SP_1_THK, SP_1_WD, L, Z - Web_Thickness, Y + hgt, Color_Side_Plate_1);


                hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_2_WD) / 2;
                // Draw Side Plate 2 Section
                Draw(vdoc, "Side Plate 2", SP_2_THK, SP_2_WD, L, Z - Web_Thickness - SP_1_THK, Y + hgt, Color_Side_Plate_2);


                hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_3_WD) / 2;
                // Draw Side Plate 3 Section
                Draw(vdoc, "Side Plate 3", SP_3_THK, SP_3_WD, L, Z + Web_Thickness, Y + hgt, Color_Side_Plate_3);


                hgt = (BP_THK + BF_THK + Web_Depth + TP_THK + TF_THK - SP_4_WD) / 2;
                // Draw Side Plate 4 Section
                Draw(vdoc, "Side Plate 4", SP_4_THK, SP_4_WD, L, Z + Web_Thickness + SP_3_THK, Y + hgt, Color_Side_Plate_4);

                #endregion Web Section
            }
        }

        public void Draw(vdDocument vdoc, string sectionName, double Thickness, double depth, double hgt, Color c)
        {
            Draw(vdoc, sectionName, Thickness, depth, L, Z, hgt, c);
        }

        public void Draw(vdDocument vdoc, string sectionName, double thickness, double depth, double length, double z, double hgt, Color c)
        {
            if(Is_Cross_Girder)
            {
                Draw_Cross(vdoc, sectionName, thickness, depth, length, z, hgt, c);
                return;
            }
            vdLayer ly = Add_Layer(vdoc, sectionName, c);

            if (thickness == 0.0 || depth == 0.0) return;

            //gPoint insPnt = new gPoint(X, Y, Z);
            //gPoint endPnt = new gPoint(X + L, Y, Z);

            int _Columns = 100;

            List<double> lst_x = new List<double>();
            List<double> lst_z = new List<double>();


            List<double> lst_x2 = new List<double>();
            List<double> lst_z2 = new List<double>();

            for (int iCols = 0; iCols < _Columns; iCols++)
            {
                lst_x.Add(((length / _Columns) * iCols));
                lst_z.Add((z - thickness / 2));
                lst_z2.Add((z + thickness / 2));
            }

            for (int iCols = 1; iCols < lst_x.Count; iCols++)
            {

                //var pnt1 = new gPoint(lst_x[iCols - 1], Y, lst_z[iCols - 1]);
                //var pnt2 = new gPoint(lst_x[iCols], Y, lst_z[iCols]);


                //var pnt3 = new gPoint(lst_x2[iCols - 1], Y, lst_z2[iCols - 1]);
                //var pnt4 = new gPoint(lst_x2[iCols], Y, lst_z2[iCols]);

                var pnt1 = new gPoint(lst_x[iCols - 1], hgt, lst_z[iCols - 1]);

                var pnt2 = new gPoint(lst_x[iCols], hgt, lst_z[iCols]);

                var pnt4 = new gPoint(lst_x[iCols-1], hgt, lst_z2[iCols - 1]);

                var pnt3 = new gPoint(lst_x[iCols], hgt, lst_z2[iCols]);

                Add_Web(vdoc, pnt1, pnt2, pnt3, pnt4, depth, c);

            }

            #region Web Section


            //Add_Web(insPnt, endPnt, vdoc, Web_Thickness, Web_Depth);


            #endregion Web Section
        }


        public void Draw_Cross(vdDocument vdoc, string sectionName, double thickness, double depth, double length, double z, double hgt, Color c)
        {
            vdLayer ly = Add_Layer(vdoc, sectionName, c);

            if (thickness == 0.0 || depth == 0.0) return;

            //gPoint insPnt = new gPoint(X, Y, Z);
            //gPoint endPnt = new gPoint(X + L, Y, Z);

            int _Columns = 10;

            List<double> lst_x = new List<double>();
            List<double> lst_z = new List<double>();


            List<double> lst_x2 = new List<double>();
            List<double> lst_z2 = new List<double>();

            for (int iCols = 0; iCols <= _Columns; iCols++)
            {
                //lst_x.Add(((length / _Columns) * iCols) - thickness / 2);
                //lst_x2.Add(((length / _Columns) * iCols) + thickness / 2);

                //lst_z.Add((z));
                //lst_z2.Add((z + length));



                lst_x.Add(X - thickness / 2);
                lst_x2.Add(X + thickness / 2);

                lst_z.Add(z + (length / _Columns) * iCols);
                lst_z2.Add(z + (length / _Columns) * iCols);

            }

            for (int iCols = 1; iCols < lst_x.Count; iCols++)
            {

                //var pnt1 = new gPoint(lst_x[iCols - 1], Y, lst_z[iCols - 1]);
                //var pnt2 = new gPoint(lst_x[iCols], Y, lst_z[iCols]);


                //var pnt3 = new gPoint(lst_x2[iCols - 1], Y, lst_z2[iCols - 1]);
                //var pnt4 = new gPoint(lst_x2[iCols], Y, lst_z2[iCols]);

                var pnt1 = new gPoint(lst_x[iCols - 1], hgt, lst_z[iCols - 1]);

                var pnt2 = new gPoint(lst_x[iCols], hgt, lst_z[iCols]);

                var pnt4 = new gPoint(lst_x2[iCols - 1], hgt, lst_z2[iCols - 1]);

                var pnt3 = new gPoint(lst_x2[iCols], hgt, lst_z2[iCols]);

                Add_Web(vdoc, pnt1, pnt2, pnt3, pnt4, depth, c);

            }

            #region Web Section


            //Add_Web(insPnt, endPnt, vdoc, Web_Thickness, Web_Depth);


            #endregion Web Section
        }

        #endregion Draw Straight


        #region Add Functions

        public vdLayer Add_Layer(vdDocument doc, string layer_name, Color layer_color)
        {
            vdLayer cl = doc.Layers.FindName(layer_name);

            if (cl == null)
            {
                cl = new vdLayer(doc, layer_name);
                cl.setDocumentDefaults();
                doc.Layers.AddItem(cl);
            }
            cl.PenColor = new vdColor(layer_color);
            doc.ActiveLayer = cl;
            return cl;
        }

        private void Add_Web(vdDocument vdoc, gPoint endPnt, gPoint insPnt, double THK, double DPT)
        {

            vdPolyface pf = new vdPolyface(vdoc);
            pf.setDocumentDefaults();
            vdoc.ActiveLayOut.Entities.Add(pf);


            pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z - THK / 2));
            pf.VertexList.Add(new gPoint(endPnt.x, insPnt.y, endPnt.z - THK / 2));
            pf.VertexList.Add(new gPoint(endPnt.x, insPnt.y, endPnt.z + THK / 2)); 
            pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y, insPnt.z + THK / 2));   

            pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + DPT, insPnt.z - THK / 2));
            pf.VertexList.Add(new gPoint(endPnt.x, insPnt.y + DPT, endPnt.z - THK / 2));
            pf.VertexList.Add(new gPoint(endPnt.x, insPnt.y + DPT, endPnt.z + THK / 2));  
            pf.VertexList.Add(new gPoint(insPnt.x, insPnt.y + DPT, insPnt.z + THK / 2));
            Add_FaceList(ref pf);
            vdoc.Redraw(true);
        }

        private void Add_Web(vdDocument vdoc, gPoint pnt1, gPoint pnt2, gPoint pnt3, gPoint pnt4, double DPT, Color c)
        {

            vdPolyface pf = new vdPolyface(vdoc);
            pf.setDocumentDefaults();
            vdoc.ActiveLayOut.Entities.Add(pf);
            //pf.PenColor = new vdColor(c);
            pf.Layer.PenColor = new vdColor(c);

            pf.VertexList.Add(new gPoint(pnt1.x, pnt1.y, pnt1.z));
            pf.VertexList.Add(new gPoint(pnt2.x, pnt1.y, pnt2.z));
            pf.VertexList.Add(new gPoint(pnt3.x, pnt1.y, pnt3.z));
            pf.VertexList.Add(new gPoint(pnt4.x, pnt1.y, pnt4.z));



            pf.VertexList.Add(new gPoint(pnt1.x, pnt1.y + DPT, pnt1.z));
            pf.VertexList.Add(new gPoint(pnt2.x, pnt1.y + DPT, pnt2.z));
            pf.VertexList.Add(new gPoint(pnt3.x, pnt1.y + DPT, pnt3.z));
            pf.VertexList.Add(new gPoint(pnt4.x, pnt1.y + DPT, pnt4.z));


            Add_FaceList(ref pf);
            vdoc.Redraw(true);
        }

        public void Add_FaceList(ref vdPolyface pf)
        {
            pf.FaceList.RemoveAll();

            pf.FaceList.Add(1);
            pf.FaceList.Add(2);
            pf.FaceList.Add(3);
            pf.FaceList.Add(4);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(5);
            pf.FaceList.Add(6);
            pf.FaceList.Add(7);
            pf.FaceList.Add(8);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(1);
            pf.FaceList.Add(2);
            pf.FaceList.Add(6);
            pf.FaceList.Add(5);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(2);
            pf.FaceList.Add(3);
            pf.FaceList.Add(7);
            pf.FaceList.Add(6);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(3);
            pf.FaceList.Add(4);
            pf.FaceList.Add(8);
            pf.FaceList.Add(7);
            pf.FaceList.Add(-1);
            pf.FaceList.Add(4);
            pf.FaceList.Add(1);
            pf.FaceList.Add(5);
            pf.FaceList.Add(8);
            pf.FaceList.Add(-1);
        }

        #endregion Add Functions



        public void Draw1(vdDocument doc)
        {
            doc.ActiveLayOut.Entities.RemoveAll();
            //create and display the result Frame Rectangle as a PolyFace object.
            vdPolyface face = new vdPolyface();
            face.SetUnRegisterDocument(doc);
            face.setDocumentDefaults();

            gPoints Pathpoints = new gPoints(new gPoint[] { new gPoint(0, 0), new gPoint(10, 0), new gPoint(10, 10), new gPoint(0, 10), new gPoint(0, 0) });
            gPoints Sectionpoints = new gPoints(new gPoint[] { new gPoint(0, 0), new gPoint(1, 0), new gPoint(1, 1), new gPoint(0, 1), new gPoint(0, 0) });
            gPoint sectionOrigin = new gPoint(0.5, 0.5);
            vdArray<gPoints> Sections = new vdArray<gPoints>(new gPoints[] { Sectionpoints });

            //bool suc = face.Generate3dPathSection(Pathpoints, Sections, sectionOrigin, 0.0, 1.0, null, null);
            bool suc = false;

            //suc = face.Generate3dPathSection(Pathpoints, Sections, sectionOrigin, 0.0, 1.0);

            vdArc arc = new vdArc(doc);
            arc.setDocumentDefaults();

            arc.Center = sectionOrigin;
            arc.StartAngle = 0;
            arc.EndAngle = 75 * Math.PI / 180;
            //arc.EndAngle = Math.PI*2;
            arc.Radius = 62;
            arc.ExtrusionVector = new Vector(0, 0, 1);



            vdArc arc2 = new vdArc(doc);
            arc2.setDocumentDefaults();

            arc2.Center = new gPoint();
            arc2.StartAngle = 90 * Math.PI / 180;
            //arc2.EndAngle = 100 * Math.PI / 180;
            arc2.EndAngle = 93 * Math.PI / 180;
            //arc2.EndAngle = 2 * Math.PI;
            arc2.Radius = 12;


            //suc = face.Generate3dPathSection(arc, arc2, sectionOrigin, 0, 1.0);
            suc = face.Generate3dPathSection(arc, arc2, sectionOrigin, 10, 1.0);
            //suc = face.Generate3dPathSection(arc, arc2, sectionOrigin, 10, 1.0, new Vector(0, 0, 1), new Vector(0, 0, 0));


            //doc.Model.Entities.AddItem(arc);

            if (suc)
            {
                doc.Model.Entities.AddItem(face);
                face.Invalidate();
            }

        }

    }
}
