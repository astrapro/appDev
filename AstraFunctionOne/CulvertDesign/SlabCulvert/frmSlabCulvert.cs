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


namespace AstraFunctionOne.CulvertDesign.SlabCulvert
{
    public partial class frmSlabCulvert : Form
    {

        //const string Title = "ANALYSIS OF RCC SLAB MINOR BRIDGE";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RCC SLAB MINOR BRIDGE [BS]";
                return "DESIGN OF RCC SLAB MINOR BRIDGE [IRC]";
            }
        }



        #region Variable Declaration

        string file_path = "";
        string rep_file_name = "";
        string user_input_file = "";
        string system_path = "";
        string drawing_path = "";
        string user_path = "";


        double D, CW, FP, L, WC, support_width, conc_grade, st_grade, sigma_cb, sigma_st;
        double m, j, Q, a1, b1, b2, W1, cover, delta_c, delta_wc, bar_dia;
        CONCRETE_GRADE CON_GRADE;

        bool is_process = false;

        IApplication iApp = null;

        #region  Drawing Variable
        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
        double _sp1, _sp2, _sp3, _sp4, _sp7;

        #endregion

        #endregion
        public frmSlabCulvert(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;
            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp7 = 0.0;

            //FilePath = iApp.LastDesignWorkingFolder;

            FilePath = Path.Combine(iApp.LastDesignWorkingFolder, Title);

        }

        #region Chiranjit [2012 07 20]


        public void WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("D  = {0:f2}", D);
                sw.WriteLine("CW  = {0:f2}", CW);
                sw.WriteLine("FP  = {0:f2}", FP);
                sw.WriteLine("L  = {0:f2}", L);
                sw.WriteLine("WC  = {0:f2}", WC);
                sw.WriteLine("support_width  = {0:f2}", support_width);
                sw.WriteLine("conc_grade  = {0:f2}", conc_grade);
                sw.WriteLine("st_grade  = {0:f2}", st_grade);
                sw.WriteLine("sigma_cb  = {0:f2}", sigma_cb);

                sw.WriteLine("sigma_st  = {0:f2}", sigma_st);
                sw.WriteLine("m  = {0:f2}", m);
                sw.WriteLine("j  = {0:f2}", j);
                sw.WriteLine("Q  = {0:f2}", Q);
                sw.WriteLine("a1  = {0:f2}", a1);
                sw.WriteLine("b1  = {0:f2}", b1);
                sw.WriteLine("b2  = {0:f2}", b2);
                sw.WriteLine("W1  = {0:f2}", W1);
                sw.WriteLine("cover  = {0:f2}", cover);
                sw.WriteLine("delta_c  = {0:f2}", delta_c);
                sw.WriteLine("delta_wc  = {0:f2}", delta_wc);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public string GetAstraDirectoryPath(string userpath)
        {


            string  kPath = Path.Combine(userpath, "Design of RCC Slab Culvert");

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
                this.Text = Title + " : " + value;

                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "DESIGN_OF_RCC_SLAB_CULVERT.TXT");
                user_input_file = Path.Combine(system_path, "SLAB_CULVERT.FIL");

                btnProcess.Enabled = Directory.Exists(value);
                btnReport.Enabled = File.Exists(rep_file_name);
                btnDrawing.Enabled = File.Exists(user_input_file);

            }
        }

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (fbd.SelectedPath != user_path)
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                }
            }
        }


        public void Read_From_File()
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
                        case "D":
                            D = mList.GetDouble(1);
                            txt_D.Text = D.ToString();
                            break;
                        case "CW":
                            CW = mList.GetDouble(1);
                            txt_CW.Text = CW.ToString();
                            break;
                        case "FP":
                            FP = mList.GetDouble(1);
                            txt_FP.Text = FP.ToString();
                            break;
                        case "L":
                            L = mList.GetDouble(1);
                            txt_L.Text = L.ToString();
                            break;
                        case "support_width":
                            support_width = mList.GetDouble(1);
                            txt_width_support.Text = support_width.ToString();
                            break;
                        case "W":
                            W1 = mList.GetDouble(1);
                            txt_W1.Text = W1.ToString();
                            break;
                        case "conc_grade":
                            conc_grade = mList.GetDouble(1);
                            cmb_slab_fck.Text = conc_grade.ToString();
                            break;
                        case "st_grade":
                            st_grade = mList.GetDouble(1);
                            //cmb_slab_fy.Text = st_grade.ToString();
                            break;
                        case "sigma_cb":
                            sigma_cb = mList.GetDouble(1);
                            ////txt_sigma_cb.Text = sigma_cb.ToString();
                            break;
                        case "sigma_st":
                            sigma_st = mList.GetDouble(1);
                            //txt_sigma_st.Text = sigma_st.ToString();
                            break;
                       
                        case "m":
                            m = mList.GetDouble(1);
                            //txt_m.Text = m.ToString();
                            break;
                        case "j":
                            j = mList.GetDouble(1);
                            txt_bar_dia.Text = j.ToString();
                            break;
                        case "Q":
                            Q = mList.GetDouble(1);
                            //txt_Q.Text = Q.ToString();
                            break;
                        case "a1":
                            a1 = mList.GetDouble(1);
                            txt_a1.Text = a1.ToString();
                            break;
                        case "b1":
                            b1 = mList.GetDouble(1);
                            txt_b1.Text = b1.ToString();
                            break;
                        case "W1":
                            W1 = mList.GetDouble(1);
                            txt_W1.Text = W1.ToString();
                            break;
                        case "cover":
                            cover = mList.GetDouble(1);
                            txt_cover.Text = cover.ToString();
                            break;
                        case "delta_c":
                            delta_c = mList.GetDouble(1);
                            txt_delta_c.Text = delta_c.ToString();
                            break;
                        case "delta_wc":
                            delta_wc = mList.GetDouble(1);
                            txt_delta_wc.Text = delta_wc.ToString();
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
        



        void InitializeData()
        {
            D = MyList.StringToDouble(txt_D.Text,0.0);
            CW = MyList.StringToDouble(txt_CW.Text,0.0);
            FP = MyList.StringToDouble(txt_FP.Text,0.0);
            L = MyList.StringToDouble(txt_L.Text,0.0);
            WC = MyList.StringToDouble(txt_WC.Text,0.0);
            support_width = MyList.StringToDouble(txt_width_support.Text,0.0);
            conc_grade = MyList.StringToDouble(cmb_slab_fck.Text,0.0);
            CON_GRADE = (CONCRETE_GRADE)(int)conc_grade;
            st_grade = MyList.StringToDouble(cmb_slab_fy.Text, 0.0);
            sigma_cb = MyList.StringToDouble(txt_slab_sigma_c.Text,0.0);
            sigma_st = MyList.StringToDouble(txt_slab_sigma_st.Text, 0.0);
            m = MyList.StringToDouble(txt_slab_m.Text,0.0);
            bar_dia = MyList.StringToDouble(txt_bar_dia.Text,0.0);
            Q = MyList.StringToDouble(txt_slab_Q.Text,0.0);
            a1 = MyList.StringToDouble(txt_a1.Text,0.0);
            b1 = MyList.StringToDouble(txt_b1.Text,0.0);
            b2 = MyList.StringToDouble(txt_b2.Text,0.0);
            W1 = MyList.StringToDouble(txt_W1.Text,0.0);
            cover = MyList.StringToDouble(txt_cover.Text,0.0);
            delta_c = MyList.StringToDouble(txt_delta_c.Text,0.0);
            delta_wc = MyList.StringToDouble(txt_delta_wc.Text,0.0);

        }
        void Calculate_Program(string fileName)
        {
            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));

            try
            {
                #region TechSOFT Banner
                //sw.WriteLine("----------------------------------------------------------------------------------------------");
                //sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*   DESIGN OF MINOR BRIDGE / RCC SLAB BRIDGE  *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region User's Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Thickness of Slab [D] = {0:f2} mm               Marked as (B) in the Drawing", D);
                sw.WriteLine("Carriageway width [CW] = {0:f2} m               Marked as (D) in the Drawing", CW);
                sw.WriteLine("Footpath width [FP] = {0:f2} m", FP);
                sw.WriteLine("Clear Span [L] = {0:f2} m                       Marked as (A) in the Drawing", L);
                sw.WriteLine("Thickness of Wearing Course [WC] = {0:f2} mm    Marked as (C) in the Drawing", WC);
                sw.WriteLine("Width of End Support / Bearing = {0:f2} m       Marked as (E) in the Drawing", support_width);
                sw.WriteLine("Concrete Grade = M {0:f0} ", conc_grade);
                sw.WriteLine("Steel Grade = Fe {0:f0} ", st_grade);
                sw.WriteLine("Permissible Stress [σ_cb] = {0:f2} N/sq. mm", sigma_cb);
                sw.WriteLine("Permissible Stress [σ_st] = {0:f2} ", sigma_st);
                sw.WriteLine("Modular Ratio [m] = {0:f2} ", m);
                sw.WriteLine("Bar Diameter [bar_dia] = {0:f0} ", bar_dia);
                sw.WriteLine("Q = {0:f2} ", Q);
                sw.WriteLine("Live Load Dimension [a1] = {0:f2} m", a1);
                sw.WriteLine("Live Load Dimension [b1] = {0:f2} m", b1);
                sw.WriteLine("Live Load Dimension [b2] = {0:f2} m", b2);
                sw.WriteLine("Total Live Load [W1] = {0:f2} kN", W1);
                sw.WriteLine("Clear cover to Reinforcement Bars [cover] = {0:f2} mm", cover);
                sw.WriteLine("Unit weight of Concrete [γ_c] = {0:f2} N/cu.m", delta_c);
                sw.WriteLine("Unit weight of Wearing course [γ_wc] = {0:f2} N/cu.m", delta_wc);
                sw.WriteLine();
                sw.WriteLine();
                

               
                #endregion


                #region STEP 1
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Calculating Effective Span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Overall thickness of Slab = {0:f2} mm = D", D);

                double deff = D - (cover + 10);
                sw.WriteLine("Effective thickness of Slab = {0:f2} - ({1:f2} + 10) = {2:f2} mn = deff",
                    D,
                    cover,
                    deff);
                sw.WriteLine();

                sw.WriteLine("Effective span is lesser of");
                double total_span = L + (deff / 1000);
                sw.WriteLine("  i)   Clear Span + Effective Depth = {0:f2} + {1:f2} = {2:f2} m",
                    L, (deff / 1000), total_span);

                double l = L + support_width;
                sw.WriteLine("  ii)  Centre to Centre distance of End Supports / Bearings = {0:f2} + {1:f2} = {2:f2} m",
                    L, support_width, l);


                sw.WriteLine();
                sw.WriteLine("  So, Effective Span = {0:f2} m = l", l);



                #endregion

                #region STEP 2

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Bending Moment by Permanent Loads ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double slab_weight = (D / 1000.0) * delta_c;

                sw.WriteLine("Weight of Slab = (D /1000) * γc = {0:f3} * {1:f2} = {2:f2} kN/sq.m.",
                    (D / 1000.0),
                    delta_c,
                    slab_weight);

                double wt_wear_cour = (WC / 1000) * delta_wc;

                sw.WriteLine("Weight of Wearing Course = (WC /1000) * γwc = {0:f3} * {1} = {2:f3} kN/sq.m.",
                    (WC / 1000.0), delta_wc, wt_wear_cour);


                double W2 = (int)(slab_weight + wt_wear_cour);
                W2 = W2 + 1;

                sw.WriteLine();
                sw.WriteLine("Total Load = {0:f3} kN/sq.m = {1} kN/sq.m. = W2",
                    (slab_weight + wt_wear_cour), W2);

                double M1 = (W2 * l * l) / 8;

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Permanent Loads = M1 = ({0:f2} * l*l) / 8", W2);
                sw.WriteLine("                                    = ({0:f2} * {1:f2}*{1:f2}) / 8 ",
                    W2, l);
                sw.WriteLine("                                    = {0:f2} kN-m", M1);

                #endregion

                #region STEP 3

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Bending Moment by Vehicle Load / Live Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("For 5m Span Impact Factor 25%");
                sw.WriteLine("For 9m Span Impact Factor 10%");

                sw.WriteLine();
                sw.WriteLine("So, for {0:f2} m Span Impact factor ", l);
                //double im_fact = 25 - ((25 - 10) / (9 - 5)) * (6.4 - 5);


                double fact = 25.0 - ((25.0 - 10.0) / (9.0 - 5.0)) * (6.4 - 5);

                //double fact = 25 - ((25 - 10) / (9 - 5)) * (l - 5);
                sw.WriteLine("  = 25 - ((25 - 10) / (9 - 5)) * ({0:f2} - 5) = {1:f2}% = fact", l, fact);

                sw.WriteLine();
                sw.WriteLine("Length of Load = a1 = {0:f2} m.", a1);

                double ld = a1 + 2 * ((D + WC) / 1000);

                sw.WriteLine("Length of Load including 45° dispersal = a1 + 2 * ((D + WC) / 1000)");
                sw.WriteLine("                                       = {0:f2} + 2 * (({1:f2} + {2:f2}) / 1000)",
                    a1, D, WC);
                sw.WriteLine("                                       = {0:f2} m. = ld", ld);

                sw.WriteLine();
                sw.WriteLine("Effective Width of Slab perpendicular to Span = be = K * x * (1 - (x / L)) + bw");

                sw.WriteLine();
                sw.WriteLine("Placing the Load symmetrically on the Span");

                double x = l / 2;
                sw.WriteLine("x = Distance from centre of end support to centre of Load = l/2 = {0:f2}/2={1:f2} m.", l, x);

                double B = CW + (2 * FP);
                sw.WriteLine("B = Width of Slab = CW + (2 * FP) =  {0:f2} + (2 * {1:f2}) = {2:f2} m",
                    CW, FP, B);


                double b_by_l = B / l;
                sw.WriteLine();
                sw.WriteLine("B / l = {0:f2} / {1:f2} = {2:f2}", B, l, b_by_l);
                double bw = b1 + (2 * (WC / 1000));

                sw.WriteLine("bw = b1 + (2 * (WC / 1000)) = {0:f3} + (2 * {1:f3}) = {2:f3} m.",
                    b1, (WC / 1000.0), bw);

                sw.WriteLine();
                sw.WriteLine("From Table of IRC 21:2000 ");
                sw.WriteLine();

                double K = KValue.Get_K_Value(b_by_l);
                //double K = 2.84;

                sw.WriteLine();
                sw.WriteLine("For B / l = {0:f3}, for simply Supported Slab,  K = {1:f3}", b_by_l, K);
                double be = K * x * (1 - (x / l)) + bw;

                //sw.WriteLine("So, Effective Width of Load = be = 2.84 * 3.2 * {1 - (3.2 / 6.4)} + 1.01 = 5.56 m.");
                sw.WriteLine();
                sw.WriteLine("So, Effective Width of Load = be ");
                sw.WriteLine("                            = {0:f2} * {1:f2} * (1 - ({1:f2} / {2:f2})) + {3:f2}",
                    K, x, l, bw);
                sw.WriteLine("                            = {0:f2} m.", be);

                double Wd = (2 * (be / 2)) + (2 * (b1 / 2)) + b2;

                sw.WriteLine();
                sw.WriteLine("Width of Load with 45 dispersal = Wd");
                sw.WriteLine("                                =(2 * (be / 2)) + (2 * (b1 / 2)) + b2");
                sw.WriteLine("                                =(2 * ({0:f2}) / 2)) + (2 * ({1:f2} / 2)) + {2:f3}",
                    be, b1, b2);
                sw.WriteLine("                                = {0:f3} m", Wd);

               
                double TLL = W1 * ((fact/ 100.0) + 1.0);
                sw.WriteLine();
                sw.WriteLine("Total Live Load including Impact = TLL");
                sw.WriteLine("                                 = W1 * ((fact/ 100.0) + 1.0) kN");
                sw.WriteLine("                                 = {0:f2} * ({1:f2}) ", W1, (fact * L / 100));
                sw.WriteLine("                                 = {0:f0} kN", TLL);

                double LLUA = TLL / (ld * Wd); // live load unit area
                sw.WriteLine();
                sw.WriteLine("Live Load per Unit Area = LLUA");
                sw.WriteLine("                        = TLL / (ld * wd)");
                sw.WriteLine("                        = {0:f0} / ({1:f2} * {2:f2})", TLL, ld, Wd);

                sw.WriteLine("                        = {0:f2} kN/sq.m.", LLUA);

                double M2 = Math.Abs(((LLUA * ld) / 2) * (l / 2) - ((LLUA * ld) / 2) * (ld / 4));

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Live Load = M2");
                sw.WriteLine("                             = ((LLUA * ld) / 2) * (l / 2) - ((LLUA * ld) / 2) * (ld / 4)");
                sw.WriteLine("                             = (({0:f2}*{1:f2})/2) * ({2:f2}/2) - (({0:f2} * {1:f2})/2) * ({1:f2}/4)",
                    LLUA, ld, l);
                sw.WriteLine("                             = {0:f2} kN-m", M2);


                double M = M1 + M2;

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment = M = M1 + M2 ");
                sw.WriteLine("                          = {0:f2} + {1:f2}", M1, M2);
                sw.WriteLine("                          = {0:f2} kN-m", M);
                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Shear Force by Live Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Effective Span = l = {0:f2} m", l);
                sw.WriteLine("Length of Load including 45° dispersal = ld = {0:f2} m", ld);
                sw.WriteLine();
                sw.WriteLine("To get maximum Shear Force at support let us place the Load ");
                sw.WriteLine("coinciding the start point of the above lengths");

                x = ld / 2;

                sw.WriteLine();
                sw.WriteLine("x = ld / 2 = {0:f2} / 2 = {1:f2} m.", ld, x);
                b_by_l = B / l;

                sw.WriteLine("B / l = {0:f2} / {1:f2} = {2:f2}", B, l, b_by_l);
                K = KValue.Get_K_Value(b_by_l);
                sw.WriteLine();
                sw.WriteLine("From IRC 21:2000, K = {0:f3}", K);
                sw.WriteLine("bw = {0:f2} m", bw);

                be = K * x * (1 - (x / l)) + bw;

                sw.WriteLine();
                sw.WriteLine("Effective width of Load = be");
                sw.WriteLine("                        = K * x * (1 - (x / l)) + bw");
                sw.WriteLine("                        = {0:f2} * {1:f2} * (1 - ({1:f2}/{2:f2})) + {3:f3}",
                    K, x, l, bw);
                sw.WriteLine("                        = {0:f2} m", be);

                sw.WriteLine();
                sw.WriteLine("Width of Load with 45° dispersal = Wd");
                sw.WriteLine("                                 =((2 * be) / 2) + ((2 * b1) / 2) + b2");
                sw.WriteLine("                                 =((2 * {0:f2}) / 2) + ((2 * {1:f2}) / 2) + {2:f3}",
                    be, b1, b2);
                sw.WriteLine("                                 = {0:f3} m", Wd);

                LLUA = TLL / (ld * Wd); // live load unit area
                sw.WriteLine();
                sw.WriteLine("Live Load per Unit area = TLL / (ld * wd)");
                sw.WriteLine("                        = {0:f2} / ({1:f2} * {2:f2})", TLL, ld, Wd);
                sw.WriteLine("                        = {0:f2} kN/sq.m.", LLUA);

                double V1 = (LLUA * ld * 2 * (b1 + 2 * (WC / 1000) + 2 * (D / 1000)) / l);

                sw.WriteLine();
                sw.WriteLine("Shear Force by Live Load = V1");
                sw.WriteLine("                         = [LLUA * ld * 2 * (b1 + 2 * (wc / 1000) + 2 * (D / 1000)]/l");
                sw.WriteLine("                         = [{0:f2} * {1:f2} * 2 * ({2:f2} + 2 * {3:f3} + 2 * {4:f3}] / {5:f3}",
                    LLUA, ld, b1, (WC / 1000.0), (D / 1000), l);
                sw.WriteLine("                         = {0:f3} kN", V1);


                double V2 = W2 * l / 2;
                sw.WriteLine();
                sw.WriteLine("Dead Load Shear = V2 = W2 * l / 2 ");
                sw.WriteLine("                     = ({0:f2} * {1:f2}) / 2 ", W2, l);
                sw.WriteLine("                     = {0:f2} kN", V2);

                double V = V1 + V2;

                sw.WriteLine();
                sw.WriteLine("Total Design Shear = V = V1 + V2 kN");
                sw.WriteLine("                       = {0:f2} + {1:f2} kN", V1, V2);
                sw.WriteLine("                       = {0:f2} kN", V);
                #endregion

                #region STEP 5 : Structural Design of Slab 
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Structural Design of Slab ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double d = Math.Sqrt((M * 10E5) / (Q * 1000));
                sw.WriteLine("Required Effective Depth = d = √((M * 10E5) / (Q * b))");
                sw.WriteLine("                             = √(({0:f2} * 10E5) / ({1:f2} * 1000))",
                    M, Q);
                sw.WriteLine("                             = {0:f2} m", d);
                sw.WriteLine();


                sw.WriteLine();
                if (deff > d)
                {
                    sw.WriteLine("Effective depth provided = deff = {0:f2} > d = {1:f2} OK", deff, d);
                }
                else
                {
                    sw.WriteLine("Effective depth provided = deff = {0:f2} < d = {1:f2} NOT OK", deff, d);
                }

                j = 0.5 + Math.Sqrt((0.25) - ((M * 10E5) / (0.87 * ((int)conc_grade) * 1000 * deff * deff)));
                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                sw.WriteLine();
                //double Ast = (M * 10E5) / (sigma_st * j * deff);
                double Ast = (M * 10E5) / (0.87 * sigma_st * j * deff);
                sw.WriteLine();
                sw.WriteLine("Required Steel reinforcement = Ast = ( M * 10^6) / (σ_st * j * d)");
                sw.WriteLine("                                   = ({0:f2} * 10^6) / ({1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, d);
                sw.WriteLine("                                   = {0:f3} sq.mm", Ast);


                double spacing = (1000 * Math.PI * bar_dia * bar_dia / 4.0) / Ast;

                sw.WriteLine();
                sw.WriteLine("Using {0} mm dia. Bars, spacing = [1000 * ((π * {0} * {0} ) / 4)] / {1:f2}", bar_dia, Ast);
                sw.WriteLine("                               = {0:f2} mm", spacing);

                if (spacing > 50 && spacing < 250)
                {
                    spacing = (int)(spacing / 10.0); ;
                    spacing = (spacing * 10.0); ;
                }
                else if (spacing < 50)
                {
                    sw.WriteLine("Spacing {0:f0} mm c/c < 50 mm c/c , NOT OK, Redesign", spacing);
                }
                else
                {
                    sw.WriteLine();
                    spacing = 240;
                }
                sw.WriteLine();
                sw.WriteLine("                               = {0:f2} mm (Adopt)", spacing);
                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0} @ {1:f0} mm c/c          Marked as (1) in the Drawing", bar_dia, spacing);

                _bd1 = bar_dia;
                _sp1 = spacing;

                double BMDS = 0.2 * M1 + 0.3 * M2;

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Distribution Steel = 0.2 * M1 + 0.3 * M2");
                sw.WriteLine("                                      = 0.2 * {0:f2} + 0.3 * {1:f2}", M1, M2);
                sw.WriteLine("                                      = {0:f3} kN-m", BMDS);
                BMDS = (int)BMDS;
                BMDS += 1.0;

                sw.WriteLine("                                      = {0:f2} kN-m", BMDS);

                double e_dep = deff - bar_dia - bar_dia / 2.0;
                sw.WriteLine();
                sw.WriteLine("Using {0} mm. dia Bars, Effective Depth = deff - ({0} + {0}/2) = {1:f2} mm",bar_dia, e_dep);

                double req_steel = (BMDS * 10E5) / (0.87 * sigma_st * j * e_dep);

                sw.WriteLine();
                sw.WriteLine("Required Steel reinforcement = ({0:f2} * 10^6) / (0.87 *  {1:f3} * {2:f3} * {3:f2}) = {4:f3} sq.mm",
                    BMDS, sigma_st, j, e_dep, req_steel); ;

                //spacing = (1000 * Math.PI * bar_dia * bar_dia) / (req_steel * 4);

                spacing = (1000 * Math.PI * bar_dia * bar_dia / 4.0) / req_steel;
                sw.WriteLine();
                sw.WriteLine("Spacing of bars = (1000 * π * {0} * {0}) / ({1:f2} * 4)",bar_dia, req_steel);
                sw.WriteLine("                = {0:f3} mm", spacing);

                if (spacing > 250)
                    spacing = 250;
                else
                {
                    spacing = (int)(spacing / 10.0);
                    spacing = (spacing * 10.0);

                }
                sw.WriteLine("                = {0:f3} mm (Adopt)", spacing);

                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0} @ {1:f0} mm c/c as distribution Steel.      Marked as (2) in the Drawing", bar_dia, spacing);

                _bd2 = bar_dia;
                _sp2 = spacing;
                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Shear Reinforcements ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double tau = V * 10E2 / (1000 * deff);

                sw.WriteLine("Design Shear = τ = (V * 10E2) / (b * deff) ");
                sw.WriteLine("                 = ({0:f2} * 10E2) / (1000 * {1:f2}) N/sq.mm", V, deff);
                sw.WriteLine("                 = {0:f3} N/sq.mm",tau);
                sw.WriteLine();
                double K1 = 1.14 - 0.7 * (deff / 1000); // 1.14 = ?, 0.7 = ?
                if (K1 >= 0.5)
                {
                    sw.WriteLine("K1 = 1.14 - 0.7 * (deff / 1000) = 1.14 - 0.7 * {0:f3} = {1:f3} >= 0.5, O.K.",
                        (deff / 1000.0),
                        K1);
                }
                else
                {
                    sw.WriteLine("K1 = 1.14 - 0.7 * (deff / 1000) = 1.14 - 0.7 * {0:f3} = {1:f3} < 0.5, NOT OK",
                        (deff / 1000.0),
                        K1);
                }

                sw.WriteLine();




                double ast_prov = (Math.PI * bar_dia * bar_dia / 4.0) * (1000.0 / 80);
                //sw.WriteLine();
                //sw.WriteLine("Provided Steel reinforcement = ast_prov = {0:f0} sq.mm", ast_prov);

                double p = (ast_prov * 100) / (1000 * deff);

                //sw.WriteLine();
                //sw.WriteLine("Percentage p = ({0:f0} * 100) / (1000 * {1:f2}) = {2:f3}%",
                //    ast_prov, deff, p);

                double K2 = 0.5 + 0.25 * p;
                sw.WriteLine();


                List<double> list_bar_dia = new List<double>();

                list_bar_dia.Add(12);
                list_bar_dia.Add(16);
                list_bar_dia.Add(20);
                list_bar_dia.Add(25);
                list_bar_dia.Add(28);
                list_bar_dia.Add(32);
                list_bar_dia.Add(35);
                list_bar_dia.Add(40);

                int cnt = 0;
                do
                {
                    
                    ast_prov = (Math.PI * bar_dia * bar_dia / 4.0) * (1000.0 / spacing);
                    //sw.WriteLine();
                    //sw.WriteLine("Provided Steel reinforcement = ast_prov = {0:f0} sq.mm", ast_prov);

                    p = (ast_prov * 100) / (1000 * deff);

                    //sw.WriteLine();
                    //sw.WriteLine("Percentage p = ({0:f0} * 100) / (1000 * {1:f2}) = {2:f3}%",
                    //    ast_prov, deff, p);

                    K2 = 0.5 + 0.25 * p;
                    if (K2 < 1.0)
                    {
                        if (spacing > 70)
                            spacing -= 10;

                        else if (cnt < 8)
                        {
                            bar_dia = list_bar_dia[cnt++];
                            spacing = 240;
                        }

                        if (spacing == 70) break;

                    }
                }
                while (K2 < 1.0 && spacing >= 70);


                sw.WriteLine();
                sw.WriteLine("Bending up alternate bars, provide T{0} bars @ {1} mm c/c", bar_dia, spacing);
                sw.WriteLine();
                sw.WriteLine("Provided Steel reinforcement = ast_prov = (π * bar_dia * bar_dia / 4.0) * (1000.0 / spacing)");
                sw.WriteLine("                                        = (π * {0} * {0} / 4.0) * (1000.0 / {1})", bar_dia, spacing);
                sw.WriteLine();
                sw.WriteLine("                                        = {0:f0} sq.mm", ast_prov);
                sw.WriteLine();
                sw.WriteLine("Percentage p = ({0:f0} * 100) / (1000 * {1:f2}) = {2:f3}%",
                    ast_prov, deff, p);
                if (K2 < 1.0)
                {
                    sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} < 1.0,  NOT OK.", p, K2);
                    K2 = 1.0;
                    sw.WriteLine("So, K2 = 1.0");
                
                }
                else
                {
                    sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} >= 1.0, OK.", p, K2);
                }

                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------------");
                sw.WriteLine("Concrete Grade (M)  15     20     25     30     35     40       45   50   ");
                sw.WriteLine("τ_co               0.28   0.34   0.40   0.45   0.50   0.50     0.5   0.5  ");
                sw.WriteLine("--------------------------------------------------------------------------");
                sw.WriteLine();
                double tau_co = 0.0;
                switch (CON_GRADE)
                {
                    case CONCRETE_GRADE.M15:
                        tau_co = 0.28;
                        break;
                    case CONCRETE_GRADE.M20:
                        tau_co = 0.34;
                        break;
                    case CONCRETE_GRADE.M25:
                        tau_co = 0.40;
                        break;
                    case CONCRETE_GRADE.M30:
                        tau_co = 0.45;
                        break;
                    case CONCRETE_GRADE.M35:
                        tau_co = 0.50;
                        break;
                    case CONCRETE_GRADE.M40:
                        tau_co = 0.50;
                        break;
                    default:
                        tau_co = 0.50;
                        break;
                }

                sw.WriteLine("Therefore, Permissible Shear Stress");
                double tau_c = K1 * K2 * tau_co;
                sw.WriteLine("τ_c = K1 * K2 * τ_co");
                sw.WriteLine("    = {0:f3} * {1:f3} * {2:f3}", K1, K2, tau_co);
                if (tau_c > tau)
                    sw.WriteLine("    = {0:f3} > τ = {1:f3} N/sq.mm, OK", tau_c, tau);
                else
                    sw.WriteLine("    = {0:f3} < τ = {1:f3} N/sq.mm, NOT OK, more Steel will required.", tau_c, tau);


                //sw.WriteLine("      = 0.80 * 1.0 * 0.40 = 0.328 N / Sq.mm. > τ = 0.254 N / Sq.mm.
                //sw.WriteLine("So, O.K.
                //sw.WriteLine("Else not O.K.. more Steel will be required.

                #endregion


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



            #region Page 1 USER DATA
            //Thickness of Slab (Assumed) = 500 mm
            //Carriageway width   = 7.5 m = CW
            //Footpath width         = 1.0 m = FP
            //Clear Span = 6.0 m

            //Thickness of Wearing Course = 80 mm = WC
            //Width of End Support / Bearing = 0.4 m
            //Concrete Grade = M25
            //Steel Grade = Fe 415
            //Permissible Stress σcb = 8.3 N/sq. mm.
            //Permissible Stress σst = 200 N/sq. mm

            //Modular Ratio m = 10

            //j = 0.9
            //Q = 1.1

            //Live Load Dimension a1 = 3.6 m
            //Live Load Dimension b1 = 0.85 m
            //Live Load Dimension b2 = 1.20 m

            //Total Live Load W1 = 700 KN

            //Clear cover to Reinforcement Bars = 30 mm’
            //Unit weight of Concrete = δc = 24 KN/cu.m.
            //Unit weight of Wearing course = δwc = 22 KN / cu.m.
            #endregion

            #region Page2
            //            Overall depth of Slab = 500 mm = D
            //Effective depth of Slab = 500 - (cover + 10) = 460 mn = d eff.

            //Effective span is lesser of

            //i)                Clear Span + Effective Depth = 6 + 0.46 = 6.46 m
            //ii)                Centre to Centre distance of End Supports / Bearings = 6 + 0.4 = 6.4 m

            //So, Effective Span = 6.4 m = l

            //STEP 2:

            //Bending Moment by Permanent Loads
            //Weight of Slab = (D /1000) * δc = 0.5 * 24 = 12 KN/sq.m.
            //Weight of Wearing Course = (WC /1000) * δwc = 0.080 * 22 = 1.76 KN/sq.m.
            //Total Load = 13.76 NK/sq.m. - 14 KN/sq.m. = W2
            //Bending Moment for Permanent Loads = M1 = (14 * l2) / 8 = (14 * 6.42) / 8 = 72 KN m.

            //STEP 3:

            //Bending Moment by Vehicle Load / Live Load

            //For 5m Span Impact Factor 25%
            //For 9m Span Impact Factor 10%

            //So, for 6.4m Span Impact factor 
            //= 25 - {(25 - 10) / (9 - 5)} * (6.4 - 5) = 25 - (15 / 4) * 1.4 = 19.7% = If
            //Length of Load = a1 = 3.6 m.
            //Length of Load including 45° dispersal = 3.6 + 2 * {(D + WC) / 1000}
            //                        = 3.6 + 2 * (0.58) = 4.76 m. = ld

            //Effective Width of Slab perpendicular to Span = be = Kx * {1 - (x / L)} + bw

            //Placing the Load symmetrically on the Span

            //x = Distance from centre of end support to centre of Load = l / 2 = 6.4 / 2 = 3.2 m.
            //B = Width of Slab = CW + (2 * FP) =  7.5 + 2 = 9.5 m
            //B / l = 9.5 / 6.4 = 1.48
            //bw = b1 + {2 * (WC / 1000)} = 0.85 + (2 * 0.08) = 1.01 m.

            //From Table of IRC 21:2000 (given at the end of this design)

            //For B / l = 1.48, for simply Supported Slab,  K = 2.84
            //So, Effective Width of Load = be = 2.84 * 3.2 * {1 - (3.2 / 6.4)} + 1.01 = 5.56 m.

            //Width of Load with 45 dispersal = 2 * (5.56 / 2) + 2 * (0.85 / 2) + 1.2
            //                     = {(2 * be) / 2} + {(2 * b1) / 2} + b2
            //                     = 7.61 m = Wd


            #endregion


            #region Page 3
            //            Total Live Load including Impact = W1 * (IF / 100) = 700 * 1.197 = 838 KN
            //Live Load per Unit area = 838 / (ld * wd) = 838 / (4.76 * 7.61) = 23.134 KN / sq.m.

            //Bending Moment for Live Load = M2 = {(23.14 * ld) / 2} * (l / 2) - {(23.14 * ld) / 2) * (ld / 4)
            //                      = {(23.14 * 4.76) / 2} * 3.2 - {(23.14 * 4.76) / 2) * 1.19
            //                      = 110.7 KN m.

            //Design Bending Moment = M = M1 + M2 = 72 + 110.7 = 182.7 = 185 KN m.

            //STEP 4:

            //Shear Force by Live Load
            //Effective Span = l = 6.4m.
            //Length of Load including 45° dispersal = ld = 4.76 m.
            //To get maximum Shear Force at support let us place the Load coinciding the start point of the above lengths

            //x = ld / 2 = 4.76 / 2 = 2.38 m.
            //B / l = 9.5 / 6.4 = 1.48

            //From IRC 21:2000, K = 2.84
            //bw = 1.01m.

            //Effective width of Load = be = Kx * {1 - (x / L)} + bw
            //                = 2.84 * 2.38 * {1 - (2.38 / 6.4)}+ 1.01
            //                = 5.256 m.

            //Width of Load with 45° dispersal = 2 * (5.256 / 2) + 2 * (0.85 / 2) + 1.2 = 7.3 M. = wd

            //Live Load per unit area = 838 / (ld * wd) = 838 / (4.76 * 7.3) = 24.1 KN / sq.m.

            //Shear Force by Live Load = [24.1 * 4.76 * 2 * (b1 + 2 * (wc / 1000) + 2 * (D / 1000)] / l
            //                   = [24.1 * 4.76 * 2 * {0.85 + (2 * 0.08) + 1}] / 6.4
            //                   = 72 KN = V1

            //Dead Load Shear = (W2 * l) / 2 = (14 * 6.4) / 2 = 45 KN = V2

            //Total Design Shear = V = V1 + V2 = 72 + 45 = 117 KN

            //STEP 5:

            //Structural Design of Slab.
            //                      ________________
            //Required Effective Depth = d = √ ( M * 106) / (Q * b)
            //                      _____________________
            //                = √ ( 185 * 106) / (1.1 * 1000) = 410 mm

            #endregion

            #region Page 4
            //            Effective depth provided = d eff = 460 > d = 410, O.K.

            //Required Steel reinforcement = Ast = ( M * 106) / (σst * j * d)
            //                          = (185 * 106) / ( 200 * 0.9 * 460)
            //                          = 2234 Sq. mm.

            //Using 20 mm dia. Bars, spacing = [1000 * {(π * 20 * 20) / 4}] / 2234
            //                    = 140 mm

            //Provide T20 @ 140 mm c/c,

            //Bending Moment for Distribution Steel = 0.2 * M1 + 0.3 * M2
            //                        = 0.2 * 72 + 0.3 * 110
            //                        = 47.4
            //                        = 48 KN m.

            //Using 12 mm. diaBars, Effective Depth = deff - (10 + 6) = 444 mm

            //Required Steel = (48 * 106) / ( 200 * 0.9 * 444) = 600.6 Sq. mm.

            //Spacing of bars = (1000 * π * 12 * 12) / (600.6 * 4) = 188.3 mm

            //Provide T12 @ 150 mm c/c as distribution Steel.

            #endregion

            #region Page 5
            //STEP 6:    Shear Reinforcements
            //Design Shear = τ = V / bd eff = (117 * 103) / (1000 * 460) = 0.254 N / Sq. mm.
            //K1 = 1.14 - 0.7 * (d eff / 1000) = 1.14 - 0.7 * 0.460 = 0.82 >= 0.5, O.K.
            //Bending up alternate bars, provide T20 bars @ 280 mm c/c
            //Ast provided = 1122 Sq. mm.
            //Percentage p = (1122 * 100) / (1000 * 460) = 0.243%
            //K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * 0.243 = 0.560 < 1.0, Not O.K.
            //So, K2 = 1.0
            //Concrete Grade (M)                15                20                25                30                35                40
            // τ co                                                0.28                0.34                0.40                0.45                0.50                0.50
            //Therefore, Permissible Shear Stress
            //τ c = K1 * K2 * τ co
            //      = 0.80 * 1.0 * 0.40 = 0.328 N / Sq.mm. > τ = 0.254 N / Sq.mm.
            //So, O.K.
            //Else not O.K.. more Steel will be required.

            #endregion

        }
        public void Write_Drawing()
        {
            drawing_path = Path.Combine(system_path, "SLAB_CULVERT_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
          
            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";

            double _A, _B, _C, _D, _E;
            //double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
            //double _sp1, _sp2, _sp3, _sp4, _sp7;

            _A = L * 1000;
            _B = D;
            _C = WC;
            _D = CW * 1000;
            _E = support_width * 1000;

            //_bd1 = 0.0;
            //_bd2 = 0.0;
            _bd3 = 10;
            _bd4 = 10;
            _bd5 = 20;
            _bd6 = 20;
            _bd7 = 10;
            //_sp1 = 0.0;
            //_sp2 = 0.0;
            _sp3 = 300;
            _sp4 = 300;
            _sp7 = 300;
            #endregion

            try
            {

                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);

                sw.WriteLine("_bd1=T{0:f0}", _bd1);
                sw.WriteLine("_bd2=T{0:f0}", _bd2);
                sw.WriteLine("_bd3=T{0:f0}", _bd3);
                sw.WriteLine("_bd4=T{0:f0}", _bd4);
                sw.WriteLine("_bd5=T{0:f0}", _bd5);
                sw.WriteLine("_bd6=T{0:f0}", _bd6);
                sw.WriteLine("_bd7=T{0:f0}", _bd7);

                sw.WriteLine("_sp1={0:f0} c/c", _sp1);
                sw.WriteLine("_sp2={0:f0} c/c", _sp2);
                sw.WriteLine("_sp3={0:f0} c/c", _sp3);
                sw.WriteLine("_sp4={0:f0} c/c", _sp4);
                sw.WriteLine("_sp7={0:f0} c/c", _sp7);
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

        private void btnProcess_Click(object sender, EventArgs e)
        {
            DemoCheck();
            InitializeData();
            WriteUserInput();
            Calculate_Program(rep_file_name);
            Write_Drawing();
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
            iApp.View_Result(rep_file_name);
            iApp.Save_Form_Record(this, user_path);
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            //iApp.RunViewer(User_Drawing_Folder, drawing_command);

            //iApp.SetDrawingFile_Path(drawing_path, "Slab_Culvert", "");
            iApp.RunViewer(Path.Combine(file_path, "Slab Culvert Drawings"), "Slab_Culvert");
            //iApp.OpenDefaultDrawings(user_path, "Slab_Culvert");
        }

        private void frmSlabCulvert_Load(object sender, EventArgs e)
        {
            cmb_deck_select_load.Items.AddRange(LoadApplied.Get_All_LoadName(iApp).ToArray());
            cmb_deck_select_load.SelectedIndex = 0;

            cmb_slab_fck.SelectedIndex = 2;
            cmb_slab_fy.SelectedIndex = 1;
          
            #region Chiranjit Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    iApp.Read_Form_Record(this, user_path);

                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                FilePath = user_path;

                //Button_Enable_Disable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option

        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            //iApp.RunExe(rep_file_name);

            if (File.Exists(rep_file_name))
                iApp.View_Result(rep_file_name);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_concrete_grade_TextChanged(object sender, EventArgs e)
        {
            InitializeData();
            //double fck, fcc, j, Q, fcb, n;

            //fck = conc_grade;

            //fcb = fck / 3;
            //fcc = fck / 4;

            //n = m * fcb / (m * fcb + sigma_st);

            //j = 1 - (n / 3.0);
            //Q = n * j * fcb / 2;

            //txt_sigma_cb.Text = fcb.ToString("0.00");
            //txt_bar_dia.Text = j.ToString("0.000");
            //txt_Q.Text = Q.ToString("0.000");
        }

        private void cmb_deck_select_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplied la = LoadApplied.Get_Applied_Load(cmb_deck_select_load.Text);

            txt_W1.Text = la.Total_Load.ToString();
            txt_a1.Text = la.b1_b.ToString();
            txt_b1.Text = la.a1_a.ToString();

           

        }

        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_slab") ||
                ctrl.Name.ToLower().StartsWith("txt_slab"))
            {
                astg = new ASTRAGrade(cmb_slab_fck.Text, cmb_slab_fy.Text);
                astg.Modular_Ratio = MyList.StringToDouble(txt_slab_m.Text, 10.0);
                txt_slab_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_slab_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
                txt_slab_j.Text = astg.j.ToString("f3");
                txt_slab_Q.Text = astg.Q.ToString("f3");
            }
        }


        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_D.Text = "500.0";
                txt_CW.Text = "7.50";
                txt_FP.Text = "1.0";
                txt_L.Text = "6.0";
            }
        }
        #endregion Chiranjit [2012 07 20]

    }
}
