using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;


namespace LimitStateMethod.SubStructure
{
    public partial class frmPier_Design_with_Piles : Form
    {

        const string Title = "DESIGN OF PIER WITH PILES";
        IApplication iApp;
        bool IsCreateData = true;
        Summary_Load_Cases SLC_Pier_Base { get; set; }
        Summary_Load_Cases SLC_Pier_Base_One_span { get; set; }
        Summary_Load_Cases SLC_Pile_Cap { get; set; }
        public frmPier_Design_with_Piles(IApplication thisApp)
        {
            InitializeComponent();
            iApp = thisApp;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            SLC_Pier_Base = new Summary_Load_Cases();
            SLC_Pier_Base_One_span = new Summary_Load_Cases();
            SLC_Pile_Cap = new Summary_Load_Cases();
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

        public string Report_File
        {
            get
            {
                return Path.Combine(user_path, "Design of Pier with Piles.txt");
            }
        }

        public void Calculate_Program(string file_name)
        {
            List<string> list = new List<string>();
            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*            ASTRA Pro Release 21            *");
            list.Add("\t\t*        TechSOFT Engineering Services       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*          DESIGN OF PIER WITH PILES         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion


            TableData tab1 = new TableData(dgv1);


            TableData tab2 = new TableData(dgv2);
            TableData tab3 = new TableData(dgv3);



            List<double> LIST_B = new List<double>();
            List<double> LIST_H = new List<double>();
            List<double> LIST_D = new List<double>();
            List<double> LIST_S = new List<double>();
            List<double> LIST_W = new List<double>();



            #region Read Inputs from Form

            LIST_B.Add(MyList.StringToDouble(txt_B1.Text, 0.0));
            LIST_B.Add(MyList.StringToDouble(txt_B2.Text, 0.0));
            LIST_B.Add(MyList.StringToDouble(txt_B3.Text, 0.0));
            LIST_B.Add(MyList.StringToDouble(txt_B4.Text, 0.0));
            LIST_B.Add(MyList.StringToDouble(txt_B5.Text, 0.0));
            LIST_B.Add(MyList.StringToDouble(txt_B6.Text, 0.0));

            LIST_H.Add(MyList.StringToDouble(txt_H1.Text, 0.0));
            LIST_H.Add(MyList.StringToDouble(txt_H2.Text, 0.0));
            LIST_H.Add(MyList.StringToDouble(txt_H3.Text, 0.0));
            LIST_H.Add(MyList.StringToDouble(txt_H4.Text, 0.0));
            LIST_H.Add(MyList.StringToDouble(txt_H5.Text, 0.0));

            LIST_D.Add(MyList.StringToDouble(txt_D1.Text, 0.0));
            LIST_D.Add(MyList.StringToDouble(txt_D2.Text, 0.0));
            LIST_D.Add(MyList.StringToDouble(txt_D3.Text, 0.0));
            LIST_D.Add(MyList.StringToDouble(txt_D4.Text, 0.0));


            LIST_S.Add(MyList.StringToDouble(txt_S1.Text, 0.0));
            LIST_S.Add(MyList.StringToDouble(txt_S2.Text, 0.0));

            LIST_S.Add(MyList.StringToDouble(txt_S3.Text, 0.0));
            LIST_S.Add(MyList.StringToDouble(txt_S4.Text, 0.0));
            LIST_S.Add(MyList.StringToDouble(txt_S5.Text, 0.0));
            LIST_S.Add(MyList.StringToDouble(txt_S6.Text, 0.0));
            LIST_S.Add(MyList.StringToDouble(txt_S7.Text, 0.0));
            LIST_S.Add(MyList.StringToDouble(txt_S8.Text, 0.0));

            LIST_W.Add(MyList.StringToDouble(txt_W1.Text, 0.0));


            #endregion Read Inputs from Form

            SLC_Pier_Base.Clear();
            SLC_Pier_Base_One_span.Clear();
            SLC_Pile_Cap.Clear();

            SLC_Pier_Base.Descriptions.Add("Normal - Longitudinal Moment Case");
            SLC_Pier_Base.Descriptions.Add("Normal - Transverse Moment Case");
            SLC_Pier_Base.Descriptions.Add("Seismic Case -Longitudinal");
            SLC_Pier_Base.Descriptions.Add("Seismic Case -Transverse");



            SLC_Pier_Base_One_span.Descriptions.Add("Normal - Longitudinal Moment Case");
            SLC_Pier_Base_One_span.Descriptions.Add("Normal - Transverse Moment Case");
            SLC_Pier_Base_One_span.Descriptions.Add("Seismic Case -Longitudinal");
            SLC_Pier_Base_One_span.Descriptions.Add("Seismic Case -Transverse");



            SLC_Pile_Cap.Descriptions.Add("Normal - Longitudinal Moment Case");
            SLC_Pile_Cap.Descriptions.Add("Normal - Transverse Moment Case");
            SLC_Pile_Cap.Descriptions.Add("Seismic Case -Longitudinal");
            SLC_Pile_Cap.Descriptions.Add("Seismic Case -Transverse");



            double L1, L2, L3, L4, L5, L6, L7;
            double H1, H2, H3, H4, H5, H6, H7;
            double D1, D2, D3, D4, D5, D6, D7;
            double W1, W2, W3, W4, W5, W6, W7;
            double B1, B2, B3, B4, B5, B6, B7;
            double A1, A2, A3, A4, A5, A6, A7;

            double DMG, SMG, Pile_len;



            L1 = 215.520; //(Deck Level)
            DMG = 1.936; //(Depth of Main Long Girder)
            L2 = L1 - DMG; //()
            H1 = 0.5; //()
            H2 = 1.5; //()
            H3 = 6.504; //()

            L3 = 205.580;
            H4 = 1.8;


            D1 = 3.6;
            D2 = 1.5;
            D3 = 3.6;

            double conc_den, soil_den, over_all_sup_dep, crs_slpe, wc, vc, hc;


            conc_den = MyList.StringToDouble(txt_DEN_Con.Text, 2.4); //t/m^3
            soil_den = MyList.StringToDouble(txt_DEN_S.Text, 1.8);
            over_all_sup_dep = MyList.StringToDouble(txt_DSS.Text, 1.5);
            Pile_len = MyList.StringToDouble(txt_PL.Text, 20.0);
            crs_slpe = MyList.StringToDouble(txt_CS.Text, 0.09375); // 3.75*0.025  
            wc = MyList.StringToDouble(txt_WC.Text, 0.065); // 


            vc = MyList.StringToDouble(txt_VLC.Text, 0.0); // 
            hc = MyList.StringToDouble(txt_HLC.Text, 0.0); // 


            List<string> list_vert_load_names = new List<string>();
            List<string> list_vert_load_values = new List<string>();
            List<string> list_Dist_cg_shaft_long = new List<string>();
            List<string> list_Moment_cg_shaft_long = new List<string>();
            List<string> list_Dist_cg_shaft_trans = new List<string>();
            List<string> list_Moment_cg_shaft_trans = new List<string>();

            int i = 0;



            //This Program has brought from "03 Pier Design with 6 Piles.XLS" excel worksheet design program.
            list.Add(string.Format(""));
            list.Add(string.Format("USER'S INPUT DATA :"));
            list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));
            
            //List<double> LIST_B = new List<double>();
            //List<double> LIST_H = new List<double>();
            //List<double> LIST_D = new List<double>();
            //List<double> LIST_S = new List<double>();
            //List<double> LIST_W = 
            for (i = 0; i < LIST_B.Count; i++)
            {
                list.Add(string.Format("B{0} = {1:f3} m", (i + 1), LIST_B[i]));
            }
            list.Add(string.Format(""));
            for (i = 0; i < LIST_H.Count; i++)
            {
                list.Add(string.Format("H{0} = {1:f3} m", (i + 1), LIST_H[i]));
            }
            list.Add(string.Format(""));
            for (i = 0; i < LIST_D.Count; i++)
            {
                list.Add(string.Format("D{0} = {1:f3} m", (i + 1), LIST_D[i]));
            }
            list.Add(string.Format(""));
            for (i = 0; i < LIST_S.Count; i++)
            {
                list.Add(string.Format("S{0} = {1:f3} m", (i + 1), LIST_S[i]));
            }
            list.Add(string.Format(""));
            for (i = 0; i < LIST_W.Count; i++)
            {
                list.Add(string.Format("W{0} = {1:f3} m", (i + 1), LIST_W[i]));
            }
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Density Of Concrete = {0:f3} t/m^3", conc_den));
            list.Add(string.Format("2. Density Of Soil = {0:f3} t/m^3", soil_den));
            list.Add(string.Format("3. Length of Pile  = {0:f3} m", Pile_len));
            list.Add(string.Format("4. Over all Depth of Super Structure =   {0:f3} m", over_all_sup_dep));
            list.Add(string.Format("5. Cross Slope =  {0:f3} m", crs_slpe));
            list.Add(string.Format("6. Wearing Coat =  {0:f3} m", wc));

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #region STEP 1 : LOAD CALCULATIONS AT PIER BASE
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : LOAD CALCULATIONS AT PIER BASE"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Normal Case"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("S.No   Vertical Load   Value (t)   Moment About Pier 456.2   "));
            //list.Add(string.Format("                                   Distance w.r.t c.g of Shaft (m) in Long   Moment about c.g of  Shaft in Long (t-m)   Distance w.r.t c.g of Shaft (m) in Trans   Moment about c.g of  Shaft in Trans (t-m) "));

            //list.Add(string.Format("S.No   Vertical Load   Value (t)  "));

            #region Print table
            list.Add(string.Format(""));
            //list.Add(string.Format("---------0---------0---------0---------0---------0---------0---------0---------0---------0---------0---------0"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                              --------------------   Moment About Pier     ---------------------"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No  Vertical Load                Value (t)   Distance w.r.t    Moment about    Distance w.r.t    Moment about"));
            list.Add(string.Format("                                                c.g of Shaft     c.g of Shaft    c.g of Shaft     c.g of Shaft"));
            list.Add(string.Format("                                                (m) in Long      in Long (t-m)   (m) in Trans     in Trans (t-m) "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            for (i = 0; i < tab1.VERT_LOAD_NAMES.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-28} {2,9:f3} {3,12:f3} {4,17:f3} {5,16:f3} {6,18:f3} ", (i + 1),
                    tab1.VERT_LOAD_NAMES[i],
                    tab1.VERT_LOAD_VALUES[i],
                    tab1.DIST_CG_SHAFT_LONG[i],
                    tab1.MOMENT_CG_SHAFT_LONG[i],
                    tab1.DIST_CG_SHAFT_TRANS[i],
                    tab1.MOMENT_CG_SHAFT_TRANS[i]
                    ));
            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            #endregion Print table

            list.Add(string.Format(""));

            double fc = MyList.StringToDouble(txt_CF.Text, 0.05);

            //list.Add(string.Format("1     Dead Load + SIDL                479.01       0.000             0.000            0.000              0.000 "));
            //list.Add(string.Format("2     Live Load (Max Long Moment)      92.00       1.100           101.200            1.155            106.260 "));
            //list.Add(string.Format("3     Live Load (Max Trans Moment)     46.78       1.100            51.460            2.950            138.010 "));
            //list.Add(string.Format("4     Pier Cap                        111.24       0.000             0.000            0.000              0.000 "));
            //list.Add(string.Format("5     Pier Shaft                      168.58       0.000             0.000            0.000              0.000 "));
            //list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Force (As per Clause 214.5.1 of IRC:6-2000)  "));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficent of friction = fc = {0:f3}", fc));

            //double AHF = 20.0; //user input
            double AHF1 = MyList.StringToDouble(txt_AHF_1.Text, 0.0); //user input


            list.Add(string.Format("Applied Horizontal force = {0:f3} t ", AHF1));

            list.Add(string.Format("Dead Load + SIDL - Reaction = {0:f3} t", tab1.VERT_LOAD_VALUES[0]));
            list.Add(string.Format(""));
            //double LLR = 15.581; //user input
            double LLR1 = MyList.StringToDouble(txt_LLR_1.Text, 0.0); //user input
            list.Add(string.Format("Live Load Reaction at free end - Max Long Moment Case = {0:f3} t", LLR1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Normal Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));


            double V1 = (AHF1 - fc * (tab1.VERT_LOAD_VALUES[0] + LLR1));


            list.Add(string.Format("Greater of  - I       ({0:f3}-{1:f3}*({2:f3}+{3:f3}))  = {4:f3} t",
                AHF1, fc, tab1.VERT_LOAD_VALUES[0], LLR1, V1));


            double V2 = (AHF1 / 2.0 + fc * (tab1.VERT_LOAD_VALUES[0] + LLR1));

            double maxV1 = Math.Max(V1, V2);

            list.Add(string.Format("or          -II       ({0:f3}/2+{1:f3}*({2:f3}+{3:f3}))  = {4:f3} t",
                AHF1, fc, tab1.VERT_LOAD_VALUES[0], LLR1, V2));
            list.Add(string.Format(""));
            list.Add(string.Format(" Max Longitudinal Moment = {0:f3} t", maxV1));

            double V3 = LIST_H[1] - LIST_H[3] + ((LIST_H[2] - LIST_H[3]) / 2.0);


            list.Add(string.Format("cg from Pier shaft base (Long) {0:f3} - {1:f3} + {2:f3}  = {3:f3} m",
                LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), V3));
            //list.Add(string.Format("Moment about Pier Shaft base (Long)  34.730*8.754   304.022   t-m "));
            list.Add(string.Format(""));

            double V4 = maxV1 * V3;
            list.Add(string.Format("Moment about Pier Shaft base (Long)  {0:f3} x {1:f3} = {2:f3} t-m", maxV1, V3, V4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Free Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));
            double V5 = fc * (tab1.VERT_LOAD_VALUES[0] + LLR1);
            list.Add(string.Format("Horizontal force  ({0:f3}*({1:f3} + {2:f3})) = {3:f3} t", fc, tab1.VERT_LOAD_VALUES[0], LLR1, V5));
            list.Add(string.Format(""));

            double V6 = V5 * V3;
            list.Add(string.Format("Moment about Pier Shaft base (Long)   {0:f3} * {1:f3}  = {2:f3} t-m", V5, V3, V6));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Case Calculations"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic coefficient :"));
            list.Add(string.Format("---------------------"));
            list.Add(string.Format(""));


            double Z, Sa_by_g, R, I, Ah;

            Z = MyList.StringToDouble(txt_Z.Text, 0.0);
            Sa_by_g = MyList.StringToDouble(txt_Sa_by_g.Text, 0.0);
            R = MyList.StringToDouble(txt_R.Text, 0.0);
            I = MyList.StringToDouble(txt_I.Text, 0.0);

            Ah = (Z / 2) * (Sa_by_g) / (R / I);

            txt_HSC.Text = Ah.ToString("f3");

            list.Add(string.Format("Ah = horizontal seismic coefficient   =   ( Z / 2 ) . ( Sa / g ) / ( R / I )  "));
            //list.Add(string.Format("Z = Zone Factor  =   0.24   (Bridge is Zone IV) "));
            list.Add(string.Format("Z = Zone Factor  =   {0:f3} ", Z));
            list.Add(string.Format("I = Importance Factor  =   {0:f3}  ", I));
            list.Add(string.Format("Sa / g = Average response acceleration coefficient = {0:f3}", Sa_by_g));
            list.Add(string.Format("R = Response reduction factor  = {0:f3}", R));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal seismic coefficient = Ah = {0:f3}", Ah));

            //list.Add(string.Format("Vertical seismic coefficient  =   0.090 For Case IV "));
            double V7 = (Ah / 2);
            list.Add(string.Format("Vertical seismic coefficient  =  {0:f3}", V7));
            list.Add(string.Format(""));

            double V8 = LIST_B[0] * LIST_D[0];
            double V9 = 0.5 * (LIST_B[0] + LIST_B[1]) * LIST_D[1];

            double V10 = (V8 * 0.5 * LIST_D[0] + V9 * (LIST_D[0] + ((LIST_D[1] / 3) * ((LIST_B[0] + 2 * LIST_B[1]) / (LIST_B[0] + 2 * LIST_B[1]))))) / (V8 + V9);

            list.Add(string.Format("cg of Pier cap from Top            Rect        Trap   "));
            list.Add(string.Format("Weight                            {0:f3}    {1:f3}          {2:f3}   ", V8, V9, V10));
            list.Add(string.Format(""));
            double V11 = LIST_D[0] + LIST_D[1] - V10;

            double CG = V11;

            list.Add(string.Format(" {0:f3} ", V11));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Seismic Case - Longitudinal"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));

            double V12 = (tab1.VERT_LOAD_VALUES[0] * Ah + AHF1 / 2 - fc * (tab1.VERT_LOAD_VALUES[0] + LLR1 / 2));
            list.Add(string.Format("Greater of  - I ({0:f3}*{1:f3}+{2:f3}/2-{3:f3}*({0:f3}+{4:f3}/2))   = {5:f3} t",
                 tab1.VERT_LOAD_VALUES[0], Ah, AHF1, fc, LLR1, V12));


            double V13 = (((tab1.VERT_LOAD_VALUES[0] * Ah) / 2 + AHF1 / 2) / 2 + fc * (tab1.VERT_LOAD_VALUES[0] + LLR1 / 2));


            //Greater of  - I		(479.01*0.18+20/2-0.05*(479.01+15.581/2))			71.882
            //or -II		(((479.01*0.18)/2+20/2)/2+0.05*(479.01+15.581/2))		

            double maxV2 = Math.Max(V12, V13);
            list.Add(string.Format("or   -II ((({0:f3}*{1:f3})/2+{2:f3}/2)/2+{3:f3}*({0:f3}+{4:f3}/2))  = {5:f3} t ",
                tab1.VERT_LOAD_VALUES[0], Ah, AHF1, fc, LLR1, V13
                ));
            //list.Add(string.Format(" 72.451   t "));
            list.Add(string.Format(" Max Longitudinal Moment = {0:f3} t", maxV2));
            list.Add(string.Format(""));

            //list.Add(string.Format("cg from Pier shaft base (Long) 213.584-205.08+0.25 8.754   m "));

            list.Add(string.Format("cg from Pier shaft base (Long) {0:f3} - {1:f3} + {2:f3}  = {3:f3} m",
                LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), V3));

            list.Add(string.Format(""));
            double V14 = maxV2 * V3;
            list.Add(string.Format("Moment about Pier Shaft base (Long) = {0:f3} * {1:f3}  = {2:f3} t-m", maxV2, V3, V14));

            list.Add(string.Format(""));
            list.Add(string.Format("2. Free Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));

            double V15 = fc * (tab1.VERT_LOAD_VALUES[0] + LLR1 / 2.0);
            list.Add(string.Format("Horizontal force  ({0:f3}*({1:f3} + {2:f3}/2)) = {3:f3} t", fc, tab1.VERT_LOAD_VALUES[0], LLR1, V15));
            list.Add(string.Format(""));

            double V16 = V15 * V3;
            list.Add(string.Format("Moment about Pier Shaft base (Long) = {0:f3} * {1:f3}  = {2:f3} t-m", V15, V3, V16));
            list.Add(string.Format(""));
            //list.Add(string.Format("Horizontal force (0.05*(479.01+15.581/2)) 24.340   t "));
            //list.Add(string.Format("Moment about Pier Shaft base (Long)  24.340*8.754   213.073   t-m "));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Seismic Case - Transverse"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));

            double V17 = (AHF1 / 2.0 - fc * (tab1.VERT_LOAD_VALUES[0] + LLR1 / 2));

            list.Add(string.Format("Greater of  - I ({0:f3}/2-{1:f3}*({2:f3}+{3:f3}/2)) = {4:f3} t ",
                AHF1, fc, tab1.VERT_LOAD_VALUES[0], LLR1, V17));

            double V18 = ((AHF1 / 2) / 2 + fc * (tab1.VERT_LOAD_VALUES[0] + LLR1 / 2));

            list.Add(string.Format("or -II (({0:f3}//2)/2+{1:f3}*({2:f3}+{3:f3}/2))  = {4:f3} t ",
                AHF1, fc, tab1.VERT_LOAD_VALUES[0], LLR1, V18));
            list.Add(string.Format(""));
            double maxV3 = Math.Max(V17, V18);
            list.Add(string.Format(" Max Longitudinal Moment = {0:f3} t ", maxV3));


            //list.Add(string.Format("cg from Pier shaft base (Long) 213.584-205.08+0.25 =  8.754   m "));
            list.Add(string.Format("cg from Pier shaft base (Long) {0:f3} - {1:f3} + {2:f3}  = {3:f3} m",
             LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), V3));

            double V19 = maxV3 * V3;
            list.Add(string.Format("Moment about Pier Shaft base (Long) = {0:f3} * {1:f3} = {2:f3} t-m", maxV3, V3, V19));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces and Moments at the Base of Pier"));
            list.Add(string.Format("--------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Normal Case"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format("I: Max Longitudinal Moment Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));


            double V20 = tab1.VERT_LOAD_VALUES[0] + tab1.VERT_LOAD_VALUES[1] + tab1.VERT_LOAD_VALUES[3] + tab1.VERT_LOAD_VALUES[4];

            list.Add(string.Format("Vertical Load = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} t ",
                tab1.VERT_LOAD_VALUES[0], tab1.VERT_LOAD_VALUES[1], tab1.VERT_LOAD_VALUES[3], tab1.VERT_LOAD_VALUES[4], V20));

            double V21 = tab1.MOMENT_CG_SHAFT_TRANS[1];
            list.Add(string.Format("Transverse Moment  = {0:f3} t-m ", V21));

            double V22 = tab1.MOMENT_CG_SHAFT_LONG[1] + V4;
            list.Add(string.Format("Longitudinal Moment = {0:f3} + {1:f3} = {2:f3} t-m",
                tab1.MOMENT_CG_SHAFT_LONG[1], V4, V22));

            SLC_Pier_Base.Vertical_Load.Add(V20);
            SLC_Pier_Base.Mll.Add(V22);
            SLC_Pier_Base.Mtt.Add(V21);


            list.Add(string.Format(""));
            list.Add(string.Format("II:  Max Transverse Moment Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));

            double V23 = tab1.VERT_LOAD_VALUES[0] + tab1.VERT_LOAD_VALUES[2] + LLR1 + tab1.VERT_LOAD_VALUES[3] +
                 tab1.VERT_LOAD_VALUES[4];

            list.Add(string.Format("Vertical Load = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} = {5:f3} t ",
                tab1.VERT_LOAD_VALUES[0], tab1.VERT_LOAD_VALUES[2], LLR1, tab1.VERT_LOAD_VALUES[3],
                 tab1.VERT_LOAD_VALUES[4], V23
                ));

            double V24 = tab1.MOMENT_CG_SHAFT_TRANS[2];

            list.Add(string.Format("Transverse Moment =  {0:f3} t-m ", V24));

            double V25 = tab1.MOMENT_CG_SHAFT_LONG[2] + V4;

            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Moment = {0:f3} + {1:f3} = {2:f3}   t-m ",
                tab1.MOMENT_CG_SHAFT_LONG[2], V4, V25));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            SLC_Pier_Base.Vertical_Load.Add(V23);
            SLC_Pier_Base.Mll.Add(V25);
            SLC_Pier_Base.Mtt.Add(V24);



            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Case: (Longitudinal)"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Coefficient = {0:f3}  ", Ah));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            //list.Add(string.Format("S.No   Horizontal Load   Value (t)   Acting at a distance from pile cap top (m)   Moment about Pile Cap Top (t-m) "));
            //list.Add(string.Format("1   On Dead Load + SIDL   86.22   9.722   838.25 "));
            //list.Add(string.Format("2   On Pier Cap   20.02   7.75   155.211 "));
            //list.Add(string.Format("3   On Pier Shaft   30.35   3.25   98.68 "));
            //list.Add(string.Format("Total 136.59 1092.14 "));

            #region On Dead Load + SIDL
            list.Add(string.Format("On Dead Load + SIDL "));
            list.Add(string.Format("---------------------"));

            list.Add(string.Format(""));
            double V26 = Ah * tab1.VERT_LOAD_VALUES[0];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[0], V26));
            list.Add(string.Format(""));


            //double V27 ='Design-Input'!$H$6-'Design-Input'!$H$21+0.25+0.5*('Design-Input'!$H$3-'Design-Input'!$H$6)
            double V27 = LIST_H[1] - LIST_H[3] + ((LIST_H[2] - LIST_H[3]) / 2.0) + 0.5 * (LIST_H[0] - LIST_H[1]);
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} - {1:f3} + {2:f3} + 0.5*({3:f3} - {0:f3})",
                LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), LIST_H[0]));
            list.Add(string.Format("                                       = {0:f3} m", V27));
            list.Add(string.Format(""));
            double V28 = V26 * V27;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V26, V27, V28));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Dead Load + SIDL
            list.Add(string.Format("On Pier Cap"));
            list.Add(string.Format("-----------"));

            list.Add(string.Format(""));
            double V29 = Ah * tab1.VERT_LOAD_VALUES[3];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[3], V29));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            double V30 = LIST_D[2] + V11;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} + {1:f3} = {2:f3})",
                LIST_D[2], V11, V30));
            list.Add(string.Format(""));
            double V31 = V29 * V30;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V29, V30, V31));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Pier Shaft
            list.Add(string.Format("On Pier Shaft"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            double V32 = Ah * tab1.VERT_LOAD_VALUES[4];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[4], V32));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            double V33 = 0.5 * LIST_D[2];
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} / 2 = {1:f3})",
                LIST_D[2], V33));
            list.Add(string.Format(""));
            double V34 = V32 * V33;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V32, V33, V34));
            list.Add(string.Format(""));

            #endregion On Pier Shaft

            #region Total

            list.Add(string.Format(""));
            double V35 = V26 + V29 + V32;
            list.Add(string.Format("Total Horizontal Load = {0:f3} + {1:f3} + {2:f3} = {3:f3} t", V26, V29, V32, V35));
            list.Add(string.Format(""));


            double V36 = V28 + V31 + V34;

            list.Add(string.Format("Total Moment about Pile Cap Top = {0:f3} + {1:f3} + {2:f3}  = {3:f3} t-m", V28, V31, V34, V36));
            list.Add(string.Format(""));

            #endregion Total




            list.Add(string.Format(""));
            list.Add(string.Format(""));




            list.Add(string.Format("Seismic Case: (Transverse)"));
            list.Add(string.Format("--------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("S.No   Horizontal Load   Value (t)   Acting at a distance from pile cap top (m)   Moment about Pile Cap Top (t-m) "));
            //list.Add(string.Format("1   On Dead Load + SIDL   86.22   9.722   838.25 "));
            //list.Add(string.Format("2   On Pier Cap   20.02   7.75   155.21 "));
            //list.Add(string.Format("3   On Pier Shaft   30.35   3.25   98.68 "));
            //list.Add(string.Format("4   Live Load   4.21   11.454   48.23 "));
            //list.Add(string.Format("Total 140.80 1140.37 "));



            #region On Dead Load + SIDL
            list.Add(string.Format("On Dead Load + SIDL "));
            list.Add(string.Format("---------------------"));

            list.Add(string.Format(""));
            //double V26 = Ah * tab1.VERT_LOAD_VALUES[0];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[0], V26));
            list.Add(string.Format(""));


            //double V27 ='Design-Input'!$H$6-'Design-Input'!$H$21+0.25+0.5*('Design-Input'!$H$3-'Design-Input'!$H$6)
            //double V27 = LIST_H[1] - LIST_H[3] + ((LIST_H[2] - LIST_H[3]) / 2.0) + 0.5 * (LIST_H[0] - LIST_H[1]);
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} - {1:f3} + {2:f3} + 0.5*({3:f3} - {0:f3})",
                LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), LIST_H[0]));
            list.Add(string.Format("                                       = {0:f3} m", V27));
            list.Add(string.Format(""));
            //double V28 = V26 * V27;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V26, V27, V28));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Dead Load + SIDL
            list.Add(string.Format("On Pier Cap"));
            list.Add(string.Format("-----------"));

            list.Add(string.Format(""));
            //double V29 = Ah * tab1.VERT_LOAD_VALUES[3];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[3], V29));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            //double V30 = LIST_D[2] + V11;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} + {1:f3} = {2:f3})",
                LIST_D[2], V11, V30));
            list.Add(string.Format(""));
            //double V31 = V29 * V30;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V29, V30, V31));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Pier Shaft
            list.Add(string.Format("On Pier Shaft"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            //double V32 = Ah * tab1.VERT_LOAD_VALUES[4];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[4], V32));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            //double V33 = 0.5 * LIST_D[2];
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} / 2 = {1:f3})",
                LIST_D[2], V33));
            list.Add(string.Format(""));
            //double V34 = V32 * V33;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V32, V33, V34));
            list.Add(string.Format(""));

            #endregion On Pier Shaft


            #region Live Load
            list.Add(string.Format("Live Load"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            double V37 = (Ah * tab1.VERT_LOAD_VALUES[2]) / 2;
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab1.VERT_LOAD_VALUES[2], V37));
            list.Add(string.Format(""));


            //double V30 ='Design-Input'!$H$6-'Design-Input'!$H$21+1.5+1.2+0.25


            double V38 = LIST_H[1] - LIST_H[3] + over_all_sup_dep + 1.2 + 0.25;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} - {1:f3} + {2:f3} + 1.2 + 0.25  = {3:f3})",
                LIST_H[1], LIST_H[3], over_all_sup_dep, V38));
            list.Add(string.Format(""));


            double V39 = V37 * V38;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", V37, V38, V39));
            list.Add(string.Format(""));

            #endregion Live Load


            #region Total

            list.Add(string.Format(""));
            double V40 = V26 + V29 + V32 + V37;
            list.Add(string.Format("Total Horizontal Load = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} t", V26, V29, V32, V37, V40));
            list.Add(string.Format(""));


            double V41 = V28 + V31 + V34 + V39;

            list.Add(string.Format("Total Moment about Pile Cap Top = {0:f3} + {1:f3} + {2:f3} + {3:f3}  = {4:f3} t-m", V28, V31, V34, V39, V41));
            list.Add(string.Format(""));

            #endregion Total




            list.Add(string.Format("Longitudinal Seismic Case"));
            list.Add(string.Format("--------------------------"));
            list.Add(string.Format(""));

            double V42 = tab1.VERT_LOAD_VALUES[0] + 0.5 * tab1.VERT_LOAD_VALUES[1] + tab1.VERT_LOAD_VALUES[3] + tab1.VERT_LOAD_VALUES[4];


            list.Add(string.Format("Vertical Load = {0:f3} + 0.5 * {1:f3} + {2:f3} + {3:f3}   = {4:f3} t ",
                tab1.VERT_LOAD_VALUES[0], tab1.VERT_LOAD_VALUES[1],
                tab1.VERT_LOAD_VALUES[3], tab1.VERT_LOAD_VALUES[4], V42));
            list.Add(string.Format(""));

            double V43 = 0.5 * tab1.MOMENT_CG_SHAFT_TRANS[1];
            list.Add(string.Format("Transverse Moment = 0.5 * {0:f3}  = {1:f3} t-m", tab1.MOMENT_CG_SHAFT_TRANS[1], V43));
            list.Add(string.Format(""));

            double V44 = tab1.MOMENT_CG_SHAFT_LONG[1] + V36;
            list.Add(string.Format("Longitudinal Moment = {0:f3} + {1:f3} = {2:f3} t-m ",
                tab1.MOMENT_CG_SHAFT_LONG[1], V36, V44));
            list.Add(string.Format(""));



            SLC_Pier_Base.Vertical_Load.Add(V42);
            SLC_Pier_Base.Mll.Add(V44);
            SLC_Pier_Base.Mtt.Add(V43);


            list.Add(string.Format(""));
            list.Add(string.Format("Transverse Seismic Case"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));

            double V45 = tab1.VERT_LOAD_VALUES[0] + 0.5 * tab1.VERT_LOAD_VALUES[2] + tab1.VERT_LOAD_VALUES[3] + tab1.VERT_LOAD_VALUES[4];

            //list.Add(string.Format("Vertical Load 479.01+0.5*46.782+111.24+168.58 782.22   t "));
            //list.Add(string.Format("Transverse Moment 0.5*138.007+1,140.367 1209.371   t-m "));
            //list.Add(string.Format("Longitudinal Moment 51.4602+256.843 308.303   t-m "));
            list.Add(string.Format(""));


            list.Add(string.Format("Vertical Load = {0:f3} + 0.5 * {1:f3} + {2:f3} + {3:f3}   = {4:f3} t ",
                tab1.VERT_LOAD_VALUES[0], tab1.VERT_LOAD_VALUES[2],
                tab1.VERT_LOAD_VALUES[3], tab1.VERT_LOAD_VALUES[4], V45));
            list.Add(string.Format(""));

            double V46 = 0.5 * tab1.MOMENT_CG_SHAFT_TRANS[2] + V41;
            list.Add(string.Format("Transverse Moment = 0.5 * {0:f3} + {1:f3}  = {2:f3} t-m",
                tab1.MOMENT_CG_SHAFT_TRANS[1], V41, V46));
            list.Add(string.Format(""));

            double V47 = tab1.MOMENT_CG_SHAFT_LONG[1] + V19;
            list.Add(string.Format("Longitudinal Moment = {0:f3} + {1:f3} = {2:f3} t-m ",
                tab1.MOMENT_CG_SHAFT_LONG[1], V19, V47));
            list.Add(string.Format(""));



            SLC_Pier_Base.Vertical_Load.Add(V45);
            SLC_Pier_Base.Mll.Add(V47);
            SLC_Pier_Base.Mtt.Add(V46);


            #endregion STEP 1 : LOAD CALCULATIONS AT PIER BASE

            #region STEP 2 : ONE SPAN DISLODGED CONDITION


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 2 : ONE SPAN DISLODGED CONDITION"));
            list.Add(string.Format("-------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Normal Case"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                              --------------------   Moment About Pier     ---------------------"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No  Vertical Load                Value (t)   Distance w.r.t    Moment about    Distance w.r.t    Moment about"));
            list.Add(string.Format("                                                c.g of Shaft     c.g of Shaft    c.g of Shaft     c.g of Shaft"));
            list.Add(string.Format("                                                (m) in Long      in Long (t-m)   (m) in Trans     in Trans (t-m) "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));

            for (i = 0; i < tab2.VERT_LOAD_NAMES.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-28} {2,9:f3} {3,12:f3} {4,17:f3} {5,16:f3} {6,18:f3} ", (i + 1),
                    tab2.VERT_LOAD_NAMES[i],
                    tab2.VERT_LOAD_VALUES[i],
                    tab2.DIST_CG_SHAFT_LONG[i],
                    tab2.MOMENT_CG_SHAFT_LONG[i],
                    tab2.DIST_CG_SHAFT_TRANS[i],
                    tab2.MOMENT_CG_SHAFT_TRANS[i]
                    ));
            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));


            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Force (As per Clause 214.5.1 of IRC:6-2000)  "));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficent of friction = fc = {0:f3}", fc));
            //list.Add(string.Format("Coefficent of friction 0.05  "));
            double AHF2 = MyList.StringToDouble(txt_AHF_2.Text, 0.0); //user input
            list.Add(string.Format("Applied Horizontal force = {0:f3} t ", AHF2));
            //list.Add(string.Format("Dead Load + SIDL -Reaction 239.51   t "));

            double Val1 = tab2.VERT_LOAD_VALUES[0];

            list.Add(string.Format("Dead Load + SIDL - Reaction = {0:f3} t", Val1));
            double LLR2 = MyList.StringToDouble(txt_LLR_2.Text, 0.0); //user input
            list.Add(string.Format("Live Load Reaction at free end - Max Long Moment Case = {0:f3} t", LLR2));
            //list.Add(string.Format("Live Load Reaction at free end - Max Long Moment Case 0.000   t "));

            double dl_sidl = 2 * Val1;

            list.Add(string.Format("Dead Load + SIDL - Full Span = 2 x {0:f3} = {1:f3} t", Val1, dl_sidl));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Normal Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case  "));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format(""));


            //list.Add(string.Format("Greater of  - I (0-0.05*(239.505+0)) -11.975   t "));
            //list.Add(string.Format("or -  II    (0/2+0.05*(239.505+0)) 11.975   t "));
            //list.Add(string.Format(" 11.975   t "));


            double dval1 = (AHF2 - fc * (Val1 + LLR2));


            list.Add(string.Format("Greater of  - I       ({0:f3}-{1:f3}*({2:f3}+{3:f3}))  = {4:f3} t",
                AHF2, fc, Val1, LLR2, dval1));


            double dval2 = (AHF2 / 2.0 + fc * (Val1 + LLR2));

            double maxV4 = Math.Max(dval1, dval2);
            list.Add(string.Format(""));
            list.Add(string.Format(" Max Longitudinal Moment    = {0:f3} t", maxV4));
            list.Add(string.Format(""));

            //double V51 = (F16 - F15 * (F17 + F18));
            double cg2 = (LIST_H[1] - LIST_H[3] + 0.25);

            //list.Add(string.Format("cg from Pier shaft base (Long) 213.584-205.08+0.25 8.754   m "));
            list.Add(string.Format("cg from Pier shaft base (Long)   ({0:f3} - {1:f3} + 0.25) = {2:f3}  m ",
                LIST_H[1], LIST_H[3], cg2));

            double mom1 = maxV4 * cg2;
            list.Add(string.Format("Moment about Pier Shaft base (Long)  {0:f3} x {1:f3} = {2:f3} t-m", maxV4, cg2, mom1));

            //list.Add(string.Format("Moment about Pier Shaft base (Long)  11.975*8.754   104.831   t-m "));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Free Bearing - Max Longitudinal Moment Case  "));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Horizontal force (0.05*(239.505+0)) 11.975   t "));

            double HF2 = fc * (Val1 + LLR2);
            list.Add(string.Format("Horizontal force  ({0:f3}*({1:f3} + {2:f3})) = {3:f3} t",
                fc, Val1, LLR2, HF2));
            list.Add(string.Format(""));
            //list.Add(string.Format("Moment about Pier Shaft base (Long)  11.975*8.754   104.831   t-m "));
            double mom2 = HF2 * cg2;
            list.Add(string.Format("Moment about Pier Shaft base (Long)   {0:f3} * {1:f3}  = {2:f3} t-m", HF2, cg2, mom2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Case Calculations"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic coefficient : "));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Ah = horizontal seismic coefficient   =   ( Z / 2 ) . ( Sa / g ) / ( R / I )  "));
            //list.Add(string.Format("Z = Zone Factor  =   0.24   (Bridge is Zone IV) "));
            list.Add(string.Format("Z = Zone Factor  = {0:f3} ", Z));
            list.Add(string.Format("I = Importance Factor  =   {0:f3}  ", I));
            list.Add(string.Format("Sa / g = Average response acceleration coefficient  = {0:f3}", Sa_by_g));
            list.Add(string.Format("R = Response reduction factor  = {0:f3}", R));
            list.Add(string.Format("Horizontal seismic coefficient  =  {0:f3}", Ah));

            double VSC = Ah / 2.0;
            //list.Add(string.Format("Vertical seismic coefficient  =   0.090 For Case IV "));
            list.Add(string.Format("Vertical seismic coefficient  = Ah/2 =  {0:f3} / 2 =  {1:f3} ", Ah, VSC));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Seismic Case - Longitudinal "));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case  "));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));

            dval1 = (dl_sidl * Ah + AHF2 / 2.0 - fc * (Val1 + LLR2 / 2));
            //list.Add(string.Format("Greater of  - I (479.01*0.18+0/2-0.05*(239.505+0/2)) 74.247   t "));
            list.Add(string.Format("Greater of  - I ( {0:f3} * {1:f3} +  {2:f3} / 2 -  {3:f3} *({4:f3} + {5:f3}/2))  = {6:f3} t ",
                dl_sidl, Ah, AHF2, fc, Val1, LLR2, dval1));
            list.Add(string.Format(""));

            dval2 = (((dl_sidl * Ah) / 2.0 + AHF2 / 2.0) / 2.0 - fc * (Val1 + LLR2 / 2));


            list.Add(string.Format("or -II ((({0:f3}*{1:f3})/2+{2:f3}/2)/2+{3:f3}*({4:f3}+{5:f3}/2))  = {6:f3} t ",
                dl_sidl, Ah, AHF2, fc, Val1, LLR2, dval2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double max_lm2 = Math.Max(dval1, dval2);
            list.Add(string.Format("Max Longitudinal Moment = max_lm2 = {0:f3} t ", max_lm2));
            list.Add(string.Format(""));
            //list.Add(string.Format("cg from Pier shaft base (Long) 213.584-205.08+0.25 8.754   m "));
            list.Add(string.Format("cg from Pier shaft base (Long) = {0:f3} m", cg2));
            list.Add(string.Format(""));

            double mom3 = max_lm2 * cg2;
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom3 = {0:f3} * {1:f3} = {2:f3} t-m ", max_lm2, cg2, mom3));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Free Bearing - Max Longitudinal Moment Case "));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));
            double HF3 = (fc * (Val1 + LLR2 / 2.0));
            list.Add(string.Format("Horizontal force = HF3 = ({0:f3}*({1:f3}+{2:f3}/2)) = {3:f3} t", fc, Val1, LLR2, HF3));
            list.Add(string.Format(""));

            double mom4 = HF3 * cg2;
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom4 = {0:f3} * {1:f3} = {2:f3} t-m ", HF3, cg2, mom4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Seismic Case - Transvesre "));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("3. Fixed Bearing - Max Longitudinal Moment Case  "));
            list.Add(string.Format("-------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            dval1 = (AHF2 / 2.0 - fc * (Val1 + LLR2 / 2));

            list.Add(string.Format("Greater of  - I ({0:f3} / 2 - {1:f3} * ({2:f3} + {3:f3}/2)) = {4:f3} t ",
                AHF2, fc, Val1, LLR2, dval1));
            list.Add(string.Format(""));


            dval2 = ((AHF2 / 2.0) / 2.0 + fc * (Val1 + LLR2 / 2));

            list.Add(string.Format("or -II (({0:f3}/2)/2+{1:f3}*({2:f3}+{3:f3}/2)) = {4:f3} t ",
                AHF2, fc, Val1, LLR2, dval2));
            list.Add(string.Format(""));

            double max_lm3 = Math.Max(dval1, dval2);
            list.Add(string.Format(" Max Longitudinal Moment = max_lm3 = {0:f3} t ", max_lm3));

            list.Add(string.Format(""));
            list.Add(string.Format("cg from Pier shaft base (Long) = {0:f3} m", cg2));
            list.Add(string.Format(""));

            double mom5 = max_lm3 * cg2;
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom5 = {0:f3} * {1:f3} = {2:f3} t-m ",
                max_lm3, cg2, mom5));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces and Moments at the Base of Pier - One Span Dislodged Condition "));
            list.Add(string.Format("----------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Normal Case"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format("I: Max Longitudinal Moment Case  "));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));


            double VL1 = Val1 + tab2.VERT_LOAD_VALUES[1] + tab2.VERT_LOAD_VALUES[3] + tab2.VERT_LOAD_VALUES[4];

            //list.Add(string.Format("Vertical Load 239.505++111.24+168.58 519.33   t "));
            list.Add(string.Format("Vertical Load = VL1 = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} t ",
                Val1, tab2.VERT_LOAD_VALUES[1], tab2.VERT_LOAD_VALUES[3], tab2.VERT_LOAD_VALUES[4], VL1));
            list.Add(string.Format(""));

            double TM1 = tab2.MOMENT_CG_SHAFT_TRANS[1];

            list.Add(string.Format("Transverse Moment = TM1 = {0:f3} t-m", TM1));
            list.Add(string.Format(""));

            double LM1 = tab2.MOMENT_CG_SHAFT_LONG[1] + mom1;

            list.Add(string.Format("Longitudinal Moment = LM1 = {0:f3} + {1:f3} = {2:f3} t-m ",
                tab2.MOMENT_CG_SHAFT_LONG[1], mom1, LM1));
            list.Add(string.Format(""));


            SLC_Pier_Base_One_span.Vertical_Load.Add(VL1);
            SLC_Pier_Base_One_span.Mll.Add(LM1);
            SLC_Pier_Base_One_span.Mtt.Add(TM1);


            list.Add(string.Format(""));
            list.Add(string.Format("II:  Max Transverse Moment Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double VL2 = Val1 + LLR2 + tab2.VERT_LOAD_VALUES[3] + tab2.VERT_LOAD_VALUES[4];

            //list.Add(string.Format("Vertical Load = VL2 = 239.505+0+111.24+168.58 519.33   t "));
            list.Add(string.Format("Vertical Load = VL2 = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} t ",
               Val1, LLR2, tab2.VERT_LOAD_VALUES[3], tab2.VERT_LOAD_VALUES[4], VL2));


            list.Add(string.Format(""));
            double TM2 = tab2.MOMENT_CG_SHAFT_TRANS[2];
            list.Add(string.Format("Transverse Moment = TM2 = {0:f3} t-m ", TM2));
            list.Add(string.Format(""));
            double LM2 = tab2.MOMENT_CG_SHAFT_LONG[2] + mom1;
            list.Add(string.Format("Longitudinal Moment = LM2 = {0:f3} + {1:f3} = {2:f3} t-m ",
                tab2.MOMENT_CG_SHAFT_LONG[2], mom1, LM2));
            list.Add(string.Format(""));



            SLC_Pier_Base_One_span.Vertical_Load.Add(VL2);
            SLC_Pier_Base_One_span.Mll.Add(LM2);
            SLC_Pier_Base_One_span.Mtt.Add(TM2);


            B2 = LIST_B[1];
            double MI1 = Math.PI * Math.Pow(B2, 4.0) / 64.0;
            list.Add(string.Format("Moment of Inertia = MI1 = π * B2^4 / 64 = {0:f3} * {1:f3} ^ 4 / 64 = {2:f3} m^4 ",
                Math.PI, B2, MI1));
            list.Add(string.Format(""));

            double ar = Math.PI * Math.Pow(B2, 2.0) / 4.0;
            list.Add(string.Format("Area = ar = π * B2^2 / 4 = {0:f3} * {1:f3} ^ 2 / 4 = {2:f3} m^4 ",
                Math.PI, B2, ar));
            list.Add(string.Format(""));

            D3 = LIST_D[2];
            list.Add(string.Format("(Super Structure on one side condition) - 1.75 x L  "));
            list.Add(string.Format(""));

            double eff_len1 = 1.75 * D3;
            list.Add(string.Format("Effective Length of Pier = eff_len1 = 1.75 x {0:f3} = {1:f3} m", D3, eff_len1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("(Service with simply supported span on both sides and pier fixed at base condition) - 1.2 x L  "));
            list.Add(string.Format(""));
            double eff_len2 = 1.2 * D3;
            list.Add(string.Format("Effective Length of Pier = eff_len2 = 1.2 x {0:f3} = {1:f3} m", D3, eff_len2));
            //list.Add(string.Format("Effective Length of Pier 1.2*6.504 7.805   m "));

            double r = Math.Sqrt(MI1 / ar);
            list.Add(string.Format("Radius of gyration = r = SQRT(MI1/ar) = SQRT({0:f3}/{1:f3}) = {2:f3}", MI1, ar, r));
            list.Add(string.Format(""));

            double lr1 = eff_len1 / r;

            list.Add(string.Format("L/r - Super Structure on one side condition = {0:f3} / {1:f3} = {2:f3}", eff_len1, r, lr1));
            list.Add(string.Format(""));
            double lr2 = eff_len2 / r;
            list.Add(string.Format("L/r - Service condition  = {0:f3} / {1:f3} = {2:f3}", eff_len2, r, lr2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Case Calculations "));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Seismic Case: (Longitudinal)"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Horizontal Seismic Coefficient = Ah = {0:f3}", Ah));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No   Horizontal Load   Value (t)   Acting at a distance from pile cap top (m)   Moment about Pile Cap Top (t-m) "));
            //list.Add(string.Format("1   On Dead Load + SIDL   86.222   9.722   838.25 "));
            //list.Add(string.Format("2   On Pier Cap   20.02   7.752   155.211 "));
            //list.Add(string.Format("3   On Pier Shaft   30.35   3.25   98.68 "));
            //list.Add(string.Format("Total 136.59 1092.14 "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Seismic Case: (Transverse)   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No   Horizontal Load   Value (t)   Acting at a distance from pile cap top (m)   Moment about Pile Cap Top (t-m) "));
            //list.Add(string.Format("1   On Dead Load + SIDL   43.111   9.722   419.12 "));
            //list.Add(string.Format("2   On Pier Cap   20.02   7.752   155.21 "));
            //list.Add(string.Format("3   On Pier Shaft   30.35   3.25   98.68 "));
            //list.Add(string.Format("Total 93.48 673.02 "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Longitudinal Seismic Case   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Vertical Load 239.505+0.5*+111.24+168.58 519.33   t "));
            //list.Add(string.Format("Transverse Moment 0.5*0.000 0.000   t-m "));
            //list.Add(string.Format("Longitudinal Moment 0+1,092.142 1092.142   t-m "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Transverse Seismic Case   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("Vertical Load 239.505+0.5*+111.24+168.58 519.33   t "));
            //list.Add(string.Format("Transverse Moment 0.5*0.000+673.018 673.018   t-m "));
            //list.Add(string.Format("Longitudinal Moment +104.831 104.831   t-m "));
            list.Add(string.Format(""));



            #region On Dead Load + SIDL
            list.Add(string.Format("On Dead Load + SIDL "));
            list.Add(string.Format("---------------------"));

            list.Add(string.Format(""));
            double hl1 = Ah * dl_sidl;
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, dl_sidl, hl1));
            list.Add(string.Format(""));


            //double V27 ='Design-Input'!$H$6-'Design-Input'!$H$21+0.25+0.5*('Design-Input'!$H$3-'Design-Input'!$H$6)
            double ac1 = LIST_H[1] - LIST_H[3] + ((LIST_H[2] - LIST_H[3]) / 2.0) + 0.5 * (LIST_H[0] - LIST_H[1]);
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} - {1:f3} + {2:f3} + 0.5*({3:f3} - {0:f3})",
                LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), LIST_H[0]));
            list.Add(string.Format("                                       = {0:f3} m", V27));
            list.Add(string.Format(""));

            double mom_top1 = hl1 * ac1;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl1, ac1, mom_top1));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Dead Load + SIDL
            list.Add(string.Format("On Pier Cap"));
            list.Add(string.Format("-----------"));

            list.Add(string.Format(""));

            double hl2 = Ah * tab2.VERT_LOAD_VALUES[3];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[3], hl2));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            double ac2 = D3 + CG;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} + {1:f3} = {2:f3})",
                D3, CG, ac2));
            list.Add(string.Format(""));
            double mom_top2 = hl2 * ac2;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl2, ac2, mom_top2));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Pier Shaft
            list.Add(string.Format("On Pier Shaft"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            double hl3 = Ah * tab2.VERT_LOAD_VALUES[4];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[4], hl3));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            double ac3 = 0.5 * D3;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} / 2 = {1:f3})",
                D3, ac3));
            list.Add(string.Format(""));

            double mom_top3 = hl3 * ac3;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl3, ac3, mom_top3));
            list.Add(string.Format(""));

            #endregion On Pier Shaft

            #region Total

            list.Add(string.Format(""));
            double THL1 = hl1 + hl2 + hl3;
            list.Add(string.Format("Total Horizontal Load = {0:f3} + {1:f3} + {2:f3} = {3:f3} t",
                hl1, hl2, hl3, THL1));
            list.Add(string.Format(""));


            double tot_mom_top1 = mom_top1 + mom_top2 + mom_top3;

            list.Add(string.Format("Total Moment about Pile Cap Top = {0:f3} + {1:f3} + {2:f3}  = {3:f3} t-m",
                mom_top1, mom_top2, mom_top3, tot_mom_top1));
            list.Add(string.Format(""));

            #endregion Total




            list.Add(string.Format(""));
            list.Add(string.Format(""));




            list.Add(string.Format("Seismic Case: (Transverse)   "));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("S.No   Horizontal Load   Value (t)   Acting at a distance from pile cap top (m)   Moment about Pile Cap Top (t-m) "));
            //list.Add(string.Format("1   On Dead Load + SIDL   86.22   9.722   838.25 "));
            //list.Add(string.Format("2   On Pier Cap   20.02   7.75   155.21 "));
            //list.Add(string.Format("3   On Pier Shaft   30.35   3.25   98.68 "));
            //list.Add(string.Format("4   Live Load   4.21   11.454   48.23 "));
            //list.Add(string.Format("Total 140.80 1140.37 "));



            #region On Dead Load + SIDL
            list.Add(string.Format("On Dead Load + SIDL "));
            list.Add(string.Format("---------------------"));

            list.Add(string.Format(""));
            double hl4 = Ah * dl_sidl * 0.5;
            list.Add(string.Format("Horizontal Load = 0.5 * {0:f3} * {1:f3} = {2:f3} t", Ah, dl_sidl, hl4));
            list.Add(string.Format(""));


            //double V27 ='Design-Input'!$H$6-'Design-Input'!$H$21+0.25+0.5*('Design-Input'!$H$3-'Design-Input'!$H$6)
            double ac4 = LIST_H[1] - LIST_H[3] + ((LIST_H[2] - LIST_H[3]) / 2.0) + 0.5 * (LIST_H[0] - LIST_H[1]);
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} - {1:f3} + {2:f3} + 0.5*({3:f3} - {0:f3})",
                LIST_H[1], LIST_H[3], ((LIST_H[2] - LIST_H[3]) / 2.0), LIST_H[0]));
            list.Add(string.Format("                                       = {0:f3} m", ac4));
            list.Add(string.Format(""));

            double mom_top4 = hl4 * ac4;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl4, ac4, mom_top4));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Dead Load + SIDL
            list.Add(string.Format("On Pier Cap"));
            list.Add(string.Format("-----------"));

            list.Add(string.Format(""));

            double hl5 = Ah * tab2.VERT_LOAD_VALUES[3];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[3], hl5));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            double ac5 = D3 + CG;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} + {1:f3} = {2:f3})",
                D3, CG, ac5));
            list.Add(string.Format(""));
            double mom_top5 = hl5 * ac5;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl5, ac5, mom_top5));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Pier Shaft
            list.Add(string.Format("On Pier Shaft"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            double hl6 = Ah * tab2.VERT_LOAD_VALUES[4];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[4], hl6));
            list.Add(string.Format(""));


            //double V30 =='Design-Input'!$G$14+$F$49
            double ac6 = 0.5 * D3;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} / 2 = {1:f3})",
                D3, ac3));
            list.Add(string.Format(""));

            double mom_top6 = hl6 * ac6;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl6, ac6, mom_top6));
            list.Add(string.Format(""));

            #endregion On Pier Shaft



            #region Total

            list.Add(string.Format(""));
            double THL2 = hl4 + hl5 + hl6;
            list.Add(string.Format("Total Horizontal Load = {0:f3} + {1:f3} + {2:f3} = {3:f3} t", hl4, hl5, hl6, THL2));
            list.Add(string.Format(""));


            double tot_mom_top2 = mom_top4 + mom_top5 + mom_top6;


            list.Add(string.Format("Total Moment about Pile Cap Top = {0:f3} + {1:f3} + {2:f3} = {3:f3} t-m",
                mom_top4, mom_top5, mom_top6, tot_mom_top2));
            list.Add(string.Format(""));

            #endregion Total




            list.Add(string.Format("Longitudinal Seismic Case"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));

            double VL3 = Val1 + 0.5 * tab2.VERT_LOAD_VALUES[1] + tab2.VERT_LOAD_VALUES[3] + tab2.VERT_LOAD_VALUES[4];


            list.Add(string.Format("Vertical Load = {0:f3} + 0.5 * {1:f3} + {2:f3} + {3:f3}   = {4:f3} t ",
                Val1,
                tab2.VERT_LOAD_VALUES[1],
                tab2.VERT_LOAD_VALUES[3],
                tab2.VERT_LOAD_VALUES[4],
                VL3));

            list.Add(string.Format(""));


            double TM3 = 0.5 * tab2.MOMENT_CG_SHAFT_TRANS[1];
            list.Add(string.Format("Transverse Moment = 0.5 * {0:f3}  = {1:f3} t-m", tab2.MOMENT_CG_SHAFT_TRANS[1], TM3));
            list.Add(string.Format(""));

            double LM3 = tab2.MOMENT_CG_SHAFT_LONG[1] + tot_mom_top1;
            list.Add(string.Format("Longitudinal Moment = {0:f3} + {1:f3} = {2:f3} t-m ",
                tab2.MOMENT_CG_SHAFT_LONG[1], tot_mom_top1, LM3));
            list.Add(string.Format(""));



            SLC_Pier_Base_One_span.Vertical_Load.Add(VL3);
            SLC_Pier_Base_One_span.Mll.Add(LM3);
            SLC_Pier_Base_One_span.Mtt.Add(TM3);




            list.Add(string.Format(""));
            list.Add(string.Format("Transverse Seismic Case"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double VL4 = Val1 + 0.5 * tab2.VERT_LOAD_VALUES[2] + tab2.VERT_LOAD_VALUES[3] + tab2.VERT_LOAD_VALUES[4];

            //list.Add(string.Format("Vertical Load 479.01+0.5*46.782+111.24+168.58 782.22   t "));
            //list.Add(string.Format("Transverse Moment 0.5*138.007+1,140.367 1209.371   t-m "));
            //list.Add(string.Format("Longitudinal Moment 51.4602+256.843 308.303   t-m "));
            list.Add(string.Format(""));


            list.Add(string.Format("Vertical Load = {0:f3} + 0.5 * {1:f3} + {2:f3} + {3:f3}   = {4:f3} t ",
                Val1,
                tab2.VERT_LOAD_VALUES[2],
                tab2.VERT_LOAD_VALUES[3],
                tab2.VERT_LOAD_VALUES[4],
                VL4));
            list.Add(string.Format(""));

            double TM4 = 0.5 * tab2.MOMENT_CG_SHAFT_TRANS[2] + tot_mom_top2;
            list.Add(string.Format("Transverse Moment = 0.5 * {0:f3} + {1:f3}  = {2:f3} t-m",
                tab1.MOMENT_CG_SHAFT_TRANS[2], tot_mom_top2, TM4));
            list.Add(string.Format(""));

            double LM4 = tab2.MOMENT_CG_SHAFT_LONG[2] + mom5;
            list.Add(string.Format("Longitudinal Moment = {0:f3} + {1:f3} = {2:f3} t-m ",
                tab2.MOMENT_CG_SHAFT_LONG[2], mom5, LM4));
            list.Add(string.Format(""));



            SLC_Pier_Base_One_span.Vertical_Load.Add(VL4);
            SLC_Pier_Base_One_span.Mll.Add(LM4);
            SLC_Pier_Base_One_span.Mtt.Add(TM4);


            #endregion STEP 2 : ONE SPAN DISLODGED CONDITION

            #region STEP 3 : LOAD CALCULATIONS - AT PILE CAP BASE
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 3 : LOAD CALCULATIONS - AT PILE CAP BASE"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Load Calculations of Pile Cap and soil wt above pile cap "));
            list.Add(string.Format(""));
            double dp = MyList.StringToDouble(txt_dp.Text, 0.0);
            int tp = MyList.StringToInt(txt_tp.Text, 0);
            int tr = MyList.StringToInt(txt_tr.Text, 0);


            list.Add(string.Format(""));
            list.Add(string.Format("Total No. of Pile = tp = {0} nos", tp));
            list.Add(string.Format("Total No. of row = tr = {0} nos", tr));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double wpcl = LIST_S[2] + LIST_S[3] + LIST_S[4];
            list.Add(string.Format("Total Width of Pile Cap in Longitudinal Dircetion = wpcl = S3 + S4 + S5"));
            list.Add(string.Format("                                                         = {0:f3} + {1:f3} + {2:f3}", LIST_S[2], LIST_S[3], LIST_S[4]));
            list.Add(string.Format("                                                         = {0:f3} m", wpcl));
            list.Add(string.Format(""));
            double wpct = LIST_S[5] + (tr - 1) * LIST_S[6] + LIST_S[7];
            list.Add(string.Format("Total Width of Pile Cap in Transverse direction = wpct = S6 + tr x S7 + S8"));
            list.Add(string.Format("                                                       = {0:f3} + {1} x {2:f3} + {3:f3}", LIST_S[5], (tr - 1), LIST_S[6], LIST_S[7]));
            list.Add(string.Format("                                                       = {0:f3} m", wpct));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("Depth of pile cap = dp = {0:f3} m", dp));

            double pcwt = wpcl * wpct * LIST_D[3] * conc_den;

            list.Add(string.Format("Self Weight of Pile cap = pcwt = wpcl * wpct * dp * conc_den"));
            list.Add(string.Format("                               = {0:f3} * {1:f3} * {2:f3} * {3:f3}",
                wpcl, wpct, LIST_D[3], conc_den));
            list.Add(string.Format("                               = {0:f3} t", pcwt));
            list.Add(string.Format(""));

            double swt = wpcl * wpct * (LIST_H[2] - LIST_H[3]) * soil_den;

            list.Add(string.Format("Soil weight above pile cap = swt = wpcl * wpct * (H3 - H4) * soil_den"));
            list.Add(string.Format("                                 = {0:f3} * {1:f3} * ({2:f3} - {3:f3}) * {4:f3}",
                wpcl, wpct, LIST_H[2], LIST_H[3], conc_den));
            list.Add(string.Format("                                 = {0:f3} t", swt));
            list.Add(string.Format(""));
            //list.Add(string.Format("Soil weight above pile cap     39.933 t  "));
            //list.Add(string.Format("5.1*8.7*(205.58-205.08)*1.8 "));
            list.Add(string.Format(""));
            //list.Add(string.Format("Normal Case "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No Vertical Load Value (t) Moment About Cg of Pile Cap     "));
            //list.Add(string.Format("   Distance w.r.t c.g of Pile cap (m) in Long Moment about c.g of  Pile Cap in Long (t-m) Distance w.r.t c.g of Pile Cap (m) in Trans Moment about c.g of  Pile Cap in Trans (t-m)  "));
            //list.Add(string.Format("1 Dead Load + SIDL 479.01 0.000 0.000 0.000 0.000  "));
            //list.Add(string.Format("2 Live Load (Max Long Moment) 92.00 1.100 101.200 1.155 106.260  "));
            //list.Add(string.Format("3 Live Load (Max Trans Moment) 46.78 1.100 51.460 2.950 138.007  "));
            //list.Add(string.Format("4 Pier Cap  111.24 0.000 0.000 0.000 0.000  "));
            //list.Add(string.Format("5 Pier Shaft 168.58 0.000 0.000 0.000 0.000  "));
            list.Add(string.Format(""));

            list.Add(string.Format("Normal Case"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                              --------------------   Moment About Pier     ---------------------"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No  Vertical Load                Value (t)   Distance w.r.t    Moment about    Distance w.r.t    Moment about"));
            list.Add(string.Format("                                                c.g of Shaft     c.g of Shaft    c.g of Shaft     c.g of Shaft"));
            list.Add(string.Format("                                                (m) in Long      in Long (t-m)   (m) in Trans     in Trans (t-m) "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));

            for (i = 0; i < tab3.VERT_LOAD_NAMES.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-28} {2,9:f3} {3,12:f3} {4,17:f3} {5,16:f3} {6,18:f3} ", (i + 1),
                    tab3.VERT_LOAD_NAMES[i],
                    tab3.VERT_LOAD_VALUES[i],
                    tab3.DIST_CG_SHAFT_LONG[i],
                    tab3.MOMENT_CG_SHAFT_LONG[i],
                    tab3.DIST_CG_SHAFT_TRANS[i],
                    tab3.MOMENT_CG_SHAFT_TRANS[i]
                    ));
            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));






            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Force (As per Clause 214.5.1 of IRC:6-2000) "));
            list.Add(string.Format(""));
            list.Add(string.Format("Coefficent of friction = fc = {0:f3}", fc));
            list.Add(string.Format("Applied Horizontal force = AHF1 = {0:f3} t", AHF1));

            Val1 = tab3.VERT_LOAD_VALUES[0];

            list.Add(string.Format("Dead Load + SIDL Reaction at Pier - From both spans = {0:f3} t", Val1));
            list.Add(string.Format("Live Load Reaction at free end = LLR1 = {0:f3} t", LLR1));
            list.Add(string.Format("Dead Load + SIDL -Full Span = {0:f3} t", Val1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Normal Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Greater of  - I  (20-0.05*(479.01+15.581))   -4.730 t  "));
            dval1 = AHF1 - fc * (Val1 + LLR1);
            list.Add(string.Format("Greater of  - I = (AHF1 - fc * (Val1 + LLR1))"));
            list.Add(string.Format("                = ({0:f3} - {1:f3} * ({2:f3} + {3:f3}))", AHF1, fc, Val1, LLR1));
            list.Add(string.Format("                = {0:f3} t", dval1));
            list.Add(string.Format(""));

            dval2 = (AHF1 / 2 + fc * (Val1 + LLR1));
            //list.Add(string.Format("or          -II = (20/2+0.05*(479.01+15.581))   34.730 t  "));
            list.Add(string.Format("or          -II = (AHF1 / 2 + fc * (Val1 + LLR1))"));
            list.Add(string.Format("                = ({0:f3} / 2 + {1:f3} * ({2:f3} + {3:f3}))", AHF1, fc, Val1, LLR1));
            list.Add(string.Format("                = {0:f3} t", dval2));
            list.Add(string.Format(""));

            double mlm1 = Math.Max(dval1, dval2);


            list.Add(string.Format("Maximum Longitudinal Moment = mlm1 = {0:f3} t", mlm1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double cg1 = LIST_H[1] - LIST_H[4] + 0.25;
            //list.Add(string.Format("cg from Pier shaft base (Long) = cg1 = 213.584-203.28+0.25  10.554 m  "));
            list.Add(string.Format("cg from Pier shaft base (Long) = cg1 = H2 - H5 + 0.25"));
            list.Add(string.Format("                                     = {0:f3} - {0:f3} + 0.25", LIST_H[1], LIST_H[4]));
            list.Add(string.Format("                                     = {0:f3} m", cg1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double mom_long1 = mlm1 * cg1;

            //list.Add(string.Format("Moment about Pier Shaft base (Long) = 34.730*10.554 366.536 t-m  "));
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom_long1 = mlm1 * cg1"));
            list.Add(string.Format("                                                = {0:f3} * {1:f3}", mlm1, cg1));
            list.Add(string.Format("                                                = {0:f3} t-m", mom_long1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Free Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));

            double hf1 = fc * (Val1 + LLR1);

            //list.Add(string.Format("Horizontal force  (0.05*(479.01+15.581))   24.730 t  "));
            list.Add(string.Format("Horizontal force = hf1 = (fc * (Val1 + LLR1))"));
            list.Add(string.Format("                       = ({0:f3} * ({0:f3} + {0:f3}))", fc, Val1, LLR1));
            list.Add(string.Format("                       = {0:f3} t", hf1));
            list.Add(string.Format(""));

            double mom_long2 = hf1 * cg1;

            //list.Add(string.Format("Moment about Pier Shaft base (Long) =    24.730*10.554 260.996 t-m  "));
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom_long2 = hf1 * cg1"));
            list.Add(string.Format("                                                = {0:f3} * {1:f3}", hf1, cg1));
            list.Add(string.Format("                                                = {0:f3} t-m", mom_long2));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Seismic Case - Longitudinal "));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Seismic Coefficient = Ah = {0:f3} ", Ah));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Fixed Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Greater of  - I  (479.01*0.18+20/2-0.05*(479.01+15.581/2))   71.882 t  "));

            dval1 = Val1 * Ah + AHF1 / 2 - fc * (Val1 + LLR1 / 2.0);
            list.Add(string.Format("Greater of  - I  = (Val1 * Ah + AHF1 / 2 - fc * (Val1 + LLR1 / 2.0)) "));
            list.Add(string.Format("                 = ({0:f3} * {1:f3} + {2:f3} / 2 - {3:f3} * ({0:f3} + {4:f3} / 2.0))",
                Val1, Ah, AHF1, fc, LLR1));
            list.Add(string.Format("                 = {0:f3} t", dval1));


            dval2 = ((Val1 * Ah) / 2 * (AHF1 / 2.0) / 2.0 + fc * (Val1 + LLR1 / 2));

            list.Add(string.Format("or    - II  = (({0:f3} * {1:f3}) / 2 * ({2:f3} / 2.0) / 2.0 + {3:f3} * ({0:f3} + {4:f3} / 2))",
                               Val1, Ah, AHF1, fc, LLR1));
            list.Add(string.Format("            = {0:f3} t", dval2));

            double mlm2 = Math.Max(dval1, dval2);
            list.Add(string.Format(" Maximum Longitudinal Moment = mlm2 = {0:f3} t", mlm2));


            list.Add(string.Format(""));
            //list.Add(string.Format("cg from Pier shaft base (Long)   213.584-203.28+0.25  10.554 m  "));
            double mom_long3 = mlm2 * cg1;
            list.Add(string.Format("Moment about Pier Shaft base (Long)  = mom_long3 = mlm2 * cg1"));
            list.Add(string.Format("                                     = {0:f3} * {1:f3}", mlm2, cg1));
            list.Add(string.Format("                                     = {0:f3} t-m", mom_long3));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Free Bearing - Max Longitudinal Moment Case "));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));


            HF2 = fc * (Val1 + LLR1 / 2.0);
            //list.Add(string.Format("Horizontal force  (0.05*(479.01+15.581/2))   24.340 t  "));
            list.Add(string.Format("Horizontal force  (fc * (Val1 + LLR1 / 2.0))"));
            list.Add(string.Format("                  ({0:f3} * ({1:f3} + {2:f3} / 2.0))", fc, Val1, LLR1));
            list.Add(string.Format("                  ({0:f3} * ({0:f3} + {0:f3} / 2.0))   24.340 t  ", HF2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double mom_long4 = HF2 * cg1;

            //list.Add(string.Format("Moment about Pier Shaft base (Long)    24.340*10.554 256.885 t-m  "));
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom_long4 = hf2 * cg1"));
            list.Add(string.Format("                                                = {0:f3} * {1:f3} t-m", HF2, cg1));
            list.Add(string.Format("                                                = {0:f3} t-m", mom_long4));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Force at Bearings : Seismic Case - Transverse"));
            list.Add(string.Format("---------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("3. Fixed Bearing - Max Longitudinal Moment Case"));
            list.Add(string.Format("-----------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            dval1 = AHF1 / 2 - fc * (Val1 + LLR1 / 2);

            list.Add(string.Format("Greater of  - I = (AHF1 / 2 - fc * (Val1 + LLR1 / 2))"));
            list.Add(string.Format("                = ({0:f3} / 2 - {1:f3} * ({2:f3} + {3:f3} / 2))", AHF1, fc, Val1, LLR1));
            list.Add(string.Format("                = {0:f3} t ", dval1));
            list.Add(string.Format(""));

            dval2 = (AHF1 / 2) / 2 + fc * (Val1 + LLR1 / 2);
            //list.Add(string.Format("or -II  (20/2)/2+0.05*(479.01+15.581/2))   29.340 t  "));
            list.Add(string.Format("or   - II = ((AHF1 / 2) / 2 + fc * (Val1 + LLR1 / 2))"));
            list.Add(string.Format("          = (({0:f3} / 2) / 2 + {0:f3} * ({0:f3} + {0:f3} / 2))",
                AHF1, fc, Val1, LLR1));
            list.Add(string.Format("          = {0:f3} t", dval2));
            list.Add(string.Format(""));

            double mlm3 = Math.Max(dval1, dval2);
            list.Add(string.Format(" Maximum Longitudinal Moment = mlm3 = {0:f3} t", mlm3));
            list.Add(string.Format(""));




            //list.Add(string.Format("cg from Pier shaft base (Long)   213.584-203.28+0.25  10.554 m  "));

            double mom_long5 = mlm3 * cg1;
            //list.Add(string.Format("Moment about Pier Shaft base (Long)    29.340*10.554 309.655 t-m  "));
            list.Add(string.Format("Moment about Pier Shaft base (Long) = mom_long5 = mlm3 * cg1"));
            list.Add(string.Format("                                                = {0:f3} * {1:f3}", mlm3, cg1));
            list.Add(string.Format("                                                = {0:f3} t-m", mom_long5));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces and Moments at the Base of Pile Cap "));
            list.Add(string.Format("------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Normal Case"));
            list.Add(string.Format("-----------"));
            list.Add(string.Format(""));
            list.Add(string.Format("I: Max Longitudinal Moment Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));


            VL1 = Val1 + tab3.VERT_LOAD_VALUES[1] + tab3.VERT_LOAD_VALUES[3] + tab3.VERT_LOAD_VALUES[4]
                 + pcwt + swt;

            //list.Add(string.Format("Vertical Load  479.01+92+111.24+168.58+191.68+39.93   1082.45 t  "));
            list.Add(string.Format("Vertical Load = vl1 = Val1 + Val2 + Val4 + Val5 + pcwt + swt"));
            list.Add(string.Format("                    = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                Val1, tab3.VERT_LOAD_VALUES[1], tab3.VERT_LOAD_VALUES[3], tab3.VERT_LOAD_VALUES[4], pcwt, swt));
            list.Add(string.Format("                    = {0:f3} t", VL1));

            list.Add(string.Format(""));

            TM1 = tab3.MOMENT_CG_SHAFT_TRANS[1];

            list.Add(string.Format("Transverse Moment = tm1 = {0:f3} t-m", TM1));
            list.Add(string.Format(""));

            LM1 = tab3.MOMENT_CG_SHAFT_LONG[1] + tab3.MOMENT_CG_SHAFT_LONG[3] + tab3.MOMENT_CG_SHAFT_LONG[4] + mom_long1;
            //list.Add(string.Format("Longitudinal Moment  101.200+0.000+0.000+366.536   467.736 t-m  "));
            list.Add(string.Format("Longitudinal Moment = lm1 = {0:f3} + {1:f3} + {2:f3} + {3:f3}",
                tab3.MOMENT_CG_SHAFT_LONG[1], tab3.MOMENT_CG_SHAFT_LONG[3], tab3.MOMENT_CG_SHAFT_LONG[4], mom_long1));


            list.Add(string.Format("                          = {0:f3} t-m", LM1));


            SLC_Pile_Cap.Vertical_Load.Add(VL1);
            SLC_Pile_Cap.Mll.Add(LM1);
            SLC_Pile_Cap.Mtt.Add(TM1);


            list.Add(string.Format(""));
            list.Add(string.Format("II:  Max Transverse Moment Case"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));

            VL2 = Val1 + tab3.VERT_LOAD_VALUES[2] + LLR1 + tab3.VERT_LOAD_VALUES[3] + tab3.VERT_LOAD_VALUES[4] + pcwt + swt;

            //list.Add(string.Format("Vertical Load  479.01+46.782+111.24+168.58+191.68+39.93   1052.81 t  "));

            list.Add(string.Format("Vertical Load = vl2 = Val1 + Val3 + LLR1 + Val4 + Val5 + pcwt + swt"));
            list.Add(string.Format("                    = {0:f3} + {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3} + {6:f3}",
                Val1, tab3.VERT_LOAD_VALUES[2], LLR1, tab3.VERT_LOAD_VALUES[3], tab3.VERT_LOAD_VALUES[4], pcwt, swt));
            list.Add(string.Format("                    = {0:f3} t", VL2));

            list.Add(string.Format(""));
            TM2 = tab3.MOMENT_CG_SHAFT_TRANS[2];

            list.Add(string.Format("Transverse Moment = tm2 = {0:f3} t-m", TM2));
            list.Add(string.Format(""));

            LM2 = tab3.MOMENT_CG_SHAFT_LONG[2] + tab3.MOMENT_CG_SHAFT_LONG[3] + tab3.MOMENT_CG_SHAFT_LONG[4] + mom_long1;

            //list.Add(string.Format("Longitudinal Moment  51.460+0.000+0.000+366.536   417.996 t-m  "));
            list.Add(string.Format("Longitudinal Moment = lm2 = {0:f3} + {1:f3} + {2:f3} + {3:f3}",
                  tab3.MOMENT_CG_SHAFT_LONG[2], tab3.MOMENT_CG_SHAFT_LONG[3], tab3.MOMENT_CG_SHAFT_LONG[4], mom_long1));

            list.Add(string.Format("                          = {0:f3} t-m", LM2));


            SLC_Pile_Cap.Vertical_Load.Add(VL2);
            SLC_Pile_Cap.Mll.Add(LM2);
            SLC_Pile_Cap.Mtt.Add(TM2);


            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Seismic Case"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Seismic Coefficient = Ah = {0:f3}", Ah));
            list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No Horizontal Load Value (t) Acting at a distance from pile cap Base (m) Moment about Pile Cap Base (t-m)    "));
            //list.Add(string.Format("1 On Dead Load + SIDL 86.222 11.522 993.45    "));
            //list.Add(string.Format("2 On Pier Cap 20.02 9.552 191.253    "));
            //list.Add(string.Format("3 On Pier Shaft 30.35 5.05 153.30    "));
            //list.Add(string.Format("Total  136.59  1338.00    "));
            list.Add(string.Format(""));


            #region On Dead Load + SIDL
            list.Add(string.Format("On Dead Load + SIDL "));
            list.Add(string.Format("---------------------"));

            list.Add(string.Format(""));
            hl1 = Ah * Val1;
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, Val1, hl1));
            list.Add(string.Format(""));


            //double V27 ='Design-Input'!$H$6-'Design-Input'!$H$21+0.25+0.5*('Design-Input'!$H$3-'Design-Input'!$H$6)
            ac1 = LIST_H[1] - LIST_H[4] + ((LIST_H[2] - LIST_H[3]) / 2.0) + 0.5 * (LIST_H[0] - LIST_H[1]);
            list.Add(string.Format("Acting at a distance from pile cap Base = {0:f3} - {1:f3} + {2:f3} + 0.5*({3:f3} - {0:f3})",
                LIST_H[1], LIST_H[4], ((LIST_H[2] - LIST_H[3]) / 2.0), LIST_H[0]));
            list.Add(string.Format("                                       = {0:f3} m", ac1));
            list.Add(string.Format(""));

            mom_top1 = hl1 * ac1;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl1, ac1, mom_top1));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Dead Load + SIDL
            list.Add(string.Format("On Pier Cap"));
            list.Add(string.Format("-----------"));

            list.Add(string.Format(""));

            hl2 = Ah * tab2.VERT_LOAD_VALUES[3];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[3], hl2));
            list.Add(string.Format(""));

            D4 = LIST_D[3];
            //double V30 =='Design-Input'!$G$14+$F$49
            ac2 = D3 + CG + D4;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} + {1:f3} + {2:f3} = {3:f3}",
                D3, CG, D4, ac2));
            list.Add(string.Format(""));
            mom_top2 = hl2 * ac2;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl2, ac2, mom_top2));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Pier Shaft
            list.Add(string.Format("On Pier Shaft"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            hl3 = Ah * tab2.VERT_LOAD_VALUES[4];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[4], hl3));
            list.Add(string.Format(""));


            ac3 = 0.5 * D3 + D4;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} / 2 + {1:f3} = {2:f3})",
                D3, D4, ac3));
            list.Add(string.Format(""));

            mom_top3 = hl3 * ac3;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl3, ac3, mom_top3));
            list.Add(string.Format(""));

            #endregion On Pier Shaft

            #region Total

            list.Add(string.Format(""));
            THL1 = hl1 + hl2 + hl3;
            list.Add(string.Format("Total Horizontal Load = {0:f3} + {1:f3} + {2:f3} = {3:f3} t",
                hl1, hl2, hl3, THL1));
            list.Add(string.Format(""));


            tot_mom_top1 = mom_top1 + mom_top2 + mom_top3;

            list.Add(string.Format("Total Moment about Pile Cap Top = {0:f3} + {1:f3} + {2:f3}  = {3:f3} t-m",
                mom_top1, mom_top2, mom_top3, tot_mom_top1));
            list.Add(string.Format(""));

            #endregion Total








            list.Add(string.Format(""));
            list.Add(string.Format("Transverse Seismic Case "));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("S.No Horizontal Load Value (t) Acting at a distance from pile cap top (m) Moment about Pile Cap Top (t-m)    "));
            //list.Add(string.Format("1 On Dead Load + SIDL 86.222 11.522 993.45    "));
            //list.Add(string.Format("2 On Pier Cap 20.02 9.552 191.25    "));
            //list.Add(string.Format("3 On Pier Shaft 30.35 5.05 153.30    "));
            //list.Add(string.Format("4 Live Load 4.21 13.254 55.80    "));
            //list.Add(string.Format("Total  140.80  1393.81    "));
            list.Add(string.Format(""));

            #region On Dead Load + SIDL
            list.Add(string.Format("On Dead Load + SIDL "));
            list.Add(string.Format("---------------------"));

            list.Add(string.Format(""));
            hl1 = Ah * Val1;
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, Val1, hl1));
            list.Add(string.Format(""));


            //double V27 ='Design-Input'!$H$6-'Design-Input'!$H$21+0.25+0.5*('Design-Input'!$H$3-'Design-Input'!$H$6)
            ac1 = LIST_H[1] - LIST_H[4] + ((LIST_H[2] - LIST_H[3]) / 2.0) + 0.5 * (LIST_H[0] - LIST_H[1]);
            list.Add(string.Format("Acting at a distance from pile cap Base = {0:f3} - {1:f3} + {2:f3} + 0.5*({3:f3} - {0:f3})",
                LIST_H[1], LIST_H[4], ((LIST_H[2] - LIST_H[3]) / 2.0), LIST_H[0]));
            list.Add(string.Format("                                       = {0:f3} m", ac1));
            list.Add(string.Format(""));

            mom_top1 = hl1 * ac1;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl1, ac1, mom_top1));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Dead Load + SIDL
            list.Add(string.Format("On Pier Cap"));
            list.Add(string.Format("-----------"));

            list.Add(string.Format(""));

            hl2 = Ah * tab2.VERT_LOAD_VALUES[3];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[3], hl2));
            list.Add(string.Format(""));

            D4 = LIST_D[3];
            //double V30 =='Design-Input'!$G$14+$F$49
            ac2 = D3 + CG + D4;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} + {1:f3} + {2:f3} = {3:f3}",
                D3, CG, D4, ac2));
            list.Add(string.Format(""));
            mom_top2 = hl2 * ac2;

            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl2, ac2, mom_top2));
            list.Add(string.Format(""));

            #endregion On Dead Load + SIDL

            #region On Pier Shaft
            list.Add(string.Format("On Pier Shaft"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            hl3 = Ah * tab2.VERT_LOAD_VALUES[4];
            list.Add(string.Format("Horizontal Load = {0:f3} *  {1:f3} = {2:f3} t", Ah, tab2.VERT_LOAD_VALUES[4], hl3));
            list.Add(string.Format(""));


            ac3 = 0.5 * D3 + D4;
            list.Add(string.Format("Acting at a distance from pile cap top = {0:f3} / 2 + {1:f3} = {2:f3})",
                D3, D4, ac3));
            list.Add(string.Format(""));

            mom_top3 = hl3 * ac3;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl3, ac3, mom_top3));
            list.Add(string.Format(""));

            #endregion On Pier Shaft

            #region Live Load
            list.Add(string.Format("Live Load"));
            list.Add(string.Format("-------------"));

            list.Add(string.Format(""));
            hl4 = 0.5 * Ah * tab3.VERT_LOAD_VALUES[2];
            list.Add(string.Format("Horizontal Load = 0.5 * {0:f3} *  {1:f3} = {2:f3} t", Ah, tab3.VERT_LOAD_VALUES[2], hl4));
            list.Add(string.Format(""));


            ac4 = LIST_H[1] - LIST_H[4] + 1.5 + 1.2 + 0.25;
            list.Add(string.Format("Acting at a distance from pile cap top = H2 - H5 + 1.5 + 1.2 + 0.25"));
            list.Add(string.Format("                                       = {0:f3} - {0:f3} + 1.5 + 1.2 + 0.25", LIST_H[1], LIST_H[4]));
            list.Add(string.Format("                                       = {0:f3} m", ac4));
            list.Add(string.Format(""));

            mom_top4 = hl4 * ac4;


            list.Add(string.Format("Moment about Pile Cap Top = {0:f3} * {1:f3} = {2:f3} ", hl4, ac4, mom_top4));
            list.Add(string.Format(""));

            #endregion On Pier Shaft

            #region Total

            list.Add(string.Format(""));
            THL2 = hl1 + hl2 + hl3 + hl4;
            list.Add(string.Format("Total Horizontal Load = {0:f3} + {1:f3} + {2:f3}  + {3:f3} = {4:f3} t",
                hl1, hl2, hl3, hl4, THL2));
            list.Add(string.Format(""));


            tot_mom_top2 = mom_top1 + mom_top2 + mom_top3 + mom_top4;

            list.Add(string.Format("Total Moment about Pile Cap Top = {0:f3} + {1:f3} + {2:f3} + {3:f3}  = {4:f3} t-m",
                mom_top1, mom_top2, mom_top3, mom_top4, tot_mom_top2));
            list.Add(string.Format(""));

            #endregion Total

            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Seismic Case"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            VL3 = Val1 + 0.5 * tab3.VERT_LOAD_VALUES[1] + tab3.VERT_LOAD_VALUES[3] + tab3.VERT_LOAD_VALUES[4] + pcwt + swt;

            //list.Add(string.Format("Vertical Load  479.01+0.5*92+111.24+168.58+191.68+39.93   1036.45 t  "));
            list.Add(string.Format("Vertical Load = vl3 = {0:f3} + 0.5 * {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                Val1, tab3.VERT_LOAD_VALUES[1], tab3.VERT_LOAD_VALUES[3], tab3.VERT_LOAD_VALUES[4], pcwt, swt));

            list.Add(string.Format("                    = {0:f3} t", VL3));


            TM3 = 0.5 * tab3.MOMENT_CG_SHAFT_TRANS[1];
            list.Add(string.Format("Transverse Moment = tm3 = 0.5 * {0:f3} = {1:f3} t-m  ",
                tab3.MOMENT_CG_SHAFT_TRANS[1], TM3));
            list.Add(string.Format(""));

            LM3 = tab3.MOMENT_CG_SHAFT_LONG[1] + tab3.MOMENT_CG_SHAFT_LONG[3] + tab3.MOMENT_CG_SHAFT_LONG[4] + tot_mom_top1;

            //list.Add(string.Format("Longitudinal Moment = lm3 = 101.200+0.000+0.000+1,338.004   1439.204 t-m  "));

            list.Add(string.Format("Longitudinal Moment = lm3 = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} t-m",
                tab3.MOMENT_CG_SHAFT_LONG[1], tab3.MOMENT_CG_SHAFT_LONG[3], tab3.MOMENT_CG_SHAFT_LONG[4], tot_mom_top1, LM3));
            list.Add(string.Format(""));


            SLC_Pile_Cap.Vertical_Load.Add(VL3);
            SLC_Pile_Cap.Mll.Add(LM3);
            SLC_Pile_Cap.Mtt.Add(TM3);


            list.Add(string.Format(""));
            list.Add(string.Format("Transverse Seismic Case "));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            VL4 = Val1 + 0.5 * tab3.VERT_LOAD_VALUES[2] + tab3.VERT_LOAD_VALUES[3] + tab3.VERT_LOAD_VALUES[4] + pcwt + swt;

            //list.Add(string.Format("Vertical Load  479.01+0.5*92+111.24+168.58+191.68+39.93   1036.45 t  "));
            list.Add(string.Format("Vertical Load = vl4 = {0:f3} + 0.5 * {1:f3} + {2:f3} + {3:f3} + {4:f3} + {5:f3}",
                Val1, tab3.VERT_LOAD_VALUES[1], tab3.VERT_LOAD_VALUES[3], tab3.VERT_LOAD_VALUES[4], pcwt, swt));

            list.Add(string.Format("                    = {0:f3} t", VL4));
            list.Add(string.Format(""));



            TM4 = 0.5 * tab3.MOMENT_CG_SHAFT_TRANS[2] + tot_mom_top2;
            list.Add(string.Format("Transverse Moment = tm3 = 0.5 * {0:f3} + {1:f3} = {2:f3} t-m  ",
                tab3.MOMENT_CG_SHAFT_TRANS[1], tot_mom_top2, TM3));
            list.Add(string.Format(""));


            //list.Add(string.Format("Transverse Moment  0.5*138.007+1,393.808   1462.812 t-m  "));


            //list.Add(string.Format("Longitudinal Moment  51.460+0.000+0.000+309.655   361.115 t-m  "));

            LM4 = tab3.MOMENT_CG_SHAFT_LONG[2] + tab3.MOMENT_CG_SHAFT_LONG[3] + tab3.MOMENT_CG_SHAFT_LONG[4] + mom_long5;

            //list.Add(string.Format("Longitudinal Moment = lm3 = 101.200+0.000+0.000+1,338.004   1439.204 t-m  "));

            list.Add(string.Format("Longitudinal Moment = lm3 = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} t-m",
                tab3.MOMENT_CG_SHAFT_LONG[2], tab3.MOMENT_CG_SHAFT_LONG[3], tab3.MOMENT_CG_SHAFT_LONG[4], mom_long5, LM4));


            SLC_Pile_Cap.Vertical_Load.Add(VL4);
            SLC_Pile_Cap.Mll.Add(LM4);
            SLC_Pile_Cap.Mtt.Add(TM4);


            list.Add(string.Format(""));

            #endregion STEP 3 : LOAD CALCULATIONS - AT PILE CAP BASE

            #region STEP 4 : SUMMARY OF LOAD CASES
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 4 : SUMMARY OF LOAD CASES"));
            list.Add(string.Format("-------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("A) Forces and Moments at the Base of Pier"));
            list.Add(string.Format("-----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Description of Cases               Vertical       Mll       Mtt "));
            list.Add(string.Format("                                           Load (t)     (t-m)      (t-m)"));
            list.Add(string.Format("--------------------------------------------------------------------------"));
            //list.Add(string.Format(""));

            for (i = 0; i < SLC_Pier_Base.Count; i++)
            {
                list.Add(string.Format(" {0,-5} {1,-33} {2,10:f3} {3,10:f3} {4,10:f3}",
                    (i + 1),
                    SLC_Pier_Base.Descriptions[i],
                    SLC_Pier_Base.Vertical_Load[i],
                    SLC_Pier_Base.Mll[i],
                    SLC_Pier_Base.Mtt[i]
                    ));
            }
            list.Add(string.Format("--------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("B) Forces and Moments at the Base of Pier - One Span Dislodged Condition"));
            list.Add(string.Format("------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Description of Cases               Vertical       Mll       Mtt "));
            list.Add(string.Format("                                           Load (t)     (t-m)      (t-m)"));
            list.Add(string.Format("--------------------------------------------------------------------------"));
            //list.Add(string.Format(""));

            for (i = 0; i < SLC_Pier_Base_One_span.Count; i++)
            {
                list.Add(string.Format(" {0,-5} {1,-33} {2,10:f3} {3,10:f3} {4,10:f3}",
                    (i + 1),
                    SLC_Pier_Base_One_span.Descriptions[i],
                    SLC_Pier_Base_One_span.Vertical_Load[i],
                    SLC_Pier_Base_One_span.Mll[i],
                    SLC_Pier_Base_One_span.Mtt[i]
                    ));
            }
            list.Add(string.Format("--------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("C) Forces and Moments at the Base of Pile Cap"));
            list.Add(string.Format("----------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Description of Cases               Vertical       Mll       Mtt "));
            list.Add(string.Format("                                           Load (t)     (t-m)      (t-m)"));
            list.Add(string.Format("--------------------------------------------------------------------------"));
            //list.Add(string.Format(""));

            for (i = 0; i < SLC_Pile_Cap.Count; i++)
            {
                list.Add(string.Format(" {0,-5} {1,-33} {2,10:f3} {3,10:f3} {4,10:f3}",
                    (i + 1),
                    SLC_Pile_Cap.Descriptions[i],
                    SLC_Pile_Cap.Vertical_Load[i],
                    SLC_Pile_Cap.Mll[i],
                    SLC_Pile_Cap.Mtt[i]
                    ));
            }
            list.Add(string.Format("--------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            #endregion STEP 4 : SUMMARY OF LOAD CASES

            #region STEP 5 : DESIGN OF PILE & PILE CAP

            Pile_Cap_CG cgl = new Pile_Cap_CG();
            Pile_Cap_Along algl = new Pile_Cap_Along();

            Pile_Cap_CG cgt = new Pile_Cap_CG();
            Pile_Cap_Along algt = new Pile_Cap_Along();


            List<Pile_Cap_CG> CG_Long = new List<Pile_Cap_CG>();
            List<Pile_Cap_Along> Along_Long = new List<Pile_Cap_Along>();

            List<Pile_Cap_CG> CG_Trans = new List<Pile_Cap_CG>();
            List<Pile_Cap_Along> Along_Trans = new List<Pile_Cap_Along>();

            double pcdia = MyList.StringToDouble(txt_pcdia.Text, 0.0);


            #region C.G. in Longitudinal Direction

            cgl = new Pile_Cap_CG();
            cgl.No_of_Piles = tr;

            int nos = tr + 1;

            cgl.No_of_Piles = tr;

            for (i = nos; i < tp; i++)
            {
                cgl.Pile_Nos += i + ",";
            }
            cgl.Pile_Nos += tp;

            cgl.Pile_Dia = pcdia;
            cgl.Pile_Section = "p-p";
            cgl.Distance_from_section_x = LIST_S[2];

            CG_Long.Add(cgl);


            cgl = new Pile_Cap_CG();
            cgl.No_of_Piles = tr;

            for (i = 1; i < tr; i++)
            {
                cgl.Pile_Nos += i + ",";
            }

            cgl.Pile_Nos += tr;
            cgl.Pile_Dia = pcdia;
            cgl.Pile_Section = "t-t";
            cgl.Distance_from_section_x = LIST_S[2] + LIST_S[3];

            CG_Long.Add(cgl);

            list.Add(string.Format(""));
            list.Add(string.Format("STEP 5 : DESIGN OF PILE & PILE CAP"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("C.G. in Longitudinal Direction"));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("No.of        Pile       Pile        Pile           Area of      Total area      Distance                  "));
            list.Add(string.Format("Piles        No.s       Dia        Section          pile         area in        from section      A*x      "));
            list.Add(string.Format("                        (m)                                     section (A)      A-A (x)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));

            double total_area = 0.0;
            double total_Ax = 0.0;
            for (i = 0; i < CG_Long.Count; i++)
            {
                cgl = CG_Long[i];
                list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,10:f3} {5,15:f3} {6,13:f3} {7,12:f3}",
                   cgl.No_of_Piles,
                   cgl.Pile_Nos,
                   cgl.Pile_Dia,
                   cgl.Pile_Section,
                   cgl.Area_of_pile,
                   cgl.Total_Area_in_section_A,
                   cgl.Distance_from_section_x,
                   cgl.Ax
                   ));

                total_area += cgl.Total_Area_in_section_A;
                total_Ax += cgl.Ax;

            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));

            //list.Add(string.Format(""));


            list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,10:f3} {5,15:f3} {6,13:f3} {7,12:f3}",
               "",
               "",
               "",
               "",
               "",
               total_area,
               "",
               total_Ax
               ));

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double cgpg = total_Ax / total_area;
            list.Add(string.Format("Cg of Pile Group = {0:f3} / {1:f3} = {2:f3} m    from left end Section A-A",
                total_Ax, total_area, cgpg));
            list.Add(string.Format(""));

            double dist_cg = Math.Abs(cgpg - (wpcl / 2));
            list.Add(string.Format("Distance between C/L of brg to cg of pile group = cgpg - (wpcl / 2) "));
            list.Add(string.Format("                                                = {0:f3} - ({1:f3} / 2)", cgpg, wpcl));
            list.Add(string.Format("                                                = {0:f3} m", dist_cg));
            list.Add(string.Format(""));

            #endregion C.G. in Longitudinal Direction

            #region Along Longitudinal Direction



            double E37, C17, D18;
            C17 = wpcl / 2;
            D18 = dist_cg;

            for (i = 1; i <= tr; i++)
            {
                algl = new Pile_Cap_Along();
                algl.Pile_No = i;
                algl.Pile_Dia = pcdia;
                algl.Distance_from_section_B_B = LIST_S[2] + LIST_S[3];

                //=IF((E37-$C$17-$D$18)<0,($C$17+$D$18-E37),(E37-$C$17-$D$18))

                E37 = algl.Distance_from_section_B_B;

                if ((E37 - C17 - D18) < 0)
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (C17 + D18 - E37);
                else
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (E37 - C17 - D18);

                Along_Long.Add(algl);

            }

            for (i = tr + 1; i <= tp; i++)
            {
                algl = new Pile_Cap_Along();
                algl.Pile_No = i;
                algl.Pile_Dia = pcdia;
                algl.Distance_from_section_B_B = LIST_S[2];

                //=IF((E37-$C$17-$D$18)<0,($C$17+$D$18-E37),(E37-$C$17-$D$18))

                E37 = algl.Distance_from_section_B_B;

                if ((E37 - C17 - D18) < 0)
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (C17 + D18 - E37);
                else
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (E37 - C17 - D18);

                Along_Long.Add(algl);

            }

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Along Longitudinal Direction"));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("----------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Pile       Dia of    Area of       Distance from     Distance w.r.t                 Total M.I "));
            list.Add(string.Format("No          pile    pile (m^2)       section          c.g of Pile          d^2      = ∑x^2   "));
            list.Add(string.Format("             (m)       A             A-A (m)         group (m) - d                              "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------"));

            double total_area_ax = 0.0;
            double total_MI_exx = 0.0;

            for (i = 0; i < Along_Long.Count; i++)
            {
                algl = Along_Long[i];
                list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,15:f3} {5,15:f3} {6,13:f3}",
                   algl.Pile_No,
                   algl.Pile_Dia,
                   algl.Area_of_pile_A,
                   algl.Distance_from_section_B_B,
                   algl.Distance_w_r_t_c_g_of_Pile_group_d,
                   algl.dd,
                   algl.Total_M_I
                   ));

                total_area_ax += algl.Area_of_pile_A;
                total_MI_exx += algl.Total_M_I;
            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------"));

            //list.Add(string.Format(""));


            list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,15:f3} {5,15:f3} {6,13:f3}",
               "",
               "",
               total_area_ax,
               "",
               "",
               "",
               total_MI_exx
               ));

            list.Add(string.Format("----------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            cgpg = total_Ax / total_area;
            list.Add(string.Format("Cg of Pile Group = {0:f3} / {1:f3} = {2:f3} m    from left end Section A-A",
                total_Ax, total_area, cgpg));
            list.Add(string.Format(""));

            dist_cg = Math.Abs(cgpg - (wpcl / 2));
            list.Add(string.Format("Distance between C/L of brg to cg of pile group = cgpg - (wpcl / 2) "));
            list.Add(string.Format("                                                = {0:f3} - ({1:f3} / 2)", cgpg, wpcl));
            list.Add(string.Format("                                                = {0:f3} m", dist_cg));
            list.Add(string.Format(""));


            #endregion Along Longitudinal Direction


            list.Add(string.Format(""));


            #region C.G. in Transverse Direction

            cgl = new Pile_Cap_CG();
            cgl.No_of_Piles = tr;

            nos = tr + 1;

            cgl.No_of_Piles = tr;

            List<string> ls = new List<string>();

            ls.Add("w-w");
            ls.Add("v-v");
            ls.Add("u-u");
            ls.Add("t-t");
            ls.Add("s-s");
            ls.Add("r-r");
            ls.Add("r-r");
            ls.Add("q-q");





            for (i = 0; i < tr; i++)
            {
                cgl = new Pile_Cap_CG();
                cgl.No_of_Piles = 2;
                cgl.Pile_Nos += (i + 1) + "," + (i + 1 + tr);
                cgl.Pile_Dia = pcdia;

                cgl.Pile_Section = ls[i];

                cgl.Distance_from_section_x = LIST_S[7] + (tr - 1 - i) * LIST_S[6];


                CG_Trans.Add(cgl);
            }

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("C.G. in Transverse Direction"));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("No.of        Pile       Pile        Pile           Area of      Total area      Distance                  "));
            list.Add(string.Format("Piles        No.s       Dia        Section          pile         area in        from section      A*y      "));
            list.Add(string.Format("                        (m)                                     section (A)      B-B (y)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));

            total_area = 0.0;
            total_Ax = 0.0;
            for (i = 0; i < CG_Trans.Count; i++)
            {
                cgl = CG_Trans[i];
                list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,10:f3} {5,15:f3} {6,13:f3} {7,12:f3}",
                   cgl.No_of_Piles,
                   cgl.Pile_Nos,
                   cgl.Pile_Dia,
                   cgl.Pile_Section,
                   cgl.Area_of_pile,
                   cgl.Total_Area_in_section_A,
                   cgl.Distance_from_section_x,
                   cgl.Ax
                   ));

                total_area += cgl.Total_Area_in_section_A;
                total_Ax += cgl.Ax;

            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));

            //list.Add(string.Format(""));


            list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,10:f3} {5,15:f3} {6,13:f3} {7,12:f3}",
               "",
               "",
               "",
               "",
               "",
               total_area,
               "",
               total_Ax
               ));

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            cgpg = total_Ax / total_area;
            list.Add(string.Format("Cg of Pile Group = {0:f3} / {1:f3} = {2:f3} m    from left end Section B-B",
                total_Ax, total_area, cgpg));
            list.Add(string.Format(""));

            dist_cg = Math.Abs(cgpg - (wpct / 2));
            list.Add(string.Format("Distance between C/L of brg to cg of pile group = cgpg - (wpcl / 2) "));
            list.Add(string.Format("                                                = {0:f3} - ({1:f3} / 2)", cgpg, wpct));
            list.Add(string.Format("                                                = {0:f3} m", dist_cg));
            list.Add(string.Format(""));

            #endregion C.G. in Transverse Direction



            list.Add(string.Format(""));

            #region Along Transverse Direction



            double E61, I12, F56;

            I12 = wpct / 2;
            F56 = dist_cg;
            Along_Trans.Clear();

            for (i = 1; i <= tr; i++)
            {
                algl = new Pile_Cap_Along();
                algl.Pile_No = i;
                algl.Pile_Dia = pcdia;
                algl.Distance_from_section_B_B = LIST_S[7] + (tr - i) * LIST_S[6];

                //=IF((E61-$I$12-$F$56)<0,($I$12+$F$56-E61),(E61-$I$12-$F$56))

                E61 = algl.Distance_from_section_B_B;

                if ((E61 - I12 - F56) < 0)
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (I12 + F56 - E61);
                else
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (E61 - I12 - F56);

                Along_Trans.Add(algl);

            }

            for (i = tr + 1; i <= tp; i++)
            {

                algl = new Pile_Cap_Along();
                algl.Pile_No = i;
                algl.Pile_Dia = pcdia;
                algl.Distance_from_section_B_B = LIST_S[7] + (tp - i) * LIST_S[6];

                //=IF((E61-$I$12-$F$56)<0,($I$12+$F$56-E61),(E61-$I$12-$F$56))

                E61 = algl.Distance_from_section_B_B;

                if ((E61 - I12 - F56) < 0)
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (I12 + F56 - E61);
                else
                    algl.Distance_w_r_t_c_g_of_Pile_group_d = (E61 - I12 - F56);

                Along_Trans.Add(algl);

            }

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Along Transverse Direction"));
            list.Add(string.Format("------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("----------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Pile       Dia of    Area of       Distance from     Distance w.r.t                 Total M.I "));
            list.Add(string.Format("No          pile    pile (m^2)       section          c.g of Pile          d^2      = ∑x^2   "));
            list.Add(string.Format("             (m)       A             A-A (m)         group (m) - d                              "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------"));

            total_area_ax = 0.0;
            double total_MI_eyy = 0.0;

            for (i = 0; i < Along_Trans.Count; i++)
            {
                algl = Along_Trans[i];
                list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,15:f3} {5,15:f3} {6,13:f3}",
                   algl.Pile_No,
                   algl.Pile_Dia,
                   algl.Area_of_pile_A,
                   algl.Distance_from_section_B_B,
                   algl.Distance_w_r_t_c_g_of_Pile_group_d,
                   algl.dd,
                   algl.Total_M_I
                   ));

                total_area_ax += algl.Area_of_pile_A;
                total_MI_eyy += algl.Total_M_I;
            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------"));

            //list.Add(string.Format(""));


            list.Add(string.Format("   {0,-9} {1,-10} {2,-12:f3} {3,-10} {4,15:f3} {5,15:f3} {6,13:f3}",
               "",
               "",
               total_area_ax,
               "",
               "",
               "",
               total_MI_eyy
               ));

            list.Add(string.Format("----------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            cgpg = total_Ax / total_area;
            list.Add(string.Format("Cg of Pile Group = {0:f3} / {1:f3} = {2:f3} m    from left end Section B-B",
                total_Ax, total_area, cgpg));
            list.Add(string.Format(""));

            dist_cg = Math.Abs(cgpg - (wpct / 2));
            list.Add(string.Format("Distance between C/L of brg to cg of pile group = cgpg - (wpct / 2) "));
            list.Add(string.Format("                                                = {0:f3} - ({1:f3} / 2)", cgpg, wpct));
            list.Add(string.Format("                                                = {0:f3} m", dist_cg));
            list.Add(string.Format(""));


            #endregion Along Transverse Direction


            list.Add(string.Format(""));


            #endregion STEP 5 : DESIGN OF PILE & PILE CAP

            #region STEP 6 : PILE REACTIONS

            List<Pile_Reaction> PR_NORM_LONG = new List<Pile_Reaction>();
            List<Pile_Reaction> PR_NORM_TRANS = new List<Pile_Reaction>();
            List<Pile_Reaction> PR_SEIS_LONG = new List<Pile_Reaction>();
            List<Pile_Reaction> PR_SEIS_TRANS = new List<Pile_Reaction>();

            Pile_Reaction pr;
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 6 : PILE REACTIONS"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double dia = pcdia * 1000;
            double pile_capacity = 250.0; //user input

            list.Add(string.Format("Pile Capacity for {0} mm dia pile = {1} t", dia, pile_capacity));
            list.Add(string.Format("No. of Piles = {0} nos", tp));
            list.Add(string.Format("ex^2 = {0:f3} ", total_MI_exx));
            list.Add(string.Format("ey^2 = {0:f3} ", total_MI_eyy));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double maxlm1, maxtm1, minlm1, mintm1;
            double maxlm2, maxtm2, minlm2, mintm2;

            maxlm1 = maxtm1 = maxlm2 = maxtm2 = 0.0;
            minlm1 = mintm1 = minlm2 = mintm2 = double.MaxValue;
            int cc = 1;
            for (i = 0; i < Along_Long.Count; i++)
            {
                cc = (i >= (Along_Long.Count / 2)) ? -1 : 1;

                algl = Along_Long[i];
                algt = Along_Trans[i];

                #region Normal Longitudinal
                pr = new Pile_Reaction();

                pr.Pile_No = algl.Pile_No;
                pr.Dia_of_Pile = algl.Pile_Dia * 1000;

                pr.x = algl.Distance_w_r_t_c_g_of_Pile_group_d;
                pr.y = algt.Distance_w_r_t_c_g_of_Pile_group_d;

                pr.P_by_N = SLC_Pile_Cap.Vertical_Load[0] / tp;

                pr.Mll_x_ex = SLC_Pile_Cap.Mll[0] * pr.x / total_MI_exx;
                pr.Mtt_y_ey = SLC_Pile_Cap.Mtt[0] * pr.y / total_MI_eyy;

                pr.Mll_x_ex *= cc;

                if(i > 0)
                {
                    if(PR_NORM_LONG[i-1].Mtt_y_ey == 0.0)
                        pr.Mtt_y_ey *= (-1);

                }

                pr.Total = pr.P_by_N + pr.Mll_x_ex + pr.Mtt_y_ey;

                pr.Check = (pile_capacity > pr.Total);


                PR_NORM_LONG.Add(pr);
                
                if(maxlm1 < pr.Total)
                    maxlm1 = pr.Total;

                if(minlm1 > pr.Total)
                    minlm1 = pr.Total;

                #endregion Normal Longitudinal

                #region Normal Transverse
                pr = new Pile_Reaction();

                pr.Pile_No = algl.Pile_No;
                pr.Dia_of_Pile = algl.Pile_Dia * 1000;

                pr.x = algl.Distance_w_r_t_c_g_of_Pile_group_d;
                pr.y = algt.Distance_w_r_t_c_g_of_Pile_group_d;

                pr.P_by_N = SLC_Pile_Cap.Vertical_Load[1] / tp;

                pr.Mll_x_ex = SLC_Pile_Cap.Mll[1] * pr.x / total_MI_exx;
                pr.Mtt_y_ey = SLC_Pile_Cap.Mtt[1] * pr.y / total_MI_eyy;

                pr.Mll_x_ex *= cc;

                if (i > 0)
                {
                    if (PR_NORM_TRANS[i - 1].Mtt_y_ey == 0.0)
                        pr.Mtt_y_ey *= (-1);

                }
                pr.Total = pr.P_by_N + pr.Mll_x_ex + pr.Mtt_y_ey;

                pr.Check = (pile_capacity > pr.Total);

                PR_NORM_TRANS.Add(pr);

                
                if(maxtm1 < pr.Total)
                    maxtm1 = pr.Total;

                if(mintm1 > pr.Total)
                    mintm1 = pr.Total;

                #endregion Normal Transverse

                #region Seismic Longitudinal
                pr = new Pile_Reaction();

                pr.Pile_No = algl.Pile_No;
                pr.Dia_of_Pile = algl.Pile_Dia * 1000;

                pr.x = algl.Distance_w_r_t_c_g_of_Pile_group_d;
                pr.y = algt.Distance_w_r_t_c_g_of_Pile_group_d;

                pr.P_by_N = SLC_Pile_Cap.Vertical_Load[2] / tp;

                pr.Mll_x_ex = SLC_Pile_Cap.Mll[2] * pr.x / total_MI_exx;
                pr.Mtt_y_ey = SLC_Pile_Cap.Mtt[2] * pr.y / total_MI_eyy;

                pr.Mll_x_ex *= cc;

                if (i > 0)
                {
                    if (PR_SEIS_LONG[i - 1].Mtt_y_ey == 0.0)
                        pr.Mtt_y_ey *= (-1);

                }
                pr.Total = pr.P_by_N + pr.Mll_x_ex + pr.Mtt_y_ey;

                pr.Check = (pile_capacity * 1.25 > pr.Total);

                PR_SEIS_LONG.Add(pr);

                if (maxlm2 < pr.Total)
                    maxlm2 = pr.Total;

                if(minlm2 > pr.Total)
                    minlm2 = pr.Total;
                #endregion Seismic Longitudinal

                #region Seismic Transverse
                pr = new Pile_Reaction();

                pr.Pile_No = algl.Pile_No;
                pr.Dia_of_Pile = algl.Pile_Dia * 1000;

                pr.x = algl.Distance_w_r_t_c_g_of_Pile_group_d;
                pr.y = algt.Distance_w_r_t_c_g_of_Pile_group_d;

                pr.P_by_N = SLC_Pile_Cap.Vertical_Load[3] / tp;

                pr.Mll_x_ex = SLC_Pile_Cap.Mll[3] * pr.x / total_MI_exx;
                pr.Mtt_y_ey = SLC_Pile_Cap.Mtt[3] * pr.y / total_MI_eyy;

                pr.Mll_x_ex *= cc;

                if (i > 0)
                {
                    if (PR_SEIS_TRANS[i - 1].Mtt_y_ey == 0.0)
                        pr.Mtt_y_ey *= (-1);

                }
                pr.Total = pr.P_by_N + pr.Mll_x_ex + pr.Mtt_y_ey;

                pr.Check = (pile_capacity * 1.25 > pr.Total);

                PR_SEIS_TRANS.Add(pr);
                
                if(maxtm2 < pr.Total)
                    maxtm2 = pr.Total;

                if(mintm2 > pr.Total)
                    mintm2 = pr.Total;

                #endregion Seismic Transverse

            }

            list.Add(string.Format(""));
           
            //S.No	Dia of Pile (mm)	Pile No

            //S.No	Dia of Pile (mm) x (m)	y (m)	P/N	Mll * x/ex^2	Mtt * y/ey^2	Total (t)	Check
            #region PR_NORM_LONG
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                            Distances w.r.t                 Normal - Longitudinal Moment Case                                "));
            list.Add(string.Format("                            along directions     -------------------------------------------------------------"));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Dia of    Pile No   x (m)      y (m)       P/N    Mll * x/ex^2    Mtt * y/ey^2    Total (t)    Check"));
            list.Add(string.Format("       Pile (mm)                                                                   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            for (i = 0; i < PR_NORM_LONG.Count; i++)
            {
                pr = PR_NORM_LONG[i];
                list.Add(string.Format(" {0,-5} {1,7} {2,7} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,14:f2} {8,15:f2} {9,10}",
                    (i+1), 
                    pr.Dia_of_Pile, 
                    pr.Pile_No, 
                    pr.x, 
                    pr.y, 
                    pr.P_by_N, 
                    pr.Mll_x_ex, 
                    pr.Mtt_y_ey, 
                    pr.Total, 
                    pr.Check ? "  OK " : "NOT OK" 
                    ));

            }
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Longitudinal Moment Capacity = {0:f3} t", pile_capacity));
            list.Add(string.Format("Maximum Longitudinal Moment = maxlm1 = {0:f3} t", maxlm1));
            list.Add(string.Format("Minimum Longitudinal Moment = minlm1 = {0:f3} t", minlm1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion PR_NORM_LONG

            #region PR_NORM_TRANS
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                            Distances w.r.t                 Normal - Transverse Moment case"));
            list.Add(string.Format("                            along directions     -------------------------------------------------------------"));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Dia of    Pile No   x (m)      y (m)       P/N    Mll * x/ex^2    Mtt * y/ey^2    Total (t)    Check"));
            list.Add(string.Format("       Pile (mm)                                                                   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
           
            for (i = 0; i < PR_NORM_TRANS.Count; i++)
            {
                pr = PR_NORM_TRANS[i];
                list.Add(string.Format(" {0,-5} {1,7} {2,7} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,14:f2} {8,15:f2} {9,10}",
                    (i + 1),
                    pr.Dia_of_Pile,
                    pr.Pile_No,
                    pr.x,
                    pr.y,
                    pr.P_by_N,
                    pr.Mll_x_ex,
                    pr.Mtt_y_ey,
                    pr.Total,
                    pr.Check ? "  OK " : "NOT OK"
                    ));

            }
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("Transverse Moment Capacity = {0:f3} t", pile_capacity));
            list.Add(string.Format("Maximum Longitudinal Moment = maxtm2 = {0:f3} t", maxtm1));
            list.Add(string.Format("Minimum Longitudinal Moment = mintm2 = {0:f3} t", mintm1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion PR_NORM_TRANS

            #region PR_SEIS_LONG
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                            Distances w.r.t                 Seismic Case - Longitudinal"));
            list.Add(string.Format("                            along directions     -------------------------------------------------------------"));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Dia of    Pile No   x (m)      y (m)       P/N    Mll * x/ex^2    Mtt * y/ey^2    Total (t)    Check"));
            list.Add(string.Format("       Pile (mm)                                                                   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
           
            for (i = 0; i < PR_SEIS_LONG.Count; i++)
            {
                pr = PR_SEIS_LONG[i];
                list.Add(string.Format(" {0,-5} {1,7} {2,7} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,14:f2} {8,15:f2} {9,10}",
                    (i + 1),
                    pr.Dia_of_Pile,
                    pr.Pile_No,
                    pr.x,
                    pr.y,
                    pr.P_by_N,
                    pr.Mll_x_ex,
                    pr.Mtt_y_ey,
                    pr.Total,
                    pr.Check ? "  OK " : "NOT OK"
                    ));

            }
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("Longitudinal Moment Capacity = {0:f3} t", pile_capacity));
            list.Add(string.Format("Maximum Longitudinal Moment = maxlm2 = {0:f3} t", maxlm2));
            list.Add(string.Format("Minimum Longitudinal Moment = minlm2 = {0:f3} t", minlm2));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion PR_SEIS_LONG

            #region PR_SEIS_TRANS
            list.Add(string.Format(""));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                            Distances w.r.t                  Seismic Case - Transverse"));
            list.Add(string.Format("                            along directions     -------------------------------------------------------------"));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No    Dia of    Pile No   x (m)      y (m)       P/N    Mll * x/ex^2    Mtt * y/ey^2    Total (t)    Check"));
            list.Add(string.Format("       Pile (mm)                                                                   "));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
           
            for (i = 0; i < PR_SEIS_TRANS.Count; i++)
            {
                pr = PR_SEIS_TRANS[i];
                list.Add(string.Format(" {0,-5} {1,7} {2,7} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,14:f2} {8,15:f2} {9,10}",
                    (i + 1),
                    pr.Dia_of_Pile,
                    pr.Pile_No,
                    pr.x,
                    pr.y,
                    pr.P_by_N,
                    pr.Mll_x_ex,
                    pr.Mtt_y_ey,
                    pr.Total,
                    pr.Check ? "  OK " : "NOT OK"
                    ));

            }
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Transverse Moment Capacity = {0:f3} t", pile_capacity * 1.25));
            list.Add(string.Format("Maximum Transverse Moment = maxtm2 = {0:f3} t", maxtm2));
            list.Add(string.Format("Minimum Transverse Moment = mintm2 = {0:f3} t", mintm2));
            list.Add(string.Format(""));

            #endregion PR_SEIS_TRANS

            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion STEP 6 : PILE REACTIONS

            #region STEP 7 : STRUCTURAL DESIGN OF PILE
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 7 : STRUCTURAL DESIGN OF PILE"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("CALCULATION OF NO. OF PILES"));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            double tvl = SLC_Pile_Cap.Max_Vert_Load();

            list.Add(string.Format("Total Vertical load at cg of pile cap = tvl = {0:f3} t ", tvl));

            double thf = Math.Max(mlm1, hf1);
            list.Add(string.Format("Total Horizontal force = thf = {0:f3} t", thf));

            double shf = Math.Max(THL1, THL2);
            list.Add(string.Format("Seismic Horizontal force = shf = {0:f3} t ", shf));

            //list.Add(string.Format("Vertical Capacity of 1200mm dia 20.0m long pile 250 t "));
            list.Add(string.Format("Vertical Load Capacity of pile = vc = {0:f3} t", vc));
            list.Add(string.Format(""));
            list.Add(string.Format("Horizontal Load Capacity of Pile = hc = {0:f3} t ", hc));
            list.Add(string.Format(""));

            double vpn = tvl / vc;
            list.Add(string.Format("No. of Plies required (Vertical) = vpn = tvl / vc = {0:f3} / {1:f3} = {2:f3} nos ",tvl, vc, vpn));
            list.Add(string.Format(""));
            double hpn = thf / hc;
            list.Add(string.Format("No. of Plies required (Horizontal) = hpn = tvl / vc = {0:f3} / {1:f3} = {2:f3} nos ", thf, hc, hpn));
            list.Add(string.Format(""));

            double hsnp =  shf / (hc*1.25);
            list.Add(string.Format("No. of Plies required (Horizontal- in Seismic Case) = shf / (hc*1.25)"));
            list.Add(string.Format("                                                    = {0:f3} / ({1:f3}*1.25)", shf, hc));
            list.Add(string.Format("                                                    = {0:f3} nos ", hsnp));
            list.Add(string.Format(""));
            list.Add(string.Format("Provided No of piles = {0} nos ", tp));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STRUCTURAL DESIGN OF PILE"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            double mlfl = maxV1;
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Lateral Force in Longitudinal Direction on Pile Foundation under normal condition = mlfl = {0:f3} t", mlfl));
            list.Add(string.Format(""));

            double cmaxvl1 = 0.0;
            double cminvl1 = double.MaxValue;

            for (i = 0; i < PR_NORM_LONG.Count;i++ )
            {
                if (cmaxvl1 < PR_NORM_LONG[i].Total)
                    cmaxvl1 = PR_NORM_LONG[i].Total;
                if (cminvl1 > PR_NORM_LONG[i].Total)
                    cminvl1 = PR_NORM_LONG[i].Total;
            }
            list.Add(string.Format("Corresponding Max. Vertical Load on one Pile for Load - Normal Case = cmaxvl1 = {0:f3} t", cmaxvl1));
            list.Add(string.Format("and Corresponding Minimum Vertical Load on one Pile for Load - Normal Case = cminvl1 = {0:f3} t", cminvl1));
            list.Add(string.Format(""));
            list.Add(string.Format("Maximum Lateral Force in Longitudinal Direction on Pile Foundation under Seismic Condition = {0:f3} t", shf));
            list.Add(string.Format(""));
            double cmaxvl2 = Math.Max(maxlm2, maxtm2);
            list.Add(string.Format("Corresponding Maximum Vertical Load on one Pile - Seismic case = {0:f3} t ", cmaxvl2));
            double cminvl2 = Math.Max(minlm2, mintm2);
            list.Add(string.Format("Corresponding Minimum Vertical Load on one Pile - Seismic case = {0:f3} t ", cminvl2));
            list.Add(string.Format(""));
            list.Add(string.Format("Total No. of Piles = tp = {0}", tp));
            list.Add(string.Format("Length of Pile = lp = {0} m ", Pile_len));
            list.Add(string.Format("Diameter of Pile = dp = {0} m ", pcdia));
            list.Add(string.Format(""));
            list.Add(string.Format("LOAD CALCULATIONS FOR STRUCTURAL DESIGN "));
            list.Add(string.Format("----------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("a) Normal Case "));
            list.Add(string.Format("--------------"));

            double maxlf = mlfl / tp;
            double Q = ((int)maxlf) + 1;
            list.Add(string.Format("Max. Lateral Force on one Pile under Normal Condition = Q = {0:f3} / {1:f3} = {2:f3} t  = {3} t", mlfl, tp, maxlf, Q));
            list.Add(string.Format(""));
            if (hc > Q)
                list.Add(string.Format("Max. Lateral load Capacity of Pile under Normal Condition = {0:f3}  t  > {1:f3} t, SO,   OK", hc, Q));
            else
                list.Add(string.Format("Max. Lateral load Capacity of Pile under Normal Condition = {0:f3}  t  <=  {1:f3} t, SO,  NOT OK", hc, Q));

            //list.Add(string.Format(" O.K "));
            list.Add(string.Format("Now, Maximum Moment in Pile is given by = 2.95*Q tm "));

            double mmp = 2.95 * Q;
            list.Add(string.Format("Therefore, Maximum Moment in Pile under Normal condition = mmp = 2.95*{0} = {1:f3} t-m ", Q, mmp));
            list.Add(string.Format(""));
            list.Add(string.Format("Corresponding Maximum Vertical Load (Excluding Self Weight of pile) = {0:f3} t ", cmaxvl1));
            list.Add(string.Format(""));
            list.Add(string.Format("Corresponding Minimum Vertical Load (Excluding Self Weight of pile) = {0:f3} t ", cminvl1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("b) Seismic Case "));
            list.Add(string.Format("----------------"));
            list.Add(string.Format(""));
            double maxlfps = shf / tp;
            list.Add(string.Format("Maximum Lateral Force on one Pile under Seismic Condition = shf/tp = {0:f3} / {1} = {2:f3} t ", shf, tp, maxlfps));
            list.Add(string.Format(""));
            list.Add(string.Format("Max. Lateral load Capacity of Pile under Seismic Condition = 1.25 x hc"));
            list.Add(string.Format("                                                           = 1.25 x {0:f3}", hc));
            if((1.25*hc) > maxlfps)
                list.Add(string.Format("                                                           = {0:f3} t > {1:f3}, SO   OK.", (1.25 * hc), maxlfps));
            else
                list.Add(string.Format("                                                           = {0:f3} t < {1:f3}, SO   NOT OK.", (1.25 * hc), maxlfps));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            double mmps = 2.95 * maxlfps;
            list.Add(string.Format("Maximum Moment in Pile under Seismic condition = 2.95*Q = 2.95*{0:f3} = {1:f3} tm ", maxlfps, mmps));
            list.Add(string.Format(""));
            list.Add(string.Format("Corresponding Maximum Vertical Load (Excluding Self Weight of pile) = {0:f3} t ", cmaxvl2));
            list.Add(string.Format("Corresponding Minimum Vertical Load (Excluding Self Weight of pile) = {0:f3} t  ", cminvl2));
            list.Add(string.Format(""));

            double pwt = conc_den * Pile_len * Math.PI * pcdia * pcdia / 4;
            list.Add(string.Format("Self weight of pile = pwt = conc_den * Pile_len * π * pcdia^2 / 4  = 54.3 t "));
            list.Add(string.Format("                          = {0:f3} * {1:f3} * {2:f5} * {3:f3}^2 / 4",
                conc_den , Pile_len , Math.PI , pcdia));
            list.Add(string.Format("                          = {0:f3} t ", pwt));
            list.Add(string.Format(""));
            list.Add(string.Format("The design loads and moments are summarised below : "));
            list.Add(string.Format(""));
            list.Add(string.Format("Sr. No. Description                  Vertical Load (t)     Moment (tm)   Eccentricity M/V (m) "));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Normal Case (Max. Vertical load)  {0,14:f3} {1,17:f3} {2,18:f3}", cmaxvl1, mmp, (mmp / cmaxvl1)));
            list.Add(string.Format("2. Normal Case (Min. Vertical Load)  {0,14:f3} {1,17:f3} {2,18:f3}", cminvl1, mmp, (mmp / cminvl1)));
            list.Add(string.Format("3. Seismic Case (Max. Vertical Load) {0,14:f3} {1,17:f3} {2,18:f3}", cmaxvl2, mmps, (mmps / cmaxvl2)));
            list.Add(string.Format("4. Seismic Case (Min. Vertical Load) {0,14:f3} {1,17:f3} {2,18:f3}", cminvl2, mmps, (mmps / cminvl2)));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation of Reinforcement"));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));

            double rpa = 0.83;
            list.Add(string.Format("The reinforcement in pile assumed {0}% of cross-sectional area. {0}% of c/s area ", rpa));
            list.Add(string.Format(""));

            double ar_rein = (rpa / 100) * Math.PI * (pcdia * 100) * (pcdia * 100) / 4.0;
            //list.Add(string.Format("Area of Reinforcement = 0.83/100 * 1.131 * 10000 = 93.871 cm2 "));
            list.Add(string.Format("Area of Reinforcement = ar_rein = (rpa / 100) * π * (pcdia * 100)^2"));
            list.Add(string.Format("                                = ({0:f3} / 100) * {1:f3} * ({2:f3} * 100)^2", rpa, Math.PI, pcdia));
            list.Add(string.Format("                                = {0:f3} Sq.cm ", ar_rein));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            
            double bdia = 25;

            list.Add(string.Format("Bar dia to be provided = bdia = {0} mm ", bdia));

            double nbr = ar_rein / (Math.PI * bdia * bdia / 400.0);
            list.Add(string.Format("No. of {0} dia. Bars reqd. = ar_rein / (π * bdia^2 / 400.0) = 19 No. ", bdia));
            list.Add(string.Format("                           = {0:f3} / ({1:f5} * {2:f3}^2 / 400.0)", ar_rein, Math.PI, bdia));
            list.Add(string.Format("                           = {0:f3} ", nbr));
            nbr = (int)nbr;
            list.Add(string.Format("                           = {0:f3} Nos", nbr));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double bar_prvd = 22;

            list.Add(string.Format("No. of {0} dia. Bars prov. = {1} No. ", bdia, bar_prvd));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide {0} dia , {1} nos. around periphery ", bdia, bar_prvd));
            list.Add(string.Format(""));

            double ast_pr = bar_prvd * (Math.PI * bdia * bdia / 400.0);
            list.Add(string.Format("As provided = bar_prvd * (Math.PI * bdia^2 / 400.0)"));
            list.Add(string.Format("            = {0} * ({1:f5} * {2:f3}^2 / 400.0) ", bar_prvd, Math.PI, bdia));
            list.Add(string.Format("            = {0:f3} Sq.cm", ast_pr));

            list.Add(string.Format(""));



            //list.Add(string.Format("As provided = 22 * 4.91 = 108.000 cm2 "));
            list.Add(string.Format(""));
            list.Add(string.Format("Provide 8 dia. @ 150mm as helical reinforcement "));
            list.Add(string.Format(""));
            list.Add(string.Format("Pile Reinforcement below Depth of Fixity "));
            list.Add(string.Format(""));
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("-------------------------------------------------------------------------------------"));
                list.Add(string.Format("The Pile reinforcement 21b - 16  dia  have been provided below the depth of fixity. "));
                list.Add(string.Format("The details of  depth of  fixity have  been  derived in page no.20. To economize on"));
                list.Add(string.Format("the area of reinforcement, bars  have  been curtailed after providing the anchorage"));
                list.Add(string.Format("length  as  per  clause 304.6.2 of IRC: 21 - 2000  and minimum reinforcement as per "));
                list.Add(string.Format("clause 709.4.4 of IRC: 78-2000 below the  depth  of  fixity."));
                list.Add(string.Format("-------------------------------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
            }
            list.Add(string.Format(""));
            #region End of Report
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            #endregion

            #endregion

            File.WriteAllLines(file_name, list.ToArray());

            MessageBox.Show("Report file created as " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            if (IsCreateData)
                user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!Directory.Exists(user_path))
                Directory.CreateDirectory(user_path);


            if (iApp.Check_Demo_Version())
            {
                Save_FormRecord.Get_Demo_Data(this);
            }

            Save_FormRecord.Write_All_Data(this, user_path);


            Calculate_Program(Report_File);

            iApp.View_Result(Report_File);

            iApp.Save_Form_Record(this, user_path);
        }

        private void frmPier_Design_with_Piles_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            MyList mlist = null;



            #region DGV 1 LOAD CALCULATIONS AT PIER BASE
            list.Add(string.Format("1,Dead Load + SIDL,479.01,0.000,0.000,0.000,0.000 "));
            list.Add(string.Format("2,Live Load (Max Long Moment),92.00,1.100,101.20,1.155,106.26 "));
            list.Add(string.Format("3,Live Load (Max Trans Moment),46.78,1.100,51.46,2.950,138.01 "));
            list.Add(string.Format("4,Pier Cap,111.24,0.000,0.000,0.000,0.000 "));
            list.Add(string.Format("5,Pier Shaft,168.58,0.000,0.000,0.000,0.000 "));



            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv1.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion DGV 1 LOAD CALCULATIONS AT PIER BASE


            #region DGV 2 ONE SPAN DISLODGED CONDITION

            list.Clear();


            list.Add(string.Format("1,Dead Load + SIDL,239.51,1.100,263.456,0.000,0.000 "));
            list.Add(string.Format("2,Live Load (Max Long Moment),0.000,1.100,0.000,1.155,0.000 "));
            list.Add(string.Format("3,Live Load (Max Trans Moment),0.000,0.000,0.000,2.950,0.000 "));
            list.Add(string.Format("4,Pier Cap,111.24,0.000,0.000,0.000,0.000 "));
            list.Add(string.Format("5,Pier Shaft,168.58,0.000,0.000,0.000,0.000 "));


            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv2.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion DGV 2 ONE SPAN DISLODGED CONDITION


            #region DGV 3 LOAD CALCULATIONS - AT PILE CAP BASE

            list.Clear();

            list.Add(string.Format("1,Dead Load + SIDL,479.01,0.000,0.000,0.000,0.000 "));
            list.Add(string.Format("2,Live Load (Max Long Moment),92.00,1.100,101.20,1.155,106.26 "));
            list.Add(string.Format("3,Live Load (Max Trans Moment),46.78,1.100,51.46,2.950,138.01 "));
            list.Add(string.Format("4,Pier Cap,111.24,0.000,0.000,0.000,0.000 "));
            list.Add(string.Format("5,Pier Shaft,168.58,0.000,0.000,0.000,0.000 "));


            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv3.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion DGV 2 LOAD CALCULATIONS - AT PILE CAP BASE

            TextChanged1();



            //Chiranjit [2013 04 04]
            Save_FormRecord.Set_Demo_Data(this);
            iApp.Check_Demo_Version();

            #region Chiranjit Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    iApp.Read_Form_Record(this, user_path);
                    //iApp.LastDesignWorkingFolder = user_path;


                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option



        }

        private void txt_H1_TextChanged(object sender, EventArgs e)
        {
            TextChanged1();
        }

        private void TextChanged1()
        {

            txt_H1_1.Text = txt_H1.Text;
            txt_H2_1.Text = txt_H2.Text;
            txt_H3_1.Text = txt_H3.Text;
            txt_H4_1.Text = txt_H4.Text;
            txt_H5_1.Text = txt_H5.Text;

            txt_D1_1.Text = txt_D1.Text;
            txt_D2_1.Text = txt_D2.Text;
            txt_D3_1.Text = txt_D3.Text;
            txt_D4_1.Text = txt_D4.Text;
        }

        private void txt_Seismic_TextChanged(object sender, EventArgs e)
        {
            //( Z / 2 ) . ( Sa / g ) / ( R / I )

            double Z, Sa_by_g, R, I, Ah;

            Z = MyList.StringToDouble(txt_Z.Text, 0.0);
            Sa_by_g = MyList.StringToDouble(txt_Sa_by_g.Text, 0.0);
            R = MyList.StringToDouble(txt_R.Text, 0.0);
            I = MyList.StringToDouble(txt_I.Text, 0.0);

            Ah = (Z / 2) * (Sa_by_g) / (R / I);

            txt_HSC.Text = Ah.ToString("f3");
        }

        private void dgv3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
                TableData.Data_Calculate(dgv);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class TableData
    {

        public List<string> VERT_LOAD_NAMES { get; set; }
        public List<double> VERT_LOAD_VALUES { get; set; }
        public List<double> DIST_CG_SHAFT_LONG { get; set; }
        public List<double> MOMENT_CG_SHAFT_LONG { get; set; }
        public List<double> DIST_CG_SHAFT_TRANS { get; set; }
        public List<double> MOMENT_CG_SHAFT_TRANS { get; set; }

        public TableData()
        {
            VERT_LOAD_NAMES = new List<string>();
            VERT_LOAD_VALUES = new List<double>();
            DIST_CG_SHAFT_LONG = new List<double>();
            MOMENT_CG_SHAFT_LONG = new List<double>();
            DIST_CG_SHAFT_TRANS = new List<double>();
            MOMENT_CG_SHAFT_TRANS = new List<double>();
        }

        public TableData(DataGridView dgv)
            : this()
        {
            Data_Collect_From_Grid(dgv);
        }

        public bool Data_Collect_From_Grid(DataGridView dgv)
        {
            try
            {
                int c = 1;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    c = 1;
                    try
                    {
                        VERT_LOAD_NAMES.Add(dgv[c++, i].Value.ToString());
                        VERT_LOAD_VALUES.Add(MyList.StringToDouble(dgv[c++, i].Value.ToString(), 0.0));
                        DIST_CG_SHAFT_LONG.Add(MyList.StringToDouble(dgv[c++, i].Value.ToString(), 0.0));
                        MOMENT_CG_SHAFT_LONG.Add(MyList.StringToDouble(dgv[c++, i].Value.ToString(), 0.0));
                        DIST_CG_SHAFT_TRANS.Add(MyList.StringToDouble(dgv[c++, i].Value.ToString(), 0.0));
                        MOMENT_CG_SHAFT_TRANS.Add(MyList.StringToDouble(dgv[c++, i].Value.ToString(), 0.0));
                    }
                    catch (Exception exx) { }
                }

                return true;

            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public static bool Data_Calculate(DataGridView dgv)
        {
            try
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    try
                    {
                        dgv[4, i].Value = (MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0) *
                            MyList.StringToDouble(dgv[3, i].Value.ToString(), 0.0)).ToString("f3");

                        dgv[6, i].Value = (MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0) *
                            MyList.StringToDouble(dgv[5, i].Value.ToString(), 0.0)).ToString("f3");

                    }
                    catch (Exception exx) { }
                }

                return true;

            }
            catch (Exception ex)
            {
            }

            return false;
        }

    }

    public class Summary_Load_Cases
    {
        //Description of Cases	Vertical Load (t) 	Mll    (tm)	Mtt    (tm)

        public List<string> Descriptions { get; set; }
        public List<double> Vertical_Load { get; set; }
        public List<double> Mll { get; set; }
        public List<double> Mtt { get; set; }

        public int Count
        {
            get
            {
                return Vertical_Load.Count;

            }
        }
        public Summary_Load_Cases()
        {
            Descriptions = new List<string>();
            Vertical_Load = new List<double>();
            Mll = new List<double>();
            Mtt = new List<double>();
        }
        public void Clear()
        {
            Descriptions.Clear();
            Vertical_Load.Clear();
            Mll.Clear();
            Mtt.Clear();
        }

        public double Max_Vert_Load()
        {
            double d = 0.0;

            foreach (var item in Vertical_Load)
            {
                if (d < item)
                    d = item;
            }

            return d;
        }
    }

    public class Pile_Cap_CG
    {
        //No.of Piles	
        //Pile No.s	
        //Pile-Dia (m)	
        //Pile-Section 
        //Area of pile	
        //Total area in section (A)	
        //distance from section A-A (x)
        //A*x
        public Pile_Cap_CG()
        {
            No_of_Piles = 0;
            Pile_Nos = "";
            Pile_Dia = 0.0;
            Pile_Section = "";
            Distance_from_section_x = 0.0;
        }

        public int No_of_Piles { get; set; }
        public string Pile_Nos { get; set; }
        public double Pile_Dia { get; set; }
        public string Pile_Section { get; set; }
        public double Area_of_pile
        {
            get
            {
                return Math.PI * Pile_Dia * Pile_Dia / 4.0;
            }
        }

        public double Total_Area_in_section_A
        {
            get
            {
                return No_of_Piles * Area_of_pile;
            }
        }
        public double Distance_from_section_x { get; set; }
        public double Ax
        {
            get
            {
                return Total_Area_in_section_A * Distance_from_section_x;
            }
        }
    }


    public class Pile_Cap_Along
    {
        //Pile No	
        //dia of pile m	
        //Area of pile (m2) - A	
        //distance from section B-B (m)	
        //Distance w.r.t c.g of Pile group (m) - d	
        //d*d
        //Total M.I = ey2

        public Pile_Cap_Along()
        {
            Pile_No = 0;
            Pile_Dia = 0.0;
            Distance_from_section_B_B = 0.0;
            Distance_w_r_t_c_g_of_Pile_group_d = 0.0;
        }
        public int Pile_No { get; set; }
        public double Pile_Dia { get; set; }
        public double Area_of_pile_A
        {
            get
            {
                return Math.PI * Pile_Dia * Pile_Dia / 4.0;
            }
        }
        public double Distance_from_section_B_B { get; set; }
        public double Distance_w_r_t_c_g_of_Pile_group_d { get; set; }
        public double dd
        {
            get
            {
                return Distance_w_r_t_c_g_of_Pile_group_d * Distance_w_r_t_c_g_of_Pile_group_d;
            }
        }
        public double Total_M_I
        {
            get
            {
                return dd;
            }
        }
    }

    public class Pile_Reaction
    {
        //Dia of Pile (mm)	
        //Pile No
        //x (m)	
        //y (m)	
        //P/N	
        //Mll * x/ex^2	
        //Mtt * y/ey^2	
        //Total (t)	
        //Check

        public double Dia_of_Pile { get; set; }
        public int Pile_No { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double P_by_N { get; set; }
        public double Mll_x_ex { get; set; }
        public double Mtt_y_ey { get; set; }
        public double Total { get; set; }
        public bool Check { get; set; }

        public Pile_Reaction()
        {
            Dia_of_Pile = 0.0;
            Pile_No = 0;
            x = 0.0;
            y = 0.0;
            P_by_N = 0.0;
            Mll_x_ex = 0.0;
            Mtt_y_ey = 0.0;
            Total = 0.0;
            Check = false;
        }
    }

}
