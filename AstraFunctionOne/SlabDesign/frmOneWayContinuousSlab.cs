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
    public partial class frmOneWayContinuousSlab : Form,ISLAB03
    {
        string lenStr = "";
        List<double> lengths = null;

        #region Variable Declaration
        double d_Do, d_SL, d_Fo, d_FL, d_DF, d_LL, d_LF, d_d1, d_d2;
        double d_Dp, d_sigma_ck, d_sigma_y, d_alpha, d_beta, d_gamma;
        double d_delta, d_lamda, d_w1,d_h1;
        double d_b;
        IApplication iApp = null;
        #endregion

        string rep_file_name = "";
        string file_path = "";
        string user_input_file = "";
        string filePathSet = null;
        string system_path = "";
        string user_path = "";
        KValueTable kV = null;



        bool is_process = false;

        public frmOneWayContinuousSlab(IApplication app)
        {
            iApp = app;
            InitializeComponent();
            lengths = new List<double>();
            d_Do = 0.0d; 
            d_SL = 0.0d; 
            d_Fo = 0.0d; 
            d_FL = 0.0d; 
            d_DF = 0.0d; 
            d_LL = 0.0d; 
            d_LF = 0.0d; 
            d_d1 = 0.0d; 
            d_d2 = 0.0;
            d_Dp = 0.0d; 
            d_sigma_ck = 0.0d; 
            d_sigma_y = 0.0d; 
            d_alpha = 0.0d; 
            d_beta = 0.0d; 
            d_gamma = 0.0;
            d_delta = 0.0d; 
            d_lamda = 0.0d;
            d_w1 = 0.0d;
            d_b = 0.0d;

            string kval = Path.Combine(Application.StartupPath, "vdres\\kvalue.txt");
            kV = new KValueTable(kval);

        }
        
        #region ISLAB03 Members

        public string LengthString
        {
            get
            {
                return lenStr;
            }
            set
            {
                lenStr = value;
                string str = lenStr.Replace(';', ' ').Replace(',', ' ').Trim().TrimEnd().TrimStart().ToUpper();
                MyList mList = new MyList(MyList.RemoveAllSpaces(str), ' ');
                lengths.Clear();
                for (int i = 0; i < mList.Count; i++)
                {
                    try
                    {
                        lengths.Add(mList.GetDouble(i));
                    }
                    catch (Exception exx)
                    {
                    }
                }
            }
        }

        public List<double> Lengths
        {
            get
            {
                return lengths;
            }
            set
            {
                lengths = value;
            }
        }

        public double AssumeSlabThickness
        {
            get
            {
                return d_Do;
            }
            set
            {
                d_Do = value;
            }
        }

        public double SlabLoad
        {
            get
            {
                return d_SL;
            }
            set
            {
                d_SL = value;
            }
        }

        public double FinishThickness
        {
            get
            {
                return d_Fo;
            }
            set
            {
                d_Fo = value;
            }
        }

        public double FinishLoad
        {
            get
            {
                return d_FL;
            }
            set
            {
                d_FL = value;
            }
        }

        public double FixedLoadfactor
        {
            get
            {
                return d_DF;
            }
            set
            {
                d_DF = value;
            }
        }

        public double ImposedLoad
        {
            get
            {
                return d_LL;
            }
            set
            {
                d_LL = value;
            }
        }

        public double ImposedLoadFactor
        {
            get
            {
                return d_LF;
            }
            set
            {
                d_LF = value;
            }
        }

        public double DiaMainReinforcement
        {
            get
            {
                return d_d1;
            }
            set
            {
                d_d1 = value;
            }
        }

        public double DiaDistributionReinforcement
        {
            get
            {
                return d_d2;
            }
            set
            {
                d_d2 = value;
            }
        }

        public double PercentageDistReinforcement
        {
            get
            {
                return d_Dp;
            }
            set
            {
                d_Dp = value;
            }
        }

        public double ConcreteGrade
        {
            get
            {
                return d_sigma_ck;
            }
            set
            {
                d_sigma_ck = value;
                
            }
        }

        public double SteelGrade
        {
            get
            {
                return d_sigma_y;
            }
            set
            {
                d_sigma_y = value;
            }
        }

        public double Alpha
        {
            get
            {
                return d_alpha;
            }
            set
            {
                d_alpha = value;
            }
        }


        public double Beta
        {
            get
            {
                return d_beta;
            }
            set
            {
                d_beta = value;
            }
        }

        public double Gamma
        {
            get
            {
                return d_gamma;
            }
            set
            {
                d_gamma= value;
            }
        }

        public double Delta
        {
            get
            {
                return d_delta;
            }
            set
            {
                d_delta = value;
            }
        }

        public double Lamda
        {
            get
            {
                return d_lamda;
            }
            set
            {
                d_lamda = value;
            }
        }

        public double WallThickness
        {
            get
            {
                return d_w1;
            }
            set
            {
                d_w1 = value;
            }
        }

        bool ISLAB03.Calculate(string fileName)
        {
            throw new NotImplementedException();
        }
        public double Perpendicular_Length
        {
            get
            {
                return d_b;
            }
            set
            {
                d_b = value;
            }
        }
        public double ClearCover
        {
            get
            {
                return d_h1;
            }
            set
            {
                d_h1 = value;
            }
        }

        #endregion

        #region Form Events
        private void frmSLAB03_Load(object sender, EventArgs e)
        {
            Enable_Button = false;

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program();
            MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            is_process = true;
            FilePath = user_path;
            iApp.View_Result(rep_file_name);

        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult viewResult = new frmViewResult(rep_file_name);
            viewResult.Show();
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.ONE_WAY_CONTINUOUS_RCC_SLAB, eSLAB_Part.VIEW);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.ONE_WAY_CONTINUOUS_RCC_SLAB, eSLAB_Part.DRAWING);
        }
        private void btnBoQ_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(system_path, eSLAB.ONE_WAY_CONTINUOUS_RCC_SLAB, eSLAB_Part.BoQ);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path != "" && Path.GetDirectoryName(user_path).ToUpper() == fbd.SelectedPath.ToUpper())
                {
                    return;
                }
                is_process = false;
                FilePath = fbd.SelectedPath;
            }
        }
        #endregion

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
            kPath = Path.Combine(kPath, "Design of One way Continuous RCC Slab");
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

                this.Text = "DESIGN OF ONE WAY CONTINUOUS RCC SLAB : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "ONE_WAY_CONTINUOUS_RCC_SLAB");
                if (!Directory.Exists(file_path))
                {
                    Directory.CreateDirectory(file_path);
                }
                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Structural_Oneway_Continuous_slab.TXT");
                user_input_file = Path.Combine(system_path, "ONE_WAY_CONTINUOUS_RCC_SLAB.FIL");

                btnProcess.Enabled = Directory.Exists(user_path);
                btnReport.Enabled = File.Exists(user_input_file);
                btnView.Enabled = File.Exists(user_input_file);
                btnDrawing.Enabled = File.Exists(user_input_file);
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
        public void InitializeData()
        {
            try
            {
                LengthString = txt_L.Text;
                d_Do = MyList.StringToDouble(txt_Do.Text, 0.0);
                d_b = MyList.StringToDouble(txt_b.Text, 0.0);
                d_SL = MyList.StringToDouble(txt_SL.Text, 0.0);
                d_Fo = MyList.StringToDouble(txt_Fo.Text, 0.0);
                d_FL = MyList.StringToDouble(txt_FL.Text, 0.0);
                d_DF = MyList.StringToDouble(txt_DF.Text, 0.0);
                d_LL = MyList.StringToDouble(txt_LL.Text, 0.0);
                d_LF = MyList.StringToDouble(txt_LF.Text, 0.0);
                d_d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
                d_d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
                d_Dp = MyList.StringToDouble(txt_Dp.Text, 0.0);
                d_sigma_ck = MyList.StringToDouble(txt_sigma_ck.Text, 0.0);
                d_sigma_y = MyList.StringToDouble(txt_sigma_y.Text, 0.0);
                d_alpha = MyList.StringToDouble(txt_alpha.Text, 0.0);
                d_beta = MyList.StringToDouble(txt_beta.Text, 0.0);
                d_gamma = MyList.StringToDouble(txt_gamma.Text, 0.0);
                d_delta = MyList.StringToDouble(txt_delta.Text, 0.0);
                d_lamda = MyList.StringToDouble(txt_lamda.Text, 0.0);
                d_w1 = MyList.StringToDouble(txt_wall_thickness.Text, 0.0);
                d_h1 = MyList.StringToDouble(txt_h1.Text, 0.0);
            }
            catch (Exception exx)
            {
            }
        }
        public void Calculate_Program()
        {

            string design_file = Path.Combine(system_path, "DESIGN.FIL");
            string boq_file = Path.Combine(system_path, "BoQ.FIL");

            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            StreamWriter sw_design = new StreamWriter(new FileStream(design_file, FileMode.Create));
            StreamWriter sw_boq = new StreamWriter(new FileStream(boq_file, FileMode.Create));

            try
            {
                #region Write Report
                sw.WriteLine("\t\t*******************************************************");
                sw.WriteLine("\t\t*                ASTRA Pro Release 21                 *");
                sw.WriteLine("\t\t*       TechSOFT Engineering Services (I) Pvt. Ltd.   *");
                sw.WriteLine("\t\t*                                                     *");
                sw.WriteLine("\t\t*                  DESIGN OF SINGLE SPAN              *");
                sw.WriteLine("\t\t*  ONE WAY CONTINUOUS RCC SLAB BY LIMIT STATE METHOD  *");
                sw.WriteLine("\t\t*******************************************************");
                sw.WriteLine("\t\t-------------------------------------------------------");
                sw.WriteLine("\t\t    THIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t-------------------------------------------------------");
                //sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Span Length = {0} m", LengthString);
                sw.WriteLine("Assume Slab Thickness = {0} mm", AssumeSlabThickness);
                sw.WriteLine("Slab Load = {0} kN/sq.m", SlabLoad);
                sw.WriteLine("Finish Thickness = {0} mm", FinishThickness);
                sw.WriteLine("Finish Load = {0} kN/sq.m", FinishLoad);
                sw.WriteLine("Permanent Load Factor = {0}", FixedLoadfactor);
                sw.WriteLine("Imposed Load = {0} kN/sq.m", ImposedLoad);
                sw.WriteLine("Imposed Load Factor = {0}", ImposedLoadFactor);
                sw.WriteLine("Dia of Main Reinforcement = {0} mm", DiaMainReinforcement);
                sw.WriteLine("Dia of Distribution/Temperature Reinforcement = {0} mm", DiaDistributionReinforcement);
                sw.WriteLine("Percentage Distribution Reinforcement = {0} %", PercentageDistReinforcement.ToString("0.00"));
                sw.WriteLine("Grade of Concrete = M {0} N/sq.mm", ConcreteGrade);
                sw.WriteLine("Grade of Steel = Fe {0} N/sq.mm", SteelGrade);
                sw.WriteLine("α = {0}", Alpha);
                sw.WriteLine("β = {0}", Beta);
                sw.WriteLine("γ = {0}", Gamma);
                sw.WriteLine("δ = {0}", Delta);
                sw.WriteLine("λ = {0}", Lamda);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                
                double end_span = 0.0;
                double val = 0.0;
                if (Lengths.Count > 1)
                {
                    if (Lengths[0] > Lengths[Lengths.Count - 1])
                        end_span = Lengths[0];
                    else
                        end_span = Lengths[Lengths.Count - 1];
                }

                double mid_span = 0.0d;
                for (int i = 1; i < Lengths.Count - 1; i++)
                {
                    if (Lengths[i] > mid_span)
                        mid_span = Lengths[i];
                }


                sw.WriteLine();
                sw.WriteLine("Consider a 1m wide strip and assume overall thickness ");
                sw.WriteLine("of Slab as {0} mm. Owing to symmetry only half the slab ", d_Do);
                sw.WriteLine("need be considered.");
                sw.WriteLine();

                double DL = (d_Do / 1000.0) * 1.0 * d_SL;
                sw.WriteLine("   Dead load of slab (DL) = {0:f3} * 1.0 * {1:f2}", (d_Do / 1000.0), d_SL);
                sw.WriteLine("                          = {0} kN/m", DL);

                double thick_dead_load = (d_Fo / 1000.0) * d_FL;
                sw.WriteLine();
                sw.WriteLine("   Dead load of {0:f2} cm thick finish = {1:f3} * 1.0 * {2}", (d_Fo / 10.0), (d_Fo / 1000.0), d_FL);
                sw.WriteLine("                                     = {0:f3} kN/m", thick_dead_load);
                sw.WriteLine();

                double total_dead_load = DL + thick_dead_load;
                sw.WriteLine("   Total dead load = {0:f2} + {1:f2}", DL, thick_dead_load);
                sw.WriteLine("                   = {0:f2} kN/m", total_dead_load);

                double IL = d_LL * 1.0;
                sw.WriteLine();
                sw.WriteLine("   Imposed Load (IL) = {0} * 1.0 ", d_LL);
                sw.WriteLine("                     = {0:f2} kN/m", IL);

                double factored_dead_load = d_DF * total_dead_load;
                sw.WriteLine();
                sw.WriteLine("   Factored dead load  = {0:f2} * {1:f2}", d_DF, total_dead_load);
                sw.WriteLine("                       = {0:f2} kN/m", factored_dead_load);

                double v1 = factored_dead_load * Lengths[0];
                sw.WriteLine("                       = {0:f2} * {1:f2}", factored_dead_load, Lengths[0]);
                sw.WriteLine("                       = {0:f2} kN/m", v1);

                double factored_imposed_load = ImposedLoadFactor * ImposedLoad;
                sw.WriteLine();
                sw.WriteLine("   Factored imposed load = {0:f2} * {1:f2} ", ImposedLoadFactor, ImposedLoad);
                sw.WriteLine("                         = {0:f2} kN/m", factored_imposed_load);

                double v2 = factored_imposed_load * Lengths[0];
                sw.WriteLine();
                sw.WriteLine("                         = {0:f2} * {1:f2}", factored_imposed_load, Lengths[0]);
                sw.WriteLine("                         = {0} kN", v2);

                sw.WriteLine();
                sw.WriteLine("    Moment at End Span = (DL * l)/12 + (LL * l)/10");
                sw.WriteLine("                       = ({0} * {1})/12 + ({2} * {1})/10", v1, end_span, v2);
                double E_end_moment = ((v1 * end_span) / 12) + ((v2 * end_span) / 10);
                sw.WriteLine("                       = {0} kN/m", E_end_moment.ToString("0.00"));


                sw.WriteLine();
                sw.WriteLine("    Moment at Interior Span = (DL * l)/24 + (LL * l)/12");
                sw.WriteLine("                            = ({0} * {1})/24 + ({2} * {1})/12", v1, mid_span, v2);

                double F_interior_moment = ((v1 * mid_span) / 16) + ((v2 * mid_span) / 12);
                sw.WriteLine("                            = {0} kN/m", F_interior_moment.ToString("0.00"));

                sw.WriteLine();
                sw.WriteLine("    Moment at support next to end support");
                sw.WriteLine("          = (DL * l)/10 + (LL * l)/9");
                sw.WriteLine("          = ({0} * {1})/10 + ({2} * {1})/9", v1, end_span, v2);

                double B_end_support_moment = ((v1 * end_span) / 10) + ((v2 * end_span) / 9);
                sw.WriteLine("          = {0} kN/m", B_end_support_moment.ToString("0.00"));


                sw.WriteLine();
                double max_moment = 0.0;

                if (E_end_moment > F_interior_moment && E_end_moment > B_end_support_moment)
                    max_moment = E_end_moment;
                else if (F_interior_moment > E_end_moment && F_interior_moment > B_end_support_moment)
                    max_moment = F_interior_moment;
                else if (B_end_support_moment > E_end_moment && B_end_support_moment > F_interior_moment)
                    max_moment = B_end_support_moment;

                //sw.WriteLine("Maximum bending moments at critical sections are computed using bending moment");
                ////Table ?
                //sw.WriteLine("coefficients given in Table 11.1 and are tabulated in Table 14.2");

                sw.WriteLine();
                sw.WriteLine("    For a balanced design, Mu = 0.138 * σ_ck * b * d * d");

                double d = Math.Sqrt((max_moment * 10E+5) / (0.138 * d_sigma_ck * 1000));

                sw.WriteLine();
                sw.WriteLine("    d =√(({0} * 10E+5)/(0.138 * {1} * 1000))", max_moment.ToString("0.00"), ConcreteGrade.ToString("0"));
                sw.WriteLine("      = {0:f2} mm", d);

                val = (int)d / 10;
                val += 2;
                val = val * 10;

                sw.WriteLine();
                if (val < 100)
                {
                    d = 100;
                    sw.WriteLine("This effective depth is too small, ");
                    sw.WriteLine();
                    sw.WriteLine("Let us adopt an over all depth D = 100 mm");
                    sw.WriteLine();
                    sw.WriteLine("Assume {0} mm bars with {1} mm clear cover.", DiaMainReinforcement, ClearCover);
                }
                else
                {
                    d = val;
                }

                double eff_depth = d - ClearCover - d_d1 / 2;
                sw.WriteLine();
                sw.WriteLine("   Effective depth = {0:f2} - {1:f2} - {2}/2 ", d, ClearCover, d_d1);
                sw.WriteLine("                   = {0:f2} mm", eff_depth);
                sw.WriteLine();


                // At, B


                double a, b, c, At1;

                a = (0.87 * d_sigma_y * d_sigma_y) / (d_sigma_ck * 1000);
                b = 0.87 * d_sigma_y * eff_depth;
                c = B_end_support_moment * 10E+5;
                double b_ac = (b * b) - 4 * a * c;
                At1 = (b) - Math.Sqrt(Math.Abs(b_ac));
                At1 = At1 / (2 * a);

                At1 = (int)At1;
                At1 += 1;
                sw.WriteLine("At B, the area of steel is given by ");
                sw.WriteLine();
                sw.WriteLine("          Mu = 0.87 * σ_y * At(d-((σ_y*At)/(σ_ck*b))");
                sw.WriteLine();
                sw.WriteLine("    {0:f3} * 10E+5 = 0.87 * {1:f2} * At({2:f2} - (({1:f2} * At )/({3:f2} * 1000))",
                    B_end_support_moment,
                    d_sigma_y,
                    eff_depth,
                    d_sigma_ck);
                sw.WriteLine();
                sw.WriteLine("          At = {0:f3} sq.mm/m", At1);
                sw.WriteLine();

                double area1 = Math.PI * d_d1 * d_d1 / 4;
                int nos_main_rod = (int)(At1 / area1);
                nos_main_rod++;

                double main_spacing = (double)1000.0 / nos_main_rod;
                main_spacing = (int)main_spacing / 10;
                main_spacing = main_spacing * 10;

                double At1_ = area1 * 1000 / main_spacing;

                sw.WriteLine();
                sw.WriteLine("Use {0} mm bars @ {1} mm c/c.", d_d1, main_spacing);
                sw.WriteLine();

                if (At1_ > At1)
                {
                    sw.WriteLine(" Area of steel provided = {0:f0} sq.mm/m > {1:f0} sq.mm/m, OK", At1_, At1);
                }
                else
                {
                    sw.WriteLine(" Area of steel provided = {0:f0} sq.mm/m > {1:f0} sq.mm/m, NOT OK", At1_, At1);
                }


                double At2;

                a = (0.87 * d_sigma_y * d_sigma_y) / (d_sigma_ck * 1000);
                b = 0.87 * d_sigma_y * eff_depth;
                c = E_end_moment * 10E+5;
                b_ac = (b * b) - 4 * a * c;
                At2 = (b) - Math.Sqrt(Math.Abs(b_ac));
                At2 = At2 / (2 * a);

                At2 = (int)At2;
                //At2 += 1;
                sw.WriteLine();
                sw.WriteLine(" At E, the area of steel is given by ");
                sw.WriteLine();
                sw.WriteLine("Or,  {0} * 10E+5 = 0.87 * {1} * At(({2} - ({3}*At)/({4} * 1000))",
                    E_end_moment.ToString("0.00"),
                    d_sigma_y,
                    eff_depth,
                    d_sigma_y,
                    d_sigma_ck);

                sw.WriteLine();
                sw.WriteLine("Or, As = {0:f2} sq.mm/m", At2);

                int nos_dist_rod = (int)(At2 / area1);
                nos_dist_rod++;

                double main_spacing_2 = (double)1000.0 / nos_dist_rod;
                main_spacing_2 = (int)main_spacing_2 / 10;
                main_spacing_2++;
                main_spacing_2 = main_spacing_2 * 10;
                double At2_ = area1 * 1000 / main_spacing_2;
                sw.WriteLine();
                sw.WriteLine("Use {0} mm bars @ {1} mm c/c, As = {2:f0} sq.mm/m",
                    d_d1,
                    main_spacing_2,
                    At2_);

                // At, F

                sw.WriteLine();
                sw.WriteLine("At F, The Area of steel is given by,");
                sw.WriteLine();
                sw.WriteLine("  {0} * 10E+5 = 0.87 * {1} * At({2} - (({3} * At)/({4} * 1000))",
                    F_interior_moment.ToString("0.000"),
                    d_sigma_y,
                    eff_depth,
                    d_sigma_y,
                    d_sigma_ck);


                double At3;

                a = (0.87 * d_sigma_y * d_sigma_y) / (d_sigma_ck * 1000);
                b = 0.87 * d_sigma_y * eff_depth;
                c = F_interior_moment * 10E+5;
                b_ac = (b * b) - 4 * a * c;
                At3 = (b) - Math.Sqrt(Math.Abs(b_ac));
                At3 = At3 / (2 * a);

                At3 = (int)At3;

                int nos_main_rod_1 = (int)(At3 / area1);
                nos_main_rod_1++;

                double main_spacing_1 = (double)1000.0 / nos_main_rod_1;
                main_spacing_1 = (int)main_spacing_1 / 10;
                main_spacing_1 = (main_spacing_1 * 10);
                main_spacing_1--;

                double At3_ = (area1 * 1000) / main_spacing_1;

                sw.WriteLine();
                sw.WriteLine("Use {0:f0} mm bars @ {1:f0} mm c/c",
                    d_d1,
                    main_spacing_1);

                sw.WriteLine("As = {0:f2} sq.mm/m.", At3_);
                sw.WriteLine();
                sw.WriteLine("The Code requires that the maximum bar spacing should not exceed 3d");
                sw.WriteLine();
                sw.WriteLine("Minimum area of high strength main steel");
                sw.WriteLine("       = {0} % of the gross concrete", PercentageDistReinforcement);

                double strength_main_steel = (PercentageDistReinforcement / 100.0) * 1000 * d;

                sw.WriteLine("       = ({0:f2} /100) * 1000 * {1:f2}", PercentageDistReinforcement, d);
                sw.WriteLine("       = {0:f2} sq.mm.m", strength_main_steel);

                sw.WriteLine();
                if (At3_ > strength_main_steel)
                    sw.WriteLine("Thus the actual area provided is OK");
                else
                    sw.WriteLine("Thus the actual area provided is NOT OK");


                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Temperature reinforcement equal to 0.15% of the gross concrete");
                sw.WriteLine("area will be provided in the longitudinal direction");
                sw.WriteLine();
                //
                double dist_area = (0.15 / 100) * 1000 * 100;
                sw.WriteLine("  (0.15/100) * 1000 * 100 = {0:f3} sq.mm/m", dist_area);
                //area2
                double area2 = Math.PI * d_d2 * d_d2 / 4.0;

                int no_dist_rod = (int)(dist_area / area2);
                no_dist_rod++;

                double dist_spacing = 1000 / no_dist_rod;
                if (dist_spacing > 120)
                    dist_spacing = 120;
                //
                sw.WriteLine();
                sw.WriteLine("Use {0} mm MS bars @ {1} mm c/c", d_d2, dist_spacing);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Check for shear");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(" The maximum shear force occurs at the first interior support B in the Span AB,");
                sw.WriteLine();

                double Vu = 0.6 * v1 + 0.6 * v2;
                sw.WriteLine(" SF = 0.6 * {0} + 0.6 * {1} = {2} kN",
                    v1, v2, Vu);

                sw.WriteLine();
                double tau_v = Vu / (1000 * eff_depth);
                sw.WriteLine("    Nominal shear stress τ_v = Vu/b*d");
                sw.WriteLine("                             = {0} * 1000 / 1000 * {1}", Vu, eff_depth);
                sw.WriteLine("                             = {0:f4}", tau_v);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At B, percentage of tension steel,");

                double B_percent_tension_steel = (100 * At1_) / (1000 * eff_depth);

                sw.WriteLine("   100 As/b*d = (100 * {0})/(1000 * {1}) = {2}%",
                    At1_.ToString("0.00"), eff_depth, B_percent_tension_steel.ToString("0.0000"));


                ShearValue shV = new ShearValue();
                double tau_c = shV.Get_M15(B_percent_tension_steel);
                sw.WriteLine("So, τ_c = {0} N/sq.mm", tau_c);
                
                double k_value = kV.Get_KValue(d);

                sw.WriteLine("and    k = {0}", k_value);
                double tau_c_dash = k_value * tau_c;

                sw.WriteLine("τ_c` = {0} * {1} > τ_v    OK", k_value, tau_c);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Check for development length");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("The slab is checked for development length. Let us take the end support A.");

                //
                sw.WriteLine();
                sw.WriteLine("  φ < ((4 * τ_bd)/σ_s)*((1.3*(M1/V) + Lo))");
                //
                //

                double percent_curtain_bar = 50.0;

                sw.WriteLine(" Let us curtail {0}% bars at 0.1 l from the centre of support A.", percent_curtain_bar);
                sw.WriteLine();
                //sw.WriteLine("ταβγδλγσ");
                sw.WriteLine(" M1 = 0.87 * σ_y * At((d - (σ_y * At)/(σ_ck * b))");
                percent_curtain_bar = percent_curtain_bar / 100;

                double M1 = 0.87 * d_sigma_y * percent_curtain_bar * At2_ * ((eff_depth - (d_sigma_y * At2_ * percent_curtain_bar) / (d_sigma_ck * 1000)));

                sw.WriteLine("   = 0.87 * {0} * {1} * {2} * (({3} - ({4} * {5} * {6})/({7} * 1000))",
                    d_sigma_y,
                    percent_curtain_bar,
                    At2_.ToString("0.00"),
                    eff_depth,
                    d_sigma_y,
                    At2_.ToString("0.00"),
                    percent_curtain_bar,
                    d_sigma_ck);
                M1 = M1 / 10E+5;
                sw.WriteLine("    = {0:f2} * 10E+5 N-mm", M1);
                sw.WriteLine("    = {0:f2} kN-m", M1);

                sw.WriteLine();
                double V = 0.4 * v1 + 0.45 * v2;
                sw.WriteLine("   Shear force V = 0.4 * {0} + 0.45 * {1}", v1, v2);
                sw.WriteLine("                 = {0:f2} kN", V);
                sw.WriteLine();
                sw.WriteLine("Assuming Lo = 200 mm including anchor value of U-hook");

                double phi = ((4 * 1.6) / (0.87 * d_sigma_y)) * (((k_value * M1 * 1000) / V) + 200);

                //
                // CC
                sw.WriteLine();
                sw.WriteLine("  φ <= ((4 * 1.6)/(0.87*{0}))*(({1}*{2}*1000)/{3} + 200)",
                    d_sigma_y,
                    k_value,
                    M1.ToString("0.00"),
                    V.ToString("0.00"));

                sw.WriteLine("   = {0:f2} mm.", phi);

                //
                sw.WriteLine();
                sw.WriteLine(" Since the actual bar diameter is less than {0} mm, the slab is safe", phi.ToString("0.000"));
                sw.WriteLine("in development length. The bars must be embedded in the support by");
                sw.WriteLine("at least Ld/3 distance and a U-hook must be provided.");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Check for Deflection.");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(" The Slab is checked for deflection. Let us consider the end span.");
                sw.WriteLine();
                sw.WriteLine(" L/D <= α * β * γ * δ * λ");
                sw.WriteLine("   α  = {0} for continuous span", Alpha);
                sw.WriteLine("   β  = 1 since L < 10 m");
                sw.WriteLine();

                //double Pt = (100 * (50 * 1000 / 170)) / (1000 * 81);
                double Pt = (100 * (area1 * 1000 / main_spacing_1)) / (1000 * eff_depth);
                // CC
                sw.WriteLine("Pt = 100 * ({0:f0} * 1000/{1:f0})/(1000 * {2:f2})",
                    area1.ToString("0"),
                    main_spacing_1,
                    eff_depth);

                sw.WriteLine("   = {0:f2}%", Pt);
                sw.WriteLine();

                double gama = ModificationFactor.GetGamma(Pt);
                sw.WriteLine(" For a service stress of 240 Mpa in Fe 415 grade steel bar");
                sw.WriteLine(" the value of γ = {0}", gama.ToString("0.00"));
                sw.WriteLine("              δ = 1 since compressive reinforcement is zero.");
                sw.WriteLine("and           λ = 1");
                sw.WriteLine();

                double l_d = Alpha * gama;
                sw.WriteLine();
                sw.WriteLine(" Allowable L/d = {0} * {1:f3}", Alpha, gama.ToString("0.000"));
                sw.WriteLine("               = {0:f2}", l_d);
                sw.WriteLine();
                sw.WriteLine(" Actual    L/d = {0}/{1}", (end_span*1000), eff_depth);

                val = (end_span * 1000.0 / eff_depth);
                if (val < l_d)
                    sw.WriteLine("               = {0:f2} < {1:f2}, OK", val, l_d);
                else
                {
                    sw.WriteLine("               = {0:f2} > {1:f2}, NOT OK", val, l_d);
                }
                sw.WriteLine();

                #endregion

                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");


                #region Write DESIGN File
                sw_design.WriteLine("SLAB DESIGN 03");
                sw_design.WriteLine();
                sw_design.WriteLine("lengths = {0}", LengthString);
                //lengths = 3000, 3000, 3000, 3000, 3000
                sw_design.WriteLine("span_bredth = {0}", d_b);
                //span_bredth = 8000;
                sw_design.WriteLine("main_dia = {0}", d_d1);
                //main_dia = 8.0;
                sw_design.WriteLine("dist_dia = {0}", d_d2);
                //dist_dia = 6.0;
                sw_design.WriteLine("end_span_reinforcement = {0}", main_spacing_2 * 2);
                //end_span_reinforcement = 340;
                sw_design.WriteLine("support_reinforcement = {0}", main_spacing);
                //support_reinforcement = 140;
                sw_design.WriteLine("interior_reinforcement = {0}", main_spacing_1 * 2);
                //interior_reinforcement = 480;
                sw_design.WriteLine("start_centre_distance = {0}", 300);
                //start_centre_distance = 300;
                sw_design.WriteLine("start_wall_distance = {0}", 450);
                //start_wall_distance = 450;
                sw_design.WriteLine("clear_cover = {0}", ClearCover);
                //clear_cover = 15;
                sw_design.WriteLine("upper_distance = {0}", 900);
                //upper_distance = 900;
                sw_design.WriteLine("lower_distance = {0}", 600);
                //lower_distance = 600;
                sw_design.WriteLine("depth = {0}", d_Do);
                //depth = 120;
                sw_design.WriteLine("wall_thickness = {0}", WallThickness);
                //wall_thickness = 250;
                sw_design.WriteLine("bar_hill = {0}", 25.4);
                //bar_hill = 25.4;
                sw_design.WriteLine("pillar_length = {0}", 500);
                //pillar_length = 700;
                sw_design.WriteLine();
                sw_design.WriteLine("FINISH");
                #endregion

                string size1 = "9.1 x 1.0";
                string bgd = "Fe 415";
                double d1 = 8;
                double d2 = 6;
                double dist1 = 2935;
                double dist2 = 2635;
                double dist3 = 1800;
                double dist4 = 1000;
                double bno1 = 9;
                double bno2 = 9;
                double bno3 = 16;
                double bno4 = 69;
                double bno5 = 38;


                double bwt1 = 0.00616 * d1 * d1 * dist1 * bno1;

                bwt1 = bwt1 / 10e5;


                double bwt2 = 0.00616 * d1 * d1 * dist2 * bno1;
                bwt2 = bwt2 / 10e5;
                double bwt3 = 0.00616 * d1 * d1 * dist3 * bno2;
                bwt3 = bwt3 / 10e5;
                double bwt4 = 0.00616 * d2 * d2 * dist4 * bno3;
                bwt4 = bwt4 / 10e5;
                double bwt5 = 0.00616 * d2 * d2 * dist4 * bno4;
                bwt5 = bwt5 / 10e5;
               
                double depth = 90;
                double shape1 = 2935;
                double shape2 = 2635;
                double shape3 = 1800;
                double shape4 = 1000;


                #region BoQ File


                sw_boq.WriteLine("BoQ Text");
                sw_boq.WriteLine("size1={0}", size1);
                sw_boq.WriteLine("bgd={0}", bgd);
                sw_boq.WriteLine("d1={0}", d1);
                sw_boq.WriteLine("d2={0}", d2);
                sw_boq.WriteLine("dist1={0}", dist1);
                sw_boq.WriteLine("dist2={0}", dist2);
                sw_boq.WriteLine("dist3={0}", dist3);
                sw_boq.WriteLine("dist4={0}", dist4);
                sw_boq.WriteLine("bno1={0}", bno1);
                sw_boq.WriteLine("bno2={0}", bno2);
                sw_boq.WriteLine("bno3={0}", bno3);
                sw_boq.WriteLine("bno4={0}", bno4);
                sw_boq.WriteLine("bno5={0}", bno5);
                sw_boq.WriteLine("bwt1={0:f4}", bwt1);
                sw_boq.WriteLine("bwt2={0:f4}", bwt2);
                sw_boq.WriteLine("bwt3={0:f4}", bwt3);
                sw_boq.WriteLine("bwt4={0:f4}", bwt4);
                sw_boq.WriteLine("bwt5={0:f4}", bwt5);
                sw_boq.WriteLine("depth={0}", depth);
                sw_boq.WriteLine("shape1={0}", shape1);
                sw_boq.WriteLine("shape2={0}", shape2);
                sw_boq.WriteLine("shape3={0}", shape3);
                sw_boq.WriteLine("shape4={0}", shape4);
                sw_boq.WriteLine("FINISH");

                #endregion
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
                sw_design.Flush();
                sw_design.Close();
                sw_boq.Flush();
                sw_boq.Close();
            }
        }
        public void SetApp(string fileName)
        {
            string envFile = Path.Combine(Application.StartupPath, "env.set");
            StreamWriter sw = new StreamWriter(new FileStream(envFile, FileMode.Create));
            try
            {
                sw.WriteLine("CODE SLAB03 VIEW");
                sw.WriteLine("PATH " + filePathSet);

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
        public void Read_From_File()
        {
            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            MyList mList = null;
            string kStr = "";

            string option = "";


            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = lst_content[i].Trim().TrimEnd().TrimStart();
                //kStr = kStr.Replace(" ", "");
                mList = new MyList(kStr, '=');
                option = mList.StringList[0].Trim();
                #region Switch
                switch (option)
                {
                    case "LengthString":
                        LengthString = mList.StringList[1];
                        txt_L.Text = LengthString;
                        break;
                    case "d_Do":
                        d_Do = mList.GetDouble(1);
                        txt_Do.Text = d_Do.ToString("0.00");
                        break;
                    case "d_b":
                        d_b = mList.GetDouble(1);
                        txt_b.Text = d_b.ToString("0.00");
                        break;

                    case "d_SL":
                        d_SL = mList.GetDouble(1);
                        txt_SL.Text = d_SL.ToString("0.00");
                        break;
                    case "d_Fo":
                        d_Fo = mList.GetDouble(1);
                        txt_Fo.Text = d_Fo.ToString("0.00");
                        break;
                    case "d_FL":
                        d_FL = mList.GetDouble(1);
                        txt_FL.Text = d_FL.ToString("0.00");
                        break;
                    case "d_DF":
                        d_DF = mList.GetDouble(1);
                        txt_DF.Text = d_DF.ToString("0.00");
                        break;
                    case "d_LL":
                        d_LL = mList.GetDouble(1);
                        txt_LL.Text = d_LL.ToString("0.00");
                        break;
                    case "d_LF":
                        d_LF = mList.GetDouble(1);
                        txt_LF.Text = d_LF.ToString("0.00");
                        break;
                    case "d_d1":
                        d_d1 = mList.GetDouble(1);
                        txt_d1.Text = d_d1.ToString("0.00");
                        break;
                    case "d_d2":
                        d_d2 = mList.GetDouble(1);
                        txt_d2.Text = d_d2.ToString("0.00");
                        break;
                    case "d_Dp":
                        d_Dp = mList.GetDouble(1);
                        txt_Dp.Text = d_Dp.ToString("0.00");
                        break;
                    case "d_sigma_ck":
                        d_sigma_ck = mList.GetDouble(1);
                        txt_sigma_ck.Text = d_sigma_ck.ToString("0");
                        break;
                    case "d_sigma_y":
                        d_sigma_y = mList.GetDouble(1);
                        txt_sigma_y.Text = d_sigma_y.ToString("0");
                        break;
                    case "d_alpha":
                        d_alpha = mList.GetDouble(1);
                        txt_alpha.Text = d_alpha.ToString("0.00");
                        break;
                    case "d_beta":
                        d_beta = mList.GetDouble(1);
                        txt_beta.Text = d_beta.ToString("0.00");
                        break;
                    case "d_gamma":
                        d_gamma = mList.GetDouble(1);
                        txt_gamma.Text = d_gamma.ToString("0.00");
                        break;
                    case "d_delta":
                        d_delta = mList.GetDouble(1);
                        txt_delta.Text = d_delta.ToString("0.00");
                        break;
                    case "d_lamda":
                        d_lamda = mList.GetDouble(1);
                        txt_lamda.Text = d_lamda.ToString("0.00");
                        break;
                    case "d_w1":
                        d_w1 = mList.GetDouble(1);
                        txt_wall_thickness.Text = d_w1.ToString("0.00");
                        break;
                    case "d_h1":
                        d_h1 = mList.GetDouble(1);
                        txt_h1.Text = d_h1.ToString("0.00");
                        break;
                }
                #endregion
            }
            lst_content.Clear();
        }

        public void Write_User_Input()
        {

            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {


                sw.WriteLine("LengthString = {0}", LengthString);
                sw.WriteLine("d_Do = {0}", d_Do);
                sw.WriteLine("d_b = {0}", d_b);
                sw.WriteLine("d_SL = {0}", d_SL);
                sw.WriteLine("d_Fo = {0}", d_Fo);
                sw.WriteLine("d_FL = {0}", d_FL);
                sw.WriteLine("d_DF = {0}", d_DF);
                sw.WriteLine("d_LL = {0}", d_LL);
                sw.WriteLine("d_LF = {0}", d_LF);
                sw.WriteLine("d_d1 = {0}", d_d1);
                sw.WriteLine("d_d2 = {0}", d_d2);
                sw.WriteLine("d_Dp = {0}", d_Dp);
                sw.WriteLine("d_sigma_ck = {0}", d_sigma_ck);
                sw.WriteLine("d_sigma_y = {0}", d_sigma_y);
                sw.WriteLine("d_alpha = {0}", Alpha);
                sw.WriteLine("d_beta = {0}", Beta);
                sw.WriteLine("d_gamma = {0}", Gamma);
                sw.WriteLine("d_delta = {0}", Delta);
                sw.WriteLine("d_lamda = {0}", Lamda);
                sw.WriteLine("d_w1 = {0}", d_w1);
                sw.WriteLine("d_h1 = {0}", d_h1);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
}
