using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LimitStateMethod.PSC_I_Girder
{
    public class PSC_I_Girder
    {
        public PSC_I_Girder()
        {

        }

        public void Calculate_Program()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("Distance between C/C Exp. Joint  = jdskw = 37.50 m (Skew)   "));
            list.Add(string.Format("Distance between C/C Exp. Joint  = jdsq = 26.52 m (Square)   "));
            list.Add(string.Format("Effective span C/C bearing   Leff = eslsk = 34.30 m (Skew)   "));
            list.Add(string.Format("Effective span c/C bearing   Leff = eslsw  = 24.25 m (Square)   "));
            list.Add(string.Format("Overhang of girder off the bearing = ovhg = 1 m"));
            list.Add(string.Format("Overhang of slab off the bearing = ovhg = 1.600 m "));
            list.Add(string.Format("Expansion Joint = exp = 40 mm "));
            list.Add(string.Format("Deck Width = wd = 12.0 m "));
            list.Add(string.Format("Deck Width (Skew)  = wds = 16.97 m "));
            list.Add(string.Format("Angle of skew = Ang = 45.00 deg. "));
            list.Add(string.Format("Clear carriage way = Bcw = 11.00 m "));
            list.Add(string.Format("Width of outer railing = wor = 0.000 m "));
            list.Add(string.Format("Width of Footpath  = wor  = 0.000 m "));
            list.Add(string.Format("Width of Crash Barrier =  Wkerb = 1 mm "));
            list.Add(string.Format("Spacing of main girder c/c =  Spmg = 3.000 m (Square)   "));
            list.Add(string.Format("Spacing of main girder c/c =  Spmg = 4.243 m (Skew)   "));
            list.Add(string.Format("Thk of deck slab = Df = 200 mm "));
            list.Add(string.Format("Thk of deck slab at overhang  = tso  = 400 mm "));
            list.Add(string.Format("Thk of wearing coat =  Wc = 75 mm "));
            list.Add(string.Format("Length of cantilever =  Lcan = 1.50 m "));
            list.Add(string.Format("Cantilever slab thk at fixed end =  Dcan1 = 200 mm "));
            list.Add(string.Format("Cantilever slab thk at free end =  Dcan2 = 200 mm "));
            list.Add(string.Format("No of main girder = Nomg = 4.0  "));
            list.Add(string.Format("Depth of main girder  = Dmg = 2400 mm "));
            list.Add(string.Format("Flange width of girder = bf = 1000 mm "));
            list.Add(string.Format("Web thk of girder at mid span =    = 0.30 m "));
            list.Add(string.Format("Web thk of girder at Support    = 0.65 m "));
            list.Add(string.Format("Thickness of top flange    = 150 mm "));
            list.Add(string.Format("Thickness of top haunch    = 75 mm "));
            list.Add(string.Format("Bottom width of flange    = 650 mm "));
            list.Add(string.Format("Thickness of bottom flange    = 250 mm "));
            list.Add(string.Format("Thickness of bottom haunch   Bbw = 200 mm "));
            list.Add(string.Format("Length of varying portion   Lwv = 2.50 m "));
            list.Add(string.Format("Length of solid portion   Lwu = 2.50 m "));
            list.Add(string.Format("No of Intermediate cross girder   Nocg = 1.00  "));
            list.Add(string.Format("Depth of Int. cross girder    = 2350 mm "));
            list.Add(string.Format("Depth of End. cross girder    = 2350 mm "));
            list.Add(string.Format("Web thk of Intermediate cross girder    bwcg = 300 mm "));
            list.Add(string.Format("Web thk of end cross girder     = 600 mm "));
            list.Add(string.Format("Grade of concrete for precast Girder  = M 45 Mpa "));
            list.Add(string.Format("Grade of concrete of other componant      Cgrade = M 40 Mpa "));
            list.Add(string.Format("Grade of reinforcement   Sgrade = Fe 500 Mpa "));
            list.Add(string.Format(" Shape Factor ?   ? = 0.80  "));
            list.Add(string.Format(" strength Factor h     h  = 1.00  "));
            list.Add(string.Format("Partial factor of safety  (Basic and seismic)   ?c = 1.50  "));
            list.Add(string.Format("Partial factor of safety Accidental    ?c = 1.20  "));
            list.Add(string.Format("Coefficient to consider the influence of the strength   a = 0.67  "));
            list.Add(string.Format("Design value of concrete comp strength for Basic and seismic   fcd = 17.87 Mpa "));
            list.Add(string.Format("Design value of concrete comp strength for Accidental   fcd = 17.87 Mpa "));
            list.Add(string.Format("Clear cover   cov = 40.0 mm "));
            list.Add(string.Format("Unit weight of dry concrete   wcon = 2.40 t/m3 "));
            list.Add(string.Format("Unit weight of wet concrete    = 2.60 t/m3 "));
            list.Add(string.Format("Weight of wearing course   wwc = 0.20 t/m2 "));
            list.Add(string.Format("Weight of Crash Barrier   wrail = 1.00 t/m "));
            list.Add(string.Format("Weight of Railing    = 0.30 t/m "));
            list.Add(string.Format("Intensity of Load for shuttering    = 0.50 t/m "));
            list.Add(string.Format("Stress in concrete (compression)   fck = 40 Mpa "));
            list.Add(string.Format("Stress in steel (tension)   fyk = 500 Mpa "));
            list.Add(string.Format("Partial factor of safety for basic and seismic   gs = 1.15  IRC-112 Fig 6.3 (Note) "));
            list.Add(string.Format("Partial factor of safety for Accidental   gs = 1.00  IRC-112 Fig 6.3 (Note) "));
            list.Add(string.Format("Ecm    = 33000 Mpa "));
            list.Add(string.Format("Es    = 200000 Mpa "));
            list.Add(string.Format("Modular ratio   m = 6.061  "));
            list.Add(string.Format("Anchorage from c/L of bearing    = 1.050 m (Min. 1/2 of end X-girder Thk.)   "));
            list.Add(string.Format("   "));
            list.Add(string.Format("Length of Girder beyond c/L of Bearing    = 0.900 m "));
            list.Add(string.Format("Distance of Jack from c/L of Bearing    = 0.900 m "));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
        }
    }



    public class MidSection
    {
        public double b1, b2, b3, b4, b5, b6, b7, b8;
        public double d1, d2, d3, d4, d5, d6, d7, d8, d9;


        public List<string> Results { get; set; }

        public double Emod_Deck { get; set; }
        public double Emod_Girder { get; set; }
        public double Emod_Ratio
        {
            get
            {
                return Emod_Deck / Emod_Girder;
            }
        }
        public MidSection()
        {

            b1 = b2 = b3 = b4 = b5 = b6 = b7 = b8 = 0.0;
            d1 = d2 = d3 = d4 = d5 = d6 = d7 = d8 = d9 = 0.0;
            Emod_Deck = 32500.0;
            Emod_Girder = 33500.0;

            Results = new List<string>();
        }
        public double A1
        {
            get
            {
                double area = b1 * d1 / 1000000 * Emod_Ratio;

                return area;
            }
        }
        public double A2
        {
            get
            {
                double area = b2 * d2 / 1000000;

                return area;
            }
        }
        public double A3
        {
            get
            {
                double area = ((b2 - b5) * d3 * 0.5) / 1000000;

                return area;
            }
        }
        public double A4
        {
            get
            {
                double area = (b5 * d3) / 1000000;

                return area;
            }
        }
        public double A5
        {
            get
            {
                double area = (b5 * d5) / 1000000;

                return area;
            }
        }
        public double A6
        {
            get
            {

                double area = ((b8 - b5) * d6 * 0.5) / 1000000;

                return area;
            }
        }
        public double A7
        {
            get
            {
                double area = (b6 * d6) / 1000000;

                return area;
            }
        }
        public double A8
        {
            get
            {
                double area = (b8 * d8) / 1000000;

                return area;
            }
        }
        public double A9
        {
            get
            {
                double area = d9 * b1 * 0.5 / 1000000;

                return area;
            }
        }
        public double Composite_Area_Sum
        {
            get
            {
                double area = A1 + A2 + A3 + A4 + A5 + A6 + A7 + A8 + A9;

                return area;
            }
        }

        public double Girder_Area_Sum
        {
            get
            {
                double area = A2 + A3 + A4 + A5 + A6 + A7 + A8 + A9;

                return area;
            }
        }

        public double Y1
        {
            get
            {
                return (d1 / 2) / 1000.0;
            }

        }

        public double Y2
        {
            get
            {
                return (d2 / 2.0 + d1) / 1000.0;
            }

        }
        public double Y3
        {
            get
            {
                return (d1 + d2 + d3 / 3) / 1000.0;
            }

        }
        public double Y4
        {
            get
            {
                return (d1 + d2 + d3 / 2) / 1000.0;
            }

        }
        public double Y5
        {
            get
            {
                return (d1 + d2 + d5 / 2) / 1000.0;
            }
        }

        public double Y6
        {
            get
            {
                return (d1 + d2 + d3 + d5 + (d6 * 2.0 / 3.0)) / 1000.0;
            }
        }

        public double Y7
        {
            get
            {
                return (d1 + d2 + d3 + d5 + (d6 / 2.0)) / 1000.0;
            }
        }

        public double Y8
        {
            get
            {
                return (d1 + d2 + d3 + d5 + d6 + (d8 / 2.0)) / 1000.0;
            }
        }

        public double Composite_YTop;
        public double Girder_YTop;
        public double AY1, AY2, AY3, AY4, AY5, AY6, AY7, AY8;
        public double Composite_YTop2;
        public double I1, I2, I3, I4, I5, I6, I7, I8;
        public double AYYC1, AYYC2, AYYC3, AYYC4, AYYC5, AYYC6, AYYC7, AYYC8;
        public double AYYG1, AYYG2, AYYG3, AYYG4, AYYG5, AYYG6, AYYG7, AYYG8;
        public double IG, IG1, IG2, IG3, IG4, IG5, IG6, IG7, IG8;
        public double IC, IC1, IC2, IC3, IC4, IC5, IC6, IC7, IC8;
        double Composite_ZTop, Composite_YBot, Composite_ZBot;

        public void Calculate(string step_no)
        {
            List<string> list = new List<string>();

            b3 = b2 - b5;
            d3 = d4;

            b6 = b8 - b5;
            b7 = b5;

            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.1 : SECTION DIMENTION DATA", step_no));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Element [1]   b1 = {0} mm, d1 = {1} mm", b1, d1));
            list.Add(string.Format("Element [2]   b2 = {0} mm, d2 = {1} mm", b2, d2));
            list.Add(string.Format("Element [3]   b3 = b2 - b5 = {0} - {1} = {2} mm, d3 = d4 = {3} mm", b2, b5, b3, d3));
            list.Add(string.Format("Element [4]   b4 = {0} mm, d4 = {1} mm", b4, d4));
            list.Add(string.Format("Element [5]   b5 = {0} mm, d5 = {1} mm", b5, d5));
            list.Add(string.Format("Element [6]   b6 = b8 - b5 = {0} - {1} = {2} mm, d6 = {3} mm", b8, b5, b6, d6));
            list.Add(string.Format("Element [7]   b7 = b5 = {0} mm, d7 = {1} mm", b7, d7));
            list.Add(string.Format("Element [8]   b8 = {0} mm, d8 = {1} mm", b8, d8));
            list.Add(string.Format(""));
            list.Add(string.Format("Element [9]   d9 = {0} mm", d9));
            list.Add(string.Format(""));
            list.Add(string.Format("Modulus of Elasticity of Deck = Emod_Deck = {0:f3}", Emod_Deck));
            list.Add(string.Format("Modulus of Elasticity of Girder = Emod_Girder = {0:f3}", Emod_Girder));

            //double Emod_ratio = Emod_Deck / Emod_Girder;
            list.Add(string.Format("Emod_ratio = Emod_Deck / Emod_Girder = {0:f3} / {1:f3} = {2:f5}", Emod_Deck, Emod_Girder, Emod_Ratio));




            //list.Add(string.Format("Calculation for Section Properties of Outer Girder at Mid Span"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.2 : AREA of each section Element", step_no));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Part of Deck Slab = A1 = (b1 * d1 / 10^6) * Emod_Ratio"));
            list.Add(string.Format("                          = ({0} * {1} / 10^6) * {2:f4}", b1, d1, Emod_Ratio));
            list.Add(string.Format("                          = {0:f4} Sq.m", A1));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Top Flange = A2 = (b2 * d2 / 10^6)"));
            list.Add(string.Format("                   = ({0} * {1} / 10^6)", b2, d2));
            list.Add(string.Format("                   = {0:f4} Sq.m", A2));
            list.Add(string.Format(""));
            list.Add(string.Format("3. Top Haunch Both Triangles = A3 = (((b2 - b5) * d3 * 0.5) / 10^6)"));
            list.Add(string.Format("                                  = ((({0} - {1}) * {2} * 0.5) / 10^6)", b2, b5, d3));
            list.Add(string.Format("                                  = {0:f4} Sq.m", A3));
            list.Add(string.Format(""));
            list.Add(string.Format("4. Top Haunch middle rectangle = A4 = (b5 * d3 / 10^6)"));
            list.Add(string.Format("                                    = ({0} * {1} / 10^6)", b5, d3));
            list.Add(string.Format("                                    = {0:f4} Sq.m", A4));
            list.Add(string.Format(""));
            list.Add(string.Format("5. Stem of Girder = A5 = (b5 * d5 / 10^6)"));
            list.Add(string.Format("                       = ({0} * {1} / 10^6)", b5, d5));
            list.Add(string.Format("                       = {0:f4} Sq.m", A5));
            list.Add(string.Format(""));
            list.Add(string.Format("6. Bottom Haunch both Triangles = A6 = (((b8 - b5) * d6 * 0.5) / 10^6)"));
            list.Add(string.Format("                                     = ((({0} - {1}) * {2} * 0.5) / 10^6)", b8, b5, d6));
            list.Add(string.Format("                                     = {0:f4} Sq.m", A6));

            list.Add(string.Format(""));
            list.Add(string.Format("7. Bottom Haunch middle rectangles = A7 = (b5 * d6 / 10^6)"));
            list.Add(string.Format("                                        = ({0} * {1} / 10^6)", b5, d6));
            list.Add(string.Format("                                        = {0:f4} Sq.m", A7));

            list.Add(string.Format(""));
            list.Add(string.Format("8. Bottom Flange = A8 = (b8 * d8 / 10^6)"));
            list.Add(string.Format("                      = ({0} * {1} / 10^6)", b8, d8));
            list.Add(string.Format("                      = {0:f4} Sq.m", A8));
            list.Add(string.Format(""));
            list.Add(string.Format("9. Trianglar Deck = A9 = (b1 * d9 * 0.5 / 10^6)"));
            list.Add(string.Format("                       = ({0} * {1} * 0.5 / 10^6)", b1, d9));
            list.Add(string.Format("                      = {0:f4} Sq.m", A9));


            list.Add(string.Format(""));
            list.Add(string.Format("Composite Area Sum = A1 + A2 + A3 + A4 + A5 + A6 + A7 + A8 + A9"));
            list.Add(string.Format("                   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3} + {7:f3} + {8:f3}",
                A1, A2, A3, A4, A5, A6, A7, A8, A9));
            list.Add(string.Format("                   = {0:f3} Sq.m", Composite_Area_Sum));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("Girder Area Sum = A2 + A3 + A4 + A5 + A6 + A7 + A8"));
            list.Add(string.Format("                = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                A2, A3, A4, A5, A6, A7, A8));
            list.Add(string.Format("                = {0:f3} Sq.m", Girder_Area_Sum));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.3 : Y-from Top at each Cross Section Elements", step_no));
            list.Add(string.Format("-----------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Y1 = d1 / 2 = {0} / 2 = {1:f3} m", d1, Y1));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Y2 = (d1 + d2 / 2) = ({0} + {1} / 2) = {2:f3} m", d1, d2, Y2));
            list.Add(string.Format(""));
            list.Add(string.Format("3. Y3 = (d1 + d2 + d3 / 3) = ({0} + {1} + {2} / 3) = {3:f3} m", d1, d2, d3, Y3));
            list.Add(string.Format(""));
            list.Add(string.Format("4. Y4 = (d1 + d2 + d3 / 2) = ({0} + {1} + {2} / 2) = {3:f3} m", d1, d2, d3, Y4));
            list.Add(string.Format(""));
            list.Add(string.Format("5. Y5 = (d1 + d2 + d5 / 2) = ({0} + {1} + {2} / 2) = {3:f3} m", d1, d2, d5, Y5));
            list.Add(string.Format(""));
            list.Add(string.Format("6. Y6 = (d1 + d2 + d3 + d5 + (d6 * 2 / 3)) = ({0} + {1} + {2} + {3} + ({4} * 2 / 3)) = {5:f3} m", d1, d2, d3, d5, d6, Y6));
            list.Add(string.Format(""));
            list.Add(string.Format("7. Y7 = (d1 + d2 + d3 + d5 + (d6 / 2)) = ({0} + {1} + {2} + {3} + ({4} / 2)) = {5:f3} m", d1, d2, d3, d5, d6, Y7));
            list.Add(string.Format(""));
            list.Add(string.Format("8. Y8 = (d1 + d2 + d3 + d5 + d6 + (d8 / 2)) = ({0} + {1} + {2} + {3} + {4} + ({5} / 2)) = {6:f3} m", d1, d2, d3, d5, d6, d8, Y8));
            list.Add(string.Format(""));
            Composite_YTop = Y1 + Y2 + Y3 + Y4 + Y5 + Y6 + Y7 + Y8;

            list.Add(string.Format("Composite_Top = Y1 + Y2 + Y3 + Y4 + Y5 + Y6 + Y7 + Y8"));
            list.Add(string.Format("              = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3} + {7:f3}",
                Y1, Y2, Y3, Y4, Y5, Y6, Y7, Y8));
            list.Add(string.Format("              = {0:f3} m", Composite_YTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.4 : A x Y for Each Cross Section Element", step_no));
            list.Add(string.Format("-----------------------------------------------------"));
            list.Add(string.Format(""));

            AY1 = A1 * Y1;
            list.Add(string.Format("1. AY1 = A1 x Y1 = {0:f3} x {1:f3} = {2:f3}", A1, Y1, AY1));

            AY2 = A2 * Y2;
            list.Add(string.Format("2. AY2 = A2 x Y2 = {0:f3} x {1:f3} = {2:f3}", A2, Y2, AY2));

            AY3 = A3 * Y3;
            list.Add(string.Format("3. AY3 = A3 x Y3 = {0:f3} x {1:f3} = {2:f3}", A3, Y3, AY3));

            AY4 = A4 * Y4;
            list.Add(string.Format("4. AY4 = A4 x Y4 = {0:f3} x {1:f3} = {2:f3}", A4, Y4, AY4));

            AY5 = A5 * Y5;
            list.Add(string.Format("5. AY5 = A5 x Y5 = {0:f3} x {1:f3} = {2:f3}", A5, Y5, AY5));

            AY6 = A6 * Y6;
            list.Add(string.Format("6. AY6 = A6 x Y6 = {0:f3} x {1:f3} = {2:f3}", A6, Y6, AY6));

            AY7 = A7 * Y7;
            list.Add(string.Format("7. AY7 = A7 x Y7 = {0:f3} x {1:f3} = {2:f3}", A7, Y7, AY7));

            AY8 = A8 * Y8;
            list.Add(string.Format("8. AY8 = A8 x Y8 = {0:f3} x {1:f3} = {2:f3}", A8, Y8, AY8));

            list.Add(string.Format(""));
            Composite_YTop2 = (AY1 + AY2 + AY3 + AY4 + AY5 + AY6 + AY7 + AY8) / Composite_Area_Sum;
            list.Add(string.Format(""));
            list.Add(string.Format("Composite_YTop2 = (AY1 + AY2 + AY3 + AY4 + AY5 + AY6 + AY7 + AY8) / Composite_Area_Sum"));
            list.Add(string.Format("                = ({0:f3} / {1:f3})",
                (AY1 + AY2 + AY3 + AY4 + AY5 + AY6 + AY7 + AY8), Composite_Area_Sum));
            list.Add(string.Format(""));
            list.Add(string.Format("                = {0:f3}", Composite_YTop2));
            list.Add(string.Format(""));

            Girder_YTop = ((AY2 + AY3 + AY4 + AY5 + AY6 + AY7 + AY8) / Girder_Area_Sum) - (d1 / 1000);
            list.Add(string.Format(""));
            list.Add(string.Format("Girder_YTop = (AY2 + AY3 + AY4 + AY5 + AY6 + AY7 + AY8) / Composite_Area_Sum - d1"));
            list.Add(string.Format("            = ({0:f3} / {1:f3}) - {2:f3}",
                (AY2 + AY3 + AY4 + AY5 + AY6 + AY7 + AY8), Girder_Area_Sum, (d1 / 1000)));
            list.Add(string.Format(""));
            list.Add(string.Format("            = {0:f3}", Girder_YTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.5 : Moment of Inertia = ISelf for each section", step_no));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            I1 = ((b1 * Math.Pow(d1, 3) / 12.0) / Math.Pow(10, 12)) * Emod_Ratio;
            list.Add(string.Format("1. I1 = ((b1 * d1^3 / 12) / 10^12) * Emod_Ratio"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12) / 10^12) * {2:f4}", b1, d1, Emod_Ratio));
            list.Add(string.Format("      = {0:f6} m^4", I1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I2 = ((b2 * Math.Pow(d2, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("2. I2 = ((b2 * d2^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12) / 10^12)", b2, d2));
            list.Add(string.Format("      = {0:f6} m^4", I2));
            list.Add(string.Format(""));

            I3 = (((b2 - b5) * Math.Pow(d3, 3) / 36.0) / Math.Pow(10, 12));
            list.Add(string.Format("3. I3 = (((b2 - b5) * d3^3 / 12) / 10^12)"));
            list.Add(string.Format("      = ((({0} - {1}) * {2}^3 / 12)  / 10^12)", b2, b5, d3, Emod_Ratio));
            list.Add(string.Format("      = {0:f6} m^4", I3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I4 = ((b5 * Math.Pow(d3, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("4. I4 = ((b5 * d3^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12) ", b5, d3));
            list.Add(string.Format("      = {0:f6} m^4", I4));
            list.Add(string.Format(""));

            I5 = ((b5 * Math.Pow(d5, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("5. I5 = ((b5 * d5^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12)", b5, d5));
            list.Add(string.Format("      = {0:f6} m^4", I5));
            list.Add(string.Format(""));


            I6 = (((b8 - b5) * Math.Pow(d6, 3) / 36.0) / Math.Pow(10, 12));
            list.Add(string.Format("6. I6 = (((b8 - b5) * d6^3 / 12) / 10^12) "));
            list.Add(string.Format("      = ((({0} - {1}) * {2}^3 / 12)  / 10^12)", b8, b5, d6));
            list.Add(string.Format("      = {0:f6} m^4", I6));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I7 = ((b5 * Math.Pow(d6, 3) / 12.0) / Math.Pow(10, 12)) * Emod_Ratio;
            list.Add(string.Format("7. I7 = ((b5 * d6^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12)", b5, d6));
            list.Add(string.Format("      = {0:f6} m^4", I7));
            list.Add(string.Format(""));

            I8 = ((b8 * Math.Pow(d8, 3) / 12.0) / Math.Pow(10, 12)) * Emod_Ratio;
            list.Add(string.Format("8. I8 = ((b8 * d8^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12)", b8, d8));
            list.Add(string.Format("      = {0:f6} m^4", I8));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.6 : AY^2 for Composite Section (Girder + Deck Slab)", step_no));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));

            AYYC1 = A1 * Math.Pow((Y1 - Composite_YTop2), 2);
            list.Add(string.Format("1. AYYC1 = A1 * (Y1 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A1, Y1, Composite_YTop2, AYYC1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC2 = A2 * Math.Pow((Y2 - Composite_YTop2), 2);
            list.Add(string.Format("2. AYYC2 = A2 * (Y2 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A2, Y2, Composite_YTop2, AYYC2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC3 = A3 * Math.Pow((Y3 - Composite_YTop2), 2);
            list.Add(string.Format("3. AYYC3 = A3 * (Y3 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A3, Y3, Composite_YTop2, AYYC3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC4 = A4 * Math.Pow((Y4 - Composite_YTop2), 2);
            list.Add(string.Format("4. AYYC4 = A4 * (Y4 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A4, Y4, Composite_YTop2, AYYC4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC5 = A5 * Math.Pow((Y5 - Composite_YTop2), 2);
            list.Add(string.Format("5. AYYC5 = A5 * (Y5 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A5, Y5, Composite_YTop2, AYYC5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC6 = A6 * Math.Pow((Y6 - Composite_YTop2), 2);
            list.Add(string.Format("6. AYYC6 = A6 * (Y6 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A6, Y6, Composite_YTop2, AYYC6));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC7 = A7 * Math.Pow((Y7 - Composite_YTop2), 2);
            list.Add(string.Format("7. AYYC7 = A7 * (Y7 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A7, Y7, Composite_YTop2, AYYC7));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC8 = A8 * Math.Pow((Y8 - Composite_YTop2), 2);
            list.Add(string.Format("8. AYYC8 = A8 * (Y8 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A8, Y8, Composite_YTop2, AYYC8));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("AY^2 for Girder Section"));
            list.Add(string.Format("-----------------------"));
            //Girder_t


            AYYG1 = 0;
            list.Add(string.Format(""));
            list.Add(string.Format("1.  AYYG1 = ----------"));
            list.Add(string.Format(""));

            AYYG2 = A2 * Math.Pow((Y2 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("2.  AYYG2 = A2 * (Y2 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A2, Y2, Girder_YTop, d1, AYYG2));
            list.Add(string.Format(""));

            AYYG3 = A3 * Math.Pow((Y3 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("3.  AYYG3 = A3 * (Y3 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A3, Y3, Girder_YTop, d1, AYYG2));
            list.Add(string.Format(""));

            AYYG4 = A4 * Math.Pow((Y4 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("4.  AYYG4 = A4 * (Y4 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A4, Y4, Girder_YTop, d1, AYYG4));
            list.Add(string.Format(""));

            AYYG5 = A5 * Math.Pow((Y5 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("2.  AYYG2 = A2 * (Y2 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A5, Y5, Girder_YTop, d1, AYYG5));
            list.Add(string.Format(""));

            AYYG6 = A6 * Math.Pow((Y6 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("6.  AYYG6 = A6 * (Y6 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A6, Y6, Girder_YTop, d1, AYYG6));
            list.Add(string.Format(""));

            AYYG7 = A7 * Math.Pow((Y7 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("7.  AYYG7 = A7 * (Y7 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A7, Y7, Girder_YTop, d1, AYYG7));
            list.Add(string.Format(""));

            AYYG8 = A8 * Math.Pow((Y8 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("8.  AYYG8 = A8 * (Y8 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A8, Y8, Girder_YTop, d1, AYYG8));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("I-Girder"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));
            IG1 = 0;
            list.Add(string.Format("1. IG1 = I1 + AYYG1 = -------------"));
            list.Add(string.Format(""));

            IG2 = I2 + AYYG2;
            list.Add(string.Format("2. IG2 = I2 + AYYG2 = {0:f5} + {1:f5} = {2:f5} ", I2, AYYG2, IG2));
            list.Add(string.Format(""));


            IG3 = I3 + AYYG3;
            list.Add(string.Format("3. IG3 = I3 + AYYG3 = {0:f5} + {1:f5} = {2:f5} ", I3, AYYG3, IG3));
            list.Add(string.Format(""));


            IG4 = I4 + AYYG4;
            list.Add(string.Format("4. IG4 = I4 + AYYG4 = {0:f5} + {1:f5} = {2:f5} ", I4, AYYG4, IG4));
            list.Add(string.Format(""));


            IG5 = I5 + AYYG5;
            list.Add(string.Format("5. IG5 = I5 + AYYG5 = {0:f5} + {1:f5} = {2:f5} ", I5, AYYG5, IG5));
            list.Add(string.Format(""));


            IG6 = I6 + AYYG6;
            list.Add(string.Format("6. IG6 = I6 + AYYG6 = {0:f5} + {1:f5} = {2:f5} ", I6, AYYG6, IG6));
            list.Add(string.Format(""));


            IG7 = I7 + AYYG7;
            list.Add(string.Format("7. IG7 = I7 + AYYG7 = {0:f5} + {1:f5} = {2:f5} ", I7, AYYG7, IG7));
            list.Add(string.Format(""));


            IG8 = I8 + AYYG8;
            list.Add(string.Format("8. IG8 = I8 + AYYG8 = {0:f5} + {1:f5} = {2:f5} ", I8, AYYG8, IG8));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("I-Composite"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            IC1 = I1 + AYYC1;
            list.Add(string.Format("1. IC1 = I1 + AYYC1 = {0:f5} + {1:f5} = {2:f5} ", IC1, AYYC1, IC1));
            list.Add(string.Format(""));

            IC2 = I2 + AYYC2;
            list.Add(string.Format("2. IC2 = I2 + AYYC2 = {0:f5} + {1:f5} = {2:f5} ", IC2, AYYC2, IC2));
            list.Add(string.Format(""));

            IC3 = I3 + AYYC3;
            list.Add(string.Format("3. IC3 = I3 + AYYC3 = {0:f5} + {1:f5} = {2:f5} ", IC3, AYYC3, IC3));
            list.Add(string.Format(""));

            IC4 = I4 + AYYC4;
            list.Add(string.Format("4. IC4 = I4 + AYYC4 = {0:f5} + {1:f5} = {2:f5} ", IC4, AYYC4, IC4));
            list.Add(string.Format(""));

            IC5 = I5 + AYYC5;
            list.Add(string.Format("5. IC5 = I5 + AYYC5 = {0:f5} + {1:f5} = {2:f5} ", IC5, AYYC5, IC5));
            list.Add(string.Format(""));

            IC6 = I6 + AYYC6;
            list.Add(string.Format("6. IC6 = I6 + AYYC6 = {0:f5} + {1:f5} = {2:f5} ", IC6, AYYC6, IC6));
            list.Add(string.Format(""));

            IC7 = I7 + AYYC7;
            list.Add(string.Format("7. IC7 = I7 + AYYC7 = {0:f5} + {1:f5} = {2:f5} ", IC7, AYYC7, IC7));
            list.Add(string.Format(""));

            IC8 = I8 + AYYC8;
            list.Add(string.Format("2. IC2 = I2 + AYYC2 = {0:f5} + {1:f5} = {2:f5} ", IC8, AYYC8, IC8));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE {0}.1 :SECTION PROPERTY TABLE", step_no));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Element    Area    Y(from top)    A.Y        ISelf      AY^2        AY^2          I          I"));
            list.Add(string.Format("          (Sq.m)      (m)      (Sq.Sq.m)   (Sq.Sq.m)   Composite   Girder      Composite   Girder"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                1, A1, Y1, AY1, I1, AYYC1, AYYG1, IC1, IG1));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                2, A2, Y2, AY2, I2, AYYC2, AYYG2, IC2, IG2));

            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                3, A3, Y3, AY3, I3, AYYC3, AYYG3, IC3, IG3));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                4, A4, Y4, AY4, I4, AYYC4, AYYG4, IC4, IG4));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                5, A5, Y5, AY5, I5, AYYC5, AYYG5, IC5, IG5));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                6, A6, Y6, AY6, I6, AYYC6, AYYG6, IC6, IG6));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                7, A7, Y7, AY7, I7, AYYC7, AYYG7, IC7, IG7));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                8, A8, Y8, AY8, I8, AYYC8, AYYG8, IC8, IG8));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("PROPERTIES OF GIRDER SECTION"));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format("Area of Girder = Girder_Area_Sum = {0:f4} Sq.m ", Girder_Area_Sum));
            list.Add(string.Format(""));

            IG = IG2 + IG3 + IG4 + IG5 + IG6 + IG7 + IG8;
            list.Add(string.Format("MOMENT OF INERTIA ( Iz) = IG2 + IG3 + IG4 + IG5 + IG6 + IG7 + IG8"));
            list.Add(string.Format("                        = {0:f5} + {1:f5} + {2:f5} + {3:f5} + {4:f5} + {5:f5} + {6:f5}",
               IG2, IG3, IG4, IG5, IG6, IG7, IG8));
            list.Add(string.Format("                        = {0:f5} Sq.m ", IG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Y (from top) of Girder = Girder_YTop = {0:f4} m ", Girder_YTop));
            list.Add(string.Format(""));

            double Girder_ZTop = IG / Girder_YTop;
            list.Add(string.Format(""));
            list.Add(string.Format("Z (from top) of Girder = Iz /  Girder_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Girder_YTop, Girder_ZTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double DMG = (d2 + d3 + d5 + d7 + d8) / 1000.0;

            double Girder_YBot = DMG - Girder_YTop;
            list.Add(string.Format("Y (from bottom) of Girder = DMG - Girder_YTop = {0:f4} - {1:f4} = {2:f4} m ", DMG, Girder_YTop, Girder_YBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Girder_ZBot = IG / Girder_YBot;
            list.Add(string.Format("Z (from bottom) of Girder = Iz /  Girder_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Girder_YBot, Girder_ZBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("PROPERTIES OF COMPOSITE SECTION"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format("Area of Composite Section = Composite_Area_Sum = {0:f4} Sq.m ", Composite_Area_Sum));
            list.Add(string.Format(""));

            IC = IC1 + IC2 + IC3 + IC4 + IC5 + IC6 + IC7 + IC8;
            list.Add(string.Format("MOMENT OF INERTIA ( Iz) = IC1 + IC2 + IC3 + IC4 + IC5 + IC6 + IC7 + IC8"));
            list.Add(string.Format("                        = {0:f5} + {1:f5} + {2:f5} + {3:f5} + {4:f5} + {5:f5} + {6:f5} + {7:f5}",
               IC1, IC2, IC3, IC4, IC5, IC6, IC7, IC8));
            list.Add(string.Format("                        = {0:f5} Sq.m ", IC));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Y (from top) of Composite = Composite_YTop = {0:f4} m ", Composite_YTop2));
            list.Add(string.Format(""));

            Composite_ZTop = IC / Composite_YTop2;
            list.Add(string.Format(""));
            list.Add(string.Format("Z (from top) of Composite = Iz /  Girder_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Composite_YTop2, Composite_ZTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //double DMG = d2 + d3 + d5 + d7 + d8;
            Composite_YBot = (d1 / 1000 + DMG) - Composite_YTop2;
            list.Add(string.Format("Y (from bottom) of Composite = DMG - Composite_YTop = {0:f4} - {1:f4} = {2:f4} m ", (d1 + DMG) / 1000, Composite_YTop2, Composite_YBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Composite_ZBot = IC / Composite_YBot;
            list.Add(string.Format("Z (from bottom) of Composite = Iz /  Composite_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Composite_YBot, Composite_ZBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Results = list;

        }
    }
    public class SupportSection
    {

        public double b1, b2, b3, b4;
        public double d1, d2, d3, d4, d5;


        public List<string> Results { get; set; }

        public double Emod_Deck { get; set; }
        public double Emod_Girder { get; set; }
        public double Emod_Ratio
        {
            get
            {
                return Emod_Deck / Emod_Girder;
            }
        }
        public SupportSection()
        {

            b1 = b2 = b3 = b4 = 0.0;
            d1 = d2 = d3 = d4 = d5 = 0.0;
            Emod_Deck = 32500.0;
            Emod_Girder = 33500.0;

            Results = new List<string>();
        }



        public double Composite_Area_Sum;

        public double Girder_Area_Sum;

        public double A1, A2, A3, A4, A5;
        public double Y1, Y2, Y3, Y4, Y5;


        public double Composite_YTop;
        public double Girder_YTop;
        public double AY1, AY2, AY3, AY4;
        public double Composite_YTop2;
        public double I1, I2, I3, I4;
        public double AYYC1, AYYC2, AYYC3, AYYC4;
        public double AYYG1, AYYG2, AYYG3, AYYG4;
        public double IG, IG1, IG2, IG3, IG4;
        public double IC, IC1, IC2, IC3, IC4;
        double Composite_YBot, Composite_ZBot, Composite_ZTop;

        public void Calculate(string step_no)
        {
            List<string> list = new List<string>();

            //b3 = b2 - b5;
            //d3 = d4;

            //b6 = b8 - b5;
            //b7 = b5;

            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.1 : SECTION DIMENTION DATA", step_no));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Element [1]   b1 = {0} mm, d1 = {1} mm", b1, d1));
            list.Add(string.Format("Element [2]   b2 = {0} mm, d2 = {1} mm", b2, d2));
            list.Add(string.Format("Element [3]   b3 = {0} mm, d3 = {1} mm", b3, d3));
            list.Add(string.Format("Element [4]   b4 = {0} mm, d4 = {1} mm", b4, d4));
            list.Add(string.Format(""));
            list.Add(string.Format("Element [5]   d5 = {0} mm", d5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Modulus of Elasticity of Deck = Emod_Deck = {0:f3}", Emod_Deck));
            list.Add(string.Format("Modulus of Elasticity of Girder = Emod_Girder = {0:f3}", Emod_Girder));

            //double Emod_ratio = Emod_Deck / Emod_Girder;
            list.Add(string.Format("Emod_ratio = Emod_Deck / Emod_Girder = {0:f3} / {1:f3} = {2:f5}", Emod_Deck, Emod_Girder, Emod_Ratio));




            //list.Add(string.Format("Calculation for Section Properties of Outer Girder at Mid Span"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.2 : AREA of each section Element", step_no));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));

            A1 = (b1 * d1 / 1000000.0) * Emod_Ratio;
            list.Add(string.Format("1. Part of Deck Slab = A1 = (b1 * d1 / 10^6) * Emod_Ratio"));
            list.Add(string.Format("                          = ({0} * {1} / 10^6) * {2:f4}", b1, d1, Emod_Ratio));
            list.Add(string.Format("                          = {0:f4} Sq.m", A1));
            list.Add(string.Format(""));

            double d0 = d2 + d2 + d4;
            A2 = (b2 * d2 / 1000000.0);



            list.Add(string.Format("2. Top Flange = A2 = (b2 * d2 / 10^6)"));
            list.Add(string.Format("                   = ({0} * {1} / 10^6)", b2, d2));
            list.Add(string.Format("                   = {0:f4} Sq.m", A2));
            list.Add(string.Format(""));
            A3 = ((b3 * d3 * 0.5) / 1000000.0);
            list.Add(string.Format("3. Top Haunch Both Triangles = A3 = ((b3 * d3 * 0.5) / 10^6)"));
            list.Add(string.Format("                                  = (({0} * {1} * 0.5) / 10^6)", b3, d3));
            list.Add(string.Format("                                  = {0:f4} Sq.m", A3));
            list.Add(string.Format(""));
            A4 = ((b4 * d4) / 1000000.0);
            list.Add(string.Format("4. Stem of Girder = A4 = (b4 * d4 / 10^6)"));
            list.Add(string.Format("                       = ({0} * {1} / 10^6)", b4, d4));
            list.Add(string.Format("                       = {0:f4} Sq.m", A4));
            list.Add(string.Format(""));

            A5 = ((b1 * d5 * 0.5) / 1000000.0);
            list.Add(string.Format("5. Trianglar Deck = A5 = (b1 * d5 * 0.5 / 10^6)"));
            list.Add(string.Format("                       = ({0} * {1} * 0.5 / 10^6)", b1, d5));
            list.Add(string.Format("                       = {0:f4} Sq.m", A5));

            Composite_Area_Sum = A1 + A2 + A3 + A4 + A5;
            list.Add(string.Format(""));
            list.Add(string.Format("Composite Area Sum = A1 + A2 + A3 + A4 + A5"));
            list.Add(string.Format("                   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3}",
                A1, A2, A3, A4, A5));
            list.Add(string.Format("                   = {0:f3} Sq.m", Composite_Area_Sum));
            list.Add(string.Format(""));

            Girder_Area_Sum = A2 + A3 + A4;
            list.Add(string.Format(""));
            list.Add(string.Format("Girder Area Sum = A2 + A3 + A4"));
            list.Add(string.Format("                = {0:f3} + {1:f3} + {2:f3} ",
                A2, A3, A4));
            list.Add(string.Format("                = {0:f3} Sq.m", Girder_Area_Sum));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.3 : Y-from Top at each Cross Section Elements", step_no));
            list.Add(string.Format("-----------------------------------------------------------"));
            list.Add(string.Format(""));
            Y1 = d1 / 2;
            list.Add(string.Format("1. Y1 = d1 / 2 = {0} / 2 = {1:f3} mm = {2:f3} m", d1, Y1, Y1 /= 1000));
            list.Add(string.Format(""));
            Y2 = (d1 + d2 / 2);
            list.Add(string.Format("2. Y2 = (d1 + d2 / 2) = ({0} + {1} / 2) = {2:f3} mm = {3:f3} m", d1, d2, Y2, Y2 /= 1000));
            list.Add(string.Format(""));
            Y3 = (d1 + d2 + d3 / 3);
            list.Add(string.Format("3. Y3 = (d1 + d2 + d3 / 3) = ({0} + {1} + {2} / 3) = {3:f3} mm = {4:f3} m", d1, d2, d3, Y3, Y3 /= 1000));
            list.Add(string.Format(""));
            Y4 = (d1 + d2 + d4 / 2);
            list.Add(string.Format("4. Y4 = (d1 + d2 + d4 / 2) = ({0} + {1} + {2} / 2) = {3:f3} mm = {4:f3} m", d1, d2, d4, Y4, Y4 /= 1000));

            Composite_YTop = Y1 + Y2 + Y3 + Y4;

            list.Add(string.Format("Composite_Top = Y1 + Y2 + Y3 + Y4 + Y5 + Y6 + Y7 + Y8"));
            list.Add(string.Format("              = {0:f3} + {1:f3} + {2:f3} + {3:f3}",
                Y1, Y2, Y3, Y4));
            list.Add(string.Format("              = {0:f3} m", Composite_YTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.4 : A x Y for Each Cross Section Element", step_no));
            list.Add(string.Format("-----------------------------------------------------"));
            list.Add(string.Format(""));

            AY1 = A1 * Y1;
            list.Add(string.Format("1. AY1 = A1 x Y1 = {0:f3} x {1:f3} = {2:f3}", A1, Y1, AY1));

            AY2 = A2 * Y2;
            list.Add(string.Format("2. AY2 = A2 x Y2 = {0:f3} x {1:f3} = {2:f3}", A2, Y2, AY2));

            AY3 = A3 * Y3;
            list.Add(string.Format("3. AY3 = A3 x Y3 = {0:f3} x {1:f3} = {2:f3}", A3, Y3, AY3));

            AY4 = A4 * Y4;
            list.Add(string.Format("4. AY4 = A4 x Y4 = {0:f3} x {1:f3} = {2:f3}", A4, Y4, AY4));

            Composite_Area_Sum = A1 + A2 + A3 + A4 + A5;
            Girder_Area_Sum = A2 + A3 + A4;

            list.Add(string.Format(""));
            Composite_YTop2 = (AY1 + AY2 + AY3 + AY4) / Composite_Area_Sum;
            list.Add(string.Format(""));
            list.Add(string.Format("Composite_YTop2 = (AY1 + AY2 + AY3 + AY4) / Composite_Area_Sum"));
            list.Add(string.Format("                = ({0:f3} / {1:f3})",
                (AY1 + AY2 + AY3 + AY4), Composite_Area_Sum));
            list.Add(string.Format(""));
            list.Add(string.Format("                = {0:f3}", Composite_YTop2));
            list.Add(string.Format(""));

            Girder_YTop = ((AY2 + AY3 + AY4) / Girder_Area_Sum) - (d1 / 1000);
            list.Add(string.Format(""));
            list.Add(string.Format("Girder_YTop = (AY2 + AY3 + AY4) / Composite_Area_Sum - d1"));
            list.Add(string.Format("            = ({0:f3} / {1:f3}) - {2:f3}",
                (AY2 + AY3 + AY4), Girder_Area_Sum, (d1 / 1000)));
            list.Add(string.Format(""));
            list.Add(string.Format("            = {0:f3}", Girder_YTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.5 : Moment of Inertia = ISelf for each section", step_no));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            I1 = ((b1 * Math.Pow(d1, 3) / 12.0) / Math.Pow(10, 12)) * Emod_Ratio;
            list.Add(string.Format("1. I1 = ((b1 * d1^3 / 12) / 10^12) * Emod_Ratio"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12) / 10^12) * {2:f4}", b1, d1, Emod_Ratio));
            list.Add(string.Format("      = {0:f6} m^4", I1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I2 = ((b2 * Math.Pow(d2, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("2. I2 = ((b2 * d2^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12) / 10^12)", b2, d2));
            list.Add(string.Format("      = {0:f6} m^4", I2));
            list.Add(string.Format(""));

            I3 = (((b3) * Math.Pow(d3, 3) / 36.0) / Math.Pow(10, 12));
            list.Add(string.Format("3. I3 = (((b3) * d3^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12)", b3, d3));
            list.Add(string.Format("      = {0:f6} m^4", I3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I4 = ((b4 * Math.Pow(d4, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("4. I4 = ((b5 * d2^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12) ", b4, d4));
            list.Add(string.Format("      = {0:f6} m^4", I4));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.6 : AY^2 for Composite Section (Girder + Deck Slab)", step_no));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));

            AYYC1 = A1 * Math.Pow((Y1 - Composite_YTop2), 2);
            list.Add(string.Format("1. AYYC1 = A1 * (Y1 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A1, Y1, Composite_YTop2, AYYC1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC2 = A2 * Math.Pow((Y2 - Composite_YTop2), 2);
            list.Add(string.Format("2. AYYC2 = A2 * (Y2 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A2, Y2, Composite_YTop2, AYYC2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC3 = A3 * Math.Pow((Y3 - Composite_YTop2), 2);
            list.Add(string.Format("3. AYYC3 = A3 * (Y3 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A3, Y3, Composite_YTop2, AYYC3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC4 = A4 * Math.Pow((Y4 - Composite_YTop2), 2);
            list.Add(string.Format("4. AYYC4 = A4 * (Y4 - Composite_YTop2)^2 = {0:f3} * ({1:f3} - {2:f3})^2 = {3:f3}", A4, Y4, Composite_YTop2, AYYC4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("AY^2 for Girder Section"));
            list.Add(string.Format("-----------------------"));
            //Girder_t


            AYYG1 = 0;
            list.Add(string.Format(""));
            list.Add(string.Format("1.  AYYG1 = ----------"));
            list.Add(string.Format(""));

            AYYG2 = A2 * Math.Pow((Y2 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("2.  AYYG2 = A2 * (Y2 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A2, Y2, Girder_YTop, d1, AYYG2));
            list.Add(string.Format(""));

            AYYG3 = A3 * Math.Pow((Y3 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("3.  AYYG3 = A3 * (Y3 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A3, Y3, Girder_YTop, d1, AYYG2));
            list.Add(string.Format(""));

            AYYG4 = A4 * Math.Pow((Y4 - Girder_YTop - (d1 / 1000)), 2);
            list.Add(string.Format(""));
            list.Add(string.Format("4.  AYYG4 = A4 * (Y4 - Girder_YTop - (d1 / 1000))^2 = {0:f3} * ({1:f3} - {2:f3} - ({3} / 1000))^2 = {4:f3}",
                A4, Y4, Girder_YTop, d1, AYYG4));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("I-Girder"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));
            IG1 = 0;
            list.Add(string.Format("1. IG1 = I1 + AYYG1 = -------------"));
            list.Add(string.Format(""));

            IG2 = I2 + AYYG2;
            list.Add(string.Format("2. IG2 = I2 + AYYG2 = {0:f5} + {1:f5} = {2:f5} ", I2, AYYG2, IG2));
            list.Add(string.Format(""));


            IG3 = I3 + AYYG3;
            list.Add(string.Format("3. IG3 = I3 + AYYG3 = {0:f5} + {1:f5} = {2:f5} ", I3, AYYG3, IG3));
            list.Add(string.Format(""));


            IG4 = I4 + AYYG4;
            list.Add(string.Format("4. IG4 = I4 + AYYG4 = {0:f5} + {1:f5} = {2:f5} ", I4, AYYG4, IG4));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("I-Composite"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            IC1 = I1 + AYYC1;
            list.Add(string.Format("1. IC1 = I1 + AYYC1 = {0:f5} + {1:f5} = {2:f5} ", IC1, AYYC1, IC1));
            list.Add(string.Format(""));

            IC2 = I2 + AYYC2;
            list.Add(string.Format("2. IC2 = I2 + AYYC2 = {0:f5} + {1:f5} = {2:f5} ", IC2, AYYC2, IC2));
            list.Add(string.Format(""));

            IC3 = I3 + AYYC3;
            list.Add(string.Format("3. IC3 = I3 + AYYC3 = {0:f5} + {1:f5} = {2:f5} ", IC3, AYYC3, IC3));
            list.Add(string.Format(""));

            IC4 = I4 + AYYC4;
            list.Add(string.Format("4. IC4 = I4 + AYYC4 = {0:f5} + {1:f5} = {2:f5} ", IC4, AYYC4, IC4));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE {0}.1 : SECTION PROPERTY TABLE", step_no));
            list.Add(string.Format("------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Element    Area    Y(from top)    A.Y        ISelf      AY^2        AY^2          I          I"));
            list.Add(string.Format("          (Sq.m)      (m)      (Sq.Sq.m)   (Sq.Sq.m)   Composite   Girder      Composite   Girder"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                1, A1, Y1, AY1, I1, AYYC1, AYYG1, IC1, IG1));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                2, A2, Y2, AY2, I2, AYYC2, AYYG2, IC2, IG2));

            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                3, A3, Y3, AY3, I3, AYYC3, AYYG3, IC3, IG3));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}{7,12:f6}{8,12:f6}",
                4, A4, Y4, AY4, I4, AYYC4, AYYG4, IC4, IG4));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("PROPERTIES OF GIRDER SECTION"));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format("Area of Girder = Girder_Area_Sum = {0:f4} Sq.m ", Girder_Area_Sum));
            list.Add(string.Format(""));

            IG = IG1 + IG2 + IG3 + IG4;
            list.Add(string.Format("MOMENT OF INERTIA ( Iz) = IG1 + IG2 + IG3 + IG4"));
            list.Add(string.Format("                        = {0:f5} + {1:f5} + {2:f5} + {3:f5}",
               IG1, IG2, IG3, IG4));
            list.Add(string.Format("                        = {0:f5} Sq.m ", IG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Y (from top) of Girder = Girder_YTop = {0:f4} m ", Girder_YTop));
            list.Add(string.Format(""));

            double Girder_ZTop = IG / Girder_YTop;
            list.Add(string.Format(""));
            list.Add(string.Format("Z (from top) of Girder = Iz /  Girder_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Girder_YTop, Girder_ZTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double DMG = (d2 + d3 + d4) / 1000.0;

            double Girder_YBot = DMG - Girder_YTop;
            list.Add(string.Format("Y (from bottom) of Girder = DMG - Girder_YTop = {0:f4} - {1:f4} = {2:f4} m ", DMG, Girder_YTop, Girder_YBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double Girder_ZBot = IG / Girder_YBot;
            list.Add(string.Format("Z (from bottom) of Girder = Iz /  Girder_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Girder_YBot, Girder_ZBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("PROPERTIES OF COMPOSITE SECTION"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format("Area of Composite Section = Composite_Area_Sum = {0:f4} Sq.m ", Composite_Area_Sum));
            list.Add(string.Format(""));

            IC = IC1 + IC2 + IC3 + IC4;
            list.Add(string.Format("MOMENT OF INERTIA ( Iz) = IC1 + IC2 + IC3 + IC4"));
            list.Add(string.Format("                        = {0:f5} + {1:f5} + {2:f5} + {3:f5}",
               IC1, IC2, IC3, IC4));
            list.Add(string.Format("                        = {0:f5} Sq.m ", IC));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Y (from top) of Composite = Composite_YTop = {0:f4} m ", Composite_YTop2));
            list.Add(string.Format(""));

            Composite_ZTop = IC / Composite_YTop2;
            list.Add(string.Format(""));
            list.Add(string.Format("Z (from top) of Girder = Iz /  Girder_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Composite_YTop2, Composite_ZTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //double DMG = d2 + d3 + d5 + d7 + d8;
            Composite_YBot = (d1 / 1000 + DMG) - Composite_YTop2;
            list.Add(string.Format("Y (from bottom) of Girder = DMG - Composite_YTop = {0:f4} - {1:f4} = {2:f4} m ", (d1 + DMG) / 1000, Composite_YTop2, Composite_YBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Composite_ZBot = IC / Composite_YBot;
            list.Add(string.Format("Z (from bottom) of Girder = Iz /  Composite_YTop = {0:f4} / {1:f4} = {2:f4} m ", IC, Composite_YBot, Composite_ZBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            Results = list;
        }
    }
    public class EndSection
    {

        public double b1, b2, b3, b4;
        public double d1, d2, d3, d4, d5;


        public List<string> Results { get; set; }

        public double Emod_Deck { get; set; }
        public double Emod_Girder { get; set; }
        public double Emod_Ratio
        {
            get
            {
                return Emod_Deck / Emod_Girder;
            }
        }
        public EndSection()
        {

            b1 = b2 = b3 = b4 = 0.0;
            d1 = d2 = d3 = d4 = d5 = 0.0;
            Emod_Deck = 32500.0;
            Emod_Girder = 33500.0;

            Results = new List<string>();
        }



        public double Composite_Area_Sum;

        public double Girder_Area_Sum;

        public double A1, A2, A3, A4, A5;
        public double Y1, Y2, Y3, Y4, Y5;


        public double Composite_YTop;
        public double Girder_YTop;
        public double AY1, AY2, AY3, AY4;
        public double Composite_YTop2;
        public double I1, I2, I3, I4;
        public double AYYC1, AYYC2, AYYC3, AYYC4;
        public double AYYG1, AYYG2, AYYG3, AYYG4;
        public double IG, IG1, IG2, IG3, IG4;
        public double IC, IC1, IC2, IC3, IC4;
        double Composite_YBot, Composite_ZBot, Composite_ZTop;

        public void Calculate(string step_no)
        {
            List<string> list = new List<string>();

            //b3 = b2 - b5;
            //d3 = d4;

            //b6 = b8 - b5;
            //b7 = b5;

            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.1 : SECTION DIMENTION DATA", step_no));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Element [1]   b1 = {0} mm, d1 = {1} mm", b1, d1));
            list.Add(string.Format("Element [2]   b2 = {0} mm, d2 = {1} mm", b2, d2));
            list.Add(string.Format("Element [3]   b3 = {0} mm, d3 = {1} mm", b3, d3));
            list.Add(string.Format("Element [4]   b4 = {0} mm, d4 = {1} mm", b4, d4));
            list.Add(string.Format(""));
            list.Add(string.Format("Element [5]   d5 = {0} mm", d5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Modulus of Elasticity of Deck = Emod_Deck = {0:f3}", Emod_Deck));
            list.Add(string.Format("Modulus of Elasticity of Girder = Emod_Girder = {0:f3}", Emod_Girder));

            //double Emod_ratio = Emod_Deck / Emod_Girder;
            list.Add(string.Format("Emod_ratio = Emod_Deck / Emod_Girder = {0:f3} / {1:f3} = {2:f5}", Emod_Deck, Emod_Girder, Emod_Ratio));




            //list.Add(string.Format("Calculation for Section Properties of Outer Girder at Mid Span"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.2 : AREA of each section Element", step_no));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));

            A1 = (b1 * d1 / 1000000.0);
            list.Add(string.Format("1. Part of Deck Slab = A1 = (b1 * d1 / 10^6)"));
            list.Add(string.Format("                          = ({0} * {1} / 10^6)", b1, d1));
            list.Add(string.Format("                          = {0:f4} Sq.m", A1));
            list.Add(string.Format(""));

            double d0 = d2 + d2 + d4;
            A2 = (b2 * d2 / 1000000.0);



            list.Add(string.Format("2. Top Flange = A2 = (b2 * d2 / 10^6)"));
            list.Add(string.Format("                   = ({0} * {1} / 10^6)", b2, d2));
            list.Add(string.Format("                   = {0:f4} Sq.m", A2));
            list.Add(string.Format(""));
            A3 = ((b3 * d3 * 0.5) / 1000000.0);
            list.Add(string.Format("3. Top Haunch Triangle = A3 = ((b3 * d3 * 0.5) / 10^6)"));
            list.Add(string.Format("                            = (({0} * {1} * 0.5) / 10^6)", b3, d3));
            list.Add(string.Format("                            = {0:f4} Sq.m", A3));
            list.Add(string.Format(""));
            A4 = ((b4 * d4) / 1000000.0);
            list.Add(string.Format("4. Stem of Girder = A4 = (b4 * d4 / 10^6)"));
            list.Add(string.Format("                       = ({0} * {1} / 10^6)", b4, d4));
            list.Add(string.Format("                       = {0:f4} Sq.m", A4));
            list.Add(string.Format(""));

            //A5 = ((b1 * d5 * 0.5) / 1000000.0);
            //list.Add(string.Format("5. Trianglar Deck = A5 = (b1 * d5 * 0.5 / 10^6)"));
            //list.Add(string.Format("                       = ({0} * {1} * 0.5 / 10^6)", b1, d5));
            //list.Add(string.Format("                       = {0:f4} Sq.m", A5));


            Composite_Area_Sum = A1 + A2 + A3 + A4;
            Girder_Area_Sum = A2 + A3 + A4;

            list.Add(string.Format(""));
            list.Add(string.Format("Composite Area Sum = A1 + A2 + A3 + A4"));
            list.Add(string.Format("                   = {0:f3} + {1:f3} + {2:f3} + {3:f3}",
                A1, A2, A3, A4));
            list.Add(string.Format("                   = {0:f3} Sq.m", Composite_Area_Sum));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.3 : Y-from Top at each Cross Section Elements", step_no));
            list.Add(string.Format("-----------------------------------------------------------"));
            list.Add(string.Format(""));
            Y1 = d1 / 2 + d4;
            list.Add(string.Format("1. Y1 = d1 / 2 + d4 = {0} / 2 + {1} = {2:f3} mm = {3:f3} m", d1, d4, Y1, Y1 /= 1000));
            list.Add(string.Format(""));
            Y2 = (d4 - d2 / 2) / 1000.0;
            list.Add(string.Format("2. Y2 = (d4 - d2 / 2) / 1000.0 = ({0} - {1} / 2) / 1000.0 = {2:f3} m", d4, d2, Y2));
            list.Add(string.Format(""));
            Y3 = (d4 - d3 / 3) / 1000.0;
            list.Add(string.Format("3. Y3 = (d4 - d3 / 3) /1000 = ({0} - {1} / 3) = {2:f3} m", d4, d3, Y3));
            list.Add(string.Format(""));
            Y4 = (d4 / 2) / 1000.0;
            list.Add(string.Format("4. Y4 = (d4 / 2) / 1000 = ({0} / 2) = {2:f3} m", d4, d4, Y4));

            Composite_YTop = Y1 + Y2 + Y3 + Y4;

            list.Add(string.Format("Composite_Top = Y1 + Y2 + Y3 + Y4 + Y5 + Y6 + Y7 + Y8"));
            list.Add(string.Format("              = {0:f3} + {1:f3} + {2:f3} + {3:f3}",
                Y1, Y2, Y3, Y4));
            list.Add(string.Format("              = {0:f3} m", Composite_YTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.4 : A x Y for Each Cross Section Element", step_no));
            list.Add(string.Format("-----------------------------------------------------"));
            list.Add(string.Format(""));

            AY1 = A1 * Y1;
            list.Add(string.Format("1. AY1 = A1 x Y1 = {0:f3} x {1:f3} = {2:f3}", A1, Y1, AY1));

            AY2 = A2 * Y2;
            list.Add(string.Format("2. AY2 = A2 x Y2 = {0:f3} x {1:f3} = {2:f3}", A2, Y2, AY2));

            AY3 = A3 * Y3;
            list.Add(string.Format("3. AY3 = A3 x Y3 = {0:f3} x {1:f3} = {2:f3}", A3, Y3, AY3));

            AY4 = A4 * Y4;
            list.Add(string.Format("4. AY4 = A4 x Y4 = {0:f3} x {1:f3} = {2:f3}", A4, Y4, AY4));

            list.Add(string.Format(""));
            Composite_YTop2 = (AY1 + AY2 + AY3 + AY4) / Composite_Area_Sum;
            list.Add(string.Format(""));
            list.Add(string.Format("Composite_YTop2 = (AY1 + AY2 + AY3 + AY4) / Composite_Area_Sum"));
            list.Add(string.Format("                = ({0:f3} / {1:f3})",
                (AY1 + AY2 + AY3 + AY4), Composite_Area_Sum));
            list.Add(string.Format(""));
            list.Add(string.Format("                = {0:f3}", Composite_YTop2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.5 : Moment of Inertia = ISelf for each section", step_no));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            I1 = ((b1 * Math.Pow(d1, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("1. I1 = ((b1 * d1^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12) / 10^12)", b1, d1));
            list.Add(string.Format("      = {0:f6} m^4", I1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I2 = ((b2 * Math.Pow(d2, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("2. I2 = ((b2 * d2^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12) / 10^12)", b2, d2));
            list.Add(string.Format("      = {0:f6} m^4", I2));
            list.Add(string.Format(""));

            I3 = (((b3) * Math.Pow(d3, 3) / 36.0) / Math.Pow(10, 12));
            list.Add(string.Format("3. I3 = (((b3) * d3^3 / 36) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 36)  / 10^12)", b3, d3));
            list.Add(string.Format("      = {0:f6} m^4", I3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            I4 = ((b4 * Math.Pow(d4, 3) / 12.0) / Math.Pow(10, 12));
            list.Add(string.Format("4. I4 = ((b4 * d4^3 / 12) / 10^12)"));
            list.Add(string.Format("      = (({0} * {1}^3 / 12)  / 10^12) ", b4, d4));
            list.Add(string.Format("      = {0:f6} m^4", I4));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("STEP {0}.6 : AY^2 for Composite Section", step_no));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));

            AYYC1 = A1 * Math.Pow((Y1), 2);
            list.Add(string.Format("1. AYYC1 = A1 * (Y1)^2 = {0:f3} * ({1:f3})^2 = {2:f3}", A1, Y1, AYYC1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC2 = A2 * Math.Pow((Y2), 2);
            list.Add(string.Format("2. AYYC2 = A2 * (Y2)^2 = {0:f3} * ({1:f3})^2 = {2:f3}", A2, Y2, AYYC2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC3 = A3 * Math.Pow((Y3), 2);
            list.Add(string.Format("3. AYYC3 = A3 * (Y3)^2 = {0:f3} * ({1:f3})^2 = {2:f3}", A3, Y3, AYYC3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            AYYC4 = A4 * Math.Pow((Y4), 2);
            list.Add(string.Format("4. AYYC4 = A4 * (Y4)^2 = {0:f3} * ({1:f3})^2 = {2:f3}", A4, Y4, AYYC4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("I-Composite"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            IC1 = I1 + AYYC1;
            list.Add(string.Format("1. IC1 = I1 + AYYC1 = {0:f5} + {1:f5} = {2:f5} ", IC1, AYYC1, IC1));
            list.Add(string.Format(""));

            IC2 = I2 + AYYC2;
            list.Add(string.Format("2. IC2 = I2 + AYYC2 = {0:f5} + {1:f5} = {2:f5} ", IC2, AYYC2, IC2));
            list.Add(string.Format(""));

            IC3 = I3 + AYYC3;
            list.Add(string.Format("3. IC3 = I3 + AYYC3 = {0:f5} + {1:f5} = {2:f5} ", IC3, AYYC3, IC3));
            list.Add(string.Format(""));

            IC4 = I4 + AYYC4;
            list.Add(string.Format("4. IC4 = I4 + AYYC4 = {0:f5} + {1:f5} = {2:f5} ", IC4, AYYC4, IC4));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE {0}.1 : SECTION PROPERTY TABLE", step_no));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format("Element    Area    Y(from top)    A.Y        ISelf       AY^2          I     "));
            list.Add(string.Format("          (Sq.m)      (m)      (Sq.Sq.m)   (Sq.Sq.m)   Composite   Composite"));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}",
                1, A1, Y1, AY1, I1, AYYC1, IC1));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}",
                2, A2, Y2, AY2, I2, AYYC2, IC2));

            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}",
                3, A3, Y3, AY3, I3, AYYC3, IC3));
            list.Add(string.Format("{0,5}{1,12:f4}{2,10:f3}{3,12:f6}{4,12:f6}{5,12:f6}{6,12:f6}",
                4, A4, Y4, AY4, I4, AYYC4, IC4));
            list.Add(string.Format("------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));



            list.Add(string.Format("PROPERTIES OF COMPOSITE SECTION"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format("Area of Composite Section = Composite_Area_Sum = {0:f4} Sq.m ", Composite_Area_Sum));
            list.Add(string.Format(""));

            //double DMG = d2 + d3;
            Composite_YBot = (AY1 + AY2 + AY3 + AY4) / Composite_Area_Sum;
            list.Add(string.Format("Y (from bottom) of Girder = (AY1 + AY2 + AY3 + AY4) / Composite_Area_Sum "));
            list.Add(string.Format("                          = ({0:f4} + {1:f4} + {2:f4} + {3:f4}) / {4:f4} m ", AY1, AY2, AY3, AY4, Composite_Area_Sum));
            list.Add(string.Format("                          = {0:f4} m ", Composite_YBot));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            IC = (IC1 + IC2 + IC3 + IC4) - (Composite_Area_Sum * Composite_YBot * Composite_YBot);
            list.Add(string.Format("MOMENT OF INERTIA ( Iz) = (IC1 + IC2 + IC3 + IC4) - (Composite_Area_Sum * Composite_YBot^2)"));
            list.Add(string.Format("                        = ({0:f5}) - ({1:f5} x {2:f5}^2)",
               (IC1 + IC2 + IC3 + IC4), Composite_Area_Sum, Composite_YBot));
            list.Add(string.Format("                        = {0:f5} Sq.m ", IC));
            list.Add(string.Format(""));

            Composite_YTop = (d1 + d4) / 1000 - Composite_YBot;
            list.Add(string.Format(""));
            list.Add(string.Format("Y (from top) of Composite = (d1 + d4)/1000 - Composite_YBot", Composite_YTop2));
            list.Add(string.Format("                          = ({0} + {1})/1000 - {2:f4}", d1, d4, Composite_YBot));
            list.Add(string.Format("                          = {0:f4} m ", Composite_YTop));
            list.Add(string.Format(""));



            Composite_ZBot = IC / Composite_YBot;
            list.Add(string.Format("Z (from bottom) of Composite = Iz /  Composite_YBot = {0:f4} / {1:f4} = {2:f4} m ", IG, Composite_YBot, Composite_ZBot));
            list.Add(string.Format(""));


            Composite_ZTop = IC / Composite_YTop;
            list.Add(string.Format(""));
            list.Add(string.Format("Z (from top) of Composite = Iz /  Composite_YTop = {0:f4} / {1:f4} = {2:f4} m ", IG, Composite_YTop, Composite_ZTop));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            Results = list;
        }
    }
}
