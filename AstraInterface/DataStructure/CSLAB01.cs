using System;
using System.Collections.Generic;
using System.Text;
using System.IO ;

namespace AstraInterface.DataStructure
{
    public class CSLAB01
    {
        #region Member Variable

        double d1;// Dia of Main Reinforcement
        double d2;// Dia of Distribution Reinforcement
        double h1;// Clear Cover of Reinforcement
        double h2;// Clear Cover of Reinforcement
        double gamma_c;// Unit weight of Concrete
        double gc; // Grade of Concrete
        double l; // Clear Span
        double b1, b2; // Thickness of Support wall at start and end
        double w1; // Weight of Floor Finish + Ceiling Plaster Live Load
        double w2; // Weight of Live Load
        double dst; // Percent of Distribution steel
        double sigma_st;// Strength of Streal Reinforcement
        double s1; // Main Spacing
        double s2; // Distributed Spacing
        double dl;// Dead Load
        double totalLength;
        double d_B;
        //double slab_Load;
        double D;

        string filePath;
        #endregion
        #region ctor
        public CSLAB01(string FilFilePath)
        {
            d1 = 0.0d;// Dia of Main Reinforcement
            d2 = 0.0d;// Dia of Distribution Reinforcement
            h1 = 0.0d;// Clear Cover of Reinforcement
            gamma_c = 0.0d;// Unit weight of Concrete
            gc = 0.0d; // Grade of Concrete
            l = 0.0d; // Clear Span
            b1 = 0.0d;
            b2 = 0.0d; // Thickness of Support wall at start and end
            w1 = 0.0d; // Weight of Floor Finish + Ceiling Plaster Live Load
            w2 = 0.0d; // Weight of Live Load
            dst = 0.0d; // Percent of Distribution steel
            sigma_st = 0.0d;// Strength of Streal Reinforcement

            d1 = 10;
            d2 = 6;
            h1 = 15;
            gamma_c = 25000;
            gc = 15;
            l = 2.5;
            b1 = 250;
            b2 = 250;
            w1 = 744;
            w2 = 2000;
            Dst = 0.15d;
            sigma_st = 140.0d;
            //slab_Load = 25.0d;
            //CalculateProgram("C:\\kSLAB01.txt");
        }
        public CSLAB01()
        {
            d1 = 0.0d;// Dia of Main Reinforcement
            d2 = 0.0d;// Dia of Distribution Reinforcement
            h1 = 0.0d;// Clear Cover of Reinforcement
            gamma_c = 0.0d;// Unit weight of Concrete
            gc = 0.0d; // Grade of Concrete
            l = 0.0d; // Clear Span
            b1 = 0.0d;
            b2 = 0.0d; // Thickness of Support wall at start and end
            w1 = 0.0d; // Weight of Floor Finish + Ceiling Plaster Live Load
            w2 = 0.0d; // Weight of Live Load
            dst = 0.0d; // Percent of Distribution steel
            sigma_st = 0.0d;// Strength of Streal Reinforcement

            d1 = 10;
            d2 = 6;
            h1 = 15;
            gamma_c = 25000;
            gc = 15;
            l = 2.5;
            b1 = 250;
            b2 = 250;
            w1 = 744;
            w2 = 2000;
            Dst = 0.15d;
            sigma_st = 140.0d;
            //CalculateProgram("C:\\kSLAB01.txt");
        }

        #endregion

        #region Private Methods
        #endregion

        #region Public  Property

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
        public double H
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
        public double Gamma_C
        {
            get
            {
                return gamma_c;
            }
            set
            {
                gamma_c = value;
            }
        }
        public double Gc
        {
            get
            {
                return gc;
            }
            set
            {
                gc = value;
            }
        }
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
        public double B1
        {
            get
            {
                return b1;
            }
            set
            {
                b1 = value;
            }
        }
        public double B2
        {
            get
            {
                return b2;
            }
            set
            {
                b2 = value;
            }
        }
        public double W1
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
        public double W2
        {
            get
            {
                return w2;
            }
            set
            {
                w2 = value;
            }
        }
        public double Dst
        {
            get
            {
                return dst;
            }
            set
            {
                dst = value;
            }
        }
        public double Sigma_St
        {
            get
            {
                return sigma_st;
            }
            set
            {
                sigma_st = value;
            }
        }
        public double MainSpace
        {
            get
            {
                return s1;
            }
            set
            {
                s1 = value;
            }
        }
        public double TotalLength
        {
            get
            {
                return totalLength;
            }
            set
            {
                totalLength = value;
            }
        }
        public double B
        {
            get
            {
                return d_B;
            }
            set
            {
                d_B = value;
            }
        }
        public double DistributedSpace
        {
            get
            {
                return s2;
            }
            set
            {
                s2 = value;
            }
        }
        public string FilePath
        {
            get
            {
                return filePath;
            }
        }
        #endregion

        #region Public  Methods

        public bool CalculateProgram(string fileName)
        {
            bool bSuccess = false;
            filePath = Path.GetDirectoryName(fileName);
            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));
            string design_file = Path.GetDirectoryName(fileName);
            design_file = Path.Combine(design_file, "DESIGN.FIL");

            StreamWriter filFile = new StreamWriter(new FileStream(design_file, FileMode.Create));

            try
            {
                // Step 1
                //eH.depth = 
                totalLength = l * 1000 + b1 + b2;

                double d = (l * 1000 + ((b1 + b2) / 2)) / (20 * 1.5); // eH_depth

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 22            *");
                sw.WriteLine("\t\t*       TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*           DESIGN OF SINGLE SPAN            *");
                sw.WriteLine("\t\t*  ONE WAY RCC SLAB BY WORKING STRESS METHOD *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("■ Step 1:");
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Eff_Depth = (({0} + {1})/20 * 1.5) = {2} mm.", (l * 1000.0d).ToString("0.0"), ((b1 + b2) / 2).ToString("0.0"), d.ToString("0.00"));
                d = d / 10.0d;
                d = (double)((int)d) * 10;
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Let us Provide eff. depth = {0} mm.", d);
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Using {0}Ø rod and providing a clear cover of {1} mm.", d1, h1);
                D = d + h1 + (d1 / 2.0d);
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Provide overall depth = {0} + {1} = {2} mm.", d, (h1 + (d1 / 2)).ToString("0.00"), D);
                //Step 2
                // EH_Span should be the less of the followings
                l = l * 1000;
                double EH_Span = l + (b1 / 2.0d) + (b2 / 2.0d);

                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("■ Step 2:");
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Eff Span should be the less of the following :");
                double l1 = EH_Span;
                double l2 = l + d;
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("(i) c/c distance between the support = {0} mm.", l1);
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("(ii) Clear Span + eff. depth of slab = {0} + {1} = {2} mm.", l, d, l2);
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("▲ Provide effective span l = {0} mm.", ((l1 > l2) ? l2 : l1));
                //sw.WriteLine("--------------------------------------------------------------------");
                // Step 3
                // Load calculation Considering 1 m width of the Slab.
                // Dead Load of the Slab
                dl = (D / 1000.0d) * gamma_c;

                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("■ Step 3:");
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Load Calculation Considering 1 m width of the slab.");

                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Dead Load of the Slab = {0} * {1} = {2} N/m.", (D / 1000.0).ToString("0.00"), gamma_c, dl);
                sw.WriteLine("Floor Finish + Ceiling plaster    = {0} N/m.", w1);
                sw.WriteLine("Live Load                         = {0} N/m.", w2);


                double w = dl + w1 + w2;

                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("                    Total Load  w = {0} N/m.", w);


                // Step 4
                //Design Moment M = w*l*l/8
                l = l / 1000.0d;
                double M = w * l * l / 8;
                double eff_depth_reqd = Math.Sqrt((M * 1000.0d) / (0.848 * 1000.0d));
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("■ Step 4:");
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Design moment M = (w*l*l/8) = {0} N-m.", M.ToString("0.00"));
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Now eff depth reqd. = √(M/Qb) = √({0}/({1}) = {2} mm. ", (M * 1000.0d).ToString("0.00"), (0.848 * 1000.0d).ToString("0.000"), eff_depth_reqd.ToString("0.00"));

                if (eff_depth_reqd < 90)
                {
                    sw.WriteLine("So, the eff depth provided is OK.({0} mm < 90 mm)", eff_depth_reqd.ToString("0.00"));
                }
                else
                    sw.WriteLine("So, the eff depth provided is not OK. ({0} mm > 90 mm)", eff_depth_reqd.ToString("0.00"));



                // Step 5
                // Required Ast
                double Ast = ((M * 1000.0d) / (sigma_st * 0.87 * d));
                double a_st = (Math.PI * (d1 * d1) / 4.0d);
                double no_rod = Ast / a_st;
                s1 = (1000.0 * a_st) / Ast; // Spacing
                int iS1 = (int)(s1 / 10);

                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("■ Step 5:");
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Required Ast = {0} sq.mm.", eff_depth_reqd);
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("No of rod = ({0} / {1} = (Providing {2}Ø rod, c/s area of rod = {1} sq.mm.", Ast.ToString("0.00"), a_st.ToString("0.00"), d1);
                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Spacing = ({0} * {1}) / {2} = {3} mm.", 1000, a_st.ToString("0.00"), Ast.ToString("0.00"), s1.ToString("0.00"));
                s1 = iS1 * 10.0d;

                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("So we shall provide {0}Ø rod @ {1} mm c/c.", d1, s1);

                if (s1 < 450 && s1 < 3 * d)
                {
                    //sw.WriteLine("--------------------------------------------------------------------");
                    sw.WriteLine("So, the eff depth provided is OK.({0} mm < 90 mm)", eff_depth_reqd.ToString("0.00"));
                }
                else
                {
                    //sw.WriteLine("--------------------------------------------------------------------");
                    sw.WriteLine("So, the eff depth provided is not OK. ({0} mm > 90 mm)", eff_depth_reqd.ToString("0.00"));
                }



                //Step 6
                dst = (dst * 1000.0 * D / 100.0);
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("■ Step 6:");
                sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Area of distribution steel = {0} sq.mm.", dst.ToString("0.00"));

                double rod_Spacing = ((Math.PI * (d2 * d2) / 4));
                rod_Spacing = rod_Spacing * 1000 / Dst;


                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("Using {0}Ø rod spacing = {1} c/c", d2, rod_Spacing.ToString("0.00"));

                iS1 = (int)rod_Spacing / 10;
                rod_Spacing = iS1 * 10.0d;
                s2 = rod_Spacing;

                //sw.WriteLine("--------------------------------------------------------------------");
                sw.WriteLine("So we provide distribution steel @ {0} mm c/c.", rod_Spacing.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("****************************************************************");
                sw.WriteLine("   *****************       END OF REPORT       *************");
                sw.WriteLine("****************************************************************");
                bSuccess = true;


                //DrawSlab01((,1000,D,180,170));

                #region With ID code
                //filFile.WriteLine("SLAB DESIGN");
                //filFile.WriteLine("100 L = {0} mm; B = {1} m; D = {2} mm", (l * 1000 + b1 + b2).ToString("0.00"), 1000, D.ToString("0.00"));
                //filFile.WriteLine("101 h1 = {0} mm; h2 = {1} mm", h1.ToString("0.00"), h1.ToString("0.00"));
                //filFile.WriteLine("102 d1 = {0} mm; d2 = {1} mm", d1.ToString("0.00"), d2.ToString("0.00"));
                //filFile.WriteLine("103 Gamma_C = {0} mm; Sigma_St = {1} mm", d1.ToString("0.00"), d2.ToString("0.00"));
                //filFile.WriteLine("104 Gc = {0} mm; l = {1} mm", Gc.ToString("0.00"), l.ToString("0.00"));
                //filFile.WriteLine("105 b1 = {0} mm; b2 = {1} mm", b1.ToString("0.00"), l.ToString("0.00"));
                //filFile.WriteLine("ENFORCEMENT BAR");
                //filFile.WriteLine("B1 = {0} mm; MAIN SPACING = {1} mm; DIST SPACING = {2}", b1.ToString("0.00"), s1.ToString("0.00"), s2.ToString("0.00"));
                //filFile.WriteLine("END");
                #endregion

                filFile.WriteLine("SLAB DESIGN");
                filFile.WriteLine("L = {0} mm; B = {1} m; D = {2} mm", (l * 1000 + b1 + b2).ToString("0.00"), 1000, D.ToString("0.00"));
                filFile.WriteLine("h1 = {0} mm; h2 = {1} mm", h1.ToString("0.00"), h1.ToString("0.00"));
                //filFile.WriteLine("d1 = {0} mm; d2 = {1} mm", d1.ToString("0.00"), d2.ToString("0.00"));
                //filFile.WriteLine("Gamma_C = {0} mm; Sigma_St = {1} mm", d1.ToString("0.00"), d2.ToString("0.00"));
                //filFile.WriteLine("Gc = {0} mm; l = {1} mm", Gc.ToString("0.00"), l.ToString("0.00"));
                //filFile.WriteLine("b1 = {0} mm; b2 = {1} mm", b1.ToString("0.00"), l.ToString("0.00"));
                filFile.WriteLine("B1 = {0} mm; MAIN SPACING = {1} mm; DIST SPACING = {2}", b1.ToString("0.00"), s1.ToString("0.00"), s2.ToString("0.00"));
                filFile.WriteLine("END");
                filFile.Flush();
                filFile.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                bSuccess = false;
            }
            sw.Flush();
            sw.Close();
            return bSuccess;
        }

        #endregion
    }
}
