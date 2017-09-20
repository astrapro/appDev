using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{

    public class CTimeHistory5
    {
        public bool Tx, Ty, Tz, Rx, Ry, Rz;
        public int nodeNo;
        public CTimeHistory5()
        {
            Tx = Ty = Tz = Rx = Ry = Rz = false;
            nodeNo = 0;
        }
        public override string ToString()
        {
            string str = "N105    " + nodeNo;
            if (Tx)
                str += "    1";
            if (Ty)
                str += "    2";
            if (Tz)
                str += "    3";
            if (Rx)
                str += "    4";
            if (Ry)
                str += "    5";
            if (Rz)
                str += "    6";
            return str;
        }
        public static CTimeHistory5 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CTimeHistory5 dataObj = new CTimeHistory5();
            string[] values = strData.Split(new char[] { ' ' });

            while (strData.IndexOf("  ") != -1)
            {
                strData = strData.Replace("  ", " ");
            }
            strData = strData.Replace('\t', ' ');
            values = strData.Split(new char[] { ' ' });

            int iCount = 0;
            if (values.Length <= 2) throw new Exception();
            foreach (string str in values)
            {
                if (iCount == 1)
                    dataObj.nodeNo = int.Parse(str);
                if (iCount > 1)
                {
                    if (str == "1")
                        dataObj.Tx = true;
                    else if (str == "2")
                        dataObj.Ty = true;
                    else if (str == "3")
                        dataObj.Tz = true;
                    else if (str == "4")
                        dataObj.Rx = true;
                    else if (str == "5")
                        dataObj.Ry = true;
                    else if (str == "6")
                        dataObj.Rz = true;
                }
                iCount++;
            }
            return dataObj;
        }
    }
    public class CTimeHistory5Collection : IList<CTimeHistory5>
    {
        List<CTimeHistory5> list;
        public bool NodalConstraint;
        public CTimeHistory5Collection()
        {
            list = new List<CTimeHistory5>();
            NodalConstraint = false;
        }
        public string GetNodeNumbers()
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].nodeNo + ",";
            }
            if (str.Length > 0)
                return str.Substring(0, str.Length - 1);
            return str;
        }


        #region IList<CTimeHistory5> Members

        public int IndexOf(CTimeHistory5 item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].nodeNo == item.nodeNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, CTimeHistory5 item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public CTimeHistory5 this[int index]
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

        #region ICollection<CTimeHistory5> Members

        public void Add(CTimeHistory5 item)
        {
            list.Add(item);
        }
        public void Add(string data)
        {
            try
            {
                list.Add(CTimeHistory5.Parse(data));
            }
            catch (Exception exx) { }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CTimeHistory5 item)
        {
            foreach (CTimeHistory5 th5 in list)
            {
                if (th5.nodeNo == item.nodeNo) return true;
            }
            return false;
        }

        public void CopyTo(CTimeHistory5[] array, int arrayIndex)
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

        public bool Remove(CTimeHistory5 item)
        {
            int indx = this.IndexOf(item);
            if (indx != -1)
            {
                this.RemoveAt(indx);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<CTimeHistory5> Members

        public IEnumerator<CTimeHistory5> GetEnumerator()
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
}
