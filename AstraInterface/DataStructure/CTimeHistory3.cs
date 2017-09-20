using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CTimeHistory3
    {
        public int xDiv;
        public double scaleFactor;
        public CTimeHistory3()
        {
            xDiv = 0;
            scaleFactor = 0;
        }
        public static CTimeHistory3 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CTimeHistory3 dataObj = new CTimeHistory3();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length != 6)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace('\t', ' ');
                values = strData.Split(new char[] { ' ' });
            }
            int iCount = 1;

            dataObj.xDiv = int.Parse(values[iCount]); iCount++;
            dataObj.scaleFactor = double.Parse(values[iCount]); iCount++;
            return dataObj;
        }

        public override string ToString()
        {
            string str = "N103    " + xDiv + "    " + scaleFactor.ToString("0.0") + " FIRST TIME FUNCTION";
            return str;
        }
    }
}
