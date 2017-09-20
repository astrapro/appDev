using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CTimeHistory1
    {
        public int timeSteps, printInterval;
        public double stepInterval, dampingFactor;
        public CTimeHistory1()
        {
            timeSteps = 0;
            printInterval = 0;
            stepInterval = 0.0;
            dampingFactor = 0.0;
        }
        public static CTimeHistory1 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CTimeHistory1 dataObj = new CTimeHistory1();
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
            int iCount = 4;

            dataObj.timeSteps = int.Parse(values[iCount]); iCount++;
            dataObj.printInterval = int.Parse(values[iCount]); iCount++;
            dataObj.stepInterval = double.Parse(values[iCount]); iCount++;
            dataObj.dampingFactor = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }
        public override string ToString()
        {
            string str = string.Format("N101{0,5}{1,5}{2,5}{3,8}{4,8}{5,8}{6,8}", 1, 1, 1, this.timeSteps, this.printInterval, this.stepInterval, this.dampingFactor.ToString("0.00"));
            return str;
        
        }
    }
}
