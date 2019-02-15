using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
//using Microsoft.Internal.Performance;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using AstraInterface.DataStructure;


namespace LimitStateMethod.RCC_T_Girder
{
    
    public  class RCC_T_Girder_Excel_Update
    {

        DataRetrieve dr;
        public string Excel_File_Name { get; set; }
        public string Report_File_Name { get; set; }
        public Excel_User_Inputs Long_User_Inputs { get; set; }
        public Excel_User_Inputs Cross_User_Inputs { get; set; }

        public RCC_T_Girder_Excel_Update()
        {
            Long_User_Inputs = new Excel_User_Inputs();
            Cross_User_Inputs = new Excel_User_Inputs();
            No_Crash_Barrier_Sode = 2;
        }
        public void Update_Excel_Long_Girder()
        {
            try
            {
                dr = new DataRetrieve();
                //string fileName = @"C:\Excel Test\ANALYSIS_RESULT.TXT";

                dr.Read_Data(Report_File_Name);
                Insert_Values_into_Excel_Long_Girder();
            }
            catch (Exception ex) { }
        }
        public void Insert_Values_into_Excel_Long_Girder()
        {
            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.Application();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;
          
            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["8. Summ Force"];

            String cellFormulaAsString = myExcelWorksheet.get_Range("A1", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            int cel_index = 7;

            VDataCollection vdc = dr.DL_Self_Weight;

            #region DL_Self_Weight
            for (int i = 0; i < vdc.Count; i++)
            {
                myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion DL_Self_Weight

            cel_index = 16;
            vdc = dr.DL_Deck_Wet_Conc;
            #region DL_Deck_Wet_Conc
            for (int i = 0; i < vdc.Count; i++)
            {
                myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion DL_Deck_Wet_Conc


            cel_index = 25;
            vdc = dr.DL_Deck_Dry_Conc;
            #region DL_Deck_Dry_Conc
            for (int i = 0; i < vdc.Count; i++)
            {
                myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion DL_Deck_Dry_Conc

            cel_index = 35;
            vdc = dr.DL_Self_Deck;
            #region DL_Self_Deck
            for (int i = 0; i < vdc.Count; i++)
            {
                myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion DL_Self_Deck

            cel_index = 48;
            vdc = dr.SIDL_Crash_Barrier;
            #region SIDL_Crash_Barrier
            for (int i = 0; i < vdc.Count; i++)
            {
                myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion SIDL_Crash_Barrier

            cel_index = 61;
            vdc = dr.SIDL_Wearing;
            #region SIDL_Wearing
            for (int i = 0; i < vdc.Count; i++)
            {
                myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion SIDL_Wearing


            cel_index = 75;
            vdc = dr.LL_Moving;
            #region LL_Moving
            for (int i = 0; i < vdc.Count; i++)
            {
                if (vdc[i].LoadText != "")
                    myExcelWorksheet.get_Range("L" + (cel_index + i), misValue).Formula = vdc[i].LoadText;

                myExcelWorksheet.get_Range("M" + (cel_index + i), misValue).Formula = vdc[i].Support / 10;
                myExcelWorksheet.get_Range("N" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening / 10;
                myExcelWorksheet.get_Range("O" + (cel_index + i), misValue).Formula = vdc[i]._L_8 / 10;
                myExcelWorksheet.get_Range("P" + (cel_index + i), misValue).Formula = vdc[i]._L_4 / 10;
                myExcelWorksheet.get_Range("Q" + (cel_index + i), misValue).Formula = vdc[i]._3L_8 / 10;
                myExcelWorksheet.get_Range("R" + (cel_index + i), misValue).Formula = vdc[i].Mid / 10;
            }

            if((cel_index + vdc.Count) <= 84)
            {

                //myExcelWorksheet.get_Range("A84" + (i), misValue).EntireRow.Hidden = true;

                cel_index = cel_index + vdc.Count;

                for (int i = cel_index; i <= 84; i++)
                {
                    myExcelWorksheet.get_Range("A" + (i), misValue).EntireRow.Hidden = true;

                    myExcelWorksheet.get_Range("L" + (i), misValue).Formula = "'";

                    myExcelWorksheet.get_Range("M" + (i), misValue).Formula = 0.0;
                    myExcelWorksheet.get_Range("N" + (i), misValue).Formula = 0.0;
                    myExcelWorksheet.get_Range("O" + (i), misValue).Formula = 0.0;
                    myExcelWorksheet.get_Range("P" + (i), misValue).Formula = 0.0;
                    myExcelWorksheet.get_Range("Q" + (i), misValue).Formula = 0.0;
                    myExcelWorksheet.get_Range("R" + (i), misValue).Formula = 0.0;
                }
            }
            #endregion LL_Moving







            #region User Inputs

            if (Long_User_Inputs != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["1.Input"];



                List<int> lst_F = new List<int>();

                int cnt = 0;
                int i = 0;
                for (i = 5; i <= 71; i++)
                {
                    if (i == 6 ||
                        i == 12 ||
                        i == 17 ||
                        i == 22 ||
                        i == 39 ||
                        i == 40 ||
                        i == 45 ||
                        i == 46 ||
                        i == 50 ||
                        i == 51 ||
                        i == 55 ||
                        i == 59 ||
                        i == 60 ||
                        i == 63 ||
                        i == 65 )
                    {
                        continue;
                    }
                    lst_F.Add(i);

                    Long_User_Inputs[cnt++].Excel_Cell_Reference = "F" + i;
                }


                Excel.Range ran = myExcelWorksheet.get_Range("F5:F71", misValue);
                //myExcelWorksheet.Range["F4:F46"].Locked = false;
                //if ((bool)ran.Locked)
                //{
                //    MessageBox.Show("");
                //}
                myExcelWorksheet.Unprotect("2011ap");
                ran.Locked = false;
                for (i = 0; i < Long_User_Inputs.Count; i++)
                {
                    //myExcelWorksheet.get_Range("F" + lst_F[i].ToString(), misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                    myExcelWorksheet.get_Range(Long_User_Inputs[i].Excel_Cell_Reference, misValue).Formula = Long_User_Inputs[i].Input_Value;
                }
                //ran.Locked = true;

                myExcelWorksheet.get_Range("F12", misValue).Formula = "=F10-"+ No_Crash_Barrier_Sode+"*F15";

                //myExcelWorksheet.Protect("2011ap");

            }


            #endregion User Inputs






            myExcelWorkbook.Save();
            //myExcelWorkbook.Close(true, fileName, null);
            Marshal.ReleaseComObject(myExcelWorkbook);
        }


        public void Insert_Values_into_Excel_Cross_Girder()
        {
            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.Application();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet;

            //String cellFormulaAsString = myExcelWorksheet.get_Range("A1", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            int cel_index = 7;

            VDataCollection vdc;



            #region User Inputs

            if (Cross_User_Inputs != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["End Cross girder as deep beam"];



                List<int> lst_F = new List<int>();

                int cnt = 0;
                int i = 0;
                for (i = 24; i <= 36; i++)
                {
                    if ((i >= 26 && i <= 28) ||
                        i == 31 ||
                        i == 32)
                    {
                        continue;
                    }
                    lst_F.Add(i);

                    Cross_User_Inputs[cnt++].Excel_Cell_Reference = "K" + i;
                }


                Excel.Range ran = myExcelWorksheet.get_Range("K24:K36", misValue);
                //myExcelWorksheet.Range["F4:F46"].Locked = false;
                //if ((bool)ran.Locked)
                //{
                //    MessageBox.Show("");
                //}
                myExcelWorksheet.Unprotect("2011ap");
                ran.Locked = false;
                for (i = 0; i < Cross_User_Inputs.Count; i++)
                {
                    //myExcelWorksheet.get_Range("F" + lst_F[i].ToString(), misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                    myExcelWorksheet.get_Range(Cross_User_Inputs[i].Excel_Cell_Reference, misValue).Formula = Cross_User_Inputs[i].Input_Value;
                }
                ran.Locked = true;
                myExcelWorksheet.Protect("2011ap");

            }

            #endregion User Inputs

            myExcelWorkbook.Save();
            //myExcelWorkbook.Close(true, fileName, null);
            Marshal.ReleaseComObject(myExcelWorkbook);
        }

        public int No_Crash_Barrier_Sode { get; set; }

    }
    public class DataRetrieve
    {
        public VDataCollection DL_Self_Weight { get; set; }

        public VDataCollection DL_Deck_Wet_Conc { get; set; }

        public VDataCollection DL_Deck_Dry_Conc { get; set; }

        public VDataCollection DL_Self_Deck { get; set; }

        public VDataCollection SIDL_Crash_Barrier { get; set; }

        public VDataCollection SIDL_Wearing { get; set; }

        public VDataCollection LL_Moving { get; set; }

        public DataRetrieve()
        {
            DL_Self_Weight = new VDataCollection();

            DL_Deck_Wet_Conc = new VDataCollection();

            DL_Deck_Dry_Conc = new VDataCollection();

            DL_Self_Deck = new VDataCollection();

            SIDL_Crash_Barrier = new VDataCollection();

            SIDL_Wearing = new VDataCollection();

            LL_Moving = new VDataCollection();
        }

        public void ClearAll()
        {
            DL_Self_Weight.Clear();
            DL_Deck_Wet_Conc.Clear();
            DL_Deck_Dry_Conc.Clear();
            DL_Self_Deck.Clear();
            SIDL_Crash_Barrier.Clear();
            SIDL_Wearing.Clear();
            LL_Moving.Clear();
        }
        public void Read_Data(string fileName)
        {
            ClearAll();
            //string fileName = @"C:\Excel Test\ANALYSIS_RESULT.TXT";
            List<string> list = new List<string>(File.ReadAllLines(fileName));

            int find = 0;
            string kStr = "";
            int indx = 0;

            for (int i = 0; i < list.Count; i++)
            {
                kStr = list[i];
                if (kStr.StartsWith("-----")) continue;
                if (kStr == "") continue;
                if (i >= 123)
                    kStr = kStr + "";


                if (kStr.Contains("Summary of B.M. & S.F. due to Dead Load (Forces due to Self weight of girder) kN-m"))
                {
                    find = 1;
                    continue;
                }
                else if (kStr.Contains("Summary of B.M. & S.F. due to Dead Load (Forces due to Deck slab Wet concrete and Shuttering load)"))
                {
                    find = 2;
                    continue;
                }
                else if (kStr.Contains("Summary of B.M. & S.F. due to Deshutering load"))
                {
                    find = 3;
                    continue;
                }
                else if (kStr.Contains("Summary of B.M. & S.F. per girder due to Dead Load (Forces due to Self weight of girder,Deck slab dry concrete)"))
                {
                    find = 4;
                    continue;
                }
                else if (kStr.Contains("Summary of B.M. & S.F. per girder due to SIDL(Crash barrier)"))
                {
                    find = 5;
                    continue;
                }
                else if (kStr.Contains("Summary of B.M. & S.F. per girder due to SIDL(Wearing coat)"))
                {
                    find = 6;
                    continue;
                }
                else if (kStr.Contains("Summary of B.M. & S.F. per girder due to Live Load"))
                {
                    find = 7;
                    continue;
                }
                else if (kStr.ToUpper().StartsWith("CHECK FOR LIVE LOAD DEFLECTION"))
                {
                    find = 8;
                    break;
                }
                try
                {

                    VData vd = new VData();
                    MyList mlist = null;

                    kStr = MyList.RemoveAllSpaces(kStr);

                    mlist = new MyList(kStr, ' ');
                    indx = mlist.Count - 1;
                    vd.Mid = mlist.GetDouble(indx--);
                    vd._3L_8 = mlist.GetDouble(indx--);
                    vd._L_4 = mlist.GetDouble(indx--);
                    vd._L_8 = mlist.GetDouble(indx--);
                    vd.Web_Widening = mlist.GetDouble(indx--);
                    vd.Support = mlist.GetDouble(indx--);




                    switch (find)
                    {
                        case 1:
                            DL_Self_Weight.Add(vd);
                            break;
                        case 2:
                            DL_Deck_Wet_Conc.Add(vd);
                            break;
                        case 3:
                            DL_Deck_Dry_Conc.Add(vd);
                            break;
                        case 4:
                            DL_Self_Deck.Add(vd);

                            break;
                        case 5:
                            SIDL_Crash_Barrier.Add(vd);

                            break;
                        case 6:
                            SIDL_Wearing.Add(vd);
                            break;
                        case 7:
                            string lname = mlist.GetString(0, ")").Trim();
                            vd.LoadText = lname;
                            LL_Moving.Add(vd);
                            break;
                    }
                }
                catch (Exception ex) { }

            }


        }
    }
    public class VData
    {
        public string LoadText { get; set; }
        public double Support { get; set; }
        public double Web_Widening { get; set; }
        public double _L_8 { get; set; }
        public double _L_4 { get; set; }
        public double _3L_8 { get; set; }
        public double Mid { get; set; }

        public VData()
        {
            LoadText = "";
            Support = 0.0;
            Web_Widening = 0.0;
            _L_8 = 0.0;
            _L_4 = 0.0;
            _3L_8 = 0.0;
            Mid = 0.0;
        }


        //Support       Web widening    1/8th     1/4th      3/8th        Mid 
    }
    public class VDataCollection : List<VData>
    {
        public VDataCollection()
            : base()
        {

        }
    }


    public class RCC_Deckslab_Excel_Update : List<Deck_Data>
    {
        public string Excel_File_Name { get; set; }
        public string Report_File_Name { get; set; }
        public Excel_User_Inputs Deckslab_User_Inputs { get; set; }
        public Excel_User_Inputs Deckslab_User_Live_loads { get; set; }
        public Excel_User_Inputs Deckslab_Design_Inputs { get; set; }

        public List<LoadData> llc = null;

        
        public RCC_Deckslab_Excel_Update()
            : base()
        {
            Excel_File_Name = "";
            Report_File_Name = "";

            Deckslab_User_Inputs = new Excel_User_Inputs();
            Deckslab_User_Live_loads = new Excel_User_Inputs();
            Deckslab_Design_Inputs = new Excel_User_Inputs();

        }

        public void Read_Update_Data()
        {
            Clear();
            if (!File.Exists(Report_File_Name)) return;

            List<string> list = new List<string>(File.ReadAllLines(Report_File_Name));
            MyList mlist = null;

            int i = 0;
            string kStr = "";
            Deck_Data dc = null;
            int indx = 0;
            for (i = 0; i < list.Count; i++)
            {

                kStr = MyList.RemoveAllSpaces(list[i]);
                if (kStr == "" || kStr.StartsWith("--------------")) continue;

                mlist = new MyList(kStr, ' ');
                dc = new Deck_Data();
                try
                {
                    indx = mlist.Count - 1;

                    for (int j = 0; j < 15; j++)
                    {
                        dc.Add(mlist.GetDouble(indx--));
                    }
                    dc.Reverse();
                    this.Add(dc);
                }
                catch (Exception ex) { }
            }
            kStr = "";



            #region Update_ExcelData

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.Application();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["5.Design"];

            String cellFormulaAsString = myExcelWorksheet.get_Range("A1", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            //int cel_index = 30;
            //int cel_index = 32;
            int cel_index = 26;

            int ci = (int)('B');


            char c = (char)ci;
            #region DL_Self_Weight
            for ( i = 0; i < Count; i++)
            {
                ci = (int)('B');
                for (int j = 0; j < (this[i].Count); j++)
                {
                    c = (char)ci++;
                    myExcelWorksheet.get_Range(c.ToString() + (cel_index + i), misValue).Formula = this[i][j].ToString();

                }

                //myExcelWorksheet.get_Range("C" + (cel_index + i), misValue).Formula = vdc[i].Support;
                //myExcelWorksheet.get_Range("D" + (cel_index + i), misValue).Formula = vdc[i].Web_Widening;
                //myExcelWorksheet.get_Range("E" + (cel_index + i), misValue).Formula = vdc[i]._L_8;
                //myExcelWorksheet.get_Range("F" + (cel_index + i), misValue).Formula = vdc[i]._L_4;
                //myExcelWorksheet.get_Range("G" + (cel_index + i), misValue).Formula = vdc[i]._3L_8;
                //myExcelWorksheet.get_Range("H" + (cel_index + i), misValue).Formula = vdc[i].Mid;
            }
            #endregion DL_Self_Weight



            #region User Inputs

            if (Deckslab_User_Inputs != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["1.Input"];



                List<int> lst_F = new List<int>();

                int cnt = 0;
                for (i = 4; i <= 49; i++)
                {
                    if (i == 5 ||
                        i == 16 ||
                        i == 19 ||
                        i == 30 ||
                        i == 31 ||
                        i == 36 ||
                        i == 37 ||
                        (i > 43 && i < 49))
                    {
                        continue;
                    }
                    lst_F.Add(i);

                    Deckslab_User_Inputs[cnt++].Excel_Cell_Reference = "F" + i;
                }
                

                Excel.Range ran = myExcelWorksheet.get_Range("F4:F50", misValue);
                //myExcelWorksheet.Range["F4:F46"].Locked = false;
                //if ((bool)ran.Locked)
                //{
                //    MessageBox.Show("");
                //}
                myExcelWorksheet.Unprotect("2011ap");
                ran.Locked = false;
                for (i = 0; i < Deckslab_User_Inputs.Count; i++)
                {
                    //myExcelWorksheet.get_Range("F" + lst_F[i].ToString(), misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                    myExcelWorksheet.get_Range(Deckslab_User_Inputs[i].Excel_Cell_Reference, misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                }
                ran.Locked = true;
                myExcelWorksheet.Protect("2011ap");

            }


            #endregion User Inputs

            //Deckslab_User_Live_loads.Clear();

            #region Live Loads

            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["2.LiveLoad"];

            double max_val = llc[0].Get_Maximum_Load();

            double val = (max_val * 10/2.0);

            List<string> lst_xls = new List<string>();

            //lst_xls.Add(string.Format("57"));
            lst_xls.Add(string.Format("L28"));
            lst_xls.Add(string.Format("L29"));
            lst_xls.Add(string.Format("F105"));
            lst_xls.Add(string.Format("F106"));
            lst_xls.Add(string.Format("M105"));
            lst_xls.Add(string.Format("M106"));
            lst_xls.Add(string.Format("G369"));
            lst_xls.Add(string.Format("M369"));
            lst_xls.Add(string.Format("G374"));
            lst_xls.Add(string.Format("M374"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            }

            lst_xls.Clear();
            max_val = llc[3].Get_Maximum_Load();
            val = (max_val * 10);
            //val = (max_val * 10 / 2.0);

            //lst_xls.Add(string.Format("100"));
            lst_xls.Add(string.Format("G161"));
            lst_xls.Add(string.Format("M161"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            }
            //lst_xls.Add(string.Format(""));
            //lst_xls.Add(string.Format(""));
            //lst_xls.Add(string.Format(""));
            //lst_xls.Add(string.Format(""));
            lst_xls.Clear();
            max_val = llc[4].Get_Maximum_Load();
            val = (max_val * 10);
            //val = (max_val * 10 / 2.0);
            //lst_xls.Add(string.Format("50"));
            lst_xls.Add(string.Format("G229"));
            lst_xls.Add(string.Format("G230"));
            lst_xls.Add(string.Format("M229"));
            lst_xls.Add(string.Format("M230"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            }
            //lst_xls.Add(string.Format(""));
            lst_xls.Clear();
            max_val = llc[1].Total_Loads / 2.0; ;
            val = (max_val * 10);
            //lst_xls.Add(string.Format("35"));
            lst_xls.Add(string.Format("G297"));
            lst_xls.Add(string.Format("M297"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();

            }
            //lst_xls.Add(string.Format(""));
            //lst_xls.Add(string.Format(""));
            lst_xls.Clear();
            max_val = llc[2].Get_Maximum_Load();
            val = (max_val * 10 / 2.0);
            //lst_xls.Add(string.Format("85"));
            lst_xls.Add(string.Format("G363"));
            lst_xls.Add(string.Format("M363"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = val.ToString();
            }

            lst_xls.Clear();
            //lst_xls.Add(string.Format("2.600"));
            lst_xls.Add(string.Format("I12"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[0].Input_Value;
            }

            lst_xls.Clear();
            //lst_xls.Add(string.Format("0"));
            lst_xls.Add(string.Format("B24"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[1].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("250"));
            lst_xls.Add(string.Format("L30"));
            lst_xls.Add(string.Format("G370"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[5].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("500"));
            lst_xls.Add(string.Format("L31"));
            lst_xls.Add(string.Format("M370"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[6].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("1.200"));
            lst_xls.Add(string.Format("L32"));
            lst_xls.Add(string.Format("G371"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[7].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("1.800"));
            lst_xls.Add(string.Format("L33"));
            lst_xls.Add(string.Format("M108"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[8].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("0.400"));
            lst_xls.Add(string.Format("L34"));
            lst_xls.Add(string.Format("G372"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[9].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("5.273"));
            lst_xls.Add(string.Format("G166"));
            lst_xls.Add(string.Format("G232"));
            lst_xls.Add(string.Format("G364"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[2].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("810"));
            lst_xls.Add(string.Format("G164"));
            lst_xls.Add(string.Format("G365"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[10].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("1.22"));
            lst_xls.Add(string.Format("G165"));
            lst_xls.Add(string.Format("G234"));
            lst_xls.Add(string.Format("G366"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[3].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("1.93"));
            lst_xls.Add(string.Format("I166"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[11].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("1.25"));
            lst_xls.Add(string.Format("I167"));
            lst_xls.Add(string.Format("G235"));
            lst_xls.Add(string.Format("G300"));
            lst_xls.Add(string.Format("G367"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[4].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("360"));
            lst_xls.Add(string.Format("G233"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[12].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("840"));
            lst_xls.Add(string.Format("G298"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[13].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("30"));
            lst_xls.Add(string.Format("G299"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[14].Input_Value;
            }


            lst_xls.Clear();
            //lst_xls.Add(string.Format("4570"));
            lst_xls.Add(string.Format("M298"));
            for (i = 0; i < lst_xls.Count; i++)
            {
                myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_User_Live_loads[15].Input_Value;
            }
            #endregion Live Loads


            #region Design User Inputs

            if (Deckslab_Design_Inputs != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["5.Design"];



                lst_xls.Clear();
                //lst_xls.Add(string.Format("16"));
                lst_xls.Add(string.Format("G87"));
                lst_xls.Add(string.Format("E111"));
                lst_xls.Add(string.Format("E115"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[0].Input_Value;
                }

                lst_xls.Clear();
                //lst_xls.Add(string.Format("1000"));
                lst_xls.Add(string.Format("B92"));
                lst_xls.Add(string.Format("N205"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[1].Input_Value;
                }


                lst_xls.Clear();
                //lst_xls.Add(string.Format("10"));
                lst_xls.Add(string.Format("I111"));
                lst_xls.Add(string.Format("I115"));
                lst_xls.Add(string.Format("F134"));
                lst_xls.Add(string.Format("F141"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[2].Input_Value;
                }


                lst_xls.Clear();
                //lst_xls.Add(string.Format("200"));
                lst_xls.Add(string.Format("G111"));
                lst_xls.Add(string.Format("G115"));
                lst_xls.Add(string.Format("K111"));
                lst_xls.Add(string.Format("K115"));
                lst_xls.Add(string.Format("H134"));
                lst_xls.Add(string.Format("H141"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[3].Input_Value;
                }


                lst_xls.Clear();
                //lst_xls.Add(string.Format("0.8"));
                lst_xls.Add(string.Format("N188"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[4].Input_Value;
                }

                lst_xls.Clear();
                //lst_xls.Add(string.Format("0.5"));
                lst_xls.Add(string.Format("N191"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[5].Input_Value;
                }

                lst_xls.Clear();
                //lst_xls.Add(string.Format("0.5"));
                lst_xls.Add(string.Format("N216"));
                for (i = 0; i < lst_xls.Count; i++)
                {
                    myExcelWorksheet.get_Range(lst_xls[i], misValue).Formula = Deckslab_Design_Inputs[6].Input_Value;
                }

            }


            #endregion User Inputs

            try
            {
                myExcelWorkbook.Save();
                //myExcelWorkbook.Close(true, fileName, null);
                Marshal.ReleaseComObject(myExcelWorkbook);
            }
            catch (Exception ex) { }
            #endregion Update_ExcelData
        }
    }
    public class Deck_Data : List<double>
    {
        public Deck_Data()
            : base()
        {
        }
    }
}
