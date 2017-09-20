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

    public class Continuous_PSC_BoxGirderAnalysis
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        public JointNode[,] Joints_Array;
        public Member[,] Long_Girder_Members_Array;
        public Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis Bridge_Analysis = null;
        //CompleteDesign complete_design = null;
        public List<LoadData> LoadList = null;
        public List<LoadData> Live_Load_List = null;
        public TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        //Chiranjit [2013 07 23]
        public List<Continuous_Box_Section_List> Mid_Span_Sections { get; set; }
        public List<Continuous_Box_Section_List> End_Span_Sections { get; set; }

        public Transverse_Load_Data Transverse_Data { get; set; }

        //Chiranjit [2013 07 25] Add Dead Loads

        public List<string> LOADS_DL_SIDL_Continous_Span { get; set; }
        public List<string> LOADS_DL_SIDL_Mid_Span { get; set; }
        public List<string> LOADS_DL_SIDL_End_Span { get; set; }
        public List<string> LOADS_FPLL_Continous_Span { get; set; }
        public List<string> LOADS_SINK_Continous_Span { get; set; }

        public List<string> LOADS_Temperature_Section { get; set; }
        public List<string> LOADS_Transverse_Section { get; set; }




        string input_file, working_folder, user_path;
        public Continuous_PSC_BoxGirderAnalysis(IApplication thisApp)
        {
            iApp = thisApp;

            input_file = working_folder = "";

            Mid_Span_Sections = new List<Continuous_Box_Section_List>();
            End_Span_Sections = new List<Continuous_Box_Section_List>();

            LOADS_DL_SIDL_Continous_Span = new List<string>();
            LOADS_DL_SIDL_Mid_Span = new List<string>();
            LOADS_DL_SIDL_End_Span = new List<string>();
            LOADS_FPLL_Continous_Span = new List<string>();
            LOADS_SINK_Continous_Span = new List<string>();
        }

        #region Properties

        public double Length
        {
            get
            {
                return (L1 + L2 + L3);
            }
        }

        public double L1 { get; set; }
        public double L2 { get; set; }
        public double L3 { get; set; }





        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }
        public int Total_Rows
        {
            get
            {
                //return (int)(((WidthBridge - (WidthCantilever)) / Spacing_Long_Girder) + 1);
                return 11;
            }
        }
        public int Total_Columns
        {
            get
            {
                //return (int)(((Length - (Effective_Depth)) / Spacing_Cross_Girder) + 5);
                //return (int)(((Length) / Spacing_Cross_Girder) + 2);
                return 11;
            }
        }
        public double Skew_Angle { get; set; }
        //Chiranjit [2011 10 17]  distance from end to support
        public double Support_Distance { get; set; }

        public double WidthCantilever { get; set; }
        public double Spacing_Long_Girder
        {
            get
            {
                return MyList.StringToDouble(((WidthBridge - (2 * WidthCantilever)) / 6.0).ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //return MyList.StringToDouble(txt_cross_girder_spacing.Text, 0.0);
                return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);
            }
        }


        #endregion Properties

        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(working_folder, "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(working_folder, "ANALYSIS_REP.TXT");
            }
        }
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                try
                {
                    input_file = value;
                    working_folder = Path.GetDirectoryName(input_file);
                    user_path = working_folder;
                }
                catch (Exception ex) { }
            }
        }
        public int NoOfInsideJoints
        {
            get
            {
                //return MyList.StringToInt(txt_cd_total_joints.Text, 0);
                return 1;
            }
        }

        #region File Names

        //public string _70R_1_Train_Lane_Backward
        //public string _70R_1_Train_Lane_Backward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "70R_1_Train_Lane_Backward");
        //        return Path.Combine(f, "70R_1_Train_Lane_Backward.txt");
        //    }
        //}

        ////public string _70R_1_Train_Lane_Forward
        //public string _70R_1_Train_Lane_Forward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "70R_1_Train_Lane_Forward");
        //        return Path.Combine(f, "70R_1_Train_Lane_Forward.txt");
        //    }
        //}

        ////public string _70R_2_Train_Lane_Backward
        //public string _70R_2_Train_Lane_Backward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "70R_2_Train_Lane_Backward");
        //        return Path.Combine(f, "70R_2_Train_Lane_Backward.txt");
        //    }
        //}

        ////public string _70R_2_Train_Lane_Forward
        //public string _70R_2_Train_Lane_Forward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "70R_2_Train_Lane_Forward");
        //        return Path.Combine(f, "70R_2_Train_Lane_Forward.txt");
        //    }
        //}

        ////public string _70R_3_Train_Lane_Backward
        //public string _70R_3_Train_Lane_Backward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "70R_3_Train_Lane_Backward");
        //        return Path.Combine(f, "70R_3_Train_Lane_Backward.txt");
        //    }
        //}

        ////public string _70R_3_Train_Lane_Forward
        //public string _70R_3_Train_Lane_Forward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "70R_3_Train_Lane_Forward");
        //        return Path.Combine(f, "70R_3_Train_Lane_Forward.txt");
        //    }
        //}

        ////public string _Class_A_1_Train_Lane_Backward
        //public string _Class_A_1_Train_Lane_Backward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "Class_A_1_Train_Lane_Backward");
        //        return Path.Combine(f, "Class_A_1_Train_Lane_Backward.txt");
        //    }
        //}

        ////public string _Class_A_1_Train_Lane_Forward
        //public string _Class_A_1_Train_Lane_Forward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "Class_A_1_Train_Lane_Forward");
        //        return Path.Combine(f, "Class_A_1_Train_Lane_Forward.txt");
        //    }
        //}

        ////public string _Class_A_2_Train_Lane_Backward
        //public string _Class_A_2_Train_Lane_Backward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "Class_A_2_Train_Lane_Backward");
        //        return Path.Combine(f, "Class_A_2_Train_Lane_Backward.txt");
        //    }
        //}

        ////public string _Class_A_2_Train_Lane_Forward
        //public string _Class_A_2_Train_Lane_Forward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "Class_A_2_Train_Lane_Forward");
        //        return Path.Combine(f, "Class_A_2_Train_Lane_Forward.txt");
        //    }
        //}

        ////public string _Class_A_3_Train_Lane_Backward
        //public string _Class_A_3_Train_Lane_Backward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "Class_A_3_Train_Lane_Backward");
        //        return Path.Combine(f, "Class_A_3_Train_Lane_Backward.txt");
        //    }
        //}

        ////public string _Class_A_3_Train_Lane_Forward
        //public string _Class_A_3_Train_Lane_Forward
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "Class_A_3_Train_Lane_Forward");
        //        return Path.Combine(f, "Class_A_3_Train_Lane_Forward.txt");
        //    }
        //}

        ////public string _DL_SIDL_Contunuous
        //public string _DL_SIDL_Contunuous
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "DL_SIDL_Contunuous");
        //        return Path.Combine(f, "DL_SIDL_Contunuous.txt");
        //    }
        //}

        ////public string _DL1_Simply_Supported_36m
        //public string _DL1_Simply_Supported_36m
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "DL1_Simply_Supported_36m");
        //        return Path.Combine(f, "DL1_Simply_Supported_36m.txt");
        //    }
        //}


        ////public string _DL2_Simply_Supported_72m
        //public string _DL2_Simply_Supported_72m
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "DL2_Simply_Supported_72m");
        //        return Path.Combine(f, "DL2_Simply_Supported_72m.txt");
        //    }
        //}

        ////public string _FPLL 
        //public string _FPLL
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "FPLL");
        //        return Path.Combine(f, "FPLL.txt");
        //    }
        //}

        ////public string _SINK 
        //public string _SINK
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "SINK");
        //        return Path.Combine(f, "SINK.txt");
        //    }
        //}

        ////public string _SINK1 
        //public string _SINK1
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "SINK1");
        //        return Path.Combine(f, "SINK1.txt");
        //    }
        //}


        ////public string _SINK2 
        //public string _SINK2
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "SINK2");
        //        return Path.Combine(f, "SINK2.txt");
        //    }
        //}

        ////public string _SINK3 
        //public string _SINK3
        //{
        //    get
        //    {
        //        string f = Path.Combine(working_folder, "SINK3");
        //        return Path.Combine(f, "SINK3.txt");
        //    }
        //}


        #endregion File Names



        //Analysis DL+SIDL END SPAN   
        public string Analysis_DL_SIDL_END_SPANS
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_DL_SIDL_END_SPAN");
                return Path.Combine(f, "Analysis_DL_SIDL_END_SPAN.txt");
            }
        }
        public string Analysis_REP_DL_SIDL_END_SPAN
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_DL_SIDL_END_SPANS), "ANALYSIS_REP.TXT");
            }
        }
        public BridgeMemberAnalysis DL_SIDL_END_SPAN { get; set; }
        
        //Analysis DL+SIDL MID SPAN
        public string Analysis_DL_SIDL_MID_SPAN
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_DL_SIDL_MID_SPAN");
                return Path.Combine(f, "Analysis_DL_SIDL_MID_SPAN.txt");
            }
        }

        public string Analysis_REP_DL_SIDL_MID_SPAN
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_DL_SIDL_MID_SPAN), "ANALYSIS_REP.TXT");
            }
        }
        public BridgeMemberAnalysis DL_SIDL_MID_SPAN { get; set; }

        //Analysis DL+SIDL 3 CONTINUOUS SPANS
        public string Analysis_DL_SIDL_3_CONTINUOUS_SPANS
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_DL_SIDL_3_CONTINUOUS_SPANS");
                return Path.Combine(f, "Analysis_DL_SIDL_3_CONTINUOUS_SPANS.TXT");
            }
        }
        public BridgeMemberAnalysis DL_SIDL_3_CONTINUOUS_SPANS { get; set; }

        public string Analysis_REP_DL_SIDL_3_CONTINUOUS_SPANS
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_DL_SIDL_3_CONTINUOUS_SPANS), "ANALYSIS_REP.TXT");
            }
        }
        //Analysis LL 3 CONTINUOUS SPANS
        public string Analysis_LL_3_CONTINUOUS_SPANS
        {
            get
            {
                return Get_Analysis_LiveLoad_Files(12);
                //string f = Path.Combine(working_folder, "Analysis_LL_3_CONTINUOUS_SPANS");
                //return Path.Combine(f, "Analysis_LL_3_CONTINUOUS_SPANS.txt");
            }
        }

        public string Get_Analysis_LiveLoad_Files(int index)
        {
            string title = "";


            #region File Names
            //switch (index)
            //{
            //    case 1:
            //        title = "Analysis_LL_Type3_1_Lane_Forward";
            //        break;
            //    case 2:
            //        title = "Analysis_LL_Type3_1_Lane_Backward";
            //        break;
            //    case 3:
            //        title = "Analysis_LL_Type3_2_Lane_Forward";
            //        break;
            //    case 4:
            //        title = "Analysis_LL_Type3_2_Lane_Backward";
            //        break;
            //    case 5:
            //        title = "Analysis_LL_Type3_3_Lane_Forward";
            //        break;
            //    case 6:
            //        title = "Analysis_LL_Type3_3_Lane_Backward";
            //        break;

            //    case 7:
            //        title = "Analysis_LL_Type1_1_Lane_Forward";
            //        break;
            //    case 8:
            //        title = "Analysis_LL_Type1_1_Lane_Backward";
            //        break;
            //    case 9:
            //        title = "Analysis_LL_Type1_2_Lane_Forward";
            //        break;
            //    case 10:
            //        title = "Analysis_LL_Type1_2_Lane_Backward";
            //        break;
            //    case 11:
            //        title = "Analysis_LL_Type1_3_Lane_Forward";
            //        break;
            //    case 12:
            //        title = "Analysis_LL_Type1_3_Lane_Backward";
            //        break;
            //}
            #endregion File Names



            #region File Names 
            switch (index)
            {
                case 1:
                    title = "Analysis_LL_" + Live_Load_Names[2] + "_1_Lane_Backward";
                    break;
                case 2:
                    title = "Analysis_LL_" + Live_Load_Names[2] + "_1_Lane_Forward";
                    break;
                case 3:
                    title = "Analysis_LL_" + Live_Load_Names[2] + "_2_Lane_Backward";
                    break;
                case 4:
                    title = "Analysis_LL_" + Live_Load_Names[2] + "_2_Lane_Forward";
                    break;
                case 5:
                    title = "Analysis_LL_" + Live_Load_Names[2] + "_3_Lane_Backward";
                    break;
                case 6:
                    title = "Analysis_LL_" + Live_Load_Names[2] + "_3_Lane_Forward";
                    break;

                case 7:
                    title = "Analysis_LL_" + Live_Load_Names[0] + "_1_Lane_Backward";
                    break;
                case 8:
                    title = "Analysis_LL_" + Live_Load_Names[0] + "_1_Lane_Forward";
                    break;
                case 9:
                    title = "Analysis_LL_" + Live_Load_Names[0] + "_2_Lane_Backward";
                    break;
                case 10:
                    title = "Analysis_LL_" + Live_Load_Names[0] + "_2_Lane_Forward";
                    break;
                case 11:
                    title = "Analysis_LL_" + Live_Load_Names[0] + "_3_Lane_Backward";
                    break;
                case 12:
                    title = "Analysis_LL_" + Live_Load_Names[0] + "_3_Lane_Forward";
                    break;
            }
            #endregion File Names

            string f = Path.Combine(working_folder, title);
            return Path.Combine(f, title + ".txt");
        }
        public string Get_Analysis_REP_LiveLoad_Files(int index)
        {
            return MyList.Get_Analysis_Report_File(Get_Analysis_LiveLoad_Files(index));

        }
        public string Analysis_REP_LL_3_CONTINUOUS_SPANS
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_LL_3_CONTINUOUS_SPANS), "ANALYSIS_REP.TXT");
            }
        }
        public List<BridgeMemberAnalysis> LL_All_Analysis { get; set; }


        //Analysis DS 3 CONTINUOUS SPANS
        public string Analysis_SINK_CONTINUOUS_SPANS
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_SINK_CONTINUOUS_SPANS");
                return Path.Combine(f, "Analysis_SINK_CONTINUOUS_SPANS.txt");
            }
        }

        public string Analysis_REP_SINK_CONTINUOUS_SPANS
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_SINK_CONTINUOUS_SPANS), "ANALYSIS_REP.TXT");
            }
        }
        public BridgeMemberAnalysis DS_3_CONTINUOUS_SPANS { get; set; }


        //Analysis FPLL 3 CONTINUOUS SPANS
        public string Analysis_FPLL_3_CONTINUOUS_SPANS
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_FPLL_3_CONTINUOUS_SPANS");
                return Path.Combine(f, "Analysis_FPLL_3_CONTINUOUS_SPANS.txt");
            }
        }

        public string Analysis_REP_FPLL_3_CONTINUOUS_SPANS
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_FPLL_3_CONTINUOUS_SPANS), "ANALYSIS_REP.TXT");
            }
        }
        //Analysis_Temperature_Load_Section
        public string Analysis_Temperature_Load_Section
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_Temperature_Load_Section");
                return Path.Combine(f, "Analysis_Temperature_Load_Section.txt");
            }
        }
        public string Analysis_REP_Temperature_Load_Section
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_Temperature_Load_Section), "ANALYSIS_REP.TXT");
            }
        }


        //Analysis_Transverse_Load_Section
        public string Analysis_Transverse_Load_Section
        {
            get
            {
                string f = Path.Combine(working_folder, "Analysis_Transverse_Load_Section");
                return Path.Combine(f, "Analysis_Transverse_Load_Section.txt");
            }
        }
        public string Analysis_REP_Transverse_Load_Section
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Analysis_Transverse_Load_Section), "ANALYSIS_REP.TXT");
            }
        }


        public BridgeMemberAnalysis FPLL_3_CONTINUOUS_SPANS { get; set; }
        public BridgeMemberAnalysis Temperature_Section { get; set; }
        public BridgeMemberAnalysis Transverse_Section { get; set; }


        public List<string> Live_Load_Names { get; set; }


        public void Create_Folders()
        {

            string fldr = Path.GetDirectoryName(Analysis_DL_SIDL_3_CONTINUOUS_SPANS);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);

            for (int i = 1; i <= 12; i++)
            {
                fldr = Path.GetDirectoryName(Get_Analysis_LiveLoad_Files(i));
                if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);
            }




            fldr = Path.GetDirectoryName(Analysis_DL_SIDL_END_SPANS);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            fldr = Path.GetDirectoryName(Analysis_DL_SIDL_MID_SPAN);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            fldr = Path.GetDirectoryName(Analysis_FPLL_3_CONTINUOUS_SPANS);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            fldr = Path.GetDirectoryName(Analysis_Temperature_Load_Section);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            fldr = Path.GetDirectoryName(Analysis_Transverse_Load_Section);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            fldr = Path.GetDirectoryName(Analysis_SINK_CONTINUOUS_SPANS);
            if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);

            #region  Moving Loads

            //70R 1Train (Lane) Backward

            //70R 1Train (Lane) Forward

            //70R 2 Train (Lane) Backward

            //70R 2 Train (Lane) Forward

            //70R 3 Train (Lane) Backward

            //70R 3 Train (Lane) Forward

            //Class A 1 Train (Lane) Backward

            //Class A 1 Train (Lane) Forward

            //Class A 2 Train (Lane) Backward

            //Class A 2 Train (Lane) Forward


            //Class A 3 Train (Lane) Backward

            //Class A 3 Train (Lane) Forward

            //DL+SIDL Contunuous

            //DL1 Simply Supported 36m


            //DL2 Simply Supported 72m

            //FPLL.anl

            //SINK.anl

            //SINK1.anl

            //SINK2.anl

            //SINK3.anl

            //string fldr = Path.GetDirectoryName(_70R_1_Train_Lane_Backward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_70R_1_Train_Lane_Forward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_70R_2_Train_Lane_Backward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);

            //fldr = Path.GetDirectoryName(_70R_2_Train_Lane_Forward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_70R_3_Train_Lane_Backward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_70R_3_Train_Lane_Forward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_Class_A_1_Train_Lane_Backward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_Class_A_1_Train_Lane_Forward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_Class_A_2_Train_Lane_Backward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_Class_A_2_Train_Lane_Forward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_Class_A_3_Train_Lane_Backward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_Class_A_3_Train_Lane_Forward);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_DL_SIDL_Contunuous);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_DL1_Simply_Supported_36m);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_DL2_Simply_Supported_72m);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_FPLL);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_SINK);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_SINK1);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_SINK2);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);


            //fldr = Path.GetDirectoryName(_SINK3);
            //if (!Directory.Exists(fldr)) Directory.CreateDirectory(fldr);

            #endregion
        }



        public void WriteData_LiveLoad(string file_name, List<string> ll_txt_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR CONTINUOUS BOX GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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

            //list.Add("SECTION PROPERTIES");
            list.AddRange(Get_Continuous_LL_Member_Properties_Data());
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));
           
            list.Add("SUPPORT");
            //list.Add("11 TO 15  116 TO 120 276 TO 280 381 TO 385 FIXED");
            list.Add("7 TO 9  70 TO 72 166 TO 168 229 TO 231 FIXED");

            list.Add("LOAD GENERATION 191");
            list.Add("TYPE 1 -18.8 0 2.75 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 6.25 XINC 0.2");
            list.Add("TYPE 1 -18.8 0 9.75 XINC 0.2");
          
            list.Add("PERFORM ANALYSIS");
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add("FINISH");

            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            string ll = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
            File.WriteAllLines(ll, ll_txt_data.ToArray());
        }


        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList = new List<LoadData>();
            //LoadList.Clear();
            MyList mlist = null;
            for (i = 0; i < dgv_live_load.RowCount; i++)
            {
                try
                {
                    ld = new LoadData();
                    mlist = new MyList(MyList.RemoveAllSpaces(dgv_live_load[0, i].Value.ToString().ToUpper()), ':');
                    ld.TypeNo = mlist.StringList[0];
                    ld.Code = mlist.StringList[1];

                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -60.0);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 0.0);

                    for (int j = 0; j < Live_Load_List.Count; j++)
                    {
                        if (Live_Load_List[j].TypeNo == ld.TypeNo)
                        {
                            ld.LoadWidth = Live_Load_List[j].LoadWidth;
                            break;
                        }
                    }
                    if ((ld.Z + ld.LoadWidth) > WidthBridge)
                    {
                        throw new Exception("Width of Bridge Deck is insufficient to accommodate \ngiven numbers of Lanes of Vehicle Load. \n\nBridge Width = " + WidthBridge + " <  Load Width (" + ld.Z + " + " + ld.LoadWidth + ") = " + (ld.Z + ld.LoadWidth));
                    }
                    else
                    {
                        ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                        ld.ImpactFactor = MyList.StringToDouble(dgv_live_load[5, i].Value.ToString(), 0.5);
                        LoadList.Add(ld);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        List<string> Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Bridge_Analysis.Analysis.Members);

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


            z_min = Bridge_Analysis.Analysis.Joints.MinZ;
            double z_max = Bridge_Analysis.Analysis.Joints.MaxZ;


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

        public void Create_ALL_Data(List<string> ll_txt_data)
        {

            Create_Continuous_Data(Analysis_DL_SIDL_3_CONTINUOUS_SPANS, LOADS_DL_SIDL_Continous_Span.ToArray());

            Create_End_Span_Data(Analysis_DL_SIDL_END_SPANS);
            Create_Mid_Span_Data(Analysis_DL_SIDL_MID_SPAN);

            //Create_Continuous_Data(Analysis_LL_3_CONTINUOUS_SPANS);
            Create_Continuous_Data(Analysis_FPLL_3_CONTINUOUS_SPANS, LOADS_FPLL_Continous_Span.ToArray());
            //Create_Continuous_Data(Analysis_DS_3_CONTINUOUS_SPANS, LOADS_SINK_Continous_Span.ToArray());
            Create_Sink_Continuous_Data(Analysis_SINK_CONTINUOUS_SPANS, LOADS_SINK_Continous_Span.ToArray());
            //Chiranjit [2013 12 16]
            Create_Temparature_Data(Analysis_Temperature_Load_Section, LOADS_Temperature_Section.ToArray());
            Create_Temparature_Data(Analysis_Transverse_Load_Section, LOADS_Transverse_Section.ToArray());

            Create_LiveLoad_Data();
            for (int i = 1; i <= 12; i++)
            {
                WriteData_LiveLoad(Get_Analysis_LiveLoad_Files(i), ll_txt_data);
            }


        }
        public void Create_Continuous_Data(string file_name)
        {

            List<string> list = new List<string>();


            List<double> lst_factor = new List<double>();

            #region Left Span

            lst_factor.Add(0);
            lst_factor.Add(0.013793103);
            lst_factor.Add(0.027586207);
            lst_factor.Add(0.04137931);
            lst_factor.Add(0.126896552);
            lst_factor.Add(0.137931034);
            lst_factor.Add(0.220689655);
            lst_factor.Add(0.226206897);
            lst_factor.Add(0.303448276);
            lst_factor.Add(0.325517241);
            lst_factor.Add(0.386206897);
            lst_factor.Add(0.424827586);
            lst_factor.Add(0.524137931);
            lst_factor.Add(0.623448276);
            lst_factor.Add(0.662068966);
            lst_factor.Add(0.722758621);
            lst_factor.Add(0.744827586);
            lst_factor.Add(0.822068966);
            lst_factor.Add(0.827586207);
            lst_factor.Add(0.910344828);
            lst_factor.Add(0.92137931);
            lst_factor.Add(0.993103448);
            lst_factor.Add(1);

            #endregion Left Span

            #region Mid Span

            List<double> lst_mid_factor = new List<double>();



            lst_mid_factor.Add(0.010204082);
            lst_mid_factor.Add(0.020408163);
            lst_mid_factor.Add(0.023809524);
            lst_mid_factor.Add(0.059183673);
            lst_mid_factor.Add(0.081632653);
            lst_mid_factor.Add(0.108163265);
            lst_mid_factor.Add(0.157142857);
            lst_mid_factor.Add(0.163265306);
            lst_mid_factor.Add(0.206122449);
            lst_mid_factor.Add(0.244897959);
            lst_mid_factor.Add(0.255102041);
            lst_mid_factor.Add(0.299319728);
            lst_mid_factor.Add(0.304081633);
            lst_mid_factor.Add(0.353061224);
            lst_mid_factor.Add(0.402040816);
            lst_mid_factor.Add(0.451020408);
            lst_mid_factor.Add(0.5);
            lst_mid_factor.Add(0.548979592);
            lst_mid_factor.Add(0.597959184);
            lst_mid_factor.Add(0.646938776);
            lst_mid_factor.Add(0.695918367);
            lst_mid_factor.Add(0.700680272);
            lst_mid_factor.Add(0.744897959);
            lst_mid_factor.Add(0.755102041);
            lst_mid_factor.Add(0.793877551);
            lst_mid_factor.Add(0.836734694);
            lst_mid_factor.Add(0.842857143);
            lst_mid_factor.Add(0.891836735);
            lst_mid_factor.Add(0.918367347);
            lst_mid_factor.Add(0.940816327);
            lst_mid_factor.Add(0.976190476);
            lst_mid_factor.Add(0.979591837);
            lst_mid_factor.Add(0.989795918);
            lst_mid_factor.Add(1);

            #endregion Mid Span

            #region Right Span

            List<double> lst_right_factor = new List<double>();


            lst_right_factor.Add(0.006896552);
            lst_right_factor.Add(0.07862069);
            lst_right_factor.Add(0.089655172);
            lst_right_factor.Add(0.172413793);
            lst_right_factor.Add(0.177931034);
            lst_right_factor.Add(0.255172414);
            lst_right_factor.Add(0.277241379);
            lst_right_factor.Add(0.337931034);
            lst_right_factor.Add(0.376551724);
            lst_right_factor.Add(0.475862069);
            lst_right_factor.Add(0.575172414);
            lst_right_factor.Add(0.613793103);
            lst_right_factor.Add(0.674482759);
            lst_right_factor.Add(0.696551724);
            lst_right_factor.Add(0.773793103);
            lst_right_factor.Add(0.779310345);
            lst_right_factor.Add(0.862068966);
            lst_right_factor.Add(0.873103448);
            lst_right_factor.Add(0.95862069);
            lst_right_factor.Add(0.972413793);
            lst_right_factor.Add(0.986206897);
            lst_right_factor.Add(1);
            #endregion Right Span

            List<double> lst_x_coords = new List<double>();

            for (int i = 0; i < lst_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            }

            for (int i = 0; i < lst_mid_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0)) * lst_mid_factor[i]);
            }
            for (int i = 0; i < lst_right_factor.Count; i++)
            {
                lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) + (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            }

            list.Add(string.Format("ASTRA FLOOR CONTINUOUS PSC BOX GIRDER BRIDGE ANALYSIS"));
            list.Add(string.Format("UNIT   MTON   MET"));
            list.Add(string.Format("JOINT   COORDINATES"));


            for (int i = 0; i < lst_x_coords.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-7:f2} {2,-7:f3} {3,-7:f3}", (i + 1), lst_x_coords[i], 0, 0));
            }


            #region Default Data
            //list.Add(string.Format("1   0   0   0"));
            //list.Add(string.Format("2   0.5   0   0"));
            //list.Add(string.Format("3   1   0   0"));
            //list.Add(string.Format("4   1.5   0   0"));
            //list.Add(string.Format("5   4.6   0   0"));
            //list.Add(string.Format("6   5   0   0"));
            //list.Add(string.Format("7   8   0   0"));
            //list.Add(string.Format("8   8.2   0   0"));
            //list.Add(string.Format("9   11   0   0"));
            //list.Add(string.Format("10   11.8   0   0"));
            //list.Add(string.Format("11   14   0   0"));
            //list.Add(string.Format("12   15.4   0   0"));
            //list.Add(string.Format("13   19   0   0"));
            //list.Add(string.Format("14   22.6   0   0"));
            //list.Add(string.Format("15   24   0   0"));
            //list.Add(string.Format("16   26.2   0   0"));
            //list.Add(string.Format("17   27   0   0"));
            //list.Add(string.Format("18   29.8   0   0"));
            //list.Add(string.Format("19   30   0   0"));
            //list.Add(string.Format("20   33   0   0"));
            //list.Add(string.Format("21   33.4   0   0"));
            //list.Add(string.Format("22   36   0   0"));
            //list.Add(string.Format("23   36.25   0   0"));
            //list.Add(string.Format("24   37   0   0"));
            //list.Add(string.Format("25   37.75   0   0"));
            //list.Add(string.Format("26   38   0   0"));
            //list.Add(string.Format("27   40.6   0   0"));
            //list.Add(string.Format("28   42.25   0   0"));
            //list.Add(string.Format("29   44.2   0   0"));
            //list.Add(string.Format("30   47.8   0   0"));
            //list.Add(string.Format("31   48.25   0   0"));
            //list.Add(string.Format("32   51.4   0   0"));
            //list.Add(string.Format("33   54.25   0   0"));
            //list.Add(string.Format("34   55   0   0"));
            //list.Add(string.Format("35   58.25   0   0"));
            //list.Add(string.Format("36   58.6   0   0"));
            //list.Add(string.Format("37   62.2   0   0"));
            //list.Add(string.Format("38   65.8   0   0"));
            //list.Add(string.Format("39   69.4   0   0"));
            //list.Add(string.Format("40   73   0   0"));
            //list.Add(string.Format("41   76.6   0   0"));
            //list.Add(string.Format("42   80.2   0   0"));
            //list.Add(string.Format("43   83.8   0   0"));
            //list.Add(string.Format("44   87.4   0   0"));
            //list.Add(string.Format("45   87.75   0   0"));
            //list.Add(string.Format("46   91   0   0"));
            //list.Add(string.Format("47   91.75   0   0"));
            //list.Add(string.Format("48   94.6   0   0"));
            //list.Add(string.Format("49   97.75   0   0"));
            //list.Add(string.Format("50   98.2   0   0"));
            //list.Add(string.Format("51   101.8   0   0"));
            //list.Add(string.Format("52   103.75   0   0"));
            //list.Add(string.Format("53   105.4   0   0"));
            //list.Add(string.Format("54   108   0   0"));
            //list.Add(string.Format("55   108.25   0   0"));
            //list.Add(string.Format("56   109   0   0"));
            //list.Add(string.Format("57   109.75   0   0"));
            //list.Add(string.Format("58   110   0   0"));
            //list.Add(string.Format("59   112.6   0   0"));
            //list.Add(string.Format("60   113   0   0"));
            //list.Add(string.Format("61   116   0   0"));
            //list.Add(string.Format("62   116.2   0   0"));
            //list.Add(string.Format("63   119   0   0"));
            //list.Add(string.Format("64   119.8   0   0"));
            //list.Add(string.Format("65   122   0   0"));
            //list.Add(string.Format("66   123.4   0   0"));
            //list.Add(string.Format("67   127   0   0"));
            //list.Add(string.Format("68   130.6   0   0"));
            //list.Add(string.Format("69   132   0   0"));
            //list.Add(string.Format("70   134.2   0   0"));
            //list.Add(string.Format("71   135   0   0"));
            //list.Add(string.Format("72   137.8   0   0"));
            //list.Add(string.Format("73   138   0   0"));
            //list.Add(string.Format("74   141   0   0"));
            //list.Add(string.Format("75   141.4   0   0"));
            //list.Add(string.Format("76   144.5   0   0"));
            //list.Add(string.Format("77   145   0   0"));
            //list.Add(string.Format("78   145.5   0   0"));
            //list.Add(string.Format("79   146   0   0"));
            #endregion Default Data



            list.Add(string.Format("MEMBER   INCIDENCE"));
            list.Add(string.Format("1   1   2"));
            list.Add(string.Format("2   2   3"));
            list.Add(string.Format("3   3   4"));
            list.Add(string.Format("4   4   5"));
            list.Add(string.Format("5   5   6"));
            list.Add(string.Format("6   6   7"));
            list.Add(string.Format("7   7   8"));
            list.Add(string.Format("8   8   9"));
            list.Add(string.Format("9   9   10"));
            list.Add(string.Format("10   10   11"));
            list.Add(string.Format("11   11   12"));
            list.Add(string.Format("12   12   13"));
            list.Add(string.Format("13   13   14"));
            list.Add(string.Format("14   14   15"));
            list.Add(string.Format("15   15   16"));
            list.Add(string.Format("16   16   17"));
            list.Add(string.Format("17   17   18"));
            list.Add(string.Format("18   18   19"));
            list.Add(string.Format("19   19   20"));
            list.Add(string.Format("20   20   21"));
            list.Add(string.Format("21   21   22"));
            list.Add(string.Format("22   22   23"));
            list.Add(string.Format("23   23   24"));
            list.Add(string.Format("24   24   25"));
            list.Add(string.Format("25   25   26"));
            list.Add(string.Format("26   26   27"));
            list.Add(string.Format("27   27   28"));
            list.Add(string.Format("28   28   29"));
            list.Add(string.Format("29   29   30"));
            list.Add(string.Format("30   30   31"));
            list.Add(string.Format("31   31   32"));
            list.Add(string.Format("32   32   33"));
            list.Add(string.Format("33   33   34"));
            list.Add(string.Format("34   34   35"));
            list.Add(string.Format("35   35   36"));
            list.Add(string.Format("36   36   37"));
            list.Add(string.Format("37   37   38"));
            list.Add(string.Format("38   38   39"));
            list.Add(string.Format("39   39   40"));
            list.Add(string.Format("40   40   41"));
            list.Add(string.Format("41   41   42"));
            list.Add(string.Format("42   42   43"));
            list.Add(string.Format("43   43   44"));
            list.Add(string.Format("44   44   45"));
            list.Add(string.Format("45   45   46"));
            list.Add(string.Format("46   46   47"));
            list.Add(string.Format("47   47   48"));
            list.Add(string.Format("48   48   49"));
            list.Add(string.Format("49   49   50"));
            list.Add(string.Format("50   50   51"));
            list.Add(string.Format("51   51   52"));
            list.Add(string.Format("52   52   53"));
            list.Add(string.Format("53   53   54"));
            list.Add(string.Format("54   54   55"));
            list.Add(string.Format("55   55   56"));
            list.Add(string.Format("56   56   57"));
            list.Add(string.Format("57   57   58"));
            list.Add(string.Format("58   58   59"));
            list.Add(string.Format("59   59   60"));
            list.Add(string.Format("60   60   61"));
            list.Add(string.Format("61   61   62"));
            list.Add(string.Format("62   62   63"));
            list.Add(string.Format("63   63   64"));
            list.Add(string.Format("64   64   65"));
            list.Add(string.Format("65   65   66"));
            list.Add(string.Format("66   66   67"));
            list.Add(string.Format("67   67   68"));
            list.Add(string.Format("68   68   69"));
            list.Add(string.Format("69   69   70"));
            list.Add(string.Format("70   70   71"));
            list.Add(string.Format("71   71   72"));
            list.Add(string.Format("72   72   73"));
            list.Add(string.Format("73   73   74"));
            list.Add(string.Format("74   74   75"));
            list.Add(string.Format("75   75   76"));
            list.Add(string.Format("76   76   77"));
            list.Add(string.Format("77   77   78"));
            list.Add(string.Format("78   78   79"));

            list.AddRange(Get_Continuous_Member_Properties_Data().ToArray());

            //list.Add(string.Format("MEMBER   PROPERTY"));
            //list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));


            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("3   FIXED   BUT   MZ"));
            list.Add(string.Format("24   56   77   FIXED   BUT   FX   MZ   "));
            //list.Add(string.Format("LOAD    1    DL + SIDL  + LL "));
            list.Add(string.Format("LOAD   1  DL + SIDL"));
            list.Add(string.Format("MEMBER LOAD"));
            list.Add(string.Format("1 TO 35 UNI GY -1.0"));
            list.Add(string.Format("48 TO 59 UNI GY -5.5"));
            list.Add(string.Format("36 TO 47 60 TO 71 UNI GY -5.5"));

            //list.Add(string.Format("DEFINE   MOVING   LOAD LL.TXT"));
            //list.Add(string.Format("TYPE   1   LD   8   2*12   4*17   DIS   3.96   1.52   2.13   1.37   3.05   1.37"));
            //list.Add(string.Format("LOAD   GENERATION   161"));
            //list.Add(string.Format("TYPE   1   -13.4   0   0   XINC   1   "));
            list.Add(string.Format("PERFORM ANALYSIS"));
            //list.Add(string.Format("*   MAXIMUM   REACTION   AT   SUPPORT   1"));
            //list.Add(string.Format("LOAD   LIST   11"));
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            //File.WriteAllLines(file_name, list.ToArray(), true);
            File.WriteAllLines(file_name, list.ToArray());

        }
        public void Create_Continuous_Data(string file_name, string[] loads)
        {

            List<string> list = new List<string>();


            List<double> lst_factor = new List<double>();

            #region Left Span

            lst_factor.Add(0);
            lst_factor.Add(0.013793103);
            lst_factor.Add(0.027586207);
            lst_factor.Add(0.04137931);
            lst_factor.Add(0.126896552);
            lst_factor.Add(0.137931034);
            lst_factor.Add(0.220689655);
            lst_factor.Add(0.226206897);
            lst_factor.Add(0.303448276);
            lst_factor.Add(0.325517241);
            lst_factor.Add(0.386206897);
            lst_factor.Add(0.424827586);
            lst_factor.Add(0.524137931);
            lst_factor.Add(0.623448276);
            lst_factor.Add(0.662068966);
            lst_factor.Add(0.722758621);
            lst_factor.Add(0.744827586);
            lst_factor.Add(0.822068966);
            lst_factor.Add(0.827586207);
            lst_factor.Add(0.910344828);
            lst_factor.Add(0.92137931);
            lst_factor.Add(0.993103448);
            lst_factor.Add(1);

            #endregion Left Span

            #region Mid Span

            List<double> lst_mid_factor = new List<double>();



            lst_mid_factor.Add(0.010204082);
            lst_mid_factor.Add(0.020408163);
            lst_mid_factor.Add(0.023809524);
            lst_mid_factor.Add(0.059183673);
            lst_mid_factor.Add(0.081632653);
            lst_mid_factor.Add(0.108163265);
            lst_mid_factor.Add(0.157142857);
            lst_mid_factor.Add(0.163265306);
            lst_mid_factor.Add(0.206122449);
            lst_mid_factor.Add(0.244897959);
            lst_mid_factor.Add(0.255102041);
            lst_mid_factor.Add(0.299319728);
            lst_mid_factor.Add(0.304081633);
            lst_mid_factor.Add(0.353061224);
            lst_mid_factor.Add(0.402040816);
            lst_mid_factor.Add(0.451020408);
            lst_mid_factor.Add(0.5);
            lst_mid_factor.Add(0.548979592);
            lst_mid_factor.Add(0.597959184);
            lst_mid_factor.Add(0.646938776);
            lst_mid_factor.Add(0.695918367);
            lst_mid_factor.Add(0.700680272);
            lst_mid_factor.Add(0.744897959);
            lst_mid_factor.Add(0.755102041);
            lst_mid_factor.Add(0.793877551);
            lst_mid_factor.Add(0.836734694);
            lst_mid_factor.Add(0.842857143);
            lst_mid_factor.Add(0.891836735);
            lst_mid_factor.Add(0.918367347);
            lst_mid_factor.Add(0.940816327);
            lst_mid_factor.Add(0.976190476);
            lst_mid_factor.Add(0.979591837);
            lst_mid_factor.Add(0.989795918);
            lst_mid_factor.Add(1);

            #endregion Mid Span

            #region Right Span

            List<double> lst_right_factor = new List<double>();


            lst_right_factor.Add(0.006896552);
            lst_right_factor.Add(0.07862069);
            lst_right_factor.Add(0.089655172);
            lst_right_factor.Add(0.172413793);
            lst_right_factor.Add(0.177931034);
            lst_right_factor.Add(0.255172414);
            lst_right_factor.Add(0.277241379);
            lst_right_factor.Add(0.337931034);
            lst_right_factor.Add(0.376551724);
            lst_right_factor.Add(0.475862069);
            lst_right_factor.Add(0.575172414);
            lst_right_factor.Add(0.613793103);
            lst_right_factor.Add(0.674482759);
            lst_right_factor.Add(0.696551724);
            lst_right_factor.Add(0.773793103);
            lst_right_factor.Add(0.779310345);
            lst_right_factor.Add(0.862068966);
            lst_right_factor.Add(0.873103448);
            lst_right_factor.Add(0.95862069);
            lst_right_factor.Add(0.972413793);
            lst_right_factor.Add(0.986206897);
            lst_right_factor.Add(1);
            #endregion Right Span

            List<double> lst_x_coords = new List<double>();



            for (int i = 0; i < lst_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            }

            for (int i = 0; i < lst_mid_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0)) * lst_mid_factor[i]);
            }
            for (int i = 0; i < lst_right_factor.Count; i++)
            {
                lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) + (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            }

            list.Add(string.Format("ASTRA FLOOR CONTINUOUS PSC BOX GIRDER BRIDGE ANALYSIS"));
            list.Add(string.Format("UNIT   MTON   MET"));
            list.Add(string.Format("JOINT   COORDINATES"));


            for (int i = 0; i < lst_x_coords.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-7:f2} {2,-7:f3} {3,-7:f3}", (i + 1), lst_x_coords[i], 0, 0));
            }


            #region Default Data
            //list.Add(string.Format("1   0   0   0"));
            //list.Add(string.Format("2   0.5   0   0"));
            //list.Add(string.Format("3   1   0   0"));
            //list.Add(string.Format("4   1.5   0   0"));
            //list.Add(string.Format("5   4.6   0   0"));
            //list.Add(string.Format("6   5   0   0"));
            //list.Add(string.Format("7   8   0   0"));
            //list.Add(string.Format("8   8.2   0   0"));
            //list.Add(string.Format("9   11   0   0"));
            //list.Add(string.Format("10   11.8   0   0"));
            //list.Add(string.Format("11   14   0   0"));
            //list.Add(string.Format("12   15.4   0   0"));
            //list.Add(string.Format("13   19   0   0"));
            //list.Add(string.Format("14   22.6   0   0"));
            //list.Add(string.Format("15   24   0   0"));
            //list.Add(string.Format("16   26.2   0   0"));
            //list.Add(string.Format("17   27   0   0"));
            //list.Add(string.Format("18   29.8   0   0"));
            //list.Add(string.Format("19   30   0   0"));
            //list.Add(string.Format("20   33   0   0"));
            //list.Add(string.Format("21   33.4   0   0"));
            //list.Add(string.Format("22   36   0   0"));
            //list.Add(string.Format("23   36.25   0   0"));
            //list.Add(string.Format("24   37   0   0"));
            //list.Add(string.Format("25   37.75   0   0"));
            //list.Add(string.Format("26   38   0   0"));
            //list.Add(string.Format("27   40.6   0   0"));
            //list.Add(string.Format("28   42.25   0   0"));
            //list.Add(string.Format("29   44.2   0   0"));
            //list.Add(string.Format("30   47.8   0   0"));
            //list.Add(string.Format("31   48.25   0   0"));
            //list.Add(string.Format("32   51.4   0   0"));
            //list.Add(string.Format("33   54.25   0   0"));
            //list.Add(string.Format("34   55   0   0"));
            //list.Add(string.Format("35   58.25   0   0"));
            //list.Add(string.Format("36   58.6   0   0"));
            //list.Add(string.Format("37   62.2   0   0"));
            //list.Add(string.Format("38   65.8   0   0"));
            //list.Add(string.Format("39   69.4   0   0"));
            //list.Add(string.Format("40   73   0   0"));
            //list.Add(string.Format("41   76.6   0   0"));
            //list.Add(string.Format("42   80.2   0   0"));
            //list.Add(string.Format("43   83.8   0   0"));
            //list.Add(string.Format("44   87.4   0   0"));
            //list.Add(string.Format("45   87.75   0   0"));
            //list.Add(string.Format("46   91   0   0"));
            //list.Add(string.Format("47   91.75   0   0"));
            //list.Add(string.Format("48   94.6   0   0"));
            //list.Add(string.Format("49   97.75   0   0"));
            //list.Add(string.Format("50   98.2   0   0"));
            //list.Add(string.Format("51   101.8   0   0"));
            //list.Add(string.Format("52   103.75   0   0"));
            //list.Add(string.Format("53   105.4   0   0"));
            //list.Add(string.Format("54   108   0   0"));
            //list.Add(string.Format("55   108.25   0   0"));
            //list.Add(string.Format("56   109   0   0"));
            //list.Add(string.Format("57   109.75   0   0"));
            //list.Add(string.Format("58   110   0   0"));
            //list.Add(string.Format("59   112.6   0   0"));
            //list.Add(string.Format("60   113   0   0"));
            //list.Add(string.Format("61   116   0   0"));
            //list.Add(string.Format("62   116.2   0   0"));
            //list.Add(string.Format("63   119   0   0"));
            //list.Add(string.Format("64   119.8   0   0"));
            //list.Add(string.Format("65   122   0   0"));
            //list.Add(string.Format("66   123.4   0   0"));
            //list.Add(string.Format("67   127   0   0"));
            //list.Add(string.Format("68   130.6   0   0"));
            //list.Add(string.Format("69   132   0   0"));
            //list.Add(string.Format("70   134.2   0   0"));
            //list.Add(string.Format("71   135   0   0"));
            //list.Add(string.Format("72   137.8   0   0"));
            //list.Add(string.Format("73   138   0   0"));
            //list.Add(string.Format("74   141   0   0"));
            //list.Add(string.Format("75   141.4   0   0"));
            //list.Add(string.Format("76   144.5   0   0"));
            //list.Add(string.Format("77   145   0   0"));
            //list.Add(string.Format("78   145.5   0   0"));
            //list.Add(string.Format("79   146   0   0"));
            #endregion Default Data


            #region Member Incidence
            list.Add(string.Format("MEMBER   INCIDENCE"));
            list.Add(string.Format("1   1   2"));
            list.Add(string.Format("2   2   3"));
            list.Add(string.Format("3   3   4"));
            list.Add(string.Format("4   4   5"));
            list.Add(string.Format("5   5   6"));
            list.Add(string.Format("6   6   7"));
            list.Add(string.Format("7   7   8"));
            list.Add(string.Format("8   8   9"));
            list.Add(string.Format("9   9   10"));
            list.Add(string.Format("10   10   11"));
            list.Add(string.Format("11   11   12"));
            list.Add(string.Format("12   12   13"));
            list.Add(string.Format("13   13   14"));
            list.Add(string.Format("14   14   15"));
            list.Add(string.Format("15   15   16"));
            list.Add(string.Format("16   16   17"));
            list.Add(string.Format("17   17   18"));
            list.Add(string.Format("18   18   19"));
            list.Add(string.Format("19   19   20"));
            list.Add(string.Format("20   20   21"));
            list.Add(string.Format("21   21   22"));
            list.Add(string.Format("22   22   23"));
            list.Add(string.Format("23   23   24"));
            list.Add(string.Format("24   24   25"));
            list.Add(string.Format("25   25   26"));
            list.Add(string.Format("26   26   27"));
            list.Add(string.Format("27   27   28"));
            list.Add(string.Format("28   28   29"));
            list.Add(string.Format("29   29   30"));
            list.Add(string.Format("30   30   31"));
            list.Add(string.Format("31   31   32"));
            list.Add(string.Format("32   32   33"));
            list.Add(string.Format("33   33   34"));
            list.Add(string.Format("34   34   35"));
            list.Add(string.Format("35   35   36"));
            list.Add(string.Format("36   36   37"));
            list.Add(string.Format("37   37   38"));
            list.Add(string.Format("38   38   39"));
            list.Add(string.Format("39   39   40"));
            list.Add(string.Format("40   40   41"));
            list.Add(string.Format("41   41   42"));
            list.Add(string.Format("42   42   43"));
            list.Add(string.Format("43   43   44"));
            list.Add(string.Format("44   44   45"));
            list.Add(string.Format("45   45   46"));
            list.Add(string.Format("46   46   47"));
            list.Add(string.Format("47   47   48"));
            list.Add(string.Format("48   48   49"));
            list.Add(string.Format("49   49   50"));
            list.Add(string.Format("50   50   51"));
            list.Add(string.Format("51   51   52"));
            list.Add(string.Format("52   52   53"));
            list.Add(string.Format("53   53   54"));
            list.Add(string.Format("54   54   55"));
            list.Add(string.Format("55   55   56"));
            list.Add(string.Format("56   56   57"));
            list.Add(string.Format("57   57   58"));
            list.Add(string.Format("58   58   59"));
            list.Add(string.Format("59   59   60"));
            list.Add(string.Format("60   60   61"));
            list.Add(string.Format("61   61   62"));
            list.Add(string.Format("62   62   63"));
            list.Add(string.Format("63   63   64"));
            list.Add(string.Format("64   64   65"));
            list.Add(string.Format("65   65   66"));
            list.Add(string.Format("66   66   67"));
            list.Add(string.Format("67   67   68"));
            list.Add(string.Format("68   68   69"));
            list.Add(string.Format("69   69   70"));
            list.Add(string.Format("70   70   71"));
            list.Add(string.Format("71   71   72"));
            list.Add(string.Format("72   72   73"));
            list.Add(string.Format("73   73   74"));
            list.Add(string.Format("74   74   75"));
            list.Add(string.Format("75   75   76"));
            list.Add(string.Format("76   76   77"));
            list.Add(string.Format("77   77   78"));
            list.Add(string.Format("78   78   79"));
            #endregion Member Incidence

            list.AddRange(Get_Continuous_Member_Properties_Data().ToArray());

            //list.Add(string.Format("MEMBER   PROPERTY"));
            //list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));


            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("3   FIXED   BUT   MZ"));
            //list.Add(string.Format("24   56   77   FIXED   BUT   FX   MZ   "));
            if(file_name == Analysis_SINK_CONTINUOUS_SPANS)
            {
                list.Add(string.Format("24 56 77 FIXED BUT FY MX MY MZ"));
            }
            else
                list.Add(string.Format("24   56   77   FIXED   BUT   FX   MZ"));

            //list.Add(string.Format("LOAD    1    DL + SIDL  + LL "));
            //list.Add(string.Format("LOAD   1  DL + SIDL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 35 UNI GY -1.0"));
            //list.Add(string.Format("48 TO 59 UNI GY -5.5"));
            //list.Add(string.Format("36 TO 47 60 TO 71 UNI GY -5.5"));
            list.AddRange(loads);
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            //File.WriteAllLines(file_name, list.ToArray(), true);
            File.WriteAllLines(file_name, list.ToArray());

        }
        public void Create_Sink_Continuous_Data(string file_name, string[] loads)
        {
            List<string> list = new List<string>();


            List<double> lst_factor = new List<double>();

            #region Left Span

            lst_factor.Add(0);
            lst_factor.Add(0.013793103);
            lst_factor.Add(0.027586207);
            lst_factor.Add(0.04137931);
            lst_factor.Add(0.126896552);
            lst_factor.Add(0.137931034);
            lst_factor.Add(0.220689655);
            lst_factor.Add(0.226206897);
            lst_factor.Add(0.303448276);
            lst_factor.Add(0.325517241);
            lst_factor.Add(0.386206897);
            lst_factor.Add(0.424827586);
            lst_factor.Add(0.524137931);
            lst_factor.Add(0.623448276);
            lst_factor.Add(0.662068966);
            lst_factor.Add(0.722758621);
            lst_factor.Add(0.744827586);
            lst_factor.Add(0.822068966);
            lst_factor.Add(0.827586207);
            lst_factor.Add(0.910344828);
            lst_factor.Add(0.92137931);
            lst_factor.Add(0.993103448);
            lst_factor.Add(1);

            #endregion Left Span

            #region Mid Span

            List<double> lst_mid_factor = new List<double>();



            lst_mid_factor.Add(0.010204082);
            lst_mid_factor.Add(0.020408163);
            lst_mid_factor.Add(0.023809524);
            lst_mid_factor.Add(0.059183673);
            lst_mid_factor.Add(0.081632653);
            lst_mid_factor.Add(0.108163265);
            lst_mid_factor.Add(0.157142857);
            lst_mid_factor.Add(0.163265306);
            lst_mid_factor.Add(0.206122449);
            lst_mid_factor.Add(0.244897959);
            lst_mid_factor.Add(0.255102041);
            lst_mid_factor.Add(0.299319728);
            lst_mid_factor.Add(0.304081633);
            lst_mid_factor.Add(0.353061224);
            lst_mid_factor.Add(0.402040816);
            lst_mid_factor.Add(0.451020408);
            lst_mid_factor.Add(0.5);
            lst_mid_factor.Add(0.548979592);
            lst_mid_factor.Add(0.597959184);
            lst_mid_factor.Add(0.646938776);
            lst_mid_factor.Add(0.695918367);
            lst_mid_factor.Add(0.700680272);
            lst_mid_factor.Add(0.744897959);
            lst_mid_factor.Add(0.755102041);
            lst_mid_factor.Add(0.793877551);
            lst_mid_factor.Add(0.836734694);
            lst_mid_factor.Add(0.842857143);
            lst_mid_factor.Add(0.891836735);
            lst_mid_factor.Add(0.918367347);
            lst_mid_factor.Add(0.940816327);
            lst_mid_factor.Add(0.976190476);
            lst_mid_factor.Add(0.979591837);
            lst_mid_factor.Add(0.989795918);
            lst_mid_factor.Add(1);

            #endregion Mid Span

            #region Right Span

            List<double> lst_right_factor = new List<double>();


            lst_right_factor.Add(0.006896552);
            lst_right_factor.Add(0.07862069);
            lst_right_factor.Add(0.089655172);
            lst_right_factor.Add(0.172413793);
            lst_right_factor.Add(0.177931034);
            lst_right_factor.Add(0.255172414);
            lst_right_factor.Add(0.277241379);
            lst_right_factor.Add(0.337931034);
            lst_right_factor.Add(0.376551724);
            lst_right_factor.Add(0.475862069);
            lst_right_factor.Add(0.575172414);
            lst_right_factor.Add(0.613793103);
            lst_right_factor.Add(0.674482759);
            lst_right_factor.Add(0.696551724);
            lst_right_factor.Add(0.773793103);
            lst_right_factor.Add(0.779310345);
            lst_right_factor.Add(0.862068966);
            lst_right_factor.Add(0.873103448);
            lst_right_factor.Add(0.95862069);
            lst_right_factor.Add(0.972413793);
            lst_right_factor.Add(0.986206897);
            lst_right_factor.Add(1);
            #endregion Right Span

            List<double> lst_x_coords = new List<double>();


            for (int i = 0; i < lst_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + 0.25) * lst_factor[i]);
            }

            for (int i = 0; i < lst_mid_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + 0.25) + (L2 + 1.5) * lst_mid_factor[i]);
            }
            for (int i = 0; i < lst_right_factor.Count; i++)
            {
                lst_x_coords.Add(((L1 + 0.25) + (L2 + 1.5)) + (L3 + 0.25) * lst_right_factor[i]);
            }


            list.Add(string.Format("ASTRA FLOOR CONTINUOUS PSC BOX GIRDER BRIDGE ANALYSIS"));
            list.Add(string.Format("UNIT   MTON   MET"));
            list.Add(string.Format("JOINT   COORDINATES"));


            for (int i = 0; i < lst_x_coords.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-7:f2} {2,-7:f3} {3,-7:f3}", (i + 1), lst_x_coords[i], 0, 0));
            }


            #region Default Data
            //list.Add(string.Format("1   0   0   0"));
            //list.Add(string.Format("2   0.5   0   0"));
            //list.Add(string.Format("3   1   0   0"));
            //list.Add(string.Format("4   1.5   0   0"));
            //list.Add(string.Format("5   4.6   0   0"));
            //list.Add(string.Format("6   5   0   0"));
            //list.Add(string.Format("7   8   0   0"));
            //list.Add(string.Format("8   8.2   0   0"));
            //list.Add(string.Format("9   11   0   0"));
            //list.Add(string.Format("10   11.8   0   0"));
            //list.Add(string.Format("11   14   0   0"));
            //list.Add(string.Format("12   15.4   0   0"));
            //list.Add(string.Format("13   19   0   0"));
            //list.Add(string.Format("14   22.6   0   0"));
            //list.Add(string.Format("15   24   0   0"));
            //list.Add(string.Format("16   26.2   0   0"));
            //list.Add(string.Format("17   27   0   0"));
            //list.Add(string.Format("18   29.8   0   0"));
            //list.Add(string.Format("19   30   0   0"));
            //list.Add(string.Format("20   33   0   0"));
            //list.Add(string.Format("21   33.4   0   0"));
            //list.Add(string.Format("22   36   0   0"));
            //list.Add(string.Format("23   36.25   0   0"));
            //list.Add(string.Format("24   37   0   0"));
            //list.Add(string.Format("25   37.75   0   0"));
            //list.Add(string.Format("26   38   0   0"));
            //list.Add(string.Format("27   40.6   0   0"));
            //list.Add(string.Format("28   42.25   0   0"));
            //list.Add(string.Format("29   44.2   0   0"));
            //list.Add(string.Format("30   47.8   0   0"));
            //list.Add(string.Format("31   48.25   0   0"));
            //list.Add(string.Format("32   51.4   0   0"));
            //list.Add(string.Format("33   54.25   0   0"));
            //list.Add(string.Format("34   55   0   0"));
            //list.Add(string.Format("35   58.25   0   0"));
            //list.Add(string.Format("36   58.6   0   0"));
            //list.Add(string.Format("37   62.2   0   0"));
            //list.Add(string.Format("38   65.8   0   0"));
            //list.Add(string.Format("39   69.4   0   0"));
            //list.Add(string.Format("40   73   0   0"));
            //list.Add(string.Format("41   76.6   0   0"));
            //list.Add(string.Format("42   80.2   0   0"));
            //list.Add(string.Format("43   83.8   0   0"));
            //list.Add(string.Format("44   87.4   0   0"));
            //list.Add(string.Format("45   87.75   0   0"));
            //list.Add(string.Format("46   91   0   0"));
            //list.Add(string.Format("47   91.75   0   0"));
            //list.Add(string.Format("48   94.6   0   0"));
            //list.Add(string.Format("49   97.75   0   0"));
            //list.Add(string.Format("50   98.2   0   0"));
            //list.Add(string.Format("51   101.8   0   0"));
            //list.Add(string.Format("52   103.75   0   0"));
            //list.Add(string.Format("53   105.4   0   0"));
            //list.Add(string.Format("54   108   0   0"));
            //list.Add(string.Format("55   108.25   0   0"));
            //list.Add(string.Format("56   109   0   0"));
            //list.Add(string.Format("57   109.75   0   0"));
            //list.Add(string.Format("58   110   0   0"));
            //list.Add(string.Format("59   112.6   0   0"));
            //list.Add(string.Format("60   113   0   0"));
            //list.Add(string.Format("61   116   0   0"));
            //list.Add(string.Format("62   116.2   0   0"));
            //list.Add(string.Format("63   119   0   0"));
            //list.Add(string.Format("64   119.8   0   0"));
            //list.Add(string.Format("65   122   0   0"));
            //list.Add(string.Format("66   123.4   0   0"));
            //list.Add(string.Format("67   127   0   0"));
            //list.Add(string.Format("68   130.6   0   0"));
            //list.Add(string.Format("69   132   0   0"));
            //list.Add(string.Format("70   134.2   0   0"));
            //list.Add(string.Format("71   135   0   0"));
            //list.Add(string.Format("72   137.8   0   0"));
            //list.Add(string.Format("73   138   0   0"));
            //list.Add(string.Format("74   141   0   0"));
            //list.Add(string.Format("75   141.4   0   0"));
            //list.Add(string.Format("76   144.5   0   0"));
            //list.Add(string.Format("77   145   0   0"));
            //list.Add(string.Format("78   145.5   0   0"));
            //list.Add(string.Format("79   146   0   0"));
            #endregion Default Data


            #region Member Incidence
            list.Add(string.Format("MEMBER   INCIDENCE"));
            list.Add(string.Format("1   1   2"));
            list.Add(string.Format("2   2   3"));
            list.Add(string.Format("3   3   4"));
            list.Add(string.Format("4   4   5"));
            list.Add(string.Format("5   5   6"));
            list.Add(string.Format("6   6   7"));
            list.Add(string.Format("7   7   8"));
            list.Add(string.Format("8   8   9"));
            list.Add(string.Format("9   9   10"));
            list.Add(string.Format("10   10   11"));
            list.Add(string.Format("11   11   12"));
            list.Add(string.Format("12   12   13"));
            list.Add(string.Format("13   13   14"));
            list.Add(string.Format("14   14   15"));
            list.Add(string.Format("15   15   16"));
            list.Add(string.Format("16   16   17"));
            list.Add(string.Format("17   17   18"));
            list.Add(string.Format("18   18   19"));
            list.Add(string.Format("19   19   20"));
            list.Add(string.Format("20   20   21"));
            list.Add(string.Format("21   21   22"));
            list.Add(string.Format("22   22   23"));
            list.Add(string.Format("23   23   24"));
            list.Add(string.Format("24   24   25"));
            list.Add(string.Format("25   25   26"));
            list.Add(string.Format("26   26   27"));
            list.Add(string.Format("27   27   28"));
            list.Add(string.Format("28   28   29"));
            list.Add(string.Format("29   29   30"));
            list.Add(string.Format("30   30   31"));
            list.Add(string.Format("31   31   32"));
            list.Add(string.Format("32   32   33"));
            list.Add(string.Format("33   33   34"));
            list.Add(string.Format("34   34   35"));
            list.Add(string.Format("35   35   36"));
            list.Add(string.Format("36   36   37"));
            list.Add(string.Format("37   37   38"));
            list.Add(string.Format("38   38   39"));
            list.Add(string.Format("39   39   40"));
            list.Add(string.Format("40   40   41"));
            list.Add(string.Format("41   41   42"));
            list.Add(string.Format("42   42   43"));
            list.Add(string.Format("43   43   44"));
            list.Add(string.Format("44   44   45"));
            list.Add(string.Format("45   45   46"));
            list.Add(string.Format("46   46   47"));
            list.Add(string.Format("47   47   48"));
            list.Add(string.Format("48   48   49"));
            list.Add(string.Format("49   49   50"));
            list.Add(string.Format("50   50   51"));
            list.Add(string.Format("51   51   52"));
            list.Add(string.Format("52   52   53"));
            list.Add(string.Format("53   53   54"));
            list.Add(string.Format("54   54   55"));
            list.Add(string.Format("55   55   56"));
            list.Add(string.Format("56   56   57"));
            list.Add(string.Format("57   57   58"));
            list.Add(string.Format("58   58   59"));
            list.Add(string.Format("59   59   60"));
            list.Add(string.Format("60   60   61"));
            list.Add(string.Format("61   61   62"));
            list.Add(string.Format("62   62   63"));
            list.Add(string.Format("63   63   64"));
            list.Add(string.Format("64   64   65"));
            list.Add(string.Format("65   65   66"));
            list.Add(string.Format("66   66   67"));
            list.Add(string.Format("67   67   68"));
            list.Add(string.Format("68   68   69"));
            list.Add(string.Format("69   69   70"));
            list.Add(string.Format("70   70   71"));
            list.Add(string.Format("71   71   72"));
            list.Add(string.Format("72   72   73"));
            list.Add(string.Format("73   73   74"));
            list.Add(string.Format("74   74   75"));
            list.Add(string.Format("75   75   76"));
            list.Add(string.Format("76   76   77"));
            list.Add(string.Format("77   77   78"));
            list.Add(string.Format("78   78   79"));
            #endregion Member Incidence

            list.AddRange(Get_Sink_Member_Properties_Data().ToArray());

            //list.Add(string.Format("MEMBER   PROPERTY"));
            //list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));


            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("3   FIXED   BUT   MZ"));
            //list.Add(string.Format("24   56   77   FIXED   BUT   FX   MZ   "));
            if (file_name == Analysis_SINK_CONTINUOUS_SPANS)
            {
                list.Add(string.Format("24 56 77 FIXED BUT FY MX MY MZ"));
            }
            else
                list.Add(string.Format("24   56   77   FIXED   BUT   FX   MZ"));

            //list.Add(string.Format("LOAD    1    DL + SIDL  + LL "));
            //list.Add(string.Format("LOAD   1  DL + SIDL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 35 UNI GY -1.0"));
            //list.Add(string.Format("48 TO 59 UNI GY -5.5"));
            //list.Add(string.Format("36 TO 47 60 TO 71 UNI GY -5.5"));
            list.AddRange(loads);
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            //File.WriteAllLines(file_name, list.ToArray(), true);
            File.WriteAllLines(file_name, list.ToArray());

        }

        public void Create_End_Span_Data(string file_name)
        {
            List<string> list = new List<string>();


            List<double> lst_factor = new List<double>();

            #region Left Span

            lst_factor.Add(0);
            lst_factor.Add(0.013793103);
            lst_factor.Add(0.027586207);
            lst_factor.Add(0.04137931);
            lst_factor.Add(0.126896552);
            lst_factor.Add(0.137931034);
            lst_factor.Add(0.220689655);
            lst_factor.Add(0.226206897);
            lst_factor.Add(0.303448276);
            lst_factor.Add(0.325517241);
            lst_factor.Add(0.386206897);
            lst_factor.Add(0.424827586);
            lst_factor.Add(0.524137931);
            lst_factor.Add(0.623448276);
            lst_factor.Add(0.662068966);
            lst_factor.Add(0.722758621);
            lst_factor.Add(0.744827586);
            lst_factor.Add(0.822068966);
            lst_factor.Add(0.827586207);
            lst_factor.Add(0.910344828);
            lst_factor.Add(0.92137931);
            lst_factor.Add(0.993103448);
            lst_factor.Add(1);

            #endregion Left Span



            List<double> lst_x_coords = new List<double>();



            for (int i = 0; i < lst_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            }


            //list.Add(string.Format("ASTRA   SPACE   LIVE   LOAD   ANALYSIS-CONTINUOUS   STAGE"));
            list.Add(string.Format("ASTRA FLOOR CONTINUOUS PSC BOX GIRDER BRIDGE ANALYSIS"));
            list.Add(string.Format("UNIT MTON MET"));
            list.Add(string.Format("JOINT COORDINATES"));


            for (int i = 0; i < lst_x_coords.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-7:f2} {2,-7:f3} {3,-7:f3}", (i + 1), lst_x_coords[i], 0, 0));
            }

            list.Add(string.Format("MEMBER INCIDENCE"));
            list.Add(string.Format("1   1   2"));
            list.Add(string.Format("2   2   3"));
            list.Add(string.Format("3   3   4"));
            list.Add(string.Format("4   4   5"));
            list.Add(string.Format("5   5   6"));
            list.Add(string.Format("6   6   7"));
            list.Add(string.Format("7   7   8"));
            list.Add(string.Format("8   8   9"));
            list.Add(string.Format("9   9   10"));
            list.Add(string.Format("10   10   11"));
            list.Add(string.Format("11   11   12"));
            list.Add(string.Format("12   12   13"));
            list.Add(string.Format("13   13   14"));
            list.Add(string.Format("14   14   15"));
            list.Add(string.Format("15   15   16"));
            list.Add(string.Format("16   16   17"));
            list.Add(string.Format("17   17   18"));
            list.Add(string.Format("18   18   19"));
            list.Add(string.Format("19   19   20"));
            list.Add(string.Format("20   20   21"));
            list.Add(string.Format("21   21   22"));
            list.Add(string.Format("22   22   23"));


            list.AddRange(Get_End_Section_Member_Properties_Data().ToArray());

            //list.Add(string.Format("MEMBER   PROPERTY"));
            //list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 3162277 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));


            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("3   FIXED   BUT   MZ"));
            list.Add(string.Format("21  FIXED BUT   FX   MZ   ")); 
            //list.Add(string.Format("LOAD   1  DL + SIDL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 22 UNI GY -1.0")); 

            list.AddRange(LOADS_DL_SIDL_End_Span.ToArray()); 


            list.Add(string.Format("PERFORM ANALYSIS")); 
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            //File.WriteAllLines(file_name, list.ToArray(), true);
            File.WriteAllLines(file_name, list.ToArray());

        }
        public void Create_Mid_Span_Data(string file_name)
        {


            List<string> list = new List<string>();


            List<double> lst_factor = new List<double>();



            #region Mid Span

            List<double> lst_mid_factor = new List<double>();


            lst_mid_factor.Add(0.0);

            lst_mid_factor.Add(0.010204082);
            lst_mid_factor.Add(0.020408163);
            lst_mid_factor.Add(0.023809524);
            lst_mid_factor.Add(0.059183673);
            lst_mid_factor.Add(0.081632653);
            lst_mid_factor.Add(0.108163265);
            lst_mid_factor.Add(0.157142857);
            lst_mid_factor.Add(0.163265306);
            lst_mid_factor.Add(0.206122449);
            lst_mid_factor.Add(0.244897959);
            lst_mid_factor.Add(0.255102041);
            lst_mid_factor.Add(0.299319728);
            lst_mid_factor.Add(0.304081633);
            lst_mid_factor.Add(0.353061224);
            lst_mid_factor.Add(0.402040816);
            lst_mid_factor.Add(0.451020408);
            lst_mid_factor.Add(0.5);
            lst_mid_factor.Add(0.548979592);
            lst_mid_factor.Add(0.597959184);
            lst_mid_factor.Add(0.646938776);
            lst_mid_factor.Add(0.695918367);
            lst_mid_factor.Add(0.700680272);
            lst_mid_factor.Add(0.744897959);
            lst_mid_factor.Add(0.755102041);
            lst_mid_factor.Add(0.793877551);
            lst_mid_factor.Add(0.836734694);
            lst_mid_factor.Add(0.842857143);
            lst_mid_factor.Add(0.891836735);
            lst_mid_factor.Add(0.918367347);
            lst_mid_factor.Add(0.940816327);
            lst_mid_factor.Add(0.976190476);
            lst_mid_factor.Add(0.979591837);
            lst_mid_factor.Add(0.989795918);
            lst_mid_factor.Add(1);

            #endregion Mid Span



            List<double> lst_x_coords = new List<double>();




            for (int i = 0; i < lst_mid_factor.Count; i++)
            {
                lst_x_coords.Add((L2 + (Support_Distance * 3.0)) * lst_mid_factor[i]);
            }




            list.Add(string.Format("ASTRA FLOOR CONTINUOUS PSC BOX GIRDER BRIDGE ANALYSIS"));
            list.Add(string.Format("UNIT   MTON   MET"));
            list.Add(string.Format("JOINT   COORDINATES"));


            for (int i = 0; i < lst_x_coords.Count; i++)
            {
                list.Add(string.Format("{0,-5} {1,-7:f2} {2,-7:f3} {3,-7:f3}", (i + 1), lst_x_coords[i], 0, 0));
            }


            list.Add(string.Format("MEMBER INCIDENCE"));
            list.Add(string.Format("1   1   2"));
            list.Add(string.Format("2   2   3"));
            list.Add(string.Format("3   3   4"));
            list.Add(string.Format("4   4   5"));
            list.Add(string.Format("5   5   6"));
            list.Add(string.Format("6   6   7"));
            list.Add(string.Format("7   7   8"));
            list.Add(string.Format("8   8   9"));
            list.Add(string.Format("9   9   10"));
            list.Add(string.Format("10   10   11"));
            list.Add(string.Format("11   11   12"));
            list.Add(string.Format("12   12   13"));
            list.Add(string.Format("13   13   14"));
            list.Add(string.Format("14   14   15"));
            list.Add(string.Format("15   15   16"));
            list.Add(string.Format("16   16   17"));
            list.Add(string.Format("17   17   18"));
            list.Add(string.Format("18   18   19"));
            list.Add(string.Format("19   19   20"));
            list.Add(string.Format("20   20   21"));
            list.Add(string.Format("21   21   22"));
            list.Add(string.Format("22   22   23"));
            list.Add(string.Format("23   23   24"));
            list.Add(string.Format("24   24   25"));
            list.Add(string.Format("25   25   26"));
            list.Add(string.Format("26   26   27"));
            list.Add(string.Format("27   27   28"));
            list.Add(string.Format("28   28   29"));
            list.Add(string.Format("29   29   30"));
            list.Add(string.Format("30   30   31"));
            list.Add(string.Format("31   31   32"));
            list.Add(string.Format("32   32   33"));
            list.Add(string.Format("33   33   34"));
            list.Add(string.Format("34   34   35"));
            //list.Add(string.Format("35   35   36"));
            //list.Add(string.Format("36   36   37"));
            //list.Add(string.Format("37   37   38"));
            //list.Add(string.Format("38   38   39"));
            //list.Add(string.Format("39   39   40"));
            //list.Add(string.Format("40   40   41"));
            //list.Add(string.Format("41   41   42"));
            //list.Add(string.Format("42   42   43"));
            //list.Add(string.Format("43   43   44"));
            //list.Add(string.Format("44   44   45"));
            //list.Add(string.Format("45   45   46"));
            //list.Add(string.Format("46   46   47"));
            //list.Add(string.Format("47   47   48"));
            //list.Add(string.Format("48   48   49"));
            //list.Add(string.Format("49   49   50"));
            //list.Add(string.Format("50   50   51"));
            //list.Add(string.Format("51   51   52"));
            //list.Add(string.Format("52   52   53"));
            //list.Add(string.Format("53   53   54"));
            //list.Add(string.Format("54   54   55"));
            //list.Add(string.Format("55   55   56"));
            //list.Add(string.Format("56   56   57"));
            //list.Add(string.Format("57   57   58"));
            //list.Add(string.Format("58   58   59"));
            //list.Add(string.Format("59   59   60"));
            //list.Add(string.Format("60   60   61"));
            //list.Add(string.Format("61   61   62"));
            //list.Add(string.Format("62   62   63"));
            //list.Add(string.Format("63   63   64"));
            //list.Add(string.Format("64   64   65"));
            //list.Add(string.Format("65   65   66"));
            //list.Add(string.Format("66   66   67"));
            //list.Add(string.Format("67   67   68"));
            //list.Add(string.Format("68   68   69"));
            //list.Add(string.Format("69   69   70"));
            //list.Add(string.Format("70   70   71"));
            //list.Add(string.Format("71   71   72"));
            //list.Add(string.Format("72   72   73"));
            //list.Add(string.Format("73   73   74"));
            //list.Add(string.Format("74   74   75"));
            //list.Add(string.Format("75   75   76"));
            //list.Add(string.Format("76   76   77"));
            //list.Add(string.Format("77   77   78"));
            //list.Add(string.Format("78   78   79"));

            list.AddRange(Get_Mid_Section_Member_Properties_Data().ToArray());

            //list.Add(string.Format("MEMBER   PROPERTY"));
            //list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", (WidthBridge - WidthCantilever)));
            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("E 3162277 ALL"));
            list.Add(string.Format("DENSITY CONCRETE ALL"));
            list.Add(string.Format("POISSON CONCRETE ALL"));


            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("2   FIXED   BUT   MZ"));
            list.Add(string.Format("34   FIXED   BUT   FX   MZ"));
            
            //list.Add(string.Format("LOAD   1  DL + SIDL"));
            //list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("1 TO 34 UNI GY -1.0"));
            list.AddRange(LOADS_DL_SIDL_Mid_Span.ToArray()); 
           
            list.Add(string.Format("PERFORM ANALYSIS"));
            //list.Add(string.Format("*   MAXIMUM   REACTION   AT   SUPPORT   1"));
            //list.Add(string.Format("LOAD   LIST   11"));
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            //File.WriteAllLines(file_name, list.ToArray(), true);
            File.WriteAllLines(file_name, list.ToArray());

        }
        public void Create_Temparature_Data(string file_name, string[] temp_loads)
        {

            List<string> list = new List<string>();
           
            list.Add(string.Format(""));
            list.Add(string.Format("ASTRA FLOOR CPSC BOX GIRDER BRIDGE DECK ANALYSIS WITH TEMPERATURE LOAD"));
            list.Add(string.Format("UNIT  MTON  MET  "));
            list.Add(string.Format("JOINT  COORDINATES "));
            list.Add(string.Format("1  0  0  0"));
            list.Add(string.Format("2  0  0.4  0"));
            list.Add(string.Format("3  0  1.7  0"));
            list.Add(string.Format("4  0  3  0"));
            list.Add(string.Format("5  0  3.275  0"));
            list.Add(string.Format("6  0.15  3.275  0"));
            list.Add(string.Format("7  0.45  3.275  0"));
            list.Add(string.Format("8  2.75  3.275  0"));
            list.Add(string.Format("9  5.05  3.275  0"));
            list.Add(string.Format("10  5.35  3.275  0"));
            list.Add(string.Format("11  5.5  3.275  0"));
            list.Add(string.Format("12  5.5  3  0"));
            list.Add(string.Format("13  5.5  1.7  0"));
            list.Add(string.Format("14  5.5  0.4  0"));
            list.Add(string.Format("15  5.5  0  0"));
            list.Add(string.Format("16  5.35  0  0"));
            list.Add(string.Format("17  5.05  0  0"));
            list.Add(string.Format("18  2.75  0  0"));
            list.Add(string.Format("19  0.45  0  0"));
            list.Add(string.Format("20  0.15  0  0"));
            list.Add(string.Format("21  -0.15  3.275  0"));
            list.Add(string.Format("22  -2.79  3.275  0"));
            list.Add(string.Format("23  5.65  3.275  0"));
            list.Add(string.Format("24  8.29  3.275  0"));
            list.Add(string.Format("MEMBER  INCIDENCES "));
            list.Add(string.Format("1  1  2  19  1  1  "));
            list.Add(string.Format("20  20  1  "));
            list.Add(string.Format("21  5  21  "));
            list.Add(string.Format("22  21  22  "));
            list.Add(string.Format("23  11  23  "));
            list.Add(string.Format("24  23  24  "));
            list.Add(string.Format("MEMBER  PROPERTIES "));
            list.Add(string.Format("1  2  3  4  11  12  13  14  PRI  YD  0.3  ZD  1"));
            list.Add(string.Format("5  10  PRI  YD  0.4  ZD  1"));
            list.Add(string.Format("6  9  PRI  YD  0.325  ZD  1"));
            list.Add(string.Format("7  8  PRI  YD  0.25  ZD  1"));
            list.Add(string.Format("15  20  PRI  YD  0.5  ZD  1"));
            list.Add(string.Format("16  19  PRI  YD  0.35  ZD  1"));
            list.Add(string.Format("17  18  PRI  YD  0.2  ZD  1"));
            list.Add(string.Format("21  23  PRI  YD  0.3  ZD  1"));
            list.Add(string.Format("22  24  PRI  YD  0.25  ZD  1"));
            list.Add(string.Format("CONSTANTS"));
            list.Add(string.Format("DENSITY  2.5  ALL  "));
            list.Add(string.Format("E  3224658  ALL  "));
            list.Add(string.Format("ALPHA  1.17E-05  ALL  "));
            list.Add(string.Format("SUPPORT"));
            list.Add(string.Format("1  15  PINNED  "));
            list.AddRange(temp_loads);
            #region Chiranjit [2013 12 16]
            //list.Add(string.Format("LOAD  1  CHANGE  IN  TEMP  FOR  50  DEGREE  CENTIGRADE  IN  ALL  MEMBERS  "));
            //list.Add(string.Format("TEMP  LOAD "));
            //list.Add(string.Format("1  TO  24  TEMP  50 "));
            //list.Add(string.Format("LOAD  2  POSITIVE  TEMP  DIFFERENCES  LEFT  WEB  AT  TOP  "));
            //list.Add(string.Format("TEMP  LOAD "));
            //list.Add(string.Format("5  10  TEMP  8.9  17.8 "));
            //list.Add(string.Format("6  9  TEMP  9.5  16.6 "));
            //list.Add(string.Format("7  8  TEMP  10.1  15.4 "));
            //list.Add(string.Format("15  TO  20  TEMP  1.05  2.1  "));
            //list.Add(string.Format("1  TO  4  TEMP  8.9  17.8  "));
            //list.Add(string.Format("11  TO  14  TEMP  1.05  2.1  "));
            //list.Add(string.Format("LOAD  3  POSITIVE  TEMP  DIFFERENCES  RIGHT  WEB  AT  TOP  "));
            //list.Add(string.Format("TEMP  LOAD "));
            //list.Add(string.Format("5  10  TEMP  8.9  17.8 "));
            //list.Add(string.Format("6  9  TEMP  9.5  16.6 "));
            //list.Add(string.Format("7  8  TEMP  10.1  15.4 "));
            //list.Add(string.Format("15  TO  20  TEMP  1.05  2.1  "));
            //list.Add(string.Format("11  TO  14  TEMP  8.9  17.8  "));
            //list.Add(string.Format("1  TO  4  TEMP  1.05  2.1  "));
            //list.Add(string.Format("LOAD  4  REVERSE  TEMP  DIFFERENCES  LEFT  WEB  AT  TOP  "));
            //list.Add(string.Format("TEMP  LOAD "));
            //list.Add(string.Format("5  10  TEMP  -5.3  -10.6 "));
            //list.Add(string.Format("6  9  TEMP  -5.51875  -10.1625 "));
            //list.Add(string.Format("7  8  TEMP  -5.65  -9.9 "));
            //list.Add(string.Format("15  20  TEMP  -3.3  -6.6 "));
            //list.Add(string.Format("16  19  TEMP  -3.5  -6.2 "));
            //list.Add(string.Format("17  18  TEMP  -3.7  -5.8 "));
            //list.Add(string.Format("1  TO  4  TEMP  -5.3  -10.6  "));
            //list.Add(string.Format("11  TO  14  TEMP  -3.3  -6.6  "));
            //list.Add(string.Format("LOAD  5  REVERSE  TEMP  DIFFERENCES  RIGHT  WEB  AT  TOP  "));
            //list.Add(string.Format("TEMP  LOAD "));
            //list.Add(string.Format("5  10  TEMP  -5.3  -10.6 "));
            //list.Add(string.Format("6  9  TEMP  -5.51875  -10.1625 "));
            //list.Add(string.Format("7  8  TEMP  -5.65  -9.9 "));
            //list.Add(string.Format("15  20  TEMP  -3.3  -6.6 "));
            //list.Add(string.Format("16  19  TEMP  -3.5  -6.2 "));
            //list.Add(string.Format("17  18  TEMP  -3.7  -5.8 "));
            //list.Add(string.Format("11  TO  14  TEMP  -5.3  -10.6  "));
            //list.Add(string.Format("1  TO  4  TEMP  -3.3  -6.6  "));
            #endregion Chiranjit [2013 12 16]
            list.Add(string.Format("PERFORM  ANALYSIS "));
            list.Add(string.Format("FINISH"));

            File.WriteAllLines(file_name, list.ToArray());

        }
        public List<string> Get_Continuous_Member_Properties_Data()
        {

            List<string> list = new List<string>();


            List<double> End_Avg_Area = Get_Average_Area_End_Span();
            List<double> End_ISelf_Area = Get_Average_ISelf_End_Span();
            List<double> Mid_Avg_Area = Get_Average_Area_Mid_Span();
            List<double> Mid_ISelf_Area = Get_Average_ISelf_Mid_Span();



            #region Member Incidence
            //list.Add(string.Format("MEMBER   INCIDENCE"));
            //list.Add(string.Format("1   1   2"));
            //list.Add(string.Format("2   2   3"));
            //list.Add(string.Format("3   3   4"));
            //list.Add(string.Format("4   4   5"));
            //list.Add(string.Format("5   5   6"));
            //list.Add(string.Format("6   6   7"));
            //list.Add(string.Format("7   7   8"));
            //list.Add(string.Format("8   8   9"));
            //list.Add(string.Format("9   9   10"));
            //list.Add(string.Format("10   10   11"));
            //list.Add(string.Format("11   11   12"));
            //list.Add(string.Format("12   12   13"));
            //list.Add(string.Format("13   13   14"));
            //list.Add(string.Format("14   14   15"));
            //list.Add(string.Format("15   15   16"));
            //list.Add(string.Format("16   16   17"));
            //list.Add(string.Format("17   17   18"));
            //list.Add(string.Format("18   18   19"));
            //list.Add(string.Format("19   19   20"));
            //list.Add(string.Format("20   20   21"));
            //list.Add(string.Format("21   21   22"));
            //list.Add(string.Format("22   22   23"));
            //list.Add(string.Format("23   23   24"));
            //list.Add(string.Format("24   24   25"));
            //list.Add(string.Format("25   25   26"));
            //list.Add(string.Format("26   26   27"));
            //list.Add(string.Format("27   27   28"));
            //list.Add(string.Format("28   28   29"));
            //list.Add(string.Format("29   29   30"));
            //list.Add(string.Format("30   30   31"));
            //list.Add(string.Format("31   31   32"));
            //list.Add(string.Format("32   32   33"));
            //list.Add(string.Format("33   33   34"));
            //list.Add(string.Format("34   34   35"));
            //list.Add(string.Format("35   35   36"));
            //list.Add(string.Format("36   36   37"));
            //list.Add(string.Format("37   37   38"));
            //list.Add(string.Format("38   38   39"));
            //list.Add(string.Format("39   39   40"));
            //list.Add(string.Format("40   40   41"));
            //list.Add(string.Format("41   41   42"));
            //list.Add(string.Format("42   42   43"));
            //list.Add(string.Format("43   43   44"));
            //list.Add(string.Format("44   44   45"));
            //list.Add(string.Format("45   45   46"));
            //list.Add(string.Format("46   46   47"));
            //list.Add(string.Format("47   47   48"));
            //list.Add(string.Format("48   48   49"));
            //list.Add(string.Format("49   49   50"));
            //list.Add(string.Format("50   50   51"));
            //list.Add(string.Format("51   51   52"));
            //list.Add(string.Format("52   52   53"));
            //list.Add(string.Format("53   53   54"));
            //list.Add(string.Format("54   54   55"));
            //list.Add(string.Format("55   55   56"));
            //list.Add(string.Format("56   56   57"));
            //list.Add(string.Format("57   57   58"));
            //list.Add(string.Format("58   58   59"));
            //list.Add(string.Format("59   59   60"));
            //list.Add(string.Format("60   60   61"));
            //list.Add(string.Format("61   61   62"));
            //list.Add(string.Format("62   62   63"));
            //list.Add(string.Format("63   63   64"));
            //list.Add(string.Format("64   64   65"));
            //list.Add(string.Format("65   65   66"));
            //list.Add(string.Format("66   66   67"));
            //list.Add(string.Format("67   67   68"));
            //list.Add(string.Format("68   68   69"));
            //list.Add(string.Format("69   69   70"));
            //list.Add(string.Format("70   70   71"));
            //list.Add(string.Format("71   71   72"));
            //list.Add(string.Format("72   72   73"));
            //list.Add(string.Format("73   73   74"));
            //list.Add(string.Format("74   74   75"));
            //list.Add(string.Format("75   75   76"));
            //list.Add(string.Format("76   76   77"));
            //list.Add(string.Format("77   77   78"));
            //list.Add(string.Format("78   78   79"));
            #endregion Member Incidence

            #region MEMBER   PROPERTIES
            list.Add(string.Format("SECTION PROPERTIES"));
            list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("2   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("3   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("4   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("5   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));





            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("6   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));





            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("7   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("8   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));





            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("9   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));


            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("10   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));




            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("11   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, End_Span_Sections[4].I, (End_Span_Sections[4].I + 0.001)));




            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("12   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));


            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("13   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));





            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("14   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));



            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("15   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));


            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("16   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));



            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("17   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("18   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("19   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));


            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("20   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));



            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("21   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));



            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("22   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("23   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));





            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("24   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("25   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("26   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));



            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("27   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(string.Format("28   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1], (Mid_ISelf_Area[1] + 0.001)));




            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(string.Format("29   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2], (Mid_ISelf_Area[2] + 0.001)));





            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(string.Format("30   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3], (Mid_ISelf_Area[3] + 0.001)));

     


            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("31   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1]));
            list.Add(string.Format("31   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));




            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("32   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));


            
            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(string.Format("33   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, Mid_Span_Sections[5].I, (Mid_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(string.Format("34   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], Mid_ISelf_Area[5], (Mid_ISelf_Area[5] + 0.001)));



            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("35   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("36   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("37   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("38   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("39   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("40   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("41   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("42   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("43   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("44   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(string.Format("45   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], Mid_ISelf_Area[5], (Mid_ISelf_Area[5] + 0.001)));

            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(string.Format("46   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, Mid_Span_Sections[5].I, (Mid_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("47   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("48   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(string.Format("49   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3], (Mid_ISelf_Area[3] + 0.001)));

            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(string.Format("50   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2], (Mid_ISelf_Area[2] + 0.001)));

            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(string.Format("51   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1], (Mid_ISelf_Area[1] + 0.001)));

            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("52   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("53   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("54   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("55   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("56   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("57   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("58   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("59   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("60   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));

            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("61   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));

            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("62   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));

            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("63   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));

            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("64   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("65   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("66   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("67   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("68   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, End_Span_Sections[4].I, (End_Span_Sections[4].I + 0.001)));

            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("69   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("70   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("71   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));



            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("72   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));


            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("73   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));



            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("74   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));

            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("75   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("76   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("77   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            #endregion MEMBER PROPERTIES

            return list;
        }

        public List<string> Get_Sink_Member_Properties_Data()
        {

            List<string> list = new List<string>();


            List<double> End_Avg_Area = Get_Average_Area_End_Span();
            List<double> End_ISelf_Area = Get_Average_ISelf_End_Span();
            List<double> Mid_Avg_Area = Get_Average_Area_Mid_Span();
            List<double> Mid_ISelf_Area = Get_Average_ISelf_Mid_Span();



            #region Member Incidence
            //list.Add(string.Format("MEMBER   INCIDENCE"));
            //list.Add(string.Format("1   1   2"));
            //list.Add(string.Format("2   2   3"));
            //list.Add(string.Format("3   3   4"));
            //list.Add(string.Format("4   4   5"));
            //list.Add(string.Format("5   5   6"));
            //list.Add(string.Format("6   6   7"));
            //list.Add(string.Format("7   7   8"));
            //list.Add(string.Format("8   8   9"));
            //list.Add(string.Format("9   9   10"));
            //list.Add(string.Format("10   10   11"));
            //list.Add(string.Format("11   11   12"));
            //list.Add(string.Format("12   12   13"));
            //list.Add(string.Format("13   13   14"));
            //list.Add(string.Format("14   14   15"));
            //list.Add(string.Format("15   15   16"));
            //list.Add(string.Format("16   16   17"));
            //list.Add(string.Format("17   17   18"));
            //list.Add(string.Format("18   18   19"));
            //list.Add(string.Format("19   19   20"));
            //list.Add(string.Format("20   20   21"));
            //list.Add(string.Format("21   21   22"));
            //list.Add(string.Format("22   22   23"));
            //list.Add(string.Format("23   23   24"));
            //list.Add(string.Format("24   24   25"));
            //list.Add(string.Format("25   25   26"));
            //list.Add(string.Format("26   26   27"));
            //list.Add(string.Format("27   27   28"));
            //list.Add(string.Format("28   28   29"));
            //list.Add(string.Format("29   29   30"));
            //list.Add(string.Format("30   30   31"));
            //list.Add(string.Format("31   31   32"));
            //list.Add(string.Format("32   32   33"));
            //list.Add(string.Format("33   33   34"));
            //list.Add(string.Format("34   34   35"));
            //list.Add(string.Format("35   35   36"));
            //list.Add(string.Format("36   36   37"));
            //list.Add(string.Format("37   37   38"));
            //list.Add(string.Format("38   38   39"));
            //list.Add(string.Format("39   39   40"));
            //list.Add(string.Format("40   40   41"));
            //list.Add(string.Format("41   41   42"));
            //list.Add(string.Format("42   42   43"));
            //list.Add(string.Format("43   43   44"));
            //list.Add(string.Format("44   44   45"));
            //list.Add(string.Format("45   45   46"));
            //list.Add(string.Format("46   46   47"));
            //list.Add(string.Format("47   47   48"));
            //list.Add(string.Format("48   48   49"));
            //list.Add(string.Format("49   49   50"));
            //list.Add(string.Format("50   50   51"));
            //list.Add(string.Format("51   51   52"));
            //list.Add(string.Format("52   52   53"));
            //list.Add(string.Format("53   53   54"));
            //list.Add(string.Format("54   54   55"));
            //list.Add(string.Format("55   55   56"));
            //list.Add(string.Format("56   56   57"));
            //list.Add(string.Format("57   57   58"));
            //list.Add(string.Format("58   58   59"));
            //list.Add(string.Format("59   59   60"));
            //list.Add(string.Format("60   60   61"));
            //list.Add(string.Format("61   61   62"));
            //list.Add(string.Format("62   62   63"));
            //list.Add(string.Format("63   63   64"));
            //list.Add(string.Format("64   64   65"));
            //list.Add(string.Format("65   65   66"));
            //list.Add(string.Format("66   66   67"));
            //list.Add(string.Format("67   67   68"));
            //list.Add(string.Format("68   68   69"));
            //list.Add(string.Format("69   69   70"));
            //list.Add(string.Format("70   70   71"));
            //list.Add(string.Format("71   71   72"));
            //list.Add(string.Format("72   72   73"));
            //list.Add(string.Format("73   73   74"));
            //list.Add(string.Format("74   74   75"));
            //list.Add(string.Format("75   75   76"));
            //list.Add(string.Format("76   76   77"));
            //list.Add(string.Format("77   77   78"));
            //list.Add(string.Format("78   78   79"));
            #endregion Member Incidence

            #region MEMBER   PROPERTIES
            list.Add(string.Format("SECTION PROPERTIES"));
            list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("2   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, 0.0001, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("3   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("4   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("5   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, 0.0001,(End_Span_Sections[1].I + 0.001)));





            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("6   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], 0.0001, (End_ISelf_Area[1] + 0.0001)));





            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("7   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("8   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));





            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("9   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], 0.0001, (End_ISelf_Area[2] + 0.0001)));


            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("10   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));




            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("11   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, 0.0001,(End_Span_Sections[4].I + 0.001)));




            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("12   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 0.001)));


            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("13   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 0.001)));





            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("14   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));



            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("15   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));


            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("16   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], 0.0001, (End_ISelf_Area[2] + 0.0001)));



            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("17   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("18   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("19   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], 0.0001, (End_ISelf_Area[1] + 0.0001)));


            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("20   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, 0.0001,(End_Span_Sections[1].I + 0.001)));



            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("21   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));



            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("22   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("23   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, 0.0001, (End_Span_Sections[0].I + 0.001)));





            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("24   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, 0.0001, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("25   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, 0.0001, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("26   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));



            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("27   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, 0.0001, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(string.Format("28   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], 0.0001,(Mid_ISelf_Area[1] + 0.001)));




            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(string.Format("29   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], 0.0001,(Mid_ISelf_Area[2] + 0.001)));





            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(string.Format("30   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], 0.0001,(Mid_ISelf_Area[3] + 0.001)));




            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("31   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1]));
            list.Add(string.Format("31   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], 0.0001,(Mid_ISelf_Area[4] + 0.001)));




            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("32   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], 0.0001,(Mid_ISelf_Area[4] + 0.001)));



            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(string.Format("33   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, 0.0001,(Mid_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(string.Format("34   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], 0.0001,(Mid_ISelf_Area[5] + 0.001)));



            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("35   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("36   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("37   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("38   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("39   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("40   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("41   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("42   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("43   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("44   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(string.Format("45   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], 0.0001,(Mid_ISelf_Area[5] + 0.001)));

            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(string.Format("46   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, 0.0001,(Mid_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("47   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], 0.0001,(Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("48   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], 0.0001,(Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(string.Format("49   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], 0.0001,(Mid_ISelf_Area[3] + 0.001)));

            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(string.Format("50   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], 0.0001,(Mid_ISelf_Area[2] + 0.001)));

            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(string.Format("51   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], 0.0001,(Mid_ISelf_Area[1] + 0.001)));

            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("52   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, 0.0001, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("53   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("54   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("55   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("56   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("57   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("58   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("59   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("60   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], 0.0001, (End_ISelf_Area[1] + 0.0001)));

            //list.Add(string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("61   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));

            //list.Add(string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("62   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));

            //list.Add(string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("63   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], 0.0001, (End_ISelf_Area[2] + 0.0001)));

            //list.Add(string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("64   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("65   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("66   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("67   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, 0.0001,(End_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("68   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, 0.0001,(End_Span_Sections[4].I + 0.001)));

            //list.Add(string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("69   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("70   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], 0.0001,(End_ISelf_Area[3] + 0.0001)));

            //list.Add(string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("71   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], 0.0001, (End_ISelf_Area[2] + 0.0001)));



            //list.Add(string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("72   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, 0.0001,(End_Span_Sections[2].I + 0.001)));


            //list.Add(string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("73   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], 0.0001, (End_ISelf_Area[1] + 0.0001)));



            //list.Add(string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("74   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, 0.0001,(End_Span_Sections[1].I + 0.001)));

            //list.Add(string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("75   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("76   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("77   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], 0.0001,(End_ISelf_Area[0] + 0.0001)));

            list.Add(string.Format("78   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            #endregion MEMBER PROPERTIES

            return list;
        }



        public List<string> Get_End_Section_Member_Properties_Data()
        {
            List<string> list = new List<string>();


            List<double> End_Avg_Area = Get_Average_Area_End_Span();
            List<double> End_ISelf_Area = Get_Average_ISelf_End_Span();
            List<double> Mid_Avg_Area = Get_Average_Area_Mid_Span();
            List<double> Mid_ISelf_Area = Get_Average_ISelf_Mid_Span();


            #region MEMBER   PROPERTIES
            list.Add(string.Format("SECTION PROPERTIES"));
            list.Add(string.Format("1   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            //list.Add(string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("2   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("3   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("4   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("5   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));





            //list.Add(string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("6   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));





            //list.Add(string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("7   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("8   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));





            //list.Add(string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("9   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));


            //list.Add(string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("10   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));




            //list.Add(string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("11   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, End_Span_Sections[4].I, (End_Span_Sections[4].I + 0.001)));




            //list.Add(string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("12   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));


            //list.Add(string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("13   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));





            //list.Add(string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("14   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));



            //list.Add(string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(string.Format("15   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));


            //list.Add(string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(string.Format("16   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));



            //list.Add(string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("17   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("18   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(string.Format("19   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));


            //list.Add(string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("20   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));



            //list.Add(string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("21   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));



            //list.Add(string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("22   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            #endregion MEMBER PROPERTIES

            return list;
        }
        public List<string> Get_Mid_Section_Member_Properties_Data()
        {

            List<string> list = new List<string>();


            List<double> End_Avg_Area = Get_Average_Area_End_Span();
            List<double> End_ISelf_Area = Get_Average_ISelf_End_Span();
            List<double> Mid_Avg_Area = Get_Average_Area_Mid_Span();
            List<double> Mid_ISelf_Area = Get_Average_ISelf_Mid_Span();





            #region MEMBER   PROPERTIES
            list.Add(string.Format("SECTION PROPERTIES"));


            #region 
            ////list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("1   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[6].Total_Area, End_Span_Sections[6].I));

            ////list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("2   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[6].Total_Area, End_Span_Sections[6].I));

            ////list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("3   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[5], End_ISelf_Area[5]));

            ////list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("4   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[4], End_ISelf_Area[4]));

            ////list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("5   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[3], End_ISelf_Area[3]));

            ////list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("6   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4]));

            ////list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("7   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3]));

            ////list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("8   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2]));

            ////list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            ////list.Add(string.Format("31   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1]));
            //list.Add(string.Format("9   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I));

            ////list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("10   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I));

            ////list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("11   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1]));

            ////list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("34   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[0], Mid_ISelf_Area[0]));

            ////list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("12   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("13   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("14   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("15   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("16   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("17   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("18   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("19   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("20   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            //list.Add(string.Format("21   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I));

            ////list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            //list.Add(string.Format("22   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[0], Mid_ISelf_Area[0]));

            ////list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            //list.Add(string.Format("23   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1]));

            ////list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("24   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I));

            ////list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("25   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I));

            ////list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            //list.Add(string.Format("26   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2]));

            ////list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            //list.Add(string.Format("27   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3]));

            ////list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            //list.Add(string.Format("28   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4]));

            ////list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("29   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[3], End_ISelf_Area[3]));

            ////list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("30   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[4], End_ISelf_Area[4]));

            ////list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("31   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[5], End_ISelf_Area[5]));

            ////list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("32   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[6].Total_Area, End_Span_Sections[6].I));

            ////list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("33   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Span_Sections[6].Total_Area, End_Span_Sections[6].I));

            ////list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            //list.Add(string.Format("34   PRI   AX   {0:f4}   IX   0.0001   IY   0.001   IZ   {1:f4}", End_Avg_Area[5], End_ISelf_Area[5]));

            #endregion




            //list.Add(string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("1   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("2   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("3   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("4   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("5   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(string.Format("6   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1], (Mid_ISelf_Area[1] + 0.001)));

            //list.Add(string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(string.Format("7   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2], (Mid_ISelf_Area[2] + 0.001)));

            //list.Add(string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(string.Format("8   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3], (Mid_ISelf_Area[3] + 0.001)));

            //list.Add(string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            //list.Add(string.Format("31   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1]));
            list.Add(string.Format("9   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("10   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(string.Format("11   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, Mid_Span_Sections[5].I, (Mid_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(string.Format("12   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], Mid_ISelf_Area[5], (Mid_ISelf_Area[5] + 0.001)));

            //list.Add(string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("13   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("14   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("15   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("16   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("17   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("18   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("19   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("20   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("21   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(string.Format("22   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(string.Format("23   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], Mid_ISelf_Area[5], (Mid_ISelf_Area[5] + 0.001)));

            //list.Add(string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(string.Format("24   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, Mid_Span_Sections[5].I, (Mid_Span_Sections[5].I + 0.001)));

            //list.Add(string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("25   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(string.Format("26   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(string.Format("27   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3], (Mid_ISelf_Area[3] + 0.001)));

            //list.Add(string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(string.Format("28   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2], (Mid_ISelf_Area[2] + 0.001)));

            //list.Add(string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(string.Format("29   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1], (Mid_ISelf_Area[1] + 0.001)));

            //list.Add(string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("30   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("31   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("32   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("33   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("34   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(string.Format("35   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            #endregion MEMBER PROPERTIES

            return list;
        }


        public List<string> Get_Continuous_LL_Member_Properties_Data()
        {

            List<string> list = new List<string>();


            List<double> End_Avg_Area = Get_Average_Area_End_Span();
            List<double> End_ISelf_Area = Get_Average_ISelf_End_Span();
            List<double> Mid_Avg_Area = Get_Average_Area_Mid_Span();
            List<double> Mid_ISelf_Area = Get_Average_ISelf_Mid_Span();

          

            #region Member Incidence
            //list.Add(mem_nos[nd++] + string.Format("MEMBER   INCIDENCE"));
            //list.Add(mem_nos[nd++] + string.Format("1   1   2"));
            //list.Add(mem_nos[nd++] + string.Format("2   2   3"));
            //list.Add(mem_nos[nd++] + string.Format("3   3   4"));
            //list.Add(mem_nos[nd++] + string.Format("4   4   5"));
            //list.Add(mem_nos[nd++] + string.Format("5   5   6"));
            //list.Add(mem_nos[nd++] + string.Format("6   6   7"));
            //list.Add(mem_nos[nd++] + string.Format("7   7   8"));
            //list.Add(mem_nos[nd++] + string.Format("8   8   9"));
            //list.Add(mem_nos[nd++] + string.Format("9   9   10"));
            //list.Add(mem_nos[nd++] + string.Format("10   10   11"));
            //list.Add(mem_nos[nd++] + string.Format("11   11   12"));
            //list.Add(mem_nos[nd++] + string.Format("12   12   13"));
            //list.Add(mem_nos[nd++] + string.Format("13   13   14"));
            //list.Add(mem_nos[nd++] + string.Format("14   14   15"));
            //list.Add(mem_nos[nd++] + string.Format("15   15   16"));
            //list.Add(mem_nos[nd++] + string.Format("16   16   17"));
            //list.Add(mem_nos[nd++] + string.Format("17   17   18"));
            //list.Add(mem_nos[nd++] + string.Format("18   18   19"));
            //list.Add(mem_nos[nd++] + string.Format("19   19   20"));
            //list.Add(mem_nos[nd++] + string.Format("20   20   21"));
            //list.Add(mem_nos[nd++] + string.Format("21   21   22"));
            //list.Add(mem_nos[nd++] + string.Format("22   22   23"));
            //list.Add(mem_nos[nd++] + string.Format("23   23   24"));
            //list.Add(mem_nos[nd++] + string.Format("24   24   25"));
            //list.Add(mem_nos[nd++] + string.Format("25   25   26"));
            //list.Add(mem_nos[nd++] + string.Format("26   26   27"));
            //list.Add(mem_nos[nd++] + string.Format("27   27   28"));
            //list.Add(mem_nos[nd++] + string.Format("28   28   29"));
            //list.Add(mem_nos[nd++] + string.Format("29   29   30"));
            //list.Add(mem_nos[nd++] + string.Format("30   30   31"));
            //list.Add(mem_nos[nd++] + string.Format("31   31   32"));
            //list.Add(mem_nos[nd++] + string.Format("32   32   33"));
            //list.Add(mem_nos[nd++] + string.Format("33   33   34"));
            //list.Add(mem_nos[nd++] + string.Format("34   34   35"));
            //list.Add(mem_nos[nd++] + string.Format("35   35   36"));
            //list.Add(mem_nos[nd++] + string.Format("36   36   37"));
            //list.Add(mem_nos[nd++] + string.Format("37   37   38"));
            //list.Add(mem_nos[nd++] + string.Format("38   38   39"));
            //list.Add(mem_nos[nd++] + string.Format("39   39   40"));
            //list.Add(mem_nos[nd++] + string.Format("40   40   41"));
            //list.Add(mem_nos[nd++] + string.Format("41   41   42"));
            //list.Add(mem_nos[nd++] + string.Format("42   42   43"));
            //list.Add(mem_nos[nd++] + string.Format("43   43   44"));
            //list.Add(mem_nos[nd++] + string.Format("44   44   45"));
            //list.Add(mem_nos[nd++] + string.Format("45   45   46"));
            //list.Add(mem_nos[nd++] + string.Format("46   46   47"));
            //list.Add(mem_nos[nd++] + string.Format("47   47   48"));
            //list.Add(mem_nos[nd++] + string.Format("48   48   49"));
            //list.Add(mem_nos[nd++] + string.Format("49   49   50"));
            //list.Add(mem_nos[nd++] + string.Format("50   50   51"));
            //list.Add(mem_nos[nd++] + string.Format("51   51   52"));
            //list.Add(mem_nos[nd++] + string.Format("52   52   53"));
            //list.Add(mem_nos[nd++] + string.Format("53   53   54"));
            //list.Add(mem_nos[nd++] + string.Format("54   54   55"));
            //list.Add(mem_nos[nd++] + string.Format("55   55   56"));
            //list.Add(mem_nos[nd++] + string.Format("56   56   57"));
            //list.Add(mem_nos[nd++] + string.Format("57   57   58"));
            //list.Add(mem_nos[nd++] + string.Format("58   58   59"));
            //list.Add(mem_nos[nd++] + string.Format("59   59   60"));
            //list.Add(mem_nos[nd++] + string.Format("60   60   61"));
            //list.Add(mem_nos[nd++] + string.Format("61   61   62"));
            //list.Add(mem_nos[nd++] + string.Format("62   62   63"));
            //list.Add(mem_nos[nd++] + string.Format("63   63   64"));
            //list.Add(mem_nos[nd++] + string.Format("64   64   65"));
            //list.Add(mem_nos[nd++] + string.Format("65   65   66"));
            //list.Add(mem_nos[nd++] + string.Format("66   66   67"));
            //list.Add(mem_nos[nd++] + string.Format("67   67   68"));
            //list.Add(mem_nos[nd++] + string.Format("68   68   69"));
            //list.Add(mem_nos[nd++] + string.Format("69   69   70"));
            //list.Add(mem_nos[nd++] + string.Format("70   70   71"));
            //list.Add(mem_nos[nd++] + string.Format("71   71   72"));
            //list.Add(mem_nos[nd++] + string.Format("72   72   73"));
            //list.Add(mem_nos[nd++] + string.Format("73   73   74"));
            //list.Add(mem_nos[nd++] + string.Format("74   74   75"));
            //list.Add(mem_nos[nd++] + string.Format("75   75   76"));
            //list.Add(mem_nos[nd++] + string.Format("76   76   77"));
            //list.Add(mem_nos[nd++] + string.Format("77   77   78"));
            //list.Add(mem_nos[nd++] + string.Format("78   78   79"));
            #endregion Member Incidence

            #region MEMBER   PROPERTIES
            list.Add(string.Format("SECTION PROPERTIES"));



            List<string> mem_nos = new List<string>();
            List<int> lval = new List<int>();
            List<int> lval_dummy = new List<int>();
            List<string> mem_nos_dummy = new List<string>();

            int nd = 0;
            for (int i = 1; i < _Columns; i++)
            {
                lval.Clear();
                //lval_dummy.Clear();
                for (int j = 0; j < _Rows; j++)
                {
                    //nd = i + 78*j;
                    if (j == ((_Rows / 2)))
                        lval.Add(i + (_Columns - 1) * j);
                    else
                        lval_dummy.Add(i + (_Columns - 1) * j);
                }
                mem_nos.Add(MyList.Get_Array_Text(lval));
                //mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));
            }
            mem_nos_dummy.Add(MyList.Get_Array_Text(lval_dummy));
            mem_nos.Add(lval_dummy[lval_dummy.Count - 1] + 1 + " TO " + MemColls.Count);


            list.Add(mem_nos_dummy[nd] + string.Format("  PRI   AX   0.0001   IX   0.0001   IY   0.0001   IZ   0.0001"));

            list.Add(mem_nos[nd++] + string.Format("   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            //list.Add(mem_nos[nd++] + string.Format("2   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("3   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(mem_nos[nd++] + string.Format("4   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(mem_nos[nd++] + string.Format("5   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));





            //list.Add(mem_nos[nd++] + string.Format("6   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));





            //list.Add(mem_nos[nd++] + string.Format("7   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("8   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));





            //list.Add(mem_nos[nd++] + string.Format("9   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));


            //list.Add(mem_nos[nd++] + string.Format("10   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));




            //list.Add(mem_nos[nd++] + string.Format("11   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, End_Span_Sections[4].I, (End_Span_Sections[4].I + 0.001)));




            //list.Add(mem_nos[nd++] + string.Format("12   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));


            //list.Add(mem_nos[nd++] + string.Format("13   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));





            //list.Add(mem_nos[nd++] + string.Format("14   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));



            //list.Add(mem_nos[nd++] + string.Format("15   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));


            //list.Add(mem_nos[nd++] + string.Format("16   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));



            //list.Add(mem_nos[nd++] + string.Format("17   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("18   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("19   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));


            //list.Add(mem_nos[nd++] + string.Format("20   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("21   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));



            //list.Add(mem_nos[nd++] + string.Format("22   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));


            //list.Add(mem_nos[nd++] + string.Format("23   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));





            //list.Add(mem_nos[nd++] + string.Format("24   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("25   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("26   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));



            //list.Add(mem_nos[nd++] + string.Format("27   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("28   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1], (Mid_ISelf_Area[1] + 0.001)));




            //list.Add(mem_nos[nd++] + string.Format("29   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2], (Mid_ISelf_Area[2] + 0.001)));





            //list.Add(mem_nos[nd++] + string.Format("30   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3], (Mid_ISelf_Area[3] + 0.001)));




            //list.Add(mem_nos[nd++] + string.Format("31   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));




            //list.Add(mem_nos[nd++] + string.Format("32   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("33   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, Mid_Span_Sections[5].I, (Mid_Span_Sections[5].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("34   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], Mid_ISelf_Area[5], (Mid_ISelf_Area[5] + 0.001)));



            //list.Add(mem_nos[nd++] + string.Format("35   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("36   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("37   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("38   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("39   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("40   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("41   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("42   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("43   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("44   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 1)));

            //list.Add(mem_nos[nd++] + string.Format("45   PRI   AX   6.583304443   IX   0.0001   IY   0.001   IZ   12.12210332"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[5], Mid_ISelf_Area[5], (Mid_ISelf_Area[5] + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("46   PRI   AX   7.210491943   IX   0.0001   IY   0.001   IZ   12.57015003"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Span_Sections[5].Total_Area, Mid_Span_Sections[5].I, (Mid_Span_Sections[5].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("47   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("48   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[4], Mid_ISelf_Area[4], (Mid_ISelf_Area[4] + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("49   PRI   AX   7.377094336   IX   0.0001   IY   0.001   IZ   12.69319171"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[3], Mid_ISelf_Area[3], (Mid_ISelf_Area[3] + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("50   PRI   AX   7.809985547   IX   0.0001   IY   0.001   IZ   13.02259159"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[2], Mid_ISelf_Area[2], (Mid_ISelf_Area[2] + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("51   PRI   AX   8.406328711   IX   0.0001   IY   0.001   IZ   13.48331798"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", Mid_Avg_Area[1], Mid_ISelf_Area[1], (Mid_ISelf_Area[1] + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("52   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[0].Total_Area, End_Span_Sections[0].I, (End_Span_Sections[0].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("53   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("54   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("55   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("56   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("57   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("58   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("59   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("60   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("61   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("62   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("63   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("64   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("65   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("66   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("67   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[5].Total_Area, End_Span_Sections[5].I, (End_Span_Sections[5].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("68   PRI   AX   6.075   IX   0.0001   IY   0.001   IZ   11.76160605"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[4].Total_Area, End_Span_Sections[4].I, (End_Span_Sections[4].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("69   PRI   AX   6.5332875   IX   0.0001   IY   0.001   IZ   12.0856395"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("70   PRI   AX   7.160475   IX   0.0001   IY   0.001   IZ   12.53368622"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[3], End_ISelf_Area[3], (End_ISelf_Area[3] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("71   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[2], End_ISelf_Area[2], (End_ISelf_Area[2] + 0.0001)));



            //list.Add(mem_nos[nd++] + string.Format("72   PRI   AX   7.329375   IX   0.0001   IY   0.001   IZ   12.65769947"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[2].Total_Area, End_Span_Sections[2].I, (End_Span_Sections[2].I + 0.001)));


            //list.Add(mem_nos[nd++] + string.Format("73   PRI   AX   7.9734375   IX   0.0001   IY   0.001   IZ   13.1539181"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[1], End_ISelf_Area[1], (End_ISelf_Area[1] + 0.0001)));



            //list.Add(mem_nos[nd++] + string.Format("74   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Span_Sections[1].Total_Area, End_Span_Sections[1].I, (End_Span_Sections[1].I + 0.001)));

            //list.Add(mem_nos[nd++] + string.Format("75   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("76   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            //list.Add(mem_nos[nd++] + string.Format("77   PRI   AX   8.6175   IX   0.0001   IY   0.001   IZ   13.65013672"));
            list.Add(mem_nos[nd++] + string.Format("   PRI   AX   {0:f4}   IX   {1:f4}   IY   0.001   IZ   {2:f4}", End_Avg_Area[0], End_ISelf_Area[0], (End_ISelf_Area[0] + 0.0001)));

            list.Add(mem_nos[nd++] + string.Format("   PRI    YD   0.4   ZD   {0:f3}", WidthBridge));
            #endregion MEMBER PROPERTIES

            list.Add(mem_nos[nd++] + string.Format("      PRI   AX   0.001   IX   0.0001   IY   0.001   IZ   0.00011"));

            return list;
        }


        public List<double> Get_Average_Area_Mid_Span()
        {
            List<double> list = new List<double>();

            if (Mid_Span_Sections.Count > 0)
            {
                for (int i = 1; i < Mid_Span_Sections.Count; i++)
                {
                    list.Add((Mid_Span_Sections[i - 1].Total_Area + Mid_Span_Sections[i].Total_Area) / 2.0);
                }
            }
            return list;
        }
        public List<double> Get_Average_ISelf_Mid_Span()
        {
            List<double> list = new List<double>();

            if (Mid_Span_Sections.Count > 0)
            {
                for (int i = 1; i < Mid_Span_Sections.Count; i++)
                {
                    list.Add((Mid_Span_Sections[i - 1].I + Mid_Span_Sections[i].I) / 2.0);
                }
            }
            return list;
        }
        public List<double> Get_Average_Area_End_Span()
        {
            List<double> list = new List<double>();

            if (End_Span_Sections.Count > 0)
            {
                for (int i = 1; i < End_Span_Sections.Count; i++)
                {
                    list.Add((End_Span_Sections[i - 1].Total_Area + End_Span_Sections[i].Total_Area) / 2.0);
                }
            }
            return list;
        }
        public List<double> Get_Average_ISelf_End_Span()
        {
            List<double> list = new List<double>();

            if (End_Span_Sections.Count > 0)
            {
                for (int i = 1; i < End_Span_Sections.Count; i++)
                {
                    list.Add((End_Span_Sections[i - 1].I + End_Span_Sections[i].I) / 2.0);
                }
            }
            return list;
        }



        public void Create_LiveLoad_Data()
        {
 
            double x_incr = Spacing_Cross_Girder;
            double z_incr = Spacing_Long_Girder;

            JointNode nd;
        

            int iCols = 0;
            int iRows = 0;

            if (Joints == null)
                Joints = new JointNodeCollection();
            Joints.Clear();

            double skew_length = Math.Tan((Skew_Angle * (Math.PI / 180.0)));

            double val1 = 12.1;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            last_x = 0.0;


            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();


            List<double> lst_factor = new List<double>();

            #region Left Span

            lst_factor.Add(0);
            lst_factor.Add(0.013793103);
            lst_factor.Add(0.027586207);
            lst_factor.Add(0.04137931);
            lst_factor.Add(0.126896552);
            lst_factor.Add(0.137931034);
            lst_factor.Add(0.220689655);
            lst_factor.Add(0.226206897);
            lst_factor.Add(0.303448276);
            lst_factor.Add(0.325517241);
            lst_factor.Add(0.386206897);
            lst_factor.Add(0.424827586);
            lst_factor.Add(0.524137931);
            lst_factor.Add(0.623448276);
            lst_factor.Add(0.662068966);
            lst_factor.Add(0.722758621);
            lst_factor.Add(0.744827586);
            lst_factor.Add(0.822068966);
            lst_factor.Add(0.827586207);
            lst_factor.Add(0.910344828);
            lst_factor.Add(0.92137931);
            lst_factor.Add(0.993103448);
            lst_factor.Add(1);

            #endregion Left Span

            #region Mid Span

            List<double> lst_mid_factor = new List<double>();

            lst_mid_factor.Add(0.010204082);
            lst_mid_factor.Add(0.020408163);
            lst_mid_factor.Add(0.023809524);
            lst_mid_factor.Add(0.059183673);
            lst_mid_factor.Add(0.081632653);
            lst_mid_factor.Add(0.108163265);
            lst_mid_factor.Add(0.157142857);
            lst_mid_factor.Add(0.163265306);
            lst_mid_factor.Add(0.206122449);
            lst_mid_factor.Add(0.244897959);
            lst_mid_factor.Add(0.255102041);
            lst_mid_factor.Add(0.299319728);
            lst_mid_factor.Add(0.304081633);
            lst_mid_factor.Add(0.353061224);
            lst_mid_factor.Add(0.402040816);
            lst_mid_factor.Add(0.451020408);
            lst_mid_factor.Add(0.5);
            lst_mid_factor.Add(0.548979592);
            lst_mid_factor.Add(0.597959184);
            lst_mid_factor.Add(0.646938776);
            lst_mid_factor.Add(0.695918367);
            lst_mid_factor.Add(0.700680272);
            lst_mid_factor.Add(0.744897959);
            lst_mid_factor.Add(0.755102041);
            lst_mid_factor.Add(0.793877551);
            lst_mid_factor.Add(0.836734694);
            lst_mid_factor.Add(0.842857143);
            lst_mid_factor.Add(0.891836735);
            lst_mid_factor.Add(0.918367347);
            lst_mid_factor.Add(0.940816327);
            lst_mid_factor.Add(0.976190476);
            lst_mid_factor.Add(0.979591837);
            lst_mid_factor.Add(0.989795918);
            lst_mid_factor.Add(1);

            #endregion Mid Span

            #region Right Span

            List<double> lst_right_factor = new List<double>();


            lst_right_factor.Add(0.006896552);
            lst_right_factor.Add(0.07862069);
            lst_right_factor.Add(0.089655172);
            lst_right_factor.Add(0.172413793);
            lst_right_factor.Add(0.177931034);
            lst_right_factor.Add(0.255172414);
            lst_right_factor.Add(0.277241379);
            lst_right_factor.Add(0.337931034);
            lst_right_factor.Add(0.376551724);
            lst_right_factor.Add(0.475862069);
            lst_right_factor.Add(0.575172414);
            lst_right_factor.Add(0.613793103);
            lst_right_factor.Add(0.674482759);
            lst_right_factor.Add(0.696551724);
            lst_right_factor.Add(0.773793103);
            lst_right_factor.Add(0.779310345);
            lst_right_factor.Add(0.862068966);
            lst_right_factor.Add(0.873103448);
            lst_right_factor.Add(0.95862069);
            lst_right_factor.Add(0.972413793);
            lst_right_factor.Add(0.986206897);
            lst_right_factor.Add(1);
            #endregion Right Span

            List<double> lst_x_coords = new List<double>();

            
            int i = 0;

            for (i = 0; i < lst_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) * lst_factor[i]);
            }
            
            for (i = 0; i < lst_mid_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance *3)) * lst_mid_factor[i]);
            }
            for (i = 0; i < lst_right_factor.Count; i++)
            {
                lst_x_coords.Add(((L1 + (Support_Distance / 2.0)) + (L2 + (Support_Distance * 3.0))) + (L3 + (Support_Distance / 2.0)) * lst_right_factor[i]);
            }
        
            list_x.AddRange(lst_x_coords);

            bool flag = true;

            list_x.Sort();


            list_z.Add(0);

            //last_z = WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            last_z = WidthBridge / 2.0;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            //last_z = WidthBridge - WidthCantilever;
            //last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            //list_z.Add(last_z);

            last_z = WidthBridge;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            last_z = WidthCantilever + z_incr;
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

                if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
                    list_z.Add(last_z);
                last_z += z_incr;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            } while (last_z <= WidthBridge);

            list_z.Sort();
            #endregion Chiranjit [2013 07 25] Correct Create Data

            _Columns = list_x.Count;
            _Rows = list_z.Count;

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

                    nd.X = list_x[iCols];

                    nd.NodeNo = Joints.JointNodes.Count + 1;
                    Joints.Add(nd);

                    Joints_Array[iRows, iCols] = nd;

                    last_x = nd.X;
                }
            }




            int nodeNo = 0;
            Joints.Clear();

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);
                }
            }

            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
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
        }


        //Chiranjit [2013 07 24] Add Moving Load File
        public void Create_LiveLoad_Data_2013_11_27()
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

            double val1 = 12.1;
            double val2 = val1 * skew_length;



            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();

            last_x = 0.0;


            #region Chiranjit [2013 07 25] Correct Create Data

            list_x.Clear();


            List<double> lst_factor = new List<double>();

            #region Left Span

            lst_factor.Add(0);
            lst_factor.Add(0.013793103);
            lst_factor.Add(0.027586207);
            lst_factor.Add(0.04137931);
            lst_factor.Add(0.126896552);
            lst_factor.Add(0.137931034);
            lst_factor.Add(0.220689655);
            lst_factor.Add(0.226206897);
            lst_factor.Add(0.303448276);
            lst_factor.Add(0.325517241);
            lst_factor.Add(0.386206897);
            lst_factor.Add(0.424827586);
            lst_factor.Add(0.524137931);
            lst_factor.Add(0.623448276);
            lst_factor.Add(0.662068966);
            lst_factor.Add(0.722758621);
            lst_factor.Add(0.744827586);
            lst_factor.Add(0.822068966);
            lst_factor.Add(0.827586207);
            lst_factor.Add(0.910344828);
            lst_factor.Add(0.92137931);
            lst_factor.Add(0.993103448);
            lst_factor.Add(1);

            #endregion Left Span

            #region Mid Span

            List<double> lst_mid_factor = new List<double>();

            lst_mid_factor.Add(0.010204082);
            lst_mid_factor.Add(0.020408163);
            lst_mid_factor.Add(0.023809524);
            lst_mid_factor.Add(0.059183673);
            lst_mid_factor.Add(0.081632653);
            lst_mid_factor.Add(0.108163265);
            lst_mid_factor.Add(0.157142857);
            lst_mid_factor.Add(0.163265306);
            lst_mid_factor.Add(0.206122449);
            lst_mid_factor.Add(0.244897959);
            lst_mid_factor.Add(0.255102041);
            lst_mid_factor.Add(0.299319728);
            lst_mid_factor.Add(0.304081633);
            lst_mid_factor.Add(0.353061224);
            lst_mid_factor.Add(0.402040816);
            lst_mid_factor.Add(0.451020408);
            lst_mid_factor.Add(0.5);
            lst_mid_factor.Add(0.548979592);
            lst_mid_factor.Add(0.597959184);
            lst_mid_factor.Add(0.646938776);
            lst_mid_factor.Add(0.695918367);
            lst_mid_factor.Add(0.700680272);
            lst_mid_factor.Add(0.744897959);
            lst_mid_factor.Add(0.755102041);
            lst_mid_factor.Add(0.793877551);
            lst_mid_factor.Add(0.836734694);
            lst_mid_factor.Add(0.842857143);
            lst_mid_factor.Add(0.891836735);
            lst_mid_factor.Add(0.918367347);
            lst_mid_factor.Add(0.940816327);
            lst_mid_factor.Add(0.976190476);
            lst_mid_factor.Add(0.979591837);
            lst_mid_factor.Add(0.989795918);
            lst_mid_factor.Add(1);

            #endregion Mid Span

            #region Right Span

            List<double> lst_right_factor = new List<double>();


            lst_right_factor.Add(0.006896552);
            lst_right_factor.Add(0.07862069);
            lst_right_factor.Add(0.089655172);
            lst_right_factor.Add(0.172413793);
            lst_right_factor.Add(0.177931034);
            lst_right_factor.Add(0.255172414);
            lst_right_factor.Add(0.277241379);
            lst_right_factor.Add(0.337931034);
            lst_right_factor.Add(0.376551724);
            lst_right_factor.Add(0.475862069);
            lst_right_factor.Add(0.575172414);
            lst_right_factor.Add(0.613793103);
            lst_right_factor.Add(0.674482759);
            lst_right_factor.Add(0.696551724);
            lst_right_factor.Add(0.773793103);
            lst_right_factor.Add(0.779310345);
            lst_right_factor.Add(0.862068966);
            lst_right_factor.Add(0.873103448);
            lst_right_factor.Add(0.95862069);
            lst_right_factor.Add(0.972413793);
            lst_right_factor.Add(0.986206897);
            lst_right_factor.Add(1);
            #endregion Right Span

            List<double> lst_x_coords = new List<double>();


            int i = 0;

            for ( i = 0; i < lst_factor.Count; i++)
            {
                lst_x_coords.Add(L1 * lst_factor[i]);
            }

            for ( i = 0; i < lst_mid_factor.Count; i++)
            {
                lst_x_coords.Add(L1 + L2 * lst_mid_factor[i]);
            }
            for ( i = 0; i < lst_right_factor.Count; i++)
            {
                lst_x_coords.Add((L1 + L2) + L3 * lst_right_factor[i]);
            }



            //last_x = Length / 2.0;
            //last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            //list_x.Add(last_x);

            list_x.AddRange(lst_x_coords);




            //last_x = x_incr;

            bool flag = true;

            list_x.Sort();


            list_z.Add(0);

            last_z = WidthCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

         









            last_z = WidthBridge / 2.0;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);









            last_z = WidthBridge - WidthCantilever;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

           


            last_z = WidthBridge;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);


            z_incr = (WidthBridge - 2 * WidthCantilever) / 2.0;
            last_z = WidthCantilever + z_incr;
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

                if (!flag && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever - 0.2))
                    list_z.Add(last_z);
                last_z += z_incr;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            } while (last_z <= WidthBridge);

            list_z.Sort();
            #endregion Chiranjit [2013 07 25] Correct Create Data



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

            for (iCols = 0; iCols < _Columns; iCols++)
            {
                for (iRows = 0; iRows < _Rows; iRows++)
                {
                    nodeNo++;
                    //Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                }
            }

            Member mem = new Member();

            if (MemColls == null)
                MemColls = new MemberCollection();
            MemColls.Clear();
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
        }


        //Chiranjit [2013 07 25] Add Moving Load File
        public List<string> Get_Bending_Moments_Shear_Forces()
        {
            string cont_joints = "3 4 5 8 10 12 13 14 16 18 21 23 24 25 27 29 30 32 34 36 37 38 39 40";
            string end_joints = "3 4 5 8 10 12 13 14 16 18 21";
            //string mid_joints = "27 29 30 32 34 36 37 38 39 40";
            string mid_joints = " 5  7  8 10 12 14 15 16 17 18";


            List<string> end_sections = new List<string>();
            end_sections.Add(string.Format("Support 1"));
            end_sections.Add(string.Format("Face of Diaphragm"));
            end_sections.Add(string.Format("1 L1/10"));
            end_sections.Add(string.Format("2 L1/10"));
            end_sections.Add(string.Format("3 L1/10"));
            end_sections.Add(string.Format("4 L1/10"));
            end_sections.Add(string.Format("5 L1/10"));
            end_sections.Add(string.Format("6 L1/10"));
            end_sections.Add(string.Format("7 L1/10"));
            end_sections.Add(string.Format("8 L1/10"));
            end_sections.Add(string.Format("9 L1/10"));


            List<string> mid_sections = new List<string>();
            mid_sections.Add(string.Format("1 L2/20"));
            mid_sections.Add(string.Format("2 L2/20"));
            mid_sections.Add(string.Format("3 L2/20"));
            mid_sections.Add(string.Format("4 L2/20"));
            mid_sections.Add(string.Format("5 L2/20"));
            mid_sections.Add(string.Format("6 L2/20"));
            mid_sections.Add(string.Format("7 L2/20"));
            mid_sections.Add(string.Format("8 L2/20"));
            mid_sections.Add(string.Format("9 L2/20"));
            mid_sections.Add(string.Format("10 L2/20"));


            List<string> cont_sections = new List<string>();
            cont_sections.Add(string.Format("Support 1"));
            cont_sections.Add(string.Format("Face of Diaphragm"));
            cont_sections.Add(string.Format("1 L1/10"));
            cont_sections.Add(string.Format("2 L1/10"));
            cont_sections.Add(string.Format("3 L1/10"));
            cont_sections.Add(string.Format("4 L1/10"));
            cont_sections.Add(string.Format("5 L1/10"));
            cont_sections.Add(string.Format("6 L1/10"));
            cont_sections.Add(string.Format("7 L1/10"));
            cont_sections.Add(string.Format("8 L1/10"));
            cont_sections.Add(string.Format("9 L1/10"));
            cont_sections.Add(string.Format("Face of Diaphragm"));
            cont_sections.Add(string.Format("Support 2"));
            cont_sections.Add(string.Format("Face of Diaphragm"));
            cont_sections.Add(string.Format("1 L2/20"));
            cont_sections.Add(string.Format("2 L2/20"));
            cont_sections.Add(string.Format("3 L2/20"));
            cont_sections.Add(string.Format("4 L2/20"));
            cont_sections.Add(string.Format("5 L2/20"));
            cont_sections.Add(string.Format("6 L2/20"));
            cont_sections.Add(string.Format("7 L2/20"));
            cont_sections.Add(string.Format("8 L2/20"));
            cont_sections.Add(string.Format("9 L2/20"));
            cont_sections.Add(string.Format("10 L2/20"));

            List<int> ll_joints = new List<int>();

            //ll_joints.AddRange(MyList.Get_Array_Intiger(cont_joints).ToArray());
            //int v = 3;
            int v = 7;
            for (int i = 0; i < _Columns; i++)
            {
                ll_joints.Add(v);
                //v += 5;
                v += 3;
            }



            //Chiranjit [2013 07 26] Read Data from Files
            #region Chiranjit [2013 07 26]

            List<int> end_list = MyList.Get_Array_Intiger(end_joints);
            List<int> mid_list = MyList.Get_Array_Intiger(mid_joints);
            List<int> cont_list = MyList.Get_Array_Intiger(cont_joints);

            List<MaxForce> ls_BM = new List<MaxForce>();
            List<MaxForce> ls_SF = new List<MaxForce>();
            List<MaxForce> ls_BM_Negative = new List<MaxForce>();
            List<MaxForce> ls_SF_Negative = new List<MaxForce>();
            MaxForce mfrc = new MaxForce();

            List<string> list = new List<string>();

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("------------------------------------------------------------------------"));
            list.Add(string.Format("SUMMARY OF BENDING MOMENTS AND SHEAR FORCES"));
            list.Add(string.Format("------------------------------------------------------------------------"));
            list.Add(string.Format(""));


            string format = "{0,-8} {1,-20:f3} {2,12:f3} {3,12:f3} {4,15:f3} {5,15:f3}";

            #region DL_SIDL_END_SPAN

            if (DL_SIDL_END_SPAN != null)
            {

                list.Add(string.Format("------------------------------------------------------------------------"));
                list.Add(string.Format("SPAN 1 (SIMPLY SUPPORTED) - {0} m SPAN", L1));
                list.Add(string.Format("------------------------------------------------------------------------"));
                list.Add(string.Format(""));

                foreach (var item in end_list)
                {
                    mfrc = DL_SIDL_END_SPAN.GetJoint_MomentForce(item, true);
                    ls_BM.Add(mfrc);
                    mfrc = DL_SIDL_END_SPAN.GetJoint_ShearForce(item, true);
                    ls_SF.Add(mfrc);
                }

                list.Add(string.Format(""));
                //list.Add(string.Format("DL_SIDL_END_SPAN"));
                list.Add(string.Format(""));
                //(INCREASE 15% FOR DISTORTION, WARPING & INCIDENTAL LOADS)

                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                #region Print Reports
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format(format,
                "",
                    "",
                    "",
            "",
            "(INCREASE 15% ",
            "FOR DISTORTION"));

                list.Add(string.Format(format,
                "",
                "",
                "",
                "",
            "WARPING   &  ",
            "INCIDENTAL LOADS)"));

                list.Add(string.Format(format,
                "DESIGN",
                    "DESIGN",
                    "DEAD LOAD",
            "DEAD LOAD",
            "DEAD LOAD",
            "DEAD LOAD"));

                list.Add(string.Format(format,
                " NODE",
                    "SECTION",
                    "BM (T-m)",
                    "SF (T)",
                    "BM (T-m)",
                    "SF (T)"));
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                for (int i = 0; i < ls_BM.Count; i++)
                {
                    //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
                    //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

                    list.Add(string.Format(format,
                        ls_BM[i].NodeNo,
                        end_sections[i],
                        ls_BM[i].Force,
                        ls_SF[i].Force,
                        ls_BM[i].Force * 1.15,
                        ls_SF[i].Force * 1.15));

                }
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                #endregion Print Reports
            }
            #endregion DL_SIDL_END_SPAN

            list.Add(string.Format(""));
            if (DL_SIDL_MID_SPAN != null)
            {
                #region DL_SIDL_MID_SPAN

                ls_BM.Clear();
                ls_SF.Clear();

                foreach (var item in mid_list)
                {
                    mfrc = DL_SIDL_MID_SPAN.GetJoint_MomentForce(item, true);
                    ls_BM.Add(mfrc);
                    mfrc = DL_SIDL_MID_SPAN.GetJoint_ShearForce(item, true);
                    ls_SF.Add(mfrc);
                }

                list.Add(string.Format(""));
                list.Add(string.Format("------------------------------------------------------------------------"));
                list.Add(string.Format("SPAN 2 (SIMPLY SUPPORTED) - {0} m SPAN", L2));
                list.Add(string.Format("------------------------------------------------------------------------"));
                //list.Add(string.Format("DL_SIDL_MID_SPAN"));
                list.Add(string.Format(""));


                #region Print Reports
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format(format,
                "",
                    "",
                    "",
            "",
            "(INCREASE 15% ",
            "FOR DISTORTION"));

                list.Add(string.Format(format,
                "",
                "",
                "",
                "",
            "WARPING   &  ",
            "INCIDENTAL LOADS)"));

                list.Add(string.Format(format,
                "DESIGN",
                    "DESIGN",
                    "DEAD LOAD",
            "DEAD LOAD",
            "DEAD LOAD",
            "DEAD LOAD"));

                list.Add(string.Format(format,
                " NODE",
                    "SECTION",
                    "BM (T-m)",
                    "SF (T)",
                    "BM (T-m)",
                    "SF (T)"));
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                for (int i = 0; i < ls_BM.Count; i++)
                {
                    //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
                    //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

                    list.Add(string.Format(format,
                        ls_BM[i].NodeNo,
                        mid_sections[i],
                        ls_BM[i].Force,
                        ls_SF[i].Force,
                        ls_BM[i].Force * 1.15,
                        ls_SF[i].Force * 1.15));

                }
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                #endregion Print Reports

                #endregion DL_SIDL_MID_SPAN
                list.Add(string.Format(""));
            }
            if (DL_SIDL_3_CONTINUOUS_SPANS != null)
            {
                #region DL_SIDL_3_CONTINUOUS_SPANS

                ls_BM.Clear();
                ls_SF.Clear();

                foreach (var item in cont_list)
                {
                    mfrc = DL_SIDL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, 1);
                    ls_BM.Add(mfrc);
                    mfrc = DL_SIDL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true, 1);
                    ls_SF.Add(mfrc);
                }

                list.Add(string.Format("----------------------------------------------------------------------------"));
                list.Add(string.Format("BENDING MOMENT AND SHEAR FORCE SUMMARY - CONTINUOUS STAGE (DUE TO DEAD LOAD)" ));
                list.Add(string.Format("----------------------------------------------------------------------------"));
                //list.Add(string.Format("DL_SIDL_3_CONTINUOUS_SPANS"));
                list.Add(string.Format(""));


                #region Print Reports
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format(format,
                "",
                    "",
                    "",
            "",
            "(INCREASE 15% ",
            "FOR DISTORTION"));

                list.Add(string.Format(format,
                "",
                "",
                "",
                "",
            "WARPING   &  ",
            "INCIDENTAL LOADS)"));

                list.Add(string.Format(format,
                "DESIGN",
                    "DESIGN",
                    "DEAD LOAD",
            "DEAD LOAD",
            "DEAD LOAD",
            "DEAD LOAD"));

                list.Add(string.Format(format,
                " NODE",
                    "SECTION",
                    "BM (T-m)",
                    "SF (T)",
                    "BM (T-m)",
                    "SF (T)"));
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                for (int i = 0; i < ls_BM.Count; i++)
                {
                    //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
                    //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

                    list.Add(string.Format(format,
                        ls_BM[i].NodeNo,
                        cont_sections[i],
                        ls_BM[i].Force,
                        ls_SF[i].Force,
                        ls_BM[i].Force * 1.15,
                        ls_SF[i].Force * 1.15));

                }
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                #endregion Print Reports

                #endregion DL_SIDL_3_CONTINUOUS_SPANS

                #region DL_SIDL_3_CONTINUOUS_SPANS

                ls_BM.Clear();
                ls_SF.Clear();

                foreach (var item in cont_list)
                {
                    mfrc = DL_SIDL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, 2);
                    ls_BM.Add(mfrc);
                    mfrc = DL_SIDL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true, 2);
                    ls_SF.Add(mfrc);
                }

                list.Add(string.Format(""));
                list.Add(string.Format("-------------------------------------------------------------------------------------------"));
                list.Add(string.Format("BENDING MOMENT AND SHEAR FORCE SUMMARY - CONTINUOUS STAGE  (DUE TO SUPERIMPOSED DEAD LOAD)"));
                list.Add(string.Format("-------------------------------------------------------------------------------------------"));
                //list.Add(string.Format("DL_SIDL_3_CONTINUOUS_SPANS"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));


                #region Print Reports
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format(format,
                "",
                    "",
                    "",
            "",
            "(INCREASE 15% ",
            "FOR DISTORTION"));

                list.Add(string.Format(format,
                "",
                "",
                "",
                "",
            "WARPING   &  ",
            "INCIDENTAL LOADS)"));

                list.Add(string.Format(format,
                "DESIGN",
                    "DESIGN",
                    "SIDL LOAD",
            "SIDL LOAD",
            "SIDL LOAD",
            "SIDL LOAD"));

                list.Add(string.Format(format,
                " NODE",
                    "SECTION",
                    "BM (T-m)",
                    "SF (T)",
                    "BM (T-m)",
                    "SF (T)"));
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                for (int i = 0; i < ls_BM.Count; i++)
                {
                    //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
                    //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

                    list.Add(string.Format(format,
                        ls_BM[i].NodeNo,
                        cont_sections[i],
                        ls_BM[i].Force,
                        ls_SF[i].Force,
                        ls_BM[i].Force * 1.15,
                        ls_SF[i].Force * 1.15));

                }
                list.Add(string.Format("------------------------------------------------------------------------------------------"));

                #endregion Print Reports

                #endregion DL_SIDL_3_CONTINUOUS_SPANS

                list.Add(string.Format(""));
            }

            if (DS_3_CONTINUOUS_SPANS != null)
            {

                #region Chiranjit [2013 12 12]


                int tot_load_case = 14;

                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("-----------------------------------------------------------------------------"));
                list.Add(string.Format("BENDING MOMENT AND SHEAR FORCE SUMMARY - CONTINUOUS STAGE  (DUE TO SINK LOAD)"));
                list.Add(string.Format("-----------------------------------------------------------------------------"));
                list.Add(string.Format(""));

                for (int lc = 1; lc <= tot_load_case; lc++)
                {
                    #region DS_3_CONTINUOUS_SPANS

                    ls_BM.Clear();
                    ls_SF.Clear();


                    foreach (var item in cont_list)
                    {
                        mfrc = DS_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, lc);
                        ls_BM.Add(mfrc);
                        mfrc = DS_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, lc);
                        ls_SF.Add(mfrc);
                    }


                    list.Add(string.Format("-----------------------------------------------------------------------------------"));
                    list.Add(string.Format("MAX BM AND CORRESPONDING SF SUMMARY DUE TO DIFFERENTIAL SINKING AT LOAD CASE {0}", lc));
                    list.Add(string.Format("-----------------------------------------------------------------------------------"));
                    #region Print Reports
                    list.Add(string.Format("------------------------------------------------------------------------------------------"));
                    list.Add(string.Format(format,
                    "",
                        "",
                        "",
                "",
                "(INCREASE 15% ",
                "FOR DISTORTION"));

                    list.Add(string.Format(format,
                    "",
                    "",
                    "",
                    "",
                "WARPING   &  ",
                "INCIDENTAL LOADS)"));

                    list.Add(string.Format(format,
                    "DESIGN",
                        "DESIGN",
                        "SINK LOAD",
                        "SINK LOAD",
                        "SINK LOAD",
                        "SINK LOAD"));

                    list.Add(string.Format(format,
                    " NODE",
                        "SECTION",
                        "BM (T-m)",
                        "SF (T)",
                        "BM (T-m)",
                        "SF (T)"));
                    list.Add(string.Format("------------------------------------------------------------------------------------------"));

                    for (int i = 0; i < ls_BM.Count; i++)
                    {
                        //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
                        //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

                        list.Add(string.Format(format,
                            ls_BM[i].NodeNo,
                            cont_sections[i],
                            ls_BM[i].Force,
                            ls_BM[i].Stress,
                            //ls_SF[i].Force,
                            ls_BM[i].Force * 1.15,
                            ls_SF[i].Force * 1.15));

                    }
                    list.Add(string.Format("------------------------------------------------------------------------------------------"));
                    list.Add(string.Format(""));

                    #endregion Print Reports
                    list.Add(string.Format(""));


                    #endregion DS_3_CONTINUOUS_SPANS
                }




                #endregion Chiranjit [2013 12 12]



                #region DS_3_CONTINUOUS_SPANS

            //    ls_BM.Clear();
            //    ls_SF.Clear();

            //    foreach (var item in cont_list)
            //    {
            //        mfrc = DS_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true);
            //        ls_BM.Add(mfrc);
            //        mfrc = DS_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true);
            //        ls_SF.Add(mfrc);
            //    }

            //    list.Add(string.Format(""));
            //    list.Add(string.Format("------------------------------------------------------------------------"));
            //    list.Add(string.Format("MAX BENDING MOMENTS AND SHEAR FORCES SUMMARY DUE TO DIFFERENTIAL SINKING"));
            //    list.Add(string.Format("------------------------------------------------------------------------"));
            //    //list.Add(string.Format("DS_3_CONTINUOUS_SPANS"));
            //    list.Add(string.Format(""));
            //    #region Print Reports
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));
            //    list.Add(string.Format(format,
            //    "",
            //        "",
            //        "",
            //"",
            //"(INCREASE 15% ",
            //"FOR DISTORTION"));

            //    list.Add(string.Format(format,
            //    "",
            //    "",
            //    "",
            //    "",
            //"WARPING   &  ",
            //"INCIDENTAL LOADS)"));

            //    list.Add(string.Format(format,
            //    "DESIGN",
            //        "DESIGN",
            //        "SINK LOAD",
            //"SINK LOAD",
            //"SINK LOAD",
            //"SINK LOAD"));

            //    list.Add(string.Format(format,
            //    " NODE",
            //        "SECTION",
            //        "BM (T-m)",
            //        "SF (T)",
            //        "BM (T-m)",
            //        "SF (T)"));
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));

            //    for (int i = 0; i < ls_BM.Count; i++)
            //    {
            //        //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
            //        //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

            //        list.Add(string.Format(format,
            //            ls_BM[i].NodeNo,
            //            cont_sections[i],
            //            ls_BM[i].Force,
            //            ls_SF[i].Force,
            //            ls_BM[i].Force * 1.15,
            //            ls_SF[i].Force * 1.15));

            //    }
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));

            //    #endregion Print Reports

                #endregion DS_3_CONTINUOUS_SPANS
                list.Add(string.Format(""));
            }
            if (FPLL_3_CONTINUOUS_SPANS != null)
            {

                #region Chiranjit [2013 12 12]


                int tot_load_case = 7;

                    list.Add(string.Format(""));
                    list.Add(string.Format(""));
                    list.Add(string.Format("----------------------------------------------------------------------------------------"));
                    list.Add(string.Format("BENDING MOMENT AND SHEAR FORCE SUMMARY - CONTINUOUS STAGE  (DUE TO FOOTPATH LIVE LOAD)"));
                    list.Add(string.Format("----------------------------------------------------------------------------------------"));
                    list.Add(string.Format(""));

                for (int lc = 1; lc <= tot_load_case; lc++)
                {
                    #region FPLL_3_CONTINUOUS_SPANS

                    ls_BM.Clear();
                    ls_SF.Clear();


                    foreach (var item in cont_list)
                    {
                        //mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, false);


                        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, lc);
                        ls_BM.Add(mfrc);
                        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, lc);
                        ls_SF.Add(mfrc);
                    }


                    list.Add(string.Format("-----------------------------------------------------------------------------------"));
                    list.Add(string.Format("MAX BM AND CORRESPONDING SF SUMMARY DUE TO FOOTPATH LIVE LOAD AT LOAD CASE {0}", lc));
                    list.Add(string.Format("-----------------------------------------------------------------------------------"));
                    #region Print Reports
                    list.Add(string.Format("------------------------------------------------------------------------------------------"));
                    list.Add(string.Format(format,
                    "",
                        "",
                        "",
                "",
                "(INCREASE 15% ",
                "FOR DISTORTION"));

                    list.Add(string.Format(format,
                    "",
                    "",
                    "",
                    "",
                "WARPING   &  ",
                "INCIDENTAL LOADS)"));

                    list.Add(string.Format(format,
                    "DESIGN",
                        "DESIGN",
                        "FPLL LOAD",
                "FPLL LOAD",
                "FPLL LOAD",
                "FPLL LOAD"));

                    list.Add(string.Format(format,
                    " NODE",
                        "SECTION",
                        "BM (T-m)",
                        "SF (T)",
                        "BM (T-m)",
                        "SF (T)"));
                    list.Add(string.Format("------------------------------------------------------------------------------------------"));

                    for (int i = 0; i < ls_BM.Count; i++)
                    {
                        //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
                        //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

                        list.Add(string.Format(format,
                            ls_BM[i].NodeNo,
                            cont_sections[i],
                            -ls_BM[i].Force,
                            -ls_BM[i].Stress,
                            //ls_SF[i].Force,
                            -ls_BM[i].Force * 1.15,
                            ls_SF[i].Force * 1.15));

                    }
                    list.Add(string.Format("------------------------------------------------------------------------------------------"));
                    list.Add(string.Format(""));

                    #endregion Print Reports
                    list.Add(string.Format(""));

                    
                    #endregion FPLL_3_CONTINUOUS_SPANS
                }

                


                #endregion Chiranjit [2013 12 12]

                #region FPLL_3_CONTINUOUS_SPANS

                //    ls_BM.Clear();
            //    ls_SF.Clear();

            //    ls_BM_Negative.Clear();
            //    ls_SF_Negative.Clear();

            //    list.Add(string.Format(""));
            //    list.Add(string.Format(""));
            //    list.Add(string.Format("----------------------------------------------------------------------------------------"));
            //    list.Add(string.Format("BENDING MOMENT AND SHEAR FORCE SUMMARY - CONTINUOUS STAGE  (DUE TO FOOTPATH LIVE LOAD)"));
            //    list.Add(string.Format("----------------------------------------------------------------------------------------"));
            //    list.Add(string.Format(""));


            //    foreach (var item in cont_list)
            //    {

            //        //mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, false);
            //        //ls_BM.Add(mfrc);
            //        //mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true, false);
            //        //ls_SF.Add(mfrc);


            //        //mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, true);
            //        //ls_BM_Negative.Add(mfrc);
            //        //mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true, true);
            //        //ls_SF_Negative.Add(mfrc);

            //        //Chiranjit [2013 08 04]
            //        //According to the excel program
            //        //Negative(-) Value should be Positive(+)
            //        //Positive(+) Value should be Negative(-)

            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, false);


            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, 2);
            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, 3);
            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, 4);
            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, 5);

            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(8, 5);

            //        mfrc.Force = -mfrc.Force;
            //        ls_BM_Negative.Add(mfrc);
            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true, false);
            //        mfrc.Force = -mfrc.Force;
            //        ls_SF_Negative.Add(mfrc);


            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_MomentForce(item, true, true);
            //        mfrc.Force = -mfrc.Force;
            //        ls_BM.Add(mfrc);
            //        mfrc = FPLL_3_CONTINUOUS_SPANS.GetJoint_ShearForce(item, true, true);
            //        mfrc.Force = -mfrc.Force;
            //        ls_SF.Add(mfrc);
            //    }

               
            //    //list.Add(string.Format("FPLL_3_CONTINUOUS_SPANS"));
            //    //for (int i = 0; i < ls_BM.Count; i++)
            //    //{
            //    //    list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3}",
            //    //        ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

            //    //}
            //    list.Add(string.Format("------------------------------------------------------------------------------"));
            //    list.Add(string.Format("MAX POSITIVE (+VE) BM AND CORRESPONDING SF SUMMARY DUE TO FOOTPATH LIVE LOAD"));
            //    list.Add(string.Format("------------------------------------------------------------------------------"));
            //    #region Print Reports
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));
            //    list.Add(string.Format(format,
            //    "",
            //        "",
            //        "",
            //"",
            //"(INCREASE 15% ",
            //"FOR DISTORTION"));

            //    list.Add(string.Format(format,
            //    "",
            //    "",
            //    "",
            //    "",
            //"WARPING   &  ",
            //"INCIDENTAL LOADS)"));

            //    list.Add(string.Format(format,
            //    "DESIGN",
            //        "DESIGN",
            //        "FPLL LOAD",
            //"FPLL LOAD",
            //"FPLL LOAD",
            //"FPLL LOAD"));

            //    list.Add(string.Format(format,
            //    " NODE",
            //        "SECTION",
            //        "BM (T-m)",
            //        "SF (T)",
            //        "BM (T-m)",
            //        "SF (T)"));
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));

            //    for (int i = 0; i < ls_BM.Count; i++)
            //    {
            //        //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
            //        //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

            //        list.Add(string.Format(format,
            //            ls_BM[i].NodeNo,
            //            cont_sections[i],
            //            ls_BM[i].Force,
            //            ls_SF[i].Force,
            //            ls_BM[i].Force * 1.15,
            //            ls_SF[i].Force * 1.15));

            //    }
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));
            //    list.Add(string.Format(""));

            //    #endregion Print Reports
            //    list.Add(string.Format(""));

            //    list.Add(string.Format("------------------------------------------------------------------------------"));
            //    list.Add(string.Format("MAX NEGATIVE (-VE) BM AND CORRESPONDING SF SUMMARY DUE TO FOOTPATH LIVE LOAD"));
            //    list.Add(string.Format("------------------------------------------------------------------------------"));
            //    list.Add(string.Format(""));
            //    #region Print Reports
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));
            //    list.Add(string.Format(format,
            //    "",
            //        "",
            //        "",
            //"",
            //"(INCREASE 15% ",
            //"FOR DISTORTION"));

            //    list.Add(string.Format(format,
            //    "",
            //    "",
            //    "",
            //    "",
            //"WARPING   &  ",
            //"INCIDENTAL LOADS)"));

            //    list.Add(string.Format(format,
            //    "DESIGN",
            //        "DESIGN",
            //        "FPLL LOAD",
            //"FPLL LOAD",
            //"FPLL LOAD",
            //"FPLL LOAD"));

            //    list.Add(string.Format(format,
            //    " NODE",
            //        "SECTION",
            //        "BM (T-m)",
            //        "SF (T)",
            //        "BM (T-m)",
            //        "SF (T)"));
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));

            //    for (int i = 0; i < ls_BM.Count; i++)
            //    {
            //        //list.Add(string.Format("{0,-10} {1,15:f3} {2,15:f3} {1,15:f3} ",
            //        //    ls_BM[i].NodeNo, ls_BM[i].Force, ls_SF[i].Force));

            //        list.Add(string.Format(format,
            //            ls_BM[i].NodeNo,
            //            cont_sections[i],
            //            ls_BM_Negative[i].Force,
            //            ls_SF_Negative[i].Force,
            //            ls_BM_Negative[i].Force * 1.15,
            //            ls_SF_Negative[i].Force * 1.15));

            //    }
            //    list.Add(string.Format("------------------------------------------------------------------------------------------"));

                //    #endregion Print Reports

                #endregion FPLL_3_CONTINUOUS_SPANS
                list.Add(string.Format(""));
            }


            if (Temperature_Section != null)
            {

                #region Chiranjit [2013 12 12]

                Calculate_Section_Area_Yt_Yb_Zt_Zb();

                List<int> des_node = MyList.Get_Array_Intiger(cont_joints);
              
                #region Temperature_Section

                ls_BM.Clear();
                ls_SF.Clear();

                ls_BM_Negative.Clear();
                ls_SF_Negative.Clear();

                List<int> lst_jnt = MyList.Get_Array_Intiger("1 TO 24");
                double bm = 0;

                mfrc = Temperature_Section.GetJoint_MomentForce(lst_jnt, 2);

                bm = mfrc.Force;

                mfrc = Temperature_Section.GetJoint_MomentForce(lst_jnt, 3);

                if (Math.Abs(bm) < Math.Abs(mfrc.Force))
                {
                    bm = mfrc.Force;
                }

                for (int i = 0; i < lst_Zb.Count; i++)
                {
                    ls_BM.Add(-bm * 10000.0 / (lst_Zt[i] * 10000.0));
                    ls_SF.Add(bm * 1000.0 / (lst_Zb[i] * 10000.0));
                }

                mfrc = Temperature_Section.GetJoint_MomentForce(lst_jnt, 4);
                bm = mfrc.Force;

                mfrc = Temperature_Section.GetJoint_MomentForce(lst_jnt, 5);

                if (Math.Abs(bm) < Math.Abs(mfrc.Force))
                {
                    bm = mfrc.Force;
                }

                for (int i = 0; i < lst_Zb.Count; i++)
                {
                    ls_BM_Negative.Add(bm * 10000.0 / (lst_Zt[i] * 10000.0));
                    ls_SF_Negative.Add(bm * 1000.0 / (lst_Zb[i] * 10000.0));
                }

                #region Print Reports
                list.Add(string.Format("--------------------------------"));
                list.Add(string.Format("STRESSES DUE TO TEMPERATURE RISE"));
                list.Add(string.Format("--------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------------------------------------"));
                list.Add(string.Format("DESIGN   DESIGN                       FT             FB"));
                list.Add(string.Format(" NODE    SECTION                  (KG/sq.cm)     (KG/sq.cm)"));
                list.Add(string.Format("----------------------------------------------------------------"));
                for (int i = 0; i < ls_BM.Count; i++)
                {
                    list.Add(string.Format(" {0,-5}   {1,-20} {2,11:f3} {3,15:f3}", des_node[i], cont_sections[i],ls_BM[i], ls_SF[i]));
                }
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("--------------------------------"));
                list.Add(string.Format("STRESSES DUE TO TEMPERATURE FALL"));
                list.Add(string.Format("--------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------------------------------------"));
                list.Add(string.Format("DESIGN   DESIGN                       FT             FB"));
                list.Add(string.Format(" NODE    SECTION                  (KG/sq.cm)     (KG/sq.cm)"));
                list.Add(string.Format("----------------------------------------------------------------"));
                for (int i = 0; i < ls_BM_Negative.Count; i++)
                {
                    list.Add(string.Format(" {0,-5}   {1,-20} {2,11:f3} {3,15:f3}", des_node[i], cont_sections[i], ls_BM_Negative[i], ls_SF_Negative[i]));
                }
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format("------------------------------------------------------------------------------------------"));
                list.Add(string.Format(""));

                #endregion Print Reports
                list.Add(string.Format(""));


                #endregion FPLL_3_CONTINUOUS_SPANS

                #endregion Chiranjit [2013 12 12]
            }

            List<LiveLoad_Forces_Result_Data> ll_1 = new List<LiveLoad_Forces_Result_Data>();
            List<LiveLoad_Forces_Result_Data> ll_2 = new List<LiveLoad_Forces_Result_Data>();



            List<LiveLoad_Forces_Result_Data> ll_3 = new List<LiveLoad_Forces_Result_Data>();
            List<LiveLoad_Forces_Result_Data> ll_4 = new List<LiveLoad_Forces_Result_Data>();


            BeamMemberForce bmf;
            LiveLoad_Forces_Result_Data ll_frd;



            List<List<LiveLoad_Forces_Result_Data>> ll_BBM_Positive = new List<List<LiveLoad_Forces_Result_Data>>();
            List<List<LiveLoad_Forces_Result_Data>> ll_BBM_Negative = new List<List<LiveLoad_Forces_Result_Data>>();

            List<List<LiveLoad_Forces_Result_Data>> ll_SSF_Positive = new List<List<LiveLoad_Forces_Result_Data>>();
            List<List<LiveLoad_Forces_Result_Data>> ll_SSF_Negative = new List<List<LiveLoad_Forces_Result_Data>>();


            List<int> ll_jnt = new List<int>();
            iApp.Progress_Works.Clear();
            for (int i = 0; i < LL_All_Analysis.Count; i++)
            {
                iApp.Progress_Works.Add("Read Live Load Results from " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)));
            }
            if (LL_All_Analysis != null)
            {
                #region LL_3_CONTINUOUS_SPANS

                for (int i = 0; i < LL_All_Analysis.Count; i++)
                {

                    ll_1 = new List<LiveLoad_Forces_Result_Data>();
                    ll_2 = new List<LiveLoad_Forces_Result_Data>();
                    ll_3 = new List<LiveLoad_Forces_Result_Data>();
                    ll_4 = new List<LiveLoad_Forces_Result_Data>();

                    iApp.Progress_ON("Read Live Load Results");
                    for (int j = 0; j < cont_list.Count; j++)
                    {
                        var item = cont_list[j];

                        ll_frd = new LiveLoad_Forces_Result_Data();

                        ll_frd.DesignNode = ll_joints[item - 1];

                        ll_jnt.Add(ll_frd.DesignNode);

                        bmf = LL_All_Analysis[i].GetJoint_MaxBendingMoment_Corrs_ShearForce(ll_jnt, true, false); //Positive

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        bmf = LL_All_Analysis[i].GetJoint_MinimumBendingMoment_Corrs_ShearForce(ll_jnt, true, false); //Positive

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        ll_1.Add(ll_frd);




                        ll_frd = new LiveLoad_Forces_Result_Data();

                        ll_frd.DesignNode = ll_joints[item - 1];


                        bmf = LL_All_Analysis[i].GetJoint_MaxBendingMoment_Corrs_ShearForce(ll_jnt, true, true); //Negative

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        bmf = LL_All_Analysis[i].GetJoint_MinimumBendingMoment_Corrs_ShearForce(ll_jnt, true, true); //Negative

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        ll_2.Add(ll_frd);



                        ll_frd = new LiveLoad_Forces_Result_Data();

                        ll_frd.DesignNode = ll_joints[item - 1];


                        bmf = LL_All_Analysis[i].GetJoint_MaxShearForce_Corrs_BendingMoment(ll_jnt, true, false); //Positive

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        bmf = LL_All_Analysis[i].GetJoint_MinimumShearForce_Corrs_BendingMoment(ll_jnt, true, false); //Positive

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        ll_3.Add(ll_frd);




                        ll_frd = new LiveLoad_Forces_Result_Data();

                        ll_frd.DesignNode = ll_joints[item - 1];


                        bmf = LL_All_Analysis[i].GetJoint_MaxShearForce_Corrs_BendingMoment(ll_jnt, true, true); //Negative

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);


                        bmf = LL_All_Analysis[i].GetJoint_MinimumShearForce_Corrs_BendingMoment(ll_jnt, true, true); //Negative

                        ll_frd.BM.Add(bmf.MaxBendingMoment);
                        ll_frd.SF.Add(bmf.MaxShearForce);

                        ll_4.Add(ll_frd);

                        ll_jnt.Clear();
                        iApp.SetProgressValue(j, cont_list.Count);
                    }
                    ll_BBM_Positive.Add(ll_1);
                    ll_BBM_Negative.Add(ll_2);
                    ll_SSF_Positive.Add(ll_3);
                    ll_SSF_Negative.Add(ll_4);

                    iApp.Progress_OFF();

                }
                iApp.Progress_Works.Clear();

                list.Add(string.Format(""));
                //list.Add(string.Format("LL_3_CONTINUOUS_SPANS"));

                format = "{0,10} {1,15:f3} {2,15:f3}";

                string kStr = "";
                for (int i = 0; i < ll_BBM_Positive.Count; i++)
                {
                    kStr = (string.Format("MAX POSITIVE (+VE) BM AND CORRESPONDING SF SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " ")));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                    list.Add(string.Format(kStr));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));

                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    //list.Add(string.Format("MAX POSITIVE (+VE) BM AND CORRESPONDING SF SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_"," ")));
                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    list.Add(string.Format(""));

                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format("   DESIGN NODE      +VE BM        CORRES SF        "));
                    list.Add(string.Format("                     (T-m)            (T)"));
                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format(""));

                    for (int j = 0; j < ll_BBM_Positive[i].Count; j++)
                    {
                        for (int k = 0; k < ll_BBM_Positive[i][j].BM.Count; k++)
                        {
                            list.Add(string.Format(format, 
                                ll_BBM_Positive[i][j].DesignNode,
                                ll_BBM_Positive[i][j].BM[k],
                                ll_BBM_Positive[i][j].SF[k]));
                        }
                    }

                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    //list.Add(string.Format("MAX NEGATIVE (-VE) BM AND CORRESPONDING SF SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " ")));
                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));

                    kStr = string.Format("MAX NEGATIVE (-VE) BM AND CORRESPONDING SF SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " "));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                    list.Add(string.Format(kStr));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));

                    
                    
                    //list.Add(string.Format(""));

                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format("   DESIGN NODE      -VE BM        CORRES SF        "));
                    list.Add(string.Format("                     (T-m)            (T)"));
                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format(""));

                    for (int j = 0; j < ll_BBM_Negative[i].Count; j++)
                    {
                        for (int k = 0; k < ll_BBM_Negative[i][j].BM.Count; k++)
                        {
                            list.Add(string.Format(format, 
                                ll_BBM_Negative[i][j].DesignNode,
                                ll_BBM_Negative[i][j].BM[k],
                                ll_BBM_Negative[i][j].SF[k]));
                        }
                    }


                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    //list.Add(string.Format("MAX POSITIVE (+VE) SF AND CORRESPONDING BM SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " ")));
                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    //list.Add(string.Format(""));



                    kStr = string.Format("MAX POSITIVE (+VE) SF AND CORRESPONDING BM SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " "));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                    list.Add(string.Format(kStr));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));


                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format("   DESIGN NODE      +VE SF        CORRES BM        "));
                    list.Add(string.Format("                      (T)           (T-m)"));
                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format(""));

                    for (int j = 0; j < ll_SSF_Positive[i].Count; j++)
                    {
                        for (int k = 0; k < ll_SSF_Positive[i][j].BM.Count; k++)
                        {
                            list.Add(string.Format(format, 
                                ll_SSF_Positive[i][j].DesignNode,
                                ll_SSF_Positive[i][j].SF[k],
                                ll_SSF_Positive[i][j].BM[k]));
                        }
                    }


                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    //list.Add(string.Format("MAX NEGATIVE (-VE) SF AND CORRESPONDING BM SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " ")));
                    //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                    //list.Add(string.Format(""));


                    kStr = string.Format("MAX NEGATIVE (-VE) SF AND CORRESPONDING BM SUMMARY FROM " + Path.GetFileName(Path.GetDirectoryName(LL_All_Analysis[i].Analysis_File)).ToUpper().Replace("_", " "));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));
                    list.Add(string.Format(kStr));
                    list.Add(string.Format("".PadLeft(kStr.Length, '-')));


                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format("   DESIGN NODE      -VE SF        CORRES BM        "));
                    list.Add(string.Format("                      (T)           (T-m)"));
                    list.Add(string.Format("-------------------------------------------------"));
                    list.Add(string.Format(""));

                    for (int j = 0; j < ll_SSF_Negative[i].Count; j++)
                    {
                        for (int k = 0; k < ll_SSF_Negative[i][j].BM.Count; k++)
                        {
                            list.Add(string.Format(format, 
                                ll_SSF_Negative[i][j].DesignNode,
                                ll_SSF_Negative[i][j].SF[k],
                                ll_SSF_Negative[i][j].BM[k]));
                        }
                    }
                }
                #endregion LL_3_CONTINUOUS_SPANS
            }
            File.WriteAllLines(File_Forces_Results, list.ToArray());
            return list;
            #endregion Chiranjit [2013 07 26]


        }
        public List<string> Get_Transverse_Analysis_Result()
        {
            List<string> list = new List<string>();

            string cant_mems = "22 24";
            string deck_mems = "6 TO 9";
            string soffit_mems = "16 TO 19";
            string left_web_mems = "2 TO 3";
            string right_web_mems = "13  12";


            string format = "{0,-15} {1,9} {2,9} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3} {9,10:f3}";

          
            AstraBeamMember abm;

            List<AstraBeamMember> lst_cant_mems = new List<AstraBeamMember>();
            List<AstraBeamMember> lst_deck_mems = new List<AstraBeamMember>();
            List<AstraBeamMember> lst_soffit_mems = new List<AstraBeamMember>();
            List<AstraBeamMember> lst_left_web_mems = new List<AstraBeamMember>();
            List<AstraBeamMember> lst_right_web_mems = new List<AstraBeamMember>();


            List<double> lst_pos_temp = new List<double>();
            List<double> lst_neg_temp = new List<double>();

            List<double> lst_pos_cwll = new List<double>();
            List<double> lst_neg_cwll = new List<double>();

            List<int> mems = null;
            List<int> jnts = new List<int>();


            double dval = 0.0;
            int i = 0;
            int j = 0;

            List<int> load_cwll = MyList.Get_Array_Intiger("4 TO 11");


            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                     MEMB      NODE      DL BM    SIDL BM    FPLL BM     TEMPERATURE (t-m)       CWLL BM (t-m)"));
            list.Add(string.Format("                      NO        NO       (t-m)     (t-m)      (t-m)      +VE BM     -VE BM     +VE BM     -VE BM "));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            

            #region Cantilever
            mems = MyList.Get_Array_Intiger(cant_mems);
            abm = new AstraBeamMember(new ReadForceType());
            for (i = 0; i < mems.Count; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    abm = Transverse_Section.Get_BeamMember_Force(mems[i], j);
                    lst_cant_mems.Add(abm);
                }
                jnts.Clear();
                jnts.Add(abm.StartNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);

                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);




                jnts.Clear();
                jnts.Add(abm.EndNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);


                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);





            }
            int temp_incr = -1;
            for (i = 0; i < lst_cant_mems.Count; i += 3)
            {

                temp_incr++;
                list.Add(string.Format(format,
                    (i == 0 ? "CANTILEVER SLAB" : ""),
                    lst_cant_mems[i].BeamNo,
                    lst_cant_mems[i].StartNodeForce.JointNo,
                    lst_cant_mems[i].StartNodeForce.MaxBendingMoment,
                    lst_cant_mems[i + 1].StartNodeForce.MaxBendingMoment,
                    lst_cant_mems[i + 2].StartNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));
                temp_incr++;
                list.Add(string.Format(format,
                    "",
                    "",
                    lst_cant_mems[i].EndNodeForce.JointNo,
                    lst_cant_mems[i].EndNodeForce.MaxBendingMoment,
                    lst_cant_mems[i + 1].EndNodeForce.MaxBendingMoment,
                    lst_cant_mems[i + 2].EndNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));
            }

            #endregion Cantilever
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));

            lst_pos_temp.Clear();
            lst_neg_temp.Clear();
            lst_pos_cwll.Clear();
            lst_neg_cwll.Clear();
            #region Deck Slab
            mems = MyList.Get_Array_Intiger(deck_mems);
            for (i = 0; i < mems.Count; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    abm = Transverse_Section.Get_BeamMember_Force(mems[i], j);
                    lst_deck_mems.Add(abm);
                }
                jnts.Clear();
                jnts.Add(abm.StartNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);



                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);




                jnts.Clear();
                jnts.Add(abm.EndNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);


                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);

            }
            temp_incr = -1;
            for (i = 0; i < lst_deck_mems.Count; i += 3)
            {

                temp_incr++;
                list.Add(string.Format(format,
                    (i == 0 ? "DECK SLAB" : ""),
                    lst_deck_mems[i].BeamNo,
                    lst_deck_mems[i].StartNodeForce.JointNo,
                    lst_deck_mems[i].StartNodeForce.MaxBendingMoment,
                    lst_deck_mems[i + 1].StartNodeForce.MaxBendingMoment,
                    lst_deck_mems[i + 2].StartNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));

                temp_incr++;
                list.Add(string.Format(format,
                    "",
                    "",
                    lst_deck_mems[i].EndNodeForce.JointNo,
                    lst_deck_mems[i].EndNodeForce.MaxBendingMoment,
                    lst_deck_mems[i + 1].EndNodeForce.MaxBendingMoment,
                    lst_deck_mems[i + 2].EndNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));
            }

            #endregion Deck Slab
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));

            lst_pos_temp.Clear();
            lst_neg_temp.Clear();
            lst_pos_cwll.Clear();
            lst_neg_cwll.Clear();
            #region Soffit
            mems = MyList.Get_Array_Intiger(soffit_mems);
            for (i = 0; i < mems.Count; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    abm = Transverse_Section.Get_BeamMember_Force(mems[i], j);
                    lst_soffit_mems.Add(abm);
                }
                jnts.Clear();
                jnts.Add(abm.StartNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);


                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);



                jnts.Clear();
                jnts.Add(abm.EndNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);


                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);


            }
            temp_incr = -1;
            for (i = 0; i < lst_soffit_mems.Count; i += 3)
            {

                temp_incr++;
                list.Add(string.Format(format,
                    (i == 0 ? "SOFFIT" : ""),
                    lst_soffit_mems[i].BeamNo,
                    lst_soffit_mems[i].StartNodeForce.JointNo,
                    lst_soffit_mems[i].StartNodeForce.MaxBendingMoment,
                    lst_soffit_mems[i + 1].StartNodeForce.MaxBendingMoment,
                    lst_soffit_mems[i + 2].StartNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));

                temp_incr++;
                list.Add(string.Format(format,
                    "",
                    "",
                    lst_soffit_mems[i].EndNodeForce.JointNo,
                    lst_soffit_mems[i].EndNodeForce.MaxBendingMoment,
                    lst_soffit_mems[i + 1].EndNodeForce.MaxBendingMoment,
                    lst_soffit_mems[i + 2].EndNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));
            }
            #endregion Soffit
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));

            lst_pos_temp.Clear();
            lst_neg_temp.Clear();
            lst_pos_cwll.Clear();
            lst_neg_cwll.Clear();
            #region Left Web
            mems = MyList.Get_Array_Intiger(left_web_mems);
            for (i = 0; i < mems.Count; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    abm = Transverse_Section.Get_BeamMember_Force(mems[i], j);
                    lst_left_web_mems.Add(abm);
                }
                jnts.Clear();
                jnts.Add(abm.StartNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);



                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);



                jnts.Clear();
                jnts.Add(abm.EndNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);

                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);

            }
            temp_incr = -1;
            for (i = 0; i < lst_left_web_mems.Count; i += 3)
            {

                temp_incr++;
                list.Add(string.Format(format,
                    (i == 0 ? "LEFT WEB" : ""),
                    lst_left_web_mems[i].BeamNo,
                    lst_left_web_mems[i].StartNodeForce.JointNo,
                    lst_left_web_mems[i].StartNodeForce.MaxBendingMoment,
                    lst_left_web_mems[i + 1].StartNodeForce.MaxBendingMoment,
                    lst_left_web_mems[i + 2].StartNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));

                temp_incr++;
                list.Add(string.Format(format,
                    "",
                    "",
                    lst_left_web_mems[i].EndNodeForce.JointNo,
                    lst_left_web_mems[i].EndNodeForce.MaxBendingMoment,
                    lst_left_web_mems[i + 1].EndNodeForce.MaxBendingMoment,
                    lst_left_web_mems[i + 2].EndNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));
            }
            #endregion Left Web
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));

            lst_pos_temp.Clear();
            lst_neg_temp.Clear();
            lst_pos_cwll.Clear();
            lst_neg_cwll.Clear();
            #region Right Web
            mems = MyList.Get_Array_Intiger(right_web_mems);
            for (i = 0; i < mems.Count; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    abm = Transverse_Section.Get_BeamMember_Force(mems[i], j);
                    lst_right_web_mems.Add(abm);
                }
                jnts.Clear();
                jnts.Add(abm.StartNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);



                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);



                jnts.Clear();
                jnts.Add(abm.EndNodeForce.JointNo);
                dval = Temperature_Section.GetJoint_Max_Hogging(jnts, true);
                lst_pos_temp.Add(dval);

                dval = Temperature_Section.GetJoint_Max_Sagging(jnts, true);
                lst_neg_temp.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Hogging(jnts, load_cwll);
                lst_pos_cwll.Add(dval);
                dval = Transverse_Section.GetJoint_Max_Sagging(jnts, load_cwll);
                lst_neg_cwll.Add(dval);

            }
            temp_incr = -1;
            for (i = 0; i < lst_right_web_mems.Count; i += 3)
            {
                temp_incr++;
                list.Add(string.Format(format,
                    (i == 0 ? "RIGHT WEB" : ""),
                    lst_right_web_mems[i].BeamNo,
                    lst_right_web_mems[i].StartNodeForce.JointNo,
                    lst_right_web_mems[i].StartNodeForce.MaxBendingMoment,
                    lst_right_web_mems[i + 1].StartNodeForce.MaxBendingMoment,
                    lst_right_web_mems[i + 2].StartNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));

                temp_incr++;
                list.Add(string.Format(format,
                    "",
                    "",
                    lst_right_web_mems[i].EndNodeForce.JointNo,
                    lst_right_web_mems[i].EndNodeForce.MaxBendingMoment,
                    lst_right_web_mems[i + 1].EndNodeForce.MaxBendingMoment,
                    lst_right_web_mems[i + 2].EndNodeForce.MaxBendingMoment,
                    lst_pos_temp[temp_incr],
                    lst_neg_temp[temp_incr],
                    lst_pos_cwll[temp_incr],
                    lst_neg_cwll[temp_incr]
                    ));
            }
            #endregion Right Web
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));




            


            File.WriteAllLines(File_Transverse_Results, list.ToArray());

            return list;
        }
        public string File_Forces_Results { get { return Path.Combine(working_folder, "FORCES_RESULT.TXT"); } }
        public string File_Transverse_Results { get { return Path.Combine(working_folder, "TRANSVERSE_RESULT.TXT"); } }


        public List<string> Get_Section_Properties_Summary()
        {
            List<string> list = new List<string>();
            list.Add("");
            list.Add("--------------------------------------------------------------------------");
            list.Add(string.Format("CALCULATION OF SECTION PROPERTIES OF {0} M SPAN", L1));
            list.Add("--------------------------------------------------------------------------");


            List<double> lst_A = new List<double>();
            List<double> lst_Ix = new List<double>();
            List<double> lst_Yt = new List<double>();
            List<double> lst_Yb = new List<double>();

            list.Add("");
            for (int i = 0; i < End_Span_Sections.Count; i++)
            {
                lst_A.Add(End_Span_Sections[i].Total_Area);
                lst_Ix.Add(End_Span_Sections[i].I);
                lst_Yt.Add(End_Span_Sections[i].Yt);
                lst_Yb.Add(End_Span_Sections[i].Yb);
                if (i == 0)
                {
                    list.Add(string.Format("--------------------------------------------------------------"));
                    list.Add(string.Format("CALCULATION OF SECTION PROPERTIES AT SUPPORT"));
                    list.Add(string.Format("--------------------------------------------------------------"));
                }
                else
                {
                    list.Add(string.Format("--------------------------------------------------------------"));
                    list.Add(string.Format("CALCULATION OF SECTION PROPERTIES AT {0} L1/10", i));
                    list.Add(string.Format("--------------------------------------------------------------"));
                }
                list.Add("");
                list.AddRange(End_Span_Sections[i].Result_Output.ToArray());
                list.Add("");
            }


            //string format = "{0,10:f4} {1,10:f4} {2,10:f4} {3,10:f4} {4,10:f4}";



            //for (int i = 0; i < lst_A.Count; i++)
            //{
            //    list.Add(string.Format(format, "", lst_A[i], lst_Ix[i], lst_Yt[i], lst_Yb[i]));

            //}


            list.Add("--------------------------------------------------------------------------");
            list.Add(string.Format("CALCULATION OF SECTION PROPERTIES OF {0} M SPAN", L2));
            list.Add("--------------------------------------------------------------------------");
            list.Add("");


            lst_A.Clear();
            lst_Ix.Clear();
            lst_Yt.Clear();
            lst_Yb.Clear();
           


            for (int i = 0; i < Mid_Span_Sections.Count; i++)
            {


                lst_A.Add(Mid_Span_Sections[i].Total_Area);
                lst_Ix.Add(Mid_Span_Sections[i].I);
                lst_Yt.Add(Mid_Span_Sections[i].Yt);
                lst_Yb.Add(Mid_Span_Sections[i].Yb);
                if (i == 0)
                {
                    list.Add(string.Format("--------------------------------------------------------------"));
                    list.Add(string.Format("CALCULATION OF SECTION PROPERTIES AT SUPPORT"));
                    list.Add(string.Format("--------------------------------------------------------------"));
                }
                else
                {
                    list.Add(string.Format("--------------------------------------------------------------"));
                    list.Add(string.Format("CALCULATION OF SECTION PROPERTIES AT {0} L2/20", i));
                    list.Add(string.Format("--------------------------------------------------------------"));
                }
               
                list.AddRange(Mid_Span_Sections[i].Result_Output.ToArray());
                list.Add("");

            }
            return list;
        }

        private void Get_End_Span_Load_Data()
        {

            List<string> list = new List<string>();

            list.Add(string.Format("2 UNI GY -21.54375"));
            list.Add(string.Format("3 UNI GY -21.54375"));
            list.Add(string.Format("4 UNI GY -21.54375"));
            list.Add(string.Format("5 UNI GY -21.54375"));
            list.Add(string.Format("6 TRAP GY -21.54375 -18.3235"));
            list.Add(string.Format("7 UNI GY -18.3235 "));
            list.Add(string.Format("8 UNI GY -18.3235 "));
            list.Add(string.Format("9 TRAP GY -18.3235 -17.479"));
            list.Add(string.Format("10 TRAP GY -17.479 -15.1875"));
            list.Add(string.Format("11 UNI GY -15.1875 "));
            list.Add(string.Format("12 UNI GY -15.1875 "));
            list.Add(string.Format("13 UNI GY -15.1875 "));
            list.Add(string.Format("14 UNI GY -15.1875 "));
            list.Add(string.Format("15 TRAP GY -15.1875 -17.479"));
            list.Add(string.Format("16 TRAP GY -17.479        -18.3235"));
            list.Add(string.Format("17 UNI GY -18.3235 "));
            list.Add(string.Format("18 UNI GY -18.3235 "));
            list.Add(string.Format("19 TRAP GY -18.3235 -21.54375"));
            list.Add(string.Format("20 UNI GY -21.54375 "));
            list.Add(string.Format("21 UNI GY -21.54375 "));
            list.Add(string.Format("22 UNI GY -21.54375"));
        }

        public List<Forces_Results> FRC_Results { get; set; }


        public Forces_Results FRC_SPAN_1
        {
            get
            {
                return FRC_Results[0];
            }
        }
        public Forces_Results FRC_SPAN_2
        {
            get
            {
                return FRC_Results[1];
            }
        }
        public Forces_Results FRC_DL
        {
            get
            {
                return FRC_Results[2];
            }
        }
        public Forces_Results FRC_SIDL
        {
            get
            {
                return FRC_Results[3];
            }
        }
        public Forces_Results FRC_SINK
        {
            get
            {
                return FRC_Results[4];
            }
        }
        public Forces_Results FRC_POSITIVE_FPLL
        {
            get
            {
                return FRC_Results[5];
            }
        }
        public Forces_Results FRC_NEGATIVE_FPLL
        {
            get
            {
                return FRC_Results[6];
            }
        }
        public Forces_Results FRC_POSITIVE_LL
        {
            get
            {
                return FRC_Results[7];
            }
        }
        public Forces_Results FRC_NEGATIVE_LL
        {
            get
            {
                return FRC_Results[8];
            }
        }
        public void Read_All_Forces(string[] list)
        {

            FRC_Results = new List<Forces_Results>();

            string kStr = "";


            Forces_Results frc = new Forces_Results();
            bool flag = false;
            Forces_Result_Data frd = null;

            MyList mlist = null;

            bool _is_SF_BM = false;
            for (int i = 0; i < list.Length; i++)
            {
                kStr = list[i];
                if (kStr == "" || kStr.Contains("-----------")) 
                    continue;

                if (kStr.StartsWith("SPAN 1 (SIMPLY SUPPORTED)"))
                {
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("SPAN 2 (SIMPLY SUPPORTED)"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.EndsWith("(DUE TO DEAD LOAD)"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.EndsWith("(DUE TO SUPERIMPOSED DEAD LOAD)"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX BENDING MOMENTS AND SHEAR FORCES SUMMARY DUE TO DIFFERENTIAL SINKING"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX BM AND CORRESPONDING SF SUMMARY DUE TO DIFFERENTIAL SINKING AT LOAD CASE"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX POSITIVE (+VE) BM AND CORRESPONDING SF SUMMARY DUE TO FOOTPATH LIVE LOAD"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX NEGATIVE (-VE) BM AND CORRESPONDING SF SUMMARY DUE TO FOOTPATH LIVE LOAD"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX BM AND CORRESPONDING SF SUMMARY DUE TO FOOTPATH LIVE LOAD AT LOAD CASE"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }

                else if (kStr.StartsWith("STRESSES DUE TO TEMPERATURE RISE"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("STRESSES DUE TO TEMPERATURE FALL"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX POSITIVE (+VE) BM AND CORRESPONDING SF SUMMARY DUE TO MOVING LIVE LOAD"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX NEGATIVE (-VE) BM AND CORRESPONDING SF SUMMARY DUE TO MOVING LIVE LOAD"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX POSITIVE (+VE) BM AND CORRESPONDING SF SUMMARY"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    _is_SF_BM = false;
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX NEGATIVE (-VE) BM AND CORRESPONDING SF SUMMARY"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    _is_SF_BM = false;
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX POSITIVE (+VE) SF AND CORRESPONDING BM SUMMARY"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    _is_SF_BM = true;
                    flag = true; continue;
                }
                else if (kStr.StartsWith("MAX NEGATIVE (-VE) SF AND CORRESPONDING BM SUMMARY"))
                {
                    FRC_Results.Add(frc);
                    frc = new Forces_Results();
                    _is_SF_BM = true;
                    flag = true; continue;
                }

                if (flag)
                {
                    kStr = list[i].Trim().TrimEnd().TrimStart().Replace("   ", ",");

                    while (kStr.Contains(",,")) kStr = kStr.Replace(",,", ",");

                    mlist = new MyList(kStr, ',');

                    if (mlist.Count == 6)
                    {
                        if (mlist.IsInt(0))
                        {
                            frd = new Forces_Result_Data();
                            frd.DesignNode = mlist.GetInt(0);
                            frd.DesignSection = mlist[1];
                            frd.BM = mlist.GetDouble(2);
                            frd.SF = mlist.GetDouble(3);
                            frd.BM_Incr = mlist.GetDouble(4);
                            frd.SF_Incr = mlist.GetDouble(5);

                            frc.Add(frd);
                        }
                    }
                    else if (mlist.Count == 4)
                    {
                        if (mlist.IsInt(0))
                        {
                            frd = new Forces_Result_Data();
                            frd.DesignNode = mlist.GetInt(0);
                            frd.DesignSection = mlist[1];
                            frd.BM = mlist.GetDouble(2);
                            frd.SF = mlist.GetDouble(3);
                            frc.Add(frd);
                        }
                    }
                    else if (mlist.Count == 3)
                    {
                        if (mlist.IsInt(0))
                        {
                            frd = new Forces_Result_Data();
                            frd.DesignNode = mlist.GetInt(0);

                            if (_is_SF_BM)
                            {
                                frd.SF = mlist.GetDouble(1);
                                frd.BM = mlist.GetDouble(2);
                            }
                            else
                            {
                                frd.BM = mlist.GetDouble(1);
                                frd.SF = mlist.GetDouble(2);
                            }
                            frc.Add(frd);
                        }
                    }
                }
            }
            FRC_Results.Add(frc);


        }


        #region Area  Yt Yb Zt Zb
        List<double> lst_Area = new List<double>();
        List<double> lst_Yt = new List<double>();
        List<double> lst_Yb = new List<double>();
        List<double> lst_Zt = new List<double>();
        List<double> lst_Zb = new List<double>();
        #endregion Area  Yt Yb Zt Zb

        public void Calculate_Section_Area_Yt_Yb_Zt_Zb()
        {
            lst_Area.Clear();
            lst_Yt.Clear();
            lst_Yb.Clear();
            lst_Zt.Clear();
            lst_Zb.Clear();

            List<string> des_nodes = new List<string>();
      
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

            #region Calculate Area

            for (i = 0; i < End_Span_Sections.Count; i++)
            {
                var v = End_Span_Sections[i];
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


            for (i = 0; i < Mid_Span_Sections.Count; i++)
            {
                var v = Mid_Span_Sections[i];
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

        }


    }
    public class Continuous_Box_Section_List : List<Continuous_Box_Section_Data>
    {
        public Continuous_Box_Section_List()
            : base()
        {
        }

        public double Total_Area { get; set; }
        public double Total_Area_CG_Top { get; set; }
        public double Total_Area_YY { get; set; }
        public double ISelf { get; set; }
        public double I { get; set; }
        public double Yt { get; set; }
        public double Yb { get; set; }


        public List<string> Result_Output { get; set; }


        public void Calculate_Data(DataGridView dgv)
        {
            Continuous_Box_Section_Data cbs_data = null;

            this.Clear();

            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                cbs_data = new Continuous_Box_Section_Data();

                cbs_data.ComponentNo = i + 1;
                cbs_data.Shape = dgv[1, i].Value.ToString().ToUpper().StartsWith("R") ? Continuous_Box_Section_Data.eShape.RECT : Continuous_Box_Section_Data.eShape.TRI;
                cbs_data.Nos = MyList.StringToInt(dgv[2, i].Value.ToString(), 1);
                //cbs_data.Coeff = MyList.StringToDouble(dgv[2, i].Value.ToString(), 1);
                cbs_data.B = MyList.StringToDouble(dgv[4, i].Value.ToString(), 1);
                cbs_data.D = MyList.StringToDouble(dgv[5, i].Value.ToString(), 1);

                this.Add(cbs_data);

            }


            this[0].Comp_CG_Top = this[0].D / 2.0;

            this[1].Comp_CG_Top = this[0].D + this[1].D / 3.0;

            this[2].Comp_CG_Top = this[2].D / 2.0;

            this[3].Comp_CG_Top = this[2].D + this[3].D / 3.0;

            this[4].Comp_CG_Top = this[2].D + this[4].D / 2.0;

            this[5].Comp_CG_Top = this[2].D + this[4].D - this[5].D / 3.0;

            this[6].Comp_CG_Top = this[2].D + this[4].D + this[6].D / 2.0;

            //this[6].Area_CG_Top = this[2].D + this[4].D + this[6].D / 2.0;

            double a = 0.0, b = 0.0, c = 0.0;


            for (int i = 0; i < this.Count; i++)
            {
                a += this[i].Area;
                c += this[i].Area_CG_Top;
            }
            Total_Area = a;
            Total_Area_CG_Top = c;

            dgv[6, 7].Value = a;
            dgv[8, 7].Value = c;

            Yt = c / a;

            //Yb = this[0].D + this[1].D + this[4].D + this[6].D;
            Yb = this[2].D + this[4].D + this[6].D;

            Yb -= Yt;



            a = 0.0;
            b = 0.0;
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Y = Math.Abs(Yt - this[i].Comp_CG_Top);

                a += this[i].AYY;
                b += this[i].ISelf;
            }

            double IZ = a + b;

            I = IZ;






            for (int i = 0; i < this.Count; i++)
            {
                dgv[3, i].Value = this[i].Coeff;
                dgv[6, i].Value = this[i].Area;
                dgv[7, i].Value = this[i].Comp_CG_Top;
                dgv[8, i].Value = this[i].Area_CG_Top;
                dgv[9, i].Value = Yt;
                dgv[10, i].Value = this[i].Y;
                dgv[11, i].Value = this[i].AYY;
                dgv[12, i].Value = this[i].ISelf;
                dgv[13, i].Value = IZ;
            }

            dgv[11, 7].Value = a;
            dgv[12, 7].Value = b;
            Total_Area_YY = a;
            ISelf = b;

            Format_Grid_Data(dgv);

            Result_Output = new List<string>();

            string format = "   {0,-9} {1,-5} {2,4} {3,6} {4,7:f3} {5,7:f3} {6,7:f3} {7,9:f4} {8,9:f4} {9,9:f4} {10,9:f4} {11,9:f4} {12,9:f4} {13,9:f4}";

       
            Result_Output.Add(string.Format(""));
            Result_Output.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------"));
            Result_Output.Add(string.Format("                                                          COMP                          COMP.     A*Y^2     "));
            Result_Output.Add(string.Format(" COMPONENT  SHAPE   NOS. COEEF.    B       D     AREA     C.G      A*C.G.      N.A.     C.G.      FROM      ISELF      IX"));
            Result_Output.Add(string.Format("    NO                                                    FROM     (TOP)       YT.      FROM      N.A               "));
            Result_Output.Add(string.Format("                                                          TOP                           N.A. Y                 "));
            Result_Output.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------"));
            Result_Output.Add(string.Format("                                  (m)     (m)   (sq.m)    (m)      (m^3)       (m)      (m)       (m^4)     (m^4)     (m^4) "));
            Result_Output.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------"));


            //Result_Output.Add(string.Format(format,
            //"COMPONENT",
            //"SHAPE",
            //"NOS.",
            //"COEEF.",
            //"B",
            //"D",
            //"A",
            //"COMP",
            //"A*C.G.",
            //"N.A.",
            //"COMP.",
            //"A*Y^2",
            //"ISELF",
            //"IX"));


            //Result_Output.Add(string.Format(format,
            //"NO.",
            //"",
            //"",
            //"",
            //"",
            //"",
            //"",
            //"C.G",
            //"",
            //"YT.",
            //"C.G.",
            //"FROM",
            //"",
            //""));


            //Result_Output.Add(string.Format(format,
            //"",
            //"",
            //"",
            //"",
            //"",
            //"",
            //"",
            //"FROM",
            //"(TOP)",
            //"",
            //"FROM",
            //"N.A.Y",
            //"N.A",
            //""));

            //Result_Output.Add(string.Format(format,
            //"",
            //"",
            //"",
            //"",
            //"",
            //"",
            //"",
            //"TOP",
            //"",
            //"",
            //"",
            //"",
            //"",
            //""));


            //Result_Output.Add("");

            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i == dgv.RowCount - 1)
                    Result_Output.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------"));


                Result_Output.Add(string.Format(format,
                dgv[0, i].Value,
                dgv[1, i].Value,
                dgv[2, i].Value,
                dgv[3, i].Value,
                dgv[4, i].Value,
                dgv[5, i].Value,
                dgv[6, i].Value,
                dgv[7, i].Value,
                dgv[8, i].Value,
                dgv[9, i].Value,
                dgv[10, i].Value,
                dgv[11, i].Value,
                dgv[12, i].Value,
                dgv[13, i].Value));
            }
            Result_Output.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------"));
        }

        public void Grid_Defaulf_Data1(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Rows.Add("(1)",
                "RECT",
                2,
                1,
                2.64,
                0.2,
                1.056,
                0.1,
                0.1056,
                1.437921663,
                1.337921663,
                1.8902763,
                0.00352,
                12.33795123);



            dgv.Rows.Add("(2)",
                "TRI",
                    2,
                    0.5,
                    2.64,
                    0.1,
                    0.264,
                    0.233333333,
                    0.0616,
                    1.437921663,
                    1.204588329,
                    0.383072723,
                    0.000146667,
                12.33795123);


            dgv.Rows.Add("(3)",
                "RECT",
                1,
                1,
                5.8,
                0.25,
                1.45,
                0.125,
                0.18125,
                1.437921663,
                1.312921663,
                2.499456774,
                0.007552083,
                12.33795123);



            dgv.Rows.Add("(4)",
                "TRI",
                2,
                0.5,
                0.3,
                0.15,
                0.045,
                0.3,
                0.0135,
                1.437921663,
                1.137921663,
                0.058268957,
                0.00005625,
                12.33795123);


            dgv.Rows.Add("(5)",
                "RECT",
                2,
                    1,
                    0.3,
                    3.05,
                    1.83,
                    1.775,
                    3.24825,
                    1.437921663,
                    0.337078337,
                    0.207927904,
                    1.41863125,
                12.33795123);



            dgv.Rows.Add("(6)",
                "TRI",
            2,
                    0.5,
                    1.5,
                    0.3,
                    0.45,
                    3.2,
                    1.44,
                    1.437921663,
                    1.762078337,
                    1.39721403,
                    0.00225,
                12.33795123);





            dgv.Rows.Add("(7)",
                "RECT",
                    1,
                    1,
                    5.8,
                    0.2,
                    1.16,
                    3.4,
                    3.944,
                    1.437921663,
                    1.962078337,
                    4.465711626,
                    0.003866667,
                12.33795123);



            dgv.Rows.Add(" ",
                "",
                "",
                "",
                "",
                "",
                "6.255",
                "",
                "8.9942",
                "",
                "",
                "10.90192831",
                "1.436022917",
                "");

            Format_Grid_Data(dgv);
        }

        public void Grid_Defaulf_Data_Mid_Span(DataGridView dgv, int section_no)
        {

            dgv.Rows.Clear();

            switch (section_no)
            {
                case 1:
                    #region Section 1

                    dgv.Rows.Add("(1)",
                        "RECT",
                        2,
                        1,
                        2.64,
                        0.2,
                        1.056,
                        0.1,
                        0.1056,
                        1.437921663,
                        1.337921663,
                        1.8902763,
                        0.00352,
                        12.33795123);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.3,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.5,
                            0.3,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);





                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");

                    #endregion Section 2
                    break;
                case 2:
                    #region Section 2
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.4828,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.3172,
                            0.2634,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);





                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 3:
                    #region Section 3
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.525,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.275,
                            0.255,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);





                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;


                case 4:
                    #region Section 4
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.5419,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.2581,
                            0.2516,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 5:
                    #region Section 5
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.6769,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.1231,
                            0.2246,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 6:
                    #region Section 6
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.05,
                            0.210,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 7:
                    #region Section 7
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.05,
                            0.210,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 8:
                    #region Section 8
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.05,
                            0.210,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 9:
                    #region Section 9
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.05,
                            0.210,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;
                case 10:
                    #region Section 10
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            1.05,
                            0.210,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;
            }
            Calculate_Data(dgv);
        }

        public void Grid_Defaulf_Data_End_Span(DataGridView dgv, int section_no)
        {

            dgv.Rows.Clear();


            switch (section_no)
            {
                case 1:
                    #region Section 1

                    dgv.Rows.Add("(1)",
                        "RECT",
                        2,
                        1,
                        2.64,
                        0.2,
                        1.056,
                        0.1,
                        0.1056,
                        1.437921663,
                        1.337921663,
                        1.8902763,
                        0.00352,
                        12.33795123);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.3,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.9,
                            0.3,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);





                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");

                    #endregion Section 2
                    break;
                case 2:
                    #region Section 2
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.465,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.735,
                            0.245,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);





                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 3:
                    #region Section 3
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.525,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.675,
                            0.255,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);





                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;


                case 4:
                    #region Section 4
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.45,
                            0.15,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 5:
                    #region Section 5
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.45,
                            0.15,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 6:
                    #region Section 6
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.45,
                            0.15,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

                case 7:
                    #region Section 7
                    dgv.Rows.Add("(1)",
                            "RECT",
                            2,
                            1,
                            2.64,
                            0.2,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0,
                            0.0);



                    dgv.Rows.Add("(2)",
                        "TRI",
                            2,
                            0.5,
                            2.64,
                            0.1,
                            0.264,
                            0.233333333,
                            0.0616,
                            1.437921663,
                            1.204588329,
                            0.383072723,
                            0.000146667,
                        12.33795123);


                    dgv.Rows.Add("(3)",
                        "RECT",
                        1,
                        1,
                        5.8,
                        0.25,
                        1.45,
                        0.125,
                        0.18125,
                        1.437921663,
                        1.312921663,
                        2.499456774,
                        0.007552083,
                        12.33795123);



                    dgv.Rows.Add("(4)",
                        "TRI",
                        2,
                        0.5,
                        0.3,
                        0.15,
                        0.045,
                        0.3,
                        0.0135,
                        1.437921663,
                        1.137921663,
                        0.058268957,
                        0.00005625,
                        12.33795123);


                    dgv.Rows.Add("(5)",
                        "RECT",
                        2,
                            1,
                            0.75,
                            3.05,
                            1.83,
                            1.775,
                            3.24825,
                            1.437921663,
                            0.337078337,
                            0.207927904,
                            1.41863125,
                        12.33795123);



                    dgv.Rows.Add("(6)",
                        "TRI",
                    2,
                            0.5,
                            0.45,
                            0.15,
                            0.45,
                            3.2,
                            1.44,
                            1.437921663,
                            1.762078337,
                            1.39721403,
                            0.00225,
                        12.33795123);

                    dgv.Rows.Add("(7)",
                        "RECT",
                            1,
                            1,
                            5.8,
                            0.2,
                            1.16,
                            3.4,
                            3.944,
                            1.437921663,
                            1.962078337,
                            4.465711626,
                            0.003866667,
                        12.33795123);



                    dgv.Rows.Add(" ",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "6.255",
                        "",
                        "8.9942",
                        "",
                        "",
                        "10.90192831",
                        "1.436022917",
                        "");
                    #endregion Section 2
                    break;

            }
            Calculate_Data(dgv);
        }

        public void Format_Grid_Data(DataGridView dgv)
        {

            for (int i = 0; i < dgv.RowCount; i++)
            {

                for (int j = 1; j < dgv.ColumnCount; j++)
                {
                    try
                    {
                        if (j == 1)
                        {
                            if (dgv[j, i].Value.ToString().StartsWith("R"))
                                dgv[j, i].Value = "RECT";
                            else if (dgv[j, i].Value.ToString().StartsWith("T"))
                                dgv[j, i].Value = "TRI";
                        }
                        else if (j == 2)
                        {

                            dgv[j, i].Value = MyList.StringToDouble(dgv[j, i].Value.ToString(), 0.0).ToString("f0");

                        }
                        else if (j == 3)
                        {
                            dgv[j, i].Value = MyList.StringToDouble(dgv[j, i].Value.ToString(), 0.0).ToString("f2");

                        }
                        else if (j == 4 || j == 5)
                        {
                            dgv[j, i].Value = MyList.StringToDouble(dgv[j, i].Value.ToString(), 0.0).ToString("f3");

                        }
                        //else if (j == 9)
                        //{

                        //    dgv[j, i].Value = MyList.StringToDouble(dgv[j, i].Value.ToString(), 0.0).ToString("f2");

                        //}
                        else if (j > 3)
                            dgv[j, i].Value = MyList.StringToDouble(dgv[j, i].Value.ToString(), 0.0).ToString("f4");




                        double val = MyList.StringToDouble(dgv[j, i].Value.ToString(), 0.0);
                        if (val == 0.0 && j != 1)
                            dgv[j, i].Value = "";

                    }
                    catch (Exception ex) { }
                }
            }
        }
    }

    public class Continuous_Box_Section_Data
    {
        #region Properties
        public enum eShape
        {
            RECT = 0,
            TRI = 1,
        }
        public int ComponentNo { get; set; }
        public eShape Shape { get; set; }
        public int Nos { get; set; }
        public double Coeff
        {
            get
            {
                return (Shape == eShape.RECT ? 1.0 : 0.5);
            }

        }
        public double B { get; set; }
        public double D { get; set; }
        public double Area
        {
            get
            {
                return (Nos * Coeff * B * D);
            }
        }
        public double Comp_CG_Top { get; set; }
        public double Area_CG_Top
        {
            get
            {
                return Area * Comp_CG_Top;
            }

        }
        public double Y { get; set; }
        public double AYY
        {
            get
            {
                return Area * Y * Y;
            }
        }
        public double ISelf
        {
            get
            {
                if (Shape == eShape.RECT)
                    return (Nos * B * D * D * D / 12.0);
                return (Nos * B * D * D * D / 36.0);
            }
        }
        #endregion Properties

        public Continuous_Box_Section_Data()
        {
            ComponentNo = -1;
            Shape = eShape.RECT;
            Nos = 0;
            B = 0;
            D = 0;
            Comp_CG_Top = 0;
            //Area_CG_Top = 0;
            Y = 0;
        }
    }

    public class Cross_Section_Box_Girder
    {

        #region Properties
        public double CS_B1 { get; set; }
        public double CS_B2 { get; set; }
        public double CS_B3 { get; set; }
        public double CS_B4 { get; set; }
        public double CS_B5 { get; set; }
        public double CS_B6 { get; set; }
        public double CS_B7 { get; set; }
        public double CS_B8 { get; set; }
        public double CS_B9 { get; set; }
        public double CS_B10 { get; set; }
        public double CS_B11 { get; set; }
        public double CS_B12 { get; set; }
        public double CS_B13 { get; set; }


        public double CS_D1 { get; set; }
        public double CS_D2 { get; set; }
        public double CS_D3 { get; set; }
        public double CS_D4 { get; set; }
        public double CS_D5 { get; set; }
        public double CS_D6 { get; set; }
        public double CS_D7 { get; set; }
        public double CS_D8 { get; set; }
        public double CS_D9 { get; set; }


        public double CS_Anc_B1 { get; set; }
        public double CS_Anc_B2 { get; set; }
        public double CS_Anc_B3 { get; set; }
        public double CS_Anc_B4 { get; set; }
        public double CS_Anc_B5 { get; set; }
        public double CS_Anc_B6 { get; set; }
        public double CS_Anc_B7 { get; set; }
        public double CS_Anc_B8 { get; set; }
        public double CS_Anc_B9 { get; set; }
        public double CS_Anc_B10 { get; set; }
        public double CS_Anc_B11 { get; set; }
        public double CS_Anc_B12 { get; set; }
        public double CS_Anc_B13 { get; set; }



        public double CS_Anc_D1 { get; set; }
        public double CS_Anc_D2 { get; set; }
        public double CS_Anc_D3 { get; set; }
        public double CS_Anc_D4 { get; set; }
        public double CS_Anc_D5 { get; set; }
        public double CS_Anc_D6 { get; set; }
        public double CS_Anc_D7 { get; set; }
        public double CS_Anc_D8 { get; set; }
        public double CS_Anc_D9 { get; set; }
        public double CS_Anc_D10 { get; set; }
        public double CS_Anc_D11 { get; set; }
        public double CS_Anc_D12 { get; set; }

        #endregion Properties

        public Cross_Section_Box_Girder()
        {
            CS_B1 = 0.0;
            CS_B2 = 0.0;
            CS_B3 = 0.0;
            CS_B4 = 0.0;
            CS_B5 = 0.0;
            CS_B6 = 0.0;
            CS_B7 = 0.0;
            CS_B8 = 0.0;
            CS_B9 = 0.0;
            CS_B10 = 0.0;
            CS_B11 = 0.0;
            CS_B12 = 0.0;
            CS_B13 = 0.0;


            CS_D1 = 0.0;
            CS_D2 = 0.0;
            CS_D3 = 0.0;
            CS_D4 = 0.0;
            CS_D5 = 0.0;
            CS_D6 = 0.0;
            CS_D7 = 0.0;
            CS_D8 = 0.0;
            CS_D9 = 0.0;


            CS_Anc_B1 = 0.0;
            CS_Anc_B2 = 0.0;
            CS_Anc_B3 = 0.0;
            CS_Anc_B4 = 0.0;
            CS_Anc_B5 = 0.0;
            CS_Anc_B6 = 0.0;
            CS_Anc_B7 = 0.0;
            CS_Anc_B8 = 0.0;
            CS_Anc_B9 = 0.0;
            CS_Anc_B10 = 0.0;
            CS_Anc_B11 = 0.0;
            CS_Anc_B12 = 0.0;
            CS_Anc_B13 = 0.0;



            CS_Anc_D1 = 0.0;
            CS_Anc_D2 = 0.0;
            CS_Anc_D3 = 0.0;
            CS_Anc_D4 = 0.0;
            CS_Anc_D5 = 0.0;
            CS_Anc_D6 = 0.0;
            CS_Anc_D7 = 0.0;
            CS_Anc_D8 = 0.0;
            CS_Anc_D9 = 0.0;
            CS_Anc_D10 = 0.0;
            CS_Anc_D11 = 0.0;
            CS_Anc_D12 = 0.0;
        }

    }

    public class Forces_Results : List<Forces_Result_Data>
    {
        //DESIGN   DESIGN                  DEAD LOAD    DEAD LOAD       DEAD LOAD       DEAD LOAD
        // NODE    SECTION                  BM (T-m)       SF (T)        BM (T-m)          SF (T)


        public Forces_Results()
            : base()
        {

        }

    }

    public class Forces_Result_Data
    {
        //DESIGN   DESIGN                  DEAD LOAD    DEAD LOAD       DEAD LOAD       DEAD LOAD
        // NODE    SECTION                  BM (T-m)       SF (T)        BM (T-m)          SF (T)

        public int DesignNode { get; set; }
        public string DesignSection { get; set; }
        public double BM { get; set; }
        public double SF { get; set; }
        public double BM_Incr { get; set; }
        public double SF_Incr { get; set; }

        public Forces_Result_Data()
        {
            DesignNode = 0;
            DesignSection = "";
            BM = 0.0;
            SF = 0.0;
            BM_Incr = 0.0;
            SF_Incr = 0.0;
        }

    }


    public class LiveLoad_Forces_Result_Data
    {

        //DESIGN   DESIGN                  DEAD LOAD    DEAD LOAD       DEAD LOAD       DEAD LOAD
        // NODE    SECTION                  BM (T-m)       SF (T)        BM (T-m)          SF (T)

        public int DesignNode { get; set; }
        public string DesignSection { get; set; }
        public List<double> BM { get; set; }
        public List<double> SF { get; set; }

        public LiveLoad_Forces_Result_Data()
        {
            DesignNode = 0;
            DesignSection = "";
            BM = new List<double>();
            SF = new List<double>();
        }

    }


}
//Chiranjit [2013 07 24] Add Moving Load File
//Chiranjit [2013 07 26] Read Data from Files
