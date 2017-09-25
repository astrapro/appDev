using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;

namespace AstraFunctionOne.BridgeDesign.Design6
{
    public partial class frmDesignCantileverSlab : Form
    {
        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";

        bool is_process = false;

        #region Variable Declaration

        double a1, a2, d1, a3, d2, d3, w1, a4, fact, a5, a6, d4, w2, a7, rcc_x, rcc_y;
        double concrete_grade, steel_grade, sigma_cb, sigma_st, m, j, Q, gamma_c, gamma_wc, cover, wid_hnd_rail;

        #endregion

        #region Drawing Variable

        double _bd1, _bd2, _sp1, _sp2;


        #endregion

        IApplication iApp = null;
        public sbyte TBeamDesign { get; set; }
        public frmDesignCantileverSlab(IApplication app, sbyte TBeamDesignOption)
        {
            TBeamDesign = TBeamDesignOption;
            InitializeComponent();
            this.iApp = app;
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
        void Calculate_Program(string fileName)
        {

            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));
            #region TechSOFT Banner
            //sw.WriteLine("****************************************************************");
            ////sw.WriteLine("DESIGN OF SINGLE SPAN ONE WAY RCC SLAB BY WORKING STRESS METHOD");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21              *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*          DESIGN OF CANTILEVER SLAB          *");
            sw.WriteLine("\t\t*            FOR RCC T-BEAM BRIDGE            *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            #region USER INPUT DATA
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("USER'S DATA");
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Width of Girder [a1] = {0:f3} m     Marked as (C) in the Drawing", a1);
            sw.WriteLine();
            sw.WriteLine("Width of Kerb [a2] = {0:f3} m       Marked as (B) in the Drawing", a2);
            sw.WriteLine();
            sw.WriteLine("Height of Kerb [d1] = {0:f3} m      Marked as (K) in the Drawing", d1);
            sw.WriteLine();
            sw.WriteLine("Distance from Girder Centre to Kerb [a3] = {0:f3} m   Marked as (A) in the Drawing", a3);
            sw.WriteLine();
            sw.WriteLine("Thickness of Cantilever Slab at Girder face [d2] = {0:f3} m  Marked as (E) in the Drawing", d2);
            sw.WriteLine();
            sw.WriteLine("Thickness of Cantilever Slab at Free End [d3] = {0:f3} m   Marked as (L) in the Drawing", d3);
            sw.WriteLine();
            sw.WriteLine("Applied Wheel Load [w1] = {0:f3} kN    Marked as (M) in the Drawing", w1);
            sw.WriteLine();
            sw.WriteLine("Load Width [a4] = {0:f3} m    Marked as (G) in the Drawing", a4);
            sw.WriteLine();
            sw.WriteLine("Impact Factor [fact] = {0:f3} ", fact);
            sw.WriteLine();
            sw.WriteLine("Distance of Load Centre from Girder Face [a5] = {0:f3} m   Marked as (D) in the Drawing", a5);
            sw.WriteLine();
            sw.WriteLine("Distance of Edge of Load from Kerb Face [a6] = {0:f3}m     Marked as (H) in the Drawing", a6);
            sw.WriteLine();
            sw.WriteLine("Thickness of wearing Course [d4] = {0:f3} m     Marked as (F) in the Drawing", d4);
            sw.WriteLine();
            sw.WriteLine("Load from Hand Rail [w2] = {0:f3} kN", w2);
            sw.WriteLine();
            sw.WriteLine("Distance from Post Edge to Free End [a7] = {0:f3} m    Marked as (J) in the Drawing", a7);
            sw.WriteLine();
            sw.WriteLine("Width of Hand Railing = {0:f3} m    Marked as (I) in the Drawing", wid_hnd_rail);
            sw.WriteLine();
            sw.WriteLine("RCC Post Size = {0:f3} m X {1:f3} m ", rcc_x, rcc_y);
            sw.WriteLine();
            //sw.WriteLine("rcc_y = {0:f3} ",rcc_y); 
            sw.WriteLine("Concrete Grade = M {0:f0}  f_ck = {0:f0} N/sq.mm  ", concrete_grade);
            sw.WriteLine();
            sw.WriteLine("Steel Grade = Fe {0:f0}    f_y  = {0:f0} N/sq.mm", steel_grade);
            sw.WriteLine();
            sw.WriteLine("Permissible Stress [σ_cb] = {0:f3} N/sq.m", sigma_cb);
            sw.WriteLine();
            sw.WriteLine("Permissible Stress [σ_st] = {0:f3} N/sq.m", sigma_st);
            sw.WriteLine();
            sw.WriteLine("Lever Arm Factor [j] = {0:f3} ",j); 
            sw.WriteLine("Moment Factor [Q] = {0:f3} ",Q);
            sw.WriteLine("Unit Weight of Concrete [γ_c] = {0:f3} kN/cu.m. ", gamma_c);
            sw.WriteLine("Unit Weight of Wearing Course [γ_wc] = {0:f3} kN/cu.m.", gamma_wc);
            sw.WriteLine("Clear Cover [cover] = {0:f3} m", cover);
            sw.WriteLine("Modular Ratio [m] = {0:f2} ", m);
            sw.WriteLine();
            sw.WriteLine();

            #endregion

            try
            {
                #region STEP 1
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Permanent Loads and Moments");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,14}{4,15}",
                    "SL.N.",
                    "Structural",
                    "Load",
                    "Distance",
                    "Bending Moment");
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,14}{4,15}",
                                   "",
                                   " Element",
                                   "(kN)",
                                   "(m)  ",
                                   "(kN-m)");

                //1
                double dist = a3 + a2 - (a1 / 2) - (rcc_x / 2 + a7);
                double load = w2;
                double bend_mom_1 = dist * load;

                sw.WriteLine();
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                    "1.",
                    "Hand Rail",
                    load.ToString("0.000"),
                    dist.ToString("0.000"),
                    bend_mom_1.ToString("0.000"));

                //2
                dist = a3 + a2 - (a1 / 2) - (rcc_x / 2 + a7);
                load = rcc_x * rcc_y * gamma_c;
                double bend_mom_2 = dist * load;

                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                    "2.",
                    "RCC Post",
                    load.ToString("0.000"),
                    dist.ToString("0.000"),
                    bend_mom_2.ToString("0.000"));

                //3
                dist = a3 - (a1 / 2) + (a2 / 2);
                load = a2 * d1 * gamma_c;
                double bend_mom_3 = dist * load;

                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                                    "3.",
                                    "Kerb",
                                    load.ToString("0.000"),
                                    dist.ToString("0.000"),
                                    bend_mom_3.ToString("0.000"));

                //4
                dist = (a3 - (a1 / 2)) / 2;
                load = (a3 - (a1 / 2)) * d4 * gamma_wc;
                double bend_mom_4 = dist * load;
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                                    "4.",
                                    "Wearing Course",
                                    load.ToString("0.000"),
                                    dist.ToString("0.000"),
                                    bend_mom_4.ToString("0.000"));

                //5
                dist = (a3 - (a1 / 2) + a2) / 2;
                load = (a3 - a1 / 2 + a2) * (d2 + d3) / 2 * gamma_c;
                double bend_mom_5 = dist * load;
                sw.WriteLine("{0,-6}{1,-15}{2,10}{3,12}{4,17}",
                                    "5.",
                                    "R.C Slab",
                                    load.ToString("0.000"),
                                    dist.ToString("0.000"),
                                    bend_mom_5.ToString("0.000"));

                double total_fixed_load_bend_mom = 0.0;
                total_fixed_load_bend_mom = bend_mom_1 + bend_mom_2 + bend_mom_3 +
                    bend_mom_4 + bend_mom_5;
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("{0,-45}{1,15}",
                                    "TOTAL Permanent Load Bending Moment",
                                    total_fixed_load_bend_mom.ToString("0.000"));

                sw.WriteLine("------------------------------------------------------------");
               

                #endregion
                #region STEP 2

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Design Live Load Bending Moment");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double x = a5; // ?
                sw.WriteLine("For wheel Load placed at a6 = {0:f2} m  ", a6);
                sw.WriteLine("distance between Kerb and Load and at a5 = {0:f2} m distance", a5);
                sw.WriteLine("between Load centre to face of girder = x");

                double bw = (a4 / 2) + 2 * d4;
                sw.WriteLine();
                sw.WriteLine("bw = ((a4/2) + 2 * d4) = {0:f2} + 2 * {1:f2} = {2:f2} M",
                    (a4 / 2), d4, bw);
                sw.WriteLine();
                sw.WriteLine("be = effective width of dispersion perpendicular to Span ");
                double be = 1.2 * x + bw;

                sw.WriteLine("   = 1.2 * x + bw = 1.2 * {0:f2} + {1:f2} = {2:f2} m", x, bw, be);
                double w3 = w1 * fact / be;

                sw.WriteLine();
                sw.WriteLine("Live Load per meter width including impact = w3");
                sw.WriteLine("     = (Applied Wheel Load * Fact)/be");
                sw.WriteLine("     = ({0:f3} * {1:f2}) / {2:f2} = {3:f2} kN",
                    w1, fact, be, w3);
                double w4 = w3 * a5;
                sw.WriteLine();
                sw.WriteLine("Design Live Load Bending Moment = w3 * a5 = {0:f2} * {1:f2}", w3, a5);
                sw.WriteLine("                                = {0:f2} kN-m", w4);





                #endregion

                //double total_fixed_load_bend_mom = 21.519;

                #region STEP 3

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Design Bending Moment");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double M = total_fixed_load_bend_mom + w4;
                sw.WriteLine("Design Bending Moment = M ");
                sw.WriteLine("  = Permanent Load Bending Moment + Live Load Bending Moment");
                sw.WriteLine("  = {0:f2} + {1:f2}", total_fixed_load_bend_mom, w4);
                sw.WriteLine("  = {0:f2} kN-m", M);

                #endregion

                #region STEP 4

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double deff = Math.Sqrt((M * 10E5 / Q * 10E2))/1000.0;
                sw.WriteLine("Effective Depth required = √((M * 10^6)/(Q*10E2))");
                sw.WriteLine("                         = √(({0:f2} * 10^6)/({1:f2}*10E2))",
                    M,
                    Q);
                sw.WriteLine("                         = {0:f2} mm", deff);

                double deff_pro = (d2 - cover)*1000;

                sw.WriteLine();
                sw.WriteLine("Effective Depth Provided = d2 - cover");
                sw.WriteLine("                         = {0:f2} - {1:f2}", d2, cover);
                if (deff_pro > deff)
                {
                    sw.WriteLine("                         = {0:f2} mm > {1:f2} mm ,OK", deff_pro, deff);
                    deff = deff_pro;
                    sw.WriteLine("                So, deff = {0:f2} mm", deff);
                }
                else
                {
                    sw.WriteLine("                         = {0:f2} mm < {1:f2} mm ,NOT OK", deff_pro, deff);
                }


                sw.WriteLine();
                double ast = (M * 10E5) / (sigma_st * j * deff);
                sw.WriteLine("Steel Reinforcements = Ast = (M * 10^6)/(σ_st*j*deff)");
                sw.WriteLine("                     = ({0:f2} * 10^6)/({1:f2}*{2:f2}*{3:f2})",
                    M,
                    sigma_st,
                    j,
                    deff);
                sw.WriteLine("                     = {0:f0} sq.mm / m", ast);

                double bar_dia = 16;

                List<double> lst_Bar = new List<double>();
                lst_Bar.Add(10);
                lst_Bar.Add(12);
                lst_Bar.Add(16);
                lst_Bar.Add(20);
                lst_Bar.Add(25);
                lst_Bar.Add(32);

                double _ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 150.0);
                int indx = -1;
                do
                {
                    indx++;
                    bar_dia = lst_Bar[indx]; 
                    _ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 150.0);
                }
                while (_ast < ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @ 150 mm c/c, Ast = {1:f0} sq.mm     Marked as (1) in the Drawing",
                    lst_Bar[indx],
                    _ast);

                _bd1 = bar_dia;
                _sp1 = 150;
                double req_dist_steel = (0.12 / 100) * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("Required Distribution Steel = (0.12*1000*{0:f2})/100", deff);
                sw.WriteLine("                            = {0:f2} sq.mm", req_dist_steel);

                indx = 0;
                do
                {
                    indx++;
                    bar_dia = lst_Bar[indx];
                    _ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 150.0);
                }
                while (_ast < req_dist_steel);
                _bd2 = bar_dia;
                _sp2 = 150;
                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @ 150 mm c/c, Ast = {1:f0} sq.mm     Marked as (2) in the Drawing",
                    lst_Bar[indx],
                    _ast);

                #endregion

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------");
                sw.WriteLine("-----------------        END OF REPORT         -------------------------");
                sw.WriteLine("------------------------------------------------------------------------");
                sw.WriteLine();
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void InitializeData()
        {
            #region Variable Initialization
            a1 = MyList.StringToDouble(txt_Cant_a1.Text, 0.0);
            a2 = MyList.StringToDouble(txt_Cant_a2.Text, 0.0);
            d1 = MyList.StringToDouble(txt_Cant_d1.Text, 0.0);
            a3 = MyList.StringToDouble(txt_Cant_a3.Text, 0.0);
            d2 = MyList.StringToDouble(txt_Cant_d2.Text, 0.0);
            d3 = MyList.StringToDouble(txt_Cant_d3.Text, 0.0);
            w1 = MyList.StringToDouble(txt_Cant_w1.Text, 0.0);
            a4 = MyList.StringToDouble(txt_Cant_a4.Text, 0.0);
            fact = MyList.StringToDouble(txt_Cant_fact.Text, 0.0);
            a5 = MyList.StringToDouble(txt_Cant_a5.Text, 0.0);
            a6 = MyList.StringToDouble(txt_Cant_a6.Text, 0.0);
            d4 = MyList.StringToDouble(txt_Cant_d4.Text, 0.0);
            w2 = MyList.StringToDouble(txt_Cant_w2.Text, 0.0);
            a7 = MyList.StringToDouble(txt_Cant_a7.Text, 0.0);
            rcc_x = MyList.StringToDouble(txt_Cant_RCC_X.Text, 0.0);
            rcc_y = MyList.StringToDouble(txt_Cant_RCC_Y.Text, 0.0);
            wid_hnd_rail = MyList.StringToDouble(txt_Cant_width_of_hand_rail.Text, 0.0);


            concrete_grade = MyList.StringToDouble(txt_Cant_concrete_grade.Text, 0.0);
            steel_grade = MyList.StringToDouble(txt_Cant_steel_grade.Text, 0.0);
            sigma_cb = MyList.StringToDouble(txt_Cant_sigma_cb.Text, 0.0);
            sigma_st = MyList.StringToDouble(txt_Cant_sigma_st.Text, 0.0);
            m = MyList.StringToDouble(txt_Cant_m.Text, 0.0);
            j = MyList.StringToDouble(txt_Cant_j.Text, 0.0);
            Q = MyList.StringToDouble(txt_Cant_Q.Text, 0.0);
            gamma_c = MyList.StringToDouble(txt_Cant_gamma_c.Text, 0.0);
            gamma_wc = MyList.StringToDouble(txt_Cant_gamma_wc.Text, 0.0);
            cover = MyList.StringToDouble(txt_Cant_cover.Text, 0.0);

            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult f_v = new frmViewResult(rep_file_name);
            f_v.ShowDialog();
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
            else
                return;
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
            kPath = Path.Combine(kPath, "Design of Cantilever Slab");

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
                this.Text = "DESIGN OF CANTILEVER SLAB :" + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_T_Beam_Cant_Slab.TXT");
                user_input_file = Path.Combine(system_path, "DESIGN_OF_CANTILEVER_SLAB.FIL");
                user_drawing_file = Path.Combine(system_path, "CANTILEVER_SLAB_DRAWING.FIL");


                btnProcess.Enabled = Directory.Exists(file_path);
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

        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
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
                        case "a1":
                            a1 = mList.GetDouble(1);
                            txt_Cant_a1.Text = a1.ToString();
                            break;
                        case "a2":
                            a2 = mList.GetDouble(1);
                            txt_Cant_a2.Text = a2.ToString();
                            break;
                        case "d1":
                            d1 = mList.GetDouble(1);
                            txt_Cant_d1.Text = d1.ToString();
                            break;
                        case "a3":
                            a3 = mList.GetDouble(1);
                            txt_Cant_a3.Text = a3.ToString();
                            break;
                        case "d3":
                            d3 = mList.GetDouble(1);
                            txt_Cant_d3.Text = d3.ToString();
                            break;
                        case "w1":
                            w1 = mList.GetDouble(1);
                            txt_Cant_w1.Text = w1.ToString();
                            break;
                        case "a4":
                            a4 = mList.GetDouble(1);
                            txt_Cant_a4.Text = a4.ToString();
                            break;
                        case "fact":
                            fact = mList.GetDouble(1);
                            txt_Cant_fact.Text = fact.ToString();
                            break;
                        case "a5":
                            a5 = mList.GetDouble(1);
                            txt_Cant_a5.Text = a5.ToString();
                            break;
                        case "a6":
                            a6 = mList.GetDouble(1);
                            txt_Cant_a6.Text = a6.ToString();
                            break;
                        case "d4":
                            d4 = mList.GetDouble(1);
                            txt_Cant_d4.Text = d4.ToString();
                            break;
                        case "w2":
                            w2 = mList.GetDouble(1);
                            txt_Cant_w2.Text = w2.ToString();
                            break;
                        case "a7":
                            a7 = mList.GetDouble(1);
                            txt_Cant_a7.Text = a7.ToString();
                            break;
                        case "width_of_hand_rail":
                            wid_hnd_rail = mList.GetDouble(1);
                            txt_Cant_width_of_hand_rail.Text = a7.ToString();
                            break;
                        case "rcc_x":
                            rcc_x = mList.GetDouble(1);
                            txt_Cant_RCC_X.Text = rcc_x.ToString();
                            break;
                        case "rcc_y":
                            rcc_y = mList.GetDouble(1);
                            txt_Cant_RCC_Y.Text = rcc_y.ToString();
                            break;
                        case "concrete_grade":
                            concrete_grade = mList.GetDouble(1);
                            txt_Cant_concrete_grade.Text = concrete_grade.ToString();
                            break;
                        case "steel_grade":
                            steel_grade = mList.GetDouble(1);
                            txt_Cant_steel_grade.Text = steel_grade.ToString();
                            break;
                        case "sigma_cb":
                            sigma_cb = mList.GetDouble(1);
                            txt_Cant_sigma_cb.Text = sigma_cb.ToString();
                            break;
                        case "sigma_st":
                            sigma_st = mList.GetDouble(1);
                            txt_Cant_sigma_st.Text = sigma_st.ToString();
                            break;
                        case "m":
                            m = mList.GetDouble(1);
                            txt_Cant_m.Text = m.ToString();
                            break;
                        case "j":
                            j = mList.GetDouble(1);
                            txt_Cant_j.Text = j.ToString();

                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            txt_Cant_Q.Text = Q.ToString();
                            break;
                        case "gamma_c":
                            gamma_c = mList.GetDouble(1);
                            txt_Cant_gamma_c.Text = gamma_c.ToString();
                            break;
                        case "gamma_wc":
                            gamma_wc = mList.GetDouble(1);
                            txt_Cant_gamma_wc.Text = gamma_wc.ToString();
                            break;
                        case "cover":
                            cover = mList.GetDouble(1);
                            txt_Cant_cover.Text = cover.ToString();
                            throw new Exception("DATA_INITIALIZED");
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex) { }
            lst_content.Clear();
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                #region SWITCH
                sw.WriteLine("a1 = {0}", txt_Cant_a1.Text);
                sw.WriteLine("a2 = {0}", txt_Cant_a2.Text);
                sw.WriteLine("d1 = {0}", txt_Cant_d1.Text);
                sw.WriteLine("a3 = {0}", txt_Cant_a3.Text);
                sw.WriteLine("d3 = {0}", txt_Cant_d3.Text);
                sw.WriteLine("w1 = {0}", txt_Cant_w1.Text);
                sw.WriteLine("a4 = {0}", txt_Cant_a4.Text);
                sw.WriteLine("fact = {0}", txt_Cant_fact.Text);
                sw.WriteLine("a5 = {0}", txt_Cant_a5.Text);
                sw.WriteLine("a6 = {0}", txt_Cant_a6.Text);
                sw.WriteLine("d4 = {0}", txt_Cant_d4.Text);
                sw.WriteLine("w2 = {0}", txt_Cant_w2.Text);
                sw.WriteLine("a7 = {0}", txt_Cant_a7.Text);
                sw.WriteLine("width_of_hand_rail = {0}", txt_Cant_width_of_hand_rail.Text);
                sw.WriteLine("rcc_x = {0}", txt_Cant_RCC_X.Text);
                sw.WriteLine("rcc_y = {0}", txt_Cant_RCC_Y.Text);
                sw.WriteLine("concrete_grade = {0}", txt_Cant_concrete_grade.Text);
                sw.WriteLine("steel_grade = {0}", txt_Cant_steel_grade.Text);
                sw.WriteLine("sigma_cb = {0}", txt_Cant_sigma_cb.Text);
                sw.WriteLine("sigma_st = {0}", txt_Cant_sigma_st.Text);
                sw.WriteLine("m = {0}", txt_Cant_m.Text);
                sw.WriteLine("j = {0}", txt_Cant_j.Text);
                sw.WriteLine("Q = {0}", txt_Cant_Q.Text);
                sw.WriteLine("gamma_c = {0}", txt_Cant_gamma_c.Text);
                sw.WriteLine("gamma_wc = {0}", txt_Cant_gamma_wc.Text);
                sw.WriteLine("cover = {0}", txt_Cant_cover.Text);
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            if (TBeamDesign == 2)
            {
                iApp.SetDrawingFile_Path(user_drawing_file, "TBEAM_Cantilever", @"TBEAM_Worksheet_Design1");
            }
            else
                iApp.SetDrawingFile_Path(user_drawing_file, "TBEAM_Cantilever", "");

        }
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_A={0}", a3 * 1000);
                sw.WriteLine("_B={0}", a2 * 1000);
                sw.WriteLine("_C={0}", a1 * 1000);
                sw.WriteLine("_D={0}", a5 * 1000);
                sw.WriteLine("_E={0}", d2 * 1000);
                sw.WriteLine("_F={0}", d4 * 1000);
                sw.WriteLine("_G={0}", a4 * 1000);
                sw.WriteLine("_H={0}", a6 * 1000);
                sw.WriteLine("_I={0}", wid_hnd_rail*1000);
                sw.WriteLine("_J={0}", a7 * 1000);
                sw.WriteLine("_K={0}", d1 * 1000);
                sw.WriteLine("_L={0}", d3 * 1000);
                sw.WriteLine("_M={0} kN", w1);
                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_sp2={0}", _sp2);

            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private void frmDesignCantileverSlab_Load(object sender, EventArgs e)
        {
            if (TBeamDesign == 2)
            {
                user_input_file = Path.Combine(iApp.AppFolder, "DESIGN\\DefaultData\\DESIGN_OF_CANTILEVER_SLAB.FIL");
                Read_User_Input();
                user_input_file = "";
                pictureBox2.BackgroundImage = global::AstraFunctionOne.Properties.Resources.TBeam_Main_Girder_Bottom_Flange;
                
            }
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

            txt_Cant_sigma_cb.Text = fcb.ToString("0.00");
            txt_Cant_j.Text = j.ToString("0.000");
            txt_Cant_Q.Text = Q.ToString("0.000");
        }
    }
}
