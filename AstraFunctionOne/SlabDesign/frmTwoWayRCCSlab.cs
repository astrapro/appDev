using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
namespace AstraFunctionOne.SlabDesign
{
    public partial class frmTwoWayRCCSlab : Form
    {
        ModificationFactorCollection modification_fact = null;
        BendingCollection bend_fact = null;
        string rep_file_name = "";
        string filePathSet = "";

        string file_path = "";
        string user_path = "";
        string user_input_file = "";
        string system_path = "";
        
        List<string> list_drawing = null;
        IApplication iApp = null;


        bool is_process = false;
        double D, W, Pt, Pw, Ft, Fw, Ww, Df, Lf, Ll, lx, ly, sigma_ck, sigma_y, bw, h;


        public frmTwoWayRCCSlab(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
            D = 0; 
            W = 0; 
            Pt = 0; 
            Pw = 0; 
            Ft = 0; 
            Fw = 0; 
            Ww = 0; 
            Df = 0; 
            Lf = 0;
            Ll = 0;
            lx = 0;
            ly = 0;
            sigma_ck = 0;
            sigma_y = 0; 
            bw = 0;
            h = 0;
            list_drawing = new List<string>();
        }
        private void frmSLAB04_Load(object sender, EventArgs e)
        {
            cmb_BS_CODE.SelectedIndex = 3;
            modification_fact = new ModificationFactorCollection();
            bend_fact = new BendingCollection();
            Enable_Button = false;

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
            //MessageBox.Show(m_fac.Get_MF(223, 0.357).ToString());
            //MessageBox.Show(m_fac.Get_MF(263, 0.357).ToString());
            //MessageBox.Show(m_fac.Get_MF(103, 3.7).ToString());
            //m_fac.
        }
        public void CalculateProgram(string fName)
        {
            // User Input Data


            // Slab Thickness
            // Default Data
            //D = 130;
            //W = 25;
            //Pt = 6;
            //Pw = 24;
            //Ft = 30;
            //Fw = 24;
            //Ww = 1;
            //Ll = 4;
            //Df = 1.5;
            //Lf = 1.5;
            //lx = 4.2;
            //ly = 5.9;
            //sigma_ck = 20;
            //sigma_y = 415;
            //bw = 250;
            //h = 15;



            // Design Calculation

            //Step 1.   Load Combination
            // Permanent Load
            StreamWriter sw = new StreamWriter(new FileStream(fName, FileMode.Create));
            double val1, val2, val3, total_fixed_load, factored_load;

            double beta_x1, beta_x2, beta_y1, beta_y2, ly_div_lx;
            double Mx1, Mx2, My1, My2, d, j, Ast;
            double main_dia = 10;
            double dist_dia = 8;
            double main_ast, required_bar;
            val1 = val2 = val3 = 0.0d;
            double max_span_reinforcement;

            try
            {
                sw.WriteLine("\t\t*******************************************************");
                sw.WriteLine("\t\t*                ASTRA Pro Release 22                 *");
                sw.WriteLine("\t\t*       TechSOFT Engineering Services (I) Pvt. Ltd.   *");
                sw.WriteLine("\t\t*                                                     *");
                sw.WriteLine("\t\t*                  DESIGN OF SINGLE SPAN              *");
                sw.WriteLine("\t\t*          TWO WAY RCC SLAB BY LIMIT STATE METHOD     *");
                sw.WriteLine("\t\t*******************************************************");
                sw.WriteLine("\t\t-------------------------------------------------------");
                sw.WriteLine("\t\t    THIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t-------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                #region User's Data
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Slab Thickness = {0} mm", txt_D.Text);
                sw.WriteLine("Concrete Unit Weight = {0} kN/cu.m", txt_W.Text);
                sw.WriteLine("Ceiling Plaster Thickness = {0:f2} mm", Pt);
                sw.WriteLine("Plaster Unit Weight = {0:f2} kN/cu.m", Pw);
                sw.WriteLine("Floor Finish Thickness = {0:f2} mm", Ft);
                sw.WriteLine("Finish Unit Weight = {0:f2} mm", Fw);
                sw.WriteLine("Partition Wall = {0:f2} kN/sq.m", Ww);
                sw.WriteLine("Imposed / Live Load = {0:f2} kN/sq.m", Ll);
                sw.WriteLine("Dead Load Factor = {0:f2}", Df);
                sw.WriteLine("Live Load Factor = {0:f2}", Lf);
                sw.WriteLine("Length in Shorter Direction = {0:f2} m", lx);
                sw.WriteLine("Length in Longer Direction = {0:f2} m", ly);
                sw.WriteLine("Concrete Grade = M {0:f0} N/sq.mm.", sigma_ck);
                sw.WriteLine("Reinforcement Steel Grade = Fe {0:f0} N/sq.mm.", sigma_y);
                sw.WriteLine("Support Width = {0:f2} mm", bw);
                sw.WriteLine("Clear Cover = {0:f2} mm", h);

                string ss = cmb_BS_CODE.Text.Substring(2, cmb_BS_CODE.Text.Length - 2);

                sw.WriteLine("Slab Panel = {0}", MyList.RemoveAllSpaces(ss));
                //sw.WriteLine("Slab Panel BS-8110-Part 1-1985 = {0}", MyList.RemoveAllSpaces(ss));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                #endregion


                #region Step 1
            step_1:

                val1 = ((D / 1000.0) * W);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Load Calculations");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent / Dead Load :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("{0} mm Thick slab = {1} * {2} = {3} kN/sq.m",
                    D,
                    (D / 1000),
                    W,
                    val1);

                val2 = ((Pt / 1000.0) * Pw);
                sw.WriteLine("{0} mm Thick ceiling plaster = {1} * {2} = {3} kN/sq.m",
                    Pt,
                    (Pt / 1000),
                    Pw,
                    val2);


                val3 = ((Ft / 1000.0) * Fw);
                sw.WriteLine("{0} mm Thick Floor finish = {1} * {2} = {3} kN/sq.m",
                    Ft,
                    (Ft / 1000),
                    Fw,
                    val3);
                sw.WriteLine("Partition Wall = {0} kN/sq.m.", Ww);

                total_fixed_load = (val1 + val2 + val3 + Ww);
                sw.WriteLine("Total Permanent / Dead Load {0}",
                    total_fixed_load);

                sw.WriteLine("Factored Load = {0} * Df + Ll * Lf",
                    total_fixed_load);
                sw.WriteLine("              = {0} * {1} + {2} * {3}",
                    total_fixed_load,
                    Df,
                    Ll,
                    Lf);
                factored_load = total_fixed_load * Df + Ll * Lf;
                sw.WriteLine("              = w = {0} kN/sq.m.",
                    factored_load);
                #endregion

                #region Step 2
            step_2:
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2: Moment Calculation");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                ly_div_lx = ly / lx;

                sw.WriteLine("lx / ly = {0} / {1} = {2} <= 2 So can be designed as two way slab.",
                    lx,
                    ly,
                    ly_div_lx.ToString("0.000"));

                // Refer to BS 8110

                //Bending bnd = bend_fact.Get_Bending(cmb_.SelectedIndex + 1, 1.85);
                Bending bnd = bend_fact.Get_Bending(cmb_BS_CODE.SelectedIndex + 1, ly_div_lx);
                //beta_x1 = -0.071;
                //beta_x2 = 0.053;

                //beta_y1 = -0.047;
                //beta_y2 = 0.035;

                beta_x1 = -bnd.Beta_x1;
                beta_x2 = bnd.Beta_x2;

                beta_y1 = -bnd.Beta_y1;
                beta_y2 = bnd.Beta_y2;

                sw.WriteLine();
                sw.WriteLine("1. Positive Bending Moment in shorter Direction = β_x * w * l_x * l_x");

                Mx1 = beta_x2 * factored_load * lx * lx;
                sw.WriteLine("                                            Mx1 = {0} * {1} * {2} * {2}",
                    beta_x2,
                    factored_load,
                    lx);

                sw.WriteLine("                                                = {0} kN-m", Mx1.ToString("0.00"));


                sw.WriteLine();
                sw.WriteLine("2. Negative Bending Moment in shorter Direction ");
                Mx2 = beta_x1 * factored_load * lx * lx;
                sw.WriteLine("                                            Mx2 = {0} * {1} * {2} * {2}",
                    beta_x1,
                    factored_load,
                    lx);

                sw.WriteLine("                                                = {0} kN-m", Mx2.ToString("0.00"));

                sw.WriteLine();
                sw.WriteLine("3. Positive Bending Moment in Longer Direction  = β_y * w * l_x * l_x");
                My1 = beta_y2 * factored_load * lx * lx;
                sw.WriteLine("                                            My1 = {0} * {1} * {2} * {2}",
                    beta_y2,
                    factored_load,
                    lx);
                sw.WriteLine("                                                = {0} kN-m", My1.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("4. Negative Bending Moment in Longer Direction ");
                My2 = beta_y1 * factored_load * lx * lx;
                sw.WriteLine("                                            My2 = {0} * {1} * {2} * {2}",
                    beta_y2,
                    factored_load,
                    lx);
                sw.WriteLine("                                                = {0} kN-m", My2.ToString("0.00"));

                #endregion

                #region Step 3
            step_3:
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Reinforcement in Shorter Direction :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Bottom Bars");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Positive Bending Moment = ");
                sw.WriteLine("                   Mx1 = {0} kN-m.", Mx1.ToString("0.00"));

                d = D - 20; //20 ?
                sw.WriteLine("                     d = D - 20 = {0} - 20 = {1}",
                    D,
                    d);
                sw.WriteLine("Lever Arm Coefficient = j = 0.5 + √((0.25) - ((Mx1 * 10E+5)/(0.87*σ_ck*1000*d*d)))");
                sw.WriteLine("                          = 0.5 + √((0.25) - (({0} * 10E+5)/(0.87 * {1} * 1000 * {2} * {2})))",
                    Mx1.ToString("0.00"),
                    sigma_ck,
                    d);

                j = 0.5 + Math.Sqrt(0.25 - ((Mx1 * 10E+5) / (0.87 * sigma_ck * 1000 * d * d)));
                sw.WriteLine("                          = {0} ", j.ToString("0.000"));
                Ast = (Mx1 * 10E+5) / (0.87 * sigma_y * j * d); // max_ast 1
                max_span_reinforcement = Ast;

                sw.WriteLine("Area of steel required at Bottom = Ast = (Mx1 * 10E+5) / (0.87 * σ_y * j * d)");
                sw.WriteLine("                                       = ({0} * 10E+5) / (0.87 * {1} * {2} * {3})",
                    Mx1.ToString("0.00"),
                    sigma_y,
                    j.ToString("0.000"),
                    d);
                sw.WriteLine("                                       = {0} sq.mm", Ast.ToString("0.00"));


                main_ast = (Math.PI * main_dia * main_dia) / 4;
                required_bar = main_ast * 1000.00 / Ast;

                sw.WriteLine("   Required spacing of T{0} bars = ({1} * 1000) / {2} = {3} mm", main_dia, main_ast.ToString("0.00"), Ast.ToString("0.00"), required_bar.ToString("0.00"));

                if (required_bar > 200) required_bar = 200;

                sw.WriteLine("Provide T{0} @ {1} c/c", main_dia, required_bar);


                double bottom_ast = main_ast * (1000.0 / required_bar);
                sw.WriteLine("Ast (Bottom) Provided {0} sq.mm", bottom_ast.ToString("0.00"));

                double percent_steel = (bottom_ast * 100) / (1000 * d);
                sw.WriteLine("Percentage of steel provided = (Ast * 100)/(b * d) = ({0} * 100) / (1000 * {1}) = {2}%",
                    bottom_ast.ToString("0.00"),
                    d,
                    percent_steel.ToString("0.000"));

                double fs = 0.58 * sigma_y * (Ast / bottom_ast);
                sw.WriteLine("     fs = 0.58 * {0} * ({1} / {2})  = {3}",
                    sigma_y, main_ast.ToString("0.00"), bottom_ast.ToString("0.00"),
                    fs.ToString("0.00"));

                double d_required, d_provided, l, MF;

                // Refer to Fig 4, Page 38
                MF = modification_fact.Get_MF(fs, percent_steel);

                l = lx * 1000;
                d_required = l / (26 * MF);
                sw.WriteLine("d required = l / (26 * MF) =  {0}/(26 * 1.5) = {1} mm", l, d_required.ToString("0.00"));


                if (d > d_required)
                {
                    sw.WriteLine("d provided = {0} mm > {1}   OK", d, d_required.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("d provided = {0} mm < {1}   NOT OK", d, d_required.ToString("0.00"));
                }
                #endregion

                #region Step 4
            step_4:
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Reinforcement in shorter direction");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Top Bars");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Negative Bending Moment = Mx2 = {0} kN-m", Mx2.ToString("0.00"));

                sw.WriteLine("Lever Arm Coefficient = j = 0.5 + √((0.25) - ((Mx2 * 10E+5)/(0.87*σ_ck*1000*d*d)))");
                sw.WriteLine("                         = 0.5 + √((0.25) - (({0} * 10E+5)/(0.87 * {1} * 1000 * {2} * {2})))",
                    Mx2.ToString("0.00"),
                    sigma_ck,
                    d);



                double j2;
                j2 = 0.5 + Math.Sqrt((0.25) - ((Math.Abs(Mx2) * 10E+5) / (0.87 * sigma_ck * 1000 * d * d)));

                sw.WriteLine("                         = {0} ", j2.ToString("0.000"));

                double top_area_req = (Math.Abs(Mx2) * 10E+5) / (0.87 * j2 * sigma_y * d);

                sw.WriteLine("Area of steel requirement (at Top) = Mx2 * 10E+5 / 0.87 * j * σ_y * d");
                sw.WriteLine("                                   = {0} * 10E+5 / 0.87 * {1} * {2} * {3}",
                    Math.Abs(Mx2).ToString("0.00"),
                    j2.ToString("0.00"),
                    sigma_y,
                    d);
                sw.WriteLine("                                   = {0} sq.mm", top_area_req.ToString("0.00"));


                double req_spacing_bars = main_ast * 1000 / top_area_req;
                sw.WriteLine("Required spacing of T{0} bars = {1} * 1000 / {2} = {3} mm",
                    main_dia,
                    main_ast.ToString("0.00"),
                    top_area_req.ToString("0.00"),
                    req_spacing_bars.ToString("0.00"));
                req_spacing_bars = (int)req_spacing_bars / 10;
                req_spacing_bars = req_spacing_bars * 10;

                sw.WriteLine("Provide T{0} @ {1} c/c",
                    main_dia,
                    req_spacing_bars);

                double top_ast = main_ast * 1000.0 / req_spacing_bars;
                double top_ast_provided = main_ast * 1000 / req_spacing_bars;

                sw.WriteLine("Ast (at Top) provided = {0} * 1000 / {1} = {2} sq.mm.",
                    main_ast.ToString("0.00"),
                    req_spacing_bars,
                    top_ast_provided.ToString("0.00"));


                #endregion

                #region Step 5
            step_5:
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Reinforcement in Longer direction");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Bottom Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Positive Bending Moment   = My1 = {0} kN-m.", My1.ToString("0.00"));
                sw.WriteLine("Lever Arm Coefficient = j = 0.5 + √((0.25) - ((My1 * 10E+5)/(0.87*σ_ck*1000*d*d)))");
                sw.WriteLine("                          = 0.5 + √((0.25) - (({0} * 10E+5)/(0.87 * {1} * 1000 * {2} * {2})))",
                    My1.ToString("0.00"),
                    sigma_ck,
                    d);

                double j3;
                j3 = 0.5 + Math.Sqrt((0.25) - ((Math.Abs(My1) * 10E+5) / (0.87 * sigma_ck * 1000 * d * d)));

                sw.WriteLine("                         = {0} ", j3.ToString("0.00"));


                double bottom_area_req = (Math.Abs(My1) * 10E+5) / (0.87 * j3 * sigma_y * d);

                if (max_span_reinforcement < bottom_area_req)
                    max_span_reinforcement = bottom_area_req;

                sw.WriteLine("Area of steel requirement (at Top) = My1 * 10E+5 / 0.87 * σ_y * j * d");
                sw.WriteLine("                                   = {0} * 10E+5 / 0.87 * {1} * {2} * {3}",
                    My1.ToString("0.00"),
                    sigma_y,
                    j3.ToString("0.00"),
                    d);
                sw.WriteLine("                                   = {0} sq.mm", bottom_area_req.ToString("0.00"));

                double dist_ast = Math.PI * dist_dia * dist_dia / 4;

                double req_dist_spacing_bars = dist_ast * 1000 / bottom_area_req;

                sw.WriteLine("Required spacing of T{0} bars = {1} * 1000 / {2} = {3} mm",
                    dist_dia,
                    dist_ast.ToString("0.00"),
                    bottom_area_req.ToString("0.00"),
                    req_dist_spacing_bars.ToString("0.00"));
                req_dist_spacing_bars = (int)req_dist_spacing_bars / 10;
                req_dist_spacing_bars = req_dist_spacing_bars * 10;

                if (req_dist_spacing_bars > 200)
                    req_dist_spacing_bars = 200;

                sw.WriteLine("Provide T{0} @ {1} c/c",
                    dist_dia,
                    req_dist_spacing_bars);

                bottom_ast = dist_ast * 1000.0 / req_dist_spacing_bars;
                double bottom_ast_provided = dist_ast * 1000 / req_dist_spacing_bars;

                sw.WriteLine("Provided Area of steel = {0} sq.mm.", bottom_ast_provided.ToString("0.00"));

                #endregion

                #region Step 6

            step_6:
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Reinforcement in Longer Direction");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Top Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Negative Bending Moment   = My2 = {0} kN-m.", My2.ToString("0.00"));
                sw.WriteLine("Lever Arm Coefficient = j = 0.5 + √((0.25) - ((My2 * 10E+5)/(0.87*σ_ck*1000*d*d)))");
                sw.WriteLine("                          = 0.5 + √((0.25) - (({0} * 10E+5)/(0.87 * {1} * 1000 * {2} * {2})))",
                    My2.ToString("0.00"),
                    sigma_ck,
                    d);

                double j4;
                j4 = 0.5 + Math.Sqrt((0.25) - ((Math.Abs(My2) * 10E+5) / (0.87 * sigma_ck * 1000 * d * d)));

                sw.WriteLine("                          = {0} ", j4.ToString("0.00"));


                double bottom_area_req_1 = (Math.Abs(My2) * 10E+5) / (0.87 * j4 * sigma_y * d);

                sw.WriteLine("Area of steel requirement (at Top) = My2 * 10E+5 / 0.87 * σ_y * j * d");
                sw.WriteLine("                                   = {0} * 10E+5 / 0.87 * {1} * {2} * {3}",
                    My2.ToString("0.00"),
                    sigma_y,
                    j4.ToString("0.00"),
                    d);
                sw.WriteLine("                                   = {0} sq.mm", bottom_area_req_1.ToString("0.00"));


                req_dist_spacing_bars = main_ast * 1000 / bottom_area_req_1;

                sw.WriteLine("Required spacing of T{0} bars = {1} * 1000 / {2} = {3} mm",
                    main_dia,
                    main_ast.ToString("0.00"),
                    bottom_area_req_1.ToString("0.00"),
                    req_dist_spacing_bars.ToString("0.00"));
                req_dist_spacing_bars = (int)req_dist_spacing_bars / 10;
                req_dist_spacing_bars = req_dist_spacing_bars * 10;

                if (req_dist_spacing_bars > 200) req_dist_spacing_bars = 200;


                sw.WriteLine("Provide T{0} @ {1} c/c",
                    main_dia,
                    req_dist_spacing_bars);

                //bottom_ast = main_ast * 1000.0 / req_dist_spacing_bars;
                bottom_ast_provided = main_ast * 1000 / req_dist_spacing_bars;

                sw.WriteLine("Provided Area of steel = {0} sq.mm.", bottom_ast_provided.ToString("0.00"));
                #endregion

                #region Step 7
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : Distribution Bars (Temperature steel)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Area of Steel required = 0.12 % of (b * D)");
                sw.WriteLine("                       = (0.12/100) * 1000 * {0}", D);
                double percent_steel_area = (0.12 / 100.0) * 1000 * D;
                sw.WriteLine("                       = {0} sq.mm.", percent_steel_area.ToString("0.00"));

                req_dist_spacing_bars = dist_ast * 1000 / percent_steel_area;

                sw.WriteLine(" Required spacing  = {0} * 1000/{1} = {2} ",
                    dist_ast.ToString("0.00"),
                    percent_steel_area.ToString("0.00"),
                    req_dist_spacing_bars.ToString("0.00"));

                double pro_dist_bar = 200;

                if (req_dist_spacing_bars > 200)
                    req_dist_spacing_bars = 200;
                sw.WriteLine("Provide T{0} @ {1} mm c/c",
                    dist_dia,
                    req_dist_spacing_bars.ToString("0.00"));
                double provide_area = dist_ast * 1000 / pro_dist_bar;
                if (provide_area > percent_steel_area)
                {
                    sw.WriteLine("Provide Area of steel = {0} sq.mm. > {1} sq.mm. OK",
                        provide_area.ToString("0.00"),
                        percent_steel_area);
                }
                else
                {
                    sw.WriteLine("Provide Area of steel = {0} sq.mm. < {1} sq.mm. NOT OK",
                        provide_area.ToString("0.00"),
                        percent_steel_area);

                }

                #endregion

                #region Step 8
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : Corner Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Corner with two edges discontinuous");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Area of steel requirement = 0.75 * Maximum span Reinforcement at Bottom");
                //TO DO


                //double max_span_reinforcement = Ast;


                double area_steel_req = 0.75 * max_span_reinforcement;
                sw.WriteLine("                          = 0.75 * {0}", max_span_reinforcement.ToString("0.00"));
                sw.WriteLine("                          = {0} sq.mm.", area_steel_req.ToString("0.00"));

                req_dist_spacing_bars = dist_ast * 1000 / area_steel_req;

                req_dist_spacing_bars = (int)req_dist_spacing_bars / 10;
                req_dist_spacing_bars = req_dist_spacing_bars * 10;
                sw.WriteLine("Provide T{0} @ {1} c/c at Top and Bottom.",
                    dist_dia,
                    req_dist_spacing_bars);

                provide_area = dist_ast * 1000 / req_dist_spacing_bars;
                sw.WriteLine("Provide Area of steel = {0} sq.mm.", provide_area.ToString("0.00"));
                #endregion

                #region Step 9
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Step 9 : Conner Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Conner with one discontinuous.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Area of steel required = 3/8 * Maximum span Reinforcement.");
                sw.WriteLine("                       = 0.375 * {0}", max_span_reinforcement.ToString("0.00"));

                area_steel_req = 0.375 * max_span_reinforcement;
                sw.WriteLine("                       = {0}", area_steel_req.ToString("0.00"));
                req_spacing_bars = dist_ast * 1000 / area_steel_req;

                sw.WriteLine("Required spacing = {0} * 1000 / {1} = {2} mm",
                    dist_ast.ToString("0.00"),
                    area_steel_req.ToString("0.00"),
                    req_spacing_bars.ToString("0.00"));

                req_dist_spacing_bars = dist_ast * 1000 / 200;

                if (req_dist_spacing_bars > 200)
                    req_dist_spacing_bars = 200.0;

                sw.WriteLine("Provide T{0} @ {1:f0} mm c/c.", dist_dia, req_dist_spacing_bars);
                sw.WriteLine("Provide Area of steel = {0} * 1000/ {1} = {2} sq.mm.",
                    dist_ast.ToString("0.00"),
                    200,
                    provide_area.ToString("0.00"));
                #endregion

                #region Step 10
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Step 10 : Reinforcement at discontinuous Edges.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Area of steel required = 0.5 * Maximum span Reinforcement.");
                sw.WriteLine("                       = 0.5 * {0}", max_span_reinforcement.ToString("0.00"));

                area_steel_req = 0.5 * max_span_reinforcement;

                sw.WriteLine("                       = {0}", area_steel_req.ToString("0.00"));
                req_spacing_bars = dist_ast * 1000 / area_steel_req;

                sw.WriteLine("Required spacing = {0} * 1000 / {1} = {2} mm",
                    dist_ast.ToString("0.00"),
                    area_steel_req.ToString("0.00"),
                    req_spacing_bars.ToString("0.00"));

                provide_area = dist_ast * 1000 / 200;
                sw.WriteLine("Provide T{0} @ 200 mm c/c.",
                    dist_dia, provide_area.ToString("0.00"));
                sw.WriteLine("Provide Area of steel = {0} * 1000/ {1} = {2} sq.mm.",
                    dist_ast.ToString("0.00"),
                    req_spacing_bars.ToString("0.00"),
                    provide_area.ToString("0.00"));
                #endregion

                #region Step 11
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 11");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Reinforcement for the edge strips must not be less than distribution steel ");
                sw.WriteLine("(nominal spacing) provide T{0} @ 200 c/c at Bottom.", dist_dia);
                sw.WriteLine("Provide area of steel = 251.3 sq.mm.");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");


                #endregion

                #region Write DESIGN File
                //SLAB04 VIEW
                //long_span = 5700;
                //short_span = 4300;
                //clear_cover = 25;
                //height = 150;
                //start_long_dist = 700;
                //start_short_dist = 500;
                //long_space = 140;
                //short_space = 100;
                //middle_bar_dist = 110;
                //long_span_Corner = 840;
                //short_span_Corner = 900;
                //long_corner_space = 70;
                //short_corner_space = 100;
                //panel_moment_type = 4;
                //FINISH
                double start_long_dist, start_short_dist, long_space, short_space;
                double middle_bar_dist, long_span_Corner, short_span_Corner;
                double long_corner_space, short_corner_space;


                start_long_dist = ly * 1000 / 8;
                start_short_dist = lx * 1000 / 8;

                long_space = 140;
                short_space = 100;
                middle_bar_dist = 110;


                long_span_Corner = (ly*1000)/6.785;
                short_span_Corner = (lx * 1000) / 4.777777;
                //short_span_Corner = (lx * 1000 - (2 * bw - 2 * h)) / 5;
                long_corner_space = long_space / 2;
                short_corner_space = short_space;

                long_span_Corner = (int)(long_span_Corner / long_corner_space);
                short_span_Corner = (int)(short_span_Corner / short_corner_space);
                short_span_Corner += 1.0;
                long_span_Corner *= long_corner_space;
                short_span_Corner *= short_corner_space;

                string design_file = Path.Combine(system_path, "DESIGN.FIL");
                StreamWriter design_write = new StreamWriter(new FileStream(design_file, FileMode.Create));

                try
                {
                    design_write.WriteLine("SLAB04 VIEW");
                    design_write.WriteLine("long_span = {0};", ly * 1000);
                    design_write.WriteLine("short_span = {0};", lx * 1000);
                    design_write.WriteLine("clear_cover = {0};", h);
                    design_write.WriteLine("height = {0};", D);
                    design_write.WriteLine("start_long_dist = {0};", start_long_dist);
                    design_write.WriteLine("start_short_dist = {0};", start_short_dist);
                    design_write.WriteLine("long_space = {0};", long_space);
                    design_write.WriteLine("short_space = {0};", short_space);
                    design_write.WriteLine("middle_bar_dist = {0};", middle_bar_dist);
                    design_write.WriteLine("long_span_Corner = {0};", long_span_Corner);
                    design_write.WriteLine("short_span_Corner = {0};", short_span_Corner);
                    design_write.WriteLine("long_corner_space = {0};", long_corner_space);
                    design_write.WriteLine("short_corner_space = {0};", short_corner_space);
                    design_write.WriteLine("panel_moment_type = {0};", (cmb_BS_CODE.SelectedIndex + 1));
                    design_write.WriteLine("FINISH");
                }
                catch (Exception ex) { }
                finally
                {
                    design_write.Flush();
                    design_write.Close();
                }
                // Slab Thickness
                // Default Data
                //D = 130;
                //W = 25;
                //Pt = 6;
                //Pw = 24;
                //Ft = 30;
                //Fw = 24;
                //Ww = 1;
                //Ll = 4;
                //Df = 1.5;
                //Lf = 1.5;
                //lx = 4.2;
                //ly = 5.9;
                //sigma_ck = 20;
                //sigma_y = 415;
                //bw = 250;
                //h = 15;

                #endregion

                #region Write BoQ

                string size1 = string.Format("{0:f2} x {1:f2}", lx, ly);
                double d1 = 8;
                double d2 = 10;

                string bcd1 = "T" + d1.ToString("0") + "_B1";
                string bcd2 = "T" + d1.ToString("0") + "_B2";
                string bcd3 = "T" + d2.ToString("0") + "_B3";//
                string bcd4 = "T" + d1.ToString("0") + "_B4"; // "T8_B4";
                string bcd5 = "T" + d1.ToString("0") + "_C1";//"T8_C1";
                string bcd6 = "T" + d1.ToString("0") + "_C2";//"T8_C2";
                string bgd = "Fe " + txt_f_y.Text;

                //SLAB04 VIEW
                //long_span = 5700;
                //short_span = 4300;
                //clear_cover = 25;
                //height = 150;
                //start_long_dist = 700;
                //start_short_dist = 500;
                //long_space = 140;
                //short_space = 100;
                //middle_bar_dist = 110;
                //long_span_Corner = 840;
                //short_span_Corner = 900;
                //long_corner_space = 70;
                //short_corner_space = 100;
                //panel_moment_type = 4;
                //FINISH
                lx = lx * 1000;
                ly = ly * 1000;
                double bl1 = lx - 2 * h;

                double bl2 = ly - 2 * h;
                double bl3 = lx - 2 * h - 2 * start_short_dist;
                double bl4 = ly - 2 * h - 2 * start_long_dist;
                double bl5 = short_corner_space;
                double bl6 = long_corner_space;


                double shape1 = start_short_dist + bw - h;//665;
                double shape2 = lx - 2 * h; // 4250
                double shape3 = ly - 2 * h; // 5750
                double shape4 = lx - (2 * h) - 2 * start_short_dist; //3250;
                double shape5 = lx - (2 * h);//4250;
                double shape6 = short_span_Corner;
                double shape7 = long_span_Corner;


                double bno1 = (int)((ly) / (2 * middle_bar_dist));
                double bno2 = (int)((lx) / (2 * short_space)); ;
                double bno3 = (int)(shape5 / (2 * middle_bar_dist));
                double bno4 = (int)(shape4 / (2 * short_space));
                double bno5 = (int)(short_span_Corner / (short_corner_space));
                double bno6 = (int)(long_span_Corner / (long_corner_space));
                double depth1 = d - 2 * h;
                double depth2 =  d - 2 * h + main_dia;

                double bwt1 = 0.00616 * main_dia * main_dia * shape2 * bno1;
                bwt1 = bwt1 / 10e5;
                double bwt2 = 0.00616 * dist_dia * dist_dia * shape3 * bno3;
                bwt2 = bwt2 / 10e5;

                double bwt3 = 0.00616 * main_dia * main_dia * shape4 * bno3;
                bwt3 = bwt3 / 10e5;
                double bwt4 = 0.00616 * dist_dia * dist_dia * shape5 * bno4;
                bwt4 = bwt4 / 10e5;

                double bwt5 = (0.00616 * dist_dia * dist_dia * shape6 * bno5);
                bwt5 = bwt5 / 10e5;
                double bwt6 = 0.00616 * dist_dia * dist_dia * shape7 * bno6;
                bwt6 = bwt6 / 10e5;

                string f_name = Path.Combine(system_path, "BoQ.FIL");
                StreamWriter sw_boq = new StreamWriter(new FileStream(f_name, FileMode.Create));

                try
                {
                    sw_boq.WriteLine("size1={0}",size1);
                    sw_boq.WriteLine("bcd1={0}",bcd1);
                    sw_boq.WriteLine("bcd2={0}",bcd2);
                    sw_boq.WriteLine("bcd3={0}",bcd3);
                    sw_boq.WriteLine("bcd4={0}",bcd4);
                    sw_boq.WriteLine("bcd5={0}", bcd5);
                    sw_boq.WriteLine("bcd6={0}", bcd6);
                    sw_boq.WriteLine("bgd={0}",bgd);
                    sw_boq.WriteLine("d1={0}",d1);
                    sw_boq.WriteLine("d2={0}",d2);
                    sw_boq.WriteLine("bl1={0}",bl1);
                    sw_boq.WriteLine("bl2={0}",bl2);
                    sw_boq.WriteLine("bl3={0}",bl3);
                    sw_boq.WriteLine("bl4={0}",bl4);
                    sw_boq.WriteLine("bl5={0}",bl5);
                    sw_boq.WriteLine("bl6={0}",bl6);

                    sw_boq.WriteLine("bno1={0}",bno1);
                    sw_boq.WriteLine("bno2={0}",bno2);
                    sw_boq.WriteLine("bno3={0}",bno3);
                    sw_boq.WriteLine("bno4={0}",bno4);
                    sw_boq.WriteLine("bno5={0}",bno5);
                    sw_boq.WriteLine("bno6={0}",bno6);
                    sw_boq.WriteLine("depth1={0}",depth1);
                    sw_boq.WriteLine("depth2={0}",depth2);
                    sw_boq.WriteLine("bwt1={0:f4}",bwt1);
                    sw_boq.WriteLine("bwt2={0:f4}", bwt2);
                    sw_boq.WriteLine("bwt3={0:f4}", bwt3);
                    sw_boq.WriteLine("bwt4={0:f4}", bwt4);
                    sw_boq.WriteLine("bwt5={0:f4}", bwt5);
                    sw_boq.WriteLine("bwt6={0:f4}", bwt6);
                    sw_boq.WriteLine("shape1={0}",shape1);
                    sw_boq.WriteLine("shape2={0}",shape2);
                    sw_boq.WriteLine("shape3={0}",shape3);
                    sw_boq.WriteLine("shape4={0}",shape4);
                    sw_boq.WriteLine("shape5={0}",shape5);
                    sw_boq.WriteLine("shape6={0}",shape6);
                    sw_boq.WriteLine("shape7={0}",shape7);
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(ex.ToString());
                    
                }
                finally
                {
                    sw_boq.Flush();
                    sw_boq.Close();
                }

                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }

        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(Path.Combine(system_path, "DRAWING.FIL"),FileMode.Create));
            try
            {
                list_drawing.Add("A=25 mm");
                list_drawing.Add("B=560 mm");
                list_drawing.Add("C=710 mm");
                list_drawing.Add("D=110 mm");
                list_drawing.Add("E=5500 mm");
                list_drawing.Add("F=5590 mm");
                list_drawing.Add("G=8 - 200 c/c");
                list_drawing.Add("H=8 - 100 c/c");
                list_drawing.Add("J=10 - 110 c/c");
                list_drawing.Add("K=8 - 140 c/c");
                foreach (string str in list_drawing)
                {
                    sw.WriteLine(str);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }
        private void InitializeData()
        {
            #region User Input Data
            // 
            D = MyList.StringToDouble(txt_D.Text, 0.0);
            W = MyList.StringToDouble(txt_W.Text, 0.0); ;
            Pt = MyList.StringToDouble(txt_Pt.Text, 0.0); ;
            Pw = MyList.StringToDouble(txt_Pw.Text, 0.0); ;
            Ft = MyList.StringToDouble(txt_Ft.Text, 0.0); ;
            Fw = MyList.StringToDouble(txt_Fw.Text, 0.0); ;
            Ww = MyList.StringToDouble(txt_Ww.Text, 0.0); ;
            Ll = MyList.StringToDouble(txt_Ll.Text, 0.0); ;
            Df = MyList.StringToDouble(txt_Df.Text, 0.0); ;
            Lf = MyList.StringToDouble(txt_Lf.Text, 0.0); ;
            lx = MyList.StringToDouble(txt_lx.Text, 0.0); ;
            ly = MyList.StringToDouble(txt_ly.Text, 0.0); ;
            sigma_ck = MyList.StringToDouble(txt_f_ck.Text, 0.0);
            sigma_y = MyList.StringToDouble(txt_f_y.Text, 0.0);
            bw = MyList.StringToDouble(txt_bw.Text, 0.0);
            h = MyList.StringToDouble(txt_h.Text, 0.0); ;
            #endregion
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                sw.WriteLine();
                sw.WriteLine();

                #region User Input Data
                // 
                sw.WriteLine("D = {0}", txt_D.Text);
                sw.WriteLine("W = {0}", txt_W.Text);
                sw.WriteLine("Pt = {0:f2}", Pt);
                sw.WriteLine("Pw = {0:f2}", Pw);
                sw.WriteLine("Ft = {0:f2}", Ft);
                sw.WriteLine("Fw = {0:f2}", Fw);
                sw.WriteLine("Ww = {0:f2}", Ww);
                sw.WriteLine("Ll = {0:f2}", Ll);
                sw.WriteLine("Df = {0:f2}", Df);
                sw.WriteLine("Lf = {0:f2}", Lf);
                sw.WriteLine("lx = {0}", txt_lx.Text);
                sw.WriteLine("ly = {0}", txt_ly.Text);
                sw.WriteLine("sigma_ck = {0:f0}", sigma_ck);
                sw.WriteLine("sigma_y = {0:f0}", sigma_y);
                sw.WriteLine("bw = {0:f2}", bw);
                sw.WriteLine("h = {0:f2}", h);
                sw.WriteLine("sel_index = {0}", cmb_BS_CODE.SelectedIndex);

                #endregion

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }




        }

        public void Read_User_Input()
        {
            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));

            #region User Input Data
            string kStr = "";
            string option = "";
            MyList mList = null;
            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                mList = new MyList(kStr, '=');
                option =MyList.RemoveAllSpaces(mList.StringList[0]);

                try
                {
                    #region Switch

                    switch (option)
                    {
                        case "D":
                            D = mList.GetDouble(1);
                            txt_D.Text = D.ToString("0.00");
                            break;
                        case "W":
                            W = mList.GetDouble(1);
                            txt_W.Text = W.ToString("0.00");
                            break;
                        case "Pt":
                            Pt = mList.GetDouble(1);
                            txt_Pt.Text = Pt.ToString("0.00");
                            break;
                        case "Pw":
                            Pw = mList.GetDouble(1);
                            txt_Pw.Text = Pw.ToString("0.00");
                            break;
                        case "Ft":
                            Ft = mList.GetDouble(1);
                            txt_Ft.Text = Ft.ToString("0.00");
                            break;
                        case "Fw":
                            Fw = mList.GetDouble(1);
                            txt_Fw.Text = Fw.ToString("0.00");
                            break;
                        case "Ww":
                            Ww = mList.GetDouble(1);
                            txt_Ww.Text = Ww.ToString("0.00");
                            break;
                        case "Ll":
                            Ll = mList.GetDouble(1);
                            txt_Ll.Text = Ll.ToString("0.00");
                            break;
                        case "Df":
                            Df = mList.GetDouble(1);
                            txt_Df.Text = Df.ToString("0.00");
                            break;
                        case "Lf":
                            Lf = mList.GetDouble(1);
                            txt_Lf.Text = Lf.ToString("0.00");
                            break;
                        case "lx":
                            lx = mList.GetDouble(1);
                            txt_lx.Text = lx.ToString("0.00");
                            break;
                        case "ly":
                            ly = mList.GetDouble(1);
                            txt_ly.Text = ly.ToString("0.00");
                            break;
                        case "sigma_ck":
                            sigma_ck = mList.GetDouble(1);
                            txt_f_ck.Text = sigma_ck.ToString("0");
                            break;
                        case "sigma_y":
                            sigma_y = mList.GetDouble(1);
                            txt_f_y.Text = sigma_y.ToString("0");
                            break;
                        case "bw":
                            bw = mList.GetDouble(1);
                            txt_bw.Text = bw.ToString("0.00");
                            break;
                        case "h":
                            h = mList.GetDouble(1);
                            txt_h.Text = h.ToString("0.00");
                            break;
                        case "sel_index":
                            cmb_BS_CODE.SelectedIndex = mList.GetInt(1);
                            break;
                    }

                    #endregion
                }
                catch (Exception ex) { }
            }

            #endregion
            lst_content.Clear();
        }


        public bool Enable_Button
        {
            set
            {
                btnReport.Enabled = value;
                btnView.Enabled = value;
                btnDrawing.Enabled = value;
                btnBoQ.Enabled = value;
            }
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            double lx, ly, fac;
            lx = MyList.StringToDouble(txt_lx.Text, 0.0);
            ly = MyList.StringToDouble(txt_ly.Text, 0.0);

            fac = ly / lx;
            if (fac > 2)
            {
                MessageBox.Show(this, "As ly / lx = " + ly + " / " + lx + " = " + fac.ToString("0.000") + " >= 2\n" +
                    "So, The Slab can't be designed as TWO WAY SLAB.\n" +
                    "Try the design as one way slab.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InitializeData();
            Write_User_Input();
            CalculateProgram(rep_file_name);
            Write_Drawing_File();
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
            iApp.View_Result(rep_file_name);
        }
        public void Beta_x1_x2(double ly_div_lx)
        {
            
            
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult fviewRes = new frmViewResult(rep_file_name);
            fviewRes.ShowDialog();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Data saved in " + Path.Combine(filePathSet, "DESIGN_SLAB04\\SLAB04.txt"));
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.TWO_WAY_RCC_SLAB, eSLAB_Part.VIEW);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.TWO_WAY_RCC_SLAB, eSLAB_Part.DRAWING);
        }
        private void btnBoQ_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.TWO_WAY_RCC_SLAB, eSLAB_Part.BoQ);
        }
        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                }
            }
        }
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Structural Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "RCC Slab");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Two Way RCC Slab");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }

        public string FilePath
        {
            set
            {
                this.Text = "DESIGN OF TWO WAY RCC SLAB : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "TWO_WAY_RCC_SLAB");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");

                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Structural_Twoway_ slab.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_TWO_WAY_RCC_SLAB.FIL");

                #region Set Buttons Enable and Disable

                btnProcess.Enabled = true;
                btnReport.Enabled = File.Exists(user_input_file);
                btnView.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);
                btnBoQ.Enabled = File.Exists(user_input_file);

                #endregion

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }

            }
        }
        

        class ModificationFactor
        {
            double fs, p, mf;
            public ModificationFactor()
            {
                fs = p = mf = 0.0d;
            }
            public double Fs
            {
                get
                {
                    return fs;
                }
                set
                {
                    fs = value;
                }
            }
            public double P
            {
                get
                {
                    return p;
                }
                set
                {
                    p = value;
                }
            }

            public double MF
            {
                get
                {
                    return mf;
                }
                set
                {
                    mf = value;
                }
            }
        }
        class ModificationFactorCollection
        {
            List<ModificationFactor> lstModFac = new List<ModificationFactor>();

            public ModificationFactorCollection()
            {
                lstModFac = new List<ModificationFactor>();
            }
            void Set_FS120()
            {
                ModificationFactor mod_fac = new ModificationFactor();
                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 0.7;
                mod_fac.MF = 2.0;
                lstModFac.Add(mod_fac);


                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 0.8;
                mod_fac.MF = 1.8;
                lstModFac.Add(mod_fac);


                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 1.0;
                mod_fac.MF = 1.6;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 1.2;
                mod_fac.MF = 1.5;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 1.4;
                mod_fac.MF = 1.4;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 1.6;
                mod_fac.MF = 1.35;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 1.8;
                mod_fac.MF = 1.3;
                lstModFac.Add(mod_fac);


                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 2.0;
                mod_fac.MF = 1.25;
                lstModFac.Add(mod_fac);


                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 2.2;
                mod_fac.MF = 1.2;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 2.4;
                mod_fac.MF = 1.175;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 2.6;
                mod_fac.MF = 1.15;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 2.8;
                mod_fac.MF = 1.125;
                lstModFac.Add(mod_fac);



                mod_fac = new ModificationFactor();
                mod_fac.Fs = 120;
                mod_fac.P = 3.0;
                mod_fac.MF = 1.1;
                lstModFac.Add(mod_fac);
            }
            void Set_FS145()
            {
                ModificationFactor mod_fac = new ModificationFactor();

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 0.7;
                mod_fac.MF = 2.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 0.5;
                mod_fac.MF = 2.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 0.6;
                mod_fac.MF = 1.8;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 0.8;
                mod_fac.MF = 1.55;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 1.0;
                mod_fac.MF = 1.45;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 1.2;
                mod_fac.MF = 1.35;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 1.4;
                mod_fac.MF = 1.3;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 1.6;
                mod_fac.MF = 1.2;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 1.8;
                mod_fac.MF = 1.15;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 2.0;
                mod_fac.MF = 1.15;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 2.2;
                mod_fac.MF = 1.1;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 2.4;
                mod_fac.MF = 1.05;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 2.6;
                mod_fac.MF = 1.05;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 2.8;
                mod_fac.MF = 1.05;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 145;
                mod_fac.P = 3.0;
                mod_fac.MF = 1.0;
                lstModFac.Add(mod_fac);

            }
            void Set_FS190()
            {
                ModificationFactor mod_fac = new ModificationFactor();

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 0.25;
                mod_fac.MF = 2.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 0.3;
                mod_fac.MF = 1.8;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 0.4;
                mod_fac.MF = 1.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 0.6;
                mod_fac.MF = 1.45;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 0.8;
                mod_fac.MF = 1.3;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 1.0;
                mod_fac.MF = 1.2;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 1.2;
                mod_fac.MF = 1.15;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 1.4;
                mod_fac.MF = 1.1;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 1.6;
                mod_fac.MF = 1.05;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 1.8;
                mod_fac.MF = 1.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 2.0;
                mod_fac.MF = 0.95;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 2.2;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 2.4;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 2.6;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 2.8;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 190;
                mod_fac.P = 3.0;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

            }
            void Set_FS240()
            {
                ModificationFactor mod_fac = new ModificationFactor();

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 0.1;
                mod_fac.MF = 2.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 0.2;
                mod_fac.MF = 1.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 0.4;
                mod_fac.MF = 1.35;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 0.5;
                mod_fac.MF = 1.15;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 0.8;
                mod_fac.MF = 1.05;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 1.0;
                mod_fac.MF = 1.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 1.2;
                mod_fac.MF = 0.95;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 1.4;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 1.6;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 1.8;
                mod_fac.MF = 0.85;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 2.0;
                mod_fac.MF = 0.85;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 2.2;
                mod_fac.MF = 0.82;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 2.4;
                mod_fac.MF = 0.8;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 2.6;
                mod_fac.MF = 0.8;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 2.8;
                mod_fac.MF = 0.8;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 240;
                mod_fac.P = 3.0;
                mod_fac.MF = 0.75;
                lstModFac.Add(mod_fac);

            }
            void Set_FS290()
            {
                //ModificationFactor m_fac = new ModificationFactor();

                //m_fac.Fs = 290;
                //m_fac.P = p_value;

                //double p1, p2;
                //double mf1, mf2;

                #region Set Value
                ModificationFactor mod_fac = new ModificationFactor();

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 0.0;
                mod_fac.MF = 2.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 0.1;
                mod_fac.MF = 1.6;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 0.2;
                mod_fac.MF = 1.4;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 0.4;
                mod_fac.MF = 1.1;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 0.6;
                mod_fac.MF = 1.0;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 0.8;
                mod_fac.MF = 0.9;
                lstModFac.Add(mod_fac);

                //mod_fac = new ModificationFactor();
                //mod_fac.Fs = 290;
                //mod_fac.P = 1.0;
                //mod_fac.MF = 1.0;

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 1.2;
                mod_fac.MF = 0.8;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 1.4;
                mod_fac.MF = 0.75;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 1.6;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 1.8;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 2.0;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 2.2;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 2.4;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 2.6;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 2.8;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);

                mod_fac = new ModificationFactor();
                mod_fac.Fs = 290;
                mod_fac.P = 3.0;
                mod_fac.MF = 0.7;
                lstModFac.Add(mod_fac);
                #endregion

            }

            public double Get_MF(double fs, double p_value)
            {
                lstModFac.Clear();

                if (fs < 120)
                {
                    Set_FS120();
                }
                else if (fs < 145)
                {
                    Set_FS145();
                }
                else if (fs < 190)
                {
                    Set_FS190();
                }
                else if (fs < 240)
                {
                    Set_FS240();
                }
                else if (fs > 240)
                {
                    Set_FS290();
                }

                ModificationFactor m_fac = new ModificationFactor();

                m_fac.Fs = fs;
                m_fac.P = p_value;

                double p1, p2;
                double mf1, mf2;

                for (int i = 0; i < lstModFac.Count; i++)
                {
                    if (p_value < lstModFac[0].P)
                    {
                        m_fac.MF = lstModFac[0].MF;
                        break;
                    }
                    if (p_value > lstModFac[lstModFac.Count - 1].P)
                    {
                        m_fac.MF = lstModFac[lstModFac.Count - 1].MF;
                        break;
                    }

                    if (lstModFac[i].P == m_fac.P)
                    {
                        m_fac.MF = lstModFac[i].MF;
                        break;
                    }
                    else if (p_value > lstModFac[i].P && p_value < lstModFac[i + 1].P && i < lstModFac.Count - 1)
                    {
                        p1 = lstModFac[i].P;
                        p2 = lstModFac[i + 1].P;
                        mf1 = lstModFac[i].MF;
                        mf2 = lstModFac[i + 1].MF;

                        m_fac.MF = mf1 + ((p_value - p1) / (p1 - p2)) * (mf1 - mf2);
                        break;
                    }
                }
                return m_fac.MF;
            }

        }
        class Bending
        {
            double ly_div_lx;
            double beta_x1;
            double beta_x2;
            double beta_y1;
            double beta_y2;
            int case_no = 0;

            public Bending()
            {
                ly_div_lx = 0.0;
                beta_x1 = 0.0;
                beta_x2 = 0.0;
                beta_y1 = 0.0;
                beta_y2 = 0.0;
                case_no = 0;
            }

            public int CaseNo
            {
                get
                {
                    return case_no;
                }
                set
                {
                    case_no = value;
                }
            }
            public double Ly_div_Lx
            {
                get
                {
                    return ly_div_lx;
                }
                set
                {
                    ly_div_lx = value;
                }
            }
            public double Beta_x1
            {
                get
                {
                    return beta_x1;
                }
                set
                {
                    beta_x1 = value;
                }
            }
            public double Beta_x2
            {
                get
                {
                    return beta_x2;
                }
                set
                {
                    beta_x2 = value;
                }
            }
            public double Beta_y1
            {
                get
                {
                    return beta_y1;
                }
                set
                {
                    beta_y1 = value;
                }
            }
            public double Beta_y2
            {
                get
                {
                    return beta_y2;
                }
                set
                {
                    beta_y2 = value;
                }
            }
        }
        class BendingCollection
        {
            List<Bending> list = null;

            public BendingCollection()
            {
                list = new List<Bending>();
                //Bending bnd = new Bending();
            }
            private void Set_Case_1()
            {
                Bending bnd = new Bending();

                #region Case No 1

                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.032;
                bnd.Beta_x2 = 0.024;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.037;
                bnd.Beta_x2 = 0.028;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.043;
                bnd.Beta_x2 = 0.032;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.047;
                bnd.Beta_x2 = 0.036;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.051;
                bnd.Beta_x2 = 0.039;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.053;
                bnd.Beta_x2 = 0.041;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.060;
                bnd.Beta_x2 = 0.045;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 1;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.065;
                bnd.Beta_x2 = 0.049;

                bnd.Beta_y1 = 0.032;
                bnd.Beta_y2 = 0.024;
                list.Add(bnd);
                #endregion
            }
            private void Set_Case_2()
            {
                Bending bnd = new Bending();
                #region Case No 2

                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.037;
                bnd.Beta_x2 = 0.028;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.043;
                bnd.Beta_x2 = 0.032;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.048;
                bnd.Beta_x2 = 0.036;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.051;
                bnd.Beta_x2 = 0.039;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);




                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.055;
                bnd.Beta_x2 = 0.041;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.057;
                bnd.Beta_x2 = 0.044;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.064;
                bnd.Beta_x2 = 0.048;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);





                bnd = new Bending();
                bnd.CaseNo = 2;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.068;
                bnd.Beta_x2 = 0.052;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);




                #endregion

            }
            private void Set_Case_3()
            {
                Bending bnd = new Bending();
                #region Case No 3

                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.037;
                bnd.Beta_x2 = 0.028;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);




                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.044;
                bnd.Beta_x2 = 0.033;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.052;
                bnd.Beta_x2 = 0.039;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.057;
                bnd.Beta_x2 = 0.044;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.063;
                bnd.Beta_x2 = 0.047;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.067;
                bnd.Beta_x2 = 0.051;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.077;
                bnd.Beta_x2 = 0.059;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 3;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.085;
                bnd.Beta_x2 = 0.065;

                bnd.Beta_y1 = 0.037;
                bnd.Beta_y2 = 0.028;
                list.Add(bnd);



                #endregion

            }
            private void Set_Case_4()
            {
                Bending bnd = new Bending();
                #region Case No 4

                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.047;
                bnd.Beta_x2 = 0.035;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.053;
                bnd.Beta_x2 = 0.040;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);




                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.060;
                bnd.Beta_x2 = 0.045;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.065;
                bnd.Beta_x2 = 0.049;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.071;
                bnd.Beta_x2 = 0.053;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.075;
                bnd.Beta_x2 = 0.056;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.084;
                bnd.Beta_x2 = 0.063;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 4;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.091;
                bnd.Beta_x2 = 0.069;

                bnd.Beta_y1 = 0.047;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);

                #endregion

            }
            private void Set_Case_5()
            {
                Bending bnd = new Bending();
                #region Case No 5

                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.045;
                bnd.Beta_x2 = 0.035;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.049;
                bnd.Beta_x2 = 0.037;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);




                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.052;
                bnd.Beta_x2 = 0.040;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);




                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.056;
                bnd.Beta_x2 = 0.043;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.059;
                bnd.Beta_x2 = 0.044;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.060;
                bnd.Beta_x2 = 0.045;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.065;
                bnd.Beta_x2 = 0.049;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 5;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.069;
                bnd.Beta_x2 = 0.052;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);

                #endregion

            }
            private void Set_Case_6()
            {
                Bending bnd = new Bending();
                #region Case No 6

                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.035;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.043;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.051;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.057;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.063;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.068;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.080;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 6;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0;
                bnd.Beta_x2 = 0.088;

                bnd.Beta_y1 = 0.045;
                bnd.Beta_y2 = 0.035;
                list.Add(bnd);

                #endregion

            }
            private void Set_Case_7()
            {
                Bending bnd = new Bending();
                #region Case No 7

                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.057;
                bnd.Beta_x2 = 0.043;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.064;
                bnd.Beta_x2 = 0.048;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.071;
                bnd.Beta_x2 = 0.053;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.076;
                bnd.Beta_x2 = 0.057;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.080;
                bnd.Beta_x2 = 0.060;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.084;
                bnd.Beta_x2 = 0.064;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.091;
                bnd.Beta_x2 = 0.069;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 7;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.097;
                bnd.Beta_x2 = 0.073;

                bnd.Beta_y1 = 0;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                #endregion

            }
            private void Set_Case_8()
            {
                Bending bnd = new Bending();
                #region Case No 8

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.043;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.051;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.059;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.065;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.071;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.076;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.087;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                bnd = new Bending();
                bnd.CaseNo = 8;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.096;

                bnd.Beta_y1 = 0.057;
                bnd.Beta_y2 = 0.043;
                list.Add(bnd);

                #endregion

            }
            private void Set_Case_9()
            {
                Bending bnd = new Bending();
                #region Case No 9

                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.0;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.056;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.1;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.064;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.2;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.072;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.3;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.079;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);


                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.4;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.085;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.5;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.089;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 1.75;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.1;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);



                bnd = new Bending();
                bnd.CaseNo = 9;
                bnd.Ly_div_Lx = 2.0;
                bnd.Beta_x1 = 0.0;
                bnd.Beta_x2 = 0.107;

                bnd.Beta_y1 = 0.0;
                bnd.Beta_y2 = 0.056;
                list.Add(bnd);

                #endregion

            }

            public Bending Get_Bending(int case_no, double ly_div_lx)
            {
                Bending bnd = new Bending();

                bnd.Ly_div_Lx = ly_div_lx;
                list.Clear();
                #region switch case
                switch (case_no)
                {
                    case 1:
                        Set_Case_1();
                        break;
                    case 2:
                        Set_Case_2();
                        break;

                    case 3:
                        Set_Case_3();
                        break;

                    case 4:
                        Set_Case_4();
                        break;

                    case 5:
                        Set_Case_5();
                        break;

                    case 6:
                        Set_Case_6();
                        break;

                    case 7:
                        Set_Case_7();
                        break;

                    case 8:
                        Set_Case_8();
                        break;

                    case 9:
                        Set_Case_9();
                        break;
                }
                #endregion

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Ly_div_Lx > ly_div_lx)
                    {
                        bnd = list[i - 1];
                        break;
                    }
                }

                return bnd;
            }
        }

    }
}
