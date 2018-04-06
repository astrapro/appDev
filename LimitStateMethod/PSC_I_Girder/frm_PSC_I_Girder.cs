﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
//using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
//using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.PSC_I_Girder;


using LimitStateMethod.RCC_T_Girder;
using LimitStateMethod.LS_Progress;
using LimitStateMethod.DeckSlab;


using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;


namespace LimitStateMethod.PSC_I_Girder
{
    public partial class frm_PSC_I_Girder : Form
    {

        //SYMBOLS
        //const string Title = "ANALYSIS OF PSC I-GIRDER BRIDGE (LIMIT STATE METHOD)";
        const string Title = "PSC I-GIRDER BRIDGE (LIMIT STATE METHOD)";
        IApplication iApp;
        PSC_I_Girder_LS_Extend Bridge_Analysis = null;

        DeckSlab_Analysis_Extend Deck_Analysis = null;

        //PreStressedConcrete_SectionProperties sec_1 = new PreStressedConcrete_SectionProperties();
        //PreStressedConcrete_SectionProperties sec_2 = new PreStressedConcrete_SectionProperties();
        //PreStressedConcrete_SectionProperties sec_3 = new PreStressedConcrete_SectionProperties();
        //PreStressedConcrete_SectionProperties sec_4 = new PreStressedConcrete_SectionProperties();
        //PreStressedConcrete_SectionProperties sec_5 = new PreStressedConcrete_SectionProperties();


        //TGirder_Section_Properties LG_INNER_MID;
        //TGirder_Section_Properties LG_OUTER_MID;
        //TGirder_Section_Properties LG_INNER_SUP;
        //TGirder_Section_Properties LG_OUTER_SUP;
        //TGirder_Section_Properties CG_INTER;
        //TGirder_Section_Properties CG_END;



        MidSection mid_og = new MidSection();
        MidSection mid_ig = new MidSection();
        SupportSection sup_og = new SupportSection();
        SupportSection sup_ig = new SupportSection();
        EndSection end_g = new EndSection();


        //Chiranjit [2012 06 08]
        RccPier rcc_pier = null;

        //Chiranjit [2012 05 27]
        RCC_AbutmentWall Abut = null;

        bool IsCreateData = true;
        //bool IsInnerGirder

        public frm_PSC_I_Girder(IApplication app)
        {
            InitializeComponent();
            iApp = app;

            IsCreateData = true;
            user_path = iApp.LastDesignWorkingFolder;

            //InitializeComponent();
            //iApp = thisApp;
            //user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
            Result = new List<string>();

        }
 


        public void PSC_Girder_User_Input()
        {
            List<string> lst_input = new List<string>();
            #region user input
            lst_input.Add(string.Format("Distance between C/C Exp. Joint"));

            lst_input.Add(string.Format("Overhang of girder off the bearing"));
            lst_input.Add(string.Format("Overhang of slab off the bearing"));
            lst_input.Add(string.Format("Expansion Joint"));
            lst_input.Add(string.Format("Deck Width"));

            lst_input.Add(string.Format("Angle of skew"));
            lst_input.Add(string.Format("Clear carriage way"));
            lst_input.Add(string.Format("Width of outer railing"));
            lst_input.Add(string.Format("Width of Footpath"));
            lst_input.Add(string.Format("Width of Crash Barrier"));
            lst_input.Add(string.Format("Spacing of main girder c/c"));

            lst_input.Add(string.Format("Spacing of cross girder c/c"));
            lst_input.Add(string.Format("Thk of deck slab"));
            lst_input.Add(string.Format("Thk of deck slab at overhang"));
            lst_input.Add(string.Format("Thk of wearing coat"));

            lst_input.Add(string.Format("Cantilever slab thk at fixed end"));
            lst_input.Add(string.Format("Cantilever slab thk at free end"));
            lst_input.Add(string.Format("No of main girder"));
            lst_input.Add(string.Format("Depth of main girder"));
            lst_input.Add(string.Format("Flange width of girder"));
            lst_input.Add(string.Format("Web thk of girder at mid span"));
            lst_input.Add(string.Format("Web thk of girder at Support"));
            lst_input.Add(string.Format("Thickness of top flange"));
            lst_input.Add(string.Format("Thickness of top haunch"));
            lst_input.Add(string.Format("Bottom width of flange"));
            lst_input.Add(string.Format("Thickness of bottom flange"));
            lst_input.Add(string.Format("Thickness of bottom haunch"));
            lst_input.Add(string.Format("Length of varying portion"));
            lst_input.Add(string.Format("Length of solid portion"));
            lst_input.Add(string.Format("No of Intermediate cross girder"));


            lst_input.Add(string.Format("Web thk of Intermediate cross girder "));
            lst_input.Add(string.Format("Web thk of end cross girder "));
            lst_input.Add(string.Format("Grade of concrete for precast Girder                   "));
            lst_input.Add(string.Format("Grade of concrete of other componant                 "));
            lst_input.Add(string.Format("Grade of reinforcement"));


            lst_input.Add(string.Format("Partial factor of safety  (Basic and seismic)"));
            lst_input.Add(string.Format("Partial factor of safety Accidental "));
            lst_input.Add(string.Format("Coefficient to consider the influence of the strength"));


            lst_input.Add(string.Format("Clear cover"));
            lst_input.Add(string.Format("Unit weight of dry concrete"));
            lst_input.Add(string.Format("Unit weight of wet concrete"));
            lst_input.Add(string.Format("Weight of wearing course"));
            lst_input.Add(string.Format("Weight of Crash Barrier"));
            lst_input.Add(string.Format("Weight of Railing"));
            lst_input.Add(string.Format("Intensity of Load for shuttering"));


            lst_input.Add(string.Format("Partial factor of safety for basic and seismic"));
            lst_input.Add(string.Format("Partial factor of safety for Accidental"));

            lst_input.Add(string.Format("Es"));

            lst_input.Add(string.Format("Anchorage from c/L of bearing"));

            lst_input.Add(string.Format("Length of Girder beyond c/L of Bearing"));
            lst_input.Add(string.Format("Distance of Jack from c/L of Bearing"));


            lst_input.Add(string.Format("Modulus of Elasticity of Deck = s3"));
            lst_input.Add(string.Format("Modulus of Elasticity of Girder = s4"));
            //lst_input.Add(string.Format("Modular Ratio = s3/s4"));


            #endregion
            List<string> lst_inp_vals = new List<string>();
            #region Value
            lst_inp_vals.Add(string.Format("26.52"));
            lst_inp_vals.Add(string.Format("1.050"));
            lst_inp_vals.Add(string.Format("1.600"));
            lst_inp_vals.Add(string.Format("40"));
            lst_inp_vals.Add(string.Format("12.0"));

            lst_inp_vals.Add(string.Format("45.00"));
            lst_inp_vals.Add(string.Format("11.00"));
            lst_inp_vals.Add(string.Format("0.000"));
            lst_inp_vals.Add(string.Format("0.000"));
            lst_inp_vals.Add(string.Format("500"));
            lst_inp_vals.Add(string.Format("3.000"));

            lst_inp_vals.Add(string.Format("17.150"));
            lst_inp_vals.Add(string.Format("200"));
            lst_inp_vals.Add(string.Format("400"));
            lst_inp_vals.Add(string.Format("65"));

            lst_inp_vals.Add(string.Format("200"));
            lst_inp_vals.Add(string.Format("200"));
            lst_inp_vals.Add(string.Format("4.0"));
            lst_inp_vals.Add(string.Format("2400"));
            lst_inp_vals.Add(string.Format("1000"));
            lst_inp_vals.Add(string.Format("0.30"));
            lst_inp_vals.Add(string.Format("0.65"));
            lst_inp_vals.Add(string.Format("150"));
            lst_inp_vals.Add(string.Format("75"));
            lst_inp_vals.Add(string.Format("650"));
            lst_inp_vals.Add(string.Format("250"));
            lst_inp_vals.Add(string.Format("200"));
            lst_inp_vals.Add(string.Format("2.50"));
            lst_inp_vals.Add(string.Format("2.50"));
            lst_inp_vals.Add(string.Format("1.00"));


            lst_inp_vals.Add(string.Format("300"));
            lst_inp_vals.Add(string.Format("600"));
            lst_inp_vals.Add(string.Format("M 45"));
            lst_inp_vals.Add(string.Format("M 40"));
            lst_inp_vals.Add(string.Format("Fe 500"));


            lst_inp_vals.Add(string.Format("1.50"));
            lst_inp_vals.Add(string.Format("1.20"));
            lst_inp_vals.Add(string.Format("0.67"));


            lst_inp_vals.Add(string.Format("40.0"));
            lst_inp_vals.Add(string.Format("2.50"));
            lst_inp_vals.Add(string.Format("2.60"));
            lst_inp_vals.Add(string.Format("0.20"));
            lst_inp_vals.Add(string.Format("1.00"));
            lst_inp_vals.Add(string.Format("0.30"));
            lst_inp_vals.Add(string.Format("0.50"));


            lst_inp_vals.Add(string.Format("1.15"));
            lst_inp_vals.Add(string.Format("1.00"));

            lst_inp_vals.Add(string.Format("200000"));

            lst_inp_vals.Add(string.Format("1.050"));

            lst_inp_vals.Add(string.Format("0.900"));
            lst_inp_vals.Add(string.Format("0.900"));

            lst_inp_vals.Add(string.Format("32500"));
            lst_inp_vals.Add(string.Format("33500"));



            #endregion

            List<string> lst_units = new List<string>();
            #region Input Units
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("deg."));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));

            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));

            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format(""));


            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("Mpa"));
            lst_units.Add(string.Format("Mpa"));
            lst_units.Add(string.Format("Mpa"));
            lst_units.Add(string.Format(""));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));



            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("t/m3"));
            lst_units.Add(string.Format("t/m3"));
            lst_units.Add(string.Format("t/m2"));
            lst_units.Add(string.Format("t/m"));
            lst_units.Add(string.Format("t/m"));
            lst_units.Add(string.Format("t/m"));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));

            lst_units.Add(string.Format("Mpa"));

            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));


            #endregion Input Units

            dgv_long_user_input.Rows.Clear();
            for (int i = 0; i < lst_inp_vals.Count; i++)
            {
                dgv_long_user_input.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);
            }

            //Cable_Input_Data();
            //Cable_Auto_Data();
        }

        public void Calculate_Section_Properties()
        {
            //List<string> list = Design_PSC_I_Girder();


            List<string> list = new List<string>();


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3 : SECTION PROPERTIES"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            mid_og = new MidSection();
            mid_ig = new MidSection();
            sup_og = new SupportSection();
            sup_ig = new SupportSection();
            end_g = new EndSection();

            #region mid_og

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3.1 : OUTER MAIN GIRDER SECTION PROPERTIES AT MID SPAN"));
            list.Add(string.Format("------------------------------------------------------------"));
            mid_og = new MidSection();


            mid_og.b1 = MyList.StringToDouble(txt_sp_out_mid_b1.Text, 0.0);
            mid_og.b2 = MyList.StringToDouble(txt_sp_out_mid_b2.Text, 0.0);
            mid_og.b3 = MyList.StringToDouble(txt_sp_out_mid_b3.Text, 0.0);
            mid_og.b4 = MyList.StringToDouble(txt_sp_out_mid_b4.Text, 0.0);
            mid_og.b5 = MyList.StringToDouble(txt_sp_out_mid_b5.Text, 0.0);
            mid_og.b6 = MyList.StringToDouble(txt_sp_out_mid_b6.Text, 0.0);
            mid_og.b7 = MyList.StringToDouble(txt_sp_out_mid_b7.Text, 0.0);
            mid_og.b8 = MyList.StringToDouble(txt_sp_out_mid_b8.Text, 0.0);


            mid_og.d1 = MyList.StringToDouble(txt_sp_out_mid_d1.Text, 0.0);
            mid_og.d2 = MyList.StringToDouble(txt_sp_out_mid_d2.Text, 0.0);
            mid_og.d3 = MyList.StringToDouble(txt_sp_out_mid_d3.Text, 0.0);
            mid_og.d4 = MyList.StringToDouble(txt_sp_out_mid_d4.Text, 0.0);
            mid_og.d5 = MyList.StringToDouble(txt_sp_out_mid_d5.Text, 0.0);
            mid_og.d6 = MyList.StringToDouble(txt_sp_out_mid_d6.Text, 0.0);
            mid_og.d7 = MyList.StringToDouble(txt_sp_out_mid_d7.Text, 0.0);
            mid_og.d8 = MyList.StringToDouble(txt_sp_out_mid_d8.Text, 0.0);
            mid_og.d9 = MyList.StringToDouble(txt_sp_out_mid_d9.Text, 0.0);

            mid_og.Calculate("3.1");


            list.AddRange(mid_og.Results.ToArray());
            #endregion mid_og

            #region mid_og

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3.2 : INNER MAIN GIRDER SECTION PROPERTIES AT MID SPAN"));
            list.Add(string.Format("-----------------------------------------------------------"));


            mid_ig.b1 = MyList.StringToDouble(txt_sp_inn_mid_b1.Text, 0.0);
            mid_ig.b2 = MyList.StringToDouble(txt_sp_inn_mid_b2.Text, 0.0);
            mid_ig.b3 = MyList.StringToDouble(txt_sp_inn_mid_b3.Text, 0.0);
            mid_ig.b4 = MyList.StringToDouble(txt_sp_inn_mid_b4.Text, 0.0);
            mid_ig.b5 = MyList.StringToDouble(txt_sp_inn_mid_b5.Text, 0.0);
            mid_ig.b6 = MyList.StringToDouble(txt_sp_inn_mid_b6.Text, 0.0);
            mid_ig.b7 = MyList.StringToDouble(txt_sp_inn_mid_b7.Text, 0.0);
            mid_ig.b8 = MyList.StringToDouble(txt_sp_inn_mid_b8.Text, 0.0);


            mid_ig.d1 = MyList.StringToDouble(txt_sp_inn_mid_d1.Text, 0.0);
            mid_ig.d2 = MyList.StringToDouble(txt_sp_inn_mid_d2.Text, 0.0);
            mid_ig.d3 = MyList.StringToDouble(txt_sp_inn_mid_d3.Text, 0.0);
            mid_ig.d4 = MyList.StringToDouble(txt_sp_inn_mid_d4.Text, 0.0);
            mid_ig.d5 = MyList.StringToDouble(txt_sp_inn_mid_d5.Text, 0.0);
            mid_ig.d6 = MyList.StringToDouble(txt_sp_inn_mid_d6.Text, 0.0);
            mid_ig.d7 = MyList.StringToDouble(txt_sp_inn_mid_d7.Text, 0.0);
            mid_ig.d8 = MyList.StringToDouble(txt_sp_inn_mid_d8.Text, 0.0);
            mid_ig.d9 = MyList.StringToDouble(txt_sp_inn_mid_d9.Text, 0.0);

            mid_ig.Calculate("3.2");


            list.AddRange(mid_ig.Results.ToArray());
            #endregion mid_og

            #region supp_og

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3.3 : OUTER MAIN GIRDER SECTION PROPERTIES AT SUPPORT"));
            list.Add(string.Format("----------------------------------------------------------"));


            sup_og.b1 = MyList.StringToDouble(txt_sp_out_sup_b1.Text, 0.0);
            sup_og.b2 = MyList.StringToDouble(txt_sp_out_sup_b2.Text, 0.0);
            sup_og.b3 = MyList.StringToDouble(txt_sp_out_sup_b3.Text, 0.0);
            sup_og.b4 = MyList.StringToDouble(txt_sp_out_sup_b4.Text, 0.0);


            sup_og.d1 = MyList.StringToDouble(txt_sp_out_sup_d1.Text, 0.0);
            sup_og.d2 = MyList.StringToDouble(txt_sp_out_sup_d2.Text, 0.0);
            sup_og.d3 = MyList.StringToDouble(txt_sp_out_sup_d3.Text, 0.0);
            sup_og.d4 = MyList.StringToDouble(txt_sp_out_sup_d4.Text, 0.0);
            sup_og.d5 = MyList.StringToDouble(txt_sp_out_sup_d5.Text, 0.0);

            sup_og.Calculate("3.3");


            list.AddRange(sup_og.Results.ToArray());
            #endregion supp_og

            #region supp_ig

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3.4 : INNER MAIN GIRDER SECTION PROPERTIES AT SUPPORT"));
            list.Add(string.Format("----------------------------------------------------------"));


            sup_ig.b1 = MyList.StringToDouble(txt_sp_inn_sup_b1.Text, 0.0);
            sup_ig.b2 = MyList.StringToDouble(txt_sp_inn_sup_b2.Text, 0.0);
            sup_ig.b3 = MyList.StringToDouble(txt_sp_inn_sup_b3.Text, 0.0);
            sup_ig.b4 = MyList.StringToDouble(txt_sp_inn_sup_b4.Text, 0.0);


            sup_ig.d1 = MyList.StringToDouble(txt_sp_inn_sup_d1.Text, 0.0);
            sup_ig.d2 = MyList.StringToDouble(txt_sp_inn_sup_d2.Text, 0.0);
            sup_ig.d3 = MyList.StringToDouble(txt_sp_inn_sup_d3.Text, 0.0);
            sup_ig.d4 = MyList.StringToDouble(txt_sp_inn_sup_d4.Text, 0.0);
            sup_ig.d5 = MyList.StringToDouble(txt_sp_inn_sup_d5.Text, 0.0);

            sup_ig.Calculate("3.4");

            list.AddRange(sup_ig.Results.ToArray());
            #endregion mid_og

            #region End Girder

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3.5 : END DIAPHRAM SECTION PROPERTIES AT SUPPORT"));
            list.Add(string.Format("-----------------------------------------------------"));

            end_g.b1 = MyList.StringToDouble(txt_sp_end_b1.Text, 0.0);
            end_g.b2 = MyList.StringToDouble(txt_sp_end_b2.Text, 0.0);
            end_g.b3 = MyList.StringToDouble(txt_sp_end_b3.Text, 0.0);
            end_g.b4 = MyList.StringToDouble(txt_sp_end_b4.Text, 0.0);


            end_g.d1 = MyList.StringToDouble(txt_sp_end_d1.Text, 0.0);
            end_g.d2 = MyList.StringToDouble(txt_sp_end_d2.Text, 0.0);
            end_g.d3 = MyList.StringToDouble(txt_sp_end_d3.Text, 0.0);
            end_g.d4 = MyList.StringToDouble(txt_sp_end_d4.Text, 0.0);

            end_g.Calculate("3.5");


            list.AddRange(end_g.Results.ToArray());
            #endregion End Girder

            //File.WriteAllLines(Report_File, list.ToArray());

            rtb_sp_details.Lines = list.ToArray();


            #region Show Results
            txt_smp_i_a_mid.Text = mid_ig.Girder_Area_Sum.ToString("f4");
            txt_smp_i_ix_mid.Text = mid_ig.IG.ToString("f4");
            txt_smp_i_iz_mid.Text = mid_ig.IG.ToString("f4");

            txt_smp_i_a_sup.Text = sup_ig.Girder_Area_Sum.ToString("f4");
            txt_smp_i_Ix_sup.Text = sup_ig.IG.ToString("f4");
            txt_smp_i_Iz_sup.Text = sup_ig.IG.ToString("f4");

            //txt_smp_i_a_mid.Text = lg_outer_mid.Girder_Section_A.ToString("f4");
            //txt_smp_i_a_sup.Text = lg_outer_mid.Girder_Section_A.ToString("f4");


            txt_smp_ii_a_mid.Text = mid_ig.Composite_Area_Sum.ToString("f4");
            txt_smp_ii_a_sup.Text = sup_ig.Composite_Area_Sum.ToString("f4");

            txt_smp_ii_ix_mid.Text = mid_ig.IC.ToString("f4");
            txt_smp_ii_ix_sup.Text = sup_ig.IC.ToString("f4");

            txt_smp_ii_iz_mid.Text = mid_ig.IC.ToString("f4");
            txt_smp_ii_iz_sup.Text = sup_ig.IC.ToString("f4");



            txt_smp_iii_a_mid.Text = mid_og.Composite_Area_Sum.ToString("f4");
            txt_smp_iii_a_sup.Text = sup_og.Composite_Area_Sum.ToString("f4");
            txt_smp_iii_ix_mid.Text = mid_og.IC.ToString("f4");
            txt_smp_iii_ix_sup.Text = sup_og.IC.ToString("f4");
            txt_smp_iii_iz_mid.Text = mid_og.IC.ToString("f4");
            txt_smp_iii_iz_sup.Text = sup_og.IC.ToString("f4");


            txt_smp_iv_a_cg.Text = end_g.Composite_Area_Sum.ToString("f4");
            txt_smp_iv_ix_cg.Text = end_g.IC.ToString("f4");
            txt_smp_iv_iz_cg.Text = end_g.IC.ToString("f4");


            //txt_smp_v_a_cg.Text = CG_INTER.Composite_Section_A.ToString("f4");
            //txt_smp_v_ix_cg.Text = CG_INTER.Composite_Section_Ix.ToString("f4");
            //txt_smp_v_iz_cg.Text = CG_INTER.Composite_Section_Iz.ToString("f4");

            #endregion Show Results
        }

        private void Design_PSC_I_Girder()
        {

            List<string> list = new List<string>();


            Excel_User_Inputs esi = new Excel_User_Inputs();

            esi.Read_From_Grid(dgv_long_user_input);

            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*                 ASTRA Pro                  *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*   DESIGN  OF PRE STRESSED CONCRETE (PSC)    *");
            list.Add("\t\t*  I GIRDER BRIDGE FOR  LIMIT STATE METHOD    *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("USER'S INPUT DATA :"));
            list.Add(string.Format("--------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            int i = 0;
            foreach (var item in esi)
            {
                i++;
                list.Add(string.Format("{0}. {1} = {2} {3}", i, item.Input_Text, item.Input_Value, item.Input_Unit));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.AddRange(rtb_sp_details.Lines);
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            Result = list;

            File.WriteAllLines(Report_File, list.ToArray());

        }

        private void frm_PSC_I_Girder_Load(object sender, EventArgs e)
        {
            PSC_Girder_User_Input();

            #region Analysis Data
            Bridge_Analysis = new PSC_I_Girder_LS_Extend(iApp);
            Default_Moving_LoadData(dgv_deck_liveloads);
            Default_Moving_LoadData(dgv_long_liveloads);
            Default_Moving_Type_LoadData(dgv_long_loads);
            Default_Moving_Type_LoadData(dgv_deck_loads);
            Deckslab_User_Input();
            PSC_Girder_User_Input();
            cmb_HB.SelectedIndex = 0;
            British_Interactive();



            cmb_long_open_file.Items.Clear();
            cmb_long_open_file.Items.Add(string.Format("DEAD LOAD ANALYSIS"));
            cmb_long_open_file.Items.Add(string.Format("TOTAL ANALYSIS"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 1"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 2"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 3"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 4"));
            cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 5"));
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 6"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 7"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 8"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 9"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 10"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 11"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 12"));
                cmb_long_open_file.Items.Add(string.Format("LIVE LOAD ANALYSIS 13"));

                tbc_PSC_Girder.TabPages.Remove(tab_mov_data_British);
            }
            else
                tbc_PSC_Girder.TabPages.Remove(tab_mov_data_indian);

            cmb_long_open_file.Items.Add(string.Format("PSC GIRDER ANALYSIS RESULTS"));
            #endregion Analysis Data


            #region RCC Abutment
            Abut = new RCC_AbutmentWall(iApp);
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;
            cmb_abut_fck.SelectedIndex = 3;
            cmb_abut_fy.SelectedIndex = 1;
            #endregion RCC Abutment


            #region RCC Pier
            rcc_pier = new RccPier(iApp);
            //pic_pier_interactive_diagram.BackgroundImage = AstraFunctionOne.ImageCollection.Pier_drawing;
            cmb_rcc_pier_fck.SelectedIndex = 3;
            cmb_rcc_pier_fy.SelectedIndex = 1;
            cmb_pier_2_k.SelectedIndex = 1;
            #endregion RCC Pier

            cmb_long_open_file.SelectedIndex = 0;
            cmb_deck_input_files.SelectedIndex = 0;


            Button_Enable_Disable();

            txt_Ana_B.Text = txt_Ana_B.Text + "";
            txt_LL_load_gen.Text = (L / 0.2).ToString("0");

            //PSC_Section_Properties();

            Text_Changed();
            Button_Enable_Disable();

            //Chiranjit [2014 08 26]
            Deckslab_Interactives();

            tab_deck.TabPages.Remove(tab_deck_ll_data);

        }


        string Report_File { get { return Path.Combine(user_path, "Design of PSC I Girder Bridge.txt"); } }
  
        private void btn_design_process_Click(object sender, EventArgs e)
        {
            //Write_All_Data(true);
            //MessageBox.Show("1");
            Analysis_Initialize_InputData(true);
            //IsCreateData = true;
            if (IsCreateData)
                user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            //MessageBox.Show("2");
            if (!Directory.Exists(user_path))
                Directory.CreateDirectory(user_path);
            string usp = Path.Combine(user_path, "PSC Girder Analysis");
            if (!Directory.Exists(usp))
                Directory.CreateDirectory(usp);


            //Calculate_Section_Properties();

            Design_PSC_I_Girder();
            iApp.View_Result(Report_File);
        }


        #region Define Properties Chiranjit [2013 09 23]
        //Define Properties
        public double L { get { return MyList.StringToDouble(txt_Ana_L.Text, 26.0); } set { txt_Ana_L.Text = value.ToString("f3"); } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }
        public double CW { get { return MyList.StringToDouble(txt_Ana_CW.Text, 0.0); } set { txt_Ana_CW.Text = value.ToString("f3"); } }
        public double CL { get { return MyList.StringToDouble(txt_Ana_CL.Text, 0.0); } set { txt_Ana_CL.Text = value.ToString("f3"); } }
        public double CR { get { return MyList.StringToDouble(txt_Ana_CR.Text, 0.0); } set { txt_Ana_CR.Text = value.ToString("f3"); } }
        public double Ds { get { return MyList.StringToDouble(txt_Ana_Ds.Text, 0.0); } set { txt_Ana_Ds.Text = value.ToString("f3"); } }
        public double Dso { get { return MyList.StringToDouble(txt_Ana_Dso.Text, 0.0); } set { txt_Ana_Dso.Text = value.ToString("f3"); } }
        public double Y_c_dry { get { return MyList.StringToDouble(txt_Ana_gamma_c_dry.Text, 0.0); } set { txt_Ana_gamma_c_dry.Text = value.ToString("f3"); } }
        public double Y_c_wet { get { return MyList.StringToDouble(txt_Ana_gamma_c_wet.Text, 0.0); } set { txt_Ana_gamma_c_wet.Text = value.ToString("f3"); } }
        public double Ang { get { return MyList.StringToDouble(txt_Ana_ang.Text, 0.0); } set { txt_Ana_ang.Text = value.ToString("f3"); } }
        public double NMG { get { return MyList.StringToDouble(txt_Ana_NMG.Text, 0.0); } set { txt_Ana_NMG.Text = value.ToString("f3"); } }
        public double SMG { get { return MyList.StringToDouble(txt_Ana_SMG.Text, 0.0); } set { txt_Ana_SMG.Text = value.ToString("f3"); } }
        public double DMG { get { return MyList.StringToDouble(txt_Ana_DMG.Text, 0.0); } set { txt_Ana_DMG.Text = value.ToString("f3"); } }
        public double Deff { get { return (DMG - 0.0500 - 0.016 - 6 * 0.032); } }
        public double BMG { get { return MyList.StringToDouble(txt_sp_inn_mid_b5.Text, 0.0); } set { txt_sp_inn_mid_b5.Text = value.ToString("f3"); } }
        public double NCG { get { return MyList.StringToDouble(txt_Ana_NCG.Text, 0.0); } set { txt_Ana_NCG.Text = value.ToString("f3"); } }
        public double DCG { get { return MyList.StringToDouble(txt_sp_end_d4.Text, 0.0); } set { txt_sp_end_d4.Text = value.ToString("f3"); } }
        public double BCG { get { return MyList.StringToDouble(txt_sp_end_b4.Text, 0.0); } set { txt_sp_end_b4.Text = value.ToString("f3"); } }
        public double Dw { get { return MyList.StringToDouble(txt_Ana_Dw.Text, 0.0); } set { txt_Ana_Dw.Text = value.ToString("f3"); } }
        public double wgwc { get { return MyList.StringToDouble(txt_Ana_wgwc.Text, 0.0); } set { txt_Ana_wgwc.Text = value.ToString("f3"); } }
        public double Hc { get { return MyList.StringToDouble(txt_Ana_Hc.Text, 0.0); } set { txt_Ana_Hc.Text = value.ToString("f3"); } }
        public double Wc { get { return MyList.StringToDouble(txt_Ana_Wc.Text, 0.0); } set { txt_Ana_Wc.Text = value.ToString("f3"); } }
        public double Wf { get { return MyList.StringToDouble(txt_Ana_Wf.Text, 0.0); } set { txt_Ana_Wf.Text = value.ToString("f3"); } }
        public double Hs { get { return MyList.StringToDouble(txt_Ana_Hf.Text, 0.0); } set { txt_Ana_Hf.Text = value.ToString("f3"); } }
        public double Wk { get { return MyList.StringToDouble(txt_Ana_Wk.Text, 0.0); } set { txt_Ana_Wk.Text = value.ToString("f3"); } }
        public double Wr { get { return MyList.StringToDouble(txt_Ana_Wr.Text, 0.0); } set { txt_Ana_Wr.Text = value.ToString("f3"); } }
        //public double swf { get { return MyList.StringToDouble(txt_Ana_swf.Text, 0.0); } set { txt_Ana_swf.Text = value.ToString("f3"); } }
        public double swf { get { return 1.0; } }


        public double og { get { return MyList.StringToDouble(txt_Ana_og.Text, 0.0); } set { txt_Ana_og.Text = value.ToString("f3"); } }
        public double os { get { return MyList.StringToDouble(txt_Ana_os.Text, 0.0); } set { txt_Ana_os.Text = value.ToString("f3"); } }
        public double eg { get { return MyList.StringToDouble(txt_Ana_eg.Text, 0.0); } set { txt_Ana_eg.Text = value.ToString("f3"); } }
        public double Lvp { get { return MyList.StringToDouble(txt_Ana_Lvp.Text, 0.0); } set { txt_Ana_Lvp.Text = value.ToString("f3"); } }
        public double Lsp { get { return MyList.StringToDouble(txt_Ana_Lsp.Text, 0.0); } set { txt_Ana_Lsp.Text = value.ToString("f3"); } }
        public double wgc { get { return MyList.StringToDouble(txt_Ana_wgc.Text, 0.0); } set { txt_Ana_wgc.Text = value.ToString("f3"); } }
        public double wgr { get { return MyList.StringToDouble(txt_Ana_wgr.Text, 0.0); } set { txt_Ana_wgr.Text = value.ToString("f3"); } }
        public double ils { get { return MyList.StringToDouble(txt_Ana_ils.Text, 0.0); } set { txt_Ana_ils.Text = value.ToString("f3"); } }
        public double leff { get { return MyList.StringToDouble(txt_Ana_Leff.Text, 0.0); } set { txt_Ana_Leff.Text = value.ToString("f3"); } }

        public double Es { get { return MyList.StringToDouble(txt_Ana_Es.Text, 0.0); } set { txt_Ana_Es.Text = value.ToString("f3"); } }
        public double Ec { get { return MyList.StringToDouble(txt_Ana_Ec.Text, 0.0); } set { txt_Ana_Ec.Text = value.ToString("f3"); } }
        public double Ecm { get { return MyList.StringToDouble(txt_Ana_Ecm.Text, 0.0); } set { txt_Ana_Ecm.Text = value.ToString("f3"); } }

        #endregion Chiranjit [2012 06 20]


        //Chiranjit [2011 11 04] for Display Results
        public List<string> Result { get; set; }
        public string Result_Report
        {
            get
            {
                return Path.Combine(user_path, "ANALYSIS_RESULT.TXT");
            }
        }
        public string user_path { get; set; }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
            }
        }
        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
            }
        }
        public string Design_Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(Drawing_Folder, "DESIGN DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(Drawing_Folder, "DESIGN DRAWINGS"));
                return Path.Combine(Drawing_Folder, "DESIGN DRAWINGS");
            }
        }
        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            //rft.M2 = chk_M2.Checked;
            //rft.M3 = chk_M3.Checked;
            //rft.R3 = chk_R3.Checked;
            //rft.R2 = chk_R2.Checked;
            rft.M2 = true;
            rft.M3 = true;
            rft.R3 = true;
            rft.R2 = true;
            return rft;
        }

        #region Form Events
        private void frm_RCC_T_Girder_LS_Load(object sender, EventArgs e)
        {
            #region Analysis Data
            Bridge_Analysis = new PSC_I_Girder_LS_Extend(iApp);
            Default_Moving_LoadData(dgv_deck_liveloads);
            Default_Moving_LoadData(dgv_long_liveloads);
            Default_Moving_Type_LoadData(dgv_long_loads);
            Default_Moving_Type_LoadData(dgv_deck_loads);
            Deckslab_User_Input();
            PSC_Girder_User_Input();

            #endregion Analysis Data


            #region RCC Abutment
            Abut = new RCC_AbutmentWall(iApp);
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;
            cmb_abut_fck.SelectedIndex = 3;
            cmb_abut_fy.SelectedIndex = 1;
            #endregion RCC Abutment


            #region RCC Pier
            rcc_pier = new RccPier(iApp);
            //pic_pier_interactive_diagram.BackgroundImage = AstraFunctionOne.ImageCollection.Pier_drawing;
            cmb_rcc_pier_fck.SelectedIndex = 3;
            cmb_rcc_pier_fy.SelectedIndex = 1;
            cmb_pier_2_k.SelectedIndex = 1;
            #endregion RCC Pier

            cmb_long_open_file.SelectedIndex = 0;
            cmb_deck_input_files.SelectedIndex = 0;


            Button_Enable_Disable();

            txt_Ana_B.Text = txt_Ana_B.Text + "";
            txt_LL_load_gen.Text = (L / 0.2).ToString("0");

            //PSC_Section_Properties();

            Text_Changed();
            Button_Enable_Disable();

            //tab_sec_props.TabPages.Remove(tab_details);
        }

        #region PSC I Girder Code
        void Calculate_Interactive_Values()
        {
            //return;
            //double eff_dp = MyList.StringToDouble(txt_Ana_eff_depth.Text, 0.0);
            double eff_dp = Deff;
            double lg_spa = SMG;
            double len = MyList.StringToDouble(txt_Ana_L.Text, 0.0);
            double wd = MyList.StringToDouble(txt_Ana_B.Text, 0.0);
            double cant_wd = MyList.StringToDouble(txt_Ana_Ds.Text, 0.0);
            double long_bw = MyList.StringToDouble(txt_sp_inn_mid_b5.Text, 0.0);



            //Long Girder

            //Cross Girders

            int lng_no = (int)((wd - 2 * cant_wd) / lg_spa);
            int cg_no = (int)((len) / lg_spa);
            lng_no++;
            cg_no++;

            //txt_Cross_grade_concrete.Text = cmb_Long_concrete_grade.Text;
            //txt_Cross_grade_steel.Text = cmb_Long_Steel_Grade.Text;




            //Abutment
            //txt_abut_DMG.Text = ((MyList.StringToDouble(txt_sec_in_mid_lg_D.Text, 0.0) / 1000) + 0.2).ToString("f3");

            txt_abut_DMG.Text = (DMG + Ds - MyList.StringToDouble(txt_abut_d3.Text, 0.0)).ToString("f3");
            txt_abut_B.Text = wd.ToString("f3");
            //txt_abut_concrete_grade.Text = cmb_Long_concrete_grade.Text;
            //txt_abut_steel_grade.Text = cmb_Long_Steel_Grade.Text;
            txt_abut_gamma_c.Text = txt_Ana_gamma_c_dry.Text;
            txt_abut_L.Text = len.ToString("f3");


            //RCC Pier Form 1
            txt_RCC_Pier_L.Text = len.ToString("f3");
            txt_RCC_Pier_CW.Text = txt_Ana_CW.Text;
            txt_RCC_Pier__B.Text = wd.ToString("f3");
            txt_RCC_Pier_DMG.Text = DMG.ToString("f3");
            txt_RCC_Pier_Ds.Text = Ds.ToString("f3");
            txt_RCC_Pier_NMG.Text = lng_no.ToString("f0");
            txt_RCC_Pier_NP.Text = lng_no.ToString("f0");
            txt_RCC_Pier___B.Text = (wd - 2.0).ToString("f3");


            double B9 = MyList.StringToDouble(txt_RCC_Pier_B9.Text, 0.0);
            double B10 = MyList.StringToDouble(txt_RCC_Pier_B10.Text, 0.0);
            double B11 = MyList.StringToDouble(txt_RCC_Pier_B11.Text, 0.0);
            double B12 = MyList.StringToDouble(txt_RCC_Pier_B12.Text, 0.0);
            double B14 = MyList.StringToDouble(txt_RCC_Pier___B.Text, 0.0);

            //txt_RCC_Pier_D.Text = ((B10 + B12) / 2.0).ToString("f3");
            //txt_RCC_Pier_b.Text = ((B9 + B11) / 2.0).ToString("f3");



            txt_pier_2_SBC.Text = txt_abut_p_bearing_capacity.Text;
            txt_pier_2_SC.Text = txt_abut_sc.Text;
            txt_pier_2_B16.Text = ((B14 - B12) / 2.0).ToString("f3");
        }

        void Text_Changed()
        {
            SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);

            double Bb = MyList.StringToDouble(txt_sp_inn_mid_b8.Text, 0.65);
            double Db = MyList.StringToDouble(txt_sp_inn_mid_d8.Text, 0.65);

            //Chiranjit [2012 12 26]
            //DMG = L / 10.0;
            //DCG = DMG - 0.4;

            //txt_LL_load_gen.Text = ((L + Math.Abs(MyList.StringToDouble(txt_Ana_X.Text, 0.0))) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");
            txt_LL_load_gen.Text = ((L) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");


            leff = L - eg / 1000.0 - 2 * os;

            //Chiranjit comment this line on 2014/02/12 (yyyy/mm/dd)

            //if (Bs != 0.0)
            //    CW = B - Bs;
            //else
            //    CW = B - 2 * Wp;

            if (rbtn_crash_barrier.Checked)
                CW = B - 2 * Wc;
            else if (rbtn_footpath.Checked)
            {
                if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    CW = B - Wf - Wk;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    CW = B - Wf - Wk;
                else
                    CW = B - 2 * Wf;
            }

            Calculate_Interactive_Values();
            Ana_Initialize_Analysis_InputData();
            Calculate_Load_Computation();


            //txt_sp_inn_mid_b1.Text = txt_Ana_SMG.Text;
            //txt_sp_inn_mid_d1.Text = txt_Ana_Ds.Text;
            //txt_sec_in_mid_lg_D.Text = txt_Ana_DMG.Text;

            //txt_sp_out_mid_b1.Text = txt_Ana_SMG.Text;
            //txt_sp_out_mid_d1.Text = txt_Ana_Ds.Text;
            //txt_sec_out_mid_lg_D.Text = txt_Ana_DMG.Text;

            //txt_sp_inn_sup_b1.Text = txt_Ana_SMG.Text;
            //txt_sp_inn_sup_d1.Text = txt_Ana_Ds.Text;
            //txt_sec_in_sup_lg_D.Text = txt_Ana_DMG.Text;

            //txt_sp_out_sup_b1.Text = txt_Ana_SMG.Text;
            //txt_sp_out_sup_d1.Text = txt_Ana_Ds.Text;
            //txt_sec_out_sup_lg_D.Text = txt_Ana_DMG.Text;


            //txt_sp_end_b1.Text = (0.7 * SMG).ToString("f3");
            //txt_sp_end_d4.Text = txt_Ana_DCG.Text;
            //txt_sp_end_d1.Text = txt_Ana_Ds.Text;
            //txt_sp_end_d3.Text = (Dso - Ds).ToString("f2");

            //txt_sec_int_cg_d.Text = txt_Ana_DCG.Text;
            //txt_sec_int_cg_Ds.Text = txt_Ana_Ds.Text;



            txt_sp_inn_mid_b1.Text = (SMG * 1000).ToString();
            txt_sp_inn_mid_d1.Text = (Ds * 1000).ToString();
            //txt_sec_in_mid_lg_D.Text = txt_Ana_DMG.Text;

            txt_sp_out_mid_b1.Text = (SMG * 1000).ToString();
            txt_sp_out_mid_d1.Text = (Ds * 1000).ToString();
            //txt_sec_out_mid_lg_D.Text = txt_Ana_DMG.Text;

            txt_sp_inn_sup_b1.Text = (SMG * 1000).ToString();
            txt_sp_inn_sup_d1.Text = (Ds * 1000).ToString();
            //txt_sec_in_sup_lg_D.Text = txt_Ana_DMG.Text;

            txt_sp_out_sup_b1.Text = txt_Ana_SMG.Text;
            txt_sp_out_sup_d1.Text = (Ds * 1000).ToString();
            //txt_sec_out_sup_lg_D.Text = txt_Ana_DMG.Text;


            txt_sp_end_b1.Text = (0.7 * SMG*1000).ToString("f0");
            //txt_sp_end_d4.Text = (DCG * 1000).ToString();
            txt_sp_end_d1.Text = (Ds * 1000).ToString();
            txt_sp_end_d3.Text = ((Dso - Ds) * 1000).ToString("f0");

            //txt_sec_int_cg_d.Text = txt_Ana_DCG.Text;
            //txt_sec_int_cg_Ds.Text = txt_Ana_Ds.Text;


            Calculate_Section_Properties();



            #region Main Girder Inputs
            if (dgv_long_user_input.RowCount >= 44)
            {
                dgv_long_user_input[1, 0].Value = txt_Ana_L.Text;
                dgv_long_user_input[1, 1].Value = txt_Ana_og.Text;
                dgv_long_user_input[1, 2].Value = txt_Ana_os.Text;
                dgv_long_user_input[1, 3].Value = txt_Ana_eg.Text;
                dgv_long_user_input[1, 4].Value = txt_Ana_B.Text;
                dgv_long_user_input[1, 5].Value = txt_Ana_ang.Text;
                dgv_long_user_input[1, 6].Value = txt_Ana_CW.Text;
                dgv_long_user_input[1, 7].Value = txt_Ana_Wr.Text;
                dgv_long_user_input[1, 8].Value = txt_Ana_Wf.Text;
                dgv_long_user_input[1, 9].Value = txt_Ana_Wc.Text;
                dgv_long_user_input[1, 10].Value = SMG.ToString("f3");
                dgv_long_user_input[1, 11].Value = SCG.ToString("f3");
                dgv_long_user_input[1, 12].Value = (Ds * 1000).ToString();
                dgv_long_user_input[1, 13].Value = (Dso * 1000).ToString();
                dgv_long_user_input[1, 14].Value = (Dw * 1000).ToString();
                dgv_long_user_input[1, 17].Value = txt_Ana_NMG.Text;
                dgv_long_user_input[1, 18].Value = (DMG * 1000).ToString();

                //dgv_long_user_input[1, 19].Value = MyList.StringToDouble(txt_sp_inn_mid_b1tf.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 20].Value = txt_sec_in_mid_lg_bw.Text;
                //dgv_long_user_input[1, 21].Value = txt_sec_in_sup_lg_bw.Text;
                //dgv_long_user_input[1, 22].Value = MyList.StringToDouble(txt_sec_in_mid_lg_D1.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 23].Value = MyList.StringToDouble(txt_sec_in_mid_lg_D2.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 24].Value = MyList.StringToDouble(txt_sec_in_mid_lg_bwf.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 25].Value = MyList.StringToDouble(txt_sec_in_mid_lg_D4.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 26].Value = MyList.StringToDouble(txt_sec_in_mid_lg_D3.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 27].Value = txt_Ana_Lvp.Text;
                //dgv_long_user_input[1, 28].Value = txt_Ana_Lsp.Text;
                //dgv_long_user_input[1, 29].Value = (NCG - 2);
                //dgv_long_user_input[1, 30].Value = MyList.StringToDouble(txt_sec_int_cg_bw.Text, 0.0) * 1000 + "";
                //dgv_long_user_input[1, 31].Value = MyList.StringToDouble(txt_sec_end_cg_bw.Text, 0.0) * 1000 + "";

                dgv_long_user_input[1, 39].Value = (Y_c_dry / 10.0).ToString("f3");
                dgv_long_user_input[1, 40].Value = (Y_c_wet / 10.0).ToString("f3");
                dgv_long_user_input[1, 41].Value = (wgwc / 10.0).ToString("f3");
                dgv_long_user_input[1, 42].Value = (wgc / 10.0).ToString("f3");
                dgv_long_user_input[1, 43].Value = (wgr / 10.0).ToString("f3");
                dgv_long_user_input[1, 44].Value = (ils / 10.0).ToString("f3");



                dgv_long_user_input[1, 0].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 0].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 1].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 2].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 3].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 4].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 5].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 6].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 7].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 8].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 9].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 10].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 11].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 12].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 13].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 14].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 17].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 18].Style.ForeColor = Color.Red;

                dgv_long_user_input[1, 19].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 20].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 21].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 22].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 23].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 24].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 25].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 26].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 27].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 28].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 29].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 30].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 31].Style.ForeColor = Color.Red;

                dgv_long_user_input[1, 39].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 40].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 41].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 42].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 43].Style.ForeColor = Color.Red;
                dgv_long_user_input[1, 44].Style.ForeColor = Color.Red;
            }
            #endregion Main Girder Inputs

            #region Deck Slab Inputs
            if (dgv_deck_user_input.RowCount >= 30)
            {
                dgv_deck_user_input[1, 0].Value = txt_Ana_L.Text;
                dgv_deck_user_input[1, 1].Value = txt_Ana_og.Text;
                dgv_deck_user_input[1, 2].Value = txt_Ana_os.Text;
                dgv_deck_user_input[1, 3].Value = txt_Ana_eg.Text;
                dgv_deck_user_input[1, 4].Value = txt_Ana_B.Text;
                dgv_deck_user_input[1, 5].Value = txt_Ana_ang.Text;
                dgv_deck_user_input[1, 6].Value = txt_Ana_CW.Text;
                dgv_deck_user_input[1, 8].Value = txt_Ana_Wf.Text;
                dgv_deck_user_input[1, 9].Value = txt_Ana_Wc.Text;
                dgv_deck_user_input[1, 10].Value = SMG.ToString("f3");
                dgv_deck_user_input[1, 11].Value = txt_Ana_Ds.Text;
                dgv_deck_user_input[1, 12].Value = txt_Ana_Dso.Text;
                dgv_deck_user_input[1, 15].Value = txt_Ana_Dw.Text;
                dgv_deck_user_input[1, 16].Value = txt_Ana_NMG.Text;
                //dgv_deck_user_input[1, 19].Value = txt_sp_inn_mid_b1tf.Text;
                dgv_deck_user_input[1, 20].Value = (NCG - 2);
                dgv_deck_user_input[1, 27].Value = (Y_c_dry / 10.0).ToString("f3");
                dgv_deck_user_input[1, 28].Value = (Y_c_wet / 10.0).ToString("f3");
                //dgv_deck_user_input[1, 29].Value = (wgwc / 10.0).ToString("f3");
                dgv_deck_user_input[1, 30].Value = (wgc / 10.0).ToString("f3");
                dgv_deck_user_input[1, 31].Value = (wgr / 10.0).ToString("f3");
                dgv_deck_user_input[1, 32].Value = (ils / 10.0).ToString("f3");


                dgv_deck_user_input[1, 0].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 1].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 2].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 3].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 4].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 5].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 6].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 8].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 9].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 10].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 11].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 12].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 15].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 16].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 19].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 20].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 27].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 28].Style.ForeColor = Color.Red;
                //dgv_deck_user_input[1, 29].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 30].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 31].Style.ForeColor = Color.Red;
                dgv_deck_user_input[1, 32].Style.ForeColor = Color.Red;
            }
            #endregion Deck Slab Inputs




            //uC_Abut1.Length = L;
            //uC_Abut1.Width = B;
            //uC_Abut1.Overhang = og;
        }

        private void txt_sp_inn_mid_b1_TextChanged(object sender, EventArgs e)
        {
            Calculate_Section_Properties();
            //leff
        }

        //Chiranjit [2012 09 21]

        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            try
            {
                Calculate_Section_Properties();

                Write_All_Data(true);
                Analysis_Initialize_InputData(true);
                if (IsCreateData)
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                if (!Directory.Exists(user_path))
                    Directory.CreateDirectory(user_path);
                string usp = Path.Combine(user_path, "PSC Girder Analysis");
                if (!Directory.Exists(usp))
                    Directory.CreateDirectory(usp);

                if(iApp.DesignStandard == eDesignStandard.IndianStandard)
                    LONG_GIRDER_LL_TXT();
                else
                    LONG_GIRDER_BRITISH_LL_TXT();
             

                Bridge_Analysis.Long_inner_mid = mid_ig;
                Bridge_Analysis.Long_outer_mid = mid_og;
                Bridge_Analysis.Long_inner_sup = sup_ig;
                Bridge_Analysis.Long_outer_sup = sup_og;

                Bridge_Analysis.End_Cross_Girder = end_g;

                string inp_file = Path.Combine(user_path, "INPUT_DATA.TXT");

                Bridge_Analysis.Start_Support = Start_Support_Text;
                Bridge_Analysis.End_Support = END_Support_Text;
                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    Bridge_Analysis.CreateData();
                else
                {
                    Bridge_Analysis.HA_Lanes = HA_Lanes;
                    Bridge_Analysis.CreateData_British();
                }

                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.Input_File);
                Bridge_Analysis.WriteData_Total_Analysis(inp_file);

                txt_analysis_file.Text = Bridge_Analysis.Input_File;

                Calculate_Load_Computation(Bridge_Analysis.Outer_Girders_as_String,
                    Bridge_Analysis.Inner_Girders_as_String,
                    Bridge_Analysis.joints_list_for_load);



                Bridge_Analysis.WriteData_Total_Analysis(Bridge_Analysis.TotalAnalysis_Input_File);
                Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.LiveLoadAnalysis_Input_File, all_loads[0]);

                Bridge_Analysis.WriteData_DeadLoad_Analysis(Bridge_Analysis.DeadLoadAnalysis_Input_File);

                if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                {
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true, 2);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 2);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, true, 2);
                }
                else
                {
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.DeadLoadAnalysis_Input_File, false, true, 4);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.TotalAnalysis_Input_File, true, true, 4);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.LiveLoadAnalysis_Input_File, true, true, 4);
                }

                Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.Input_File, true, true, 1);


                for (int i = 0; i < all_loads.Count; i++)
                {
                    Bridge_Analysis.WriteData_LiveLoad_Analysis(Bridge_Analysis.GetAnalysis_Input_File(i + 3), all_loads[i]);
                    Ana_Write_Long_Girder_Load_Data(Bridge_Analysis.GetAnalysis_Input_File(i + 3), true, false, i);
                }

                Bridge_Analysis.Analysis = new BridgeMemberAnalysis(iApp, Bridge_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Bridge_Analysis.LiveLoad_File;

                Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                if (Bridge_Analysis.Live_Load_List == null) return;

                Button_Enable_Disable();
                Write_All_Data();



               MessageBox.Show(this, "Analysis Input data is created as INPUT_DATA.TXT.",
                "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ThreadAbortException ex1) { MessageBox.Show(ex1.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_Ana_process_analysis_Click1(object sender, EventArgs e)
        {
            #region Process
            //Chiranjit [2012 07 13]
            Write_All_Data(true);
            int i = 0;

            string flPath = Bridge_Analysis.Input_File;
            do
            {
                if (i == 0)
                    flPath = Bridge_Analysis.TotalAnalysis_Input_File;
                else if (i == 1)
                {
                    MessageBox.Show(this, "PROCESS ANALYSIS FOR LIVE LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flPath = Bridge_Analysis.LiveLoadAnalysis_Input_File;
                }
                else if (i == 2)
                {
                    MessageBox.Show(this, "PROCESS ANALYSIS FOR DEAD LOAD.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flPath = Bridge_Analysis.DeadLoadAnalysis_Input_File;
                }


                File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                System.Diagnostics.Process prs = new System.Diagnostics.Process();

                System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                if (prs.Start())
                    prs.WaitForExit();
                i++;
            }
            while (i < 3);

            string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
            if (File.Exists(ana_rep_file))
            {



                //List<string> Work_List = new List<string>();

                iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Total Load Analysis Result");

                iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File");
                iApp.Progress_Works.Add("Set Structure Geometry for Dead Load Analysis");
                iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Dead Load Analysis Result");


                iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File");
                iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Live Load Analysis Result");


                iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


                //iApp.Progress_Works = new ProgressList(Work_List);

                Bridge_Analysis.Analysis = null;
                Bridge_Analysis.Analysis = new BridgeMemberAnalysis(iApp, ana_rep_file);


                string s1 = "";
                string s2 = "";
                try
                {
                    for (i = 0; i < Bridge_Analysis.Analysis.Supports.Count; i++)
                    {
                        if (i < Bridge_Analysis.Analysis.Supports.Count / 2)
                        {
                            if (i == Bridge_Analysis.Analysis.Supports.Count / 2 - 1)
                            {
                                s1 += Bridge_Analysis.Analysis.Supports[i].NodeNo;
                            }
                            else
                                s1 += Bridge_Analysis.Analysis.Supports[i].NodeNo + ",";
                        }
                        else
                        {
                            if (i == Bridge_Analysis.Analysis.Supports.Count - 1)
                            {
                                s2 += Bridge_Analysis.Analysis.Supports[i].NodeNo;
                            }
                            else
                                s2 += Bridge_Analysis.Analysis.Supports[i].NodeNo + ",";
                        }
                    }
                }
                catch (Exception ex) { }
                //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
                double BB = B;



                txt_node_displacement.Text = Bridge_Analysis.Analysis.Node_Displacements.Get_Max_Deflection().ToString();


                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                frm_ViewDesign_Forces_Load();






                //Chiranjit [2012 06 22]
                //txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                //txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                //txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                //txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
                //txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



                //txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                //txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
                //txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

                ////txt_abut_w6.Text = Total_LiveLoad_Reaction;
                //txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                ////txt_abut_w6.ForeColor = Color.Red;

                ////txt_abut_w5.Text = Total_DeadLoad_Reaction;
                //txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                //txt_abut_w5.ForeColor = Color.Red;

            }

            grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_ana_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;


            Button_Enable_Disable();


            Button_Enable_Disable();
            Write_All_Data(false);
            #endregion Process
            iApp.Save_Form_Record(this, user_path);


            iApp.Progress_Works.Clear();

        }


        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
            try
            {
                #region Process
                int i = 0;
                Write_All_Data(true);
                Analysis_Initialize_InputData(false);


                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                string flPath = Bridge_Analysis.Input_File;
                i = 0;
                iApp.Progress_Works.Clear();

                pcol.Clear();

                if(iApp.DesignStandard == eDesignStandard.IndianStandard)
                    LONG_GIRDER_LL_TXT();
                else
                    LONG_GIRDER_BRITISH_LL_TXT();

                //Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT"); ;

                for (i = 0; i < (all_loads.Count + 3); i++)
                {
                    flPath = Bridge_Analysis.GetAnalysis_Input_File(i);

                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);

                    if (File.Exists(flPath))
                    {
                        iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath).ToUpper() + " (ANALYSIS_REP.TXT)");
                        //iApp.Progress_Works.Add("Reading Joint Coordinates and Member Connectivity");
                        //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");
                    }
                }
                //iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                //iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                //iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");

                //while (i < 3) ;



                //frm_LS_Process ff = new frm_LS_Process(pcol);
                //ff.Owner = this;
                ////MessageBox.Show(ff.ShowDialog().ToString());
                //if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                //{
                //    iApp.Progress_Works.Clear();
                //    return;
                //}

                if (!iApp.Show_and_Run_Process_List(pcol))
                {
                    iApp.Progress_Works.Clear();
                    return;
                }



                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                string ana_rep_file = Bridge_Analysis.Total_Analysis_Report;
                Bridge_Analysis.All_Analysis.Clear();

                for (i = 0; i < pcol.Count; i++)
                {
                    flPath = MyList.Get_Analysis_Report_File(pcol[i].Process_File_Name);
                    if (File.Exists(flPath))
                    {
                        Bridge_Analysis.Analysis = new BridgeMemberAnalysis(iApp, flPath);
                        Bridge_Analysis.All_Analysis.Add(Bridge_Analysis.Analysis);
                        if (iApp.Is_Progress_Cancel)
                        {
                            iApp.Progress_Works.Clear();
                            iApp.Progress_OFF();
                            return;
                        }
                    }
                    else
                    {
                        //MessageBox.Show(flPath + " not found.");

                        //MessageBox.Show("This must be remembered that all the 16 analyses results will be " +
                        //                "required in the design. So, for the first time all the 16 analyses " +
                        //                "are to be processed.\nSo, do not remove any Check Mark for the first time. \n\nProcess Aborting....", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //goto _file_Check;
                    }
                }

                if (!iApp.Is_Progress_Cancel)
                    Show_Long_Girder_Moment_Shear();

                #region Abutment & Pier
                string s1 = "";
                string s2 = "";
                try
                {
                    for (i = 0; i < Bridge_Analysis.All_Analysis[1].Supports.Count; i++)
                    {
                        if (i < Bridge_Analysis.All_Analysis[1].Supports.Count / 2)
                        {
                            if (i == Bridge_Analysis.All_Analysis[1].Supports.Count / 2 - 1)
                            {
                                s1 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo;
                            }
                            else
                                s1 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo + ",";
                        }
                        else
                        {
                            if (i == Bridge_Analysis.All_Analysis[1].Supports.Count - 1)
                            {
                                s2 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo;
                            }
                            else
                                s2 += Bridge_Analysis.All_Analysis[1].Supports[i].NodeNo + ", ";
                        }
                    }
                }
                catch (Exception ex) { }
                double BB = MyList.StringToDouble(txt_Ana_B.Text, 8.5);



                frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
                frm_ViewForces_Load();

                frm_Pier_ViewDesign_Forces(Bridge_Analysis.Total_Analysis_Report, s1, s2);
                frm_ViewDesign_Forces_Load();


                //Chiranjit [2012 06 22]
                txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
                txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

                txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
                txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
                txt_ana_MSTD.Text = txt_max_Mz_kN.Text;



                txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
                txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
                txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

                txt_abut_w6.Text = Total_LiveLoad_Reaction;
                txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
                txt_abut_w6.ForeColor = Color.Red;

                txt_abut_w5.Text = Total_DeadLoad_Reaction;
                txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
                txt_abut_w5.ForeColor = Color.Red;

                #endregion Abutment & Pier


            _file_Check:
                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;



                Button_Enable_Disable();
                Write_All_Data(false);

                iApp.Progress_Works.Clear();

                #endregion Process
            }
            catch (Exception ex) { }
        }
        void Analysis_Initialize_InputData(bool initialize)
        {
            if (initialize)
                Bridge_Analysis = new PSC_I_Girder_LS_Extend(iApp);





            //Bridge_Analysis.Long_inner_mid = LG_INNER_MID;
            //Bridge_Analysis.Long_Outer_Mid_Section = LG_OUTER_MID;

            //Bridge_Analysis.Long_Inner_Support_Section = LG_INNER_SUP;
            //Bridge_Analysis.Long_Outer_Support_Section = LG_OUTER_SUP;

            //Bridge_Analysis.Cross_End_Section = CG_END;
            //Bridge_Analysis.Cross_Intermediate_Section = CG_INTER;




            Bridge_Analysis.Long_inner_mid = mid_ig;
            Bridge_Analysis.Long_outer_mid = mid_og;

            Bridge_Analysis.Long_inner_sup = sup_ig;
            Bridge_Analysis.Long_outer_sup = sup_og;

            Bridge_Analysis.End_Cross_Girder = end_g;
            Bridge_Analysis.Inter_Cross_Girder = end_g;
            //Bridge_Analysis.Cross_Intermediate_Section = CG_INTER;





            Bridge_Analysis.Length = L;
            Bridge_Analysis.WidthBridge = B;
            Bridge_Analysis.Width_LeftCantilever = CL;
            Bridge_Analysis.Width_RightCantilever = CR;
            Bridge_Analysis.Skew_Angle = Ang;
            //Bridge_Analysis.Effective_Depth = Deff;


            Bridge_Analysis.Support_Distance = L * (1.6 / 26.52); // 1.6
            Bridge_Analysis.Effective_Depth = L * (5.0 / 26.52); // 1.6


            Bridge_Analysis.Support_Distance = og; // 1.6
            Bridge_Analysis.Effective_Depth = Lsp; // 1.6
            Bridge_Analysis.NMG = NMG;

            Bridge_Analysis.NCG = NCG;





            Bridge_Analysis.Ds = Ds;
            Bridge_Analysis.Lvp = Lvp;
            Bridge_Analysis.Lsp = Lsp;
            Bridge_Analysis.Leff = leff;

            Bridge_Analysis.Wc = Wc;
            Bridge_Analysis.og = og;
            Bridge_Analysis.os = os;
            Bridge_Analysis.Es = Es;
            Bridge_Analysis.Ec = Ec;
            Bridge_Analysis.Ecm = Ecm;

            if (rbtn_footpath.Checked)
            {
                if (chk_fp_left.Checked && !chk_fp_right.Checked)
                {
                    Bridge_Analysis.Wf_left = Wf;
                    Bridge_Analysis.Wk_left = Wk;

                    Bridge_Analysis.Wf_right = 0.0;
                    Bridge_Analysis.Wk_right = Wk;
                }
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                {
                    Bridge_Analysis.Wf_left = 0.0;
                    Bridge_Analysis.Wk_left = Wk;

                    Bridge_Analysis.Wf_right = Wf;
                    Bridge_Analysis.Wk_right = Wk;
                }
                else
                {
                    Bridge_Analysis.Wf_left = Wf;
                    Bridge_Analysis.Wk_left = Wk;

                    Bridge_Analysis.Wf_right = Wf;
                    Bridge_Analysis.Wk_right = Wk;
                }
                Bridge_Analysis.Wr = Wr;
            }
            Bridge_Analysis.Start_Support = Start_Support_Text;
            Bridge_Analysis.End_Support = END_Support_Text;

        }


        #endregion PSC I Girder Code

        private void btn_WorkSheet_Design_Click(object sender, EventArgs e)
        {
            string excel_file_name = "";
            string copy_path = "";
            Button btn = sender as Button;

            //if (btn.Name == btn_WSD_Tee_Girders.Name)
            //{
            //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\TBEAM Bridge\TBEAM Worksheet Design 2\04 TeeGirder Design.xls");
            //}
            //else if (btn.Name == btn_WSD_Open.Name)
            //{
            //    iApp.Open_WorkSheet_Design();
            //    return;
            //}

            copy_path = Path.Combine(user_path, Path.GetFileName(excel_file_name));

            if (File.Exists(excel_file_name))
            {
                iApp.OpenExcelFile(Worksheet_Folder, excel_file_name, "2011ap");
            }
        }
        #endregion Form Events

        #region Bridge Deck Analysis Form Events

        public void Open_Create_Data()
        {

            try
            {
                Ana_Initialize_Analysis_InputData();
            }
            catch (Exception ex) { }
        }


        private void rbtn_Ana_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = false;
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;
            //btn_create_data.Enabled = rbtn_ana_create_analysis_file.Checked;

            //if (rbtn_ana_select_analysis_file.Checked)
            //{

            //    string chk_file = Path.Combine(Analysis_Path, "INPUT_DATA.TXT");

            //    if (File.Exists(chk_file))
            //    {
            //        Show_ReadMemberLoad(chk_file);
            //        Ana_OpenAnalysisFile(chk_file);
            //        Read_All_Data();
            //        iApp.LiveLoads.Fill_Combo(ref cmb_load_type);
            //        #region Read Previous Record
            //        //Chiranjit [2013 01 03]
            //        iApp.Read_Form_Record(this, Analysis_Path);
            //    }
            //    #endregion

            //}
            Button_Enable_Disable();


        }
        public string Analysis_Path
        {
            get
            {
                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, Title)))
                    return Path.Combine(iApp.LastDesignWorkingFolder, Title);
                return iApp.LastDesignWorkingFolder;
            }
        }

        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text File (*.txt)|*.txt";
                    ofd.InitialDirectory = Analysis_Path;
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        IsCreateData = false;
                        string chk_file = "";
                        user_path = Path.GetDirectoryName(ofd.FileName);

                        string usp = Path.Combine(user_path, "PSC Girder Analysis");
                        if (Directory.Exists(usp))
                        {
                            chk_file = Path.Combine(usp, "INPUT_DATA.TXT");
                            Bridge_Analysis.Input_File = chk_file;
                        }

                        Ana_OpenAnalysisFile(chk_file);
                        Read_All_Data();

                        #region Read Previous Record
                        iApp.Read_Form_Record(this, user_path);
                        txt_analysis_file.Text = chk_file;
                        #endregion

                        rbtn_ana_select_analysis_file.Checked = true; //Chiranjit [2013 06 25]
                        Open_Create_Data();//Chiranjit [2013 06 25]

                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Button_Enable_Disable();

                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");


            }
        }

        private void txt_Ana_length_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txt_Ana_X.Text = "-" + txt_Ana_L.Text; //Chiranjit [2013 05 29]
                Text_Changed();
                //L
            }
            catch (Exception ex) { }
        }
        private void chk_Ana_CheckedChanged(object sender, EventArgs e)
        {
            //grb_SIDL.Enabled = chk_ana_active_SIDL.Checked;
        }
        #endregion Bridge Deck Analysis Form Events

        #region Bridge Deck Analysis Methods

        void Ana_Initialize_Analysis_InputData()
        {

            if (Deck_Analysis == null)
                Deck_Analysis = new DeckSlab_Analysis_Extend(iApp);

            double Bs = (B - CL - CR) / (NMG - 1);

            Deck_Analysis.Length = MyList.StringToDouble(dgv_deck_user_input[1, 4].Value.ToString(), 0.0);
            //Deck_Analysis.WidthBridge = 6.0;

            Deck_Analysis.Width_LeftCantilever = MyList.StringToDouble(txt_Ana_CL.Text, 0.0);
            Deck_Analysis.Width_RightCantilever = MyList.StringToDouble(txt_Ana_CR.Text, 0.0);
            Deck_Analysis.Skew_Angle = MyList.StringToDouble(dgv_deck_user_input[1, 5].Value.ToString(), 0.0);
            Deck_Analysis.Number_Of_Long_Girder = MyList.StringToInt(txt_Ana_NMG.Text, 4);
            Deck_Analysis.Number_Of_Cross_Girder = MyList.StringToInt(txt_Ana_NCG.Text, 3);
            Deck_Analysis.WidthBridge = L / (Deck_Analysis.Number_Of_Cross_Girder - 1);

            //Deck_Analysis.Lwv = MyList.StringToDouble(txt_Ana_Lwv.Text, 0.0);
            //Deck_Analysis.Wkerb = MyList.StringToDouble(txt_Ana_Wkerb.Text, 0.0);


        }
        void Ana_Write_Long_Girder_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";

            if (add_DeadLoad)
            {
                load_lst.AddRange(txt_member_load.Lines);

                if (add_LiveLoad)
                {
                    load_lst.Insert(0, "LOAD 1 DL + SIDL");
                    for (i = 1; i < load_lst.Count; i++)
                    {
                        if (load_lst[i].StartsWith("LOAD"))
                            load_lst[i] = "*" + load_lst[i];
                    }
                }

            }
            else
            {
                load_lst.AddRange(self_member_load.ToArray());


                if(HA_Lanes.Count > 0)
                {
                    load_lst.Add(string.Format("{0} UNI GY -{1}", Bridge_Analysis.HA_Loading_Members, txt_HA_UDL.Text));

                    //load_lst.Add(string.Format("{0} CON GZ -{1} 0.5", Bridge_Analysis.HA_Loading_Members, txt_HA_CON.Text));


                    foreach (var item in MyList.Get_Array_Intiger(Bridge_Analysis.HA_Loading_Members))
                    {
                        load_lst.Add(string.Format("{0} CON GY -{1} {2:f3}", item, txt_HA_CON.Text, Bridge_Analysis.MemColls.Get_Member_Length(item.ToString()) / 2));
                    }
                }
                //load_lst.Add("LOAD 1");
                //load_lst.Add("MEMBER LOAD");
                //load_lst.Add("1 TO " + Bridge_Analysis.MemColls.Count  + " UNI GY -0.001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Bridge_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                load_lst.AddRange(all_loads[load_no].ToArray());
                if (long_ll.Count > 0)
                    File.WriteAllLines(MyList.Get_LL_TXT_File(file_name), long_ll.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        void Ana_Write_DeckSlab_Load_Data(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";

            if (add_DeadLoad)
            {

                load_lst.Add("LOAD 1 DEAD LOAD");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[0]));

                load_lst.Add("LOAD 2 SIDL EXCEPT SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[1]));

                load_lst.Add("LOAD 3 SIDL SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[2]));

                load_lst.Add("LOAD 4 FPLL");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, 0.0));

            }
            else
            {
                //load_lst.Add("LOAD 1");
                //load_lst.Add("MEMBER LOAD");
                //load_lst.Add("1 TO " + Deck_Analysis.MemColls.Count + " UNI GY -0.001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                List<string> list = new List<string>();
                //list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));

                if (load_no == 1)
                {
                    list.AddRange(deck_ll_1.ToArray());
                }
                if (load_no == 2)
                {
                    list.AddRange(deck_ll_2.ToArray());
                }
                if (load_no == 3)
                {
                    list.AddRange(deck_ll_3.ToArray());

                }
                if (load_no == 4)
                {
                    list.AddRange(deck_ll_4.ToArray());

                }
                if (load_no == 5)
                {
                    list.AddRange(deck_ll_5.ToArray());

                }
                if (load_no == 6)
                {
                    list.AddRange(deck_ll_6.ToArray());
                }
                load_lst.AddRange(list.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Ana_Write_DeckSlab_Load_Data_2014_08_25(string file_name, bool add_LiveLoad, bool add_DeadLoad, int load_no)
        {
            //string file_name = Bridge_Analysis.Input_File;
            //= Bridge_Analysis.TotalAnalysis_Input_File;
            if (!File.Exists(file_name)) return;

            List<string> inp_file_cont = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            int indx = -1;
            bool flag = false;
            MyList mlist = null;
            int i = 0;

            bool isMoving_load = false;
            for (i = 0; i < inp_file_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(inp_file_cont[i].ToUpper());
                mlist = new MyList(kStr, ' ');

                if (kStr.Contains("LOAD GEN"))
                    isMoving_load = true;

                if (mlist.StringList[0].StartsWith("LOAD") && flag == false)
                {
                    if (indx == -1)
                        indx = i;
                    flag = true;
                }
                if (kStr.Contains("ANALYSIS") || kStr.Contains("PRINT"))
                {
                    flag = false;
                }
                if (flag)
                {
                    inp_file_cont.RemoveAt(i);
                    i--;
                }

            }

            List<string> load_lst = new List<string>();

            string s = " DL";

            if (add_DeadLoad)
            {

                load_lst.Add("LOAD 1 DEAD LOAD");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[0]));

                load_lst.Add("LOAD 2 SIDL EXCEPT SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[1]));

                load_lst.Add("LOAD 3 SIDL SURFACING");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, deck_member_load[2]));

                load_lst.Add("LOAD 4 FPLL");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add(string.Format("{0} UNI GY -{1:f4}", Deck_Analysis.Outer_Girders_as_String, 0.0));

            }
            else
            {
                load_lst.Add("LOAD 1");
                load_lst.Add("MEMBER LOAD");
                load_lst.Add("1 TO " + Deck_Analysis.MemColls.Count + " UNI GY -0.001");
            }

            //Bridge_Analysis.LoadReadFromGrid(dgv_live_load);

            //Bridge_Analysis.Live_Load_List = iApp.LiveLoads;
            Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));
            if (add_LiveLoad)
            {

                List<string> list = new List<string>();
                list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));

                if (load_no == 1)
                {
                    list.AddRange(deck_ll_1.ToArray());
                }
                if (load_no == 2)
                {
                    list.AddRange(deck_ll_2.ToArray());
                }
                if (load_no == 3)
                {
                    list.AddRange(deck_ll_3.ToArray());

                }
                if (load_no == 4)
                {
                    list.AddRange(deck_ll_4.ToArray());

                }
                if (load_no == 5)
                {
                    list.AddRange(deck_ll_5.ToArray());

                }
                if (load_no == 6)
                {
                    list.AddRange(deck_ll_6.ToArray());
                }
                load_lst.AddRange(list.ToArray());
            }
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Show_Long_Girder_Moment_Shear()
        {
            if (Bridge_Analysis.MemColls == null)
                Bridge_Analysis.CreateData(true);
            MemberCollection mc = new MemberCollection(Bridge_Analysis.MemColls);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Bridge_Analysis.Joints;

            double L = Bridge_Analysis.Length;
            double W = Bridge_Analysis.WidthBridge;

            //Bridge_Analysis.Effective_Depth
            double val = L / 2;
            int i = 0;

            List<int> _support_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();
            List<int> _L8_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _3L8_inn_joints = new List<int>();
            List<int> _L2_inn_joints = new List<int>();



            List<int> _support_out_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _3L8_out_joints = new List<int>();
            List<int> _L2_out_joints = new List<int>();




            //List<int> _L4_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();

            List<int> _cross_joints = new List<int>();


            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Deff;

            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {
                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < jn_col.Count; i++)
                {
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

                    if (_Z_joints[zc] == jn_col[i].Z)
                    {
                        if (x_min > jn_col[i].X)
                            x_min = jn_col[i].X;
                        if (x_max < jn_col[i].X)
                            x_max = jn_col[i].X;
                    }
                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }
            if (_X_min.Count > 0)
            {
                _X_min.Sort();
                x_min = _X_min[0];
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Bridge_Analysis.Effective_Depth;


            double cant_wi_left = Bridge_Analysis.Width_LeftCantilever;
            double cant_wi_right = Bridge_Analysis.Width_RightCantilever;
            //Bridge_Analysis.Width_LeftCantilever = cant_wi;
            //Bridge_Analysis.Width_RightCantilever = _Z_joints[_Z_joints.Count - 1] - _Z_joints[_Z_joints.Count - 3];


            #region Find Joints
            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _3L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _3L8_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if ((jn_col[i].X.ToString("0.0") == (Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Bridge_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }

                    //if (jn_col[i].X.ToString("0.0") == (x_min).ToString("0.0"))
                    //{
                    //    if (jn_col[i].Z >= cant_wi_left)
                    //        _support_inn_joints.Add(jn_col[i].NodeNo);
                    //}
                    //if (jn_col[i].X.ToString("0.0") == (L + x_min).ToString("0.0"))
                    //{
                    //    if (jn_col[i].Z <= (W - cant_wi_right))
                    //        _support_out_joints.Add(jn_col[i].NodeNo);
                    //}
                    if (jn_col[i].X.ToString("0.0") == (Bridge_Analysis.Support_Distance + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _support_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == (L - Bridge_Analysis.Support_Distance + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _support_out_joints.Add(jn_col[i].NodeNo);
                    }

                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }

            #endregion Find Joints

            Result.Clear();
            Result.Add("");
            Result.Add("");
            //Result.Add("Analysis Result of RCC T-BEAM Bridge");
            Result.Add("");


            BeamMemberForce f = null;




            List<int> tmp_jts = new List<int>();


            List<BeamMemberForce> ll_SF = new List<BeamMemberForce>();
            List<BeamMemberForce> ll_BM = new List<BeamMemberForce>();



            List<List<int>> All_inn_Joints = new List<List<int>>();

            All_inn_Joints.Add(_support_inn_joints);
            All_inn_Joints.Add(_deff_inn_joints);
            All_inn_Joints.Add(_L4_inn_joints);
            All_inn_Joints.Add(_3L8_inn_joints);
            All_inn_Joints.Add(_L2_inn_joints);

            List<List<int>> All_out_Joints = new List<List<int>>();

            All_out_Joints.Add(_support_out_joints);
            All_out_Joints.Add(_deff_out_joints);
            All_out_Joints.Add(_L4_out_joints);
            All_out_Joints.Add(_3L8_out_joints);
            All_out_Joints.Add(_L2_out_joints);

            int j = 0;
            List<string> list_SF = new List<string>();
            List<string> list_BM = new List<string>();
            List<string> list = new List<string>();



            MyList ml = null;

            List<string> ll_type = new List<string>();

            for (i = 0; i < long_ll_types.Count; i++)
            {
                ml = new MyList(long_ll_types[i], ' ');
                ll_type.Add(ml[2]);
            }

            //list.Add(string.Format("1 Lane Type 3 most Eccentric"));
            //list.Add(string.Format("1 Lane Type 3 on member"));
            //list.Add(string.Format("1 Lane Type 3 placed concentrically"));
            //list.Add(string.Format("1 Lane Type 1 Placed most Eccentrically"));
            //list.Add(string.Format("2 Lane Type 1 Placed most Eccentrically"));
            //list.Add(string.Format("3 Lane Type 1 Placed most concentrically"));
            //list.Add(string.Format("1 Lane Type 1 + 1 Lane Type 3"));
            //list.Add(string.Format("1 Lane Type 3 + 1 Lane Type 1"));
            //list.Add(string.Format("1 Lane Type 3 + 1 Lane Type 1"));
            //list.Add(string.Format("2 Lane Type 1 PLACED AFTER Type 2"));
            //list.Add(string.Format("2 Lane Type 2 PLACED MOST ECCENTRICALLY"));
            //list.Add(string.Format("2 Lane Type 2 PLACED AT CENTER OF THE EACH LANE"));
            //list.Add(string.Format("Type 2 PLACED AT THE CENTER OF INNER GIRDER"));



            //list.Add(string.Format("1 LANE {0} MOST ECCENTRIC", ll_type[2]));
            //list.Add(string.Format("1 LANE {0} ON MEMBER", ll_type[2]));
            //list.Add(string.Format("1 LANE {0} PLACED CONCENTRICALLY", ll_type[2]));
            //list.Add(string.Format("1 LANE {0} PLACED MOST ECCENTRICALLY", ll_type[0]));
            //list.Add(string.Format("2 LANE {0} PLACED MOST ECCENTRICALLY", ll_type[0]));
            //list.Add(string.Format("3 LANE {0} PLACED MOST CONCENTRICALLY", ll_type[0]));
            //list.Add(string.Format("1 LANE {0} + 1 LANE {1}", ll_type[0], ll_type[2]));
            //list.Add(string.Format("1 LANE {0} + 1 LANE {1}", ll_type[2], ll_type[0]));
            //list.Add(string.Format("1 LANE {0} + 1 LANE {1}", ll_type[2], ll_type[0]));
            //list.Add(string.Format("2 LANE {0} PLACED AFTER {1}", ll_type[0], ll_type[1]));
            //list.Add(string.Format("2 LANE {0} PLACED MOST ECCENTRICALLY", ll_type[1]));
            //list.Add(string.Format("2 LANE {0} PLACED AT CENTER OF THE EACH LANE", ll_type[1]));
            //list.Add(string.Format("{0} PLACED AT THE CENTER OF INNER GIRDER", ll_type[1]));


            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
         



            for (i = 3; i < Bridge_Analysis.All_Analysis.Count; i++)
            {
                ll_BM.Clear();
                ll_SF.Clear();
                list_SF.Clear();
                list_BM.Clear();
                for (j = 0; j < All_inn_Joints.Count; j++)
                {
                    tmp_jts.Clear();
                    tmp_jts.AddRange(All_inn_Joints[j].ToArray());
                    tmp_jts.AddRange(All_out_Joints[j].ToArray());


                    f = Bridge_Analysis.All_Analysis[i].GetJoint_BendingMoment_Corrs_ShearForce(tmp_jts, true);
                    ll_BM.Add(f);
                    list_BM.Add(string.Format("BM SEC.({0}-{0})    {1,10:f3} t-m            SF SEC.({0}-{0})    {2,10:f3} t", (j + 1), Math.Abs(f.MaxBendingMoment), Math.Abs(f.MaxShearForce)));

                    f = Bridge_Analysis.All_Analysis[i].GetJoint_ShearForce_Corrs_BendingMoment(tmp_jts, true);
                    ll_SF.Add(f);

                    list_SF.Add(string.Format("BM SEC.({0}-{0})    {1,10:f3} t-m            SF SEC.({0}-{0})    {2,10:f3} t", (j + 1), Math.Abs(f.MaxBendingMoment), Math.Abs(f.MaxShearForce)));
                }

                Result.Add(string.Format(""));
                Result.Add(string.Format("----------------------------------------------------------------------------"));
                //Result.Add(string.Format("LOAD ANALYSIS TYPE : {0}", i));
                Result.Add(string.Format("LIVE LOAD ANALYSIS {0}  : {1}", (i - 2), list[i - 3].ToUpper()));
                Result.Add(string.Format("----------------------------------------------------------------------------"));
                Result.Add(string.Format("Maximum BM & corrs. SF"));
                Result.Add(string.Format("-----------------------"));
                Result.AddRange(list_BM.ToArray());
                Result.Add(string.Format(""));
                Result.Add(string.Format("Maximum SF & corrs. BM"));
                Result.Add(string.Format("-----------------------"));
                Result.AddRange(list_SF.ToArray());
                Result.Add(string.Format(""));
                Result.Add(string.Format("----------------------------------------------------------------------------"));

            }
            #region Vertical Deflection


            List<int> outer_joints = new List<int>();
            List<int> outer_joints_right = new List<int>();

            outer_joints.Add(_support_inn_joints[0]);
            outer_joints_right.Add(_support_inn_joints[_support_inn_joints.Count - 1]);

            //outer_joints.Add(_L8_inn_joints[0]);
            //outer_joints_right.Add(_L8_inn_joints[_L8_inn_joints.Count - 1]);

            //outer_joints.Add(_3L16_inn_joints[0]);
            //outer_joints_right.Add(_3L16_inn_joints[_3L16_inn_joints.Count - 1]);

            outer_joints.Add(_L4_inn_joints[0]);
            outer_joints_right.Add(_L4_inn_joints[_L4_inn_joints.Count - 1]);

            //outer_joints.Add(_5L16_inn_joints[0]);
            //outer_joints_right.Add(_5L16_inn_joints[_5L16_inn_joints.Count - 1]);

            outer_joints.Add(_3L8_inn_joints[0]);
            outer_joints_right.Add(_3L8_inn_joints[_3L8_inn_joints.Count - 1]);

            //outer_joints.Add(_7L16_inn_joints[0]);
            //outer_joints_right.Add(_7L16_inn_joints[_7L16_inn_joints.Count - 1]);

            outer_joints.Add(_L2_inn_joints[0]);
            outer_joints_right.Add(_L2_inn_joints[_L2_inn_joints.Count - 1]);



            //outer_joints.Add(_7L16_out_joints[0]);
            //outer_joints_right.Add(_7L16_out_joints[_7L16_out_joints.Count - 1]);




            outer_joints.Add(_3L8_out_joints[0]);
            outer_joints_right.Add(_3L8_out_joints[_3L8_out_joints.Count - 1]);




            //outer_joints.Add(_5L16_out_joints[0]);
            //outer_joints_right.Add(_5L16_out_joints[_5L16_out_joints.Count - 1]);



            outer_joints.Add(_L4_out_joints[0]);
            outer_joints_right.Add(_L4_out_joints[_L4_out_joints.Count - 1]);



            //outer_joints.Add(_3L16_out_joints[0]);
            //outer_joints_right.Add(_3L16_out_joints[_3L16_out_joints.Count - 1]);



            //outer_joints.Add(_L8_out_joints[0]);
            //outer_joints_right.Add(_L8_out_joints[_L8_out_joints.Count - 1]);



            outer_joints.Add(_support_out_joints[0]);
            outer_joints_right.Add(_support_out_joints[_support_out_joints.Count - 1]);






            //iApp.Progress_ON("Reading Maximum deflection....");


            List<NodeResultData> lst_nrd = new List<NodeResultData>();

            //iApp.SetProgressValue(10, 100);


            for (i = 1; i < Bridge_Analysis.All_Analysis.Count; i++)
            {
                lst_nrd.Add(Bridge_Analysis.All_Analysis[i].Node_Displacements.Get_Max_Deflection());
                //iApp.SetProgressValue(20 + i * 3, 100);

                //lst_nrd.Add(Long_Girder_Analysis.LiveLoad_1_Analysis.Node_Displacements.Get_Max_Deflection());
                //iApp.SetProgressValue(30, 100);
                //lst_nrd.Add(Long_Girder_Analysis.LiveLoad_2_Analysis.Node_Displacements.Get_Max_Deflection());
                //iApp.SetProgressValue(40, 100);
                //lst_nrd.Add(Long_Girder_Analysis.LiveLoad_3_Analysis.Node_Displacements.Get_Max_Deflection());
                //lst_nrd.Add(Long_Girder_Analysis.LiveLoad_4_Analysis.Node_Displacements.Get_Max_Deflection());
                //iApp.SetProgressValue(50, 100);
                //lst_nrd.Add(Long_Girder_Analysis.LiveLoad_5_Analysis.Node_Displacements.Get_Max_Deflection());
                //lst_nrd.Add(Long_Girder_Analysis.LiveLoad_6_Analysis.Node_Displacements.Get_Max_Deflection());
                //iApp.SetProgressValue(60, 100);
            }


            //lst_nrd.Add(Long_Girder_Analysis.DeadLoad_Analysis.Node_Displacements.Get_Max_Deflection());


            int max_indx = 0;
            double max_def, allow_def;


            max_def = 0;
            allow_def = (L / 800.0);

            for (i = 0; i < lst_nrd.Count; i++)
            {
                if (max_def < Math.Abs(lst_nrd[i].Max_Translation))
                {
                    max_def = Math.Abs(lst_nrd[i].Max_Translation);
                    max_indx = i;
                }
            }



            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format("---------------------------------------------------------------------------------"));
            Result.Add(string.Format("CHECK FOR LIVE LOAD DEFLECTION"));
            Result.Add(string.Format("---------------------------------------------------------------------------------"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-"));
            Result.Add(string.Format(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(lst_nrd[max_indx].ToString()));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));


            Result.Add(string.Format("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1} M. ", L, allow_def));
            Result.Add(string.Format(""));
            if (max_def > allow_def)
                Result.Add(string.Format("MAXIMUM  VERTICAL DEFLECTION = {0:f6} M. > {1:f6} M. ", max_def, allow_def));
            else
                Result.Add(string.Format("MAXIMUM  VERTICAL DEFLECTION = {0:f6} M. < {1:f6} M. ", max_def, allow_def));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format("---------------------------------------------------------------------------------"));
            Result.Add(string.Format("CHECK FOR DEAD LOAD DEFLECTION LEFT SIDE OUTER GIRDER"));
            Result.Add(string.Format("---------------------------------------------------------------------------------"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-"));
            Result.Add(string.Format(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION"));
            Result.Add(string.Format(""));
            lst_nrd.Clear();
            for (i = 0; i < outer_joints.Count; i++)
            {
                lst_nrd.Add(Bridge_Analysis.All_Analysis[0].Node_Displacements.Get_Node_Deflection(outer_joints[i]));
                Result.Add(string.Format(lst_nrd[i].ToString()));
            }
            //iApp.SetProgressValue(70, 100);

            Result.Add(string.Format(""));
            Result.Add(string.Format("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1} M. ", L, allow_def));
            Result.Add(string.Format(""));
            Result.Add(string.Format("MAXIMUM NODE DISPLACEMENTS FOR LEFT SIDE OUTER LONG GIRDER"));
            Result.Add(string.Format("----------------------------------------------------------"));
            Result.Add(string.Format(""));
            for (i = 0; i < lst_nrd.Count; i++)
            {
                if (lst_nrd[i].Max_Translation < allow_def)
                    Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. < {2:f6} M.    OK.", lst_nrd[i].NodeNo, lst_nrd[i].Max_Translation, allow_def));
                else
                    Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. > {2:f6} M.    NOT OK.", lst_nrd[i].NodeNo, Math.Abs(lst_nrd[i].Max_Translation), allow_def));
            }

            //iApp.SetProgressValue(80, 100);


            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format("---------------------------------------------------------------------------------"));
            Result.Add(string.Format("CHECK FOR DEAD LOAD DEFLECTION RIGHT SIDE OUTER GIRDER"));
            Result.Add(string.Format("---------------------------------------------------------------------------------"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(" MAXIMUM     NODE DISPLACEMENTS / ROTATIONS"));
            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format(" NODE     LOAD          X-            Y-            Z-             X-               Y-            Z-"));
            Result.Add(string.Format(" NUMBER   CASE    TRANSLATION    TRANSLATION    TRANSLATION     ROTATION        ROTATION      ROTATION"));
            Result.Add(string.Format(""));
            lst_nrd.Clear();
            for (i = 0; i < outer_joints_right.Count; i++)
            {
                lst_nrd.Add(Bridge_Analysis.All_Analysis[0].Node_Displacements.Get_Node_Deflection(outer_joints_right[i]));
                Result.Add(string.Format(lst_nrd[i].ToString()));
            }
            //iApp.SetProgressValue(90, 100);

            Result.Add(string.Format(""));
            Result.Add(string.Format("ALLOWABLE DEFLECTION = SPAN/800 M. = {0}/800 M. = {1} M. ", L, allow_def));
            Result.Add(string.Format(""));
            Result.Add(string.Format("MAXIMUM NODE DISPLACEMENTS FOR RIGHT SIDE OUTER LONG GIRDER"));
            Result.Add(string.Format("------------------------------------------------------------"));
            Result.Add(string.Format(""));
            for (i = 0; i < lst_nrd.Count; i++)
            {
                if (lst_nrd[i].Max_Translation < allow_def)
                    Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. < {2:f6} M.    OK.", lst_nrd[i].NodeNo, lst_nrd[i].Max_Translation, allow_def));
                else
                    Result.Add(string.Format("MAXIMUM  VERTICAL DISPLACEMENT AT NODE {0}  = {1:f6} M. > {2:f6} M.    NOT OK.", lst_nrd[i].NodeNo, Math.Abs(lst_nrd[i].Max_Translation), allow_def));
            }

            //iApp.SetProgressValue(100, 100);

            Result.Add(string.Format(""));
            Result.Add(string.Format(""));
            Result.Add(string.Format("The Deflection for Dead Load is to be controlled by providing Longitudinal"));
            Result.Add(string.Format("Camber along the length of the Main Girder between end to end supports."));

            //iApp.Progress_OFF();

            #endregion Vertical Deflection

            Result.Add(string.Format(""));
            rtb_live_load_results.Lines = Result.ToArray();

            File.WriteAllLines(File_Long_Girder_Results, Result.ToArray());

            iApp.RunExe(File_Long_Girder_Results);
            Result.Add(string.Format(""));
        }

        void Show_Deckslab_Moment_Shear()
        {
            MemberCollection mc = new MemberCollection(Deck_Analysis.LiveLoad_2_Analysis.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = Deck_Analysis.LiveLoad_2_Analysis.Analysis.Joints;

            #region Deck Model

            double L = Deck_Analysis.Length;
            double W = Deck_Analysis.WidthBridge;
            double val = L / 2;
            int i = 0;

            Deck_Analysis.Effective_Depth = L / 16.0; ;


            List<int> _support_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();
            List<int> _L8_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _3L8_inn_joints = new List<int>();
            List<int> _L2_inn_joints = new List<int>();



            List<int> _3L16_inn_joints = new List<int>();
            List<int> _5L16_inn_joints = new List<int>();
            List<int> _7L16_inn_joints = new List<int>();



            List<int> _support_out_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _3L8_out_joints = new List<int>();
            List<int> _L2_out_joints = new List<int>();


            List<int> _3L16_out_joints = new List<int>();
            List<int> _5L16_out_joints = new List<int>();
            List<int> _7L16_out_joints = new List<int>();



            //List<int> _L4_out_joints = new List<int>();
            //List<int> _deff_out_joints = new List<int>();

            List<int> _cross_joints = new List<int>();


            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }
            //val = MyList.StringToDouble(txt_Ana_eff_depth.Text, -999.0);
            val = Deff;

            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {
                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < jn_col.Count; i++)
                {
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

                    if (_Z_joints[zc] == jn_col[i].Z)
                    {
                        if (x_min > jn_col[i].X)
                            x_min = jn_col[i].X;
                        if (x_max < jn_col[i].X)
                            x_max = jn_col[i].X;
                    }
                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }
            #endregion Deck Model

            Deck_Analysis.TotalLoad_Analysis = Deck_Analysis.LiveLoad_2_Analysis;

            val = Deck_Analysis.LiveLoad_2_Analysis.Analysis.Effective_Depth;

            double cant_wi_left = 0.0;
            double cant_wi_right = 0.0;

            #region Find Joints
            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left && jn_col[i].Z <= (W - cant_wi_right))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _3L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _3L8_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if ((jn_col[i].X.ToString("0.0") == (Deck_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - Deck_Analysis.Effective_Depth + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }

                    if (jn_col[i].X.ToString("0.0") == (x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _support_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == (L + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _support_out_joints.Add(jn_col[i].NodeNo);
                    }

                    #region 3L/16


                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 16.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _3L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 16.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _3L16_out_joints.Add(jn_col[i].NodeNo);
                    }


                    #endregion 3L/16



                    #region 5L/16


                    if (jn_col[i].X.ToString("0.0") == ((5 * L / 16.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _5L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (5 * L / 16.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _5L16_out_joints.Add(jn_col[i].NodeNo);
                    }


                    #endregion 3L/16



                    #region 7L/16


                    if (jn_col[i].X.ToString("0.0") == ((7 * L / 16.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi_left)
                            _7L16_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (7 * L / 16.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi_right))
                            _7L16_out_joints.Add(jn_col[i].NodeNo);
                    }


                    #endregion 3L/16



                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }

            #endregion Find Joints

            Result.Clear();
            Result.Add("");
            Result.Add("");
            //Result.Add("Analysis Result of RCC T-BEAM Bridge");
            Result.Add("");


            #region Maximum Hogging

            List<double> Max_Hog_LL_1 = new List<double>();
            List<double> Max_Hog_LL_2 = new List<double>();
            List<double> Max_Hog_LL_3 = new List<double>();
            List<double> Max_Hog_LL_4 = new List<double>();
            List<double> Max_Hog_LL_5 = new List<double>();
            List<double> Max_Hog_LL_6 = new List<double>();


            List<double> Max_Hog_LL_7 = new List<double>();

            int index = 0;

            MaxForce f = null;

            List<int> tmp_jts = new List<int>();

            #region Live Load SHEAR Force

            #region Maximum Hogging Live Load 1

            Max_Hog_LL_1.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_deff_inn_joints[index]);
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_L8_inn_joints[index]);
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_3L16_inn_joints[index]);
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_L4_inn_joints[index]);
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_5L16_inn_joints[index]);
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_3L8_inn_joints[index]);
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_7L16_inn_joints[index]);
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L2_inn_joints[index]);
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_7L16_out_joints[index]);
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.AddRange(_3L8_out_joints.ToArray());
            tmp_jts.Add(_3L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.AddRange(_5L16_out_joints.ToArray());
            tmp_jts.Add(_5L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L4_out_joints[index]);
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_3L16_out_joints[index]);
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_L8_out_joints[index]);
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);

            tmp_jts.Clear();
            //tmp_jts.Add(_deff_out_joints[index]);
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_1.Add(val);


            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 2

            Max_Hog_LL_2.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_2.Add(val);


            #endregion Maximum Hogging LL 2


            Max_Hog_LL_3.Clear();
            #region Maximum Hogging Live Load 1

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_3.Add(val);



            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 4

            Max_Hog_LL_4.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_4.Add(val);



            #endregion Maximum Hogging LL 1

            #region Maximum Hogging Live Load 5

            Max_Hog_LL_5.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_5.Add(val);


            #endregion Maximum Hogging LL 5

            #region Maximum Hogging Live Load 6

            Max_Hog_LL_6.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Hogging(tmp_jts, true);
            val = f.Force;
            Max_Hog_LL_6.Add(val);



            #endregion Maximum Hogging LL 6

            #endregion Live Load SHEAR Force

            MyList.Array_Multiply_With(ref Max_Hog_LL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_3, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_4, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_5, 10.0);
            MyList.Array_Multiply_With(ref Max_Hog_LL_6, 10.0);


            #endregion Maximum Hogging

            #region Maximum Sagging

            List<double> Max_Sag_LL_1 = new List<double>();
            List<double> Max_Sag_LL_2 = new List<double>();
            List<double> Max_Sag_LL_3 = new List<double>();
            List<double> Max_Sag_LL_4 = new List<double>();
            List<double> Max_Sag_LL_5 = new List<double>();
            List<double> Max_Sag_LL_6 = new List<double>();


            List<double> Max_Sag_LL_7 = new List<double>();




            #region Live Load SHEAR Force


            #region Maximum Hogging Live Load 1

            Max_Sag_LL_1.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_1.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_1_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_1.Add(val);

            #endregion Maximum Hogging LL 1


            #region Maximum Hogging Live Load 2

            Max_Sag_LL_2.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_2.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_2_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_2.Add(val);



            #endregion Maximum Hogging LL 2


            #region Maximum Hogging Live Load 1

            Max_Sag_LL_3.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_3.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);





            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_3_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_3.Add(val);



            #endregion Maximum Hogging LL 1


            #region Maximum Hogging Live Load 4

            Max_Sag_LL_4.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_4.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_4_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_4.Add(val);



            #endregion Maximum Hogging LL 1


            #region Maximum Hogging Live Load 5

            Max_Sag_LL_5.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_5.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_5_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_5.Add(val);



            #endregion Maximum Hogging LL 5



            #region Maximum Hogging Live Load 6

            Max_Sag_LL_6.Clear();

            //tmp_jts.Clear();
            //tmp_jts.AddRange(_support_inn_joints.ToArray());
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);


            //tmp_jts.Clear();
            //tmp_jts.Add(_support_out_joints[index]);
            //f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            //val = f.Force;
            //SF_LL_6.Add(val);



            tmp_jts.Clear();
            tmp_jts.Add(_deff_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_L4_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_5L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_3L8_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_7L16_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L2_inn_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_7L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_3L8_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.AddRange(_5L16_out_joints.ToArray());
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L4_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);


            tmp_jts.Clear();
            tmp_jts.Add(_3L16_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_L8_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);

            tmp_jts.Clear();
            tmp_jts.Add(_deff_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);




            tmp_jts.Clear();
            tmp_jts.Add(_support_out_joints[index]);
            f = Deck_Analysis.LiveLoad_6_Analysis.GetJoint_Max_Sagging(tmp_jts, true);
            val = f.Force;
            Max_Sag_LL_6.Add(val);



            #endregion Maximum Hogging LL 6



            #endregion Live Load SHEAR Force

            MyList.Array_Multiply_With(ref Max_Sag_LL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_3, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_4, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_5, 10.0);
            MyList.Array_Multiply_With(ref Max_Sag_LL_6, 10.0);


            #endregion Maximum Hogging





            #region DL + SIDL + FPLL

            List<double> Max_Moment_DL = new List<double>();
            List<double> Max_Moment_SIDL_1 = new List<double>();
            List<double> Max_Moment_SIDL_2 = new List<double>();
            List<double> Max_Moment_FPLL = new List<double>();

            #region _deff_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_deff_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _deff_inn_joints

            #region _L8_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L8_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _L8_inn_joints

            #region _3L16_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_3L16_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _deff_inn_joints

            #region _L4_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L4_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _L4_inn_joints


            #region _5L16_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_5L16_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _deff_inn_joints


            #region _3L8_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_3L8_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _3L8_inn_joints


            #region _7L16_inn_joints
            tmp_jts.Clear();


            tmp_jts.Add(_7L16_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _7L16_inn_joints


            #region _L2_inn_joints

            tmp_jts.Clear();


            tmp_jts.Add(_L2_inn_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _L2_inn_joints


            #region _7L16_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_7L16_out_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _7L16_out_joints


            #region _3L8_out_joints
            tmp_jts.Clear();

            tmp_jts.AddRange(_3L8_out_joints.ToArray());

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _3L8_out_joints


            #region _5L16_out_joints
            tmp_jts.Clear();


            tmp_jts.AddRange(_5L16_out_joints.ToArray());

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _5L16_out_joints


            #region _L4_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L4_out_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _L4_out_joints

            #region _3L16_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_3L16_out_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _3L16_out_joints

            #region _L8_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_L8_out_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);
            #endregion _L8_out_joints

            #region _deff_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_deff_out_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _deff_out_joints

            #region _support_out_joints
            tmp_jts.Clear();


            tmp_jts.Add(_support_out_joints[index]);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 1);
            val = f.Force;
            Max_Moment_DL.Add(val);


            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 2);
            val = f.Force;
            Max_Moment_SIDL_1.Add(val);



            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 3);
            val = f.Force;
            Max_Moment_SIDL_2.Add(val);

            f = Deck_Analysis.DeadLoad_Analysis.GetJoint_MomentForce(tmp_jts[0], true, 4);
            val = f.Force;
            Max_Moment_FPLL.Add(val);

            #endregion _support_out_joints



            MyList.Array_Multiply_With(ref Max_Moment_DL, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_SIDL_1, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_SIDL_2, 10.0);
            MyList.Array_Multiply_With(ref Max_Moment_FPLL, 10.0);

            #endregion DL + SIDL + FPLL

            val = 0.0;


            //string format = "{0,-25} {1,12:f3} {2,10:f3} {3,15:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3} {12,10:f3} {13,10:f3} {14,10:f3} {15,10:f3} {16,10:f3}";
            string format = "{0,-25} {1,12:f3} {2,10:f3} {3,15:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3} {10,10:f3} {11,10:f3} {12,10:f3} {13,10:f3} {14,10:f3} {15,10:f3}";

            Result.Add(string.Format(""));

            #region Summary 1

            Result.Add(string.Format(""));
            Result.Add(string.Format("-------------------------------------------------------------"));
            Result.Add(string.Format("RCC T GIRDER (LIMIT STATE METHOD) DECK SLAB ANALYSIS RESULTS "));
            Result.Add(string.Format("--------------------------------------------------------------"));
            Result.Add(string.Format(""));
            Result.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            Result.Add(string.Format("                                NODE 2     NODE 3          NODE 4     NODE 5     NODE 6     NODE 7     NODE 8     NODE 9     NODE 10   NODE 11    NODE 12    NODE 13    NODE 14    NODE 15    NODE 16  "));
            //Result.Add(string.Format("                                NODE 5     NODE 8          NODE 11    NODE 14    NODE 17    NODE 20    NODE 23    NODE 26   NODE 29    NODE 32    NODE 35    NODE 38    NODE 41    NODE 44    NODE 47"));
            Result.Add(string.Format("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Result.Add(string.Format(""));
            int indx = 0;
            try
            {

                #region Max Dead Load

                indx = 0;
                Result.Add(string.Format(format,
                    "DL",
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++],
                    Max_Moment_DL[indx++]
                    ));

                #endregion  Max Dead Load


                #region Max SIDL (Except surfacing)

                indx = 0;
                Result.Add(string.Format(format,
                    "SIDL (Except surfacing)",
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++],
                    Max_Moment_SIDL_1[indx++]
                    ));

                #endregion  Max SIDL (Except surfacing)

                #region Max SIDL (Surfacing)

                indx = 0;
                Result.Add(string.Format(format,
                    "SIDL (Surfacing)",
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++],
                    Max_Moment_SIDL_2[indx++]
                    ));

                #endregion  Max SIDL (Surfacing)


                #region Max FPLL

                indx = 0;
                Result.Add(string.Format(format,
                    "FPLL",
                    Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++],
                     Max_Moment_FPLL[indx++]
                    ));

                #endregion  Max FPLL


                #region Max LL_1

                #region Max_Hog_LL_1
                indx = 0;
                Result.Add(string.Format(format,
                    "1CLA (Max. Hog)",
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++],
                    Max_Hog_LL_1[indx++]
                    ));
                #endregion Max_Hog_LL_1

                #region Max_Sag_LL_1
                indx = 0;
                Result.Add(string.Format(format,
                    "1CLA (Max. Sag)",
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++],
                    Max_Sag_LL_1[indx++]
                    ));
                #endregion Max_Hog_LL_1

                #endregion Max LL1

                #region Max LL_2
                #region Max_Hog_LL_2
                indx = 0;
                Result.Add(string.Format(format,
                    "2CLA (Max. Hog)",
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++],
                    Max_Hog_LL_2[indx++]
                    ));
                #endregion Max_Hog_LL_2

                #region Max_Sag_LL_2
                indx = 0;
                Result.Add(string.Format(format,
                    "2CLA (Max. Sag)",
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++],
                    Max_Sag_LL_2[indx++]
                    ));
                #endregion Max_Hog_LL_2

                #endregion Max LL1


                #region Max LL_3
                #region Max_Hog_LL_3
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-L (Max. Hog)",
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++],
                    Max_Hog_LL_3[indx++]
                    ));
                #endregion Max_Hog_LL_3

                #region Max_Sag_LL_3
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-L (Max. Sag)",
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++],
                    Max_Sag_LL_3[indx++]
                    ));
                #endregion Max_Hog_LL_3

                #endregion Max LL1

                #region Max LL_4
                #region Max_Hog_LL_4
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-M (Max. Hog)",
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++],
                    Max_Hog_LL_4[indx++]
                    ));
                #endregion Max_Hog_LL_4

                #region Max_Sag_LL_4
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 40T-M (Max. Sag)",
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++],
                    Max_Sag_LL_4[indx++]
                    ));
                #endregion Max_Hog_LL_4

                #endregion Max LL1

                #region Max LL_5
                #region Max_Hog_LL_5
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 70R TR (Max. Hog)",
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++],
                    Max_Hog_LL_5[indx++]
                    ));
                #endregion Max_Hog_LL_5

                #region Max_Sag_LL_5
                indx = 0;
                Result.Add(string.Format(format,
                    "1L 70R TR (Max. Sag)",
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++],
                    Max_Sag_LL_5[indx++]
                    ));
                #endregion Max_Hog_LL_5

                #endregion Max LL1

                #region Max LL_6
                #region Max_Hog_LL_6
                indx = 0;
                Result.Add(string.Format(format,
                    "1L70RW+1LCA(Max Hog)",
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++],
                    Max_Hog_LL_6[indx++]
                    ));
                #endregion Max_Hog_LL_6

                #region Max_Sag_LL_6
                indx = 0;
                Result.Add(string.Format(format,
                    "1L70RW+1LCA(Max Sag)",
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++],
                    Max_Sag_LL_6[indx++]
                    ));
                #endregion Max_Hog_LL_6

                #endregion Max LL1



                List<double> temp = new List<double>();
                for (i = 0; i < Max_Hog_LL_1.Count; i++)
                {
                    temp.Add(Max_Hog_LL_1[i]);
                    temp.Add(Max_Hog_LL_2[i]);
                    temp.Add(Max_Hog_LL_3[i]);
                    temp.Add(Max_Hog_LL_4[i]);
                    temp.Add(Max_Hog_LL_5[i]);
                    temp.Add(Max_Hog_LL_6[i]);

                    temp.Sort();
                    temp.Reverse();

                    Max_Hog_LL_7.Add(temp[0]);

                    temp.Clear();

                    temp.Add(Max_Sag_LL_1[i]);
                    temp.Add(Max_Sag_LL_2[i]);
                    temp.Add(Max_Sag_LL_3[i]);
                    temp.Add(Max_Sag_LL_4[i]);
                    temp.Add(Max_Sag_LL_5[i]);
                    temp.Add(Max_Sag_LL_6[i]);

                    temp.Sort();
                    //temp.Reverse();

                    Max_Sag_LL_7.Add(temp[0]);
                }



                #region Max LL_7
                #region Max_Hog_LL_7
                indx = 0;
                Result.Add(string.Format(format,
                    "LL (Max Hog)",
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++],
                    Max_Hog_LL_7[indx++]
                    ));
                #endregion Max_Hog_LL_7

                #region Max_Sag_LL_7
                indx = 0;
                Result.Add(string.Format(format,
                    "LL (Max Sag)",
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++],
                    Max_Sag_LL_7[indx++]
                    ));
                #endregion Max_Hog_LL_7

                #endregion Max LL1


            }
            catch (Exception exxx) { }
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            Result.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- "));
            Result.Add(string.Format(""));

            #endregion Summary 1




            Result.Add(string.Format(""));
            //rtb_ana_result.Lines = Result.ToArray();

            File.WriteAllLines(File_DeckSlab_Results, Result.ToArray());

            iApp.RunExe(File_DeckSlab_Results);
            Result.Add(string.Format(""));
        }


        public string File_Long_Girder_Results
        {
            get
            {
                return Path.Combine(user_path, "LONG_GIRDER_ANALYSIS_RESULT.TXT");
            }
        }
        public string File_DeckSlab_Results
        {
            get
            {
                return Path.Combine(user_path, "DECKSLAB_ANALYSIS_RESULT.TXT");
            }
        }

        public void Button_Enable_Disable()
        {
            btn_view_data.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_view_structure.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
            btn_View_Moving_Load.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            btn_view_report.Enabled = File.Exists(Bridge_Analysis.Total_Analysis_Report);
            //btn_process_analysis.Enabled = File.Exists(Long_Girder_Analysis.TotalAnalysis_Input_File);

            btn_RCC_Pier_Report.Enabled = File.Exists(rcc_pier.rep_file_name);
            btn_cnt_Report.Enabled = File.Exists(Abut.rep_file_name);

            if (Deck_Analysis != null)
            {
                btn_Deck_Analysis.Enabled = File.Exists(Deck_Analysis.Input_File);
                btn_LS_deck_ws.Enabled = File.Exists(File_DeckSlab_Results);
            }
            else
            {
                btn_Deck_Analysis.Enabled = false;
                btn_LS_deck_ws.Enabled = false;
            }
            if (Bridge_Analysis != null)
            {
                btn_ana_process_analysis.Enabled = File.Exists(Bridge_Analysis.TotalAnalysis_Input_File);
                //btn_LS_long_ws.Enabled = File.Exists(File_Long_Girder_Results);
            }

            Deck_Buttons();

            btn_LS_psc_rep_open.Enabled = File.Exists(Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_PSC_Girder)));
            btn_LS_deck_rep_open.Enabled = File.Exists(Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Deckslab)));


        }
        public void Ana_OpenAnalysisFile(string file_name)
        {
            string analysis_file = "";
            analysis_file = file_name;



            string usp = Path.Combine(user_path, "Deck Slab Analysis");
            if (Directory.Exists(usp))
            {
                Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
            }
            usp = Path.Combine(user_path, "PSC Girder Analysis");
            if (Directory.Exists(usp))
            {
                Bridge_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
            }


            if (File.Exists(analysis_file))
            {
                btn_view_structure.Enabled = true;
                Abut.FilePath = user_path;
                rcc_pier.FilePath = user_path;
            }
            Button_Enable_Disable();
        }

        List<string> load_list_1 = new List<string>();
        List<string> load_list_2 = new List<string>();
        List<string> load_list_3 = new List<string>();
        List<string> load_list_4 = new List<string>();
        List<string> load_list_5 = new List<string>();
        List<string> load_list_6 = new List<string>();
        List<string> load_total_7 = new List<string>();


        List<string> Ana_Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Analysis.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            double z_min = double.MaxValue;
            double x = double.MaxValue;
            int indx = -1;

            int i = 0;
            int j = 0;

            List<double> list_z = new List<double>();
            List<string> list_arr = new List<string>();

            List<MemberCollection> list_mc = new List<MemberCollection>();

            double last_z = 0.0;

            while (mc.Count != 0)
            {
                indx = -1;
                for (i = 0; i < mc.Count; i++)
                {
                    if (z_min > mc[i].StartNode.Z)
                    {
                        z_min = mc[i].StartNode.Z;
                        indx = i;
                    }
                }
                if (indx != -1)
                {

                    if (!list_z.Contains(z_min))
                        list_z.Add(z_min);

                    sort_membs.Add(mc[indx]);
                    mc.Members.RemoveAt(indx);
                    z_min = double.MaxValue;
                }
            }

            last_z = -1.0;

            //Inner & Outer Long Girder
            MemberCollection outer_long = new MemberCollection();
            MemberCollection inner_long = new MemberCollection();
            MemberCollection inner_cross = new MemberCollection();


            z_min = Bridge_Analysis.Joints.MinZ;
            double z_max = Bridge_Analysis.Joints.MaxZ;


            //Store inner and outer Long Girder
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (((sort_membs[i].StartNode.Z == z_min) || (sort_membs[i].StartNode.Z == z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    outer_long.Add(sort_membs[i]);
                }
                else if (((sort_membs[i].StartNode.Z != z_min) && (sort_membs[i].StartNode.Z != z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    inner_long.Add(sort_membs[i]);
                }
            }

            List<int> Outer_Joints = new List<int>();
            List<int> Inner_Joints = new List<int>();

            for (i = 0; i < outer_long.Count; i++)
            {
                if (Outer_Joints.Contains(outer_long[i].EndNode.NodeNo) == false)
                    Outer_Joints.Add(outer_long[i].EndNode.NodeNo);
                if (Outer_Joints.Contains(outer_long[i].StartNode.NodeNo) == false)
                    Outer_Joints.Add(outer_long[i].StartNode.NodeNo);
            }

            for (i = 0; i < inner_long.Count; i++)
            {
                if (Inner_Joints.Contains(inner_long[i].EndNode.NodeNo) == false)
                    Inner_Joints.Add(inner_long[i].EndNode.NodeNo);
                if (Inner_Joints.Contains(inner_long[i].StartNode.NodeNo) == false)
                    Inner_Joints.Add(inner_long[i].StartNode.NodeNo);
            }
            Outer_Joints.Sort();
            Inner_Joints.Sort();


            string inner_long_text = "";
            string outer_long_text = "";
            int last_val = 0;
            int to_val = 0;
            int from_val = 0;

            last_val = Outer_Joints[0];
            from_val = last_val;
            bool flag_1 = false;
            for (i = 0; i < Outer_Joints.Count; i++)
            {
                if (i < Outer_Joints.Count - 1)
                {
                    if ((Outer_Joints[i] + 1) == (Outer_Joints[i + 1]))
                    {
                        if (flag_1 == false)
                        {
                            from_val = Outer_Joints[i];
                        }
                        flag_1 = true;
                        to_val = Outer_Joints[i + 1];
                    }
                    else
                    {
                        if (flag_1)
                        {
                            outer_long_text = from_val + " TO " + to_val + " ";
                            flag_1 = false;
                        }
                        else
                        {
                            outer_long_text = outer_long_text + " " + last_val;
                        }
                    }
                    last_val = Outer_Joints[i];
                }
                else
                {
                    if (flag_1)
                    {
                        outer_long_text += from_val + " TO " + to_val + " ";
                        flag_1 = false;
                    }
                    else
                    {
                        outer_long_text = outer_long_text + " " + last_val;
                    }
                }
            }

            for (i = 0; i < Inner_Joints.Count; i++)
            {
                if (i < Inner_Joints.Count - 1)
                {
                    if ((Inner_Joints[i] + 1) == (Inner_Joints[i + 1]))
                    {
                        if (flag_1 == false)
                        {
                            from_val = Inner_Joints[i];
                        }
                        flag_1 = true;
                        to_val = Inner_Joints[i + 1];
                    }
                    else
                    {
                        if (flag_1)
                        {
                            inner_long_text = from_val + " TO " + to_val + " ";
                            flag_1 = false;
                        }
                        else
                        {
                            inner_long_text = inner_long_text + " " + last_val;
                        }
                    }
                    last_val = Inner_Joints[i];
                }
                else
                {
                    if (flag_1)
                    {
                        inner_long_text += from_val + " TO " + to_val + " ";
                        flag_1 = false;
                    }
                    else
                    {
                        inner_long_text = inner_long_text + " " + last_val;
                    }
                }
            }
            list_arr.Add(inner_long_text + " FY  -" + load.ToString("0.000"));
            list_arr.Add(outer_long_text + " FY  -" + (load / 2.0).ToString("0.000"));

            return list_arr;
        }
        public void Default_Moving_LoadData_Old(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            list.Add(string.Format("TYPE 1, IRCCLASSA"));
            list.Add(string.Format("AXLE LOAD IN TONS , 2.7,2.7,11.4,11.4,6.8,6.8,6.8,6.8"));
            list.Add(string.Format("AXLE SPACING IN METRES, 1.10,3.20,1.20,4.30,3.00,3.00,3.00"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.800"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 2, IRC70RTRACK"));
            list.Add(string.Format("AXLE LOAD IN TONS, 7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0"));
            list.Add(string.Format("AXLE SPACING IN METRES, 0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 2.900"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
            list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, IRC70RW40TBL"));
            list.Add(string.Format("AXLE LOAD IN TONS,10.0,10.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.93"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 5, IRC70RW40TBM"));
            list.Add(string.Format("AXLE LOAD IN TONS,5.0,5.0,5.0,5.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,0.795,0.38,0.795"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format(""));


            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }
        public void Default_Moving_LoadData(DataGridView dgv_live_load)
        {
            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            list.Clear();
            list.Add(string.Format("TYPE 1, IRCCLASSA"));
            list.Add(string.Format("AXLE LOAD IN TONS , 2.7,2.7,11.4,11.4,6.8,6.8,6.8,6.8"));
            list.Add(string.Format("AXLE SPACING IN METRES, 1.10,3.20,1.20,4.30,3.00,3.00,3.00"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.800"));
            list.Add(string.Format("IMPACT FACTOR, 1.179"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 2, IRC70RTRACK"));
            list.Add(string.Format("AXLE LOAD IN TONS, 7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0,7.0"));
            list.Add(string.Format("AXLE SPACING IN METRES, 0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457,0.457"));
            list.Add(string.Format("AXLE WIDTH IN METRES, 2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.25"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
            list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.25"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, IRC70RW40TBL"));
            list.Add(string.Format("AXLE LOAD IN TONS,10.0,10.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.93"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format("IMPACT FACTOR, 1.10"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 5, IRC70RW40TBM"));
            list.Add(string.Format("AXLE LOAD IN TONS,5.0,5.0,5.0,5.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,0.795,0.38,0.795"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.790"));
            list.Add(string.Format("IMPACT FACTOR, 1.10"));
            list.Add(string.Format(""));


            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        public void Default_Moving_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();

            if (dgv_live_load.Name == dgv_long_loads.Name)
            {
                #region Long Girder
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 3"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 3"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 3"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,5.9"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 1"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 5,TYPE 1,TYPE 1,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,1.5,4.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 6,TYPE 1,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,0,0,0"));
                list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 7,TYPE 1,TYPE 3,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,1.5,4.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 8,TYPE 3,TYPE 1,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,1.5,4.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 9,TYPE 3,TYPE 1,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,1.5,4.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 10,TYPE 1,TYPE 1,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,1.5,4.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 11,TYPE 2,TYPE 2,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,1.5,4.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 12,TYPE 2,TYPE 2,"));
                list.Add(string.Format("X,0,0,"));
                list.Add(string.Format("Z,2.5,5.5,"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 13,TYPE 2"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,2.5"));
                list.Add(string.Format(""));
                #endregion
            }
            else if (dgv_live_load.Name == dgv_deck_loads.Name)
            {
                #region Deck Slab
                list.Clear();
                list.Add(string.Format("LOAD 1,TYPE 1"));
                list.Add(string.Format("X,0"));
                list.Add(string.Format("Z,1.5 "));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2,TYPE 1,TYPE 1"));
                list.Add(string.Format("X,0,5.0"));
                list.Add(string.Format("z,1.5,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3,TYPE 4"));
                list.Add(string.Format("X,0.0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4,TYPE 5"));
                list.Add(string.Format("X,0.0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 5,TYPE 2"));
                list.Add(string.Format("X,0.0"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 6,TYPE 3,TYPE 1"));
                list.Add(string.Format("X,0.0,5.0"));
                list.Add(string.Format("Z,1.5,1.5"));
                list.Add(string.Format(""));
                #endregion
            }


            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                for (int j = 0; j < mlist.Count; j++)
                {
                    dgv_live_load[j, i].Value = mlist[j];
                }
            }
        }

        public void Deckslab_User_Input()
        {
            List<string> lst_input = new List<string>();

            #region user input
            lst_input.Add(string.Format("Distance between C/C Exp. Joint"));

            lst_input.Add(string.Format("Overhang of girder off the bearing"));
            lst_input.Add(string.Format("Overhang of slab off the bearing"));
            lst_input.Add(string.Format("Expansion Joint"));
            lst_input.Add(string.Format("Deck Width"));
            lst_input.Add(string.Format("Angle of skew"));
            lst_input.Add(string.Format("Clear carriage way"));
            lst_input.Add(string.Format("Width of outer railing"));
            lst_input.Add(string.Format("Width of Footpath"));
            lst_input.Add(string.Format("Width of Crash Barrier"));
            lst_input.Add(string.Format("Spacing of main girder c/c"));

            lst_input.Add(string.Format("Thk of deck slab"));
            lst_input.Add(string.Format("Thk of deck slab at overhang"));

            lst_input.Add(string.Format("Cantilever slab thk at fixed end"));
            lst_input.Add(string.Format("Cantilever slab thk at free end"));
            lst_input.Add(string.Format("Thk of wearing coat"));
            lst_input.Add(string.Format("No of main girder"));
            lst_input.Add(string.Format("Width of footh path"));
            lst_input.Add(string.Format("Width of raillings"));
            lst_input.Add(string.Format("Top Flange width of girder"));
            lst_input.Add(string.Format("No of Intermediate cross girder"));
            lst_input.Add(string.Format("Grade of concrete"));
            lst_input.Add(string.Format("Grade of reinforcement"));


            lst_input.Add(string.Format("Partial factor of safety  (Basic and seismic)"));
            lst_input.Add(string.Format("Partial factor of safety Accidental "));
            lst_input.Add(string.Format("Coefficient to consider the influence of the strength"));


            lst_input.Add(string.Format("Clear cover"));
            lst_input.Add(string.Format("Unit weight of dry concrete"));
            lst_input.Add(string.Format("Unit weight of wet concrete"));
            lst_input.Add(string.Format(" Unit Weight of wearing course"));
            lst_input.Add(string.Format("Weight of Crash Barrier"));
            lst_input.Add(string.Format("Weight of Railing"));
            lst_input.Add(string.Format("Intensity of Load for shuttering"));
            lst_input.Add(string.Format("Es"));

            #endregion



            List<string> lst_inp_vals = new List<string>();
            #region Value
            lst_inp_vals.Add(string.Format("19.58"));

            lst_inp_vals.Add(string.Format("0.500"));
            lst_inp_vals.Add(string.Format("0.759"));
            lst_inp_vals.Add(string.Format("40.0"));
            lst_inp_vals.Add(string.Format("12.0"));
            lst_inp_vals.Add(string.Format("26.00"));
            lst_inp_vals.Add(string.Format("11.1"));
            lst_inp_vals.Add(string.Format("0.30"));
            lst_inp_vals.Add(string.Format("1.50"));
            lst_inp_vals.Add(string.Format("0.45"));
            lst_inp_vals.Add(string.Format("3.00"));

            lst_inp_vals.Add(string.Format("0.21"));
            lst_inp_vals.Add(string.Format("0.40"));

            lst_inp_vals.Add(string.Format("0.21"));
            lst_inp_vals.Add(string.Format("0.21"));
            lst_inp_vals.Add(string.Format("0.065"));
            lst_inp_vals.Add(string.Format("4.00"));
            lst_inp_vals.Add(string.Format("0.00"));
            lst_inp_vals.Add(string.Format("0.00"));
            lst_inp_vals.Add(string.Format("0.800"));
            lst_inp_vals.Add(string.Format("1.00"));
            lst_inp_vals.Add(string.Format("M35"));
            lst_inp_vals.Add(string.Format("Fe500"));


            lst_inp_vals.Add(string.Format("1.50"));
            lst_inp_vals.Add(string.Format("1.20"));
            lst_inp_vals.Add(string.Format("0.67"));


            lst_inp_vals.Add(string.Format("40.0"));
            lst_inp_vals.Add(string.Format("2.50"));
            lst_inp_vals.Add(string.Format("2.60"));
            lst_inp_vals.Add(string.Format("2.200"));
            lst_inp_vals.Add(string.Format("1.00"));
            lst_inp_vals.Add(string.Format("0.50"));
            lst_inp_vals.Add(string.Format("0.50"));

            lst_inp_vals.Add(string.Format("200000.00"));
            #endregion

            #region Input Units
            List<string> lst_units = new List<string>();
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("deg."));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));

            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("Nos"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("m"));
            lst_units.Add(string.Format("nos"));
            lst_units.Add(string.Format("Mpa"));
            lst_units.Add(string.Format("Mpa"));


            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));


            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("t/m3"));
            lst_units.Add(string.Format("t/m3"));
            lst_units.Add(string.Format("t/m^3"));
            lst_units.Add(string.Format("t/m"));
            lst_units.Add(string.Format("t/m"));
            lst_units.Add(string.Format("t/m2"));
            lst_units.Add(string.Format("Mpa"));
            #endregion Input Units

            dgv_deck_user_input.Rows.Clear();
            for (int i = 0; i < lst_inp_vals.Count; i++)
            {
                dgv_deck_user_input.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);
            }

            #region Design Data
            lst_input.Clear();
            lst_input.Add(string.Format("Bar dia to be used"));
            lst_input.Add(string.Format("b"));
            lst_input.Add(string.Format("bottom bar dia"));
            lst_input.Add(string.Format("c/c distance"));
            lst_input.Add(string.Format("Co-efficient which takes account of the bond properties of the bonded reinforcement"));
            lst_input.Add(string.Format("Co-fficient which takes into account of the distribution of strain"));
            lst_input.Add(string.Format("Mean value of the tensile strength of the concrete "));

            lst_inp_vals.Clear();
            lst_inp_vals.Add(string.Format("16.0"));
            lst_inp_vals.Add(string.Format("1000"));
            lst_inp_vals.Add(string.Format("10"));
            lst_inp_vals.Add(string.Format("200"));
            lst_inp_vals.Add(string.Format("0.8"));
            lst_inp_vals.Add(string.Format("0.5"));
            lst_inp_vals.Add(string.Format("3.000"));


            lst_units.Clear();
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format("mm"));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format(""));
            lst_units.Add(string.Format("Mpa"));

            //MessageBox.Show((DateTime.Now - DateTime.UtcNow).ToString());
            dgv_deck_design_input.Rows.Clear();
            for (int i = 0; i < lst_inp_vals.Count; i++)
            {
                dgv_deck_design_input.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);
            }
            #endregion Design Data

            #region Live Load Data
            //lst_input.Clear();
            //lst_input.Add(string.Format("alpha (Constant)"));

            //lst_input.Add(string.Format("Distances  from start of deck"));

            //lst_input.Add(string.Format("Max. tyre pressure "));
            //lst_input.Add(string.Format("Distance between two loads across span "));
            //lst_input.Add(string.Format("Impact factor "));

            ////lst_input.Add(string.Format("1Lane Class - A"));
            //lst_input.Add(string.Format("1Lane  Load Type 1"));
            //lst_input.Add(string.Format("Contact width across span"));
            //lst_input.Add(string.Format("Contact width along span"));
            //lst_input.Add(string.Format("Distance between two vehicle c/c of  wheels (Traffic Direction)"));
            //lst_input.Add(string.Format("Distance between two Wheels C/C (Transverse direction)"));
            //lst_input.Add(string.Format("Minimum edge distance upto c/l of wheel"));

            ////lst_input.Add(string.Format("1Lane 40T Boggie (L-Type)"));
            //lst_input.Add(string.Format("1Lane  Load Type 4"));
            //lst_input.Add(string.Format("Contact width along span"));
            //lst_input.Add(string.Format("Distances between C/C of wheel Across the traffic"));

            ////lst_input.Add(string.Format("1Lane 40T Boggie (M-Type)"));
            //lst_input.Add(string.Format("1Lane  Load Type 5"));
            //lst_input.Add(string.Format("Contact width along span"));

            ////lst_input.Add(string.Format("1Lane 70R Track"));
            //lst_input.Add(string.Format("1Lane  Load Type 2"));
            //lst_input.Add(string.Format("Contact width along span"));
            //lst_input.Add(string.Format("Distance between two loads across span "));
            //lst_input.Add(string.Format("Contact width across span"));


            //lst_inp_vals.Clear();
            //lst_inp_vals.Add(string.Format("2.600"));
            //lst_inp_vals.Add(string.Format("0"));
            //lst_inp_vals.Add(string.Format("5.273"));
            //lst_inp_vals.Add(string.Format("1.22"));
            //lst_inp_vals.Add(string.Format("1.25"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("250"));
            //lst_inp_vals.Add(string.Format("500"));
            //lst_inp_vals.Add(string.Format("1.200"));
            //lst_inp_vals.Add(string.Format("1.800"));
            //lst_inp_vals.Add(string.Format("0.400"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("810"));
            //lst_inp_vals.Add(string.Format("1.93"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("360"));

            //lst_inp_vals.Add(string.Format(""));
            //lst_inp_vals.Add(string.Format("840"));
            //lst_inp_vals.Add(string.Format("30"));
            //lst_inp_vals.Add(string.Format("4570"));


            //lst_units.Clear();
            //lst_units.Add(string.Format("(Cont.)"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("kg/cm^2"));
            //lst_units.Add(string.Format("m"));

            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("m"));


            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("m"));


            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));


            //lst_units.Add(string.Format(""));
            //lst_units.Add(string.Format("mm"));
            //lst_units.Add(string.Format("m"));
            //lst_units.Add(string.Format("mm"));


            ////MessageBox.Show((DateTime.Now - DateTime.UtcNow).ToString());

            ////dgv_deck_user_live_loads.Rows.Clear();
            ////for (int i = 0; i < lst_inp_vals.Count; i++)
            ////{
            ////    dgv_deck_user_live_loads.Rows.Add(lst_input[i], lst_inp_vals[i], lst_units[i]);

            ////    if (lst_inp_vals[i] == "")
            ////    {
            ////        dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
            ////        //dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 9.0, FontStyle.Bold, GraphicsUnit.Pixel);
            ////        dgv_deck_user_live_loads.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);
            ////    }

            ////}
            #endregion Design Data
        }

        private void Cable_Input_Data()
        {


            #region list_name
            List<string> list_name = new List<string>();
            list_name.Add(string.Format("Projected Length"));
            list_name.Add(string.Format("Wobble coeff.(k)"));
            list_name.Add(string.Format("Cable Type"));
            //list_name.Add(string.Format("Es"));
            list_name.Add(string.Format("Modulus of Elasticity (Es)"));
            list_name.Add(string.Format("Coeff. of Friction, (m)"));
            list_name.Add(string.Format("Slip at Live Anchorage"));
            //list_name.Add(string.Format("a"));
            list_name.Add(string.Format("St. length of cable at left end in elevation"));
            //list_name.Add(string.Format("b"));
            list_name.Add(string.Format("Length of curved profile in elevation"));
            //list_name.Add(string.Format("d"));
            list_name.Add(string.Format("Length of curved prfile in elevation"));
            //list_name.Add(string.Format("e"));
            list_name.Add(string.Format("St. length of cable at left end in elevation"));
            //list_name.Add(string.Format("Y1"));
            list_name.Add(string.Format("Coordinate of cable at jacking end"));
            //list_name.Add(string.Format("Y6"));
            list_name.Add(string.Format("Coordinate of cable at jacking end"));
            //list_name.Add(string.Format("Y3"));
            list_name.Add(string.Format("Coordinate of cable at mid span"));
            //list_name.Add(string.Format("p"));
            list_name.Add(string.Format("St. length of cable at left end in plan"));
            //list_name.Add(string.Format("Z1"));
            list_name.Add(string.Format("Coordinate of cable at jacking end in plan"));
            //list_name.Add(string.Format("Z4"));
            list_name.Add(string.Format("Coordinate of cable at mid span in plan"));
            list_name.Add(string.Format("Assumingslip travels from Anchorage"));

            #endregion

            #region list_unit
            List<string> list_unit = new List<string>();
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format(""));
            list_unit.Add(string.Format(""));
            list_unit.Add(string.Format("t/m^2"));
            list_unit.Add(string.Format(""));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("mm"));
            list_unit.Add(string.Format("m"));
            #endregion



            #region list_cable_1
            List<string> list_cable_1 = new List<string>();

            list_cable_1.Add(string.Format("30700"));
            list_cable_1.Add(string.Format("0.0020"));
            list_cable_1.Add(string.Format("17K"));
            list_cable_1.Add(string.Format("19500000"));
            list_cable_1.Add(string.Format("0.170"));
            list_cable_1.Add(string.Format("6"));
            list_cable_1.Add(string.Format("1000"));
            list_cable_1.Add(string.Format("13000"));
            list_cable_1.Add(string.Format("13000"));
            list_cable_1.Add(string.Format("1000"));
            list_cable_1.Add(string.Format("1100"));
            list_cable_1.Add(string.Format("1100"));
            list_cable_1.Add(string.Format("120"));
            list_cable_1.Add(string.Format("1000"));
            list_cable_1.Add(string.Format("0"));
            list_cable_1.Add(string.Format("0.001"));
            list_cable_1.Add(string.Format("15.35"));
            #endregion


            #region list_cable_2
            List<string> list_cable_2 = new List<string>();
            list_cable_2.Add(string.Format("30700"));
            list_cable_2.Add(string.Format("0.0020"));
            list_cable_2.Add(string.Format("18K"));
            list_cable_2.Add(string.Format("1.950E+07"));
            list_cable_2.Add(string.Format("0.170 "));
            list_cable_2.Add(string.Format("6"));
            list_cable_2.Add(string.Format("1000"));
            list_cable_2.Add(string.Format("7015"));
            list_cable_2.Add(string.Format("7015"));
            list_cable_2.Add(string.Format("1000"));
            list_cable_2.Add(string.Format("750"));
            list_cable_2.Add(string.Format("750"));
            list_cable_2.Add(string.Format("120"));
            list_cable_2.Add(string.Format("1000"));
            list_cable_2.Add(string.Format("0"));
            list_cable_2.Add(string.Format("180"));
            list_cable_2.Add(string.Format("15.35"));
            #endregion

            #region list_cable_3
            List<string> list_cable_3 = new List<string>();
            list_cable_3.Add(string.Format("30700"));
            list_cable_3.Add(string.Format("0.0020"));
            list_cable_3.Add(string.Format("18K"));
            list_cable_3.Add(string.Format("1.950E+07"));
            list_cable_3.Add(string.Format("0.170 "));
            list_cable_3.Add(string.Format("6"));
            list_cable_3.Add(string.Format("1000"));
            list_cable_3.Add(string.Format("2764"));
            list_cable_3.Add(string.Format("2764"));
            list_cable_3.Add(string.Format("1000"));
            list_cable_3.Add(string.Format("400"));
            list_cable_3.Add(string.Format("400"));
            list_cable_3.Add(string.Format("120"));
            list_cable_3.Add(string.Format("1000"));
            list_cable_3.Add(string.Format("0"));
            list_cable_3.Add(string.Format("-180"));
            list_cable_3.Add(string.Format("15.35"));
            #endregion


            #region list_cable_4
            List<string> list_cable_4 = new List<string>();
            list_cable_4.Add(string.Format("30700"));
            list_cable_4.Add(string.Format("0.0020"));
            list_cable_4.Add(string.Format("17K"));
            list_cable_4.Add(string.Format("1.950E+07"));
            list_cable_4.Add(string.Format("0.170 "));
            list_cable_4.Add(string.Format("6"));
            list_cable_4.Add(string.Format("1000"));
            list_cable_4.Add(string.Format("13000"));
            list_cable_4.Add(string.Format("13000"));
            list_cable_4.Add(string.Format("1000"));
            list_cable_4.Add(string.Format("1450"));
            list_cable_4.Add(string.Format("1450"));
            list_cable_4.Add(string.Format("500"));
            list_cable_4.Add(string.Format("1000"));
            list_cable_4.Add(string.Format("0"));
            list_cable_4.Add(string.Format("0.001"));
            list_cable_4.Add(string.Format("16.448"));
            #endregion



            dgv_cable_data.Rows.Clear();
            for (int i = 0; i < list_name.Count; i++)
            {
                dgv_cable_data.Rows.Add(list_name[i], list_unit[i], list_cable_1[i], list_cable_2[i], list_cable_3[i], list_cable_4[i]);
            }
        }

        #endregion Bridge Deck Analysis Methods

        #region Long Girder Form Events

        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_abut") ||
                ctrl.Name.ToLower().StartsWith("txt_abut"))
            {
                astg = new ASTRAGrade(cmb_abut_fck.Text, cmb_abut_fy.Text);
                txt_abut_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_abut_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }

            else if (ctrl.Name.ToLower().StartsWith("cmb_rcc_pier") ||
                ctrl.Name.ToLower().StartsWith("txt_rcc_pier"))
            {
                astg = new ASTRAGrade(cmb_rcc_pier_fck.Text, cmb_rcc_pier_fy.Text);
                txt_rcc_pier_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_rcc_pier_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }
        }


        #endregion Long Girder

        #region Design of RCC Pier

        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(rcc_pier.rep_file_name);
        }

        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
        }
        private void btn_dwg_rcc_deck_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Name == btn_dwg_rcc_abut.Name)
            {
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
            }
            else if (b.Name == btn_dwg_pier.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
            }
        }

        #endregion Design of RCC Pier

        #region Chiranjit [2012 06 20]

        void Text_Changed_T_Girder()
        {

            double SMG = (B - CL - CR) / (NMG - 1);
            double SCG = L / (NCG - 1);

            //double Bb = MyList.StringToDouble(txt_Ana_Bb.Text, 0.65);
            //double Db = MyList.StringToDouble(txt_Ana_Db.Text, 0.65);

            //Chiranjit [2012 12 26]
            //DMG = L / 10.0;
            //DCG = DMG - 0.4;

            //txt_LL_load_gen.Text = ((L + Math.Abs(MyList.StringToDouble(txt_Ana_X.Text, 0.0))) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");
            txt_LL_load_gen.Text = ((L) / (MyList.StringToDouble(txt_XINCR.Text, 0.0))).ToString("f0");

            Ana_Initialize_Analysis_InputData();
            Calculate_Load_Computation();
        }

        //Chiranjit [2012 06 20]



        #endregion Chiranjit [2012 06 10]

        #region Chiranjit [2012 07 06]


        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
        //IApplication iApp = null;
        //double B = 0.0;
        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            //iApp = app;
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
            //B = abut_width;
        }
        public string Total_DeadLoad_Reaction
        {
            get
            {
                return txt_dead_kN_m.Text;
            }
            set
            {
                txt_dead_kN_m.Text = value;
            }
        }
        public string Total_LiveLoad_Reaction
        {
            get
            {
                return txt_live_kN_m.Text;
            }
            set
            {
                txt_live_kN_m.Text = value;
            }
        }
        void frm_ViewForces_Load()
        {
            try
            {
                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                LL_support_reactions = new SupportReactionTable(iApp, LL_Analysis_Rep);
                Show_and_Save_Data_DeadLoad();
            }
            catch (Exception ex) { }
        }
        void Show_and_Save_Data_DeadLoad()
        {

            dgv_left_end_design_forces.Rows.Clear();
            dgv_right_end_design_forces.Rows.Clear();

            SupportReaction sr = null;
            MyList mlist = new MyList(MyList.RemoveAllSpaces(Supports), ' ');

            double tot_dead_vert_reac = 0.0;
            double tot_live_vert_reac = 0.0;

            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    sr = DL_support_reactions.Get_Data(mlist.GetInt(i));
                    dgv_left_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));

                    tot_dead_vert_reac += Math.Abs(sr.Max_Reaction); ;
                }
                catch (Exception ex)
                {
                }

            }

            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    sr = LL_support_reactions.Get_Data(mlist.GetInt(i));
                    dgv_right_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction).ToString("f3"));
                    tot_live_vert_reac += Math.Abs(sr.Max_Reaction);
                }
                catch (Exception ex)
                {
                }

            }

            txt_dead_vert_reac_ton.Text = (tot_dead_vert_reac).ToString("f3");
            txt_live_vert_rec_Ton.Text = (tot_live_vert_reac).ToString("f3");
        }


        private void txt_dead_vert_reac_ton_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            //if (txt.Name == txt_dead_vert_reac_ton.Name)
            //{
            Text_Changed_Forces();
            //}

        }

        private void Text_Changed_Forces()
        {
            if (B != 0)
            {
                txt_dead_vert_reac_kN.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10)).ToString("f3");
                txt_dead_kN_m.Text = ((MyList.StringToDouble(txt_dead_vert_reac_ton.Text, 0.0) * 10) / B).ToString("f3");
                //}
                //else if (txt.Name == txt_live_vert_rec_Ton.Name)
                //{
                txt_live_vert_rec_kN.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10)).ToString("f3");
                txt_live_kN_m.Text = ((MyList.StringToDouble(txt_live_vert_rec_Ton.Text, 0.0) * 10) / B).ToString("f3");
                //}
            }
            //else if (txt.Name == txt_dead_kN_m.Name)
            //{
            txt_abut_w5.Text = txt_dead_kN_m.Text;
            txt_pier_2_P2.Text = txt_dead_kN_m.Text;
            //}
            //else if (txt.Name == txt_live_kN_m.Name)
            //{
            txt_abut_w6.Text = txt_live_kN_m.Text;
            txt_pier_2_P3.Text = txt_live_kN_m.Text;
            //}
            //else if (txt.Name == txt_final_vert_rec_kN.Name)
            //{
            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mx_kN.Name)
            //{
            txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
            //}
            //else if (txt.Name == txt_max_Mz_kN.Name)
            //{
            txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;


            txt_abut_B.Text = txt_RCC_Pier__B.Text = txt_RCC_Pier___B.Text = txt_Ana_B.Text;

            txt_RCC_Pier_L.Text = txt_abut_L.Text = txt_Ana_L.Text;



        }
        #endregion View Force

        #region frm_Pier_ViewDesign_Forces
        string analysis_rep = "";
        SupportReactionTable support_reactions = null;
        string Left_support = "";
        string Right_support = "";
        public void frm_Pier_ViewDesign_Forces(string Analysis_Report_file, string left_support, string right_support)
        {

            analysis_rep = Analysis_Report_file;

            Left_support = left_support.Replace(",", " ");
            Right_support = right_support.Replace(",", " ");
        }

        private void frm_ViewDesign_Forces_Load()
        {
            support_reactions = new SupportReactionTable(iApp, analysis_rep);
            try
            {
                Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }

        void Show_and_Save_Data()
        {
            if (!File.Exists(analysis_rep)) return;
            string format = "{0,27} {1,10:f3} {2,10:f3} {3,10:f3}";
            List<string> list_arr = new List<string>(File.ReadAllLines(analysis_rep));
            list_arr.Add("");
            list_arr.Add("                   =====================================");
            list_arr.Add("                     DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                   =====================================");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add(string.Format(""));
            list_arr.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list_arr.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list_arr.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list_arr.Add("");
            SupportReaction sr = null;

            MyList mlist = new MyList(MyList.RemoveAllSpaces(Left_support), ' ');

            double tot_left_vert_reac = 0.0;
            double tot_right_vert_reac = 0.0;

            double tot_left_Mx = 0.0;
            double tot_left_Mz = 0.0;

            double tot_right_Mx = 0.0;
            double tot_right_Mz = 0.0;


            dgv_left_des_frc.Rows.Clear();
            dgv_right_des_frc.Rows.Clear();
            list_arr.Add("LEFT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_left_des_frc.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);

                tot_left_vert_reac += Math.Abs(sr.Max_Reaction); ;
                tot_left_Mx += sr.Max_Mx;
                tot_left_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }

            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Ton
            //tot_left_vert_reac /= 10.0;
            //tot_left_Mx /= 10.0;
            //tot_left_Mz /= 10.0;

            txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");
            list_arr.Add(string.Format(format, "TOTAL", tot_left_vert_reac, tot_left_Mx, tot_left_Mz));
            list_arr.Add("");

            mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_des_frc.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz);

                tot_right_vert_reac += Math.Abs(sr.Max_Reaction);
                tot_right_Mx += sr.Max_Mx;
                tot_right_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }
            list_arr.Add("");

            //Chiranjit [2012 07 06]
            //Change unit kN to Ton
            //tot_right_vert_reac /= 10.0;
            //tot_right_Mx /= 10.0;
            //tot_right_Mz /= 10.0;
            txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");


            //txt_both_ends_total.Text = (tot_left_vert_reac + tot_right_vert_reac).ToString("0.000");
            list_arr.Add("");
            //list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Ton");

            txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            txt_final_Mx.Text = ((Math.Abs(tot_left_Mx) > Math.Abs(tot_right_Mx)) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = (MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0).ToString("f3");


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_max_Mx_kN.Text + " kN-m");
            txt_final_Mz.Text = ((Math.Abs(tot_left_Mz) > Math.Abs(tot_right_Mz))  ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = (MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0).ToString("f3");

            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_max_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");





            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_max_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_max_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }
        #endregion frm_Pier_ViewDesign_Forces
        #endregion

        #region Chiranjit [2012 07 10]
        //Write All Data in a File
        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        public void Write_All_Data(bool showMessage)
        {
            Text_Changed();

            if (showMessage) DemoCheck();

            iApp.Save_Form_Record(this, user_path);
        }
        public void Read_All_Data()
        {
            if (iApp.IsDemo) return;

            string data_file = Bridge_Analysis.Input_File;

            if (!File.Exists(data_file)) return;
            try
            {
                Abut.FilePath = user_path;
                rcc_pier.FilePath = user_path;
            }
            catch (Exception ex) { }
            Button_Enable_Disable();
        }
        #endregion Chiranjit [2012 07 10]

        #region Chiranjit [2012 07 20]

        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                txt_Ana_L.Text = "0.0";
                txt_Ana_L.Text = "26.52";
                txt_Ana_B.Text = "12.0";
                txt_Ana_CW.Text = "11.0";
                txt_Ana_ang.Text = "45";

                Deckslab_User_Input();
                PSC_Girder_User_Input();
            }
        }
        #endregion Chiranjit [2012 07 20]

        #region Excel Files

        public string Excel_PSC_Girder
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder BS.xlsx");


                return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder IS.xlsx");
            }
        }
        public string Excel_Deckslab
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder_Deck Slab BS.xlsx");
                return Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder_Deck Slab IS.xlsx");
            }
        }

        #endregion Excel Files

        private void btn_LS_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string excel_file_name = "";
            string copy_path = "";
            //if (btn.Name == btn_LS_long_ws.Name)
            if (btn.Name == btn_design_process.Name)
            {
                //excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\RCC T Girder LS\RCC Precast Long Girder.xlsm");


                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder BS.xlsx");
                //else
                //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder IS.xlsx");
                Write_All_Data(true);

                excel_file_name = Excel_PSC_Girder;
                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));
                File.Copy(excel_file_name, copy_path, true);
                PSC_I_Girder_Excel_Update rcc_excel = new PSC_I_Girder_Excel_Update();
                rcc_excel.Excel_File_Name = copy_path;
                rcc_excel.PSC_Girder_User_Inputs.Type_of_Cable = txt_cab_no.Text;
                rcc_excel.PSC_Girder_User_Inputs.Strand_Area = txt_cab_area.Text;
                rcc_excel.PSC_Girder_User_Inputs.UTS = txt_cab_UTS.Text;
                rcc_excel.PSC_Girder_User_Inputs.Es = txt_cab_Es.Text;
                rcc_excel.PSC_Girder_User_Inputs.Permissible_Slip = txt_cab_slip.Text;
                rcc_excel.PSC_Girder_User_Inputs.Jacking_Distance = txt_cab_jack_dist.Text;

                rcc_excel.DGV_Cable = dgv_cable_data;
                rcc_excel.cab1_ref = cab1;
                rcc_excel.cab2_ref = cab2;
                rcc_excel.cab3_ref = cab3;
                rcc_excel.cab4_ref = cab4;




                rcc_excel.PSC_Girder_User_Inputs.Read_From_Grid(dgv_long_user_input);
                rcc_excel.Report_File_Name = File_Long_Girder_Results;
                rcc_excel.Update_Data();

                iApp.Excel_Open_Message();
                Button_Enable_Disable();
                return;

            }
            else if (btn.Name == btn_LS_psc_rep_open.Name)
            {
                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_PSC_Girder));
                if (File.Exists(copy_path))
                    iApp.OpenExcelFile(copy_path, "2011ap");
            }
            else if (btn.Name == btn_LS_deck_rep_open.Name)
            {
                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Deckslab));
                if (File.Exists(copy_path))
                    iApp.OpenExcelFile(copy_path, "2011ap");
            }
            else if (btn.Name == btn_LS_deck_ws.Name)
            {

                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder BS\Design of PSC I Girder_Deck Slab BS.xlsx");
                //else
                //    excel_file_name = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\PSC I Girder IS\Design of PSC I Girder_Deck Slab IS.xlsx");

                Deckslab_Moving_Loads();

                Write_All_Data(true);

                excel_file_name = Excel_Deckslab;

                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                try
                {
                    copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));
                    File.Copy(excel_file_name, copy_path, true);
                    RCC_Deckslab_Excel_Update_Extend rcc_excel = new RCC_Deckslab_Excel_Update_Extend();
                    rcc_excel.Excel_File_Name = copy_path;
                    rcc_excel.Report_File_Name = File_DeckSlab_Results;

                    Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Deck_Analysis.LiveLoad_File);
                    rcc_excel.llc = Deck_Analysis.Live_Load_List;
                    rcc_excel.Deckslab_User_Inputs.Read_From_Grid(dgv_deck_user_input);
                    rcc_excel.Deckslab_Design_Inputs.Read_From_Grid(dgv_deck_design_input);
                    //rcc_excel.Deckslab_User_Live_loads.Read_From_Grid(dgv_deck_user_live_loads);

                    rcc_excel.Deck_Loads = Deck_LL_Loads;

                    rcc_excel.Read_Update_Data();
                    iApp.Excel_Open_Message();
                }
                catch (Exception ex) { }
                Button_Enable_Disable();
                return;
            }
            else
            {
                iApp.Open_WorkSheet_Design();
                return;
            }

            copy_path = Path.Combine(user_path, Path.GetFileName(excel_file_name));

            if (File.Exists(excel_file_name))
            {
                iApp.OpenExcelFile(Worksheet_Folder, excel_file_name, "2011ap");
            }
            Button_Enable_Disable();
        }

        private void btn_Deck_Analysis_Click(object sender, EventArgs e)
        {
            //Write_All_Data(true);
            user_path = IsCreateData ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                DECKSLAB_LL_TXT();
                #region Process
                int i = 1;
                Write_All_Data(true);

                string flPath = Deck_Analysis.Input_File;

                ProcessCollection pcol = new ProcessCollection();

                ProcessData pd = new ProcessData();

                do
                {
                    if (i == 1)
                    {
                        flPath = Deck_Analysis.LL_Analysis_1_Input_File;
                    }
                    else if (i == 2)
                    {
                        flPath = Deck_Analysis.LL_Analysis_2_Input_File;
                    }
                    else if (i == 3)
                    {
                        flPath = Deck_Analysis.LL_Analysis_3_Input_File;
                    }
                    else if (i == 4)
                    {

                        flPath = Deck_Analysis.LL_Analysis_4_Input_File;
                    }
                    else if (i == 5)
                    {

                        flPath = Deck_Analysis.LL_Analysis_5_Input_File;
                    }
                    else if (i == 6)
                    {
                        flPath = Deck_Analysis.LL_Analysis_6_Input_File;
                    }
                    else if (i == 7)
                    {
                        flPath = Deck_Analysis.DeadLoadAnalysis_Input_File;
                    }


                    //MessageBox.Show(this, "PROCESS ANALYSIS FOR "
                    // + Path.GetFileNameWithoutExtension(flPath).ToUpper(), "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    //File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    //File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
                    ////System.Environment.SetEnvironmentVariable("SURVEY", flPath);

                    //System.Diagnostics.Process prs = new System.Diagnostics.Process();

                    //System.Environment.SetEnvironmentVariable("SURVEY", flPath);
                    //System.Environment.SetEnvironmentVariable("ASTRA", flPath);

                    //prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
                    //if (prs.Start())
                    //    prs.WaitForExit();


                    pd = new ProcessData();
                    pd.Process_File_Name = flPath;
                    pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                    pcol.Add(pd);


                    i++;
                }
                while (i <= 7);

                //frm_LS_Process ff = new frm_LS_Process(pcol);
                //ff.Owner = this;
                ////ff.ShowDialog();
                //if (ff.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                //if (!iApp.Show_and_Run_Process_List(pcol)) return;


                //while (i < 3) ;

                //string ana_rep_file = Bridge_Analysis.Analysis_Report;
                //string ana_rep_file = Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File);

                if (iApp.Show_and_Run_Process_List(pcol))
                {
                    iApp.Progress_Works.Clear();
                    //iApp.Progress_Works.Add("Reading Analysis Data from Total Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Total Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");


                    //iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 1 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 2 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 3 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 4 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 5 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Live Load Analysis 6 Report File (ANALYSIS_REP.TXT)");
                    //iApp.Progress_Works.Add("Set Structure Geometry for Live Load Analysis");
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis Result");



                    iApp.Progress_Works.Add("Reading Analysis Data from Dead Load Analysis Report File (ANALYSIS_REP.TXT)");

                    //iApp.Progress_Works.Add("Reading support reaction forces from Total Load Analysis Report");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Live Load Analysis Report");
                    //iApp.Progress_Works.Add("Reading support reaction forces from Dead Load Analysis Report");


                    //Deck_Analysis.TotalLoad_Analysis = null;
                    //Long_Girder_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, Long_Girder_Analysis.Total_Analysis_Report);
                    //Long_Girder_Analysis.LiveLoad_Analysis = new BridgeMemberAnalysis(iApp, Long_Girder_Analysis.LiveLoad_Analysis_Report);

                    Deck_Analysis.LiveLoad_1_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_1_Input_File));

                    Deck_Analysis.LiveLoad_2_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_2_Input_File));

                    Deck_Analysis.LiveLoad_3_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_3_Input_File));

                    Deck_Analysis.LiveLoad_4_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_4_Input_File));

                    Deck_Analysis.LiveLoad_5_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_5_Input_File));

                    Deck_Analysis.LiveLoad_6_Analysis = new BridgeMemberAnalysis(iApp,
                        Deck_Analysis.Get_Analysis_Report_File(Deck_Analysis.LL_Analysis_6_Input_File));

                    Deck_Analysis.DeadLoad_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis.DeadLoad_Analysis_Report);

                    if (!iApp.Is_Progress_Cancel)
                        Show_Deckslab_Moment_Shear();
                    else
                    {
                        iApp.Progress_Works.Clear();
                        iApp.Progress_OFF();
                        return;

                    }

                }

                //grb_create_input_data.Enabled = rbtn_ana_create_analysis_file.Checked;
                grb_select_analysis.Enabled = !rbtn_ana_create_analysis_file.Checked;

                //grb_create_input_data.Enabled = !rbtn_ana_select_analysis_file.Checked;
                grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;

                Button_Enable_Disable();
                Write_All_Data(false);
                iApp.Progress_Works.Clear();

                #endregion Process
                Write_All_Data(false);
            }
            catch (Exception ex) { }
        }

        private void btn_Deck_Create_Analysis_Data_Click(object sender, EventArgs e)
        {

            Write_All_Data(true);
            user_path = IsCreateData ? Path.Combine(iApp.LastDesignWorkingFolder, Title) : user_path;
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            try
            {
                //DECKSLAB_LL_TXT();
                #region Create Data

                Ana_Initialize_Analysis_InputData();

                string usp = Path.Combine(user_path, "Deck Slab Analysis");
                if (!Directory.Exists(usp))
                {
                    Directory.CreateDirectory(usp);
                }
                Deck_Analysis.Input_File = Path.Combine(usp, "INPUT_DATA.TXT");
                Deck_Analysis.CreateData();

                Deckslab_Moving_Loads();

                Deck_Analysis.WriteData_DeadLoad_Analysis(Deck_Analysis.Input_File);

                Calculate_Load_Computation(Deck_Analysis.Outer_Girders_as_String,
                    Deck_Analysis.Outer_Girders_as_String,
                     Deck_Analysis.joints_list_for_load);

                //Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LiveLoadAnalysis_Input_File);

                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_1_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_2_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_3_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_4_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_5_Input_File, deck_ll);
                Deck_Analysis.WriteData_LiveLoad_Analysis(Deck_Analysis.LL_Analysis_6_Input_File, deck_ll);

                Deck_Analysis.WriteData_DeadLoad_Analysis(Deck_Analysis.DeadLoadAnalysis_Input_File);

                //Ana_Write_DeckSlab_Load_Data(Deck_Analysis.Input_File, true, false);
                //Ana_Write_DeckSlab_Load_Data(Deck_Analysis.TotalAnalysis_Input_File, true, false);
                //Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LiveLoadAnalysis_Input_File, true, false);

                //Chiranjit [2013 09 24] Without Dead Load
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_1_Input_File, true, false, 1);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_2_Input_File, true, false, 2);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_3_Input_File, true, false, 3);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_4_Input_File, true, false, 4);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_5_Input_File, true, false, 5);
                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.LL_Analysis_6_Input_File, true, false, 6);

                Ana_Write_DeckSlab_Load_Data(Deck_Analysis.DeadLoadAnalysis_Input_File, false, true, 1);

                //Deck_Analysis.TotalLoad_Analysis = new BridgeMemberAnalysis(iApp, Deck_Analysis.TotalAnalysis_Input_File);

                string ll_txt = Deck_Analysis.LiveLoad_File;

                Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                //if (Deck_Analysis.Live_Load_List == null) return;

                Button_Enable_Disable();

               MessageBox.Show(this, "Analysis Input data is created as INPUT_DATA.TXT.",
                  "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #endregion Create Data

                Write_All_Data(false);


            }
            catch (Exception ex) { }
        }


        List<string> deck_ll = new List<string>();
        List<string> deck_ll_types = new List<string>();


        List<string> deck_ll_1 = new List<string>();
        List<string> deck_ll_2 = new List<string>();
        List<string> deck_ll_3 = new List<string>();
        List<string> deck_ll_4 = new List<string>();
        List<string> deck_ll_5 = new List<string>();
        List<string> deck_ll_6 = new List<string>();

        public void DECKSLAB_LL_TXT_1()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            deck_ll.Clear();
            deck_ll_types.Clear();

            bool flag = false;
            for (i = 0; i < dgv_deck_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_deck_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_deck_liveloads[c, i].Value.ToString();

                    if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }
                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    deck_ll_types.Add(txt);
                }
                deck_ll.Add(txt);
            }
            deck_ll.Add(string.Format(""));
            deck_ll.Add(string.Format("TYPE 6 IRC40RWHEEL"));
            deck_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            deck_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            deck_ll.Add(string.Format("2.740"));
            i = 0;

            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            deck_ll_1 = new List<string>();
            deck_ll_2 = new List<string>();
            deck_ll_3 = new List<string>();
            deck_ll_4 = new List<string>();
            deck_ll_5 = new List<string>();
            deck_ll_6 = new List<string>();

            int fl = 0;
            double xinc = MyList.StringToDouble(txt_deck_xincr.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_deck_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_deck_loads[0, i].Value.ToString().ToUpper();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < deck_ll_types.Count; f++)
                        {
                            if (deck_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", deck_ll_types[f], imp_fact);
                                break;
                            }
                        }
                        list.Add(txt);
                    }
                    list.Add(string.Format("LOAD GENERATION {0:f0}", (1 + (L / xinc))));
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0.0 {2:f3} XINC {3:f3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    if (count == 1)
                    {
                        deck_ll_1.Clear();
                        deck_ll_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        deck_ll_2.Clear();
                        deck_ll_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        deck_ll_3.Clear();
                        deck_ll_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        deck_ll_4.Clear();
                        deck_ll_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        deck_ll_5.Clear();
                        deck_ll_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {
                        deck_ll_6.Clear();
                        deck_ll_6.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_deck_loads.ColumnCount; c++)
                {
                    kStr = dgv_deck_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

        }
        public void DECKSLAB_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            deck_ll.Clear();
            deck_ll_types.Clear();
            List<string> long_ll_impact = new List<string>();

            bool flag = false;
            for (i = 0; i < dgv_deck_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_deck_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_deck_liveloads[c, i].Value.ToString();

                    if (flag)
                    {
                        long_ll_impact.Add(kStr);
                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }
                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    deck_ll_types.Add(txt);
                }
                deck_ll.Add(txt);
            }
            deck_ll.Add(string.Format(""));
            deck_ll.Add(string.Format("TYPE 6 IRC40RWHEEL"));
            deck_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            deck_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            deck_ll.Add(string.Format("2.740"));
            i = 0;

            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            deck_ll_1 = new List<string>();
            deck_ll_2 = new List<string>();
            deck_ll_3 = new List<string>();
            deck_ll_4 = new List<string>();
            deck_ll_5 = new List<string>();
            deck_ll_6 = new List<string>();

            int fl = 0;
            double xinc = MyList.StringToDouble(txt_deck_xincr.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_deck_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_deck_loads[0, i].Value.ToString().ToUpper();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < deck_ll_types.Count; f++)
                        {
                            if (deck_ll_types[f].StartsWith(def_load[j]))
                            {
                                //txt = string.Format("{0} {1:f3}", deck_ll_types[f], imp_fact);
                                txt = string.Format("{0} {1:f3}", deck_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        list.Add(txt);
                    }
                    list.Add(string.Format("LOAD GENERATION {0:f0}", (1 + (L / xinc))));
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0.0 {2:f3} XINC {3:f3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    if (count == 1)
                    {
                        deck_ll_1.Clear();
                        deck_ll_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        deck_ll_2.Clear();
                        deck_ll_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        deck_ll_3.Clear();
                        deck_ll_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        deck_ll_4.Clear();
                        deck_ll_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        deck_ll_5.Clear();
                        deck_ll_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {
                        deck_ll_6.Clear();
                        deck_ll_6.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_deck_loads.ColumnCount; c++)
                {
                    kStr = dgv_deck_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

        }

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();

        public void LONG_GIRDER_LL_TXT()
        {

            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();


            bool flag = false;
            for (i = 0; i < dgv_long_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_long_liveloads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        long_ll_impact.Add(kStr);
                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            long_ll.Add(string.Format(""));
            long_ll.Add(string.Format("TYPE 6 40RWHEEL"));
            long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            load_list_1 = new List<string>();
            load_list_2 = new List<string>();
            load_list_3 = new List<string>();
            load_list_4 = new List<string>();
            load_list_5 = new List<string>();
            load_list_6 = new List<string>();
            load_total_7 = new List<string>();



            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_long_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_long_loads[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                    if (count == 1)
                    {
                        load_list_1.Clear();
                        load_list_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        load_list_2.Clear();
                        load_list_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        load_list_3.Clear();
                        load_list_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        load_list_4.Clear();
                        load_list_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        load_list_5.Clear();
                        load_list_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {

                        load_list_6.Clear();
                        load_list_6.AddRange(list.ToArray());

                    }
                    else if (count == 7)
                    {
                        load_total_7.Clear();
                        load_total_7.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_long_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

            //Long_Girder_Analysis.LoadList_1 = 
        }

        private void btn_restore_ll_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (MessageBox.Show("All values will be changed to original default values, want to change ?",
                "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (btn.Name == btn_deck_restore_ll.Name)
                    Default_Moving_LoadData(dgv_deck_liveloads);
                else if (btn.Name == btn_long_restore_ll.Name)
                    Default_Moving_LoadData(dgv_long_liveloads);
            }
        }
        private void btn_Ana_view_data_Click(object sender, EventArgs e)
        {
            string file_name = "";
            string ll_txt = "";

            Button btn = sender as Button;

            #region Set File Name
            if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
            {
                file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);
            }
            else
            {
                file_name = File_Long_Girder_Results;
            }
            #endregion Set File Name

            ll_txt = MyList.Get_LL_TXT_File(file_name);
            if (btn.Name == btn_view_data.Name)
            {
                iApp.View_Input_File(file_name);
                //if (File.Exists(ll_txt))
                //    iApp.RunExe(ll_txt);
                //if (File.Exists(file_name))
                //    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                if (File.Exists(file_name))
                    iApp.OpenWork(file_name, false);
            }
            else if (btn.Name == btn_view_report.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.RunExe(file_name);
            }
            else if (btn.Name == btn_View_Moving_Load.Name)
            {
                file_name = MyList.Get_Analysis_Report_File(file_name);
                if (File.Exists(file_name))
                    iApp.OpenWork(file_name, true);
            }
        }

        private void btn_deck_open_input_file_Click(object sender, EventArgs e)
        {
            ComboBox cmb = cmb_deck_input_files;

            Button btn = sender as Button;

            string filename = "";

            if (cmb.SelectedIndex == 0) filename = Deck_Analysis.DeadLoadAnalysis_Input_File;
            else if (cmb.SelectedIndex == 1) filename = Deck_Analysis.LL_Analysis_1_Input_File;
            else if (cmb.SelectedIndex == 2) filename = Deck_Analysis.LL_Analysis_2_Input_File;
            else if (cmb.SelectedIndex == 3) filename = Deck_Analysis.LL_Analysis_3_Input_File;
            else if (cmb.SelectedIndex == 4) filename = Deck_Analysis.LL_Analysis_4_Input_File;
            else if (cmb.SelectedIndex == 5) filename = Deck_Analysis.LL_Analysis_5_Input_File;
            else if (cmb.SelectedIndex == 6) filename = Deck_Analysis.LL_Analysis_6_Input_File;
            else if (cmb.SelectedIndex == 7) filename = File_DeckSlab_Results;
            if (!File.Exists(filename)) return;
            if (btn.Name == btn_deck_view_data.Name)
            {
                iApp.View_Input_File(filename);
                //string ll = Path.Combine(Path.GetDirectoryName(filename), "LL.TXT");
                //if (File.Exists(ll))
                //    iApp.RunExe(ll);
            }
            else if (btn.Name == btn_deck_view_report.Name)
                iApp.RunExe(MyList.Get_Analysis_Report_File(filename));
            else if (btn.Name == btn_deck_view_struc.Name)
                iApp.OpenWork(filename, false);
            else if (btn.Name == btn_deck_view_moving.Name)
                iApp.OpenWork(filename, true);

        }

        #region Design of RCC Pier

        private void cmb_pier_2_k_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_pier_2_k.SelectedIndex)
            {
                case 0: txt_pier_2_k.Text = "1.50"; break;
                case 1: txt_pier_2_k.Text = "0.66"; break;
                case 2: txt_pier_2_k.Text = "0.50"; break;
                case 3: txt_pier_2_k.Text = ""; txt_pier_2_k.Focus(); break;
            }
        }
        private void btn_RccPier_Process_Click(object sender, EventArgs e)
        {

        }

        private void btn_RccPier_Drawing_Click1(object sender, EventArgs e)
        {
            //iapp.SetDrawingFile(user_input_file, "PIER");

            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
            //iapp.RunViewer(drwg_path);
        }
        public void RCC_Pier_Initialize_InputData()
        {
            rcc_pier.L1 = 0.0d;
            rcc_pier.W1 = 0.0d;
            rcc_pier.W2 = 0.0d;
            rcc_pier.W3 = 0.0d;
            rcc_pier.W4 = 0.0d;
            rcc_pier.W5 = 0.0d;
            rcc_pier.total_vehicle_load = 0.0d;
            rcc_pier.D1 = 0.0d;
            rcc_pier.D2 = 0.0d;
            rcc_pier.D3 = 0.0d;

            rcc_pier.RL6 = 0.0d;
            rcc_pier.RL5 = 0.0d;
            rcc_pier.RL4 = 0.0d;
            rcc_pier.RL3 = 0.0d;
            rcc_pier.RL2 = 0.0d;
            rcc_pier.RL1 = 0.0d;
            rcc_pier.H1 = 0.0d;
            rcc_pier.H2 = 0.0d;
            rcc_pier.H3 = 0.0d;
            rcc_pier.H4 = 0.0d;
            rcc_pier.H5 = 0.0d;
            rcc_pier.H6 = 0.0d;
            rcc_pier.H7 = 0.0d;
            rcc_pier.H8 = 0.0d;
            rcc_pier.B1 = 0.0d;
            rcc_pier.B2 = 0.0d;
            rcc_pier.B3 = 0.0d;
            rcc_pier.B4 = 0.0d;
            rcc_pier.B5 = 0.0d;
            rcc_pier.B6 = 0.0d;
            rcc_pier.B7 = 0.0d;
            rcc_pier.B8 = 0.0d;
            rcc_pier.B9 = 0.0d;
            rcc_pier.B10 = 0.0d;
            rcc_pier.B11 = 0.0d;
            rcc_pier.B12 = 0.0d;
            rcc_pier.B13 = 0.0d;
            rcc_pier.B14 = 0.0d;
            rcc_pier.B15 = 1.078d;
            rcc_pier.B16 = 0.0d;
            rcc_pier.NR = 0.0d;
            rcc_pier.NP = 0.0d;
            rcc_pier.gama_c = 0.0d;
            rcc_pier.MX1 = 0.0d;
            rcc_pier.MY1 = 0.0d;
            rcc_pier.sigma_s = 0.0d;

            #region Data Input Form 1 Variables
            rcc_pier.L1 = MyList.StringToDouble(txt_RCC_Pier_L.Text, 0.0);
            rcc_pier.w1 = MyList.StringToDouble(txt_RCC_Pier_CW.Text, 0.0);
            rcc_pier.w2 = MyList.StringToDouble(txt_RCC_Pier__B.Text, 0.0);
            rcc_pier.w3 = MyList.StringToDouble(txt_RCC_Pier_Wp.Text, 0.0);


            rcc_pier.a1 = MyList.StringToDouble(txt_RCC_Pier_Hp.Text, 0.0);
            rcc_pier.NB = MyList.StringToDouble(txt_RCC_Pier_NMG.Text, 0.0);
            rcc_pier.d1 = MyList.StringToDouble(txt_RCC_Pier_DMG.Text, 0.0);
            rcc_pier.d2 = MyList.StringToDouble(txt_RCC_Pier_Ds.Text, 0.0);
            rcc_pier.gama_c = MyList.StringToDouble(txt_RCC_Pier_gama_c.Text, 0.0);
            rcc_pier.B1 = MyList.StringToDouble(txt_RCC_Pier_B1.Text, 0.0);
            rcc_pier.B2 = MyList.StringToDouble(txt_RCC_Pier_B2.Text, 0.0);
            rcc_pier.H1 = MyList.StringToDouble(txt_RCC_Pier_H1.Text, 0.0);
            rcc_pier.B3 = MyList.StringToDouble(txt_RCC_Pier_B3.Text, 0.0);
            rcc_pier.B4 = MyList.StringToDouble(txt_RCC_Pier_B4.Text, 0.0);
            rcc_pier.H2 = MyList.StringToDouble(txt_RCC_Pier_H2.Text, 0.0);
            rcc_pier.B5 = MyList.StringToDouble(txt_RCC_Pier_B5.Text, 0.0);
            rcc_pier.B6 = MyList.StringToDouble(txt_RCC_Pier_B6.Text, 0.0);
            rcc_pier.RL1 = MyList.StringToDouble(txt_RCC_Pier_RL1.Text, 0.0);
            rcc_pier.RL2 = MyList.StringToDouble(txt_RCC_Pier_RL2.Text, 0.0);
            rcc_pier.RL3 = MyList.StringToDouble(txt_RCC_Pier_RL3.Text, 0.0);
            rcc_pier.RL4 = MyList.StringToDouble(txt_RCC_Pier_RL4.Text, 0.0);
            rcc_pier.RL5 = MyList.StringToDouble(txt_RCC_Pier_RL5.Text, 0.0);
            rcc_pier.form_lev = MyList.StringToDouble(txt_RCC_Pier_Form_Lev.Text, 0.0);
            rcc_pier.B7 = MyList.StringToDouble(txt_RCC_Pier_B7.Text, 0.0);
            rcc_pier.H3 = MyList.StringToDouble(txt_RCC_Pier_H3.Text, 0.0);
            rcc_pier.H4 = MyList.StringToDouble(txt_RCC_Pier_H4.Text, 0.0);
            rcc_pier.B8 = MyList.StringToDouble(txt_RCC_Pier_B8.Text, 0.0);
            rcc_pier.H5 = MyList.StringToDouble(txt_RCC_Pier_H5.Text, 0.0);
            rcc_pier.H6 = MyList.StringToDouble(txt_RCC_Pier_H6.Text, 0.0);
            rcc_pier.H7 = MyList.StringToDouble(txt_RCC_Pier_H7.Text, 0.0);
            rcc_pier.B9 = MyList.StringToDouble(txt_RCC_Pier_B9.Text, 0.0);
            rcc_pier.B10 = MyList.StringToDouble(txt_RCC_Pier_B10.Text, 0.0);
            rcc_pier.B11 = MyList.StringToDouble(txt_RCC_Pier_B11.Text, 0.0);
            rcc_pier.B12 = MyList.StringToDouble(txt_RCC_Pier_B12.Text, 0.0);
            rcc_pier.B13 = MyList.StringToDouble(txt_RCC_Pier_B13.Text, 0.0);
            rcc_pier.B14 = MyList.StringToDouble(txt_RCC_Pier___B.Text, 0.0);
            rcc_pier.over_all = rcc_pier.H7 + rcc_pier.H5 + rcc_pier.H6;
            //rcc_pier.B15 = MyList.StringToDouble(txt_RCC_Pier_B15.Text, 0.0);


            rcc_pier.p1 = MyList.StringToDouble(txt_RCC_Pier_p1.Text, 0.0);
            rcc_pier.p2 = MyList.StringToDouble(txt_RCC_Pier_p2.Text, 0.0);
            rcc_pier.d_dash = MyList.StringToDouble(txt_RCC_Pier_d_dash.Text, 0.0);

            rcc_pier.D = MyList.StringToDouble(txt_RCC_Pier_D.Text, 0.0);
            rcc_pier.b = MyList.StringToDouble(txt_RCC_Pier_b.Text, 0.0);

            //rcc_pier.Pu = MyList.StringToDouble(txt_Pu.Text, 0.0);
            //rcc_pier.Mux = MyList.StringToDouble(txt_Mux.Text, 0.0);
            //rcc_pier.Muy = MyList.StringToDouble(txt_Muy.Text, 0.0);
            rcc_pier.NP = MyList.StringToDouble(txt_RCC_Pier_NP.Text, 0.0);
            rcc_pier.NR = MyList.StringToDouble(txt_RCC_Pier_NR.Text, 0.0);
            rcc_pier.MX1 = MyList.StringToDouble(txt_RCC_Pier_Mx1.Text, 0.0);
            rcc_pier.MY1 = MyList.StringToDouble(txt_RCC_Pier_Mz1.Text, 0.0);
            rcc_pier.total_vehicle_load = MyList.StringToDouble(txt_RCC_Pier_vehi_load.Text, 0.0);
            rcc_pier.W1 = MyList.StringToDouble(txt_RCC_Pier_W1_supp_reac.Text, 0.0);

            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);
            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);

            //rcc_pier.sigma_s = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
            //rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);



            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            //Chiranjit [2012 06 16]
            rcc_pier.NB = rcc_pier.NP;
            #endregion Data Input Form 1 Variables

            #region Data Input Form 2 Variables
            rcc_pier.P2 = MyList.StringToDouble(txt_pier_2_P2.Text, 0.0);
            rcc_pier.P3 = MyList.StringToDouble(txt_pier_2_P3.Text, 0.0);

            rcc_pier.B16 = MyList.StringToDouble(txt_pier_2_B16.Text, 0.0);
            //rcc_pier.total_pairs = MyList.StringToDouble(txt_pier_2_total_pairs.Text, 0.0);
            rcc_pier.PL = MyList.StringToDouble(txt_pier_2_PL.Text, 0.0);
            rcc_pier.PML = MyList.StringToDouble(txt_pier_2_PML.Text, 0.0);
            rcc_pier.APD = txt_pier_2_APD.Text;
            rcc_pier.PD = txt_pier_2_PD.Text;
            rcc_pier.SC = MyList.StringToDouble(txt_pier_2_SC.Text, 0.0);
            rcc_pier.HHF = MyList.StringToDouble(txt_pier_2_HHF.Text, 0.0);
            rcc_pier.V = MyList.StringToDouble(txt_pier_2_V.Text, 0.0);
            rcc_pier.K = MyList.StringToDouble(txt_pier_2_k.Text, 0.0);
            rcc_pier.CF = MyList.StringToDouble(txt_pier_2_CF.Text, 0.0);
            rcc_pier.LL = MyList.StringToDouble(txt_pier_2_LL.Text, 0.0);
            rcc_pier.Vr = MyList.StringToDouble(txt_pier_2_Vr.Text, 0.0);
            rcc_pier.Itc = MyList.StringToDouble(txt_pier_2_Itc.Text, 0.0);
            rcc_pier.sdia = MyList.StringToDouble(txt_pier_2_sdia.Text, 0.0);
            rcc_pier.sleg = MyList.StringToDouble(txt_pier_2_slegs.Text, 0.0);
            rcc_pier.ldia = MyList.StringToDouble(txt_pier_2_ldia.Text, 0.0);
            rcc_pier.SBC = MyList.StringToDouble(txt_pier_2_SBC.Text, 0.0);

            #endregion Data Input Form 2 Variables

        }
        #endregion Design of RCC Pier

        #region Abutment
        private void btn_Abutment_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();
            Abut = new RCC_AbutmentWall(iApp);
            Abut.FilePath = user_path;
            Abutment_Initialize_InputData();
            Abut.Write_Cantilever__User_input();
            Abut.Calculate_Program(Abut.rep_file_name);
            Abut.Write_Cantilever_Drawing_File();
            iApp.Save_Form_Record(this, user_path);
            MessageBox.Show(this, "Report file written in " + Abut.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(Abut.rep_file_name);
            Abut.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_Abutment_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Abut.rep_file_name);
        }
        private void Abutment_Initialize_InputData()
        {
            #region Variables Initialize with default data

            Abut.d1 = MyList.StringToDouble(txt_abut_DMG.Text, 0.0);
            Abut.t = MyList.StringToDouble(txt_abut_t.Text, 0.0);
            Abut.H = MyList.StringToDouble(txt_abut_H.Text, 0.0);
            Abut.a = MyList.StringToDouble(txt_abut_a.Text, 0.0);
            Abut.gamma_b = MyList.StringToDouble(txt_abut_gamma_b.Text, 0.0);
            Abut.gamma_c = MyList.StringToDouble(txt_abut_gamma_c.Text, 0.0);
            Abut.phi = MyList.StringToDouble(txt_abut_phi.Text, 0.0);
            Abut.p = MyList.StringToDouble(txt_abut_p_bearing_capacity.Text, 0.0);
            Abut.f_ck = MyList.StringToDouble(cmb_abut_fck.Text, 0.0);
            Abut.f_y = MyList.StringToDouble(cmb_abut_fy.Text, 0.0);
            Abut.w6 = MyList.StringToDouble(txt_abut_w6.Text, 0.0);
            Abut.w5 = MyList.StringToDouble(txt_abut_w5.Text, 0.0);
            Abut.F = MyList.StringToDouble(txt_abut_F.Text, 0.0);
            Abut.d2 = MyList.StringToDouble(txt_abut_d2.Text, 0.0);
            Abut.d3 = MyList.StringToDouble(txt_abut_d3.Text, 0.0);
            Abut.B = MyList.StringToDouble(txt_abut_B.Text, 0.0);
            Abut.theta = MyList.StringToDouble(txt_abut_theta.Text, 0.0);
            Abut.delta = MyList.StringToDouble(txt_abut_delta.Text, 0.0);
            Abut.z = MyList.StringToDouble(txt_abut_z.Text, 0.0);
            Abut.mu = MyList.StringToDouble(txt_abut_mu.Text, 0.0);
            Abut.L1 = MyList.StringToDouble(txt_abut_L1.Text, 0.0);
            Abut.L2 = MyList.StringToDouble(txt_abut_L2.Text, 0.0);
            Abut.L3 = MyList.StringToDouble(txt_abut_L3.Text, 0.0);
            Abut.L4 = MyList.StringToDouble(txt_abut_L4.Text, 0.0);
            Abut.h1 = MyList.StringToDouble(txt_abut_h1.Text, 0.0);
            Abut.L = MyList.StringToDouble(txt_abut_L.Text, 0.0);
            Abut.d4 = MyList.StringToDouble(txt_abut_d4.Text, 0.0);
            Abut.cover = MyList.StringToDouble(txt_abut_cover.Text, 0.0);
            Abut.factor = MyList.StringToDouble(txt_abut_fact.Text, 0.0);
            Abut.sc = MyList.StringToDouble(txt_abut_sc.Text, 0.0);

            #endregion
        }
        #endregion Abutment

        private void btn_dwg_open_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b.Name == btn_dwg_open.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Drawings of PSC I Girder Bridge"), "PSC_I_GIRDER_LS");
            }
            if (b.Name == btn_dwg_rcc_abut.Name)
            {
                //iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Cantilever", "Abutment_Sample");
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Abutment Drawings"), "PSC_I_Girder_Abutment");
            }
            else if (b.Name == btn_dwg_pier.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), "PSC_I_Girder_Pier");
            }
        }

        private void cmb_deck_input_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deck_Buttons();
        }

        private void Deck_Buttons()
        {
            ComboBox cmb = cmb_deck_input_files;

            string filename = "";
            if (Deck_Analysis != null)
            {
                if (cmb.SelectedIndex == 0) filename = Deck_Analysis.DeadLoadAnalysis_Input_File;
                else if (cmb.SelectedIndex == 1) filename = Deck_Analysis.LL_Analysis_1_Input_File;
                else if (cmb.SelectedIndex == 2) filename = Deck_Analysis.LL_Analysis_2_Input_File;
                else if (cmb.SelectedIndex == 3) filename = Deck_Analysis.LL_Analysis_3_Input_File;
                else if (cmb.SelectedIndex == 4) filename = Deck_Analysis.LL_Analysis_4_Input_File;
                else if (cmb.SelectedIndex == 5) filename = Deck_Analysis.LL_Analysis_5_Input_File;
                else if (cmb.SelectedIndex == 6) filename = Deck_Analysis.LL_Analysis_6_Input_File;
                else if (cmb.SelectedIndex == 7) filename = File_DeckSlab_Results;
            }
            btn_deck_view_data.Enabled = File.Exists(filename);
            btn_deck_view_moving.Enabled = File.Exists(MyList.Get_LL_TXT_File(filename)) && File.Exists(MyList.Get_Analysis_Report_File(filename));
            btn_deck_view_struc.Enabled = File.Exists(filename) && cmb.SelectedIndex != 7;
            btn_deck_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(filename));
        }

        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Set File Name

            string file_name = "";
            if (Bridge_Analysis != null)
            {
                if (cmb_long_open_file.SelectedIndex < cmb_long_open_file.Items.Count - 1)
                {
                    file_name = Bridge_Analysis.GetAnalysis_Input_File(cmb_long_open_file.SelectedIndex);
                }
                else
                {
                    file_name = File_Long_Girder_Results;
                }
            }

            #endregion Set File Name

            btn_view_data.Enabled = File.Exists(file_name);
            btn_View_Moving_Load.Enabled = File.Exists(MyList.Get_LL_TXT_File(file_name)) && File.Exists(MyList.Get_Analysis_Report_File(file_name));
            btn_view_structure.Enabled = File.Exists(file_name) && cmb_long_open_file.SelectedIndex != cmb_long_open_file.Items.Count - 1;
            btn_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));
        }
        //Chiranjit [2012 06 20]
        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            Control rbtn = sender as Control;


            if (rbtn.Name == chk_fp_left.Name)
            {
                if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                    chk_fp_right.Checked = true;
            }
            else if (rbtn.Name == chk_fp_right.Name)
            {
                if (!chk_fp_left.Checked && !chk_fp_right.Checked)
                    chk_fp_left.Checked = true;
            }

            if (rbtn.Name == chk_WC.Name)
            {
                grb_ana_wc.Enabled = chk_WC.Checked;
                if (grb_ana_wc.Enabled == false)
                {
                    txt_Ana_Dw.Text = "0.000";
                    txt_Ana_wgwc.Text = "0.000";
                }
                else
                {
                    txt_Ana_Dw.Text = "0.080";
                    txt_Ana_wgwc.Text = "22.000";
                }
            }
            else if (rbtn.Name == rbtn_crash_barrier.Name)
            {
                grb_ana_parapet.Enabled = rbtn_crash_barrier.Checked;
                if (!rbtn_crash_barrier.Checked)
                {
                    txt_Ana_Hc.Text = "0.000";
                    txt_Ana_Wc.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hc.Text = "1.200";
                    txt_Ana_Wc.Text = "0.500";
                }
            }
            else if (rbtn.Name == rbtn_footpath.Name)
            {
                grb_ana_sw_fp.Enabled = rbtn_footpath.Checked;
                if (!rbtn_footpath.Checked)
                {
                    txt_Ana_Wf.Text = "0.000";
                    txt_Ana_Hf.Text = "0.000";
                    txt_Ana_Wk.Text = "0.000";
                    txt_Ana_Wr.Text = "0.000";
                }
                else
                {
                    txt_Ana_Wf.Text = "1.000";
                    txt_Ana_Hf.Text = "0.250";
                    txt_Ana_Wk.Text = "0.500";
                    txt_Ana_Wr.Text = "0.100";
                }
            }

            if (rbtn_crash_barrier.Checked)
            {
                pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.case_1;

            }
            else if (rbtn_footpath.Checked)
            {
                if (chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.case_2;
                else if (chk_fp_left.Checked && !chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.case_3;
                else if (!chk_fp_left.Checked && chk_fp_right.Checked)
                    pic_diagram.BackgroundImage = LimitStateMethod.Properties.Resources.case_4;
            }

            Text_Changed();
        }
        public void Calculate_Load_Computation_1()
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();


            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for RCC T - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab"));
            wi1 = SMG * SCG * (Ds * Y_c_dry + Dw * wgwc);
            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wi1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wi2 = SCG * BMG * DMG * Y_c_dry;
            list.Add(string.Format("wi2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c_dry, wi2));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders"));
            wi3 = wi1 + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kN.",
                wi1, wi2, wi3));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL"));
            wi4 = wi3 / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kN/m.",
                wi3, SCG, wi4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders"));
            NIG = NMG * (NCG - 1);
            list.Add(string.Format("NIG = NMG*(NCG - 1) = {0:f0} * ({1:f0}-1) = {2:f0} nos.",
                NMG, NCG, NIG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            NIM = 70;
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            list.Add(string.Format("NIM = 70 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wiu = wi4 * NIG / NIM;
            list.Add(string.Format("wiu = wi4*NIG/NIM = {0:f3} * {1}/70 = {2:f3} kN/m. = {3:f3} Ton/m.",
                wi4, NIG, wiu, (wiu = wiu / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));

            list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} Ton/m.",
                wiu, swf, (wiu = wiu * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            //member_load.Add(string.Format("MEMBER LOAD "));
            member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));

            //list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("                131 TO 200 UNI GY -{0:f4}", wiu));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Outer Girder members"));
            list.Add(string.Format(""));
            //list.Add(string.Format("if(CL > CR) then (C=CL) else (C=CR)"));
            if (CL > CR) C = CL; else C = CR;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab and Wearing Course"));
            wo1 = ((SMG / 2) + C) * SCG * (Ds * Y_c_dry + Dw * wgwc);
            list.Add(string.Format("wo1 = [(SMG/2) + C]*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wo1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wo2 = SCG * BMG * DMG * Y_c_dry;
            list.Add(string.Format("wo2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c_dry, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hc * Wc * Y_c_dry;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hc, Wc, Y_c_dry, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Wf * Hs * Y_c_dry;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wf, Hs, Y_c_dry, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * Wr * Wk * Y_c_dry;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wr, Wk, Y_c_dry, wo5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders ")); wo6 = wo1 + wo2 + wo3 + wo4 + wo5;
            list.Add(string.Format("wo6 = wo1 + wo2 + wo3 + wo4 + wo5 "));
            list.Add(string.Format("   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} ",
                wo1, wo2, wo3, wo4, wo5));
            list.Add(string.Format("   = {0:f3} kN.", wo6));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL")); wo7 = wo6 / SCG;
            list.Add(string.Format("wo7 = wo6/SCG = {0:f3}/{1:f3} = {2:f3} kN/m.", wo6, SCG, wo7));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total outer members segments of main long girders")); NOG = 2 * (NCG - 1);
            list.Add(string.Format("NOG = 2*(NCG - 1) = 2*({0:f0}-1) = {1:f0} nos.", NCG, NOG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            NOM = 20;
            list.Add(string.Format("NOM = 20 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wou = wo7 * NOG / NOM;
            list.Add(string.Format("wou = wo7*NOG/NOM = {0:f3}*{1:f0}/{2} = {3:f3} kN/m. = {4:f3} Ton/m.",
                wo7, NOG, NOM, wou, (wou = wou / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));
            list.Add(string.Format("wou = wou*swf = {0:f3}*{1:f3} = {2:f4} Ton/m.",
                wou, swf, (wou = wou * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format("                MEMBER LOAD "));
            member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                121 TO 130 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c_dry;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c_dry, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            list.Add(string.Format("                13 TO 21 FZ -{0:f4}", wjl));
            list.Add(string.Format("                24 TO 32 FZ -{0:f4}", wjl));
            list.Add(string.Format("                35 TO 43 FZ -{0:f4}", wjl));
            list.Add(string.Format("                46 TO 54 FZ -{0:f4}", wjl));
            list.Add(string.Format("                57 TO 65 FZ -{0:f4}", wjl));
            list.Add(string.Format("                68 TO 76 FZ -{0:f4}", wjl));
            list.Add(string.Format("                79 TO 87 FZ -{0:f4}", wjl));
            list.Add(string.Format("                90 TO 98 FZ -{0:f4}", wjl));
            list.Add(string.Format("                101 TO 109 FZ -{0:f4}", wjl));


            //member_load.Add(string.Format("LOAD 2"));
            member_load.Add(string.Format("JOINT LOAD"));
            member_load.Add(string.Format("13 TO 21 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("24 TO 32 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("35 TO 43 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("46 TO 54 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("57 TO 65 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("68 TO 76 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("79 TO 87 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("90 TO 98 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("101 TO 109 FZ -{0:f4}", wjl));

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));

            txt_member_load.Lines = member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        public void Calculate_Load_Computation()
        {
            List<string> list = new List<string>();
            List<string> member_load = new List<string>();


            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for PSC I - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab"));
            wi1 = SMG * SCG * (Ds * Y_c_dry + Dw * wgwc);
            list.Add(string.Format("wi1 = SMG*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wi1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wi2 = SCG * BMG * DMG * Y_c_dry;
            list.Add(string.Format("wi2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c_dry, wi2));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders"));
            wi3 = wi1 + wi2;
            list.Add(string.Format("wi3 = wi1 + wi2 = {0:f3} + {1:f3} = {2:f3} kN.",
                wi1, wi2, wi3));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL"));
            wi4 = wi3 / SCG;
            list.Add(string.Format("wi4 = wi3/SCG = {0:f3} / {1:f3} = {2:f3} kN/m.",
                wi3, SCG, wi4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders"));
            NIG = NMG * (NCG - 1);
            list.Add(string.Format("NIG = NMG*(NCG - 1) = {0:f0} * ({1:f0}-1) = {2:f0} nos.",
                NMG, NCG, NIG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            NIM = 70;
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            list.Add(string.Format("NIM = 70 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wiu = wi4 * NIG / NIM;
            list.Add(string.Format("wiu = wi4*NIG/NIM = {0:f3} * {1}/70 = {2:f3} kN/m. = {3:f3} Ton/m.",
                wi4, NIG, wiu, (wiu = wiu / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));

            list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} Ton/m.",
                wiu, swf, (wiu = wiu * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            //member_load.Add(string.Format("MEMBER LOAD "));
            member_load.Add(string.Format("131 TO 200 UNI GY -{0:f4}", wiu));

            //list.Add(string.Format("                MEMBER LOAD "));
            list.Add(string.Format("                131 TO 200 UNI GY -{0:f4}", wiu));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Outer Girder members"));
            list.Add(string.Format(""));
            //list.Add(string.Format("if(CL > CR) then (C=CL) else (C=CR)"));
            if (CL > CR) C = CL; else C = CR;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load from RCC Deck Slab and Wearing Course"));
            wo1 = ((SMG / 2) + C) * SCG * (Ds * Y_c_dry + Dw * wgwc);
            list.Add(string.Format("wo1 = [(SMG/2) + C]*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wo1));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wo2 = SCG * BMG * DMG * Y_c_dry;
            list.Add(string.Format("wo2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c_dry, wo2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hc * Wc * Y_c_dry;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hc, Wc, Y_c_dry, wo3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk")); wo4 = SCG * Wf * Hs * Y_c_dry;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wf, Hs, Y_c_dry, wo4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * Wr * Wk * Y_c_dry;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wr, Wk, Y_c_dry, wo5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total load on main long girders ")); wo6 = wo1 + wo2 + wo3 + wo4 + wo5;
            list.Add(string.Format("wo6 = wo1 + wo2 + wo3 + wo4 + wo5 "));
            list.Add(string.Format("   = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} ",
                wo1, wo2, wo3, wo4, wo5));
            list.Add(string.Format("   = {0:f3} kN.", wo6));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL")); wo7 = wo6 / SCG;
            list.Add(string.Format("wo7 = wo6/SCG = {0:f3}/{1:f3} = {2:f3} kN/m.", wo6, SCG, wo7));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total outer members segments of main long girders")); NOG = 2 * (NCG - 1);
            list.Add(string.Format("NOG = 2*(NCG - 1) = 2*({0:f0}-1) = {1:f0} nos.", NCG, NOG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total inner members segments of main long girders in model (constant value)"));
            NOM = 20;
            list.Add(string.Format("NOM = 20 nos."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in inner members segments of main long girders in model "));
            wou = wo7 * NOG / NOM;
            list.Add(string.Format("wou = wo7*NOG/NOM = {0:f3}*{1:f0}/{2} = {3:f3} kN/m. = {4:f3} Ton/m.",
                wo7, NOG, NOM, wou, (wou = wou / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored UDL"));
            list.Add(string.Format("wou = wou*swf = {0:f3}*{1:f3} = {2:f4} Ton/m.",
                wou, swf, (wou = wou * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format("                MEMBER LOAD "));
            member_load.Add(string.Format("121 TO 130 UNI GY -{0:f4}", wou));
            member_load.Add(string.Format("201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                121 TO 130 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                201 TO 210 UNI GY -{0:f4}", wou));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c_dry;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c_dry, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            list.Add(string.Format("                13 TO 21 FZ -{0:f4}", wjl));
            list.Add(string.Format("                24 TO 32 FZ -{0:f4}", wjl));
            list.Add(string.Format("                35 TO 43 FZ -{0:f4}", wjl));
            list.Add(string.Format("                46 TO 54 FZ -{0:f4}", wjl));
            list.Add(string.Format("                57 TO 65 FZ -{0:f4}", wjl));
            list.Add(string.Format("                68 TO 76 FZ -{0:f4}", wjl));
            list.Add(string.Format("                79 TO 87 FZ -{0:f4}", wjl));
            list.Add(string.Format("                90 TO 98 FZ -{0:f4}", wjl));
            list.Add(string.Format("                101 TO 109 FZ -{0:f4}", wjl));


            //member_load.Add(string.Format("LOAD 2"));
            member_load.Add(string.Format("JOINT LOAD"));
            member_load.Add(string.Format("13 TO 21 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("24 TO 32 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("35 TO 43 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("46 TO 54 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("57 TO 65 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("68 TO 76 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("79 TO 87 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("90 TO 98 FZ -{0:f4}", wjl));
            member_load.Add(string.Format("101 TO 109 FZ -{0:f4}", wjl));

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));

            txt_member_load.Lines = member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        //Chiranjit [2013 05 03]
        List<double> deck_member_load = new List<double>();

        public void Calculate_Load_Computation_Old(string outer_girders, string inner_girders, List<string> joints_nos)
        {

            outer_girders = Bridge_Analysis._Outer_Girder_Support + " " + Bridge_Analysis._Outer_Girder_Mid;
            inner_girders = Bridge_Analysis._Inner_Girder_Support + " " + Bridge_Analysis._Inner_Girder_Mid;
            //Bridge_Analysis._
            List<string> list = new List<string>();
            List<string> long_member_load = new List<string>();

            //Bridge_Analysis

            double SMG, SCG, wi1, wi2, wi3, wi4, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for PSC I - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            list.Add(string.Format("//Load of self weight of main long girder"));
            wi1 = SCG * BMG * DMG * Y_c_dry;
            list.Add(string.Format("wi1 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN. = {5:f3} Ton",
                SCG, BMG, DMG, Y_c_dry, wi1, (wi1 = wi1 / 10)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("load on main girder = wi1 / {0:f3} = {1:f3} / {0:f3} = {2:f3} Ton/m", SCG, wi1, (wi1 = wi1 / SCG)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("LOAD 1 SELF WEIGHT"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi1));

            //member_load.Add(string.Format("LOAD 1 SELF WEIGHT"));
            //member_load.Add(string.Format("MEMBER LOAD"));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi1));



            list.Add(string.Format("//Load from RCC Deck Slab"));
            wi2 = SMG * SCG * (Ds * Y_c_dry + Dw * wgwc);
            list.Add(string.Format("wi2 = SMG*SCG*(Ds*Y_c + Dw*Y_w) "));
            list.Add(string.Format("   = {0:f3}*{1:f3}*({2:f3}*{3:f3}+{4:f3}*{5:f3}) ",
                SMG, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wi2));
            wi2 = wi2 / 10;
            list.Add(string.Format(""));
            list.Add(string.Format("   = {0:f3} T.", wi2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main girder = wi2 / {0:f3} = {1:f3} / {0:f3} = {2:f3} Ton/m", SCG, wi2, (wi2 = wi2 / SCG)));


            list.Add(string.Format(""));
            //member_load.Add(string.Format("LOAD 1 SELF WEIGHT"));
            //member_load.Add(string.Format("MEMBER LOAD"));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi1));
            //member_load.Add(string.Format("LOAD 2 DECK SLAB LOAD"));
            //member_load.Add(string.Format("MEMBER LOAD"));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi2));



            //list.Add(string.Format(""));
            //list.Add(string.Format("//Factored UDL"));
            //wiu = wi1;
            //list.Add(string.Format("wiu = wiu*swf = {0:f3} * {1:f3} = {2:f3} Ton/m.",
            //    wiu, swf, (wiu = wiu * swf)));
            //list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            //member_load.Add(string.Format("MEMBER LOAD "));
            //member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wiu));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Outer Girder members"));
            list.Add(string.Format(""));
            //list.Add(string.Format("if(CL > CR) then (C=CL) else (C=CR)"));
            if (CL > CR) C = CL; else C = CR;
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("//Load from RCC Deck Slab"));
            wo1 = ((SMG / 2) + C) * SCG * (Ds * Y_c_dry);

            list.Add(string.Format("wo1 = [(SMG/2) + C]*SCG*(Ds*Y_c) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wo1));
            list.Add(string.Format(""));
            wo1 = wo1 / 10;
            list.Add(string.Format("   = {0:f3} T.", wo1));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main girder = wo1 /  {0:f3} = {1:f3}/ {0:f3} = {2:f3} Ton/m", SCG, wo1, (wo1 = wo1 / SCG)));
            list.Add(string.Format(""));



            list.Add(string.Format("//Load from Wearing Course"));
            double wo11 = C * SCG * (Dw * wgwc);



            list.Add(string.Format("wo1 = C *SCG*(Dw*Y_w) "));
            list.Add(string.Format("   = ({0:f3}/2 + {1:f3})*{2:f3}*({3:f3}*{4:f3}+{5:f3}*{6:f3}) ",
                SMG, C, SCG, Ds, Y_c_dry, Dw, wgwc));
            list.Add(string.Format("   = {0:f3} kN.", wo11));
            list.Add(string.Format(""));
            wo11 = wo11 / 10;
            list.Add(string.Format("   = {0:f3} T.", wo11));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main girder = wo1 / {0:f3} = {1:f3}/{0:f3} = {2:f3} Ton", SCG, wo11, (wo11 = wo11 / SCG)));
            list.Add(string.Format(""));


            list.Add(string.Format("//Load of self weight of main long girder"));
            wo2 = SCG * BMG * DMG * Y_c_dry;
            list.Add(string.Format("wo2 = SCG*BMG*DMG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, BMG, DMG, Y_c_dry, wo2));

            wo2 = wo2 / 10;
            list.Add(string.Format("   = {0:f3} T.", wo2));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main outer girder = wo1 / {0:f3} = {1:f3}/{0:f3} = {2:f3} Ton/m", SCG, wo2, (wo2 = wo2 / SCG)));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("//Load of one side Parapet wall"));
            wo3 = SCG * Hc * Wc * Y_c_dry;
            list.Add(string.Format("wo3 = SCG*Hp*Wp*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
               SCG, Hc, Wc, Y_c_dry, wo3));
            list.Add(string.Format(""));
            wo3 = wo3 / 10;
            list.Add(string.Format("   = {0:f3} T.", wo3));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main outer girder = wo1 / {0:f3} = {1:f3}/{0:f3} = {2:f3} Ton", SCG, wo3, (wo3 = wo3 / SCG)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side walk"));

            wo4 = SCG * Wf * Hs * Y_c_dry;
            list.Add(string.Format("wo4 = SCG*Bs*Hs*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wf, Hs, Y_c_dry, wo4));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            wo4 = wo4 / 10;
            list.Add(string.Format("   = {0:f3} T.", wo4));
            list.Add(string.Format(""));
            list.Add(string.Format("load on main outer girder = wo1 / {0:f3} = {1:f3}/{0:f3} = {1:f3} Ton/m", SCG, wo4, (wo4 = wo4 / SCG)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Load of Side Walk Parapet wall")); wo5 = SCG * Wr * Wk * Y_c_dry;
            list.Add(string.Format("wo5 = SCG*Hps*Wps*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SCG, Wr, Wk, Y_c_dry, wo5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                **********************************"));
            list.Add(string.Format("                MEMBER LOAD "));


            long_member_load.Add(string.Format("LOAD 1 DEAD LOAD SELF WEIGHT"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi1));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo1));

            long_member_load.Add(string.Format("LOAD 2 DEAD LOAD DECK SLAB WET CONCRETE AND SHUTTERING"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi2 / 2));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo2 / 2));




            long_member_load.Add(string.Format("LOAD 3 DEAD LOAD DESHUTTERING"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi2));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo2));

            long_member_load.Add(string.Format("LOAD 4 DEAD LOAD SELF WEIGHT + DECK SLAB DRY CONCRETE"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi1 + wi2)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo1 + wo2)));




            //Chiranjit [2013 10 05]
            //Dead Load Value for DeckSlab analysis
            #region Dead Load Value for DeckSlab analysis
            deck_member_load.Clear();
            deck_member_load.Add(wo3);
            deck_member_load.Add(wo2);
            deck_member_load.Add(wo11);


            #endregion Dead Load Value for DeckSlab analysis


            long_member_load.Add(string.Format("LOAD 5 SIDL CRASH BARRIER"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo3));


            long_member_load.Add(string.Format("LOAD 6 SIDL WEARING COAT"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo11));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.AddRange(long_member_load.ToArray());
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * DCG * BCG * Y_c_dry;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c_dry, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            long_member_load.Add(string.Format("JOINT LOAD"));
            foreach (var item in joints_nos)
            {
                list.Add(string.Format("                {0} FY -{1:f4}", item, wjl));
                long_member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl));

            }

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));



            //member_load.Add(string.Format(""));
            txt_member_load.Lines = long_member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }

        //Chiranjit [2014 06 20]
        List<string> self_member_load = new List<string>();

        public void Calculate_Load_Computation(string outer_girders, string inner_girders, List<string> joints_nos)
        {

            outer_girders = Bridge_Analysis._Outer_Girder_Support + " " + Bridge_Analysis._Outer_Girder_Mid;
            inner_girders = Bridge_Analysis._Inner_Girder_Support + " " + Bridge_Analysis._Inner_Girder_Mid;
            //Bridge_Analysis._
            List<string> list = new List<string>();
            List<string> long_member_load = new List<string>();

            //Chiranjit [2014 06 20]
            self_member_load.Clear();
            //Bridge_Analysis

            double SMG, SCG, wi1, wi2, wi3, wi4, wi5, wi6, NIG, NIM, wiu, wo1, wo2, wo3, wo4, wo5, wo6, wo7, NOG, NOM;
            double wou, wc1, NIGJ, NIMJ, wjl, C;

            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format("ASTRA Load Computation for PSC I - Girder Bridge"));
            list.Add(string.Format("--------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of main long girders "));
            SMG = (B - CL - CR) / (NMG - 1);
            list.Add(string.Format("SMG = (B-CL-CR)/(NMG-1) = ({0:f3}-{1:f3}-{2:f3})/({3:f0}-1) = {4:f3} m.",
                B, CL, CR, NMG, SMG));
            list.Add(string.Format(""));
            list.Add(string.Format("//Spacing of cross girders "));
            SCG = L / (NCG - 1);
            list.Add(string.Format("SCG = L/(NCG-1) = {0:f3}/({1:f0}-1) = {2:f3} m.",
                L, NCG, SCG));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//UDL in all main long Inner Girder members"));
            list.Add(string.Format(""));

            //Self weight
            wi1 = (mid_ig.Girder_Area_Sum + sup_ig.Girder_Area_Sum) * Y_c_dry / 10;
            wo1 = (mid_og.Girder_Area_Sum + sup_og.Girder_Area_Sum) * Y_c_dry / 10;

            //Deck Slab Load Dry Concrete
            wi2 = (SMG / 2) * Ds * Y_c_dry / 10;
            wo2 = (CL + SMG / 2) * Ds * Y_c_dry / 10;


            //Deck Slab Load Wet Concrete
            wi3 = (SMG / 2) * Ds * Y_c_wet / 10;
            wo3 = (CL + SMG / 2) * Ds * Y_c_wet / 10;

            //Shuttering
            wi4 = (SMG / 2) * Ds * ils / 10;
            wo4 = (CL + SMG / 2) * Ds * ils / 10;

            //Crash Barrier
            //wi5 = wgc;
            wo5 = wgc / 10;


            // Wearing Coat
            //wi6 = (SMG / 2) * Ds * ils;
            wo6 = SCG * wgwc / 10;


            long_member_load.Add(string.Format("LOAD 1 DEAD LOAD SELF WEIGHT"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, wi1));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo1));

            //Chiranjit [2014 06 20]
            self_member_load.AddRange(long_member_load.ToArray());


            long_member_load.Add(string.Format("LOAD 2 DEAD LOAD DECK SLAB WET CONCRETE AND SHUTTERING"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi3 - wi4)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo3 - wo4)));

            long_member_load.Add(string.Format("LOAD 3 DEAD LOAD DESHUTTERING"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi3 + wi4)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo3 + wo4)));

            long_member_load.Add(string.Format("LOAD 4 DEAD LOAD SELF WEIGHT + DECK SLAB DRY CONCRETE"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", inner_girders, (wi1 + wi2)));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, (wo1 + wo2)));


            long_member_load.Add(string.Format("LOAD 5 SIDL CRASH BARRIER"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo5));


            long_member_load.Add(string.Format("LOAD 6 SIDL WEARING COAT"));
            long_member_load.Add(string.Format("MEMBER LOAD"));
            long_member_load.Add(string.Format("{0} UNI GY -{1:f4}", outer_girders, wo6));



            #region Dead Load Value for DeckSlab analysis
            deck_member_load.Clear();
            deck_member_load.Add(wo3);
            deck_member_load.Add(wo2);
            deck_member_load.Add(wo3);
            #endregion Dead Load Value for DeckSlab analysis



            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Concentrated JOINT LOADS in all main long Inner and Outer Girder members"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Self weight of Cross Girders"));
            list.Add(string.Format("")); wc1 = SMG * (DCG / 1000) * (BCG / 1000) * Y_c_dry;
            list.Add(string.Format("wc1 = SMG*DCG*BCG*Y_c = {0:f3}*{1:f3}*{2:f3}*{3:f3} = {4:f3} kN.",
                SMG, DCG, BCG, Y_c_dry, wc1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints"));
            list.Add(string.Format("")); NIGJ = NMG * NCG;
            list.Add(string.Format("NIGJ = NMG*NCG = {0:f0}*{1:f0}= {2:f0} nos.", NMG, NCG, NIGJ));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Total number of Inner Joints in model (Constant value, always)"));
            list.Add(string.Format("")); NIMJ = 81;
            list.Add(string.Format("NIMJ = 81"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Joint Loads applicable to all inner joints in model"));
            list.Add(string.Format("")); wjl = wc1 * NIGJ / NIMJ;
            list.Add(string.Format("wjl = wc1*NIGJ/NIMJ = {0:f3}*{1}/{2} = {3:f3} kN. = {4:f3} Ton.",
                 wc1, NIGJ, NIMJ, wjl, (wjl = wjl / 10.0)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//Factored Joint Load"));
            list.Add(string.Format("wjl = wjl*swf = {0:f3}*{1:f3} = {2:f4} Ton.",
                wjl, swf, (wjl = wjl * swf)));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("//In Analysis Input Data file UDL in all inner Girder members is to be mentioned as"));
            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format("                JOINT LOAD"));
            long_member_load.Add(string.Format("JOINT LOAD"));
            foreach (var item in joints_nos)
            {
                list.Add(string.Format("                {0} FY -{1:f4}", item, wjl));
                long_member_load.Add(string.Format("{0} FY -{1:f4}", item, wjl));

            }

            list.Add(string.Format(""));
            list.Add(string.Format("                ***********************************"));
            list.Add(string.Format(""));
            list.Add(string.Format("//END OF LOAD COMPUTATION"));
            list.Add(string.Format(""));



            //member_load.Add(string.Format(""));
            txt_member_load.Lines = long_member_load.ToArray();
            rtb_calc_load.Lines = list.ToArray();
            File.WriteAllLines(Path.Combine(user_path, "Load_Computation.txt"), list.ToArray());
            //iApp.RunExe(Path.Combine(user_path, "Load_Computation.txt"));
        }


        public double wi5 { get; set; }

        private void txt_pier_2_APD_TextChanged(object sender, EventArgs e)
        {

            txt_pier_2_APD.TextAlign = HorizontalAlignment.Left;
            txt_pier_2_APD.WordWrap = true;

            double b16 = MyList.StringToDouble(txt_pier_2_B16.Text, 0.0);

            string kStr = txt_pier_2_APD.Text.Replace(",", " ").Trim().TrimEnd().TrimStart();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            kStr = "";
            try
            {
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (mlist.GetDouble(i) < b16)
                    {
                        kStr += mlist.StringList[i] + ",";
                    }
                }
                kStr = kStr.Substring(0, kStr.Length - 1);
            }
            catch (Exception ex) { }

            txt_pier_2_PD.Text = kStr;
        }

        #region Chiranjit [2014 03 12] Support Input
        public string Start_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (rbtn_ssprt_pinned.Checked)
                    kStr = "PINNED";
                else if (rbtn_ssprt_fixed.Checked)
                {
                    kStr = "FIXED";


                    if (chk_ssprt_fixed_FX.Checked
                        || chk_ssprt_fixed_FY.Checked
                        || chk_ssprt_fixed_FZ.Checked
                        || chk_ssprt_fixed_MX.Checked
                        || chk_ssprt_fixed_MY.Checked
                        || chk_ssprt_fixed_MZ.Checked)
                        kStr += " BUT";

                    if (chk_ssprt_fixed_FX.Checked) kStr += " FX";
                    if (chk_ssprt_fixed_FY.Checked) kStr += " FY";
                    if (chk_ssprt_fixed_FZ.Checked) kStr += " FZ";
                    if (chk_ssprt_fixed_MX.Checked) kStr += " MX";
                    if (chk_ssprt_fixed_MY.Checked) kStr += " MY";
                    if (chk_ssprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
        }
        public string END_Support_Text
        {
            get
            {
                string kStr = "PINNED";
                if (rbtn_esprt_pinned.Checked)
                    kStr = "PINNED";
                else if (rbtn_esprt_fixed.Checked)
                {
                    kStr = "FIXED";
                    if (chk_esprt_fixed_FX.Checked
                        || chk_esprt_fixed_FY.Checked
                        || chk_esprt_fixed_FZ.Checked
                        || chk_esprt_fixed_MX.Checked
                        || chk_esprt_fixed_MY.Checked
                        || chk_esprt_fixed_MZ.Checked)
                        kStr += " BUT";
                    if (chk_esprt_fixed_FX.Checked) kStr += " FX";
                    if (chk_esprt_fixed_FY.Checked) kStr += " FY";
                    if (chk_esprt_fixed_FZ.Checked) kStr += " FZ";
                    if (chk_esprt_fixed_MX.Checked) kStr += " MX";
                    if (chk_esprt_fixed_MY.Checked) kStr += " MY";
                    if (chk_esprt_fixed_MZ.Checked) kStr += " MZ";
                }
                return kStr;
            }
        }

        private void rbtn_ssprt_pinned_CheckedChanged(object sender, EventArgs e)
        {

            chk_esprt_fixed_FX.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_FY.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_FZ.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MX.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MY.Enabled = rbtn_esprt_fixed.Checked;
            chk_esprt_fixed_MZ.Enabled = rbtn_esprt_fixed.Checked;

            chk_ssprt_fixed_FX.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_FY.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_FZ.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MX.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MY.Enabled = rbtn_ssprt_fixed.Checked;
            chk_ssprt_fixed_MZ.Enabled = rbtn_ssprt_fixed.Checked;
        }
        #endregion Chiranjit [2014 03 12] Support Input


        private void dgv_cable_data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cable_Auto_Data();
        }

        public List<string> cab1, cab2, cab3, cab4;
        private void Cable_Auto_Data()
        {

            cab1 = new List<string>();
            cab2 = new List<string>();
            cab3 = new List<string>();
            cab4 = new List<string>();

            #region Cable 1

            cab1.Add(string.Format("c2"));
            cab1.Add(string.Format("i2"));
            cab1.Add(string.Format("c3"));
            cab1.Add(string.Format("i3"));
            cab1.Add(string.Format("c5"));
            cab1.Add(string.Format("i5"));
            cab1.Add(string.Format("b7"));
            cab1.Add(string.Format("b8"));
            cab1.Add(string.Format("b9"));
            cab1.Add(string.Format("b11"));
            cab1.Add(string.Format("b12"));
            cab1.Add(string.Format("b13"));
            cab1.Add(string.Format("b14"));
            cab1.Add(string.Format("f7"));
            cab1.Add(string.Format("f14"));
            cab1.Add(string.Format("f17"));
            cab1.Add(string.Format("e85"));

            #endregion Cable 1

            #region Cable 2

            cab2.Add(string.Format(""));
            cab2.Add(string.Format(""));
            cab2.Add(string.Format("c3"));
            cab2.Add(string.Format(""));
            cab2.Add(string.Format(""));
            cab2.Add(string.Format(""));
            cab2.Add(string.Format("b7"));
            cab2.Add(string.Format("b8"));
            cab2.Add(string.Format("b9"));
            cab2.Add(string.Format("b11"));
            cab2.Add(string.Format("b12"));
            cab2.Add(string.Format("b13"));
            cab2.Add(string.Format("b14"));
            cab2.Add(string.Format(""));
            cab2.Add(string.Format("f14"));
            cab2.Add(string.Format("f17"));
            cab2.Add(string.Format("e89"));

            #endregion Cable 1

            #region Cable 3
            cab3.Add(string.Format(""));
            cab3.Add(string.Format(""));
            cab3.Add(string.Format("c3"));
            cab3.Add(string.Format(""));
            cab3.Add(string.Format(""));
            cab3.Add(string.Format(""));
            cab3.Add(string.Format("b7"));
            cab3.Add(string.Format("b8"));
            cab3.Add(string.Format("b9"));
            cab3.Add(string.Format("b11"));
            cab3.Add(string.Format("b12"));
            cab3.Add(string.Format("b13"));
            cab3.Add(string.Format("b14"));
            cab3.Add(string.Format(""));
            cab3.Add(string.Format("f14"));
            cab3.Add(string.Format("f17"));
            cab3.Add(string.Format("e77"));
            #endregion Cable 3

            #region Cable 3
            cab4.Add(string.Format(""));
            cab4.Add(string.Format(""));
            cab4.Add(string.Format("c3"));
            cab4.Add(string.Format(""));
            cab4.Add(string.Format(""));
            cab4.Add(string.Format(""));
            cab4.Add(string.Format("b7"));
            cab4.Add(string.Format("b8"));
            cab4.Add(string.Format("b9"));
            cab4.Add(string.Format("b11"));
            cab4.Add(string.Format("b12"));
            cab4.Add(string.Format("b13"));
            cab4.Add(string.Format("b14"));
            cab4.Add(string.Format(""));
            cab4.Add(string.Format("f14"));
            cab4.Add(string.Format("f17"));
            cab4.Add(string.Format("e85"));
            #endregion Cable 3

            for (int i = 0; i < cab1.Count; i++)
            {
                if (cab1[i] == "")
                    dgv_cable_data[2, i].Style.ForeColor = Color.Blue;
                else
                    dgv_cable_data[2, i].Style.ForeColor = Color.Black;

                if (cab2[i] == "")
                {
                    dgv_cable_data[3, i].Style.ForeColor = Color.Blue;

                    dgv_cable_data[3, i].Value = dgv_cable_data[2, i].Value;
                    dgv_cable_data[4, i].Value = dgv_cable_data[2, i].Value;
                    dgv_cable_data[5, i].Value = dgv_cable_data[2, i].Value;
                }
                else
                    dgv_cable_data[3, i].Style.ForeColor = Color.Black;

                if (cab3[i] == "")
                    dgv_cable_data[4, i].Style.ForeColor = Color.Blue;
                else
                    dgv_cable_data[4, i].Style.ForeColor = Color.Black;

                if (cab4[i] == "")
                    dgv_cable_data[5, i].Style.ForeColor = Color.Blue;
                else
                    dgv_cable_data[5, i].Style.ForeColor = Color.Black;
            }

        }

        private void btn_edit_load_combs_Click(object sender, EventArgs e)
        {
            LimitStateMethod.LoadCombinations.frm_LoadCombination ff = new LoadCombinations.frm_LoadCombination(iApp, dgv_long_liveloads, dgv_long_loads);

            ff.Owner = this;

            ff.ShowDialog();

            //ff.Filled_Type_LoadData(dgv_long_loads);
            //ff.Filled_LoadData(dgv_long_liveloads);

            //Default_Moving_LoadData
        }

        private void btn_deck_ll_Click(object sender, EventArgs e)
        {
            Deckslab_Moving_Loads();
        }

        List<DeckSlabLoads> Deck_LL_Loads { get; set; }
        private void Deckslab_Moving_Loads()
        {
            if (Deck_LL_Loads == null)
                Deck_LL_Loads = new List<DeckSlabLoads>();

            Deck_LL_Loads.Clear();

            DeckSlabLoads dslds = new DeckSlabLoads();

            //Deck_Analysis

            List<string> list = new List<string>();



            Member mbr;


            double pos = 0;

            int i = 0;

            #region Load 1

            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;  
            dslds.Width = B;  
            dslds.CL = CL; 
            dslds.NMG = NMG;  
            dslds.Alpha = 2.6; 
            dslds.Wct = Wc;  
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll1_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll1_lw2.Text, 0.0);

            dslds.lw3 = 0.0;
            dslds.lw4 = 0.0;


            dslds.wacc = MyList.StringToDouble(txt_deck_ll1_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll1_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll1_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll1_dTrans.Text, 0.0);
            dslds.min_ed = MyList.StringToDouble(txt_deck_ll1_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll1_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll1_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll1_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll1_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll1_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll1_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_1 = list;

            #endregion Load 1
            Deck_LL_Loads.Add(dslds);

            #region Load 2
            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll2_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll2_lw2.Text, 0.0);

            dslds.lw3 = MyList.StringToDouble(txt_deck_ll2_lw3.Text, 0.0);
            dslds.lw4 = MyList.StringToDouble(txt_deck_ll2_lw4.Text, 0.0);


            dslds.wacc = MyList.StringToDouble(txt_deck_ll2_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll2_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll2_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll2_dTrans.Text, 0.0);
            dslds.min_ed = MyList.StringToDouble(txt_deck_ll2_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll2_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll2_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll2_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll2_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll2_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll2_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_2 = list;

            #endregion Load 2
            Deck_LL_Loads.Add(dslds);


            #region Load 3
            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll3_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll3_lw2.Text, 0.0);

            //dslds.lw3 = MyList.StringToDouble(txt_deck_ll3_lw3.Text, 0.0);
            //dslds.lw4 = MyList.StringToDouble(txt_deck_ll3_lw4.Text, 0.0);

            dslds.max_tyre_pressure = MyList.StringToDouble(txt_deck_ll3_max_prs.Text, 0.0);
            dslds.contact_area = MyList.StringToDouble(txt_deck_ll3_cnt_area.Text, 0.0);

            dslds.wacc = MyList.StringToDouble(txt_deck_ll3_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll3_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll3_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll3_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll3_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll3_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll3_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll3_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll3_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll3_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll3_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_3 = list;

            #endregion Load 3
            Deck_LL_Loads.Add(dslds);


            #region Load 4

            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll4_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll4_lw2.Text, 0.0);

            dslds.lw3 = MyList.StringToDouble(txt_deck_ll4_lw3.Text, 0.0);
            dslds.lw4 = MyList.StringToDouble(txt_deck_ll4_lw4.Text, 0.0);


            dslds.max_tyre_pressure = MyList.StringToDouble(txt_deck_ll4_max_prs.Text, 0.0);
            dslds.contact_area = MyList.StringToDouble(txt_deck_ll4_cnt_area.Text, 0.0);


            dslds.wacc = MyList.StringToDouble(txt_deck_ll4_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll4_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll4_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll4_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll4_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll4_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll4_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll4_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll4_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll4_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll4_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_4 = list;

            #endregion Load 4

            Deck_LL_Loads.Add(dslds);

            #region Load 5

            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll5_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll5_lw2.Text, 0.0);

            dslds.lw3 = 0.0;
            dslds.lw4 = 0.0;


            dslds.wacc = MyList.StringToDouble(txt_deck_ll5_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll5_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll5_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll5_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll5_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll5_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll5_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll5_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll5_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll5_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll5_impf.Text, 0.0);
            dslds.Calculate_Data();
            Set_Deckslab_Wheel_Data(dslds, list);
            deck_ll_5 = list;

            #endregion Load 5
            Deck_LL_Loads.Add(dslds);


            #region Load 6

            list = new List<string>();
            dslds = new DeckSlabLoads();
            dslds.SMG = SMG;
            dslds.Width = B;
            dslds.CL = CL;
            dslds.NMG = NMG;
            dslds.Alpha = 2.6;
            dslds.Wct = Wc;
            dslds.Ds = Ds;
            dslds.b_by_lo = (L - 2 * os) / SMG / 2;

            dslds.lw1 = MyList.StringToDouble(txt_deck_ll6a_lw1.Text, 0.0);
            dslds.lw2 = MyList.StringToDouble(txt_deck_ll6a_lw2.Text, 0.0);


            dslds.max_tyre_pressure = MyList.StringToDouble(txt_deck_ll6a_max_prs.Text, 0.0);
            dslds.contact_area = MyList.StringToDouble(txt_deck_ll6a_cnt_area.Text, 0.0);

            //dslds.lw3 = MyList.StringToDouble(txt_deck_ll6a_lw3.Text, 0.0);
            //dslds.lw4 = MyList.StringToDouble(txt_deck_ll6a_lw4.Text, 0.0);


            dslds.wacc = MyList.StringToDouble(txt_deck_ll6a_wacc.Text, 0.0);

            dslds.walg = MyList.StringToDouble(txt_deck_ll6a_walg.Text, 0.0);
            dslds.dTraffic = MyList.StringToDouble(txt_deck_ll6a_dTraffic.Text, 0.0);
            dslds.dTrans = MyList.StringToDouble(txt_deck_ll6a_dTrans.Text, 0.0);
            //dslds.min_ed = MyList.StringToDouble(txt_deck_ll6a_min_ed.Text, 0.0);
            dslds.b1 = MyList.StringToDouble(txt_deck_ll6a_b1.Text, 0.0);
            dslds.initpos = MyList.StringToDouble(txt_deck_ll6a_initpos.Text, 0.0);
            dslds.finalpos = MyList.StringToDouble(txt_deck_ll6a_finalpos.Text, 0.0);
            dslds.nlg = MyList.StringToDouble(txt_deck_ll6a_nlg.Text, 0.0);
            dslds.incr = MyList.StringToDouble(txt_deck_ll6a_incr.Text, 0.0);
            dslds.impf = MyList.StringToDouble(txt_deck_ll6a_impf.Text, 0.0);
            dslds.Calculate_Data();
            //Set_Deckslab_Comb_Wheel_Data(dslds, list);
            //deck_ll_4 = list;



            //list = new List<string>();
            DeckSlabLoads data2 = new DeckSlabLoads();
            data2.SMG = SMG;
            data2.Width = B;
            data2.CL = CL;
            data2.NMG = NMG;
            data2.Alpha = 2.6;
            data2.Wct = Wc;
            data2.Ds = Ds;
            data2.b_by_lo = (L - 2 * os) / SMG / 2;

            data2.lw1 = MyList.StringToDouble(txt_deck_ll6_lw1.Text, 0.0);
            data2.lw2 = MyList.StringToDouble(txt_deck_ll6_lw2.Text, 0.0);

            //data2.lw3 = MyList.StringToDouble(txt_deck_ll6_lw3.Text, 0.0);
            //data2.lw4 = MyList.StringToDouble(txt_deck_ll6_lw4.Text, 0.0);


            data2.wacc = MyList.StringToDouble(txt_deck_ll6_wacc.Text, 0.0);

            data2.walg = MyList.StringToDouble(txt_deck_ll6_walg.Text, 0.0);
            data2.dTraffic = MyList.StringToDouble(txt_deck_ll6_dTraffic.Text, 0.0);
            data2.dTrans = MyList.StringToDouble(txt_deck_ll6_dTrans.Text, 0.0);
            //data2.min_ed = MyList.StringToDouble(txt_deck_ll6_min_ed.Text, 0.0);
            data2.b1 = MyList.StringToDouble(txt_deck_ll6_b1.Text, 0.0);
            data2.initpos = MyList.StringToDouble(txt_deck_ll6_initpos.Text, 0.0);
            data2.finalpos = MyList.StringToDouble(txt_deck_ll6_finalpos.Text, 0.0);
            data2.nlg = MyList.StringToDouble(txt_deck_ll6_nlg.Text, 0.0);
            data2.incr = MyList.StringToDouble(txt_deck_ll6_incr.Text, 0.0);
            data2.impf = MyList.StringToDouble(txt_deck_ll6_impf.Text, 0.0);
            data2.Calculate_Data();
            Set_Deckslab_Comb_Wheel_Data(dslds, data2, list);
            deck_ll_6 = list;
            #endregion Load 6
            Deck_LL_Loads.Add(dslds);
            Deck_LL_Loads.Add(data2);


        }

        private void Set_Deckslab_Wheel_Data(DeckSlabLoads dslds, List<string> list)
        {
            Member mbr;

            for (int i = 0; i < dslds.WH_1_data.Count; i++)
            {
                list.Add(string.Format("LOAD {0}", (i + 1)));
                list.Add(string.Format("MEMBER LOAD"));

                var item = dslds.WH_1_data[i];
                mbr = Get_Deck_Member(item.Position_X);

                if (mbr != null)
                {
                    //list.Add(string.Format("{0} CON GY -{1:f3} D {2:f3}", mbr.MemberNo, item.P_with_Impact, (item.Position_X - mbr.StartNode.X)));
                    list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                }

                if (dslds.WH_2_data.Count > 0)
                {
                    item = dslds.WH_2_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (dslds.WH_3_data.Count > 0)
                {
                    item = dslds.WH_3_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }
                if (dslds.WH_4_data.Count > 0)
                {
                    item = dslds.WH_4_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }
            }
            //pos = 0.0;
        }


        private void Set_Deckslab_Comb_Wheel_Data(DeckSlabLoads data1, DeckSlabLoads data2, List<string> list)
        {

            Member mbr;

            for (int i = 0; i < data1.WH_1_data.Count; i++)
            {
                list.Add(string.Format("LOAD {0}", (i + 1)));
                list.Add(string.Format("MEMBER LOAD"));

                var item = data1.WH_1_data[i];
                mbr = Get_Deck_Member(item.Position_X);

                if (mbr != null)
                {
                    //list.Add(string.Format("{0} CON GY -{1:f3} D {2:f3}", mbr.MemberNo, item.P_with_Impact, (item.Position_X - mbr.StartNode.X)));
                    list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                }

                if (data2.WH_1_data.Count > 0)
                {
                    item = data2.WH_1_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }



                if (data1.WH_2_data.Count > 0)
                {
                    item = data1.WH_2_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }


                if (data2.WH_2_data.Count > 0)
                {
                    item = data2.WH_2_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }


                if (data1.WH_3_data.Count > 0)
                {
                    item = data1.WH_3_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (data2.WH_3_data.Count > 0)
                {
                    item = data2.WH_3_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (data1.WH_4_data.Count > 0)
                {
                    item = data1.WH_4_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }

                if (data2.WH_4_data.Count > 0)
                {
                    item = data2.WH_4_data[i];
                    mbr = Get_Deck_Member(item.Position_X);

                    if (mbr != null)
                    {
                        list.Add(string.Format("{0} CON GY -{1:f3} {2:f3}", mbr.MemberNo, item.P_with_Impact / 10.0, (item.Position_X - mbr.StartNode.X)));
                    }
                }
            }
            //pos = 0.0;
        }

        public Member Get_Deck_Member(double pos)
        {
            int i = 0;
            Member mbr;
            for(i = 0; i < Deck_Analysis.MemColls.Count;i++)
            {
                mbr = Deck_Analysis.MemColls[i];
                if (pos >= mbr.StartNode.X && pos <= mbr.EndNode.X)
                    return mbr;
            }
            return null;
        }

        private void txt_deck_ll_lw1_TextChanged(object sender, EventArgs e)
        {
            Deckslab_Interactives();
        }

        private void Deckslab_Interactives()
        {

            Excel_User_Inputs eui = new Excel_User_Inputs();

            eui.Read_From_Grid(dgv_deck_user_input);

            double wacc = MyList.StringToDouble(txt_deck_ll1_wacc.Text, 0.0);
            double walg = MyList.StringToDouble(txt_deck_ll1_walg.Text, 0.0);
            double min_ed = MyList.StringToDouble(txt_deck_ll1_min_ed.Text, 0.0);
            double dTrans = MyList.StringToDouble(txt_deck_ll1_dTrans.Text, 0.0);
            double dTraffic = MyList.StringToDouble(txt_deck_ll2_dTraffic.Text, 0.0);
            double nlg = MyList.StringToDouble(txt_deck_ll1_nlg.Text, 0.0);

            double b1 = (wacc / 1000) + 2 * Dw;
            double initpos = Wc + Wf + min_ed;
            double finalpos = B - initpos - dTrans;
            double incr = (finalpos - initpos) / nlg;
            double impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll1_b1.Text = b1.ToString("f3");
            txt_deck_ll1_initpos.Text = initpos.ToString("f3");
            txt_deck_ll1_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll1_incr.Text = incr.ToString("f4");
            txt_deck_ll1_impf.Text = impf.ToString("f2");

            #region Deck Slab Load 1


            wacc = MyList.StringToDouble(txt_deck_ll1_wacc.Text, 0.0);
            walg = MyList.StringToDouble(txt_deck_ll1_walg.Text, 0.0);
            min_ed = MyList.StringToDouble(txt_deck_ll1_min_ed.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll1_dTrans.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll1_nlg.Text, 0.0);

            b1 = (wacc / 1000) + 2 * Dw;
            initpos = Wc + Wf + min_ed;
            finalpos = B - initpos - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll1_b1.Text = b1.ToString("f3");
            txt_deck_ll1_initpos.Text = initpos.ToString("f3");
            txt_deck_ll1_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll1_incr.Text = incr.ToString("f4");
            //txt_deck_ll1_impf.Text = impf.ToString("f2");


            #endregion Deck Slab Load 1

            #region Deck Slab Load 2

            wacc = MyList.StringToDouble(txt_deck_ll2_wacc.Text, 0.0);
            walg = MyList.StringToDouble(txt_deck_ll2_walg.Text, 0.0);
            min_ed = MyList.StringToDouble(txt_deck_ll2_min_ed.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll2_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll1_dTraffic.Text, 0.0) + walg / 1000;
            nlg = MyList.StringToDouble(txt_deck_ll2_nlg.Text, 0.0);

            b1 = (wacc / 1000) + 2 * Dw;
            initpos = Wc + Wf + min_ed;
            finalpos = B - initpos - dTrans - dTraffic - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll2_dTraffic.Text = dTraffic.ToString("f3");
            txt_deck_ll2_b1.Text = b1.ToString("f3");
            txt_deck_ll2_initpos.Text = initpos.ToString("f3");
            txt_deck_ll2_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll2_incr.Text = incr.ToString("f4");
            //txt_deck_ll2_impf.Text = impf.ToString("f2");


            #endregion Deck Slab Load 2

            #region Deck Slab Load 3

            wacc = MyList.StringToDouble(txt_deck_ll3_wacc.Text, 0.0);
            //walg = MyList.StringToDouble(txt_deck_ll3_walg.Text, 0.0);

            double max_wl = MyList.StringToDouble(txt_deck_ll3_max_wl.Text, 0.0);
            double max_prs = MyList.StringToDouble(txt_deck_ll3_max_prs.Text, 0.0);
            double cnt_area = max_wl * 100.0 / max_prs;


            walg = cnt_area * 100 / wacc;

            //initpos = MyList.StringToDouble(txt_deck_ll3_initpos.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll3_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll3_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll3_nlg.Text, 0.0);


            b1 = (walg / 1000) + 2 * Dw;
            initpos = Wc + Wf + dTraffic;
            finalpos = B - initpos - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll3_walg.Text = walg.ToString("f3");
            txt_deck_ll3_b1.Text = b1.ToString("f3");
            txt_deck_ll3_initpos.Text = initpos.ToString("f3");
            txt_deck_ll3_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll3_incr.Text = incr.ToString("f4");
            txt_deck_ll3_cnt_area.Text = cnt_area.ToString("f2");


            #endregion Deck Slab Load 4

            #region Deck Slab Load 4

            wacc = MyList.StringToDouble(txt_deck_ll4_wacc.Text, 0.0);
            //walg = MyList.StringToDouble(txt_deck_ll4_walg.Text, 0.0);

             max_wl = MyList.StringToDouble(txt_deck_ll4_max_wl.Text, 0.0);
             max_prs = MyList.StringToDouble(txt_deck_ll4_max_prs.Text, 0.0);
             cnt_area = max_wl * 100.0 / max_prs;


            walg = cnt_area * 100 / wacc;

            initpos = MyList.StringToDouble(txt_deck_ll4_initpos.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll4_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll4_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll4_nlg.Text, 0.0);


            b1 = (walg / 1000) + 2 * Dw;
            //initpos = Wc + Wf + dTraffic;
            finalpos = B - initpos - dTrans - dTraffic - dTrans;
            incr = (finalpos - initpos) / nlg;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll4_walg.Text = walg.ToString("f3");
            txt_deck_ll4_b1.Text = b1.ToString("f3");
            txt_deck_ll4_initpos.Text = initpos.ToString("f3");
            txt_deck_ll4_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll4_incr.Text = incr.ToString("f4");
            txt_deck_ll4_cnt_area.Text = cnt_area.ToString("f2");


            #endregion Deck Slab Load 4


            #region Deck Slab Load 5


            wacc = MyList.StringToDouble(txt_deck_ll5_wacc.Text, 0.0);
            walg = MyList.StringToDouble(txt_deck_ll5_walg.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll5_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll5_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll5_nlg.Text, 0.0);

            b1 = (wacc / 1000) + 2 * Dw;
            initpos = Wc + Wf + dTraffic + walg / 2 / 1000;
            finalpos = B - initpos - dTrans;
            incr = (finalpos - initpos) / nlg;

            txt_deck_ll5_b1.Text = b1.ToString("f3");
            txt_deck_ll5_initpos.Text = initpos.ToString("f3");
            txt_deck_ll5_finalpos.Text = finalpos.ToString("f3");
            txt_deck_ll5_incr.Text = incr.ToString("f4");
            //txt_deck_ll5_impf.Text = impf.ToString("f2");


            #endregion Deck Slab Load 5


            #region Deck Slab Load 6


            #region Deck Slab Load 6 Type 1

            wacc = MyList.StringToDouble(txt_deck_ll6a_wacc.Text, 0.0);
            //walg = MyList.StringToDouble(txt_deck_ll6a_walg.Text, 0.0);

            max_wl = MyList.StringToDouble(txt_deck_ll6a_max_wl.Text, 0.0);
            max_prs = MyList.StringToDouble(txt_deck_ll6a_max_prs.Text, 0.0);
            cnt_area = max_wl * 100.0 / max_prs;


            walg = cnt_area * 100 / wacc;

            initpos = MyList.StringToDouble(txt_deck_ll6a_initpos.Text, 0.0);
            dTrans = MyList.StringToDouble(txt_deck_ll6a_dTrans.Text, 0.0);
            dTraffic = MyList.StringToDouble(txt_deck_ll6a_dTraffic.Text, 0.0);
            nlg = MyList.StringToDouble(txt_deck_ll6a_nlg.Text, 0.0);


            double wacc1 = MyList.StringToDouble(txt_deck_ll6_wacc.Text, 0.0);
            double walg1 = MyList.StringToDouble(txt_deck_ll6_walg.Text, 0.0);
            double dTrans1 = MyList.StringToDouble(txt_deck_ll6_dTrans.Text, 0.0);
            double dTraffic1 = MyList.StringToDouble(txt_deck_ll6_dTraffic.Text, 0.0);
            double nlg1 = MyList.StringToDouble(txt_deck_ll6_nlg.Text, 0.0);


            //=$G$376+1.93+(0.86/2+1.2+0.5/2+1.8)

            double val = initpos + dTrans + dTraffic + dTrans1 + walg1 / 2 / 1000 - 0.43;

            double val2 = (B - walg1 / 1000 - min_ed - val);

            b1 = (walg / 1000) + 2 * Dw;


            double initpos1 = initpos + dTrans + dTraffic;



            double finalpos1 = B - dTraffic1 - walg1/1000 - min_ed - 0.42;
            incr = (finalpos1 - initpos1) / nlg1;
            //impf = 1 + 4.5 / (6 + SMG);

            txt_deck_ll6a_walg.Text = walg.ToString("f3");
            txt_deck_ll6a_b1.Text = b1.ToString("f3");
            txt_deck_ll6a_initpos.Text = initpos.ToString("f3");
            txt_deck_ll6a_finalpos.Text = (initpos + val2).ToString("f3");
            txt_deck_ll6a_incr.Text = incr.ToString("f4");
            txt_deck_ll6a_cnt_area.Text = cnt_area.ToString("f2");


            #endregion Deck Slab Load 6



            b1 = (wacc / 1000) + 2 * Dw;
            //initpos = Wc + Wf + dTraffic + walg / 2 / 1000;
            //finalpos = B - initpos - dTrans;
            incr = (finalpos1 - initpos1) / nlg;

            txt_deck_ll6_b1.Text = b1.ToString("f3");
            txt_deck_ll6_initpos.Text = initpos1.ToString("f3");
            txt_deck_ll6_finalpos.Text = (initpos1 + val2).ToString("f3");
            txt_deck_ll6_incr.Text = incr.ToString("f4");
            //txt_deck_ll6_impf.Text = impf.ToString("f2");





            #endregion Deck Slab Load 6


        }
     
        #region British Standard Loading

        private void txt_deck_width_TextChanged(object sender, EventArgs e)
        {
            British_Interactive();
           
        }



        private void rbtn_HA_HB_CheckedChanged(object sender, EventArgs e)
        {
            British_Interactive();
       }

        public void  British_Interactive()
        {
            double d, lane_width, impf, lf;
            int nos_lane, i;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);

            txt_no_lanes.Text = nos_lane.ToString();
            txt_no_lanes.Enabled = false;


            chk_HA_1L.Enabled = (nos_lane >= 1);
            chk_HA_2L.Enabled = (nos_lane >= 2);
            chk_HA_3L.Enabled = (nos_lane >= 3);
            chk_HA_4L.Enabled = (nos_lane >= 4);
            chk_HA_5L.Enabled = (nos_lane >= 5);
            chk_HA_6L.Enabled = (nos_lane >= 6);
            chk_HA_7L.Enabled = (nos_lane >= 7);
            chk_HA_8L.Enabled = (nos_lane >= 8);
            chk_HA_9L.Enabled = (nos_lane >= 9);
            chk_HA_10L.Enabled = (nos_lane >= 10);


            chk_HB_1L.Enabled = (nos_lane >= 1);
            chk_HB_2L.Enabled = (nos_lane >= 2);
            chk_HB_3L.Enabled = (nos_lane >= 3);
            chk_HB_4L.Enabled = (nos_lane >= 4);
            chk_HB_5L.Enabled = (nos_lane >= 5);
            chk_HB_6L.Enabled = (nos_lane >= 6);
            chk_HB_7L.Enabled = (nos_lane >= 7);
            chk_HB_8L.Enabled = (nos_lane >= 8);
            chk_HB_9L.Enabled = (nos_lane >= 9);
            chk_HB_10L.Enabled = (nos_lane >= 10);


            grb_ha.Enabled = (rbtn_HA.Checked || rbtn_HA_HB.Checked);
            grb_hb.Enabled = (rbtn_HB.Checked || rbtn_HA_HB.Checked);

            if (rbtn_HA.Checked)
            {
                chk_HA_1L.Checked = chk_HA_1L.Enabled;
                chk_HA_2L.Checked = chk_HA_2L.Enabled;
                chk_HA_3L.Checked = chk_HA_3L.Enabled;
                chk_HA_4L.Checked = chk_HA_4L.Enabled;
                chk_HA_5L.Checked = chk_HA_5L.Enabled;
                chk_HA_6L.Checked = chk_HA_6L.Enabled;
                chk_HA_7L.Checked = chk_HA_7L.Enabled;
                chk_HA_8L.Checked = chk_HA_8L.Enabled;
                chk_HA_9L.Checked = chk_HA_9L.Enabled;
                chk_HA_10L.Checked = chk_HA_10L.Enabled;
            }

            if (rbtn_HB.Checked)
            {
                chk_HB_1L.Checked = chk_HB_1L.Enabled;
                chk_HB_2L.Checked = chk_HB_2L.Enabled;
                chk_HB_3L.Checked = chk_HB_3L.Enabled;
                chk_HB_4L.Checked = chk_HB_4L.Enabled;
                chk_HB_5L.Checked = chk_HB_5L.Enabled;
                chk_HB_6L.Checked = chk_HB_6L.Enabled;
                chk_HB_7L.Checked = chk_HB_7L.Enabled;
                chk_HB_8L.Checked = chk_HB_8L.Enabled;
                chk_HB_9L.Checked = chk_HB_9L.Enabled;
                chk_HB_10L.Checked = chk_HB_10L.Enabled;
            }
        }

        public void Default_British_LoadData(DataGridView dgv_live_load)
        {

            List<string> list = new List<string>();
            List<string> lst_spc = new List<string>();
            dgv_live_load.Rows.Clear();

            string load = cmb_HB.Text;
            int i = 0;
            list.Clear();
            int typ_no = 1;

            double ll = MyList.StringToDouble(load.Replace("HB_", ""), 1.0);


            for (i = 6; i <= 26; i += 5)
            {
                list.Add(string.Format("TYPE {0}, {1}_{2}", typ_no++, load, i));
                list.Add(string.Format("AXLE LOAD IN TONS ,{0:f1}, {0:f1}, {0:f1}, {0:f1}", ll));
                list.Add(string.Format("AXLE SPACING IN METRES, 1.8,{0:f1},1.8", i));
                list.Add(string.Format("AXLE WIDTH IN METRES, 1.0"));
                list.Add(string.Format("IMPACT FACTOR, {0}", txt_LL_impf.Text));
                list.Add(string.Format(""));
            }

            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');

                //for (int j = 0; j < mlist.Count; j++)
                //{
                //    dgv_live_load[j, i].Value = mlist[j];
                //}

                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Default_British_Type_LoadData(DataGridView dgv_live_load)
        {
            List<string> lst_spcs = new List<string>();
            dgv_live_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_live_load.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            if (chk_HB_1L.Checked) lanes.Add(1);
            if (chk_HB_2L.Checked) lanes.Add(2);
            if (chk_HB_3L.Checked) lanes.Add(3);
            if (chk_HB_4L.Checked) lanes.Add(4);
            if (chk_HB_5L.Checked) lanes.Add(5);
            if (chk_HB_6L.Checked) lanes.Add(6);
            if (chk_HB_7L.Checked) lanes.Add(7);
            if (chk_HB_8L.Checked) lanes.Add(8);
            if (chk_HB_9L.Checked) lanes.Add(9);
            if (chk_HB_10L.Checked) lanes.Add(10);


            #region Long Girder
            list.Clear();


            double d, lane_width, impf, lf;
            int nos_lane;


            d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = (int)(d / lane_width);



            string load = "LOAD 1";
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 5; ld++)
            {
                load = "LOAD "  + ld;
                x = "X";
                z = "Z";

                for (i = 0; i < lanes.Count; i++)
                {
                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25);

                    load += ",TYPE " + ld;
                    x += ",-" + (1 + 5 * ld + 1.8 + 1.8).ToString();
                    z += "," + ((lanes[i] - 1) * lane_width + 0.25 + 1.0 + 1.0);
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }
            #endregion Load 1

            //list.Add(string.Format("LOAD 1,TYPE 1"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 2,TYPE 2"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 3,TYPE 3"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,5.9"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 4,TYPE 4"));
            //list.Add(string.Format("X,0"));
            //list.Add(string.Format("Z,1.5"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("LOAD 5,TYPE 5"));
            //list.Add(string.Format("X,0,0"));
            //list.Add(string.Format("Z,1.5,4.5"));
            //list.Add(string.Format(""));
            #endregion




            dgv_live_load.Columns.Clear();

            for (i = 0; i <= lanes.Count * 2; i++)
            {
                if (i == 0)
                {
                    dgv_live_load.Columns.Add("col_brts" + i, "Load Data");
                    dgv_live_load.Columns[i].Width = 70;
                    dgv_live_load.Columns[i].ReadOnly = true;
                }
                else
                {
                    dgv_live_load.Columns.Add("col_brts" + i, i.ToString());
                    dgv_live_load.Columns[i].Width = 50;
                }
            }
 

            for (i = 0; i < list.Count; i++)
            {
                dgv_live_load.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_live_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_live_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        private void cmb_HB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default_British_LoadData(dgv_long_british_loads);
            Default_British_Type_LoadData(dgv_british_loads);
        }

        private void chk_HA_1L_CheckedChanged(object sender, EventArgs e)
        {
            chk_HB_1L.Checked = (!chk_HA_1L.Checked && chk_HB_1L.Enabled);
            chk_HB_2L.Checked = !chk_HA_2L.Checked && chk_HB_2L.Enabled;
            chk_HB_3L.Checked = !chk_HA_3L.Checked && chk_HB_3L.Enabled;
            chk_HB_4L.Checked = !chk_HA_4L.Checked && chk_HB_4L.Enabled;
            chk_HB_5L.Checked = !chk_HA_5L.Checked && chk_HB_5L.Enabled;
            chk_HB_6L.Checked = !chk_HA_6L.Checked && chk_HB_6L.Enabled;
            chk_HB_7L.Checked = !chk_HA_7L.Checked && chk_HB_7L.Enabled;
            chk_HB_8L.Checked = !chk_HA_8L.Checked && chk_HB_8L.Enabled;
            chk_HB_9L.Checked = !chk_HA_9L.Checked && chk_HB_9L.Enabled;
            chk_HB_10L.Checked = !chk_HA_10L.Checked && chk_HB_10L.Enabled;
            Default_British_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;

        }

        private void chk_HB_1L_CheckedChanged(object sender, EventArgs e)
        {
            chk_HA_1L.Checked = !chk_HB_1L.Checked && chk_HB_1L.Enabled;
            chk_HA_2L.Checked = !chk_HB_2L.Checked && chk_HB_2L.Enabled;
            chk_HA_3L.Checked = !chk_HB_3L.Checked && chk_HB_3L.Enabled;
            chk_HA_4L.Checked = !chk_HB_4L.Checked && chk_HB_4L.Enabled;
            chk_HA_5L.Checked = !chk_HB_5L.Checked && chk_HB_5L.Enabled;
            chk_HA_6L.Checked = !chk_HB_6L.Checked && chk_HB_6L.Enabled;
            chk_HA_7L.Checked = !chk_HB_7L.Checked && chk_HB_7L.Enabled;
            chk_HA_8L.Checked = !chk_HB_8L.Checked && chk_HB_8L.Enabled;
            chk_HA_9L.Checked = !chk_HB_9L.Checked && chk_HB_9L.Enabled;
            chk_HA_10L.Checked = !chk_HB_10L.Checked && chk_HB_10L.Enabled;

            Default_British_Type_LoadData(dgv_british_loads);


            //if (!chk_HA_1L.Enabled) chk_HA_1L.Checked = false;
            //if (!chk_HA_2L.Enabled) chk_HA_2L.Checked = false;
            //if (!chk_HA_3L.Enabled) chk_HA_3L.Checked = false;
            //if (!chk_HA_4L.Enabled) chk_HA_4L.Checked = false;
            //if (!chk_HA_5L.Enabled) chk_HA_5L.Checked = false;
            //if (!chk_HA_6L.Enabled) chk_HA_6L.Checked = false;
            //if (!chk_HA_7L.Enabled) chk_HA_7L.Checked = false;
            //if (!chk_HA_8L.Enabled) chk_HA_8L.Checked = false;
            //if (!chk_HA_9L.Enabled) chk_HA_9L.Checked = false;
            //if (!chk_HA_10L.Enabled) chk_HA_10L.Checked = false;



            //if (!chk_HB_1L.Enabled) chk_HB_1L.Checked = false;
            //if (!chk_HB_2L.Enabled) chk_HB_2L.Checked = false;
            //if (!chk_HB_3L.Enabled) chk_HB_3L.Checked = false;
            //if (!chk_HB_4L.Enabled) chk_HB_4L.Checked = false;
            //if (!chk_HB_5L.Enabled) chk_HB_5L.Checked = false;
            //if (!chk_HB_6L.Enabled) chk_HB_6L.Checked = false;
            //if (!chk_HB_7L.Enabled) chk_HB_7L.Checked = false;
            //if (!chk_HB_8L.Enabled) chk_HB_8L.Checked = false;
            //if (!chk_HB_9L.Enabled) chk_HB_9L.Checked = false;
            //if (!chk_HB_10L.Enabled) chk_HB_10L.Checked = false;
        }

        private void chk_HA_3L_EnabledChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.Enabled) chk.Checked = false;
        }
        public void LONG_GIRDER_BRITISH_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();


            bool flag = false;
            for (i = 0; i < dgv_long_british_loads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_long_british_loads[c, i].Value.ToString();


                    //if (kStr != "" && kStr.StartsWith("TYPE"))
                    //{
                    //    long_ll_types.Add(kStr);
                    //}

                    if (flag)
                    {
                        long_ll_impact.Add(kStr);
                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
            long_ll.Add(string.Format(""));
            //long_ll.Add(string.Format("TYPE 6 40RWHEEL"));
            //long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            //long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            //long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            load_list_1 = new List<string>();
            load_list_2 = new List<string>();
            load_list_3 = new List<string>();
            load_list_4 = new List<string>();
            load_list_5 = new List<string>();
            load_list_6 = new List<string>();
            load_total_7 = new List<string>();



            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            for (i = 0; i < dgv_british_loads.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv_british_loads[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                    if (count == 1)
                    {
                        load_list_1.Clear();
                        load_list_1.AddRange(list.ToArray());
                    }
                    else if (count == 2)
                    {

                        load_list_2.Clear();
                        load_list_2.AddRange(list.ToArray());
                    }
                    else if (count == 3)
                    {
                        load_list_3.Clear();
                        load_list_3.AddRange(list.ToArray());
                    }
                    else if (count == 4)
                    {

                        load_list_4.Clear();
                        load_list_4.AddRange(list.ToArray());
                    }
                    else if (count == 5)
                    {

                        load_list_5.Clear();
                        load_list_5.AddRange(list.ToArray());
                    }
                    else if (count == 6)
                    {

                        load_list_6.Clear();
                        load_list_6.AddRange(list.ToArray());

                    }
                    else if (count == 7)
                    {
                        load_total_7.Clear();
                        load_total_7.AddRange(list.ToArray());
                    }
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv_british_loads.ColumnCount; c++)
                {
                    kStr = dgv_british_loads[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
                //def_load.Add(txt);
            }

            fl = 3;

            //Long_Girder_Analysis.LoadList_1 = 
        }

        public List<int> HA_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HA_1L.Checked) lanes.Add(1);
                if (chk_HA_2L.Checked) lanes.Add(2);
                if (chk_HA_3L.Checked) lanes.Add(3);
                if (chk_HA_4L.Checked) lanes.Add(4);
                if (chk_HA_5L.Checked) lanes.Add(5);
                if (chk_HA_6L.Checked) lanes.Add(6);
                if (chk_HA_7L.Checked) lanes.Add(7);
                if (chk_HA_8L.Checked) lanes.Add(8);
                if (chk_HA_9L.Checked) lanes.Add(9);
                if (chk_HA_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }
        public List<int> HB_Lanes
        {
            get
            {
                List<int> lanes = new List<int>();

                if (chk_HB_1L.Checked) lanes.Add(1);
                if (chk_HB_2L.Checked) lanes.Add(2);
                if (chk_HB_3L.Checked) lanes.Add(3);
                if (chk_HB_4L.Checked) lanes.Add(4);
                if (chk_HB_5L.Checked) lanes.Add(5);
                if (chk_HB_6L.Checked) lanes.Add(6);
                if (chk_HB_7L.Checked) lanes.Add(7);
                if (chk_HB_8L.Checked) lanes.Add(8);
                if (chk_HB_9L.Checked) lanes.Add(9);
                if (chk_HB_10L.Checked) lanes.Add(10);

                return lanes;
            }
        }

        #endregion British Standard Loading

        private void btn_deck_design_BS_Click(object sender, EventArgs e)
        {
            //uC_Deckslab1.iApp = iApp;
            //uC_Deckslab1.YD = "1";
            frm_Deckslab_LS fm = new frm_Deckslab_LS(iApp);
            fm.ShowDialog();
        }
    }
  
    
    public class PSC_I_Girder_LS_Extend : PSC_I_Girder_Analysis_LS
    {
        public MidSection Long_inner_mid;
        public MidSection Long_outer_mid;
        public SupportSection Long_inner_sup;
        public SupportSection Long_outer_sup;
        public EndSection End_Cross_Girder;
        public EndSection Inter_Cross_Girder;

        public List<int> HA_Lanes;

        public string HA_Loading_Members;

        IApplication iApp;
        public PSC_I_Girder_LS_Extend(IApplication app):base(app)
        {
            iApp = app;
            HA_Lanes = new List<int>();
        }
        public override void Write_Composite_Section_Properties(List<string> list)
        {

            list.Add(string.Format("{0} PRIS AX {1:f4} IZ {2:f4}",
                Cross_Girders_as_String,
                End_Cross_Girder.Composite_Area_Sum,
                End_Cross_Girder.IC));
            //Steel_Section.Section_Cross_Girder.Ixx_in_Sq_Sq_m,
            //Steel_Section.Section_Cross_Girder.IC));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                L2_Girders_as_String,
                Long_inner_mid.Composite_Area_Sum,
                Long_inner_mid.IC));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                            L4_Girders_as_String,
                Steel_Section.Section_Long_Girder_at_L4_Span.Area_in_Sq_m,
                Steel_Section.Section_Long_Girder_at_L4_Span.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Long_Girder_at_L4_Span.Izz_in_Sq_Sq_m));


            list.Add(string.Format("{0}  PRIS AX {1:f4} IX {2:f4} IZ {3:f4}",
                           Deff_Girders_as_String,
                Steel_Section.Section_Long_Girder_at_End_Span.Area_in_Sq_m,
                Steel_Section.Section_Long_Girder_at_End_Span.Ixx_in_Sq_Sq_m,
                Steel_Section.Section_Long_Girder_at_End_Span.Izz_in_Sq_Sq_m));

        }

        public void CreateData_British()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));

            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;
            //Joints_Array = new JointNode[Total_Rows, Total_Columns];
            //Long_Girder_Members_Array = new Member[Total_Rows, Total_Columns - 1];
            //Cross_Girder_Members_Array = new Member[Total_Rows - 1, Total_Columns];


            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            //double val1 = 12.1;
            double val1 = WidthBridge;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            //Store Joint Coordinates
            double L_2, L_4, eff_d;
            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data

            //Effective_Depth = Lvp;
            Effective_Depth = Lsp;

            list_x.Clear();
            list_x.Add(0.0);

            list_x.Add(og);

            list_x.Add(Lsp);


            //last_x = (Lsp + Lvp);
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            //last_x = 3 * Length / 16.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            //last_x = 5 * Length / 16.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = 3 * Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            //last_x = 7 * Length / 16.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            int i = 0;
            for (i = list_x.Count - 2; i >= 0; i--)
            {
                last_x = Length - list_x[i];
                list_x.Add(last_x);
            }
            MyList.Array_Format_With(ref list_x, "F3");
            last_x = x_incr + Effective_Depth;

            bool flag = true;
            do
            {
                flag = false;
                for (i = 0; i < list_x.Count; i++)
                {
                    if (last_x.ToString("0.00") == list_x[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;
                last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);

            }
            while (last_x <= Length);
            list_x.Sort();


            list_z.Clear();
            list_z.Add(0);

            if (Wc != 0.0)
            {
                last_z = Wc;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);

                last_z = WidthBridge - Wc;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }
            else if (Wf_left != 0.0 && Wf_right != 0.0)
            {
                last_z = Wf_left;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);


                last_z = WidthBridge - Wf_right;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }
            else if (Wf_left != 0.0 && Wf_right == 0.0)
            {
                last_z = Wf_left;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);


                last_z = WidthBridge - Wk_right;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }
            else if (Wf_left == 0.0 && Wf_right != 0.0)
            {
                last_z = Wk_left;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);


                last_z = WidthBridge - Wf_right;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }

            if (Width_LeftCantilever != 0.0)
            {
                last_z = Width_LeftCantilever;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }

            if (Width_RightCantilever != 0.0)
            {
                last_z = WidthBridge - Width_RightCantilever;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
                list_z.Add(last_z);
            }

            last_z = WidthBridge;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = Width_LeftCantilever + z_incr;
            do
            {
                flag = false;
                for (i = 0; i < list_z.Count; i++)
                {
                    if (last_z.ToString("0.00") == list_z[i].ToString("0.00"))
                    {
                        flag = true; break;
                    }
                }

                if (!flag && last_z > Width_LeftCantilever && last_z < (WidthBridge - Width_RightCantilever - 0.2))
                    list_z.Add(last_z);
                last_z += z_incr;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            } while (last_z <= WidthBridge);




            //HA_Lanes.Add(1);
            //HA_Lanes.Add(2);
            //HA_Lanes.Add(3);


            List<double> HA_distances = new List<double>();
            if(HA_Lanes.Count > 0)
            {
                double ha = 0.0;

                for (i = 0; i < HA_Lanes.Count; i++)
                {
                    ha = 1.75 + (HA_Lanes[i] - 1) * 3.5;
                    if (!list_z.Contains(ha))
                    {
                        list_z.Add(ha);
                        HA_distances.Add(ha);
                    }
                }
            }

            list_z.Sort();

            #endregion Chiranjit [2011 09 23] Correct Create Data



            _Columns = list_x.Count;
            _Rows = list_z.Count;

            //int i = 0;

            List<double> list = new List<double>();

            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list = new List<double>();
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    list.Add(list_x[iCols] + list_z[iRows] * skew_length);
                }
                z_table.Add(list_z[iRows], list);
            }

            Joints_Array = new JointNode[_Rows, _Columns];
            Long_Girder_Members_Array = new Member[_Rows, _Columns - 1];
            Cross_Girder_Members_Array = new Member[_Rows - 1, _Columns];



            for (iRows = 0; iRows < _Rows; iRows++)
            {
                list_x = z_table[list_z[iRows]] as List<double>;
                for (iCols = 0; iCols < _Columns; iCols++)
                {
                    nd = new JointNode();
                    nd.Y = 0;
                    nd.Z = list_z[iRows];

                    //nd.X = list_x[iCols] + (skew_length * list_z[iRows]);
                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }
            int nodeNo = 0;
            Joints.Clear();

            //support_left_joints = Joints_Array[0, 0].NodeNo + " TO " + Joints_Array[0, iCols - 1].NodeNo;
            //support_right_joints = Joints_Array[iRows - 1, 0].NodeNo + " TO " + Joints_Array[iRows - 1, iCols - 1].NodeNo;

            support_left_joints = "";
            support_right_joints = "";

            joints_list_for_load.Clear();
            List<int> list_nodes = new List<int>();

            //string str_joints = "";

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                    if (iCols == 1 && iRows > 1 && iRows < _Rows - 2)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 2 && iRows > 1 && iRows < _Rows - 2)
                        support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else
                    {
                        if (iRows > 0 && iRows < _Rows - 1)
                            list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                    }
                }
                if (list_nodes.Count > 0)
                {
                    joints_list_for_load.Add(MyList.Get_Array_Text(list_nodes));
                    list_nodes.Clear();
                }
            }

            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
            #region Chiranjit [2013 05 30]
            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 1; iRows < _Rows; iRows++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows - 1, iCols];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Cross_Girder_Members_Array[iRows - 1, iCols] = mem;
                }
            }
            for (iRows = 0; iRows < _Rows; iRows++)
            {
                for (iCols = 1; iCols < _Columns; iCols++)
                {
                    mem = new Member();
                    mem.StartNode = Joints_Array[iRows, iCols - 1];
                    mem.EndNode = Joints_Array[iRows, iCols];
                    mem.MemberNo = MemColls.Count + 1;
                    MemColls.Add(mem);
                    Long_Girder_Members_Array[iRows, iCols - 1] = mem;

                }
            }
            #endregion Chiranjit [2013 05 30]

            #region Chiranjit [2013 06 06]

            if (Width_LeftCantilever > 0)
            {
                List_Envelop_Outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                List_Envelop_Inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
            }
            else
            {
                List_Envelop_Outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                List_Envelop_Inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]
        }


        public override void WriteData(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            list.Add("SECTION PROPERTIES");
            list.Add("153 TO 158 173 TO 178 PRIS AX 1.146 IX 0.022 IZ 0.187");
            list.Add("151 160 171 180 PRIS AX 1.1037 IX 0.067 IZ 0.167");
            list.Add("152 159 172 179 PRIS AX 0.7001 IX 0.0442 IZ 0.105");
            list.Add("133 TO 138 193 TO 198 PRIS AX 1.215 IX 0.023 IZ 0.192");
            list.Add("131 140 191 200 PRIS AX 1.2407 IX 0.0698 IZ 0.181");
            list.Add("132 139 192 199 PRIS AX 0.7897 IX 0.0461 IZ 0.115");
            list.Add("11 TO 20 91 TO 100 111 TO 130 141 TO 150 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            list.Add("161 TO 170 181 TO 190 201 TO 220 PRIS AX 0.001 IX 0.0001 IZ 0.0001");
            list.Add("1 TO 10 101 TO 110 PRIS AX 0.339 IX 0.007 IZ 0.242");
            list.Add("51 TO 60 PRIS AX 0.385 IX 0.008 IZ 0.277");
            list.Add("41 TO 50 61 TO 70 PRIS AX 0.523 IX 0.010 IZ 0.003");
            list.Add("31 TO 40 71 TO 80 PRIS AX 0.406 IX 0.008 IZ 0.002");
            list.Add("21 TO 30 81 TO 90 PRIS AX 0.482 IX 0.009 IZ 0.003");
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add("3 5 7 9  PINNED");
            list.Add("113 115 117 119  PINNED");
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("LOAD GENERATION 191");
            //list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);

        }


        public override void WriteData_Total_Analysis(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            List<int> DeckSlab = new List<int>();

            List<int> Inner_Girder_Mid = new List<int>();
            List<int> Inner_Girder_Support = new List<int>();

            List<int> Outer_Girder_Mid = new List<int>();
            List<int> Outer_Girder_Support = new List<int>();

            List<int> Cross_Girder_Inter = new List<int>();
            List<int> Cross_Girder_End = new List<int>();


            List<int> HA_Members = new List<int>();

            List<double> HA_Dists = new List<double>();
            HA_Dists = new List<double>();
            if (HA_Lanes != null)
            {
                for (i = 0; i < HA_Lanes.Count; i++)
                {
                    HA_Dists.Add(1.75 + (HA_Lanes[i] - 1) * 3.5);
                }
            }

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            int index = 2;

            for (int c = 0; c < _Rows; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (i <= 1 || i >= (_Columns - 3))
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Support.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {
                            var item = Long_Girder_Members_Array[c, i];
                         
                            if (HA_Dists.Contains(item.EndNode.Z) && HA_Dists.Contains(item.StartNode.Z))
                                HA_Members.Add(item.MemberNo);
                            else
                                Inner_Girder_Support.Add(item.MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                    {
                        if (c == index || c == _Rows - index - 1)
                        {
                            Outer_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else if (c > index && c < _Rows - index - 1)
                        {

                            var item = Long_Girder_Members_Array[c, i];

                            if (HA_Dists.Contains(item.EndNode.Z) && HA_Dists.Contains(item.StartNode.Z))
                                HA_Members.Add(item.MemberNo);
                            else
                                Inner_Girder_Mid.Add(item.MemberNo);


                            //Inner_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
                        }
                        else
                            DeckSlab.Add(Long_Girder_Members_Array[c, i].MemberNo);
                    }
                }
            }

            Outer_Girder_Mid.Sort();
            Outer_Girder_Support.Sort();


            Inner_Girder_Mid.Sort();
            Inner_Girder_Support.Sort();
            DeckSlab.Sort();
            index = 2;
            List<int> lst_index = new List<int>();
            for (int n = 1; n <= NCG - 2; n++)
            {
                for (i = 0; i < _Columns; i++)
                {
                    if (Cross_Girder_Members_Array[0, i].StartNode.X.ToString("0.00") == (Spacing_Cross_Girder * n + Effective_Depth).ToString("0.00"))
                    {
                        index = i;
                        lst_index.Add(i);
                    }
                }
            }
            for (int c = 0; c < _Rows - 1; c++)
            {
                for (i = 0; i < _Columns - 1; i++)
                {
                    if (lst_index.Contains(i))
                        Cross_Girder_Inter.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    else if (i == 1 || i == _Columns - 2)
                    {
                        Cross_Girder_End.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                    }
                    else
                        DeckSlab.Add(Cross_Girder_Members_Array[c, i].MemberNo);
                }
            }

            DeckSlab.Sort();
            Cross_Girder_Inter.Sort();
            Cross_Girder_End.Sort();


            //_Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder);
            //_Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder);
            //_Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder);




            //string _DeckSlab = "";
            //string _Inner_Girder_Mid = "";
            //string _Inner_Girder_Support = "";
            //string _Outer_Girder_Mid = "";
            //string _Outer_Girder_Support = "";
            //string _Cross_Girder_Inter = "";
            //string _Cross_Girder_End = "";




            _DeckSlab = MyList.Get_Array_Text(DeckSlab);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder_Mid);
            _Inner_Girder_Support = MyList.Get_Array_Text(Inner_Girder_Support);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder_Mid);
            _Outer_Girder_Support = MyList.Get_Array_Text(Outer_Girder_Support);
            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder_Inter);
            _Cross_Girder_End = MyList.Get_Array_Text(Cross_Girder_End);



            HA_Loading_Members = MyList.Get_Array_Text(HA_Members);

            list.Add("SECTION PROPERTIES");
            if (Long_inner_mid != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANTS");
            //list.Add("E 2.85E6 ALL");
            list.Add("E " + Ecm * 100 + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");

            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));

            
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**dEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }

        private void Set_Section_Properties(List<string> list)
        {

            if (Inter_Cross_Girder == null)
            {
                list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
                    _DeckSlab,
                    Ds));
            }
            else
            {
                list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
                    _DeckSlab,
                    Ds));

                if (_Cross_Girder_Inter != "")
                    list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {2:f4}",
                    _Cross_Girder_Inter,
                    Inter_Cross_Girder.Composite_Area_Sum,
                    Inter_Cross_Girder.IC));
            }


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {2:f4}",
                _Cross_Girder_End,
                End_Cross_Girder.Composite_Area_Sum,
                End_Cross_Girder.IC));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {2:f4}",
                _Outer_Girder_Support,
                Long_outer_sup.Composite_Area_Sum,
                Long_outer_sup.IC));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {2:f4}",
                _Outer_Girder_Mid,
                Long_outer_mid.Composite_Area_Sum,
                Long_outer_mid.IC));


            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {2:f4}",
                _Inner_Girder_Support,
                Long_inner_sup.Composite_Area_Sum,
                Long_inner_sup.IC));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {2:f4}",
                _Inner_Girder_Mid,
                Long_inner_mid.Composite_Area_Sum,
                Long_inner_mid.IC));


            if (HA_Lanes.Count > 0)
            {
                list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
                    HA_Loading_Members,
                    Ds));
            }
        }
        public override void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }


            list.Add("SECTION PROPERTIES");
            if (Long_inner_mid != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));
            }
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            list.Add("E " + Ecm * 100 + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));

            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("1 TO 220 UNI GY -0.0001");

            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);

            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            string fn = Path.GetDirectoryName(file_name);
            fn = Path.Combine(fn, "LL.TXT");
            File.WriteAllLines(fn, ll_data.ToArray());

            list.Clear();
        }

        public override void WriteData_DeadLoad_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR PSC I GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
            list.Add("UNIT METER MTON");
            list.Add("JOINT COORDINATES");
            for (i = 0; i < Joints.Count; i++)
            {
                list.Add(Joints[i].ToString());
            }
            list.Add("MEMBER INCIDENCES");
            for (i = 0; i < MemColls.Count; i++)
            {
                list.Add(MemColls[i].ToString());
            }

            list.Add("SECTION PROPERTIES");
            if (Long_inner_mid != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            //list.Add("E 2.85E6 ALL");
            list.Add("E " + Ecm * 100 + " ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));



            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD + SIDL");
            list.Add("**DEAD lOAD");
            list.Add("MEMBER LOAD");
            list.Add("153 TO 158 173 TO 178 UNI GY -2.7504");
            list.Add("151 160 171 180 UNI GY -2.66888");
            list.Add("152 159 172 179 UNI GY -1.68024");
            list.Add("133 TO 138 193 TO 198 UNI GY -2.916");
            list.Add("131 140 191 200 UNI GY -2.97768");
            list.Add("132 139 192 199 UNI GY -1.89528");
            list.Add("1 TO 10 101 TO 110 UNI GY -0.702");
            list.Add("** SIDL");
            list.Add("MEMBER LOAD");
            list.Add("** WEARING COAT");
            list.Add("131 TO 140 191 TO 200 UNI GY -0.68");
            list.Add("151 TO 160 171 TO 180 UNI GY -0.53");
            list.Add("**CRASH BARRIER");
            list.Add("111 TO 120 211 TO 220 UNI GY -1.0");
            list.Add("**** OUTER GIRDER *********");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //iApp.LiveLoads.Impact_Factor(ref list, iApp.DesignStandard);
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 A70R 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 A70RR 1.188");
            //list.Add("TYPE 7 A70RR 1.188");
            //list.Add("TYPE 8 A70RR 1.188");
            //list.Add("TYPE 9 A70RR 1.188");
            //list.Add("TYPE 10 A70RR 1.188");
            //list.Add("TYPE 11 A70RR 1.188");
            //list.Add("TYPE 12 A70RR 1.188");
            //list.Add("TYPE 13 A70RR 1.188");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("LOAD GENERATION 191");
            //list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            //list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
            //list.Add("**** 3 LANE CLASS A *****");
            //list.Add("*LOAD GENERATION 160");
            //list.Add("*TYPE 1 -18.8 0 2.125 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 5.625 XINC 0.2");
            //list.Add("*TYPE 1 -18.8 0 9.125 XINC 0.2");
            //list.Add("*PLOT DISPLACEMENT FILE");
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + List_Envelop_Inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }


    }

}
