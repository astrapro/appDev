using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.Interface
{
    public interface IReport
    {
        void Calculate_Program();
        void Write_User_Input();
        void Read_User_Input();
        string FilePath { set; }
        void InitializeData();
        //string GetAstraDirectoryPath(string userpath);
    }
}
