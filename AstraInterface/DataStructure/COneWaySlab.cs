using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AstraInterface.Interface;

namespace AstraInterface.DataStructure
{
    public class COneWaySlab : ISLAB02
    {
        #region Member Variable
        double l; // Floor Slab Length
        double b; // Floor Slab Breadth
        double ll; // Super imposed/ Live Load
        double sigma_ck; // Concrete Grade
        double sigma_y; // steel Grade
        double alpha; // Constants 
        double beta; // Constants
        double gamma; // Constants
        double delta; // Constants
        double lamda; // Constants
        double d1; // Dia of Main Reinforcement
        double d2; // Dia of Distribution Reinforcement
        double h1; // Clear Cover
        double h2; // End Cover
        double ads; // Provide Distribution Reinforcements
        double tc; // Shear Strength of concrete as % of steel
        double Slab_load; // Shear Strength of concrete as % of steel
        double w1; // Thickness of supporting wall


        KValueTable kValue = null;

        string DataPath = "";
        #endregion
        #region ctor
        public COneWaySlab(string kValueFilePath, string dataPath)
        {
            l = 0.0; // Floor Slab Length
            b = 0.0; // Floor Slab Breadth
            ll = 0.0; // Super imposed/ Live Load
            sigma_ck = 0.0; // Concrete Grade
            sigma_y = 0.0; // steel Grade
            alpha = 0.0; // Constants 
            beta = 0.0; // Constants
            gamma = 0.0; // Constants
            delta = 0.0; // Constants
            lamda = 0.0; // Constants
            d1 = 0.0; // Dia of Main Reinforcement
            d2 = 0.0; // Dia of Distribution Reinforcement
            h1 = 0.0; // Clear Cover
            h2 = 0.0; // End Cover
            ads = 0.0; // Provide Distribution Reinforcements
            tc = 0.0; // Shear Strength of concrete as % of steel
            Slab_load = 0.0d;
            w1 = 0.0;
            kValue = new KValueTable(kValueFilePath);
            if (File.Exists(dataPath))
            {
                DataPath = Path.GetDirectoryName(dataPath);
            }
            else
                DataPath = dataPath;

            DataPath = Path.Combine(DataPath, "DESIGN_SLAB02");
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);
            //CalculateMethod();
        }
        #endregion
        #region Public Property
        #region ISLAB02 Members

        public double L
        {
            get
            {
                return l;
            }
            set
            {
                l = value;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        public double LL
        {
            get
            {
                return ll;
            }
            set
            {
                ll = value;
            }
        }

        public double Sigma_ck
        {
            get
            {
                return sigma_ck;
            }
            set
            {
                sigma_ck = value;
            }
        }

        public double Sigma_y
        {
            get
            {
                return sigma_y;
            }
            set
            {
                sigma_y = value;
            }
        }

        public double Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
            }
        }

        public double Beta
        {
            get
            {
                return beta;
            }
            set
            {
                beta = value;
            }
        }

        public double Gamma
        {
            get
            {
                return gamma;
            }
            set
            {
                gamma = value;
            }
        }

        public double Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
            }
        }

        public double Lamda
        {
            get
            {
                return lamda;
            }
            set
            {
                lamda = value;
            }
        }

        public double D1
        {
            get
            {
                return d1;
            }
            set
            {
                d1 = value;
            }
        }

        public double D2
        {
            get
            {
                return d2;
            }
            set
            {
                d2 = value;
            }
        }

        public double Slab_Load
        {
            get
            {
                return Slab_load;
            }
            set
            {
                Slab_load = value;
            }
        }
        public double H1
        {
            get
            {
                return h1;
            }
            set
            {
                h1 = value;
            }
        }

        public double H2
        {
            get
            {
                return h2;
            }
            set
            {
                h2 = value;
            }
        }

        public double Ads
        {
            get
            {
                return ads;
            }
            set
            {
                ads = value;
            }
        }

        public double Tc
        {
            get
            {
                return tc;
            }
            set
            {
                tc = value;
            }
        }

        #endregion

        #endregion

        #region Public Method

        public void CalculateMethod(ref string reportFileName)
        {
            string fName = Path.Combine(DataPath, "DESIGN_OF_ONE_WAY_RCC_SLAB.TXT");

            string file_path = Path.Combine(DataPath, "AstraSys");

            if (!Directory.Exists(file_path))
                Directory.CreateDirectory(file_path);

            string view_file = Path.Combine(file_path, "DESIGN.FIL");
            string boq_file = Path.Combine(file_path, "BoQ.TXT");
            StreamWriter sw_view = new StreamWriter(new FileStream(view_file, FileMode.Create));
            StreamWriter sw_boq = new StreamWriter(new FileStream(boq_file, FileMode.Create));
            StreamWriter sw = new StreamWriter(new FileStream(fName, FileMode.Create));
            reportFileName = fName;
            try
            {
                 sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                   *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*           DESIGN OF SINGLE SPAN             *");
                sw.WriteLine("\t\t*  ONE WAY RCC SLAB BY LIMIT STATE METHOD     *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t-----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(" Length Perpendicular to Span [L] = {0} m", L);
                sw.WriteLine(" Design Span for Slab [B]= {0} m", B);
                sw.WriteLine(" Super imposed / Live Load [LL]= {0} kN/sq.m", LL);
                sw.WriteLine(" Slab Load = {0} kN/sq.m",Slab_load);
                sw.WriteLine(" Concrete Grade [f_ck] = M {0} N/sq.mm",sigma_ck);
                sw.WriteLine(" steel Grade [f_y] = Fe {0} N/sq.mm",sigma_y);
                sw.WriteLine(" Dia of Main Reinforcement [d1] = {0} mm",d1);
                sw.WriteLine(" Dia of Distribution Reinforcement [d2] = {0} mm",d2);
                sw.WriteLine(" Clear Cover [h1] = {0} mm",h1);
                sw.WriteLine(" End Cover [h2] = {0} mm",h2);
                sw.WriteLine(" Provide Distribution Reinforcements [ads] = {0} %",ads);
                sw.WriteLine(" Shear Strength of Conctrete as % of steel = {0} %",tc);
                sw.WriteLine(" α = {0}, β = {1}, γ = {2}, δ = {3}, λ = {4})", alpha, beta, gamma, delta, lamda);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 1: Calculations for Overall and effective depth");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Since length of the slab is more than twice the width, it is a one way slab.");
                sw.WriteLine("Load will be transferred to the supports along the shorter span.");
                sw.WriteLine("Consider a 100 cm wide strip of the slab parallel to its shorter span.");

                double d, lowest_Span;

                double val1, val2;
                lowest_Span = (L > B) ? B * 1000 : L * 1000;
                d = (lowest_Span / (alpha * beta * gamma * delta * lamda));

                sw.WriteLine("Minimum depth of slab d = L /(α * β * γ * δ * λ)");
                sw.WriteLine();
                sw.WriteLine("Let α = {0}, β = {1}, γ = {2}, δ = {3} and λ = {4}", alpha, beta, gamma, delta, lamda);
                sw.WriteLine("So, d = {0}/{1} = {2} mm", lowest_Span.ToString("0.0"), (alpha * beta * gamma * delta * lamda).ToString("0.00"), d.ToString("0.00"));

                double D = (d + h1);
                sw.WriteLine("Let us adopt overall depth   D = {0} mm.", D.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 2: Calculations for Design Load, Moment and Shear");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double deadLoad_slab = (D / 1000) * 1.0 * Slab_load;
                sw.WriteLine("Dead Load of slab = {0} * {1} * {2} = {3} kN/m.", (D / 1000).ToString("0.00"), 1.0, Slab_load, deadLoad_slab.ToString("0.00"));
                sw.WriteLine("Superimposed load = {0} * 1 = {1} kN/m.", ll.ToString("0.00"), ll.ToString("0.00"));
                double totalLoad = deadLoad_slab + ll;
                sw.WriteLine("Total Load  = {0} kN/m.", totalLoad.ToString("0.00"));

                double factoredLoad, loadFactor;
                loadFactor = 1.5d;
                factoredLoad = totalLoad * loadFactor;
                sw.WriteLine("Factored load if the load factor is {0}", loadFactor.ToString("0.00"));
                sw.WriteLine("                               = {0} * {1} = {2} kN/m", loadFactor.ToString("0.00"), totalLoad.ToString("0.00"), factoredLoad.ToString("0.00"));
                sw.WriteLine("Maximum BM at centre of shorter span");
                sw.WriteLine("                       = (Wu * l * l) / 8");
                sw.WriteLine("Assume steel consists of {0} mm bars with {1} mm clear cover.", d1, h1);

                double half_depth = d1 / 2;
                double eff_depth = D - h1 - half_depth;
                sw.WriteLine("Effective depth = {0} - {1} - {2} = {3} mm", D.ToString("0.0"), h1.ToString("0.0"), half_depth.ToString("0.00"), eff_depth.ToString("0.00"));

                lowest_Span = lowest_Span / 1000;

                double eff_span = (lowest_Span) + (eff_depth / 1000);
                sw.WriteLine("Effective Span of Slab = {0} + d = {0} + {1} = {2} m", (lowest_Span).ToString("0.00"), (eff_depth / 1000).ToString("0.00"), eff_span.ToString("0.00"));
                double BM = factoredLoad * eff_span * eff_span / 8;
                //sw.WriteLine("So, BM = ({0} * {1} * {1}) / 8 = {2} kNm", factoredLoad.ToString("0.00"), eff_span.ToString("0.00"), BM.ToString("0.00"));
                sw.WriteLine("So, BM = M = ({0} * {1} * {1}) / 8 = {2} kNm", factoredLoad.ToString("0.00"), eff_span.ToString("0.00"), BM.ToString("0.00"));
                sw.WriteLine("Max shear force = Vu = (Wn * lc) / 2");
                double max_shear_force = (factoredLoad * lowest_Span / 2.0);
                sw.WriteLine("                = {0} * {1}/2 = {2} kN = {3} N", factoredLoad.ToString("0.00"), lowest_Span.ToString("0.0"), max_shear_force.ToString("0.0"), (max_shear_force * 1000).ToString("0.00"));

                max_shear_force *= 1000;


                sw.WriteLine("Depth of the slab is given by");
                sw.WriteLine("              BM = 0.138 * σ_ck * b * d* d");

                double M = (BM * 10E+5);
                d = ((BM * 10E+5) / (0.138 * sigma_ck * 1000));
                d = Math.Sqrt(d);
                sw.WriteLine("or      d = √(({0} * 10E+5)/(0.138 * {1} * 1000)) = {2} mm", BM.ToString("0.00"), sigma_ck.ToString("0.00"), d.ToString("0.00"));

                d = (int)(d / 10);
                d += 2;
                d *= 10;
                sw.WriteLine("Adopt effective depth d = {0} mm and over all depth", d.ToString("0.0"));
                sw.WriteLine("                      D = {0} mm", eff_depth);


                sw.WriteLine("Adopt of tension steel is given by ");
                sw.WriteLine("             M = 0.87 * σ_y * A_t( d - ((σ_y * A_t)/(σ_ck * b))");

                double a, b, c, At;

                a = (0.87 * sigma_y * sigma_y) / (sigma_ck * 1000);
                b = 0.87 * sigma_y * d;
                c = M;
                double b_ac = (b * b) - 4 * a * c;
                At = (b) - Math.Sqrt(Math.Abs(b_ac));
                At = At / (2 * a);

                At = (int)At / 10;
                At += 1;
                At = At * 10;
                sw.WriteLine("   {0} * 10E+5 = 0.87 * {1} * At * ({2} - {3} * At / ({4} * 1000))",
                    BM.ToString("0.00"), sigma_y.ToString("0.00"), d.ToString("0.0"), sigma_y.ToString("0.00"), sigma_ck.ToString("0.00"));
                sw.WriteLine("or                     At = {0} sq.mm", At);
                sw.WriteLine("Use {0} mm bars @ {1} mm c/c giving total area ", d1, d.ToString("0.0"));

                double est_value = (Math.PI * (d1 * d1) / 4) * (1000 / d);

                val1 = est_value;
                val2 = At;
                if (val1 > val2)
                {
                    sw.WriteLine("                         = {0} sq.mm. > {1} sq.mm      OK", est_value.ToString("0"), At.ToString("0"));
                }
                else
                {
                    sw.WriteLine("                         = {0} sq.mm. < {1} sq.mm      NOT OK", est_value.ToString("0"), At.ToString("0"));
                }
                double n_rod = At / ((Math.PI * (d1 * d1) / 4));

                n_rod = (int)n_rod;
                n_rod += 1;

                sw.WriteLine("  Bend alternate bars at L/{0} from the face of support ", n_rod);
                sw.WriteLine("where moment reduces to less than half its maximum value.");
                sw.WriteLine("Temperature reinforcement equal to {0}% of the gross ", ads);
                sw.WriteLine("concrete area will be provided in the longitudinal direction.");

                double dirArea = (ads / 100) * 1000 * eff_depth;
                sw.WriteLine("       = {0} * 1000 * {1} = {2} sq.mm.", (Ads / 100).ToString("0.0000"), eff_depth.ToString("0.00"), dirArea.ToString("0.00"));
                sw.WriteLine("Use {0} mm MS bars @ 100 mm c/c giving total area ", d2.ToString("0"));

                double a_st = Math.PI * d2 * d2 / 4;

                val1 = a_st * 10;
                val2 = dirArea;
                if (val1 > val2)
                {
                    sw.WriteLine("   = {0} * (1000/100) = {1} sq.mm.  > {2} sq.mm            OK", a_st.ToString("0.00"), (a_st * 10).ToString("0.00"), dirArea.ToString("0.0"));
                }
                else
                {
                    sw.WriteLine("   = {0} * (1000/100) = {1} sq.mm.  < {2} sq.mm           NOT OK", a_st.ToString("0.00"), (a_st * 10).ToString("0.00"), dirArea.ToString("0.0"));
                }


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 3: Check for Shear");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Percent tension steel = (100 * At)/ (b * d)");
                a_st = Math.PI * d1 * d1 / 4;



                double percent = (100 * (a_st * (1000 / 300))) / (1000 * d);
                sw.WriteLine("                      = (100 * ({0} * (1000/300)) / (1000 * {1}) = {2}%", a_st.ToString("0.0"), d.ToString("0.0"), percent.ToString("0.00"));
                sw.WriteLine("Shear strength of concrete for {0}% steel", percent.ToString("0.00"));
                ShearValue sh = new ShearValue();
                double tau_c = 0.0;

                tau_c = sh.Get_M15(percent);

                sw.WriteLine("     τ_c = {0} N/sq.mm.", tau_c);


                double k = kValue.Get_KValue(eff_depth);
                double tc_dash = k * tau_c;





                //tau_c = tau_c;
                sw.WriteLine("For {0} mm thick slab, k = {1}", eff_depth.ToString("0.00"), k.ToString("0.00"));
                sw.WriteLine("         So, τ_c` = k * Tc = {0} * {1} = {2} N/sq.mm", k.ToString("0.00"), tau_c.ToString("0.00"), tc_dash.ToString("0.00"));
                double Vu = max_shear_force;
                double t_v = Vu / (1000 * d);

                sw.WriteLine("Nominal shear stress Tv = Vu / b * d = {0}/(1000 * {1}) = {2} N/sq.mm", Vu, d, t_v.ToString("0.00"));

                sw.WriteLine("The Slab is safe in shear.");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 4: Check for development length");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Moment of resistance offered by {0} mm bars @ {1} mm c/c", d1,d*2);
                sw.WriteLine("M1 = 0.87 * σ_y * At * (d - (σ_y * At / σ_ck * b))");
                sw.WriteLine("   = 0.87 * {0} * {1} * (1000/300) * ({2} - ({3} * {4} * (1000 / 300)) / {5} * 1000)",
                    sigma_y.ToString("0.00"), a_st.ToString("0.00"), d.ToString("0.00"), sigma_y.ToString("0.00"), a_st.ToString("0.00"), Sigma_ck.ToString("0.00"));

                double M1 = 0.87 * sigma_y * a_st * (1000.0 / 300.0) * (d - (sigma_y * a_st * (1000.0d / 300.0d) / (sigma_ck * 1000.0)));





                sw.WriteLine("M1 = {0} Nmm", M1.ToString("0.00"));
                sw.WriteLine("Vu  = {0} N", Vu);
                sw.WriteLine("Let us assume anchorage length Lo = 0");
                sw.WriteLine("                  Ld <= 1.3 * (M1/Vu)");
                sw.WriteLine("                  56φ <= 1.3 * ({0}/{1})", M1.ToString("0.00"), Vu.ToString("0.00"));

                double phi = (1.3 * (M1 / Vu) / 56.0);


                val1 = phi;
                val2 = d1;
                if (val1 > val2)
                {
                    sw.WriteLine("                  φ < {0}", phi.ToString("0.00"));
                    sw.WriteLine("We have provided φ = {0} mm, So OK.", d1.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("                  φ > {0}", phi.ToString("0.00"));
                    sw.WriteLine("We have provided φ = {0} mm, So NOT OK.", d1.ToString("0.00"));
                }

                sw.WriteLine("The Code requires that bars must be carried into the supports by atleast Ld / 3 = 190 mm");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 5: Check for deflection");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                
                sw.WriteLine("Percent tension steel at midspan");
                sw.WriteLine("               = (100 * As) / (b * d)");
                sw.WriteLine("               = (100 * {0} * 1000 / 150) / (1000 * 150)");
                double pps = (100 * 78.5 * 1000 / 150) / (1000 * 150);
                sw.WriteLine("               = {0}%", pps.ToString("0.00"));

                double gama = ModificationFactor.GetGamma(pps);
                sw.WriteLine("             γ = {0}%", gama.ToString("0.00"));
                //sw.WriteLine(" σσσγγβαδλ■  √");
                sw.WriteLine("  β = {0}, δ = {1} and λ = {2}", beta, delta, lamda);

                // Constant 20
                sw.WriteLine("Allowable L/d = 20 * {0} = {1}", gama.ToString("0.00"), (20 * gama).ToString("0.00"));
                
                val1 = (eff_span * 1000) / d;
                val2 = (20 * gama);

                if (val1 < val2)
                {
                    sw.WriteLine("Actual L/d = {0} / {1} = {2} < {3}       OK", (eff_span * 1000).ToString("0.00"), d, (eff_span * 1000 / d).ToString("0.00"), (20 * gama).ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("Actual L/d = {0} / {1} = {2} > {3}       NOT OK", (eff_span * 1000).ToString("0.00"), d, (eff_span * 1000 / d).ToString("0.00"), (20 * gama).ToString("0.00"));
                }
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
     
                double fd = 500;

                sw_view.WriteLine("SLAB DESIGN 02");
                sw_view.WriteLine("L = {0}",lowest_Span*1000);
                sw_view.WriteLine("D = {0}",eff_depth);
                sw_view.WriteLine("b1 = {0}",d);
                sw_view.WriteLine("b2 = 100");
                sw_view.WriteLine("h1 = {0}",h1);
                sw_view.WriteLine("h2 = {0}",h2);
                sw_view.WriteLine("d1 = {0}",d1);
                sw_view.WriteLine("d2 = {0}",d2);
                sw_view.WriteLine("fd = {0:f0}", fd);
                sw_view.WriteLine("w1 = {0}", w1);
                sw_view.WriteLine("w2 = {0}", w1);
                sw_view.WriteLine("END");
                
//BoQ CODE
//S_No 1.,2.,3.
//Member 3.5 8.0
//Bar_Mark 01,02,03
//Bar_CODE T10_B1,T6_B2,T10_B1
//Bar_Grade Fe415,Fe415,Fe415
//Bar_Dia 10,6,10
//Bar_Length 3470,7970,3470
//Bar_Nos 56,35,56
//Bar_Weight 0.330,0.220,0.330
//Bar_Shape 3019,120,1209
//Bar_Shape 1209,120,3019
//END BoQ

                int main_bar_nos, dist_bar_nos;
                double wt_main_bar, wt_dist_bar;


                sw_boq.WriteLine("BoQ Code");
                sw_boq.WriteLine("S_No {0}.,{1}.,{2}.",1,2,3);
                sw_boq.WriteLine("Member {0} {1}", B.ToString("0.0"), L.ToString("0.0"));
                sw_boq.WriteLine("Bar_Spacing {0},{1}", d.ToString("0.0"), "100.0");
                sw_boq.WriteLine("Bar_Mark 01,02,03");
                sw_boq.WriteLine("Bar_Code T{0}_B1,T{1}_B2,T{0}_B1",d1,d2);
                sw_boq.WriteLine("BAR_Grade Fe{0},Fe{0},Fe{0}",sigma_y);
                sw_boq.WriteLine("Bar_Dia {0},{1},{0}",d1,d2);
                sw_boq.WriteLine("Bar_Length {0},{1},{0}",(B*1000 - 2*h1),(L*1000 - 2*h1));

                main_bar_nos = (int)(L*1000 / d);
                dist_bar_nos = (int)(B*1000 / 100.0d);

                wt_main_bar = 0.00616 * d1 * d1 * (B) * main_bar_nos;
                wt_dist_bar = 0.00616 * d2 * d2 * (L) * dist_bar_nos;
                wt_main_bar /= 1000.0d;
                wt_dist_bar /= 1000.0d;
                //sw_boq.WriteLine("Bar_Nos {0},{1},{0}", "56", "35", "56");
                sw_boq.WriteLine("Bar_Nos {0},{1},{0}", main_bar_nos, dist_bar_nos);
                sw_boq.WriteLine("Bar_Weight {0},{1},{0}", wt_main_bar.ToString("0.000"), wt_dist_bar.ToString("0.000"));
                //sw_boq.WriteLine("Bar_Weight {0},{1},{0}", "0.330", "0.220");

                double k_d = eff_depth - (2 * ((h1 + (d1 / 2))));

                double aaa = Math.Sqrt((k_d * k_d) + (k_d * k_d));

                double tot_len = (lowest_Span * 1000.0) + 2 * w1 - 2 * h1;

                double sh1 = tot_len - (fd + (w1 - h1));
                double sh2 = (fd + (w1 - h1)) - eff_depth;




                sw_boq.WriteLine("Bar_Shape {0},{1:f0},{2}", sh1, aaa, sh2);
                //sw_boq.WriteLine("Bar_Shape {0},{1:f0},{2}", 3140, aaa, 1290);
                sw_boq.WriteLine("Bar_Shape {0}", ((L * 1000.0) - (2 * h1)));

                sw_boq.WriteLine("Bar_Shape {0},{1},{2}", sh2, aaa, sh1);
                sw_boq.WriteLine("END BoQ");
            }
            catch (Exception exx)
            {
                reportFileName = null;
            }
            finally
            {
                GC.Collect();
                sw.Flush();
                sw.Close();
                sw_view.Flush();
                sw_view.Close();
                sw_boq.Flush();
                sw_boq.Close();
            }
        }

        #endregion

        #region ISLAB02 Members


        public double WallThickness
        {
            get
            {
                return w1;
            }
            set
            {
                w1 = value;
            }
        }

        #endregion
    }
  
    public class KValueTable
    {
        //Ashok Kumar Jain
        //Limit State Design
        //Page 353
        string fileName = "";

        MyList mList = null;
        List<double> lstD = null;
        List<double> lstK = null;
        public KValueTable(string fileName)
        {
            //hash_kTable = new Hashtable();
            this.fileName = fileName;
            lstD = new List<double>();
            lstK = new List<double>();
            SetTableValue();
        }
        void SetTableValue()
        {
            //D 300 275 250 225 200 175 150
            //K 1.0 1.05 1.10 1.15 1.20 1.25 1.30
            List<string> lstStr = new List<string>();
            //List<string> lstStr = new List<string>(File.ReadAllLines(fileName));

            lstStr.Add("D 300 275 250 225 200 175 150");
            lstStr.Add("K 1.0 1.05 1.10 1.15 1.20 1.25 1.30");
            string kStr = "";
            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimEnd().TrimStart().ToUpper();
                if (kStr == "K VALUES FOR SHEAR" || kStr == "") continue;
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');

                if (mList.StringList[0] == "D")
                {
                    for (int j = 1; j < mList.Count; j++)
                    {
                        lstD.Add(mList.GetDouble(j));
                    }
                }
                if (mList.StringList[0] == "K")
                {
                    for (int j = 1; j < mList.Count; j++)
                    {
                        lstK.Add(mList.GetDouble(j));
                    }
                }
            }
            lstStr.Clear();
        }

        public double Get_KValue(double D)
        {
            double k = 0.0d;
            for (int i = 0; i < lstD.Count; i++)
            {
                if (D >= lstD[0])
                {
                    k = lstK[0]; break;
                }
                else if (D <= lstD[lstD.Count - 1])
                {
                    k = lstK[lstK.Count - 1]; break;
                }
                else if (D >= lstD[i])
                {
                    k = lstK[i - 1]; break;
                }
            }
            return k;
        }
    }
    public class ShearValue
    {
        //IS 456 :2000
        // Page 73
        List<double> lstPercent = null;
        List<double> lstM15 = null;
        //List<double> lstM20 = null;
        //List<double> lstM25 = null;
        //List<double> lstM30 = null;
        //List<double> lstM35 = null;
        //List<double> lstM40 = null;


        public ShearValue()
        {
            lstPercent = new List<double>();
            lstM15 = new List<double>();
            //lstM20 = List<double>();
            //lstM25 = List<double>();
            //lstM30 = List<double>();
            //lstM35 = List<double>();
            //lstM40 = List<double>();
            SetValue();
        }
        private void SetValue()
        {
            // 1
            lstPercent.Add(0.15);
            //lstM15.Add(0.28);
            //lstM20.Add(0.28);
            //lstM25.Add(0.29);
            //lstM30.Add(0.29);
            //lstM35.Add(0.29);
            //lstM40.Add(0.30);

            // 2
            lstPercent.Add(0.25);
            //lstM15.Add(0.35);
            //lstM20.Add(0.36);
            //lstM25.Add(0.36);
            //lstM30.Add(0.37);
            //lstM35.Add(0.37);
            //lstM40.Add(0.38);

            // 3
            lstPercent.Add(0.5);
            //lstM15.Add(0.46);
            //lstM20.Add(0.48);
            //lstM25.Add(0.49);
            //lstM30.Add(0.50);
            //lstM35.Add(0.50);
            //lstM40.Add(0.51);

            // 4
            lstPercent.Add(0.75);
            //lstM15.Add(0.54);
            //lstM20.Add(0.56);
            //lstM25.Add(0.57);
            //lstM30.Add(0.59);
            //lstM35.Add(0.59);
            //lstM40.Add(0.60);
            //lstM40.Add(0.68);
            //lstM40.Add(0.74);
            //lstM40.Add(0.79);
            //lstM40.Add(0.84);
            //lstM40.Add(0.88);
            //lstM40.Add(0.92);
            //lstM40.Add(0.95);
            //lstM40.Add(0.98);
            //lstM40.Add(1.01);

            // 5
            lstPercent.Add(1.00);
            //lstM15.Add(0.6);
            //lstM20.Add(0.62);
            //lstM25.Add(0.64);
            //lstM30.Add(0.66);
            //lstM35.Add(0.67);

            // 6
            lstPercent.Add(1.25);
            //lstM15.Add(0.64);
            //lstM20.Add(0.67);
            //lstM25.Add(0.7);
            //lstM30.Add(0.71);
            //lstM35.Add(0.73);

            // 7
            lstPercent.Add(1.50);
            //lstM15.Add(0.68);
            //lstM20.Add(0.72);
            //lstM25.Add(0.74);
            //lstM30.Add(0.76);
            //lstM35.Add(0.78);

            // 8
            lstPercent.Add(1.75);
            //lstM15.Add(0.71);
            //lstM20.Add(0.75);
            //lstM25.Add(0.78);
            //lstM30.Add(0.80);
            //lstM35.Add(0.82);

            // 9
            lstPercent.Add(2.0);
            //lstM15.Add(0.71);
            //lstM20.Add(0.79);
            //lstM25.Add(0.82);
            //lstM30.Add(0.84);
            //lstM35.Add(0.86);

            // 10
            lstPercent.Add(2.25);
            //lstM15.Add(0.71);
            //lstM20.Add(0.81);
            //lstM25.Add(0.85);
            //lstM30.Add(0.88);
            //lstM35.Add(0.90);

            // 11
            lstPercent.Add(2.5);
            //lstM15.Add(0.71);
            //lstM20.Add(0.82);
            //lstM25.Add(0.88);
            //lstM30.Add(0.91);
            //lstM35.Add(0.93);

            // 12
            lstPercent.Add(2.75);
            //lstM15.Add(0.71);
            //lstM20.Add(0.82);
            //lstM25.Add(0.90);
            //lstM30.Add(0.94);
            //lstM35.Add(0.96);


            // 13
            lstPercent.Add(3.0);
            //lstM15.Add(0.71);
            //lstM20.Add(0.82);
            //lstM25.Add(0.92);
            //lstM30.Add(0.96);
            //lstM35.Add(0.99);





        }


        //public double GetTau_c(double percent,MType tp)
        //{
        //    switch (tp)
        //    {
        //        case MType.M15:
        //            {

        //            }
        //            break;
        //        case MType.M20:
        //            break;
        //    }
        //}
        public double Get_M15(double percent)
        {
            double tau = 0.0d;

            lstM15.Add(0.28);
            lstM15.Add(0.35);
            lstM15.Add(0.46);
            lstM15.Add(0.54);
            lstM15.Add(0.60);
            lstM15.Add(0.64);
            lstM15.Add(0.68);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);

            for (int i = 0; i < lstPercent.Count; i++)
            {
                if (percent <= lstPercent[0])
                {
                    tau = lstM15[0]; break;
                }
                else if (percent <= lstPercent[i])
                {
                    tau = lstM15[i]; break;
                }
                else if (percent >= lstPercent[lstPercent.Count - 1])
                {
                    tau = lstM15[i]; break;
                }
            }
            return tau;
        }

        public enum MType
        {
            M15 = 0,
            M20 = 1,
            M25 = 2,
            M30 = 3,
            M35 = 4,
            M40 = 5,
        }
    }
    public class ModificationFactor
    {
        //double gamma;
        //double prcnt;
        static List<double> lstGamma = null;
        static List<double> lstPercent = null;

        public ModificationFactor()
        {
        }
        public static double GetGamma(double percent)
        {
            double p1, p2, g1, g2;
            double gamma = 0.0;

            #region Set Values
            lstGamma = new List<double>();
            lstPercent = new List<double>();

            lstPercent.Add(0.0);
            lstGamma.Add(0.0);

            lstPercent.Add(0.1);
            lstGamma.Add(2.0);

            lstPercent.Add(0.2);
            lstGamma.Add(1.6);

            lstPercent.Add(0.3);
            lstGamma.Add(1.4);

            lstPercent.Add(0.4);
            lstGamma.Add(1.28);

            lstPercent.Add(0.6);
            lstGamma.Add(1.1);

            lstPercent.Add(0.8);
            lstGamma.Add(1.05);

            lstPercent.Add(1.0);
            lstGamma.Add(0.95);

            lstPercent.Add(1.2);
            lstGamma.Add(0.91);

            lstPercent.Add(1.4);
            lstGamma.Add(0.9);

            lstPercent.Add(1.6);
            lstGamma.Add(0.84);

            lstPercent.Add(1.8);
            lstGamma.Add(0.82);

            lstPercent.Add(2.0);
            lstGamma.Add(0.805);

            lstPercent.Add(2.2);
            lstGamma.Add(0.81);

            lstPercent.Add(2.4);
            lstGamma.Add(0.8);

            lstPercent.Add(2.6);
            lstGamma.Add(0.795);

            lstPercent.Add(2.8);
            lstGamma.Add(0.79);

            lstPercent.Add(3.0);
            lstGamma.Add(0.78);

            #endregion


            if (percent >= lstPercent[0] && percent <= lstPercent[lstPercent.Count - 1])
            {
                for (int i = 0; i < lstPercent.Count; i++)
                {
                    if (percent == lstPercent[i])
                    {
                        gamma = lstGamma[i]; break;
                    }
                    if (lstPercent[i] > percent)
                    {
                        p1 = lstPercent[i - 1];
                        p2 = lstPercent[i];
                        g1 = lstGamma[i - 1];
                        g2 = lstGamma[i];

                        gamma = ((g2 - g1) / (p2 - p1)) * (percent - p1) + g1;
                        break;
                    }
                }
            }
            return gamma;
        }
    }
    
}
