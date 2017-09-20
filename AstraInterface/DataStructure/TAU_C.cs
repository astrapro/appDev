using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AstraInterface.DataStructure
{
    //public class TAU_C
    //{
    //    public static double Get_Tau_c(double percent, CONCRETE_GRADE con_grade)
    //    {
    //        int indx = -1;
    //        percent = Double.Parse(percent.ToString("0.00"));
    //        string table_file = Path.Combine(Application.StartupPath, "TABLES");
    //        table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");

    //        List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
    //        string kStr = "";
    //        MyList mList = null;

    //        bool find = false;

    //        double a1, b1, a2, b2, returned_value;

    //        a1 = 0.0;
    //        b1 = 0.0;
    //        a2 = 0.0;
    //        b2 = 0.0;
    //        returned_value = 0.0;

    //        List<MyList> lst_list = new List<MyList>();


    //        #region Swith Case
    //        switch (con_grade)
    //        {
    //            case CONCRETE_GRADE.M15:
    //                indx = 1;
    //                break;
    //            case CONCRETE_GRADE.M20:
    //                indx = 2;
    //                break;
    //            case CONCRETE_GRADE.M25:
    //                indx = 3;
    //                break;

    //            case CONCRETE_GRADE.M30:
    //                indx = 4;
    //                break;

    //            case CONCRETE_GRADE.M35:
    //                indx = 5;
    //                break;

    //            case CONCRETE_GRADE.M40:
    //                indx = 6;
    //                break;
    //        }
    //        #endregion


    //        for (int i = 0; i < lst_content.Count; i++)
    //        {
    //            kStr = MyList.RemoveAllSpaces(lst_content[i]);
    //            mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
    //            find = ((double.TryParse(mList.StringList[0], out a2)) && (mList.Count == 7));
    //            if (find)
    //            {
    //                lst_list.Add(mList);
    //            }
    //        }

    //        for (int i = 0; i < lst_list.Count; i++)
    //        {
    //            a1 = lst_list[i].GetDouble(0);
    //            if (percent < lst_list[0].GetDouble(0))
    //            {
    //                returned_value = lst_list[0].GetDouble(indx);
    //                break;
    //            }
    //            else if (percent > (lst_list[lst_list.Count - 1].GetDouble(0)))
    //            {
    //                returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
    //                break;
    //            }

    //            if (a1 == percent)
    //            {
    //                returned_value = lst_list[i].GetDouble(indx);
    //                break;
    //            }
    //            else if (a1 > percent)
    //            {
    //                a2 = a1;
    //                b2 = lst_list[i].GetDouble(indx);

    //                a1 = lst_list[i - 1].GetDouble(0);
    //                b1 = lst_list[i - 1].GetDouble(indx);

    //                returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (percent - a1);
    //                break;
    //            }
    //        }

    //        lst_list.Clear();
    //        lst_content.Clear();


    //        returned_value = Double.Parse(returned_value.ToString("0.000"));
    //        return returned_value;
    //    }
    //}
    //public enum CONCRETE_GRADE
    //{
    //    M40 = 40,
    //    M35 = 35,
    //    M30 = 30,
    //    M25 = 25,
    //    M20 = 20,
    //    M15 = 15,
    //}
    //public enum STEEL_GRADE
    //{
    //    Fe415 = 415,
    //    //M35 = 35,
    //    //M30 = 30,
    //    //M25 = 25,
    //    //M20 = 20,
    //    //M15 = 15,
    //    //  sw.WriteLine("Concrete Grade (M)                15                    20                    25                    30                    35                    40");
    //    //        sw.WriteLine("τ co                                                      0.28                0.34                0.40                0.45                0.50                0.50");

    //}
    
}
