using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace AstraInterface.DataStructure
{
    public class KValue
    {
        public KValue()
        {
        }
        public static double Get_K_Value(double B_by_L)
        {
            string fName = Application.StartupPath;
            fName = Path.Combine(Application.StartupPath, "KVALUE.TXT");
            List<string> lst_content = new List<string>(File.ReadAllLines(fName));
            string kStr = "";
            MyList mList = null;
            bool isFind = false;
            List<MyList> lst_MList = new List<MyList>();
            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                mList = new MyList(kStr, ' ');
                if (mList.StringList[0] == "0.1")
                {
                    isFind = true;
                }
                if (isFind && mList.Count == 3)
                {
                    try
                    {
                        lst_MList.Add(mList);
                    }
                    catch (Exception ex) { }
                }
            }

            #region KValues

//            Table : Values of K (IRC: 21-1987)

//                       K                                             K
//           B/L                  For Simply                                For Continous
//                  Supported Slab                        Slab

            //0.1                    0.40                            0.40
            //0.2                    0.80                            0.80
            //0.3                    1.16                            1.16
            //0.4                    1.48                            1.44
            //0.5                    1.72                            1.68
            //0.6                    1.96                             1.84
            //0.7       2.12                             1.96
            //0.8                    2.24                            2.08
            //0.9                    2.36                            2.16
            //1.0                    2.48                             2.24
            //1.1                    2.60                             2.28
            //1.2                    2.64                              2.36
            //1.3                    2.72                             2.40
            //1.4                    2.80                             2.48
            //1.5                    2.84                             2.48
            //1.6                    2.88                2.52
            //1.7                    2.92                             2.56
            //1.8                    2.96                             2.60
            //1.9                    3.00                             2.60
            //2.0                    3.00                2.60
            #endregion
            double K = 0.0;
            for (int i = 0; i < lst_MList.Count; i++)
            {
                if (B_by_L < lst_MList[0].GetDouble(0))
                {
                    K = lst_MList[0].GetDouble(1);
                    break;
                }
                else if (B_by_L > lst_MList[lst_MList.Count-1].GetDouble(0))
                {
                    K = lst_MList[lst_MList.Count - 1].GetDouble(1);
                    break;
                }

                if (i < lst_MList.Count - 1)
                {
                    if (B_by_L >= lst_MList[i].GetDouble(0) && 
                        B_by_L <= lst_MList[i+1].GetDouble(0))
                    {
                        K = lst_MList[i + 1].GetDouble(1);
                        break;
                    }
                }
            }

            return K;
        }
    }
}
