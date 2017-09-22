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

namespace AstraFunctionOne.BridgeDesign.LongGirder
{
    public partial class frm_RccTBeam_Bottom_Flange : Form
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        string rep_file_name = "";
        string file_path = "";
        string user_input_file = "";
        string system_path = "";
        string user_path = "";
        string drawing_path = "";
        bool is_process = false;

        double Ds;
        double total_bars_L2, total_bars_L4, total_bars_Deff;
        double D;
        double Bb, Bs, Db, Dw, As, nl;

        double bw;
        double L;
        double Gs;
        double design_moment_mid;
        double design_moment_quarter;
        double design_moment_deff;
        double concrete_grade;
        double steel_grade;
        double modular_ratio;
        double bar_dia;
        double v1, v2, v3;
        double cover;
        double sigma_sv;
        double deff;
        double space_main, space_cross;
        double allow_stress_concrete;
        bool isInner;
        IApplication iApp = null;

        double bar_dia_L2, bar_dia_L4, bar_dia_Deff;


        double inner_deff_moment = 0.0;
        double outer_deff_moment = 0.0;

        double inner_L4_moment = 0.0;
        double outer_L4_moment = 0.0;

        double inner_L2_moment = 0.0;
        double outer_L2_moment = 0.0;

        double inner_deff_shear = 0.0;
        double outer_deff_shear = 0.0;

        double inner_L4_shear = 0.0;
        double outer_L4_shear = 0.0;

        double inner_L2_shear = 0.0;
        double outer_L2_shear = 0.0;



        #region Drawing Variable
        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7, _bd8, _bd9, _bd10;
        double _n1, _n2, _n3, _n4, _n5, _n6, _n7, _n8, _n9, _n10;
        double _sp7, _sp8, _sp9, _sp10;
        #endregion

        public frm_RccTBeam_Bottom_Flange(IApplication App)
        {
            InitializeComponent();
            this.iApp = App;

            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;
            _bd8 = 0.0;
            _bd9 = 0.0;
            _bd10 = 0.0;
            _n1 = 0.0;
            _n2 = 0.0;
            _n3 = 0.0;
            _n4 = 0.0;
            _n5 = 0.0;
            _n6 = 0.0;
            _n7 = 0.0;
            _n8 = 0.0;
            _n9 = 0.0;
            _n10 = 0.0;
            _sp7 = 0.0;
            _sp8 = 0.0;
            _sp9 = 0.0;
            _sp10 = 0.0;
        }
        public void InitializeData()
        {

            Ds = MyList.StringToDouble(txt_Ds.Text, 0.0);
            D = MyList.StringToDouble(txt_D.Text, 0.0);
            bw = MyList.StringToDouble(txt_bw.Text, 0.0);
            L = MyList.StringToDouble(txt_span_girders.Text, 0.0);
            Gs = MyList.StringToDouble(txt_Gs.Text, 0.0);

            design_moment_mid = MyList.StringToDouble(txt_design_moment_mid.Text, 0.0);
            design_moment_quarter = MyList.StringToDouble(txt_design_moment_quarter.Text, 0.0);
            design_moment_deff = MyList.StringToDouble(txt_design_moment_deff.Text, 0.0);

            v1 = MyList.StringToDouble(txt_design_shear_mid.Text, 0.0);
            v2 = MyList.StringToDouble(txt_design_shear_quarter.Text, 0.0);
            v3 = MyList.StringToDouble(txt_design_shear_deff.Text, 0.0);



            concrete_grade = MyList.StringToDouble(cmb_concrete_grade.Text, 0.0);
            allow_stress_concrete = MyList.StringToDouble(txt_allowable_stress_concrete_grade.Text, 0.0);
            
            steel_grade = MyList.StringToDouble(cmb_Steel_Grade.Text, 0.0);
            modular_ratio = MyList.StringToDouble(txt_modular_ratio.Text, 0.0);
            bar_dia = MyList.StringToDouble(txt_bar_dia_L2.Text, 0.0);//cm
            total_bars_L2 = MyList.StringToDouble(txt_total_bar_L2.Text, 0.0);

            bar_dia_L2 = MyList.StringToDouble(txt_bar_dia_L2.Text, 0.0);//cm
            total_bars_L2 = MyList.StringToDouble(txt_total_bar_L2.Text, 0.0);

            bar_dia_L4 = MyList.StringToDouble(txt_bar_dia_L4.Text, 0.0);//cm
            total_bars_L4 = MyList.StringToDouble(txt_total_bar_L4.Text, 0.0);

            bar_dia_Deff = MyList.StringToDouble(txt_bar_dia_Deff.Text, 0.0);//cm
            total_bars_Deff = MyList.StringToDouble(txt_total_bar_Deff.Text, 0.0);


            cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            sigma_sv = MyList.StringToDouble(txt_sigma_sv.Text, 0.0);

            isInner = rbtn_inner_girder.Checked;

            //space_main = MyList.StringToDouble(txt_space_main_girder.Text, 0.0);
            space_cross = MyList.StringToDouble(txt_space_cross_girder.Text, 0.0);


            //Bs = MyList.StringToDouble(txt_Bs.Text, 0.0);
            Bb = MyList.StringToDouble(txt_Bb.Text, 0.0);
            Db = MyList.StringToDouble(txt_Db.Text, 0.0);
            Db = MyList.StringToDouble(txt_Db.Text, 0.0);
            nl = MyList.StringToDouble(txt_nl.Text, 0.0);

            As = (Math.PI * (bar_dia) * (bar_dia) / 4.0) * total_bars_L2;
            Dw = D - Ds - Db;

            //d_s = 0.252;
            //D = 1.2;
            //bw = 0.3;
            //L = 13.6;
            //Gs = 3.0;
            //design_moment = 255.77;
            //concrete_grade = 25;
            //steel_grade = 415;
            //modular_ratio = 10;
            //bar_dia = 3.2;//cm
            //total_bars = 17;
        }
        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "LONG_GIRDERS_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));

            try
            {
                sw.WriteLine("_L1={0}", L);
                sw.WriteLine("_L2={0:f2}", (L / 2));
                sw.WriteLine("_L3={0:f2}", (L / 4));
                sw.WriteLine("_deff={0:f4}", (deff / 1000));
                sw.WriteLine("_D={0}", D / 1000);
                sw.WriteLine("_Ds={0}", Ds / 1000);
                sw.WriteLine("_Bw={0}", bw / 1000);

                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_bd3={0}", _bd3);
                sw.WriteLine("_bd4={0}", _bd4);
                sw.WriteLine("_bd5={0}", _bd5);
                sw.WriteLine("_bd6={0}", _bd6);
                sw.WriteLine("_bd7={0}", _bd7);
                sw.WriteLine("_bd8={0}", _bd8);
                sw.WriteLine("_bd9={0}", _bd9);
                sw.WriteLine("_bd10={0}", _bd10);

                sw.WriteLine("_sp7={0}", _sp7);
                sw.WriteLine("_sp8={0}", _sp8);
                sw.WriteLine("_sp9={0}", _sp9);
                sw.WriteLine("_sp10={0}", _sp10);


                sw.WriteLine("_n1={0}", _n1);
                sw.WriteLine("_n2={0}", _n2);
                sw.WriteLine("_n3={0}", _n3);
                sw.WriteLine("_n4={0}", _n4);
                sw.WriteLine("_n5={0}", _n5);
                sw.WriteLine("_n6={0}", _n6);
                sw.WriteLine("_n7={0}", _n7);
                sw.WriteLine("_n8={0}", _n8);
                sw.WriteLine("_n9={0}", _n9);
                sw.WriteLine("_n10={0}", _n10);


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

        void Read_Max_Moment_Shear_from_Analysis()
        {

            //string f_path = Environment.GetEnvironmentVariable("TBEAM_ANALYSIS");
            string f_path = Path.Combine(user_path, "FORCES.TXT");
            if (File.Exists(f_path ))
            {
                txt_design_shear_quarter.ForeColor = Color.Red;
                txt_design_shear_mid.ForeColor = Color.Red;
                txt_design_shear_deff.ForeColor = Color.Red;
                txt_design_moment_quarter.ForeColor = Color.Red;
                txt_design_moment_mid.ForeColor = Color.Red;
                txt_design_moment_deff.ForeColor = Color.Red;

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
                                txt_span_girders.Text = mlist.GetDouble(1).ToString();
                                break;
                            case "LONG_INN_DEFF_MOM":
                                inner_deff_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_DEFF_SHR":
                                inner_deff_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L2_MOM":
                                inner_L2_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L2_SHR":
                                inner_L2_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L4_MOM":
                                inner_L4_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_INN_L4_SHR":
                                inner_L4_shear = mlist.GetDouble(1);
                                break;

                            case "LONG_OUT_DEFF_MOM":
                                outer_deff_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_DEFF_SHR":
                                outer_deff_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L2_MOM":
                                outer_L2_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L2_SHR":
                                outer_L2_shear = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L4_MOM":
                                outer_L4_moment = mlist.GetDouble(1);
                                break;
                            case "LONG_OUT_L4_SHR":
                                outer_L4_shear = mlist.GetDouble(1);
                                break;
                        }
                    }
                }
                Load_Analysis_Data();
                //list.Add(string.Format("LONG_INN_DEFF_MOM={0}", txt_inner_long_deff_moment.Text));
                //list.Add(string.Format("LONG_INN_DEFF_SHR={0}", txt_inner_long_deff_shear.Text));
                //list.Add(string.Format("LONG_INN_L2_MOM={0}", txt_inner_long_L2_moment.Text));
                //list.Add(string.Format("LONG_INN_L2_SHR={0}", txt_inner_long_L2_shear.Text));
                //list.Add(string.Format("LONG_INN_L4_MOM={0}", txt_inner_long_L4_moment.Text));
                //list.Add(string.Format("LONG_INN_L4_SHR={0}", txt_inner_long_L4_shear.Text));

                //list.Add(string.Format("LONG_OUT_DEFF_MOM={0}", txt_outer_long_deff_moment.Text));
                //list.Add(string.Format("LONG_OUT_DEFF_SHR={0}", txt_outer_long_deff_shear.Text));
                //list.Add(string.Format("LONG_OUT_L2_MOM={0}", txt_outer_long_L2_moment.Text));
                //list.Add(string.Format("LONG_OUT_L2_SHR={0}", txt_outer_long_L2_shear.Text));
                //list.Add(string.Format("LONG_OUT_L4_MOM={0}", txt_outer_long_L4_moment.Text));
                //list.Add(string.Format("LONG_OUT_L4_SHR={0}", txt_outer_long_L4_shear.Text));

                //list.Add(string.Format("CROSS_MOM={0}", txt_inner_cross_max_moment.Text));
                //list.Add(string.Format("CROSS_SHR={0}", txt_inner_cross_max_shear.Text));
            }
        }
        //Chiranjit [2011 06 16]
        // Set All the values from TBeam Analysis Data
        public void Load_Analysis_Data()
        {
            //Read_Max_Moment_Shear_from_Analysis();
            if (rbtn_inner_girder.Checked)
            {
                if (inner_deff_moment != 0.0)
                {
                    txt_design_moment_deff.Text = inner_deff_moment.ToString();
                    txt_design_moment_deff.ForeColor = Color.Red;
                }
                if (inner_L2_moment != 0.0)
                {
                    txt_design_moment_mid.Text = inner_L2_moment.ToString();
                    txt_design_moment_mid.ForeColor = Color.Red;
                }
                if (inner_L4_moment != 0.0)
                {
                    txt_design_moment_quarter.Text = inner_L4_moment.ToString();
                    txt_design_moment_quarter.ForeColor = Color.Red;
                }
                

                if (inner_deff_shear != 0.0)
                {
                    txt_design_shear_deff.Text = inner_deff_shear.ToString();
                    txt_design_shear_deff.ForeColor = Color.Red;
                }
                if (inner_L2_shear != 0.0)
                {
                    txt_design_shear_mid.Text = inner_L2_shear.ToString();
                    txt_design_shear_mid.ForeColor = Color.Red;
                }
                if (inner_L4_shear != 0.0)
                {
                    txt_design_shear_quarter.Text = inner_L4_shear.ToString();
                    txt_design_shear_quarter.ForeColor = Color.Red;
                }
               
            }
            else
            {
                if (outer_deff_moment != 0.0)
                {
                    txt_design_moment_deff.Text = outer_deff_moment.ToString();
                }
                if (outer_L2_moment != 0.0)
                {
                    txt_design_moment_mid.Text = outer_L2_moment.ToString();
                }
                if (outer_L4_moment != 0.0)
                {
                    txt_design_moment_quarter.Text = outer_L4_moment.ToString();
                }


                if (outer_deff_shear != 0.0)
                {
                    txt_design_shear_deff.Text = outer_deff_shear.ToString();
                }
                if (outer_L2_shear != 0.0)
                {
                    txt_design_shear_mid.Text = outer_L2_shear.ToString();
                }
                if (outer_L4_shear != 0.0)
                {
                    txt_design_shear_quarter.Text = outer_L4_shear.ToString();
                }

            }
        }


        public void Calculate_Program(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));

            #region TechSOFT Banner
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 22             *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*       DESIGN OF LONGITUDINAL GIRDER         *");
            sw.WriteLine("\t\t*  FOR T-BEAM RCC BRIDGE WITH BOTTOM FLANGE   *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine();

            #endregion

            try
            {

                #region USER DATA
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Thickness of Deck Slab [Ds] = {0} mm                  Marked as (Ds) in the Drawing", Ds);
                sw.WriteLine("Depth of Longitudinal Girder [D]= {0} mm              Marked as (D) in the Drawing", D);
                sw.WriteLine("Web Thickness of Longitudinal Girder [bw] = {0} mm    Marked as (Bw) in the Drawing", bw);
                sw.WriteLine("Span of Girder [L] = {0} m                            Marked as (L) in the Drawing", L);
                sw.WriteLine("c/c distance of Longitudinal Girder [Gs] = {0} m", Gs);
                sw.WriteLine("Spacing of Cross Girders = {0} m", txt_space_cross_girder.Text);


                

                sw.WriteLine();
                //sw.WriteLine("Width of Top Flange [Bs] = {0} mm", Bs);
                sw.WriteLine("Width of Bottom Flange [Bb] = {0} mm", Bb);
                sw.WriteLine("Depth of Bottom Flange [Db] = {0} mm", Db);
                sw.WriteLine("Depth of Web           [Dw] = D - Ds - Db ");
                sw.WriteLine("                            = {0} - {1} - {2}", D, Ds, Db);
                sw.WriteLine("                            = {0} mm", Dw);
                sw.WriteLine("                            = {0} m", (Dw/1000.0));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At Mid Span (L/2) DESIGN_MOMENT [M1] = {0}  t-m", design_moment_mid);
                sw.WriteLine("At Quarter Span (L/4) DESIGN_MOMENT [M2] = {0}  t-m", design_moment_quarter);
                sw.WriteLine("At Effective Depth Distance (Deff) DESIGN_MOMENT [M3] = {0} t-m", design_moment_deff);

                sw.WriteLine();
                sw.WriteLine("At Mid Span (L/2) DESIGN_SHEAR [V1]= {0} t", v1);
                sw.WriteLine("At Quarter Span (L/4) DESIGN_SHEAR [V2] = {0} t", v2);
                sw.WriteLine("At Effective Depth Distance (Deff) DESIGN_SHEAR [V3] = {0} t", v3);


                sw.WriteLine();
                sw.WriteLine("CONCRETE GRADE = M {0} ", concrete_grade);
                sw.WriteLine("STEEL GRADE = Fe {0} ", steel_grade);
                sw.WriteLine();
                sw.WriteLine("MODULAR RATIO [m] = {0} ", modular_ratio);
                sw.WriteLine("BAR DIA [Do] = {0} cm", bar_dia);
                sw.WriteLine("TOTAL BARS [no] = {0} ", total_bars_L2);
                sw.WriteLine("TOTAL Layers of Bars [nl] = {0} ", nl);
                sw.WriteLine("Area [As] = {0:f0} sq.mm", As);
                sw.WriteLine();
                sw.WriteLine("COVER [cover] = {0:f3} mm", cover);
                sw.WriteLine("Permissible Stress in Steel [σ_sv] = {0} N/sq.mm.", sigma_sv);
                sw.WriteLine("Permissible Stress in Concrete [σ_c] = {0} kg/sq.cm.", allow_stress_concrete);
                sw.WriteLine();
                //sw.WriteLine("Spacing of main Girders = {0} m", txt_space_main_girder.Text);
                sw.WriteLine();
                sw.WriteLine();

                //cm
                #endregion

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF GIRDER AT MID SPAN (L/2)");
                sw.WriteLine("DESIGN MOMENT = {0:f2} t-m", design_moment_mid);
                sw.WriteLine("------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : OBTAINED THE EFFECTIVE WIDTH OF FLANGE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                _n1 = total_bars_L2;
                _bd1 = bar_dia * 10;

                _n2 = total_bars_L2 - 2;
                _bd2 = bar_dia * 10;

                _n3 = total_bars_L2 - 4;
                _bd3 = bar_dia * 10;

                _n4 = 2;
                _bd4 = bar_dia * 10;
                _n5 = 2;
                _bd5 = bar_dia * 10;
                _n6 = 3;
                _bd6 = 20;
                //_bd6 = bar_dia * 10;
                bar_dia = bar_dia_L2;

                Write_Moment_Program(sw, design_moment_mid, total_bars_L2, 1, 0);


                if (design_moment_quarter != 0)
                {
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("DESIGN OF GIRDER AT QUARTER SPAN (L/4)");
                    sw.WriteLine("DESIGN MOMENT = {0:f2} t-m", design_moment_quarter);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    bar_dia = bar_dia_L4;
                    Write_Moment_Program(sw, design_moment_quarter, total_bars_L4, 2, 4);
                }
                if (design_moment_deff != 0)
                {
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("DESIGN OF GIRDER AT EFFECTIVE DEPTH DISTANCE");
                    sw.WriteLine("DESIGN MOMENT = {0:f2} t-m", design_moment_deff);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    bar_dia = bar_dia_Deff ;
                    Write_Moment_Program(sw, design_moment_deff, total_bars_Deff, 3, 5);
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF GIRDER AT MID SPAN (L/2)");
                sw.WriteLine("DESIGN SHEAR = {0:f2} t-m", v1);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Shear_Program(sw, v1, ref _bd7, ref _sp7, ref _n7, 7);
                sw.WriteLine();

                if (v2 != 0)
                {
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("DESIGN OF GIRDER AT QUARTER SPAN (L/4)");
                    sw.WriteLine("DESIGN SHEAR = {0:f2} t-m", v2);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    Write_Shear_Program(sw, v2, ref _bd8, ref _sp8, ref _n8, 8);
                }
                if (v3 != 0)
                {
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("DESIGN OF GIRDER AT EFFECTIVE DEPTH DISTANCE");
                    sw.WriteLine("DESIGN SHEAR = {0:f2} t-m", v3);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    Write_Shear_Program(sw, v3, ref _bd9, ref _sp9, ref _n9, 9);
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : DESIGN OF SIDE REINFORCEMENTS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Providing 0.1% of area of each side face of the Long Girders");
                sw.WriteLine("with spacing not more than 450 mm, ");

                double req_st = 0.001 * 1000 * D;
                sw.WriteLine();
                sw.WriteLine("Required Steel = 0.001 * b * D");
                sw.WriteLine("               = 0.001 * 1000 * {0:f2}", (D));
                sw.WriteLine("               = {0:f2} sq.mm", req_st);

                double ast = Math.PI * 16 * 16 / 4.0;

                double no_bar = (int)(req_st / ast);
                no_bar += 1;

                ast = ast * no_bar;

                sw.WriteLine("Provide 16 mm dia bars, {0:f0} nos, Ast = {1:f2} sq.mm.       Marked as (10) in the Drawing", no_bar, ast);
                sw.WriteLine();

                double spacing = (D - Ds - 300.0) / no_bar;
                sw.WriteLine("Spacing = (D - Ds - 300)/ {0:f0}", no_bar);
                sw.WriteLine("        = ({0:f2} - {1:f2} - 300) / {2:f0}", D, Ds, no_bar);
                sw.WriteLine("        = {0:f2} mm ", spacing);

                spacing = (int)(spacing / 10.0);
                spacing *= 10;
                sw.WriteLine("        = {0:f2} mm ", spacing);

                _bd10 = 16;
                _sp10 = spacing;
                _n10 = no_bar;


                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : DESIGN OF TOP REINFORCEMENTS");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("0.25% of girder Sectional Area.");

                req_st = (0.25 / 100.0) * bw * deff * 10.0;
                sw.WriteLine("=(0.25/100) * {0:f2} * {1:f2}",
                    bw, deff * 10);
                sw.WriteLine("={0:f2} sq.mm.", req_st);


                ast = Math.PI * 20 * 20 / 4.0;

                no_bar = (int)(req_st / ast);
                no_bar += 1;

                ast = ast * no_bar;

                _bd7 = 10;
                _n6 = no_bar;
                sw.WriteLine();
                sw.WriteLine("Let us provide {0:f0} T20 bars at Top.      Marked as (6) in the Drawing", no_bar);

                sw.WriteLine();
                sw.WriteLine("Provided Steel = {0:f2} sq.mm ", ast);
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
        //list.Add(string.Format("Thickness of Deck Slab = {0} mm", thickness_slab));

        public void Write_Moment_Program(StreamWriter sw, double moment_value, double bar_no, int mark, int bent_mark)
        {
            double M = moment_value;

            sw.WriteLine();
            sw.WriteLine();

            double thickness_slab = Ds;
            sw.WriteLine("Thickness of Deck Slab = {0} mm", thickness_slab);
            sw.WriteLine();
            sw.WriteLine("Effective width of top compression flage");
            sw.WriteLine("(  Referring to clause no 305.12.2 of IRC : 21-1987 , ");
            //sw.WriteLine();
            sw.WriteLine(" it should be the least of the following)");
            sw.WriteLine();

            double one_fourth_L;
            one_fourth_L = L / 4;
            sw.WriteLine("(i)   1/4 th of Effective span of the beam = L/4 = {0}/4={1} m",
                L,
                one_fourth_L.ToString("0.00"));
            //if (!isInner)
            //{
            //    Gs = Gs / 2.0;
            //}
            sw.WriteLine("(ii)  The c/c distance of webs of beam = {0:f2} m", Gs);

            double bf;
            bf = 12 * Ds + bw;

            sw.WriteLine("(iii) 12 * ds + bw = 12 * {0} + {1} = {2} mm.", Ds, bw, bf);

            bf = (int)bf;
            if (!isInner)
            {
                bf = bf / 2.0;
            }
            sw.WriteLine();
            Bs = bf;
            sw.WriteLine("Therefore, The Width of Top Flange = Bs = {0:f2} mm", bf);


            double d1, d2;
            d1 = bar_dia;

            sw.WriteLine();

            //double mark, bent_mark;
            //mark = 2;
            //bent_mark = 4;

            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 2 : MAIN REINFORCEMENTS (BOTTOM) AND EFFECTIVE DEPTH");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Let us provide {0:f0} nos T{1:f0} bars             Marked as ({2}) in the Drawing", bar_no, d1, mark);
            if (bent_mark != 0)
                sw.WriteLine("                         Two Bars are bent up, Marked as ({0}) in the Drawing", bent_mark);



            double Ast = (Math.PI * bar_dia * bar_dia * bar_no) / 4;
            sw.WriteLine();
            Ast = double.Parse(Ast.ToString("0"));
            sw.WriteLine("Ast = {0} sq.mm", Ast);
            Ast = Ast / 100;
            sw.WriteLine();
            sw.WriteLine("    = {0} sq.cm", Ast);

            double eff_depth;
            //eff_depth = (D * 100) - (cover / 10) - (bar_dia / 2) - (nl * bar_dia);
            //Chiranjit [2011 06 06]
            eff_depth = (D) - (cover) - (bar_dia / 2) - (nl * bar_dia);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Effective Depth = {0:f2} - {1:f0} - {2:f1} - {3:f0} * {4:f1} = {5:f2} mm      Marked as (Deff) in the Drawing", D, (cover), (bar_dia / 2.0), nl, bar_dia, eff_depth);


            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 3 : DEPTH OF NEUTRAL AXIS");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            #region Draw Figure
            sw.WriteLine("        |--------------Bs-------------|");
            sw.WriteLine("      _  _____________________________   _");
            sw.WriteLine("      | |                      |      |  |");
            sw.WriteLine("      | |______________________|______| _| Ds  _");
            sw.WriteLine("      |             |    |     |               |");
            sw.WriteLine("      |             |    |     |n              |");
            sw.WriteLine("      |  -----------|----|---------- NA        |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      D             |-Bw-|                     Dw");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |          ___|    |___  _              _|");
            sw.WriteLine("      |         |            |  |");
            sw.WriteLine("      |         |            |  | Db");
            sw.WriteLine("      |_        |____________| _|");
            sw.WriteLine("");
            sw.WriteLine("                |-----Bb-----|");
            sw.WriteLine();
            sw.WriteLine();
            #endregion Draw Figure

            sw.WriteLine("Let 'n' be the depth of Neutral Axis from the top of Deck slab");


            sw.WriteLine();
            double a1;
            a1 = (Ds * (Bs - bw));
            //d1 = 
            
            sw.WriteLine("a1 = Ds * (Bs - bw)");
            sw.WriteLine("   = {0} * ({1} - {2})", Ds/10.0, Bs/10.0, bw/10.0);
            a1 = a1 / 100.0;
            sw.WriteLine("   = {0} sq.cm", a1.ToString("0.00"));
            sw.WriteLine();
            double _d1 = Ds / 20;
            sw.WriteLine("d1 = n - Ds/2 = n - {0}/2 = n - {1}", Ds/10.0, _d1);



            double a2 = 1;
            //a2 = bw
            sw.WriteLine();
            sw.WriteLine("a2 = Bw * n = {0} * n", bw/10.0);
            sw.WriteLine("d2 = n/2");




            double a3 = 1;
            //a3 = (Bb * Db)/100.0;
            sw.WriteLine();
            //Chiranjit [2011 07 08]
            //For Checking
            //sw.WriteLine("a3 =  Bb * Db;");
            //sw.WriteLine("   = {0} * {1}", Bb/10.0, Db/10.0);
            //sw.WriteLine("   = {0} sq.cm", a3);
            sw.WriteLine();
            double _d3 = 1;
            //double _d3 = (D - (Db / 2)) / 10.0;
            //sw.WriteLine("d3 = D-n-Db/2 = {0} - n - {1}/2 = {2:f2} - n", D/10.0, Db/10.0, _d3);

            double a4 = 1;
            a4 = modular_ratio * (Ast);

            sw.WriteLine();
            sw.WriteLine("a4 = m * Ast");
            sw.WriteLine("   = {0} * {1}", modular_ratio, Ast);
            sw.WriteLine("   = {0} sq.cm", a4);
            sw.WriteLine();
            sw.WriteLine("d4 = deff - n = {0:f2} - n", eff_depth/10.0);

          

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Now from equation");
            sw.WriteLine();
            //Chiranjit [2011 07 08]
            //sw.WriteLine("a1 * d1 + a2 * d2   =   a3 * d3 + a4 * d4");
            sw.WriteLine("a1 * d1 + a2 * d2   =   a4 * d4");
            sw.WriteLine();
            //Chiranjit [2011 07 08]
            //sw.WriteLine("{0:f3} * (n - {1:f3}) + ({2:f3} * n) * (n/2)   =  {3:f3} * ({4:f3} - n) + {5:f3} * ({6:f3} - n)",
            //    a1, _d1, bw / 10.0, a3, _d3, a4, eff_depth / 10.0);
            sw.WriteLine("{0} * (n - {1}) + ({2} * n) * (n/2)   =  {3} * ({4} - n)",
                a1, _d1, bw / 10.0, a4, eff_depth / 10.0);
            sw.WriteLine();
            sw.WriteLine();

            double _v1, _v2, _v3, _v4, _v5, _v6, _v7;

            _v1 = a1;
            _v2 = (Ds / 20.0) * a1;
            _v3 = (bw / 20.0);
            //Chiranjit [2011 07 08]
            //_v4 = a3 * _d3;
            //_v5 = a3;
            _v6 = a4 * eff_depth / 10.0;
            _v7 = a4;

            //Chiranjit [2011 07 08]
            //sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n + {5} - {6} * n",
            //    _v1, _v2, _v3, _v4, _v5, _v6, _v7);
            sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n",
                _v1, _v2, _v3, _v6, _v7);
            sw.WriteLine();


            //Chiranjit [2011 07 08]
            //sw.WriteLine("{0} * n*n + {1}*n + {2}*n + {3}*n = {4} + {5} + {6}",
            //    _v3, _v1, _v5, _v7, _v2, _v4, _v6);
            sw.WriteLine("{0} * n*n + {1}*n + {2}*n = {3} + {4}",
                _v3, _v1, _v7, _v2, _v6);

            double _a, _b, _c;
            //Chiranjit [2011 07 08]
            //_b = ((a1 + a5) / a3);
            //_c = ((a1 * a2 + a4) / a3);

            _a = _v3;
            //Chiranjit [2011 07 08]
            //_b = (_v1 + _v5 + _v7);
            //_c = (_v2 + _v4 + _v6);

            _b = (_v1  + _v7);
            _c = (_v2  + _v6);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("    {0} * n*n + {1}*n  = {2} ", _a, _b, _c);
            sw.WriteLine();
            _b = _b / _a;
            _c = (_c / _a);
            sw.WriteLine("or,   n*n + {0:f0}*n  - {1:f0} = 0", _b, _c);
            sw.WriteLine();
            
            sw.WriteLine();

            double n;

            double root_a;
            root_a = Math.Sqrt((_b * _b + 4 * _c));

            n = (root_a - _b) / 2;

            n /= 100.0;
            Bs /= 1000.0;
            bw /= 1000.0;
            Ds /= 1000.0;
            D /= 1000.0;
            Db /= 1000.0;
            eff_depth /= 1000.0;
            Bb /= 1000.0;

            n = MyList.StringToDouble(n.ToString("0.00000"), 0.0);
            sw.WriteLine();
            sw.WriteLine("n = {0:f4} m  = Depth of Neutral Axis from Top Edge", n);


            //Chiranjit [2011 07 08]
            //double ina = ((Bs * n * n * n) / 3.0) -
            //    (((Bs - bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
            //    (Bb * (D - n) * (D - n) * (D - n) / 3.0) -
            //    (((Bb - bw) * (D - n - Db) * (D - n - Db) * (D - n - Db)) / 3.0) +
            //    (modular_ratio * Ast / 10E4 * (eff_depth - n) * (eff_depth - n));
            double ina = ((Bs * n * n * n) / 3.0) -
                         (((Bs - bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
                         (modular_ratio * Ast / 10E3 * (eff_depth - n) * (eff_depth - n));

            sw.WriteLine();


            if (n > Ds)
            {
                sw.WriteLine("Moment of Inertia about Neutral Axis [Neutral Axis is lying outside the Top Flange of the Girder]");
                sw.WriteLine();
                sw.WriteLine();
                //Chiranjit [2011 07 08]
                //sw.WriteLine("  Ina = ((Bs * n^3) / 3.0) - (((Bs - bw) * (n - Ds)^3) / 3.0) +  ");
                //sw.WriteLine("        (Bb * (D - n)^3 / 3.0) - ");
                //sw.WriteLine("        (((Bb - Bw) * (D - n - Db)^3) / 3.0) +  ");
                //sw.WriteLine("        (m * Ast / 10E5 * (eff_depth - n)^2)");
                //sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("  Ina = ((Bs * n^3) / 3.0) - (((Bs - bw) * (n - Ds)^3) / 3.0) +  ");
                sw.WriteLine("        (m * Ast / 10^4 * (eff_depth - n)^2)");
                sw.WriteLine();
                //Chiranjit [2011 07 08]
                //sw.WriteLine("  Ina = (({0} * {1}^3) / 3.0) - ((({0} - {2}) * ({1} - {3})^3) / 3.0) +  ",
                //    Bs, n, bw, Ds);
                //sw.WriteLine("        ({0} * ({1} - {2})^3 / 3.0) - ",
                //    Bb, D, n);
                //sw.WriteLine("        ((({0} - {1}) * ({2} - {3} - {4})^3) / 3.0) +  ",
                //    Bb, bw, D, n, Db);
                //sw.WriteLine("        ({0} * {1} / 10E5 * ({2} - {3})^2)",
                //    mark, Ast, eff_depth, n);
                sw.WriteLine("  Ina = (({0} * {1}^3) / 3.0) - ((({0} - {2}) * ({1} - {3})^3) / 3.0) +  ",
                    Bs, n, bw, Ds);
                sw.WriteLine("        ({0} * {1} / 10^4 * ({2} - {3})^2)",
                    modular_ratio, Ast, eff_depth, n);
            }
            else
            {
                sw.WriteLine("Moment of Inertia about Neutral Axis [Neutral Axis lies within the Top Flange of the Girder]");
                sw.WriteLine();

                //Chiranjit [2011 07 08]
                //ina = (Bs * n * n * n / 3.0) +
                //    (Bs * (Ds - n) * (Ds - n) * (Ds - n) / 3.0) +
                //    (bw * Dw * ((Dw / 2) + Ds - n) * ((Dw / 2) + Ds - n)) +
                //    (Bb * Db * (D - n - (Db / 2)) * (D - n - (Db / 2)));

                ina = (Bs * n * n * n / 3.0) +
                    (Bs * (Ds - n) * (Ds - n) * (Ds - n) / 3.0) +
                    (bw * Dw * ((Dw / 2) + Ds - n) * ((Dw / 2) + Ds - n)) ;

                //Chiranjit [2011 07 08]
                //sw.WriteLine("       Ina = (Bs * n^3 / 3.0) + ");
                //sw.WriteLine("             (Bs * (Ds - n)^3 / 3.0) + ");
                //sw.WriteLine("             (bw * Dw * ((Dw / 2) + Ds - n)^2 + ");
                //sw.WriteLine("             (Bb * Db * (D - n - (Db / 2))^2)");
                //sw.WriteLine("");
                sw.WriteLine("       Ina = (Bs * n^3 / 3.0) + ");
                sw.WriteLine("             (Bs * (Ds - n)^3 / 3.0) + ");
                sw.WriteLine("             (bw * Dw * ((Dw / 2) + Ds - n)^2");
                sw.WriteLine("");

                //Chiranjit [2011 07 08]
                //sw.WriteLine("       Ina = ({0} * {1}^3 / 3.0) + ", Bs, n);
                //sw.WriteLine("             ({0} * ({1} - {2})^3 / 3.0) + ", Bs, Ds, n);
                //sw.WriteLine("             ({0} * {1} * (({1} / 2) + {2} - {3})^2 + ", bw, Dw, Ds, n);
                //sw.WriteLine("             ({0} * {1} * ({2} - {3} - ({1} / 2))^2)", Bb, Db, D, n);
                //sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("       Ina = ({0} * {1}^3 / 3.0) + ", Bs, n);
                sw.WriteLine("             ({0} * ({1} - {2})^3 / 3.0) + ", Bs, Ds, n);
                sw.WriteLine("             ({0} * {1} * (({1} / 2) + {2} - {3})^2", bw, Dw, Ds, n);
                sw.WriteLine();
                sw.WriteLine();
            }
            sw.WriteLine();
            
           
            
         
            sw.WriteLine();
            sw.WriteLine("   = {0:f6} sq.sq.m.", ina);

          
            double Zt;
            Zt = (ina / (n));
            sw.WriteLine();
            sw.WriteLine("Zt = Ina / n = {0:f6}/{1:f4} = {2:f4} cu.m",ina, n, Zt);


            double Zb;
            Zb = (ina / ((eff_depth - n)));
            sw.WriteLine("Zb = Ina / (deff - n) = {0:f6} / ({1} - {2:f4}) = {3:f4} cu.m",ina, eff_depth, n, Zb);


            n *= 100.0;
            Bs *= 1000.0;
            bw *= 1000.0;
            Bb *= 1000.0;
            Ds *= 1000.0;
            D *= 1000.0;
            Db *= 1000.0;
            eff_depth *= 1000.0;


            double fc;
            fc = (M * 10E4) / (Zt * 10E5);
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 4 : CHECK FOR STRESSES");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Stress in concrete = fc = (M * 10E4)/(Zt * E5)");
            sw.WriteLine("                   = ({0} * 10E4)/({1:f4} * 10^6)",
                M.ToString("0.000"), Zt.ToString("0.000"));


            sw.WriteLine();
            sw.WriteLine();
             if (fc < allow_stress_concrete)
            {
                sw.WriteLine("                   = {0:e3} kg/sq.cm. < (σ_c) {1}  kg/sq.cm,   OK",
                    fc, allow_stress_concrete);
            }
            else
            {
                sw.WriteLine("                   = {0:e3} kg/sq.cm. > (σ_c) {1} kg/sq.cm,  NOT OK",
                    fc, allow_stress_concrete);
 
            }

            double fs;
            fs = (M * 10E4) / (Zb * 10E5);
            fs = fs * 10;
            sw.WriteLine();
            sw.WriteLine("Allowable Stress in Fe {0} grade of steel = {1} kg/sq.cm", steel_grade, sigma_sv*10);
            sw.WriteLine();
            sw.WriteLine("Stress in Steel = fs = (M * 10E4)/(Zb*10^6)");
            sw.WriteLine("                     = ({0} * 10E4)/({1:f4}*10^6)", M, Zb);

            sw.WriteLine();
            double chk_val = sigma_sv * 10;
            if (fs < chk_val)
            {
                sw.WriteLine("                     = {0:e4} kg/sq.cm < {1} kg/sq.cm,  OK", fs, chk_val);
            }
            else
            {
                sw.WriteLine("                     = {0:e4} kg/sq.cm > {1} kg/sq.cm,  NOT OK", fs, chk_val);
            }

            double lever_arm = (M * 10E4) / (fs * Ast);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Therefore, Lever Arm = (M*10E4)/(fs*Ast)");
            sw.WriteLine("                     = ({0} * 10E4)/({1:e4} * {2})",
                M,
                fs,
                Ast);
            sw.WriteLine("                     = {0} cm", lever_arm.ToString("0.000"));
            lever_arm = lever_arm / 100;
            sw.WriteLine("                     = {0} m", lever_arm.ToString("0.000"));

            sw.WriteLine();
            sw.WriteLine();
        }
        public void Write_Moment_Program1(StreamWriter sw, double moment_value, double bar_no, int mark, int bent_mark)
        {
            double M = moment_value;

            sw.WriteLine();
            sw.WriteLine();

            double thickness_slab = Ds;
            sw.WriteLine("Thickness of Deck Slab = {0} mm", thickness_slab);
            sw.WriteLine();
            sw.WriteLine("Effective width of top compression flage");
            sw.WriteLine("(  Referring to clause no 305.12.2 of IRC : 21-1987 , ");
            //sw.WriteLine();
            sw.WriteLine(" it should be the least of the following)");
            sw.WriteLine();

            double one_fourth_L;
            one_fourth_L = L / 4;
            sw.WriteLine("(i)   1/4 th of Effective span of the beam = L/4 = {0}/4={1} m",
                L,
                one_fourth_L.ToString("0.00"));
            //if (!isInner)
            //{
            //    Gs = Gs / 2.0;
            //}
            sw.WriteLine("(ii)  The c/c distance of webs of beam = {0:f2} m", Gs);

            double bf;
            bf = 12 * Ds + bw;

            sw.WriteLine("(iii) 12 * ds + bw = 12 * {0} + {1} = {2} mm.", Ds, bw, bf);

            bf = (int)bf;
            if (!isInner)
            {
                bf = bf / 2.0;
            }
            sw.WriteLine();
            Bs = bf;
            sw.WriteLine("Therefore, The Width of Top Flange = Bs = {0:f2} mm", bf);


            double d1, d2;
            d1 = bar_dia * 10;

            sw.WriteLine();

            //double mark, bent_mark;
            //mark = 2;
            //bent_mark = 4;

            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 2 : MAIN REINFORCEMENTS (BOTTOM) AND EFFECTIVE DEPTH");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Let us provide {0:f0} nos T{1:f0} bars             Marked as ({2}) in the Drawing", bar_no, d1, mark);
            if (bent_mark != 0)
                sw.WriteLine("                         Two Bars are bent up, Marked as ({0}) in the Drawing", bent_mark);



            double Ast = (Math.PI * bar_dia * bar_dia * bar_no) / 4;
            sw.WriteLine();
            Ast = double.Parse(Ast.ToString("0"));
            sw.WriteLine("Ast = {0} sq.cm", Ast);

            double eff_depth;
            //eff_depth = (D * 100) - (cover / 10) - (bar_dia / 2) - (nl * bar_dia);
            //Chiranjit [2011 06 06]
            eff_depth = (D) - (cover) - (bar_dia * 10 / 2) - (nl * bar_dia * 10);
            sw.WriteLine();
            sw.WriteLine("Effective Depth = {0:f2} - {1:f0} - {2:f1} - {3:f0} * {4:f1} = {5:f2} mm      Marked as (Deff) in the Drawing", D, (cover / 10.0), (bar_dia * 10 / 2.0), nl, bar_dia * 10, eff_depth);


            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 3 : DEPTH OF NEUTRAL AXIS");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            #region Draw Figure
            sw.WriteLine("        |--------------Bs-------------|");
            sw.WriteLine("      _  _____________________________   _");
            sw.WriteLine("      | |                      |      |  |");
            sw.WriteLine("      | |______________________|______| _| Ds  _");
            sw.WriteLine("      |             |    |     |               |");
            sw.WriteLine("      |             |    |     |n              |");
            sw.WriteLine("      |  -----------|----|---------- NA        |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      D             |-Bw-|                     Dw");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |             |    |                     |");
            sw.WriteLine("      |          ___|    |___  _              _|");
            sw.WriteLine("      |         |            |  |");
            sw.WriteLine("      |         |            |  | Db");
            sw.WriteLine("      |_        |____________| _|");
            sw.WriteLine("");
            sw.WriteLine("                |-----Bb-----|");
            sw.WriteLine();
            sw.WriteLine();
            #endregion Draw Figure

            sw.WriteLine("Let 'n' be the depth of Neutral Axis from the top of Deck slab");


            sw.WriteLine();
            double a1;
            a1 = (Ds * (Bs - bw));
            //d1 = 

            sw.WriteLine("a1 = Ds * (Bs - bw)");
            sw.WriteLine("   = {0} * ({1} - {2})", Ds / 10.0, Bs / 10.0, bw / 10.0);
            a1 = a1 / 100.0;
            sw.WriteLine("   = {0} sq.cm", a1.ToString("0.00"));
            sw.WriteLine();
            double _d1 = Ds / 20;
            sw.WriteLine("d1 = n - Ds/2 = n - {0}/2 = n - {1}", Ds / 10.0, _d1);



            double a2 = 1;
            //a2 = bw
            sw.WriteLine();
            sw.WriteLine("a2 = Bw * n = {0} * n", bw / 10.0);
            sw.WriteLine("d2 = n/2");




            double a3 = 1;
            a3 = (Bb * Db) / 100.0;
            sw.WriteLine();
            sw.WriteLine("a3 =  Bb * Db;");
            sw.WriteLine("   = {0} * {1}", Bb / 10.0, Db / 10.0);
            sw.WriteLine("   = {0} sq.cm", a3);
            sw.WriteLine();
            double _d3 = (D - (Db / 2)) / 10.0;
            sw.WriteLine("d3 = D-n-Db/2 = {0} - n - {1}/2 = {2:f2} - n", D / 10.0, Db / 10.0, _d3);

            double a4 = 1;
            a4 = modular_ratio * (Ast);

            sw.WriteLine();
            sw.WriteLine("a4 = m * Ast");
            sw.WriteLine("   = {0} * {1}", modular_ratio, Ast);
            sw.WriteLine("   = {0} sq.cm", a4);
            sw.WriteLine();
            sw.WriteLine("d4 = deff - n = {0:f2} - n", eff_depth / 10.0);

            //double a5 = 1;
            //a5 = modular_ratio * Ast;
            //sw.WriteLine();
            //sw.WriteLine("a5 = m * Ast");
            //sw.WriteLine("   = {0} * {1:f0}", modular_ratio, Ast);
            //sw.WriteLine("   = {0:E2} ", a5);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Now from equation");
            sw.WriteLine();
            sw.WriteLine("a1 * d1 + a2 * d2   =   a3 * d3 + a4 * d4");
            sw.WriteLine();
            //sw.WriteLine("{0:f3} * (n - {1:f3}) + ({2:f3} * n) * (n/2)   =  {3:f3} * ({4:f3} - n) + {5:f3} * ({6:f3} - n)",
            //    a1, _d1, bw / 10.0, a3, _d3, a4, eff_depth / 10.0);
            sw.WriteLine("{0} * (n - {1}) + ({2} * n) * (n/2)   =  {3} * ({4} - n) + {5} * ({6} - n)",
                a1, _d1, bw / 10.0, a3, _d3, a4, eff_depth / 10.0);
            sw.WriteLine();
            sw.WriteLine();

            double _v1, _v2, _v3, _v4, _v5, _v6, _v7;

            _v1 = a1;
            _v2 = (Ds / 20.0) * a1;
            _v3 = (bw / 20.0);
            _v4 = a3 * _d3;
            _v5 = a3;
            _v6 = a4 * eff_depth / 10.0;
            _v7 = a4;

            sw.WriteLine("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n + {5} - {6} * n",
                _v1, _v2, _v3, _v4, _v5, _v6, _v7);
            sw.WriteLine();

            //sw.WriteLine("{0:f3} * n - {1:f3} + {2:f3} * n * n    =  {3:f3} - {4:f3} * n + {5:f3} - {6:f3}",
            //    _v1, _v2, _v3, _v4, _v5, _v6, _v7);
            //sw.WriteLine();

            //sw.WriteLine("{0:f3} * n*n + {1:f3}*n + {2:f3}*n + {3:f3}*n = {4:f3} + {5:f3} + {6:f3}",
            //    _v3, _v1, _v5, _v7, _v2, _v4, _v6);

            sw.WriteLine("{0} * n*n + {1}*n + {2}*n + {3}*n = {4} + {5} + {6}",
                _v3, _v1, _v5, _v7, _v2, _v4, _v6);

            double _a, _b, _c;
            //_b = ((a1 + a5) / a3);
            //_c = ((a1 * a2 + a4) / a3);

            _a = _v3;
            _b = (_v1 + _v5 + _v7);
            _c = (_v2 + _v4 + _v6);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("    {0} * n*n + {1}*n  = {2} ", _a, _b, _c);
            sw.WriteLine();
            _b = _b / _a;
            _c = (_c / _a);
            sw.WriteLine("or,   n*n + {0:f0}*n  - {1:f0} = 0", _b, _c);
            sw.WriteLine();

            sw.WriteLine();

            double n;

            double root_a;
            root_a = Math.Sqrt((_b * _b + 4 * _c));

            n = (root_a - _b) / 2;

            n /= 100.0;
            Bs /= 1000.0;
            bw /= 1000.0;
            Ds /= 1000.0;
            D /= 1000.0;
            Db /= 1000.0;
            eff_depth /= 1000.0;
            Bb /= 1000.0;

            n = MyList.StringToDouble(n.ToString("0.00000"), 0.0);
            sw.WriteLine();
            sw.WriteLine("n = {0:f4} m  = Depth of Neutral Axis from Top Edge", n);


            double ina = ((Bs * n * n * n) / 3.0) -
                (((Bs - bw) * (n - Ds) * (n - Ds) * (n - Ds)) / 3.0) +
                (Bb * (D - n) * (D - n) * (D - n) / 3.0) -
                (((Bb - bw) * (D - n - Db) * (D - n - Db) * (D - n - Db)) / 3.0) +
                (modular_ratio * Ast / 10E4 * (eff_depth - n) * (eff_depth - n));

            sw.WriteLine();


            if (n > Ds)
            {
                sw.WriteLine("Moment of Inertia about Neutral Axis [Neutral Axis is lying outside the Top Flange of the Girder]");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("  Ina = ((Bs * n^3) / 3.0) - (((Bs - bw) * (n - Ds)^3) / 3.0) +  ");
                sw.WriteLine("        (Bb * (D - n)^3 / 3.0) - ");
                sw.WriteLine("        (((Bb - Bw) * (D - n - Db)^3) / 3.0) +  ");
                sw.WriteLine("        (m * Ast / 10^6 * (eff_depth - n)^2)");
                sw.WriteLine();
                sw.WriteLine("  Ina = (({0} * {1}^3) / 3.0) - ((({0} - {2}) * ({1} - {3})^3) / 3.0) +  ",
                    Bs, n, bw, Ds);
                sw.WriteLine("        ({0} * ({1} - {2})^3 / 3.0) - ",
                    Bb, D, n);
                sw.WriteLine("        ((({0} - {1}) * ({2} - {3} - {4})^3) / 3.0) +  ",
                    Bb, bw, D, n, Db);
                sw.WriteLine("        ({0} * {1} / 10^6 * ({2} - {3})^2)",
                    mark, Ast, eff_depth, n);
            }
            else
            {
                sw.WriteLine("Moment of Inertia about Neutral Axis [Neutral Axis lies within the Top Flange of the Girder]");
                sw.WriteLine();

                ina = (Bs * n * n * n / 3.0) +
                    (Bs * (Ds - n) * (Ds - n) * (Ds - n) / 3.0) +
                    (bw * Dw * ((Dw / 2) + Ds - n) * ((Dw / 2) + Ds - n)) +
                    (Bb * Db * (D - n - (Db / 2)) * (D - n - (Db / 2)));

                sw.WriteLine("       Ina = (Bs * n^3 / 3.0) + ");
                sw.WriteLine("             (Bs * (Ds - n)^3 / 3.0) + ");
                sw.WriteLine("             (bw * Dw * ((Dw / 2) + Ds - n)^2 + ");
                sw.WriteLine("             (Bb * Db * (D - n - (Db / 2))^2)");
                sw.WriteLine("");

                sw.WriteLine("       Ina = ({0} * {1}^3 / 3.0) + ", Bs, n);
                sw.WriteLine("             ({0} * ({1} - {2})^3 / 3.0) + ", Bs, Ds, n);
                sw.WriteLine("             ({0} * {1} * (({1} / 2) + {2} - {3})^2 + ", bw, Dw, Ds, n);
                sw.WriteLine("             ({0} * {1} * ({2} - {3} - ({1} / 2))^2)", Bb, Db, D, n);
                sw.WriteLine();
                sw.WriteLine();
            }
            sw.WriteLine();




            //sw.WriteLine("  Ina = ((Bs * n^3) / 3.0) - (((Bs - bw) * (n - Ds)^3) / 3.0) + (((Bb - Bw) * (D - n - Db)^3) / 3.0) + (m * Ast / 10E5 * (eff_depth - n)^2)");

            //sw.WriteLine("      = (({0:f3} * {1:f3}^3) / 3.0) - ((({0:f3} - {2:f3}) * ({1:f3} - {3:f3})^3) / 3.0) + ((({4:f3} - {1:f3}) * ({5:f3} - {1:f3} - {6:f3})^3) / 3.0) + ({7:f3} * {8:f3} / 10E5 * ({9:f3} - {1:f3})^2)",
            //    Bs, n, bw, Ds, Bb, D, Db, modular_ratio, Ast, eff_depth);






            //sw.WriteLine("      = (({0:f3} * {1:f3}^3) / 3.0) - ((({0:f3} - {2:f3}) * ({1:f3} - {3:f3})^3) / 3.0) + ((({4:f3} - {1:f3}) * ({5:f3} - {1:f3} - {6:f3})^3) / 3.0) + ({7:f3} * {8:f3} / 10E5 * ({9:f3} - {1:f3})^2)", Bs, n, bw, Ds, Bb, D, Db, modular_ratio, Ast, eff_depth);
            //sw.WriteLine("      = (({0:f3} * {1:f3}^3) / 3.0) - ((({0:f3} - {2:f3}) * ({1:f3} - {3:f3})^3) / 3.0) + ((({4:f3} - {1:f3}) * ({5:f3} - {1:f3} - {6:f3})^3) / 3.0) + ({7:f3} * {8:f3} / 10E5 * ({9:f3} - {1:f3})^2)", Bs, n, bw, Ds, Bb, D, Db, modular_ratio, Ast, eff_depth);

            sw.WriteLine();
            sw.WriteLine("   = {0:f6} sq.sq.m.", ina);


            double Zt;
            Zt = (ina / (n));
            sw.WriteLine();
            sw.WriteLine("Zt = Ina / n = {0:f6}/{1:f4} = {2:f4} cu.m", ina, n, Zt);


            double Zb;
            Zb = (ina / ((eff_depth - n)));
            sw.WriteLine("Zb = Ina / (deff - n) = {0:f6} / ({1} - {2:f4}) = {3:f4} cu.m", ina, eff_depth, n, Zb);


            n *= 100.0;
            Bs *= 1000.0;
            bw *= 1000.0;
            Bb *= 1000.0;
            Ds *= 1000.0;
            D *= 1000.0;
            Db *= 1000.0;
            eff_depth *= 1000.0;


            double fc;
            fc = (M * 10E4) / (Zt * 10E5);
            //fc = (design_moment * 10E4) / (Zt * 10E5);
            sw.WriteLine();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("STEP 4 : CHECK FOR STRESSES");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Stress in concrete = fc = (M * 10E4)/(Zt * E5)");
            sw.WriteLine("                   = ({0} * 10E4)/({1:f4} * 10^6)",
                M.ToString("0.000"), Zt.ToString("0.000"));

            //sw.WriteLine("                   = {0} kg/sq.cm.", fc.ToString("0.000"));

            sw.WriteLine();
            sw.WriteLine();
            //sw.WriteLine("  Allowable Stress in M25 grade of Concrete ");
            //sw.WriteLine("(Ref to Clause 303.1 and 303.2.1 of IRC:21-2000");
            if (fc < allow_stress_concrete)
            {
                //sw.WriteLine("fc < 83 kg/sq.cm, OK");
                sw.WriteLine("                   = {0:e3} kg/sq.cm. < (σ_c) {1}  kg/sq.cm,   OK",
                    fc, allow_stress_concrete);
            }
            else
            {
                sw.WriteLine("                   = {0:e3} kg/sq.cm. > (σ_c) {1} kg/sq.cm,  NOT OK",
                    fc, allow_stress_concrete);

            }

            double fs;
            fs = (M * 10E4) / (Zb * 10E5);
            fs = fs * 10;
            sw.WriteLine();
            sw.WriteLine("Allowable Stress in Fe {0} grade of steel = {1} kg/sq.cm", steel_grade, sigma_sv * 10);
            sw.WriteLine();
            sw.WriteLine("Stress in Steel = fs = (M * 10E4)/(Zb*10^6)");
            sw.WriteLine("                     = ({0} * 10E4)/({1:f4}*10^6)", M, Zb);
            //sw.WriteLine("                     = {0:f4} kg/sq.cm", fs);

            sw.WriteLine();
            double chk_val = sigma_sv * 10;
            if (fs < chk_val)
            {
                sw.WriteLine("                     = {0:e4} kg/sq.cm < {1} kg/sq.cm,  OK", fs, chk_val);
                //sw.WriteLine("Allowable Stress in Fe {0} grade of steel = {1} kg/sq.cm > {2:f2}, OK", steel_grade, chk_val, fs);
            }
            else
            {
                sw.WriteLine("                     = {0:e4} kg/sq.cm > {1} kg/sq.cm,  NOT OK", fs, chk_val);
                //sw.WriteLine("Allowable Stress in Fe {0} grade of steel = {1} kg/sq.cm <  {2:f2}, NOT OK", steel_grade, chk_val, fs);
                //sw.WriteLine("Allowable Stress in Fe 415 grade of steel = 2000 kg/sq.cm < fs, NOT OK");
            }

            double lever_arm = (M * 10E4) / (fs * Ast);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Therefore, Lever Arm = (M*10E4)/(fs*Ast)");
            sw.WriteLine("                     = ({0} * 10E4)/({1:e4} * {2})",
                M,
                fs,
                Ast);
            sw.WriteLine("                     = {0} cm", lever_arm.ToString("0.000"));
            lever_arm = lever_arm / 100;
            sw.WriteLine("                     = {0} m", lever_arm.ToString("0.000"));

            sw.WriteLine();
            sw.WriteLine();
        }

        public void Write_Shear_Program(StreamWriter sw, double shear_value, ref double bd, ref double sp, ref double no, int mark)
        {
            double rad = Math.PI / 180.0;
            //sw.WriteLine();
            //sw.WriteLine("STEP 5");
            //sw.WriteLine("Design of Shear Reinforcement");
            //sw.WriteLine("-----------------------------");

            double shear = shear_value;

            sw.WriteLine();
            sw.WriteLine("Design Shear = {0:f2} t", shear_value);
            sw.WriteLine("             = {0:f2} * 10E3 N", shear_value);

            //deff = (D * 100.0) - (cover / 10) - (bar_dia / 2) - (3 * bar_dia);
            //Chiranjit [2011 06 06]
            deff = (D/10) - (cover / 10) - (bar_dia / 2) - (3 * bar_dia);
            double tau_v = (shear * 10E3) / (bw * deff*10);

            sw.WriteLine();
            sw.WriteLine("Nominal Shear Stress τ_v = V/(b * deff)");
            sw.WriteLine("                         = {0:f2}*10E3/({1:f2} * {2:f2})",
                shear, bw, deff*10);
            sw.WriteLine("                         = {0:f3} N/sq.mm", tau_v);
            sw.WriteLine();

            double ck_val = 0.07 * concrete_grade;
            if (ck_val > tau_v)
            {
                sw.WriteLine("0.07 * {0} = {1:f3} N/sq.mm > {2:f3} N/sq.mm , OK",
                    concrete_grade, ck_val, tau_v);
            }
            else
            {
                sw.WriteLine("0.07 * {0} = {1:f3} N/sq.mm < {2:f3} N/sq.mm , NOT OK",
                    concrete_grade, ck_val, tau_v);
                sw.WriteLine("Increase size of Girder, and redesign.");
            }

            sw.WriteLine();
            sw.WriteLine("Assuming 2 bars {0:f0} mm dia are bent up at the section,", (bar_dia * 10));
            sw.WriteLine("Shear force resisted by bent up bars is given by, ");

            double Asv = Math.PI * (bar_dia * 10.0) * (bar_dia * 10.0) / 4;

            double Vs = sigma_sv * 2 * Asv * Math.Sin(45 * rad) / 10000.0;

            sw.WriteLine("      Vs = σ_sv * Asv * Sin 45°");
            sw.WriteLine("         = ({0:f2} * 2 * {1:f2} * 1)/(10000.0 * √2)", sigma_sv, Asv);
            sw.WriteLine("         = {0:f2} t", Vs);
            sw.WriteLine();
            double balance_shear = shear - Vs;
            sw.WriteLine("Balance Shear = {0:f3} - {1:f3} = {2:f3} t", shear, Vs, balance_shear);
            sw.WriteLine("Using 10 mm diameter, 4 legged Stirrups, ");

            Asv = Math.PI * 10.0 * 10.0 / 4.0;
            double Sv = (sigma_sv * 4 * Asv * deff * 10) / (balance_shear * 10E3);
            sw.WriteLine("Spacing = Sv = (σ_sv * Asv * deff)/V");
            sw.WriteLine("             = ({0:f2} * 4 * {1:f2} * {2:f2} * 10)/({3:f2}*10E3)",
                sigma_sv, Asv, deff, balance_shear);
            sw.WriteLine("             = {0:f3} mm", Sv);

            if (Sv > 50 && Sv < 200)
            {
                Sv = (int)Sv / 10;
                Sv = Sv * 10;
            }
            else if (Sv > 200)
            {
                Sv = 200;
            }
            else
            {
                Sv = 0;
            }


            if (Sv == 0)
            {
                sw.WriteLine("Increase size of section and redesign.");
            }
            else
            {
                sw.WriteLine("Provide 10 diameter, 4 legged stirrups at {0:f0} mm. Marked as ({1}) in the Drawing", Sv, mark);
                sw.WriteLine("centre to centre distance.");
            }

            bd = 10;
            sp = Sv;
            no = 4;

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
                    indx = lst_content[i].LastIndexOf(" ");
                    if (indx > 0)
                        kStr = MyList.RemoveAllSpaces(lst_content[i].Substring(0, indx));
                    else
                        kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "D_S":
                            Ds = mList.GetDouble(1);
                            txt_Ds.Text = Ds.ToString();
                            break;
                        case "D":
                            D = mList.GetDouble(1);
                            txt_D.Text = D.ToString();
                            break;
                        case "Bb":
                            txt_Bb.Text = mList.StringList[1];
                            break;
                        //case "Bs":
                        //    //txt_Bs.Text = mList.StringList[1];
                        //    break;
                        case "Db":
                            txt_Db.Text = mList.StringList[1];
                            break;
                        case "nl":
                            txt_nl.Text = mList.StringList[1];
                            break;
                        case "BW":
                            bw = mList.GetDouble(1);
                            txt_bw.Text = bw.ToString();
                            break;
                        case "L":
                            L = mList.GetDouble(1);
                            txt_span_girders.Text = L.ToString();
                            break;
                        case "GS":
                            Gs = mList.GetDouble(1);
                            txt_Gs.Text = Gs.ToString();
                            break;
                        case "DESIGN_MOMENT_MID":
                            design_moment_mid = mList.GetDouble(1);
                            txt_design_moment_mid.Text = design_moment_mid.ToString();
                            break;



                        case "DESIGN_MOMENT_QUARTER":
                            design_moment_quarter = mList.GetDouble(1);
                            txt_design_moment_quarter.Text = design_moment_quarter.ToString();
                            break;


                        case "DESIGN_MOMENT_DEFF":
                            design_moment_deff = mList.GetDouble(1);
                            txt_design_moment_deff.Text = design_moment_deff.ToString();
                            break;

                        case "V1":
                            v1 = mList.GetDouble(1);
                            txt_design_shear_mid.Text = v1.ToString();
                            break;

                        case "V2":
                            v2 = mList.GetDouble(1);
                            txt_design_shear_quarter.Text = v2.ToString();
                            break;


                        case "V3":
                            v3 = mList.GetDouble(1);
                            txt_design_shear_deff.Text = v3.ToString();
                            break;

                        case "CONCRETE_GRADE":
                            cmb_concrete_grade.SelectedItem = mList.StringList[1];
                            break;
                        case "ALLOW_CONC_STRESS":
                            txt_allowable_stress_concrete_grade.Text = mList.StringList[1];
                            break;
                        case "STEEL_GRADE":
                            steel_grade = mList.GetDouble(1);
                            cmb_Steel_Grade.Text = steel_grade.ToString();
                            break;
                        case "MODULAR_RATIO":
                            modular_ratio = mList.GetDouble(1);
                            txt_modular_ratio.Text = modular_ratio.ToString();
                            break;
                        case "BAR_DIA":
                            bar_dia = mList.GetDouble(1);
                            txt_bar_dia_L2.Text = bar_dia.ToString();
                            break;
                        case "TOTAL_BARS":
                            total_bars_L2 = mList.GetDouble(1);
                            txt_total_bar_L2.Text = total_bars_L2.ToString();
                            break;
                        case "COVER":
                            cover = mList.GetDouble(1);
                            txt_cover.Text = cover.ToString();
                            break;
                        case "SIGMA_SV":
                            sigma_sv = mList.GetDouble(1);
                            txt_sigma_sv.Text = sigma_sv.ToString();
                            break;
                        case "SPACE_MAIN":
                            space_main = mList.GetDouble(1);
                            //txt_space_main_girder.Text = space_main.ToString();
                            break;
                        case "SPACE_CROSS":
                            space_cross = mList.GetDouble(1);
                            txt_space_cross_girder.Text = space_cross.ToString();
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
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region USER DATA
                sw.WriteLine();
                sw.WriteLine("USER DATA");
                sw.WriteLine("---------");
                sw.WriteLine("D_S ={0} m", Ds);
                sw.WriteLine("D ={0} m", D);
                sw.WriteLine("Bb ={0} m", Bb);
                sw.WriteLine("Bs ={0} m", Bs);
                sw.WriteLine("Db ={0} m", Db);
                sw.WriteLine("Dw ={0} m", Dw);
                sw.WriteLine("nl ={0} m", nl);



                sw.WriteLine("BW ={0} cm", bw);
                sw.WriteLine("L ={0} m", L);
                sw.WriteLine("GS ={0} cm", Gs);

                sw.WriteLine("DESIGN_MOMENT_MID ={0} ", design_moment_mid);
                sw.WriteLine("DESIGN_MOMENT_QUARTER ={0} ", design_moment_quarter);
                sw.WriteLine("DESIGN_MOMENT_DEFF ={0} ", design_moment_deff);

                sw.WriteLine("V1 ={0} ", v1);
                sw.WriteLine("V2 ={0} ", v2);
                sw.WriteLine("V3 ={0} ", v3);

                sw.WriteLine("CONCRETE_GRADE ={0} ", concrete_grade);
                sw.WriteLine("ALLOW_CONC_STRESS ={0} ", allow_stress_concrete);
                sw.WriteLine("STEEL_GRADE ={0} ", steel_grade);
                sw.WriteLine("MODULAR_RATIO ={0} ", modular_ratio);
                sw.WriteLine("BAR_DIA ={0} cm", bar_dia);
                sw.WriteLine("TOTAL_BARS ={0} ", total_bars_L2);
                sw.WriteLine("COVER ={0} ", cover);
                sw.WriteLine("SIGMA_SV ={0} ", sigma_sv);
                sw.WriteLine("SPACE_MAIN ={0} ", space_main);
                sw.WriteLine("SPACE_CROSS ={0} ", space_cross);
                sw.WriteLine();

                ////cm
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();

            //design_moment_mid = MyList.StringToDouble(txt_design_moment_mid.Text, 0.0);
            //design_moment_quarter = MyList.StringToDouble(txt_design_moment_quarter.Text, 0.0);
            //design_moment_deff = MyList.StringToDouble(txt_design_moment_deff.Text, 0.0);

            //v1 = MyList.StringToDouble(txt_design_shear_mid.Text, 0.0);
            //v2 = MyList.StringToDouble(txt_design_shear_quarter.Text, 0.0);
            //v3 = MyList.StringToDouble(txt_design_shear_deff.Text, 0.0);
//            LONG_LENGTH=0
//LONG_INN_DEFF_MOM=190.3
//LONG_INN_DEFF_SHR=221.5
//LONG_INN_L2_MOM=389
//LONG_INN_L2_SHR=54.34
//LONG_INN_L4_MOM=341.2
//LONG_INN_L4_SHR=101.4
//LONG_OUT_DEFF_MOM=175.6
//LONG_OUT_DEFF_SHR=109.8
//LONG_OUT_L2_MOM=372.9
//LONG_OUT_L2_SHR=34.95
//LONG_OUT_L4_MOM=332
//LONG_OUT_L4_SHR=43.87
//CROSS_MOM=22.03
//CROSS_SHR=29.66

            if (design_moment_mid == 0.0 && design_moment_quarter == 0.0 && design_moment_deff == 0.0 &&
                v1 == 0.0 && v2 == 0.0 && v3 == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : Moment (L/2) = 389.0 Ton-m\n";
                msg += "            : Shear  (L/2) = 54.34 Ton\n";
                msg += "            : Moment (L/4) = 341.2 Ton-m\n";
                msg += "            : Shear  (L/4) = 101.4 Ton\n";
                msg += "            : Moment (L/4) = 175.6 Ton-m\n";
                msg += "            : Shear  (L/4) = 109.8 Ton\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {
                Write_User_Input();
                Calculate_Program(rep_file_name);
                Write_Drawing_File();
                is_process = true;
                FilePath = user_path;
                if (File.Exists(rep_file_name))
                {
                    MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iApp.View_Result(rep_file_name);
                }
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult f_v_r = new frmViewResult(rep_file_name);
            f_v_r.ShowDialog();
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
            kPath = Path.Combine(kPath, "Design of RCC T-Beam Bridge with Bottom Flange");

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
                this.Text = "DESIGN OF RCC T-BEAM BRIDGE WITH BOTTOM FLANGE : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(value, "LONGITUDINAL_GIRDERS");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);


                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_T_Beam_Long_Girder_with_Bottom_Flange.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_LONGITUDINAL_GIRDERS_WITH_BOTTOM_FLANGE.FIL");



                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(user_input_file);
                //btnDrawing.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }

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
                    Read_Max_Moment_Shear_from_Analysis();
                }
            }

        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(drawing_path, "TBEAM_Long_Girder", @"TBEAM_Worksheet_Design1");
            //iApp.SetDrawingFile_Path(drawing_path, "REINFORCEMENT_DETAILS_OF_LONG_GIRDERS", @"Bridge\T Beam");
        }

        
        private void rbtn_inner_girder_CheckedChanged(object sender, EventArgs e)
        {
            isInner = rbtn_inner_girder.Checked;
            Load_Analysis_Data();
        }

        private void cmb_concrete_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_concrete_grade.Name)
            {
                switch (cmb_concrete_grade.Text)
                {
                    case "15":
                        txt_allowable_stress_concrete_grade.Text = "50.0";
                        break;
                    case "20":
                        txt_allowable_stress_concrete_grade.Text = "66.7";
                        break;
                    case "25":
                        txt_allowable_stress_concrete_grade.Text = "83.3";
                        break;
                    case "30":
                        txt_allowable_stress_concrete_grade.Text = "100.0";
                        break;
                    case "35":
                        txt_allowable_stress_concrete_grade.Text = "116.7";
                        break;
                    case "40":
                        txt_allowable_stress_concrete_grade.Text = "133.3";
                        break;
                    case "45":
                        txt_allowable_stress_concrete_grade.Text = "150.0";
                        break;
                    case "50":
                        txt_allowable_stress_concrete_grade.Text = "166.7";
                        break;
                    case "55":
                        txt_allowable_stress_concrete_grade.Text = "183.0";
                        break;
                    case "60":
                        txt_allowable_stress_concrete_grade.Text = "200.0";
                        break;
                }
            }
            else
            {
                switch (cmb_Steel_Grade.Text)
                {
                    case "240":
                        txt_sigma_sv.Text = "125";
                        break;
                    case "415":
                        txt_sigma_sv.Text = "200";
                        break;
                    case "500":
                        txt_sigma_sv.Text = "240";
                        break;
                }
            }
        }
        private void frm_RccTBeam_Bottom_Flange_Load(object sender, EventArgs e)
        {
            cmb_concrete_grade.SelectedIndex = 2;
            cmb_Steel_Grade.SelectedIndex = 1;
            //Load_Analysis_Data();

        }
    }
}
