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
namespace AstraFunctionOne.BridgeDesign.Foundation
{
    public partial class frmPileFoundation : Form
    {
        string rep_file_name = "";
        string user_input_file = "";
        string user_drawing_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";
        bool is_process = false;
        IApplication iApp = null;

        double D, P, K, AM, N_gamma, Nq, Nc, FS, PCBL, SL, FL, sigma_ck, fy, gamma_c;
        double Np, N, gamma_sub, cap_sigma_ck, cap_fy, sigma_cbc, sigma_st, m, F, d1, d2, d3;
        double LPC, BPC, LPr, BPr, DPC, l1, l2, l3;
        string ref_string = "";


        string _1, _2, _3, _4;

        PileFoundationTableCollection pft_list = null;
        public frmPileFoundation(IApplication app)
        {
            InitializeComponent();
            iApp = app;

        }

        #region Form Event
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            Calculate_Program();
            Write_Drawing_File();
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
        private void btnDrawing_Click(object sender, EventArgs e)
        {

            iApp.SetDrawingFile_Path(user_drawing_file, "Pile_Foundation", "");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region IReport Members

        public void Calculate_Program()
        {
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 20.0            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*       DESIGN OF RCC PILE FOUNDATION         *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("FOR DESIGN OF PILE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
               
                sw.WriteLine("Pile Diameter [D] = {0} m", txt_D.Text);
                sw.WriteLine("Applied Load on a Pile [P] = {0} Ton", txt_P.Text);
                sw.WriteLine("Coefficient of Active Earth Pressure [K] = {0}", txt_K.Text);
                sw.WriteLine("Applied Moment on a Pile [AM] = {0} Ton-m", txt_AM.Text);
                sw.WriteLine("Nγ = {0}", txt_N_gamma.Text);
                sw.WriteLine("Nq = {0}", txt_Nq.Text);
                sw.WriteLine("Nc = {0}", txt_Nc.Text);
                sw.WriteLine("Factor of Safety [FS] = {0}", txt_FS.Text);
                sw.WriteLine("Pile Cap Bottom Level [PCBL] = {0} m", txt_PCBL.Text);
                sw.WriteLine("Scour Level [SL) = {0} m", txt_SL.Text);
                sw.WriteLine("Founding Level [FL] = {0}", txt_FL.Text);
                sw.WriteLine("Concrete Grade [σ_ck] = M{0:f0}", sigma_ck);
                sw.WriteLine("Steel Grade [fy] = Fe{0:f0}", fy);
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} Ton/cm", txt_gamma_c.Text);
                sw.WriteLine("Total Piles [Np] = {0}", txt_Np.Text);
                sw.WriteLine("Total Piles in front row [N] = {0}", txt_N.Text);
                //sw.WriteLine("γ_sub = {0} Ton/cu.m", txt_gamma_sub.Text);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("FOR DESIGN OF PILE CAP");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Concrete Grade [σ_ck] = M {0:f0}", cap_sigma_ck);
                sw.WriteLine("Steel Grade [fy] = Fe {0:f0}", cap_fy);
                sw.WriteLine();
                sw.WriteLine("Allowable Stress in concrete");
                sw.WriteLine("in bending compression [σ_cbc] = {0} kg/sq.cm", txt_sigma_cbc.Text);
                sw.WriteLine("Allowable stress in steel [σ_st] = {0} kg/sq.cm", txt_sigma_st.Text);
                sw.WriteLine("Modular Ratio [m] = {0}", txt_m.Text);
                sw.WriteLine("Load Factor [F] = {0}", txt_F.Text);
                sw.WriteLine("Diameter of Main Steel Reinforcement bars [d1] = {0} mm", txt_d1.Text);
                sw.WriteLine("Bottom Reinforcement Bar Diameter [d2] = {0} mm", txt_d2.Text);
                sw.WriteLine("Top Reinforcement Bar Diameter [d3] = {0} mm", txt_d3.Text);
                sw.WriteLine("Pile Cap Length [LPC] = {0} mm                  Marked as (LPC) in the Drawing", txt_LPC.Text);
                sw.WriteLine("Pile Cap Width [BPC] = {0} mm                   Marked as (BPC) in the Drawing", txt_BPC.Text);
                sw.WriteLine("Pier Length [LPr] = {0} mm                      Marked as (LPr) in the Drawing", txt_LPr.Text);
                sw.WriteLine("Pier Width [BPr] = {0} mm                       Marked as (BPr) in the Drawing", txt_BPr.Text);
                sw.WriteLine("Depth of Pile Cap [DPC] = {0} mm                Marked as (DPC) in the Drawing", txt_DPC.Text);
                sw.WriteLine("Distance   [l1] = {0} mm                        Marked as (l1) in the Drawing", txt_L1.Text);
                sw.WriteLine("Distance   [l2] = {0} mm                         Marked as (l2) in the Drawing", txt_L2.Text);
                sw.WriteLine("Distance   [l3] = {0} mm                        Marked as (l3) in the Drawing", txt_L3.Text);

                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                #endregion
              
                #region STEP 1 : CAPACITY FROM SOIL STRUCTURE INTERACTION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : CAPACITY FROM SOIL STRUCTURE INTERACTION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double L1 = PCBL - FL;
                L1 = double.Parse(L1.ToString("0.000"));
                sw.WriteLine("Pile Length = L1 = PCBL - FL");
                sw.WriteLine("            = {0:f3} - {1:f3}", PCBL, FL);
                sw.WriteLine("            = {0:f3} m", L1);

                double L2 = SL - FL;
                L2 = double.Parse(L2.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("Embedded Length = L2 = SL - FL");
                sw.WriteLine("                = {0:f3} - {1:f3}", SL, FL);
                sw.WriteLine("                = {0:f3} m", L2);
                sw.WriteLine();

                double Ap = Math.PI * D * D / 4.0;
                Ap = double.Parse(Ap.ToString("0.000"));
                sw.WriteLine("Cross Sectional Area of PIle = Ap = π*D*D/4");
                sw.WriteLine("                             = π*{0}*{0}/4", D);
                sw.WriteLine("                             = {0:f3} sq.m", Ap);
                sw.WriteLine();

                #region (A) FOR COHESIONLESS COMPONENT OF SOIL
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("(A) FOR COHESIONLESS COMPONENT OF SOIL :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                #region SKIN FRICTION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("SKIN FRICTION :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                //sw.WriteLine("Layers ,Thickness, Depth below, Surface, φ (deg), δ (deg), γ_sub, P_D, P_Di");
                //sw.WriteLine("Layers ,of Sub soil Layer, scour level(H), Area (As), φ (deg), δ (deg), γ_sub, Pd, Pdi");
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,8} {4,5} {5,7} {6,7} {7,7} {8,7}",
                       "Layers",
                       "Thickness",
                       "Depth below",
                       "Surface",
                       "φ  ",
                       "δ  ",
                       "γ_sub",
                       "P_D",
                       "P_Di");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,8} {4,5} {5,7} {6,7} {7,7} {8,7}",
                                       "",
                                       "of Sub ",
                                       "scour  ",
                                       "Area  ",
                                       "(deg) ",
                                       "(deg) ",
                                       "Ton /",
                                       "γ_sub*H",
                                       "Ton /");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,8} {4,5} {5,7} {6,7} {7,7} {8,7}",
                                                      "",
                                                      "soil Layer",
                                                      "level(H) ",
                                                      "(As)  ",
                                                      "",
                                                      "",
                                                      "cu.m",
                                                      "Ton /",
                                                      "sq.mm");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,8} {4,5} {5,7} {6,7} {7,7} {8,7}",
                                                        "",
                                                        "m     ",
                                                        "m     ",
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        "sq.m",
                                                        "");
                sw.WriteLine("-----------------------------------------------------------------------------");

                for (int i = 0; i < pft_list.Count; i++)
                {
                    sw.WriteLine("{0,-6:f0} {1,10:f3} {2,10:f3} {3,8:f3} {4,6:f1}° {5,6:f1}° {6,7:f3} {7,7:f3} {8,7:f3}",
                        pft_list[i].Layers,
                        pft_list[i].Thickness,
                        pft_list[i].H_DepthBelowScourLevel,
                        pft_list[i].SurfaceArea,
                        pft_list[i].Phi,
                        pft_list[i].Delta,
                        pft_list[i].GammaSub,
                        pft_list[i].P_D,
                        pft_list[i].P_Di);
                }
                sw.WriteLine("-----------------------------------------------------------------------------");

                sw.WriteLine();
                for (int i = 0; i < pft_list.Count; i++)
                {
                    if (i == 0)
                    {
                        sw.WriteLine("P_Di1 = (0 + {0})/2 = {1} Ton/sq.m", pft_list[0].P_D, pft_list[0].P_Di);
                    }
                    else
                    {
                        //sw.WriteLine("P_Di1 = (0 + {0})/2 = {1} Ton/sq.m", pft_list[0].P_D, pft_list[0].P_Di);
                        sw.WriteLine("P_Di{0} = ({1} + {2})/2 = {3} Ton/sq.m", 
                            (i + 1),
                            pft_list[i - 1].P_D,
                            pft_list[i].P_D, 
                            pft_list[i].P_Di);

                    }
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Ultimate Resistance by Skin Friction :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double sk_frc, Rfs;
                sk_frc = 0;
                Rfs = 0;

                List<double> list_dbl = new List<double>();
                for (int i = 0; i < pft_list.Count; i++)
                {
                    sk_frc = pft_list[i].SurfaceArea * K * pft_list[i].P_Di * Math.Tan((Math.PI / 180.0) * pft_list[i].Delta);
                    sk_frc = double.Parse(sk_frc.ToString("0.000"));
                    Rfs += sk_frc;

                    list_dbl.Add(sk_frc);
                    //sw.WriteLine("For Layer {0} : As{1}* K * P_Di{1} * tan δ", pft_list[i].Layers, (i + 1));

                    sw.WriteLine("For Layer {0} : As{1}* K * P_Di{1} * tan δ",
                        pft_list[i].Layers,
                        (i + 1));
                    sw.WriteLine("            = {0} * {1} * {2} * tan {3}",
                         pft_list[i].SurfaceArea,
                         K,
                         pft_list[i].P_Di,
                         pft_list[i].Delta);
                    sw.WriteLine("            = {0} Ton", sk_frc);
                    sw.WriteLine();
                }


                sw.WriteLine("Total Ultimate Resistance due to Skin Friction = Rfs = {0} Ton", Rfs);
                
                for (int i = 0; i < pft_list.Count; i++)
                {
                }

                sw.WriteLine();

                #endregion

                #region END BEARING
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("END BEARING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Ultimate Resistance by End Bearing :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double R_us = Ap * ((1.0 / 2.0) * 0.92 * D * N_gamma);

                int cnt = pft_list.Count;

                if (cnt >= 1)
                {
                    R_us = Ap * ((1.0 / 2.0) * pft_list[cnt - 1].GammaSub * D * N_gamma + (pft_list[cnt-1].P_D * Nq));

                    sw.WriteLine("R_us = Ap * ((1/2) * γ * D * Nγ + P_D{0} * Nq) ", cnt);
                    sw.WriteLine("     = {0:f3} * (0.5 * {1:f3} * {2} * {3:f3} + {4:f3} * {5:f3})) ",
                        Ap,
                        pft_list[cnt - 1].GammaSub,
                        D,
                        N_gamma,
                        pft_list[cnt - 1].P_D,
                        Nq);
                    sw.WriteLine();
                    sw.WriteLine("     = {0:f3} Ton", R_us);
                }
                sw.WriteLine();

                double total_resist = Rfs + R_us;
                sw.WriteLine("Total Ultimate Resistance of Pile = Rfs + R_us");
                sw.WriteLine("                                  = {0:f3} + {1:f3}", Rfs, R_us);
                sw.WriteLine("                                  = {0:f3} Ton", total_resist);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Factor of Safety = FS = {0}", FS);
                double Qus = total_resist / FS;

                sw.WriteLine();
                sw.WriteLine("Safe Load on Pile = {0:f3} / {1} = {2:f3} Ton", total_resist, FS, Qus);
                
                #endregion

                #region END BEARING
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("(B) FOR COHESIVE COMPONENT OF SOIL :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("{0,-6} {1,10} {2,14} {3,14} {4,7} {5,7} {6,7}",
                    "Layers",
                    "Layer",
                    "Depth",
                    "Surface",
                    "",
                    "",
                    "Ultimate");
                sw.WriteLine("{0,-6} {1,10} {2,14} {3,14} {4,7} {5,7} {6,7}",
                    "",
                    "Thickness",
                    "below Scour",
                    "Area  ",
                    "α  ",
                    "c  ",
                    "resistance");
                sw.WriteLine("{0,-6} {1,10} {2,14} {3,14} {4,7} {5,7} {6,7}",
                    "",
                    "[D](m)",
                    "Level[H](m)",
                    "[As](sq.m)",
                    "",
                    "",
                    "[As*α*c](Ton)");
                sw.WriteLine("-----------------------------------------------------------------------------");

                double Rfc = 0.0;

                for (int i = 0; i < pft_list.Count; i++)
                {
                   sk_frc = (pft_list[i].SurfaceArea * pft_list[i].Alpha * pft_list[i].Cohesion);
                   Rfc += sk_frc;
                   sw.WriteLine("{0,-6} {1,10:f3} {2,14:f3} {3,14:f3} {4,7:f3} {5,7:f3} {6,7:f3}",
                   pft_list[i].Layers,
                   pft_list[i].Thickness,
                   pft_list[i].H_DepthBelowScourLevel,
                   pft_list[i].SurfaceArea,
                   pft_list[i].Alpha,
                   pft_list[i].Cohesion,
                   sk_frc);
                }
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("{0,63} {1,7:f3} Ton",
                    "Total Ultimate Resistance = ",
                    Rfc);
                sw.WriteLine();
                //sw.WriteLine("Total Ultimate Resistance = {0:f3} Ton", Rfc);
                //sw.WriteLine();

                double end_brng = Ap * pft_list[cnt - 1].Cohesion * Nc;
                sw.WriteLine("End Bearing = Ap * C{0} * Nc", cnt);
                sw.WriteLine("            = {0} * {1} * {2}", Ap, pft_list[cnt - 1].Cohesion, Nc);
                sw.WriteLine("            = {0:f3} ", end_brng);
                sw.WriteLine();

                double Qu = Rfc + end_brng;
                sw.WriteLine("Total Ultimate Resistance of Pile = Qu");
                sw.WriteLine("    Qu = {0:f3} + {1:f3} = {2:f3} Ton", Rfc, end_brng, Qu);
                sw.WriteLine();
                sw.WriteLine("Factor of Safety = FS = {0}", FS);
                sw.WriteLine();

                double Quc = Qu / FS;
                sw.WriteLine("Safe Load on Pile = {0:f3}/{1} = {2:f3} Ton", Qu, FS, Quc);
                sw.WriteLine();

                double perm_load = Qus + Quc;
                sw.WriteLine("Permissible safe Load on Pile = Qus + Quc");
                sw.WriteLine("                              = {0:f3} + {1:f3}", Qus, Quc);
                sw.WriteLine("                              = {0:f3} Ton", perm_load);
                sw.WriteLine();
                sw.WriteLine("Applied Load on Pile = P = {0} Ton", P);
                sw.WriteLine();

                double self_wt = Ap * L1 * (gamma_c - 1.0);

                sw.WriteLine("Self weight of Pile = Ap * L1 * (γ_c - 1)");
                sw.WriteLine("                    = {0:f3} * {1} * ({2} - 1)", Ap, L1, gamma_c);
                sw.WriteLine("                    = {0:f3} Ton", self_wt);
                sw.WriteLine();

                double total_load = P + self_wt;
                if (total_load < perm_load)
                    sw.WriteLine("Total Load on Pile = {0} + {1:f3} = {2:f3} Ton < {3:f3} Ton, Hence, Safe", P, self_wt, total_load, perm_load);
                else
                    sw.WriteLine("Total Load on Pile = {0} + {1:f3} = {2:f3} Ton > {3:f3} Ton, Hence, Not Safe", P, self_wt, total_load, perm_load);

               
                #endregion

                #region STEP 2 : STRUCTURAL DESIGN OF PILE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : STRUCTURAL DESIGN OF PILE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Pile Dia = D = {0} m = {1} mm", D, (D * 1000));
                D = D * 1000;
                sw.WriteLine();
                double cover = 0.1 * D;
                frmPile_Graph fpg = new frmPile_Graph();

                sw.WriteLine("Cover = d' = 0.1 * D = 0.1 * {0} = {1} mm", D, cover);
                sw.WriteLine();
                sw.WriteLine("Cover / Pile Dia = d' / D = {0} / {1} = {2}", cover, D, (cover / D));
                fpg.txt_ddash.Text = (cover / D).ToString("0.00");
                sw.WriteLine();
                
                double val1 = P * 1000 * 10 / (sigma_ck * D * D);
                sw.WriteLine("Pu/(σ_ck*D*D) = {0}*1000*10/({1}*{2}*{2})", P, sigma_ck, D);
                sw.WriteLine("              = {0:f4}", val1);
                fpg.txt_Pu.Text = val1.ToString("0.0000");
                sw.WriteLine();

                val1 = (AM * 1000 * 10 * 1000) / (sigma_ck * D * D * D);
                sw.WriteLine("Mu/(σ_ck*D**3) = {0}/({1}*{2}^3)", AM, sigma_ck, D);
                sw.WriteLine("               = {0:f4}", val1);
                fpg.txt_Mu.Text = val1.ToString("0.0000");
                sw.WriteLine();
                fpg.txt_sigma_y.Text = fy.ToString();
                fpg.txt_sigma_ck.Text = sigma_ck.ToString();
                fpg.txt_obtaned_value.Text = "0.0";

                fpg.ShowDialog();
                val1 = MyList.StringToDouble(fpg.txt_obtaned_value.Text, 0.0);
                sw.WriteLine("From figure, we get p/σ_ck = {0}", val1);
                sw.WriteLine();
                if (val1 < 0.4)
                {
                    val1 = 0.4;
                }
                sw.WriteLine("In piles provide minimum {0}% Steel.", val1);
                sw.WriteLine();

                double area_mn_st = (val1 / 100) * (Math.PI / 4.0) * D * D;
                sw.WriteLine("Area of Main Steel Reinforcement = As");
                sw.WriteLine("   As = ({0}/100) * (π / 4) * D * D",val1);
                sw.WriteLine("      = ({0}/100) * (π / 4) * {1} * {1}", val1, D);
                sw.WriteLine("      = {0:f2} sq.mm", area_mn_st);
                sw.WriteLine();

                double ar_one_st_br = (Math.PI * d1 * d1) / 4.0;
                sw.WriteLine("Area of one Steel reinforcement bar = π * {0} * {0} / 4", d1);
                sw.WriteLine("                                    = {0:f2} sq.mm", ar_one_st_br);
                sw.WriteLine();

                double total_bar = (int)((area_mn_st / ar_one_st_br) + 1);
                sw.WriteLine("Total number of bars = {0:f2} / {1:f2} = {2:f0}", area_mn_st, ar_one_st_br, total_bar);
                sw.WriteLine();
                sw.WriteLine("Provide {0} numbers T{1} mm dia bars.", total_bar, d1);
                sw.WriteLine();
                sw.WriteLine("Use 8 mm diameter lateral MS bars as Ties / Binders");
                sw.WriteLine("the pitch / spacing = r < 500 mm");
                sw.WriteLine("                        < 16*d1 = 16*{0} = {1} mm", d1, (16 * d1));

                val1 = (int)((16 * d1) / 100.0);
                val1 *= 100;

                double provide_spacing = val1;
                sw.WriteLine("                        < {0} mm", val1);
                sw.WriteLine();

                sw.WriteLine("Provide {0} numbers T{1} mm dia bars with spacing of {2} mm c/c.     Marked as (4) in the Drawing",total_bar,d1, provide_spacing);
                //(4)  Main Bars 10 Nos. Dia. 20 MM. in Piles
                _4 = string.Format("Main Bars {0} Nos. Dia. {1} mm. in Piles", total_bar, d1, provide_spacing);


                #endregion

                #region STEP 3 : STRUCTURAL DESIGN OF PILE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : DESIGN OF PILE CAP :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double n = (m * sigma_cbc) / (sigma_st + (m * sigma_cbc));
                sw.WriteLine("Neutral Axis Factor = n = (m * σ_cbc) / (σ_st + (m * σ_cbc))");
                sw.WriteLine("         = ({0} * {1}) / ({2} + ({0} * {1}))", m, sigma_cbc, sigma_st);
                sw.WriteLine("         = {0:f3}", n);
                sw.WriteLine();

                double j = (1 - (n / 3));
                sw.WriteLine("Lever Arm Factor = j = 1 - (n/3) = 1 - ({0:f3}/3) = {1:f3}", n, j);
                sw.WriteLine();


                double Q = 0.5 * sigma_cbc * j * n;
                sw.WriteLine("Q = 0.5 * σ_cbc * j * n");
                sw.WriteLine("  = 0.5 * {0} * {1:f3} * {2:f3}", sigma_cbc, j, n);
                sw.WriteLine("  = {0:f3}", Q);
                sw.WriteLine();
                double P2 = N * P;
                sw.WriteLine("Sum of Forces on Piles in front row = P2 = N * P");
                sw.WriteLine("  P2 = {0} + {1} = {2} Ton", N, P, P2);
                sw.WriteLine();

                double mom_pier = P2 * ((l1 / 1000.0) - (l2 / 1000.0));
                sw.WriteLine("Moment at the Face of Pier = 260 * ((l1 / 1000.0) - (l2 / 1000.0))");
                sw.WriteLine("     = {0} * (({1} / 1000.0) - ({2} / 1000.0))",P2, l1, l2);
                sw.WriteLine("     = {0:f2} Ton-m", mom_pier);
                sw.WriteLine();

                double P3 = (l1 / 1000.0) * (LPC / 1000.0) * (DPC / 1000.0) * gamma_c;
                sw.WriteLine("Relief due to self wt of Pile Cap = P3");
                sw.WriteLine(" = P3 = (l1 / 1000.0) * (LPC / 1000.0) * (DPC / 1000.0) * γ_c");
                sw.WriteLine(" = ({0} / 1000.0) * ({1} / 1000.0) * ({2} / 1000.0) * {3}", l1, LPC, DPC, gamma_c);
                sw.WriteLine(" = {0:f3} Ton", P3);
                sw.WriteLine();

                double mom_self_wt = P3 * (l1 / (1000.0 * 2));
                sw.WriteLine("Moment due to self wt of Pile Cap");
                sw.WriteLine("  = {0:f3} * (l1 / (1000.0 * 2))", P3);
                sw.WriteLine("  = {0:f3} * ({1} / (1000.0 * 2))", P3, l1);
                sw.WriteLine("  = {0:f3} Ton-m", mom_self_wt);
                sw.WriteLine();

                double total_mom = mom_pier - mom_self_wt;
                sw.WriteLine("Total Moment at the Face of Pier = {0:f3} - {1:f3} = {2:f3} Ton-m", mom_pier, mom_self_wt, total_mom);

                sw.WriteLine();

                
                double M = total_mom / (LPC / 1000.0);
                sw.WriteLine("Moment per Lenear metre = {0:f3} / (LPC/1000)", total_mom);
                sw.WriteLine("                        = {0:f3} / ({1}/1000)", total_mom, LPC);
                sw.WriteLine("                        = {0:f2} Ton-m/m", M);
                sw.WriteLine();


                double req_dep = (M * 10E4) / (Q * 100);
                req_dep = Math.Sqrt(req_dep);

                sw.WriteLine("Depth required = √(({0:f2}*10E4)/(Q*100))", M);
                sw.WriteLine("               = √(({0:f2}*10E4)/({1:f2}*100))", M, Q);
                sw.WriteLine("               = {0:f2} cm = {1:f2} mm", req_dep, (req_dep * 10));

                req_dep = req_dep * 10;
                sw.WriteLine();
                sw.WriteLine("Overall Depth Provided = {0} mm", DPC);
                sw.WriteLine();
                sw.WriteLine("Clear Cover = 175 mm");
                sw.WriteLine();
                double half_bar_dia = d2 / 2.0;
                sw.WriteLine("Half Bar diameter of Steel Reinforcements = {0} m", half_bar_dia);
                sw.WriteLine();

                double eff_dep = DPC - 175 - half_bar_dia;
                sw.WriteLine("Effective Depth Provided = {0} - 175 - {1}", DPC, half_bar_dia);
                if (eff_dep > req_dep)
                {
                    sw.WriteLine("                         = {0:f3} mm > {1:f2} mm, Hence OK", eff_dep, req_dep);
                }
                else
                {
                    sw.WriteLine("                         = {0:f3} mm < {1:f3} mm, Hence NOT OK", eff_dep, req_dep);
                }
                double deff = eff_dep / 10;
                sw.WriteLine();
                sw.WriteLine("deff = {0} mm = {1} cm", eff_dep, deff);
                sw.WriteLine();

                double req_st_renf = (M * 10E4) / (j * sigma_st * deff * 1);
                sw.WriteLine("Required Steel Reinforcement = M * 10E4/(j*σ_st*deff*1)");
                sw.WriteLine("    = {0:f3} * 10E4/({1:f3}*{2}*{3}*1)", M, j, sigma_st, deff);
                sw.WriteLine("    = {0:f3} sq.cm/m", req_st_renf);
                sw.WriteLine();

                double req_min_tension = (0.2 / 100) * deff;
                sw.WriteLine("Required minimum Steel for tension = 0.2%");
                sw.WriteLine("                                   = (0.2/100) * {0}", deff);
                sw.WriteLine("                                   = {0:f3} sq.cm/m", req_min_tension);
                sw.WriteLine();
                sw.WriteLine("Provide Steel Reinforcements 25 Diameter bars @150 mm c/c spacing.    Marked as (2) in the Drawing");
                //(2)  Main Bottom Bars Dia. 25 @ 150 c/c
                _2 = string.Format("Main Bottom Bars Dia. 25 @ 150 c/c");

                
                sw.WriteLine();
                double pro_area_st = (Math.PI * d2 * d2 / 4.0) * (1000.0 /150.0);

                pro_area_st = pro_area_st / 100;

                if (pro_area_st > req_min_tension)
                {
                    sw.WriteLine("Area of Steel Provided at the bottom of the Pile Cap");
                    sw.WriteLine("in Longitudinal direction = {0:f3} sq.cm/m > {1:f3} sq.cm/m, Hence OK",
                        pro_area_st, req_min_tension);
                }
                else
                {
                    sw.WriteLine("Area of Steel Provided at the bottom of the Pile Cap");
                    sw.WriteLine("in Longitudinal direction = {0:f3} sq.cm/m < {1:f3} sq.cm/m, Hence NOT OK",
                        pro_area_st, req_min_tension);
                }
                sw.WriteLine();

                double nom_steel = (0.06 / 100) * deff;
                sw.WriteLine("Steel Provided in Longitudinal derection at the top of ");
                sw.WriteLine("Pile Cap = Nominal Steel = 0.06% of Area");
                sw.WriteLine("         = (0.06/100) * deff");
                sw.WriteLine("         = (0.06/100) * {0}", deff);
                sw.WriteLine("         = {0:f3} sq.cm/m", nom_steel);
                sw.WriteLine();
                sw.WriteLine("Provided {0} mm dia bars at 150 mm c/c spacing.         Marked as (1) in the Drawing", d3);
                //(1)  Main Top Bars Dia. 16 @ 150 c/c
                _1 = string.Format("Main Top Bars Dia. {0} @ 150 c/c", d3);

                sw.WriteLine();

                double area_top = (Math.PI * d3 * d3 / 4.0) * (1000.0 / 150.0);
                sw.WriteLine("Area of Steel Provided = (π*{0}*{0}/4)*(1000/150)", d3);
                sw.WriteLine("                       = {0:f2} sq.mm/m", area_top);
                area_top = area_top / 100;
                sw.WriteLine("                       = {0:f2} sq.cm/m", area_top);

                sw.WriteLine();
                sw.WriteLine("Distribution Steel provided at top and bottom of Pile Cap");
                sw.WriteLine(" {0} mm dia bars at 150 mm c/c spacing", d3);
                sw.WriteLine();
                area_top = (Math.PI * d3 * d3 / 4.0) * (1000.0 / 150.0);
                sw.WriteLine("Area of Steel Provided = (π*{0}*{0}/4)*(1000/150)", d3);
                sw.WriteLine("                       = {0:f2} sq.mm/m", area_top);
                area_top = area_top / 100;
                sw.WriteLine("                       = {0:f2} sq.cm/m", area_top);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Shear Reinforcement :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                deff = deff / 100;
                sw.WriteLine("Critical section at deff = {0} m from face of Pier", deff);
                sw.WriteLine();
                //sw.WriteLine("Factor for Distribution of Load = 0.1375");
                sw.WriteLine();

                //double P4 = P2 * 0.1375;
                double P4 = P2;

                //sw.WriteLine("Reaction on Piles in Front row  = P4 = {0} * 0.1375 = {1:f2} Ton", P2, P4);
                sw.WriteLine("Reaction on Piles in Front row  = P4 = {0:f2} Ton", P4);
                sw.WriteLine();

                //val1 = (((LPC - (l1 - deff)) / 1000.0) * DPC * gamma_c);
                deff = deff * 1000;
                val1 = (LPC / 1000.0) * ((l1 / 1000.0) - (deff / 1000.0)) * (DPC / 1000.0) * gamma_c;
                double tau_v = (P4 - val1) / (deff);

                //double _tau_v = P4 * 10 * (LPC / 1000.0) * ((l1 / 1000.0) - (deff / 1000.0));
                double _tau_v = P4 * 10 * LPC * (l1 - deff);

                tau_v = (_tau_v) / (LPC * deff * 1000);
                tau_v = double.Parse(tau_v.ToString("0.000"));

                sw.WriteLine("Nominnal Shear stress = τ_v");
                sw.WriteLine();
                //sw.WriteLine("τ_v = ((P4 - (LPC * ((LPC - (l1 - deff)) / 1000.0) * DPC * γ_c)) / (deff * 1000.0))");
                //sw.WriteLine("    = (({0:f2} - ({1} * (({1} - ({2} - {3})) / 1000.0) * {4} * {5})) / ({3} * 1000.0))",
                //    P4, LPC, l1, deff, DPC, gamma_c);


                sw.WriteLine("τ_v = (P4*10*LPC*(l1 - deff)) / (LPC * deff * 1000)");
                sw.WriteLine("    = ({0}*10*{1}*({2} - {3})) / ({1} * {3} * 1000)", P4, LPC, l1, deff);
                //sw.WriteLine();
                sw.WriteLine("    = {0:f4} N/sq.mm", tau_v);
                sw.WriteLine();

                deff = deff / 10;
                double percent = (100 * pro_area_st) / (100.0 * deff);
                percent = double.Parse(percent.ToString("0.000"));
                sw.WriteLine("Percent of bottom main reinforcement");
                sw.WriteLine(" p = (100 * {0:f3}) / (100.0 * {1})", pro_area_st, deff);
                sw.WriteLine("   = {0:f3}", percent);
                sw.WriteLine();

                double tau_c = Get_Table_1_Value(percent, (CONCRETE_GRADE)cap_sigma_ck);
                sw.WriteLine("Permissible Shear Stress for p = {0} and for M{1:f0} Concrete", percent, cap_sigma_ck);
                sw.WriteLine("from Table 1 (given at the end of the report).");
                sw.WriteLine();
                if (tau_c > tau_v)
                {
                    sw.WriteLine("τ_c = {0:f2} N/sq.mm > τ_v", tau_c);

                    sw.WriteLine();

                    sw.WriteLine("So, no shear Reinforcement is required and provide");
                    sw.WriteLine("provide minimum shear reinforcement.");
                    sw.WriteLine();
                    double min_shr_renf = 0.0011 * 100 * 250;
                    sw.WriteLine("Minimum Shear Reinforcement = 0.0011 * b * S");
                    sw.WriteLine("                            = 0.0011 * 100 * 250");
                    sw.WriteLine("                            = 27.50 sq.cm/m");
                    sw.WriteLine();
                    sw.WriteLine("Provide 8 mm diameter 42 legged at 200 mm c/c spacing");
                    sw.WriteLine();

                    pro_area_st = (Math.PI * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0);
                    sw.WriteLine();
                    sw.WriteLine("Area of Steel Provided = (π * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0)");
                    sw.WriteLine("                       = {0:f2} sq.mm/m", pro_area_st);

                    pro_area_st = pro_area_st / 100.0;
                    sw.WriteLine("                       = {0:f2} sq.cm/m", pro_area_st);
                }
                else
                {
                    sw.WriteLine("τ_c = {0:f2} N/sq.mm < τ_v", tau_c);
                    sw.WriteLine();

                    double bal_sh_strs = tau_v - tau_c;
                    bal_sh_strs = double.Parse(bal_sh_strs.ToString("0.000"));
                    sw.WriteLine("Balance Shear Stress");
                    sw.WriteLine();
                    sw.WriteLine("τ_v - τ_c = {0} - {1} = {2} N/sq.mm", tau_v, tau_c, bal_sh_strs);
                    sw.WriteLine();

                    double bal_shr_frc = bal_sh_strs * l1 * deff / 1000.0;
                    bal_shr_frc = double.Parse(bal_shr_frc.ToString("0"));
                    sw.WriteLine("Balance Shear Force");
                    sw.WriteLine();
                    sw.WriteLine(" = ({0} * {1} * {2}) / 1000", bal_sh_strs, l1, deff);
                    sw.WriteLine(" = {0} kN", bal_shr_frc);
                    sw.WriteLine();
                    sw.WriteLine("Using 10 mm bars @200 mm c/c            Marked as (3) in the Drawing");
                    //(3)  Shear Reinf. Bars Dia. 10, 42 Legged, Spacing 200 
                    _3 = string.Format("Shear Reinf. Bars Dia. 10, 42 Legged, Spacing 200");

                    sw.WriteLine();

                    double Ast = (bal_shr_frc * 200.0 * 1000.0) / (200.0 * deff);
                    Ast = double.Parse(Ast.ToString("0"));
                    sw.WriteLine("Required Shear Reinforcement Steel");
                    sw.WriteLine("Ast = ({0} * 200.0 * 1000.0) / (200.0 * {1})", bal_shr_frc, deff);
                    sw.WriteLine("    = {0} sq.mm", Ast);
                    sw.WriteLine("    = {0} sq.cm", (Ast / 100.0));
                    sw.WriteLine();
                    double min_shr_renf = 0.0011 * 100 * 250;
                    sw.WriteLine("Minimum Shear Reinforcement = 0.0011 * b * S");
                    sw.WriteLine("                            = 0.0011 * 100 * 250");
                    sw.WriteLine("                            = 27.50 sq.cm/m");
                    sw.WriteLine();
                    if (Ast < min_shr_renf)
                    {
                        sw.WriteLine();
                        sw.WriteLine("Provide 8 mm diameter 42 legged at 200 mm c/c spacing");
                        sw.WriteLine();

                        pro_area_st = (Math.PI * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0);
                        sw.WriteLine();
                        sw.WriteLine("Area of Steel Provided = (π * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0)");
                        sw.WriteLine("                       = {0:f2} sq.mm/m", pro_area_st);

                        pro_area_st = pro_area_st / 100.0;
                        sw.WriteLine("                       = {0:f2} sq.cm/m", pro_area_st);
                    }


                    //sw.WriteLine("τ_c = {0} N/sq.mm < τ_v", tau_c);
                    //sw.WriteLine();
                    //sw.WriteLine("Provide Shear reinforcement for balance of");
                    //sw.WriteLine("Shear Stress (τ_v - τ_c) N/sq.mm");
                    sw.WriteLine();
                }
                #endregion

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 1");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Table_1(sw);
                #region END OF REPORT
                sw.WriteLine();
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

        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                //Length of Pile Cap = LPC = 8800 mm.
                //Width of Pile Cap = BPC = 4300 mm.
                //Thickness of Pile Cap = DPC = 1500 mm.
                //Length of Pier = LPr = 6100 mm.
                //Width of Pier = BPr = 1100 mm.
                //Diameter of Pile = D = 1000 mm.
                //Distance = l1 = 1600 mm.
                //Distance = l2 = 650 mm.
                //Distance = l3 = 1500 mm.

                //(1)  Main Top Bars Dia. 16 @ 150 c/c
                //(2)  Main Bottom Bars Dia. 25 @ 150 c/c
                //(3)  Shear Reinf. Bars Dia. 10, 42 Legged, Spacing 200 
                //(4)  Main Bars 10 Nos. Dia. 20 MM. in Piles


                sw.WriteLine("_LPC=Length of Pile Cap = LPC = {0} mm.", LPC);
                sw.WriteLine("_BPC=Width of Pile Cap = BPC = {0} mm.", BPC);
                sw.WriteLine("_DPC=Thickness of Pile Cap = DPC = {0} mm.", DPC);
                sw.WriteLine("_LPr=Length of Pier = LPr = {0} mm.", LPr);
                sw.WriteLine("_BPr=Width of Pier = BPr = {0} mm.", BPr);
                sw.WriteLine("_D=Diameter of Pile = D = {0} mm.", D * 1000);
                sw.WriteLine("_l1=Distance = l1 = {0} mm.", l1);
                sw.WriteLine("_l2=Distance = l2 = {0} mm.", l2);
                sw.WriteLine("_l3=Distance = l3 = {0} mm.", l3);
                sw.WriteLine("_1={0}", _1);
                sw.WriteLine("_2={0}", _2);
                sw.WriteLine("_3={0}", _3);
                sw.WriteLine("_4={0}", _4);
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

                sw.WriteLine("D = {0}", txt_D.Text);
                sw.WriteLine("P = {0}", txt_P.Text);
                sw.WriteLine("K = {0}", txt_K.Text);
                sw.WriteLine("AM = {0}", txt_AM.Text);
                sw.WriteLine("N_gamma = {0}", txt_N_gamma.Text);
                sw.WriteLine("Nq = {0}", txt_Nq.Text);
                sw.WriteLine("Nc = {0}", txt_Nc.Text);
                sw.WriteLine("FS = {0}", txt_FS.Text);
                sw.WriteLine("PCBL = {0}", txt_PCBL.Text);
                sw.WriteLine("SL = {0}", txt_SL.Text);
                sw.WriteLine("FL = {0}", txt_FL.Text);
                sw.WriteLine("sigma_ck = {0}", txt_sigma_ck.Text);
                sw.WriteLine("fy = {0}", txt_fy.Text);
                sw.WriteLine("gamma_c = {0}", txt_gamma_c.Text);
                sw.WriteLine("Np = {0}", txt_Np.Text);
                sw.WriteLine("N = {0}", txt_N.Text);
                //sw.WriteLine("gamma_sub = {0}", txt_gamma_sub.Text);
                sw.WriteLine("cap_sigma_ck = {0}", txt_cap_sigma_ck.Text);
                sw.WriteLine("cap_fy = {0}", txt_cap_fy.Text);
                sw.WriteLine("sigma_cbc = {0}", txt_sigma_cbc.Text);
                sw.WriteLine("sigma_st = {0}", txt_sigma_st.Text);
                sw.WriteLine("m = {0}", txt_m.Text);
                sw.WriteLine("F = {0}", txt_F.Text);
                sw.WriteLine("d1 = {0}", txt_d1.Text);
                sw.WriteLine("d2 = {0}", txt_d2.Text);
                sw.WriteLine("d3 = {0}", txt_d3.Text);
                sw.WriteLine("LPC = {0}", txt_LPC.Text);
                sw.WriteLine("BPC = {0}", txt_BPC.Text);
                sw.WriteLine("LPr = {0}", txt_LPr.Text);
                sw.WriteLine("BPr = {0}", txt_BPr.Text);
                sw.WriteLine("DPC = {0}", txt_DPC.Text);
                sw.WriteLine("L1 = {0}", txt_L1.Text);
                sw.WriteLine("L2 = {0}", txt_L2.Text);
                sw.WriteLine("L3 = {0}", txt_L3.Text);

                sw.WriteLine("TABLE = SUB_SOIL");
                for (int i = 0; i < pft_list.Count; i++)
                {
                    sw.WriteLine(pft_list[i].ToString());
                }

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
                D = MyList.StringToDouble(txt_D.Text, 0.0);
                P = MyList.StringToDouble(txt_P.Text, 0.0);
                K = MyList.StringToDouble(txt_K.Text, 0.0);
                AM = MyList.StringToDouble(txt_AM.Text, 0.0);
                N_gamma = MyList.StringToDouble(txt_N_gamma.Text, 0.0);
                Nq = MyList.StringToDouble(txt_Nq.Text, 0.0);
                Nc = MyList.StringToDouble(txt_Nc.Text, 0.0);
                FS = MyList.StringToDouble(txt_FS.Text, 0.0);
                PCBL = MyList.StringToDouble(txt_PCBL.Text, 0.0);
                SL = MyList.StringToDouble(txt_SL.Text, 0.0);
                FL = MyList.StringToDouble(txt_FL.Text, 0.0);
                sigma_ck = MyList.StringToDouble(txt_sigma_ck.Text, 0.0);
                fy = MyList.StringToDouble(txt_fy.Text, 0.0);
                gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
                Np = MyList.StringToDouble(txt_Np.Text, 0.0);
                N = MyList.StringToDouble(txt_N.Text, 0.0);
                //gamma_sub = MyList.StringToDouble(txt_gamma_sub.Text, 0.0);
                cap_sigma_ck = MyList.StringToDouble(txt_cap_sigma_ck.Text, 0.0);
                cap_fy = MyList.StringToDouble(txt_cap_fy.Text, 0.0);
                sigma_cbc = MyList.StringToDouble(txt_sigma_cbc.Text, 0.0);
                sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
                m = MyList.StringToDouble(txt_m.Text, 0.0);
                F = MyList.StringToDouble(txt_F.Text, 0.0);
                d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
                d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
                d3 = MyList.StringToDouble(txt_d3.Text, 0.0);
                LPC = MyList.StringToDouble(txt_LPC.Text, 0.0);
                BPC = MyList.StringToDouble(txt_BPC.Text, 0.0);
                LPr = MyList.StringToDouble(txt_LPr.Text, 0.0);
                BPr = MyList.StringToDouble(txt_BPr.Text, 0.0);
                DPC = MyList.StringToDouble(txt_DPC.Text, 0.0);
                l1 = MyList.StringToDouble(txt_L1.Text, 0.0);
                l2 = MyList.StringToDouble(txt_L2.Text, 0.0);
                l3 = MyList.StringToDouble(txt_L3.Text, 0.0);
                Read_Grid_Data();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }
        public void Read_User_Input()
        {
            #region USER DATA INPUT DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";
            bool table_data_find = false;
                        pft_list.Clear();
            
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    if (table_data_find)
                        pft_list.Add(PileFoundationTable.Parse(lst_content[i]));
                    #region SWITCH
                    switch (VarName)
                    {
                        case "D":
                            txt_D.Text = mList.StringList[1].Trim();
                            break;
                        case "P":
                            txt_P.Text = mList.StringList[1].Trim();
                            break;
                        case "K":
                            txt_K.Text = mList.StringList[1].Trim();
                            break;
                        case "AM":
                            txt_AM.Text = mList.StringList[1].Trim();
                            break;
                        case "N_gamma":
                            txt_N_gamma.Text = mList.StringList[1].Trim();
                            break;
                        case "Nq":
                            txt_Nq.Text = mList.StringList[1].Trim();
                            break;
                        case "Nc":
                            txt_Nc.Text = mList.StringList[1].Trim();
                            break;
                        case "FS":
                            txt_FS.Text = mList.StringList[1].Trim();
                            break;
                        case "PCBL":
                            txt_PCBL.Text = mList.StringList[1].Trim();
                            break;
                        case "SL":
                            txt_SL.Text = mList.StringList[1].Trim();
                            break;
                        case "FL":
                            txt_FL.Text = mList.StringList[1].Trim();
                            break;
                        case "sigma_ck":
                            txt_sigma_ck.Text = mList.StringList[1].Trim();
                            break;
                        case "fy":
                            txt_fy.Text = mList.StringList[1].Trim();
                            break;
                        case "gamma_c":
                            txt_gamma_c.Text = mList.StringList[1].Trim();
                            break;
                        case "Np":
                            txt_Np.Text = mList.StringList[1].Trim();
                            break;
                        case "N":
                            txt_N.Text = mList.StringList[1].Trim();
                            break;
                        //case "gamma_sub":
                        //    txt_gamma_sub.Text = mList.StringList[1].Trim();
                        //    break;
                        case "cap_sigma_ck":
                            txt_cap_sigma_ck.Text = mList.StringList[1].Trim();
                            break;
                        case "cap_fy":
                            txt_cap_fy.Text = mList.StringList[1].Trim();
                            break;
                        case "sigma_cbc":
                            txt_sigma_cbc.Text = mList.StringList[1].Trim();
                            break;
                        case "sigma_st":
                            txt_sigma_st.Text = mList.StringList[1].Trim();
                            break;
                        case "m":
                            txt_m.Text = mList.StringList[1].Trim();
                            break;
                        case "F":
                            txt_F.Text = mList.StringList[1].Trim();
                            break;
                        case "d1":
                            txt_d1.Text = mList.StringList[1].Trim();
                            break;
                        case "d2":
                            txt_d2.Text = mList.StringList[1].Trim();
                            break;
                        case "d3":
                            txt_d3.Text = mList.StringList[1].Trim();
                            break;
                        case "LPC":
                            txt_LPC.Text = mList.StringList[1].Trim();
                            break;
                        case "BPC":
                            txt_BPC.Text = mList.StringList[1].Trim();
                            break;
                        case "LPr":
                            txt_LPr.Text = mList.StringList[1].Trim();
                            break;
                        case "BPr":
                            txt_BPr.Text = mList.StringList[1].Trim();
                            break;
                        case "DPC":
                            txt_DPC.Text = mList.StringList[1].Trim();
                            break;
                        case "L1":
                            txt_L1.Text = mList.StringList[1].Trim();
                            break;
                        case "L2":
                            txt_L2.Text = mList.StringList[1].Trim();
                            break;
                        case "L3":
                            txt_L3.Text = mList.StringList[1].Trim();
                            break;
                        case "TABLE":
                            table_data_find = true;
                            break;
                    }
                    #endregion
                }

                dgv.Rows.Clear();
                for (int i = 0; i < pft_list.Count; i++)
                {
                    dgv.Rows.Add(i + 1, 
                        pft_list[i].Layers, 
                        pft_list[i].Thickness, 
                        pft_list[i].Phi,
                        pft_list[i].Alpha,
                        pft_list[i].Cohesion,
                        pft_list[i].GammaSub);
                }
                Checked_Grid();
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
            #endregion
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
            kPath = Path.Combine(kPath, "Bridge Foundation");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Pile Foundation");

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
                this.Text = "DESIGN OF RCC PILE FOUNDATION : " + value;
                user_path = value;

                
                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RCC_PILE_FOUNDATION");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Found_Pile_Found.TXT");
                user_input_file = Path.Combine(system_path, "RCC_PILE_FOUNDATION.FIL");
                user_drawing_file = Path.Combine(system_path, "RCC_PILE_FOUNDATION_DRAWING.FIL");

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

        #endregion

        public void Read_Grid_Data()
        {
            try
            {
                pft_list.Clear();
                PileFoundationTable pft = null;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    pft = new PileFoundationTable();
                    
                    pft.Layers = MyList.StringToInt(dgv[1, i].Value.ToString(), 0);
                    pft.Thickness = double.Parse(dgv[2, i].Value.ToString());
                    pft.Phi = double.Parse(dgv[3, i].Value.ToString());
                    pft.Alpha = double.Parse(dgv[4, i].Value.ToString());
                    pft.Cohesion = double.Parse(dgv[5, i].Value.ToString());
                    pft.GammaSub = double.Parse(dgv[6, i].Value.ToString());
                    pft_list.Add(pft);
                }
            }
            catch (Exception ex) { }
        }
        public double Get_Table_1_Value(double percent, CONCRETE_GRADE con_grade)
        {
            return iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);

        }
        public void Write_Table_1(StreamWriter sw)
        {
            List<string> lst_content = iApp.Tables.Get_Tables_Permissible_Shear_Stress();

            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();
        }

        private void frmPileFoundation_Load(object sender, EventArgs e)
        {
            pft_list = new PileFoundationTableCollection(1);


            PileFoundationTable pft = new PileFoundationTable();

            pft.Layers = 1;
            pft.Thickness = 10.00;
            pft.Phi = 12;
            pft.Alpha = 0.5;
            pft.Cohesion = 2.5;
            pft.GammaSub = 0.92;

            //pft_list.Add(pft);
            dgv.Rows.Add(1, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);


            pft = new PileFoundationTable();

            pft.Layers = 2;
            pft.Thickness = 3.618;
            pft.Phi = 23;
            pft.Alpha = 0.5;
            pft.Cohesion = 0;
            pft.GammaSub = 0.92;
            //pft_list.Add(pft);
            dgv.Rows.Add(2, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);
            Checked_Grid();
        }

        private void Checked_Grid()
        {
            double d = 0;

            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                dgv[0, i].Value = i + 1;

                for (int k = 2; k < dgv.ColumnCount; k++)
                {
                    if (dgv[k, i].Value != null)
                    {
                        if (!double.TryParse(dgv[k, i].Value.ToString(), out d))
                            d = 0.0;
                    }
                    else
                    {
                        d = 0.0;
                    }
                    dgv[k, i].Value = d.ToString("0.000");
                }
            }
        }
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            Checked_Grid();
        }
    }
    class PileFoundationTable
    {
        int sl_n, layers;
        double thickness, phi, alpha, cohesion, H, thick_sub_soil_layer, surface_area, delta, gamma_sub, p_d, p_di;
        
        public PileFoundationTable()
        {
            sl_n = -1;
            thickness = 0.0d; 
            phi = 0.0d; 
            alpha = 0.0d;
            cohesion = 0.0d;
            H = 0.0d;
            thick_sub_soil_layer = 0.0d;
            surface_area = 0.0d; 
            delta = 0.0d;
            gamma_sub = 0.0d; 
            p_d = 0.0d;
            p_di = 0.0d;
        
        }
        public int SL_No
        {
            get
            {
                return sl_n;
            }
            set
            {
                sl_n = value;
            }
        }
        public int Layers
        {
            get
            {
                return layers;
            }
            set
            {
                layers = value;
            }
        }
        public double Thickness
        {
            get
            {
                return thickness;
            }
            set
            {
                thickness = double.Parse(value.ToString("0.000"));
            }
        }
        public double Phi
        {
            get
            {
                return phi;
            }
            set
            {
                phi = double.Parse(value.ToString("0.000"));
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
                alpha = double.Parse(value.ToString("0.000"));
            }
        }
        public double Cohesion
        {
            get
            {
                return cohesion;
            }
            set
            {
                cohesion = double.Parse(value.ToString("0.000"));
            }
        }
        public double H_DepthBelowScourLevel
        {
            get
            {
                return H;
            }
            set
            {
                H = double.Parse(value.ToString("0.000"));
            }
        }
        public double ThicknessSubSoil
        {
            get
            {
                return thick_sub_soil_layer;
            }
            set
            {
                thick_sub_soil_layer = double.Parse(value.ToString("0.000"));
            }
        }
        public double SurfaceArea
        {
            get
            {
                return surface_area;
            }
            set
            {
                surface_area = double.Parse(value.ToString("0.000"));
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
                delta = double.Parse(value.ToString("0.000"));
            }
        }
        public double GammaSub
        {
            get
            {
                return gamma_sub;
            }
            set
            {
                gamma_sub = double.Parse(value.ToString("0.000"));
            }
        }
        public double P_D
        {
            get
            {
                return p_d;
            }
            set
            {
                p_d = double.Parse(value.ToString("0.000"));
            }
        }
        public double P_Di
        {
            get
            {
                return p_di;
            }
            set
            {
                p_di = double.Parse(value.ToString("0.000"));
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", Layers, Thickness, Phi, Alpha, Cohesion, GammaSub); 
        }
        public static PileFoundationTable Parse(string str)
        {
            string temp = str;

            temp = MyList.RemoveAllSpaces(temp);
            MyList mList = new MyList(temp, ' ');

            PileFoundationTable pft = new PileFoundationTable();
            if (mList.Count == 6)
            {
                pft.Layers = mList.GetInt(0);
                pft.Thickness = mList.GetDouble(1);
                pft.Phi = mList.GetDouble(2);
                pft.Alpha = mList.GetDouble(3);
                pft.Cohesion = mList.GetDouble(4);
                pft.GammaSub = mList.GetDouble(5);
            }
            else
                throw new Exception("Wrong Data!");
            return pft;
        }
    
    }
    class PileFoundationTableCollection : IList<PileFoundationTable>
    {
        List<PileFoundationTable> list = null;
        double pile_dia, gama_sub;
        public PileFoundationTableCollection(double pile_diameter)
        {
            list = new List<PileFoundationTable>();
            pile_dia = pile_diameter;
        }

        #region IList<PileFoundationTable> Members

        public int IndexOf(PileFoundationTable item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Layers == item.Layers) && (item.Thickness == list[i].Thickness))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, PileFoundationTable item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public PileFoundationTable this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<PileFoundationTable> Members

        public void Add(PileFoundationTable item)
        {
            double val = 0.0;

            int cnt = list.Count - 1;
            if (cnt >= 0)
            {
                item.SL_No = cnt + 2;
                item.H_DepthBelowScourLevel = item.Thickness + list[cnt].Thickness;
                item.SurfaceArea = Math.PI * pile_dia * item.Thickness;
                if (item.Cohesion == 0.0)
                {
                    item.Delta = item.Phi;
                }
                else
                {
                    item.Delta = (2.0 / 3.0) * item.Phi;
                }
                item.P_D = item.H_DepthBelowScourLevel * item.GammaSub;
                item.P_Di = (list[cnt].P_D + item.P_D) / 2.0;
            }
            else
            {
                item.SL_No = 1;
                item.H_DepthBelowScourLevel = item.Thickness;
                item.SurfaceArea = Math.PI * pile_dia * item.Thickness;
                if (item.Cohesion == 0.0)
                {
                    item.Delta = item.Phi;
                }
                else
                {
                    item.Delta = (2.0 / 3.0) * item.Phi;
                }
                item.P_D = item.H_DepthBelowScourLevel * item.GammaSub;
                item.P_Di = (0 + item.P_D) / 2.0;
            }
            //item.GammaSub = gama_sub;

            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(PileFoundationTable item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(PileFoundationTable[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(PileFoundationTable item)
        {
            int i = IndexOf(item);
            if (i != -1)
            {
                RemoveAt(i);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<PileFoundationTable> Members

        public IEnumerator<PileFoundationTable> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }
}
