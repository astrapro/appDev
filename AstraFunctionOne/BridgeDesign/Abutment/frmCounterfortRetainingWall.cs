using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.DataStructure;
using AstraInterface.Interface;

namespace AstraFunctionOne.BridgeDesign.Abutment
{
    public partial class frmCounterfortRetainingWall : Form, IDesign
    {
        #region Variable Declaration
        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string drawing_path = "";
        string user_input_file = "";
        bool is_process = false;

        double h1, h2, th1, th2, q0, gama_s, gama_c, P, phi, mu, f_ck, f_y, c, t, m, Kc, jc, Rc, l, th3;
        CONCRETE_GRADE con_grade = CONCRETE_GRADE.M20;

        #endregion

        #region Drawing Variable
        List<string> list_rebar = new List<string>();
        List<string> list_rebar_drawing = new List<string>();
        List<string> list_pressure = new List<string>();
        List<string> list_dims = new List<string>();
        double B, b, d;
        #endregion
        string ref_string = "";
        IApplication iApp = null;
        public frmCounterfortRetainingWall(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
        }

        #region IDesign Members

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_input();
            Calculate_Program(rep_file_name);
            Write_Drawing_File();
            if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            is_process = true;
            FilePath = user_path;
        }

        public void Write_User_input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region USER Data

                sw.WriteLine("h1 = {0} ", txt_h1.Text);
                sw.WriteLine("h1 = {0} ", txt_h1.Text);
                sw.WriteLine("h2 = {0} ", txt_h2.Text);
                sw.WriteLine("th1 = {0} ", txt_th1.Text);
                sw.WriteLine("th2 = {0} ", txt_th2.Text);
                sw.WriteLine("q0 = {0} ", txt_q0.Text);
                sw.WriteLine("gama_s = {0} ", txt_gama_s.Text);
                sw.WriteLine("gama_c = {0} ", txt_gama_c.Text);
                sw.WriteLine("P = {0} ", txt_P.Text);
                sw.WriteLine("phi = {0} ", txt_phi.Text);
                sw.WriteLine("mu = {0} ", txt_mu.Text);
                sw.WriteLine("f_ck = {0} ", txt_f_ck.Text);
                sw.WriteLine("f_y = {0} ", txt_f_y.Text);
                sw.WriteLine("c = {0} ", txt_cb.Text);
                sw.WriteLine("t = {0} ", txt_t.Text);
                sw.WriteLine("m = {0} ", txt_m.Text);
                sw.WriteLine("Kc = {0} ", txt_Kc.Text);
                sw.WriteLine("jc = {0} ", txt_jc.Text);
                sw.WriteLine("Rc = {0} ", txt_Rc.Text);
                sw.WriteLine("th3 = {0} ", txt_th3.Text);
                sw.WriteLine("l = {0} ", txt_l.Text);
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Calculate_Program(string file_name)
        {

            #region Local Variables
            double rad, Ka, Kp, H, a, _B, _val1, _val2, w1, lev_arm1, moment1, w2, lev_arm2, moment2;
            double w3, lev_arm3, moment3, w4, lev_arm4, moment4, Wv, moment, Rm, Pa, Ps, Ph, om;
            double p2, P1_, P2_;
            double safety_fact, sliding_fac, M, x, e, p1;
            double earth_load, self_load, p, M1, d_eff, V1, _p, tau_c, d_req, _d_eff, tau_v, dia1;
            double sigma_st, Ast1, spacing, Ld, Lo, Ast2, S2, pp1, pp2, pp3, p_, ratio;
            double V, spacing_rebars, S3, area, percent, Vc, x1, omega1, y1, Asv, Sv, D, self_weight;
            double upw_int_D, net_int_D, net_int_E, actual_ast, BM, net_upw_press, spacing_1, spacing_2;
            double spacing_bottom_steel, spacing_top_steel, Asd, shear_force_D, actual_ast1, x2;
            double intensity_below_D, intensity_below_E, y2, actual_SF, z, ph, depth, total_depth;
            double provide_total_depth, available_depth, shear_force_V, reinfocement_p, upto_point;
            double counterfort_spaced, H1, dnw_press_B, dnw_press_C, at_C, at_B, _pre_h, pressure_int_h;
            double shear_force_F, keep_D, deg, min, theta, sin_theta, cos_theta, F1C1, depth_FG;
            double eff_dep, A_phi, nos_bars, d_dash, SF, root_H, force, steel_area_reg;
            double force_at_C, force_at_B, steel_area_C, steel_area_B, spacing_ties_B, spacing_ties_C;
            double dnw_wgt, transmitted_D, transmitted_E, total_upw_frc, net_shear_force, Vs, Pp, area_1;
            //val = MyList.StringToDouble(val.ToString("0.00"), 0.0);
            int counter1, counter2;
            double _b, _BB, _d, _th2, _th1, force_D1C1;
           

            _b = 0;
            _BB = 0;
            _d = 0;

            _th1 = th1;
            _th2 = th2;

            bool safe1, safe2;
            safe1 = true;
            safe2 = false;
            om = 0.0;
            Ph = 0.0;
            counter1 = 0;
            counter2 = 0;
            #endregion

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            #region TechSOFT Banner
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21              *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*           DESIGN  OF RCC ABUTMENT           *");
            sw.WriteLine("\t\t*        AS COUNTERFORT RETAINING  WALL       *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion


            try
            {
                //sw.WriteLine("DESIGN OF RCC ABUTMENT");
                //sw.WriteLine("----------------------");
                sw.WriteLine();
                sw.WriteLine();

                #region USER Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Height of Wall above ground Level [h1] = {0} m.", txt_h1.Text);
                sw.WriteLine("Foundation depth below ground Level [h2] = {0} m.", txt_h2.Text);
                sw.WriteLine("Thickness of stem of Wall [th1] = {0} m.", txt_th1.Text);
                sw.WriteLine("Thickness of base Slab [th2] = {0} m.", txt_th2.Text);
                sw.WriteLine("Safe bearing capacity of soil [q0] = {0} kN/sq.m.", txt_q0.Text);
                sw.WriteLine("Unit weight of Soil [γ_s] = {0} kN/cu.m.", txt_gama_s.Text);
                sw.WriteLine("Unit weight of Concrete [γ_c] = {0} kN/cu.m.", txt_gama_c.Text);
                sw.WriteLine("Surcharge [P] = {0} kN/m.", txt_P.Text);
                sw.WriteLine("Angle of internal friction of soil [φ] = {0}° ", txt_phi.Text);
                sw.WriteLine("Coefficient of friction between Concrete and Soil [µ] = {0}° ", txt_mu.Text);
                sw.WriteLine("Concrete Grade [f_ck] = {0}", txt_f_ck.Text);
                sw.WriteLine("Steel Grade [f_y] = {0}", txt_f_y.Text);
                sw.WriteLine("σ_cbc [c] = {0} N/sq.mm", txt_cb.Text);
                sw.WriteLine("σ_st [t] = {0} N/sq.mm", txt_t.Text);
                sw.WriteLine("Moduler Ratio [m] = {0}", txt_m.Text);
                sw.WriteLine("Kc = {0}", txt_Kc.Text);
                sw.WriteLine("jc = {0}", txt_jc.Text);
                sw.WriteLine("Rc = {0}", txt_Rc.Text);
                sw.WriteLine("Thickness of Counterfot wall [th3] = {0} m", txt_th3.Text);
                //sw.WriteLine("Clear spacing between Counterfort walls [l] = {0} m", txt_l.Text);

                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                list_pressure.Clear();
                list_rebar.Clear();
                list_rebar_drawing.Clear();
                list_dims.Clear();
                #region STEP 1

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Design Constants");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Ka = (1-Sin φ)/(1+Sin φ)");
                rad = Math.PI / 180;

                Ka = (1 - Math.Sin(phi * rad)) / (1 + Math.Sin(phi * rad));
                Kp = (1 / Ka);
                Ka = MyList.StringToDouble(Ka.ToString("0.0000"), 0.0);
                Kp = MyList.StringToDouble(Kp.ToString("0.000"), 0.0);


                sw.WriteLine("   = (1-Sin {0})/(1+Sin {0})", phi);
                sw.WriteLine("   = (1-{0})/(1+{0})", Math.Sin(phi * rad));
                sw.WriteLine("   = {0}", Ka);
                sw.WriteLine();
                sw.WriteLine("Kp = (1/Ka) = (1/{0}) = {1}", Ka, Kp);
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 2
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Dimentions of Various Parts");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Let us assume Base width = B");
                sw.WriteLine("The Ratio of Length of Toe slab to the Base");
                sw.WriteLine();
                sw.WriteLine("a = (1 - (q0/(2.2 * γ * H)))");
                //sw.WriteLine();
                H = h1 + h2;
                a = (1 - (q0 / (2.2 * gama_s * H)));
                a = MyList.StringToDouble(a.ToString("0.00"), 0.0);
                H = MyList.StringToDouble(H.ToString("0.000"), 0.0);
                sw.WriteLine("  = (1 - ({0}/(2.2 * {1} * {2})))", q0, gama_s, H);
                sw.WriteLine("  = {0} ", a);
                sw.WriteLine();
                sw.WriteLine("H = h1 + h2 = {0} + {1} = {2} m", h1, h2, H);
                sw.WriteLine();

                //Ka = (1 - Math.Sin(phi * rad)) / (1 + Math.Sin(phi * rad));
                B = 0.95 * H * Math.Sqrt((((1 - Math.Sin(phi * rad)) / (1 + Math.Sin(phi * rad))) / ((1 - a) * (1 + 3 * a))));
                B = MyList.StringToDouble(B.ToString("0.000"), 0.0);
                sw.WriteLine("B = 0.95 * H * √((Ka / ((1 - a) * (1 + 3 * a))))");
                sw.WriteLine("  = 0.95 * {0:f3} * √(({1} / ((1 - {2}) * (1 + 3 * {2}))))", H, Ka, a);
                sw.WriteLine("  = {0}", B);
                sw.WriteLine();
                sw.WriteLine("Taking minimum from the range of 0.5* H to 0.6*H");

                _B = 0.5 * H;
                B = _B;
                B = MyList.StringToDouble(B.ToString("0.000"), 0.0);
                sw.WriteLine("    B = 0.5*H = 0.5 * {0} = {1} m", H, _B);
                sw.WriteLine();
                b = a * _B;
                b = MyList.StringToDouble(b.ToString("0.000"), 0.0);
                sw.WriteLine("Length of Toe Slab = b = a * B");
                sw.WriteLine("                   = {0} * {1}", a, _B);
                sw.WriteLine("                   = {0} m", b);
                b = b * 10.0;
                b = (int)b;
                b = b / 10.0;
                _b = b;
                sw.WriteLine("                   ≈ {0} m", b);
                sw.WriteLine();
                d = _B - b - th1;
                _d = d;

                //d = 2.5;
                d = MyList.StringToDouble(d.ToString("0.000"), 0.0);
                sw.WriteLine("Length of Heel Slab = d = B - b - th1");
                sw.WriteLine("                    = {0} - {1} - {2}", B, b, th1);
                sw.WriteLine("                    = {0} m", d);
                sw.WriteLine();
                sw.WriteLine("Clear spacing of Counterfort walls = l");
                l = 3.5 * Math.Pow((H / gama_s), 0.25);
                l = MyList.StringToDouble(l.ToString("0.000"), 0.0);
                sw.WriteLine("      = 3.5 * (H/γ)^0.25");
                sw.WriteLine("      = 3.5 * ({0}/{1})^0.25", H, gama_s);
                sw.WriteLine("      = {0} m", l);
                //l -= 0.5;
                _val1 = (int)l;
                _val2 = _val1 + 1;
                _val1 = _val1 + 0.5;
                if (l > _val1)
                    l = _val2;
                else
                    l = _val1;
                sw.WriteLine("      = {0} m", l);
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region STEP 3
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Stability of Wall");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("W1 = Weight of Stem of Wall");
                sw.WriteLine("W2 = Weight of Base Slab");
                sw.WriteLine("W3 = Weight of Soil on Heel Slab");
                sw.WriteLine("W4 = Weight of Surcharge");
                sw.WriteLine();
                sw.WriteLine();
                #region safe 1
            _safe1:

                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "S.No.",
                    "Force/Weight",
                    "Lever Arm",
                    "Moment about Toe");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    "(kN)    ",
                    "(m)   ",
                    "(kN-m)    ");
                sw.WriteLine();
                w1 = th1 * (h1 + h2 - th2) * 1 * gama_c;
                w1 = MyList.StringToDouble(w1.ToString("0.000"), 0.0);
                lev_arm1 = b + (th1 / 2);
                lev_arm1 = MyList.StringToDouble(lev_arm1.ToString("0.000"), 0.0);
                moment1 = w1 * lev_arm1;
                moment1 = MyList.StringToDouble(moment1.ToString("0.000"), 0.0);

                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "1.",
                    "W1    th1 *(h1 + h2-th2) * 1 * γ_c",
                    "  b + (th1/2)",
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} * ({1} + {2}-{3}) * 1 * {4}", th1, h1, h2, th2, gama_c),
                    string.Format("= {0} + ({1}/2)", b, th1),
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} ", w1),
                    string.Format("= {0}", lev_arm1),
                    moment1);
                sw.WriteLine();

                _B = B;
                w2 = th2 * _B * 1 * gama_c;
                lev_arm2 = _B / 2;
                moment2 = w2 * lev_arm2;
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "2.",
                    "W2    th2 * B * 1 * γ_c",
                    "  B / 2",
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} * {1} * 1 * {2}", th2, _B, gama_c),
                    string.Format("= {0} / 2", _B),
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} ", w2),
                    string.Format("= {0}", lev_arm2),
                    moment2);
                sw.WriteLine();


                w3 = d * (h1 + h2 - th2) * 1 * gama_s;
                lev_arm3 = b + th1 + (d / 2);
                moment3 = w3 * lev_arm3;
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "3.",
                    "W3    d * (h1+h2-th2) * 1 * γ_s",
                    "  b+th1+(d/2)",
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} * ({1}+{2}-{3}) * 1 * {4} ", d, h1, h2, th2, gama_s),
                    string.Format("= {0}+ {1} + ({2}/2)", b, th1, d),
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} ", w3),
                    string.Format("= {0}", lev_arm3),
                    moment3);
                sw.WriteLine();

                w4 = P * d;
                lev_arm4 = b + th1 + (d / 2);
                moment4 = w4 * lev_arm4;
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "3.",
                    "W4    P * d",
                    "  b+th1+(d/2)",
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} * {1} ", P, d),
                    string.Format("= {0}+ {1} + ({2}/2)", b, th1, d),
                    "");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("    = {0} ", w4),
                    string.Format("= {0}", lev_arm4),
                    moment4);
                sw.WriteLine();

                Wv = w1 + w2 + w3 + w4;
                moment = moment1 + moment2 + moment3 + moment4;
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    "---------------------",
                    "",
                    "----------------");
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                    "",
                    string.Format("∑Wv = {0} ", Wv),
                    "",
                    moment);
                sw.WriteLine("{0,-5:f0} {1,-42:f3} {2,-22:f3} {3,20:f3}",
                   "",
                   "---------------------",
                   "",
                   "----------------");
                sw.WriteLine();
                sw.WriteLine();
                Rm = moment;
                Rm = MyList.StringToDouble(Rm.ToString("0.000"), 0.0);

                sw.WriteLine("Resisting Moment = Rm = {0:f3}", Rm);
                #endregion

                if (safe1)
                {
                    #region safe 2
                    sw.WriteLine();
                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                        "Horizontal Forces",
                        "Lever Arm",
                        "Moment about Toe");
                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                        "(kN)",
                        "(m)",
                        "(kN-m)");
                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                        "Pa   = Ka * γ * H * H/2.0",
                        "   H/3",
                        "");
                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                        string.Format("     = {0} * {1} * {2} * {2}/2.0", Ka, gama_s, H),
                        string.Format(" = {0}/3", H),
                        "");

                    Pa = (1 / Kp) * gama_s * H * H / 2.0;
                    Pa = MyList.StringToDouble(Pa.ToString("0.000"), 0.0);
                    lev_arm1 = H / 3;
                    lev_arm1 = MyList.StringToDouble(lev_arm1.ToString("0.000"), 0.0);
                    moment1 = Pa * lev_arm1;
                    moment1 = MyList.StringToDouble(moment1.ToString("0.000"), 0.0);

                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                       string.Format("     = {0:f3}", Pa),
                       string.Format(" = {0:f3}", lev_arm1),
                       moment1);

                    sw.WriteLine();

                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                       "Ps   = Ka * (P * d)",
                       "   H/2",
                       "");
                    sw.WriteLine("{0,-40} {1,-20} {2,20}",
                       string.Format("     = {0} * ({1} * {2})", Ka, P, d),
                       string.Format(" = {0}/2", H),
                       "");

                    Ps = Ka * P * d;
                    Ps = MyList.StringToDouble(Ps.ToString("0.000"), 0.0);
                    lev_arm2 = H / 2;
                    lev_arm2 = MyList.StringToDouble(lev_arm2.ToString("0.000"), 0.0);
                    moment2 = Ps * lev_arm2;
                    moment2 = MyList.StringToDouble(moment2.ToString("0.000"), 0.0);

                    sw.WriteLine("{0,-40} {1,-20} {2,20:f3}",
                       string.Format("     = {0:f3}", Ps),
                       string.Format(" = {0:f3}", lev_arm2),
                       moment2);


                    sw.WriteLine();
                    Ph = Pa + Ps;
                    Ph = MyList.StringToDouble(Ph.ToString("0.000"), 0.0);
                    om = moment1 + moment2;
                    om = MyList.StringToDouble(om.ToString("0.000"), 0.0);
                    sw.WriteLine("{0,-40} {1,-20} {2,20:f3}",
                       "---------------------",
                       "",
                       "------------------");

                    sw.WriteLine("{0,-40} {1,-20} {2,20:f3}",
                       string.Format("∑Ph  = {0:f3}", Ph),
                       "",
                       string.Format("{0}", om));

                    sw.WriteLine("{0,-40} {1,-20} {2,20:f3}",
                       "---------------------",
                       "",
                       "------------------");
                    sw.WriteLine();
                    sw.WriteLine("Over Turning Moment = om = {0}", om);
                    sw.WriteLine();

                    safety_fact = Rm / om;
                    safety_fact = MyList.StringToDouble(safety_fact.ToString("0.000"), 0.0);
                    sw.WriteLine("Factor of Safety against overturning = Rm/om");
                    sw.WriteLine("                                     = {0}/{1}", Rm, om);
                    if (safety_fact < 2.0)
                    {
                        sw.WriteLine("                                     = {0} < 2.0 , Hence Unsafe", safety_fact);
                    }
                    else
                    {
                        sw.WriteLine("                                     = {0} > 2.0 , Hence Safe", safety_fact);
                    }
                    sliding_fac = mu * Wv / Ph;
                    sliding_fac = MyList.StringToDouble(sliding_fac.ToString("0.000"), 0.0);
                    sw.WriteLine();

                    sw.WriteLine("Factor of safety against Sliding = µ * ∑Wv / ∑Ph");
                    sw.WriteLine("                                 = {0} * {1} / {2}", mu, Wv, Ph);
                    if (sliding_fac < 1.0)
                    {
                        sw.WriteLine("                                 = {0} < 1.0 , Hence Unsafe", sliding_fac);
                        sw.WriteLine("A Shear key is to be provided under the base width of base may also be increased.");
                    }
                    else
                        sw.WriteLine("                                 = {0} > 1.0 , Hence Safe", sliding_fac);
                    #endregion
                }

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Pressure Distribution");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                M = Rm - om;
                M = MyList.StringToDouble(M.ToString("0.000"), 0.0);
                sw.WriteLine("Net Moment = M = Rm - om");
                sw.WriteLine("           = {0} - {1}", Rm, om);
                sw.WriteLine("           = {0} kN-m", M);
                sw.WriteLine();
                sw.WriteLine("Distance x from Toe , to the point of application of the resultant.");
                x = M / Wv;
                x = MyList.StringToDouble(x.ToString("0.00"), 0.0);
                sw.WriteLine("      x = M/W");
                sw.WriteLine("        = {0}/{1}", M, Wv);
                sw.WriteLine("        = {0} m", x);

                e = (B / 2) - x;
                e = MyList.StringToDouble(e.ToString("0.000"), 0.0);
                sw.WriteLine();
                sw.WriteLine(" Eccentricity = e = (B/2)-x");
                sw.WriteLine("                  = ({0}/2) - {1}", B, x);
                sw.WriteLine("                  = {0} m", e);
                _val1 = (B / 6.0);
                _val1 = MyList.StringToDouble(_val1.ToString("0.000"), 0.0);

                if (e < _val1)
                {
                    sw.WriteLine(" B/6 = {0}/6 = {1:f3} m > {2} m, No tension will be developed.", B, (B / 6.0), e);
                    //sw.WriteLine("         e <= B/6 = {0}/6 = {1:f3} m , No tension will be developed.", B, (B / 6.0));
                }
                else if (e > _val1)
                {
                    sw.WriteLine(" B/6 = {0}/6 = {1:f3} m < {2} m, there will be tension in the base.", B, (B / 6.0), e);
                    //sw.WriteLine("         e > B/6 = {0}/6 = {1:f3} m , there will be tension in the base.", B, (B / 6.0));
                }
                else
                {
                    sw.WriteLine(" B/6 = {0}/6 = {1:f3} m = e = {2} m, No tension will be developed.", B, (B / 6.0), e);
                    //sw.WriteLine("         e > B/6 = {0}/6 = {1:f3} m , there will be tension in the base.", B, (B / 6.0));
                }
                p1 = (Wv / B) * (1 + (6 * e / B));
                p1 = MyList.StringToDouble(p1.ToString("0.000"), 0.0);
                sw.WriteLine();
                sw.WriteLine("Pressure under the Toe = P1 = (∑Wv/B)*(1+(6e/B))");
                sw.WriteLine("                       = ({0}/{1})*(1+6*{2}/{1}))", Wv, B, e);
                if (p1 > q0)
                {
                    sw.WriteLine("                       = {0} kN/sq.m > {1} (Safe bearing capacity of soil = q0), Hence Unsafe", p1, q0);
                    safe1 = false;
                }
                else
                {
                    safe1 = true;
                    sw.WriteLine("                       = {0} kN/sq.m < {1} (Safe bearing capacity of soil = q0), Hence Safe", p1, q0);
                    list_pressure.Add(string.Format("_P1=P1 = {0} kN/sq.m.", p1));
                }
                sw.WriteLine();
                sw.WriteLine();
                if (safe1 == false)
                {
                    counter1++;
                    sw.WriteLine("------------------------------------------------------------");
                    if (counter1 <= 5)
                    {
                        sw.WriteLine("Let us try with values");
                        sw.WriteLine("width of Toe slab = b = b+0.1 = {0} + 0.1 = {1} m", b, (b + 0.1));
                        b = b + 0.1;
                        sw.WriteLine("width of Base Slab = B = B+0.1 = {0} + 0.1 = {1} m", B, (B + 0.1));
                        B = B + 0.1;
                        sw.WriteLine("width of Heel Slab = d = {0} m", d);
                        sw.WriteLine();
                        sw.WriteLine("Make Toe of slab = b = {0} m", b);
                    }
                    else if (counter1 > 5 && counter1 <= 11)
                    {
                        //b = _b;
                        //B = _BB;
                        sw.WriteLine("Let us try with values");
                        sw.WriteLine("width of Toe slab = b = {0} m", b);
                        sw.WriteLine("width of Base Slab = B = {0} m", B);
                        sw.WriteLine("width of Heel Slab = d = d + 0.1 = {0} + 0.1 = {1} m", d, (d + 0.1));
                        d = d + 0.1;
                        sw.WriteLine();
                        sw.WriteLine("Make Heel of Slab = d = {0} m", d);
                        if (counter1 == 11)
                        {
                            counter1 = 0;
                            counter2++;
                            if (counter2 <= 5)
                            {
                                sw.WriteLine("Thickness of stem of Wall = th2 = th2 + 0.1 = {0} + 0.1 = {1}", th2, (th2 + 0.1));
                                th2 = th2 + 0.1;
                                b = _b;
                                B = _B;
                            }
                            else
                            {
                                sw.WriteLine("Thickness of stem of Wall = th1 = th1 + 0.1 = {0} + 0.1 = {1}", th2, (th2 + 0.1));
                                th1 = th1 + 0.1;
                                b = _b;
                                B = _B;
                            }
                        }
                    }
                    sw.WriteLine("------------------------------------------------------------");
                    goto _safe1;
                }

                sw.WriteLine();
                p2 = (Wv / B) * (1 - (6 * e / B));
                p2 = MyList.StringToDouble(p2.ToString("0.000"), 0.0);
                sw.WriteLine("Pressure under the Heel = P2 = ∑W/B(1-(6*e/B))");
                sw.WriteLine("                        = {0}/{1}(1-(6*{2}/{1}))", Wv, B, e);
                sw.WriteLine("                        = {0} kN/sq.m", p2);
                list_pressure.Add(string.Format("_P2=P2 = {0} kN/sq.m.", p2));

                sw.WriteLine();

                P1_ = ((p1 - p2) / B) * (d + th1) + p2;
                P1_ = MyList.StringToDouble(P1_.ToString("0.000"), 0.0);
                sw.WriteLine("Pressure at toe side face of vertical stem of wall = P1'");
                sw.WriteLine("        = P1' = ((p1 - p2) / B) * (d + th1) + p2 ");
                sw.WriteLine("        = (({0} - {1}) / {2}) * ({3} + {4}) + {1} ", p1, p2, B, d, th1);
                sw.WriteLine("        = {0} kN/sq.m", P1_);
                list_pressure.Add(string.Format("_P1'=P1' = {0} kN/sq.m.", P1_));
                sw.WriteLine();

                P2_ = ((p1 - p2) / B) * d + p2;
                P2_ = MyList.StringToDouble(P2_.ToString("0.000"), 0.0);
                sw.WriteLine("Pressure at heel side face of vertical stem of wall = P2'");
                sw.WriteLine("        = P2' = ((p1 - p2) / B) * d + p2 ");
                sw.WriteLine("        = (({0} - {1}) / {2}) * {3} + {1} ", p1, p2, B, d);
                sw.WriteLine("        = {0} kN/sq.m", P2_);
                list_pressure.Add(string.Format("_P2'=P2' = {0} kN/sq.m.", P2_));
                sw.WriteLine();
                sw.WriteLine();


                #endregion

                #region STEP 4
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : DESIGN OF HEEL SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();



                sw.WriteLine("Clear spacing between counterforts = l = 3.0 m");
                sw.WriteLine("The pressure diagram is shown in separate tab of Input data dialog box.");
                sw.WriteLine("Consider a strip, 1 m wide, at th outer edge 'c' of Heel slab.");
                sw.WriteLine();

                earth_load = (h1 + h2 - th2) * gama_s;
                earth_load = MyList.StringToDouble(earth_load.ToString("0.000"), 0.0);
                sw.WriteLine("Downward load due to weight of earth = pp1 = (h1+h2-th2)*γ_s");
                sw.WriteLine("                                     = ({0}+{1}-{2})*{3}", h1, h2, th2, gama_s);
                sw.WriteLine("                                     = {0} kN/sq.m", earth_load);
                sw.WriteLine();
                sw.WriteLine("Downward load due to surcharge = pp2  = {0} kN/sq.m", P);
                sw.WriteLine();
                self_load = th2 * gama_c;
                self_load = MyList.StringToDouble(self_load.ToString("0.000"), 0.0);
                sw.WriteLine("Downward load due to self weight of heel slab = pp3 = th2 * γ_c");
                sw.WriteLine("                                              = {0}* {1}", th2, gama_c);
                sw.WriteLine("                                              = {0} kN/sq.m", self_load);
                p = earth_load + P + self_load - p2;
                sw.WriteLine();
                sw.WriteLine("So, the net downward intensity of load = p = pp1 + pp2 + pp3 - p2");
                sw.WriteLine("                                       = {0} + {1} + {2} - {3}", earth_load, P, self_load, p2);
                sw.WriteLine("                                       = {0} kN/sq.m", p);
                sw.WriteLine();
                sw.WriteLine("Maximum negative bending moment in heel slab at Counterforts, ");
                M1 = p * l * l / 12;
                M1 = MyList.StringToDouble(M1.ToString("0.000"), 0.0);
                sw.WriteLine("        = M1 = p * l * l /12");
                sw.WriteLine("        = {0} * {1} * {1} /12", p, l);
                sw.WriteLine("        = {0} kN-m", M1);
                sw.WriteLine("        = {0} * 10^6 N-mm", M1);
                sw.WriteLine();

                d_eff = Math.Sqrt(M1 * 10E5 / (1000 * Rc));
                d_eff = MyList.StringToDouble(d_eff.ToString("0"), 0.0);
                sw.WriteLine("Effective depth = d = √(M1/(1000*Rc))");
                sw.WriteLine("                = √({0}*10^6/(1000*{1}))", M1, Rc);
                sw.WriteLine("                = {0} mm", d_eff);
                sw.WriteLine();
                V1 = p * l / 2.0;
                V1 = MyList.StringToDouble(V1.ToString("0"), 0.0);
                sw.WriteLine("Shear Force = V1 = p*l/2");
                sw.WriteLine("            = {0}*{1}/2", p, l);
                sw.WriteLine("            = {0} kN", V1);
                sw.WriteLine();
                _p = 0.72;
                tau_c = 0.33;
                sw.WriteLine("For a balanced section having p = 0.72%, τ_c = 0.33 N/sq.mm");
                d_req = (V1 * 1000) / (tau_c * 1000);
                d_req = MyList.StringToDouble(d_req.ToString("0"), 0.0);
                sw.WriteLine("Depth required from shear point of view = V1*1000/τ_c*1000");
                sw.WriteLine("                                        = {0}*1000/{1}*1000", V1, tau_c);

                _val1 = 1.5 * d_eff;

                if (d_req > _val1)
                {
                    sw.WriteLine("                                        = {0} mm, This is Excessive.", d_req);
                    _val2 = d_req - d_eff;
                    _val2 = _val2 / 2;
                    _val1 = d_eff + _val2;
                    _d_eff = d_eff;
                    d_eff = _val1;

                }
                else
                    sw.WriteLine("                                        = {0} mm, OK.", d_req);


                d_eff = (int)(d_eff / 100.0);
                d_eff = d_eff + 1;
                d_eff = d_eff * 100.0;
                sw.WriteLine();
                sw.WriteLine("Provide overall thickness of Heel Slab = {0} mm", d_eff);
                sw.WriteLine("Considering 60 mm effective depth, d = {0} - 60 = {1} mm", d_eff, (d_eff - 60));
                sw.WriteLine();
                d_eff = d_eff - 60;
                tau_v = (V1 * 1000) / (1000 * d_eff);
                tau_v = MyList.StringToDouble(tau_v.ToString("0.000"), 0.0);
                sw.WriteLine("     τ_v = V1*1000/(1000*d)");
                sw.WriteLine("         = {0}*1000/(1000*{1})", V1, d_eff);
                if (tau_v > tau_c)
                {
                    sw.WriteLine("         = {0} N/sq.mm > {1} N/sq.mm", tau_v, tau_c);
                }
                else
                {
                    sw.WriteLine("         = {0} N/sq.mm < {1} N/sq.mm", tau_v, tau_c);
                }
                sw.WriteLine();
                sw.WriteLine("Hence shear reinforcements will be neccessary.");
                sw.WriteLine();
                sw.WriteLine("Supports are the Counterfort walls which are 3 m c/c");
                sw.WriteLine("Area of steel required at supports = Ast1");
                sw.WriteLine("      = Ast1 = M1/(σ_st*jc*d)");
                sigma_st = 230;
                Ast1 = (M1 * 10e5) / (sigma_st * jc * d_eff);
                Ast1 = MyList.StringToDouble(Ast1.ToString("0"), 0.0);
                sw.WriteLine("             = {0}/({1}*{2}*{3})", M1, sigma_st, jc, d_eff);
                sw.WriteLine("             = {0} sq.mm", Ast1);
                dia1 = 12.0;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Using 12 mm Φ bar");
                spacing = 1000.0 * (Math.PI * dia1 * dia1 / 4) / Ast1;
                spacing = MyList.StringToDouble(spacing.ToString("0"), 0.0);
                sw.WriteLine("Spacing = 1000*(π*12*12/4)/Ast1");
                sw.WriteLine("        = 1000*(π*12*12/4)/{0}", Ast1);
                sw.WriteLine("        = {0} mm", spacing);

                //if (spacing > 100)
                //{
                spacing = (int)(spacing / 10);
                //spacing += 1;
                spacing = spacing * 10;
                sw.WriteLine("        ≈ {0} mm", spacing);
                //}
                sw.WriteLine();
                dia1 = 12.0;
                Ast1 = (1000 * Math.PI * dia1 * dia1) / (4.0 * spacing);
                Ast1 = MyList.StringToDouble(Ast1.ToString("0"), 0.0);

                sw.WriteLine("Provide 12 mm bars at {0} c/c Ast1 = (1000*π*{1}*{1})/(4*{0})", spacing, dia1);
                list_rebar.Add(string.Format("Top Rebars                                 {0}mm dia. @ {1}mm c/c.   Marked as [1] in the drawing.", dia1, spacing));
                list_rebar_drawing.Add(string.Format("_01={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing));
                
                
                
                sw.WriteLine("                                   = {0} sq.mm", Ast1);
                sw.WriteLine();
                sw.WriteLine("Check the reinforcements for development length of the point of");
                sw.WriteLine("contraflexure, to satisfy the criterion = M1/V1 + Lo > Ld");
                sw.WriteLine();
                sw.WriteLine("For a fixed beam or slab carrying UDL, the point of contraflexure is situated ");
                sw.WriteLine("at a distance of x = 0.211 * d, when d = distance between twoadjacent ");
                sw.WriteLine("Counterforts. In our care the slab is continuous, but we will assume the ");
                sw.WriteLine("same position for point of contraflexure, ie, at x = 0.211*3.0 = 0.63 m ");
                sw.WriteLine("from the face of a counterfort wall.");
                sw.WriteLine();
                x = 0.211 * 3.0;
                x = MyList.StringToDouble(x.ToString("0.00"), 0.0);
                sw.WriteLine("Shear Force at this point");
                sw.WriteLine("  = V = p*(L/2)*((l/2)-x)/(L/2)");
                V = p * ((l / 2) - x);
                V = MyList.StringToDouble(V.ToString("0.000"), 0.0);
                sw.WriteLine("  = p*((l/2)-x)");
                sw.WriteLine("  = {0}*(({1}/2)-{2})", p, l, x);
                sw.WriteLine("  = {0} kN", V);
                sw.WriteLine();
                sw.WriteLine("Assuming that all the bars are available at the point of contraflexure,");
                M = sigma_st * Ast1 * jc * d_eff;
                M = M / 10E7;
                M = MyList.StringToDouble(M.ToString("0.000"), 0.0);
                sw.WriteLine();
                sw.WriteLine("M = σ_st * Ast * jc * d");
                sw.WriteLine("  = {0} * {1} * {2} * {3}", sigma_st, Ast1, jc, d_eff);
                sw.WriteLine("  = {0} * 10^8 N-mm", M);
                sw.WriteLine();
                sw.WriteLine("Lo = 12 * dia or d which ever is greater");
                Lo = ((12 * dia1) > d_eff) ? (12 * dia1) : d_eff;
                sw.WriteLine("Lo = 12 * {0} or {1} = {2} or {1} = {3}", dia1, d_eff, (12 * dia1), Lo);
                Ld = 45 * dia1;
                sw.WriteLine("Ld = 45*dia = 45 * {0} = {1} mm", dia1, Ld);
                _val1 = ((M * 10E7) / (V * 1000)) + Lo;
                sw.WriteLine();
                sw.WriteLine("(M/V)+Lo = ({0}*10^8)/{1}*1000)+ {2}", M, V, Lo);
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                if (_val1 > Ld)
                {
                    sw.WriteLine("         = {0} mm > Ld = {1} mm, Hence Safe.", _val1, Ld);
                }
                else
                {
                    sw.WriteLine("         = {0} mm < Ld = {1} mm, Hence Unsafe.", _val1, Ld);
                }
                sw.WriteLine();
                sw.WriteLine("Continue these bars for a length Lo = d = {0} mm beyond the point", Lo);
                sw.WriteLine("of contraflexure. After this length curtail half of the bars and ");
                sw.WriteLine("continue the remaining half throughout the length. At the point of");
                _val2 = (x * 1000) + Lo;
                _val2 = MyList.StringToDouble(_val2.ToString("0.000"), 0.0);
                sw.WriteLine("curtailment length of each bar available = x+d");
                sw.WriteLine("                                         = {0}*1000+{1}", x, d_eff);
                sw.WriteLine("                                         = {0} + {1}", (x * 1000), d_eff);
                if (_val2 > Ld)
                {
                    sw.WriteLine("                                         = {0} mm > Ld = {1} mm", _val2, Ld);
                }
                else
                {
                    sw.WriteLine("                                         = {0} mm < Ld = {1} mm", _val2, Ld);
                }

                //sw.WriteLine("                                         = {0} mm > Ld (={1} mm)", (x * 1000 +d_eff), Ld);
                sw.WriteLine();
                sw.WriteLine("These bars will be provided at the top of the heel slab.");
                sw.WriteLine();
                sw.WriteLine("Maximum positive bending moment = p*l*l/16");
                sw.WriteLine("                                = (p*l*l/12) * (3/4)");
                sw.WriteLine("                                = M1 * (3/4)");
                sw.WriteLine();
                Ast2 = Ast1 * (3.0 / 4.0);
                Ast2 = MyList.StringToDouble(Ast2.ToString("0.000"), 0.0);
                sw.WriteLine("Area of Bottom steel = Ast2 = Ast1 * 3/4");
                sw.WriteLine("                            = {0} * 3/4", Ast1);
                sw.WriteLine("                            = {0} sq.mm", Ast2);
                sw.WriteLine();
                S2 = (1000 * Math.PI * dia1 * dia1) / (4 * Ast2);
                S2 = MyList.StringToDouble(S2.ToString("0"), 0.0);
                _val1 = S2;

                S2 = (int)(S2 / 10);
                S2 = (S2 * 10);
                sw.WriteLine("Using 12 mm dia bars spacing S2 = 1000 * π * 12*12/4*Ast2");
                sw.WriteLine("                                = (1000 * π * 12*12)/(4*{0})", Ast2);
                sw.WriteLine("                                = {0} mm", _val1);
                sw.WriteLine("                                ≈ {0} mm", S2);
                list_rebar.Add(string.Format("Bottom Rebars                                  12mm dia. @ {0}mm c/c. Marked as [2] in the drawing.", S2));
                list_rebar_drawing.Add(string.Format("_02={0}mm Ø Rebars @ {1}mm c/c", dia1, S2));

                
                Ast2 = (1000 * Ast1 / 10.0) / S2;
                Ast2 = MyList.StringToDouble(Ast2.ToString("0.000"), 0.0);

                sw.WriteLine();
                sw.WriteLine("Actual Ast2 = (1000 * {0:f2}) / {1}", (Math.PI * 144.0 / 4), S2);
                sw.WriteLine("            = {0} sq.mm", Ast2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Let us check whether these bars fulfil the requirements of development length");
                sw.WriteLine("criterion at the point of contraflexure, inherent in the criterion M/v + Lo > Ld");
                sw.WriteLine();
                sw.WriteLine("V = shear at point of contraflexure");
                sw.WriteLine("  = {0} kN at a distance {1} m from the face of the counterfort wallthe counterfort wall", V, x);
                sw.WriteLine("Assuming th at all the bars are available at point of conraflexure,");
                sw.WriteLine();
                M = sigma_st * Ast2 * jc * d_eff;
                M = M / 10E5;
                M = MyList.StringToDouble(M.ToString("0.0"), 0.0);
                sw.WriteLine("M = σ_st * Ast * jc * d");
                sw.WriteLine("  = {0} * {1} * {2} * {3}", sigma_st, Ast2, jc, d_eff);
                sw.WriteLine("  = {0} * 10^6 N-mm", M);
                sw.WriteLine();
                sw.WriteLine("Lo = {0} mm", Lo);
                sw.WriteLine("Ld = {0} mm", Ld);

                _val1 = ((M * 10e5) / (V * 1000)) + Lo;
                _val1 = MyList.StringToDouble(_val1.ToString("0.0"), 0.0);
                sw.WriteLine("(M/V) + Lo = (({0}*10^6) / ({1}*1000)) + {2}", M, V, Lo);
                if (_val1 > Ld)
                {
                    sw.WriteLine("           = {0} mm > Ld = {1} mm, Hence Safe", _val1, Ld);
                }
                else
                {
                    sw.WriteLine("           = {0} mm < Ld = {1} mm, Hence Unsafe", _val1, Ld);
                }

                sw.WriteLine();
                sw.WriteLine("Thus continue at the bottom bars to a point at a distance Lo = {0} mm ", Lo);
                _val2 = x * 1000 - Lo;
                sw.WriteLine("from the point of contraflexure, ie upto a point at a distance {0}-{1}= {2} mm ", (x * 1000), Lo, _val2);
                sw.WriteLine("from the center of each counterfort wall / support. At this point, ");
                sw.WriteLine("half of the bars may be discontinued. But as the distance is too short, ");
                sw.WriteLine("so it is suggegted to continue these bars upto the center of the ");
                sw.WriteLine("counterfort walls.");
                sw.WriteLine();
                sw.WriteLine("Reinforcement near point B(Face of wall stem on Heel side)");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The centre to centre spacing of reinforcement near \"B\" may be increased,");
                sw.WriteLine("became p decreased due to increase in upward soil reaction. Upward soil reaction ");
                sw.WriteLine("at B = p2' = {0} kN/sq.m as calculated before", P2_);
                sw.WriteLine();
                p_ = (earth_load + P + 0.5 * gama_c) - P2_;
                p_ = MyList.StringToDouble(p_.ToString("0.00"), 0.0);
                sw.WriteLine("Net downward load = p' = pp1 + pp2 + pp3 - {0}", P2_);
                sw.WriteLine("                  = pp1 + pp2 + th3* γ_s - {0}", P2_);
                sw.WriteLine("                  = ({0} + {1} + {2}* {3} - {4})", earth_load, P, 0.5, gama_c, P2_);
                sw.WriteLine("                  = {0} kN/sq.m", p_);
                sw.WriteLine();
                sw.WriteLine("Load intensity at C (outer most point of the heel slab) = {0} kN/sq.m", p);
                ratio = p_ / p;
                ratio = MyList.StringToDouble(ratio.ToString("0.000"), 0.0);
                sw.WriteLine("So, ratio = {0}/{1} = {2}", p_, p, ratio);
                _val1 = 100.0 / ratio;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                spacing_rebars = (int)(_val1 / 10.0);
                spacing_rebars = spacing_rebars * 10;
                sw.WriteLine("Hence, spacing of rebars at top, near counterfort walls / supports");
                sw.WriteLine("      = S1/{0}", ratio);
                sw.WriteLine("      = 100/{0}", ratio);
                sw.WriteLine("      = {0} mm", _val1);
                sw.WriteLine("      ≈ {0} mm ", spacing_rebars);
                list_rebar.Add(string.Format("Top Rebars                                 12mm dia. @ {0}mm c/c. Marked as [3] in the drawing.", spacing_rebars));
                list_rebar_drawing.Add(string.Format("_03={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing_rebars));
                sw.WriteLine();
                S3 = S2 / ratio;
                S3 = MyList.StringToDouble(S3.ToString("0.000"), 0.0);
                sw.WriteLine("spacing of rebars at bottom, at mid span = S3 = S2/{0}", ratio);
                sw.WriteLine("                                              = {0}/{1} mm", S2, ratio);
                sw.WriteLine("                                              = {0} mm", S3);
                S3 = (int)(S3 / 10.0);
                S3 = (S3 * 10.0);
                sw.WriteLine("                                              ≈ {0} mm", S3);
                list_rebar.Add(string.Format("Bottom Rebars                                  12mm dia. @ {0}mm c/c. Marked as [4] in the drawing.", S3));
                list_rebar_drawing.Add(string.Format("_04={0}mm Ø Rebars @ {1}mm c/c", dia1, S3));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Distribution Steel ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                area = (0.12 / 100) * (1000 * (d_eff + 60));
                area = MyList.StringToDouble(area.ToString("0.000"), 0.0);
                sw.WriteLine("Area of Steel = 0.12/100 * (1000*D)");
                sw.WriteLine("              = 0.12/100 * (1000*{0})", (d_eff + 60));
                sw.WriteLine("              = {0} sq. mm", area);
                area_1 = area;
                list_rebar.Add(string.Format("Distribution Rebars                 12mm dia. @ {0}mm c/c. Marked as [5] in the drawing.", spacing_rebars));
                list_rebar_drawing.Add(string.Format("_05={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing_rebars));
                
                sw.WriteLine(" for 12mm dia @ {0} c/c", spacing_rebars);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("SHEAR Reinforcements");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //tau_v = 0.47;
                sw.WriteLine("Shear Stress at the outer edge of heel slab = τ_v = {0} N/sq.mm", tau_v);
                percent = (100 * Ast1) / (1000 * d_eff);

                percent = MyList.StringToDouble(percent.ToString("0.000"), 0.0);

                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
                tau_c = MyList.StringToDouble(tau_c.ToString("0.00"), 0.0);

                sw.WriteLine("Steel rebars provided = 100 * Ast1/(1000*d)");
                sw.WriteLine("                      = (100 * {0})/(1000*{1})", Ast1, d_eff);
                sw.WriteLine("                      = {0} %", percent);
                sw.WriteLine();
                sw.WriteLine("From Table 1 (given at the end of this report)  {0} ", ref_string);
                if (tau_c < tau_v)
                {
                    sw.WriteLine("          τ_c = {0} N/sq.mm < τ_v", tau_c);
                }
                else
                {
                    sw.WriteLine("          τ_c = {0} N/sq.mm > τ_v", tau_c);
                }
                Vc = tau_c * 1000.0 * d_eff;
                Vc = MyList.StringToDouble(Vc.ToString("0.000"), 0.0);
                sw.WriteLine("Vc = τ_c * 1000*d");
                sw.WriteLine("   = {0} * 1000*{1}", tau_c, d_eff);
                sw.WriteLine("   = {0} N", Vc);
                Vc = Vc / 1000.0;
                sw.WriteLine("   = {0}kN", Vc);
                sw.WriteLine();
                sw.WriteLine("If the shear force is {0} kN at a distance x1 from the face of the Counterfort wall", Vc);
                x1 = (l / 2) - ((Vc / V1) * (l / 2.0));
                x1 = MyList.StringToDouble(x1.ToString("0.00"), 0.0);


                sw.WriteLine("  Vc/V1 = ((l/2)-x1)/(l/2)");
                sw.WriteLine("{0}/{1} = ({2}-x1)/{2}", Vc, V1, (l / 2));
                sw.WriteLine("     x1 = {0} m", x1);
                sw.WriteLine();
                sw.WriteLine("Shear stirrups are required up to a distance {0} m on either", x1);
                sw.WriteLine("side of each counterfort wall");
                sw.WriteLine();
                sw.WriteLine("Consider a point B1 at a distance Y1 from the edge of heel slab,");
                sw.WriteLine("such that shear force at the Counterfort is {0} kN", Vc);
                sw.WriteLine();
                sw.WriteLine("Net downward pressure at Edge(at C) = P4 = {0} kN/sq.m", p);
                list_pressure.Add(string.Format("_P4=P4 = {0} kN/sq.m.", p));

                sw.WriteLine("Net downward pressure at face of stem of Retaining wall (at B) = P4' = {0} kN/sq.m", p_);
                list_pressure.Add(string.Format("_P4'=P4' = {0} kN/sq.m.", p_));
                sw.WriteLine("Let Net downward pressure at B1 = ω1 kN/sq.m");
                sw.WriteLine("Shear force of the face of counterforts, at B1 = (3/2)*ω1");
                sw.WriteLine("                                               = 1.5 * ω1");
                sw.WriteLine();
                sw.WriteLine("Therefore, {0} = 1.5*ω", Vc);
                omega1 = Vc / 1.5;
                omega1 = MyList.StringToDouble(omega1.ToString("0.00"), 0.0);
                sw.WriteLine("           ω1 = {0} kN/sq.m", omega1);
                y1 = (p - omega1) / (((p - p_) / 2.0));
                y1 = MyList.StringToDouble(y1.ToString("0.00"), 0.0);
                sw.WriteLine();
                sw.WriteLine("However, at a distance y1 from edge (c)");
                sw.WriteLine("ω1 = P4 - ((P4-P4')/2)*y1");
                sw.WriteLine("y1 = (P4 - ω1) / (((P4 - P4') / 2.0))");
                sw.WriteLine("   = ({0} - {1}) / ((({0} - {2}) / 2.0))", p, omega1, p_);
                //sw.WriteLine();
                //y1 = 1.56;
                area = x1 * y1;
                area = MyList.StringToDouble(area.ToString("0.000"), 0.0);
                sw.WriteLine("   = {0} m", y1);
                sw.WriteLine();
                sw.WriteLine("Hence shear rebars are required in the triangular zone on either side of the");
                sw.WriteLine("counterfort walls. However, let us provide shear stirrups in the rectangular");
                sw.WriteLine("area of x1*y1 = {0} m * {1} m = {2} sq.m", x1, y1, area);
                sw.WriteLine();
                sw.WriteLine("Let us provide 4 legged stirrups of 8 mm dia.");
                Asv = 4.0 * (Math.PI / 4) * 8.0 * 8.0;
                Asv = MyList.StringToDouble(Asv.ToString("0"), 0.0);
                sw.WriteLine("Area = Asv");
                sw.WriteLine("     = 4 * π/4 * 8*8");
                sw.WriteLine("     = {0} sq. mm", Asv);
                sw.WriteLine();
                Sv = (Asv * sigma_st * d_eff) / ((V1 - Vc) * 1000.0);
                Sv = MyList.StringToDouble(Sv.ToString("0"), 0.0);
                sw.WriteLine("Spacing = Sv = (Asv*σ_st*d)/(V1-Vc)*1000");
                sw.WriteLine("        = {0} mm", Sv);
                if (Sv >= 100 && Sv < 150)
                    Sv = 100;
                else if (Sv >= 150 && Sv < 200)
                    Sv = 150;
                else if (Sv >= 200 && Sv < 250)
                    Sv = 200;
                else if (Sv >= 200 && Sv < 250)
                    Sv = 200;
                else
                    Sv = 250;
                sw.WriteLine("        ≈ {0} mm", Sv);

                sw.WriteLine();
                sw.WriteLine("Let us provide {0} mm spacing on either side of each Counterfort wall.", Sv);
                list_rebar.Add(string.Format("Shear Reinforcements                  8mm dia. 4 Legged @ {0}mm c/c. Marked as [6] in the drawing.", Sv));
                list_rebar_drawing.Add(string.Format("_06=8mm Ø 4 lgd ties @ {0}mm c/c", Sv));
                sw.WriteLine();
                #endregion

                #region STEP 5
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : DESIGN OF TOE SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("As the toe slab is also large, provide counterforts over the toe slab");
                sw.WriteLine("also having upto the ground level on the toe side and at {0} m. Clear ", l);
                sw.WriteLine("distance from  face to face the toe slab will thus bend like a continuos slab");
                sw.WriteLine();
                sw.WriteLine("Let us assume the overall thickness & toe slab is equal to same as that");
                D = d_eff + 60;
                sw.WriteLine("of toe slab = D = {0} mm.", D);
                sw.WriteLine();
                sw.WriteLine("Downward weight per unit area, for self weight");
                self_weight = (D / 1000.0) * 1 * 1 * gama_c;
                sw.WriteLine(" = D*1*1*γ_c");

                sw.WriteLine(" = {0} kN/sq.m", self_weight);
                sw.WriteLine();
                sw.WriteLine("Upward pressure intensity at D = P1 = {0} kN/sq.m", p1);
                sw.WriteLine();

                net_int_D = p1 - self_weight;
                list_pressure.Add(string.Format("_P11=P11 = {0} kN/sq.m.", p1));
                
                net_int_D = MyList.StringToDouble(net_int_D.ToString("0.00"), 0.0);
                sw.WriteLine("Net upward pressure intensity at D = {0} - {1} = P3 = {2} kN/sq.m", p1, self_weight, net_int_D);
                list_pressure.Add(string.Format("_P3=P3 = {0} kN/sq.m.", net_int_D));
                net_int_E = P1_ - self_weight;
                list_pressure.Add(string.Format("_P12=P12 = {0} kN/sq.m.", P1_));
                net_int_E = MyList.StringToDouble(net_int_E.ToString("0.00"), 0.0);
                sw.WriteLine();
                sw.WriteLine("Net upward pressure intensity at E = {0} - {1} = P3' = {2} kN/sq.m", P1_, self_weight, net_int_E);
                list_pressure.Add(string.Format("_P3'=P3' = {0} kN/sq.m.", net_int_E));
                sw.WriteLine();

                M1 = net_int_D * l * l / 12;
                M1 = MyList.StringToDouble(M1.ToString("0.00"), 0.0);
                sw.WriteLine("Maximum negative bending moment = M1");
                sw.WriteLine("                                = w*l*l/12");
                sw.WriteLine("                                = {0} kN-m", M1);
                sw.WriteLine("                                = {0} * 10^6 N-mm", M1);
                sw.WriteLine();

                d_eff = Math.Sqrt((M1 * 10e5) / (Rc * 1000));
                d_eff = MyList.StringToDouble(d_eff.ToString("0"), 0.0);
                sw.WriteLine();
                sw.WriteLine("d = √(M1/(Rc*1000)) = 359 mm");
                sw.WriteLine("  = √({0}*10^6/({1}*1000))", M1, Rc);
                sw.WriteLine("  = {0} mm", d_req);
                sw.WriteLine();

                V = net_int_D * (l / 2.0);
                V = MyList.StringToDouble(V.ToString("0.0"), 0.0);
                sw.WriteLine("Shear Force = V");
                sw.WriteLine("            = {0} * (3/2)", net_int_D);
                sw.WriteLine("            = {0} kN", V);
                shear_force_D = V;
                sw.WriteLine();
                tau_c = iApp.Tables.Permissible_Shear_Stress(0.5, con_grade, ref ref_string);
                sw.WriteLine();
                sw.WriteLine("From Table 1 (given at the end of the report) {0}", tau_c, ref_string);
                sw.WriteLine("Taking a permissible stress τ_c = {0} N/sq.mm for p = 0.5%", tau_c);

                d_req = (V * 1000) / (1000 * tau_c);
                d_req = MyList.StringToDouble(d_req.ToString("0"), 0.0);

                sw.WriteLine();
                sw.WriteLine("the depth of slab required from Shear force requirements,");
                sw.WriteLine("     d = V*1000 / (1000*τ_c)");
                sw.WriteLine("       = {0}*1000 / (1000*{1})", V, tau_c);
                sw.WriteLine();
                _val1 = 1.5 * d_eff;
                if (d_req > _val1)
                {
                    sw.WriteLine("       = {0} mm, This is Excessive.", d_req);
                    _val2 = d_req - d_eff;
                    _val2 = _val2 / 2;
                    _val1 = d_eff + _val2;
                    _d_eff = d_eff;
                    d_eff = _val1;

                }
                else
                    sw.WriteLine("                                        = {0} mm, OK.", d_req);

                d_eff = (int)(d_eff / 100.0);
                //d_eff = d_eff + 1;
                d_eff = d_eff * 100.0;
                D = d_eff;
                d_eff = D - 60;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("So let us keep the overall thickness same as heel slab = D = 500 mm");
                sw.WriteLine("and provides shear stirrups to take up excessive shearing stress");
                sw.WriteLine();
                sw.WriteLine("Area of steel at supports/ Counterfort walls, at bottom ");
                Ast1 = (M1 * 10e5) / (sigma_st * jc * d_eff);
                Ast1 = MyList.StringToDouble(Ast1.ToString("0"), 0.0);
                sw.WriteLine("         Ast1 = M1 / (σ_st * jc * d)");
                sw.WriteLine("              = {0} * 10^6 / ({1} * {2} * {3})", M1, sigma_st, jc, d_eff);
                sw.WriteLine("              = {0} sq.mm", Ast1);
                sw.WriteLine();
                spacing = 1000 * Math.PI * 12 * 12 / (4 * Ast1);
                spacing = MyList.StringToDouble(spacing.ToString("0.000"), 0.0);
                sw.WriteLine();
                sw.WriteLine("Using 12 mm  dia bars, spacing = 1000 * π*12*12/(4*Ast1)");
                sw.WriteLine("                               = {0} mm", spacing);
                spacing_1 = spacing;
                spacing = (int)(spacing / 10);
                spacing = (int)(spacing * 10);
                sw.WriteLine("                               ≈ {0} mm", spacing);
                sw.WriteLine();
                actual_ast = (1000.0 * Math.PI * 12.0 * 12.0) / (4.0 * spacing);
                actual_ast = MyList.StringToDouble(actual_ast.ToString("0.00"), 0.0);
                sw.WriteLine();
                sw.WriteLine("Let us provide 12 Φ stirrups @ {0} mm c/c, Actual steel provide", spacing);
                list_rebar.Add(string.Format("Top Rebars                                 12mm dia. @ {0}mm c/c. Marked as [7] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_07=12mm Ø Rebars @ {0}mm c/c", spacing));

                sw.WriteLine("                          = 1000 * π * 12*12/(4*{0})", spacing);
                sw.WriteLine("                          = {0} sq.mm.", actual_ast);
                actual_ast1 = actual_ast;
                sw.WriteLine();
                sw.WriteLine("Let us check the depth from consider of development length,");
                sw.WriteLine("at the point of contraflexure, to fulfil the criterion : (M/V) + Lo >= Ld");
                sw.WriteLine();
                sw.WriteLine("The point of Contraflexure occurs at a distance x = {0} from the face", x);
                sw.WriteLine("of Counterfort wall a support, Hence Shear Force at the Point of Contraflexure is");
                V = net_int_D * ((l / 2) - x);
                V = MyList.StringToDouble(V.ToString("0.00"), 0.0);

                sw.WriteLine();
                sw.WriteLine("V = w*((l/2)-x)");
                sw.WriteLine("  = {0} * ({1}/2 - {2})", net_int_D, l, 2);
                sw.WriteLine("  = {0} kN", V);
                M = sigma_st * actual_ast * jc * d_eff;
                M = M / 10E7;
                M = MyList.StringToDouble(M.ToString("0.00"), 0.0);
                sw.WriteLine();
                sw.WriteLine("M = σ_st * Ast * jc * d");
                sw.WriteLine("  = {0} * {1} * {2} * {3}", sigma_st, actual_ast, jc, d_eff);
                sw.WriteLine("  = {0} * 10^8", M);
                sw.WriteLine();
                Lo = ((d_eff > (12 * 12)) ? d_eff : (12 * 12));
                sw.WriteLine("Lo = 12Φ  or d (which ever is greater) = {0} mm", Lo);
                sw.WriteLine("Ld = 45 * 12 = 540 mm");
                sw.WriteLine();
                _val1 = ((M * 10e7) / (V * 1000)) + Lo;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                sw.WriteLine("M/V + Lo = (({0}*10^8)/({1}*1000)) + {2}", M, V, Lo);
                if (_val1 > Ld)
                {
                    sw.WriteLine("         = {0} mm > Ld", _val1);
                    sw.WriteLine("Continue thease bars at the bottom of the Toe slab, beyond the point ");
                    sw.WriteLine("of Contraflexure by the distance of Lo = {0} mm, ie, the distance of", Lo);
                    sw.WriteLine("x+d = {0} + {1} = {2} mm from the face of counterfort/support", x*1000, Lo, (x*1000+Lo));
                    sw.WriteLine();
                }
                else
                {
                    sw.WriteLine("         = {0} mm < Ld", _val1);
                }
                sw.WriteLine();
                BM = (3.0 / 4.0) * M1;
                BM = MyList.StringToDouble(BM.ToString("0"), 0.0);
                sw.WriteLine("Next, Positive bending Moment = (3/4)*M1");
                sw.WriteLine("                              = (3/4)* {0}*10^6", M1);
                sw.WriteLine("                              = {0} * 10^6 mm", BM);
                sw.WriteLine();
                area = (3.0 / 4.0) * Ast1;
                area = MyList.StringToDouble(area.ToString("0"), 0.0);
                sw.WriteLine("Area of bottom Steel rebars = (3/4)*Ast1");
                sw.WriteLine("                            = (3/4)*{0}", Ast1);
                sw.WriteLine("                            = {0} sq.mm", area);
                sw.WriteLine();
                sw.WriteLine("Area of one 12 mm Φ bars = (π*12*12/4)");
                sw.WriteLine("                         = 113 sq.mm");
                sw.WriteLine();
                spacing = (1000 * 113.0) / area;
                spacing = MyList.StringToDouble(spacing.ToString("0"), 0.0);
                sw.WriteLine("Spacing 0f 12 mm Φ bars = 1000*113/{0}", area);
                sw.WriteLine("                        = {0} mm", spacing);
                spacing_2 = spacing;
                sw.WriteLine();
                spacing = (int)(spacing / 10);
                spacing = spacing * 10;
                sw.WriteLine("Provide spacing @ {0} mm c/c", spacing);
                list_rebar.Add(string.Format("Bottom Rebars                                  12mm dia. @ {0}mm c/c. Marked as [8] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_08={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing));

                sw.WriteLine();
                actual_ast = (1000 * 113.0) / spacing;
                actual_ast = MyList.StringToDouble(actual_ast.ToString("0.000"), 0.0);
                sw.WriteLine("Actual Steel provided = 1000*113/{0}", spacing);
                sw.WriteLine("                      = {0} sq.mm", actual_ast);
                sw.WriteLine();
                sw.WriteLine("This steel has to satisfy M/V+lo >= Ld at the point of Contraflexure");
                sw.WriteLine("where V = Shear Force at the point of Contraflexure = {0} kN", V);
                sw.WriteLine();
                sw.WriteLine("Assuming that all the bars, provided at the top toe are available");
                sw.WriteLine("at the point of Contraflexure.");
                sw.WriteLine();
                M = sigma_st * actual_ast * jc * d_eff;
                M = M / 10E5;
                M = MyList.StringToDouble(M.ToString("0.00"), 0.0);
                sw.WriteLine("M = σ_st * Ast * jc * d");
                sw.WriteLine("  = {0} * {1} * {2} * {3}", sigma_st, actual_ast, jc, d_eff);
                sw.WriteLine("  = {0} * 10^6", M);
                sw.WriteLine();
                sw.WriteLine("Lo = 12Φ or d whichever is more = {0} mm", Lo);
                sw.WriteLine();
                sw.WriteLine("Ld = 45Φ = 45*12 = 540 mm");
                sw.WriteLine();
                _val1 = (M * 10e5) / (V * 1000) + Lo;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                sw.WriteLine("M/V + Lo = ({0}*10^6)/(V*1000) + Lo");
                if (_val1 >= Ld)
                {
                    sw.WriteLine("         = {0} mm >= Ld, OK", _val1);
                }
                else
                {
                    sw.WriteLine("         = {0} mm < Ld", _val1);
                }
                sw.WriteLine();
                sw.WriteLine("The rebars are to be continued for the distance of Lo = 440 mm");
                sw.WriteLine("beyond the point of contraflexure, their length the force of");
                _val1 = x * 1000.0 - d_eff;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                sw.WriteLine("Counterfort = x + d = {0} - {1} = {2} mm", (x * 1000), d_eff, _val1);
                sw.WriteLine();
                sw.WriteLine("Hence, it is better to continue these rebars uo to the Counterfort at ");
                sw.WriteLine("the top face of the toe slab.");
                sw.WriteLine();
                sw.WriteLine("REINFORCEMENT AT E(at the face of the stem wall)");
                sw.WriteLine("      = Upward Soil pressure 1 m per face of stem wall");
                sw.WriteLine();
                _val1 = p1 - ((p1 - p2) / B) * (b - 1.0);
                _val1 = MyList.StringToDouble(_val1.ToString("0.00"), 0.0);
                sw.WriteLine("      = p1 - ((p1 - p2)/B) * (b-1.0)");
                sw.WriteLine("      = {0} - (({0} - {1})/{2}) * ({3}-1.0)", p1, p2, B, b);
                sw.WriteLine("      = {0} kN/sq.m", _val1);
                sw.WriteLine();
                net_upw_press = _val1 - self_weight;
                net_upw_press = MyList.StringToDouble(net_upw_press.ToString("0.000"), 0.0);
                sw.WriteLine("Net upward pressure = {0} - weight of slab of length 1.0 mm", _val1);
                sw.WriteLine("                    = {0} - {1}", _val1, self_weight);
                sw.WriteLine("                    = {0} kN/sq.m", net_upw_press);
                _val1 = net_upw_press / net_int_D;
                _val1 = MyList.StringToDouble(_val1.ToString("0.00"), 0.0);
                sw.WriteLine();
                sw.WriteLine("The factor is  = (Net Pressure for Toe slab)/(Net upward pressure for whole base slab))");
                sw.WriteLine("               = {0}/{1}", net_upw_press, net_int_D);
                sw.WriteLine("               ≈ {0} of (w = {1}, Net upward Base for whole base slab)", _val1, net_int_D);
                sw.WriteLine();
                _val2 = spacing_1 / _val1;
                _val2 = MyList.StringToDouble(_val2.ToString("0.00"), 0.0);
                spacing_bottom_steel = (int)(_val2 / 10);
                spacing_bottom_steel = spacing_bottom_steel * 10;

                sw.WriteLine(" Spacing of bottom steel rebars = {0}/{1} = {2} ≈ {3} mm", spacing_1, _val1, _val2, spacing_bottom_steel);
                list_rebar.Add(string.Format("Bottom Rebars                                  12mm dia. @ {0}mm c/c. Marked as [10] in the drawing.", spacing_bottom_steel));
                list_rebar_drawing.Add(string.Format("_09={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing_bottom_steel));
                _val2 = spacing_2 / _val1;
                _val2 = MyList.StringToDouble(_val2.ToString("0"), 0.0);
                spacing_top_steel = (int)(_val2 / 10);
                spacing_top_steel *= 10;
                sw.WriteLine(" Spacing of top steel rebars = = {0}/{1} = {2} ≈ {3} mm", spacing_2, _val1, _val2, spacing_top_steel);
                list_rebar.Add(string.Format("Top Rebars                                 12mm dia. @ {0}mm c/c. Marked as [9] in the drawing.", spacing_top_steel));
                list_rebar_drawing.Add(string.Format("_10={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing_top_steel));
                
                
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DISTRIBUTION STEEL ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                Asd = (0.12 / 100) * (1000 * D);
                Asd = MyList.StringToDouble(Asd.ToString("0"), 0.0);
                sw.WriteLine("Asd = (0.12/100)*(1000*Do)");
                sw.WriteLine("    = (0.12/100)*(1000*{0})", D);
                sw.WriteLine("    = {0} sq.mm", Asd);
                sw.WriteLine();
                sw.WriteLine("Using 12 mm Φ bars, area of one bar = 113 sq.mm");
                _val1 = (1000.0 * 113.0) / Asd;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                spacing = (int)(_val1 / 10);
                spacing *= 10;
                sw.WriteLine("Spacing = 1000 * 113/{0} = {1} ≈ {2} mm", Asd, _val1, spacing);
                list_rebar.Add(string.Format("Distribution Rebars                 12mm dia. @ {0}mm c/c. Marked as [11] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_11={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("SHEAR REINFORCEMENTS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Shear force at D = {0} kN", shear_force_D);
                tau_v = (shear_force_D * 1000) / (1000 * d_eff);
                tau_v = MyList.StringToDouble(tau_v.ToString("0.00"), 0.0);
                sw.WriteLine("             τ_v = ({0} * 1000)/(1000*{1})", shear_force_D, tau_v);
                sw.WriteLine("                        = {0}  N/sq.mm", tau_v);
                sw.WriteLine();
                percent = (100 * actual_ast1) / (1000 * d_eff);
                percent = MyList.StringToDouble(percent.ToString("0.00"), 0.0);
                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
                tau_c = MyList.StringToDouble(tau_c.ToString("0.00"), 0.0);
                sw.WriteLine("    (100*As)/(b*d) = (100 * {0})/(100 * {1})", actual_ast1, d_eff);
                sw.WriteLine("                   = {0}% ", percent, tau_c);
                sw.WriteLine();
                sw.WriteLine(" Hence from Table {0}, τ_c = {1} N/sq.mm", ref_string, tau_c);
                sw.WriteLine();
                if (tau_v > tau_c)
                {
                    sw.WriteLine("Since        τ_v > τ_c  , shear reinforcement is necessary.");
                }
                else
                {
                    sw.WriteLine("Since        τ_v < τ_c  , shear reinforcement is not necessary.");
                }
                _val1 = tau_c * 1000 * d_eff;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                Vc = (_val1 / 1000.0);
                Vc = MyList.StringToDouble(Vc.ToString("0.0"), 0.0);
                sw.WriteLine("Now        Vc  = τ_c*b*d");
                sw.WriteLine("               = {0} * 1000 * {1}", tau_c, d_eff);
                sw.WriteLine("               = {0} N", _val1);
                sw.WriteLine("               = {0} kN", Vc);
                sw.WriteLine();
                sw.WriteLine("Consider a section distant x2 from the face of the counterfort (Fig. 2) where");
                sw.WriteLine("S.F. is {0} kN. Position x2 is given by", Vc);
                _val1 = (l / 2) - (Vc / shear_force_D) * (l / 2);
                _val1 = MyList.StringToDouble(_val1.ToString("0.000"), 0.0);
                x2 = _val1;
                sw.WriteLine("  ({0}/{1}) = ({2} - x2) / {2}", Vc, shear_force_D, (l / 2));
                sw.WriteLine("     or,  x2 = {0}  m.", x2);
                sw.WriteLine();


                sw.WriteLine("Hence shear stirrups are required upto a distance of {0} m on either side of counterforts.", x2);
                sw.WriteLine("This requirement is there for a strip of unit width passing through D. Let us consider");
                sw.WriteLine("a strip through E1,distant  y2  from D, such that shear force  at  the  counterforts is  ");
                sw.WriteLine("{0} kN. To find the position of y2, consider the net pressure distribution below the toe slab.", Vc);
                sw.WriteLine();
                sw.WriteLine("Self weight of toe slab = {0} kN/sq.m. Hence net pressure intensity below D and", self_weight);
                sw.WriteLine("E are respectively {0} - {1} = {2} kN/sq.m and {3} - {1}  = {4} kN/sq.m respectively.(Fig 2)", p1, self_weight, net_int_D, P1_, net_int_E);
                sw.WriteLine();
                sw.WriteLine("Let the net pressure intensity at  E1, be w2 kN/sq.m");
                sw.WriteLine("S.F. at the counterforts at E1 = w2 * ({0}/2) = {1}*w2 kN.   This should be equal to  {2} kN.", l, (l / 2), Vc);
                //sw.WriteLine("                               = 127.6 kN.");
                sw.WriteLine();
                w2 = Vc / (l / 2);
                w2 = MyList.StringToDouble(w2.ToString("0.0"), 0.0);
                sw.WriteLine("        w2 = ({0}/{1}) = {2} kN/sq.m", Vc, (l / 2), w2);
                sw.WriteLine();
                _val1 = ((net_int_D - net_int_E) / b);
                _val1 = MyList.StringToDouble(_val1.ToString("0.0"), 0.0);
                sw.WriteLine("      However, at y2 from D, w2 = {0} - (({0}-{1})/{2}) * y2", net_int_D, net_int_E, b);
                sw.WriteLine("                        or   w2 = {0} - {1}*y2", net_int_D, _val1);
                sw.WriteLine();
                y2 = net_int_D - w2;
                y2 = y2 / _val1;
                y2 = MyList.StringToDouble(y2.ToString("0.00"), 0.0);

                sw.WriteLine("Equating the two we get, {0} - {1}*y2 = {2}", net_int_D, _val1, w2);
                sw.WriteLine("From which  y2 = {0}  m. This is more than DE( = {1} m).", y2, b);
                sw.WriteLine("Hence shear force at E is more than {0} kN.", Vc);
                actual_SF = (l / 2.0) * net_int_E;
                actual_SF = MyList.StringToDouble(actual_SF.ToString("0.0"), 0.0);
                sw.WriteLine("Actual S.F. at E = {0} * {1} = {2} kN", (l / 2), net_int_E, actual_SF);
                sw.WriteLine();
                sw.WriteLine("Consider a section distant z from the face of counterforts (Point E), where S.F.");
                sw.WriteLine("is {0} kN. The position of z is given by", Vc);
                sw.WriteLine("           ({0}/{1}) = ({2} - z)/{2}", Vc, actual_SF, (l / 2));

                z = (l / 2) - ((Vc / actual_SF) * (l / 2));
                z = MyList.StringToDouble(z.ToString("0.00"), 0.0);
                sw.WriteLine("                       z = {0} m.", z);
                sw.WriteLine();
                sw.WriteLine(" Hence shear stirrups are to be provided for a region DEE2D1 where EE2 = {0} m", z);
                sw.WriteLine("only. However, we will provide stirrups for whole of rectangular area (shown dotted), for");
                sw.WriteLine("width DD1 = x2 = {0} m and length DE = {1} m.", x2, b);
                sw.WriteLine();
                sw.WriteLine(" Let us provide 8 legged stirrups, of 8 mm Φ wire.");
                sw.WriteLine();
                Asv = 8 * (Math.PI / 4) * 8 * 8;
                Asv = MyList.StringToDouble(Asv.ToString("0"), 0.0);
                sw.WriteLine("     Asv = 8*(π/4)*8*8 = 402 sq.mm");
                spacing = (Asv * sigma_st * d_eff) / ((shear_force_D - Vc) * 1000);
                spacing = MyList.StringToDouble(spacing.ToString("0.000"), 0.0);
                //_val1 = spacing;
                spacing = (int)(spacing / 10);
                spacing = (spacing * 10);

                sw.WriteLine(" Spacing = (Asv * σ_st * d)/(V-Vc)");
                sw.WriteLine("         = ({0} * {1} * {2})/(({3}  - {4}) * l000)", Asv, sigma_st, d_eff, shear_force_D, Vc);
                sw.WriteLine("         ≈ {0} mm ", spacing);
                list_rebar.Add(string.Format("Shear Reinforcements                  8mm dia. 8 Legged @ {0}mm c/c. Marked as [12] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_12=8mm Ø 8 lgd ties @ {0} c/c", spacing));

                sw.WriteLine();
                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Design of stem Wall (vertical Wall)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The stem  acts as a continuous slab. Consider 1 m strip at B.");
                sw.WriteLine("The intensity of earth pressure is given by");
                sw.WriteLine();
                ph = (1 / Kp) * gama_s * (H - D / 1000.0);
                ph = MyList.StringToDouble(ph.ToString("0"), 0.0);
                sw.WriteLine("   ph = Ka * γ * H1");
                sw.WriteLine("      = (1/{0}) * {1} * {2}", Kp, gama_s, (H - D / 1000.0));
                sw.WriteLine("      = {0} kN/sq.m (where revised value of H1 = {1} - {2} = {3} m)", ph, H, D / 1000.0, (H - D / 1000.0));
                sw.WriteLine();
                sw.WriteLine("So, Negative B.M. in slab near counterforts is");
                sw.WriteLine();
                M1 = (ph * l * l) / 12.0;
                M1 = MyList.StringToDouble(M1.ToString("0.00"), 0.0);
                sw.WriteLine("       M1 = (ph * l * l)/12");
                sw.WriteLine("          = ({0} * {1} * {1})/12", ph, l);
                sw.WriteLine("          = {0} kN-m", M1);
                sw.WriteLine("          = {0} * 10^6  N-mm", M1);
                sw.WriteLine();
                depth = Math.Sqrt((M1 * 10e5) / (1000 * Rc));
                depth = MyList.StringToDouble(depth.ToString("0"), 0.0);
                sw.WriteLine("d = √(({0} * 10^6)/(1000 * {1})) ", M1, Rc);
                sw.WriteLine("  = {0}  mm.", depth);
                sw.WriteLine();
                total_depth = depth + 60;
                sw.WriteLine("Providing effective cover = 60 mm, total depth = {0} + 60 = {1} mm. However,", depth, total_depth);

                provide_total_depth = (int)(total_depth / 10);
                provide_total_depth += 5;
                provide_total_depth *= 10;
                available_depth = provide_total_depth - 60;
                sw.WriteLine("Provide total depth = {0} mm so that available d = {0} - 60 = {1} mm. This increased", provide_total_depth, available_depth);
                sw.WriteLine("thickness will keep the shear stress within limits so that additional shear reinforcement");
                sw.WriteLine("is not required.");


                sw.WriteLine();
                sw.WriteLine();
                shear_force_V = (ph * (l / 2));
                shear_force_V = MyList.StringToDouble(shear_force_V.ToString("0.0"), 0.0);
                sw.WriteLine("Shear   force   V = (({0} * {1})/2) = {2} kN.", ph, l, shear_force_V);
                tau_v = (shear_force_V * 1000) / (1000 * available_depth);
                tau_v = MyList.StringToDouble(tau_v.ToString("0.00"), 0.0);
                sw.WriteLine();
                sw.WriteLine("  τ_v = ({0} * 1000)/(1000 * {1}) = {2} N/sq.mm", shear_force_V, available_depth, tau_v);
                
                tau_c = iApp.Tables.Permissible_Shear_Stress(0.5, con_grade, ref ref_string);
                tau_c = MyList.StringToDouble(tau_c.ToString("0.00"), 0.0);
                sw.WriteLine("From Table ({0})", ref_string);
                sw.WriteLine();
                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("This is less than τ_c = {0} N/sq.m at 0.5% reinforcement", tau_c);
                }
                else
                {
                    sw.WriteLine("This is greater than τ_c = {0} N/sq.m at 0.5% reinforcement", tau_c);
                }
                sw.WriteLine();
                Ast1 = (M1 * 10e5) / (sigma_st * jc * available_depth);
                Ast1 = MyList.StringToDouble(Ast1.ToString("0"), 0.0);
                sw.WriteLine("Area of steel near counterforts is Ast1 = ({0} * 10^6)/({1} * {2} * {3})", M1, sigma_st, jc, available_depth);
                sw.WriteLine("                                        = {0} sq.mm", Ast1);
                sw.WriteLine();
                reinfocement_p = 0.5 * 1000 * available_depth / 100;
                reinfocement_p = MyList.StringToDouble(reinfocement_p.ToString("0"), 0.0);
                sw.WriteLine("Reinforcement corresponding to   p = 0.5% is = p*d*b/100");
                sw.WriteLine("                                   = ({0} * 1000 * {1}) / 100", 0.5, available_depth);
                sw.WriteLine("                                   = {0} cu.mm", reinfocement_p);
                sw.WriteLine();
                spacing = (1000 * 113) / reinfocement_p;
                spacing = MyList.StringToDouble(spacing.ToString("0"), 0.0);
                sw.WriteLine("Spacing of 12 mm Φ bars = (1000 * 113) / {0} = {1} mm", reinfocement_p, spacing);
                sw.WriteLine();
                spacing = (int)(spacing / 10);
                spacing = spacing * 10;
                sw.WriteLine("Hence provide 12 mm Φ bars @ {0} mm c/c.", spacing);
                list_rebar.Add(string.Format("Heel side Rebars                 12mm dia. @ {0}mm c/c.   Marked as [13] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_13={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing));
                sw.WriteLine();
                actual_ast = (1000 * 113) / spacing;
                actual_ast = MyList.StringToDouble(actual_ast.ToString("0"), 0.0);

                sw.WriteLine("Actual Ast = (1000 * 113)/{0} ≈ {1} sq. mm and", spacing, actual_ast);
                sw.WriteLine();
                percent = (100 * actual_ast) / (1000 * available_depth);
                percent = MyList.StringToDouble(percent.ToString("0.00"), 0.0);
                sw.WriteLine("and  (100 * As) / (b * d) = (100 * {0})/(1000*{1})", actual_ast, available_depth);
                sw.WriteLine("                          =  {0}% ", percent);
                sw.WriteLine();

                sw.WriteLine("Let us check these bars for development length, near points of contraflexure, so");
                sw.WriteLine("as to satisfy the criterion : M/V + Lo >= Ld");
                sw.WriteLine();
                sw.WriteLine("For a fixed beam or slab carrying U.D.L., the point of contraflexure is at  a distance");
                sw.WriteLine("of 0.211 L = {0} m from the face of counterforts, S.F. at this point is given by", x);

                V = shear_force_V * (l / 2 - x);
                V = MyList.StringToDouble(V.ToString("0.00"), 0.0);
                sw.WriteLine(" V = (p * L/2) * (l/2 - x) / (L/2)");
                sw.WriteLine("   = p * (l/2 - x)");
                sw.WriteLine("   = {0} * ({1}/2 - {2})", shear_force_D, l, x);
                sw.WriteLine("   = {0} kN", V);
                sw.WriteLine();
                sw.WriteLine("Assuming that all the bars will be available at the point of contraflexure");

                M = sigma_st * actual_ast * jc * available_depth;
                M /= 10e5;
                M = MyList.StringToDouble(M.ToString("0.00"), 0.0);

                sw.WriteLine("    M  = σ_st * Ast * jc * d");
                sw.WriteLine("       = {0} * {1} * {2} * {3}", sigma_st, actual_ast, jc, d);
                sw.WriteLine("       = {0} * 10^6 N-mm", M);
                sw.WriteLine();
                Lo = ((12 * 12) > available_depth) ? (12 * 12.0) : available_depth;
                sw.WriteLine("Lo = 12Φ   or  d, whichever is more = {0} mm", Lo);
                Ld = 45 * 12;
                sw.WriteLine("Ld ≈ 45 * 12 ≈ {0} mm", Ld);
                sw.WriteLine();
                _val1 = (M * 10e5) / (V * 1000) + available_depth;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);

                sw.WriteLine(" M/V + Lo = ({0} * 10^6)/({1} * 1000) + {2} ", M, V, available_depth);
                sw.WriteLine("           = {0}  mm", _val1);
                sw.WriteLine();
                sw.WriteLine("Hence safe. It is thus essential to continue all the bars upto a point distant {0} mm ", available_depth);
                _val2 = x * 1000 + available_depth;
                upto_point = (int)(_val2 / 100);
                upto_point++;
                upto_point = upto_point * 100;

                sw.WriteLine("beyond point of contraflexure, i.e. upto a point {0} + {1} = {2}  mm ≈ {3} mm from", (x * 1000), available_depth, _val2, upto_point);
                sw.WriteLine("the face of counterforts. These bars are to be provided at the inner face of the stem");
                sw.WriteLine("slab.");
                sw.WriteLine();
                BM = (3.0 / 4.0) * M1;
                BM = MyList.StringToDouble(BM.ToString("0.00"), 0.0);
                sw.WriteLine(" Maximum positive  B.M. = (3/4) * M1");
                sw.WriteLine("                        = (3/4)*({0} * 10^6)", M1);
                sw.WriteLine("                        = {0} * 10^6  N-mm", BM);
                sw.WriteLine();
                area = (3.0 / 4.0) * actual_ast;
                area = MyList.StringToDouble(area.ToString("0"), 0.0);
                sw.WriteLine(" Area of steel  = (3/4)* Ast");
                sw.WriteLine("                = (3/4)* {0}", actual_ast);
                sw.WriteLine("                = {0} sq.mm", area);
                sw.WriteLine();
                spacing = (1000 * 113) / area;
                spacing = MyList.StringToDouble(spacing.ToString("0"), 0.0);
                _val1 = spacing;
                spacing = (int)(_val1 / 10);
                spacing = spacing * 10;
                sw.WriteLine("Spacing of 12 mm Φ bars = (1000 * 113) / {0} = {1} mm.", area, _val1);
                list_rebar.Add(string.Format("Toe side Rebars                 12mm dia. @ {0}mm c/c. Marked as [14] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_14={0}mm Ø Rebars @ {1}mm c/c", dia1, spacing));
                sw.WriteLine("Hence, provide these @ {0} mm c/c.", spacing);
                actual_ast1 = (1000 * 113) / spacing;
                actual_ast1 = MyList.StringToDouble(actual_ast1.ToString("0"), 0.0);
                sw.WriteLine("Actual   Ast = (1000 * 113)/{0} = {1} sq.mm.", spacing, actual_ast1);
                sw.WriteLine();
                sw.WriteLine("Let us check these bars for development length at the point of contraflexure, so");
                sw.WriteLine("as to satisfy the criterion : (M/V) + L0 >= Ld");
                sw.WriteLine("Assuming that all the reinforcement is extended upto point of contraflexure");
                sw.WriteLine();
                sw.WriteLine();

                M = sigma_st * actual_ast1 * jc * available_depth;
                M /= 10e5;
                M = MyList.StringToDouble(M.ToString("0"), 0.0);
                sw.WriteLine(" M  = {0} * {1} * {2} * {3}", sigma_st, actual_ast1, jc, available_depth);
                sw.WriteLine("    = {0} * 10^6 N-mm", M);
                sw.WriteLine(" Lo  = 12Φ   or  d   ,   whichever   is  more    = {0}  mm", Lo);
                sw.WriteLine(" Ld  = 45 * 12 = 540  mm", Ld);
                sw.WriteLine(" V  = {0}  kN,   as   before,", V);
                sw.WriteLine();
                _val1 = (M * 10e5) / (V * 1000) + Lo;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                sw.WriteLine(" M/V + Lo = ({0} * 10^6)/({1} * 1000) + {2}", M, V, Lo);
                if (_val1 > Ld)
                {
                    sw.WriteLine("          = {0} mm  > Ld.  Hence   safe", _val1);
                }
                else
                {
                    sw.WriteLine("          = {0}   mm  < Ld.  Hence  unsafe.", _val1);
                }
                sw.WriteLine();
                sw.WriteLine("The spacing of the reinforcement at B, found above can be increased with height.");
                sw.WriteLine("The pressure ph, and hence the bending moment decreases linearly with height.");
                sw.WriteLine();
                sw.WriteLine("                                         Ast ∞ h");
                sw.WriteLine();
                sw.WriteLine("Hence the spacing of the bars can be increased gradually to say 300 mm c/c near the top.");
                sw.WriteLine("Distribution steel = (0.12/100) * (1000 * 300) = 360 sq.mm.");
                sw.WriteLine();
                sw.WriteLine("Using 10 mm Φ bars, AΦ = 78.5 sq.mm");
                sw.WriteLine();
                sw.WriteLine(" Spacing = (1000 * 78.5) / 360 = 218 mm. Hence provide these @ 200 mm c/c.");
                list_rebar.Add(string.Format("Distribution Rebars                 10mm dia. @ 200mm c/c. Marked as [15] in the drawing."));
                list_rebar_drawing.Add(string.Format("_15=10mm Ø Rebars @ 200 to 300 mm c/c", dia1, _val1));
                sw.WriteLine();

                #endregion

                #region STEP 7
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : Design of main counterfort");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                counterfort_spaced = l + th3;
                //P2_
                sw.WriteLine("Let us assume thickness of counterfort = {0} mm.", th3*1000);
                sw.WriteLine("The counterforts will thus be spaced @ {0} cm c/c. They will thus receive earth pressure",counterfort_spaced*100);
                sw.WriteLine("from a width of {0} m and downward reaction from the heel slab for a width of {0} m.", counterfort_spaced);
                sw.WriteLine();
                sw.WriteLine("At any section at depth h below the top A, the earth pressure acting on each");
                _pre_h = (1 / 3.0) * (gama_s * counterfort_spaced);
                _pre_h = MyList.StringToDouble(_pre_h.ToString("0.00"), 0.0);
                sw.WriteLine("counterfort will be = (1/3) * {0} * h * {1}  = {2} * h  kN/m", gama_s,counterfort_spaced,_pre_h);
                sw.WriteLine();
                H1 = H - th3;
                sw.WriteLine("Similarly, net downward pressure on heel at C is");

                p2 = MyList.StringToDouble(p2.ToString("0.0"), 0.0);
                dnw_press_C = H1 * gama_s + (th3 * gama_c) - p2;
                dnw_press_C = MyList.StringToDouble(dnw_press_C.ToString("0.00"), 0.0);
                sw.WriteLine("                      = {0} * {1} + {2}  * {3} - {4}", H1, gama_s, th3, gama_c, p2);
                sw.WriteLine("                      = {0} kN/sq.m", dnw_press_C);
                sw.WriteLine();

                P2_ = MyList.StringToDouble(P2_.ToString("0.0"), 0.0);
                dnw_press_B = H1 * gama_s + (th3 * gama_c) - P2_;
                dnw_press_B = MyList.StringToDouble(dnw_press_B.ToString("0.0"), 0.0);
                sw.WriteLine("and that at B is = {0} * {1} + {2}  * {3} - {4}", H1, gama_s, th3, gama_c, P2_);
                sw.WriteLine("                 = {0} kN/sq.m", dnw_press_B);
                sw.WriteLine();
                sw.WriteLine("Hence reaction transferred to each counterfort will be");
                at_C = dnw_press_C * counterfort_spaced;
                at_C = MyList.StringToDouble(at_C.ToString("0.00"), 0.0);
                sw.WriteLine("At   C,  {0} * {1} = P5 = {2} kN/m.", dnw_press_C, counterfort_spaced, at_C);
                list_pressure.Add(string.Format("_P5=P5 = {0} kN/sq.m.", at_C));
                at_B = dnw_press_B * counterfort_spaced;
                at_B = MyList.StringToDouble(at_B.ToString("0.00"), 0.0);
                sw.WriteLine("At   B,  {0} * {1} = P6 = {2} kN/m.", dnw_press_B, counterfort_spaced, at_B);
                list_pressure.Add(string.Format("_P6=P6 = {0} kN/sq.m.", at_B));
                sw.WriteLine();
                sw.WriteLine("The variations of horizontal and vertical forces on the counterfort are shown in the ");
                sw.WriteLine("drawing Fig 3. The critical section for the counterfort will be at F, since below ");
                sw.WriteLine("this, enormous depth will be available to resist bending.");
                sw.WriteLine();
                pressure_int_h = h1 * _pre_h;
                pressure_int_h = MyList.StringToDouble(pressure_int_h.ToString("0.00"), 0.0);
                sw.WriteLine(" Pressure intensity at h = {0} m is = {1} * {0} = {2} kN/m", h1, _pre_h, pressure_int_h);

                shear_force_F = ((1.0 / 2.0) * pressure_int_h * h1);
                shear_force_F = MyList.StringToDouble(shear_force_F.ToString("0.00"), 0.0);
                sw.WriteLine(" Shear force at F = (1/2) * {0} * {1} = {2} kN", pressure_int_h, h1, shear_force_F);
                sw.WriteLine();
                M = shear_force_F * h1 / l;
                M = MyList.StringToDouble(M.ToString("0.00"), 0.0);
                sw.WriteLine("        B.M.   M  = {0} * ({1}/{2})", shear_force_F, h1, l);
                sw.WriteLine("                  = {0}  kN-m", M);
                sw.WriteLine("                  = {0} * 10^6 N-mm.", M);
                sw.WriteLine();
                sw.WriteLine("The counterfort acts as  a T-beam. However, even as a rectangular beam, depth required is ");
                sw.WriteLine();
                depth = Math.Sqrt((M * 10e5) / (D * Rc));
                depth = MyList.StringToDouble(depth.ToString("0"), 0.0);
                sw.WriteLine("   d  = √(({0} * 10^6)/({1} * {2}))", M, D, Rc);
                sw.WriteLine("      = {0} mm", depth);

                total_depth = depth + 60;
                keep_D = (int)(total_depth / 10);
                keep_D += 4;
                keep_D *= 10;
                sw.WriteLine(" Total depth = {0} + 60 = {1} mm. However, keep D = {2} mm", depth, total_depth, keep_D);

                depth = keep_D - 60;
                sw.WriteLine(" so that d = {0} - 60 = {1}  mm.", keep_D, depth);
                sw.WriteLine();
                sw.WriteLine("Angle  θ of face AC is given by");

                _val1 = Math.Atan(2 / 7.5);
                _val2 = _val1 / rad;
                deg = (int)_val2;
                min = _val2 - deg;
                min *= 60;
                min = MyList.StringToDouble(min.ToString("0"), 0.0);
                theta = _val1;
                sw.WriteLine("   tan  θ = 2/7.5 = 0.267");
                sw.WriteLine("        θ = {0}° {1}'", deg, min);
                sw.WriteLine();

                sin_theta = Math.Sin(theta);
                sin_theta = MyList.StringToDouble(sin_theta.ToString("0.0000"), 0.0);
                cos_theta = Math.Cos(theta);
                cos_theta = MyList.StringToDouble(cos_theta.ToString("0.0000"), 0.0);
                sw.WriteLine("  sin θ = {0}    and     cos θ = {1}", sin_theta, cos_theta);
                sw.WriteLine();
                F1C1 = h1 * sin_theta;
                F1C1 = MyList.StringToDouble(F1C1.ToString("0.00"), 0.0);
                sw.WriteLine("Depth  F1*C1 = AF1 * sin θ = {0} * {1} = {2} m = {3}  mm", h1, sin_theta, F1C1, F1C1 * 1000);
                F1C1 *= 1000;
                depth_FG = F1C1 + (available_depth + 60);
                sw.WriteLine("Depth FG = {0} + {1} = {2} mm. This is much more than required.", F1C1, (available_depth + 60), depth_FG);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Assuming that steel reinforcement is provided in two layers with 20 mm space between");
                sw.WriteLine("them and providing a nominal cover of 50 mm and 20 mm Φ main bars, the effective depth");
                eff_dep = depth_FG - (50 + 12 + 20 + 10.0);
                sw.WriteLine("will to be equal to {0} - (50 + 12 + 20 + 10) = {1} mm.", depth_FG, eff_dep);
                sw.WriteLine();
                Ast1 = (M * 10e5) / (sigma_st * jc * eff_dep);
                Ast1 = MyList.StringToDouble(Ast1.ToString("0"), 0.0);
                sw.WriteLine("     Ast = (M * 10^6)/(sigma_st * jc * eff_dep)");
                sw.WriteLine("         = ({0} * 10^6)/({1} * {2} * {3})", M, sigma_st, jc, eff_dep);
                sw.WriteLine("         = {0} mm", Ast1);
                sw.WriteLine();
                A_phi = 314;
                nos_bars = Ast1 / A_phi;
                nos_bars = MyList.StringToDouble(nos_bars.ToString("0"), 0.0);
                nos_bars += 1;
                sw.WriteLine(" Using 20 mm Φ bars, AΦ = 314 sq.mm");
                sw.WriteLine(" No. of bars = {0}/314 = {1} , Provide these in   two   layers.", Ast1, nos_bars);
                list_rebar.Add(string.Format("Inclined face Rebars                 20mm dia. {0} bars in two layers   Marked as [16] in the drawing.", nos_bars));
                list_rebar_drawing.Add(string.Format("_16=20mm Ø {0} nos. Rebars in two layers", nos_bars));
                sw.WriteLine();
                _val1 = Math.Tan(theta);
                d_dash = (eff_dep / cos_theta);
                d_dash = MyList.StringToDouble(d_dash.ToString("0"), 0.0);
                SF = shear_force_F * 1000 - ((M * 10e5) / (eff_dep / cos_theta)) * _val1;
                SF = MyList.StringToDouble(SF.ToString("0"), 0.0);

                sw.WriteLine(" Effective   S.F.  = Q - (M/d') * tan θ,");
                sw.WriteLine("            where d' = d/cos θ = {0}/{1} = {2} mm", eff_dep, cos_theta, d_dash);
                sw.WriteLine(" Effective S.F. = Q - (M/d') * tan θ");
                sw.WriteLine("                  = {0} * 1000 -(({1} * 10^6)/{2:f0}) * {3:f4}", shear_force_F, M, d_dash, _val1);
                sw.WriteLine("                  = {0} N", SF);
                sw.WriteLine();
                sw.WriteLine();
                tau_v = SF / (D * d_dash);
                tau_v = MyList.StringToDouble(tau_v.ToString("0.000"), 0.0);
                sw.WriteLine("   τ_v = {0} / ({1} * {2}) = {3}  N/sq.mm", SF, D, d_dash, tau_v);
                sw.WriteLine();
                sw.WriteLine();
                percent = 100 * 10 * A_phi / (D * d_dash);
                percent = MyList.StringToDouble(percent.ToString("0.00"), 0.0);
                sw.WriteLine("     100*As/(b*d) = 100 * 10 * 314/({0} * {1}))", D, d_dash);
                sw.WriteLine("                  = {0}%", percent);
                sw.WriteLine();
                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, con_grade,ref  ref_string);
                sw.WriteLine("   τ_c  = {0}  N/sq.mm.", tau_c);
                sw.WriteLine();
                if (tau_v > tau_c)
                {
                    sw.WriteLine("Thus the shear stress τ_v, is more than permissible shear stress τ_c. However, the");
                    sw.WriteLine("vertical and horizontal ties provided in the counterforts will bear the excess shear stress.");
                }
                else
                {
                    sw.WriteLine("Thus the shear stress τ_v < τ_C.");
                    sw.WriteLine();
                }

                root_H = Math.Sqrt(H);
                root_H = MyList.StringToDouble(root_H.ToString("0.00"), 0.0);
                sw.WriteLine("The height  h  where half of the reinforcement can be curtailed will be equal to ");
                sw.WriteLine("√H = √{0} = {1} m below  A, i.e. at point  H. To locate the position of point of ", H, root_H);
                sw.WriteLine("curtailment on  AC, draw HI parallel to FG. Thus, half the bars can be curtailed ");
                sw.WriteLine("at I. However, these should be extended by a distance 12 Φ = {0} mm beyond I, i.e.", available_depth);
                sw.WriteLine("extended upto I1. The location of H, corresponding to I, can be located by drawing ");
                sw.WriteLine("line I1H1 parallel to FG. It should be noted  that  I1G   should  not be less ");
                sw.WriteLine("than 45 Φ = 900  mm. Similarly, other bars can be curtailed, if desired. shown in Fig 3");

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Design   of   horizontal    ties");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                sw.WriteLine("The vertical stem slab has a tendency to separate out from the counterfort, and hence ");
                sw.WriteLine("should be tied to it by horizontal ties. At any depth h below the top, force causing ");
                _pre_h = (1 / 3.0) * gama_s * l;
                _pre_h = MyList.StringToDouble(_pre_h.ToString("0.00"), 0.0);
                sw.WriteLine("separation ((1/3) * {0} * h * {1} = {0} * h kN/m.", gama_s, l, _pre_h);

                force = _pre_h * h1;
                force = MyList.StringToDouble(force.ToString("0.00"), 0.0);
                sw.WriteLine("At    h = {0} m, force = {1} * {0} = {2} kN/m.", h1, _pre_h, force);
                sw.WriteLine();
                steel_area_reg = (force * 1000) / sigma_st;
                steel_area_reg = MyList.StringToDouble(steel_area_reg.ToString("0"), 0.0);
                sw.WriteLine(" Steel area required = ({0} * 1000)/ {1} = {2} sq.mm  per metre height.", force, sigma_st, steel_area_reg);
                sw.WriteLine();
                A_phi = 157;
                sw.WriteLine(" Using 10 mm Φ  2 legged ties,   AΦ = 2*(π/4)*(10*10) = 157 sq.mm");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine();
                _val1 = (1000 * A_phi) / steel_area_reg;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                spacing = (int)(_val1 / 10);
                spacing -= 3;
                spacing = (spacing * 10);

                sw.WriteLine(" Spacing  = (1000 * {0})/{1} = {2} mm c/c. However, provide these at {3} mm c/c.", A_phi, steel_area_reg, _val1, spacing);
                list_rebar.Add(string.Format("Horizontal Tie Rebars   10mm dia. 2 Legged Ties @ {0}-{1}mm c/c. Marked as [17] in the drawing.", spacing, (spacing + 50)));
                list_rebar_drawing.Add(string.Format("_17=10mm Ø 2 lgd horizontal ties @ {0} to {1} c/c", spacing, (spacing + 50)));
                sw.WriteLine();
                sw.WriteLine("This spacing can be increased gradually to 300 mm c/c towards top.");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Design of vertical ties");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Similar to the stem slab, heel slab has also tendency to separate out from the counterfort,");
                sw.WriteLine("due to net downward force, unless tied properly by vertical ties. The downward force");
                force_at_C = at_C * (l / counterfort_spaced);
                force_at_C = MyList.StringToDouble(force_at_C.ToString("0.00"), 0.0);

                force_at_B = at_B * (l / counterfort_spaced);
                force_at_B = MyList.StringToDouble(force_at_B.ToString("0.00"), 0.0);
                sw.WriteLine("at  C will be {0} * ({1}/{2}) = {3} kN/m ", at_C, l, counterfort_spaced, force_at_C);
                sw.WriteLine();
                sw.WriteLine("at  B will be {0} * ({1}/{2}) = {3} kN/m (see Fig 3)", at_B, l, counterfort_spaced, force_at_B);
                sw.WriteLine();
                sw.WriteLine("Near end C, the heel slab is tied to the counterforts with the help of main reinforcement");
                sw.WriteLine("of counterforts.");
                sw.WriteLine();
                steel_area_C = force_at_C * 1000 / sigma_st;
                steel_area_C = MyList.StringToDouble(steel_area_C.ToString("0"), 0.0);
                sw.WriteLine("Steel area at C = ({0} * 1000)/ {1} =  {2}  sq.mm", force_at_C, sigma_st, steel_area_C);
                sw.WriteLine();
                A_phi = 226.2;
                sw.WriteLine("Using 12 mm Φ  2 legged ties, AΦ = 2 * (π/4) * (12*12) = 226.2 sq.mm");
                sw.WriteLine();
                _val1 = 1000 * A_phi / steel_area_C;
                _val1 = MyList.StringToDouble(_val1.ToString("0"), 0.0);
                spacing_ties_C = (int)(_val1 / 10);
                spacing_ties_C *= 10;
                sw.WriteLine("Spacing of ties = (1000 * {0})/{1} = {2} mm ≈ {3} mm c/c.", A_phi, steel_area_C, _val1, spacing_ties_C);
                sw.WriteLine();
                steel_area_B = force_at_B * 1000 / sigma_st;
                steel_area_B = MyList.StringToDouble(steel_area_B.ToString("0"), 0.0);
                sw.WriteLine("Steel area at   B = ({0}  * 1000)/{1} = {2} sq.mm", force_at_B, sigma_st, steel_area_B);
                sw.WriteLine();
                spacing_ties_B = (1000 * A_phi) / steel_area_B;
                spacing_ties_B = MyList.StringToDouble(spacing_ties_B.ToString("0"), 0.0);
                spacing_ties_B = (int)(spacing_ties_B / 100);
                spacing_ties_B *= 100;
                sw.WriteLine("Spacing   of   ties = (1000 * {0})/{1} ≈ {2} mm c/c.", A_phi, steel_area_B, spacing_ties_B);
                list_rebar.Add(string.Format("Vertical Tie Rebars       12mm dia. 2 Legged Ties @ {0}-{1}mm c/c. Marked as [18] in the drawing.", spacing_ties_C, spacing_ties_B));
                list_rebar_drawing.Add(string.Format("_18=12mm Ø 2 lgd vertical ties @ {0} to {1} c/c", spacing_ties_C, spacing_ties_B));
                sw.WriteLine();
                sw.WriteLine("Thus the spacing of vertical ties can be increased gradually");
                sw.WriteLine(" from {0} mm c/c atC to {1} mm c/c at B.", spacing_ties_C, spacing_ties_B);
                sw.WriteLine();
                #endregion

                #region STEP 8

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : Design of front counterforts");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                p1 = MyList.StringToDouble(p1.ToString("0.0"), 0.0);
                P1_ = MyList.StringToDouble(P1_.ToString("0.0"), 0.0);
                sw.WriteLine("Refer Fig. 1. The upward pressure intensity varies from {0} kN/sq.m at  D to {1}kN/sq.m at E.", p1, P1_);
                dnw_wgt = (th3 * gama_c);
                dnw_wgt = MyList.StringToDouble(dnw_wgt.ToString("0.00"), 0.0);
                sw.WriteLine("Downward weight of {0} mm thick toe slab  = {1}  * {2} = {3} kN/sq.m.", th3 * 1000, th3, gama_c, dnw_wgt);
                net_int_D = p1 - dnw_wgt;
                net_int_E = P1_ - dnw_wgt;
                sw.WriteLine("Hence net w  at  D = {0} - {1} = {2} kN/sq.m", p1, dnw_wgt, net_int_D);
                sw.WriteLine("Hence net w  at  E = {0} - {1} = {2} kN/sq.m", P1_, dnw_wgt, net_int_E);
                sw.WriteLine("The centre to centre spacing of counterforts, {0} mm wide is {1} m.", D, counterfort_spaced);
                transmitted_D = net_int_D * counterfort_spaced;
                transmitted_D = MyList.StringToDouble(transmitted_D.ToString("0"), 0.0);
                double p9;
                p9 = net_int_D * counterfort_spaced;
                p9 = MyList.StringToDouble(p9.ToString("0"), 0.0);

                sw.WriteLine("Hence upward force transmitted to counterfort at D = P9 = {0} * {1} = {2} kN/m and", net_int_D, counterfort_spaced, p9);
                list_pressure.Add(string.Format("_P9=P9 = {0} kN/sq.m.", p9));

                transmitted_E = net_int_E * counterfort_spaced;
                transmitted_E = MyList.StringToDouble(transmitted_E.ToString("0"), 0.0);
                sw.WriteLine("that at E = P10 = {0} * {1}  = {2} kN/m.", net_int_E, counterfort_spaced, transmitted_E);
                list_pressure.Add(string.Format("_P10=P10 = {0} kN/sq.m.", transmitted_E));
                sw.WriteLine();
                sw.WriteLine("The counterfort will bend up as cantilever about face FE shown in Fig 4. Hence DF will be in");
                sw.WriteLine("compression while D1E1 will be in tension, and main reinforcement will be provided at");
                total_upw_frc = (1.0 / 2.0) * (transmitted_D + transmitted_E) * (F1C1 / 1000);
                total_upw_frc = MyList.StringToDouble(total_upw_frc.ToString("0"), 0.0);
                //total_upw_frc = (1 / 2.0) * (552 + 303.1) * 1.8;
                sw.WriteLine("bottom face D1E1. Total upward force =(1/2)*({0} + {1}) * {2}", transmitted_D, transmitted_E, (F1C1 / 1000));
                sw.WriteLine("                                     = {0} kN acting", total_upw_frc);
                sw.WriteLine();
                x = (transmitted_E + 2 * transmitted_D) / (transmitted_E + transmitted_D) * ((F1C1 / 1000) / 3.0);
                x = MyList.StringToDouble(x.ToString("0.00"), 0.0);
                sw.WriteLine("     at x = ({0} + 2 * {1})/({0} + {1}) * ({2}/3)    = {3}   m from   E", transmitted_E, transmitted_D, (F1C1 / 1000), x);
                BM = total_upw_frc * x;
                BM = MyList.StringToDouble(BM.ToString("0"), 0.0);
                BM = (int)(BM / 10);
                BM = (BM * 10);
                sw.WriteLine("   B.M.   = {0} * {1} = {2} kN-m = {2} x 10^6   N-mm ", total_upw_frc, x, BM);
                sw.WriteLine();
                depth = Math.Sqrt((BM * 10e5) / (D * Rc));
                depth = MyList.StringToDouble(depth.ToString("0"), 0.0);
                sw.WriteLine("  d  = √(({0} * 10^6)/({1} * {2}))", BM, D, Rc);
                sw.WriteLine("     = {0}  mm", depth);
                sw.WriteLine();
                depth = (int)(depth / 100);
                depth += 2;
                depth *= 100;
                sw.WriteLine("Hence provide a total depth of {0}  mm, so that with an effective 80 mm,", depth);
                available_depth = depth - 80;
                sw.WriteLine("available  d = {0} - 80  = {1}  mm.", depth, available_depth);
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Thus, project the counterfort 400 mm above ground level, to point F1 as shown.");
                Ast1 = (BM * 10e5) / (sigma_st * jc * available_depth);
                Ast1 = MyList.StringToDouble(Ast1.ToString("0"), 0.0);
                sw.WriteLine(" Ast  = ({0} * 10^6)/({1} * {2} * {3}) = {4} sq.mm.", BM, sigma_st, jc, available_depth, Ast1);
                A_phi = 491;
                sw.WriteLine(" Using 25 mm Φ bars, AΦ = 491 sq.mm");
                sw.WriteLine();
                nos_bars = Ast1 / A_phi;
                _val1 = MyList.StringToDouble(nos_bars.ToString("0.00"), 0.0);
                nos_bars = (int)_val1;
                nos_bars += 1;
                sw.WriteLine("No. of bars = {0}/{1} = {2}  ≈ {3}. Provide these in one layer.", Ast1, A_phi, _val1, nos_bars);
                list_rebar.Add(string.Format("Inclined face Holding Rebars                 16mm dia. 2 bars in one layer   Marked as [19] in the drawing.", nos_bars));
                list_rebar_drawing.Add(string.Format("_19=2 Nos. 16 mm Ø holding Rebars at top", nos_bars));
                sw.WriteLine("These bars should be continued by a distance of 45 Φ = 45 * 25 ≈ 1125 mm beyond E.");
                V = total_upw_frc - (BM / 1.3) * (0.9 / 1.8);
                V = MyList.StringToDouble(V.ToString("0.0"), 0.0);
                sw.WriteLine("Net shear force = F  - ((M/d) * tan θ). From Fig 18.33, tan θ = 0.9/1.8");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("      V  = {0} - ({1}/1.3) *  (0.9/1.8) = {2} kN", total_upw_frc, BM, V);
                tau_v = (V * 1000) / (D * available_depth);
                tau_v = MyList.StringToDouble(tau_v.ToString("0.000"), 0.0);
                sw.WriteLine("and   τ_v = ({0}* 1000)/({1} * {2}) = {3} N/sq.mm", V, D, available_depth, tau_v);
                sw.WriteLine();
                percent = 100 * (6 * A_phi) / (D * available_depth);
                percent = MyList.StringToDouble(percent.ToString("0.00"), 0.0);
                sw.WriteLine("  100 * (As/(b*d)) = 100 * (6 * {0}) / ({1} * {2}) = {3}%", A_phi, D, available_depth, percent);
                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
                if (tau_v > tau_c)
                {
                    sw.WriteLine(" Hence from Table 1, given at the end of this report,");
                    sw.WriteLine(" τ_c = {0} N/sq.mm. Since τ_v > τ_c shear reinforcement is required.", tau_c);
                }
                else
                {
                    sw.WriteLine(" Hence from Table 1, given at the end of this report,");
                    sw.WriteLine(" τ_c = {0} N/sq.mm. Since τ_v < τ_c, shear reinforcement is not required.", tau_c);
                }
                sw.WriteLine();
                Asv = 2 * (Math.PI / 4.0) * 12 * 12;
                Asv = MyList.StringToDouble(Asv.ToString("0"), 0.0);
                sw.WriteLine(" Using 12 mm Φ 2-legged stirrups, Asv = 2*(π/4)*12*12 = 226 sq.mm");
                list_rebar.Add(string.Format("At Bottom of Toe Rebars 25mm dia. {0} bars in one layer   Marked as [20] in the drawing.", nos_bars));
                list_rebar_drawing.Add(string.Format("_20=25mm Ø {0} Nos. Rebars in one layer", nos_bars));
                sw.WriteLine();
                Vc = tau_c * D * available_depth;
                Vc = MyList.StringToDouble(Vc.ToString("0"), 0.0);
                sw.WriteLine(" Vc = τ_c * b * d = {0} * {1} * {2} = {3}  N", tau_c, D, available_depth, Vc);
                sw.WriteLine();
                Vs = V * 1000 - Vc;
                Vs /= 1000;
                Vs = MyList.StringToDouble(Vs.ToString("0.0"), 0.0);
                sw.WriteLine(" Vs = V - Vc = {0} * 1000 - {1} = {2} * 1000 N", V, Vc, Vs);
                sw.WriteLine();
                Sv = sigma_st * Asv * available_depth / Vs * 1000;
                Sv /= 10e5;
                Sv = MyList.StringToDouble(Sv.ToString("0"), 0.0);
                sw.WriteLine(" Sv = σ_st * Asv * d / Vs");
                sw.WriteLine("    = ({0} * {1} * {2})/({3} * 1000)", sigma_st, Asv, available_depth, Vs);
                sw.WriteLine("    = {0} mm, subject to a maximum of 300 mm", Sv);
                sw.WriteLine();
                Sv = (int)(Sv / 10);
                Sv = Sv * 10;
                sw.WriteLine(" However, provide these @ {0} mm c/c. Provide  2-16  mm Φ holding bars at  the top.", Sv);
                sw.WriteLine(" Provide  2-16  mm Φ holding bars at  the top.");
                list_rebar.Add(string.Format("Tie Rebars                                12mm dia. 2 Legged Ties @ {0}mm c/c. Marked as [21] in the drawing.", Sv));
                list_rebar_drawing.Add(string.Format("_21=12mm Ø 2 lgd ties @ {0} to 300mm c/c", Sv));
                sw.WriteLine();


                #endregion

                #region STEP 9

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP  9 : Firing effects in  stem, toe and heel slab");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At the junction of stem, toe and heel slabs fixing moments are induced, which are at right angles");
                sw.WriteLine("to their normal direction of bending. These moments are not determinate, but normal reinforcement");
                sw.WriteLine("given below may be provided");
                sw.WriteLine();
                sw.WriteLine(" (i)  In stem @ 0.8 * 0.3 = 0.24% of  cross section, to be provided at the inner face, ");
                sw.WriteLine("      in vertical direction, for a length = 45 Φ.");
                sw.WriteLine();
                sw.WriteLine("         Ast = (0.24/100)*1000*300 = 720 sq.mm");
                sw.WriteLine();
                sw.WriteLine("      Using 10 mm Φ bars, AΦ = 78.5 sq.mm");
                
                sw.WriteLine();
                spacing = (1000 * 78.5) / steel_area_B;
                spacing = (int)(spacing / 10);
                spacing = (int)(spacing * 10);
                sw.WriteLine("      Spacing = (1000 * 78.5)/{0} ≈ {1} mm c/c", steel_area_B, spacing);
                list_rebar.Add(string.Format("At the inner face, in vertical direction 10mm dia. @ {0}mm c/c. Marked as [22] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_22=At the inner face, in vertical direction 10mm  Ø @ {0}mm c/c.", spacing));
                
                sw.WriteLine("      Length of embedment in stem, above heel slab ≈ 450 mm.");
                sw.WriteLine();
                sw.WriteLine(" (ii)  In toe slab @ 0.12% to be provided at the lower face");
                sw.WriteLine();
                Ast1 = (0.12 / 100) * (1000 * D);
                Ast1 = (int)(Ast1);

                sw.WriteLine("       Ast =(0.12/100)*(1000 * {0}) = {1} sq.mm", D, Ast1);
                sw.WriteLine();
                spacing = (1000 * 78.5) / Ast1;
                spacing = (int)(spacing / 10);
                spacing = (int)(spacing * 10);
                sw.WriteLine("       Spacing of 10 mm Φ bars = (1000 * 78.5)/{0} ≈ {1} mm c/c.", Ast1, spacing);
                list_rebar.Add(string.Format("At the lower face,                 10mm dia. @ {0}mm c/c. Marked as [23] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_23=In Toe Slab at the lower face, 10mm Ø @ {0}mm c/c.", spacing));
                
                sw.WriteLine("       Length of embedment in toe slab = 450 mm.");
                sw.WriteLine();
                sw.WriteLine(" (iii) In heel, @ 0.12% to be provided at the upper face.");

                sw.WriteLine("       Provide 10 mm bars @ 125 c/c to be embeded by a distance of 450 mm.");
                list_rebar.Add(string.Format("At the upper face,                 10mm dia. @ {0}mm c/c. Marked as [24] in the drawing.", spacing));
                list_rebar_drawing.Add(string.Format("_24=In Heel Slab at the upper face, 10mm Ø @ {0}mm c/c.", spacing));
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Each of the above reinforcement should be anchored properly in the adjoining slab,");
                sw.WriteLine("as shown in Fig 5");
                sw.WriteLine();

                #endregion

                #region STEP 10

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 10 : Design of shear key");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The  wall is unsafe , in sliding, and hence shear key will have to be provided,as shown in Fig 6.");
                sw.WriteLine("Let the depth of key = a");
                sw.WriteLine();
                sw.WriteLine("Intensity of passive pressure p, developed in front of the key depends upon the ");
                sw.WriteLine("soil pressure p in front of key.");
                sw.WriteLine();
                Pp = Kp * P1_;
                Pp = MyList.StringToDouble(Pp.ToString("0.00"), 0.0);
                sw.WriteLine("Pp  = Kp * p  = {0} * {1} = {2} kN/sq.m", Kp, P1_, Pp);
                sw.WriteLine();
                sw.WriteLine("Sliding force at level D1C1 = Ph = (1/{0})*({1}/2)*(({2}+a)*({2}+a))", Kp, gama_s, H);
                sw.WriteLine();
                Ph = (1 / Kp) * gama_s / 2;
                Ph = MyList.StringToDouble(Ph.ToString("0.00"), 0.0);
                sw.WriteLine("or  Ph   = {0} * ({1} + a)^2", Ph, H);
                sw.WriteLine();
                force_D1C1 = B * gama_s;
                force_D1C1 = MyList.StringToDouble(force_D1C1.ToString("0.00"), 0.0);
                sw.WriteLine(" Weight of soil between bottom of base and D1C1  = {0} * a * {1}  = {2} * a   kg", B, gama_s, force_D1C1);
                sw.WriteLine();

                double _a, _c;
                x = 1.5;
                _a = 1;
                _b = (x * Ph * 2 * H - mu * force_D1C1 - Pp);
                _c = -(mu * Wv - x * Ph * H * H);

                _val1 = ((-_b) + Math.Sqrt((_b * _b) - (4 * _a * _c))) / (2 * _a);
                _val1 = MyList.StringToDouble(_val1.ToString("0.00"), 0.0);
                _val2 = ((-_b) - Math.Sqrt((_b * _b) - (4 * _a * _c))) / (2 * _a);
                _val2 = MyList.StringToDouble(_val2.ToString("0.00"), 0.0);

                if (_val1 >= 0 && _val2 >= 0)
                {
                    a = ((_val1 < _val2) ? _val1 : _val2);
                }
                else
                {
                    a = ((_val1 > _val2) ? _val1 : _val2);
                }
                sw.WriteLine(" ∑W = ∑Wv + {0} * a", force_D1C1);
                sw.WriteLine("    = {0} + {1} * a", Wv, force_D1C1);
                sw.WriteLine(" Hence for equilibrium of wall, permitting F.S. = 1.5 against sliding, we have");
                sw.WriteLine();
                sw.WriteLine("    1.5 = (µ∑W+ Pp)/Ph");
                sw.WriteLine("        = ({0}*({1} + {2} * a) + {3} * a) /({4}*({5} + a)^2)", mu, Wv, force_D1C1, Pp, Ph, H);
                sw.WriteLine();
                sw.WriteLine("   which gives a = {0} m", a);
                sw.WriteLine(" Hence keep a = {0} mm and width of key = {0} mm.", a * 1000);
                sw.WriteLine(" Actual force to   be   resisted    by   key = 1.5*Ph - µ∑W");
                _val1 = 1.5 * Ph * (H + a) * (H + a) - mu * (Wv + force_D1C1 * a);
                _val1 = MyList.StringToDouble(_val1.ToString("0.0"), 0.0);
                sw.WriteLine("         = 1.5 * {0} * ({1} + {2})^2 - {3}* ({4} + {5} * {2})", Ph, H, a, mu, Wv, force_D1C1);
                sw.WriteLine("         = {0} kN", _val1);
                sw.WriteLine();
                SF = (_val1 * 1000) / (a * 1000 * 1000);
                SF = MyList.StringToDouble(SF.ToString("0.00"), 0.0);
                sw.WriteLine(" Shear stress = ({0} * 1000)/({1} * 1000) = {2} N/sq.mm", _val1, a * 1000, SF);
                sw.WriteLine();
                a = a * 1000;
                BM = (_val1 * 1000 * 200) / ((1.0 / 6.0) * 1000 * (a * a));
                BM = MyList.StringToDouble(BM.ToString("0.00"), 0.0);
                sw.WriteLine(" Bending stress = ({0} * 1000 * 200)/((1/6)* 1000 * (a*a))", _val1);
                if (BM >= SF)
                {
                    sw.WriteLine("                = {0} N/sq.mm , Hence safe)", BM);
                }
                else
                {
                    sw.WriteLine("                = {0} N/sq.mm , Hence unsafe.", BM);
                }
                //sw.WriteLine(" The details of reinforcement etc. are shown in Fig. 6.");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                list_dims.Add(string.Format("_H_={0}", H * 1000));
                list_dims.Add(string.Format("_Th2_={0}", th2 * 1000));
                list_dims.Add(string.Format("_b_={0}", b * 1000));
                list_dims.Add(string.Format("_Th1_={0}", th1 * 1000));
                list_dims.Add(string.Format("_d_={0}", d * 1000));
                list_dims.Add(string.Format("_b__={0}mm", b * 1000));
                list_dims.Add(string.Format("_l_={0}mm", l * 1000));
                list_dims.Add(string.Format("_d__={0}mm", d * 1000));
                list_dims.Add(string.Format("_Th3_={0}", th3 * 1000));
                list_dims.Add(string.Format("_Th3__={0}mm", th3 * 1000));
                list_dims.Add(string.Format("_l__={0}m", l));
                list_dims.Add(string.Format("_Th2__={0}mm", th2 * 1000));

                #region Report SUMMARY
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Summary of Abutment design as Counterfort Retaining Wall   ");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("1. Dimensions");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("B = {0}m  Width of full base Slab", B);
                sw.WriteLine("b = {0}m  Width of Toe Slab", b);
                sw.WriteLine("d = {0}m  Width of Heel Slab", d);
                sw.WriteLine("Th1 = {0}m Thickness of Vertical Stem Wall", th1);
                sw.WriteLine("Th2 = {0}m Thickness of Base Slab", th2);
                sw.WriteLine("Th3 = {0}m Thickness of Counterfort Wall", th3);
                sw.WriteLine("l = {0}m Clear spacing between Counterfort walls", l);
                sw.WriteLine("Size of Sher Key = 0.4m x 0.4m");

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Steel Reinforcement Detailing");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("2. Heel Slab");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //sw.WriteLine("Top Rebars                                     12mm dia. @ 97mm c/c.   Marked as [1] in the drawing.");
                //sw.WriteLine("Bottom Rebars                                  12mm dia. @ 120mm c/c.  Marked as [2] in the drawing.");
                sw.WriteLine(list_rebar[0]);
                sw.WriteLine(list_rebar[1]);
                sw.WriteLine();
                sw.WriteLine("At face of Stem wall");
                sw.WriteLine();
                //sw.WriteLine("Top Rebars                                     12mm dia. @ 180mm c/c. Marked as [3] in the drawing.");
                //sw.WriteLine("Bottom Rebars                                  12mm dia. @ 220mm c/c. Marked as [4] in the drawing.");
                //sw.WriteLine("Distribution Rebars                 12mm dia. @ 180mm c/c. Marked as [5] in the drawing.");
                //sw.WriteLine("Shear Reinforcements  8mm dia. 4 Legged @ 150mm c/c. Marked as [6] in the drawing.");
                sw.WriteLine(list_rebar[2]);
                sw.WriteLine(list_rebar[3]);
                sw.WriteLine(list_rebar[4]);
                sw.WriteLine(list_rebar[5]);
                
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("3. Toe Slab");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //sw.WriteLine("Top Rebars             12mm dia. @ 110mm c/c. Marked as [7] in the drawing.");
                //sw.WriteLine("Bottom Rebars          12mm dia. @ 80mm c/c.  Marked as [8] in the drawing.");
                sw.WriteLine(list_rebar[6]);
                sw.WriteLine(list_rebar[7]);

                sw.WriteLine();
                sw.WriteLine("At face of Stem wall");
                sw.WriteLine();
                //sw.WriteLine("Top Rebars             12mm dia. @ 140mm c/c. Marked as [9] in the drawing.");
                //sw.WriteLine("Bottom Rebars          12mm dia. @ 110mm c/c. Marked as [10] in the drawing.");
                //sw.WriteLine("Distribution Rebars    12mm dia. @ 180mm c/c. Marked as [11] in the drawing.");
                //sw.WriteLine("Shear Reinforcements   8mm dia. 8 Legged @ 310mm c/c. Marked as [12] in the drawing.");
                sw.WriteLine(list_rebar[9]);
                sw.WriteLine(list_rebar[8]);
                sw.WriteLine(list_rebar[10]);
                sw.WriteLine(list_rebar[11]);

                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("4. Vertical Stem Wall");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //sw.WriteLine("Heel side Rebars      12mm dia. @ 90mm c/c.   Marked as [13] in the drawing.");
                //sw.WriteLine("Toe side Rebars       12mm dia. @ 120mm c/c.  Marked as [14] in the drawing.");
                //sw.WriteLine("Distribution Rebars   10mm dia. @ 200mm c/c.  Marked as [15] in the drawing.");
                sw.WriteLine(list_rebar[12]);
                sw.WriteLine(list_rebar[13]);
                sw.WriteLine(list_rebar[14]);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("5. Main Counterfort Wall");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //sw.WriteLine("Inclined face Rebars    20mm dia. 10 bars in two layers   Marked as [16] in the drawing.");
                sw.WriteLine(list_rebar[15]);
                sw.WriteLine();
                //sw.WriteLine("Horizontal Tie Rebars   10mm dia. 2 Legged Ties @ 250-300mm c/c. Marked as [17] in the drawing.");
                //sw.WriteLine("Vertical Tie Rebars     12mm dia. 2 Legged Ties @ 130-310mm c/c. Marked as [18] in the drawing.");
                sw.WriteLine(list_rebar[16]);
                sw.WriteLine(list_rebar[17]);
                sw.WriteLine();




                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("6. Front Counterfort Wall");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                //sw.WriteLine("Inclined face Holding Rebars                 16mm dia. 2 bars in one layer   Marked as [19] in the drawing.");
                //sw.WriteLine("At Bottom of Toe Rebars 25mm dia. 6 bars in one layer         Marked as [20] in the drawing.");
                //sw.WriteLine("Tie Rebars    12mm dia. 2 Legged Ties @ 220mm c/c.            Marked as [21] in the drawing.");
                sw.WriteLine(list_rebar[18]);
                sw.WriteLine(list_rebar[19]);
                sw.WriteLine(list_rebar[20]);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("7. Temperature Rebars as shown in Fig 5 in the drawing.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("In Vertical Stem Wall ");
                //sw.WriteLine("At the inner face, in vertical direction 10mm dia. @ 100mm c/c. Marked as [22] in the drawing.");
                sw.WriteLine(list_rebar[21]);
                sw.WriteLine();

                sw.WriteLine("In Toe Slab ");
                //sw.WriteLine("At the lower face,                 10mm dia. @ 125mm c/c. Marked as [23] in the drawing.");
                sw.WriteLine(list_rebar[22]);
                sw.WriteLine();

                sw.WriteLine("In Heel Slab ");
                //sw.WriteLine("At the upper face,                 10mm dia. @ 125mm c/c. Marked as [24] in the drawing.");
                sw.WriteLine(list_rebar[23]);
                sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("8. Shear Key as shown in Fig 6 in the drawing.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Depth of Shear Key = 320mm.");
                sw.WriteLine("Width of Shear Key = 320mm.");

                sw.WriteLine();
                sw.WriteLine("The details of reinforcement etc. are shown in Fig. 6");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region Write Table File
                //string table_file = Path.Combine(Application.StartupPath, "TABLES");
                //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");
                List<string> list = iApp.Tables.Get_Tables_Permissible_Shear_Stress();
                //int indx = list.IndexOf("       PERMISSIBLE SHEAR STRESS τ_c(N/sq.mm)");

                //if (indx != -1)
                //    list[indx] = "Table 1 PERMISSIBLE SHEAR STRESS τ_c(N/sq.mm)";

                sw.WriteLine("----------------------------------------------");
                sw.WriteLine("-------------      TABLE 1    ----------------");
                sw.WriteLine("----------------------------------------------");
                foreach (string s in list)
                {
                    sw.WriteLine(s);
                }
                list.Clear();
                list = null;
                #endregion
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void InitializeData()
        {
            #region Variables Initialize with default data
            h1 = MyList.StringToDouble(txt_h1.Text, 0.0);
            h2 = MyList.StringToDouble(txt_h2.Text, 0.0); 
            th1 = MyList.StringToDouble(txt_th1.Text, 0.0); 
            th2 = MyList.StringToDouble(txt_th2.Text, 0.0); 
            q0 = MyList.StringToDouble(txt_q0.Text, 0.0); 
            gama_s = MyList.StringToDouble(txt_gama_s.Text, 0.0); 
            gama_c = MyList.StringToDouble(txt_gama_c.Text, 0.0); 
            P = MyList.StringToDouble(txt_P.Text, 0.0); 
            phi = MyList.StringToDouble(txt_phi.Text, 0.0); 
            mu = MyList.StringToDouble(txt_mu.Text, 0.0); 
            f_ck = MyList.StringToDouble(txt_f_ck.Text, 0.0);
            con_grade = (CONCRETE_GRADE)(int)(f_ck);
            f_y = MyList.StringToDouble(txt_f_y.Text, 0.0); 
            c = MyList.StringToDouble(txt_cb.Text, 0.0); 
            t = MyList.StringToDouble(txt_t.Text, 0.0); 
            m = MyList.StringToDouble(txt_m.Text, 0.0); 
            Kc = MyList.StringToDouble(txt_Kc.Text, 0.0); 
            jc = MyList.StringToDouble(txt_jc.Text, 0.0);
            Rc = MyList.StringToDouble(txt_Rc.Text, 0.0);
            th3 = MyList.StringToDouble(txt_th3.Text, 0.0);
            l = MyList.StringToDouble(txt_l.Text, 0.0);
            

            #endregion
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult f_v_r = new frmViewResult(rep_file_name);
            f_v_r.ShowDialog();
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

            string kPath = Path.Combine(user_path, "Bridge Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Abutment & Pier");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }


            kPath = Path.Combine(kPath, "Design of RCC Abutment As Counterfort Retaining Wall");

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
                this.Text = "Design of RCC Abutment as Counterfort Retaining Wall : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_ABUTMENT");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_RCC_Abutment_As_Counterfort_Retaining_Wall.TXT");
                user_input_file = Path.Combine(system_path, "RCC_ABUTMENT.FIL");


                btnProcess.Enabled = Directory.Exists(value);
                btnDrawing.Enabled = File.Exists(user_input_file);
                //btnSample_Drawing.Enabled = File.Exists(user_input_file);
                btnReport.Enabled = File.Exists(user_input_file);
                btnSample_Drawing.Enabled = true;

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }

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
            int indx = -1;
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
                        case "h1":
                            h1 = mList.GetDouble(1);
                            txt_h1.Text = h1.ToString();
                            break;

                        case "h2":
                            h2 = mList.GetDouble(1);
                            txt_h2.Text = h2.ToString();
                            break;

                        case "th1":
                            th1 = mList.GetDouble(1);
                            txt_th1.Text = th1.ToString();
                            break;

                        case "th2":
                            th2 = mList.GetDouble(1);
                            txt_th2.Text = th2.ToString();
                            break;

                        case "th3":
                            th3 = mList.GetDouble(1);
                            txt_th3.Text = th3.ToString();
                            break;

                        case "q0":
                            q0 = mList.GetDouble(1);
                            txt_q0.Text = q0.ToString();
                            break;

                        case "gama_s":
                            gama_s = mList.GetDouble(1);
                            txt_gama_s.Text = gama_s.ToString();
                            break;

                        case "gama_c":
                            gama_c = mList.GetDouble(1);
                            txt_gama_c.Text = gama_c.ToString();
                            break;

                        case "P":
                            P = mList.GetDouble(1);
                            txt_P.Text = P.ToString();
                            break;

                        case "phi":
                            phi = mList.GetDouble(1);
                            txt_phi.Text = phi.ToString();
                            break;

                        case "mu":
                            mu = mList.GetDouble(1);
                            txt_mu.Text = mu.ToString();
                            break;

                        case "f_ck":
                            f_ck = mList.GetDouble(1);
                            txt_f_ck.Text = f_ck.ToString();
                            break;

                        case "f_y":
                            f_y = mList.GetDouble(1);
                            txt_f_y.Text = f_y.ToString();
                            break;

                        case "c":
                            c = mList.GetDouble(1);
                            txt_cb.Text = c.ToString();
                            break;

                        case "t":
                            t = mList.GetDouble(1);
                            txt_t.Text = t.ToString();
                            break;

                        case "m":
                            m = mList.GetDouble(1);
                            txt_m.Text = m.ToString();
                            break;

                        case "Kc":
                            Kc = mList.GetDouble(1);
                            txt_Kc.Text = Kc.ToString();
                            break;

                        case "jc":
                            jc = mList.GetDouble(1);
                            txt_jc.Text = jc.ToString();
                            break;

                        case "Rc":
                            Rc = mList.GetDouble(1);
                            txt_Rc.Text = Rc.ToString();
                            break;

                        case "l":
                            l = mList.GetDouble(1);
                            txt_l.Text = l.ToString();
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
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "Abutment_Counterfort", "");

        }
        public void Write_Drawing_File()
        {
            drawing_path  = Path.Combine(system_path, "ABUTMENT_COUNTERFORT_DRAWING.FIL");

            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                foreach (string s in list_pressure)
                {
                    sw.WriteLine(s);
                }
                foreach (string s in list_rebar_drawing)
                {
                    sw.WriteLine(s);
                }
                foreach (string s in list_dims)
                {
                    sw.WriteLine(s);
                }
                sw.WriteLine("_Th1=Th1 = {0} m", th1);
                sw.WriteLine("_Th2=Th2 = {0} m", th2);
                sw.WriteLine("_Th3=Th3 = {0} m", th3);
                sw.WriteLine("_h1=h1 = {0} m", h1);
                sw.WriteLine("_h2=h2 = {0} m", h2);
                sw.WriteLine("_b=b = {0} m", b);
                sw.WriteLine("_d=d = {0} m", d);
                sw.WriteLine("_B=B = {0} m", B);
                sw.WriteLine("_l=l = {0} m", l);

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
        private void btnSample_Drawing_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnSample_Drawing_Click_1(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "", "Abutment_Sample");
        }

        private void txt_f_ck_TextChanged(object sender, EventArgs e)
        {
            InitializeData();
            double fck,fcc, j, Q, fcb, n;
            fck = f_ck;

            fcb = fck / 3;
            fcc = fck / 4;

            n = m * fcb / (m * fcb * t);

            j = 1 - (n / 3.0);
            Q = n * j * fcb / 2;

            txt_cb.Text = fcb.ToString("0.00");
            txt_jc.Text = j.ToString("0.000");
            txt_q0.Text = j.ToString("0.000");
        }
    }
}
