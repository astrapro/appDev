using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
namespace AstraFunctionOne.BeamDesign
{
    public partial class frmSimplySupportedSingleSpanBeam : Form
    {
        #region Variable Declaration
        string rep_file_name = "";
        string file_path = "";
        string user_input_file = "";
        string user_path = "";
        string system_path = "";
        bool is_process = false;


        double Lo, b, D, a, Fact, w2, f_ck, f_y, f_s, dia, gamma_c, cover;

        CONCRETE_GRADE con_grade = CONCRETE_GRADE.M15;
        
        #endregion

        IApplication iApp = null;
        string ref_string = "";
        public frmSimplySupportedSingleSpanBeam(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        #region User Methods and Properties

        public void CalculateProgram()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 20.0            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*         DESIGN OF SIMPLY SUPPORTED          *");
                sw.WriteLine("\t\t*             SINGLE SPAN BEAM                *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region User's Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Clear Span [Lo] = {0:f4} m", Lo);
                sw.WriteLine("Width [b] = {0:f4} m", b);
                sw.WriteLine("Depth [D] = {0:f4} m", D);
                sw.WriteLine("Support Width [a] = {0:f4} m", a);
                sw.WriteLine("Imposed Load [w2] = {0:f4} m", w2);
                sw.WriteLine("Load Factor [fact] = {0} ", Fact);
                sw.WriteLine("Concrete Grade [f_ck] = M {0:f0} ", f_ck);
                sw.WriteLine("Steel Grade [f_y] = Fe {0:f0} ", f_y);
                sw.WriteLine("Stress at service Loads [f_s] = {0} MPa N/sq.mm.", f_s);
                sw.WriteLine("Bar Diameter [dia] = {0:f0} mm", dia);
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} ", gamma_c);
                sw.WriteLine("Cover = {0:f4} m", cover);
                sw.WriteLine();
                sw.WriteLine();



                #endregion

                #region Design Calculation
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion



                #region STEP 1
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Calculate Load and Bending Moment");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double L = Lo + 2 * (a / 2.0);
                sw.WriteLine("Effective Span = L = Lo + 2 * (a/2)");
                sw.WriteLine("                   = {0} + 2*({1}/2)", Lo, a);
                sw.WriteLine("                   = {0:f3} m", L);
                sw.WriteLine();
                sw.WriteLine();

                double w1 = b * D * gamma_c;
                sw.WriteLine("Weight Beam = w1 = b * D * γ_c");
                sw.WriteLine("                 = {0} * {1} * {2}", b, D, gamma_c);
                sw.WriteLine("                 = {0:f3} kN/m", w1);
                sw.WriteLine();
                sw.WriteLine("Imposed Load = w2 = {0} kN/m", w2);
                sw.WriteLine();
                sw.WriteLine();

                double w = Fact * (w1 + w2);
                w = double.Parse(w.ToString("0"));
                sw.WriteLine("Design Load = Factored Total Load = w ");
                sw.WriteLine("            = Fact * (w1 + w2)");
                sw.WriteLine("            = {0} * ({1:f3} + {2:f3})", Fact, w1, w2);
                sw.WriteLine("            = {0} kN/m", w);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Design Bending Moment for Simply Supported Beam");
                double M = w * L * L / 8.0;
                M = double.Parse(M.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("   M = w*L*L/8");
                sw.WriteLine("     = {0:f3}*{1}*{1}/8", w, L);
                sw.WriteLine("     = {0} kN-m", M);
                sw.WriteLine();

                #endregion

                #region STEP 2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Design Section");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                double d = 0.0;
                double check_value = 0.0;

                //D -= 0.1;
                //b -= 0.05;
                do
                {

                    check_value = (D * 1000 - cover * 1000 - dia / 2.0);
                    d = (M * 10E5) / (0.138 * f_ck * b * 1000);
                    d = Math.Sqrt(d);

                    sw.WriteLine("Required Effective Depth = d");
                    sw.WriteLine("                         = √((M*10^6)/(0.138*f_ck*b))");
                    sw.WriteLine("                         = √(({0:f2}*10^6)/(0.138*{1:f0}*{2:f2}))", M, f_ck, b * 1000);

                    if (d > check_value)
                    {
                        sw.WriteLine("                         = {0:f2} > {1:f2} (D*1000-cover*1000-(dia/2)) , NOT OK", d, check_value);

                        sw.WriteLine();
                        sw.WriteLine("Let us try Beam Section");
                        sw.WriteLine();
                        D += 0.1;
                        b += 0.05;
                        w1 = b * D * gamma_c;
                        w = Fact * (w1 + w2);
                        M = w * L * L / 8.0;


                        sw.WriteLine("  D = {0:f3} + 0.10 = {1:f3} m", (D - 0.1), D);
                        sw.WriteLine("  b = {0:f3} + 0.05 = {1:f3} m", (b - 0.05), b);
                        sw.WriteLine();
                        sw.WriteLine("              w1 = b * D * γ_c");
                        sw.WriteLine("                 = {0} * {1} * {2}", b, D, gamma_c);
                        sw.WriteLine("                 = {0:f3} kN/m", w1);
                        sw.WriteLine();
                        sw.WriteLine("               w =  Fact * (w1 + w2)");
                        sw.WriteLine("                 = {0} * ({1:f3} + {2:f3})", Fact, w1, w2);
                        sw.WriteLine("                 = {0:f3} kN/m", w);
                        sw.WriteLine();
                        sw.WriteLine("   M = w*L*L/8");
                        sw.WriteLine("     = {0:f3}*{1}*{1}/8", w, L);
                        sw.WriteLine("     = {0:f3} kN-m", M);
                        sw.WriteLine();
                    }
                    else
                    {
                        sw.WriteLine("                         = {0:f2} < {1:f2} (D*1000-cover*1000-(dia/2)) , OK", d, check_value);
                    }
                }
                while (d > check_value);


                sw.WriteLine();
                sw.WriteLine();
                double Xm = 0.48 * check_value;

                double Ast = (0.36 * f_ck * b * 1000 * Xm) / (0.87 * f_y);

                sw.WriteLine("Required Steel for tension at bottom = Ast");
                sw.WriteLine();
                sw.WriteLine("Ast = (0.36 * f_ck * b * 1000 * Xm) / (0.87 * f_y)");
                sw.WriteLine("    = (0.36 * {0:f0} * {1:f2} * 0.48 * {2:f3}) / (0.87 * {3:f0})",
                    f_ck, b * 1000, check_value, f_y);
                sw.WriteLine("    = {0:f3} sq.mm", Ast);

                sw.WriteLine();
                sw.WriteLine("Where Xm = 0.48 * d");

                double bar_ast = 0;
                double bar_no = 0.0;
                do
                {
                    bar_no += 2.0;
                    bar_ast = Math.PI * dia * dia / 4.0;
                    bar_ast *= bar_no;
                }
                while (bar_ast < Ast);

                sw.WriteLine();
                sw.WriteLine("Using {0:f0} mm Dia bar, area of {1:f0} bars = {2:f2} sq.mm > {3:f2} sq.mm ,OK ",
                    dia, bar_no, bar_ast, Ast);
                sw.WriteLine();
                sw.WriteLine();

                Ast = bar_ast;
                sw.WriteLine("Provide {0:f0} nos T{1:f0} bars at bottom,",
                    bar_no, dia);
                sw.WriteLine("  Ast = {0:f2} sq.mm", Ast);
                sw.WriteLine("   NO = {0:f0} ", bar_no);
                sw.WriteLine();

                d = check_value;
                double req_ten_st = (0.85 * b * 1000 * d) / f_y;

                sw.WriteLine("Check : Minimum Tension Steel required = (0.85*b*d)/f_y");
                sw.WriteLine("                                       = (0.85*{0}*{1})/{2}",
                    (b * 1000), d, f_y);

                if (req_ten_st < Ast)
                {
                    sw.WriteLine("                                       = {0:f2} sq.mm < {1:f2} sq.mm , OK", req_ten_st, Ast);
                }
                else
                {
                    sw.WriteLine("                                       = {0:f2} sq.mm > {1:f2} sq.mm , NOT OK", req_ten_st, Ast);

                }

                #endregion


                #region STEP 3
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : CURTAILMENT OF TENSION STEEL ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bond Stress (τ_bd) for Mild Steel Bars");
                sw.WriteLine();
                sw.WriteLine("____________________________________________________________");
                sw.WriteLine("Concrete Grade    15    20    25    30    35    40  or more");
                sw.WriteLine("τ_bd (N/sq.mm)    1.0   1.2   1.4   1.5   1.7   1.9 ");
                sw.WriteLine("____________________________________________________________");

                double tau_bd = 0.0;
                if (f_ck == 15)
                    tau_bd = 1.0;
                else if (f_ck == 20)
                    tau_bd = 1.2;
                else if (f_ck == 25)
                    tau_bd = 1.4;
                else if (f_ck == 30)
                    tau_bd = 1.5;
                else if (f_ck == 35)
                    tau_bd = 1.7;
                else if (f_ck >= 40)
                    tau_bd = 1.9;


                sw.WriteLine();
                sw.WriteLine("τ_bd = Bond Stress for Concrete Grade M {0:f0} = {1:f1} N/sq.mm", f_ck, tau_bd);
                sw.WriteLine("     = for HYSD deforced bay increase by 60%");
                sw.WriteLine("     = {0:f1} + {0:f1}*0.6", tau_bd);

                tau_bd = tau_bd + tau_bd * 0.6;
                sw.WriteLine("     = {0:f2} N/sq.mm", tau_bd);
                sw.WriteLine();


                double dev_len = (0.87 * f_y * dia) / (4 * tau_bd);
                sw.WriteLine("Development Length for T{0:f0} bars = Ld", dia);
                sw.WriteLine("  Ld = (0.87 * f_y * dia) / (4 * τ_bd)");
                sw.WriteLine("     = (0.87 * {0:f0} * {1:f0}) / (4 * {2:f2})",
                    f_y, dia, tau_bd);
                sw.WriteLine("     = {0:f2} mm", dev_len);

                dev_len = (int)(dev_len / 100.0);
                dev_len += 1.0;
                dev_len = (dev_len * 100.0);
                sw.WriteLine("     ≈ {0:f0} mm", dev_len);
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("In order to develop full tensile Strength at Mid Span, no curtailment");
                sw.WriteLine("be done up to distance of {0:f0} mm on either side of the Mid Span.", dev_len);
                sw.WriteLine();
                sw.WriteLine("Let us calculate Factored Bending Moment at L/4 from mid span,");
                sw.WriteLine("L/4 = {0:f2}/4 = {1:f3} m.", L, (L / 4.0));
                sw.WriteLine();
                sw.WriteLine("Bending Moment of Left Support Reaction about point at a distance");
                sw.WriteLine("{0:f3} m from Left Support = m1 ", (L / 4.0));
                sw.WriteLine();

                double m1 = Fact * ((w1 + w2) / 2.0) * (L / 4.0);
                m1 = double.Parse(m1.ToString("0"));
                sw.WriteLine("  m1 = Fact * (w/2) * {0:f3}", (L / 4.0));
                sw.WriteLine("     = {0} * ({1:f3}/2) * {2:f3}", Fact, (w1 + w2), (L / 4.0));
                sw.WriteLine("     = {0:f3} kNm", m1);
                sw.WriteLine();
                sw.WriteLine("Bending Moment of Uniformly distributed Load W about point");
                sw.WriteLine("at a distance {0:f3} m from Left Support = m2", (L / 4.0));

                //Problem
                double m2 = (Fact * (w1 + w2) * (L / 4.0) * (L / 4.0)) / 2.0;
                m2 = double.Parse(m2.ToString("0.00"));
                sw.WriteLine("  m2 = Fact * (w/2) * {0:f3}", (L / 4.0));
                sw.WriteLine("     = ({0} * {1:f3} * {2:f3})/2.0", Fact, (w1 + w2), (L / 4.0));
                sw.WriteLine("     = {0} kNm", m2);
                sw.WriteLine();


                // ppp
                m2 = 71.54;
                double tot_ben_mom = m1 + m2;
                sw.WriteLine("Total Bending Moment = m1 + m2");
                sw.WriteLine("                     = {0:f2} + {1:f2}", m1, m2);
                sw.WriteLine("                     = {0:f2} kNm", tot_ben_mom);

                sw.WriteLine();
                sw.WriteLine("Required tension steel at L/4 distance is given by equation.");
                sw.WriteLine();
                sw.WriteLine("  BM = 0.87*f_y*Ast*(d-(f_y*Ast)/(f_ck*b))");
                sw.WriteLine();
                sw.WriteLine("  {0:f2} * 10^6 = 0.87*{1:f2}*Ast*({2:f2}-({1:f0}*Ast)/({3:f0}*{4:f2}))",
                    tot_ben_mom, f_y, d, f_ck, b * 1000);

                double _a, _b, _c, _val, _v1, _v2;

                _val = 0.87 * f_y * d;
                _b = _val;
                _val = (0.87 * f_y * (f_y / (f_ck * b * 1000)));
                _a = _val;

                sw.WriteLine("  {0:f2} * 10^6 = {1:f2}*Ast - {2:f2}*Ast*Ast", tot_ben_mom, _b, _a);


                _c = tot_ben_mom / _a;
                _b = _b / _a;
                _a = _a / _a;

                sw.WriteLine();
                sw.WriteLine("  Ast*Ast - {0:f2}*Ast + {1:f3}*10^6 = 0", _b, _c);
                sw.WriteLine();
                sw.WriteLine("  Ast = ({0:f2} ±√(({0:f2}*{0:f2})-4*{1:f2}*10^6))/2.0", _b, _c);

                _val = (Math.Sqrt(_b * _b - (4 * _c * 10E5)));
                sw.WriteLine();
                sw.WriteLine("      = ({0:f2} ± {1:f2})/2.0", _b, _val);

                _v1 = (_b + (Math.Sqrt(_b * _b - (4 * _c * 10E5)))) / 2.0;
                _v2 = (_b - (Math.Sqrt(_b * _b - (4 * _c * 10E5)))) / 2.0;

                _val = (_v1 < _v2) ? _v1 : _v2;

                Ast = _val;
                sw.WriteLine();
                sw.WriteLine("      = {0:f2} sq.mm", Ast);

                sw.WriteLine();
                sw.WriteLine("Let us bent 2 nos T{0:f0} bars at a distance", dia);
                sw.WriteLine("12 * dia = 12 * {0:f0} = {1:f2} mm away from cut off point.", dia, (dia * 12.0));
                sw.WriteLine();
                sw.WriteLine("This leaves NO - 2 = {0:f0} - 2 = {1:f0} bars at the section", bar_no, (bar_no - 2));

                bar_no -= 2;
                bar_ast = bar_no * (Math.PI * dia * dia / 4.0);

                sw.WriteLine("having Ast = {0:f0}*π*{1:f0}*{1:f0}/4 = {2:f2} sq.mm", bar_no, dia, bar_ast);
                sw.WriteLine("at L/4 distance > required Ast = {0:f2} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("12 * dia = {0:f0} mm is less than d = {1:f2} mm", (12 * dia), d);
                sw.WriteLine();
                sw.WriteLine("Hence, 2 nos T{0:f0} bars may be bent or curtailed at a distance", dia);

                double dist = (L / 4) + (d / 1000.0);
                sw.WriteLine("  = (L/4) + ({0:f2}/1000)", d);
                sw.WriteLine("  = {0:f3} + {1:f3}", (L / 4.0), (d / 1000.0));
                sw.WriteLine("  = {0:f3} m from either side of Mid Span.", dist);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Design Standard specifies that at least one third of Total bars");
                sw.WriteLine("must go inside the supports.");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("We shall try both alternatives");
                sw.WriteLine(" (i)  Curtailment of 2 nos. T{0:f0} bars", dia);
                sw.WriteLine(" (ii) Bending up of 2 nos T{0:f0} bars", dia);
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Alternative-I Curtailment of 2 nos bars");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Shear Force at (L/4) from centre of span");

                double V = ((w1 + w2) * (Lo / 2.0) - (w1 + w2) * (Lo / 4.0)) * Fact;
                sw.WriteLine("  V = (w * (Lo / 2.0) - w * (Lo / 4.0)) * Fact");
                sw.WriteLine("    = ({0:f3} * {1:f2} - {0:f3} * {2:f3}) * {3}", (w1 + w2), (Lo / 2.0), (Lo / 4.0), Fact);
                sw.WriteLine("    = {0:f3} kN", V);
                sw.WriteLine();


                bar_ast = double.Parse(bar_ast.ToString("0"));

                sw.WriteLine("NO-2 = {0:f0} - 2 = {1:f0}  T{2:f0} bars are containing beyond the point ", (bar_no + 2), bar_no, dia);
                sw.WriteLine("of Curtailment, Ast = {0:f0}*π*{1:f0}*{1:f0}/4 = {2} sq.mm", bar_no, dia, bar_ast);
                sw.WriteLine();
                sw.WriteLine();


                double p = (bar_ast * 100.0) / (b * 1000.0 * d);
                sw.WriteLine("Percentage of Steel = p = ({0} * 100) / ({1} * {2})", bar_ast, (b * 1000), d);
                sw.WriteLine("                        = {0:f2}%", p);
                sw.WriteLine();

                //Get Table Value Tau_c

                //double tau_c = 0.48;

                double tau_c = Get_Tau_C(p, con_grade);
                sw.WriteLine("Respective Shear strength of Concrete = τ_c = {0:f2} N/sq.mm.", tau_c);
                sw.WriteLine();
                double tau_v = V * 1000 / (b * 1000 * d);
                tau_v = double.Parse(tau_v.ToString("0.000"));
                sw.WriteLine("Nominal Shear Stress = τ_v");
                sw.WriteLine("                     = V * 1000 / (b * 1000 * d)");
                sw.WriteLine("                     = {0:f2} * 1000 / ({1:f2} * {2:f2})", V, (b * 1000), d);
                sw.WriteLine("                     = {0} N/sq.mm", tau_v);

                sw.WriteLine();
                sw.WriteLine();


                double max_stress = 0.0;

                if (f_ck == 15)
                    max_stress = 2.5;
                else if (f_ck == 20)
                    max_stress = 2.8;
                else if (f_ck == 25)
                    max_stress = 3.1;
                else if (f_ck == 30)
                    max_stress = 3.5;
                else if (f_ck == 35)
                    max_stress = 3.7;
                else if (f_ck >= 40)
                    max_stress = 4.0;

                if (max_stress > tau_v)
                {
                    sw.WriteLine("Maximum permissible shear stress in M{0:f0} concrete = {1:f3} > {2:f3} N/sq.mm,OK", f_ck, max_stress, tau_v);
                }
                else
                {
                    sw.WriteLine("Maximum permissible shear stress in M{0:f0} concrete = {1:f3} < {2:f3} N/sq.mm, NOT OK", f_ck, max_stress, tau_v);
                }
                sw.WriteLine();
                sw.WriteLine("___________________________________________________________________");
                sw.WriteLine("Concrete Grade M         15    20    25    30    35    40  or more");
                sw.WriteLine("Maximum Permissible      2.5   2.8   3.1   3.5   3.7   4.0");
                sw.WriteLine("Shear Stress (N/sq.mm)");
                sw.WriteLine("___________________________________________________________________");
                sw.WriteLine();
                sw.WriteLine("Increasing nominal shear stress by 50% at the cut off point");

                double tau_us = 1.5 * tau_v - tau_c;
                sw.WriteLine("Design shear stress = τ_us");
                sw.WriteLine("                    = 1.5 * {0:f3} - {1:f3}", tau_v, tau_c);
                sw.WriteLine("                    = {0:f3} N/sq.mm.", tau_us);
                sw.WriteLine();

                double Asv = Math.PI * 8 * 8 / 4.0;
                Asv *= 2;
                Asv = double.Parse(Asv.ToString("0.00"));
                sw.WriteLine("Use 8mm, 2 legged stirrups giving Asv = {0:f1} sq.mm", Asv);
                sw.WriteLine();

                double x = (0.87 * f_y * Asv) / (tau_us * b * 1000);
                sw.WriteLine("Spacing of Stirrups    x = (0.87 * f_y * Asv) / (τ_us * b * 1000)");
                sw.WriteLine("                         = (0.87 * {0:f0} * {1:f2}) / ({2:f4} * {3:f2})", f_y, Asv, tau_us, (b * 1000));
                sw.WriteLine();

                if (x > 100)
                {
                    sw.WriteLine("                         = {0:f2} mm > 100 mm OK", x);
                }
                else
                {
                    sw.WriteLine("                         = {0:f2} mm < 100 mm , NOT OK", x);
                }
                sw.WriteLine();

                if (x > 300)
                {
                    sw.WriteLine("                         = {0:f2} mm > 300 mm, NOT OK", x);
                }
                else
                {
                    sw.WriteLine("                         = {0:f2} mm < 100 mm , OK", x);
                } 
                
                sw.WriteLine();
                if (x < (0.75*d))
                {
                    sw.WriteLine("                         = {0:f2} mm < (0.75*d) = {1:f2} mm, OK", x, (0.75 * d));
                }
                else
                {
                    sw.WriteLine("                         = {0:f2} mm > (0.75*d) = {1:f2} mm, Not OK", x, (0.75 * d));
                }

                //Problem
                //check_value = 0.75 * d;
                //if(x < check_value


                sw.WriteLine();

                x = (0.87 * f_y * Asv) / (0.4 * b * 1000);
                sw.WriteLine("Spacing of minimum shear stirrups");
                sw.WriteLine(" x = (0.87 * f_y * Asv) / (0.4 * b * 1000)");
                sw.WriteLine("   = (0.87 * {0:f0} * {1:f2}) / ({2} * {3:f2})", f_y, Asv, 0.4, (b * 1000));
                if (x < 300)
                {
                    sw.WriteLine("   = {0:f2} mm < 300 mm , OK", x);
                }

                if (x > 250)
                    x = 250;
                else
                {
                    x = (int)(x / 10.0);
                    x = (x * 10.0);
                }
                sw.WriteLine();
                sw.WriteLine("Provide 8 mm, 2 legged stirrups @{0:f0} mm c/c", x);

                #endregion

                #region STEP 5
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Alternative-II, Bending up of 2 bars at 45°");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Nominal Shear Stress τ_v = {0:f3} N/sq.mm", tau_v);
                sw.WriteLine();
                sw.WriteLine("Shear strength of Concrete τ_c = {0} N/sq.mm", tau_c);
                sw.WriteLine();
                sw.WriteLine("Design Shear stress τ_us = 1.5 * τ_v - τ_c");
                sw.WriteLine("                         = 1.5 * {0:f3} - {1:f3}", tau_v, tau_c);
                sw.WriteLine("                         = {0:f3} N/sq.mm", tau_us);
                sw.WriteLine();

                Ast = (Math.PI * dia * dia / 4.0) * 2.0;
                sw.WriteLine("2 T{0:f0} bars, Ast = {1:f2} sq.mm", dia, Ast);
                sw.WriteLine("Vertical Component of resisted by above steel");

                double vert_comp = 0.87 * f_y * Ast * (Math.Sin((Math.PI / 180.0) * 45.0));
                sw.WriteLine("  = 0.87 * f_y * Ast * Sin 45°");
                sw.WriteLine("  = 0.87 * {0:f0} * {1:f2} * {2:f2}", f_y, Ast, Math.Sin((Math.PI / 180.0) * 45.0));
                sw.WriteLine("  = {0:f2} N", vert_comp);

                sw.WriteLine();

                double tau_b = vert_comp / (b * 1000 * d);
                sw.WriteLine("Corresponding Shear stress = τ_b ");
                sw.WriteLine("                           = {0:f2} / ({1:f2} * {2:f2})", vert_comp, (b * 1000), d);
                sw.WriteLine("                           = {0:f3} N/sq.mm", tau_b);
                sw.WriteLine();
                sw.WriteLine("The bent up bars are sufficient to resist the shear stress,but ");
                sw.WriteLine("the design standard requires minimum shear stirrups in the beam,");
                sw.WriteLine();
                sw.WriteLine("Provided, 8 mm, 2-legged shear stirrups at 250 mm c/c.");

                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : CHECK FOR MAXIMUM SHEAR");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Maximum Shear force accurs at the inner edge of supports,");

                V = (w1 + w2) * (Lo / 2.0);
                sw.WriteLine("  V = (W*Lo/2.0)");
                sw.WriteLine("    = ({0:f2}*{1:f2}/2.0)", (w1 + w2), Lo);
                sw.WriteLine("    = {0:f2} kN", V);
                sw.WriteLine();
                sw.WriteLine();

                double Vu = Fact * V;
                sw.WriteLine("Factored Shear force = Vu ");
                sw.WriteLine("                     = {0:f2} * {1:f2}", Fact, V);
                sw.WriteLine("                     = {0:f2} kN", Vu);
                sw.WriteLine();


                tau_v = (Vu * 1000) / (b * 1000 * d);
                sw.WriteLine("Nominal Shear Stress = τ_v");
                sw.WriteLine("                     = (Vu * 1000) / (b * d)");
                sw.WriteLine("                     = ({0:f2} * 1000) / ({1:f2} * {2:f2})", Vu, (b * 1000), d);
                sw.WriteLine("                     = {0:f2} N/sq.mm", tau_v);
                sw.WriteLine();

                // Problem

                sw.WriteLine("Maximum permissible Shear Stress in M{0:f0} Concrete", f_ck);
                if (max_stress > tau_v)
                {
                    sw.WriteLine("                                    = {0:f2} N/sq.mm > τ_v , OK", max_stress);
                }
                else
                {
                    sw.WriteLine("                                    = {0:f2} N/sq.mm < τ_v , NOT OK", max_stress);
                }
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("For {0:f2}% tension steel, Shear strength of Concrete", p);
                sw.WriteLine("                                             τ_c = {0:f2} N/sq.mm", tau_c);
                sw.WriteLine();

                tau_us = tau_v - tau_c;
                sw.WriteLine("Design Shear Stress = τ_us");
                sw.WriteLine("                    = τ_v - τ_c = {0:f2} - {1:f2}");
                sw.WriteLine("                    = {0:f2} - {1:f2}", tau_v, tau_c);
                sw.WriteLine("                    = {0:f2} N/sq.mm", tau_us);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Again minimum stirrups are required.");










                #endregion

                #region STEP 7
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : CHECK FOR DEVELOPMENT LENGTH AT SUPPORT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("     Total Length of each of the 4 nos T{0:f0} bars beyond the ", dia);
                sw.WriteLine("cut off / Bent up point must be at least Ld in order to develop");
                sw.WriteLine("full tensile strength.");
                sw.WriteLine();

                double Ld = dev_len;

                sw.WriteLine("Development Length = Ld = {0:f2} mm", Ld);
                sw.WriteLine();
                sw.WriteLine("Design Standard specifies that Ld/3 length must go inside the Support.");
                sw.WriteLine();

                sw.WriteLine("So, Length at bay extending beyond cut off / Bent up Point");
                double Le = (Lo / 2 - Ld / 1000) + (Ld / (3 * 1000));

                sw.WriteLine("Le = (Lo/2 - Ld/1000) + Ld/(3*1000)");
                sw.WriteLine("   = ({0:f2}/2 - {1:f2}) + {1:f2}/3", Lo, (Ld / 1000.0));
                sw.WriteLine("   = {0:f2} m", Le);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Length available from cut off / Bent up point to outer side of the Support");

                double La = ((Lo / 2.0) - (Ld / 1000)) + a;
                sw.WriteLine(" = (Lo/2 - Ld/1000) + a");
                sw.WriteLine(" = ({0:f2}/2 - {1:f2}) + {2:f3}", Lo, (Ld / 1000.0), a);

                if (Le < La)
                {
                    sw.WriteLine(" = {0:f2} m > {1:f2} m (Le), OK", La, Le);
                }
                else
                {
                    sw.WriteLine(" = {0:f2} m < {1:f2} m (Le), NOT OK", La, Le);
                    sw.WriteLine();
                    sw.WriteLine("Le = La - Cover");
                    sw.WriteLine("   = {0:f2} - {1:f2}", La, cover);

                    Le = La - cover;
                    sw.WriteLine("   = {0:f2} m", Le);
                }

                #endregion

                #region STEP 8
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : CHECK FOR BOND");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Moment of resistance provided by 4 nos T{0:f0} bars", dia);
                sw.WriteLine();
                //bar_ast
                double MR = 0.87 * f_y * bar_ast * (d - ((f_y * bar_ast) / (f_ck * b * 1000.0)));

                MR = MR;
                sw.WriteLine("MR = 0.87*f_y*Ast*[d-((f_y*Ast)/(f_ck*b))]");
                sw.WriteLine("   = 0.87*{0:f0}*{1:f0}*[{2:f2}-(({0:f0}*{1:f0})/({3:f0}*{4:f2}))]",
                    f_y, bar_ast, d, f_ck, (b * 1000));

                sw.WriteLine();
                MR = (MR / 10E5);
                sw.WriteLine("   = {0:f2} * 10^6 N-mm", MR);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Factored Shear Force at Centre of Support");

                double Vcs = Fact * (w1 + w2) * L / 2.0;
                sw.WriteLine(" Vcs = {0} * {1:f3} * {2:f2}/2", Fact, (w1 + w2), L);
                sw.WriteLine("     = {0:f3} kN", Vcs);
                sw.WriteLine();
                sw.WriteLine("Development Length of a bar = 56 * dia");
                sw.WriteLine();


                double Ls = (Ld / 3.0) - (a * 1000 / 2.0);
                sw.WriteLine("Let us assume, embedment Length of Bar = Ls");
                sw.WriteLine("                                       = Ld/3 - (a*1000/2)");
                sw.WriteLine("                                       = {0:f2}/3 - ({1:f2}/2)", Ld, (a * 1000));
                sw.WriteLine("                                       = {0:f2} mm", Ls);
                sw.WriteLine();


                double phi = ((4 * tau_bd) / (0.87 * f_y)) * (((1.3 * MR * 10E5) / (Vcs * 10E2)) + Ls);
                sw.WriteLine("φ = ((4*τ_bd)/(0.87*f_y)) * [(1.3*MR/Vcs) + Ls]");
                sw.WriteLine("  = ((4*{0:f2})/(0.87*{1:f2})) * [(1.3*{2:f2}*10^6/{3:f2}*10E2) + {4:f2}]",
                    tau_bd, f_y, MR, Vcs, Ls);
                sw.WriteLine();
                sw.WriteLine("  = {0:f2} mm", phi);
                sw.WriteLine();

                if (dia < phi)
                {
                    sw.WriteLine("Diameter of provided bars = dia = {0:f0} < φ, OK", dia);
                }
                else
                {
                    sw.WriteLine("Diameter of provided bars = dia = {0:f0} > φ, NOT OK", dia);
                }








                #endregion

                #region STEP 9
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 9 : CHECK FOR DEFLECTION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Main reinforcement steel beyond the cut off / Bent up Point");
                sw.WriteLine("  = 4 nos  T{0:f0} bars, Ast = {1:f0}", dia, bar_ast);

                double pt = (bar_ast * 100) / (b * 1000 * d);

                sw.WriteLine("pt = ({0:f0}*100)/({1:f2}*{2:f2})", bar_ast, (b * 1000), d);
                sw.WriteLine("   = {0:f2}%", pt);
                sw.WriteLine();

                double gama = Get_Gamma(pt, (int)f_s);
                sw.WriteLine("fs = 240 N/sq.mm, From Table 2,  γ = {0:f2}", gama);
                sw.WriteLine();
                sw.WriteLine("Minimum effective depth of beam, Safe against deflection");
                sw.WriteLine();
                sw.WriteLine("        = L/(αβγδλ)");
                double min_eff_dep = (L / (10.0 * 1 * 1.2));
                sw.WriteLine("        = {0:f2}/(10*1*1.2*1*1)", L);
                sw.WriteLine("        = {0:f2} m", min_eff_dep);
                sw.WriteLine();
                sw.WriteLine();

                _val = d / 1000.0;

                if (_val > min_eff_dep)
                {
                    sw.WriteLine("Provide Effective Depth = {0:f3} m > {1:f3} m, OK", _val, min_eff_dep);
                }
                else
                {
                    sw.WriteLine("Provide Effective Depth = {0:f3} m > {1:f3} m, NOT OK", _val, min_eff_dep);
                }


                #endregion


                #region Printed Tables
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Table 1 : DESIGN SHEAR STRENGTH OF CONCRETE, τ_c, N/sq.mm");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("((100*As)/bd) __________________Concrete Grade______________________");
                sw.WriteLine("              M 15      M 20      M 25      M 30      M 35      M 40 and above");
                sw.WriteLine("     (1)       (2)       (3)       (4)       (5)       (6)       (7)");
                sw.WriteLine("  <=0.15      0.28      0.28      0.29      0.29      0.29      0.30");
                sw.WriteLine("    0.25      0.35      0.36      0.36      0.37      0.37      0.38");
                sw.WriteLine("    0.50      0.46      0.48      0.49      0.50      0.50      0.51");
                sw.WriteLine("    0.75      0.54      0.56      0.57      0.59      0.59      0.60");
                sw.WriteLine("    1.00      0.60      0.62      0.64      0.66      0.67      0.68");
                sw.WriteLine("    1.25      0.64      0.67      0.70      0.71      0.73      0.74");
                sw.WriteLine("    1.50      0.68      0.72      0.74      0.76      0.78      0.79");
                sw.WriteLine("    1.75      0.71      0.75      0.78      0.80      0.82      0.84");
                sw.WriteLine("    2.00      0.71      0.79      0.82      0.84      0.86      0.88");
                sw.WriteLine("    2.25      0.71      0.81      0.85      0.88      0.90      0.92");
                sw.WriteLine("    2.50      0.71      0.82      0.88      0.91      0.93      0.95");
                sw.WriteLine("    2.75      0.71      0.82      0.90      0.94      0.96      0.98");
                sw.WriteLine("    3.00      0.71      0.82      0.92      0.96      0.99      1.01");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Table 2 : MODIFICATION FACTOR FOR TENSION REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("SL.N  (%)       (fs = 290)  (fs = 240)  (fs = 190)  (fs = 145) (fs = 120)");
                sw.WriteLine("                 (N/sq.mm)   (N/sq.mm)   (N/sq.mm)   (N/sq.mm)  (N/sq.mm)");
                sw.WriteLine();
                sw.WriteLine("1     0.00       2.000");
                sw.WriteLine("2     0.20       1.370       1.685");
                sw.WriteLine("3     0.40       1.030       1.330       1.670");
                sw.WriteLine("4     0.60       0.980       1.157       1.420       1.770");
                sw.WriteLine("5     0.80       0.900       1.050       1.280       1.550       1.770");
                sw.WriteLine("6     1.00       0.850       0.990       1.194       1.420       1.600");
                sw.WriteLine("7     1.20       0.810       0.943       1.123       1.330       1.490");
                sw.WriteLine("8     1.40       0.780       0.910       1.070       1.260       1.410");
                sw.WriteLine("9     1.60       0.750       0.885       1.030       1.220       1.340");
                sw.WriteLine("10    1.80       0.730       0.845       0.985       1.160       1.285");
                sw.WriteLine("11    2.00       0.720       0.830       0.960       1.120       1.230");
                sw.WriteLine("12    2.20       0.690       0.825       0.930       1.085       1.200");
                sw.WriteLine("13    2.40       0.680       0.805       0.910       1.060       1.168");
                sw.WriteLine("14    2.60       0.670       0.790       0.900       1.035       1.140");
                sw.WriteLine("15    2.80       0.665       0.787       0.890       1.020       1.120");
                sw.WriteLine("16    3.00       0.663       0.785       0.880       1.000       1.100");


                #endregion

                #region End of Report
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion


            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }
        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "Lo":
                            Lo = mList.GetDouble(1);
                            txt_Lo.Text = Lo.ToString();
                            break;
                        case "b":
                            b = mList.GetDouble(1);
                            txt_b.Text = b.ToString();
                            break;
                        case "D":
                            D = mList.GetDouble(1);
                            txt_D.Text = D.ToString();
                            break;
                        case "a":
                            a = mList.GetDouble(1);
                            txt_a.Text = a.ToString();
                            break;
                        case "w2":
                            w2 = mList.GetDouble(1);
                            txt_w2.Text = w2.ToString();
                            break;
                        case "fact":
                            Fact = mList.GetDouble(1);
                            txt_fact.Text = Fact.ToString();
                            break;
                        case "f_ck":
                            f_ck = mList.GetDouble(1);
                            txt_f_ck.Text = f_ck.ToString();
                            break;
                        case "f_y":
                            f_y = mList.GetDouble(1);
                            txt_f_y.Text = f_y.ToString();
                            break;
                        case "f_s":
                            f_s = mList.GetDouble(1);
                            txt_f_s.Text = f_s.ToString();
                            break;
                        case "dia":
                            dia = mList.GetDouble(1);
                            txt_dia.Text = dia.ToString();
                            break;

                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "Cover":
                            cover = mList.GetDouble(1);
                            txt_cover.Text = cover.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("Lo = {0:f4}", Lo);
                sw.WriteLine("b = {0:f4} ", b);
                sw.WriteLine("D = {0:f4} ", D);
                sw.WriteLine("a = {0:f4} ", a);
                sw.WriteLine("w2 = {0:f4}", w2);
                sw.WriteLine("fact = {0} ", Fact);
                sw.WriteLine("f_ck = {0:f0} ", f_ck);
                sw.WriteLine("f_y = {0:f0} ", f_y);
                sw.WriteLine("f_s = {0}", f_s);
                sw.WriteLine("dia = {0:f0}", dia);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("Cover = {0:f4} ", cover);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }
        void InitializeData()
        {
            Lo = MyList.StringToDouble(txt_Lo.Text, 0.0);
            b = MyList.StringToDouble(txt_b.Text, 0.0);
            D = MyList.StringToDouble(txt_D.Text, 0.0);
            a = MyList.StringToDouble(txt_a.Text, 0.0);
            w2 = MyList.StringToDouble(txt_w2.Text, 0.0);
            Fact = MyList.StringToDouble(txt_fact.Text, 0.0);
            f_ck = MyList.StringToDouble(txt_f_ck.Text, 0.0);
            f_y = MyList.StringToDouble(txt_f_y.Text, 0.0);
            f_s = MyList.StringToDouble(txt_f_s.Text, 0.0);
            dia = MyList.StringToDouble(txt_dia.Text, 0.0);
            gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
            cover = MyList.StringToDouble(txt_cover.Text, 0.0);

            con_grade = (CONCRETE_GRADE)(int)(f_ck);
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

            kPath = Path.Combine(kPath, "RCC Beam");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }


            kPath = Path.Combine(kPath, "Design of Simply Supported Single Span Beam");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }



        /// <summary>
        /// Set All Paths
        /// </summary>
        public string FilePath
        {
            set
            {
                this.Text = "DESIGN OF SIMPLY SUPPORTED SINGLE SPAN BEAM" + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "SIMPLY_SUPPORTED_SINGLE_SPAN_BEAM");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Structural_Simply_Supp_Sing_Span_Beam.TXT");
                user_input_file = Path.Combine(system_path, "SIMPLY_SUPPORTED_SINGLE_SPAN_BEAM.FIL");

                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }
            }

        }
        #endregion


        #region Form Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            CalculateProgram();
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
            iApp.View_Result(rep_file_name);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult f_v_r = new frmViewResult(rep_file_name);
            f_v_r.ShowDialog();
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {

        }
        #endregion


        public double Get_Tau_C(double percent_val, CONCRETE_GRADE con_grade)
        {
            return iApp.Tables.Permissible_Shear_Stress(percent_val, con_grade, ref ref_string );

            //string table_file = Path.Combine(Application.StartupPath, "Tables");
            //table_file = Path.Combine(table_file, "Simply_Support_Tab_1.txt");


            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, b1, a2, b2, tau_c;

            //a1 = 0.0;
            //b1 = 0.0;
            //a2 = 0.0;
            //b2 = 0.0;
            //tau_c = 0.0;


            //int indx = -1;


            //switch (con_grade)
            //{
            //    case CONCRETE_GRADE.M15:
            //        indx = 1;
            //        break;
            //    case CONCRETE_GRADE.M20:
            //        indx = 2;
            //        break;
            //    case CONCRETE_GRADE.M25:
            //        indx = 3;
            //        break;
            //    case CONCRETE_GRADE.M30:
            //        indx = 4;
            //        break;
            //    case CONCRETE_GRADE.M35:
            //        indx = 5;
            //        break;
            //    case CONCRETE_GRADE.M40:
            //        indx = 6;
            //        break;
            //}

            //List<MyList> lst_list = new List<MyList>();

            //for (int i = 0; i < lst_content.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(lst_content[i]);
            //    kStr = kStr.Replace("<=", "");
            //    if (kStr.StartsWith("--------------"))
            //    {
            //        find = !find; continue;
            //    }
            //    if (find)
            //    {
            //        mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);
            //    if ((percent_val < a1) && i == 0)
            //    {
            //        tau_c = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    if (a1 == percent_val)
            //    {
            //        tau_c = lst_list[i].GetDouble(indx);
            //        break;

            //    }
            //    else if (a1 > percent_val)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i-1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        tau_c = b1 + ((b2 - b1) / (a2 - a1)) * (percent_val - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //tau_c = Double.Parse(tau_c.ToString("0.00"));
            //return tau_c;
        }
        
        public double Get_Gamma(double percent_val, int fs)
        {

            return iApp.Tables.Constant_Gamma(percent_val, fs, ref ref_string);

            //percent_val = Double.Parse(percent_val.ToString("0.00"));

            //string table_file = Path.Combine(Application.StartupPath, "Tables");
            //table_file = Path.Combine(table_file, "Simply_Support_Tab_2.txt");


            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, b1, a2, b2, gama;

            //a1 = 0.0;
            //b1 = 0.0;
            //a2 = 0.0;
            //b2 = 0.0;
            //gama = 0.0;

            //int indx = -1;



            //switch (fs)
            //{
            //    case 290:
            //        indx = 2;
            //        break;
            //    case 240:
            //        indx = 3;
            //        break;
            //    case 190:
            //        indx = 4;
            //        break;
            //    case 145:
            //        indx = 5;
            //        break;
            //    case 120:
            //        indx = 6;
            //        break;
            //}

            //List<MyList> lst_list = new List<MyList>();

            //for (int i = 0; i < lst_content.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(lst_content[i]);
            //    kStr = kStr.Replace("<=", "");
            //    if (kStr.StartsWith("--------------"))
            //    {
            //        find = !find; continue;
            //    }
            //    if (find)
            //    {
            //        mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(1);

            //    if (a1 == percent_val)
            //    {
            //        gama = lst_list[i].GetDouble(1);
            //        break;

            //    }
            //    else if (a1 > percent_val)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(1);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        gama = b1 + ((b2 - b1) / (a2 - a1)) * (percent_val - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //gama = Double.Parse(gama.ToString("0.000"));
            //return gama;
        }

        private void frmSimplySupportedSingleSpanBeam_Load(object sender, EventArgs e)
        {

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }

    }
}
