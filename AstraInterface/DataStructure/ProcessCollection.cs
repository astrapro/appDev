using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{

    public class ProcessCollection : List<ProcessData>
    {
        public bool IsAst006 { get; set; }
        public bool IsOrtho { get; set; }
        public string AstEXE { get; set; }
        public string EMod_File { get; set; }
        public ProcessCollection()
            : base()
        {
            IsOrtho = false;
            IsAst006 = false;
            AstEXE = "";
        }
    }
    public class ProcessData
    {
        public int Serial_No { get; set; }
        public bool IS_RUN { get; set; }
        public string Process_Text { get; set; }
        public string Remark { get; set; }
        public string Process_File_Name { get; set; }
        public string Stage_File_Name { get; set; }
        public string Skew_File_Name { get; set; }
        public bool IS_Stage_File { get; set; }
        public bool IS_Skew_File { get; set; }
        public bool IS_Orthotropic { get; set; }

        public ProcessData()
        {
            Serial_No = -1;
            IS_RUN = true;
            Process_Text = "";
            Remark = "Waiting";
            Process_File_Name = "";

            IS_Stage_File = false;
            Stage_File_Name = "";


            IS_Skew_File = false;
            Skew_File_Name = "";

            IS_Orthotropic = false;
        }
    }
}
