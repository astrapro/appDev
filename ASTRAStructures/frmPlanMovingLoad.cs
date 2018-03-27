using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;

using VDRAW = VectorDraw.Professional.ActionUtilities;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
namespace ASTRAStructures
{
    public partial class frmPlanMovingLoad : Form
    {
        IApplication iApp;

        double Skew_Angle = 0.0;
        public frmPlanMovingLoad(IApplication app, double skew_angle)
        {
            InitializeComponent();
            iApp = app;
            Skew_Angle = skew_angle;
        }

        private void btn_DrawModel_Click(object sender, EventArgs e)
        {
            Draw_Model();
        }
        vdDocument VDoc
        {
            get
            {
                return uC_CAD1.VDoc;
            }
        }
        void Draw_Model()
        {
            List<double> lst_X = new List<double>();
            List<double> lst_Y = new List<double>();


            for (int i = 0; i < 10; i++)
            {
                lst_X.Add(i);
                if (i < 5) lst_Y.Add(i);
            }

            VDoc.ActiveLayOut.Entities.RemoveAll();
            vdLine ln;

            //double Skew_Angle = 26;

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));



            double x = 0.0;
            double y = 0.0;

            for (int r = 0; r < lst_Y.Count; r++)
            {
                for (int i = 1; i < lst_X.Count; i++)
                {
                    ln = new vdLine(VDoc);
                    ln.setDocumentDefaults();


                    //ln.StartPoint = new gPoint(lst_X[i - 1], lst_Y[i - 1]);
                    //ln.EndPoint = new gPoint(lst_X[i], lst_Y[i]);

                    //x = lst_X[i - 1] ;
                    x = lst_X[i - 1] + lst_Y[r] * skew_length;

                    ln.StartPoint = new gPoint(x, lst_Y[r]);

                    x = lst_X[i] + lst_Y[r] * skew_length;


                    ln.EndPoint = new gPoint(x, lst_Y[r]);


                    VDoc.ActiveLayOut.Entities.Add(ln);
                }
            }


            for (int r = 0; r < lst_X.Count; r++)
            {
                for (int i = 1; i < lst_Y.Count; i++)
                {
                    ln = new vdLine(VDoc);
                    ln.setDocumentDefaults();

                    x = lst_X[r] + lst_Y[i - 1] * skew_length;

                    ln.StartPoint = new gPoint(x, lst_Y[i - 1]);

                    x = lst_X[r] + lst_Y[i] * skew_length;

                    ln.EndPoint = new gPoint(x, lst_Y[i]);


                    VDoc.ActiveLayOut.Entities.Add(ln);
                }
            }


            VDoc.Redraw(true);
            VDRAW.vdCommandAction.View3D_VTop(VDoc);
            VDRAW.vdCommandAction.ZoomOut_Ex(VDoc);
            VDRAW.vdCommandAction.ZoomOut_Ex(VDoc);
        }

        private void btn_Moving_Load_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btn_Moving_Load) Run_MovingLoad();
            if (btn == btn_stop)
            {
                timer1.Stop();
            }
        }


        vdRect r1;
        vdRect r2;

        List<vdRect> wheels;
        void Run_MovingLoad()
        {

            if (r1 == null)
            {

                r1 = new vdRect(VDoc);
                r1.setDocumentDefaults();
                VDoc.ActiveLayOut.Entities.Add(r1);
            
                r2 = new vdRect(VDoc);
                r2.setDocumentDefaults();
                VDoc.ActiveLayOut.Entities.Add(r2);

                wheels = new List<vdRect>();
                wheels.Add(r1);
                wheels.Add(r2);



                r1 = new vdRect(VDoc);
                r1.setDocumentDefaults();
                VDoc.ActiveLayOut.Entities.Add(r1);

                r2 = new vdRect(VDoc);
                r2.setDocumentDefaults();
                VDoc.ActiveLayOut.Entities.Add(r2);


                wheels.Add(r1);
                wheels.Add(r2);



                r1 = new vdRect(VDoc);
                r1.setDocumentDefaults();
                VDoc.ActiveLayOut.Entities.Add(r1);

                r2 = new vdRect(VDoc);
                r2.setDocumentDefaults();
                VDoc.ActiveLayOut.Entities.Add(r2);


                wheels.Add(r1);
                wheels.Add(r2);



                timer1.Interval = 500;
            }
            r1 = wheels[0];
            r1.InsertionPoint = new gPoint(0, 4-0.5);
            r1.Width = 0.3;
            r1.Height = 0.1;

            r2 = wheels[1];
            r2.InsertionPoint = new gPoint(0, 4 - 0.5 - 0.3);
            r2.Width = 0.3;
            r2.Height = 0.1;


            r1 = wheels[2];
            r1.InsertionPoint = new gPoint(0-0.5, 4 - 0.5);
            r1.Width = 0.3;
            r1.Height = 0.1;

            r2 = wheels[3];
            r2.InsertionPoint = new gPoint(0 - 0.5, 4 - 0.5 - 0.3);
            r2.Width = 0.3;
            r2.Height = 0.1;



            r1 = wheels[4];
            r1.InsertionPoint = new gPoint(0 - 0.5 - 0.5, 4 - 0.5);
            r1.Width = 0.3;
            r1.Height = 0.1;

            r2 = wheels[5];
            r2.InsertionPoint = new gPoint(0 - 0.5 - 0.5, 4 - 0.5 - 0.3);
            r2.Width = 0.3;
            r2.Height = 0.1;

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (r1 == null) return;
             timer1.Interval = (int) (MyList.StringToDouble(txt_time) * 1000);


            r1 = wheels[0];
            //if (r1.InsertionPoint.x > 12)
            //{
            //    //r1.InsertionPoint.x = 0.0;
            //    //r2.InsertionPoint.x = 0.0;


            //    foreach (var item in wheels)
            //    {
            //        item.InsertionPoint.x = 0.0;
            //    }

            //}

            foreach (var item in wheels)
            {
                item.InsertionPoint.x = item.InsertionPoint.x + 0.5;
                item.Update();

                if (item.InsertionPoint.x > 11)
                {
                    item.InsertionPoint.x = 0.0;
                }
            }

            //r1.InsertionPoint.x = r1.InsertionPoint.x + 0.5;
            //r2.InsertionPoint.x = r2.InsertionPoint.x + 0.5;
           

            //r1.Update();
            //r2.Update();
            VDoc.Redraw(true);
        }

        private void frmPlanMovingLoad_Load(object sender, EventArgs e)
        {
            Draw_Model();
        }

        private void frmPlanMovingLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timer1.Stop();
                VDoc.Dispose();
            }
            catch (Exception exx) { }
        }

    }
}
