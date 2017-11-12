using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
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
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            Draw();
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

                 FL_THK = 0.3;
                 FL_WD = 1.0;


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
    }
}
