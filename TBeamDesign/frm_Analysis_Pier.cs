using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.TrussBridge;
using AstraFunctionOne;


namespace BridgeAnalysisDesign
{
    public partial class frm_Analysis_Pier : Form
    {
        
        IApplication iApp;
        public JointNodeCollection Joints { get; set; }
        JointNode[,] Joints_Array;
        Member[,] Long_Girder_Members_Array;
        Member[,] Cross_Girder_Members_Array;
        public MemberCollection MemColls { get; set; }

        BridgeMemberAnalysis Truss_Analysis = null;
        //CompleteDesign complete_design = null;
        List<LoadData> LoadList = null;
        List<LoadData> Live_Load_List = null;
        TotalDeadLoad SIDL = null;

        int _Columns = 0, _Rows = 0;

        double span_length = 0.0;


        string input_file, working_folder, user_path;
        public frm_Analysis_Pier(IApplication thisApp)
        {
            iApp = thisApp;
            InitializeComponent();
            input_file = working_folder = "";
            //Total_Rows = 0; Total_Columns = 0;
        }

        #region Properties

        public double Length
        {
            get
            {
                return MyList.StringToDouble(txt_length.Text, 0.0);
            }
            set
            {
                txt_length.Text = value.ToString();
            }
        }
        public double WidthBridge
        {
            get
            {
                return MyList.StringToDouble(txt_width.Text, 0.0);
            }
            set
            {
                txt_width.Text = value.ToString();
            }
        }
        public double Effective_Depth
        {
            get
            {
                return MyList.StringToDouble(txt_eff_depth.Text, 0.0);
            }
            set
            {
                txt_eff_depth.Text = value.ToString();
            }
        }
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
        public double Skew_Angle
        {
            get
            {
                return MyList.StringToDouble(txt_skew_angle.Text, 0.0);
            }
            set
            {
                txt_skew_angle.Text = value.ToString();
            }
        }

        public double WidthCantilever
        {
            get
            {
                return MyList.StringToDouble(txt_width_cantilever.Text, 0.0);
            }
            set
            {
                txt_width_cantilever.Text = value.ToString();
            }
        }
        public double Spacing_Long_Girder
        {
            get
            {
                return MyList.StringToDouble(((WidthBridge - (2 * WidthCantilever)) / 6.0).ToString("0.000"), 0.0);
            }
            set
            {
                //txt_long_girder_spacing.Text = value.ToString();
            }
        }
        public double Spacing_Cross_Girder
        {
            get
            {
                //return MyList.StringToDouble(txt_cross_girder_spacing.Text, 0.0);
                return MyList.StringToDouble(((Length) / 8.0).ToString("0.000"), 0.0);
            }
            set
            {
                //txt_cross_girder_spacing.Text = value.ToString();
            }
        }


        #endregion Properties

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
                input_file = value;
                working_folder = Path.GetDirectoryName(input_file);
                user_path = working_folder;
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

        //Chiranjit [2011 07 09]
        //Create Bridge Input Data by user's given values.
        //Long Girder Spacing, Cross Girder Spacing, Cantilever Width
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
            double L_2, L_4, eff_d;
            double x_max, x_min;

            //int _Columns, _Rows;

            //_Columns = Total_Columns;
            //_Rows = Total_Rows;

            last_x = 0.0;
            //Creating X Coordinates at every Z level

            list_x.Clear();
            list_x.Add(0.0);
            list_x.Add(Effective_Depth);
            list_x.Add(Length - Effective_Depth);
            list_x.Add(Length / 4.0);
            list_x.Add(Length / 2.0);
            list_x.Add(Length);
            last_x = x_incr;
            do
            {
                if (!list_x.Contains(last_x) && last_x > Effective_Depth && last_x < (Length - Effective_Depth))
                    list_x.Add(last_x);
                last_x += x_incr;

            }
            while (last_x <= Length);

            list_x.Sort();

            list_z.Clear();
            list_z.Add(0);
            list_z.Add(WidthCantilever);
            list_z.Add(WidthCantilever / 2);
            list_z.Add(WidthBridge - WidthCantilever);
            list_z.Add(WidthBridge - WidthCantilever / 2);
            list_z.Add(WidthBridge);
            last_z = WidthCantilever + z_incr;
            do
            {
                if (!list_z.Contains(last_z) && last_z > WidthCantilever && last_z < (WidthBridge - WidthCantilever-0.2))
                    list_z.Add(last_z);
                last_z += z_incr;

            } while (last_z <= WidthBridge);
            list_z.Sort();





            _Columns = list_x.Count;
            _Rows = list_z.Count;

            int i = 0;

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



            //Chiranjit [2011 07 11]
            //Calculate L/2, L/4, Deff for Skew Angle
            //for(i = 0; i < list_z.Count; i++)
            //{
            //    list_x = z_table[list_z[i]] as List<double>;
            //    if (list_x != null)
            //    {
            //        list_x.Sort();

            //        //x_max = list_x[list_x.Count - 1] + (skew_length * list_z[i]);
            //        //x_min = list_x[0] + (skew_length * list_z[i]);
            //        x_max = list_x[list_x.Count - 1];
            //        x_min = list_x[0];
            //        if (i == 0)
            //        {
            //            span_length = (x_max - x_min);
            //            //Length = span_length;
            //        }

            //        L_2 = (x_max + x_min) / 2.0;
            //        L_4 = (L_2 + x_min) / 2.0;
            //        eff_d = (Effective_Depth + x_min);

            //        if (list_x.Contains(eff_d) == false)
            //            list_x.Add(eff_d);
            //        if (list_x.Contains(L_2) == false)
            //            list_x.Add(L_2);
            //        if (list_x.Contains(L_4) == false)
            //            list_x.Add(L_4);

            //        L_2 = (x_max + x_min) / 2.0;
            //        L_4 = x_max - L_4;
            //        eff_d = x_max - eff_d;
            //        if (list_x.Contains(eff_d) == false)
            //            list_x.Add(eff_d);
            //        if (list_x.Contains(L_2) == false)
            //            list_x.Add(L_2);
            //        if (list_x.Contains(L_4) == false)
            //            list_x.Add(L_4);


            //        list_x.Sort();

            //        //_Columns = list_x.Count;
            //        //_Rows = list_z.Count;

            //        z_table[list_z[i]] = list_x;
            //    }
            //}

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
                    Joints_Array[iRows, iCols].NodeNo = nodeNo;
                    Joints.Add(Joints_Array[iRows, iCols]);

                }
            }


            Member mem = new Member();
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
        //Chiranjit [2011 07 11]
        //Write Default data as given Astra Examples 8
        public void WriteData(string file_name)
        {
            string kStr = "";
            List<string> list = new List<string>();
            int i = 0;

            list.Add("ASTRA FLOOR BRIDGE DECK ANALYSIS FOR PIER");
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
            list.Add(txt_support_end1.Text.Replace(",", " ") + " PINNED");
            list.Add(txt_support_end2.Text.Replace(",", " ") + " PINNED");
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
            list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            list.Add("TYPE 1 CLA 1.179");
            list.Add("TYPE 2 A70R 1.188");
            list.Add("TYPE 3 A70RT 1.10");
            list.Add("TYPE 4 CLAR 1.179");
            list.Add("TYPE 5 A70RR 1.188");
            list.Add("**** 3 LANE CLASS A *****");
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
            list.Add("PRINT MAX FORCE ENVELOPE LIST 131 TO 140");
            list.Add("PRINT MAX FORCE ENVELOPE LIST 151 TO 160");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");


            list.Add(kStr);



            File.WriteAllLines(file_name, list.ToArray());

            list.Clear();
            file_name = Path.Combine(working_folder, "LL.TXT");
            //list.Add("FILE LL.TXT");
            //list.Add("");
            //list.Add("TYPE1 IRCCLASSA");
            //list.Add("68 68 68 68 114 114 114 27");
            //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
            //list.Add("1.80");
            //list.Add("");
            //list.Add("TYPE 2 IRCCLASSB");
            //list.Add("41 41 41 41 68 68 16 16");
            //list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
            //list.Add("1.80");
            //list.Add("");
            //list.Add("TYPE 3 IRC70RTRACK");
            //list.Add("70 70 70 70 70 70 70 70 70 70");
            //list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
            //list.Add("0.84");
            //list.Add("");
            //list.Add("TYPE 4 IRC70RWHEEL");
            //list.Add("170 170 170 170 120 120 80");
            //list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
            //list.Add("0.450 1.480 0.450");
            //list.Add("");
            //list.Add("TYPE 5 IRCCLASSAATRACK");
            //list.Add("70 70 70 70 70 70 70 70 70 70");
            //list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
            //list.Add("0.85");
            //list.Add("");
            //list.Add("TYPE 6 IRC24RTRACK");
            //list.Add("25 25 25 25 25 25 25 25 25 25");
            //list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
            //list.Add("0.36");
            list.Add("FILE LL.TXT");
            list.Add("");
            list.Add("TYPE1 IRCCLASSA");
            list.Add("6.8 6.8 6.8 6.8 11.4 11.4 11.4 2.7");
            list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
            list.Add("1.80");
            list.Add("");
            list.Add("TYPE 2 IRCCLASSB");
            list.Add("4.1 4.1 4.1 4.1 6.8 6.8 1.6 1.6");
            list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
            list.Add("1.80");
            list.Add("");
            list.Add("TYPE 3 IRC70RTRACK");
            list.Add("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0");
            list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
            list.Add("0.84");
            list.Add("");
            list.Add("TYPE 4 IRC70RWHEEL");
            list.Add("17.0 17.0 17.0 17.0 12.0 12.0 8.0");
            list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
            list.Add("0.450 1.480 0.450");
            list.Add("");
            list.Add("TYPE 5 IRCCLASSAATRACK");
            list.Add("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0");
            list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
            list.Add("0.85");
            list.Add("");
            list.Add("TYPE 6 IRC24RTRACK");
            list.Add("2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5");
            list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
            list.Add("0.36");
            list.Add("");
            //list.Add("TYPE 7  BROADGAUGERAIL");
            //list.Add("44 66 66 93 93 93 93");
            //list.Add("3.96 1.52 2.13 1.37 3.05 1.37");
            //list.Add("1.93");
            File.WriteAllLines(file_name, list.ToArray());
        }

        void SetWorkingFolder()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = user_path;
                fbd.Description = "Select your Working Folder";
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                    {
                        Input_File = fbd.SelectedPath;
                    }
                }
            }
        }


        public bool OpenAnalysisFile(string file_name)
        {
            string analysis_file = file_name;
            if (File.Exists(analysis_file))
            {
                btn_view_structure.Enabled = true;

                Input_File = (file_name);
                string rep_file = Path.Combine(user_path, "ANALYSIS_REP.TXT");
                //if (File.Exists(rep_file))
                //{
                //    Truss_Analysis = new SteelTrussMemberAnalysis(rep_file);
                //}
                //else
                this.Refresh();
                iApp.ShowPleaseWait("Reading Result please wait.....");
                Truss_Analysis = new BridgeMemberAnalysis(iApp, analysis_file);


                txt_length.Text = Truss_Analysis.Analysis.Length.ToString();
                txt_X.Text = "-" + txt_length.Text;
                txt_width.Text = Truss_Analysis.Analysis.Width.ToString();
                txt_gd_np.Text = (Truss_Analysis.Analysis.NoOfPanels - 1).ToString("0");
                txt_analysis_file.Visible = true;
                txt_analysis_file.Text = analysis_file;

                //if (File.Exists(kFile))
                //{
                //    //Read_DL_SIDL();
                //    //Read_Live_Load();
                //}

                iApp.ClosePleaseWait();
                this.Refresh();
                MessageBox.Show(this, "File opened successfully.");
            }


            string ll_txt = Path.Combine(user_path, "LL.txt");

            Live_Load_List = LoadData.GetLiveLoads(ll_txt);

            if (Live_Load_List == null) return true;

            cmb_load_type.Items.Clear();

            return true;
        }

        void LoadReadFromGrid()
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
                    ld.X = MyList.StringToDouble(dgv_live_load[1, i].Value.ToString(), -60.0);
                    ld.Y = MyList.StringToDouble(dgv_live_load[2, i].Value.ToString(), 0.0);
                    ld.Z = MyList.StringToDouble(dgv_live_load[3, i].Value.ToString(), 1.0);
                    ld.XINC = MyList.StringToDouble(dgv_live_load[4, i].Value.ToString(), 0.5);
                    LoadList.Add(ld);
                }
                catch (Exception ex) { }
            }
        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("UNIT KN ME");

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = null;


            if (chk_active_LL.Checked)
            {
                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
                load_lst.Add("TYPE 1 CLA 1.179");
                load_lst.Add("TYPE 2 CLB 1.188");
                load_lst.Add("TYPE 3 A70RT 1.10");
                load_lst.Add("TYPE 4 CLAR 1.179");
                load_lst.Add("TYPE 5 A70RR 1.188");
                load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
                load_lst.Add("TYPE 7 RAILBG 1.25");


                load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);

                LoadReadFromGrid();
                foreach (LoadData ld in LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }

            }
            if (calc_width > WidthBridge)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + WidthBridge;

                str = str + "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }

        List<string> Get_Joints_Load(double load)
        {
            MemberCollection mc = new MemberCollection(Truss_Analysis.Analysis.Members);

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


            z_min = Truss_Analysis.Analysis.Joints.MinZ;
            double z_max = Truss_Analysis.Analysis.Joints.MaxZ;


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

        public void Button_Enable_Disable()
        {
            btn_view_data.Enabled = File.Exists(Input_File);
            btn_view_structure.Enabled = File.Exists(Input_File);
            btn_View_Moving_Load.Enabled = File.Exists(Analysis_Report);
            btn_view_report.Enabled = File.Exists(Analysis_Report);
            btn_View_Design_forces.Enabled = File.Exists(Analysis_Report);

            btn_process_analysis.Enabled = File.Exists(Input_File);
        }


        #region Form Events
        private void frmTBeamAnalysis_Load(object sender, EventArgs e)
        {
            Joints = new JointNodeCollection();
            MemColls = new MemberCollection();
            Button_Enable_Disable();

            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 2.75, 0.2);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 6.25, 0.2);
            dgv_live_load.Rows.Add("TYPE 1 : IRCCLASSA", -18.8, 0, 9.75, 0.2);
        }

        private void btn_create_data_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Filter = "All Text Files(*.txt)|*.txt";
                ofd.DefaultExt = ".txt";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    Input_File = ofd.FileName;
                    CreateData();
                    WriteData(Input_File);
                    Write_Load_Data();
                    Truss_Analysis = new BridgeMemberAnalysis(iApp, Input_File);

                    string ll_txt = Path.Combine(working_folder, "LL.txt");

                    Live_Load_List = LoadData.GetLiveLoads(ll_txt);

                    if (Live_Load_List == null) return;

                    cmb_load_type.Items.Clear();
                    for (int i = 0; i < Live_Load_List.Count; i++)
                    {
                        cmb_load_type.Items.Add(Live_Load_List[i].TypeNo + " : " + Live_Load_List[i].Code);
                    }
                    if (cmb_load_type.Items.Count > 0)
                    {
                        cmb_load_type.SelectedIndex = cmb_load_type.Items.Count - 1;
                        //if (dgv_live_load.RowCount == 0)
                        //Add_LiveLoad();
                    }
                }
            }
            Button_Enable_Disable();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Joints_Array = null;
            Long_Girder_Members_Array = null;
            Cross_Girder_Members_Array = null;
            MemColls.Clear();
            MemColls = null;

            this.Close();
        }

        private void btn_view_report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Analysis_Report);


        }

        private void btn_working_folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (Directory.Exists(working_folder))
                    fbd.SelectedPath = working_folder;
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    working_folder = fbd.SelectedPath;
                }
            }
        }


        private void btn_view_data_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Input_File);
        }

        private void btn_view_structure_Click(object sender, EventArgs e)
        {
            if (File.Exists(Input_File))
                iApp.OpenWork(Input_File, false);
        }

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            string flPath = Input_File;
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(flPath), "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            File.WriteAllText(Path.Combine(iApp.AppFolder, "PAT001.tmp"), Path.GetDirectoryName(flPath) + "\\");
            //System.Environment.SetEnvironmentVariable("SURVEY", flPath);

            System.Diagnostics.Process prs = new System.Diagnostics.Process();

            System.Environment.SetEnvironmentVariable("SURVEY", flPath);
            System.Environment.SetEnvironmentVariable("ASTRA", flPath);


            prs.StartInfo.FileName = Path.Combine(Application.StartupPath, "ast001.exe");
            if (prs.Start())
                prs.WaitForExit();

            this.Refresh();
            iApp.ShowPleaseWait(" Please wait....");

            string ana_rep_file = Analysis_Report;
            //if (File.Exists(ana_rep_file))
            //{
            //    Truss_Analysis = null;
            //    //Truss_Analysis = new SteelTrussMemberAnalysis(ana_rep_file);
            //}

            //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            //grb_select_analysis.Enabled = !rbtn_create_analysis_file.Checked;

            //grb_create_input_data.Enabled = !rbtn_select_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_select_analysis_file.Checked;

            //if (rbtn_create_analysis_file.Checked)
            //{
            //    if (!Directory.Exists(user_path))
            //    {
            //        SetWorkingFolder();
            //    }

            //    txt_width.Text = "12.0";
            //    txt_length.Text = "21.0";
            //    txt_eff_depth.Text = "2.3";
            //    txt_rows.Text = "7";
            //    txt_cols.Text = "5";
            //    txt_skew_angle.Text = "0";
            //}
            Button_Enable_Disable();
            if (File.Exists(Analysis_Report))
                Show_Design_Forces();
            iApp.ClosePleaseWait();
            this.Refresh();

        }
        public void Start_Please_wait()
        {

        }
        private void btn_add_load_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_live_load.Rows.Add(cmb_load_type.Text, txt_X.Text, txt_Y.Text, txt_Z.Text, txt_XINCR.Text);
            }
            catch (Exception ex) { }
        }

        private void Write_Load_Data()
        {
            string file_name = Input_File;

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
            s += (chk_active_SIDL.Checked ? " + SIDL " : "");
            s += (chk_active_LL.Checked ? " + LL " : "");

            load_lst.Add("LOAD    1   " + s);
            load_lst.Add("MEMBER LOAD");
            if (chk_active_SIDL.Checked)
            {
                load_lst.AddRange(txt_member_load.Lines);

                if (dgv_live_load.RowCount != 0)
                {
                    if (!File.Exists(Path.Combine(user_path, "LL.TXT")))
                    {
                        MessageBox.Show(this, "Load data file \"LL.TXT\" not found in working folder " + user_path);
                    }

                    LoadReadFromGrid();
                }
            }
            else
            {
                load_lst.Add("1 TO 220 UNI GY -0.001");
            }
            load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
            File.WriteAllLines(file_name, inp_file_cont.ToArray());
            MessageBox.Show(this, "Load data is added in file " + file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void rbtn_select_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            //grb_create_input_data.Enabled = rbtn_create_analysis_file.Checked;
            grb_select_analysis.Enabled = rbtn_select_analysis_file.Checked;
            btn_create_data.Enabled = rbtn_create_analysis_file.Checked;
            Button_Enable_Disable();
        }
        private void txt_custom_LL_Xcrmt_TextChanged(object sender, EventArgs e)
        {
        }
        private void btn_browse_input_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File (*.txt)|*.txt";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    OpenAnalysisFile(ofd.FileName);
                    if (File.Exists(Analysis_Report))
                        Show_Design_Forces();
                }
            }
            Button_Enable_Disable();
        }
        #endregion Form Events

        private void dgv_SIDL_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btn_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_live_load.Rows.RemoveAt(dgv_live_load.CurrentRow.Index);
            }
            catch (Exception ex) { }
        }
        private void btn_live_load_remove_all_Click(object sender, EventArgs e)
        {
            dgv_live_load.Rows.Clear();

        }

        private void btn_View_Moving_Load_Click(object sender, EventArgs e)
        {
            if (File.Exists(Input_File))
                iApp.OpenWork(Input_File, true);
        }

        private void txt_length_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_X.Text = txt_length.Text;
            }
            catch (Exception ex) { }
        }

        private void txt_width_TextChanged(object sender, EventArgs e)
        {
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            grb_LL.Enabled = chk_active_LL.Checked;
            grb_SIDL.Enabled = chk_active_SIDL.Checked;
        }
        private void btn_View_design_forces_Click(object sender, EventArgs e)
        {
            Show_Design_Forces();
        }
        void Show_Design_Forces()
        {

            frm_Pier_ViewDesign_Forces fv = new frm_Pier_ViewDesign_Forces(iApp, Analysis_Report, txt_support_end1.Text, txt_support_end2.Text);
            fv.Owner = this;
            fv.Show();
        }
    }
}
