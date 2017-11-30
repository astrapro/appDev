using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;

using VectorDraw.Actions;

namespace AstraAccess.Footing
{
    public partial class frm_RaftFoundation : Form
    {
        public frm_RaftFoundation()
        {
            InitializeComponent();
        }

        IApplication iApp;

        ColumnCollections All_Collections { get; set; }

        public frm_RaftFoundation(IApplication iapp)
        {
            InitializeComponent();
            iApp = iapp;

            All_Collections = new ColumnCollections();

            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

        }

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RAFT FOUNDATION [BS]";
                return "DESIGN OF RAFT FOUNDATION";
            }
        }
        public void Load_Default_Data()
        {
            List<string> list = new List<string>();
            //list.Add(string.Format("Column$Coordinates(X,Y)$Gridlengths(X,Y)$L(M)$B(M)$Load(kN)"));
            list.Add(string.Format("A-1$(0.0,12.0)$lx=0.0,ly=12.0$0.3$0.3$500"));
            list.Add(string.Format("B-1$(0.0,6.0)$lx=0.0,ly=6.0$0.3$0.0$600"));
            list.Add(string.Format("C-1$(0.0,0.0)$lx=0.0,ly=0.0$0.3$0.0$550"));
            //list.Add(string.Format(""));
            list.Add(string.Format("A-2$(7.0,12.0)$lx=7.0,ly=12.0$0.3$0.3$1500"));
            list.Add(string.Format("B-2$(7.0,6.0)$lx=7.0,ly=6.0$0.3$0.0$2000"));
            list.Add(string.Format("C-2$(7.0,0.0)$lx=7.0,ly=0.0$0.3$0.0$1200"));
            //list.Add(string.Format(""));
            list.Add(string.Format("A-3$(14.0,12.0)$lx=14.0,ly=12.0$0.3$0.3$1500"));
            list.Add(string.Format("B-3$(14.0,6.0)$lx=14.0,ly=6.0$0.3$0.0$2000"));
            list.Add(string.Format("C-3$(14.0,0.0)$lx=14.0,ly=0.0$0.3$0.0$1200"));
            //list.Add(string.Format(""));
            list.Add(string.Format("A-4$(21.0,12.0)$lx=21.0,ly=12.0$0.3$0.3$500"));
            list.Add(string.Format("B-4$(21.0,6.0)$lx=21.0,ly=6.0$0.3$0.0$1200"));
            list.Add(string.Format("C-4$(21.0,0.0)$lx=21.0,ly=0.0$0.3$0.0$550"));

            dgv_ColumnData.Rows.Clear();
            MyList.Fill_List_to_Grid(dgv_ColumnData, list, '$');

            cmb_fck.SelectedItem = "20";
            cmb_fy.SelectedItem = "415";

            string fileName = Path.Combine(Application.StartupPath, @"DRAWINGS\Structures\Raft Foundation\Cordinate Drawing.dwg");
            if (File.Exists(fileName))
            {
                uC_CAD1.VDoc.Open(fileName);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(uC_CAD1.VDoc);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(uC_CAD1.VDoc);
            }

        }
        public void CalculateProgram(string fileName)
        {
            List<string> list = new List<string>();


            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*            ASTRA Pro Release 22             *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*          DESIGN OF RAFT FOUNDATION          *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("Column     Coordinates (X,Y)    Grid lengths  (X,Y)         L (M)      B (M)   Load (kN)"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            foreach (var item in All_Collections)
            {
                list.Add(string.Format("{0, -10} {1, -20} {2, -22} {3, 10} {4, 10} {5, 10}",
                    item.ColumnName,
                    "(" + item.Coordinate_X.ToString("f2") + "," + item.Coordinate_Y.ToString("f2") + ")",
                    "lx=" + item.GridLength_X.ToString("f2") + "," + "ly=" + item.GridLength_Y.ToString("f2"), 
                    item.Length.ToString("f3"), 
                    item.Width.ToString("f3"), 
                    item.Load.ToString("f3")
                    ));
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));



            #region 

            double brn_cap = MyList.StringToDouble(txt_bearing_cap.Text, 0.0);
            double bar_dia = MyList.StringToDouble(txt_bar_dia.Text, 0.0);
            double cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            double fck = MyList.StringToDouble(cmb_fck.Text, 0.0);
            double fy = MyList.StringToDouble(cmb_fy.Text, 0.0);

            //list.Add(string.Format("Bearing Capacity of Soil = 65 kN/Sq.m, Bar Dia = 20 mm., Cover = 60 mm., fck =20 N/mm2, fy = 415 N/mm2"));
            list.Add(string.Format("Bearing Capacity of Soil = {0} kN/Sq.m, Bar Dia = {1} mm., Cover = {2} mm., fck = {3} N/sq.mm, fy = {4} N/sq.mm",

                brn_cap, bar_dia, cover, fck, fy
                
                ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("DESIGN CALCULATIONS:"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            List<double> All_Loads = All_Collections.Get_All_Loads();

            list.Add(string.Format("Total vertical column load "));

            string kStr = "P = " + All_Loads[0];
            int i = 0;
            double P = All_Loads[0];
            for (i = 1; i < All_Loads.Count; i++)
            {
                P += All_Loads[i];
                kStr += " + " + All_Loads[i];

            }
                //list.Add(string.Format("P = 500 + 600 + 550 + 1500 + 2000 + 1200 + 1500 + 2000 + 1200 + 500 + 1200 + 550 "));

            list.Add(string.Format(kStr));


            list.Add(string.Format("  = {0} kN", P));
            list.Add(string.Format(""));
            list.Add(string.Format("Referring to figure for Raft layout and column loads, "));
            list.Add(string.Format("Centre of Gravity of Loads along X - direction is obtained "));
            list.Add(string.Format("by taking moment of column loads about the Grid Line 1-1."));
            list.Add(string.Format(""));


            List<string> lst_CG_X = new List<string>();
            List<ColumnData> lst_cols = new List<ColumnData>();
            double x_val = 0.0;

            double sum = 0.0;

            List<double> lst_G = new List<double>();
            for (i = 0; i < All_Collections.X_Coordinates.Count; i++)
            {
                x_val = All_Collections.X_Coordinates[i];
                lst_cols = All_Collections.Get_Columns_X(x_val);

                sum = 0.0;
                for (int j = 0; j < lst_cols.Count; j++)
                {
                    var itm = lst_cols[j];
                    if(j == 0)
                    {
                        kStr = x_val.ToString("f2") + " x (" + itm.Load.ToString();
                    }
                    else
                    {
                        kStr += " + " + itm.Load.ToString();

                    }
                    sum += itm.Load;
                }

                kStr += ")";

                sum = sum * x_val;
                lst_G.Add(sum);

                
                lst_CG_X.Add(kStr);

            }



            for (i = 0; i < lst_CG_X.Count; i++)
            {
                if(i == 0)
                {
                    kStr = "[" + lst_CG_X[i];
                }
                else
                {
                    kStr += " + " + lst_CG_X[i];
                }
                
            }

            kStr += "]";

            //list.Add(string.Format("CG-X = [0x (500 + 600 + 550) + 7x (1500 + 2000 + 1200) + 14x(1500 + 2000 + 1200) + 21x (500 + 1200 + 550)] / 13300 "));
            list.Add(string.Format("CG-X = {0} / {1:f2} ", kStr, P));

            double CG_X = MyList.Get_Array_Sum(lst_G) / P;

            //list.Add(string.Format("     = 10.975 m"));
            list.Add(string.Format("     = {0:f3} m", CG_X));
            list.Add(string.Format(""));



            List<double> lst_X_dist = new List<double>();
            for (i = 1; i < All_Collections.X_Coordinates.Count; i++)
            {
                x_val = All_Collections.X_Coordinates[i] - All_Collections.X_Coordinates[i - 1];
                lst_X_dist.Add(x_val);
                if(i == 1)
                {
                    kStr = x_val.ToString("f2");
                }
                else
                {
                    kStr += " + " + x_val.ToString("f2");
                }
            }


            double GEO_X = MyList.Get_Array_Sum(lst_X_dist) / (2);

            //list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = (7.0 + 7.0 + 7.0) / 2 = 21.0 / 2 =  10.5 m. "));

            list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = ({0}) / {1} = {2} / {1} =  {3:f2} m. ", kStr, 2, MyList.Get_Array_Sum(lst_X_dist), GEO_X));

            list.Add(string.Format(""));
            list.Add(string.Format("Eccentricity along X- direction "));
            list.Add(string.Format(""));

            double eX = CG_X - GEO_X;
            //list.Add(string.Format("eX = CG-X  -  GEO-X = 10.975 - 10.50 = 0.475 m"));
            list.Add(string.Format("eX = CG-X  -  GEO-X = {0:f3} - {1:f3} = {2:f3} m", CG_X, GEO_X, eX));
            list.Add(string.Format(""));
            list.Add(string.Format("Referring to figure for Raft layout and column loads, "));
            list.Add(string.Format("Centre of Gravity of Loads along Y- direction is obtained "));
            list.Add(string.Format("by taking moment of column loads about the Grid Line C-C."));
            list.Add(string.Format(""));



            #region CG-Y
            lst_G = new List<double>();
            double y_val = 0.0;
            List<string> lst_CG_Y = new List<string>();

            for (i = 0; i < All_Collections.Y_Coordinates.Count; i++)
            {
                y_val = All_Collections.Y_Coordinates[i];
                lst_cols = All_Collections.Get_Columns_Y(y_val);

                sum = 0.0;
                for (int j = 0; j < lst_cols.Count; j++)
                {
                    var itm = lst_cols[j];
                    if (j == 0)
                    {
                        kStr = y_val.ToString("f2") + " x (" + itm.Load.ToString();
                    }
                    else
                    {
                        kStr += " + " + itm.Load.ToString();

                    }
                    sum += itm.Load;
                }

                kStr += ")";

                sum = sum * y_val;
                lst_G.Add(sum);


                lst_CG_Y.Add(kStr);

            }



            for (i = 0; i < lst_CG_Y.Count; i++)
            {
                if (i == 0)
                {
                    kStr = "[" + lst_CG_Y[i];
                }
                else
                {
                    kStr += " + " + lst_CG_Y[i];
                }

            }

            kStr += "]";

            //list.Add(string.Format("CG-Y = 6 x (500+2000+2000+1200) + 12 x (500+1500+1500+500) / 13300 = 6.005 m "));
            //list.Add(string.Format("CG-Y = 6 x (500+2000+2000+1200) + 12 x (500+1500+1500+500) / 13300 = 6.005 m "));
            list.Add(string.Format("CG-Y = {0} / {1:f2} ", kStr, P));

            double CG_Y = MyList.Get_Array_Sum(lst_G) / P;
            list.Add(string.Format("     = {0:f3} ", CG_Y));
            #endregion CG-Y



            #region GEO Y


            List<double> lst_Y_dist = new List<double>();
            for (i = 1; i < All_Collections.Y_Coordinates.Count; i++)
            {
                y_val = Math.Abs(All_Collections.Y_Coordinates[i] - All_Collections.Y_Coordinates[i - 1]);
                lst_Y_dist.Add(y_val);
                if (i == 1)
                {
                    kStr = y_val.ToString("f2");
                }
                else
                {
                    kStr += " + " + y_val.ToString("f2");
                }
            }


            double GEO_Y = MyList.Get_Array_Sum(lst_Y_dist) / 2;

            //list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = (7.0 + 7.0 + 7.0) / 2 = 21.0 / 2 =  10.5 m. "));

            list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = ({0}) / {1} = {2} / {1} =  {3:f2} m. ", kStr, 2, MyList.Get_Array_Sum(lst_Y_dist), GEO_Y));

            #endregion GEO Y



            //list.Add(string.Format("CG-Y = 6 x (500+2000+2000+1200) + 12 x (500+1500+1500+500) / 13300 = 6.005 m "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Geometric mid-point Y - direction = GEO-Y =  6.0 m."));

            //GEO_Y = 6.0;


            list.Add(string.Format(""));

            double ey = CG_Y - GEO_Y;
            list.Add(string.Format("Eccentricity along Y - direction                "));
            //list.Add(string.Format("ey = 6.005-6.0 = 0.225 m"));
            list.Add(string.Format("ey = {0:f3}-{1:f3} = {2:f3} m", CG_Y, GEO_Y, ey));
            list.Add(string.Format(""));




            list.Add(string.Format("Length from left face of left most column to right face of right most column "));


            double L = 0.0;

            #region L
            lst_cols.Clear();

            List<ColumnData> col_d = All_Collections.Get_Columns_Y(All_Collections.Y_Coordinates[0]);
            for (i = 0; i < col_d.Count; i++)
            {
                if (i == 0)
                {
                    L = 0.0;
                    L += col_d[i].Length;
                    kStr = col_d[i].Length.ToString("f2");
                }
                else
                {
                    L += Math.Abs(col_d[i].GridLength_X - col_d[i - 1].GridLength_X);
                    kStr += " + " + Math.Abs(col_d[i].GridLength_X - col_d[i - 1].GridLength_X).ToString("f2");
                }

                if (i == col_d.Count - 1)
                {
                    L += col_d[i].Length;
                    kStr += " + "  + col_d[i].Length.ToString("f2");
                }
            }


            #endregion L

            //L = 21.6;

            //list.Add(string.Format("L = 0.3+7.0+7.0+7.0+0.3=21.6 m."));
            list.Add(string.Format("L = {0} = {1:f3} m.", kStr, L));

            
            double B = 12.6;
            col_d = All_Collections.Get_Columns_X(All_Collections.X_Coordinates[0]);

            kStr = "";
            for (i = 0; i < col_d.Count; i++)
            {
                if (i == 0)
                {
                    B = 0.0;
                    B += col_d[i].Width;
                    kStr = col_d[i].Width.ToString("f2");
                }
                else
                {
                    B += Math.Abs(col_d[i].GridLength_Y - col_d[i - 1].GridLength_Y);
                    kStr += " + "  + Math.Abs(col_d[i].GridLength_Y - col_d[i - 1].GridLength_Y).ToString("f2") ;
                }

                if (i == col_d.Count - 1)
                {
                    B += col_d[i].Width;
                    kStr += " + "  +  col_d[i].Width.ToString("f2");
                }
            }

            list.Add(string.Format("Width from lower face of  bottom most column to upper face of top most column "));
            //list.Add(string.Format("B = 0.3+6.0+6.0+0.3=12.6 m."));
            list.Add(string.Format("B = {0} = {1:f3} m.", kStr, B));
            list.Add(string.Format(""));



            double IX = L * B * B * B / 12.0;
            list.Add(string.Format("IX = L x B^3 / 12  = {0:f3} x ({1:f3}^3 / 12) = {2:f3} m^4", L, B, IX));
            list.Add(string.Format(""));

            double IY = B * L * L * L / 12.0;
            list.Add(string.Format("Iy = B x L^3 / 12  = {0:f3} x ({1:f3}^3 / 12) = {2:f3} m^4", B, L, IY));
            list.Add(string.Format(""));

            double A = L * B;
            list.Add(string.Format("A = L x B = {0:f3} x {1:f3}  = {2:f3} m^2", L, B, A));
            list.Add(string.Format(""));

            double MX = P * ey;
            double MY = P * eX;
            list.Add(string.Format("MX = P x eY = {0:f3} x {1:f3} = {2:f3} kNm", P, ey, MX));
            list.Add(string.Format("My = P x eX = {0:f3} x {1:f3} = {2:f3} kNm", P, eX, MY));
            list.Add(string.Format(""));

            double p = P / A;
            list.Add(string.Format("p = P/A = {0:f3} / {1:f3} = {2:f3} kN/m^2", P, A, p));
            list.Add(string.Format(""));
            list.Add(string.Format("Soil pressure at different points is as follows :"));
            list.Add(string.Format("Σ = (P/A)   +/-    (My / Iy) x X   +/-   (Mx / Ix) x Y "));
            list.Add(string.Format(""));

            double X = L / 2;
            double Y = B / 2;
            list.Add(string.Format("X = {0:f3} / 2 = {1:f3} m.", L, X));
            list.Add(string.Format("Y = {0:f3} / 2 = {0:f3} m.", B, Y));
            list.Add(string.Format(""));

            list.Add(string.Format("p = P/A = {0:f3} kN/Sq.m", p));


            x_val = All_Collections.Get_Max_X();

            ColumnCollections cols = new ColumnCollections(All_Collections.Get_Columns_X(x_val));
            double pmy = MY * X / IY;
            double pmx = MY * X / IY;



            ColumnData cd = cols[0];





            //pmy = MY * ((cd.Coordinate_X - X + (2.0 * cd.Length))) / IY;
            pmy = MY * X / IY;
            list.Add(string.Format("pmy = {0:f3} x {1:f3} / {2:f3} = {3:f3} kN/Sq.m", MY, X, IY, pmy));

             pmx = MX * Y / IX;

            list.Add(string.Format("pmx = {0:f3} x {1:f3} / {2:f3} = {3:f3} kN/Sq.m", MX, Y, IX, pmx));



            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Corner ({0}), Soil pressure", cd.ColumnName));


            double sigma_A4 = p + pmy + pmx;


            cd.Sigma = sigma_A4;

            //list.Add(string.Format("σ(A-4) = 48.87 + 6.435 + 5.236 = 60.541 < 65 kNm^2"));
            if (sigma_A4 < brn_cap)
                list.Add(string.Format("σ({0}) = {1:f3} + {2:f3} + {3:f3} = {4:f3} < {5:f3} kNm/m^2", cd.ColumnName, p, pmy, pmx, sigma_A4, brn_cap));
            else
                list.Add(string.Format("σ({0}) = {1:f3} + {2:f3} + {3:f3} = {4:f3} > {5:f3} kNm/m^2", cd.ColumnName, p, pmy, pmx, sigma_A4, brn_cap));
            list.Add(string.Format(""));




            cd = cols[cols.Count - 1];


            double sigma_C4 = p + pmy - pmx;


            cd.Sigma = sigma_C4;
            list.Add(string.Format("Corner {0}, Soil pressure", cd.ColumnName));
            //list.Add(string.Format("σ(C-4) = 48.87 + 6.435 - 5.236 = 50.069 kNm/m2"));


            list.Add(string.Format("σ({0}) = {1:f3} + {2:f3} - {3:f3} = {4:f3} kNm/m^2", cd.ColumnName, p, pmy, pmx, sigma_C4));
            list.Add(string.Format(""));



            x_val = All_Collections.Get_Min_X();

            cols = new ColumnCollections( All_Collections.Get_Columns_X(x_val));

            double sigma_A1 = p - pmy + pmx;


            cd = cols[0];
            cd.Sigma = sigma_A1;


            list.Add(string.Format("Corner {0}, Soil pressure", cd.ColumnName));
            //list.Add(string.Format("σ(A-I)  = 48.87 - 6.435 + 5.236 = 47.671 kNm/m^2"));
            list.Add(string.Format("σ({0})  = {1:f3} - {2:f3} + {3:f3} = {4:f3} kNm/m^2", cd.ColumnName, p, pmy, pmx, sigma_A1));
            list.Add(string.Format(""));

            double sigma_C1 = p - pmy - pmx;



            cd = cols[cols.Count - 1];
            cd.Sigma = sigma_C1;

            list.Add(string.Format("Corner {0}, Soil pressure", cd.ColumnName));
            list.Add(string.Format("σ({0})  = {1:f3} - {2:f3} - {3:f3} = {4:f3} kNm/m^2", cd.ColumnName, p, pmy, pmx, sigma_C1));
            list.Add(string.Format(""));



            double sigma_B4 = p + pmy - 0;

            list.Add(string.Format("Grid B-4, Soil pressure"));
            list.Add(string.Format("σ(B-4) = {0:f3} + {1:f3} + 0 = {2:f3} kNm/m^2", p, pmy, sigma_B4));
            list.Add(string.Format(""));

            double sigma_B1 = p - pmy - 0;

            list.Add(string.Format("Grid B-I, Soil pressure"));
            //list.Add(string.Format("σ(B-I) = 48.87 - 6.435 + 0 = 42.435 kNm/m^2"));
            list.Add(string.Format("σ(B-I) = {0:f3} - {1:f3} + 0 = {2:f3} kNm/m^2", p, pmy, sigma_B1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("In the x-direction, the raft is divided in three strips, that is, three equivalent beams :"));
            list.Add(string.Format(""));









            double p1 = sigma_A4;
            list.Add(string.Format("(i) Beam A-A with 3.3 m width and p1 "));

            list.Add(string.Format("σ(A-4) = soil pressure of {0:f3} kNm/m^2", p1));
            list.Add(string.Format(""));

            double p2 = (sigma_A4 + sigma_B4) / 2;
            list.Add(string.Format("(ii) Beam B-B with 6.0 m width and p2 ="));
            list.Add(string.Format("(σ(A-4) + σ(B-4))/2 = soil pressure of ({0:f3} + {1:f3})/2 = {2:f3} kNm/m^2", sigma_A4, sigma_B4, p2));
            list.Add(string.Format(""));

            double p3 = sigma_C4;
            list.Add(string.Format("(iii) Beam C-C with 3.3 m width and p3 "));
            list.Add(string.Format("σc-4 = soil pressure of {0:f3} = {0:f3} kNm/m^2", p3));
            list.Add(string.Format(""));
            list.Add(string.Format("The bending moment is obtained by using a coefficient of 1/10 and L as centre of column distance,"));
            list.Add(string.Format(""));
            list.Add(string.Format("                + M  =  - M = wL^2/10"));
            list.Add(string.Format(""));


            double large_span = 7.0;

            double BM1 = p1 * large_span * large_span / 10.0;
            list.Add(string.Format("For strip A-A, largest span = {0:f2} m,", large_span));
            //list.Add(string.Format("Maximum moment = BM1 = p1 x 7^2 / 10  = 60.54 x 7^2 / 10 = 296.60 kNm/m2"));
            list.Add(string.Format("Maximum moment = BM1 = p1 x {0:f2}^2 / 10  = {1:f2} x {0:f2}^2 / 10 = {2:f2} kNm/m^2", large_span, p1, BM1));
            list.Add(string.Format(""));

            double BM2 = p2 * large_span * large_span / 10;
            list.Add(string.Format("For strip B-B, largest span = {0:f3} m,", large_span));
            list.Add(string.Format("Maximum moment = BM2 = p2 x {0:f3}^2 / 10  = {1:f3} x {0:f3}^2 / 10 = {2:f3} kNm/m^2", p2, large_span, BM2));
            list.Add(string.Format(""));

            double BM3 = p3 * large_span * large_span / 10;

            list.Add(string.Format("For strip C-C, largest span = {0:f3} m,", large_span));
            list.Add(string.Format("Maximum moment = BM3 = p3 x {0:f3}^2 / 10  = {1:f3} x {0:f3}^2 / 10 = {2:f3} kNm/m^2", p3, large_span, BM3));
            list.Add(string.Format(""));
            list.Add(string.Format("For any strip in the y-direction, take M = (W x I^2)/8,   because there it is a two span equivalent beam."));
            list.Add(string.Format(""));


            //list.Add(string.Format("For strip 4-4, largest span = 6.0m,"));
            //list.Add(string.Format("Maximum moment = p1 x 6^2 / 8  = 60.54 x 62 / 8 = 272.4 kNm/m2"));

            large_span = 6.0;
            list.Add(string.Format("For strip 4-4, largest span = {0:f3} m,", large_span));
            double Max_moment = p1 * large_span * large_span / 8;
            list.Add(string.Format("Maximum moment = p1 x {0:f3}^2 / 8  = {1:f3} x {0:f3}^2 / 8 = {2:f3} kNm/m^2", large_span, p1, Max_moment));
            list.Add(string.Format(""));
            //list.Add(string.Format("and so on."));
            list.Add(string.Format(""));
            list.Add(string.Format("The depth of the raft will be governed by two way shear at one of the exterior columns."));
            list.Add(string.Format("In case location of critical shear is not obvious, it may be necessary to check all possible locations."));
            list.Add(string.Format(""));

            double tau_c = 0.25 * Math.Sqrt(fck);
            //list.Add(string.Format("Shear strength of concrete τc'  = τc = 0.25 √ fck = 0.25 √ 20 = 1.11 N/mm2"));
            list.Add(string.Format("Shear strength of concrete τc'  = τc = 0.25 √ fck = 0.25 √{0} = {1:f3} N/mm^2", fck, tau_c));
            list.Add(string.Format(""));
            list.Add(string.Format("For a corner column (say C- I)"));
            list.Add(string.Format("Perimeter bo =  2 (d/2 + 450) = d + 900 mm (Fig. a)"));
            list.Add(string.Format(""));

            double P_C1 = 550;

            list.Add(string.Format("Vu = 1.5 x Pc-1 x 1000,   bo = d"));
            list.Add(string.Format("τv  = Vu / [(bo + 900) x d] = τc'"));
            list.Add(string.Format("i.e,  1.5 x Pc-1 x 1000 / [(d + 900) x d] = τc'"));
            //list.Add(string.Format("or,  1.5 x 550 x 1000 / [d2 + 900xd] = 1.11"));
            list.Add(string.Format("or,  1.5 x {0:f2} x 1000 / [d^2 + 900xd] = {1:f3}", P_C1, tau_c));



            double _c = (1.5 * P_C1 * 1000 / tau_c);
            //double _c = (1.5 * P_C1 * 1000 / 1.11);
 
            //list.Add(string.Format("or,   d^2 + 900 d - 743243.243 = 0"));
            //list.Add(string.Format("or,   d  =  [- 900 + √(9002 + 4 x 1.5 x Pc-1 x 1000 x τc')]/[2]"));
            //list.Add(string.Format("         =  [- 900 + √(9002 + 4 x 1.5 x 550 x 1000 x 1.11)]/[2] = 522.493 mm "));


            list.Add(string.Format("or,   d^2 + 900 d - {0:f3} = 0", _c));

            double _val1 = ((-900) + Math.Sqrt((900 * 900) - (4 * 1 * (-_c)))) / 2.0;
            double _val2 = ((-900) - Math.Sqrt((900 * 900) - (4 * 1 * (-_c)))) / 2.0;

            double d = _val1;
            list.Add(string.Format("or,   d  =  [- 900 + √(900^2 + 4 x 1.5 x Pc-1 x 1000 x τc')]/[2]"));
            list.Add(string.Format("         =  [- 900 + √(900^2 + 4 x 1.5 x {0:f2} x 1000 x {1:f3})]/[2] = {2:f2} mm ", P_C1, tau_c, d));
            list.Add(string.Format(""));
            list.Add(string.Format("For a slide column (say A-2)"));


            list.Add(string.Format("Perimeter bo  =  2 (0.5 d + 450) + (d + 300) = 2 d + 1200 (Fig. b)"));
            list.Add(string.Format(""));

            double P_A2 = 1500;

            list.Add(string.Format("Vu = 1.5 x [P(A-2)] x 1000,   bo = d"));
            list.Add(string.Format("τv  = Vu / [(bo + 900) x d] = τc'"));
            list.Add(string.Format("i.e,  1.5 x [P(A-2)] x 1000 / [(2 x d + 1200) x d] = τc'"));
            list.Add(string.Format("or,   1.5 x {0:f2} x 1000 /  [2xd^2 +  1200xd] = {1:f2}", P_A2, tau_c));


            list.Add(string.Format("or,   d^2 + 600 x d - (1.5 x {0:f2} x 1000 / ({0:f2} x 2) = 0", P_A2, tau_c));

            tau_c = 1.11;
            _val1 = (-600 + Math.Sqrt(600 * 600 + 4 * 1.5 * P_A2 * 1000 * tau_c)) / 2.0;

             _c = (1.5 * P_A2 * 1000 / tau_c)/2;
             _val1 = ((-600) + Math.Sqrt((600 * 600) - (4 * 1 * (-_c)))) / 2.0;

            double d2 = _val1;
            list.Add(string.Format("or,   d  =  [- 600 + √(600^2 + 4 x 1.5 x PA-2 x 1000 x τc')]/[2] "));
            list.Add(string.Format("         =  [- 600 + √(600^2 + 4 x 1.5 x {0:f3} x 1000 x {1:f3})]/[2] = {2:f3} mm", P_A2, tau_c, d2));
            list.Add(string.Format(""));


            d2 = (int)(d2 / 10) + 1;

            d2 = (int)(d2 * 10);

            list.Add(string.Format("Adopt effective depth = {0} mm, ", d2));

            double d1 = d;

            d = d2;

            double overall_dep = d + cover;

            list.Add(string.Format(""));
            list.Add(string.Format("Hence the overall depth = Adopt effective depth + Cover = {0} + {1} = {2} mm.", d, cover, overall_dep));
            list.Add(string.Format(""));
            list.Add(string.Format("Reinforcements in the long direction is derived from the equation,"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("BM = 0.87 x fy x At x [d - fy x At / (fck x b)], where  BM = BM1 x 10^6 = 296.60 x 10^6 Nmm."));
            //list.Add(string.Format("At = ((d x fck x b)  +/-  √ [(d x fck x b)2 - (4 x fy x BM1 x fck x b) / (0.87 x fy)]) / (2 x fy)"));
            //list.Add(string.Format("= {(760 x 20 x 1000)  +/-  √ [(760 x 20 x 1000)2  -  (4 x 415 x 296.60 x 1000000 x 20 x 1000) / (0.87 x 415)]}/(2 x 415)"));
            //list.Add(string.Format("= {15200000 +/-  √ [231.04 x 1012 - 9847.12 x 1012/361.05]}/830"));
            //list.Add(string.Format("= {15200000 +/-  √ [231.04 x 1012 - 27.274x 1012]}/830"));
            //list.Add(string.Format("= {15200000 +/-  √ 203.766 x 1012}/830"));
            //list.Add(string.Format("= {15200000 +/-  14274662.868}/830"));
            //list.Add(string.Format("= 925337.132 / 830"));
            //list.Add(string.Format("= 1114.864 Sq.mm"));
            //list.Add(string.Format("Minimum steel = 0.12% of 820 x 1000 = 984 Sq.mm"));
            //list.Add(string.Format("Provide 20mm dia Rebars @ spacing 200 mm c/c.  (1884.96 Sq.mm)"));



            list.Add(string.Format("BM = 0.87 x fy x At x [d - fy x At / (fck x b)], where  BM = BM1 x 10^6 = {0:f3} x 10^6 Nmm.", BM1));
            list.Add(string.Format("At = ((d x fck x b)  +/-  √ [(d x fck x b)^2 - (4 x fy x BM1 x fck x b) / (0.87 x fy)]) / (2 x fy)"));



            //list.Add(string.Format("   = {(760 x 20 x 1000)  +/-  √ [(760 x 20 x 1000)^2  -  (4 x 415 x 296.60 x 1000000 x 20 x 1000) / (0.87 x 415)]}/(2 x 415)"));
            list.Add(string.Format("   = [({0} x {1} x 1000)  +/-  √ [({0} x {1} x 1000)^2  -  (4 x {2} x {3:f3} x 1000000 x {1} x 1000) / (0.87 x {2})]]/(2 x {2})", d, fck, fy, BM1));

            //fck = 20;
            //fy = 415;
            //_val1 = (d * 20 * 1000);

            //double _At =  ((d * fck * b)  +/-  √ [(d x fck x b)^2 - (4 x fy x BM1 x fck x b) / (0.87 x fy)]) / (2 x fy)
            double _At = ((d * fck * 1000) - Math.Sqrt((d * fck * 1000) * (d * fck * 1000) - (4 * fy * BM1 * 1000000 * fck * 1000) / (0.87 * fy))) / (2 * fy);

            //list.Add(string.Format("   = {15200000 +/-  √ [231.04 x 10^12 - 9847.12 x 10^12/361.05]}/830"));
            //list.Add(string.Format("   = {15200000 +/-  √ [231.04 x 10^12 - 27.274x 10^12]}/830"));
            //list.Add(string.Format("   = {15200000 +/-  √ 203.766 x 10^12}/830"));
            //list.Add(string.Format("   = {15200000 +/-  14274662.868}/830"));
            //list.Add(string.Format("   = 925337.132 / 830"));
            list.Add(string.Format("   = {0:f3} Sq.mm, ", _At));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double min_steel = overall_dep * 1000 * 0.12 / 100;

            //list.Add(string.Format("Minimum steel = 0.12% of 820 x 1000 = 984 Sq.mm"));
            //list.Add(string.Format("Provide 20mm dia Rebars @ spacing 200 mm c/c.  (1884.96 Sq.mm)"));



            list.Add(string.Format("Minimum steel = 0.12% of {0} x 1000 = {1:f2} Sq.mm", overall_dep, min_steel));


            double bar_A = Math.PI * bar_dia * bar_dia / 4;



            double bar_nos = min_steel / bar_A;

            bar_nos = (int)(bar_nos + 2);


            double spac = (int) (1000 / bar_nos);


            list.Add(string.Format("Provide {0}mm dia Rebars @ spacing {1} mm c/c.  ({2:f3} Sq.mm)", bar_dia, spac, bar_nos * bar_A));

            list.Add(string.Format(""));
            #endregion


            list.Add(string.Format("-------------------------------------------------------------"));
            list.Add(string.Format("                         END OF DESIGN "));
            list.Add(string.Format("-------------------------------------------------------------"));


            File.WriteAllLines(fileName, list.ToArray());
        }


        public void CalculateProgram_2017_04_10(string fileName)
        {
            List<string> list = new List<string>();


            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*            ASTRA Pro Release 22             *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*          DESIGN OF RAFT FOUNDATION          *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("Column     Coordinates (X,Y)    Grid lengths  (X,Y)         L (M)      B (M)   Load (kN)"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            foreach (var item in All_Collections)
            {
                list.Add(string.Format("{0, -10} {1, -20} {2, -22} {3, 10} {4, 10} {5, 10}",
                    item.ColumnName,
                    "(" + item.Coordinate_X.ToString("f2") + "," + item.Coordinate_Y.ToString("f2") + ")",
                    "lx=" + item.GridLength_X.ToString("f2") + "," + "ly=" + item.GridLength_Y.ToString("f2"),
                    item.Length.ToString("f3"),
                    item.Width.ToString("f3"),
                    item.Load.ToString("f3")
                    ));
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));



            #region

            double brn_cap = MyList.StringToDouble(txt_bearing_cap.Text, 0.0);
            double bar_dia = MyList.StringToDouble(txt_bar_dia.Text, 0.0);
            double cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            double fck = MyList.StringToDouble(cmb_fck.Text, 0.0);
            double fy = MyList.StringToDouble(cmb_fy.Text, 0.0);

            //list.Add(string.Format("Bearing Capacity of Soil = 65 kN/Sq.m, Bar Dia = 20 mm., Cover = 60 mm., fck =20 N/mm2, fy = 415 N/mm2"));
            list.Add(string.Format("Bearing Capacity of Soil = {0} kN/Sq.m, Bar Dia = {1} mm., Cover = {2} mm., fck = {3} N/sq.mm, fy = {4} N/sq.mm",

                brn_cap, bar_dia, cover, fck, fy

                ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("DESIGN CALCULATIONS:"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            List<double> All_Loads = All_Collections.Get_All_Loads();

            list.Add(string.Format("Total vertical column load "));

            string kStr = "P = " + All_Loads[0];
            int i = 0;
            double P = All_Loads[0];
            for (i = 1; i < All_Loads.Count; i++)
            {
                P += All_Loads[i];
                kStr += " + " + All_Loads[i];

            }
            //list.Add(string.Format("P = 500 + 600 + 550 + 1500 + 2000 + 1200 + 1500 + 2000 + 1200 + 500 + 1200 + 550 "));

            list.Add(string.Format(kStr));


            list.Add(string.Format("  = {0} kN", P));
            list.Add(string.Format(""));
            list.Add(string.Format("Referring to figure for Raft layout and column loads, "));
            list.Add(string.Format("Centre of Gravity of Loads along X - direction is obtained "));
            list.Add(string.Format("by taking moment of column loads about the Grid Line 1-1."));
            list.Add(string.Format(""));


            List<string> lst_CG_X = new List<string>();
            List<ColumnData> lst_cols = new List<ColumnData>();
            double x_val = 0.0;

            double sum = 0.0;

            List<double> lst_G = new List<double>();
            for (i = 0; i < All_Collections.X_Coordinates.Count; i++)
            {
                x_val = All_Collections.X_Coordinates[i];
                lst_cols = All_Collections.Get_Columns_X(x_val);

                sum = 0.0;
                for (int j = 0; j < lst_cols.Count; j++)
                {
                    var itm = lst_cols[j];
                    if (j == 0)
                    {
                        kStr = x_val.ToString("f2") + " x (" + itm.Load.ToString();
                    }
                    else
                    {
                        kStr += " + " + itm.Load.ToString();

                    }
                    sum += itm.Load;
                }

                kStr += ")";

                sum = sum * x_val;
                lst_G.Add(sum);


                lst_CG_X.Add(kStr);

            }



            for (i = 0; i < lst_CG_X.Count; i++)
            {
                if (i == 0)
                {
                    kStr = "[" + lst_CG_X[i];
                }
                else
                {
                    kStr += " + " + lst_CG_X[i];
                }

            }

            kStr += "]";

            //list.Add(string.Format("CG-X = [0x (500 + 600 + 550) + 7x (1500 + 2000 + 1200) + 14x(1500 + 2000 + 1200) + 21x (500 + 1200 + 550)] / 13300 "));
            list.Add(string.Format("CG-X = {0} / {1:f2} ", kStr, P));

            double CG_X = MyList.Get_Array_Sum(lst_G) / P;

            //list.Add(string.Format("     = 10.975 m"));
            list.Add(string.Format("     = {0:f3} m", CG_X));
            list.Add(string.Format(""));



            List<double> lst_X_dist = new List<double>();
            for (i = 1; i < All_Collections.X_Coordinates.Count; i++)
            {
                x_val = All_Collections.X_Coordinates[i] - All_Collections.X_Coordinates[i - 1];
                lst_X_dist.Add(x_val);
                if (i == 1)
                {
                    kStr = x_val.ToString("f2");
                }
                else
                {
                    kStr += " + " + x_val.ToString("f2");
                }
            }


            double GEO_X = MyList.Get_Array_Sum(lst_X_dist) / (2);

            //list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = (7.0 + 7.0 + 7.0) / 2 = 21.0 / 2 =  10.5 m. "));

            list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = ({0}) / {1} = {2} / {1} =  {3:f2} m. ", kStr, 2, MyList.Get_Array_Sum(lst_X_dist), GEO_X));

            list.Add(string.Format(""));
            list.Add(string.Format("Eccentricity along X- direction "));
            list.Add(string.Format(""));

            double eX = CG_X - GEO_X;
            //list.Add(string.Format("eX = CG-X  -  GEO-X = 10.975 - 10.50 = 0.475 m"));
            list.Add(string.Format("eX = CG-X  -  GEO-X = {0:f3} - {1:f3} = {2:f3} m", CG_X, GEO_X, eX));
            list.Add(string.Format(""));
            list.Add(string.Format("Referring to figure for Raft layout and column loads, "));
            list.Add(string.Format("Centre of Gravity of Loads along Y- direction is obtained "));
            list.Add(string.Format("by taking moment of column loads about the Grid Line C-C."));
            list.Add(string.Format(""));



            #region CG-Y
            lst_G = new List<double>();
            double y_val = 0.0;
            List<string> lst_CG_Y = new List<string>();

            for (i = 0; i < All_Collections.Y_Coordinates.Count; i++)
            {
                y_val = All_Collections.Y_Coordinates[i];
                lst_cols = All_Collections.Get_Columns_Y(y_val);

                sum = 0.0;
                for (int j = 0; j < lst_cols.Count; j++)
                {
                    var itm = lst_cols[j];
                    if (j == 0)
                    {
                        kStr = y_val.ToString("f2") + " x (" + itm.Load.ToString();
                    }
                    else
                    {
                        kStr += " + " + itm.Load.ToString();

                    }
                    sum += itm.Load;
                }

                kStr += ")";

                sum = sum * y_val;
                lst_G.Add(sum);


                lst_CG_Y.Add(kStr);

            }



            for (i = 0; i < lst_CG_Y.Count; i++)
            {
                if (i == 0)
                {
                    kStr = "[" + lst_CG_Y[i];
                }
                else
                {
                    kStr += " + " + lst_CG_Y[i];
                }

            }

            kStr += "]";

            //list.Add(string.Format("CG-Y = 6 x (500+2000+2000+1200) + 12 x (500+1500+1500+500) / 13300 = 6.005 m "));
            //list.Add(string.Format("CG-Y = 6 x (500+2000+2000+1200) + 12 x (500+1500+1500+500) / 13300 = 6.005 m "));
            list.Add(string.Format("CG-Y = {0} / {1:f2} ", kStr, P));

            double CG_Y = MyList.Get_Array_Sum(lst_G) / P;
            list.Add(string.Format("     = {0:f3} ", CG_Y));
            #endregion CG-Y



            #region GEO Y


            List<double> lst_Y_dist = new List<double>();
            for (i = 1; i < All_Collections.Y_Coordinates.Count; i++)
            {
                y_val = Math.Abs(All_Collections.Y_Coordinates[i] - All_Collections.Y_Coordinates[i - 1]);
                lst_Y_dist.Add(y_val);
                if (i == 1)
                {
                    kStr = y_val.ToString("f2");
                }
                else
                {
                    kStr += " + " + y_val.ToString("f2");
                }
            }


            double GEO_Y = MyList.Get_Array_Sum(lst_Y_dist) / 2;

            //list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = (7.0 + 7.0 + 7.0) / 2 = 21.0 / 2 =  10.5 m. "));

            list.Add(string.Format("Geometric mid-point Y - direction = GEO-X = ({0}) / {1} = {2} / {1} =  {3:f2} m. ", kStr, 2, MyList.Get_Array_Sum(lst_Y_dist), GEO_Y));

            #endregion GEO Y



            //list.Add(string.Format("CG-Y = 6 x (500+2000+2000+1200) + 12 x (500+1500+1500+500) / 13300 = 6.005 m "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Geometric mid-point Y - direction = GEO-Y =  6.0 m."));

            //GEO_Y = 6.0;


            list.Add(string.Format(""));

            double ey = CG_Y - GEO_Y;
            list.Add(string.Format("Eccentricity along Y - direction                "));
            //list.Add(string.Format("ey = 6.005-6.0 = 0.225 m"));
            list.Add(string.Format("ey = {0:f3}-{1:f3} = {2:f3} m", CG_Y, GEO_Y, ey));
            list.Add(string.Format(""));




            list.Add(string.Format("Length from left face of left most column to right face of right most column "));


            double L = 0.0;

            #region L
            lst_cols.Clear();

            List<ColumnData> col_d = All_Collections.Get_Columns_Y(All_Collections.Y_Coordinates[0]);
            for (i = 0; i < col_d.Count; i++)
            {
                if (i == 0)
                {
                    L = 0.0;
                    L += col_d[i].Length;
                    kStr = col_d[i].Length.ToString("f2");
                }
                else
                {
                    L += Math.Abs(col_d[i].GridLength_X - col_d[i - 1].GridLength_X);
                    kStr += " + " + Math.Abs(col_d[i].GridLength_X - col_d[i - 1].GridLength_X).ToString("f2");
                }

                if (i == col_d.Count - 1)
                {
                    L += col_d[i].Length;
                    kStr += " + " + col_d[i].Length.ToString("f2");
                }
            }


            #endregion L

            //L = 21.6;

            //list.Add(string.Format("L = 0.3+7.0+7.0+7.0+0.3=21.6 m."));
            list.Add(string.Format("L = {0} = {1:f3} m.", kStr, L));


            double B = 12.6;
            col_d = All_Collections.Get_Columns_X(All_Collections.X_Coordinates[0]);

            kStr = "";
            for (i = 0; i < col_d.Count; i++)
            {
                if (i == 0)
                {
                    B = 0.0;
                    B += col_d[i].Width;
                    kStr = col_d[i].Width.ToString("f2");
                }
                else
                {
                    B += Math.Abs(col_d[i].GridLength_Y - col_d[i - 1].GridLength_Y);
                    kStr += " + " + Math.Abs(col_d[i].GridLength_Y - col_d[i - 1].GridLength_Y).ToString("f2");
                }

                if (i == col_d.Count - 1)
                {
                    B += col_d[i].Width;
                    kStr += " + " + col_d[i].Width.ToString("f2");
                }
            }

            list.Add(string.Format("Width from lower face of  bottom most column to upper face of top most column "));
            //list.Add(string.Format("B = 0.3+6.0+6.0+0.3=12.6 m."));
            list.Add(string.Format("B = {0} = {1:f3} m.", kStr, B));
            list.Add(string.Format(""));



            double IX = L * B * B * B / 12.0;
            list.Add(string.Format("IX = L x B^3 / 12  = {0:f3} x ({1:f3}^3 / 12) = {2:f3} m^4", L, B, IX));
            list.Add(string.Format(""));

            double IY = B * L * L * L / 12.0;
            list.Add(string.Format("Iy = B x L^3 / 12  = {0:f3} x ({1:f3}^3 / 12) = {2:f3} m^4", B, L, IY));
            list.Add(string.Format(""));

            double A = L * B;
            list.Add(string.Format("A = L x B = {0:f3} x {1:f3}  = {2:f3} m^2", L, B, A));
            list.Add(string.Format(""));

            double MX = P * ey;
            double MY = P * eX;
            list.Add(string.Format("MX = P x eY = {0:f3} x {1:f3} = {2:f3} kNm", P, ey, MX));
            list.Add(string.Format("My = P x eX = {0:f3} x {1:f3} = {2:f3} kNm", P, eX, MY));
            list.Add(string.Format(""));

            double p = P / A;
            list.Add(string.Format("p = P/A = {0:f3} / {1:f3} = {2:f3} kN/m^2", P, A, p));
            list.Add(string.Format(""));
            list.Add(string.Format("Soil pressure at different points is as follows :"));
            list.Add(string.Format("Σ = (P/A)   +/-    (My / Iy) x X   +/-   (Mx / Ix) x Y "));
            list.Add(string.Format(""));

            double X = L / 2;
            double Y = B / 2;
            list.Add(string.Format("X = {0:f3} / 2 = {1:f3} m.", L, X));
            list.Add(string.Format("Y = {0:f3} / 2 = {0:f3} m.", B, Y));
            list.Add(string.Format(""));

            list.Add(string.Format("p = P/A = {0:f3} kN/Sq.m", p));



            double pmy = MY * X / IY;
            list.Add(string.Format("pmy = {0:f3} x {1:f3} / {2:f3} = {3:f3} kN/Sq.m", MY, X, IY, pmy));

            double pmx = MX * Y / IX;

            list.Add(string.Format("pmx = {0:f3} x {1:f3} / {2:f3} = {3:f3} kN/Sq.m", MX, Y, IX, pmx));



            list.Add(string.Format(""));
            list.Add(string.Format(""));


            x_val = All_Collections.Get_Max_X();

            ColumnCollections cols = new ColumnCollections(All_Collections.Get_Columns_X(x_val));

            list.Add(string.Format("Corner A-4, Soil pressure"));


            double sigma_A4 = p + pmy + pmx;




            //list.Add(string.Format("σ(A-4) = 48.87 + 6.435 + 5.236 = 60.541 < 65 kNm^2"));
            if (sigma_A4 < brn_cap)
                list.Add(string.Format("σ(A-4) = {0:f3} + {1:f3} + {2:f3} = {3:f3} < {4:f3} kNm/m^2", p, pmy, pmx, sigma_A4, brn_cap));
            else
                list.Add(string.Format("σ(A-4) = {0:f3} + {1:f3} + {2:f3} = {3:f3} > {4:f3} kNm/m^2", p, pmy, pmx, sigma_A4, brn_cap));
            list.Add(string.Format(""));


            double sigma_C4 = p + pmy - pmx;

            list.Add(string.Format("Corner C-4, Soil pressure"));
            //list.Add(string.Format("σ(C-4) = 48.87 + 6.435 - 5.236 = 50.069 kNm/m2"));
            list.Add(string.Format("σ(C-4) = {0:f3} + {1:f3} - {2:f3} = {3:f3} kNm/m^2", p, pmy, pmx, sigma_C4));
            list.Add(string.Format(""));

            double sigma_A1 = p - pmy + pmx;

            list.Add(string.Format("Corner A-I, Soil pressure"));
            //list.Add(string.Format("σ(A-I)  = 48.87 - 6.435 + 5.236 = 47.671 kNm/m^2"));
            list.Add(string.Format("σ(A-I)  = {0:f3} - {1:f3} + {2:f3} = {3:f3} kNm/m^2", p, pmy, pmx, sigma_A1));
            list.Add(string.Format(""));

            double sigma_C1 = p - pmy - pmx;

            list.Add(string.Format("Corner C-I, Soil pressure"));
            list.Add(string.Format("σ(C-I)  = {0:f3} - {1:f3} - {2:f3} = {3:f3} kNm/m^2", p, pmy, pmx, sigma_C1));
            list.Add(string.Format(""));

            double sigma_B4 = p + pmy - 0;

            list.Add(string.Format("Grid B-4, Soil pressure"));
            list.Add(string.Format("σ(B-4) = {0:f3} + {1:f3} + 0 = {2:f3} kNm/m^2", p, pmy, sigma_B4));
            list.Add(string.Format(""));

            double sigma_B1 = p - pmy - 0;

            list.Add(string.Format("Grid B-I, Soil pressure"));
            //list.Add(string.Format("σ(B-I) = 48.87 - 6.435 + 0 = 42.435 kNm/m^2"));
            list.Add(string.Format("σ(B-I) = {0:f3} - {1:f3} + 0 = {2:f3} kNm/m^2", p, pmy, sigma_B1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("In the x-direction, the raft is divided in three strips, that is, three equivalent beams :"));
            list.Add(string.Format(""));









            double p1 = sigma_A4;
            list.Add(string.Format("(i) Beam A-A with 3.3 m width and p1 "));

            list.Add(string.Format("σ(A-4) = soil pressure of {0:f3} kNm/m^2", p1));
            list.Add(string.Format(""));

            double p2 = (sigma_A4 + sigma_B4) / 2;
            list.Add(string.Format("(ii) Beam B-B with 6.0 m width and p2 ="));
            list.Add(string.Format("(σ(A-4) + σ(B-4))/2 = soil pressure of ({0:f3} + {1:f3})/2 = {2:f3} kNm/m^2", sigma_A4, sigma_B4, p2));
            list.Add(string.Format(""));

            double p3 = sigma_C4;
            list.Add(string.Format("(iii) Beam C-C with 3.3 m width and p3 "));
            list.Add(string.Format("σc-4 = soil pressure of {0:f3} = {0:f3} kNm/m^2", p3));
            list.Add(string.Format(""));
            list.Add(string.Format("The bending moment is obtained by using a coefficient of 1/10 and L as centre of column distance,"));
            list.Add(string.Format(""));
            list.Add(string.Format("                + M  =  - M = wL2/10"));
            list.Add(string.Format(""));


            double large_span = 7.0;

            double BM1 = p1 * large_span * large_span / 10.0;
            list.Add(string.Format("For strip A-A, largest span = {0:f2} m,", large_span));
            //list.Add(string.Format("Maximum moment = BM1 = p1 x 7^2 / 10  = 60.54 x 7^2 / 10 = 296.60 kNm/m2"));
            list.Add(string.Format("Maximum moment = BM1 = p1 x {0:f2}^2 / 10  = {1:f2} x {0:f2}^2 / 10 = {2:f2} kNm/m^2", large_span, p1, BM1));
            list.Add(string.Format(""));

            double BM2 = p2 * large_span * large_span / 10;
            list.Add(string.Format("For strip B-B, largest span = {0:f3} m,", large_span));
            list.Add(string.Format("Maximum moment = BM2 = p2 x {0:f3}^2 / 10  = {1:f3} x {0:f3}^2 / 10 = {2:f3} kNm/m^2", p2, large_span, BM2));
            list.Add(string.Format(""));

            double BM3 = p3 * large_span * large_span / 10;

            list.Add(string.Format("For strip C-C, largest span = {0:f3} m,", large_span));
            list.Add(string.Format("Maximum moment = BM3 = p3 x {0:f3}^2 / 10  = {1:f3} x {0:f3}^2 / 10 = {2:f3} kNm/m^2", p3, large_span, BM3));
            list.Add(string.Format(""));
            list.Add(string.Format("For any strip in the y-direction, take M = (W x I^2)/8,   because there it is a two span equivalent beam."));
            list.Add(string.Format(""));


            //list.Add(string.Format("For strip 4-4, largest span = 6.0m,"));
            //list.Add(string.Format("Maximum moment = p1 x 6^2 / 8  = 60.54 x 62 / 8 = 272.4 kNm/m2"));

            large_span = 6.0;
            list.Add(string.Format("For strip 4-4, largest span = {0:f3} m,", large_span));
            double Max_moment = p1 * large_span * large_span / 8;
            list.Add(string.Format("Maximum moment = p1 x {0:f3}^2 / 8  = {1:f3} x {0:f3}^2 / 8 = {2:f3} kNm/m^2", large_span, p1, Max_moment));
            list.Add(string.Format(""));
            //list.Add(string.Format("and so on."));
            list.Add(string.Format(""));
            list.Add(string.Format("The depth of the raft will be governed by two way shear at one of the exterior columns."));
            list.Add(string.Format("In case location of critical shear is not obvious, it may be necessary to check all possible locations."));
            list.Add(string.Format(""));

            double tau_c = 0.25 * Math.Sqrt(fck);
            //list.Add(string.Format("Shear strength of concrete τc'  = τc = 0.25 √ fck = 0.25 √ 20 = 1.11 N/mm2"));
            list.Add(string.Format("Shear strength of concrete τc'  = τc = 0.25 √ fck = 0.25 √{0} = {1:f3} N/mm^2", fck, tau_c));
            list.Add(string.Format(""));
            list.Add(string.Format("For a corner column (say C- I)"));
            list.Add(string.Format("Perimeter bo =  2 (d/2 + 450) = d + 900 mm (Fig. a)"));
            list.Add(string.Format(""));

            double P_C1 = 550;

            list.Add(string.Format("Vu = 1.5 x Pc-1 x 1000,   bo = d"));
            list.Add(string.Format("τv  = Vu / [(bo + 900) x d] = τc'"));
            list.Add(string.Format("i.e,  1.5 x Pc-1 x 1000 / [(d + 900) x d] = τc'"));
            //list.Add(string.Format("or,  1.5 x 550 x 1000 / [d2 + 900xd] = 1.11"));
            list.Add(string.Format("or,  1.5 x {0:f2} x 1000 / [d^2 + 900xd] = {1:f3}", P_C1, tau_c));



            double _c = (1.5 * P_C1 * 1000 / tau_c);
            //double _c = (1.5 * P_C1 * 1000 / 1.11);

            //list.Add(string.Format("or,   d^2 + 900 d - 743243.243 = 0"));
            //list.Add(string.Format("or,   d  =  [- 900 + √(9002 + 4 x 1.5 x Pc-1 x 1000 x τc')]/[2]"));
            //list.Add(string.Format("         =  [- 900 + √(9002 + 4 x 1.5 x 550 x 1000 x 1.11)]/[2] = 522.493 mm "));


            list.Add(string.Format("or,   d^2 + 900 d - {0:f3} = 0", _c));

            double _val1 = ((-900) + Math.Sqrt((900 * 900) - (4 * 1 * (-_c)))) / 2.0;
            double _val2 = ((-900) - Math.Sqrt((900 * 900) - (4 * 1 * (-_c)))) / 2.0;

            double d = _val1;
            list.Add(string.Format("or,   d  =  [- 900 + √(900^2 + 4 x 1.5 x Pc-1 x 1000 x τc')]/[2]"));
            list.Add(string.Format("         =  [- 900 + √(900^2 + 4 x 1.5 x {0:f2} x 1000 x {1:f3})]/[2] = {2:f2} mm ", P_C1, tau_c, d));
            list.Add(string.Format(""));
            list.Add(string.Format("For a slide column (say A-2)"));


            list.Add(string.Format("Perimeter bo  =  2 (0.5 d + 450) + (d + 300) = 2 d + 1200 (Fig. b)"));
            list.Add(string.Format(""));

            double P_A2 = 1500;

            list.Add(string.Format("Vu = 1.5 x [P(A-2)] x 1000,   bo = d"));
            list.Add(string.Format("τv  = Vu / [(bo + 900) x d] = τc'"));
            list.Add(string.Format("i.e,  1.5 x [P(A-2)] x 1000 / [(2 x d + 1200) x d] = τc'"));
            list.Add(string.Format("or,   1.5 x {0:f2} x 1000 /  [2xd^2 +  1200xd] = {1:f2}", P_A2, tau_c));


            list.Add(string.Format("or,   d^2 + 600 x d - (1.5 x {0:f2} x 1000 / ({0:f2} x 2) = 0", P_A2, tau_c));

            tau_c = 1.11;
            _val1 = (-600 + Math.Sqrt(600 * 600 + 4 * 1.5 * P_A2 * 1000 * tau_c)) / 2.0;

            _c = (1.5 * P_A2 * 1000 / tau_c) / 2;
            _val1 = ((-600) + Math.Sqrt((600 * 600) - (4 * 1 * (-_c)))) / 2.0;

            double d2 = _val1;
            list.Add(string.Format("or,   d  =  [- 600 + √(600^2 + 4 x 1.5 x PA-2 x 1000 x τc')]/[2] "));
            list.Add(string.Format("         =  [- 600 + √(600^2 + 4 x 1.5 x {0:f3} x 1000 x {1:f3})]/[2] = {2:f3} mm", P_A2, tau_c, d2));
            list.Add(string.Format(""));


            d2 = (int)(d2 / 10) + 1;

            d2 = (int)(d2 * 10);

            list.Add(string.Format("Adopt effective depth = {0} mm, ", d2));

            double d1 = d;

            d = d2;

            double overall_dep = d + cover;

            list.Add(string.Format(""));
            list.Add(string.Format("Hence the overall depth = Adopt effective depth + Cover = {0} + {1} = {2} mm.", d, cover, overall_dep));
            list.Add(string.Format(""));
            list.Add(string.Format("Reinforcements in the long direction is derived from the equation,"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("BM = 0.87 x fy x At x [d - fy x At / (fck x b)], where  BM = BM1 x 10^6 = 296.60 x 10^6 Nmm."));
            //list.Add(string.Format("At = ((d x fck x b)  +/-  √ [(d x fck x b)2 - (4 x fy x BM1 x fck x b) / (0.87 x fy)]) / (2 x fy)"));
            //list.Add(string.Format("= {(760 x 20 x 1000)  +/-  √ [(760 x 20 x 1000)2  -  (4 x 415 x 296.60 x 1000000 x 20 x 1000) / (0.87 x 415)]}/(2 x 415)"));
            //list.Add(string.Format("= {15200000 +/-  √ [231.04 x 1012 - 9847.12 x 1012/361.05]}/830"));
            //list.Add(string.Format("= {15200000 +/-  √ [231.04 x 1012 - 27.274x 1012]}/830"));
            //list.Add(string.Format("= {15200000 +/-  √ 203.766 x 1012}/830"));
            //list.Add(string.Format("= {15200000 +/-  14274662.868}/830"));
            //list.Add(string.Format("= 925337.132 / 830"));
            //list.Add(string.Format("= 1114.864 Sq.mm"));
            //list.Add(string.Format("Minimum steel = 0.12% of 820 x 1000 = 984 Sq.mm"));
            //list.Add(string.Format("Provide 20mm dia Rebars @ spacing 200 mm c/c.  (1884.96 Sq.mm)"));



            list.Add(string.Format("BM = 0.87 x fy x At x [d - fy x At / (fck x b)], where  BM = BM1 x 10^6 = {0:f3} x 10^6 Nmm.", BM1));
            list.Add(string.Format("At = ((d x fck x b)  +/-  √ [(d x fck x b)^2 - (4 x fy x BM1 x fck x b) / (0.87 x fy)]) / (2 x fy)"));



            //list.Add(string.Format("   = {(760 x 20 x 1000)  +/-  √ [(760 x 20 x 1000)^2  -  (4 x 415 x 296.60 x 1000000 x 20 x 1000) / (0.87 x 415)]}/(2 x 415)"));
            list.Add(string.Format("   = [({0} x {1} x 1000)  +/-  √ [({0} x {1} x 1000)^2  -  (4 x {2} x {3:f3} x 1000000 x {1} x 1000) / (0.87 x {2})]]/(2 x {2})", d, fck, fy, BM1));

            //fck = 20;
            //fy = 415;
            //_val1 = (d * 20 * 1000);

            //double _At =  ((d * fck * b)  +/-  √ [(d x fck x b)^2 - (4 x fy x BM1 x fck x b) / (0.87 x fy)]) / (2 x fy)
            double _At = ((d * fck * 1000) - Math.Sqrt((d * fck * 1000) * (d * fck * 1000) - (4 * fy * BM1 * 1000000 * fck * 1000) / (0.87 * fy))) / (2 * fy);

            //list.Add(string.Format("   = {15200000 +/-  √ [231.04 x 10^12 - 9847.12 x 10^12/361.05]}/830"));
            //list.Add(string.Format("   = {15200000 +/-  √ [231.04 x 10^12 - 27.274x 10^12]}/830"));
            //list.Add(string.Format("   = {15200000 +/-  √ 203.766 x 10^12}/830"));
            //list.Add(string.Format("   = {15200000 +/-  14274662.868}/830"));
            //list.Add(string.Format("   = 925337.132 / 830"));
            list.Add(string.Format("   = {0:f3} Sq.mm, ", _At));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double min_steel = overall_dep * 1000 * 0.12 / 100;

            //list.Add(string.Format("Minimum steel = 0.12% of 820 x 1000 = 984 Sq.mm"));
            //list.Add(string.Format("Provide 20mm dia Rebars @ spacing 200 mm c/c.  (1884.96 Sq.mm)"));



            list.Add(string.Format("Minimum steel = 0.12% of {0} x 1000 = {1:f2} Sq.mm", overall_dep, min_steel));


            double bar_A = Math.PI * bar_dia * bar_dia / 4;



            double bar_nos = min_steel / bar_A;

            bar_nos = (int)(bar_nos + 2);


            double spac = (int)(1000 / bar_nos);


            list.Add(string.Format("Provide {0}mm dia Rebars @ spacing {1} mm c/c.  ({2:f3} Sq.mm)", bar_dia, spac, bar_nos * bar_A));

            list.Add(string.Format(""));
            #endregion


            list.Add(string.Format("-------------------------------------------------------------"));
            list.Add(string.Format("                         END OF DESIGN "));
            list.Add(string.Format("-------------------------------------------------------------"));


            File.WriteAllLines(fileName, list.ToArray());
        }

        #region Chiranjit [2016 09 07
        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Combined_Footing;
            }
        }
        public string user_path
        {
            get
            {
                return iApp.user_path;
            }

            set
            {
                iApp.user_path = value;
            }
        }
        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                   "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);

            btn_Process.Enabled = true;
        }
        string Report_File
        {
            get
            {
                return Path.Combine(user_path, "Raft_Foundation_Report.txt");
            }
        }

        public string Drawing_Folder
        {
            get
            { 
                return Path.Combine(user_path, "DRAWINGS");
            }
        }
        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

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

            txt_project_name.Text = prj_name;

        }

        public void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

        #endregion Chiranjit [2016 09 07]


        private void btn_psc_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_open_design.Name)
            {
                //frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    user_path = frm.Example_Path;
                //    iApp.Read_Form_Record(this, user_path);

                //    txt_project_name.Text = Path.GetFileName(user_path);

                //    #region Save As
                //    if (frm.SaveAs_Path != "")
                //    {

                //        string src_path = user_path;
                //        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                //        Create_Project();
                //        string dest_path = user_path;

                //        MyList.Folder_Copy(src_path, dest_path);
                //    }

                //    txt_project_name.Text = Path.GetFileName(user_path);

                //    #endregion Save As

                //}
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_project_name.Text != "")
                //    frm.Project_Name = txt_project_name.Text;
                //else
                //    frm.Project_Name = "Design of Composite Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_project_name.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    IsCreate_Data = true;
                //}
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            //throw new NotImplementedException();
        }

        private void Button_Enable_Disable()
        {
            //throw new NotImplementedException();
        }
        public bool Select_Column()
        {
            MessageBox.Show("Select a Column :", "ASTRA", MessageBoxButtons.OK);
            vdFigure vfig;
            gPoint gp;

            StatusCode scc = uC_CAD1.VDoc.ActionUtility.getUserEntity(out vfig, out gp);

            if (scc == StatusCode.Cancel) return false;

            vdPolyline pl = vfig as vdPolyline;


            frm_ColumnSelection fr = new frm_ColumnSelection();


            List<double> xx = new List<double>();
            List<double> yy = new List<double>();

            for(int i = 0; i < pl.VertexList.Count; i++)
            {
                var item =  pl.VertexList[i];

                if (!xx.Contains(item.x)) xx.Add(item.x);
                if (!yy.Contains(item.y)) yy.Add(item.y);
            }

            xx.Sort();
            yy.Sort();

            string kStr = xx[0].ToString("f2") + "," + yy[0].ToString("f2");

            fr.Coordinates = "(" + kStr + ")";

            kStr = "lx=" + xx[0].ToString("f2") + ", ly=" + yy[0].ToString("f2");

            fr.GridLength =  kStr ;

            fr.L = Math.Abs(xx[0] - xx[1]).ToString("f2");
            fr.B = Math.Abs(yy[0] - yy[1]).ToString("f2");

            fr.Owner = this;
          if(  fr.ShowDialog() == System.Windows.Forms.DialogResult.Yes);
          {
              dgv_ColumnData.Rows.Add("", fr.Coordinates, fr.GridLength, fr.L, fr.B, fr.Load);
              Select_Column();
          }


            //dgv_ColumnData.Rows.Add("", fr.Coordinates, fr.GridLength, fr.L, fr.B, fr.Load);
            return true;
        }
        private void frm_RaftFoundations_Load(object sender, EventArgs e)
        {
            Set_Project_Name();
            Load_Default_Data();

            uC_CAD1.iApp = iApp;



        }

        public void ss()
        {
            vdPolyline vpl = null;

            List<double> lx_x = new List<double>();
            List<double> lx_y = new List<double>();

            lx_x.Add(0.0);
            lx_y.Add(0.0);






            lx_x.Add(0.506);
            lx_y.Add(1.807);


            lx_x.Add(0.934);
            lx_y.Add(3.3275);


            lx_x.Add(1.5410);
            lx_y.Add(5.4875);


            lx_x.Add(1.868);
            lx_y.Add(6.655);


            lx_x.Add(2.215);
            lx_y.Add(7.8904);


            lx_x.Add(2.685);
            lx_y.Add(9.5624);


            lx_x.Add(3.0820);
            lx_y.Add(10.9791);


            lx_x.Add(3.3210);
            lx_y.Add(11.8286);


            lx_x.Add(3.7370);
            lx_y.Add(13.31);


            lx_x.Add(3.7720);
            lx_y.Add(14.0850);


            lx_x.Add(3.807);
            lx_y.Add(14.86);


            lx_x.Add(3.907);
            lx_y.Add(15.5621);


            lx_x.Add(3.998);
            lx_y.Add(16.1955);


            lx_x.Add(4.088);
            lx_y.Add(16.8289);


            lx_x.Add(4.157);
            lx_y.Add(17.31);


            lx_x.Add(4.288);
            lx_y.Add(18.1104);


            lx_x.Add(4.410);
            lx_y.Add(18.86);


            lx_x.Add(4.6470);
            lx_y.Add(20.31);


            lx_x.Add(4.722);
            lx_y.Add(21.86);


            lx_x.Add(5.186);
            lx_y.Add(23.6851);


            //lx_x.Add(6.53);
            //lx_y.Add(24.2563);


            lx_x.Add(5.47);
            lx_y.Add(24.9002);


            lx_x.Add(5.69);
            lx_y.Add(25.8123);


            lx_x.Add(5.922);
            lx_y.Add(26.81);


            double L = 11.844;
            for (int i = 0; i < lx_x.Count; i++)
            {
                vpl = new vdPolyline(uC_CAD1.VDoc);
                vpl.setDocumentDefaults();
                var x = lx_x[i];
                var y = lx_y[i];
                vpl.VertexList.Add(new gPoint(x, y, x));
                vpl.VertexList.Add(new gPoint(L - x, y, x));
                vpl.VertexList.Add(new gPoint(L - x, y, L - x));
                vpl.VertexList.Add(new gPoint(x, y, L - x));
                vpl.VertexList.Add(new gPoint(x, y, x));

                uC_CAD1.VDoc.ActiveLayOut.Entities.Add(vpl);
            }


        }

        private void btn_SelectColumn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == button1.Name)
            {
                Select_Column();
            }
            else if (btn.Name == btn_update.Name)
            {
                All_Collections.Read_From_Grid(dgv_ColumnData);

                List<ColumnData> col_data = All_Collections.Get_Sorted_Order();
                //col_data = All_Collections.Get_Sorted_Order();

                ColumnCollections col = new ColumnCollections(col_data);


                col.Load_Grid(dgv_ColumnData);




                //col_data.Sort();
            }
        }

        private void btn_Process_Click(object sender, EventArgs e)
        {
  
            All_Collections.Read_From_Grid(dgv_ColumnData);

            CalculateProgram(Report_File);
            iApp.View_Result(Report_File);

            btn_Report.Enabled = true;
        }

        private void btn_drawings_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Drawing_Folder, "RAFT_FOUNDATION");
        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            if (File.Exists(Report_File))
            {
                System.Diagnostics.Process.Start(Report_File);
            }
        }

    }

    public class ColumnCollections : List<ColumnData>
    {
        public List<double> X_Coordinates { get; set; }
        public List<double> Y_Coordinates { get; set; }
        public ColumnCollections()
            : base()
        {
            X_Coordinates = new List<double>();
            Y_Coordinates = new List<double>();
        }
        public ColumnCollections(List<ColumnData> lst_data)
            : base(lst_data)
        {
            X_Coordinates = new List<double>();
            Y_Coordinates = new List<double>();
        }
        public void Read_From_Grid(DataGridView dgv)
        {
            X_Coordinates.Clear();
            Y_Coordinates.Clear();
            Clear();

            string kStr = "";

            MyList ml = null;
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                ColumnData cd = new ColumnData();

                if (dgv[0, i].Value == null)  dgv[0, i].Value = "";
                cd.ColumnName = dgv[0, i].Value.ToString();

                kStr = dgv[1, i].Value.ToString().Replace("(", "").Replace(")", "");
                ml = new MyList(kStr, ',');

                cd.Coordinate_X = ml.GetDouble(0);
                cd.Coordinate_Y = ml.GetDouble(1);


                kStr = dgv[2, i].Value.ToString().ToLower().Replace("(", "").Replace(")", "").Replace("lx", "").Replace("ly", "").Replace("=", "");

                ml = new MyList(kStr, ',');

                cd.GridLength_X = ml.GetDouble(0);
                cd.GridLength_Y = ml.GetDouble(1);

                cd.Length = MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0);
                cd.Width = MyList.StringToDouble(dgv[4, i].Value.ToString(), 0.0);
                cd.Load = MyList.StringToDouble(dgv[5, i].Value.ToString(), 0.0);


                if (!X_Coordinates.Contains(cd.Coordinate_X)) X_Coordinates.Add(cd.Coordinate_X);
                if (!Y_Coordinates.Contains(cd.Coordinate_Y)) Y_Coordinates.Add(cd.Coordinate_Y);

                Add(cd);

            }


        }

        public void Load_Grid(DataGridView dgv)
        {

            dgv.Rows.Clear();

            for (int i = 0; i < this.Count; i++)
            {
                ColumnData cd = this[i];

                dgv.Rows.Add(cd.ColumnName,
                   "(" +  cd.Coordinate_X.ToString() + "," +  cd.Coordinate_Y.ToString() + ")",
                   "lx=" +  cd.GridLength_X.ToString() + ",ly=" +  cd.GridLength_Y.ToString() + "",
                    cd.Length.ToString("f3"),
                    cd.Width.ToString("f3"),
                    cd.Load.ToString()
                    );
            }


        }

        public List<ColumnData> Get_Columns_X(double val)
        {
            List<ColumnData> list = new List<ColumnData>();

            foreach (var item in this)
            {
                if(item.Coordinate_X == val)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public List<ColumnData> Get_Columns_Y(double val)
        {
            List<ColumnData> list = new List<ColumnData>();

            foreach (var item in this)
            {
                if (item.Coordinate_Y == val)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public double Get_Max_X()
        {
            double x = 0.0;

            foreach (var item in this)
            {
                if (item.Coordinate_X > x) x = item.Coordinate_X;
            }
            return x;
        }
        public double Get_Max_Y()
        {
            double y = 0.0;

            foreach (var item in this)
            {
                if (item.Coordinate_Y > y) y = item.Coordinate_Y;
            }
            return y;
        }
        public double Get_Min_X()
        {
            double x = double.MaxValue;

            foreach (var item in this)
            {
                if (x > item.Coordinate_X) x = item.Coordinate_X;
            }
            return x;
        }
        public double Get_Min_Y()
        {
            double y = double.MaxValue;

            foreach (var item in this)
            {
                if ( y > item.Coordinate_Y) y = item.Coordinate_Y;
            }
            return y;
        }

        public List<double> Get_All_Loads()
        {
            List<double> list = new List<double>();

            foreach (var item in this)
            {
                list.Add(item.Load);
            }
            return list;
        }

        public List<ColumnData> Get_Sorted_Order()
        {
            List<ColumnData> lst = new List<ColumnData>();


            char Y_Axix = 'A';
            int cs = Y_Axix;



            List<ColumnData> lst_X = new List<ColumnData>();
            List<ColumnData> lst_Y = new List<ColumnData>();


            List<string> Column_Names = new List<string>();

            ColumnCollections cols = new ColumnCollections();

            string kStr = "";

            X_Coordinates.Sort();
            Y_Coordinates.Sort();
            Y_Coordinates.Reverse();
            for (int i = 0; i < X_Coordinates.Count; i++)
            {
                Y_Axix = 'A';
                cs = Y_Axix;


                 lst_X = Get_Columns_X(X_Coordinates[i]);


                 cols = new ColumnCollections(lst_X);
                for (int j = 0; j < Y_Coordinates.Count; j++)
                {
                    Y_Axix = (char)cs;
                    kStr = Y_Axix + "-" + (i + 1).ToString();
                    Column_Names.Add(kStr);
                    cs++;

                    lst_Y = cols.Get_Columns_Y(Y_Coordinates[j]);

                    lst_Y[0].ColumnName = kStr;
                    if (lst_Y.Count > 0) lst.Add(lst_Y[0]);
                }
            }



            //Clear();
            //AddRange(lst.ToArray());
            return lst;

        }

    }
    public class ColumnData
    {

        //Column                          Coordinates (X,Y)         Grid lengths  (X,Y)          L (M)          B (M)           Load (kN)
        //A-1                                  (0.0, 12.0)       	lx=0.0, ly=12.0                 0.3              0.3               500
        //B-1                                  (0.0, 6.0)       		lx=0.0, ly=6.0                   0.3              0.0               600
        //C-1                                  (0.0, 0.0)        	lx=0.0, ly=0.0                   0.3              0.0               550

        //A-2                                  (7.0, 12.0)        	lx=7.0, ly=12.0                 0.3             0.3                1500
        //B-2                                  (7.0, 6.0)       		lx=7.0, ly=6.0                   0.3              0.0               2000
        //C-2                                  (7.0, 0.0)       		lx=7.0, ly=0.0                   0.3              0.0               1200

        //A-3                                  (14.0, 12.0)       	lx=14.0, ly=12.0               0.3             0.3               1500
        //B-3                                  (14.0, 6.0)       	lx=14.0, ly=6.0                 0.3              0.0               2000
        //C-3                                  (14.0, 0.0)       	lx=14.0, ly=0.0                 0.3              0.0               1200

        //A-4                                  (21.0, 12.0)       	lx=21.0, ly=12.0               0.3             0.3               500
        //B-4                                  (21.0, 6.0)       	lx=21.0, ly=6.0                 0.3              0.0               1200
        //C-4                                  (21.0, 0.0)       	lx=21.0, ly=0.0                 0.3              0.0               550

        public string ColumnName { get; set; }
        public double Coordinate_X { get; set; }
        public double Coordinate_Y { get; set; }
        public double GridLength_X { get; set; }
        public double GridLength_Y { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Load { get; set; }


        public double Sigma { get; set; }
        public ColumnData()
        {
            ColumnName = "";
            Coordinate_X = 0.0;
            Coordinate_Y = 0.0;
            GridLength_X = 0.0;
            GridLength_Y = 0.0;
            Length = 0.0;
            Width = 0.0;
            Load = 0.0;

            Sigma = 0.0;
        }

        public override string ToString()
        {
            return string.Format("{0}      ({1:f2},{2:f2})      lx={3:f2}, ly={4:f2}      {5:f2}      {6:f2}     {7:f2}",
                ColumnName,
                Coordinate_X,
                Coordinate_Y,
                GridLength_X,
                GridLength_Y,
                Length,
                Width,
                Load                
                );
        }
    }
}
