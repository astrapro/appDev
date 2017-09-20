using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CTimeHistoryAnalysis
    {
        CTimeHistory1 th1;
        CTimeHistory2 th2;
        CTimeHistory3 th3;
        CTimeHistory4 th4;
        CTimeHistory5Collection th5;
        CTimeHistory6Collectioon th6;
        bool bIsDefault;

        public CTimeHistoryAnalysis()
        {

            th1 = new CTimeHistory1();
            th2 = new CTimeHistory2();
            th3 = new CTimeHistory3();
            th4 = new CTimeHistory4();
            th5 = new CTimeHistory5Collection();
            th6 = new CTimeHistory6Collectioon();
            bIsDefault = true;
        }
        public bool IsDefault
        {
            get
            {
                return bIsDefault;
            }
            set
            {
                bIsDefault = value;
            }
        }
        public CTimeHistory1 THist_1
        {
            get
            {
                return th1;
            }
            set
            {
                IsDefault = false;
                th1 = value;
            }
        }
        public CTimeHistory2 THist_2
        {
            get
            {
                return th2;
            }
            set
            {
                IsDefault = false;
                th2 = value;
            }
        }
        public CTimeHistory3 THist_3
        {
            get
            {
                return th3;
            }
            set
            {
                IsDefault = false;
                th3 = value;
            }
        }
        public CTimeHistory4 THist_4
        {
            get
            {
                return th4;
            }
            set
            {
                IsDefault = false;
                th4 = value;
            }
        }
        public CTimeHistory5Collection THist_5
        {
            get
            {
                return th5;
            }
            set
            {
                IsDefault = false;
                th5 = value;
            }
        }
        public CTimeHistory6Collectioon THist_6
        {
            get
            {
                return th6;
            }
            set
            {
                IsDefault = false;
                th6 = value;
            }
        }

        public string MemberNumbers()
        {
            string str = "";

            for (int i = 0; i < THist_6.Count; i++)
            {
                str += THist_6[i].memberNo + ",";
            }
            return str.Substring(0, str.Length - 1);
        }

    }
}
