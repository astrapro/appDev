using System;
using System.Collections.Generic;
using System.IO;

using AstraInterface.Interface;

namespace BridgeAnalysisDesign.BearingDesign
{
    public enum eDesignBearing
    {
        VERSO_MONO_AXIAL_BEARING_TRANSVERSE = 0,
        VERSO_FIXED_BEARING = 1,
        VERSO_BI_AXIAL_BEARING = 2,
        VERSO_MONO_AXIAL_BEARING_LONGITUDINAL = 3,
    }

    public class POT_PTFE_VERSO_BEARING_DESIGN
    {
        /*
        Symbols
            
         * 
           𝜃          𝜃          √          π          λ          σ          µ          °      
           γ          δ          ρ          β          φ          Σ          α          ∆
         * 
        */

        IApplication iapp;
        public eDesignBearing BearingDesign { get; set; }

        public POT_PTFE_VERSO_BEARING_DESIGN(IApplication app, eDesignBearing bearing_des)
        {
            iapp = app;
            BearingDesign = bearing_des;
        }

        #region Parameters
        #region Design Parameter
        public double Nmax { get; set; }
        public double Nnorm { get; set; }
        public double Nmin { get; set; }
        public double theta_p { get; set; }
        public double theta_v { get; set; }
        public double theta { get; set; }
        public double e { get; set; }
        public double Hlatn { get; set; }
        public double Hlts { get; set; }
        public double Hlng_n { get; set; }
        public double Hlng_s { get; set; }
        public double H { get; set; }
        public double elong { get; set; }
        public double etrans { get; set; }
        #endregion Design Parameter


        #region Bearing Parameter

        #region Elastomer
        public double di { get; set; }
        public double he { get; set; }
        public double Ae { get { return (Math.PI * di * di / 4.0); } }
        public double IRHD { get; set; }
        public double k1 { get; set; }
        public double k2 { get; set; }
        public double Med { get { return (di * di * di * (k1 * theta_p + k2 * theta_v)); } }
        public double MRdn { get { return (0.2 * (di / 2) * Hlng_n * 1000); } }
        public double MRst { get { return (0.2 * (di / 2) * Hlts * 1000); } }
        public double MRsl { get { return (0.2 * (di / 2) * Hlng_s * 1000); } }
        public double dp { get; set; }
        public double hm { get; set; }
        public double hts { get; set; }
        public double w { get; set; }
        public double An { get; set; }




        public double Do_ { get; set; }
        public double do_ { get; set; }
        public double deb { get { return (di + 2 * 2 * kb); } }
        public double bp { get { return ((do_ - di) / 2); } }

        public double deb_avibl { get; set; }
        public double kb { get; set; }
        public double hc { get; set; }

        public double a { get; set; }
        public double ks { get; set; }
        public double as_ { get; set; }
        public double ku { get; set; }
        public double b { get; set; }
        public double kt { get; set; }
        public double bs { get; set; }
        public double Lu { get; set; }
        public double det
        {

            get
            {
                if (BearingDesign == eDesignBearing.VERSO_FIXED_BEARING)
                    return Math.Min((di + 2 * 2 * hm), b);
                else
                    return Math.Min((dp + 2 * 2 * (kt + ks)), b);
            }
        }
        public double det_avibl { get; set; }

        public double bottom_Mj { get; set; }
        public double bottom_n { get; set; }
        public double bottom_D { get; set; }
        public double bottom_Ab { get; set; }
        public double bottom_bc { get; set; }
        public double bottom_L { get; set; }

        public double top_Mj { get; set; }
        public double top_n { get; set; }
        public double top_Ab { get; set; }
        public double top_bc { get; set; }

        public double ha { get { return (kb + he + (w / 2.0)); } }
        public double h { get; set; }


 



        #endregion Elastomer



        #endregion Bearing Parameter

        public double fck { get; set; }

        public double conc_ads_norm { get { return (0.25 * fck * (deb_avibl / deb)); } }
        public double conc_abs_norm { get { return (0.33 * fck); } }
        public double conc_ads_seismic { get { return (1.25 * (0.25 * fck * (deb_avibl / deb))); } }
        public double conc_abs_seismic { get { return (1.25 * 0.33 * fck); } }

        public double conc_sigma_cb_seismic { get { return (Nmax * 1000.0 / (Math.PI * deb * deb / 4.0)); } }
        public double conc_sigma_cb_norm { get { return (Nnorm * 1000.0 / (Math.PI * deb * deb / 4.0)); } }

        public double conc_sigma_cbc_norm
        {
            get
            {
                if(BearingDesign == eDesignBearing.VERSO_BI_AXIAL_BEARING)
                    return (H * 1000 * ha / (Math.PI * deb * deb * deb / 32) + (Med + MRdn) / ((Math.PI * deb * deb * deb / 32)));
                
                return (Hlng_n * 1000 * ha / (Math.PI * deb * deb * deb / 32) + (Med + MRdn) / ((Math.PI * deb * deb * deb / 32)));
            }
        }
        public double conc_sigma_cbc_seismic { get; set; }

        public double lamda_b_norm { get; set; }
        public double lamda_b_seismic { get; set; }


        public double steel_fy { get; set; }
        public double fy { get; set; }



        public double steel_ads_norm { get; set; }
        public double steel_abs_norm { get; set; }

        public double steel_ads_seismic { get; set; }
        public double steel_abs_seismic { get; set; }

        public double steel_sigma_ct_seismic { get; set; }
        public double steel_sigma_ct_norm { get; set; }


        public double steel_sigma_ctc_seismic { get; set; }
        public double steel_sigma_ctc_norm { get; set; }

        public double steel_Hlat { get; set; }

        public double lamda_t_norm { get; set; }
        public double lamda_t_seismic { get; set; }




        // PTFE
        public double sigma_cp { get; set; }
        public double sigma_cpc { get; set; }


        // ELASTOMERIC DISC
        public double sigma_ce { get; set; }
        public double sigma_cpce { get; set; }
        public double eff { get; set; }
        public double clrns { get; set; }


        //POT CYLINDER
        public double sigma_t1 { get; set; }
        public double sigma_t2 { get; set; }
        public double sigma_t { get; set; }

        // POT INTERFACE
        public double sigma_q1 { get; set; }
        public double sigma_q2 { get; set; }
        public double sigma_q { get; set; }

        public double sigma_bt1 { get; set; }
        public double sigma_bt2 { get; set; }
        public double sigma_bt { get; set; }


        public double sigma_e { get; set; }

        public double we { get; set; }


        // GUIDE

        public double sigma_qu { get; set; }
        public double sigma_bu { get; set; }
        public double sigma_eu { get; set; }
        public double sigma_du { get; set; }



        // ANCHOR
        //Bottom
        public double bottom_Hperm { get; set; }
        public double bottom_mu { get; set; }
        public double bottom_sigma_bq_perm { get; set; }

        public double bottom_H_Maxm { get; set; }

        //Top
        public double top_Hperm { get; set; }
        public double top_mu { get; set; }
        public double top_sigma_bq_perm { get; set; }

        public double top_H_Maxm { get; set; }

        public double sigma_cav { get; set; }
        public double Sctr { get; set; }
        public double sigma_cpk { get; set; }

        #endregion Parameters
        public string Working_Folder { get; set; }

        public string Report_File
        {
            get
            {
                string file_name = "PTFE_" + BearingDesign.ToString();

                string str = Path.Combine(Working_Folder, file_name);

                str = Path.Combine(Working_Folder, file_name);
                
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }

                return Path.Combine(str, file_name + ".TXT");
            }
        }

        public string Drawing_Folder
        {
            get
            {
                string str = "";

                
                string file_name = "PTFE_" + BearingDesign.ToString();

                str = Path.Combine(Working_Folder, file_name);


                //str = Path.Combine(str, "DRAWINGS");
                
                
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }

                return str;
            }
        }
       

        public void Generate_Report()
        {
            switch (BearingDesign)
            {
                case eDesignBearing.VERSO_MONO_AXIAL_BEARING_TRANSVERSE:
                    Generate_Report_VERSO_MONO_AXIAL_BEARING_TRANSVERSE();
                    break;
                case eDesignBearing.VERSO_FIXED_BEARING:
                    Generate_Report_VERSO_FIXED_BEARING();
                    break;
                case eDesignBearing.VERSO_BI_AXIAL_BEARING:
                    Generate_Report_VERSO_BI_AXIAL_BEARING();
                    break;
                case eDesignBearing.VERSO_MONO_AXIAL_BEARING_LONGITUDINAL:
                    Generate_Report_VERSO_MONO_AXIAL_BEARING_LONGITUDINAL();
                    break;
            }
        }
        public void Generate_Report_VERSO_MONO_AXIAL_BEARING_TRANSVERSE()
        {
            List<string> list = new List<string>();
            #region TechSOFT Banner

            list.Add(string.Format(""));
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 20.0          *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*        DESIGN OF STEFLOMET  POT/PTFE       *");
            list.Add("\t\t*     VERSO-MONO AXIAL TRANSVERSE BEARING    *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #region Step 1 :  DESIGN PARAMETERS
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 1 :  DESIGN PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Vertical load [Nmax] = {0} kN", Nmax));
            list.Add(string.Format("Transverse Horizontal Force Normal [Hlatn]= {0} kN", Hlatn));

            list.Add(string.Format("Normal Vertical Load [Nnorm] = {0} kN", Nnorm));
            list.Add(string.Format("Transverse Horzontal Force Seismic [Hlts] = {0} kN", Hlts));

            list.Add(string.Format("Dead load [Nmin] = {0:f3} kN", Nmin));
            list.Add(string.Format("Long Horizontal Force Normal [Hlng_n] = {0} kN", Hlng_n));
            list.Add(string.Format("Rotation due to Dead Load [θp] = {0} rad", theta_p));
            list.Add(string.Format("Long Horizontal Force Seismic [Hlng_s] = {0} kN", Hlng_s));
            list.Add(string.Format("Rotation due to Live Load [θy] = {0} rad", theta_v));
            list.Add(string.Format("Design Horizontal Force For bearing components only [H] = {0} rad", H));


            list.Add(string.Format("Rotation [θ] = {0} rad", theta));

            list.Add(string.Format("Long Movement [elong](±)  = {0} mm", elong));
            list.Add(string.Format("Transverse Movement [etrans](±)  = {0} mm", etrans));

            list.Add(string.Format(""));

            #endregion Step 1 :  DESIGN PARAMETERS

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2 :  BEARING PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.1 : ELASTOMER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));
            list.Add(string.Format("Dia of Elastomeric pad [di] = {0} mm", di));
            list.Add(string.Format("Thickness of the Pad [he] = {0} mm", he));
            //list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4 = {0:f4} mm^2", Ae));

            list.Add(string.Format(""));
            list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4"));
            list.Add(string.Format("                 = (π x {0}^2) / 4", di));
            list.Add(string.Format("                 = {0:f4} mm^2", Ae));
            list.Add(string.Format(""));
            list.Add(string.Format("Hardness IRHD ( Shore-A) = {0} ± 5", IRHD));

            list.Add(string.Format("Value of Constant [k1] = {0} ", k1));
            list.Add(string.Format("Value of Constant [k2] = {0} ", k2));

            list.Add(string.Format("Moment due to resistance to Rotation = Med = di^3 x ( k1 X θp + k2 X θv)"));
            list.Add(string.Format("                                           = {0}^3 x ( {1} X {2} + {3} X {4})", di, k1, theta_p, k2, theta_v));
            list.Add(string.Format("                                           = {0:f3} N-mm", Med));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Normal) = MRdn = 0.2 X ( di / 2) X Hlng_n*1000"));
            list.Add(string.Format("                                                    = 0.2 X ( {0} / 2) X {1}*1000", di, Hlng_n));
            list.Add(string.Format("                                                    = {0:f3} N-mm", MRdn));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Transverse Seismic) = MRst = 0.2 X (di / 2) X Hlts*1000"));
            list.Add(string.Format("                                                                = 0.2 X ({0} / 2) X {1}*1000", di, Hlts));
            list.Add(string.Format("                                                                = {0:f3} N-mm", MRst));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Longitudinal Seismic) = MRsl =  0.2  X ( di / 2) X Hlng_s*1000"));
            list.Add(string.Format("                                                                  =  0.2  X ( {0} / 2) X {1}*1000", di, Hlng_s));
            list.Add(string.Format("                                                                  =  {0:f3} N-mm", MRsl));
            list.Add(string.Format(""));

            double MRds = Math.Max(MRst, MRsl);
            list.Add(string.Format("Moment due to frictional resistance (Seismic) = MRds = Max ( MRst OR MRsl )"));
            list.Add(string.Format("                                                     = Max ( {0:f3} OR {1:f3} )", MRst, MRsl));
            list.Add(string.Format("                                                     = {0:f3} N-mm", MRds));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.2 : SADDLE "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia of PTFE [dp] = {0} mm", dp));
            list.Add(string.Format("Contact with piston [w] = {0} mm", w));
            list.Add(string.Format("Thickness of Saddle plate [hm] = {0} mm", hm));
            list.Add(string.Format("Eff. Plan area of PTFE [An] = (π x dp^2) / 4 =  {0} mm^2", An));
            list.Add(string.Format("Height of SS on saddle [hts] = {0} mm", hts));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.3 : POT CYLINDER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia / sq of base plate [Do] = {0} mm", Do_));
            list.Add(string.Format("Thkness of base plate [kb] = {0} mm", kb));
            list.Add(string.Format("Outer dia of cylinder [do] = {0} mm", do_));
            list.Add(string.Format("Depth of cyfinder [hc] = {0} mm", hc));
            list.Add(string.Format("Effective dia at the bottom [deb] = di + 2 x 2 x kb"));
            list.Add(string.Format("                                  = {0} + 2 x 2 x {1}", di, kb));
            list.Add(string.Format("                                  = {0:f2} mm", deb));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of cylinder wall [bp] = (do-di)/2 = ({0} - {1})/2 = {2} mm", do_, di, bp));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically simifar to deb = deb_avibl = {0} mm", deb_avibl));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.4 : TOP ASSEMBLY "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of top piate [a] = {0} mm", a));
            list.Add(string.Format("Width of top plate [b] = {0} mm", b));
            list.Add(string.Format("Thickness of SS Plate [ks] = {0} mm", ks));
            list.Add(string.Format("Thickness of top plate [kt] = {0} mm", kt));
            list.Add(string.Format("Length of SS plate [as] = {0} mm", as_));
            list.Add(string.Format("Width of SS plate [bs] = {0} mm", bs));
            list.Add(string.Format("Thickness of guide [ku] = {0} mm", ku));
            list.Add(string.Format("Effective Length of guide [Lu] = {0} mm", Lu));
            list.Add(string.Format("Effective dia of the dispersed area at the top structure = det "));
            list.Add(string.Format(""));

            list.Add(string.Format("   det = Minimum Value of ((dp+2 x 2x(kt+ks)) & b)"));

            double val1 = 0;
            val1 = (dp + 2 * 2 * (kt + ks));



            list.Add(string.Format("       = Minimum Value of (({0}+2 x 2x({1}+{2})) & {3})", dp, kt, ks, b));
            list.Add(string.Format("       = Minimum Value of ({0:f2} & {1:f2})", val1, b));
            list.Add(string.Format(""));
            list.Add(string.Format("       = {0:f3} mm", det));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically similar to [det_avibl] = {0} mm", det_avibl));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.5 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("BOTTOM"));
            list.Add(string.Format("------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bolt size [Mj] = {0} mm", bottom_Mj));
            list.Add(string.Format("Root area of bolt [Ab] = {0} mm^2", bottom_Ab));
            list.Add(string.Format("No. off boits [n] = {0} ", bottom_n));
            list.Add(string.Format("Class of bolt [bc] = {0} ", bottom_bc));
            list.Add(string.Format("Dia of Sleeve [D] = {0} mm", bottom_D));
            list.Add(string.Format("Length of sleeve [L] = {0} mm", bottom_L));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TOP"));
            list.Add(string.Format("---"));
            list.Add(string.Format(""));

            list.Add(string.Format(" Bolt Size [Mj] = {0} mm", top_Mj));
            list.Add(string.Format(" Root area of bolt [Ab] = {0} mm^2", top_Ab));
            list.Add(string.Format(" No. of bolts [n] = {0} ", top_n));
            list.Add(string.Format(" Class of bolt [bc] = {0} ", top_bc));






            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.6 : Ht of application of horizontal Force"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ht of application of horiz. Force = ha = kb + he + w/2"));
            list.Add(string.Format("                                       = {0} + {1} + {2}/2", kb, he, w));
            list.Add(string.Format("                                       = {0} mm", ha));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.7 : Overall height of bearing"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Overall height of bearing = h = {0} mm", h));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3 : DESIGN CHECK"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at bottom of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Concrete [fck] = M{0} ", fck));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Normal) = σ_cb_all = 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                            = 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                            = {0:f3} MPa", conc_ads_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Normal) = σ_cbc_all = 0.33 x fck"));
            list.Add(string.Format("                                              = 0.33 x {0}", fck));
            list.Add(string.Format("                                              = {0:f3} MPa ", conc_abs_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Seismic) = σ_cb_all = 1.25 x 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                             = 1.25 x 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                             = {0:f3} MPa", conc_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = σ_cbc_all = 1.25 x 0.33 x fck"));
            list.Add(string.Format("                                               = 1.25 x 0.33 x {0}", fck));
            list.Add(string.Format("                                               = {0:f3} MPa", conc_abs_seismic));
            list.Add(string.Format(""));



            list.Add(string.Format("Bottom direct Seismic = σ_cb = Nmax  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                             = {0}  x 1000/(π x {1:f3}^2 /4)", Nmax, deb));
            list.Add(string.Format("                             = {0:f3} MPa", conc_sigma_cb_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom direct Normal = σ_cb = Nnorm  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                            = {0}  x 1000/(π x {1}^2 /4)", Nnorm, deb));
            list.Add(string.Format("                            = {0:f3} MPa", conc_sigma_cb_norm));
            list.Add(string.Format(""));

            list.Add(string.Format("Bottom bending Normal = σ_cbc =  Hlngn × 1000 x ha / π x deb^3 /32", conc_sigma_cbc_norm));
            list.Add(string.Format("                                 + (Med + MRdn) / ((π x deb^3 / 32)", conc_sigma_cbc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0} × 1000 x {1} / π x {2:f3}^3 /32", Hlng_n, ha, deb));
            list.Add(string.Format("                                 + ({0:f3} + {1:f3}) / ((π x {2:f3}^3 / 32)", Med, MRdn, deb));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0:f3}  MPa", conc_sigma_cbc_norm));

            list.Add(string.Format(""));
            //list.Add(string.Format("Bottom bending (Seismic) = σ_cbc   Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));
            list.Add(string.Format(""));

            double sigma_cbc1 = Hlng_s * 1000 * ha / (Math.PI * deb * deb * deb / 32.0) + (Med + MRds) / (Math.PI * deb * deb * deb / 32.0);
            double sigma_cbc2 = Hlts * 1000 * ha / (Math.PI * deb * deb * deb / 32.0);




            list.Add(string.Format("  σ_cbc1 =  Hlng_s x 1000 x ha / (π x deb^3 / 32) + (Med  + MRds) / (π x deb^3 / 32)"));
            list.Add(string.Format("         =  {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32) + ({3:f3}  + {4:f3}) / (π x {2:f3}^3 / 32)", Hlng_s, ha, deb, Med, MRds));
            list.Add(string.Format("         =  {0:f3} MPa", sigma_cbc1));
            list.Add(string.Format(""));
            list.Add(string.Format("  σ_cbc2 = Hlts x 1000 x ha / (π x deb^3 / 32)"));
            list.Add(string.Format("         = {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32)", Hlts, ha, deb));
            list.Add(string.Format("         = {0:f3} MPa", sigma_cbc2));
            list.Add(string.Format(""));



            conc_sigma_cbc_seismic = Math.Max(sigma_cbc1, sigma_cbc2);

            list.Add(string.Format("Bottom bending( Seismic) = σ_cbc =  Maximum Value of  ( {0:f3} OR {1:f3} )", sigma_cbc1, sigma_cbc2));
            list.Add(string.Format("                                 =  {0:f3} MPa ", conc_sigma_cbc_seismic));

            list.Add(string.Format(""));
            //list.Add(string.Format(" Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));

            //list.Add(string.Format("OR, Hlat_s x 1000 x ha / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));


            lamda_b_norm = (conc_sigma_cb_norm / conc_ads_norm) + (conc_sigma_cbc_norm / conc_abs_norm);

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_norm, conc_ads_norm, conc_sigma_cbc_norm, conc_abs_norm));
            list.Add(string.Format(""));

            if (lamda_b_norm <= 1)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Normal), Hence OK", lamda_b_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Normal), Hence NOT OK", lamda_b_norm));

            list.Add(string.Format(""));


            lamda_b_seismic = (conc_sigma_cb_seismic / conc_ads_seismic) + (conc_sigma_cbc_seismic / conc_abs_seismic);


            //list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all =   {0:f3} MPa (Seismic)", lamda_b_seismic));

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_seismic, conc_ads_seismic, conc_sigma_cbc_seismic, conc_abs_seismic));
            list.Add(string.Format(""));

            if (lamda_b_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_b_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_b_seismic));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at Top of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Steel = fy = {0} MPa", steel_fy));

            steel_ads_norm = 0.66 * steel_fy;

            list.Add(string.Format("Allowable  direct stress (Normal) = σ_ct_all =  0.66 * fy "));
            list.Add(string.Format("                                             =  0.66 * {0}", steel_fy));
            list.Add(string.Format("                                             =  {0:f3} MPa", steel_ads_norm));
            list.Add(string.Format(""));


            steel_abs_norm = 0.75 * steel_fy;

            list.Add(string.Format("Allowable  bending stress = σ_ctc_all =  0.75 * fy"));
            list.Add(string.Format("                                     =  0.75 * {0:f3} ", steel_fy));
            list.Add(string.Format("                                     =  {0:f3} MPa", steel_abs_norm));
            list.Add(string.Format(""));

            steel_ads_seismic = steel_ads_norm;
            steel_abs_seismic = steel_abs_norm;
            list.Add(string.Format("Allowable direct stress (Seismic) = Allowable direct stress( Normal ) = {0:f3} MPa", steel_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = Allowable bending stress( Normal ) = {0:f3} MPa", steel_abs_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_seismic = Nmax * 1000 / (Math.PI * det * det / 4);
            list.Add(string.Format("Top direct  σ_ct (Seismic)= Nmax x 1000/(π x det^2/4)"));
            list.Add(string.Format("                          = {0} x 1000/(π x {1:f3}^2/4)", Nmax, det));
            list.Add(string.Format("                          = {0:f3} MPa", steel_sigma_ct_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_norm = (Nnorm * 1000) / (Math.PI * det * det / 4.0);
            list.Add(string.Format("Top direct  σ_ct (Normal)= Nnorm x 1000/(π x det^2/4)", steel_sigma_ct_norm));
            list.Add(string.Format("                         = {0} x 1000/(π x {1:f2}^2/4)", Nnorm, det));
            list.Add(string.Format("                         = {0:f3} MPa", steel_sigma_ct_norm));
            list.Add(string.Format(""));

            steel_sigma_ctc_norm = Hlng_n * 1000 * (h - ha) / (Math.PI * det * det * det / 32.0) + (Med + MRdn) / (Math.PI * det * det * det / 32.0);

            list.Add(string.Format("Top bending normal = σ_ctc =  Hlngn x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("                              + (Med + MRdn) / (π x det^3 / 32)", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Top bending normal = σ_ctc =  {0:f3} x 1000 x ( {1} - {2} ) /(π x {3:f3}^3 / 32)", Hlng_n, h, ha, det));
            list.Add(string.Format("                              + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32)", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("                           =  {0:f3} MPa", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_ctc1 = Hlng_s * 1000 * (h - ha) / (Math.PI * det * det * det / 32) + (Med + MRdn) / (Math.PI * det * det * det / 32);
            list.Add(string.Format(" σ_ctc1 =  Hlng_s x 1000 x ( h-ha ) /(π x det^3 /32) "));
            list.Add(string.Format("           + (Med + MRdn) / (π x det^3 / 32)"));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} x 1000 x ( {1}-{2} ) /(π x {3:f3}^3 /32) ", Hlng_s, h, ha, det));
            list.Add(string.Format("           + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32))", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} MPa", sigma_ctc1));
            list.Add(string.Format(""));

            double sigma_ctc2 = Hlts * 1000 * (h - ha) / (Math.PI * det * det * det / 32);

            list.Add(string.Format(" σ_ctc2 = Hlts  x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("        = {0:f3}  x 1000 x ( {1}-{2} ) /(π x {2:f3}^3 / 32)", Hlts, h, ha, det));
            list.Add(string.Format("        = {0:f3} MPa", steel_sigma_ctc_seismic));

            steel_sigma_ctc_seismic = Math.Max(sigma_ctc1, sigma_ctc2);

            list.Add(string.Format(""));

            list.Add(string.Format("Top bending seismic = σ_ctc = Maximum Value of (σ_ctc1  OR  σ_ctc2)"));
            list.Add(string.Format("                            = Maximum Value of ({0:f3}  OR  {1:f3})", sigma_ctc1, sigma_ctc2));
            list.Add(string.Format("                            = {0:f3} MPa", steel_sigma_ctc_seismic));
            list.Add(string.Format(""));
            //list.Add(string.Format("OR"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Hlat_s  x 1000 x ( h-ha ) /(π x det^3 / 32)    =   {0:f3} MPa", steel_sigma_ctc_seismic));
            lamda_t_norm = (steel_sigma_ct_norm / steel_ads_norm) + (steel_sigma_ctc_norm / steel_abs_norm);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_norm, steel_ads_norm, steel_sigma_ctc_norm, steel_abs_norm));

            if (lamda_t_norm <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa  <= 1 (Normal), Hence OK", lamda_t_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa  > 1 (Normal), Hence NOT OK", lamda_t_norm));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            lamda_t_seismic = (steel_sigma_ct_seismic / steel_ads_seismic) + (steel_sigma_ctc_seismic / steel_abs_seismic);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all", lamda_t_seismic));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_seismic, steel_ads_seismic, steel_sigma_ctc_seismic, steel_abs_seismic));

            if (lamda_t_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_t_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_t_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.1 : PTFE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cp = Nnorm * 1000.0 / An;
            list.Add(string.Format("(a)  Contact stress = σ_cp = Nnorm x 1000 / An "));
            list.Add(string.Format("                           = {0} x 1000 / {1:f3}", Nnorm, An));
            if (sigma_cp < 40)
                list.Add(string.Format("                           = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cp));
            else
                list.Add(string.Format("                           = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cp));


            list.Add(string.Format(""));


            sigma_cpc = sigma_cp + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * dp * dp * dp / 32));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_cp + (Med + (Max of (MRdn OR MRds))/(π x dp^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_cp, Med, MRdn, MRds, dp));
            if (sigma_cpc < 45)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >  45 N/mm^2,    Hence NOT OK", sigma_cpc));

            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.2 : ELASTOMERIC DISC"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            sigma_ce = Nmax * 1000 / Ae;
            list.Add(string.Format("(a)  Compressive stress = σ_ce = Nmax x 1000 / Ae"));
            list.Add(string.Format("                               = {0:f3} x 1000 / {1:f3}", Nmax, Ae));
            if (sigma_ce <= 35)
                list.Add(string.Format("                               = {0:f3} N/mm^2  <=  35 N/mm^2,    Hence OK", sigma_ce));
            else
                list.Add(string.Format("                               = {0:f3} N/mm^2  >  35 N/mm^2,    Hence NOT OK", sigma_ce));


            list.Add(string.Format(""));

            sigma_cpc = sigma_ce + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * di * di * di / 32));

            //list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc  = σ_cp + (Med + (Max of (MRdn))/(π x di^3/32) = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cp));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_ce + (Med + (Max of (MRdn OR MRds))/(π x di^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_ce, Med, MRdn, MRds, di));

            if (sigma_cpc < 40)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cpc));


            eff = 0.5 * di * theta / he;
            list.Add(string.Format(""));
            list.Add(string.Format("(c)  Strain at the perimeter due to rotation θ rad. eff = 0.5 x di x θ / he"));
            list.Add(string.Format("                                                        = 0.5 x {0} x θ / {1}", di, theta, he));
            if (eff < 0.15)
                list.Add(string.Format("                                                        = {0:f3} < 0.15, Hence OK", eff));
            else
                list.Add(string.Format("                                                        = {0:f3} >= 0.15, Hence NOT OK", eff));


            clrns = hc - he - w - theta * (di / 2);


            list.Add(string.Format(""));
            list.Add(string.Format("(d)  Clearance between cylinder top and piston bottom = hc-he-w-θ x (di/2)"));
            list.Add(string.Format("                                                      = {0} - {1} - {2} - {3} x ({4}/2)", hc, he, w, theta, di));
            if (clrns > 5)
                list.Add(string.Format("                                                      = {0:f3} > 5 ,   Hence OK", clrns));
            else
                list.Add(string.Format("                                                      = {0:f3} <= 5 ,   Hence NOT OK", clrns));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.3 : POT CYLINDER"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Hoop's (tensile) stress  in the cross section, due to"));
            list.Add(string.Format(""));


            sigma_t1 = di * he * sigma_ce / (2 * bp * hc);
            list.Add(string.Format("(a)  Fluid pressure σ_t1 = di  x he x σ_ce / ( 2 x bp x hc)"));
            list.Add(string.Format("                         = {0}  x {1} x {2:f3} / ( 2 x {3} x {4})", di, he, sigma_ce, bp, hc));
            list.Add(string.Format("                         = {0:f3} MPa", sigma_t1));
            list.Add(string.Format(""));

            sigma_t2 = (H * 1000) / (2.0 * bp * hc);
            list.Add(string.Format("(b)  Horizontal force σ_t2 = H x 1000 / ( 2 x bp x hc )"));
            list.Add(string.Format("                           = {0} x 1000 / ( 2 x {1} x {2} )", H, bp, hc));
            list.Add(string.Format("                           = {0:f3} MPa", sigma_t2));
            list.Add(string.Format(""));

            sigma_t = sigma_t1 + sigma_t2;
            list.Add(string.Format("(c) Total stress = σ_t = σ_t1 + σ_t2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3}", sigma_t1, sigma_t2));
            list.Add(string.Format("                       = {0:f3} MPa", sigma_t));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.4 : POT INTERFACE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Shear stress in cylinder & base interface considering    1 mm orange slice, due to"));
            list.Add(string.Format(""));

            sigma_q1 = he * sigma_ce / bp;
            list.Add(string.Format("(a)  Fluid pressure, σ_q1 = he x σ_ce/bp "));
            list.Add(string.Format("                          = {0} x {1:f3}/{2} ", he, sigma_ce, bp));
            list.Add(string.Format("                          = {0:f3} MPa", sigma_q1));
            list.Add(string.Format(""));





            list.Add(string.Format("(b)  Assuming Parabolic distribution factor = 1.5, "));
            list.Add(string.Format(""));
            sigma_q2 = (1.5 * H * 1000 / di) / bp;
            list.Add(string.Format("      Horizontal Force = σ_q2 = (1.5 x H x 1000/di) / bp"));
            list.Add(string.Format("                              = (1.5 x {0} x 1000/{1}) / {2}", H, di, bp));
            list.Add(string.Format("                              = {0:f3} MPa", sigma_q2));
            list.Add(string.Format(""));
            sigma_q = sigma_q1 + sigma_q2;
            list.Add(string.Format("(c)  Total stress = σ_q = σ_q1 + σ_q2"));
            list.Add(string.Format("                        = {0:f3}  + {1:f3}", sigma_q1, sigma_q2));
            if (sigma_q < 153)
                list.Add(string.Format("                        = {0:f3} MPa  < 153 MPa Hence OK", sigma_q));
            else
                list.Add(string.Format("                        = {0:f3} MPa  > 153 MPa Hence NOT OK", sigma_q));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Beriding stress in the interface considering  1 mm orange slice"));
            list.Add(string.Format(""));

            sigma_bt1 = ((sigma_ce * he * he / 2) * 6.0) / (bp * bp);

            list.Add(string.Format("(a) Fluid  pressure = σ_bt1 = (σ_ce  x he^2 /2) x 6 / bp^2", sigma_bt1));
            list.Add(string.Format("                            = ({0:f3}  x {1}^2 /2) x 6 / {2}^2 ", sigma_ce, he, bp));
            list.Add(string.Format("                            = {0:f3} MPa", sigma_bt1));

            sigma_bt2 = ((1.5 * 1000 * H / di) * (ha - kb) * 6) / (bp * bp);
            list.Add(string.Format(""));

            list.Add(string.Format("(b) Horizontal force = σ_bt2 = ( 1.5 x 1000 x H/di) x ( ha-kb) x 6 / bp^2", sigma_bt2));
            list.Add(string.Format("                             = ( 1.5 x 1000 x {0}/{1}) x ( {2}-{3}) x 6 / {4}^2", H, di, ha, kb, bp));
            list.Add(string.Format("                             = {0:f3} MPa", sigma_bt2));

            sigma_bt = sigma_bt1 + sigma_bt2;
            list.Add(string.Format(""));

            list.Add(string.Format("(c) Totai Stress  σ_bt = σ_bt1  + σ_bt2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3} ", sigma_bt1, sigma_bt2));
            if (sigma_bt < 224.4)
                list.Add(string.Format("                       = {0:f3} MPa  < 224.4  MPa,   Hence OK", sigma_bt));
            else
                list.Add(string.Format("                       = {0:f3} MPa  >= 224.4  MPa,   Hence NOT OK", sigma_bt));

            list.Add(string.Format(""));
            sigma_e = Math.Sqrt((sigma_bt * sigma_bt + 3 * sigma_q * sigma_q));
            list.Add(string.Format("(iii) Combined stress  σ_e  = √(σ_bt^2 + 3 × σ_q^2) "));
            list.Add(string.Format("                            = √({0:f3}^2 + 3 × {1:f3}^2)", sigma_bt, sigma_q));

            if (sigma_e < 306.0)
                list.Add(string.Format("                            = {0:f3} MPa < 306 MPa   Hence OK", sigma_e));
            else
                list.Add(string.Format("                            = {0:f3} MPa > 306 MPa   Hence NOT OK", sigma_e));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.5 : Eff.cont.width at piston-cylinder interface"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            double sigma_p = 254.84;
            list.Add(string.Format("σ_p = {0:f3} MPa", sigma_p));

            we = (H * 1000 * 1.3) / (di * sigma_p);
            list.Add(string.Format("Eff.cont.width at piston-cylinder interface = we = H x 1000 x 1.3 / (di x σ_e)"));
            list.Add(string.Format("                                                 = {0} x 1000 x 1.3 / ({1} x {2:f3})", H, di, sigma_e));
            if (we < w)
                list.Add(string.Format("                                                 = {0:f4} mm  <  {1} (w)     Hence OK", we, w));
            else
                list.Add(string.Format("                                                 = {0:f4} mm  >=   {1} (w)     Hence NOT OK", we, w));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.6 : GUIDE "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            sigma_qu = (H * 1000) / (Lu * ku);
            list.Add(string.Format("(i)  Shear stress  σ_qu = H x 1000 / ( Lu x ku ) "));
            list.Add(string.Format("                        = {0} x 1000 / ( {1} x {2} )", H, Lu, ku));
            if (sigma_qu < 153)
                list.Add(string.Format("                        = {0:f4} MPa   <   153 MPa, Hence OK", sigma_qu));
            else
                list.Add(string.Format("                        = {0:f4} MPa   >=   153 MPa, Hence NOT OK", sigma_qu));



            list.Add(string.Format(""));





            sigma_bu = H * 1000 * 6 * (hts / 2 + 8) / (Lu * ku * ku);

            list.Add(string.Format("(ii)  Bending Stress = σ_bu = H x 1000 x 6 x ( hts / 2+8) / ( Lu x ku^2 )"));
            list.Add(string.Format("                            = {0} x 1000 x 6 x ( {1} / 2+8) / ( {2} x {3}^2 )", H, hts, Lu, ku));

            if (sigma_bu < 224.4)
                list.Add(string.Format("                            = {0:f4} MPa   <   224.4 MPa, Hence OK", sigma_bu));
            else
                list.Add(string.Format("                            = {0:f4} MPa   >=   224.4 MPa, Hence NOT OK", sigma_bu));

            list.Add(string.Format(""));

            sigma_eu = Math.Sqrt(sigma_bu * sigma_bu + 3 * sigma_qu * sigma_qu);

            list.Add(string.Format("(iii) Combined stress = σ_eu = √(σ_bu^2 + 3 x σ_qu^2)"));
            list.Add(string.Format("(                            = √({0:f3}^2 + 3 x {1:f3}^2)", sigma_bu, sigma_qu));
            if (sigma_eu < 306.0)
                list.Add(string.Format("(                            = {0:f4} MPa   <   306 MPa ", sigma_eu));
            else
                list.Add(string.Format("(                            = {0:f4} MPa   >=   306 MPa ", sigma_eu));

            sigma_du = H * 1000 / (hts * (Lu - 10));
            list.Add(string.Format(""));

            list.Add(string.Format("(iv)  Direct stress on SS = σ_du = H × 1000/(hts x (Lu-10)) "));
            list.Add(string.Format("                                 = {0} × 1000/({1} x ({2}-10)) ", H, hts, Lu));
            if (sigma_du < 200)
                list.Add(string.Format("                                 = {0:f4} MPa < 200 MPa ", sigma_du));
            else
                list.Add(string.Format("                                 = {0:f4} MPa >= 200 MPa ", sigma_du));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.7 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(a) Anchor Screws"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            bottom_Hperm = ((bottom_n * bottom_Ab * bottom_sigma_bq_perm) / 1000) + (Nmin * bottom_mu);

            //list.Add(string.Format("Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)] > H = Maximum Horizontal force on bearing."));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3}", bottom_mu));
            list.Add(string.Format("σ_bq_perm  = Permissible  shear stress  in  bolt = {0:f3}  MPa", bottom_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maximum Horizontal force on bearing"));
            list.Add(string.Format(""));
            //list.Add(string.Format("The total resistive force = Hperm = {0:f3} kN > H , OK ", bottom_Hperm));


            list.Add(string.Format("The total resistive force = Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)]"));
            list.Add(string.Format("                                  = [({0} x {1:f3} x {2:f3}) / 1000)+({3:f3} x {4:f3})]", bottom_n, bottom_Ab, bottom_sigma_bq_perm, Nmin, bottom_mu));

            if (bottom_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H),   Hence OK", bottom_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H),   Hence NOT OK", bottom_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format("Top :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Anchor Screws"));
            //list.Add(string.Format("-------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Bottom :"));
            list.Add(string.Format(""));

            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            top_Hperm = ((top_n * top_Ab * top_sigma_bq_perm) / 1000);
            //list.Add(string.Format("Hperm = ((n x Ab x σ_bq_perm) / 1000) > H, Maximum Horizontal force on bearing."));
            list.Add(string.Format(""));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3} ", top_mu));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_bq,perm  = Permissible shear stress in bolt = {0:f3} MPa", top_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maxm. Horizontal force on bearing"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force = Hperm = (n x Ab x σ_bq_perm) / 1000", top_Hperm));
            list.Add(string.Format("                                  = ({0} x {1:f3} x {2:f3}) / 1000 ", top_n, top_Ab, top_sigma_bq_perm));
            if (top_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H) , OK", top_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H)  , NOT OK", top_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b)  Anchor Sleeves"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Screws."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cav = (bottom_Ab * bottom_sigma_bq_perm) / (bottom_L * bottom_D);
            list.Add(string.Format("( i ) Avg. bearing stress in conc. σ_cav. = (Ab x σ_bq_perm) / (L x D ) "));
            list.Add(string.Format("                                          = ({0:f3} x {1:f3}) / ({2:f3} x {3:f3} )", bottom_Ab, bottom_sigma_bq_perm, bottom_L, bottom_D));
            list.Add(string.Format("                                          = {0:f3} MPa", sigma_cav));
            list.Add(string.Format(""));


            double sigma_ctr = 2 * sigma_cav;

            list.Add(string.Format("( ii) Considering traingular distribution, bending stress in conc = σ_ctr = 2 x σ_cav"));
            list.Add(string.Format("                                                                          = 2 x {0:f3}", sigma_cav));
            list.Add(string.Format("                                                                          = {0:f3} MPa", sigma_ctr));
            list.Add("");
            double sigma_cpk = 1.5 * sigma_ctr;
            list.Add(string.Format("(iii) Peak stress = σ_cpk = 1.5 x σ_ctr"));
            list.Add(string.Format("                          = 1.5 x {0:f3}", sigma_ctr));
            if (sigma_cpk < 18.56)
                list.Add(string.Format("                          = {0:f3} MPa  < 18.56 MPa, Hence OK", sigma_cpk));
            else
                list.Add(string.Format("                          = {0:f3} MPa  >= 18.56 MPa, Hence NOT OK", sigma_cpk));
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");

            #region Sample


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CONTRACTOR : M/s. SEW INFRASTRUCTURE                       LTD.                                   2/25/201316:40"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                   DESIGN       OF Steflomet POT/PTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("1     REFERENCE:"));
            //list.Add(string.Format("       Drawing                                                                  :3..P14VMT Sp¢     Rev-0      REFERENCE :                                         IRC:83(Part-W)"));
            //list.Add(string.Format("       Project :For four laning of krishnagar Baharampour section of NH-34 from km 115.000 to km 193.000 West Barigal."));
            //list.Add(string.Format("       For ROB at Chanage;  133+432+501                      Quantity -4+4 nos.                 Location: A1&A4                                    Type - Transverselý Guided."));

            //list.Add(string.Format("       Normal Vertical Load, Nnorm                              : 1,334.00                   kN         Trans. Horz. Force  Hlts (Seismic)           0.00                 kN"));




            //list.Add(string.Format("       Dead load,  Nmin                                         :   332.00                      kN         Long Horz Force( Normal ) Hlng_n                  65.00                kN"));
            //list.Add(string.Format("       Rotation due to Dead Load. θp                            :     0.0065                     rad          Long Harz. Force, (Seismic) Hlng_s          174.00              kN"));
            //list.Add(string.Format("       Rotation due to Live Load, θy                            :     0.0015                      rad         Design horz. Force, H(For bearing            174.00              kN"));
            //list.Add(string.Format("       Rotation, e                                              : 0.010                        rad         components only)"));
            //list.Add(string.Format("       Movement, elong(±)                                               O                              mm        Movement, etrans.(±)                              10.0                 mm"));


            //list.Add(string.Format("             3      BEARINGPARAMETERS"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("      1.   Steel components to conform IS:2062(MS), IS: 1030 Gr.340-570 W(CS) Therefore, Yield Stress, fy 340 MPa"));

            //list.Add(string.Format("            Dia of Elastomeric pad, di                               :  240                         mm         Thickness of the Pad ,he                           16                   mm"));
            //list.Add(string.Format("            Area of Pad. Ae  = π x di2 / 4                       .   : 4.52E+04                 mm2       Hardness  IRHD ( Shore-A)                      50 ± 5"));
            //list.Add(string.Format("            Value of Constant, k1                                     :  2.2                                          Value of Constant, k2                               101"));
            //list.Add(string.Format("            Moment due to resistance to Rotation.                                        Med = di' X ( k, X Op + k2 X 0,)                                         2.29E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Normal)..                             MRdn = 0.2 X ( di / 2) X He                                             1.56E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Transverse Seismic),               MRst  = 0.2 X ( d  / 2) X Hlts                                .             0.00E+00  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Longitudinal Seismic),            MRsl  = 0.2  X ( di / 2) X H  .                                             4.18E+06  N-mm"));
            //list.Add(string.Format("       3.  SADDLE"));
            //list.Add(string.Format("            Dia of PTFE(dp)                                             :  240                          mm        Contact with piston,w                              6                       mm"));
            //list.Add(string.Format("            Thickness of Saddle plate,  hm                        :  12                            mm        Eff. Plan area of PTFE,An                       45238.93         mm®"));
            //list.Add(string.Format("            Height of SS on saddle,hts                               :  12                            mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("       4.  POT CYLINDER"));
            //list.Add(string.Format("            Dia / sq of base plate,  Do                                :  310                          mm        Thkness of base plate,kb                          15                     mm"));
            //list.Add(string.Format("            Outer dia of cylinder, do                                  :  294                          mm        Depth of cyfinder,hc                                 32                   mm"));
            //list.Add(string.Format("            Effective dia at the bottom, deb                     .  300                          mm        Width of cylinder wall                                27                    mm"));
            //list.Add(string.Format("            di + 2 x 2 x kb                                                                                                   bp=(do-di)/2"));
            //list.Add(string.Format("            Available diameter (deb,avibl) at the substructure, geometrically    simifar to, deb =                                                          650                   mm"));
            //list.Add(string.Format("       5.   TOP ASSEMBLY"));
            //list.Add(string.Format("             Length of top piate, a.                                   :  400                           mm       Width of top plate,b                                  500                  mm"));
            //list.Add(string.Format("             Thk.ness of SS Plate(ks)                                 : 3                               mm        Thickness of top plate, kt                        14                     mm"));
            //list.Add(string.Format("             Length of SS plate, as                                     :  320                          mm        Width of SS plate,  bs                                300                  mm"));
            //list.Add(string.Format("             Thk. of guide, ku                                            :  20                            mm        Effective Length of guide,Lu                     240                 mm"));
            //list.Add(string.Format("             Effective dia of the dispersed  area at the top structure, det  = Min((dp+2 x 2x(kt+ks)) & b]                                           308                  mm"));
            //list.Add(string.Format("             Available diameter (det,avibl)  at the substructure, geometrically similar to,  det =                                                           650                   mm"));
            //list.Add(string.Format("       6.   ANCHOR"));
            //list.Add(string.Format("             Bottom:"));
            //list.Add(string.Format("             Bolt size( IS:1363) Mj                              :        16                            mm         Root area of bolt Ab                                167                   mm2"));
            //list.Add(string.Format("             No. off boits(n)                                        :        4                                            Class of bolt, bc                                10.9"));
            //list.Add(string.Format("             Dia of Sleeve, D                                       :        40                              mm       Length of sleeve , L                                 170                   mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             TOP :"));
            //list.Add(string.Format("             Size (1S:1363) Mj                                     :        20                                          Root area of bolt,Ab                                 272                  mm2"));
            //list.Add(string.Format("             No. of bolts(n)                                         :        4                                             Class of bolt(tS:1367)                               10.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        7.   Ht of applicat5on of horiz. Force(ha)= kb + he + w/2                                                                                                      34                    mm"));
            //list.Add(string.Format("        8.   Overall height of bearing (h) mm                                                                                                                                    88                     mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(" 4     DES1GN CHECK :"));
            //list.Add(string.Format("        Structure at bottom of Bearing :                              Concrete                                Grade of Concrete    , fck                          M45"));
            //list.Add(string.Format("        Allowable direct stress(Normal)                              0.25 fck X ((debavi) / (deb))                                                                    22.50              MPa"));
            //list.Add(string.Format("        Allowable bending stress(Normal)                           0.33 fck                                                                                                  14.85               MPa"));
            //list.Add(string.Format("        Allowable direct stress ( Seismic)                             1.25 x 0.25 fck X ((debavl) / (deb))                                                        28.13               MPa"));
            //list.Add(string.Format("        Allowabie bending stress ( Seismic)                          1.25 x 0.33 fck.                                                                                      18.56              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Seismic)                                   N max  x 1000!(x(deb) /4) MPa =                                                           19.81              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Normal)                                   N norm. x 1000/(x(deb)2/4) MPa =                                                        18.87               MPa"));
            //list.Add(string.Format("        Bottom bending( Normal) σ_cbc = Hn × 1000 x ha / x.(deb)3 /32 + (Med + Mw) / (u deb' / 32)  MPa =                         -        2.29                 MPa"));
            //list.Add(string.Format("        Bottom bending( Seismic) σ_cbc,Max, Hing,s   x 1000 x ha / (x deb' / 32)   + (Ma  + Man) / (n deb3 / 32) MPa ="));
            //list.Add(string.Format("        OR, Hlat,s x 1000 x ha / (x deb' / 32)   MPa =                                                                                                                                      4.67  MPa"));
            //list.Add(string.Format("        λb=acb/acb,all      +acbc /acbc,all=                                                0.993                 MPa<=1(Normal)                                     Hence  OK"));
            //list.Add(string.Format("        λb = acb / ocb,all +  acbc / acbc,all   =                                          0.956                 MPa<=1( Seism c)                                    Hence  OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                       CONTRACTOR             :    M/s. SEW             INFRASTRUCTURE                      LTD.                                  2/25/201316:40"));
            //list.Add(string.Format("                DESIGN OF Steflomet POTIPTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format("   Structure at Top of Bearing :                                  STEEL to IS :  2062               fy.=                                                                         230  MPa"));
            //list.Add(string.Format("   Allowable  direct stress( Normal )=                         0.66 * fy                                                                                                151.80            MPa"));
            //list.Add(string.Format("   Arlowable bending stress (Normal)=                     0.75 * fy                                                                                               172.5               MPa"));
            //list.Add(string.Format("   Allowable direct stress( Seismic )= Allowable direct stress( Normal )                                                           151.80             MPa"));
            //list.Add(string.Format("                     .                     Allowable bending  stress ( Seismic)   =                    Allowable bending stress( Normal )                                                        172.50"));
            //list.Add(string.Format("   Top directµct(Seismic)= Nmaxx1000/(udet2/4)N/mm2;                                                                                                        18.79              MPa"));
            //list.Add(string.Format("   Topdirect,act(Normal)=  N x1000 / (udet2/4)N/mm2  =                                                                                                       17.90              MPa"));
            //list.Add(string.Format("   Top bending normal,acic =  Hn x 1000 x ( h-ha ) /(x.det3 / 32) + (M,ø + Mpø) / (x det® / 32) MPa =                                 2.57                MPa"));
            //list.Add(string.Format("   Top bending seismic,actc =Max, Hlng,s   x 1000 x ( h-ha ) /(n.det' / 32) + (Me + Maø) / (2 det' / 32) MPa ="));
            //list.Add(string.Format("   OR, Hlat,s  x 1000 x ( h-ha ) /(x.det3 / 32) MPa =                                                                                                                              3.28  MPa"));
            //list.Add(string.Format("   at = act / act,ali + actc / actc.all  =                                                              0.133  MPa<=1(Normal)                                    Hence OK"));
            //list.Add(string.Format("   At = act / oct,ali + actc / ocic,all =                                                              0.143 MPa<=1( Seismic)                                    Hence  OK"));
            //list.Add(string.Format("2 PTFE :"));
            //list.Add(string.Format("   (a)  Contdct stress = acp  = N x 1000 / An =                                 29.49                N/mm2                                                    < 40 N/mm^2       Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Stress, acpc  = göp+(Me,d+(Max of (Msg))/(xXdp'/32)=          34.25                                                       < 45 N/mm^2       Hence  OK"));
            //list.Add(string.Format("3  ELASTOMERIC DISC :"));
            //list.Add(string.Format("   (a)  Compressive stress,ace    = Nmax x 1000 / Ae :                    29.49                 MPa                                                        <=35 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Strëss, acpc   = ace+(M,,a+(Max of (MR.d))/(xXdí3/32)=       34.25                                                       <=40 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (c)  Strain at the perimeter due to rotation u rad. sp = 0.5 x di x u / he, eff =                                                                0.08 < 0.15        Hence   OK"));
            //list.Add(string.Format("   (d)  Clearance between cylinder top and piston bottom= bc-he-w-u.(di/2)    =                                                               8.75 >5 mm           Hence   OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4  POT CYL1NDER:"));
            //list.Add(string.Format("   (i)  Hoop's (tensile) stress  in the cross section, due to"));
            //list.Add(string.Format("   (a)  Fluid pressure at1=di  x he x ace / ( 2 x bp x hc) =                                                                                                          65.53              MPa"));
            //list.Add(string.Format("   (b)  Horizontal force at2 = H ×1000 x / ( 2 x bp x bc ) =                                                                                                     100.69.            MPa"));
            //list.Add(string.Format("   (c) Total stress = at1 + at2  =                                                    166.22 MPa                                                                  <= 204                 MPa"));
            //list.Add(string.Format("5  POT INTERFACE :"));
            //list.Add(string.Format("   (i)  Shear stress in cylinder & base interface considering    1mm orange slice, due to"));
            //list.Add(string.Format("  (a)  Fluid pressure, aq1= he    x ace/bp =                                                                                                                                17.47              MPa"));
            //list.Add(string.Format("   (b) Assuming Parabolic distribution factor =1.5, Horizontal Force aq2  = (1.5 x H x 1000/di) / bp =                                    40.28               MPa"));
            //list.Add(string.Format("   (c) Total stress og = agi   + aq2  =                                                              57.75 MPa                                                         153                 MPa   Hence  OK"));
            //list.Add(string.Format("   (ii) Beriding stress in the interface considering  1 mm orange slice"));
            //list.Add(string.Format("  (a)  Fluid  pressure, obt1 = (ace  x he2 /2) x 6 / bp2=                                 31.065 MPa"));
            //list.Add(string.Format("   (b) Horizontal force,abt2=( 1.5 x 1000 x H/di) x ( ha.kb) x 6 / bp2=       170.06  MPa"));
            //list.Add(string.Format("   (c) Totai Stress obt = abt1  + abt2 =                                                         201.13 MPa                                                                     224.4  MPa   Hence OK"));
            //list.Add(string.Format("   (iii) Combined stress  ,ae  = V( abt2 , 3×,92) =                                           224.6  MPa                                                                        306 MPa   Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             6 Eff.cont.width at piston-cylinder interface,we = Hx1000x1.3/(dixap)=    3.6961 mm                                                          < w    Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             7 GUIDE :"));
            //list.Add(string.Format("  (i)   Shear stress, aqu   = H x 1000 / ( Lu x ku ) =                                        36.25 MPa                                                                        153  MPa   Hence OK"));
            //list.Add(string.Format("  (ii) Bending Stress,abu    = H x 1000 x 6 x ( his / 2+8) / ( Lu x ku2 ) =      184.88 MPa                                                                     224.4  MPa  Hence  OK"));
            //list.Add(string.Format("  (iii) Combined stress,  aeu  = %( abu2 + 3 x aqu2) =                                  195.25  MPa                                                                       306  MPa   Hence  OK"));
            //list.Add(string.Format("  (iv) DirectstressonSS,adu=H×1000/(htsx(Lu-10))=                                    60.42  MPa                                                                        200 MPa   HenceOK"));
            //list.Add(string.Format("             8 ANCHOR :"));
            //list.Add(string.Format("   ( a) Anchor Screws"));
            //list.Add(string.Format("   Bottom:"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000)+(Nmin x µ)) > H. Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bottom of bearing, Coefficient of friction, µ =                                     0.2"));
            //list.Add(string.Format("         σ_bqiperm  = Permissible  shear stress  in  bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        Thetotalresistiveforce            Hperm=                                 223.3BkN>H         OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        Anchor Screws"));
            //list.Add(string.Format("        Bottom :"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000] > H, Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bóttom of bearing, Coefficient of friction, µ=                                      0.2"));
            //list.Add(string.Format("         abq,perm  = Permissible shear stress    in bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        The totalresistiveforce           Hperm=                                 255.68 kN>  H        OK"));
            //list.Add(string.Format("   (b)  Anchor Sleeves"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("         Bottom Screws."));
            //list.Add(string.Format(""));
            //list.Add(string.Format("( i ) Avg. bearing stress in conc. acav. =[Ab x abq,perm] / (  L x D ) =                                                                                          5.77  MPa"));
            //list.Add(string.Format("( ii) Considering traingular distribution , bending  stress  in conc, Sctr  = 2 x scav =                    .                                                 11.54 MPa"));
            //list.Add(string.Format("(iii) Peak stress, acpk=1.5xactr=                                                                                                                                                17.31  MPa"));
            //list.Add(string.Format("                                                                                                                                                                                  18.56 MPa  Hence   OK"));

            #endregion Sample

            File.WriteAllLines(Report_File, list.ToArray());
        }

        public void Generate_Report_VERSO_FIXED_BEARING()
        {
            List<string> list = new List<string>();
            #region TechSOFT Banner

            list.Add(string.Format(""));
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 20.0          *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*        DESIGN OF STEFLOMET POT/PTFE        *");
            list.Add("\t\t*             VERSO-FIXED BEARING            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #region Step 1 :  DESIGN PARAMETERS
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 1 :  DESIGN PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Vertical load [Nmax] = {0} kN", Nmax));
            list.Add(string.Format("Transverse Horizontal Force Normal [Hlatn]= {0} kN", Hlatn));

            list.Add(string.Format("Normal Vertical Load [Nnorm] = {0} kN", Nnorm));
            list.Add(string.Format("Transverse Horzontal Force Seismic [Hlts] = {0} kN", Hlts));

            list.Add(string.Format("Dead load [Nmin] = {0:f3} kN", Nmin));
            list.Add(string.Format("Long Horizontal Force Normal [Hlng_n] = {0} kN", Hlng_n));
            list.Add(string.Format("Rotation due to Dead Load [θp] = {0} rad", theta_p));
            list.Add(string.Format("Long Horizontal Force Seismic [Hlng_s] = {0} kN", Hlng_s));
            list.Add(string.Format("Rotation due to Live Load [θy] = {0} rad", theta_v));
            list.Add(string.Format("Design Horizontal Force For bearing components only [H] = {0} rad", H));


            list.Add(string.Format("Rotation [θ] = {0} rad", theta));

            list.Add(string.Format("Long Movement [elong](±)  = {0} mm", elong));
            list.Add(string.Format("Transverse Movement [etrans](±)  = {0} mm", etrans));

            list.Add(string.Format(""));

            #endregion Step 1 :  DESIGN PARAMETERS

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2 :  BEARING PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.1 : ELASTOMER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));
            list.Add(string.Format("Dia of Elastomeric pad [di] = {0} mm", di));
            list.Add(string.Format("Thickness of the Pad [he] = {0} mm", he));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4"));
            list.Add(string.Format("                 = (π x {0}^2) / 4", di));
            list.Add(string.Format("                 = {0:f4} mm^2", Ae));
            list.Add(string.Format(""));


            list.Add(string.Format("Hardness IRHD ( Shore-A) = {0} ± 5", IRHD));

            list.Add(string.Format("Value of Constant [k1] = {0} ", k1));
            list.Add(string.Format("Value of Constant [k2] = {0} ", k2));

            list.Add(string.Format("Moment due to resistance to Rotation = Med = di^3 x ( k1 X θp + k2 X θv)"));
            list.Add(string.Format("                                           = {0}^3 x ( {1} X {2} + {3} X {4})", di, k1, theta_p, k2, theta_v));
            list.Add(string.Format("                                           = {0:f3} N-mm", Med));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Normal) = MRdn = 0.2 X ( di / 2) X Hlng_n*1000"));
            list.Add(string.Format("                                                    = 0.2 X ( {0} / 2) X {1}*1000", di, Hlng_n));
            list.Add(string.Format("                                                    = {0:f3} N-mm", MRdn));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Transverse Seismic) = MRst = 0.2 X (di / 2) X Hlts*1000"));
            list.Add(string.Format("                                                                = 0.2 X ({0} / 2) X {1}*1000", di, Hlts));
            list.Add(string.Format("                                                                = {0:f3} N-mm", MRst));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Longitudinal Seismic) = MRsl =  0.2  X ( di / 2) X Hlng_s*1000"));
            list.Add(string.Format("                                                                  =  0.2  X ( {0} / 2) X {1}*1000", di, Hlng_s));
            list.Add(string.Format("                                                                  =  {0:f3} N-mm", MRsl));
            list.Add(string.Format(""));

            double MRds = Math.Max(MRst, MRsl);
            list.Add(string.Format("Moment due to frictional resistance (Seismic) = MRds = Max ( MRst OR MRsl )"));
            list.Add(string.Format("                                                     = Max ( {0:f3} OR {1:f3} )", MRst, MRsl));
            list.Add(string.Format("                                                     = {0:f3} N-mm", MRds));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format("STEP 2.2 : SADDLE "));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Dia of PTFE [dp] = {0} mm", dp));
            //list.Add(string.Format("Contact with piston [w] = {0} mm", w));
            //list.Add(string.Format("Thickness of Saddle plate [hm] = {0} mm", hm));
            //list.Add(string.Format("Eff. Plan area of PTFE [An] = (π x dp^2) / 4 =  {0} mm^2", An));
            //list.Add(string.Format("Height of SS on saddle [hts] = {0} mm", hts));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.2 : POT CYLINDER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia / sq of base plate [Do] = {0} mm", Do_));
            list.Add(string.Format("Thkness of base plate [kb] = {0} mm", kb));
            list.Add(string.Format("Outer dia of cylinder [do] = {0} mm", do_));
            list.Add(string.Format("Depth of cyfinder [hc] = {0} mm", hc));
            list.Add(string.Format("Effective dia at the bottom [deb] = di + 2 x 2 x kb"));
            list.Add(string.Format("                                  = {0} + 2 x 2 x {1}", di, kb));
            list.Add(string.Format("                                  = {0:f2} mm", deb));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of cylinder wall [bp] = (do-di)/2 = ({0} - {1})/2 = {2} mm", do_, di, bp));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically simifar to deb = deb_avibl = {0} mm", deb_avibl));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.3 : TOP ASSEMBLY "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of top piate [a] = {0} mm", a));
            list.Add(string.Format("Width of top plate [b] = {0} mm", b));
            list.Add(string.Format("Thickness of Saddle Plate [hm] = {0} mm", hm));
            list.Add(string.Format("Thickness of top plate [kt] = {0} mm", kt));
            //list.Add(string.Format("Length of SS plate [as] = {0} mm", as_));
            //list.Add(string.Format("Width of SS plate [bs] = {0} mm", bs));
            //list.Add(string.Format("Thickness of guide [ku] = {0} mm", ku));
            //list.Add(string.Format("Effective Length of guide [Lu] = {0} mm", Lu));
            list.Add(string.Format("Effective dia of the dispersed area at the top structure = det "));
            list.Add(string.Format(""));

            list.Add(string.Format("   det = Minimum Value of ((dp+2 x 2x(kt+ks)) & b)"));

            double val1 = 0;
            val1 = (di + 2 * 2 * hm);



            list.Add(string.Format("       = Minimum Value of (({0}+2 x 2 x {1} ) & {2})", di, hm, b));
            list.Add(string.Format("       = Minimum Value of ({0:f2} & {1:f2})", val1, b));
            list.Add(string.Format(""));
            list.Add(string.Format("       = {0:f3} mm", det));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically similar to [det_avibl] = {0} mm", det_avibl));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.4 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("BOTTOM"));
            list.Add(string.Format("------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bolt size [Mj] = {0} mm", bottom_Mj));
            list.Add(string.Format("Root area of bolt [Ab] = {0} mm^2", bottom_Ab));
            list.Add(string.Format("No. off boits [n] = {0} ", bottom_n));
            list.Add(string.Format("Class of bolt [bc] = {0} ", bottom_bc));
            list.Add(string.Format("Dia of Sleeve [D] = {0} mm", bottom_D));
            list.Add(string.Format("Length of sleeve [L] = {0} mm", bottom_L));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TOP"));
            list.Add(string.Format("---"));
            list.Add(string.Format(""));

            list.Add(string.Format(" Bolt Size [Mj] = {0} mm", top_Mj));
            list.Add(string.Format(" Root area of bolt [Ab] = {0} mm^2", top_Ab));
            list.Add(string.Format(" No. of bolts [n] = {0} ", top_n));
            list.Add(string.Format(" Class of bolt [bc] = {0} ", top_bc));






            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.5 : Ht of application of horizontal Force"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ht of application of horiz. Force = ha = kb + he + w/2"));
            list.Add(string.Format("                                       = {0} + {1} + {2}/2", kb, he, w));
            list.Add(string.Format("                                       = {0} mm", ha));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.6 : Overall height of bearing"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Overall height of bearing = h = {0} mm", h));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.1 : DESIGN CHECK"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at bottom of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Concrete [fck] = M{0} ", fck));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Normal) = σ_cb_all = 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                            = 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                            = {0:f3} MPa", conc_ads_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Normal) = σ_cbc_all = 0.33 x fck"));
            list.Add(string.Format("                                              = 0.33 x {0}", fck));
            list.Add(string.Format("                                              = {0:f3} MPa ", conc_abs_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Seismic) = σ_cb_all = 1.25 x 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                             = 1.25 x 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                             = {0:f3} MPa", conc_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = σ_cbc_all = 1.25 x 0.33 x fck"));
            list.Add(string.Format("                                               = 1.25 x 0.33 x {0}", fck));
            list.Add(string.Format("                                               = {0:f3} MPa", conc_abs_seismic));
            list.Add(string.Format(""));



            list.Add(string.Format("Bottom direct Seismic = σ_cb = Nmax  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                             = {0}  x 1000/(π x {1:f3}^2 /4)", Nmax, deb));
            list.Add(string.Format("                             = {0:f3} MPa", conc_sigma_cb_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom direct Normal = σ_cb = Nnorm  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                            = {0}  x 1000/(π x {1}^2 /4)", Nnorm, deb));
            list.Add(string.Format("                            = {0:f3} MPa", conc_sigma_cb_norm));
            list.Add(string.Format(""));

            list.Add(string.Format("Bottom bending Normal = σ_cbc =  Hlngn × 1000 x ha / π x deb^3 /32", conc_sigma_cbc_norm));
            list.Add(string.Format("                                 + (Med + MRdn) / ((π x deb^3 / 32)", conc_sigma_cbc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0} × 1000 x {1} / π x {2:f3}^3 /32", Hlng_n, ha, deb));
            list.Add(string.Format("                                 + ({0:f3} + {1:f3}) / ((π x {2:f3}^3 / 32)", Med, MRdn, deb));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0:f3}  MPa", conc_sigma_cbc_norm));

            list.Add(string.Format(""));
            //list.Add(string.Format("Bottom bending (Seismic) = σ_cbc   Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));
            list.Add(string.Format(""));

            double sigma_cbc1 = Hlng_s * 1000 * ha / (Math.PI * deb * deb * deb / 32.0) + (Med + MRds) / (Math.PI * deb * deb * deb / 32.0);
            double sigma_cbc2 = Hlts * 1000 * ha / (Math.PI * deb * deb * deb / 32.0);




            list.Add(string.Format("  σ_cbc1 =  Hlng_s x 1000 x ha / (π x deb^3 / 32) + (Med  + MRds) / (π x deb^3 / 32)"));
            list.Add(string.Format("         =  {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32) + ({3:f3}  + {4:f3}) / (π x {2:f3}^3 / 32)", Hlng_s, ha, deb, Med, MRds));
            list.Add(string.Format("         =  {0:f3} MPa", sigma_cbc1));
            list.Add(string.Format(""));
            list.Add(string.Format("  σ_cbc2 = Hlts x 1000 x ha / (π x deb^3 / 32)"));
            list.Add(string.Format("         = {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32)", Hlts, ha, deb));
            list.Add(string.Format("         = {0:f3} MPa", sigma_cbc2));
            list.Add(string.Format(""));



            conc_sigma_cbc_seismic = Math.Max(sigma_cbc1, sigma_cbc2);

            list.Add(string.Format("Bottom bending( Seismic) = σ_cbc =  Maximum Value of  ( {0:f3} OR {1:f3} )", sigma_cbc1, sigma_cbc2));
            list.Add(string.Format("                                 =  {0:f3} MPa ", conc_sigma_cbc_seismic));

            list.Add(string.Format(""));
            //list.Add(string.Format(" Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));

            //list.Add(string.Format("OR, Hlat_s x 1000 x ha / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));


            lamda_b_norm = (conc_sigma_cb_norm / conc_ads_norm) + (conc_sigma_cbc_norm / conc_abs_norm);

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_norm, conc_ads_norm, conc_sigma_cbc_norm, conc_abs_norm));
            list.Add(string.Format(""));

            if (lamda_b_norm <= 1)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Normal), Hence OK", lamda_b_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Normal), Hence NOT OK", lamda_b_norm));

            list.Add(string.Format(""));


            lamda_b_seismic = (conc_sigma_cb_seismic / conc_ads_seismic) + (conc_sigma_cbc_seismic / conc_abs_seismic);


            //list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all =   {0:f3} MPa (Seismic)", lamda_b_seismic));

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_seismic, conc_ads_seismic, conc_sigma_cbc_seismic, conc_abs_seismic));
            list.Add(string.Format(""));

            if (lamda_b_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_b_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_b_seismic));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at Top of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Steel = fy = {0} MPa", steel_fy));

            steel_ads_norm = 0.66 * steel_fy;

            list.Add(string.Format("Allowable  direct stress (Normal) = σ_ct_all =  0.66 * fy "));
            list.Add(string.Format("                                             =  0.66 * {0}", steel_fy));
            list.Add(string.Format("                                             =  {0:f3} MPa", steel_ads_norm));
            list.Add(string.Format(""));


            steel_abs_norm = 0.75 * steel_fy;

            list.Add(string.Format("Allowable  bending stress = σ_ctc_all =  0.75 * fy"));
            list.Add(string.Format("                                     =  0.75 * {0:f3} ", steel_fy));
            list.Add(string.Format("                                     =  {0:f3} MPa", steel_abs_norm));
            list.Add(string.Format(""));

            steel_ads_seismic = steel_ads_norm;
            steel_abs_seismic = steel_abs_norm;
            list.Add(string.Format("Allowable direct stress (Seismic) = Allowable direct stress( Normal ) = {0:f3} MPa", steel_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = Allowable bending stress( Normal ) = {0:f3} MPa", steel_abs_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_seismic = Nmax * 1000 / (Math.PI * det * det / 4);
            list.Add(string.Format("Top direct  σ_ct (Seismic)= Nmax x 1000/(π x det^2/4)"));
            list.Add(string.Format("                          = {0} x 1000/(π x {1:f3}^2/4)", Nmax, det));
            list.Add(string.Format("                          = {0:f3} MPa", steel_sigma_ct_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_norm = (Nnorm * 1000) / (Math.PI * det * det / 4.0);
            list.Add(string.Format("Top direct  σ_ct (Normal)= Nnorm x 1000/(π x det^2/4)", steel_sigma_ct_norm));
            list.Add(string.Format("                         = {0} x 1000/(π x {1:f2}^2/4)", Nnorm, det));
            list.Add(string.Format("                         = {0:f3} MPa", steel_sigma_ct_norm));
            list.Add(string.Format(""));

            steel_sigma_ctc_norm = Hlng_n * 1000 * (h - ha) / (Math.PI * det * det * det / 32.0) + (Med + MRdn) / (Math.PI * det * det * det / 32.0);
            list.Add(string.Format(""));

            list.Add(string.Format("Top bending normal = σ_ctc =  Hlngn x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("                              + (Med + MRdn) / (π x det^3 / 32)", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Top bending normal = σ_ctc =  {0:f3} x 1000 x ( {1} - {2} ) /(π x {3:f3}^3 / 32)", Hlng_n, h, ha, det));
            list.Add(string.Format("                              + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32)", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("                           =  {0:f3} MPa", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_ctc1 = Hlng_s * 1000 * (h - ha) / (Math.PI * det * det * det / 32) + (Med + MRdn) / (Math.PI * det * det * det / 32);
            list.Add(string.Format(" σ_ctc1 =  Hlng_s x 1000 x ( h-ha ) /(π x det^3 /32) "));
            list.Add(string.Format("           + (Med + MRdn) / (π x det^3 / 32)"));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} x 1000 x ( {1}-{2} ) /(π x {3:f3}^3 /32) ", Hlng_s, h, ha, det));
            list.Add(string.Format("           + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32))", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} MPa", sigma_ctc1));
            list.Add(string.Format(""));

            double sigma_ctc2 = Hlts * 1000 * (h - ha) / (Math.PI * det * det * det / 32);
            list.Add(string.Format(""));

            list.Add(string.Format(" σ_ctc2 = Hlts  x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("        = {0:f3}  x 1000 x ( {1}-{2} ) /(π x {2:f3}^3 / 32)", Hlts, h, ha, det));
            list.Add(string.Format("        = {0:f3} MPa", steel_sigma_ctc_seismic));

            steel_sigma_ctc_seismic = Math.Max(sigma_ctc1, sigma_ctc2);

            list.Add(string.Format(""));

            list.Add(string.Format("Top bending seismic = σ_ctc = Maximum Value of (σ_ctc1  OR  σ_ctc2)"));
            list.Add(string.Format("                            = Maximum Value of ({0:f3}  OR  {1:f3})", sigma_ctc1, sigma_ctc2));
            list.Add(string.Format("                            = {0:f3} MPa", steel_sigma_ctc_seismic));
            list.Add(string.Format(""));
            //list.Add(string.Format("OR"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Hlat_s  x 1000 x ( h-ha ) /(π x det^3 / 32)    =   {0:f3} MPa", steel_sigma_ctc_seismic));
            lamda_t_norm = (steel_sigma_ct_norm / steel_ads_norm) + (steel_sigma_ctc_norm / steel_abs_norm);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_norm, steel_ads_norm, steel_sigma_ctc_norm, steel_abs_norm));

            if (lamda_t_norm <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa  <= 1 (Normal), Hence OK", lamda_t_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa  > 1 (Normal), Hence NOT OK", lamda_t_norm));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            lamda_t_seismic = (steel_sigma_ct_seismic / steel_ads_seismic) + (steel_sigma_ctc_seismic / steel_abs_seismic);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all", lamda_t_seismic));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_seismic, steel_ads_seismic, steel_sigma_ctc_seismic, steel_abs_seismic));

            if (lamda_t_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_t_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_t_seismic));
            list.Add(string.Format(""));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format("STEP 3.1 : PTFE"));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //sigma_cp = Nnorm * 1000.0 / An;
            //list.Add(string.Format("(a)  Contact stress = σ_cp = Nnorm x 1000 / An "));
            //list.Add(string.Format("                           = {0} x 1000 / {1:f3}", Nnorm, An));
            //if (sigma_cp < 40)
            //    list.Add(string.Format("                           = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cp));
            //else
            //    list.Add(string.Format("                           = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cp));


            //list.Add(string.Format(""));


            //sigma_cpc = sigma_cp + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * dp * dp * dp / 32));


            //list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_cp + (Med + (Max of (MRdn OR MRds))/(π x dp^3/32)"));
            //list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_cp, Med, MRdn, MRds, dp));
            //if (sigma_cpc < 45)
            //    list.Add(string.Format("                                  = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cpc));
            //else
            //    list.Add(string.Format("                                  = {0:f3} N/mm^2  >  45 N/mm^2,    Hence NOT OK", sigma_cpc));

            //list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.2 : ELASTOMERIC DISC"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            sigma_ce = Nnorm * 1000 / Ae;
            list.Add(string.Format("(a)  Compressive stress = σ_ce = Nnorm x 1000 / Ae"));
            list.Add(string.Format("                               = {0:f3} x 1000 / {1:f3}", Nnorm, Ae));
            if (sigma_ce <= 35)
                list.Add(string.Format("                               = {0:f3} N/mm^2  <=  35 N/mm^2,    Hence OK", sigma_ce));
            else
                list.Add(string.Format("                               = {0:f3} N/mm^2  >  35 N/mm^2,    Hence NOT OK", sigma_ce));


            list.Add(string.Format(""));

            sigma_cpc = sigma_ce + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * di * di * di / 32));

            //list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc  = σ_cp + (Med + (Max of (MRdn))/(π x di^3/32) = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cp));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_ce + (Med + (Max of (MRdn OR MRds))/(π x di^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_ce, Med, MRdn, MRds, di));

            if (sigma_cpc < 40)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cpc));


            eff = 0.5 * di * theta / he;
            list.Add(string.Format(""));
            list.Add(string.Format("(c)  Strain at the perimeter due to rotation θ rad. eff = 0.5 x di x θ / he"));
            list.Add(string.Format("                                                        = 0.5 x {0} x θ / {1}", di, theta, he));
            if (eff < 0.15)
                list.Add(string.Format("                                                        = {0:f3} < 0.15, Hence OK", eff));
            else
                list.Add(string.Format("                                                        = {0:f3} >= 0.15, Hence NOT OK", eff));


            clrns = hc - he - w - theta * (di / 2);


            list.Add(string.Format(""));
            list.Add(string.Format("(d)  Clearance between cylinder top and piston bottom = hc-he-w-θ x (di/2)"));
            list.Add(string.Format("                                                      = {0} - {1} - {2} - {3} x ({4}/2)", hc, he, w, theta, di));
            if (clrns > 5)
                list.Add(string.Format("                                                      = {0:f3} > 5 ,   Hence OK", clrns));
            else
                list.Add(string.Format("                                                      = {0:f3} <= 5 ,   Hence NOT OK", clrns));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.3 : POT CYLINDER"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Hoop's (tensile) stress  in the cross section, due to"));
            list.Add(string.Format(""));


            sigma_t1 = di * he * sigma_ce / (2 * bp * hc);
            list.Add(string.Format("(a)  Fluid pressure σ_t1 = di  x he x σ_ce / ( 2 x bp x hc)"));
            list.Add(string.Format("                         = {0}  x {1} x {2:f3} / ( 2 x {3} x {4})", di, he, sigma_ce, bp, hc));
            list.Add(string.Format("                         = {0:f3} MPa", sigma_t1));
            list.Add(string.Format(""));

            sigma_t2 = (H * 1000) / (2.0 * bp * hc);
            list.Add(string.Format("(b)  Horizontal force σ_t2 = H x 1000 / ( 2 x bp x hc )"));
            list.Add(string.Format("                           = {0} x 1000 / ( 2 x {1} x {2} )", H, bp, hc));
            list.Add(string.Format("                           = {0:f3} MPa", sigma_t2));
            list.Add(string.Format(""));

            sigma_t = sigma_t1 + sigma_t2;
            list.Add(string.Format("(c) Total stress = σ_t = σ_t1 + σ_t2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3}", sigma_t1, sigma_t2));
            
            if (sigma_t < 204)
                list.Add(string.Format("                       = {0:f3} MPa   <  204 MPa ,  Hence OK", sigma_t));
            else
                list.Add(string.Format("                       = {0:f3} MPa   >=  204 MPa ,  Hence NOT OK", sigma_t));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.4 : POT INTERFACE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Shear stress in cylinder & base interface considering    1 mm orange slice, due to"));
            list.Add(string.Format(""));

            sigma_q1 = he * sigma_ce / bp;
            list.Add(string.Format("(a)  Fluid pressure, σ_q1 = he x σ_ce/bp "));
            list.Add(string.Format("                          = {0} x {1:f3}/{2} ", he, sigma_ce, bp));
            list.Add(string.Format("                          = {0:f3} MPa", sigma_q1));
            list.Add(string.Format(""));





            list.Add(string.Format("(b)  Assuming Parabolic distribution factor = 1.5, "));
            list.Add(string.Format(""));
            sigma_q2 = (1.5 * H * 1000 / di) / bp;
            list.Add(string.Format("      Horizontal Force = σ_q2 = (1.5 x H x 1000/di) / bp"));
            list.Add(string.Format("                              = (1.5 x {0} x 1000/{1}) / {2}", H, di, bp));
            list.Add(string.Format("                              = {0:f3} MPa", sigma_q2));
            list.Add(string.Format(""));
            sigma_q = sigma_q1 + sigma_q2;
            list.Add(string.Format("(c)  Total stress = σ_q = σ_q1 + σ_q2"));
            list.Add(string.Format("                        = {0:f3}  + {1:f3}", sigma_q1, sigma_q2));
            if (sigma_q < 153)
                list.Add(string.Format("                        = {0:f3} MPa  < 153 MPa Hence OK", sigma_q));
            else
                list.Add(string.Format("                        = {0:f3} MPa  > 153 MPa Hence NOT OK", sigma_q));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Beriding stress in the interface considering  1 mm orange slice"));
            list.Add(string.Format(""));

            sigma_bt1 = ((sigma_ce * he * he / 2) * 6.0) / (bp * bp);

            list.Add(string.Format("(a) Fluid  pressure = σ_bt1 = (σ_ce  x he^2 /2) x 6 / bp^2", sigma_bt1));
            list.Add(string.Format("                            = ({0:f3}  x {1}^2 /2) x 6 / {2}^2 ", sigma_ce, he, bp));
            list.Add(string.Format("                            = {0:f3} MPa", sigma_bt1));

            sigma_bt2 = ((1.5 * 1000 * H / di) * (ha - kb) * 6) / (bp * bp);
            list.Add(string.Format(""));

            list.Add(string.Format("(b) Horizontal force = σ_bt2 = ( 1.5 x 1000 x H/di) x ( ha-kb) x 6 / bp^2", sigma_bt2));
            list.Add(string.Format("                             = ( 1.5 x 1000 x {0}/{1}) x ( {2}-{3}) x 6 / {4}^2", H, di, ha, kb, bp));
            list.Add(string.Format("                             = {0:f3} MPa", sigma_bt2));

            sigma_bt = sigma_bt1 + sigma_bt2;
            list.Add(string.Format(""));

            list.Add(string.Format("(c) Totai Stress  σ_bt = σ_bt1  + σ_bt2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3} ", sigma_bt1, sigma_bt2));
            if (sigma_bt < 224.4)
                list.Add(string.Format("                       = {0:f3} MPa  < 224.4  MPa,   Hence OK", sigma_bt));
            else
                list.Add(string.Format("                       = {0:f3} MPa  >= 224.4  MPa,   Hence NOT OK", sigma_bt));

            list.Add(string.Format(""));
            sigma_e = Math.Sqrt((sigma_bt * sigma_bt + 3 * sigma_q * sigma_q));
            list.Add(string.Format("(iii) Combined stress  σ_e  = √(σ_bt^2 + 3 × σ_q^2) "));
            list.Add(string.Format("                            = √({0:f3}^2 + 3 × {1:f3}^2)", sigma_bt, sigma_q));

            if (sigma_e < 306.0)
                list.Add(string.Format("                            = {0:f3} MPa < 306 MPa   Hence OK", sigma_e));
            else
                list.Add(string.Format("                            = {0:f3} MPa > 306 MPa   Hence NOT OK", sigma_e));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.5 : Eff.cont.width at piston-cylinder interface"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double sigma_p = 254.84;
            list.Add(string.Format("σ_p = {0:f3} MPa", sigma_p));
            list.Add(string.Format(""));


            we = (H * 1000 * 1.3) / (di * sigma_p);
            list.Add(string.Format("Eff.cont.width at piston-cylinder interface = we = H x 1000 x 1.3 / (di x σ_p)"));
            list.Add(string.Format("                                                 = {0} x 1000 x 1.3 / ({1} x {2:f3})", H, di, sigma_p));
            if (we < w)
                list.Add(string.Format("                                                 = {0:f4} mm  <  {1} (w)     Hence OK", we, w));
            else
                list.Add(string.Format("                                                 = {0:f4} mm  >=   {1} (w)     Hence NOT OK", we, w));

            //list.Add(string.Format(""));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format("STEP 3.6 : GUIDE "));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //sigma_qu = (H * 1000) / (Lu * ku);
            //list.Add(string.Format("(i)  Shear stress  σ_qu = H x 1000 / ( Lu x ku ) "));
            //list.Add(string.Format("                        = {0} x 1000 / ( {1} x {2} )", H, Lu, ku));
            //if (sigma_qu < 153)
            //    list.Add(string.Format("                        = {0:f4} MPa   <   153 MPa, Hence OK", sigma_qu));
            //else
            //    list.Add(string.Format("                        = {0:f4} MPa   >=   153 MPa, Hence NOT OK", sigma_qu));



            //list.Add(string.Format(""));





            //sigma_bu = H * 1000 * 6 * (hts / 2 + 8) / (Lu * ku * ku);

            //list.Add(string.Format("(ii)  Bending Stress = σ_bu = H x 1000 x 6 x ( hts / 2+8) / ( Lu x ku^2 )"));
            //list.Add(string.Format("                            = {0} x 1000 x 6 x ( {1} / 2+8) / ( {2} x {3}^2 )", H, hts, Lu, ku));

            //if (sigma_bu < 224.4)
            //    list.Add(string.Format("                            = {0:f4} MPa   <   224.4 MPa, Hence OK", sigma_bu));
            //else
            //    list.Add(string.Format("                            = {0:f4} MPa   >=   224.4 MPa, Hence NOT OK", sigma_bu));

            //list.Add(string.Format(""));

            //sigma_eu = Math.Sqrt(sigma_bu * sigma_bu + 3 * sigma_qu * sigma_qu);

            //list.Add(string.Format("(iii) Combined stress = σ_eu = √(σ_bu^2 + 3 x σ_qu^2)"));
            //list.Add(string.Format("(                            = √({0:f3}^2 + 3 x {1:f3}^2)", sigma_bu, sigma_qu));
            //if (sigma_eu < 306.0)
            //    list.Add(string.Format("(                            = {0:f4} MPa   <   306 MPa ", sigma_eu));
            //else
            //    list.Add(string.Format("(                            = {0:f4} MPa   >=   306 MPa ", sigma_eu));

            //sigma_du = H * 1000 / (hts * (Lu - 10));
            //list.Add(string.Format(""));

            //list.Add(string.Format("(iv)  Direct stress on SS = σ_du = H × 1000/(hts x (Lu-10)) "));
            //list.Add(string.Format("                                 = {0} × 1000/({1} x ({2}-10)) ", H, hts, Lu));
            //if (sigma_du < 200)
            //    list.Add(string.Format("                                 = {0:f4} MPa < 200 MPa ", sigma_du));
            //else
            //    list.Add(string.Format("                                 = {0:f4} MPa >= 200 MPa ", sigma_du));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.7 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(a) Anchor Screws"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            bottom_Hperm = ((bottom_n * bottom_Ab * bottom_sigma_bq_perm) / 1000) + (Nmin * bottom_mu);

            //list.Add(string.Format("Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)] > H = Maximum Horizontal force on bearing."));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3}", bottom_mu));
            list.Add(string.Format("σ_bq_perm  = Permissible  shear stress  in  bolt = {0:f3}  MPa", bottom_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maximum Horizontal force on bearing"));
            list.Add(string.Format(""));
            //list.Add(string.Format("The total resistive force = Hperm = {0:f3} kN > H , OK ", bottom_Hperm));


            list.Add(string.Format("The total resistive force = Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)]"));
            list.Add(string.Format("                                  = [({0} x {1:f3} x {2:f3}) / 1000)+({3:f3} x {4:f3})]", bottom_n, bottom_Ab, bottom_sigma_bq_perm, Nmin, bottom_mu));

            if (bottom_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H),   Hence OK", bottom_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H),   Hence NOT OK", bottom_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format("Top :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Anchor Screws"));
            //list.Add(string.Format("-------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Bottom :"));
            list.Add(string.Format(""));

            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            top_Hperm = ((top_n * top_Ab * top_sigma_bq_perm) / 1000);
            //list.Add(string.Format("Hperm = ((n x Ab x σ_bq_perm) / 1000) > H, Maximum Horizontal force on bearing."));
            list.Add(string.Format(""));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3} ", top_mu));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_bq,perm  = Permissible shear stress in bolt = {0:f3} MPa", top_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maxm. Horizontal force on bearing"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force = Hperm = (n x Ab x σ_bq_perm) / 1000", top_Hperm));
            list.Add(string.Format("                                  = ({0} x {1:f3} x {2:f3}) / 1000 ", top_n, top_Ab, top_sigma_bq_perm));
            if (top_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H) , OK", top_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H)  , NOT OK", top_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b)  Anchor Sleeves"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Screws."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cav = (bottom_Ab * bottom_sigma_bq_perm) / (bottom_L * bottom_D);
            list.Add(string.Format("( i ) Avg. bearing stress in conc. σ_cav. = (Ab x σ_bq_perm) / (L x D ) "));
            list.Add(string.Format("                                          = ({0:f3} x {1:f3}) / ({2:f3} x {3:f3} )", bottom_Ab, bottom_sigma_bq_perm, bottom_L, bottom_D));
            list.Add(string.Format("                                          = {0:f3} MPa", sigma_cav));
            list.Add(string.Format(""));


            double sigma_ctr = 2 * sigma_cav;

            list.Add(string.Format("( ii) Considering traingular distribution, bending stress in conc = σ_ctr = 2 x σ_cav"));
            list.Add(string.Format("                                                                          = 2 x {0:f3}", sigma_cav));
            list.Add(string.Format("                                                                          = {0:f3} MPa", sigma_ctr));
            list.Add("");
            double sigma_cpk = 1.5 * sigma_ctr;
            list.Add(string.Format("(iii) Peak stress = σ_cpk = 1.5 x σ_ctr"));
            list.Add(string.Format("                          = 1.5 x {0:f3}", sigma_ctr));
            if (sigma_cpk < 18.56)
                list.Add(string.Format("                          = {0:f3} MPa  < 18.56 MPa, Hence OK", sigma_cpk));
            else
                list.Add(string.Format("                          = {0:f3} MPa  >= 18.56 MPa, Hence NOT OK", sigma_cpk));
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");

            #region Sample


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CONTRACTOR : M/s. SEW INFRASTRUCTURE                       LTD.                                   2/25/201316:40"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                   DESIGN       OF Steflomet POT/PTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("1     REFERENCE:"));
            //list.Add(string.Format("       Drawing                                                                  :3..P14VMT Sp¢     Rev-0      REFERENCE :                                         IRC:83(Part-W)"));
            //list.Add(string.Format("       Project :For four laning of krishnagar Baharampour section of NH-34 from km 115.000 to km 193.000 West Barigal."));
            //list.Add(string.Format("       For ROB at Chanage;  133+432+501                      Quantity -4+4 nos.                 Location: A1&A4                                    Type - Transverselý Guided."));

            //list.Add(string.Format("       Normal Vertical Load, Nnorm                              : 1,334.00                   kN         Trans. Horz. Force  Hlts (Seismic)           0.00                 kN"));




            //list.Add(string.Format("       Dead load,  Nmin                                         :   332.00                      kN         Long Horz Force( Normal ) Hlng_n                  65.00                kN"));
            //list.Add(string.Format("       Rotation due to Dead Load. θp                            :     0.0065                     rad          Long Harz. Force, (Seismic) Hlng_s          174.00              kN"));
            //list.Add(string.Format("       Rotation due to Live Load, θy                            :     0.0015                      rad         Design horz. Force, H(For bearing            174.00              kN"));
            //list.Add(string.Format("       Rotation, e                                              : 0.010                        rad         components only)"));
            //list.Add(string.Format("       Movement, elong(±)                                               O                              mm        Movement, etrans.(±)                              10.0                 mm"));


            //list.Add(string.Format("             3      BEARINGPARAMETERS"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("      1.   Steel components to conform IS:2062(MS), IS: 1030 Gr.340-570 W(CS) Therefore, Yield Stress, fy 340 MPa"));

            //list.Add(string.Format("            Dia of Elastomeric pad, di                               :  240                         mm         Thickness of the Pad ,he                           16                   mm"));
            //list.Add(string.Format("            Area of Pad. Ae  = π x di2 / 4                       .   : 4.52E+04                 mm2       Hardness  IRHD ( Shore-A)                      50 ± 5"));
            //list.Add(string.Format("            Value of Constant, k1                                     :  2.2                                          Value of Constant, k2                               101"));
            //list.Add(string.Format("            Moment due to resistance to Rotation.                                        Med = di' X ( k, X Op + k2 X 0,)                                         2.29E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Normal)..                             MRdn = 0.2 X ( di / 2) X He                                             1.56E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Transverse Seismic),               MRst  = 0.2 X ( d  / 2) X Hlts                                .             0.00E+00  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Longitudinal Seismic),            MRsl  = 0.2  X ( di / 2) X H  .                                             4.18E+06  N-mm"));
            //list.Add(string.Format("       3.  SADDLE"));
            //list.Add(string.Format("            Dia of PTFE(dp)                                             :  240                          mm        Contact with piston,w                              6                       mm"));
            //list.Add(string.Format("            Thickness of Saddle plate,  hm                        :  12                            mm        Eff. Plan area of PTFE,An                       45238.93         mm®"));
            //list.Add(string.Format("            Height of SS on saddle,hts                               :  12                            mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("       4.  POT CYLINDER"));
            //list.Add(string.Format("            Dia / sq of base plate,  Do                                :  310                          mm        Thkness of base plate,kb                          15                     mm"));
            //list.Add(string.Format("            Outer dia of cylinder, do                                  :  294                          mm        Depth of cyfinder,hc                                 32                   mm"));
            //list.Add(string.Format("            Effective dia at the bottom, deb                     .  300                          mm        Width of cylinder wall                                27                    mm"));
            //list.Add(string.Format("            di + 2 x 2 x kb                                                                                                   bp=(do-di)/2"));
            //list.Add(string.Format("            Available diameter (deb,avibl) at the substructure, geometrically    simifar to, deb =                                                          650                   mm"));
            //list.Add(string.Format("       5.   TOP ASSEMBLY"));
            //list.Add(string.Format("             Length of top piate, a.                                   :  400                           mm       Width of top plate,b                                  500                  mm"));
            //list.Add(string.Format("             Thk.ness of SS Plate(ks)                                 : 3                               mm        Thickness of top plate, kt                        14                     mm"));
            //list.Add(string.Format("             Length of SS plate, as                                     :  320                          mm        Width of SS plate,  bs                                300                  mm"));
            //list.Add(string.Format("             Thk. of guide, ku                                            :  20                            mm        Effective Length of guide,Lu                     240                 mm"));
            //list.Add(string.Format("             Effective dia of the dispersed  area at the top structure, det  = Min((dp+2 x 2x(kt+ks)) & b]                                           308                  mm"));
            //list.Add(string.Format("             Available diameter (det,avibl)  at the substructure, geometrically similar to,  det =                                                           650                   mm"));
            //list.Add(string.Format("       6.   ANCHOR"));
            //list.Add(string.Format("             Bottom:"));
            //list.Add(string.Format("             Bolt size( IS:1363) Mj                              :        16                            mm         Root area of bolt Ab                                167                   mm2"));
            //list.Add(string.Format("             No. off boits(n)                                        :        4                                            Class of bolt, bc                                10.9"));
            //list.Add(string.Format("             Dia of Sleeve, D                                       :        40                              mm       Length of sleeve , L                                 170                   mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             TOP :"));
            //list.Add(string.Format("             Size (1S:1363) Mj                                     :        20                                          Root area of bolt,Ab                                 272                  mm2"));
            //list.Add(string.Format("             No. of bolts(n)                                         :        4                                             Class of bolt(tS:1367)                               10.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        7.   Ht of applicat5on of horiz. Force(ha)= kb + he + w/2                                                                                                      34                    mm"));
            //list.Add(string.Format("        8.   Overall height of bearing (h) mm                                                                                                                                    88                     mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(" 4     DES1GN CHECK :"));
            //list.Add(string.Format("        Structure at bottom of Bearing :                              Concrete                                Grade of Concrete    , fck                          M45"));
            //list.Add(string.Format("        Allowable direct stress(Normal)                              0.25 fck X ((debavi) / (deb))                                                                    22.50              MPa"));
            //list.Add(string.Format("        Allowable bending stress(Normal)                           0.33 fck                                                                                                  14.85               MPa"));
            //list.Add(string.Format("        Allowable direct stress ( Seismic)                             1.25 x 0.25 fck X ((debavl) / (deb))                                                        28.13               MPa"));
            //list.Add(string.Format("        Allowabie bending stress ( Seismic)                          1.25 x 0.33 fck.                                                                                      18.56              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Seismic)                                   N max  x 1000!(x(deb) /4) MPa =                                                           19.81              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Normal)                                   N norm. x 1000/(x(deb)2/4) MPa =                                                        18.87               MPa"));
            //list.Add(string.Format("        Bottom bending( Normal) σ_cbc = Hn × 1000 x ha / x.(deb)3 /32 + (Med + Mw) / (u deb' / 32)  MPa =                         -        2.29                 MPa"));
            //list.Add(string.Format("        Bottom bending( Seismic) σ_cbc,Max, Hing,s   x 1000 x ha / (x deb' / 32)   + (Ma  + Man) / (n deb3 / 32) MPa ="));
            //list.Add(string.Format("        OR, Hlat,s x 1000 x ha / (x deb' / 32)   MPa =                                                                                                                                      4.67  MPa"));
            //list.Add(string.Format("        λb=acb/acb,all      +acbc /acbc,all=                                                0.993                 MPa<=1(Normal)                                     Hence  OK"));
            //list.Add(string.Format("        λb = acb / ocb,all +  acbc / acbc,all   =                                          0.956                 MPa<=1( Seism c)                                    Hence  OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                       CONTRACTOR             :    M/s. SEW             INFRASTRUCTURE                      LTD.                                  2/25/201316:40"));
            //list.Add(string.Format("                DESIGN OF Steflomet POTIPTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format("   Structure at Top of Bearing :                                  STEEL to IS :  2062               fy.=                                                                         230  MPa"));
            //list.Add(string.Format("   Allowable  direct stress( Normal )=                         0.66 * fy                                                                                                151.80            MPa"));
            //list.Add(string.Format("   Arlowable bending stress (Normal)=                     0.75 * fy                                                                                               172.5               MPa"));
            //list.Add(string.Format("   Allowable direct stress( Seismic )= Allowable direct stress( Normal )                                                           151.80             MPa"));
            //list.Add(string.Format("                     .                     Allowable bending  stress ( Seismic)   =                    Allowable bending stress( Normal )                                                        172.50"));
            //list.Add(string.Format("   Top directµct(Seismic)= Nmaxx1000/(udet2/4)N/mm2;                                                                                                        18.79              MPa"));
            //list.Add(string.Format("   Topdirect,act(Normal)=  N x1000 / (udet2/4)N/mm2  =                                                                                                       17.90              MPa"));
            //list.Add(string.Format("   Top bending normal,acic =  Hn x 1000 x ( h-ha ) /(x.det3 / 32) + (M,ø + Mpø) / (x det® / 32) MPa =                                 2.57                MPa"));
            //list.Add(string.Format("   Top bending seismic,actc =Max, Hlng,s   x 1000 x ( h-ha ) /(n.det' / 32) + (Me + Maø) / (2 det' / 32) MPa ="));
            //list.Add(string.Format("   OR, Hlat,s  x 1000 x ( h-ha ) /(x.det3 / 32) MPa =                                                                                                                              3.28  MPa"));
            //list.Add(string.Format("   at = act / act,ali + actc / actc.all  =                                                              0.133  MPa<=1(Normal)                                    Hence OK"));
            //list.Add(string.Format("   At = act / oct,ali + actc / ocic,all =                                                              0.143 MPa<=1( Seismic)                                    Hence  OK"));
            //list.Add(string.Format("2 PTFE :"));
            //list.Add(string.Format("   (a)  Contdct stress = acp  = N x 1000 / An =                                 29.49                N/mm2                                                    < 40 N/mm^2       Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Stress, acpc  = göp+(Me,d+(Max of (Msg))/(xXdp'/32)=          34.25                                                       < 45 N/mm^2       Hence  OK"));
            //list.Add(string.Format("3  ELASTOMERIC DISC :"));
            //list.Add(string.Format("   (a)  Compressive stress,ace    = Nmax x 1000 / Ae :                    29.49                 MPa                                                        <=35 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Strëss, acpc   = ace+(M,,a+(Max of (MR.d))/(xXdí3/32)=       34.25                                                       <=40 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (c)  Strain at the perimeter due to rotation u rad. sp = 0.5 x di x u / he, eff =                                                                0.08 < 0.15        Hence   OK"));
            //list.Add(string.Format("   (d)  Clearance between cylinder top and piston bottom= bc-he-w-u.(di/2)    =                                                               8.75 >5 mm           Hence   OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4  POT CYL1NDER:"));
            //list.Add(string.Format("   (i)  Hoop's (tensile) stress  in the cross section, due to"));
            //list.Add(string.Format("   (a)  Fluid pressure at1=di  x he x ace / ( 2 x bp x hc) =                                                                                                          65.53              MPa"));
            //list.Add(string.Format("   (b)  Horizontal force at2 = H ×1000 x / ( 2 x bp x bc ) =                                                                                                     100.69.            MPa"));
            //list.Add(string.Format("   (c) Total stress = at1 + at2  =                                                    166.22 MPa                                                                  <= 204                 MPa"));
            //list.Add(string.Format("5  POT INTERFACE :"));
            //list.Add(string.Format("   (i)  Shear stress in cylinder & base interface considering    1mm orange slice, due to"));
            //list.Add(string.Format("  (a)  Fluid pressure, aq1= he    x ace/bp =                                                                                                                                17.47              MPa"));
            //list.Add(string.Format("   (b) Assuming Parabolic distribution factor =1.5, Horizontal Force aq2  = (1.5 x H x 1000/di) / bp =                                    40.28               MPa"));
            //list.Add(string.Format("   (c) Total stress og = agi   + aq2  =                                                              57.75 MPa                                                         153                 MPa   Hence  OK"));
            //list.Add(string.Format("   (ii) Beriding stress in the interface considering  1 mm orange slice"));
            //list.Add(string.Format("  (a)  Fluid  pressure, obt1 = (ace  x he2 /2) x 6 / bp2=                                 31.065 MPa"));
            //list.Add(string.Format("   (b) Horizontal force,abt2=( 1.5 x 1000 x H/di) x ( ha.kb) x 6 / bp2=       170.06  MPa"));
            //list.Add(string.Format("   (c) Totai Stress obt = abt1  + abt2 =                                                         201.13 MPa                                                                     224.4  MPa   Hence OK"));
            //list.Add(string.Format("   (iii) Combined stress  ,ae  = V( abt2 , 3×,92) =                                           224.6  MPa                                                                        306 MPa   Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             6 Eff.cont.width at piston-cylinder interface,we = Hx1000x1.3/(dixap)=    3.6961 mm                                                          < w    Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             7 GUIDE :"));
            //list.Add(string.Format("  (i)   Shear stress, aqu   = H x 1000 / ( Lu x ku ) =                                        36.25 MPa                                                                        153  MPa   Hence OK"));
            //list.Add(string.Format("  (ii) Bending Stress,abu    = H x 1000 x 6 x ( his / 2+8) / ( Lu x ku2 ) =      184.88 MPa                                                                     224.4  MPa  Hence  OK"));
            //list.Add(string.Format("  (iii) Combined stress,  aeu  = %( abu2 + 3 x aqu2) =                                  195.25  MPa                                                                       306  MPa   Hence  OK"));
            //list.Add(string.Format("  (iv) DirectstressonSS,adu=H×1000/(htsx(Lu-10))=                                    60.42  MPa                                                                        200 MPa   HenceOK"));
            //list.Add(string.Format("             8 ANCHOR :"));
            //list.Add(string.Format("   ( a) Anchor Screws"));
            //list.Add(string.Format("   Bottom:"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000)+(Nmin x µ)) > H. Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bottom of bearing, Coefficient of friction, µ =                                     0.2"));
            //list.Add(string.Format("         σ_bqiperm  = Permissible  shear stress  in  bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        Thetotalresistiveforce            Hperm=                                 223.3BkN>H         OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        Anchor Screws"));
            //list.Add(string.Format("        Bottom :"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000] > H, Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bóttom of bearing, Coefficient of friction, µ=                                      0.2"));
            //list.Add(string.Format("         abq,perm  = Permissible shear stress    in bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        The totalresistiveforce           Hperm=                                 255.68 kN>  H        OK"));
            //list.Add(string.Format("   (b)  Anchor Sleeves"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("         Bottom Screws."));
            //list.Add(string.Format(""));
            //list.Add(string.Format("( i ) Avg. bearing stress in conc. acav. =[Ab x abq,perm] / (  L x D ) =                                                                                          5.77  MPa"));
            //list.Add(string.Format("( ii) Considering traingular distribution , bending  stress  in conc, Sctr  = 2 x scav =                    .                                                 11.54 MPa"));
            //list.Add(string.Format("(iii) Peak stress, acpk=1.5xactr=                                                                                                                                                17.31  MPa"));
            //list.Add(string.Format("                                                                                                                                                                                  18.56 MPa  Hence   OK"));

            #endregion Sample

            File.WriteAllLines(Report_File, list.ToArray());
        }

        public void Generate_Report_VERSO_BI_AXIAL_BEARING()
        {
            List<string> list = new List<string>();
            #region TechSOFT Banner

            list.Add(string.Format(""));
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 20.0          *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*        DESIGN OF STEFLOMET  POT/PTFE       *");
            list.Add("\t\t*          VERSO BI - AXIAL BEARING          *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #region Step 1 :  DESIGN PARAMETERS
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 1 :  DESIGN PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Vertical load [Nmax] = {0} kN", Nmax));
            list.Add(string.Format("Transverse Horizontal Force Normal [Hlatn]= {0} kN", Hlatn));

            list.Add(string.Format("Normal Vertical Load [Nnorm] = {0} kN", Nnorm));
            list.Add(string.Format("Transverse Horzontal Force Seismic [Hlts] = {0} kN", Hlts));

            list.Add(string.Format("Dead load [Nmin] = {0:f3} kN", Nmin));
            list.Add(string.Format("Long Horizontal Force Normal [Hlng_n] = {0} kN", Hlng_n));
            list.Add(string.Format("Rotation due to Dead Load [θp] = {0} rad", theta_p));
            list.Add(string.Format("Long Horizontal Force Seismic [Hlng_s] = {0} kN", Hlng_s));
            list.Add(string.Format("Rotation due to Live Load [θy] = {0} rad", theta_v));
            list.Add(string.Format("Design Horizontal Force For bearing components only [H] = {0} rad", H));


            list.Add(string.Format("Rotation [θ] = {0} rad", theta));

            list.Add(string.Format("Long Movement [elong](±)  = {0} mm", elong));
            list.Add(string.Format("Transverse Movement [etrans](±)  = {0} mm", etrans));

            list.Add(string.Format(""));

            #endregion Step 1 :  DESIGN PARAMETERS

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2 :  BEARING PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.1 : ELASTOMER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));
            list.Add(string.Format("Dia of Elastomeric pad [di] = {0} mm", di));
            list.Add(string.Format("Thickness of the Pad [he] = {0} mm", he));
            //list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4 = {0:f4} mm^2", Ae));

            list.Add(string.Format(""));
            list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4"));
            list.Add(string.Format("                 = (π x {0}^2) / 4", di));
            list.Add(string.Format("                 = {0:f4} mm^2", Ae));
            list.Add(string.Format(""));
            list.Add(string.Format("Hardness IRHD ( Shore-A) = {0} ± 5", IRHD));

            list.Add(string.Format("Value of Constant [k1] = {0} ", k1));
            list.Add(string.Format("Value of Constant [k2] = {0} ", k2));

            list.Add(string.Format("Moment due to resistance to Rotation = Med = di^3 x ( k1 X θp + k2 X θv)"));
            list.Add(string.Format("                                           = {0}^3 x ( {1} X {2} + {3} X {4})", di, k1, theta_p, k2, theta_v));
            list.Add(string.Format("                                           = {0:f3} N-mm", Med));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Normal) = MRdn = 0.2 X ( di / 2) X Hlng_n*1000"));
            list.Add(string.Format("                                                    = 0.2 X ( {0} / 2) X {1}*1000", di, Hlng_n));
            list.Add(string.Format("                                                    = {0:f3} N-mm", MRdn));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Transverse Seismic) = MRst = 0.2 X (di / 2) X Hlts*1000"));
            list.Add(string.Format("                                                                = 0.2 X ({0} / 2) X {1}*1000", di, Hlts));
            list.Add(string.Format("                                                                = {0:f3} N-mm", MRst));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Longitudinal Seismic) = MRsl =  0.2  X ( di / 2) X Hlng_s*1000"));
            list.Add(string.Format("                                                                  =  0.2  X ( {0} / 2) X {1}*1000", di, Hlng_s));
            list.Add(string.Format("                                                                  =  {0:f3} N-mm", MRsl));
            list.Add(string.Format(""));

            double MRds = Math.Max(MRst, MRsl);
            list.Add(string.Format("Moment due to frictional resistance (Seismic) = MRds = Max ( MRst OR MRsl )"));
            list.Add(string.Format("                                                     = Max ( {0:f3} OR {1:f3} )", MRst, MRsl));
            list.Add(string.Format("                                                     = {0:f3} N-mm", MRds));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.2 : SADDLE "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia of PTFE [dp] = {0} mm", dp));
            list.Add(string.Format("Contact with piston [w] = {0} mm", w));
            list.Add(string.Format("Thickness of Saddle plate [hm] = {0} mm", hm));
            list.Add(string.Format("Eff. Plan area of PTFE [An] = (π x dp^2) / 4 =  {0} mm^2", An));
            //list.Add(string.Format("Height of SS on saddle [hts] = {0} mm", hts));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.3 : POT CYLINDER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia / sq of base plate [Do] = {0} mm", Do_));
            list.Add(string.Format("Thkness of base plate [kb] = {0} mm", kb));
            list.Add(string.Format("Outer dia of cylinder [do] = {0} mm", do_));
            list.Add(string.Format("Depth of cyfinder [hc] = {0} mm", hc));
            list.Add(string.Format("Effective dia at the bottom [deb] = di + 2 x 2 x kb"));
            list.Add(string.Format("                                  = {0} + 2 x 2 x {1}", di, kb));
            list.Add(string.Format("                                  = {0:f2} mm", deb));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of cylinder wall [bp] = (do-di)/2 = ({0} - {1})/2 = {2} mm", do_, di, bp));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically simifar to deb = deb_avibl = {0} mm", deb_avibl));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.4 : TOP ASSEMBLY "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of top piate [a] = {0} mm", a));
            list.Add(string.Format("Width of top plate [b] = {0} mm", b));
            list.Add(string.Format("Thickness of SS Plate [ks] = {0} mm", ks));
            list.Add(string.Format("Thickness of top plate [kt] = {0} mm", kt));
            list.Add(string.Format("Length of SS plate [as] = {0} mm", as_));
            list.Add(string.Format("Width of SS plate [bs] = {0} mm", bs));
            //list.Add(string.Format("Thickness of guide [ku] = {0} mm", ku));
            //list.Add(string.Format("Effective Length of guide [Lu] = {0} mm", Lu));
            list.Add(string.Format("Effective dia of the dispersed area at the top structure = det "));
            list.Add(string.Format(""));

            list.Add(string.Format("   det = Minimum Value of ((dp+2 x 2x(kt+ks)) & b)"));

            double val1 = 0;
            val1 = (dp + 2 * 2 * (kt + ks));



            list.Add(string.Format("       = Minimum Value of (({0}+2 x 2x({1}+{2})) & {3})", dp, kt, ks, b));
            list.Add(string.Format("       = Minimum Value of ({0:f2} & {1:f2})", val1, b));
            list.Add(string.Format(""));
            list.Add(string.Format("       = {0:f3} mm", det));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically similar to [det_avibl] = {0} mm", det_avibl));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.5 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("BOTTOM"));
            list.Add(string.Format("------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bolt size [Mj] = {0} mm", bottom_Mj));
            list.Add(string.Format("Root area of bolt [Ab] = {0} mm^2", bottom_Ab));
            list.Add(string.Format("No. off boits [n] = {0} ", bottom_n));
            list.Add(string.Format("Class of bolt [bc] = {0} ", bottom_bc));
            list.Add(string.Format("Dia of Sleeve [D] = {0} mm", bottom_D));
            list.Add(string.Format("Length of sleeve [L] = {0} mm", bottom_L));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TOP"));
            list.Add(string.Format("---"));
            list.Add(string.Format(""));

            list.Add(string.Format(" Bolt Size [Mj] = {0} mm", top_Mj));
            list.Add(string.Format(" Root area of bolt [Ab] = {0} mm^2", top_Ab));
            list.Add(string.Format(" No. of bolts [n] = {0} ", top_n));
            list.Add(string.Format(" Class of bolt [bc] = {0} ", top_bc));






            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.6 : Ht of application of horizontal Force"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ht of application of horiz. Force = ha = kb + he + w/2"));
            list.Add(string.Format("                                       = {0} + {1} + {2}/2", kb, he, w));
            list.Add(string.Format("                                       = {0} mm", ha));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.7 : Overall height of bearing"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Overall height of bearing = h = {0} mm", h));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3 : DESIGN CHECK"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at bottom of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Concrete [fck] = M{0} ", fck));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Normal) = σ_cb_all = 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                            = 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                            = {0:f3} MPa", conc_ads_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Normal) = σ_cbc_all = 0.33 x fck"));
            list.Add(string.Format("                                              = 0.33 x {0}", fck));
            list.Add(string.Format("                                              = {0:f3} MPa ", conc_abs_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Seismic) = σ_cb_all = 1.25 x 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                             = 1.25 x 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                             = {0:f3} MPa", conc_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = σ_cbc_all = 1.25 x 0.33 x fck"));
            list.Add(string.Format("                                               = 1.25 x 0.33 x {0}", fck));
            list.Add(string.Format("                                               = {0:f3} MPa", conc_abs_seismic));
            list.Add(string.Format(""));



            list.Add(string.Format("Bottom direct Seismic = σ_cb = Nmax  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                             = {0}  x 1000/(π x {1:f3}^2 /4)", Nmax, deb));
            list.Add(string.Format("                             = {0:f3} MPa", conc_sigma_cb_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom direct Normal = σ_cb = Nnorm  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                            = {0}  x 1000/(π x {1}^2 /4)", Nnorm, deb));
            list.Add(string.Format("                            = {0:f3} MPa", conc_sigma_cb_norm));
            list.Add(string.Format(""));

            list.Add(string.Format("Bottom bending Normal = σ_cbc =  H × 1000 x ha / π x deb^3 /32", conc_sigma_cbc_norm));
            list.Add(string.Format("                                 + (Med + MRdn) / ((π x deb^3 / 32)", conc_sigma_cbc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0} × 1000 x {1} / π x {2:f3}^3 /32", H, ha, deb));
            list.Add(string.Format("                                 + ({0:f3} + {1:f3}) / ((π x {2:f3}^3 / 32)", Med, MRdn, deb));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0:f3}  MPa", conc_sigma_cbc_norm));

            list.Add(string.Format(""));
            //list.Add(string.Format("Bottom bending (Seismic) = σ_cbc   Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));
            list.Add(string.Format(""));

            double sigma_cbc1 = Hlng_s * 1000 * ha / (Math.PI * deb * deb * deb / 32.0) + (Med + MRds) / (Math.PI * deb * deb * deb / 32.0);
            double sigma_cbc2 = Hlts * 1000 * ha / (Math.PI * deb * deb * deb / 32.0);




            list.Add(string.Format("  σ_cbc1 =  Hlng_s x 1000 x ha / (π x deb^3 / 32) + (Med  + MRds) / (π x deb^3 / 32)"));
            list.Add(string.Format("         =  {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32) + ({3:f3}  + {4:f3}) / (π x {2:f3}^3 / 32)", Hlng_s, ha, deb, Med, MRds));
            list.Add(string.Format("         =  {0:f3} MPa", sigma_cbc1));
            list.Add(string.Format(""));
            list.Add(string.Format("  σ_cbc2 = Hlts x 1000 x ha / (π x deb^3 / 32)"));
            list.Add(string.Format("         = {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32)", Hlts, ha, deb));
            list.Add(string.Format("         = {0:f3} MPa", sigma_cbc2));
            list.Add(string.Format(""));



            conc_sigma_cbc_seismic = Math.Max(sigma_cbc1, sigma_cbc2);

            list.Add(string.Format("Bottom bending( Seismic) = σ_cbc =  Maximum Value of  ( {0:f3} OR {1:f3} )", sigma_cbc1, sigma_cbc2));
            list.Add(string.Format("                                 =  {0:f3} MPa ", conc_sigma_cbc_seismic));

            list.Add(string.Format(""));
            //list.Add(string.Format(" Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));

            //list.Add(string.Format("OR, Hlat_s x 1000 x ha / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));


            lamda_b_norm = (conc_sigma_cb_norm / conc_ads_norm) + (conc_sigma_cbc_norm / conc_abs_norm);

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_norm, conc_ads_norm, conc_sigma_cbc_norm, conc_abs_norm));
            list.Add(string.Format(""));

            if (lamda_b_norm <= 1)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Normal), Hence OK", lamda_b_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Normal), Hence NOT OK", lamda_b_norm));

            list.Add(string.Format(""));


            lamda_b_seismic = (conc_sigma_cb_seismic / conc_ads_seismic) + (conc_sigma_cbc_seismic / conc_abs_seismic);


            //list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all =   {0:f3} MPa (Seismic)", lamda_b_seismic));

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_seismic, conc_ads_seismic, conc_sigma_cbc_seismic, conc_abs_seismic));
            list.Add(string.Format(""));

            if (lamda_b_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_b_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_b_seismic));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at Top of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Steel = fy = {0} MPa", steel_fy));

            steel_ads_norm = 0.66 * steel_fy;

            list.Add(string.Format("Allowable  direct stress (Normal) = σ_ct_all =  0.66 * fy "));
            list.Add(string.Format("                                             =  0.66 * {0}", steel_fy));
            list.Add(string.Format("                                             =  {0:f3} MPa", steel_ads_norm));
            list.Add(string.Format(""));


            steel_abs_norm = 0.75 * steel_fy;

            list.Add(string.Format("Allowable  bending stress = σ_ctc_all =  0.75 * fy"));
            list.Add(string.Format("                                     =  0.75 * {0:f3} ", steel_fy));
            list.Add(string.Format("                                     =  {0:f3} MPa", steel_abs_norm));
            list.Add(string.Format(""));

            steel_ads_seismic = steel_ads_norm;
            steel_abs_seismic = steel_abs_norm;
            list.Add(string.Format("Allowable direct stress (Seismic) = Allowable direct stress( Normal ) = {0:f3} MPa", steel_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = Allowable bending stress( Normal ) = {0:f3} MPa", steel_abs_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_seismic = Nmax * 1000 / (Math.PI * det * det / 4);
            list.Add(string.Format("Top direct  σ_ct (Seismic)= Nmax x 1000/(π x det^2/4)"));
            list.Add(string.Format("                          = {0} x 1000/(π x {1:f3}^2/4)", Nmax, det));
            list.Add(string.Format("                          = {0:f3} MPa", steel_sigma_ct_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_norm = (Nnorm * 1000) / (Math.PI * det * det / 4.0);
            list.Add(string.Format("Top direct  σ_ct (Normal)= Nnorm x 1000/(π x det^2/4)", steel_sigma_ct_norm));
            list.Add(string.Format("                         = {0} x 1000/(π x {1:f2}^2/4)", Nnorm, det));
            list.Add(string.Format("                         = {0:f3} MPa", steel_sigma_ct_norm));
            list.Add(string.Format(""));

            steel_sigma_ctc_norm = Hlng_n * 1000 * (h - ha) / (Math.PI * det * det * det / 32.0) + (Med + MRdn) / (Math.PI * det * det * det / 32.0);

            list.Add(string.Format("Top bending normal = σ_ctc =  Hlngn x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("                              + (Med + MRdn) / (π x det^3 / 32)", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Top bending normal = σ_ctc =  {0:f3} x 1000 x ( {1} - {2} ) /(π x {3:f3}^3 / 32)", Hlng_n, h, ha, det));
            list.Add(string.Format("                              + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32)", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("                           =  {0:f3} MPa", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_ctc1 = Hlng_s * 1000 * (h - ha) / (Math.PI * det * det * det / 32) + (Med + MRdn) / (Math.PI * det * det * det / 32);
            list.Add(string.Format(" σ_ctc1 =  Hlng_s x 1000 x ( h-ha ) /(π x det^3 /32) "));
            list.Add(string.Format("           + (Med + MRdn) / (π x det^3 / 32)"));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} x 1000 x ( {1}-{2} ) /(π x {3:f3}^3 /32) ", Hlng_s, h, ha, det));
            list.Add(string.Format("           + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32))", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} MPa", sigma_ctc1));

            double sigma_ctc2 = Hlts * 1000 * (h - ha) / (Math.PI * det * det * det / 32);

            list.Add(string.Format(" σ_ctc2 = Hlts  x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("        = {0:f3}  x 1000 x ( {1}-{2} ) /(π x {2:f3}^3 / 32)", Hlts, h, ha, det));
            list.Add(string.Format("        = {0:f3} MPa", steel_sigma_ctc_seismic));

            steel_sigma_ctc_seismic = Math.Max(sigma_ctc1, sigma_ctc2);


            list.Add(string.Format("Top bending seismic = σ_ctc = Maximum Value of (σ_ctc1  OR  σ_ctc2)"));
            list.Add(string.Format("                            = Maximum Value of ({0:f3}  OR  {1:f3})", sigma_ctc1, sigma_ctc2));
            list.Add(string.Format("                            = {0:f3} MPa", steel_sigma_ctc_seismic));
            list.Add(string.Format(""));
            //list.Add(string.Format("OR"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Hlat_s  x 1000 x ( h-ha ) /(π x det^3 / 32)    =   {0:f3} MPa", steel_sigma_ctc_seismic));
            lamda_t_norm = (steel_sigma_ct_norm / steel_ads_norm) + (steel_sigma_ctc_norm / steel_abs_norm);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_norm, steel_ads_norm, steel_sigma_ctc_norm, steel_abs_norm));

            if (lamda_t_norm <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa  <= 1 (Normal), Hence OK", lamda_t_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa  > 1 (Normal), Hence NOT OK", lamda_t_norm));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            lamda_t_seismic = (steel_sigma_ct_seismic / steel_ads_seismic) + (steel_sigma_ctc_seismic / steel_abs_seismic);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all", lamda_t_seismic));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_seismic, steel_ads_seismic, steel_sigma_ctc_seismic, steel_abs_seismic));

            if (lamda_t_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_t_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_t_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.1 : PTFE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cp = Nnorm * 1000.0 / An;
            list.Add(string.Format("(a)  Contact stress = σ_cp = Nnorm x 1000 / An "));
            list.Add(string.Format("                           = {0} x 1000 / {1:f3}", Nnorm, An));
            if (sigma_cp < 40)
                list.Add(string.Format("                           = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cp));
            else
                list.Add(string.Format("                           = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cp));


            list.Add(string.Format(""));


            sigma_cpc = sigma_cp + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * dp * dp * dp / 32));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_cp + (Med + (Max of (MRdn OR MRds))/(π x dp^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_cp, Med, MRdn, MRds, dp));
            if (sigma_cpc < 45)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >  45 N/mm^2,    Hence NOT OK", sigma_cpc));

            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.2 : ELASTOMERIC DISC"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            sigma_ce = Nmax * 1000 / Ae;
            list.Add(string.Format("(a)  Compressive stress = σ_ce = Nmax x 1000 / Ae"));
            list.Add(string.Format("                               = {0:f3} x 1000 / {1:f3}", Nmax, Ae));
            if (sigma_ce <= 35)
                list.Add(string.Format("                               = {0:f3} N/mm^2  <=  35 N/mm^2,    Hence OK", sigma_ce));
            else
                list.Add(string.Format("                               = {0:f3} N/mm^2  >  35 N/mm^2,    Hence NOT OK", sigma_ce));


            list.Add(string.Format(""));

            sigma_cpc = sigma_ce + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * di * di * di / 32));

            //list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc  = σ_cp + (Med + (Max of (MRdn))/(π x di^3/32) = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cp));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_ce + (Med + (Max of (MRdn OR MRds))/(π x di^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_ce, Med, MRdn, MRds, di));

            if (sigma_cpc < 40)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cpc));


            eff = 0.5 * di * theta / he;
            list.Add(string.Format(""));
            list.Add(string.Format("(c)  Strain at the perimeter due to rotation θ rad. eff = 0.5 x di x θ / he"));
            list.Add(string.Format("                                                        = 0.5 x {0} x θ / {1}", di, theta, he));
            if (eff < 0.15)
                list.Add(string.Format("                                                        = {0:f3} < 0.15, Hence OK", eff));
            else
                list.Add(string.Format("                                                        = {0:f3} >= 0.15, Hence NOT OK", eff));


            clrns = hc - he - w - theta * (di / 2);


            list.Add(string.Format(""));
            list.Add(string.Format("(d)  Clearance between cylinder top and piston bottom = hc-he-w-θ x (di/2)"));
            list.Add(string.Format("                                                      = {0} - {1} - {2} - {3} x ({4}/2)", hc, he, w, theta, di));
            if (clrns > 5)
                list.Add(string.Format("                                                      = {0:f3} > 5 ,   Hence OK", clrns));
            else
                list.Add(string.Format("                                                      = {0:f3} <= 5 ,   Hence NOT OK", clrns));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.3 : POT CYLINDER"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Hoop's (tensile) stress  in the cross section, due to"));
            list.Add(string.Format(""));


            sigma_t1 = di * he * sigma_ce / (2 * bp * hc);
            list.Add(string.Format("(a)  Fluid pressure σ_t1 = di  x he x σ_ce / ( 2 x bp x hc)"));
            list.Add(string.Format("                         = {0}  x {1} x {2:f3} / ( 2 x {3} x {4})", di, he, sigma_ce, bp, hc));
            list.Add(string.Format("                         = {0:f3} MPa", sigma_t1));
            list.Add(string.Format(""));

            sigma_t2 = (H * 1000) / (2.0 * bp * hc);
            list.Add(string.Format("(b)  Horizontal force σ_t2 = H x 1000 / ( 2 x bp x hc )"));
            list.Add(string.Format("                           = {0} x 1000 / ( 2 x {1} x {2} )", H, bp, hc));
            list.Add(string.Format("                           = {0:f3} MPa", sigma_t2));
            list.Add(string.Format(""));

            sigma_t = sigma_t1 + sigma_t2;
            list.Add(string.Format("(c) Total stress = σ_t = σ_t1 + σ_t2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3}", sigma_t1, sigma_t2));
            list.Add(string.Format("                       = {0:f3} MPa", sigma_t));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.4 : POT INTERFACE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Shear stress in cylinder & base interface considering    1 mm orange slice, due to"));
            list.Add(string.Format(""));

            sigma_q1 = he * sigma_ce / bp;
            list.Add(string.Format("(a)  Fluid pressure, σ_q1 = he x σ_ce/bp "));
            list.Add(string.Format("                          = {0} x {1:f3}/{2} ", he, sigma_ce, bp));
            list.Add(string.Format("                          = {0:f3} MPa", sigma_q1));
            list.Add(string.Format(""));





            list.Add(string.Format("(b)  Assuming Parabolic distribution factor = 1.5, "));
            list.Add(string.Format(""));
            sigma_q2 = (1.5 * H * 1000 / di) / bp;
            list.Add(string.Format("      Horizontal Force = σ_q2 = (1.5 x H x 1000/di) / bp"));
            list.Add(string.Format("                              = (1.5 x {0} x 1000/{1}) / {2}", H, di, bp));
            list.Add(string.Format("                              = {0:f3} MPa", sigma_q2));
            list.Add(string.Format(""));
            sigma_q = sigma_q1 + sigma_q2;
            list.Add(string.Format("(c)  Total stress = σ_q = σ_q1 + σ_q2"));
            list.Add(string.Format("                        = {0:f3}  + {1:f3}", sigma_q1, sigma_q2));
            if (sigma_q < 153)
                list.Add(string.Format("                        = {0:f3} MPa  < 153 MPa Hence OK", sigma_q));
            else
                list.Add(string.Format("                        = {0:f3} MPa  > 153 MPa Hence NOT OK", sigma_q));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Beriding stress in the interface considering  1 mm orange slice"));
            list.Add(string.Format(""));

            sigma_bt1 = ((sigma_ce * he * he / 2) * 6.0) / (bp * bp);

            list.Add(string.Format("(a) Fluid  pressure = σ_bt1 = (σ_ce  x he^2 /2) x 6 / bp^2", sigma_bt1));
            list.Add(string.Format("                            = ({0:f3}  x {1}^2 /2) x 6 / {2}^2 ", sigma_ce, he, bp));
            list.Add(string.Format("                            = {0:f3} MPa", sigma_bt1));

            sigma_bt2 = ((1.5 * 1000 * H / di) * (ha - kb) * 6) / (bp * bp);
            list.Add(string.Format(""));

            list.Add(string.Format("(b) Horizontal force = σ_bt2 = ( 1.5 x 1000 x H/di) x ( ha-kb) x 6 / bp^2", sigma_bt2));
            list.Add(string.Format("                             = ( 1.5 x 1000 x {0}/{1}) x ( {2}-{3}) x 6 / {4}^2", H, di, ha, kb, bp));
            list.Add(string.Format("                             = {0:f3} MPa", sigma_bt2));

            sigma_bt = sigma_bt1 + sigma_bt2;
            list.Add(string.Format(""));

            list.Add(string.Format("(c) Totai Stress  σ_bt = σ_bt1  + σ_bt2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3} ", sigma_bt1, sigma_bt2));
            if (sigma_bt < 224.4)
                list.Add(string.Format("                       = {0:f3} MPa  < 224.4  MPa,   Hence OK", sigma_bt));
            else
                list.Add(string.Format("                       = {0:f3} MPa  >= 224.4  MPa,   Hence NOT OK", sigma_bt));

            list.Add(string.Format(""));
            sigma_e = Math.Sqrt((sigma_bt * sigma_bt + 3 * sigma_q * sigma_q));
            list.Add(string.Format("(iii) Combined stress  σ_e  = √(σ_bt^2 + 3 × σ_q^2) "));
            list.Add(string.Format("                            = √({0:f3}^2 + 3 × {1:f3}^2)", sigma_bt, sigma_q));

            if (sigma_e < 306.0)
                list.Add(string.Format("                            = {0:f3} MPa < 306 MPa   Hence OK", sigma_e));
            else
                list.Add(string.Format("                            = {0:f3} MPa > 306 MPa   Hence NOT OK", sigma_e));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.5 : Eff.cont.width at piston-cylinder interface"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            double sigma_p = 254.84;
            list.Add(string.Format("σ_p = {0:f3} MPa", sigma_p));

            we = (H * 1000 * 1.3) / (di * sigma_p);
            list.Add(string.Format("Eff.cont.width at piston-cylinder interface = we = H x 1000 x 1.3 / (di x σ_e)"));
            list.Add(string.Format("                                                 = {0} x 1000 x 1.3 / ({1} x {2:f3})", H, di, sigma_e));
            if (we < w)
                list.Add(string.Format("                                                 = {0:f4} mm  <  {1} (w)     Hence OK", we, w));
            else
                list.Add(string.Format("                                                 = {0:f4} mm  >=   {1} (w)     Hence NOT OK", we, w));

            list.Add(string.Format(""));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format("STEP 3.6 : GUIDE "));
            //list.Add(string.Format("-------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //sigma_qu = (H * 1000) / (Lu * ku);
            //list.Add(string.Format("(i)  Shear stress  σ_qu = H x 1000 / ( Lu x ku ) "));
            //list.Add(string.Format("                        = {0} x 1000 / ( {1} x {2} )", H, Lu, ku));
            //if (sigma_qu < 153)
            //    list.Add(string.Format("                        = {0:f4} MPa   <   153 MPa, Hence OK", sigma_qu));
            //else
            //    list.Add(string.Format("                        = {0:f4} MPa   >=   153 MPa, Hence NOT OK", sigma_qu));



            //list.Add(string.Format(""));





            //sigma_bu = H * 1000 * 6 * (hts / 2 + 8) / (Lu * ku * ku);

            //list.Add(string.Format("(ii)  Bending Stress = σ_bu = H x 1000 x 6 x ( hts / 2+8) / ( Lu x ku^2 )"));
            //list.Add(string.Format("                            = {0} x 1000 x 6 x ( {1} / 2+8) / ( {2} x {3}^2 )", H, hts, Lu, ku));

            //if (sigma_bu < 224.4)
            //    list.Add(string.Format("                            = {0:f4} MPa   <   224.4 MPa, Hence OK", sigma_bu));
            //else
            //    list.Add(string.Format("                            = {0:f4} MPa   >=   224.4 MPa, Hence NOT OK", sigma_bu));

            //list.Add(string.Format(""));

            //sigma_eu = Math.Sqrt(sigma_bu * sigma_bu + 3 * sigma_qu * sigma_qu);

            //list.Add(string.Format("(iii) Combined stress = σ_eu = √(σ_bu^2 + 3 x σ_qu^2)"));
            //list.Add(string.Format("(                            = √({0:f3}^2 + 3 x {1:f3}^2)", sigma_bu, sigma_qu));
            //if (sigma_eu < 306.0)
            //    list.Add(string.Format("(                            = {0:f4} MPa   <   306 MPa ", sigma_eu));
            //else
            //    list.Add(string.Format("(                            = {0:f4} MPa   >=   306 MPa ", sigma_eu));

            //sigma_du = H * 1000 / (hts * (Lu - 10));
            //list.Add(string.Format(""));

            //list.Add(string.Format("(iv)  Direct stress on SS = σ_du = H × 1000/(hts x (Lu-10)) "));
            //list.Add(string.Format("                                 = {0} × 1000/({1} x ({2}-10)) ", H, hts, Lu));
            //if (sigma_du < 200)
            //    list.Add(string.Format("                                 = {0:f4} MPa < 200 MPa ", sigma_du));
            //else
            //    list.Add(string.Format("                                 = {0:f4} MPa >= 200 MPa ", sigma_du));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.6 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(a) Anchor Screws"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            bottom_Hperm = ((bottom_n * bottom_Ab * bottom_sigma_bq_perm) / 1000) + (Nmin * bottom_mu);

            //list.Add(string.Format("Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)] > H = Maximum Horizontal force on bearing."));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3}", bottom_mu));
            list.Add(string.Format("σ_bq_perm  = Permissible  shear stress  in  bolt = {0:f3}  MPa", bottom_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maximum Horizontal force on bearing"));
            list.Add(string.Format(""));
            //list.Add(string.Format("The total resistive force = Hperm = {0:f3} kN > H , OK ", bottom_Hperm));


            list.Add(string.Format("The total resistive force = Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)]"));
            list.Add(string.Format("                                  = [({0} x {1:f3} x {2:f3}) / 1000)+({3:f3} x {4:f3})]", bottom_n, bottom_Ab, bottom_sigma_bq_perm, Nmin, bottom_mu));

            if (bottom_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H),   Hence OK", bottom_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H),   Hence NOT OK", bottom_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format("Top :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Anchor Screws"));
            //list.Add(string.Format("-------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Bottom :"));
            list.Add(string.Format(""));

            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            top_Hperm = ((top_n * top_Ab * top_sigma_bq_perm) / 1000);
            //list.Add(string.Format("Hperm = ((n x Ab x σ_bq_perm) / 1000) > H, Maximum Horizontal force on bearing."));
            list.Add(string.Format(""));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3} ", top_mu));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_bq,perm  = Permissible shear stress in bolt = {0:f3} MPa", top_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maxm. Horizontal force on bearing"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force = Hperm = (n x Ab x σ_bq_perm) / 1000", top_Hperm));
            list.Add(string.Format("                                  = ({0} x {1:f3} x {2:f3}) / 1000 ", top_n, top_Ab, top_sigma_bq_perm));
            if (top_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H) , OK", top_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H)  , NOT OK", top_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b)  Anchor Sleeves"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Screws."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cav = (bottom_Ab * bottom_sigma_bq_perm) / (bottom_L * bottom_D);
            list.Add(string.Format("( i ) Avg. bearing stress in conc. σ_cav. = (Ab x σ_bq_perm) / (L x D ) "));
            list.Add(string.Format("                                          = ({0:f3} x {1:f3}) / ({2:f3} x {3:f3} )", bottom_Ab, bottom_sigma_bq_perm, bottom_L, bottom_D));
            list.Add(string.Format("                                          = {0:f3} MPa", sigma_cav));
            list.Add(string.Format(""));


            double sigma_ctr = 2 * sigma_cav;

            list.Add(string.Format("( ii) Considering traingular distribution, bending stress in conc = σ_ctr = 2 x σ_cav"));
            list.Add(string.Format("                                                                          = 2 x {0:f3}", sigma_cav));
            list.Add(string.Format("                                                                          = {0:f3} MPa", sigma_ctr));
            list.Add("");
            double sigma_cpk = 1.5 * sigma_ctr;
            list.Add(string.Format("(iii) Peak stress = σ_cpk = 1.5 x σ_ctr"));
            list.Add(string.Format("                          = 1.5 x {0:f3}", sigma_ctr));
            if (sigma_cpk < 18.56)
                list.Add(string.Format("                          = {0:f3} MPa  < 18.56 MPa, Hence OK", sigma_cpk));
            else
                list.Add(string.Format("                          = {0:f3} MPa  >= 18.56 MPa, Hence NOT OK", sigma_cpk));
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");

            #region Sample


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CONTRACTOR : M/s. SEW INFRASTRUCTURE                       LTD.                                   2/25/201316:40"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                   DESIGN       OF Steflomet POT/PTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("1     REFERENCE:"));
            //list.Add(string.Format("       Drawing                                                                  :3..P14VMT Sp¢     Rev-0      REFERENCE :                                         IRC:83(Part-W)"));
            //list.Add(string.Format("       Project :For four laning of krishnagar Baharampour section of NH-34 from km 115.000 to km 193.000 West Barigal."));
            //list.Add(string.Format("       For ROB at Chanage;  133+432+501                      Quantity -4+4 nos.                 Location: A1&A4                                    Type - Transverselý Guided."));

            //list.Add(string.Format("       Normal Vertical Load, Nnorm                              : 1,334.00                   kN         Trans. Horz. Force  Hlts (Seismic)           0.00                 kN"));




            //list.Add(string.Format("       Dead load,  Nmin                                         :   332.00                      kN         Long Horz Force( Normal ) Hlng_n                  65.00                kN"));
            //list.Add(string.Format("       Rotation due to Dead Load. θp                            :     0.0065                     rad          Long Harz. Force, (Seismic) Hlng_s          174.00              kN"));
            //list.Add(string.Format("       Rotation due to Live Load, θy                            :     0.0015                      rad         Design horz. Force, H(For bearing            174.00              kN"));
            //list.Add(string.Format("       Rotation, e                                              : 0.010                        rad         components only)"));
            //list.Add(string.Format("       Movement, elong(±)                                               O                              mm        Movement, etrans.(±)                              10.0                 mm"));


            //list.Add(string.Format("             3      BEARINGPARAMETERS"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("      1.   Steel components to conform IS:2062(MS), IS: 1030 Gr.340-570 W(CS) Therefore, Yield Stress, fy 340 MPa"));

            //list.Add(string.Format("            Dia of Elastomeric pad, di                               :  240                         mm         Thickness of the Pad ,he                           16                   mm"));
            //list.Add(string.Format("            Area of Pad. Ae  = π x di2 / 4                       .   : 4.52E+04                 mm2       Hardness  IRHD ( Shore-A)                      50 ± 5"));
            //list.Add(string.Format("            Value of Constant, k1                                     :  2.2                                          Value of Constant, k2                               101"));
            //list.Add(string.Format("            Moment due to resistance to Rotation.                                        Med = di' X ( k, X Op + k2 X 0,)                                         2.29E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Normal)..                             MRdn = 0.2 X ( di / 2) X He                                             1.56E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Transverse Seismic),               MRst  = 0.2 X ( d  / 2) X Hlts                                .             0.00E+00  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Longitudinal Seismic),            MRsl  = 0.2  X ( di / 2) X H  .                                             4.18E+06  N-mm"));
            //list.Add(string.Format("       3.  SADDLE"));
            //list.Add(string.Format("            Dia of PTFE(dp)                                             :  240                          mm        Contact with piston,w                              6                       mm"));
            //list.Add(string.Format("            Thickness of Saddle plate,  hm                        :  12                            mm        Eff. Plan area of PTFE,An                       45238.93         mm®"));
            //list.Add(string.Format("            Height of SS on saddle,hts                               :  12                            mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("       4.  POT CYLINDER"));
            //list.Add(string.Format("            Dia / sq of base plate,  Do                                :  310                          mm        Thkness of base plate,kb                          15                     mm"));
            //list.Add(string.Format("            Outer dia of cylinder, do                                  :  294                          mm        Depth of cyfinder,hc                                 32                   mm"));
            //list.Add(string.Format("            Effective dia at the bottom, deb                     .  300                          mm        Width of cylinder wall                                27                    mm"));
            //list.Add(string.Format("            di + 2 x 2 x kb                                                                                                   bp=(do-di)/2"));
            //list.Add(string.Format("            Available diameter (deb,avibl) at the substructure, geometrically    simifar to, deb =                                                          650                   mm"));
            //list.Add(string.Format("       5.   TOP ASSEMBLY"));
            //list.Add(string.Format("             Length of top piate, a.                                   :  400                           mm       Width of top plate,b                                  500                  mm"));
            //list.Add(string.Format("             Thk.ness of SS Plate(ks)                                 : 3                               mm        Thickness of top plate, kt                        14                     mm"));
            //list.Add(string.Format("             Length of SS plate, as                                     :  320                          mm        Width of SS plate,  bs                                300                  mm"));
            //list.Add(string.Format("             Thk. of guide, ku                                            :  20                            mm        Effective Length of guide,Lu                     240                 mm"));
            //list.Add(string.Format("             Effective dia of the dispersed  area at the top structure, det  = Min((dp+2 x 2x(kt+ks)) & b]                                           308                  mm"));
            //list.Add(string.Format("             Available diameter (det,avibl)  at the substructure, geometrically similar to,  det =                                                           650                   mm"));
            //list.Add(string.Format("       6.   ANCHOR"));
            //list.Add(string.Format("             Bottom:"));
            //list.Add(string.Format("             Bolt size( IS:1363) Mj                              :        16                            mm         Root area of bolt Ab                                167                   mm2"));
            //list.Add(string.Format("             No. off boits(n)                                        :        4                                            Class of bolt, bc                                10.9"));
            //list.Add(string.Format("             Dia of Sleeve, D                                       :        40                              mm       Length of sleeve , L                                 170                   mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             TOP :"));
            //list.Add(string.Format("             Size (1S:1363) Mj                                     :        20                                          Root area of bolt,Ab                                 272                  mm2"));
            //list.Add(string.Format("             No. of bolts(n)                                         :        4                                             Class of bolt(tS:1367)                               10.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        7.   Ht of applicat5on of horiz. Force(ha)= kb + he + w/2                                                                                                      34                    mm"));
            //list.Add(string.Format("        8.   Overall height of bearing (h) mm                                                                                                                                    88                     mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(" 4     DES1GN CHECK :"));
            //list.Add(string.Format("        Structure at bottom of Bearing :                              Concrete                                Grade of Concrete    , fck                          M45"));
            //list.Add(string.Format("        Allowable direct stress(Normal)                              0.25 fck X ((debavi) / (deb))                                                                    22.50              MPa"));
            //list.Add(string.Format("        Allowable bending stress(Normal)                           0.33 fck                                                                                                  14.85               MPa"));
            //list.Add(string.Format("        Allowable direct stress ( Seismic)                             1.25 x 0.25 fck X ((debavl) / (deb))                                                        28.13               MPa"));
            //list.Add(string.Format("        Allowabie bending stress ( Seismic)                          1.25 x 0.33 fck.                                                                                      18.56              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Seismic)                                   N max  x 1000!(x(deb) /4) MPa =                                                           19.81              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Normal)                                   N norm. x 1000/(x(deb)2/4) MPa =                                                        18.87               MPa"));
            //list.Add(string.Format("        Bottom bending( Normal) σ_cbc = Hn × 1000 x ha / x.(deb)3 /32 + (Med + Mw) / (u deb' / 32)  MPa =                         -        2.29                 MPa"));
            //list.Add(string.Format("        Bottom bending( Seismic) σ_cbc,Max, Hing,s   x 1000 x ha / (x deb' / 32)   + (Ma  + Man) / (n deb3 / 32) MPa ="));
            //list.Add(string.Format("        OR, Hlat,s x 1000 x ha / (x deb' / 32)   MPa =                                                                                                                                      4.67  MPa"));
            //list.Add(string.Format("        λb=acb/acb,all      +acbc /acbc,all=                                                0.993                 MPa<=1(Normal)                                     Hence  OK"));
            //list.Add(string.Format("        λb = acb / ocb,all +  acbc / acbc,all   =                                          0.956                 MPa<=1( Seism c)                                    Hence  OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                       CONTRACTOR             :    M/s. SEW             INFRASTRUCTURE                      LTD.                                  2/25/201316:40"));
            //list.Add(string.Format("                DESIGN OF Steflomet POTIPTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format("   Structure at Top of Bearing :                                  STEEL to IS :  2062               fy.=                                                                         230  MPa"));
            //list.Add(string.Format("   Allowable  direct stress( Normal )=                         0.66 * fy                                                                                                151.80            MPa"));
            //list.Add(string.Format("   Arlowable bending stress (Normal)=                     0.75 * fy                                                                                               172.5               MPa"));
            //list.Add(string.Format("   Allowable direct stress( Seismic )= Allowable direct stress( Normal )                                                           151.80             MPa"));
            //list.Add(string.Format("                     .                     Allowable bending  stress ( Seismic)   =                    Allowable bending stress( Normal )                                                        172.50"));
            //list.Add(string.Format("   Top directµct(Seismic)= Nmaxx1000/(udet2/4)N/mm2;                                                                                                        18.79              MPa"));
            //list.Add(string.Format("   Topdirect,act(Normal)=  N x1000 / (udet2/4)N/mm2  =                                                                                                       17.90              MPa"));
            //list.Add(string.Format("   Top bending normal,acic =  Hn x 1000 x ( h-ha ) /(x.det3 / 32) + (M,ø + Mpø) / (x det® / 32) MPa =                                 2.57                MPa"));
            //list.Add(string.Format("   Top bending seismic,actc =Max, Hlng,s   x 1000 x ( h-ha ) /(n.det' / 32) + (Me + Maø) / (2 det' / 32) MPa ="));
            //list.Add(string.Format("   OR, Hlat,s  x 1000 x ( h-ha ) /(x.det3 / 32) MPa =                                                                                                                              3.28  MPa"));
            //list.Add(string.Format("   at = act / act,ali + actc / actc.all  =                                                              0.133  MPa<=1(Normal)                                    Hence OK"));
            //list.Add(string.Format("   At = act / oct,ali + actc / ocic,all =                                                              0.143 MPa<=1( Seismic)                                    Hence  OK"));
            //list.Add(string.Format("2 PTFE :"));
            //list.Add(string.Format("   (a)  Contdct stress = acp  = N x 1000 / An =                                 29.49                N/mm2                                                    < 40 N/mm^2       Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Stress, acpc  = göp+(Me,d+(Max of (Msg))/(xXdp'/32)=          34.25                                                       < 45 N/mm^2       Hence  OK"));
            //list.Add(string.Format("3  ELASTOMERIC DISC :"));
            //list.Add(string.Format("   (a)  Compressive stress,ace    = Nmax x 1000 / Ae :                    29.49                 MPa                                                        <=35 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Strëss, acpc   = ace+(M,,a+(Max of (MR.d))/(xXdí3/32)=       34.25                                                       <=40 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (c)  Strain at the perimeter due to rotation u rad. sp = 0.5 x di x u / he, eff =                                                                0.08 < 0.15        Hence   OK"));
            //list.Add(string.Format("   (d)  Clearance between cylinder top and piston bottom= bc-he-w-u.(di/2)    =                                                               8.75 >5 mm           Hence   OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4  POT CYL1NDER:"));
            //list.Add(string.Format("   (i)  Hoop's (tensile) stress  in the cross section, due to"));
            //list.Add(string.Format("   (a)  Fluid pressure at1=di  x he x ace / ( 2 x bp x hc) =                                                                                                          65.53              MPa"));
            //list.Add(string.Format("   (b)  Horizontal force at2 = H ×1000 x / ( 2 x bp x bc ) =                                                                                                     100.69.            MPa"));
            //list.Add(string.Format("   (c) Total stress = at1 + at2  =                                                    166.22 MPa                                                                  <= 204                 MPa"));
            //list.Add(string.Format("5  POT INTERFACE :"));
            //list.Add(string.Format("   (i)  Shear stress in cylinder & base interface considering    1mm orange slice, due to"));
            //list.Add(string.Format("  (a)  Fluid pressure, aq1= he    x ace/bp =                                                                                                                                17.47              MPa"));
            //list.Add(string.Format("   (b) Assuming Parabolic distribution factor =1.5, Horizontal Force aq2  = (1.5 x H x 1000/di) / bp =                                    40.28               MPa"));
            //list.Add(string.Format("   (c) Total stress og = agi   + aq2  =                                                              57.75 MPa                                                         153                 MPa   Hence  OK"));
            //list.Add(string.Format("   (ii) Beriding stress in the interface considering  1 mm orange slice"));
            //list.Add(string.Format("  (a)  Fluid  pressure, obt1 = (ace  x he2 /2) x 6 / bp2=                                 31.065 MPa"));
            //list.Add(string.Format("   (b) Horizontal force,abt2=( 1.5 x 1000 x H/di) x ( ha.kb) x 6 / bp2=       170.06  MPa"));
            //list.Add(string.Format("   (c) Totai Stress obt = abt1  + abt2 =                                                         201.13 MPa                                                                     224.4  MPa   Hence OK"));
            //list.Add(string.Format("   (iii) Combined stress  ,ae  = V( abt2 , 3×,92) =                                           224.6  MPa                                                                        306 MPa   Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             6 Eff.cont.width at piston-cylinder interface,we = Hx1000x1.3/(dixap)=    3.6961 mm                                                          < w    Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             7 GUIDE :"));
            //list.Add(string.Format("  (i)   Shear stress, aqu   = H x 1000 / ( Lu x ku ) =                                        36.25 MPa                                                                        153  MPa   Hence OK"));
            //list.Add(string.Format("  (ii) Bending Stress,abu    = H x 1000 x 6 x ( his / 2+8) / ( Lu x ku2 ) =      184.88 MPa                                                                     224.4  MPa  Hence  OK"));
            //list.Add(string.Format("  (iii) Combined stress,  aeu  = %( abu2 + 3 x aqu2) =                                  195.25  MPa                                                                       306  MPa   Hence  OK"));
            //list.Add(string.Format("  (iv) DirectstressonSS,adu=H×1000/(htsx(Lu-10))=                                    60.42  MPa                                                                        200 MPa   HenceOK"));
            //list.Add(string.Format("             8 ANCHOR :"));
            //list.Add(string.Format("   ( a) Anchor Screws"));
            //list.Add(string.Format("   Bottom:"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000)+(Nmin x µ)) > H. Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bottom of bearing, Coefficient of friction, µ =                                     0.2"));
            //list.Add(string.Format("         σ_bqiperm  = Permissible  shear stress  in  bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        Thetotalresistiveforce            Hperm=                                 223.3BkN>H         OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        Anchor Screws"));
            //list.Add(string.Format("        Bottom :"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000] > H, Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bóttom of bearing, Coefficient of friction, µ=                                      0.2"));
            //list.Add(string.Format("         abq,perm  = Permissible shear stress    in bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        The totalresistiveforce           Hperm=                                 255.68 kN>  H        OK"));
            //list.Add(string.Format("   (b)  Anchor Sleeves"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("         Bottom Screws."));
            //list.Add(string.Format(""));
            //list.Add(string.Format("( i ) Avg. bearing stress in conc. acav. =[Ab x abq,perm] / (  L x D ) =                                                                                          5.77  MPa"));
            //list.Add(string.Format("( ii) Considering traingular distribution , bending  stress  in conc, Sctr  = 2 x scav =                    .                                                 11.54 MPa"));
            //list.Add(string.Format("(iii) Peak stress, acpk=1.5xactr=                                                                                                                                                17.31  MPa"));
            //list.Add(string.Format("                                                                                                                                                                                  18.56 MPa  Hence   OK"));

            #endregion Sample

            File.WriteAllLines(Report_File, list.ToArray());
        }
        public void Generate_Report_VERSO_MONO_AXIAL_BEARING_LONGITUDINAL()
        {

            List<string> list = new List<string>();
            #region TechSOFT Banner

            list.Add(string.Format(""));
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 20.0          *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*        DESIGN OF STEFLOMET  POT/PTFE       *");
            list.Add("\t\t*    VERSO-MONO AXIAL LONGITUDINAL BEARING   *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #region Step 1 :  DESIGN PARAMETERS
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 1 :  DESIGN PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Vertical load [Nmax] = {0} kN", Nmax));
            list.Add(string.Format("Transverse Horizontal Force Normal [Hlatn]= {0} kN", Hlatn));

            list.Add(string.Format("Normal Vertical Load [Nnorm] = {0} kN", Nnorm));
            list.Add(string.Format("Transverse Horzontal Force Seismic [Hlts] = {0} kN", Hlts));

            list.Add(string.Format("Dead load [Nmin] = {0:f3} kN", Nmin));
            list.Add(string.Format("Long Horizontal Force Normal [Hlng_n] = {0} kN", Hlng_n));
            list.Add(string.Format("Rotation due to Dead Load [θp] = {0} rad", theta_p));
            list.Add(string.Format("Long Horizontal Force Seismic [Hlng_s] = {0} kN", Hlng_s));
            list.Add(string.Format("Rotation due to Live Load [θy] = {0} rad", theta_v));
            list.Add(string.Format("Design Horizontal Force For bearing components only [H] = {0} rad", H));


            list.Add(string.Format("Rotation [θ] = {0} rad", theta));

            list.Add(string.Format("Long Movement [elong](±)  = {0} mm", elong));
            list.Add(string.Format("Transverse Movement [etrans](±)  = {0} mm", etrans));

            list.Add(string.Format(""));

            #endregion Step 1 :  DESIGN PARAMETERS

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2 :  BEARING PARAMETERS "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));


            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.1 : ELASTOMER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Steel Yield Stress [fy] = {0} MPa", fy));
            list.Add(string.Format("Dia of Elastomeric pad [di] = {0} mm", di));
            list.Add(string.Format("Thickness of the Pad [he] = {0} mm", he));
            //list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4 = {0:f4} mm^2", Ae));

            list.Add(string.Format(""));
            list.Add(string.Format("Area of Pad [Ae] = (π x di^2) / 4"));
            list.Add(string.Format("                 = (π x {0}^2) / 4", di));
            list.Add(string.Format("                 = {0:f4} mm^2", Ae));
            list.Add(string.Format(""));
            list.Add(string.Format("Hardness IRHD ( Shore-A) = {0} ± 5", IRHD));

            list.Add(string.Format("Value of Constant [k1] = {0} ", k1));
            list.Add(string.Format("Value of Constant [k2] = {0} ", k2));

            list.Add(string.Format("Moment due to resistance to Rotation = Med = di^3 x ( k1 X θp + k2 X θv)"));
            list.Add(string.Format("                                           = {0}^3 x ( {1} X {2} + {3} X {4})", di, k1, theta_p, k2, theta_v));
            list.Add(string.Format("                                           = {0:f3} N-mm", Med));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Normal) = MRdn = 0.2 X ( di / 2) X Hlng_n*1000"));
            list.Add(string.Format("                                                    = 0.2 X ( {0} / 2) X {1}*1000", di, Hlng_n));
            list.Add(string.Format("                                                    = {0:f3} N-mm", MRdn));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Transverse Seismic) = MRst = 0.2 X (di / 2) X Hlts*1000"));
            list.Add(string.Format("                                                                = 0.2 X ({0} / 2) X {1}*1000", di, Hlts));
            list.Add(string.Format("                                                                = {0:f3} N-mm", MRst));
            list.Add(string.Format(""));
            list.Add(string.Format("Moment due to frictional resistance (Longitudinal Seismic) = MRsl =  0.2  X ( di / 2) X Hlng_s*1000"));
            list.Add(string.Format("                                                                  =  0.2  X ( {0} / 2) X {1}*1000", di, Hlng_s));
            list.Add(string.Format("                                                                  =  {0:f3} N-mm", MRsl));
            list.Add(string.Format(""));

            double MRds = Math.Max(MRst, MRsl);
            list.Add(string.Format("Moment due to frictional resistance (Seismic) = MRds = Max ( MRst OR MRsl )"));
            list.Add(string.Format("                                                     = Max ( {0:f3} OR {1:f3} )", MRst, MRsl));
            list.Add(string.Format("                                                     = {0:f3} N-mm", MRds));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.2 : SADDLE "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia of PTFE [dp] = {0} mm", dp));
            list.Add(string.Format("Contact with piston [w] = {0} mm", w));
            list.Add(string.Format("Thickness of Saddle plate [hm] = {0} mm", hm));
            list.Add(string.Format("Eff. Plan area of PTFE [An] = (π x dp^2) / 4 =  {0} mm^2", An));
            list.Add(string.Format("Height of SS on saddle [hts] = {0} mm", hts));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.3 : POT CYLINDER "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Dia / sq of base plate [Do] = {0} mm", Do_));
            list.Add(string.Format("Thkness of base plate [kb] = {0} mm", kb));
            list.Add(string.Format("Outer dia of cylinder [do] = {0} mm", do_));
            list.Add(string.Format("Depth of cyfinder [hc] = {0} mm", hc));
            list.Add(string.Format("Effective dia at the bottom [deb] = di + 2 x 2 x kb"));
            list.Add(string.Format("                                  = {0} + 2 x 2 x {1}", di, kb));
            list.Add(string.Format("                                  = {0:f2} mm", deb));
            list.Add(string.Format(""));
            list.Add(string.Format("Width of cylinder wall [bp] = (do-di)/2 = ({0} - {1})/2 = {2} mm", do_, di, bp));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically simifar to deb = deb_avibl = {0} mm", deb_avibl));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.4 : TOP ASSEMBLY "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Length of top piate [a] = {0} mm", a));
            list.Add(string.Format("Width of top plate [b] = {0} mm", b));
            list.Add(string.Format("Thickness of SS Plate [ks] = {0} mm", ks));
            list.Add(string.Format("Thickness of top plate [kt] = {0} mm", kt));
            list.Add(string.Format("Length of SS plate [as] = {0} mm", as_));
            list.Add(string.Format("Width of SS plate [bs] = {0} mm", bs));
            list.Add(string.Format("Thickness of guide [ku] = {0} mm", ku));
            list.Add(string.Format("Effective Length of guide [Lu] = {0} mm", Lu));
            list.Add(string.Format("Effective dia of the dispersed area at the top structure = det "));
            list.Add(string.Format(""));

            list.Add(string.Format("   det = Minimum Value of ((dp+2 x 2x(kt+ks)) & b)"));

            double val1 = 0;
            val1 = (dp + 2 * 2 * (kt + ks));



            list.Add(string.Format("       = Minimum Value of (({0}+2 x 2x({1}+{2})) & {3})", dp, kt, ks, b));
            list.Add(string.Format("       = Minimum Value of ({0:f2} & {1:f2})", val1, b));
            list.Add(string.Format(""));
            list.Add(string.Format("       = {0:f3} mm", det));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Available diameter at the substructure, geometrically similar to [det_avibl] = {0} mm", det_avibl));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.5 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("BOTTOM"));
            list.Add(string.Format("------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bolt size [Mj] = {0} mm", bottom_Mj));
            list.Add(string.Format("Root area of bolt [Ab] = {0} mm^2", bottom_Ab));
            list.Add(string.Format("No. off boits [n] = {0} ", bottom_n));
            list.Add(string.Format("Class of bolt [bc] = {0} ", bottom_bc));
            list.Add(string.Format("Dia of Sleeve [D] = {0} mm", bottom_D));
            list.Add(string.Format("Length of sleeve [L] = {0} mm", bottom_L));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TOP"));
            list.Add(string.Format("---"));
            list.Add(string.Format(""));

            list.Add(string.Format(" Bolt Size [Mj] = {0} mm", top_Mj));
            list.Add(string.Format(" Root area of bolt [Ab] = {0} mm^2", top_Ab));
            list.Add(string.Format(" No. of bolts [n] = {0} ", top_n));
            list.Add(string.Format(" Class of bolt [bc] = {0} ", top_bc));






            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.6 : Ht of application of horizontal Force"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ht of application of horiz. Force = ha = kb + he + w/2"));
            list.Add(string.Format("                                       = {0} + {1} + {2}/2", kb, he, w));
            list.Add(string.Format("                                       = {0} mm", ha));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2.7 : Overall height of bearing"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Overall height of bearing = h = {0} mm", h));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3 : DESIGN CHECK"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at bottom of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Concrete [fck] = M{0} ", fck));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Normal) = σ_cb_all = 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                            = 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                            = {0:f3} MPa", conc_ads_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Normal) = σ_cbc_all = 0.33 x fck"));
            list.Add(string.Format("                                              = 0.33 x {0}", fck));
            list.Add(string.Format("                                              = {0:f3} MPa ", conc_abs_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable direct stress (Seismic) = σ_cb_all = 1.25 x 0.25 x fck x ((deb_avibl) / (deb))"));
            list.Add(string.Format("                                             = 1.25 x 0.25 x {0} x (({1:f3}) / ({2:f3}))", fck, deb_avibl, deb));
            list.Add(string.Format("                                             = {0:f3} MPa", conc_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = σ_cbc_all = 1.25 x 0.33 x fck"));
            list.Add(string.Format("                                               = 1.25 x 0.33 x {0}", fck));
            list.Add(string.Format("                                               = {0:f3} MPa", conc_abs_seismic));
            list.Add(string.Format(""));



            list.Add(string.Format("Bottom direct Seismic = σ_cb = Nmax  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                             = {0}  x 1000/(π x {1:f3}^2 /4)", Nmax, deb));
            list.Add(string.Format("                             = {0:f3} MPa", conc_sigma_cb_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom direct Normal = σ_cb = Nnorm  x 1000/(π x deb^2 /4)"));
            list.Add(string.Format("                            = {0}  x 1000/(π x {1}^2 /4)", Nnorm, deb));
            list.Add(string.Format("                            = {0:f3} MPa", conc_sigma_cb_norm));
            list.Add(string.Format(""));

            list.Add(string.Format("Bottom bending Normal = σ_cbc =  Hlngn × 1000 x ha / π x deb^3 /32", conc_sigma_cbc_norm));
            list.Add(string.Format("                                 + (Med + MRdn) / ((π x deb^3 / 32)", conc_sigma_cbc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0} × 1000 x {1} / π x {2:f3}^3 /32", Hlng_n, ha, deb));
            list.Add(string.Format("                                 + ({0:f3} + {1:f3}) / ((π x {2:f3}^3 / 32)", Med, MRdn, deb));
            list.Add(string.Format(""));
            list.Add(string.Format("                              =  {0:f3}  MPa", conc_sigma_cbc_norm));

            list.Add(string.Format(""));
            //list.Add(string.Format("Bottom bending (Seismic) = σ_cbc   Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));
            list.Add(string.Format(""));

            double sigma_cbc1 = Hlng_s * 1000 * ha / (Math.PI * deb * deb * deb / 32.0) + (Med + MRds) / (Math.PI * deb * deb * deb / 32.0);
            double sigma_cbc2 = Hlts * 1000 * ha / (Math.PI * deb * deb * deb / 32.0);




            list.Add(string.Format("  σ_cbc1 =  Hlng_s x 1000 x ha / (π x deb^3 / 32) + (Med  + MRds) / (π x deb^3 / 32)"));
            list.Add(string.Format("         =  {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32) + ({3:f3}  + {4:f3}) / (π x {2:f3}^3 / 32)", Hlng_s, ha, deb, Med, MRds));
            list.Add(string.Format("         =  {0:f3} MPa", sigma_cbc1));
            list.Add(string.Format(""));
            list.Add(string.Format("  σ_cbc2 = Hlts x 1000 x ha / (π x deb^3 / 32)"));
            list.Add(string.Format("         = {0:f3} x 1000 x {1} / (π x {2:f3}^3 / 32)", Hlts, ha, deb));
            list.Add(string.Format("         = {0:f3} MPa", sigma_cbc2));
            list.Add(string.Format(""));



            conc_sigma_cbc_seismic = Math.Max(sigma_cbc1, sigma_cbc2);

            list.Add(string.Format("Bottom bending( Seismic) = σ_cbc =  Maximum Value of  ( {0:f3} OR {1:f3} )", sigma_cbc1, sigma_cbc2));
            list.Add(string.Format("                                 =  {0:f3} MPa ", conc_sigma_cbc_seismic));

            list.Add(string.Format(""));
            //list.Add(string.Format(" Hlng_s x 1000 x ha / (π x deb^3 / 32)   + (Med  + Mrd) / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));

            //list.Add(string.Format("OR, Hlat_s x 1000 x ha / (π x deb^3 / 32) = {0:f3} MPa", conc_sigma_cbc_seismic));


            lamda_b_norm = (conc_sigma_cb_norm / conc_ads_norm) + (conc_sigma_cbc_norm / conc_abs_norm);

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_norm, conc_ads_norm, conc_sigma_cbc_norm, conc_abs_norm));
            list.Add(string.Format(""));

            if (lamda_b_norm <= 1)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Normal), Hence OK", lamda_b_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Normal), Hence NOT OK", lamda_b_norm));

            list.Add(string.Format(""));


            lamda_b_seismic = (conc_sigma_cb_seismic / conc_ads_seismic) + (conc_sigma_cbc_seismic / conc_abs_seismic);


            //list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all =   {0:f3} MPa (Seismic)", lamda_b_seismic));

            list.Add(string.Format(" λb = σ_cb/σ_cb_all + σ_cbc /σ_cbc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", conc_sigma_cb_seismic, conc_ads_seismic, conc_sigma_cbc_seismic, conc_abs_seismic));
            list.Add(string.Format(""));

            if (lamda_b_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_b_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_b_seismic));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Structure at Top of Bearing "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Grade of Steel = fy = {0} MPa", steel_fy));

            steel_ads_norm = 0.66 * steel_fy;

            list.Add(string.Format("Allowable  direct stress (Normal) = σ_ct_all =  0.66 * fy "));
            list.Add(string.Format("                                             =  0.66 * {0}", steel_fy));
            list.Add(string.Format("                                             =  {0:f3} MPa", steel_ads_norm));
            list.Add(string.Format(""));


            steel_abs_norm = 0.75 * steel_fy;

            list.Add(string.Format("Allowable  bending stress = σ_ctc_all =  0.75 * fy"));
            list.Add(string.Format("                                     =  0.75 * {0:f3} ", steel_fy));
            list.Add(string.Format("                                     =  {0:f3} MPa", steel_abs_norm));
            list.Add(string.Format(""));

            steel_ads_seismic = steel_ads_norm;
            steel_abs_seismic = steel_abs_norm;
            list.Add(string.Format("Allowable direct stress (Seismic) = Allowable direct stress( Normal ) = {0:f3} MPa", steel_ads_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("Allowable bending stress (Seismic) = Allowable bending stress( Normal ) = {0:f3} MPa", steel_abs_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_seismic = Nmax * 1000 / (Math.PI * det * det / 4);
            list.Add(string.Format("Top direct  σ_ct (Seismic)= Nmax x 1000/(π x det^2/4)"));
            list.Add(string.Format("                          = {0} x 1000/(π x {1:f3}^2/4)", Nmax, det));
            list.Add(string.Format("                          = {0:f3} MPa", steel_sigma_ct_seismic));
            list.Add(string.Format(""));

            steel_sigma_ct_norm = (Nnorm * 1000) / (Math.PI * det * det / 4.0);
            list.Add(string.Format("Top direct  σ_ct (Normal)= Nnorm x 1000/(π x det^2/4)", steel_sigma_ct_norm));
            list.Add(string.Format("                         = {0} x 1000/(π x {1:f2}^2/4)", Nnorm, det));
            list.Add(string.Format("                         = {0:f3} MPa", steel_sigma_ct_norm));
            list.Add(string.Format(""));

            steel_sigma_ctc_norm = Hlng_n * 1000 * (h - ha) / (Math.PI * det * det * det / 32.0) + (Med + MRdn) / (Math.PI * det * det * det / 32.0);

            list.Add(string.Format("Top bending normal = σ_ctc =  Hlngn x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("                              + (Med + MRdn) / (π x det^3 / 32)", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format("Top bending normal = σ_ctc =  {0:f3} x 1000 x ( {1} - {2} ) /(π x {3:f3}^3 / 32)", Hlng_n, h, ha, det));
            list.Add(string.Format("                              + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32)", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("                           =  {0:f3} MPa", steel_sigma_ctc_norm));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sigma_ctc1 = Hlng_s * 1000 * (h - ha) / (Math.PI * det * det * det / 32) + (Med + MRdn) / (Math.PI * det * det * det / 32);
            list.Add(string.Format(" σ_ctc1 =  Hlng_s x 1000 x ( h-ha ) /(π x det^3 /32) "));
            list.Add(string.Format("           + (Med + MRdn) / (π x det^3 / 32)"));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} x 1000 x ( {1}-{2} ) /(π x {3:f3}^3 /32) ", Hlng_s, h, ha, det));
            list.Add(string.Format("           + ({0:f3} + {1:f3}) / (π x {2:f3}^3 / 32))", Med, MRdn, det));
            list.Add(string.Format(""));
            list.Add(string.Format("        =  {0:f3} MPa", sigma_ctc1));

            double sigma_ctc2 = Hlts * 1000 * (h - ha) / (Math.PI * det * det * det / 32);
            list.Add(string.Format(""));

            list.Add(string.Format(" σ_ctc2 = Hlts  x 1000 x ( h-ha ) /(π x det^3 / 32)"));
            list.Add(string.Format("        = {0:f3}  x 1000 x ( {1}-{2} ) /(π x {2:f3}^3 / 32)", Hlts, h, ha, det));
            list.Add(string.Format("        = {0:f3} MPa", steel_sigma_ctc_seismic));

            steel_sigma_ctc_seismic = Math.Max(sigma_ctc1, sigma_ctc2);

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Top bending seismic = σ_ctc = Maximum Value of (σ_ctc1  OR  σ_ctc2)"));
            list.Add(string.Format("                            = Maximum Value of ({0:f3}  OR  {1:f3})", sigma_ctc1, sigma_ctc2));
            list.Add(string.Format("                            = {0:f3} MPa", steel_sigma_ctc_seismic));
            list.Add(string.Format(""));
            //list.Add(string.Format("OR"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Hlat_s  x 1000 x ( h-ha ) /(π x det^3 / 32)    =   {0:f3} MPa", steel_sigma_ctc_seismic));
            lamda_t_norm = (steel_sigma_ct_norm / steel_ads_norm) + (steel_sigma_ctc_norm / steel_abs_norm);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all"));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_norm, steel_ads_norm, steel_sigma_ctc_norm, steel_abs_norm));

            if (lamda_t_norm <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa  <= 1 (Normal), Hence OK", lamda_t_norm));
            else
                list.Add(string.Format("    = {0:f3} MPa  > 1 (Normal), Hence NOT OK", lamda_t_norm));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            lamda_t_seismic = (steel_sigma_ct_seismic / steel_ads_seismic) + (steel_sigma_ctc_seismic / steel_abs_seismic);

            list.Add(string.Format(" λt = σ_ct/σ_ct_all + σ_cbc /σ_ctc_all", lamda_t_seismic));
            list.Add(string.Format("    = {0:f3}/{1:f3} + {2:f3} /{3:f3}", steel_sigma_ct_seismic, steel_ads_seismic, steel_sigma_ctc_seismic, steel_abs_seismic));

            if (lamda_t_seismic <= 1.0)
                list.Add(string.Format("    = {0:f3} MPa <= 1 (Seismic), Hence OK", lamda_t_seismic));
            else
                list.Add(string.Format("    = {0:f3} MPa > 1 (Seismic), Hence NOT OK", lamda_t_seismic));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.1 : PTFE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cp = Nnorm * 1000.0 / An;
            list.Add(string.Format("(a)  Contact stress = σ_cp = Nnorm x 1000 / An "));
            list.Add(string.Format("                           = {0} x 1000 / {1:f3}", Nnorm, An));
            if (sigma_cp < 40)
                list.Add(string.Format("                           = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cp));
            else
                list.Add(string.Format("                           = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cp));


            list.Add(string.Format(""));


            sigma_cpc = sigma_cp + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * dp * dp * dp / 32));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_cp + (Med + (Max of (MRdn OR MRds))/(π x dp^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_cp, Med, MRdn, MRds, dp));
            if (sigma_cpc < 45)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >  45 N/mm^2,    Hence NOT OK", sigma_cpc));

            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.2 : ELASTOMERIC DISC"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            sigma_ce = Nmax * 1000 / Ae;
            list.Add(string.Format("(a)  Compressive stress = σ_ce = Nmax x 1000 / Ae"));
            list.Add(string.Format("                               = {0:f3} x 1000 / {1:f3}", Nmax, Ae));
            if (sigma_ce <= 35)
                list.Add(string.Format("                               = {0:f3} N/mm^2  <=  35 N/mm^2,    Hence OK", sigma_ce));
            else
                list.Add(string.Format("                               = {0:f3} N/mm^2  >  35 N/mm^2,    Hence NOT OK", sigma_ce));


            list.Add(string.Format(""));

            sigma_cpc = sigma_ce + ((Med + Math.Max(MRds, MRdn)) / (Math.PI * di * di * di / 32));

            //list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc  = σ_cp + (Med + (Max of (MRdn))/(π x di^3/32) = {0:f3} N/mm^2  <  45 N/mm^2,    Hence OK", sigma_cp));


            list.Add(string.Format("(b)  Extreme Fibre Stress = σ_cpc = σ_ce + (Med + (Max of (MRdn OR MRds))/(π x di^3/32)"));
            list.Add(string.Format("                                  = {0:f3} + ({1:f3} + (Max ({2:f3} OR {3:f3}))/(π x {4:f3}^3/32)", sigma_ce, Med, MRdn, MRds, di));

            if (sigma_cpc < 40)
                list.Add(string.Format("                                  = {0:f3} N/mm^2  <  40 N/mm^2,    Hence OK", sigma_cpc));
            else
                list.Add(string.Format("                                  = {0:f3} N/mm^2  >=  40 N/mm^2,    Hence NOT OK", sigma_cpc));


            eff = 0.5 * di * theta / he;
            list.Add(string.Format(""));
            list.Add(string.Format("(c)  Strain at the perimeter due to rotation θ rad. eff = 0.5 x di x θ / he"));
            list.Add(string.Format("                                                        = 0.5 x {0} x θ / {1}", di, theta, he));
            if (eff < 0.15)
                list.Add(string.Format("                                                        = {0:f3} < 0.15, Hence OK", eff));
            else
                list.Add(string.Format("                                                        = {0:f3} >= 0.15, Hence NOT OK", eff));


            clrns = hc - he - w - theta * (di / 2);


            list.Add(string.Format(""));
            list.Add(string.Format("(d)  Clearance between cylinder top and piston bottom = hc-he-w-θ x (di/2)"));
            list.Add(string.Format("                                                      = {0} - {1} - {2} - {3} x ({4}/2)", hc, he, w, theta, di));
            if (clrns > 5)
                list.Add(string.Format("                                                      = {0:f3} > 5 ,   Hence OK", clrns));
            else
                list.Add(string.Format("                                                      = {0:f3} <= 5 ,   Hence NOT OK", clrns));
            list.Add(string.Format(""));


            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.3 : POT CYLINDER"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Hoop's (tensile) stress  in the cross section, due to"));
            list.Add(string.Format(""));


            sigma_t1 = di * he * sigma_ce / (2 * bp * hc);
            list.Add(string.Format("(a)  Fluid pressure σ_t1 = di  x he x σ_ce / ( 2 x bp x hc)"));
            list.Add(string.Format("                         = {0}  x {1} x {2:f3} / ( 2 x {3} x {4})", di, he, sigma_ce, bp, hc));
            list.Add(string.Format("                         = {0:f3} MPa", sigma_t1));
            list.Add(string.Format(""));

            sigma_t2 = (H * 1000) / (2.0 * bp * hc);
            list.Add(string.Format("(b)  Horizontal force σ_t2 = H x 1000 / ( 2 x bp x hc )"));
            list.Add(string.Format("                           = {0} x 1000 / ( 2 x {1} x {2} )", H, bp, hc));
            list.Add(string.Format("                           = {0:f3} MPa", sigma_t2));
            list.Add(string.Format(""));

            sigma_t = sigma_t1 + sigma_t2;
            list.Add(string.Format("(c) Total stress = σ_t = σ_t1 + σ_t2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3}", sigma_t1, sigma_t2));
            list.Add(string.Format("                       = {0:f3} MPa", sigma_t));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.4 : POT INTERFACE"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("(i)  Shear stress in cylinder & base interface considering    1 mm orange slice, due to"));
            list.Add(string.Format(""));

            sigma_q1 = he * sigma_ce / bp;
            list.Add(string.Format("(a)  Fluid pressure, σ_q1 = he x σ_ce/bp "));
            list.Add(string.Format("                          = {0} x {1:f3}/{2} ", he, sigma_ce, bp));
            list.Add(string.Format("                          = {0:f3} MPa", sigma_q1));
            list.Add(string.Format(""));





            list.Add(string.Format("(b)  Assuming Parabolic distribution factor = 1.5, "));
            list.Add(string.Format(""));
            sigma_q2 = (1.5 * H * 1000 / di) / bp;
            list.Add(string.Format("      Horizontal Force = σ_q2 = (1.5 x H x 1000/di) / bp"));
            list.Add(string.Format("                              = (1.5 x {0} x 1000/{1}) / {2}", H, di, bp));
            list.Add(string.Format("                              = {0:f3} MPa", sigma_q2));
            list.Add(string.Format(""));
            sigma_q = sigma_q1 + sigma_q2;
            list.Add(string.Format("(c)  Total stress = σ_q = σ_q1 + σ_q2"));
            list.Add(string.Format("                        = {0:f3}  + {1:f3}", sigma_q1, sigma_q2));
            if (sigma_q < 153)
                list.Add(string.Format("                        = {0:f3} MPa  < 153 MPa Hence OK", sigma_q));
            else
                list.Add(string.Format("                        = {0:f3} MPa  > 153 MPa Hence NOT OK", sigma_q));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(ii) Beriding stress in the interface considering  1 mm orange slice"));
            list.Add(string.Format(""));

            sigma_bt1 = ((sigma_ce * he * he / 2) * 6.0) / (bp * bp);

            list.Add(string.Format("(a) Fluid  pressure = σ_bt1 = (σ_ce  x he^2 /2) x 6 / bp^2", sigma_bt1));
            list.Add(string.Format("                            = ({0:f3}  x {1}^2 /2) x 6 / {2}^2 ", sigma_ce, he, bp));
            list.Add(string.Format("                            = {0:f3} MPa", sigma_bt1));

            sigma_bt2 = ((1.5 * 1000 * H / di) * (ha - kb) * 6) / (bp * bp);
            list.Add(string.Format(""));

            list.Add(string.Format("(b) Horizontal force = σ_bt2 = ( 1.5 x 1000 x H/di) x ( ha-kb) x 6 / bp^2", sigma_bt2));
            list.Add(string.Format("                             = ( 1.5 x 1000 x {0}/{1}) x ( {2}-{3}) x 6 / {4}^2", H, di, ha, kb, bp));
            list.Add(string.Format("                             = {0:f3} MPa", sigma_bt2));

            sigma_bt = sigma_bt1 + sigma_bt2;
            list.Add(string.Format(""));

            list.Add(string.Format("(c) Totai Stress  σ_bt = σ_bt1  + σ_bt2"));
            list.Add(string.Format("                       = {0:f3} + {1:f3} ", sigma_bt1, sigma_bt2));
            if (sigma_bt < 224.4)
                list.Add(string.Format("                       = {0:f3} MPa  < 224.4  MPa,   Hence OK", sigma_bt));
            else
                list.Add(string.Format("                       = {0:f3} MPa  >= 224.4  MPa,   Hence NOT OK", sigma_bt));

            list.Add(string.Format(""));
            sigma_e = Math.Sqrt((sigma_bt * sigma_bt + 3 * sigma_q * sigma_q));
            list.Add(string.Format("(iii) Combined stress  σ_e  = √(σ_bt^2 + 3 × σ_q^2) "));
            list.Add(string.Format("                            = √({0:f3}^2 + 3 × {1:f3}^2)", sigma_bt, sigma_q));

            if (sigma_e < 306.0)
                list.Add(string.Format("                            = {0:f3} MPa < 306 MPa   Hence OK", sigma_e));
            else
                list.Add(string.Format("                            = {0:f3} MPa > 306 MPa   Hence NOT OK", sigma_e));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.5 : Eff.cont.width at piston-cylinder interface"));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            double sigma_p = 254.84;
            list.Add(string.Format("σ_p = {0:f3} MPa", sigma_p));

            we = (H * 1000 * 1.3) / (di * sigma_p);
            list.Add(string.Format("Eff.cont.width at piston-cylinder interface = we = H x 1000 x 1.3 / (di x σ_e)"));
            list.Add(string.Format("                                                 = {0} x 1000 x 1.3 / ({1} x {2:f3})", H, di, sigma_e));
            if (we < w)
                list.Add(string.Format("                                                 = {0:f4} mm  <  {1} (w)     Hence OK", we, w));
            else
                list.Add(string.Format("                                                 = {0:f4} mm  >=   {1} (w)     Hence NOT OK", we, w));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.6 : GUIDE "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            sigma_qu = (H * 1000) / (Lu * ku);
            list.Add(string.Format("(i)  Shear stress  σ_qu = H x 1000 / ( Lu x ku ) "));
            list.Add(string.Format("                        = {0} x 1000 / ( {1} x {2} )", H, Lu, ku));
            if (sigma_qu < 153)
                list.Add(string.Format("                        = {0:f4} MPa   <   153 MPa, Hence OK", sigma_qu));
            else
                list.Add(string.Format("                        = {0:f4} MPa   >=   153 MPa, Hence NOT OK", sigma_qu));



            list.Add(string.Format(""));





            sigma_bu = H * 1000 * 6 * (hts / 2 + 8) / (Lu * ku * ku);

            list.Add(string.Format("(ii)  Bending Stress = σ_bu = H x 1000 x 6 x ( hts / 2+8) / ( Lu x ku^2 )"));
            list.Add(string.Format("                            = {0} x 1000 x 6 x ( {1} / 2+8) / ( {2} x {3}^2 )", H, hts, Lu, ku));

            if (sigma_bu < 224.4)
                list.Add(string.Format("                            = {0:f4} MPa   <   224.4 MPa, Hence OK", sigma_bu));
            else
                list.Add(string.Format("                            = {0:f4} MPa   >=   224.4 MPa, Hence NOT OK", sigma_bu));

            list.Add(string.Format(""));

            sigma_eu = Math.Sqrt(sigma_bu * sigma_bu + 3 * sigma_qu * sigma_qu);

            list.Add(string.Format("(iii) Combined stress = σ_eu = √(σ_bu^2 + 3 x σ_qu^2)"));
            list.Add(string.Format("(                            = √({0:f3}^2 + 3 x {1:f3}^2)", sigma_bu, sigma_qu));
            if (sigma_eu < 306.0)
                list.Add(string.Format("(                            = {0:f4} MPa   <   306 MPa ", sigma_eu));
            else
                list.Add(string.Format("(                            = {0:f4} MPa   >=   306 MPa ", sigma_eu));

            sigma_du = H * 1000 / (hts * (Lu - 10));
            list.Add(string.Format(""));

            list.Add(string.Format("(iv)  Direct stress on SS = σ_du = H × 1000/(hts x (Lu-10)) "));
            list.Add(string.Format("                                 = {0} × 1000/({1} x ({2}-10)) ", H, hts, Lu));
            if (sigma_du < 200)
                list.Add(string.Format("                                 = {0:f4} MPa < 200 MPa ", sigma_du));
            else
                list.Add(string.Format("                                 = {0:f4} MPa >= 200 MPa ", sigma_du));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 3.7 : ANCHOR "));
            list.Add(string.Format("-------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(a) Anchor Screws"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            bottom_Hperm = ((bottom_n * bottom_Ab * bottom_sigma_bq_perm) / 1000) + (Nmin * bottom_mu);

            //list.Add(string.Format("Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)] > H = Maximum Horizontal force on bearing."));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3}", bottom_mu));
            list.Add(string.Format("σ_bq_perm  = Permissible  shear stress  in  bolt = {0:f3}  MPa", bottom_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maximum Horizontal force on bearing"));
            list.Add(string.Format(""));
            //list.Add(string.Format("The total resistive force = Hperm = {0:f3} kN > H , OK ", bottom_Hperm));


            list.Add(string.Format("The total resistive force = Hperm = [(n x Ab x σ_bq_perm) / 1000)+(Nmin x µ)]"));
            list.Add(string.Format("                                  = [({0} x {1:f3} x {2:f3}) / 1000)+({3:f3} x {4:f3})]", bottom_n, bottom_Ab, bottom_sigma_bq_perm, Nmin, bottom_mu));

            if (bottom_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H),   Hence OK", bottom_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H),   Hence NOT OK", bottom_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format("Top :"));
            list.Add(string.Format("--------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Anchor Screws"));
            //list.Add(string.Format("-------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Bottom :"));
            list.Add(string.Format(""));

            list.Add(string.Format("The total resistive force against shifting of bearing"));
            list.Add(string.Format(""));

            top_Hperm = ((top_n * top_Ab * top_sigma_bq_perm) / 1000);
            //list.Add(string.Format("Hperm = ((n x Ab x σ_bq_perm) / 1000) > H, Maximum Horizontal force on bearing."));
            list.Add(string.Format(""));

            list.Add(string.Format("At bottom of bearing, Coefficient of friction, µ = {0:f3} ", top_mu));
            list.Add(string.Format(""));
            list.Add(string.Format("σ_bq,perm  = Permissible shear stress in bolt = {0:f3} MPa", top_sigma_bq_perm));
            list.Add(string.Format(""));
            list.Add(string.Format("H = Maxm. Horizontal force on bearing"));
            list.Add(string.Format(""));
            list.Add(string.Format("The total resistive force = Hperm = (n x Ab x σ_bq_perm) / 1000", top_Hperm));
            list.Add(string.Format("                                  = ({0} x {1:f3} x {2:f3}) / 1000 ", top_n, top_Ab, top_sigma_bq_perm));
            if (top_Hperm > H)
                list.Add(string.Format("                                  = {0:f3} kN > {1:f3} kN (H) , OK", top_Hperm, H));
            else
                list.Add(string.Format("                                  = {0:f3} kN < {1:f3} kN (H)  , NOT OK", top_Hperm, H));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(b)  Anchor Sleeves"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bottom Screws."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            sigma_cav = (bottom_Ab * bottom_sigma_bq_perm) / (bottom_L * bottom_D);
            list.Add(string.Format("( i ) Avg. bearing stress in conc. σ_cav. = (Ab x σ_bq_perm) / (L x D ) "));
            list.Add(string.Format("                                          = ({0:f3} x {1:f3}) / ({2:f3} x {3:f3} )", bottom_Ab, bottom_sigma_bq_perm, bottom_L, bottom_D));
            list.Add(string.Format("                                          = {0:f3} MPa", sigma_cav));
            list.Add(string.Format(""));


            double sigma_ctr = 2 * sigma_cav;

            list.Add(string.Format("( ii) Considering traingular distribution, bending stress in conc = σ_ctr = 2 x σ_cav"));
            list.Add(string.Format("                                                                          = 2 x {0:f3}", sigma_cav));
            list.Add(string.Format("                                                                          = {0:f3} MPa", sigma_ctr));
            list.Add("");
            double sigma_cpk = 1.5 * sigma_ctr;
            list.Add(string.Format("(iii) Peak stress = σ_cpk = 1.5 x σ_ctr"));
            list.Add(string.Format("                          = 1.5 x {0:f3}", sigma_ctr));
            if (sigma_cpk < 18.56)
                list.Add(string.Format("                          = {0:f3} MPa  < 18.56 MPa, Hence OK", sigma_cpk));
            else
                list.Add(string.Format("                          = {0:f3} MPa  >= 18.56 MPa, Hence NOT OK", sigma_cpk));
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            list.Add("");
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------            Thank you for using ASTRA Pro          ---------------");
            list.Add("---------------------------------------------------------------------------");

            #region Sample


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("CONTRACTOR : M/s. SEW INFRASTRUCTURE                       LTD.                                   2/25/201316:40"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                   DESIGN       OF Steflomet POT/PTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("1     REFERENCE:"));
            //list.Add(string.Format("       Drawing                                                                  :3..P14VMT Sp¢     Rev-0      REFERENCE :                                         IRC:83(Part-W)"));
            //list.Add(string.Format("       Project :For four laning of krishnagar Baharampour section of NH-34 from km 115.000 to km 193.000 West Barigal."));
            //list.Add(string.Format("       For ROB at Chanage;  133+432+501                      Quantity -4+4 nos.                 Location: A1&A4                                    Type - Transverselý Guided."));

            //list.Add(string.Format("       Normal Vertical Load, Nnorm                              : 1,334.00                   kN         Trans. Horz. Force  Hlts (Seismic)           0.00                 kN"));




            //list.Add(string.Format("       Dead load,  Nmin                                         :   332.00                      kN         Long Horz Force( Normal ) Hlng_n                  65.00                kN"));
            //list.Add(string.Format("       Rotation due to Dead Load. θp                            :     0.0065                     rad          Long Harz. Force, (Seismic) Hlng_s          174.00              kN"));
            //list.Add(string.Format("       Rotation due to Live Load, θy                            :     0.0015                      rad         Design horz. Force, H(For bearing            174.00              kN"));
            //list.Add(string.Format("       Rotation, e                                              : 0.010                        rad         components only)"));
            //list.Add(string.Format("       Movement, elong(±)                                               O                              mm        Movement, etrans.(±)                              10.0                 mm"));


            //list.Add(string.Format("             3      BEARINGPARAMETERS"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("      1.   Steel components to conform IS:2062(MS), IS: 1030 Gr.340-570 W(CS) Therefore, Yield Stress, fy 340 MPa"));

            //list.Add(string.Format("            Dia of Elastomeric pad, di                               :  240                         mm         Thickness of the Pad ,he                           16                   mm"));
            //list.Add(string.Format("            Area of Pad. Ae  = π x di2 / 4                       .   : 4.52E+04                 mm2       Hardness  IRHD ( Shore-A)                      50 ± 5"));
            //list.Add(string.Format("            Value of Constant, k1                                     :  2.2                                          Value of Constant, k2                               101"));
            //list.Add(string.Format("            Moment due to resistance to Rotation.                                        Med = di' X ( k, X Op + k2 X 0,)                                         2.29E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Normal)..                             MRdn = 0.2 X ( di / 2) X He                                             1.56E+06  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Transverse Seismic),               MRst  = 0.2 X ( d  / 2) X Hlts                                .             0.00E+00  N-mm"));
            //list.Add(string.Format("            Moment due to frictional resistance (Longitudinal Seismic),            MRsl  = 0.2  X ( di / 2) X H  .                                             4.18E+06  N-mm"));
            //list.Add(string.Format("       3.  SADDLE"));
            //list.Add(string.Format("            Dia of PTFE(dp)                                             :  240                          mm        Contact with piston,w                              6                       mm"));
            //list.Add(string.Format("            Thickness of Saddle plate,  hm                        :  12                            mm        Eff. Plan area of PTFE,An                       45238.93         mm®"));
            //list.Add(string.Format("            Height of SS on saddle,hts                               :  12                            mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("       4.  POT CYLINDER"));
            //list.Add(string.Format("            Dia / sq of base plate,  Do                                :  310                          mm        Thkness of base plate,kb                          15                     mm"));
            //list.Add(string.Format("            Outer dia of cylinder, do                                  :  294                          mm        Depth of cyfinder,hc                                 32                   mm"));
            //list.Add(string.Format("            Effective dia at the bottom, deb                     .  300                          mm        Width of cylinder wall                                27                    mm"));
            //list.Add(string.Format("            di + 2 x 2 x kb                                                                                                   bp=(do-di)/2"));
            //list.Add(string.Format("            Available diameter (deb,avibl) at the substructure, geometrically    simifar to, deb =                                                          650                   mm"));
            //list.Add(string.Format("       5.   TOP ASSEMBLY"));
            //list.Add(string.Format("             Length of top piate, a.                                   :  400                           mm       Width of top plate,b                                  500                  mm"));
            //list.Add(string.Format("             Thk.ness of SS Plate(ks)                                 : 3                               mm        Thickness of top plate, kt                        14                     mm"));
            //list.Add(string.Format("             Length of SS plate, as                                     :  320                          mm        Width of SS plate,  bs                                300                  mm"));
            //list.Add(string.Format("             Thk. of guide, ku                                            :  20                            mm        Effective Length of guide,Lu                     240                 mm"));
            //list.Add(string.Format("             Effective dia of the dispersed  area at the top structure, det  = Min((dp+2 x 2x(kt+ks)) & b]                                           308                  mm"));
            //list.Add(string.Format("             Available diameter (det,avibl)  at the substructure, geometrically similar to,  det =                                                           650                   mm"));
            //list.Add(string.Format("       6.   ANCHOR"));
            //list.Add(string.Format("             Bottom:"));
            //list.Add(string.Format("             Bolt size( IS:1363) Mj                              :        16                            mm         Root area of bolt Ab                                167                   mm2"));
            //list.Add(string.Format("             No. off boits(n)                                        :        4                                            Class of bolt, bc                                10.9"));
            //list.Add(string.Format("             Dia of Sleeve, D                                       :        40                              mm       Length of sleeve , L                                 170                   mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             TOP :"));
            //list.Add(string.Format("             Size (1S:1363) Mj                                     :        20                                          Root area of bolt,Ab                                 272                  mm2"));
            //list.Add(string.Format("             No. of bolts(n)                                         :        4                                             Class of bolt(tS:1367)                               10.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        7.   Ht of applicat5on of horiz. Force(ha)= kb + he + w/2                                                                                                      34                    mm"));
            //list.Add(string.Format("        8.   Overall height of bearing (h) mm                                                                                                                                    88                     mm"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(" 4     DES1GN CHECK :"));
            //list.Add(string.Format("        Structure at bottom of Bearing :                              Concrete                                Grade of Concrete    , fck                          M45"));
            //list.Add(string.Format("        Allowable direct stress(Normal)                              0.25 fck X ((debavi) / (deb))                                                                    22.50              MPa"));
            //list.Add(string.Format("        Allowable bending stress(Normal)                           0.33 fck                                                                                                  14.85               MPa"));
            //list.Add(string.Format("        Allowable direct stress ( Seismic)                             1.25 x 0.25 fck X ((debavl) / (deb))                                                        28.13               MPa"));
            //list.Add(string.Format("        Allowabie bending stress ( Seismic)                          1.25 x 0.33 fck.                                                                                      18.56              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Seismic)                                   N max  x 1000!(x(deb) /4) MPa =                                                           19.81              MPa"));
            //list.Add(string.Format("        Bottom direct σ_cb( Normal)                                   N norm. x 1000/(x(deb)2/4) MPa =                                                        18.87               MPa"));
            //list.Add(string.Format("        Bottom bending( Normal) σ_cbc = Hn × 1000 x ha / x.(deb)3 /32 + (Med + Mw) / (u deb' / 32)  MPa =                         -        2.29                 MPa"));
            //list.Add(string.Format("        Bottom bending( Seismic) σ_cbc,Max, Hing,s   x 1000 x ha / (x deb' / 32)   + (Ma  + Man) / (n deb3 / 32) MPa ="));
            //list.Add(string.Format("        OR, Hlat,s x 1000 x ha / (x deb' / 32)   MPa =                                                                                                                                      4.67  MPa"));
            //list.Add(string.Format("        λb=acb/acb,all      +acbc /acbc,all=                                                0.993                 MPa<=1(Normal)                                     Hence  OK"));
            //list.Add(string.Format("        λb = acb / ocb,all +  acbc / acbc,all   =                                          0.956                 MPa<=1( Seism c)                                    Hence  OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("                       CONTRACTOR             :    M/s. SEW             INFRASTRUCTURE                      LTD.                                  2/25/201316:40"));
            //list.Add(string.Format("                DESIGN OF Steflomet POTIPTFE VERSO-MONO AXIAL BEARING (TRANSVERSE)"));
            //list.Add(string.Format("   Structure at Top of Bearing :                                  STEEL to IS :  2062               fy.=                                                                         230  MPa"));
            //list.Add(string.Format("   Allowable  direct stress( Normal )=                         0.66 * fy                                                                                                151.80            MPa"));
            //list.Add(string.Format("   Arlowable bending stress (Normal)=                     0.75 * fy                                                                                               172.5               MPa"));
            //list.Add(string.Format("   Allowable direct stress( Seismic )= Allowable direct stress( Normal )                                                           151.80             MPa"));
            //list.Add(string.Format("                     .                     Allowable bending  stress ( Seismic)   =                    Allowable bending stress( Normal )                                                        172.50"));
            //list.Add(string.Format("   Top directµct(Seismic)= Nmaxx1000/(udet2/4)N/mm2;                                                                                                        18.79              MPa"));
            //list.Add(string.Format("   Topdirect,act(Normal)=  N x1000 / (udet2/4)N/mm2  =                                                                                                       17.90              MPa"));
            //list.Add(string.Format("   Top bending normal,acic =  Hn x 1000 x ( h-ha ) /(x.det3 / 32) + (M,ø + Mpø) / (x det® / 32) MPa =                                 2.57                MPa"));
            //list.Add(string.Format("   Top bending seismic,actc =Max, Hlng,s   x 1000 x ( h-ha ) /(n.det' / 32) + (Me + Maø) / (2 det' / 32) MPa ="));
            //list.Add(string.Format("   OR, Hlat,s  x 1000 x ( h-ha ) /(x.det3 / 32) MPa =                                                                                                                              3.28  MPa"));
            //list.Add(string.Format("   at = act / act,ali + actc / actc.all  =                                                              0.133  MPa<=1(Normal)                                    Hence OK"));
            //list.Add(string.Format("   At = act / oct,ali + actc / ocic,all =                                                              0.143 MPa<=1( Seismic)                                    Hence  OK"));
            //list.Add(string.Format("2 PTFE :"));
            //list.Add(string.Format("   (a)  Contdct stress = acp  = N x 1000 / An =                                 29.49                N/mm2                                                    < 40 N/mm^2       Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Stress, acpc  = göp+(Me,d+(Max of (Msg))/(xXdp'/32)=          34.25                                                       < 45 N/mm^2       Hence  OK"));
            //list.Add(string.Format("3  ELASTOMERIC DISC :"));
            //list.Add(string.Format("   (a)  Compressive stress,ace    = Nmax x 1000 / Ae :                    29.49                 MPa                                                        <=35 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (b)  Extreme Fibre Strëss, acpc   = ace+(M,,a+(Max of (MR.d))/(xXdí3/32)=       34.25                                                       <=40 N/mm^2      Hence  OK"));
            //list.Add(string.Format("   (c)  Strain at the perimeter due to rotation u rad. sp = 0.5 x di x u / he, eff =                                                                0.08 < 0.15        Hence   OK"));
            //list.Add(string.Format("   (d)  Clearance between cylinder top and piston bottom= bc-he-w-u.(di/2)    =                                                               8.75 >5 mm           Hence   OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4  POT CYL1NDER:"));
            //list.Add(string.Format("   (i)  Hoop's (tensile) stress  in the cross section, due to"));
            //list.Add(string.Format("   (a)  Fluid pressure at1=di  x he x ace / ( 2 x bp x hc) =                                                                                                          65.53              MPa"));
            //list.Add(string.Format("   (b)  Horizontal force at2 = H ×1000 x / ( 2 x bp x bc ) =                                                                                                     100.69.            MPa"));
            //list.Add(string.Format("   (c) Total stress = at1 + at2  =                                                    166.22 MPa                                                                  <= 204                 MPa"));
            //list.Add(string.Format("5  POT INTERFACE :"));
            //list.Add(string.Format("   (i)  Shear stress in cylinder & base interface considering    1mm orange slice, due to"));
            //list.Add(string.Format("  (a)  Fluid pressure, aq1= he    x ace/bp =                                                                                                                                17.47              MPa"));
            //list.Add(string.Format("   (b) Assuming Parabolic distribution factor =1.5, Horizontal Force aq2  = (1.5 x H x 1000/di) / bp =                                    40.28               MPa"));
            //list.Add(string.Format("   (c) Total stress og = agi   + aq2  =                                                              57.75 MPa                                                         153                 MPa   Hence  OK"));
            //list.Add(string.Format("   (ii) Beriding stress in the interface considering  1 mm orange slice"));
            //list.Add(string.Format("  (a)  Fluid  pressure, obt1 = (ace  x he2 /2) x 6 / bp2=                                 31.065 MPa"));
            //list.Add(string.Format("   (b) Horizontal force,abt2=( 1.5 x 1000 x H/di) x ( ha.kb) x 6 / bp2=       170.06  MPa"));
            //list.Add(string.Format("   (c) Totai Stress obt = abt1  + abt2 =                                                         201.13 MPa                                                                     224.4  MPa   Hence OK"));
            //list.Add(string.Format("   (iii) Combined stress  ,ae  = V( abt2 , 3×,92) =                                           224.6  MPa                                                                        306 MPa   Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             6 Eff.cont.width at piston-cylinder interface,we = Hx1000x1.3/(dixap)=    3.6961 mm                                                          < w    Hence OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("             7 GUIDE :"));
            //list.Add(string.Format("  (i)   Shear stress, aqu   = H x 1000 / ( Lu x ku ) =                                        36.25 MPa                                                                        153  MPa   Hence OK"));
            //list.Add(string.Format("  (ii) Bending Stress,abu    = H x 1000 x 6 x ( his / 2+8) / ( Lu x ku2 ) =      184.88 MPa                                                                     224.4  MPa  Hence  OK"));
            //list.Add(string.Format("  (iii) Combined stress,  aeu  = %( abu2 + 3 x aqu2) =                                  195.25  MPa                                                                       306  MPa   Hence  OK"));
            //list.Add(string.Format("  (iv) DirectstressonSS,adu=H×1000/(htsx(Lu-10))=                                    60.42  MPa                                                                        200 MPa   HenceOK"));
            //list.Add(string.Format("             8 ANCHOR :"));
            //list.Add(string.Format("   ( a) Anchor Screws"));
            //list.Add(string.Format("   Bottom:"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000)+(Nmin x µ)) > H. Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bottom of bearing, Coefficient of friction, µ =                                     0.2"));
            //list.Add(string.Format("         σ_bqiperm  = Permissible  shear stress  in  bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        Thetotalresistiveforce            Hperm=                                 223.3BkN>H         OK"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        Anchor Screws"));
            //list.Add(string.Format("        Bottom :"));
            //list.Add(string.Format("        The total resistive force against shifting of bearing"));
            //list.Add(string.Format("        Hperm = [ (n x Ab x sbq,perm) / 1000] > H, Maximum Horizontal force on bearing."));
            //list.Add(string.Format("        At bóttom of bearing, Coefficient of friction, µ=                                      0.2"));
            //list.Add(string.Format("         abq,perm  = Permissible shear stress    in bolt =                        235  MPa"));
            //list.Add(string.Format("        H = Maxm. Horizontal force on bearing"));
            //list.Add(string.Format("        The totalresistiveforce           Hperm=                                 255.68 kN>  H        OK"));
            //list.Add(string.Format("   (b)  Anchor Sleeves"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("         Bottom Screws."));
            //list.Add(string.Format(""));
            //list.Add(string.Format("( i ) Avg. bearing stress in conc. acav. =[Ab x abq,perm] / (  L x D ) =                                                                                          5.77  MPa"));
            //list.Add(string.Format("( ii) Considering traingular distribution , bending  stress  in conc, Sctr  = 2 x scav =                    .                                                 11.54 MPa"));
            //list.Add(string.Format("(iii) Peak stress, acpk=1.5xactr=                                                                                                                                                17.31  MPa"));
            //list.Add(string.Format("                                                                                                                                                                                  18.56 MPa  Hence   OK"));

            #endregion Sample

            File.WriteAllLines(Report_File, list.ToArray());
        }

    }
}
