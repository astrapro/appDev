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

namespace AstraFunctionOne.UnderPass
{
    public partial class frmRccAbutment : Form
    {
        #region Variable Declaration
        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string drawing_path = "";
        string system_path = "";
        string user_input_file = "";
        bool is_process = false;



        double d1, t, H, a, gamma_b, gamma_c, phi, p, f_ck, f_y, w6, w5, F, d2, d3, d4, B;
        double theta, delta, z, mu, L1, L2, L3, L4, h1, L, cover, factor;


        
        #endregion

        #region Drawing Variable

        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
        double _sp1, _sp2, _sp3, _sp4, _sp5, _sp6, _sp7;
        #endregion
        string ref_string = "";

        IApplication iApp = null;
        public frmRccAbutment(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;

            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;

            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp5 = 0.0;
            _sp6 = 0.0;
            _sp7 = 0.0;



        }

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
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
        }
        public void Calculate_Program(string file_name)
        {



            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            #region TechSOFT Banner
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 20.0           *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*           DESIGN OF RCC ABUTMENT            *");
            sw.WriteLine("\t\t*          FOR  VEHICULAR UNDERPASS           *");
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
                sw.WriteLine("Depth of Girder Seat = {0} m. = d1", d1.ToString("0.00"));
                sw.WriteLine("Thickness of wall = {0} m = t", t);
                sw.WriteLine("Height of Retained Earth = {0} m = H",H);
                sw.WriteLine("Width of wall = {0} m = B", B);
                sw.WriteLine("Equivalent height of Earth for Live Load Surcharge = {0} m = d2", d2);
                sw.WriteLine("Thickness of Approach Slab = {0} m = d3", d3);
                sw.WriteLine("Length of base in front of wall = {0} m = L1", L1);
                sw.WriteLine("Length of base in wall location = {0} m = L2", L2);
                sw.WriteLine("Length of base at back of wall  = {0} m = L3", L3);
                sw.WriteLine("Thickness of wall at the Top = {0} m = L4", L4);
                sw.WriteLine("Thickness of Base = {0:f2} m = d4", d4);
                sw.WriteLine("Angle between wall and Horizontal base on Earth side = {0}° = θ", theta);
                sw.WriteLine("Inclination of Earth fill side with the Horizontal = {0}° = δ", delta);
                sw.WriteLine("Angle of friction between Earth and Wall = {0}° = z", z);
                sw.WriteLine("Coefficient of friction between Earth and wall = {0} = µ", mu);
                sw.WriteLine("Unit weight of Back fill Earth = {0} kN/cu.m = γ_b", gamma_b);
                sw.WriteLine("Unit weight of Concrete = {0} kN/cu.m = γ_c", gamma_c);
                sw.WriteLine("Angle of Internal friction of backfill = {0}° = φ", phi);
                sw.WriteLine("Bearing Capacity = {0} kN/sq.m = p", p);
                sw.WriteLine("Concrete Grade = M {0:f0} = f_ck = {0:f0}", f_ck);
                sw.WriteLine("Steel Grade = Fe {0:f0} = f_y = {0:f0}", f_y);
                sw.WriteLine("Live Load from vehicles = {0:f2} kN/m = w6", w6);
                sw.WriteLine("Permanent Load from Super Structure = {0:f2} kN/m = w5", w5);
                sw.WriteLine("Braking Force = {0:f2} kN = F", F);
                sw.WriteLine("Bending Moment and Shear Force Factor = {0:f2} = Fact", factor);
                sw.WriteLine("Clear Cover = {0:f2} mm = cover", cover);
                #endregion

                double rad = Math.PI / 180;

                phi = phi * rad;
                delta = delta * rad;
                z *= rad;
                theta *= rad;



                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------"); 
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region STEP 1
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Approximate Sizing (dimensions)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Using Rankine's formula for the depth of foundation base");
                sw.WriteLine();

                double hd = (p / gamma_b) * Math.Pow(((1 - Math.Sin(phi)) / (1 + Math.Sin(phi))),2.0);
                sw.WriteLine("hd = (p/γ_b) * ((1 - Sin φ)/(1 + Sin φ))^2");
                sw.WriteLine("   = ({0}/{1}) * ((1 - Sin {2})/(1 + Sin {2}))^2",
                    p,
                    gamma_b,
                    phi/rad);

                if (hd < a)
                    sw.WriteLine("   = {0} m < a = {1} m , OK", hd.ToString("0.000"), a.ToString("0.000"));
                else
                    sw.WriteLine("   = {0} m > a = {1} m, NOT OK", hd.ToString("0.000"), a.ToString("0.000"));

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Assuming Thickness of base to be 10% of total height {0} m", H);

                double d = d4;
                sw.WriteLine();
                sw.WriteLine("Let us Provide thickness of base = d = {0} cm = {1} m",
                    d * 100,
                    d);

                sw.WriteLine();
                sw.WriteLine("Length of base = l = H √((Ca * Cos δ)/((1 - m) * (1 + 3 * m)))"); //Ca = ?
                //TO DO

                sw.WriteLine();
                sw.WriteLine("Angle of Back fill is horizontal, δ = {0}, m = 1 - (4/(9*q))", delta);
                sw.WriteLine();

                double q = (gamma_b / p) * (H - d);
                sw.WriteLine("where q = γ*h/p = {0}/{1} * (H - d) = {0}/{1} * ({2} - {3}) = {4}",
                    gamma_b,
                    p,
                    H,
                    d,
                    q.ToString("0.000"));
                double Ca = 0.0;
                Ca = (1 - Math.Sin(phi)) / (1 + Math.Sin(phi));
                sw.WriteLine("     Ca = (1 - Sin φ)/(1 + Sin φ)) ");
                sw.WriteLine("        = (1 - Sin {0:f0})/(1 + Sin {0:f0})", phi/rad);
                sw.WriteLine("        = {0:f3} ", Ca);
                sw.WriteLine();

                double m = 1 - (4 / (9 * q));
                sw.WriteLine("      m = 1 - (4/(9*{0:f3}))", q);
                sw.WriteLine("        = {0} ", m.ToString("0.00"));


                double l = 0.0;
                l = H * (Math.Sqrt(((Ca * Math.Cos(delta)) / ((1 - m) * (1 + 3 * m)))));
                //sw.WriteLine("Length of base = l = H √((Ca * Cos δ)/(1 - m) * (1 + 3 * m))");
                sw.WriteLine("      l = {0} √(({1} * Cos {2})/(1 - {3}) * (1 + 3 * {3}))",
                    H.ToString("0.000"),
                    Ca.ToString("0.000"),
                    delta/rad,
                    m.ToString("0.000"));
                sw.WriteLine("        = {0:f2} ", l);
                double provided_l = L1 + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("Provided l = L1 + L2 + L3 = {0} + {1} + {2} = {3} m",
                    L1, L2, L3, provided_l);

                double ml = m * provided_l;
                sw.WriteLine();
                sw.WriteLine("        ml = {0:f2} * {1:f2} = {2:f2}",
                    m,
                    provided_l,
                    ml);
                double adopting_average_thickness = t; //m 100 cm
                sw.WriteLine();
                sw.WriteLine("Adopting average thickness of wall = {0:f2} cm = {1:f2} m",
                    t*100,
                    t);

                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region STEP 2

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Stability Check");
                sw.WriteLine("         Weight of wall");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double w1 = (H - d1 - d4) * (L2 - L4) * gamma_c;
                sw.WriteLine("   w1 = (H - d1 - d4) * (L2 - L4) * γ_c");
                sw.WriteLine("      = ({0} - {1} - {2}) * ({3} - {4}) * {5}",
                    H,
                    d1,
                    d4,
                    L2,
                    L4,
                    gamma_c);
                sw.WriteLine("      = {0:f3} kN", w1);

                sw.WriteLine();
                double D1 = ((L2 - L4) / 2) + L3;
                sw.WriteLine("    D1 = (L2-L4)/2 + L3");
                sw.WriteLine("       = ({0}-{1})/2 + {2} = {3:f3} m",
                    L2,
                    L4,
                    L3,
                    D1);

                sw.WriteLine();
                double w2 = (H - d4) * L4 * gamma_c;
                sw.WriteLine("    w2 = (H - d4) * L4 * γ_c");
                sw.WriteLine("       = ({0} - {1}) * {2} * {3}",
                    H,
                    d4,
                    L4,
                    gamma_c);
                sw.WriteLine("       = {0} kN", w2);

                double D2 = L4 / 2 + (L2 - L4) + L3;
                sw.WriteLine();
                sw.WriteLine("    D2 = L4 / 2 + (L2 - L4) + L3");
                sw.WriteLine("       = {0} / 2 + ({1} - {0}) + {2}",
                    L4,
                    L2,
                    L3);
                sw.WriteLine("       = {0} m", D2);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Weight of Base");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double w3 = (L1 + L2 + L3) * d4 * gamma_c;
                sw.WriteLine("    w3 = (L1 + L2 + L3) * d3 * γ_c");
                sw.WriteLine("       = ({0} + {1} + {2}) * {3} * {4}",
                    L1,
                    L2,
                    L3,
                    d4,
                    gamma_c);
                sw.WriteLine("       = {0:f2} kN",w3);
                double D3 = provided_l / 2;
                sw.WriteLine();
                sw.WriteLine("    D3 = {0} m", D3);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Weight of Earth on Heel Slab");
                sw.WriteLine("------------------------------------------------------------");

                double w4 = (H - d4) * L1 * gamma_b;
                sw.WriteLine();
                sw.WriteLine("   w4 = (H - d4) * L1 * γ_b");
                sw.WriteLine("      = ({0} - {1}) * {2} * {3}",
                    H,
                    d4,
                    L1,
                    gamma_b);
                sw.WriteLine();
                sw.WriteLine("      = {0:f2} kN", w4);

                double D4 = (L1 / 2) + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("   D4 = (L1 / 2) + L2 + L3");
                sw.WriteLine("      = ({0} / 2) + {1} + {2}",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("      = {0:f2} m", D4);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permanent Load from Super Structure = w5 = {0} kN", w5);
                sw.WriteLine();
                double D5 = L3 + ((L2 - L4) / 2);
                sw.WriteLine("                                  D5 = L3 + ((L2 - L4) / 2)");
                sw.WriteLine("                                     = {0} + (({1} - {2}) / 2)",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("                                     = {0:f2} m", D5);
                sw.WriteLine();
                sw.WriteLine("Vertical Load from Super Structure = w6 = {0} kN", w6);
                double D6 = L3 + (L2 - L4) / 2;
                sw.WriteLine();
                sw.WriteLine("                                     D6 = L3 + (L2 - L4) / 2");
                sw.WriteLine("                                        = {0} + ({1} - {2}) / 2",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("                                        = {0:f2} m", D6);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Force due to  braking");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Braking Force = F = {0} kN", F);

                double one_abut_force = F / 2;
                sw.WriteLine();
                sw.WriteLine("Force on one Abutment wall = {0}/2 = {1} kN",
                    F,
                    one_abut_force.ToString("0.0"));
                sw.WriteLine();
                sw.WriteLine("Width of Abutment wall = B = {0} m", B);

                sw.WriteLine();
                double P2 = one_abut_force / B;
                sw.WriteLine("Horizontal Force per m. of wall = {0:f2}/{1:f2} = {2:f3} kN/m = P2",
                    one_abut_force,
                    B,
                    P2);

                double d7 = H - d1;
                sw.WriteLine();
                sw.WriteLine("d7 = H - d1 = {0} - {1} = {2:f3} m",
                    H,
                    d1,
                    d7);
                //double h1 = 1.2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bracking is applied at height = h1 = {0} m", h1);// ? to be check
                sw.WriteLine("Span of Longitudinal Girder = {0} m = L", L);// ? to be check

                sw.WriteLine();
                sw.WriteLine("Vetical bracking force reaction on one abutment");
                double w7 = (F * (h1 + d1 + d3)) / (L * B);
                sw.WriteLine("      W7 = (F * (h1 + d1 + d3)) / (L * B)");
                sw.WriteLine("         = ({0} * ({1} + {2} + {3})) / ({4} * {5})",
                    F,
                    h1,
                    d1,
                    d3,
                    L,
                    B);
                sw.WriteLine("         = {0:f2} kN/m", w7);

                double D8 = L3 + (L2 - L4) / 2;
                sw.WriteLine();
                sw.WriteLine("D8 = L3 + (L2 - L4) / 2");
                sw.WriteLine("   = {0} + ({1} - {2}) / 2",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("   = {0:f2} m", D8);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Active Earth Pressure");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("P1 = 0.5 * H * H * γ_b * ka");

                //double radian = Math.PI / 180;
                double radian = 1;

                double Ka = (Math.Sin(theta * radian - phi * radian) / Math.Sin(theta * radian)) * (1 / (Math.Sqrt(Math.Sin(theta * radian + z * radian)) +
                    (Math.Sqrt((Math.Sin(phi * radian + z * radian)) * (Math.Sin(phi * radian - delta * radian)) / (Math.Sin(theta * radian - delta * radian))))));

                //sw.WriteLine("P1 = 0.5 * H * H * gamma_b * ka");
                //sw.WriteLine("γσµφδπρ√τ≈αβθ = 0.5 * H * H * gamma_b * ka");
                sw.WriteLine();
                sw.WriteLine("θ = {0}° , φ = {1}°, z = {2}°, δ = {3}",
                    (theta/rad),
                    phi/rad,
                    z/rad,
                    delta/rad);
                sw.WriteLine();
                sw.WriteLine("ka = (Sin(θ - φ) / Sin(θ)) / ((√(Sin(θ + z)) + (√(Sin(φ + z) * Sin(φ - δ)/Sin(θ - δ)))");
                sw.WriteLine("   = {0:f3}",Ka);
                sw.WriteLine();

                double P1 = 0.5 * H * H * gamma_b * Ka;

                sw.WriteLine("P1 = 0.5 * {0} * {0} * {1} * {2:f3} = {3:f2} kN/m",
                    H, gamma_b, Ka, P1);

                sw.WriteLine();
                double D9 = 0.42 * H;
                sw.WriteLine("D9 = 0.42 * H = 0.42 * {0} = {1} m", H, D9.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Load from Vehicle and Approach Slab");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double h2 = d2;

                sw.WriteLine("Equivalent height of earth for Vehicle Load Surcharge = d2 = {0:f2} m", d2);

                double hor_force_vehicle = d2 * gamma_b * Ka * H;
                sw.WriteLine();
                sw.WriteLine("Horizontal force for Vehicle Load Surcharge = d2 * γ_b * Ka * H");
                sw.WriteLine("                                            = {0:f2} * {1} * {2:f4} * {3:f2}",
                    h2,
                    gamma_b,
                    Ka,
                    H);
                sw.WriteLine("                                            = {0:f2} ", hor_force_vehicle);

                double hor_force_approach_slab = d3 * gamma_c * Ka * H;
                sw.WriteLine();
                sw.WriteLine("Horizontal force for Approach slab = d3 * γ_c * ka * H");
                sw.WriteLine("                                   = {0} * {1} * {2:f3} * {3}",
                    d3, gamma_c, Ka, H);
                sw.WriteLine("                                   = {0:f2} ", hor_force_approach_slab);

                double P3_total_hor_force = hor_force_approach_slab + hor_force_vehicle;
                sw.WriteLine();
                sw.WriteLine("Total Horizontal Force = {0:f2} + {1:f2} = {2:f2} kN/m.",
                    hor_force_approach_slab,
                    hor_force_vehicle,
                    P3_total_hor_force);
                double D10 = H / 2.0;
                sw.WriteLine();

                sw.WriteLine("D10 = H / 2.0 = {0} / 2.0 = {1} m",
                    H,
                    D10);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Verticle Force due to Vehicle Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //d3
                double w8 = (h2 * gamma_b + d3 * gamma_c) * L1;
                sw.WriteLine("w8 = (h2 * γ_b + d3 * γ_c) * L1;");
                sw.WriteLine("   = ({0:f2} * {1} + {2:f2} * {3}) * {4:f2}",
                    h2, gamma_b, d3, gamma_c, L1);
                sw.WriteLine("   = {0:f2} kN/m", w8);

                double D11 = (L1 / 2) + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("D11 = (L1 / 2) + L2 + L3");
                sw.WriteLine("    = ({0:f2} / 2) + {1:f2} + {2:f2}",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("    = {0:f2} m.",
                    D11);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("________________________________________________________________________________");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                    "", "", "V", "H", "DISTANCE", "Mv", "Mh");
                sw.WriteLine("________________________________________________________________________________");

                double Mv1 = (w1 * D1);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                   "1.",
                                   "Self Weight (w1)",
                                   w1.ToString("0.000"),
                                   "",
                                   "D1=" + D1.ToString("0.00"),
                                   Mv1.ToString("0.000"),
                                   "");

                double Mv2 = (w2 * D2);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                      "2.",
                                      "Self Weight (w2)",
                                      w2.ToString("0.000"),
                                      "",
                                      "D2=" + D2.ToString("0.00"),
                                      Mv2.ToString("0.000"),
                                      "");

                double Mv3 = (w3 * D3);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                   "3.",
                                   "Self Weight (w3)",
                                   w3.ToString("0.000"),
                                   "",
                                   "D3=" + D3.ToString("0.00"),
                                   Mv3.ToString("0.000"),
                                   "");

                double Mv4 = (w4 * D4);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                   "4.",
                                   "Weight of ",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "");
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                   "Earth on Heel Slab (w4)",
                                                   w4.ToString("0.000"),
                                                   "",
                                                   "D4=" + D4.ToString("0.00"),
                                                   Mv4.ToString("0.000"),
                                                   "");

                double Mv5 = (w5 * D5);
                sw.WriteLine("{0,5}{1,-27}{2,15}{3,13}{4,13}{5,13}{6,13}",
                                   "5.",
                                   "Permanent Load from ",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "");
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Super Structure (w5)",
                                                  w5.ToString("0.000"),
                                                  "",
                                                  "D5=" + D5.ToString("0.00"),
                                                  Mv5.ToString("0.000"),
                                                  "");

                double Mh1 = (P1 * D9);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                   "6.",
                                   "Active Earth Pressure (P1)",
                                   "",
                                   P1.ToString("0.000"),
                                   "D9=" + D9.ToString("0.00"),
                                   "",
                                   Mh1.ToString("0.000"));

                double Mv7 = (w8 * D11);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                   "7.",
                                   "Vertical Load for ",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "");
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                   "Vehicle Load Surcharge (w8)",
                                                   w8.ToString("0.000"),
                                                   "",
                                                   "D11=" + D11.ToString("0.00"),
                                                   Mv7.ToString("0.000"),
                                                   "");

                double Mh2 = (P3_total_hor_force * D10);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                   "8.",
                                   "Horizontal Force for ",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "");

                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                   "Vehicle Load Surcharge (P3)",
                                                   "",
                                                   P3_total_hor_force.ToString("0.000"),
                                                   "D10=" + D10.ToString("0.00"),
                                                   "",
                                                   Mh2.ToString("0.000"));

                sw.WriteLine("________________________________________________________________________________");
                double V1 = w1 + w2 + w3 + w4 + w5 + w8;
                double H1 = P1 + P3_total_hor_force;
                double MV1_SUM = Mv1 + Mv2 + Mv3 + Mv4 + Mv5 + Mv7;
                double MH1_SUM = Mh1 + Mh2;
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "Sum",
                                                  " of Items in",
                                                   "V2=",
                                                  "H2=",
                                                  "",
                                                  "MV2=",
                                                  "MH2=");
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                    "",
                                                   "Span Unloaded Condition",
                                                   "" + V1.ToString("0.00"),
                                                   "" + H1.ToString("0.00"),
                                                   "",
                                                   "" + MV1_SUM.ToString("0.00"),
                                                   "" + MH1_SUM.ToString("0.00"));
                sw.WriteLine("________________________________________________________________________________");
                double Mh3 = (P2 * d7);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "9.",
                                                   "Horizontal ",
                                                   "",
                                                   "",
                                                   "",
                                                   "",
                                                   "");

                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                   "Bracking Force (P2)",
                                                   "",
                                                   P2.ToString("0.000"),
                                                   "D7=" + d7.ToString("0.00"),
                                                   "",
                                                   Mh3.ToString("0.000"));

                double Mv8 = (w7 * D8);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "10.",
                                                   "Verticle",
                                                   "",
                                                   "",
                                                   "",
                                                   "",
                                                   "");

                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                   "Bracking Force (w7)",
                                                   w7.ToString("0.000"),
                                                   "",
                                                   "D8=" + D8.ToString("0.00"),
                                                   Mv8.ToString("0.000"),
                                                   "");

                double Mv9 = (w6 * D6);
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "11.",
                                                   "Vehicle Load from ",
                                                   "",
                                                   "",
                                                   "",
                                                   "",
                                                   "");

                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                                  "",
                                                                  "Super Structure (w6)",
                                                                  w6.ToString("0.000"),
                                                                  "",
                                                                  "D6=" + D6.ToString("0.00"),
                                                                  Mv9.ToString("0.000"),
                                                                  "");

                sw.WriteLine("________________________________________________________________________________");
                double V2 = V1 + w7 + w6;
                double H2 = H1 + P2;
                double MV2_SUM = MV1_SUM + Mv8 + Mv9;
                double MH2_SUM = MH1_SUM + Mh3;
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "Sum",
                                                  " of Items in",
                                                  "V2=",
                                                  "H2=",
                                                  "",
                                                  "MV2=",
                                                  "MH2=");
                 sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                   "Span Loaded Condition",
                                                   "" + V2.ToString("0.00"),
                                                   "" + H2.ToString("0.00"),
                                                   "",
                                                   "" + MV2_SUM.ToString("0.00"),
                                                   "" + MH2_SUM.ToString("0.00"));

                sw.WriteLine("________________________________________________________________________________");
                sw.WriteLine("________________________________________________________________________________");

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Check for Stability against Overturning");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("CASE I : Span Loaded Condition");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Overturning Moment about toe = MH2 = {0} kN-m.", MH2_SUM.ToString("0.000"));
                sw.WriteLine("Restoring Moment about toe = MV2 = {0} kN-m.", MV2_SUM.ToString("0.000"));
                sw.WriteLine();

                double safety_factor = MV2_SUM / MH2_SUM;
                sw.WriteLine("Factor of Safety against overturning = MV2/MH2");

                if (safety_factor > 2.0)
                {
                    sw.WriteLine("                                     = {0}/{1} = {2} > 2.0 , OK",
                        MV2_SUM.ToString("0.000"),
                        MH2_SUM.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                }
                else
                {

                    sw.WriteLine("                                     = {0}/{1} = {2} < 2.0, NOT OK",
                        MV2_SUM.ToString("0.000"),
                        MH2_SUM.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                    sw.WriteLine("Increase the Length of base wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }

                sw.WriteLine();
                sw.WriteLine("Location of Resultant from toe");
                double Xo = (MV2_SUM - MH2_SUM) / V2;
                sw.WriteLine("Xo = (MV2 - MH2)/V2 = ({0} - {1})/{2} = {3} m",
                    MV2_SUM.ToString("0.000"),
                    MH2_SUM.ToString("0.000"),
                    V2.ToString("0.000"),
                    Xo.ToString("0.000"));

                double emax = (L1 + L2 + L3) / 6.0;
                sw.WriteLine();
                sw.WriteLine("Maximum permissible Eccentricity  = (L1 + L2 + L3)/6.0");
                sw.WriteLine("                                  = ({0} + {1} + {2})/6.0",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("                                  = {0} = emax", emax.ToString("0.000"));

                double e1 = (L1 + L2 + L3) / 2 - Xo;

                sw.WriteLine();
                sw.WriteLine("Eccentricity of Resultant = (L1 + L2 + L3)/2 - Xo");
                sw.WriteLine("                          = ({0} + {1} + {2})/2 - {3:f2}",
                    L1,
                    L2,
                    L3,
                    Xo);
                if (e1 < emax)
                    sw.WriteLine("                      e1  = {0} < {1}(emax) , OK", e1.ToString("0.00"), emax.ToString("0.000"));
                else
                {
                    sw.WriteLine("                      e1  = {0} > {1}(emax), NOT OK", e1.ToString("0.00"), emax.ToString("0.000"));
                    sw.WriteLine("Increase the length of base of wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("CASE II : Span Unloaded Condition");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Overturning Moment about toe = {0}", MH1_SUM.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Restoring Moment about toe   = {0}", MV1_SUM.ToString("0.00"));

                safety_factor = MV1_SUM / MH1_SUM;
                sw.WriteLine();
                if (safety_factor >= 2.0)
                {
                    sw.WriteLine("Factor of Safety against overturning = {0} / {1} = {2} > 2.0 , OK",
                        MV1_SUM.ToString("0.00"),
                        MH1_SUM.ToString("0.00"),
                        safety_factor.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against overturning = {0} / {1} = {2} < 2.0, NOT OK",
                        MV1_SUM.ToString("0.00"),
                        MH1_SUM.ToString("0.00"),
                        safety_factor.ToString("0.00"));
                }

                sw.WriteLine();
                sw.WriteLine("Location of Resultant for toe");
                Xo = (MV1_SUM - MH1_SUM) / V1;
                sw.WriteLine("Xo = (MV1 - MH1)/V1 = ({0} - {1})/{2} = {3}",
                    MV1_SUM.ToString("0.000"),
                    MH1_SUM.ToString("0.000"),
                    V1.ToString("0.000"),
                    Xo.ToString("0.000"));

                emax = (L1 + L2 + L3) / 6.0;
                sw.WriteLine();
                sw.WriteLine("Maximum permissible Eccentricity  = (L1 + L2 + L3)/6.0");
                sw.WriteLine("                                  = ({0} + {1} + {2})/6.0",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("                                  = {0} = emax", emax.ToString("0.000"));

                double e2 = (L1 + L2 + L3) / 2 - Xo;

                sw.WriteLine();
                sw.WriteLine("Eccentricity of Resultant = (L1 + L2 + L3)/2 - Xo");
                sw.WriteLine("                          = ({0} + {1} + {2})/2 - {3:f2}",
                    L1,
                    L2,
                    L3,
                    Xo);
                if (e2 < emax)
                    sw.WriteLine("                      e2  = {0} < {1}(emax) , OK", e2.ToString("0.000"), emax.ToString("0.000"));
                else
                {
                    sw.WriteLine("                      e2  = {0} > {1}(emax), NOT OK", e2.ToString("0.000"), emax.ToString("0.000"));
                    sw.WriteLine("Increase the length of base of wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }



                #endregion


                #region STEP 4

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 :Check for Stresses at Base ");
                sw.WriteLine("        For Span Loaded Condition");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total downward forces = V2 = {0:f3} kN", V2);
                sw.WriteLine("Extreme Stresses at Base = (V2 / ((L1+L2+L3)*1.0)) *( 1.0 ± ((6 * e1)/(L1+L2+L3)))");
                sw.WriteLine("                         = ({0} / {1}*1.0) *( 1.0 ± ((6 * {2})/{1}))",
                    V2.ToString("0.000"),
                    provided_l,
                    e1.ToString("0.00"));

                double val1, val2, Pr1, Pr5;
                val1 = V2 / provided_l;
                val2 = (6 * e1) / provided_l;

                //p2
                Pr1 = val1 * (1 + val2);
                Pr5 = val1 * (1 - val2);


                sw.WriteLine("                         = {0} * (1 ± {1})", val1.ToString("0.000"), val2.ToString("0.000"));
                sw.WriteLine("                         = {0} = p1 and {1} = p2",
                    Pr1.ToString("0.000"),
                    Pr5.ToString("0.000"));

                if (Pr1 < 350)
                {
                    sw.WriteLine("p1 < 350 kN = Bearing Capacity, OK");
                }
                else
                {
                    sw.WriteLine("p1 > 350 kN = Bearing Capacity, NOT OK");
                }
                if (Pr5 >= 0)
                {
                    sw.WriteLine("p2 > 0 = No Tension, OK");
                }
                else
                    sw.WriteLine("p2 < 0 = No Tension, NOT OK");



                #endregion


                #region STEP 5
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5");
                sw.WriteLine("Check for Sliding");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                
                sw.WriteLine("Sliding Force = H2 = {0} kN", H2.ToString("0.000"));
                double FF = mu * V2;
                sw.WriteLine();
                sw.WriteLine("Force resisting Sliding = µ * V2 = {0} * {1} = {2} = FF",
                    mu.ToString("0.00"),
                    V2.ToString("0.00"),
                    FF.ToString("0.000"));
                safety_factor = FF / H2;
                sw.WriteLine();
                if (safety_factor > 1.5)
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2 = {0}/{1} = {2} > 1.5 , OK",
                        FF.ToString("0.000"),
                        H2.ToString("0.000"),
                        safety_factor.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2 = {0}/{1} = {2} < 1.5 , NOT OK",
                        FF.ToString("0.000"),
                        H2.ToString("0.000"),
                        safety_factor.ToString("0.00"));
                    sw.WriteLine("Shear key will be required.");
                }
                #endregion


                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Reinforcement Steel Bars.");
                sw.WriteLine("Design of Base Slab at Front Toe for Steel requirement.");
                sw.WriteLine("------------------------------------------------------------");
                
                sw.WriteLine();
                sw.WriteLine("Thickness of Base Slab = d4 = {0:f3} m", d4);

                double deff = d4 - (cover / 1000);
                sw.WriteLine("Deff = d4 - cover = {0:f3} - {1:f3} = {2:f3} m",
                    d4,
                    (cover/1000),
                    deff);

                double Pr2 = ((Pr1 - Pr5) / provided_l) * (provided_l - (L3 - deff));
                Pr2 += Pr5;
                double Pr3 = ((Pr1 - Pr5) / provided_l) * (L1 + L2);
                Pr3 += Pr5;
                double Pr4 = ((Pr1 - Pr5) / provided_l) * (L1);
                Pr4 += Pr5;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("ON BASE :");
                sw.WriteLine();
                sw.WriteLine("Pr1 = Upward pressure at Toe = {0} kN/sq.m.", Pr1.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Pr2 = Upward Pressure at a distance of effective depth from Front of wall");
                sw.WriteLine("    = {0:F3} kN/sq.m.",
                    Pr2);
                sw.WriteLine();
                sw.WriteLine("Pr3 = Upward Pressure at The Front Face of wall = {0:f3} kN/sq.m.",
                                    Pr3);

                sw.WriteLine();
                sw.WriteLine("Pr4 = Upward Pressure at The Backfill Face of wall = {0:f3} kN/sq.m.",
                                    Pr4);
                sw.WriteLine();
                sw.WriteLine("Pr5 = Upward Pressure at Heel = {0:f3} kN/sq.m.",
                                                    Pr5);
                double Dpr = d4 * gamma_c;
                sw.WriteLine();
                sw.WriteLine("Dpr = downward Pressure by Self weight of Base = {0:f2} * {1} = {2:f3} kN/sq.m.",
                    d4,
                    gamma_c,
                    Dpr);

                double Vu = factor * (((Pr1 + Pr2) / 2) - Dpr) * (L3 - deff);
                sw.WriteLine();
                sw.WriteLine("Design Shear Force ");
                sw.WriteLine("   = Vu = Shear Force Factor * [((Pr1 * Pr2) / 2) - Dpr) * (L3 - deff)");
                sw.WriteLine("   = {0:f2} * [(({1:f3} * {2:f3}) / 2) - {3:f3}) * ({4:f3} - {5:f2})",
                    factor,
                    Pr1,
                    Pr2,
                    Dpr,
                    L3,
                    deff);
                sw.WriteLine("                        = {0:f3}", Vu);
                double Mu = factor * (((L3 - deff) * Pr1 * L3 * L3 * 0.67 +
                    (L3 - deff) * Pr2 * L3 * L3 * (L3 - deff) - Dpr * L3 * L3 * (L3 - deff)));

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment");
                sw.WriteLine("  = Mu = Bending Moment Factor * [(L3-deff) * Pr1 * L3 * L3 * 0.67 + ");
                sw.WriteLine("        (L3-deff) * Pr2 * L3 * L3 * (L3-deff) - Dpr * L3 * L3 * (L3 - deff)]");

                sw.WriteLine("       = {0:f2} * [({1:f2}-{2:f2}) * {3:f3} * {1:f3} * {1:f3} * 0.67 + ",
                    factor,
                    L3,
                    deff,
                    Pr1);

                sw.WriteLine("        ({0:f2}-{1:f2}) * {2:f2} * {0:f2} * {0:f2} * ({0:f2}-{1:f2}) - {3:f2} * {0:f2} * {0:f2} * ({0:f2} - {1:f2})]",

                    L3,
                    deff,
                    Pr2,
                    Dpr);


                sw.WriteLine();
                sw.WriteLine("      = {0:f3} kN-m.", Mu);

                double b = 1000;

                double eff_depth = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Effective Depth of Base Slab");
                sw.WriteLine("         = √((Mu * 10E5)/(0.138*σ_c*b))");
                sw.WriteLine("         = √(({0:f2} * 10E5)/(0.138*{1}*{2}))",
                    Mu,
                    f_ck,
                    b);
                if (eff_depth < deff*1000)
                {
                    sw.WriteLine("                = {0:f3} < 700 , Provided Deff , OK.",
                        eff_depth);
                }
                else
                {
                    sw.WriteLine("                = {0:f3} > 700 , Provided Deff, NOT OK.",
                        eff_depth);
                }

                sw.WriteLine();
                sw.WriteLine("Provide Base Thick {0:f2} mm", (d4 * 1000));
                sw.WriteLine();
                sw.WriteLine("Area of Steel required at bottom Base slab at Toe");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Mu = 0.87 * f_y * Ast * [d - (f_y * Ast)/(f_ck * b)]");
                sw.WriteLine("{0:f2} * 10E5 = 0.87 * {1:f3} * Ast * [{2:f2} - ({1:f2} * Ast)/({3:f2} * {4:F2})]",
                    Mu,
                    f_y,
                    deff * 1000,
                    f_ck,
                    b);
                double _a, _b, _c, _d, _Mu;

                _Mu = Mu;

                _a = 0.87 * f_y;
                _b = (f_y / (f_ck * b));

                _c = _a * deff*1000;
                _d = _a * _b;

                _b = _c / _d;

                _c = _Mu / _d;

                sw.WriteLine();
                sw.WriteLine("Ast * Ast - {0:f2} * Ast + {1:f2} * 10E5 = 0",
                    _b,
                    _c);

                double Ast1, Ast2;
                sw.WriteLine();
                sw.WriteLine("Ast = ({0:f2} ± √({0:f2}*{0:f2} - 4*{1:f3}))/2",
                    _b,
                    _c);

                _d = Math.Sqrt((_b * _b - 4 * _c * 10E5));
                sw.WriteLine("    =  ({0:f2} ± {1:f2})/2",
                    _b,
                    _d);

                Ast1 = (_b + _d) / 2;
                Ast2 = (_b - _d) / 2;

                sw.WriteLine("    = {0:f2}, {1:f2}", Ast1, Ast2);

                double Ast_provided = Math.PI * 20 * 20 / 4;
                int no_bar = 1000 / 200;

                double bar_dia = 15;
                do
                {
                    bar_dia += 5;
                    if (bar_dia == 30) bar_dia += 2;
                    Ast_provided = Math.PI * bar_dia * bar_dia / 4;
                    no_bar = 1000 / 200;
                    Ast_provided = Ast_provided * no_bar;
                }
                while (Ast_provided < Ast2);
                sw.WriteLine();
                sw.WriteLine("Provided T{0:f0} bars @ 200 mm c/c at bottom of Base Slab at Toe", bar_dia);

                _bd4 = bar_dia;
                _sp4 = 200;

                sw.WriteLine();
                sw.WriteLine("Provided Provided Ast = {0:f2} sq.mm.", Ast_provided);

                double Pst = Ast_provided * 200 / (b * deff * 1000);
                sw.WriteLine();
                sw.WriteLine("Percentage of Tension Steel = Pst = {0:f2}%",
                    Pst);

                double tau_c = iApp.Tables.Permissible_Shear_Stress(Pst, (CONCRETE_GRADE)(int) f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine("Allowable Shear Stress = τ_c = {0:f2} IS 456 : 2000", tau_c);

                sw.WriteLine();
                double tau_v = Vu * 10E2 / (b * deff * 1000);
                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("Applied Shear Stress τ_v = Vu/b*d = {0:f3} * 10E2/({1:f2}*{2:f2}) = {3:f2} < {4:f2} , OK",
                    Vu,
                    b,
                    deff * 1000,
                    tau_v,
                    tau_c);

                }
                else
                {
                    sw.WriteLine("Applied Shear Stress τ_v = Vu/b*d = {0:f3} * 10E2/({1:f2}*{2:f2}) = {3:f2} > {4:f2}, NOT OK",
                        Vu,
                        b,
                        deff * 1000,
                        tau_v,
                        tau_c);
                }
                double dist_steel = 0.12 / 100 * b * deff * 1000;
                sw.WriteLine();
                sw.WriteLine("Distribution Steel = 0.12/100 * {0} * {1} = {2:f2} sq.mm.",
                    b,
                    deff,
                    dist_steel);

                sw.WriteLine();
                sw.WriteLine("Provide T10 @ 90 mm c/c");
                _bd3 = 10;
                _sp3 = 90;

                _bd5 = 10;
                _sp5 = 90;

                Ast_provided = Math.PI * 10 * 10 / 4;
                no_bar =(int) (1000.0 / 90.0);
                Ast_provided = Ast_provided * no_bar;
                sw.WriteLine("Ast Provided = {0:f2} sq.mm", Ast_provided);

                #endregion


                #region STEP 7
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : Design of Base Slab at Backfill Heel");
                sw.WriteLine("         Heel Side for Steel Reinforcements");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Upward Pressure varies from Pr3 = {0:f2} to Pr5 = {1:f2}",
                    Pr3,
                    Pr5);
                sw.WriteLine("downward Pressure is Earth Load + Surcharge + Self Weight");

                double Pr6 = (H - d4) * gamma_b + h2 * gamma_b + d3*gamma_c + d4 * gamma_c;
                sw.WriteLine("             Pr6 = (H-d4)*γ_b + h2 * γ_b  + d3*γ_c + d4*γ_c");
                sw.WriteLine("                 = ({0:f3}-{1:f2})*{2:f2} + {3:f2} * {2:f2}  + {4:f2}*{5:f2} + {1:f3}*{5:f3}",
                    H,
                    d4,
                    gamma_b,
                    h2,
                    d3,
                    gamma_c);
                sw.WriteLine("                 = {0:f2}", Pr6);

                sw.WriteLine();
                sw.WriteLine("Here downward pressure Pr6 = {0:f2} is more than Pr4 = {1:f2} and Pr5 = {2:f2}",
                    Pr6, Pr4, Pr5);//? Pr3 = 157 and Pr6 = 135 , How
                sw.WriteLine();
                sw.WriteLine("So, tension reinforcement steel will be required at the top");
                
                Vu = factor * (Pr6 * L1 - 0.5 * Pr4 * L1 - 0.5 * Pr5 * L1);


                sw.WriteLine();
                sw.WriteLine("Design Shear Force");
                sw.WriteLine("           Vu = Shear Force Factor * (Pr6 * L1 - 0.5 * Pr4 * L1 - 0.5 * Pr5 * L1)");
                sw.WriteLine("              = {0:f2} * ({1:f2} * {2:f2} - 0.5 * {3:f3} * {2:f3} - 0.5 * {4:f3} * {2:f3})",
                    factor,
                    Pr6,
                    L1,
                    Pr4,
                    Pr5);
                
                sw.WriteLine("              = {0:f2} kN", Vu);

                Mu = factor * (Pr6 * L1 * L1 * 0.5 - 0.5 * Pr4 * L1 * L1 * 0.33 - 0.5 * Pr5 * L1 * L1 * 0.67);

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment ");
                sw.WriteLine("      Mu = Bending Moment Factor * (Pr6 * L1 * L1 * 0.5 - 0.5 * Pr4 * L1 * L1 * 0.33 - 0.5 * Pr5 * L1 * L1 * 0.67)");
                sw.WriteLine("         = {0:f2} * ({1:f2} * {2:f3} * {2:f3} * 0.5 - 0.5 * {3:f3} * {1:f3} * {1:f3} * 0.33 - 0.5 * {4:f3} * {2:f3} * {2:f3} * 0.67)",
                    factor,
                    Pr6,
                    L1,
                    Pr4,
                    Pr5);
                sw.WriteLine();
                sw.WriteLine("         = {0:f2} kN-m",Mu);


                eff_depth = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine();
                sw.WriteLine("Effective Depth of Base Slab at Heel = √((Mu * 10E5)/(0.138*σ_c*b))");
                sw.WriteLine("                                     = √(({0:f2} * 10E5)/(0.138*{1:f2}*{2:f2}))",
                    Mu,
                    f_ck, b);

                if (eff_depth < (deff * 1000))
                    sw.WriteLine("                                     = {0:f2} mm < {1:f2}", eff_depth,
                        (deff * 1000));

                sw.WriteLine();
                sw.WriteLine("Area of Steel required at top of base slab at Heel");
                //σ_st

                sw.WriteLine();
                sw.WriteLine("  Mu = 0.87 * σ_st * Ast * (d-((σ_st*Ast)/(σ_c*b))");
                sw.WriteLine();
                sw.WriteLine("  {0:f2}*10E5 = 0.87 * {1:f2} * Ast * ({2:f2}-(({1:f2}*Ast)/({3}*{4}))",
                    Mu,
                    f_y,
                    deff * 1000,
                    f_ck,
                    b);

                _c = 0.87 * f_y * deff * 1000;
                _d = (0.87 * f_y * f_y) / (f_ck * b);


                _b = _c / _d;

                sw.WriteLine();
                sw.WriteLine("Ast*Ast - {0:f2}*Ast + {1:f2} * 10E6 = 0",
                    _b, _c); 
                
                _c = (Mu / _d) * 10E5;
                _d = Math.Sqrt((_b * _b - 4 * _c));

                Ast1 = (_b + _d) / 2;
                Ast2 = (_b - _d) / 2;

                sw.WriteLine();
                sw.WriteLine("Ast = {0:f2} and {1:f2} sq.mm.", Ast1, Ast2);
                bar_dia = 15;
                do
                {
                    bar_dia += 5;
                    if (bar_dia == 30) bar_dia += 2;
                    Ast_provided = (Math.PI * bar_dia * bar_dia / 4) * 10;
                }
                while (Ast_provided < Ast2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @ 100 mm c/c at Top of bar slab at Heel.", bar_dia);

                _bd6 = bar_dia;
                _sp6 = 100;


                sw.WriteLine();
                sw.WriteLine("Provide Ast = {0:f2} sq.mm", Ast_provided);

                double percent = Ast_provided * 100 / (1000 * deff * 1000);

                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Allowable Shear Stress = τ_c = {0:f2} (IS 456:2000)", tau_c);

                tau_v = Vu * 1000 / (b * deff * 1000);

                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("Applied Shear Stress τ_v = ({0:f2} * 1000)/(1000*{1:f1}) = {2:f2} N/sq.mm. < τ_c , OK",
                        Vu,
                        deff*1000,
                        tau_v);
                }
                else
                {
                    sw.WriteLine("Applied Shear Stress τ_v = ({0:f2} * 1000)/(1000*{1:f1}) = {2:f2} N/sq.mm. > τ_c ,NOT OK",
                        Vu,
                        deff * 1000,
                        tau_v);
                }
                sw.WriteLine();
                sw.WriteLine("Distribution Steel = 0.12/100 * {0} * {1} = {2:f2} sq.mm.",
                    b,
                    deff,
                    dist_steel);

                sw.WriteLine();
                sw.WriteLine("Provide T10 @ 90 mm c/c");

                _bd7 = 10;
                _sp7 = 90;

                Ast_provided = Math.PI * 10 * 10 / 4;
                no_bar = (int)(1000.0 / 90.0);
                Ast_provided = Ast_provided * no_bar;
                sw.WriteLine();
                sw.WriteLine("Ast Provided = {0:f2} sq.mm", Ast_provided);
                
                #endregion


                #region STEP 8
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : Design of Wall Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At the bottom of the front face of the wall");
                sw.WriteLine("   Design Bending Moment ");
                sw.WriteLine("     = (1/6) * Ca * γ_b * H * H * H + (1/2) * Ca * γ_b * h1 * H * H");
                sw.WriteLine("     = (1/6) * {0:f2} * {1:f0} * {2:f2} * {2:f2} * {2:f2} + (1/2) * {0:f2} * {1:f0} * {3:f2} * {2:f2} * {2:f2}",
                    Ca,
                    gamma_b,
                    H,
                    h1);

                double deg_bend_mom = (1.0 / 6.0) * Ca * gamma_b * H * H * H + (1.0 / 2.0) * Ca * gamma_b * h2 * H * H;
                sw.WriteLine();
                sw.WriteLine("     = {0:f2} kN-m", deg_bend_mom);


                double deg_shear = Ca * gamma_b * h1 * H + 0.5 * Ca * gamma_b * H * H;
                sw.WriteLine();
                sw.WriteLine("   Design Shear ");
                sw.WriteLine("      = Ca * γ_b * h1 * H + 0.5 * Ca * γ_b * H * H");
                sw.WriteLine("      = {0:f2} * {1:f0} * {2:f2} * {3:f2} + 0.5 * {0:f2} * {1:f0} * {3:f2} * {3:f2}",
                    Ca,
                    gamma_b,
                    h1,
                    H);
                sw.WriteLine("                = {0:f2} kN", deg_shear);

                sw.WriteLine();
                sw.WriteLine();
                double fact_bend_mom = factor * deg_bend_mom;
                sw.WriteLine("   Mu = Factored Bending Moment = {0:f2} * {1:f2} = {2:f2} kN-m",
                    factor,
                    deg_bend_mom,
                    fact_bend_mom);

                double fact_shear_force = factor * deg_shear;
                sw.WriteLine("   Vu = Factored Shear Force = {0:f2} * {1:f2} = {2:f2} kN",
                                    factor,
                                    deg_shear,
                                    fact_shear_force);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Effective Thickness of wall at the base");
                _d = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine(" d = √((Mu * 10E5)/(0.138*σ_c*b))");
                sw.WriteLine("   = √(({0:f2} * 10E5)/(0.138*{1:f2}*{2}))",
                    Mu, f_ck, b);
                if (_d < 1000)
                {
                    sw.WriteLine("   = {0:f2} mm < 1000 mm , OK",
                        _d);
                }
                else
                {
                    sw.WriteLine("   = {0:f2} mm > 1000 mm, NOT OK",
                    _d);
                }
                //sw.WriteLine("Area of steel required = Ast = (0.36 * σ_c * b * 0.48 * (L2-cover))/(0.87 * σ_st)");
                sw.WriteLine();
                sw.WriteLine("Area of steel required = Ast = (0.36 * σ_c * b * 0.48 * d)/(0.87 * σ_st)");
                double _ast = (0.36 * f_ck * b * 0.48 * _d) / (0.87 * f_y);

                sw.WriteLine("                             = (0.36 * {0} * {1} * 0.48 * d)/(0.87 * {3})",
                    f_ck,
                    b,
                    _d,
                    f_y);
                sw.WriteLine("                             = {0:f2} sq.mm.", _ast);

                bar_dia = 15;


                do
                {
                    bar_dia += 5;
                    Ast_provided = (Math.PI * bar_dia * bar_dia / 4) * (1000 / 120);
                }
                while (Ast_provided < _ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @120 mm c/c", bar_dia);

                _bd1 = bar_dia;
                _sp1 = 120;


                sw.WriteLine("Provided Ast = {0:f2} sq.mm",
                    Ast_provided);

                percent = (Ast_provided * 100) / (b * (t*1000-cover));
                //percent = (Ast_provided * 100) / (b * _d);
                sw.WriteLine();
                sw.WriteLine("Percentage of Steel provided = p = {0:f2}*100/({1}*{2}) = {3:f2}%",
                    Ast_provided,
                    b,
                    (t * 1000 - cover),
                    percent);
                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine(" Allowable Shear Stress of Concrete = τ_c = {0:f2} N/sq.mm",
                    tau_c);
                tau_v = Vu * 1000 / (b * (t * 1000 - cover));

                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("Applied Shear Stress = τ_v = Vu/b*d = {0:f2} * 1000/({1} * {2}) = {3:f2} < τ_c , OK",
                        Vu,
                        b,
                        (t * 1000 - cover),
                        tau_v);
                }
                else
                {
                    sw.WriteLine("Applied Shear Stress = τ_v = Vu/b*d = {0:f2} * 1000/({1} * {2}) = {3:f2} N/sqmm > τ_c NOT , OK",
                    Vu,
                    b,
                    _d,
                    tau_v);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Distribution Steel for Temperature Reinforcements:");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Sectional Area of the wall");

                double total_sectional_area = (H - d4) * d3 + (H - d4 - d1) * deff;
                sw.WriteLine("    = (H - d4) * d3 + (H - d4 - h1) * deff");
                sw.WriteLine("    = ({0:f2} - {1:f2}) * {2:f2} + ({0:f2} - {1:f2} - {3:f2}) * {4:F2}",
                    H,
                    d4,
                    d3,
                    h1,
                    deff);
                sw.WriteLine("    = {0:f2} sq.m",
                    total_sectional_area);
                sw.WriteLine("    = {0:f2} sq.mm",
                    total_sectional_area * 1000000);

                _a = (0.12 / 100) * total_sectional_area * 100 * 10e3;

                sw.WriteLine();
                sw.WriteLine("Area of Temperature Steel  = 0.12% = {0:f2} sq.mm", _a);

                _ast = Math.PI * 100 / 4;
                no_bar = (int)(_a / _ast);
                sw.WriteLine();
                sw.WriteLine("Use 10 mm bars, Number of bars = {0:f2}/{1:f2} = {2} nos",
                    _a,
                    _ast,
                    no_bar);




                int front_bar = (int)(no_bar * (2.0 / 3.0));
                int back_fill_bar =(int) (no_bar * (1.0 / 3.0));
                sw.WriteLine();
                sw.WriteLine("Provide {0} bars horizontally on the Front face", front_bar);

                _c = (H - d1 - d4) * 1000 / front_bar;
                sw.WriteLine("    = (H - d1 - d4) * 1000 / {0}", front_bar);
                sw.WriteLine("    = ({0:f2} - {1:f2} - {2:f2}) * 1000 / {3}",
                    H,
                    h1,
                    d4,
                    front_bar);
                sw.WriteLine("    = {0:f2} mm", _c);

                _c = (int)(_c / 10.0);
                _c += 1.0;
                _c *= 10;
                sw.WriteLine("    ≈ {0:f2} mm", _c);




                sw.WriteLine();
                sw.WriteLine("Provide {0} bars horizontally on the Backfill side face", back_fill_bar);

                _c = (H - d4) * 1000 / back_fill_bar;
                sw.WriteLine("    = (H - d4) * 1000 / {0}",back_fill_bar);
                sw.WriteLine("    = ({0:f2} - {1:f2}) * 1000 / {2:f2}",
                    H,
                    d4,
                    back_fill_bar);
                sw.WriteLine("    = {0:f2} mm", _c);

                _c = (int)(_c / 10.0);
                _c += 1.0;
                _c *= 10;
                sw.WriteLine("    ≈ {0:f2} mm c/c", _c);

                _bd2 = 10;
                _sp2 = _c;
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

            d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
            t = MyList.StringToDouble(txt_t.Text, 0.0);
            H = MyList.StringToDouble(txt_H.Text, 0.0);
            a = MyList.StringToDouble(txt_a.Text, 0.0);
            gamma_b = MyList.StringToDouble(txt_gamma_b.Text, 0.0);
            gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
            phi = MyList.StringToDouble(txt_phi.Text, 0.0);
            p = MyList.StringToDouble(txt_p_bearing_capacity.Text, 0.0);
            f_ck = MyList.StringToDouble(txt_concrete_grade.Text, 0.0);
            f_y = MyList.StringToDouble(txt_steel_grade.Text, 0.0);
            w6 = MyList.StringToDouble(txt_w6.Text, 0.0);
            w5 = MyList.StringToDouble(txt_w5.Text, 0.0);
            F = MyList.StringToDouble(txt_F.Text, 0.0);
            d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_d3.Text, 0.0);
            B = MyList.StringToDouble(txt_B.Text, 0.0);
            theta = MyList.StringToDouble(txt_theta.Text, 0.0);
            delta = MyList.StringToDouble(txt_delta.Text, 0.0);
            z = MyList.StringToDouble(txt_z.Text, 0.0);
            mu = MyList.StringToDouble(txt_mu.Text, 0.0);
            L1 = MyList.StringToDouble(txt_L1.Text, 0.0);
            L2 = MyList.StringToDouble(txt_L2.Text, 0.0);
            L3 = MyList.StringToDouble(txt_L3.Text, 0.0);
            L4 = MyList.StringToDouble(txt_L4.Text, 0.0);
            h1 = MyList.StringToDouble(txt_h1.Text, 0.0);
            L = MyList.StringToDouble(txt_L.Text, 0.0);
            d4 = MyList.StringToDouble(txt_d4.Text, 0.0);
            cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            factor = MyList.StringToDouble(txt_fact.Text, 0.0);

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
            kPath = Path.Combine(kPath, "Underpass");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Vehicular Underpass");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of RCC Abutment");

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
                this.Text = "DESIGN OF ABUTMENT : " + value;
                user_path = value;


                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_ABUTMENT_UNDERPASS");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Underpass_Veh_Abutment.TXT");
                user_input_file = Path.Combine(system_path, "RCC_ABUTMENT_UNDERPASS.FIL");


                btnProcess.Enabled = Directory.Exists(value);
                btnDrawing.Enabled = File.Exists(user_input_file);
                btnSample_Drawing.Enabled = File.Exists(user_input_file);
                btnReport.Enabled = File.Exists(user_input_file);

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
                        case "d1":
                            d1 = mList.GetDouble(1);
                            txt_d1.Text = d1.ToString();
                            break;
                        case "t":
                            t = mList.GetDouble(1);
                            txt_t.Text = t.ToString();
                            break;
                        case "H":
                            H = mList.GetDouble(1);
                            txt_H.Text = H.ToString();
                            break;
                        case "a":
                            a = mList.GetDouble(1);
                            txt_a.Text = a.ToString();
                            break;
                        case "gamma_b":
                            gamma_b = mList.GetDouble(1);
                            txt_gamma_b.Text = gamma_b.ToString();
                            break;
                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "phi":
                            phi = mList.GetDouble(1);
                            txt_phi.Text = phi.ToString();
                            break;
                        case "p":
                            p = mList.GetDouble(1);
                            txt_p_bearing_capacity.Text = p.ToString();
                            break;
                        case "f_ck":
                            f_ck = mList.GetDouble(1);
                            txt_concrete_grade.Text = f_ck.ToString();
                            break;

                        case "f_y":
                            f_y = mList.GetDouble(1);
                            txt_steel_grade.Text = f_y.ToString();
                            break;

                        case "w6":
                            w6 = mList.GetDouble(1);
                            txt_w6.Text = w6.ToString();
                            break;

                        case "w5":
                            w5 = mList.GetDouble(1);
                            txt_w5.Text = w5.ToString();
                            break;

                        case "F":
                            F = mList.GetDouble(1);
                            txt_F.Text = F.ToString();
                            break;

                        case "d2":
                            d2 = mList.GetDouble(1);
                            txt_d2.Text = d2.ToString();
                            break;

                        case "d3":
                            d3 = mList.GetDouble(1);
                            txt_d3.Text = d3.ToString();
                            break;

                        case "B":
                            B = mList.GetDouble(1);
                            txt_B.Text = B.ToString();
                            break;


                        case "theta":
                            theta = mList.GetDouble(1);
                            txt_theta.Text = theta.ToString();
                            break;

                        case "delta":
                            delta = mList.GetDouble(1);
                            txt_delta.Text = delta.ToString();
                            break;

                        case "z":
                            z = mList.GetDouble(1);
                            txt_z.Text = z.ToString();
                            break;

                        case "mu":
                            mu = mList.GetDouble(1);
                            txt_mu.Text = mu.ToString();
                            break;

                        case "L1":
                            L1 = mList.GetDouble(1);
                            txt_L1.Text = L1.ToString();
                            break;

                        case "L2":
                            L2 = mList.GetDouble(1);
                            txt_L2.Text = L2.ToString();
                            break;

                        case "L3":
                            L3 = mList.GetDouble(1);
                            txt_L3.Text = L3.ToString();
                            break;

                        case "L4":
                            L4 = mList.GetDouble(1);
                            txt_L4.Text = L4.ToString();
                            break;

                        case "h1":
                            h1 = mList.GetDouble(1);
                            txt_h1.Text = h1.ToString();
                            break;

                        case "L":
                            L = mList.GetDouble(1);
                            txt_L.Text = L.ToString();
                            break;
                        case "d4":
                            d4 = mList.GetDouble(1);
                            txt_d4.Text = d4.ToString();
                            break;

                        case "cover":
                            cover = mList.GetDouble(1);
                            txt_cover.Text = cover.ToString();
                            break;

                        case "factor":
                            factor = mList.GetDouble(1);
                            txt_fact.Text = factor.ToString();
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
        void Write_User_input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region USER Data

                sw.WriteLine("d1 = {0} ", d1);
                sw.WriteLine("t = {0}", t);
                sw.WriteLine("H = {0}", H);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("d2 = {0}", d2);
                sw.WriteLine("d3 = {0}", d3);
                sw.WriteLine("L1 = {0}", L1);
                sw.WriteLine("L2 = {0}", L2);
                sw.WriteLine("L3 = {0}", L3);
                sw.WriteLine("L4 = {0}", L4);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("d4 = {0:f2}", d4);
                sw.WriteLine("theta = {0}", theta);
                sw.WriteLine("delta = {0}", delta);
                sw.WriteLine("z = {0}", z);
                sw.WriteLine("mu = {0}", mu);
                sw.WriteLine("gamma_b = {0}", gamma_b);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("phi = {0}", phi);
                sw.WriteLine("p = {0}", p);
                sw.WriteLine("f_ck = {0:f0}", f_ck);
                sw.WriteLine("f_y = {0:f0}", f_y);
                sw.WriteLine("w6 = {0:f2}", w6);
                sw.WriteLine("w5 = {0:f2}", w5);
                sw.WriteLine("F = {0:f2}", F);
                sw.WriteLine("factor = {0:f2}", factor);
                sw.WriteLine("cover = {0:f2}", cover);
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "Abutment_Reinforcements", "");
        }
        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "RCC_ABUTMENT_UNDERPASS_DRAWING.FIL");

            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                sw.WriteLine("_L1={0}", L1 * 1000);
                sw.WriteLine("_L2={0}", L2 * 1000);
                sw.WriteLine("_L3={0}", L3 * 1000);
                sw.WriteLine("_D={0}", ((L1 + L2 + L3) * 1000));
                sw.WriteLine("_d4={0}", d4 * 1000);
                sw.WriteLine("_H={0}", H * 1000);
                sw.WriteLine("_d1={0}", d1 * 1000);
                sw.WriteLine("_d3={0}", d3 * 1000);
                sw.WriteLine("_L4={0}", L4 * 1000);

                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_bd3={0}", _bd3);
                sw.WriteLine("_bd4={0}", _bd4);
                sw.WriteLine("_bd5={0}", _bd5);
                sw.WriteLine("_bd6={0}", _bd6);
                sw.WriteLine("_bd7={0}", _bd7);

                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_sp2={0}", _sp2);
                sw.WriteLine("_sp3={0}", _sp3);
                sw.WriteLine("_sp4={0}", _sp4);
                sw.WriteLine("_sp5={0}", _sp5);
                sw.WriteLine("_sp6={0}", _sp6);
                sw.WriteLine("_sp7={0}", _sp7);
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
            iApp.SetDrawingFile_Path(drawing_path, "", "Abutment_Sample");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
