using System;
using System.Collections.Generic;
using System.Text;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using System.IO;
using System.Threading;

namespace AstraInterface.DataStructure
{
    public class CAstraDoc:IDocument
    {
        public CAstraDoc()
        {

        }

        // Astra Document Variables
        Thread thd;
        string strTitle;
        string FileName = "";
        int i;
        short lfct_flag, wfct_flag;
        double lfact, wfact;
        string lunit = "", munit = "";
        short modex = -1;
        public delegate void myDele();

        CBasicInfo basicInfo = new CBasicInfo();
        StructureType StructType = StructureType.PLANE;
        CNodeDataCollection NodeDataArr = new CNodeDataCollection();
        CMemberConnectivityCollection BeamConnArr = new CMemberConnectivityCollection();
        CSectionPropertyCollection SecPropArr = new CSectionPropertyCollection();
        CMaterialPropertyCollection MatPropArr = new CMaterialPropertyCollection();
        CJointNodalLoadCollection JointNodalLoadArr = new CJointNodalLoadCollection();
        CMemberBeamLoadingCollection MebrBmLoadArr = new CMemberBeamLoadingCollection();
        CMemberTrussCollection MemberTrussArr = new CMemberTrussCollection();
        CSelfWeight SlfWgt = new CSelfWeight();
        CAnalysis ana = new CAnalysis();
        CTimeHistoryAnalysis timeHist = new CTimeHistoryAnalysis();
        CResponse response = new CResponse();
        CSupportCollection SupportArr = new CSupportCollection();
        CMovingLoadCollection MovingLoadArr = new CMovingLoadCollection();
        CLoadGenerationCollection LoadGenerationArr = new CLoadGenerationCollection();
        CBeamConnectivityReleaseCollection BeamConnectivityReleaseArr = new CBeamConnectivityReleaseCollection();
        CLoadCombinationCollection LoadCombArr = new CLoadCombinationCollection();
        CAreaLoadCollection AreaLoadArr = new CAreaLoadCollection();
        CElementDataCollection ElementDataArr = new CElementDataCollection();
        CMaterialPropertyInformationCollection MatPropInfoArr = new CMaterialPropertyInformationCollection();
        CElementMultiplierCollection ElementMultiplierArr = new CElementMultiplierCollection();
        // End Variable Declarion
        
        public string getNodeName(string strData)
        {
            if (strData.Length > 5)
                return strData.Substring(0, 4);
            else
                return "";
        }
        public void SetFact()
        {
            NodeData.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, NodeData.LengthUnit);
            NodeData.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, NodeData.MassUnit);

            SecPropArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, SecPropArr.LengthUnit);
            SecPropArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, SecPropArr.MassUnit);

            MatPropArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, MatPropArr.LengthUnit);
            MatPropArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, MatPropArr.MassUnit);

            JointNodalLoadArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, JointNodalLoadArr.LengthUnit);
            JointNodalLoadArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, JointNodalLoadArr.MassUnit);

            MebrBmLoadArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, MebrBmLoadArr.LengthUnit);
            MebrBmLoadArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, MebrBmLoadArr.MassUnit);

            MatPropArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, MatPropArr.LengthUnit);
            MatPropArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, MatPropArr.MassUnit);

            //MemberTrussArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, MemberTrussArr.LengthUnit);
            //MemberTrussArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, MemberTrussArr.MassUnit);

            MatPropInfoArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, MatPropInfoArr.LengthUnit);
            MatPropInfoArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, MatPropInfoArr.MassUnit);

            ElementDataArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, ElementDataArr.LengthUnit);
            ElementDataArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, ElementDataArr.MassUnit);

            MatPropInfoArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, MatPropInfoArr.LengthUnit);
            MatPropInfoArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, MatPropInfoArr.MassUnit);

            AreaLoadArr.LengthFactor = CAstraUnits.GetLengthFact(BasicInfo.LengthUnit, AreaLoadArr.LengthUnit);
            AreaLoadArr.MassFactor = CAstraUnits.GetMassFact(BasicInfo.MassUnit, AreaLoadArr.MassUnit);
        }
        #region IDocument Members
        public CBasicInfo BasicInfo
        {
            get
            {
                return basicInfo;
            }
            set
            {
                basicInfo = value;
            }
        }


        public string ProjectTitle
        {
            get
            {
                return this.strTitle;
            }
            set
            {

                this.strTitle = value;
                if (!this.strTitle.Contains("ASTRA"))
                {
                    strTitle = strTitle.Insert(0, "ASTRA ");
                }
            }
        }
        public StructureType SType
        {
            get
            {
                return this.StructType;
            }
            set
            {
                this.StructType = value;
            }
        }
        public CNodeDataCollection NodeData
        {
            get
            {
                return this.NodeDataArr;
            }
            set
            {
                this.NodeDataArr = value;
            }
        }

        public bool Read(string FilePath)
        {
            ClearVars();
            //To Do: read from file
            FileStream kFs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(kFs);
            string str = "";

            while (sr.EndOfStream == false)
            {
                str = sr.ReadLine();
                if (BasicInfo.UsersTitle == "")
                {
                    if (str.Contains("ASTRA"))
                    {
                        if (str.Length > 12)
                        {
                            str = str.Remove(0, 12);
                            BasicInfo.UsersTitle = str;
                            continue;
                        }
                    }
                }
                //if (ProjectTitle == "")
                //{
                //    ProjectTitle = str;
                //    continue;
                //}
                if (str.Contains("UNIT") && this.LUnit == "" && this.MUnit == "")
                {
                    string[] ss = CAstraUnits.GetBasicLengthMassUnits(str);
                    this.MUnit = ss[0];
                    this.LUnit = ss[1];

                    switch (this.MUnit.ToUpper())
                    {
                        case "MTON": this.wfct_flag = 1;
                            break;
                        case "KN": this.wfct_flag = 2;
                            break;
                        case "KG": this.wfct_flag = 3;
                            break;
                        case "NEW": this.wfct_flag = 4;
                            break;
                        case "N": this.wfct_flag = 4;
                            break;
                        case "GM": this.wfct_flag = 5;
                            break;
                        case "GMS": this.wfct_flag = 5;
                            break;
                        case "KIP": this.wfct_flag = 6;
                            break;
                        case "LBS": this.wfct_flag = 7;
                            break;
                    }
                    switch (this.LUnit.ToUpper())
                    {
                        case "MM": this.lfct_flag = 1;
                            break;
                        case "CM": this.lfct_flag = 2;
                            break;
                        case "M": this.lfct_flag = 3;
                            break;
                        case "ME": this.lfct_flag = 3;
                            break;
                        case "METRES": this.lfct_flag = 3;
                            break;
                        case "YDS": this.lfct_flag = 4;
                            break;
                        case "FT": this.lfct_flag = 5;
                            break;
                        case "INCH": this.lfct_flag = 6;
                            break;
                        case "IN": this.lfct_flag = 6;
                            break;
                    }
                }

                if (str.ToUpper().Contains("STRUCTURE"))
                {

                    str = str.Replace('\t', ' ');
                    string[] values = str.Split(new char[] { ' ' });
                    while (str.IndexOf("  ") != -1)
                    {
                        str = str.Replace("  ", " ");
                    }
                    if (values.Length == 3)
                    {
                        int.Parse(values[1]);
                        BasicInfo.Type = (StructureType)int.Parse(values[1]); 
                        BasicInfo.RunningOption = short.Parse(values[2]); 
                    }



                    //str = str.Remove(0, 10);
                    //str = str.Trim();
                    //string[] values = str.Split(new char[] { ' ' });
                    //while (str.IndexOf("  ") != -1)
                    //{
                    //    str = str.Replace("  ", " ");
                    //}
                    //str = str.Replace('\t', ' ');
                    //values = str.Split(new char[] { ' ' });
                    //str = values[0];
                    //StructureType = ((str == "1") ? StructureType.SPACE : (str == "2") ? StructureType.FLOOR : StructureType.PLANE);
                    //try { this.Modex = short.Parse(values[1]); }
                    //catch (Exception ex) { this.Modex = -1; }
                }
                string find = getNodeName(str);

                if (find == "N000")
                {
                    str = str.Replace('\t', ' ');
                    string[] values = str.Split(new char[] { ' ' });
                    while (str.IndexOf("  ") != -1)
                    {
                        str = str.Replace("  ", " ");
                    }
                    if (values.Length == 3)
                    {
                        BasicInfo.MassUnit = CAstraUnits.GetMassUnit(values[1]);
                        BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(values[2]);
                    }
                }
                if (find == "N001")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        NodeData.MassFactor = d[0];
                        NodeData.LengthFactor = d[1];

                        string[] unt = CAstraUnits.GetUnits(str);
                        BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        NodeData.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        NodeData.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                        

                    }
                    try
                    {
                        NodeData.Add(CNodeData.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N002")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        BeamConnectivity.MassFactor = d[0];
                        BeamConnectivity.LengthFactor = d[1];
                    }
                    try
                    {
                        BeamConnectivity.Add(CMemberConnectivity.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N003")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        SectionProperty.MassFactor = d[0];
                        SectionProperty.LengthFactor = d[1];

                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        SectionProperty.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        SectionProperty.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        SectionProperty.Add(CSectionProperty.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N004")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        MaterialProperty.MassFactor = d[0];
                        MaterialProperty.LengthFactor = d[1];

                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        MaterialProperty.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        MaterialProperty.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        MaterialProperty.Add(CMaterialProperty.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N005")
                {
                    try
                    {
                        Support.Add(CSupport.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N008")
                {
                    try
                    {
                        MemberTruss.Add(CMemberTruss.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N010")
                {
                    try
                    {
                         SelfWeight = (CSelfWeight.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N007")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        MemberBeamLoad.MassFactor = d[0];
                        MemberBeamLoad.LengthFactor = d[1];
                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        MemberBeamLoad.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        MemberBeamLoad.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        MemberBeamLoad.Add(CMemberBeamLoading.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N009")
                {
                    try
                    {
                        BeamConnectivityRelease.Add(CBeamConnectivityRelease.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N006")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        JointNodalLoad.MassFactor = d[0];
                        JointNodalLoad.LengthFactor = d[1];

                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        JointNodalLoad.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        JointNodalLoad.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        //CJointNodalLoad jntLoad = new CJointNodalLoad();
                        //jntLoad = CJointNodalLoad.Parse(str);
                        JointNodalLoad.Add(CJointNodalLoad.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }

                else if (find == "N099")
                {
                    try
                    {
                        Analysis = CAnalysis.Parse(str);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N011")
                {
                    if (str.Contains("FILE"))
                    {
                        int j = str.IndexOf("FILE");
                        str = str.Remove(0, j + 5);
                        str = str.ToUpper().Trim();
                        MovingLoad.FileName = str;
                    } 
                    try
                    {
                        MovingLoad.Add(CMovingLoad.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N012")
                {
                    string[] values = str.Split(new char[] { ' ' });
                    if (values.Length == 4)
                    {
                        try
                        {
                            LoadGeneration.repeatTime = int.Parse(values[3]);
                        }
                        catch (Exception ex) { }
                    }
                    try
                    {
                        LoadGeneration.Add(CLoadGeneration.Parse(str));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (find == "N013")
                {
                    try
                    {
                        LoadCombination.Add(CLoadCombination.Parse(str));
                    }
                    catch (Exception ex) { }
                }
                else if (find == "N014")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        AreaLoad.MassFactor = d[0];
                        AreaLoad.LengthFactor = d[1];

                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        AreaLoad.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        AreaLoad.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        AreaLoad.Add(CAreaLoad.Parse(str));
                        
                    }
                    catch (Exception ex) { }
                }
                else if (find == "N019")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        MatPropertyInfo.MassFactor = d[0];
                        MatPropertyInfo.LengthFactor = d[1];
                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        MatPropertyInfo.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        MatPropertyInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        MatPropertyInfo.Add(CMaterialPropertyInformation.Parse(str));
                    }
                    catch (Exception ex) { }
                }

                else if (find == "N016")
                {
                    if (str.Contains("UNIT"))
                    {
                        double[] d = CAstraUnits.GetLengthMassUnits(str);
                        ElementData.MassFactor = d[0];
                        ElementData.LengthFactor = d[1];
                        string[] unt = CAstraUnits.GetUnits(str);
                        //BasicInfo.MassUnit = CAstraUnits.GetMassUnit(unt[0]);
                        //BasicInfo.LengthUnit = CAstraUnits.GetLengthUnit(unt[1]);
                        ElementData.MassUnit = CAstraUnits.GetMassUnit(unt[2]);
                        ElementData.LengthUnit = CAstraUnits.GetLengthUnit(unt[3]);
                    }
                    try
                    {
                        //ElementData.Add(CElementData.Parse(str));
                    }
                    catch (Exception ex) { }
                }


                else if (find == "N020")
                {
                    try
                    {
                        ElementMultiplier.Add(CElementMultiplier.Parse(str));
                    }
                    catch (Exception ex) { }
                }
                else if (find == "N018")
                {
                    try
                    {
                        ElementData.Add(CElementData.Parse(str));
                    }
                    catch (Exception ex) { }
                }
                if (Analysis.NDYN == 2)
                {
                    if (find == "N101")
                    {
                        try
                        {
                            TimeHistory.THist_1 = CTimeHistory1.Parse(str);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                    else if (find == "N102")
                    {
                        try
                        {
                            TimeHistory.THist_2 = CTimeHistory2.Parse(str);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                    else if (find == "N103")
                    {
                        try
                        {
                            TimeHistory.THist_3 = CTimeHistory3.Parse(str);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                    else if (find == "N104")
                    {
                        try
                        {
                            TimeHistory.THist_4 = CTimeHistory4.Parse(str);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                    else if (find == "N105")
                    {
                        try
                        {
                            if (TimeHistory.THist_5.Count > 0) TimeHistory.THist_5.NodalConstraint = true;
                            TimeHistory.THist_5.Add(CTimeHistory5.Parse(str));
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                    else if (find == "N106")
                    {
                        try
                        {
                            TimeHistory.THist_6.Add(CTimeHistory6.Parse(str));
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                }
                else if (Analysis.NDYN == 3)
                {
                    Response.ReadFromStream(sr);
                }
                    
            }
            kFs.Close();
            sr.Close();
            return true; 
        }

        public bool Write(string FilePath)
        {
            #region Write to file
            SetFact();
            string strc = "";

            //To Do: write to file
            this.FileName = FilePath;
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine(ProjectTitle);
            sw.WriteLine("ASTRA " + BasicInfo.Type.ToString() +  "\t" + BasicInfo.UsersTitle);

            strc = "STRUCTURE\t" + (int)BasicInfo.Type + "\t" + BasicInfo.RunningOption.ToString();

            //strc = "STRUCTURE\t" + (StructureType == StructureType.Space ? 1 : StructureType == StructureType.Floor ? 2 : 3);
            //strc = strc + '\t' + Modex.ToString();

            sw.WriteLine(strc);
            sw.WriteLine("N000 " + BasicInfo.MassUnit.ToString() + " " + BasicInfo.LengthUnit.ToString());
            if (this.NodeData.Count > 0)
            {
                sw.WriteLine("N001 UNIT {0:f3} {1:f3} {2} {3} {4} {5} NODE, x[NODE]*lfact, y[NODE]*lfact, z[NODE]*lfact, TX, TY, TZ, RX, RY, RZ",
                    NodeData.MassFactor, NodeData.LengthFactor, BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                    BasicInfo.MassUnit.ToString(), NodeData.LengthUnit.ToString());
                //sw.WriteLine("N001 UNIT {0:f3} {1:f3} {2} {3} {4} {5} NODE, x[NODE]*lfact, y[NODE]*lfact, z[NODE]*lfact, TX, TY, TZ, RX, RY, RZ",
                //                   NodeData.MassFactor, NodeData.LenthFactor, this.MUnit, this.LUnit,
                //                   CAstraUnits.GetMassUnit(NodeData.MassFactor), CAstraUnits.GetLengthUnit(NodeData.LenthFactor));


                this.NodeData.Sort();
                for (i = 0; i < NodeDataArr.Count; i++)
                {
                    string st = NodeDataArr[i].ToString();
                    sw.WriteLine(st);
                }
            }
            if (this.BeamConnectivity.Count > 0)
            {
                sw.WriteLine("N002 UNIT {0:f3} {1:f3} {2} {3} {4} {5} ELTYPE=2, MEMBER#, NODE1#, x[NODE1]*lfact, y[NODE1]*lfact, z[NODE1]*lfact, NODE2#, x[NODE2]*lfact, y[NODE2]*lfact, z[NODE2]*lfact",
                    BeamConnectivity.MassFactor, BeamConnectivity.LengthFactor,
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString());

                for (i = 0; i < BeamConnectivity.Count; i++)
                {
                    sw.WriteLine(BeamConnectivity[i].ToString());
                }
            }
            if (this.SectionProperty.Count > 0)
            {
                sw.WriteLine("N003 UNIT {0:f3} {1:f3} {2} {3} {4} {5} member#, section_ID#,B, D, Do, Di, area*lfact*lfact, ix*lfact*lfact*lfact*lfact, iy*lfact*lfact*lfact*lfact, iy*lfact*lfact*lfact*lfact",
                    SectionProperty.MassFactor, SectionProperty.LengthFactor, 
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                SectionProperty.MassUnit.ToString(), SectionProperty.LengthUnit.ToString());
                for (i = 0; i < SecPropArr.Count; i++)
                {
                    sw.WriteLine(SecPropArr[i].ToString());
                }
            }
            if (MatPropArr.Count > 0)
            {
                sw.WriteLine("N004 UNIT {0:f3} {1:f3} {2} {3} {4} {5} member#, mat_ID#, emod*wfact/(lfact*lfact), pr, mden*wfact/(lfact*lfact*lfact), wden*wfact/(lfact*lfact*lfact), alpha, beta",
                    MaterialProperty.MassFactor, MaterialProperty.LengthFactor,
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                MaterialProperty.MassUnit.ToString(), MaterialProperty.LengthUnit.ToString());

                for (i = 0; i < MatPropArr.Count; i++)
                {
                    sw.WriteLine(MatPropArr[i].ToString());
                }
            }
            if (this.MemberTruss.Count > 0)
            {
                sw.WriteLine("N008 ELTYPE=1, TRUSS_MEMB #");
                for (i = 0; i < MemberTruss.Count; i++)
                {
                    sw.WriteLine(MemberTruss[i].ToString());
                }
            }
            if (this.BeamConnectivityRelease.Count > 0)
            {
                sw.WriteLine("N009 member#, N1# FX FY FZ MX MY MZ N2# FX FY FZ MX MY MZ");
                for (i = 0; i < BeamConnectivityRelease.Count; i++)
                {
                    sw.WriteLine(BeamConnectivityRelease[i].ToString());
                }
            }
            if (this.Support.Count > 0)
            {
                sw.WriteLine("N005 member#, dof[0], dof[1], dof[2], dof[3], dof[4], dof[5]");
                for (i = 0; i < Support.Count; i++)
                {
                    sw.WriteLine(Support[i].ToString());
                }
            }

            if (!(SelfWeight.SELFWEIGHTX == -1 && SelfWeight.SELFWEIGHTY == -1 && SelfWeight.SELFWEIGHTZ == -1))
            {
                sw.WriteLine("N010 loadcase#, SELFWEIGHTX, SELFWEIGHTY, SELFWEIGHTZ");
                sw.WriteLine(SelfWeight.ToString());
            }
            if (JointNodalLoad.Count > 0)
            {

                sw.WriteLine("N006 UNIT {0:f3} {1:f3} {2} {3} {4} {5} Member#, loadcase#, udlx*wfact/lfact,udly*wfact/lfact,udlz*wfact/lfact, px*wfact, d1*lfact, py*wfact, d2*lfact, pz*wfact, d3*lfact",
                    JointNodalLoad.MassFactor, JointNodalLoad.LengthFactor,
                   BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                JointNodalLoad.MassUnit.ToString(), JointNodalLoad.LengthUnit.ToString());

                int xx = JointNodalLoad[0].loadcase;

                for (i = 0; i < JointNodalLoad.Count; i++)
                {
                    if (xx != JointNodalLoad[i].loadcase)
                    {
                        sw.WriteLine("N006 UNIT {0:f3} {1:f3} {2} {3} {4} {5} Member#, loadcase#, udlx*wfact/lfact,udly*wfact/lfact,udlz*wfact/lfact, px*wfact, d1*lfact, py*wfact, d2*lfact, pz*wfact, d3*lfact",
                            JointNodalLoad.MassFactor, JointNodalLoad.LengthFactor,
                            BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                JointNodalLoad.MassUnit.ToString(), JointNodalLoad.LengthUnit.ToString());


                        xx = JointNodalLoad[i].loadcase;
                    }
                    sw.WriteLine(JointNodalLoad[i].ToString());
                }
            }
            if (this.MemberBeamLoad.Count > 0)
            {
                sw.WriteLine("N007 UNIT {0:f3} {1:f3} {2} {3} {4} {5} NODE#, loadcase#, fx*wfact, fy*wfact, fz*wfact, mx*wfact*lfact, my*wfact*lfact, mz*wfact*lfact",
                    MemberBeamLoad.MassFactor, MemberBeamLoad.LengthFactor,
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                MemberBeamLoad.MassUnit.ToString(), MemberBeamLoad.LengthUnit.ToString());

                for (i = 0; i < MemberBeamLoad.Count; i++)
                {
                    sw.WriteLine(MemberBeamLoad[i].ToString());
                }
            }
            

            if (MovingLoad.Count > 0)
            {
                sw.WriteLine("N011\tDEFINE MOVING LOAD");
                sw.WriteLine("N011\tDEFINE MOVING LOAD FILE " + MovingLoad.FileName);
                for (i = 0; i < MovingLoad.Count; i++)
                {
                    sw.WriteLine(MovingLoad[i].ToString());
                }
            }

            if (LoadGeneration.Count > 0)
            {
                sw.WriteLine("N012\tLOAD GENERATION");
                sw.WriteLine("N012\tLOAD GENERATION " + LoadGeneration.repeatTime);
                for (i = 0; i < LoadGeneration.Count; i++)
                {
                    sw.WriteLine(LoadGeneration[i].ToString());
                }
            }
            if (LoadCombination.Count > 0)
            {
                sw.WriteLine("N013\tLOAD CASE (COMBINATION), Load, Factor");
                for (i = 0; i < LoadCombination.Count; i++)
                {
                    sw.WriteLine(LoadCombination[i].ToString());
                }
            }
            if (AreaLoad.Count > 0)
            {
                sw.WriteLine("N014 UNIT {0:f3} {1:f3} {2} {3} {4} {5} MEMB#, loadcase#, fy*wfact",
                    AreaLoad.MassFactor, AreaLoad.LengthFactor,
                     BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                AreaLoad.MassUnit.ToString(), AreaLoad.LengthUnit.ToString());

                for (i = 0; i < AreaLoad.Count; i++)
                {
                    sw.WriteLine(AreaLoad[i].ToString());
                }
            }

            if (this.ElementData.Count > 0)
            {
                sw.WriteLine("N016 UNIT {0:f3} {1:f3} {2} {3} {4} {5} ELTYPE=6, Element#, THICKNESS",
                    this.ElementData.MassFactor, this.ElementData.LengthFactor,
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                ElementData.MassUnit.ToString(), ElementData.LengthUnit.ToString());

                for (i = 0; i < this.ElementData.Count; i++)
                {
                    sw.WriteLine(this.ElementData[i].ElementThickNess());
                }
                sw.WriteLine("N017      ELEMENT     LOADCASE     NORMAL Pressure");
                for (i = 0; i < this.ElementData.Count; i++)
                {
                    sw.WriteLine(this.ElementData[i].ElementPressure());
                }

                sw.WriteLine("N018  ELEMENT NUMBER    NODE-I     NODE-J   NODE-K      NODE-L      NODE-0      MATERIAL NUMBER     AVERAGE THICKNESS   NORMAL Pressure      TEMPARATURE DIFFERENCE      THERMAL GRADIENT");
                for (i = 0; i < this.ElementData.Count; i++)
                {
                    sw.WriteLine(this.ElementData[i].ToString());
                }
            }
            if (MatPropertyInfo.Count > 0)
            {
                sw.WriteLine("N019 UNIT {0:f3} {1:f3} {2} {3} {4} {5} MATERIAL NUMBER   MASS DENSITY    THERMAL ALPHA(X)    EXPENSION ALPHA(Y) COEFFICIENTS ALPHA(Z)    C(XX)   C(XY)   C(XG) C(YY) C(YG) G(XY)",
                    this.MatPropertyInfo.MassFactor, this.MatPropertyInfo.LengthFactor,
                    BasicInfo.MassUnit.ToString(), BasicInfo.LengthUnit.ToString(),
                MatPropertyInfo.MassUnit.ToString(), MatPropertyInfo.LengthUnit.ToString());

                for (i = 0; i < this.MatPropertyInfo.Count; i++)
                {
                    sw.WriteLine(this.MatPropertyInfo[i].ToString());
                }
            }
            if (ElementMultiplier.Count > 0)
            {
                sw.WriteLine("N020  ELEMENT LOAD CASE NUMBER    Pressure     THERMAL EFFECTS     X-ACCELERATION      Y-ACCELERATION      Z-ACCELERATION");
                for (i = 0; i < this.ElementMultiplier.Count; i++)
                {
                    sw.WriteLine(this.ElementMultiplier[i].ToString());
                }
            }

            #endregion

            if (Analysis.NF != -1)
            {
                sw.WriteLine("N099 NDYN NF");
                //sw.WriteLine("N099\tNDYN    <NDYN_ID#>    NF    <NF_value>");
                sw.WriteLine(Analysis.ToString());
            }

            if (Analysis.NDYN == 2)
            {
                if (TimeHistory.IsDefault == false)
                {
                    sw.WriteLine("N101    1");
                    sw.WriteLine(TimeHistory.THist_1.ToString());
                    sw.WriteLine("N101");

                    sw.WriteLine(TimeHistory.THist_2.ToString());
                    sw.WriteLine("N102");

                    sw.WriteLine(TimeHistory.THist_3.ToString());
                    sw.WriteLine(TimeHistory.THist_4.ToString());

                    if (TimeHistory.THist_5.Count > 0)
                    {
                        sw.WriteLine("N105    1");
                        for (int i = 0; i < TimeHistory.THist_5.Count; i++)
                        {
                            sw.WriteLine(TimeHistory.THist_5[i].ToString());
                        }
                        sw.WriteLine("N105");
                    }
                    if (TimeHistory.THist_6.Count > 0)
                    {
                        sw.WriteLine("N106    1");
                        for (int i = 0; i < TimeHistory.THist_6.Count; i++)
                        {
                            sw.WriteLine(TimeHistory.THist_6[i].ToString());
                        }
                        sw.WriteLine("N106    0");
                        sw.WriteLine("N106");
                    }
                }
            }
            else if (Analysis.NDYN == 3)
            {
                response.WriteToStream(sw);
            }
            sw.Flush(); 
            sw.Close();
            run();
            return true;
        }
        public CMemberConnectivityCollection BeamConnectivity
        {
            get
            {
                return this.BeamConnArr;
            }
            set
            {
                this.BeamConnArr = value;
            }
        }

        public CSectionPropertyCollection SectionProperty
        {
            get
            {
                return this.SecPropArr;
            }
            set
            {
                this.SecPropArr = value;
            }
        }

        public CMaterialPropertyCollection MaterialProperty
        {
            get
            {
                return this.MatPropArr;
            }
            set
            {
                this.MatPropArr = value;
            }
        }

        public CJointNodalLoadCollection JointNodalLoad
        {
            get
            {
                return JointNodalLoadArr;
            }
            set
            {
                this.JointNodalLoadArr = value;
            }
        }

        public CMemberBeamLoadingCollection MemberBeamLoad
        {
            get
            {
                return this.MebrBmLoadArr;
            }
            set
            {
                this.MebrBmLoadArr = value;
            }
        }
        public CMemberTrussCollection MemberTruss
        {
            get
            {
                return this.MemberTrussArr;
            }
            set
            {
                this.MemberTrussArr = value;
            }
        }
        public CSelfWeight SelfWeight
        {
            get
            {
                return SlfWgt;
            }
            set
            {
                this.SlfWgt = value;
            }
        }
        public CAnalysis Analysis
        {
            get
            {
                return this.ana;
            }
            set
            {
                this.ana = value;
            }
        }
        public CSupportCollection Support
        {
            get
            {
                return this.SupportArr;
            }
            set
            {
                this.SupportArr = value;
            }
        }
        public CMovingLoadCollection MovingLoad
        {
            get
            {
                return this.MovingLoadArr;
            }
            set
            {
                this.MovingLoadArr = value;
            }
        }
        public CLoadGenerationCollection LoadGeneration
        {
            get
            {
                return this.LoadGenerationArr;
            }
            set
            {
                this.LoadGenerationArr = value;
            }
        }

        public void ClearVars()
        {
            BasicInfo.Clear();
            strTitle = "";
            StructType = StructureType.SPACE;
            lfact = 0.0;
            lunit_flag = 0;
            wfact = 0.0;
            wunit_flag = 0;
            LUnit = "";
            MUnit = "";
            NodeDataArr.Clear();
            BeamConnArr.Clear();
            SecPropArr.Clear();
            MatPropArr.Clear();
            JointNodalLoadArr.Clear();
            BeamConnectivityReleaseArr.Clear();
            MebrBmLoadArr.Clear();
            MemberTrussArr.Clear();
            SupportArr.Clear();
            MovingLoadArr.Clear();
            LoadGenerationArr.Clear();
            LoadCombArr.Clear();
            MatPropInfoArr.Clear();
            ElementDataArr.Clear();
            AreaLoadArr.Clear();
            ElementMultiplierArr.Clear();
            Analysis.NF = 0;
            //Analysis.NF = -1;
            Analysis.AnalysisType = "NDYN";
            Analysis.NDYN = 0;
            //Analysis.NDYN_ID = 0 + " NF";
            SelfWeight.SELFWEIGHTX = -1;
            SelfWeight.SELFWEIGHTY = -1;
            SelfWeight.SELFWEIGHTZ = -1;
            SelfWeight.loadcase = 0;
            TimeHistory.IsDefault = true;
            TimeHistory.THist_5.Clear();
            TimeHistory.THist_6.Clear();
            Response.Clear();
            
        }

        public CBeamConnectivityReleaseCollection BeamConnectivityRelease
        {
            get
            {
                return this.BeamConnectivityReleaseArr;
            }
            set
            {
                this.BeamConnectivityReleaseArr = value;
            }
        }
        private void run()
        {
            ThreadStart thdStart = new ThreadStart(start);
            thd = new Thread(thdStart);
            thd.Start();
        }
        public void start()
        {
            Write_Extra(FileName);
        }

        private void Write_Extra(string FileName)
        {
            FileInfo flInfo = new FileInfo(FileName);
            string fl = "N001.FIL";
            fl= Path.Combine(flInfo.DirectoryName, fl);
            FileStream fs = new FileStream(fl, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (i = 0; i < NodeData.Count; i++)
            {
                sw.WriteLine("{0} {1:f3} {2:f3} {3:f3}", NodeData[i].NodeNo, NodeData[i].X, NodeData[i].Y, NodeData[i].Z);
            }
            sw.Flush();
            sw.Close();
            
            fl = "N002.FIL";
            fl = Path.Combine(flInfo.DirectoryName, fl);
            fs = new FileStream(fl, FileMode.Create);
            sw = new StreamWriter(fs);
            for (i = 0; i < BeamConnectivity.Count; i++)
            {

                sw.WriteLine("{0} {1} {2} {3:f3} {4:f3} {5:f3} {6} {7:f3} {8:f3} {9:f3}", 2, BeamConnectivity[i].memberNo,
                    BeamConnectivity[i].Node1, BeamConnectivity[i].X1, BeamConnectivity[i].Y1, BeamConnectivity[i].Z1, 
                    BeamConnectivity[i].Node2, BeamConnectivity[i].X2, BeamConnectivity[i].Y2, BeamConnectivity[i].Z2);
            }
            sw.Flush();
            sw.Close();
            thd.Abort();
        }
        public string getElement(int elementNo, int loadCase)
        {
            int node1, node2;
            node1 = 0;
            node2 = 0;

            for (i = 0; i < BeamConnectivity.Count; i++)
            {
                if (BeamConnectivity[i].memberNo == elementNo)
                {
                    node1 = BeamConnectivity[i].Node1;
                    node2 = BeamConnectivity[i].Node2;
                    break;
                }
            }

            return ("" + elementNo + '\t' + loadCase + '\t' + getNode(node1) + '\t' + getNode(node2) + "");
        }
        public string getNode(int NodeNo)
        {
            string kStr = "";
            for (i = 0; i < MemberBeamLoad.Count; i++)
            {
                if (MemberBeamLoad[i].Node == NodeNo)
                {
                    kStr = NodeNo.ToString() + '\t' + MemberBeamLoad[i].fx + '\t' + MemberBeamLoad[i].fy + '\t' + 
                        MemberBeamLoad[i].fz+ '\t' + MemberBeamLoad[i].mx+ '\t' + MemberBeamLoad[i].my+ '\t' + 
                        MemberBeamLoad[i].mz;
                    return kStr;
                }
            }
            return kStr;
        }

        public CLoadCombinationCollection LoadCombination
        {
            get
            {
                return this.LoadCombArr;
            }
            set
            {
                this.LoadCombArr = value;
            }
        }

        public CAreaLoadCollection AreaLoad
        {
            get
            {
                return this.AreaLoadArr;
            }
            set
            {
                this.AreaLoadArr = value;
            }
        }


        public CMaterialPropertyInformationCollection MatPropertyInfo
        {
            get
            {
                return this.MatPropInfoArr;
            }
            set
            {
                this.MatPropInfoArr = value;
            }
        }

        public CElementDataCollection ElementData
        {
            get
            {
                return this.ElementDataArr;
            }
            set
            {
                this.ElementDataArr = value;
            }
        }

        public CElementMultiplierCollection ElementMultiplier
        {
            get
            {
                return this.ElementMultiplierArr;
            }
            set
            {
                this.ElementMultiplierArr = value;
            }
        }

        public double LFact
        {
            get
            {
                return this.lfact;
            }
            set
            {
                this.lfact = value;
            }
        }

        public double WFact
        {
            get
            {
                return this.wfact;
            }
            set
            {
                this.wfact = value;
            }
        }

        public short wunit_flag
        {
            get
            {
                return this.wfct_flag;
            }
            set
            {
                this.wfct_flag = value;
            }
        }

        public short lunit_flag
        {
            get
            {
                return this.lfct_flag;
            }
            set
            {
                this.lfct_flag = value;
            }
        }

        public short Modex
        {
            get
            {
                return modex;
            }
            set
            {
                modex = value;
            }
        }

        public string LUnit
        {
            get
            {
                return lunit;
            }
            set
            {
                lunit = value;
            }
        }

        public string MUnit
        {
            get
            {
                return munit;
            }
            set
            {
                munit = value;
            }
        }

        #endregion

        #region IDocument Members


        public CTimeHistoryAnalysis TimeHistory
        {
            get
            {
                return timeHist;
            }
            set
            {
                timeHist = value;
            }
        }

        #endregion

        #region IDocument Members


        public CResponse Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
            }
        }

        #endregion




        #region IDocument Members


        public string FilePath
        {
            get
            {
                return FileName;
            }
            set
            {
                if (File.Exists(value))
                {
                    FileName = value;
                }
                else
                {
                    FileName = "";
                }
            }
        }

        #endregion
    }
}
