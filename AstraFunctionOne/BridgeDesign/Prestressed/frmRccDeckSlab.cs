using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using System.IO;
namespace AstraFunctionOne.BridgeDesign
{
    public partial class frmRccDeckSlab : Form, IReport
    {
        double S, CW, Wk, tk, Fw, twc, Wt, Ltl, Wtl, L, bl, B, bc;
        double _Do, fck, fci, sigma_cb, sigma_st, m, Q, j, fy, gamma_c, gamma_wc, _IF, CF;
        double tds,dm,dd;

        IApplication iApp = null;
        string rep_file_name = "";
        string file_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_path = "";
        string drawing_path = "";
        bool is_process = false;

        string ref_string = "";

        // Drawing Variable
        string _A, _B, _C, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;


        public frmRccDeckSlab(IApplication app)
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

        #region IReport Members

        public void Calculate_Program()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t************************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                   *");
                sw.WriteLine("\t\t*         TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                              *");
                sw.WriteLine("\t\t*          DESIGN OF RCC DECK SLAB             *");
                sw.WriteLine("\t\t*                                              *");
                sw.WriteLine("\t\t************************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                #endregion

                #region USER'S DATA
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Effective Span [S] = {0} m", txt_S.Text);
                sw.WriteLine("Width of Carrage way [CW] = {0} m", txt_CW.Text);
                sw.WriteLine("Width of Kerb [Wk] = {0} m", txt_Wk.Text);
                sw.WriteLine("Thickness of Kerb [tk] = {0} m", txt_tk.Text);
                sw.WriteLine("Width of Footpath [Fw] = {0} m", txt_Fw.Text);
                sw.WriteLine("Thickness of Wearing Course [twc] = {0} m    Marked as (E) in Drawing", txt_twc.Text);
                //
                _E = string.Format("{0:f3}", twc);



                sw.WriteLine("Track Load [Wt] = {0} kN", txt_Wt.Text);
                sw.WriteLine("Track Loading Length [Ltl] = {0} m", txt_Ltl.Text);
                sw.WriteLine("Track Loading Width [Wtl] = {0} m", txt_Wtl.Text);
                sw.WriteLine("Spacing of Main Long Girders [L] = {0} m     Marked as (A) in Drawing", txt_L.Text);
                //
                _A = string.Format("{0}", txt_L.Text);

                sw.WriteLine("Width of Long Girder [bl] = {0} m            Marked as (B) in Drawing", txt_bl.Text);
                //
                _B = string.Format("{0}", txt_bl.Text);

                sw.WriteLine("Spacing of Cross Girders [B] = {0} m         Marked as (C) in Drawing", txt_B.Text);
                //
                _C = string.Format("{0}", txt_B.Text);


                sw.WriteLine("Width of Cross Girders [bc] = {0} m          Marked as (D) in Drawing", txt_bc.Text);
                //
                _D = string.Format("{0}", txt_bc.Text);


                sw.WriteLine("Thickness of Deck Slab [Do] = {0} m          Marked as (F) in Drawing", txt_Do.Text);
                //
                _F = string.Format("{0}", txt_Do.Text);

                sw.WriteLine("Concrete Grade [fck] = M {0} = {0} N/sq.mm", txt_fck.Text);
                sw.WriteLine("Concrete Cube strength at transfer [fci] = {0} N/sq.mm", txt_fci.Text);
                sw.WriteLine("Permissible compressive stress in concrete [σ_cb] = {0} N/sq.mm", txt_sigma_cb.Text);
                sw.WriteLine("Permissible tensile stress in steel [σ_st] = {0} N/sq.mm", txt_sigma_st.Text);
                sw.WriteLine("Modular Ratio [m] = {0}", txt_m.Text);
                sw.WriteLine("Moment Factor [Q] = {0}", txt_Q.Text);
                sw.WriteLine("Lever Arm Factor [j] = {0}", txt_j.Text);
                sw.WriteLine("Steel Grade [fy] = Fe {0} = {0} N/sq.mm", txt_fy.Text);
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", txt_gamma_c.Text);
                sw.WriteLine("Unit Weight of Wearing Course [γ_wc] = {0} kN/cu.m", txt_gamma_wc.Text);
                sw.WriteLine("Impact Factor [IF] = {0}", txt_IF.Text);
                sw.WriteLine("Continuity Factor [CF] = {0}", txt_CF.Text);
                sw.WriteLine("Diameter of Main Reinforcement Bars [dm] = {0}", txt_dm.Text);
                sw.WriteLine("Diameter of Distribution Reinforcement Bars [dd] = {0}", txt_dd.Text);
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                #region STEP 1 : CROSS SECTION OF DECK SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : CROSS SECTION OF DECK SLAB ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For each panel enclosed by 2 long and 2 Cross girders");
                sw.WriteLine();
                sw.WriteLine("Length = L = {0} m", L);
                sw.WriteLine();
                sw.WriteLine("Width = B = {0} m", B);
                sw.WriteLine();
                tds = _Do;
                sw.WriteLine("Thickness of Deck Slab = tds = {0} m", tds);
                sw.WriteLine();
                sw.WriteLine("Thickness of Wearing Course = twc = {0} m", twc);
                sw.WriteLine();
                #endregion

                #region STEP 2 : LIVE LOAD BENDING MOMENT
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : LIVE LOAD BENDING MOMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Live Load  = Wt = {0} kN", Wt);
                sw.WriteLine();
                sw.WriteLine("Live Load Length = Ltl = {0} m", Ltl);
                sw.WriteLine();
                sw.WriteLine("Live Load Width = Wtl = {0} m", Wtl);
                sw.WriteLine();
                sw.WriteLine("Considering 45° dispersion through wearing Course");
                sw.WriteLine();

                double u = Wtl + 2 * twc;
                sw.WriteLine("    u = {0} + 2 * {1} = {2} m", Wtl, twc, u);
                sw.WriteLine();
                double v = Ltl + 2 * twc;
                v = double.Parse(v.ToString("0.00"));
                sw.WriteLine("    v = {0} + 2 * {1} = {2} m", Ltl, twc, v);
                sw.WriteLine();
                sw.WriteLine();

                double u_by_B = u / B;
                sw.WriteLine("   u/B = {0} / {1} = {2} ", u, B, u_by_B);
                sw.WriteLine();
                double v_by_L = v/L;
                sw.WriteLine("   v/L = {0} / {1} = {2} ", v, L, v_by_L);
                sw.WriteLine();

                double K = B / L;
                K = double.Parse(K.ToString("0.0"));
                sw.WriteLine("  K = B/L = {0} / {1} = {2}", B, L, K);
                
                sw.WriteLine();

                double m1, m2;

                m1 = 0.098;
                m2 = 0.02;

                frmCurve fcurv = new frmCurve(K, u_by_B, v_by_L, LoadType.PartialLoad);
                //if (K == 0.5)
                //{
                    fcurv.txt_m1.Text = m1.ToString();
                    fcurv.txt_m2.Text = m2.ToString();
                //}
                fcurv.ShowDialog();

                m1 = fcurv.m1;
                m2 = fcurv.m2;

                sw.WriteLine(" Using Pegeaud's Curves     m1 = {0} and  m2 = {1}", m1, m2);
                sw.WriteLine();

                double MB = Wt * (m1 + 0.15 * m2);
                sw.WriteLine("Bending Moment along Short span = MB");
                sw.WriteLine("                                = Wt*(m1 + 0.15 * m2)");
                sw.WriteLine("                                = {0}*({1} + 0.15 * {2})", Wt, m1, m2);
                sw.WriteLine("                                = {0} kN-m", MB);
                sw.WriteLine();
                sw.WriteLine("As slab is continuous, Design B.M. = CF * MB = {0} * {1}", CF, MB);
                sw.WriteLine();

                double MB1 = CF * _IF * MB;
                MB1 = double.Parse(MB1.ToString("0.00"));
                sw.WriteLine("Including Impact Factor , MB1 = CF * IF * MB");
                sw.WriteLine("                              = {0} * {1} * {2}", CF, _IF, MB);
                sw.WriteLine("                              = {0} kN-m", MB1);
                sw.WriteLine();

                double ML = Wt * (m2 + 0.15 * m1);
                sw.WriteLine("Bending Moment along Long Span = ML ");
                sw.WriteLine("                               = Wt * (m2 + 0.15 * m1)");
                sw.WriteLine("                               = {0} * ({1} + 0.15 * {2})", Wt, m2, m1);
                sw.WriteLine("                               = {0} kN-m", ML);
                sw.WriteLine();

                double ML1 = CF * _IF * ML;
                sw.WriteLine("Design B.M. = ML1");
                sw.WriteLine("            = CF * IF * ML");
                sw.WriteLine("            = {0} * {1} * {2}", CF, _IF, ML);
                sw.WriteLine("            = {0} kN-m", ML1);
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 3 : LIVE LOAD SHEAR FORCES
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : LIVE LOAD SHEAR FORCES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Considering dispersion along short span");

                double cons_disp = Wtl + 2 * (twc + _Do);
                sw.WriteLine("    = Wtl + 2 * (twc + Do)");
                sw.WriteLine("    = {0} + 2 * ({1} + {2})", Wtl, twc, _Do);
                sw.WriteLine("    = {0} m", cons_disp);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For maximum Shear force Load is Placed at a Location ");
                sw.WriteLine("So that the whole dispersion is in the span.");
                sw.WriteLine();

                double x = cons_disp/2.0;
                sw.WriteLine("So, placing the Load at {0}/2 = {1} m from the edge at the beam.", cons_disp, x);
                sw.WriteLine("So   x = {0}", x);
                sw.WriteLine();
                sw.WriteLine();

                
                sw.WriteLine("Effective width of slab = K * x * [1 - (x/L)] + bw");
                sw.WriteLine();
                sw.WriteLine("Width of Long Girders = {0} m", bl);
                sw.WriteLine("Width of Cross Girder = {0} m", bc);
                sw.WriteLine();
                double B1 = L - bl;
                sw.WriteLine("Clear Length of Panel = B1 = {0} - {1} = {2} m", B, bc, B1);
                sw.WriteLine();


                double L1 = B - bc;
                sw.WriteLine("Clear width of Panel = L1 = {0} - {1} = {2} m", L, bl, L1);
                sw.WriteLine();
                
                double B_by_L = B1/L1;
                B_by_L = double.Parse(B_by_L.ToString("0.00"));

                sw.WriteLine(" B1 / L1 = {0} / {1} = {2}, From Table 1, given at the end of the Report,", B1, L1, B_by_L);
                K = Get_Table_1_Value(B_by_L, 2);
                //K = 2.6;
                sw.WriteLine(" K = {0}", K);
                sw.WriteLine();

                double eff_wdt_slab = K * x * (1 - (x / L1)) + Ltl + 2*twc;
                eff_wdt_slab = double.Parse(eff_wdt_slab.ToString("0.000"));
                sw.WriteLine("Effective width of slab = K * x * [1 - (x/L1)] + Ltl");
                sw.WriteLine("       = {0} * {1} * [1 - ({1}/{2})] + {3}", K, x, L1, Ltl);
                sw.WriteLine("       = {0} m", eff_wdt_slab);
                sw.WriteLine();
                double load_per_mtr = Wt / eff_wdt_slab;
                load_per_mtr = double.Parse(load_per_mtr.ToString("0"));
                sw.WriteLine("Load per metre width = {0}/{1} = {2} kN", Wt, eff_wdt_slab, load_per_mtr);
                sw.WriteLine();

                double V = load_per_mtr * (L1 - x) / L1;
                V = double.Parse(V.ToString("0"));
                sw.WriteLine(" V = Shear force per metre width");
                sw.WriteLine("   = {0} * (L1-x)/L1", load_per_mtr);
                sw.WriteLine("   = {0} * ({1}-{2})/{1}", load_per_mtr, L1, x);
                sw.WriteLine("   = {0} kN", V);
                sw.WriteLine();

                double sh_frc_imp = _IF * V;
                sh_frc_imp = double.Parse(sh_frc_imp.ToString("0.00"));
                sw.WriteLine(" Shear force with impact = {0}* {1} = {2} kN", _IF, V, sh_frc_imp);
                sw.WriteLine();
               
                #endregion

                #region STEP 4 : Permanent Load BENDING MOMENTS AND SHEAR FORCES
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Permanent Load BENDING MOMENTS AND SHEAR FORCES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double self_weight_dk_slb = _Do * gamma_c;
                self_weight_dk_slb = double.Parse(self_weight_dk_slb.ToString("0.00"));
                sw.WriteLine("Self weigth of Deck Slab = {0} * {1} = {2} kN/sq.m", _Do, gamma_c, self_weight_dk_slb);
                sw.WriteLine();

                double self_wt_wrng_crs = twc * gamma_wc;
                self_wt_wrng_crs = double.Parse(self_wt_wrng_crs.ToString("0.00"));
                sw.WriteLine("Self weight of wearing course = {0} * {1} = {2} kN/sq.m", twc, gamma_wc, self_wt_wrng_crs);
                sw.WriteLine();

                double total_load = self_weight_dk_slb + self_wt_wrng_crs;
                total_load = double.Parse(total_load.ToString("0.00"));
                sw.WriteLine(" Total Load = {0} kN/sq.m", total_load);
                sw.WriteLine();

                double total_load_panel = L * B * total_load;
                total_load_panel = double.Parse(total_load_panel.ToString("0.00"));
                sw.WriteLine("Total Load on a Panel = L*B*{0}", total_load);
                sw.WriteLine("                      = {0} * {1} * {2}", L, B, total_load);
                sw.WriteLine("                      = {0} kN", total_load_panel);
                sw.WriteLine();
                sw.WriteLine("As the Panel is fully Loaded with uniformly distributed load");

                K = B / L;

                sw.WriteLine("So, u/B = 1    and   v/L = 1   and  K = B/L = {0}/ {1} = {2}", B, L, K);

                double one_by_K = (1 / K);
                sw.WriteLine(" and 1/K = (1/{0}) = {1}", K, one_by_K);
                sw.WriteLine();

                fcurv = new frmCurve(K, 1.0, 1.0, LoadType.FullyLoad);

                m1 = 0.047;
                m2 = 0.01;

                if (one_by_K == 2.0)
                {
                    fcurv.txt_m1.Text = m1.ToString();
                    fcurv.txt_m2.Text = m2.ToString();
                }
                fcurv.ShowDialog();

                m1 = fcurv.m1;
                m2 = fcurv.m2;

                sw.WriteLine();
                sw.WriteLine("From Pigeaud's Curve, m1 = {0}  and m2 = {1}", m1, m2);
                sw.WriteLine();


                double MB2 = total_load_panel * (m1 + 0.15 * m2);
                MB2 = double.Parse(MB2.ToString("0.00"));
                sw.WriteLine("Bending Moment along short span = MB2");
                sw.WriteLine("                                = {0}*(m1+0.15*m2)");
                sw.WriteLine("                                = {0}*({1}+0.15*{2})", total_load_panel, m1, m2);
                sw.WriteLine("                                = {0} kN-m", MB2);
                sw.WriteLine();

                double ML2 = total_load_panel * (m2 + 0.15 * m1);
                ML2 = double.Parse(ML2.ToString("0.00"));
                sw.WriteLine("Bending Moment along long span = ML2");
                sw.WriteLine("                                = {0}*(m2+0.15*m1)");
                sw.WriteLine("                                = {0}*({1}+0.15*{2})", total_load_panel, m2, m1);
                sw.WriteLine("                                = {0} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Design B.M. including continuity Factor = CF * {0}", MB2);
                sw.WriteLine("                                        = {0} * {1}", CF, MB2);
                MB2 = CF * MB2;
                MB2 = double.Parse(MB2.ToString("0.00"));
                sw.WriteLine("                                        = {0} kN-m", MB2);
                sw.WriteLine();

                sw.WriteLine("Design B.M. including continuity Factor = CF * {0}", ML2);
                sw.WriteLine("                                        = {0} * {1}", CF, ML2);
                ML2 = CF * ML2;
                ML2 = double.Parse(ML2.ToString("0.00"));
                sw.WriteLine("                                        = {0} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine();

                double fxd_ld_sh_frc = 0.5 * total_load * L1;
                fxd_ld_sh_frc = double.Parse(fxd_ld_sh_frc.ToString("0.00"));
                sw.WriteLine("Permanent Load Shear Force = 0.5*{0}*L1", total_load);
                sw.WriteLine("                       = 0.5*{0}*{1}", total_load, L1);
                sw.WriteLine("                       = {0} kN", fxd_ld_sh_frc);
                sw.WriteLine();
                sw.WriteLine();

                MB = MB1 + MB2;
                sw.WriteLine("Design Moments = MB = MB1 + MB2 = {0} + {1} = {2} kN-m", MB1, MB2, MB);
                ML = ML1 + ML2;
                sw.WriteLine("               = ML = ML1 + ML2 = {0} + {1} = {2} kN-m", ML1, ML2, ML);
                sw.WriteLine();
                #endregion

                #region STEP 5 : STRUCTURAL DESIGN OF DECK SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : STRUCTURAL DESIGN OF DECK SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double d = (MB * 10E5) / (Q * 1000);

                d = Math.Sqrt(d);
                d = double.Parse(d.ToString("0"));
                sw.WriteLine("Required Effective Depth = d = √((M*10E5)/(Q*b))");
                sw.WriteLine("                         = √(({0}*10E5)/({1}*1000))", MB, Q);
                sw.WriteLine("                         = {0} mm", d);
                sw.WriteLine();


                d = (int)(d / 10.0);
                d += 1.0;
                d = d * 10;
                sw.WriteLine();
                sw.WriteLine("Adopt effective depth = d = {0} mm", d);
                sw.WriteLine();
                sw.WriteLine("Required steel for reinforcement along short span ");

                double Ast = (MB * 10E5) / (sigma_st * j * d);

                Ast = double.Parse(Ast.ToString("0"));
                double _ast = Ast;
                sw.WriteLine(" Ast = (M*10E5)/(σ_st*j*d)");
                sw.WriteLine("     = ({0}*10E5)/({1}*{2}*{3})", MB, sigma_st, j, d);
                sw.WriteLine("     = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();


                double spacing, pro_ast;
                spacing = 200;
                do
                {
                    spacing -= 10;
                    pro_ast = (Math.PI * dm * dm / 4.0) * (1000 / spacing);
                    pro_ast = double.Parse(pro_ast.ToString("0"));
                }
                while (pro_ast < _ast);
                _ast = pro_ast;
                sw.WriteLine("Use {0} mm diameter bars at {1} mm c/c spacing     Marked as (1) in Drawing", dm, spacing);

                //Bars 12 mm Dia 
                _bd1 = string.Format("Bars {0} mm Dia", dm);
                //at 120 mm c/c
                _sp1 = string.Format("at {0} mm c/c", spacing);

                
                sw.WriteLine("Provided Ast = {0} sq.mm", pro_ast);
                sw.WriteLine();

                double d2 = d - (dm / 2.0) - (dd / 2.0);
                sw.WriteLine("Effective depth for long span using {0} mm dia bars", dd);
                sw.WriteLine("     d2 = d - (dm / 2.0) - (dd / 2.0)");
                sw.WriteLine("        = {0} - {1:f0} - {2:f0}", d, (dm / 2.0), (dd / 2.0));
                sw.WriteLine("        = {0} mm", d2);
                sw.WriteLine();

                Ast = (ML * 10E5) / (sigma_st * j * d);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Required steel along long span = Ast");
                sw.WriteLine("                               = (ML*10E5)/(σ_st*j*d)");
                sw.WriteLine("                               = ({0}*10E5)/({1}*{2}*{3})", ML, sigma_st, j, d);
                sw.WriteLine("                               = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine("Requirement of minimum reinforcement using HYSD bars is 0.15% of");
                
                Ast = 0.0015 * _Do * 1000 * 1000;
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("cross section area of Slab, Ast = 0.0015*Do*1000*1000");
                sw.WriteLine("                                = 0.0015 * {0} * 1000 * 1000", _Do);
                sw.WriteLine("                                = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();

                //spacing = 150;

                spacing = 140;
                do
                {
                    spacing += 10;
                    pro_ast = (Math.PI * dd * dd / 4.0) * (1000 / spacing);
                    pro_ast = double.Parse(pro_ast.ToString("0"));
                }
                while (pro_ast < Ast);

                Ast = Math.PI * dd * dd / 4.0 * (1000.0 / spacing);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Use {0} mm diameter bars at {1} mm c/c spacing, Ast = {2} sq.mm.    Marked as (2) in Drawing", dd, spacing, Ast);
                //Bars 10 mm Dia 
                _bd2 = string.Format("Bars {0} mm Dia", dd);
                //at 150 mm c/c
                _sp2 = string.Format("at {0} mm c/c", spacing);

                
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 6 : CHECK FOR SHEAR STRESS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : CHECK FOR SHEAR STRESS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double tau_v = (sh_frc_imp * 1000.0) / (1000.0 * d);
                tau_v = double.Parse(tau_v.ToString("0.000"));
                sw.WriteLine("Nominal Shear Stress = τ_v");
                sw.WriteLine("                     = (V * 1000.0) / (1000.0 * d)");
                sw.WriteLine("                     = ({0} * 1000.0) / (1000.0 * {1})", sh_frc_imp, d);
                sw.WriteLine("                     = {0} N/sq.mm", tau_v);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The permissible Shear Stress in slab = τ_c");
                sw.WriteLine("                                     = K1 * K2 * τ_co");
                sw.WriteLine();

                double _d = d / 1000.0;

                double K1 = 1.14 - 0.7 * _d;
                K1 = double.Parse(K1.ToString("0.000"));
                sw.WriteLine("Where K1 = 1.14-0.7*d");
                sw.WriteLine("         = 1.14-0.7*{0}", _d);
                sw.WriteLine("         = {0} ", _d);
                if (K1 < 0.5)
                {
                    K1 = 0.5;
                    sw.WriteLine("         = {0} ", _d);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("  and K2 = 0.5+0.25*p");
                sw.WriteLine();
                double p = (_ast * 100.0) / (1000.0 * d);
                p = double.Parse(p.ToString("0.000"));
                sw.WriteLine("      p = Ast*100/b*d");
                sw.WriteLine("        = {0}*100/(1000*{1})", _ast, d);
                sw.WriteLine("        = {0}", p);
                sw.WriteLine();
                double K2 = 0.5 + 0.25 * p;
                sw.WriteLine(" K2 = 0.5 + 0.25 * {0}", p);
                sw.WriteLine("    = {0}", K2);

                if (K2 < 1.0)
                {
                    K2 = 1.0;
                    sw.WriteLine("    = {0} (K2 <= 1.0) ", K2);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("For M20 Grade Concrete, Refering to Table 2 (Given at the end of the Report)");
                int con_grade = 20;
                double tau_co = 0.34;
                switch (con_grade)
                {
                    case 15:
                        tau_co = 0.28;
                        break;
                    case 20:
                        tau_co = 0.34;
                        break;
                    case 25:
                        tau_co = 0.40;
                        break;
                    case 30:
                        tau_co = 0.45;
                        break;
                    case 35:
                        tau_co = 0.50;
                        break;
                    case 40:
                        tau_co = 0.50;
                        break;
                    default:
                        tau_co = 0.50;
                        break;

                }



                sw.WriteLine("τ_co = 0.34 N/sq.mm");
                sw.WriteLine();

                double tau_c = K1 * K2 * tau_co;
                tau_c = double.Parse(tau_c.ToString("0.000"));
                sw.WriteLine("τ_c = K1 * K2 * τ_co");
                sw.WriteLine("    = {0} * {1} * {2}", K1, K2, tau_co);
                sw.WriteLine("    = {0} N/sq.mm.", tau_c);
                sw.WriteLine();

                if (tau_v < tau_c)
                {
                    sw.WriteLine("Since τ_v < τ_c the shear stresses are within Safe permissible limits.");
                }
                else
                {
                    sw.WriteLine("Since τ_v > τ_c the shear stresses are NOT within Safe permissible limits.");
                }
                sw.WriteLine();
                #endregion

                Write_Table_1(sw);

                #region TABLE 2 : BASIC VALUES OF SHEAR STRESS τ_co (IRC:21-1987)
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 2 : BASIC VALUES OF SHEAR STRESS τ_co (IRC:21-1987)");
                sw.WriteLine("______________________________________________________________");
                sw.WriteLine("Concrete Grade   M 15    M 20    M 25    M 30    M 35    M 40");
                sw.WriteLine("-------------------------------------------------------------");
                sw.WriteLine("τ_co (N/sq.mm)   0.28    0.34    0.40    0.45    0.50    0.50");
                sw.WriteLine("______________________________________________________________");
                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("S = {0}", txt_S.Text);
                sw.WriteLine("CW = {0}", txt_CW.Text);
                sw.WriteLine("Wk = {0}", txt_Wk.Text);
                sw.WriteLine("tk = {0}", txt_tk.Text);
                sw.WriteLine("Fw = {0}", txt_Fw.Text);
                sw.WriteLine("twc = {0}", txt_twc.Text);
                sw.WriteLine("Wt = {0}", txt_Wt.Text);
                sw.WriteLine("Ltl = {0}", txt_Ltl.Text);
                sw.WriteLine("Wtl = {0}", txt_Wtl.Text);
                sw.WriteLine("L = {0}", txt_L.Text);
                sw.WriteLine("bl = {0}", txt_bl.Text);
                sw.WriteLine("B = {0}", txt_B.Text);
                sw.WriteLine("bc = {0}", txt_bc.Text);
                sw.WriteLine("_Do = {0}", txt_Do.Text);
                sw.WriteLine("fck = {0}", txt_fck.Text);
                sw.WriteLine("fci = {0}", txt_fci.Text);
                sw.WriteLine("sigma_cb = {0}", txt_sigma_cb.Text);
                sw.WriteLine("sigma_st = {0}", txt_sigma_st.Text);
                sw.WriteLine("m = {0}", txt_m.Text);
                sw.WriteLine("Q = {0}", txt_Q.Text);
                sw.WriteLine("j = {0}", txt_j.Text);
                sw.WriteLine("fy = {0}", txt_fy.Text);
                sw.WriteLine("gamma_c = {0}", txt_gamma_c.Text);
                sw.WriteLine("gamma_wc = {0}", txt_gamma_wc.Text);
                sw.WriteLine("_IF = {0}", txt_IF.Text);
                sw.WriteLine("CF = {0}", txt_CF.Text);
                sw.WriteLine("dm = {0}", txt_dm.Text);
                sw.WriteLine("dd = {0}", txt_dd.Text);

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
                            txt_S.Text = mList.StringList[1].Trim().TrimEnd().TrimStart().Trim().TrimEnd().TrimStart();
                            break;
                        case "CW":
                            txt_CW.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Wk":
                            txt_Wk.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Fw":
                            txt_Fw.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "twc":
                            txt_twc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Wt":
                            txt_Wt.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Ltl":
                            txt_Ltl.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Wtl":
                            txt_Wtl.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "L":
                            txt_L.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "bl":
                            txt_bl.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "B":
                            txt_B.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "bc":
                            txt_bc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "_Do":
                            txt_Do.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fck":
                            txt_fck.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fci":
                            txt_fci.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_cb":
                            txt_sigma_cb.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_st":
                            txt_sigma_st.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "m":
                            txt_m.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Q":
                            txt_Q.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "j":
                            txt_j.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fy":
                            txt_fy.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "gamma_c":
                            txt_gamma_c.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "gamma_wc":
                            txt_gamma_wc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "_IF":
                            txt_IF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "CF":
                            txt_CF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dm":
                            txt_dm.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dd":
                            txt_dd.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
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
            kPath = Path.Combine(kPath, "Pre stressed Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Rcc Deck Slab");

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
                this.Text = "DESIGN OF RCC DECK SLAB : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_DECK_SLAB");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_PS_Slab.TXT");
                user_input_file = Path.Combine(system_path, "RCC_DECK_SLAB.FIL");

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

        public void InitializeData()
        {
            #region USER DATA INPUT
            try
            {
                S = MyList.StringToDouble(txt_S.Text,0.0);
                CW = MyList.StringToDouble(txt_CW.Text,0.0);
                Wk = MyList.StringToDouble(txt_Wk.Text,0.0);
                tk = MyList.StringToDouble(txt_tk.Text,0.0);
                Fw = MyList.StringToDouble(txt_Fw.Text,0.0);
                twc = MyList.StringToDouble(txt_twc.Text,0.0);
                Wt = MyList.StringToDouble(txt_Wt.Text,0.0);
                Ltl = MyList.StringToDouble(txt_Ltl.Text,0.0);
                Wtl = MyList.StringToDouble(txt_Wtl.Text,0.0);
                L = MyList.StringToDouble(txt_L.Text,0.0);
                bl = MyList.StringToDouble(txt_bl.Text,0.0);
                B = MyList.StringToDouble(txt_B.Text,0.0);
                bc = MyList.StringToDouble(txt_bc.Text,0.0);
                _Do = MyList.StringToDouble(txt_Do.Text,0.0);
                fck = MyList.StringToDouble(txt_fck.Text,0.0);
                fci = MyList.StringToDouble(txt_fci.Text,0.0);
                sigma_cb = MyList.StringToDouble(txt_sigma_cb.Text,0.0);
                sigma_st = MyList.StringToDouble(txt_sigma_st.Text,0.0);
                m = MyList.StringToDouble(txt_m.Text,0.0);
                Q = MyList.StringToDouble(txt_Q.Text,0.0);
                j = MyList.StringToDouble(txt_j.Text,0.0);
                fy = MyList.StringToDouble(txt_fy.Text,0.0);
                gamma_c = MyList.StringToDouble(txt_gamma_c.Text,0.0);
                gamma_wc = MyList.StringToDouble(txt_gamma_wc.Text,0.0);
                _IF = MyList.StringToDouble(txt_IF.Text,0.0);
                CF = MyList.StringToDouble(txt_CF.Text, 0.0);
                dm = MyList.StringToDouble(txt_dm.Text, 0.0);
                dd = MyList.StringToDouble(txt_dd.Text, 0.0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }

        #endregion

        public double Get_Table_1_Value(double B_by_L, int indx)
        {
            return iApp.Tables.K_Val_Simply_Continous_Supported_Slab(B_by_L, indx, ref ref_string);

            //B_by_L = Double.Parse(B_by_L.ToString("0.0"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "RccDeckSlab_Table_1.txt");

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
            //    if (B_by_L < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (B_by_L > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == B_by_L)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > B_by_L)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (B_by_L - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.00"));
            //return returned_value;
        }
        public void Write_Table_1(StreamWriter sw)
        {
            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "RccDeckSlab_Table_1.txt");
            List<string> lst_content = iApp.Tables.Get_Tables_K_Val_Simply_Continous_Supported_Slab();

            sw.WriteLine();
            sw.WriteLine("TABLE 1 : ");
            sw.WriteLine("--------");
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();
            sw.WriteLine();
            lst_content.Clear();
        }
        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "RCC_DECK_SLAB_DRAWING.FIL");
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
                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_sp2={0}", _sp2);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
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
            Calculate_Program();
            Write_Drawing_File();
            if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            is_process = true;
            FilePath = user_path;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "PreStressed_Deck_Slab", "");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_fck_TextChanged(object sender, EventArgs e)
        {
            InitializeData();
            double fcc, j, Q, fcb, n;


            fcb = fck / 3;
            fcc = fck / 4;

            n = m * fcb / (m * fcb + sigma_st);

            j = 1 - (n / 3.0);
            Q = n * j * fcb / 2;

            txt_sigma_cb.Text = fcb.ToString("0.00");
            txt_j.Text = j.ToString("0.000");
            txt_Q.Text = Q.ToString("0.000");
        }
    }
}
