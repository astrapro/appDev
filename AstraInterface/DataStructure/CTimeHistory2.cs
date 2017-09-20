using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CTimeHistory2
    {
        public int value;
        public int groundMotion;
        public CTimeHistory2()
        {
            value = 1;
            groundMotion = 3;
        }
        public static CTimeHistory2 Parse(string strData)
        {

            string org = strData;
            CTimeHistory2 th2 = new CTimeHistory2();
            string[] values = strData.Split(new char[] { ' ' });
            if (values.Length > 2)
            {
                if (values[1] == "1")
                    th2.groundMotion = 1;
                else if (values[2] == "1")
                    th2.groundMotion = 2;
                else if (values[3] == "1")
                    th2.groundMotion = 3;
                else if (values[4] == "1")
                    th2.groundMotion = 4;
                else if (values[5] == "1")
                    th2.groundMotion = 5;
                else if (values[6] == "1")
                    th2.groundMotion = 6;
            }
            else
            {
                strData = org.Remove(0, 4);
                if (strData.Length <= 5)
                {
                    th2.groundMotion = 1;
                }
                else if (strData.Length > 5 && strData.Length <= 10)
                {
                    th2.groundMotion = 2;
                }
                else if (strData.Length > 10 && strData.Length <= 15)
                {
                    th2.groundMotion = 3;
                }
                else if (strData.Length > 15 && strData.Length <= 20)
                {
                    th2.groundMotion = 4;
                }
                else if (strData.Length > 20 && strData.Length <= 25)
                {
                    th2.groundMotion = 5;
                }
                else if (strData.Length > 25)
                {
                    th2.groundMotion = 6;
                }
            }
            return th2;
        }
        public override string ToString()
        {
            string str = "";
            switch (groundMotion)
            {
                case 1:
                    str = "N102    1    0    0    0    0    0";
                    break;
                case 2:
                    str = "N102    0    1    0    0    0    0";
                    break;
                case 3:
                    str = "N102    0    0    1    0    0    0";
                    break;
                case 4:
                    str = "N102    0    0    0    1    0    0";
                    break;
                case 5:
                    str = "N102    0    0    0    0    1    0";
                    break;
                case 6:
                    str = "N102    0    0    0    0    0    1";
                    break;
            }
            return str;
        }
    }
}
