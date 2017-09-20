using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{

    public class ProcessCollection : List<ProcessData>
    {
        public ProcessCollection()
            : base()
        {

        }
    }
    public class ProcessData
    {
        public int Serial_No { get; set; }
        public bool IS_RUN { get; set; }
        public string Process_Text { get; set; }
        public string Remark { get; set; }
        public string Process_File_Name { get; set; }

        public ProcessData()
        {
            Serial_No = -1;
            IS_RUN = true;
            Process_Text = "";
            Remark = "Waiting";
            Process_File_Name = "";
        }
    }
}
