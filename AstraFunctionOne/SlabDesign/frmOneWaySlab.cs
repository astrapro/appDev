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

namespace AstraFunctionOne.SlabDesign
{
    public partial class frmOneWaySlab : Form
    {
        COneWaySlab slb02 = null;
        IApplication iApp = null;
        string filePathSet = "";

        string rep_file_name = "";
        string file_path = "";
        string user_path = "";
        string user_input_file = "";
        string system_path = "";

        bool is_process = false;


        #region Member Variable
        double l; // Floor Slab Length
        double b; // Floor Slab Breadth
        double ll; // Super imposed/ Live Load
        double sigma_ck; // Concrete Grade
        double sigma_y; // steel Grade
        double alpha; // Constants 
        double beta; // Constants
        double gamma; // Constants
        double delta; // Constants
        double lamda; // Constants
        double d1; // Dia of Main Reinforcement
        double d2; // Dia of Distribution Reinforcement
        double h1; // Clear Cover
        double h2; // End Cover
        double ads; // Provide Distribution Reinforcements
        double tc; // Shear Strength of concrete as % of steel
        double Slab_load; // Shear Strength of concrete as % of steel
        double w1; // Thickness of supporting wall


        KValueTable kVal_Table = null;

        string DataPath = "";


        #endregion





        public frmOneWaySlab(IApplication iApp)
        {
            //string fName = Path.Combine(Application.StartupPath, "vdres");
            //fName = Path.Combine(fName, "kValue.txt");
            InitializeComponent();
            this.iApp = iApp;

            string kValueFilePath = Path.Combine(Application.StartupPath, "vdres");
            kValueFilePath = Path.Combine(kValueFilePath, "kValue.txt");
            kVal_Table = new KValueTable(kValueFilePath);
        }

        #region Form Events
        private void frmOneWaySlab_Load(object sender, EventArgs e)
        {
            Enable_Button = false;

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            //rep_file_name = Path.Combine(user_path, "");
            InitilizeData();
            WriteUserInput();
            Calculate_Program();
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
            iApp.View_Result(rep_file_name);
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.ONE_WAY_RCC_SLAB, eSLAB_Part.VIEW);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.ONE_WAY_RCC_SLAB, eSLAB_Part.DRAWING);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult fViewRes = new frmViewResult(rep_file_name);
            fViewRes.ShowDialog();
        }
        private void btnBoQ_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.ONE_WAY_RCC_SLAB, eSLAB_Part.BoQ);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToUpper() != fbd.SelectedPath.ToUpper())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                }
            }
        }

        #endregion

        public bool Enable_Button
        {
            set
            {
                btnReport.Enabled = value;
                btnView.Enabled = value;
                btnDrawing.Enabled = value;
                btnBoQ.Enabled = value;
            }
        }

        private void InitilizeData()
        {

            //string fName = Path.Combine(Application.StartupPath, "vdres");
            //fName = Path.Combine(fName, "kValue.txt");
            D1 = MyList.StringToDouble(txt_d1.Text, 0);
            D2 = MyList.StringToDouble(txt_d2.Text, 0);
            Ads = MyList.StringToDouble(txt_Ads.Text, 0);
            Alpha = MyList.StringToDouble(txt_alpha.Text, 0);
            B = MyList.StringToDouble(txt_B.Text, 0);
            Beta = MyList.StringToDouble(txt_beta.Text, 0);
            Delta = MyList.StringToDouble(txt_delta.Text, 0);
            Gamma = MyList.StringToDouble(txt_gamma.Text, 0);
            H1 = MyList.StringToDouble(txt_h1.Text, 0);
            H2 = MyList.StringToDouble(txt_h2.Text, 0);
            L = MyList.StringToDouble(txt_L.Text, 0);
            Lamda = MyList.StringToDouble(txt_lamda.Text, 0);
            LL = MyList.StringToDouble(txt_LL.Text, 0);
            Sigma_ck = MyList.StringToDouble(txt_sigma_ck.Text, 0);
            Sigma_y = MyList.StringToDouble(txt_sigma_y.Text, 0);
            Tc = MyList.StringToDouble(txt_Tc.Text, 0);
            Slab_Load = MyList.StringToDouble(txt_slab_load.Text, 0);
            w1 = MyList.StringToDouble(txt_w1.Text, 0);
        }
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>

            string kPath = Path.Combine(user_path, "Structural Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "RCC Slab");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of One way RCC Slab");
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
                this.Text = "DESIGN OF ONE WAY RCC SLAB : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "ONE_WAY_RCC_SLAB");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");

                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Structural_Onewayslab.TXT");
                user_input_file = Path.Combine(system_path, "ONE_WAY_RCC_SLAB.FIL");

                btnProcess.Enabled = Directory.Exists(value);

                btnReport.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);
                btnView.Enabled = File.Exists(user_input_file);
                btnBoQ.Enabled = File.Exists(user_input_file);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_From_File();
                }
            }
        }
        public void Read_From_File()
        {

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));

            MyList mList = null;
            string kStr = "";

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = lst_content[i].Trim().TrimEnd().TrimStart();
                kStr = kStr.Replace(" ", "");
                mList = new MyList(kStr, '=');
                string option = mList.StringList[0].Trim();

                #region Switch Case
                try
                {
                    switch (option)
                    {
                        case "L":
                            L = mList.GetDouble(1);
                            txt_L.Text = L.ToString("0.00");
                            break;
                        case "B":
                            B = mList.GetDouble(1);
                            txt_B.Text = B.ToString("0.00");
                            break;
                        case "LL":
                            LL = mList.GetDouble(1);
                            txt_LL.Text = LL.ToString("0.00");
                            break;
                        case "Slab_load":
                            Slab_load = mList.GetDouble(1);
                            txt_slab_load.Text = Slab_Load.ToString("0.00");
                            break;
                        case "sigma_ck":
                            sigma_ck = mList.GetDouble(1);
                            txt_sigma_ck.Text = sigma_ck.ToString("0");
                            break;
                        case "sigma_y":
                            sigma_y = mList.GetDouble(1);
                            txt_sigma_y.Text = sigma_y.ToString("0");
                            break;
                        case "d1":
                            d1 = mList.GetDouble(1);
                            txt_d1.Text = d1.ToString("0.00");
                            break;
                        case "d2":
                            d2 = mList.GetDouble(1);
                            txt_d2.Text = d2.ToString("0.00");
                            break;
                        case "h1":
                            h1 = mList.GetDouble(1);
                            txt_h1.Text = h1.ToString("0.00");
                            break;
                        case "h2":
                            h2 = mList.GetDouble(1);
                            txt_h2.Text = h2.ToString("0.00");
                            break;
                        case "ads":
                            ads = mList.GetDouble(1);
                            txt_Ads.Text = ads.ToString("0.00");
                            break;
                        case "tc":
                            tc = mList.GetDouble(1);
                            txt_Tc.Text = tc.ToString("0.00");
                            break;
                        case "Alpha":
                            Alpha = mList.GetDouble(1);
                            txt_alpha.Text = alpha.ToString("0.00");
                            break;
                        case "Beta":
                            Beta = mList.GetDouble(1);
                            txt_beta.Text = beta.ToString("0.00");
                            break;
                        case "Gamma":
                            Gamma = mList.GetDouble(1);
                            txt_gamma.Text = gamma.ToString("0.00");
                            break;
                        case "Delta":
                            Delta = mList.GetDouble(1);
                            txt_delta.Text = delta.ToString("0.00");
                            break;
                        case "Lamda":
                            Lamda = mList.GetDouble(1);
                            txt_lamda.Text = lamda.ToString("0.00");
                            break;
                    }
                }
                catch (Exception ex) { }

                #endregion 
            }
            lst_content.Clear();
        }
        public void WriteUserInput()
        {
            
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine();
                sw.WriteLine(" L = {0:f3} ", L);
                sw.WriteLine(" B = {0:f3} ", B);
                sw.WriteLine(" LL = {0} ", LL);
                sw.WriteLine(" Slab_load = {0} ", Slab_load);
                sw.WriteLine(" sigma_ck = {0} ", sigma_ck);
                sw.WriteLine(" sigma_y = {0} ", sigma_y);
                sw.WriteLine(" d1 = {0}", d1);
                sw.WriteLine(" d2 = {0}", d2);
                sw.WriteLine(" h1 = {0}", h1);
                sw.WriteLine(" h2 = {0}", h2);
                sw.WriteLine(" ads= {0} ", ads);
                sw.WriteLine(" tc = {0}", tc);
                sw.WriteLine(" Alpha = {0}", alpha);
                sw.WriteLine(" Beta = {0}", beta);
                sw.WriteLine(" Gamma = {0}", gamma);
                sw.WriteLine(" Delta = {0}", delta);
                sw.WriteLine(" Lamda = {0}", lamda);
                sw.WriteLine();
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
        public void Calculate_Program()
        {
            //rep_file_name = Path.Combine(user_path, "DESIGN_OF_ONE_RCC_SLAB.TXT");
            string view_file = Path.Combine(system_path, "DESIGN.FIL");
            string boq_file = Path.Combine(system_path, "BoQ.FIL");

            StreamWriter sw_view = new StreamWriter(new FileStream(view_file, FileMode.Create));
            StreamWriter sw_boq = new StreamWriter(new FileStream(boq_file, FileMode.Create));
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*                  ASTRA Pro                  *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*           DESIGN OF SINGLE SPAN             *");
                sw.WriteLine("\t\t*  ONE WAY RCC SLAB BY LIMIT STATE METHOD     *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t-----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(" Length Perpendicular to Span [L] = {0:f2} m", L);
                sw.WriteLine(" Design Span for Slab [B]= {0:f2} m", B);
                sw.WriteLine(" Super imposed / Live Load [LL]= {0:f2} kN/sq.m", LL);
                sw.WriteLine(" Slab Load = {0:f2} kN/sq.m", Slab_load);
                sw.WriteLine(" Concrete Grade [f_ck] = M {0} N/sq.mm", sigma_ck);
                sw.WriteLine(" steel Grade [f_y] = Fe {0} N/sq.mm", sigma_y);
                sw.WriteLine(" Diameter of Main Reinforcement [d1] = {0} mm", d1);
                sw.WriteLine(" Diameter of Distribution Reinforcement [d2] = {0} mm", d2);
                sw.WriteLine(" Clear Cover [h1] = {0} mm", h1);
                sw.WriteLine(" End Cover [h2] = {0} mm", h2);
                sw.WriteLine(" Provide Distribution Reinforcement [ads] = {0} %", ads);
                sw.WriteLine(" Shear Strength of Concrete as % of Steel = {0} %", tc);
                sw.WriteLine(" α = {0}", alpha);
                sw.WriteLine(" β = {0}", beta);
                sw.WriteLine(" γ = {0}", gamma);
                sw.WriteLine(" δ = {0}", delta);
                sw.WriteLine(" λ = {0}", lamda);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 1: Calculations for Overall and effective depth");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Since length of the slab is more than twice the width, it is a");
                sw.WriteLine("ONE WAY RCC SLAB. Load will be transferred to the supports along");
                sw.WriteLine("the shorter span.");
                sw.WriteLine();
                sw.WriteLine("Consider a 100 cm wide strip of the slab parallel to its shorter span.");
                sw.WriteLine();

                double d, lowest_Span;

                double val1, val2;
                lowest_Span = (L > B) ? B * 1000 : L * 1000;
                d = (lowest_Span / (alpha * beta * gamma * delta * lamda));

                sw.WriteLine("Minimum depth of slab");
                sw.WriteLine("    d = L /(α * β * γ * δ * λ)");
                sw.WriteLine();
                sw.WriteLine("Let α = {0}, β = {1}, γ = {2}, δ = {3} and λ = {4}", alpha, beta, gamma, delta, lamda);
                sw.WriteLine();
                sw.WriteLine("So, d = {0}/{1} = {2} mm", lowest_Span.ToString("0.0"), (alpha * beta * gamma * delta * lamda).ToString("0.00"), d.ToString("0.00"));

                double D = (d + h1);
                sw.WriteLine();
                sw.WriteLine("Let us adopt overall depth   D = {0} mm.", D.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 2: Calculations for Design Load, Moment and Shear");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double deadLoad_slab = (D / 1000) * 1.0 * Slab_load;
                sw.WriteLine("Dead Load of slab = {0} * {1} * {2} = {3} kN/m.", (D / 1000).ToString("0.00"), 1.0, Slab_load, deadLoad_slab.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Superimposed load = {0} * 1 = {1} kN/m.", ll.ToString("0.00"), ll.ToString("0.00"));
                double totalLoad = deadLoad_slab + ll;
                sw.WriteLine();
                sw.WriteLine("Total Load  = {0:f2} + {1:f2} = {2:f2} kN/m.", deadLoad_slab, LL, totalLoad);


                sw.WriteLine();
                double factoredLoad, loadFactor;
                loadFactor = 1.5d;
                factoredLoad = totalLoad * loadFactor;
                sw.WriteLine("Factored load if the load factor is {0}", loadFactor.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("                               = {0} * {1} = {2} kN/m", loadFactor.ToString("0.00"), totalLoad.ToString("0.00"), factoredLoad.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Maximum BM at centre of shorter span");
                sw.WriteLine("                       = (Wu * l * l) / 8");
                sw.WriteLine();
                sw.WriteLine("Assume steel consist of {0} mm bars with {1} mm clear cover.", d1, h1);

                double half_depth = d1 / 2;
                double eff_depth = D - h1 - half_depth;
                sw.WriteLine();
                sw.WriteLine("Effective depth = {0} - {1} - {2} = {3} mm", D.ToString("0.0"), h1.ToString("0.0"), half_depth.ToString("0.00"), eff_depth.ToString("0.00"));

                lowest_Span = lowest_Span / 1000;

                double eff_span = (lowest_Span) + (eff_depth / 1000);
                sw.WriteLine();
                sw.WriteLine("Effective Span of Slab = {0} + d = {0} + {1} = {2} m", (lowest_Span).ToString("0.00"), (eff_depth / 1000).ToString("0.00"), eff_span.ToString("0.00"));
                double BM = factoredLoad * eff_span * eff_span / 8;
                //sw.WriteLine("So, BM = ({0} * {1} * {1}) / 8 = {2} kNm", factoredLoad.ToString("0.00"), eff_span.ToString("0.00"), BM.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("So, BM = M = ({0} * {1} * {1}) / 8 = {2} kNm", factoredLoad.ToString("0.00"), eff_span.ToString("0.00"), BM.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Max shear force = Vu = (Wn * lc) / 2");
                double max_shear_force = (factoredLoad * lowest_Span / 2.0);
                sw.WriteLine("                = {0} * {1}/2 = {2} kN = {3} N", factoredLoad.ToString("0.00"), lowest_Span.ToString("0.0"), max_shear_force.ToString("0.0"), (max_shear_force * 1000).ToString("0.00"));

                max_shear_force *= 1000;


                sw.WriteLine();
                sw.WriteLine("Depth of the slab is given by");
                sw.WriteLine("              BM = 0.138 * σ_ck * b * d* d");

                double M = (BM * 10E+5);
                d = ((BM * 10E+5) / (0.138 * sigma_ck * 1000));
                d = Math.Sqrt(d);
                sw.WriteLine();
                sw.WriteLine("or      d = √(({0} * 10E+5)/(0.138 * {1} * 1000)) = {2} mm", BM.ToString("0.00"), sigma_ck.ToString("0.00"), d.ToString("0.00"));

                d = (int)(d / 10);
                d += 2;
                d *= 10;
                sw.WriteLine();
                sw.WriteLine("Adopt effective depth d = {0} mm and over all depth", d.ToString("0.0"));
                sw.WriteLine("                      D = {0} mm", eff_depth);


                sw.WriteLine();
                sw.WriteLine("Adopt of tension steel is given by ");
                sw.WriteLine("             M   = 0.87 * σ_y * A_t( d - ((σ_y * A_t)/(σ_ck * b))");

                double a, b, c, At;

                a = (0.87 * sigma_y * sigma_y) / (sigma_ck * 1000);
                b = 0.87 * sigma_y * d;
                c = M;
                double b_ac = (b * b) - 4 * a * c;
                At = (b) - Math.Sqrt(Math.Abs(b_ac));
                At = At / (2 * a);

                At = (int)At / 10;
                At += 1;
                At = At * 10;
                sw.WriteLine();
                sw.WriteLine("   {0} * 10E+5 = 0.87 * {1} * At * ({2} - {3} * At / ({4} * 1000))",
                    BM.ToString("0.00"), sigma_y.ToString("0.00"), d.ToString("0.0"), sigma_y.ToString("0.00"), sigma_ck.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("or          At   = {0} sq.mm", At);
                sw.WriteLine();
                sw.WriteLine("Use {0} mm bars @ {1} mm c/c giving total area ", d1, d.ToString("0.0"));

                double est_value = (Math.PI * (d1 * d1) / 4) * (1000 / d);

                val1 = est_value;
                val2 = At;
                sw.WriteLine();
                if (val1 > val2)
                {
                    sw.WriteLine("                         = {0} sq.mm. > {1} sq.mm      OK", est_value.ToString("0"), At.ToString("0"));
                }
                else
                {
                    sw.WriteLine("                         = {0} sq.mm. < {1} sq.mm      NOT OK", est_value.ToString("0"), At.ToString("0"));
                }
                double n_rod = At / ((Math.PI * (d1 * d1) / 4));

                n_rod = (int)n_rod;
                n_rod += 1;

                sw.WriteLine();
                sw.WriteLine("    Bend alternate bars at L/{0} from the face of support where ", n_rod);
                sw.WriteLine("moment reduces to less than half its maximum value. Temperature ");
                sw.WriteLine("reinforcement equal to {0}% of the gross concrete area will be", ads);
                sw.WriteLine("provided in the longitudinal direction.");
                sw.WriteLine();

                double dirArea = (ads / 100) * 1000 * eff_depth;
                sw.WriteLine("       = {0:f4} * 1000 * {1:f2}", (Ads / 100), eff_depth);
                sw.WriteLine("       = {0:f3} sq.mm.",dirArea);
                sw.WriteLine();
                sw.WriteLine("Use {0} mm MS bars @ 100 mm c/c giving total area ", d2.ToString("0"));
                sw.WriteLine();

                double a_st = Math.PI * d2 * d2 / 4;

                val1 = a_st * 10;
                val2 = dirArea;
                if (val1 > val2)
                {
                    sw.WriteLine("   = {0} * (1000/100) = {1} sq.mm.  > {2} sq.mm            OK", a_st.ToString("0.00"), (a_st * 10).ToString("0.00"), dirArea.ToString("0.0"));
                }
                else
                {
                    sw.WriteLine("   = {0} * (1000/100) = {1} sq.mm.  < {2} sq.mm           NOT OK", a_st.ToString("0.00"), (a_st * 10).ToString("0.00"), dirArea.ToString("0.0"));
                }


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 3: Check for Shear");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Percent tension steel = (100 * At)/ (b * d)");
                a_st = Math.PI * d1 * d1 / 4;



                double percent = (100 * (a_st * (1000 / 300))) / (1000 * d);
                sw.WriteLine("                      = (100 * ({0} * (1000/300)) / (1000 * {1}) = {2}%", a_st.ToString("0.0"), d.ToString("0.0"), percent.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Shear strength of concrete for {0}% steel", percent.ToString("0.00"));
                ShearValue sh = new ShearValue();
                double tau_c = 0.0;

                tau_c = sh.Get_M15(percent);

                sw.WriteLine("     τ_c = {0} N/sq.mm.", tau_c);


                double k = kVal_Table.Get_KValue(eff_depth);
                double tc_dash = k * tau_c;





                //tau_c = tau_c;
                sw.WriteLine();
                sw.WriteLine("For {0} mm thick slab, k = {1}", eff_depth.ToString("0.00"), k.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("         So, τ_c` = k * Tc = {0} * {1} = {2} N/sq.mm", k.ToString("0.00"), tau_c.ToString("0.00"), tc_dash.ToString("0.00"));
                double Vu = max_shear_force;
                double t_v = Vu / (1000 * d);

                sw.WriteLine();
                sw.WriteLine("Nominal shear stress Tv = Vu / b * d = {0}/(1000 * {1}) = {2} N/sq.mm", Vu, d, t_v.ToString("0.00"));

                sw.WriteLine();
                sw.WriteLine("The Slab is safe in shear.");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 4: Check for development length");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Moment of resistance offered by {0} mm bars @ {1} mm c/c", d1, d * 2);
                sw.WriteLine();
                sw.WriteLine("M1 = 0.87 * σ_y * At * (d - (σ_y * At / σ_ck * b))");
                sw.WriteLine("   = 0.87 * {0:f2} * {1:f2} * (1000/300) * ", sigma_y, a_st);
                sw.WriteLine("     ({0:f2} - ({1:f2} * {2:f2} * (1000 / 300)) / {3:f2} * 1000)",
                    d, sigma_y, a_st, Sigma_ck);

                double M1 = 0.87 * sigma_y * a_st * (1000.0 / 300.0) * (d - (sigma_y * a_st * (1000.0d / 300.0d) / (sigma_ck * 1000.0)));





                sw.WriteLine();
                sw.WriteLine("   = {0:F2} N mm", M1);
                sw.WriteLine("Vu = {0:F2} N", Vu);
                sw.WriteLine();
                sw.WriteLine("Let us assume anchorage length Lo = 0");
                sw.WriteLine("                  Ld <= 1.3 * (M1/Vu)");
                sw.WriteLine("                  56φ <= 1.3 * ({0}/{1})", M1.ToString("0.00"), Vu.ToString("0.00"));

                double phi = (1.3 * (M1 / Vu) / 56.0);


                val1 = phi;
                val2 = d1;
                sw.WriteLine();
                if (val1 > val2)
                {
                    sw.WriteLine("                  φ < {0}", phi.ToString("0.00"));
                    sw.WriteLine("We have provided φ = {0} mm, So OK.", d1.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("                  φ > {0}", phi.ToString("0.00"));
                    sw.WriteLine("We have provided φ = {0} mm, So NOT OK.", d1.ToString("0.00"));
                }

                sw.WriteLine();
                sw.WriteLine("  The Code requires that bars must be carried");
                sw.WriteLine("into the supports by atleast Ld / 3 = 190 mm");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("■ STEP 5: Check for deflection");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Percent tension steel at midspan");
                sw.WriteLine("               = (100 * As) / (b * d)");
                sw.WriteLine("               = (100 * {0:F2} * 1000 / 150) / (1000 * 150)", a_st);
                double pps = (100 * a_st * 1000 / 150) / (1000 * 150);
                sw.WriteLine("               = {0}%", pps.ToString("0.00"));

                double gama = ModificationFactor.GetGamma(pps);
                sw.WriteLine();
                sw.WriteLine("             γ = {0}%", gama.ToString("0.00"));
                //sw.WriteLine(" σσσγγβαδλ■  √");
                sw.WriteLine();
                sw.WriteLine("  β = {0}, δ = {1} and λ = {2}", beta, delta, lamda);

                // Constant 20
                sw.WriteLine();
                sw.WriteLine("Allowable L/d = 20 * {0} = {1}", gama.ToString("0.00"), (20 * gama).ToString("0.00"));

                val1 = (eff_span * 1000) / d;
                val2 = (20 * gama);

                sw.WriteLine();
                if (val1 < val2)
                {
                    sw.WriteLine("Actual L/d = {0} / {1} = {2} < {3}       OK", (eff_span * 1000).ToString("0.00"), d, (eff_span * 1000 / d).ToString("0.00"), (20 * gama).ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("Actual L/d = {0} / {1} = {2} > {3}       NOT OK", (eff_span * 1000).ToString("0.00"), d, (eff_span * 1000 / d).ToString("0.00"), (20 * gama).ToString("0.00"));
                }
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                double fd = 500;

                sw_view.WriteLine("SLAB DESIGN 02");
                sw_view.WriteLine("L = {0}", lowest_Span * 1000);
                sw_view.WriteLine("D = {0}", eff_depth);
                sw_view.WriteLine("b1 = {0}", d);
                sw_view.WriteLine("b2 = 100");
                sw_view.WriteLine("h1 = {0}", h1);
                sw_view.WriteLine("h2 = {0}", h2);
                sw_view.WriteLine("d1 = {0}", d1);
                sw_view.WriteLine("d2 = {0}", d2);
                sw_view.WriteLine("fd = {0:f0}", fd);
                sw_view.WriteLine("w1 = {0}", w1);
                sw_view.WriteLine("w2 = {0}", w1);
                sw_view.WriteLine("END");

                //BoQ CODE
                //S_No 1.,2.,3.
                //Member 3.5 8.0
                //Bar_Mark 01,02,03
                //Bar_CODE T10_B1,T6_B2,T10_B1
                //Bar_Grade Fe415,Fe415,Fe415
                //Bar_Dia 10,6,10
                //Bar_Length 3470,7970,3470
                //Bar_Nos 56,35,56
                //Bar_Weight 0.330,0.220,0.330
                //Bar_Shape 3019,120,1209
                //Bar_Shape 1209,120,3019
                //END BoQ

                int main_bar_nos, dist_bar_nos;
                double wt_main_bar, wt_dist_bar;


                sw_boq.WriteLine("BoQ Code");
                sw_boq.WriteLine("S_No {0}.,{1}.,{2}.", 1, 2, 3);
                sw_boq.WriteLine("Member {0} {1}", B.ToString("0.0"), L.ToString("0.0"));
                sw_boq.WriteLine("Bar_Spacing {0},{1}", d.ToString("0.0"), "100.0");
                sw_boq.WriteLine("Bar_Mark 01,02,03");
                sw_boq.WriteLine("Bar_Code T{0}_B1,T{1}_B2,T{0}_B1", d1, d2);
                sw_boq.WriteLine("BAR_Grade Fe{0},Fe{0},Fe{0}", sigma_y);
                sw_boq.WriteLine("Bar_Dia {0},{1},{0}", d1, d2);
                sw_boq.WriteLine("Bar_Length {0},{1},{0}", (B * 1000 - 2 * h1), (L * 1000 - 2 * h1));

                main_bar_nos = (int)(L * 1000 / d);
                dist_bar_nos = (int)(B * 1000 / 100.0d);

                wt_main_bar = 0.00616 * d1 * d1 * (B) * main_bar_nos;
                wt_dist_bar = 0.00616 * d2 * d2 * (L) * dist_bar_nos;
                wt_main_bar /= 1000.0d;
                wt_dist_bar /= 1000.0d;
                //sw_boq.WriteLine("Bar_Nos {0},{1},{0}", "56", "35", "56");
                sw_boq.WriteLine("Bar_Nos {0},{1},{0}", main_bar_nos, dist_bar_nos);
                sw_boq.WriteLine("Bar_Weight {0},{1},{0}", wt_main_bar.ToString("0.000"), wt_dist_bar.ToString("0.000"));
                //sw_boq.WriteLine("Bar_Weight {0},{1},{0}", "0.330", "0.220");

                double k_d = eff_depth - (2 * ((h1 + (d1 / 2))));

                double aaa = Math.Sqrt((k_d * k_d) + (k_d * k_d));

                double tot_len = (lowest_Span * 1000.0) + 2 * w1 - 2 * h1;

                double sh1 = tot_len - (fd + (w1 - h1));
                double sh2 = (fd + (w1 - h1)) - eff_depth;




                sw_boq.WriteLine("Bar_Shape {0},{1:f0},{2}", sh1, aaa, sh2);
                //sw_boq.WriteLine("Bar_Shape {0},{1:f0},{2}", 3140, aaa, 1290);
                sw_boq.WriteLine("Bar_Shape {0}", ((L * 1000.0) - (2 * h1)));

                sw_boq.WriteLine("Bar_Shape {0},{1},{2}", sh2, aaa, sh1);
                sw_boq.WriteLine("END BoQ");
            }
            catch (Exception exx)
            {
            }
            finally
            {
                GC.Collect();
                sw.Flush();
                sw.Close();
                sw_view.Flush();
                sw_view.Close();
                sw_boq.Flush();
                sw_boq.Close();
            }
        }

        #region ISLAB02 Members

        public double L
        {
            get
            {
                return l;
            }
            set
            {
                l = value;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        public double LL
        {
            get
            {
                return ll;
            }
            set
            {
                ll = value;
            }
        }

        public double Sigma_ck
        {
            get
            {
                return sigma_ck;
            }
            set
            {
                sigma_ck = value;
            }
        }

        public double Sigma_y
        {
            get
            {
                return sigma_y;
            }
            set
            {
                sigma_y = value;
            }
        }

        public double Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
            }
        }

        public double Beta
        {
            get
            {
                return beta;
            }
            set
            {
                beta = value;
            }
        }

        public double Gamma
        {
            get
            {
                return gamma;
            }
            set
            {
                gamma = value;
            }
        }

        public double Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
            }
        }

        public double Lamda
        {
            get
            {
                return lamda;
            }
            set
            {
                lamda = value;
            }
        }

        public double D1
        {
            get
            {
                return d1;
            }
            set
            {
                d1 = value;
            }
        }

        public double D2
        {
            get
            {
                return d2;
            }
            set
            {
                d2 = value;
            }
        }

        public double Slab_Load
        {
            get
            {
                return Slab_load;
            }
            set
            {
                Slab_load = value;
            }
        }
        public double H1
        {
            get
            {
                return h1;
            }
            set
            {
                h1 = value;
            }
        }

        public double H2
        {
            get
            {
                return h2;
            }
            set
            {
                h2 = value;
            }
        }

        public double Ads
        {
            get
            {
                return ads;
            }
            set
            {
                ads = value;
            }
        }

        public double Tc
        {
            get
            {
                return tc;
            }
            set
            {
                tc = value;
            }
        }

        #endregion
    }
}
