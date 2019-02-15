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
using AstraFunctionOne.Properties;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;

namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    public partial class frm_Continuous_Box_Girder : Form
    {
        
        IApplication iApp = null;

        //const string Title = "ANALYSIS OF CONTINUOUS PSC BOX GIRDER BRIDGE";
        //const string Title = "ANALYSIS OF PSC BOX GIRDER BRIDGE CONTINUOUS SPANS";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "PSC BOX GIRDER BRIDGE CONTINUOUS SPANS [BS]";
                return "PSC BOX GIRDER BRIDGE CONTINUOUS SPANS [IRC]";
            }
        }



        Continuous_Box_Girder_Design cnt;
        RCC_AbutmentWall Abut = null;
        RccPier rcc_pier = null;

        public Continuous_Box_Girder_Design Girder_Design
        {
            get
            {
                return cnt;
            }
        }
        public frm_Continuous_Box_Girder(IApplication app)
        {
            InitializeComponent();

            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

            cnt = new Continuous_Box_Girder_Design(iApp);
            Results = new List<string>();

        }
        Continuous_PSC_BoxGirderAnalysis Analysis = null;

        #region User Inputs

        public double L1 { get { return MyList.StringToDouble(txt_Ana_L1.Text, 13.0); } set { txt_Ana_L1.Text = value.ToString("f3"); } }
        public double L2 { get { return MyList.StringToDouble(txt_Ana_L2.Text, 13.0); } set { txt_Ana_L2.Text = value.ToString("f3"); } }
        public double L3 { get { return L1; } set { L1 = value; } }
        public double B { get { return MyList.StringToDouble(txt_Ana_B.Text, 0.0); } set { txt_Ana_B.Text = value.ToString("f3"); } }
        public double Width_Cant { get { return MyList.StringToDouble(txt_Ana_width_cantilever.Text, 0.0); } set { txt_Ana_width_cantilever.Text = value.ToString("f3"); } }
        public double EffectiveDepth { get { return MyList.StringToDouble(txt_Ana_DL_eff_depth.Text, 0.0); } set { txt_Ana_DL_eff_depth.Text = value.ToString("f3"); } }

        //Chiranjit [2013 06 17]
        public double DL_Factor
        {
            get
            {
                return MyList.StringToDouble(txt_Ana_DL_factor.Text, 1.0);
            }
        }
        public double LL_Factor
        {
            get
            {
                return MyList.StringToDouble(txt_Ana_LL_factor.Text, 1.0);
            }
        }


        public List<string> Results { get; set; }

        bool IsCreate_Data = true;


        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
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

        public string Worksheet_Folder
        {
            get
            {
                if (Path.GetFileName(user_path) == Project_Name)
                    if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
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
        public string Input_File
        {
            get
            {
                if (Directory.Exists(user_path))
                {
                    //return Path.Combine(Path.Combine(user_path, "Live Load Analysis"), "Input_Data_LL.txt");
                    return Path.Combine(user_path, "INPUT_DATA.TXT");
                }
                return "";
                //return "";
            }
        }

   
        #endregion User Inputs

        public string Analysis_Path
        {
            get
            {
                //if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, Title)))
                //    return Path.Combine(iApp.LastDesignWorkingFolder, Title);
                return iApp.user_path;
            }
        }

        //Chiranjit [2013 04 27]

        public ReadForceType GetForceType()
        {
            ReadForceType rft = new ReadForceType();
            rft.M1 = true;
            rft.R1 = true;
            rft.M2 = chk_M2.Checked;
            rft.M3 = chk_M3.Checked;
            rft.R3 = chk_R3.Checked;
            rft.R2 = chk_R2.Checked;
            return rft;
        }

        string File_Section_Properties { get { return Path.Combine(Analysis_Path, "SECTION_PROPERTIES_SUMMARY.TXT"); } }

        public void Default_Transverse_Load()
        {
            #region Sample Data
            //Joint Load

            //,1,SELFWEIGHT,ALL,Y,-1
            //,2,RAILING,22 24,FY,-0.3
            //,2,RAILING KERB,22 24,FY,-0.48
            //,2,UTILITIES,22,FY,-0.15
            //,2,WATER SUPPLY PIPE,22,FY,-0.3

            //Member Load

            //,2,ROAD KERB,22 24,CON,GY,-0.162,0.9625
            //,2,FOOTPATH SLAB,22 24,UNI,GY,-0.24, 0.85, 2.375
            //,2,WEARING COURSE,22 24,UNI,GY,-0.193,0,0.85
            //,2,WEARING COURSE,5 TO 10 21 23,UNI,GY,-0.193
            //,3,FPLL,22,UNI,GY,-0.373,0.85,2.375

            //,4,CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY,7,CONC,GY,-3.86,1.34
            //,4,CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY,8,CONC,GY,-3.86,0.965
            //,5,CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-6.17,0.18
            //,5,CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-3.53,2.11
            //,6,CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY,7,CONC,GY,-4.25,1.27

            //,6,CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY,8,CONC,GY,-4.25,1.03
            //,7,CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-5.53,0.17
            //,7,CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-3.98,2.23
            //,8,CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY,7,CONC,GY,-2.59,1.4
            //,8,CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY,8,CONC,GY,-2.59,0.9

            //,9,CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY,22,CONC,GY,-6.32,0.45
            //,9,CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-3.25 0.75
            //,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,5,CONC,GY,-8.68,0.1
            //,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,7,CONC,GY,-2.59,1.45
            //,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,8,CONC,GY,-2.59,0.85

            //,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,10,CONC,GY,-8.68,0.05
            //,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,22,CONC,GY,-6.32,0.45
            //,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,7,CONC,GY,-3.25,0.3
            //,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,7,CONC,GY,-2.59,2.0
            //,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,8,CONC,GY,-2.59,1.5




            //LOAD 1 SELF WEIGHT
            //SELFWEIGHT Y -1
            // LOAD 2 SIDL
            // JOINT LOAD
            // * RAILING
            // 22 24 FY -0.3
            // * RAILING KERB
            // 22 24 FY -0.48
            // * UTILITIES
            // 22 FY -0.15
            // * WATER SUPPLY PIPE
            // 22 FY -0.3
            // MEMBER LOAD
            // * ROAD KERB
            // 22 24 CONC GY -0.162 0.9625
            // * FOOTPATH SLAB
            // 22 24 UNI GY -0.24 0.85 2.375
            // * WEARING COURSE
            // 22 24 UNI GY -0.193 0 0.85
            // 5 TO 10 21 23 UNI GY -0.193
            // LOAD 3 FPLL
            // MEMBER LOAD
            // 22 UNI GY -0.373 0.85 2.375
            // LOAD 4 CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY
            // MEMBER LOAD
            // 7 CONC GY -3.86 1.34
            // 8 CONC GY -3.86 0.965
            // LOAD 5 CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY
            // MEMBER LOAD
            // 7 CONC GY -6.17 0.18
            // 7 CONC GY -3.53 2.11
            // LOAD 6 CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY
            // MEMBER LOAD
            // 7 CONC GY -4.25 1.27
            // 8 CONC GY -4.25 1.03
            // LOAD 7 CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY
            // MEMBER LOAD
            // 7 CONC GY -5.53 0.17
            // 7 CONC GY -3.98 2.23
            // LOAD 8 CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY
            // MEMBER LOAD
            // 7 CONC GY -2.59 1.4
            // 8 CONC GY -2.59 0.9
            // LOAD 9 CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY
            // MEMBER LOAD
            // 22 CONC GY -6.32 0.45
            // TRANSVERSE ANALYSIS OF BOX FOR DL, SIDL AND FPLL
            // 7 CONC GY -3.25 0.75
            // LOAD 10 CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY
            // MEMBER LOAD
            // 5 CONC GY -8.68 0.1
            // 7 CONC GY -2.59 1.45
            // 8 CONC GY -2.59 0.85
            // 10 CONC GY -8.68 0.05
            // LOAD 11 CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY
            // MEMBER LOAD
            // 22 CONC GY -6.32 0.45
            // 7 CONC GY -3.25 0.3
            // 7 CONC GY -2.59 2
            // 8 CONC GY -2.59 1.5
            #endregion Sample Data

            #region Set_Data

            List<string> list = new List<string>();
            list.Add(string.Format("Joint Load"));
            list.Add(string.Format(""));



            list.Clear();
            //list.Add(string.Format(",1,SELFWEIGHT,ALL,Y,-1"));

            
            list.Add(string.Format("1,2,RAILING,22 24,FY,-0.3"));
            list.Add(string.Format("2,2,RAILING KERB,22 24,FY,-0.48"));
            list.Add(string.Format("3,2,UTILITIES,22,FY,-0.15"));
            list.Add(string.Format("4,2,WATER SUPPLY PIPE,22,FY,-0.3"));

            int i = 0;
            MyList mlist = null;

            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_trans_joint.Rows.Add(mlist.StringList.ToArray());
            }






            list.Add(string.Format(""));
            list.Add(string.Format("Member Load"));
            list.Add(string.Format(""));
            list.Clear();
            list.Add(string.Format("1,2,ROAD KERB,22 24,CON,GY,-0.162,0.9625"));
            list.Add(string.Format("2,2,FOOTPATH SLAB,22 24,UNI,GY,-0.24, 0.85, 2.375"));
            list.Add(string.Format("3,2,WEARING COURSE,22 24,UNI,GY,-0.193,0,0.85"));
            list.Add(string.Format("4,2,WEARING COURSE,5 TO 10 21 23,UNI,GY,-0.193"));
            list.Add(string.Format("5,3,FPLL,22,UNI,GY,-0.373,0.85,2.375"));
            list.Add(string.Format("6,4,CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY,7,CONC,GY,-3.86,1.34"));
            list.Add(string.Format("7,4,CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY,8,CONC,GY,-3.86,0.965"));
            list.Add(string.Format("8,5,CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-6.17,0.18"));
            list.Add(string.Format("9,5,CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-3.53,2.11"));
            list.Add(string.Format("10,6,CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY,7,CONC,GY,-4.25,1.27"));
            list.Add(string.Format("11,6,CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY,8,CONC,GY,-4.25,1.03"));
            list.Add(string.Format("12,7,CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-5.53,0.17"));
            list.Add(string.Format("13,7,CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-3.98,2.23"));
            list.Add(string.Format("14,8,CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY,7,CONC,GY,-2.59,1.4"));
            list.Add(string.Format("15,8,CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY,8,CONC,GY,-2.59,0.9"));
            list.Add(string.Format("16,9,CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY,22,CONC,GY,-6.32,0.45"));
            list.Add(string.Format("17,9,CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY,7,CONC,GY,-3.25,0.75"));
            list.Add(string.Format("18,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,5,CONC,GY,-8.68,0.1"));
            list.Add(string.Format("19,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,7,CONC,GY,-2.59,1.45"));
            list.Add(string.Format("20,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,8,CONC,GY,-2.59,0.85"));
            list.Add(string.Format("21,10,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,10,CONC,GY,-8.68,0.05"));
            list.Add(string.Format("22,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,22,CONC,GY,-6.32,0.45"));
            list.Add(string.Format("23,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,7,CONC,GY,-3.25,0.3"));
            list.Add(string.Format("24,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,7,CONC,GY,-2.59,2.0"));
            list.Add(string.Format("25,11,CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY,8,CONC,GY,-2.59,1.5"));


            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_trans_member.Rows.Add(mlist.StringList.ToArray());
            }



            #endregion Set_Data
        }
        public void Default_Continuous_Span_Load()
        {

            #region Sample Data
            //Joint Load

//DEAD LOAD REACTION FROM SPAN 1,3,FY,-364.34
//DEAD LOAD REACTION FROM SPAN 1,22,FY,-323.11
//DEAD LOAD REACTION FROM SPAN 2,26,FY,-624.21
//DEAD LOAD REACTION FROM SPAN 2,54,FY,-624.21
//DEAD LOAD REACTION FROM SPAN 1,58,FY,-323.11
//DEAD LOAD REACTION FROM SPAN 1,77,FY,-364.34
//WEIGHT OF INTERMEDIATE DIAPHRAGM,24 56,FY,-39.328

           



            //JOINT LOAD
            //* DEAD LOAD REACTION FROM SPAN 1 IN TEMPORARY CONDITION
            //3 FY -364.34
            //22 FY -323.11
            //* DEAD LOAD REACTION FROM SPAN 2 IN TEMPORARY CONDITION
            //26 FY -624.21
            //54 FY -624.21
            //* DEAD LOAD REACTION FROM SPAN 3 IN TEMPORARY CONDITION
            //77 FY -323.11
            //58 FY -364.34
            //* WEIGHT OF INTERMEDIATE DIAPHRAGM
            //24 56 FY -39.328
            //MEMBER LOAD
            //1 78 UNI GY -11.08
            //23 24 55 56 UNI GY -24.23125



            #endregion Sample Data

            #region Set_Data

            List<string> list = new List<string>();
            list.Add(string.Format("Joint Load"));
            list.Add(string.Format(""));



            list.Clear();
            list.Add(string.Format("DEAD LOAD REACTION FROM SPAN 1,3,FY,-364.34"));
            list.Add(string.Format("DEAD LOAD REACTION FROM SPAN 1,22,FY,-323.11"));
            list.Add(string.Format("DEAD LOAD REACTION FROM SPAN 2,26,FY,-624.21"));
            list.Add(string.Format("DEAD LOAD REACTION FROM SPAN 2,54,FY,-624.21"));
            list.Add(string.Format("DEAD LOAD REACTION FROM SPAN 3,58,FY,-323.11"));
            list.Add(string.Format("DEAD LOAD REACTION FROM SPAN 3,77,FY,-364.34"));
            list.Add(string.Format("WEIGHT OF INTERMEDIATE DIAPHRAGM,24 56,FY,-39.328"));


            int i = 0;
            MyList mlist = null;

            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_cont_span_joint.Rows.Add(mlist.StringList.ToArray());
            }


            #endregion Set_Data
        }

        public void Default_Temperature_Load()
        {
            List<string> lst_temp = new List<string>();
            //LOAD  2  POSITIVE  TEMP  DIFFERENCES  LEFT  WEB  AT  TOP
            lst_temp.Clear();
            lst_temp.Add(string.Format("5 10,8.9,17.8"));
            lst_temp.Add(string.Format("6 9,9.5,16.6"));
            lst_temp.Add(string.Format("7 8,10.1,15.4"));
            lst_temp.Add(string.Format("15 TO 20,1.05,2.1 "));
            lst_temp.Add(string.Format("1 TO 4,8.9,17.8 "));
            lst_temp.Add(string.Format("11 TO 14,1.05,2.1 "));

            MyList mlist;
            foreach (var item in lst_temp)
            {
                mlist = new MyList(item, ',');
                dgv_temp_1.Rows.Add(mlist.StringList.ToArray());
                
            }
            //LOAD  3  POSITIVE  TEMP  DIFFERENCES  RIGHT  WEB  AT  TOP
            lst_temp.Clear();
            lst_temp.Add(string.Format("5 10,8.9,17.8"));
            lst_temp.Add(string.Format("6 9,9.5,16.6"));
            lst_temp.Add(string.Format("7 8,10.1,15.4"));
            lst_temp.Add(string.Format("15 TO 20,1.05,2.1 "));
            lst_temp.Add(string.Format("11 TO 14,8.9,17.8 "));
            lst_temp.Add(string.Format("1 TO 4,1.05,2.1 "));

            foreach (var item in lst_temp)
            {
                mlist = new MyList(item, ',');
                dgv_temp_2.Rows.Add(mlist.StringList.ToArray());

            }

            //LOAD  4  REVERSE  TEMP  DIFFERENCES  LEFT  WEB  AT  TOP
            lst_temp.Clear();
            lst_temp.Add(string.Format("5 10,-5.3,-10.6"));
            //lst_temp.Add(string.Format("6 9,-5.51875,-10.1625"));
            lst_temp.Add(string.Format("6 9,-5.52,-10.17"));
            lst_temp.Add(string.Format("7 8,-5.65,-9.9"));
            lst_temp.Add(string.Format("15 20,-3.3,-6.6"));
            lst_temp.Add(string.Format("16 19,-3.5,-6.2"));
            lst_temp.Add(string.Format("17 18,-3.7,-5.8"));
            lst_temp.Add(string.Format("1 TO 4,-5.3,-10.6 "));
            lst_temp.Add(string.Format("11 TO 14,-3.3,-6.6 "));

            foreach (var item in lst_temp)
            {
                mlist = new MyList(item, ',');
                dgv_temp_3.Rows.Add(mlist.StringList.ToArray());
            }
            //LOAD  5  REVERSE  TEMP  DIFFERENCES  RIGHT  WEB  AT  TOP
            lst_temp.Clear();
            lst_temp.Add(string.Format("5 10,-5.3,-10.6"));
            //lst_temp.Add(string.Format("6 9,-5.51875,-10.1625"));
            lst_temp.Add(string.Format("6 9,-5.52,-10.17"));
            lst_temp.Add(string.Format("7 8,-5.65,-9.9"));
            lst_temp.Add(string.Format("15 20,-3.3,-6.6"));
            lst_temp.Add(string.Format("16 19,-3.5,-6.2"));
            lst_temp.Add(string.Format("17 18,-3.7,-5.8"));
            lst_temp.Add(string.Format("11 TO 14,-5.3,-10.6 "));
            lst_temp.Add(string.Format("1 TO 4,-3.3,-6.6 "));

            foreach (var item in lst_temp)
            {
                mlist = new MyList(item, ',');
                dgv_temp_4.Rows.Add(mlist.StringList.ToArray());

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


                #region STAAD Data
                //list.Add(string.Format("LOAD 1, TYPE 3"));
                //list.Add(string.Format("X,-13.4"));
                //list.Add(string.Format("Z,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 2, TYPE 4"));
                //list.Add(string.Format("X,-13.4"));
                //list.Add(string.Format("Z,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 3, TYPE 3, TYPE 3"));
                //list.Add(string.Format("X,-13.4,-56.8"));
                //list.Add(string.Format("Z,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 4, TYPE 4, TYPE 4"));
                //list.Add(string.Format("X,-13.4,-56.8"));
                //list.Add(string.Format("Z,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 5, TYPE 3, TYPE 3, TYPE 3"));
                //list.Add(string.Format("X,-13.4,-56.8,-100.2"));
                //list.Add(string.Format("Z,0.0,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 6, TYPE 4, TYPE 4, TYPE 4"));
                //list.Add(string.Format("X,-13.4,-56.8,-100.2"));
                //list.Add(string.Format("Z,0.0,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 7, TYPE 1"));
                //list.Add(string.Format("X,-18.8"));
                //list.Add(string.Format("Z,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 8, TYPE 2"));
                //list.Add(string.Format("X,-18.8"));
                //list.Add(string.Format("Z,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 9, TYPE 1, TYPE 1"));
                //list.Add(string.Format("X,-18.8,-57.6"));
                //list.Add(string.Format("Z,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 10, TYPE 2, TYPE 2"));
                //list.Add(string.Format("X,-18.8,-57.6"));
                //list.Add(string.Format("Z,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 11, TYPE 1, TYPE 1, TYPE 1"));
                //list.Add(string.Format("X,-18.8,-57.6,-96.4"));
                //list.Add(string.Format("Z,0.0,0.0,0.0"));
                //list.Add(string.Format(""));
                //list.Add(string.Format("LOAD 12, TYPE 2, TYPE 2, TYPE 2"));
                //list.Add(string.Format("X,-18.8,-57.6,-96.4"));
                //list.Add(string.Format("Z,0.0,0.0,0.0"));
                //list.Add(string.Format(""));
                #endregion STAAD Data


                #region ASTRA Data

                list.Add(string.Format("LOAD 1, TYPE 3"));
                list.Add(string.Format("X,-13.4"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 2, TYPE 4"));
                list.Add(string.Format("X,-13.4"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 3, TYPE 3, TYPE 3"));
                list.Add(string.Format("X,-13.4,-13.4"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 4, TYPE 4, TYPE 4"));
                list.Add(string.Format("X,-13.4,-13.4"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 5, TYPE 3, TYPE 3, TYPE 3"));
                list.Add(string.Format("X,-13.4,-13.4,-13.4"));
                list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 6, TYPE 4, TYPE 4, TYPE 4"));
                list.Add(string.Format("X,-13.4,-13.4,-13.4"));
                list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 7, TYPE 1"));
                list.Add(string.Format("X,-18.8"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 8, TYPE 2"));
                list.Add(string.Format("X,-18.8"));
                list.Add(string.Format("Z,1.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 9, TYPE 1, TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 10, TYPE 2, TYPE 2"));
                list.Add(string.Format("X,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 11, TYPE 1, TYPE 1, TYPE 1"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));
                list.Add(string.Format("LOAD 12, TYPE 2, TYPE 2, TYPE 2"));
                list.Add(string.Format("X,-18.8,-18.8,-18.8"));
                list.Add(string.Format("Z,1.5,4.5,7.5"));
                list.Add(string.Format(""));

                #endregion ASTRA Data


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
            list.Add(string.Format("TYPE 2, IRCCLASSA_BACK"));
            list.Add(string.Format("AXLE LOAD IN TONS ,6.8,6.8,6.8,6.8,11.4,11.4, 2.7,2.7"));
            list.Add(string.Format("AXLE SPACING IN METRES,3.00,3.00,3.00,4.30,1.20,3.20,1.10 "));
            list.Add(string.Format("AXLE WIDTH IN METRES, 1.800"));
            list.Add(string.Format("IMPACT FACTOR, 1.179"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 3, IRC70RWHEEL"));
            list.Add(string.Format("AXLE LOAD IN TONS,17.0,17.0,17.0,17.0,12.0,12.0,8.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,1.37,3.05,1.37,2.13,1.52,3.96"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.250"));
            list.Add(string.Format(""));
            list.Add(string.Format("TYPE 4, IRC70RWHEEL_BACK"));
            list.Add(string.Format("AXLE LOAD IN TONS,8.0,12.0,12.0,17.0,17.0,17.0,17.0"));
            list.Add(string.Format("AXLE SPACING IN METRES,3.96,1.52,2.13,1.37,3.05,1.37"));
            list.Add(string.Format("AXLE WIDTH IN METRES,2.900"));
            list.Add(string.Format("IMPACT FACTOR, 1.250"));
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

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads_list = new List<List<string>>();
        List<string> list_load_names = new List<string>();

        public void LONG_GIRDER_LL_TXT()
        {
            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            List<string> long_ll_impact = new List<string>();

            bool flag = false;

            list_load_names = new List<string>();

            for (i = 0; i < dgv_long_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_long_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_long_liveloads[c, i].Value.ToString().ToUpper();

                    if (kStr.StartsWith("TYPE"))
                    {
                        list_load_names.Add(dgv_long_liveloads[c + 1, i].Value.ToString());
                    }


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
            long_ll.Add(string.Format("TYPE 5 IRC40RWHEEL"));
            long_ll.Add(string.Format("12.0 12.0 12.0 7.0 7.0 5.0 "));
            long_ll.Add(string.Format("1.07 4.27 3.05 1.22 3.66 "));
            long_ll.Add(string.Format("2.740"));
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            all_loads_list = new List<List<string>>();


            //load_list_1 = new List<string>();
            //load_list_2 = new List<string>();
            //load_list_3 = new List<string>();
            //load_list_4 = new List<string>();
            //load_list_5 = new List<string>();
            //load_list_6 = new List<string>();
            //load_total_7 = new List<string>();

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
                    all_loads_list.Add(new List<string>(list.ToArray()));
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

        public void Create_Analysis_Data()
        {

            Continuous_PSC_BoxGirderAnalysis ana = new Continuous_PSC_BoxGirderAnalysis(iApp);
            ana.Support_Distance = MyList.StringToDouble(txt_support_distance.Text, 0.5);
            ana.Input_File = Input_File;
            ana.L1 = L1;
            ana.L2 = L2;
            ana.L3 = L3;
            ana.WidthBridge = B;
            ana.WidthCantilever = Width_Cant;
            ana.Effective_Depth = EffectiveDepth;

            ana.LOADS_DL_SIDL_Continous_Span = new List<string>(txt_Ana_DL_SIDL_continuous.Lines);
            ana.LOADS_DL_SIDL_End_Span = new List<string>(txt_Ana_DL_SIDL_end_spans.Lines);
            ana.LOADS_DL_SIDL_Mid_Span = new List<string>(txt_Ana_DL_SIDL_mid_span.Lines);
            ana.LOADS_FPLL_Continous_Span = new List<string>(txt_Ana_FPLL_continuous.Lines);
            ana.LOADS_SINK_Continous_Span = new List<string>(txt_Ana_SINK_continuous.Lines);


            ana.Mid_Span_Sections.Clear();
            ana.End_Span_Sections.Clear();


            Continuous_Box_Section_List cls;

            #region End Section



            //Section SUPPORT
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_end_section_1);
            ana.End_Span_Sections.Add(cls);

            //Section 1 L1/10
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_end_section_2);
            ana.End_Span_Sections.Add(cls);

            //Section 2 L1/10
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_end_section_3);
            ana.End_Span_Sections.Add(cls);


            //Section 3 L1/10
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_end_section_4);
            ana.End_Span_Sections.Add(cls);

            //Section 4 L1/10
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_end_section_5);
            ana.End_Span_Sections.Add(cls);

            //Section 5 L1/10
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_end_section_5);
            ana.End_Span_Sections.Add(cls);

            #endregion End Section

            #region Mid Section


            //Section At Support
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_1);
            ana.Mid_Span_Sections.Add(cls);


            //Section 1 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_2);
            ana.Mid_Span_Sections.Add(cls);


            //Section 2 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_3);
            ana.Mid_Span_Sections.Add(cls);

            //Section 3 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_4);
            ana.Mid_Span_Sections.Add(cls);

            //Section 4 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_5);
            ana.Mid_Span_Sections.Add(cls);

            //Section 5 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_6);
            ana.Mid_Span_Sections.Add(cls);

            //Section 6 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_7);
            ana.Mid_Span_Sections.Add(cls);


            //Section 7 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_7);
            ana.Mid_Span_Sections.Add(cls);


            //Section 8 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_7);
            ana.Mid_Span_Sections.Add(cls);

            //Section 9 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_7);
            ana.Mid_Span_Sections.Add(cls);

            //Section 10 L2/20
            cls = new Continuous_Box_Section_List();
            cls.Calculate_Data(dgv_mid_section_7);
            ana.Mid_Span_Sections.Add(cls);



            #endregion Mid Section

            LONG_GIRDER_LL_TXT();

            ana.Live_Load_Names = list_load_names;
            ana.LOADS_Temperature_Section = Get_Temperature_Load_Data();
            ana.LOADS_Transverse_Section = Get_Transverse_Load_Data();

            //ana.Transverse_Data = Get_Transverse_Load_Data();



            ana.Create_Folders();
            ana.Create_ALL_Data(long_ll);


 


          


            Analysis = ana;


            cmb_long_open_file.Items.Clear();
            string kStr = "";
            foreach (var item in Directory.GetDirectories(user_path))
            {
                kStr = Path.GetFileNameWithoutExtension(item).ToUpper().Replace("_", " ");

                if (kStr.StartsWith("ANALYSIS"))
                    cmb_long_open_file.Items.Add(kStr);

            }

            kStr = Path.GetFileNameWithoutExtension(Analysis.File_Forces_Results).ToUpper().Replace("_", " ");
            cmb_long_open_file.Items.Add(kStr);

            kStr = Path.GetFileNameWithoutExtension(Analysis.File_Transverse_Results).ToUpper().Replace("_", " ");
            cmb_long_open_file.Items.Add(kStr);


            kStr = Path.GetFileNameWithoutExtension(File_Section_Properties).ToUpper().Replace("_", " ");

            cmb_long_open_file.Items.Add(kStr);


            for (int i = 1; i <= 12; i++)
            {
                Write_Ana_Load_Data(i);
            }
        }

        public List<string> Get_Temperature_Load_Data()
        {
            List<string> list = new List<string>();

            list.Add(string.Format("SELFWEIGHT Y  -1.0"));
            list.Add(string.Format("LOAD 1 CHANGE IN TEMP FOR 50 DEGREE CENTIGRADE IN ALL MEMBERS "));
            list.Add(string.Format("TEMP LOAD "));
            list.Add(string.Format("1 TO 24 TEMP 50 "));
            list.Add(string.Format("LOAD 2 POSITIVE TEMP DIFFERENCES LEFT MEMBER AT TOP "));
            list.Add(string.Format("TEMP LOAD "));
            for (int i = 0; i < dgv_temp_1.RowCount; i++)
            {
                list.Add(string.Format("{0} TEMP {1} {2}", dgv_temp_1[0, i].Value
                , dgv_temp_1[1, i].Value, dgv_temp_1[2, i].Value));
            }
            list.Add(string.Format("LOAD 3 POSITIVE TEMP DIFFERENCES RIGHT MEMBER AT TOP "));
            list.Add(string.Format("TEMP LOAD "));
            for (int i = 0; i < dgv_temp_2.RowCount; i++)
            {
                list.Add(string.Format("{0} TEMP {1} {2}", dgv_temp_2[0, i].Value
                , dgv_temp_2[1, i].Value, dgv_temp_2[2, i].Value));
            }
            list.Add(string.Format("LOAD 4 REVERSE TEMP DIFFERENCES LEFT MEMBER AT TOP "));
            list.Add(string.Format("TEMP LOAD "));
            for (int i = 0; i < dgv_temp_3.RowCount; i++)
            {
                list.Add(string.Format("{0} TEMP {1} {2}", dgv_temp_3[0, i].Value
                , dgv_temp_3[1, i].Value, dgv_temp_3[2, i].Value));
            }
            list.Add(string.Format("LOAD 5 REVERSE TEMP DIFFERENCES RIGHT MEMBER AT TOP "));
            list.Add(string.Format("TEMP LOAD "));
            for (int i = 0; i < dgv_temp_4.RowCount; i++)
            {
                list.Add(string.Format("{0} TEMP {1} {2}", dgv_temp_4[0, i].Value
                , dgv_temp_4[1, i].Value, dgv_temp_4[2, i].Value));
            }
            return list;
        }
        public List<string> Get_Transverse_Load_Data()
        {
            Transverse_Load_Data TLD = new Transverse_Load_Data();
            TLD.SELF_WEIGHT = txt_trans_load1.Text;

            try
            {
                return TLD.Get_Data(dgv_trans_joint, dgv_trans_member);
            }
            catch (Exception ex) { }

            return new List<string>();
        }

        private void Process_Data(string flPath)
        {

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);

            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (prs.Start())
                prs.WaitForExit();
        }

        private void Write_Ana_Load_Data(int index)
        {

            //string file_name = (IsLiveLoad) ? Deck_Analysis_LL.Input_File : Deck_Analysis_DL.Input_File;
            //string file_name = Analysis.Analysis_LL_3_CONTINUOUS_SPANS;
            string file_name = Analysis.Get_Analysis_LiveLoad_Files(index);

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

            load_lst.Add("LOAD    1   " + s);
            load_lst.Add("MEMBER LOAD");
            load_lst.Add(string.Format("1 TO {0} UNI GY -0.0001", Analysis.MemColls.Count));
            Analysis.Live_Load_List = LoadData.GetLiveLoads(Path.Combine(Path.GetDirectoryName(file_name), "ll.txt"));

            load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);

            load_lst.AddRange(all_loads_list[index - 1].ToArray());

            inp_file_cont.InsertRange(indx, load_lst);

            inp_file_cont.Remove("");

            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            //MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("UNIT kN ME");

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = null;


            if (true)
            {
                //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                //load_lst.Add("TYPE 1 CLA 1.179");
                //load_lst.Add("TYPE 2 CLB 1.188");
                //load_lst.Add("TYPE 3 A70RT 1.10");
                //load_lst.Add("TYPE 4 CLAR 1.179");
                //load_lst.Add("TYPE 5 A70RR 1.188");
                //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                //load_lst.Add("TYPE 7 RAILBG 1.25");

                //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);


                //Analysis.LoadReadFromGrid(dgv_Ana_live_load);

                if (!load_lst.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                    load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                foreach (LoadData ld in Analysis.LoadList)
                {
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }
                load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
                foreach (LoadData ld in Analysis.LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }

            }
            if (calc_width > Analysis.WidthBridge)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Analysis.WidthBridge;

                str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }


        public void Button_Enable_Disable()
        {
            if (Analysis == null) return;
            string excel_file_name = "";


            btn_Process_Analysis.Enabled = File.Exists(Analysis.Analysis_DL_SIDL_3_CONTINUOUS_SPANS);
            btn_Ana_LL_view_data.Enabled = File.Exists(Analysis.Analysis_LL_3_CONTINUOUS_SPANS);
            btn_Ana_LL_view_structure.Enabled = File.Exists(Analysis.Analysis_LL_3_CONTINUOUS_SPANS);
            btn_Ana_LL_view_moving.Enabled = File.Exists(Analysis.Analysis_REP_LL_3_CONTINUOUS_SPANS);
            btn_Ana_LL_view_report.Enabled = File.Exists(Analysis.Analysis_REP_LL_3_CONTINUOUS_SPANS);

            btn_process_box_design.Enabled = File.Exists(Analysis.File_Forces_Results);
            excel_file_name = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Box_Girder));
            btn_open_box_design.Enabled = File.Exists(excel_file_name);

            btn_process_trans_design.Enabled = File.Exists(Analysis.File_Transverse_Results);
            excel_file_name = Path.Combine(Worksheet_Folder, Path.GetFileName(Excel_Box_Transverse));
            btn_open_trans_design.Enabled = File.Exists(excel_file_name);
        }

        private void Update_Lengths()
        {
            txt_1_L1_10.Text = (L1 / 10).ToString("f3");
            txt_2_L1_10.Text = (2 * L1 / 10).ToString("f3");
            txt_3_L1_10.Text = (3 * L1 / 10).ToString("f3");
            txt_4_L1_10.Text = (4 * L1 / 10).ToString("f3");
            txt_5_L1_10.Text = (5 * L1 / 10).ToString("f3");

            txt_1_L2_20.Text = (1 * L2 / 20).ToString("f3");
            txt_2_L2_20.Text = (2 * L2 / 20).ToString("f3");
            txt_3_L2_20.Text = (3 * L2 / 20).ToString("f3");
            txt_4_L2_20.Text = (4 * L2 / 20).ToString("f3");
            txt_5_L2_20.Text = (5 * L2 / 20).ToString("f3");
            txt_6_L2_20.Text = (6 * L2 / 20).ToString("f3");
            txt_7_L2_20.Text = (7 * L2 / 20).ToString("f3");
            txt_8_L2_20.Text = (8 * L2 / 20).ToString("f3");
            txt_9_L2_20.Text = (9 * L2 / 20).ToString("f3");
            txt_10_L2_20.Text = (10 * L2 / 20).ToString("f3");

            txt_LL_load_gen.Text = ((((L1 * 2 + L2 + 2) + Get_Max_Vehicle_Length()) / MyList.StringToDouble(txt_XINCR.Text, 0.2)) + 1).ToString("0");
        }

        private void Update_Loads()
        {
            List<string> list = new List<string>();

            #region Update End Span Section Data
            list.Add(string.Format("SELFWEIGHT Y -1.0"));
            list.Add(string.Format("LOAD 1 SELF WEIGHT"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("2 UNI GY -21.54375 "));
            list.Add(string.Format("2 UNI GY -{0:f4}", txt_end_load_1.Text));
            list.Add(string.Format("3 UNI GY -{0:f4} ", txt_end_load_1.Text));
            list.Add(string.Format("4 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("5 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("6 UNI GY -21.54375 -18.3235"));
            list.Add(string.Format("6 UNI GY -{0:f4}", txt_end_load_2.Text, txt_end_load_3.Text));
            //list.Add(string.Format("7 UNI GY -18.3235 "));
            list.Add(string.Format("7 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("8 UNI GY -18.3235 "));
            list.Add(string.Format("8 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("9 UNI GY -18.3235 -17.479"));
            list.Add(string.Format("9 UNI GY -{0:f4} ", txt_end_load_4.Text));
            //list.Add(string.Format("10 UNI GY -17.479 -15.1875"));
            list.Add(string.Format("10 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("11 UNI GY -15.1875 "));
            list.Add(string.Format("11 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("12 UNI GY -15.1875 "));
            list.Add(string.Format("12 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("13 UNI GY -15.1875 "));
            list.Add(string.Format("13 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("14 UNI GY -15.1875 "));
            list.Add(string.Format("14 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("15 UNI GY -15.1875 -17.479"));
            list.Add(string.Format("15 UNI GY -{0:f4} ", txt_end_load_4.Text));
            //list.Add(string.Format("16 UNI GY -17.479 -18.3235"));
            list.Add(string.Format("16 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("17 UNI GY -18.3235 "));
            list.Add(string.Format("17 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("18 UNI GY -18.3235 "));
            list.Add(string.Format("18 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("19 UNI GY -18.3235 -21.54375"));
            list.Add(string.Format("19 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("20 UNI GY -21.54375 "));
            list.Add(string.Format("20 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("21 UNI GY -21.54375 "));
            list.Add(string.Format("21 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("22 UNI GY -21.54375 "));
            list.Add(string.Format("22 UNI GY -{0:f4} ", txt_end_load_1.Text));
            list.Add(string.Format("* END DIAPHRAGM  "));
            list.Add(string.Format("JOINT LOAD   "));
            list.Add(string.Format("3 FY -29.819  "));
            list.Add(string.Format("* FUTURE PRESTRESSING BLOCK "));
            list.Add(string.Format("5 FY -3.5"));
            list.Add(string.Format("* DEVIATOR BLOCK  "));
            list.Add(string.Format("10 13 16 FY -1.7"));
            #endregion Update End Span Section Data

            txt_Ana_DL_SIDL_end_spans.Lines = list.ToArray();
            list.Clear();

            #region Update Mid Section Data
            list.Add(string.Format("LOAD 1 SELF WEIGHT "));
            list.Add(string.Format("MEMBER LOAD   "));
            //list.Add(string.Format("3 UNI GY -21.54375 "));
            list.Add(string.Format("3 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("4 UNI GY -21.54375 "));
            list.Add(string.Format("4 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("5 UNI GY -21.54375 "));
            list.Add(string.Format("5 UNI GY -{0:f4} ", txt_mid_load_2.Text));
            //list.Add(string.Format("6 UNI GY -21.54375 -20.488"));
            list.Add(string.Format("6 UNI GY -{0:f4} ", txt_mid_load_3.Text));
            //list.Add(string.Format("7 UNI GY -20.488 -18.562"));
            list.Add(string.Format("7 UNI GY -{0:f4} ", txt_mid_load_4.Text));
            //list.Add(string.Format("8 UNI GY -18.562 -18.3235"));
            list.Add(string.Format("8 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("9 UNI GY -18.3235 "));
            list.Add(string.Format("9 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("10 UNI GY -18.3235 "));
            list.Add(string.Format("10 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("11 UNI GY -18.3235 -17.729"));
            list.Add(string.Format("11 UNI GY -{0:f4} ", txt_mid_load_6.Text));
            //list.Add(string.Format("12 UNI GY -17.729 -15.1875"));
            list.Add(string.Format("12 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("13 UNI GY -15.1875 "));
            list.Add(string.Format("13 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("14 UNI GY -15.1875 "));
            list.Add(string.Format("14 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("15 UNI GY -15.1875 "));
            list.Add(string.Format("15 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("16 UNI GY -15.1875 "));
            list.Add(string.Format("16 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("17 UNI GY -15.1875 "));
            list.Add(string.Format("17 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("18 UNI GY -15.1875 "));
            list.Add(string.Format("18 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("19 UNI GY -15.1875 "));
            list.Add(string.Format("19 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("20 UNI GY -15.1875 "));
            list.Add(string.Format("20 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("21 UNI GY -15.1875 "));
            list.Add(string.Format("21 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("22 UNI GY -15.1875 "));
            list.Add(string.Format("22 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("23 UNI GY -15.1875 -17.729"));
            list.Add(string.Format("23 UNI GY -{0:f4} ", txt_mid_load_6.Text));
            //list.Add(string.Format("24 UNI GY -17.729 -18.3235"));
            list.Add(string.Format("24 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("25 UNI GY -18.3235 "));
            list.Add(string.Format("25 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("26 UNI GY -18.3235 "));
            list.Add(string.Format("26 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("27 UNI GY -18.3235 -18.562"));
            list.Add(string.Format("27 UNI GY -{0:f4} ", txt_mid_load_4.Text));
            //list.Add(string.Format("28 UNI GY -18.562 -20.488"));
            list.Add(string.Format("28 UNI GY -{0:f4} ", txt_mid_load_3.Text));
            //list.Add(string.Format("29 UNI GY -20.488 -21.54375"));
            list.Add(string.Format("29 UNI GY -{0:f4} ", txt_mid_load_2.Text));
            //list.Add(string.Format("30 UNI GY -21.54375 "));
            list.Add(string.Format("30 UNI GY -{0:f4} ", txt_mid_load_2.Text));
            //list.Add(string.Format("31 UNI GY -21.54375 "));
            list.Add(string.Format("31 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("32 UNI GY -21.54375"))
            list.Add(string.Format("32 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("* DEVIATOR BLOCK "));
            //list.Add(string.Format("JOINT LOAD  "));
            //list.Add(string.Format("30 36 39 41 44 50 FY -1.7  "));

            #endregion
            txt_Ana_DL_SIDL_mid_span.Lines = list.ToArray();
            list.Clear();

            #region Update Continuous Span Section Data

            #region Chiranjit [2014 01 05] Comment Line

            list.Add(string.Format("SELFWEIGHT Y  -1.0"));
            list.Add(string.Format("LOAD 1 SELF WEIGHT"));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("2 UNI GY -21.54375 "));
            list.Add(string.Format("2 UNI GY -{0:f4}", txt_end_load_1.Text));
            list.Add(string.Format("3 UNI GY -{0:f4} ", txt_end_load_1.Text));
            list.Add(string.Format("4 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("5 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("6 UNI GY -21.54375 -18.3235"));
            list.Add(string.Format("6 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("7 UNI GY -18.3235 "));
            list.Add(string.Format("7 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("8 UNI GY -18.3235 "));
            list.Add(string.Format("8 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("9 UNI GY -18.3235 -17.479"));
            list.Add(string.Format("9 UNI GY -{0:f4} ", txt_end_load_4.Text));
            //list.Add(string.Format("10 UNI GY -17.479 -15.1875"));
            list.Add(string.Format("10 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("11 UNI GY -15.1875 "));
            list.Add(string.Format("11 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("12 UNI GY -15.1875 "));
            list.Add(string.Format("12 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("13 UNI GY -15.1875 "));
            list.Add(string.Format("13 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("14 UNI GY -15.1875 "));
            list.Add(string.Format("14 UNI GY -{0:f4} ", txt_end_load_5.Text));
            //list.Add(string.Format("15 UNI GY -15.1875 -17.479"));
            list.Add(string.Format("15 UNI GY -{0:f4} ", txt_end_load_4.Text));
            //list.Add(string.Format("16 UNI GY -17.479 -18.3235"));
            list.Add(string.Format("16 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("17 UNI GY -18.3235 "));
            list.Add(string.Format("17 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("18 UNI GY -18.3235 "));
            list.Add(string.Format("18 UNI GY -{0:f4} ", txt_end_load_3.Text));
            //list.Add(string.Format("19 UNI GY -18.3235 -21.54375"));
            list.Add(string.Format("19 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("20 UNI GY -21.54375 "));
            list.Add(string.Format("20 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("21 UNI GY -21.54375 "));
            list.Add(string.Format("21 UNI GY -{0:f4} ", txt_end_load_2.Text));
            //list.Add(string.Format("22 UNI GY -21.54375 "));
            list.Add(string.Format("22 UNI GY -{0:f4} ", txt_end_load_1.Text));
            //list.Add(string.Format("* END DIAPHRAGM  "));
            //list.Add(string.Format("JOINT LOAD   "));
            //list.Add(string.Format("3 FY -29.819  "));
            //list.Add(string.Format("* FUTURE PRESTRESSING BLOCK "));
            //list.Add(string.Format("5 FY -3.5  "));
            //list.Add(string.Format("* DEVIATOR BLOCK  "));
            //list.Add(string.Format("10 13 16 FY -1.7"));


            //list.Add(string.Format("LOAD 1 SELF WEIGHT "));
            //list.Add(string.Format("MEMBER LOAD   "));
            //list.Add(string.Format("3 UNI GY -21.54375 "));
            list.Add(string.Format("25 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("4 UNI GY -21.54375 "));
            list.Add(string.Format("26 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("5 UNI GY -21.54375 "));
            list.Add(string.Format("27 UNI GY -{0:f4} ", txt_mid_load_2.Text));
            //list.Add(string.Format("6 UNI GY -21.54375 -20.488"));
            list.Add(string.Format("28 UNI GY -{0:f4} ", txt_mid_load_3.Text));
            //list.Add(string.Format("7 UNI GY -20.488 -18.562"));
            list.Add(string.Format("29 UNI GY -{0:f4} ", txt_mid_load_4.Text));
            //list.Add(string.Format("8 UNI GY -18.562 -18.3235"));
            list.Add(string.Format("30 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("9 UNI GY -18.3235 "));
            list.Add(string.Format("31 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("10 UNI GY -18.3235 "));
            list.Add(string.Format("32 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("11 UNI GY -18.3235 -17.729"));
            list.Add(string.Format("33 UNI GY -{0:f4} ", txt_mid_load_6.Text));
            //list.Add(string.Format("12 UNI GY -17.729 -15.1875"));
            list.Add(string.Format("34 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("13 UNI GY -15.1875 "));
            list.Add(string.Format("35 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("14 UNI GY -15.1875 "));
            list.Add(string.Format("36 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("15 UNI GY -15.1875 "));
            list.Add(string.Format("37 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("16 UNI GY -15.1875 "));
            list.Add(string.Format("38 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("17 UNI GY -15.1875 "));
            list.Add(string.Format("39 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("18 UNI GY -15.1875 "));
            list.Add(string.Format("40 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("19 UNI GY -15.1875 "));
            list.Add(string.Format("41 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("20 UNI GY -15.1875 "));
            list.Add(string.Format("42 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("21 UNI GY -15.1875 "));
            list.Add(string.Format("43 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("22 UNI GY -15.1875 "));
            list.Add(string.Format("44 UNI GY -{0:f4} ", txt_mid_load_7.Text));
            //list.Add(string.Format("23 UNI GY -15.1875 -17.729"));
            list.Add(string.Format("45 UNI GY -{0:f4} ", txt_mid_load_6.Text));
            //list.Add(string.Format("24 UNI GY -17.729 -18.3235"));
            list.Add(string.Format("46 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("25 UNI GY -18.3235 "));
            list.Add(string.Format("47 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("26 UNI GY -18.3235 "));
            list.Add(string.Format("48 UNI GY -{0:f4} ", txt_mid_load_5.Text));
            //list.Add(string.Format("27 UNI GY -18.3235 -18.562"));
            list.Add(string.Format("49 UNI GY -{0:f4} ", txt_mid_load_4.Text));
            //list.Add(string.Format("28 UNI GY -18.562 -20.488"));
            list.Add(string.Format("50 UNI GY -{0:f4} ", txt_mid_load_3.Text));
            //list.Add(string.Format("29 UNI GY -20.488 -21.54375"));
            list.Add(string.Format("51 UNI GY -{0:f4} ", txt_mid_load_2.Text));
            //list.Add(string.Format("30 UNI GY -21.54375 "));
            list.Add(string.Format("52 UNI GY -{0:f4} ", txt_mid_load_2.Text));
            //list.Add(string.Format("31 UNI GY -21.54375 "));
            list.Add(string.Format("53 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            //list.Add(string.Format("32 UNI GY -21.54375"))
            list.Add(string.Format("54 UNI GY -{0:f4} ", txt_mid_load_1.Text));

            list.Add(string.Format("55 UNI GY -{0:f4} ", txt_mid_load_1.Text));
            list.Add(string.Format("56 UNI GY -{0:f4} ", txt_mid_load_1.Text));

            list.Add(string.Format("57 UNI GY -{0:f4} ", txt_end_load_1.Text));
            list.Add(string.Format("58 UNI GY -{0:f4} ", txt_end_load_1.Text));
            list.Add(string.Format("59 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("60 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("61 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("62 UNI GY -{0:f4} ", txt_end_load_3.Text));
            list.Add(string.Format("63 UNI GY -{0:f4} ", txt_end_load_3.Text));
            list.Add(string.Format("64 UNI GY -{0:f4} ", txt_end_load_3.Text));
            list.Add(string.Format("65 UNI GY -{0:f4} ", txt_end_load_4.Text));
            list.Add(string.Format("66 UNI GY -{0:f4} ", txt_end_load_5.Text));
            list.Add(string.Format("67 UNI GY -{0:f4} ", txt_end_load_5.Text));
            list.Add(string.Format("68 UNI GY -{0:f4} ", txt_end_load_5.Text));
            list.Add(string.Format("69 UNI GY -{0:f4} ", txt_end_load_4.Text));
            list.Add(string.Format("70 UNI GY -{0:f4} ", txt_end_load_3.Text));
            list.Add(string.Format("71 UNI GY -{0:f4} ", txt_end_load_3.Text));
            list.Add(string.Format("72 UNI GY -{0:f4} ", txt_end_load_3.Text));
            list.Add(string.Format("73 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("74 UNI GY -{0:f4} ", txt_end_load_2.Text));
            list.Add(string.Format("75 UNI GY -{0:f4} ", txt_end_load_1.Text));
            list.Add(string.Format("76 UNI GY -{0:f4}", txt_end_load_1.Text));


            //list.Add(string.Format("* DEVIATOR BLOCK "));
            //list.Add(string.Format("JOINT LOAD  "));
            ////list.Add(string.Format("* END DIAPHRAGM  "));
            ////list.Add(string.Format("JOINT LOAD   "));
            //list.Add(string.Format("3 FY -29.819  "));
            //list.Add(string.Format("* FUTURE PRESTRESSING BLOCK "));
            //list.Add(string.Format("5 FY -3.5  "));
            //list.Add(string.Format("* DEVIATOR BLOCK  "));
            //list.Add(string.Format("10 13 16 FY -1.7"));
            //list.Add(string.Format("30 36 39 41 44 50 FY -1.7  "));

            #endregion Chiranjit [2014 01 05]  Comment Line

            List<string> lst_desc = new List<string>();
            List<string> lst_jnts = new List<string>();
            List<string> lst_dir = new List<string>();
            List<string> lst_load = new List<string>();

            for (int i = 0; i < dgv_cont_span_joint.RowCount; i++)
            {
                lst_desc.Add(dgv_cont_span_joint[0, i].Value.ToString());
                lst_jnts.Add(dgv_cont_span_joint[1, i].Value.ToString());
                lst_dir.Add(dgv_cont_span_joint[2, i].Value.ToString());
                lst_load.Add(dgv_cont_span_joint[3, i].Value.ToString());
            }


            list.Clear();
            list.Add(string.Format("LOAD 1 "));
            list.Add(string.Format("JOINT LOAD"));
            //list.Add(string.Format("* DEAD LOAD REACTION FROM SPAN 1 IN TEMPORARY CONDITION"));
            //list.Add(string.Format("3 FY -364.34"));
            //list.Add(string.Format("22 FY -323.11"));
            //list.Add(string.Format("* DEAD LOAD REACTION FROM SPAN 2 IN TEMPORARY CONDITION"));
            //list.Add(string.Format("26 FY -624.21"));
            //list.Add(string.Format("54 FY -624.21"));
            //list.Add(string.Format("* DEAD LOAD REACTION FROM SPAN 3 IN TEMPORARY CONDITION"));
            //list.Add(string.Format("77 FY -323.11"));
            //list.Add(string.Format("58 FY -364.34"));
            //list.Add(string.Format("* WEIGHT OF INTERMEDIATE DIAPHRAGM"));
            //list.Add(string.Format("24 56 FY -39.328"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 78 UNI GY -11.08"));
            //list.Add(string.Format("23 24 55 56 UNI GY -24.23125"));


            int indx = 0;
            list.Add(string.Format("* {0}", lst_desc[indx]));
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));
            indx++;
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));

            indx++;
            list.Add(string.Format("* {0}", lst_desc[indx]));
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));
            indx++;
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));

            indx++;
            list.Add(string.Format("* {0}", lst_desc[indx]));
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));
            indx++;
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));

            indx++;
            list.Add(string.Format("* {0}", lst_desc[indx]));
            list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));
            indx++;
            //list.Add(string.Format("{0} {1} {2}", lst_jnts[indx], lst_dir[indx], lst_load[indx]));

            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 78 UNI GY -{0}", B));
            list.Add(string.Format("23 24 55 56 UNI GY -{0}", txt_mid_load_1.Text));



            double Hp = MyList.StringToDouble(txt_Ana_Hp.Text, 0.5);
            double Wp = MyList.StringToDouble(txt_Ana_Wp.Text, 0.4);
            double gama_c = MyList.StringToDouble(txt_ana_gama_c.Text, 2.4);
            double gama_w = MyList.StringToDouble(txt_Ana_gamma_w.Text, 2.4);
            double Bs = MyList.StringToDouble(txt_Ana_Bs.Text, 2.4);
            double Hs = MyList.StringToDouble(txt_Ana_Hs.Text, 2.4);
            double Wps = MyList.StringToDouble(txt_Ana_Wps.Text, 0.225);
            double Hps = MyList.StringToDouble(txt_Ana_Hps.Text, 0.3);
            double Dw = MyList.StringToDouble(txt_Ana_Dw.Text, 0.3);
            double util_fp = MyList.StringToDouble(txt_ana_util_fp_load.Text, 0.3);
            double water_ld = MyList.StringToDouble(txt_ana_water_load.Text, 0.3);



            //* RAILING (@ 0.3T/M) 
            //1 TO 78 UNI GY -0.6   

            //* RAILING KERB (0.5*0.4*2.4=0.48T/M)  (Hp*Wp*gama_c*2=0.48T/M) 
            //1 TO 78 UNI GY -1 


            //* FOOTPATH SLAB (1.5*0.1*2.4=0.36T/M)  (Bs*Hs*gama_c*2=0.36T/M) 
            //1 TO 78 UNI GY -0.8 

            //* ROAD KERB (0.225*0.3*2.4=0.162T/M)  (Wps*Hps*gama_c*2=0.162T/M) 
            //1 TO 78 UNI GY -0.35 

            //* WEARING COURSE ((0.05+0.125)*0.5*7.5*2.2=1.45T/M) ((Dw+0.125)*0.5*(width-canti)*2.2=1.45T/M) 
            //1 TO 78 UNI GY -1.7 

            //* UTILITIES BELOW FOOTPATH SLAB (@0.15 T/M) 
            //1 TO 78 UNI GY -0.3 

            //* WATER SUPPLY PIPE WITH FULL SUPPLY (0.15 T/M) 
            //1 TO 78 UNI GY -0.15 


            double load = MyList.StringToDouble(txt_ana_railing_load.Text, 0.6);
            list.Add(string.Format("LOAD 2 SIDL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("* RAILING (@ {0:f3} T/M) ", (load / 2.0)));
            list.Add(string.Format("1 TO 78 UNI GY -{0:f3} ", load));

            load = Hp * Wp * gama_c;

            list.Add(string.Format("* RAILING KERB ({0}*{1}*{2} = {3:f4} T/M) ", Hp, Wp, gama_c, load));
            list.Add(string.Format("1 TO 78 UNI GY -{0:f3} ", (load * 2)));

            load = Bs * Hs * gama_c;
            list.Add(string.Format("* FOOTPATH SLAB ({0}*{1}*{2} = {3:f3} T/M)", Bs, Hs, gama_c, load));
            list.Add(string.Format("1 TO 78 UNI GY -{0:f3}  ", (load * 2)));

            load = Wps * Hps * gama_c;
            list.Add(string.Format("* ROAD KERB ({0}*{1}*{2} =  {3:f3} T/M) ", Wps, Hps, gama_c, load));
            list.Add(string.Format("1 TO 78 UNI GY -{0:f3}  ", (load * 2)));

            load = (Dw + 0.125) * 0.5 * (B - Width_Cant * 2.0) * gama_w;

            list.Add(string.Format("* WEARING COURSE (({0}+0.125)*0.5*{1}*{2} = {3} T/M) ", Dw, (B - Width_Cant * 2.0), gama_w, load));
            list.Add(string.Format("1 TO 78 UNI GY -{0:f3}  ", load));
            list.Add(string.Format("* UTILITIES BELOW FOOTPATH SLAB (@{0} T/M) ", util_fp));
            list.Add(string.Format("1 TO 78 UNI GY -{0} ", util_fp));
            list.Add(string.Format("* WATER SUPPLY PIPE WITH FULL SUPPLY ({0} T/M) ", water_ld));
            list.Add(string.Format("1 TO 78 UNI GY -{0} ", water_ld));



            #endregion
            txt_Ana_DL_SIDL_continuous.Lines = list.ToArray();
            list.Clear();

            #region Update Differential SINK

            double sink = MyList.StringToDouble(txt_ana_support_load_1.Text, 15.0);
            list.Add(string.Format("LOAD 1 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORT 1 SINKS BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 FY -{0}", (sink / 1000.0)));
            list.Add(string.Format("LOAD 2 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORT 2 SINKS BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("24 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 3 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORT 3 SINKS BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("56 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 4 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORT 4 SINKS BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("77 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 5 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 1 & 2 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 24 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 6 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 1 & 3 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 56 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 7 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 1 & 4 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 77 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 8 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 1, 2 & 3 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 24 56 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 9 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 1, 2 & 4 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 24 77 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 10 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 1, 3 & 4 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("3 56 77 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 11 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 2 & 3 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("24 56 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 12 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 2 & 4 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("24 77 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 13 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 2, 3 & 4 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("24 56 77 FY -{0}", (sink/1000.0)));
            list.Add(string.Format("LOAD 14 DIFFERENTIAL SINKING OF SUPPORTS"));
            list.Add(string.Format("* SUPPORTS 3 & 4 SINK BY {0} MM", sink));
            list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
            list.Add(string.Format("56 77 FY -{0}", (sink/1000.0)));
            #endregion Update Differential SINK
            txt_Ana_SINK_continuous.Lines = list.ToArray();
            list.Clear();

            #region Foothpath Live Load
            list.Add(string.Format("LOAD 1 FPLL "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("1 TO 23 56 TO 78 UNI GY -{0}", txt_ana_fpll_case_1.Text));
            list.Add(string.Format("24 TO 55 UNI GY -{0}", txt_ana_fpll_case_2.Text));
            list.Add(string.Format("LOAD 2 FPLL SPAN 1 LOADED "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("1 TO 23 UNI GY -{0}", txt_ana_fpll_case_1.Text));
            list.Add(string.Format("LOAD 3 FPLL SPAN 2 LOADED "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("24 TO 55 UNI GY -{0}", txt_ana_fpll_case_2.Text));
            list.Add(string.Format("LOAD 4 FPLL SPAN 3 LOADED "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("56 TO 78 UNI GY -{0}", txt_ana_fpll_case_1.Text));
            list.Add(string.Format("LOAD 5 FPLL SPAN 1 AND SPAN 2 LOADED  "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("1 TO 23 UNI GY -{0}", txt_ana_fpll_case_1.Text));
            list.Add(string.Format("24 TO 55 UNI GY -{0}", txt_ana_fpll_case_2.Text));
            list.Add(string.Format("LOAD 6 FPLL SPAN 1 AND SPAN 3 LOADED  "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("1 TO 23 56 TO 78 UNI GY -{0}", txt_ana_fpll_case_1.Text));
            list.Add(string.Format("LOAD 7 FPLL SPAN 2 AND SPAN 3 LOADED  "));
            list.Add(string.Format("MEMBER LOAD "));
            list.Add(string.Format("56 TO 78 UNI GY -{0}", txt_ana_fpll_case_1.Text));
            list.Add(string.Format("24 TO 55 UNI GY -{0}", txt_ana_fpll_case_2.Text));
            #endregion Foothpath Live Load

            txt_Ana_FPLL_continuous.Lines = list.ToArray();
            list.Clear();

            //list.Add(string.Format("LOAD 1 FPLL "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("1 TO 23 56 TO 78 UNI GY -1.12  "));
            //list.Add(string.Format("24 TO 55 UNI GY -0.92 "));
            //list.Add(string.Format("LOAD 2 FPLL SPAN 1 LOADED "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("1 TO 23 UNI GY -1.12 "));
            //list.Add(string.Format("LOAD 3 FPLL SPAN 2 LOADED "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("24 TO 55 UNI GY -0.92 "));
            //list.Add(string.Format("LOAD 4 FPLL SPAN 3 LOADED "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("56 TO 78 UNI GY -1.12 "));
            //list.Add(string.Format("LOAD 5 FPLL SPAN 1 AND SPAN 2 LOADED  "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("1 TO 23 UNI GY -1.12 "));
            //list.Add(string.Format("24 TO 55 UNI GY -0.92 "));
            //list.Add(string.Format("LOAD 6 FPLL SPAN 1 AND SPAN 3 LOADED  "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("1 TO 23 56 TO 78 UNI GY -1.12  "));
            //list.Add(string.Format("LOAD 7 FPLL SPAN 2 AND SPAN 3 LOADED  "));
            //list.Add(string.Format("MEMBER LOAD "));
            //list.Add(string.Format("56 TO 78 UNI GY -1.12 "));
            //list.Add(string.Format("24 TO 55 UNI GY -0.92 "));

        }


        private string Selected_File_Name()
        {
            if (cmb_long_open_file.Items.Count == 0) return "";

            if (cmb_long_open_file.SelectedIndex == -1)
                cmb_long_open_file.SelectedIndex = 0;


            string kStr = cmb_long_open_file.Text.Replace(" ", "_");
            string file_name = "";

            if (kStr.StartsWith("ANALYSIS"))
            {
                file_name = Path.Combine(user_path, kStr);
                file_name = Path.Combine(file_name, kStr + ".txt");


                btn_Ana_LL_view_structure.Enabled = File.Exists(file_name);
                btn_Ana_LL_view_moving.Enabled = File.Exists(file_name)
                    && File.Exists(MyList.Get_LL_TXT_File(file_name))
                     && File.Exists(MyList.Get_Analysis_Report_File(file_name));

                btn_Ana_LL_view_report.Enabled = File.Exists(MyList.Get_Analysis_Report_File(file_name));

            }
            else
            {
                file_name = Path.Combine(user_path, kStr + ".TXT");

                btn_Ana_LL_view_structure.Enabled = false;
                btn_Ana_LL_view_moving.Enabled = false;
                btn_Ana_LL_view_report.Enabled = false;
            }

            btn_Ana_LL_view_data.Enabled = File.Exists(file_name);

            return file_name;
        }

        public string Excel_Box_Girder
        {
            get
            {
                string excel_file = "";
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    excel_file = Path.Combine(Application.StartupPath, @"DESIGN\Continuous PSC Box Girder\Contunuous Box Girder Analysis BS.xlsx");
                else if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                    excel_file = Path.Combine(Application.StartupPath, @"DESIGN\Continuous PSC Box Girder\Contunuous Box Girder Analysis IS.xlsx");
                if (File.Exists(excel_file) == false)
                    excel_file = Path.Combine(Application.StartupPath, @"DESIGN\Continuous PSC Box Girder\Contunuous Box Girder Analysis.xlsx");

                return excel_file;
            }
        }
        public string Excel_Box_Transverse
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return Path.Combine(Application.StartupPath, @"DESIGN\Continuous PSC Box Girder\CPSC Box Transverse Design.xls");
                
                return Path.Combine(Application.StartupPath, @"DESIGN\Continuous PSC Box Girder\CPSC Box Transverse Design.xls");
            }
        }

        #region Form Events

        private void frm_Continuous_Box_Girder_Load(object sender, EventArgs e)
        {
            #region Cross Section Data

            double gama_c = 2.4;
            gama_c = MyList.StringToDouble(txt_ana_gama_c.Text, 2.4);

            Continuous_Box_Section_List cc = new Continuous_Box_Section_List();

            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_7, 1);

            txt_mid_load_7.Text = (cc.Total_Area * gama_c).ToString("f3");



            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_6, 2);
            txt_mid_load_6.Text = (cc.Total_Area * gama_c).ToString("f3");


            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_5, 3);
            txt_mid_load_5.Text = (cc.Total_Area * gama_c).ToString("f3");

            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_4, 4);
            txt_mid_load_4.Text = (cc.Total_Area * gama_c).ToString("f3");



            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_3, 5);
            txt_mid_load_3.Text = (cc.Total_Area * gama_c).ToString("f3");


            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_2, 7);
            txt_mid_load_2.Text = (cc.Total_Area * gama_c).ToString("f3");


            cc.Grid_Defaulf_Data_Mid_Span(dgv_mid_section_1, 9);
            txt_mid_load_1.Text = (cc.Total_Area * gama_c).ToString("f3");

            cc.Grid_Defaulf_Data_End_Span(dgv_end_section_5, 1);
            txt_end_load_5.Text = (cc.Total_Area * gama_c).ToString("f3");

            cc.Grid_Defaulf_Data_End_Span(dgv_end_section_4, 2);
            txt_end_load_4.Text = (cc.Total_Area * gama_c).ToString("f3");


            cc.Grid_Defaulf_Data_End_Span(dgv_end_section_3, 3);
            txt_end_load_3.Text = (cc.Total_Area * gama_c).ToString("f3");

            cc.Grid_Defaulf_Data_End_Span(dgv_end_section_2, 6);
            txt_end_load_2.Text = (cc.Total_Area * gama_c).ToString("f3");

            cc.Grid_Defaulf_Data_End_Span(dgv_end_section_1, 5);
            txt_end_load_1.Text = (cc.Total_Area * gama_c).ToString("f3");

            #endregion

            Default_Moving_Type_LoadData(dgv_long_loads);
            Default_Moving_LoadData(dgv_long_liveloads);
            Default_Temperature_Load();
            Default_Transverse_Load();
            Default_Continuous_Span_Load();
            Update_Lengths();

            txt_LL_load_gen.Text = ((L1 * 2 + L2) / MyList.StringToDouble(txt_XINCR.Text, 0.2)).ToString("0");

            #region RCC Abutment
            Abut = new RCC_AbutmentWall(iApp);
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;
            cmb_abut_fck.SelectedIndex = 2;
            cmb_abut_fy.SelectedIndex = 1;
            #endregion RCC Abutment


            #region IRC Abutment


            tabControl1.TabPages.Remove(tab_abutment);
            uC_RCC_Abut1.iApp = iApp;
            uC_RCC_Abut1.Load_Data();

            #endregion IRC Abutment



            #region RCC Pier
            cmb_pier_2_k.SelectedIndex = 1;
            rcc_pier = new RccPier(iApp);
            cmb_rcc_pier_fck.SelectedIndex = 2;
            cmb_rcc_pier_fy.SelectedIndex = 1;
            #endregion RCC Pier

            Set_Project_Name();

            //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            //{
                Select_Moving_Load_Combo(dgv_long_loads, cmb_bs_view_moving_load);
            //}
        }

        public void Select_Moving_Load_Combo(DataGridView dgv, ComboBox cmb)
        {
            string load = "";
            cmb.Items.Clear();
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                load = dgv[0, i].Value.ToString();
                if (load.StartsWith("LOAD"))
                {
                    if (!cmb.Items.Contains(load))
                        cmb.Items.Add(load);
                }
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;

        }
        private void Open_Project()
        {

            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                    IsCreate_Data = false;

                    //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");
                    //Chiranjit [2013 07 28]
                    iApp.Read_Form_Record(this, user_path);

                    rbtn_Ana_DL_create_analysis_file.Checked = false;
                    rbtn_Ana_DL_select_analysis_file.Checked = true;
                    //grb_Ana_DL_select_analysis.Enabled = true;

                    txt_Ana_analysis_file.Text = chk_file;

                    Create_Analysis_Data();

                    Abut.FilePath = user_path;

                    uC_RCC_Abut1.iApp = iApp;

                    rcc_pier.FilePath = user_path;


                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data....", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Abut != null)
                        Abut.FilePath = user_path;
                    if (rcc_pier != null)
                        rcc_pier.FilePath = user_path;
                //}

                Button_Enable_Disable();
                grb_create_input_data.Enabled = true;
                Text_Changed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Design Option

        }


        private bool Check_Project_Folder()
        {

            if (Path.GetFileName(user_path) != Project_Name)
            {
                MessageBox.Show(this, "New Project is not created. Please create New Project.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }

        private void btn_Ana_DL_create_data_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check_Project_Folder()) return;
                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }
                
                if(iApp.IsDemo)
                {
                    txt_Ana_L1.Text = "36.0";
                    txt_Ana_L2.Text = "72.0";
                    txt_Ana_B.Text = "11.08";
                }

                //if (IsCreate_Data)
                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }
                Update_Loads();
                Create_Analysis_Data();
                //long_ll_types
                File.WriteAllLines(File_Section_Properties, Analysis.Get_Section_Properties_Summary().ToArray());

                MessageBox.Show(this, "Dead Load and Live Load Analysis Input Data files are Created in Working folder.");

                btn_Process_Analysis.Enabled = true;
                //ana.Mid_Span_Sections[0].I


                if (cmb_long_open_file.Items.Count > 0)
                    cmb_long_open_file.SelectedIndex = 0;


                Button_Enable_Disable();



                #region Save Data
                iApp.Save_Form_Record(this, user_path);

                #endregion Save Data

            }
            catch (Exception ex) { }

        }
        private void btn_Ana_browse_input_file_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                ofd.InitialDirectory = Analysis_Path;
                //ofd.InitialDirectory = user_path;
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    IsCreate_Data = false;

                    string chk_file = Path.Combine(Path.GetDirectoryName(ofd.FileName), "INPUT_DATA.TXT");

                    if (!File.Exists(chk_file)) chk_file = ofd.FileName;

                    user_path = Path.GetDirectoryName(chk_file);
                    //Chiranjit [2013 07 28]
                    iApp.Read_Form_Record(this, Analysis_Path);

                    rbtn_Ana_DL_create_analysis_file.Checked = false;
                    rbtn_Ana_DL_select_analysis_file.Checked = true;
                    //grb_Ana_DL_select_analysis.Enabled = true;

                    txt_Ana_analysis_file.Text = chk_file;

                    Create_Analysis_Data();
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Abut != null)
                        Abut.FilePath = user_path;
                    if (rcc_pier != null)
                        rcc_pier.FilePath = user_path;


                    Button_Enable_Disable();


                }
            }



        }

        private void dgv_section_1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            Continuous_Box_Section_List cbc = new Continuous_Box_Section_List();

            try
            {
                cbc.Calculate_Data(dgv);

                double gama_c = 2.4;
                if (dgv.Name == dgv_end_section_1.Name)
                    txt_end_load_1.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_end_section_2.Name)
                    txt_end_load_2.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_end_section_3.Name)
                    txt_end_load_3.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_end_section_4.Name)
                    txt_end_load_4.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_end_section_5.Name)
                    txt_end_load_5.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_1.Name)
                    txt_mid_load_1.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_2.Name)
                    txt_mid_load_2.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_3.Name)
                    txt_mid_load_3.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_4.Name)
                    txt_mid_load_4.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_5.Name)
                    txt_mid_load_5.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_6.Name)
                    txt_mid_load_6.Text = (cbc.Total_Area * gama_c).ToString("f3");

                else if (dgv.Name == dgv_mid_section_7.Name)
                    txt_mid_load_7.Text = (cbc.Total_Area * gama_c).ToString("f3");
            }
            catch (Exception ex) { }
        }

        private void btn_process_design_Click(object sender, EventArgs e)
        {

            string excel_file_name = "";
            string copy_path = "";
            //Write_All_Data(true);

            Button btn = sender as Button;

            if (btn.Name == btn_process_box_design.Name)
            {

                excel_file_name = Excel_Box_Girder;

                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));



                File.Copy(excel_file_name, copy_path, true);



                Analysis.Read_All_Forces(File.ReadAllLines(Analysis.File_Forces_Results));
                Continuous_Box_Girder_Excel_Program rcc_excel = new Continuous_Box_Girder_Excel_Program(iApp);
                rcc_excel.Excel_File_Name = copy_path;
                rcc_excel.Analysis_Data = Analysis;
                rcc_excel.Report_File_Name = Analysis.File_Forces_Results;
                rcc_excel.Update_Excel_Long_Girder();
                iApp.Excel_Open_Message();
                Button_Enable_Disable();
            }


            if (btn.Name == btn_process_trans_design.Name)
            {
                excel_file_name = Excel_Box_Transverse;

                if (!File.Exists(excel_file_name))
                {
                    MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));



                File.Copy(excel_file_name, copy_path, true);



                Continuous_Box_Girder_Excel_Program rcc_excel = new Continuous_Box_Girder_Excel_Program(iApp);
                rcc_excel.Excel_File_Name = copy_path;
                rcc_excel.Report_File_Name = Analysis.File_Transverse_Results;
                rcc_excel.Insert_Values_into_Excel_Box_Transverse();
                Button_Enable_Disable();
            }



            return;

        }

        private void btn_open_design_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            string excel_file_name = "";


            string copy_path = "";

            if (btn.Name == btn_open_box_design.Name)
            {
                excel_file_name = Excel_Box_Girder;
            }
            else if (btn.Name == btn_open_trans_design.Name)
            {
                excel_file_name = Excel_Box_Transverse;
            }

            copy_path = Path.Combine(Worksheet_Folder, Path.GetFileName(excel_file_name));


            if (File.Exists(copy_path))
            {
                iApp.OpenExcelFile(copy_path, "2011ap");
            }


        }

        private void dgv_trans_joint_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("");
        }

        private void btn_long_restore_ll_Click(object sender, EventArgs e)
        {
            Default_Moving_LoadData(dgv_long_liveloads);
            Default_Moving_Type_LoadData(dgv_long_loads);
        }

        private void cmb_long_open_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file_name = Selected_File_Name();
        }

        private void btn_Ana_LL_view_data_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            frm_Cont_Open_File f = new frm_Cont_Open_File();

            string file_name = Selected_File_Name();

            if (btn.Name == btn_Ana_LL_view_data.Name)
            {
                //iApp.View_Input_File(Deck_Analysis_DL.Input_File);
                iApp.View_Input_File(file_name);
            }
            else if (btn.Name == btn_Ana_LL_view_structure.Name)
            {
                iApp.OpenWork(file_name, false);
            }
            else if (btn.Name == btn_Ana_LL_view_moving.Name)
                iApp.OpenWork(file_name, true);
            else if (btn.Name == btn_Ana_LL_view_report.Name)
            {
                iApp.RunExe(MyList.Get_Analysis_Report_File(file_name));
            }
        }

        private void rbtn_Ana_DL_create_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;
            //btn_Ana_DL_create_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            Button_Enable_Disable();
        }

        private void txt_Ana_L1_TextChanged(object sender, EventArgs e)
        {
            Update_Lengths();

            uC_RCC_Abut1.Length = L1;
            uC_RCC_Abut1.Width = B;
            uC_RCC_Abut1.Overhang = MyList.StringToDouble(txt_support_distance.Text, 0.0);
        }

        private void btn_Process_Analysis_Click(object sender, EventArgs e)
        {
            int i = 0;
            bool flag = false;

            string flPath = Analysis.Analysis_DL_SIDL_3_CONTINUOUS_SPANS;

            ProcessCollection pcol = new ProcessCollection();

            ProcessData pd = new ProcessData();

            #region Process Analysis Data

            flPath = Analysis.Analysis_DL_SIDL_3_CONTINUOUS_SPANS;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);

            flPath = Analysis.Analysis_DL_SIDL_END_SPANS;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);


            flPath = Analysis.Analysis_DL_SIDL_MID_SPAN;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);


            flPath = Analysis.Analysis_SINK_CONTINUOUS_SPANS;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);



            flPath = Analysis.Analysis_FPLL_3_CONTINUOUS_SPANS;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);



            flPath = Analysis.Analysis_Temperature_Load_Section;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);

            flPath = Analysis.Analysis_Transverse_Load_Section;

            pd = new ProcessData();
            pd.Process_File_Name = flPath;
            pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
            pcol.Add(pd);


            for (i = 1; i <= 12; i++)
            {
                flPath = Analysis.Get_Analysis_LiveLoad_Files(i);
                pd = new ProcessData();
                pd.Process_File_Name = flPath;
                pd.Process_Text = "PROCESS ANALYSIS FOR " + Path.GetFileNameWithoutExtension(flPath).ToUpper();
                pcol.Add(pd);
            }



            if (!iApp.Show_and_Run_Process_List(pcol))
            {
                Button_Enable_Disable();
                return;
            }

            iApp.Progress_Works.Clear();

            if (File.Exists(Analysis.Analysis_REP_DL_SIDL_3_CONTINUOUS_SPANS))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from DL_SIDL_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Set Structure Geometry for DL_SIDL_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from DL_SIDL_3_CONTINUOUS_SPANS Result");
            }
            if (File.Exists(Analysis.Analysis_REP_DL_SIDL_END_SPAN))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_DL_SIDL_END_SPANS");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_DL_SIDL_END_SPANS");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_DL_SIDL_END_SPANS Result");
            }
            if (File.Exists(Analysis.Analysis_REP_DL_SIDL_MID_SPAN))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_DL_SIDL_MID_SPAN");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_DL_SIDL_MID_SPAN");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_DL_SIDL_MID_SPAN Result");
            }
            if (File.Exists(Analysis.Analysis_REP_SINK_CONTINUOUS_SPANS))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_DS_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_DS_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_DS_3_CONTINUOUS_SPANS Result");
            }
            if (File.Exists(Analysis.Analysis_REP_FPLL_3_CONTINUOUS_SPANS))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_FPLL_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_FPLL_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_FPLL_3_CONTINUOUS_SPANS Result");
            }
            if (File.Exists(MyList.Get_Analysis_Report_File(Analysis.Analysis_Temperature_Load_Section)))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_Temperature_Load_Section");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_Temperature_Load_Section");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_Temperature_Load_Section Result");
            }
            if (File.Exists(MyList.Get_Analysis_Report_File(Analysis.Analysis_Transverse_Load_Section)))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_Transverse_Load_Section");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_Transverse_Load_Section");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_Transverse_Load_Section Result");
            }
            if (File.Exists(Analysis.Analysis_REP_LL_3_CONTINUOUS_SPANS))
            {
                iApp.Progress_Works.Add("Reading Analysis Data from Analysis_LL_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Set Structure Geometry for Analysis_LL_3_CONTINUOUS_SPANS");
                //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from Analysis_LL_3_CONTINUOUS_SPANS Result");
            }


            for (i = 1; i <= 12; i++)
            {
                flPath = Analysis.Get_Analysis_REP_LiveLoad_Files(i);
                if (File.Exists(flPath))
                {
                    flPath = Analysis.Get_Analysis_LiveLoad_Files(i);
                    iApp.Progress_Works.Add("Reading Analysis Data from " + Path.GetFileNameWithoutExtension(flPath));
                    //iApp.Progress_Works.Add("Set Structure Geometry for " + Path.GetFileNameWithoutExtension(flPath));
                    //iApp.Progress_Works.Add("Reading Bending Moment & Shear Force from " + Path.GetFileNameWithoutExtension(flPath) + " Result");
                }
            }


            if (File.Exists(Analysis.Analysis_REP_DL_SIDL_3_CONTINUOUS_SPANS))
            {
                Analysis.DL_SIDL_3_CONTINUOUS_SPANS = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_DL_SIDL_3_CONTINUOUS_SPANS);
            }
            if (File.Exists(Analysis.Analysis_REP_DL_SIDL_END_SPAN))
            {
                Analysis.DL_SIDL_END_SPAN = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_DL_SIDL_END_SPAN);
            }
            if (File.Exists(Analysis.Analysis_REP_DL_SIDL_MID_SPAN))
            {
                Analysis.DL_SIDL_MID_SPAN = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_DL_SIDL_MID_SPAN);
            }
            if (File.Exists(Analysis.Analysis_REP_SINK_CONTINUOUS_SPANS))
            {
                Analysis.DS_3_CONTINUOUS_SPANS = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_SINK_CONTINUOUS_SPANS);
            }
            if (File.Exists(Analysis.Analysis_REP_FPLL_3_CONTINUOUS_SPANS))
            {
                Analysis.FPLL_3_CONTINUOUS_SPANS = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_FPLL_3_CONTINUOUS_SPANS);
            }
            if (File.Exists(Analysis.Analysis_REP_Temperature_Load_Section))
            {
                Analysis.Temperature_Section = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_Temperature_Load_Section);
            }
            if (File.Exists(Analysis.Analysis_REP_Transverse_Load_Section))
            {
                Analysis.Transverse_Section = new BridgeMemberAnalysis(iApp, Analysis.Analysis_REP_Transverse_Load_Section);
            }

            for (i = 1; i <= 12; i++)
            {
                if (i == 1)
                {
                    if (Analysis.LL_All_Analysis == null)
                        Analysis.LL_All_Analysis = new List<BridgeMemberAnalysis>();
                    else
                        Analysis.LL_All_Analysis.Clear();
                }

                if (File.Exists(Analysis.Get_Analysis_REP_LiveLoad_Files(i)))
                {
                    Analysis.LL_All_Analysis.Add(new BridgeMemberAnalysis(iApp, Analysis.Get_Analysis_REP_LiveLoad_Files(i)));
                }
            }



            iApp.Progress_Works.Clear();


            Reaction_Forces();

            #endregion Process Analysis Data


            Results = Analysis.Get_Bending_Moments_Shear_Forces();


            Results.Add("");
            Results.Add("");
            Results.Add("--------------------------------------------------------------------------------------");
            Results.Add("TRANSVERSE ANALYSIS RESULT");
            Results.Add("--------------------------------------------------------------------------------------");
            Results.Add("");
            List<string> Results2 = Analysis.Get_Transverse_Analysis_Result();

            Results.AddRange(Results2.ToArray());

            rtb_analysis_results.Lines = Results.ToArray();

            grb_create_input_data.Enabled = rbtn_Ana_DL_create_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = !rbtn_Ana_DL_create_analysis_file.Checked;

            grb_create_input_data.Enabled = !rbtn_Ana_DL_select_analysis_file.Checked;
            grb_Ana_DL_select_analysis.Enabled = rbtn_Ana_DL_select_analysis_file.Checked;
            Button_Enable_Disable();

            iApp.Save_Form_Record(this, user_path);
        }

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


        double Get_Max_Vehicle_Length()
        {
            double mvl = 13.4;

            List<double> lst_mvl = new List<double>();
            DataGridView dgv = dgv_long_liveloads;

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                dgv = dgv_long_loads;
            }
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value.ToString().StartsWith("AXLE SPACING"))
                    {
                        mvl = 0;
                        for (int c = 1; c < dgv.ColumnCount; c++)
                        {
                            try
                            {
                                mvl += MyList.StringToDouble(dgv[c, i].Value.ToString(), 0.0);
                            }
                            catch (Exception exx)
                            {

                            }
                        }
                        lst_mvl.Add(mvl);
                    }
                }
                catch (Exception ex1) { }

            }
            if (lst_mvl.Count > 0)
            {
                lst_mvl.Sort();
                lst_mvl.Reverse();
                mvl = lst_mvl[0];
            }
            return mvl;



            double veh_len, veh_gap, train_length;

            veh_len = mvl;
            veh_gap = mvl;
            //train_length = veh_len;
            train_length = 0.0;
            double eff = L1;
            bool fl = false;
            while (train_length <= eff)
            {
                fl = !fl;
                if (fl)
                {
                    train_length += veh_gap;
                    //if (train_length > L)
                    //{
                    //    train_length = train_length - veh_gap;
                    //}
                }
                else
                {
                    train_length += veh_len;
                }
            }



            //return mvl;

            return train_length;


        }
        void Text_Changed()
        {
            double x_incr = MyList.StringToDouble(txt_XINCR.Text, 0.2);

            txt_RCC_Pier_L.Text = L2.ToString("f3");

            //txt_abut_DMG.Text = (DMG + 0.2).ToString("f3");

            txt_abut_B.Text = B.ToString("f3");
            txt_RCC_Pier__B.Text = B.ToString("f3");
            txt_RCC_Pier___B.Text = B.ToString("f3");

            txt_RCC_Pier_CW.Text = B.ToString();

            //txt_RCC_Pier_DS.Text = (Ds).ToString();

            //txt_abut_gamma_c.Text = Y_c.ToString();
            //txt_RCC_Pier_gama_c.Text = Y_c.ToString();

           
            //txt_RCC_Pier_NMG.Text = NMG.ToString();
            //txt_RCC_Pier_NP.Text = NMG.ToString();


            //txt_RCC_Pier_Hp.Text = Hp.ToString();

            //txt_RCC_Pier_Wp.Text = Wp.ToString();


            //txt_abut_DMG.Text = (DMG + TMG + TMG + 0.2).ToString("f3");
            //txt_RCC_Pier_DMG.Text = (DMG + TMG + TMG + 0.2).ToString("f3");
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Text_Changed();
        }
        //Chiranjit [2012 06 20]
        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            if (chk.Name == chk_WC.Name)
            {
                grb_ana_wc.Enabled = chk.Checked;
                if (grb_ana_wc.Enabled == false)
                {
                    txt_Ana_Dw.Text = "0.000";
                    txt_Ana_gamma_w.Text = "0.000";
                }
                else
                {
                    txt_Ana_Dw.Text = "0.080";
                    txt_Ana_gamma_w.Text = "22.000";
                }
            }
            else if (chk.Name == chk_parapet.Name)
            {
                grb_ana_parapet.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_Hp.Text = "0.000";
                    txt_Ana_Wp.Text = "0.000";
                }
                else
                {
                    txt_Ana_Hp.Text = "1.200";
                    txt_Ana_Wp.Text = "0.500";
                }
            }
            else if (chk.Name == chk_sw_fp.Name)
            {
                grb_ana_sw_fp.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_Bs.Text = "0.000";
                    txt_Ana_Hs.Text = "0.000";
                    txt_Ana_Wps.Text = "0.000";
                    txt_Ana_Hps.Text = "0.000";
                }
                else
                {
                    txt_Ana_Bs.Text = "1.000";
                    txt_Ana_Hs.Text = "0.250";
                    txt_Ana_Wps.Text = "0.500";
                    txt_Ana_Hps.Text = "1.000";
                }
            }
            else if (chk.Name == chk_swf.Name)
            {
                txt_Ana_swf.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt_Ana_swf.Text = "1.000";
                }
                else
                {
                    txt_Ana_swf.Text = "1.400";
                }
            }
        }

        #endregion Form Events

        #region Abutment
        private void btn_Abutment_Process_Click(object sender, EventArgs e)
        {

            if (Abut == null) Abut = new RCC_AbutmentWall(iApp);
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
            Abut.p = MyList.StringToDouble(txt_abut_p.Text, 0.0);
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
        #endregion Abutment

        #region Design of RCC Pier
        private void cmb_pier_2_k_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            switch (cmb_pier_2_k.SelectedIndex)
            {
                case 0: txt_pier_2_k.Text = "1.50"; break;
                case 1: txt_pier_2_k.Text = "0.66"; break;
                case 2: txt_pier_2_k.Text = "0.50"; break;
                case 3: txt_pier_2_k.Text = ""; txt_pier_2_k.Focus(); break;
            }
        }

        private void btn_RccPier_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(rcc_pier.rep_file_name);
        }
        private void btn_RccPier_Process_Click(object sender, EventArgs e)
        {
            //Chiranjit [2012 07 13]
            double MX1, MY1, W1;

            MX1 = MY1 = W1 = 0.0;

            MX1 = MyList.StringToDouble(txt_RCC_Pier_Mx1.Text, 0.0);
            MY1 = MyList.StringToDouble(txt_RCC_Pier_Mz1.Text, 0.0);
            W1 = MyList.StringToDouble(txt_RCC_Pier_W1_supp_reac.Text, 0.0);

            if (MX1 == 0.0 && MY1 == 0.0 && W1 == 0.0)
            {
                string msg = "Design forces are not found from Bridge Deck Analysis in the current folder\n";
                msg += "Please enter the Design Forces manualy.\n\n";
                msg += "For Example : W1  = 6101.1 kN\n";
                msg += "            : MX1 = 274.8 kN-m\n";
                msg += "            : MZ1 = 603.1 kN-m\n";

                MessageBox.Show(msg, "ASTRA");
            }
            else
            {
                if (rcc_pier == null) rcc_pier = new RccPier(iApp);
                rcc_pier.FilePath = user_path;
                RCC_Pier_Initialize_InputData();
                rcc_pier.Calculate_Program();
                //rcc_pier.Write_User_Input();
                rcc_pier.Write_Drawing_File();
                iApp.Save_Form_Record(this, user_path);
                if (File.Exists(rcc_pier.rep_file_name)) { MessageBox.Show(this, "Report file written in " + rcc_pier.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rcc_pier.rep_file_name); }
                rcc_pier.is_process = true;
            }
            Button_Enable_Disable();
        }
        private void btn_RccPier_Drawing_Click(object sender, EventArgs e)
        {
            string drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS\\RccPierDrawings");
            iApp.RunViewer(Drawing_Folder, "RCC_Pier_Worksheet_Design_1");
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
            rcc_pier.d2 = MyList.StringToDouble(txt_RCC_Pier_DS.Text, 0.0);
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

            rcc_pier.m = MyList.StringToDouble(txt_rcc_pier_m.Text, 0.0);
            rcc_pier.sigma_s = MyList.StringToDouble(txt_rcc_pier_sigma_st.Text, 0.0);
            rcc_pier.fck1 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy1 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);
            rcc_pier.perm_flex_stress = MyList.StringToDouble(txt_rcc_pier_sigma_c.Text, 0.0);
            rcc_pier.fck2 = MyList.StringToDouble(cmb_rcc_pier_fck.Text, 0.0);
            rcc_pier.fy2 = MyList.StringToDouble(cmb_rcc_pier_fy.Text, 0.0);

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



            rcc_pier.rdia = MyList.StringToDouble(txt_RCC_Pier_rdia.Text, 0.0);
            rcc_pier.tdia = MyList.StringToDouble(txt_RCC_Pier_tdia.Text, 0.0);



            rcc_pier.hdia = MyList.StringToDouble(txt_pier_2_hdia.Text, 0.0);
            rcc_pier.hlegs = MyList.StringToDouble(txt_pier_2_hlegs.Text, 0.0);
            rcc_pier.vdia = MyList.StringToDouble(txt_pier_2_vdia.Text, 0.0);
            rcc_pier.vlegs = MyList.StringToDouble(txt_pier_2_vlegs.Text, 0.0);
            rcc_pier.vspc = MyList.StringToDouble(txt_pier_2_vspc.Text, 0.0);

        }
        #endregion Design of RCC Pier

        public void Reaction_Forces()
        {

            string s1 = "";
            string s2 = "";
            int i = 0;

            for (i = 0; i < Analysis.LL_All_Analysis[1].Supports.Count; i++)
            {
                if (i < Analysis.LL_All_Analysis[1].Supports.Count / 2)
                {
                    if (i == Analysis.LL_All_Analysis[1].Supports.Count / 2 - 1)
                    {
                        s1 += Analysis.LL_All_Analysis[1].Supports[i].NodeNo;
                    }
                    else
                        s1 += Analysis.LL_All_Analysis[1].Supports[i].NodeNo + ",";
                }
                else
                {
                    if (i == Analysis.LL_All_Analysis[1].Supports.Count - 1)
                    {
                        s2 += Analysis.LL_All_Analysis[1].Supports[i].NodeNo;
                    }
                    else
                        s2 += Analysis.LL_All_Analysis[1].Supports[i].NodeNo + ",";
                }
            }
            //double BB = MyList.StringToDouble(txt_Abut_B.Text, 8.5);
            double BB = B;


            //NodeResultData nrd = Bridge_Analysis.Structure.Node_Displacements.Get_Max_Deflection();//Chiranjit [2013 06 27]
            //NodeResultData LL_nrd = LL_Analysis.Node_Displacements.Get_Max_Deflection();//Chiranjit [2013 07 05]
            //NodeResultData DL_nrd = DL_Analysis.Node_Displacements.Get_Max_Deflection();//Chiranjit [2013 07 05]

          

            //txt_LL_node_displace.Text = LL_nrd.ToString();
            //txt_res_LL_node_trans.Text = LL_nrd.Max_Translation.ToString();
            //txt_res_LL_node_trans_jn.Text = LL_nrd.NodeNo.ToString();
            //txt_res_LL_node_trans_ld.Text = LL_nrd.LoadCase.ToString();

            //txt_DL_node_displace.Text = DL_nrd.ToString();
            //txt_res_DL_node_trans.Text = DL_nrd.Max_Translation.ToString();
            //txt_res_DL_node_trans_jn.Text = DL_nrd.NodeNo.ToString();
            //txt_res_DL_node_trans_ld.Text = DL_nrd.LoadCase.ToString();



            //frm_ViewForces(BB, Bridge_Analysis.DeadLoad_Analysis_Report, Bridge_Analysis.LiveLoad_Analysis_Report, (s1 + " " + s2));
            frm_ViewForces(BB, Analysis.Analysis_REP_DL_SIDL_3_CONTINUOUS_SPANS, Analysis.Analysis_REP_LL_3_CONTINUOUS_SPANS, (s1 + " " + s2));
            frm_ViewForces_Load();

            frm_Pier_ViewDesign_Forces(Analysis.Analysis_REP_LL_3_CONTINUOUS_SPANS, s1, s2);
            frm_ViewDesign_Forces_Load();


            

            //txt_ana_DLSR.Text = Total_DeadLoad_Reaction;
            //txt_ana_LLSR.Text = Total_LiveLoad_Reaction;

            //txt_ana_TSRP.Text = txt_final_vert_rec_kN.Text;
            //txt_ana_MSLD.Text = txt_max_Mx_kN.Text;
            //txt_ana_MSTD.Text = txt_max_Mz_kN.Text;

            txt_RCC_Pier_W1_supp_reac.Text = txt_final_vert_rec_kN.Text;
            txt_RCC_Pier_Mx1.Text = txt_max_Mx_kN.Text;
            txt_RCC_Pier_Mz1.Text = txt_max_Mz_kN.Text;

            txt_abut_w6.Text = Total_LiveLoad_Reaction;
            txt_pier_2_P3.Text = Total_LiveLoad_Reaction;
            txt_abut_w6.ForeColor = Color.Red;

            txt_abut_w5.Text = Total_DeadLoad_Reaction;
            txt_pier_2_P2.Text = Total_DeadLoad_Reaction;
            txt_abut_w5.ForeColor = Color.Red;

        }

        #region View Force
        string DL_Analysis_Rep = "";
        string LL_Analysis_Rep = "";

        SupportReactionTable DL_support_reactions = null;
        SupportReactionTable LL_support_reactions = null;
        string Supports = "";
         
        public void frm_ViewForces(double abut_width, string DL_Analysis_Report_file, string LL_Analysis_Report_file, string supports)
        {
            DL_Analysis_Rep = DL_Analysis_Report_file;
            LL_Analysis_Rep = LL_Analysis_Report_file;
            Supports = supports.Replace(",", " ");
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
                //DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                DL_support_reactions = new SupportReactionTable(iApp, DL_Analysis_Rep);
                DL_support_reactions.Clear();
                for (int i = 0; i < Analysis.DL_SIDL_3_CONTINUOUS_SPANS.Supports.Count; i++)
                {
                    SupportReaction sr = new SupportReaction();

                    sr.JointNo = Analysis.DL_SIDL_3_CONTINUOUS_SPANS.Supports[i].NodeNo;
                    var sl = new List<int>();
                    sl.Add(sr.JointNo);
                    sr.Max_Reaction = Analysis.DL_SIDL_3_CONTINUOUS_SPANS.GetJoint_R2_Shear(sl);
                    DL_support_reactions.Add(sr);
                }


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

            for (int i = 0; i < DL_support_reactions.Count; i++)
            {
                try
                {
                    sr = DL_support_reactions[i];
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
            Text_Changed_Forces();
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
            //txt_abut_w5.Text = txt_dead_kN_m.Text;
            txt_pier_2_P2.Text = txt_dead_kN_m.Text;
            //}
            //else if (txt.Name == txt_live_kN_m.Name)
            //{
            //txt_abut_w6.Text = txt_live_kN_m.Text;
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

            //txt_RCC_Pier_L.Text = txt_abut_L.Text = txt_Ana_L.Text;



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

        private void btn_open_cpsc_box_drawings_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string draw_cmd = "PSC_Box_Girder_Pier";
            string draw_folder = "PSC_Box_Girder_Pier";

            if (btn.Name == btn_dwg_box.Name)
            {
                draw_cmd = "CONTINUOUS_PSC_BOX_GIRDER";
            }
            else if (btn.Name == btn_dwg_abut.Name)
            {
                draw_cmd = "RCC_Abutment_Drawings";
            }
            else if (btn.Name == btn_dwg_pier.Name)
            {
                draw_cmd = "PSC_Box_Girder_Pier";
            }
            draw_folder = draw_cmd.Replace("_", " ") + "Drawings";

            //iApp.RunViewer(Path.Combine(Drawing_Folder, "RCC Pier Drawings"), draw_cmd);
            iApp.RunViewer(Path.Combine(Drawing_Folder, draw_folder), draw_cmd);

        }

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



        #region Create Project / Open Project

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
            Write_All_Data();

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

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    //iApp.Read_Form_Record(this, frm.Example_Path);
                    //txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    //Open_Project();

                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;

                        MyList.Folder_Copy(src_path, dest_path);
                    }


                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);

                    //Write_All_Data();

                    #endregion Save As
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                IsCreate_Data = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }


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
                return eASTRADesignType.Continuous_PSC_Box_Girder_Bridge_WS;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]

        private void btn_bs_view_moving_load_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Check_Project_Folder()) return;
                if (Path.GetFileName(user_path) != Project_Name)
                {
                    Create_Project();
                }

                if (!Directory.Exists(user_path))
                {
                    Directory.CreateDirectory(user_path);
                }
                Update_Loads();
                Update_Lengths();
                Create_Analysis_Data();
           
                string file_name = Analysis.Get_Analysis_LiveLoad_Files(cmb_bs_view_moving_load.SelectedIndex+1);

                iApp.View_MovingLoad(file_name, 0.0, MyList.StringToDouble(txt_bs_vehicle_gap.Text));
                //iApp.View_MovingLoad(ll_file, 0.0, MyList.StringToDouble(txt_bs_vehicle_gap.Text));
                
                #region Save Data

                #endregion Save Data

            }
            catch (Exception ex) { }

        }


    }

    public class Secondary
    {
        #region List Variables
        public List<double> lst_cable_nos;
        public List<double> lst_cable_nos_1;
        public List<double> lst_cable_nos_2;
        public List<double> lst_cable_nos_3;
        public List<double> lst_ecc;
        public List<double> lst_Fh_1;
        public List<double> lst_Fh_2;
        public List<double> lst_Fh_3;
        public List<double> lst_Fh_Total;
        public List<double> lst_Fv_1;
        public List<double> lst_Fv_2;
        public List<double> lst_Fv_3;
        public List<double> lst_Fv_Total;
        public List<double> lst_Mp_1;
        public List<double> lst_Mp_2;
        public List<double> lst_Mp_3;
        public List<double> lst_Mp_Total;

        public List<string> all_hogg_sagg;
        public List<string> all_sections;

        public List<double> all_inertia;
        public List<double> all_m1_m1_ds;
        public List<double> all_m1_m2_ds;
        public List<double> all_m2_m2_ds;
        public List<double> all_mid_ordinate_mo_diagram;
        public List<double> all_Mid_ordinate_of_m1_diagram;
        public List<double> all_mid_ordinate_of_m2_diagram;
        public List<double> all_mo_m1_ds;
        public List<double> all_mo_m2_ds;
        public List<double> all_negative_Fh;
        public List<double> all_negative_Ms;
        public List<double> all_negative_MTotal;
        public List<double> all_ordinate_m1_diagram;
        public List<double> all_ordinate_mo_diagram;
        public List<double> all_ordinate_ms_diagram;
        public List<double> all_ordinate_of_m2_diagram;
        public List<double> all_SF_left;
        public List<double> all_SF_right;
        public List<double> all_strip_len;
        public List<double> all_total_momnet;
        public List<double> all_V10;
        public List<double> all_V11;
        public List<double> all_V12;
        public List<double> all_V20;
        public List<double> all_V21;
        public List<double> all_V22;
        public List<double> all_Width_of_strip_6;
        public List<double> all_x;
        #endregion List Variables
        public List<string> Sections = new List<string>();
        public List<double> all_EI = new List<double>();

        public List<string> Results { get; set; }
        public Secondary()
        {
            lst_cable_nos = new List<double>();
            lst_cable_nos_1 = new List<double>();
            lst_cable_nos_2 = new List<double>();
            lst_cable_nos_3 = new List<double>();
            lst_ecc = new List<double>();
            lst_Fh_1 = new List<double>();
            lst_Fh_2 = new List<double>();
            lst_Fh_3 = new List<double>();
            lst_Fh_Total = new List<double>();
            lst_Fv_1 = new List<double>();
            lst_Fv_2 = new List<double>();
            lst_Fv_3 = new List<double>();
            lst_Fv_Total = new List<double>();
            lst_Mp_1 = new List<double>();
            lst_Mp_2 = new List<double>();
            lst_Mp_3 = new List<double>();
            lst_Mp_Total = new List<double>();

            all_hogg_sagg = new List<string>();
            all_sections = new List<string>();

            all_inertia = new List<double>();
            all_m1_m1_ds = new List<double>();
            all_m1_m2_ds = new List<double>();
            all_m2_m2_ds = new List<double>();
            all_mid_ordinate_mo_diagram = new List<double>();
            all_Mid_ordinate_of_m1_diagram = new List<double>();
            all_mid_ordinate_of_m2_diagram = new List<double>();
            all_mo_m1_ds = new List<double>();
            all_mo_m2_ds = new List<double>();
            all_negative_Fh = new List<double>();
            all_negative_Ms = new List<double>();
            all_negative_MTotal = new List<double>();
            all_ordinate_m1_diagram = new List<double>();
            all_ordinate_mo_diagram = new List<double>();
            all_ordinate_ms_diagram = new List<double>();
            all_ordinate_of_m2_diagram = new List<double>();


            all_SF_left = new List<double>();
            all_SF_right = new List<double>();
            all_strip_len = new List<double>();
            all_total_momnet = new List<double>();
            all_V10 = new List<double>();
            all_V11 = new List<double>();
            all_V12 = new List<double>();
            all_V20 = new List<double>();
            all_V21 = new List<double>();
            all_V22 = new List<double>();
            all_Width_of_strip_6 = new List<double>();
            all_x = new List<double>();


            Sections = new List<string>();
            all_EI = new List<double>();

        }

        public void Clear()
        {
            lst_cable_nos.Clear();
            lst_cable_nos_1.Clear();
            lst_cable_nos_2.Clear();
            lst_cable_nos_3.Clear();
            lst_ecc.Clear();
            lst_Fh_1.Clear();
            lst_Fh_2.Clear();
            lst_Fh_3.Clear();
            lst_Fh_Total.Clear();
            lst_Fv_1.Clear();
            lst_Fv_2.Clear();
            lst_Fv_3.Clear();
            lst_Fv_Total.Clear();
            lst_Mp_1.Clear();
            lst_Mp_2.Clear();
            lst_Mp_3.Clear();
            lst_Mp_Total.Clear();

            all_hogg_sagg.Clear();
            all_sections.Clear();

            all_inertia.Clear();
            all_m1_m1_ds.Clear();
            all_m1_m2_ds.Clear();
            all_m2_m2_ds.Clear();
            all_mid_ordinate_mo_diagram.Clear();
            all_Mid_ordinate_of_m1_diagram.Clear();
            all_mid_ordinate_of_m2_diagram.Clear();
            all_mo_m1_ds.Clear();
            all_mo_m2_ds.Clear();
            all_negative_Fh.Clear();
            all_negative_Ms.Clear();
            all_negative_MTotal.Clear();
            all_ordinate_m1_diagram.Clear();
            all_ordinate_mo_diagram.Clear();
            all_ordinate_ms_diagram.Clear();
            all_ordinate_of_m2_diagram.Clear();


            all_SF_left.Clear();
            all_SF_right.Clear();
            all_strip_len.Clear();
            all_total_momnet.Clear();
            all_V10.Clear();
            all_V11.Clear();
            all_V12.Clear();
            all_V20.Clear();
            all_V21.Clear();
            all_V22.Clear();
            all_Width_of_strip_6.Clear();
            all_x.Clear();

            Sections.Clear();
            all_EI.Clear();
        }


        public List<string> Calculate_Secondary(Continuous_Box_Girder_Design cnt)
        {
            Clear();
            List<string> list = new List<string>();

            #region Summary of Prestress Force at Various Sections after Friction and Slip

            //List<string> Sections = new List<string>();
            Sections.Add(string.Format("Support 1"));
            Sections.Add(string.Format("Face of Diaphragm"));
            Sections.Add(string.Format("1 L1/10"));
            Sections.Add(string.Format("2 L1/10"));
            Sections.Add(string.Format("3 L1/10"));
            Sections.Add(string.Format("4 L1/10"));
            Sections.Add(string.Format("5 L1/10"));
            Sections.Add(string.Format("6 L1/10"));
            Sections.Add(string.Format("7 L1/10"));
            Sections.Add(string.Format("8 L1/10"));
            Sections.Add(string.Format("9 L1/10"));
            Sections.Add(string.Format("Face of Diaphragm"));
            Sections.Add(string.Format("Support 2"));
            Sections.Add(string.Format("Face of Diaphragm"));
            Sections.Add(string.Format("1 L2/20"));
            Sections.Add(string.Format("2 L2/20"));
            Sections.Add(string.Format("3 L2/20"));
            Sections.Add(string.Format("4 L2/20"));
            Sections.Add(string.Format("5 L2/20"));
            Sections.Add(string.Format("6 L2/20"));
            Sections.Add(string.Format("7 L2/20"));
            Sections.Add(string.Format("8 L2/20"));
            Sections.Add(string.Format("9 L2/20"));
            Sections.Add(string.Format("10 L2/20"));




            //List<double> lst_Fh_1 = new List<double>();
            //List<double> lst_Mp_1 = new List<double>();
            //List<double> lst_Fv_1 = new List<double>();
            //List<double> lst_cable_nos_1 = new List<double>();


            //List<double> lst_Fh_2 = new List<double>();
            //List<double> lst_Mp_2 = new List<double>();
            //List<double> lst_Fv_2 = new List<double>();
            //List<double> lst_cable_nos_2 = new List<double>();

            //List<double> lst_Fh_3 = new List<double>();
            //List<double> lst_Mp_3 = new List<double>();
            //List<double> lst_Fv_3 = new List<double>();
            //List<double> lst_cable_nos_3 = new List<double>();

            //List<double> lst_cable_nos = new List<double>();

            double val1 = 0.0;

            int i = 0;

            for (i = 0; i < cnt.Table_Cap_Cable_PSF_after_FS_Loss.Count; i++)
            {
                var item1 = cnt.Table_Cap_Cable_PSF_after_FS_Loss[i][0];
                var item2 = cnt.Table_Cap_Cable_PSF_after_FS_Loss[i][1];
                var item3 = cnt.Table_Cap_Cable_PSF_after_FS_Loss[i][2];
                if (i == 0)
                {
                    lst_Fh_1.Add(0);
                    lst_Fh_1.Add(0);
                    lst_Mp_1.Add(0);
                    lst_Mp_1.Add(0);
                    lst_Fv_1.Add(0);
                    lst_Fv_1.Add(0);

                    lst_cable_nos_1.Add(0);
                    lst_cable_nos_1.Add(0);

                    lst_Fh_2.Add(0);
                    lst_Fh_2.Add(0);
                    lst_Mp_2.Add(0);
                    lst_Mp_2.Add(0);
                    lst_Fv_2.Add(0);
                    lst_Fv_2.Add(0);


                    lst_cable_nos_2.Add(0);
                    lst_cable_nos_2.Add(0);


                    lst_Fh_3.Add(0);
                    lst_Fh_3.Add(0);
                    lst_Mp_3.Add(0);
                    lst_Mp_3.Add(0);
                    lst_Fv_3.Add(0);
                    lst_Fv_3.Add(0);

                    lst_cable_nos_3.Add(0);
                    lst_cable_nos_3.Add(0);



                    lst_cable_nos.Add(0);
                    lst_cable_nos.Add(0);

                }
                lst_Fh_1.Add(item1.Fh);
                lst_Mp_1.Add(item1.Fh_x_e);
                lst_Fv_1.Add(item1.Fv);

                lst_Fh_2.Add(item2.Fh);
                lst_Mp_2.Add(item2.Fh_x_e);
                lst_Fv_2.Add(item2.Fv);

                lst_Fh_3.Add(item3.Fh);
                lst_Mp_3.Add(item3.Fh_x_e);
                lst_Fv_3.Add(item3.Fv);

                if (item1.Fh > 0)
                    lst_cable_nos_1.Add(item1.Nos_of_Cables);
                else
                    lst_cable_nos_1.Add(0);

                if (item2.Fh > 0)
                    lst_cable_nos_2.Add(item2.Nos_of_Cables);
                else
                    lst_cable_nos_2.Add(0);

                if (item3.Fh > 0)
                    lst_cable_nos_3.Add(item3.Nos_of_Cables);
                else
                    lst_cable_nos_3.Add(0);


                if (i == cnt.Table_Cap_Cable_PSF_after_FS_Loss.Count - 1)
                {
                    lst_Fh_1.Add(0);
                    lst_Mp_1.Add(0);
                    lst_Fv_1.Add(0);

                    lst_Fh_2.Add(0);
                    lst_Mp_2.Add(0);
                    lst_Fv_2.Add(0);

                    lst_Fh_3.Add(0);
                    lst_Mp_3.Add(0);
                    lst_Fv_3.Add(0);

                    lst_cable_nos_1.Add(0);
                    lst_cable_nos_2.Add(0);
                    lst_cable_nos_3.Add(0);

                }

            }


            //lst_Fh_Total.Add(item1.Fh + item2.Fh + item3.Fh);
            //lst_Fv_Total.Add(item1.Fv + item2.Fv + item3.Fv);
            list.Add(string.Format(""));
            list.Clear();

            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                              CAP CABLE 1                           CAP CABLE 2                     CAP CABLE 3                        TOTAL                      "));
            list.Add(string.Format("                         ----------------------------      --------------------------        --------------------------      -------------------------      Hogging"));
            list.Add(string.Format("Distance from the         Fh           Mp        Fv          Fh        Mp          Fv        Fh          Mp       Fv           Fh       Mp          Fv         or  "));
            list.Add(string.Format("C/L of Bearing           (t)         (t-m)       (t)        (t)       (t-m)       (t)        (t)       (t-m)      (t)         (t)      (t-m)        (t)     Sagging"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            string format = "{0,-20} {1,10:f2} {2,10:f2} {3,10:f2} {4,10:f2} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f4}";



            //List<double> lst_Fh_Total = new List<double>();
            //List<double> lst_Mp_Total = new List<double>();
            //List<double> lst_Fv_Total = new List<double>();
            //List<double> lst_ecc = new List<double>();


            for (i = 0; i < Sections.Count; i++)
            {
                lst_Fh_Total.Add(lst_Fh_1[i] + lst_Fh_2[i] + lst_Fv_3[i]);
                lst_Mp_Total.Add(lst_Mp_1[i] + lst_Mp_2[i] + lst_Mp_3[i]);
                lst_Fv_Total.Add(lst_Fv_1[i] + lst_Fv_2[i] + lst_Fv_3[i]);

                if (lst_Fh_Total[i] > 0.0)
                    lst_ecc.Add(lst_Mp_Total[i] / lst_Fh_Total[i]);
                else
                    lst_ecc.Add(0.0);

                list.Add(string.Format(format, Sections[i],
                    lst_Fh_1[i], lst_Mp_1[i], lst_Fv_1[i],
                    lst_Fh_2[i], lst_Mp_2[i], lst_Fv_2[i],
                    lst_Fh_3[i], lst_Mp_3[i], lst_Fv_3[i],
                    lst_Fh_Total[i], lst_Mp_Total[i], lst_Fv_Total[i],
                    (lst_Mp_Total[i] >= 0 ? "Sagging" : "Hogging")));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format("TOTAL NUMBER OF CABLES IN EACH CAP CABLE"));
            list.Add(string.Format("-------------------------------------------"));
            //list.Add(string.Format("NO. OF CABLES"));
            //list.Add(string.Format(""));


            format = "{0,-20} {1,10:f0} {2,10:f0} {3,10:f0} {4,10:f0} {5,10:f2} {6,10:f2} {7,10:f2} {8,10:f4} {9,14:f2}";

            lst_cable_nos.Clear();
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Sections                  Cable no   Cable no   Cable no     Total      Fh         Mp        Fv        Ecc         Result "));
            list.Add(string.Format("                              1          2          3         Nos      (t)        (t-m)      (t)       (m)                "));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("Support 1                     0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("Face of Diaphragm             0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("1 L1/10                       0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("2 L1/10                       0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("3 L1/10                       0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("4 L1/10                       0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("5 L1/10                       0          0          0          0       0.00       0.00       0.00     0.0000        ----   "));
            //list.Add(string.Format("6 L1/10                       4          4          0          8    1856.36   -1802.95   -1802.95    -0.9712     above c.g."));
            list.Add(string.Format(""));



            for (i = 0; i < Sections.Count; i++)
            {

                val1 = lst_cable_nos_1[i] + lst_cable_nos_2[i] + lst_cable_nos_3[i];

                lst_cable_nos.Add(val1);


                list.Add(string.Format(format, Sections[i],
                    lst_cable_nos_1[i], lst_cable_nos_2[i], lst_cable_nos_3[i], lst_cable_nos[i],
                    lst_Fh_Total[i], lst_Mp_Total[i], lst_Fv_Total[i], lst_ecc[i],
                    //=IF(AJ21>0,"below c.g.",IF(AJ21<0,"above c.g.","-"))
                    (lst_ecc[i] > 0 ? "below c.g." : lst_ecc[i] < 0 ? "above c.g." : "  ----   ")));


            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format("CALCULATION OF SECONDARY MOMENT DUE TO PRESTRESS LOSS"));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format(""));

            double fck = cnt.Fcu;
            list.Add(string.Format("Grade of concrete for Girder (fck) = M {0}", cnt.Fcu));

            double Ec = 5000 * Math.Sqrt(fck);

            list.Add(string.Format("Modulus of Elasticity of concrete (Ec) = 5000 x SQRT(fck)"));
            list.Add(string.Format("                                       = 5000 x SQRT({0})", fck));
            list.Add(string.Format("                                       = {0:f3} MPa", Ec));
            list.Add(string.Format(""));

            Ec = Ec * 1000.0;
            list.Add(string.Format("                                       = {0:f2} kN/m^2", (Ec)));
            list.Add(string.Format(""));
            Ec = Ec / 9.80655;

            list.Add(string.Format("                                       = {0:f2} t/m^2", (Ec)));
            list.Add(string.Format(""));

            double deg_indeter = 2;
            list.Add(string.Format("Degree of indeterminacy of structure = {0}", deg_indeter));
            list.Add(string.Format("No. of compatibility equations of elastic stability = {0}", deg_indeter));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("∑V10   +    ∑V11 p1   +   ∑V12   p2   =   0  ............(i)"));
            list.Add(string.Format("∑V20   +    ∑V21 p1   +   ∑V22   p2   =   0  ............(ii)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));



            #endregion Summary of Prestress Force at Various Sections after Friction and Slip

            //List<string> all_sections = new List<string>();

            #region All Sections

            all_sections.Add(string.Format("Support 1"));
            all_sections.Add(string.Format("Face of Diaphragm"));
            all_sections.Add(string.Format("1 L1/10"));
            all_sections.Add(string.Format("2 L1/10"));
            all_sections.Add(string.Format("3 L1/10"));
            all_sections.Add(string.Format("4 L1/10"));
            all_sections.Add(string.Format("5 L1/10"));
            all_sections.Add(string.Format("6 L1/10"));
            all_sections.Add(string.Format("7 L1/10"));
            all_sections.Add(string.Format("8 L1/10"));
            all_sections.Add(string.Format("9 L1/10"));
            all_sections.Add(string.Format("Face of Diaphragm"));
            all_sections.Add(string.Format("Support 2"));
            all_sections.Add(string.Format("Face of Diaphragm"));
            all_sections.Add(string.Format("1 L2/20"));
            all_sections.Add(string.Format("2 L2/20"));
            all_sections.Add(string.Format("3 L2/20"));
            all_sections.Add(string.Format("4 L2/20"));
            all_sections.Add(string.Format("5 L2/20"));
            all_sections.Add(string.Format("6 L2/20"));
            all_sections.Add(string.Format("7 L2/20"));
            all_sections.Add(string.Format("8 L2/20"));
            all_sections.Add(string.Format("9 L2/20"));
            all_sections.Add(string.Format("10 L2/20"));
            all_sections.Add(string.Format("11 L2/20"));
            all_sections.Add(string.Format("12 L2/20"));
            all_sections.Add(string.Format("13 L2/20"));
            all_sections.Add(string.Format("14 L2/20"));
            all_sections.Add(string.Format("15 L2/20"));
            all_sections.Add(string.Format("16 L2/20"));
            all_sections.Add(string.Format("17 L2/20"));
            all_sections.Add(string.Format("18 L2/20"));
            all_sections.Add(string.Format("19 L2/20"));
            all_sections.Add(string.Format("Face of Diaphragm"));
            all_sections.Add(string.Format("Support 3"));
            all_sections.Add(string.Format("Face of Diaphragm"));
            all_sections.Add(string.Format("9 L3/10"));
            all_sections.Add(string.Format("8 L3/10"));
            all_sections.Add(string.Format("7 L3/10"));
            all_sections.Add(string.Format("6 L3/10"));
            all_sections.Add(string.Format("5 L3/10"));
            all_sections.Add(string.Format("4 L3/10"));
            all_sections.Add(string.Format("3 L3/10"));
            all_sections.Add(string.Format("2 L3/10"));
            all_sections.Add(string.Format("1 L3/10"));
            all_sections.Add(string.Format("Face of Diaphragm"));
            all_sections.Add(string.Format("support 4"));
            #endregion All Sections


            //List<double> all_x = new List<double>();

            double val2 = 0.0;
            double val = 0.0;

            all_x.Clear();
            //all_sections.Add(string.Format(""));
            #region X from Support 1

            for (i = 1; i < cnt.End_Dist_Supp.Count; i++)
            {
                if (i == 13) continue;
                val = cnt.End_Dist_Supp[i] - cnt.End_Cables[0].Table_Friction_Loss[1].X;
                all_x.Add(val);
            }

            double C78 = all_x[all_x.Count - 1];



            double C40 = 1.5;
            val2 = cnt.Mid_cc_supp;

            for (i = 0; i <= 10; i++)
            {
                if (i == 0)
                    val = C40 / 2;
                else
                    val = i * val2 / 20.0;

                all_x.Add(C78 + val);
            }
            double C89 = all_x[all_x.Count - 1];




            for (i = 22; i > 11; i--)
            {
                val = (C89 - all_x[i]);

                all_x.Add(C89 + val);
            }

            double C100 = all_x[all_x.Count - 1];


            val = (C100 - all_x[all_x.Count - 2]);
            all_x.Add(C100 + val);

            for (i = 10; i >= 0; i--)
            {
                val = (C78 - all_x[i]);

                all_x.Add(C100 + val);
            }
            #endregion X from Support 1


            //List<double> all_strip_len = new List<double>();


            #region Length of each strip

            for (i = 1; i < all_x.Count; i++)
            {
                val = all_x[i] - all_x[i - 1];
                all_strip_len.Add(val);
            }
            all_strip_len.Add(0.0);

            #endregion Length of each strip

            //List<double> all_inertia = new List<double>();

            #region Inertia of Girder in each strip

            val = cnt.Analysis.End_Span_Sections[0].I;

            all_inertia.Add(val); //Support 1
            all_inertia.Add(val); //Face of Diaphragm

            //        1 L1/10    TO      4 L1/10
            for (i = 2; i < cnt.Analysis.End_Span_Sections.Count; i++)
            {
                val1 = cnt.Analysis.End_Span_Sections[i - 1].I;
                val2 = cnt.Analysis.End_Span_Sections[i].I;
                val = (val1 + val2) / 2.0;
                all_inertia.Add(val); //   1 L1/10    TO      4 L1/10
            }

            for (i = 5; i >= 0; i--)
            {
                val = all_inertia[i];

                all_inertia.Add(val); //   1 L1/10    TO      4 L1/10

            }


            val = cnt.Analysis.Mid_Span_Sections[0].I;

            all_inertia.Add(val); //Support 2



            //        1 L2/10    TO      10 L1/10
            for (i = 1; i < cnt.Analysis.Mid_Span_Sections.Count; i++)
            {
                val1 = cnt.Analysis.Mid_Span_Sections[i - 1].I;
                val2 = cnt.Analysis.Mid_Span_Sections[i].I;
                val = (val1 + val2) / 2.0;
                all_inertia.Add(val); //   1 L1/10    TO      4 L1/10
            }



            //        1 L2/10    TO      10 L1/10
            for (i = 22; i >= 0; i--)
            {
                val = all_inertia[i];
                all_inertia.Add(val); //   1 L1/10    TO      4 L1/10
            }


            all_inertia.Add(0); //   1 L1/10    TO      4 L1/10
            #endregion Inertia of Girder in each strip

            //List<double> all_Width_of_strip_6 = new List<double>();

            #region Width of strip / 6



            for (i = 0; i < all_strip_len.Count; i++)
            {
                val = all_strip_len[i] / 6.0;
                all_Width_of_strip_6.Add(val);

            }

            #endregion Width of strip / 6


            //List<double> all_ordinate_mo_diagram = new List<double>();

            #region ordinate of mo diagram due to P.M. P.M.




            for (i = 0; i < lst_Mp_Total.Count * 2 - 1; i++)
            {
                if (i < lst_Mp_Total.Count)
                    val = lst_Mp_Total[i];
                else
                    val = lst_Mp_Total[lst_Mp_Total.Count - (i - lst_Mp_Total.Count) - 1];

                all_ordinate_mo_diagram.Add(val);

            }


            #endregion ordinate of mo diagram due to P.M. P.M.

            //Mid ordinate of mo diagram due to P.M.
            //List<double> all_mid_ordinate_mo_diagram = new List<double>();

            #region Mid ordinate of mo diagram due to P.M.


            for (i = 1; i < all_ordinate_mo_diagram.Count; i++)
            {

                val = (all_ordinate_mo_diagram[i - 1] + all_ordinate_mo_diagram[i]) / 2.0;

                all_mid_ordinate_mo_diagram.Add(val);

            }
            all_mid_ordinate_mo_diagram.Add(0.0);

            #endregion Mid ordinate of mo diagram due to P.M.

            //List<double> all_ordinate_m1_diagram = new List<double>();

            #region ordinate of m1 diagram

            double I78 = -1.000;

            //=I$78*($C$100-C87)/(-C$78+C$100)
            //=I$78*(C66-C$66)/(C$78-C$66)
            for (i = 0; i < all_x.Count; i++)
            {
                if (i < 12)
                {
                    val = I78 * (all_x[i] - all_x[0]) / (C78 - all_x[0]);
                }
                else if (i == 12)
                {
                    val = I78;
                }
                else if (i < 36)
                {
                    //=I$78*($C$100-C79)/(-C$78+C$100)
                    //=I$78*($C$100-C80)/(-C$78+C$100)
                    //=I$78*($C$100-C81)/(-C$78+C$100)
                    //=I$78*(C100-C$100)/(C$78-C$100)

                    val = I78 * (C100 - all_x[i]) / (-C78 + C100);
                }
                else
                    val = 0.0;

                all_ordinate_m1_diagram.Add(val);
            }


            #endregion ordinate of m1 diagram


            //List<double> all_Mid_ordinate_of_m1_diagram = new List<double>();

            #region Mid ordinate  of m1 diagram




            for (i = 1; i < all_ordinate_m1_diagram.Count; i++)
            {
                val = (all_ordinate_m1_diagram[i - 1] - all_ordinate_m1_diagram[i]) / 2.0;

                all_Mid_ordinate_of_m1_diagram.Add(val);
            }

            all_Mid_ordinate_of_m1_diagram.Add(0);

            #endregion Mid ordinate  of m1 diagram

            //List<double> all_ordinate_of_m2_diagram = new List<double>();


            #region ordinate of m2 diagram

            double K100 = -1.0;

            for (i = 0; i < all_x.Count; i++)
            {

                if (i < 12)
                    val = 0.0;
                else if (i < 35)
                {
                    //=K$100*(C78-C$78)/(C$100-C$78)
                    //=K$100*(C79-C$78)/(C$100-C$78)
                    //=K$100*(C80-C$78)/(C$100-C$78)
                    //=K$100*(C99-C$78)/(C$100-C$78)
                    val = K100 * (all_x[i] - C78) / (C100 - C78);
                }
                else if (i == 35)
                {
                    //=K$100*(C78-C$78)/(C$100-C$78)
                    //=K$100*(C79-C$78)/(C$100-C$78)
                    //=K$100*(C80-C$78)/(C$100-C$78)
                    //=K$100*(C99-C$78)/(C$100-C$78)
                    val = K100;
                }
                else
                {
                    //=K$100*(C101-C$112)/(C$100-C$112)
                    //=K$100*(C102-C$112)/(C$100-C$112)
                    //=K$100*(C103-C$112)/(C$100-C$112)
                    val = K100 * (all_x[i] - all_x[all_x.Count - 1]) / (C100 - all_x[all_x.Count - 1]);
                }

                all_ordinate_of_m2_diagram.Add(val);
            }

            //all_ordinate_of_m2_diagram.Add(0.0);

            #endregion ordinate of m2 diagram

            //List<double> all_mid_ordinate_of_m2_diagram = new List<double>();

            #region Mid ordinate m2 diagram



            for (i = 1; i < all_ordinate_of_m2_diagram.Count; i++)
            {

                val = (all_ordinate_of_m2_diagram[i - 1] + all_ordinate_of_m2_diagram[i]) / 2.0;
                all_mid_ordinate_of_m2_diagram.Add(val);
            }

            all_mid_ordinate_of_m2_diagram.Add(0.0);

            #endregion ordinate of m2 diagram


            //List<double> all_mo_m1_ds = new List<double>();

            #region mo m1 ds
            //=(G66*I66+4*H66*J66+G67*I67)*$F66

            for (i = 1; i < all_x.Count; i++)
            {
                val = (all_ordinate_mo_diagram[i - 1] * all_ordinate_m1_diagram[i - 1]
                    + 4 * all_mid_ordinate_mo_diagram[i - 1] * all_Mid_ordinate_of_m1_diagram[i - 1]
                    + all_ordinate_mo_diagram[i] * all_ordinate_m1_diagram[i]) * all_Width_of_strip_6[i - 1];

                all_mo_m1_ds.Add(val);
            }

            all_mo_m1_ds.Add(0.0);
            #endregion mo m1 ds


            //List<double> all_mo_m2_ds = new List<double>();

            #region mo m2 ds
            //=(G66*I66+4*H66*J66+G67*I67)*$F66

            for (i = 1; i < all_x.Count; i++)
            {
                val = (all_ordinate_mo_diagram[i - 1] * all_ordinate_of_m2_diagram[i - 1]
                    + 4 * all_mid_ordinate_mo_diagram[i - 1] * all_mid_ordinate_of_m2_diagram[i - 1]
                    + all_ordinate_mo_diagram[i] * all_ordinate_of_m2_diagram[i]) * all_Width_of_strip_6[i - 1];

                all_mo_m2_ds.Add(val);
            }
            all_mo_m2_ds.Add(0.0);
            val2 = 0.0;

            #endregion mo m1 ds

            //List<double> all_m1_m1_ds = new List<double>();

            //m1 m1 ds
            #region m1 m1 ds
            //=$F66*(I66^2+I67^2+4*J66^2)

            for (i = 1; i < all_x.Count; i++)
            {
                val = all_Width_of_strip_6[i - 1] * (Math.Pow(all_ordinate_m1_diagram[i - 1], 2)
                    + Math.Pow(all_ordinate_m1_diagram[i], 2)
                    + 4 * Math.Pow(all_Mid_ordinate_of_m1_diagram[i - 1], 2));

                all_m1_m1_ds.Add(val);
            }
            val2 = 0.0;
            all_m1_m1_ds.Add(0.0);

            #endregion m1 m1 ds

            //List<double> all_m1_m2_ds = new List<double>();

            #region m1 m2 ds
            //=$F66*(I66*K66+4*J66*L66+I67*K67)

            for (i = 1; i < all_x.Count; i++)
            {
                val = all_Width_of_strip_6[i - 1] * (all_ordinate_m1_diagram[i - 1] * all_ordinate_of_m2_diagram[i - 1]
                    + 4 * all_Mid_ordinate_of_m1_diagram[i - 1] * all_mid_ordinate_of_m2_diagram[i - 1]
                    + all_ordinate_m1_diagram[i] * all_ordinate_of_m2_diagram[i]);

                all_m1_m2_ds.Add(val);
            }
            val2 = 0.0;
            all_m1_m2_ds.Add(0.0);

            #endregion m1 m1 ds

            //List<double> all_m2_m2_ds = new List<double>();

            #region m2 m2 ds
            //=$F66*(K66^2+K67^2+4*L66^2)

            for (i = 1; i < all_x.Count; i++)
            {
                val = all_Width_of_strip_6[i - 1] * (Math.Pow(all_ordinate_of_m2_diagram[i - 1], 2)
                    + Math.Pow(all_ordinate_of_m2_diagram[i], 2)
                    + 4 * Math.Pow(all_mid_ordinate_of_m2_diagram[i - 1], 2));

                all_m2_m2_ds.Add(val);
            }
            all_m2_m2_ds.Add(0.0);

            #endregion m2 m2 ds

            //List<double> all_EI = new List<double>();
            
            #region EI
            //=H$50*E66
            for (i = 0; i < all_x.Count; i++)
            {
                val = Ec * all_inertia[i];

                all_EI.Add(val);
            }

            val2 = 0.0;



            #endregion EI


            #region V10 V11 V12 V20 V21 V22

            // V10 = mo m1 ds / EI
            // V11 =     (m1 m1 ds / EI)
            // V12 =     (m1 m2 ds / EI)


            //List<double> all_V10 = new List<double>();
            //List<double> all_V11 = new List<double>();
            //List<double> all_V12 = new List<double>();

            // V20 = mo m2 ds / EI
            // V21 =     (m2 m1 ds / EI)
            // V22 =     (m2 m2 ds / EI)

            //List<double> all_V20 = new List<double>();
            //List<double> all_V21 = new List<double>();
            //List<double> all_V22 = new List<double>();



            //=H$50*E66
            for (i = 0; i < all_EI.Count - 1; i++)
            {
                // V10 = mo m1 ds / EI
                val = all_mo_m1_ds[i] / all_EI[i];
                all_V10.Add(val);
                // V11 =     (m1 m1 ds / EI)
                val = all_m1_m1_ds[i] / all_EI[i];
                all_V11.Add(val);
                // V12 =     (m1 m2 ds / EI)
                val = all_m1_m2_ds[i] / all_EI[i];
                all_V12.Add(val);




                // V20 = mo m2 ds / EI
                val = all_mo_m2_ds[i] / all_EI[i];
                all_V20.Add(val);
                // V21 =     (m2 m1 ds / EI)
                val = all_m1_m2_ds[i] / all_EI[i];
                all_V21.Add(val);
                // V22 =     (m2 m2 ds / EI)
                val = all_m2_m2_ds[i] / all_EI[i];
                all_V22.Add(val);
            }

            all_V10.Add(0.0);
            all_V11.Add(0.0);
            all_V12.Add(0.0);
            all_V20.Add(0.0);
            all_V21.Add(0.0);
            all_V22.Add(0.0);


            double a, b, c, d, e, f;
            double p1, p2;




            a = MyList.Get_Array_Sum(all_V11);
            b = MyList.Get_Array_Sum(all_V12);

            c = MyList.Get_Array_Sum(all_V21);
            d = MyList.Get_Array_Sum(all_V22);


            e = -MyList.Get_Array_Sum(all_V10);
            f = -MyList.Get_Array_Sum(all_V20);





            val1 = d / (a * d - b * c);


            val2 = b / (b * c - a * d);

            double val3 = c / (b * c - a * d);

            double val4 = a / (a * d - b * c);





            p1 = val1 * e + val2 * f;
            p2 = val3 * e + val4 * f;




            #endregion V10 V11 V12 V20 V21 V22



            //List<double> all_ordinate_ms_diagram = new List<double>();
            //List<double> all_total_momnet = new List<double>();
            //List<string> all_hogg_sagg = new List<string>();


            #region ordinate of ms diagram due to S.M. S.M. (kN-m)

            for (i = 0; i < all_ordinate_m1_diagram.Count; i++)
            {
                val = p1 * all_ordinate_m1_diagram[i] + p2 * all_ordinate_of_m2_diagram[i];

                all_ordinate_ms_diagram.Add(val);

                val += all_ordinate_mo_diagram[i];
                all_total_momnet.Add(val);

                if (val < 0)
                    all_hogg_sagg.Add("Hogging");
                else
                    all_hogg_sagg.Add("Sagging");

            }

            #endregion ordinate of ms diagram due to S.M. S.M. (kN-m)


            //List<double> all_negative_Ms = new List<double>();
            //List<double> all_negative_Fh = new List<double>();
            //List<double> all_negative_MTotal = new List<double>();
            //List<double> all_SF_left = new List<double>();
            //List<double> all_SF_right = new List<double>();


            #region Hogging and Sagging

            all_SF_left.Add(0);

            for (i = 0; i < all_x.Count; i++)
            {
                all_negative_Ms.Add(-all_ordinate_ms_diagram[i]);

                if (i <= (all_x.Count / 2))
                    all_negative_Fh.Add(-lst_Fh_Total[i]);
                else
                    all_negative_Fh.Add(-(lst_Fh_Total[lst_Fh_Total.Count - (i - lst_Fh_Total.Count + 2)]));

                all_negative_MTotal.Add(-all_total_momnet[i]);
            }

            for (i = 0; i < all_x.Count; i++)
            {
                if (i < all_x.Count - 1)
                {
                    val = -(all_negative_Ms[i] - all_negative_Ms[i + 1]) / (all_x[i + 1] - all_x[i]);

                    all_SF_right.Add(val);
                    all_SF_left.Add(val);
                }
            }

            all_SF_right.Add(0.0);

            #endregion Hogging and Sagging

            #region Format Table

            format = "{0,-3} {1,-20} {2,9:f3} {3,12:f3} {4,12:f3} {5,12:f3} {6,16:f3} {7,15:f3}";

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                              X        Length of    Inertia of     Width       Ordinate of      Mid Ordinate"));
            list.Add(string.Format("   Section                  from       each Strip   Girder in    of Strip/6    mo diagram      of mo diagram"));
            list.Add(string.Format("                          Support 1                 each Strip                 due to P.M.       due to P.M."));
            list.Add(string.Format("                            (m)           (m)         (m^4)          (m)          (t-m)             (t-m)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            for (i = 0; i < all_sections.Count; i++)
            {
                list.Add(string.Format(format, (i == 23 ? "" : i > 23 ? (24 - (i - 23)).ToString() : (i + 1).ToString()),
                   all_sections[i], all_x[i], all_strip_len[i], all_inertia[i],
                    all_Width_of_strip_6[i], all_ordinate_mo_diagram[i],
                    all_mid_ordinate_mo_diagram[i]));

            }
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            format = "{0,-3} {1,-20} {2,12:f3} {3,14:f3} {4,14:f3} {5,16:f3}";


            list.Add(string.Format("------------------------------------------------------------------------------------------"));
            list.Add(string.Format("    Section                ordinate of    Mid ordinate     ordinate of    Mid ordinate "));
            list.Add(string.Format("                            m1 diagram    of m1 diagram     m2 diagram   of m2 diagram"));
            list.Add(string.Format("                               (t-m)           (t-m)          (t-m)          (t-m)    "));
            list.Add(string.Format("------------------------------------------------------------------------------------------"));

            for (i = 0; i < all_sections.Count; i++)
            {
                list.Add(string.Format(format, (i == 23 ? "" : i > 23 ? (24 - (i - 23)).ToString() : (i + 1).ToString()),
                   all_sections[i],
                   all_ordinate_m1_diagram[i],
                   all_Mid_ordinate_of_m1_diagram[i],
                   all_ordinate_of_m2_diagram[i],
                   all_mid_ordinate_of_m2_diagram[i]));

            }
            list.Add(string.Format("------------------------------------------------------------------------------------------"));
            list.Add(string.Format("------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            format = "{0,-3} {1,-20} {2,9:f3} {3,12:f3} {4,12:f3} {5,12:f3} {6,12:f3} {7,18:f3}";


            list.Add(string.Format("------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("    Section                mo m1 ds     mo m2 ds     m1 m1 ds     m1 m2 ds      m2 m2 ds         EI        "));
            list.Add(string.Format("                                                                                               (t-m^2)     "));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------"));
            for (i = 0; i < all_sections.Count; i++)
            {
                list.Add(string.Format(format, (i == 23 ? "" : i > 23 ? (24 - (i - 23)).ToString() : (i + 1).ToString()),
                   all_sections[i],
                   all_mo_m1_ds[i],
                   all_mo_m2_ds[i],
                   all_m1_m1_ds[i],
                   all_m1_m2_ds[i],
                   all_m2_m2_ds[i],
                    all_EI[i]));
            }

            list.Add(string.Format("------------------------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            format = "{0,-3} {1,-20} {2,12:E3} {3,16:E3} {4,17:E3} {5,17:E3} {6,16:E3} {7,16:E3}";



            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("    Section                   V10               V11              V12               V20               V21               V22"));
            list.Add(string.Format("                               =                 =                =                 =                 =                 = "));
            list.Add(string.Format("                        (mo m1 ds / EI)   (m1 m1 ds / EI)   (m1 m ds / EI)   (mo m2 ds / EI)   (m1 m2 ds / EI)   (m2 m2 ds / EI)"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            for (i = 0; i < all_sections.Count; i++)
            {
                list.Add(string.Format(format, (i == 23 ? "" : i > 23 ? (24 - (i - 23)).ToString() : (i + 1).ToString()),
                   all_sections[i],
                   all_V10[i],
                   all_V11[i],
                   all_V12[i],
                   all_V20[i],
                   all_V21[i],
                   all_V22[i]));
            }
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(format, "",
               "",
              MyList.Get_Array_Sum(all_V10),
              MyList.Get_Array_Sum(all_V11),
              MyList.Get_Array_Sum(all_V12),
              MyList.Get_Array_Sum(all_V20),
              MyList.Get_Array_Sum(all_V21),
              MyList.Get_Array_Sum(all_V22)));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));




            a = MyList.Get_Array_Sum(all_V11);
            b = MyList.Get_Array_Sum(all_V12);

            c = MyList.Get_Array_Sum(all_V21);
            d = MyList.Get_Array_Sum(all_V22);


            e = -MyList.Get_Array_Sum(all_V10);
            f = -MyList.Get_Array_Sum(all_V20);

            list.Add(string.Format("∑V10  + ∑V11 * p1   +  ∑V12 * p2 = 0    .........(i)"));
            list.Add(string.Format(""));
            list.Add(string.Format("∑V20  + ∑V21 * p1   +  ∑V22 * p2 = 0    .........(ii)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("From the above equation no (i), put the values"));
            list.Add(string.Format(""));
            list.Add(string.Format("                   {0:E3}  + {1:E3} * p1   +  {2:E3} * p2 = 0 ", -e, a, b));
            list.Add(string.Format(""));
            list.Add(string.Format("From the above equation no (ii), put the values"));
            list.Add(string.Format(""));
            list.Add(string.Format("                   {0:E3}  + {1:E3} * p1   +  {2:E3} * p2 = 0 ", -f, c, d));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Forming a matrix, "));
            list.Add(string.Format(""));
            list.Add(string.Format("                   {0:E3}    {1:E3}    p1 = {2:E3} ", a, b, e));
            list.Add(string.Format(""));
            list.Add(string.Format("                   {0:E3}    {1:E3}    p2 = {2:E3} ", c, d, f));



            val1 = d / (a * d - b * c);


            val2 = b / (b * c - a * d);

            val3 = c / (b * c - a * d);

            val4 = a / (a * d - b * c);



            list.Add(string.Format(""));
            list.Add(string.Format("Inverting the matrix and multiplying by R.H.S. values,"));
            list.Add(string.Format(""));
            list.Add(string.Format("        p1  =   {0:E3}      {1:E3}      {2:E3}  ", val1, val2, e));
            list.Add(string.Format("        p2  =   {0:E3}      {1:E3}      {2:E3}  ", val3, val4, f));


            p1 = val1 * e + val2 * f;
            p2 = val3 * e + val4 * f;

            list.Add(string.Format(""));
            list.Add(string.Format("So,   p1 = {0:E3} * {1:E3} + {2:E3} * {3:E3} = {4:f3}", val1, e, val2, f, p1));
            list.Add(string.Format(""));
            list.Add(string.Format("and   p2 = {0:E3} * {1:E3} + {2:E3} * {3:E3} = {4:f3}", val3, e, val4, f, p2));
            list.Add(string.Format(""));


            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("    Section                 Ordinate of     Total Moment       Hogging                                             Shear Force Due to"));
            list.Add(string.Format("                            ms diagram      due to mo & ms        or        =-Ms         =-Fh       =-MTotal        section Moment"));
            list.Add(string.Format("                            due to S.M.    Mt = P.M. + S.M.    Sagging                                             Left         Right"));
            list.Add(string.Format("                              (t-m)            (t-m)                        (t-m)                     (t-m)         (t)         (t)"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            format = "{0,-3} {1,-18} {2,12:f3} {3,18:f3} {4,15:f3} {5,11:f3} {6,12:f3} {7,12:f3} {8,12:f3} {9,11:f3}";
            for (i = 0; i < all_sections.Count; i++)
            {
                list.Add(string.Format(format, (i == 23 ? "" : i > 23 ? (24 - (i - 23)).ToString() : (i + 1).ToString()),
                   all_sections[i],
                   all_ordinate_ms_diagram[i],
                   all_total_momnet[i],
                   all_hogg_sagg[i],
                   all_negative_Ms[i],
                   all_negative_Fh[i],
                   all_negative_MTotal[i],
                   all_SF_left[i],
                   all_SF_right[i]));
            }
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            #endregion Format Table

            //lst_cable_nos
            //lst_cable_nos_1
            //lst_cable_nos_2
            //lst_cable_nos_3
            //lst_ecc
            //lst_Fh_1
            //lst_Fh_2
            //lst_Fh_3
            //lst_Fh_Total
            //lst_Fh_Total
            //lst_Fv_1
            //lst_Fv_2
            //lst_Fv_3
            //lst_Fv_Total    
            //lst_Mp_1
            //lst_Mp_2
            //lst_Mp_3
            //lst_Mp_Total

            //all_hogg_sagg
            //all_inertia
            //all_m1_m1_ds
            //all_m1_m2_ds
            //all_m2_m2_ds
            //all_mid_ordinate_mo_diagram
            //all_Mid_ordinate_of_m1_diagram
            //all_mid_ordinate_of_m2_diagram
            //all_mo_m1_ds
            //all_mo_m2_ds
            //all_negative_Fh
            //all_negative_Ms
            //all_negative_MTotal
            //all_ordinate_m1_diagram
            //all_ordinate_mo_diagram
            //all_ordinate_ms_diagram
            //all_ordinate_of_m2_diagram
            //all_sections
            //all_SF_left
            //all_SF_right
            //all_strip_len
            //all_total_momnet
            //all_V10
            //all_V11
            //all_V12
            //all_V20
            //all_V21
            //all_V22
            //all_Width_of_strip_6
            //all_x


            Results = list;

            cnt.Table_Secondary = this;

            return list;
        }
    }

    public class Stress_Continuous
    {
        public Stress_Continuous()
        {

        }
        public List<string> Results { get; set; }

        public void Calculate_Stress_Continuous(Continuous_Box_Girder_Design cnt)
        {
            List<string> des_nodes = new List<string>();

            #region SS
            #endregion SS

            #region Design Nodes
            des_nodes.Add(string.Format("3"));
            des_nodes.Add(string.Format("4"));
            des_nodes.Add(string.Format("5"));
            des_nodes.Add(string.Format("8"));
            des_nodes.Add(string.Format("10"));
            des_nodes.Add(string.Format("12"));
            des_nodes.Add(string.Format("13"));
            des_nodes.Add(string.Format("14"));
            des_nodes.Add(string.Format("16"));
            des_nodes.Add(string.Format("18"));
            des_nodes.Add(string.Format("21"));
            des_nodes.Add(string.Format("23"));
            des_nodes.Add(string.Format("24"));
            des_nodes.Add(string.Format("25"));
            des_nodes.Add(string.Format("27"));
            des_nodes.Add(string.Format("29"));
            des_nodes.Add(string.Format("30"));
            des_nodes.Add(string.Format("32"));
            des_nodes.Add(string.Format("34"));
            des_nodes.Add(string.Format("36"));
            des_nodes.Add(string.Format("37"));
            des_nodes.Add(string.Format("38"));
            des_nodes.Add(string.Format("39"));
            des_nodes.Add(string.Format("40"));
            #endregion Design Nodes

            List<string> des_section = new List<string>();

            #region DESIGN SECTION

            des_section.Add(string.Format("Support 1"));
            des_section.Add(string.Format("Face of Diaphragm"));
            des_section.Add(string.Format("1 L1/10"));
            des_section.Add(string.Format("2 L1/10"));
            des_section.Add(string.Format("3 L1/10"));
            des_section.Add(string.Format("4 L1/10"));
            des_section.Add(string.Format("5 L1/10"));
            des_section.Add(string.Format("6 L1/10"));
            des_section.Add(string.Format("7 L1/10"));
            des_section.Add(string.Format("8 L1/10"));
            des_section.Add(string.Format("9 L1/10"));
            des_section.Add(string.Format("Face of Diaphragm"));
            des_section.Add(string.Format("Support 2"));
            des_section.Add(string.Format("Face of Diaphragm"));
            des_section.Add(string.Format("1 L2/20"));
            des_section.Add(string.Format("2 L2/20"));
            des_section.Add(string.Format("3 L2/20"));
            des_section.Add(string.Format("4 L2/20"));
            des_section.Add(string.Format("5 L2/20"));
            des_section.Add(string.Format("6 L2/20"));
            des_section.Add(string.Format("7 L2/20"));
            des_section.Add(string.Format("8 L2/20"));
            des_section.Add(string.Format("9 L2/20"));
            des_section.Add(string.Format("10 L2/20"));

            #endregion DESIGN SECTION

            List<double> dist = new List<double>();

            double val, val1, val2;

            int i = 0;

            #region Distance

            val = 0.0;
            val1 = 0.0;
            val2 = 0.0;


            for (i = 0; i < 10; i++)
            {
                val = i * cnt.End_cc_supp / 10;
                dist.Add(val);
                if (i == 0)
                {
                    dist.Add(0.5);
                }

            }


            val = cnt.End_cc_supp - 0.75;
            dist.Add(val);

            val = cnt.End_cc_supp;
            dist.Add(val);

            val = cnt.End_cc_supp + 0.75;
            dist.Add(val);

            for (i = 1; i <= 10; i++)
            {
                val =cnt.End_cc_supp + i * cnt.Mid_cc_supp / 20;
                dist.Add(val);
                if (i == 0)
                {
                    dist.Add(0.5);
                }

            }

            #endregion Distance

            List<double> lst_Area = new List<double>();
            List<double> lst_Yt = new List<double>();
            List<double> lst_Yb = new List<double>();
            List<double> lst_Zt = new List<double>();
            List<double> lst_Zb = new List<double>();

            #region Area


            for (i = 0; i < cnt.Analysis.End_Span_Sections.Count; i++)
            {
                var v = cnt.Analysis.End_Span_Sections[i];
                lst_Area.Add(v.Total_Area);
                lst_Yt.Add(v.Yt);
                lst_Yb.Add(v.Yb);
                lst_Zt.Add(v.I / v.Yt);
                lst_Zb.Add(v.I / v.Yb);

                if (i == 0)
                {
                    lst_Area.Add(v.Total_Area);
                    lst_Yt.Add(v.Yt);
                    lst_Yb.Add(v.Yb);
                    lst_Zt.Add(v.I / v.Yt);
                    lst_Zb.Add(v.I / v.Yb);
                }
            }


            for (i = 5; i > 0; i--)
            {
                val = lst_Area[i];
                lst_Area.Add(val);

                val = lst_Yt[i];
                lst_Yt.Add(val);

                val = lst_Yb[i];
                lst_Yb.Add(val);

                val = lst_Zt[i];
                lst_Zt.Add(val);

                val = lst_Zb[i];
                lst_Zb.Add(val);

            }


            for (i = 0; i < cnt.Analysis.Mid_Span_Sections.Count; i++)
            {
                var v = cnt.Analysis.Mid_Span_Sections[i];
                lst_Area.Add(v.Total_Area);
                lst_Yt.Add(v.Yt);
                lst_Yb.Add(v.Yb);
                lst_Zt.Add(v.I / v.Yt);
                lst_Zb.Add(v.I / v.Yb);

                if (i == 0)
                {
                    lst_Area.Add(v.Total_Area);
                    lst_Yt.Add(v.Yt);
                    lst_Yb.Add(v.Yb);
                    lst_Zt.Add(v.I / v.Yt);
                    lst_Zb.Add(v.I / v.Yb);
                }
            }


            #endregion Area

            #region STRESSES IN SIMPLY SUPPORTED STAGE


            List<double> lst_SSS_Ft = new List<double>();
            List<double> lst_SSS_Fb = new List<double>();


            StressSummaryData ssd_ft = (StressSummaryData)cnt.End_Stress_Summary.Data["26"];
            StressSummaryData ssd_fb = (StressSummaryData)cnt.End_Stress_Summary.Data["27"];

            lst_SSS_Ft.Add(0.0);
            lst_SSS_Fb.Add(0.0);

            for (i = 0; i < ssd_ft.Count; i++)
            {
                lst_SSS_Ft.Add(ssd_ft[i]);
                lst_SSS_Fb.Add(ssd_fb[i]);
            }

            ssd_ft = (StressSummaryData)cnt.Mid_Stress_Summary.Data["26"];
            ssd_fb = (StressSummaryData)cnt.Mid_Stress_Summary.Data["27"];

            lst_SSS_Ft.Add(0.0);
            lst_SSS_Fb.Add(0.0);

            lst_SSS_Ft.Add(0.0);
            lst_SSS_Fb.Add(0.0);

            for (i = 1; i < ssd_ft.Count; i++)
            {
                lst_SSS_Ft.Add(ssd_ft[i]);
                lst_SSS_Fb.Add(ssd_fb[i]);
            }

            #endregion STRESSES IN SIMPLY SUPPORTED STAGE


            #region DEAD LOAD CONTINUOUS STAGE


            List<double> lst_DLCS_BM = new List<double>();
            List<double> lst_DLCS_Ft = new List<double>();
            List<double> lst_DLCS_Fb = new List<double>();


            lst_DLCS_BM.Add(0.0);
            lst_DLCS_Ft.Add(0.0);
            lst_DLCS_Fb.Add(0.0);
            for (i = 1; i < cnt.Analysis.FRC_DL.Count; i++)
            {
                val = cnt.Analysis.FRC_DL[i].BM_Incr;

                val1 = val * 1000 * 100 / (lst_Zt[i] * 1000000);
                val2 = -val * 1000 * 100 / (lst_Zb[i] * 1000000);

                lst_DLCS_BM.Add(val);
                lst_DLCS_Ft.Add(val1);
                lst_DLCS_Fb.Add(val2);
            }


            #endregion DEAD LOAD CONTINUOUS STAGE

            #region TOTAL DUE TO SIMPLY SUPPORTED STGES + DEAD LOAD CONTINUOUS STAGE

            List<double> lst_A_Ft = new List<double>();
            List<double> lst_A_Fb = new List<double>();

            for (i = 0; i < lst_DLCS_Ft.Count; i++)
            {
                val1 = lst_SSS_Ft[i] + lst_DLCS_Ft[i];
                val2 = lst_SSS_Fb[i] + lst_DLCS_Fb[i];

                lst_A_Ft.Add(val1);
                lst_A_Fb.Add(val2);
            }


            #endregion TOTAL DUE TO SIMPLY SUPPORTED STGES + DEAD LOAD CONTINUOUS STAGE

            #region PRESTRESS DUE TO CONTINUITY CABLE
            
            
            List<double> lst_B_Fh = new List<double>();
            List<double> lst_B_e = new List<double>();

            lst_B_Fh.Add(0.0);
            lst_B_e.Add(0.0);

            lst_B_Fh.Add(0.0);
            lst_B_e.Add(0.0);


            for (i = 0; i < cnt.Table_Cap_Cable_PSF_after_FS_Loss.Count; i++)
            {
                val1 = cnt.Table_Cap_Cable_PSF_after_FS_Loss[i].Fh;
                val2 = cnt.Table_Cap_Cable_PSF_after_FS_Loss[i].e;

                lst_B_Fh.Add(val1);
                lst_B_e.Add(val2);
            }

            lst_B_Fh.Add(0.0);
            lst_B_e.Add(0.0);

            List<double> lst_B_y = new List<double>();
            List<double> lst_B_Fh_e = new List<double>();
            List<double> lst_B_Fh_A = new List<double>();
            List<double> lst_B_Fh_e_Zt = new List<double>();
            List<double> lst_B_Fh_e_Zb = new List<double>();

            for (i = 0; i < lst_Yb.Count; i++)
            {
                val = lst_Yb[i] - lst_B_e[i];
                lst_B_y.Add(val);

                //FH * e
                val = lst_B_Fh[i] * lst_B_e[i];
                lst_B_Fh_e.Add(val);
                
                //FH / A
                //=P13*1000/(D13*10000)
                val = (lst_B_Fh[i] * 1000) / (lst_Area[i] * 10000);
                lst_B_Fh_A.Add(val);

                //FH*e/ZT	
                //=-S13*1000*100/(G13*1000000)
                val = -lst_B_Fh_e[i] * 1000 * 100 / (lst_Zt[i] * 1000000);
                lst_B_Fh_e_Zt.Add(val);

                //FH*e/ZB
                //=S13*1000*100/(H13*1000000)
                val = -lst_B_Fh_e[i] * 1000 * 100 / (lst_Zb[i] * 1000000);
                lst_B_Fh_e_Zb.Add(val);
            }
            #endregion PRESTRESS DUE TO CONTINUITY CABLE

            val2 = 0.0;

            #region STRESS DUE TO PRESTRESS AFTER FRICTION AND SLIP

            List<double> lst_C_Ft = new List<double>();
            List<double> lst_C_Fb = new List<double>();
            for (i = 0; i < lst_B_Fh_A.Count; i++)
            {
                val1 = lst_B_Fh_A[i] + lst_B_Fh_e_Zt[i];
                val2 = lst_B_Fh_A[i] + lst_B_Fh_e_Zb[i];

                lst_C_Ft.Add(val1);
                lst_C_Fb.Add(val2);

            }

            #endregion STRESS DUE TO PRESTRESS AFTER FRICTION AND SLIP

            #region D = A + C

            List<double> lst_D_Ft = new List<double>();
            List<double> lst_D_Fb = new List<double>();
            for (i = 0; i < lst_B_Fh_A.Count; i++)
            {
                val1 = lst_A_Ft[i] + lst_C_Ft[i];
                val2 = lst_A_Fb[i] + lst_C_Fb[i];

                lst_D_Ft.Add(val1);
                lst_D_Fb.Add(val2);
            }

            #endregion D = A + C

            #region STRESS AT CG OF PRESTRESS FORCE

            double U156 = cnt.Analysis.Mid_Span_Sections[7][2].D + 
                            cnt.Analysis.Mid_Span_Sections[7][4].D +
                            cnt.Analysis.Mid_Span_Sections[7][6].D;

            List<double> lst_SCGPF = new List<double>();


            for (i = 0; i < lst_D_Fb.Count; i++)
            {
                //=((Z13-Y13)*(('72m prop'!$U$156/1000)-R13)/('72m prop'!$U$156/1000))+Y13
                //(D_Fb - D_Ft)*((3.5 - B_y)/3.5)+D_Ft 
                val = (lst_D_Fb[i] - lst_D_Ft[i]) * ((U156 - lst_B_y[i]) / 3.5) + lst_D_Ft[i];
                lst_SCGPF.Add(val);
            }

            #endregion STRESS AT CG OF PRESTRESS FORCE

            #region AVERAGE STRESS

            //=0.5*(AA13+AA14)*(C14-C13)

            List<double> lst_AVGS = new List<double>();

            for (i = 1; i < lst_SCGPF.Count; i++)
            {
                val = 0.5 * (lst_SCGPF[i - 1] + lst_SCGPF[i]) * (dist[i] - dist[i - 1]);
                lst_AVGS.Add(val);
            }
            lst_AVGS.Add(0.0);

            val1 = MyList.Get_Array_Sum(lst_AVGS);
            val = val1 / dist[dist.Count - 1];

            List<double> lst_AVGS_2 = new List<double>();

            for (i = 0; i < lst_AVGS.Count; i++)
            {
                lst_AVGS_2.Add(val);
            }
            #endregion AVERAGE STRESS

            List<string> list = new List<string>();

            string format = "";

            #region TABLE 14.1

            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.1 : "));
            list.Add(string.Format("---------------"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                  Distance    "));
            list.Add(string.Format(" Node    Section               from Support   Area        YT         YB         ZT         ZB"));
            list.Add(string.Format("                                   (m)        (m^2)      (m)        (m)        (m^3)      (m^3)"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------"));
            
            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i], 
                    des_section[i], 
                    dist[i],
                    lst_Area[i],
                    lst_Yt[i],
                    lst_Yb[i],
                    lst_Zt[i],
                    lst_Zb[i]));
            }

            list.Add(string.Format("------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion TABLE 14.1

            #region TABLE 14.2 : STRESSES IN SIMPLY SUPPORTED STAGE
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.2"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format("FT1 = FT1 + FT2"));
            list.Add(string.Format("FB1 = FB1 + FB2"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
          
            list.Add(string.Format("TABLE 14.2 : STRESSES IN SIMPLY SUPPORTED STAGE"));
            list.Add(string.Format("------------------------------------------------")); 
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     FT1         FB1"));
            list.Add(string.Format(" Node    Section                (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("-------------------------------------------------------"));
         
            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_SSS_Ft[i],
                    lst_SSS_Fb[i]));
            }
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            #endregion TABLE 14.2

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            #region TABLE 14.3  : DEAD LOAD CONTINUOUS STAGE

            list.Add(string.Format("TABLE 14.3 : DEAD LOAD CONTINUOUS STAGE"));
            list.Add(string.Format("------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     BM        FT2          FB2    "));
            list.Add(string.Format(" Node    Section                  (T-m)    (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("----------------------------------------------------------------"));
            //list.Add(string.Format("   3     Support 1                 0.000      0.000      0.000"));
            //list.Add(string.Format("   4     Face of Diaphragm         0.500     15.820     12.133"));
            //list.Add(string.Format("   5     1 L1/10                   3.600     27.489     28.074"));
            //list.Add(string.Format("   8     2 L1/10                   7.200     43.698     48.950"));


            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3} {4,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_DLCS_BM[i],
                    lst_DLCS_Ft[i],
                    lst_DLCS_Fb[i]));
            }
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion TABLE 14.3


            #region TABLE 14.4 [A] TOTAL DUE TO SIMPLY SUPPORTED STGES + DEAD LOAD CONTINUOUS STAGE
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.4 [A]"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format("FT3 = FT1 + FT2"));
            list.Add(string.Format("FB3 = FB1 + FB2"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.4 [A] : TOTAL DUE TO SIMPLY SUPPORTED STGES + DEAD LOAD CONTINUOUS STAGE"));
            list.Add(string.Format("---------------------------------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                    FT3         FB3"));
            list.Add(string.Format(" Node    Section                (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("-------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_A_Ft[i],
                    lst_A_Fb[i]
                    ));
            }
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            #endregion TABLE 14.4 TOTAL DUE TO SIMPLY SUPPORTED STGES + DEAD LOAD CONTINUOUS STAGE


            #region TABLE 14.5 [B] PRESTRESS DUE TO CONTINUITY CABLE
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.5 [B]"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format("Fh  = Fh [Ref. STEP 12]"));
            list.Add(string.Format(""));
            list.Add(string.Format("e = Eccy.  Of Cable Group  [Ref. STEP 12]"));
            list.Add(string.Format(""));
            list.Add(string.Format("y = YB - e"));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.5 [B] : PRESTRESS DUE TO CONTINUITY CABLE"));
            list.Add(string.Format("----------------------------------------------------"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                    Fh          e          y         FH*e       FH/A      FH*e/ZT    FH*e/ZB"));
            list.Add(string.Format(" Node    Section                   (T)        (m)        (m)       (T-m)     (KG/cm^2)  (KG/cm^2)  (KG/cm^2)"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_B_Fh[i],
                    lst_B_e[i],
                    lst_B_y[i],
                    lst_B_Fh_e[i],
                    lst_B_Fh_A[i],
                    lst_B_Fh_e_Zt[i],
                    lst_B_Fh_e_Zb[i]
                    ));
            }
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("---------------------------------------------------------------------------------------------------------------"));
            #endregion TABLE 14.5 PRESTRESS DUE TO CONTINUITY CABLE


            #region TABLE 14.6 [C] : STRESS DUE TO PRESTRESS AFTER FRICTION AND SLIP

            list.Add(string.Format(""));
             list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.6 [C]"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format("FT4  = (Fh / A) + (Fh*e / ZT) [Ref. TABLE 14.5]"));
            list.Add(string.Format(""));
            list.Add(string.Format("FB4 = (Fh / A) + (Fh*e / ZB) [Ref. TABLE 14.5]"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format("")); 
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.6 [C] : STRESS DUE TO PRESTRESS AFTER FRICTION AND SLIP"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     FT4         FB4"));
            list.Add(string.Format(" Node    Section                (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("-------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_C_Ft[i],
                    lst_C_Fb[i]));
            }
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            #endregion TABLE 14.2

            #region TABLE 14.7 : D

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.7 [D]"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("FT5  = FT3 + FT4 [Ref. TABLE 14.4, TABLE 14.6]"));
            list.Add(string.Format(""));
            list.Add(string.Format("FB5 = FB3 + FB4 [Ref. TABLE 14.4, TABLE 14.6]"));
            list.Add(string.Format(""));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.7 [D] :"));
            list.Add(string.Format("----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     FT5         FB5"));
            list.Add(string.Format(" Node    Section                (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("-------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_C_Ft[i],
                    lst_C_Fb[i]));
            }
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            #endregion TABLE 14.2

            #region TABLE 14.8 : CG OF PRESTRESS FORCE

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.8"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            //                    =((Z13-Y13)*(('72m prop'!$U$156/1000)-R13)/('72m prop'!$U$156/1000))+Y13
            list.Add(string.Format("STRESS  = (FB5 - FT5)*({0}- y[TABLE 14.5])/{0}) + FT5    [Ref. TABLE 14.7]",U156));
            list.Add(string.Format(""));
            // =0.5*(AA13+AA14)*(C14-C13)

            list.Add(string.Format("AVG STRESS = 0.5*(STRESS[i] + STRESS[i+1])*(Distance[i+1]-Distance[i])  (where [i] = Section Serial no.) "));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TOTAL STRESS  = ∑(AVG STRESS) / {0}", dist[dist.Count-1]));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("TABLE 14.7 [D] : STRESS AT CG OF PRESTRESS FORCE"));
            list.Add(string.Format("TABLE 14.8 : PRESTRESS FORCE AT CG"));
            list.Add(string.Format("-----------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                  DISTANCE        STRESS      AVG STRESS     TOTAL STRESS"));
            list.Add(string.Format(" Node    Section                   (m)          (KG/cm^2)     (KG/cm^2)      (KG/cm^2)"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,15:f3} {4,13:f3} {5,15:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    dist[i],
                    lst_SCGPF[i],
                    lst_AVGS[i],
                    lst_AVGS_2[i]
                    ));
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            #endregion TABLE 14.2

            int table_no = 9;

            #region TABLE 14.9 [E] STRESS DUE TO SECONDARY MOMENT

            List<double> lst_E_BM = new List<double>();

            
            for (i = 0; i < dist.Count; i++)
            {
                val = cnt.Table_Secondary.all_ordinate_ms_diagram[i];
                lst_E_BM.Add(val);
            }
            val = 0.0;

            List<double> lst_E_Ft = new List<double>();
            List<double> lst_E_Fb = new List<double>();

            for (i = 0; i < lst_E_BM.Count; i++)
            {
                val = -(lst_E_BM[i] * 100000) / (lst_Zt[i] * 1000000);
                lst_E_Ft.Add(val);
                val = (lst_E_BM[i] * 100000) / (lst_Zb[i] * 1000000);
                lst_E_Fb.Add(val);
            }



            #region TABLE 14.9 [E] STRESS DUE TO SECONDARY MOMENT

            list.Add(string.Format("TABLE 14.{0} [E] : STRESS DUE TO SECONDARY MOMENT", table_no++));
            list.Add(string.Format("----------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     BM        FT          FB    "));
            list.Add(string.Format(" Node    Section                  (T-m)    (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("----------------------------------------------------------------"));
            //list.Add(string.Format("   3     Support 1                 0.000      0.000      0.000"));
            //list.Add(string.Format("   4     Face of Diaphragm         0.500     15.820     12.133"));
            //list.Add(string.Format("   5     1 L1/10                   3.600     27.489     28.074"));
            //list.Add(string.Format("   8     2 L1/10                   7.200     43.698     48.950"));


            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3} {4,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_E_BM[i],
                    lst_E_Ft[i],
                    lst_E_Fb[i]));
            }
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion TABLE 14.3




            #endregion  TABLE 14.9 STRESS DUE TO SECONDARY MOMENT

            #region TABLE 14.10 [F] = [D] + [E]

            List<double> lst_F_Ft = new List<double>();
            List<double> lst_F_Fb = new List<double>();
          
            

            for (i = 0; i < lst_E_BM.Count; i++)
            {
                val = lst_D_Ft[i] + lst_E_Ft[i];
                lst_F_Ft.Add(val);

                val = lst_D_Fb[i] + lst_E_Fb[i];
                lst_F_Fb.Add(val);
            }

            #region TABLE 14.10 [F] = [D] + [E]

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.10 [F]"));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format("FT  = FT[D] + FT[E] [Ref. TABLE 14.7, TABLE 14.9 ]"));
            list.Add(string.Format(""));
            list.Add(string.Format("FB  = FB[D] + FB[E] [Ref. TABLE 14.7, TABLE 14.9]"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.{0} [F] : [D] + [E]", table_no++));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     FT4         FB4"));
            list.Add(string.Format(" Node    Section                (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("-------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_F_Ft[i],
                    lst_F_Fb[i]));
            }
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            #endregion TABLE 14.10



            #endregion  TABLE 14.9 STRESS DUE TO SECONDARY MOMENT


            //lst_SCGPF
            //=IF(F_Ft>F_Fb,F_Ft-((F_Ft-F_Fb)*((Yt+Yb)-B_y)/((Yt+Yb))),F_Ft+((F_Fb-F_Ft)*((Yt+Yb)-B_y)/(Yt+Yb)))


            #region  TABLE 14.11 STRESS AT C.G. OF CABLES

            List<double> lst_SCGPF_2 = new List<double>();


            for (i = 0; i < lst_E_BM.Count; i++)
            {

                if (lst_F_Ft[i] > lst_F_Fb[i])
                    val = lst_F_Ft[i] - ((lst_F_Ft[i] - lst_F_Fb[i]) * ((lst_Yt[i] + lst_Yb[i]) - lst_B_y[i]) / ((lst_Yt[i] + lst_Yb[i])));

                else
                    val = lst_F_Ft[i] + ((lst_F_Fb[i] - lst_F_Ft[i]) * ((lst_Yt[i] + lst_Yb[i]) - lst_B_y[i]) / (lst_Yt[i] + lst_Yb[i]));

                lst_SCGPF_2.Add(val);
            }




            #endregion  TABLE 14.11 STRESS AT C.G. OF CABLES


            #region AVERAGE STRESS

            //=0.5*(AA13+AA14)*(C14-C13)

            List<double> lst_AVGS_3 = new List<double>();

            for (i = 1; i < lst_SCGPF_2.Count; i++)
            {
                val = 0.5 * (lst_SCGPF_2[i - 1] + lst_SCGPF_2[i]) * (dist[i] - dist[i - 1]);
                lst_AVGS_3.Add(val);
            }
            lst_AVGS_3.Add(0.0);

            val1 = MyList.Get_Array_Sum(lst_AVGS_3);
            val = val1 / dist[dist.Count - 1];

            List<double> lst_AVGS_4 = new List<double>();

            for (i = 0; i < lst_AVGS_3.Count; i++)
            {
                lst_AVGS_4.Add(val);
            }

            #endregion AVERAGE STRESS

            #region 14.11 STRESS AT C.G. OF CABLES

            list.Add(string.Format(""));


            #region Calculation
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.11"));
            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            //                    =((Z13-Y13)*(('72m prop'!$U$156/1000)-R13)/('72m prop'!$U$156/1000))+Y13
            list.Add(string.Format("STRESS    Calculation"));
            list.Add(string.Format("------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("    IF (FT[F] > FB[F])"));
            list.Add(string.Format("        STRESS = FT[F] - ((FT[F] - FB[F]) * ((YT + YB) - y[B]) / ((YT + YB)));"));
            list.Add(string.Format(""));
            list.Add(string.Format("    IF (FT[F] < FB[F])"));
            list.Add(string.Format("        STRESS = FT[F] + ((FB[F] - FT[F]) * ((YT + YB) - y[B]) / (YT + YB));"));
            list.Add(string.Format(""));
            list.Add(string.Format("(REF. TABLE NO, 14.10 [F], 14.5 [B], 14.1)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            // =0.5*(AA13+AA14)*(C14-C13)
            list.Add(string.Format("AVG STRESS = 0.5*(STRESS[i] + STRESS[i+1])*(Distance[i+1]-Distance[i])  (where [i] = Section Serial no.) "));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TOTAL STRESS  = ∑(AVG STRESS) / {0}", dist[dist.Count - 1]));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            list.Add(string.Format("---------------------------------------"));
            list.Add(string.Format(""));
            #endregion Calculation


            list.Add(string.Format(""));
            //list.Add(string.Format("TABLE 14.7 [D] : STRESS AT CG OF PRESTRESS FORCE"));
            list.Add(string.Format("TABLE 14.11 : STRESS AT C.G. OF CABLES"));
            list.Add(string.Format("------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                  DISTANCE        STRESS      AVG STRESS     TOTAL STRESS"));
            list.Add(string.Format(" Node    Section                   (m)          (KG/cm^2)     (KG/cm^2)      (KG/cm^2)"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,15:f3} {4,13:f3} {5,15:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    dist[i],
                    lst_SCGPF_2[i],
                    lst_AVGS_3[i],
                    lst_AVGS_4[i]
                    ));
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format("-----------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            #endregion TABLE 14.12


            val2 = 0.0;


            List<double> lst_AVGF_1 = new List<double>();
            List<double> lst_Length = new List<double>();

            #region AVERAGE PRESTRESS FORCE AFTER FRICTION AND SLIP

            for (i = 1; i < lst_AVGS_3.Count; i++)
            {
                val = 0.5 * (lst_AVGS_3[i - 1] + lst_AVGS_3[i]) * (dist[i] - dist[i - 1]);
                lst_AVGF_1.Add(val);

                if(val != 0)
                    lst_Length.Add(dist[i] - dist[i - 1]);
                else
                    lst_Length.Add(0.0);

            }
            lst_AVGF_1.Add(0.0);
            lst_Length.Add(0.0);

            List<double> lst_AVGF_2 = new List<double>();
            val1 = MyList.Get_Array_Sum(lst_AVGF_1);
            val2 = MyList.Get_Array_Sum(lst_Length);
            val = val1 / val2;


            for (i = 0; i < lst_AVGF_1.Count; i++)
            {
                if (lst_AVGF_1[i] != 0.0)
                    lst_AVGF_2.Add(val);
                else
                    lst_AVGF_2.Add(0.0);
            }


            #region TABLE 14.12 AVERAGE PRESTRESS FORCE AFTER FRICTION AND SLIP

            list.Add(string.Format("TABLE 14.12 : AVERAGE PRESTRESS FORCE AFTER FRICTION AND SLIP"));
            list.Add(string.Format("-----------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                  AVERAGE    LENGTH      TOTAL  "));
            list.Add(string.Format(" Node    Section                  FORCE                 FORCE  "));
            list.Add(string.Format("                                   (T)        (m)        (T)   "));
            list.Add(string.Format("----------------------------------------------------------------"));
            //list.Add(string.Format("   3     Support 1                 0.000      0.000      0.000"));
            //list.Add(string.Format("   4     Face of Diaphragm         0.500     15.820     12.133"));
            //list.Add(string.Format("   5     1 L1/10                   3.600     27.489     28.074"));
            //list.Add(string.Format("   8     2 L1/10                   7.200     43.698     48.950"));


            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3} {4,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_AVGF_1[i],
                    lst_Length[i],
                    lst_AVGF_2[i]));
            }
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion TABLE 14.12 AVERAGE PRESTRESS FORCE AFTER FRICTION AND SLIP


            #endregion AVERAGE PRESTRESS FORCE AFTER FRICTION AND SLIP

            val2 = 0.0;

            #region ELASTIC SHORTENING LOSS

            double Ecj = 5000 * Math.Sqrt(cnt.Fcu);
            Ecj = Ecj * 10;


            List<double> lst_ESL_Stress = new List<double>();

            for (i = 0; i < lst_AVGF_2.Count; i++)
            {
                if (lst_AVGF_2[i] != 0.0)
                {
                    //0.5*(Es/Ecj)*AVG. STRESS
                    val = 0.5 * (cnt.Es / Ecj) * lst_AVGS_4[i];
                    lst_ESL_Stress.Add(val);
                }
                else
                    lst_ESL_Stress.Add(0.0);
            }


            //cnt
            //    % ES LOSS
            List<double> lst_EL_Loss = new List<double>();
            List<double> lst_G_Ft = new List<double>();
            List<double> lst_G_Fb = new List<double>();

            //=IF(SECONDARY!AE14=0,0,U50/(T50*1000/(SECONDARY!AE14*'cap cab 1'!$C$15*0.01))*100)

            double C15 = cnt.Cap_Cables_Profile[0].Adopt_Cable * cnt.Cap_Cables_Profile[0].Area;
            for (i = 0; i < lst_AVGF_2.Count; i++)
            {
                if (cnt.Table_Secondary.lst_cable_nos[i] == 0)
                    val = 0;
                else
                    val = (lst_ESL_Stress[i] / (lst_AVGF_2[i] * 1000 / (cnt.Table_Secondary.lst_cable_nos[i] * C15 * 0.01)) * 100);
                
                lst_EL_Loss.Add(val);


                val = -1 * lst_EL_Loss[i] * (lst_C_Ft[i] + lst_E_Ft[i]) / 100.0;

                lst_G_Ft.Add(val);

                val = -1 * lst_EL_Loss[i] * (lst_C_Fb[i] + lst_E_Fb[i]) / 100.0;
                lst_G_Fb.Add(val);
            }

            val2 = 0.0;




            //=-1*V50*(W13+K50)/100
            for (i = 0; i < lst_AVGF_2.Count; i++)
            {
                if (cnt.Table_Secondary.lst_cable_nos[i] == 0)
                    val = 0;
                else
                    val = (lst_ESL_Stress[i] / (lst_AVGF_2[i] * 1000 / (cnt.Table_Secondary.lst_cable_nos[i] * C15 * 0.01)) * 100);

                lst_EL_Loss.Add(val);
            }



            #region TABLE 14.13 ELASTIC SHORTENING LOSS

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.13 : ELASTIC SHORTENING LOSS"));
            list.Add(string.Format("-------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                   STRESS    % ES LOSS "));
            list.Add(string.Format(" Node    Section                 (KG/cm^2)           "));
            list.Add(string.Format("----------------------------------------------------------------"));
            //list.Add(string.Format("   3     Support 1                 0.000      0.000      0.000"));
            //list.Add(string.Format("   4     Face of Diaphragm         0.500     15.820     12.133"));
            //list.Add(string.Format("   5     1 L1/10                   3.600     27.489     28.074"));
            //list.Add(string.Format("   8     2 L1/10                   7.200     43.698     48.950"));


            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_ESL_Stress[i],
                    lst_EL_Loss[i] 
                    ));
            }
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format("----------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion TABLE 14.12 AVERAGE PRESTRESS FORCE AFTER FRICTION AND SLIP



            #endregion ELASTIC SHORTENING LOSS


            #region TABLE 14.14 : H = F + G

            List<double> lst_H_Ft = new List<double>();
            List<double> lst_H_Fb = new List<double>();

            for (i = 0; i < lst_F_Ft.Count; i++)
            {
                val1 = lst_F_Ft[i] + lst_G_Ft[i];
                val2 = lst_F_Fb[i] + lst_G_Fb[i];
                lst_H_Ft.Add(val1);
                lst_H_Fb.Add(val2);
            }

            #region TABLE 14.14 [H] = [F] + [G]

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Calculation Formula for TABLE 14.14 [H] "));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format("FT  = FT[F] + FT[G] [Ref. TABLE 14.7, TABLE 14.9 ]"));
            list.Add(string.Format(""));
            list.Add(string.Format("FB  = FB[F] + FB[G] [Ref. TABLE 14.7, TABLE 14.9]"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("TABLE 14.14 [H] = [F] + [G]", table_no++));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("DESIGN   DESIGN                     FT         FB"));
            list.Add(string.Format(" Node    Section                (KG/cm^2)   (KG/cm^2)"));
            list.Add(string.Format("-------------------------------------------------------"));

            format = "{0,4:f3}     {1,-20:f3} {2,10:f3} {3,10:f3}";

            for (i = 0; i < dist.Count; i++)
            {
                list.Add(string.Format(format, des_nodes[i],
                    des_section[i],
                    lst_H_Ft[i],
                    lst_H_Fb[i]));
            }
            list.Add(string.Format("-------------------------------------------------------"));
            list.Add(string.Format("-------------------------------------------------------"));
            #endregion TABLE 14.10

            #endregion H = F + G




            #region SUPERIMPOSED DEAD LOAD CONTINUOUS STAGE

            List<double> lst_I_BM = new List<double>();
            List<double> lst_I_Ft = new List<double>();
            List<double> lst_I_Fb = new List<double>();


            for (i = 0; i < dist.Count; i++)
            {
                val = cnt.Analysis.FRC_SIDL[i].BM_Incr;
                lst_I_BM.Add(val);

                //=AA73*1000*100/(F73*1000000)
                val = lst_I_BM[i] * 1000 * 100 / (lst_Zt[i] * 1000000);
                lst_I_Ft.Add(val);

                //=-AA50*1000*100/(G50*1000000)
                val = -lst_I_BM[i] * 1000 * 100 / (lst_Zb[i] * 1000000);
                lst_I_Fb.Add(val);
            }




            #endregion SUPERIMPOSED DEAD LOAD CONTINUOUS STAGE


            val2 = 0.0;

            #region J = H + I
            List<double> lst_J_Ft = new List<double>();
            List<double> lst_J_Fb = new List<double>();

            for (i = 0; i < lst_F_Ft.Count; i++)
            {
                val1 = lst_H_Ft[i] + lst_I_Ft[i];
                val2 = lst_H_Fb[i] + lst_I_Fb[i];
                lst_J_Ft.Add(val1);
                lst_J_Fb.Add(val2);
            }

            val2 = 0.0;

            #endregion J = H + I

            


            Results = list;
        }

    }

    public class Transverse_Load_Data
    {
        public List<Transverse_Joint_Data> JointLoads { get; set; }
        public List<Transverse_Member_Data> MemberLoads { get; set; }
        public string SELF_WEIGHT { get; set; }
        public Transverse_Load_Data()
        {
            JointLoads = new List<Transverse_Joint_Data>();
            MemberLoads = new List<Transverse_Member_Data>();
            SELF_WEIGHT = "-1";
        }

        public void Set_Data(DataGridView dgv_joint, DataGridView dgv_mems)
        {

            if (dgv_joint.RowCount == 0 || dgv_joint.RowCount == 0) return;

            int i = 0;

            Transverse_Joint_Data TJD = new Transverse_Joint_Data();
            Transverse_Member_Data TMD = new Transverse_Member_Data();
            JointLoads.Clear();
            for (i = 0; i < dgv_joint.RowCount; i++)
            {
                TJD = new Transverse_Joint_Data();
                TJD.Serial_No = i + 1;
                TJD.Loadcase = MyList.StringToInt(dgv_joint[1, i].Value.ToString(), 1);
                TJD.Description = dgv_joint[2, i].Value.ToString();
                TJD.Joints = dgv_joint[3, i].Value.ToString();
                TJD.Direction = dgv_joint[4, i].Value.ToString();
                TJD.Load = dgv_joint[5, i].Value.ToString();

                JointLoads.Add(TJD);
            }

            MemberLoads.Clear();
            for (i = 0; i < dgv_mems.RowCount; i++)
            {
                TMD = new Transverse_Member_Data();
                TMD.Serial_No = i + 1;
                TMD.Loadcase = MyList.StringToInt(dgv_mems[1, i].Value.ToString(), 1);
                TMD.Description = dgv_mems[2, i].Value.ToString();
                TMD.Members = dgv_mems[3, i].Value.ToString();
                TMD.LoadType = dgv_mems[4, i].Value.ToString();
                TMD.Direction = dgv_mems[5, i].Value.ToString();
                if (dgv_mems[6, i].Value != null)
                    TMD.Load = dgv_mems[6, i].Value.ToString();
                if (dgv_mems[7, i].Value != null)
                    TMD.Start_Dist = dgv_mems[7, i].Value.ToString();
                if (dgv_mems[8, i].Value != null)
                    TMD.End_Dist = dgv_mems[8, i].Value.ToString();

                MemberLoads.Add(TMD);
            }
        }
        public List<string> Get_Data(DataGridView dgv_joint, DataGridView dgv_mems)
        {
            List<string> list = new List<string>();
            if (dgv_joint.RowCount == 0 || dgv_joint.RowCount == 0) return list;
            
            Set_Data(dgv_joint, dgv_mems);



            int index = 0;
            //list.Add(string.Format("SELFWEIGHT Y -1.0"));
            list.Add(string.Format("SELFWEIGHT Y {0:f2}", SELF_WEIGHT));
            list.Add(string.Format("LOAD 1 INCIDENTAL LOAD"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("1 TO 24 FY -0.9"));

            //list.Add(string.Format("LOAD 1 SELF WEIGHT"));
            //list.Add(string.Format("SELFWEIGHT Y {0}", SELF_WEIGHT));
            list.Add(string.Format("LOAD 2 SIDL"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("* {0}", JointLoads[index].Description));
            list.Add(string.Format("{0} {1} {2}", JointLoads[index].Joints, JointLoads[index].Direction, JointLoads[index].Load));

            index = 1;
            //list.Add(string.Format("* RAILING KERB"));
            //list.Add(string.Format("22 24 FY -0.48"));
            list.Add(string.Format("* {0}", JointLoads[index].Description));
            list.Add(string.Format("{0} {1} {2}", JointLoads[index].Joints, JointLoads[index].Direction, JointLoads[index].Load));



            index = 2;
            //list.Add(string.Format("* UTILITIES"));
            //list.Add(string.Format("22 FY -0.15"));
            list.Add(string.Format("* {0}", JointLoads[index].Description));
            list.Add(string.Format("{0} {1} {2}", JointLoads[index].Joints, JointLoads[index].Direction, JointLoads[index].Load));




            index = 3;
            //list.Add(string.Format("* WATER SUPPLY PIPE"));
            //list.Add(string.Format("22 FY -0.3"));
            list.Add(string.Format("* {0}", JointLoads[index].Description));
            list.Add(string.Format("{0} {1} {2}", JointLoads[index].Joints, JointLoads[index].Direction, JointLoads[index].Load));




            list.Add(string.Format("MEMBER LOAD"));

            index = 0;

            //list.Add(string.Format("* ROAD KERB"));
            //list.Add(string.Format("22 24 CONC GY -0.162 0.9625"));

            list.Add(string.Format("* {0}", MemberLoads[index].Description));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));




            index = 1;
            //list.Add(string.Format("* FOOTPATH SLAB"));
            //list.Add(string.Format("22 24 UNI GY -0.24 0.85 2.375"));
            list.Add(string.Format("* {0}", MemberLoads[index].Description));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));




            index = 2;
            //list.Add(string.Format("* WEARING COURSE"));
            //list.Add(string.Format("22 24 UNI GY -0.193 0 0.85"));

            list.Add(string.Format("* {0}", MemberLoads[index].Description));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            index = 3;
            //list.Add(string.Format("5 TO 10 21 23 UNI GY -0.193"));

            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            list.Add(string.Format("LOAD 3 FPLL"));
            list.Add(string.Format("MEMBER LOAD"));



            index = 4;
            //list.Add(string.Format("22 UNI GY -0.373 0.85 2.375"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));



            index = 5;
            //list.Add(string.Format("LOAD 4 CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY"));

            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));
            list.Add(string.Format("MEMBER LOAD"));


            //list.Add(string.Format("7 CONC GY -3.86 1.34"));

            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 6;
            //list.Add(string.Format("8 CONC GY -3.86 0.965"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            //list.Add(string.Format("LOAD 5 CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY"));


            index = 7;
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));



            list.Add(string.Format("MEMBER LOAD"));

            //list.Add(string.Format("7 CONC GY -6.17 0.18"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            index = 8;
            //list.Add(string.Format("7 CONC GY -3.53 2.11"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));



            index = 9;
            //list.Add(string.Format("LOAD 6 CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY"));
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));


            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("7 CONC GY -4.25 1.27"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            index = 10;

            //list.Add(string.Format("8 CONC GY -4.25 1.03"));

            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));



            index = 11;
            //list.Add(string.Format("LOAD 7 CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY"));
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));
            list.Add(string.Format("MEMBER LOAD"));



            //list.Add(string.Format("7 CONC GY -5.53 0.17"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            index = 12;
            //list.Add(string.Format("7 CONC GY -3.98 2.23"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 13;
            //list.Add(string.Format("LOAD 8 CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY"));
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("7 CONC GY -2.59 1.4"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 14;
            //list.Add(string.Format("8 CONC GY -2.59 0.9"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));


            index = 15;
            //list.Add(string.Format("LOAD 9 CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY"));
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("22 CONC GY -6.32 0.45"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 16;
            //list.Add(string.Format("7 CONC GY -3.25 0.75"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 17;
            //list.Add(string.Format("LOAD 10 CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY"));
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("5 CONC GY -8.68 0.1"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 18;
            //list.Add(string.Format("7 CONC GY -2.59 1.45"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 19;
            //list.Add(string.Format("8 CONC GY -2.59 0.85"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));

            index = 20;
            //list.Add(string.Format("10 CONC GY -8.68 0.05"));

            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));




            index = 21;
            //list.Add(string.Format("LOAD 11 CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY"));
            list.Add(string.Format("LOAD {0}  {1}", MemberLoads[index].Loadcase, MemberLoads[index].Description));
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("22 CONC GY -6.32 0.45"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));
            index = 22;
            //list.Add(string.Format("7 CONC GY -3.25 0.3"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));
            index = 23;
            //list.Add(string.Format("7 CONC GY -2.59 2"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));
            index = 24;
            //list.Add(string.Format("8 CONC GY -2.59 1.5"));
            list.Add(string.Format("{0} {1} {2} {3} {4} {5}", MemberLoads[index].Members,
                MemberLoads[index].LoadType,
                MemberLoads[index].Direction,
                MemberLoads[index].Load,
                MemberLoads[index].Start_Dist,
                MemberLoads[index].End_Dist));
            //list.Add(string.Format(""));

            #region Default Data
            //LOAD 1 SELF WEIGHT
            //SELFWEIGHT Y -1
            //LOAD 2 SIDL
            //JOINT LOAD
            //* RAILING
            //22 24 FY -0.3
            //* RAILING KERB
            //22 24 FY -0.48
            //* UTILITIES
            //22 FY -0.15
            //* WATER SUPPLY PIPE
            //22 FY -0.3
            //MEMBER LOAD
            //* ROAD KERB
            //22 24 CONC GY -0.162 0.9625
            //* FOOTPATH SLAB
            //22 24 UNI GY -0.24 0.85 2.375
            //* WEARING COURSE
            //22 24 UNI GY -0.193 0 0.85
            //5 TO 10 21 23 UNI GY -0.193
            //LOAD 3 FPLL
            //MEMBER LOAD
            //22 UNI GY -0.373 0.85 2.375
            //LOAD 4 CWLL-40T BOGIE AXLE 1-LANE PLACED SYMMETRICALLY
            //MEMBER LOAD
            //7 CONC GY -3.86 1.34
            //8 CONC GY -3.86 0.965
            //LOAD 5 CWLL-40T BOGIE AXLE 1-LANE PLACED MOST ECCENTRICALLY
            //MEMBER LOAD
            //7 CONC GY -6.17 0.18
            //7 CONC GY -3.53 2.11
            //LOAD 6 CWLL-70'R TRACKED 1-LANE PLACED SYMMETRICALLY
            //MEMBER LOAD
            //7 CONC GY -4.25 1.27
            //8 CONC GY -4.25 1.03
            //LOAD 7 CWLL-70'R TRACKED 1-LANE PLACED MOST ECCENTRICALLY
            //MEMBER LOAD
            //7 CONC GY -5.53 0.17
            //7 CONC GY -3.98 2.23
            //LOAD 8 CWLL-CLASS-A 1-LANE PLACED SYMMETRICALLY
            //MEMBER LOAD
            //7 CONC GY -2.59 1.4
            //8 CONC GY -2.59 0.9
            //LOAD 9 CWLL-CLASS-A 1-LANE PLACED MOST ECCENTRICALLY
            //MEMBER LOAD
            //22 CONC GY -6.32 0.45
            //TRANSVERSE ANALYSIS OF BOX FOR DL, SIDL AND FPLL
            //7 CONC GY -3.25 0.75
            //LOAD 10 CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY
            //MEMBER LOAD
            //5 CONC GY -8.68 0.1
            //7 CONC GY -2.59 1.45
            //8 CONC GY -2.59 0.85
            //10 CONC GY -8.68 0.05
            //LOAD 11 CWLL-CLASS-A 2-LANES PLACED SYMMETRICALLY
            //MEMBER LOAD
            //22 CONC GY -6.32 0.45
            //7 CONC GY -3.25 0.3
            //7 CONC GY -2.59 2
            //8 CONC GY -2.59 1.5
            #endregion Default Data

            return list;
        }

    }
    public class Transverse_Joint_Data
    {
        public int Serial_No { get; set; }
        public int Loadcase { get; set; }
        public string Description { get; set; }
        public string Joints { get; set; }
        public string Direction { get; set; }
        public string Load { get; set; }

        public Transverse_Joint_Data()
        {
            Serial_No = -1;
            Loadcase = -1;
            Description = "";
            Joints = "";
            Direction = "";
            Load = "";
        }

    }
    public class Transverse_Member_Data
    {
        public int Serial_No { get; set; }
        public int Loadcase { get; set; }
        public string Description { get; set; }
        public string Members { get; set; }
        public string LoadType { get; set; }
        public string Direction { get; set; }
        public string Load { get; set; }
        public string Start_Dist { get; set; }
        public string End_Dist { get; set; }

        public Transverse_Member_Data()
        {
            Serial_No = -1;
            Loadcase = -1;
            Description = "";
            Members = "";
            LoadType = "";
            Direction = "";
            Load = "";
            Start_Dist = "";
            End_Dist = "";
        }
    }

}
