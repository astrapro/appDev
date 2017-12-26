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


namespace AstraFunctionOne.BridgeDesign
{
    public partial class frmDesignDeckSlab : Form
    {
        string rep_file_name = "";
        string file_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_path = "";
        string drawing_path = "";
        bool is_process = false;
        

        double Ds, gamma_c;
        double self_weight_slab, self_weight_wearing_cource, total_weight;
        double Dwc, gamma_wc;
        double width_carrage_way, effe_span, concrete_grade, steel_grade, sigma_cb, sigma_st;
        double m, j, Q, minimum_cover, L, no_main_girder, width_long_girders;
        double B, width_cross_girders;
        double load, width, length, impact_factor, continuity_factor, mu;



        double _A, _B, _C, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;
        IApplication iApp = null;
        public sbyte TBeamDesign { get; set; }
        public frmDesignDeckSlab(IApplication app, sbyte TBeamDesignOption)
        {
            TBeamDesign = TBeamDesignOption;
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
            sw.WriteLine("\t\t*                 ASTRA Pro                  *");
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
                sw.WriteLine(" Width of Carriage way = {0:f3} m", width_carrage_way);
                sw.WriteLine(" Effective Span of Tee Beam = {0} m", effe_span);
                sw.WriteLine(" Concrete Grade = M {0:f0} ", concrete_grade);
                sw.WriteLine(" Steel Grade = Fe {0:f0} ", steel_grade);
                sw.WriteLine(" Permissible Stress in Concrete [σ_cb] = {0} N/sq.m", sigma_cb);
                sw.WriteLine(" Permissible Stress in Steel [σ_st] = {0} N/sq.m", sigma_st);
                sw.WriteLine(" Spacing of Cross Girders [L]= {0} m {1,40}", L, "Marked as (C) in the Drawing");
                sw.WriteLine(" No. Of Main girders = {0:f0} ", no_main_girder);
                sw.WriteLine(" Spacing of main Girders [B] = {0} m {1,40}", B, "Marked as (A) in the Drawing");
                sw.WriteLine(" Width of Cross Girders = {0} mm {1,40}", width_cross_girders, "Marked as (D) in the Drawing");
                sw.WriteLine(" Width of Long Girders = {0} mm {1,40}", width_long_girders, "Marked as (B) in the Drawing");
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

                sw.WriteLine(" Thickness of concrete Deck Slab [Ds] = {0} mm {1,40}", Ds, "Marked as (F) in the Drawing");
                sw.WriteLine(" Unit weight of concrete Deck Slab [γ_c]= {0} kN/cu.m", gamma_c);
                sw.WriteLine(" Thickness of Asphalt Wearing Course [Dwc] = {0} mm {1,40}", Dwc, "Marked as (E) in the Drawing");
                sw.WriteLine(" Unit weight of Asphalt Wearing Course [γ_wc] = {0} kN/cu.m", gamma_wc);

                sw.WriteLine();
                sw.WriteLine();
                #endregion

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Calculations for Bending Moments for Permanent Load ");
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
                sw.WriteLine("                 Total weight = {0} + {1}", self_weight_slab,self_weight_wearing_cource);
                sw.WriteLine("                              = {0} kN/sq.m.", total_weight);

                double w1;

                w1 = B * no_main_girder * total_weight;
                sw.WriteLine();
                sw.WriteLine("Total Permanent Load on Slab Panel = W1");
                sw.WriteLine("                                   = {0} * {1} * {2}",
                                  B,
                                  no_main_girder,
                                  total_weight);
                sw.WriteLine("                                   = {0} kN", w1);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Full slab Panel is Loaded with uniformly distributed load");
                double k;
                k = B / no_main_girder;

                sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f2}",
                    B,
                    no_main_girder,
                    k);
                sw.WriteLine("1/k = 1 / {0} = {1}",
                                    k,
                                    (1 / k));

                f_c = new frmCurve(k, 0.0, 0.0, LoadType.FullyLoad);
                f_c.ShowDialog();
                double m1, m2, MB, ML;

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
                sw.WriteLine("MB = (CF * w1) * (m1 + µm2)");

                sw.WriteLine("   = ({0:f2} * {1:f2}) * ({2:f4} + {3:f4} * {4:f4})",
                    continuity_factor,
                    w1,
                    m1,
                    mu,
                    m2);
                MB = (continuity_factor * w1) * (m1 + (mu * m2));
                sw.WriteLine("   = {0:f3} kN-m", MB);


                sw.WriteLine();
                sw.WriteLine("ML = (CF * w1) * (m2 + µm1)");

                sw.WriteLine("   = ({0:f2} * {1:f2}) * ({2:f4} + {3:f4} * {4:f4})",
                    continuity_factor,
                    w1,
                    m2,
                    mu,
                    m1);
                ML = (continuity_factor * w1) * (m2 + mu * m1);
                sw.WriteLine("   = {0:f3} kN-m", ML);

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
                sw.WriteLine("       = MB = w2 * (m1 + µ*m2)");
                sw.WriteLine("       = {0:f3} * ({1:f3} + {2:f3}*{3:f3}", w2, _m1, mu, _m2);
                sw.WriteLine("       = {0:f3} kN", _MB);

                double _ML = w2 * (_m2 + mu * _m1);
                sw.WriteLine();
                sw.WriteLine("       = ML = w2 * (m2 + µ*m1)");
                sw.WriteLine("       = {0:f3} * ({1:f3} + {2:f3} * {3:f3}", w2, m2, mu, m1);
                sw.WriteLine("       = {0:f3} kN", _ML);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The Slab is designed as Continuous,");
                sw.WriteLine("The Imposed Load Bending Moment are obtained");
                sw.WriteLine("by applying Continuity Factor(CF) as");
                sw.WriteLine();
                sw.WriteLine("MB = CF * MB = {0:f3} * {1:f3} = {2:f3} kN-m",
                    continuity_factor, _MB,
                    (continuity_factor * _MB));
                _MB = continuity_factor * _MB;

                sw.WriteLine("ML = CF * ML = {0:f3} * {1:f3} = {2:f3} kN-m",
                                    continuity_factor, _ML,
                                    (continuity_factor * _ML));
                _ML = continuity_factor * _ML;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Final Design Bending Moments for the Slab ");
                sw.WriteLine("Short Span B.N. = MB = {0:f3} + {1:f3} = {2:f3} kN-m",
                    MB, _MB,
                    (MB + _MB));
                MB += _MB;
                sw.WriteLine("Long Span B.M. = ML = {0:f3} + {1:f3} = {2:f3} kN-m",
                    ML, _ML,
                    (ML + _ML));
                ML += _ML;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Calculations for Effective Depth ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double d;

                d = (MB * 10E5) / (Q * 1000);
                d = Math.Sqrt(d);

                sw.WriteLine("d = √((MB * 10E+5)/(Q * 1000))");
                sw.WriteLine("  = √(({0:f3} * 10E+5)/({1:f3} * 1000)) ", MB, Q);
                sw.WriteLine("  = {0:f3} mm", d);

                double d1, d2;
                d1 = 12;

                sw.WriteLine();
                sw.WriteLine("Using {0:f0} mm dia bars", d1);
                double overall_depth = 0.0;
                overall_depth = d + minimum_cover + d1 / 2;

                sw.WriteLine();
                sw.WriteLine("Overall depth of Deck slab = {0:f3} + {1:f3} + {2:f3}", d, minimum_cover, (d1 / 2));

                sw.WriteLine("                           = {0:f3} mm", overall_depth);

                double _over_dep;
                _over_dep = overall_depth / 100;
                _over_dep = (double)(int)_over_dep;
                sw.WriteLine();
                sw.WriteLine("{0:f3} / 100 = {1:f3} = {2:f0}",
                    overall_depth,
                    (overall_depth / 100),
                    _over_dep);

                double _o_depth;
                _o_depth = _over_dep * 100 + 50;
                sw.WriteLine();
                if (_o_depth > overall_depth)
                {
                    sw.WriteLine("{0:f0} * 100 + 50 = {1:f0} > {2:f2} OK", _over_dep, _o_depth, overall_depth);
                }
                else
                {
                    _o_depth += 50;
                    sw.WriteLine("{0:f0} * 100 + 50 + 50 = {1:f0} > {2:f2} OK", _over_dep, _o_depth, overall_depth);
                }

                sw.WriteLine();

                if (_o_depth < overall_depth)
                    _o_depth += 50;
                double eff_depth;
                eff_depth = _o_depth - minimum_cover - (d1 / 2);
                sw.WriteLine();
                sw.WriteLine("Effective depth d = {0:f3} - {1:f3} - {2:f3} = {3:f3} mm",
                    _o_depth,
                    minimum_cover,
                    (d1 / 2), eff_depth);

                double adopt_eff_depth;
                adopt_eff_depth = (int)(eff_depth / 10);
                adopt_eff_depth *= 10;

                sw.WriteLine();
                sw.WriteLine("Adopt Eff. Depth  = {0:f3} mm", adopt_eff_depth);
                
                double Ast1 = (MB * 10E5) / (sigma_st * j * adopt_eff_depth);
                //S = S / 10;
                //S = (int)S;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Calculations for Reinforcement along shorter span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Ast1 = (Mb * 10^6)/(σ_st * j * d)");
                sw.WriteLine("     = ({0:f3} * 10^6)/({1:f3} * {2:f3}* {3:f3})", MB, sigma_st, j, adopt_eff_depth);
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
                sw.WriteLine("Adopt T12 bars @{0:f0} mm c/c {1,40}", S, "Marked as (1) in the Drawing");

                _sp1 = S;
                _bd1 = 12;



                d2 = 10;
                double res_eff_depth;
                res_eff_depth = adopt_eff_depth - (d2 / 2);
                double Ast2;
                Ast2 = (ML * 10E5) / (sigma_st * j * res_eff_depth);

                //S = (1000.0 * (Math.PI * 12.0 * 12.0 / 4.0)) / Ast2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Calculations for Reinforcement along longer span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Use 10 mm dia bars,");
                sw.WriteLine("Respective effective depth = {0} - {1} = {2} mm",
                    adopt_eff_depth,
                    (d2/2).ToString("0"),
                    res_eff_depth);

                sw.WriteLine();
                sw.WriteLine("Ast2 = (ML * 10^6)/(σ_st * j* d)");
                sw.WriteLine("     = ({0:f3} * 10^6)/({1:f2} * {2:f2}* {3:f2})",
                    ML, sigma_st, j, res_eff_depth);
                sw.WriteLine("     = {0:f2} sq.mm",Ast2);


                double spacing_bars;
                spacing_bars = (1000 * Math.PI * d2 * d2) / (4 * Ast2);

               

                sw.WriteLine();
                sw.WriteLine("spacing of Bars = S = (1000 * (π * 10 * 10/4))/ Ast2");
                sw.WriteLine("                    = {0:f3} mm", spacing_bars);
                sw.WriteLine("                    = {0:f3}/10 = {1:f3} = {1:f0} ", spacing_bars,
                    (spacing_bars/10));

                double _spacing_bars = (int)(spacing_bars / 10);
                _spacing_bars = _spacing_bars * 10;

                sw.WriteLine("                    = {0:f0} * 10 = {1:f0} mm", (_spacing_bars/10), _spacing_bars);



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
                sw.WriteLine("Adopt T10 bars @{0:f0} mm c/c {1,40}", _spacing_bars, "Marked as (2) in the Drawing");
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
                _B = (width_long_girders / 1000.0);
                _C = L;
                _D = (width_cross_girders / 1000.0);
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
                width_carrage_way = MyList.StringToDouble(txt_Deck_WidthCarrageWay.Text, 0.0);
                effe_span = MyList.StringToDouble(txt_Deck_effective_span_Tee_beam.Text, 0.0);
                concrete_grade = MyList.StringToDouble(txt_Deck_concrete_grade.Text, 0.0);
                steel_grade = MyList.StringToDouble(txt_Deck_steel_grade.Text, 0.0);
                sigma_cb = MyList.StringToDouble(txt_Deck_sigma_cb.Text, 0.0);
                sigma_st = MyList.StringToDouble(txt_Deck_sigma_st.Text, 0.0);
                L = MyList.StringToDouble(txt_Deck_L.Text, 0.0);
                no_main_girder = MyList.StringToDouble(txt_Deck_no_of_main_girders.Text, 0.0);
                B = MyList.StringToDouble(txt_Deck_B.Text, 0.0);
                width_cross_girders = MyList.StringToDouble(txt_Deck_width_of_cross_girders.Text, 0.0);
                width_long_girders = MyList.StringToDouble(txt_Deck_width_of_long_girders.Text, 0.0);
                m = MyList.StringToDouble(txt_Deck_m.Text, 0.0);
                j = MyList.StringToDouble(txt_Deck_j.Text, 0.0);
                Q = MyList.StringToDouble(txt_Deck_Q.Text, 0.0);

                minimum_cover = MyList.StringToDouble(txt_Deck_minimum_cover.Text, 0.0);

                load = MyList.StringToDouble(txt_Deck_imposed_load.Text, 0.0);
                width = MyList.StringToDouble(txt_Deck_width.Text, 0.0);
                length = MyList.StringToDouble(txt_Deck_length.Text, 0.0);
                impact_factor = MyList.StringToDouble(txt_Deck_impact_factor.Text, 0.0);
                continuity_factor = MyList.StringToDouble(txt_Deck_continuity_factor.Text, 0.0);
                mu = MyList.StringToDouble(txt_Deck_mu.Text, 0.0);

                Ds = MyList.StringToDouble(txt_Deck_Ds.Text, 0.0);
                gamma_c = MyList.StringToDouble(txt_Deck_gamma_c.Text, 0.0);
                Dwc = MyList.StringToDouble(txt_Deck_Dwc.Text, 0.0);
                gamma_wc = MyList.StringToDouble(txt_Deck_gamma_wc.Text, 0.0);

                self_weight_slab = (Ds / 1000) * gamma_c;
                self_weight_wearing_cource = (Dwc / 1000) * gamma_wc;
                total_weight = (self_weight_slab + self_weight_wearing_cource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }

        public void Read_User_Input(string file_name)
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(file_name));
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
                        case "WIDTH_CARRAGE_WAY":
                            width_carrage_way = mList.GetDouble(1);
                            txt_Deck_WidthCarrageWay.Text = width_carrage_way.ToString();
                            break;
                        case "EFFE_SPAN":
                            effe_span = mList.GetDouble(1);
                            txt_Deck_effective_span_Tee_beam.Text = effe_span.ToString();
                            break;
                        case "CONCRETE_GRADE":
                            concrete_grade = mList.GetDouble(1);
                            txt_Deck_concrete_grade.Text = concrete_grade.ToString();
                            break;
                        case "STEEL_GRADE":
                            steel_grade = mList.GetDouble(1);
                            txt_Deck_steel_grade.Text = steel_grade.ToString();
                            break;
                        case "SIGMA_CB":
                            sigma_cb = mList.GetDouble(1);
                            txt_Deck_sigma_cb.Text = sigma_cb.ToString();
                            break;
                        case "SIGMA_ST":
                            sigma_st = mList.GetDouble(1);
                            txt_Deck_sigma_st.Text = sigma_st.ToString();
                            break;
                        case "SPACING_CROSS_GIRDER":
                            L = mList.GetDouble(1);
                            txt_Deck_L.Text = L.ToString();
                            break;
                        case "NO_MAIN_GIRDER":
                            no_main_girder = mList.GetDouble(1);
                            txt_Deck_no_of_main_girders.Text = no_main_girder.ToString();
                            break;
                        case "SPACING_MAIN_GIRDER":
                            B = mList.GetDouble(1);
                            txt_Deck_B.Text = B.ToString();
                            break;
                        case "WIDTH_CROSS_GIRDERS":
                            width_cross_girders = mList.GetDouble(1);
                            txt_Deck_width_of_cross_girders.Text = width_cross_girders.ToString();
                            break;
                        case "WIDTH_LONG_GIRDERS":
                            width_long_girders = mList.GetDouble(1);
                            txt_Deck_width_of_long_girders.Text = width_long_girders.ToString();
                            break;
                        case "M":
                            m = mList.GetDouble(1);
                            txt_Deck_m.Text = m.ToString();
                            break;
                        case "J":
                            j = mList.GetDouble(1);
                            txt_Deck_j.Text = j.ToString();
                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            txt_Deck_Q.Text = Q.ToString();
                            break;
                        case "MINIMUM_COVER":
                            minimum_cover = mList.GetDouble(1);
                            txt_Deck_minimum_cover.Text = minimum_cover.ToString();
                            break;
                        case "LOAD":
                            load = mList.GetDouble(1);
                            txt_Deck_imposed_load.Text = load.ToString();
                            break;
                        case "WIDTH":
                            width = mList.GetDouble(1);
                            txt_Deck_width.Text = width.ToString();
                            break;
                        case "LENGTH":
                            length = mList.GetDouble(1);
                            txt_Deck_length.Text = length.ToString();
                            break;
                        case "IMPACT_FACTOR":
                            impact_factor = mList.GetDouble(1);
                            txt_Deck_impact_factor.Text = impact_factor.ToString();
                            break;
                        case "CONTINUITY_FACTOR":
                            continuity_factor = mList.GetDouble(1);
                            txt_Deck_continuity_factor.Text = continuity_factor.ToString();
                            break;
                        case "MU":
                            mu = mList.GetDouble(1);
                            txt_Deck_mu.Text = mu.ToString();
                            break;
                        case "DS":
                            Ds = mList.GetDouble(1);
                            txt_Deck_Ds.Text = Ds.ToString();
                            break;
                        case "GAMMA_C":
                            gamma_c = mList.GetDouble(1);
                            txt_Deck_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "DWC":
                            Dwc = mList.GetDouble(1);
                            txt_Deck_Dwc.Text = Dwc.ToString();
                            break;
                        case "GAMMA_WC":
                            gamma_wc = mList.GetDouble(1);
                            txt_Deck_gamma_wc.Text = gamma_wc.ToString();
                            break;
                        //case "SELF_WEIGHT_SLAB":
                        //    self_weight_slab = mList.GetDouble(1);
                        //    txt_self_weight_slab = mList.GetDouble(1);
                        //    break;
                        //case "SELF_WEIGHT_WEARING_COURCE":
                        //    self_weight_wearing_cource = mList.GetDouble(1);
                        //    break;
                        //case "TOTAL_WEIGHT":
                        //    total_weight = mList.GetDouble(1);
                        //    break;
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

                sw.WriteLine("WIDTH_CARRAGE_WAY = {0}", txt_Deck_WidthCarrageWay.Text);
                sw.WriteLine("EFFE_SPAN = {0}", txt_Deck_effective_span_Tee_beam.Text);
                sw.WriteLine("CONCRETE_GRADE = {0}", txt_Deck_concrete_grade.Text);
                sw.WriteLine("STEEL_GRADE = {0}", txt_Deck_steel_grade.Text);
                sw.WriteLine("SIGMA_CB = {0}", txt_Deck_sigma_cb.Text);
                sw.WriteLine("SIGMA_ST = {0}", txt_Deck_sigma_st.Text);
                sw.WriteLine("SPACING_CROSS_GIRDER = {0}", txt_Deck_L.Text);
                sw.WriteLine("NO_MAIN_GIRDER = {0}", txt_Deck_no_of_main_girders.Text);
                sw.WriteLine("SPACING_MAIN_GIRDER = {0}", txt_Deck_B.Text);
                sw.WriteLine("WIDTH_CROSS_GIRDERS = {0}", txt_Deck_width_of_cross_girders.Text);
                sw.WriteLine("WIDTH_LONG_GIRDERS = {0}", txt_Deck_width_of_long_girders.Text);
                sw.WriteLine("M = {0}", txt_Deck_m.Text);
                sw.WriteLine("J = {0}", txt_Deck_j.Text);
                sw.WriteLine("Q = {0}", txt_Deck_Q.Text);
                sw.WriteLine("MINIMUM_COVER = {0}", txt_Deck_minimum_cover.Text);
                sw.WriteLine("LOAD = {0}", txt_Deck_imposed_load.Text);
                sw.WriteLine("WIDTH = {0}", txt_Deck_width.Text);
                sw.WriteLine("LENGTH = {0}", txt_Deck_length.Text);
                sw.WriteLine("IMPACT_FACTOR = {0}", txt_Deck_impact_factor.Text);
                sw.WriteLine("CONTINUITY_FACTOR = {0}", txt_Deck_continuity_factor.Text);
                sw.WriteLine("MU = {0}", txt_Deck_mu.Text);
                sw.WriteLine("DS = {0}", txt_Deck_Ds.Text);
                sw.WriteLine("GAMMA_C = {0}", txt_Deck_gamma_c.Text);
                sw.WriteLine("DWC = {0}", txt_Deck_Dwc.Text);
                sw.WriteLine("GAMMA_WC = {0}", txt_Deck_gamma_wc.Text);

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void frmDesignDeckSlab_Load(object sender, EventArgs e)
        {
            if (TBeamDesign == 2)
            {
                user_input_file = Path.Combine(iApp.AppFolder,"DESIGN\\DefaultData\\DESIGN_OF_DECK_SLAB.FIL");
                Read_User_Input(user_input_file);
                user_input_file = "";
                pictureBox2.BackgroundImage = global::AstraFunctionOne.Properties.Resources.TBeam_Main_Girder_Bottom_Flange;

            }
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
            kPath = Path.Combine(kPath, "RCC T-Beam Bridge");

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
                this.Text = "DESIGN OF DECK SLAB : " + value;
                user_path = value;

                //file_path = Path.Combine(user_path, "DECK_SLAB");
                file_path = GetAstraDirectoryPath(user_path);
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);


                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_T_Beam_Deck_Slab.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_DECK_SLAB.FIL");


                btnReport.Enabled = File.Exists(rep_file_name);
                //btnDrawing.Enabled = File.Exists(rep_file_name);
                btnProcess.Enabled = Directory.Exists(value);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input(user_input_file);
                }

            }
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            if (TBeamDesign == 2)
            {
                iApp.SetDrawingFile_Path(drawing_path, "TBEAM_Deck_Slab", "TBEAM_DeckSlab_Plan,TBEAM_Worksheet_Design1");
            }
            else
                iApp.SetDrawingFile_Path(drawing_path, "TBEAM_Deck_Slab", "TBEAM_DeckSlab_Plan");
        }

        private void txt_concrete_grade_TextChanged(object sender, EventArgs e)
        {
            InitializeData();
            double fck, fcc, j, Q, fcb, n;

            fck = concrete_grade;

            fcb = fck / 3;
            fcc = fck / 4;

            n = m * fcb / (m * fcb + sigma_st);

            j = 1 - (n / 3.0);
            Q = n * j * fcb / 2;

            txt_Deck_sigma_cb.Text = fcb.ToString("0.00");
            txt_Deck_j.Text = j.ToString("0.000");
            txt_Deck_Q.Text = Q.ToString("0.000");

        }
    }
}
