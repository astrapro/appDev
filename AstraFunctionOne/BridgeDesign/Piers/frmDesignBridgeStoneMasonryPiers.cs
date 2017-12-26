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

namespace AstraFunctionOne.BridgeDesign.Design5
{
    public partial class frmDesignBridgePiers : Form
    {
        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string drawing_path = "";
        string user_input_file = "";
        bool is_process = false;
        

        #region Variable Declarion
        double w1, w2, e, b1, b2, l, h, HFL, w3, gamma_c;
        double f1, f2, A, F, V;

        List<double> lst_s = new List<double>();

        #endregion
        IApplication iApp = null;
        public frmDesignBridgePiers(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Calculate_Program(rep_file_name);
            Write_User_Input();
            Write_Drawing_File();
            if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            is_process = true;
            FilePath = user_path;
        }
        void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("w1 = {0:f3}", w1);
                sw.WriteLine("w2 = {0:f3}", w2);
                sw.WriteLine("e = {0:f3} ", e);
                sw.WriteLine("b1 = {0:f3} ", b1);
                sw.WriteLine("b2 = {0:f3} ", b2);
                sw.WriteLine("l = {0:f3}  ", l);
                sw.WriteLine("h = {0:f3} ", h);
                sw.WriteLine("HFL = {0:f3} ", HFL);
                sw.WriteLine("w3 = {0:f3} ", w3);
                sw.WriteLine("γ_c = {0:f3} ", gamma_c);
                sw.WriteLine("f1 = {0:f3} ", f1);
                sw.WriteLine("f2 = {0:f3} ", f2);
                sw.WriteLine("A = {0:f3} ", A);
                sw.WriteLine("F = {0:f3} ", F);
                sw.WriteLine("V = {0:f3} ", V);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "PIER_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                string _A = (b1 * 1000.0).ToString();
                string _B = (HFL * 1000).ToString();
                string _C = (b2 * 1000.0).ToString();
                string _D = (e * 1000.0).ToString();
                string _E = (h * 1000.0).ToString();
                string _F = (l * 1000.0).ToString();
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                //sw.WriteLine("_G={0}", _G);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        void Calculate_Program(string fileName)
        {
            //#region Variable Declarion
            //double w1, w2, e, b1, b2, l, h, HFL, w3, gamma_c;
            //double f1, f2, A, F, V;

            //List<double> lst_s = new List<double>();

            //#endregion

            #region Variable Initialization
            w1 = MyList.StringToDouble(txt_w1.Text,0.0);
            w2 = MyList.StringToDouble(txt_w2.Text,0.0);
            e = MyList.StringToDouble(txt_e.Text,0.0);
            b1 = MyList.StringToDouble(txt_b1.Text,0.0);
            b2 = MyList.StringToDouble(txt_b2.Text,0.0);
            l = MyList.StringToDouble(txt_l.Text,0.0);
            h = MyList.StringToDouble(txt_h.Text,0.0);
            HFL = MyList.StringToDouble(txt_HFL.Text,0.0);
            w3 = MyList.StringToDouble(txt_w3.Text,0.0);
            gamma_c = MyList.StringToDouble(txt_gamma_c.Text,0.0);
            f1 = MyList.StringToDouble(txt_f1.Text,0.0);
            f2 = MyList.StringToDouble(txt_f2.Text,0.0);
            A = MyList.StringToDouble(txt_A.Text,0.0);
            F = MyList.StringToDouble(txt_F.Text,0.0);
            V = MyList.StringToDouble(txt_V.Text,0.0);
            #endregion

            #region Write File
            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));

            #region USER INPUT DATA

            //sw.WriteLine("USER INPUT DATA");
            //sw.WriteLine("------------------------------------------------------------");

            //sw.WriteLine("w1 = {0:f3} ",w1);
            //sw.WriteLine("w2 = {0:f3} ",w2);
            //sw.WriteLine("e = {0:f3} ",e); 
            //sw.WriteLine("b1 = {0:f3} ",b1);
            //sw.WriteLine("b2 = {0:f3} ",b2);
            //sw.WriteLine("l = {0:f3} ",l); 
            //sw.WriteLine("h = {0:f3} ",h); 
            //sw.WriteLine("HFL = {0:f3} ",HFL);
            //sw.WriteLine("w3 = {0:f3} ",w3); 
            //sw.WriteLine("gamma_c = {0:f3} ",gamma_c); 
            //sw.WriteLine("f1 = {0:f3} ",f1); 
            //sw.WriteLine("f2 = {0:f3} ",f2); 
            //sw.WriteLine("A = {0:f3} ",A); 
            //sw.WriteLine("F = {0:f3} ",F); 
            //sw.WriteLine("V = {0:f3} ",V);
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine();

            #endregion


            #region TechSOFT Banner
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*                  ASTRA Pro                  *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*          DESIGN  OF  BRIDGE PIERS           *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            try
            {
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Permanent Load from each Span [w1] = {0:f3} kN", w1);
                sw.WriteLine("Live Load from each Span [w2] = {0:f3} kN", w2);
                sw.WriteLine("Acts at distance from Centre Line Pier [e] = {0:f3} m    Marked as D in the Drawing", e);
                sw.WriteLine("Width of Pier at Bottom [b1] = {0:f3} m                  Marked as A in the Drawing", b1);
                sw.WriteLine("Width of Pier at Top [b2] = {0:f3} m                     Marked as C in the Drawing", b2);
                sw.WriteLine("Length of Pier [l] = {0:f3} m                            Marked as F in the Drawing", l);
                sw.WriteLine("Height of Pier [h] = {0:f3} m                            Marked as E in the Drawing", h);
                sw.WriteLine("Height of high flood Level [HFL] = {0:f3} m              Marked as B in the Drawing", HFL);
                sw.WriteLine("Vehicle Load on each Span [w3] = {0:f3} kN", w3);
                sw.WriteLine("Unit weight of concrete  [γ_c] = {0:f3} kN/cu.m", gamma_c);
                sw.WriteLine("Frictional Coefficient of Left Side Bending [f1] = {0:f3} ", f1);
                sw.WriteLine("Frictional Coefficient of Right Side Bending [f2] = {0:f3} ", f2);
                sw.WriteLine("Area of Deck and Handrail in elevation [A] = {0:f3} sq.m", A);
                sw.WriteLine("Wind Force [F] = {0:f3} kN/sq.m", F);
                sw.WriteLine("Mean water current Velocity [V] = {0:f3} m/sec", V);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
               


                #region STEP 1


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF BRIDGE PIERS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Stresses due to Permanent Loads and Self weight.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permanent Load from Super Structure = 2 * w1 = 2 * {0:f2} = {1:f2} kN",
                    w1,
                    (2 * w1));

                double sefl_wt_pier = l * ((b1 + b2) / 2.0) * h * gamma_c;

                sw.WriteLine("Self weight of Pier = l * ((b1 + b2) / 2.0) * h * γ_c");
                sw.WriteLine("                    = {0:f2} * (({1:f2} + {2:f2}) / 2.0) * {3:f2} * {4:f2}",
                    l, b1, b2, h, gamma_c);
                sw.WriteLine("                    = {0:f3} kN", sefl_wt_pier);

                //w3 = 2 * w1 + sefl_wt_pier;
                double _w3 = 2 * w1 + sefl_wt_pier;

                sw.WriteLine("  Total Direct Load = w3 =  {0:f2} + {1:f2} = {2:f3} kN",
                    (2 * w1), sefl_wt_pier, _w3);

                double stress_base_pier = _w3 / (b1 * l);
                sw.WriteLine("Stress at the base of Pier = w3 / (b1 * l) = {0:f2} / ({1:f2} * {2:f2})",
                    _w3, b1, l);
                sw.WriteLine("                           = {0:f3}", stress_base_pier);

                lst_s.Add(stress_base_pier);

                #endregion

                #region STEP 2
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Effect of Buoyancy");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double b3 = (((b1 - b2) / (2 * h)) * (h - HFL)) * 2 + b2;
                sw.WriteLine("Width of HFL = b3 = (((b1 - b2) / (2 * h)) * (h - HFL)) * 2 + b2");
                sw.WriteLine("                  = ((({0:f2} - {1:f2}) / (2 * {2:f2})) * ({2:f2} - {3:f3})) * 2 + {4:f2}",
                    b1, b2, h, HFL, b2);
                sw.WriteLine("                  = {0:f3} m", b3);
                sw.WriteLine();

                double submerged_vol = l * ((b3 + b1) / 2) * HFL;
                sw.WriteLine("Submerged volume of Pier = l * ((b3 + b1) / 2) * HFL");
                sw.WriteLine("                         = {0:f2} * (({1:f2} + {2:f2}) / 2) * {3:f2}",
                    l,
                    b3,
                    b1,
                    HFL);
                sw.WriteLine("                         = {0:f2} cu.m", submerged_vol);

                sw.WriteLine();
                double reduction_wt = submerged_vol * 10.0;
                sw.WriteLine("Reduction in Weight of Pier for buoyancy = {0:f2} * 10 = {1:f2} kN", submerged_vol,
                    reduction_wt);

                double stress_base_pier_buoyancy = -(reduction_wt / (l * b1));
                sw.WriteLine("Stress at base due to buoyancy = -{0:f2}/l*b1 ", reduction_wt);
                sw.WriteLine("                               = -{0:f2}/{1:f2}*{2:f2}", reduction_wt, l, b1);
                sw.WriteLine("                               = {0:f2} kN/sq.m ", stress_base_pier_buoyancy);

                lst_s.Add(stress_base_pier_buoyancy);
                sw.WriteLine();

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Stress due to eccentricity of Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double Ml = w2 * e;
                sw.WriteLine("Bending Moment due to eccentricity of Live Load = Ml = w2 * e");
                sw.WriteLine("                                                = {0:f2} * {1:f2}", w2, e);
                sw.WriteLine("                                                = {0:f2} kN-m", Ml);
                sw.WriteLine();

                double Zt = l * b1 * b1 / 6;

                sw.WriteLine(" Section Modulus of Pier at Base about transverse axis");
                sw.WriteLine("  (Axis at right angle to the direction of Traffic)");
                sw.WriteLine("    = Zt = l * b1 * b1 / 6");
                sw.WriteLine("    = {0:f2} * {1:f2} * {1:f2} / 6", l, b1);
                sw.WriteLine("    = {0:f2} cu.m.", Zt);
                sw.WriteLine();

                sw.WriteLine("Stress at base for eccentric Live Load = (w2/(l*b1) ± (Ml/Zt)");


                double val1, val2, val3, val4;
                val1 = w2 / (l * b1);
                val2 = Ml / Zt;

                val3 = val1 + val2;
                val4 = val1 - val2;

                sw.WriteLine("                                       = ({0:f2}/({1:f2}*{2:f2}) ± ({3:f2}/{4:f2})",
                    w2,
                    l, b1, Ml, Zt);
                sw.WriteLine("                                       = {0:f2} ± {1:f2}", val1, val2);
                sw.WriteLine("                                       = {0:f2} or {1:f2}", val3, val4);
                lst_s.Add(val3);




                #endregion


                #region STEP 4 : Stress due to Longitudinal Forces

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Stress due to Longitudinal Forces");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine(" i) Stresses due to braking Force ");
                sw.WriteLine("    Vehicle Load = w3 = {0:f2} kN", w3);
                double w4 = 0.2 * w3;
                sw.WriteLine();
                sw.WriteLine("    Horizontal Force for Braking = w4 = 0.2 * w3 = 0.2 * {0:f2} = {1:f2} kN",
                    w3,
                    w4);

                double M = w4 * h;

                sw.WriteLine();
                sw.WriteLine("    Bending Moment at base of Pier = M = w4 * h = {0:f2} * {1:f2}", w4, h);
                sw.WriteLine("                                   = {0:f2} kN-m", M);
                sw.WriteLine();

                double stress_base_braking_force = (M / Zt);
                sw.WriteLine("Stress at base due to braking force = ±(M/Zt) = ±({0:f2}/{1:f2}) ", M, Zt);
                sw.WriteLine("                                    = ±{0:f2} kN/sq.m ", stress_base_braking_force);

                lst_s.Add(stress_base_braking_force);


                sw.WriteLine();
                sw.WriteLine(" ii) Stresses due to resistance in bearings against movement due to temperature.");
                sw.WriteLine();
                sw.WriteLine("Applied Permanent Load on Bearing = w1 = {0:f2} kN", w1);
                sw.WriteLine("Applied Live Load on Bearing  = w2 = {0:f2} kN", w2);
                sw.WriteLine("For wrost effect let us assume Live Load on Left Span only");
                sw.WriteLine();

                double left_side_force = f1 * (w1 + w2);
                sw.WriteLine("Force of resistance by left side bearing = f1*(w1+w2)");
                sw.WriteLine("                                         = {0:f2}*({1:f2}+{2:f2})", f1, w1, w2);
                sw.WriteLine("                                         = {0:f2} kN", left_side_force);
                sw.WriteLine();

                double right_side_force = f2 * w1;
                sw.WriteLine("Force of resistance by right side bearing = f2*w1");
                sw.WriteLine("                                         = {0:f2}*{1:f2}", f2,w1);
                sw.WriteLine("                                         = {0:f2} kN", right_side_force);
                sw.WriteLine();

                val1 = Math.Max(left_side_force, right_side_force);
                val2 = Math.Min(left_side_force, right_side_force);
                double unbalanced_force_bearing = (val1 - val2);
                sw.WriteLine("Unbalanced force at bearing = {0:f2} - {1:f2} = {2:f2} kN",
                    val1, val2,unbalanced_force_bearing);
                M = unbalanced_force_bearing * h;
                sw.WriteLine();
                sw.WriteLine("Bending Momnet at base = M = {0:f2} * h = {0:f2} * {1:f2} = {2:f2} kN/sq.m",
                    unbalanced_force_bearing,
                    h,
                    M);
                double stress_at_base = (M / Zt);
                sw.WriteLine();
                sw.WriteLine("Stresses at base = ±(M/Zt) = ±({0:f2}/{1:f2}) = ±{2:f2} kN/sq.m",
                    M,
                    Zt,
                    stress_at_base);
                lst_s.Add(stress_at_base);
                #endregion

                #region STEP 5
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Stresses due to wind Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                
                double wind_force = A * F;
                sw.WriteLine("Wind force on Exposed Surface at bearing level = A * F");
                sw.WriteLine("                                               = {0:f2} * {1:f2}", A, F);
                sw.WriteLine("                                               = {0:f2} kN", wind_force);
                sw.WriteLine();
                sw.WriteLine("Bending Moment at base = {0:f2} * h = {0:f2} * {1:f2}", wind_force, h);
                M = wind_force * h;
                sw.WriteLine("                       = {0:f2} kN-m", (wind_force * h));

                sw.WriteLine();
                sw.WriteLine("Section Modulus of Pier at base about Longitudinal axis");
                sw.WriteLine("       (Axis parallel to direction of trafic)");

                double Zl = (b1 * l * l) / 6;
                sw.WriteLine("       Zl = (b1 * l * l) / 6 = ({0:f2} * {1:f2} * {1:f2})/6",
                    b1,
                    l);
                sw.WriteLine("          = {0:f2} cu.m.", Zl);

                sw.WriteLine();
                stress_at_base = M / Zl;
                sw.WriteLine("     Stress at base = ±({0:f2}/{1:f2}) = ±{2:f2} kN/sq.m",
                    M,
                    Zl, stress_at_base);
                lst_s.Add(stress_at_base);

                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Stresses due to Water Current");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double k = 0.66;
                sw.WriteLine("k = {0:f2}", k);
                
                double p1 = 0.5 * k * V * V;
                sw.WriteLine("Intensity of Pressure = 0.5*k*V*V");
                sw.WriteLine("                      = 0.5*{0:f2}*{1:f2}*{1:f2}", k, V);
                sw.WriteLine("                      = {0:f2} kN/sq.m", p1);

                double water_curr_force = ((b1 + b2) / 2) * h * p1;
                sw.WriteLine();
                sw.WriteLine("Force by water current = ((b1 + b2) / 2) * h * p1");
                sw.WriteLine("                       = (({0:f2} + {1:f2}) / 2) * {2:f2} * {3:f2}",
                    b1, b2, h, p1);
                sw.WriteLine("                       = {0:f2} kN", water_curr_force);

                double h1 = (2.0 / 3.0) * HFL;
                sw.WriteLine();
                sw.WriteLine("Force acts at (2/3)*HFL = (2/3) * {0:f2} = {1:f2} m above base",
                    HFL, h1);
                
                M = water_curr_force * h1;

                sw.WriteLine();
                sw.WriteLine("Bending Moment at base = {0:f2} * {1:f2} = {2:f2} kN-m",
                    water_curr_force, h1, M);
                double p2 = M / Zl;
                sw.WriteLine();
                sw.WriteLine("Stress at base = p2 = ±({0:f2}/Zl) = ±({0:f2}/{1:f2}) = ±{0:f2} kN/sq.m",
                    M,
                    Zl,
                    p2);

                sw.WriteLine();
                sw.WriteLine("Considering current direction varies by 20 degrees");
                double p3 = p1 * Math.Cos(20 * (Math.PI / 180));
                sw.WriteLine();
                sw.WriteLine("Pressure parallel to Pier = p3 = p1 * Cos 20");
                sw.WriteLine("                          = {0:f2} * {1:f2} = {2:f3}",
                    p1,
                    Math.Cos(20 * (Math.PI / 180)),
                    p3);

                double p4 = p1 * Math.Sin(20 * (Math.PI / 180));
                sw.WriteLine();
                sw.WriteLine("Pressure perpendicular to Pier = p3 = p1 * Sin 20");
                sw.WriteLine("                          = {0:f2} * {1:f2} = {2:f3}",
                    p1,
                    Math.Sin(20 * (Math.PI / 180)),
                    p4);

                double p5 = p2 * p3 / p1;
                sw.WriteLine();
                sw.WriteLine("Stress at base by component parallel to Pier = p5 = ±(p2 * p3/p1)");
                sw.WriteLine("                                             = ±({0:f2} * {1:f2}/{2:f2})",
                    p2, p3, p1);
                sw.WriteLine("                                             = ± {0:f2} kN/sq.m. ", p5);

                sw.WriteLine();
                double p6 = p4 * l * HFL;
                sw.WriteLine("For perpendicular to Pier = p6 = p4 * l * HFL");
                sw.WriteLine("                          = {0:f2} * {1:f2} * {2:f2}", p4, l, HFL);
                sw.WriteLine("                          = {0:f2} kN", p6);

                M = p6 * h1;
                sw.WriteLine();
                sw.WriteLine("Bending Moment at base = p6 * h1");
                sw.WriteLine("                       = {0:f2} * {1:f2}", p6, h1);
                sw.WriteLine("                       = {0:f2} kN-m", M);

                sw.WriteLine();
                sw.WriteLine("Stress at base due to Component perpendicular to pier ");

                double p7 = M / Zt;
                sw.WriteLine("   p7 = ± {0:f2}/Zt =  ± {0:f2}/{1:f2} = {2:f2} kN/sq.m  ",
                    M,
                    Zt, p7);

                double max_stress_water_curr = 0.0;

                sw.WriteLine();
                if (p7 > p5)
                {
                    max_stress_water_curr = p2 + p7;
                    sw.WriteLine("Maximum stress by water current = ±(p2+p7)");
                    sw.WriteLine("                                = ±({0:f2}+{1:f2})", p2, p7);
                    sw.WriteLine("                                = ±{0:f2} kN/sq.m", max_stress_water_curr);

                }
                else
                {
                    max_stress_water_curr = p2 + p5;
                    sw.WriteLine("Maximum stress by water current = ±(p2+p5)");
                    sw.WriteLine("                                = ±({0:f2}+{1:f2})", p2, p5);
                    sw.WriteLine("                                = ±{0:f2} kN/sq.m", max_stress_water_curr);
                }

                lst_s.Add(max_stress_water_curr);
                #endregion
                
                #region STEP 7
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : SUMMARY OF STRESSES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Stresses should be considered for Dry river and flooded river cases.");
                sw.WriteLine("The stresses are presented below, the extreme comprehensive and tesile");
                sw.WriteLine("stresses are well within permissible limits.");
                sw.WriteLine();
                //sw.WriteLine("-------------------------------------------------------------------------");
                sw.WriteLine("_________________________________________________________________________");
                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                    "SL.NO",
                    "LOADS",
                    "Stresses(kN/sq.m)",
                    "Stresses");

                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                    "",
                                    "",
                                    "(Dry River)",
                                    "(Flood River)");
                sw.WriteLine("-------------------------------------------------------------------------");
               

                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "1.",
                                                    "Permanent Load and Self Weight",
                                                    "+" + lst_s[0].ToString("0.00"),
                                                    "+" + lst_s[0].ToString("0.00"));


                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "2.",
                                                    "Buoyancy",
                                                    "0",
                                                    lst_s[1].ToString("0.00"));


                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "3.",
                                                    "Eccentric Live Load",
                                                    "+" + lst_s[2].ToString("0.00"),
                                                    "+" + lst_s[2].ToString("0.00"));


                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "4a.",
                                                    "Tractive Forces",
                                                    "+" + lst_s[3].ToString("0.00"),
                                                    "±" + lst_s[3].ToString("0.00"));


                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "4b.",
                                                    "Bearing Resistance",
                                                    "±" + lst_s[4].ToString("0.00"),
                                                    "+" + lst_s[4].ToString("0.00"));


                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "5.",
                                                    "Wind Load",
                                                    "±" + lst_s[5].ToString("0.00"),
                                                    "±" + lst_s[5].ToString("0.00"));


                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "6.",
                                                    "Water Current",
                                                    "0",
                                                    "±" + lst_s[6].ToString("0.00"));

                double sum_flood = lst_s[0] + lst_s[1] +
                    lst_s[2] +
                    lst_s[3] +
                    lst_s[4] +
                    lst_s[5] +
                    lst_s[6];
                double sum_dry = lst_s[0] +
                    lst_s[2] +
                    lst_s[3] +
                    lst_s[4] +
                    lst_s[5];


                sw.WriteLine("_________________________________________________________________________");
                sw.WriteLine("{0,-5} {1,-27}{2,20}{3,20}",
                                                    "",
                                                    "SUM",
                                                    "+" + sum_dry.ToString("0.00"),
                                                    "+" + sum_flood.ToString("0.00"));

                sw.WriteLine("-------------------------------------------------------------------------");
                #endregion

                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
            

            }
            catch (Exception ex) { }
            finally
            {
                GC.Collect();
                sw.Flush();
                sw.Close();
            }
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
            kPath = Path.Combine(kPath, "Design of Pier");

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
                this.Text = "DESIGN OF BRIDGE PierS : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "BRIDGE_PIERS");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_Pier.TXT");
                user_input_file = Path.Combine(system_path, "BRIDGE_PIERS.FIL");

                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_From_File();
                }

                
            }
        }

        public void Read_From_File()
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
                        case "w1":
                            w1 = mList.GetDouble(1);
                            txt_w1.Text = w1.ToString();
                            break;
                        
                        case "w2":
                            w2 = mList.GetDouble(1);
                            txt_w2.Text = w2.ToString();
                            break;
                        
                        case "e":
                            e = mList.GetDouble(1);
                            txt_e.Text = e.ToString();
                            break;
                        
                        case "b1":
                            b1 = mList.GetDouble(1);
                            txt_b1.Text = b1.ToString();
                            break;
                        
                        case "b2":
                            b2 = mList.GetDouble(1);
                            txt_b2.Text = b2.ToString();
                            break;
                        
                        case "l":
                            l = mList.GetDouble(1);
                            txt_l.Text = l.ToString();
                            break;
                        
                        case "h":
                            h = mList.GetDouble(1);
                            txt_h.Text = h.ToString();
                            break;
                        
                        case "HFL":
                            HFL = mList.GetDouble(1);
                            txt_HFL.Text = HFL.ToString();
                            break;
                        
                        case "w3":
                            w3 = mList.GetDouble(1);
                            txt_w3.Text = w3.ToString();
                            break;
                        
                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = gamma_c.ToString();
                            break;
                        
                        case "f1":
                            f1 = mList.GetDouble(1);
                            txt_f1.Text = f1.ToString();
                            break;
                        
                        case "f2":
                            f2 = mList.GetDouble(1);
                            txt_f2.Text = f2.ToString();
                            break;
                        
                        case "A":
                            A = mList.GetDouble(1);
                            txt_A.Text = A.ToString();
                            break;
                        
                        case "F":
                            F = mList.GetDouble(1);
                            txt_F.Text = F.ToString();
                            break;
                        case "V":
                            V = mList.GetDouble(1);
                            txt_V.Text = V.ToString();
                            throw new Exception("DATA_INITIALIZED");
                            break;
                    }
                    #endregion
                    #region USER INPUT DATA

                    //sw.WriteLine(" = {0:f3} ", w1);
                    //sw.WriteLine(" = {0:f3} ", w2);
                    //sw.WriteLine(" = {0:f3} ", e);
                    //sw.WriteLine(" = {0:f3} ", b1);
                    //sw.WriteLine(" = {0:f3} ", b2);
                    //sw.WriteLine(" = {0:f3} ", l);
                    //sw.WriteLine(" = {0:f3} ", h);
                    //sw.WriteLine(" = {0:f3} ", );
                    //sw.WriteLine(" = {0:f3} ", w3);
                    //sw.WriteLine(" = {0:f3} ", gamma_c);
                    //sw.WriteLine(" = {0:f3} ", f1);
                    //sw.WriteLine(" = {0:f3} ", f2);
                    //sw.WriteLine(" = {0:f3} ", A);
                    //sw.WriteLine(" = {0:f3} ", F);
                    //sw.WriteLine(" = {0:f3} ", V);

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
            iApp.SetDrawingFile_Path(drawing_path, "Pier", "");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
