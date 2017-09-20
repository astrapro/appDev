using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.Interface;
using AstraInterface.DataStructure;


namespace AstraInterface.TrussBridge
{
    public class SupportReactionData
    {
        public SupportReactionData()
        {
            JointNo = 0;
            Max_Reaction_LoadCase = 0;
            Max_Reaction = 0;
            Max_Positive_Mx = 0;
            Max_Positive_Mx_LoadCase = 0;
            Max_Negative_Mx = 0;
            Max_Negative_Mx_LoadCase = 0;
            Max_Positive_Mz = 0;
            Max_Positive_Mz_LoadCase = 0;
            Max_Negative_Mz = 0;
            Max_Negative_Mz_LoadCase = 0;
        }

        #region Properties
        public int JointNo { get; set; }
        public int Max_Reaction_LoadCase { get; set; }
        public double Max_Reaction { get; set; }
        public double Max_Positive_Mx { get; set; }
        public int Max_Positive_Mx_LoadCase { get; set; }
        public double Max_Negative_Mx { get; set; }
        public int Max_Negative_Mx_LoadCase { get; set; }

        public double Max_Positive_Mz { get; set; }
        public int Max_Positive_Mz_LoadCase { get; set; }
        public double Max_Negative_Mz { get; set; }
        public int Max_Negative_Mz_LoadCase { get; set; }

        public double Max_Mx
        {
            get
            {
                return ((Math.Abs(Max_Negative_Mx) > Math.Abs(Max_Positive_Mx)) ? Math.Abs(Max_Negative_Mx) : Math.Abs(Max_Positive_Mx));
            }
        }
        public double Max_Mz
        {
            get
            {
                return ((Math.Abs(Max_Negative_Mz) > Math.Abs(Max_Positive_Mz)) ? Math.Abs(Max_Negative_Mz) : Math.Abs(Max_Positive_Mz));
            }
        }
        #endregion
    }
    public class SupportReactionCollection : IList<SupportReactionData>
    {
        List<SupportReactionData> list = null;
        AstraInterface.Interface.IApplication iApp = null;
        public SupportReactionCollection(AstraInterface.Interface.IApplication app, string Analysis_file_name)
        {
            list = new List<SupportReactionData>();
            iApp = app;
            ReadData_from_File(Analysis_file_name);
        }

        public SupportReactionData Get_Data(int JointNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].JointNo == JointNo)
                {
                    return list[i];
                }
            }
            return null;
        }
        public void ReadData_from_File(string file_name)
        {
            if (!File.Exists(file_name)) return;
            string kStr = "";
            StreamReader sr = new StreamReader(new FileStream(file_name, FileMode.Open));
            try
            {
                //Sample Data
                //At Support=   18  For Load Case=   15  Maximum Reaction= 3.642E+002
                //At Support=   18  For Load Case=   36  Maximum +ve Mx= 1.779E+002  For Load Case=   36  Maximum -ve Mx=-1.565E+002
                //At Support=   18  For Load Case=   44  Maximum +ve Mz= 1.402E+002  For Load Case=    1  Maximum -ve Mz=-4.812E+001

                //At Support=   52  For Load Case=   34  Maximum Reaction= 4.549E+002
                //At Support=   52  For Load Case=   36  Maximum +ve Mx= 1.950E+002  For Load Case=   36  Maximum -ve Mx=-2.136E+002
                //At Support=   52  For Load Case=   33  Maximum +ve Mz= 2.471E+002  For Load Case=   14  Maximum -ve Mz=-7.351E+001
                MyList mlist = null;
                MyList mlist_2 = null;
                SupportReactionData sup_rc = null;
                int ln_no = 0; // for 3 times Read line nos

                //iApp.Progress_ON("Reading support forces... ");
                while (sr.EndOfStream == false)
                {
                    kStr = sr.ReadLine();

                    kStr = MyList.RemoveAllSpaces(kStr.ToUpper());
                    mlist = new MyList(kStr, '=');
                    if (mlist.StringList[0].StartsWith("AT SUPPORT"))
                    {
                        if (mlist.Count == 4)
                        {
                            kStr = kStr.Replace("AT SUPPORT=", " ");
                            kStr = kStr.Replace("FOR LOAD CASE=", " ");
                            kStr = kStr.Replace("MAXIMUM REACTION=", " ");

                            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

                            mlist = new MyList(kStr, ' ');

                            sup_rc = new SupportReactionData();
                            sup_rc.JointNo = mlist.GetInt(0);
                            sup_rc.Max_Reaction_LoadCase = mlist.GetInt(1);
                            sup_rc.Max_Reaction = mlist.GetDouble(2);
                            ln_no = 1; continue;
                        }

                        //At Support=   18  For Load Case=   36  Maximum +ve Mx= 1.779E+002  For Load Case=   36  Maximum -ve Mx=-1.565E+002
                        if (ln_no == 1)
                        {
                            kStr = kStr.Replace("AT SUPPORT=", " ");
                            kStr = kStr.Replace("FOR LOAD CASE=", " ");
                            kStr = kStr.Replace("MAXIMUM +VE MX=", " ");
                            kStr = kStr.Replace("MAXIMUM -VE MX=", " ");

                            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

                            mlist = new MyList(kStr, ' ');

                            //sup_rc = new SupportReaction();
                            //sup_rc.JointNo = mlist.GetInt(0);
                            sup_rc.Max_Positive_Mx_LoadCase = mlist.GetInt(1);
                            sup_rc.Max_Positive_Mx = mlist.GetDouble(2);

                            sup_rc.Max_Negative_Mx_LoadCase = mlist.GetInt(3);
                            sup_rc.Max_Negative_Mx = mlist.GetDouble(4);

                            ln_no = 2; continue;
                        }
                        if (ln_no == 2)
                        {
                            kStr = kStr.Replace("AT SUPPORT=", " ");
                            kStr = kStr.Replace("FOR LOAD CASE=", " ");
                            kStr = kStr.Replace("MAXIMUM +VE MZ=", " ");
                            kStr = kStr.Replace("MAXIMUM -VE MZ=", " ");

                            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

                            mlist = new MyList(kStr, ' ');

                            //sup_rc = new SupportReaction();
                            //sup_rc.JointNo = mlist.GetInt(0);
                            sup_rc.Max_Positive_Mz_LoadCase = mlist.GetInt(1);
                            sup_rc.Max_Positive_Mz = mlist.GetDouble(2);

                            sup_rc.Max_Negative_Mz_LoadCase = mlist.GetInt(3);
                            sup_rc.Max_Negative_Mz = mlist.GetDouble(4);
                            ln_no = 0;

                            list.Add(sup_rc);
                        }
                        //iApp.SetProgressValue((double)sr.BaseStream.Position, (double)sr.BaseStream.Length);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sr.Close();
            }
        }

        #region IList<SupportReaction> Members

        public int IndexOf(SupportReactionData item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.JointNo == list[i].JointNo)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, SupportReactionData item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SupportReactionData this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;

            }
        }

        #endregion

        #region ICollection<SupportReaction> Members

        public void Add(SupportReactionData item)
        {
            int indx = IndexOf(item);
            if (indx == -1)
            {
                list.Add(item);
            }
            else
                list[indx] = item;
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(SupportReactionData item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(SupportReactionData[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SupportReactionData item)
        {
            int indx = IndexOf(item);
            RemoveAt(indx);

            return (indx != -1);
        }

        #endregion

        #region IEnumerable<SupportReaction> Members

        public IEnumerator<SupportReactionData> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }
    public enum eSteelTrussOption
    {
        None = -1,
        //Analysis = 0,
        //LongMainGirder = 1,
        //CrossGirder = 2,
        RCCDeckSlab = 3,
        //CantileverSlab = 4,
        Abutment = 5,
        RCCPier_1 = 6,
        RCCPier_2 = 7,
        //MovingLoad = 8,
    }
}
