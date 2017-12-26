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
namespace AstraFunctionOne.BridgeDesign.RailBridge
{
    public partial class frmSteel_Plate_Girder_Railway_Bridge : Form
    {
        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";
        bool is_process = false;

        string ref_string = "";

        double L, FLL, G, Wm, Ws, TRL, FL, ES1, ES2, wa, sigma_b, tau_v, Lf, sigma_tt, K, PBS;
        double Pw, Cw, pl, Llb, sigma_t, Dw, tw;

        //Drawing Variable
        string _A, _B, _C, _D, _E, _F, _G, _H, _I, _J, _K, _L,_M,_N, _O;

        IApplication iApp = null;
        public frmSteel_Plate_Girder_Railway_Bridge(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        #region Form Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(user_drawing_file, "Rail_Bridge", "");
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program();
            Write_Drawing_File();
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
            }
        }
        #endregion

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
                        case "L":
                            L = mList.GetDouble(1);
                            txt_L.Text = L.ToString();
                            break;
                        case "FLL":
                            FLL = mList.GetDouble(1);
                            txt_FLL.Text = FLL.ToString();
                            break;
                        case "G":
                            G = mList.GetDouble(1);
                            txt_G.Text = G.ToString();
                            break;
                        case "Wm":
                            Wm = mList.GetDouble(1);
                            txt_Wm.Text = Wm.ToString();
                            break;
                        case "Ws":
                            Ws = mList.GetDouble(1);
                            txt_Ws.Text = Ws.ToString();
                            break;
                        case "TRL":
                            TRL = mList.GetDouble(1);
                            txt_TRL.Text = TRL.ToString();
                            break;
                        case "FL":
                            FL = mList.GetDouble(1);
                            txt_FL.Text = FL.ToString();
                            break;
                        case "ES1":
                            ES1 = mList.GetDouble(1);
                            txt_ES1.Text = ES1.ToString();
                            break;
                        case "ES2":
                            ES2 = mList.GetDouble(1);
                            txt_ES2.Text = ES2.ToString();
                            break;
                        case "wa":
                            wa = mList.GetDouble(1);
                            txt_wa.Text = wa.ToString();
                            break;
                        case "sigma_b":
                            sigma_b = mList.GetDouble(1);
                            txt_sigma_b.Text = sigma_b.ToString();
                            break;
                        case "tau_v":
                            tau_v = mList.GetDouble(1);
                            txt_tau_v.Text = tau_v.ToString();
                            break;
                        case "Lf":
                            Lf = mList.GetDouble(1);
                            txt_Lf.Text = Lf.ToString();
                            break;
                        case "sigma_tt":
                            sigma_tt = mList.GetDouble(1);
                            txt_sigma_tt.Text = sigma_tt.ToString();
                            break;
                        case "K":
                            K = mList.GetDouble(1);
                            txt_K.Text = K.ToString();
                            break;
                        case "PBS":
                            PBS = mList.GetDouble(1);
                            txt_PBS.Text = PBS.ToString();
                            break;
                        case "Pw":
                            Pw = mList.GetDouble(1);
                            txt_Pw.Text = Pw.ToString();
                            break;
                        case "Cw":
                            Cw = mList.GetDouble(1);
                            txt_Cw.Text = Cw.ToString();
                            break;
                        case "pl":
                            pl = mList.GetDouble(1);
                            txt_pl.Text = pl.ToString();
                            break;
                        case "Llb":
                            Llb = mList.GetDouble(1);
                            txt_Llb.Text = Llb.ToString();
                            break;
                        case "sigma_t":
                            sigma_t = mList.GetDouble(1);
                            txt_sigma_t.Text = sigma_t.ToString();
                            break;
                        case "Dw":
                            Dw = mList.GetDouble(1);
                            txt_Dw.Text = Dw.ToString();
                            break;
                        case "tw":
                            tw = mList.GetDouble(1);
                            txt_tw.Text = tw.ToString();
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
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("FLL = {0}", FLL);
                sw.WriteLine("G = {0}", G);
                sw.WriteLine("Wm = {0}", Wm);
                sw.WriteLine("Ws = {0}", Ws);
                sw.WriteLine("TRL = {0}", TRL);
                sw.WriteLine("FL = {0}", FL);
                sw.WriteLine("ES1 = {0}", ES1);
                sw.WriteLine("ES2 = {0}", ES2);
                sw.WriteLine("wa = {0}", wa);
                sw.WriteLine("sigma_b = {0}", sigma_b);
                sw.WriteLine("tau_v = {0}", tau_v);
                sw.WriteLine("Lf = {0}", Lf);
                sw.WriteLine("sigma_tt = {0}", sigma_tt);
                sw.WriteLine("K = {0}", K);
                sw.WriteLine("PBS = {0}", PBS);
                sw.WriteLine("Pw = {0}", Pw);
                sw.WriteLine("Cw = {0}", Cw);
                sw.WriteLine("pl = {0}", pl);
                sw.WriteLine("Llb = {0}", Llb);
                sw.WriteLine("sigma_t = {0}", sigma_t);
                sw.WriteLine("Dw = {0}", Dw);
                sw.WriteLine("tw = {0}", tw);
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
                L = MyList.StringToDouble(txt_L.Text, 0.0);
                FLL = MyList.StringToDouble(txt_FLL.Text, 0.0);
                G = MyList.StringToDouble(txt_G.Text, 0.0);
                Wm = MyList.StringToDouble(txt_Wm.Text, 0.0);
                Ws = MyList.StringToDouble(txt_Ws.Text, 0.0);
                TRL = MyList.StringToDouble(txt_TRL.Text, 0.0);
                FL = MyList.StringToDouble(txt_FL.Text, 0.0);
                ES1 = MyList.StringToDouble(txt_ES1.Text, 0.0);
                ES2 = MyList.StringToDouble(txt_ES2.Text, 0.0);
                wa = MyList.StringToDouble(txt_wa.Text, 0.0);
                sigma_b = MyList.StringToDouble(txt_sigma_b.Text, 0.0);
                tau_v = MyList.StringToDouble(txt_tau_v.Text, 0.0);
                Lf = MyList.StringToDouble(txt_Lf.Text, 0.0);
                sigma_tt = MyList.StringToDouble(txt_sigma_tt.Text, 0.0);
                K = MyList.StringToDouble(txt_K.Text, 0.0);
                PBS = MyList.StringToDouble(txt_PBS.Text, 0.0);
                Pw = MyList.StringToDouble(txt_Pw.Text, 0.0);
                Cw = MyList.StringToDouble(txt_Cw.Text, 0.0);
                pl = MyList.StringToDouble(txt_pl.Text, 0.0);
                Llb = MyList.StringToDouble(txt_Llb.Text, 0.0);
                sigma_t = MyList.StringToDouble(txt_sigma_t.Text, 0.0);
                Dw = MyList.StringToDouble(txt_Dw.Text, 0.0);
                tw = MyList.StringToDouble(txt_tw.Text, 0.0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }
        public void Calculate_Program()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));

            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine(); 
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*                 ASTRA Pro                  *");
                sw.WriteLine("\t\t*      TechSOFT Engineering Services         *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*       DESIGN OF STEEL PLATE GIRDER         *");
                sw.WriteLine("\t\t*          FOR RAILWAY BRIDGE                *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion
                sw.WriteLine();
                sw.WriteLine();
                
                #region User'a Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Effective Span of Girder [L] = {0} m", L);
                sw.WriteLine();
                sw.WriteLine("Permanent Load as open Floor [FLL] = {0} kN/m", FLL);
                sw.WriteLine();
                sw.WriteLine("Rail Gauge [G] = {0} m                  Marked as (G) in the Drawing", G);
                _G = string.Format("{0} mm", G * 1000);
                _C = string.Format("{0} mm", 850);
                _D = string.Format("{0} mm", 825);

                sw.WriteLine();
                sw.WriteLine("Equivalent Total moving load per track for Bending Moment Calculation [Wm] = {0} kN", Wm);
                sw.WriteLine();
                sw.WriteLine("Equivalent total moving load per track for Shear force Calculation [Ws] = {0} kN", Ws);
                sw.WriteLine();
                sw.WriteLine("Top of Rail Level [TRL] = {0}", TRL);
                sw.WriteLine();
                sw.WriteLine("Foundation Level [FL] = {0}", FL);
                sw.WriteLine();
                sw.WriteLine("Embankment side slope [ES] = {0}:{1}", ES1, ES2);
                sw.WriteLine();
                sw.WriteLine("Width of Abutment [wa] = {0} m            Marked as (O) in the Drawing", wa);
                _O = string.Format("{0} mm", wa * 1000);
                _H = string.Format("{0} mm", (wa * 1000) / 2.0);


                sw.WriteLine();
                sw.WriteLine("Permissible bending stress [σ_b] = {0} N/sq.mm. = σ_b", sigma_b);

                sw.WriteLine();
                sw.WriteLine("Average Shear Stress for mild steel");
                sw.WriteLine("having yield stress of 236 N/Sq.mm. [τ_v] = {0} N/sq.mm.", tau_v);
                sw.WriteLine();


                sw.WriteLine("Interval of Cross Bearings as Effective length of Compression Flange  [Lf]= {0} m", Lf);
                sw.WriteLine();
                sw.WriteLine("Permissible shear stress through fillet weld [σ_tt] = {0} N/Sq.mm.", sigma_tt);

                sw.WriteLine();
                sw.WriteLine("Constant ‘K’ for angle between plates = {0}", K);
                sw.WriteLine();
                sw.WriteLine("Permissible Bearing Stress [PBS] = {0} N/sq.mm.", PBS);
                sw.WriteLine();
                sw.WriteLine("Wind Load [Pw] = {0} kN/sq.m", Pw);
                sw.WriteLine();
                sw.WriteLine("Coefficient for Wind load on Leeward Girder [Cw] = {0}", Cw);
                sw.WriteLine();
                sw.WriteLine("Lateral load by racking forces [pl] = {0} kN/m.", pl);
                sw.WriteLine();
                sw.WriteLine("Length of Cross Frame for Lateral Bracking [Llb] = {0} m", Llb);
                sw.WriteLine();
                sw.WriteLine("Tensile Strength of Steel [σ_t] = {0} N/Sq.mm", sigma_t);
                sw.WriteLine();
                sw.WriteLine("Depth of Web Plate [Dw] = {0} mm", Dw);
                sw.WriteLine();
                sw.WriteLine("Thickness of Web Plate [tw] = {0} mm ", tw);
                sw.WriteLine();

                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #endregion

                #region STEP 1
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Permanent Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permanent Load of open Floor track = {0:f2} kN/m", FLL);
                sw.WriteLine();

                double self_weight = 0.2 * L + 1.0;
                sw.WriteLine("Self weight = (0.2 * L + 1)");
                sw.WriteLine("            = 0.2*{0:f2} + 1", L);
                sw.WriteLine("            = {0:f2} kN/m", self_weight);
                sw.WriteLine();

                double Wf = self_weight + FLL;
                sw.WriteLine("Total Permanent Load = {0:f2} + {1:f2}", FLL, self_weight);
                sw.WriteLine("                 = {0:f2} kN/m ", Wf);
                sw.WriteLine();

                #endregion

                #region STEP 2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Moving Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Equivalent total moving load per track for Bending moment = Wm ");
                sw.WriteLine("                                                          = {0:f2} kN", Wm);

                double tot_m_load_BM = Wm / 2.0;
                sw.WriteLine("Total Moving Load per girder for B.M. = {0:f2} / 2", Wm);
                sw.WriteLine("                                      = {0:f2} kN", tot_m_load_BM);
                sw.WriteLine();
                sw.WriteLine("Equivalent total moving load per track for shear force = Ws");
                sw.WriteLine("                                                       = {0:f2} kN", Ws);
                sw.WriteLine();
                double Wl = Ws / 2.0;
                sw.WriteLine("Total moving load per girder for S.F. = Wl");
                sw.WriteLine("                                      = {0:f2}/2", Ws);
                sw.WriteLine("                                      = {0:f2} kN", Wl);
                sw.WriteLine();

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Impact Factor");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();




                // Calculate from Table 1
                //double I = 0.372;
                double I = Get_Table_1_Value(L, 3);
                sw.WriteLine("The Impact factor for steel Rail Bridge is");
                sw.WriteLine("given by Coefficient of Dynamic Augment (CDA) = I = {0}  {1}", I, ref_string);


                #endregion

                #region STEP 4
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Bending Moments");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double BM_fixed_load = (Wf * L * L) / 8.0;
                sw.WriteLine("B.M. by Permanent Load = Wf*L*L/8");
                sw.WriteLine("                   = {0:f2}*{1:f2}*{1:f2}/8", Wf, L);
                sw.WriteLine("                   = {0:f2} kN-m", BM_fixed_load);
                sw.WriteLine();

                //double BM_moving_load = Wz * L * L / 8.0;
                double BM_moving_load = (Wl * L) / 8.0;
                sw.WriteLine("B.M. by Moving Load = Wl*L/8");
                sw.WriteLine("                    = {0:f2}*{1:f2}/8", Wl, L);
                sw.WriteLine("                    = {0:f2} kN-m", BM_moving_load);
                sw.WriteLine();

                double BM_impact_moving_load = I * BM_moving_load;

                sw.WriteLine("B.M. by Impact on Moving Load = I * {0}", BM_moving_load);
                sw.WriteLine("                              = {0}*{1}", I, BM_moving_load);
                sw.WriteLine("                              = {0:f2} kN-m", BM_impact_moving_load);
                sw.WriteLine();

                double M = BM_fixed_load + BM_impact_moving_load + BM_moving_load;

                sw.WriteLine("Total design Bending Moment = M");
                sw.WriteLine("                            = {0:f2} + {1:f2} + {2:f2}", BM_fixed_load, BM_impact_moving_load, BM_moving_load);
                sw.WriteLine("                            = {0:f2} kN-m", M);
                sw.WriteLine();

                #endregion

                #region STEP 5
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Shear Force");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double SF_fixed_load = Wf * L / 2.0;
                sw.WriteLine("S.F. by Permanent Load = Wf*L/2");
                sw.WriteLine("                   = {0:f2}*{1:f2}/2", Wf, L);
                sw.WriteLine("                   = {0:f2} kN-m", SF_fixed_load);
                sw.WriteLine();

                double SF_moving_load = Wl / 2.0;
                sw.WriteLine("S.F. by Moving Load = Wl/2");
                sw.WriteLine("                    = {0:f2}/2", Wl);
                sw.WriteLine("                    = {0:f2} kN-m", SF_moving_load);
                sw.WriteLine();


                double SF_impact_moving_load = I * SF_moving_load;
                sw.WriteLine("S.F. by Impact on Moving Load = I * {0:f2}", SF_moving_load);
                sw.WriteLine("                              = {0:f3} * {1:f2}", I, SF_moving_load);
                sw.WriteLine("                              = {0:f3} kN-m", SF_impact_moving_load);
                sw.WriteLine();

                double V = SF_fixed_load + SF_moving_load + SF_impact_moving_load;
                sw.WriteLine("Total design Shear Force = V");
                sw.WriteLine("                         = {0:f2} + {1:f2} + {2:f2}", SF_fixed_load, SF_moving_load, SF_impact_moving_load);
                sw.WriteLine("                         = {0:f2} kN-m", V);
                sw.WriteLine();
                #endregion

                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : TRIAL SECTION OF WEB PLATE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double D1 = L * 1000.0 / 10.0;
                sw.WriteLine("Tentative Depth of Girder = D1");
                sw.WriteLine("                          = L * 1000/10");
                sw.WriteLine("                          = {0:f2} * 1000/10", L);
                sw.WriteLine("                          = {0:f2} mm", D1);
                sw.WriteLine();


                double D2 = (M * 10E5) / sigma_b;
                D2 = Math.Pow(D2, (1.0 / 3.0));
                D2 = D2 * 5;

                D2 = double.Parse(D2.ToString("0.00"));
                sw.WriteLine("Economical Depth of Girder = D2");
                sw.WriteLine("                           = 5*((M/σ_b)^(1/3))");
                sw.WriteLine("                           = 5*((({0:f2}*10E5)/{1:f2})^(1/3))", M, sigma_b);
                sw.WriteLine("                           = {0:f2} mm", D2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Web depth of Girder based on Shear Force Consideration assuming ");
                sw.WriteLine("12 mm thick plate is calculated as :");
                sw.WriteLine();

                double D3 = (V * 1000.0) / (tau_v * 12.0);
                sw.WriteLine("D3 = V/(τ_v*12)");
                sw.WriteLine("   = {0:f2}*1000/({1:f2}*12)", V, tau_v);

                D3 = double.Parse(D3.ToString("0.0"));
                sw.WriteLine("   = {0:f2} mm", D3);
                sw.WriteLine();

                double val1 = 0.0;

                val1 = ((int)(D2 / 100.0)) + ((int)(D3 / 100.0)) + 1;

                val1 = val1 * 100;

                double _Dw = (val1 / 2.0);

                //if (_Dw > Dw)
                //{
                //    val1 = _Dw;
                //    _Dw = Dw;
                //    Dw = val1;
                //}

                sw.WriteLine("Let us adopt a web Plate = Dw * tw = {0} * {1} sq.mm.      Marked as (A) & (E) in the Drawing", _Dw, tw);
                _A = string.Format("{0} mm", _Dw);
                _E = string.Format("{0} mm", tw);

                sw.WriteLine();




                #endregion

                #region STEP 7
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : TRIAL SECTION OF FLANGE PLATE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double Af = ((M * 10E5) / (sigma_b * _Dw)) - (_Dw * tw / 6.0);
                Af = double.Parse(Af.ToString("0"));     
                sw.WriteLine("Tentative Flange area = Af");
                sw.WriteLine("                      =[(M/(σ_b*Dw))-(Aw/6)]");
                sw.WriteLine("                      =[({0:f2}*10E5/({1:f2}*{2:f2}))-({2:f2}*{3:f0}/6)]",
                    M, sigma_b, _Dw, tw);
                sw.WriteLine("                      ={0} sq.mm", Af);

                sw.WriteLine();
                sw.WriteLine();

                double B1 = (L * 1000) / 40.0;
                sw.WriteLine("Flange width = B1");
                sw.WriteLine("             = L*1000/40");
                sw.WriteLine("             = {0:f2}/40", (L * 1000));
                sw.WriteLine("             = {0:f2} mm", B1);
                sw.WriteLine();

                double B2 = (L * 1000.0) / 45.0;
                sw.WriteLine("        To   = B2");
                sw.WriteLine("             = L*1000/45");
                sw.WriteLine("             = {0:f2}/45", (L * 1000.0));
                sw.WriteLine("             = {0:f2} mm", B2);
                sw.WriteLine();

                // Problem Bf

                double Bf = (B1 > B2) ? B1 : B2;
                Bf = (int)(Bf / 100);
                Bf = Bf * 100;
                sw.WriteLine("Let us try Bf = {0}", Bf);
                sw.WriteLine();
                sw.WriteLine();

                double tf = Af / Bf;
                tf = double.Parse(tf.ToString("0.0"));


                sw.WriteLine("Required thickness of Flange Plate = tf");
                sw.WriteLine("                                   = Af/Bf");
                sw.WriteLine("                                   = {0:f2}/{1:f2}", Af, Bf);
                sw.WriteLine("                                   = {0:f2} mm", tf);
                sw.WriteLine();
                sw.WriteLine();
                tf = double.Parse(tf.ToString("0"));
                sw.WriteLine("Let us adopt the Flange Plates = Bf*tf");
                sw.WriteLine("                               = {0} * {1} sq.mm      Marked as (B) & (F) in the Drawing", Bf, tf);
                _B = string.Format("{0} mm", Bf);
                _F = string.Format("{0} mm", tf);
                

                sw.WriteLine();
                #endregion

                #region STEP 8
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : CHECK FOR MAXIMUM STRESSES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double r = (_Dw / 2.0) + (tf / 2.0);
                sw.WriteLine("r = distance from Centre of Flange Plate to x-x axis Located at mid section");
                sw.WriteLine("  = (Dw/2) + (tf/2) ");
                sw.WriteLine("  = ({0:f2}/2) + ({1:f2}/2)", _Dw, tf);
                sw.WriteLine("  = {0:f0} mm", r);
                sw.WriteLine();
                sw.WriteLine();

                double Ixx = (tw * _Dw * _Dw * _Dw / 12.0) + (2 * Af * r * r);
                sw.WriteLine("Moment of inertia about x-x axis");
                sw.WriteLine("  = Ixx");
                sw.WriteLine("  = (tw * Dw**3 / 12.0) + (2 * Af * r * r)");
                sw.WriteLine("  = ({0:f2} * {1:f2}^3 / 12.0) + (2 * {2:f2} * {3:f2} * {3:f2})",
                    tw, _Dw, Af, r);
                Ixx = Ixx / 10E6;
                Ixx = double.Parse(Ixx.ToString("0"));
                sw.WriteLine("  = {0} * 10E6 sq.sq.mm", Ixx);
                sw.WriteLine();
                sw.WriteLine();

                double Iyy = ((2 * tf * Bf * Bf * Bf) / 12.0) + ((_Dw * tw * tw * tw) / 12.0);
                sw.WriteLine("Moment of inertia about y-y axis, which vertical passing through the web");
                sw.WriteLine(" = Iyy");
                sw.WriteLine(" = ((2 * tf * Bf**3) / 12.0) + ((Dw * tw**3) / 12.0)");
                sw.WriteLine(" = ((2 * {0:f2} * {1:f2}^3) / 12.0) + (({2:f2} * {3:f2}^3) / 12.0)",
                    tf, Bf, _Dw, tw);
                Iyy = Iyy / 10E6;
                Iyy = double.Parse(Iyy.ToString("0"));
                sw.WriteLine(" = {0} * 10E6 sq.sq.mm", Iyy);
                sw.WriteLine();
                sw.WriteLine();

                double A = 2 * tf * Bf + _Dw * tw;
                sw.WriteLine("A = 2 * tf * Bf + Dw * tw");
                sw.WriteLine("  = 2 * {0:f2} * {1:f2} + {2:f2} * {3:f2}", tf, Bf, _Dw, tw);
                sw.WriteLine("  = {0:f2} sq.mm", A);
                sw.WriteLine();
                sw.WriteLine();

                double ry = (Iyy * 10E6) / A;
                ry = Math.Sqrt(ry);

                ry = double.Parse(ry.ToString("0"));
                sw.WriteLine("ry = √(Iyy/A)");
                sw.WriteLine("   = √({0:f2}*10E6/{1:f2})", Iyy, A);
                sw.WriteLine("   = {0} mm", ry);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("The critical compressive stress for I-sections having equal Moment");
                sw.WriteLine("of Inertia about y-y axis is given by:");
                sw.WriteLine();

                double Le = Lf * 1000.0;
                sw.WriteLine("Le = Lf*1000");
                sw.WriteLine("  = {0:f2}*1000", Lf);
                sw.WriteLine("  = {0:f2} mm", Le);
                sw.WriteLine();

                double D0 = _Dw + 2 * tf;
                sw.WriteLine("D0 = Dw + 2*tf");
                sw.WriteLine("   = {0:f2} + 2*{1:f2}", _Dw, tf);
                sw.WriteLine("   = {0:f2} mm", D0);
                sw.WriteLine();
                sw.WriteLine();


                double _val1, _val2;

                _val1 = (1 + 0.05 * ((Le * tf) / (ry * D0)) * ((Le * tf) / (ry * D0)));
                //_val1 = Math.Sqrt(_val1);
                //_val1 = 2677300 * _val1;

                _val1 = Math.Sqrt(_val1);
                _val2 = 2677300.0 / ((Le / ry) * (Le / ry));

                // Problem Cs = formula
                double Cs = _val1 * _val2;
                //Cs = 2414.0;
                Cs = double.Parse(Cs.ToString("0"));


                sw.WriteLine("Cs = ((2677300/(Le/ry)^2) * {1+0.05*(Le*tf/ry*D0)^2}^0.5");
                sw.WriteLine("   = ((2677300/({0}/{1})^2) * (1+0.05*({0}*{2}/{1}*{3})^2)^0.5", Le, ry, tf, D0);
                sw.WriteLine("   = {0} N/sq.mm", Cs);
                sw.WriteLine();
                sw.WriteLine();
               
                double sigma_y = 236;
                double sigma_bc = Get_Table_2_Value(Cs, 1);
                sw.WriteLine("The permissible working stress σ_bc for different values of ");
                sw.WriteLine("critical stress Cs is given in Table 2 at the end of the report.", ref_string);
                sw.WriteLine("{0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("From the Table 2 corresponding to Cs = {0:f2} N/sq.mm", Cs);
                sw.WriteLine("and σ_y = {0:f2} N/sq.mm the permissible bending Stress", sigma_y);
                sw.WriteLine("    σ_bc = {0:f2} N/sq.mm the permissible bending Stress", sigma_bc);
                sw.WriteLine();

                double y = (_Dw / 2.0) + tf;
                sw.WriteLine("where y = distance from top fibre to mid section = (Dw/2)+tf");
                sw.WriteLine("        = ({0:f2}/2)+{1:f2}", _Dw, tf);
                sw.WriteLine("        = {0:f2} mm", y);
                sw.WriteLine();
                sw.WriteLine("Actual maximum bending stress = σ_bc");

                double _sigma_bc = (M * 10e5 * y) / (Ixx * 10e6);


                sw.WriteLine("   = M*y/Ixx");
                sw.WriteLine("   = ({0:f2} * 10E5 *{1:f2})/({2:f2}*10E6)", M, y, Ixx);

                if (_sigma_bc < sigma_bc)
                {
                    sw.WriteLine("   = {0:f2} N/sq.mm < {1:f2} N/sq.mm", _sigma_bc, sigma_bc);
                    sw.WriteLine();
                    sw.WriteLine("Hence actual flexural stresses are within SAFE permissible limits");
                }
                else
                {
                    sw.WriteLine("   = {0:f2} N/sq.mm > {1:f2} N/sq.mm", _sigma_bc, sigma_bc);
                    sw.WriteLine();
                    sw.WriteLine("Hence actual flexural stresses are not within SAFE permissible limits");
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permissible Average Shear Stress depends on:");

                double avg_shr_stress = _Dw / tw;
                avg_shr_stress = double.Parse(avg_shr_stress.ToString("0"));

                sw.WriteLine("Dw / tw = {0:f2} / {1:f2} ", _Dw, tw);
                sw.WriteLine("        = {0:f2} ", avg_shr_stress);
                sw.WriteLine();
                sw.WriteLine();
                double c = 0.9 * _Dw;
                sw.WriteLine("With stiffner spacing = c");
                sw.WriteLine("                       = 0.9 * Dw");
                sw.WriteLine("                       = 0.9 * {0:f2}", _Dw);
                sw.WriteLine("                       = {0:f2} mm", c);

                sw.WriteLine();
                c = (int)(c / 100.0);
                c += 1.0;
                c = (c * 100.0);
                sw.WriteLine("Adopt stiffner spacing = {0:f2} mm               Marked as (I) in the Drawing", c);
                _I = string.Format("{0:f2} mm", c);

                sw.WriteLine();
                sw.WriteLine();


                // Calculate from Table 3
                double req_avg_shear = Get_Table_3_Value(avg_shr_stress, 0.9);
                //double req_avg_shear = 87;
                sw.WriteLine("From Table 3, given at the end of the report permissible average shear ({0})", ref_string );
                sw.WriteLine("stress for steel for σ_y = {0:f0} N/sq.mm. is obtained as {1:f0} N/sq.mm.", sigma_y, req_avg_shear);

                sw.WriteLine();
                sw.WriteLine();

                avg_shr_stress = (V * 1000) / (_Dw * tw);
                sw.WriteLine("Average shear stress = V*1000/(Dw*tw)");
                sw.WriteLine("                     = {0:f2}*1000/({1:f2}*{2:f2})", V, _Dw, tw);
                sw.WriteLine();




                if (avg_shr_stress < req_avg_shear)
                {
                    sw.WriteLine("                     = {0:f2} N/sq.mm < {1:f0} N/sq.mm", avg_shr_stress, req_avg_shear);
                    sw.WriteLine();
                    sw.WriteLine("Hence average Shear stress is within permissible limits.");

                }
                else
                {
                    sw.WriteLine("                     = {0:f2} N/sq.mm > {1:f0} N/sq.mm", avg_shr_stress, req_avg_shear);
                    sw.WriteLine();
                    sw.WriteLine("Hence average Shear stress is not within permissible limits.");
                }

                #endregion

                #region STEP 9
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 9 : JOINT OF FLANGE AND WEB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                // Problem
                sw.WriteLine("Maximum shear force at the joint of web and flange is given by τ = V * Af * y/Ixx");
                sw.WriteLine();
                V = double.Parse(V.ToString("0.0"));
                sw.WriteLine("    V = {0} kN", V);
                sw.WriteLine("   Af = Bf * tf");
                Af = Bf * tf;
                sw.WriteLine("      = {0:f2} * {1:f2}", Bf, tf);
                sw.WriteLine("      = {0:f2} sq.mm", Af);
                sw.WriteLine();
                sw.WriteLine("  Ixx = {0:f2} * 10E6 sq.sq.mm", Ixx);

                y = (_Dw/2.0) + (tf / 2);

                sw.WriteLine();
                sw.WriteLine("    y = Dw/2 + (tf / 2);");
                sw.WriteLine("      = {0:f2} + {1:f0}", (_Dw/2), (tf / 2.0));
                sw.WriteLine("      = {0:f2} mm", y);


                double tau = V * 10E2 * Af * y / (Ixx * 10E6);
                sw.WriteLine();
                sw.WriteLine();

                tau = double.Parse(tau.ToString("0"));
                sw.WriteLine("   τ = {0:f2} * 10E2 * {1:f2} * {2:f0}/({3:f2}*10E6)",
                    V, Af, y, Ixx);
                sw.WriteLine("     = {0} N/mm.", tau);

                sw.WriteLine();

                sw.WriteLine("Providing continuous fillet weld on either side,");
                sw.WriteLine("strength of weld of size ‘S’ = 2 * K * S * σ_tt");
                
                double sigma_tf = 102;

                val1 = 2 * K * sigma_tt;
                val1 = double.Parse(val1.ToString("0.0"));
                sw.WriteLine("                             = 2 * 0.7 * 5 * 102");
                sw.WriteLine("                             = {0} * S N/mm.", val1);

                double S = val1;

                sw.WriteLine("So, S = {0:f2} / {1:f2}", tau, S);
                S = tau / S;
                S = double.Parse(S.ToString("0.00"));
                sw.WriteLine("      = {0:f2} mm", S);

                S = (int)(S);
                S += 2.0;

                sw.WriteLine("Adopt, {0:f0} mm fillet weld on either side continuously.", S);
                #endregion

                #region STEP 10
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 10 : INTERMEDIATE STIFFENERS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double _tau_v = _Dw / tw;


                sw.WriteLine("Dw/tw = {0:f0} / {1:f0}", _Dw, tw);
                _tau_v = double.Parse(_tau_v.ToString("0"));
                if (_tau_v > tau_v)
                {
                    sw.WriteLine("      = {0:f0} > τv = {1:f0} N/Sq.mm.", _tau_v, tau_v);
                }
                else
                {
                    sw.WriteLine("      = {0:f0} < τv = {1:f0} N/Sq.mm.", _tau_v, tau_v);
                }


                sw.WriteLine();
                sw.WriteLine("Vertical stiffeners are required");
                sw.WriteLine();

                double spacing1 = 0.33 * _Dw;
                double spacing2 = 1.5 * _Dw;
                sw.WriteLine("Spacing = 0.33 * Dw to 1.5 * Dw");
                sw.WriteLine("        = 0.33 * {0:f0} to 1.5 * {0:f0}", _Dw);
                sw.WriteLine("        = {0:f2} mm to {1:f0} mm", spacing1, spacing2);
                sw.WriteLine();

                double adopt_spacing = (spacing2 + spacing1) / 2.0;

                sw.WriteLine("Adopt spacing = C ");
                sw.WriteLine("              = (({0:f0} + {1:f0})/2)", spacing1, spacing2);
                sw.WriteLine("              = {0:f0} mm", adopt_spacing);
                double C = (int)(adopt_spacing / 100.0);
                C += 1;
                C = C * 100.0;

                sw.WriteLine("              ≈ {0:f0} mm ", C);
                sw.WriteLine();

                sw.WriteLine("Maximum unsupported panel length of web = {0:f0} mm", C);

                double req_leng = 270 * tw;
                sw.WriteLine();

                sw.WriteLine("Requirement = 270 * tw");
                sw.WriteLine("            = 270 * {0:f0}", tw);
                if (req_leng > C)
                {
                    sw.WriteLine("            = {0:f0} mm > {1:f0} mm", req_leng, C);
                }
                else
                {
                    sw.WriteLine("            = {0:f0} mm < {1:f0} mm", req_leng, C);
                }
                sw.WriteLine();

                sw.WriteLine("Intermediate stiffeners are desired for minimum Moment of inertia ");
                sw.WriteLine();
                sw.WriteLine("  = I = (1.5 * Dw*Dw*Dw * tw * tw * tw)/ (C*C)");

                I = (1.5 * _Dw * _Dw * _Dw * tw * tw * tw) / (C * C);

                sw.WriteLine("  = (1.5 * {0:f0}^3 * {1:f0}^3)/ ({2:f0}*{2:f0})", _Dw, tw, C);

                I = I / 10E3;
                I = double.Parse(I.ToString("0"));
                sw.WriteLine("  = {0:f0} * 10E3 sq.sq.mm", I);



                sw.WriteLine();

                sw.WriteLine("For 10mm thick stiffeners plate, outstand should not exceed");
                sw.WriteLine();


                //Problem (t?)
                double h = tw * 10;
                sw.WriteLine(" h = 12 * t");
                sw.WriteLine("   = 12 * 10");
                sw.WriteLine("   = {0:f0} mm", h);
                sw.WriteLine();

                double adpt_stiff = 10 * h;
                sw.WriteLine("Adopt stiffeners plate of size = 10 * {0:f0} sq.mm.       Marked as (J) & (K) in the Drawing", h);
                _J = string.Format("10 mm");
                _K = string.Format("{0:f0} mm", h);

                //sw.WriteLine("                               =  {0:f0} sq.mm.", adpt_stiff);
                sw.WriteLine();

                double _I_ = 10.0 * h * h * h / 3;

                _I_ = (_I_ / 10E3);

                sw.WriteLine("I = 10 * {0:f0}^3 / 3", h);
                if (_I_ > I)
                {
                    sw.WriteLine("  = {0:f0} * 10E3 > {1:f0} * 10E3 sq.sq.mm.", _I_, I);
                }
                else
                {
                    sw.WriteLine("  = {0:f0} * 10E3 < {1:f0} * 10E3 sq.sq.mm.", _I_, I);
                }
                #endregion

                #region STEP 11
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 11 : CONNECTIONS OF VERTICAL STIFFENERS TO WEB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double sh_weld = (125.0 * tw * tw) / h;


                sw.WriteLine("Shear on welds joining stiffeners to web");
                sw.WriteLine("  = (125 * tw * tw) / h");
                sw.WriteLine("  = (125 * {0:f0} * {0:f0}) / {1:f0} ", tw, h);
                sw.WriteLine("  = {0:f0} N/mm.", sh_weld);
                sw.WriteLine();

                S = sh_weld / (K * sigma_tf);
                S = double.Parse(S.ToString("0.00"));
                sw.WriteLine("For size of weld = S");
                sw.WriteLine("                 = {0:f0} / (K * σ_tt)", sh_weld);
                sw.WriteLine("                 = {0:f0} / ({1:f2} * {2:f2}) ", sh_weld, K, sigma_tf);
                sw.WriteLine("                 = {0} mm",S);
                sw.WriteLine();


                double eff_min_leng = 10 * tw;
                sw.WriteLine("Effective minimum length of weld = 10 * tw");
                sw.WriteLine("                                 = 10 * {0:f0}", tw);
                sw.WriteLine("                                 = {0:f0} mm.", eff_min_leng);
                sw.WriteLine();



                // Problem Lc = 160?
                double Lc = 160.0;
                if (eff_min_leng < 160)
                {
                    Lc = 160;
                }
                else
                {
                    Lc = eff_min_leng;
                }
                sw.WriteLine("Adopt Lc = {0:f0} mm long , 5 mm minimum size intermittent fillet welds on either side.", Lc);
                #endregion

                #region STEP 12
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 12 : END BEARING STIFFENER");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Maximum shear force = V = {0:f2} kN", V);
                sw.WriteLine();

                //Problem (he = 300?)
                double he = 300;
                sw.WriteLine("The end bearing stiffener is designed as column if he = {0:f0} mm", he);
                sw.WriteLine();

                double end_brng_stiffner = he / tw;
                if (end_brng_stiffner > 12)
                {
                    sw.WriteLine("he / tw = {0:f0} /{1:f0} = {2:f0} mm > 12", he, tw, end_brng_stiffner);
                }
                else
                {
                    sw.WriteLine("he / tw = {0:f0} /{1:f0} = {2:f0} mm < 12", he, tw, end_brng_stiffner);
                }
                sw.WriteLine();



                double req_bearing_area = V * 10E2 / PBS;
                req_bearing_area = double.Parse(req_bearing_area.ToString("0")); 
                sw.WriteLine("Bearing area required = V * 10E2 / σ_b");
                sw.WriteLine("                      = {0:f2} * 10E2 / {1:f0}", V, PBS);
                sw.WriteLine("                      = {0} sq.mm.", req_bearing_area);
                sw.WriteLine();



                double plate_size = 2 * he * end_brng_stiffner;
                sw.WriteLine("Using two plates of size {0:f0} * {1:f0} provided", he, end_brng_stiffner);
                sw.WriteLine("  = 2 * {0:f0} * {1:f0} ", he, end_brng_stiffner);

                if (plate_size > req_bearing_area)
                {
                    sw.WriteLine("  = {0:f0} sq.mm. > {1:f0} sq.mm. ", plate_size, req_bearing_area);
                }
                else
                {
                    sw.WriteLine("  = {0:f0} sq.mm. < {1:f0} sq.mm. ", plate_size, req_bearing_area);
                }
                sw.WriteLine();

                double Db = he;
                double tb = end_brng_stiffner;

                sw.WriteLine("So, tb = {0:f0}, Db = {1:f0}            Marked as (L) & (M) in the Drawing", tb, Db);
                _L = string.Format("{0:f0} mm", tb);
                _M = string.Format("{0:f0} mm", Db);
                sw.WriteLine();


                double Wb = 20 * tw;
                sw.WriteLine("The length of web plates which acts along with the stiffener plates ");
                sw.WriteLine("to bear the end reaction = Wb");
                sw.WriteLine("                         = 20*tw");
                sw.WriteLine("                         = 20 * {0:f0}", tw);
                sw.WriteLine("                         = {0:f0} mm         Marked as (N) in the Drawing", Wb);
                _N = string.Format("{0:f0} mm", Wb);

                sw.WriteLine();



                sw.WriteLine("Moment of Inertia of end bearing two stiffeners plates");
                sw.WriteLine();

                _I_ = ((tb * ((Db * 2 + tw) * (Db * 2 + tw) * (Db * 2 + tw))) / 12.0) + ((2.0 * Wb * tw * tw * tw) / 12.0);


                sw.WriteLine("I = ((tb * ((Db * 2 + tw)^3))/12.0) + ((2*Wb*tw**3)/12)");
                sw.WriteLine();
                sw.WriteLine("  = (({0} * (({1} * 2 + {2})^3))/12.0) + ((2*{3}*{2}^3)/12)", tb, Db, tw, Wb);


                //sw.WriteLine("  = (({0:f0} * (({1:f0} * 2 + {2:f0}) * (({1:f0} * 2 + {2:f0}) * (({1:f0} * 2 + {2:f0}))) / 12.0)", tb, Db, tw);
                //sw.WriteLine("     + ((2.0 * {0:f0} * {1:f0} * {1:f0} * {1:f0}) / 12.0)", Wb, tw);

                _I_ = (_I_ / 10E5);
                _I_ = double.Parse(_I_.ToString("0"));
                sw.WriteLine("  = {0:f0} * 10E5 sq.sq.mm", _I_);
                sw.WriteLine();

                A = ((Db * 2 + tw) * tb) + (2 * Wb * tw);

                sw.WriteLine("Area = A ");
                sw.WriteLine("     = (Db * 2 + tw) * tb + 2 * Wb * tw");
                sw.WriteLine("     = ({0:f0} * 2 + {1:f0}) * {2:f0} + 2 * {3:f0} * {1:f0}", Db, tw, tb, Wb);
                sw.WriteLine("     = {0:f0} sq.mm.", A);
                sw.WriteLine();


                sw.WriteLine("Slenderness Ratio = λ = L/r");
                sw.WriteLine();

                //Problem L = 1120 ?
                Le = 0.7 * _Dw;
                sw.WriteLine("Where, L = {0} mm", Le);
                sw.WriteLine();

                double _r = Math.Sqrt(((_I_ * 10E5) / A));
                _r = double.Parse(_r.ToString("0"));
                sw.WriteLine(" r  = √(I / A)");
                sw.WriteLine("    = √({0:f0}*10E5 / {1:f0})", _I_, A);
                sw.WriteLine("    = {0:f0} mm", _r);
                sw.WriteLine();

                double lamda = (Le / _r);
                lamda = double.Parse(lamda.ToString("0.0"));
                sw.WriteLine(" λ = {0:f0} / {1:f0}", Le, _r);
                sw.WriteLine("   = {0:f2}", lamda);
                sw.WriteLine();


                // Calculate from Table 4
                double sigma_ac = 138;

                sigma_ac = Get_Table_4_Value(lamda, 1);
                sigma_ac = (int)sigma_ac;

                sw.WriteLine("Referring to Table 4, given at the end of this report, ({0})", ref_string);
                sw.WriteLine();

                sw.WriteLine("Permissible stress in axial compression = σ_ac ");
                sw.WriteLine("                                        = {0:f0} N/Sq.mm.", sigma_ac);
                sw.WriteLine();

                double req_area = (V * 10E2) / sigma_ac;

                req_area = (int)req_area;
                sw.WriteLine("Area required = V * 10E2 / σ_ac");
                sw.WriteLine("              = {0:f2} * 10E2 / {1:f0}", V, sigma_ac);
                sw.WriteLine("              = {0:f0} sq.mm.", req_area);

                #endregion

                #region STEP 13
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 13 : CONNECTIONS BETWEEN BEARING STIFFENERS AND WEB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();



                sw.WriteLine("Length available for welding using alternate intermittent welds ");
                sw.WriteLine();

                Le = 2 * (_Dw - 40);
                sw.WriteLine(" = 2*(Dw - 40)");
                sw.WriteLine(" = 2*({0:f0} - 40)", _Dw);
                sw.WriteLine(" = {0:f0} mm", Le);
                sw.WriteLine();



                double req_strength_weld = (V * 10E2) / Le;

                sw.WriteLine("Required strength of weld = V*10E2 / {0:f0} ", Le);
                sw.WriteLine("                          = {0:f0}*10E2 / {1:f0} ", V, Le);
                sw.WriteLine("                          = {0:f2} N/mm", req_strength_weld);
                sw.WriteLine();



                double weld_size = req_strength_weld / (K * sigma_tt);

                sw.WriteLine("Size of Weld = 391.5 / (K* σtt) ");
                sw.WriteLine("             = {0:f2} / ({1:f2} * {2})", req_strength_weld, K, sigma_tt);
                sw.WriteLine("             = {0:f2} mm.", weld_size);
                sw.WriteLine();

                weld_size = (int)weld_size;
                weld_size += 1.0;


                sw.WriteLine("Adopt {0:f0} mm fillet welds of length Lc = {1:f0} mm intermittently on either side.", weld_size, Lc);
                #endregion

                #region STEP 14
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 14 : LATERAL BRACING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                sw.WriteLine("To resist the structure against wind, racking and centrifugal forces,");
                sw.WriteLine("lateral bracing is provided End cross frames and intermediate cross frames");
                sw.WriteLine("are provided for spans more than 20 m.");
                sw.WriteLine();



                sw.WriteLine("Wind Load = Pw");
                sw.WriteLine("          = {0} kN/sq.mm,", Pw);
                sw.WriteLine();
                sw.WriteLine("    Coeff = Cw");
                sw.WriteLine("          = {0}", Cw);
                sw.WriteLine();



                double depth_girder = (_Dw + 2 * tw) / 1000.0;
                sw.WriteLine("Depth of Girder = (Dw + 2 * tw) / 1000");
                sw.WriteLine("                = ({0:f0} + 2 * {1:f0}) / 1000", _Dw, tw);
                sw.WriteLine("                = {0:f2} m", depth_girder);
                sw.WriteLine();

                L = MyList.StringToDouble(txt_L.Text, 0.0);
                sw.WriteLine("Span = L = {0} m", L);
                sw.WriteLine();


                double wnd_load_windward = Pw * depth_girder * L;
                sw.WriteLine("Wind Load on windward Girder = Pw * 1.7 * 30 ");
                sw.WriteLine("                             = {0} * {1:f2} * {2:f2} ", Pw, depth_girder, L);
                sw.WriteLine("                             = {0:f2} kN", wnd_load_windward);
                sw.WriteLine();




                double wnd_load_Lee_ward = Cw * wnd_load_windward;

                sw.WriteLine("Wind Load on Lee Ward Girder = Cw * {0:f2}", wnd_load_windward);
                sw.WriteLine("                             = {0:f2} * {1:f2}", Cw, wnd_load_windward);
                sw.WriteLine("                             = {0:f2} kN", wnd_load_Lee_ward);
                sw.WriteLine();

                double tot_wnd_load = wnd_load_windward + wnd_load_Lee_ward;
                sw.WriteLine("Total Wind Load = {0:f2} + {1:f2}", wnd_load_windward, wnd_load_Lee_ward);
                sw.WriteLine("                = {0:f2} kN.", tot_wnd_load);
                sw.WriteLine();




                sw.WriteLine("Lateral Load by racking forces = pl");
                sw.WriteLine("                               = {0} kN/m", pl);
                sw.WriteLine();

                double tot_rack_force = pl * L;
                sw.WriteLine("Total racking force = 6 * L");
                sw.WriteLine("                    = 6 * {0:f2}", L);
                sw.WriteLine("                    = {0:f2} kN", tot_rack_force);
                sw.WriteLine();




                double total_lateral_load = tot_rack_force + tot_wnd_load;
                sw.WriteLine("Total Lateral Load = {0:f2} + {1:f2}", tot_rack_force, tot_wnd_load);
                sw.WriteLine("                   = {0:f2} kN", total_lateral_load);
                sw.WriteLine();

                double lat_load_each_end = total_lateral_load / 2.0;
                sw.WriteLine("Lateral Load at each end = {0:f2} / 2", total_lateral_load);
                sw.WriteLine("                         = {0:f2} kN", lat_load_each_end);
                sw.WriteLine();




                sw.WriteLine("Considering length of each cross frames = Llb");
                sw.WriteLine("                                        = {0:f2} m", Llb);

                sw.WriteLine();


                double max_tension_diagonal = (total_lateral_load / 2.0) * Math.Sqrt(((Llb * Llb + G * G) / G));

                sw.WriteLine("Maximum tension in the diagonal = ({0:f0} / 2) * √((Llb*Llb + G*G) / G)", total_lateral_load);
                sw.WriteLine("                                = ({0:f0} / 2) * √(({1}*{1} + {2}*{2}) / {2})", total_lateral_load, Llb, G);
                sw.WriteLine("                                = {0:f2} kN", max_tension_diagonal);
                sw.WriteLine();




                req_area = (max_tension_diagonal * 10E2) / (0.6 * sigma_t);

                sw.WriteLine("Area required = {0:f2} * 103 / (0.6 * σt)", max_tension_diagonal);
                sw.WriteLine("              = {0:f2} * 10E2 / (0.6 * {1:f2})", max_tension_diagonal, sigma_t);
                sw.WriteLine("              = {0:f2} sq.mm", req_area);
                sw.WriteLine();

                sw.WriteLine("Adopt Angle Section 100 * 100 with 10mm thickness ");
                sw.WriteLine();

                double max_comp_for_each_frame = total_lateral_load / 2.0;
                max_comp_for_each_frame = double.Parse(max_comp_for_each_frame.ToString("0"));
                sw.WriteLine("Maximum compressive force in horizontal member of each");
                sw.WriteLine();
                sw.WriteLine("frame = {0:f0}/2", total_lateral_load);
                sw.WriteLine("      = {0:f0} kN", max_comp_for_each_frame);
                sw.WriteLine();



                sw.WriteLine("Length of Members = G");
                sw.WriteLine("                  = {0} m", G);
                sw.WriteLine();


                // Problem  0.65 ?
                double eff_len = 0.65 * G;
                sw.WriteLine("Effective Length = 0.65 * {0}", G);
                sw.WriteLine("                 = {0:f3} m", eff_len);
                sw.WriteLine();

                sw.WriteLine("For angle section 100 * 75 with 10 mm thickness");
                sw.WriteLine();

                double Rxx, Ryy, Area;
                Rxx = Ryy = Area = 0.0;
                Ryy = Get_ISA_Value(10, "10075", ref Area, ref Rxx, ref Ryy);

                Area *= 100;// cm to mm
                Ryy *= 10; // cm to mm
                lamda = eff_len * 1000 / Ryy;

                lamda = double.Parse(lamda.ToString("0.0"));
                //lamda = 50.4;

                sw.WriteLine("Area = A = {0} Sq.mm., r = {1} mm,", Area, Ryy);
                sw.WriteLine();
                sw.WriteLine("       λ = ({0} * 1000) / {1}", eff_len, Ryy);
                sw.WriteLine("         = {0}", lamda);
                sw.WriteLine();



                //Rxx = Ryy = Area = r = 0.0;
                
                


                sigma_ac = Get_Table_4_Value(lamda, 1);
                sigma_ac = double.Parse(sigma_ac.ToString("0"));

                sw.WriteLine("From Table 4 ({0}), Permissible stress = σac = {1:f0} N/Sq.mm",ref_string, sigma_ac);

                double safe_load = ((_Dw + (tw / 2) * 2) * sigma_ac) / 1000.0;
                safe_load = double.Parse(safe_load.ToString("0"));

                total_lateral_load = double.Parse(total_lateral_load.ToString("0"));

                if (safe_load > (total_lateral_load / 2.0))
                {
                    sw.WriteLine("Safe Load on Member = (Dw + (tw/2) * 2) * σ_ac /1000 = {0} kN > ({1} /2)", safe_load, total_lateral_load);
                } 
                else
                {
                    sw.WriteLine("Safe Load on Member = (Dw + (tw/2) * 2) * σ_ac /1000 = {0} kN < ({1} /2)", safe_load, total_lateral_load);
                }
                #endregion

                #region STEP 15
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 15 : DESIGN OF CROSS FRAMES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                val1 = (total_lateral_load / 2.0);
                val1 = double.Parse(val1.ToString("0"));
                sw.WriteLine("Lateral load to be resisted by one frame = {0}/2 = {1:f0} kN", total_lateral_load, val1);
                sw.WriteLine();

                double diagonal_tension = 1.0 * Math.Sqrt(Llb * Llb + G * G) / (Llb);
                diagonal_tension *= val1;
                diagonal_tension = double.Parse(diagonal_tension.ToString("0"));

                sw.WriteLine("Tension in the diagonal = {0} * secθ", val1);
                sw.WriteLine("              = {0} * (1/cosθ)", val1);
                sw.WriteLine("  = {0} * [1 / (2.0 / √ ({1}*{1} + {2}*{2}))]", val1, Llb, G);
                sw.WriteLine("  = {0} kN", diagonal_tension);
                sw.WriteLine();

                req_area = diagonal_tension * 1000 / (0.6 * sigma_t);
                req_area = double.Parse(req_area.ToString("0"));
                sw.WriteLine("Area required = ({0}*1000) / (0.6 * σ_t)", diagonal_tension);
                sw.WriteLine("              = ({0} * 1000) / (0.6 * {1})", diagonal_tension, sigma_t);
                sw.WriteLine("              = {0} sq.mm.", req_area);


                Ryy = Get_ISA_Value(10, "9060",ref Area, ref Rxx, ref Ryy);

                Area *= 100;
                Ryy *= 10;
                sw.WriteLine("Provide angle section 90*60 with 10 mm thickness Area provided = {0} sq.mm.", Area);
                sw.WriteLine("Adopt interval of cross frames at 6 m.");


                sw.WriteLine();

                Write_Tables(sw);
                #region Comment
                /*
                #region Tables Print
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Table 1: E.U.D.L., C.D.A. and longitudinal Loads for");
                sw.WriteLine("         Modified B.G. Loading");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Span       Total E.U.D.L. Total E.U.D.L       C.D.A.      Tractive     Braking");
                sw.WriteLine("(m)        for B.M (kN)   for S.F. (kN)       (I.F.)      Effort (kN)  Force (kN)");

                sw.WriteLine("----------------------------------------------------------------------------------");
                sw.WriteLine("1           490              490               1.000         81           62");
                sw.WriteLine("2           490              519               1.000        164          123");
                sw.WriteLine("3           490              662               1.000        164          123");
                sw.WriteLine("4           596              778               0.950        245          184");
                sw.WriteLine("5           741              888               0.877        245          184");
                sw.WriteLine("6           838              985               0.817        245          185");
                sw.WriteLine("7           911              1068              0.765        327          221");
                sw.WriteLine("8           981              1154              0.721        409          276");
                sw.WriteLine("9          1040              1265              0.683        409          276");
                sw.WriteLine("10         1101              1377              0.650        490          331");
                sw.WriteLine("12         1377              1589              0.594        490          331");
                sw.WriteLine("15         1631              1801              0.531        490          368");
                sw.WriteLine("20         1964              2168              0.458        735          496");
                sw.WriteLine("25         2356              2586              0.408        735          565");
                sw.WriteLine("30         2727              2997              0.372        981          662");
                sw.WriteLine("40         3498              3815              0.324        981          816");
                sw.WriteLine("50         4253              4630              0.293        981          978");
                sw.WriteLine("60         5051              5442              0.271        981         1140");
                sw.WriteLine("70         5831              6254              0.255        981         1301");
                sw.WriteLine("80         6603              7065              0.243        981         1463");
                sw.WriteLine("90         7391              7876              0.233        981         1625");
                sw.WriteLine("100        8201              8686              0.225        981         1787");
                sw.WriteLine("110        9011              9496              0.219        981         1949");
                sw.WriteLine("120        9820             10306              0.213        981         2110");
                sw.WriteLine("130       10630             11115              0.209        981         2272");
                sw.WriteLine("-----------------------------------------------------------------------------------");



                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Table 2: Allowable Working Stress σbc for different ");
                sw.WriteLine("         Values of Critical Stress Cs (IRC:24)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Cs                                σbc for steel conforming                                σbc for steel conforming to");
                sw.WriteLine("        IS: 226 (Mid steel with                                IS:961 (High tensile steel");
                sw.WriteLine("        σy = 136 N/mm2 (N/mm2)                                with σy = 299, 331 & 362 N/mm2)");
                sw.WriteLine("(N/mm2)                                (N/mm2)                                                                        (N/mm2)");

                sw.WriteLine("----------------------------------------------------------------------");
                sw.WriteLine("30               15                              15");
                sw.WriteLine("40               20                              20");
                sw.WriteLine("50               25                              25");
                sw.WriteLine("60               30                              30");
                sw.WriteLine("70               35                              35");
                sw.WriteLine("80               38                              38");
                sw.WriteLine("90               42                              42");
                sw.WriteLine("100              46                              46");
                sw.WriteLine("120              53                              54");
                sw.WriteLine("140              60                              62");
                sw.WriteLine("160              67                              70");
                sw.WriteLine("180              72                              77");
                sw.WriteLine("200              76                              84");
                sw.WriteLine("220              80                              90");
                sw.WriteLine("240              84                              96");
                sw.WriteLine("260              88                              102");
                sw.WriteLine("280              92                              108");
                sw.WriteLine("300              96                              114");
                sw.WriteLine("350              105                             127");
                sw.WriteLine("400              112                             137");
                sw.WriteLine("450              119                             146");
                sw.WriteLine("500              124                             153");
                sw.WriteLine("550              129                             159");
                sw.WriteLine("600              133                             165");
                sw.WriteLine("650              136                             171");
                sw.WriteLine("700              139                             174");
                sw.WriteLine("800              144                             188");
                sw.WriteLine("900              149                             194");
                sw.WriteLine("1500             158                             212");
                sw.WriteLine("2150             158                             224");
                sw.WriteLine("----------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Table 3: Allowable Average Shear Stress in Stiffened Webs");
                sw.WriteLine("         of Steel Conforming to IS: 226 (IRC:24-1967)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
                sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
                sw.WriteLine("-----------------------------------------------------------------------");

                sw.WriteLine("110    87       87       87      87       87      87      87");
                sw.WriteLine("130    87       87       87      87       87      84      82");
                sw.WriteLine("150    87       87       87      85       80      77      75");
                sw.WriteLine("170    87       87       83      80       76      72      70");
                sw.WriteLine("190    87       87       79      75");
                sw.WriteLine("200    87       85       77");
                sw.WriteLine("220    87       80       73");
                sw.WriteLine("240    87       77");
                sw.WriteLine("-----------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Table 4: Allowable Working Stress σac in N/mm2 on Effective");
                sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine();

                sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
                sw.WriteLine("λ= (L/r)                ______________________________________");
                sw.WriteLine("           236        299        331       362");
                sw.WriteLine("---------------------------------------------------");
                sw.WriteLine("0         140.0      171.2       191.5    210.0");
                sw.WriteLine("20        136.0      167.0       186.0    204.0");
                sw.WriteLine("40        130.0      157.0       174.0    190.0");
                sw.WriteLine("60        118.0      139.0       151.6    162.0");
                sw.WriteLine("80        101.0      113.5       120.3    125.5");
                sw.WriteLine("100        80.5       87.0        90.2     92.7");
                sw.WriteLine("120        63.0       66.2        68.0     69.0");
                sw.WriteLine("140        49.4       51.2        52.0     52.6");
                sw.WriteLine("160        39.0       40.1        40.7     41.1");
                sw.WriteLine("---------------------------------------------------");


                #endregion
                /**/
                #endregion
                #endregion

                #region End of Report
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion
                
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }

        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_A={0}",_A);
                sw.WriteLine("_B={0}",_B);
                sw.WriteLine("_C={0}",_C);
                sw.WriteLine("_D={0}",_D);
                sw.WriteLine("_E={0}",_E);
                sw.WriteLine("_F={0}",_F);
                sw.WriteLine("_G={0}",_G);
                sw.WriteLine("_H={0}",_H);
                sw.WriteLine("_I={0}",_I);
                sw.WriteLine("_J={0}",_J);
                sw.WriteLine("_K={0}",_K);
                sw.WriteLine("_L={0}",_L);
                sw.WriteLine("_M={0}",_M);
                sw.WriteLine("_N={0}",_N);
                sw.WriteLine("_O={0}",_O);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();   
            }

        }
        public double Get_Table_1_Value(double span_length, int indx)
        {
            return iApp.Tables.EUDL_CDA(span_length, indx, ref ref_string);

            //span_length = Double.Parse(span_length.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "Tables");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_1.txt");

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
            //    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
               
            //    if (double.TryParse(mList.StringList[0], out a2) && mList.Count == 6)
            //    {
            //        find = true;
            //    }
            //    else
            //    {
            //        find = false;
            //    }
            //    if (find)
            //    {
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);

            //    if (a1 == span_length)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;

            //    }
            //    else if (a1 > span_length)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (span_length - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_2_Value(double Cs_value, int indx)
        {

            return iApp.Tables.Allowable_Working_Stress_Critical(Cs_value, indx, ref ref_string);

            //Cs_value = Double.Parse(Cs_value.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_2.txt");

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
            //    if (Cs_value < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (Cs_value > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == Cs_value)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > Cs_value)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (Cs_value - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_3_Value(double d_by_t, double d_point)
        {

            return iApp.Tables.Allowable_Average_Shear_Stress(d_by_t, d_point , ref ref_string);


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

            //double a1, returned_value;
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
        public double Get_Table_4_Value(double lamda, int indx)
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
        /// <summary>
        /// Get the ISA Table value
        /// </summary>
        /// <param name="thickness">Thickness of steel</param>
        /// <param name="size">Size given like 9060, 10065, 10075</param>
        /// <returns></returns>
        public double Get_ISA_Value(double thickness, string size, ref double A, ref double Rxx, ref double Ryy)
        {

            return iApp.Tables.Indian_Standard_Angles(thickness, size, ref A, ref  Rxx, ref Ryy, ref ref_string);

            //thickness = Double.Parse(thickness.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "ISA.txt");

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
            //    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');

            //    if (double.TryParse(mList.StringList[0], out a2) && mList.Count == 9)
            //    {
            //        find = true;
            //    }
            //    else
            //    {
            //        find = false;
            //    }
            //    if (find)
            //    {
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    kStr = lst_list[i].StringList[0];

            //    if (size == kStr)
            //    {
            //        a1 = lst_list[i].GetDouble(1);
            //        if (a1 == thickness)
            //        {
            //            A = lst_list[i].GetDouble(2);
            //            Rxx = lst_list[i].GetDouble(5);
            //            Ryy = lst_list[i].GetDouble(6);
            //            returned_value = Ryy;
            //            break;
            //        }
            //    }
            
            //}

            //lst_list.Clear();
            //lst_content.Clear();
            //return returned_value;
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
            kPath = Path.Combine(kPath, "Railway Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Steel Plate Girder Railway Bridge");

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
                this.Text = "DESIGN OF STEEL PLATE GIRDER RAILWAY BRIDGE : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Steel_PG_Rail_Girder.TXT");
                user_input_file = Path.Combine(system_path, "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE.FIL");
                user_drawing_file = Path.Combine(system_path, "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE_DRAWING.FIL");

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
        public string Title
        {
            get
            {
                return "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE";
            }
        }
        public void Write_Tables(StreamWriter sw)
        {

            sw.WriteLine();
            sw.WriteLine();

            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "ISA.txt");

            sw.WriteLine();
            sw.WriteLine("TABLE 1 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            List<string> lst_content = iApp.Tables.Get_Tables_Indian_Standard_Angles();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();

            sw.WriteLine();
            sw.WriteLine();

            //table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_1.txt");
            sw.WriteLine();
            sw.WriteLine("TABLE 2 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            lst_content = iApp.Tables.Get_Tables_EUDL_CDA();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();

            sw.WriteLine();
            sw.WriteLine();

            //table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_2.txt");

            sw.WriteLine();
            sw.WriteLine("TABLE 3 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            lst_content = iApp.Tables.Get_Tables_Allowable_Working_Stress_Critical();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();

            sw.WriteLine();

            //table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            sw.WriteLine();
            sw.WriteLine("TABLE 4 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            lst_content = iApp.Tables.Get_Tables_Allowable_Average_Shear_Stress();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();
            lst_content.Clear();

            sw.WriteLine();
            sw.WriteLine();

            table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");
            sw.WriteLine();
            sw.WriteLine("TABLE 5 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            lst_content = iApp.Tables.Get_Tables_Allowable_Working_Stress_Cross_Section();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();

            sw.WriteLine();
            sw.WriteLine();

           


        }
    }
}
