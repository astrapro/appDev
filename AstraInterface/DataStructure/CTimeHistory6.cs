using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CTimeHistory6
    {
        public enum Member
        {
            START = 0,
            END
        };
        public int memberNo;
        public Member member;

        public CTimeHistory6()
        {
            member = Member.START;
            memberNo = 0;
        }

        public override string ToString()
        {
            string str = "";
            if (member == Member.START)
            {
                str = "N106    " + this.memberNo + "    1    2    3    4    5    6    0";
            }
            else if (member == Member.END)
            {
                str = "N106    " + this.memberNo + "    7    8    9    10    11    12    0";
            }
            return str;
        }
        public static CTimeHistory6 Parse(string strData)
        {
            string org = strData;
            strData = strData.Trim();
            CTimeHistory6 dataObj = new CTimeHistory6();
            string[] values = strData.Split(new char[] { ' ' });

            while (strData.IndexOf("  ") != -1)
            {
                strData = strData.Replace("  ", " ");
            }
            strData = strData.Replace('\t', ' ');
            values = strData.Split(new char[] { ' ' });

            int iCount = 0;
            if (values.Length != 9)
                throw new Exception();
            foreach (string str in values)
            {
                if (iCount == 1)
                    dataObj.memberNo = int.Parse(str);
                if (iCount > 1)
                {
                    if (str == "1")
                    {
                        dataObj.member = Member.START;
                        break;
                    }
                    else if (str == "7")
                    {
                        dataObj.member = Member.END;
                        break;
                    }
                }
                iCount++;
            }
            return dataObj;
        }

    }
    public class CTimeHistory6Collectioon : IList<CTimeHistory6>
    {

        List<CTimeHistory6> list;
        public CTimeHistory6Collectioon()
        {
            list = new List<CTimeHistory6>();
        }

        #region IList<CTimeHistory6> Members

        public int IndexOf(CTimeHistory6 item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].memberNo == item.memberNo)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, CTimeHistory6 item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public CTimeHistory6 this[int index]
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

        #region ICollection<CTimeHistory6> Members

        public void Add(CTimeHistory6 item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CTimeHistory6 item)
        {
            if (IndexOf(item) == -1)
                return false;
            return true;
        }

        public void CopyTo(CTimeHistory6[] array, int arrayIndex)
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

        public bool Remove(CTimeHistory6 item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {
                RemoveAt(indx);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<CTimeHistory6> Members

        public IEnumerator<CTimeHistory6> GetEnumerator()
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
