using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;

using VectorDraw.Geometry;

namespace AstraInterface.DataStructure
{
    public class CBridgeStructure
    {
        JointNodeCollection joints;

        MemberCollection members;
        public List<string> ASTRA_Data { get; set; }
        public CBridgeStructure(string analysis_file)
        {
            
            joints = new JointNodeCollection();
            members = new MemberCollection();
            AnalysisFileName = analysis_file;
            MemberGroups = new MemberGroupCollection();
            ASTRA_Data = new List<string>();
            Effective_Depth = Width_Cantilever = Support_Distance =  0.0;
            ReadDataFromFile();
            SetAstraStructure();
        }

        public CBridgeStructure()
        {

            joints = new JointNodeCollection();
            members = new MemberCollection();
            MemberGroups = new MemberGroupCollection();
            ASTRA_Data = new List<string>();
            Effective_Depth = Width_Cantilever = Support_Distance = 0.0;
        }

        public CBridgeStructure(List<string> file_con)
        {

            joints = new JointNodeCollection();
            members = new MemberCollection();
            MemberGroups = new MemberGroupCollection();
            ASTRA_Data = new List<string>();
            Effective_Depth = Width_Cantilever = Support_Distance = 0.0;
            ReadDataFromData(file_con);
            SetAstraStructure();
        }

        public string AnalysisFileName { get; set; }
        public MemberGroupCollection MemberGroups { get; set; }
        public JointNodeCollection Supports { get; set; }


        void ReadDataFromFile()
        {
            List<string> file_con = new List<string>(File.ReadAllLines(AnalysisFileName));
            ReadDataFromData(file_con);
        }
        void ReadDataFromData(List<string> file_con)
        {
            string kStr = "";
            int flag = 0;
            try
            {
                ASTRA_Data = new List<string>();
                //bool flag = false;
                for (int i = 0; i < file_con.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(file_con[i].ToUpper());

                    if (kStr.Contains("USER'S DATA") && kStr.Contains("END OF USER") == false)
                    {
                        flag = 1; continue;

                    }
                    if (kStr.Contains("JOINT COO") && kStr.Contains("END OF USER") == false)
                    {
                        flag = 1;

                    }
                    if (flag == 1)
                        ASTRA_Data.Add(kStr);

                    //if (i == 440)
                    //    kStr = kStr.ToUpper();

                    if (kStr.Contains("FINIS") || kStr.Contains("END OF USER"))
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                //file_con.Clear();
                //file_con = null;
            }


            //Read from SAP DATA
            //List<string> file_con = new List<string>(File.ReadAllLines(AnalysisFileName));
            //string kStr = "";


            if (ASTRA_Data.Count == 0)
            {
                //file_con = new List<string>(File.ReadAllLines(AnalysisFileName));
                kStr = "";

                flag = 0;


                #region  Read from SAP Data
                int total_nodes = 0;
                int total_members = 0;
                int counter = 0;

                MyList mlist = "";
                try
                {
                    ASTRA_Data = new List<string>();

                    for (int i = 0; i < file_con.Count; i++)
                    {
                        if (file_con[i] == "") continue;
                        if (file_con[i].StartsWith("*")) continue;


                        kStr = MyList.RemoveAllSpaces(file_con[i].ToUpper());
                        if (kStr.StartsWith("*") || kStr == "") continue;

                        if (kStr.Contains("NUMBER OF NODAL POINTS"))
                        {
                            mlist = new MyList(kStr, '=');
                            total_nodes = mlist.GetInt(1);
                            flag = 0;
                        }
                        if (kStr.Contains("GENERATED NODAL DATA"))
                        {
                            counter = 0;
                            flag = 1; continue;
                        }
                        if (kStr.Contains("NUMBER OF TRUSS MEMBERS"))
                        {
                            mlist = new MyList(kStr, '=');
                            total_members = mlist.GetInt(1);
                            flag = 0;
                        }
                        if (kStr.Contains("TRUSS ELEMENT DATA"))
                        {

                            counter = 0;
                            flag = 2; continue;
                        }
                        if (kStr.Contains("NUMBER OF BEAMS"))
                        {
                            mlist = new MyList(kStr, '=');
                            total_members = mlist.GetInt(1);
                            flag = 0;
                        }
                        if (kStr.Contains("3/D BEAM ELEMENT DATA"))
                        {
                            counter = 0;
                            flag = 3; continue;
                        }
                        if (flag == 1)
                        {
                            do
                            {
                                kStr = MyList.RemoveAllSpaces(file_con[i].ToUpper());
                                mlist = kStr;
                                if (mlist.Count == 11)
                                {
                                    try
                                    {
                                        JointNode jn = new JointNode();
                                        jn.NodeNo = mlist.GetInt(0);
                                        jn.X = mlist.GetDouble(7);
                                        jn.Y = mlist.GetDouble(8);
                                        jn.Z = mlist.GetDouble(9);
                                        joints.Add(jn);
                                        counter++;
                                    }
                                    catch (Exception ee1) { }
                                }
                                i++;
                            }
                            while (counter < total_nodes);
                            flag = 0;
                        }
                        else if (flag == 2)
                        {
                            counter = 0;
                            do
                            {
                                kStr = MyList.RemoveAllSpaces(file_con[i].ToUpper());
                                mlist = kStr;
                                if (mlist.Count == 6)
                                {
                                    try
                                    {
                                        Member jn = new Member();

                                        jn.MemberNo = mlist.GetInt(0);
                                        jn.StartNode.NodeNo = mlist.GetInt(1);
                                        jn.EndNode.NodeNo = mlist.GetInt(2);
                                        Members.Add(jn);
                                        counter++;
                                    }
                                    catch (Exception ee1) { }
                                }
                                i++;
                            }
                            while (counter < total_members);
                            flag = 0;
                        }
                        else if (flag == 3)
                        {
                            counter = 0;
                            do
                            {
                                kStr = MyList.RemoveAllSpaces(file_con[i].ToUpper());
                                mlist = kStr;
                                if (mlist.Count == 12)
                                {
                                    try
                                    {
                                        Member jn = new Member();
                                        jn.MemberNo = mlist.GetInt(0);
                                        jn.StartNode.NodeNo = mlist.GetInt(1);
                                        jn.EndNode.NodeNo = mlist.GetInt(2);
                                        Members.Add(jn);
                                        counter++;
                                    }
                                    catch (Exception ee1) { }
                                }
                                i++;
                            }
                            while (counter < total_members);
                            flag = 0;
                        }

                        if (kStr.Contains("FINIS") || kStr.Contains("E Q U A T I O N P A R A M E T E R S"))
                            break;
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    file_con.Clear();
                    file_con = null;
                }

                #endregion  Read from SAP Data
            }

        }


        public string ProjectTitle { get; set; }
        public EMassUnits Base_MassUnit { get; set; }
        public eLengthUnits Base_LengthUnit { get; set; }


        public void SetASTRADocFromTXT()
        {
            List<string> fileLines = ASTRA_Data;
            Joints.Clear();
            Members.Clear();
            List<String> lstStr = new List<string>();
            //StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            string kStr = "";
            string option = "";

            int LoadCaseNo = 0;
            string LoadTitle = "";

            ProjectTitle = "";
            Base_MassUnit = EMassUnits.MTON;
            Base_LengthUnit = eLengthUnits.METRES;

            //int LoadCombination = -1;

            bool isComb = false;

            //List<string> fileLines = new List<string>(File.ReadAllLines(fileName));
            while (fileLines.Contains(""))
            {
                fileLines.Remove("");
            }

            //fileLines = fileLines;
            int lineNo = -1;

            try
            {
                MyList mList = null;
                do
                {
                    lineNo++;
                    kStr = MyList.RemoveAllSpaces(fileLines[lineNo].ToUpper());
                    if (kStr.StartsWith("*")) continue;
                    if (kStr.StartsWith("//")) continue;
                    //kStr = sr.ReadLine().ToUpper();

                    #region Option Strings

                    //if (lineNo > 246)
                    //    mList = new MyList(kStr.ToUpper(), ' ');


                    mList = new MyList(kStr.ToUpper(), ' ');
                    if (kStr.Contains("ASTRA"))
                    {
                        ProjectTitle = kStr;
                        
                    }
                    #region UNIT
                    if (kStr.StartsWith("UNIT"))
                    {
                        option = "";
                        int c = 1;

                        int uflag = 0;
                        if (fileLines[lineNo + 1].ToUpper().StartsWith("JOI"))
                        {
                            uflag = 0; // Base unit
                        }
                        else if (fileLines[lineNo + 1].ToUpper().StartsWith("SEC") ||
                            fileLines[lineNo + 1].ToUpper().StartsWith("MEM"))
                        {
                            uflag = 1; // Property unit
                        }
                        else if (fileLines[lineNo + 1].ToUpper().StartsWith("LOAD"))
                        {
                            uflag = 2; // Load unit
                        }
                        do
                        {
                            if (mList.StringList[c].StartsWith("MTO"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.MTON;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.MTON;
                            }
                            else if (mList.StringList[c].StartsWith("TO"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.MTON;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.MTON;
                            }
                            else if (mList.StringList[c].StartsWith("GM"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.GMS;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.GMS;
                            }
                            else if (mList.StringList[c].StartsWith("KG"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.KG;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.KG;
                            }
                            else if (mList.StringList[c].StartsWith("KIP"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.KIP;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.KIP;
                            }
                            else if (mList.StringList[c].StartsWith("KN"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.KN;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.KN;
                            }
                            else if (mList.StringList[c].StartsWith("LB"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.LBS;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.LBS;
                            }
                            else if (mList.StringList[c].StartsWith("NEW"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.NEW;
                                //else if (uflag == 2)
                                //    Load_MassUnit = EMassUnits.NEW;
                            }
                            else if (mList.StringList[c].StartsWith("ME"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = eLengthUnits.METRES;
                            }
                            else if (mList.StringList[c].StartsWith("CM"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = eLengthUnits.CM;
                            }
                            else if (mList.StringList[c].StartsWith("FT"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = eLengthUnits.FT;
                            }
                            else if (mList.StringList[c].StartsWith("IN"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = eLengthUnits.INCH;
                            }
                            else if (mList.StringList[c].StartsWith("MM"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = eLengthUnits.MM;
                            }
                            else if (mList.StringList[c].StartsWith("YD"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = eLengthUnits.YDS;
                            }

                            c++;
                        }
                        while (c < mList.Count);


                        //if (uflag == 0)
                        //{
                        //    //Prop_LengthUnit = Base_LengthUnit;
                        //    //Load_MassUnit = Base_MassUnit;
                        //    //Load_LengthUnit = Base_LengthUnit;
                        //}
                    }
                    #endregion UNIT

                    if (mList.Count == 2)
                    {
                        if (mList.StringList[0].Length > 3)
                            mList.StringList[0] = mList.StringList[0].Substring(0, 3);
                        if (mList.StringList[1].Length > 3)
                            mList.StringList[1] = mList.StringList[1].Substring(0, 3);

                        if (mList.StringList[0].ToUpper().Contains("JOI") &&
                            (mList.StringList[1].ToUpper().Contains("COO")))
                        {
                            option = "JOINT COORDINATE";
                            continue;
                        }
                        if (mList.StringList[0].ToUpper().Contains("MEM") ||
                            mList.StringList[0].ToUpper().Contains("SEC"))
                        {
                            if ((mList.StringList[1].ToUpper().Contains("INC")) ||
                                (mList.StringList[1].ToUpper().Contains("CON")))
                            {
                                option = "MEMB INCI";
                                continue;
                            }
                            else if (mList.StringList[1].ToUpper().Contains("PRO"))
                            {
                                option = "MEMBER PROPERTY"; continue;
                            }
                        }

                        if (mList.StringList[0].ToUpper().Contains("ELE"))
                        {
                            if ((mList.StringList[1].ToUpper().Contains("INC")) ||
                                (mList.StringList[1].ToUpper().Contains("CON")))
                            {
                                option = "ELEMENT INCIDENCE"; continue;
                            }
                            else if (mList.StringList[1].ToUpper().Contains("PRO"))
                            {
                                option = "ELEMENT PROP"; continue;
                            }
                        }
                        if (mList.StringList[0].ToUpper().Contains("MAT"))
                        {
                            //if (mList.StringList[1].ToUpper().Contains("CON"))
                            //{
                            //    if (Mat_Prop == null)
                            //        Mat_Prop = new MaterialProperty();
                            //    else
                            //    {
                            //        Mat_Prop = new MaterialProperty();
                            //    }
                            //    Constants.Add(Mat_Prop);
                            //    option = "CONSTANT"; continue;
                            //}
                        }

                        //if (kStr.Contains("MEMB INCI") || kStr.Contains("MEMBER INCIDENCE") || kStr.Contains("MEMBER INCI"))
                        //{
                        //    option = "MEMB INCI";
                        //}


                        //if (kStr.Contains("JOINT COORD") || kStr.Contains("JOINT COORDINATE"))
                        //{
                        //    option = "JOINT COORDINATE";
                        //    continue;
                        //}
                        //if (kStr.Contains("MEMB INCI") || kStr.Contains("MEMBER INCIDENCE") || kStr.Contains("MEMBER INCI"))
                        //{
                        //    option = "MEMB INCI";
                        //}
                    }
                    if (kStr.ToUpper().StartsWith("CONSTANT"))
                    {
                        //if (Mat_Prop == null)
                        //    Mat_Prop = new MaterialProperty();
                        //else
                        //{
                        //    Mat_Prop = new MaterialProperty();
                        //}
                        //Constants.Add(Mat_Prop);


                        option = "CONSTANT"; continue;
                    }
                    if (kStr.StartsWith("START GROUP"))
                    {
                        option = "GROUP"; continue;
                    }
                    if (option == "GROUP" && kStr.StartsWith("END"))
                    {
                        option = ""; continue;
                    }

                    if (kStr.StartsWith("SUPPORT DISPLACEMENT"))
                    {
                        //[261]	"SUPPORT DISPLACEMENT LOAD"	string

                        option = "SUPPORT DISPACEMENT LOAD"; continue;
                    }
                    if (kStr.StartsWith("SUPPORT"))
                    {
                        option = "SUPPORT"; continue;
                    }
                    //if (kStr.Contains("CONSTANT"))
                    //{
                    //    option = "CONSTANT"; continue;
                    //}
                    if (kStr.Trim().TrimEnd().TrimStart().StartsWith("LOAD")
                        && (!kStr.Contains("GENERATION")))
                    {
                        lstStr.Clear();
                        lstStr.AddRange(kStr.Split(new char[] { ' ' }));
                        if (mList.Count > 2)
                            LoadTitle = mList.GetString(2);
                        else
                            LoadTitle = "";

                        if (lstStr.Count == 3)
                        {
                            if (int.TryParse(lstStr[1], out LoadCaseNo))
                            {
                                isComb = true;
                                option = "LOAD";
                                //continue;
                            }
                        }
                        else
                        {
                            if (int.TryParse(lstStr[1], out LoadCaseNo))
                            {
                                isComb = false;
                                option = "LOAD"; continue;
                            }
                        }
                    }
                    if (kStr.Contains("SELF"))
                    {
                        option = "SELF";

                        //if (mList.Count > 2)
                        //{
                        //    SelfWeight_Direction = mList.StringList[1];
                        //    SelfWeight = mList.StringList[2];
                        //}
                        //if (LoadCaseNo != 0)
                        //{


                        //    if (DefineLoad.LoadCase != LoadCaseNo)
                        //    {
                        //        DefineLoad = new LoadDefine();
                        //        LoadDefines.Add(DefineLoad);
                        //    }
                        //    DefineLoad.LoadCase = LoadCaseNo;
                        //    DefineLoad.LoadTitle = LoadTitle;
                        //    DefineLoad.Selfweight = kStr;
                        //    //DefineLoad.ElementLoadList.Add(str);
                        //}

                        continue;
                    }
                    if (kStr.Contains("MEMB TRUSS") || kStr.Contains("MEMBER TRUSS"))
                    {
                        option = "MEMBER TRUSS"; continue;
                    }
                    if (kStr.Contains("MEMB RELEASE"))
                    {
                        option = "MEMB RELEASE"; continue;
                    }
                    if (kStr.Contains("SELFWEIGHT"))
                    {
                        option = "SELFWEIGHT"; continue;
                    }
                    if (kStr.Contains("JOINT LOAD"))
                    {
                        option = "JOINT LOAD"; continue;
                    }
                    if (kStr.Contains("MEMB LOAD") || kStr.Contains("MEMBER LOAD"))
                    {
                        option = "MEMBER LOAD"; continue;
                    }
                    if (kStr.Contains("ELEM LOAD") || kStr.Contains("ELEMENT LOAD"))
                    {
                        option = "ELEMENT LOAD"; continue;
                    }
                    if (kStr.Contains("LOAD COMB"))
                    {
                        lstStr.Clear();
                        lstStr.AddRange(kStr.Split(new char[] { ' ' }));

                        if (mList.Count > 3)
                            LoadTitle = mList.GetString(3);
                        else
                            LoadTitle = "";
                        if (lstStr.Count >= 3)
                        {
                            if (int.TryParse(lstStr[2], out LoadCaseNo))
                            {
                                isComb = true;
                                //option = "LOAD"; continue;
                                option = "LOAD COMB"; continue;
                            }
                        }
                        else
                        {
                            if (int.TryParse(lstStr[1], out LoadCaseNo))
                            {
                                isComb = false;
                                option = "LOAD COMB"; continue;
                                //option = "LOAD"; continue;
                            }
                        }

                        //option = "LOAD COMB"; continue;
                    }
                    if (kStr.Contains("PRINT") && !kStr.Contains("INTERVAL"))
                    {
                        option = "PRINT";

                        //if (kStr.Contains("PRINT ANALYSIS ALL"))
                        //    Analysis_Specification.Print_Analysis_All = true;
                        //else if (kStr.StartsWith("PRINT SUPPORT REACTION"))
                        //{
                        //    Analysis_Specification.Print_Support_Reaction = true;
                        //}
                        //else if (kStr.StartsWith("PRINT STATIC CHECK"))
                        //{
                        //    Analysis_Specification.Print_Static_Check = true;
                        //}
                        //else if (kStr.StartsWith("PRINT LOAD DATA"))
                        //{
                        //    Analysis_Specification.Print_Load_Data = true;
                        //}
                        //else if (kStr.StartsWith("PRINT MAX FORCE"))
                        //{
                        //    Analysis_Specification.Print_Max_Force = true;
                        //    if (mList.StringList.Contains("LIST"))
                        //    {
                        //        Analysis_Specification.List_Maxforce.Add(mList.GetString(mList.StringList.IndexOf("LIST") + 1));
                        //    }
                        //}
                        continue;
                    }
                    if (kStr.Contains("PERFORM ANALYSIS"))
                    {
                        //Analysis_Specification.Perform_Analysis = true;
                        option = "PERFORM ANALYSIS"; continue;
                    }
                    if (kStr.Contains("FINISH"))
                    {
                        option = "FINISH"; continue;
                    }
                    if (kStr.Contains("LOADING"))
                    {
                        option = "LOADING"; continue;
                    }
                    if (kStr.Contains("TEMP LOAD"))
                    {
                        option = "TEMP LOAD"; continue;
                    }
                    if (kStr.Contains("AREA LOAD"))
                    {
                        option = "AREA LOAD"; continue;
                    }
                    if (kStr.Contains("REPEAT LOAD"))
                    {
                        //LoadCaseNo++;
                        option = "REPEAT LOAD"; continue;
                    }
                    if (kStr.Contains("ELEMENT INCIDENCE"))
                    {
                        option = "ELEMENT INCIDENCE"; continue;
                    }
                    if (kStr.Contains("ELEMENT PROP") ||
                        kStr.Contains("ELEMENT PROPERTIES") ||
                        kStr.Contains("ELEMENT PROPERTY"))
                    {
                        option = "ELEMENT PROP"; continue;
                    }
                    if (kStr.Contains("ELEMENT LOAD"))
                    {
                        option = "ELEMENT LOAD"; continue;
                    }
                    if (kStr.Contains("DEFINE MOVING"))
                    {
                        option = "DEFINE MOVING";

                        //string ll_txt = Path.Combine(Path.GetDirectoryName(fileName), "LL.TXT");
                        //string ll_txt = MyList.Get_LL_TXT_File(fileName);

                        //if (File.Exists(ll_txt))
                        //    MovingLoads = MovingLoadData.GetMovingLoads(ll_txt);

                        //load_generation.Read_Type_From_Text_File(fileName);

                        //LiveLoad ll = new LiveLoad();

                        //for (var item = 0; item < load_generation.Count; item++)
                        //{
                        //    ll = new LiveLoad();
                        //    ll.Type = load_generation[0].TypeNo;
                        //    ll.X_Distance = load_generation[0].X;
                        //    ll.Y_Distance = load_generation[0].Y;
                        //    ll.Z_Distance = load_generation[0].Z;
                        //    ll.X_Increment = load_generation[0].XINC;
                        //    LL_Definition.Add(ll);
                        //}

                        continue;
                    }
                    if (kStr.Contains("LOAD GENE"))
                    {
                        //bIsMovingLoad = true;
                        option = "LOAD GENERATION";
                        //load_generation.Read_Type_From_Text_File(fileName);
                        continue;
                    }
                    if (kStr.Contains("PRINT SUPPORT"))
                    {
                        option = "PRINT SUPPORT"; continue;
                    }
                    if (kStr.Contains("PRINT MAX"))
                    {
                        option = "PRINT MAX"; continue;
                    }
                    if (kStr.Contains("MEMBER PROPERTY") ||
                        kStr.Contains("MEMB PROP") ||
                        kStr.Contains("MEM PROP") ||
                        kStr.Contains("MEMBER PROPERTIES"))
                    {
                        option = "MEMBER PROPERTY"; continue;
                    }
                    if (kStr.Contains("DEAD"))
                    {
                        option = "DEAD"; continue;
                    }
                    if (kStr.Contains("PERFORM EIGEN"))
                    {
                        //option = "PERFORM EIGEN";
                        option = "DYNAMIC";
                        //DynamicAnalysis.Add(kStr);
                        //IsDynamicLoad = true;
                        continue;
                    }
                    if (kStr.Contains("PERFORM TIME HISTORY"))
                    {
                        option = "DYNAMIC";
                        //option = "TIME HISTORY";
                        //DynamicAnalysis.Add(kStr);
                        //IsDynamicLoad = true;
                        continue;
                    }
                    if (kStr.Contains("PERFORM RESPONSE"))
                    {
                        //option = "PERFORM RESPONSE";
                        option = "DYNAMIC";
                        //DynamicAnalysis.Add(kStr);
                        //IsDynamicLoad = true;
                        continue;
                    }
                    if (kStr.Contains("FREQUENCIES"))
                    {
                        //FREQUENCIES = mList.GetString(1);
                        //option = "FREQUENCIES"; continue;
                    }
                    #region Dynamic Analysis
                    //if (kStr.Contains("TIME STEPS"))
                    //{
                    //    option = "TIME STEPS"; continue;
                    //}
                    //if (kStr.Contains("PRINT INTERVAL"))
                    //{
                    //    option = "PRINT INTERVAL"; continue;
                    //}
                    //if (kStr.Contains("STEP INTERVAL"))
                    //{
                    //    option = "STEP INTERVAL"; continue;
                    //}
                    //if (kStr.Contains("DAMPING FACTOR"))
                    //{
                    //    option = "DAMPING FACTOR"; continue;
                    //}
                    //if (kStr.Contains("GROUND MOTION"))
                    //{
                    //    option = "GROUND MOTION"; continue;
                    //}
                    //if (kStr.Contains("X DIVISION"))
                    //{
                    //    option = "X DIVISION"; continue;
                    //}
                    //if (kStr.Contains("SCALE FACTOR"))
                    //{
                    //    option = "SCALE FACTOR"; continue;
                    //}
                    //if (kStr.Contains("TIME VALUES"))
                    //{
                    //    option = "TIME VALUES"; continue;
                    //}
                    //if (kStr.Contains("TIME FUNCTION"))
                    //{
                    //    option = "TIME FUNCTION"; continue;
                    //}
                    //if (kStr.Contains("NODAL CONSTRAINT"))
                    //{
                    //    option = "NODAL CONSTRAINT"; continue;
                    //}
                    //if (kStr.Contains("MEMBER STRESS"))
                    //{
                    //    option = "MEMBER STRESS"; continue;
                    //}
                    //if (kStr.Contains("CUTOFF FREQUENCY"))
                    //{
                    //    option = "CUTOFF FREQUENCY"; continue;
                    //}
                    //if (kStr.Contains("PERFORM RESPONSE"))
                    //{
                    //    option = "PERFORM RESPONSE"; continue;
                    //}
                    //if (kStr.Contains("DIRECTION"))
                    //{
                    //    option = "DIRECTION"; continue;
                    //}
                    //if (kStr.Contains("SPECTRUM TYPE ACCELERATION"))
                    //{
                    //    option = "SPECTRUM TYPE ACCELERATION"; continue;
                    //}
                    //if (kStr.Contains("SPECTRUM TYPE DISPLACEMENT"))
                    //{
                    //    option = "SPECTRUM TYPE DISPLACEMENT"; continue;
                    //}
                    //if (kStr.Contains("SPECTRUM POINTS"))
                    //{
                    //    option = "SPECTRUM POINTS"; continue;
                    //}
                    //if (kStr.Contains("PERIOD ACCELERATION"))
                    //{
                    //    option = "PERIOD ACCELERATION"; continue;
                    //}
                    #endregion Dynamic Analysis

                    #endregion
                    switch (option)
                    {
                        case "JOINT COORDINATE":
                            try
                            {
                                Joints.AddTXT(kStr);
                            }
                            catch (Exception ex)
                            {
                                kStr = "";
                            }
                            break;
                        case "MEMB INCI":
                            //Members.AddTXT(kStr);
                            try
                            {
                                Members.AddTXT(kStr);
                            }
                            catch (Exception ex)
                            {
                                kStr = "";
                            }
                            break;


                        case "GROUP":
                            //Members.AddTXT(kStr);
                            try
                            {
                                //Members.Add_Group(kStr);
                                //MemberGroups.Add(kStr);

                            }
                            catch (Exception ex)
                            {
                                kStr = "";
                            }
                            break;


                        //case "MEMBER TRUSS":
                        //    Members.AddMemberTruss(kStr);
                        //    MemberTrusses.Add(kStr);
                        //    break;

                        //case "MEMBER CABLE":
                        //    //Members.AddMemberTruss(kStr);
                        //    MemberCables.Add(kStr);
                        //    break;
                        //case "MEMBER RELEASE":
                        //    //Members.AddMemberTruss(kStr);
                        //    MemberReleases.Add(kStr);
                        //    break;
                        //case "SUPPORT":
                        //    Supports.Add(kStr);
                        //    JointSupports.Add(kStr);
                        //    break;
                        //case "JOINT LOAD":
                        //    JointLoads.AddTXT(kStr, LoadCaseNo);

                        //    if (DefineLoad.LoadCase != LoadCaseNo)
                        //    {
                        //        DefineLoad = new LoadDefine();
                        //        LoadDefines.Add(DefineLoad);
                        //    }
                        //    DefineLoad.LoadCase = LoadCaseNo;
                        //    DefineLoad.LoadTitle = LoadTitle;
                        //    DefineLoad.JointLoadList.Add(kStr);

                        //    break;
                        //case "MEMBER LOAD":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        MemberLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);

                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.MemberLoadList.Add(str);
                        //    }
                        //    break;

                        //case "AREA LOAD":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        //AreaLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);
                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.AreaLoadList.Add(str);
                        //    }
                        //    break;


                        //case "TEMP LOAD":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        //AreaLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);
                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.TempLoadList.Add(str);
                        //    }
                        //    break;

                        //case "REPEAT LOAD":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        MemberLoads.SetLoadCombination(str, LoadCaseNo);
                        //        JointLoads.SetLoadCombination(str, LoadCaseNo);


                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.RepeatLoadList.Add(str);

                        //    }
                        //    break;


                        //case "ELEMENT LOAD":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        //MemberLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);

                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.ElementLoadList.Add(str);
                        //    }
                        //    break;
                        //case "SUPPORT DISPACEMENT LOAD":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        //MemberLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);

                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.SupportDisplacements.Add(str);
                        //    }
                        //    break;

                        //case "LOAD COMB":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        MemberLoads.SetLoadCombination(str, LoadCaseNo);
                        //        JointLoads.SetLoadCombination(str, LoadCaseNo);


                        //        if (DefineLoad.LoadCase != LoadCaseNo)
                        //        {
                        //            DefineLoad = new LoadDefine();
                        //            LoadDefines.Add(DefineLoad);
                        //        }
                        //        DefineLoad.LoadCase = LoadCaseNo;
                        //        DefineLoad.LoadTitle = LoadTitle;
                        //        DefineLoad.CominationLoadList.Add(str);

                        //    }
                        //    break;

                        //case "ELEMENT INCIDENCE":
                        //    lstStr.Clear();
                        //    lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                        //    foreach (string str in lstStr)
                        //    {
                        //        try
                        //        {
                        //            //Elements.Add(Element.Parse(str.Trim().TrimEnd().TrimStart()));
                        //        }
                        //        catch (Exception exx) { }
                        //    }
                        //    break;

                        case "MEMBER PROPERTY":
                            //lstStr.Clear();
                            //lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            //MemberProperties.AnalysisData = AnalysisData;

                            //MemberProps.Add(kStr);

                            //foreach (string str in lstStr)
                            //{
                            //    try
                            //    {
                            //        MemberProperties.AddTxt(str);
                            //    }
                            //    catch (Exception exx) { }
                            //}
                            break;

                        case "DEFINE MOVING":
                            //try
                            //{

                            //    int type = mList.GetInt(1);

                            //    if (mList.Count > 3)
                            //    {
                            //        for (int j = 0; j < LL_Definition.Count; j++)
                            //        {
                            //            if (LL_Definition[j].Type == type)
                            //                LL_Definition[j].Impact_Factor = mList.GetDouble(3);
                            //        }
                            //    }
                            //}
                            //catch (Exception exx) { }

                            break;

                        case "LOAD GENERATION":
                            //try
                            //{
                            //    load_generation.Read_Type_From_Text_File(fileName);
                            //}
                            //catch (Exception exx) { }

                            break;

                        case "ELEMENT PROP":
                            //try
                            //{
                            //    Elements.SetElementProperty(kStr);
                            //    ElementProps.Add(kStr);
                            //}
                            //catch (Exception exx) { }

                            break;

                        case "CONSTANT":
                            //try
                            //{
                            //    MemberProperties.AnalysisData = AnalysisData;
                            //    MemberProperties.SetConstants(kStr);

                            //    if (mList.StringList[0].StartsWith("D"))
                            //    {
                            //        Mat_Prop.Density = mList.StringList[1];
                            //    }
                            //    else if (mList.StringList[0].StartsWith("E"))
                            //    {
                            //        Mat_Prop.Elastic_Modulus = mList.StringList[1];
                            //    }
                            //    else if (mList.StringList[0].StartsWith("P"))
                            //    {
                            //        Mat_Prop.Possion_Ratio = mList.StringList[1];
                            //    }
                            //    else if (mList.StringList[0].StartsWith("A"))
                            //    {
                            //        Mat_Prop.Alpha = mList.StringList[1];
                            //    }
                            //    Mat_Prop.MemberNos = mList.GetString(2);

                            //}
                            //catch (Exception exx) { }
                            break;
                        case "DYNAMIC":
                            //DynamicAnalysis.Add(kStr);
                            break;
                        case "FINISH":
                            break;
                    }
                }
                while (lineNo != fileLines.Count);

            }
            catch (Exception exx) { }
            finally
            {
                //sr.Close();
                fileLines.Clear();
                fileLines = null;
            }
        }


        void SetAstraStructure()
        {
            try
            {
                if (joints.Count == 0)
                {
                    joints.Clear();
                    joints.ReadJointCoordinates(ASTRA_Data);
                }
                Supports = new JointNodeCollection();

                MyList  mlist = null;
                string kStr = "";

                bool flag = false;
                int j = 0;
                for (int i = 0; i < ASTRA_Data.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(ASTRA_Data[i].ToUpper().Trim().TrimEnd().TrimStart());
                    kStr = kStr.Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    if (kStr.StartsWith("SUPPORT"))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        if (kStr.Contains("PINNED") || kStr.Contains("FIXED"))
                        {
                            kStr = kStr.Replace("FIXED","");
                            kStr = kStr.Replace("PINNED", "");
                            kStr = kStr.Replace("BUT", "");
                            kStr = kStr.Replace("MX", "");
                            kStr = kStr.Replace("MY", "");
                            kStr = kStr.Replace("MZ", "");
                            kStr = kStr.Replace("FX", "");
                            kStr = kStr.Replace("FY", "");
                            kStr = kStr.Replace("FZ", "");
                            kStr = MyList.RemoveAllSpaces(kStr);

                            

                            foreach(var item in MyList.Get_Array_Intiger(kStr))
                            {
                                try
                                {

                                    //Supports.Add(joints.GetJoints(mlist.GetInt(j)));
                                    Supports.Add(joints.GetJoints(item));
                                }
                                catch (Exception ex) { break; }
                            }
                        }
                    }
                }

                if (members.Count == 0)
                    members.ReadMembers(ASTRA_Data);
                members.SetCoordinates(joints);
                MemberGroups.ReadFromFile(ASTRA_Data, members);
            }
            catch (Exception ex) { }
        }

        public JointNodeCollection Joints
        {
            get
            {
                return joints;
            }
        }

        public MemberCollection Members
        {
            get
            {
                return members;
            }
        }

        public double Length
        {
            get
            {
                return joints.OuterLength;
            }
        }
        public double Width
        {
            get
            {
                return joints.Width;
            }
        }
        public double Height
        {
            get
            {
                return joints.Height;
            }
        }
        public double NoOfPanels
        {
            get
            {
                return joints.Get_NoOfPanels();
            }
        }
        public double NoOfStringers
        {
            get
            {
                return joints.Get_NoOfStringers();
            }
        }

        public double Effective_Depth { get; set; }
        public double Width_Cantilever { get; set; }
        public double Support_Distance { get; set; }

        public double Skew_Angle
        {
            get
            {
                try
                {
                    if (Joints.Count > 1)
                    {
                        return (int)((180.0 / Math.PI) * Math.Atan((Joints[1].X / Joints[1].Z)));
                    }
                }
                catch (Exception ex) { }
                return 0.0;
            }
        }

        public Member Get_Member(int mem_no)
        {
            try
            {
                if (Members[mem_no - 1].MemberNo == mem_no) return Members[mem_no - 1];
                for (int i = 0; i < Members.Count; i++)
                {
                    if (Members[i].MemberNo == mem_no)
                        return Members[i];
                }
            }
            catch (Exception ex) { }
            return null;

        }
    }
    public class JointNode
    {
        public JointNode()
        {
            NodeNo = -1;
            XYZ = new gPoint();
        }

        public JointNode(double x, double y, double z)
        {
            NodeNo = -1;
            XYZ = new gPoint(x, y, z);
        }
        public int NodeNo { get; set; }
        public double X { get { return XYZ.x; } set { XYZ.x = value; } }
        public double Y { get { return XYZ.y; }  set { XYZ.y = value; } }
        public double Z { get { return XYZ.z; }  set { XYZ.z = value; } }
        public gPoint XYZ { get; set; }
        public static JointNode Parse(string s)
        {
            string str = s.Replace(',', ' ').ToUpper();
            str = MyList.RemoveAllSpaces(str.Replace(';', ' '));
            MyList mList = new MyList(str, ' ');
            JointNode jNode = new JointNode();
            if (mList.Count == 4)
            {
                //1                0                0                8.43
                jNode.NodeNo = mList.GetInt(0);
                jNode.XYZ.x = mList.GetDouble(1);
                jNode.XYZ.y = mList.GetDouble(2);
                jNode.XYZ.z = mList.GetDouble(3);
            }
            else
                throw new Exception("Invalid Joint data : " + s);

            return jNode;

        }
     
        public override string ToString()
        {
            return string.Format("{0,-7} {1,10:f3} {2,10:f3} {3,10:f3} ", NodeNo, X, Y, Z);
        }
    }
    public class JointNodeCollection : List<JointNode>
    {
        public JointNodeCollection list { get { return this; } }
        public JointNodeCollection()
            : base()
        {
            //list = new List<JointNode>();
        }
        public JointNodeCollection(int capacity)
            : base(capacity)
        {
            //list = new List<JointNode>();
        }
        public JointNodeCollection(IEnumerable<JointNode> collection)
            : base(collection)
        {
            //list = new List<JointNode>();
        }
        public List<JointNode> JointNodes
        {
            get
            {
                return list;
            }
        }
        //public JointNode this[int index]
        //{
        //    get
        //    {
        //        return list[index];
        //    }
        //}

        //public void Add(JointNode jn)
        //{
        //    list.Add(jn);
        //}
        //public void Clear()
        //{
        //    list.Clear();
        //}
        //public int Count
        //{
        //    get
        //    {
        //        return list.Count;
        //    }
        //}
        public void ReadJointCoordinates(List<string> ASTRA_Data)
        {
            try
            {
                string kStr = "";
                bool flag = false;
                list.Clear();
                for (int i = 0; i < ASTRA_Data.Count; i++)
                {
                    kStr = ASTRA_Data[i].ToUpper();

                    kStr = MyList.RemoveAllSpaces(kStr);
                    if (kStr.Contains("JOINT C"))
                    {
                        flag = true; continue;
                    }
                    if (flag)
                    {
                        try
                        {
                            MyList mtxt = new MyList(kStr, ';');
                            foreach (var item in mtxt.StringList)
                            {
                                list.Add(JointNode.Parse(item));
                            }
                        }
                        catch (Exception ex) { }
                    }
                    if (kStr.Contains("MEMBER")) break;
                }
                Set_Max_Min_Val();
            }
            catch (Exception ex) { }
        }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        public double MaxZ { get; set; }

        public double MinZ { get; set; }
        public double MinY { get; set; }
        public double MinX { get; set; }

        public void Set_Max_Min_Val()
        {
            MaxX = -99999999.9999;
            MaxY = -99999999.9999;
            MaxZ = -99999999.9999;
            MinX = 99999999.9999;
            MinY = 99999999.9999;
            MinZ = 99999999.9999;

            for (int i = 0; i < list.Count; i++)
            {
                if (MaxX < list[i].XYZ.x)
                    MaxX = list[i].XYZ.x;

                if (MaxY < list[i].XYZ.y)
                    MaxY = list[i].XYZ.y;

                if (MaxZ < list[i].XYZ.z)
                    MaxZ = list[i].XYZ.z;

                if (MinX > list[i].XYZ.x)
                    MinX = list[i].XYZ.x;

                if (MinY > list[i].XYZ.y)
                    MinY = list[i].XYZ.y;

                if (MinZ > list[i].XYZ.z)
                    MinZ = list[i].XYZ.z;
            }
        }

        public double Length
        {
            get
            {
                return (MaxX - MinX);
            }
        }

        public double OuterLength
        {
            get
            {
                double outer_length = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    //if (list[i].Z == MinZ)
                        if (list[i].Z == 0.0)
                        if (outer_length < list[i].X)
                            outer_length = list[i].X;
                }
                
                return outer_length;
            }
        }

        public double Width
        {
            get
            {
                return (MaxZ - MinZ);
            }
        }
        public double Height
        {
            get
            {
                return (MaxY - MinY);
            }
        }

        public JointNode GetJoints(int JointNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].NodeNo == JointNo)
                {
                    return list[i];
                }
            }
            return null;
        }
        public JointNode GetJoints(double x, double y, double z)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((Math.Abs(list[i].X - x) < 0.9) &&
                    list[i].Y.ToString("0.00") == y.ToString("0.00") &&
                    list[i].Z.ToString("0.00") == z.ToString("0.00"))
                {
                    return list[i];
                }
            }
            throw new Exception("Joint not found. ");
        }

        public JointNode GetJoints(double x, double y, double z, string format)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (
                    list[i].X.ToString(format) == x.ToString(format) &&
                    list[i].Y.ToString(format) == y.ToString(format) &&
                    list[i].Z.ToString(format) == z.ToString(format))
                {
                    return list[i];
                }
            }
            throw new Exception("Joint not found. ");
        }
        public JointNode GetJoints(double x, double y, double z, double x_margin)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((Math.Abs(list[i].X - x) < x_margin) &&
                    list[i].Y.ToString("0.00") == y.ToString("0.00") &&
                    list[i].Z.ToString("0.00") == z.ToString("0.00"))
                {
                    return list[i];
                }
            }
            throw new Exception("Joint not found. ");
        }

        public int Get_NoOfPanels()
        {
            //List<double> list_x = new List<double>();
            double curr_val = MinX;
            int panel_count = 0;

            
            List<double> lst_x = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.y == MinY)
                if (!lst_x.Contains(list[i].XYZ.x))
                {
                    lst_x.Add(list[i].XYZ.x);
                    panel_count++;
                }
                //if (curr_val < list[i].XYZ.x)
                //{
                //    curr_val = list[i].XYZ.x;
                //    panel_count++;
                //}
            }
            return lst_x.Count;
        }
        public int Get_NoOfStringers()
        {
            double curr_val = MinX;
            List<double> l = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                if (!l.Contains(list[i].Z))
                    l.Add(list[i].Z);
            }
            return l.Count - 2;
        }

        public List<string> Get_Joints_Load_as_String(double inner_load, double outter_load)
        {
            int i = 0;
            string kStr = "";
            List<string> lst = new List<string>();

            List<int> inn_lst = new List<int>();
            List<int> out_lst = new List<int>();

            //if (MinY < 0.0)
            //    MinY = 0.0;

            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MinZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MinZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
            }
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }

                if (list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }
            }
            inn_lst.Sort();
            out_lst.Sort();
            kStr = "";
            int jnt1,jnt2,jnt3;
            int toIndx, fromIndx;
            bool flag = false;
            jnt1 = jnt2 = jnt3 = 0;
            int cnt = 0;
            for (i = 0; i < out_lst.Count; i++)
            {
                if (i == 0)
                {
                    jnt1 = out_lst[i];
                    jnt2 = jnt1;
                    continue;
                }
                jnt3 = out_lst[i];

                if (jnt3 == (jnt2 + 1))
                {
                    jnt2 = jnt3;
                    flag = true;
                    continue;
                }
                else
                {
                    if (flag)
                    {
                        kStr += jnt1 + "   TO  " + jnt2 + "  " ;
                        jnt1 = jnt3;
                        jnt2 = jnt1;
                        flag = false;
                    }
                    else
                    {
                        kStr += jnt3 + " ";
                    }
                }

                if (cnt > 9)
                {
                    kStr += "    FY  -" + inner_load.ToString("0.000");
                    lst.Add(kStr);
                    kStr = "";
                    cnt = 0;
                }
                cnt++;
                //kStr += out_lst[i].ToString() + "  ";
            }
            if (flag)
            {
                kStr += jnt1 + "   TO  " + jnt2 + "  ";
                jnt1 = jnt3;
                //jnt2 = jnt1;
                //flag = false;
            }
            //else
            //{
            //    //kStr += jnt3 + " ";
            //}

            kStr += "    FY  -" + inner_load.ToString("0.000");
            lst.Add(kStr);
            kStr = "";
            for (i = 0; i < inn_lst.Count; i++)
            {
                kStr += inn_lst[i].ToString() + "  ";
            }
            kStr += "   FY  -" + outter_load.ToString("0.000");
            lst.Add(kStr);

            return lst;

        }
        public List<string> Get_Joints_Load_as_String(double inner_load, double outter_load, string direction)
        {
            int i = 0;
            string kStr = "";
            List<string> lst = new List<string>();

            List<int> inn_lst = new List<int>();
            List<int> out_lst = new List<int>();

            //if (MinY < 0.0)
            //    MinY = 0.0;

            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MinZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MinZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == (MinY < 0 ? 0.0 : MinY))
                {
                    inn_lst.Add(list[i].NodeNo);
                }
            }
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }

                if (list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }
            }
            inn_lst.Sort();
            out_lst.Sort();
            kStr = "";
            int jnt1, jnt2, jnt3;
            int toIndx, fromIndx;
            bool flag = false;
            jnt1 = jnt2 = jnt3 = 0;
            int cnt = 0;
            for (i = 0; i < out_lst.Count; i++)
            {
                if (i == 0)
                {
                    jnt1 = out_lst[i];
                    jnt2 = jnt1;
                    continue;
                }
                jnt3 = out_lst[i];

                if (jnt3 == (jnt2 + 1))
                {
                    jnt2 = jnt3;
                    flag = true;
                    continue;
                }
                else
                {
                    if (flag)
                    {
                        kStr += jnt1 + "   TO  " + jnt2 + "  ";
                        jnt1 = jnt3;
                        jnt2 = jnt1;
                        flag = false;
                    }
                    else
                    {
                        kStr += jnt3 + " ";
                    }
                }

                if (cnt > 9)
                {
                    kStr += "  " + direction + "  -" + inner_load.ToString("0.000");
                    lst.Add(kStr);
                    kStr = "";
                    cnt = 0;
                }
                cnt++;
                //kStr += out_lst[i].ToString() + "  ";
            }
            if (flag)
            {
                kStr += jnt1 + "   TO  " + jnt2 + "  ";
                jnt1 = jnt3;
                //jnt2 = jnt1;
                //flag = false;
            }
            //else
            //{
            //    //kStr += jnt3 + " ";
            //}

            kStr += "    " + direction + "  -" + inner_load.ToString("0.000");
            lst.Add(kStr);
            kStr = "";
            for (i = 0; i < inn_lst.Count; i++)
            {
                kStr += inn_lst[i].ToString() + "  ";
            }
            kStr += "   " + direction + "  -" + outter_load.ToString("0.000");
            lst.Add(kStr);

            return lst;

        }

        public int  Get_Total_Joints_At_Truss_Floor()
        {
            int i = 0;
            string kStr = "";
            List<string> lst = new List<string>();

            List<int> inn_lst = new List<int>();
            List<int> out_lst = new List<int>();


            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
            }
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }

                if (list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }
            }
            //kStr = "";
            //int jnt1, jnt2, jnt3;
            //int toIndx, fromIndx;
            //bool flag = false;
            //jnt1 = jnt2 = jnt3 = 0;
            //for (i = 0; i < out_lst.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        jnt1 = out_lst[i];
            //        jnt2 = jnt1;
            //        continue;
            //    }
            //    jnt3 = out_lst[i];

            //    if (jnt3 == (jnt2 + 1))
            //    {
            //        jnt2 = jnt3;
            //        flag = true;
            //        continue;
            //    }
            //    else
            //    {
            //        if (flag)
            //        {
            //            kStr += jnt1 + "   TO  " + jnt2 + "  ";
            //            jnt1 = jnt3;
            //            jnt2 = jnt1;
            //            flag = false;
            //        }
            //        else
            //        {
            //            kStr += jnt3 + " ";
            //        }
            //    }
            //    //kStr += out_lst[i].ToString() + "  ";
            //}
            //if (flag)
            //{
            //    kStr += jnt1 + "   TO  " + jnt2 + "  ";
            //    jnt1 = jnt3;
            //    //jnt2 = jnt1;
            //    //flag = false;
            //}
            //else
            //{
            //    //kStr += jnt3 + " ";
            //}

            //kStr += "    FY  -" + inner_load.ToString("0.000");
            //lst.Add(kStr);
            //kStr = "";
            //for (i = 0; i < inn_lst.Count; i++)
            //{
            //    kStr += inn_lst[i].ToString() + "  ";
            //}
            //kStr += "   FY  -" + outter_load.ToString("0.000");
            //lst.Add(kStr);

            return (inn_lst.Count + out_lst.Count);

        }

        public void AddTXT(string itemCollection)
        {
            itemCollection = itemCollection.Replace('\t', ' ');
            string[] values = itemCollection.Split(new char[] { ';' });
            List<string> lstStr = new List<string>();

            int n1 = 0, n2 = 0, kdif = 0;
            double dx = 0.0d, dy = 0.0d, dz = 0.0d;
            double x = 0.0d, y = 0.0d, z = 0.0d;
            double kx = 0.0d, ky = 0.0d, kz = 0.0d;
            int addFactor = 0, nodeDif = 0;


            foreach (string str1 in values)
            {
                JointNode jc;
                string str;
                str = str1.Trim().TrimEnd().TrimStart();
                try
                {
                    lstStr.Clear();
                    lstStr.AddRange(str.Split(new char[] { ' ' }));
                    // Example:
                    //1 0 0 0 ;
                    if (lstStr.Count == 4)
                    {
                        Add(JointNode.Parse(str));
                    }
                    // Example:
                    // 4	0	0	31.125	1
                    else if (lstStr.Count == 5)
                    {
                        n1 = list[list.Count - 1].NodeNo;
                        x = list[list.Count - 1].X;
                        y = list[list.Count - 1].Y;
                        z = list[list.Count - 1].Z;

                        n2 = int.Parse(lstStr[0]);
                        dx = double.Parse(lstStr[1]);
                        dy = double.Parse(lstStr[2]);
                        dz = double.Parse(lstStr[3]);

                        addFactor = int.Parse(lstStr[4]);

                        nodeDif = (n2 - n1) / addFactor;

                        //kx = (x + dx) / (nodeDif);
                        //ky = (y + dy) / (nodeDif);
                        //kz = (z + dz) / (nodeDif);
                        kx = (dx - x) / (nodeDif);
                        ky = (dy - y) / (nodeDif);
                        kz = (dz - z) / (nodeDif);

                        for (int i = 1; i < nodeDif; i++)
                        {
                            jc = new JointNode();
                            jc.NodeNo = n1 + addFactor * i;
                            jc.XYZ = new  gPoint(x + kx * i, y + ky * i, z + kz * i);
                            Add(jc);
                        }
                        jc = new JointNode();
                        jc.NodeNo = n2;
                        jc.XYZ = new gPoint(dx, dy, dz);
                        Add(jc);
                    }
                    // Example:
                    //1 0 0 0 5 20 0 0 ;
                    else if (lstStr.Count == 8)
                    {
                        try
                        {
                            int indx = 0;
                            n1 = int.Parse(lstStr[indx]); indx++;
                            x = double.Parse(lstStr[indx]); indx++;
                            y = double.Parse(lstStr[indx]); indx++;
                            z = double.Parse(lstStr[indx]); indx++;

                            jc = new JointNode();
                            jc.NodeNo = n1;
                            jc.XYZ = new gPoint(x, y, z);
                            Add(jc);


                            n2 = int.Parse(lstStr[indx]); indx++;
                            dx = double.Parse(lstStr[indx]); indx++;
                            dy = double.Parse(lstStr[indx]); indx++;
                            dz = double.Parse(lstStr[indx]); indx++;

                            kdif = n2 - n1;

                            kx = (x + dx) / (kdif);
                            ky = (y + dy) / (kdif);
                            kz = (z + dz) / (kdif);

                            for (int i = 1; i < kdif; i++)
                            {
                                jc = new JointNode();
                                jc.NodeNo = n1 + i;
                                jc.XYZ = new gPoint(x + kx * i, y + ky * i, z + kz * i);
                                Add(jc);
                            }
                            jc = new JointNode();
                            jc.NodeNo = n2;
                            jc.XYZ = new gPoint(dx, dy, dz);
                            Add(jc);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                }
                catch (Exception exx)
                {
                }
            }
        }

    }
    public class Member
    {
        public Member()
        {
            MemberNo = -1;
            StartNode = new JointNode();
            EndNode = new JointNode();
        }
        public int MemberNo { get; set; }
        public JointNode StartNode { get; set; }
        public JointNode EndNode { get; set; }
        public double Angle3D
        {
            get
            {
                if (StartNode != null && EndNode != null)
                {
                    return StartNode.XYZ.GetAngle(EndNode.XYZ);
                }
                return 0.0;
            }
        }
        public double InclinationAngle_in_Radian
        {
            get
            {
                if (StartNode != null && EndNode != null)
                {
                    return Math.Atan((Math.Abs(EndNode.Y - StartNode.Y)) / (Math.Abs(EndNode.X - StartNode.X)));
                }
                return 0.0;
            }
        }
        public double InclinationAngle_in_Degree
        {
            get
            {
                double v = ((180.0 / Math.PI)) * InclinationAngle_in_Radian;
                if (v > 90)
                    v -= 90;
                return v;
            }
        }
        public double Length
        {
            get
            {
                return StartNode.XYZ.Distance3D(EndNode.XYZ);
            }
        }
        public static Member Parse(string s)
        {
            string str = s.Replace(",", " ").ToUpper();
            str = s.Replace(";", " ").ToUpper();

            str = MyList.RemoveAllSpaces(str);

            MyList mList = new MyList(str, ' ');
            Member mem;
            if (mList.Count == 3)
            {
                mem = new Member();
                //1                1                2                
                mem.MemberNo = mList.GetInt(0);
                mem.StartNode.NodeNo = mList.GetInt(1);
                mem.EndNode.NodeNo = mList.GetInt(2);
            }
            else
            {
                throw new Exception("Invalid Member Data : " + s);
            }
            return mem;
        }

        public override string ToString()
        {
            return string.Format("{0,-7} {1,7} {2,7}", MemberNo, StartNode.NodeNo, EndNode.NodeNo);
        }
    }
    public class MemberCollection : List<Member>
    {
        //List<Member> this = null;
        //List<Member> this
        //{
        //    get
        //    {
        //        return this;
        //    }
        //    set
        //    {
        //         this = value;
        //    }
        //}
        public MemberCollection():base()
        {
            //this = new List<Member>();
        }
        //Chiranjit [2011 06 15]
        // Initialize Data from another object
        public MemberCollection(MemberCollection mem)
        {
            try
            {
                this.Clear();
                this.AddRange(mem);
                //this = new List<Member>(mem.Members);
            }
            catch (Exception ex) 
            {

            }
        }
        public List<Member> Members
        {
            get
            {
                return this;
            }
        }
        public void AddTXT(string itemData)
        {
            string[] values = itemData.Split(new char[] { ';' });
            Member mIncnd1, mIncnd2;

            List<string> lstStr = new List<string>();
            string kStr = "";
            int n1 = 0, sNode = 0, eNode = 0, toNode = 0, indx = 0;

            foreach (string str in values)
            {
                kStr = str.Trim().TrimEnd().TrimStart();
                lstStr.Clear();
                lstStr.AddRange(kStr.Split(new char[] { ' ' }));
                try
                {
                    if (lstStr.Count == 4)
                    {
                        indx = 0;
                        n1 = int.Parse(lstStr[indx]); indx++;
                        sNode = int.Parse(lstStr[indx]); indx++;
                        eNode = int.Parse(lstStr[indx]); indx++;
                        toNode = int.Parse(lstStr[indx]); indx++;

                        mIncnd1 = new Member();
                        mIncnd1.MemberNo = n1;
                        mIncnd1.StartNode.NodeNo = sNode;
                        mIncnd1.EndNode.NodeNo = eNode;
                        this.Add(mIncnd1);

                        for (int i = n1 + 1; i <= toNode; i++)
                        {
                            sNode++; eNode++;
                            mIncnd2 = new Member();
                            mIncnd2.MemberNo = i;
                            mIncnd2.StartNode.NodeNo = sNode;
                            mIncnd2.EndNode.NodeNo = eNode;
                            this.Add(mIncnd2);
                        }
                    }
                    else if (lstStr.Count == 6)
                    {
                        indx = 0;
                        n1 = int.Parse(lstStr[indx]); indx++;
                        sNode = int.Parse(lstStr[indx]); indx++;
                        eNode = int.Parse(lstStr[indx]); indx++;
                        toNode = int.Parse(lstStr[indx]); indx++;

                        int sni = int.Parse(lstStr[indx]); indx++;
                        int eni = int.Parse(lstStr[indx]); indx++;


                        mIncnd1 = new Member();
                        mIncnd1.MemberNo = n1;
                        mIncnd1.StartNode.NodeNo = sNode;
                        mIncnd1.EndNode.NodeNo = eNode;
                        this.Add(mIncnd1);

                        for (int i = n1 + 1; i <= toNode; i++)
                        {
                            sNode += sni; eNode += eni;
                            mIncnd2 = new Member();
                            mIncnd2.MemberNo = i;
                            mIncnd2.StartNode.NodeNo = sNode;
                            mIncnd2.EndNode.NodeNo = eNode;
                            this.Add(mIncnd2);
                        }
                    }
                    else
                    {
                        this.Add(Member.Parse(str));
                    }
                }
                catch (Exception exx)
                {
                }
                lstStr.Clear();
            }
        }

        public bool Contains(Member item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].MemberNo == item.MemberNo)
                {
                    return true;
                }
            }
            return false;
        }

        //public Member this[int index]
        //{
        //    get
        //    {
        //        if(this.Count >0)
        //        return this[index];
        //        return null;
        //    }
        //}
        //public void Add(Member m)
        //{
        //    Add(m);
        //}
        //public void Clear()
        //{
        //    Clear();
        //}
        //public int Count
        //{
        //    get
        //    {
        //        return Count;
        //    }
        //}
        public void ReadMembers(List<string> ASTRA_Data)
        {
            try
            {
                string kStr = "";
                bool flag = false;

                int n1 = 0, sNode = 0, eNode = 0, toNode = 0, indx = 0;
                Member mIncnd1, mIncnd2;

                MyList mlist = null;
                for (int i = 0; i < ASTRA_Data.Count; i++)
                {
                    kStr = ASTRA_Data[i].ToUpper();
                    if (kStr.Contains("MEMBER"))
                    {
                        flag = true; continue;
                    }
                    if (flag)
                    {
                        try
                        {
                            //kStr = kStr.Replace(";", " ").Trim().TrimEnd();

                            MyList mtxt = new MyList(kStr, ';');


                            foreach (var item in mtxt.StringList)
                            {
                                kStr = item;

                                mlist = new MyList(kStr, ' ');


                                if (mlist.Count == 4)
                                {
                                    indx = 0;
                                    n1 = int.Parse(mlist[indx]); indx++;
                                    sNode = int.Parse(mlist[indx]); indx++;
                                    eNode = int.Parse(mlist[indx]); indx++;
                                    toNode = int.Parse(mlist[indx]); indx++;

                                    mIncnd1 = new Member();
                                    mIncnd1.MemberNo = n1;
                                    mIncnd1.StartNode.NodeNo = sNode;
                                    mIncnd1.EndNode.NodeNo = eNode;
                                    this.Add(mIncnd1);

                                    for (int j = n1 + 1; j <= toNode; j++)
                                    {
                                        sNode++; eNode++;
                                        mIncnd2 = new Member();
                                        mIncnd2.MemberNo = j;
                                        mIncnd2.StartNode.NodeNo = sNode;
                                        mIncnd2.EndNode.NodeNo = eNode;
                                        this.Add(mIncnd2);
                                    }
                                }
                                else if (mlist.Count == 6)
                                {
                                    indx = 0;
                                    n1 = int.Parse(mlist[indx]); indx++;
                                    sNode = int.Parse(mlist[indx]); indx++;
                                    eNode = int.Parse(mlist[indx]); indx++;
                                    toNode = int.Parse(mlist[indx]); indx++;
                                    //Start Node Increment
                                    int sn_inc = int.Parse(mlist[indx]); indx++;
                                    //End Node Increment
                                    int en_inc = int.Parse(mlist[indx]); indx++;
                                    mIncnd1 = new Member();
                                    mIncnd1.MemberNo = n1;
                                    mIncnd1.StartNode.NodeNo = sNode;
                                    mIncnd1.EndNode.NodeNo = eNode;
                                    this.Add(mIncnd1);

                                    for (int j = n1 + 1; j <= toNode; j++)
                                    {
                                        sNode += sn_inc;
                                        eNode += en_inc;
                                        mIncnd2 = new Member();
                                        mIncnd2.MemberNo = j;
                                        mIncnd2.StartNode.NodeNo = sNode;
                                        mIncnd2.EndNode.NodeNo = eNode;
                                        this.Add(mIncnd2);
                                    }
                                }
                                else
                                    this.Add(Member.Parse(kStr));

                            }
                            kStr = ASTRA_Data[i].ToUpper();


                        }
                        catch (Exception ex) { }
                    }
                    if (kStr.Contains("MEMBER TRUSS")) break;
                    if (kStr.Contains("SECTION")) break;
                    if (kStr.Contains("MATERIAL")) break;
                }
            }
            catch (Exception ex) { }
        }

        public void SetCoordinates(JointNodeCollection joints)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].EndNode = joints.GetJoints(this[i].EndNode.NodeNo);
                this[i].StartNode = joints.GetJoints(this[i].StartNode.NodeNo);
            }
        }
        public Member GetMember(int memNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].MemberNo == memNo)
                    return this[i];
            }
            return null;
        }

        public double Get_Member_Length(string mem_def)
        {
            string kStr = MyList.RemoveAllSpaces(mem_def);
            MyList mList = new MyList(kStr, ' ');

            Member mem = GetMember(mList.GetInt(0));

            if (mem != null) return mem.Length;

            return 0.0;
        }
        public List<double> Get_Member_Lengths(string mem_def)
        {
            List<double> lens = new List<double>();
            string kStr = MyList.RemoveAllSpaces(mem_def);
            MyList mList = new MyList(kStr, ' ');

            List<int> mems = MyList.Get_Array_Intiger(mem_def);

            foreach (var item in mems)
            {
                Member mem = GetMember(item);
                if (mem != null) lens.Add(mem.Length);
            }

            return lens;
        }
    }

    public class MemberGroup
    {
        string memText = "";
        public enum eMType
        {
            TRUSS = 1,
            BEAM = 2,
            CABLE = 3,
        }
        public MemberGroup()
        {
            GroupName = "";
            MemberNosText = "";
            MemberNos = new List<int>();
            GroupType = eMType.BEAM;
        }
        public string GroupName { get; set; }
        //public string GroupTag
        //{
        //    get
        //    {
        //        return string.Format("{0} [{1}]", GroupName, GroupType);
        //    }
        //}
        //public MemberCollection MemberColl { get; set; }
        public eMType GroupType { get; set; }
        public string MemberNosText
        { 
            get
            {
                if (memText == "" && MemberNos.Count > 0)
                {
                    foreach (var item in MemberNos) memText += item + " ";
                }
                return memText;
            }
            set
            {
                memText = value;

            }
        }
        public static MemberGroup Parse(string str)
        {

            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                
            string kStr = "";
            kStr = str.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mList = new MyList(kStr, ' ');
            
            MemberGroup mGrp = new MemberGroup();

            mGrp.GroupName = mList.StringList[0];
            for (int i = 1; i < mList.Count; i++)
            {
                mGrp.MemberNosText += mList.StringList[i] + " ";

            }
            //mGrp.SetMemNos();
            return mGrp;

        }

        public void SetMemNos()
        {
            if (memText == "") return;
            MemberNos = MyList.Get_Array_Intiger(memText);
            //MemberNos = new List<int>();

            //string str = MyList.RemoveAllSpaces(memText);
            //MyList mList = new MyList(str, ' ');

            //int indx = -1;
            //int count = 0;
            //int i = 0;
            //do
            //{
            //    indx = mList.StringList.IndexOf("TO");
            //    if (indx == -1)
            //    {
            //        indx = mList.StringList.IndexOf("-");
            //    }
            //    if (indx != -1)
            //    {
            //        for (i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
            //        {
            //            MemberNos.Add(i);
            //        }
            //        mList.StringList.RemoveRange(0, indx + 2);
            //    }
            //    else if (mList.Count > 0)
            //    {
            //        for (i = 0; i < mList.Count; i++)
            //        {
            //            MemberNos.Add(mList.GetInt(i));
            //        }
            //        mList.StringList.Clear();
            //    }
            //}
            //while (mList.Count != 0 && mList.Count != 1);


        }
        public List<int> MemberNos { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", GroupName, MemberNosText);
        }
    }

    public class MemberGroupCollection
    {
        Hashtable hash_ta;
        public List<MemberGroup> GroupCollection { get; set; }
        public MemberGroupCollection()
        {
            hash_ta = new Hashtable();
            GroupCollection = new List<MemberGroup>();
        }
        #region Chiranjit [2012 02 20]
        public void ReadFromFile_OLD(List<string> astra_data, MemberCollection All_Members)
        {
            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                
            string kStr = "";
            bool flag = false;
            for (int i = 0; i < astra_data.Count; i++)
            {
                kStr = astra_data[i].Replace(',', ' ');
                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr.StartsWith("*")) continue;
                if (kStr.StartsWith("//")) continue;
                MyList mList = new MyList(kStr, ' ');

                if (kStr.Contains("START GROUP"))
                {
                    flag = true; continue;
                }
                else if (kStr.Contains("END") && kStr.Contains("XGIRDER_END") == false)
                {
                    flag = false; break;
                }
                if (flag)
                {
                    //int toIndex = mList.StringList.IndexOf("T0"
                    GroupCollection.Add(MemberGroup.Parse(kStr));
                    hash_ta.Add(mList.StringList[0], GroupCollection[GroupCollection.Count - 1]);
                }

            }
        }

        public void ReadFromFile(List<string> astra_data, MemberCollection All_Members)
        {
            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                
            string kStr = "";
            string gname = "";
            MemberGroup memGroup = null;
            bool flag1 = false;
            bool flag2 = false;
            for (int i = 0; i < astra_data.Count; i++)
            {
                kStr = astra_data[i].Replace(',', ' ');
                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr.StartsWith("*")) continue;
                if (kStr.StartsWith("//")) continue;
                MyList mList = new MyList(kStr, ' ');

                if (kStr.Contains("START GROUP"))
                {
                    flag1 = true; continue;
                }
                else if (kStr.StartsWith("FINISH"))
                {
                    flag1 = false; break;
                }
                else if (kStr.StartsWith("END"))
                {
                    flag1 = false;
                    flag2 = true; continue;
                }

                //int toIndex = mList.StringList.IndexOf("T0"

                memGroup = hash_ta[mList.StringList[0]] as MemberGroup;
                if (memGroup != null)
                {
                    if (flag2)
                    {
                        memGroup = hash_ta[mList.StringList[0]] as MemberGroup;
                        if (mList.Count == 1)
                        {
                            gname = MyList.RemoveAllSpaces(astra_data[i - 1].ToUpper().Trim().TrimEnd().TrimStart());

                            mList = new MyList(gname, ' ');
                            if (mList.Count == 2)
                            {
                                if (mList.StringList[1] == "BEAM")
                                {
                                    memGroup.GroupType = MemberGroup.eMType.BEAM;
                                }
                                else if (mList.StringList[1] == "TRUSS")
                                {
                                    memGroup.GroupType = MemberGroup.eMType.TRUSS;
                                }
                                else if (mList.StringList[1] == "CABLE")
                                {
                                    memGroup.GroupType = MemberGroup.eMType.CABLE;
                                }
                            }
                            //hash_ta.Add(mList.StringList[0], GroupCollection[GroupCollection.Count - 1]);
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (flag1)
                        {
                            GroupCollection.Add(MemberGroup.Parse(kStr));
                            hash_ta.Add(mList.StringList[0], GroupCollection[GroupCollection.Count - 1]);
                        }

                    }
                    catch (Exception ex) { }
                }
            }
        }
        #endregion Chiranjit [2012 02 20]

        public MemberGroup GetMemberGroup(string memGroup)
        {
            return (MemberGroup)hash_ta[memGroup];
        }
    }
    public class LoadData
    {
        public LoadData()
        {
            TypeNo = "TYPE 6";
            Code = "IRC24RTRACK";
            X = -60.0;
            Y = 0.0d;
            Z = 1.0d;
            XINC = 0.5d;
            YINC = 0.0d;
            ZINC = 0.0d;
            LoadWidth = 0d;

            ImpactFactor = -1.0d;
        }
        public LoadData(LoadData obj)
        {
            TypeNo = obj.TypeNo;
            Code = obj.Code;
            X = obj.X;
            Y = obj.Y;
            Z = obj.Z;
            XINC = obj.XINC;
            YINC = obj.YINC;
            ZINC = obj.ZINC;
            LoadWidth = obj.LoadWidth;

            ImpactFactor = obj.ImpactFactor;

            Loads = new MyList(obj.Loads);
            Distances = new MyList(obj.Distances);

        }
        public string TypeNo { get; set; }
        public string Code { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double XINC { get; set; }
        public double YINC { get; set; }
        public double ZINC { get; set; }
        public double LoadWidth { get; set; }
        public MyList Loads { get; set; }
        public MyList Loads_In_KN
        {
            get
            {
                if (Loads == null) return null;
                string str = "";
                for (int i = 0; i < Loads.Count; i++)
                {
                    try
                    {
                        str += " " + (Loads.GetDouble(i) * 10).ToString("f3");
                    }
                    catch (Exception ex) { }
                }

                str = MyList.RemoveAllSpaces(str.Replace(",",""));
                return new MyList(str, ' ');
            }
        }
        public MyList Distances { get; set; }
        public double Distance
        {
            get
            {
                double d = 0.0;

                for (int i = 0; i < Distances.Count; i++)
                {
                    d += MyList.StringToDouble(Distances.StringList[i], 0.0);
                }
                return -d;
            }
        }

        public double Total_Loads
        {
            get
            {
                double d = 0.0;

                for (int i = 0; i < Loads.Count; i++)
                {
                    d += MyList.StringToDouble(Loads.StringList[i], 0.0);
                }
                return d;
            }
        }

        public double Default_ImpactFactor
        {
            get
            {
                switch (Code)
                {
                    case "IRCCLASSA":
                        return 1.179;
                        break;
                    case "IRCCLASSB":
                        return 1.188;
                        break;
                    case "LRFD_HTL57":
                        return 1.25;
                        break;
                    case "LRFD_HL93_HS20":
                        return 1.25;
                        break;
                    case "LRFD_HL93_H20":
                        return 1.25;
                        break;
                    case "IRC70RTRACK":
                        return 1.10;
                        break;
                    case "IRC70RWHEEL":
                        return 1.179;
                        break;
                    case "IRCCLASSAATRACK":
                        return 1.188;
                        break;
                    case "IRC24RTRACK":
                        return 1.188;
                        break;
                    case "BG_RAIL_1":
                        return 1.90;
                        break;
                    case "BG_RAIL_2":
                        return 1.90;
                        break;
                    case "MG_RAIL_1":
                        return 1.90;
                        break;
                    case "MG_RAIL_2":
                        return 1.90;
                        break;
                }
                return 1.25;
            }
            /*
            get
            {
                switch (TypeNo)
                {
                    case "TYPE1":
                    case "TYPE 1":
                        return 1.179;
                        break;
                    case "TYPE2":
                    case "TYPE 2":
                        return 1.188;
                        break;
                    case "TYPE3":
                    case "TYPE 3":
                        return 1.25;
                        break;
                    case "TYPE4":
                    case "TYPE 4":
                        return 1.25;
                        break;
                    case "TYPE5":
                    case "TYPE 5":
                        return 1.25;
                        break;
                    case "TYPE6":
                    case "TYPE 6":
                        return 1.10;
                        break;
                    case "TYPE7":
                    case "TYPE 7":
                        return 1.179;
                        break;
                    case "TYPE8":
                    case "TYPE 8":
                        return 1.188;
                        break;
                    case "TYPE9":
                    case "TYPE 9":
                        return 1.188;
                        break;
                    case "TYPE10":
                    case "TYPE11":
                    case "TYPE12":
                    case "TYPE13":
                    case "TYPE14":
                    case "TYPE 10":
                    case "TYPE 11":
                    case "TYPE 12":
                    case "TYPE 13":
                    case "TYPE 14":
                        return 1.90;
                        break;
                }
                return 1.25;
            }
            /**/
        }
        double im_fact = -1;
        public double ImpactFactor
        {
            get { if (im_fact == -1) im_fact = Default_ImpactFactor; return im_fact; }
            set { im_fact = value; }
        }
        public static LoadData Parse(string txt)
        {
            //TYPE 2 -48.750 0 0.500 XINC 0.2

            txt = txt.ToUpper().Replace("TYPE", "");
            txt = txt.Replace(",", " ");
            txt = MyList.RemoveAllSpaces(txt);
            MyList mlist = new MyList(txt, ' ');
            LoadData ld = new LoadData();

            ld.TypeNo = "TYPE " + mlist.GetInt(0);
            ld.X = mlist.GetDouble(1);
            ld.Y = mlist.GetDouble(2);
            ld.Z = mlist.GetDouble(3);

            if (mlist.StringList[4] == "XINC")
                ld.XINC = mlist.GetDouble(5);
            if (mlist.StringList[4] == "YINC")
                ld.YINC = mlist.GetDouble(5);
            if (mlist.StringList[4] == "ZINC")
                ld.ZINC = mlist.GetDouble(5);

            return ld;

        }
        public static List<LoadData> GetLiveLoads(List<string> content)
        {
            //if (!File.Exists(file_path)) return null;

            //TYPE1 IRCCLASSA
            //68 68 68 68 114 114 114 27
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80
            List<LoadData> LL_list = new List<LoadData>();
            //List<string> file_content = new List<string>(File.ReadAllLines(file_path));
            MyList mlist = null;
            string kStr = "";

            int icount = 0;
            for (int i = 0; i < content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(content[i]);
                kStr = kStr.Replace(',', ' ');
                kStr = MyList.RemoveAllSpaces(kStr);
                mlist = new MyList(kStr, ' ');

                if (mlist.StringList[0].Contains("TYPE"))
                {
                    LoadData ld = new LoadData();
                    if (mlist.Count == 2)
                    {
                        kStr = mlist.StringList[0].Replace("TYPE", "");
                        ld.TypeNo = "TYPE " + kStr;
                        ld.Code = mlist.StringList[1];
                        LL_list.Add(ld);
                    }
                    else if (mlist.Count == 3)
                    {
                        ld.TypeNo = "TYPE " + mlist.StringList[1];
                        ld.Code = mlist.StringList[2];
                        LL_list.Add(ld);
                    }
                    if ((i + 3) < content.Count)
                    {
                        ld.Loads = new MyList(MyList.RemoveAllSpaces(content[i + 1].Replace(",", " ")), ' ');
                        ld.Distances = new MyList(MyList.RemoveAllSpaces(content[i + 2].Replace(",", " ")), ' ');
                        ld.LoadWidth = MyList.StringToDouble(content[i + 3], 0.0);
                    }
                }
            }
            return LL_list;

            //TYPE 2 IRCCLASSB
            //20.5 20.5 20.5 20.5 34.0 34.0 8.0 8.0
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80

            //TYPE 3 IRC70RTRACK
            //70 70 70 70 70 70 70 70 70 70
            //0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457
            //0.84

            //TYPE 4 IRC70RWHEEL
            //85 85 85 85 60 60 40
            //1.37 3.05 1.37 2.13 1.52 3.96
            //0.450 1.480 0.450

            //TYPE 5 IRCCLASSAATRACK
            //70 70 70 70 70 70 70 70 70 70
            //0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360
            //0.85

            //TYPE 6 IRC24RTRACK
            //62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5
            //0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366
            //0.36
        }
        public static List<LoadData> GetLiveLoads(string file_path)
        {

            if (!File.Exists(file_path)) return null;

            List<string> file_content = new List<string>(File.ReadAllLines(file_path));

            return GetLiveLoads(file_content);
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2:f3} ", TypeNo, Code, ((ImpactFactor != -1.0) ? ImpactFactor : Default_ImpactFactor));
        }
        public void ToStream(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("{0} {1}", TypeNo, Code);
            sw.WriteLine("{0}", Loads.ToString());
            sw.WriteLine("{0}", Distances.ToString());
            sw.WriteLine("{0:f3}", LoadWidth);
            sw.WriteLine();
        }
        public void ToStream_In_KN(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("{0} {1}", TypeNo, Code);
            sw.WriteLine("{0}", Loads_In_KN.ToString());
            sw.WriteLine("{0}", Distances.ToString());
            sw.WriteLine("{0:f3}", LoadWidth);
            sw.WriteLine();
        }
        public string Data
        {
            get
            {
                string str = "";
                str += string.Format("{0}  {1}  {2:f3}", TypeNo, Code, ImpactFactor) + "\n\r";
                str += string.Format("{0}", Loads.ToString()) + "\n\r"; ;
                str += string.Format("{0}", Distances.ToString()) + "\n\r"; ;
                str += string.Format("{0:f3}", LoadWidth);
                str += "";

                return str;
            }
        }


        public List<string> DataArray
        {
            get
            {
                List<string> list = new List<string>();
                string str = "";
                list.Add(string.Format("{0}  {1}", TypeNo, Code));
                list.Add(string.Format("Loads {0}", Loads.ToString()));
                list.Add(string.Format("Distances {0}", Distances.ToString()));
                list.Add(string.Format("Load Width {0:f3}", LoadWidth));
                list.Add(string.Format("Impact Factor {0:f3}", ImpactFactor));
                list.Add(string.Format(""));
                list.Add(string.Format("Total Loads : {0:f3}", Loads.SUM));
                list.Add(string.Format("Total Distances : {0:f3}", Distances.SUM));

                return list;
            }
        }
        public string Data_KN
        {
            get
            {
                string str = "";
                str += string.Format("{0}  {1}  {2:f3}", TypeNo, Code, ImpactFactor) + "\n\r";
                str += string.Format("{0}", Loads_In_KN.ToString()) + "\n\r"; ;
                str += string.Format("{0}", Distances.ToString()) + "\n\r"; ;
                str += string.Format("{0:f3}", LoadWidth);
                str += "";

                return str;
            }
        }

        public double Get_Maximum_Load()
        {
            double max = 0;
            for (int i = 0; i < Loads.Count; i++)
            {
                try
                {
                    if (max <= Loads.GetDouble(i))
                        max = Loads.GetDouble(i);
                }
                catch (Exception ex) { }
            }
            return max;
        }
    }
    public class LiveLoadCollections : List<LoadData>
    {
        public LiveLoadCollections(string file_name)
            : base()
        {
            try
            {
                if (File.Exists(file_name))
                {

                    AddRange(LoadData.GetLiveLoads(file_name));
                }
            }
            catch (Exception ex) { }
        }
        public LiveLoadCollections()
            : base()
        {
            
        }
        public void Fill_Combo(ref ComboBox cmb)
        {
            try
            {
                cmb.Items.Clear();

                foreach (var item in this)
                {
                    cmb.Items.Add(item.TypeNo + " : " + item.Code);
                }
                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;

            }
            catch (Exception ex) { }
        }

        public void Fill_Combo_Without_Type(ref ComboBox cmb)
        {
            try
            {
                cmb.Items.Clear();

                foreach (var item in this)
                {
                    cmb.Items.Add(item.Code);
                }
                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;

            }
            catch (Exception ex) { }
        }

        public void Impact_Factor_2012_02_09(ref List<string> list, bool IsAASHTO)
        {
            if (!list.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            if (IsAASHTO)
            {

                if (!list.Contains("TYPE 1 LRFD_HTL57 1.25"))
                    list.Add("TYPE 1 LRFD_HTL57 1.25");
                if (!list.Contains("TYPE 2 LRFD_HL93_HS20 1.25"))
                    list.Add("TYPE 2 LRFD_HL93_HS20 1.25");
                if (!list.Contains("TYPE 3 LRFD_HL93_H20 1.25"))
                    list.Add("TYPE 3 LRFD_HL93_H20 1.25");
                if (!list.Contains("TYPE 4 CLA 1.179"))
                    list.Add("TYPE 4 CLA 1.179");
                if (!list.Contains("TYPE 5 CLB 1.188"))
                    list.Add("TYPE 5 CLB 1.188");
                if (!list.Contains("TYPE 6 A70RT 1.10"))
                    list.Add("TYPE 6 A70RT 1.10");
                if (!list.Contains("TYPE 7 CLAR 1.179"))
                    list.Add("TYPE 7 CLAR 1.179");
                if (!list.Contains("TYPE 8 A70RR 1.188"))
                    list.Add("TYPE 8 A70RR 1.188");
                if (!list.Contains("TYPE 9 IRC24RTRACK 1.188"))
                    list.Add("TYPE 9 IRC24RTRACK 1.188");
                if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                    list.Add("TYPE 10 BG_RAIL_1 1.9");
                if (!list.Contains("TYPE 11 BG_RAIL_2 1.9"))
                    list.Add("TYPE 11 BG_RAIL_2 1.90");
                if (!list.Contains("TYPE 12 MG_RAIL_1 1.9"))
                    list.Add("TYPE 12 MG_RAIL_1 1.90");
                if (!list.Contains("TYPE 13 MG_RAIL_2 1.9"))
                    list.Add("TYPE 13 MG_RAIL_2 1.90");
            }
            else
            {

                if (!list.Contains("TYPE 1 CLA 1.179"))
                    list.Add("TYPE 1 CLA 1.179");
                if (!list.Contains("TYPE 2 CLB 1.188"))
                    list.Add("TYPE 2 CLB 1.188");
                if (!list.Contains("TYPE 3 A70RT 1.10"))
                    list.Add("TYPE 3 A70RT 1.10");
                if (!list.Contains("TYPE 4 CLAR 1.179"))
                    list.Add("TYPE 4 CLAR 1.179");
                if (!list.Contains("TYPE 5 A70RR 1.188"))
                    list.Add("TYPE 5 A70RR 1.188");
                if (!list.Contains("TYPE 6 IRC24RTRACK 1.188"))
                    list.Add("TYPE 6 IRC24RTRACK 1.188");
                if (!list.Contains("TYPE 7 LRFD_HTL57 1.25"))
                    list.Add("TYPE 7 LRFD_HTL57 1.25");
                if (!list.Contains("TYPE 8 LRFD_HL93_HS20 1.25"))
                    list.Add("TYPE 8 LRFD_HL93_HS20 1.25");
                if (!list.Contains("TYPE 9 LRFD_HL93_H20 1.25"))
                    list.Add("TYPE 9 LRFD_HL93_H20 1.25");
                if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                    list.Add("TYPE 10 BG_RAIL_1 1.9");
                if (!list.Contains("TYPE 11 BG_RAIL_2 1.9"))
                    list.Add("TYPE 11 BG_RAIL_2 1.90");
                if (!list.Contains("TYPE 12 MG_RAIL_1 1.9"))
                    list.Add("TYPE 12 MG_RAIL_1 1.90");
                if (!list.Contains("TYPE 13 MG_RAIL_2 1.9"))
                    list.Add("TYPE 13 MG_RAIL_2 1.90");
            }
        }
        //Chiranjit [2012 02 09]
        public void Impact_Factor(ref List<string> list, eDesignStandard des_stnd)
        {
            bool IsAASHTO = (des_stnd == eDesignStandard.BritishStandard);

            if (!list.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                list.Add("DEFINE MOVING LOAD FILE LL.TXT");

            foreach (var item in this)
            {
                list.Add(item.ToString());
            }
            if (false)
            {
                #region set Impact Factor


                if (IsAASHTO)
                {

                    if (!list.Contains("TYPE 1 LRFD_HTL57 1.25"))
                        list.Add("TYPE 1 LRFD_HTL57 1.25");
                    if (!list.Contains("TYPE 2 LRFD_HL93_HS20 1.25"))
                        list.Add("TYPE 2 LRFD_HL93_HS20 1.25");
                    if (!list.Contains("TYPE 3 LRFD_HL93_H20 1.25"))
                        list.Add("TYPE 3 LRFD_HL93_H20 1.25");
                    if (!list.Contains("TYPE 4 CLA 1.179"))
                        list.Add("TYPE 4 CLA 1.179");
                    if (!list.Contains("TYPE 5 CLB 1.188"))
                        list.Add("TYPE 5 CLB 1.188");
                    if (!list.Contains("TYPE 6 A70RT 1.10"))
                        list.Add("TYPE 6 A70RT 1.10");
                    if (!list.Contains("TYPE 7 CLAR 1.179"))
                        list.Add("TYPE 7 CLAR 1.179");
                    if (!list.Contains("TYPE 8 A70RR 1.188"))
                        list.Add("TYPE 8 A70RR 1.188");
                    if (!list.Contains("TYPE 9 IRC24RTRACK 1.188"))
                        list.Add("TYPE 9 IRC24RTRACK 1.188");
                    if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                        list.Add("TYPE 10 BG_RAIL_1 1.9");
                    if (!list.Contains("TYPE 11 BG_RAIL_2 1.90"))
                        list.Add("TYPE 11 BG_RAIL_2 1.90");
                    if (!list.Contains("TYPE 12 MG_RAIL_1 1.90"))
                        list.Add("TYPE 12 MG_RAIL_1 1.90");
                    if (!list.Contains("TYPE 13 MG_RAIL_2 1.90"))
                        list.Add("TYPE 13 MG_RAIL_2 1.90");
                }

                else
                {

                    if (!list.Contains("TYPE 1 CLA 1.179"))
                        list.Add("TYPE 1 CLA 1.179");
                    if (!list.Contains("TYPE 2 CLB 1.188"))
                        list.Add("TYPE 2 CLB 1.188");
                    if (!list.Contains("TYPE 3 A70RT 1.10"))
                        list.Add("TYPE 3 A70RT 1.10");
                    if (!list.Contains("TYPE 4 CLAR 1.179"))
                        list.Add("TYPE 4 CLAR 1.179");
                    if (!list.Contains("TYPE 5 A70RR 1.188"))
                        list.Add("TYPE 5 A70RR 1.188");
                    if (!list.Contains("TYPE 6 IRC24RTRACK 1.188"))
                        list.Add("TYPE 6 IRC24RTRACK 1.188");
                    if (!list.Contains("TYPE 7 LRFD_HTL57 1.25"))
                        list.Add("TYPE 7 LRFD_HTL57 1.25");
                    if (!list.Contains("TYPE 8 LRFD_HL93_HS20 1.25"))
                        list.Add("TYPE 8 LRFD_HL93_HS20 1.25");
                    if (!list.Contains("TYPE 9 LRFD_HL93_H20 1.25"))
                        list.Add("TYPE 9 LRFD_HL93_H20 1.25");
                    if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                        list.Add("TYPE 10 BG_RAIL_1 1.9");
                    if (!list.Contains("TYPE 11 BG_RAIL_2 1.90"))
                        list.Add("TYPE 11 BG_RAIL_2 1.90");
                    if (!list.Contains("TYPE 12 MG_RAIL_1 1.90"))
                        list.Add("TYPE 12 MG_RAIL_1 1.90");
                    if (!list.Contains("TYPE 13 MG_RAIL_2 1.90"))
                        list.Add("TYPE 13 MG_RAIL_2 1.90");
                }
                #endregion set Impact Factor
            }
        }

        public void Save_LL_TXT(string folder, bool IsMTon)
        {

            StreamWriter sw = new StreamWriter(Path.Combine(folder, "LL.TXT"));
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("FILE LL.TXT");
                sw.WriteLine();
                sw.WriteLine();
                if (IsMTon)
                {
                    foreach (var item in this)
                    {
                        item.ToStream(sw);
                    }
                }
                else
                {
                    foreach (var item in this)
                    {
                        item.ToStream_In_KN(sw);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public double Get_Distance(string type_no)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].TypeNo == type_no)
                    return this[i].Distance;
            }
            return 0.0;
        }

        public LoadData Get_LoadData(string code)
        {
            foreach (var item in this)
            {
                if (code == item.Code)
                    return item;
            }
            return null;
        }
        public string Get_CodeByType(string type)
        {
            foreach (var item in this)
            {
                if (type == item.TypeNo)
                    return item.Code;
            }
            return "";
        }
    }
}
