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
using AstraInterface.Interface;



namespace BridgeAnalysisDesign.PSC_BoxGirder
{
    
    public class Continuous_Box_Girder_Excel_Program
    {
        Live_Load_Box_Girder_Data_Collection dr;
        public string Excel_File_Name { get; set; }
        public string Report_File_Name { get; set; }
        public Excel_User_Inputs Long_User_Inputs { get; set; }
        public Excel_User_Inputs Cross_User_Inputs { get; set; }
        public Continuous_PSC_BoxGirderAnalysis Analysis_Data { get; set; }
        public Box_Transverse_Data Transverse { get; set; }
        IApplication iApp;

        public Continuous_Box_Girder_Excel_Program(IApplication iApp)
        {
            Long_User_Inputs = new Excel_User_Inputs();
            Cross_User_Inputs = new Excel_User_Inputs();

            this.iApp = iApp;
        }
        public void Update_Excel_Long_Girder()
        {
            try
            {
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

            myExcelApp = new Excel.ApplicationClass();


            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["CWLL"];
         

            String cellFormulaAsString = myExcelWorksheet.get_Range("A1", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.


            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["36m prop"];

            myExcelWorksheet.get_Range("K2").Formula = Analysis_Data.L1.ToString();


            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["72m prop"];

            myExcelWorksheet.get_Range("K2").Formula = Analysis_Data.L2.ToString();




            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["CWLL"];
         


            int cel_index = 7;

            List<int> lst_cells = new List<int>();

            #region Excel Cell Reference
            lst_cells.Add(7);
            lst_cells.Add(86);
            lst_cells.Add(164);
            lst_cells.Add(243);
            lst_cells.Add(322);
            lst_cells.Add(401);
            lst_cells.Add(479);
            lst_cells.Add(558);
            lst_cells.Add(635);
            lst_cells.Add(714);
            lst_cells.Add(792);
            lst_cells.Add(871);
            lst_cells.Add(950);
            lst_cells.Add(1029);
            lst_cells.Add(1107);
            lst_cells.Add(1186);
            lst_cells.Add(1263);
            lst_cells.Add(1342);
            lst_cells.Add(1420);
            lst_cells.Add(1499);
            lst_cells.Add(1578);
            lst_cells.Add(1657);
            lst_cells.Add(1735);
            lst_cells.Add(1814);
            lst_cells.Add(1891);
            lst_cells.Add(1970);
            lst_cells.Add(2048);
            lst_cells.Add(2127);
            lst_cells.Add(2206);
            lst_cells.Add(2285);
            lst_cells.Add(2363);
            lst_cells.Add(2442);
            lst_cells.Add(2521);
            lst_cells.Add(2600);
            lst_cells.Add(2678);
            lst_cells.Add(2757);
            lst_cells.Add(2836);
            lst_cells.Add(2915);
            lst_cells.Add(2993);
            lst_cells.Add(3072);
            lst_cells.Add(3150);
            lst_cells.Add(3229);
            lst_cells.Add(3307);
            lst_cells.Add(3386);
            lst_cells.Add(3465);
            lst_cells.Add(3544);
            lst_cells.Add(3622);
            lst_cells.Add(3701);
            #endregion Excel Cell Reference

            if (Analysis_Data != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["CWLL"];

                List<int> lst_F = new List<int>();

                int cnt = 0;
                int i = 0;

                iApp.Progress_Works.Clear();

                for (i = 1; i <= 12; i++)
                {
                    iApp.Progress_Works.Add("Update Maximum +VE BM Corresponding SF from Analysis " + (i));
                    iApp.Progress_Works.Add("Update Maximum -VE BM Corresponding SF from Analysis " + (i));
                    iApp.Progress_Works.Add("Update Maximum +VE SF Corresponding BM from Analysis " + (i));
                    iApp.Progress_Works.Add("Update Maximum -VE SF Corresponding BM from Analysis " + (i));
                }


                iApp.Progress_Works.Add("Update BM Corresponding SF for SPAN 1 (SIMPLY SUPPORTED) ");
                iApp.Progress_Works.Add("Update BM Corresponding SF for SPAN 2 (SIMPLY SUPPORTED) ");
                iApp.Progress_Works.Add("Update BM Corresponding SF for CONTINUOUS STAGE (DL + SIDL)");
                for (i = 1; i <= 7; i++)
                {
                    iApp.Progress_Works.Add("Reading BM Corresponding SF for FPLL Load Case " + i);
                }


                iApp.Progress_Works.Add("Reading BM Corresponding SF for SINK Load Case 1 TO 4");
                iApp.Progress_Works.Add("Reading BM Corresponding SF for SINK Load Case 5 TO 8");
                iApp.Progress_Works.Add("Reading BM Corresponding SF for SINK Load Case 9 TO 12");
                iApp.Progress_Works.Add("Reading BM Corresponding SF for SINK Load Case 13 TO 14");


                int counter = 0;
                int cell_counter = 0;
                int start_ll = 27;  //Start Live Load Index
                for (i = start_ll; i < Analysis_Data.FRC_Results.Count; i++)
                {
                    cel_index = lst_cells[i - start_ll];

                    //iApp.SetProgressValue(i, Analysis_Data.FRC_Results.Count);

                    iApp.Progress_ON("Update Analysis Results");
                    counter++;
                    if (counter > 4)
                        counter = counter - 4;
                    cell_counter = 0;

                    for (int j = 0; j < Analysis_Data.FRC_Results[i].Count; j++)
                    {
                        if (counter <= 2)
                        {
                            myExcelWorksheet.get_Range("D" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].BM.ToString();
                            myExcelWorksheet.get_Range("F" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].SF.ToString();
                        }
                        else
                        {
                            myExcelWorksheet.get_Range("D" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].SF.ToString();
                            myExcelWorksheet.get_Range("F" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].BM.ToString();
                        }

                        cel_index++;

                        cell_counter++;

                        if (cell_counter >= 2 && j != Analysis_Data.FRC_Results[i].Count - 1)
                        {
                            myExcelWorksheet.get_Range("D" + cel_index.ToString(), misValue).Formula = "";
                            myExcelWorksheet.get_Range("F" + cel_index.ToString(), misValue).Formula = "";
                            cel_index++;
                            cell_counter = 0;
                        }
                        if (iApp.Is_Progress_Cancel) break;
                        iApp.SetProgressValue(j, Analysis_Data.FRC_Results[i].Count);
                    }
                    iApp.Progress_OFF();
                    //break;
                }






                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["bm sf sum"];
                Forces_Result_Data frd;

                int incr_diff = 34; // Cell difference between 113 to 147

                for (i = 0; i < start_ll; i++)
                {
                    #region Update bm and sf
                    cel_index = 0;
                    if (i == 0) cel_index = 11;
                    else if (i == 1) cel_index = 31;
                    else if (i == 2) cel_index = 48;
                    else if (i == 3) continue;
                    else if (i == 4) cel_index = 617;
                    else if (i > 4 && i < 18) continue;
                    else if (i == 18) cel_index = 113;




                    iApp.Progress_ON("Reading BM Corresponding SF for DL, SIDL, FPLL..");
                    for (int j = 0; j < Analysis_Data.FRC_Results[i].Count; j++)
                    {
                        iApp.SetProgressValue(j, Analysis_Data.FRC_Results[i].Count);
                        if (i == 2)
                        {
                            myExcelWorksheet.get_Range("I" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].BM.ToString();
                            myExcelWorksheet.get_Range("L" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].SF.ToString();
                            myExcelWorksheet.get_Range("O" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i + 1][j].BM.ToString();
                            myExcelWorksheet.get_Range("R" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i + 1][j].SF.ToString();
                        }
                        else if (i == 4)
                        {
                            //Positive Neative Cell Index difference
                            incr_diff = 683 - 617;


                            List<double> lst_bm1 = new List<double>();
                            List<double> lst_sf1 = new List<double>();
                           
                            List<double> lst_bm2 = new List<double>();
                            List<double> lst_sf2 = new List<double>();

                            double bm = 0.0;
                            double sf = 0.0;
                            double bm_negative = 0.0;
                            double sf_negative = 0.0;
                            int c =0;
                            List<Forces_Results> lst = new List<Forces_Results>();

                            for (c = i; c < (i + 14); c++)
                            {
                                lst.Add(Analysis_Data.FRC_Results[c]);
                            }

                            #region From Loadcase 1 to 4

                            for (c = 0; c < 4; c++)
                            {
                                if (lst[c][j].BM >= 0)
                                {
                                    if (Math.Abs(bm) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm = lst[c][j].BM;
                                        sf = lst[c][j].SF;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(bm_negative) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm_negative = lst[c][j].BM;
                                        sf_negative = lst[c][j].SF;
                                    }
                                }
                            }

                            myExcelWorksheet.get_Range("H" + cel_index.ToString(), misValue).Formula = bm.ToString();
                            myExcelWorksheet.get_Range("J" + cel_index.ToString(), misValue).Formula = sf.ToString();


                            myExcelWorksheet.get_Range("H" + (cel_index + incr_diff).ToString(), misValue).Formula = bm_negative.ToString();
                            myExcelWorksheet.get_Range("J" + (cel_index + incr_diff).ToString(), misValue).Formula = sf_negative.ToString();

                            #endregion From Loadcase 1 to 4


                            #region From Loadcase 5 to 8

                            for (c = 4; c < 8; c++)
                            {
                                if (lst[c][j].BM >= 0)
                                {
                                    if (Math.Abs(bm) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm = lst[c][j].BM;
                                        sf = lst[c][j].SF;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(bm_negative) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm_negative = lst[c][j].BM;
                                        sf_negative = lst[c][j].SF;
                                    }
                                }
                            }

                            myExcelWorksheet.get_Range("M" + cel_index.ToString(), misValue).Formula = bm.ToString();
                            myExcelWorksheet.get_Range("O" + cel_index.ToString(), misValue).Formula = sf.ToString();


                            myExcelWorksheet.get_Range("M" + (cel_index + incr_diff).ToString(), misValue).Formula = bm_negative.ToString();
                            myExcelWorksheet.get_Range("O" + (cel_index + incr_diff).ToString(), misValue).Formula = sf_negative.ToString();
                            #endregion From Loadcase 5 to 8


                            #region From Loadcase 9 to 12

                            for (c = 8; c < 12; c++)
                            {
                                if (lst[c][j].BM >= 0)
                                {
                                    if (Math.Abs(bm) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm = lst[c][j].BM;
                                        sf = lst[c][j].SF;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(bm_negative) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm_negative = lst[c][j].BM;
                                        sf_negative = lst[c][j].SF;
                                    }
                                }
                            }

                            myExcelWorksheet.get_Range("R" + cel_index.ToString(), misValue).Formula = bm.ToString();
                            myExcelWorksheet.get_Range("T" + cel_index.ToString(), misValue).Formula = sf.ToString();


                            myExcelWorksheet.get_Range("R" + (cel_index + incr_diff).ToString(), misValue).Formula = bm_negative.ToString();
                            myExcelWorksheet.get_Range("T" + (cel_index + incr_diff).ToString(), misValue).Formula = sf_negative.ToString();
                          
                            
                            #endregion From Loadcase 9 to 12


                            #region From Loadcase 13 to 14

                            for (c = 11; c < 14; c++)
                            {
                                if (lst[c][j].BM >= 0)
                                {
                                    if (Math.Abs(bm) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm = lst[c][j].BM;
                                        sf = lst[c][j].SF;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(bm_negative) < Math.Abs(lst[c][j].BM))
                                    {
                                        bm_negative = lst[c][j].BM;
                                        sf_negative = lst[c][j].SF;
                                    }
                                }
                            }

                            myExcelWorksheet.get_Range("W" + cel_index.ToString(), misValue).Formula = bm.ToString();
                            myExcelWorksheet.get_Range("Y" + cel_index.ToString(), misValue).Formula = sf.ToString();


                            myExcelWorksheet.get_Range("W" + (cel_index + incr_diff).ToString(), misValue).Formula = bm_negative.ToString();
                            myExcelWorksheet.get_Range("Y" + (cel_index + incr_diff).ToString(), misValue).Formula = sf_negative.ToString();


                            #endregion From Loadcase 13 to 14
                        }

                        else if (i == 18)
                        {
                            incr_diff = 34; 
                            //cel_index = 113;
                            // FPLL Load Case 1
                            #region FPLL Load Case 1

                            frd = Analysis_Data.FRC_Results[i][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("H" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("I" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("K" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("L" + cel_index.ToString(), misValue).Formula = "";

                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("H" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "H", cel_index);
                                myExcelWorksheet.get_Range("I" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "I", cel_index);

                                myExcelWorksheet.get_Range("K" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("L" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("H" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("I" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("K" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("L" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("H" + (cel_index + incr_diff).ToString(), misValue).Formula =  "";
                                myExcelWorksheet.get_Range("I" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("K" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "K", cel_index);
                                myExcelWorksheet.get_Range("L" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "L", cel_index);

                            }
                            #endregion FPLL Load Case 1

                            #region FPLL Load Case 2

                            frd = Analysis_Data.FRC_Results[i + 1][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("N" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("O" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("Q" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("R" + cel_index.ToString(), misValue).Formula = "";


                                myExcelWorksheet.get_Range("N" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "N", cel_index);
                                myExcelWorksheet.get_Range("O" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "O", cel_index);

                                myExcelWorksheet.get_Range("Q" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("R" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("N" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("O" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("Q" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("R" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();


                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("N" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("O" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("Q" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "Q", cel_index);
                                myExcelWorksheet.get_Range("R" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "R", cel_index);

                            }
                            #endregion FPLL Load Case 2

                            #region FPLL Load Case 3

                            frd = Analysis_Data.FRC_Results[i + 2][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("T" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("U" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("W" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("X" + cel_index.ToString(), misValue).Formula = "";


                                myExcelWorksheet.get_Range("T" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "T", cel_index);
                                myExcelWorksheet.get_Range("U" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "U", cel_index);

                                myExcelWorksheet.get_Range("W" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("X" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("T" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("U" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("W" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("X" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();
                                
                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("T" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("U" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("W" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "W", cel_index);
                                myExcelWorksheet.get_Range("X" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "X", cel_index);

                            }
                            #endregion FPLL Load Case 3

                            #region FPLL Load Case 4

                            frd = Analysis_Data.FRC_Results[i + 3][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("Z" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AA" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("AC" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AD" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("Z" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "Z", cel_index);
                                myExcelWorksheet.get_Range("AA" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AA", cel_index);

                                myExcelWorksheet.get_Range("AC" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AD" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("Z" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AA" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AC" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AD" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();


                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("Z" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AA" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AC" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AC", cel_index);
                                myExcelWorksheet.get_Range("AD" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AD", cel_index);

                            }
                            #endregion FPLL Load Case 4

                            #region FPLL Load Case 5

                            frd = Analysis_Data.FRC_Results[i + 4][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("AF" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AG" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("AI" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AJ" + cel_index.ToString(), misValue).Formula = "";


                                myExcelWorksheet.get_Range("AF" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AF", cel_index);
                                myExcelWorksheet.get_Range("AG" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AG", cel_index);

                                myExcelWorksheet.get_Range("AI" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AJ" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("AF" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AG" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AI" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AJ" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();


                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("AF" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AG" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AI" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AI", cel_index);
                                myExcelWorksheet.get_Range("AJ" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AJ", cel_index);

                            
                            }
                            #endregion FPLL Load Case 5

                            #region FPLL Load Case 6

                            frd = Analysis_Data.FRC_Results[i + 5][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("AL" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AM" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("AO" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AP" + cel_index.ToString(), misValue).Formula = "";


                                myExcelWorksheet.get_Range("AL" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AL", cel_index);
                                myExcelWorksheet.get_Range("AM" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AM", cel_index);

                                myExcelWorksheet.get_Range("AO" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AP" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("AL" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AM" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AO" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AP" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();



                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("AL" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AM" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AO" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AO", cel_index);
                                myExcelWorksheet.get_Range("AP" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AP", cel_index);

                            }
                            #endregion FPLL Load Case 6

                            #region FPLL Load Case 7

                            frd = Analysis_Data.FRC_Results[i + 6][j];

                            if (frd.BM >= 0)
                            {
                                myExcelWorksheet.get_Range("AR" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AS" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();

                                myExcelWorksheet.get_Range("AU" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AV" + cel_index.ToString(), misValue).Formula = "";



                                myExcelWorksheet.get_Range("AR" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AR", cel_index);
                                myExcelWorksheet.get_Range("AS" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AS", cel_index);

                                myExcelWorksheet.get_Range("AU" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AV" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                            }
                            else
                            {
                                myExcelWorksheet.get_Range("AR" + cel_index.ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AS" + cel_index.ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AU" + cel_index.ToString(), misValue).Formula = frd.BM.ToString();
                                myExcelWorksheet.get_Range("AV" + cel_index.ToString(), misValue).Formula = frd.SF.ToString();



                                //Adding 15% formaula

                                myExcelWorksheet.get_Range("AR" + (cel_index + incr_diff).ToString(), misValue).Formula = "";
                                myExcelWorksheet.get_Range("AS" + (cel_index + incr_diff).ToString(), misValue).Formula = "";

                                myExcelWorksheet.get_Range("AU" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AU", cel_index);
                                myExcelWorksheet.get_Range("AV" + (cel_index + incr_diff).ToString(), misValue).Formula = string.Format("={0}{1}*1.15", "AV", cel_index);

                            }
                            #endregion FPLL Load Case 4
                        }
                        else if (i < 2)
                        {
                            myExcelWorksheet.get_Range("H" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].BM.ToString();
                            myExcelWorksheet.get_Range("K" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].SF.ToString();
                        }

                        cel_index++;
                    }
                    iApp.Progress_OFF();

                    #endregion  Update bm and sf
                }

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["stress cont"];
                
                i = 25;
                cel_index = 346;
                incr_diff = 494 - 346;
                iApp.Progress_ON("Reading BM Corresponding SF for SINK Load....");

                for (int j = 0; j < Analysis_Data.FRC_Results[i].Count; j++)
                {
                    iApp.SetProgressValue(j, Analysis_Data.FRC_Results[i].Count);
                    myExcelWorksheet.get_Range("C" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].BM.ToString();
                    myExcelWorksheet.get_Range("D" + cel_index.ToString(), misValue).Formula = Analysis_Data.FRC_Results[i][j].SF.ToString();
                  
                    myExcelWorksheet.get_Range("C" + (cel_index + incr_diff).ToString(), misValue).Formula = Analysis_Data.FRC_Results[i + 1][j].BM.ToString();
                    myExcelWorksheet.get_Range("D" + (cel_index + incr_diff).ToString(), misValue).Formula = Analysis_Data.FRC_Results[i + 1][j].SF.ToString();

                    cel_index++;
                }

                iApp.Progress_Works.Clear();
                iApp.Progress_OFF();


                //myExcelWorksheet.Unprotect("2011ap");
                //myExcelWorksheet.Protect("2011ap");

            }








            myExcelWorkbook.Save();
            //myExcelWorkbook.Close(true, fileName, null);
            Marshal.ReleaseComObject(myExcelWorkbook);
        }


        public void Insert_Values_into_Excel_Box_Transverse()
        {

            Transverse = new Box_Transverse_Data(Report_File_Name);


            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet;
            Excel.Worksheet myExcelWorksheet_cwll;

            //String cellFormulaAsString = myExcelWorksheet.get_Range("A1", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            int cel_index = 11;

            //VDataCollection vdc;



            #region User Inputs


            if (Transverse != null)
            {
                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["BM SF"];
                myExcelWorksheet_cwll = (Excel.Worksheet)myExcelWorkbook.Sheets["CWLL"];


                int i = 0;

                for (i = 0; i < Transverse.DL_BM.Count; i++)
                {
                    myExcelWorksheet.get_Range("E" + cel_index.ToString(), misValue).Formula = Transverse.DL_BM[i].ToString();
                    myExcelWorksheet.get_Range("F" + cel_index.ToString(), misValue).Formula = Transverse.SIDL_BM[i].ToString();
                    myExcelWorksheet.get_Range("G" + cel_index.ToString(), misValue).Formula = Transverse.FPLL_BM[i].ToString();
                    myExcelWorksheet.get_Range("J" + cel_index.ToString(), misValue).Formula = Transverse.TEMP_POS_BM[i].ToString();
                    myExcelWorksheet.get_Range("K" + cel_index.ToString(), misValue).Formula = Transverse.TEMP_NEG_BM[i].ToString();


                    myExcelWorksheet_cwll.get_Range("E" + cel_index.ToString(), misValue).Formula = Transverse.CWLL_POS_BM[i].ToString();
                    myExcelWorksheet_cwll.get_Range("F" + cel_index.ToString(), misValue).Formula = Transverse.CWLL_NEG_BM[i].ToString();


                    cel_index++;
                    if(cel_index == 15 || cel_index == 24 || cel_index == 33 || cel_index == 38)
                        cel_index++;
                }


            }

            #endregion User Inputs

            myExcelWorkbook.Save();
            //myExcelWorkbook.Close(true, fileName, null);
            Marshal.ReleaseComObject(myExcelWorkbook);
        }


    }

    public class Live_Load_Box_Girder_Data_Collection : List<Live_Load_Box_Girder_Data>
    {
        public string Analysis_Type { get; set; }

        public Live_Load_Box_Girder_Data_Collection()
            : base()
        {
            Analysis_Type = "";
        }
    }
    public class Live_Load_Box_Girder_Data
    {
        public int Design_Node { get; set; }
        public double BM { get; set; }
        public double SF { get; set; }

        public Live_Load_Box_Girder_Data()
        {
            Design_Node = -1;
            BM = 0.0;
            SF = 0.0;
        }
        public override string ToString()
        {
            return string.Format("{0,10} {1,-10:f3} {2,-10:f3}", Design_Node, BM, SF);
        }
    }


    public class Box_Transverse_Data
    {
        public List<string> DL_BM { get; set; }
        public List<string> SIDL_BM { get; set; }
        public List<string> FPLL_BM { get; set; }
        public List<string> TEMP_POS_BM { get; set; }
        public List<string> TEMP_NEG_BM { get; set; }
        public List<string> CWLL_POS_BM { get; set; }
        public List<string> CWLL_NEG_BM { get; set; }
     
        public Box_Transverse_Data(string file_name)
        {
            DL_BM = new List<string>();
            SIDL_BM = new List<string>();
            FPLL_BM = new List<string>();
            TEMP_POS_BM = new List<string>();
            TEMP_NEG_BM = new List<string>();
            CWLL_POS_BM = new List<string>();
            CWLL_NEG_BM = new List<string>();

            Read_Data(file_name);
        }
        public void Read_Data(string file_name)
        {
            if (!File.Exists(file_name)) return;

            List<string> list = new List<string>(File.ReadAllLines(file_name));

            string kStr = "";

            MyList mlist = null;

            int indx = 0;


            DL_BM.Clear();
            SIDL_BM.Clear();
            FPLL_BM.Clear();
            TEMP_POS_BM.Clear();
            TEMP_NEG_BM.Clear();
            CWLL_POS_BM.Clear();
            CWLL_NEG_BM.Clear();


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].StartsWith("-------------------------") || list[i] == "") continue;
                kStr = MyList.RemoveAllSpaces(list[i].ToUpper());

                if (kStr.StartsWith("CANTILEVER")) indx = 1;
                else if (kStr.StartsWith("DECK")) indx = 2;
                else if (kStr.StartsWith("SOFFIT")) indx = 3;
                else if (kStr.StartsWith("LEFT WEB")) indx = 4;
                else if (kStr.StartsWith("RIGHT WEB")) indx = 5;



                if (indx != 0)
                {
                    mlist = new MyList(kStr, ' ');

                    try
                    {
                        CWLL_NEG_BM.Add(mlist.StringList[mlist.Count - 1]);
                        CWLL_POS_BM.Add(mlist.StringList[mlist.Count - 2]);

                        TEMP_NEG_BM.Add(mlist.StringList[mlist.Count - 3]);
                        TEMP_POS_BM.Add(mlist.StringList[mlist.Count - 4]);

                        FPLL_BM.Add(mlist.StringList[mlist.Count - 5]);
                        SIDL_BM.Add(mlist.StringList[mlist.Count - 6]);
                        DL_BM.Add(mlist.StringList[mlist.Count - 7]);
                    }
                    catch (Exception ex) { }
                }

            }
        }

    }


}
