using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AstraInterface.Interface;

namespace AstraInterface.DataStructure
{
    public enum StructureType
    {
        SPACE = 1,
        FLOOR = 2,
        PLANE = 3,
    }
    public class CNodeData
    {
        public CNodeData()
        {

        }             
        public int NodeNo;
        
        public double X;
        public double Y;
        public double Z;

        public bool TX;
        public bool TY;
        public bool TZ;

        public bool RX;
        public bool RY;
        public bool RZ;

        public override string ToString()
        {
            const char t = '\t';
            string str ="N001" + t + this.NodeNo + t + this.X + t + this.Y + t + this.Z + t + (this.TX ? 1 : 0) + t +
                (this.TY ? 1 : 0) + t + (this.TZ ? 1 : 0) + t + (this.RX ? 1 : 0) + t + (this.RY ? 1:0) + t + (this.RZ ? 1:0) + "";



            str = string.Format("{0,-5} {1,5} {2,10} {3,10} {4,10} {5,5} {6,5} {7,5} {8,5} {9,5} {10,5}",
                        "N001", this.NodeNo, this.X.ToString("0.000"), this.Y.ToString("0.000"), this.Z.ToString("0.000"), (this.TX ? 1 : 0), (this.TY ? 1 : 0), 
                        (this.TZ ? 1 : 0), (this.RX ? 1 : 0), (this.RY ? 1 : 0), (this.RZ ? 1 : 0));

            return str;


        }
        public string AsText
        {
            get
            {
                return string.Format("{0,-7} {1,10:f3} {2,10:f3} {3,10:f3}", NodeNo, X, Y, Z);
            }
        }
        public static CNodeData Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            short fl = 0;
            CNodeData dataObj = new CNodeData();
            string[] values = strData.Split(new char[] { ' ' });

            if (values.Length != 11)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.NodeNo = int.Parse(values[iCount]); iCount++;
            dataObj.X = double.Parse(values[iCount]); iCount++;
            dataObj.Y = double.Parse(values[iCount]); iCount++;
            dataObj.Z = double.Parse(values[iCount]); iCount++;

            dataObj.TX = (values[iCount] == "0" ? false : true); iCount++;
            dataObj.TY = (values[iCount] == "0" ? false : true); iCount++;
            dataObj.TZ = (values[iCount] == "0" ? false : true); iCount++;

            dataObj.RX = (values[iCount] == "0" ? false : true); iCount++;
            dataObj.RY = (values[iCount] == "0" ? false : true); iCount++;
            dataObj.RZ = (values[iCount] == "0" ? false : true); iCount++;
            return dataObj;

        }
        public static bool operator ==(CNodeData lhs, CNodeData rhs)
        {
            if (lhs.NodeNo == rhs.NodeNo && lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.TX == rhs.TX && lhs.TY == rhs.TY && lhs.TZ == rhs.TZ && lhs.RX == rhs.RX && lhs.RY == rhs.RY && lhs.RZ == rhs.RZ)
                return true;
            else
                return false;
        }
        public static bool operator !=(CNodeData lhs, CNodeData rhs)
        {
            return !(lhs == rhs);
        }
        
    }
    public class CNodeDataCollection
    {
        string NodeName = "N001";//This should be always N001
        public double MassFactor = 1.0;
        public double LengthFactor = 1.0;
        public eLengthUnits LengthUnit;
        public EMassUnits MassUnit;
        List<CNodeData> list;
        public CNodeDataCollection()
        {
            list = new List<CNodeData>();
            MassUnit = EMassUnits.KG;
            LengthUnit = eLengthUnits.METRES;
        }

        public void Add(CNodeData data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CNodeData data)
        {
            list.Insert(Index, data);
        }
        public CNodeData this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public int IndexOf(CNodeData cnd)
        {
            int i = -1;
            foreach (CNodeData ccnd in list)
            {
                i++;
                if (ccnd.NodeNo == cnd.NodeNo)
                    return i;
            }
            return -1;
        }
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CNodeDataCollection FromStream(StreamReader sr)
        {
            CNodeDataCollection data = new CNodeDataCollection();
            //To Do Parse logic
            return data;
        }
        public void Sort()
        {
            CNodeDataCollection ndc = new CNodeDataCollection();

            List<int> nc = new List<int>();
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                nc.Add(this[i].NodeNo);
            }
            nc.Sort();
            for (i = 0; i < nc.Count; i++)
            {
                ndc.Add(getCNodeData(nc[i]));
            }
            
            for (i = 0; i < this.Count; i++)
            {
                this[i] = ndc[i];
            }

        }
        public CNodeData getCNodeData(int NodeNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].NodeNo == NodeNo)
                {
                    return this[i];
                }
            }
            return null;
        }
    }
    public class CMemberConnectivity
    {
        public CMemberConnectivity()
        {
            memberNo = 0;
            Node1 = Node2 = 0;
            X1 = Y1 = Z1 = X2 = Y2 = Z2 = 0;
        }
        short elType = 2;
        public int memberNo;
        public int Node1,Node2;
        public double X1, Y1, Z1, X2, Y2, Z2;

        public override string ToString()
        {
            string str = "N002" + '\t' + this.elType + '\t' + this.memberNo + '\t' + this.Node1 + '\t' + this.X1 + '\t' +
                            this.Y1 + '\t' + this.Z1 + '\t' + this.Node2 + '\t' + this.X2 + '\t' + this.Y2 + '\t' +
                            this.Z2;


            //str = string.Format("{0,-5} {1,5} {2,10} {3,10} {4,10} {5,5} {6,5} {7,5} {8,5} {9,5} {10,5}",
            //           "N002", this.elType, this.memberNo, this.Y.ToString("0.000"), this.Z.ToString("0.000"), (this.TX ? 1 : 0), (this.TY ? 1 : 0),
            //           (this.TZ ? 1 : 0), (this.RX ? 1 : 0), (this.RY ? 1 : 0), (this.RZ ? 1 : 0));

           

            return str;
        }
        /*
        public static CBeamConnectivity Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            short fl = 0;
            CBeamConnectivity dataObj = new CBeamConnectivity();
            string[] values = strData.Split(new char[] { '\t' });

            if (values.Length != 11)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 2;

            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.Node1 = int.Parse(values[iCount]); iCount++;
            dataObj.X1 = double.Parse(values[iCount]); iCount++;
            dataObj.Y1 = double.Parse(values[iCount]); iCount++;
            dataObj.Z1 = double.Parse(values[iCount]); iCount++;

            dataObj.Node2 = int.Parse(values[iCount]); iCount++;
            dataObj.X2 = double.Parse(values[iCount]); iCount++;
            dataObj.Y2 = double.Parse(values[iCount]); iCount++;
            dataObj.Z2 = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
        public static CMemberConnectivity Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            short fl = 0;
            CMemberConnectivity dataObj = new CMemberConnectivity();
            string[] values = strData.Split(new char[] { ' ' });

            if (values.Length != 11)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 2;

            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.Node1 = int.Parse(values[iCount]); iCount++;
            dataObj.X1 = double.Parse(values[iCount]); iCount++;
            dataObj.Y1 = double.Parse(values[iCount]); iCount++;
            dataObj.Z1 = double.Parse(values[iCount]); iCount++;

            dataObj.Node2 = int.Parse(values[iCount]); iCount++;
            dataObj.X2 = double.Parse(values[iCount]); iCount++;
            dataObj.Y2 = double.Parse(values[iCount]); iCount++;
            dataObj.Z2 = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        public static bool operator ==(CMemberConnectivity lhs, CMemberConnectivity rhs)
        {
            if (lhs.memberNo == rhs.memberNo && lhs.Node1 == rhs.Node1 && lhs.X1 == rhs.X1 && lhs.Y1 == rhs.Y1 && lhs.Z1 == rhs.Z1 && lhs.Node2 == rhs.Node2 && lhs.X2 == rhs.X2 && lhs.Y2 == rhs.Y2 && lhs.Z2 == rhs.Z2)
                return true;
            else
                return false;
        }
        public static bool operator !=(CMemberConnectivity lhs, CMemberConnectivity rhs)
        {
            return !(lhs == rhs);
        }

       
    }
    public class CMemberConnectivityCollection
    {
        public CMemberConnectivityCollection() { }
        string NodeName = "N002";//This should be always N002
        public double MassFactor = 1.0;
        public double LengthFactor = 1.0;
        int i = 0;
        List<CMemberConnectivity> list = new List<CMemberConnectivity>();

        public void Add(CMemberConnectivity data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CMemberConnectivity data)
        {
            list.Insert(Index, data);
        }
        public CMemberConnectivity this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CMemberConnectivityCollection FromStream(StreamReader sr)
        {
            CMemberConnectivityCollection data = new CMemberConnectivityCollection();
            //To Do Parse logic
            return data;
        }
        public bool deleteElement(string elementNo)
        {
            return deleteElement(CTextConvert.getInt(elementNo));            
        }
        public bool deleteElement(int elementNo)
        {
            for (i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    this.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public CMemberConnectivity getCBeamConnectivity(string elementNo)
        {
            return getCBeamConnectivity(CTextConvert.getInt(elementNo));
        }
        public CMemberConnectivity getCBeamConnectivity(int elementNo)
        {
            for (i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return this[i];
                }
            }
            return null;
        }
        public bool isPresentElement(int elementNo)
        {
            for (i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isPresentElement(string elementNo)
        {
            return isPresentElement(CTextConvert.getInt(elementNo));
        }
        public void Sort()
        {
            int i = 0;
            List<int> nc = new List<int>();
            CMemberConnectivityCollection bcc = new CMemberConnectivityCollection();
            for (i = 0; i < this.Count; i++)
            {
                nc.Add(this[i].memberNo);
            }
            nc.Sort();
            for (i = 0; i < nc.Count; i++)
            {
                bcc.Add(this.getCBeamConnectivity(nc[i]));
            }
            for (i = 0; i < bcc.Count; i++)
            {
                this[i] = bcc[i];
            }
            bcc.Clear();
        }
        public int IndexOf(int elementNo)
        {
            for (i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(string elementNo)
        {
            return IndexOf(CTextConvert.getInt(elementNo));
        }
    }
    public class CSectionProperty
    {
        public CSectionProperty() { }
        public int memberNo, sectionId;
        public double B, D, outerD,innerD,area, ix, iy, iz;
        /*
        public static CSectionProperty Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CSectionProperty dataObj = new CSectionProperty();
            string[] values = strData.Split(new char[] { '\t' });

            if (values.Length != 11)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;

            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.sectionId = int.Parse(values[iCount]); iCount++;

            dataObj.B = double.Parse(values[iCount]); iCount++;
            dataObj.D = double.Parse(values[iCount]); iCount++;

            dataObj.outerD = double.Parse(values[iCount]); iCount++;
            dataObj.innerD = double.Parse(values[iCount]); iCount++;
            dataObj.area = double.Parse(values[iCount]); iCount++;
            dataObj.ix = double.Parse(values[iCount]); iCount++;
            dataObj.iy = double.Parse(values[iCount]); iCount++;
            dataObj.iz = double.Parse(values[iCount]); iCount++;

            return dataObj;

        }
        */
        public static CSectionProperty Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CSectionProperty dataObj = new CSectionProperty();
            string[] values = strData.Split(new char[] { ' ' });

            if (values.Length != 11)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;

            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.sectionId = int.Parse(values[iCount]); iCount++;

            dataObj.B = double.Parse(values[iCount]); iCount++;
            dataObj.D = double.Parse(values[iCount]); iCount++;
            
            dataObj.outerD = double.Parse(values[iCount]); iCount++;
            dataObj.innerD = double.Parse(values[iCount]); iCount++;
            dataObj.area = double.Parse(values[iCount]); iCount++;
            dataObj.ix = double.Parse(values[iCount]); iCount++;
            dataObj.iy = double.Parse(values[iCount]); iCount++;
            dataObj.iz = double.Parse(values[iCount]); iCount++;

            return dataObj;

        }
        public static bool operator ==(CSectionProperty lhs, CSectionProperty rhs)
        {
            if(lhs.sectionId == rhs.sectionId && lhs.memberNo == rhs.memberNo && lhs.area == rhs.area && lhs.B == rhs.B && lhs.D == rhs.D && lhs.innerD == rhs.innerD && lhs.ix == rhs.ix && lhs.iy == rhs.iy && lhs.iz == rhs.iz && lhs.outerD == rhs.outerD)
                return true;
            else
                return false;
        }
        public static bool operator !=(CSectionProperty lhs, CSectionProperty rhs)
        {
            return !(lhs == rhs);
        }
        public override string ToString()
        {
            string str = "N003" + '\t' + this.memberNo + '\t' + this.sectionId + '\t' + this.B + '\t' + this.D + '\t' +
                this.outerD + '\t' + this.innerD + '\t' + this.area + '\t' + this.ix + '\t' + this.iy + '\t' +
                this.iz;
            return str;
        }

    }
    public class CSectionPropertyCollection
    {
        public double MassFactor = 1;
        public double LengthFactor = 1;
        public eLengthUnits LengthUnit;
        public EMassUnits MassUnit;
        List<CSectionProperty> list;

        public CSectionPropertyCollection()
        {
            list = new List<CSectionProperty>();
            LengthUnit = eLengthUnits.METRES;
            MassUnit = EMassUnits.KG;
        }
        
        public void Add(CSectionProperty data)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].sectionId == data.sectionId) && (list[i].memberNo == data.memberNo))
                {
                    list.RemoveAt(i);
                }
            }
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CSectionProperty data)
        {
            list.Insert(Index, data);
        }
        public CSectionProperty this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CSectionPropertyCollection FromStream(StreamReader sr)
        {
            CSectionPropertyCollection data = new CSectionPropertyCollection();
            //To Do Parse logic
            return data;
        }

        public int IndexOf(int elementNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return i;
                }
            }
            return -1; 
        }
        public bool DeleteElement(int elementNo)
        {
            if (this.IndexOf(elementNo) != -1)
            {
                this.RemoveAt(this.IndexOf(elementNo));
                return true;
            }
            else
                return false;
        }
        public bool DeleteSection(int sectionId)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].sectionId == sectionId)
                {
                    this.RemoveAt(i);
                    i = -1;
                    fl = true;
                }
            }
            return fl;
        }

        public CSectionProperty GetCSectionProperty(int elementNo)
        {
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return this[i];
                }
            }
            return null;
        }
    }
    public class CMaterialProperty
    {
        public CMaterialProperty()
        {
        }
        public int memberNo,matID;
        public double emod, pr, mden, wden, alpha, beta;
        public static CMaterialProperty Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMaterialProperty dataObj = new CMaterialProperty();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 9)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;

            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.matID = int.Parse(values[iCount]); iCount++;

            dataObj.emod = double.Parse(values[iCount]); iCount++;
            dataObj.pr = double.Parse(values[iCount]); iCount++;

            dataObj.mden = double.Parse(values[iCount]); iCount++;
            dataObj.wden = double.Parse(values[iCount]); iCount++;
            dataObj.alpha = double.Parse(values[iCount]); iCount++;
            dataObj.beta = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        /*
        public static CMaterialProperty Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMaterialProperty dataObj = new CMaterialProperty();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 9)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;

            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.matID = int.Parse(values[iCount]); iCount++;

            dataObj.emod = double.Parse(values[iCount]); iCount++;
            dataObj.pr = double.Parse(values[iCount]); iCount++;

            dataObj.mden = double.Parse(values[iCount]); iCount++;
            dataObj.wden = double.Parse(values[iCount]); iCount++;
            dataObj.alpha = double.Parse(values[iCount]); iCount++;
            dataObj.beta = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        */
        public static bool operator ==(CMaterialProperty lhs, CMaterialProperty rhs)
        {
            if (lhs.alpha == rhs.alpha && lhs.beta == rhs.beta && lhs.memberNo == rhs.memberNo && lhs.emod == rhs.emod && lhs.matID == rhs.matID && lhs.mden == rhs.mden && lhs.pr == rhs.pr && lhs.wden == rhs.wden)
                return true;
            else
                return false;
        }
        public static bool operator !=(CMaterialProperty lhs, CMaterialProperty rhs)
        {
            return !(lhs == rhs);
        }
        public override string ToString()
        {
            string str = "N004" + '\t' + this.memberNo + '\t' + this.matID + '\t' + this.emod + '\t' + this.pr + '\t' +
                        this.mden + '\t' + this.wden + '\t' + this.alpha.ToString("F6") + '\t' + this.beta.ToString("F6");
            return str;
        }

    }
    public class CMaterialPropertyCollection
    {
        public double MassFactor = 1;
        public double LengthFactor = 1;
        List<CMaterialProperty> list = new List<CMaterialProperty>();
        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public EMassUnits MassUnit = EMassUnits.KG;
        public CMaterialPropertyCollection()
        {
        }
       
        public void Add(CMaterialProperty data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CMaterialProperty data)
        {
            list.Insert(Index, data);
        }
        public CMaterialProperty this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CMaterialPropertyCollection FromStream(StreamReader sr)
        {
            CMaterialPropertyCollection data = new CMaterialPropertyCollection();
            //To Do Parse logic
            return data;
        }

        public int IndexOf(string memberNo)
        {
            return IndexOf(CTextConvert.getInt(memberNo));
        }
        public int IndexOf(int memberNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == memberNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool DeleteElement(int memberNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == memberNo)
                {
                    this.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public bool DeleteMaterial(int matId)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].matID == matId)
                {
                    this.RemoveAt(i);
                    i = -1;
                    fl = true;
                }
            }
            return fl;
        }

        public void Sort()
        {
            CMaterialPropertyCollection cmpc = new CMaterialPropertyCollection();
            List<int> nc = new List<int>();
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                nc.Add(this[i].memberNo);
            }
            nc.Sort();
            cmpc.Clear();
            for (i = 0; i < nc.Count; i++)
            {
                cmpc.Add(GetMatProperty(nc[i]));
            }
            for (i = 0; i < cmpc.Count; i++)
            {
                this[i] = cmpc[i];
            }
            cmpc.Clear();
        }
        public CMaterialProperty GetMatProperty(int elementNo)
        {
            if (this.IndexOf(elementNo) != -1)
            {
                return this[this.IndexOf(elementNo)];
            }
            else
                return null;
        }
    }
    public class CMemberTruss
    {
        public CMemberTruss() 
        {
            ELTYPE = 1;
        }
        public int ELTYPE, TRUSS_MEMB;

        public static CMemberTruss Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMemberTruss dataObj = new CMemberTruss();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 3)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 2;
            dataObj.TRUSS_MEMB = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        /*
        public static CMemberTruss Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMemberTruss dataObj = new CMemberTruss();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 3)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 2;
            dataObj.TRUSS_MEMB = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N008" + '\t' + this.ELTYPE + '\t' + this.TRUSS_MEMB;
            return str;
        }
        public static bool operator ==(CMemberTruss lhs, CMemberTruss rhs)
        {
            if (lhs.TRUSS_MEMB == rhs.TRUSS_MEMB)
                return true;
            else
                return false;
        }
        public static bool operator !=(CMemberTruss lhs, CMemberTruss rhs)
        {
            return !(lhs == rhs);
        }

    }
    public class CMemberTrussCollection
    {
        public CMemberTrussCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;
        List<CMemberTruss> list = new List<CMemberTruss>();

        public void Add(CMemberTruss data)
        {
            bool IsPresent = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TRUSS_MEMB == data.TRUSS_MEMB && list[i].ELTYPE == data.ELTYPE)
                {
                    list.RemoveAt(i);
                }
            }
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CMemberTruss data)
        {
            list.Insert(Index, data);
        }
        public CMemberTruss this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CMemberTrussCollection FromStream(StreamReader sr)
        {
            CMemberTrussCollection data = new CMemberTrussCollection();
            //To Do Parse logic
            return data;
        }
    }
    public class CSelfWeight
    {
        public CSelfWeight() 
        {
            SELFWEIGHTX = -1.0; SELFWEIGHTY = -1.0; SELFWEIGHTZ = -1.0;
        }
        public int loadcase;
        public double SELFWEIGHTX, SELFWEIGHTY, SELFWEIGHTZ;

        public static CSelfWeight Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CSelfWeight dataObj = new CSelfWeight();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 5)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.SELFWEIGHTX = double.Parse(values[iCount]); iCount++;
            dataObj.SELFWEIGHTY = double.Parse(values[iCount]); iCount++;
            dataObj.SELFWEIGHTZ = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        /*
        public static CSelfWeight Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CSelfWeight dataObj = new CSelfWeight();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 5)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.SELFWEIGHTX = double.Parse(values[iCount]); iCount++;
            dataObj.SELFWEIGHTY = double.Parse(values[iCount]); iCount++;
            dataObj.SELFWEIGHTZ = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N010" + '\t' + this.loadcase + '\t' + this.SELFWEIGHTX + '\t' + this.SELFWEIGHTY + '\t' +
                            this.SELFWEIGHTZ;

            return str;
        }

    }
    public class CMemberBeamLoading
    {
        public CMemberBeamLoading() { }
        public int Node, loadcase;
        public double fx, fy, fz, mx, my, mz;
        public static CMemberBeamLoading Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMemberBeamLoading dataObj = new CMemberBeamLoading();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 9)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.Node = int.Parse(values[iCount]); iCount++;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.fx = double.Parse(values[iCount]); iCount++;
            dataObj.fy = double.Parse(values[iCount]); iCount++;
            dataObj.fz = double.Parse(values[iCount]); iCount++;
            dataObj.mx = double.Parse(values[iCount]); iCount++;
            dataObj.my = double.Parse(values[iCount]); iCount++;
            dataObj.mz = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        /*
        public static CMemberBeamLoading Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMemberBeamLoading dataObj = new CMemberBeamLoading();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 9)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.Node = int.Parse(values[iCount]); iCount++;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.fx = double.Parse(values[iCount]); iCount++;
            dataObj.fy = double.Parse(values[iCount]); iCount++;
            dataObj.fz = double.Parse(values[iCount]); iCount++;
            dataObj.mx = double.Parse(values[iCount]); iCount++;
            dataObj.my = double.Parse(values[iCount]); iCount++;
            dataObj.mz = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N007" + '\t' + this.Node + '\t' + this.loadcase + '\t' + this.fx + '\t' + this.fy + '\t' +
                this.fz + '\t' + this.mx + '\t' + this.my + '\t' + this.mz;
            return str;
        }
        public static bool operator ==(CMemberBeamLoading lhs, CMemberBeamLoading rhs)
        {
            if(lhs.fx == rhs.fx && lhs.fy == rhs.fy && lhs.fz == rhs.fz && lhs.loadcase == rhs.loadcase && lhs.mx == rhs.mx && lhs.my == rhs.my && lhs.mz == rhs.mz && lhs.Node == rhs.Node)
                return true;
            else
                return false;
        }
        public static bool operator !=(CMemberBeamLoading lhs, CMemberBeamLoading rhs)
        {
            return !(lhs == rhs);
        }
    }
    public class CMemberBeamLoadingCollection
    {
        public CMemberBeamLoadingCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;

        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public EMassUnits MassUnit = EMassUnits.KG;
        List<CMemberBeamLoading> list = new List<CMemberBeamLoading>();
        
        public void Add(CMemberBeamLoading data)
        {
            list.Add(data);
            
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CMemberBeamLoading data)
        {
            list.Insert(Index, data);
        }
        public CMemberBeamLoading this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public int IndexOf(int loadcase, int nodeNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].loadcase == loadcase && this[i].Node == nodeNo)
                    return i;
            }
            return -1;
        }
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CMemberBeamLoadingCollection FromStream(StreamReader sr)
        {
            CMemberBeamLoadingCollection data = new CMemberBeamLoadingCollection();
            //To Do Parse logic
            return data;
        }
    }
    public class CJointNodalLoad
    {
        public string mark;
        public int memberNo, loadcase, loadNo;
        public double udlx, udly, udlz, px, d1, py, d2, pz, d3;

        public CJointNodalLoad()
        {
            mark = "";
            memberNo = 0;
            loadcase = 0;
            loadNo = 1;
            udlx = udly = udlz = px = d1 = py = d2 = pz = d3 = 0.0;
        }
        
        public static CJointNodalLoad Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CJointNodalLoad dataObj = new CJointNodalLoad();
            strData = strData.Replace('\t', ' ');
            strData = strData.Replace("  ", " ");
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 13)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.mark = values[iCount]; iCount++;
            //dataObj.mark = dataObj.mark.Remove(0, 4);
            dataObj.mark = dataObj.mark.Trim();
            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.udlx = double.Parse(values[iCount]); iCount++;
            dataObj.udly = double.Parse(values[iCount]); iCount++;
            dataObj.udlz = double.Parse(values[iCount]); iCount++;
            dataObj.px = double.Parse(values[iCount]); iCount++;
            dataObj.d1 = double.Parse(values[iCount]); iCount++;
            dataObj.py = double.Parse(values[iCount]); iCount++;
            dataObj.d2 = double.Parse(values[iCount]); iCount++;
            dataObj.pz = double.Parse(values[iCount]); iCount++;
            dataObj.d3 = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        /*
        public static CJointNodalLoad Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CJointNodalLoad dataObj = new CJointNodalLoad();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 12)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 0;
            dataObj.mark = values[iCount]; iCount++;
            dataObj.mark = dataObj.mark.Remove(0, 4);
            dataObj.mark = dataObj.mark.Trim();
            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.udlx = double.Parse(values[iCount]); iCount++;
            dataObj.udly = double.Parse(values[iCount]); iCount++;
            dataObj.udlz = double.Parse(values[iCount]); iCount++;
            dataObj.px = double.Parse(values[iCount]); iCount++;
            dataObj.d1 = double.Parse(values[iCount]); iCount++;
            dataObj.py = double.Parse(values[iCount]); iCount++;
            dataObj.d2 = double.Parse(values[iCount]); iCount++;
            dataObj.pz = double.Parse(values[iCount]); iCount++;
            dataObj.d3 = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
        public override string ToString()
        {
            const char t = '\t';
            string str = "N006 " + this.mark + " " + t + this.memberNo + t + this.loadcase + t + this.udlx + t + this.udly + t + this.udlz + t + this.px + t +
                this.d1 + t + this.py + t + this.d2 + t + this.pz + t + this.d3;
            return str;
        }

        public static bool operator ==(CJointNodalLoad lhs, CJointNodalLoad rhs)
        {
            //if (lhs.d1 == rhs.d1 && lhs.d2 == rhs.d2 && lhs.d3 == rhs.d3 && lhs.loadcase == rhs.loadcase && lhs.mark == rhs.mark && lhs.memberNo == rhs.memberNo && lhs.px == rhs.px && lhs.py == rhs.py && lhs.pz == rhs.pz && lhs.udlx == rhs.udlx && lhs.udly == rhs.udly && lhs.udlz == rhs.udlz)
            if (lhs.d1 == rhs.d1 && lhs.d2 == rhs.d2 && lhs.d3 == rhs.d3 && lhs.loadcase == rhs.loadcase && lhs.mark == rhs.mark && lhs.px == rhs.px && lhs.py == rhs.py && lhs.pz == rhs.pz && lhs.udlx == rhs.udlx && lhs.udly == rhs.udly && lhs.udlz == rhs.udlz)
                return true;
            else
                return false;
        }
        public static bool operator !=(CJointNodalLoad lhs, CJointNodalLoad rhs)
        {
            return !(lhs == rhs);
        }

    }
    public class CJointNodalLoadCollection
    {
        public CJointNodalLoadCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;
        List<CJointNodalLoad> list = new List<CJointNodalLoad>();
        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public EMassUnits MassUnit = EMassUnits.KG;
        public int GetMaximumLoadNo(int loadCase)
        {
            int maxLoadNo = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].loadcase == loadCase)
                {
                    if (maxLoadNo < list[i].loadNo)
                        maxLoadNo = list[i].loadNo;
                }
            }
            return maxLoadNo;
        }

        public void Add(CJointNodalLoad data)
        {
            bool bPresent = false;
            this.DeleteElement(data);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == data)
                    {
                        data.loadNo = list[i].loadNo;
                        bPresent = true;
                        break;
                    }
                }
                if (!bPresent)
                {
                    data.loadNo = (GetMaximumLoadNo(data.loadcase) + 1);
                    //data.loadNo = list[list.Count - 1].loadNo + 1;
                }
            }
            else
            {
                data.loadNo = 1;
            }
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CJointNodalLoad data)
        {
            list.Insert(Index, data);
        }
        public CJointNodalLoad this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CJointNodalLoadCollection FromStream(StreamReader sr)
        {
            CJointNodalLoadCollection data = new CJointNodalLoadCollection();
            //To Do Parse logic
            return data;
        }
        public bool DeleteElement(string elementNo,string loadNo)
        {
            return this.DeleteElement(CTextConvert.getInt(elementNo),CTextConvert.getInt(loadNo));
        }
        public bool DeleteElement(int elementNo,int loadNo)
        {
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo && this[i].loadNo == loadNo)
                {
                    this.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void DeleteAllElement(CJointNodalLoad jnd)
        {
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                if (list[i] == jnd)
                {
                    if (list[i].loadcase == jnd.loadcase && list[i].loadNo == jnd.loadNo
                        && list[i].mark == jnd.mark)
                    {
                        this.RemoveAt(i);
                        i = -1;
                    }
                }
            }
        }
        public bool DeleteElement(CJointNodalLoad jnd)
        {
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                if (list[i] == jnd)
                {
                    if (list[i].loadcase == jnd.loadcase && list[i].loadNo == jnd.loadNo
                        && list[i].mark == jnd.mark && list[i].memberNo == jnd.memberNo)
                    {
                        this.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteLoadcase(int loadcase,int loadNo)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].loadcase == loadcase && this[i].loadNo == loadNo)
                {
                    this.RemoveAt(i);
                    i = -1;
                    fl = true;
                }
            }
            return fl;
        }
        public bool IsPresentElement(string elementNo)
        {
            return this.IsPresentElement(CTextConvert.getInt(elementNo));
        }
        public bool IsPresentElement(int elementNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOf(string elementNo)
        {
            return this.IndexOf(CTextConvert.getInt(elementNo));
        }
        public int IndexOf(int elementNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public CJointNodalLoad GetCJointNodalLoad(string elementNo)
        {
            return this.GetCJointNodalLoad(CTextConvert.getInt(elementNo));
        }
        public CJointNodalLoad GetCJointNodalLoad(int elementNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == elementNo)
                {
                    return this[i];
                }
            }
            return null;
        }
        public void Sort()
        {

            
        }
    }
    public class CSupport
    {
        public CSupport() { }
        public int nodeNo;
        public bool dof0, dof1, dof2, dof3, dof4, dof5;

        public static CSupport Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CSupport dataObj = new CSupport();
            strData = strData.Replace('\t',' ');
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 8)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace("  ", " ");
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.nodeNo = int.Parse(values[iCount]); iCount++;
            dataObj.dof0 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof3 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof4 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof5 = ((values[iCount] == "1" ? true : false)); iCount++;

            return dataObj;
        }
        /*
        public static CSupport Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CSupport dataObj = new CSupport();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 8)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.element = int.Parse(values[iCount]); iCount++;
            dataObj.dof0 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof3 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof4 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.dof5 = ((values[iCount] == "1" ? true : false)); iCount++;

            return dataObj;
        }
        */
        
        public override string ToString()
        {
            string str = "N005" + '\t' + this.nodeNo + '\t' + (dof0 ? 1 : 0) + '\t' + (dof1 ? 1 : 0) + '\t' +
                (dof2 ? 1 : 0) + '\t' + (dof3 ? 1 : 0) + '\t' + (dof4 ? 1 : 0) + '\t' + (dof5 ? 1 : 0);
            return str;
        }
        public static bool operator ==(CSupport lhs, CSupport rhs)
        {
            if(lhs.dof0 == rhs.dof0 && lhs.dof1 == rhs.dof1 && lhs.dof2 == rhs.dof2 && lhs.dof3 == rhs.dof3 && lhs.dof4 == rhs.dof4 && lhs.dof5 == rhs.dof5 && lhs.nodeNo == rhs.nodeNo)
                return true;
            else
                return true;
        }
        public static bool operator !=(CSupport lhs, CSupport rhs)
        {
            return !(lhs == rhs);
        }
    }
    public class CSupportCollection
    {
        public CSupportCollection()
        {
        }
        List<CSupport> list = new List<CSupport>();

        public void Add(CSupport data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CSupport data)
        {
            list.Insert(Index, data);
        }
        public CSupport this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public int IndexOf(int nodeNo)
        {
            int i = -1;
            foreach (CSupport csrt in list)
            {
                i++;
                if (csrt.nodeNo == nodeNo)
                    return i;
            }
            return -1;
        }
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CSupportCollection FromStream(StreamReader sr)
        {
            CSupportCollection data = new CSupportCollection();
            //To Do Parse logic
            return data;
        }
    }
    public class CBeamConnectivityRelease
    {
        public CBeamConnectivityRelease() { }
        public int member;
        public string N1, N2;
        public bool FX1, FY1, FZ1, MX1, MY1, MZ1, FX2, FY2, FZ2, MX2, MY2, MZ2;

        public static CBeamConnectivityRelease Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CBeamConnectivityRelease dataObj = new CBeamConnectivityRelease();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 16)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.member = int.Parse(values[iCount]); iCount++;
            dataObj.N1 = (values[iCount]); iCount++;
            dataObj.FX1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FY1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FZ1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MX1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MY1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MZ1 = ((values[iCount] == "1" ? true : false)); iCount++;

            dataObj.N2 = (values[iCount]); iCount++;
            dataObj.FX2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FY2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FZ2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MX2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MY2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MZ2 = ((values[iCount] == "1" ? true : false)); iCount++;

            return dataObj;
        }
        /*
        public static CBeamConnectivityRelease Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CBeamConnectivityRelease dataObj = new CBeamConnectivityRelease();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 16)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.member = int.Parse(values[iCount]); iCount++;
            dataObj.N1 = (values[iCount]); iCount++;
            dataObj.FX1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FY1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FZ1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MX1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MY1 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MZ1 = ((values[iCount] == "1" ? true : false)); iCount++;

            dataObj.N2 = (values[iCount]); iCount++;
            dataObj.FX2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FY2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.FZ2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MX2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MY2 = ((values[iCount] == "1" ? true : false)); iCount++;
            dataObj.MZ2 = ((values[iCount] == "1" ? true : false)); iCount++;

            return dataObj;
        }
      */
        public override string ToString()
        {
            string str = "N009" + '\t' + this.member + '\t' + this.N1 + '\t' + (this.FX1 ? 1 : 0) + '\t' +
                (this.FY1 ? 1 : 0) + '\t' + (this.FZ1 ? 1:0) + '\t' + (this.MX1 ? 1 : 0) + '\t' + (this.MY1 ? 1:0) + '\t' + (this.MZ1 ? 1:0) + '\t' + 
                 this.N2 + '\t' + (this.FX2 ? 1 : 0) + '\t' + (this.FY2 ? 1 : 0) + '\t' + (this.FZ2 ? 1 : 0) + '\t' + (this.MX2 ? 1 : 0) + '\t' + 
                (this.MY2 ? 1 : 0) + '\t' + (this.MZ2 ? 1 : 0);
            return str;
        }
    }
    public class CBeamConnectivityReleaseCollection
    {
        public CBeamConnectivityReleaseCollection()
        {
        }
        List<CBeamConnectivityRelease> list = new List<CBeamConnectivityRelease>();

        public void Add(CBeamConnectivityRelease data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CBeamConnectivityRelease data)
        {
            list.Insert(Index, data);
        }
        public CBeamConnectivityRelease this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CBeamConnectivityReleaseCollection FromStream(StreamReader sr)
        {
            CBeamConnectivityReleaseCollection data = new CBeamConnectivityReleaseCollection();
            //To Do Parse logic
            return data;
        }
        public int IndexOf(string member)
        {
            return this.IndexOf(CTextConvert.getInt(member));
        }
        public int IndexOf(int memberNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].member == memberNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public void Sort()
        {
            CBeamConnectivityReleaseCollection bcrc = new CBeamConnectivityReleaseCollection();
            List<int> nc = new List<int>();
            int i ;
            for (i = 0; i < this.Count; i++)
            {
                nc.Add(this[i].member);
            }
            nc.Sort();
            for (i = 0; i < nc.Count; i++)
            {
                bcrc.Add(this.GetCBeamConnectivityRelease(nc[i]));
            }
            for (i = 0; i < bcrc.Count; i++)
            {
                this[i] = bcrc[i];
            }
            nc.Clear();
            bcrc.Clear();
        }
        public CBeamConnectivityRelease GetCBeamConnectivityRelease(int elementNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].member == elementNo)
                {
                    return this[i];
                }
            }
            return null;
        }
    }
    public class CMovingLoad
    {
        public CMovingLoad() { }
        public string Type;
        public string LoadName;
        public double impactFactor;
        public static CMovingLoad Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMovingLoad dataObj = new CMovingLoad();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 5)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 2;
            dataObj.Type = "TYPE " + values[iCount]; iCount++;
            dataObj.LoadName = values[iCount]; iCount++;
            dataObj.impactFactor = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }
       
        /*
        public static CMovingLoad Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMovingLoad dataObj = new CMovingLoad();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 4)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.Type = values[iCount]; iCount++;
            dataObj.LoadName = values[iCount]; iCount++;
            dataObj.impactFactor =double.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N011" + '\t' + this.Type + '\t' + this.LoadName + '\t' + this.impactFactor;
            return str;
        }
    }
    public class CMovingLoadCollection
    {
        public CMovingLoadCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;
        public string FileName = "";
        List<CMovingLoad> list = new List<CMovingLoad>();
        public EMassUnits MassUnit = EMassUnits.KG;
        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public void Add(CMovingLoad data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CMovingLoad data)
        {
            list.Insert(Index, data);
        }
        public CMovingLoad this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CMovingLoadCollection FromStream(StreamReader sr)
        {
            CMovingLoadCollection data = new CMovingLoadCollection();
            //To Do Parse logic
            return data;
        }

        public int IndexOf(string type)
        {
            for(int  i = 0; i < this.Count;i++)
            {
                if (this[i].Type == type)
                    return i;
            }
            return -1;
        }
    }
    public class CLoadGeneration
    {
        public string Type;
        public int SerialNo;
        public double X, Y, Z;
        public string XINC;
        public CLoadGeneration() 
        {
            Type = "";
            SerialNo = 0;
            X = Y = Z = 0.0;
            XINC = "";
        }
        public static CLoadGeneration Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CLoadGeneration dataObj = new CLoadGeneration();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 8)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.Type = values[1] + " " + values[2]; iCount++;
            dataObj.X = double.Parse(values[3]); iCount++;
            dataObj.Y = double.Parse(values[4]); iCount++;
            dataObj.Z = double.Parse(values[5]); iCount++;
            dataObj.XINC = values[7].ToString(); iCount++;
            return dataObj;
        }
        /*
        public static CLoadGeneration Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CLoadGeneration dataObj = new CLoadGeneration();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 6)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.Type = values[iCount]; iCount++;
            dataObj.X = double.Parse(values[iCount]); iCount++;
            dataObj.Y = double.Parse(values[iCount]); iCount++;
            dataObj.Z = double.Parse(values[iCount]); iCount++;
            dataObj.XINC = values[iCount]; iCount++;
            dataObj.XINC = dataObj.XINC.Remove(0, 4).Trim();
            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N012" + '\t' + this.Type + '\t' + this.X + '\t' + this.Y + '\t' + this.Z + '\t' + "XINC " + this.XINC;
            return str;
        }

    }
    public class CLoadGenerationCollection
    {
        public CLoadGenerationCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;
        List<CLoadGeneration> list = new List<CLoadGeneration>();
        public int repeatTime;
        public void Add(CLoadGeneration data)
        {
            data.SerialNo = GetSerialNo(data.Type);
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CLoadGeneration data)
        {
            list.Insert(Index, data);
        }
        public CLoadGeneration this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public int IndexOf(string sType, int slNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == sType && list[i].SerialNo == slNo) return i;
            }
            return -1;
        }
        public int GetSerialNo(string sType)
        {
            int slNo = 1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == sType)
                    slNo++;
            }
            return slNo;
        }
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CLoadGenerationCollection FromStream(StreamReader sr)
        {
            CLoadGenerationCollection data = new CLoadGenerationCollection();
            //To Do Parse logic
            return data;
        }
    }
    public class CLoadCombination
    {
        public CLoadCombination() { }
        public int loadCase;
        public int load;
        public double factor;
        public static CLoadCombination Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CLoadCombination dataObj = new CLoadCombination();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 4)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.loadCase = int.Parse(values[iCount]); iCount++;
            dataObj.load = int.Parse(values[iCount]); iCount++;
            dataObj.factor = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        /*
        public static CLoadCombination Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CLoadCombination dataObj = new CLoadCombination();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 4)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.loadCase = int.Parse(values[iCount]); iCount++;
            dataObj.load = int.Parse(values[iCount]); iCount++;
            dataObj.factor = double.Parse(values[iCount]); iCount++;
            
            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N013" + '\t' + this.loadCase + '\t' + this.load + '\t' + this.factor;
            return str;
        }
    }
    public class CLoadCombinationCollection
    {
        public CLoadCombinationCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;
        List<CLoadCombination> list = new List<CLoadCombination>();
        public int repeatTime;
        public void Add(CLoadCombination data)
        {
            int indx = IndexOf(data);
            if (indx != -1)
                RemoveAt(indx);
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CLoadCombination data)
        {
            list.Insert(Index, data);
        }
        public CLoadCombination this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public int IndexOf(CLoadCombination lcmb)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].loadCase == lcmb.loadCase && list[i].load == lcmb.load)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool DeleteLoadCase(int loadCase)
        {
            bool bSuccess = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].loadCase == loadCase)
                {
                    RemoveAt(i);
                    i = 0;
                    bSuccess = true;
                }
            }
            return bSuccess;
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CLoadCombinationCollection FromStream(StreamReader sr)
        {
            CLoadCombinationCollection data = new CLoadCombinationCollection();
            //To Do Parse logic
            return data;
        }
    }
    public class CAnalysis 
    {
        
        public string AnalysisType;
        public int NDYN;
        public int NF;
        public CAnalysis() 
        {
            AnalysisType = "NDYN";
            NDYN = 1;
            NF = -1;
        }
         
        public static CAnalysis Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CAnalysis dataObj = new CAnalysis();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 5)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
           
            dataObj.AnalysisType = values[1];;
            dataObj.NDYN = int.Parse(values[2]);
            dataObj.NF = int.Parse(values[4]); 
            return dataObj;
        }
        public override string ToString()
        {
            string str = "N099 " +  this.AnalysisType + "\t" + this.NDYN + " NF" + '\t' + this.NF;
            return str;
        }

    }
    public class CAstraUnits
    {
        public CAstraUnits() { }
        public static double GetMassFact(string unit, ref short wunit_flag)
        {

            // MASS UNIT//
            double wfact = 0.0;
            if (unit.ToUpper() == "MTON")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 1;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 1.0;                // will remain same
                            break;
                        case 2: wfact = 10.0;
                            break;
                        case 3: wfact = 1000.0;
                            break;
                        case 4: wfact = 10000.0;
                            break;
                        case 5: wfact = 1000000.0;
                            break;
                        case 6: wfact = 2.2046;
                            break;
                        case 7: wfact = 2204.6;
                            break;
                    }

                }
            }

            else if (unit.ToUpper() == "KN")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 2;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 0.1;
                            break;
                        case 2: wfact = 1.0; // will remain same
                            break;
                        case 3: wfact = 100.0;
                            break;
                        case 4: wfact = 1000.0;
                            break;
                        case 5: wfact = 100000.0;
                            break;
                        case 6: wfact = 0.22046;
                            break;
                        case 7: wfact = 220.46;
                            break;
                    }
                }
            }
            else if (unit.ToUpper() == "KG")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 3;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 0.001;
                            break;
                        case 2: wfact = 0.01;
                            break;
                        case 3: wfact = 1.0;                // will remain same
                            break;
                        case 4: wfact = 10.0;
                            break;
                        case 5: wfact = 1000.0;
                            break;
                        case 6: wfact = 0.0022046;
                            break;
                        case 7: wfact = 2.2046;
                            break;
                    }
                }
            }

            else if (unit.ToUpper() == "N" || unit.ToUpper() == "NEW")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 4;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 0.0001;
                            break;
                        case 2: wfact = 0.001;
                            break;
                        case 3: wfact = 0.1;
                            break;
                        case 4: wfact = 1.0;                // will remain same
                            break;
                        case 5: wfact = 100.0;
                            break;
                        case 6: wfact = 0.00022046;
                            break;
                        case 7: wfact = 0.22046;
                            break;
                    }
                }
            }
            else if (unit.ToUpper() == "GMS" || unit.ToUpper() == "GM")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 5;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 0.000001;
                            break;
                        case 2: wfact = 0.00001;
                            break;
                        case 3: wfact = 0.001;                // will remain same
                            break;
                        case 4: wfact = 0.01;
                            break;
                        case 5: wfact = 1.0;
                            break;
                        case 6: wfact = 0.0022046;
                            break;
                        case 7: wfact = 2.2046;
                            break;
                    }
                }
            }
           
            else if (unit.ToUpper() == "KIP")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 6;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 0.453597;
                            break;
                        case 2: wfact = 4.53597;
                            break;
                        case 3: wfact = 453.597;
                            break;
                        case 4: wfact = 4535.97;
                            break;
                        case 5: wfact = 453597.0;                
                            break;
                        case 6: wfact = 1.0; // will remain same
                            break;
                        case 7: wfact = 1000.0;
                            break;
                    }
                }
            }

            else if (unit.ToUpper() == "LBS")
            {
                if (wunit_flag == 0)
                {
                    wfact = 1.0;
                    wunit_flag = 7;
                }
                else
                {
                    switch (wunit_flag)
                    {
                        case 1: wfact = 0.000453597;
                            break;
                        case 2: wfact = 0.00453597;
                            break;
                        case 3: wfact = 0.453597;
                            break;
                        case 4: wfact = 4.53597;
                            break;
                        case 5: wfact = 453.597;
                            break;
                        case 6: wfact = 0.001;
                            break;
                        case 7: wfact = 1.0;                // will remain same
                            break;
                    }

                }
            }
            return wfact;
        }
        public static double GetLengthFact(string unit, ref short lunit_flag)
        {
            // LENGTH UNIT//
            double lfact = 0.0;
            if (unit.ToUpper() == "MM")
            {
                if (lunit_flag == 0)
                {
                    lfact = 1.0;
                    lunit_flag = 1;
                }
                else
                {
                    switch (lunit_flag)
                    {
                        case 1: lfact = 1.0;                // will remain same
                            break;
                        case 2: lfact = 0.1;
                            break;
                        case 3: lfact = 0.001;
                            break;
                        case 4: lfact = 0.0010936;
                            break;
                        case 5: lfact = 0.003281;
                            break;
                        case 6: lfact = 0.03937;
                            break;
                    }
                }
            }
            else if (unit.ToUpper() == "CM")
            {
                if (lunit_flag == 0)
                {
                    lfact = 1.0;
                    lunit_flag = 2;
                }
                else
                {
                    switch (lunit_flag)
                    {
                        case 1: lfact = 10.0;
                            break;
                        case 2: lfact = 1.0; // will remain same
                            break;
                        case 3: lfact = 0.01;
                            break;
                        case 4: lfact = 0.010936;
                            break;
                        case 5: lfact = 0.03281;
                            break;
                        case 6: lfact = 0.3937;
                            break;
                    }
                }
            }

            else if (unit.ToUpper() == "M" || unit.ToUpper() == "ME" || unit.ToUpper() == "METRES")
            {
                if (lunit_flag == 0)
                {
                    lfact = 1.0;
                    lunit_flag = 3;
                }
                else
                {
                    switch (lunit_flag)
                    {
                        case 1: lfact = 1000.0;
                            break;
                        case 2: lfact = 100.0;
                            break;
                        case 3: lfact = 1.0;                // will remain same
                            break;
                        case 4: lfact = 1.0936;
                            break;
                        case 5: lfact = 3.281;
                            break;
                        case 6: lfact = 39.37;
                            break;
                    }
                }
            }
            else if (unit.ToUpper() == "YDS")
            {
                if (lunit_flag == 0)
                {
                    lfact = 1.0;
                    lunit_flag = 4;
                }
                else
                {
                    switch (lunit_flag)
                    {
                        case 1: lfact = 304.8;
                            break;
                        case 2: lfact = 30.48;
                            break;
                        case 3: lfact = 0.3048;
                            break;
                        case 4: lfact = 1.0; // will remain same
                            break;
                        case 5: lfact = 3.0;
                            break;
                        case 6: lfact = 36.0;
                            break;
                    }
                }
            }

            else if (unit.ToUpper() == "FT")
            {
                if (lunit_flag == 0)
                {
                    lfact = 1.0;
                    lunit_flag = 5;
                }
                else
                {
                    switch (lunit_flag)
                    {
                        case 1: lfact = 304.8;
                            break;
                        case 2: lfact = 30.48;
                            break;
                        case 3: lfact = 0.3048;
                            break;
                        case 4: lfact = 0.333; // will remain same
                            break;
                        case 5: lfact = 1.0;
                            break;
                        case 6: lfact = 12.0;
                            break;
                    }
                }
            }

            else if (unit.ToUpper() == "INCH" || unit.ToUpper() == "IN")
            {
                if (lunit_flag == 0)
                {
                    lfact = 1.0;
                    lunit_flag = 6;
                }
                else
                {
                    switch (lunit_flag)
                    {
                        case 1: lfact = 25.4;
                            break;
                        case 2: lfact = 2.54;
                            break;
                        case 3: lfact = 0.0254;
                            break;
                        case 4: lfact = 0.0278;
                            break;
                        case 5: lfact = 0.0833;
                            break;
                        case 6: lfact = 1.0; // will remain same
                            break;
                    }
                }
            }
            return lfact;
        }
        public static string GetLengthUnit(double fact)
        {
            string unit = "";
            //Mass Factor
            if(fact == 0.01) unit = "CM";
            else if(fact == 1.0) unit = "METRES";
            else if(fact == 0.001) unit = "MM";
            else if(fact == 0.3048) unit = "FT";
            else if(fact == 0.305) unit = "FT";
            else if(fact == 0.0254) unit = "INCH";
            else if(fact == 0.025) unit = "INCH";
            else if(fact == 91.44) unit = "YDS";
            return unit;
        }
        public static string GetMassUnit(double fact)
        {
            string unit = "";
            //Mass Factor
            if(fact == 1.0) unit = "KG";
            else if(fact == 100.00) unit = "KN";
            else if(fact == 1000.00) unit = "MTON";
            else if(fact == 0.10) unit = "NEW";
            else if(fact == 0.001) unit = "GMS";
            else if(fact ==  0.4524) unit = "LBS";
            else if(fact ==  0.452) unit = "LBS";
            else if(fact ==  452.40) unit = "KIP";
            return unit;
        }
        public static double GetFact(string unit)
        {
            double fact = 0.0;
            //Mass Factor
            if (unit.ToUpper() == "CM")
            {
                fact = 0.01;
            }
            else if (unit.ToUpper() == "M" || unit.ToUpper() == "ME" || unit.ToUpper() == "METRES")
            {
                fact = 1.0;
            }
            else if (unit.ToUpper() == "MM")
            {
                fact = 0.001;
            }
            else if (unit.ToUpper() == "FT")
            {
                fact = 0.3048;
            }
            else if (unit.ToUpper() == "INCH")
            {
                fact = 0.0254;
            }
            else if (unit.ToUpper() == "YDS")
            {
                fact = 30.48 * 3.0;
            }
            // Weight Factor
            else if (unit.ToUpper() == "KG")
            {
                fact = 1.0;
            }
            else if (unit.ToUpper() == "KN")
            {
                fact = 100.00;
            }
            else if (unit.ToUpper() == "MTON")
            {
                fact = 1000.00;
            }
            else if (unit.ToUpper() == "N" || unit.ToUpper() == "NEW")
            {
                fact = 0.10;
            }
            else if (unit.ToUpper() == "GMS")
            {
                fact = 0.001;
            }
            else if (unit.ToUpper() == "LBS")
            {
                fact = 0.4524;
            }
            else if (unit.ToUpper() == "KIP")
            {
                fact = 452.40;
            }
            return fact;
        }
        public static int GetLengthUnitIndex(double lenFact)
        {
            //Mass Factor
            string unit = "";
            int index = -1;
            if (lenFact == 1.0)
            {
                unit = "M";
                index = 0;
            }
            else if (lenFact == 0.01)
            {
                unit = "CM";
                index = 1;
            } 
            else if (lenFact == 0.001)
            {
                unit = "MM";
                index = 2;
            }
            else if (lenFact == (30.48 * 3.0))
            {
                unit = "YDS";
                index = 3;
            }
            else if (lenFact == 0.3048 || lenFact == 0.305)
            {
                unit = "FT";
                index = 4;
            }
            else if (lenFact == 0.0254 || lenFact == 0.025)
            {
                unit = "INCH";
                index = 5;
            }

            return index;
          
            //return unit;
        }
        public static int GetWeightUnitIndex(double wtFact)
        {
                // Weight Factor
            int index = -1;
            string unit = "";
            if (wtFact == 1.0)
            {
                unit = "KG";
                index = 0;
            }
            else if (wtFact == 100.00)
            {
                
                unit = "KN";
                index = 1;

            }
            else if (wtFact == 1000.00)
            {
                unit = "MTON";
                index = 2;

            }
            else if (wtFact == 0.10)
            {
                unit = "N";
                index = 3;
            }
            else if (wtFact == 0.001)
            {
                unit = "GMS";
                index = 4;
            }
            else if (wtFact == 0.4524)
            {
                unit = "LBS";
                index = 5;
            }
            else if (wtFact == 452.40)
            {
                unit = "KIP";
                index = 6;
            }
            return index;
        }
        public static string[] GetBasicLengthMassUnits(string strData)
        {
            string[] sunts = new string[2];
            sunts[0] = "";
            sunts[1] = "";

            string org = strData;
            strData = strData.Trim();
            string[] values = strData.Split(new char[] { ' ' });

            while (strData.IndexOf("  ") != -1)
            {
                strData = strData.Replace("  ", " ");
            }
            values = strData.Split(new char[] { ' ' });

            int iCount = 4;

            sunts[0] = values[iCount]; iCount++;
            sunts[1] = values[iCount]; iCount++;
            return sunts;
        }
        public static double[] GetLengthMassUnits(string strData)
        {
            double[] d = new double[2];
            d[0] = 1.0;
            d[1] = 1.0;
            string org = strData;
            strData = strData.Trim();
            string[] values = strData.Split(new char[] { ' ' });

            while (strData.IndexOf("  ") != -1)
            {
                strData = strData.Replace("  ", " ");
            }
            values = strData.Split(new char[] { ' ' });

            int iCount = 2;
            d[0] = double.Parse(values[iCount]); iCount++;
            d[1] = double.Parse(values[iCount]); iCount++;
            return d;
        }
        public static string[] GetUnits(string strData)
        {
            string[] units = new string[4];
            string[] values;
            if (strData.Contains("UNIT"))
            {
                strData = strData.Replace('\t', ' ');
                strData = strData.Replace("  ", " ");
                values = strData.Split(new char[] { ' ' });
                units[0] = values[4];
                units[1] = values[5];
                units[2] = values[6];
                units[3] = values[7];
            }
            return units;
        }
        public static EMassUnits GetMassUnit(string unit)
        {
            if (unit == "KG")
                return EMassUnits.KG;
            else if (unit == "GMS")
                return EMassUnits.GMS;
            else if (unit == "KIP")
                return EMassUnits.KIP;
            else if (unit == "KN")
                return EMassUnits.KN;
            else if (unit == "LBS")
                return EMassUnits.LBS;
            else if (unit == "MTON")
                return EMassUnits.MTON;
            else if (unit == "NEW")
                return EMassUnits.NEW;
            return EMassUnits.NEW;
        }
        public static eLengthUnits GetLengthUnit(string unit)
        {
            if (unit == "METRES" || unit == "ME" || unit == "M")
                return eLengthUnits.METRES;
            else if (unit == "CM")
                return eLengthUnits.CM;
            else if (unit == "FT")
                return eLengthUnits.FT;
            else if (unit == "INCH" || unit == "IN")
                return eLengthUnits.INCH;
            else if (unit == "MM")
                return eLengthUnits.MM;
            else if (unit == "YDS")
                return eLengthUnits.YDS;
            return eLengthUnits.METRES;
        }

        public static double GetMassFact(EMassUnits basicUnit,EMassUnits currUnit)
        {

            // MASS UNIT//
            //double wfact = 0.0;
            short wunit_flag = 0;
            switch (basicUnit)
            {
                case EMassUnits.MTON :
                    wunit_flag = 1;//MTON
                    break;
                case EMassUnits.KN:
                    wunit_flag = 2;//KN
                    break;
                case EMassUnits.KG:
                    wunit_flag = 3;//KG
                    break;
                case EMassUnits.NEW:
                    wunit_flag = 4;//NEW
                    break;
                case EMassUnits.GMS:
                    wunit_flag = 5;//GMS
                    break;
                case EMassUnits.KIP:
                    wunit_flag = 6;//KIP
                    break;
                case EMassUnits.LBS:
                    wunit_flag = 7;//LBS
                    break;
            }

            return GetMassFact(currUnit.ToString(), ref wunit_flag);
        }
        public static double GetLengthFact(eLengthUnits basicUnit,eLengthUnits currUnit)
        {
            // LENGTH UNIT//
            double lfact = 0.0;

            short lunit_flag = 0;

            switch (basicUnit)
            {
                case eLengthUnits.MM:
                    lunit_flag = 1;//MM
                    break;
                case eLengthUnits.CM:
                    lunit_flag = 2;//CM
                    break;
                case eLengthUnits.METRES:
                    lunit_flag = 3;//ME
                    break;
                case eLengthUnits.YDS:
                    lunit_flag = 4;//YDS
                    break;
                case eLengthUnits.FT:
                    lunit_flag = 5;//FT
                    break;
                case eLengthUnits.INCH:
                    lunit_flag = 6;//INCH
                    break;
            }
            return GetLengthFact(currUnit.ToString(), ref lunit_flag);
        }


    }
    public class CTextConvert
    {
        public static int getInt(string txt)
        {
            int val = 0;
            try
            {
                val = int.Parse(txt);

            }
            catch (Exception ex)
            {
            }
            return val;
        }
        public static int getInt(string txt, int defaultValue)
        {
            int val = 0;
            try
            {
                val = int.Parse(txt);

            }
            catch (Exception ex)
            {
                val = defaultValue;
            }
            return val;
        }
        public static double getDouble(string txt)
        {
            double val = 0.00;
            try
            {
                val = double.Parse(txt);
            }
            catch (Exception ex)
            {
            }
            return val;
        }
        public static double getDouble(string txt, double defaultValue)
        {
            double val = 0.00;
            try
            {
                val = double.Parse(txt);

            }
            catch (Exception ex)
            {
                val = defaultValue;
            }
            return val;
        }
    }

    public class CAreaLoad
    {
        public int memberNo;
        public int loadNo;
        public int loadcase;
        public double fy;
        public CAreaLoad() 
        {
            memberNo = 0;
            loadcase = 0;
            loadNo = 0;
            fy = 0;
        }
        public static CAreaLoad Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CAreaLoad dataObj = new CAreaLoad();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 4)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.fy = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        /*
        public static CAreaLoad Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CAreaLoad dataObj = new CAreaLoad();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 4)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.memberNo = int.Parse(values[iCount]); iCount++;
            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.fy = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N014" + '\t' + this.memberNo + '\t' + this.loadcase + '\t' + this.fy;
            return str;
        }

    }
    public class CAreaLoadCollection
    {
        public CAreaLoadCollection()
        {
        }
        public double MassFactor = 1.0;
        public double LengthFactor = 1.0;
        List<CAreaLoad> list = new List<CAreaLoad>();
        public int repeatTime;
        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public EMassUnits MassUnit = EMassUnits.KG;

        public void Add(CAreaLoad data)
        {
            this.DeleteMember(data.memberNo,data.loadcase);
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CAreaLoad data)
        {
            list.Insert(Index, data);
        }
        public CAreaLoad this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CAreaLoadCollection FromStream(StreamReader sr)
        {
            CAreaLoadCollection data = new CAreaLoadCollection();
            //To Do Parse logic
            return data;
        }
        public bool DeleteMember(int memberNo)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == memberNo)
                {
                    this.RemoveAt(i);
                    fl = true;
                }
            }
            return fl;
        }

        public bool DeleteMember(int memberNo,int loadCase)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].memberNo == memberNo && this[i].loadcase == loadCase)
                {
                    this.RemoveAt(i);
                    fl = true;
                }
            }
            return fl;
        }
        public bool DeleteLoadNo(int loadcase,int LoadNo)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].loadcase == loadcase && this[i].loadNo == LoadNo)
                {
                    this.RemoveAt(i);
                    i = -1;
                    fl = true;
                }
            }
            SetLoadNo(loadcase);
            return fl;
        }
        public bool DeleteLoadcase(int loadcase)
        {
            bool fl = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].loadcase == loadcase)
                {
                    this.RemoveAt(i);
                    i = -1;
                    fl = true;
                }
            }
            return fl;
        }

        public void SetLoadNo(int LoadCase)
        {
            int lno = 1;
            bool fl = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].loadcase == LoadCase)
                {
                    if (fl)
                    {
                        list[i].loadNo = lno; fl = false;
                    }
                    else
                    {
                        if (list[i].fy == list[i - 1].fy)
                        {
                            list[i].loadNo = lno;
                        }
                        else
                        {
                            lno++;
                            list[i].loadNo = lno;
                        }
                    }
                }

            }
        }
        public int MaxLoadNo(int LoadCase)
        {
            int maxLno = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].loadcase == LoadCase)
                {
                    maxLno = list[i].loadNo;
                }
            }
            return maxLno;
        }
    }
    public class CMaterialPropertyInformation
    {
        public int matIdNo;
        public double massDen, alphaX, alphaY, alphaZ, Exx, Exy, Exs, Eyy, Eys, Gxy;

        // Distributed Lateral, Temperature Multiplier, X-Direction Acceleration, Y-Direction Acceleration, Z-Direction Acceleration

        public static CMaterialPropertyInformation Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMaterialPropertyInformation dataObj = new CMaterialPropertyInformation();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 12)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.matIdNo = int.Parse(values[iCount]); iCount++;
            dataObj.massDen = double.Parse(values[iCount]); iCount++;
            if (values.Length == 9)
            {
                dataObj.alphaX = 0.0;
                dataObj.alphaY = 0.0;
                dataObj.alphaZ = 0.0;
            }
            else
            {
                dataObj.alphaX = double.Parse(values[iCount]); iCount++;
                dataObj.alphaY = double.Parse(values[iCount]); iCount++;
                dataObj.alphaZ = double.Parse(values[iCount]); iCount++;
            }
            dataObj.Exx = double.Parse(values[iCount]); iCount++;
            dataObj.Exy = double.Parse(values[iCount]); iCount++;
            dataObj.Exs = double.Parse(values[iCount]); iCount++;
            dataObj.Eyy = double.Parse(values[iCount]); iCount++;
            dataObj.Eys = double.Parse(values[iCount]); iCount++;
            dataObj.Gxy = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
       /*
        public static CMaterialPropertyInformation Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CMaterialPropertyInformation dataObj = new CMaterialPropertyInformation();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 12)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.matIdNo = int.Parse(values[iCount]); iCount++;
            dataObj.massDen = double.Parse(values[iCount]); iCount++;
            dataObj.alphaX = double.Parse(values[iCount]); iCount++;
            dataObj.alphaY = double.Parse(values[iCount]); iCount++;
            dataObj.alphaZ = double.Parse(values[iCount]); iCount++;
            dataObj.Exx = double.Parse(values[iCount]); iCount++;
            dataObj.Exy = double.Parse(values[iCount]); iCount++;
            dataObj.Exs = double.Parse(values[iCount]); iCount++;
            dataObj.Eyy = double.Parse(values[iCount]); iCount++;
            dataObj.Eys = double.Parse(values[iCount]); iCount++;
            dataObj.Gxy = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
        public override string ToString()
        {
            string str = "N019" + '\t' + this.matIdNo + '\t' + this.massDen + '\t' + this.alphaX + '\t' + this.alphaY + '\t' +
                this.alphaZ + '\t' + this.Exx + '\t' + this.Exy + '\t' + this.Exs + '\t' + this.Eyy + '\t' + this.Eys + '\t' +
                this.Gxy;
            return str;
        }

        public CMaterialPropertyInformation() { }
    }
    public class CMaterialPropertyInformationCollection
    {
        public CMaterialPropertyInformationCollection()
        {
        }
        public double MassFactor = CAstraUnits.GetFact("MTon");
        public double LengthFactor = CAstraUnits.GetFact("M");
        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public EMassUnits MassUnit = EMassUnits.KG;

        List<CMaterialPropertyInformation> list = new List<CMaterialPropertyInformation>();
        public void Add(CMaterialPropertyInformation data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CMaterialPropertyInformation data)
        {
            list.Insert(Index, data);
        }
        public CMaterialPropertyInformation this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }
        public bool DeleteMatId(int matId)
        {
            bool b = false;

            int index = this.IndexOf(matId);

            if (index != -1)
            {
                this.RemoveAt(index);
                b = true;
                for (int i = index; i < this.Count; i++)
                {
                    this[i].matIdNo -= 1;
                }
            }
            return b;
        }
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CMaterialPropertyInformationCollection FromStream(StreamReader sr)
        {
            CMaterialPropertyInformationCollection data = new CMaterialPropertyInformationCollection();
            //To Do Parse logic
            return data;
        }

        public int IndexOf(int matIdNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].matIdNo == matIdNo)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class CElementMultiplier
    {
        public byte loadcase;
        public double Pressure;
        public double thermalEffects;
        public double XAcceleration;
        public double YAcceleration;
        public double ZAcceleration;

        public CElementMultiplier() { }

        public override string ToString()
        {
            string str = "N020" + '\t' + this.loadcase + '\t' + this.Pressure + '\t' + this.thermalEffects +
                '\t' + XAcceleration + '\t' + this.YAcceleration + '\t' + this.ZAcceleration;
            return str;
        }
        public static CElementMultiplier Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CElementMultiplier dataObj = new CElementMultiplier();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 7)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.loadcase = byte.Parse(values[iCount]); iCount++;
            dataObj.Pressure = double.Parse(values[iCount]); iCount++;
            dataObj.thermalEffects = double.Parse(values[iCount]); iCount++;
            dataObj.XAcceleration = double.Parse(values[iCount]); iCount++;
            dataObj.YAcceleration = double.Parse(values[iCount]); iCount++;
            dataObj.ZAcceleration = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
       /*
        public static CElementMultiplier Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CElementMultiplier dataObj = new CElementMultiplier();
            string[] values = strData.Split(new char[] { '\t' });
            if (values.Length != 7)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 1;
            dataObj.loadcase = byte.Parse(values[iCount]); iCount++;
            dataObj.Pressure = double.Parse(values[iCount]); iCount++;
            dataObj.thermalEffects = double.Parse(values[iCount]); iCount++;
            dataObj.XAcceleration = double.Parse(values[iCount]); iCount++;
            dataObj.YAcceleration = double.Parse(values[iCount]); iCount++;
            dataObj.ZAcceleration = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        */
    }
    public class CElementMultiplierCollection
    {
        public CElementMultiplierCollection()
        {
        }
        List<CElementMultiplier> list = new List<CElementMultiplier>();
        public void Add(CElementMultiplier data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CElementMultiplier data)
        {
            list.Insert(Index, data);
        }
        public CElementMultiplier this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public int IndexOf(int loadcase)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].loadcase == (byte)loadcase)
                    return i;
            }
            return -1;
        }
       
        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CElementMultiplierCollection FromStream(StreamReader sr)
        {
            CElementMultiplierCollection data = new CElementMultiplierCollection();
            //To Do Parse logic
            return data;
        }
    }

    public class CElementData
    {
        public int elementNo, node1, node2, node3, node4,node5, matId;
        public double elementThickness, distLateralPressure, meanTempVar, meanTempGrad;
        public CElementData() { }
        public override string ToString()
        {
            string str = "N018" + '\t' + this.elementNo + '\t' + this.node1 + '\t' + this.node2 + '\t' + this.node3 + '\t' +
                this.node4 + '\t' + this.node5 + '\t' + this.matId + '\t' + this.elementThickness + '\t' + this.distLateralPressure +
                '\t' + this.meanTempVar + '\t' + this.meanTempGrad;
            return str;
        }
        public static CElementData Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CElementData dataObj = new CElementData();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 12)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;
            dataObj.elementNo = int.Parse(values[iCount]); iCount++;
            dataObj.node1 = int.Parse(values[iCount]); iCount++;
            dataObj.node2 = int.Parse(values[iCount]); iCount++;
            dataObj.node3 = int.Parse(values[iCount]); iCount++;
            dataObj.node4 = int.Parse(values[iCount]); iCount++;
            dataObj.node5 = int.Parse(values[iCount]); iCount++;
            dataObj.matId = int.Parse(values[iCount]); iCount++;
            dataObj.elementThickness = double.Parse(values[iCount]); iCount++;
            dataObj.distLateralPressure = double.Parse(values[iCount]); iCount++;
            dataObj.meanTempVar = double.Parse(values[iCount]); iCount++;
            dataObj.meanTempGrad = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        public string ElementThickNess()
        {
            return String.Format("N016\t{0}\t{1}", this.elementNo, this.elementThickness);
        }
        public string ElementPressure()
        {
            return String.Format("N017\t{0}\t{1}\t{2}", this.elementNo,1,this.distLateralPressure);
        }
    }
    public class CElementDataCollection
    {
        public CElementDataCollection()
        {
        }
        public double MassFactor = 1;
        public double LengthFactor = 1;
        List<CElementData> list = new List<CElementData>();
        public eLengthUnits LengthUnit = eLengthUnits.METRES;
        public EMassUnits MassUnit = EMassUnits.KG;

        public void Add(CElementData data)
        {
            list.Add(data);
        }
        public void RemoveAt(int Index)
        {
            list.RemoveAt(Index);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void InsertAt(int Index, CElementData data)
        {
            list.Insert(Index, data);
        }
        public CElementData this[int Index]
        {
            get
            {
                return this.list[Index];
            }
            set
            {
                this.list[Index] = value;
            }
        }
        public int Count
        {
            get { return list.Count; }
        }

        public void ToStream(StreamWriter sw)
        {
            //To Do Write logic
        }
        public static CElementDataCollection FromStream(StreamReader sr)
        {
            CElementDataCollection data = new CElementDataCollection();
            //To Do Parse logic
            return data;
        }

        public int IndexOf(int elementNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].elementNo == elementNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public void Sort()
        {
            List<CElementData> led = new List<CElementData>();
            List<int> nc = new List<int>();
            int i = 0;
            for (i = 0; i < this.Count; i++)
            {
                nc.Add(this[i].elementNo);
            }
            nc.Sort();
            for (i = 0; i < this.Count; i++)
            {
                led.Add(this[this.IndexOf(nc[i])]);
            }
            for (i = 0; i < led.Count; i++)
            {
                this[i] = led[i];
            }
        }
    }


    //Chiranjit [2012 06 20]
    public class LoadApplied
    {
        public string Load_Name { get; set; }
        public double Total_Load { get; set; }
        public double Applied_Load { get; set; }
        public double a1_a { get; set; }
        public double b1_b { get; set; }
        public double LoadWidth { get { return a1_a; } }
        public double LoadLength { get { return b1_b; } }
        static List<LoadApplied> list = null;
        public LoadApplied()
        {
            Load_Name = "";
            Total_Load = 554.0;
            Applied_Load = 554.0;
            a1_a = 554.0;
            b1_b = 554.0;
        }
        public static void Load_Default()
        {
            list = new List<LoadApplied>();

            LoadApplied la = new LoadApplied();

            la.Load_Name = "IRC CLASS A";
            la.Total_Load = 554.0;
            la.Applied_Load = 57.0;
            la.a1_a = 0.5;
            la.b1_b = 19.4;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "IRC CLASS B";
            la.Total_Load = 332.0;
            la.Applied_Load = 34.0;
            la.a1_a = 0.38;
            la.b1_b = 19.4;
            list.Add(la);


            la = new LoadApplied();
            la.Load_Name = "IRC 70R TRACK";
            la.Total_Load = 700.0;
            la.Applied_Load = 35.0;
            la.a1_a = 0.84;
            la.b1_b = 4.57;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "IRC 70R WHEEL";
            la.Total_Load = 1000.0;
            la.Applied_Load = 85.0;
            la.a1_a = 0.795;
            la.b1_b = 15.22;
            list.Add(la);


            la = new LoadApplied();
            la.Load_Name = "IRC CLASS AA TRACK";
            la.Total_Load = 700.0;
            la.Applied_Load = 35.0;
            la.a1_a = 0.85;
            la.b1_b = 3.60;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "IRC 24R TRACK";
            la.Total_Load = 250.0;
            la.Applied_Load = 12.50;
            la.a1_a = 0.18;
            la.b1_b = 3.2940;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "LRFD HTL 57";
            la.Total_Load = 570.0;
            la.Applied_Load = 52.5;
            la.a1_a = 0.90;
            la.b1_b = 16.917;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "LRFD HL 93 HS 20";
            la.Total_Load = 360.0;
            la.Applied_Load = 80.0;
            la.a1_a = 0.90;
            la.b1_b = 8.5344;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "LRFD HL 93 H 20";
            la.Total_Load = 200.0;
            la.Applied_Load = 80.0;
            la.a1_a = 0.90;
            la.b1_b = 4.2672;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "BG RAIL 1";
            la.Total_Load = 2942.4;
            la.Applied_Load = 122.6;
            la.a1_a = 0.838;
            la.b1_b = 36.03;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "BG RAIL 2";
            la.Total_Load = 2647.2;
            la.Applied_Load = 110.3;
            la.a1_a = 0.838;
            la.b1_b = 30.50;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "MG RAIL 1";
            la.Total_Load = 1184.6;
            la.Applied_Load = 64.7;
            la.a1_a = 0.838;
            la.b1_b = 17.907;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "MG RAIL 2";
            la.Total_Load = 959.9;
            la.Applied_Load = 52.45;
            la.a1_a = 0.838;
            la.b1_b = 16.815;
            list.Add(la);

            la = new LoadApplied();
            la.Load_Name = "USER DEFINE";
            la.Total_Load = 0.0;
            la.Applied_Load = 0.0;
            la.a1_a = 0.0;
            la.b1_b = 0.0;
            list.Add(la);




        }

        public static void Load_Default(IApplication iApp)
        {
            if(list == null)
                list = new List<LoadApplied>();
            else
                list.Clear();

            LoadApplied la = new LoadApplied();
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                #region Indian Standard
                la.Load_Name = "IRC CLASS A";
                la.Total_Load = 554.0;
                la.Applied_Load = 57.0;
                la.a1_a = 0.5;
                la.b1_b = 19.4;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "IRC CLASS B";
                la.Total_Load = 332.0;
                la.Applied_Load = 34.0;
                la.a1_a = 0.38;
                la.b1_b = 19.4;
                list.Add(la);


                la = new LoadApplied();
                la.Load_Name = "IRC 70R TRACK";
                la.Total_Load = 700.0;
                la.Applied_Load = 35.0;
                la.a1_a = 0.84;
                la.b1_b = 4.57;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "IRC 70R WHEEL";
                la.Total_Load = 1000.0;
                la.Applied_Load = 85.0;
                la.a1_a = 0.795;
                la.b1_b = 15.22;
                list.Add(la);


                la = new LoadApplied();
                la.Load_Name = "IRC CLASS AA TRACK";
                la.Total_Load = 700.0;
                la.Applied_Load = 35.0;
                la.a1_a = 0.85;
                la.b1_b = 3.60;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "IRC 24R TRACK";
                la.Total_Load = 250.0;
                la.Applied_Load = 12.50;
                la.a1_a = 0.18;
                la.b1_b = 3.2940;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "LRFD HTL 57";
                la.Total_Load = 570.0;
                la.Applied_Load = 52.5;
                la.a1_a = 0.90;
                la.b1_b = 16.917;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "LRFD HL 93 HS 20";
                la.Total_Load = 360.0;
                la.Applied_Load = 80.0;
                la.a1_a = 0.90;
                la.b1_b = 8.5344;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "LRFD HL 93 H 20";
                la.Total_Load = 200.0;
                la.Applied_Load = 80.0;
                la.a1_a = 0.90;
                la.b1_b = 4.2672;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "BG RAIL 1";
                la.Total_Load = 2942.4;
                la.Applied_Load = 122.6;
                la.a1_a = 0.838;
                la.b1_b = 36.03;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "BG RAIL 2";
                la.Total_Load = 2647.2;
                la.Applied_Load = 110.3;
                la.a1_a = 0.838;
                la.b1_b = 30.50;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "MG RAIL 1";
                la.Total_Load = 1184.6;
                la.Applied_Load = 64.7;
                la.a1_a = 0.838;
                la.b1_b = 17.907;
                list.Add(la);

                la = new LoadApplied();
                la.Load_Name = "MG RAIL 2";
                la.Total_Load = 959.9;
                la.Applied_Load = 52.45;
                la.a1_a = 0.838;
                la.b1_b = 16.815;
                list.Add(la);

                #endregion Indian Standard
            }
            else
            {
                foreach (var item in iApp.LiveLoads)
                {

                    la = new LoadApplied();
                    la.Load_Name = item.Code;
                    la.Total_Load = item.Loads.SUM;
                    la.Applied_Load = la.Total_Load;
                    la.a1_a = 0.5;
                    la.b1_b = Math.Abs(item.Distance);
                    list.Add(la);
                }
                //la = new LoadApplied();
                //la.Load_Name = "HB ";
                //la.Total_Load = 554.0;
                //la.Applied_Load = 57.0;
                //la.a1_a = 0.5;
                //la.b1_b = 19.4;
                //list.Add(la);

            }


            la = new LoadApplied();
            la.Load_Name = "USER DEFINE";
            la.Total_Load = 0.0;
            la.Applied_Load = 0.0;
            la.a1_a = 0.0;
            la.b1_b = 0.0;
            list.Add(la);
        }

        public static LoadApplied Get_Applied_Load(string load_name)
        {
            if (list == null) Load_Default();

            foreach (var item in list)
            {
                if (load_name.ToUpper() == item.Load_Name.ToUpper())
                    return item;
            }

            return new LoadApplied();
        }
        public static List<string> Get_All_LoadName(IApplication iapp)
        {
            //if (list == null) 
                Load_Default(iapp);
            List<string> loads = new List<string>();
            foreach (var item in list)
            {
                loads.Add(item.Load_Name);
            }
            return loads;
        }
    }

}
