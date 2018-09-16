using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;
using HEADSNeed.ASTRA.ASTRAForms;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

using VDRAW = VectorDraw.Professional.ActionUtilities;
using AstraInterface.Interface;


namespace ASTRAStructures
{
    public class SteelColumnDesign
    {
        IApplication iApp;
        public double l, a, P, M, V, e, Pms, fy, fs, fb, Pcs, Ps;

        public double n, tb, Dr, Nr;
        public double A, h, Bf, tw, Ixx, Iyy, rxx, ryy, rxx_comp;
        public string Section_Name = "ISMB 300";
        public string ColumnNo = "C1";
        public SteelColumnDesign(IApplication iapp)
        {
            iApp = iapp;


            l = a = P = M = V = e = Pms = fy = fs = fb = Pcs = Ps = 0.0;

            l = 4.0;
            a = 300;
            P = 1350;
            M = 50;
            V = 40;
            e = 100;
            Pms = 165;
            fy = 250;
            fs = 100;
            Pcs = 120;
            Ps = 150;

            n = 2;
            tb = 6.0;
            Dr = 20.0;
            Nr = 6.0;


            A = 5626;
            h = 300;
            Bf = 140;
            tw = 8.8;
            Ixx = 8603.6;
            Iyy = 453.9;
            rxx = 123.7;
            ryy = 28.4;
        }
        public List<string> Design_Individual_Program()
        {
            List<string> list = new List<string>();
            IS_DESIGN_OK = true;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Steel Column"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User’s Input data:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of Column = l = {0:f2} m.", l));
            list.Add(string.Format("Bearing at each end = a = {0:f2} mm.", a));
            list.Add(string.Format("Maximum Compressive Load = P = {0:f3} kN", P));
            list.Add(string.Format("(The load acts on the y-y axis)"));
            list.Add(string.Format("Maximum Bending Moment  = M = {0:f3} kN-m", M));
            list.Add(string.Format("Maximum Shear Force = V = {0:f3} kN", V));
            list.Add(string.Format("Eccentricity of Vertical Load = e = {0:f2} mm", e));
            list.Add(string.Format("Permissible Bending Stress = Pms = {0:f2} N/Sq.mm", Pms));
            list.Add(string.Format("Permissible Stress = fy = {0} N/Sq.mm", fy));
            list.Add(string.Format("Permissible stress of a rivet in shear  = fs = {0} N/Sq.mm", fs));
            list.Add(string.Format("Permissible stress of a rivet in bearing  = fb = {0} N/Sq.mm", fb));
            list.Add(string.Format("Permissible Compressive Stress = Pcs = {0} N/Sq.mm", Pcs));
            list.Add(string.Format("Safe compressive stress for splice plate = Ps = {0} N/Sq.mm", Ps));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of Steel Section = n = {0}", n));
            list.Add(string.Format("Minimum thickness of Batten Plates = tb = {0} mm thick", tb));
            list.Add(string.Format("Diameter of Rivets = Dr = {0} mm", Dr));
            list.Add(string.Format("Number of rivets for connecting Battens to each flange = Nr = {0}", Nr));
            list.Add(string.Format(""));
            //list.Add(string.Format("Selected Steel Section = ISMB 300 (Selected from Combo) "));
            list.Add(string.Format("Selected Steel Section = {0} ", Section_Name));
            list.Add(string.Format(""));


            list.Add(string.Format("A = {0} Sq.mm", A));
            list.Add(string.Format("h = {0} mm", h));
            list.Add(string.Format("Bf = {0} mm", Bf));
            list.Add(string.Format("tw = {0} mm", tw));
            list.Add(string.Format("Ixx = {0} x 10000 Sq. Sq. mm,", Ixx / 10000));
            list.Add(string.Format("Iyy = {0} x 10000 Sq. Sq. mm,", Iyy / 10000));
            list.Add(string.Format("rxx = {0} mm, ryy = {1} mm,", rxx, ryy));
            list.Add(string.Format("for combined section rxx = {0} mm", rxx));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Design Calculations:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Section Area"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double req_val = 0.0;
            double prv_val = 0.0;

            req_val = P * 1000 / Pcs;
            prv_val = A * n;

            list.Add(string.Format("Required Section Area = P / Pcs = {0:f2} x 1000 / {1:f2} = {2:f2} Sq.mm", P, Pcs, req_val));
            list.Add(string.Format(""));
            if (prv_val > req_val)
                list.Add(string.Format("Provided Section Area = A x n = {0:f2} x {1} = {2:f2} Sq.mm > {3:f2} Sq.mm Hence OK.", A, n, prv_val, req_val));
            else if (prv_val < req_val)
            {
                IS_DESIGN_OK = false;
                list.Add(string.Format("Provided Section Area = A x n = {0:f2} x {1} = {2:f2} Sq.mm < {3:f2} Sq.mm Hence NOT OK.", A, n, prv_val, req_val));
            }
            else
                list.Add(string.Format("Provided Section Area = A x n = {0:f2} x {1} = {2:f2} Sq.mm = {3:f2} Sq.mm Hence OK.", A, n, prv_val, req_val));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : Slenderness Ratio"));
            list.Add(string.Format("--------------------------"));
            list.Add(string.Format(""));

            double le = 0.8 * l * 1000;
            list.Add(string.Format("Effective Length of Column = le = 0.8 x l x 1000 = 0.8 x {0:f3} x 1000 = {1:f3} mm", l, le));


            double le10 = le * 1.10;
            list.Add(string.Format("For battened column the effective length is increased by 10% = le = 1.10 x {0:f3} = {1:f3} mm.", le, le10));
            list.Add(string.Format(""));

            double lamda = le10 / rxx;

            list.Add(string.Format("Slenderness Ratio based on rxx = lamda = le / rxx = {0:f3} / {1:f3} = {2:f3}", le10, rxx, lamda));


            string kStr = "";
            //double Sigma_ac = iApp.Tables.Permissible_Bending_Compressive_Stresses(h / tw, lamda, ref kStr);
            double Sigma_ac = iApp.Tables.Permissible_Bending_Compressive_Stresses(h / tw, lamda, ref kStr);
            //double Sigma_ac = 146;

            list.Add(string.Format("Allowable Compressive Stress corresponding to this Slenderness Ratio = Sigma_ac = {0:f3} N/Sq.mm", Sigma_ac));
            list.Add(string.Format(""));

            double comp_strs = (P * 1000 / (2 * A));
            list.Add(string.Format("Actual Compressive stress induced = P x 1000 / (2 x A)"));
            list.Add(string.Format("                                  = {0:f3} x 1000 / (2 x  {1:f3}) ", P, A));

            if (Sigma_ac > comp_strs)
                list.Add(string.Format("                                  = {0:f0} N/Sq. mm < {1:f0} N/Sq.mm, Hence, OK.", comp_strs, Sigma_ac));
            else
            {
                IS_DESIGN_OK = false;
                list.Add(string.Format("                                  = {0:f0} N/Sq. mm > {1:f0} N/Sq.mm, Hence, NOT OK.", comp_strs, Sigma_ac));
            }

            list.Add(string.Format(""));


            list.Add(string.Format("The spacing between the two sections of the column is so chosen,"));
            list.Add(string.Format("that the radii of gyration about X & Y axes are equal."));
            list.Add(string.Format(""));

            double spacing = 2 * Math.Sqrt(Math.Pow(rxx, 2) - Math.Pow(ryy, 2));
            list.Add(string.Format("Therefore, spacing = 2 x SQRT(rxx^2 - ryy^2) = 2 x SQRT({0:f3}^2 - {1:f3}^2) = {2:f3} sq.mm", rxx, ryy, spacing));
            list.Add(string.Format(""));

            //double s = Math.Round(spacing, 0, MidpointRounding.AwayFromZero);

            double s = (int)spacing;

            //s = 245;
            //double ss = 
            //list.Add(string.Format("Let us provide spacing of 245 mm between the centres of the two sections,"));

            list.Add(string.Format("Let us provide spacing of {0} mm between the centres of the two sections,", s));

            list.Add(string.Format("Figure 1 shows the position of the two I-sections."));
            //list.Add(string.Format("Therefore, for the composite section, therefore, ‘s’ = 245 mm."));
            list.Add(string.Format("Therefore, for the composite section, therefore, ‘s’ = {0} mm.", s));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Figure 1 (4.29) shown the position of the two I-sections."));
            list.Add(string.Format(""));
            list.Add(string.Format("For the composite section, "));

            double Iyy_dash = (n * (Iyy + A * rxx * rxx));
            list.Add(string.Format(" Iyy’ = {0} x (Iyy + A x rxx^2)", n));
            //list.Add(string.Format("      = 2 x (453.9 x 10^4 + 5,626 x 123.7^2)"));
            //list.Add(string.Format("      = 17,792.8 x 10^4 mm4"));
            list.Add(string.Format("      = {0} x ({1} x 10^4 + {2} x {3}^2)", n, Iyy / 10000, A, rxx));
            list.Add(string.Format("      = {0:f0} x 10^4 mm^4", Iyy_dash / 10000));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Ixx_dash = (n * Ixx);

            list.Add(string.Format("But   Ixx’ = {0} x Ixx", n));


            list.Add(string.Format("           = {0} x {1:f0} x 10^4", n, Ixx / 10000));
            //list.Add(string.Format("           = 17,207.2 x 10^4 mm4"));
            list.Add(string.Format("           = {0:f0} x 10^4 mm4", Ixx_dash / 10000));
            list.Add(string.Format(""));

            if (Ixx_dash < Iyy_dash)
            {
                list.Add(string.Format("So, for combined section   Ixx’ < Iyy’"));
                list.Add(string.Format(""));
                list.Add(string.Format("Therefore, rxx’ < ryy’ Hence, the slenderness ratio adopted is satisfactory."));
            }
            else
            {
                list.Add(string.Format("So, for combined section   Ixx’ > Iyy’"));
                list.Add(string.Format(""));
                list.Add(string.Format("Therefore, rxx’ > ryy’ Hence, the slenderness ratio adopted is satisfactory."));

            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("STEP 3:  Design of Battens"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));

            //list.Add(string.Format("Figure 2 (4.30)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Spacing of Battens:"));
            list.Add(string.Format(""));



            //double slender_ratio = d / ryy
            list.Add(string.Format("Let the centre to centre spacing of battens be 'd' mm"));
            list.Add(string.Format("Slenderness ratio of one I- section = d / ryy    of one I section = d / {0:f3}", ryy));



            list.Add(string.Format("The ratio of one I- section between centres of battens shall not exceed the following:"));
            list.Add(string.Format("The slenderness ratio of one I- section between centres of battens shall not exceed the following:"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i) 50"));
            list.Add(string.Format("(ii) 0.7 x lamda (the slenderness ratio of the member as a whole about the X-X axis)."));
            list.Add(string.Format(""));

            double d_1 = 50 * ryy;
            double d_2 = 0.7 * lamda * ryy;

            list.Add(string.Format("From (i)  d / {0:f3} = 50                 d = {1:f3} mm", ryy, d_1));
            list.Add(string.Format("From (ii) d / {0:f3} = 0.7 x {1:f3}         d = {2:f3} mm", ryy, lamda, d_2));
            list.Add(string.Format(""));

            double d = Math.Min(d_1, d_2);

            d = Math.Round(d, 1);

            //list.Add(string.Format("Taking the lower value, provide a spacing         ‘d’ = 560 mm"));
            list.Add(string.Format("Taking the lower value, provide a spacing      ‘d’ = {0:f3} mm", d));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Depth of the Battens:"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));

            double d1_1 = s;
            double d1_2 = 2 * Bf;
            list.Add(string.Format("(a)  End Battens. The effective depth of the end battens   ‘d1’   shall be "));
            list.Add(string.Format("(i)  Not less than the spacing of the two I - sections i.e. not less than   ‘s’ = {0} mm", d1_1));
            list.Add(string.Format("(ii) Not less than twice the width of one I - section i.e. not less than 2 x (Bf = {0}) = {1} mm.", Bf, d1_2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double d1 = Math.Max(d1_1, d1_2);
            list.Add(string.Format("The effective depth is the distance between the extreme rivets in the vertical row."));
            list.Add(string.Format("By taking the higher value, effective depth = {0} mm. ", d1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _d = 20;
            double _c = 30;
            list.Add(string.Format("By using   ‘d’ = {0} mm diameter rivets and providing an effective margin of   ‘c’ = {1} mm. ", _d, _c));
            list.Add(string.Format(""));
            double Db = d1 + 2 * _c;
            //list.Add(string.Format("The total depth of the battens = ‘Db’ = 280 + 2 x 30 = 340 mm"));
            list.Add(string.Format("The total depth of the battens = ‘Db’ = {0} + 2 x {1} = {2} mm", d1, _c, Db));
            list.Add(string.Format(""));
            list.Add(string.Format("(b)  Intermediate battens. The effective depth of an intermediate batten  ‘d1’  shall be "));
            list.Add(string.Format("(i)  Not less than 3/4 x s (spacing of the I - sections) "));

            d1_1 = (3.0 / 4.0) * s;
            //d1_2 = (3.0 / 4.0) * 245;
            //list.Add(string.Format("i.e. Not less than = 3/4 x 245 = 183.75 mm"));
            list.Add(string.Format("i.e. Not less than = 3/4 x {0} = {1:f2} mm", s, d1_1));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Not less than 2 x Bf (the width of one I - section) "));
            list.Add(string.Format("i.e. Not less than = 2 x {0} = {1} mm", Bf, d1_2));

            double eff_dep = Math.Max(d1_1, d1_2);
            list.Add(string.Format("Hence, for the intermediate battens, by taking the higher value, effective depth = {0} mm.", eff_dep));
            list.Add(string.Format(""));

            list.Add(string.Format("Width of Battens :"));
            list.Add(string.Format("------------------"));
            list.Add(string.Format(""));

            double B = s + Bf;
            list.Add(string.Format("This is made equal to the total width of the whole column along the plane of battens,"));
            //list.Add(string.Format("                        = B = 245 + 2 x 70  = 385 mm"));
            list.Add(string.Format("                        = B = {0} + 2 x {1}  = {2} mm", s, Bf, B));
            list.Add(string.Format(""));

            list.Add(string.Format("Thickness of Battens :"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));

            double Vh = 2.5 * P / 100.0;
            list.Add(string.Format("Horizontal shear = Vh = 2.5% of P (the column load)"));
            //list.Add(string.Format("                      = 2.5 x 1350 /100 = 33.75 kN"));
            list.Add(string.Format("                      = 2.5 x {0} /100 = {1:f3} kN", P, Vh));
            list.Add(string.Format(""));


            double allow_shr_strss = 100.0;
            //double allow_shr_strss = iApp.Tables.all;
            //list.Add(string.Format("Allowing a shear stress of 100 N/ Sq. mm"));
            list.Add(string.Format("Allowing a shear stress of {0} N/ Sq. mm", allow_shr_strss));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Va = (Vh / 2) * 1000 / 100.0;
            list.Add(string.Format("Horizontal shear area per plate = Va"));
            //list.Add(string.Format("                                = (Vh / 2) x 1,000 / 100 Sq. mm"));
            //list.Add(string.Format("                                = 16.875 x 1,000 / 100 Sq. mm"));
            //list.Add(string.Format("                                = 168.75  Sq. mm"));
            list.Add(string.Format("                                = (Vh / 2) x 1,000 / 100 Sq. mm"));
            list.Add(string.Format("                                = {0:f3} x 1,000 / 100 Sq. mm", Vh));
            list.Add(string.Format("                                = {0:f3}  Sq. mm", Va));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            tb = Va / B;
            //list.Add(string.Format("Thickness of battens plate = tb = Va / B = 168.75 / 385 = 0.44 mm"));
            list.Add(string.Format("Thickness of battens plate = tb = Va / B = {0:f3} / {1:f3} = {2:f3} mm", Va, B, tb));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Minimum thickness of the batten plate"));
            list.Add(string.Format(""));
            list.Add(string.Format("                         = 1 / 50 x distance between innermost line of rivets"));

            double tb_min = 1.0 / 50.0 * (s - _c - _c);
            list.Add(string.Format(" = 1 / 50 x [{0} - {1} - {1}] = {2} mm", s, _c, tb_min));
            list.Add(string.Format(""));

            //double = 
            if (tb_min < 6)
            {
                list.Add(string.Format("Thickness of the batten plate shall not be less than = tb_min = 6 mm"));
                tb_min = 6;
            }
            list.Add(string.Format(""));
            tb = tb_min;
            list.Add(string.Format("Provide batten plates of thickness = tb = tb_min = {0} mm.", tb));
            list.Add(string.Format(""));
            list.Add(string.Format("Next, we shall check the batten plates for longitudinal shear and bending moment."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check for longitudinal shear : "));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double F1 = Vh * 1000 * d / (n * s);
            list.Add(string.Format("Longitudinal shear per batten plate = F1 = S x d / (n x s)"));
            list.Add(string.Format("                                         = Vh x 1000 x d / (n x s)"));
            //list.Add(string.Format("                                         = 33.75 x 1,000 x 560 / (2 x 245)"));
            list.Add(string.Format("                                         = {0:f3} x 1,000 x {1} / ({2} x {3})", Vh, d, n, s));
            list.Add(string.Format("                                         = {0:f3} N", F1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Vstrs = F1 / (Db * tb);
            list.Add(string.Format("Vertical shear stress induced = F1 / (Db x tb) "));
            list.Add(string.Format("                              = {0:f3} / ({1}  x {2} )", F1, Db, tb));
            if (Vstrs < allow_shr_strss)
                list.Add(string.Format("                              = {0:f3} N / Sq. mm (less than {1} N/mm^2)", Vstrs, allow_shr_strss));
            else
                list.Add(string.Format("                              = {0:f3} N / Sq. mm (greater than {1} N/mm^2)", Vstrs, allow_shr_strss));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Check  for bending moment :"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));

            double _M = Vh * 1000 * d / (2 * n);
            list.Add(string.Format("B.M. per batten plate = M = S x d / (2 x n)"));
            list.Add(string.Format("                          = Vh x 1000 x d / (2 x n) "));
            list.Add(string.Format("                          = {0:f3} x 1,000 x {1} / 2 x {2}  Nmm", Vh, d, n));
            list.Add(string.Format("                          = {0:f3} N-mm", _M));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Let  ‘f’ be the bending stress induced"));
            list.Add(string.Format("Equating the moment of resistance to the bending moment,"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("M’ = (1 / 6) x f x tb x Db^2 = (1 / 6) x f x 6 x 340^2"));



            list.Add(string.Format(""));
            //list.Add(string.Format("Therefore, M’ = M,    i.e, (1 / 6) x f x 6 x 340^2 = 47,25,000"));
            list.Add(string.Format("Therefore, M’ = M,    i.e, (1 / 6) x f x {0} x {1}^2 = {2}", tb, Db, _M));
            list.Add(string.Format(""));

            double f = _M * 6 / (tb * Db * Db);


            //double M_dash = (1./6.0)*

            //list.Add(string.Format("f  = 47,25,000 x 6 / (6 x 340^2) = 40.9 N / Sq.mm (less than 165 N / mm)"));

            if (f < Pms)
            {
                list.Add(string.Format("f  = {0} x 6 / ({1} x {2}^2) = {3:f3} N / Sq.mm (less than {4} N / mm)", _M, tb, Db, f, Pms));
            }
            else
            {
                list.Add(string.Format("f  = {0} x 6 / ({1} x {2}^2) = {3:f3} N / Sq.mm (greater than {4} N / mm)", _M, tb, Db, f, Pms));
            }
            list.Add(string.Format(""));


            list.Add(string.Format("Connection of the battens to the I - sections"));
            list.Add(string.Format(""));

            //list.Add(string.Format("Longitudinal shear = F1 = 38,571 N "));
            //list.Add(string.Format("            Moment = M = 47,25,000 Nmm"));
            list.Add(string.Format("Longitudinal shear = F1 = {0:f3} N ", F1));
            //list.Add(string.Format("            Moment = M = 47,25,000 Nmm", M));
            list.Add(string.Format("            Moment = M = {0:f3} N-mm", _M));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            //list.Add(string.Format("Provide Nr = 6 rivets for connecting to each flange.", Nr));
            list.Add(string.Format("Provide Nr = {0} rivets for connecting to each flange.", Nr));
            list.Add(string.Format(""));


            double thk1 = 40;
            //Ss = Nr * (40 * 40) + 4 * (140 * 140);
            double Ss = Nr * (thk1 * thk1) + 4 * (Bf * Bf);
            //list.Add(string.Format("Σx^2  + Σy^2 with respect to G,  Ss = 6 x 40^2 + 4 x 140^2 = 88,000 mm^2"));
            list.Add(string.Format("Σx^2  + Σy^2 with respect to G,  Ss = {0} x {1}^2 + 4 x {2}^2 = {3:f2} mm^2", Nr, thk1, Bf, Ss));
            list.Add(string.Format(""));
            //list.Add(string.Format(" K = M / Ss = 47,25,000 / 88,000 N/mm"));
            //list.Add(string.Format("   = 53.69 N/mm"));
            double K = _M / Ss;
            list.Add(string.Format(" K = M / Ss = {0:f3} / {1:f3} N/mm", _M, Ss));
            list.Add(string.Format("            = {0:f3} N/mm", K));


            list.Add(string.Format(""));

            list.Add(string.Format("Consider the rivet marked A,"));

            double Rt = F1 / Nr;
            //list.Add(string.Format("Resistance against translation = Rt = F1 / Nr = 38,571 / 6 = 6,428.5 N"));
            list.Add(string.Format("Resistance against translation = Rt = F1 / Nr = {0:f3} / {1} = {2:f3} N", F1, Nr, Rt));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Resistance against rotation (torsional shear) = Sa "));
            list.Add(string.Format("                                              = K x ra"));
            list.Add(string.Format("                                              = {0:f3} x ra", K));
            list.Add(string.Format(""));

            list.Add(string.Format("Total Vertical component = V = Rt + K x (ra x sin θ)"));
            //list.Add(string.Format("                             = 6,428.5 + 53.69 x (ra x sin θ)"));
            list.Add(string.Format("                             = {0:f3} + {1:f3} x (ra x sin θ)", Rt, K));
            list.Add(string.Format(""));
            //V = Rt + K * (ra * sin θ)
            double _V = Rt + K * thk1;
            //list.Add(string.Format("                             = {0:f3} + 53.69 x 40 = 8,576.1 N"));
            list.Add(string.Format("                             = {0:f3} + {1:f3} x {2:f0} = {3:f3} N", Rt, K, thk1, _V));
            list.Add(string.Format(""));
            //H = K * (ra * cos θ)
            double H = (K * Bf);
            list.Add(string.Format("Horizontal component = H = K x (ra x cos θ)"));
            list.Add(string.Format("                         = {0:f3} x (ra x cos θ)", K));
            list.Add(string.Format("                         = {0:f3} x {1:f0}", K, Bf));
            list.Add(string.Format("                         = {0:f3} N", H));
            list.Add(string.Format(""));


            A = Math.Sqrt(_V * _V + H * H);
            list.Add(string.Format("Resultant resistance offered by the rivet A, = SQRT(V^2 + H^2)"));
            list.Add(string.Format("                                             = SQRT ({0:f3}^2 + {1:f3}^2)", _V, H));
            list.Add(string.Format("                                             = {0:f3} N", A));

            double Dr1 = Dr + 1.5;

            //list.Add(string.Format("Diameter of rivets = Dr = 20 mm,  Therefore, Dr1 = 20 + 1.5 = 21.5 mm"));
            list.Add(string.Format("Diameter of rivets = Dr = {0} mm,  Therefore, Dr1 = {0} + 1.5 = {1} mm", Dr, Dr1));

            double stngth_single = fs * (Math.PI * Dr1 * Dr1 / 4);


            //list.Add(string.Format("Strength of a rivet in single shear = fs x 3.1415 x (d^2 / 4) = 100 x 3.1415 x 21.5^2 / 4 = 36,305 N"));
            list.Add(string.Format("Strength of a rivet in single shear = fs x 3.1416 x (d^2 / 4)"));
            list.Add(string.Format("                                    = {0} x 3.1415 x {1}^2 / 4", fs, Dr1));
            list.Add(string.Format("                                    = {0:f3} N", stngth_single));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double brng_strn = fb * Dr1 * tb;
            list.Add(string.Format("Strength of a rivet in bearing = fb x Dr1 x tb"));
            //list.Add(string.Format("                               = 300 x 21.5 x 6"));
            list.Add(string.Format("                               = {0} x {1} x {2}", fb, Dr1, tb));
            list.Add(string.Format("                               = {0:f3} N", brng_strn));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double sf_ld = Math.Min(stngth_single, brng_strn);


            //list.Add(string.Format("Safe load per rivet = Minimum of the above two values = 36,305 N > 11,404 N,  Hence OK."));
            if (sf_ld >= A) list.Add(string.Format("Safe load per rivet = Minimum of the above two values = {0:f3} N > {1:f3} N,  Hence OK.", sf_ld, A));
            else
            {
                IS_DESIGN_OK = false;
                list.Add(string.Format("Safe load per rivet = Minimum of the above two values = {0:f3} N < {1:f3} N,  Hence NOT OK.", sf_ld, A));
            }



            double no_face = ((int)(l * 1000 / d) + 1);
            list.Add(string.Format("Total number of battens per face = Length of the column / Spacing of battens + 1"));
            list.Add(string.Format("                                 = (l x 1000 / d) + 1"));
            //list.Add(string.Format("                                 = 4,000 / 560 + 1 = 9"));
            list.Add(string.Format("                                 = {0} * 1000 / {1} + 1", l, d));
            list.Add(string.Format("                                 = {0}", no_face));


            double tot_bat = 2 * no_face;

            list.Add(string.Format("Hence, provide {0} battens in each side face, therefore, total 2 x {0} = {1} battens in two side faces.", no_face, tot_bat));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4:  Design of flange splices at two joining sections"));
            list.Add(string.Format("------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Design 91. An ISHB 250 x 54.7 kg/m column has to be spliced at a section."));
            list.Add(string.Format("An ISHB 250 x 54.7 kg/m column has to be spliced at a section."));
            list.Add(string.Format("The axial load on the column is 500 kN, the horizontal shear at the splice "));
            list.Add(string.Format("section is 40 kN, and the bending moment at the splice section is 50 kN-m."));
            list.Add(string.Format("Assume that the column ends are faced for complete bearing. Design the column splice."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Maximum Compressive Load  = P = 1350 kN (500)"));
            list.Add(string.Format("Maximum Compressive Load  = P = {0} kN (500)", P));
            //list.Add(string.Format("Maximum Bending Moment = M = 50 kN-m "));
            list.Add(string.Format("Maximum Bending Moment = M = {0} kN-m ", M));
            //list.Add(string.Format("Maximum Shear Force = V = 40 kN "));
            list.Add(string.Format("Maximum Shear Force = V = {0} kN ", V));
            list.Add(string.Format(""));

            //P = 1350;
            //M = 50;
            //V = 40;

            list.Add(string.Format("Solution. In this case we will provide flange and web splices."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of the flange splices"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Load transmitted to flange splices from column"));
            list.Add(string.Format(""));

            double P1 = 0.5 * P;
            //list.Add(string.Format("P1 = 50% of the column load = (50/100) x 1350 = 675 kN"));
            list.Add(string.Format("P1 = 50% of the column load = (50/100) x {0} = {1} kN", P, P1));
            list.Add(string.Format(""));

            double P2 = P1 / 2;
            //list.Add(string.Format("Load on one splice plate = P2 = 675 / 2 = 337.5 kN"));
            list.Add(string.Format("Load on one splice plate = P2 = {0} / 2 = {1:f2} kN", P1, P2));
            list.Add(string.Format(""));

            double P3 = M / (h / 1000);
            list.Add(string.Format("Load transmitted to one splice plate due to moment,"));
            list.Add(string.Format("P3 = M/(h/1000) "));
            //list.Add(string.Format("   = 50 / (300/1000) = 50 / 0.30 = 166.67 kN"));
            list.Add(string.Format("   = {0} / ({1:f2}/1000)", M, h));
            list.Add(string.Format("   = {0:f2} kN", P3));
            list.Add(string.Format(""));

            double P4 = P2 + P3;
            list.Add(string.Format("Maximum load on one splice plate"));
            list.Add(string.Format(" P4 = P2 + P3"));
            //list.Add(string.Format("    = 337.5 + 166.67 = 504.17 kN"));
            list.Add(string.Format("    = {0:f3} + {1:f3}", P2, P3));
            list.Add(string.Format("    = {0:f3} kN", P4));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("Safe compressive stress for splice plate = Ps = 150 N/mm^2", ps));
            list.Add(string.Format("Safe compressive stress for splice plate = Ps = {0} N/mm^2", Ps));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double area = P4 * 1000 / Ps;
            list.Add(string.Format("Area of one splice plate = P4 x 1000 / Ps = {0:f2} x 1000 / {1} = {2:f2} Sq.mm", P4, Ps, area));
            list.Add(string.Format(""));

            double wd1 = 2 * Bf;
            list.Add(string.Format("Width of the splice plate = width of flange of the column = 2 x Bf = 2 x {0} = {1} mm.", Bf, wd1));
            list.Add(string.Format(""));

            double t2 = (int)(area / wd1);
            //list.Add(string.Format("Thickness of the splice plate = 3361.13 / 280 = 12 mm"));
            list.Add(string.Format("Thickness of the splice plate = {0:f3} / {1} = {2} mm", area, wd1, t2));
            list.Add(string.Format(""));


            //list.Add(string.Format("Provide {0} mm thick splice plates, t2 = {1} mm.", wd1, t2));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide {0} mm thick splice plates, t2 = {0} mm.", t2));


            double Dr2 = Dr + 1.5;
            //list.Add(string.Format("Provide 20 mm diameter rivets, with 1.5 mm clearance, diameter = Dr2 = 20 + 1.5 = 21.5 mm."));
            list.Add(string.Format("Provide {0} mm diameter rivets, with 1.5 mm clearance, diameter = Dr2 = {0} + 1.5 = {1} mm.", Dr, Dr2));
            list.Add(string.Format(""));


            double V1 = fs * Math.PI * Dr2 * Dr2 / 4;
            list.Add(string.Format("Strength of a rivet in single shear   = fs x 3.1416  x Dr2^2 / 4 "));
            //list.Add(string.Format("                                      = 100 x 3.1416 x 21.5^2/4 = 36,356 N"));
            list.Add(string.Format("                                      = {0} x 3.1416 x {1}^2/4", fs, Dr2));
            list.Add(string.Format("                                      = {0:f0} N", V1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double V2 = fb * Dr2 * t2;
            list.Add(string.Format("Strength of a rivet in bearing  = fb x Dr2 x t2 "));
            list.Add(string.Format("                                = {0} x {1} x {2}", fb, Dr2, t2));
            list.Add(string.Format("                                = {0:f3} N", V2));
            list.Add(string.Format(""));

            double Pr = Math.Min(V1, V2);

            //list.Add(string.Format("Safe load per rivet = Pr = 36,356 N"));
            list.Add(string.Format("Safe load per rivet = Pr = {0:f3} N", Pr));
            list.Add(string.Format(""));
            list.Add(string.Format("Number of rivets required on each side of the splice section = P4 x 1000 / Pr"));
            //list.Add(string.Format("                                                             = 504.17 x 103 / 36,356 = 13.8 say 16 rivets"));

            double rvt_nos = ((int)(P4 * 1000 / Pr) + 1);

            if (rvt_nos % 2 != 0.0) rvt_nos = rvt_nos + 1;

            list.Add(string.Format("                                                             = {0:f3} x 10^3 / {1:f3}", P4, Pr, rvt_nos));

            list.Add(string.Format("                                                             = {0:f3} say {1} rivets", (P4 * 1000 / Pr), rvt_nos));
            list.Add(string.Format(""));


            list.Add(string.Format("Provide {0} rivets arranged in 4 horizontal rows with 4 (fixed) rivets per row, so 4 vertical rows.", rvt_nos));
            //list.Add(string.Format("(In case of {0} rivets arrange in 2 horizontal rows with 4 (fixed) rivets per row, so 4 vertical rows.)", (rvt_nos / 2)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Web Splice."));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("Two splice plates are provided one on either side of the web."));
            //list.Add(string.Format("Horizontal shear transmitted to splice plates = 40 kN"));
            list.Add(string.Format("Horizontal shear transmitted to splice plates = {0} kN", V));
            //list.Add(string.Format("Safe average shear stress = 100 N/Sq.mm"));


            double AVG_Shear_Stress = allow_shr_strss;

            list.Add(string.Format("Safe average shear stress = {0} N/Sq.mm", allow_shr_strss));

            double area_shr_plt = V * 1000 / AVG_Shear_Stress;
            list.Add(string.Format("Area of shear plates {0} x 1,000 / {1} = {2} Sq.mm", V, AVG_Shear_Stress, area_shr_plt));

            double area_each_shr_plt = area_shr_plt / 2.0;
            list.Add(string.Format("Area of each shear plate = {0} Sq.mm", area_each_shr_plt));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("Provide 4 vertical rows of rivets"));

            double sp_row = 40;
            double Prv_sp_row = 35;
            //list.Add(string.Format("Let, the spacing between the two rows be 40 mm"));
            //list.Add(string.Format("Providing an effective margin of 35 mm."));
            //list.Add(string.Format("Width of the splice plates = 40 x (4-1) + 35 + 35 = 120 +35 + 35 = 190 mm"));

            list.Add(string.Format("Let, the spacing between the two rows be {0} mm", sp_row));
            list.Add(string.Format("Providing an effective margin of {0} mm.", Prv_sp_row));

            double wd_sp_plt = sp_row * (4 - 1) + Prv_sp_row + Prv_sp_row;
            list.Add(string.Format("Width of the splice plates = {0} x (4-1) + {1} + {1} = {2} mm", sp_row, Prv_sp_row, wd_sp_plt));

            double thk_shr_plt = area_each_shr_plt / wd_sp_plt;

            //list.Add(string.Format("Thickness of shear plates = 200 / 190 = 1.05 mm"));
            list.Add(string.Format("Thickness of shear plates = {0:f3} / {1:f3} = {2:f3} mm", area_each_shr_plt, wd_sp_plt, thk_shr_plt));

            if (thk_shr_plt < 6)
            {
                thk_shr_plt = 6.0;
            }
            list.Add(string.Format("Provide a thickness of {0} mm for the Splice Plates from shear criterion. ", thk_shr_plt));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of the connection "));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double rvt1_nos = 4;
            double rvt1_ecc = 20;

            double rvt2_nos = 4;
            double rvt2_ecc = 60;

            list.Add(string.Format("Let us provide all 16 rivets with 8 rivets on each side of the splice. "));
            list.Add(string.Format("Figure 4  shows the proposed arrangement. Consider the rivets on first "));
            list.Add(string.Format("row of 4 rivets at an eccentricity of 20 mm and second row of 4 rivets at an "));
            list.Add(string.Format("eccentricity of 60 mm on upper and lower halves of the joint."));
            Rt = V * 1000 / 8;

            //list.Add(string.Format("Resistance against translation = Rt = 40 x 10^3 / 8 = 5,000 N"));
            list.Add(string.Format("Resistance against translation = Rt = {0} x 10^3 / 8 = {1:f3} N", V, Rt));
            list.Add(string.Format(""));
            list.Add(string.Format("Resistance against rotation (Torsional shear) = K x ra"));
            list.Add(string.Format("Where,    K = P.e / Σx^2 + Σy^2"));
            list.Add(string.Format(""));

            double area_horz = (rvt1_nos * (rvt1_ecc * rvt1_ecc + rvt1_ecc * rvt1_ecc)
                                + rvt2_nos * (rvt2_ecc * rvt2_ecc + rvt1_ecc * rvt1_ecc)
                                + rvt1_nos * (rvt1_ecc * rvt1_ecc + rvt2_ecc * rvt2_ecc)
                                + rvt2_nos * (rvt2_ecc * rvt2_ecc + rvt2_ecc * rvt2_ecc)) / 4;


            list.Add(string.Format("Measuring horizontal (x) and vertical (y) distances from the joint, to the rivets, above and below the joint,"));
            //list.Add(string.Format("        Σx^2 + Σy^2 = 4 x (20^2 + 20^2) + 4 x (60^2 + 20^2) + 4 x (20^2 + 60^2) + 4 x (60^2 + 60^2) = 16,000 Sq. mm"));
            list.Add(string.Format("        Σx^2 + Σy^2 = ({0} x ({1}^2 + {1}^2) + {2} x ({3}^2 + {1}^2) + {1} x ({0}^2 + {3}^2) + {2} x ({3}^2 + {3}^2))/4",
                rvt1_nos, rvt1_ecc, rvt2_nos, rvt2_ecc));
            //list.Add(string.Format("                    = 16,000 Sq. mm"));
            list.Add(string.Format("                    = {0:f3} Sq. mm", area_horz));
            list.Add(string.Format(""));


            K = V * 1000 * V / area_horz;
            //list.Add(string.Format("K = 40 x 10^3 x 40 /16,000 = 100 N/mm"));
            list.Add(string.Format("K = {0} x 10^3 x {0} / {1:f3} = {2:f3} N/mm", V, area_horz, K));
            list.Add(string.Format(""));

            list.Add(string.Format("Resistance against rotation = 100 x ra"));
            list.Add(string.Format(""));

            thk1 = 40;
            H = (Rt + K * thk1);//??
            list.Add(string.Format("Total horizontal component = H = Rt + K x (r x  cos θ)"));
            //list.Add(string.Format("                               = 5000 + 100 x 40 = 9000 N"));
            list.Add(string.Format("                               = {0} + {1} x {2}", Rt, K, thk1));
            list.Add(string.Format("                               = {0:f3} N", H));
            list.Add(string.Format(""));


            //V = = K x (r x sin θ)

            V1 = K * thk1;
            list.Add(string.Format("Vertical component = V = K x (r x sin θ)"));
            //list.Add(string.Format("                       = 100 x 40 = 4000 N"));
            list.Add(string.Format("                       = {0} x {1}", K, thk1));
            list.Add(string.Format("                       = {0:f3} N", V1));
            list.Add(string.Format(""));


            double R = Math.Sqrt(H * H + V1 * V1);
            //list.Add(string.Format("Resultant resistance = √(9000^2 + 4000^2) = 9848.86 N"));
            list.Add(string.Format("Resultant resistance = √({0:f3}^2 + {1:f3}^2) = {2:f3} N", H, V1, R));
            list.Add(string.Format(""));

            double bar_dia = 16.0;
            double Dr3 = bar_dia + 1.5;
            //list.Add(string.Format("Provide 16 mm diameter rivets, allowing 1.5 mm clearance, Dr3 = 16+1.5 = 17.5 mm,"));
            list.Add(string.Format("Provide {0} mm diameter rivets, allowing 1.5 mm clearance, Dr3 = {0} + 1.5 = {1} mm,", bar_dia, Dr3));
            list.Add(string.Format(""));


            double Rvt_dbl_shr = 2 * fs * Math.PI * Dr3 * Dr3 / 4;
            list.Add(string.Format("Rivet value in double shear = 2 x fs x π x Dr3^2 / 4"));
            //list.Add(string.Format("                            = 2 x 100 x  3.1416 x 17.5^2 / 4"));
            list.Add(string.Format("                            = 2 x {0} x  3.1416 x {1}^2 / 4", fs, Dr3));
            list.Add(string.Format("                            = {0:f3} N", Rvt_dbl_shr));
            list.Add(string.Format(""));


            double Rvt_Brg = fb * Dr3 * tw;
            list.Add(string.Format("Rivet value in bearing = fb x Dr3 x tw"));
            //list.Add(string.Format("                       = 300 x 17.5 x 8.8 (Thickness of web = tw = 8.8 mm)"));
            //list.Add(string.Format("                       = 46,200 N"));
            list.Add(string.Format("                       = {0} x {1} x {2} (Thickness of web = tw = {2} mm)", fb, Dr3, tw));
            list.Add(string.Format("                       = {0:f3} N", Rvt_Brg));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double safe_load = Math.Min(Rvt_dbl_shr, Rvt_Brg);
            list.Add(string.Format("Safe load per rivet = {0} N (The lower of the above two values)", safe_load));

            if (safe_load >= R) list.Add(string.Format("Hence, the design in safe, OK"));
            else list.Add(string.Format("Hence, the design in unsafe, NOT OK"));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5:  Design of the base plate for the column"));
            list.Add(string.Format("-------------------------------------------------"));
            list.Add(string.Format(""));

            double W = 600.0;

            //D 
            list.Add(string.Format(""));
            //list.Add(string.Format("Column load = W = 600 kN"));
            //list.Add(string.Format("Eccentricity = e = 100 mm"));
            //list.Add(string.Format("Depth of the column section = 300 mm"));

            //list.Add(string.Format("Providing a cantilever projection of 200 mm beyond the flanges, length of the base plate"));
            //list.Add(string.Format("      = I = 300 + 200 + 200 = 700 mm"));
            //list.Add(string.Format("      = e / I = 100 / 700 = 0.14"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        This is not in between 1/6 = 0.167 and 1/3 = 0.333"));
            //list.Add(string.Format("        Distance from the load line to the extreme compression edge"));
            //list.Add(string.Format("                                = I / 2 - e = 700 / 2 - 100 = 250 mm"));
            //list.Add(string.Format("Length of the pressure diagram = X = 3 x 250 = 750 mm"));
            //list.Add(string.Format("Let the maximum compressive pressure reach 4 N/mm2."));
            //list.Add(string.Format("Let the width of the base plate be b mm."));
            //list.Add(string.Format("Equating the upward reaction to the downward load."));
            //list.Add(string.Format("                1/ 2 x 750 x 4 x b = 1350 x 1,000"));
            //list.Add(string.Format("b = 900 mm"));
            //list.Add(string.Format("Provide a width of 900 mm."));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Let the actual maximum upward pressure intensity be p N / mm2"));
            //list.Add(string.Format("1 / 2 x 750 x p x 900 = 1350 x 1,000                                                 "));
            //list.Add(string.Format("P = 4  N/mm2"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Figure 5 (5.13) shows the pressure distribution diagram below the base plate."));
            //list.Add(string.Format(" "));
            //list.Add(string.Format("Figure 5 (5.13)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Provide 700 mm x 900 mm x 35 mm Base Plate."));

            double D = h;
            list.Add(string.Format("Column load = W = {0} kN", W));
            list.Add(string.Format("Eccentricity = e = {0} mm", e));
            list.Add(string.Format("Depth of the column section = {0} mm", D));
            list.Add(string.Format(""));

            double cant_proj = 200.0;
            list.Add(string.Format("Providing a cantilever projection of 200 mm beyond the flanges, length of the base plate"));

            double I = D + cant_proj + cant_proj;
            list.Add(string.Format("      = I = {0} + {1} + {1} = {2} mm", D, cant_proj, I));
            list.Add(string.Format(""));

            double e_by_I = e / I;
            list.Add(string.Format("      = e / I = {0} / {1} = {2:f3}", e, I, e_by_I));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("This is not in between 1/6 = 0.167 and 1/3 = 0.333"));
            list.Add(string.Format("Distance from the load line to the extreme compression edge"));
            list.Add(string.Format(""));

            double ext_cmp_ed = I / 2 - e;
            list.Add(string.Format("     = I / 2 - e = {0} / 2 - {1} = {2} mm", I, e, ext_cmp_ed));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double X = 3 * ext_cmp_ed;
            list.Add(string.Format("Length of the pressure diagram = X = 3 x {0} = {1} mm", ext_cmp_ed, X));
            list.Add(string.Format(""));

            double max_comp_prs = 4;

            list.Add(string.Format("Let the maximum compressive pressure reach {0} N/Sq.mm.", max_comp_prs));
            list.Add(string.Format(""));
            list.Add(string.Format("Let the width of the base plate be 'b' mm."));
            list.Add(string.Format("Equating the upward reaction to the downward load."));
            list.Add(string.Format(""));
            //list.Add(string.Format("    1/ 2 x 750 x 4 x b = 1350 x 1,000"));

            double b = 2 * (P * 1000) / (X * max_comp_prs);
            list.Add(string.Format("    1/ 2 x X x 4 x b = P x 1,000"));
            list.Add(string.Format(""));
            list.Add(string.Format("                     b = 2 x ({0} x 1000)/({1} x {2})", P, X, max_comp_prs));
            list.Add(string.Format("                     b = {0:f0} mm", b));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide a width of {0:f0} mm.", b));
            list.Add(string.Format(""));
            list.Add(string.Format("Let the actual maximum upward pressure intensity be 'p' N / mm2"));
            list.Add(string.Format(""));
            list.Add(string.Format("1 / 2 x 750 x p x 900 = 1350 x 1,000"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("P = 4 N/Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Figure 5 (5.13) shows the pressure distribution diagram below the base plate."));
            list.Add(string.Format(" "));
            //list.Add(string.Format("Figure 5 (5.13)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide 700 mm x 900 mm x 35 mm Base Plate."));


            //File.WriteAllLines(Report_File, list.ToArray());

            return list;

        }

        public void Calculate_Program()
        {
            //Design_Individual_Program
            File.WriteAllLines(Get_Report_File(), Design_Individual_Program().ToArray());

        }
        public string Report_File { get; set; }

        public bool IS_DESIGN_OK { get; set; }

        public string Get_Report_File()
        {
            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            //tmp_file = Path.Combine(tmp_file, "DESIGN_STEEL_COLUMN_" + step.ToString("000") + ".TXT");
            tmp_file = Path.Combine(tmp_file, "DESIGN_STEEL_COLUMN_" + ColumnNo + ".TXT");
            return tmp_file;
        }
        public string Get_BOQ_File(int step)
        {
            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "BOQ");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "BOQ_C" + step.ToString("000") + ".TXT");

            return tmp_file;
        }

        public string Get_Design_Summary_File(int step)
        {
            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "DESIGN SUMMARY");

            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            tmp_file = Path.Combine(tmp_file, "DESIGN_SUMMARY_C" + step.ToString("000") + ".TXT");

            return tmp_file;
        }

        public string File_Design_Summary
        {
            get
            {
                string des_sum = Path.Combine(Path.GetDirectoryName(Get_Report_File()), "COLUMN_DESIGN_SUMMARY.TXT");
                return des_sum;
            }
        }


        public object bar_nos { get; set; }
    }

    public class SteelBeamDesign
    {
        IApplication iApp;

        public double l, a, M, V, Pms, Pss, Pbs, h, tw, Ixx, h1, h2, w, Z;
        public string sectionName = "ISLB 400";

        public SteelBeamDesign(IApplication iapp)
        {
            iapp = iApp;

            l = 8.0;
            a = 300;
            M = 151.13;
            V = 70.2;
            Pms = 165;
            Pss = 100;
            Pbs = 187.5;

            h = 400; tw = 8; Ixx = 19306.3; h1 = 306.2; h2 = 31.9; w = 56.9; Z = 965.3;
        }
        public void Calculate_Program()
        {
            List<string> list = new List<string>();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design of Steel Beam "));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("User’s Input data :"));
            list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));

            //double l, a, M, V, Pms, Pss, Pbs, h, tw, Ixx, h1, h2, w, Z;

            IS_DESIGN_OK = true;

            list.Add(string.Format("Span = l = {0} m.", l));
            list.Add(string.Format("Bearing at each end = a = {0} mm.", a));
            list.Add(string.Format("Maximum Bending Moment  = M = {0} kN-m", M));
            list.Add(string.Format("Maximum Shear Force = V = {0} kN", V));
            list.Add(string.Format("Permissible Bending Stress = Pms = {0} N/Sq.mm", Pms));
            list.Add(string.Format("Permissible Shear Stress = Pss = {0} N/Sq.mm", Pss));
            list.Add(string.Format("Permissible Bearing Stress = Pbs = {0} N/Sq.mm", Pbs));
            list.Add(string.Format("Selected Steel Section = {0}", sectionName));
            list.Add(string.Format("(Overall depth = h = {0} mm, tw = {1} mm, Ixx = {2} x 10000 Sq. Sq. mm,", h, tw, Ixx / 10000));
            list.Add(string.Format(" h1 = {0} mm, h2 = {1} mm, w = {2} kg/m, Z = {3} x 1000 cu.mm)", h1, h2, w, Z / 1000));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Calculations :"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Check for Section Modulus"));
            list.Add(string.Format("----------------------------------"));
            list.Add(string.Format(""));

            double req_mod1 = M * 1000000 / Pms;
            //list.Add(string.Format("Required Section Modulus = M x 1000000 / Pms = 151.13 x 1000000 / 165 = 915.94 x 1000 cu.mm"));
            list.Add(string.Format("Required Section Modulus = M x 10^6 / Pms = {0} x 10^6 / {1} = {2:f2} x 1000 cu.mm", M, Pms, req_mod1 / 1000));
            if (Z >= req_mod1)
            {
                //list.Add(string.Format("Provided Section Modulus = 965.3 x 1000 cu.mm > 915.94 x 1000         Hence OK."));
                list.Add(string.Format("Provided Section Modulus = {0:f2} x 1000 cu.mm > {1:f2} x 1000         Hence OK.", Z / 1000, req_mod1 / 1000));
            }
            else
            {
                list.Add(string.Format("Provided Section Modulus = {0:f2} x 1000 cu.mm < {1:f2} x 1000         Hence NOT OK.", Z / 1000, req_mod1 / 1000));
                IS_DESIGN_OK = false;
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : Check for Shear Stress"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));

            double ISS = V * 1000 / (h * tw);
            //list.Add(string.Format("Induced Shear Stress = V  x 1000 / (h x tw) = 70.2 x 1000 / (400 x 8) = 21.93 N/Sq.mm "));
            list.Add(string.Format("Induced Shear Stress = V  x 1000 / (h x tw) = {0:f2} x 1000 / ({1} x {2}) = {3:f2} N/Sq.mm ", V, h, tw, ISS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("Permissible Shear Stress = 100 N/Sq.mm  > 21.93 Sq.mm  Hence OK."));
            if (Pss > ISS)
            {
                list.Add(string.Format("Permissible Shear Stress = {0} N/Sq.mm  > {1} Sq.mm  Hence OK.", Pss, ISS));
            }
            else
            {
                list.Add(string.Format("Permissible Shear Stress = {0:f2} N/Sq.mm  < {1} Sq.mm  Hence NOT OK.", Pss, ISS));
                IS_DESIGN_OK = false;
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3 : Check for Web Crippling at Support "));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double IBS = V * 1000 / (tw * (a + h2 * Math.Sqrt(3)));
            list.Add(string.Format("Induced Bearing Stress = V x 1000 / [tw x (a + h2 x sqrt (3))]"));
            //list.Add(string.Format("                       = 70.2 x 1000 / [8 x (300 + 31.9 x sqrt (3))]"));
            list.Add(string.Format("                       = {0} x 1000 / [{1} x ({2} + {3} x sqrt(3))]", V, tw, a, h2));
            //list.Add(string.Format("                       = 24.70 N/Sq.mm  "));
            list.Add(string.Format("                       = {0:f3} N/Sq.mm", IBS));
            list.Add(string.Format(""));
            if (Pbs >= IBS) list.Add(string.Format("Permissible Bearing Stress = {0:f3} N/Sq.mm  > {1:f3} Sq.mm,   Hence OK.", Pbs, IBS));
            else list.Add(string.Format("Permissible Bearing Stress = {0:f3} N/Sq.mm  < {1:f3} Sq.mm,   Hence NOT OK.", Pbs, IBS));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4 : Check for Buckling of the Web"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Check for Buckling of the Web:"));
            list.Add(string.Format(""));


            double SRWB = h1 * Math.Sqrt(3) / l;
            list.Add(string.Format("Slenderness Ratio for Web Buckling = h1 x sqrt(3) / l"));
            //list.Add(string.Format("                                   = 336.2 x sqrt(3) / 8"));
            list.Add(string.Format("                                   = {0:f3} x {1:f3} / {2}", h1, Math.Sqrt(3), l));
            list.Add(string.Format("                                   = {0:f3}", SRWB));
            list.Add(string.Format(""));

            double Sigma_ac = 110;
            //list.Add(string.Format("Corresponding Safe Compressive Stress  = Sigma_ac = 110 N/Sq.mm"));
            list.Add(string.Format("Corresponding Safe Compressive Stress  = Sigma_ac = {0} N/Sq.mm", Sigma_ac));
            list.Add(string.Format(""));
            list.Add(string.Format("(Refer to Table for “Permissible_Bending_Compressive_Stresses”)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double B = a + h / 2;
            //list.Add(string.Format("B = a + h/2 = 300 + 400/2 = 500 mm."));
            list.Add(string.Format("B = a + h/2 = {0} + {1}/2 = {2} mm.", a, h, B));
            list.Add(string.Format(""));

            double LBCW = (Sigma_ac * tw * B) / 1000;
            list.Add(string.Format("Load Bearing Capacity of Web = Sigma_ac x tw x B"));
            //list.Add(string.Format("                             = 110 x 8 x 500 "));
            list.Add(string.Format("                             = {0} x {1} x {2} ", Sigma_ac, tw, B));
            //list.Add(string.Format("                             = 4,40,000 N"));
            list.Add(string.Format("                             = {0:f2} x 1000 N", LBCW));

            if (LBCW >= V)
            {
                list.Add(string.Format("                             = {0:f2} kN > V = {1:f2} kN   Hence OK.", LBCW, V));
            }
            else
            {
                list.Add(string.Format("                             = {0:f2} kN < V = {1:f2} kN   Hence NOT OK.", LBCW, V));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5 : Check for Deflection"));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Deflection = (5/384) x (w x l^4/EI)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Where, w = load per metre length, l = span, E = Elastic Modulus and I = Moment of Inertia of Beam Section."));
            list.Add(string.Format(""));

            double max_defl = 4.85;
            list.Add(string.Format("Maximum Deflection Obtained from Analysis Result (for Beams with 8 m span) = {0:f5} mm.", max_defl));
            list.Add(string.Format(""));

            double eff_span = l + 2 * a / (2 * 1000);
            //list.Add(string.Format("Effective Span = span + 2 x a / (2 x 1000) = 8.0 + 0.300 = 8.3 m."));
            list.Add(string.Format("Effective Span = span + 2 x a / (2 x 1000) = {0} + {1:f3} = {2:f3} m.", l, 2 * a / (2 * 1000), eff_span));
            list.Add(string.Format(""));
            list.Add(string.Format("Permissible Deflection  = Effective Span x 1000 / 325 "));

            double PD = eff_span * 1000 / 325;
            //list.Add(string.Format("                        = 8.3 x 1000 / 325"));
            list.Add(string.Format("                        = {0:f3} x 1000 / 325", eff_span));
            //list.Add(string.Format("                        = 25.5 mm > Maximum Deflection = 8.3 mm    Hence OK"));

            if (PD >= max_defl)
                list.Add(string.Format("                        = {0:f3} mm > Maximum Deflection = {1:f3} mm    Hence OK", PD, max_defl));
            else
            {
                list.Add(string.Format("                        = {0:f3} mm < Maximum Deflection = {1:f3} mm    Hence NOT OK", PD, max_defl));
                IS_DESIGN_OK = false;
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            //File.WriteAllLines(Report_File, list.ToArray());
            File.WriteAllLines(Get_Report_File(), list.ToArray());
        }

        public string Report_File { get; set; }

        public bool IS_DESIGN_OK { get; set; }

        #region IBeamDesign Members

        public List<BeamData> All_Beam_Data
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ASTRADoc AST_DOC
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Beam_Nos
        {
            get;
            set;
        }

        public string Beam_Title
        {
            get;
            set;
        }

        public Beam_BOQ BOQ
        {
            get;
            set;
        }



        public void Get_Continuous_Beams(ref JointCoordinateCollection cont_jcc,
            ref MemberIncidenceCollection mbr_coll, ref DirecctionCollection dc1)
        {

            MemberIncidence b1 = AST_DOC.Members.Get_Member(MyList.StringToInt(Beam_Nos, 0));

            if (b1 == null) return;

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();

            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();

            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            List<bool> flags = new List<bool>();



            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();




            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }


            if (jcc[0].NodeNo < jcc[1].NodeNo)
            {
                if (!cont_jcc.Contains(jcc[1]))
                {
                    cont_jcc.Add(jcc[1]);
                }
            }
            cont_jcc.Add(jcc[0]);

            mbr_coll.Add(b1);

            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                foreach (var item in AST_DOC.Members)
                {
                    if (b1.Direction == item.Direction)
                    {
                        if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                        {
                            if (!cont_jcc.Contains(item.EndNode))
                            {
                                mbr_coll.Add(item);
                                cont_jcc.Add(item.EndNode);
                                i = 0; break;
                            }
                        }
                        else if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                        {
                            if (!cont_jcc.Contains(item.StartNode))
                            {
                                mbr_coll.Add(item);
                                cont_jcc.Add(item.StartNode);
                                i = 0; break;
                            }
                        }
                    }
                }
            }

            List<MemberIncidenceCollection> list_mic = new List<MemberIncidenceCollection>();


            for (int i = 0; i < cont_jcc.Count; i++)
            {
                mic4 = new MemberIncidenceCollection();
                foreach (var item in AST_DOC.Members)
                {
                    if (item.EndNode.NodeNo == cont_jcc[i].NodeNo ||
                        item.StartNode.NodeNo == cont_jcc[i].NodeNo)
                        mic4.Add(item);
                }
                list_mic.Add(mic4);
            }



            //DirecctionCollection dc1 = new DirecctionCollection();

            Axis_Direction ad = new Axis_Direction();
            int index = 0;

            for (index = 0; index < cont_jcc.Count; index++)
            {
                ad = new Axis_Direction();

                var mcc = list_mic[index];
                foreach (var item in mcc)
                {
                    JointCoordinate jc = item.StartNode;
                    if (item.StartNode.NodeNo == cont_jcc[index].NodeNo)
                    {
                        jc = item.EndNode;
                    }
                    ad.JointNo = cont_jcc[index].NodeNo;
                    if ((cont_jcc[index].Point.x < jc.Point.x))
                    {
                        ad.X_Positive_Member = item;
                        ad.X_Positive = true;
                    }
                    if ((cont_jcc[index].Point.x > jc.Point.x))
                    {
                        ad.X_Negative = true;
                        ad.X_Negative_Member = item;
                    }
                    if ((cont_jcc[index].Point.y < jc.Point.y))
                    {
                        ad.Y_Positive_Member = item;
                        ad.Y_Positive = true;
                    }
                    if ((cont_jcc[index].Point.y > jc.Point.y))
                    {
                        ad.Y_Negative_Member = item;
                        ad.Y_Negative = true;
                    }
                    if ((cont_jcc[index].Point.z < jc.Point.z))
                    {
                        ad.Z_Positive_Member = item;
                        ad.Z_Positive = true;
                    }
                    if ((cont_jcc[index].Point.z > jc.Point.z))
                    {

                        ad.Z_Negative_Member = item;
                        ad.Z_Negative = true;
                    }
                }
                dc1.Add(ad);
            }
        }

        public List<int> Get_Continuous_Beams(MemberIncidence b1)
        {

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            //if (jcc[0].NodeNo < jcc[1].NodeNo)
            //{
            //    if (!cont_jcc.Contains(jcc[1]))
            //    {
            //        cont_jcc.Add(jcc[1]);
            //    }
            //}
            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);
            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }



            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }

        public List<int> Get_Continuous_Beams(MemberIncidence b1,
            ref JointCoordinateCollection cont_jcc,
            ref MemberIncidenceCollection mbr_coll,
            ref DirecctionCollection dc1)
        {

            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            //MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }


            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);
            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    if (!mbr_coll.Contains(item))
                                        mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    if (!mbr_coll.Contains(item))
                                        mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }


            List<MemberIncidenceCollection> list_mic = new List<MemberIncidenceCollection>();


            for (int i = 0; i < cont_jcc.Count; i++)
            {
                mic4 = new MemberIncidenceCollection();
                foreach (var item in AST_DOC.Members)
                {
                    if (item.EndNode.NodeNo == cont_jcc[i].NodeNo ||
                        item.StartNode.NodeNo == cont_jcc[i].NodeNo)
                        mic4.Add(item);
                }
                list_mic.Add(mic4);
            }


            Axis_Direction ad = new Axis_Direction();
            int index = 0;

            for (index = 0; index < cont_jcc.Count; index++)
            {
                ad = new Axis_Direction();

                var mcc = list_mic[index];
                foreach (var item in mcc)
                {
                    JointCoordinate jc = item.StartNode;
                    if (item.StartNode.NodeNo == cont_jcc[index].NodeNo)
                    {
                        jc = item.EndNode;
                    }
                    ad.JointNo = cont_jcc[index].NodeNo;
                    if ((cont_jcc[index].Point.x < jc.Point.x))
                    {
                        ad.X_Positive_Member = item;
                        ad.X_Positive = true;
                    }
                    if ((cont_jcc[index].Point.x > jc.Point.x))
                    {
                        ad.X_Negative = true;
                        ad.X_Negative_Member = item;
                    }
                    if ((cont_jcc[index].Point.y < jc.Point.y))
                    {
                        ad.Y_Positive_Member = item;
                        ad.Y_Positive = true;
                    }
                    if ((cont_jcc[index].Point.y > jc.Point.y))
                    {
                        ad.Y_Negative_Member = item;
                        ad.Y_Negative = true;
                    }
                    if ((cont_jcc[index].Point.z < jc.Point.z))
                    {
                        ad.Z_Positive_Member = item;
                        ad.Z_Positive = true;
                    }
                    if ((cont_jcc[index].Point.z > jc.Point.z))
                    {

                        ad.Z_Negative_Member = item;
                        ad.Z_Negative = true;
                    }
                }
                dc1.Add(ad);
            }

            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
        }



        //Chiranjit [2015 03 06]



        public static List<string> Get_Banner()
        {
            List<string> list = new List<string>();


            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*                 ASTRA Pro                  *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*       DESIGN OF RCC FLANGED BEAM           *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add("");
            list.Add("");

            #endregion


            return list;

        }


        //Chiranjit [2015 03 06]
        public List<string> Design_Program_Loop(int step)
        {
            //Store Bar Nos
            return new List<string>();
        }


        public double Floor_Level = 0.0;

        public List<string> Design_Program_Individual()
        {
            //Store Bar Nos

            List<string> list = new List<string>();

            return list;
        }

        public string Get_Report_File()
        {

            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);
            //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
            tmp_file = Path.Combine(tmp_file, "DESIGN_STEEL_BEAM_FL_" + Floor_Level.ToString("f3").Replace(".", "_") + "_NO_" + Beam_Nos + ".TXT");
            return tmp_file;
        }


        public string Get_BOQ_File(int step)
        {

            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            tmp_file = Path.Combine(tmp_file, "BOQ");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
            tmp_file = Path.Combine(tmp_file, "BOQ_B" + step.ToString("000") + ".TXT");
            return tmp_file;
        }

        public string Get_Design_Summary_File(int step)
        {

            string tmp_file = Path.Combine(Path.GetDirectoryName(Report_File), "Reports");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            tmp_file = Path.Combine(tmp_file, "DESIGN SUMMARY");
            if (!Directory.Exists(tmp_file))
                Directory.CreateDirectory(tmp_file);

            //tmp_file = Path.Combine(tmp_file, "B" + step + ".tmp");
            tmp_file = Path.Combine(tmp_file, "DESIGN_SUMMARY_B" + step.ToString("000") + ".TXT");
            return tmp_file;
        }
        public string File_Design_Summary
        {
            get
            {
                string des_sum = Path.Combine(Path.GetDirectoryName(Get_Report_File()), "BEAM_DESIGN_SUMMARY.TXT");
                return des_sum;
            }
        }
        #endregion
    }

    interface IBeamDesign
    {
        System.Collections.Generic.List<BeamData> All_Beam_Data { get; set; }
        HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc AST_DOC { get; set; }
        string Beam_Nos { get; set; }
        string Beam_Title { get; set; }
        Beam_BOQ BOQ { get; set; }
        void Calculate_Program();
        void Calculate_Program_Loop();
        System.Collections.Generic.List<string> Design_Program_Individual();
        System.Collections.Generic.List<string> Design_Program_Loop(int step);
        System.Collections.Generic.List<string> Design_Summary { get; set; }
        eDesignStandard DesignStandard { get; set; }
        string File_Design_Summary { get; }
        void Get_All_Forces(HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref double AM1, ref double AM2, ref double AM3, ref double AM4, ref double AV1, ref double AV2, ref double AV3);
        string Get_BOQ_File(int step);
        System.Collections.Generic.List<int> Get_Continuous_Beams(HEADSNeed.ASTRA.ASTRAClasses.MemberIncidence b1, ref HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref HEADSNeed.ASTRA.ASTRAClasses.MemberIncidenceCollection mbr_coll, ref HEADSNeed.ASTRA.ASTRAClasses.DirecctionCollection dc1);
        System.Collections.Generic.List<int> Get_Continuous_Beams(HEADSNeed.ASTRA.ASTRAClasses.MemberIncidence b1);
        void Get_Continuous_Beams(ref HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref HEADSNeed.ASTRA.ASTRAClasses.MemberIncidenceCollection mbr_coll, ref HEADSNeed.ASTRA.ASTRAClasses.DirecctionCollection dc1);
        string Get_Design_Summary_File(int step);
        string Get_Report_File(int step);
        bool IS_DESIGN_OK { get; set; }
        double L { get; set; }
        double Lx1 { get; set; }
        double Lx2 { get; set; }
        double Ly1 { get; set; }
        double Ly2 { get; set; }
        string Report_File { get; set; }
    }

    interface IColumnDesign
    {
        System.Collections.Generic.List<ColumnData> All_Column_Data { get; set; }
        HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc AST_DOC { get; set; }
        string Beam_Nos { get; set; }
        Column_BOQ BOQ { get; set; }
        System.Collections.Generic.List<string> BOQ_Summary { get; set; }
        void Calculate_Program();
        void Calculate_Program_Loop();
        ColumnData Col_Data { get; set; }
        System.Collections.Generic.List<string> Design_Program_Individual(int step);
        System.Collections.Generic.List<string> Design_Program_Individual2(int step);
        System.Collections.Generic.List<string> Design_Program_Loop(int step);
        System.Collections.Generic.List<string> Design_Summary { get; set; }
        string File_Design_Summary { get; }
        string Get_BOQ_File(int step);
        System.Collections.Generic.List<int> Get_Continuous_Members(HEADSNeed.ASTRA.ASTRAClasses.MemberIncidence b1, ref HEADSNeed.ASTRA.ASTRAClasses.JointCoordinateCollection cont_jcc, ref HEADSNeed.ASTRA.ASTRAClasses.MemberIncidenceCollection mbr_coll);
        string Get_Design_Summary_File(int step);
        string Get_Report_File(int step);
        bool IS_DESIGN_OK { get; set; }
        string Report_File { get; set; }
        void Write_All_Data();
    }

}
