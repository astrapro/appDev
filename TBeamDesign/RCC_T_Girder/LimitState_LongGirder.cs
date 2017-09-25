using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;


namespace BridgeAnalysisDesign.RCC_T_Girder
{
    public class LimitState_LongGirder
    {

        IApplication iApp;
        public LimitState_LongGirder(IApplication iapp)
        {
            iApp = iapp;
        }

        //public double CGrade { get; set; }
        //public double SGrade { get; set; }

        //public double Lamda
        //{
        //    get
        //    {
        // if (CGrade < 6.0)
        //     return 0.8;
        // return (0.8 - (CGrade - 60.0) / 500.0);

        //    }
        //}

        //public double Eta
        //{
        //    get
        //    {
        // if (CGrade < 6.0)
        //     return 1.0;
        // return (1.0 - (CGrade - 60.0) / 250.0);
        //    }
        //}

        //public double Gama_c { get; set; }
        //public double Alpha { get; set; }
        //public double fcd
        //{
        //    get
        //    {
        // return (Alpha * CGrade / Gama_c);
        //    }
        //}
        //public double cov { get; set; }
        //public double fcd
        //{
        //    get
        //    {
        // return CGrade;
        //    }
        //}
        //public double Lc { get; set; }



        #region Variables

        public string rep_file_name = "";
        public string file_path = "";
        public string user_input_file = "";
        public string system_path = "";
        public string user_path = "";
        public string drawing_path = "";
        public bool is_process = false;

        //public double Ds;
        //public double total_bars_L2, total_bars_L4, total_bars_Deff;
        ////public double D;
        //public double Bb, Bf, Db, Dw, As, nl;

        ////public double bw;
        //public double L;
        //public double Gs;
        //public double design_moment_mid;
        //public double design_moment_quarter;
        //public double design_moment_deff;
        //public double concrete_grade;
        //public double steel_grade;
        //public double modular_ratio;
        //public double bar_dia;
        //public double v1, v2, v3;
        //public double cover;
        //public double sigma_sv;
        //public double deff;
        //public double space_main, space_cross;
        //public double allow_stress_concrete;
        //public bool isInner;

        //public double bar_dia_L2, bar_dia_L4, bar_dia_Deff;


        //public double inner_deff_moment = 0.0;
        //public double outer_deff_moment = 0.0;

        //public double inner_L4_moment = 0.0;
        //public double outer_L4_moment = 0.0;

        //public double inner_L2_moment = 0.0;
        //public double outer_L2_moment = 0.0;

        //public double inner_deff_shear = 0.0;
        //public double outer_deff_shear = 0.0;

        //public double inner_L4_shear = 0.0;
        //public double outer_L4_shear = 0.0;

        //public double inner_L2_shear = 0.0;
        //public double outer_L2_shear = 0.0;

        #endregion Variables

        public string FilePath
        {
            set
            {
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(value, "Limit State Design");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                file_path = Path.Combine(file_path, "Design of Long Main Girders");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);
              
                user_input_file = Path.Combine(system_path, "DESIGN_OF_LONGITUDINAL_GIRDERS_WITH_BOTTOM_FLANGE.FIL");
                drawing_path = Path.Combine(system_path, "LONG_GIRDERS_DRAWING.FIL");
            }
        }
        public string Inner_File
        {
            get
            {
                return Path.Combine(file_path, "T-Beam_Inner_Long_Girder.TXT");
            }
        }
        public string Outer_File
        {
            get
            {
                return Path.Combine(file_path, "T-Beam_Outer_Long_Girder.TXT");
            }
        }

    



        public double Cgrade { get; set; }
        public double Sgrade { get; set; }
        public double Lamda { get; set; }
        public double Eta { get; set; }
        public double Gama_c { get; set; }
        public double Alpha { get; set; }
        public double fcd { get; set; }
        public double cov { get; set; }
        public double fck { get; set; }
        public double Lc { get; set; }
        public double fyk { get; set; }
        public double gama_s { get; set; }
        public double gama_a { get; set; }

        public double Ecm { get; set; }
        public double Es { get; set; }


        public double dia { get; set; }
        public double alpha_cw { get; set; }
        //public double acw { get; set; }
        public double V1 { get; set; }
        public double b { get; set; }
        public double m { get; set; }
        public double mu { get; set; }

        public double a { get; set; }

        public double ce { get; set; }
        public double alpha_t { get; set; }

        public double BM { get; set; }



        public double Bs { get; set; }
        public double Ds { get; set; }
        //public double bf { get; set; }
        public double Bb { get; set; }
        public double bw { get; set; }
        //public double D1 { get; set; }
        //public double D2 { get; set; }
        //public double D3 { get; set; }
        public double Db { get; set; }
        public double D { get; set; }
        public double d { get; set; }
        public double Xu { get; set; }

        public double SF { get; set; }
        public double K1 { get; set; }
        public double K2 { get; set; }
        public double bar_dia;



        public List<SteelLayerData> SteelLayers { get; set; }

        public List<string> Get_Basic_Data()
        {
            List<string> list = new List<string>();
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format("USER'S INPUT DATA"));
            list.Add(string.Format("-------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1  Grade of concrete = Cgrade = M{0} Mpa ", Cgrade));
            list.Add(string.Format("2  Grade of reinforcement = Sgrade = Fe {0} Mpa ", Sgrade));
            list.Add(string.Format("3  Shape Factor λ  = λ = {0}                                  {1}", Lamda, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112-Anexure A2- A2.9(2) " : "")));
            list.Add(string.Format("4  strength Factor η = η = {0}                                {1}  ", Eta, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112-Anexure A2- A2.9(2)" : "")));
            list.Add(string.Format("5  Partial factor of safety (Basic and seismic) = γc = {0:f3} {1} ", Gama_c, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112-Cl 6.4.2.8" : "")));

            list.Add(string.Format("6  Coefficient to consider the influence of the strength = α  = {0} ", Alpha));
            list.Add(string.Format("7  Design value of concrete comp strength for Basic and seismic = fcd = {0} Mpa ", fcd));
            list.Add(string.Format("8  Clear cover = cov = {0} mm ", cov));
            list.Add(string.Format("9  Stress in concrete (compression) = fck = {0} Mpa ", fck));
            list.Add(string.Format("10 Limiting Strain in concrete = Lc = {0}", Lc));
            list.Add(string.Format("11 Stress in steel (tension) = fyk = Fe {0} Mpa", fyk));
            list.Add(string.Format("12 Partial factor of safety for basic and seismic = γs = {0}   {1} ", gama_s, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 Fig 6.3 (Note)" : "")));
            list.Add(string.Format("13 Partial factor of safety for Accidental = γa = {0}          {1} ", gama_a, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 Fig 6.3 (Note)" : "")));

            list.Add(string.Format("14 Ecm = {0} Mpa ", Ecm));
            list.Add(string.Format("15 Es = {0} Mpa ", Es));
            list.Add(string.Format("16 Modular ratio = m = {0:f4} ", m));
            list.Add(string.Format("17 Diameter of bar to be used for shear stirrups  = dia = {0:f3}", dia));
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                list.Add(string.Format("   Constant as per IRC 112, Clause 10.3.1 of  α_cw {0} For no axial force ", alpha_cw));

            list.Add(string.Format("18 Strength reduction factor for concrete cracked in shear V1 {0}  ", V1, (fck < 80 ? "For fck <= 80MPa" : "")));
            list.Add(string.Format("19 Ratio of the longitudinal force in the new concrete and the total logitudinal force = β = {0}  {1}", b, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 10.3.4" : "")));
            list.Add(string.Format("   Factor depends on the roughness of the interface = = µ = {0}            {1}", mu, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 10.3.4" : "")));
            list.Add(string.Format("20 Angle of the reinforcement to the interface = a = {0}°                  {1}", a, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 10.3.4" : "")));
            list.Add(string.Format("21 Creep coefficient = Ce = {0} ", ce));
            list.Add(string.Format("22 Coefficient of thermal expansion = α_t = {0} (/°C) ", alpha_t));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------------------------------------------------------"));
            list.Add(string.Format("Design Check for Longitudinal Girder (Ultimate Limit State ) at L/2 Section"));
            list.Add(string.Format("---------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("---------------------------------------------------------"));
            string format = "{0,10} {1,14} {2,14} {3,14}";
            list.Add(string.Format(format, "Layer", "Dia of Bar", "No. of Bars", "CG from bot"));
            list.Add(string.Format("---------------------------------------------------------"));

            double total_bars = 0.0;
            double total_cg = 0.0;

            double total_area = 0.0;

            format = "{0,7} {1,12} {2,14} {3,14}";
            foreach (var item in SteelLayers)
            {
                list.Add(string.Format(format, item.Layer, item.Bar_Dia, item.Bars_No, item.CG_from_bot));
                total_bars += item.Bars_No;
                total_cg += item.Bars_No * item.CG_from_bot;

                total_area += (Math.PI * item.Bar_Dia * item.Bar_Dia / 4) * item.Bars_No;
            }
            list.Add(string.Format("---------------------------------------------------------"));
            total_cg = total_cg / total_bars;
            list.Add(string.Format(format, "Total", "", total_bars, total_cg));
            list.Add(string.Format("---------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Area of bottom steel    =   {0:f3} sq.mm", total_area));


            #region Figure
            //list.Add(@"");
            //list.Add(@"");
            //list.Add(@"     _________________________________________            "));
            //list.Add(@"     |                                    ^  |     "));
            //list.Add(@"     |                  W                 |  |                                      "));
            //list.Add(@"     |_______________________________________|"));
            //list.Add(@"           |     |                |  ^    |                                 "));
            //list.Add(@"        D1 |     |<----- Wtf ---->|  |    |                                 "));
            //list.Add(@"           |     |________________|  |    |                                                 "));
            //list.Add(@"          --- --- \              /   |    |                                 "));
            //list.Add(@"               |   \            /    |    |                                 "));
            //list.Add(@"            D2 |    \          /     |    |                                  "));
            //list.Add(@"              ---    |        |      |    |                                 "));
            //list.Add(@"                     |        |      |    |                                  "));
            //list.Add(@"    N                |        |   N  |    |                                 "));
            //list.Add(@"     ---------------------------------------------                           "));
            //list.Add(@"                     |        |           |                                 "));
            //list.Add(@"                     |        |                                              "));
            //list.Add(@"                     |        |           d                                 "));
            //list.Add(@"                     |        |                                              "));
            //list.Add(@"                     |<- bw ->|           |                                 "));
            //list.Add(@"                     |        |           |                                 "));
            //list.Add(@"             ---     |        |           |                                 "));
            //list.Add(@"              |     /          \          |                                   "));
            //list.Add(@"           D3 |    /            \         |                                   "));
            //list.Add(@"              |   /              \        |                                  "));
            //list.Add(@"       ---   --- ------------------       |                                                  "));
            //list.Add(@"        |        |                |       |                                  "));
            //list.Add(@"     D4 |        |         ----------------                                 "));
            //list.Add(@"        |        |________________|                                                         "));
            //list.Add(@"       ---                                                                "));
            //list.Add(@"                 <----- bwf ------>                                      "));
            //list.Add(@"                                                                          "));
            //list.Add(@"                                                                          "));
            //list.Add(@""));
            #endregion Figure


            Bs = Bs;
            Ds = Ds;

            Bb = Bb;
            Db = Db;

            list.Add(string.Format("Bending Moment  = BM = {0} KN-m ", BM));
            list.Add(string.Format("    "));
            list.Add(string.Format("Width of Slab  = Bs  = {0} mm  ", Bs));
            list.Add(string.Format("Depth of slab   = Ds  = {0} mm  ", Ds));
            //list.Add(string.Format("Width of top flange  =  Bs  = {0} mm  ", Bs));
            list.Add(string.Format("Width of bottom flange  =  Bb  = {0} mm  ", Bb));
            list.Add(string.Format("Width of web  =  bw  = {0} mm  ", bw));
            //list.Add(string.Format("Thickness of top flange  =  D1  = {0} mm  ", D1));
            //list.Add(string.Format("Thickness of top haunch  =  D2  = {0} mm  ", D2));
            //list.Add(string.Format("Thickness of bottom haunch  =  D3  = {0} mm  ", D3));
            list.Add(string.Format("Thickness of bottom flange  =  Db  = {0} mm  ", Db));
            list.Add(string.Format("Depth of girder  =  D  = {0} mm  ", D));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------"));
            list.Add(string.Format("STEP 1 : DEPTH OF NEUTRAL AXIS FROM THE TOP OF DECK SLAB"));
            list.Add(string.Format("-----------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            d = D - total_cg - dia;

            list.Add(string.Format("Effective depth of girder  = d = D - CG_bot - dia"));
            list.Add(string.Format("                               = {0} - {1} - {2} ", D, total_cg, dia));
            list.Add(string.Format("                               = {0} mm  ", d));
            //list.Add(string.Format("Depth of neutral axis  =  Xu  = {0} mm OK ", Xu));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            #region Draw Figure
            list.Add(string.Format("        |--------------Bs-------------|"));
            list.Add(string.Format("      _  _____________________________   _"));
            list.Add(string.Format("      | |                      |      |  |"));
            list.Add(string.Format("      | |______________________|______| _| Ds  _"));
            list.Add(string.Format("      |             |    |     |               |"));
            list.Add(string.Format("      |             |    |     |n              |"));
            list.Add(string.Format("      |  -----------|----|---------- NA        |"));
            list.Add(string.Format("      |             |    |                     |"));
            list.Add(string.Format("      |             |    |                     |"));
            list.Add(string.Format("      D             |-Bw-|                     Dw"));
            list.Add(string.Format("      |             |    |                     |"));
            list.Add(string.Format("      |             |    |                     |"));
            list.Add(string.Format("      |          ___|    |___  _              _|"));
            list.Add(string.Format("      |         |            |  |"));
            list.Add(string.Format("      |         |            |  | Db"));
            list.Add(string.Format("      |_        |____________| _|"));
            list.Add(string.Format(""));
            list.Add(string.Format("                |-----Bb-----|"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion Draw Figure

            list.Add(string.Format("Let 'n' be the depth of Neutral Axis from the top of Deck slab"));

            Bs = Bs;
            Ds = Ds;

            list.Add(string.Format(""));
            double a1;
            //Bs = Bs * 1000.0;
            a1 = (Ds * (Bs - bw));
            //d1 = 

            list.Add(string.Format("a1 = Ds * (Bs - bw)"));
            list.Add(string.Format("   = {0} * ({1} - {2})", Ds, Bs, bw));
            list.Add(string.Format("   = {0} sq.mm", a1.ToString("f3")));
            list.Add(string.Format(""));
            double _d1 = Ds / 2;
            list.Add(string.Format("d1 = n - Ds/2 = n - {0}/2 = n - {1}", Ds, _d1));



            double a2 = 1;
            //a2 = bw
            list.Add(string.Format(""));
            list.Add(string.Format("a2 = Bw * n = {0} * n", bw));
            list.Add(string.Format("d2 = n/2"));




            double a3 = 1;
            //a3 = (Bb * Db)/100.0;
            list.Add(string.Format(""));
            //Chiranjit [2011 07 08]
            //For Checking
            //list.Add(string.Format("a3 =  Bb * Db;"));
            //list.Add(string.Format("   = {0} * {1}", Bb/10.0, Db/10.0);
            //list.Add(string.Format("   = {0} sq.cm", a3);
            list.Add(string.Format(""));
            double _d3 = 1;
            //double _d3 = (D - (Db / 2)) / 10.0;
            //list.Add(string.Format("d3 = D-n-Db/2 = {0} - n - {1}/2 = {2:f2} - n", D/10.0, Db/10.0, _d3);


            //double Ast = (Math.PI * dia * dia * total_bars) / 4;

            double Ast = 0.0;

            for (int i = 0; i < SteelLayers.Count; i++)
            {
                Ast += SteelLayers[i].Area;
            }



            double a4 = 1;
      
            a4 = m * (Ast);

            double eff_depth = d;
            list.Add(string.Format(""));
            list.Add(string.Format("a4 = m * Ast"));
            list.Add(string.Format("   = {0} * {1:f3}", m, Ast));
            list.Add(string.Format("   = {0:f3} sq.mm", a4));
            list.Add(string.Format(""));
            list.Add(string.Format("d4 = deff - n = {0:f2} - n", eff_depth));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Now from equation"));
            list.Add(string.Format(""));
            //Chiranjit [2011 07 08]
            //list.Add(string.Format("a1 * d1 + a2 * d2   =   a3 * d3 + a4 * d4"));
            list.Add(string.Format("a1 * d1 + a2 * d2   =   a4 * d4"));
            list.Add(string.Format(""));
            //Chiranjit [2011 07 08]
            //list.Add(string.Format("{0:f3} * (n - {1:f3}) + ({2:f3} * n) * (n/2)   =  {3:f3} * ({4:f3} - n) + {5:f3} * ({6:f3} - n)",
            //    a1, _d1, bw / 10.0, a3, _d3, a4, eff_depth / 10.0);
            list.Add(string.Format("{0} * (n - {1}) + ({2} * n) * (n/2)   =  {3:f3} * ({4} - n)", a1, _d1, bw, a4, eff_depth));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _v1, _v2, _v3, _v4, _v5, _v6, _v7;

            _v1 = a1;
            _v2 = (Ds / 20.0) * a1;
            _v3 = (bw / 20.0);
            //Chiranjit [2011 07 08]
            //_v4 = a3 * _d3;
            //_v5 = a3;
            _v6 = a4 * eff_depth;
            _v7 = a4;

            //Chiranjit [2011 07 08]
            //list.Add(string.Format("{0} * n - {1} + {2} * n * n    =  {3} - {4} * n + {5} - {6} * n",
            //    _v1, _v2, _v3, _v4, _v5, _v6, _v7);
            list.Add(string.Format("{0} * n - {1} + {2} * n * n    =  {3:f3} - {4:f3} * n", _v1, _v2, _v3, _v6, _v7));
            list.Add(string.Format(""));


            //Chiranjit [2011 07 08]
            //list.Add(string.Format("{0} * n*n + {1}*n + {2}*n + {3}*n = {4} + {5} + {6}",
            //    _v3, _v1, _v5, _v7, _v2, _v4, _v6);
            list.Add(string.Format("{0} * n*n + {1}*n + {2:f3}*n = {3:f3} + {4:f3}", _v3, _v1, _v7, _v2, _v6));

            double _a, _b, _c;
            //Chiranjit [2011 07 08]
            //_b = ((a1 + a5) / a3);
            //_c = ((a1 * a2 + a4) / a3);

            _a = _v3;
            //Chiranjit [2011 07 08]
            //_b = (_v1 + _v5 + _v7);
            //_c = (_v2 + _v4 + _v6);

            _b = (_v1 + _v7);
            _c = (_v2 + _v6);

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("    {0} * n*n + {1:f4}*n  = {2:f3} ", _a, _b, _c));
            list.Add(string.Format(""));
            _b = _b / _a;
            _c = (_c / _a);
            list.Add(string.Format("or,   n*n + {0:f0}*n  - {1:f0} = 0", _b, _c));
            list.Add(string.Format(""));

            list.Add(string.Format(""));

            double n;

            double root_a;
            root_a = Math.Sqrt((_b * _b + 4 * _c));

            n = (root_a - _b) / 2;

            n = double.Parse(n.ToString("f3"));

            list.Add(string.Format("Xu = n = {0:f4} mm = {1:f4} m = Depth of Neutral Axis from Top Edge", n, (n / 1000.0)));

            Xu = n;

            //n /= 1000.00;
            //Bs /= 1000.0;
            //bw /= 1000.0;
            //Ds /= 1000.0;
            //D /= 1000.0;
            //Db /= 1000.0;++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //eff_depth /= 1000.0;
            //Bb /= 1000.0;



            #region Chiranjit [2013 07 14]

            //double _b = 0;
            //double _d = 0;
            //double _A = 0;
            //double _X = 0;
            //double _AX = 0;
            //double _AXX = 0;
            //double _ISelf = 0;

            //List<double> ls_b = new List<double>();
            //List<double> ls_d = new List<double>();
            //List<double> ls_A = new List<double>();
            //List<double> ls_X = new List<double>();
            //List<double> ls_AX = new List<double>();
            //List<double> ls_AXX = new List<double>();
            //List<double> ls_ISelf = new List<double>();



            //list.Add(string.Format("Section Calculations :"));
            //list.Add(string.Format("----------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Deck Slab : "));
            //list.Add(string.Format("------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("b = W = {0} mm", (_b = W)));
            //list.Add(string.Format("d = T = {0} mm", (_d = T)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A1 = Area = b x d = {0} x {1} = {2} sq.mm", _b, _d, (_A = _b*_d)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("X1 = Distance from bottom Edge = D - (d/2) = {0} - ({1}/2) = {2} mm", D, _d, (_X = D - (_d / 2))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX1 = A1 x X1 = {0} x {1} = {2} cu.mm", _A, _X, (_AX = _A * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX1 = AX1 x X1 = {0} x {1} = {2} cu.mm", _AX, _X, (_AXX = _AX * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Iself1 = (b x d^3)/12 = ({0} x {1}^3)/12 = {2} cu.mm", _b, _d, (_ISelf = _b * _d * _d * _d / 12.0)));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //ls_b.Add(_b);
            //ls_d.Add(_d);
            //ls_A.Add(_A);
            //ls_X.Add(_X);
            //ls_AX.Add(_AX);
            //ls_AXX.Add(_AXX);
            //ls_ISelf.Add(_ISelf);

            //list.Add(string.Format(""));
            //list.Add(string.Format("Central Vertical Web : "));
            //list.Add(string.Format("-----------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("b = bw = {0} mm", (_b = bw)));
            //list.Add(string.Format("d = D - D1 - T - D4 = {0} - {1} - {2} - {3} = {4} mm", D , D1 , T , D4, (_d = D - D1 - T - D4)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A2 = Area = b x d = {0} x {1} = {2} sq.mm", _b, _d, (_A = _b * _d)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("X2 = Distance from bottom Edge = (d/2) = ({0}/2) = {1} mm", D, _d, (_X = (_d / 2))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX1 = A2 x X2 = {0} x {1} = {2} cu.mm", _A, _X, (_AX = _A * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX1 = AX2 x X2 = {0} x {1} = {2} cu.mm", _AX, _X, (_AXX = _AX * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Iself1 = (b x d^3)/12 = ({0} x {1}^3)/12 = {2} cu.mm", _b, _d, (_ISelf = _b * _d * _d * _d / 12.0)));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //ls_b.Add(_b);
            //ls_d.Add(_d);
            //ls_A.Add(_A);
            //ls_X.Add(_X);
            //ls_AX.Add(_AX);
            //ls_AXX.Add(_AXX);
            //ls_ISelf.Add(_ISelf);



            //list.Add(string.Format(""));
            //list.Add(string.Format("Bottom Flange : "));
            //list.Add(string.Format("----------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("b = bwf = {0} mm", (_b = bwf)));
            //list.Add(string.Format("d = D4 = {0} = {1} mm",  D4, (_d =  D4)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A3 = Area = b x d = {0} x {1} = {2} sq.mm", _b, _d, (_A = _b * _d)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("X3 = Distance from bottom Edge = (D4/2) = ({0}/2) = {1} mm", D4, _d, (_X = (d / 2))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX3 = A3 x X3 = {0} x {1} = {2} cu.mm", _A, _X, (_AX = _A * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX3 = AX3 x X3 = {0} x {1} = {2} cu.mm", _AX, _X, (_AXX = _AX * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Iself3 = (b x d^3)/12 = ({0} x {1}^3)/12 = {2} cu.mm", _b, _d, (_ISelf = _b * _d * _d * _d / 12.0)));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //ls_b.Add(_b);
            //ls_d.Add(_d);
            //ls_A.Add(_A);
            //ls_X.Add(_X);
            //ls_AX.Add(_AX);
            //ls_AXX.Add(_AXX);
            //ls_ISelf.Add(_ISelf);


            //_A = MyList.Get_Array_Sum(ls_A);
            //_X = MyList.Get_Array_Sum(ls_X);
            //_AX = MyList.Get_Array_Sum(ls_AX);
            //_AXX = MyList.Get_Array_Sum(ls_AXX);
            //_ISelf = MyList.Get_Array_Sum(ls_ISelf);


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A = A1 + A2 + A3"));
            //list.Add(string.Format("  = {0} + {1} + {2} ", ls_A[0], ls_A[1], ls_A[2]));
            //list.Add(string.Format("  = {0} sq.mm", _A));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX = AX1 + AX2 + AX3"));
            //list.Add(string.Format("   = {0} + {1} + {2} ", ls_AX[0], ls_AX[1], ls_AX[2]));
            //list.Add(string.Format("   = {0} cu.mm", _AX));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX = AXX1 + AXX2 + AXX3 " ));
            //list.Add(string.Format("    = {0} + {1} + {2} ", ls_AXX[0], ls_AXX[1], ls_AXX[2]));
            //list.Add(string.Format("    = {0} sq.sq.mm", _AXX));
            //list.Add(string.Format(""));

            //double Yb = _AX / _A;
            //list.Add(string.Format("Yb = _AX / _A = {0} / {1} = {2}  mm", _AX, _A, Yb));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //double Yt = D - Yb;
            //list.Add(string.Format("Yt = D - Yb = {0} - {1} = {2}  mm", D, Yb, Yt));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Xu = Yt = {0}  mm", Yt));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("ISelf =  ISelf +  ISelf2 + ISelf3"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("      = {0} + {1} + {2} ", ls_ISelf[0], ls_ISelf[1], ls_ISelf[2]));
            //list.Add(string.Format("      = {0} sq.mm", _ISelf));


            #endregion Chiranjit [2013 07 14]

            double strain_steel = (d - Xu) / Xu * Lc;
         
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("From the strain diagram limiting conc strain is = Lc = {0}", Lc));
            list.Add(string.Format(""));
            list.Add(string.Format("Strain in steel = (d - Xu) / Xu * Lc = {0:f5}", strain_steel));
            list.Add(string.Format("                = ({0} - {1}) / {1} * {2} = {0:f5}", d, Xu, Lc));
            list.Add(string.Format("                = {0:f5}", strain_steel));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double X = 1 / (fyk / (gama_s * 2 * Math.Pow(10, 5) * Lc) + 1);
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("From the strain diagram  = 1/ (fyk / (γs x 2 x 10^5 x Lc) + 1)"));
            list.Add(string.Format(""));
            list.Add(string.Format("                         = 1/ ({0} / ({1}x 2 x 10^5 x {2}) + 1)", fyk, gama_s, Lc));
            list.Add(string.Format(""));
            list.Add(string.Format("                         = {0:f3} ", X));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------------------------------------"));
            list.Add(string.Format("STEP 2 : Calculations of force and moment  capacity of concrete"));
            list.Add(string.Format("---------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            List<double> lst_area = new List<double>();
            List<double> lst_CG_top = new List<double>();
            List<double> lst_Fc = new List<double>();
            List<double> lst_moment = new List<double>();

            double deck_slab_area = 0.0;
            if (Lamda * Xu < Ds)
                deck_slab_area = Lamda * Bs * Xu;
            else
                deck_slab_area = Bs * Ds;

            lst_area.Add(deck_slab_area);

            double top_flange_area = 0.0;

            //if (Lamda * Xu < Ds) top_flange_area = 0.0;
            //else if (Lamda * Xu < (Ds + D1))
            //    top_flange_area = ((Xu - Ds) * Bs);
            //else
            //    top_flange_area = D1 * Bs;

            //lst_area.Add(top_flange_area);
            //double haunch_area = 0.0;

            //if (Lamda * Xu < Ds) haunch_area = 0.0;
            //else if (Lamda * Xu < (Ds + D1)) haunch_area = 0.0;
            //else if (Lamda * Xu < (D1 + Ds + D2)) haunch_area = (0.5 * (Bs + (1 - (Lamda * Xu - Ds - D1) / D2)) * (Xu - Ds - D1));
            //else haunch_area = (0.5 * (Bs + bw) * D2);


            //lst_area.Add(haunch_area);

            double web_area = 0.0;

            if (Lamda * Xu > (Ds)) web_area = (Lamda * Xu - Ds) * bw;
            else web_area = 0;

            lst_area.Add(web_area);

            //CG from Top
            double deck_CG_top = 0.0;
            if (Lamda * Xu < Ds) deck_CG_top = Lamda * Xu / 2.0;
            else deck_CG_top = Ds / 2;
            lst_CG_top.Add(deck_CG_top);

            double top_flange_CG_top = 0.0;

            if (Lamda * Xu < Ds) top_flange_CG_top = 0.0;
            else if (Lamda * Xu < (Ds)) top_flange_CG_top = (((Lamda * Xu - Ds) / 2 + Ds));
            else top_flange_CG_top = (Ds);
            lst_CG_top.Add(top_flange_CG_top);



            //double haunch_CG_top = 0.0;

            //if (Lamda * Xu < Ds) haunch_CG_top = 0.0;
            //else if (Lamda * Xu < (Ds)) haunch_CG_top = 0.0;
            //else if (Lamda * Xu < (Ds)) haunch_CG_top = (Ds + D1 + (Lamda * Xu - Ds - D1) / 3 * ((2 * bw + Bs) / (bw + Bs)));
            //else haunch_CG_top = (Ds + ((2 * bw + Bs) / (bw + Bs)));

            //lst_CG_top.Add(haunch_CG_top);


            double web_CG_top = 0.0;
            if (Lamda * Xu > (Ds)) web_CG_top = ((Ds) + (Lamda * Xu - Ds) / 2);
            else web_CG_top = 0.0;
            lst_CG_top.Add(web_CG_top);

            double Fc_deck = 0.0;
            double Fc_top_flange = 0.0;
            double Fc_haunch = 0.0;
            double Fc_web = 0.0;

            Fc_deck = Eta * deck_slab_area * fcd;
            Fc_top_flange = Eta * top_flange_area * fcd;
            //Fc_haunch = Eta * haunch_area * fcd;
            Fc_web = Eta * web_area * fcd;



            for (int i = 0; i < lst_area.Count; i++)
            {
                lst_Fc.Add(Eta * lst_area[i] * fcd / 1000);
            }

            for (int i = 0; i < lst_Fc.Count; i++)
            {
                lst_moment.Add(lst_CG_top[i] * lst_Fc[i] / 1000);
            }

            list.Add(string.Format(""));

            List<string> lst_section = new List<string>();

            lst_section.Add("Deck slab");
            //lst_section.Add("Top flange (D1)");
            //lst_section.Add("Haunch(D2)");
            lst_section.Add("Web ");









            format = "{0,-15} {1,14:f3} {2,16:f3} {3,14:f3} {4,14:f3}";

            list.Add(string.Format("---------------------------------------------------------------------------------------"));
            list.Add(string.Format(format, "  Section  ", "Area", "Cg from top", "Fc=nFcd*A", "Moment=Fcd*Cg"));
            list.Add(string.Format(format, " ", "(mm^2)", "(mm)", "(Kn)", "(KN-m)"));
            list.Add(string.Format("---------------------------------------------------------------------------------------"));


            double total_sec_area = 0.0;
            double total_Fc = 0.0;
            double total_Moment = 0.0;
            for (int i = 0; i < lst_section.Count; i++)
            {
                list.Add(string.Format(format, lst_section[i],
                    lst_area[i],
                    lst_CG_top[i],
                    lst_Fc[i],
                    lst_moment[i]));

                total_sec_area += lst_area[i];
                total_Fc += lst_Fc[i];
                total_Moment += lst_moment[i];
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------"));


            list.Add(string.Format(format, "Total",
                total_sec_area,
                "",
                total_Fc,
                total_Moment));
            list.Add(string.Format("---------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("STEP 3 : Calculations of force and moment capacity for steel"));
            list.Add(string.Format("----------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

          
            double Fs = fyk / gama_s * total_area / 1000;
            list.Add(string.Format("Fs= fyk / γs x As /1000"));
            list.Add(string.Format("  = {0} / {1} x {2:f3} /1000", fyk, gama_s, total_area));
            list.Add(string.Format("  = {0:f2}", Fs));

            list.Add(string.Format(""));

            double fib_moment = Fs * d / 1000;

            list.Add(string.Format("Moment about top fibre = Fs * d / 1000 = {0:f3} x {1} / 1000 = {2:f3}", Fs, d, fib_moment));
            list.Add(string.Format(""));
            list.Add(string.Format("Comparision of total compression = total tension = FC= FT"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double diffe = Fs - total_Fc;

            list.Add(string.Format("Difference in compression and tension which"));
            list.Add(string.Format("has to be be Zero as mentioned above FC = FT"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            list.Add(string.Format("Taking moments about top of fibre gives moment of resistance = Mrd"));
            list.Add(string.Format(""));

            double Mrd = fib_moment - total_Moment;
            if (Mrd > BM)
                list.Add(string.Format("Mrd = {0:f3} - {1:f3} = {2:f4} kn-m ,  OK", fib_moment, total_Moment, Mrd));
            else
                list.Add(string.Format("Mrd = {0:f3} - {1:f3} = {2:f4} kn-m ,  NOT OK, Increase The Reinforcement", fib_moment, total_Moment, Mrd));


            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format("STEP 4 : SHEAR CHECK {0}", (iApp.DesignStandard == eDesignStandard.IndianStandard ? "AS PER IRC-112" : "")));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format(""));



            
            double Ved = SF;






            list.Add(string.Format("VED  is ultimate design shear force in the section considered resulting from external loading = {0:f3} KN", Ved));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("VRd,c  is the design shear reinforcement of the member without shear reinforcement"));
            list.Add(string.Format(""));
            list.Add(string.Format("        (0.12 x k x(80rl fck)^0.33+ 0.15 σ_cp) x bw x d   {0}", (iApp.DesignStandard == eDesignStandard.IndianStandard ? "[IRC 112,  Clause 10.3.2]" : "")));

            list.Add(string.Format(""));
            list.Add(string.Format("        subject to a minimum of (νmin + 0.15 σcp) x bw x d        "));
            list.Add(string.Format(""));
            list.Add(string.Format("Where"));
            list.Add(string.Format(""));

            double K = 1 + Math.Sqrt(200 / d);
            list.Add(string.Format("K  = 1 + sqrt(200/d)"));
            list.Add(string.Format("   = 1 + sqrt(200/{0})", d));
            if (K <= 2.0)
                list.Add(string.Format("   = {0:f3} <= 2.0 Hence OK ", K));
            else
                list.Add(string.Format("   = {0:f3} > 2.0 NOT OK ", K));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("fck is the concrete cylinder strength in MPA = M{0} Mpa", fck));
            list.Add(string.Format(""));

            double Asl = total_area;

            //bw = bw * 1000;


            double r = Asl / bw/ d;

            list.Add(string.Format("r = Asl / (bw x d) "));
            list.Add(string.Format("  = {0:f3} / ({1} x {0:f3})", Asl, bw, d));
            list.Add(string.Format("  = {0:f3}", r));
            list.Add(string.Format(""));

            if (r > 0.02)
            {
                list.Add(string.Format("  = {0:f3} > 0.02 = 0.02", r));
                r = 0.02;
            }

            list.Add(string.Format(""));


            list.Add(string.Format("Asl = the area of longitudinal tensile reinforcement which extends a minimum of design = {0:f3} mm^2 ", Asl));
            list.Add(string.Format("        anchorage length and an effective depth beyond the section considered      "));
            list.Add(string.Format(""));
            list.Add(string.Format("bw  = the smallest width of cross-section in the tensile zone = {0:f3} mm ", bw));
            list.Add(string.Format(""));
            list.Add(string.Format("d   = effective depth of tensile reinforcement = {0:f3} mm", d));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("NED  = the axial force in the cross-section from the loading = 0"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ac = the gross concrete cross-section area = Not Applicable "));
            list.Add(string.Format(""));
            list.Add(string.Format("σcp  = NED/Ac (in MPA) = 0"));

            double vmin = 0.0;

            vmin = 0.031 * Math.Pow(K, 1.5) * Math.Pow(fck, 0.5);
            list.Add(string.Format("νmin  = 0.031 x K ^(3/2) x fck ^ (1/2)"));
            list.Add(string.Format("      = 0.031 x {0:f3} ^(3/2) x {1:f3}^(1/2)", K, fck));
            list.Add(string.Format("      = {0:f4}", vmin));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double chk_val = 0.0;
            double sigma_cp = 0.0;
            chk_val = (vmin + 0.15 * sigma_cp) * bw * d / 1000;

            list.Add(string.Format("   (νmin + 0.15 x σ_cp) x bw x d / 1000"));
            list.Add(string.Format(" = ({0:f3} + 0.15 x 0) x {1:f3} x {2:f3} / 1000", vmin, bw, d));
            list.Add(string.Format(" = {0:f3} kN ", chk_val));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(""));

            double VRdc = Math.Max((0.12 * K * Math.Pow((80 * r * fck), 0.33) * bw * d / 1000), chk_val);


            list.Add(string.Format(""));
            //list.Add(string.Format("Thus ignoring any axial load = VRdc  = {0:f3} KN ", VRdc));
            list.Add(string.Format("Thus ignoring any axial load = VRdc"));
            list.Add(string.Format(""));
            list.Add(string.Format("VRdc = (0.12*K*(80*p*fck)^0.33)*bw*d/1000"));
            list.Add(string.Format(""));
            list.Add(string.Format("     = (0.12*{0:f3}*(80*{1:f3}*{2})^0.33)*{3}*{4}/1000", K, r, fck, bw, d));
            list.Add(string.Format(""));
            list.Add(string.Format("     = {0:f3} KN ", VRdc));
            list.Add(string.Format(""));

            if (VRdc < Ved)
            {
                list.Add(string.Format("     = {0:f3} kN < {1:f3} kN,   Design shear reinforcement is required", VRdc, Ved));
                //list.Add(string.Format("Design shear reinforcement is required"));
            }
            else
            {
                list.Add(string.Format("     = {0:f3} kN > {1:f3} kN,   Shear reinforcement is not required", VRdc, Ved));
                //list.Add(string.Format("Shear reinforcement is not required"));
            }

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double theta = 45;

            list.Add(string.Format("Try vertical links where α = {0} and α strut angle θ of {1}° :", Alpha, theta));
            list.Add(string.Format(""));
            list.Add(string.Format("α_cw    is a co-efficient taking in to account of the state of the stress in the compressive chord = {0}", alpha_cw));
            list.Add(string.Format("        where σcp = {0}", sigma_cp));
            list.Add(string.Format(""));
            list.Add(string.Format("        σcp = is the mean compressive stress, measured positive in the concrete"));
            list.Add(string.Format("              due to the design axial force"));
            list.Add(string.Format(""));

            

            list.Add(string.Format("V1      is the strength reduction factor for concrete cracked in shear = {0:f3}", V1));
            list.Add(string.Format(""));
            list.Add(string.Format("        V1 = 0.6                for fck <= 80 MPa"));
            list.Add(string.Format(""));
            list.Add(string.Format("        V1 = 0.9-fck/250        for fck => 80 MPa"));
            list.Add(string.Format(""));
            double Z = 0.9 * d;
            list.Add(string.Format("Z   =  lever arm can be taken as 0.9d for R.C . Section and to be calculated for PSC section"));
            list.Add(string.Format("    =  0.9 x {0}", d));
            list.Add(string.Format("    =  {0:f3} mm ", Z));
            list.Add(string.Format(""));
            list.Add(string.Format("fcd   =  {0:f3} Mpa", fcd));
            list.Add(string.Format(""));
            //theta =(0.5*(ASIN((2*J72*1000)/(J101*J113*J108*J105*J110))))*(180/PI())
            //chk_val = ((2 * Ved * 1000) / (alpha_cw * bw*1000 * Z * V1 * fcd));
            theta = (0.5 * (Math.Asin((2 * Ved * 1000) / (alpha_cw * bw * Z * V1 * fcd)))) * (180 / Math.PI);
            //list.Add(string.Format("θ  = 0.5 * sin⁻¹(2*VNS/(α_cw* bw*z*v1*fcd))        ( IRC 112:2011, Clause 10.3.3.1) =        2.993 o        cotq        =        2.50        "));
            list.Add(string.Format("θ  = 0.5 * sin⁻¹(2 x Ved x 1000/(α_cw x bw x Z x V1 x fcd))      {0}", (iApp.DesignStandard == eDesignStandard.IndianStandard) ? "( IRC 112:2011, Clause 10.3.3.1)" : ""));
            list.Add(string.Format("   = 0.5 * sin⁻¹(2 x {0} x 1000/({1} x {2} x {3} x {4} x {5})) ", Ved, alpha_cw, bw, Z, V1, fcd));
            list.Add(string.Format("   = {0:f3}° (deg)", theta));
            list.Add(string.Format(""));



            //list.Add(string.Format("θ  = 0.5 * sin⁻¹(2*VNS/(α_cw* bw*z*v1*fcd))        ( IRC 112:2011, Clause 10.3.3.1) =        2.993 o        cotq        =        2.50        "));

            if (theta < 21.8)
            {
                theta = 21.8;
                list.Add(string.Format("θ < 21.8"));
                list.Add(string.Format("θ  = {0}°                         {1}", theta, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "(As per IRC 112:2011 Clause 10.2.2.2)" : "")));
            }
            else if (theta > 45)
            {
                theta = 45.0;
                list.Add(string.Format("θ > 45"));
                list.Add(string.Format("θ  = {0}°                         {1}", theta, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "(As per IRC 112:2011 Clause 10.2.2.2)" : "")));
            }
            list.Add(string.Format(""));

            double tan_theta = Math.Tan(theta * Math.PI / 180);
            double cot_theta = 1 / tan_theta;
            list.Add(string.Format("cot θ = cot {0:f3} = {1:f3}", theta, (1 / tan_theta)));
            list.Add(string.Format(""));
            list.Add(string.Format("tan θ = tan {0:f3} = {1:f3}", theta, tan_theta));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            //list.Add(string.Format("        =        21.80        o (As per clause 10.2.2.2)        tan θ = 0.40 "));
            //list.Add(string.Format("bw        =        300        mm        "));
            double VRD_max = alpha_cw * bw * Z * V1 * fcd / (cot_theta + tan_theta) / 1000;
            list.Add(string.Format("VRD_max = design value of maximum shear force"));
            list.Add(string.Format("        = (αcw x bw x Z x V1 x fcd / (cotθ+tanθ))/1000"));
            list.Add(string.Format("        = ({0} x {1} x {2} x {3} x {4} / ({5:f3} + {6:f3}))/1000", alpha_cw, bw, Z, V1, fcd, cot_theta, tan_theta));

            if (VRD_max > Ved)
                list.Add(string.Format("        = {0:f3} kN > {1:f3} kN,   HENCE OK", VRD_max, Ved));
            else
                list.Add(string.Format("        = {0:f3} kN < {1:f3} kN,   NOTOK", VRD_max, Ved));

            list.Add(string.Format(""));


            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 5 : CHECKING OF SHEAR REINFORCEMENT  {0}", (iApp.DesignStandard == eDesignStandard.IndianStandard ? " IRC 112:2011 Clause 10.3.3.2" : "")));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Asw / s  >=  VRds / (Z x fywd x cotθ)"));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            double VRds = Ved;
            list.Add(string.Format("VRds  = design value of the shear force which can be sustained by the yielding reinforcement = {0} kN", VRds));
            list.Add(string.Format("      "));

            double fywd = 0.8 * fyk / gama_s;
            list.Add(string.Format("fywd  = is the design yeild strength of the shear reinforcement "));
            list.Add(string.Format("      = 0.8 * fyk / γs "));
            list.Add(string.Format("      = 0.8 x {0} / {1} ", fyk, gama_s));
            list.Add(string.Format("      = {0:f3} Mpa", fywd));
            list.Add(string.Format(""));

            //double val = VRds / Z / fywd / cot_theta * 1000;
            double val = (VRds*1000) / (Z * fywd * cot_theta);


            list.Add(string.Format(" VRds / (Z x fywd x cotθ) = ({0} x 10^3) / ({1:f3} x {2:f3} x {3:f3}) = {4:f3} mm^2/mm", VRds, Z, fywd, cot_theta, val));
            list.Add(string.Format(""));

            short provide_layer = 2;
            double provide_bar_dia = 12.0;
            double s = 200.0;

            double Asw = provide_layer * Math.PI / 4 * provide_bar_dia * provide_bar_dia;

            //list.Add(string.Format("zfywd cotq      "));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide  {0} L  legged {1} φ   @ {2} mm c/c", provide_layer, provide_bar_dia, s));


            list.Add(string.Format(""));
            list.Add(string.Format("Asw  provided  = {0:f3} mm^2/m", Asw));
            list.Add(string.Format(""));
            list.Add(string.Format("Asw / s = {0:f3} / {1:f3} = {2:f3} mm^2/mm", Asw, s, (Asw / s)));
            list.Add(string.Format(""));
            double chk_rho = ((Asw / s)) / (bw * Math.Sin(90.0 * Math.PI / 180));
            list.Add(string.Format("ρ = ((Asw / s)) / (bw x Sin(90))"));
            list.Add(string.Format("  = (({0:f3} / {1})) / ({2} x Sin(90))", Asw, s, bw));
            list.Add(string.Format("  = {0:f6}", chk_rho));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("ρmin  = 0.072 x √fck / fyk"));
            list.Add(string.Format("      = 0.072 x √{0} / {1}", fck, fyk));

            double rho_min = 0.072 * Math.Sqrt(fck) / fyk;



            if (rho_min < chk_rho)
                list.Add(string.Format("      = {0:f6} < {1:f6} Hence OK", rho_min, chk_rho));
            else
                list.Add(string.Format("      = {0:f6} > {1:f6} NOT OK", rho_min, chk_rho));

            list.Add(string.Format(""));




            #region Chiranjit [2013 07 17]
            list.Add(string.Format(""));



            lst_area.Clear();
            List<double> lst_y = new List<double>();
            List<double> lst_Ay = new List<double>();
            List<double> lst_Ayy = new List<double>();
            List<double> lst_I0 = new List<double>();


            //Xu = 309.0;

            //Xu = (d - ((Bs * Ds) / (total_area * m) + (bw * Ds) / (total_area * m))) / (1 - (bw / (total_area * m)));

            if (Xu < Ds)
                _a = Xu * Bs;
            else
                _a = Ds * Bs;


            double _y = 0.0;
            if (Xu < Ds)
                _y = Xu / 2.0;
            else
                _y = Xu - (Ds / 2);



            double I0 = 0.0;
            if (Xu < Ds)
                I0 = Bs * Xu * Xu * Xu / 12.0;
            else
                I0 = Bs * Ds * Ds * Ds / 12.0;

            lst_area.Add(_a);
            lst_y.Add(_y);

            lst_Ay.Add(_a * _y);
            lst_Ayy.Add(_a * _y * _y);

            lst_I0.Add(I0);


            //Top Flange Area
            //if (Xu < Ds) _a = 0;
            //else if (Xu < Ds) _a = (Xu - Ds);
            //else _a = Bs;



            //Top Flange y 
            //if (Xu < Ds)
            //    _y = 0;
            //else if (Xu < (Ds))
            //    _y = (Xu - Ds) / 2;
            //else _y = Xu - Ds;



            //if (Xu < Ds) I0 = 0;
            //else if (Xu < (Ds)) I0 = Bs + Math.Pow((Xu - Ds), 3) / 12;
            //else
            //    I0 = Xu * Math.Pow(D1, 3) / 12;


            //lst_area.Add(_a);
            //lst_y.Add(_y);

            //lst_Ay.Add(_a * _y);
            //lst_Ayy.Add(_a * _y * _y);
            //lst_I0.Add(I0);

            //if(Xu < (T +  D1)) = Bs + Math.Pow((Xu-T),3)/12;
            //+))


            //Web Plate Area
            if (Xu < Ds) _a = 0.0;
            //else if (Xu < Ds + D1) _a = 0.0;
            //else if (Xu < Ds + D1 + D2) _a = 0;
            else _a = bw * (Xu - Ds);


            double va = bw * Ds;




            if (Xu < Ds) _y = 0.0;

            //else if (Xu < Ds) _y = 0.0;

            //else if (Xu < Ds + D1 + D2) _y = 0;

            else _y = ((Xu  - Ds) / 2);


            if (Xu < Ds) I0 = 0.0;
            //else if (Xu < Ds + D1) I0 = 0.0;
            //else if (Xu < Ds + D1 + D2) I0 = 0.0;
            else I0 = Math.Pow((bw * (Xu - Ds)), 3.0) / 12.0;



            lst_area.Add(_a);
            lst_y.Add(_y);

            lst_Ay.Add(_a * _y);
            lst_Ayy.Add(_a * _y * _y);
            lst_I0.Add(I0);





            format = "{0,15} {1,15:f3} {2,12:f3} {3,15:E3} {4,15:E3} {5,15:E3}";
            list.Add(string.Format("---------------------------------------------------------------------------------------------"));
            list.Add(string.Format(format, "Section", "Area(mm^2)", "y    ", "A*y   ", "A*y^2   ", "I0     "));





            list.Add(string.Format("---------------------------------------------------------------------------------------------"));
            for (int i = 0; i < lst_section.Count; i++)
            {
                list.Add(string.Format(format,
                    lst_section[i],
                    lst_area[i],
                    lst_y[i],
                    lst_Ay[i],
                    lst_Ayy[i],
                    lst_I0[i]));
            
                    
            }
            _a = MyList.Get_Array_Sum(lst_area);
            double _Ay = MyList.Get_Array_Sum(lst_Ay);
            double _Ayy = MyList.Get_Array_Sum(lst_Ayy);
            I0 = MyList.Get_Array_Sum(lst_I0);

            list.Add(string.Format("---------------------------------------------------------------------------------------------"));


            list.Add(string.Format(format,
                "Total",
                _a,
                "",
                _Ay,
                _Ayy,
                I0));
            list.Add(string.Format("---------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("STEP 6 : STRESS CHECKING & CRACK WIDTH CHECKING DURING SLS CONDITION {0}", (iApp.DesignStandard == eDesignStandard.IndianStandard ? "As per IRC-112:2011, Clause-12.3.4" : "")));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format("PERMISSIBLE STRESSES"));
            list.Add(string.Format("--------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double perm_strs_conc = fck * 0.48;
            double perm_strs_stl = fyk * 0.8;

            list.Add(string.Format("In concrete = fck x 0.48 = {0} x 0.48 = {1:f3} Mpa ", fck, perm_strs_conc));
            list.Add(string.Format(""));
            list.Add(string.Format("In steel = fyk x 0.8 = {0} x 0.8  =  {1:f3} Mpa ", fck, perm_strs_stl));
            list.Add(string.Format(""));

            list.Add(string.Format("Area of beam above neutral axis  = {0:f3} sq.mm ", _a));
            list.Add(string.Format(""));

            total_cg = _Ay / _a;

            list.Add(string.Format("CG of beam above neutral axis   = Ay / A = {0:f3} / {1:f3} = {2:f3} mm ", _Ay, _a, total_cg));

            list.Add(string.Format(""));

            list.Add(string.Format("Depth of Neutral Axis = N = Xu = {0:f3} mm", Xu));
         


            list.Add(string.Format("Comparing moment of area about neutral axis    "));
            list.Add(string.Format(""));
            //list.Add(string.Format("W*T+WTF*T1*(N-T1*0.5)+WTF*T2*(N-2*T2*2/3)+(N-T1-T2)^2/2*WW  = m*Ast*(d-N)"));
            list.Add(string.Format("Bs * Ds + (N^2/2*Bw) = m*Ast*(d-N)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double N = Xu;
            double steel_moi = m * Ast * (d - N) * (d - N);



            list.Add(string.Format("Moment of Inertia of steel = m*Ast*(d-N)^2"));
            list.Add(string.Format("                           = {0:f3}*{1:f3}*({2:f3}-{3:f3})^2", m, Ast, d, N));
            list.Add(string.Format("                           = {0:E3} mm^4", steel_moi));
            list.Add(string.Format(""));

            double crk_moi = _Ayy + I0 + steel_moi;
            list.Add(string.Format("Cracked Moment of Inertia  = {0:E3} + {1:E3} + {2:E3} = {3:E3} mm^4 ", _Ayy, I0, steel_moi, crk_moi));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double Zt = crk_moi / Xu;

            list.Add(string.Format("Top Section Modulus at top of deck  = Zt = {0:E3}/{1:f3} = {2:E3} mm^3 ", crk_moi, Xu, Zt));
            list.Add(string.Format(""));
            double Zb = crk_moi / (d - Xu);
            list.Add(string.Format("Bottom Section Modulus  = Zt =  {0:E3}/({1:f3}-{2:f3}) = {3:E3} mm^3  ", crk_moi, N, Xu, Zb));
            list.Add(string.Format(""));


            //BM = 2956.0;

            //chk_val = (BM * 10 * 1000) / (Zt * 1000);
            chk_val = (BM *  1000  /  Zt * 1000);

            list.Add(string.Format("Stress in concrete at top of deck slab = (BM *  1000  /  Zt * 1000)"));
            list.Add(string.Format("                                       = ({0} *  1000  /  {1:E3} * 1000)", BM, Zt));
            list.Add(string.Format(""));

            if (chk_val < perm_strs_conc)
                list.Add(string.Format("                                       = {0:f3} Mpa < {1:f3} Mpa, HENCE OK ", chk_val, perm_strs_conc));
            else
                list.Add(string.Format("                                       = {0:f3} Mpa > {1:f3} Mpa, NOT OK ", chk_val, perm_strs_conc));



            chk_val = BM * 10000000 / Zb;
            list.Add(string.Format("Stress in Steel at CG  =  BM x 10^6 / Zb"));
            list.Add(string.Format("                       =  {0:f3} x 10^6 / {1:E3}", BM, Zb));
            if (chk_val < perm_strs_stl)
                list.Add(string.Format("                       =  {0:f3} Mpa < {1:f3} Mpa, HENCE OK ", chk_val, perm_strs_stl));
            else
                list.Add(string.Format("                       =  {0:f3} Mpa > {1:f3} Mpa, NOT OK ", chk_val, perm_strs_stl));

            list.Add(string.Format(""));

            bar_dia = SteelLayers[0].Bar_Dia;
            double sigma_sc = chk_val * ((D - cov - dia - bar_dia / 2) - Xu) / (d - Xu);
             

            list.Add(string.Format("Stress in Steel at bottom fibre = {0:f3} * ((D - cov - dia - {1} / 2) - Xu) / (d - Xu)", chk_val, bar_dia));
            list.Add(string.Format("                                = {0:f3} * (({1} - {2} - {3} - {4} / 2) - {5}) / ({6} - {5})", chk_val, bar_dia, cov, dia, bar_dia, Xu, d));
            if (sigma_sc < perm_strs_stl)
                list.Add(string.Format("                                = {0:f3} Mpa  < {1:f3} Mpa,  HENCE OK", sigma_sc, perm_strs_stl));
            else
                list.Add(string.Format("                                = {0:f3} Mpa  > {1:f3} Mpa,  NOT OK", sigma_sc, perm_strs_stl));

            list.Add(string.Format(""));





            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format("STEP 7 : MAXIMUM CRACK WIDTH"));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format(" "));
            double phi = bar_dia;

            double spacing = 5 * (cov + phi / 2.0);

            double alpha_e = Es / Ecm * 2;

            double val1 = (D - Xu) / 3.0;
            double val2 = D / 2;
            double val3 = 2.5 * (D - d);

            double Ac_eff = bw * Math.Min(val1, Math.Min(val2, val3));
            double Pp_eff = Asl / Ac_eff;
            double h = D;
            double fct_eff = iApp.Tables.Limit_State_Crack(fck);

            double sigma_sm = Math.Max(0.6 * sigma_sc / Es, (sigma_sc - (0.5 * fct_eff / Pp_eff * (1 + alpha_e * Pp_eff))) / Es);


            double sr_max = 3.4 * cov + (0.425 * K1 * K2 * bar_dia) / Pp_eff;
            double Wk = sr_max * sigma_sm;

            
            //=IF(Wk<0.3,"Hence OK"," NOT OK")

            list.Add(string.Format(""));


            list.Add(string.Format("(∑sm-∑cm)  = ((σ_sc - kt x  fct_eff/Pp_eff (1+ α_e x Pp_eff)) / Es) >= 0.6 σ_sc / Es ", sigma_sm));
            list.Add(string.Format(""));

            list.Add(string.Format( "where spacing of bonded reinforcement with in the tension zone <= 5 x (C + φ/2) = 5 x ({0} + {1}/2) =  {1} mm  ",cov, phi, spacing));
            list.Add(string.Format(" "));
            list.Add(string.Format(" φ  = the bar diameter.Where different diameters are used", phi));
            list.Add(string.Format("      in section an equivalent diameter canbe used φ_eq"));
            list.Add(string.Format(""));
            list.Add(string.Format("    = {0} mm", phi));
            list.Add(string.Format(" "));
            list.Add(string.Format(" C = Clear cover to longitudinal reinforcement = {0} mm", cov));
            list.Add(string.Format(" "));
            list.Add(string.Format(" K1 = the co-efficient which takes account of the bond properties of the bonded reinforcement  = {0}", K1));
            list.Add(string.Format(" "));
            list.Add(string.Format(" K1 = the co-efficient which takes account of the bond properties  = {0}", K1));
            list.Add(string.Format(" "));
            list.Add(string.Format(" K2 = is a co-fficient which takes into account of the distribution of strain = {0}", K2));
            list.Add(string.Format(" "));
            list.Add(string.Format(" σ_sc  = is the stress in the tension reinforcement assuming a cracked section = {0:f3} Mpa ", sigma_sc));
            list.Add(string.Format(" "));


            list.Add(string.Format(" α_e = is the ratio Es/Ec x 2 = {0:f3}", alpha_e));
            list.Add(string.Format(" "));


            list.Add(string.Format(" Pp_eff  = As/Ac_eff = {0:f3} / {1:f3} = {2:f3}", Asl, Ac_eff, Pp_eff));
            list.Add(string.Format(" "));


            list.Add(string.Format(" Ac_eff = the effective area of concrete in tension surrounding the reinforcement of depth hc_eff ,", Ac_eff));
            list.Add(string.Format(" "));
            list.Add(string.Format("          where hc_eff is the lesser of following     "));
            list.Add(string.Format(" "));
            //list.Add(string.Format("        = {0:f3} mm^2", Ac_eff));
            list.Add(string.Format(" "));
            list.Add(string.Format(" "));
            list.Add(string.Format(" "));
            b = bw;
            list.Add(string.Format("   b = {0} mm ", bw));
            list.Add(string.Format(" "));


            list.Add(string.Format("   h = D = {0} mm", h));
            list.Add(string.Format(" "));
            list.Add(string.Format(" "));

            list.Add(string.Format("Case 1 :  One third of the tension zone depth of the cracked section = (h-Xu)/3 = ({0:f3} - {1:f3})/3 = {2:f3}", h, Xu, val1));
            list.Add(string.Format(" "));
            //list.Add(string.Format("   with x negative when whole section is in tension d = {0} mm  ", d));
            list.Add(string.Format(""));
            //list.Add(string.Format("   x = {0} mm  ", Xu));
            list.Add(string.Format(" "));
            list.Add(string.Format("Case 2 :  half of section depth, D/2 = {0}/2 = {1}  ",D,  val2));
            list.Add(string.Format(" "));
            list.Add(string.Format("Case 3 :   2.5 x (D-d) = 2.5 x ({0}-{1}) = {2} ", D, d, val3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double hc_eff = Math.Min(val1, Math.Min(val2, val3));

            list.Add(string.Format("Ac_eff = bw x hc_eff = {0:f3} x {1:f3} = {2:f3} mm^2", bw, hc_eff, Ac_eff));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format(" As = Area of tension reinforcement = {0:f3} mm^2  ", Asl));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("  fct_eff = the mean value of the tensile strength of the concrete effective"));
            list.Add(string.Format("            at the time when the cracks may first be expected to occur"));
            list.Add(string.Format(""));
            list.Add(string.Format("          = {0:f3} Mpa  =  fctm (from Table 1)", fct_eff));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format(" "));
            list.Add(string.Format(" Sr_max = Maximum crack spacing"));
            list.Add(string.Format(""));
            list.Add(string.Format("        = 3.4c + (0.425 x k1 x k2 x φ) / Pp_eff ", sr_max));
            list.Add(string.Format(""));
            list.Add(string.Format("        = 3.4c + (0.425 x {0} x {1} x {2}) / {3:f3}", K1, K2, bar_dia, Pp_eff));
            list.Add(string.Format(""));
            list.Add(string.Format("        = {0:f3} mm  ", sr_max));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            sigma_sm = Math.Max(0.6 * sigma_sc / Es, (sigma_sc - (0.5 * fct_eff / Pp_eff * (1 + alpha_e * Pp_eff))) / Es);


            list.Add(string.Format(" "));
            list.Add(string.Format("(∑sm-∑cm)  = Maximum Value of the following Conditions"));
            list.Add(string.Format(" "));

            val1 = 0.6 * sigma_sc / Es;
            val2 = (sigma_sc -  (0.5 * fct_eff / Pp_eff * (1 + alpha_e * Pp_eff))) / Es;

            list.Add(string.Format("Condition 1 = 0.6 * σ_sc / Es = 0.6 * {0:f3} / {1:f2} = {2:f6}", sigma_sc, Es, val1));
            list.Add(string.Format(" "));
            list.Add(string.Format("Condition 2 = ((σ_sc - kt x  fct_eff/Pp_eff x (1 + α_e x Pp_eff)) / Es)"));
            list.Add(string.Format("            = (({0:f3} - {1:f3} x  {2:f3} /{3:f4} x (1+ {4:f2} x {3:f3})) / {5:f3})",
                sigma_sc, 0.5, fct_eff, Pp_eff, alpha_e, Es));
            list.Add(string.Format(" "));
            list.Add(string.Format("            = {0:f6}", val2));
            list.Add(string.Format(" "));
            list.Add(string.Format(" "));
            //list.Add(string.Format("(∑sm-∑cm)  = ((σ_sc - kt x  fct_eff/Pp_eff (1+ae x Pp_eff)) / Es) >= 0.6 σ_sc / Es = {0} Hence OK  ", sigma_sm));
            if (sigma_sm > val1)
                list.Add(string.Format("(∑sm-∑cm)  = {0:f6} > {1:f6},   HENCE OK", sigma_sm, val1));
            else
                list.Add(string.Format("(∑sm-∑cm)  = {0:f6} < {1:f6},   NOT OK", sigma_sm, val1));
            
            
            list.Add(string.Format(""));
            list.Add(string.Format(""));
 

            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Crack Width = Wk = Sr_max * (∑sm-∑cm)"));
            list.Add(string.Format("                    = {0:f3} x {1:f6}", sr_max, sigma_sm));

            if (Wk < 0.3)
                list.Add(string.Format("                    = {0:f3} < 0.3,   HENCE OK  ", Wk));
            else
                list.Add(string.Format("                    = {0:f3} > 0.3,   NOT OK  ", Wk));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("----------------------------"));
            list.Add(string.Format("TABLE 1 : CRACK CHECK"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.AddRange(iApp.Tables.Get_Tables_Limit_State_Crack().ToArray());
            list.Add(string.Format(""));

           

            #endregion Chiranjit [2013 07 17]
            return list;
        }

        public List<string> Get_Basic_Data_2013_07_14()
        {
            List<string> list = new List<string>();
            //list.Add(string.Format(""));

            //list.Add(string.Format(""));
            //list.Add(string.Format("-------------------------------------------------------------------"));
            //list.Add(string.Format("USER'S INPUT DATA"));
            //list.Add(string.Format("-------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("1  Grade of concrete = Cgrade = M{0} Mpa ", Cgrade));
            //list.Add(string.Format("2  Grade of reinforcement = Sgrade = Fe {0} Mpa ", Sgrade));
            //list.Add(string.Format("3  Shape Factor λ  = λ = {0} {1}", Lamda, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112-Anexure A2- A2.9(2) " : "")));
            //list.Add(string.Format("4  strength Factor η = η = {0} {1}  ", Eta, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112-Anexure A2- A2.9(2)" : "")));
            //list.Add(string.Format("5  Partial factor of safety (Basic and seismic) = ɣc = {0:f3} {1} ", Gama_c, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112-Cl 6.4.2.8" : "")));

            //list.Add(string.Format("6  Coefficient to consider the influence of the strength = α  = {0} ", Alpha));
            //list.Add(string.Format("7  Design value of concrete comp strength for Basic and seismic = fcd = {0} Mpa ", fcd));
            //list.Add(string.Format("8  Clear cover = cov = {0} mm ", cov));
            //list.Add(string.Format("9  Stress in concrete (compression) = fck = {0} Mpa ", fck));
            //list.Add(string.Format("10 Limiting Strain in concrete = Lc = {0}", Lc));
            //list.Add(string.Format("11 Stress in steel (tension) = fyk = Fe {0} Mpa", fyk));
            //list.Add(string.Format("12 Partial factor of safety for basic and seismic = ϒs = {0} {1} ", gama_s, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 Fig 6.3 (Note)" : "")));
            //list.Add(string.Format("13 Partial factor of safety for Accidental = ϒa = {0} {1} ", gama_a, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 Fig 6.3 (Note)" : "")));

            //list.Add(string.Format("14 Ecm = {0} Mpa ", Ecm));
            //list.Add(string.Format("15 Es = {0} Mpa ", Es));
            //list.Add(string.Format("16 Modular ratio = m = {0:f4} ", m));
            //list.Add(string.Format("17 Diameter of bar to be used for shear stirrups  = dia = {0:f3}", dia));
            //if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            //    list.Add(string.Format("   Constant as per cl. 10.3.1 of IRC 112 α_cw {0} For no axial force ", alpha_cw));

            //list.Add(string.Format("18 Strength reduction factor for concrete cracked in shear V1 {0}  ", V1, (fck < 80 ? "For fck <= 80MPa" : "")));
            //list.Add(string.Format("19 Ratio of the longitudinal force in the new concrete and the total logitudinal force = β = {0}  ", b, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 10.3.4" : "")));
            //list.Add(string.Format("   Factor depends on the roughness of the interface = = µ = {0}  {1}", mu, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 10.3.4" : "")));
            //list.Add(string.Format("20 Angle of the reinforcement to the interface = a = {0}° {1}", a, (iApp.DesignStandard == eDesignStandard.IndianStandard ? "IRC-112 10.3.4" : "")));
            //list.Add(string.Format("21 Creep coefficient = Ce = {0} ", ce));
            //list.Add(string.Format("22 Coefficient of thermal expansion = α_t = {0} (/°C) ", alpha_t));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("---------------------------------------------------------------------------------------"));
            //list.Add(string.Format("Design Check for Longitudinal Girder (Ultimate Limit State ) at L/2 Section"));
            //list.Add(string.Format("---------------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));

            //list.Add(string.Format("---------------------------------------------------------"));
            //string format = "{0,10} {1,14} {2,14} {3,14}";
            //list.Add(string.Format(format, "Layer", "Dia of Bar", "No. of Bars", "CG from bot"));
            //list.Add(string.Format("---------------------------------------------------------"));

            //double total_bars = 0.0;
            //double total_cg = 0.0;

            //double total_area = 0.0;

            //format = "{0,7} {1,12} {2,14} {3,14}";
            //foreach (var item in SteelLayers)
            //{
            //    list.Add(string.Format(format, item.Layer, item.Bar_Dia, item.Bars_No, item.CG_from_bot));
            //    total_bars += item.Bars_No;
            //    total_cg += item.Bars_No * item.CG_from_bot;

            //    total_area += (Math.PI * item.Bar_Dia * item.Bar_Dia / 4) * item.Bars_No;
            //}
            //list.Add(string.Format("---------------------------------------------------------"));
            //total_cg = total_cg / total_bars;
            //list.Add(string.Format(format, "Total", "", total_bars, total_cg));
            //list.Add(string.Format("---------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Area of bottom steel    =   {0:f3} sq.mm", total_area));


            //#region Figure
            //list.Add(@"");
            //list.Add(@"");
            //list.Add(@"     _________________________________________            ");
            //list.Add(@"     |                                    ^  |     ");
            //list.Add(@"     |                  W                 |  |                                      ");
            //list.Add(@"     |_______________________________________|");
            //list.Add(@"           |     |                |  ^    |                                 ");
            //list.Add(@"        D1 |     |<----- Wtf ---->|  |    |                                 ");
            //list.Add(@"           |     |________________|  |    |                                                 ");
            //list.Add(@"          --- --- \              /   |    |                                 ");
            //list.Add(@"               |   \            /    |    |                                 ");
            //list.Add(@"            D2 |    \          /     |    |                                  ");
            //list.Add(@"              ---    |        |      |    |                                 ");
            //list.Add(@"                     |        |      |    |                                  ");
            //list.Add(@"    N                |        |   N  |    |                                 ");
            //list.Add(@"     ---------------------------------------------                           ");
            //list.Add(@"                     |        |           |                                 ");
            //list.Add(@"                     |        |                                              ");
            //list.Add(@"                     |        |           d                                 ");
            //list.Add(@"                     |        |                                              ");
            //list.Add(@"                     |<- bw ->|           |                                 ");
            //list.Add(@"                     |        |           |                                 ");
            //list.Add(@"             ---     |        |           |                                 ");
            //list.Add(@"              |     /          \          |                                   ");
            //list.Add(@"           D3 |    /            \         |                                   ");
            //list.Add(@"              |   /              \        |                                  ");
            //list.Add(@"       ---   --- ------------------       |                                                  ");
            //list.Add(@"        |        |                |       |                                  ");
            //list.Add(@"     D4 |        |         ----------------                                 ");
            //list.Add(@"        |        |________________|                                                         ");
            //list.Add(@"       ---                                                                ");
            //list.Add(@"                 <----- bwf ------>                                      ");
            //list.Add(@"                                                                          ");
            //list.Add(@"                                                                          ");
            //list.Add(@"");
            //#endregion Figure

            ////list.Add(string.Format("Bending Moment  = BM = {0} KN-m ", BM));
            ////list.Add(string.Format("    "));
            ////list.Add(string.Format("Width of Slab  = W  = {0} mm  ", Bs));
            ////list.Add(string.Format("Depth of slab   = T  = {0} mm  ", Ds));
            ////list.Add(string.Format("Width of top flange  =  bf  = {0} mm  ", bf));
            ////list.Add(string.Format("Width of bottom flange  =  bwf  = {0} mm  ", Bb));
            ////list.Add(string.Format("Width of web  =  bw  = {0} mm  ", bw));
            ////list.Add(string.Format("Thickness of top flange  =  D1  = {0} mm  ", D1));
            ////list.Add(string.Format("Thickness of top haunch  =  D2  = {0} mm  ", D2));
            ////list.Add(string.Format("Thickness of bottom haunch  =  D3  = {0} mm  ", D3));
            ////list.Add(string.Format("Thickness of bottom flange  =  D4  = {0} mm  ", D3));
            ////list.Add(string.Format("Depth of girder  =  D  = {0} mm  ", D));
            ////list.Add(string.Format("Effective depth of girder  =  d  = {0} mm  ", d));
            ////list.Add(string.Format("Depth of neutral axis  =  Xu  = {0} mm OK ", Xu));
            ////list.Add(string.Format(""));
            ////list.Add(string.Format(""));




            //double _b = 0;
            //double _d = 0;
            //double _A = 0;
            //double _X = 0;
            //double _AX = 0;
            //double _AXX = 0;
            //double _ISelf = 0;

            //List<double> ls_b = new List<double>();
            //List<double> ls_d = new List<double>();
            //List<double> ls_A = new List<double>();
            //List<double> ls_X = new List<double>();
            //List<double> ls_AX = new List<double>();
            //List<double> ls_AXX = new List<double>();
            //List<double> ls_ISelf = new List<double>();



            //list.Add(string.Format("Section Calculations :"));
            //list.Add(string.Format("----------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Deck Slab : "));
            //list.Add(string.Format("------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("b = W = {0} mm", (_b = Bs)));
            //list.Add(string.Format("d = T = {0} mm", (_d = Ds)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A1 = Area = b x d = {0} x {1} = {2} sq.mm", _b, _d, (_A = _b * _d)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("X1 = Distance from bottom Edge = D - (d/2) = {0} - ({1}/2) = {2} mm", D, _d, (_X = D - (_d / 2))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX1 = A1 x X1 = {0} x {1} = {2} cu.mm", _A, _X, (_AX = _A * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX1 = AX1 x X1 = {0} x {1} = {2} cu.mm", _AX, _X, (_AXX = _AX * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Iself1 = (b x d^3)/12 = ({0} x {1}^3)/12 = {2} cu.mm", _b, _d, (_ISelf = _b * _d * _d * _d / 12.0)));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //ls_b.Add(_b);
            //ls_d.Add(_d);
            //ls_A.Add(_A);
            //ls_X.Add(_X);
            //ls_AX.Add(_AX);
            //ls_AXX.Add(_AXX);
            //ls_ISelf.Add(_ISelf);

            //list.Add(string.Format(""));
            //list.Add(string.Format("Central Vertical Web : "));
            //list.Add(string.Format("-----------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("b = bw = {0} mm", (_b = bw)));
            //list.Add(string.Format("d = D - D1 - T - D4 = {0} - {1} - {2} - {3} = {4} mm", D, D1, Ds, Db, (_d = D - D1 - Ds - Db)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A2 = Area = b x d = {0} x {1} = {2} sq.mm", _b, _d, (_A = _b * _d)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("X2 = Distance from bottom Edge = (d/2) = ({0}/2) = {1} mm", D, _d, (_X = (_d / 2))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX1 = A2 x X2 = {0} x {1} = {2} cu.mm", _A, _X, (_AX = _A * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX1 = AX2 x X2 = {0} x {1} = {2} cu.mm", _AX, _X, (_AXX = _AX * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Iself1 = (b x d^3)/12 = ({0} x {1}^3)/12 = {2} cu.mm", _b, _d, (_ISelf = _b * _d * _d * _d / 12.0)));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //ls_b.Add(_b);
            //ls_d.Add(_d);
            //ls_A.Add(_A);
            //ls_X.Add(_X);
            //ls_AX.Add(_AX);
            //ls_AXX.Add(_AXX);
            //ls_ISelf.Add(_ISelf);



            //list.Add(string.Format(""));
            //list.Add(string.Format("Bottom Flange : "));
            //list.Add(string.Format("----------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("b = bwf = {0} mm", (_b = Bb)));
            //list.Add(string.Format("d = D4 = {0} = {1} mm", Db, (_d = Db)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A3 = Area = b x d = {0} x {1} = {2} sq.mm", _b, _d, (_A = _b * _d)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("X3 = Distance from bottom Edge = (D4/2) = ({0}/2) = {1} mm", Db, _d, (_X = (d / 2))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX3 = A3 x X3 = {0} x {1} = {2} cu.mm", _A, _X, (_AX = _A * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX3 = AX3 x X3 = {0} x {1} = {2} cu.mm", _AX, _X, (_AXX = _AX * _X)));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Iself3 = (b x d^3)/12 = ({0} x {1}^3)/12 = {2} cu.mm", _b, _d, (_ISelf = _b * _d * _d * _d / 12.0)));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //ls_b.Add(_b);
            //ls_d.Add(_d);
            //ls_A.Add(_A);
            //ls_X.Add(_X);
            //ls_AX.Add(_AX);
            //ls_AXX.Add(_AXX);
            //ls_ISelf.Add(_ISelf);


            //_A = MyList.Get_Array_Sum(ls_A);
            //_X = MyList.Get_Array_Sum(ls_X);
            //_AX = MyList.Get_Array_Sum(ls_AX);
            //_AXX = MyList.Get_Array_Sum(ls_AXX);
            //_ISelf = MyList.Get_Array_Sum(ls_ISelf);


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("A = A1 + A2 + A3"));
            //list.Add(string.Format("  = {0} + {1} + {2} ", ls_A[0], ls_A[1], ls_A[2]));
            //list.Add(string.Format("  = {0} sq.mm", _A));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AX = AX1 + AX2 + AX3"));
            //list.Add(string.Format("   = {0} + {1} + {2} ", ls_AX[0], ls_AX[1], ls_AX[2]));
            //list.Add(string.Format("   = {0} cu.mm", _AX));
            //list.Add(string.Format(""));
            //list.Add(string.Format("AXX = AXX1 + AXX2 + AXX3 "));
            //list.Add(string.Format("    = {0} + {1} + {2} ", ls_AXX[0], ls_AXX[1], ls_AXX[2]));
            //list.Add(string.Format("    = {0} sq.sq.mm", _AXX));
            //list.Add(string.Format(""));

            //double Yb = _AX / _A;
            //list.Add(string.Format("Yb = _AX / _A = {0} / {1} = {2}  mm", _AX, _A, Yb));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //double Yt = D - Yb;
            //list.Add(string.Format("Yt = D - Yb = {0} - {1} = {2}  mm", D, Yb, Yt));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Xu = Yt = {0}  mm", Yt));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("ISelf =  ISelf +  ISelf2 + ISelf3"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("      = {0} + {1} + {2} ", ls_ISelf[0], ls_ISelf[1], ls_ISelf[2]));
            //list.Add(string.Format("      = {0} sq.mm", _ISelf));

            //double strain_steel = (d - Xu) / Xu * Lc;

            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("From the strain diagram limiting conc strain is = Lc = {0}", Lc));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Strain in steel = (d - Xu) / Xu * Lc = {0:f5}", strain_steel));
            //list.Add(string.Format("                = ({0} - {1}) / {1} * {2} = {0:f5}", d, Xu, Lc));
            //list.Add(string.Format("                = {0:f5}", strain_steel));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //double X = 1 / (fyk / (gama_s * 2 * Math.Pow(10, 5) * Lc) + 1);
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("From the strain diagram  = 1/ (fyk / (ϒs x 2 x 10^5 x Lc) + 1)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("From the strain diagram  = 1/ (fyk / (ϒs x 2 x 10^5 x Lc) + 1)"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("---------------------------------------------------------------------"));
            //list.Add(string.Format("Calculations of force and moment  capacity of concrete"));
            //list.Add(string.Format("---------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //List<double> lst_area = new List<double>();
            //List<double> lst_CG_top = new List<double>();
            //List<double> lst_Fc = new List<double>();
            //List<double> lst_moment = new List<double>();

            //double deck_slab_area = 0.0;
            //if (Lamda * Xu < Ds)
            //    deck_slab_area = Lamda * Bs * Xu;
            //else
            //    deck_slab_area = Bs * Ds;

            //lst_area.Add(deck_slab_area);

            //double top_flange_area = 0.0;

            //if (Lamda * Xu < Ds) top_flange_area = 0.0;
            //else if (Lamda * Xu < (Ds + D1))
            //    top_flange_area = ((Xu - Ds) * bf);
            //else
            //    top_flange_area = D1 * bf;

            //lst_area.Add(top_flange_area);
            //double haunch_area = 0.0;

            //if (Lamda * Xu < Ds) haunch_area = 0.0;
            //else if (Lamda * Xu < (Ds + D1)) haunch_area = 0.0;
            //else if (Lamda * Xu < (D1 + Ds + D2)) haunch_area = (0.5 * (bf + (1 - (Lamda * Xu - Ds - D1) / D2)) * (Xu - Ds - D1));
            //else haunch_area = (0.5 * (bf + bw) * D2);


            //lst_area.Add(haunch_area);

            //double web_area = 0.0;

            //if (Lamda * Xu > (D1 + D2 + bf)) web_area = (Lamda * Xu - D1 - D2 - bf) * bw;
            //else web_area = 0;

            //lst_area.Add(web_area);

            ////CG from Top
            //double deck_CG_top = 0.0;
            //if (Lamda * Xu < Ds) deck_CG_top = Lamda * Xu / 2.0;
            //else deck_CG_top = Ds / 2;
            //lst_CG_top.Add(deck_CG_top);

            //double top_flange_CG_top = 0.0;

            //if (Lamda * Xu < Ds) top_flange_CG_top = 0.0;
            //else if (Lamda * Xu < (D1 + Ds)) top_flange_CG_top = (((Lamda * Xu - Ds) / 2 + Ds));
            //else top_flange_CG_top = (Ds + D1 / 2);
            //lst_CG_top.Add(top_flange_CG_top);



            //double haunch_CG_top = 0.0;

            //if (Lamda * Xu < Ds) haunch_CG_top = 0.0;
            //else if (Lamda * Xu < (Ds + D1)) haunch_CG_top = 0.0;
            //else if (Lamda * Xu < (D1 + D2 + Ds)) haunch_CG_top = (Ds + D1 + (Lamda * Xu - Ds - D1) / 3 * ((2 * bw + bf) / (bw + bf)));
            //else haunch_CG_top = (Ds + D1 + D2 / 3 * ((2 * bw + bf) / (bw + bf)));

            //lst_CG_top.Add(haunch_CG_top);


            //double web_CG_top = 0.0;
            //if (Lamda * Xu > (D1 + D2 + Ds)) web_CG_top = ((D1 + D2 + Ds) + (Lamda * Xu - D1 - D2 - Ds) / 2);
            //else web_CG_top = 0.0;
            //lst_CG_top.Add(web_CG_top);

            //double Fc_deck = 0.0;
            //double Fc_top_flange = 0.0;
            //double Fc_haunch = 0.0;
            //double Fc_web = 0.0;

            //Fc_deck = Eta * deck_slab_area * fcd;
            //Fc_top_flange = Eta * top_flange_area * fcd;
            //Fc_haunch = Eta * haunch_area * fcd;
            //Fc_web = Eta * web_area * fcd;



            //for (int i = 0; i < lst_area.Count; i++)
            //{
            //    lst_Fc.Add(Eta * lst_area[i] * fcd / 1000);
            //}

            //for (int i = 0; i < lst_Fc.Count; i++)
            //{
            //    lst_moment.Add(lst_CG_top[i] * lst_Fc[i] / 1000);
            //}

            //list.Add(string.Format(""));

            //List<string> lst_section = new List<string>();

            //lst_section.Add("deck slab");
            //lst_section.Add("Top flange (D1)");
            //lst_section.Add("Haunch(D2)");
            //lst_section.Add("Web ");









            //format = "{0,10} {1,14} {2,14} {3,14} {4,14}";

            //list.Add(string.Format(format, "Section", "Area(mm^2)", "Cg from top(mm)", "Fc=ɳFcd*A(Kn)", "Moment=Fcd*Cg(KN-m)"));


            //double total_sec_area = 0.0;
            //double total_Fc = 0.0;
            //double total_Moment = 0.0;
            //for (int i = 0; i < lst_moment.Count; i++)
            //{
            //    list.Add(string.Format(format, lst_section[i],
            //        lst_area[i],
            //        lst_CG_top[i],
            //        lst_Fc[i],
            //        lst_moment[i]));

            //    total_sec_area += lst_area[i];
            //    total_Fc += lst_Fc[i];
            //    total_Moment += lst_moment[i];
            //}

            //list.Add(string.Format(""));

            //list.Add(string.Format(format, "Total",
            //    total_sec_area,
            //    "",
            //    total_Fc,
            //    total_Moment));

            //list.Add(string.Format(""));
            //list.Add(string.Format("Calculations of force and moment capacity for steel"));
            //list.Add(string.Format(""));


            //double Fs = fyk / gama_s * total_area / 1000;
            //list.Add(string.Format("Fs= fyk / ɣs x As /1000"));
            //list.Add(string.Format("  = {0} / {1} x {2}", fyk, gama_s, total_area));
            //list.Add(string.Format("  = {0:f2}", fyk, gama_s, total_area));

            //list.Add(string.Format(""));

            //double fib_moment = Fs * d / 1000;

            //list.Add(string.Format("Moment about top fibre = Fs * d / 1000 = {0} x {1} / 1000 = {2:f3}", Fs, d, fib_moment));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Comparision of total compression = total tension = FC= FT"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //double diffe = Fs - total_Fc;

            //list.Add(string.Format("Difference in compression and tension which"));
            //list.Add(string.Format("has to be be Zero as mentioned above FC = FT  = "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("    =  "));
            //list.Add(string.Format(""));



            //list.Add(string.Format("Taking moments about top of fibre gives moment of resistance"));
            //list.Add(string.Format(""));

            //double Mrd = fib_moment - total_Moment;
            //if (Mrd > BM)
            //    list.Add(string.Format("Mrd = {0:f3} - {1:f3} = {2:f4} kn-m ,  OK", fib_moment, total_Moment, Mrd));
            //else
            //    list.Add(string.Format("Mrd = {0:f3} - {1:f3} = {2:f4} kn-m ,  NOT OK, Increase The Reinforcement", fib_moment, total_Moment, Mrd));


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Shear Check as per IRC-112"));
            //list.Add(string.Format(""));

            //double Ved = 162.8;
            //list.Add(string.Format("VED  is ultimate design shear force in the section considered resulting from external loading = {0:f3} KN", Ved));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("VRd,c  is the design shear reinforcement of the member without shear reinforcement"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        (0.12k(80rl fck)^0.33+ 0.15 σcp) bwd   [ Clause 10.3.2]"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        subject to a minimum of (νmin + 0.15 σcp)bwd        "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Where"));
            //list.Add(string.Format(""));

            //double K = 1 + Math.Sqrt(200 / d);
            //list.Add(string.Format("K  = 1 + sqrt(200/d)"));
            //list.Add(string.Format("   = 1 + sqrt(200/{0})", d));
            //if (K <= 2.0)
            //    list.Add(string.Format("   = {0} <= 2.0 Hence OK ", K));
            //else
            //    list.Add(string.Format("   = {0} > 2.0 NOT OK ", K));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("fck is the concrete cylinder strength in MPA = M{0} Mpa", fck));
            //list.Add(string.Format(""));

            //double Asl = total_area;
            //double r = Asl / bw * d;

            //list.Add(string.Format("r = Asl / (bw x d) "));
            //list.Add(string.Format("  = {0:f3} / ({1} x {0:f3})", Asl, bw, d));
            //list.Add(string.Format("  = {0:f3}", r));
            //list.Add(string.Format(""));

            //if (r > 0.02)
            //{
            //    list.Add(string.Format("  = {0:f3} > 0.02 = 0.02", r));
            //    r = 0.02;
            //}




            //list.Add(string.Format("Asl = the area of longitudinal tensile reinforcement which extends a minimum of design = {0:f3} mm^2 ", Asl));
            //list.Add(string.Format("        anchorage length and an effective depth beyond the section considered      "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("bw  = the smallest width of cross-section in the tensile zone = {0:f3} mm ", bw));
            //list.Add(string.Format(""));
            //list.Add(string.Format("d   = effective depth of tensile reinforcement = {0:f3} mm", d));
            //list.Add(string.Format(""));
            //list.Add(string.Format("σcp  = NED/Ac (in MPA) = 0"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("NED  = the axial forcein the cross-section from the loading = 0"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Ac = the gross concrete cross-section area = NA "));
            //list.Add(string.Format(""));

            //double vmin = 0.0;

            //vmin = 0.031 * Math.Pow(K, 1.5) * Math.Pow(fck, 0.5);
            //list.Add(string.Format("νmin  = 0.031 x K ^(3/2) x fck ^ (1/2)"));
            //list.Add(string.Format("      = 0.031 x {0:f3} ^(3/2) x {1:f3}^(1/2)", K, fck));
            //list.Add(string.Format("      = {0:f4}", vmin));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //double chk_val = 0.0;
            //double sigma_cp = 0.0;
            //chk_val = (vmin + 0.15 * sigma_cp) * bw * d;

            //list.Add(string.Format("         (νmin + 0.15 x σ_cp) x bw x d"));
            //list.Add(string.Format("       = ({0:f3} + 0.15 x 0) x {1:f3} x {2:f3}", vmin, bw, d));
            //list.Add(string.Format("       = {0:f3} kN ", chk_val));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //list.Add(string.Format(""));

            //double VRdc = Math.Max((0.12 * K * Math.Pow((80 * r * fck), 0.33) * bw * d / 1000), chk_val);


            //list.Add(string.Format(""));
            //list.Add(string.Format("   (0.12*K*(80*p*fck)^0.33)*bw*d/1000"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(" = (0.12*{0:f3}*(80*{1:f3}*{2})^0.33)*{3}*{4}/1000", K, r, fck, bw, d));



            //list.Add(string.Format("Thus ignoring any axial load VRd,c  = {0:f3} KN ", VRdc));
            //list.Add(string.Format("      "));

            //if (Ved > VRdc)
            //    list.Add(string.Format("Design shear reinforcement is required"));
            //else
            //    list.Add(string.Format("Shear reinforcement is not required"));


            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            //double theta = 45;

            //list.Add(string.Format("Try vertical links where α = {0} and α strut angle θ of {1}° :", Alpha, theta));
            //list.Add(string.Format(""));
            //list.Add(string.Format("α_cw    is a co-efficient taking in to account of the state of the stress in the compressive chord = {0}", alpha_cw));
            //list.Add(string.Format("        where σcp = {0}", sigma_cp));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        σcp = is the mean compressive stress, measured positive in the concrete"));
            //list.Add(string.Format("              due to the design axial force"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("V1      is the strength reduction factor for concrete cracked in shear = {0:f3}", V1));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        V1 = 0.6        for fck <= 80 MPa"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("        V1 = 0.9-fck/250        for fck => 80 MPa"));
            //list.Add(string.Format(""));
            //double Z = 0.9 * d;
            //list.Add(string.Format("Z   =  lever arm can be taken as 0.9d for R.C . Section and to be calculated for PSC section"));
            //list.Add(string.Format("    =  0.9 x {0}", d));
            //list.Add(string.Format("    =  {0:f3} mm ", Z));
            //list.Add(string.Format(""));
            //list.Add(string.Format("fcd   =  {0:f3} Mpa", fcd));
            //list.Add(string.Format(""));

            //theta = (0.5 * (Math.Asin((2 * Ved * 1000) / (alpha_cw * bw * Z * V1 * fcd)))) * (180 / Math.PI);
            ////list.Add(string.Format("θ  = 0.5 * sin⁻¹(2*VNS/(α_cw* bw*z*v1*fcd))        ( IRC 112:2011, Clause 10.3.3.1) =        2.993 o        cotq        =        2.50        "));
            //list.Add(string.Format("θ  = 0.5 * sin⁻¹ x (2 x Ved/(α_cw x bw x Z x V1 x fcd))      {0}", (iApp.DesignStandard == eDesignStandard.IndianStandard) ? "( IRC 112:2011, Clause 10.3.3.1)" : ""));
            //list.Add(string.Format("   = 0.5 * sin⁻¹ x (2 x {0}/({1} x {2} x {3} x {4} x {5})) ", Ved, alpha_cw, bw, Z, V1, fcd));
            //list.Add(string.Format("   = {0}", theta));



            ////list.Add(string.Format("θ  = 0.5 * sin⁻¹(2*VNS/(α_cw* bw*z*v1*fcd))        ( IRC 112:2011, Clause 10.3.3.1) =        2.993 o        cotq        =        2.50        "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("cot θ = cot {0} = {1}", theta, (1 / Math.Tan(theta))));
            //list.Add(string.Format(""));
            //list.Add(string.Format("tan θ = tan {0} = {1}", theta, Math.Tan(theta)));

            //if (theta < 21.8)
            //    theta = 21.8;
            //else if (theta > 45)
            //    theta = 45.0;


            //list.Add(string.Format("        =        21.80        o (As per clause 10.2.2.2)        tanq        =        0.40        "));
            //list.Add(string.Format("bw        =        300        mm        "));
            //list.Add(string.Format("VRD.max        design value of maximum shear force      acw bwz v1 fcd / (cotq+tanq)      =        1076.6        KN        "));
            //list.Add(string.Format("        Hence Ok        "));
            //list.Add(string.Format("Checking of shear reinforcement      Clause        10.3.3.2    "));
            //list.Add(string.Format("Asw        >= VRd,s      "));
            //list.Add(string.Format("s        zfywd cotq      "));
            //list.Add(string.Format("      "));
            //list.Add(string.Format("VRD.s        design value of the shear force which can be sustained by the yielding reinforcement=        162.8        "));
            //list.Add(string.Format("      "));
            //list.Add(string.Format("fywd        is the design yeild strength of the shear reinforcement = 0.8*fywk=        288.7        Mpa        "));
            //list.Add(string.Format("      "));
            //list.Add(string.Format("VRd,s        =        0.145        mm2/mm        "));
            //list.Add(string.Format("zfywd cotq      "));
            //list.Add(string.Format("      "));
            //list.Add(string.Format("Provide 2 L        legged        12 f        @        200        mm c/c        "));
            //list.Add(string.Format("      "));
            //list.Add(string.Format("Asw        provided=        226.19        mm2/m        "));
            //list.Add(string.Format("      "));
            //list.Add(string.Format("Asw/ s        =        1.1310        mm2/mm        "));
            //list.Add(string.Format("rmin        =        =        0.00087        <        0.00377        Hence OK        "));
            //list.Add(string.Format(""));

            return list;
        }


        public void Calculate_Problem()
        {
            List<string> list = new List<string>();


            #region TechSOFT Banner
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            //sw.WriteLine("----------------------------------------------------------------------------------------------");
            list.Add(string.Format("\t\t***********************************************"));
            list.Add(string.Format("\t\t*            ASTRA Pro Release 21             *"));
            list.Add(string.Format("\t\t*        TechSOFT Engineering Services        *"));
            list.Add(string.Format("\t\t*                                             *"));
            list.Add(string.Format("\t\t*  LIMIT STATE DESIGN OF LONGITUDINAL GIRDER  *"));
            list.Add(string.Format("\t\t*  FOR T-BEAM RCC  BRIDGE WITH BOTTOM FLANGE  *"));
            list.Add(string.Format("\t\t***********************************************"));
            list.Add(string.Format("\t\t----------------------------------------------"));
            list.Add(string.Format("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " "));
            list.Add(string.Format("\t\t----------------------------------------------"));
            list.Add("");

            #endregion

            list.AddRange(Get_Basic_Data().ToArray());

            list.Add("");
            list.Add("");
            list.Add(string.Format("---------------------------------------------------------------------------"));
            list.Add(string.Format("---------------------       END OF REPORT        --------------------------"));
            list.Add(string.Format("---------------------------------------------------------------------------"));

            list.Add(string.Format("---------------------------------------------------------------------------"));
            list.Add(string.Format("---------            Thank you for using ASTRA Pro          ---------------"));
            list.Add(string.Format("---------------------------------------------------------------------------"));


            File.WriteAllLines(rep_file_name, list.ToArray());
        }

    }
    public class SteelLayerData
    {
        public string Layer { get; set; }
        public short Bar_Dia { get; set; }
        public short Bars_No { get; set; }
        public double CG_from_bot { get; set; }

        public double Area
        {
            get
            {
                return (Math.PI * Bar_Dia * Bar_Dia * Bars_No / 4.0);
            }
        }
    }
}
