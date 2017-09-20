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

namespace AstraFunctionOne.BridgeDesign.SteelTruss
{
    public partial class frmSteelTrussDeckSlab : Form
    {
        IApplication iApp;


        string rep_file_name = "";
        string file_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_path = "";
        string drawing_path = "";
        bool is_process = false;
        

        double Ds, gamma_c;
        double self_weight_slab, self_weight_wearing_cource, tw;
        double Dwc, gamma_wc;
        double effe_span, concrete_grade, steel_grade, sigma_cb, sigma_st;
        double m, j, Q, minimum_cover, L;
        //double m, j, Q, minimum_cover, L, no_main_girder, width_long_girders;
        double B;
        //double B, width_cross_girders;
        double load, width, length, impact_factor, continuity_factor, mu;



        double _A, _B, _C, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;

        public frmSteelTrussDeckSlab(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;
            _A = 0.0;
            _B = 0.0;
            _C = 0.0;
            _D = 0.0;
            _E = 0.0;
            _F = 0.0;
            _bd1 = 0.0;
            _sp1 = 0.0;
            _bd2 = 0.0;
            _sp2 = 0.0;

            B = 0d;
            L = 0d;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program(rep_file_name);
            Write_Drawing_File();
            if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            is_process = true;
            FilePath = user_path;
        }
        public void Calculate_Program(string file_name)
        {
            frmCurve f_c = null;

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            
            
            #region TechSOFT Banner
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("\t\t**********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 5.0           *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
            sw.WriteLine("\t\t*                                            *");
            sw.WriteLine("\t\t*           DESIGN OF DECK SLAB              *");
            sw.WriteLine("\t\t*          FOR T-BEAM RCC BRIDGE             *");
            sw.WriteLine("\t\t**********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion


            try
            {
                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine(" Panel Length [L]= {0} m", L, "Marked as (C) in the Drawing");
                sw.WriteLine(" Spacing of main Girders [B] = {0} m", B, "Marked as (A) in the Drawing");

                //sw.WriteLine(" Width of Carriage way = {0:f3} m", width_carrage_way);
                //sw.WriteLine(" Effective Span of Tee Beam = {0} m", effe_span);
                sw.WriteLine(" Concrete Grade = M {0:f0} ", concrete_grade);
                sw.WriteLine(" Steel Grade = Fe {0:f0} ", steel_grade);
                sw.WriteLine(" Permissible Stress in Concrete [σ_cb] = {0} N/sq.m", sigma_cb);
                sw.WriteLine(" Permissible Stress in Steel [σ_st] = {0} N/sq.m", sigma_st);
                //sw.WriteLine(" No. Of Main girders = {0:f0} ", no_main_girder);
                //sw.WriteLine(" Width of Cross Girders = {0} mm {1,40}", width_cross_girders, "Marked as (D) in the Drawing");
                //sw.WriteLine(" Width of Long Girders = {0} mm {1,40}", width_long_girders, "Marked as (B) in the Drawing");
                sw.WriteLine(" Modular ratio [m] = {0} ", m);
                sw.WriteLine(" Lever arm factor [j] = {0} ", j);
                sw.WriteLine(" Moment factor [Q] = {0} ", Q);

                sw.WriteLine(" Minimum Cover = {0:f3} mm", minimum_cover);

                sw.WriteLine(" Load = {0:f3} kN",load);
                sw.WriteLine(" Width of Load [a]= {0:f3} m",width);
                sw.WriteLine(" Length of Load [b]= {0:f3} m",length);
                sw.WriteLine(" Impact Factor [IF]= {0:f3} ", impact_factor);
                sw.WriteLine(" Continuity Factor [CF]= {0:f3} ", continuity_factor);
                sw.WriteLine(" Constant [µ] = {0:f3} ", mu);

                sw.WriteLine(" Thickness of concrete Deck Slab [Ds] = {0} mm ", Ds, "Marked as (F) in the Drawing");
                sw.WriteLine(" Unit weight of concrete Deck Slab [γ_c]= {0} kN/cu.m", gamma_c);
                sw.WriteLine(" Thickness of Asphalt Wearing Course [Dwc] = {0} mm ", Dwc, "Marked as (E) in the Drawing");
                sw.WriteLine(" Unit weight of Asphalt Wearing Course [γ_wc] = {0} kN/cu.m", gamma_wc);

                sw.WriteLine();
                sw.WriteLine();
                #endregion

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : DEAD LOAD ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Self weight of slab = Ds * γ_c = {0} * {1} = {2} kN/sq.m.",
                    (Ds / 1000),
                    gamma_c,
                    self_weight_slab);
                sw.WriteLine();
                sw.WriteLine("Self weight of wearing course = Dwc * γ_wc ");

                sw.WriteLine("                              = {0} * {1}", (Dwc / 1000), gamma_wc);
                sw.WriteLine("                              = {0} kN/sq.m.", self_weight_wearing_cource);

                sw.WriteLine();
                sw.WriteLine("            Total weight = tw = {0} + {1}", self_weight_slab,self_weight_wearing_cource);
                sw.WriteLine("                              = {0} kN/sq.m.", tw);

                double wd;
                wd = B * L * tw;
                sw.WriteLine();
                sw.WriteLine("Total Permanent Load on Slab Panel = Wd = B * L * tw");
                sw.WriteLine("                                   = {0} * {1} * {2}",
                                  B,
                                  L,
                                  tw);
                sw.WriteLine("                                   = {0} kN", wd);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Full slab Panel is Loaded with uniformly distributed load");
                double k;
                k = B / L;

                sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f2}",
                    B,
                    L,
                    k);
                sw.WriteLine("1/k = 1 / {0} = {1}",
                                    k,
                                    (1 / k));

                f_c = new frmCurve(k, 0.0, 0.0, LoadType.FullyLoad);
                f_c.m1 = 0.045;
                f_c.m2 = 0.004;
                f_c.ShowDialog();
                double m1, m2, MB1, ML1;

                m1 = f_c.m1;
                m2 = f_c.m2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Select m1 and m2 from Pigeaud's curve");
                sw.WriteLine(" m1 = {0}    and      m2 = {1}", m1, m2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Permanent Load Bending Moments are obtained by");
                sw.WriteLine("applying continuity Factor (CF) as, ");
                sw.WriteLine();

                MB1 = wd * (m1 + mu * m2);
                sw.WriteLine("MB1 = wd * (m1 + mu * m2)");

                sw.WriteLine("    = {0:f3} * ({1:f3} + {2:f3} * {3:f3})", wd, m1, mu, m2);
                MB1 = continuity_factor * MB1;

                sw.WriteLine("    = {0:f3} kN-m", MB1);
                sw.WriteLine();

                sw.WriteLine("Taking Continuity into effect = MB1 = {0:f3} * {1:f3}", continuity_factor, MB1);
                sw.WriteLine("                                    = {0:f3} ", continuity_factor, MB1);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("ML1 = (CF * Wd) * (m2 + µm1)");

                sw.WriteLine("    = ({0:f2} * {1:f2}) * ({2:f4} + {3:f4} * {4:f4})",
                    continuity_factor,
                    wd,
                    m2,
                    mu,
                    m1);
                ML1 = (continuity_factor * wd) * (m2 + mu * m1);
                sw.WriteLine("   = {0:f3} kN-m", ML1);

                //sw.WriteLine();
                //sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Calculations for Bending Moments for Imposed Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //double B = 2.5;
                //double L = 4.0;

                //m1 0.0049  0.081  m2 0.015  0.022
                //m1 0.049  0.081  m2 0.015  0.022
                double _k = B / L;
                sw.WriteLine(" B = {0:f3} m.", B);
                sw.WriteLine(" L = {0:f3} m.", L);
                sw.WriteLine();
                sw.WriteLine(" k = B / L = {0:f2} / {1:f2} = {2:f2} m.",
                    B,
                    L,
                    _k);
                double a = 0.84;
                double b = 4.57;

                double u, v;
                u = a + 2.0 * (Dwc/1000.0);
                sw.WriteLine();
                sw.WriteLine("u = a + 2 * Dwc = {0} + 2 * {1} = {2:f2} m",
                    a,
                    (Dwc/1000),
                    u);

                v = b + 2.0 * (Dwc / 1000.0);
                sw.WriteLine("v = b + 2 * Dwc = {0} + 2 * {1} = {2:f2} m",
                    b,
                    (Dwc / 1000),
                    v);
                double _v = 0.0;
                _v = v;
                if (v > L) v = L;

                double u_by_B = u / B;
                double v_by_L = v / L;

                if (k < 0.4)
                    k = 0.4;
                if (k > 1.0) k = 1.0;
                f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);
                f_c.m1 = 0.085;
                f_c.m2 = 0.009;
                f_c.ShowDialog();
                double _m1, _m2;
                _m1 = f_c.m1;
                _m2 = f_c.m2;

                sw.WriteLine();
                sw.WriteLine("u/B = {0} / {1} = {2:f2}",
                    u,
                    B,
                    u_by_B);
                sw.WriteLine("v/L = {0} / {1} = {2:f2}",
                                    v,
                                    L,
                                    v_by_L);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Select m1 & m2 from Pigeaud's curves");
                sw.WriteLine("Corresponding to k = {0:f3}, u/B = {1:f2} and v/L = {2:f2}",
                    _k,
                    u_by_B,
                    v_by_L);
                sw.WriteLine("m1 = {0} and m2 = {1}",
                    _m1,
                    _m2);

                double total_impact_load;
                total_impact_load = impact_factor * load;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Load per track including impact");
                sw.WriteLine("   = IF * load");
                sw.WriteLine("   = {0:f2} * {1:f2}", impact_factor, load);
                sw.WriteLine("   = {0:f2} N", total_impact_load);

                double w2 = total_impact_load * (L / _v);
                sw.WriteLine();
                sw.WriteLine("Effective load on slab Panel = w2");
                sw.WriteLine("    = {0:f2} * ({1:f2}/{2:f2})", total_impact_load, L, b);
                sw.WriteLine("    = {0:f2} kN", w2);
                sw.WriteLine();
                sw.WriteLine("Moment along Shorter span ");

                double _MB = w2 * (_m1 + mu * _m2);
                sw.WriteLine("       = MB2 = w2 * (m1 + µ*m2)");
                sw.WriteLine("       = {0:f3} * ({1:f3} + {2:f3}*{3:f3}", w2, _m1, mu, _m2);
                sw.WriteLine("       = {0:f3} kN", _MB);

                double _ML = w2 * (_m2 + mu * _m1);
                sw.WriteLine();
                sw.WriteLine("       = ML2 = w2 * (m2 + µ*m1)");
                sw.WriteLine("       = {0:f3} * ({1:f3} + {2:f3} * {3:f3}", w2, m2, mu, m1);
                sw.WriteLine("       = {0:f3} kN", _ML);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Slab is designed as Continuous,");
                sw.WriteLine("The Imposed Load Bending Moment are obtained");
                sw.WriteLine("by applying Continuity Factor(CF) as");
                sw.WriteLine();
                sw.WriteLine("MB2 = CF * MB2 = {0:f3} * {1:f3} = {2:f3} kN-m",
                    continuity_factor, _MB,
                    (continuity_factor * _MB));
                _MB = continuity_factor * _MB;

                sw.WriteLine("ML2 = CF * ML2 = {0:f3} * {1:f3} = {2:f3} kN-m",
                                    continuity_factor, _ML,
                                    (continuity_factor * _ML));
                _ML = continuity_factor * _ML;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Final Design Bending Moments for the Slab ");
                sw.WriteLine("Short Span B.N. = MB = {0:f3} + {1:f3} = {2:f3} kN-m",
                    MB1, _MB,
                    (MB1 + _MB));
                MB1 += _MB;
                sw.WriteLine("Long Span B.M. = ML = {0:f3} + {1:f3} = {2:f3} kN-m",
                    ML1, _ML,
                    (ML1 + _ML));
                ML1 += _ML;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Calculations for Effective Depth ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double d;

                d = (MB1 * 10E5) / (Q * 1000);
                d = Math.Sqrt(d);

                sw.WriteLine("d = √((MB * 10E+5)/(Q * 1000))");
                sw.WriteLine("  = √(({0:f3} * 10E+5)/({1:f3} * 1000)) ", MB1, Q);
                sw.WriteLine("  = {0:f3} mm", d);

                d = (int)(d / 10.0);
                d = d + 1;
                d = d * 10;

                sw.WriteLine("  = {0:f3} mm", d);

                double d1, d2;
                d1 = 12;

                //sw.WriteLine();
                //sw.WriteLine("Using {0:f0} mm dia bars", d1);
                double overall_depth = 0.0;
                overall_depth = d + minimum_cover;

                sw.WriteLine();
                sw.WriteLine("Overall depth of Deck slab = {0:f3} + {1:f3}", d, minimum_cover);

                sw.WriteLine("                           = {0:f3} mm", overall_depth);

                //double _over_dep;
                //_over_dep = overall_depth / 100;
                //_over_dep = (double)(int)_over_dep;
                sw.WriteLine();
                //sw.WriteLine("{0:f3} / 100 = {1:f3} = {2:f0}",
                //    overall_depth,
                //    (overall_depth / 100),
                //    _over_dep);

                //double _o_depth;
                //_o_depth = _over_dep * 100 + 50;
                sw.WriteLine();
                if (Ds > overall_depth)
                {
                    sw.WriteLine("Provided Overall Depth = {0:f2} > {1:f2}, So OK", Ds, overall_depth);
                }
                if (Ds < overall_depth)
                {
                    sw.WriteLine("Provided Overall Depth = {0:f2} > {1:f2}, So NOT OK", Ds, overall_depth);
                }

                sw.WriteLine();

                double eff_depth;
                eff_depth = Ds - minimum_cover;
                sw.WriteLine();
                sw.WriteLine("Provided Effective depth = d1 = {0:f3} - {1:f3} = {2:f3} mm",
                    Ds,
                    minimum_cover,
                    eff_depth);

                //double adopt_eff_depth;
                //adopt_eff_depth = (int)(eff_depth / 10);
                //adopt_eff_depth *= 10;

                //sw.WriteLine();
                //sw.WriteLine("Adopt Eff. Depth  = {0:f3} mm", adopt_eff_depth);

                double Ast1 = (MB1 * 10E5) / (sigma_st * j * eff_depth);
                //S = S / 10;
                //S = (int)S;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Calculations for Reinforcement along shorter span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Short Span Moment = MB = {0:f3} kN-m", MB1);
                sw.WriteLine();
                sw.WriteLine("Ast1 = (Mb * 10E5)/(σ_st * j * d)");
                sw.WriteLine("     = ({0:f3} * 10E5)/({1:f3} * {2:f3}* {3:f3})", MB1, sigma_st, j, eff_depth);
                sw.WriteLine("     = {0:f0} sq.mm",Ast1);
                double S = (1000 * (Math.PI * d1 * d1 / 4)) / Ast1;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("spacing of Bars = S = (1000 * (π * 12 * 12/4))/ Ast1");
                sw.WriteLine("                    = {0:f0} mm", S);
                sw.WriteLine("                    = {0:f0} /10 = {1:f3} = {1:f0}", S, (S / 10.0));

                if (S > 145)
                {
                    S = 150;
                }
                else
                {
                    S = (int)(S / 10.0);
                    S = (S * 10.0);
                }
                sw.WriteLine("                    = {0:f0} * 10 = {1:f0} mm", (S / 10.0),S);

                sw.WriteLine();
                sw.WriteLine("Adopt T12 bars @{0:f0} mm c/c", S, "Marked as (1) in the Drawing");

                _sp1 = S;
                _bd1 = 12;



                d2 = 10;
                double res_eff_depth;
                res_eff_depth = eff_depth - (d1 / 2) - (d2 / 2);
                double Ast2;
                Ast2 = (ML1 * 10E5) / (sigma_st * j * res_eff_depth);

                //S = (1000.0 * (Math.PI * 12.0 * 12.0 / 4.0)) / Ast2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Calculations for Reinforcement along longer span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Use 10 mm dia bars,");
                sw.WriteLine("Respective effective depth = {0} - {1} = {2} mm",
                    eff_depth,
                    (d2/2).ToString("0"),
                    res_eff_depth);

                sw.WriteLine();
                sw.WriteLine("Ast2 = (ML * 10E5)/(σ_st * j* d)");
                sw.WriteLine("     = ({0:f3} * 10E5)/({1:f2} * {2:f2}* {3:f2})",
                    ML1, sigma_st, j, res_eff_depth);
                sw.WriteLine("     = {0:f2} sq.mm",Ast2);
                sw.WriteLine();
                sw.WriteLine("Minimum Required Reinforcement = 0.12% of Slab Section");
                sw.WriteLine("                               = (0.12/100)*({0}*1000) of Slab Section", Ds);


                double val = (0.12 / 100) * (Ds * 1000);
                sw.WriteLine("                               = {0:f3} sq.mm", val);
                sw.WriteLine();
                sw.WriteLine();


                double spacing_bars;
                spacing_bars = (1000 * Math.PI * d2 * d2) / (4 * Ast2);

               

                sw.WriteLine();
                sw.WriteLine("spacing of Bars = S = (1000 * (π * 10 * 10/4))/ Ast2");
                sw.WriteLine("                    = {0:f3} mm", spacing_bars);
                //sw.WriteLine("                    = {0:f3}/10 = {1:f3} = {1:f0} ", spacing_bars,
                    //(spacing_bars/10));

                double _spacing_bars = (int)(spacing_bars / 10);
                _spacing_bars = _spacing_bars * 10;

                sw.WriteLine("                    = {0:f0} mm", _spacing_bars);



                if (_spacing_bars > 200)
                    _spacing_bars = 200;
                else
                {
                    _spacing_bars = (int)(_spacing_bars / 10.0);
                    _spacing_bars = _spacing_bars * 10;
                }

                _sp2 = _spacing_bars;
                _bd2 = 10;
                sw.WriteLine();
                sw.WriteLine("Provide T10 bars @{0:f0} mm c/c at Top & Bottom", _spacing_bars, "Marked as (2) in the Drawing");
                sw.WriteLine();
                Ast2 = (Math.PI * 10 * 10 / 4.0) * (1000 / S);
                sw.WriteLine("Ast2 = (π * 10 * 10/4)* (1000/{0})", S);
                sw.WriteLine("     = {0:f3} sq.mm", Ast2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

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

        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "DECK_SLAB_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                
                _A = B;
                //_B = (width_long_girders / 1000.0);
                _C = L;
                //_D = (width_cross_girders / 1000.0);
                _E = Dwc / 1000.0;
                _F = Ds / 1000.0;
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_bd1=BAR DIA  {0:f0}", _bd1);
                sw.WriteLine("_sp1=SPCG. {0:f0} MM C/C", _sp1);
                sw.WriteLine("_bd2=BAR DIA  {0}", _bd2);
                sw.WriteLine("_sp2=SPCG. {0:f0} MM C/C", _sp2);
            }
            catch (Exception ex){}
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
                L = MyList.StringToDouble(txt_L.Text, 0.0);
                B = MyList.StringToDouble(txt_B.Text, 0.0);
                Ds = MyList.StringToDouble(txt_Ds.Text, 0.0);
                Dwc = MyList.StringToDouble(txt_Dwc.Text, 0.0);
                gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
                gamma_wc = MyList.StringToDouble(txt_gamma_wc.Text, 0.0);

                concrete_grade = MyList.StringToDouble(txt_concrete_grade.Text, 0.0);
                steel_grade = MyList.StringToDouble(txt_steel_grade.Text, 0.0);
                sigma_cb = MyList.StringToDouble(txt_sigma_cb.Text, 0.0);
                sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
               
                m = MyList.StringToDouble(txt_m.Text, 0.0);
                j = MyList.StringToDouble(txt_j.Text, 0.0);
                Q = MyList.StringToDouble(txt_Q.Text, 0.0);

                minimum_cover = MyList.StringToDouble(txt_minimum_cover.Text, 0.0);

                load = MyList.StringToDouble(txt_enter_load.Text, 0.0);
                width = MyList.StringToDouble(txt_load_width.Text, 0.0);
                length = MyList.StringToDouble(txt_load_length.Text, 0.0);
                impact_factor = MyList.StringToDouble(txt_impact_factor.Text, 0.0);
                continuity_factor = MyList.StringToDouble(txt_continuity_factor.Text, 0.0);
                mu = MyList.StringToDouble(txt_mu.Text, 0.0);


                self_weight_slab = (Ds / 1000) * gamma_c;
                self_weight_wearing_cource = (Dwc / 1000) * gamma_wc;
                tw = (self_weight_slab + self_weight_wearing_cource);
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
                    //indx = lst_content[i].LastIndexOf(" ");
                    //if (indx > 0)
                    //    kStr = MyList.RemoveAllSpaces(lst_content[i].Substring(0, indx));
                    //else
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "EFFE_SPAN":
                            effe_span = mList.GetDouble(1);
                            //txt_effective_span_Tee_beam.Text = effe_span.ToString();
                            break;
                        case "CONCRETE_GRADE":
                            concrete_grade = mList.GetDouble(1);
                            txt_concrete_grade.Text = concrete_grade.ToString();
                            break;
                        case "STEEL_GRADE":
                            steel_grade = mList.GetDouble(1);
                            txt_steel_grade.Text = steel_grade.ToString();
                            break;
                        case "SIGMA_CB":
                            sigma_cb = mList.GetDouble(1);
                            txt_sigma_cb.Text = sigma_cb.ToString();
                            break;
                        case "SIGMA_ST":
                            sigma_st = mList.GetDouble(1);
                            txt_sigma_st.Text = sigma_st.ToString();
                            break;
                        case "SPACING_CROSS_GIRDER":
                            L = mList.GetDouble(1);
                            txt_L.Text = L.ToString();
                            break;
                        case "SPACING_MAIN_GIRDER":
                            B = mList.GetDouble(1);
                            txt_B.Text = B.ToString();
                            break;
                        
                        case "M":
                            m = mList.GetDouble(1);
                            txt_m.Text = m.ToString();
                            break;
                        case "J":
                            j = mList.GetDouble(1);
                            txt_j.Text = j.ToString();
                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            txt_Q.Text = Q.ToString();
                            break;
                        case "MINIMUM_COVER":
                            minimum_cover = mList.GetDouble(1);
                            txt_minimum_cover.Text = minimum_cover.ToString();
                            break;
                        case "LOAD":
                            load = mList.GetDouble(1);
                            txt_enter_load.Text = load.ToString();
                            break;
                        case "WIDTH":
                            width = mList.GetDouble(1);
                            txt_load_width.Text = width.ToString();
                            break;
                        case "LENGTH":
                            length = mList.GetDouble(1);
                            txt_load_length.Text = length.ToString();
                            break;
                        case "IMPACT_FACTOR":
                            impact_factor = mList.GetDouble(1);
                            txt_impact_factor.Text = impact_factor.ToString();
                            break;
                        case "CONTINUITY_FACTOR":
                            continuity_factor = mList.GetDouble(1);
                            txt_continuity_factor.Text = continuity_factor.ToString();
                            break;
                        case "MU":
                            mu = mList.GetDouble(1);
                            txt_mu.Text = mu.ToString();
                            break;
                        case "DS":
                            Ds = mList.GetDouble(1);
                            txt_Ds.Text = Ds.ToString();
                            break;
                        case "GAMMA_C":
                            gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "DWC":
                            Dwc = mList.GetDouble(1);
                            txt_Dwc.Text = Dwc.ToString();
                            break;
                        case "GAMMA_WC":
                            gamma_wc = mList.GetDouble(1);
                            txt_gamma_wc.Text = gamma_wc.ToString();
                            break;
                    }
                    #endregion
                }


            }
            catch(Exception ex)
            {
            }
            lst_content.Clear();
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {


                #region User Data

                //sw.WriteLine("WIDTH_CARRAGE_WAY = {0}", txtWidthCarrageWay.Text);
                //sw.WriteLine("EFFE_SPAN = {0}", txt_effective_span_Tee_beam.Text);
                sw.WriteLine("CONCRETE_GRADE = {0}", txt_concrete_grade.Text);
                sw.WriteLine("STEEL_GRADE = {0}", txt_steel_grade.Text);
                sw.WriteLine("SIGMA_CB = {0}", txt_sigma_cb.Text);
                sw.WriteLine("SIGMA_ST = {0}", txt_sigma_st.Text);
                sw.WriteLine("SPACING_CROSS_GIRDER = {0}", txt_L.Text);
                //sw.WriteLine("NO_MAIN_GIRDER = {0}", txt_no_of_main_girders.Text);
                sw.WriteLine("SPACING_MAIN_GIRDER = {0}", txt_B.Text);
                //sw.WriteLine("WIDTH_CROSS_GIRDERS = {0}", txt_width_of_cross_girders.Text);
                //sw.WriteLine("WIDTH_LONG_GIRDERS = {0}", txt_width_of_long_girders.Text);
                sw.WriteLine("M = {0}", txt_m.Text);
                sw.WriteLine("J = {0}", txt_j.Text);
                sw.WriteLine("Q = {0}", txt_Q.Text);
                sw.WriteLine("MINIMUM_COVER = {0}", txt_minimum_cover.Text);
                sw.WriteLine("LOAD = {0}", txt_enter_load.Text);
                sw.WriteLine("WIDTH = {0}", txt_load_width.Text);
                sw.WriteLine("LENGTH = {0}", txt_load_length.Text);
                sw.WriteLine("IMPACT_FACTOR = {0}", txt_impact_factor.Text);
                sw.WriteLine("CONTINUITY_FACTOR = {0}", txt_continuity_factor.Text);
                sw.WriteLine("MU = {0}", txt_mu.Text);
                sw.WriteLine("DS = {0}", txt_Ds.Text);
                sw.WriteLine("GAMMA_C = {0}", txt_gamma_c.Text);
                sw.WriteLine("DWC = {0}", txt_Dwc.Text);
                sw.WriteLine("GAMMA_WC = {0}", txt_gamma_wc.Text);

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
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
            kPath = Path.Combine(kPath, "Steel Truss Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Deck Slab");

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
                this.Text = "DESIGN OF STEEL TRUSS DECK SLAB : " + value;
                user_path = value;

                //file_path = Path.Combine(user_path, "DECK_SLAB");
                file_path = GetAstraDirectoryPath(user_path);
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);


                rep_file_name = Path.Combine(file_path, "Bridge_Steel_Truss_Deck_Slab.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_STEEL_TRUSS_DECK_SLAB.FIL");


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

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "TBEAM_Deck_Slab", "");
        }
        private void rbtn_select_load_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;

            if (rbtn_select_load.Checked)
            {
                cmb_select_load.Enabled = true;
                cmb_select_load.SelectedIndex = 0;
                txt_enter_load.Enabled = false;
                txt_load_width.Enabled = false;
                txt_load_length.Enabled = false;
            }
            else
            {
                cmb_select_load.Enabled = false;
                cmb_select_load.SelectedIndex = -1;
                txt_enter_load.Enabled = true;
                txt_load_width.Enabled = true;
                txt_load_length.Enabled = true;
            }
        }

        private void cmb_select_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_select_load.SelectedIndex)
            {
                case 0://70 R WHEEL
                    txt_load_length.Text = "5.79";
                    txt_load_width.Text = "1.35";
                    txt_enter_load.Text = "680";
                    break;
                case 1://70 R TRACK
                    txt_load_length.Text = "4.87";
                    txt_load_width.Text = "0.84";
                    txt_enter_load.Text = "350";
                    break;
                case 2://CLASS AA WHEEL
                    txt_load_length.Text = "2.50";
                    txt_load_width.Text = "1.35";
                    txt_enter_load.Text = "400";
                    break;
                case 3://CLASS AA TRACK
                    txt_load_length.Text = "3.60";
                    txt_load_width.Text = "0.85";
                    txt_enter_load.Text = "350";
                    break;
                case 4://CLASS A WHEEL
                    txt_load_length.Text = "3.2";
                    txt_load_width.Text = "1.20";
                    txt_enter_load.Text = "228";
                    break;
                case 5: //CLASS B WHEEL
                    txt_load_length.Text = "4.30";
                    txt_load_width.Text = "1.20";
                    txt_enter_load.Text = "109";
                    break;
            }
        }

        private void frmSteelTrussDeckSlab_Load(object sender, EventArgs e)
        {
            rbtn_select_load.Checked = true;
            cmb_select_load.SelectedIndex = 1;
        }
    }
}
