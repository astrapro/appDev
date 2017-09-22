using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
//using AstraFunctionOne.Footing.Isolated;
using AstraFunctionOne.BeamDesign;


namespace AstraFunctionOne.Footing
{
    public partial class frmIsolatedFooting : Form, IReport
    {
        #region File Manage Variable
        string rep_file_name = "";
        string user_input_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";

        bool is_process = false;
        IApplication iApp = null;
        #endregion

        double C, H, D, F, LX, NG, MG, NQ, MQ, NW, MW;

        double N, M, GG, GW, GQ, LY, U, G, N1, F1, F2, Y, L2, Y1, Y2;
        double F3, M1, V, S, VP, P, AP;
        string str = "";
        string W1 = "";
        string W2 = "";

        int BaseTypeIndx = 1;
        bool IsRectangeBase = false;

        frmInputBox finp_box = null;
        public frmIsolatedFooting(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }
        #region Form Events
        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseTypeIndx = (cmb_type.SelectedIndex);
            IsRectangeBase = (BaseTypeIndx == 0);
            txt_LX.Enabled = (IsRectangeBase);
        }
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
            string fName = Path.Combine(Application.StartupPath, "Column and Footing\\Footing_Isolated.pdf");
            if (!File.Exists(fName))
                fName = Path.Combine(Application.StartupPath, "ASTRAHelp\\Footing_Isolated.pdf");
            try
            {
                System.Diagnostics.Process.Start(fName);
            }
            catch
            {
            }


            //frmReport_Viewer f_r_v = new frmReport_Viewer();
            //f_r_v.Report_Viewer.ReportSource = new crtIsolatedFooting();
            //f_r_v.Text = "ISOLATED FOOTING DESIGN DESCRIPTION";
            //f_r_v.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region IReport Members
        /**/
        public void Calculate_Program()
        {

            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 22             *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*         DESIGN OF ISOLATED FOUNDATION       *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
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
                sw.WriteLine("Size of Column [C] = {0} m", C);
                sw.WriteLine("Overall Base Depth [H] = {0} m", H);
                sw.WriteLine("Effective Depth of Base [D] = {0} m", D);
                sw.WriteLine("Allowable Bearing Pressure [F] = {0} kN/sq.mm", F);
                if (IsRectangeBase)
                    sw.WriteLine("Width of Base [LX] = {0} m", LX);
                sw.WriteLine("Axial Dead Loading [NG] = {0} kN", NG);
                sw.WriteLine("Moment Dead Load [MG] = {0} kN-m", MG);
                sw.WriteLine("Axial Live Load [NQ] = {0} kN", NQ);
                sw.WriteLine("Moment Live Load [MQ] = {0} kN-m", MQ);
                sw.WriteLine("Axial Wind Load [NW] = {0}  kN", NW);
                sw.WriteLine("Moment Wind Load [MW] = {0} kN-m", MW);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("STEP 1 :");
                sw.WriteLine("------------------------------");
                sw.WriteLine();
                #endregion

                N = NG + NQ + NW;
                M = MG + MQ + MW;

                sw.WriteLine();
                sw.WriteLine("N = NG + NQ + NW");
                sw.WriteLine("  = {0} + {1} + {2}", NG, NQ, NW);
                sw.WriteLine("  = {0} kN", N);
                sw.WriteLine();
                sw.WriteLine("M = MG + MQ + MW");
                sw.WriteLine("  = {0} + {1} + {2}", MG, MQ, MW);
                sw.WriteLine("  = {0} kN-m", M);
                sw.WriteLine();

                // Ultimate P.S.F's
                GG = 1.2; GQ = 1.2; GW = 1.2;

                if (NW == 0 && MW == 0)
                {
                    GG = 1.4;
                    GQ = 1.6;
                    GW = 0;
                    sw.WriteLine("GG = 1.4; GQ = 1.6; GW = 0.0");
                }
                else if (NQ == 0 && MQ == 0)
                {
                    GG = 1.4;
                    GQ = 0;
                    GW = 1.4;
                    sw.WriteLine("GG = 1.4; GQ = 0.0; GW = 1.4");
                }
                else
                {
                    sw.WriteLine("GG = 1.2; GQ = 1.2; GW = 1.2");
                }

                // Calculate minimum size of Base
                // Base in compression over whole area

                LY = 0.5;
                U = 0.5;
                G = 1;




                sw.WriteLine("LY = 0.5, U = 0.5, G = 1");


                int Trial = 1;
               
            _340:
                sw.WriteLine();
                sw.WriteLine("STEP 2 : TRIAL {0}", Trial++);
                sw.WriteLine("------------------------------");
           
                sw.WriteLine();
                sw.WriteLine("To determine the minimum Length of Base (LY)");
                sw.WriteLine();
                sw.WriteLine("Let us try with ");
                sw.WriteLine("LY = LY + U = {0} + {1} = {2}", LY, U, (LY + U));
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                LY = LY + U;

                if (!IsRectangeBase)
                    LX = LY;

                N1 = N + 24 * LY * LX * H;
                sw.WriteLine();
                sw.WriteLine("N1 = N + 24 * LY * LX * H");
                sw.WriteLine("   = {0} + 24 * {1} * {2} * {3}", N, LY, LX, H);
                sw.WriteLine("   = {0:f2}", N1);


                F1 = N1 / LY / LX + 6 * M / LX / (LY * LY);
                sw.WriteLine();
                sw.WriteLine("F1 = N1 / LY / LX + 6 * M / LX / (LY * LY)");
                sw.WriteLine("   = {0:f2} / {1} / {2} + 6 * {3:f2} / {2} / ({1} * {1})", N1, LY, LX, M);
                sw.WriteLine("   = {0:f2}", F1);
                if (F1 > F)
                {
                    sw.WriteLine();
                    sw.WriteLine("F1 is higher than allowable Bearing Pressure F = {0} kN/sq.mm, So, Not Ok", F);
                    sw.WriteLine();
                    goto _340;
                }
                else
                {
                    sw.WriteLine("F1 is less than allowable Bearing Pressure F = {0} kN/sq.mm, So, Ok", F);
                    sw.WriteLine();
                }
                if (G == 2)
                {
                   
                    goto _410;
                }
                sw.WriteLine();
                sw.WriteLine("LY = LY - U = {0} - {1} = {2}", LY, U, (LY - U));
                LY = LY - U;
                sw.WriteLine();
                sw.WriteLine("U = U / 10 = {0} / 10 = {1}", U, (U / 10));
                U = U / 10;
                sw.WriteLine();
                sw.WriteLine("G = 2");
                G = 2;
                goto _340;


            _410:
                sw.WriteLine();
                //sw.WriteLine("STEP 3 : TRIAL {0}", Trial++);
                sw.WriteLine();
                F2 = N1 / LY / LX - 6 * M / LX / (LY * LY);
                sw.WriteLine();
                sw.WriteLine("F2 = N1 / LY / LX - 6 * M / LX / (LY * LY)");
                sw.WriteLine("   = {0:f2} / {1} / {2} - 6 * {3} / {2} / ({1} * {1})", N1, LY, LX, M);
                sw.WriteLine("   = {0:f2}", F2);
                if (F2 > 0)
                {
                    goto _580;
                }

                // Base in Compression over part area only

                U = 0.5;
                G = 1;
                LY = 0.5;

                sw.WriteLine();
                sw.WriteLine("U = 0.5");
                sw.WriteLine();
                sw.WriteLine("G = 1");
                sw.WriteLine();
                sw.WriteLine("LY = 0.5");

            _460:
                sw.WriteLine();
                sw.WriteLine("LY = LY + U = {0} - {1} = {2}", LY, U, (LY + U));
                LY = LY + U;
                if (!IsRectangeBase)
                {
                    sw.WriteLine();
                    sw.WriteLine("LX = LY = {0}", LY);
                    LX = LY;
                }

                N1 = N + 24 * LY * LX * H;
                sw.WriteLine();
                sw.WriteLine("N1 = N + 24 * LY * LX * H");
                sw.WriteLine("   = {0} + 24 * {1} * {2} * {3}", N, LY, LX, H);
                sw.WriteLine("   = {0}", N1);

                F1 = 2 * N1 / 3 / LX / (LY / 2 - M / N1);
                sw.WriteLine();
                sw.WriteLine("F1 = 2 * N1 / 3 / LX / (LY / 2 - M / N1)");
                sw.WriteLine("   = 2 * {0} / 3 / {1} / ({2} / 2 - {3} / {0})", N1, LX, LY, M);
                sw.WriteLine("   = {0:f2} ", F1);

                if (F1 < 0)
                {
                    sw.WriteLine();
                    sw.WriteLine("REPEAT 460");
                    goto _460;
                }
                if (F1 > F)
                {
                    sw.WriteLine();
                    sw.WriteLine("REPEAT 460");
                    goto _460;
                }
                if (G == 2)
                {
                    sw.WriteLine();
                    sw.WriteLine("REPEAT 540");
                    goto _540;
                }
                sw.WriteLine();
                sw.WriteLine("LY = LY - U = {0} - {1} = {2}", LY, U, (LY - U));

                LY = LY - U;
                sw.WriteLine();
                sw.WriteLine("U = U / 10 = {0} / 10 = {1}", U, (U / 10));
                U = U / 10;

                sw.WriteLine();
                sw.WriteLine("G = 2");
                G = 2;
                goto _460;

            _540:
                Y = 3 * (LY / 2 - M / N1);
                sw.WriteLine();
                sw.WriteLine("Y = 3 * (LY / 2 - M / N1)");
                sw.WriteLine("  = 3 * ({0} / 2 - {1} / {2})", LY, M, N1);
                sw.WriteLine("  = {0} ", Y);

                //sw.WriteLine("Minimum Base Length = {0} m", LY);
            //sw.WriteLine("Compression Over = {0} m of Base Only", Y);

                // Enter Required Base Length
            _580:
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("STEP 3 :");
                sw.WriteLine("------------------------------");
                sw.WriteLine();
            
                L2 = Get_Req_Base_Len(LY, Y);  // User Input

                //Y = 3 * (L2 / 2 - M / N1);
                //sw.WriteLine();
                //sw.WriteLine("Y = 3 * (L2 / 2 - M / N1)");
                //sw.WriteLine("  = 3 * ({0} / 2 - {1} / {2})", L2, M, N1);
                //sw.WriteLine("  = {0:f3} ", Y);


                sw.WriteLine();
                sw.WriteLine("Minimum Base Length = {0} m", LY);
                sw.WriteLine("Compression Over {0:f3} m of Base Only", Y);
                sw.WriteLine();
                sw.WriteLine("Required Base Length = {0} m ", L2);

                if (!IsRectangeBase)
                {
                    LX = L2;
                    sw.WriteLine();
                    sw.WriteLine("LX = L2 = {0} m", L2);

                }
                N1 = N + 24 * L2 * LX * H;
                sw.WriteLine();
                sw.WriteLine("N1 = N + 24 * L2 * LX * H");
                sw.WriteLine("   = {0} + 24 * {1} * {2} * {3}", N, L2, LX, H);
                sw.WriteLine("   = {0:f2}", N1);

                F1 = N1 / L2 / LX + 6 * M / (L2 * L2) / LX;
                sw.WriteLine();
                sw.WriteLine("F1 = N1 / L2 / LX + 6 * M / (L2 * L2) / LX");
                sw.WriteLine("   = {0:f2} / {1} / {2} + 6 * {3} / ({1} * {1}) / {2}", N1, L2, LX, M);
                sw.WriteLine("   = {0:f2} ", F1);

                F2 = N1 / L2 / LX - 6 * M / (L2 * L2) / LX;
                sw.WriteLine();
                sw.WriteLine("F2 = N1 / L2 / LX - 6 * M / (L2 * L2) / LX");
                sw.WriteLine("   = {0:f2} / {1} / {2} - 6 * {3} / ({1} * {1}) / {2}", N1, L2, LX, M);
                sw.WriteLine("   = {0:f2}", F2);

                if (F2 > 0)
                {
                    goto _720;
                }
                F1 = 2 * N1 / 3 / LX / (L2 / 2 - M / N1);

                sw.WriteLine();
                sw.WriteLine("F1 = 2 * N1 / 3 / LX / (L2 / 2 - M / N1)");
                sw.WriteLine("   = 2 * {0:f2} / 3 / {1} / ({2} / 2 - {3} / {0:f2})", N1, LX, L2, M);
                sw.WriteLine("   = {0:f2} ", F1);


                F2 = 0;
                sw.WriteLine();
                sw.WriteLine("F2 = 0;");

                Y = 3 * (L2 / 2 - M / N1);
                sw.WriteLine();
                sw.WriteLine("Y = 3 * (L2 / 2 - M / N1)");
                sw.WriteLine("  = 3 * ({0} / 2 - {1} / {2})", L2, M, N1);
                sw.WriteLine("  = {0}", Y);

                sw.WriteLine();
                sw.WriteLine("Maximum Compressive Stress = " + F1.ToString("0.00") + " kN/sq.m");
                sw.WriteLine("Compression Over " + Y.ToString("0.00") + " m of Base Only");



                str = "Maximum Compressive Stress = " + F1.ToString("0.00") + " kN/sq.m\n\n";
                str += "" + "Compression Over " + Y.ToString("0.00") + " m of Base Only\n\n";

                goto _730;

            _720:
                str += "" + "Maximum and Minimum Stresses = " + F1.ToString("0.00") + " and " + F2.ToString("0.00") + " kN/sq.m\n\n";
                sw.WriteLine();
                sw.WriteLine("Maximum Stresses = {0:f2} kN/sq.m", F1);
                sw.WriteLine("Minimum Stresses = {0:f2} kN/sq.m", F2);
                sw.WriteLine();
                sw.WriteLine("Base Stressess under Ultimate Loads");
            _730:
                str += "" + "Do you wish to try another Base Size ?\n\n";

                if (MessageBox.Show(str, "ASTRA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    str = "";
                    goto _580;
                }


                // Calculate base stresses under ultimate loads

                N1 = NG * GG + NQ * GQ + NW * GW + 24 * L2 * LX * H * GG;
                sw.WriteLine();
                sw.WriteLine("N1 = NG * GG + NQ * GQ + NW * GW + 24 * L2 * LX * H * GG");
                sw.WriteLine("   = {0} * {1} + {2} * {3} + {4} * {5} + 24 * {6} * {7} * {8} * {1}",
                    NG, GG, NQ, GQ, NW, GW, L2, LX, H);
                sw.WriteLine("   = {0:f2}", N1);


                M = MG * GG + MQ * GQ + MW * GW;
                sw.WriteLine();
                sw.WriteLine("M = MG * GG + MQ * GQ + MW * GW");
                sw.WriteLine("  = {0} * {1} + {2} * {3} + {4} * {5}", MG, GG, MQ, GQ, MW, GW);
                sw.WriteLine("  = {0:f2} ", M);

                F1 = N1 / L2 / LX + 6 * M / (L2 * L2) / LX;
                sw.WriteLine();
                sw.WriteLine("F1 = N1 / L2 / LX + 6 * M / (L2 * L2) / LX");
                sw.WriteLine("   = {0:f2} / {1} / {2} + 6 * {3:f2} / ({1} * {1}) / {2}", N1, L2, LX, M);
                sw.WriteLine("   = {0:f2}", F1);

                F2 = N1 / L2 / LX - 6 * M / (L2 * L2) / LX;
                sw.WriteLine();
                sw.WriteLine("F2 = N1 / L2 / LX - 6 * M / (L2 * L2) / LX");
                sw.WriteLine("   = {0:f2} / {1} / {2} - 6 * {3:f2} / ({1} * {1}) / {2}", N1, L2, LX, M);
                sw.WriteLine("   = {0:f2} ", F2);

                if (F2 > 0)
                {
                    goto _1030;
                }

                // Base Partly in compression under ultimate loads

                if ((L2 / 2 - M / N1) < 0)
                {
                    MessageBox.Show(this, "Base size inadequate at U.L.S", "ASTRA", MessageBoxButtons.OK);
                    goto _580;
                }


                F1 = 2 * N1 / 3 / LX / (L2 / 2 - M / N1);
                sw.WriteLine();
                sw.WriteLine("F1 = 2 * N1 / 3 / LX / (L2 / 2 - M / N1)");
                sw.WriteLine("   = 2 * {0} / 3 / {1} / ({2} / 2 - {3} / {4})", N1, LX, L2, M, N1);
                sw.WriteLine("   = {0:f2}", F1);


                Y = 3 * (L2 / 2 - M / N1);
                sw.WriteLine();
                sw.WriteLine("Y = 3 * (L2 / 2 - M / N1)");
                sw.WriteLine("  = 3 * ({0} / 2 - {1} / {2})", L2, M, N1);
                sw.WriteLine("  = {0}", Y);

                Y1 = L2 / 2 + C / 2;
                sw.WriteLine();
                sw.WriteLine("Y1 = L2 / 2 + C / 2");
                sw.WriteLine("   = {0} / 2 + {1} / 2", L2, C);
                sw.WriteLine("   = {0}", Y1);

                Y2 = L2 - Y1;
                sw.WriteLine();
                sw.WriteLine("Y2 = L2 - Y1 = {0} - {1} = {2}", L2, Y1, (L2 - Y1));


                if (Y2 > Y)
                {
                    goto _920;
                }


                F3 = F1 * (Y - Y2) / Y;
                sw.WriteLine();
                sw.WriteLine("F3 = F1 * (Y - Y2) / Y");
                sw.WriteLine("   = {0:f2} * ({1} - {2}) / {1}", F1, Y, Y2);
                sw.WriteLine("   = {0:f2}", F3);

                sw.WriteLine();
                sw.WriteLine("Bending Moment at the Face of the Column :");
                M1 = (F1 - F3) * LX * Y2 * Y2 * (1.0 / 3.0) + F3 * LX * Y2 * Y2 / 2 - LX * Y2 * H * Y2 * 24 * GG / 2;
                sw.WriteLine();
                sw.WriteLine("M1 = (F1 - F3) * LX * Y2 * Y2 * (1.0 / 3.0)");
                sw.WriteLine("     + F3 * LX * Y2 * Y2 / 2 ");
                sw.WriteLine("     - LX * Y2 * H * Y2 * 24 * GG / 2");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} - {1:f2}) * {2} * {3} * {3} * (1.0 / 3.0)", F1, F3, LX, Y2);
                sw.WriteLine("     + {0:f2} * {1} * {2} * {2} / 2 ", F3, LX, Y2);
                sw.WriteLine("     - {0} * {1} * {2} * {1} * 24 * {3} / 2", LX, Y2, H, GG);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2}", M1);

                goto _930;

            _920:
                M1 = F1 * LX * Y * (Y2 - Y / 3) - LX * Y2 * H * Y2 * 24 * GG / 2;
                sw.WriteLine();
                sw.WriteLine("M1 = F1 * LX * Y * (Y2 - Y / 3)");
                sw.WriteLine("     - LX * Y2 * H * Y2 * 24 * GG / 2");
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} * {1} * {2} * ({3} - {2} / 3)", F1, LX, Y, Y2);
                sw.WriteLine("     - {0} * {1} * {2} * {1} * 24 * {3} / 2", LX, Y2, H, GG);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} ", M1);

            _930:
                //Calculate shear force at 1.0*D from face of column
                sw.WriteLine();
                sw.WriteLine("Y1 = Y1 + 1 * D = {0} + 1 * {1} = {2}", Y1, D, (Y1 + 1 * D));
                Y1 = Y1 + 1 * D;

                Y2 = L2 - Y1;
                sw.WriteLine();
                sw.WriteLine("Y2 = L2 - Y1 = {0} - {1} = {2}", L2, Y1, (L2 - Y1));
                W1 = "";

                if (Y1 > L2)
                {
                    W1 = "Critical Shear Section outside of Base";
                    sw.WriteLine();
                    sw.WriteLine(W1);

                }

                if (Y1 > L2)
                {
                    V = 0;
                    sw.WriteLine();
                    sw.WriteLine("V = 0");
                    goto _1140;
                }

                if (Y2 > Y)
                    goto _1010;

                F3 = F1 * (Y - Y2) / Y;
                sw.WriteLine();
                sw.WriteLine("F3 = F1 * (Y - Y2) / Y");
                sw.WriteLine("   = {0:f2} * ({1} - {2}) / {1}", F1, Y, Y2);
                sw.WriteLine("   = {0:f2}", F3);

                V = (F1 + F3) * LX * Y2 / 2 - LX * Y2 * H * 24 * GG;
                sw.WriteLine();
                sw.WriteLine("V = (F1 + F3) * LX * Y2 / 2 - LX * Y2 * H * 24 * GG");
                sw.WriteLine("  = ({0:f2} + {1:f2}) * {2} * {3} / 2 ", F1, F3, LX, Y2);
                sw.WriteLine("    - {0} * {1} * {2} * 24 * {3}", LX, Y2, H, GG);
                sw.WriteLine();
                sw.WriteLine("  = {0:f2} ", V);

                goto _1140;

            _1010:
                V = F1 * LX * Y / 2 - LX * Y2 * H * 24 * GG;
                sw.WriteLine();
                sw.WriteLine("V = F1 * LX * Y / 2 - LX * Y2 * H * 24 * GG");
                sw.WriteLine("  = F1 * LX * Y / 2 ");
                sw.WriteLine("    - LX * Y2 * H * 24 * GG");

                sw.WriteLine("  = {0:f2} * {1} * {2} / 2 ", F1, LX, Y);
                sw.WriteLine("    - {0:f2} * {1} * {2} * 24 * {3}", LX, Y2, H, GG);
                sw.WriteLine();
                sw.WriteLine("  = {0:f2}", V);

                goto _1140;

            _1030:


                // whole of base area in compression under ultimate loads

                // Calculated bending moment at face of column

                Y1 = L2 / 2 + C / 2;
                sw.WriteLine();
                sw.WriteLine("Y1 = L2 / 2 + C / 2");
                sw.WriteLine("   = {0} / 2 + {1} / 2", L2, C);
                sw.WriteLine("   = {0}", Y1);


                Y2 = L2 - Y1;
                sw.WriteLine();
                sw.WriteLine("Y2 = L2 - Y1 = {0} - {1} = {2}", L2, Y1, (L2 - Y1));

                W1 = "";
                F3 = F2 + (F1 - F2) * Y1 / L2;
                sw.WriteLine();
                sw.WriteLine("F3 = F2 + (F1 - F2) * Y1 / L2");
                sw.WriteLine("   = {0:f2} + ({1:f2} - {0:f2}) * {2} / {3}", F2, F1, Y1, L2);
                sw.WriteLine("   = {0:f2}", F3);

                M1 = (F1 - F3) * LX * Y2 * Y2 * (1.0 / 3.0) + F3 * LX * Y2 * Y2 / 2 - LX * Y2 * H * Y2 * 24 * GG / 2;
                sw.WriteLine();
                sw.WriteLine("M1 = (F1 - F3) * LX * Y2 * Y2 * (1.0 / 3.0)");
                sw.WriteLine("     + F3 * LX * Y2 * Y2 / 2");
                sw.WriteLine("     - LX * Y2 * H * Y2 * 24 * GG / 2");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} - {1:f2}) * {2} * {3} * {3} * (1.0 / 3.0)", F1, F3, LX, Y2);
                sw.WriteLine("     + {0:f2} * {1} * {2} * {2} / 2", F3, LX, Y2);
                sw.WriteLine("     - {0} * {1} * {2} * {1} * 24 * {3} / 2", LX, Y2, H, GG);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} ", M1);

                // Calculate Shear Force at 1.0*D from face of column
                sw.WriteLine();
                sw.WriteLine("Shear Force at 1.0*D from face of column");

                sw.WriteLine();
                sw.WriteLine("Y1 = Y1 + 1 * D = {0} + 1.0*{1} = {2}", Y1, D, (Y1 + 1 * D));
                Y1 = Y1 + 1 * D;

                Y2 = L2 - Y1;
                sw.WriteLine();
                sw.WriteLine("Y2 = L2 - Y1 = {0} - {1} = {2}", L2, Y1, (L2 - Y1));
                if (Y1 > L2)
                {
                    W1 = "Critical shear section outside of Base";
                    sw.WriteLine();
                    sw.WriteLine(W1);
                }

                if (Y2 > L2)
                {
                    V = 0;
                    sw.WriteLine();
                    sw.WriteLine("V = 0");
                    goto _1140;
                }
                F3 = F2 + (F1 - F2) * Y1 / L2;
                sw.WriteLine();
                sw.WriteLine("F3 = F2 + (F1 - F2) * Y1 / L2");
                sw.WriteLine("   = {0:f2} + ({1:f2} - {0:f2}) * {2} / {3}", F2, F1, Y1, L2);
                sw.WriteLine("   = {0:f2} ", F3);

                V = (F1 + F3) * LX * Y2 / 2 - LX * Y2 * H * 24 * GG;
                sw.WriteLine();
                sw.WriteLine("V = (F1 + F3) * LX * Y2 / 2 - LX * Y2 * H * 24 * GG");
                sw.WriteLine("  = ({0:f2} + {1:f2}) * {2} * {3} / 2", F1, F3, LX, Y2);
                sw.WriteLine("    - {0} * {1} * {2} * 24 * {3}", LX, Y2, H, GG);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} ", V);

            _1140:
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("STEP 4 : ");
                sw.WriteLine("------------------------------");
                sw.WriteLine();
             
                sw.WriteLine();
                sw.WriteLine("Punching shear force");

                S = C / 2 + 1.5 * D;
                sw.WriteLine();
                sw.WriteLine("S = C / 2 + 1.5 * D");
                sw.WriteLine("  = {0} / 2 + 1.5 * {1}", C, D);
                sw.WriteLine("  = {0:f2}", S);
                W2 = "";

                if (S >= (L2 / 2))
                {
                    W2 = "PUNCHING SHEAR PERIMETER OUTSIDE OF BASE";
                    sw.WriteLine();
                    sw.WriteLine(W2);
                    VP = 0;
                    sw.WriteLine();
                    sw.WriteLine("VP = 0");
                    P = 0;
                    sw.WriteLine();
                    sw.WriteLine("P = 0");
                    goto _1240;
                }
                if (S >= (LX / 2))
                {
                    W2 = "PUNCHING SHEAR PERIMETER OUTSIDE OF BASE";
                    sw.WriteLine();
                    sw.WriteLine(W2);
                    VP = 0;
                    sw.WriteLine();
                    sw.WriteLine("VP = 0");
                    P = 0;
                    sw.WriteLine();
                    sw.WriteLine("P = 0");
                    goto _1240;
                }

                P = 4 * C + 12 * D;
                sw.WriteLine();
                sw.WriteLine("P = 4 * C + 12 * D");
                sw.WriteLine("  = 4 * {0} + 12 * {1}", C, D);
                sw.WriteLine("  = {0:f2}", P);

                AP = (C + 3 * D) * (C + 3 * D);
                sw.WriteLine();
                sw.WriteLine("AP = (C + 3 * D) * (C + 3 * D)");
                sw.WriteLine("   = ({0} + 3 * {1}) * ({0} + 3 * {1})", C, D);
                sw.WriteLine("   = {0:f2}", AP);
                F1 = N1 / L2 / LX - H * 24 * GG;
                sw.WriteLine();
                sw.WriteLine("F1 = N1 / L2 / LX - H * 24 * GG");
                sw.WriteLine("   = {0:f2} / {1} / {2} - {3} * 24 * {4}", N1, L2, LX, H, GG);
                sw.WriteLine("   = {0:f2}", F1);

                VP = F1 * (L2 * LX - AP);
                sw.WriteLine();
                sw.WriteLine("VP = F1 * (L2 * LX - AP)");
                sw.WriteLine("   = {0:f2} * ({1} * {2} - {3:f2})", F1, L2, LX, AP);
                sw.WriteLine("   = {0:f2}", VP);

            _1240:
                sw.WriteLine();
                sw.WriteLine("STEP 5 : ");
                sw.WriteLine("------------------------------");
                sw.WriteLine();
                sw.WriteLine("Minimum Base Length = {0} m", LY);
                sw.WriteLine("Required Base Length = {0} m", L2);
                sw.WriteLine("Base Width = {0} m", LX);
                sw.WriteLine();
                sw.WriteLine("Design Moment Capacity = {0:f2} kN-m", M1);
                sw.WriteLine("Design Shear Capacity = {0:f2} kN", V);
                sw.WriteLine("Punching Shear Capacity = {0:f2} kN", VP);
                sw.WriteLine("Punching Shear Perimeter = {0:f2} kN", P);


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

        public double Get_Req_Base_Len(double min_len, double com_ovr)
        {
            finp_box = new frmInputBox();
            finp_box.MinimumLength = min_len;
            finp_box.CompressionOver = com_ovr;
            finp_box.RequiredBaseLength = min_len;

            finp_box.ShowDialog();
            return finp_box.RequiredBaseLength;
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("C = {0}", C);
                sw.WriteLine("H = {0}", H);
                sw.WriteLine("D = {0}", D);
                sw.WriteLine("F = {0}", F);
                sw.WriteLine("LX = {0}", LX);
                sw.WriteLine("NG = {0}", NG);
                sw.WriteLine("MG = {0}", MG);
                sw.WriteLine("NQ = {0}", NQ);
                sw.WriteLine("MQ = {0}", MQ);
                sw.WriteLine("NW = {0}", NW);
                sw.WriteLine("MW = {0}", MW);
                sw.WriteLine("BaseTypeIndx = {0}", BaseTypeIndx);

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
            #region USER DATA INPUT
            try
            {
                N = 0.0d;
                M = 0.0d;
                GG = 0.0d;
                GW = 0.0d; 
                GQ = 0.0d; 
                LY = 0.0d; 
                U = 0.0d; 
                G = 0.0d; 
                N1 = 0.0d;
                F1 = 0.0d;
                F2 = 0.0d;
                Y = 0.0d; 
                L2 = 0.0d;
                Y1 = 0.0d;
                Y2 = 0.0d;
                F3 = 0.0d; 
                M1 = 0.0d; 
                V = 0.0d; 
                S = 0.0d; 
                VP = 0.0d; 
                P = 0.0d; 
                AP = 0.0d;
                str = "";
                W1 = "";
                W2 = "";

                BaseTypeIndx = 1;
                

                C = MyList.StringToDouble(txt_C.Text, 0.0);
                H = MyList.StringToDouble(txt_H.Text, 0.0);
                D = MyList.StringToDouble(txt_D.Text, 0.0);
                F = MyList.StringToDouble(txt_F.Text, 0.0);
                LX = MyList.StringToDouble(txt_LX.Text, 0.0);
                NG = MyList.StringToDouble(txt_NG.Text, 0.0);
                MG = MyList.StringToDouble(txt_MG.Text, 0.0);
                NQ = MyList.StringToDouble(txt_NQ.Text, 0.0);
                MQ = MyList.StringToDouble(txt_MQ.Text, 0.0);
                NW = MyList.StringToDouble(txt_NW.Text, 0.0);
                MW = MyList.StringToDouble(txt_MW.Text, 0.0);
                BaseTypeIndx = cmb_type.SelectedIndex;

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
                        case "C":
                            txt_C.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "H":
                            txt_H.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "D":
                            txt_D.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "F":
                            txt_F.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "LX":
                            txt_LX.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "NG":
                            txt_NG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MG":
                            txt_MG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "NQ":
                            txt_NQ.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MQ":
                            txt_MQ.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "NW":
                            txt_NW.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MW":
                            txt_MW.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "BaseTypeIndx":
                            cmb_type.SelectedIndex = mList.GetInt(1);
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

            kPath = Path.Combine(kPath, "RCC Foundation");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Isolated Foundation");
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
                this.Text = "DESIGN OF ISOLATED FOUNDATION : " + value;
                user_path = value;
                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "ISOLATED_FOOTING");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Isolated Foundation.TXT");
                user_input_file = Path.Combine(system_path, "ISOLATED_FOOTING.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                btnDescription.Enabled = File.Exists(rep_file_name);
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

        private void frmIsolatedFooting_Load(object sender, EventArgs e)
        {
            cmb_type.SelectedIndex = BaseTypeIndx;

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }

        /**/
        #endregion
    }
}
