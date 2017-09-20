using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.DataStructure;
namespace AstraFunctionOne
{
    public class CRecentFiles
    {
        List<string> lst;
        string fileName;
        string kStr = "";
        public CRecentFiles(string filePath)
        {
            lst = new List<string>();
            fileName = filePath;
        }
        public List<string> FileList
        {
            get
            {
                return lst;
            }
        }

        public void Add(string item)
        {
            if (item == "") return;
            if (lst.Contains(item))
            {
                lst.Remove(item);
                lst.Add(item);
            }
            else
            {
                lst.Add(item);
            }


            //if (item == "") return;
            //if (lst.Contains(item.ToUpper()))
            //{
            //    lst.Remove(item.ToUpper());
            //    lst.Add(item.ToUpper());
            //}
            //else
            //{
            //    lst.Add(item.ToUpper());
            //}
        }
        public void SetFilesToMenu(ref ToolStripMenuItem tsmi)
        {
            tsmi.DropDownItems.Clear();
            int cnt = 0;
            for (int i = lst.Count - 1; i >= 0; i--)
            {

                tsmi.DropDownItems.Add((i + 1) + " : " + MyList.Get_Modified_Path(lst[i]));
                tsmi.DropDownItems[tsmi.DropDownItems.Count - 1].ToolTipText = lst[i];
                if (cnt > 18)
                    return;
            }


            //tsmi.DropDownItems.Clear();
            //int cnt = 0;
            //for (int i = lst.Count - 1; i >= 0; i--)
            //{
            //    tsmi.DropDownItems.Add(lst[i].ToUpper());
            //    if (cnt > 12)
            //        return;
            //}
        }
        public void SetFilesFromMenu(ToolStripMenuItem tsmi)
        {
            //lst.Clear();
            //int cnt = 0;
            //for (int i = 0 ; i < tsmi.DropDownItems.Count ; i++)
            //{
            //    lst.Add(tsmi.DropDownItems[i].Text);
            //    cnt++;
            //    if (cnt > 12)
            //        return;
            //}
        }
        public void ReadFileNames()
        {
            if (File.Exists(fileName))
            {
                lst.Clear();
                StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open));
                while (sr.EndOfStream == false)
                {
                    kStr = sr.ReadLine();
                    if (File.Exists(kStr.ToLower()))
                    {
                        Add(kStr);
                    }
                }
                sr.Close();
            }
            while(lst.Count > 12)
            {
                lst.RemoveAt(0);
            }
        }
        public void WriteFileNames()
        {
            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));
            try
            {
                foreach (string str in lst)
                {
                    sw.WriteLine(str);
                }
            }
            catch (Exception exx)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

    }
}
