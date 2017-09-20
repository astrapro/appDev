using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public class Continuous_Box_Girder_Design
    {

        IApplication iApp;
        public string rep_file_name = "";
        public string file_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public string user_path = "";
        public string drawing_path = "";
        public bool is_process = false;
        public Cross_Section_Box_Girder Mid_Cross_Section { get; set; }
        public Cross_Section_Box_Girder End_Cross_Section { get; set; }

        public List<Mid_Span_Cable_Profile_Data> Mid_Cables { get; set; }
        public List<End_Span_Cable_Profile_Data> End_Cables { get; set; }

        //Chiranjt [2013 08 04]
        public List<Cap_Cable_Profile_Data> Cap_Cables_Profile { get; set; }


        public List<PSF_after_FS_Loss_Table> Table_Mid_PSF_after_FS_Loss { get; set; }
        public List<PSF_after_FS_Loss_Table> Table_End_PSF_after_FS_Loss { get; set; }


        public List<PSF_after_FS_Loss_Table> Table_Cap_Cable_PSF_after_FS_Loss { get; set; }


        public StressSummary Mid_Stress_Summary { get; set; }
        public StressSummary End_Stress_Summary { get; set; }


        //Chiranjit [2013 09 11]
        public Secondary Table_Secondary { get; set; }

        public Continuous_PSC_BoxGirderAnalysis Analysis { get; set; }

        #region Properties
        public int Adopt_Cables { get; set; }
        public double Duct_Dia { get; set; }
        public double UTS { get; set; }
        public double Strand_Area { get; set; }
        public double Tendon_Area { get { return (Adopt_Cables * Strand_Area); } }
        public double Es { get; set; }
        public double alj { get; set; }
        public double dist_face { get; set; }


        public int Mid_Box_Nc { get; set; }
        public int Mid_Anc_Box_Nc { get; set; }
        public double Mid_Box_Pj { get; set; }
        public double Mid_cc_supp { get; set; }
        public double Mid_cc_temp_supp { get; set; }
        public double Mid_over_supp { get; set; }




        public int End_Box_Nc { get; set; }
        public double End_Box_Pj { get; set; }
        public double End_cc_supp { get; set; }
        public double End_cc_temp_supp { get; set; }
        public double End_over_supp { get; set; }





        public double Mu { get; set; }
        public double k { get; set; }
        public double duct_dia { get; set; }
        public double Fcu { get; set; }

        //Total Slip Loss
        public double S { get; set; }


        //Chiranjit [2013 08 22]

        public List<double> End_Dist_Supp { get; set; }
        public List<double> End_Dist_Jack { get; set; }




        #endregion Properties



        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF RCC DECK SLAB : " + value;
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of Continuous PSC Box Girder");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "DESIGN_OF_CONTINUOUS_PSC_BOX_GIRDER.TXT");
                user_input_file = Path.Combine(system_path, "PSC_BOX_GIRDER.FIL");
            }
        }

        public Continuous_Box_Girder_Design(IApplication app)
        {
            iApp = app;

            Adopt_Cables = 0;
            Mid_Anc_Box_Nc = 0;
            Duct_Dia = 0.0;
            UTS = 0.0;
            Strand_Area = 0.0;
            Es = 0.0;
            alj = 0.0;
            dist_face = 0.0;
            Mid_Box_Nc = 0;
            Mid_cc_supp = 0.0;
            Mid_cc_temp_supp = 0.0;
            Mid_over_supp = 0.0;




            End_Box_Nc = 0;
            End_cc_supp = 0.0;
            End_cc_temp_supp = 0.0;
            End_over_supp = 0.0;




            Mu = 0.0;
            k = 0.0;
            duct_dia = 0.0;
            Fcu = 0.0;



            Mid_Cross_Section = new Cross_Section_Box_Girder();
            End_Cross_Section = new Cross_Section_Box_Girder();

            Mid_Cables = new List<Mid_Span_Cable_Profile_Data>();
            End_Cables = new List<End_Span_Cable_Profile_Data>();

            Table_Mid_PSF_after_FS_Loss = new List<PSF_after_FS_Loss_Table>();
            Table_End_PSF_after_FS_Loss = new List<PSF_after_FS_Loss_Table>();


            Cap_Cables_Profile = new List<Cap_Cable_Profile_Data>();

        }
    }


    public class StressSummary : List<StressSummaryData>
    {
        public Hashtable Data { get; set; }
        public StressSummary()
            : base()
        {
            Data = new Hashtable();
        }
        public List<string> Results { get; set; }

        public void Read_Mid_Span_Data(Continuous_Box_Girder_Design cnt)
        {

            Results = new List<string>();

            #region Distances
            List<double> dists = new List<double>();


            dists.Add((cnt.Mid_cc_temp_supp + cnt.Mid_over_supp * 2) / 2.0 - cnt.dist_face);
            dists.Add(cnt.Mid_cc_supp / 20);
            dists.Add(cnt.Mid_cc_supp * 2 / 20);
            dists.Add(cnt.Mid_cc_supp * 3 / 20);
            dists.Add(cnt.Mid_cc_supp * 4 / 20);
            dists.Add(cnt.Mid_cc_supp * 5 / 20);
            dists.Add(cnt.Mid_cc_supp * 6 / 20);
            dists.Add(cnt.Mid_cc_supp * 7 / 20);
            dists.Add(cnt.Mid_cc_supp * 8 / 20);
            dists.Add(cnt.Mid_cc_supp * 9 / 20);
            dists.Add(0);

            dists.Sort();
            dists.Reverse();


            StressSummaryData ssd_dist = new StressSummaryData();

            ssd_dist.AddRange(dists.ToArray());



            #endregion Distances


            RichTextBox rtb = new RichTextBox();



            
            Results.Add("");
            Results.Add("----------------------------------------------------");
            Results.Add("Stress Summary");
            Results.Add("----------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
            Results.Add(ssd_dist.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");

            StressSummaryData sec_props_A = new StressSummaryData();

            sec_props_A.Sl_No = "1";
            sec_props_A.Section = "A";
            sec_props_A.Unit = "sq.m";

            StressSummaryData sec_props_Ix = new StressSummaryData();

            sec_props_Ix.Sl_No = "";
            sec_props_A.Section = "Ix";
            sec_props_A.Unit = "m^4";

            StressSummaryData sec_props_Yt = new StressSummaryData();

            sec_props_Yt.Sl_No = "2";
            sec_props_Yt.Section = "Yt";
            sec_props_Yt.Unit = "m";

            StressSummaryData sec_props_Yb = new StressSummaryData();

            sec_props_Yb.Sl_No = "3";
            sec_props_Yb.Section = "Yb";
            sec_props_Yb.Unit = "m";


            StressSummaryData sec_props_Zt = new StressSummaryData();

            sec_props_Zt.Sl_No = "4";
            sec_props_Zt.Section = "Zt";
            sec_props_Zt.Unit = "m";

            StressSummaryData sec_props_Zb = new StressSummaryData();

            sec_props_Zb.Sl_No = "5";
            sec_props_Zb.Section = "Yb";
            sec_props_Zb.Unit = "m";

            for (int i = 0; i < cnt.Analysis.Mid_Span_Sections.Count; i++)
            {
                sec_props_A.Add(cnt.Analysis.Mid_Span_Sections[i].Total_Area);
                sec_props_Ix.Add(cnt.Analysis.Mid_Span_Sections[i].I);
                sec_props_Yt.Add(cnt.Analysis.Mid_Span_Sections[i].Yt);
                sec_props_Yb.Add(cnt.Analysis.Mid_Span_Sections[i].Yb);

                sec_props_Zt.Add(sec_props_Ix[i] / sec_props_Yt[i]);
                sec_props_Zb.Add(sec_props_Ix[i] / sec_props_Yb[i]);
            }
            this.Add(sec_props_A);
            this.Add(sec_props_Ix);
            this.Add(sec_props_Yt);
            this.Add(sec_props_Yb);
            this.Add(sec_props_Zt);
            this.Add(sec_props_Zb);

            //Results.Add("");
            Results.Add("----------------------");
            Results.Add("Section Properties");
            Results.Add("----------------------");
            Results.Add(sec_props_A.ToString());
            Results.Add(sec_props_Ix.ToString());
            Results.Add(sec_props_Yt.ToString());
            Results.Add(sec_props_Yb.ToString());
            Results.Add(sec_props_Zt.ToString());
            Results.Add(sec_props_Zb.ToString());




            //Pre stress after friction and slip
            //Results.Add("");
            Results.Add("-----------------------------------");
            Results.Add("Pre stress after friction and slip");
            Results.Add("-----------------------------------");
            //Results.Add("");

            StressSummaryData psfs_Fh = new StressSummaryData();

            psfs_Fh.Sl_No = "6";
            psfs_Fh.Section = "Fh";
            psfs_Fh.Unit = "T";


            StressSummaryData psfs_e = new StressSummaryData();

            psfs_e.Sl_No = "7";
            psfs_e.Section = "e";
            psfs_e.Unit = "m";

            StressSummaryData psfs_y = new StressSummaryData();

            psfs_y.Sl_No = "8";
            psfs_y.Section = "y";
            psfs_y.Unit = "m";


            StressSummaryData psfs_M = new StressSummaryData();

            psfs_M.Sl_No = "9=6*7";
            psfs_M.Section = "M=Fh*e";
            psfs_M.Unit = "T-m";

            StressSummaryData psfs_Fh_A = new StressSummaryData();

            psfs_Fh_A.Sl_No = "10=6/1";
            psfs_Fh_A.Section = "Fh/A";
            psfs_Fh_A.Unit = "kg/cm^2";

            StressSummaryData psfs_M_Zt = new StressSummaryData();

            psfs_M_Zt.Sl_No = "11=9/4";
            psfs_M_Zt.Section = "M/Zt";
            psfs_M_Zt.Unit = "kg/cm^2";

            StressSummaryData psfs_M_Zb = new StressSummaryData();

            psfs_M_Zb.Sl_No = "12=9/5";
            psfs_M_Zb.Section = "M/Zb";
            psfs_M_Zb.Unit = "kg/cm^2";


            //Stresses due to Prestress after Friction and slip Losses

            StressSummaryData spfsl_ft = new StressSummaryData();

            spfsl_ft.Sl_No = "13=10+11";
            spfsl_ft.Section = "ft";
            spfsl_ft.Unit = "kg/cm^2";


            StressSummaryData spfsl_fb = new StressSummaryData();

            spfsl_fb.Sl_No = "14=10+12";
            spfsl_fb.Section = "fb";
            spfsl_fb.Unit = "kg/cm^2";


            //Dead Load Stresses
            StressSummaryData DL_Md = new StressSummaryData();

            DL_Md.Sl_No = "15";
            DL_Md.Section = "DL Moment";
            DL_Md.Unit = "T-m";

            StressSummaryData DL_ft = new StressSummaryData();

            DL_ft.Sl_No = "16=15/4";
            DL_ft.Section = "ft=Md/Zt";
            DL_ft.Unit = "kg/cm^2";


            StressSummaryData DL_fb = new StressSummaryData();

            DL_fb.Sl_No = "17=15/5";
            DL_fb.Section = "fb=Md/Zb";
            DL_fb.Unit = "kg/cm^2";

            //Stresses due to DL and Prestress
            StressSummaryData SDLP_ft = new StressSummaryData();

            SDLP_ft.Sl_No = "18=13+16";
            SDLP_ft.Section = "ft";
            SDLP_ft.Unit = "kg/cm^2";

            StressSummaryData SDLP_fb = new StressSummaryData();

            SDLP_fb.Sl_No = "19=14+17";
            SDLP_fb.Section = "fb";
            SDLP_fb.Unit = "kg/cm^2";




            for (int i = 0; i < cnt.Analysis.Mid_Span_Sections.Count; i++)
            {
                //Pre stress after friction and slip

                psfs_Fh.Add(cnt.Table_Mid_PSF_after_FS_Loss[i].Fh);
                psfs_e.Add(cnt.Table_Mid_PSF_after_FS_Loss[i].e);
                psfs_y.Add(sec_props_Yb[i] - psfs_e[i]);

                psfs_M.Add(psfs_Fh[i] * psfs_e[i]);

                psfs_Fh_A.Add((psfs_Fh[i] * 1000) / (sec_props_A[i] * 10000));
                psfs_M_Zt.Add(-(psfs_M[i] * 1000 * 100) / (sec_props_Zt[i] * 10000 * 100));
                psfs_M_Zb.Add((psfs_M[i] * 1000 * 100) / (sec_props_Zb[i] * 10000 * 100));

                //Stresses due to Prestress after Friction and slip Losses


                spfsl_ft.Add(psfs_Fh_A[i] + psfs_M_Zt[i]);
                spfsl_fb.Add(psfs_Fh_A[i] + psfs_M_Zb[i]);
                if (i == 0)
                    DL_Md.Add(0.0);
                else
                    DL_Md.Add(cnt.Analysis.FRC_SPAN_2[i - 1].BM_Incr);


                DL_ft.Add((DL_Md[i] * 100000) / (sec_props_Zt[i] * 1000000));
                DL_fb.Add(-(DL_Md[i] * 100000) / (sec_props_Zb[i] * 1000000));


                SDLP_ft.Add(spfsl_ft[i] + DL_ft[i]);
                SDLP_fb.Add(spfsl_fb[i] + DL_fb[i]);

            }
            Results.Add(psfs_Fh.ToString());

            this.Add(psfs_Fh);



            Results.Add(psfs_e.ToString());
            this.Add(psfs_e);
            Results.Add(psfs_y.ToString());
            this.Add(psfs_y);
            Results.Add("");
            Results.Add(psfs_M.ToString());
            this.Add(psfs_M);
            Results.Add("");
            Results.Add(psfs_Fh_A.ToString());
            this.Add(psfs_Fh_A);
            Results.Add(psfs_M_Zt.ToString());
            this.Add(psfs_M_Zt);
            Results.Add(psfs_M_Zb.ToString());
            this.Add(psfs_M_Zb);


            //Results.Add("");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stresses due to Prestress after Friction and slip Losses");
            Results.Add("----------------------------------------------------------");
            //Results.Add("");

            Results.Add(spfsl_ft.ToString());
            this.Add(spfsl_ft);
            Results.Add(spfsl_fb.ToString());
            this.Add(spfsl_fb);

            //Results.Add("");
            Results.Add("----------------------------------------------------------");
            Results.Add("Dead Load Stresses");
            Results.Add("----------------------------------------------------------");
            //Results.Add("");
            Results.Add(DL_Md.ToString());
            this.Add(DL_Md);
            Results.Add(DL_ft.ToString());
            this.Add(DL_ft);
            Results.Add(DL_fb.ToString());
            this.Add(DL_fb);

            //Results.Add("");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stresses due to DL and Prestress");
            Results.Add("----------------------------------------------------------");
            //Results.Add("");
            Results.Add(SDLP_ft.ToString());
            this.Add(SDLP_ft);
            Results.Add(SDLP_fb.ToString());
            this.Add(SDLP_fb);



            double val = dists[1];


            StressSummaryData SCGPF_47 = new StressSummaryData();

            SCGPF_47.Sl_No = "47=18+(19-18)(d-";
            SCGPF_47.Section = "8)/d";
            SCGPF_47.Unit = "kg/cm^2";

            double d = 0.0;



            for (int i = 0; i < cnt.Analysis.Mid_Span_Sections.Count; i++)
            {
                d = (sec_props_Yb[i] + sec_props_Yt[i]);

                val = SDLP_ft[i] + (SDLP_fb[i] - SDLP_ft[i]) * (d - psfs_y[i]) / d;

                //47=18+(19-18)(d-8)/d
                SCGPF_47.Add(val);

            }

            Results.Add("----------------------------------------------------------");
            Results.Add("Loss of Prestress");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stress at C G of Prestress Force");
            

            double tl = (cnt.Mid_cc_temp_supp + cnt.Mid_over_supp * 2) / 2.0 - cnt.dist_face;


            Results.Add(string.Format("d = ({0} + {1}x2) - {2} = {3:f3}", cnt.Mid_cc_temp_supp, cnt.Mid_over_supp, cnt.dist_face, tl));
            Results.Add("----------------------------------------------------------");
            Results.Add(SCGPF_47.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            //Average Stress 
            double avg_strs = 0;
            Results.Add(string.Format(""));
            //Results.Add(string.Format("Average Stress"));
            //Results.Add(string.Format(""));

            for (int i = 1; i < dists.Count; i++)
            {
                avg_strs += ((SCGPF_47[i] + SCGPF_47[i - 1]) / 2.0) * (dists[i - 1] - dists[i]);


                if (i == 1)
                    Results.Add(string.Format("Average Stress = avg_strs = (1/{0}) x [(({1:f3} + {2:f3}) / 2) x ({3} - {4})", tl, SCGPF_47[i], SCGPF_47[i - 1], dists[i - 1], dists[i]));
                else if(i == dists.Count -1)
                    Results.Add(string.Format("                                    + (({0:f3} + {1:f3}) / 2) x ({2} - {3})]", SCGPF_47[i], SCGPF_47[i - 1], dists[i - 1], dists[i]));
               else
                    Results.Add(string.Format("                                    + (({0:f3} + {1:f3}) / 2) x ({2} - {3})", SCGPF_47[i], SCGPF_47[i - 1], dists[i - 1], dists[i]));
            }

            avg_strs = avg_strs * (1 / tl);

            Results.Add(string.Format(""));
            Results.Add(string.Format("                          = {0:f3} Kg/cm^2", avg_strs));
            Results.Add(string.Format(""));

            double Ecj = 5000 * Math.Sqrt(cnt.Fcu) * 10;
            Results.Add(string.Format(""));
            Results.Add(string.Format("Ecj = 5000 * Sqrt(Fcu) * 10"));
            Results.Add(string.Format("    = 5000 * Sqrt({0}) * 10", cnt.Fcu));
            Results.Add(string.Format(""));
            Results.Add(string.Format("    = {0:f3} N/mm^2", Ecj/10));
            Results.Add(string.Format(""));
            Results.Add(string.Format("    = {0:f3} Kg/cm^2", Ecj));
            Results.Add(string.Format(""));



            //Elastic Shortining loss 
            double ESL = 0.5 * (cnt.Es * 10 / Ecj) * avg_strs;
            Results.Add(string.Format("Elastic Shortining loss = ESL = 0.5 * (Es * 10 / Ecj) * avg_strs"));
            Results.Add(string.Format("                              = 0.5 * ({0:f3} * 10 / {1:f3}) * {2:f3}", cnt.Es, Ecj, avg_strs));
            Results.Add(string.Format("                              = {0:f3} N/mm^2", ESL));

            Results.Add(string.Format(""));

            //Average Prestress force after Friction and Slip 
            double avg_PFS = 0.0;
            Results.Add(string.Format("Average Prestress force after Friction and Slip = avg_PFS"));
            Results.Add(string.Format(""));

            for (int i = 1; i < dists.Count; i++)
            {
                avg_PFS += ((psfs_Fh[i] + psfs_Fh[i - 1]) / 2.0) * (dists[i - 1] - dists[i]);


                if (i == 1)
                    Results.Add(string.Format("avg_PFS = (1/{0}) x [(({1:f3} + {2:f3}) / 2) x ({3} - {4})", tl, psfs_Fh[i], psfs_Fh[i - 1], dists[i - 1], dists[i]));
                else if (i == dists.Count - 1)
                    Results.Add(string.Format("                  + (({0:f3} + {1:f3}) / 2) x ({2} - {3})]", psfs_Fh[i], psfs_Fh[i - 1], dists[i - 1], dists[i]));
                else
                    Results.Add(string.Format("                  + (({0:f3} + {1:f3}) / 2) x ({2} - {3})", psfs_Fh[i], psfs_Fh[i - 1], dists[i - 1], dists[i]));
     

            }
            Results.Add(string.Format(""));
            Results.Add(string.Format("        = {0:f3} T", avg_PFS));
            Results.Add(string.Format(""));

            avg_PFS = avg_PFS * (1 / tl);

            int Nc = 0;

            for (int i = 0; i < cnt.Table_Mid_PSF_after_FS_Loss[4].Count; i++)
            {
                Nc += cnt.Table_Mid_PSF_after_FS_Loss[4][i].Nos_of_Cables;
            }
            //avg_PFS = avg_PFS * (1 / tl);

            //Average stress  
            double avg_strs_2 = (avg_PFS / (Nc * cnt.Tendon_Area / 100 * 0.001));
            Results.Add(string.Format("Average stress = avg_strs_2 = (avg_PFS / (Nc * Tendon_Area / 100 * 0.001))"));
            Results.Add(string.Format("                            = ({0:f3} / ({1} * {2} / 100 * 0.001))", avg_PFS, Nc, cnt.Tendon_Area));
            Results.Add(string.Format("                            = {0:f3}", avg_strs_2));
            Results.Add(string.Format(""));

            double Es_loss = ESL * 100 / avg_strs_2;
            Results.Add(string.Format(""));
            Results.Add(string.Format("%  ES Loss = ESL * 100 / avg_strs_2"));
            Results.Add(string.Format("           = {0:f3} x 100 / {1:f3}", ESL, avg_strs_2));
            Results.Add(string.Format("           = {0:f3} %", Es_loss));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            //Initial Force (after Friction+Slip+ES Loss)  =
            double init_frc = ((100 - Es_loss) / 100) * avg_PFS;
            Results.Add(string.Format("Initial Force (after Friction+Slip+ES Loss) = init_frc"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("   init_frc = ((100 - {0:f3}) / 100) * {1:f3}", Es_loss, avg_PFS));
            Results.Add(string.Format("            = {0:f3} Kg/cm^2", init_frc));
            Results.Add(string.Format(""));


            //Initial  Stress 
            double init_strs = (init_frc / (Nc * cnt.Tendon_Area / 100 * 0.001));
            Results.Add(string.Format("Initial Stress = init_strs = (init_frc / (Nc * Tendon_Area / 100 * 0.001))"));
            Results.Add(string.Format("                           = ({0:f3} / ({1} * {2:f3} / 100 * 0.001))", init_frc, Nc, cnt.Tendon_Area));
            Results.Add(string.Format("                           = {0:f3} Kg/cm^2", init_strs));
            Results.Add(string.Format(""));

            //Residual Shrinkage strain at 28 days 			=	1.90E-04 	(table-3 of IRC:18-2000)	
            Results.Add(string.Format("Residual Shrinkage strain at 28 days = res_shrink = 0.00019 (table-3 of IRC:18-2000)"));
            //Results.Add(string.Format("Residual Shrinkage strain at 28 days = 0.00019 (table-3 of IRC:18-2000)"));
            Results.Add(string.Format(""));

            double res_shrink = 0.00019;

            //Shrinkage Loss  
            double shrink_loss = res_shrink * cnt.Es * 10;
            Results.Add(string.Format("Shrinkage Loss = res_shrink * Es * 10 "));
            Results.Add(string.Format("               = {0} * {1:f3} * 10", res_shrink, cnt.Es));
            Results.Add(string.Format("               = {0:f3} Kg/cm^2", shrink_loss));
            Results.Add(string.Format(""));





            //ES Loss @ 
            StressSummaryData Es_loss_Top = new StressSummaryData();

            Es_loss_Top.Sl_No = "20";
            Es_loss_Top.Section = "";
            Es_loss_Top.Unit = "";
            StressSummaryData Es_loss_Bottom = new StressSummaryData();

            Es_loss_Bottom.Sl_No = "21";
            Es_loss_Bottom.Section = "";
            Es_loss_Bottom.Unit = "";

            //Stresses due  to Prestress after Friction and Slip & ES Losses


            StressSummaryData SPFSESL_Top = new StressSummaryData();

            SPFSESL_Top.Sl_No = "";
            SPFSESL_Top.Section = "";
            SPFSESL_Top.Unit = "";

            StressSummaryData SPFSESL_Bottom = new StressSummaryData();

            SPFSESL_Bottom.Sl_No = "";
            SPFSESL_Bottom.Section = "";
            SPFSESL_Bottom.Unit = "";

            //Resultant Stresses after PS and DL + ES

            StressSummaryData RSPSDLES_Top = new StressSummaryData();

            RSPSDLES_Top.Sl_No = "22";
            RSPSDLES_Top.Section = "Top";
            RSPSDLES_Top.Unit = "";



            StressSummaryData RSPSDLES_Bottom = new StressSummaryData();

            RSPSDLES_Bottom.Sl_No = "23";
            RSPSDLES_Bottom.Section = "Bottom";
            RSPSDLES_Bottom.Unit = "";


            //Point of Zero Stress from Bottom


            StressSummaryData PZSB = new StressSummaryData();

            PZSB.Sl_No = "";
            PZSB.Section = "";
            PZSB.Unit = "";

            //Point of C.G. of Prestress Force from Bottom
            StressSummaryData PCGPFB = new StressSummaryData();

            PCGPFB.Sl_No = "";
            PCGPFB.Section = "";
            PCGPFB.Unit = "";


            //Stress at the C.G. of Prestress Force

            StressSummaryData SCGPF = new StressSummaryData();

            PCGPFB.Sl_No = "";
            PCGPFB.Section = "";
            PCGPFB.Unit = "";

            for (int i = 0; i < dists.Count; i++)
            {
                Es_loss_Top.Add((-1) * Es_loss * spfsl_ft[i] / 100);
                Es_loss_Bottom.Add((-1) * Es_loss * spfsl_fb[i] / 100);

                SPFSESL_Top.Add(spfsl_ft[i] - (spfsl_ft[i] * Es_loss / 100));
                SPFSESL_Bottom.Add(spfsl_fb[i] - (spfsl_fb[i] * Es_loss / 100));


                val = SDLP_ft[i] + Es_loss_Top[i];
                RSPSDLES_Top.Add(val);

                val = SDLP_fb[i] + Es_loss_Bottom[i];
                RSPSDLES_Bottom.Add(val);


                if (RSPSDLES_Top[i] < 0)
                {
                    val = (RSPSDLES_Bottom[i] * (tl) / (RSPSDLES_Bottom[i] - RSPSDLES_Top[i]));
                }
                else if (RSPSDLES_Bottom[i] < 0)
                {
                    val = (RSPSDLES_Bottom[i] * (tl) / (RSPSDLES_Bottom[i] - RSPSDLES_Top[i]));
                }
                else
                {
                    val = 0;
                }
                PZSB.Add(val);


                val = cnt.Table_Mid_PSF_after_FS_Loss[i].Yb + cnt.Table_Mid_PSF_after_FS_Loss[i].e;
                PCGPFB.Add(val);


                if (PZSB[i] == 0)
                {
                    val = RSPSDLES_Bottom[i] - ((RSPSDLES_Bottom[i] - RSPSDLES_Top[i]) * PCGPFB[i] / (tl));
                }
                else
                {
                    val = RSPSDLES_Bottom[i] - (RSPSDLES_Bottom[i] * PCGPFB[i] / PZSB[i]);
                }
                SCGPF.Add(val);
            }

            //Average Initial Stress after Elastic Shortening Losses at C.G. of Prestressing
            val = dists[0];


            Results.Add(string.Format(""));
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
            Results.Add(ssd_dist.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(string.Format("ES Loss @  {0:f3} %", Es_loss));
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(Es_loss_Top.ToString());
            this.Add(Es_loss_Top);
            Results.Add(Es_loss_Bottom.ToString());
            this.Add(Es_loss_Bottom);
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(string.Format("Stresses due  to Prestress after Friction and Slip & ES Losses"));
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(SPFSESL_Top.ToString());
            this.Add(SPFSESL_Top);
            Results.Add(SPFSESL_Bottom.ToString());
            this.Add(SPFSESL_Bottom);
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(string.Format("Resultant Stresses after PS and DL + ES"));
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(RSPSDLES_Top.ToString());
            this.Add(RSPSDLES_Top);
            PCGPFB.Sl_No = "";
            Results.Add(RSPSDLES_Bottom.ToString());
            this.Add(RSPSDLES_Bottom);
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(string.Format("Point of Zero Stress from Bottom"));
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(PZSB.ToString());
            this.Add(PZSB);
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(string.Format("Point of C.G. of Prestress Force from Bottom"));
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(PCGPFB.ToString());
            this.Add(PCGPFB);
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(string.Format("Stress at the C.G. of Prestress Force"));
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(SCGPF.ToString());
            this.Add(SCGPF);
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Average Initial Stress after Elastic Shortening Losses at C.G. of Prestressing"));
            Results.Add(string.Format(""));

            double avg_ISESLCGP = 0.0;

            for (int i = 1; i < dists.Count; i++)
            {
                avg_ISESLCGP += ((SCGPF[i] + SCGPF[i - 1]) / 2.0) * (dists[i - 1] - dists[i]);



                if (i == 1)
                    Results.Add(string.Format("avg_ISESLCGP = (1/{0}) x [(({1:f3} + {2:f3}) / 2) x ({3} - {4})", tl, SCGPF[i], SCGPF[i - 1], dists[i - 1], dists[i]));
                else if (i == dists.Count - 1)
                    Results.Add(string.Format("                       + (({0:f3} + {1:f3}) / 2) x ({2} - {3})]", SCGPF[i], SCGPF[i - 1], dists[i - 1], dists[i]));
                else
                    Results.Add(string.Format("                       + (({0:f3} + {1:f3}) / 2) x ({2} - {3})", SCGPF[i], SCGPF[i - 1], dists[i - 1], dists[i]));
     

            }
            avg_ISESLCGP = (1 / tl) * avg_ISESLCGP;
            Results.Add(string.Format(""));
            Results.Add(string.Format("             = {0:f3} Kg/cm^2", avg_ISESLCGP));
            Results.Add(string.Format(""));


            //Creep  Loss  from 28- days  to Infinity

            //Creep Strain at 100 % maturity  			=	4.00E-04 	per 10 MPa	(table-2 of IRC:18-2000)	

            double CSM = 0.0004;
            Results.Add(string.Format("Creep Strain at 100 % maturity = {0} per 10 MPa	(table-2 of IRC:18-2000)", CSM));
            Results.Add(string.Format(""));

            //Creep Loss
            double creep_loss = (CSM * avg_ISESLCGP * cnt.Es * 10) / 100;
            Results.Add(string.Format("Creep Loss = (CSM * avg_ISESLCGP * Es * 10) / 100"));
            Results.Add(string.Format("           = ({0:f3} * {1:f3} * {2:f3} * 10) / 100", CSM, avg_ISESLCGP, cnt.Es));
            Results.Add(string.Format("           = {0:f3} Kg/cm^2", creep_loss));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            //Relaxation Loss
            double relx_loss = init_strs / (cnt.UTS * 10);
            Results.Add(string.Format("Relaxation Loss = relx_loss = init_strs / (UTS * 10) "));
            Results.Add(string.Format("                            = {0:f3} / ({1:f3} * 10)", init_strs, cnt.UTS));
            Results.Add(string.Format("                            = {0:f3} ", relx_loss));
            Results.Add(string.Format(""));

            //Assuming 1000 hrs relaxation loss at  0.6 UTS
            double asume_relx_lss_6 = 1.25;
            Results.Add(string.Format("Assuming 1000 hrs relaxation loss at  0.6 UTS = 1.25 %"));
            Results.Add(string.Format(""));
            double asume_relx_lss_5 = 0.0;
            Results.Add(string.Format("  and    1000 hrs relaxation loss at  0.5 UTS = 0 %"));


            //Therefore   1000 hrs relaxation loss at  

            double relx_1000_hrs_loss = (asume_relx_lss_6 / 0.1) * (relx_loss - 0.5);
            Results.Add(string.Format("Therefore   1000 hrs relaxation loss at {0:f4}", relx_loss));
            Results.Add(string.Format(""));
            Results.Add(string.Format("  relx_1000_hrs_loss = (asume_relx_lss_6 / 0.1) * (relx_loss - 0.5) "));
            Results.Add(string.Format("                     = ({0:f3} / 0.1) * ({1:f3} - 0.5) ", asume_relx_lss_6, relx_loss));
            Results.Add(string.Format("                     = {0:f3} % ", relx_1000_hrs_loss));
            Results.Add(string.Format(""));

            //Final Relaxation Loss 
            double final_relx_loss = (3.0 * relx_1000_hrs_loss);
            Results.Add(string.Format("Final Relaxation Loss = final_relx_loss = (3.0 x 1000 hr. value)"));
            Results.Add(string.Format("                                        = (3.0 * {0:f4})", relx_1000_hrs_loss));
            Results.Add(string.Format("                                        = {0:f4} %", final_relx_loss));


            //Total Time dependent Losses upto  Infinity
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Time dependent Losses upto  Infinity"));
            Results.Add(string.Format(""));

            //Creep Loss  
            double tot_creep_loss = creep_loss * 100 / init_strs;
            Results.Add(string.Format("Creep Loss = tot_creep_loss = creep_loss * 100 / init_strs"));
            Results.Add(string.Format("                            = {0:f3} * 100 / {1:f3}", creep_loss, init_strs));
            Results.Add(string.Format("                            = {0:f3} %", creep_loss, init_strs));
            Results.Add(string.Format(""));


            //Shrinkage Loss
            double tot_shrink_loss = shrink_loss * 100 / init_strs;
            Results.Add(string.Format("Shrinkage Loss = tot_shrink_loss = shrink_loss * 100 / init_strs"));
            Results.Add(string.Format("                                 = {0:f3} * 100 / {1:f3}", shrink_loss, init_strs));
            Results.Add(string.Format("                                 = {0:f3} %", tot_shrink_loss));
            Results.Add(string.Format(""));



            double tot_depnd_loss = tot_creep_loss + tot_shrink_loss + final_relx_loss;
            Results.Add(string.Format(""));
            Results.Add(string.Format("tot_depnd_loss = tot_creep_loss + tot_shrink_loss + final_relx_loss"));
            Results.Add(string.Format("               = {0:f3} + {1:f3} + {2:f3}", tot_creep_loss, tot_shrink_loss, final_relx_loss));
            Results.Add(string.Format("               = {0:f3} %", tot_creep_loss, tot_shrink_loss, final_relx_loss));
            Results.Add(string.Format(""));

            double add_depnd_loss = tot_depnd_loss * 0.2;
            Results.Add(string.Format(""));
            Results.Add(string.Format("20 % Extra Time Dependent Losses = tot_depnd_loss * 0.2 = {0:f3} * 0.2 = {1:f3}", tot_depnd_loss, add_depnd_loss));
            Results.Add(string.Format(""));

            double tot_losses = tot_depnd_loss + add_depnd_loss;
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Losses = tot_losses = tot_depnd_loss + add_depnd_loss"));
            Results.Add(string.Format("                          = {0:f3} + {1:f3}", tot_depnd_loss, add_depnd_loss));
            Results.Add(string.Format("                          = {0:f3} %", tot_losses));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            //Time Dependent Losses @


            StressSummaryData TDL_Top = new StressSummaryData();

            TDL_Top.Sl_No = "24";
            TDL_Top.Section = "Top";
            TDL_Top.Unit = "";


            StressSummaryData TDL_Bottom = new StressSummaryData();

            TDL_Bottom.Sl_No = "25";
            TDL_Bottom.Section = "Bottom";
            TDL_Bottom.Unit = "";


            //Resultant Stresses after Losses


            StressSummaryData RSL_Top = new StressSummaryData();

            RSL_Top.Sl_No = "26";
            RSL_Top.Section = "Top";
            RSL_Top.Unit = "";

            StressSummaryData RSL_Bottom = new StressSummaryData();

            RSL_Bottom.Sl_No = "27";
            RSL_Bottom.Section = "Bottom";
            RSL_Bottom.Unit = "";



            for (int i = 0; i < dists.Count; i++)
            {
                TDL_Top.Add((-1) * tot_depnd_loss * SPFSESL_Top[i] / 100);
                TDL_Bottom.Add((-1) * tot_depnd_loss * SPFSESL_Bottom[i] / 100);

                RSL_Top.Add(RSPSDLES_Top[i] + TDL_Top[i]);
                RSL_Bottom.Add(RSPSDLES_Bottom[i] + TDL_Bottom[i]);
            }


            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
            Results.Add(ssd_dist.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");


            Results.Add(string.Format("-----------------------------"));
            Results.Add(string.Format("Time Dependent Losses   @  {0:f3} %", tot_depnd_loss));
            Results.Add(string.Format("------------------------------"));
            Results.Add(TDL_Top.ToString());
            this.Add(TDL_Top);
            Results.Add(TDL_Bottom.ToString());
            this.Add(TDL_Bottom);



            StressSummaryData RSL_check = new StressSummaryData();

            RSL_check.Sl_No = "45";
            RSL_check.Section = "";
            RSL_check.Unit = "Kg/cm^2";

            //At temp. stage   f pt
            StressSummaryData RSL_temp_stage = new StressSummaryData();

            RSL_temp_stage.Sl_No = "46";
            RSL_temp_stage.Section = "";
            RSL_temp_stage.Unit = "Kg/cm^2";

            for (int i = 0; i < dists.Count; i++)
            {
                if (cnt.Fcu * 10 * 0.5 < 200)
                    val = cnt.Fcu * 10;
                else
                    val = 200;

                RSL_check.Add(val);
                RSL_temp_stage.Add(-val / 10);
            }



            string format = "{0,-16} {1,-9} {2,-9} {3,9:f3} {4,9:f3} {5,9:f3} {6,9:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3} {13,9:f3}";


            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Resultant Stresses after Losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(RSL_Top.ToString());
            this.Add(RSL_Top);
            Results.Add(RSL_check.ToString());
            this.Add(RSL_check);
            Results.Add(string.Format(""));
            Results.Add(string.Format(format,"","","",
                (RSL_Top.Jacking_End < RSL_check.Jacking_End ? "  OK  " : "NOT OK"),
                (RSL_Top._1_L2_20 < RSL_check._1_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._2_L2_20 < RSL_check._2_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._3_L2_20 < RSL_check._3_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._4_L2_20 < RSL_check._4_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._5_L2_20 < RSL_check._5_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._6_L2_20 < RSL_check._6_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._7_L2_20 < RSL_check._7_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._8_L2_20 < RSL_check._8_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._9_L2_20 < RSL_check._9_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._10_L2_20 < RSL_check._10_L2_20 ? "  OK  " : "NOT OK")));


            Results.Add(string.Format(""));
            Results.Add(RSL_Bottom.ToString());
            this.Add(RSL_Bottom);
            Results.Add(RSL_temp_stage.ToString());
            this.Add(RSL_temp_stage);
            Results.Add(string.Format(format, "", "", "",
                (RSL_Bottom.Jacking_End > RSL_temp_stage.Jacking_End ? "  OK  " : "NOT OK"),
                (RSL_Bottom._1_L2_20 > RSL_temp_stage._1_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._2_L2_20 > RSL_temp_stage._2_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._3_L2_20 > RSL_temp_stage._3_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._4_L2_20 > RSL_temp_stage._4_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._5_L2_20 > RSL_temp_stage._5_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._6_L2_20 > RSL_temp_stage._6_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._7_L2_20 > RSL_temp_stage._7_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._8_L2_20 > RSL_temp_stage._8_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._9_L2_20 > RSL_temp_stage._9_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._10_L2_20 > RSL_temp_stage._10_L2_20 ? "  OK  " : "NOT OK")));
            Results.Add(string.Format(""));




            Results.Add("----------------------------------------------------------");
            Results.Add("Loss of Prestress");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stress at C G of Prestress Force");
            Results.Add(string.Format("d = ({0} + {1}x2) - {2} = {3:f3}", cnt.Mid_cc_temp_supp, cnt.Mid_over_supp, cnt.dist_face, tl));
            Results.Add("----------------------------------------------------------");
            Results.Add(SCGPF_47.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            Results.Add(SCGPF_47.ToString());
            this.Add(SCGPF_47);



            //Refer S. no. 1 to 47
            //Total Loss %  i / c ES losses
            StressSummaryData RSL_48 = new StressSummaryData();

            RSL_48.Sl_No = "48";
            RSL_48.Section = "";
            RSL_48.Unit = "";


            //49	Fh after losses (T)
            StressSummaryData RSL_49 = new StressSummaryData();

            RSL_49.Sl_No = "49";
            RSL_49.Section = "";
            RSL_49.Unit = "";
            //50	Fcp = Fh/ A after losses	
            StressSummaryData RSL_50 = new StressSummaryData();

            RSL_50.Sl_No = "50";
            RSL_50.Section = "";
            RSL_50.Unit = "";
            //51	Fv before losses (T)	
            StressSummaryData RSL_51 = new StressSummaryData();

            RSL_51.Sl_No = "51";
            RSL_51.Section = "";
            RSL_51.Unit = "";
            //52	Total Loss %	
            StressSummaryData RSL_52 = new StressSummaryData();

            RSL_52.Sl_No = "52";
            RSL_52.Section = "";
            RSL_52.Unit = "";
            //53	Fv after losses  (T)	
            StressSummaryData RSL_53 = new StressSummaryData();

            RSL_53.Sl_No = "53";
            RSL_53.Section = "";
            RSL_53.Unit = "";
            //54	Fhe after losses	
            StressSummaryData RSL_54 = new StressSummaryData();

            RSL_54.Sl_No = "54";
            RSL_54.Section = "";
            RSL_54.Unit = "";
            //55 =54/5	M/Zb after losses	
            StressSummaryData RSL_55 = new StressSummaryData();

            RSL_55.Sl_No = "55 =54/5";
            RSL_55.Section = "";
            RSL_55.Unit = "";
            //56 =50+55	fpt	
            StressSummaryData RSL_56 = new StressSummaryData();

            RSL_56.Sl_No = "56 =50+55";
            RSL_56.Section = "fpt";
            RSL_56.Unit = "";


            for (int i = 0; i < dists.Count; i++)
            {
                if (cnt.Fcu * 10 * 0.5 < 200)
                    val = cnt.Fcu * 10;
                else
                    val = 200;

                RSL_check.Add(val);
                RSL_temp_stage.Add(val / 10);


                RSL_48.Add(Es_loss + tot_losses);



                val = (100 - RSL_48[i]) * psfs_Fh[i] / 100.0;

                RSL_49.Add(val);


                val = RSL_49[i] * 1000.0 / (sec_props_A[i] * 10000.0);
                RSL_50.Add(val);


                val = cnt.Table_Mid_PSF_after_FS_Loss[i].Fv;
                RSL_51.Add(val);

                RSL_52.Add(RSL_48[i]);

                val = ((100 - RSL_52[i]) * RSL_51[i]) / 100.0;
                RSL_53.Add(val);


                val = (100 - RSL_52[i]) * psfs_M[i] / 100.0;
                RSL_54.Add(val);

                val = RSL_54[i] * 1000 * 100 / (sec_props_Zb[i] * 1000000);
                RSL_55.Add(val);


                val = -RSL_55[i] + RSL_50[i];
                RSL_56.Add(val);
            }

            //Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            //Results.Add(ssd_dist.ToString());
            //Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add(string.Format(""));
            //Results.Add(string.Format("Refer S. no. 1 to 47"));
            //Results.Add(string.Format(""));

            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Total Loss %  i / c ES losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_48.ToString()));
            this.Add(RSL_48);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fh after losses (T)"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_49.ToString()));
            this.Add(RSL_49);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fcp = Fh/ A after losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_50.ToString()));
            this.Add(RSL_50);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fv before losses (T)"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_51.ToString()));
            this.Add(RSL_51);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Total Loss %"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_52.ToString()));
            this.Add(RSL_52);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fv after losses  (T)"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_53.ToString()));
            this.Add(RSL_53);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fh x e after losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_54.ToString()));
            this.Add(RSL_54);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("M/Zb after losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_55.ToString()));
            this.Add(RSL_55);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_56.ToString()));
            this.Add(RSL_56);
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            Set_Data();
        }

        public void Read_End_Span_Data(Continuous_Box_Girder_Design cnt)
        {


            Results = new List<string>();

            #region Distances
            List<double> dists = new List<double>();


            //dists.Add((cnt.End_Dist_Supp[13] - cnt.End_Dist_Supp[2]));
            for (int i = 2; i < 13; i++)
            {
                dists.Add(cnt.End_Dist_Supp[13] - cnt.End_Dist_Supp[i]);
            }
           

            dists.Sort();
            dists.Reverse();


            StressSummaryData ssd_dist = new StressSummaryData();

            ssd_dist.AddRange(dists.ToArray());



            #endregion Distances


            RichTextBox rtb = new RichTextBox();




            Results.Add("");
            Results.Add("----------------------------------------------------");
            //Results.Add("Stress Summary - {0} m", cnt.Analysis.L2);
            Results.Add(string.Format("Stress Summary :    {0} m", cnt.Analysis.L1));
            Results.Add("----------------------------------------------------");
            Results.Add(string.Format(""));

            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
            Results.Add(ssd_dist.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");

            //Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
          
            	


            StressSummaryData sec_props_A = new StressSummaryData();

            sec_props_A.Sl_No = "1";
            sec_props_A.Section = "A";
            sec_props_A.Unit = "sq.m";

            StressSummaryData sec_props_Ix = new StressSummaryData();

            sec_props_Ix.Sl_No = "";
            sec_props_Ix.Section = "Ix";
            sec_props_Ix.Unit = "m^4";

            StressSummaryData sec_props_Yt = new StressSummaryData();

            sec_props_Yt.Sl_No = "2";
            sec_props_Yt.Section = "Yt";
            sec_props_Yt.Unit = "m";

            StressSummaryData sec_props_Yb = new StressSummaryData();

            sec_props_Yb.Sl_No = "3";
            sec_props_Yb.Section = "Yb";
            sec_props_Yb.Unit = "m";


            StressSummaryData sec_props_Zt = new StressSummaryData();

            sec_props_Zt.Sl_No = "4";
            sec_props_Zt.Section = "Zt";
            sec_props_Zt.Unit = "m";

            StressSummaryData sec_props_Zb = new StressSummaryData();

            sec_props_Zb.Sl_No = "5";
            sec_props_Zb.Section = "Yb";
            sec_props_Zb.Unit = "m";

            for (int i = 0; i < cnt.Analysis.End_Span_Sections.Count; i++)
            {
                sec_props_A.Add(cnt.Analysis.End_Span_Sections[i].Total_Area);
                sec_props_Ix.Add(cnt.Analysis.End_Span_Sections[i].I);
                sec_props_Yt.Add(cnt.Analysis.End_Span_Sections[i].Yt);
                sec_props_Yb.Add(cnt.Analysis.End_Span_Sections[i].Yb);

                sec_props_Zt.Add(sec_props_Ix[i] / sec_props_Yt[i]);
                sec_props_Zb.Add(sec_props_Ix[i] / sec_props_Yb[i]);
            }

            for (int i = 4; i >=0 ; i--)
            {
                sec_props_A.Add(sec_props_A[i]);
                sec_props_Ix.Add(sec_props_Ix[i]);
                sec_props_Yt.Add(sec_props_Yt[i]);
                sec_props_Yb.Add(sec_props_Yb[i]);
                sec_props_Zt.Add(sec_props_Zt[i]);
                sec_props_Zb.Add(sec_props_Zb[i]);
            }



            this.Add(sec_props_A);
            this.Add(sec_props_Ix);
            this.Add(sec_props_Yt);
            this.Add(sec_props_Yb);
            this.Add(sec_props_Zt);
            this.Add(sec_props_Zb);

            //Results.Add("");
            Results.Add("----------------------");
            Results.Add("Section Properties");
            Results.Add("----------------------");
            Results.Add(sec_props_A.ToString());
            Results.Add(sec_props_Ix.ToString());
            Results.Add(sec_props_Yt.ToString());
            Results.Add(sec_props_Yb.ToString());
            Results.Add(sec_props_Zt.ToString());
            Results.Add(sec_props_Zb.ToString());




            //Pre stress after friction and slip
            //Results.Add("");
            Results.Add("-----------------------------------");
            Results.Add("Pre stress after friction and slip");
            Results.Add("-----------------------------------");
            //Results.Add("");

            StressSummaryData psfs_Fh = new StressSummaryData();

            psfs_Fh.Sl_No = "6";
            psfs_Fh.Section = "Fh";
            psfs_Fh.Unit = "T";


            StressSummaryData psfs_e = new StressSummaryData();

            psfs_e.Sl_No = "7";
            psfs_e.Section = "e";
            psfs_e.Unit = "m";

            StressSummaryData psfs_y = new StressSummaryData();

            psfs_y.Sl_No = "8";
            psfs_y.Section = "y";
            psfs_y.Unit = "m";


            StressSummaryData psfs_M = new StressSummaryData();

            psfs_M.Sl_No = "9=6*7";
            psfs_M.Section = "M=Fh*e";
            psfs_M.Unit = "T-m";

            StressSummaryData psfs_Fh_A = new StressSummaryData();

            psfs_Fh_A.Sl_No = "10=6/1";
            psfs_Fh_A.Section = "Fh/A";
            psfs_Fh_A.Unit = "kg/cm^2";

            StressSummaryData psfs_M_Zt = new StressSummaryData();

            psfs_M_Zt.Sl_No = "11=9/4";
            psfs_M_Zt.Section = "M/Zt";
            psfs_M_Zt.Unit = "kg/cm^2";

            StressSummaryData psfs_M_Zb = new StressSummaryData();

            psfs_M_Zb.Sl_No = "12=9/5";
            psfs_M_Zb.Section = "M/Zb";
            psfs_M_Zb.Unit = "kg/cm^2";


            //Stresses due to Prestress after Friction and slip Losses

            StressSummaryData spfsl_ft = new StressSummaryData();

            spfsl_ft.Sl_No = "13=10+11";
            spfsl_ft.Section = "ft";
            spfsl_ft.Unit = "kg/cm^2";


            StressSummaryData spfsl_fb = new StressSummaryData();

            spfsl_fb.Sl_No = "14=10+12";
            spfsl_fb.Section = "fb";
            spfsl_fb.Unit = "kg/cm^2";


            //Dead Load Stresses
            StressSummaryData DL_Md = new StressSummaryData();

            DL_Md.Sl_No = "15";
            DL_Md.Section = "DL Moment";
            DL_Md.Unit = "T-m";

            StressSummaryData DL_ft = new StressSummaryData();

            DL_ft.Sl_No = "16=15/4";
            DL_ft.Section = "ft=Md/Zt";
            DL_ft.Unit = "kg/cm^2";


            StressSummaryData DL_fb = new StressSummaryData();

            DL_fb.Sl_No = "17=15/5";
            DL_fb.Section = "fb=Md/Zb";
            DL_fb.Unit = "kg/cm^2";

            //Stresses due to DL and Prestress
            StressSummaryData SDLP_ft = new StressSummaryData();

            SDLP_ft.Sl_No = "18=13+16";
            SDLP_ft.Section = "ft";
            SDLP_ft.Unit = "kg/cm^2";

            StressSummaryData SDLP_fb = new StressSummaryData();

            SDLP_fb.Sl_No = "19=14+17";
            SDLP_fb.Section = "fb";
            SDLP_fb.Unit = "kg/cm^2";




            for (int i = 1; i < cnt.Table_End_PSF_after_FS_Loss.Count; i++)
            {
                //Pre stress after friction and slip

                psfs_Fh.Add(cnt.Table_End_PSF_after_FS_Loss[i].Fh);
                psfs_e.Add(cnt.Table_End_PSF_after_FS_Loss[i].e);
                psfs_y.Add(sec_props_Yb[i - 1] - psfs_e[i - 1]);

                psfs_M.Add(psfs_Fh[i - 1] * psfs_e[i - 1]);

                psfs_Fh_A.Add((psfs_Fh[i - 1] * 1000) / (sec_props_A[i - 1] * 10000));
                psfs_M_Zt.Add((psfs_M[i - 1] * 1000 * 100) / (sec_props_Zt[i - 1] * 10000 * 100));
                psfs_M_Zb.Add((psfs_M[i - 1] * 1000 * 100) / (sec_props_Zb[i - 1] * 10000 * 100));

                //Stresses due to Prestress after Friction and slip Losses

                spfsl_ft.Add(psfs_Fh_A[i - 1] + psfs_M_Zt[i - 1]);
                spfsl_fb.Add(psfs_Fh_A[i - 1] + psfs_M_Zb[i - 1]);
                if (i == 11)
                    DL_Md.Add(0.0);
                else
                    DL_Md.Add(cnt.Analysis.FRC_SPAN_1[i].BM_Incr);

                DL_ft.Add((DL_Md[i - 1] * 100000) / (sec_props_Zt[i - 1] * 1000000));
                DL_fb.Add(-(DL_Md[i - 1] * 100000) / (sec_props_Zb[i - 1] * 1000000));

                SDLP_ft.Add(spfsl_ft[i - 1] + DL_ft[i - 1]);
                SDLP_fb.Add(spfsl_fb[i - 1] + DL_fb[i - 1]);

            }
            Results.Add(psfs_Fh.ToString());

            this.Add(psfs_Fh);



            Results.Add(psfs_e.ToString());
            this.Add(psfs_e);
            Results.Add(psfs_y.ToString());
            this.Add(psfs_y);
            Results.Add("");
            Results.Add(psfs_M.ToString());
            this.Add(psfs_M);
            Results.Add("");
            Results.Add(psfs_Fh_A.ToString());
            this.Add(psfs_Fh_A);
            Results.Add(psfs_M_Zt.ToString());
            this.Add(psfs_M_Zt);
            Results.Add(psfs_M_Zb.ToString());
            this.Add(psfs_M_Zb);


            //Results.Add("");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stresses due to Prestress after Friction and slip Losses");
            Results.Add("----------------------------------------------------------");
            //Results.Add("");

            Results.Add(spfsl_ft.ToString());
            this.Add(spfsl_ft);
            Results.Add(spfsl_fb.ToString());
            this.Add(spfsl_fb);

            //Results.Add("");
            Results.Add("----------------------------------------------------------");
            Results.Add("Dead Load Stresses");
            Results.Add("----------------------------------------------------------");
            //Results.Add("");
            Results.Add(DL_Md.ToString());
            this.Add(DL_Md);
            Results.Add(DL_ft.ToString());
            this.Add(DL_ft);
            Results.Add(DL_fb.ToString());
            this.Add(DL_fb);

            //Results.Add("");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stresses due to DL and Prestress");
            Results.Add("----------------------------------------------------------");
            //Results.Add("");
            Results.Add(SDLP_ft.ToString());
            this.Add(SDLP_ft);
            Results.Add(SDLP_fb.ToString());
            this.Add(SDLP_fb);



            double val = dists[1];


            StressSummaryData SCGPF_47 = new StressSummaryData();

            SCGPF_47.Sl_No = "47=18+(19-18)(d-";
            SCGPF_47.Section = "8)/d";
            SCGPF_47.Unit = "kg/cm^2";

            double d = 0.0;



            for (int i = 0; i < sec_props_Yt.Count; i++)
            {
                d = (sec_props_Yb[i] + sec_props_Yt[i]);

                val = SDLP_ft[i] + (SDLP_fb[i] - SDLP_ft[i]) * (d - psfs_y[i]) / d;

                //47=18+(19-18)(d-8)/d
                SCGPF_47.Add(val);

            }

            Results.Add("----------------------------------------------------------");
            Results.Add("Loss of Prestress");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stress at C G of Prestress Force");

            double tl = dists[0];


            Results.Add(string.Format("d =  {0} ", d));
            Results.Add("----------------------------------------------------------");
            Results.Add(SCGPF_47.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            //Average Stress 
            double avg_strs = 0;
            Results.Add(string.Format(""));
            //Results.Add(string.Format("Average Stress"));
            //Results.Add(string.Format(""));

            for (int i = 1; i < dists.Count; i++)
            {
                avg_strs += ((SCGPF_47[i] + SCGPF_47[i - 1]) / 2.0) * (dists[i - 1] - dists[i]);


                if (i == 1)
                    Results.Add(string.Format("Average Stress = avg_strs = (1/{0}) x [(({1:f3} + {2:f3}) / 2) x ({3} - {4})", tl, SCGPF_47[i - 1], SCGPF_47[i], dists[i - 1], dists[i]));
                else if (i == dists.Count - 1)
                    Results.Add(string.Format("                                    + (({0:f3} + {1:f3}) / 2) x ({2} - {3})]", SCGPF_47[i - 1], SCGPF_47[i], dists[i - 1], dists[i]));
                else
                    Results.Add(string.Format("                                    + (({0:f3} + {1:f3}) / 2) x ({2} - {3})", SCGPF_47[i - 1], SCGPF_47[i], dists[i - 1], dists[i]));
            }


            Results.Add(string.Format(""));
            Results.Add(string.Format("                          = {0:f3} / {1:f3}", avg_strs, tl));
            Results.Add(string.Format(""));
            avg_strs = avg_strs * (1 / tl);
            Results.Add(string.Format("                          = {0:f3} Kg/cm^2", avg_strs));
            Results.Add(string.Format(""));

            double Ecj = 5000 * Math.Sqrt(cnt.Fcu) * 10;
            Results.Add(string.Format(""));
            Results.Add(string.Format("Ecj = 5000 * Sqrt(Fcu) * 10"));
            Results.Add(string.Format("    = 5000 * Sqrt({0}) * 10", cnt.Fcu));
            Results.Add(string.Format(""));
            Results.Add(string.Format("    = {0:f3} N/mm^2", Ecj / 10));
            Results.Add(string.Format(""));
            Results.Add(string.Format("    = {0:f3} Kg/cm^2", Ecj));
            Results.Add(string.Format(""));



            //Elastic Shortining loss 
            double ESL = 0.5 * (cnt.Es * 10 / Ecj) * avg_strs;
            Results.Add(string.Format("Elastic Shortining loss = ESL = 0.5 * (Es * 10 / Ecj) * avg_strs"));
            Results.Add(string.Format("                              = 0.5 * ({0:f3} * 10 / {1:f3}) * {2:f3}", cnt.Es, Ecj, avg_strs));
            Results.Add(string.Format("                              = {0:f3} N/mm^2", ESL));

            Results.Add(string.Format(""));

            //Average Prestress force after Friction and Slip 
            double avg_PFS = 0.0;
            Results.Add(string.Format("Average Prestress force after Friction and Slip = avg_PFS"));
            Results.Add(string.Format(""));

            for (int i = 1; i < dists.Count; i++)
            {
                avg_PFS += ((psfs_Fh[i] + psfs_Fh[i - 1]) / 2.0) * (dists[i - 1] - dists[i]);


                if (i == 1)
                    Results.Add(string.Format("avg_PFS = (1/{0}) x [(({1:f3} + {2:f3}) / 2) x ({3} - {4})", tl, psfs_Fh[i - 1], psfs_Fh[i], dists[i - 1], dists[i]));
                else if (i == dists.Count - 1)
                    Results.Add(string.Format("                  + (({0:f3} + {1:f3}) / 2) x ({2} - {3})]", psfs_Fh[i - 1], psfs_Fh[i], dists[i - 1], dists[i]));
                else
                    Results.Add(string.Format("                  + (({0:f3} + {1:f3}) / 2) x ({2} - {3})", psfs_Fh[i - 1], psfs_Fh[i], dists[i - 1], dists[i]));


            }
            Results.Add(string.Format(""));
            Results.Add(string.Format("        = {0:f3} / {1:f3}", avg_PFS, tl));
            Results.Add(string.Format(""));

            avg_PFS = avg_PFS * (1 / tl);
            Results.Add(string.Format("        = {0:f3} T", avg_PFS));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            int Nc = 0;

            for (int i = 0; i < cnt.Table_End_PSF_after_FS_Loss[4].Count; i++)
            {
                Nc += cnt.Table_End_PSF_after_FS_Loss[4][i].Nos_of_Cables;
            }
            //avg_PFS = avg_PFS * (1 / tl);

            //Average stress  
            double avg_strs_2 = (avg_PFS / (Nc * cnt.Tendon_Area / 100 * 0.001));
            Results.Add(string.Format("Average stress = avg_strs_2 = (avg_PFS / (Nc * Tendon_Area / 100 * 0.001))"));
            Results.Add(string.Format("                            = ({0:f3} / ({1} * {2} / 100 * 0.001))", avg_PFS, Nc, cnt.Tendon_Area));
            Results.Add(string.Format("                            = {0:f3}", avg_strs_2));
            Results.Add(string.Format(""));

            double Es_loss = ESL * 100 / avg_strs_2;
            Results.Add(string.Format(""));
            Results.Add(string.Format("%  ES Loss = ESL * 100 / avg_strs_2"));
            Results.Add(string.Format("           = {0:f3} x 100 / {1:f3}", ESL, avg_strs_2));
            Results.Add(string.Format("           = {0:f3} %", Es_loss));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            //Initial Force (after Friction+Slip+ES Loss)  =
            double init_frc = ((100 - Es_loss) / 100) * avg_PFS;
            Results.Add(string.Format("Initial Force (after Friction+Slip+ES Loss) = init_frc"));
            Results.Add(string.Format(""));
            Results.Add(string.Format("   init_frc = ((100 - {0:f3}) / 100) * {1:f3}", Es_loss, avg_PFS));
            Results.Add(string.Format("            = {0:f3} Kg/cm^2", init_frc));
            Results.Add(string.Format(""));


            //Initial  Stress 
            double init_strs = (init_frc / (Nc * cnt.Tendon_Area / 100 * 0.001));
            Results.Add(string.Format("Initial Stress = init_strs = (init_frc / (Nc * Tendon_Area / 100 * 0.001))"));
            Results.Add(string.Format("                           = ({0:f3} / ({1} * {2:f3} / 100 * 0.001))", init_frc, Nc, cnt.Tendon_Area));
            Results.Add(string.Format("                           = {0:f3} Kg/cm^2", init_strs));
            Results.Add(string.Format(""));

            //Residual Shrinkage strain at 28 days 			=	1.90E-04 	(table-3 of IRC:18-2000)	
            Results.Add(string.Format("Residual Shrinkage strain at 28 days = res_shrink = 0.00019 (table-3 of IRC:18-2000)"));
            //Results.Add(string.Format("Residual Shrinkage strain at 28 days = 0.00019 (table-3 of IRC:18-2000)"));
            Results.Add(string.Format(""));

            double res_shrink = 0.00019;

            //Shrinkage Loss  
            double shrink_loss = res_shrink * cnt.Es * 10;
            Results.Add(string.Format("Shrinkage Loss = res_shrink * Es * 10 "));
            Results.Add(string.Format("               = {0} * {1:f3} * 10", res_shrink, cnt.Es));
            Results.Add(string.Format("               = {0:f3} Kg/cm^2", shrink_loss));
            Results.Add(string.Format(""));





            //ES Loss @ 
            StressSummaryData Es_loss_Top = new StressSummaryData();

            Es_loss_Top.Sl_No = "20";
            Es_loss_Top.Section = "Top";
            Es_loss_Top.Unit = "";
            StressSummaryData Es_loss_Bottom = new StressSummaryData();

            Es_loss_Bottom.Sl_No = "21";
            Es_loss_Bottom.Section = "Bottom";
            Es_loss_Bottom.Unit = "";

            //Stresses due  to Prestress after Friction and Slip & ES Losses


            StressSummaryData SPFSESL_Top = new StressSummaryData();

            SPFSESL_Top.Sl_No = "";
            SPFSESL_Top.Section = "Top";
            SPFSESL_Top.Unit = "";

            StressSummaryData SPFSESL_Bottom = new StressSummaryData();

            SPFSESL_Bottom.Sl_No = "";
            SPFSESL_Bottom.Section = "Bottom";
            SPFSESL_Bottom.Unit = "";

            //Resultant Stresses after PS and DL + ES

            StressSummaryData RSPSDLES_Top = new StressSummaryData();

            RSPSDLES_Top.Sl_No = "22";
            RSPSDLES_Top.Section = "Top";
            RSPSDLES_Top.Unit = "";



            StressSummaryData RSPSDLES_Bottom = new StressSummaryData();

            RSPSDLES_Bottom.Sl_No = "23";
            RSPSDLES_Bottom.Section = "Bottom";
            RSPSDLES_Bottom.Unit = "";


            //Point of Zero Stress from Bottom


            StressSummaryData PZSB = new StressSummaryData();

            PZSB.Sl_No = "";
            PZSB.Section = "";
            PZSB.Unit = "";

            //Point of C.G. of Prestress Force from Bottom
            StressSummaryData PCGPFB = new StressSummaryData();

            PCGPFB.Sl_No = "";
            PCGPFB.Section = "";
            PCGPFB.Unit = "";


            //Stress at the C.G. of Prestress Force

            StressSummaryData SCGPF = new StressSummaryData();

            PCGPFB.Sl_No = "";
            PCGPFB.Section = "";
            PCGPFB.Unit = "";

            for (int i = 0; i < dists.Count; i++)
            {
                Es_loss_Top.Add((-1) * Es_loss * spfsl_ft[i] / 100);
                Es_loss_Bottom.Add((-1) * Es_loss * spfsl_fb[i] / 100);

                SPFSESL_Top.Add(spfsl_ft[i] - (spfsl_ft[i] * Es_loss / 100));
                SPFSESL_Bottom.Add(spfsl_fb[i] - (spfsl_fb[i] * Es_loss / 100));


                val = SDLP_ft[i] + Es_loss_Top[i];
                RSPSDLES_Top.Add(val);

                val = SDLP_fb[i] + Es_loss_Bottom[i];
                RSPSDLES_Bottom.Add(val);


                if (RSPSDLES_Top[i] < 0)
                {
                    val = (RSPSDLES_Bottom[i] * (tl) / (RSPSDLES_Bottom[i] - RSPSDLES_Top[i]));
                }
                else if (RSPSDLES_Bottom[i] < 0)
                {
                    val = (RSPSDLES_Bottom[i] * (tl) / (RSPSDLES_Bottom[i] - RSPSDLES_Top[i]));
                }
                else
                {
                    val = 0;
                }
                PZSB.Add(val);


                val = cnt.Table_End_PSF_after_FS_Loss[i].Yb + cnt.Table_End_PSF_after_FS_Loss[i].e;
                PCGPFB.Add(val);


                if (PZSB[i] == 0)
                {
                    val = RSPSDLES_Bottom[i] - ((RSPSDLES_Bottom[i] - RSPSDLES_Top[i]) * PCGPFB[i] / (tl));
                }
                else
                {
                    val = RSPSDLES_Bottom[i] - (RSPSDLES_Bottom[i] * PCGPFB[i] / PZSB[i]);
                }
                SCGPF.Add(val);
            }

            //Average Initial Stress after Elastic Shortening Losses at C.G. of Prestressing
            val = dists[0];


            Results.Add(string.Format(""));
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            Results.Add(ssd_dist.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(string.Format("ES Loss @         {0:f3} %", Es_loss));
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(Es_loss_Top.ToString());
            this.Add(Es_loss_Top);
            Results.Add(Es_loss_Bottom.ToString());
            this.Add(Es_loss_Bottom);
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(string.Format("Stresses due  to Prestress after Friction and Slip & ES Losses"));
            Results.Add(string.Format("----------------------------------------------------------------"));
            Results.Add(SPFSESL_Top.ToString());
            this.Add(SPFSESL_Top);
            Results.Add(SPFSESL_Bottom.ToString());
            this.Add(SPFSESL_Bottom);
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(string.Format("Resultant Stresses after PS and DL + ES"));
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(RSPSDLES_Top.ToString());
            this.Add(RSPSDLES_Top);
            PCGPFB.Sl_No = "";
            Results.Add(RSPSDLES_Bottom.ToString());
            this.Add(RSPSDLES_Bottom);
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(string.Format("Point of Zero Stress from Bottom"));
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(PZSB.ToString());
            this.Add(PZSB);
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(string.Format("Point of C.G. of Prestress Force from Bottom"));
            Results.Add(string.Format("---------------------------------------------"));
            Results.Add(PCGPFB.ToString());
            this.Add(PCGPFB);
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(string.Format("Stress at the C.G. of Prestress Force"));
            Results.Add(string.Format("-------------------------------------"));
            Results.Add(SCGPF.ToString());
            this.Add(SCGPF);
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format("Average Initial Stress after Elastic Shortening Losses at C.G. of Prestressing"));
            Results.Add(string.Format(""));

            double avg_ISESLCGP = 0.0;

            for (int i = 1; i < dists.Count; i++)
            {
                avg_ISESLCGP += ((SCGPF[i] + SCGPF[i - 1]) / 2.0) * (dists[i - 1] - dists[i]);



                if (i == 1)
                    Results.Add(string.Format("avg_ISESLCGP = (1/{0}) x [(({1:f3} + {2:f3}) / 2) x ({3} - {4})", tl, SCGPF[i], SCGPF[i - 1], dists[i - 1], dists[i]));
                else if (i == dists.Count - 1)
                    Results.Add(string.Format("                       + (({0:f3} + {1:f3}) / 2) x ({2} - {3})]", SCGPF[i], SCGPF[i - 1], dists[i - 1], dists[i]));
                else
                    Results.Add(string.Format("                       + (({0:f3} + {1:f3}) / 2) x ({2} - {3})", SCGPF[i], SCGPF[i - 1], dists[i - 1], dists[i]));


            }
            avg_ISESLCGP = (1 / tl) * avg_ISESLCGP;
            Results.Add(string.Format(""));
            Results.Add(string.Format("             = {0:f3} Kg/cm^2", avg_ISESLCGP));
            Results.Add(string.Format(""));


            //Creep  Loss  from 28- days  to Infinity

            //Creep Strain at 100 % maturity  			=	4.00E-04 	per 10 MPa	(table-2 of IRC:18-2000)	

            double CSM = 0.0004;
            Results.Add(string.Format("Creep Strain at 100 % maturity = {0} per 10 MPa	(table-2 of IRC:18-2000)", CSM));
            Results.Add(string.Format(""));

            //Creep Loss
            double creep_loss = (CSM * avg_ISESLCGP * cnt.Es * 10) / 100;
            Results.Add(string.Format("Creep Loss = (CSM * avg_ISESLCGP * Es * 10) / 100"));
            Results.Add(string.Format("           = ({0:f3} * {1:f3} * {2:f3} * 10) / 100", CSM, avg_ISESLCGP, cnt.Es));
            Results.Add(string.Format("           = {0:f3} Kg/cm^2", creep_loss));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            //Relaxation Loss
            double relx_loss = init_strs / (cnt.UTS * 10);
            Results.Add(string.Format("Relaxation Loss = relx_loss = init_strs / (UTS * 10) "));
            Results.Add(string.Format("                            = {0:f3} / ({1:f3} * 10)", init_strs, cnt.UTS));
            Results.Add(string.Format("                            = {0:f3} ", relx_loss));
            Results.Add(string.Format(""));

            //Assuming 1000 hrs relaxation loss at  0.6 UTS
            double asume_relx_lss_6 = 1.25;
            Results.Add(string.Format("Assuming 1000 hrs relaxation loss at  0.6 UTS = 1.25 %"));
            Results.Add(string.Format(""));
            double asume_relx_lss_5 = 0.0;
            Results.Add(string.Format("  and    1000 hrs relaxation loss at  0.5 UTS = 0 %"));


            //Therefore   1000 hrs relaxation loss at  

            double relx_1000_hrs_loss = (asume_relx_lss_6 / 0.1) * (relx_loss - 0.5);
            Results.Add(string.Format("Therefore   1000 hrs relaxation loss at {0:f4}", relx_loss));
            Results.Add(string.Format(""));
            Results.Add(string.Format("  relx_1000_hrs_loss = (asume_relx_lss_6 / 0.1) * (relx_loss - 0.5) "));
            Results.Add(string.Format("                     = ({0:f3} / 0.1) * ({1:f3} - 0.5) ", asume_relx_lss_6, relx_loss));
            Results.Add(string.Format("                     = {0:f3} % ", relx_1000_hrs_loss));
            Results.Add(string.Format(""));

            //Final Relaxation Loss 
            double final_relx_loss = (3.0 * relx_1000_hrs_loss);
            Results.Add(string.Format("Final Relaxation Loss = final_relx_loss = (3.0 x 1000 hr. value)"));
            Results.Add(string.Format("                                        = (3.0 * {0:f4})", relx_1000_hrs_loss));
            Results.Add(string.Format("                                        = {0:f4} %", final_relx_loss));


            //Total Time dependent Losses upto  Infinity
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Time dependent Losses upto  Infinity"));
            Results.Add(string.Format(""));

            //Creep Loss  
            double tot_creep_loss = creep_loss * 100 / init_strs;
            Results.Add(string.Format("Creep Loss = tot_creep_loss = creep_loss * 100 / init_strs"));
            Results.Add(string.Format("                            = {0:f3} * 100 / {1:f3}", creep_loss, init_strs));
            Results.Add(string.Format("                            = {0:f3} %", creep_loss, init_strs));
            Results.Add(string.Format(""));


            //Shrinkage Loss
            double tot_shrink_loss = shrink_loss * 100 / init_strs;
            Results.Add(string.Format("Shrinkage Loss = tot_shrink_loss = shrink_loss * 100 / init_strs"));
            Results.Add(string.Format("                                 = {0:f3} * 100 / {1:f3}", shrink_loss, init_strs));
            Results.Add(string.Format("                                 = {0:f3} %", tot_shrink_loss));
            Results.Add(string.Format(""));



            double tot_depnd_loss = tot_creep_loss + tot_shrink_loss + final_relx_loss;
            Results.Add(string.Format(""));
            Results.Add(string.Format("tot_depnd_loss = tot_creep_loss + tot_shrink_loss + final_relx_loss"));
            Results.Add(string.Format("               = {0:f3} + {1:f3} + {2:f3}", tot_creep_loss, tot_shrink_loss, final_relx_loss));
            Results.Add(string.Format("               = {0:f3} %", tot_creep_loss, tot_shrink_loss, final_relx_loss));
            Results.Add(string.Format(""));

            double add_depnd_loss = tot_depnd_loss * 0.2;
            Results.Add(string.Format(""));
            Results.Add(string.Format("20 % Extra Time Dependent Losses = tot_depnd_loss * 0.2 = {0:f3} * 0.2 = {1:f3}", tot_depnd_loss, add_depnd_loss));
            Results.Add(string.Format(""));

            double tot_losses = tot_depnd_loss + add_depnd_loss;
            Results.Add(string.Format(""));
            Results.Add(string.Format("Total Losses = tot_losses = tot_depnd_loss + add_depnd_loss"));
            Results.Add(string.Format("                          = {0:f3} + {1:f3}", tot_depnd_loss, add_depnd_loss));
            Results.Add(string.Format("                          = {0:f3} %", tot_losses));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            //Time Dependent Losses @


            StressSummaryData TDL_Top = new StressSummaryData();

            TDL_Top.Sl_No = "24";
            TDL_Top.Section = "Top";
            TDL_Top.Unit = "";


            StressSummaryData TDL_Bottom = new StressSummaryData();

            TDL_Bottom.Sl_No = "25";
            TDL_Bottom.Section = "Bottom";
            TDL_Bottom.Unit = "";


            //Resultant Stresses after Losses


            StressSummaryData RSL_Top = new StressSummaryData();

            RSL_Top.Sl_No = "26";
            RSL_Top.Section = "Top";
            RSL_Top.Unit = "";

            StressSummaryData RSL_Bottom = new StressSummaryData();

            RSL_Bottom.Sl_No = "27";
            RSL_Bottom.Section = "Bottom";
            RSL_Bottom.Unit = "";



            for (int i = 0; i < dists.Count; i++)
            {
                TDL_Top.Add((-1) * tot_depnd_loss * SPFSESL_Top[i] / 100);
                TDL_Bottom.Add((-1) * tot_depnd_loss * SPFSESL_Bottom[i] / 100);

                RSL_Top.Add(SPFSESL_Top[i] + TDL_Top[i]);
                RSL_Bottom.Add(SPFSESL_Bottom[i] + TDL_Bottom[i]);
            }


            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            Results.Add("Sl.No.          Section   Unit       F/Dia sup 1  1L1/10    2L1/10    3L1/10    4L1/10    5L1/10    6L1/10    7L1/10    8L1/20    9L1/10   DEAD END");
            Results.Add(ssd_dist.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");


            Results.Add(string.Format("-----------------------------"));
            Results.Add(string.Format("Time Dependent Losses @"));
            Results.Add(string.Format("------------------------------"));
            Results.Add(TDL_Top.ToString());
            this.Add(TDL_Top);
            Results.Add(TDL_Bottom.ToString());
            this.Add(TDL_Bottom);



            StressSummaryData RSL_check = new StressSummaryData();

            RSL_check.Sl_No = "45";
            RSL_check.Section = "";
            RSL_check.Unit = "Kg/cm^2";

            //At temp. stage   f pt
            StressSummaryData RSL_temp_stage = new StressSummaryData();

            RSL_temp_stage.Sl_No = "46";
            RSL_temp_stage.Section = "";
            RSL_temp_stage.Unit = "Kg/cm^2";

            for (int i = 0; i < dists.Count; i++)
            {
                if (cnt.Fcu * 10 * 0.5 < 200)
                    val = cnt.Fcu * 10;
                else
                    val = 200;

                RSL_check.Add(val);
                RSL_temp_stage.Add(-val / 10);
            }



            string format = "{0,-16} {1,-9} {2,-9} {3,9:f3} {4,9:f3} {5,9:f3} {6,9:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3} {13,9:f3}";


            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Resultant Stresses after Losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(RSL_Top.ToString());
            this.Add(RSL_Top);
            Results.Add(RSL_check.ToString());
            this.Add(RSL_check);
            Results.Add(string.Format(""));
            Results.Add(string.Format(format, "", "", "",
                (RSL_Top.Jacking_End < RSL_check.Jacking_End ? "  OK  " : "NOT OK"),
                (RSL_Top._1_L2_20 < RSL_check._1_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._2_L2_20 < RSL_check._2_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._3_L2_20 < RSL_check._3_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._4_L2_20 < RSL_check._4_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._5_L2_20 < RSL_check._5_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._6_L2_20 < RSL_check._6_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._7_L2_20 < RSL_check._7_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._8_L2_20 < RSL_check._8_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._9_L2_20 < RSL_check._9_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Top._10_L2_20 < RSL_check._10_L2_20 ? "  OK  " : "NOT OK")));


            Results.Add(string.Format(""));
            Results.Add(RSL_Bottom.ToString());
            this.Add(RSL_Bottom);
            Results.Add(RSL_temp_stage.ToString());
            this.Add(RSL_temp_stage);
            Results.Add(string.Format(format, "", "", "",
                (RSL_Bottom.Jacking_End > RSL_temp_stage.Jacking_End ? "  OK  " : "NOT OK"),
                (RSL_Bottom._1_L2_20 > RSL_temp_stage._1_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._2_L2_20 > RSL_temp_stage._2_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._3_L2_20 > RSL_temp_stage._3_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._4_L2_20 > RSL_temp_stage._4_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._5_L2_20 > RSL_temp_stage._5_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._6_L2_20 > RSL_temp_stage._6_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._7_L2_20 > RSL_temp_stage._7_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._8_L2_20 > RSL_temp_stage._8_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._9_L2_20 > RSL_temp_stage._9_L2_20 ? "  OK  " : "NOT OK"),
                (RSL_Bottom._10_L2_20 > RSL_temp_stage._10_L2_20 ? "  OK  " : "NOT OK")));
            Results.Add(string.Format(""));




            Results.Add("----------------------------------------------------------");
            Results.Add("Loss of Prestress");
            Results.Add("----------------------------------------------------------");
            Results.Add("Stress at C G of Prestress Force");
            Results.Add(string.Format("d = {0:f3}", d));
            Results.Add("----------------------------------------------------------");
            Results.Add(SCGPF_47.ToString());
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));


            Results.Add(SCGPF_47.ToString());
            this.Add(SCGPF_47);



            //Refer S. no. 1 to 47
            //Total Loss %  i / c ES losses
            StressSummaryData RSL_48 = new StressSummaryData();

            RSL_48.Sl_No = "48";
            RSL_48.Section = "";
            RSL_48.Unit = "";


            //49	Fh after losses (T)
            StressSummaryData RSL_49 = new StressSummaryData();

            RSL_49.Sl_No = "49";
            RSL_49.Section = "";
            RSL_49.Unit = "";
            //50	Fcp = Fh/ A after losses	
            StressSummaryData RSL_50 = new StressSummaryData();

            RSL_50.Sl_No = "50";
            RSL_50.Section = "";
            RSL_50.Unit = "";
            //51	Fv before losses (T)	
            StressSummaryData RSL_51 = new StressSummaryData();

            RSL_51.Sl_No = "51";
            RSL_51.Section = "";
            RSL_51.Unit = "";
            //52	Total Loss %	
            StressSummaryData RSL_52 = new StressSummaryData();

            RSL_52.Sl_No = "52";
            RSL_52.Section = "";
            RSL_52.Unit = "";
            //53	Fv after losses  (T)	
            StressSummaryData RSL_53 = new StressSummaryData();

            RSL_53.Sl_No = "53";
            RSL_53.Section = "";
            RSL_53.Unit = "";
            //54	Fhe after losses	
            StressSummaryData RSL_54 = new StressSummaryData();

            RSL_54.Sl_No = "54";
            RSL_54.Section = "";
            RSL_54.Unit = "";
            //55 =54/5	M/Zb after losses	
            StressSummaryData RSL_55 = new StressSummaryData();

            RSL_55.Sl_No = "55 =54/5";
            RSL_55.Section = "";
            RSL_55.Unit = "";
            //56 =50+55	fpt	
            StressSummaryData RSL_56 = new StressSummaryData();

            RSL_56.Sl_No = "56 =50+55";
            RSL_56.Section = "fpt";
            RSL_56.Unit = "";


            for (int i = 0; i < dists.Count; i++)
            {
                if (cnt.Fcu * 10 * 0.5 < 200)
                    val = cnt.Fcu * 10;
                else
                    val = 200;

                RSL_check.Add(val);
                RSL_temp_stage.Add(val / 10);


                RSL_48.Add(Es_loss + tot_losses);



                val = (100 - RSL_48[i]) * psfs_Fh[i] / 100.0;

                RSL_49.Add(val);


                val = RSL_49[i] * 1000.0 / (sec_props_A[i] * 10000.0);
                RSL_50.Add(val);


                val = cnt.Table_End_PSF_after_FS_Loss[i].Fv;
                RSL_51.Add(val);

                RSL_52.Add(RSL_48[i]);

                val = ((100 - RSL_52[i]) * RSL_51[i]) / 100.0;
                RSL_53.Add(val);


                val = (100 - RSL_52[i]) * psfs_M[i] / 100.0;
                RSL_54.Add(val);

                val = RSL_54[i] * 1000 * 100 / (sec_props_Zb[i] * 1000000);
                RSL_55.Add(val);


                val = -RSL_55[i] + RSL_50[i];
                RSL_56.Add(val);
            }

            //Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add("Sl.No.          Section   Unit         Jacking    1L2/20    2L2/20    3L2/20    4L2/20    5L2/20    6L2/20    7L2/20    8L2/20    9L2/20   10L2/20");
            //Results.Add(ssd_dist.ToString());
            //Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            //Results.Add(string.Format(""));
            //Results.Add(string.Format("Refer S. no. 1 to 47"));
            //Results.Add(string.Format(""));

            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Total Loss %  i / c ES losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_48.ToString()));
            this.Add(RSL_48);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fh after losses (T)"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_49.ToString()));
            this.Add(RSL_49);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fcp = Fh/ A after losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_50.ToString()));
            this.Add(RSL_50);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fv before losses (T)"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_51.ToString()));
            this.Add(RSL_51);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Total Loss %"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_52.ToString()));
            this.Add(RSL_52);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fv after losses  (T)"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_53.ToString()));
            this.Add(RSL_53);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("Fh x e after losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_54.ToString()));
            this.Add(RSL_54);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format("M/Zb after losses"));
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_55.ToString()));
            this.Add(RSL_55);
            Results.Add(string.Format("--------------------------------"));
            Results.Add(string.Format(RSL_56.ToString()));
            this.Add(RSL_56);
            Results.Add("---------------------------------------------------------------------------------------------------------------------------------------------------");
            Results.Add(string.Format(""));
            Results.Add(string.Format(""));

            Set_Data();
        }
        public void Set_Data()
        {
            Data.Clear();
            MyList mlist = null;
            foreach (var item in this)
            {
                if (item.Sl_No != "")
                {
                    if (item.Sl_No.Contains("="))
                    {
                        mlist = new MyList(item.Sl_No, '=');
                        Data.Add(mlist[0], item);
                    }
                    else
                        Data.Add(item.Sl_No, item);
                }

            }
        }
    }

    public class StressSummaryData : List<double>
    {
        public string Sl_No { get; set; }
        public string Section { get; set; }
        public string Unit { get; set; }


        public double Jacking_End { get { return this[0]; } set { this[0] = value; } }
        public double _1_L2_20 { get { return this[1]; } set { this[1] = value; } }
        public double _2_L2_20 { get { return this[2]; } set { this[2] = value; } }
        public double _3_L2_20 { get { return this[3]; } set { this[3] = value; } }
        public double _4_L2_20 { get { return this[4]; } set { this[4] = value; } }
        public double _5_L2_20 { get { return this[5]; } set { this[5] = value; } }
        public double _6_L2_20 { get { return this[6]; } set { this[6] = value; } }
        public double _7_L2_20 { get { return this[7]; } set { this[7] = value; } }
        public double _8_L2_20 { get { return this[8]; } set { this[8] = value; } }
        public double _9_L2_20 { get { return this[9]; } set { this[9] = value; } }
        public double _10_L2_20 { get { return this[10]; } set { this[10] = value; } }

         
        public StressSummaryData()
            : base()
        {
            Sl_No = "";
            Section = "";
            Unit = "";

            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
            //this.Add(0);
        }



        public override string ToString()
        {
            string format = "{0,-16} {1,-9} {2,-9} {3,9:f3} {4,9:f3} {5,9:f3} {6,9:f3} {7,9:f3} {8,9:f3} {9,9:f3} {10,9:f3} {11,9:f3} {12,9:f3} {13,9:f3}";
            try
            {
                return string.Format(format,
                    Sl_No,
                    Section,
                    Unit,
                    Jacking_End,
                    _1_L2_20,
                    _2_L2_20,
                    _3_L2_20,
                    _4_L2_20,
                    _5_L2_20,
                    _6_L2_20,
                    _7_L2_20,
                    _8_L2_20,
                    _9_L2_20,
                    _10_L2_20);
            }
            catch (Exception ex) { }
            return "";
        }
    }
}