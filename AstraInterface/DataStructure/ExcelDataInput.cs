using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
//using AstraInterface.DataStructure;

namespace AstraInterface.DataStructure
{
    class ExcelDataInput
    {
    }

    public class DataInput
    {
        public string ITEM { get; set; }
        public string DATA { get; set; }
        public string UNIT { get; set; }
        public DataInput()
        {
            ITEM = "";
            DATA = "";
            UNIT = "";
        }
        public void Load_Data_from_Grid(DataGridView dgv, int row_index)
        {

            try
            {
                ITEM = dgv[0, row_index].Value.ToString();
                DATA = dgv[1, row_index].Value.ToString();
                UNIT = dgv[2, row_index].Value.ToString();
            }
            catch (Exception exx) { }
        }
        public override string ToString()
        {
            return string.Format("{0} = {1} {2}", ITEM, DATA, UNIT);
        }

        public double ToDouble
        {
            get
            {
                return MyList.StringToDouble(DATA);
            }
        }

        public int ToInt
        {
            get
            {
                return  (int) ToDouble;
            }
        }

    }

    public class DataInputCollections : List<DataInput>
    {
        public Hashtable ht { get; set; }

        public List<string> Headings { get; set; }


        public DataInputCollections()
            : base()
        {
            ht = new Hashtable();
            Headings = new List<string>();
        }
        public void Load_Data_from_Grid(DataGridView dgv)
        {
            Clear();
            ht.Clear();
            Headings.Clear();

            //string kStr = "";
            //string val = "";
            //string input = "";

            List<DataInput> lst = new List<DataInput>();

            for (int i = 0; i < dgv.RowCount; i++)
            {
                DataInput di = new DataInput();
                di.Load_Data_from_Grid(dgv, i);

                if (di.ITEM != "" && di.DATA == "" && di.UNIT == "")
                {
                    lst = new List<DataInput>();
                    ht.Add(di.ITEM, lst);
                    Headings.Add(di.ITEM);
                }
                else
                {
                    lst.Add(di);
                    Add(di);
                }
            }
        }

        public List<DataInput> Get_Data(int index)
        {
            return ((List<DataInput>)(ht[Headings[index]]));
        }
    }

}
