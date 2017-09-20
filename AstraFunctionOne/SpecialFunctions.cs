using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace AstraFunctionOne
{
    class SpFuncs
    {
        public static string setNumber(String text,int decimalPlace)
        {
            string str = text;
            if (text.Contains("."))
            {
                int id = text.IndexOf(".");
                int len = text.Length;
                if ((len - id) > decimalPlace)
                {
                    str = text.Substring(0, (id + decimalPlace));
                }
            }
            return str;
        }

        public static string setNumber(double num, int decimalPlace)
        {
            return setNumber(num.ToString(), decimalPlace);
        }
        
        public static int getInt(string val)
        {
            int num = 0;
            try
            {
                num = int.Parse(val);
            }
            catch (Exception ex)
            {
                num = 0;
            }
            return num;
        }
        public static int getInt(string val, int defaultValue)
        {
            int num = 0;
            try
            {
                num = int.Parse(val);
            }
            catch (Exception ex)
            {
                num = defaultValue;
            }
            return num;
        }
        public static double getDouble(string val)
        {
            double num = 0;
            try
            {
                num = double.Parse(val);
            }
            catch (Exception ex)
            {
                num = 0.0;
            }
            return num;
        }
        public static double getDouble(string val,double defaultValue)
        {
            double num = 0;
            try
            {
                num = double.Parse(val);
            }
            catch (Exception ex)
            {
                num = defaultValue;
            }
            return num;
        }
        public static double getFact(string unit)
        {
            double fact = 0.0;
//Mass Factor
            if (unit.ToUpper() == "CM")
            {
                fact = 1.0;
            }
            else if (unit.ToUpper() == "M")
            {
                fact = 100;
            }
            else if (unit.ToUpper() == "MM")
            {
                fact = 0.10;
            }
            else if (unit.ToUpper() == "FT")
            {
                fact = 30.48;
            }
            else if (unit.ToUpper() == "INCH")
            {
                fact = 2.54;
            }
            else if (unit.ToUpper() == "YDS")
            {
                fact = 30.48 * 3.0;
            }
// Weight Factor
            else if (unit.ToUpper() == "KG")
            {
                fact = 1.0;
            }
            else if (unit.ToUpper() == "KN")
            {
                fact = 100.00;
            }
            else if (unit.ToUpper() == "MTON")
            {
                fact = 1000.00;
            }
            else if (unit.ToUpper() == "N")
            {
                fact = 0.10;
            }
            else if (unit.ToUpper() == "GMS")
            {
                fact = 0.001;
            }
            else if (unit.ToUpper() == "LBS")
            {
                fact = 0.4524;
            }
            else if (unit.ToUpper() == "KIP")
            {
                fact = 452.40;
            }
            return fact;
        }
        //void SetWorkingDirFromEnvVariable()
        //{
        //    System.Collections.IDictionary environmentVariables = Environment.GetEnvironmentVariables();

        //    string strFileName = System.Environment.GetEnvironmentVariable("SURVEY");
        //    if (strFileName != null)
        //    {
        //        if (strFileName != "" && File.Exists(strFileName))
        //        {
        //            this.AppDataPath = Path.GetDirectoryName(strFileName);
        //        }
        //    }
        //}

        //void DeleteTmpFiles(string strPath)
        //{
        //    if (strPath.Trim() != "" && Directory.Exists(strPath))
        //    {
        //        string[] arrfiles = Directory.GetFiles(strPath, "*.TMP");
        //        foreach (string strFile in arrfiles)
        //        {
        //            ViewerUtils.DeleteFileIfExists(strFile);
        //        }
        //    }
        //}
    }
}
