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
using LimitStateMethod.RCC_T_Girder;


namespace LimitStateMethod.PSC_I_Girder
{
    public class PSC_I_Girder_Excel_Update : List<PSC_LS_Data>
    {
        public string Excel_File_Name { get; set; }
        public string Report_File_Name { get; set; }
        public Excel_User_Inputs PSC_Girder_User_Inputs { get; set; }

        public List<string> cab1_ref, cab2_ref, cab3_ref, cab4_ref;
        public DataGridView DGV_Cable { get; set; }
      
        public List<LoadData> llc = null;

        public PSC_I_Girder_Excel_Update()
            : base()
        {
            Excel_File_Name = "";
            Report_File_Name = "";

            PSC_Girder_User_Inputs = new Excel_User_Inputs();
        }

        public void Update_Data()
        {
            if (!File.Exists(Report_File_Name)) return;

            try
            {
                Read_Data(Report_File_Name);

                int i = 0;
                string kStr = "";
                int indx = 0;


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
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["LL-OG1 & OG2"];

                String cellFormulaAsString = myExcelWorksheet.get_Range("B12", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

                //int cel_index = 30;
                //int cel_index = 32;
                int cel_index = 26;

                int ci = (int)('B');


                char c = (char)ci;


                //Excel_User_Input_Data ex_uip = new Excel_User_Input_Data();
                double L = MyList.StringToDouble(PSC_Girder_User_Inputs[0].Input_Value, 0.0);
                double ang = MyList.StringToDouble(PSC_Girder_User_Inputs[5].Input_Value, 0.0);
                PSC_Girder_User_Inputs[0].Input_Value = (L / Math.Cos(ang * Math.PI / 180.0)).ToString("f2");

                #region Write Live Load Forces
                List<int> lst_title_cell = new List<int>();
                lst_title_cell.Add(8);
                lst_title_cell.Add(26);
                lst_title_cell.Add(46);
                lst_title_cell.Add(65);
                lst_title_cell.Add(84);
                lst_title_cell.Add(103);
                lst_title_cell.Add(122);
                lst_title_cell.Add(141);
                lst_title_cell.Add(160);
                lst_title_cell.Add(179);
                lst_title_cell.Add(198);
                lst_title_cell.Add(217);
                lst_title_cell.Add(236);

                int r = 0, cl = 0;
                //for (i = 12; i <= 274; i++)
                //for (i = 8; i <= 252; i++)
                for (i = 8; i <= 274; i++)
                {
                    if (lst_title_cell.Contains(i) && this.Count > r )
                    {

                        myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).Formula = this[r].Title;
                        continue;
                    }
                    if ((i >= 8 && i <= 11) ||
                        (i >= 17 && i <= 19) ||
                        (i >= 25 && i <= 29) ||
                        (i >= 35 && i <= 37) ||
                        (i >= 43 && i <= 49) ||
                        (i >= 55 && i <= 57) ||
                        (i >= 63 && i <= 68) ||
                        (i >= 74 && i <= 76) ||
                        (i >= 82 && i <= 87) ||
                        (i >= 93 && i <= 95) ||
                        (i >= 101 && i <= 106) ||
                        (i >= 112 && i <= 114) ||
                        (i >= 120 && i <= 125) ||
                        (i >= 131 && i <= 133) ||
                        (i >= 139 && i <= 144) ||
                        (i >= 150 && i <= 152) ||
                        (i >= 158 && i <= 163) ||
                        (i >= 169 && i <= 171) ||
                        (i >= 177 && i <= 182) ||
                        (i >= 188 && i <= 190) ||
                        (i >= 196 && i <= 201) ||
                        (i >= 207 && i <= 209) ||
                        (i >= 215 && i <= 220) ||
                        (i >= 226 && i <= 228) ||
                        (i >= 234 && i <= 239) ||
                        (i >= 245 && i <= 247) ||
                        (i >= 253 && i <= 261) ||
                        (i >= 267 && i <= 269)
                        )
                    {
                        //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = true;
                        continue;
                    }
                    else
                    {
                        try
                        {

                            if (i > 269)
                                r = this.Count - 1;
                            else if (i > 261)
                                r = this.Count - 2;

                            if (i > 269)
                                r = this.Count - 1;

                            myExcelWorksheet.get_Range(("B" + i.ToString()), misValue).Formula = this[r].Bending_Moments[cl].ToString();
                            myExcelWorksheet.get_Range(("F" + i.ToString()), misValue).Formula = this[r].Shear_Forces[cl++].ToString();
                            if (cl == 5)
                            {
                                r++;
                                cl = 0;
                            }
                        }
                        catch (Exception ex) 
                        {
                            //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = false;
                            //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = true;
                        }
                    }
                }
                #endregion DL_Self_Weight

                #region User Inputs
                if (PSC_Girder_User_Inputs != null)
                {
                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Input"];

                    List<int> lst_F = new List<int>();

                    int cnt = 0;
                    for (i = 4; i <= 71; i++)
                    {
                        if ((i >= 5 && i <= 7) ||
                            i == 12 ||
                            i == 19 ||
                            i == 24 ||
                            (i >= 40 && i <= 41) ||
                            (i >= 47 && i <= 48) ||
                            (i >= 52 && i <= 53) ||
                            (i >= 61 && i <= 62) ||
                            i == 65 ||
                            i == 67 ||
                            i == 69)
                        {
                            continue;
                        }
                        lst_F.Add(i);

                        PSC_Girder_User_Inputs[cnt++].Excel_Cell_Reference = "G" + i;
                    }


                    Excel.Range ran = myExcelWorksheet.get_Range("G4:G71", misValue);
                    //myExcelWorksheet.Range["F4:F46"].Locked = false;
                    //if ((bool)ran.Locked)
                    //{
                    //    MessageBox.Show("");
                    //}
                    myExcelWorksheet.Unprotect("2011ap");
                    ran.Locked = false;
                    for (i = 0; i < PSC_Girder_User_Inputs.Count; i++)
                    {
                        //myExcelWorksheet.get_Range("F" + lst_F[i].ToString(), misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                        myExcelWorksheet.get_Range(PSC_Girder_User_Inputs[i].Excel_Cell_Reference, misValue).Formula = PSC_Girder_User_Inputs[i].Input_Value;
                    }
                    ran.Locked = true;
                    myExcelWorksheet.Protect("2011ap");
                }


                #endregion User Inputs

                #region Cable

                if ((false))
                {
                    if (PSC_Girder_User_Inputs != null)
                    {
                        myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable"];
                        //22
                        //23
                        //24
                        //26
                        //27
                        //28
                        //31
                        //36
                        myExcelWorksheet.get_Range("F22", misValue).Formula = PSC_Girder_User_Inputs.Type_of_Cable;
                        myExcelWorksheet.get_Range("F31", misValue).Formula = PSC_Girder_User_Inputs.Type_of_Cable;
                        myExcelWorksheet.get_Range("F36", misValue).Formula = PSC_Girder_User_Inputs.Type_of_Cable;

                        myExcelWorksheet.get_Range("F23", misValue).Formula = PSC_Girder_User_Inputs.Strand_Area;
                        myExcelWorksheet.get_Range("F24", misValue).Formula = PSC_Girder_User_Inputs.UTS;
                        myExcelWorksheet.get_Range("F26", misValue).Formula = PSC_Girder_User_Inputs.Es;
                        myExcelWorksheet.get_Range("F27", misValue).Formula = PSC_Girder_User_Inputs.Permissible_Slip;
                        myExcelWorksheet.get_Range("F28", misValue).Formula = PSC_Girder_User_Inputs.Jacking_Distance;
                    }
                }

                #region Update Cable 1
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 1"];

                for (i = 0; i < cab1_ref.Count; i++)
                {
                    if (cab1_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab1_ref[i], misValue).Formula = DGV_Cable[2, i].Value.ToString().Replace("K", "");
                    }
                    //if (cab2_ref[i] != "")
                    //{
                    //    myExcelWorksheet.get_Range(cab2_ref[i], misValue).Formula = DGV_Cable[3, i].Value;
                    //}
                    //if (cab3_ref[i] != "")
                    //{
                    //    myExcelWorksheet.get_Range(cab3_ref[i], misValue).Formula = DGV_Cable[4, i].Value;
                    //}
                    //if (cab4_ref[i] != "")
                    //{
                    //    myExcelWorksheet.get_Range(cab4_ref[i], misValue).Formula = DGV_Cable[5, i].Value;
                    //}
                }
                #endregion Update Cable 1

                #region Update Cable 2
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 2"];

                for (i = 0; i < cab2_ref.Count; i++)
                {
                    if (cab2_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab2_ref[i], misValue).Formula = DGV_Cable[3, i].Value.ToString().Replace("K", ""); ;
                    }
                }
                #endregion Update Cable 1

                #region Update Cable 3
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 3"];

                for (i = 0; i < cab3_ref.Count; i++)
                {
                    if (cab3_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab3_ref[i], misValue).Formula = DGV_Cable[4, i].Value.ToString().Replace("K", ""); ;
                    }
                }
                #endregion Update Cable 3

                #region Update Cable 4
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 4"];
                for (i = 0; i < cab4_ref.Count; i++)
                {
                    if (cab4_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab4_ref[i], misValue).Formula = DGV_Cable[5, i].Value.ToString().Replace("K", ""); ;
                    }
                }
                #endregion Update Cable 1


                #endregion Cable

                //PSC_Girder_User_Inputs

                //Deckslab_User_Live_loads.Clear();

                myExcelWorkbook.Save();
                //myExcelWorkbook.Close(true, fileName, null);
                Marshal.ReleaseComObject(myExcelWorkbook);

                #endregion Update_ExcelData
            }
            catch (Exception ex) { }
        }
        public void Update_Data_British()
        {
            if (!File.Exists(Report_File_Name)) return;

            try
            {
                Read_Data(Report_File_Name);

                int i = 0;
                string kStr = "";
                int indx = 0;


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
                Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["LL-OG1 & OG2"];

                String cellFormulaAsString = myExcelWorksheet.get_Range("B12", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

                //int cel_index = 30;
                //int cel_index = 32;
                int cel_index = 26;

                int ci = (int)('B');


                char c = (char)ci;


                //Excel_User_Input_Data ex_uip = new Excel_User_Input_Data();
                double L = MyList.StringToDouble(PSC_Girder_User_Inputs[0].Input_Value, 0.0);
                double ang = MyList.StringToDouble(PSC_Girder_User_Inputs[5].Input_Value, 0.0);
                PSC_Girder_User_Inputs[0].Input_Value = (L / Math.Cos(ang * Math.PI / 180.0)).ToString("f2");

                #region Write Live Load Forces
                List<int> lst_title_cell = new List<int>();
                lst_title_cell.Add(8);
                lst_title_cell.Add(26);
                lst_title_cell.Add(46);
                lst_title_cell.Add(65);
                lst_title_cell.Add(84);
                lst_title_cell.Add(103);
                lst_title_cell.Add(122);
                lst_title_cell.Add(141);
                lst_title_cell.Add(160);
                lst_title_cell.Add(179);
                lst_title_cell.Add(198);
                lst_title_cell.Add(217);
                lst_title_cell.Add(236);

                int r = 0, cl = 0;
                //for (i = 12; i <= 274; i++)
                //for (i = 8; i <= 252; i++)
                bool hid_flag = false;

                for (i = 8; i <= 274; i++)
                {
                    if (lst_title_cell.Contains(i) && this.Count > r)
                    {

                        myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).Formula = this[r].Title;
                        continue;
                    }
                    if(hid_flag)
                    {
                        //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = true;

                    }
                    if (r == this.Count - 2 && i < 257)
                    {
                        while (i < 258)
                        {
                            myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = true;
                            i++;
                        }
                    }
                
                    //if (i > 269)
                    //    r = this.Count - 1;
                    //else if (i > 261)
                    //    r = this.Count - 2;

                    if ((i >= 8 && i <= 11) ||
                        (i >= 17 && i <= 19) ||
                        (i >= 25 && i <= 29) ||
                        (i >= 35 && i <= 37) ||
                        (i >= 43 && i <= 49) ||
                        (i >= 55 && i <= 57) ||
                        (i >= 63 && i <= 68) ||
                        (i >= 74 && i <= 76) ||
                        (i >= 82 && i <= 87) ||
                        (i >= 93 && i <= 95) ||
                        (i >= 101 && i <= 106) ||
                        (i >= 112 && i <= 114) ||
                        (i >= 120 && i <= 125) ||
                        (i >= 131 && i <= 133) ||
                        (i >= 139 && i <= 144) ||
                        (i >= 150 && i <= 152) ||
                        (i >= 158 && i <= 163) ||
                        (i >= 169 && i <= 171) ||
                        (i >= 177 && i <= 182) ||
                        (i >= 188 && i <= 190) ||
                        (i >= 196 && i <= 201) ||
                        (i >= 207 && i <= 209) ||
                        (i >= 215 && i <= 220) ||
                        (i >= 226 && i <= 228) ||
                        (i >= 234 && i <= 239) ||
                        (i >= 245 && i <= 247) ||
                        (i >= 253 && i <= 261) ||
                        (i >= 267 && i <= 269)
                        )
                    {
                        //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = true;
                        continue;
                    }
                    else
                    {
                        try
                        {

                            myExcelWorksheet.get_Range(("B" + i.ToString()), misValue).Formula = this[r].Bending_Moments[cl].ToString();
                            myExcelWorksheet.get_Range(("F" + i.ToString()), misValue).Formula = this[r].Shear_Forces[cl++].ToString();
                            if (cl == 5)
                            {
                                r++;
                                cl = 0;
                            }
                            hid_flag = false;
                        }
                        catch (Exception ex)
                        {
                            //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = false;
                            //myExcelWorksheet.get_Range(("A" + i.ToString()), misValue).EntireRow.Hidden = true;

                            hid_flag = true;
                        }
                    }
                }
                #endregion DL_Self_Weight

                #region User Inputs
                if (PSC_Girder_User_Inputs != null)
                {
                    myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Input"];

                    List<int> lst_F = new List<int>();

                    int cnt = 0;
                    for (i = 4; i <= 71; i++)
                    {
                        if ((i >= 5 && i <= 7) ||
                            i == 12 ||
                            i == 19 ||
                            i == 24 ||
                            (i >= 40 && i <= 41) ||
                            (i >= 47 && i <= 48) ||
                            (i >= 52 && i <= 53) ||
                            (i >= 61 && i <= 62) ||
                            i == 65 ||
                            i == 67 ||
                            i == 69)
                        {
                            continue;
                        }
                        lst_F.Add(i);

                        PSC_Girder_User_Inputs[cnt++].Excel_Cell_Reference = "G" + i;
                    }


                    Excel.Range ran = myExcelWorksheet.get_Range("G4:G71", misValue);
                    //myExcelWorksheet.Range["F4:F46"].Locked = false;
                    //if ((bool)ran.Locked)
                    //{
                    //    MessageBox.Show("");
                    //}
                    myExcelWorksheet.Unprotect("2011ap");
                    ran.Locked = false;
                    for (i = 0; i < PSC_Girder_User_Inputs.Count; i++)
                    {
                        //myExcelWorksheet.get_Range("F" + lst_F[i].ToString(), misValue).Formula = Deckslab_User_Inputs[i].Input_Value;
                        myExcelWorksheet.get_Range(PSC_Girder_User_Inputs[i].Excel_Cell_Reference, misValue).Formula = PSC_Girder_User_Inputs[i].Input_Value;
                    }
                    ran.Locked = true;
                    myExcelWorksheet.Protect("2011ap");
                }


                #endregion User Inputs

                #region Cable

                if ((false))
                {
                    if (PSC_Girder_User_Inputs != null)
                    {
                        myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable"];
                        //22
                        //23
                        //24
                        //26
                        //27
                        //28
                        //31
                        //36
                        myExcelWorksheet.get_Range("F22", misValue).Formula = PSC_Girder_User_Inputs.Type_of_Cable;
                        myExcelWorksheet.get_Range("F31", misValue).Formula = PSC_Girder_User_Inputs.Type_of_Cable;
                        myExcelWorksheet.get_Range("F36", misValue).Formula = PSC_Girder_User_Inputs.Type_of_Cable;

                        myExcelWorksheet.get_Range("F23", misValue).Formula = PSC_Girder_User_Inputs.Strand_Area;
                        myExcelWorksheet.get_Range("F24", misValue).Formula = PSC_Girder_User_Inputs.UTS;
                        myExcelWorksheet.get_Range("F26", misValue).Formula = PSC_Girder_User_Inputs.Es;
                        myExcelWorksheet.get_Range("F27", misValue).Formula = PSC_Girder_User_Inputs.Permissible_Slip;
                        myExcelWorksheet.get_Range("F28", misValue).Formula = PSC_Girder_User_Inputs.Jacking_Distance;
                    }
                }

                #region Update Cable 1
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 1"];

                for (i = 0; i < cab1_ref.Count; i++)
                {
                    if (cab1_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab1_ref[i], misValue).Formula = DGV_Cable[2, i].Value.ToString().Replace("K", "");
                    }
                    //if (cab2_ref[i] != "")
                    //{
                    //    myExcelWorksheet.get_Range(cab2_ref[i], misValue).Formula = DGV_Cable[3, i].Value;
                    //}
                    //if (cab3_ref[i] != "")
                    //{
                    //    myExcelWorksheet.get_Range(cab3_ref[i], misValue).Formula = DGV_Cable[4, i].Value;
                    //}
                    //if (cab4_ref[i] != "")
                    //{
                    //    myExcelWorksheet.get_Range(cab4_ref[i], misValue).Formula = DGV_Cable[5, i].Value;
                    //}
                }
                #endregion Update Cable 1

                #region Update Cable 2
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 2"];

                for (i = 0; i < cab2_ref.Count; i++)
                {
                    if (cab2_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab2_ref[i], misValue).Formula = DGV_Cable[3, i].Value.ToString().Replace("K", ""); ;
                    }
                }
                #endregion Update Cable 1

                #region Update Cable 3
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 3"];

                for (i = 0; i < cab3_ref.Count; i++)
                {
                    if (cab3_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab3_ref[i], misValue).Formula = DGV_Cable[4, i].Value.ToString().Replace("K", ""); ;
                    }
                }
                #endregion Update Cable 3

                #region Update Cable 4
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Cable 4"];
                for (i = 0; i < cab4_ref.Count; i++)
                {
                    if (cab4_ref[i] != "")
                    {
                        myExcelWorksheet.get_Range(cab4_ref[i], misValue).Formula = DGV_Cable[5, i].Value.ToString().Replace("K", ""); ;
                    }
                }
                #endregion Update Cable 1


                #endregion Cable

                //PSC_Girder_User_Inputs

                //Deckslab_User_Live_loads.Clear();

                myExcelWorkbook.Save();
                //myExcelWorkbook.Close(true, fileName, null);
                Marshal.ReleaseComObject(myExcelWorkbook);

                #endregion Update_ExcelData
            }
            catch (Exception ex) { }
        }

        public void Read_Data(string Report_File_Name)
        {
            Clear();
            if (!File.Exists(Report_File_Name)) return;

            List<string> list = new List<string>(File.ReadAllLines(Report_File_Name));
            MyList mlist = null;


            int i = 0;
            string kStr = "";
            PSC_LS_Data dc = null;
            int indx = 0;

            bool flag = false;

            string title = "";
            for (i = 0; i < list.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(list[i].ToUpper());
                if (kStr == "" || kStr.StartsWith("--------------")) continue;

                if (kStr.StartsWith("LIVE LOAD ANALYSIS"))
                {
                    mlist = new MyList(kStr, ':');
                    if (mlist.Count > 1)
                        title = mlist.StringList[1];

                }

                if (kStr.StartsWith("LIVE LOAD ANALYSIS") || kStr.StartsWith("MAXIMUM"))
                {
                    if (dc != null)
                    {
                        if (dc.Bending_Moments.Count > 0)
                            this.Add(dc);
                    }
                    dc = new PSC_LS_Data();

                    //mlist = new MyList(kStr, ':');
                    //if (mlist.Count > 1)
                    //    dc.Title = mlist.GetString(1);

                    dc.Title = title;
                    //dc.Bending_Moments = new List<double>();
                    //dc.Shear_Forces = new List<double>();
                    continue;
                }
                else if (kStr.StartsWith("CHECK FOR LIVE LOAD DEFLECTION"))
                {
                    break;
                }
                mlist = new MyList(kStr, ' ');
                try
                {
                    if (mlist.Count == 8)
                    {
                        dc.Bending_Moments.Add(mlist.GetDouble(2));
                        dc.Shear_Forces.Add(mlist.GetDouble(6));
                    }
                }
                catch (Exception ex) { }
            }
            kStr = "";

            if (dc != null)
            {
                if (dc.Bending_Moments.Count > 0)
                    this.Add(dc);
            }

            dc = new PSC_LS_Data();
            dc.Bending_Moments = new List<double>();
            dc.Shear_Forces = new List<double>();

            double max_val = 0;

            for (i = 0; i < this[0].Bending_Moments.Count; i++)
            {
                dc.Bending_Moments.Add(max_val);
                dc.Shear_Forces.Add(max_val);
            }

            for (i = 0; i < this.Count; i += 1)
            {

                for (int j = 0; j < dc.Bending_Moments.Count; j++)
                {
                    if (this[i].Bending_Moments[j] > dc.Bending_Moments[j])
                    {
                        dc.Bending_Moments[j] = this[i].Bending_Moments[j];
                        dc.Shear_Forces[j] = this[i].Shear_Forces[j];
                    }
                }
            }
            this.Add(dc);

            dc = new PSC_LS_Data();
           

            for (i = 0; i < this[0].Bending_Moments.Count; i++)
            {
                dc.Bending_Moments.Add(max_val);
                dc.Shear_Forces.Add(max_val);
            }
            for (i = 0; i < this.Count - 1; i += 1)
            {

                for (int j = 0; j < dc.Bending_Moments.Count; j++)
                {
                    if (this[i].Shear_Forces[j] > dc.Shear_Forces[j])
                    {
                        dc.Shear_Forces[j] = this[i].Shear_Forces[j];
                        dc.Bending_Moments[j] = this[i].Bending_Moments[j];
                    }
                }
            }
            this.Add(dc);
        }

    }

    public class PSC_LS_Data
    {
        public PSC_LS_Data()
        {
            Title = "";
            Bending_Moments = new List<double>();
            Shear_Forces = new List<double>();
        }
        public string Title { get; set; }

        public List<double> Bending_Moments { get; set; }
        public List<double> Shear_Forces { get; set; }
    }
}
