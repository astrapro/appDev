using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstraInterface.Interface;
using AstraInterface.DataStructure;
using System.IO;

namespace AstraFunctionOne.BridgeDesign.Prestressed
{
    public partial class frmPreStressedBridgePostTension : Form, IReport
    {
        IApplication iApp = null;
        string rep_file_name = "";
        string file_path = "";
        string system_path = "";
        string user_input_file = "";
        string user_drawing_file = "";
        string user_path = "";
        bool is_process = false;

        double L, a, d,b, bw, d1, d2, fck, doc, fci, NS, fy, dos, sigma_cb, sigma_st;
        double SF, m, Q, j, FS, fp;
        double eta; //η
        
        double DL_BM_OG, DL_BM_IG;
        double LL_BM_OG, LL_BM_IG;
        double DL_SF_OG, DL_SF_IG;
        double LL_SF_OG, LL_SF_IG;

        double space_long, space_cross;


        string _A, _B, _C, _D, _E, _F, _G, _H, _I1, _I2, _J, _K, _L;

        string ref_string = "";

        public frmPreStressedBridgePostTension(IApplication app)
        {
            InitializeComponent();
            iApp = app;
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            //iApp.SetDrawingFile_Path(drawing_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER", "");
            iApp.SetDrawingFile_Path(user_drawing_file, "PreStressed_Main_Girder", "");
        }

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
                sw.WriteLine("\t\t*            ASTRA Pro Release 22             *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*       DESIGN OF PRESTRESSED BRIDGE          *");
                sw.WriteLine("\t\t*     POST TENSIONED RCC LONG. GIRDER         *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                #endregion

                #region USER'S DATA
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Outer Girder Dead Load Bending Moment  = {0} kN-m", txt_DL_BM_OG.Text);
                sw.WriteLine("Inner Girder Dead Load Bending Moment  = {0} kN-m", txt_DL_BM_IG.Text);

                sw.WriteLine("Outer Girder Live Load Bending Moment  = {0}kN-m", txt_LL_BM_OG.Text);
                sw.WriteLine("Inner Girder Live Load Bending Moment  = {0}kN-m", txt_LL_BM_IG.Text);

                sw.WriteLine("Outer Girder Dead Load Shear Force = {0} kN", txt_DL_SF_OG.Text);
                sw.WriteLine("Inner Girder Dead Load Shear Force = {0} kN", txt_DL_SF_IG.Text);

                sw.WriteLine("Outer Girder Live Load Shear Force = {0} kN", txt_LL_SF_OG.Text);
                sw.WriteLine("Inner Girder Live Load Shear Force = {0} kN", txt_LL_SF_IG.Text);

                sw.WriteLine("Span of Girder [L] = {0} m", txt_L.Text);
                sw.WriteLine("Dimension [a] = {0} mm                 Marked as (A) in the Drawing", txt_a.Text);
                _A = string.Format("{0} mm ", txt_a.Text);

                sw.WriteLine("Overall Depth of Girder [d] = {0} mm   Marked as (B) in the Drawing", txt_d.Text);
                _B = string.Format("{0} mm ", txt_d.Text);

                sw.WriteLine("Dimension [b] = {0} mm                 Marked as (C) in the Drawing", txt_b.Text);
                _C = string.Format("{0} mm ", txt_b.Text);


                sw.WriteLine("Dimension [bw] = {0} mm                Marked as (D) in the Drawing", txt_bw.Text);
                _D = string.Format("{0} mm ", txt_bw.Text);
                
                sw.WriteLine("Dimension [d1] = {0} mm                Marked as (E) in the Drawing", txt_d1.Text);
                _E = string.Format("{0} mm ", txt_d1.Text);

                sw.WriteLine("Dimension [d2] = {0} mm                Marked as (F) in the Drawing", txt_d2.Text);
                _F = string.Format("{0} mm ", txt_d2.Text);

                sw.WriteLine("Spacing of Main Long Girders = {0} mm  Marked as (G) in the Drawing", space_long * 1000);
                //(G) = Spacing of Main Girders 2500 mm.
                _G = string.Format("Spacing of Main Girders {0} mm ", space_long * 1000);

                sw.WriteLine("Spacing of Cross Girders = {0} mm      Marked as (H) in the Drawing", space_cross * 1000);
                //(H) = Spacing of Cross Girders 5000 mm.
                _H = string.Format("Spacing of Cross Girders {0} mm ", space_cross * 1000);
               
                
                sw.WriteLine("Concrete Grade [fck] = M {0} = {0} N/sq.mm", txt_fck.Text);
                sw.WriteLine("Post Tension Cable Diameter [doc] = {0} mm", txt_doc.Text);
                sw.WriteLine("Cube Strength at Transfer [fci] = {0} N/sq.mm", txt_fci.Text);
                sw.WriteLine("Freyssinet Anchorable Number of Strands [NS] = {0}", txt_NS.Text);
                sw.WriteLine("Steel Grade [fy] = Fe {0} = {0}  N/sq.mm", txt_fy.Text);
                sw.WriteLine("Diameter of Strands [dos] = {0} mm", txt_dos.Text);
                sw.WriteLine("Permissible compressive stress in concrete [σ_cb] = {0}  N/sq.mm", txt_sigma_cb.Text);
                sw.WriteLine("Permissible tensile stress in steel [σ_st] = {0}  N/sq.mm", txt_sigma_st.Text);
                sw.WriteLine("Strand Factor [SF] = {0}", txt_SF.Text);
                sw.WriteLine("Moduler Ratio [m] = {0}", txt_m.Text);
                sw.WriteLine("Moment Factor [Q] = {0}", txt_Q.Text);
                sw.WriteLine("Lever Arm Factor [j] = {0}", txt_j.Text);
                sw.WriteLine("Loss Ratio [n] = {0}", txt_eta.Text);
                sw.WriteLine("Force per Strand [FS] = {0} kN", txt_FS.Text);
                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

               
                #region STEP 1 : CROSS SECTION OF DECK SLAB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : PROPERTIES OF MAIN GIRDER SECTION ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double _x = ((bw / 2.0) * (d - d1 - d2) * (d - d1 - d2) + (b * d2) * (d - d1 - d2 + (d2 / 2.0)) - (b * d1 * (d1 / 2.0))) / ((a * d1) + (bw * (d - d1 - d2)) + (b * d2));

                double x1 = (bw / 2.0) * (d - d1 - d2) * (d - d1 - d2);
                double x2 = (a * d2) * (d - d1 - d2 + (d2 / 2.0));
                double x3 = (b * d1 * (d1 / 2.0));
                double x4 = ((b * d1) + (bw * (d - d1 - d2)) + (a * d2));


                _x = (x1 + x2 - x3) / x4;

                double x = (int)(_x / 10.0);
                x += 1;
                x *= 10;


                sw.WriteLine("Depth of Neutral Axis = x = (x1 + x2 + x3) / x4");
                sw.WriteLine();

                x1 = double.Parse(x1.ToString("0.000"));
                sw.WriteLine("where x1 = (w / 2.0) * (d - d1 - d2) * (d - d1 - d2)", x1);
                sw.WriteLine("         = ({0} / 2.0) * ({1} - {2} - {3})^2", bw, d, d1, d2);
                sw.WriteLine("         = {0:E2} ", x1);
                sw.WriteLine();


                x2 = double.Parse(x2.ToString("0.000"));
                sw.WriteLine(" and  x2 = (b * d2) * (d - d1 - d2 + (d2 / 2.0))");
                sw.WriteLine("         = ({0} * {1}) * ({2} - {3} - {1} + ({1} / 2.0))", b, d2, d, d1);
                sw.WriteLine("         = {0:E2} ", x2);
                sw.WriteLine();

                x3 = double.Parse(x3.ToString("0.000"));
                sw.WriteLine(" and  x3 = (a * d1 * (d1 / 2.0))");
                sw.WriteLine("         = ({0} * {1} * ({1} / 2.0)) ", a, d1);
                sw.WriteLine("         = {0:E2}", x3);
                sw.WriteLine();


                x4 = double.Parse(x4.ToString("0.000"));
                sw.WriteLine(" and  x4 = ((a * d1) + (w * (d - d1 - d2)) + (b * d2))");
                sw.WriteLine("         = (({0} * {1}) + ({2} * ({3} - {1} - {4})) + ({5} * {4}))", a, d1, bw, d, d2, b);
                sw.WriteLine("         = {0:E2}", x4);
                sw.WriteLine();

                _x = double.Parse(_x.ToString("0.000"));
                sw.WriteLine("So, Depth of Neutral Axis = x = (x1 + x2 + x3) / x4");
                sw.WriteLine("                         = ({0:E2} + {1:E2} + {2:E2}) / {3:E2}", x1, x2, x3, x4);
                sw.WriteLine("                         = {0} mm", _x);
                sw.WriteLine("                         ≈ {0} mm", x);
                sw.WriteLine();


                double yt = x + d1;
                sw.WriteLine("yt = x + d1 = {0} + {1} = {2}", x, d1, yt);
                sw.WriteLine();
                double yb = d - yt;
                sw.WriteLine("yb = d - yt = {0} + {1} = {2}", d, yt, yb);
                sw.WriteLine();

                double I = ((b * d1) * (x + (d1 / 2.0)) * (x + (d1 / 2.0)))
                     + ((bw * x * x * x) / 3.0)
                     + ((bw * ((d - d1 - d2 - x) * (d - d1 - d2 - x) * (d - d1 - d2 - x))) / 3.0)
                     + (a * d2 * ((d - d1 - d2 - x + (d2 / 2.0)) * (d - d1 - d2 - x + (d2 / 2.0))));

                double I1 = ((a * d1) * (x + (d1 / 2.0)) * (x + (d1 / 2.0)));
                double I2 = ((bw * x * x * x) / 3.0);
                double I3 = ((bw * ((d - d1 - d2 - x) * (d - d1 - d2 - x) * (d - d1 - d2 - x))) / 3.0);
                double I4 = (b * d2 * ((d - d1 - d2 - x + (d2 / 2.0)) * (d - d1 - d2 - x + (d2 / 2.0))));
                
                
                I =(int)( I / 10E7);

                I1 = double.Parse(I1.ToString("0.00"));
                I2 = double.Parse(I2.ToString("0.00"));
                I3 = double.Parse(I3.ToString("0.00"));
                I4 = double.Parse(I4.ToString("0.00"));

                sw.WriteLine("Moment of Inertia = I = I1 + I2 + I3 + I4");
                sw.WriteLine();
                sw.WriteLine("where I1 = ((a * d1) * (x + (d1 / 2.0))^2)");
                sw.WriteLine("         = (({0} * {1}) * ({2} + ({1} / 2.0))^2)))", a, d1, x);
                sw.WriteLine("         = {0:E2} ", I1);
                sw.WriteLine();
                sw.WriteLine("  and I2 = ((bw * x**3) / 3.0)");
                sw.WriteLine("         = (({0} * {1}^3) / 3.0)", bw, x);
                sw.WriteLine("         = {0:E2}", I2);
                sw.WriteLine();
                sw.WriteLine("  and I3 = ((bw * (d - d1 - d2 - x)^3) / 3.0)");
                sw.WriteLine("         = (({0} * ({1} - {2} - {3} - {4})^3) / 3.0)", bw, d, d1, d2, x);
                sw.WriteLine("         = {0:E2} ", I3);
                sw.WriteLine();
                sw.WriteLine("  and I4 = (b * d2 * (d - d1 - d2 - x + (d2 / 2.0))^3)");
                sw.WriteLine("         = ({0} * {1} * ({2} - {3} - {1} - {4} + ({1} / 2.0))^3)", b, d2, d, d1, x);
                sw.WriteLine("         = {0:E2} ", I4);
                sw.WriteLine();
                sw.WriteLine(" I = I1 + I2 + I3 + I4");
                sw.WriteLine("   = {0:E2} + {1:E2} + {2:E2} + {3:E2}", I1, I2, I3, I4);
                sw.WriteLine("   = {0} * 10E7", I);
                sw.WriteLine();


                double A = b * d1 + bw * (d - d1 - d2) + (a * d2);

                A = (int)(A / 10E3);
                sw.WriteLine();
                sw.WriteLine("A = a * d1 + bw * (d - d1 - d2) + (b * d2)");
                sw.WriteLine("  = {0} * {1} + {2} * ({3} - {1} - {4}) + ({5} * {4})", a, d1, bw, d, d2, b);
                sw.WriteLine("  = {0} * 10E3", A);
                sw.WriteLine();


                double Zt = ((I * 10E7) / yt);
                double Zb = ((I * 10E7) / yb);
                sw.WriteLine("Zt = (I * 10E7) / yt");
                sw.WriteLine("   = ({0} * 10E7) / {1}", I, yt);
                sw.WriteLine("   = {0:E2}", Zt);
                sw.WriteLine();
                sw.WriteLine("Zb = (I * 10E7) / yb");
                sw.WriteLine("   = ({0} * 10E7) / {1}", I, yb);
                sw.WriteLine("   = {0:E2}", Zb);
                

                #endregion

                #region STEP 2 : PERMISSIBLE STRESSES
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : PERMISSIBLE STRESSES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                
                sw.WriteLine();
                sw.WriteLine("σ_cb = {0} N/sq.mm", sigma_cb);
                sw.WriteLine();
                sw.WriteLine("σ_st = {0}", sigma_st);
                sw.WriteLine();
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine();
                sw.WriteLine("fci = {0}", fci);
                sw.WriteLine();

                double fct = 0.45 * fci;
                fct = double.Parse(fct.ToString("0"));
                sw.WriteLine("fct = 0.45 * fci = 0.45 * {0} = {1} N/sq.mm", fci, fct);
                sw.WriteLine();


                // *** Problem fck = 20, but here use 50 why?
                double _fck = fck;

                //fck = 50;
                double fcw = 0.33 * fck;
                fcw = double.Parse(fcw.ToString("0.00"));
                sw.WriteLine("fcw = 0.33 * fck = 0.33 * {0} = {1} N/sq.mm", fck, fcw);
                sw.WriteLine();

                double ftt = 0;
                double ftw = 0;
                sw.WriteLine("ftt = 0 (Member class type = 1)");
                sw.WriteLine();
                sw.WriteLine("ftw = 0 (Member class type = 1)");
                sw.WriteLine();

                double Ec = 5700.0 * Math.Sqrt(fck);

                Ec = double.Parse(Ec.ToString("0"));
                sw.WriteLine("Ec = 5700 * √fck = 5700 * √{0} = {1} N/sq.mm = {2} kN/sq.mm", fck, Ec,(int) (Ec / 1000.0));
                sw.WriteLine();
                sw.WriteLine("η = {0}", eta);
                sw.WriteLine();


                double fbr = eta * fct - ftw;
                sw.WriteLine("fbr = η * fct - ftw");
                sw.WriteLine("    = {0} * {1} - {2}", eta, fct, ftw);
                sw.WriteLine("    = {0} N/sq.mm", fbr);
                sw.WriteLine();

                double ftr = fcw - eta * ftt;
                sw.WriteLine("ftr = fcw - η * ftt");
                sw.WriteLine("    = {0} - {1} * {2}", fcw, eta, ftt);
                sw.WriteLine("    = {0} N/sq.mm", ftr);
                sw.WriteLine();

                double Mg = DL_BM_OG;
                double Mq = LL_BM_OG;

                double Md = Mg + Mq;
                sw.WriteLine("Mg = {0} kN-m", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m", Mq);
                sw.WriteLine();
                sw.WriteLine("Md = Mg + Mq = {0} + {1} = {2} kN-m", Mg, Mq, Md);
                sw.WriteLine();

                double finf = (ftw / eta) + (Md * 10e5)/ (eta * Zb);
                finf = double.Parse(finf.ToString("0.00"));
                sw.WriteLine("finf = (ftw / η) + Md / (η * Zb)");
                sw.WriteLine("     = ({0} / {1}) + {2}*10^6 / ({1} * {3:E2})", ftw, eta, Md, Zb);
                sw.WriteLine("     = {0:E2} N/sq.mm", finf);
                sw.WriteLine();
               
                double _Zb = (Mq*10E5 + (1 - eta) * Mg*10E5) / fbr;
                sw.WriteLine("Zb = [Mq + (1 - η) * Mg] / fbr");
                sw.WriteLine("   = [{0}*10^6  + (1 - {1}) * {2}*10^6 ] / {3}", Mq, eta, Mg, fbr);

                //_Zb = (_Zb / 10E7);
                //Zb = (Zb / 10E7);

                if (_Zb < Zb)
                {
                    sw.WriteLine("   = {0:E2} Cu.mm < {1:E2} Cu.mm", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine("So, the girder is adequate.");
                }
                else
                {
                    sw.WriteLine("   = {0} * 10E7 Cu.mm > {1} * 10E7 Cu.mm", _Zb, Zb);
                    sw.WriteLine();
                    sw.WriteLine("So, the girder is not adequate.");
                }

                #endregion

                #region STEP 3 : PRESTRESSING FORCE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : PRESTRESSING FORCE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double cover = 200;
                sw.WriteLine("Allowing for two rows of Cables, required Cover = 200 mm");
                sw.WriteLine();

                double e = (d - yt - cover);
                sw.WriteLine("Maximum Possible eccentricity = e = (d - yt - cover) ");
                sw.WriteLine("                              = ({0} - {1} - {2})", d, yt, cover);
                sw.WriteLine("                              = {0} mm", e);
                sw.WriteLine();
                sw.WriteLine("Prestressing Force is obtained as");
                sw.WriteLine();


                double p = (A * 10E3 * finf * Zb ) / ((Zb) + (A * 10E3 * e));
                sw.WriteLine("p = (A * finf * Zb) / (Zb  + A * e)");
                sw.WriteLine("  = ({0}*10E3 * {1} * {2:E2}) / ({2:E2} + {0}*10E3 * {3})", A, finf, Zb, e);
                sw.WriteLine("  = {0:E2} N", p);
                p = p / 1000.0;
                sw.WriteLine("  = {0:E2} kN", p);
                sw.WriteLine();

                double Facts = SF;

                double Pk = NS * Facts * FS;
                Pk = double.Parse(Pk.ToString("0"));
                sw.WriteLine("Force in each Cable = Ns * Facts * Fs");
                sw.WriteLine("                    = {0} * {1} * {2}", NS, Facts, FS);
                sw.WriteLine("                    = {0} kN", Pk);
                sw.WriteLine();


                double Nc = p / Pk;
                sw.WriteLine("Required Number of Cables = Nc = {0:E2} / {1}", p, Pk);
                sw.WriteLine("                          = {0:f2} ", Nc);

                Nc = (int)Nc;

                Nc += 1;
                sw.WriteLine("                          = {0} ", Nc);
                sw.WriteLine();


                double ar_ech_strnd = Facts * Math.PI * dos * dos / 4.0;
                ar_ech_strnd = double.Parse(ar_ech_strnd.ToString("0"));
                sw.WriteLine("Area of each Strand = (Fact * π * dos*dos)/4.0");
                sw.WriteLine("                    = ({0} * π * {1}*{1}) / 4.0", Facts, dos);
                sw.WriteLine("                    = {0:f0} sq.mm", ar_ech_strnd);
                sw.WriteLine();
                sw.WriteLine("A cable contains NS = {0} strands,", NS);

                double total_area1 = NS * ar_ech_strnd;
                total_area1 = double.Parse(total_area1.ToString("0"));
                sw.WriteLine("Total Area = {0} * {1}", NS, ar_ech_strnd);
                sw.WriteLine("           = {0} sq.mm", total_area1);
                sw.WriteLine();

                double total_area2 = Nc * total_area1;
                sw.WriteLine("For {0} Cables, Total Area = {0} * {1} = {2} sq.mm        Marked as (I) in the Drawing", Nc, total_area1, total_area2);
                //(I) = Total 5 nos. Prestressing Cables.
                _I1 = string.Format("Total {0:f0} nos. Prestressing Cables.", Nc);
                // 7 nos. Strands per Cable, Are of each = 141 sq.mm
                _I2 = string.Format("{0:f0} nos. Strands per Cable, Are of each = {1} sq.mm", NS, ar_ech_strnd);
                

                
                sw.WriteLine();
                sw.WriteLine("The arrangement of Cables are shown in the Drawing.");
               


                #endregion

                #region STEP 4 : PERMISSIBLE TENDON ZONE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : PERMISSIBLE TENDON ZONE ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At support Section,");
                sw.WriteLine();


                double e_less_value = ((Zb * fct) / (p*1000)) - (Zb / (A * 10E3));
                e_less_value = double.Parse(e_less_value.ToString("0"));
                sw.WriteLine("e <= ((Zb*fct)/p) - (Zb/A)");
                sw.WriteLine("  <= (({0:E2} * {1})/{2:E2}) - ({0:E2}/{4}*10^6)", Zb, fct, p, _Zb, A);
                sw.WriteLine("  <= {0} mm", e_less_value);
                sw.WriteLine();

                double e_greater_value = (Zb * ftw / (eta * p*1000)) - (Zb/ (A * 10E3));
                e_greater_value = double.Parse(e_greater_value.ToString("0"));
                sw.WriteLine("and e >= (Zb*ftw/(η*p)) - (Zb/A)");
                sw.WriteLine("      >= ({0:E2} * {1}/({2}*{3:E2})) - ({0:E2}/{4}*10^6)", Zb, ftw, eta, p, A);
                sw.WriteLine("      >= {0} mm", e_greater_value);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the {0} Cables are arranged to follow a parabolic Profile with", Nc);

                double Ecg = 180;
                sw.WriteLine("the resultant force having an eccentricity of Ecg = {0} mm towards", Ecg);
                sw.WriteLine("the soffit at the support section. The position of Cables at Support");
                sw.WriteLine("Section is shown in the drawing.");
                sw.WriteLine();
               
                #endregion

                #region STEP 5 : CHECK FOR STRESSES
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : CHECK FOR STRESSES ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For section at centre of span we have, ");
                sw.WriteLine();
                sw.WriteLine("p = {0:E2} kN", p);
                sw.WriteLine();
                sw.WriteLine("e = {0} mm", e);
                sw.WriteLine();

                A = (A / 100);
                sw.WriteLine("A = {0}*10^6 sq.mm", A);
                sw.WriteLine();
                sw.WriteLine("Zt = {0:E2}", Zt);
                sw.WriteLine();
                sw.WriteLine("Zb = {0:E2}", Zb);
                sw.WriteLine();
                sw.WriteLine("η = {0}", eta);
                sw.WriteLine();
                sw.WriteLine("Mg = {0} kN-m", Mg);
                sw.WriteLine();
                sw.WriteLine("Mq = {0} kN-m", Mq);
                sw.WriteLine();


                double p_by_A = (p * 1000) / (A * 10E5);
                p_by_A = double.Parse(p_by_A.ToString("0.00"));
                sw.WriteLine("p/A = {0:f0} * 1000 / ({1} * 10^6) = {2:f2} N/sq.mm", p, A, p_by_A);
                sw.WriteLine();

                double val1, val2;

                val1 = p * 1000 * e / (Zt);
                val1 = double.Parse(val1.ToString("0.00"));
                sw.WriteLine("p*e/Zt = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zt);
                sw.WriteLine("       = {0:f2} N/sq.mm", val1);
                sw.WriteLine();

                val2 = p * 1000 * e / (Zb);
                val2 = double.Parse(val2.ToString("0.00"));
                sw.WriteLine("p*e/Zb = {0:f0} * 1000 * {1} / ({2:E2})", p, e, Zb);
                sw.WriteLine("       = {0:f2} N/sq.mm", val2);
                sw.WriteLine();

                double Mg_by_Zt = Mg * 10E5 / (Zt);
                Mg_by_Zt = double.Parse(Mg_by_Zt.ToString("0.00"));
                sw.WriteLine("Mg/Zt = {0} * 10E5 / ({1:E2})", Mg, Zt);
                sw.WriteLine("      = {0:f2}  N/sq.mm", Mg_by_Zt);
                sw.WriteLine();

                double Mg_by_Zb = Mg * 10E5 / (Zb);
                Mg_by_Zb = double.Parse(Mg_by_Zb.ToString("0.00"));
                sw.WriteLine("Mg/Zb = {0} * 10E5 / ({1:E2})", Mg, Zb);
                sw.WriteLine("      = {0:f2} N/sq.mm ", Mg_by_Zb);
                sw.WriteLine();

                double Mq_by_Zt = Mq * 10E5 / (Zt);
                Mq_by_Zt = double.Parse(Mq_by_Zt.ToString("0.00"));
                sw.WriteLine("Mq/Zt = {0} * 10E5 / ({1:E2})", Mg, Zt);
                sw.WriteLine("      = {0:f2} N/sq.mm ", Mq_by_Zt);
                sw.WriteLine();

                double Mq_by_Zb = Mq * 10E5 / (Zb);
                Mq_by_Zb = double.Parse(Mq_by_Zb.ToString("0.00"));
                sw.WriteLine("Mq/Zb = {0} * 10E5 / ({1:E2})", Mg, Zb);
                sw.WriteLine("      = {0:f2} N/sq.mm ", Mq_by_Zb);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("At transfer stage :");
                sw.WriteLine();

                //double sigma_t = (p / A) - (p * e / Zt) + (Mg / Zt);
                double sigma_t = p_by_A - val1 + Mg_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = (p / A) - (p * e / Zt) + (Mg / Zt)");
                sw.WriteLine("    = {0} - {1} + {2}", p_by_A, val1, Mg_by_Zt);
                sw.WriteLine("    = {0} N/sq.mm", sigma_t);
                sw.WriteLine();

                double sigma_b = p_by_A + val2 - Mg_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.00"));
                sw.WriteLine("σ_b = (p / A) + (p * e / Zb) - (Mg / Zb)");
                sw.WriteLine("    = {0} + {1} - {2}", p_by_A, val2, Mg_by_Zb);
                sw.WriteLine("    = {0} N/sq.mm", sigma_b);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("At working load Stage :");
                sw.WriteLine();

                sigma_t = eta * p_by_A - eta * val1 + Mg_by_Zt + Mq_by_Zt;
                sigma_t = double.Parse(sigma_t.ToString("0.00"));
                sw.WriteLine("σ_t = η*(p / A) - η*(p * e / Zt) + (Mg / Zt) + (Mq / Zt)");
                sw.WriteLine("    = {0}*{1} - {0}*{2} + {3} + {4}", eta, p_by_A, val1, Mg_by_Zt, Mq_by_Zt);
                sw.WriteLine("    = {0} N/sq.mm  (+ve, so, Compression)", sigma_t);
                sw.WriteLine();


                sigma_b = eta * p_by_A + eta * val2 - Mg_by_Zb - Mq_by_Zb;
                sigma_b = double.Parse(sigma_b.ToString("0.000"));
                sw.WriteLine("σ_t = η*(p / A) + η*(p * e / Zb) - (Mg / Zb) - (Mq / Zb)");
                sw.WriteLine("    = {0}*{1} + {0}*{2} - {3} - {4}", eta, p_by_A, val2, Mg_by_Zb, Mq_by_Zb);
                sw.WriteLine("    = {0} N/sq.mm  (-ve, so, Tension)", sigma_b);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("All the stresses at top and bottom fibres at Transfer and Service");
                sw.WriteLine("Loads are well within the Safe Permissible Limits.");
                sw.WriteLine();
                #endregion

                #region STEP 6 : CHECK FOR ULTIMATE FLEXURAL STRENGTH
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : CHECK FOR ULTIMATE FLEXURAL STRENGTH ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("For centre of span section");
                sw.WriteLine();

                double Ap = (Math.PI * dos * dos / 4.0) * Facts * NS * Nc;
                Ap = double.Parse(Ap.ToString("0"));
                sw.WriteLine("Ap = (π * dos * dos / 4.0) * Facts * NS * Nc");
                sw.WriteLine("   = (π * {0} * {0} / 4.0) * {1} * {2} * {3}", dos, Facts, NS, Nc);
                sw.WriteLine("   = {0} sq.mm", Ap);
                sw.WriteLine();
                sw.WriteLine("b = {0} mm, cover = {1} mm", b, cover);
                double dc = d - cover;
                sw.WriteLine();
                sw.WriteLine("Effective Depth = dc = d - cover = {0} - {1} = {2} mm", d, cover, dc);
                sw.WriteLine();
                sw.WriteLine("bw = {0}, fck = {1} N/sq.mm, fp = {2} N/sq.mm", bw, fck, fp);
                sw.WriteLine();

                double Df = d1;
                sw.WriteLine("Df = d1 = {0} mm", d1);
                sw.WriteLine();
                sw.WriteLine();


                double Mu = 1.5 * Mg + 2.5 * Mq;
                Mu = double.Parse(Mu.ToString("0.00"));
                sw.WriteLine("Mu = 1.5 * Mg + 2.5 * Mq");
                sw.WriteLine("   = 1.5 * {0} + 2.5 * {1}", Mg, Mq);
                sw.WriteLine("   = {0} kN-m", Mu);
                sw.WriteLine();
                sw.WriteLine("Ultimate Flexural Strength is computed as follows :");
                sw.WriteLine();
                sw.WriteLine("(i)   Failure by Yielding of steel :");
                
                double Mu1 = 0.9 * dc * Ap * fp;
                Mu1 = double.Parse(Mu1.ToString("0"));
                sw.WriteLine("      Mu1 = 0.9 * dc * Ap * fp");
                sw.WriteLine("          = 0.9 * {0} * {1} * {2}", dc, Ap, fp);

                Mu1 = (int)(Mu1 / 10E5);
                sw.WriteLine("          = {0} * 10E5 N-mm", Mu1);
                sw.WriteLine("          = {0} kN-m", Mu1);
                sw.WriteLine();

                if (Mu < Mu1)
                {
                    sw.WriteLine("Mu < Mu1, Hence, OK");
                }
                else
                {
                    sw.WriteLine("Mu > Mu1, Hence, NOT OK");
                }

                sw.WriteLine();
                sw.WriteLine("(ii)   Failure by crushing of Concrete :");

                double Mu2 = (0.176 * bw * dc * dc * fck) + ((2.0 / 3.0) * Facts * (b - bw) * (dc - (Df / 2.0)) * Df * fck);

                Mu2 = (Mu2 / 10E6);
                Mu2 = double.Parse(Mu2.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Mu2 = (0.176 * bw * dc * dc * fck)");
                sw.WriteLine("       + ((2.0 / 3.0) * Facts * (b - bw) * (dc - (Df / 2.0)) * Df * fck)");
                sw.WriteLine();
                sw.WriteLine("    = (0.176 * {0} * {1} * {1} * {2})", bw, dc, fck);
                sw.WriteLine("       + ((2.0 / 3.0) * {0} * ({1} - {2}) * ({3} - ({4} / 2.0)) * {4} * {5})", Facts, b, bw, dc, Df, fck);
                //Mu2 = (Mu2 / 10E5);
                sw.WriteLine("    = {0} * 10E5 N-mm", Mu2);
                sw.WriteLine("    = {0} kN-m", Mu2);
                sw.WriteLine();
                if (Mu < Mu2)
                {
                    sw.WriteLine("Mu < Mu2, Hence, OK");
                }
                else
                {
                    sw.WriteLine("Mu > Mu2, Hence, OK");
                }

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("(iii)  Flexural Strength at centre span section:");
                sw.WriteLine();
                sw.WriteLine("Ap = Apw + Apt");
                sw.WriteLine();

                double Apt = 0.45 * fck * (b - bw) * (Df / fp);
                Apt = double.Parse(Apt.ToString("0"));
                sw.WriteLine("where, Apt = 0.45 * fck * (b-bw) * (Df/fp)");
                sw.WriteLine("           = 0.45 * {0} * ({1}-{2}) * ({3}/{4})", fck, b, bw, Df, fp);
                sw.WriteLine("           = {0} sq.mm", Apt);
                sw.WriteLine();

                double Apw = Ap - Apt;
                sw.WriteLine("So, Apw = Ap - Apt = {0} - {1} = {2} sq.mm", Ap, Apt, Apw);
                sw.WriteLine();

                double ratio = (Apw * fp / (bw * dc * fck));
                ratio = double.Parse(ratio.ToString("0.000"));
                sw.WriteLine("Ratio = (Apw * fp / (bw * dc * fck))");
                sw.WriteLine("      = ({0} * {1} / ({2} * {3} * {4}))", Apw, fp, bw, dc, fck);
                sw.WriteLine("      = {0} ", ratio);
                sw.WriteLine();
                sw.WriteLine("From Table 2, for post Tensioned Beams with effective bond,");
                sw.WriteLine();

                double post_tension = Get_Table_2_Value(ratio, 2);

                double Xu_by_dc = Get_Table_2_Value(2, 4);

                double fpu = post_tension * 0.87 * fp;

                fpu = double.Parse(fpu.ToString("0.000"));

                double Xu = Xu_by_dc * dc;
                Xu = double.Parse(Xu.ToString("0.000"));
                sw.WriteLine("fpu / (0.87*fp) = {0:f2}    and      Xu/dc = {1:f2}", post_tension, Xu_by_dc);
                sw.WriteLine();
                sw.WriteLine("fpu = {0:f2} * 0.87 * fp    and      Xu = {1:f2} * dc", post_tension, Xu_by_dc);
                sw.WriteLine();
                sw.WriteLine("fpu = {0}  and  Xu = {1} mm", fpu, Xu);
                sw.WriteLine();

                double Mu3 = fpu * Apw * (dc - 0.42 * Xu) + 0.45 * fck * (b - bw) * Df * (dc - 0.5 * Df);

                sw.WriteLine("Mu3 = fpu * Apw * (dc - 0.42 * Xu) ");
                sw.WriteLine("      + 0.45 * fck * (b - bw) * Df * (dc - 0.5 * Df)");
                sw.WriteLine();
                sw.WriteLine("    = {0} * {1} * ({2} - 0.42 * {3}) ", fpu, Apw, dc, Xu);
                sw.WriteLine("      + 0.45 * {0} * ({1} - {2}) * {3} * ({4} - 0.5 * {3})", fck, b, bw, Df, dc);
                sw.WriteLine();

                Mu3 = (int)(Mu3 / 10E5);
                sw.WriteLine("    = {0} * 10E5 N-mm ", Mu3);
                sw.WriteLine("    = {0} kN-m ", Mu3);

                if (Mu < Mu3)
                {
                    sw.WriteLine("Mu < Mu3, Hence, OK");
                }
                else
                {
                    sw.WriteLine("Mu > Mu3, Hence, NOT OK");
                }
                sw.WriteLine();
                #endregion

                #region STEP 7 : Check for Ultimate Shear Strength
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : CHECK FOR ULTIMATE SHEAR STRENGTH ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double Vg = DL_SF_IG;
                double Vq = LL_SF_IG;

                double Vu = 1.5 * Vg + 2.5 * Vq;
                sw.WriteLine("Ultimate Shear Force = Vu = 1.5 * Vg + 2.5 * Vq");
                sw.WriteLine("                     = 1.5 * {0} + 2.5 * {1}", Vg, Vq);
                sw.WriteLine("                     = {0:f0} kN", Vu);
                sw.WriteLine();
                
               
                sw.WriteLine("Ultimate Shear resistance of Support Section uncracked in Flexure");
                sw.WriteLine("is obtained by, Vcw = 0.67*bw*d*√(ft*ft+0.8*Fcp*ft) + η.p.Sinθ");

                double ft = 0.24 * Math.Sqrt(fck);
                ft = double.Parse(ft.ToString("0.00"));
                double fcp = (eta * p * 1000) / (A * 10E5);
                fcp = double.Parse(fcp.ToString("0.00"));

                // 600 + x + 300 + x + x = 2*(1800-300-250-2*300-x)
                // 3x + 600+300 = 2*(650 -x)
                // 3x + 2x = 1300 - 600 - 300
                // 5x = 400
                // x = 80

                // x' = 250 + 300 + 300 + 80  (x = 80)
                // x' = 850 + 80 = 930
                // x' = 930 - 750 = 180
                //

                double _e = e;

                double x_dash = 180;

                e = _e - x_dash;
                double theta = 4 * e / (L * 1000);
                theta = double.Parse(theta.ToString("0.000"));
                double Vcw = 0.67 * bw * d * Math.Sqrt((ft * ft + 0.8 * fcp * ft)) + eta * p * 1000 * theta;
                Vcw = double.Parse(Vcw.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("ft = 0.24 * √(fck) = 0.24 * √({0}) = {1} N/sq.mm", fck, ft);
                sw.WriteLine();
                sw.WriteLine("fcp = (η * p) / A ");
                sw.WriteLine("    = ({0} * {1:f0} * 1000) / ({2} * 10E5)", eta, p, A);
                sw.WriteLine("    = {0} sq.mm", fcp);
                sw.WriteLine();


                sw.WriteLine("Eccentricity of Cables at Centre of span = {0} mm", _e);
                sw.WriteLine();
                sw.WriteLine("Eccentricity of Cables at Support = {0} mm", x_dash);
                sw.WriteLine();
                sw.WriteLine("Net eccentricity = e = {0} - {1} = {2} mm", _e, x_dash, e);
                sw.WriteLine();
                sw.WriteLine("Slope of Cable = θ = 4 * e / (L * 1000)");
                sw.WriteLine("               = 4 * {0} / ({1} * 1000)", e, L);
                sw.WriteLine("               = {0} ", theta);
                sw.WriteLine();
                //double Vcw = 0.67 * bw * d * Math.Sqrt((ft * ft + 0.8 * Fcp * ft)) + n * p * 1000 * theta;
                sw.WriteLine("Vcw = 0.67 * {0} * {1} * √(({2} * {2} + 0.8 * {3} * {{2}}))", bw, d, ft, fcp);
                sw.WriteLine("      + {0} * {1:f0} * 1000 * {2}", eta, p, theta);
                sw.WriteLine();
                sw.WriteLine("    = {0} N", Vcw);
                sw.WriteLine();
                Vcw = (int)(Vcw / 1000.0);
                sw.WriteLine("    = {0} kN", Vcw);
                sw.WriteLine();
                sw.WriteLine("Required Shear resistance = {0} kN", Vu);
                sw.WriteLine();
                sw.WriteLine("Available Shear capacity of Section = {0} kN", Vcw);
                sw.WriteLine();

                double V = Math.Abs(Vu - Vcw);
                sw.WriteLine("Balance Shear = V = {0} kN", V);
                sw.WriteLine();
                sw.WriteLine("Using 10 mm diameter 2-legged Stirrups HYSD bars ");
                sw.WriteLine("the spacing Sv is calculated as :");
                sw.WriteLine();


                // Asv = 79 = ?

                double Asv = Math.PI * 10.0 * 10.0 / 4.0;
                Asv = double.Parse(Asv.ToString("0"));

                double Sv = (0.87 * fy * 2 * Asv * (d - 50)) / (V * 1000);
                Sv = double.Parse(Sv.ToString("0"));

                sw.WriteLine("Sv = (0.87 * fy * 2 * Asv * (d - 50)) / (V * 1000)");
                sw.WriteLine("   = (0.87 * {0} * 2 * {1} * ({2} - 50)) / ({3} * 1000)", fy, Asv, d, V);
                sw.WriteLine();
                sw.WriteLine("   = {0} mm", Sv);
                sw.WriteLine();

                double spacing = 0;

                if (Sv > 150)
                    spacing = 150;
                sw.WriteLine("Provide 10 mm diameter  stirrups at {0} mm Centre to Centre", spacing);
                sw.WriteLine("spacing near support and gradually increased to 300 mm towards");
                sw.WriteLine("the centre of span.                 Marked as (J) in the Drawing");
                //(J) = Provide 10 mm diameter  stirrups at 150 mm c/c.
                _J = string.Format("Provide 10 mm diameter  stirrups at {0} mm c/c", spacing);




                sw.WriteLine();
                #endregion

                #region STEP 8 : SUPPLEMENTARY REINFORCEMENTS
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : SUPPLEMENTARY REINFORCEMENTS ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Longitudinal reinforcement of not less than 0.15 per cent");
                sw.WriteLine("of gross cross sectional area is to be provided to Safeguard");
                sw.WriteLine("against shrinkage cracking,");
                sw.WriteLine();

                double Ast = (0.15 * A * 10E5) / 100.0;
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Ast = (0.15 * {0} * 10E5) / 100.0", A);
                sw.WriteLine("    = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine("Provide 20 mm diameter bars with distribution in the compression");
                sw.WriteLine("Flange as Shown in the drawing.          Marked as (K) in the Drawing");
                //(K) = Provide 20 mm diameter bars
                _K = string.Format("Provide 20 mm diameter bars");


                sw.WriteLine();
                #endregion

                #region STEP 9 : DESIGN OF THE END BLOCK
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 9 : DESIGN OF THE END BLOCK ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Provide Solid end Sections over a length of 1.5 m at either end");
                sw.WriteLine("Typical equivalent prisms on which the anchorage forces are apply");
                sw.WriteLine("are described in Drawing. The Bursting Tension force is calculated");
                sw.WriteLine("using the data given in Table 3 at the end of this Report.");
                sw.WriteLine();
                sw.WriteLine("We have the following values in the Horizontal Plane");
                sw.WriteLine();

                //double Pk = 1459;
                sw.WriteLine("Pk = {0} kN", Pk);
                sw.WriteLine();

                double Ypo = 225.0 / 2.0;
                sw.WriteLine("2Ypo = 225 mm");
                sw.WriteLine();

                double Yo = 900.0 / 2.0;
                sw.WriteLine("2Yo = 900 mm");
                sw.WriteLine();

                val1 = Ypo / Yo;
                ratio = val1;
                sw.WriteLine("Ypo / Yo = {0} / {1} = {2}", Ypo, Yo, val1);
                sw.WriteLine();


                val1 = Get_Table_3_Value(ratio);
                double Fbst = val1 * Pk;
                sw.WriteLine("Bursting Tension Force = Fbst = {0} * Pk", val1);
                sw.WriteLine("                       = {0} * {1}", val1, Pk);
                sw.WriteLine("                       = {0} kN", Fbst);
                sw.WriteLine();
                sw.WriteLine("Area of steel required to resist the tension is obtained by:");
                sw.WriteLine();

                Ast = Fbst * 1000 / (0.87 * fy);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine("Ast = Fbst * 1000 / (0.87 * fy)");
                sw.WriteLine("    = {0} * 1000 / (0.87 * {1})", Fbst, fy);
                sw.WriteLine("    = {0} sq.mm", Ast);
                sw.WriteLine();
                sw.WriteLine();


                double pro_ast = 0.0;
                spacing = 60;

                do
                {
                    spacing += 20;
                    pro_ast = (Math.PI * 10 * 10 / 4.0) * (1000.0 / spacing);
                }
                while (pro_ast > Ast);


                sw.WriteLine("Provide 10 mm diameter bars at {0} mm c/c spacing in Horizontal         Marked as (L) in the Drawing", spacing);
                sw.WriteLine("and Vertical direction in form of a mesh as shown in the drawing.");
                //(L) = Provide 10 mm diameter bars at 100 mm c/c.
                _L = string.Format("Provide 10 mm diameter bars at {0} mm c/c.", spacing);


                sw.WriteLine();
                sw.WriteLine();
                #endregion

                Write_Table_2(sw);
                sw.WriteLine();
                Write_Table_3(sw);
                sw.WriteLine();

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
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
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_G={0}", _G);
                sw.WriteLine("_H={0}", _H);
                sw.WriteLine("_I={0}", _I1);
                sw.WriteLine("_J={0}", _J);
                sw.WriteLine("_K={0}", _K);
                sw.WriteLine("_L={0}", _L);
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
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {

                #region USER DATA INPUT
                try
                {
                    sw.WriteLine("L = {0}", txt_L.Text);
                    sw.WriteLine("a = {0}", txt_a.Text);
                    sw.WriteLine("d = {0}", txt_d.Text);
                    sw.WriteLine("b = {0}", txt_b.Text);
                    sw.WriteLine("bw = {0}", txt_bw.Text);
                    sw.WriteLine("d1 = {0}", txt_d1.Text);
                    sw.WriteLine("d2 = {0}", txt_d2.Text);
                    sw.WriteLine("fck = {0}", txt_fck.Text);
                    sw.WriteLine("doc = {0}", txt_doc.Text);
                    sw.WriteLine("fci = {0}", txt_fci.Text);
                    sw.WriteLine("NS = {0}", txt_NS.Text);
                    sw.WriteLine("fy = {0}", txt_fy.Text);
                    sw.WriteLine("dos = {0}", txt_dos.Text);
                    sw.WriteLine("sigma_cb = {0}", txt_sigma_cb.Text);
                    sw.WriteLine("sigma_st = {0}", txt_sigma_st.Text);
                    sw.WriteLine("SF = {0}", txt_SF.Text);
                    sw.WriteLine("m = {0}", txt_m.Text);
                    sw.WriteLine("Q = {0}", txt_Q.Text);
                    sw.WriteLine("j = {0}", txt_j.Text);
                    sw.WriteLine("eta = {0}", txt_eta.Text);
                    sw.WriteLine("FS = {0}", txt_FS.Text);
                    sw.WriteLine("fp = {0}", txt_fp.Text);

                    sw.WriteLine("DL_BM_OG = {0}", txt_DL_BM_OG.Text);
                    sw.WriteLine("DL_BM_IG = {0}", txt_DL_BM_IG.Text);
                    sw.WriteLine("LL_BM_OG = {0}", txt_LL_BM_OG.Text);
                    sw.WriteLine("LL_BM_IG = {0}", txt_LL_BM_IG.Text);
                    sw.WriteLine("DL_SF_OG = {0}", txt_DL_SF_OG.Text);
                    sw.WriteLine("DL_SF_IG = {0}", txt_DL_SF_IG.Text);
                    sw.WriteLine("LL_SF_OG = {0}", txt_LL_SF_OG.Text);
                    sw.WriteLine("LL_SF_IG = {0}", txt_LL_SF_IG.Text);

                    sw.WriteLine("space_long = {0}", txt_Space_Long.Text);
                    sw.WriteLine("space_cross = {0}", txt_Space_Cross.Text);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
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
                            txt_L.Text = mList.StringList[1].Trim().TrimEnd().TrimStart().Trim().TrimEnd().TrimStart();
                            break;
                        case "a":
                            txt_a.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "d":
                            txt_d.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "b":
                            txt_b.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "bw":
                            txt_bw.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "d1":
                            txt_d1.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "d2":
                            txt_d2.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fck":
                            txt_fck.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "doc":
                            txt_doc.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fci":
                            txt_fci.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "NS":
                            txt_NS.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fy":
                            txt_fy.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "dos":
                            txt_dos.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_cb":
                            txt_sigma_cb.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "sigma_st":
                            txt_sigma_st.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "SF":
                            txt_SF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "m":
                            txt_m.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Q":
                            txt_Q.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "j":
                            txt_j.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "eta":
                            txt_eta.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "FS":
                            txt_FS.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "DL_BM_OG":
                            txt_DL_BM_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "DL_BM_IG":
                            txt_DL_BM_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "LL_BM_OG":
                            txt_LL_BM_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "LL_BM_IG":
                            txt_LL_BM_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "DL_SF_OG":
                            txt_DL_SF_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "DL_SF_IG":
                            txt_DL_SF_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "LL_SF_OG":
                            txt_LL_SF_OG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "LL_SF_IG":
                            txt_LL_SF_IG.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "fp":
                            txt_fp.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;


                        case "space_long":
                            txt_Space_Long.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "space_cross":
                            txt_Space_Cross.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex) { }
            lst_content.Clear();
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
            kPath = Path.Combine(kPath, "Pre stressed Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "Design of Pre stressed Post tensioned Main Girder");

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
                this.Text = "DESIGN OF PRESTRESSED POST TENSIONED RCC GIRDER BRIDGE : " + value;
                user_path = value;

                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Rcc_PS_Girder.TXT");
                user_input_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER.FIL");
                user_drawing_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER_DRAWING.FIL");

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
        public void InitializeData()
        {
            #region USER DATA INPUT
            try
            {
                L = MyList.StringToDouble(txt_L.Text, 0.0);
                a = MyList.StringToDouble(txt_a.Text, 0.0);
                d = MyList.StringToDouble(txt_d.Text, 0.0);
                b = MyList.StringToDouble(txt_b.Text, 0.0);
                bw = MyList.StringToDouble(txt_bw.Text, 0.0);
                d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
                d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
                fck = MyList.StringToDouble(txt_fck.Text, 0.0);
                doc = MyList.StringToDouble(txt_doc.Text, 0.0);
                fci = MyList.StringToDouble(txt_fci.Text, 0.0);
                NS = MyList.StringToDouble(txt_NS.Text, 0.0);
                fy = MyList.StringToDouble(txt_fy.Text, 0.0);
                dos = MyList.StringToDouble(txt_dos.Text, 0.0);
                sigma_cb = MyList.StringToDouble(txt_sigma_cb.Text, 0.0);
                sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
                SF = MyList.StringToDouble(txt_SF.Text, 0.0);
                m = MyList.StringToDouble(txt_m.Text, 0.0);
                Q = MyList.StringToDouble(txt_Q.Text, 0.0);
                j = MyList.StringToDouble(txt_j.Text, 0.0);
                eta = MyList.StringToDouble(txt_eta.Text, 0.0);
                FS = MyList.StringToDouble(txt_FS.Text, 0.0);
                DL_BM_OG = MyList.StringToDouble(txt_DL_BM_OG.Text, 0.0);
                DL_BM_IG = MyList.StringToDouble(txt_DL_BM_IG.Text, 0.0);
                LL_BM_OG = MyList.StringToDouble(txt_LL_BM_OG.Text, 0.0);

                LL_BM_IG = MyList.StringToDouble(txt_LL_BM_IG.Text, 0.0);

                DL_SF_OG = MyList.StringToDouble(txt_DL_SF_OG.Text, 0.0);
                DL_SF_IG = MyList.StringToDouble(txt_DL_SF_IG.Text, 0.0);

                LL_SF_OG = MyList.StringToDouble(txt_LL_SF_OG.Text, 0.0);
                LL_SF_IG = MyList.StringToDouble(txt_LL_SF_IG.Text, 0.0);
                fp = MyList.StringToDouble(txt_fp.Text, 0.0);


                space_long = MyList.StringToDouble(txt_Space_Long.Text, 0.0);
                space_cross = MyList.StringToDouble(txt_Space_Cross.Text, 0.0);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }
        #endregion


        public double Get_Table_2_Value(double ratio, int indx)
        {

            return iApp.Tables.Pre_Post_Tensioning_with_EffectiveBond(ratio, indx, ref ref_string);

            //ratio = Double.Parse(ratio.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");


            //ratio = 0.218;
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
            //    mList = new MyList(MyList.RemoveAllSpaces(lst_content[i].Trim()), ' ');
            //    find = (double.TryParse(mList.StringList[0], out a2) && mList.Count == 5);
            //    if (find)
            //    {
            //        //mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);
            //    if (ratio < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (ratio > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == ratio)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > ratio)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (ratio - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_3_Value(double ratio)
        {
            return iApp.Tables.Bursting_Tensile_Force(ratio, ref ref_string);


            //ratio = Double.Parse(ratio.ToString("0.000"));
            //int indx  = 1;
            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_3.txt");


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
            //    mList = new MyList(MyList.RemoveAllSpaces(lst_content[i].Trim()), ' ');
            //    find = (double.TryParse(mList.StringList[0], out a2) && mList.Count == 2);
            //    if (find)
            //    {
            //        //mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);
            //    if (ratio < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (ratio > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == ratio)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > ratio)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (ratio - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }

        public void Write_Table_2(StreamWriter sw)
        {
            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Pre_Post_Tensioning_with_EffectiveBond();
            string kStr = "";
            sw.WriteLine();
            sw.WriteLine("TABLE 2 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
        }
        public void Write_Table_3(StreamWriter sw)
        {
            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "PreStressedBridge_Table_3.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Bursting_Tensile_Force();
            string kStr = "";
            sw.WriteLine();
            sw.WriteLine("TABLE 3 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
        }

        private void txt_fck_TextChanged(object sender, EventArgs e)
        {
            InitializeData();
            double fcc, j, Q, fcb, n;


            fcb = fck / 3;
            fcc = fck / 4;

            n = m * fcb / (m * fcb + sigma_st);

            j = 1 - (n / 3.0);
            Q = n * j * fcb / 2;

            txt_sigma_cb.Text = fcb.ToString("0.00");
            txt_j.Text = j.ToString("0.000");
            txt_Q.Text = Q.ToString("0.000");

        }

    }
}
