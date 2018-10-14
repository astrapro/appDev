using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using AstraFunctionOne;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;

using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;

using BridgeAnalysisDesign;
using LimitStateMethod.LS_Progress;
using LimitStateMethod.Bearing;

namespace LimitStateMethod.RCC_T_Girder
{
    public class RCC_T_Girder_LS_Girder_Analysis
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        JointNode[,] Joints_Array;
        Member[,] Long_Girder_Members_Array;
        Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis TotalLoad_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_Analysis = null;

        public BridgeMemberAnalysis LiveLoad_1_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_2_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_3_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_4_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_5_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_6_Analysis = null;

        public List<BridgeMemberAnalysis> All_LL_Analysis = null;





        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;



        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        //Chiranjit [2012 12 18]
        public TGirder_Section_Properties Long_Inner_Mid_Section { get; set; }
        public TGirder_Section_Properties Long_Inner_Support_Section { get; set; }
        public TGirder_Section_Properties Long_Outer_Mid_Section { get; set; }
        public TGirder_Section_Properties Long_Outer_Support_Section { get; set; }
        public TGirder_Section_Properties Cross_End_Section { get; set; }
        public TGirder_Section_Properties Cross_Intermediate_Section { get; set; }


        //Chiranjit [2013 06 06] Kolkata

        public string List_Envelop_Inner { get; set; }
        public string List_Envelop_Outer { get; set; }

        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        string input_file, user_path;
        public RCC_T_Girder_LS_Girder_Analysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
            List_Envelop_Inner = "";
            List_Envelop_Outer = "";
        }

        #region Properties

        public double Length { get; set; }
        public double Ds { get; set; }



        /// <summary>
        /// width of crash barrier
        /// </summary>
        public double Wc { get; set; }
        public double Wf_left { get; set; }
        public double Wf_right { get; set; }
        /// <summary>
        /// width of footpath
        /// </summary>
        public double Wk_left { get; set; }
        public double Wk_right { get; set; }

        /// <summary>
        /// width of railing
        /// </summary>
        public double Wr { get; set; }
        /// <summary>
        /// Overhang of girder off the bearing [og]
        /// </summary>
        public double og { get; set; }
        /// <summary>
        /// Overhang of slab off the bearing [os]
        /// </summary>
        public double os { get; set; }
        /// <summary>
        /// Expansion Gap [eg]
        /// </summary>
        public double eg { get; set; }
        /// <summary>
        /// Length of varring portion
        /// </summary>
        public double Lvp { get; set; }
        /// <summary>
        /// Length of Solid portion
        /// </summary>
        public double Lsp { get; set; }
        /// <summary>
        /// Effective Length
        /// </summary>
        public double Leff { get; set; }


        public double WidthBridge { get; set; }
        public double Effective_Depth { get; set; }
        public int Total_Rows
        {
            get
            {
                return 11;
            }
        }
        public int Total_Columns
        {
            get
            {
                return 11;
            }
        }
        public double Skew_Angle { get; set; }
        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }

        public double Spacing_Long_Girder
        {

            get
            {
                //Chiranjit [2013 05 02]
                //return MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / 6.0).ToString("0.000"), 0.0);

                double val = ((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (NMG - 1));
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //chiranji [2013 05 03]
                //return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);

                double val = (Length - 2 * Effective_Depth) / (NCG - 1);
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public string LiveLoad_File
        {
            get
            {
                //return Path.Combine(Working_Folder, "LL.TXT");
                return Path.Combine(Path.GetDirectoryName(Total_Analysis_Report), "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(Working_Folder, "ANALYSIS_REP.TXT");
            }
        }
        #region Analysis Input File
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                input_file = value;
                if (input_file != "") user_path = Path.GetDirectoryName(input_file);
                else user_path = "";
            }
        }
        //Chiranjit [2012 05 27]
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "DL + LL Combine Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DL_LL_Combine_Input_File.txt");
                }
                return "";
            }
        }
        public string TempAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Temp Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "Temp_Input_File.txt");
                }
                return "";
            }
        }
        //Chiranjit [2012 05 27]
        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Live Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        //Chiranjit [2014 10 22]
        public string Get_LL_Analysis_Input_File(int AnalysisNo)
        {
            if (AnalysisNo <= 0) return "";

            if (Directory.Exists(Working_Folder))
            {
                string pd = Path.Combine(Working_Folder, "LL Analysis Load " + AnalysisNo);
                if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                return Path.Combine(pd, "LL_Load_" + AnalysisNo + "_Input_File.txt");
            }
            return "";
        }

        //Chiranjit [2013 09 24]
        public string LL_Analysis_1_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 1");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_1_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_2_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 2");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_2_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_3_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 3");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_3_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_4_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 4");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_4_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_5_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 5");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_5_Input_File.txt");
                }
                return "";
            }
        }
        public string LL_Analysis_6_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 6");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_6_Input_File.txt");
                    //return Path.Combine(pd, "LL_Type_6_Input_File.txt");
                }
                return "";
            }
        }


        //Chiranjit [2012 05 27]
        public string DeadLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string Total_Analysis_Report
        {
            get
            {
                if (!File.Exists(TotalAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(TotalAnalysis_Input_File), "ANALYSIS_REP.TXT");

            }
        }
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(Working_Folder)) return "";
                return Path.Combine(Working_Folder, "ASTRA_DATA_FILE.TXT");

            }
        }
        public string LiveLoad_Analysis_Report
        {
            get
            {
                if (!File.Exists(LiveLoadAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(LiveLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
            }
        }
        public string DeadLoad_Analysis_Report
        {
            get
            {
                if (!File.Exists(DeadLoadAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
            }
        }


        public string Get_Analysis_Report_File(string input_path)
        {

            if (!File.Exists(input_path)) return "";

            return Path.Combine(Path.GetDirectoryName(input_path), "ANALYSIS_REP.TXT");


        }
        public string Working_Folder
        {
            get
            {
                if (Input_File != "")
                    return Path.GetDirectoryName(Input_File);
                return "";
            }
        }
        public int NoOfInsideJoints
        {
            get
            {
                return 1;
            }
        }
        #endregion Analysis Input File

        //Chiranjit [2013 05 02]
        //public int Number_Of_Long_Girder { get; set; }
        //public int Number_Of_Cross_Girder { get; set; }
        public int NCG { get; set; }
        public int NMG { get; set; }


        #endregion Properties



        //Chiranjit [2013 05 02]
        string support_left_joints = "";
        string support_right_joints = "";

        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();


        //Chiranjit [2011 08 01]
        //Create Bridge Input Data by user's given values.
        //Long Girder Spacing, Cross Girder Spacing, Cantilever Width
        public void CreateData()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));
            Effective_Depth = 0.0;

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
            last_x = x_incr;
            //last_x = x_incr + Effective_Depth;

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
                //List_Envelop_Outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                //List_Envelop_Inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;


                List_Envelop_Outer = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
                List_Envelop_Inner = Long_Girder_Members_Array[3, 0].MemberNo + " TO " + Long_Girder_Members_Array[3, iCols - 2].MemberNo;
            }
            else
            {
                List_Envelop_Outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                List_Envelop_Inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]
        }

        public string _DeckSlab { get; set; }
        public string _Inner_Girder_Mid { get; set; }
        public string _Inner_Girder_Support { get; set; }
        public string _Outer_Girder_Mid { get; set; }
        public string _Outer_Girder_Support { get; set; }
        public string _Cross_Girder_Inter { get; set; }
        public string _Cross_Girder_End { get; set; }

        //Chiranjit [2011 07 11]
        //Write Default data as given Astra Examples 8

        //string _DeckSlab = "";
        //string _Inner_Girder_Mid = "";
        //string _Inner_Girder_Support = "";
        //string _Outer_Girder_Mid = "";
        //string _Outer_Girder_Support = "";
        //string _Cross_Girder_Inter = "";
        //string _Cross_Girder_End = "";

        void Set_Inner_Outer_Cross_Girders()
        {

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();

            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000")) ||
                    (MemColls[i].StartNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000")))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z == WidthBridge) &&
                    (MemColls[i].EndNode.Z == WidthBridge))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    Inner_Girder.Add(MemColls[i].MemberNo);
                }
            }
            Inner_Girder.Sort();
            Outer_Girder.Sort();
            Cross_Girder.Sort();


            _Cross_Girder_Inter = MyList.Get_Array_Text(Cross_Girder);
            _Inner_Girder_Mid = MyList.Get_Array_Text(Inner_Girder);
            _Outer_Girder_Mid = MyList.Get_Array_Text(Outer_Girder);

        }

        #region Chiranjit [2014 09 02] For British Standard

        public List<int> HA_Lanes;

        public string HA_Loading_Members;
        public void WriteData_Total_Analysis(string file_name, bool is_british)
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
                //for (i = 0; i < _Columns - 1; i++)
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
                for (i = 0; i < _Columns; i++)
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
            if (Long_Inner_Mid_Section != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANTS");
            list.Add("E 2.85E6 ALL");
            //list.Add("E " + Ecm * 100 + " ALL");
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
        public void CreateData_British()
        {

            //double x_incr = (Length / (Total_Columns - 1));
            //double z_incr = (WidthBridge / (Total_Rows - 1));
            Effective_Depth = 0.0;

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
            //last_x = x_incr + Effective_Depth;
            last_x = x_incr;

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
            if (HA_Lanes.Count > 0)
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

                    if (!HA_distances.Contains(Joints_Array[iRows, iCols].Z))
                    {
                        if (iCols == 1 && iRows > 1 && iRows < _Rows - 2)
                        {

                            support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                        }
                        else if (iCols == _Columns - 2 && iRows > 1 && iRows < _Rows - 2)
                        {
                            support_right_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                        }
                        else
                        {
                            if (iRows > 0 && iRows < _Rows - 1)
                                list_nodes.Add(Joints_Array[iRows, iCols].NodeNo);
                        }
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
                //List_Envelop_Outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                //List_Envelop_Inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;

                List_Envelop_Outer = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
                List_Envelop_Inner = Long_Girder_Members_Array[3, 0].MemberNo + " TO " + Long_Girder_Members_Array[3, iCols - 2].MemberNo;
            }
            else
            {
                List_Envelop_Outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                List_Envelop_Inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]
        }

        #endregion Chiranjit [2014 09 02] For British Standard

        public void WriteData_Total_Analysis(string file_name)
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






            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
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
                            Inner_Girder_Support.Add(Long_Girder_Members_Array[c, i].MemberNo);
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
                            Inner_Girder_Mid.Add(Long_Girder_Members_Array[c, i].MemberNo);
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
                for (i = 0; i < _Columns; i++)
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


            list.Add("SECTION PROPERTIES");

            if (Long_Inner_Mid_Section != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  {1}", support_right_joints, End_Support));



            //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
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

            list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
                _DeckSlab,
                Ds));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Cross_Girder_End,
                Cross_End_Section.Composite_Section_A,
                Cross_End_Section.Composite_Section_Ix,
                Cross_End_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Cross_Girder_Inter,
                Cross_Intermediate_Section.Composite_Section_A,
                Cross_Intermediate_Section.Composite_Section_Ix,
                Cross_Intermediate_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Outer_Girder_Support,
                Long_Outer_Support_Section.Composite_Section_A,
                Long_Outer_Support_Section.Composite_Section_Ix,
                Long_Outer_Support_Section.Composite_Section_Iz));

            list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Outer_Girder_Mid,
                Long_Outer_Mid_Section.Composite_Section_A,
                Long_Outer_Mid_Section.Composite_Section_Ix,
                Long_Outer_Mid_Section.Composite_Section_Iz));

            if (_Inner_Girder_Support.StartsWith("0") == false)
                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                    _Inner_Girder_Support,
                    Long_Inner_Support_Section.Composite_Section_A,
                    Long_Inner_Support_Section.Composite_Section_Ix,
                    Long_Inner_Support_Section.Composite_Section_Iz));

            if (_Inner_Girder_Mid.StartsWith("0") == false)
                list.Add(string.Format("{0} PRIS AX {1:f4} IX {2:f4} IZ {3:f4} ",
                _Inner_Girder_Mid,
                Long_Inner_Mid_Section.Composite_Section_A,
                Long_Inner_Mid_Section.Composite_Section_Ix,
                Long_Inner_Mid_Section.Composite_Section_Iz));

            //if (HA_Loading_Members == "")
            //{
            //    list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
            //        _DeckSlab,
            //        Ds));
            //}
            if (HA_Loading_Members != "")
            {
                list.Add(string.Format("{0} PRIS YD {1:f4} ZD 1.0",
                    HA_Loading_Members,
                    Ds));
            }
        }
        public void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_data)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
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

            if (Long_Inner_Mid_Section != null)
            {

                Set_Section_Properties(list);


            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));

            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
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

        public void WriteData_DeadLoad_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
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
            if (Long_Inner_Mid_Section != null)
            {
                Set_Section_Properties(list);
            }
            else
            {
                list.Add(string.Format("{0} TO {1} PRIS AX 1.146 IX 0.022 IZ 0.187", MemColls[0].MemberNo, MemColls[MemColls.Count - 1].MemberNo));
            }
            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            //list.Add(string.Format("{0}  PINNED", support_left_joints));
            ////list.Add(string.Format("{0}  PINNED", support_right_joints));
            //list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));


            list.Add(string.Format("{0}  {1}", support_left_joints, Start_Support));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
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

        public void Clear()
        {
            Joints_Array = null;
            Long_Girder_Members_Array = null;
            Cross_Girder_Members_Array = null;
            MemColls.Clear();
            MemColls = null;
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList_1 = new List<LoadData>();
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
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
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
                        LoadList_1.Add(ld);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #region Set Loads

            List<LoadData> LoadList_tmp = new List<LoadData>(LoadList_1.ToArray());

            LoadList_2 = new List<LoadData>();
            LoadList_3 = new List<LoadData>();
            LoadList_4 = new List<LoadData>();
            LoadList_5 = new List<LoadData>();
            LoadList_6 = new List<LoadData>();

            LoadList_1.Clear();

            //70 R Wheel
            ld = new LoadData(Live_Load_List[2]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_1.Add(ld);


            //1 Lane Class A
            ld = new LoadData(Live_Load_List[0]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_2.Add(ld);



            //2 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);



            //3 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[2].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);


            //1 Lane Class A + 70 RW
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);

            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);



            //70 RW + 1 Lane Class A 
            ld = new LoadData(Live_Load_List[2]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            #endregion Set Loads

        }
        public string Get_LHS_Outer_Girder()
        {
            string LHS = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, _Columns-2].MemberNo ;
            return LHS;

        }
        public string Get_RHS_Outer_Girder()
        {
            string RHS = Long_Girder_Members_Array[_Rows - 3, 0].MemberNo + " TO " + Long_Girder_Members_Array[_Rows - 3, _Columns - 2].MemberNo;
            return RHS;
        }
    }
    /// <summary>
    /// Deck Slab Analysis with Limit State Method
    /// </summary>
    public class LS_DeckSlab_Analysis
    {
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        JointNode[,] Joints_Array;
        Member[,] Long_Girder_Members_Array;
        Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        public BridgeMemberAnalysis TotalLoad_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_Analysis = null;

        public BridgeMemberAnalysis LiveLoad_1_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_2_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_3_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_4_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_5_Analysis = null;
        public BridgeMemberAnalysis LiveLoad_6_Analysis = null;


        public BridgeMemberAnalysis DeadLoad_Analysis = null;

        public List<LoadData> LoadList_1 = null;
        public List<LoadData> LoadList_2 = null;
        public List<LoadData> LoadList_3 = null;
        public List<LoadData> LoadList_4 = null;
        public List<LoadData> LoadList_5 = null;
        public List<LoadData> LoadList_6 = null;



        public List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;

        //Chiranjit [2012 12 18]
        public TLong_SectionProperties T_Long_Inner_Section { get; set; }
        public TLong_SectionProperties T_Long_Outer_Section { get; set; }
        public TCross_SectionProperties T_Cross_Section { get; set; }


        //Chiranjit [2013 06 06] Kolkata

        string list_envelop_inner = "";
        string list_envelop_outer = "";


        string input_file, user_path;
        public LS_DeckSlab_Analysis(IApplication thisApp)
        {
            iApp = thisApp;
            input_file = "";
            Length = WidthBridge = Effective_Depth = Skew_Angle = Width_LeftCantilever = 0.0;
            Input_File = "";

            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();

        }

        #region Properties

        public double Length { get; set; }
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
        public double Width_LeftCantilever { get; set; }
        public double Width_RightCantilever { get; set; }




        public double Spacing_Long_Girder
        {

            get
            {
                //Chiranjit [2013 05 02]
                //return MyList.StringToDouble(((WidthBridge - (2 * Width_LeftCantilever)) / 6.0).ToString("0.000"), 0.0);

                //double val = ((WidthBridge - (Width_LeftCantilever + Width_RightCantilever)) / (Number_Of_Long_Girder - 1));
                double val = ((WidthBridge) / (3 - 1));




                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //chiranji [2013 05 03]
                //return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);

                //double val = (Length - 2 * Effective_Depth) / (Number_Of_Cross_Girder - 1);
                double val = (Length / 16.0);
                return MyList.StringToDouble(val.ToString("0.000"), 0.0);
            }
        }
        public string LiveLoad_File
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(LL_Analysis_1_Input_File), "LL.TXT");
            }
        }
        public string Analysis_Report
        {
            get
            {
                return Path.Combine(Working_Folder, "ANALYSIS_REP.TXT");
            }
        }


        //Chiranjit [2013 05 02]
        public int Number_Of_Long_Girder { get; set; }
        public int Number_Of_Cross_Girder { get; set; }

        public double Lwv { get; set; }
        public double Wkerb { get; set; }

        #endregion Properties

        #region Analysis Input File
        public string Input_File
        {
            get
            {
                return input_file;
            }
            set
            {
                input_file = value;
                if (File.Exists(value))
                    user_path = Path.GetDirectoryName(input_file);
            }
        }
        //Chiranjit [2012 05 27]
        public string TotalAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Total Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "Total_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        //Chiranjit [2012 05 27]
        public string LiveLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Live Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LiveLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }

        //Chiranjit [2013 09 24]
        public string LL_Analysis_1_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 1");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_1_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_2_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 2");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_2_Input_File.txt");
                }
                return "";
            }
        }

        public string LL_Analysis_3_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 3");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_3_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_4_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 4");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_4_Input_File.txt");
                }
                return "";
            }
        }


        public string LL_Analysis_5_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 5");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_5_Input_File.txt");
                }
                return "";
            }
        }
        public string LL_Analysis_6_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "LL Analysis Load 6");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "LL_Load_6_Input_File.txt");
                }
                return "";
            }
        }


        //Chiranjit [2012 05 27]
        public string DeadLoadAnalysis_Input_File
        {
            get
            {
                if (Directory.Exists(Working_Folder))
                {
                    string pd = Path.Combine(Working_Folder, "Dead Load Analysis");
                    if (!Directory.Exists(pd)) Directory.CreateDirectory(pd);
                    return Path.Combine(pd, "DeadLoad_Analysis_Input_File.txt");
                }
                return "";
            }
        }
        public string Total_Analysis_Report
        {
            get
            {
                if (!File.Exists(TotalAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(TotalAnalysis_Input_File), "ANALYSIS_REP.TXT");

            }
        }
        public string User_Input_Data
        {
            get
            {
                if (!Directory.Exists(Working_Folder)) return "";
                return Path.Combine(Working_Folder, "ASTRA_DATA_FILE.TXT");

            }
        }
        public string LiveLoad_Analysis_Report
        {
            get
            {
                if (!File.Exists(LiveLoadAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(LiveLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
            }
        }
        public string DeadLoad_Analysis_Report
        {
            get
            {
                if (!File.Exists(DeadLoadAnalysis_Input_File)) return "";
                return Path.Combine(Path.GetDirectoryName(DeadLoadAnalysis_Input_File), "ANALYSIS_REP.TXT");
            }
        }


        public string Get_Analysis_Report_File(string input_path)
        {

            if (!File.Exists(input_path)) return "";

            return Path.Combine(Path.GetDirectoryName(input_path), "ANALYSIS_REP.TXT");


        }
        public string Working_Folder
        {
            get
            {
                if (File.Exists(Input_File))
                    return Path.GetDirectoryName(Input_File);
                return "";
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
        #endregion Analysis Input File


        //Chiranjit [2013 05 02]
        string support_left_joints = "";
        string support_right_joints = "";

        //Chiranjit [2013 05 03]
        public List<string> joints_list_for_load = new List<string>();


        //Chiranjit [2011 08 01]
        //Create Bridge Input Data by user's given values.
        //Long Girder Spacing, Cross Girder Spacing, Cantilever Width
        public void CreateData1()
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

            Effective_Depth = Lwv;

            list_x.Clear();
            list_x.Add(0.0);

            last_x = Effective_Depth;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);


            last_x = 3 * Length / 16.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 4.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = 5 * Length / 16.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = 3 * Length / 8.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = 7 * Length / 16.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);

            last_x = Length / 2.0;
            last_x = MyList.StringToDouble(last_x.ToString("0.000"), 0.0);
            list_x.Add(last_x);



            int i = 0;
            for (i = 7; i >= 0; i--)
            {
                last_x = Length - list_x[i];
                list_x.Add(last_x);
            }




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

                    if (iCols == 0)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1)
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
                list_envelop_outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
            }
            else
            {
                list_envelop_outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]
        }
        public void CreateData()
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
            skew_length = 0;
            //double val1 = 12.1;
            double val1 = WidthBridge;
            double val2 = val1 * skew_length;


            List<double> supp_dist = new List<double>();

            double last_x = 0.0;
            double last_z = 0.0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            Hashtable z_table = new Hashtable();


            val1 = (Length - (Width_LeftCantilever + Width_RightCantilever)) / (Number_Of_Long_Girder - 1);


            val1 = Spacing_Cross_Girder;
            supp_dist.Clear();
            supp_dist.Add(Spacing_Cross_Girder * 2);
            supp_dist.Add(Spacing_Cross_Girder * 6);
            supp_dist.Add(Spacing_Cross_Girder * 10);
            supp_dist.Add(Spacing_Cross_Girder * 14);

            for (int c = 0; c < supp_dist.Count; c++)
            {
                supp_dist[c] = MyList.StringToDouble(supp_dist[c].ToString("f3"), 0.0);
            }


            //for (int c = 0; c < Number_Of_Long_Girder; c++)
            //{
            //    last_x = Width_LeftCantilever + c * val1;

            //    last_x = MyList.StringToDouble(last_x.ToString("f3"), 0.0);
            //    supp_dist.Add(last_x);
            //}

            supp_dist.Sort();
            last_x = 0.0;

            #region Chiranjit [2011 09 23] Correct Create Data
            bool flag = true;

            int i = 0;


            list_x.AddRange(supp_dist.ToArray());
            for (i = 0; i < 17; i++)
            {
                last_x = Length * (i / 16.0);
                flag = true;
                for (int j = 0; j < list_x.Count; j++)
                {
                    if (list_x[j].ToString("f2") == last_x.ToString("f2"))
                    {
                        flag = false; break;
                    }
                }
                if (flag)
                    list_x.Add(last_x);
            }

            last_x = x_incr + Effective_Depth;


            list_x.Sort();


            list_z.Clear();
            list_z.Add(0);


            last_z = WidthBridge;
            last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);
            list_z.Add(last_z);

            last_z = z_incr;
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

                if (!flag)
                    list_z.Add(last_z);
                last_z += z_incr;
                last_z = MyList.StringToDouble(last_z.ToString("0.000"), 0.0);

            } while (last_z <= WidthBridge);

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

                    if (iCols == 0)
                        support_left_joints += Joints_Array[iRows, iCols].NodeNo + " ";
                    else if (iCols == _Columns - 1)
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

            support_left_joints = "";
            support_right_joints = "";

            for (i = 0; i < Joints.Count; i++)
            {
                if (supp_dist.Contains(Joints[i].X))
                {
                    if (i > Joints.Count / 2)
                        support_right_joints += Joints[i].NodeNo + " ";
                    else
                        support_left_joints += Joints[i].NodeNo + " ";
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
                list_envelop_outer = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[2, 0].MemberNo + " TO " + Long_Girder_Members_Array[2, iCols - 2].MemberNo;
            }
            else
            {
                list_envelop_outer = Long_Girder_Members_Array[0, 0].MemberNo + " TO " + Long_Girder_Members_Array[0, iCols - 2].MemberNo;
                list_envelop_inner = Long_Girder_Members_Array[1, 0].MemberNo + " TO " + Long_Girder_Members_Array[1, iCols - 2].MemberNo;
            }
            #endregion Chiranjit [2013 06 06]

            Set_Inner_Outer_Cross_Girders();
        }

        public void CreateData_DeadLoad()
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

            //Store Joint Coordinates
            double L_2, L_4, eff_d, L_8;
            double x_max, x_min;

            last_x = 0.0;


            #region Chiranjit [2011 09 23] Correct Create Data

            list_x.Clear();

            int i = 0;

            for (i = 0; i < 17; i++)
            {
                list_x.Add(Length * (i / 16.0));
            }



            bool flag = true;

            list_x.Sort();


            list_z.Add(0);
            last_z = Width_LeftCantilever;

            list_z.Sort();
            #endregion Chiranjit [2011 09 23] Correct Create Data

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
        }

        public string Inner_Girders_as_String { get; set; }
        public string Outer_Girders_as_String { get; set; }
        public string Cross_Girders_as_String { get; set; }
        //Chiranjit [2011 07 11]
        //Write Default data as given Astra Examples 8


        void Set_Inner_Outer_Cross_Girders()
        {

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();

            for (int i = 0; i < MemColls.Count; i++)
            {

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000")) ||
                    (MemColls[i].StartNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000")))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z == WidthBridge) &&
                    (MemColls[i].EndNode.Z == WidthBridge))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    Inner_Girder.Add(MemColls[i].MemberNo);
                }
            }
            Inner_Girder.Sort();
            Outer_Girder.Sort();
            Cross_Girder.Sort();


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            Inner_Girders_as_String = MyList.Get_Array_Text(Inner_Girder);
            Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);

        }


        public void WriteData_Total_Analysis(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            List<int> Inner_Girder = new List<int>();
            List<int> Outer_Girder = new List<int>();
            List<int> Cross_Girder = new List<int>();

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS");
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

                if ((MemColls[i].StartNode.Z.ToString("0.000") != MemColls[i].EndNode.Z.ToString("0.000")))
                {
                    Cross_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == Width_LeftCantilever.ToString("0.000")) ||
                    (MemColls[i].StartNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000") &&
                    MemColls[i].EndNode.Z.ToString("0.000") == (WidthBridge - Width_RightCantilever).ToString("0.000")))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else if ((MemColls[i].StartNode.Z == 0.0 &&
                    MemColls[i].EndNode.Z == 0.0) ||
                    (MemColls[i].StartNode.Z == WidthBridge) &&
                    (MemColls[i].EndNode.Z == WidthBridge))
                {
                    Outer_Girder.Add(MemColls[i].MemberNo);
                }
                else
                {
                    Inner_Girder.Add(MemColls[i].MemberNo);
                }
            }
            Inner_Girder.Sort();
            Outer_Girder.Sort();
            Cross_Girder.Sort();


            Cross_Girders_as_String = MyList.Get_Array_Text(Cross_Girder);
            Inner_Girders_as_String = MyList.Get_Array_Text(Inner_Girder);
            Outer_Girders_as_String = MyList.Get_Array_Text(Outer_Girder);

            list.Add("SECTION PROPERTIES");
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));

            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));
            //list.Add("111 112 113 114 115 116 117 118 119 120 121  PINNED");
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
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_inner);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name), true, iApp.DesignStandard);
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }
        public void WriteData_LiveLoad_Analysis(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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
            //1 TO 82 PRIS YD 0.2 ZD 1
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));

            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

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
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));
            list.Clear();
        }
        public void WriteData_LiveLoad_Analysis(string file_name, List<string> ll_loads)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH MOVING LOAD");
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
            //1 TO 82 PRIS YD 0.2 ZD 1
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));

            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));

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
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());

            //iApp.Write_LiveLoad_LL_TXT(Path.GetDirectoryName(file_name));


            string fn = Path.Combine(Path.GetDirectoryName(file_name), "LL.TXT");
            File.WriteAllLines(fn, ll_loads.ToArray());

            list.Clear();
        }

        public void WriteData_DeadLoad_Analysis(string file_name)
        {

            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR RCC T GIRDER BRIDGE DECK ANALYSIS WITH DEAD LOAD");
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
            //1 TO 82 PRIS YD 0.2 ZD 1
            list.Add(string.Format("{0} PRIS YD 0.2 ZD 1", "1 TO " + MemColls.Count));


            list.Add("MATERIAL CONSTANT");
            list.Add("E 2.85E6 ALL");
            list.Add("DENSITY CONCRETE ALL");
            list.Add("POISSON CONCRETE ALL");
            list.Add("SUPPORT");
            list.Add(string.Format("{0}  PINNED", support_left_joints));
            //list.Add(string.Format("{0}  PINNED", support_right_joints));
            list.Add(string.Format("{0}  FIXED BUT FX MZ", support_right_joints));
            //list.Add("1 3 5 7 9 11 PINNED");
            //list.Add("111 113 115 117 119 121 PINNED");
            list.Add("LOAD 1 DEAD LOAD");
            list.Add("MEMBER LOAD");
            list.Add(string.Format("{0} UNI GY -2.754", Inner_Girders_as_String));
            list.Add("PRINT SUPPORT REACTIONS");
            list.Add("PRINT MAX FORCE ENVELOPE LIST " + list_envelop_outer);
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            list.Add(kStr);
            File.WriteAllLines(file_name, list.ToArray());
            //iApp.Write_LiveLoad_LL_TXT(Working_Folder, true, iApp.DesignStandard);
            list.Clear();
        }

        public void Clear()
        {
            Joints_Array = null;
            Long_Girder_Members_Array = null;
            Cross_Girder_Members_Array = null;
            MemColls.Clear();
            MemColls = null;
        }
        public void LoadReadFromGrid(DataGridView dgv_live_load)
        {
            LoadData ld = new LoadData();
            int i = 0;
            LoadList_1 = new List<LoadData>();
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
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
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
                        LoadList_1.Add(ld);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #region Set Loads

            List<LoadData> LoadList_tmp = new List<LoadData>(LoadList_1.ToArray());

            LoadList_2 = new List<LoadData>();
            LoadList_3 = new List<LoadData>();
            LoadList_4 = new List<LoadData>();
            LoadList_5 = new List<LoadData>();
            LoadList_6 = new List<LoadData>();

            LoadList_1.Clear();

            //70 R Wheel
            ld = new LoadData(Live_Load_List[6]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_1.Add(ld);


            //1 Lane Class A
            ld = new LoadData(Live_Load_List[0]);

            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_2.Add(ld);



            //2 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_3.Add(ld);



            //3 Lane Class A
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[2].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_4.Add(ld);


            //1 Lane Class A + 70 RW
            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);

            ld = new LoadData(Live_Load_List[6]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_5.Add(ld);



            //70 RW + 1 Lane Class A 
            ld = new LoadData(Live_Load_List[6]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[0].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            ld = new LoadData(Live_Load_List[0]);
            ld.X = ld.Distance;
            ld.Y = LoadList_tmp[0].Y;
            ld.Z = LoadList_tmp[1].Z;
            ld.XINC = LoadList_tmp[0].XINC;

            LoadList_6.Add(ld);

            #endregion Set Loads

        }
    }

    public class TGirder_Section_Properties
    {

        //Width of Slab		W	=
        //Depth of slab		Ds	=
        //Width of top flange		wtf	=
        //Width of bottom flange		bwf	=
        //Width of web		bw	=
        //Thickness of top flange		D1	=
        //Thickness of top haunch		D2	=
        //Thickness of bottom haunch		D3	=
        //Thickness of bottom flange		D4	=
        //Depth of girder		d	=
        //Modulus of Elasticity of Deck = Es_Deck
        //Modulus of Elasticity of Girder = Es_Girder


        public double W, Ds, wtf, bwf, bw, D1, D2, D3, D4, d, Es_Girder, Es_Deck;
        public bool Is_End_Cross_Girder { get; set; }
        public TGirder_Section_Properties()
        {
            W = 0.0;
            Ds = 0.0;
            wtf = 0.0;
            bwf = 0.0;
            bw = 0.0;
            D1 = 0.0;
            D2 = 0.0;
            D3 = 0.0;
            D4 = 0.0;
            d = 0.0;

            //PSC I Girder
            //Es_Deck = 32500;
            //Es_Girder = 33500;

            //RCC T Girder
            Es_Deck = 200000;
            Es_Girder = 200000;


            Is_End_Cross_Girder = false;
        }
        #region Properties
        public double Width_Of_Slab
        {
            get
            {
                return W;
            }
            set
            {
                W = value;
            }
        }
        public double Depth_of_slab
        {
            get
            {
                return Ds;
            }
            set
            {
                Ds = value;
            }
        }

        public double Width_of_top_flange
        {
            get
            {
                return wtf;
            }
            set
            {
                wtf = value;
            }
        }
        public double Width_of_bottom_flange
        {
            get
            {
                return bwf;
            }
            set
            {
                bwf = value;
            }
        }
        public double Width_of_web
        {
            get
            {
                return bw;
            }
            set
            {
                bw = value;
            }
        }
        public double Thickness_of_top_flange
        {
            get
            {
                return D1;
            }
            set
            {
                D1 = value;
            }
        }
        public double Thickness_of_top_haunch
        {
            get
            {
                return D2;
            }
            set
            {
                D2 = value;
            }
        }
        public double Thickness_of_bottom_haunch
        {
            get
            {
                return D3;
            }
            set
            {
                D3 = value;
            }
        }
        public double Thickness_of_bottom_flange
        {
            get
            {
                return D4;
            }
            set
            {
                D4 = value;
            }
        }
        public double Depth_of_girder
        {
            get
            {
                return d;
            }
            set
            {
                d = value;
            }
        }
        #endregion Properties

        #region Deck Slab Properties
        public double Deck_Slab_A
        {
            get
            {
                return W * Ds * (Es_Deck / Es_Girder);
            }
        }
        public double Deck_Slab_Y
        {
            get
            {
                return Ds / 2.0 + d;
            }
        }
        public double Deck_Slab_AY
        {
            get
            {
                return Deck_Slab_A * Deck_Slab_Y;
            }
        }
        public double Deck_Slab_AYY
        {
            get
            {
                return Deck_Slab_A * Deck_Slab_Y * Deck_Slab_Y;
            }
        }
        public double Deck_Slab_Ix
        {
            get
            {
                return W * Ds * Ds * Ds / 12.0;
            }
        }
        #endregion Deck Slab


        #region Rectangle Top Flange Properties
        public double Rectangle_Top_Flange_A
        {
            get
            {
                if (D1 != 0)
                    return (wtf - bw) * D1;

                return 0.0;
            }
        }
        public double Rectangle_Top_Flange_Y
        {
            get
            {
                return (d - D1 / 2.0);
            }
        }
        public double Rectangle_Top_Flange_AY
        {
            get
            {
                return Rectangle_Top_Flange_A * Rectangle_Top_Flange_Y;
            }
        }
        public double Rectangle_Top_Flange_AYY
        {
            get
            {
                return Rectangle_Top_Flange_AY * Rectangle_Top_Flange_Y;
            }
        }
        public double Rectangle_Top_Flange_Ix
        {
            get
            {
                if (D1 != 0)
                {
                    return (wtf - bw) * D1 * D1 * D1 / 12.0;
                }
                return 0.0;
            }
        }
        #endregion Rectangle_Top_Flange

        #region Triangular Top Flange Properties
        public double Triangular_Top_Flange_A
        {
            get
            {
                if (D2 != 0)
                    return 0.5 * (wtf - bw) * D2;

                return 0.0;
            }
        }
        public double Triangular_Top_Flange_Y
        {
            get
            {
                return (d - D1 - (D2 - D1) / 3.0);
            }
        }
        public double Triangular_Top_Flange_AY
        {
            get
            {
                return Triangular_Top_Flange_A * Triangular_Top_Flange_Y;
            }
        }
        public double Triangular_Top_Flange_AYY
        {
            get
            {
                return Triangular_Top_Flange_AY * Triangular_Top_Flange_Y;
            }
        }
        public double Triangular_Top_Flange_Ix
        {
            get
            {
                if (D1 != 0)
                {
                    return (wtf - bw) * D2 * D2 * D2 / 36.0;
                }
                return 0.0;
            }
        }
        #endregion Triangular_Top_Flange


        #region Web + top & bottom rectangle haunch Properties
        public double Web_A
        {
            get
            {
                return bw * d;
            }
        }
        public double Web_Y
        {
            get
            {
                return (d / 2.0);
            }
        }
        public double Web_AY
        {
            get
            {
                return Web_A * Web_Y;
            }
        }
        public double Web_AYY
        {
            get
            {
                return Web_AY * Web_Y;
            }
        }
        public double Web_Ix
        {
            get
            {
                return (bw) * d * d * d / 12.0;
            }
        }
        #endregion Web

        #region Bottom Bulb, Triangle Properties
        public double Bottom_Bulb_Triangle_A
        {
            get
            {
                if (D3 != 0)
                    return 0.5 * (bwf - bw) * D3;
                return 0.0;
            }
        }
        public double Bottom_Bulb_Triangle_Y
        {
            get
            {
                return (D4 + D3 / 3.0);
            }
        }
        public double Bottom_Bulb_Triangle_AY
        {
            get
            {
                return Bottom_Bulb_Triangle_A * Bottom_Bulb_Triangle_Y;
            }
        }
        public double Bottom_Bulb_Triangle_AYY
        {
            get
            {
                return Bottom_Bulb_Triangle_AY * Bottom_Bulb_Triangle_Y;
            }
        }
        public double Bottom_Bulb_Triangle_Ix
        {
            get
            {
                return (bwf - bw) * D3 * D3 * D3 / 36.0;
            }
        }
        #endregion Web

        #region Bottom Bulb, Rectangle Properties
        public double Bottom_Bulb_Rectangle_A
        {
            get
            {
                if (D4 != 0)
                    return (bwf - bw) * D4;
                return 0.0;
            }
        }
        public double Bottom_Bulb_Rectangle_Y
        {
            get
            {
                return (D4 / 2.0);
            }
        }
        public double Bottom_Bulb_Rectangle_AY
        {
            get
            {
                return Bottom_Bulb_Rectangle_A * Bottom_Bulb_Rectangle_Y;
            }
        }
        public double Bottom_Bulb_Rectangle_AYY
        {
            get
            {
                return Bottom_Bulb_Rectangle_AY * Bottom_Bulb_Rectangle_Y;
            }
        }
        public double Bottom_Bulb_Rectangle_Ix
        {
            get
            {
                return (bwf - bw) * D4 * D4 * D4 / 36.0;
            }
        }
        #endregion Bottom_Bulb_Rectangle


        #region Composite Section Properties
        public double Composite_Section_A
        {
            get
            {

                return (Deck_Slab_A + Rectangle_Top_Flange_A + Triangular_Top_Flange_A
                    + Web_A + Bottom_Bulb_Triangle_A + Bottom_Bulb_Rectangle_A);
            }
        }
        public double Composite_Section_Y
        {
            get
            {

                return (Composite_Section_AY / Composite_Section_A);
            }
        }
        public double Composite_Section_AY
        {
            get
            {

                return (Deck_Slab_AY + Rectangle_Top_Flange_AY + Triangular_Top_Flange_AY
                    + Web_AY + Bottom_Bulb_Triangle_AY + Bottom_Bulb_Rectangle_AY);
            }
        }
        public double Composite_Section_AYY
        {
            get
            {

                return (Deck_Slab_AYY + Rectangle_Top_Flange_AYY + Triangular_Top_Flange_AYY
                    + Web_AYY + Bottom_Bulb_Triangle_AYY + Bottom_Bulb_Rectangle_AYY);
            }
        }
        public double Composite_Section_Ix
        {
            get
            {
                return (Deck_Slab_Ix
                    + Rectangle_Top_Flange_Ix
                    + Triangular_Top_Flange_Ix
                    + Web_Ix
                    + Bottom_Bulb_Triangle_Ix
                    + Bottom_Bulb_Rectangle_Ix);
            }
        }

        public double Composite_Section_Iz
        {
            get
            {
                return (Deck_Slab_Ix + Deck_Slab_AYY
                    + Rectangle_Top_Flange_Ix + Rectangle_Top_Flange_AYY
                    + Triangular_Top_Flange_Ix + Triangular_Top_Flange_AYY
                    + Web_Ix + Web_AYY
                    + Bottom_Bulb_Triangle_Ix + Bottom_Bulb_Triangle_AYY
                    + Bottom_Bulb_Rectangle_Ix + Bottom_Bulb_Rectangle_AYY) - Composite_Section_A * Composite_Section_Y * Composite_Section_Y;
            }
        }

        #endregion Composite_Section

        #region Girder Section Properties
        public double Girder_Section_A
        {
            get
            {

                return (Rectangle_Top_Flange_A
                    + Triangular_Top_Flange_A
                    + Web_A
                    + Bottom_Bulb_Triangle_A
                    + Bottom_Bulb_Rectangle_A);
            }
        }

        public double Girder_Section_Y
        {
            get
            {

                return (Girder_Section_AY / Girder_Section_A);
            }
        }

        public double Girder_Section_AY
        {
            get
            {

                return (Rectangle_Top_Flange_AY
                    + Triangular_Top_Flange_AY
                    + Web_AY
                    + Bottom_Bulb_Triangle_AY
                    + Bottom_Bulb_Rectangle_AY);
            }
        }
        public double Girder_Section_AYY
        {
            get
            {

                return (Rectangle_Top_Flange_AYY
                    + Triangular_Top_Flange_AYY
                    + Web_AYY
                    + Bottom_Bulb_Triangle_AYY
                    + Bottom_Bulb_Rectangle_AYY);
            }
        }
        public double Girder_Section_Ix
        {
            get
            {
                return (Rectangle_Top_Flange_Ix
                    + Triangular_Top_Flange_Ix
                    + Web_Ix
                    + Bottom_Bulb_Triangle_Ix
                    + Bottom_Bulb_Rectangle_Ix);
            }
        }

        public double Girder_Section_Iz
        {
            get
            {
                return (Rectangle_Top_Flange_Ix + Rectangle_Top_Flange_AYY
                    + Triangular_Top_Flange_Ix + Triangular_Top_Flange_AYY
                    + Web_Ix + Web_AYY
                    + Bottom_Bulb_Triangle_Ix + Bottom_Bulb_Triangle_AYY
                    + Bottom_Bulb_Rectangle_Ix + Bottom_Bulb_Rectangle_AYY) - Girder_Section_A * Girder_Section_Y * Girder_Section_Y;
            }
        }
        #endregion Girder_Section
    }
}
