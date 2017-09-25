using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.Abutment;


namespace BridgeAnalysisDesign.Footing
{
    public partial class frm_CombinedFooting : Form
    {
        IApplication iApp;

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF COMBINED FOOTING [BS]";
                //if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                //    return "EXTRADOSSED CABLE STAYED BRIDGE [LRFD]";
                return "DESIGN OF COMBINED FOOTING";
            }
        }

        public string Drawing_Folder
        {
            get
            {
                return Path.Combine(user_path, "DRAWINGS");
            }
        }
        public frm_CombinedFooting(IApplication iapp)
        {

            InitializeComponent();

            iApp = iapp;

            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

        }
        public void Calculate_Program1()
        {
            List<string> list = new List<string>();




            double l1, b1, l2, b2, P1, P2, d1, d2, p, fck, fy, L, B, d, bardia, cc;

            double sigma_c, sigma_st, m;


            l1 = MyList.StringToDouble(txt_l1.Text, 0.0);
            b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            l2 = MyList.StringToDouble(txt_l2.Text, 0.0);
            b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            P1 = MyList.StringToDouble(txt_P1.Text, 0.0);
            P2 = MyList.StringToDouble(txt_P2.Text, 0.0);
            d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
            p = MyList.StringToDouble(txt_p.Text, 0.0);
            fck = MyList.StringToDouble(cmb_fck.Text, 0.0);
            fy = MyList.StringToDouble(cmb_fy.Text, 0.0);
            L = MyList.StringToDouble(txt_L.Text, 0.0);
            B = MyList.StringToDouble(txt_B.Text, 0.0);
            d = MyList.StringToDouble(txt_d.Text, 0.0);
            bardia = MyList.StringToDouble(txt_bardia.Text, 0.0);
            cc = MyList.StringToDouble(txt_cc.Text, 0.0);
            sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);
            sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
            m = MyList.StringToDouble(txt_m.Text, 0.0);



            //Allowable Flexural Stress in Concrete [σ_c] 



            #region Program
            list.Add(string.Format("Design of Combined Footing"));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Data:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Size of Column1 (Left Column) = l1 x b1 = 400 x 400 Sq.mm"));
            list.Add(string.Format("Size of Column2 (Right Column) = l2 x b2 = 600 x 600 Sq.mm"));
            list.Add(string.Format("Load on Column1 = P1 = 750 kN"));
            list.Add(string.Format("Load on Column2 = P2 = 1500 kN"));
            list.Add(string.Format("Distance from Left side property line to left face of column1 = d1 = 0.27 m."));
            list.Add(string.Format("Distance between centre of column1 to centre of column2 = d2 = 5.0 m. "));
            list.Add(string.Format("Safe Bearing Capacity of soil = p = 150 kN/Sq.m"));
            list.Add(string.Format("Concrete Grade = fck = 20 N/Sq.mm"));

            list.Add(string.Format("Allowable Flexural Stress in Concrete  = σ_c = 20 N/Sq.mm"));


            list.Add(string.Format("Steel Grade = fy = 415 N/Sq.mm"));
            list.Add(string.Format("Permissible Stress in Steel = σ_st = 415 N/Sq.mm"));
            
            list.Add(string.Format("Proposed Length of footing = L = 7.6 m."));
            list.Add(string.Format("Proposed Width of footing = B =2.2 m."));
            list.Add(string.Format("Proposed Effective depth of footing = d = 660 mm."));
            list.Add(string.Format("Proposed Bar Diameter = bardia = 20mm."));
            list.Add(string.Format("Clear Cover = cc = 30 mm."));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Calculations:"));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1: DETERMINE REQUIRED SIZE OF COMBINED FOOTING:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Let, the Left side property line coincides with the left edge of footing,"));
            list.Add(string.Format("Let, the distance from P to the left = X1,"));
            list.Add(string.Format("Distance from left edge to centre of left column1 = d3 = (l1/2) + d1 = (0.40/2) + 0.27 = 0.47 m."));
            list.Add(string.Format("Distance from left edge to centre of right column2 = d4 = (l1/2) + d1 + d2 = (0.40/2) + 0.27 + 5.0 = 5.47 m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Taking moment about the left edge,"));
            list.Add(string.Format("P x X1         = P1 x d3 + P2 x d4"));
            list.Add(string.Format("X1         = (P1 x d3 + P2 x d4) / P"));
            list.Add(string.Format("= (750 x 0.47 + 1500 x 5.47) / 2250"));
            list.Add(string.Format("= 3.803 m. "));
            list.Add(string.Format("= Distance of Centre of Gravity of applied total load from left edge"));
            list.Add(string.Format("Length of footing = 2 x X1 = 2 x 3.803 = 7.606. "));
            list.Add(string.Format("Adopt User’s proposed length of footing = L = 7.6 m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Distance of centre of left column load (column1) from CG of  total load         = X2 "));
            list.Add(string.Format("= X1 - d3 "));
            list.Add(string.Format("= 3.803 - 0.47 = 3.33 m."));
            list.Add(string.Format("Distance of centre of right column load (column2) from CG of total load  = X3 "));
            list.Add(string.Format("= d2 - X2"));
            list.Add(string.Format("= 5.0 - 3.33 = 1.67 m."));
            list.Add(string.Format("Distance from right edge to centre of right column2 = d5 = L - d4 = 7.6 - 5.47 = 2.13 m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Total applied load = P = P1+P2 = 750 + 1500 = 2250 kN"));
            list.Add(string.Format("Assumed self weight of Footing 10% = 225 kN"));
            list.Add(string.Format("Total load = W = 2250+225 = 2475 kN"));
            list.Add(string.Format("Required width of footing = W/(L x p) = 2475/(7.6 x 150) = 2.17 m. "));
            list.Add(string.Format("Adopt User’s proposed width of footing = B = 2.20 m."));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2: CALCULATE LONGITUDINAL BENDING MOMENT AND SHEAR FORCE"));
            list.Add(string.Format("Factored Load on column1 = P3 = 1.5 x P1 = 1.5 x 750 = 1125 kN."));
            list.Add(string.Format("Factored Load on column2 = P4 = 1.5 x P2 = 1.5 x 1500 = 2250 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Footing size                          = L x B = 7.60 x 2.20 m."));
            list.Add(string.Format("Not soil pressure                 = P / (L x B) =  (1125+2250) / (7.60 x 2.20) = 201.854 kN/Sq.m."));
            list.Add(string.Format("Pressure per metre length         = p2 = 201.854 x 2.20 = 444.0788 kN/m."));
            list.Add(string.Format("B.M. and S.F. in the longitudinal direction are shown in the Figure."));
            list.Add(string.Format("===================================================================================="));
            list.Add(string.Format(""));
            list.Add(string.Format("===================================================================================="));
            list.Add(string.Format("Some calculations are given below:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Minimum Shear Force at load P1 at the centre of column1 = V1 = -p2 x d3 = -444.0788 x 0.47 = -208.717 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Shear Force at load P1 at the centre of column1 = V2 = V1 + P3 = -208.717 + 1125 = 916.283 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Minimum Shear Force at load P2 at the centre of column2 = V3 = p2 x d5 = 444.0788 x 2.13 = 945.888 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Shear Force at load P2 at the centre of column2 = V4 = V3 - P4 = 945.888 - 2250 = -1304.112 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Let, in the central portion S.F. is zero at ‘dx’ distance from load P1,"));
            list.Add(string.Format(""));
            list.Add(string.Format("We can write the equation, by considering two similar triangles, from the shear force diagram,        "));
            list.Add(string.Format("                             V2 / dx = (0.0-V4) / (d2-dx)"));
            list.Add(string.Format("i.e,    916.283 / dx = 1304.112 / (5-dx)"));
            list.Add(string.Format("i.e,   916.283 x  5 - 916.283 x  dx = 1304.112 x dx"));
            list.Add(string.Format("i.e,   dx = (916.283  x 5) / (916.283 + 1304.112) = 4581.415 / 2220.395 = 2.063 m"));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment at distance ‘dx’ computed from left side"));
            list.Add(string.Format("                             BM1 = [p2 x (d3 + dx) x (d3 + dx) / 2] - P3 x dx"));
            list.Add(string.Format("                                      = [444.0788 x (0.47 + 2.063) x (0.47 + 2.063) / 2] -  1125 x 2.063"));
            list.Add(string.Format("                                      = 1424.625 - 2320.875"));
            list.Add(string.Format("         = -896.250 kN-m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment at distance ‘dx’ computed from right side"));
            list.Add(string.Format("                             BM2 = [p2 x (d5 + d2-dx) x (d5 + d2-dx) / 2] - P4 x dx"));
            list.Add(string.Format("                                      = [444.0788 x (2.13 + 4.0 - 2.063) x (2.13 + 4.0 - 2.063) / 2] -  2250 x (4.0 - 2.063)"));
            list.Add(string.Format("                                      = 3672.640 - 4358.250"));
            list.Add(string.Format("         = -685.610 kN-m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Design Bending Moment = BM = Larger of values for BM1 and BM2 = 896.250 kN-m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Thickness of combined footing = d = √ [BM x 1000000 / (0.138 x fck x B x 1000)]"));
            list.Add(string.Format("                                                              = √ [896.250 x 1000000 / (0.138 x 20 x 2.2 x 1000)]"));
            list.Add(string.Format("                                                              = 384.192 mm."));
            list.Add(string.Format(""));
            list.Add(string.Format("Adopt User’s proposed effective depth of footing = d = 660 mm."));
            list.Add(string.Format("Over thickness of footing = D = d + cc + bardia /2 = 660 + 30 + (20/2) = 700 mm."));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3: MAIN LONGITUDINAL REINFORCEMENTS FOR NEGATIVE BENDING MOMENTS"));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever arm = j         = 0.5 + √ 0.25 - (BM x 1000000) / (fck x 0.87 x d2 x B x 1000)"));
            list.Add(string.Format("                = 0.5 + √ 0.25 - (896.250 x 1000000) / (20 x 0.87 x 660 x 660 x 2.2 x 1000)"));
            list.Add(string.Format("                = 0.5 + 0.443"));
            list.Add(string.Format("                = 0.943 "));
            list.Add(string.Format(""));
            list.Add(string.Format("Ast         = BM x 1000000 / (0.87 x j x fy x d)"));
            list.Add(string.Format("        = 896.250 x 1000000 / (0.87 x 0.943 x 415 x 660)"));
            list.Add(string.Format("        = 3988.469 Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of a 20mm. dia bar = ast1 = π x bardia2 / 4 = 3.1416 x 20 x 20 / 4 =  314.16 Sq.mm"));
            list.Add(string.Format("Spacing of bars  = s = 1000 x ast1 / Ast =  1000 x 314.16 / 3988.469 = 78.767 mm."));
            list.Add(string.Format("Provide Ast1         = 20 mm. dia bars @ 75 mm c/c spacing. "));
            list.Add(string.Format("                                = ast1 x 1000 / s "));
            list.Add(string.Format("                = 314.16 x 1000 / 75"));
            list.Add(string.Format("= 4188.8 Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4: SHEAR ONE WAY ACTION UNDER HIGHER COLUMN LOAD"));
            list.Add(string.Format(""));
            list.Add(string.Format("Size of column,  l = l2 = 0.6 m.,   b = b2 = 0.6 m."));
            list.Add(string.Format("Shear Force at the face of higher column load, V = V4 = -1304.112 kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear force at distance ‘d’ from face of column         = Vu         = V - p2 x [(l/2) + d] "));
            list.Add(string.Format("= 1304.112 - 444.0788 x [(0.6/2) + 0.66]"));
            list.Add(string.Format("                                                        = 1304.112 - 426.316"));
            list.Add(string.Format("                                                        = 877.796 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Nominal Shear Stress = τv = Vu x 1000 / (b x d) = 877.796 x 1000 / (2200 x 660) = 0.605 N/Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Percentage of tension steel  provided = p = Ast1 x 100 / (B x d) = 4188.8 x 100 / (2200 x 660) = 0.29%"));
            list.Add(string.Format("Permissible Shear Stress for M 20 Grade Concrete and 0.29% of steel  = τc = 0.23 N/Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("If (τc > τv ) ‘Hence OK’, "));
            list.Add(string.Format("Else ‘Hence NOT OK, Size or Thickness may be increased’."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5: SHEAR TWO WAY ACTION UNDER HIGHER COLUMN LOAD"));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear strength of concrete = τc’ = ks x τc"));
            list.Add(string.Format("Where, ks = 0.5 + βc   and βc = 1,  "));
            list.Add(string.Format("ks = 1.5 > 1.0 (maximum value), therefore ks = 1.0,"));
            list.Add(string.Format(""));
            list.Add(string.Format("τc = 0.25 x √ fck = 0.25 x √20 = 1.118 N/Sq.mm = τc’,"));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored Load on column2 = P4 = 1.5 x P2 = 1.5 x 1500 = 2250 kN."));
            list.Add(string.Format("Pressure per metre length         = p2 = 201.854 x 2.20 = 444.0788 kN/m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear force                 = Vu         = P4 - [π x (0.45 + d/2/1000)2 / 4 x p2]"));
            list.Add(string.Format("= 2250 - [3.1416 x (0.45 + 660/2/1000)2 / 4 x 444.0788]"));
            list.Add(string.Format("= 2250 - 81.23"));
            list.Add(string.Format("= 2168.77 kN"));
            list.Add(string.Format(""));
            list.Add(string.Format("Nominal Shear Stress         = τv         = Vu x 1000 / (bo x d) "));
            list.Add(string.Format("= Vu x 1000 / [π x (0.45 + d/2/1000) x 1000 x d]"));
            list.Add(string.Format("= 2168.77 x 1000 / [3.1416 x (0.45 +660/2/1000) x 1000 x 660]"));
            list.Add(string.Format("= 2168.77 x 1000 / 1617295.68"));
            list.Add(string.Format("= 1.3409 N/Sq.mm > τc’"));
            list.Add(string.Format("If (τc ‘ > τv ) ‘Hence OK’, "));
            list.Add(string.Format("Else ‘Hence NOT OK, size or thickness of Footing may be increased’."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 6: TRANSVERSE REINFORCEMENT"));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored Load on column1 = P3 = 1.5 x P1 = 1.5 x 750 = 1125 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored upward pressure under column1 = pt1 = P3 / B = 1125 / 2.2 = 511.364 kN/m"));
            list.Add(string.Format("Distance of face of column1 from Longer Edge of footing = df1 = (B - b1) / 2 = (2.2-0.4)/2 = 0.9 m."));
            list.Add(string.Format("Transverse BM at the face of column1 = pt1 x df1 x df1 / 2 = 511.364 x 0.9 x 0.9 / 2 = 207.1 kN-m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever arm = j = 0.5 + √ 0.25 - (BM x 1000000) / (fck x 0.87 x d2 x B x 1000)"));
            list.Add(string.Format("              = 0.5 + √ 0.25 - (207.1 x 1000000) / (20 x 0.87 x 660 x 660 x 2.2 x 1000)"));
            list.Add(string.Format("              = 0.5 + 0.487"));
            list.Add(string.Format("              = 0.987"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ast = 207.1 x 1000000 / (0.87 x j x fy x d)"));
            list.Add(string.Format("    = 207.1 x 1000000 / (0.87 x 0.987 x 415 x 660)"));
            list.Add(string.Format("    = 880.545 Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of a 20mm. dia bar = ast1 = π x bardia2 / 4 = 3.1416 x 20 x 20 / 4 =  314.16 Sq.mm"));
            list.Add(string.Format("Spacing of bars  = s = 1000 x ast1 / Ast =  1000 x 314.16 / 880.545 = 356.779 mm."));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide  At1 = 20 mm. dia bars @ 300 mm c/c spacing. "));
            list.Add(string.Format("             = ast1 x 1000 / s "));
            list.Add(string.Format("             = 314.16 x 1000 / 300"));
            list.Add(string.Format("             = 1047.2 Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored Load on column2 = P4 = 1.5 x P2 = 1.5 x 1500 = 2250 kN."));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored upward pressure under column2 = pt2 = P4 / B = 2250 / 2.2 = 1022.73 kN/m"));
            list.Add(string.Format("Distance of face of column2 from Longer Edge of footing = df2 = (B - b2) / 2 = (2.2-0.6)/2 = 0.8 m."));
            list.Add(string.Format("Transverse BM at the face of column2 = pt2 x df2 x df2 / 2 = 1022.73 x 0.8 x 0.8 / 2 = 327.274 kN-m."));
            list.Add(string.Format(""));
            list.Add(string.Format("Lever arm = j         = 0.5 + √ 0.25 - (BM x 1000000) / (fck x 0.87 x d2 x B x 1000)"));
            list.Add(string.Format("                = 0.5 + √ 0.25 - (327.274 x 1000000) / (20 x 0.87 x 660 x 660 x 2.2 x 1000)"));
            list.Add(string.Format("                = 0.5 + 0.480"));
            list.Add(string.Format("                = 0.980"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ast         = 327.274 x 1000000 / (0.87 x j x fy x d)"));
            list.Add(string.Format("        = 327.274 x 1000000 / (0.87 x 0.980 x 415 x 660)"));
            list.Add(string.Format("        = 1401.439 Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of a 20mm. dia bar = ast1 = π x bardia2 / 4 = 3.1416 x 20 x 20 / 4 =  314.16 Sq.mm"));
            list.Add(string.Format("Spacing of bars  = s = 1000 x ast1 / Ast =  1000 x 314.16 / 1401.439 =  224.17 mm."));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide At2         = 20 mm. dia bars @ 200 mm c/c spacing. "));
            list.Add(string.Format("                                = ast1 x 1000 / s "));
            list.Add(string.Format("                = 314.16 x 1000 / 200"));
            list.Add(string.Format("= 1570.8 Sq.mm"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("As At2 > At1, so provide At2 in Transverse direction,"));
            list.Add(string.Format("20 mm. dia bars @ 200 mm c/c spacing."));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 7: DESIGN SUMMARY:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Size of Footing 7.60m x 2.20m, Thickness 700 mm."));
            list.Add(string.Format("Longitudinal distance from left edge of footing to left face of left column1 = 0.27m,"));
            list.Add(string.Format("Length of left column1 = 0.40 m, Length of right column2 = 0.6 m,"));
            list.Add(string.Format("Longitudinal distance between centre of column1 to centre of column2 = d2 = 5.0 m. "));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculated Rebars 20 mm. dia. @ 75 mm c/c spacing in longitudinal direction at top and bottom,"));
            list.Add(string.Format("Calculated Rebars 20 mm. dia. @ 200 mm c/c spacing in transverse direction at top and bottom."));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 8: REMARKS: "));
            list.Add(string.Format(""));
            list.Add(string.Format("Design is ‘OK’."));
            list.Add(string.Format("Design is ‘NOT OK’, in respect of Shear stress."));
            list.Add(string.Format("================================="));
            list.Add(string.Format("END OF DESIGN OF COMBINED FOOTING"));
            list.Add(string.Format("================================="));
            list.Add(string.Format(""));
            #endregion Program
        }
        public void Calculate_Program()
        {
            List<string> list = new List<string>();




            double l1, b1, l2, b2, P1, P2, d1, d2, p, fck, fy, L, B, d, bardia, cc;

            double sigma_c, sigma_st, m;


            l1 = MyList.StringToDouble(txt_l1.Text, 0.0);
            b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            l2 = MyList.StringToDouble(txt_l2.Text, 0.0);
            b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            P1 = MyList.StringToDouble(txt_P1.Text, 0.0);
            P2 = MyList.StringToDouble(txt_P2.Text, 0.0);
            d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
            d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
            p = MyList.StringToDouble(txt_p.Text, 0.0);
            fck = MyList.StringToDouble(cmb_fck.Text, 0.0);
            fy = MyList.StringToDouble(cmb_fy.Text, 0.0);
            L = MyList.StringToDouble(txt_L.Text, 0.0);
            B = MyList.StringToDouble(txt_B.Text, 0.0);
            d = MyList.StringToDouble(txt_d.Text, 0.0);
            bardia = MyList.StringToDouble(txt_bardia.Text, 0.0);
            cc = MyList.StringToDouble(txt_cc.Text, 0.0);
            sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);
            sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
            m = MyList.StringToDouble(txt_m.Text, 0.0);



            //Allowable Flexural Stress in Concrete [σ_c] 



            #region Program

            #region TechSOFT Banner
            list.Add("\t\t***********************************************");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*            ASTRA Pro Release 21             *");
            list.Add("\t\t*        TechSOFT Engineering Services        *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t*          DESIGN OF COMBINED FOOTING         *");
            list.Add("\t\t*                                             *");
            list.Add("\t\t***********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion



            #region User's Data:



            list.Add(string.Format(""));
            list.Add(string.Format("User's Data:"));
            list.Add(string.Format("---------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Size of Column1 (Left Column) = l1 x b1 = {0} x {1} Sq.mm", l1, b1));
            list.Add(string.Format("Size of Column2 (Right Column) = l2 x b2 = {0} x {1} Sq.mm", l2, b2));
            list.Add(string.Format("Load on Column1 = P1 = {0} kN", P1));
            list.Add(string.Format("Load on Column2 = P2 = {0} kN", P2));
            list.Add(string.Format("Distance from Left side property line to left face of column1 = d1 = {0} m.", d1));
            list.Add(string.Format("Distance between centre of column1 to centre of column2 = d2 = {0} m. ", d2));
            list.Add(string.Format("Safe Bearing Capacity of soil = p = {0} kN/Sq.m", p));
            list.Add(string.Format("Concrete Grade = fck = {0} N/Sq.mm", fck));

            list.Add(string.Format("Allowable Flexural Stress in Concrete  = σ_c = {0} N/Sq.mm", sigma_c));


            list.Add(string.Format("Steel Grade = fy = {0} N/Sq.mm", fy));
            list.Add(string.Format("Permissible Stress in Steel = σ_st = {0} N/Sq.mm", sigma_c));

            list.Add(string.Format("Proposed Length of footing = L = {0} m.", L));
            list.Add(string.Format("Proposed Width of footing = B = {0} m.", B));
            list.Add(string.Format("Proposed Effective depth of footing = d = {0} mm.", d));
            list.Add(string.Format("Proposed Bar Diameter = bardia = {0} mm.", bardia));
            list.Add(string.Format("Clear Cover = cc = {0} mm.", cc));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("============================================"));
            list.Add(string.Format("Design Calculations:"));
            list.Add(string.Format("============================================"));
            list.Add(string.Format(""));

            #endregion


            #region STEP 1: DETERMINE REQUIRED SIZE OF COMBINED FOOTING:


            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1: DETERMINE REQUIRED SIZE OF COMBINED FOOTING:"));
            list.Add(string.Format("----------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Let, the Left side property line coincides with the left edge of footing,"));
            list.Add(string.Format("Let, the distance from P to the left = X1,"));

            double d3 = ((l1 / 1000) / 2) + d1;
            list.Add(string.Format("Distance from left edge to centre of left column1 = d3 = (l1/2) + d1 = ({0}/2) + {1} = {2:f3} m.", (l1 / 1000), d1, d3));


            double d4 = ((l1/1000) / 2) + d1 + d2;
            list.Add(string.Format("Distance from left edge to centre of right column2 = d4 = (l1/2) + d1 + d2 = ({0}/2) + {1} + {2} = {3:f3} m.", (l1 / 1000), d1, d2, d4));
           
            
            
            list.Add(string.Format(""));
            list.Add(string.Format("Taking moment about the left edge,"));
            list.Add(string.Format(""));
            list.Add(string.Format("P x X1 = P1 x d3 + P2 x d4"));
            list.Add(string.Format("    X1 = (P1 x d3 + P2 x d4) / P"));
            list.Add(string.Format(""));

            double X1 = (P1 * d3 + P2 * d4) / (P1 + P2);
            list.Add(string.Format("       = ({0} x {1:f3} + {2} x {3:f3}) / {4}", P1, d3, P2, d4, (P1 + P2)));
            list.Add(string.Format("       = {0:f3} m. ", X1));
            list.Add(string.Format("       = Distance of Centre of Gravity of applied total load from left edge"));


            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Length of footing = 2 x X1 = 2 x {0:f3} = {1:f3} m ", X1, 2 * X1));
            list.Add(string.Format(""));
            list.Add(string.Format("Adopt User’s proposed length of footing = L = {0} m.", L));
            list.Add(string.Format(""));
            list.Add(string.Format("Distance of centre of left column load (column1) from CG of  total load = X2 "));
            list.Add(string.Format("                                                                        = X1 - d3 "));
            double X2 = X1 - d3;
            list.Add(string.Format("                                                                        = {0:f3} - {1:f3} = {2:f3} m.", X1, d3, X2));
            list.Add(string.Format(""));
            list.Add(string.Format("Distance of centre of right column load (column2) from CG of total load  = X3 "));
            list.Add(string.Format("                                                                         = d2 - X2"));
            double X3 = d2 - X2;
            list.Add(string.Format("                                                                         = {0:f3} - {1:f3} = {2:f3} m.", d2, X2, X3));
            list.Add(string.Format(""));

            double d5 = L - d4;
            list.Add(string.Format("Distance from right edge to centre of right column2 = d5 = L - d4 = {0} - {1:f3} = {2:f3} m.", L, d4, d5));
            list.Add(string.Format(""));


            double P = P1 + P2;
            list.Add(string.Format("Total applied load = P = P1+P2 = {0} + {1} = {2} kN", P1, P2, P));


            double self_wgt = P * 0.1;
            list.Add(string.Format("Assumed self weight of Footing 10% = {0} kN", self_wgt));
            list.Add(string.Format(""));

            double W = P + self_wgt;
            list.Add(string.Format("Total load = W = {0} + {1} = {2} kN", P, self_wgt, W));


            double req_foot_wd = W / (L * p);
            list.Add(string.Format("Required width of footing = W/(L x p) = {0}/({1} x {2}) = {3:f3} m. ", W, L, p, req_foot_wd));
            list.Add(string.Format(""));
            list.Add(string.Format("Adopt User’s proposed width of footing = B = {0} m.", B));
            list.Add(string.Format(""));

            #endregion STEP 1: DETERMINE REQUIRED SIZE OF COMBINED FOOTING:


            #region STEP 2: CALCULATE LONGITUDINAL BENDING MOMENT AND SHEAR FORCE

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2: CALCULATE LONGITUDINAL BENDING MOMENT AND SHEAR FORCE"));
            list.Add(string.Format("-------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double P3 = 1.5 * P1;
            double P4 = 1.5 * P2;
            list.Add(string.Format("Factored Load on column1 = P3 = 1.5 x P1 = 1.5 x {0} = {1} kN.", P1, P3));
            list.Add(string.Format("Factored Load on column2 = P4 = 1.5 x P2 = 1.5 x {0} = {1} kN.", P2, P4));
            list.Add(string.Format(""));

            list.Add(string.Format("Footing size = L x B = {0} x {1} m.", L, B));
            list.Add(string.Format(""));


            double soil_pressure = (P3 + P4) / (L * B);
            list.Add(string.Format("Not soil pressure = P / (L x B) =  ({0} + {1}) / ({2} x {3}) = {4:f3} kN/Sq.m.", P3, P4, L, B, soil_pressure));
            list.Add(string.Format(""));

            double p2 = soil_pressure * B;
            list.Add(string.Format("Pressure per metre length = p2 = {0:f3} x {1} = {2:f3} kN/m.", soil_pressure, B, p2));
            list.Add(string.Format(""));
            //list.Add(string.Format("B.M. and S.F. in the longitudinal direction are shown in the Figure."));
            //list.Add(string.Format("===================================================================================="));
            //list.Add(string.Format(""));
            //list.Add(string.Format("===================================================================================="));
            list.Add(string.Format("Some calculations are given below:"));
            list.Add(string.Format(""));

            double V1 = -p2 * d3;
            list.Add(string.Format("Minimum Shear Force at load P1 at the centre of column1 = V1 = -p2 x d3 = -{0:f3} x {1:f3} = {2:f3} kN.", p2, d3, V1));

            double V2 = V1 + P3;
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Shear Force at load P1 at the centre of column1 = V2 = V1 + P3 = {0:f3} + {1} = {2:f3} kN.", V1, P3, V2));
            list.Add(string.Format(""));

            double V3 = p2 * d5;
            list.Add(string.Format("Minimum Shear Force at load P2 at the centre of column2 = V3 = p2 x d5 = {0:f3} x {1:f3} = {2:f3} kN.", p2, d5, V3));
            list.Add(string.Format(""));

            double V4 = V3 - P4;
            list.Add(string.Format("Maximum Shear Force at load P2 at the centre of column2 = V4 = V3 - P4 = {0:f3} - {1} = {2:f3} kN.", V3, P4, V4));
            list.Add(string.Format(""));
            list.Add(string.Format("Let, in the central portion S.F. is zero at ‘dx’ distance from load P1,"));
            list.Add(string.Format(""));
            list.Add(string.Format("We can write the equation, by considering two similar triangles, from the shear force diagram,        "));
            list.Add(string.Format("                             V2 / dx = (0.0-V4) / (d2-dx)"));
            list.Add(string.Format("                      i.e,    {0:f3} / dx = {1:f3} / ({2:f2}-dx)", V2, (0.0 - V4), d2));
            list.Add(string.Format("                      i.e,    {0:f3} x  {1:f3} - {0:f3} x  dx = {2:f3} x dx", V2, d2, (0.0 - V4)));


            double dx = V2 * d2 / (V2 + (0.0 - V4));
            //list.Add(string.Format("                      i.e,    dx = (916.283  x 5) / (916.283 + 1304.112) = 4581.415 / 2220.395 = 2.063 m"));
            list.Add(string.Format("                      i.e,    dx = ({0:f3}  x {1:f3}) / ({0:f3} + {2:f3}) = {3:f3} m", V2, d2, (0.0 - V4), dx));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment at distance ‘dx’ computed from left side"));
            list.Add(string.Format(""));

            double BM1 = (p2 * (d3 + dx) * (d3 + dx) / 2) - P3 * dx;
            list.Add(string.Format(" BM1 = [p2 x (d3 + dx) x (d3 + dx) / 2] - P3 x dx"));
            list.Add(string.Format("     = [{0:f3} x ({1:f3} + {2:f3}) x ({1:f3} + {2:f3}) / 2] -  {3} x {2:f3}", p2, d3, dx, P3));
            list.Add(string.Format("     = {0:f3} - {1:f3}", (p2 * (d3 + dx) * (d3 + dx) / 2), P3 * dx));
            list.Add(string.Format("     = {0:f3} kN-m.", BM1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Bending Moment at distance ‘dx’ computed from right side"));
            list.Add(string.Format(""));

            double BM2 = (p2 * (d5 + d2 - dx) * (d5 + d2 - dx) / 2) - P4 * (d2 - dx);
            list.Add(string.Format(" BM2 = [p2 x (d5 + d2-dx) x (d5 + d2-dx) / 2] - P4 x dx"));
            //list.Add(string.Format("     = [444.0788 x (2.13 + 4.0 - 2.063) x (2.13 + 4.0 - 2.063) / 2] -  2250 x (4.0 - 2.063)"));
            list.Add(string.Format("     = [{0:f3} x ({1:f3} + {2:f3} - {3:f3}) x ({1:f3} + {2:f3} - {3:f3}) / 2] -  {4:f3} x ({2:f3} - {3:f3})", p2, d5, d2, dx, P4));
            list.Add(string.Format("     = {0:f3} - {1:f3}", (p2 * (d5 + d2 - dx) * (d5 + d2 - dx) / 2), P4 * (d2 - dx)));
            list.Add(string.Format("     = {0:f3} kN-m.", BM2));
            list.Add(string.Format(""));

            double BM = Math.Abs(BM1) > Math.Abs(BM2) ? Math.Abs(BM1) : Math.Abs(BM2);

            list.Add(string.Format("Design Bending Moment = BM = Larger of values for BM1 and BM2 = {0:f3} kN-m.", BM));
            list.Add(string.Format(""));


            double d_foot = Math.Sqrt(BM * 1000000 / (0.138 * fck * B * 1000));


            list.Add(string.Format("Thickness of combined footing = d = √ [BM x 1000000 / (0.138 x fck x B x 1000)]"));
            list.Add(string.Format("                                   = √ [{0:f3} x 1000000 / (0.138 x {1} x {2} x 1000)]", BM, fck, B));
            list.Add(string.Format("                                   = {0:f3} mm.", d_foot));
            list.Add(string.Format(""));
            list.Add(string.Format("Adopt User’s proposed effective depth of footing = d = {0} mm.", d));
            list.Add(string.Format(""));

            double D = d + cc + bardia / 2;
            list.Add(string.Format("Over thickness of footing = D = d + cc + bardia /2 = {0} + {1} + ({2}/2) = {3} mm.", d, cc, bardia, D));
            list.Add(string.Format(""));

            #endregion STEP 2: CALCULATE LONGITUDINAL BENDING MOMENT AND SHEAR FORCE


            #region STEP 3: MAIN LONGITUDINAL REINFORCEMENTS FOR NEGATIVE BENDING MOMENTS

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3: MAIN LONGITUDINAL REINFORCEMENTS FOR NEGATIVE BENDING MOMENTS"));
            list.Add(string.Format("---------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double j = 0.5 + Math.Sqrt(0.25) - (BM * 1000000) / (fck * 0.87 * d * d * B * 1000);
            list.Add(string.Format("Lever arm = j = 0.5 + √ 0.25 - (BM x 1000000) / (fck x 0.87 x d2 x B x 1000)"));
            list.Add(string.Format("              = 0.5 + √ 0.25 - ({0:f3} x 1000000) / ({1} x 0.87 x {2} x {2} x {3} x 1000)", BM, fck, d, B));
            list.Add(string.Format("              = 0.5 + {0:f3}", Math.Sqrt(0.25) - (BM * 1000000) / (fck * 0.87 * d * d * B * 1000)));
            list.Add(string.Format("              = {0:f3} ", j));
            list.Add(string.Format(""));


            double Ast = BM * 1000000 / (0.87 * j * fy * d);

            list.Add(string.Format("Ast  = BM x 1000000 / (0.87 x j x fy x d)"));
            list.Add(string.Format("     = {0:f3} x 1000000 / (0.87 x {1:f3} x {2} x {3})", BM, j, fy, d));
            list.Add(string.Format("     = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));


            double ast1 = Math.PI * bardia * bardia / 4;
            list.Add(string.Format("Area of a {0} mm. dia bar = ast1 = {1:f4} x bardia^2 / 4 = {1:f4} x {0} x {0} / 4 =  {2:f4} Sq.mm", bardia, Math.PI, ast1));

            list.Add(string.Format(""));


            double s = 1000 * ast1 / Ast;
            list.Add(string.Format("Spacing of bars  = s = 1000 x ast1 / Ast =  1000 x {0:f3} / {1:f3} = {2:f3} mm.", ast1, Ast, s));
            list.Add(string.Format(""));


            s = (int)s;
            list.Add(string.Format("Provide Ast1 = {0} mm. dia bars @ {1} mm c/c spacing. ", bardia, s));
            list.Add(string.Format("             = ast1 x 1000 / s "));
            list.Add(string.Format("             = {0:f3} x 1000 / {1:f3}", ast1, s));

            double sp_long = s;

            double Ast1 = ast1 * 1000 / s;
            list.Add(string.Format("             = {0:f3} Sq.mm", Ast1));
            list.Add(string.Format(""));

            #endregion STEP 3: MAIN LONGITUDINAL REINFORCEMENTS FOR NEGATIVE BENDING MOMENTS


            #region STEP 4: SHEAR ONE WAY ACTION UNDER HIGHER COLUMN LOAD

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4: SHEAR ONE WAY ACTION UNDER HIGHER COLUMN LOAD"));
            list.Add(string.Format("-----------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double l = l2 / 1000;
            double b = b2 / 1000;
            list.Add(string.Format("Size of column,  l = l2 = {0:f3} m.,   b = b2 = {1:f3} m.", l, b));
            list.Add(string.Format(""));

            double V = V4;
            list.Add(string.Format("Shear Force at the face of higher column load, V = V4 = {0} kN", V));
            list.Add(string.Format(""));


            double Vu = V - p2 * ((l / 2) + (d / 1000));
            list.Add(string.Format("Shear force at distance ‘d’ from face of column = Vu = V - p2 x [(l/2) + d] "));
            list.Add(string.Format("                                                     = {0:f3} - {1:f3} x [({2:f3}/2) + {3:f3}]", V, p2, l, d/1000));
            list.Add(string.Format("                                                     = {0:f3} - {1:f3}",V , p2 * ((l / 2) + (d / 1000))));
            list.Add(string.Format("                                                     = {0:f3} kN.", Vu));
            list.Add(string.Format(""));


            double tau_v = Vu * 1000 / (B * 1000 * d);
            list.Add(string.Format("Nominal Shear Stress = τv = Vu x 1000 / (b x d) = {0:f3} x 1000 / ({1} x {2}) = {3:f3} N/Sq.mm", Vu, B * 1000, d, tau_v));
            list.Add(string.Format(""));


            double prcnt = Ast1 * 100 / (B*1000 * d);

            list.Add(string.Format("Percentage of tension steel  provided = p = Ast1 x 100 / (B x d) = {0:f3} x 100 / ({1} x {2}) = {3:f3} %", Ast1, B * 1000, d, prcnt));
            list.Add(string.Format(""));
            CONCRETE_GRADE cgr = (CONCRETE_GRADE)(int) fck;

            string kStr = "";
            double tau_c = iApp.Tables.Permissible_Shear_Stress(prcnt, cgr, ref kStr);

            
            list.Add(string.Format("Permissible Shear Stress for M {0} Grade Concrete and {1:f3} % of steel  = τc = {2:f3} N/Sq.mm", fck, prcnt, tau_c));

            string Check_Step4 = "";

            if (tau_c > tau_v)
            {
                list.Add(string.Format("Permissible Shear Stress (τc) = {0:f3} > Nominal Shear Stress (τv) = {1:f3}, Hence OK", tau_c, tau_v));
                Check_Step4 = "DESIGN is 'OK' in STEP 4";
            }
            else
            {
                list.Add(string.Format("Permissible Shear Stress (τc) = {0:f3} < Nominal Shear Stress (τv) = {1:f3}, Hence NOT OK", tau_c, tau_v));
                list.Add(string.Format("Size or Thickness may be increased’."));

                Check_Step4 = "DESIGN is 'NOT OK' in STEP 4";
            }
            //list.Add(string.Format(""));
            //list.Add(string.Format("If (τc > τv ) ‘Hence OK’, "));
            //list.Add(string.Format("Else ‘Hence NOT OK, Size or Thickness may be increased’."));
            //list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion STEP 4: SHEAR ONE WAY ACTION UNDER HIGHER COLUMN LOAD


            #region STEP 5: SHEAR TWO WAY ACTION UNDER HIGHER COLUMN LOAD

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5: SHEAR TWO WAY ACTION UNDER HIGHER COLUMN LOAD"));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Shear strength of concrete = τc’ = ks x τc"));
            list.Add(string.Format("Where, ks = 0.5 + βc   and βc = 1,  "));
            list.Add(string.Format("ks = 1.5 > 1.0 (maximum value), therefore ks = 1.0,"));
            list.Add(string.Format(""));

            double tau_c_dash = 0.25 * Math.Sqrt(fck);

            list.Add(string.Format("τc = 0.25 x √ fck = 0.25 x √{0} = {1:f3} N/Sq.mm = τc’,", fck, tau_c_dash));
            list.Add(string.Format(""));


            P4 = 1.5 * P2;
            list.Add(string.Format("Factored Load on column2 = P4 = 1.5 x P2 = 1.5 x {0} = {1} kN.", P2, P4));
            list.Add(string.Format(""));

            //p2 = soil_pressure * B;
            list.Add(string.Format("Pressure per metre length = p2 = {0:f3} x {1} = {2:f3} kN/m.", soil_pressure, B, p2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Vu = P4 - (Math.PI * Math.Pow((0.45 + d / 2 / 1000), 2) / 4 * p2);
            list.Add(string.Format("Shear force = Vu = P4 - [π x (0.45 + d/2/1000)^2 / 4 x p2]"));
            list.Add(string.Format("                 = {0} - [{1:f3} x (0.45 + {2}/2/1000)2 / 4 x {3:f3}]", P4, Math.PI, d, p2));
            list.Add(string.Format("                 = {0} - {1:f3}", P4, (Math.PI * Math.Pow((0.45 + d / 2 / 1000), 2) / 4 * p2)));
            list.Add(string.Format("                 = {0:f3} kN", Vu));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            tau_v = Vu * 1000 / (Math.PI * (0.45 + d / 2 / 1000) * 1000 * d);

            list.Add(string.Format("Nominal Shear Stress = τv = Vu x 1000 / (bo x d) "));
            list.Add(string.Format("                          = Vu x 1000 / [π x (0.45 + d/2/1000) x 1000 x d]"));
            list.Add(string.Format("                          = {0:f3} x 1000 / [{1:f3} x (0.45 +{2}/2/1000) x 1000 x {2}]", Vu, Math.PI, d));
            list.Add(string.Format("                          = {0:f3} x 1000 / {1:f3}", Vu, (Math.PI * (0.45 + d / 2 / 1000) * 1000 * d)));

            string Check_Step5 = "";
            if (tau_v > tau_c_dash)
            {
                list.Add(string.Format("                          = {0:f3} N/Sq.mm > τc’ = {1:f3}, Hence OK", tau_v, tau_c_dash));

                Check_Step5 = "Design is ‘OK’, in respect of Shear stress. (STEP 5)";
            }
            else if (tau_v > tau_c_dash)
            {
                list.Add(string.Format("                          = {0:f3} N/Sq.mm < τc’ = {1:f3}, Hence NOT OK", tau_v, tau_c_dash));
                list.Add(string.Format(""));
                list.Add(string.Format("size or thickness of Footing may be increased’."));
                Check_Step5 = "Design is 'NOT OK', in respect of Shear stress. (STEP 5)";
            }
            list.Add(string.Format(""));

            #endregion STEP 5: SHEAR TWO WAY ACTION UNDER HIGHER COLUMN LOAD


            #region STEP 6: TRANSVERSE REINFORCEMENT

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 6: TRANSVERSE REINFORCEMENT"));
            list.Add(string.Format("--------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored Load on column1 = P3 = 1.5 x P1 = 1.5 x {0} = {1} kN.", P1, P3));
            list.Add(string.Format(""));


            double pt1 = P3 / B;
            list.Add(string.Format("Factored upward pressure under column1 = pt1 = P3 / B = {0} / {1} = {2:f3} kN/m", P3, B, pt1));

            double df1 = (B - (b1 / 1000)) / 2;
            list.Add(string.Format("Distance of face of column1 from Longer Edge of footing = df1 = (B - b1) / 2 = ({0} - {1}) / 2 = {2:f3} m.", B, (b1 / 1000), df1));


            BM = pt1 * df1 * df1 / 2;

            list.Add(string.Format("Transverse BM at the face of column1 = pt1 x df1 x df1 / 2"));
            list.Add(string.Format("                                     = {0:f3} x {1:f3} x {1:f3} / 2 = {2:f3} kN-m.", pt1, df1, BM));
            list.Add(string.Format(""));

            j = 0.5 + Math.Sqrt(0.25) - (BM * 1000000) / (fck * 0.87 * d * d * B * 1000);

            list.Add(string.Format("Lever arm = j = 0.5 + √ 0.25 - (BM x 1000000) / (fck x 0.87 x d^2 x B x 1000)"));
            list.Add(string.Format("              = 0.5 + √ 0.25 - ({0:f3} x 1000000) / ({1} x 0.87 x {2} x {2} x {3} x 1000)", BM, fck, d, B));
            list.Add(string.Format("              = 0.5 + {0:f3}", Math.Sqrt(0.25) - (BM * 1000000) / (fck * 0.87 * d * d * B * 1000)));
            list.Add(string.Format("              = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Ast = BM * 1000000 / (0.87 * j * fy * d);


            list.Add(string.Format("Ast = 207.1 x 1000000 / (0.87 x j x fy x d)"));
            list.Add(string.Format("    = {0:f3} x 1000000 / (0.87 x {1:f3} x {2} x {3})", BM, j, fy, d));
            list.Add(string.Format("    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));


            ast1 = Math.PI * bardia * bardia / 4;
            list.Add(string.Format("Area of a {0} mm. dia bar = ast1 = π x bardia^2 / 4 = 3.1416 x {0} x {0} / 4 =  {1:f3} Sq.mm", bardia, ast1));


            s = 1000 * ast1 / Ast;

            list.Add(string.Format("Spacing of bars  = s = 1000 x ast1 / Ast =  1000 x {0:f3} / {1:f3} = {2:f3} mm.", ast1, Ast, s));
            list.Add(string.Format(""));

            s = (int)(s / 10);
            s = (s * 10);

            double s1 = s;
            list.Add(string.Format("Provide  Ast1 = {0} mm. dia bars @ {1} mm c/c spacing. ", bardia, s));
            list.Add(string.Format("             = ast1 x 1000 / s "));
            list.Add(string.Format("             = {0:f3} x 1000 / {1}", ast1, s));
            Ast1 = ast1 * 1000 / s;
            list.Add(string.Format("             = {0:f3} Sq.mm", Ast1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Factored Load on column2 = P4 = 1.5 x P2 = 1.5 x {0} = {1} kN.", P2, P4));
            list.Add(string.Format(""));


            double pt2 = P4 / B;
            list.Add(string.Format("Factored upward pressure under column2 = pt2 = P4 / B = {0} / {1} = {2:f3} kN/m", P4, B, pt2));
            list.Add(string.Format(""));

            double df2 = (B - (b2 / 1000)) / 2;


            list.Add(string.Format("Distance of face of column2 from Longer Edge of footing = df2 = (B - b2) / 2 = ({0}-{1})/2 = {2:f3} m.", B, (b2 / 1000), df2));
            list.Add(string.Format(""));



            BM = pt2 * df2 * df2 / 2;
            list.Add(string.Format("Transverse BM at the face of column2 = pt2 x df2 x df2 / 2 = {0:f3} x {1:f3} x {1:f3} / 2 = {2:f3} kN-m.", pt2, df2, BM));
            list.Add(string.Format(""));


            j = 0.5 + Math.Sqrt(0.25) - (BM * 1000000) / (fck * 0.87 * d * d * B * 1000);


            list.Add(string.Format("Lever arm =  j  = 0.5 + √ 0.25 - (BM x 1000000) / (fck x 0.87 x d^2 x B x 1000)"));
            list.Add(string.Format("                = 0.5 + √ 0.25 - ({0:f3} x 1000000) / ({1} x 0.87 x {2} x {2} x {3} x 1000)", BM, fck, d, B));
            list.Add(string.Format("                = 0.5 + {0:f3}", Math.Sqrt(0.25) - (BM * 1000000) / (fck * 0.87 * d * d * B * 1000)));
            list.Add(string.Format("                = {0:f3}", j));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            Ast = BM * 1000000 / (0.87 * j * fy * d);


            list.Add(string.Format("Ast = {0:f3} x 1000000 / (0.87 x j x fy x d)", BM));
            list.Add(string.Format("    = {0:f3} x 1000000 / (0.87 x {1:f3} x {2} x {3})", BM, j, fy, d));
            list.Add(string.Format("    = {0:f3} Sq.mm", Ast));
            list.Add(string.Format(""));

            ast1 = Math.PI * bardia * bardia / 4;
            list.Add(string.Format("Area of a {0} mm. dia bar = ast1 = π x bardia^2 / 4 = 3.1416 x {0} x {0} / 4 =  {1:f3} Sq.mm", bardia, ast1));


            list.Add(string.Format(""));
            s = 1000 * ast1 / Ast;


            list.Add(string.Format("Spacing of bars  = s = 1000 x ast1 / Ast =  1000 x {0:f3} / {1:f3} =  {2:f3} mm.", ast1, Ast, s));
            list.Add(string.Format(""));

            s = (int)(s / 10);
            s = (int)(s * 10);
            list.Add(string.Format(""));

            double Ast2 = ast1 * 1000 / s;
            list.Add(string.Format("Provide Ast2 = {0} mm. dia bars @ {1} mm c/c spacing. ", bardia, s));
            list.Add(string.Format("             = ast1 x 1000 / s "));
            list.Add(string.Format("             = {0:f3} x 1000 / {1}", ast1, s));
            list.Add(string.Format("             = {0:f3} Sq.mm", Ast2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double sp_trans = 0;
            if (Ast2 > Ast1)
            {
                list.Add(string.Format("As Ast2 > Ast1, so provide Ast2 in Transverse direction,"));
                list.Add(string.Format("{0} mm. dia bars @ {1} mm c/c spacing.", bardia, s));
                sp_trans = s;
            }
            else
            {
                list.Add(string.Format("As Ast1 > Ast2, so provide Ast1 in Transverse direction,"));
                list.Add(string.Format("{0} mm. dia bars @ {1} mm c/c spacing.", bardia, s1));
                sp_trans = s1;
            }

            #endregion STEP 6: TRANSVERSE REINFORCEMENT

            #region STEP 7: DESIGN SUMMARY:

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 7: DESIGN SUMMARY:"));
            list.Add(string.Format(""));
            list.Add(string.Format("Size of Footing {0} m x {1} m, Thickness {2} mm.", L, B, D));


            list.Add(string.Format("Longitudinal distance from left edge of footing to left face of left column1 = {0} m,", d1));

            list.Add(string.Format("Length of left column1 = {0} m, Length of right column2 = {1} m,", l1 / 1000, l2 / 1000));


            list.Add(string.Format("Longitudinal distance between centre of column1 to centre of column2 = d2 = {0} m. ", d2));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculated Rebars {0} mm. dia. @ {1} mm c/c spacing in longitudinal direction at top and bottom,", bardia, sp_long));
            list.Add(string.Format("Calculated Rebars {0} mm. dia. @ {1} mm c/c spacing in transverse direction at top and bottom.", bardia, sp_trans));
            list.Add(string.Format(""));

            #endregion STEP 7: DESIGN SUMMARY:


            #region STEP 8: REMARKS:

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 8: REMARKS: "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Design is ‘OK’."));
            //list.Add(string.Format("Design is ‘NOT OK’, in respect of Shear stress."));


            list.Add(string.Format("{0}", Check_Step4));
            list.Add(string.Format("{0}", Check_Step5));


            list.Add(string.Format("================================="));
            list.Add(string.Format("END OF DESIGN OF COMBINED FOOTING"));
            list.Add(string.Format("================================="));
            list.Add(string.Format(""));

            #endregion STEP 8: REMARKS:

            #endregion Program

            File.WriteAllLines(Report_File, list.ToArray());

            //System.Diagnostics.Process.Start(Report_File);

            iApp.View_Result(Report_File);
        }

        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_rcc_pier") || ctrl.Name.ToLower().StartsWith("txt_rcc_pier"))
            {
                astg = new ASTRAGrade(cmb_fck.Text, cmb_fy.Text);
                txt_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString();
                txt_sigma_st.Text = astg.sigma_st_N_sq_mm.ToString();
            }
        }

        private void btn_Process_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_Process.Name)
                Calculate_Program();
            else
            {
                this.Close();
            }
        }


        #region Chiranjit [2016 09 07
        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Combined_Footing;
            }
        }
        public string user_path
        {
            get
            {
                return iApp.user_path;
            }

            set
            {
                iApp.user_path = value;
            }
        }
        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                   "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            txt_project_name.Text = prj_name;

        }

        public void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

        #endregion Chiranjit [2016 09 07]


        private void btn_psc_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_open_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, user_path);

                    txt_project_name.Text = Path.GetFileName(user_path);

                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                    }

                    txt_project_name.Text = Path.GetFileName(user_path);

                    #endregion Save As

                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(Path.Combine(iApp.LastDesignWorkingFolder, Title));
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_project_name.Text != "")
                //    frm.Project_Name = txt_project_name.Text;
                //else
                //    frm.Project_Name = "Design of Composite Bridge";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_project_name.Text = frm.Project_Name;
                //    //btn_TGirder_process.Enabled = true;
                //    IsCreate_Data = true;
                //}
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            //throw new NotImplementedException();
        }

        private void Button_Enable_Disable()
        {
            //throw new NotImplementedException();
        }

        private void frm_CombinedFooting_Load(object sender, EventArgs e)
        {
            Set_Project_Name();

            cmb_fck.SelectedIndex = 3;
            cmb_fy.SelectedIndex = 2;

        }
        string Report_File
        {
            get
            {
                return Path.Combine(user_path, "Combined_Footing_Report.txt");
            }
        }
        private void btn_Report_Click(object sender, EventArgs e)
        {
            if(File.Exists(Report_File))
                System.Diagnostics.Process.Start(Report_File);
        }

        private void btn_drawings_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Drawing_Folder, "COMBINED_FOUNDATION");

        }

    }
}
