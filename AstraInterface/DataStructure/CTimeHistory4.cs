using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CTimeHistory4
    {
        public double[] timeValues; 
        public double[] timeFunction;
        public CTimeHistory4()
        {
            timeValues = new double[3];
            timeFunction = new double[3];
        }
        public static CTimeHistory4 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CTimeHistory4 dataObj = new CTimeHistory4();
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

            dataObj.timeValues[0] = double.Parse(values[iCount]); iCount++;
            dataObj.timeFunction[0] = double.Parse(values[iCount]); iCount++;

            dataObj.timeValues[1] = double.Parse(values[iCount]); iCount++;
            dataObj.timeFunction[1] = double.Parse(values[iCount]); iCount++;

            dataObj.timeValues[2] = double.Parse(values[iCount]); iCount++;
            dataObj.timeFunction[2] = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        public static CTimeHistory4 Parse(string timeVal,string timeFunc)
        {
            string org = timeVal;
            timeVal = timeVal.Trim();
            CTimeHistory4 dataObj = new CTimeHistory4();

            string[] values = timeVal.Split(new char[] { ',' });
            dataObj.timeValues[0] = double.Parse(values[0]);
            dataObj.timeValues[1] = double.Parse(values[1]);
            dataObj.timeValues[2] = double.Parse(values[2]);

            values = timeFunc.Split(new char[] { ',' });
            dataObj.timeFunction[0] = double.Parse(values[0]);
            dataObj.timeFunction[1] = double.Parse(values[1]);
            dataObj.timeFunction[2] = double.Parse(values[2]);
            return dataObj;
        }
        public override string ToString()
        {
            string str = string.Format("N104 {0,6} {1,6} {2,6} {3,6} {4,6} {5,6}", 
                this.timeValues[0].ToString("0.0"), this.timeFunction[0].ToString("0.0"), this.timeValues[1].ToString("0.0"), 
                this.timeFunction[1].ToString("0.0"), this.timeValues[2].ToString("0.0"), this.timeFunction[2].ToString("0.0"));
            return str;
        }
    }
}
