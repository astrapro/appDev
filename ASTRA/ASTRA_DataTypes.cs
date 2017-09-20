using System;
using System.Collections.Generic;
using System.Text;
/*
namespace AstraFunctionOne
{
    class Analysis
    {
        public string kNo ;
        public int staticAnalysis;
        public int statLoadCase;
        public int dynamicAnalysis;
        public int frequencyNo;
        public int eigenvalue;
        public int force;
        public int response;
        public int direct;

        public Analysis()
        {
            kNo = "N005";
            staticAnalysis = 0;
            statLoadCase = 0;
            dynamicAnalysis = 0;
            frequencyNo = 0;
            eigenvalue = 0;
            force = 0;
            response = 0;
            direct = 0;
        }
        public static Analysis Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            Analysis dataObj = new Analysis();
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
            int iCount = 0;
            dataObj.kNo = values[iCount]; iCount++;
            dataObj.staticAnalysis = int.Parse(values[iCount]); iCount++;
            dataObj.statLoadCase = int.Parse(values[iCount]); iCount++;
            dataObj.dynamicAnalysis = int.Parse(values[iCount]); iCount++;
            dataObj.frequencyNo = int.Parse(values[iCount]); iCount++;
            dataObj.eigenvalue = int.Parse(values[iCount]); iCount++;
            dataObj.force = int.Parse(values[iCount]); iCount++;
            dataObj.response = int.Parse(values[iCount]); iCount++;
            dataObj.direct = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        public override string ToString()
        {
            string str ="" + this.kNo + '\t' + this.staticAnalysis + '\t' + this.statLoadCase + '\t' + this.dynamicAnalysis + '\t'
                + this.frequencyNo + '\t' + this.eigenvalue + '\t' + this.force + '\t' + this.response + '\t'
                + this.direct + "";
            return str;
        }
    }
    class NodalData
    {
        public string kNo;
        public int nodeNo;
        public double lengthFact;
        public int globalCartesian;
        public int globalCylindrical;
        public double X, Y, Z;
        public int Tx, Ty, Tz, Rx, Ry, Rz;
        public int clnX, clnY, clnZ;
        public NodalData()
        {
            kNo = "N001";
            nodeNo = 0;
            lengthFact = 0.00;
            globalCartesian = 0;
            globalCylindrical = 0;
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            Tx = Ty = Tz = Rx = Ry = Rz = 0;
            clnX = clnY = clnZ = 0;
        }
        public static NodalData Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            NodalData dataObj = new NodalData();
            string[] values = strData.Split(new char[] { '\t' });

            if (values.Length != 17)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 0;
            dataObj.kNo = values[iCount]; iCount++;
            dataObj.nodeNo = int.Parse(values[iCount]); iCount++;
            dataObj.lengthFact = double.Parse(values[iCount]); iCount++;
            dataObj.globalCartesian = int.Parse(values[iCount]); iCount++;
            dataObj.globalCylindrical = int.Parse(values[iCount]); iCount++;
            dataObj.X = double.Parse(values[iCount]); iCount++;
            dataObj.Y = double.Parse(values[iCount]); iCount++;
            dataObj.Z = double.Parse(values[iCount]); iCount++;
            dataObj.Tx = int.Parse(values[iCount]); iCount++;
            dataObj.Ty = int.Parse(values[iCount]); iCount++;
            dataObj.Tz = int.Parse(values[iCount]); iCount++;
            dataObj.Rx = int.Parse(values[iCount]); iCount++;
            dataObj.Ry = int.Parse(values[iCount]); iCount++;
            dataObj.Rz = int.Parse(values[iCount]); iCount++;
            dataObj.clnX = int.Parse(values[iCount]); iCount++;
            dataObj.clnY = int.Parse(values[iCount]); iCount++;
            dataObj.clnZ = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        public override string ToString()
        {
            string str = "" + this.kNo + '\t' + this.nodeNo + '\t' + this.lengthFact + '\t' + this.globalCartesian + '\t'
                + this.globalCylindrical + '\t' + this.X + '\t' + this.Y + '\t' + this.Z + '\t' +
                this.Tx + '\t' + this.Ty + '\t' + this.Tz + '\t' + this.Rx + '\t' + this.Ry + '\t'
                + this.Rz + '\t' + this.clnX + '\t' + this.clnY + '\t' + this.clnZ + "";
            return str;
        }
    }
    class TrussMaterial
    {
        public string kNo;
        public int propertyId;
        public double massFact;
        public double lengthFact;
        public double elasticity;
        public double poissonRatio;
        public double thermal;
        public double mass;
        public double area;
        public double weight;
        public int allElements;
        public int listedElement;
        public string list;

        public TrussMaterial()
        {
            kNo = "N004";
            propertyId = 0;
            massFact = 0.00;
            lengthFact = 0.0;
            elasticity = 0.00;
            poissonRatio = 0.15;
            thermal = 0.0;
            mass = 0.00;
            area = 0.0;
            weight = 0.00;
            allElements = 0;
            listedElement = 0;
            list = "";
        }
        public static TrussMaterial Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            TrussMaterial dataObj = new TrussMaterial();
            string[] values = strData.Split(new char[] { '\t' });

            if (values.Length != 13)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                values = strData.Split(new char[] { '\t' });
            }
            int iCount = 0;
            dataObj.kNo = values[iCount]; iCount++;
            dataObj.propertyId = int.Parse(values[iCount]); iCount++;
            dataObj.massFact = double.Parse(values[iCount]); iCount++;
            dataObj.lengthFact = double.Parse(values[iCount]); iCount++;
            dataObj.elasticity = double.Parse(values[iCount]); iCount++;
            dataObj.poissonRatio = double.Parse(values[iCount]); iCount++;
            dataObj.thermal = double.Parse(values[iCount]); iCount++;
            dataObj.mass = double.Parse(values[iCount]); iCount++;
            dataObj.area = double.Parse(values[iCount]); iCount++;
            dataObj.weight = double.Parse(values[iCount]); iCount++;
            dataObj.allElements = int.Parse(values[iCount]); iCount++;
            dataObj.listedElement = int.Parse(values[iCount]); iCount++;
            try
            {
                dataObj.list = values[iCount]; iCount++;
            }
            catch (Exception ex)
            {
                dataObj.list = "";
            }
            return dataObj;
        }
        public override string ToString()
        {
            string str = this.kNo + '\t' + this.propertyId.ToString() + '\t' + this.massFact + '\t' + this.lengthFact + '\t' + this.elasticity
              + '\t' + this.poissonRatio + '\t' + this.thermal + '\t' + this.mass + '\t' + this.area + '\t' + this.weight + '\t' +
                this.allElements + '\t' + this.listedElement + '\t' + this.list;
            return str;
        }
    }
    class SelfWeight
    {
        public string kNo;
        public string loadFactor;
        public double xFact;
        public double yFact;
        public double zFact;
        public double tFact;

        public SelfWeight()
        {
            kNo = "N006";
            loadFactor = "";
            xFact = 0.000;
            yFact = 0.00;
            zFact = 0.00;
            tFact = 0.00;
        }
        public static SelfWeight Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            SelfWeight dataObj = new SelfWeight();
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
            int iCount = 0;
            dataObj.kNo = values[iCount]; iCount++;
            dataObj.loadFactor = values[iCount]; iCount++;
            dataObj.xFact = double.Parse(values[iCount]); iCount++;
            dataObj.yFact = double.Parse(values[iCount]); iCount++;
            dataObj.zFact = double.Parse(values[iCount]); iCount++;
            dataObj.tFact = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }
        public override string ToString()
        {
            string str = this.kNo + '\t' + this.loadFactor + '\t' + this.xFact + '\t' + this.yFact
               + '\t' + this.zFact + '\t' + this.tFact;
            return str;
        }

    }
    class ElementConnectivity
    {
        public string kNo;
        public int elNo;
        public int startNodeNo;
        public int endNodeNo;
        public int elementGeneration;
        public int refTemp;

      
        public ElementConnectivity()
        {
            kNo = "N002";
            elNo = 0;
            startNodeNo = 0;
            endNodeNo = 0;
            elementGeneration = 0;
            refTemp = 0;

        }

        public static  ElementConnectivity Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            ElementConnectivity dataObj = new ElementConnectivity();
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
            int iCount = 0;
            dataObj.kNo = values[iCount]; iCount++;
            dataObj.elNo = int.Parse(values[iCount]); iCount++;
            dataObj.startNodeNo = int.Parse(values[iCount]); iCount++;
            dataObj.endNodeNo = int.Parse(values[iCount]); iCount++;
            dataObj.elementGeneration = int.Parse(values[iCount]); iCount++;
            dataObj.refTemp = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        public override string ToString()
        {
            string str = this.kNo + '\t' + this.elNo.ToString() + '\t' + this.startNodeNo.ToString() + '\t' + this.endNodeNo +'\t' + 
                this.elementGeneration.ToString() + '\t' + this.refTemp;
            return str;
        }
    }

    class N001
    {
        public string nName;
        public int Node;
        public double xNode;
        public double yNode;
        public double zNode;
        public int Tx;
        public int Ty;
        public int Tz;
        public int Rx;
        public int Ry;
        public int Rz;

        public N001() 
        {
            nName = "N001";
            Node = 0;
            xNode = 0.0;
            yNode = 0.0;
            zNode = 0.0;
            Tx = Ty = Tz = Rx = Ry = Rz = 0;
        }

        public static N001 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N001 dataObj = new N001();
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
            dataObj.Node = int.Parse(values[iCount]); iCount++;
            dataObj.xNode = double.Parse(values[iCount]); iCount++;
            dataObj.yNode = double.Parse(values[iCount]); iCount++;
            dataObj.zNode = double.Parse(values[iCount]); iCount++;
            dataObj.Tx = int.Parse(values[iCount]); iCount++;
            dataObj.Ty = int.Parse(values[iCount]); iCount++;
            dataObj.Tz = int.Parse(values[iCount]); iCount++;
            dataObj.Rx = int.Parse(values[iCount]); iCount++;
            dataObj.Ry = int.Parse(values[iCount]); iCount++;
            dataObj.Rz = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        public override string ToString()
        {
            const char t = '\t';
            string str = this.nName + t + this.Node + t + this.xNode + t + this.yNode + t + this.zNode + t + this.Tx + t + 
                this.Ty + t + this.Tz + t + this.Rx + t + this.Ry + t + this.Rz + "";

            return str;
        }

        public void setN001(NodalData nd)
        {
            this.Node = nd.nodeNo;
            this.xNode = nd.X;
            this.yNode = nd.Y;
            this.zNode = nd.Z;
            this.Tx = nd.Tx;
            this.Ty = nd.Ty;
            this.Tz = nd.Tz;
            this.Rx = nd.Rx;
            this.Ry = nd.Ry;
            this.Rz = nd.Rz;
        }
    }

    class N002
    {
        public string nName;
        public int elementType;
        public int beamNo;
        public int node1,node2;
        public double xNode1, yNode1, zNode1, xNode2, yNode2, zNode2;

        public N002()
        {
            nName = "N002";
            elementType = 2;
            beamNo = 0;
            node1 = 0;
            xNode1 = 0.0;
            yNode1 = 0.0;
            zNode1 = 0.0;
            node2 = 0;
            xNode2 = 0.0;
            yNode2 = 0.0;
            zNode2 = 0.0;
        }
        public static N002 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N002 dataObj = new N002();
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

            dataObj.elementType = int.Parse(values[iCount]); iCount++;
            dataObj.beamNo = int.Parse(values[iCount]); iCount++;
            dataObj.node1 = int.Parse(values[iCount]); iCount++;
            dataObj.xNode1 = double.Parse(values[iCount]); iCount++;
            dataObj.yNode1 = double.Parse(values[iCount]); iCount++;
            dataObj.zNode1 = double.Parse(values[iCount]); iCount++;
        
            dataObj.node2 = int.Parse(values[iCount]); iCount++;
            dataObj.xNode2 = double.Parse(values[iCount]); iCount++;
            dataObj.yNode2 = double.Parse(values[iCount]); iCount++;
            dataObj.zNode2 = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }

        public override string ToString()
        {
            const char t = '\t';
            string str = this.nName + t + this.elementType + t + this.beamNo + t + this.node1 + t + this.xNode1 + t + this.yNode1 + t +
                this.zNode1 + t + this.node2 + t + this.xNode2 + t + this.yNode2 + t + this.zNode2 + t + "";
            return str;
        }
    }

    class N003
    {
        public string nName;
        public int element;
        public int sectionId;
        public double area;
        public double ix;
        public double iy;
        public double iz;
        public double width;
        public double depth;
        public double outerDiameter;
        public double innerDiameter;

        public N003()
        {
            nName = "N003";
            element = 0;
            sectionId = 0;
            area = 0.0;
            ix = 0.0;
            iy = 0.0;
            iz = 0.0;
            width = 0.0;
            depth = 0.0;
            outerDiameter = 0.0;
            innerDiameter = 0.0;

        }

        public static N003 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N003 dataObj = new N003();
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

            dataObj.element = int.Parse(values[iCount]); iCount++;
            dataObj.sectionId = int.Parse(values[iCount]); iCount++;
            dataObj.width = double.Parse(values[iCount]); iCount++;
            dataObj.depth = double.Parse(values[iCount]); iCount++;
            dataObj.outerDiameter = double.Parse(values[iCount]); iCount++;
            dataObj.innerDiameter = double.Parse(values[iCount]); iCount++;

            dataObj.area = double.Parse(values[iCount]); iCount++;
            dataObj.ix = double.Parse(values[iCount]); iCount++;
            dataObj.iy = double.Parse(values[iCount]); iCount++;
            dataObj.iz = double.Parse(values[iCount]); iCount++;
            
            return dataObj;
        }

        public override string ToString()
        {

            const char t = '\t';
            string str = this.nName + t + this.element + t + this.sectionId + t + this.width + t + this.depth + t +
                this.outerDiameter + t + this.innerDiameter + t + this.area + t + this.ix + t + this.iy + t + this.iz + "";

            return str;
        }
        
    }

    class N004
    {
        public string nName;
        public int element;
        public int mat_id;
        public double emod;
        public double pr;
        public double mden;
        public double wden;
        public double alpha;
        public double beta;
      
        public N004()
        {
            nName = "N004";
            element = 0;
            mat_id = 0;
            emod = 0.0;
            pr = 0.150;
            mden = 0.0;
            wden = 0.0;
            alpha = 0.0;
            beta = 0.0;
        }
        public static N004 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N004 dataObj = new N004();
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

            dataObj.element = int.Parse(values[iCount]); iCount++;
            dataObj.mat_id = int.Parse(values[iCount]); iCount++;

            dataObj.emod = double.Parse(values[iCount]); iCount++;
            dataObj.pr = double.Parse(values[iCount]); iCount++;
            dataObj.mden = double.Parse(values[iCount]); iCount++;
            dataObj.wden = double.Parse(values[iCount]); iCount++;
            dataObj.alpha = double.Parse(values[iCount]); iCount++;
            dataObj.beta = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }

        public override string ToString()
        {
            const char t = '\t';

            string str = this.nName + t + this.element + t + this.mat_id + t + this.emod + t + this.pr + t + this.mden + t +
                this.wden + t + this.alpha + t + this.beta + "";

            return str;
        }

        public static N004 Parse(TrussMaterial tmt)
        {
            N004 n4 = new N004();
            n4.mat_id = tmt.propertyId;
            n4.emod = tmt.elasticity;
            n4.pr = tmt.poissonRatio;
            n4.mden = tmt.mass;
            n4.wden = tmt.weight;
            n4.alpha = tmt.thermal;
            n4.beta = tmt.thermal;

            return n4;
        }
    }

    class N005
    {
        public string nName;
        public int element;
        public int dof0;
        public int dof1;
        public int dof2;
        public int dof3;
        public int dof4;
        public int dof5;

        public N005()
        {
            nName = "N005";
            element = 0;
            dof0 = 0;
            dof1 = 0;
            dof2 = 0;
            dof3 = 0;
            dof4 = 0;
            dof5 = 0;
        }
        public static N005 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N005 dataObj = new N005();
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
            dataObj.dof0 = int.Parse(values[iCount]); iCount++;
            dataObj.dof1 = int.Parse(values[iCount]); iCount++;
            dataObj.dof2 = int.Parse(values[iCount]); iCount++;
            dataObj.dof3 = int.Parse(values[iCount]); iCount++;
            dataObj.dof4 = int.Parse(values[iCount]); iCount++;
            dataObj.dof5 = int.Parse(values[iCount]); iCount++;

            return dataObj;
        }

        public override string ToString()
        {
            const char t = '\t';
            string str = this.nName + t + this.element + t + this.dof0 + t + this.dof1 + t + this.dof2 + t + this.dof3 + t +
                this.dof4 + t + this.dof5 + "";

            return str;
        }
        
    }

    class N006
    {
        public string nName;
        public string mark;
        public int loadcase;
        public int total;
        public int eltype;
        public int element;
        public double udlx;
        public double udly;
        public double udlz;
        public double px;
        public double d1;
        public double py;
        public double d2;
        public double pz;
        public double d3;


        public N006()
        {
            nName = "N006";
            mark = "";
            loadcase = 0;
            total = 0;
            eltype = 2;
            element = 0;
            udlx = 0.0;
            udly = 0.0;
            udlz = 0.0;
            
            px = 0.0;
            d1 = 0.0;
            py = 0.0;
            d2 = 0.0;
            pz = 0.0;
            d3 = 0.0;

        }

        public static N006 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N006 dataObj = new N006();
            string[] values = strData.Split(new char[] { '\t' });

            if (values.Length != 14)
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
            dataObj.mark = values[iCount]; iCount++;
            dataObj.total = int.Parse(values[iCount]); iCount++;
            dataObj.element = int.Parse(values[iCount]); iCount++;
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

        public override string ToString()
        {
            string str = this.nName + '\t' + this.loadcase + '\t' + this.mark + '\t' + this.total + '\t' + this.eltype + '\t' + this.element + '\t' + this.udlx + '\t' + this.udly + '\t' + this.udlz + '\t' + this.px + '\t' + this.d1 + '\t' + this.py + '\t' + this.d2 + '\t' + this.pz + '\t' + this.d3 + "";
            return str;

        }

    }

    class N007
    {
        public string nName;
        public int loadcase;
        public int node;
        public double Fx;
        public double Fy;
        public double Fz;
        public double Mx;
        public double My;
        public double Mz;

        public N007()
        {
            nName = "N007";
            loadcase = 0;
            node = 0;
            Fx = 0.00;
            Fy = 0.00;
            Fz = 0.00;
            Mx = 0.00;
            My = 0.00;
            Mz = 0.00;
        }

        public static N007 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N007 dataObj = new N007();
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

            dataObj.loadcase = int.Parse(values[iCount]); iCount++;
            dataObj.node = int.Parse(values[iCount]); iCount++;

            dataObj.Fx = double.Parse(values[iCount]); iCount++;
            dataObj.Fy = double.Parse(values[iCount]); iCount++;
            dataObj.Fz = double.Parse(values[iCount]); iCount++;
            dataObj.Mx = double.Parse(values[iCount]); iCount++;
            dataObj.My = double.Parse(values[iCount]); iCount++;
            dataObj.Mz = double.Parse(values[iCount]); iCount++;

            return dataObj;
        }

        public override string ToString()
        {
            string str = this.nName + '\t' + this.loadcase + '\t' + this.node + '\t' + this.Fx + '\t' + this.Fy + '\t' + this.Fz + '\t' +
                this.Mx + '\t' + this.My + '\t' + this.Mz + "";

            return str;
        }

    }

    class N008
    {
        public string nName;
        public int elementType;
        public int TRUSS_MEMB;

        public N008()
        {
            nName = "N008";
            elementType = 1;
            TRUSS_MEMB = 0;
        }
        public static N008 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N008 dataObj = new N008();
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
            int iCount = 1;

            dataObj.elementType = int.Parse(values[iCount]); iCount++;
            dataObj.TRUSS_MEMB = int.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        public override string ToString()
        {
            string str = this.nName + '\t' + this.elementType + '\t' + this.TRUSS_MEMB + "";
            return str;
        }
    }

    class N009
    {
        public string nName;
        public int element;
        public string N1;
        public int FX1;
        public int FY1;
        public int FZ1;
        public int MX1;
        public int MY1;
        public int MZ1;
        public string N2;
        public int FX2;
        public int FY2;
        public int FZ2;
        public int MX2;
        public int MY2;
        public int MZ2;
        
        public N009()
        {
            nName = "N009";
            element = 0;
            N1 = "N1";
            FX1 = 0;
            FY1 = 0;
            FZ1 = 0;
            MX1 = 0;
            MY1 = 0;
            MZ1 = 0;
            N2 = "N2";
            FX2 = 0;
            FX2 = 0;
            FX2 = 0;
            MX2 = 0;
            MY2 = 0;
            MZ2 = 0;
        }
        public static N009 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N009 dataObj = new N009();
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

            dataObj.element = int.Parse(values[iCount]); iCount++;
            dataObj.N1 = values[iCount]; iCount++;
            dataObj.FX1 = int.Parse(values[iCount]); iCount++;
            dataObj.FY1 = int.Parse(values[iCount]); iCount++;
            dataObj.FZ1 = int.Parse(values[iCount]); iCount++;
            
            dataObj.MX1 = int.Parse(values[iCount]); iCount++;
            dataObj.MY1 = int.Parse(values[iCount]); iCount++;
            dataObj.MZ1 = int.Parse(values[iCount]); iCount++;

            dataObj.N2 = values[iCount]; iCount++;
            dataObj.FX2 = int.Parse(values[iCount]); iCount++;
            dataObj.FY2 = int.Parse(values[iCount]); iCount++;
            dataObj.FZ2 = int.Parse(values[iCount]); iCount++;

            dataObj.MX2 = int.Parse(values[iCount]); iCount++;
            dataObj.MY2 = int.Parse(values[iCount]); iCount++;
            dataObj.MZ2 = int.Parse(values[iCount]); iCount++;
           
            return dataObj;
        }
        public override string ToString()
        {
            const char t = '\t';
            string str = this.nName + t + this.element + t + this.N1 + t + this.FX1 + t + this.FY1 + t + this.FZ1 + t + this.MX1 + t +
                this.MY1 + t + this.MZ1 + t + this.N2 + t + this.FX2 + t + this.FY2 + t + this.FZ2 + t + this.MX2 + t + this.MY2 + t + this.MZ2 + "";
            return str;
        }

    }

    class N010
    {
        public string nName;
        public int loadCase;
        public double selfWeightX;
        public double selfWeightY;
        public double selfWeightZ;

        public N010()
        {
            nName = "N010";
            loadCase = 0;
            selfWeightX = 0.0;
            selfWeightY = 0.0;
            selfWeightZ = 0.0;
        }
        public static N010 Parse(SelfWeight slfWt)
        {
            N010 n10 = new N010();
            if (slfWt.loadFactor == "Load Case A")
                n10.loadCase = 0;
            else if (slfWt.loadFactor == "Load Case B")
                n10.loadCase = 1;
            else if (slfWt.loadFactor == "Load Case C")
                n10.loadCase = 2;
            else if (slfWt.loadFactor == "Load Case D")
                n10.loadCase = 3;
            n10.selfWeightX = slfWt.xFact;
            n10.selfWeightY = slfWt.yFact;
            n10.selfWeightZ = slfWt.zFact;
            return n10;
           
        }
        public static N010 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N010 dataObj = new N010();
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

            dataObj.loadCase = int.Parse(values[iCount]); iCount++;
            dataObj.selfWeightX = double.Parse(values[iCount]); iCount++;
            dataObj.selfWeightY = double.Parse(values[iCount]); iCount++;
            dataObj.selfWeightZ = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        public override string ToString()
        {
            string str = this.nName + '\t' + this.loadCase + '\t' + this.selfWeightX + '\t' + this.selfWeightY + '\t' + this.selfWeightZ + "";
            return str;
        }


    }

    class N099
    {
        public string nName;
        public string nType;
        public int nDyn_ID;
        public string NN;
        public int NF;
        public int nFValue;

        public N099()
        {
            nName = "N099";
            nType = "NDYN";
            NN = "NF";
            nDyn_ID = 0;
            NF = 0;
            nFValue = 0;
        }


        public static N099 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();

            N099 dataObj = new N099();
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

            dataObj.nType = values[iCount]; iCount++;
            dataObj.nDyn_ID = int.Parse(values[iCount]); iCount++;
            dataObj.NF = int.Parse(values[iCount]); iCount++;
            dataObj.nFValue= int.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        public override string ToString()
        {
            string str = this.nName + '\t' + this.nType + '\t' + this.nDyn_ID + '\t' + this.NN + '\t' + this.NF + "";
            return str;
        }


    }

    class FileInputOutput
    {
        int i, iCount, iIndex;
        public FileInputOutput() 
        {
            i = 0;
            iCount = 0;
            iIndex = 0;
        }

        public void ReadFromFile(string FilePath)
        {
            GlobalVars.n1.Clear();
            GlobalVars.n2.Clear();
            GlobalVars.n3.Clear();
            GlobalVars.n4.Clear();
            GlobalVars.n6.Clear();
            GlobalVars.n7.Clear();
            GlobalVars.n9.Clear();

            string find = "",work = "";
            bool setTitle = false;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                while (sr.EndOfStream == false)
                {
                    work = sr.ReadLine();

                    try
                    {
                        GlobalVars.astUnt.setUnit(work);
                    }
                    catch (Exception ex) { }
                    
                    if (setTitle == false)
                    {
                        GlobalVars.projTitle = work;
                        setTitle = true;
                        continue;
                    }
                    find = getNodeName(work);

                    if (work.Contains("STRUCTURE"))
                    {
                        work = work.Trim();
                        work = work.Substring(9, 2);
                        GlobalVars.structuteType = int.Parse(work);
                        continue;
                    }
                    if (find == "N001")
                    {
                        GlobalVars.n1.Add(N001.Parse(work));
                        //GlobalVars.ndData.Add(NodalData.Parse(work));
                    }
                    else if (find == "N002")
                    {
                        //GlobalVars.elCnt.Add(ElementConnectivity.Parse(work));
                        GlobalVars.n2.Add(N002.Parse(work));
                    }

                    else if (find == "N003")
                    {
                        GlobalVars.n3.Add(N003.Parse(work));
                    }
                   
                    else if (find == "N004")
                    {
                        GlobalVars.n4.Add(N004.Parse(work));
                    }
                    else if (find == "N005")
                    {
                        GlobalVars.anal = Analysis.Parse(work);
                    }
                    else if (find == "N006")
                    {
                        GlobalVars.n6.Add(N006.Parse(work));
                    }
                    else if (find == "N007")
                    {
                        GlobalVars.n7.Add(N007.Parse(work)); 
                    }
                    else if (find == "N008")
                    {
                        GlobalVars.n8.Add(N008.Parse(work)); 
                    }
                    else if (find == "N009")
                    {
                        GlobalVars.n9.Add(N009.Parse(work)); 
                    }
                    else if (find == "N010")
                    {
                        GlobalVars.n10 = N010.Parse(work);
                    }
                    else if (find == "N099")
                    {
                        GlobalVars.n99 = N099.Parse(work);
                    }
    
                }
            }
            catch (Exception ex)
            {
            }
        }

        public string getNodeName(string strData)
        {
            int i = 0;
            string str = "";
            if (strData.Contains("\t"))
            {
                i = strData.IndexOf('\t');
                str = strData.Substring(0, i);
            }
            return str;

        }

        public void writeToFile(string FilePath)
        {
            if (GlobalVars.projTitle.Contains("ASTRA"))
            {
                GlobalVars.projTitle = GlobalVars.projTitle + "  ";
                GlobalVars.projTitle = GlobalVars.projTitle.Insert(6, GlobalVars.structureName + " ");
            }
            else
            {
                GlobalVars.projTitle = "ASTRA " + GlobalVars.structureName + " " + GlobalVars.projTitle;

            }
            try
            {
                System.IO.File.Delete(FilePath);

                System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

                sw.WriteLine(GlobalVars.projTitle);
                sw.WriteLine("STRUCTURE " + GlobalVars.structuteType);

                sw.WriteLine("N001 UNIT " + GlobalVars.astUnt.n001_wfact + " " + GlobalVars.astUnt.n001_lfact + GlobalVars.n_1);

                for (i = 0; i < GlobalVars.n1.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n1[i].ToString());
                }

                sw.WriteLine("N002 UNIT" + GlobalVars.astUnt.n002_wfact.ToString() + " " + GlobalVars.astUnt.n002_lfact.ToString() + GlobalVars.n_2);
                for (i = 0; i < GlobalVars.n2.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n2[i].ToString());
                }

                sw.WriteLine("N003 UNIT" + GlobalVars.astUnt.n003_wfact.ToString() + " " + GlobalVars.astUnt.n003_lfact.ToString() + GlobalVars.n_3);
                for (i = 0; i < GlobalVars.n3.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n3[i].ToString());
                }
                sw.WriteLine("N004 UNIT" + GlobalVars.astUnt.n004_wfact.ToString() + " " + GlobalVars.astUnt.n004_lfact.ToString() + GlobalVars.n_4);
                for (i = 0; i < GlobalVars.n4.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n4[i].ToString());
                }

                sw.WriteLine("N008 ELTYPE=1, TRUSS_MEMB #");
                for (i = 0; i < GlobalVars.n8.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n8[i].ToString());
                }

                sw.WriteLine("N009 element#, N1# FX FY FZ MX MY MZ N2# FX FY FZ MX MY MZ");
                for (i = 0; i < GlobalVars.n9.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n9[i].ToString());
                }


                sw.WriteLine("N010 loadcase#, SELFWEIGHTX SELFWEIGHTXY SELFWEIGHTZ");
                sw.WriteLine(GlobalVars.n10.ToString());

                sw.WriteLine("N007 UNIT " +  GlobalVars.astUnt.n007_wfact + "   " + GlobalVars.astUnt.n007_lfact +" loadcase#, NODE#, fx*wfact, fy*wfact, fz*wfact, mx*wfact*lfact,  my*wfact*lfact,  mz*wfact*lfact");
                for (i = 0; i < GlobalVars.n7.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n7[i].ToString());
                }

                sw.WriteLine("N006 UNIT " + GlobalVars.astUnt.n006_wfact + "  " + GlobalVars.astUnt.n006_lfact + " loadcase, total, eltype, nl[0]+ii, udlx*wfact/lfact, udly*wfact/lfact, udlz*wfact/lfact, px*wfact, d1*lfact, py*wfact, d2*lfact, pz*wfact, d3*lfact");
                for (i = 0; i < GlobalVars.n6.Count; i++)
                {
                    sw.WriteLine(GlobalVars.n6[i].ToString());
                }
                sw.WriteLine(GlobalVars.n_99);
                sw.WriteLine(GlobalVars.n99.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }

    class AstraUnits
    {
        public double n001_lfact;
        public double n001_wfact;
        public double n002_lfact;
        public double n002_wfact;
        public double n003_lfact;
        public double n003_wfact;

        public double n004_lfact;
        public double n004_wfact;

        public double n007_lfact;
        public double n007_wfact;

        public double n006_lfact;
        public double n006_wfact;


        public AstraUnits()
        {
            n001_lfact = 1.0;
            n001_wfact = 0.0;
            n002_lfact = 1.0;
            n002_wfact = 1.0;
            n003_lfact = 1.0;
            n003_wfact = 1.0;
            n004_lfact = 1.0;
            n004_wfact = 1.0;

            n006_lfact = 1.0;
            n006_wfact = 1.0;
            n007_lfact = 1.0;
            n007_wfact = 1.0;
        
        }
        public double[] getUnits(string strData)
        {
            int ic = 0;
            string work = "";
            string mStr = "";
            ic = strData.IndexOf(" ");
            work = strData.Substring(0, ic);

            double[] d = new double[2];
            mStr = strData.Substring(ic + 1, strData.Length - (ic + 1));
            ic = mStr.IndexOf(" ");
            work = mStr.Substring(0, ic);
            mStr = mStr.Substring(ic + 1, mStr.Length - (ic + 1));
            ic = mStr.IndexOf(" ");
            work = mStr.Substring(0, ic);
            mStr = mStr.Substring(ic + 1, mStr.Length - (ic + 1));
            d[0] = GlobalVars.getDouble(work);
            ic = mStr.IndexOf(" ");
            work = mStr.Substring(0, ic);
            mStr = mStr.Substring(ic + 1, mStr.Length - (ic + 1));
            d[1] = GlobalVars.getDouble(work);
            return d;
        }
        public void setUnit(string strData)
        {
            int ic = 0;
            string work = "";
            //string mStr = "";
            ic = strData.IndexOf(" ");
            work = strData.Substring(0, ic);
            double[] d = new double[2];

            try
            {
                if (work == "N001")
                {
                    d = getUnits(strData);
                    n001_wfact = d[0];
                    n001_lfact = d[1];
                }
                if (work == "N002")
                {

                    d = getUnits(strData);
                    n002_wfact = d[0];
                    n002_lfact = d[1];
                }
                if (work == "N003")
                {
                    d = getUnits(strData);
                    n003_wfact = d[0];
                    n003_lfact = d[1];
               
                }
                if (work == "N004")
                {
                    d = getUnits(strData);
                    n004_wfact = d[0];
                    n004_lfact = d[1];
                }
                if (work == "N006")
                {
                    d = getUnits(strData);
                    n006_wfact = d[0];
                    n006_lfact = d[1];
                }
                if (work == "N007")
                {
                    d = getUnits(strData);
                    n007_wfact = d[0];
                    n007_lfact = d[1];
                }

            }
            catch (Exception ex) { }

        }
    }

}
*/
