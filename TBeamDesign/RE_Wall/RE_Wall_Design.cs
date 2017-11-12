using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using System.IO;

namespace BridgeAnalysisDesign.RE_Wall
{
    class RE_Wall_Design
    {

        double _h1;
        double _start_h1;
        double _end_h1;
        double _h1_incr;

        public List<double> Total_Sections { get; set; }

        public double H1
        {
            get
            {
                return _h1;
            }
            set
            {
                _h1 = value;
                _end_h1 = _h1;
            }
        }
        public double Start_H1
        {
            get
            {
                return _start_h1;
            }
            set
            {
                _start_h1 = value;
            }
        }
        public double Hf { get; set; }
        public double Hc { get; set; }
        public double Ht { get; set; }
        public double E { get; set; }
        public double H2 { get; set; }
        public double F { get; set; }
        public double B { get; set; }
        public double w { get; set; }
        public double Bs { get; set; }
        public double Hm { get { return (H1 + H2); } }
        public double L { get; set; }
        public double fn { get; set; }
        public double q { get; set; }
        public double gama1_max { get; set; }
        public double gama1_min { get; set; }
        public double phi1 { get; set; }
        public double Cu { get; set; }
        public double gama2 { get; set; }
        public double phi2 { get; set; }
        public double gama_f { get; set; }
        public double phi_f { get; set; }
        public double C { get; set; }
        public double Dp { get; set; }
        public double Df { get; set; }
        public double Sl { get; set; }
        public string Sc { get; set; }
        public string Sp { get; set; }

        //Chiranjit [2013 04 04]
        // Add new Variable

        public double Zi { get; set; }
        public double del_i { get; set; }

        public double wi { get; set; }
        public double tot_layers { get; set; }




        //Force at Connection in %
        public double cf { get; set; }
        //Coefficient of friction
        public double fo { get; set; }


        public List<Reinforcement_Layout_Section> Layout_Sections { get; set; }

        public Hashtable Strip_Properties { get; set; }


        public int Get_Strip_Type(double val)
        {
            try
            {
                Polymetric_Strip pms = null;
                for (int i = 1; i <= 10; i++)
                {
                    pms = Strip_Properties[i] as Polymetric_Strip;
                    if (pms != null)
                    {
                        if (pms.Ultimate_Tensile_Strength.ToString("f3") == val.ToString("f3"))
                            return i;

                    }
                }
            }
            catch (Exception ex) { }

            return -1;
        }




        IApplication iapp = null;
        public RE_Wall_Design(IApplication app)
        {
            iapp = app;
        }

        public string Working_Folder { get; set; }

        public string Report_File
        {
            get
            {
                string file_name = "RE_Wall_Design_Calculations.TXT";
                return Path.Combine(Working_Folder, file_name);
            }
        }

        public string Report_Table_File
        {
            get
            {
                string file_name = "RE_Wall_Design_Table_Summary.TXT";
                return Path.Combine(Working_Folder, file_name);
            }
        }


        public void Calculate_Program()
        {
            List<string> list = new List<string>();

            /*
            Symbols
            
             * 

               𝜃          𝜃          √          π          λ          σ          µ          °      
               γ          δ          ρ          β          ∆          φ          Σ          α

             * 
            */



            #region TechSOFT Banner

            list.Add(string.Format(""));
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 21            *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*    DESIGN OF RE (REINFORCED EARTH)  WALL   *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            list.Add("");
            list.Add("");
            list.Add("------------------");
            list.Add("USER'S INPUT DATA");
            list.Add("------------------");
            list.Add("");
            list.Add("WALL GEOMETRY");
            list.Add("-------------");
            list.Add("");
            list.Add("");
            list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add(string.Format("Facing Height = Hf = {0} m", Hf));
            list.Add(string.Format("Coping Height = Hc = {0} m", Hc));
            list.Add(string.Format("Free Board = Ht = {0} m", Ht));
            list.Add(string.Format("Facing Thickness = E = {0} m", E));
            list.Add(string.Format("Slope Height = H2 = {0} m", H2));
            list.Add(string.Format("Set Back = F = {0} m", F));
            list.Add(string.Format("Slope Angle = B = {0} °(deg)", B));
            list.Add(string.Format("Terrance Angle = w = {0} °(deg)", w));
            list.Add(string.Format("Angle at Toe = Bs = {0} °(deg)", Bs));
            list.Add(string.Format("Mechanical Height = Hm = {0} m", Hm));
            list.Add(string.Format("Length of Geogrid Strips = L = {0} m", L));
            list.Add(string.Format("Economic Ramification = fn = {0}", L));
            list.Add("");
            list.Add("");
            list.Add("SURCHARGE");
            list.Add("---------");
            list.Add("");
            list.Add(string.Format("Live Load Surcharge = q = {0} kpa (Road Traffic)", q));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF RE BACKFILL");
            list.Add("------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γ1_max = {0} kN/Cu.m.", gama1_max));
            list.Add(string.Format("Minimum Dry Density = γ1_min = {0} kN/Cu.m.", gama1_min));
            list.Add(string.Format("Angle of Internal Friction = φ1 = {0} °(deg).", phi1));
            list.Add(string.Format("Coefficient of Uniformity = Cu = {0} ", Cu));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF GENERAL CENTRAL BACKFILL");
            list.Add("-------------------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γ2 = {0} kN/Cu.m.", gama2));
            list.Add(string.Format("Angle of Internal Friction = φ2 = {0} °(deg).", phi2));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF FOUNDATION SOIL");
            list.Add("----------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γf = {0} kN/Cu.m.", gama2));
            list.Add(string.Format("Angle of Internal Friction = φf = {0} °(deg).", phi2));
            list.Add(string.Format("Cohesion = C = {0} kN/Sq.m", C));
            list.Add(string.Format("Thickness of Pavement = Dp = {0} m", Dp));
            list.Add(string.Format("Thickness = Df = {0} m", Df));
            list.Add("");
            list.Add("");
            list.Add("ASSUMED DESIGN LIFE");
            list.Add("-------------------");
            list.Add("");
            list.Add(string.Format("Life of Structure = Sl = {0} year(s)", Sl));
            list.Add(string.Format("Site Condition = Sc = {0} ", Sc));
            list.Add(string.Format("Strip Protection = Sp = {0} ", Sp));
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("STEP 1 : EXTERNAL STABILITY");
            list.Add("---------------------------");
            list.Add("");
            list.Add("");
            list.Add("STEP 1.1 : EARTH PRESSURE");
            list.Add("-------------------------");
            list.Add("");
            list.Add("");
            list.Add("Inclination of Earth Pressure at the back of RE Wall");
            list.Add("");


            double val1, val2;

            val1 = (1.2 - L / H1) * phi2;
            val2 = 0.8 * (1 - 0.7 * L / Hm) * phi1;
            double delta = val2;
            list.Add(string.Format(" δ = (1.2 - L/Hm) x φ2   OR 0.8 x (1- 0.7 x L/Hm) x φ1  (for B = {0}°) ", B));
            list.Add(string.Format("   = 0.8 x (1- 0.7 x {0}/{1}) x {2}", L, Hm, phi1));
            list.Add(string.Format("   = 0.8 x (1- {0:f4}) x {1}", (0.7 * L / Hm), phi1));
            list.Add(string.Format("   = {0:f4} ", delta));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Obtain Coefficient K2 from Coulomb"));
            list.Add(string.Format(""));



            double _B = MyList.Convert_Degree_To_Radian(B);
            double _w = MyList.Convert_Degree_To_Radian(w);
            double _Bs = MyList.Convert_Degree_To_Radian(Bs);
            double _phi1 = MyList.Convert_Degree_To_Radian(phi1);
            double _phi2 = MyList.Convert_Degree_To_Radian(phi2);
            double _phi_f = MyList.Convert_Degree_To_Radian(phi_f);

            double _delta = MyList.Convert_Degree_To_Radian(delta);





            double K2 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta)) / (1 + Math.Pow(Math.Sqrt(Math.Sin(_phi1 + _delta) * Math.Sin(_phi1 - _w) / (Math.Cos(_delta) * Math.Cos(_w))), 2));



            val1 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta));
            val2 = Math.Sin(_phi1 + _delta);

            double val3 = Math.Sin(_phi1 - _w);
            double val4 = (Math.Cos(_delta) * Math.Cos(_w));

            double val5 = (val2 * val3 / val4);

            double val6 = val1 / Math.Pow((1 + Math.Sqrt(val5)), 2);

            K2 = val6;


            list.Add(string.Format(" K2 = [(Cos φ2)^2/Cos δ]/[1 + √(Sin (φ1+δ)* Sin(φ1 - w)/ Cos δ * Cos w)]^2"));
            list.Add("");
            list.Add(string.Format("    = [(Cos {0})^2/Cos {1:f3}]/[1 + √(Sin ({2} + {1:f3})* Sin({2} - {3})/ Cos {1:f3} * Cos {3})]^2", phi2, delta, phi1, w));

            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3} * {2:f3} / {3:f3})]^2", val1, val2, val3, val4));
            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3})]^2", val1, val5));

            list.Add(string.Format("    = {0:f3}", K2));

            list.Add("");
            list.Add("");

            double ka1 = (1 - Math.Sin(_phi1)) / (1 + Math.Sin(_phi1));
            list.Add(string.Format(" ka1  = (1 - Sin φ1) / (1 + Sin φ1)"));
            list.Add(string.Format("      = (1 - Sin ({0})) / (1 + Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka1));
            list.Add("");
            list.Add("");
            double ka2 = (1 - Math.Sin(_phi1));

            list.Add(string.Format(" ka2  = (1 - Sin φ1) "));
            list.Add(string.Format("      = (1 - Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka2));
            list.Add("");
            list.Add("");
            list.Add("-----------------------------");
            list.Add("STEP 1.2 (A) : VERTICAL FORCES");
            list.Add("-----------------------------");
            list.Add("");
            list.Add("Partial Safety Factor for Loads = Fp = 1.5,  Table 16, BS : 8006");
            list.Add("");

            double Fp = 1.5;
            double Wr = Dp * gama1_max * L * Fp;
            list.Add(string.Format("Wr = Wt. of Pavement Crust above RE Wall"));
            list.Add(string.Format("   = Dp * γ1_max * L * Fp "));
            list.Add(string.Format("   = {0:f3} * {1} * {2} * {3} ", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wr));
            list.Add("");
            list.Add("");



            double Wm_a = (Hm - H2) * gama1_max * L * Fp;
            list.Add(string.Format("Wm (a) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format("       = (Hm - H2) * γ1_max * L * Fp"));
            list.Add(string.Format("       = ({0} - {1}) * {2} * {3} * {4}", Hm, H2, gama1_max, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Wq (a) = Traffic Load on backfill retained by RE Wall "));
            double Wq_a = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_a));
            list.Add("");

            list.Add("");
            list.Add(string.Format("Pqv (a) = Vertical Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pqv_a = (K2 * q) * (Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = Pq * Sin δ * Fp"));
            list.Add(string.Format("       = (K2 * q) * Hm * Sin δ * Fp"));
            list.Add(string.Format("       = ({0:f3} * {1}) * {2} * Sin {3:f3} * {4}", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pqv_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Pv (a) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            double Pv_a = (K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = (K2 * 0.5 * γ2 * Hm*Hm *  Sin δ * Fp)"));
            list.Add(string.Format("       = ({0:f3} * 0.5 * {1} * {2} * {2} * Sin {3:f3} * {4})", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_a));
            list.Add("");
            list.Add("");
            list.Add("-------------------------------");
            list.Add("STEP 1.3 (A) : HORIZONTAL FORCE");
            list.Add("-------------------------------");
            list.Add("");
            list.Add(string.Format("Pqh = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("------------------------------");
            list.Add("STEP 1.2 (B) : VERTICAL FORCES");
            list.Add("------------------------------");
            list.Add("");
            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrb = Weight of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrb = Dp * gama1_min * L * Fp;



            list.Add(string.Format("    = Dp * γ1_min * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_b = (Hm - H2) * gama1_min * L * Fp;


            list.Add(string.Format("Wm (b) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Fp = 1.5 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv (b) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            Fp = 1.5;
            double Pqv_b = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_b));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (b) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("");
            list.Add("-------------------------------");
            list.Add("STEP 1.3 (B) : HORIZONTAL FORCE");
            list.Add("-------------------------------");
            list.Add("");
            list.Add(string.Format("Pqh (B) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_b = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (B) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("------------------------------");
            list.Add("STEP 1.2 (C) : VERTICAL FORCES");
            list.Add("------------------------------");
            list.Add("");

            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrc = Wt. of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrc = Dp * gama1_max * L * Fp;



            list.Add(string.Format("    = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrc));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_c = (Hm - H2) * gama1_max * L * Fp;


            list.Add(string.Format("Wm (c) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 0.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));

            list.Add("");
            list.Add(string.Format("Wq (c) = Traffic Load on backfill retained by RE Wall "));
            double Wq_c = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_c));
            list.Add("");
            list.Add("");


            list.Add(string.Format("Pqv (c) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            double Pqv_c = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_c));

            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = 1.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (c) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("-------------------------------");
            list.Add("STEP 1.3 (C) : HORIZONTAL FORCE");
            list.Add("-------------------------------");
            list.Add("");
            Fp = 0.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh (C) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_c = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (C) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : COMPUTATION OF DRIVING & RESISTING MOMENT"));
            list.Add(string.Format("--------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 1 : CASE (A)"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            string format = "{0,-7} {1,-10} {2,-18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(A) = " + Wr.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wr * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(A) = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(A) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(A) = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(A) = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(A) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(A) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_a = Wr + Wm_a + Wq_a + Pqv_a + Pv_a;
            double Rh_a = Pq_h + Ph;
            double M_a = (Wr * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((H1 + H2) / 2.0)) - (Ph * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(A)= " + Rv_a.ToString("f3"),
                "ΣRh(A)= " + Rh_a.ToString("f3"),
                "",
                "ΣM(A)= " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(A) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_a, L, e));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 2 : CASE (B)"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(B) = " + Wrb.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrb * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(B) = " + Wm_b.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_b * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Pqv(B) = " + Pqv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pv(B) = " + Pv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "_____      ",
                "Pqh(B) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Ph(B) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_b = Wrb + Wm_b + Pqv_b + Pv_b;
            double Rh_b = Pq_h_b + Ph_b;
            double M_b = (Wrb * L / 2.0) + (Wm_b * L / 2.0) + (Pqv_b * L) + (Pv_b * L) - (Pq_h_b * ((H1 + H2) / 2.0)) - (Ph_b * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(B)= " + Rv_b.ToString("f3"),
                "ΣRh(B)= " + Rh_b.ToString("f3"),
                "",
                "ΣM(B)= " + M_b.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_b = M_b / Rv_b;


            list.Add(string.Format("x = ΣM(B) / ΣRv(B)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_b = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_b = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(B) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_b, L, e_b));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 3 : CASE (C)"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(C) = " + Wrc.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrc * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(C) = " + Wm_c.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(C) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(C) = " + Pqv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(C) = " + Pv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(C) = " + Pq_h_c.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h_c * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(C) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph_c * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_c = Wrc + Wm_c + Wq_c + Pqv_c + Pv_c;
            double Rh_c = Pq_h_c + Ph_c;
            double M_c = (Wrc * L / 2.0) + (Wm_c * L / 2.0) + (Wq_c * L / 2.0) + (Pqv_c * L) + (Pv_c * L) - (Pq_h_c * ((H1 + H2) / 2.0)) - (Ph_c * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(C)= " + Rv_c.ToString("f3"),
                "ΣRh(C)= " + Rh_c.ToString("f3"),
                "",
                "ΣM(C)= " + M_c.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_c = M_c / Rv_c;


            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_c, Rv_c));
            list.Add(string.Format("  = {0:f3} m", x_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_c = (L / 2.0) - x_c;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_c));
            list.Add(string.Format("                 = {0:f3} m", e_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_c = Rv_c / (L - 2 * e_c);

            list.Add(string.Format("q_req = ΣRv(C) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_c, L, e_c));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format("TABLE 4 : SUMMARY"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            format = "{0,9} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}";

            list.Add(string.Format(""));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "Case", "Rv  ", "Rh  ", " M    ", "e ", "q_req"));
            list.Add(string.Format(format, "", "(kN/m)", "(kN/m)", "(kN-m/m)", "(m)", "(kpa)"));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "A", Rv_a, Rh_a, M_a, e, q_req));
            list.Add(string.Format(format, "B", Rv_b, Rh_b, M_b, e_b, q_req_b));
            list.Add(string.Format(format, "C", Rv_c, Rh_c, M_c, e_c, q_req_c));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------"));
            list.Add(string.Format("STEP 3 : CHECK FOR SLIDING"));
            list.Add(string.Format("--------------------------"));
            list.Add(string.Format(""));


            Fp = 1.2;
            list.Add(string.Format("Partial Factor of Safety (Soil to Soil) = 1.2 (minimum), (Ref: Table 16, BS : 8006 - 1995)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("---------------------------------"));
            list.Add(string.Format("STEP 3.1 : Slip in Retained Earth"));
            list.Add(string.Format("---------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φ1)/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            double FoS_A = (Rv_a * Math.Tan(_phi1)) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_a, phi1, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_A, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double FoS_B = (Rv_b * Math.Tan(_phi1)) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_b, phi1, Rh_b));
            if (FoS_B > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_B, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS_C = (Rv_c * Math.Tan(_phi1)) / Rh_c;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_c, phi1, Rh_c));
            if (FoS_C > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format("STEP 3.2 : Slip in Foundation"));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_a, phi_f, C, L, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_b, phi_f, C, L, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / Rh_c;
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_c, phi_f, C, L, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------"));
            list.Add(string.Format("STEP 3.3 : Slip in Retained Earth for Over Design"));
            list.Add(string.Format("-------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available Factor of Safety (FoS) for Over Design = (Rv * tan φ1)/(1.2 * Rh)"));

            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi1)) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_a, phi1, Fp, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_A, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi1)) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_b, phi1, Fp, Rh_b));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_B, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi1)) / (Fp * Rh_c);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_c, phi1, Fp, Rh_c));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format("STEP 3.4 : Slip in Foundation for Over Design"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_a, phi_f, C, L, Fp, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_b, phi_f, C, L, Fp, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / (Fp * Rh_c);
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_c, phi_f, C, L, Fp, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format("STEP 3.5 : CHECK FOR OVERTURING"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS = 1.5;

            list.Add(string.Format("Factor of Safety (FoS) for Overturnig = {0} (minimum)", FoS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("FoS for Overturnig at Toe = Σ[Resisting Moment (+ve)]/Σ[Disturbing Moment (-ve)]", FoS));
            list.Add(string.Format(""));

            FoS_A = Rv_a / Rh_a;
            if (FoS_A > FoS)
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_a, Rh_a, FoS_A, FoS));
            else
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_a, Rh_a, FoS_A, FoS));

            FoS_B = Rv_b / Rh_b;
            if (FoS_B > FoS)
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_b, Rh_b, FoS_B, FoS));
            else
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_b, Rh_b, FoS_B, FoS));

            FoS_C = Rv_c / Rh_c;
            if (FoS_C > FoS)
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_c, Rh_c, FoS_C, FoS));
            else
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_c, Rh_c, FoS_C, FoS));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format("STEP 4 : CALCULATION FOR TENSION IN STRIPS FOR INTERNAL STABILITY"));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));

            Fp = 1.5;
            list.Add(string.Format("Fp = Partial Safety Factor = {0}", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("(a)  Vertical Forces"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wr = Weight of Road Crust above Retained Earth"));

            double Wra = Dp * gama1_max * L * Fp;

            list.Add(string.Format("   = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wra));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Wm = Weight of Retained Earth mass"));


            Wm_a = Zi * gama1_max * L * Fp;
            list.Add(string.Format("   = Zi * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Zi, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wm_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic Load above Retained Earth mass"));
            Wq_a = L * q * Fp;
            list.Add(string.Format("   = L * q * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv = Vertical Component of Pressure by Surcharge behind Retained Earth mass"));
            list.Add(string.Format("    = Pq * Sin δ * Fp"));

            Pqv_a = (K2 * q * (Zi + Dp)) * Math.Sin(_delta) * Fp;
            list.Add(string.Format("    = (K2 * q * (Zi + Dp)) * Sin δ * Fp"));
            list.Add(string.Format("    = ({0:f3} * {1:f3} * ({2} + {3})) * Sin ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pqv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv = Vertical Component of Earth Pressure by Retained Earth mass"));

            Pv_a = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Sin(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Sin δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Sin ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Pv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b) Horizontal Forces :"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh = Horizontal Component of Pressure by Surcharge on Retained Earth mass"));

            Pq_h = K2 * q * (Zi + Dp) * Math.Cos(_delta) * Fp;
            list.Add(string.Format("    = K2 * q * (Zi+Dp) * Cos δ * Fp"));
            list.Add(string.Format("    = {0:f3} * {1} * ({2} + {3}) * Cos ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by Retained Earth mass"));

            Ph = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Cos(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Cos ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 5 : CALCULAION FOR DRIVING AND RESISTING MOMENT"));
            list.Add(string.Format("-----------------------------------------------------"));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            format = "{0,-7} {1,-10} {2,18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr = " + Wra.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wra * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh = " + Pq_h.ToString("f3"),
                "(Zi+Dp)/2 = " + ((Zi + Dp) / 2.0).ToString("f3"),
                -(Pq_h * ((Zi + Dp) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph = " + Ph.ToString("f3"),
                "(Zi+Dp)/3 = " + ((Zi + Dp) / 3.0).ToString("f3"),
                -(Ph * (Zi + Dp) / 3.0)));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            Rv_a = Wra + Wm_a + Wq_a + Pqv_a + Pv_a;
            Rh_a = Pq_h + Ph;
            M_a = (Wra * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((Zi + Dp) / 2.0)) - (Ph * ((Zi + Dp) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv = " + Rv_a.ToString("f3"),
                "ΣRh = " + Rh_a.ToString("f3"),
                "",
                "ΣM = " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM / ΣRv "));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_v = Rv_a / (2 * x_a);
            list.Add(string.Format("Vertical Pressure = σ_v = ΣRv / (2*x)"));
            list.Add(string.Format("                        = {0:f3} / (2 * {1:f3})", Rv_a, x_a));
            list.Add(string.Format("                        = {0:f3} kPa", sigma_v));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Hense Horizontal Pressure = σ_h = σ_v * Ki"));
            list.Add(string.Format(""));

            double Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1;
            double sigma_h = sigma_v * Ki;
            list.Add(string.Format(""));
            list.Add(string.Format("By interpolating from Figure 2, with values of ka1, ka2, Zi and H1, we get the value of Ki"));
            list.Add(string.Format(""));

            list.Add(string.Format("Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1"));
            list.Add(string.Format("   = (({0:f3} - {1:f3}) * ({2:f3} - ({3:f3} + {4:f3})) / {5:f3}) + {1:f3}", ka2, ka1, H1, Zi, Dp, H1));
            list.Add(string.Format("   = {0:f3} ", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_h = σ_v * Ki"));
            list.Add(string.Format("    = {0:f3} * {1:f3}", sigma_v, Ki));
            list.Add(string.Format("    = {0:f3} kPa", sigma_h));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical spacing of strip layers = ∆i = {0} m", del_i));
            list.Add(string.Format("Calculation side for Strips = wi = {0} m", wi));
            list.Add(string.Format(""));

            double Total_Tmax = sigma_h * wi * del_i;

            list.Add(string.Format("Total Tension in Strips = Total_Tmax = σ_h * wi * ∆i"));
            list.Add(string.Format("                                     = {0:f3} * {1} * {2}", sigma_h, wi, del_i));
            list.Add(string.Format("                                     = {0:f3} kN", sigma_h, wi, del_i));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Number of Strips in the Layers = Tot_layers = {0} nos", tot_layers));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Tension per Strip = Tmax = Total_Tmax / Tot_layers"));
            list.Add(string.Format("                         = {0:f3} / {1:f0}", Total_Tmax, tot_layers));
            list.Add(string.Format("                         = {0:f3} kN", (Total_Tmax / tot_layers)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Height from Top = Zi = {0} m", Zi));
            list.Add(string.Format(""));
            list.Add(string.Format("Ki = {0:f4}", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format("Delta ∆i = {0:f3} m", del_i));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Strips (for {0} m width) = {1} ", wi, tot_layers));
            list.Add(string.Format(""));

            double frc_conn_pcnt = 85.0;

            list.Add(string.Format("Force At Connection = {0}%", frc_conn_pcnt));
            list.Add(string.Format(""));

            double fo = 0.801;


            list.Add(string.Format("Coefficient of Friction = fo = {0:f3}", fo));
            list.Add(string.Format(""));

            double La = 3.215;

            list.Add(string.Format("Adherence Length = La = {0:f3}", fo));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add(string.Format(""));
            File.WriteAllLines(Report_File, list.ToArray());
            //iapp.View_Result(Report_File);
        }

        public void Loop_Program()
        {
            List<string> list = new List<string>();


            List<string> _table_list = new List<string>();
            List<string> _summary_list = new List<string>();
            List<string> _summary_list2 = new List<string>();

            /*
            Symbols
            
             * 

               𝜃          𝜃          √          π          λ          σ          µ          °      
               γ          δ          ρ          β          ∆          φ          Σ          α

             * 
            */

            #region TechSOFT Banner

            list.Add(string.Format(""));
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 21            *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*    DESIGN OF RE (REINFORCED EARTH)  WALL   *");
            list.Add("\t\t*           CALCULATION DETAILS              *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            _table_list.Add(string.Format(""));
            _table_list.Add("\t\t**********************************************");
            _table_list.Add("\t\t*            ASTRA Pro Release 21            *");
            _table_list.Add("\t\t*        TechSOFT Engineering Services       *");
            _table_list.Add("\t\t*                                            *");
            _table_list.Add("\t\t*    DESIGN OF RE (REINFORCED EARTH)  WALL   *");
            _table_list.Add("\t\t*            DESIGN SUMMARY REPORT           *");
            _table_list.Add("\t\t*                                            *");
            _table_list.Add("\t\t**********************************************");
            _table_list.Add("\t\t----------------------------------------------");
            _table_list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            _table_list.Add("\t\t----------------------------------------------");
            _table_list.Add(string.Format(""));
            
            #endregion

            string kStr = "";



            #region User Input Data
            list.Add("");
            list.Add("");
            list.Add("------------------");
            list.Add("USER'S INPUT DATA");
            list.Add("------------------");
            list.Add("");
            list.Add("WALL GEOMETRY");
            list.Add("-------------");
            list.Add("");
            list.Add("");
            list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            //list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));
            //list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            //list.Add("");
            list.Add("");
            Hf = H1;
            list.Add(string.Format("Facing Height = Hf = {0} m", Hf));
            list.Add(string.Format("Coping Height = Hc = {0} m", Hc));
            list.Add(string.Format("Free Board = Ht = {0} m", Ht));
            list.Add(string.Format("Facing Thickness = E = {0} m", E));
            list.Add(string.Format("Slope Height = H2 = {0} m", H2));
            list.Add(string.Format("Set Back = F = {0} m", F));
            list.Add(string.Format("Slope Angle = B = {0} °(deg)", B));
            list.Add(string.Format("Terrance Angle = w = {0} °(deg)", w));
            list.Add(string.Format("Angle at Toe = Bs = {0} °(deg)", Bs));
            list.Add(string.Format("Mechanical Height = Hm = {0} m", Hm));
            list.Add(string.Format("Length of Geogrid Strips = L = {0} m", L));
            list.Add(string.Format("Economic Ramification = fn = {0}", fn));
            list.Add("");
            list.Add("");
            list.Add("SURCHARGE");
            list.Add("---------");
            list.Add("");
            list.Add(string.Format("Live Load Surcharge = q = {0} kpa (Road Traffic)", q));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF RE BACKFILL");
            list.Add("------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γ1_max = {0} kN/Cu.m.", gama1_max));
            list.Add(string.Format("Minimum Dry Density = γ1_min = {0} kN/Cu.m.", gama1_min));
            list.Add(string.Format("Angle of Internal Friction = φ1 = {0} °(deg).", phi1));
            list.Add(string.Format("Coefficient of Uniformity = Cu = {0} ", Cu));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF GENERAL CENTRAL BACKFILL");
            list.Add("-------------------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γ2 = {0} kN/Cu.m.", gama2));
            list.Add(string.Format("Angle of Internal Friction = φ2 = {0} °(deg).", phi2));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF FOUNDATION SOIL");
            list.Add("----------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γf = {0} kN/Cu.m.", gama2));
            list.Add(string.Format("Angle of Internal Friction = φf = {0} °(deg).", phi2));
            list.Add(string.Format("Cohesion = C = {0} kN/Sq.m", C));
            list.Add(string.Format("Thickness of Pavement = Dp = {0} m", Dp));
            list.Add(string.Format("Thickness = Df = {0} m", Df));
            list.Add("");
            list.Add("");
            list.Add("ASSUMED DESIGN LIFE");
            list.Add("-------------------");
            list.Add("");
            list.Add(string.Format("Life of Structure = Sl = {0} year(s)", Sl));
            list.Add(string.Format("Site Condition = Sc = {0} ", Sc));
            list.Add(string.Format("Strip Protection = Sp = {0} ", Sp));
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("--------------------------------------------------------------------");
            list.Add("                      DESIGN CALCULATIONS");
            list.Add("--------------------------------------------------------------------");
            list.Add("");
            #endregion User Input Data

            int tot = 0;
            int tot1 = 0;
            for (int i = 0; i < Layout_Sections.Count; i++) tot += Layout_Sections[i].Layers.Count;

            iapp.Progress_ON("Design of Strips........");


            //string format = "{0} {1,9:f3} {2,9:f3} {3,9:f3} {4,9:f3} {5,9:f3} {6,9:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3}";


            for (int i = 0; i < Layout_Sections.Count; i++)
            {
                for (int j = 0; j < Layout_Sections[i].Layers.Count; j++)
                {
                    H1 = Total_Sections[i];
                    Zi = Layout_Sections[i].Layers[j].z;
                    del_i = Layout_Sections[i].Layers[j].delta_h;
                    L = Layout_Sections[i].Layers[j].Strip_Length;

                    tot_layers = Layout_Sections[i].Layers[j].Number_Strips;

                    Layout_Sections[i].Layers[j].Clear_Data();



                    kStr = string.Format("STEP {0}.{1} : SECTION {0} LAYER {1} [H1 = {2:f3} m, Zi = {3:f3} m]", (i + 1), (j + 1), H1, Zi);
                    list.Add(string.Format(""));

                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                    list.Add(string.Format(kStr));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                    list.Add(string.Format(""));
                    Loop_Sub_Program(ref list, Layout_Sections[i].Layers[j]);

                    iapp.SetProgressValue(tot1++, tot);

                    //Chiranjit [2013 05 15]
                    if (iapp.Is_Progress_Cancel) break;
                    if (j == 0)
                    {


                        kStr = string.Format("STEP {0} : SECTION {0} [H1 = {1:f3} m]", (i + 1), H1);

                        _table_list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                        _table_list.Add(string.Format(kStr));
                        _table_list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                        _table_list.Add(string.Format(""));

                        _table_list.Add(string.Format("-------------------------------------"));
                        _table_list.Add(string.Format("STEP {0}.1 : SECTION DATA", (i + 1)));
                        _table_list.Add(string.Format("-------------------------------------"));
                        _table_list.Add(string.Format(""));

                        _table_list.AddRange(Get_User_Input_Data().ToArray());
                        _table_list.Add(string.Format(""));
                        _table_list.Add(string.Format("-------------------------------------"));
                        _table_list.Add(string.Format("STEP {0}.2 : EARTH PRESSURE ", (i + 1)));
                        _table_list.Add(string.Format("-------------------------------------"));
                        _table_list.Add(string.Format(""));
                        _table_list.AddRange(Layout_Sections[i].Layers[j].Get_Earth_Pressure_Report().ToArray());
                        _table_list.Add(string.Format(""));
                        _table_list.Add(string.Format(""));

                        _table_list.Add(string.Format("-------------------------------------------"));
                        _table_list.Add(string.Format("STEP {0}.3 : EXTERNAL STABILITY", (i + 1)));
                        _table_list.Add(string.Format("-------------------------------------------"));
                        _table_list.Add(string.Format(""));
                        Layout_Sections[i].Layers[j].Ext_Stability_Table.depth = Layout_Sections[i].Embedment_Depth;
                        _table_list.AddRange(Layout_Sections[i].Layers[j].Ext_Stability_Table.Get_Report_Lines().ToArray());

                        _summary_list.AddRange(Layout_Sections[i].Layers[j].TABLE_HEAD_Unit_1().ToArray());

                        _summary_list2.AddRange(Layout_Sections[i].Layers[j].TABLE_HEAD_Unit_2().ToArray());

                    }
                    _summary_list.AddRange(Layout_Sections[i].Layers[j].Get_Report_Internal_Stability().ToArray());
                    _summary_list2.AddRange(Layout_Sections[i].Layers[j].Get_Report_Adherence().ToArray());
                }


                _summary_list.Add("".PadLeft(_summary_list[0].Length, '-'));

                _summary_list.Add(string.Format(""));
                _summary_list.Add(string.Format(""));


                _table_list.Add(string.Format(""));
                _table_list.Add(string.Format("---------------------------------------------------------------------------"));
                _table_list.Add(string.Format("STEP {0}.4 : STRIP RUPTURE - TENSILE LOAD AT FACING : Overdesign Factors", (i + 1)));
                _table_list.Add(string.Format("---------------------------------------------------------------------------"));
                _table_list.Add(string.Format(""));
                _table_list.AddRange(_summary_list.ToArray());
                _summary_list.Clear();



                _summary_list2.Add("".PadLeft(_summary_list2[0].Length, '-'));

                _summary_list2.Add(string.Format(""));
                _summary_list2.Add(string.Format(""));

                _table_list.Add(string.Format(""));
                _table_list.Add(string.Format("---------------------------------------------------------------------------"));
                _table_list.Add(string.Format("STEP {0}.5 : ADHERENCE : Overdesign Factors", (i + 1)));
                _table_list.Add(string.Format("---------------------------------------------------------------------------"));
                _table_list.Add(string.Format(""));
                _table_list.AddRange(_summary_list2.ToArray());
                _summary_list2.Clear();




            }
            iapp.Progress_OFF();


            list.Add(string.Format(""));
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add(string.Format(""));

            _table_list.Add(string.Format(""));
            _table_list.Add("---------------------------------------------------------------------------");
            _table_list.Add("---------------------       END OF REPORT        --------------------------");
            _table_list.Add("---------------------------------------------------------------------------");
            _table_list.Add("");
            _table_list.Add("---------------------------------------------------------------------------");
            _table_list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            _table_list.Add("---------------------------------------------------------------------------");
            _table_list.Add(string.Format(""));
            File.WriteAllLines(Report_File, list.ToArray());
            //iapp.View_Result(Report_File);

            //_table_list.AddRange(_summary_list.ToArray());
            File.WriteAllLines(Report_Table_File, _table_list.ToArray());
        }


        public void Loop_Sub_Program(ref List<string> list)
        {


            //List<string> list = new List<string>();

            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));
            //list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("EXTERNAL STABILITY");
            list.Add("------------------");
            list.Add("");
            list.Add("");
            list.Add("EARTH PRESSURE");
            list.Add("---------------");
            list.Add("");
            list.Add("");
            list.Add("Inclination of Earth Pressure at the back of RE Wall");
            list.Add("");


            double val1, val2;

            val1 = (1.2 - L / H1) * phi2;
            val2 = 0.8 * (1 - 0.7 * L / Hm) * phi1;
            double delta = val2;
            list.Add(string.Format(" δ = (1.2 - L/Hm) x φ2   OR 0.8 x (1- 0.7 x L/Hm) x φ1  (for B = {0}°) ", B));
            list.Add(string.Format("   = 0.8 x (1- 0.7 x {0}/{1}) x {2}", L, Hm, phi1));
            list.Add(string.Format("   = 0.8 x (1- {0:f4}) x {1}", (0.7 * L / Hm), phi1));
            list.Add(string.Format("   = {0:f4} ", delta));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Obtain Coefficient K2 from Coulomb"));
            list.Add(string.Format(""));



            double _B = MyList.Convert_Degree_To_Radian(B);
            double _w = MyList.Convert_Degree_To_Radian(w);
            double _Bs = MyList.Convert_Degree_To_Radian(Bs);
            double _phi1 = MyList.Convert_Degree_To_Radian(phi1);
            double _phi2 = MyList.Convert_Degree_To_Radian(phi2);
            double _phi_f = MyList.Convert_Degree_To_Radian(phi_f);

            double _delta = MyList.Convert_Degree_To_Radian(delta);





            double K2 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta)) / (1 + Math.Pow(Math.Sqrt(Math.Sin(_phi1 + _delta) * Math.Sin(_phi1 - _w) / (Math.Cos(_delta) * Math.Cos(_w))), 2));



            val1 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta));
            val2 = Math.Sin(_phi1 + _delta);

            double val3 = Math.Sin(_phi1 - _w);
            double val4 = (Math.Cos(_delta) * Math.Cos(_w));

            double val5 = (val2 * val3 / val4);

            double val6 = val1 / Math.Pow((1 + Math.Sqrt(val5)), 2);

            K2 = val6;


            list.Add(string.Format(" K2 = [(Cos φ2)^2/Cos δ]/[1 + √(Sin (φ1+δ)* Sin(φ1 - w)/ Cos δ * Cos w)]^2"));
            list.Add("");
            list.Add(string.Format("    = [(Cos {0})^2/Cos {1:f3}]/[1 + √(Sin ({2} + {1:f3})* Sin({2} - {3})/ Cos {1:f3} * Cos {3})]^2", phi2, delta, phi1, w));

            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3} * {2:f3} / {3:f3})]^2", val1, val2, val3, val4));
            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3})]^2", val1, val5));

            list.Add(string.Format("    = {0:f3}", K2));

            list.Add("");
            list.Add("");

            double ka1 = (1 - Math.Sin(_phi1)) / (1 + Math.Sin(_phi1));
            list.Add(string.Format(" ka1  = (1 - Sin φ1) / (1 + Sin φ1)"));
            list.Add(string.Format("      = (1 - Sin ({0})) / (1 + Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka1));
            list.Add("");
            list.Add("");
            double ka2 = (1 - Math.Sin(_phi1));

            list.Add(string.Format(" ka2  = (1 - Sin φ1) "));
            list.Add(string.Format("      = (1 - Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka2));
            list.Add("");
            list.Add("");
            list.Add("(A) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");
            list.Add("Partial Safety Factor for Loads = Fp = 1.5,  Table 16, BS : 8006");
            list.Add("");

            double Fp = 1.5;
            double Wr = Dp * gama1_max * L * Fp;
            list.Add(string.Format("Wr = Wt. of Pavement Crust above RE Wall"));
            list.Add(string.Format("   = Dp * γ1_max * L * Fp "));
            list.Add(string.Format("   = {0:f3} * {1} * {2} * {3} ", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wr));
            list.Add("");
            list.Add("");



            double Wm_a = (Hm - H2) * gama1_max * L * Fp;
            list.Add(string.Format("Wm (a) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format("       = (Hm - H2) * γ1_max * L * Fp"));
            list.Add(string.Format("       = ({0} - {1}) * {2} * {3} * {4}", Hm, H2, gama1_max, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Wq (a) = Traffic Load on backfill retained by RE Wall "));
            double Wq_a = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_a));
            list.Add("");

            list.Add("");
            list.Add(string.Format("Pqv (a) = Vertical Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pqv_a = (K2 * q) * (Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = Pq * Sin δ * Fp"));
            list.Add(string.Format("       = (K2 * q) * Hm * Sin δ * Fp"));
            list.Add(string.Format("       = ({0:f3} * {1}) * {2} * Sin {3:f3} * {4}", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pqv_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Pv (a) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            double Pv_a = (K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = (K2 * 0.5 * γ2 * Hm*Hm *  Sin δ * Fp)"));
            list.Add(string.Format("       = ({0:f3} * 0.5 * {1} * {2} * {2} * Sin {3:f3} * {4})", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_a));
            list.Add("");
            list.Add("");
            list.Add("(A) : HORIZONTAL FORCE");
            list.Add("-----------------------");
            list.Add("");
            list.Add(string.Format("Pqh = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(B) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");
            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrb = Weight of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrb = Dp * gama1_min * L * Fp;



            list.Add(string.Format("    = Dp * γ1_min * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_b = (Hm - H2) * gama1_min * L * Fp;


            list.Add(string.Format("Wm (b) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Fp = 1.5 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv (b) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            Fp = 1.5;
            double Pqv_b = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_b));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (b) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(B) : HORIZONTAL FORCE");
            list.Add("----------------------");
            list.Add("");
            list.Add(string.Format("Pqh (B) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_b = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (B) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("(C) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");

            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrc = Wt. of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrc = Dp * gama1_max * L * Fp;



            list.Add(string.Format("    = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_max, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrc));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_c = (Hm - H2) * gama1_min * L * Fp;


            list.Add(string.Format("Wm (c) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 0.0;
            //list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format("Fp = {0:f1} = Partial safety Factor for Loads", Fp));
            list.Add(string.Format(""));

            list.Add("");
            list.Add(string.Format("Wq (c) = Traffic Load on backfill retained by RE Wall "));
            double Wq_c = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_c));
            list.Add("");
            list.Add("");


            list.Add(string.Format("Pqv (c) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            double Pqv_c = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_c));

            list.Add(string.Format(""));
            Fp = 1.0;
            //list.Add(string.Format("Fp = 1.0 = Partial safety Factor for Loads"));
            list.Add(string.Format("Fp = {0:f1} = Partial safety Factor for Loads", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (c) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.0", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(C) : HORIZONTAL FORCE");
            list.Add("----------------------");
            list.Add("");
            Fp = 0.0;
            //list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format("Fp = {0:f1} = Partial safety Factor for Loads", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh (C) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_c = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = {0:f1} = Partial safety Factor for Loads", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (C) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("COMPUTATION OF DRIVING & RESISTING MOMENT"));
            list.Add(string.Format("-----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (A)"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));
            string format = "{0,-7} {1,-10} {2,-18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(A) = " + Wr.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wr * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(A) = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(A) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(A) = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(A) = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(A) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(A) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_a = Wr + Wm_a + Wq_a + Pqv_a + Pv_a;
            double Rh_a = Pq_h + Ph;
            double M_a = (Wr * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((H1 + H2) / 2.0)) - (Ph * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(A)= " + Rv_a.ToString("f3"),
                "ΣRh(A)= " + Rh_a.ToString("f3"),
                "",
                "ΣM(A)= " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(A) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_a, L, e));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (B)"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(B) = " + Wrb.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrb * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(B) = " + Wm_b.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_b * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Pqv(B) = " + Pqv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pv(B) = " + Pv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "_____      ",
                "Pqh(B) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Ph(B) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_b = Wrb + Wm_b + Pqv_b + Pv_b;
            double Rh_b = Pq_h_b + Ph_b;
            double M_b = (Wrb * L / 2.0) + (Wm_b * L / 2.0) + (Pqv_b * L) + (Pv_b * L) - (Pq_h_b * ((H1 + H2) / 2.0)) - (Ph_b * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(B)= " + Rv_b.ToString("f3"),
                "ΣRh(B)= " + Rh_b.ToString("f3"),
                "",
                "ΣM(B)= " + M_b.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_b = M_b / Rv_b;


            list.Add(string.Format("x = ΣM(B) / ΣRv(B)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_b = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_b = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(B) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_b, L, e_b));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (C)"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(C) = " + Wrc.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrc * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(C) = " + Wm_c.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(C) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(C) = " + Pqv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(C) = " + Pv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(C) = " + Pq_h_c.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h_c * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(C) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph_c * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_c = Wrc + Wm_c + Wq_c + Pqv_c + Pv_c;
            double Rh_c = Pq_h_c + Ph_c;
            double M_c = (Wrc * L / 2.0) + (Wm_c * L / 2.0) + (Wq_c * L / 2.0) + (Pqv_c * L) + (Pv_c * L) - (Pq_h_c * ((H1 + H2) / 2.0)) - (Ph_c * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(C)= " + Rv_c.ToString("f3"),
                "ΣRh(C)= " + Rh_c.ToString("f3"),
                "",
                "ΣM(C)= " + M_c.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_c = M_c / Rv_c;


            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_c, Rv_c));
            list.Add(string.Format("  = {0:f3} m", x_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_c = (L / 2.0) - x_c;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_c));
            list.Add(string.Format("                 = {0:f3} m", e_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_c = Rv_c / (L - 2 * e_c);

            list.Add(string.Format("q_req = ΣRv(C) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_c, L, e_c));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SUMMARY"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));

            format = "{0,9} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}";

            list.Add(string.Format(""));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "Case", "Rv   ", "Rh   ", "M   ", "e", "q_req"));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "", "(kN/m)  ", "(kN/m)  ", "(kN-m/m) ", "(m)", "(kpa)"));
            list.Add(string.Format(format, "A", Rv_a, Rh_a, M_a, e, q_req));
            list.Add(string.Format(format, "B", Rv_b, Rh_b, M_b, e_b, q_req_b));
            list.Add(string.Format(format, "C", Rv_c, Rh_c, M_c, e_c, q_req_c));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CHECK FOR SLIDING"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));


            Fp = 1.2;
            list.Add(string.Format("Partial Factor of Safety (Soil to Soil) = 1.2 (minimum), (Ref: Table 16, BS : 8006 - 1995)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Retained Earth"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φ1)/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            double FoS_A = (Rv_a * Math.Tan(_phi1)) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_a, phi1, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_A, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double FoS_B = (Rv_b * Math.Tan(_phi1)) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_b, phi1, Rh_b));
            if (FoS_B > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_B, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS_C = (Rv_c * Math.Tan(_phi1)) / Rh_c;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_c, phi1, Rh_c));
            if (FoS_C > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Foundation"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_a, phi_f, C, L, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_b, phi_f, C, L, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / Rh_c;
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_c, phi_f, C, L, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Slip in Retained Earth for Over Design"));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available Factor of Safety (FoS) for Over Design = (Rv * tan φ1)/(1.2 * Rh)"));

            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi1)) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_a, phi1, Fp, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_A, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi1)) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_b, phi1, Fp, Rh_b));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_B, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi1)) / (Fp * Rh_c);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_c, phi1, Fp, Rh_c));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Foundation for Over Design"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_a, phi_f, C, L, Fp, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_b, phi_f, C, L, Fp, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / (Fp * Rh_c);
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_c, phi_f, C, L, Fp, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CHECK FOR OVERTURING"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS = 1.5;

            list.Add(string.Format("Factor of Safety (FoS) for Overturnig = {0} (minimum)", FoS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("FoS for Overturnig at Toe = Σ[Resisting Moment (+ve)]/Σ[Disturbing Moment (-ve)]", FoS));
            list.Add(string.Format(""));

            FoS_A = Rv_a / Rh_a;
            if (FoS_A > FoS)
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_a, Rh_a, FoS_A, FoS));
            else
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_a, Rh_a, FoS_A, FoS));

            FoS_B = Rv_b / Rh_b;
            if (FoS_B > FoS)
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_b, Rh_b, FoS_B, FoS));
            else
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_b, Rh_b, FoS_B, FoS));

            FoS_C = Rv_c / Rh_c;
            if (FoS_C > FoS)
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_c, Rh_c, FoS_C, FoS));
            else
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_c, Rh_c, FoS_C, FoS));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CALCULATION FOR TENSION IN STRIPS FOR INTERNAL STABILITY"));
            list.Add(string.Format("--------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));

            Fp = 1.5;
            list.Add(string.Format("Fp = Partial Safety Factor = {0}", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("(a)  Vertical Forces"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wr = Weight of Road Crust above Retained Earth"));

            double Wra = Dp * gama1_max * L * Fp;

            list.Add(string.Format("   = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wra));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Wm = Weight of Retained Earth mass"));


            Wm_a = Zi * gama1_max * L * Fp;
            list.Add(string.Format("   = Zi * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Zi, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wm_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic Load above Retained Earth mass"));
            Wq_a = L * q * Fp;
            list.Add(string.Format("   = L * q * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv = Vertical Component of Pressure by Surcharge behind Retained Earth mass"));
            list.Add(string.Format("    = Pq * Sin δ * Fp"));

            Pqv_a = (K2 * q * (Zi + Dp)) * Math.Sin(_delta) * Fp;
            list.Add(string.Format("    = (K2 * q * (Zi + Dp)) * Sin δ * Fp"));
            list.Add(string.Format("    = ({0:f3} * {1:f3} * ({2} + {3})) * Sin ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pqv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv = Vertical Component of Earth Pressure by Retained Earth mass"));

            Pv_a = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Sin(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Sin δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Sin ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Pv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b) Horizontal Forces :"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh = Horizontal Component of Pressure by Surcharge on Retained Earth mass"));

            Pq_h = K2 * q * (Zi + Dp) * Math.Cos(_delta) * Fp;
            list.Add(string.Format("    = K2 * q * (Zi+Dp) * Cos δ * Fp"));
            list.Add(string.Format("    = {0:f3} * {1} * ({2} + {3}) * Cos ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by Retained Earth mass"));

            Ph = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Cos(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Cos ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CALCULAION FOR DRIVING AND RESISTING MOMENT"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            format = "{0,-7} {1,-10} {2,18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr = " + Wra.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wra * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh = " + Pq_h.ToString("f3"),
                "(Zi+Dp)/2 = " + ((Zi + Dp) / 2.0).ToString("f3"),
                -(Pq_h * ((Zi + Dp) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph = " + Ph.ToString("f3"),
                "(Zi+Dp)/3 = " + ((Zi + Dp) / 3.0).ToString("f3"),
                -(Ph * (Zi + Dp) / 3.0)));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            Rv_a = Wra + Wm_a + Wq_a + Pqv_a + Pv_a;
            Rh_a = Pq_h + Ph;
            M_a = (Wra * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((Zi + Dp) / 2.0)) - (Ph * ((Zi + Dp) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv = " + Rv_a.ToString("f3"),
                "ΣRh = " + Rh_a.ToString("f3"),
                "",
                "ΣM = " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM / ΣRv "));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_v = Rv_a / (2 * x_a);
            list.Add(string.Format("Vertical Pressure = σ_v = ΣRv / (2*x)"));
            list.Add(string.Format("                        = {0:f3} / (2 * {1:f3})", Rv_a, x_a));
            list.Add(string.Format("                        = {0:f3} kPa", sigma_v));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Hense Horizontal Pressure = σ_h = σ_v * Ki"));
            list.Add(string.Format(""));

            double Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1;
            double sigma_h = sigma_v * Ki;
            list.Add(string.Format(""));
            list.Add(string.Format("By interpolating from Figure 2, with values of ka1, ka2, Zi and H1, we get the value of Ki"));
            list.Add(string.Format(""));

            list.Add(string.Format("Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1"));
            list.Add(string.Format("   = (({0:f3} - {1:f3}) * ({2:f3} - ({3:f3} + {4:f3})) / {5:f3}) + {1:f3}", ka2, ka1, H1, Zi, Dp, H1));
            list.Add(string.Format("   = {0:f3} ", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_h = σ_v * Ki"));
            list.Add(string.Format("    = {0:f3} * {1:f3}", sigma_v, Ki));
            list.Add(string.Format("    = {0:f3} kPa", sigma_h));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical spacing of strip layers = ∆i = {0} m", del_i));
            list.Add(string.Format("Calculation side for Strips = wi = {0} m", wi));
            list.Add(string.Format(""));

            double Total_Tmax = sigma_h * wi * del_i;

            list.Add(string.Format("Total Tension in Strips = Total_Tmax = σ_h * wi * ∆i"));
            list.Add(string.Format("                                     = {0:f3} * {1} * {2}", sigma_h, wi, del_i));
            list.Add(string.Format("                                     = {0:f3} kN", sigma_h, wi, del_i));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Number of Strips in the Layers = Tot_layers = {0} nos", tot_layers));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tmax = (Total_Tmax / tot_layers);
            list.Add(string.Format("Tension per Strip = Tmax = Total_Tmax / Tot_layers"));
            list.Add(string.Format("                         = {0:f3} / {1:f0}", Total_Tmax, tot_layers));
            list.Add(string.Format("                         = {0:f3} kN", Tmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("Height from Top = Zi = {0} m", Zi));
            list.Add(string.Format(""));
            list.Add(string.Format("Ki = {0:f4}", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format("Delta ∆i = {0:f3} m", del_i));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Strips (for {0} m width) = {1} ", wi, tot_layers));
            list.Add(string.Format(""));


            list.Add(string.Format("Force At Connection = {0}%", cf));
            list.Add(string.Format(""));



            list.Add(string.Format("Coefficient of Friction = fo = {0:f3}", fo));
            list.Add(string.Format(""));



            double La = 0.0;
            if (Zi >= 0.0 && Zi < (0.6 * Hm))
            {
                La = L - (0.3 * Hm - (Zi / 6.0));
                list.Add(string.Format("Adherence Length = La = L - (0.3 * Hm - (Zi / 6.0))"));
                list.Add(string.Format("                      = {0:f3} - (0.3 * {1:f3} - ({2:f3} / 6.0))", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            else
            {
                La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0);
                list.Add(string.Format("Adherence Length = La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0)"));
                list.Add(string.Format("                      = {0:f3}  - ((0.2 * {1:f3} ) - ({2:f3}  - 0.6 * {1:f3} ) / 2.0)", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _strip_strength = 37.5;

            list.Add(string.Format("Ultimate Tensile Strength of Polymetric strip = {0:f3} kN", _strip_strength));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for Connection strength"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _To = Tmax * (cf / 100.0);


            list.Add(string.Format("Tension in strip at connection = To = % Force x Tmax"));
            list.Add(string.Format("                                    = {0:f3} x {1}%", Tmax, cf));
            list.Add(string.Format("                                    = {0:f3} kN", _To));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tr = 18.88;
            double Tro = Tr;

            list.Add(string.Format("Residual strength of strip (after connection) = Tr = {0:f3} kN", Tr));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Residual strength of strip at connection  = Tro = {0:f3} kN", Tro));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for F.O.S"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));

            double Tr_by_Tmax = (Tr / Tmax);
            list.Add(string.Format("Over design factor (strip length)  = Tr / Tmax = {0:f3} / {1:f3} = {2:f3}", Tr, Tmax, Tr_by_Tmax));
            list.Add(string.Format(""));
            double Tro_by_Tmax = (Tr / Tmax);
            list.Add(string.Format(""));
            list.Add(string.Format("Over design factor (connection)  = Tro / Tmax = {0:f3} / {1:f3} = {2:f3}", Tr, Tmax, Tro_by_Tmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficient of friction : f* "));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("f* (µ in BS 8006) is the friction factor for soil/strip interaction."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("           f*o   =  1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Fp = 1.5;

            double wst = (Zi + Dp) * La * gama1_max * 1.5;


            list.Add(string.Format("Wst = Weight of fill on 'Tail' of Strip (La) "));
            list.Add(string.Format("    = (Zi + Dp) * La * γ1_max * 1.5"));
            list.Add(string.Format("    = ({0:f3} + {1:f3}) * {2:f3} * {3:f3} * 1.5", Zi, Dp, La, gama1_max));
            list.Add(string.Format("    = {0:f3} kN/m", wst));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic load above Strip "));

            double Wq = La * q * Fp;
            list.Add(string.Format("   = La * q * Fp "));
            list.Add(string.Format("   = {0:f3} * {1:f3} * {2} ", La, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double des_width = 49.0;
            list.Add(string.Format("Hence, Tf per strip for polymetric strip ({0}mm minimum design width)", des_width));
            list.Add(string.Format(""));

            double Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1;

            list.Add(string.Format(" Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1"));
            list.Add(string.Format("    = (({0:f3} + {1:f3}) * {2:f3} * 2 * ({3:f3} / 1000.0)) / 1.3 * 1.1", wst, Wq, fo, des_width));
            list.Add(string.Format("    = {0:f3} kN", Tf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tf_by_Tmax = (Tf / Tmax);
            list.Add(string.Format("Tf / Tmax = ({0:f3} / {1:f3}) = {2:f3}", Tf, Tmax, Tf_by_Tmax));


            File.WriteAllLines(Report_File, list.ToArray());
            //iapp.View_Result(Report_File);
        }

        //Chiranjit [2013 04 18]
        public void Loop_Sub_Program1(ref List<string> list, Reinforcement_Layout strip_sec)
        {

            //List<string> list = new List<string>();

            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));
            //list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("EXTERNAL STABILITY");
            list.Add("------------------");
            list.Add("");
            list.Add("");
            list.Add("EARTH PRESSURE");
            list.Add("---------------");
            list.Add("");
            list.Add("");
            list.Add("Inclination of Earth Pressure at the back of RE Wall");
            list.Add("");


            double val1, val2;

            val1 = (1.2 - L / H1) * phi2;
            val2 = 0.8 * (1 - 0.7 * L / Hm) * phi1;
            double delta = val2;
            list.Add(string.Format(" δ = (1.2 - L/Hm) x φ2   OR 0.8 x (1- 0.7 x L/Hm) x φ1  (for B = {0}°) ", B));
            list.Add(string.Format("   = 0.8 x (1- 0.7 x {0}/{1}) x {2}", L, Hm, phi1));
            list.Add(string.Format("   = 0.8 x (1- {0:f4}) x {1}", (0.7 * L / Hm), phi1));
            list.Add(string.Format("   = {0:f4} ", delta));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Obtain Coefficient K2 from Coulomb"));
            list.Add(string.Format(""));



            double _B = MyList.Convert_Degree_To_Radian(B);
            double _w = MyList.Convert_Degree_To_Radian(w);
            double _Bs = MyList.Convert_Degree_To_Radian(Bs);
            double _phi1 = MyList.Convert_Degree_To_Radian(phi1);
            double _phi2 = MyList.Convert_Degree_To_Radian(phi2);
            double _phi_f = MyList.Convert_Degree_To_Radian(phi_f);

            double _delta = MyList.Convert_Degree_To_Radian(delta);





            double K2 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta)) / (1 + Math.Pow(Math.Sqrt(Math.Sin(_phi1 + _delta) * Math.Sin(_phi1 - _w) / (Math.Cos(_delta) * Math.Cos(_w))), 2));



            val1 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta));
            val2 = Math.Sin(_phi1 + _delta);

            double val3 = Math.Sin(_phi1 - _w);
            double val4 = (Math.Cos(_delta) * Math.Cos(_w));

            double val5 = (val2 * val3 / val4);

            double val6 = val1 / Math.Pow((1 + Math.Sqrt(val5)), 2);

            K2 = val6;


            list.Add(string.Format(" K2 = [(Cos φ2)^2/Cos δ]/[1 + √(Sin (φ1+δ)* Sin(φ1 - w)/ Cos δ * Cos w)]^2"));
            list.Add("");
            list.Add(string.Format("    = [(Cos {0})^2/Cos {1:f3}]/[1 + √(Sin ({2} + {1:f3})* Sin({2} - {3})/ Cos {1:f3} * Cos {3})]^2", phi2, delta, phi1, w));

            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3} * {2:f3} / {3:f3})]^2", val1, val2, val3, val4));
            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3})]^2", val1, val5));

            list.Add(string.Format("    = {0:f3}", K2));

            list.Add("");
            list.Add("");

            double ka1 = (1 - Math.Sin(_phi1)) / (1 + Math.Sin(_phi1));
            list.Add(string.Format(" ka1  = (1 - Sin φ1) / (1 + Sin φ1)"));
            list.Add(string.Format("      = (1 - Sin ({0})) / (1 + Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka1));
            list.Add("");
            list.Add("");
            double ka2 = (1 - Math.Sin(_phi1));

            list.Add(string.Format(" ka2  = (1 - Sin φ1) "));
            list.Add(string.Format("      = (1 - Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka2));
            list.Add("");
            list.Add("");
            list.Add("(A) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");
            list.Add("Partial Safety Factor for Loads = Fp = 1.5,  Table 16, BS : 8006");
            list.Add("");

            double Fp = 1.5;
            double Wr = Dp * gama1_max * L * Fp;
            list.Add(string.Format("Wr = Wt. of Pavement Crust above RE Wall"));
            list.Add(string.Format("   = Dp * γ1_max * L * Fp "));
            list.Add(string.Format("   = {0:f3} * {1} * {2} * {3} ", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wr));
            list.Add("");
            list.Add("");



            double Wm_a = (Hm - H2) * gama1_max * L * Fp;
            list.Add(string.Format("Wm (a) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format("       = (Hm - H2) * γ1_max * L * Fp"));
            list.Add(string.Format("       = ({0} - {1}) * {2} * {3} * {4}", Hm, H2, gama1_max, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Wq (a) = Traffic Load on backfill retained by RE Wall "));
            double Wq_a = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_a));
            list.Add("");

            list.Add("");
            list.Add(string.Format("Pqv (a) = Vertical Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pqv_a = (K2 * q) * (Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = Pq * Sin δ * Fp"));
            list.Add(string.Format("       = (K2 * q) * Hm * Sin δ * Fp"));
            list.Add(string.Format("       = ({0:f3} * {1}) * {2} * Sin {3:f3} * {4}", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pqv_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Pv (a) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            double Pv_a = (K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = (K2 * 0.5 * γ2 * Hm*Hm *  Sin δ * Fp)"));
            list.Add(string.Format("       = ({0:f3} * 0.5 * {1} * {2} * {2} * Sin {3:f3} * {4})", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_a));
            list.Add("");
            list.Add("");
            list.Add("(A) : HORIZONTAL FORCE");
            list.Add("-----------------------");
            list.Add("");
            list.Add(string.Format("Pqh = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(B) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");
            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrb = Weight of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrb = Dp * gama1_min * L * Fp;



            list.Add(string.Format("    = Dp * γ1_min * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_b = (Hm - H2) * gama1_min * L * Fp;


            list.Add(string.Format("Wm (b) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Fp = 1.5 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv (b) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            Fp = 1.5;
            double Pqv_b = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_b));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (b) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(B) : HORIZONTAL FORCE");
            list.Add("----------------------");
            list.Add("");
            list.Add(string.Format("Pqh (B) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_b = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (B) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("(C) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");

            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrc = Wt. of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrc = Dp * gama1_max * L * Fp;



            list.Add(string.Format("    = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrc));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_c = (Hm - H2) * gama1_max * L * Fp;


            list.Add(string.Format("Wm (c) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 0.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));

            list.Add("");
            list.Add(string.Format("Wq (c) = Traffic Load on backfill retained by RE Wall "));
            double Wq_c = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_c));
            list.Add("");
            list.Add("");


            list.Add(string.Format("Pqv (c) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            double Pqv_c = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_c));

            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = 1.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (c) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(C) : HORIZONTAL FORCE");
            list.Add("----------------------");
            list.Add("");
            Fp = 0.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh (C) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_c = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (C) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("COMPUTATION OF DRIVING & RESISTING MOMENT"));
            list.Add(string.Format("-----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (A)"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));
            string format = "{0,-7} {1,-10} {2,-18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(A) = " + Wr.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wr * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(A) = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(A) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(A) = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(A) = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(A) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(A) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_a = Wr + Wm_a + Wq_a + Pqv_a + Pv_a;
            double Rh_a = Pq_h + Ph;
            double M_a = (Wr * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((H1 + H2) / 2.0)) - (Ph * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(A)= " + Rv_a.ToString("f3"),
                "ΣRh(A)= " + Rh_a.ToString("f3"),
                "",
                "ΣM(A)= " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_a = M_a / Rv_a;




            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(A) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_a, L, e));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (B)"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(B) = " + Wrb.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrb * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(B) = " + Wm_b.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_b * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Pqv(B) = " + Pqv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pv(B) = " + Pv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "_____      ",
                "Pqh(B) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Ph(B) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_b = Wrb + Wm_b + Pqv_b + Pv_b;
            double Rh_b = Pq_h_b + Ph_b;
            double M_b = (Wrb * L / 2.0) + (Wm_b * L / 2.0) + (Pqv_b * L) + (Pv_b * L) - (Pq_h_b * ((H1 + H2) / 2.0)) - (Ph_b * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(B)= " + Rv_b.ToString("f3"),
                "ΣRh(B)= " + Rh_b.ToString("f3"),
                "",
                "ΣM(B)= " + M_b.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_b = M_b / Rv_b;


            list.Add(string.Format("x = ΣM(B) / ΣRv(B)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_b, Rv_b));
            list.Add(string.Format("  = {0:f3} m", x_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_b = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_b = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(B) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_b, L, e_b));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (C)"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(C) = " + Wrc.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrc * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(C) = " + Wm_c.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(C) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(C) = " + Pqv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(C) = " + Pv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(C) = " + Pq_h_c.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h_c * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(C) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph_c * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_c = Wrc + Wm_c + Wq_c + Pqv_c + Pv_c;
            double Rh_c = Pq_h_c + Ph_c;
            double M_c = (Wrc * L / 2.0) + (Wm_c * L / 2.0) + (Wq_c * L / 2.0) + (Pqv_c * L) + (Pv_c * L) - (Pq_h_c * ((H1 + H2) / 2.0)) - (Ph_c * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(C)= " + Rv_c.ToString("f3"),
                "ΣRh(C)= " + Rh_c.ToString("f3"),
                "",
                "ΣM(C)= " + M_c.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_c = M_c / Rv_c;


            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_c, Rv_c));
            list.Add(string.Format("  = {0:f3} m", x_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_c = (L / 2.0) - x_c;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_c));
            list.Add(string.Format("                 = {0:f3} m", e_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_c = Rv_c / (L - 2 * e_c);

            list.Add(string.Format("q_req = ΣRv(C) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_c, L, e_c));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SUMMARY"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));

            format = "{0,9} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}";
            strip_sec.Ext_Stability_Table = new External_Stability();


            strip_sec.Ext_Stability_Table.Case.Add("A");
            strip_sec.Ext_Stability_Table.Case.Add("B");
            strip_sec.Ext_Stability_Table.Case.Add("C");

            strip_sec.Ext_Stability_Table.Rv.Add(Rv_a);
            strip_sec.Ext_Stability_Table.Rv.Add(Rv_b);
            strip_sec.Ext_Stability_Table.Rv.Add(Rv_c);
            strip_sec.Ext_Stability_Table.Rh.Add(Rh_a);
            strip_sec.Ext_Stability_Table.Rh.Add(Rh_b);
            strip_sec.Ext_Stability_Table.Rh.Add(Rh_c);
            strip_sec.Ext_Stability_Table.M.Add(M_a);
            strip_sec.Ext_Stability_Table.M.Add(M_b);
            strip_sec.Ext_Stability_Table.M.Add(M_c);
            strip_sec.Ext_Stability_Table.q_ref.Add(q_req);
            strip_sec.Ext_Stability_Table.q_ref.Add(q_req_b);
            strip_sec.Ext_Stability_Table.q_ref.Add(q_req_c);

            strip_sec.Ext_Stability_Table._2_x.Add(2 * x_a);
            strip_sec.Ext_Stability_Table._2_x.Add(2 * x_b);
            strip_sec.Ext_Stability_Table._2_x.Add(2 * x_c);




            list.Add(string.Format(""));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "Case", "Rv   ", "Rh   ", "M   ", "e", "q_req"));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "", "(kN/m)  ", "(kN/m)  ", "(kN-m/m) ", "(m)", "(kpa)"));
            list.Add(string.Format(format, "A", Rv_a, Rh_a, M_a, e, q_req));
            list.Add(string.Format(format, "B", Rv_b, Rh_b, M_b, e_b, q_req_b));
            list.Add(string.Format(format, "C", Rv_c, Rh_c, M_c, e_c, q_req_c));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CHECK FOR SLIDING"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));


            Fp = 1.2;
            list.Add(string.Format("Partial Factor of Safety (Soil to Soil) = 1.2 (minimum), (Ref: Table 16, BS : 8006 - 1995)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Retained Earth"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φ1)/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            double FoS_A = (Rv_a * Math.Tan(_phi1)) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_a, phi1, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_A, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double FoS_B = (Rv_b * Math.Tan(_phi1)) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_b, phi1, Rh_b));
            if (FoS_B > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_B, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS_C = (Rv_c * Math.Tan(_phi1)) / Rh_c;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_c, phi1, Rh_c));
            if (FoS_C > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Foundation"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_a, phi_f, C, L, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_b, phi_f, C, L, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / Rh_c;
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_c, phi_f, C, L, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Slip in Retained Earth for Over Design"));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available Factor of Safety (FoS) for Over Design = (Rv * tan φ1)/(1.2 * Rh)"));

            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi1)) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_a, phi1, Fp, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_A, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi1)) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_b, phi1, Fp, Rh_b));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_B, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi1)) / (Fp * Rh_c);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_c, phi1, Fp, Rh_c));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Foundation for Over Design"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_a, phi_f, C, L, Fp, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_b, phi_f, C, L, Fp, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / (Fp * Rh_c);
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_c, phi_f, C, L, Fp, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CHECK FOR OVERTURING"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS = 1.5;

            list.Add(string.Format("Factor of Safety (FoS) for Overturnig = {0} (minimum)", FoS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("FoS for Overturnig at Toe = Σ[Resisting Moment (+ve)]/Σ[Disturbing Moment (-ve)]", FoS));
            list.Add(string.Format(""));

            FoS_A = Rv_a / Rh_a;
            if (FoS_A > FoS)
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_a, Rh_a, FoS_A, FoS));
            else
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_a, Rh_a, FoS_A, FoS));

            FoS_B = Rv_b / Rh_b;
            if (FoS_B > FoS)
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_b, Rh_b, FoS_B, FoS));
            else
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_b, Rh_b, FoS_B, FoS));

            FoS_C = Rv_c / Rh_c;
            if (FoS_C > FoS)
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_c, Rh_c, FoS_C, FoS));
            else
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_c, Rh_c, FoS_C, FoS));


            list.Add(string.Format("INTERNAL STABILITY"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("CALCULATION FOR TENSION IN STRIPS FOR INTERNAL STABILITY"));
            list.Add(string.Format("--------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));

            Fp = 1.5;
            list.Add(string.Format("Fp = Partial Safety Factor = {0}", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("(a)  Vertical Forces"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wr = Weight of Road Crust above Retained Earth"));

            double Wra = Dp * gama1_max * L * Fp;

            list.Add(string.Format("   = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wra));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Wm = Weight of Retained Earth mass"));


            Wm_a = Zi * gama1_max * L * Fp;
            list.Add(string.Format("   = Zi * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Zi, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wm_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic Load above Retained Earth mass"));
            Wq_a = L * q * Fp;
            list.Add(string.Format("   = L * q * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv = Vertical Component of Pressure by Surcharge behind Retained Earth mass"));
            list.Add(string.Format("    = Pq * Sin δ * Fp"));

            Pqv_a = (K2 * q * (Zi + Dp)) * Math.Sin(_delta) * Fp;
            list.Add(string.Format("    = (K2 * q * (Zi + Dp)) * Sin δ * Fp"));
            list.Add(string.Format("    = ({0:f3} * {1:f3} * ({2} + {3})) * Sin ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pqv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv = Vertical Component of Earth Pressure by Retained Earth mass"));

            Pv_a = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Sin(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Sin δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Sin ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Pv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b) Horizontal Forces :"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh = Horizontal Component of Pressure by Surcharge on Retained Earth mass"));

            Pq_h = K2 * q * (Zi + Dp) * Math.Cos(_delta) * Fp;
            list.Add(string.Format("    = K2 * q * (Zi+Dp) * Cos δ * Fp"));
            list.Add(string.Format("    = {0:f3} * {1} * ({2} + {3}) * Cos ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by Retained Earth mass"));

            Ph = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Cos(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Cos ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CALCULAION FOR DRIVING AND RESISTING MOMENT"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            format = "{0,-7} {1,-10} {2,18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr = " + Wra.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wra * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh = " + Pq_h.ToString("f3"),
                "(Zi+Dp)/2 = " + ((Zi + Dp) / 2.0).ToString("f3"),
                -(Pq_h * ((Zi + Dp) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph = " + Ph.ToString("f3"),
                "(Zi+Dp)/3 = " + ((Zi + Dp) / 3.0).ToString("f3"),
                -(Ph * (Zi + Dp) / 3.0)));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            Rv_a = Wra + Wm_a + Wq_a + Pqv_a + Pv_a;
            Rh_a = Pq_h + Ph;
            M_a = (Wra * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((Zi + Dp) / 2.0)) - (Ph * ((Zi + Dp) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv = " + Rv_a.ToString("f3"),
                "ΣRh = " + Rh_a.ToString("f3"),
                "",
                "ΣM = " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM / ΣRv "));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_v = Rv_a / (2 * x_a);
            list.Add(string.Format("Vertical Pressure = σ_v = ΣRv / (2*x)"));
            list.Add(string.Format("                        = {0:f3} / (2 * {1:f3})", Rv_a, x_a));
            list.Add(string.Format("                        = {0:f3} kPa", sigma_v));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Hense Horizontal Pressure = σ_h = σ_v * Ki"));
            list.Add(string.Format(""));

            double Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1;

            strip_sec.k.Add(Ki);

            double sigma_h = sigma_v * Ki;

            strip_sec.sigma_h.Add(sigma_h);

            list.Add(string.Format(""));
            list.Add(string.Format("By interpolating from Figure 2, with values of ka1, ka2, Zi and H1, we get the value of Ki"));
            list.Add(string.Format(""));

            list.Add(string.Format("Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1"));
            list.Add(string.Format("   = (({0:f3} - {1:f3}) * ({2:f3} - ({3:f3} + {4:f3})) / {5:f3}) + {1:f3}", ka2, ka1, H1, Zi, Dp, H1));
            list.Add(string.Format("   = {0:f3} ", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_h = σ_v * Ki"));
            list.Add(string.Format("    = {0:f3} * {1:f3}", sigma_v, Ki));
            list.Add(string.Format("    = {0:f3} kPa", sigma_h));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical spacing of strip layers = ∆i = {0} m", del_i));
            list.Add(string.Format("Calculation side for Strips = wi = {0} m", wi));
            list.Add(string.Format(""));

            double Total_Tmax = sigma_h * wi * del_i;

            list.Add(string.Format("Total Tension in Strips = Total_Tmax = σ_h * wi * ∆i"));
            list.Add(string.Format("                                     = {0:f3} * {1} * {2}", sigma_h, wi, del_i));
            list.Add(string.Format("                                     = {0:f3} kN", sigma_h, wi, del_i));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Number of Strips in the Layers = Tot_layers = {0} nos", tot_layers));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tmax = (Total_Tmax / tot_layers);

            strip_sec.Tmax.Add(Tmax);

            list.Add(string.Format("Tension per Strip = Tmax = Total_Tmax / Tot_layers"));
            list.Add(string.Format("                         = {0:f3} / {1:f0}", Total_Tmax, tot_layers));
            list.Add(string.Format("                         = {0:f3} kN", Tmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("Height from Top = Zi = {0} m", Zi));
            list.Add(string.Format(""));
            list.Add(string.Format("Ki = {0:f4}", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format("Delta ∆i = {0:f3} m", del_i));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Strips (for {0} m width) = {1} ", wi, tot_layers));
            list.Add(string.Format(""));


            list.Add(string.Format("Force At Connection = {0}%", cf));
            list.Add(string.Format(""));



            list.Add(string.Format("Coefficient of Friction = fo = {0:f3}", fo));
            list.Add(string.Format(""));



            double La = 0.0;
            if (Zi >= 0.0 && Zi < (0.6 * Hm))
            {
                La = L - (0.3 * Hm - (Zi / 6.0));
                list.Add(string.Format("Adherence Length = La = L - (0.3 * Hm - (Zi / 6.0))"));
                list.Add(string.Format("                      = {0:f3} - (0.3 * {1:f3} - ({2:f3} / 6.0))", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            else
            {
                La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0);
                list.Add(string.Format("Adherence Length = La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0)"));
                list.Add(string.Format("                      = {0:f3}  - ((0.2 * {1:f3} ) - ({2:f3}  - 0.6 * {1:f3} ) / 2.0)", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _strip_strength = strip_sec.Strip_Strength;

            list.Add(string.Format("Ultimate Tensile Strength of Polymetric strip = {0:f3} kN", _strip_strength));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for Connection strength"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _To = Tmax * (cf / 100.0);

            strip_sec.To.Add(_To);


            list.Add(string.Format("Tension in strip at connection = To = % Force x Tmax"));
            list.Add(string.Format("                                    = {0:f3} x {1}%", Tmax, cf));
            list.Add(string.Format("                                    = {0:f3} kN", _To));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tr = 18.88;


            //foreach (var item in Strip_Properties)
            //{
            //    if (item.Ultimate_Tensile_Strength == _strip_strength)
            //        Tr = item.Long_Term_Design_Strength_Consider;
            //}

            double Tro = Tr;



            list.Add(string.Format("Residual strength of strip (after connection) = Tr = {0:f3} kN", Tr));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Residual strength of strip at connection  = Tro = {0:f3} kN", Tro));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for F.O.S"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));

            double Tr_by_Tmax = (Tr / Tmax);

            strip_sec.Tr_by_Tm.Add(Tr_by_Tmax);
            strip_sec.Tro_by_To.Add((Tro / _To));



            list.Add(string.Format("Over design factor (strip length)  = Tr / Tmax = {0:f3} / {1:f3} = {2:f3}", Tr, Tmax, Tr_by_Tmax));
            list.Add(string.Format(""));
            double Tro_by_Tmax = (Tr / Tmax);
            list.Add(string.Format(""));
            list.Add(string.Format("Over design factor (connection)  = Tro / Tmax = {0:f3} / {1:f3} = {2:f3}", Tr, Tmax, Tro_by_Tmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficient of friction : f* "));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("f* (µ in BS 8006) is the friction factor for soil/strip interaction."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("           f*o   =  1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Fp = 1.5;

            double wst = (Zi + Dp) * La * gama1_max * 1.5;


            list.Add(string.Format("Wst = Weight of fill on 'Tail' of Strip (La) "));
            list.Add(string.Format("    = (Zi + Dp) * La * γ1_max * 1.5"));
            list.Add(string.Format("    = ({0:f3} + {1:f3}) * {2:f3} * {3:f3} * 1.5", Zi, Dp, La, gama1_max));
            list.Add(string.Format("    = {0:f3} kN/m", wst));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic load above Strip "));

            double Wq = La * q * Fp;
            list.Add(string.Format("   = La * q * Fp "));
            list.Add(string.Format("   = {0:f3} * {1:f3} * {2} ", La, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double des_width = 49.0;
            list.Add(string.Format("Hence, Tf per strip for polymetric strip ({0}mm minimum design width)", des_width));
            list.Add(string.Format(""));

            double Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1;
            strip_sec.Tf.Add(Tf);

            list.Add(string.Format(" Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1"));
            list.Add(string.Format("    = (({0:f3} + {1:f3}) * {2:f3} * 2 * ({3:f3} / 1000.0)) / 1.3 * 1.1", wst, Wq, fo, des_width));
            list.Add(string.Format("    = {0:f3} kN", Tf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tf_by_Tmax = (Tf / Tmax);
            list.Add(string.Format("Tf / Tmax = ({0:f3} / {1:f3}) = {2:f3}", Tf, Tmax, Tf_by_Tmax));
            strip_sec.Tf_by_Tm.Add(Tf_by_Tmax);


            File.WriteAllLines(Report_File, list.ToArray());
            //iapp.View_Result(Report_File);
        }

        public void Loop_Sub_Program(ref List<string> list, Reinforcement_Layout strip_sec)
        {

            //List<string> list = new List<string>();

            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));
            //list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("EXTERNAL STABILITY");
            list.Add("------------------");
            list.Add("");
            list.Add("");
            list.Add("EARTH PRESSURE");
            list.Add("---------------");
            list.Add("");
            list.Add("");
            list.Add("Inclination of Earth Pressure at the back of RE Wall");
            list.Add("");


            double val1, val2;

            val1 = (1.2 - L / Hm) * phi2;
            //val1 = (1.2 - L / H1) * phi2;
            val2 = 0.8 * (1 - 0.7 * L / Hm) * phi1;
            double delta = val2;

            //Chiranjit [2013 04 22]
            strip_sec.y = Hm;
            strip_sec.delta = delta;


            list.Add(string.Format(" δ = (1.2 - L/Hm) x φ2   OR 0.8 x (1- 0.7 x L/Hm) x φ1  (for B = {0}°,  Hm = H1+H2 = {1}+{2}={3}) ", B, H1, H2, Hm));

            list.Add(string.Format("   = (1.2 - {0}/{1}) x {2}", L, Hm, phi2));
            list.Add(string.Format("   = {0:f3}", val1));
            list.Add("");
            list.Add(string.Format("      OR "));
            list.Add("");

            list.Add(string.Format("   = 0.8 x (1- 0.7 x {0}/{1}) x {2}", L, Hm, phi1));
            list.Add(string.Format("   = 0.8 x (1- {0:f4}) x {1}", (0.7 * L / Hm), phi1));
            list.Add(string.Format("   = {0:f4} ", delta));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Obtain Coefficient K2 from Coulomb"));
            list.Add(string.Format(""));



            double _B = MyList.Convert_Degree_To_Radian(B);
            double _w = MyList.Convert_Degree_To_Radian(w);
            double _Bs = MyList.Convert_Degree_To_Radian(Bs);
            double _phi1 = MyList.Convert_Degree_To_Radian(phi1);
            double _phi2 = MyList.Convert_Degree_To_Radian(phi2);
            double _phi_f = MyList.Convert_Degree_To_Radian(phi_f);

            double _delta = MyList.Convert_Degree_To_Radian(delta);





            double K2 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta)) / (1 + Math.Pow(Math.Sqrt(Math.Sin(_phi1 + _delta) * Math.Sin(_phi1 - _w) / (Math.Cos(_delta) * Math.Cos(_w))), 2));



            val1 = ((Math.Cos(_phi2) * Math.Cos(_phi2)) / Math.Cos(_delta));
            val2 = Math.Sin(_phi1 + _delta);

            double val3 = Math.Sin(_phi1 - _w);
            double val4 = (Math.Cos(_delta) * Math.Cos(_w));

            double val5 = (val2 * val3 / val4);

            double val6 = val1 / Math.Pow((1 + Math.Sqrt(val5)), 2);

            K2 = val6;


            //Chiranjit [2013 04 22]
            strip_sec.k2y = K2;

            list.Add(string.Format(" K2 = [(Cos φ2)^2/Cos δ]/[1 + √(Sin (φ1+δ)* Sin(φ1 - w)/ Cos δ * Cos w)]^2"));
            list.Add("");
            list.Add(string.Format("    = [(Cos {0})^2/Cos {1:f3}]/[1 + √(Sin ({2} + {1:f3})* Sin({2} - {3})/ Cos {1:f3} * Cos {3})]^2", phi2, delta, phi1, w));

            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3} * {2:f3} / {3:f3})]^2", val1, val2, val3, val4));
            list.Add(string.Format("    = {0:f3}/[1 + √({1:f3})]^2", val1, val5));

            list.Add(string.Format("    = {0:f3}", K2));

            list.Add("");
            list.Add("");

            double ka1 = (1 - Math.Sin(_phi1)) / (1 + Math.Sin(_phi1));
            list.Add(string.Format(" ka1  = (1 - Sin φ1) / (1 + Sin φ1)"));
            list.Add(string.Format("      = (1 - Sin ({0})) / (1 + Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka1));
            list.Add("");
            list.Add("");
            double ka2 = (1 - Math.Sin(_phi1));

            list.Add(string.Format(" ka2  = (1 - Sin φ1) "));
            list.Add(string.Format("      = (1 - Sin ({0}))", phi1));
            list.Add(string.Format("      = {0:f3}", ka2));
            list.Add("");
            list.Add("");
            list.Add("(A) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");
            list.Add("Partial Safety Factor for Loads = Fp = 1.5,  Table 16, BS : 8006");
            list.Add("");

            double Fp = 1.5;
            double Wr = Dp * gama1_max * L * Fp;
            list.Add(string.Format("Wr = Wt. of Pavement Crust above RE Wall"));
            list.Add(string.Format("   = Dp * γ1_max * L * Fp "));
            list.Add(string.Format("   = {0:f3} * {1} * {2} * {3} ", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wr));
            list.Add("");
            list.Add("");



            double Wm_a = (Hm - H2) * gama1_max * L * Fp;
            list.Add(string.Format("Wm (a) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format("       = (Hm - H2) * γ1_max * L * Fp"));
            list.Add(string.Format("       = ({0} - {1}) * {2} * {3} * {4}", Hm, H2, gama1_max, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Wq (a) = Traffic Load on backfill retained by RE Wall "));
            double Wq_a = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_a));
            list.Add("");

            list.Add("");
            list.Add(string.Format("Pqv (a) = Vertical Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pqv_a = (K2 * q) * (Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = Pq * Sin δ * Fp"));
            list.Add(string.Format("       = (K2 * q) * Hm * Sin δ * Fp"));
            list.Add(string.Format("       = ({0:f3} * {1}) * {2} * Sin {3:f3} * {4}", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pqv_a));
            list.Add("");
            list.Add("");
            list.Add(string.Format("Pv (a) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            double Pv_a = (K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp);
            list.Add(string.Format("       = (K2 * 0.5 * γ2 * Hm*Hm *  Sin δ * Fp)"));
            list.Add(string.Format("       = ({0:f3} * 0.5 * {1} * {2} * {2} * Sin {3:f3} * {4})", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_a));
            list.Add("");
            list.Add("");
            list.Add("(A) : HORIZONTAL FORCE");
            list.Add("-----------------------");
            list.Add("");
            list.Add(string.Format("Pqh = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(B) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");
            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrb = Weight of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrb = Dp * gama1_min * L * Fp;



            list.Add(string.Format("    = Dp * γ1_min * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_b = (Hm - H2) * gama1_min * L * Fp;


            list.Add(string.Format("Wm (b) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3} * {4}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_b));
            list.Add(string.Format(""));
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Wq_b = 0.0;
            list.Add(string.Format("Wq (b) = Traffic Load on backfill mass retained by RE Wall = 0.0"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Fp = 1.5 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv (b) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            Fp = 1.5;
            double Pqv_b = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_b));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (b) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(B) : HORIZONTAL FORCE");
            list.Add("----------------------");
            list.Add("");
            list.Add(string.Format("Pqh (B) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_b = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (B) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_b = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("(C) : VERTICAL FORCES");
            list.Add("---------------------");
            list.Add("");

            Fp = 1.0;

            list.Add(string.Format("Fp = 1.0 = Partial Safety Factor for Loads, Table 16, BS : 8006"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wrc = Wt. of Pavement Crust above retained earth wall"));
            list.Add(string.Format(""));

            double Wrc = Dp * gama1_max * L * Fp;



            list.Add(string.Format("    = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("    = {0:f3} * {1:f3} * {2:f3} * {3:f3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Wrc));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Wm_c = (Hm - H2) * gama1_max * L * Fp;


            list.Add(string.Format("Wm (c) = Weight of  backfill mass retained by RE Wall"));
            list.Add(string.Format(""));
            list.Add(string.Format("       = (Hm - H2) * γ1_min * L * Fp"));
            list.Add(string.Format("       = ({0:f3} - {1:f3}) * {2:f3} * {3:f3}", Hm, H2, gama1_min, L, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wm_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 0.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));

            list.Add("");
            list.Add(string.Format("Wq (c) = Traffic Load on backfill retained by RE Wall "));
            double Wq_c = L * q * Fp;
            list.Add(string.Format("       = L * q * Fp"));
            list.Add(string.Format("       = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Wq_c));
            list.Add("");
            list.Add("");


            list.Add(string.Format("Pqv (c) = Vertical Component of Pressure by surcharge on backfill mass retained by RE Wall"));
            list.Add(string.Format("        = Pq * Sin δ * Fp"));
            list.Add(string.Format("        = (K2 * q) * Hm * Sin δ * Fp "));
            double Pqv_c = (K2 * q) * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("        = ({0:f3} * {1:f3}) * {2:f3} * Sin {3:f3} * {4:f3} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("        = {0:f3} kN/m", Pqv_c));

            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = 1.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv (c) = Vertical Component of Earth Pressure by backfill mass retained by RE Wall"));
            list.Add(string.Format(""));

            double Pv_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Sin(_delta) * Fp;
            list.Add(string.Format("       = K2 * 0.5 * γ2 * Hm^2 * Sin δ * Fp"));
            list.Add(string.Format("       = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Sin {3:f3} * 1.5", K2, gama2, Hm, delta));
            list.Add(string.Format("       = {0:f3} kN/m", Pv_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add("");
            list.Add("(C) : HORIZONTAL FORCE");
            list.Add("----------------------");
            list.Add("");
            Fp = 0.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh (C) = Horizontal Component of pressure by Surcharge on Backfill mass retained by RE Wall"));
            double Pq_h_c = (K2 * q) * Hm * Math.Cos(_delta) * Fp;



            list.Add(string.Format("       = (K2 * q) * Hm *  Cos δ * Fp"));
            list.Add(string.Format("       = {0:f3} * {1} * {2} * Cos {3:f3} * {4} ", K2, q, Hm, delta, Fp));
            list.Add(string.Format("       = {0:f3} kN/m", Pq_h_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Fp = 1.0;
            list.Add(string.Format("Fp = 0.0 = Partial safety Factor for Loads"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph (C) = Horizontal Component of Earth Pressure by backfill mass"));
            list.Add(string.Format(""));

            double Ph_c = K2 * 0.5 * gama2 * Hm * Hm * Math.Cos(_delta) * Fp;
            list.Add(string.Format("   = K2 * 0.5 * γ2 * Hm^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1:f3} * {2:f3}^2 * Cos {3:f3} * {4:f3}", K2, gama2, Hm, delta, Fp));
            list.Add(string.Format("   = {0:f3}  kN/m", Ph_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("COMPUTATION OF DRIVING & RESISTING MOMENT"));
            list.Add(string.Format("-----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (A)"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));
            string format = "{0,-7} {1,-10} {2,-18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr(A) = " + Wr.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wr * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm(A) = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq(A) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(A) = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(A) = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(A) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(A) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_a = Wr + Wm_a + Wq_a + Pqv_a + Pv_a;
            double Rh_a = Pq_h + Ph;
            double M_a = (Wr * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((H1 + H2) / 2.0)) - (Ph * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(A)= " + Rv_a.ToString("f3"),
                "ΣRh(A)= " + Rh_a.ToString("f3"),
                "",
                "ΣM(A)= " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_a = M_a / Rv_a;




            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e = (L / 2.0) - x_a;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_a));
            list.Add(string.Format("                 = {0:f3} m", e));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(A) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_a, L, e));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (B)"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.0",
                "Wr(B) = " + Wrb.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrb * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.0",
                "Wm(B) = " + Wm_b.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_b * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "0.0",
                "Wq(B) = " + Wq_b.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_b * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv(B) = " + Pqv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv(B) = " + Pv_b.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_b * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh(B) = " + Pq_h.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph(B) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_b = Wrb + Wm_b + Pqv_b + Pv_b;
            double Rh_b = Pq_h_b + Ph_b;
            double M_b = (Wrb * L / 2.0) + (Wm_b * L / 2.0) + (Pqv_b * L) + (Pv_b * L) - (Pq_h_b * ((H1 + H2) / 2.0)) - (Ph_b * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(B)= " + Rv_b.ToString("f3"),
                "ΣRh(B)= " + Rh_b.ToString("f3"),
                "",
                "ΣM(B)= " + M_b.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_b = M_b / Rv_b;


            list.Add(string.Format("x = ΣM(B) / ΣRv(B)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_b, Rv_b));
            list.Add(string.Format("  = {0:f3} m", x_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_b = (L / 2.0) - x_b;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_b));
            list.Add(string.Format("                 = {0:f3} m", e_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_b = Rv_a / (L - 2 * e);

            list.Add(string.Format("q_req = ΣRv(B) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_b, L, e_b));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CASE (C)"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.0",
                "Wr(C) = " + Wrc.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wrc * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.0",
                "Wm(C) = " + Wm_c.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "0.0",
                "Wq(C) = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_c * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "0.0",
                "Pqv(C) = " + Pqv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.0",
                "Pv(C) = " + Pv_c.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_c * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "0.0",
                "_____      ",
                "Pqh(C) = " + Pq_h_c.ToString("f3"),
                "(H1+H2)/2 = " + ((H1 + H2) / 2.0).ToString("f3"),
                -(Pq_h_c * ((H1 + H2) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.0",
                "_____      ",
                "Ph(C) = " + Ph.ToString("f3"),
                "(H1+H2)/3 = " + ((H1 + H2) / 3.0).ToString("f3"),
                -(Ph_c * ((H1 + H2) / 3.0))));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            double Rv_c = Wrc + Wm_c + Wq_c + Pqv_c + Pv_c;
            double Rh_c = Pq_h_c + Ph_c;
            double M_c = (Wrc * L / 2.0) + (Wm_c * L / 2.0) + (Wq_c * L / 2.0) + (Pqv_c * L) + (Pv_c * L) - (Pq_h_c * ((H1 + H2) / 2.0)) - (Ph_c * ((H1 + H2) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv(C)= " + Rv_c.ToString("f3"),
                "ΣRh(C)= " + Rh_c.ToString("f3"),
                "",
                "ΣM(C)= " + M_c.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            double x_c = M_c / Rv_c;


            list.Add(string.Format("x = ΣM(A) / ΣRv(A)"));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_c, Rv_c));
            list.Add(string.Format("  = {0:f3} m", x_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double e_c = (L / 2.0) - x_c;


            list.Add(string.Format("Eccentricity = e = (L / 2) - x"));
            list.Add(string.Format("                 = ({0} / 2) - {1:f3}", L, x_c));
            list.Add(string.Format("                 = {0:f3} m", e_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical Stress at the base of the Wall, q_req following Meyerhof's equation"));
            list.Add(string.Format(""));

            double q_req_c = Rv_c / (L - 2 * e_c);

            list.Add(string.Format("q_req = ΣRv(C) / (L - 2 * e)"));
            list.Add(string.Format("      = {0:f3} / ({1} - 2 * {2:f3})", Rv_c, L, e_c));
            list.Add(string.Format("      = {0:f3} kN/sq.m.", q_req_c));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SUMMARY"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));

            format = "{0,9} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}";
            strip_sec.Ext_Stability_Table = new External_Stability();


            strip_sec.Ext_Stability_Table.Case.Add("A");
            strip_sec.Ext_Stability_Table.Case.Add("B");
            strip_sec.Ext_Stability_Table.Case.Add("C");

            strip_sec.Ext_Stability_Table.Rv.Add(Rv_a);
            strip_sec.Ext_Stability_Table.Rv.Add(Rv_b);
            strip_sec.Ext_Stability_Table.Rv.Add(Rv_c);
            strip_sec.Ext_Stability_Table.Rh.Add(Rh_a);
            strip_sec.Ext_Stability_Table.Rh.Add(Rh_b);
            strip_sec.Ext_Stability_Table.Rh.Add(Rh_c);
            strip_sec.Ext_Stability_Table.M.Add(M_a);
            strip_sec.Ext_Stability_Table.M.Add(M_b);
            strip_sec.Ext_Stability_Table.M.Add(M_c);
            strip_sec.Ext_Stability_Table.q_ref.Add(q_req);
            strip_sec.Ext_Stability_Table.q_ref.Add(q_req_b);
            strip_sec.Ext_Stability_Table.q_ref.Add(q_req_c);

            strip_sec.Ext_Stability_Table._2_x.Add(2 * x_a);
            strip_sec.Ext_Stability_Table._2_x.Add(2 * x_b);
            strip_sec.Ext_Stability_Table._2_x.Add(2 * x_c);




            list.Add(string.Format(""));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "Case", "Rv  ", "Rh  ", "M   ", "e  ", "q_req"));
            list.Add(string.Format(format, "", "(kN/m)", "(kN/m)", "(kN-m/m)", "(m) ", "(kpa)"));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(format, "A  ", Rv_a, Rh_a, M_a, e, q_req));
            list.Add(string.Format(format, "B  ", Rv_b, Rh_b, M_b, e_b, q_req_b));
            list.Add(string.Format(format, "C  ", Rv_c, Rh_c, M_c, e_c, q_req_c));
            list.Add(string.Format("".PadLeft(70, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CHECK FOR SLIDING"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));


            Fp = 1.2;
            list.Add(string.Format("Partial Factor of Safety (Soil to Soil) = 1.2 (minimum), (Ref: Table 16, BS : 8006 - 1995)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Retained Earth"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φ1)/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            double FoS_A = (Rv_a * Math.Tan(_phi1)) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_a, phi1, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_A, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double FoS_B = (Rv_b * Math.Tan(_phi1)) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_b, phi1, Rh_b));
            if (FoS_B > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_B, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS_C = (Rv_c * Math.Tan(_phi1)) / Rh_c;
            list.Add(string.Format("Case (B) : FoS = {0:f3} * tan ({1}) / {2:f3}", Rv_c, phi1, Rh_c));
            if (FoS_C > Fp)
                list.Add(string.Format("               = {0:f3} > {1},  OK ", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1},  NOT OK ", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Foundation"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / Rh_a;
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_a, phi_f, C, L, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / Rh_b;
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_b, phi_f, C, L, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / Rh_c;
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/ {4:f3}", Rv_c, phi_f, C, L, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Slip in Retained Earth for Over Design"));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available Factor of Safety (FoS) for Over Design = (Rv * tan φ1)/(1.2 * Rh)"));

            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi1)) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_a, phi1, Fp, Rh_a));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_A, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_A, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi1)) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_b, phi1, Fp, Rh_b));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_B, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_B, Fp));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi1)) / (Fp * Rh_c);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}))/({2} * {3:f3})", Rv_c, phi1, Fp, Rh_c));
            if (FoS_A > Fp)
                list.Add(string.Format("               = {0:f3} > {1} ,   OK", FoS_C, Fp));
            else
                list.Add(string.Format("               = {0:f3} < {1} ,   NOT OK", FoS_C, Fp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Slip in Foundation for Over Design"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Available Factor of Safety (FoS) for sliding at base = (Rv * tan φf + C * L )/Rh"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            FoS_A = (Rv_a * Math.Tan(_phi_f) + C * L) / (Fp * Rh_a);
            list.Add(string.Format("Case (A) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_a, phi_f, C, L, Fp, Rh_a));
            list.Add(string.Format("               = {0:f3} ", FoS_A));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_B = (Rv_b * Math.Tan(_phi_f) + C * L) / (Fp * Rh_b);
            list.Add(string.Format("Case (B) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_b, phi_f, C, L, Fp, Rh_b));
            list.Add(string.Format("               = {0:f3} ", FoS_B));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            FoS_C = (Rv_c * Math.Tan(_phi_f) + C * L) / (Fp * Rh_c);
            list.Add(string.Format("Case (C) : FoS = ({0:f3} * tan ({1}) + {2} * {3})/({4} * {5:f3})", Rv_c, phi_f, C, L, Fp, Rh_c));
            list.Add(string.Format("               = {0:f3} ", FoS_C));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("CHECK FOR OVERTURING"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double FoS = 1.5;

            list.Add(string.Format("Factor of Safety (FoS) for Overturnig = {0} (minimum)", FoS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("FoS for Overturnig at Toe = Σ[Resisting Moment (+ve)]/Σ[Disturbing Moment (-ve)]", FoS));
            list.Add(string.Format(""));

            FoS_A = Rv_a / Rh_a;
            if (FoS_A > FoS)
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_a, Rh_a, FoS_A, FoS));
            else
                list.Add(string.Format("Case (A) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_a, Rh_a, FoS_A, FoS));

            FoS_B = Rv_b / Rh_b;
            if (FoS_B > FoS)
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_b, Rh_b, FoS_B, FoS));
            else
                list.Add(string.Format("Case (B) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_b, Rh_b, FoS_B, FoS));

            FoS_C = Rv_c / Rh_c;
            if (FoS_C > FoS)
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},   OK", Rv_c, Rh_c, FoS_C, FoS));
            else
                list.Add(string.Format("Case (C) : FoS = {0:f3} / {1:f3} = {2:f3} > {3},  NOT OK", Rv_c, Rh_c, FoS_C, FoS));


            strip_sec.Case.Add("1");

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format("INTERNAL STABILITY"));
            list.Add(string.Format("---------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------------"));
            list.Add(string.Format("CASE 1 : CALCULATION FOR TENSION IN STRIPS FOR INTERNAL STABILITY"));
            list.Add(string.Format("-----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));

            Fp = 1.5;
            list.Add(string.Format("Fp = Partial Safety Factor = {0}", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("(a)  Vertical Forces"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wr = Weight of Road Crust above Retained Earth"));

            double Wra = Dp * gama1_max * L * Fp;

            list.Add(string.Format("   = Dp * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Dp, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wra));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Wm = Weight of Retained Earth mass"));


            Wm_a = Zi * gama1_max * L * Fp;
            list.Add(string.Format("   = Zi * γ1_max * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Zi, gama1_max, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wm_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic Load above Retained Earth mass"));
            Wq_a = L * q * Fp;
            list.Add(string.Format("   = L * q * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv = Vertical Component of Pressure by Surcharge behind Retained Earth mass"));
            list.Add(string.Format("    = Pq * Sin δ * Fp"));

            Pqv_a = (K2 * q * (Zi + Dp)) * Math.Sin(_delta) * Fp;
            list.Add(string.Format("    = (K2 * q * (Zi + Dp)) * Sin δ * Fp"));
            list.Add(string.Format("    = ({0:f3} * {1:f3} * ({2} + {3})) * Sin ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pqv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv = Vertical Component of Earth Pressure by Retained Earth mass"));

            Pv_a = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Sin(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Sin δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Sin ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Pv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b) Horizontal Forces :"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh = Horizontal Component of Pressure by Surcharge on Retained Earth mass"));

            Pq_h = K2 * q * (Zi + Dp) * Math.Cos(_delta) * Fp;
            list.Add(string.Format("    = K2 * q * (Zi+Dp) * Cos δ * Fp"));
            list.Add(string.Format("    = {0:f3} * {1} * ({2} + {3}) * Cos ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by Retained Earth mass"));

            Ph = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Cos(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Cos ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format("CALCULAION FOR DRIVING AND RESISTING MOMENT"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            format = "{0,-7} {1,-10} {2,18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr = " + Wra.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wra * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh = " + Pq_h.ToString("f3"),
                "(Zi+Dp)/2 = " + ((Zi + Dp) / 2.0).ToString("f3"),
                -(Pq_h * ((Zi + Dp) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph = " + Ph.ToString("f3"),
                "(Zi+Dp)/3 = " + ((Zi + Dp) / 3.0).ToString("f3"),
                -(Ph * (Zi + Dp) / 3.0)));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            Rv_a = Wra + Wm_a + Wq_a + Pqv_a + Pv_a;
            Rh_a = Pq_h + Ph;
            M_a = (Wra * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((Zi + Dp) / 2.0)) - (Ph * ((Zi + Dp) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv = " + Rv_a.ToString("f3"),
                "ΣRh = " + Rh_a.ToString("f3"),
                "",
                "ΣM = " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM / ΣRv "));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_v = Rv_a / (2 * x_a);
            list.Add(string.Format("Vertical Pressure = σ_v = ΣRv / (2*x)"));
            list.Add(string.Format("                        = {0:f3} / (2 * {1:f3})", Rv_a, x_a));
            list.Add(string.Format("                        = {0:f3} kPa", sigma_v));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Hense Horizontal Pressure = σ_h = σ_v * Ki"));
            list.Add(string.Format(""));

            double Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1;

            strip_sec.k.Add(Ki);

            double sigma_h = sigma_v * Ki;

            strip_sec.sigma_h.Add(sigma_h);

            list.Add(string.Format(""));
            list.Add(string.Format("By interpolating from Figure 2, with values of ka1, ka2, Zi and H1, we get the value of Ki"));
            list.Add(string.Format(""));

            list.Add(string.Format("Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1"));
            list.Add(string.Format("   = (({0:f3} - {1:f3}) * ({2:f3} - ({3:f3} + {4:f3})) / {5:f3}) + {1:f3}", ka2, ka1, H1, Zi, Dp, H1));
            list.Add(string.Format("   = {0:f3} ", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_h = σ_v * Ki"));
            list.Add(string.Format("    = {0:f3} * {1:f3}", sigma_v, Ki));
            list.Add(string.Format("    = {0:f3} kPa", sigma_h));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical spacing of strip layers = ∆i = {0} m", del_i));
            list.Add(string.Format("Calculation side for Strips = wi = {0} m", wi));
            list.Add(string.Format(""));

            double Total_Tmax = sigma_h * wi * del_i;

            list.Add(string.Format("Total Tension in Strips = Total_Tmax = σ_h * wi * ∆i"));
            list.Add(string.Format("                                     = {0:f3} * {1} * {2}", sigma_h, wi, del_i));
            list.Add(string.Format("                                     = {0:f3} kN", sigma_h, wi, del_i));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Number of Strips in the Layers = Tot_layers = {0} nos", tot_layers));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tmax = (Total_Tmax / tot_layers);

            strip_sec.Tmax.Add(Tmax);

            list.Add(string.Format("Tension per Strip = Tmax = Total_Tmax / Tot_layers"));
            list.Add(string.Format("                         = {0:f3} / {1:f0}", Total_Tmax, tot_layers));
            list.Add(string.Format("                         = {0:f3} kN", Tmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("Height from Top = Zi = {0} m", Zi));
            list.Add(string.Format(""));
            list.Add(string.Format("Ki = {0:f4}", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format("Delta ∆i = {0:f3} m", del_i));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Strips (for {0} m width) = {1} ", wi, tot_layers));
            list.Add(string.Format(""));


            list.Add(string.Format("Force At Connection = {0}%", cf));
            list.Add(string.Format(""));



            list.Add(string.Format("Coefficient of Friction = fo = {0:f3}", fo));
            list.Add(string.Format(""));



            double La = 0.0;
            if (Zi >= 0.0 && Zi < (0.6 * Hm))
            {
                La = L - (0.3 * Hm - (Zi / 6.0));
                list.Add(string.Format("Adherence Length = La = L - (0.3 * Hm - (Zi / 6.0))"));
                list.Add(string.Format("                      = {0:f3} - (0.3 * {1:f3} - ({2:f3} / 6.0))", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            else
            {
                La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0);
                list.Add(string.Format("Adherence Length = La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0)"));
                list.Add(string.Format("                      = {0:f3}  - ((0.2 * {1:f3} ) - ({2:f3}  - 0.6 * {1:f3} ) / 2.0)", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            strip_sec.La = La;

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _strip_strength = strip_sec.Strip_Strength;

            list.Add(string.Format("Ultimate Tensile Strength of Polymetric strip = {0:f3} kN", _strip_strength));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for Connection strength"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _To = Tmax * (cf / 100.0);

            strip_sec.To.Add(_To);


            list.Add(string.Format("Tension in strip at connection = To = % Force x Tmax"));
            list.Add(string.Format("                                    = {0:f3} x {1}%", Tmax, cf));
            list.Add(string.Format("                                    = {0:f3} kN", _To));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tr = 18.88;

            double des_width = 49.0;


            
            Polymetric_Strip pms = Strip_Properties[Get_Strip_Type(_strip_strength)] as Polymetric_Strip;

                if (pms != null)
                {
                    Tr = pms.Long_Term_Design_Strength_Consider;
                    des_width = pms.Width;
                }


            double Tro = Tr;



            list.Add(string.Format("Residual strength of strip (after connection) = Tr = {0:f3} kN", Tr));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Residual strength of strip at connection  = Tro = {0:f3} kN", Tro));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for F.O.S"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));

            double Tr_by_Tmax = (Tr / Tmax);

            strip_sec.Tr_by_Tm.Add(Tr_by_Tmax);
            strip_sec.Tro_by_To.Add((Tro / _To));



            list.Add(string.Format("Over design factor (strip length)  = Tr / Tmax = {0:f3} / {1:f3} = {2:f3}", Tr, Tmax, Tr_by_Tmax));
            list.Add(string.Format(""));
            double Tro_by_To = (Tr / _To);
            list.Add(string.Format(""));
            list.Add(string.Format("Over design factor (connection)  = Tro / To = {0:f3} / {1:f3} = {2:f3}", Tr, _To, Tro_by_To));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficient of friction : f* "));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("f* (µ in BS 8006) is the friction factor for soil/strip interaction."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("           f*o   =  1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double Za = Zi + Dp;
            strip_sec.Za = Za;
            list.Add(string.Format(""));
            list.Add(string.Format("Za = Height of fill above strip on La "));
            //list.Add(string.Format("   = (Zi+Dp) + ai * tan Bs"));
            list.Add(string.Format("   = (Zi+Dp) "));
            list.Add(string.Format("   = ({0:f3}+{1:f3})", Zi, Dp));
            list.Add(string.Format("   =  {0:f3} m", Za));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Fp = 1.5;

            double wst = (Zi + Dp) * La * gama1_max * 1.5;


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Wst = Weight of fill on 'Tail' of Strip (La) "));
            list.Add(string.Format("    = (Zi + Dp) * La * γ1_max * 1.5"));
            list.Add(string.Format("    = ({0:f3} + {1:f3}) * {2:f3} * {3:f3} * 1.5", Zi, Dp, La, gama1_max));
            list.Add(string.Format("    = {0:f3} kN/m", wst));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic load above Strip "));

            double Wq = La * q * Fp;
            list.Add(string.Format("   = La * q * Fp "));
            list.Add(string.Format("   = {0:f3} * {1:f3} * {2} ", La, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Hence, Tf per strip for polymetric strip ({0}mm minimum design width)", des_width));
            list.Add(string.Format(""));

            double Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1;
            strip_sec.Tf.Add(Tf);

            list.Add(string.Format(" Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1"));
            list.Add(string.Format("    = (({0:f3} + {1:f3}) * {2:f3} * 2 * ({3:f3} / 1000.0)) / 1.3 * 1.1", wst, Wq, fo, des_width));
            list.Add(string.Format("    = {0:f3} kN", Tf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Tf_by_Tmax = (Tf / Tmax);
            list.Add(string.Format("Tf / Tmax = ({0:f3} / {1:f3}) = {2:f3}", Tf, Tmax, Tf_by_Tmax));
            strip_sec.Tf_by_Tm.Add(Tf_by_Tmax);




            list.Add(string.Format(""));
            strip_sec.Case.Add("2");

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format("CASE 2  : CALCULATION FOR TENSION IN STRIPS FOR INTERNAL STABILITY"));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));

            Fp = 1.5;
            list.Add(string.Format("Fp = Partial Safety Factor = {0}", Fp));
            list.Add(string.Format(""));
            list.Add(string.Format("(a)  Vertical Forces"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Wr = Weight of Road Crust above Retained Earth"));

            Wra = Dp * gama1_min * L * Fp;

            list.Add(string.Format("   = Dp * γ1_min * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Dp, gama1_min, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wra));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Wm = Weight of Retained Earth mass"));


            Wm_a = Zi * gama1_min * L * Fp;
            list.Add(string.Format("   = Zi * γ1_min * L * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2} * {3}", Zi, gama1_min, L, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wm_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic Load above Retained Earth mass"));
            Wq_a = L * q * Fp;
            list.Add(string.Format("   = L * q * Fp"));
            list.Add(string.Format("   = {0} * {1} * {2}", L, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqv = Vertical Component of Pressure by Surcharge behind Retained Earth mass"));
            list.Add(string.Format("    = Pq * Sin δ * Fp"));

            Pqv_a = (K2 * q * (Zi + Dp)) * Math.Sin(_delta) * Fp;
            list.Add(string.Format("    = (K2 * q * (Zi + Dp)) * Sin δ * Fp"));
            list.Add(string.Format("    = ({0:f3} * {1:f3} * ({2} + {3})) * Sin ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pqv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pv = Vertical Component of Earth Pressure by Retained Earth mass"));

            Pv_a = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Sin(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Sin δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Sin ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Pv_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b) Horizontal Forces :"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Pqh = Horizontal Component of Pressure by Surcharge on Retained Earth mass"));

            Pq_h = K2 * q * (Zi + Dp) * Math.Cos(_delta) * Fp;
            list.Add(string.Format("    = K2 * q * (Zi+Dp) * Cos δ * Fp"));
            list.Add(string.Format("    = {0:f3} * {1} * ({2} + {3}) * Cos ({4:f3}) * {5}", K2, q, Zi, Dp, delta, Fp));
            list.Add(string.Format("    = {0:f3} kN/m", Pq_h));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Ph = Horizontal Component of Earth Pressure by Retained Earth mass"));

            Ph = K2 * 0.5 * gama2 * (Zi + Dp) * (Zi + Dp) * Math.Cos(_delta) * Fp;

            list.Add(string.Format("   = K2 * (1/2) * γ2 * (Zi + Dp)^2 * Cos δ * Fp"));
            list.Add(string.Format("   = {0:f3} * 0.5 * {1} * ({2} + {3})^2 * Cos ({4:f3}) * {5}", K2, gama2, Zi, Dp, delta, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Ph));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format("CALCULAION FOR DRIVING AND RESISTING MOMENT"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            format = "{0,-7} {1,-10} {2,18:f3} {3,18:f3} {4,22:f3} {5,20:f3}";


            list.Add(string.Format(format, "S.No:", "Fp (FoS)", "Vertical Forces", "Horizontal Forces", "Lever Arm", "Moment"));
            list.Add(string.Format(format, "", "", "   (kN/m)   ", "   (kN/m)   ", "     (m)   ", " (kN-m)"));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));
            list.Add(string.Format(format, "1.", "1.5",
                "Wr = " + Wra.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wra * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "2.", "1.5",
                "Wm = " + Wm_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wm_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "3.", "1.5",
                "Wq = " + Wq_a.ToString("f3"),
                "_____      ",
                "L/2 = " + (L / 2).ToString("f3"),
                (Wq_a * L / 2.0)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "4.", "1.5",
                "Pqv = " + Pqv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pqv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "5.", "1.5",
                "Pv = " + Pv_a.ToString("f3"),
                "_____      ",
                "L = " + (L).ToString("f3"),
                (Pv_a * L)));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "6.", "1.5",
                "_____      ",
                "Pqh = " + Pq_h.ToString("f3"),
                "(Zi+Dp)/2 = " + ((Zi + Dp) / 2.0).ToString("f3"),
                -(Pq_h * ((Zi + Dp) / 2.0))));
            list.Add(string.Format(""));

            list.Add(string.Format(format, "7.", "1.5",
                "_____      ",
                "Ph = " + Ph.ToString("f3"),
                "(Zi+Dp)/3 = " + ((Zi + Dp) / 3.0).ToString("f3"),
                -(Ph * (Zi + Dp) / 3.0)));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));



            Rv_a = Wra + Wm_a + Wq_a + Pqv_a + Pv_a;
            Rh_a = Pq_h + Ph;
            M_a = (Wra * L / 2.0) + (Wm_a * L / 2.0) + (Wq_a * L / 2.0) + (Pqv_a * L) + (Pv_a * L) - (Pq_h * ((Zi + Dp) / 2.0)) - (Ph * ((Zi + Dp) / 3.0));

            list.Add(string.Format(format, "", "",
                "ΣRv = " + Rv_a.ToString("f3"),
                "ΣRh = " + Rh_a.ToString("f3"),
                "",
                "ΣM = " + M_a.ToString("f3")));
            list.Add(string.Format("".PadLeft(100, '-')));
            list.Add(string.Format(""));

            list.Add(string.Format("Resulting Force acts at distance 'x' from the toe of the wall"));
            list.Add(string.Format(""));
            x_a = M_a / Rv_a;


            list.Add(string.Format("x = ΣM / ΣRv "));
            list.Add(string.Format("  = {0:f3} / {1:f3}", M_a, Rv_a));
            list.Add(string.Format("  = {0:f3} m", x_a));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            sigma_v = Rv_a / (2 * x_a);
            list.Add(string.Format("Vertical Pressure = σ_v = ΣRv / (2*x)"));
            list.Add(string.Format("                        = {0:f3} / (2 * {1:f3})", Rv_a, x_a));
            list.Add(string.Format("                        = {0:f3} kPa", sigma_v));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Hense Horizontal Pressure = σ_h = σ_v * Ki"));
            list.Add(string.Format(""));

            Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1;

            strip_sec.k.Add(Ki);

            sigma_h = sigma_v * Ki;

            strip_sec.sigma_h.Add(sigma_h);

            list.Add(string.Format(""));
            list.Add(string.Format("By interpolating from Figure 2, with values of ka1, ka2, Zi and H1, we get the value of Ki"));
            list.Add(string.Format(""));

            list.Add(string.Format("Ki = (ka2 - ka1) * (H1 - (Zi + Dp)) / H1 + ka1"));
            list.Add(string.Format("   = (({0:f3} - {1:f3}) * ({2:f3} - ({3:f3} + {4:f3})) / {5:f3}) + {1:f3}", ka2, ka1, H1, Zi, Dp, H1));
            list.Add(string.Format("   = {0:f3} ", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_h = σ_v * Ki"));
            list.Add(string.Format("    = {0:f3} * {1:f3}", sigma_v, Ki));
            list.Add(string.Format("    = {0:f3} kPa", sigma_h));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Vertical spacing of strip layers = ∆i = {0} m", del_i));
            list.Add(string.Format("Calculation side for Strips = wi = {0} m", wi));
            list.Add(string.Format(""));

            Total_Tmax = sigma_h * wi * del_i;

            list.Add(string.Format("Total Tension in Strips = Total_Tmax = σ_h * wi * ∆i"));
            list.Add(string.Format("                                     = {0:f3} * {1} * {2}", sigma_h, wi, del_i));
            list.Add(string.Format("                                     = {0:f3} kN", sigma_h, wi, del_i));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total Number of Strips in the Layers = Tot_layers = {0} nos", tot_layers));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Tmax = (Total_Tmax / tot_layers);

            strip_sec.Tmax.Add(Tmax);

            list.Add(string.Format("Tension per Strip = Tmax = Total_Tmax / Tot_layers"));
            list.Add(string.Format("                         = {0:f3} / {1:f0}", Total_Tmax, tot_layers));
            list.Add(string.Format("                         = {0:f3} kN", Tmax));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("Height from Top = Zi = {0} m", Zi));
            list.Add(string.Format(""));
            list.Add(string.Format("Ki = {0:f4}", Ki));
            list.Add(string.Format(""));
            list.Add(string.Format("Delta ∆i = {0:f3} m", del_i));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Strips (for {0} m width) = {1} ", wi, tot_layers));
            list.Add(string.Format(""));


            list.Add(string.Format("Force At Connection = {0}%", cf));
            list.Add(string.Format(""));



            list.Add(string.Format("Coefficient of Friction = fo = {0:f3}", fo));
            list.Add(string.Format(""));



            La = 0.0;
            if (Zi >= 0.0 && Zi < (0.6 * Hm))
            {
                La = L - (0.3 * Hm - (Zi / 6.0));
                list.Add(string.Format("Adherence Length = La = L - (0.3 * Hm - (Zi / 6.0))"));
                list.Add(string.Format("                      = {0:f3} - (0.3 * {1:f3} - ({2:f3} / 6.0))", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            else
            {
                La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0);
                list.Add(string.Format("Adherence Length = La = L - ((0.2 * Hm) - (Zi - 0.6 * Hm) / 2.0)"));
                list.Add(string.Format("                      = {0:f3}  - ((0.2 * {1:f3} ) - ({2:f3}  - 0.6 * {1:f3} ) / 2.0)", L, Hm, Zi));
                list.Add(string.Format("                      = {0:f3} ", La));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            _strip_strength = strip_sec.Strip_Strength;

            list.Add(string.Format("Ultimate Tensile Strength of Polymetric strip = {0:f3} kN", _strip_strength));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for Connection strength"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            _To = Tmax * (cf / 100.0);

            strip_sec.To.Add(_To);


            list.Add(string.Format("Tension in strip at connection = To = % Force x Tmax"));
            list.Add(string.Format("                                    = {0:f3} x {1}%", Tmax, cf));
            list.Add(string.Format("                                    = {0:f3} kN", _To));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //foreach (var item in Strip_Properties)
            //{
            //    if (strip_sec.Strip_Type.ToString("f3") == item.Ultimate_Tensile_Strength.ToString("f3"))
            //    {
            //        Tr = item.Long_Term_Design_Strength_Consider;
            //        des_width =item.Width;
            //    }
            //}

            Tro = Tr;



            list.Add(string.Format("Residual strength of strip (after connection) = Tr = {0:f3} kN", Tr));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Residual strength of strip at connection  = Tro = {0:f3} kN", Tro));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation for F.O.S"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));

            Tr_by_Tmax = (Tr / Tmax);

            strip_sec.Tr_by_Tm.Add(Tr_by_Tmax);
            strip_sec.Tro_by_To.Add((Tro / _To));



            list.Add(string.Format("Over design factor (strip length)  = Tr / Tmax = {0:f3} / {1:f3} = {2:f3}", Tr, Tmax, Tr_by_Tmax));
            list.Add(string.Format(""));
            Tro_by_To = (Tr / _To);
            list.Add(string.Format(""));
            list.Add(string.Format("Over design factor (connection)  = Tro / To = {0:f3} / {1:f3} = {2:f3}", Tr, _To, Tro_by_To));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficient of friction : f* "));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("f* (µ in BS 8006) is the friction factor for soil/strip interaction."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("           f*o   =  1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            Za = Zi + Dp;

            list.Add(string.Format(""));
            list.Add(string.Format("Za = Height of fill above strip on La "));
            //list.Add(string.Format("   = (Zi+Dp) + ai * tan Bs"));
            list.Add(string.Format("   = (Zi+Dp) "));
            list.Add(string.Format("   = ({0:f3}+{1:f3})", Zi, Dp));
            list.Add(string.Format("   =  {0:f3} m", Za));


            Fp = 1.5;

            wst = (Zi + Dp) * La * gama1_min * 1.5;


            list.Add(string.Format("Wst = Weight of fill on 'Tail' of Strip (La) "));
            list.Add(string.Format("    = (Zi + Dp) * La * γ1_min * 1.5"));
            list.Add(string.Format("    = ({0:f3} + {1:f3}) * {2:f3} * {3:f3} * 1.5", Zi, Dp, La, gama1_min));
            list.Add(string.Format("    = {0:f3} kN/m", wst));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Wq = Traffic load above Strip "));

            Wq = La * q * Fp;
            list.Add(string.Format("   = La * q * Fp "));
            list.Add(string.Format("   = {0:f3} * {1:f3} * {2} ", La, q, Fp));
            list.Add(string.Format("   = {0:f3} kN/m", Wq));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Hence, Tf per strip for polymetric strip ({0}mm minimum design width)", des_width));
            list.Add(string.Format(""));

            Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1;
            strip_sec.Tf.Add(Tf);

            list.Add(string.Format(" Tf = ((wst + Wq) * fo * 2 * (des_width / 1000.0)) / 1.3 * 1.1"));
            list.Add(string.Format("    = (({0:f3} + {1:f3}) * {2:f3} * 2 * ({3:f3} / 1000.0)) / 1.3 * 1.1", wst, Wq, fo, des_width));
            list.Add(string.Format("    = {0:f3} kN", Tf));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Tf_by_Tmax = (Tf / Tmax);
            list.Add(string.Format("Tf / Tmax = ({0:f3} / {1:f3}) = {2:f3}", Tf, Tmax, Tf_by_Tmax));
            strip_sec.Tf_by_Tm.Add(Tf_by_Tmax);




            //File.WriteAllLines(Report_File, list.ToArray());
            //iapp.View_Result(Report_File);
        }
        public List<string> Get_User_Input_Data()
        {
            List<string> list = new List<string>();
            #region User Input Data
            list.Add("");
            //list.Add("");
            //list.Add("------------------");
            //list.Add("USER'S INPUT DATA");
            //list.Add("------------------");
            list.Add("");
            list.Add("WALL GEOMETRY");
            list.Add("-------------");
            list.Add("");
            list.Add("");
            list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            list.Add("");
            //list.Add(string.Format("Zi = Depth of Strips Layer from top = {0} m", Zi));
            //list.Add(string.Format("RE Backfill Height = H1 = {0} m", H1));
            //list.Add("");
            list.Add("");
            Hf = H1;
            list.Add(string.Format("Facing Height = Hf = {0} m", Hf));
            list.Add(string.Format("Coping Height = Hc = {0} m", Hc));
            list.Add(string.Format("Free Board = Ht = {0} m", Ht));
            list.Add(string.Format("Facing Thickness = E = {0} m", E));
            list.Add(string.Format("Slope Height = H2 = {0} m", H2));
            list.Add(string.Format("Set Back = F = {0} m", F));
            list.Add(string.Format("Slope Angle = B = {0} °(deg)", B));
            list.Add(string.Format("Terrance Angle = w = {0} °(deg)", w));
            list.Add(string.Format("Angle at Toe = Bs = {0} °(deg)", Bs));
            list.Add(string.Format("Mechanical Height = Hm = {0} m", Hm));
            list.Add(string.Format("Length of Geogrid Strips = L = {0} m", L));
            list.Add(string.Format("Economic Ramification = fn = {0}", fn));
            list.Add("");
            list.Add("");
            list.Add("SURCHARGE");
            list.Add("---------");
            list.Add("");
            list.Add(string.Format("Live Load Surcharge = q = {0} kpa (Road Traffic)", q));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF RE BACKFILL");
            list.Add("------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γ1_max = {0} kN/Cu.m.", gama1_max));
            list.Add(string.Format("Minimum Dry Density = γ1_min = {0} kN/Cu.m.", gama1_min));
            list.Add(string.Format("Angle of Internal Friction = φ1 = {0} °(deg).", phi1));
            list.Add(string.Format("Coefficient of Uniformity = Cu = {0} ", Cu));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF GENERAL CENTRAL BACKFILL");
            list.Add("-------------------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γ2 = {0} kN/Cu.m.", gama2));
            list.Add(string.Format("Angle of Internal Friction = φ2 = {0} °(deg).", phi2));
            list.Add("");
            list.Add("");
            list.Add("SOIL PROPERTIES OF FOUNDATION SOIL");
            list.Add("----------------------------------");
            list.Add("");
            list.Add(string.Format("Maximum Dry Density = γf = {0} kN/Cu.m.", gama2));
            list.Add(string.Format("Angle of Internal Friction = φf = {0} °(deg).", phi2));
            list.Add(string.Format("Cohesion = C = {0} kN/Sq.m", C));
            list.Add(string.Format("Thickness of Pavement = Dp = {0} m", Dp));
            list.Add(string.Format("Thickness = Df = {0} m", Df));
            list.Add("");
            list.Add("");
            //list.Add("ASSUMED DESIGN LIFE");
            //list.Add("-------------------");
            //list.Add("");
            //list.Add(string.Format("Life of Structure = Sl = {0} year(s)", Sl));
            //list.Add(string.Format("Site Condition = Sc = {0} ", Sc));
            //list.Add(string.Format("Strip Protection = Sp = {0} ", Sp));
            list.Add("");
            list.Add("");
            #endregion User Input Data
            return list;
        }



        public void Set_Strip_Strength()
        {
            Polymetric_Strip pms = null;
            for (int i = 0; i < Layout_Sections.Count; i++)
            {
                for (int j = 0; j < Layout_Sections[i].Layers.Count; j++)
                {
                    pms = Strip_Properties[Layout_Sections[i].Layers[j].Strip_Type] as Polymetric_Strip;
                    if (pms != null)
                        Layout_Sections[i].Layers[j].Strip_Strength = pms.Ultimate_Tensile_Strength;
                }
            }
        }


        public void Set_Total_Sections()
        {
            _h1_incr = del_i / 2.0;
            Total_Sections = new List<double>();
            double val = _start_h1;

            int count = (int)((_end_h1 - _start_h1) / _h1_incr);

            for (int i = 0; i <= count; i++)
            {
                val = MyList.StringToDouble(val.ToString("f3"), 0.0);
                Total_Sections.Add(val);
                val += _h1_incr;
            }
        }


        public void Set_Default_Input_Data()
        {

            Reinforcement_Layout_Section sec_1 = null;
            Reinforcement_Layout_Section sec_2 = null;
            double val = 0.0;

            Polymetric_Strip pms = null;
            for (int i = 0; i < Total_Sections.Count; i++)
            {
                val = Total_Sections[i];
                sec_1 = Reinforcement_Layout_Section.Get_Default_Layout_Data(val);

                sec_2 = Layout_Sections[i];

                sec_2.Embedment_Depth = sec_1.Embedment_Depth;
                for (int j = 0; j < sec_1.Layers.Count; j++)
                {
                    try
                    {
                        sec_2.Layers[j].LayerNo = sec_1.Layers[j].LayerNo;
                        sec_2.Layers[j].Number_Strips = sec_1.Layers[j].Number_Strips;
                        sec_2.Layers[j].Strip_Type = sec_1.Layers[j].Strip_Type;
                    

                        pms = Strip_Properties[sec_2.Layers[j].Strip_Type] as Polymetric_Strip;
                        if (pms != null)
                            sec_2.Layers[j].Strip_Strength = pms.Ultimate_Tensile_Strength;

                        sec_2.Layers[j].Strip_Length = sec_1.Layers[j].Strip_Length;
                    }
                    catch (Exception ex) { }
                }

            }
        }


        public void Set_Design_Input_Data(bool is_total_section_change)
        {
            //_start_h1 = 2.01;
            //_end_h1 = _h1;
            _h1_incr = del_i / 2.0;

            //if (Total_Sections == null)
            if (!is_total_section_change || (Total_Sections == null))
                Set_Total_Sections();
            //Total_Sections = new List<double>();

            double val = _start_h1;

            int count = (int)((_end_h1 - _start_h1) / _h1_incr);


            double start_zi = 0.0;

            Reinforcement_Layout_Section layout_sec = new Reinforcement_Layout_Section();

            Reinforcement_Layout layout = null;

            Layout_Sections = new List<Reinforcement_Layout_Section>();

            int layers = 0;

            double _fo = fo;

            double _len = L;
            int _no = 8;


            for (int i = 0; i < Total_Sections.Count; i++)
            {
                val = Total_Sections[i];
                layout_sec = new Reinforcement_Layout_Section();
                layers = 0;
                //_len = 3.1;
                _no = 8;
                start_zi = 0.385;

                if (i % 2 == 0)
                {
                    while (start_zi < val)
                    {
                        layers++;
                        layout = new Reinforcement_Layout(layers);
                        if (layers == 1)
                        {
                            start_zi = 0.385;
                            layout.delta_h = 0.585;
                            layout.Strip_Type = 1;
                            //layout.Strip_Strength = 20.0;
                            _fo = fo;
                        }
                        else if (layers == 2)
                        {
                            start_zi += _h1_incr;
                            layout.delta_h = 0.600;
                            layout.Strip_Type = 2;
                            //layout.Strip_Strength = 37.5;
                            _fo -= 0.04;
                        }
                        else if ((start_zi + 4.0 * _h1_incr) > val)
                        {
                            start_zi += 2.0 * _h1_incr;
                            layout.delta_h = 0.825;
                            layout.Strip_Type = 2;
                            //layout.Strip_Strength = 37.5;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;

                        }
                        else
                        {
                            start_zi += 2.0 * _h1_incr;
                            //if (start_zi > val)
                            layout.delta_h = 0.80;
                            layout.Strip_Type = 2;
                            //layout.Strip_Strength = 37.5;
                            //layout.Strip_Length = 3.10;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;
                        }



                        if (layers <= 6)
                        {
                            _no = 8;
                        }
                        else if (layers > 6 && layers <= 8)
                        {
                            _no = 10;
                        }
                        else if (layers > 8 && layers <= 10)
                        {
                            _no = 12;
                        }
                        else if (layers > 10 && layers <= 12)
                        {
                            _no = 14;
                        }
                        else if (layers > 12 && layers <= 15)
                        {
                            layout.Strip_Strength = 50;
                            _no = 16;
                        }
                        else if (layers > 15 && layers <= 16)
                        {
                            layout.Strip_Strength = 65;
                            _no = 18;
                        }
                        else if (layers >= 17)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.3;
                            _no = 20;
                        }

                        if (start_zi > val) break;

                        layout.Number_Strips = _no;
                        layout.Strip_Length = _len;
                        layout.z = start_zi;
                        layout.fn = _fo;
                        layout_sec.Layers.Add(layout);
                    }

                }
                else
                {

                    while (start_zi < val)
                    {
                        layers++;
                        layout = new Reinforcement_Layout(layers);
                        if (layers == 1)
                        {
                            start_zi = 0.585;
                            layout.delta_h = 0.885;
                            layout.Strip_Type = 1;
                            //layout.Strip_Strength = 20.0;
                            _fo = fo;
                        }
                        else if (layers == 2)
                        {
                            start_zi += 0.6;
                            layout.delta_h = 0.700;
                            layout.Strip_Type = 2;
                            //layout.Strip_Strength = 37.5;
                            _fo -= 0.04;
                        }
                        else if ((start_zi + 4.0 * _h1_incr) > val)
                        {
                            start_zi += 2.0 * _h1_incr;
                            layout.delta_h = 0.825;
                            layout.Strip_Type = 2;
                            //layout.Strip_Strength = 37.5;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;

                        }
                        else
                        {
                            start_zi += 2.0 * _h1_incr;
                            //if (start_zi > val) break;
                            layout.delta_h = 0.80;
                            layout.Strip_Type = 2;
                            //layout.Strip_Strength = 37.5;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;

                        }

                        if (layers <= 6)
                        {
                            _no = 8;
                            //_len += 0.2;
                        }
                        else if (layers > 6 && layers <= 8)
                        {
                            //_len += 0.2;
                            _no = 10;
                        }
                        else if (layers > 8 && layers <= 10)
                        {
                            //_len += 0.3;
                            _no = 12;
                        }
                        else if (layers > 10 && layers <= 12)
                        {
                            //_len += 0.2;
                            _no = 14;
                        }
                        else if (layers > 12 && layers <= 15)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.4;
                            _no = 16;
                        }
                        else if (layers > 15 && layers <= 16)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.4;
                            _no = 18;
                        }
                        else if (layers >= 17)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.4;
                            _no = 20;
                        }

                        if (start_zi > val) break;

                        layout.Number_Strips = _no;
                        layout.Strip_Length = _len;
                        layout.z = start_zi;
                        layout.fn = _fo;
                        layout_sec.Layers.Add(layout);
                    }
                }


                if (layers <= 6)
                {
                    _len += 0.2;
                }
                else if (layers > 6 && layers <= 8)
                {
                    _len += 0.2;
                }
                else if (layers > 8 && layers <= 10)
                {
                    _len += 0.3;
                }
                else if (layers > 10 && layers <= 12)
                {
                    _len += 0.2;
                }
                else if (layers > 12 && layers <= 15)
                {
                    _len += 0.2;
                }
                else if (layers > 15 && layers <= 16)
                {
                    _len += 0.3;
                }
                else if (layers >= 17)
                {
                    _len += 0.3;
                }
                Layout_Sections.Add(layout_sec);
                //Total_Sections.Add(val);
                //val = val + _h1_incr;

                //val = MyList.StringToDouble(val.ToString("f3"), 1.0);
            }
            Set_Strip_Strength();

            Set_Default_Input_Data();
        }


        //Chiranjit [2013 04 21]
        public void Set_Design_Input_Data1()
        {
            //_start_h1 = 2.01;
            //_end_h1 = _h1;
            _h1_incr = del_i / 2.0;

            Total_Sections = new List<double>();

            double val = _start_h1;

            int count = (int)((_end_h1 - _start_h1) / _h1_incr);

            double start_zi = 0.0;

            Reinforcement_Layout_Section layout_sec = new Reinforcement_Layout_Section();

            Reinforcement_Layout layout = null;

            Layout_Sections = new List<Reinforcement_Layout_Section>();

            int layers = 0;

            double _fo = fo;



            double _len = L;
            int _no = 8;


            for (int i = 0; i <= count; i++)
            {

                layout_sec = new Reinforcement_Layout_Section();
                layers = 0;
                //_len = 3.1;
                _no = 8;
                start_zi = 0.385;

                if (i % 2 == 0)
                {
                    while (start_zi < val)
                    {
                        layers++;
                        layout = new Reinforcement_Layout(layers);
                        if (layers == 1)
                        {
                            start_zi = 0.385;
                            layout.delta_h = 0.585;
                            layout.Strip_Strength = 20.0;
                            _fo = fo;
                        }
                        else if (layers == 2)
                        {
                            start_zi += _h1_incr;
                            layout.delta_h = 0.600;
                            layout.Strip_Strength = 37.5;
                            _fo -= 0.04;
                        }
                        else if ((start_zi + 4.0 * _h1_incr) > val)
                        {
                            start_zi += 2.0 * _h1_incr;
                            layout.delta_h = 0.825;
                            layout.Strip_Strength = 37.5;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;

                        }
                        else
                        {
                            start_zi += 2.0 * _h1_incr;
                            //if (start_zi > val)
                            layout.delta_h = 0.80;
                            layout.Strip_Strength = 37.5;
                            //layout.Strip_Length = 3.10;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;
                        }



                        if (layers <= 6)
                        {
                            _no = 8;
                        }
                        else if (layers > 6 && layers <= 8)
                        {
                            _no = 10;
                        }
                        else if (layers > 8 && layers <= 10)
                        {
                            _no = 12;
                        }
                        else if (layers > 10 && layers <= 12)
                        {
                            _no = 14;
                        }
                        else if (layers > 12 && layers <= 15)
                        {
                            layout.Strip_Strength = 50;
                            _no = 16;
                        }
                        else if (layers > 15 && layers <= 16)
                        {
                            layout.Strip_Strength = 65;
                            _no = 18;
                        }
                        else if (layers >= 17)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.3;
                            _no = 20;
                        }

                        if (start_zi > val) break;

                        layout.Number_Strips = _no;
                        layout.Strip_Length = _len;
                        layout.z = start_zi;
                        layout.fn = _fo;
                        layout_sec.Layers.Add(layout);
                    }

                }
                else
                {

                    while (start_zi < val)
                    {
                        layers++;
                        layout = new Reinforcement_Layout(layers);
                        if (layers == 1)
                        {
                            start_zi = 0.585;
                            layout.delta_h = 0.885;
                            layout.Strip_Strength = 20.0;
                            _fo = fo;
                        }
                        else if (layers == 2)
                        {
                            start_zi += 0.6;
                            layout.delta_h = 0.700;
                            layout.Strip_Strength = 37.5;
                            _fo -= 0.04;
                        }
                        else if ((start_zi + 4.0 * _h1_incr) > val)
                        {
                            start_zi += 2.0 * _h1_incr;
                            layout.delta_h = 0.825;
                            layout.Strip_Strength = 37.5;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;

                        }
                        else
                        {
                            start_zi += 2.0 * _h1_incr;
                            //if (start_zi > val) break;
                            layout.delta_h = 0.80;
                            layout.Strip_Strength = 37.5;
                            if ((_fo - 0.08) < 0.5)
                                _fo = 0.5;
                            else
                                _fo -= 0.08;

                        }

                        if (layers <= 6)
                        {
                            _no = 8;
                            //_len += 0.2;
                        }
                        else if (layers > 6 && layers <= 8)
                        {
                            //_len += 0.2;
                            _no = 10;
                        }
                        else if (layers > 8 && layers <= 10)
                        {
                            //_len += 0.3;
                            _no = 12;
                        }
                        else if (layers > 10 && layers <= 12)
                        {
                            //_len += 0.2;
                            _no = 14;
                        }
                        else if (layers > 12 && layers <= 15)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.4;
                            _no = 16;
                        }
                        else if (layers > 15 && layers <= 16)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.4;
                            _no = 18;
                        }
                        else if (layers >= 17)
                        {
                            layout.Strip_Strength = 50;
                            //_len += 0.4;
                            _no = 20;
                        }

                        if (start_zi > val) break;

                        layout.Number_Strips = _no;
                        layout.Strip_Length = _len;
                        layout.z = start_zi;
                        layout.fn = _fo;
                        layout_sec.Layers.Add(layout);
                    }
                }


                if (layers <= 6)
                {
                    _len += 0.2;
                }
                else if (layers > 6 && layers <= 8)
                {
                    _len += 0.2;
                }
                else if (layers > 8 && layers <= 10)
                {
                    _len += 0.3;
                }
                else if (layers > 10 && layers <= 12)
                {
                    _len += 0.2;
                }
                else if (layers > 12 && layers <= 15)
                {
                    _len += 0.2;
                }
                else if (layers > 15 && layers <= 16)
                {
                    _len += 0.3;
                }
                else if (layers >= 17)
                {
                    _len += 0.3;
                }



                Layout_Sections.Add(layout_sec);

                Total_Sections.Add(val);
                val = val + _h1_incr;

                val = MyList.StringToDouble(val.ToString("f3"), 1.0);
            }
        }


        public void Read_File(string file_name)
        {
            try
            {
                Working_Folder = Path.GetDirectoryName(file_name);
                //iapp.LastDesignWorkingFolder = Path.GetDirectoryName(Working_Folder);
                if (!File.Exists(Report_Table_File)) return;
                    

                List<string> list = new List<string>(File.ReadAllLines(Report_Table_File));

                string kStr = "";

                Reinforcement_Layout lay = null;
                Reinforcement_Layout_Section lay_sec = null;
                MyList mlist = null;

                bool find_step = false;
                bool find_rupture = false;
                bool find_adherence = false;


                int _layer_no = 0;


                Total_Sections.Clear();
                Layout_Sections.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i];

                    if (kStr.StartsWith("-----------------")) continue;
                    if (kStr == "") continue;

                    if (kStr.Contains("RE Backfill Height = H1 "))
                    {

                        if (lay_sec != null)
                            Layout_Sections.Add(lay_sec);

                        lay_sec = new Reinforcement_Layout_Section();
                        lay = new Reinforcement_Layout(1);

                        mlist = new MyList(kStr, ' ');
                        Total_Sections.Add(mlist.GetDouble(6));
                        find_rupture = false;
                        find_adherence = false;
                        
                    }
                    else if (kStr.Contains("STRIP RUPTURE - TENSILE LOAD AT FACING : Overdesign Factors"))
                    {
                        //lay_sec = new Reinforcement_Layout_Section();
                        //find_rupture = false;
                        find_adherence = false;
                        
                        find_rupture = true;
                        continue;
                    }
                    else if (kStr.Contains("Minimum embedment depth = "))
                    {

                        mlist = new MyList(kStr, ' ');
                        lay_sec.Embedment_Depth = mlist.GetDouble(4);
                       
                        continue;
                    }
                    else if (kStr.Contains("ADHERENCE : Overdesign Factors"))
                    {
                        //lay_sec = new Reinforcement_Layout_Section();
                        find_rupture = false;
                        //find_adherence = false;
                        
                        find_adherence = true;
                        continue;

                    }


                    if (find_rupture)
                    {
                        mlist = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                        if (mlist.Count == 13)
                        {
                            int col = 0;
                            try
                            {
                                lay = new Reinforcement_Layout(mlist.GetInt(col++));
                                //lay.LayerNo = mlist.GetInt(col++);
                                lay.z = mlist.GetDouble(col++);
                                lay.delta_h = mlist.GetDouble(col++);
                                lay.Strip_Strength = mlist.GetDouble(col++);


                                lay.Strip_Type = Get_Strip_Type(lay.Strip_Strength);


                                lay.Number_Strips = mlist.GetInt(col++);
                                lay_sec.Layers.Add(lay);
                                //Layer      z      delta_h  Strength  Num.  Num.    Case       k     sigma_h      Tmax     To        Tr/Tm    Tro/To

                                //1         0.385     0.585    20.000     8     4       1     0.390    26.146     7.648     6.501     2.469     2.904
                                //                                                      2     0.390    24.993     7.311     6.214     2.583     3.038

                                //2         0.785     0.600    37.500     8     4       1     0.358    28.879     8.664     7.364     2.179     2.564
                                //                                                      2     0.358    27.394     8.218     6.985     2.297     2.703

                                //3         1.585     0.825    37.500     8     4       1     0.293    32.539    13.422    11.409     1.407     1.655
                            }
                            catch (Exception ex) { }
                        }
                    }
                    else if (find_adherence)
                    {
                        mlist = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                        if (mlist.Count == 13)
                        {
                            int col = 0;
                            try
                            {
                                _layer_no = mlist.GetInt(col++);


                                lay = lay_sec.Layers[_layer_no - 1];

                                //lay.LayerNo = mlist.GetInt(col++);


                                lay.z = mlist.GetDouble(col++);
                                lay.L = mlist.GetDouble(col++);
                                lay.La = mlist.GetDouble(col++);
                                col++;
                                col++;
                                col++;
                                col++;
                                //lay.Strip_Strength = mlist.GetDouble(col++);
                                //lay.Number_Strips = mlist.GetInt(col++);
                                lay.fn = mlist.GetDouble(col++);

                                //lay_sec.Layers.Add(lay);
                                //Layer      z          L        La    Strength  Num.  Ties      Case     f*         Za        Tmax     Tf        Tf/Tm

                                //1         0.385     0.585    20.000     8     4       1     0.390    26.146     7.648     6.501     2.469     2.904
                                //                                                      2     0.390    24.993     7.311     6.214     2.583     3.038

                                //2         0.785     0.600    37.500     8     4       1     0.358    28.879     8.664     7.364     2.179     2.564
                                //                                                      2     0.358    27.394     8.218     6.985     2.297     2.703

                                //3         1.585     0.825    37.500     8     4       1     0.293    32.539    13.422    11.409     1.407     1.655
                            }
                            catch (Exception ex) { }
                        }
                    }

                }
                Layout_Sections.Add(lay_sec);
            }
            catch (Exception ex) { }

        }

    }

    public class Reinforcement_Layout
    {
        public int LayerNo { get; set; }
        public int Number_Strips { get; set; }
        public int Strip_Type { get; set; }
        public double Strip_Strength { get; set; }
        public double Strip_Length { get; set; }
        public double L
        {
            get
            {
                return Strip_Length;
            }
            set
            {
                Strip_Length = value;
            }
        }
        public double La { get; set; }


        public double z { get; set; }
        public double delta_h { get; set; }

        public double Za { get; set; }
        public double fn { get; set; }
        //Chiranjit [2013 04 18]

        //output 
        public int TiesNum { get { return (Number_Strips / 2); } }
        public List<string> Case { get; set; }
        public List<double> k { get; set; }
        public List<double> sigma_h { get; set; }
        public List<double> Tmax { get; set; }
        public List<double> To { get; set; }
        public List<double> Tr_by_Tm { get; set; }
        public List<double> Tro_by_To { get; set; }
        public List<double> Tf { get; set; }
        public List<double> Tf_by_Tm { get; set; }

        public External_Stability Ext_Stability_Table { get; set; }

        public double Adherence_Length
        {
            get
            {
                return La;
            }
        }

        //Chiranjit [2013 04 22]
        //Earth Pressure
        public double delta { get; set; }
        public double k2x { get; set; }
        public double k2y { get; set; }
        public double x { get; set; }
        public double y { get; set; }

        public List<string> Get_Earth_Pressure_Report()
        {
            //delta = 0.0;
            //k2x = 0.0;
            //k2y = 0.0;
            //x = 0.0;
            //y = 0.0;

            List<string> list = new List<string>();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Inclination of Earth pressure at back of R.E. mass = δ = {0:f3} °(deg)", delta));
            list.Add(string.Format(""));
            list.Add(string.Format("Earth Pressure coefficients :"));
            list.Add(string.Format(""));
            list.Add(string.Format("k2x = {0:f3},           k2y = {1:f3}  (static)", k2x, k2y));
            list.Add(string.Format(""));
            list.Add(string.Format("x = {0:f3} m,           y = {1:f3} m", x, y));
            list.Add(string.Format(""));
            return list;
        }
        public void Clear_Data()
        {
            Case.Clear();
            k.Clear();
            sigma_h.Clear();
            Tmax.Clear();
            To.Clear();
            Tr_by_Tm.Clear();
            Tro_by_To.Clear();
            Tf.Clear();
            Tf_by_Tm.Clear();
        }

        public Reinforcement_Layout(int _layerNo)
        {
            LayerNo = _layerNo;
            Number_Strips = 0;
            Strip_Strength = 0.0;
            Strip_Length = 0.0;
            z = 0.0;
            delta_h = 0.0;

            Case = new List<string>();

            k = new List<double>();
            sigma_h = new List<double>();
            Tmax = new List<double>();
            To = new List<double>();
            Tr_by_Tm = new List<double>();
            Tro_by_To = new List<double>();
            Za = 0.0;
            fn = 0.0;

            Tf = new List<double>();
            Tf_by_Tm = new List<double>();
            Ext_Stability_Table = new External_Stability();

            delta = 0.0;
            k2x = 0.0;
            k2y = 0.0;
            x = 0.0;
            y = 0.0;
        }

        public string[] ToArray()
        {
            List<string> list = new List<string>();

            list.Add(LayerNo.ToString());
            list.Add(Number_Strips.ToString());
            list.Add(Strip_Type.ToString());
            list.Add(Strip_Strength.ToString("f3"));
            list.Add(Strip_Length.ToString("f3"));
            list.Add(z.ToString("f3"));
            list.Add(delta_h.ToString("f3"));
            list.Add(fn.ToString("f3"));

            return list.ToArray();
        }

        string format1 = "{0,-5} {1,9:f3} {2,9:f3} {3,9:f3} {4,5:f0} {5,5:f0} {6,7:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3}";
        string format2 = "{0,-5} {1,9:f3} {2,9:f3} {3,9:f3} {4,9:f3} {5,5:f0} {6,5:f0} {7,9:f0} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3}";
        //LayerNo         z   delta_h      Type      Num.      Num.      Case         k   sigma_h      Tmax        To     Tr/Tm    Tro/To


        public List<string> TABLE_HEAD_Unit_1()
        {

            List<string> list = new List<string>();
            string kStr = "";


            //                                             Strip                Ties                                                                      

            //   LayerNo         z   delta_h      Type      Num.      Num.      Case         k   sigma_h      Tmax        To     Tr/Tm    Tro/To
            //                 (m)       (m)                                                       (kPa)      (kN)      (kN)                    

            //         1     0.385     0.585    20.000     8.000     4.000         1     0.390    26.146     7.648     6.501     1.308     1.538
            //         2     0.785     0.600    37.500     8.000     4.000         1     0.358    28.879     8.664     7.364     2.179     2.564
            //         3     1.585     0.825    37.500     8.000     4.000         1     0.293    32.539    13.422    11.409     1.407     1.655

            kStr = (string.Format(format1,
                "",
                "",
                "",
                "Strip",
                "Strip",
                "Ties",
                "",
                "",
                "",
                "",
                "",
                "",
                ""));

            list.Add("".PadLeft(kStr.Length, '-'));

            list.Add(string.Format(format1,
                "",
                "",
                "",
                "Strip   ",
                "Strip",
                "Ties",
                "",
                "",
                "",
                "",
                "",
                "",
                ""));

            kStr = "".PadLeft(list[0].Length, '-');

            list.Add(string.Format(format1,
                "Layer",
                "z   ",
                "delta_h",
                "Strength",
                "Num.",
                "Num.",
                "Case",
                "k  ",
                "sigma_h",
                "Tmax",
                "To   ",
                "Tr/Tm",
                "Tro/To"));
            list.Add(string.Format(format1,
                "",
                "(m)  ",
                "(m)   ",
                "(kN)  ",
                "",
                "",
                "",
                "",
                "(kPa)  ",
                "(kN) ",
                "(kN)  ",
                "",
                ""));
            list.Add(kStr);



            return list;
        }

        public List<string> TABLE_HEAD_Unit_2()
        {


            List<string> list = new List<string>();
            string kStr = "";


            kStr = (string.Format(format2,
                "",
                "",
                "",
                "Strip",
                "Strip",
                "Ties",
                "",
                "",
                "",
                "",
                "",
                "",
                ""));

            list.Add("".PadLeft(kStr.Length, '-'));

            //list.Add(string.Format(format,
            //    "",
            //    "",
            //    "",
            //    "Strip   ",
            //    "Strip",
            //    "Ties",
            //    "",
            //    "",
            //    "",
            //    "",
            //    "",
            //    "",
            //    ""));

            kStr = "".PadLeft(list[0].Length, '-');

            list.Add(string.Format(format2,
                "Layer",
                "z   ",
                "L  ",
                "La  ",
                "Strength",
                "Num.",
                "Ties",
                "Case",
                "f*   ",
                "Za  ",
                "Tmax",
                "Tf   ",
                "Tf/Tm"));
            list.Add(string.Format(format2,
                "",
                "(m)  ",
                "(m) ",
                "(m)  ",
                "(kN)  ",
                "",
                "",
                "",
                "",
                "(m)  ",
                "(kN)",
                "(kN)  ",
                ""));
            list.Add(kStr);



            return list;
        }

        public List<string> Get_Report_Internal_Stability()
        {
            List<string> list = new List<string>();

            //string format = "{0,10} {1,9:f3} {2,9:f3} {3,9:f3} {4,9:f3} {5,9:f3} {6,9:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3}";

            if (Case.Count == 0) Case.Add("1");
            list.Add(string.Format(format1,
                LayerNo,
                z,
                delta_h,
                Strip_Strength,
                Number_Strips,
                TiesNum,
                Case[0],
                k[0],
                sigma_h[0],
                Tmax[0],
                To[0],
                Tr_by_Tm[0],
                Tro_by_To[0]));

            for (int i = 1; i < Case.Count; i++)
            {

                list.Add(string.Format(format1,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    Case[i],
                    k[i],
                    sigma_h[i],
                    Tmax[i],
                    To[i],
                    Tr_by_Tm[i],
                    Tro_by_To[i]));
            }
            list.Add(string.Format(""));


            return list;
        }

        public List<string> Get_Report_Adherence()
        {

            List<string> list = new List<string>();

            //string format = "{0,10} {1,9:f3} {2,9:f3} {3,9:f3} {4,9:f3} {5,9:f3} {6,9:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3}";

            if (Case.Count == 0) Case.Add("1");
            if (fn == 0.0)
            {
                fn = 1.001;
            }

            if (Za == 0.0)
            {
                Za = 2.385;
            }

            list.Add(string.Format(format2,
                LayerNo,
                z,
                L,
                La,
                Strip_Strength,
                Number_Strips,
                TiesNum,
                Case[0],
                fn,
                Za,
                Tmax[0],
                Tf[0],
                Tf_by_Tm[0]));

            for (int i = 1; i < Case.Count; i++)
            {

                list.Add(string.Format(format2,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                Case[i],
                fn,
                Za,
                Tmax[i],
                Tf[i],
                Tf_by_Tm[i]));
            }
            list.Add(string.Format(""));

            return list;
        }



    }
    public class Reinforcement_Layout_Section
    {
        public double Start_H1 { get; set; }
        public double End_H1 { get; set; }
        public double H2 { get; set; }
        public double Embedment_Depth { get; set; }

        public List<Reinforcement_Layout> Layers { get; set; }

        public Reinforcement_Layout_Section()
        {
            Start_H1 = 0.0;
            End_H1 = 0.0;
            H2 = 0.0;
            Embedment_Depth = 0.46;
            Layers = new List<Reinforcement_Layout>();


        }

        public static Reinforcement_Layout_Section Get_Default_Layout_Data(double H1)
        {
            Reinforcement_Layout_Section sec = new Reinforcement_Layout_Section();
            int layer_count = 1;
            if (H1 >= 2.01 && H1 < 2.41)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 3.1;

                sec.Layers.Add(lay);
                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.1;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 2.41 && H1 < 2.81)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 2.81 && H1 < 3.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 3.21 && H1 < 3.61)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.4;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 3.61 && H1 < 4.01)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 3.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.6;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 4.01 && H1 < 4.41)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 4.41 && H1 < 4.81)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 2;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 3.8;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.40;
            }
            else if (H1 >= 4.81 && H1 < 5.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.0;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.0;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.40;

            }
            else if (H1 >= 5.21 && H1 < 5.61)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.2;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.41;
            }

            else if (H1 >= 5.61 && H1 < 6.01)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.5;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.43;
            }
            else if (H1 >= 6.01 && H1 < 6.41)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.7;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.46;
            }
            else if (H1 >= 6.41 && H1 < 6.81)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.1;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.51;
            }
            else if (H1 >= 6.81 && H1 < 7.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.54;
            }
            else if (H1 >= 7.21 && H1 < 7.61)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.4;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.58;
            }
            else if (H1 >= 7.61 && H1 < 8.01)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 5.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 5.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.8;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.6;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.60;
            }
            else if (H1 >= 8.01 && H1 < 8.41)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 4.9;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.63;
            }
            else if (H1 >= 8.41 && H1 < 8.81)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 6.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.4;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.1;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.1;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.1;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.1;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.1;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.65;
            }
            else if (H1 >= 8.81 && H1 < 9.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.2;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.69;

            }
            else if (H1 >= 9.21 && H1 < 9.61)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.9;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.5;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.70;

            }
            else if (H1 >= 9.61 && H1 < 10.01)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 7.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.7;


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 5.7;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.74;
            }

            else if (H1 >= 10.01 && H1 < 10.41)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.0;
                sec.Layers.Add(lay); sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.0;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.75;
            }
            else if (H1 >= 10.41 && H1 < 10.81)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.2;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.78;
            }
            else if (H1 >= 10.81 && H1 < 11.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.0;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.5;
                sec.Layers.Add(lay);




                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.5;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.5;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.79;
            }

            else if (H1 >= 11.21 && H1 < 11.61)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.3;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 4;
                lay.Strip_Length = 6.6;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.83;
            }
            else if (H1 >= 11.61 && H1 < 12.01)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.6;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 4;
                lay.Strip_Length = 6.8;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.83;
            }
            else if (H1 >= 12.01 && H1 < 12.41)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 8.9;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.1;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.87;
            }
            else if (H1 >= 12.41 && H1 < 12.81)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.2;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 208;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);



                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.3;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.91;
            }
            else if (H1 >= 12.81 && H1 < 13.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.4;
                sec.Layers.Add(lay);
                sec.Embedment_Depth = 0.94;
            }

            else if (H1 >= 13.21)
            {
                Reinforcement_Layout lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 1;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 2;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 8;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 10;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 9.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 12;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 14;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);


                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 16;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 3;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 18;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);

                lay = new Reinforcement_Layout(layer_count++);

                lay.Number_Strips = 20;
                lay.Strip_Type = 4;
                lay.Strip_Length = 7.7;
                sec.Layers.Add(lay);

                sec.Embedment_Depth = 0.94;
            }
            return sec;

        }
        //public void Add_Layout(Reinforcement_Layout layout)
        //{
        //    for (int i = 0; i < Layers.Count; i++)
        //    {
        //        if (layout.LayerNo == Layers[i].LayerNo)
        //        {

        //        }
        //    }
        //}

    }

    public class Polymetric_Strip
    {
        public int Type { get; set; }
        public double Ultimate_Tensile_Strength { get; set; }
        public double Width { get; set; }
        public double Long_Term_Design_Strength { get; set; }
        public double Long_Term_Design_Strength_Consider { get; set; }

        public Polymetric_Strip(int _type)
        {
            Type = _type;
            Ultimate_Tensile_Strength = 0.0;
            Width = 0.0;
            Long_Term_Design_Strength = 0.0;
            Long_Term_Design_Strength_Consider = 0.0;
        }
    }

    public class External_Stability
    {
        public List<string> Case { get; set; }
        public List<double> Rv { get; set; }
        public List<double> Rh { get; set; }
        public List<double> M { get; set; }
        public List<double> q_ref { get; set; }
        public List<double> _2_x { get; set; }

        public double depth { get; set; }


        public External_Stability()
        {
            Case = new List<string>();
            Rv = new List<double>();
            Rh = new List<double>();
            M = new List<double>();
            q_ref = new List<double>();
            _2_x = new List<double>();
            depth = 0.46;
        }


        public List<string> Get_Report_Lines()
        {
            List<string> list = new List<string>();

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("EXTERNAL STABILITY"));
            //list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));


            string format = "{0,-10} {1,12:f3} {2,12:f3} {3,12:f3} {4,12:f3} {5,12:f3}";

            list.Add(string.Format("------------------------------------------------------------------------------------"));
            list.Add(string.Format(format, "Case", "Rv  ", "Rh  ", "M    ", "q_ref", "2 * x"));
            list.Add(string.Format(format, "    ", "(kN/m)", "(kN/m)", "(kN-m/m)", "(kPa)", "(m)"));
            list.Add(string.Format("------------------------------------------------------------------------------------"));
            for (int i = 0; i < Case.Count; i++)
            {
                list.Add(string.Format(format, Case[i], Rv[i], Rh[i], M[i], q_ref[i], _2_x[i]));
            }

            list.Add(string.Format("------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Minimum embedment depth = {0:f3} m", depth));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            return list;
        }

    }


}
