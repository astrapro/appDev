using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.BeamDesign;
namespace AstraFunctionOne.ColumnDesign
{
    public partial class frmRccColumnDesign : Form
    {
        string rep_file_name = "";
        string user_input_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";
        bool is_process = false;
        IApplication iApp = null;



        double K8, K9, FU, FY, B, H, D1, N, U;
        double B4, H4, D4, D5, E1, D;
        double C1, C2, H1, B1, M, EM, X, S, ASS, F4, N2;
        double M3, M4;
        double F3, F7;
        double E;
        double formatVal1 = 0.0;
        double formatVal2 = 0.0;


        //double FU, FY, B, H, D1, N, M;
        public frmRccColumnDesign(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            FU = 0.0d; 
            FY = 0.0d;
            B = 0.0d;
            H = 0.0d; 
            D1 = 0.0d;
            N = 0.0d;
            M = 0.0d;
        }

        private void frmRccColumnDesign_Load(object sender, EventArgs e)
        {

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }
        #region Form Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program();
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
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }
        private void btnDescription_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(Application.StartupPath, "Column and Footing\\Column_Axial.pdf");

            if (!File.Exists(fName))
                fName = Path.Combine(Application.StartupPath, "ASTRAHelp\\Column_Axial.pdf");
            try
            {
                System.Diagnostics.Process.Start(fName);
            }
            catch
            {
            }
            //frmReport_Viewer f_r_v = new frmReport_Viewer();
            //f_r_v.Report_Viewer.ReportSource = new crtAxialColumn();
            //f_r_v.Text = "AXIAL COLUMN DESIGN DESCRIPTION";
            //f_r_v.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Report Function
        public void SteelCompressiveStress(StreamWriter sw) // 4000
        {
            sw.WriteLine();
            sw.WriteLine("Calculated Compressive Stress in Steel = F7");
            sw.WriteLine();
            if (E >= E1)
            {
                F7 = FY / 1.15;
                sw.WriteLine("F7 = FY / 1.15 = {0} / 1.15 = {1:f2}", FY, F7);
                return;
            }
            if (E <= E1)
            {
                F7 = 200000 * E;
                sw.WriteLine("F7 = 200000 * E = 200000 * {0:f4} = {1:f2}", E, F7);
                return;
            }
        }
        public void SteelTensileStress(StreamWriter sw) // 4030
        {
            sw.WriteLine();
            sw.WriteLine("Calculated Tensile Stress in Steel = F7");
            sw.WriteLine();
            if (E >= E1)
            {
                F7 = -(FY / 1.15);
                sw.WriteLine("F7 = -(FY / 1.15) = -({0} / 1.15) = {1:E2}", FY, F7);
                return;
            }
            if (E <= E1)
            {
                F7 = -(200000 * E);
                sw.WriteLine("F7 = -(200000 * E) = -(200000 * {0:f4}) = {1:f2}", E, F7);
                return;
            }
        }

        #endregion
        #region IReport Members
        /**/
        public void Calculate_Program()
        {
            K8 = 0.45d; K9 = 0.90d;
            //double K8 = 0.45d;
            //double K9 = 0.90d;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t************************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 22              *");
                sw.WriteLine("\t\t* TechSOFT Engineering Services (I) Pvt. Ltd.  *");
                sw.WriteLine("\t\t*                                              *");
                sw.WriteLine("\t\t*          DESIGN OF RCC COLUMN                *");
                sw.WriteLine("\t\t*    FOR SYMMETRICAL REINFORCING STEEL         *");
                sw.WriteLine("\t\t************************************************");
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
                sw.WriteLine("Concrete Grade [FU] = {0} N/sq.mm", FU);
                sw.WriteLine("Steel Grade [Fy] = {0} N/sq.mm", FY);
                sw.WriteLine("Column Breadth [B] = {0} mm", B);
                sw.WriteLine("Column Depth [H] = {0} mm", H);
                sw.WriteLine("Depth to Reinforcing Steel [D1] = {0} mm", D1);
                sw.WriteLine("Axial Load [N] = {0} kN", N);
                sw.WriteLine("Moment [M] = {0} kN-m", M);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

              
                sw.WriteLine("Constant K8 = {0}  and K9 = {1}", K8, K9);
                sw.WriteLine();
                sw.WriteLine();



                E1 = FY / 1.15 / 200000.0;
                sw.WriteLine("Steel Yield Strain = E1");
                sw.WriteLine();
                sw.WriteLine("E1 = FY / 1.15 / 200000.0");
                sw.WriteLine("   = {0:F3} / 1.15 / 200000.0", FY);
                sw.WriteLine("   = {0:F3}", E1);

                N = N * 1000;
                sw.WriteLine();
                sw.WriteLine("N = N * 1000 = {0:E2} N", N);
                M = M * 1000000;
                sw.WriteLine();
                sw.WriteLine("M = M * 1000000 = {0:E2} N-mm", M);
                M4 = M;

                EM = H / 20;
                sw.WriteLine();
                sw.WriteLine("Minimum Moment = EM");
                sw.WriteLine();
                sw.WriteLine("EM = H / 20 = {0} / 20 = {1:F3} kN-m", H, EM);
                sw.WriteLine();
                if (EM > 20)
                {
                    EM = 20; // Minimum Moment
                    sw.WriteLine("EM = 20 (Minimum Moment <= 20)", EM);
                }
                if (M < (N * EM))
                {
                    M = N * EM;
                    sw.WriteLine("M = N * EM = {0:F3} * {1:F3} = {2:F3}", N, EM, M);
                }
                D = H - D1;
                sw.WriteLine();
                sw.WriteLine("D = H - D1 = {0:F3} - {1:F3} = {2:F3}", H, D1, D);
                //*****************   CHECK OF UNREINFORCED SECTION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("CHECK OF UNREINFORCED SECTION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                if (N == 0)
                {
                    goto _360;
                }
                else
                {
                    S = N / B / K8 / FU;
                    sw.WriteLine("S = N / B / K8 / FU");
                    sw.WriteLine("  = {0:F3} / {1:F3} / {2:F3} / {3:F3}", N, B, K8, FU);
                    sw.WriteLine("  = {0:F3}", S);
                }
                if (S > H)
                {
                    goto _360;
                }
                if ((N * (H - S) / 2) > M)
                {
                    //goto _940;
                }
            _360:
                // ** Iteration with X DECREASING TO BALANCE THE MOMENT
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Iteration with X DECREASING TO BALANCE THE MOMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();



                X = 2.333 * D;
                sw.WriteLine();
                sw.WriteLine("X = 2.333 * D = 2.333 * {0:F3} = {1:F3} ", D, X);
                U = 10;
                sw.WriteLine("U = 10");
            _380:
                sw.WriteLine();
                sw.WriteLine("X = X + H / U = {0:F3} + {1} / {2} = {3:f3}", X, H, U, (X + H / U));
                X = X + H / U;

            _390:
                sw.WriteLine();
                sw.WriteLine("X = X - H / U = {0:F3} - {1} / {2} = {3:f3}", X, H, U, (X - H / U));
                X = X - H / U;
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Iteration with Trial Value of X = {0:f3}", X);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                if (X < (.9 * D))
                {
                    goto _570;
                }
                if (X > (H / K9))
                {
                    sw.WriteLine();
                    sw.WriteLine("S = H = {0}", H);
                    S = H;
                }
                else if (X < (H / K9))
                {
                    S = K9 * X;
                    sw.WriteLine();
                    sw.WriteLine("S = K9 * X = {0:F3} * {1:F3}", K9, X, S);
                }
                if (X == D)
                {
                    F3 = 0;
                    goto _480;
                }
                E = Math.Abs(.0035 * (D - X) / X);
                sw.WriteLine();
                sw.WriteLine("E = ABS(.0035 * (D - X) / X)");
                sw.WriteLine("  = ABS(.0035 * ({0:F3} - {1:F3}) / {1:F3})", D, X);
                sw.WriteLine("  = {0:F4}", E);

                sw.WriteLine();
                if (X > D)
                {
                    
                    SteelCompressiveStress(sw);
                    sw.WriteLine();
                }
                if (X < D)
                {
                    
                    SteelTensileStress(sw);
                    sw.WriteLine();
                }
                F3 = F7;
                sw.WriteLine();
                sw.WriteLine("F3 = F7 = {0:F3}", F7);
            _480:
                E = 0.0035 * (X - D1) / X;
                sw.WriteLine();
                sw.WriteLine("E = 0.0035 * (X - D1) / X");
                sw.WriteLine("  = 0.0035 * ({0:F3} - {1}) / {0:F3}", X, D1);
                sw.WriteLine("  = {0:F4}", E);


                SteelCompressiveStress(sw);
                F4 = F7;
                sw.WriteLine();
                sw.WriteLine("F4 = F7 = {0:F3}", F7);
                ASS = 2 * (N - K8 * FU * B * S) / (F3 + F4);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Steel Area = ASS");
                sw.WriteLine("ASS = 2 * (N - K8 * FU * B * S) / (F3 + F4)");
                sw.WriteLine("    = 2 * ({0:F3} - {1} * {2} * {3:F3} * {4:F3})", N, K8, FU, B, S);
                sw.WriteLine("      / ({0:F3} + {1:F3})", F3, F4);
                sw.WriteLine();
                //sw.WriteLine("    = {0:F3}", ASS);
                if (ASS < 0)
                {
                    sw.WriteLine("    = {0:F3}, (the value is negative(-), So, Not Acceptable)", ASS);
                    goto _390;
                }
                if (ASS > 0)
                {
                    sw.WriteLine("    = {0:F3}, (the value is positive(+), So, Acceptable)", ASS);
                }
                M3 = K8 * FU * B * S * (H - S) / 2 + ASS * (F4 - F3) * (H / 2 - D1) / 2;
                sw.WriteLine();
                sw.WriteLine("M3 = K8 * FU * B * S * (H - S) / 2");
                sw.WriteLine("     + ASS * (F4 - F3) * (H / 2 - D1) / 2");

                sw.WriteLine();
                sw.WriteLine("   = {0:F3} * {1:F3} * {2:F3} * {3:F3} * ({4:F3} - ({3:F3})) / 2", K8, FU, B, S, H);
                sw.WriteLine("     + {0:F3} * ({1:F3} - ({2:F3})) * ({3:F3} / 2 - ({4:F3})) / 2", ASS, F4, F3, H, D1);

                sw.WriteLine();
                sw.WriteLine("   = {0:F3} N-mm = {1:f3} kN", M3, (M3/10E5));

                if (M3 < M)
                {
                    sw.WriteLine();
                    sw.WriteLine("----------------------------------------------------------------");
                    sw.WriteLine("Not within 2% of Applied Moment {0} kN-m.", (M / 10E5));
                    sw.WriteLine("So, go for next Trial.");
                    sw.WriteLine("----------------------------------------------------------------");
                    sw.WriteLine();
                    goto _390;
                }
                if (M * 1.02 > M3)
                {
                    goto _790;
                }
                sw.WriteLine();
                sw.WriteLine("X = X + H / U = {0:F3} + {1:F3} / {2:F3} = {3:F3}", X, H, U, (X + H / U));
                X = X + H / U;


                U = 10 * U;
                sw.WriteLine();
                sw.WriteLine("U = 10 * U = {0:F3}", U);
                goto _380;

            _570:
                // Iteration with X increasing to balance the axial load
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Iteration with X increasing to balance the axial load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                X = D1 + 1;
                sw.WriteLine();
                sw.WriteLine("X = D1 + 1 = {0:F3} + 1 = {1:F3}", D1, X);
                U = 10;
                sw.WriteLine();
                sw.WriteLine("U = 10");
            _590:
                sw.WriteLine();
                sw.WriteLine("X = X - H / U = {0:F3} - ({1:F3} / {2:F3}) = {3:F3}", X, H, U, (X - H / U));
                X = X - H / U;
            _600:
                sw.WriteLine();
                sw.WriteLine("X = X + H / U = {0:F3} + {1:F3} / {2:F3} = {3:F3}", X, H, U, (X + H / U));
                X = X + H / U;
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Iteration with Trial Value of X = {0:f3}", X);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                if (X > H / K9)
                {
                    S = H;
                    sw.WriteLine("S = H = {0:F3}", S);
                }
                if (X < H / K9)
                {
                    S = K9 * X;
                    sw.WriteLine("S = K9 * X = {0:F3} * {1:F3} = {2:F3}", K9, X, S);
                }
                if (X == D)
                {
                    F3 = 0;
                    sw.WriteLine();
                    sw.WriteLine("F3 = 0");
                    goto _680;
                }
                E = Math.Abs(0.0035 * (D - X) / X);
                sw.WriteLine();
                sw.WriteLine("E = ABS(0.0035 * (D - X) / X)");
                sw.WriteLine("  = ABS(0.0035 * ({0:F3} - ({1:F3})) / {1:F3})", D, X);
                sw.WriteLine("  = {0:F4}", E);
                sw.WriteLine();
                if (X > D)
                {
                    SteelCompressiveStress(sw); // ** STEEL COMPRESSIVE STRESS SUBROUTINE
                }
                if (X < D)
                {
                    SteelTensileStress(sw); //*** STEEL TENSILE STRESS SUBROUTINE
                }
                F3 = F7;
                sw.WriteLine();
                sw.WriteLine("F3 = F7 = {0:F3}", F3);
            _680:
                E = .0035 * (X - D1) / X;
                sw.WriteLine();
                sw.WriteLine("E = 0.0035 * (X - D1) / X");
                sw.WriteLine("  = 0.0035 * ({0:F3} - {1:F3}) / {0:F3}", X, D1);
                sw.WriteLine("  = {0:F3}", E);

                SteelCompressiveStress(sw);  // STEEL COMPRESSIVE STRESS SUBROUTINE
                F4 = F7;
                sw.WriteLine();
                sw.WriteLine("F4 = F7 = {0:F3}", F4);
                ASS = 2 * (M - K8 * FU * B * S * (H - S) / 2) / (F4 - F3) / (H / 2 - D1);
                sw.WriteLine();
                sw.WriteLine("ASS = 2 * (M - K8 * FU * B * S * (H - S) / 2)");
                sw.WriteLine("      / (F4 - F3) / (H / 2 - D1)");
                sw.WriteLine();
                sw.WriteLine("    = 2 * ({0:F3} - {1:F3} * {2:F3} * {3:F3} * {4:F3} * ({5:F3} - {4:F3}) / 2) ",
                    M, K8, FU, B, S, H);

                sw.WriteLine("      / ({0:F3} - ({1:F3})) / ({2:F3} / 2 - {3:F3})",
                  H, F4, F3, D1);

                sw.WriteLine();
                sw.WriteLine("    = {0:f3}", ASS);

                if (ASS < 0)
                {
                    goto _600;
                }
                N2 = K8 * FU * B * S + ASS * (F3 + F4) / 2;
                sw.WriteLine();
                sw.WriteLine("N2 = K8 * FU * B * S + ASS * (F3 + F4) / 2");
                sw.WriteLine("   = {0:F3} * {1:F3} * {2:F3} * {3:F3}", K8, FU, B, S);
                sw.WriteLine("     + {0:F3} * ({1:F3} + {2:F3}) / 2", ASS, F3, F4);
                sw.WriteLine();
                sw.WriteLine("   = {0:F3} N = {1:f3} kN", N2, (N2 / 1000));
                if (N2 < N)
                {
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------------");
                    sw.WriteLine("Not within 2% of Applied Axial Load {0} kN.", (N / 1000));
                    sw.WriteLine("So, go for next Trial.", (N / 1000));
                    sw.WriteLine("------------------------------------------------------------------");
                    sw.WriteLine();
                    goto _600;
                }
                if (X == (D1 + 1) && N2 > N)
                {
                    goto _790;
                }
                if (N * 1.02 > N2)
                {
                   
                    goto _790;
                }
                if (N == 0 && N2 - N < 0.1)
                {
                    //sw.WriteLine();
                    //sw.WriteLine("------------------------------------------------------------");
                    //sw.WriteLine("Not within 2% of Applied Load {0} kN", N);
                    //sw.WriteLine("------------------------------------------------------------");
                    //sw.WriteLine();
                    goto _790;
                }
                sw.WriteLine();
                sw.WriteLine("X = X - H / U = {0:F3} - ({1:F3} / {2:F3}) = {3:F3}", X, H, U, (X - H / U));
                X = X - H / U;
                U = 10 * U;
                sw.WriteLine();
                sw.WriteLine("U = 10 * U = {0:F3}", U);
                goto _590;
            _790:
                N2 = K8 * FU * B * S + ASS * (F3 + F4) / 2;
                sw.WriteLine();
                //sw.WriteLine("N2 = K8 * FU * B * S + ASS * (F3 + F4) / 2");
                //sw.WriteLine("   = {0:F3} * {1:F3} * {2:F3} * {3:F3}", K8, FU, B, S);
                //sw.WriteLine("     + {0:F3} * ({1:F3} + {2:F3}) / 2", ASS, F3, F4);
                //sw.WriteLine();
                //sw.WriteLine("   = {0:F3} N = {1:F3} N", N2, (N2 / 1000));
                //sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Finally within 2% of Applied Axial Load {0} kN", (N / 1000));
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                M3 = (K8 * FU * B * S * (H - S) / 2 + ASS * (F4 - F3) * (H / 2 - D1) / 2);
                sw.WriteLine();
                sw.WriteLine("M3 = K8 * FU * B * S * (H - S) / 2 ");
                sw.WriteLine("     + ASS * (F4 - F3) * (H / 2 - D1) / 2");
                sw.WriteLine();

                sw.WriteLine("   = {0:F3} * {1:F3} * {2:F3} * {3:F3} * ({4:F3} - {3:F3}) / 2 ", K8, FU, B, S, H);
                sw.WriteLine("     + {0:F3} * ({1:F3} - ({2:F3})) * ({3:F3} / 2 - {4:F3}) / 2", ASS, F4, F3, H, D1);
                sw.WriteLine("   = {0:F3} N-mm  = {1:f3} kN-m", M3, (M3/10E5));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Finally within 2% of Applied Moment {0:F2} kN-m", (M/10E5));
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                formatVal1 = (100.0 * ASS / B / H * 100) / 100.0d;

                sw.WriteLine("TOTAL STEEL AREA = {0:f2} sq.mm = {1:f2}%", ((ASS * 100) / 100), formatVal1);
                sw.WriteLine();

                formatVal1 = (100.0 * ASS / B / H);
                if (formatVal1 < 0.4)
                {
                    formatVal1 = 0.4 * B * H / 100.0;
                    sw.WriteLine("MINIMUM STEEL AREA REQUIRED = 0.4% of B*H = {0:f2} sq.mm", formatVal1);
                }
                else
                {
                    formatVal1 = (N2 / 10) / 100;
                    sw.WriteLine("AXIAL STRENGTH = {0:f2} kN", formatVal1);

                    formatVal1 = (M3 / 10000) / 100;
                    sw.WriteLine("MOMENT STRENGTH = {0:f2} kN-m", formatVal1);
                }

                #region STEP
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("STEP 1 :  ");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                #endregion



                #region END OF REPORT
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
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("FU = {0}", FU);
                sw.WriteLine("FY = {0}", FY);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("H = {0}", H);
                sw.WriteLine("D1 = {0}", D1);
                sw.WriteLine("N = {0}", N);
                sw.WriteLine("M = {0}", M);

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

            K8 = 0.0d;
            K9 = 0.0d;
            FU = 0.0d;
            FY = 0.0d;
            B = 0.0d;
            H = 0.0d;
            D1 = 0.0d;
            N = 0.0d;
            U = 0.0d;
            B4 = 0.0d;
            H4 = 0.0d;

            D4 = 0.0d;
            D5 = 0.0d;
            E1 = 0.0d;
            D = 0.0d;
            C1 = 0.0d;
            C2 = 0.0d;
            H1 = 0.0d;
            B1 = 0.0d;
            M = 0.0d;
            EM = 0.0d;

            X = 0.0d;
            S = 0.0d;
            ASS = 0.0d;
            F4 = 0.0d;
            N2 = 0.0d;
            M3 = 0.0d;
            M4 = 0.0d;
            F3 = 0.0d;
            F7 = 0.0d;
            E = 0.0d;
            formatVal1 = 0.0;
            formatVal2 = 0.0;

            #region USER DATA INPUT
            try
            {
                K8 = 0.45;
                K9 = 0.9;

                FU = MyList.StringToDouble(txt_FU.Text, 0.0);
                FY = MyList.StringToDouble(txt_Fy.Text, 0.0);
                B = MyList.StringToDouble(txt_B.Text, 0.0);
                H = MyList.StringToDouble(txt_h.Text, 0.0);
                D1 = MyList.StringToDouble(txt_d1.Text, 0.0);
                N = MyList.StringToDouble(txt_N.Text, 0.0);
                M = MyList.StringToDouble(txt_M.Text, 0.0);
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
                        case "FU":
                            txt_FU.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "FY":
                            txt_Fy.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "B":
                            txt_B.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "H":
                            txt_h.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "D1":
                            txt_d1.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "N":
                            txt_N.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "M":
                            txt_M.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
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

            string kPath = Path.Combine(user_path, "Structural Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "RCC Column");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Axially Loaded Column BS_8110");
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
                this.Text = "DESIGN OF RCC COLUMN AXIAL : " + value;
                user_path = value;
                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_COLUMN_AXIAL");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Axially Loaded Column.TXT");
                user_input_file = Path.Combine(system_path, "RCC_COLUMN_AXIAL.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                //btnDrawing.Enabled = File.Exists(rep_file_name);
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
    }
}
