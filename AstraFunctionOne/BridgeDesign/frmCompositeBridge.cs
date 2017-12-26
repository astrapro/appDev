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
using AstraInterface.TrussBridge;
//using AstraFunctionOne.BridgeDesign.SteelTrussTables;

namespace AstraFunctionOne.BridgeDesign
{
    public partial class frmCompositeBridge : Form, IReport
    {
        string rep_file_name = "";
        string user_input_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";
        string drawing_path = "";
        bool is_process = false;
        IApplication iApp = null;
        string ref_string = "";

        TableRolledSteelAngles tbl_rolledSteelAngles = null;

        double S, B1, B2, B, fck, fy, m, YS, D, L, Dwc, gamma_c, gamma_wc;
        double WL, v, u, IF, CF, Q, j, sigma_st, sigma_b, tau, sigma_tf, K, sigma_p;
        double dw, tw, bf, tf, ang_thk, off;
        double des_moment, des_shear;
        int nw, nf, na;
        string ang = "";

        string _A, _B, _C,_G, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;
        string _v, _u, _1, _2, _3, _4, _6, _7, _8, _10;


        double inner_L2_moment = 0.0;
        double outer_L2_moment = 0.0;

        double inner_deff_shear = 0.0;
        double outer_deff_shear = 0.0;


        public frmCompositeBridge(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            _A = "";
            _B = "";
            _C = "";
            _D = "";
            _E = "";
            _F = "";
            _bd1 = "";
            _sp1 = "";
            _bd2 = "";
            _sp2 = "";
        }
        #region User Method

        public void Calculate_Program()
        {
            frmCurve f_c = null;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*                 ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*         DESIGN OF COMPOSITE BRIDGE         *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bridge Span [S] = {0} m", S);
                sw.WriteLine("Carriageway Width [B1] = {0} m    Marked as (A) in the Drawing", B1);
                _A = B1 + " m.";

                sw.WriteLine("Footpath Width [B2] = {0} m", B2);
                sw.WriteLine("Spacing on either side of Main Girders [B] = {0} m     Marked as (B) in the Drawing", B);
                _B = B + " m.";
                
                
                sw.WriteLine("Concrete Grade [fck] = M {0:f0} = {0:f0} N/sq.mm", fck);
                sw.WriteLine("Reinforcement Steel Frade [fy] = Fe {0:f0} = {0:f0} N/sq.mm", fy);
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Rolled Steel Section of Yield Stress [YS] = {0} N/sq.mm", YS);
                sw.WriteLine("SLAB Thickness [D] = {0} mm     Marked as (C) in the Drawing", D);
                _C = D/1000.0 + " m.";



                sw.WriteLine("Panel Length [L] = {0} m        Marked as (D) in the Drawing", L);
                _D = L + " m.";


                sw.WriteLine("Thickness of wearing course [Dwc] = {0} mm     Marked as (G) in the Drawing", Dwc);
                _G = Dwc/1000.0 + " m.";


                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine("Unit Weight of wearing cource [γ_wc] = {0} kN/cu.m", gamma_wc);
                sw.WriteLine("Tracked Vehicle Load [WL] = {0} kN", WL);
                sw.WriteLine("Length of Loaded area [v] = {0} m     Marked as (E) in the Drawing", v);
                _E = v + " m.";


                sw.WriteLine("Width of Loaded area [u] = {0} m      Marked as (F) in the Drawing", u);
                _F = u + " m.";

                sw.WriteLine("Impact Factor [IF] = {0}", IF);
                sw.WriteLine("Continuity Factor [CF] = {0}", CF);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("[σ_st] = {0} N/sq.mm", sigma_st);
                sw.WriteLine("Permissible Bending Stress in Steel [σ_b] = {0} N/sq.mm", sigma_b);
                sw.WriteLine("Permissible Shear Stress in Steel [τ] = {0} N/sq.mm", tau);
                sw.WriteLine("Permissible Shear Stress through fillet Weld [σ_tf] = {0} N/sq.mm", sigma_tf);
                sw.WriteLine("Constant ‘K’ = {0}", K);
                sw.WriteLine("Permissible Bearing Stress [σ_p] = {0} N/sq.mm", sigma_p);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                #region Report 
                #region STEP 5 : DESIGN OF STEEL PALTE GIRDER
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : DESIGN OF STEEL PLATE GIRDER");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.1 : Load Computation:");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double M = des_moment;
                sw.WriteLine("Design Bending Moment = M = {0} kN-m", M);
                double deg_sh_frc = des_shear;
                sw.WriteLine("Design shear force = v = {0} kN", des_shear);
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.2 : Size of Web Plate and Flange Plate");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Approximate depth of Girder = S /10 = {0} / 10 = {1} m", S, (S / 10.0));
                sw.WriteLine();

                double eco_depth_girder = (M * 10E5) / sigma_b;

                eco_depth_girder = Math.Pow(eco_depth_girder, (1.0 / 3.0));
                eco_depth_girder = 5.0 * eco_depth_girder;


                sw.WriteLine("Economical depth of Girder = 5 * (M / σb)^(1/3)");
                sw.WriteLine("                           = 5 * ({0} * 10^6 / {1})^(1/3)", M, sigma_b);
                eco_depth_girder = Double.Parse(eco_depth_girder.ToString("0"));
                sw.WriteLine("                           = {0} mm", eco_depth_girder);
                sw.WriteLine();


                sw.WriteLine("Web depth by {0} mm thick plate for shear considerations", tw);
                sw.WriteLine();
                double web_depth = (deg_sh_frc * 1000) / (tau * tw);
                sw.WriteLine("    = v / (τ * {0})", tw);
                sw.WriteLine("    = {0} * 1000 / ({1}*{2})", deg_sh_frc, tau, tw);
                web_depth = Double.Parse(web_depth.ToString("0.00"));
                sw.WriteLine("    = {0} mm", web_depth);
                sw.WriteLine();

                //double dw, tw;
                //dw = 1000.0;
                //tw = 10.0;
                double Aw = nw * dw * tw;
                sw.WriteLine("Provided size of Web Plate is {0} * {1} * {2} = Aw       Marked as (3) in the Drawing", nw, dw, tw);
                //sw.WriteLine("Let us try Web as {0} mm * {1} mm = dw * tw = Aw       Marked as (3) in the Drawing", dw, tw);

                //(3) = Web depth x thickness = 1000 mm * 10 mm
                _3 = string.Format("Size of Web Plate = {0} * {1} * {2} sq.mm", nw, dw, tw);

                RolledSteelAnglesRow tab_data = tbl_rolledSteelAngles.GetDataFromTable("ISA", ang, ang_thk);

                double i1 = (nw * (tw * dw * dw * dw) / 12.0);
                double i2 = (1 / 12.0) * (bf * Math.Pow((dw + (nf * tf)), 3.0) - bf * dw * dw * dw);
                double i3 = ((tab_data.Ixx * 10000) + (tab_data.Area * 100) * (dw - (tab_data.Cxx * tab_data.Cxx * 100)));

                double I = i1 + i2 + i3;


                sw.WriteLine();
                sw.WriteLine("Moment of Inertia  = I = (nw * (tw * dw^3) / 12.0) + ");
                sw.WriteLine("                         (1 / 12.0) * (bf * (dw + (nf * tf))^3) - bf * dw^3)");
                sw.WriteLine("                         (Ixx + a * (dw - (Cxx * Cxx)))");
                sw.WriteLine();
                sw.WriteLine("                       = ({0} * ({1} * {2}^3) / 12.0) + ", nw, tw, dw);
                sw.WriteLine("                         (1 / 12.0) * ({0} * ({1} + ({2} * {3}))^3) - {0} * {1}^3) + ",
                                                            bf, dw, nf, tf);
                sw.WriteLine("                         ({0} + {1} * ({2} - ({3} * {3})))", tab_data.Ixx * 10000, tab_data.Area * 100, dw, tab_data.Cxx * 10);
                sw.WriteLine();


                sw.WriteLine("                       = {0} sq.sq.mm", I);

                double y = dw / 2.0 + tf;

                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Approximate Flange area required");
                double Af = ((M * 10E5) / (sigma_b * dw)) - (Aw / 6);
                Af = Double.Parse(Af.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Af = (M / (σ_b * dw)) - (Aw / 6)");
                sw.WriteLine("   = ({0} * 10^6) / ({1} * {2})) - (dw * tw) / 6", M, sigma_b, dw);
                sw.WriteLine("   = ({0} * 10^6) / ({1} * {2})) - ({2} * {3}) / 6", M, sigma_b, dw, tw);
                sw.WriteLine("   = {0} sq.mm", Af);
                sw.WriteLine();

                sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                sw.WriteLine("             = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);

                double Bf1 = S * 1000 / 40.0;
                double Bf2 = S * 1000 / 45.0;
                double Bf = (Bf1 > Bf2) ? Bf1 : Bf2;

                Bf = (int)(Bf / 100.0);
                Bf += 1;
                Bf *= 100.0;

                sw.WriteLine("             = {0:f0}  to  {1:f0} mm", Bf1, Bf2);
                sw.WriteLine();
                sw.WriteLine();

                Bf = bf;
                sw.WriteLine("So, Provided Size of Flange Plate = {0} * {1} * {2}           Marked as (4) in the Drawing", nf, Bf, tf);
                //(4) = Flange width x thickness = 500 mm * 30 mm

                _4 = string.Format("Flange Size = {0} * {1} * {2} ", nf, bf, tf);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.3 : Check for Maximum Stresses");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double appl_stress = (M * 1000000 * y) / I;
                appl_stress = Double.Parse(appl_stress.ToString("0"));
                sw.WriteLine("Applied Stress = M * y  / I");
                sw.WriteLine();

                //sw.WriteLine("Where, I = (tw * dw**3 / 12) + 2 * (tf * Bf) * (dw / 2 + tf /2)^2");
                //sw.WriteLine("         = ({0} * {1}^3 / 12) + 2 * ({2} * {3}) * ({1} / 2 + {2} /2)^2", tw, dw, tf, Bf);
                //I = I / 10E6;
                //I = Double.Parse(I.ToString("0"));
                sw.WriteLine("         = {0:f3} sq.sq.mm", I);
                sw.WriteLine();
                sw.WriteLine(" and y = dw / 2 + tf");
                sw.WriteLine("       = {0} + {1}", (dw / 2.0), tf);
                sw.WriteLine("       = {0} mm", y);
                sw.WriteLine();


                sw.WriteLine("So,  Applied Stress = {0} * 10^6 * {1} / ({2} * 10E6)", M, y, I);
                if (appl_stress < sigma_b)
                {
                    sw.WriteLine("                    = {0} N/sq.mm < σ_b = {1} N/sq.mm, OK", appl_stress, sigma_b);
                }
                else
                {
                    sw.WriteLine("                    = {0} N/sq.mm > σ_b = {1} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b);
                }
                sw.WriteLine();

                double u_by_B = deg_sh_frc;
                deg_sh_frc = v;
                v = u_by_B;

                double tau1 = v * 1000 / (dw * tw);
                tau1 = double.Parse(tau1.ToString("0"));
                sw.WriteLine("Average Shear Stress = τ1 ");
                sw.WriteLine("                     = v * 1000 / (dw * tw)");
                sw.WriteLine("                     = {0} * 1000 / ({1} * {2})", v, dw, tw);
                sw.WriteLine("                     = {0} N/sq.mm", tau1);
                sw.WriteLine();

                double ratio = (dw / tw);
                sw.WriteLine("Ratio dw / tw = {0} / {1} = {2}", dw, tw, ratio);
                sw.WriteLine();
                sw.WriteLine("Considering Stiffener Spacing = c = dw = {0} mm", dw);
                sw.WriteLine();

                // Calculate from Table 1
                // **Problem How to calculate value from Table1 ?
                double tau2 = Get_Table_1_Value(100, 1);
                //double tau2 = 87;
                sw.WriteLine("From Table 1 (Given at the end of the Report) Allowable ");
                sw.WriteLine("average Shear Stress = {0} N/Sq mm = τ2", tau2);

                if (tau2 > tau1)
                {
                    sw.WriteLine("As τ2 > τ1 so, Average shear stress is within Safe permissible Limits, OK.");
                }
                else
                {
                    sw.WriteLine("As τ2 < τ1 so, Average shear stress is not within Safe permissible limits, NOT OK.");
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.4 : Connection Between Flange and Web");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Maximum Shear Force at the junction of Flange and Web is given by");
                sw.WriteLine();

                sw.WriteLine("    τ  = v * a * y  / I");
                double a = Bf * tf;
                y = dw / 2.0 + (tf / 2.0);
                I = double.Parse(I.ToString("0"));

                sw.WriteLine("    a = Bf * tf = {0} * {1} = {2:f2} sq.mm", Bf, tf, a);
                sw.WriteLine();
                sw.WriteLine("    y = dw/2 + tf/2 = {0}/2 + {1}/2 = {2} mm", dw, tf, y);
                sw.WriteLine();
                sw.WriteLine("    I = {0} * 10E6 sq.sq.mm", I);
                sw.WriteLine();
                sw.WriteLine("    v = {0} * 1000 N", v);
                sw.WriteLine();

                tau = (v * 1000 * a * y) / (I * 10E6);
                tau = double.Parse(tau.ToString("0"));
                //sw.WriteLine("τ = 548 * 1000 * 15000 * 515 / (879 * 107) = 483 N/mm");
                sw.WriteLine("    τ  = {0} * 1000 * {1} * {2}  / ({3} * 10E6)", v, a, y, I);
                sw.WriteLine("       = {0} N/mm", tau);
                sw.WriteLine();

                sw.WriteLine("Adopting Continuous weld on either side, strength of weld of sizw ");
                sw.WriteLine();
                sw.WriteLine("  ‘S’ = 2 * k * S * σ_tf");

                double _S = 2 * K * sigma_tf;
                _S = double.Parse(_S.ToString("0"));
                sw.WriteLine("      = 2 * {0} * S * {1}", K, sigma_tf);
                sw.WriteLine("      = {0} * S", _S);
                sw.WriteLine();


                sw.WriteLine("Equating, {0} * S = {1},                S = {1} / {0} = {2:f2} mm", _S, tau, (tau / _S));
                sw.WriteLine();

                _S = tau / +_S;

                _S = (int)_S;
                _S += 2;

                sw.WriteLine("Use {0} mm Fillet Weld, continuous on either side.     Marked as (5) in the Drawing", _S);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.5 : Intermediate Stiffeners");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double val1, val2;

                val1 = dw / tw;
                if (val1 < 85)
                {
                    sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                    sw.WriteLine("So, Vertical Stiffeners are required");
                    //sw.WriteLine("else, Vertical Stiffeners are not required.");
                }
                else
                {
                    sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                    //sw.WriteLine("So, Vertical Stiffeners are required");
                    sw.WriteLine("So, Vertical Stiffeners are not required.");
                }
                sw.WriteLine();

                double sp_stifn1 = 0.33 * dw;
                double sp_stifn2 = 1.5 * dw;
                sw.WriteLine("Spacing of Stiffeners = 0.33 * dw  to  1.5 * dw");
                sw.WriteLine("            = 0.33 * {0}  to  1.5 * {0}", dw);
                sw.WriteLine("            = {0} mm to {1} mm", sp_stifn1, sp_stifn2);
                sw.WriteLine();

                double c = 1000;

                sw.WriteLine("Adopt Spacing = c = {0} mm", c);
                sw.WriteLine();


                sw.WriteLine("Required minimum Moment of Inertia of Stiffeners");
                sw.WriteLine();


                double _I = ((1.5 * dw * dw * dw * tw * tw * tw) / (c * c));
                sw.WriteLine("I = 1.5 * dw**3 * tw**3 / c**2");
                sw.WriteLine("  = 1.5 * {0}^3 * {1}^3 / {2}^2", dw, tw, c);
                _I = _I / 10E4;
                _I = double.Parse(_I.ToString("0"));
                sw.WriteLine("  = {0} * 10E4 sq.sq.mm", _I);
                sw.WriteLine();

                double t = 10; // t is Constant?

                sw.WriteLine("Use {0} mm thick plate, t = {0} mm", t);
                sw.WriteLine();
                sw.WriteLine("Maximum width of plate not to exceed = 12 * t = {0} mm", (12 * t));
                sw.WriteLine();

                // 80 ?
                double h = 80;
                sw.WriteLine("Use 80 mm size plate, h = 80 mm");
                sw.WriteLine();
                sw.WriteLine("Plate size is {0} mm * {1} mm      Marked as (6) in the Drawing", h, t);
                _6 = string.Format("{0} mm x {1} mm", h, t);



                sw.WriteLine();

                double _I1 = (t * (h * h * h)) / 3.0;
                //_I1 = _I1 / 10E4;
                //_I1 = double.Parse(_I1.ToString("0"));

                if (_I1 > _I)
                {
                    sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm > {2:e2} sq.sq.mm,                OK", t, _I1, _I);
                }
                else
                {
                    sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.6 : Connections of Vertical Stiffener to Web");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Shear on weld connecting stiffener to Web");
                sw.WriteLine();

                // 125 = constant ?
                double sh_wld_wb = 125 * tw * tw / h;
                sw.WriteLine("    = 125 * tw*tw / h");
                sw.WriteLine("    = 125 * {0}*{0} / {1}", tw, h);

                sh_wld_wb = double.Parse(sh_wld_wb.ToString("0.00"));
                sw.WriteLine("    = {0} kN/m", sh_wld_wb);
                sw.WriteLine("    = {0} N/mm", sh_wld_wb);
                sw.WriteLine();

                double sz_wld = sh_wld_wb / (K * sigma_tf);
                sz_wld = double.Parse(sz_wld.ToString("0.00"));
                sw.WriteLine("Size of welds = 156.25 / (K * σ_tf)");
                sw.WriteLine("              = {0} / ({1} * {2})", sh_wld_wb, K, sigma_tf);
                sw.WriteLine("              = {0} mm", sz_wld);
                sw.WriteLine();

                //sw.WriteLine("Size of welds = 156.25 / (K * σtf) = 156.25 / (0.7 * 102.5) = 2.17 mm");
                // How come 100 and 5?
                sw.WriteLine("Use 100 mm Long 5 mm Fillet Welds alternately on either side.     Marked as (7) in the Drawing");

                //(7)  5-100-100 (weld)
                _7 = string.Format("5-100-100 (weld)");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.7 : End Bearing Stiffeners");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Maximum Shear Force = v = {0} kN", v);
                sw.WriteLine();

                val1 = (h / t);
                if (val1 < 12)
                {
                    sw.WriteLine("The end bearing Stiffeners is designed as a column h / t < 12");
                }
                else
                {
                    sw.WriteLine("The end bearing Stiffeners is designed as a column h / t > 12");
                }
                sw.WriteLine();
                h = 180;
                sw.WriteLine("Use ‘h’ = outstand of stiffeners = {0} mm", h);
                sw.WriteLine();
                t = h / 12;
                sw.WriteLine("t = {0} / 12 = {1} mm", h, (h / 12.0));
                sw.WriteLine();
                sw.WriteLine("Use plate of size 180 mm * 15 mm     Marked as (8) in the Drawing");

                //(8)  180 x15 mm

                _8 = string.Format("180 x 15 mm");
                sw.WriteLine();

                double brng_ar_req = v * 1000 / sigma_p;
                sw.WriteLine();
                sw.WriteLine("Bearing area required = v * 1000 / σ_p");
                sw.WriteLine("                      = {0} * 1000 / {1} sq.mm", v, sigma_p);
                brng_ar_req = double.Parse(brng_ar_req.ToString("0"));
                sw.WriteLine("                      = {0} sq.mm", brng_ar_req);
                sw.WriteLine();

                double tot_area = 2 * h * t;
                sw.WriteLine("If two plates are used,");
                sw.WriteLine("     Total area = 2 * {0} * {1}", h, t);
                if (tot_area > brng_ar_req)
                {
                    sw.WriteLine("                = {0} sq.mm > {1} sq.mm", tot_area, brng_ar_req);
                }
                else
                {
                    sw.WriteLine("                = {0} sq.mm < {1} sq.mm", tot_area, brng_ar_req);
                }
                sw.WriteLine();

                sw.WriteLine("The length of Web plate which acts along with Stiffener ");
                sw.WriteLine("plates in bearing the reaction = lw = 20 * tw");
                sw.WriteLine("                               = 20 * {0}", tw);
                double brng_reaction = 20 * tw;
                double lw = brng_reaction;
                sw.WriteLine("                               = {0} mm", lw);
                sw.WriteLine();
                _I = ((t * (2 * h + 10) * (2 * h + 10) * (2 * h + 10)) / 12) + (2 * lw * tw * tw * tw / 12);

                //**lw = ?
                sw.WriteLine("    I = t * (2 * h + 10)^3 / 12 + 2 * lw * tw**3 / 12");
                sw.WriteLine("      = {0} * (2 * {1} + 10)^3 / 12 + 2 * {2} * {3}^3 / 12", t, h, 200, tw);
                //sw.WriteLine("      = 15 * 3703 / 12 + 2 * 200 * 103 / 12");
                _I = (_I / 10E3);
                _I = double.Parse(_I.ToString("0"));

                sw.WriteLine("      = {0} * 10E3 Sq Sq mm", _I);
                sw.WriteLine();

                double A = 2 * h * t + 2 * lw * tw;
                A = double.Parse(A.ToString("0"));
                sw.WriteLine("    Area = A = 2 * h * t + 2 * lw * tw");
                sw.WriteLine("         = 2 * {0} * {1} + 2 * {2} * {3}", h, t, lw, tw);
                sw.WriteLine("         = {0} sq.mm", A);

                sw.WriteLine();

                double r = (_I * 10E3) / A;
                r = Math.Sqrt(r);
                r = double.Parse(r.ToString("0"));
                sw.WriteLine("    r = √(I / A) = √({0} * 10E3 / {1}) = {2} mm", _I, A, r);
                sw.WriteLine();


                // ** 0.7 = ?
                double _L = 0.7 * dw;
                double lamda = (_L / r);
                lamda = double.Parse(lamda.ToString("0.00"));
                sw.WriteLine("    λ = Slenderness ratio = L / r");
                sw.WriteLine();
                sw.WriteLine("    L = Effective Length of stiffeners");
                sw.WriteLine("      = 0.7 * dw");
                sw.WriteLine("      = 0.7 * {0}", tw);
                sw.WriteLine("      = {0} mm", _L);
                sw.WriteLine();
                sw.WriteLine("    λ = {0} / {1}", _L, r);
                sw.WriteLine("      = {0}", lamda);

                sw.WriteLine();

                sw.WriteLine("    Ref. Table 2 (given at the end of the Report)");

                double sigma_ac = Get_Table_2_Value(lamda, 1);
                sigma_ac = double.Parse(sigma_ac.ToString("0"));
                sw.WriteLine("    Permissible Stress in axial compression σ_ac = {0} N/sq.mm", sigma_ac);
                sw.WriteLine();

                double area_req = v * 1000 / sigma_ac;
                area_req = double.Parse(area_req.ToString("0"));
                sw.WriteLine("    Area required = v * 1000 / σ_ac ");
                sw.WriteLine("                  = {0} * 1000 / {1}", v, sigma_ac);
                if (area_req < A)
                {
                    sw.WriteLine("                  = {0} sq.mm < {1} sq.mm,  Ok", area_req, A);
                }
                else
                {
                    sw.WriteLine("                  = {0} sq.mm > {1} sq.mm,  NOT OK", area_req, A);
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.8 : Connection between Bearing Stiffener and Web");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //** 40 = ?
                double len_alt = 2 * (dw - 40);
                sw.WriteLine("Length available for alternate intermittent weld");
                sw.WriteLine("   = 2 * (dw - 40)");
                sw.WriteLine("   = 2 * ({0} - 40)", dw);
                sw.WriteLine("   = {0} mm", len_alt);
                sw.WriteLine();

                double req_strnth_wld = (v * 1000 / len_alt);
                req_strnth_wld = double.Parse(req_strnth_wld.ToString("0"));
                sw.WriteLine("Required strength of weld = v * 1000 / 1920");
                sw.WriteLine("                          = {0} * 1000 / {1}", v, len_alt);
                sw.WriteLine("                          = {0} N/mm", req_strnth_wld);
                sw.WriteLine();

                sz_wld = req_strnth_wld / (K * sigma_tf);
                sz_wld = double.Parse(sz_wld.ToString("0.00"));


                //** σ_ac =  138 but 102.5 = ?
                //sw.WriteLine("Size of weld = 286 / (K * σ_ac) = 286 / (0.7 * 102.5) = 3.98 mm");
                sw.WriteLine("Size of weld = 286 / (K * σ_tf)");
                sw.WriteLine("             = {0} / ({1} * {2})", req_strnth_wld, K, sigma_tf);
                sw.WriteLine("             = {0} mm", sz_wld);
                sw.WriteLine();

                if (sz_wld < 5)
                    sz_wld = 5;
                else
                {
                    sz_wld = (int)sz_wld;
                    sz_wld += 1;
                }
                sw.WriteLine("Use {0} mm Fillet Weld", sz_wld);
                sw.WriteLine();

                double len_wld = 10 * tw;

                sw.WriteLine("Length of Weld >= 10 * tw = 10 * {0} = {1} mm", tw, len_wld);
                sw.WriteLine();

                sw.WriteLine("Use {0} mm Long, {1} mm Weld Alternately.     Marked as (9) in the Drawing", len_wld, sz_wld);
                sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.9 : Properties of Composite Section");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double Ace = B * 1000 * D / m;
                Ace = double.Parse(Ace.ToString("0"));

                sw.WriteLine("Ace = B * 1000 * D/m");
                sw.WriteLine("    = {0} * 1000 * {1}/{2}", B, D, m);
                sw.WriteLine("    = {0} sq.mm", Ace);
                sw.WriteLine();
                sw.WriteLine("The centroid of Composite Section (Neutral Axis) is determined");
                sw.WriteLine("by first moment of the areas about axis xx,");


                double Axy = Ace * (dw + 2 * tf + D / 2) + Bf * tf * (dw + tf + tf / 2) + dw * tw * (dw / 2 + tf) + Bf * tf * tf / 2;

                sw.WriteLine();
                sw.WriteLine("Axy  = Ace * (dw + 2 * tf + D/2) + Bf * tf * (dw + tf + tf/2) ");
                sw.WriteLine("       + dw * tw * (dw/2 +tf) + Bf * tf * tf/2");
                sw.WriteLine();
                sw.WriteLine("     = {0} * ({1} + 2 * {2} + {3}/2) + {4} * {2} * ({1} + {2} + {2}/2) ", Ace, dw, tf, D, Bf);
                sw.WriteLine("       + {0} * {1} * ({0}/2 + {2}) + {3} * {2} * {2}/2", dw, tw, tf, Bf);
                sw.WriteLine();

                Axy = double.Parse(Axy.ToString("0"));
                sw.WriteLine("     = {0}", Axy);
                //sw.WriteLine("= 77046340");
                double _A_d = Ace + (dw / 2) * tf + dw * tw + (dw / 2.0) * tf;
                _A_d = double.Parse(_A_d.ToString("0"));
                // ** formula ?
                sw.WriteLine();
                sw.WriteLine("A = Ace + (dw / 2) * tf + dw * tw + (dw / 2.0) * tf");
                sw.WriteLine("  = {0} + ({1} / 2) * {2} + {0} * {3} + ({1} / 2.0) * {2}", Ace, dw, tf, tw);
                sw.WriteLine("  = {0} sq.mm", _A_d);
                sw.WriteLine();

                y = Axy / _A_d;
                // ** sign y bar ?
                sw.WriteLine("  y = Axy / A = {0} / {1}", Axy, _A_d);
                //sw.WriteLine("    = {0:f0}", y);

                y = double.Parse(y.ToString("0"));
                sw.WriteLine("    = {0} mm", y);
                sw.WriteLine();

                double yc = dw + 2 * tf + D / 2 - y;

                sw.WriteLine("  yc = dw + 2 * tf + D/2 -  y");
                sw.WriteLine("     = {0} + 2 * {1} + {2}/2 -  {3}", dw, tf, D, y);
                sw.WriteLine("     = {0} mm", yc);
                sw.WriteLine();


                double Icomp = Ace * yc * yc +
                    (Bf * (dw + (2 * tf)) * (dw + (2 * tf)) * (dw + (2 * tf))) / 12.0
                    - ((Bf - tw) * dw * dw * dw) / 12.0 +
                    (_A_d - Ace) * (y - (dw / 2.0) - tf) * (y - (dw / 2.0) - tf);


                sw.WriteLine("Icomp = distance from centre of Deck Slab to Centroid of Composite Section");

                sw.WriteLine("      = Ace * yc * yc ");
                sw.WriteLine("        + (Bf * (dw + (2 * tf))^3 ) / 12.0");
                sw.WriteLine("        - ((Bf - tw) * dw**3) / 12.0 ");
                sw.WriteLine("        + (A - Ace) * (y - (dw / 2.0) - tf) * (y - (dw / 2.0) - tf)");
                sw.WriteLine();

                sw.WriteLine("      = {0} * {1} * {1} ", Ace, yc);
                sw.WriteLine("        + ({0} * ({1} + (2 * {2}))^3 ) / 12.0", Bf, dw, tf);
                sw.WriteLine("        - (({0} - {1}) * {2}^3) / 12.0 ", Bf, tw, dw);
                sw.WriteLine("        + ({0} - {1}) * ({2} - ({3} / 2.0) - {4}) * ({2} - ({3} / 2.0) - {4})", _A_d, Ace, y, dw, tf);
                sw.WriteLine();




                Icomp = Icomp / 10E9;
                Icomp = double.Parse(Icomp.ToString("0.000"));
                sw.WriteLine("      = {0} * 10E9 sq.sq.mm", Icomp);
                sw.WriteLine();

                sw.WriteLine("Maximum Shear force at junction of Slab and Girder is obtained by");

                tau = (v * 1000 * Ace * yc) / (Icomp * 10E9);
                sw.WriteLine("τ = v * 1000 * Ace *  yc / Icomp");
                sw.WriteLine("  = {0} * 1000 * {1} * {2} / {3} * 10E9", v, Ace, yc, Icomp);
                tau = double.Parse(tau.ToString("0"));
                sw.WriteLine("  = {0} N/mm", tau);
                sw.WriteLine();

                double Q1 = tau * Bf;
                Q1 = double.Parse(Q1.ToString("0"));
                sw.WriteLine("Total Shear force at junction Q1 =  τ * Bf ");
                sw.WriteLine("                                 =  {0} * {1}", tau, Bf);
                sw.WriteLine("                                 =  {0} N", Q1);
                sw.WriteLine();

                double _do = 20.0;
                sw.WriteLine("Using do = {0} mm diameter mild steel studs,     Marked as (10) in the Drawing", _do);
                _10 = string.Format("{0} Ø Studs", _do);

                sw.WriteLine("capacity of one shear connector is given by,");
                sw.WriteLine();
                // 196 = ?
                double Q2 = 196 * _do * _do * Math.Sqrt(fck);
                Q2 = double.Parse(Q2.ToString("0"));
                sw.WriteLine("    Q2 = 196 * do*do *  √fck");
                sw.WriteLine("       = 196 * {0}*{0} *  √{1}", _do, fck);
                sw.WriteLine("       = {0} N", Q2);
                sw.WriteLine();

                // 5 = ?
                double H = 5 * 20;
                sw.WriteLine("Height of each stud = H");
                sw.WriteLine("                    = 5 * do");
                sw.WriteLine("                    = 5 * {0}", _do);
                sw.WriteLine("                    = {0} mm", H);
                sw.WriteLine();

                double no_std_row = (Q1 / Q2);
                no_std_row = double.Parse(no_std_row.ToString("0.00"));
                sw.WriteLine("Number of studs required in a row");
                sw.WriteLine();
                if (no_std_row < 1.0)
                {
                    sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} < 1", Q1, Q2, no_std_row);
                }
                else
                {
                    sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} > 1", Q1, Q2, no_std_row);
                }
                sw.WriteLine("So, Provide a minimum of 2 mild Steel Studs in a row");
                sw.WriteLine();

                double N = 2;
                double fs = 2.0;
                double p = N * Q2 / (fs * tau);
                p = double.Parse(p.ToString("0"));
                sw.WriteLine("Pitch of Shear Connectors = p = N * Q2 / (fs * τ)");

                sw.WriteLine("N = Number of Shear Connectors in a row = 2");
                sw.WriteLine();
                sw.WriteLine("Fs = Factor of Safety = 2.0");
                sw.WriteLine();
                sw.WriteLine("p = 2 * {0} / (2 * {1})", Q2, tau);
                sw.WriteLine("  = {0} mm", p);
                sw.WriteLine();

                sw.WriteLine("Maximum permissible pitch is the lowest value of:");
                sw.WriteLine();
                sw.WriteLine("(i)     3 * Thickness of Slab = 3 * {0} = {1:f0} mm", D, (3 * D));
                sw.WriteLine("(ii)    4 * Height of Stud = 4 * (5 * do) = 4 * {0:f0} = {1:f0} mm", (5 * _do), (4 * 5 * _do));
                sw.WriteLine("(iii)   600 mm");
                sw.WriteLine();
                sw.WriteLine("Hence provide the pitch of 400 mm in the longitudinal direction.    Marked as (11) in the Drawing");

                #endregion

                #region STEP 1 : COMPUTATION OF Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : DESIGN OF RCC DECK SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.1 : COMPUTATION OF PERMANENT LOAD");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double self_weight_deck_slab = (D / 1000.0) * gamma_c;

                sw.WriteLine("Self weight of Deck Slab = (D/1000) * γ_c");
                sw.WriteLine("                         = ({0:f2}) * {1:f0}", (D / 1000), gamma_c);
                sw.WriteLine("                         = {0:f2} kN/sq.mm", self_weight_deck_slab);
                sw.WriteLine();

                double self_weight_wearing_course = (Dwc / 1000.0) * gamma_wc;
                sw.WriteLine("Self weight of wearing course = (Dwc/1000) * γ_wc");
                sw.WriteLine("                              = {0:f2} * {1}", (Dwc/1000), gamma_wc);
                sw.WriteLine("                              = {0:f2} kN/sq.mm", self_weight_wearing_course);
                sw.WriteLine();
                double DL = self_weight_deck_slab + self_weight_wearing_course;

                sw.WriteLine("Total Load = DL ");
                sw.WriteLine("           = {0:f2} + {1:f2}", self_weight_deck_slab, self_weight_wearing_course);
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                DL = (int)DL;
                DL += 1.0;
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                #endregion

                #region STEP 2.2 : BENDING MOMENT BY MOVING LOAD
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.2 : BENDING MOMENT BY MOVING LOAD");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Load = WL = {0} kN", WL);
                sw.WriteLine("Panel Dimension L = {0:f2} m,                B = {1:f2} m", L, B);
                sw.WriteLine("Load Dimension v = {0:f2}, u = {1:f2} m", v, u);
                sw.WriteLine();
                sw.WriteLine("Considering 45° Load dispersion through wearing Course");
                sw.WriteLine();

                double _v = v + (2 * (Dwc / 1000.0));
                sw.WriteLine("    v = {0:f2} + (2*{1:f2}) = {2:f2} m.", v, (Dwc / 1000.0), _v);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _v);
                double _u = u + (2 * (Dwc / 1000.0));
                sw.WriteLine("    u = {0:f2} + (2*{1:f2}) = {2:f2} m.", u, (Dwc / 1000.0), _u);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _u);

                //double u_by_B = 0.0;

                u_by_B = v;
                v = _v;
                _v = u_by_B;

                u_by_B = u;
                u = _u;
                _u = u_by_B;



                u_by_B = u / B;
                sw.WriteLine("    u / B = {0:f2} / {1:f2} = {2:f3}", u, B, u_by_B);
                sw.WriteLine();
                double v_by_L = v / L;

                sw.WriteLine("    v / L = {0:f2} / {1:f2} = {2:f3}", v, L, v_by_L);
                sw.WriteLine();

                double k = B / S ;
                sw.WriteLine("    K = B / S = {0:f2} / {1:f2} = {2:f3}", B, L, k);
                sw.WriteLine();


                k = Double.Parse(k.ToString("0.0"));
                if (k < 0.4)
                    k = 0.4;
                if (k > 1.0) k = 1.0;
                f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);
                //if (k == 0.4)
                //{
                    f_c.txt_m1.Text = "0.085";
                    f_c.txt_m2.Text = "0.017";
                //}
                f_c.ShowDialog();
                double m1, m2;
                m1 = f_c.m1;
                m2 = f_c.m2;

                sw.WriteLine();
                sw.WriteLine("From Pigeaud’s Curves, for K = {0:f1}", k);
                sw.WriteLine("    m1 = {0}", m1);
                sw.WriteLine("    m2 = {0}", m2);

                double _MB = WL * (m1 + 0.15 * m2);
                _MB = double.Parse(_MB.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WL * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0} * ({1} + 0.15 * {2})", WL, m1, m2);
                sw.WriteLine("                          = {0} kN-m", _MB);
                sw.WriteLine();

                double MB1 = IF * CF * _MB;
                MB1 = double.Parse(MB1.ToString("0"));

                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = MB1");
                sw.WriteLine("  = IF * CF * MB’ ");
                sw.WriteLine("  = {0} * {1:f2} * {0:f2} ", IF, CF, _MB);
                sw.WriteLine("  = {0} kN-m", MB1);
                sw.WriteLine();

                double _ML = WL * (m2 + 0.15 * m1);

                sw.WriteLine("Long Span Bending Moment = ML’ ");
                sw.WriteLine("                         = WL * (m2 + 0.15 * m1) ");
                sw.WriteLine("                         = {0} * ({1} + 0.15 * {2}) ", WL, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML1 = IF * CF * _ML;
                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = ML1");
                sw.WriteLine("  = IF * CF * ML’ ");
                sw.WriteLine("  = {0} * {1} * {2:f2} ", IF, CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML1);
                sw.WriteLine();
                #endregion

                #region STEP 2.3 : BENDING MOMENT BY Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.3 : BENDING MOMENT BY PERMANENT LOAD");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Permanent Load of Deck Slab = DL = {0} kN/sq.mm", DL);

                double WD = DL * B * L;
                sw.WriteLine("Permanent Load per Panel = WD");
                sw.WriteLine("                     = DL * B * L");
                sw.WriteLine("                     = {0} * {1} * {2}", DL, B, L);
                sw.WriteLine("                     = {0:f2} kN", WD);
                sw.WriteLine();
                sw.WriteLine("u / B = 1 and  v / L = 1");
                
                k = B / L;
                k = Double.Parse(k.ToString("0.000"));
                sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f1}", B, L, k);
                sw.WriteLine("1/k = 1 / {0} = {1:f2}", k, (1 / k));

                f_c = new frmCurve(k, 1.0, 1.0, LoadType.FullyLoad);

                k = Double.Parse(k.ToString("0.0"));
                if (k == 0.4)
                {
                    f_c.txt_m1.Text = "0.047";
                    f_c.txt_m2.Text = "0.006";
                }
                f_c.ShowDialog();

                m1 = f_c.m1;
                m2 = f_c.m2;
                double MB, ML;

                sw.WriteLine();
                sw.WriteLine("Using Pigeaud’s Curves, m1 = {0} and m2 = {1}", m1, m2);
                sw.WriteLine();
                _MB = WD * (m1 + 0.15 * m2);
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WD * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0:f2} * ({1} + 0.15 * {2})", WD, m1, m2);
                sw.WriteLine("                          = {0:f2} kN-m", _MB);
                sw.WriteLine();


                sw.WriteLine("Short Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = MB2");

                double MB2 = CF * _MB;
                sw.WriteLine("  = CF * MB’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _MB);
                sw.WriteLine("  = {0:f2} kN-m", MB2);
                sw.WriteLine();

                _ML = WD * (m2 + 0.15 * m1);
                sw.WriteLine("Long Span Bending Moment = ML’");
                sw.WriteLine("                         = WD * (m2 + 0.15 * m1)");
                sw.WriteLine("                         = {0:f2} * ({1} + 0.15 * {2})", WD, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML2 = CF * _ML;
                sw.WriteLine("Long Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = ML2");
                sw.WriteLine("  = CF * ML’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine("Design Bending Moments are:");
                MB = MB1 + MB2;
                
                sw.WriteLine("Along Short Span = MB");
                sw.WriteLine("                 = MB1 + MB2");
                sw.WriteLine("                 = {0:f2} + {1:f2}", MB1, MB2);
                sw.WriteLine("                 = {0:f2} kN-m", MB);
                sw.WriteLine();


                ML = ML1 + ML2;
                sw.WriteLine("Along Long Span = ML");
                sw.WriteLine("                = ML1 + ML2");
                sw.WriteLine("                = {0:f2} + {1:f2}", ML1, ML2);
                sw.WriteLine("                = {0:f2} kN-m", ML);
                sw.WriteLine();
                
                #endregion

                #region STEP 2.4 : DESIGN OF SECTION FOR RCC DECK SLAB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.4 : STRUCTURAL DETAILING FOR RCC DECK SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();



                double d = (MB * 10E5) / (Q * 1000.0);

                d = Math.Sqrt(d);
                d = double.Parse(d.ToString("0"));
                sw.WriteLine("d = √((MB * 10^6) / (Q*b))");
                sw.WriteLine("  = √(({0:f2} * 10^6) / ({1:f3}*1000))", MB, Q);
                sw.WriteLine("  = {0} mm", d);
                sw.WriteLine();

                sw.WriteLine("The overall depth of RCC Deck Slab = {0} mm", D);

                double _d = d;
                d = D - 40.0;
                sw.WriteLine();
                sw.WriteLine("Effective Depth = {0} - 40 = {1} mm = d", D, d);


                double Ast = MB * 10E5 / (sigma_st * j * d);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Required steel along short span");
                sw.WriteLine("    = Ast");
                sw.WriteLine("    = (MB * 10^6) / (σ_st * j * d)");
                sw.WriteLine("    = ({0:f2} * 10^6) / ({1} * {2} * {3})", MB, sigma_st, j, d);
                sw.WriteLine("    = {0} sq.mm", Ast);

                List<double> lst_dia = new List<double>();

                lst_dia.Add(10);
                lst_dia.Add(12);
                lst_dia.Add(16);
                lst_dia.Add(20);
                lst_dia.Add(25);
                lst_dia.Add(32);


                int dia_indx = 0;
                double dia = lst_dia[0];
                double _ast = 0.0;
                double no_bar = 0.0;
                double spacing = 140;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0} mm bars at {1:f0} mm c/c.     Marked as (1) in the Drawing", dia, spacing);
                //(1) = T12 mm bars at 140 mm c/c.
                _1 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);
                
                sw.WriteLine();

                sw.WriteLine("Effective depth for Long span using T10 mm bars");
                sw.WriteLine();

                double d1 = d - (dia / 2.0) - (10.0 / 2.0);
                sw.WriteLine("    d1 = d - ({0:f0}/2) - (10/2)", dia);
                sw.WriteLine("       = {0} - {1:f0} - 5", d, (dia / 2.0));
                sw.WriteLine("       = {0:f0} mm", d1);
                sw.WriteLine();

                Ast = (ML * 10E5) / (sigma_st * j * d1);
                Ast = double.Parse(Ast.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Required steel along long span");
                sw.WriteLine("  = Ast");
                sw.WriteLine("  = ML * 10^6 / (σ_st * j * d1)");
                sw.WriteLine("  = {0:f2} * 10^6 / ({1} * {2} * {3})", ML, sigma_st, j, d1);
                sw.WriteLine("  = {0} sq.mm", Ast);
                sw.WriteLine();

                spacing = 150;
                dia_indx = 0;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine("Provide T{0:f0} Bars at {1:f0} mm c/c.    Marked as (2) in the Drawing", dia, spacing);
                //(2) = T10 Bars at 150 mm c/c.
                _2 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                #endregion



                sw.WriteLine();
                sw.WriteLine();
                Write_Table_1(sw);
                Write_Table_2(sw);

                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
                //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
                //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
                //sw.WriteLine("-----------------------------------------------------------------------");

                //sw.WriteLine("110    87       87       87      87       87      87      87");
                //sw.WriteLine("130    87       87       87      87       87      84      82");
                //sw.WriteLine("150    87       87       87      85       80      77      75");
                //sw.WriteLine("170    87       87       83      80       76      72      70");
                //sw.WriteLine("190    87       87       79      75");
                //sw.WriteLine("200    87       85       77");
                //sw.WriteLine("220    87       80       73");
                //sw.WriteLine("240    87       77");
                //sw.WriteLine("-----------------------------------------------------------------------");


                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
                //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
                //sw.WriteLine("λ= (L/r)  ______________________________________");
                //sw.WriteLine("           236        299        331       362");
                //sw.WriteLine("---------------------------------------------------");
                //sw.WriteLine("0         140.0      171.2       191.5    210.0");
                //sw.WriteLine("20        136.0      167.0       186.0    204.0");
                //sw.WriteLine("40        130.0      157.0       174.0    190.0");
                //sw.WriteLine("60        118.0      139.0       151.6    162.0");
                //sw.WriteLine("80        101.0      113.5       120.3    125.5");
                //sw.WriteLine("100        80.5       87.0        90.2     92.7");
                //sw.WriteLine("120        63.0       66.2        68.0     69.0");
                //sw.WriteLine("140        49.4       51.2        52.0     52.6");
                //sw.WriteLine("160        39.0       40.1        40.7     41.1");
                //sw.WriteLine("---------------------------------------------------");


                sw.WriteLine();
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
        public void Calculate_Program_OLD()
        {
            frmCurve f_c = null;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*                 ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*         DESIGN OF COMPOSITE BRIDGE         *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bridge Span [S] = {0} m", S);
                sw.WriteLine("Carriageway Width [B1] = {0} m    Marked as (A) in the Drawing", B1);
                _A = B1 + " m.";

                sw.WriteLine("Footpath Width [B2] = {0} m", B2);
                sw.WriteLine("Spacing on either side of Main Girders [B] = {0} m     Marked as (B) in the Drawing", B);
                _B = B + " m.";


                sw.WriteLine("Concrete Grade [fck] = M {0:f0} = {0:f0} N/sq.mm", fck);
                sw.WriteLine("Reinforcement Steel Frade [fy] = Fe {0:f0} = {0:f0} N/sq.mm", fy);
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Rolled Steel Section of Yield Stress [YS] = {0} N/sq.mm", YS);
                sw.WriteLine("SLAB Thickness [D] = {0} mm     Marked as (C) in the Drawing", D);
                _C = D / 1000.0 + " m.";



                sw.WriteLine("Panel Length [L] = {0} m        Marked as (D) in the Drawing", L);
                _D = L + " m.";


                sw.WriteLine("Thickness of wearing course [Dwc] = {0} mm     Marked as (G) in the Drawing", Dwc);
                _G = Dwc / 1000.0 + " m.";


                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine("Unit Weight of wearing cource [γ_wc] = {0} kN/cu.m", gamma_wc);
                sw.WriteLine("Tracked Vehicle Load [WL] = {0} kN", WL);
                sw.WriteLine("Length of Loaded area [v] = {0} m     Marked as (E) in the Drawing", v);
                _E = v + " m.";


                sw.WriteLine("Width of Loaded area [u] = {0} m      Marked as (F) in the Drawing", u);
                _F = u + " m.";

                sw.WriteLine("Impact Factor [IF] = {0}", IF);
                sw.WriteLine("Continuity Factor [CF] = {0}", CF);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("[σ_st] = {0} N/sq.mm", sigma_st);
                sw.WriteLine("Permissible Bending Stress in Steel [σ_b] = {0} N/sq.mm", sigma_b);
                sw.WriteLine("Permissible Shear Stress in Steel [τ] = {0} N/sq.mm", tau);
                sw.WriteLine("Permissible Shear Stress through fillet Weld [σ_tf] = {0} N/sq.mm", sigma_tf);
                sw.WriteLine("Constant ‘K’ = {0}", K);
                sw.WriteLine("Permissible Bearing Stress [σ_p] = {0} N/sq.mm", sigma_p);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                #region Report

                #region STEP 1 : COMPUTATION OF Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : COMPUTATION OF PERMANENT LOAD");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double self_weight_deck_slab = (D / 1000.0) * gamma_c;

                sw.WriteLine("Self weight of Deck Slab = (D/1000) * γ_c");
                sw.WriteLine("                         = ({0:f2}) * {1:f0}", (D / 1000), gamma_c);
                sw.WriteLine("                         = {0:f2} kN/sq.mm", self_weight_deck_slab);
                sw.WriteLine();

                double self_weight_wearing_course = (Dwc / 1000.0) * gamma_wc;
                sw.WriteLine("Self weight of wearing course = (Dwc/1000) * γ_wc");
                sw.WriteLine("                              = {0:f2} * {1}", (Dwc / 1000), gamma_wc);
                sw.WriteLine("                              = {0:f2} kN/sq.mm", self_weight_wearing_course);
                sw.WriteLine();
                double DL = self_weight_deck_slab + self_weight_wearing_course;

                sw.WriteLine("Total Load = DL ");
                sw.WriteLine("           = {0:f2} + {1:f2}", self_weight_deck_slab, self_weight_wearing_course);
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                DL = (int)DL;
                DL += 1.0;
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                #endregion

                #region STEP 2 : BENDING MOMENT BY MOVING LOAD
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : BENDING MOMENT BY MOVING LOAD");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Load = WL = {0} kN", WL);
                sw.WriteLine("Panel Dimension L = {0:f2} m,                B = {1:f2} m", L, B);
                sw.WriteLine("Load Dimension v = {0:f2}, u = {1:f2} m", v, u);
                sw.WriteLine();
                sw.WriteLine("Considering 45° Load dispersion through wearing Course");
                sw.WriteLine();

                double _v = v + (2 * (Dwc / 1000.0));
                sw.WriteLine("    v = {0:f2} + (2*{1:f2}) = {2:f2} m.", v, (Dwc / 1000.0), _v);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _v);
                double _u = u + (2 * (Dwc / 1000.0));
                sw.WriteLine("    u = {0:f2} + (2*{1:f2}) = {2:f2} m.", u, (Dwc / 1000.0), _u);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _u);

                double u_by_B = 0.0;

                u_by_B = v;
                v = _v;
                _v = u_by_B;

                u_by_B = u;
                u = _u;
                _u = u_by_B;



                u_by_B = u / B;
                sw.WriteLine("    u / B = {0:f2} / {1:f2} = {2:f3}", u, B, u_by_B);
                sw.WriteLine();
                double v_by_L = v / L;

                sw.WriteLine("    v / L = {0:f2} / {1:f2} = {2:f3}", v, L, v_by_L);
                sw.WriteLine();

                double k = B / L;
                sw.WriteLine("    K = B / L = {0:f2} / {1:f2} = {2:f3}", B, L, k);
                sw.WriteLine();


                k = Double.Parse(k.ToString("0.0"));
                if (k < 0.4)
                    k = 0.4;
                if (k > 1.0) k = 1.0;
                f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);
                if (k == 0.4)
                {
                    f_c.txt_m1.Text = "0.085";
                    f_c.txt_m2.Text = "0.017";
                }
                f_c.ShowDialog();
                double m1, m2;
                m1 = f_c.m1;
                m2 = f_c.m2;

                sw.WriteLine();
                sw.WriteLine("From Pigeaud’s Curves, for K = {0:f1}", k);
                sw.WriteLine("    m1 = {0}", m1);
                sw.WriteLine("    m2 = {0}", m2);

                double _MB = WL * (m1 + 0.15 * m2);
                _MB = double.Parse(_MB.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WL * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0} * ({1} + 0.15 * {2})", WL, m1, m2);
                sw.WriteLine("                          = {0} kN-m", _MB);
                sw.WriteLine();

                double MB1 = IF * CF * _MB;
                MB1 = double.Parse(MB1.ToString("0"));

                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = MB1");
                sw.WriteLine("  = IF * CF * MB’ ");
                sw.WriteLine("  = {0} * {1:f2} * {0:f2} ", IF, CF, _MB);
                sw.WriteLine("  = {0} kN-m", MB1);
                sw.WriteLine();

                double _ML = WL * (m2 + 0.15 * m1);

                sw.WriteLine("Long Span Bending Moment = ML’ ");
                sw.WriteLine("                         = WL * (m2 + 0.15 * m1) ");
                sw.WriteLine("                         = {0} * ({1} + 0.15 * {2}) ", WL, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML1 = IF * CF * _ML;
                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = ML1");
                sw.WriteLine("  = IF * CF * ML’ ");
                sw.WriteLine("  = {0} * {1} * {2:f2} ", IF, CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML1);
                sw.WriteLine();
                #endregion

                #region STEP 3 : BENDING MOMENT BY Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : BENDING MOMENT BY PERMANENT LOAD");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Permanent Load of Deck Slab = DL = {0} kN/sq.mm", DL);

                double WD = DL * B * L;
                sw.WriteLine("Permanent Load per Panel = WD");
                sw.WriteLine("                     = DL * B * L");
                sw.WriteLine("                     = {0} * {1} * {2}", DL, B, L);
                sw.WriteLine("                     = {0:f2} kN", WD);
                sw.WriteLine();
                sw.WriteLine("u / B = 1 and  v / L = 1");

                k = B / L;
                k = Double.Parse(k.ToString("0.000"));
                sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f1}", B, L, k);
                sw.WriteLine("1/k = 1 / {0} = {1:f2}", k, (1 / k));

                f_c = new frmCurve(k, 1.0, 1.0, LoadType.FullyLoad);

                k = Double.Parse(k.ToString("0.0"));
                if (k == 0.4)
                {
                    f_c.txt_m1.Text = "0.047";
                    f_c.txt_m2.Text = "0.006";
                }
                f_c.ShowDialog();

                m1 = f_c.m1;
                m2 = f_c.m2;
                double MB, ML;

                sw.WriteLine();
                sw.WriteLine("Using Pigeaud’s Curves, m1 = {0} and m2 = {1}", m1, m2);
                sw.WriteLine();
                _MB = WD * (m1 + 0.15 * m2);
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WD * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0:f2} * ({1} + 0.15 * {2})", WD, m1, m2);
                sw.WriteLine("                          = {0:f2} kN-m", _MB);
                sw.WriteLine();


                sw.WriteLine("Short Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = MB2");

                double MB2 = CF * _MB;
                sw.WriteLine("  = CF * MB’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _MB);
                sw.WriteLine("  = {0:f2} kN-m", MB2);
                sw.WriteLine();

                _ML = WD * (m2 + 0.15 * m1);
                sw.WriteLine("Long Span Bending Moment = ML’");
                sw.WriteLine("                         = WD * (m2 + 0.15 * m1)");
                sw.WriteLine("                         = {0:f2} * ({1} + 0.15 * {2})", WD, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML2 = CF * _ML;
                sw.WriteLine("Long Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = ML2");
                sw.WriteLine("  = CF * ML’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine("Design Bending Moments are:");
                MB = MB1 + MB2;

                sw.WriteLine("Along Short Span = MB");
                sw.WriteLine("                 = MB1 + MB2");
                sw.WriteLine("                 = {0:f2} + {1:f2}", MB1, MB2);
                sw.WriteLine("                 = {0:f2} kN-m", MB);
                sw.WriteLine();


                ML = ML1 + ML2;
                sw.WriteLine("Along Long Span = ML");
                sw.WriteLine("                = ML1 + ML2");
                sw.WriteLine("                = {0:f2} + {1:f2}", ML1, ML2);
                sw.WriteLine("                = {0:f2} kN-m", ML);
                sw.WriteLine();

                #endregion

                #region STEP 4 : DESIGN OF SECTION FOR RCC DECK SLAB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : DESIGN OF SECTION FOR RCC DECK SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();



                double d = (MB * 10E5) / (Q * 1000.0);

                d = Math.Sqrt(d);
                d = double.Parse(d.ToString("0"));
                sw.WriteLine("d = √((MB * 10^6) / (Q*b))");
                sw.WriteLine("  = √(({0:f2} * 10^6) / ({1:f3}*1000))", MB, Q);
                sw.WriteLine("  = {0} mm", d);
                sw.WriteLine();

                sw.WriteLine("The overall depth of RCC Deck Slab = {0} mm", D);

                double _d = d;
                d = D - 40.0;
                sw.WriteLine();
                sw.WriteLine("Effective Depth = {0} - 40 = {1} mm = d", D, d);


                double Ast = MB * 10E5 / (sigma_st * j * d);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Required steel along short span");
                sw.WriteLine("    = Ast");
                sw.WriteLine("    = (MB * 10^6) / (σ_st * j * d)");
                sw.WriteLine("    = ({0:f2} * 10^6) / ({1} * {2} * {3})", MB, sigma_st, j, d);
                sw.WriteLine("    = {0} sq.mm", Ast);

                List<double> lst_dia = new List<double>();

                lst_dia.Add(10);
                lst_dia.Add(12);
                lst_dia.Add(16);
                lst_dia.Add(20);
                lst_dia.Add(25);
                lst_dia.Add(32);


                int dia_indx = 0;
                double dia = lst_dia[0];
                double _ast = 0.0;
                double no_bar = 0.0;
                double spacing = 140;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0} mm bars at {1:f0} mm c/c.     Marked as (1) in the Drawing", dia, spacing);
                //(1) = T12 mm bars at 140 mm c/c.
                _1 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                sw.WriteLine();

                sw.WriteLine("Effective depth for Long span using T10 mm bars");
                sw.WriteLine();

                double d1 = d - (dia / 2.0) - (10.0 / 2.0);
                sw.WriteLine("    d1 = d - ({0:f0}/2) - (10/2)", dia);
                sw.WriteLine("       = {0} - {1:f0} - 5", d, (dia / 2.0));
                sw.WriteLine("       = {0:f0} mm", d1);
                sw.WriteLine();

                Ast = (ML * 10E5) / (sigma_st * j * d1);
                Ast = double.Parse(Ast.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Required steel along long span");
                sw.WriteLine("  = Ast");
                sw.WriteLine("  = ML * 10^6 / (σ_st * j * d1)");
                sw.WriteLine("  = {0:f2} * 10^6 / ({1} * {2} * {3})", ML, sigma_st, j, d1);
                sw.WriteLine("  = {0} sq.mm", Ast);
                sw.WriteLine();

                spacing = 150;
                dia_indx = 0;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine("Provide T{0:f0} Bars at {1:f0} mm c/c.    Marked as (2) in the Drawing", dia, spacing);
                //(2) = T10 Bars at 150 mm c/c.
                _2 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                #endregion

                #region STEP 5 : DESIGN OF STEEL PALTE GIRDER
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : DESIGN OF STEEL PLATE GIRDER");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.1 : Load Computation:");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Spacing of Main Girder = B = {0} m", B);
                sw.WriteLine();
                sw.WriteLine("Spacing of Cross Girder = L = {0} m", L);
                sw.WriteLine();

                double deck_slab_load = DL * B;
                sw.WriteLine("Deck slab Load on Girder = DL * B");
                sw.WriteLine("                         = {0} * {1}", DL, B);
                sw.WriteLine("                         = {0:f2} kN/m", deck_slab_load);
                sw.WriteLine();

                double self_weight = (int)(0.2 * S);
                self_weight += 1;
                sw.WriteLine("Self weight of Main Girder = 0.2 * S + 1");
                sw.WriteLine("                           = 0.2 * {0} + 1", S);
                sw.WriteLine("                           = {0} kN/m", self_weight);
                sw.WriteLine();

                double W1 = deck_slab_load + self_weight;
                sw.WriteLine("Total Load = W1");
                sw.WriteLine("           = {0:f2} + {1:f2}", deck_slab_load, self_weight);
                sw.WriteLine("           = {0:f2} kN/m", W1);
                sw.WriteLine();

                sw.WriteLine("Self weight of Cross Girders assumed as 1 kN/m");
                sw.WriteLine();

                double W2 = B * 1;
                sw.WriteLine("    = W2");
                sw.WriteLine("    = B * 1");
                sw.WriteLine("    = {0} * 1 ", B);
                sw.WriteLine("    = {0:f2} kN ", W2);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.2 : Bending Moments by Permanent Loads");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("The Maximum Permanent Load moment");
                sw.WriteLine();

                double M1 = ((W1 * S * S) / 8.0) + ((W2 * S) / 4.0) + W2 * L;
                sw.WriteLine("  = M1 = [(W1*S*S)/8] +  [(W2*S)/4] + W2 * L");
                sw.WriteLine("  = [({0:f2}*{1}*{1})/8] +  [({2:f2}*{1})/4] + {2:f2} * {3}", W1, S, W2, L);
                sw.WriteLine("  = {0:f2} kN-mm", M1);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.3 : Moving Load Moments");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine(" In the span of S = {0} m let us place Load  = WL = {1} kN ", S, WL);
                sw.WriteLine(" of length v  = {0} m at the centre of span of Main Girder ", _v);
                sw.WriteLine("so centre of Load = S / 2 = {0:f2} m from either ends.", (S / 2));
                sw.WriteLine();

                sw.WriteLine("Maximum Moving Load Bending Moment");
                double M2 = (WL / 2.0) * (S / 2.0) - (WL / 2.0) * (_v / 4.0);
                sw.WriteLine("M2 = (WL/2) * (S/2) - (WL/2) * (L/4)");
                sw.WriteLine("   = ({0}/2) * ({1}/2) - ({0}/2) * ({2}/4)", WL, S, L);
                sw.WriteLine("   = {0:f2} kN-m", M2);
                M2 = double.Parse(M2.ToString("0"));
                sw.WriteLine("   = {0} kN-m", M2);
                sw.WriteLine();
                //sw.WriteLine("       = (350/2) * (18/2) - (350/2) * (3.6/4) = 1417.5 = 1418 KN m");


                sw.WriteLine("Considering 10% increase");
                sw.WriteLine();

                double bend_mom_mov_load = M2 * 1.10;

                M2 = Double.Parse(M2.ToString("0"));
                M1 = Double.Parse(M1.ToString("0"));
                bend_mom_mov_load = Double.Parse(bend_mom_mov_load.ToString("0"));
                sw.WriteLine("Bending Moment by Moving Load = M2 * 1.10 ");
                sw.WriteLine("                              = {0:f0} * 1.10", M2);
                sw.WriteLine("                              = {0:f0} kN-m", bend_mom_mov_load);
                sw.WriteLine();

                sw.WriteLine("Bending Moment by Permanent Load = M1 = {0:f0} kN-m", M1);
                sw.WriteLine();

                double M = bend_mom_mov_load + M1;
                sw.WriteLine("Design Bending Moment = M = {0} + {1} = {2} kN-m", bend_mom_mov_load, M1, M);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.4 : Shear Forces:");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double shr_frc_load = (W1 * S) / 2.0 + (W2 * 3.0) / 2.0;
                shr_frc_load = Double.Parse(shr_frc_load.ToString("0"));
                sw.WriteLine("Permanent Load Shear Force = (W1 * S) /2 + (W2 * 3) /2");
                sw.WriteLine("               = ({0} * {1}) /2 + ({2}*3) /2", W1, S, W2);
                sw.WriteLine("               = {0} kN", shr_frc_load);
                sw.WriteLine();


                v = Double.Parse(v.ToString("0.00"));
                double mv_ld_sh_fact = 1.10 * (WL * (S - (v / 2)) / S);
                mv_ld_sh_fact = Double.Parse(mv_ld_sh_fact.ToString("0"));

                sw.WriteLine("Moving Load Shear with factor = 1.10 * (WL * (S - (v/2)) / S");
                sw.WriteLine("                   = 1.10 * ({0} * ({1} - {2:f2})) / {1}", WL, S, (v / 2.0));
                //sw.WriteLine("                   = 1.10 * 350 * 16.2 / 18");
                sw.WriteLine("                   = {0} kN", mv_ld_sh_fact);
                sw.WriteLine();

                double deg_sh_frc = shr_frc_load + mv_ld_sh_fact;
                sw.WriteLine("Design shear force = v");
                sw.WriteLine("                   = {0} + {1}", shr_frc_load, mv_ld_sh_fact);
                sw.WriteLine("                   = {0} kN", deg_sh_frc);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.5 : Size of Web Plate and Flange Plate");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Approximate depth of Girder = S /10 = {0} / 10 = {1} m", S, (S / 10.0));
                sw.WriteLine();

                double eco_depth_girder = (M * 10E5) / sigma_b;

                eco_depth_girder = Math.Pow(eco_depth_girder, (1.0 / 3.0));
                eco_depth_girder = 5.0 * eco_depth_girder;


                sw.WriteLine("Economical depth of Girder = 5 * (M / σb)^(1/3)");
                sw.WriteLine("                           = 5 * ({0} * 10^6 / {1})^(1/3)", M, sigma_b);
                eco_depth_girder = Double.Parse(eco_depth_girder.ToString("0"));
                sw.WriteLine("                           = {0} mm", eco_depth_girder);
                sw.WriteLine();

                sw.WriteLine("Web depth by 10 mm thick plate for shear considerations");
                sw.WriteLine();
                double web_depth = (deg_sh_frc * 1000) / (tau * 10.0);
                sw.WriteLine("    = v / (τ * 10)");
                sw.WriteLine("    = {0} * 1000 / ({1}*10)", deg_sh_frc, tau);
                web_depth = Double.Parse(web_depth.ToString("0.00"));
                sw.WriteLine("    = {0} mm", web_depth);
                sw.WriteLine();

                double dw, tw;
                dw = 1000.0;
                tw = 10.0;
                double Aw = dw * tw;
                sw.WriteLine("Let us try Web as {0} mm * {1} mm = dw * tw = Aw       Marked as (3) in the Drawing", dw, tw);

                //(3) = Web depth x thickness = 1000 mm * 10 mm
                _3 = string.Format("Web depth x thickness = {0} mm * {1} mm", dw, tw);


                sw.WriteLine("Approximate Flange area required");
                double Af = ((M * 10E5) / (sigma_b * dw)) - (Aw / 6);
                Af = Double.Parse(Af.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Af = (M / (σ_b * dw)) - (Aw / 6)");
                sw.WriteLine("   = ({0} * 10^6) / ({1} * {2})) - (dw * tw) / 6", M, sigma_b, dw);
                sw.WriteLine("   = ({0} * 10^6) / ({1} * {2})) - ({2} * {3}) / 6", M, sigma_b, dw, tw);
                sw.WriteLine("   = {0} sq.mm", Af);
                sw.WriteLine();

                sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                sw.WriteLine("             = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);

                double Bf1 = S * 1000 / 40.0;
                double Bf2 = S * 1000 / 45.0;
                double Bf = (Bf1 > Bf2) ? Bf1 : Bf2;

                Bf = (int)(Bf / 100.0);
                Bf += 1;
                Bf *= 100.0;

                sw.WriteLine("             = {0:f0}  to  {1:f0} mm, Try Bf = {2} mm", Bf1, Bf2, Bf);
                sw.WriteLine();

                double tf = 30.0;

                sw.WriteLine("So, Let us try Flange Plate as {0} mm * {1} mm =  Bf * tf     Marked as (4) in the Drawing", Bf, tf);
                //(4) = Flange width x thickness = 500 mm * 30 mm

                _4 = string.Format("Flange width x thickness = {0} mm * {1} mm", Bf, tf);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.6 : Check for Maximum Stresses");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double I = (tw * dw * dw * dw / 12.0) + (2 * (tf * Bf) * ((dw / 2.0)
                           + (tf / 2.0)) * ((dw / 2.0) + (tf / 2.0)));
                I = Double.Parse(I.ToString("0"));
                double y = (dw / 2.0) + tf;

                double appl_stress = (M * 10E5 * y) / I;
                appl_stress = Double.Parse(appl_stress.ToString("0"));
                sw.WriteLine("Applied Stress = M * y  / I");
                sw.WriteLine();

                sw.WriteLine("Where, I = (tw * dw**3 / 12) + 2 * (tf * Bf) * (dw / 2 + tf /2)^2");
                sw.WriteLine("         = ({0} * {1}^3 / 12) + 2 * ({2} * {3}) * ({1} / 2 + {2} /2)^2", tw, dw, tf, Bf);
                I = I / 10E6;
                I = Double.Parse(I.ToString("0"));
                sw.WriteLine("         = {0} * 10E6 sq.sq.mm", I);
                sw.WriteLine();
                sw.WriteLine(" and y = dw / 2 + tf");
                sw.WriteLine("       = {0} + {1}", (dw / 2.0), tf);
                sw.WriteLine("       = {0} mm", y);
                sw.WriteLine();


                sw.WriteLine("So,  Applied Stress = {0} * 10^6 * {1} / ({2} * 10E6)", M, y, I);
                if (appl_stress < sigma_b)
                {
                    sw.WriteLine("                    = {0} N/sq.mm < σ_b = {1} N/sq.mm, OK", appl_stress, sigma_b);
                }
                else
                {
                    sw.WriteLine("                    = {0} N/sq.mm > σ_b = {1} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b);
                }
                sw.WriteLine();

                u_by_B = deg_sh_frc;
                deg_sh_frc = v;
                v = u_by_B;

                double tau1 = v * 1000 / (dw * tw);
                tau1 = double.Parse(tau1.ToString("0"));
                sw.WriteLine("Average Shear Stress = τ1 ");
                sw.WriteLine("                     = v * 1000 / (dw * tw)");
                sw.WriteLine("                     = {0} * 1000 / ({1} * {2})", v, dw, tw);
                sw.WriteLine("                     = {0} N/sq.mm", tau1);
                sw.WriteLine();

                double ratio = (dw / tw);
                sw.WriteLine("Ratio dw / tw = {0} / {1} = {2}", dw, tw, ratio);
                sw.WriteLine();
                sw.WriteLine("Considering Stiffener Spacing = c = dw = {0} mm", dw);
                sw.WriteLine();

                // Calculate from Table 1
                // **Problem How to calculate value from Table1 ?
                double tau2 = Get_Table_1_Value(100, 1);
                //double tau2 = 87;
                sw.WriteLine("From Table 1 (Given at the end of the Report) Allowable ");
                sw.WriteLine("average Shear Stress = {0} N/Sq mm = τ2", tau2);

                if (tau2 > tau1)
                {
                    sw.WriteLine("As τ2 > τ1 so, Average shear stress is within Safe permissible Limits, OK.");
                }
                else
                {
                    sw.WriteLine("As τ2 < τ1 so, Average shear stress is not within Safe permissible limits, NOT OK.");
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.7 : Connection Between Flange and Web");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Maximum Shear Force at the junction of Flange and Web is given by");
                sw.WriteLine();

                sw.WriteLine("    τ  = v * a * y  / I");
                double a = Bf * tf;
                y = dw / 2.0 + (tf / 2.0);
                I = double.Parse(I.ToString("0"));

                sw.WriteLine("    a = Bf * tf = {0} * {1} = {2:f2} sq.mm", Bf, tf, a);
                sw.WriteLine();
                sw.WriteLine("    y = dw/2 + tf/2 = {0}/2 + {1}/2 = {2} mm", dw, tf, y);
                sw.WriteLine();
                sw.WriteLine("    I = {0} * 10E6 sq.sq.mm", I);
                sw.WriteLine();
                sw.WriteLine("    v = {0} * 1000 N", v);
                sw.WriteLine();

                tau = (v * 1000 * a * y) / (I * 10E6);
                tau = double.Parse(tau.ToString("0"));
                //sw.WriteLine("τ = 548 * 1000 * 15000 * 515 / (879 * 107) = 483 N/mm");
                sw.WriteLine("    τ  = {0} * 1000 * {1} * {2}  / ({3} * 10E6)", v, a, y, I);
                sw.WriteLine("       = {0} N/mm", tau);
                sw.WriteLine();

                sw.WriteLine("Adopting Continuous weld on either side, strength of weld of sizw ");
                sw.WriteLine();
                sw.WriteLine("  ‘S’ = 2 * k * S * σ_tf");

                double _S = 2 * K * sigma_tf;
                _S = double.Parse(_S.ToString("0"));
                sw.WriteLine("      = 2 * {0} * S * {1}", K, sigma_tf);
                sw.WriteLine("      = {0} * S", _S);
                sw.WriteLine();


                sw.WriteLine("Equating, {0} * S = {1},                S = {1} / {0} = {2:f2} mm", _S, tau, (tau / _S));
                sw.WriteLine();

                _S = tau / +_S;

                _S = (int)_S;
                _S += 2;

                sw.WriteLine("Use {0} mm Fillet Weld, continuous on either side.     Marked as (5) in the Drawing", _S);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.8 : Intermediate Stiffeners");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double val1, val2;

                val1 = dw / tw;
                if (val1 < 85)
                {
                    sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                    sw.WriteLine("So, Vertical Stiffeners are required");
                    //sw.WriteLine("else, Vertical Stiffeners are not required.");
                }
                else
                {
                    sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                    //sw.WriteLine("So, Vertical Stiffeners are required");
                    sw.WriteLine("So, Vertical Stiffeners are not required.");
                }
                sw.WriteLine();

                double sp_stifn1 = 0.33 * dw;
                double sp_stifn2 = 1.5 * dw;
                sw.WriteLine("Spacing of Stiffeners = 0.33 * dw  to  1.5 * dw");
                sw.WriteLine("            = 0.33 * {0}  to  1.5 * {0}", dw);
                sw.WriteLine("            = {0} mm to {1} mm", sp_stifn1, sp_stifn2);
                sw.WriteLine();

                double c = 1000;

                sw.WriteLine("Adopt Spacing = c = {0} mm", c);
                sw.WriteLine();


                sw.WriteLine("Required minimum Moment of Inertia of Stiffeners");
                sw.WriteLine();


                double _I = ((1.5 * dw * dw * dw * tw * tw * tw) / (c * c));
                sw.WriteLine("I = 1.5 * dw**3 * tw**3 / c**2");
                sw.WriteLine("  = 1.5 * {0}^3 * {1}^3 / {2}^2", dw, tw, c);
                _I = _I / 10E4;
                _I = double.Parse(_I.ToString("0"));
                sw.WriteLine("  = {0} * 10E4 sq.sq.mm", _I);
                sw.WriteLine();

                double t = 10; // t is Constant?

                sw.WriteLine("Use 10 mm thick plate, t = {0} mm", t);
                sw.WriteLine();
                sw.WriteLine("Maximum width of plate not to exceed = 12 * t = {0} mm", (12 * t));
                sw.WriteLine();

                // 80 ?
                double h = 80;
                sw.WriteLine("Use 80 mm size plate, h = 80 mm");
                sw.WriteLine();
                sw.WriteLine("Plate size is {0} mm * 10 mm      Marked as (6) in the Drawing", h);
                _6 = string.Format("{0} mm x 10 mm", h);



                sw.WriteLine();

                double _I1 = (10.0 * (80.0 * 80.0 * 80.0)) / 3.0;
                _I1 = _I1 / 10E4;
                _I1 = double.Parse(_I1.ToString("0"));

                if (_I1 > _I)
                {
                    sw.WriteLine("I = 10 * 80**3 / 3 = 17 * 10E4 Sq Sq mm > 15 * 10E4 Sq Sq mm,                OK");
                }
                else
                {
                    sw.WriteLine("I = 10 * 80**3 / 3 = 17 * 10E4 sq.sq.mm < 15 * 10E4 sq.sq.mm,NOT OK");
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.9 : Connections of Vertical Stiffener to Web");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Shear on weld connecting stiffener to Web");
                sw.WriteLine();

                // 125 = constant ?
                double sh_wld_wb = 125 * tw * tw / h;
                sw.WriteLine("    = 125 * tw*tw / h");
                sw.WriteLine("    = 125 * {0}*{0} / {1}", tw, h);

                sh_wld_wb = double.Parse(sh_wld_wb.ToString("0.00"));
                sw.WriteLine("    = {0} kN/m", sh_wld_wb);
                sw.WriteLine("    = {0} N/mm", sh_wld_wb);
                sw.WriteLine();

                double sz_wld = sh_wld_wb / (K * sigma_tf);
                sz_wld = double.Parse(sz_wld.ToString("0.00"));
                sw.WriteLine("Size of welds = 156.25 / (K * σ_tf)");
                sw.WriteLine("              = {0} / ({1} * {2})", sh_wld_wb, K, sigma_tf);
                sw.WriteLine("              = {0} mm", sz_wld);
                sw.WriteLine();

                //sw.WriteLine("Size of welds = 156.25 / (K * σtf) = 156.25 / (0.7 * 102.5) = 2.17 mm");
                // How come 100 and 5?
                sw.WriteLine("Use 100 mm Long 5 mm Fillet Welds alternately on either side.     Marked as (7) in the Drawing");

                //(7)  5-100-100 (weld)
                _7 = string.Format("5-100-100 (weld)");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.10 : End Bearing Stiffeners");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Maximum Shear Force = v = {0} kN", v);
                sw.WriteLine();

                val1 = (h / t);
                if (val1 < 12)
                {
                    sw.WriteLine("The end bearing Stiffeners is designed as a column h / t < 12");
                }
                else
                {
                    sw.WriteLine("The end bearing Stiffeners is designed as a column h / t > 12");
                }
                sw.WriteLine();
                h = 180;
                sw.WriteLine("Use ‘h’ = outstand of stiffeners = {0} mm", h);
                sw.WriteLine();
                t = h / 12;
                sw.WriteLine("t = {0} / 12 = {1} mm", h, (h / 12.0));
                sw.WriteLine();
                sw.WriteLine("Use plate of size 180 mm * 15 mm     Marked as (8) in the Drawing");

                //(8)  180 x15 mm

                _8 = string.Format("180 x 15 mm");
                sw.WriteLine();

                double brng_ar_req = v * 1000 / sigma_p;
                sw.WriteLine();
                sw.WriteLine("Bearing area required = v * 1000 / σ_p");
                sw.WriteLine("                      = {0} * 1000 / {1} sq.mm", v, sigma_p);
                brng_ar_req = double.Parse(brng_ar_req.ToString("0"));
                sw.WriteLine("                      = {0} sq.mm", brng_ar_req);
                sw.WriteLine();

                double tot_area = 2 * h * t;
                sw.WriteLine("If two plates are used,");
                sw.WriteLine("     Total area = 2 * {0} * {1}", h, t);
                if (tot_area > brng_ar_req)
                {
                    sw.WriteLine("                = {0} sq.mm > {1} sq.mm", tot_area, brng_ar_req);
                }
                else
                {
                    sw.WriteLine("                = {0} sq.mm < {1} sq.mm", tot_area, brng_ar_req);
                }
                sw.WriteLine();

                sw.WriteLine("The length of Web plate which acts along with Stiffener ");
                sw.WriteLine("plates in bearing the reaction = lw = 20 * tw");
                sw.WriteLine("                               = 20 * {0}", tw);
                double brng_reaction = 20 * tw;
                double lw = brng_reaction;
                sw.WriteLine("                               = {0} mm", lw);
                sw.WriteLine();
                _I = ((t * (2 * h + 10) * (2 * h + 10) * (2 * h + 10)) / 12) + (2 * lw * tw * tw * tw / 12);

                //**lw = ?
                sw.WriteLine("    I = t * (2 * h + 10)^3 / 12 + 2 * lw * tw**3 / 12");
                sw.WriteLine("      = {0} * (2 * {1} + 10)^3 / 12 + 2 * {2} * {3}^3 / 12", t, h, 200, tw);
                //sw.WriteLine("      = 15 * 3703 / 12 + 2 * 200 * 103 / 12");
                _I = (_I / 10E3);
                _I = double.Parse(_I.ToString("0"));

                sw.WriteLine("      = {0} * 10E3 Sq Sq mm", _I);
                sw.WriteLine();

                double A = 2 * h * t + 2 * lw * tw;
                A = double.Parse(A.ToString("0"));
                sw.WriteLine("    Area = A = 2 * h * t + 2 * lw * tw");
                sw.WriteLine("         = 2 * {0} * {1} + 2 * {2} * {3}", h, t, lw, tw);
                sw.WriteLine("         = {0} sq.mm", A);

                sw.WriteLine();

                double r = (_I * 10E3) / A;
                r = Math.Sqrt(r);
                r = double.Parse(r.ToString("0"));
                sw.WriteLine("    r = √(I / A) = √({0} * 10E3 / {1}) = {2} mm", _I, A, r);
                sw.WriteLine();


                // ** 0.7 = ?
                double _L = 0.7 * dw;
                double lamda = (_L / r);
                lamda = double.Parse(lamda.ToString("0.00"));
                sw.WriteLine("    λ = Slenderness ratio = L / r");
                sw.WriteLine();
                sw.WriteLine("    L = Effective Length of stiffeners");
                sw.WriteLine("      = 0.7 * dw");
                sw.WriteLine("      = 0.7 * {0}", tw);
                sw.WriteLine("      = {0} mm", _L);
                sw.WriteLine();
                sw.WriteLine("    λ = {0} / {1}", _L, r);
                sw.WriteLine("      = {0}", lamda);

                sw.WriteLine();

                sw.WriteLine("    Ref. Table 2 (given at the end of the Report)");

                double sigma_ac = Get_Table_2_Value(lamda, 1);
                sigma_ac = double.Parse(sigma_ac.ToString("0"));
                sw.WriteLine("    Permissible Stress in axial compression σ_ac = {0} N/sq.mm", sigma_ac);
                sw.WriteLine();

                double area_req = v * 1000 / sigma_ac;
                area_req = double.Parse(area_req.ToString("0"));
                sw.WriteLine("    Area required = v * 1000 / σ_ac ");
                sw.WriteLine("                  = {0} * 1000 / {1}", v, sigma_ac);
                if (area_req < A)
                {
                    sw.WriteLine("                  = {0} sq.mm < {1} sq.mm,  Ok", area_req, A);
                }
                else
                {
                    sw.WriteLine("                  = {0} sq.mm > {1} sq.mm,  NOT OK", area_req, A);
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.11 : Connection between Bearing Stiffener and Web");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //** 40 = ?
                double len_alt = 2 * (dw - 40);
                sw.WriteLine("Length available for alternate intermittent weld");
                sw.WriteLine("   = 2 * (dw - 40)");
                sw.WriteLine("   = 2 * ({0} - 40)", dw);
                sw.WriteLine("   = {0} mm", len_alt);
                sw.WriteLine();

                double req_strnth_wld = (v * 1000 / len_alt);
                req_strnth_wld = double.Parse(req_strnth_wld.ToString("0"));
                sw.WriteLine("Required strength of weld = v * 1000 / 1920");
                sw.WriteLine("                          = {0} * 1000 / {1}", v, len_alt);
                sw.WriteLine("                          = {0} N/mm", req_strnth_wld);
                sw.WriteLine();

                sz_wld = req_strnth_wld / (K * sigma_tf);
                sz_wld = double.Parse(sz_wld.ToString("0.00"));


                //** σ_ac =  138 but 102.5 = ?
                //sw.WriteLine("Size of weld = 286 / (K * σ_ac) = 286 / (0.7 * 102.5) = 3.98 mm");
                sw.WriteLine("Size of weld = 286 / (K * σ_tf)");
                sw.WriteLine("             = {0} / ({1} * {2})", req_strnth_wld, K, sigma_tf);
                sw.WriteLine("             = {0} mm", sz_wld);
                sw.WriteLine();

                if (sz_wld < 5)
                    sz_wld = 5;
                else
                {
                    sz_wld = (int)sz_wld;
                    sz_wld += 1;
                }
                sw.WriteLine("Use {0} mm Fillet Weld", sz_wld);
                sw.WriteLine();

                double len_wld = 10 * tw;

                sw.WriteLine("Length of Weld >= 10 * tw = 10 * {0} = {1} mm", tw, len_wld);
                sw.WriteLine();

                sw.WriteLine("Use {0} mm Long, {1} mm Weld Alternately.     Marked as (9) in the Drawing", len_wld, sz_wld);
                sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5.12 : Properties of Composite Section");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double Ace = B * 1000 * D / m;
                Ace = double.Parse(Ace.ToString("0"));

                sw.WriteLine("Ace = B * 1000 * D/m");
                sw.WriteLine("    = {0} * 1000 * {1}/{2}", B, D, m);
                sw.WriteLine("    = {0} sq.mm", Ace);
                sw.WriteLine();
                sw.WriteLine("The centroid of Composite Section (Neutral Axis) is determined");
                sw.WriteLine("by first moment of the areas about axis xx,");


                double Axy = Ace * (dw + 2 * tf + D / 2) + Bf * tf * (dw + tf + tf / 2) + dw * tw * (dw / 2 + tf) + Bf * tf * tf / 2;

                sw.WriteLine();
                sw.WriteLine("Axy  = Ace * (dw + 2 * tf + D/2) + Bf * tf * (dw + tf + tf/2) ");
                sw.WriteLine("       + dw * tw * (dw/2 +tf) + Bf * tf * tf/2");
                sw.WriteLine();
                sw.WriteLine("     = {0} * ({1} + 2 * {2} + {3}/2) + {4} * {2} * ({1} + {2} + {2}/2) ", Ace, dw, tf, D, Bf);
                sw.WriteLine("       + {0} * {1} * ({0}/2 + {2}) + {3} * {2} * {2}/2", dw, tw, tf, Bf);
                sw.WriteLine();

                Axy = double.Parse(Axy.ToString("0"));
                sw.WriteLine("     = {0}", Axy);
                //sw.WriteLine("= 77046340");
                double _A_d = Ace + (dw / 2) * tf + dw * tw + (dw / 2.0) * tf;
                _A_d = double.Parse(_A_d.ToString("0"));
                // ** formula ?
                sw.WriteLine();
                sw.WriteLine("A = Ace + (dw / 2) * tf + dw * tw + (dw / 2.0) * tf");
                sw.WriteLine("  = {0} + ({1} / 2) * {2} + {0} * {3} + ({1} / 2.0) * {2}", Ace, dw, tf, tw);
                sw.WriteLine("  = {0} sq.mm", _A_d);
                sw.WriteLine();

                y = Axy / _A_d;
                // ** sign y bar ?
                sw.WriteLine("  y = Axy / A = {0} / {1}", Axy, _A_d);
                //sw.WriteLine("    = {0:f0}", y);

                y = double.Parse(y.ToString("0"));
                sw.WriteLine("    = {0} mm", y);
                sw.WriteLine();

                double yc = dw + 2 * tf + D / 2 - y;

                sw.WriteLine("  yc = dw + 2 * tf + D/2 -  y");
                sw.WriteLine("     = {0} + 2 * {1} + {2}/2 -  {3}", dw, tf, D, y);
                sw.WriteLine("     = {0} mm", yc);
                sw.WriteLine();


                double Icomp = Ace * yc * yc +
                    (Bf * (dw + (2 * tf)) * (dw + (2 * tf)) * (dw + (2 * tf))) / 12.0
                    - ((Bf - tw) * dw * dw * dw) / 12.0 +
                    (_A_d - Ace) * (y - (dw / 2.0) - tf) * (y - (dw / 2.0) - tf);


                sw.WriteLine("Icomp = distance from centre of Deck Slab to Centroid of Composite Section");

                sw.WriteLine("      = Ace * yc * yc ");
                sw.WriteLine("        + (Bf * (dw + (2 * tf))^3 ) / 12.0");
                sw.WriteLine("        - ((Bf - tw) * dw**3) / 12.0 ");
                sw.WriteLine("        + (A - Ace) * (y - (dw / 2.0) - tf) * (y - (dw / 2.0) - tf)");
                sw.WriteLine();

                sw.WriteLine("      = {0} * {1} * {1} ", Ace, yc);
                sw.WriteLine("        + ({0} * ({1} + (2 * {2}))^3 ) / 12.0", Bf, dw, tf);
                sw.WriteLine("        - (({0} - {1}) * {2}^3) / 12.0 ", Bf, tw, dw);
                sw.WriteLine("        + ({0} - {1}) * ({2} - ({3} / 2.0) - {4}) * ({2} - ({3} / 2.0) - {4})", _A_d, Ace, y, dw, tf);
                sw.WriteLine();




                Icomp = Icomp / 10E9;
                Icomp = double.Parse(Icomp.ToString("0.000"));
                sw.WriteLine("      = {0} * 10E9 sq.sq.mm", Icomp);
                sw.WriteLine();

                sw.WriteLine("Maximum Shear force at junction of Slab and Girder is obtained by");

                tau = (v * 1000 * Ace * yc) / (Icomp * 10E9);
                sw.WriteLine("τ = v * 1000 * Ace *  yc / Icomp");
                sw.WriteLine("  = {0} * 1000 * {1} * {2} / {3} * 10E9", v, Ace, yc, Icomp);
                tau = double.Parse(tau.ToString("0"));
                sw.WriteLine("  = {0} N/mm", tau);
                sw.WriteLine();

                double Q1 = tau * Bf;
                Q1 = double.Parse(Q1.ToString("0"));
                sw.WriteLine("Total Shear force at junction Q1 =  τ * Bf ");
                sw.WriteLine("                                 =  {0} * {1}", tau, Bf);
                sw.WriteLine("                                 =  {0} N", Q1);
                sw.WriteLine();

                double _do = 20.0;
                sw.WriteLine("Using do = {0} mm diameter mild steel studs,     Marked as (10) in the Drawing", _do);
                _10 = string.Format("{0} Ø Studs", _do);

                sw.WriteLine("capacity of one shear connector is given by,");
                sw.WriteLine();
                // 196 = ?
                double Q2 = 196 * _do * _do * Math.Sqrt(fck);
                Q2 = double.Parse(Q2.ToString("0"));
                sw.WriteLine("    Q2 = 196 * do*do *  √fck");
                sw.WriteLine("       = 196 * {0}*{0} *  √{1}", _do, fck);
                sw.WriteLine("       = {0} N", Q2);
                sw.WriteLine();

                // 5 = ?
                double H = 5 * 20;
                sw.WriteLine("Height of each stud = H");
                sw.WriteLine("                    = 5 * do");
                sw.WriteLine("                    = 5 * {0}", _do);
                sw.WriteLine("                    = {0} mm", H);
                sw.WriteLine();

                double no_std_row = (Q1 / Q2);
                no_std_row = double.Parse(no_std_row.ToString("0.00"));
                sw.WriteLine("Number of studs required in a row");
                sw.WriteLine();
                if (no_std_row < 1.0)
                {
                    sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} < 1", Q1, Q2, no_std_row);
                }
                else
                {
                    sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} > 1", Q1, Q2, no_std_row);
                }
                sw.WriteLine("So, Provide a minimum of 2 mild Steel Studs in a row");
                sw.WriteLine();

                double N = 2;
                double fs = 2.0;
                double p = N * Q2 / (fs * tau);
                p = double.Parse(p.ToString("0"));
                sw.WriteLine("Pitch of Shear Connectors = p = N * Q2 / (fs * τ)");

                sw.WriteLine("N = Number of Shear Connectors in a row = 2");
                sw.WriteLine();
                sw.WriteLine("Fs = Factor of Safety = 2.0");
                sw.WriteLine();
                sw.WriteLine("p = 2 * {0} / (2 * {1})", Q2, tau);
                sw.WriteLine("  = {0} mm", p);
                sw.WriteLine();

                sw.WriteLine("Maximum permissible pitch is the lowest value of:");
                sw.WriteLine();
                sw.WriteLine("(i)     3 * Thickness of Slab = 3 * {0} = {1:f0} mm", D, (3 * D));
                sw.WriteLine("(ii)    4 * Height of Stud = 4 * (5 * do) = 4 * {0:f0} = {1:f0} mm", (5 * _do), (4 * 5 * _do));
                sw.WriteLine("(iii)   600 mm");
                sw.WriteLine();
                sw.WriteLine("Hence provide the pitch of 400 mm in the longitudinal direction.    Marked as (11) in the Drawing");

                #endregion


                sw.WriteLine();
                sw.WriteLine();
                Write_Table_1(sw);
                Write_Table_2(sw);

                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
                //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
                //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
                //sw.WriteLine("-----------------------------------------------------------------------");

                //sw.WriteLine("110    87       87       87      87       87      87      87");
                //sw.WriteLine("130    87       87       87      87       87      84      82");
                //sw.WriteLine("150    87       87       87      85       80      77      75");
                //sw.WriteLine("170    87       87       83      80       76      72      70");
                //sw.WriteLine("190    87       87       79      75");
                //sw.WriteLine("200    87       85       77");
                //sw.WriteLine("220    87       80       73");
                //sw.WriteLine("240    87       77");
                //sw.WriteLine("-----------------------------------------------------------------------");


                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
                //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
                //sw.WriteLine("λ= (L/r)  ______________________________________");
                //sw.WriteLine("           236        299        331       362");
                //sw.WriteLine("---------------------------------------------------");
                //sw.WriteLine("0         140.0      171.2       191.5    210.0");
                //sw.WriteLine("20        136.0      167.0       186.0    204.0");
                //sw.WriteLine("40        130.0      157.0       174.0    190.0");
                //sw.WriteLine("60        118.0      139.0       151.6    162.0");
                //sw.WriteLine("80        101.0      113.5       120.3    125.5");
                //sw.WriteLine("100        80.5       87.0        90.2     92.7");
                //sw.WriteLine("120        63.0       66.2        68.0     69.0");
                //sw.WriteLine("140        49.4       51.2        52.0     52.6");
                //sw.WriteLine("160        39.0       40.1        40.7     41.1");
                //sw.WriteLine("---------------------------------------------------");


                sw.WriteLine();
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

        #region IReport Members

        /**/
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("S = {0}", S);
                sw.WriteLine("B1 = {0}", B1);
                sw.WriteLine("B2 = {0}", B2);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine("fy = {0}", fy);
                sw.WriteLine("m = {0}", m);
                sw.WriteLine("YS = {0}", YS);
                sw.WriteLine("D = {0}", D);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("Dwc = {0}", Dwc);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("gamma_wc = {0}", gamma_wc);
                sw.WriteLine("WL = {0}", WL);
                sw.WriteLine("v = {0}", v);
                sw.WriteLine("u = {0}", u);
                sw.WriteLine("IF = {0}", IF);
                sw.WriteLine("CF = {0}", CF);
                sw.WriteLine("Q = {0}", Q);
                sw.WriteLine("j = {0}", j);
                sw.WriteLine("sigma_st = {0}", sigma_st);
                sw.WriteLine("sigma_b = {0}", sigma_b);
                sw.WriteLine("tau = {0}", tau);
                sw.WriteLine("sigma_tf = {0}", sigma_tf);
                sw.WriteLine("K = {0}", K);
                sw.WriteLine("sigma_p = {0}", sigma_p);

                //Chiranjit [2011 07 21]
                sw.WriteLine("dw = {0}", dw);
                sw.WriteLine("tw = {0}", tw);
                sw.WriteLine("nw = {0}", nw);

                sw.WriteLine("bf = {0}", bf);
                sw.WriteLine("tf = {0}", tf);
                sw.WriteLine("nf = {0}", nf);

                sw.WriteLine("ang = {0}", cmb_ang.Text);
                sw.WriteLine("ang_thk = {0}", ang_thk);

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void InitializeData()
        {
            #region USER DATA INPUT
            try
            {
                S = MyList.StringToDouble(txt_S.Text, 0.0);
                B1 = MyList.StringToDouble(txt_B1.Text, 0.0);
                B2 = MyList.StringToDouble(txt_B2.Text, 0.0);
                B = MyList.StringToDouble(txt_B.Text, 0.0);
                fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                fy = MyList.StringToDouble(txt_fy.Text, 0.0);
                m = MyList.StringToDouble(txt_m.Text, 0.0);
                YS = MyList.StringToDouble(txt_YS.Text, 0.0);
                D = MyList.StringToDouble(txt_D.Text, 0.0);
                L = MyList.StringToDouble(txt_L.Text, 0.0);
                Dwc = MyList.StringToDouble(txt_Dwc.Text, 0.0);
              
                gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);

                gamma_wc = MyList.StringToDouble(txt_gamma_wc.Text, 0.0);
                WL = MyList.StringToDouble(txt_WL.Text, 0.0);
                v = MyList.StringToDouble(txt_v.Text, 0.0);
                u = MyList.StringToDouble(txt_u.Text, 0.0);
                IF = MyList.StringToDouble(txt_IF.Text, 0.0);
                CF = MyList.StringToDouble(txt_CF.Text, 0.0);
                j = MyList.StringToDouble(txt_j.Text, 0.0);
                Q = MyList.StringToDouble(txt_Q.Text, 0.0);

                sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
                sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                tau = MyList.StringToDouble(txt_tau.Text, 0.0);
                sigma_tf = MyList.StringToDouble(txt_sigma_tf.Text, 0.0);

                K = MyList.StringToDouble(txt_K.Text, 0.0);
                sigma_p = MyList.StringToDouble(txt_sigma_p.Text, 0.0);


                //Chiranjit [2011 07 21]
                dw = MyList.StringToDouble(txt_dw.Text, 0.0);
                tw = MyList.StringToDouble(txt_tw.Text, 0.0);
                bf = MyList.StringToDouble(txt_bf.Text, 0.0);
                tf = MyList.StringToDouble(txt_tf.Text, 0.0);
                ang = cmb_ang.Text;
                ang_thk = MyList.StringToDouble(cmb_ang_thk.Text, 0.0);
                off = MyList.StringToDouble(txt_off.Text, 0.0);

                des_moment = MyList.StringToDouble(txt_des_mom.Text, 0.0);
                des_shear = MyList.StringToDouble(txt_des_shr.Text, 0.0);


                nw = MyList.StringToInt(txt_nw.Text, 0);
                nf = MyList.StringToInt(txt_nf.Text, 0);
                na = MyList.StringToInt(txt_na.Text, 0);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
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
                        case "S":
                            S = mList.GetDouble(1);
                            txt_S.Text = S.ToString();
                            break;
                        case "B1":
                            B1 = mList.GetDouble(1);
                            txt_B1.Text = B1.ToString();
                            break;
                        case "B2":
                            B2 = mList.GetDouble(1);
                            txt_B2.Text = B2.ToString();
                            break;
                        case "B":
                            B = mList.GetDouble(1);
                            txt_B.Text = B.ToString();
                            break;
                        case "fck":
                            fck = mList.GetDouble(1);
                            txt_fck.Text = fck.ToString();
                            break;
                        case "fy":
                            fy = mList.GetDouble(1);
                            txt_fy.Text = fy.ToString();
                            break;
                        case "m":
                            m = mList.GetDouble(1);
                            txt_m.Text = m.ToString();
                            break;
                        case "YS":
                            YS = mList.GetDouble(1);
                            txt_YS.Text = YS.ToString();
                            break;
                        case "D":
                            D = mList.GetDouble(1);
                            txt_D.Text = D.ToString();
                            break;
                        case "L":
                            L = mList.GetDouble(1);
                            txt_L.Text = L.ToString();
                            break;
                        case "Dwc":
                            Dwc = mList.GetDouble(1);
                            txt_Dwc.Text = Dwc.ToString();
                            break;
                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "gamma_wc":
                            gamma_wc = mList.GetDouble(1);
                            txt_gamma_wc.Text = gamma_wc.ToString();
                            break;
                        case "WL":
                            WL = mList.GetDouble(1);
                            txt_WL.Text = WL.ToString();
                            break;
                        case "v":
                            v = mList.GetDouble(1);
                            txt_v.Text = v.ToString();
                            break;
                        case "u":
                            u = mList.GetDouble(1);
                            txt_u.Text = u.ToString();
                            break;
                        case "IF":
                            IF = mList.GetDouble(1);
                            txt_IF.Text = IF.ToString();
                            break;
                        case "CF":
                            CF = mList.GetDouble(1);
                            txt_CF.Text = CF.ToString();
                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            txt_Q.Text = Q.ToString();
                            break;
                        case "j":
                            j = mList.GetDouble(1);
                            txt_j.Text = j.ToString();
                            break;
                        case "sigma_st":
                            sigma_st = mList.GetDouble(1);
                            txt_sigma_st.Text = sigma_st.ToString();
                            break;
                        case "sigma_b":
                            sigma_b = mList.GetDouble(1);
                            txt_sigma_b.Text = sigma_b.ToString();
                            break;
                        case "tau":
                            tau = mList.GetDouble(1);
                            txt_tau.Text = tau.ToString();
                            break;
                        case "sigma_tf":
                            sigma_tf = mList.GetDouble(1);
                            txt_sigma_tf.Text = sigma_tf.ToString();
                            break;
                        case "K":
                            K = mList.GetDouble(1);
                            txt_K.Text = K.ToString();
                            break;
                        case "sigma_p":
                            sigma_p = mList.GetDouble(1);
                            txt_sigma_p.Text = sigma_p.ToString();
                            break;
                        case "dw":
                            txt_dw.Text = mList.StringList[1];
                            break;
                        case "tw":
                            txt_tw.Text = mList.StringList[1];
                            break;
                        case "nw":
                            txt_nw.Text = mList.StringList[1];
                            break;
                        case "bf":
                            txt_bf.Text = mList.StringList[1];
                            break;
                        case "tf":
                            txt_tf.Text = mList.StringList[1];
                            break;
                        case "nf":
                            txt_nf.Text = mList.StringList[1];
                            break;
                        case "ang":
                            cmb_ang.SelectedItem = mList.StringList[1];
                            break;
                        case "ang_thk":
                            cmb_ang_thk.Text = mList.StringList[1];
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
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Bridge Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Composite Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Deck Slab + Steel Girder");

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
                this.Text = "DESIGN OF COMPOSITE BRIDGE : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "COMPOSITE_BRIDGE");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_Comp_Slab_Girder.TXT");
                user_input_file = Path.Combine(system_path, "COMPOSITE_BRIDGE.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                btnDrawing.Enabled = File.Exists(rep_file_name);
                btnProcess.Enabled = Directory.Exists(value);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }

            }
        }

        /**/
        #endregion
        public string Title
        {
            get
            {
                return "COMPOSITE_BRIDGE";
            }
        }

        public double Get_Table_1_Value(double d_by_t, double d_point)
        {
            return iApp.Tables.Allowable_Average_Shear_Stress(d_by_t, d_point, ref ref_string);




            //int indx = -1;

            //if (d_point >= 0.4 && d_point < 0.6)
            //    indx = 1;
            //else if (d_point >= 0.6 && d_point < 0.8)
            //    indx = 2;
            //else if (d_point >= 0.8 && d_point < 1.0)
            //    indx = 3;
            //else if (d_point >= 1.0 && d_point < 1.2)
            //    indx = 4;
            //else if (d_point >= 1.2 && d_point < 1.4)
            //    indx = 5;
            //else if (d_point >= 1.4 && d_point < 1.5)
            //    indx = 6;
            //else if (d_point >= 1.5)
            //    indx = 7;


            //d_by_t = Double.Parse(d_by_t.ToString("0.0"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1,returned_value;
            ////double  b1, a2, b2, returned_value;

            ////a1 = 0.0;
            ////b1 = 0.0;
            ////a2 = 0.0;
            ////b2 = 0.0;
            //returned_value = 0.0;

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

            //    if (d_by_t < a1)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_2_Value(double lamda, int indx)
        {
            return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, indx, ref ref_string);
            //lamda = Double.Parse(lamda.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, b1, a2, b2, returned_value;

            //a1 = 0.0;
            //b1 = 0.0;
            //a2 = 0.0;
            //b2 = 0.0;
            //returned_value = 0.0;

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
            //    if (lamda < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == lamda)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > lamda)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }

        public void Write_Table_1(StreamWriter sw)
        {
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
            //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
            //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
            //sw.WriteLine("-----------------------------------------------------------------------");


            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Allowable_Average_Shear_Stress();
            string kStr = "";

            bool find = false;


            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }

            lst_content.Clear();
            sw.WriteLine("-----------------------------------------------------------------------");

        }
        public void Write_Table_2(StreamWriter sw)
        {
            sw.WriteLine();
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
            //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
            //sw.WriteLine("λ= (L/r)  ______________________________________");
            //sw.WriteLine("           236        299        331       362");
            //sw.WriteLine("---------------------------------------------------");



            List<string> lst_content = iApp.Tables.Get_Tables_Allowable_Working_Stress_Cross_Section();

            bool find = false;


            sw.WriteLine();
            sw.WriteLine("TABLE 2 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }

            sw.WriteLine("---------------------------------------------------");
            lst_content.Clear();
        }
        public void Write_Drawing_File()
        {
            //drawing_path = Path.Combine(drawing_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            drawing_path = Path.Combine(system_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                //_A = spacing_main_girder - (width_long_girders / 1000.0);
                //_B = (width_long_girders / 1000.0);
                //_C = spacing_cross_girder;
                //_D = (width_cross_girders / 1000.0);
                //_E = Dwc / 1000.0;
                //_F = Ds / 1000.0;
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_G={0}", _G);


                //(v) = (E) + 2 x (G) = 3.6 + 2 x 0.08 = 3.76 m. = 3760 mm.

                double val1, val2, val3;

                val1 = 0.0;
                val2 = 0.0;
                val3 = 0.0;

                if (double.TryParse(_E.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val1))
                {
                    if (double.TryParse(_G.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val2))
                    {
                        val3 = val1 + 2 * val2;
                        _v = string.Format("(E) + 2 x (G) = {0:f3} + 2 x {1:f3} = {2:f3} m. = {3:f2} mm.", val1, val2, val3, (val3 * 1000.0));
                    }

                }

                //(u) = (F) + 2 x (G) = 0.850 + 2 x 0.08 = 1.0 m. = 1000 mm.
                if (double.TryParse(_F.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val1))
                {
                    if (double.TryParse(_G.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val2))
                    {
                        val3 = val1 + 2 * val2;
                        _u = string.Format("(F) + 2 x (G) = {0:f3} + 2 x {1:f3} = {2:f3} m. = {3:f2} mm.", val1, val2, val3, (val3 * 1000.0));
                    }

                }

                sw.WriteLine("_v={0}", _v);
                sw.WriteLine("_u={0}", _u);
                sw.WriteLine("_1={0}", _1);
                sw.WriteLine("_2={0}", _2);
                sw.WriteLine("_3={0}", _3);
                sw.WriteLine("_4={0}", _4);
                sw.WriteLine("_6={0}", _6);
                sw.WriteLine("_7={0}", _7);
                sw.WriteLine("_8={0}", _8);
                sw.WriteLine("_10={0}", _10);
               
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        #endregion

        #region Form Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program();
            Write_Drawing_File();
            if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            is_process = true;
            FilePath = user_path;
        }
        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                    Read_Max_Moment_Shear_from_Analysis();
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "Composite_Bridge", "");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        bool flg = false;
        private void tmrComp_Tick(object sender, EventArgs e)
        {
            flg = !flg;

            if (flg)
            {
                pb1.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.CompoBridge;
            }
            else
            {
                pb1.BackgroundImage = (Image)AstraFunctionOne.Properties.Resources.DCP_3935;
            }

        }

        void Read_Max_Moment_Shear_from_Analysis()
        {

            //string f_path = Environment.GetEnvironmentVariable("TBEAM_ANALYSIS");
            string f_path = Path.Combine(user_path, "FORCES.TXT");
            if (File.Exists(f_path))
            {
                List<string> list = new List<string>(File.ReadAllLines(f_path));

                MyList mlist = null;
                string kStr = "";
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i].ToUpper();

                    mlist = new MyList(kStr, '=');
                    if (mlist.StringList.Count > 1)
                    {
                        switch (mlist.StringList[0])
                        {
                            case "LONG_LENGTH":
                                //txt_L.Text = mlist.GetDouble(1).ToString();
                                break;
                            case "LONG_INN_DEFF_SHR":
                                inner_deff_shear = mlist.GetDouble(1) * 10;
                                break;
                            case "LONG_INN_L2_MOM":
                                inner_L2_moment = mlist.GetDouble(1) * 10;
                                break;
                            case "LONG_OUT_DEFF_SHR":
                                outer_deff_shear = mlist.GetDouble(1) * 10;
                                break;
                            case "LONG_OUT_L2_MOM":
                                outer_L2_moment = mlist.GetDouble(1) * 10;
                                break;
                        }
                    }
                }
                Load_Analysis_Data();
            }
        }
        //Chiranjit [2011 06 16]
        // Set All the values from TBeam Analysis Data
        public void Load_Analysis_Data()
        {
            //Read_Max_Moment_Shear_from_Analysis();
            if (rbtn_inner_girder.Checked)
            {

                if (inner_L2_moment != 0.0)
                {
                    txt_des_mom.Text = inner_L2_moment.ToString();
                    txt_des_mom.ForeColor = Color.Red;
                }

                if (inner_deff_shear != 0.0)
                {

                    txt_des_shr.Text = inner_deff_shear.ToString();
                    txt_des_shr.ForeColor = Color.Red;
                }
            }
            else
            {

                if (outer_L2_moment != 0.0)
                {
                    txt_des_mom.Text = outer_L2_moment.ToString();
                    txt_des_mom.ForeColor = Color.Red;
                }

                if (outer_deff_shear != 0.0)
                {
                    txt_des_shr.Text = outer_deff_shear.ToString();
                    txt_des_shr.ForeColor = Color.Red;
                }
            }
        }

        private void txt_fck_TextChanged(object sender, EventArgs e)
        {
            InitializeData();
            double fcc, j, Q, fcb, n;

            //fck = concrete_grade;

            fcb = fck / 3;
            fcc = fck / 4;

            n = m * fcb / (m * fcb + sigma_st);

            j = 1 - (n / 3.0);
            Q = n * j * fcb / 2;

            txt_sigma_b.Text = fcb.ToString("0.00");
            txt_j.Text = j.ToString("0.000");
            txt_Q.Text = Q.ToString("0.000");
        }

        private void frmCompositeBridge_Load(object sender, EventArgs e)
        {
            tbl_rolledSteelAngles = iApp.Tables.Get_SteelAngles();

            string kStr = "";
            if (tbl_rolledSteelAngles.List_Table.Count > 0)
            {
                for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                {
                    kStr = tbl_rolledSteelAngles.List_Table[i].SectionCode;
                    if (cmb_ang.Items.Contains(kStr) == false)
                        cmb_ang.Items.Add(kStr);
                }
            }

            cmb_ang.SelectedItem = "100100";
            cmb_ang_thk.SelectedItem = 10.0;
            txt_na.SelectedItem = "4";
        }
        private void cmb_ang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbl_rolledSteelAngles.List_Table.Count > 0)
            {
                for (int i = 0; i < tbl_rolledSteelAngles.List_Table.Count; i++)
                {
                    if (tbl_rolledSteelAngles.List_Table[i].SectionCode == cmb_ang.Text)
                    {
                        if (cmb_ang_thk.Items.Contains(tbl_rolledSteelAngles.List_Table[i].Thickness) == false)
                            cmb_ang_thk.Items.Add(tbl_rolledSteelAngles.List_Table[i].Thickness);
                    }
                }
            }
        }

        private void rbtn_inner_girder_CheckedChanged(object sender, EventArgs e)
        {
            Load_Analysis_Data();
        }
        
    }
}
