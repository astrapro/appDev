using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AstraInterface.DataStructure
{
    public class DesignDrawings
    {
        Hashtable hash_Interactive;
        Hashtable hash_DefaultDrawing;

        public DesignDrawings(string file_path)
        {
            hash_DefaultDrawing = new Hashtable();
            hash_Interactive = new Hashtable();
            Read_From_file(file_path);
        }
        public void Read_From_file(string file_name)
        {
            hash_Interactive.Clear();
            hash_DefaultDrawing.Clear();

            List<string> list_cont = new List<string>(File.ReadAllLines(file_name));

            MyList mlist = null;
            string kStr = "";
            bool drw_flag = false;
            bool drw_interactive_flag = false;
            bool drw_default_drawing_flag = false;

            for (int i = 0; i < list_cont.Count; i++)
            {
                kStr = list_cont[i].ToUpper();
                mlist = new MyList(kStr, '=');
                if (mlist.StringList[0] == "<DRAWING>")
                {
                    drw_flag = true; continue;
                }
                else if (mlist.StringList[0] == "<ENDDRAWING>")
                {
                    drw_flag = false; continue;
                }

                if (mlist.StringList[0] == "<INTERACTIVE>")
                {
                    drw_interactive_flag = true; continue;
                }
                else if (mlist.StringList[0] == "<ENDINTERACTIVE>")
                {
                    drw_interactive_flag = false; continue;
                }

                if (mlist.StringList[0] == "<DEFAULTDRAWING>")
                {
                    drw_default_drawing_flag = true; continue;
                }
                else if (mlist.StringList[0] == "<ENDDEFAULTDRAWING>")
                {
                    drw_default_drawing_flag = false; continue;
                }

                if (drw_interactive_flag)
                {
                    if (mlist.Count > 1)
                        Add_Interactive_Drawing(mlist.StringList[0].Trim().TrimStart().TrimEnd(), mlist.StringList[1]);
                }
                if (drw_default_drawing_flag)
                {
                    if (mlist.Count > 1)
                        Add_Default_Drawing(mlist.StringList[0].Trim().TrimStart().TrimEnd(), mlist.StringList[1]);
                }

            }
        }
        public void Add_Interactive_Drawing(string _cmdString, string drawing_file_path)
        {
            hash_Interactive.Add(_cmdString, Path.Combine(Application.StartupPath, "DRAWINGS\\" + drawing_file_path));
        }
        public string Get_Interactive_Drawing_Path(string _cmdString)
        {
            return (string)hash_Interactive[_cmdString.ToUpper()];
        }
        public void Add_Default_Drawing(string _cmdString, string drawing_file_path)
        {
            hash_DefaultDrawing.Add(_cmdString, Path.Combine(Application.StartupPath, "DRAWINGS\\" + drawing_file_path));
        }
        public string Get_Default_Drawing_Path(string _cmdString)
        {
            return (string)hash_DefaultDrawing[_cmdString.ToUpper()];
        }
    }
   
}
