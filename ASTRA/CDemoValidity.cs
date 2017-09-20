using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace AstraFunctionOne
{
    public class CDemoValidity
    {
        public CDemoValidity()
        {
        }
        public static bool WriteValidity(int valid)
        {
            string filePath = System.Environment.GetFolderPath(Environment.SpecialFolder.System);
            filePath = Path.Combine(filePath, "SECSYS");


            if (!Directory.Exists(filePath))
            {

                //System.Security.AccessControl.DirectorySecurity dic = new
                //    System.Security.AccessControl.DirectorySecurity(filePath);

                //Directory.CreateDirectory(filePath, dic);


                Directory.CreateDirectory(filePath);
                DirectoryInfo dInfo = new DirectoryInfo(filePath);

                dInfo.CreationTime = new DateTime(2007, 9, 9);
                dInfo.LastAccessTime = new DateTime(2007, 9, 9);
                dInfo.LastWriteTime = new DateTime(2007, 9, 9);
            }

            string fileName = Path.Combine(filePath, "security.sys");
            //try
            //{
            //File.WriteAllText(fileName, "krishna", Encoding.ASCII);
            //}
            //catch(Exception ex)
            //{

            //}
            try
            {
                File.WriteAllText(fileName, valid.ToString("D"));
                FileInfo fInfo = new FileInfo(fileName);
                fInfo.CreationTime = new DateTime(2007, 1, 9);
                fInfo.LastAccessTime = new DateTime(2002, 2, 9);
                fInfo.LastWriteTime = new DateTime(2001, 3, 9);
            }

            //string fileName = Path.Combine(filePath, "security.sys");
            //try
            //{
            //    File.WriteAllText(fileName, valid.ToString("D"));
            //}
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static int GetValidity()
        {
            string filePath = System.Environment.GetFolderPath(Environment.SpecialFolder.System);
            filePath = Path.Combine(filePath, "SECSYS");

            int valid = -1;
            if (!Directory.Exists(filePath))
                return valid;

            string fileName = Path.Combine(filePath, "security.sys");
            try
            {
                string str = File.ReadAllText(fileName);
                str = str.TrimStart();
                str = str.TrimEnd();
                valid = int.Parse(str);
            }
            catch (Exception ex)
            {
                valid = -1;
            }
            return valid;
        }
    }
}
