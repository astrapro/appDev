using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace LimitStateMethod.Composite
{
    public partial class UC_CompositeBridgeLSM : UserControl
    {
        IApplication iapp = null;

        public IApplication iApp
        {
            get
            {
                return iapp;
            }
            set
            {
                iapp = value;

                //Load_Live_Loads();

            }
        }


        string user_path = "";

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Steel Girder Bridge Design in Limit State Method";
            }
        }

        public bool Show_Title
        {
            get
            {
                return lbl_title.Visible;
            }
            set
            {
                lbl_title.Visible = value;
            }
        }

        public UC_CompositeBridgeLSM()
        {
            InitializeComponent();
        }
        public List<TextBox> Get_GEN_Textboxes()
        {
            List<TextBox> list = new List<TextBox>();

            list.Add(txt_GEN_G2);
            list.Add(txt_GEN_G3);
            list.Add(txt_GEN_G4);

            list.Add(txt_GEN_G7);
            list.Add(txt_GEN_G9);
            list.Add(txt_GEN_G10);
            list.Add(txt_GEN_G11);


            list.Add(txt_GEN_G13);
            list.Add(txt_GEN_G14);

            list.Add(txt_GEN_G15);
            list.Add(txt_GEN_G17);
            list.Add(txt_GEN_G18);

            list.Add(txt_GEN_G20);

            list.Add(txt_GEN_G21);
            list.Add(txt_GEN_I21);

            list.Add(txt_GEN_G25);

            list.Add(txt_GEN_G30);


            list.Add(txt_GEN_F33);

            list.Add(txt_GEN_G35);

            list.Add(txt_GEN_G37);
            list.Add(txt_GEN_G38);

            list.Add(txt_GEN_G39);

            return list;
        }
        public List<TextBox> Get_SUMM_Textboxes()
        {
            List<TextBox> list = new List<TextBox>();


            //list.Add(txt_SUMM_I13);


            list.Add(txt_SUMM_J13);
            list.Add(txt_SUMM_K13);
            list.Add(txt_SUMM_L13);
            list.Add(txt_SUMM_M13);
            list.Add(txt_SUMM_N13);


            //list.Add(txt_SUMM_I15);

            list.Add(txt_SUMM_J15);
            list.Add(txt_SUMM_K15);
            list.Add(txt_SUMM_L15);
            list.Add(txt_SUMM_M15);
            list.Add(txt_SUMM_N15);



            //list.Add(txt_SUMM_I16);


            list.Add(txt_SUMM_J16);
            list.Add(txt_SUMM_K16);
            list.Add(txt_SUMM_L16);
            list.Add(txt_SUMM_M16);
            list.Add(txt_SUMM_N16);



            //list.Add(txt_SUMM_I17);


            list.Add(txt_SUMM_J17);
            list.Add(txt_SUMM_K17);
            list.Add(txt_SUMM_L17);
            list.Add(txt_SUMM_M17);
            list.Add(txt_SUMM_N17);



            list.Add(txt_SUMM_I21);
            list.Add(txt_SUMM_J21);
            list.Add(txt_SUMM_K21);
            list.Add(txt_SUMM_L21);
            list.Add(txt_SUMM_M21);
            list.Add(txt_SUMM_N21);



            list.Add(txt_SUMM_I73);
            list.Add(txt_SUMM_J73);
            list.Add(txt_SUMM_K73);
            list.Add(txt_SUMM_L73);
            list.Add(txt_SUMM_M73);
            list.Add(txt_SUMM_N73);



            list.Add(txt_SUMM_I75);
            list.Add(txt_SUMM_J75);
            list.Add(txt_SUMM_K75);
            list.Add(txt_SUMM_L75);
            list.Add(txt_SUMM_M75);
            list.Add(txt_SUMM_N75);

            list.Add(txt_SUMM_I76);
            list.Add(txt_SUMM_J76);
            list.Add(txt_SUMM_K76);
            list.Add(txt_SUMM_L76);
            list.Add(txt_SUMM_M76);
            list.Add(txt_SUMM_N76);



            list.Add(txt_SUMM_I77);
            list.Add(txt_SUMM_J77);
            list.Add(txt_SUMM_K77);
            list.Add(txt_SUMM_L77);
            list.Add(txt_SUMM_M77);
            list.Add(txt_SUMM_N77);


            //list.Add(txt_SUMM_H81);
            list.Add(txt_SUMM_I81);
            list.Add(txt_SUMM_J81);
            list.Add(txt_SUMM_K81);
            list.Add(txt_SUMM_L81);
            list.Add(txt_SUMM_M81);
            list.Add(txt_SUMM_N81);
            return list;
        }

        public event EventHandler OnProcess;
        private void btn_process_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(this, "This Process will take few minuites, please wail until the Message Box comes...", "ASTRA", MessageBoxButtons.OK);


            //MessageBox.Show(this, "The Design in Excel Worksheet will take some time to complete. Please wait until the process is complete as shown at the bottom of the Excel Worksheet.", "ASTRA", MessageBoxButtons.OK);

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                Process_Design_IS();
            }
            else
            {
                Process_Design_BS();
            }
            if (OnProcess != null) OnProcess(sender, e);
        }
        private void Process_Design_IS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Sleel Girder Design in Limit State Method (IRC).xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier Design in Limit State Method.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Steel Girder IS\STEEL GIRDER LSM IS.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["GEN"];


            List<TextBox> All_Data = Get_GEN_Textboxes();




            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);


            List<double> data = new List<double>();
            try
            {
                string kStr = "";

                #region GEN Tab

                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_GEN_", "");

                    try
                    {
                        //if (kStr == "G14")
                        //{
                        //    myExcelWorksheet.get_Range(kStr).Formula = item.Text + "%";
                        //}
                        //else
                            myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                    }
                    catch (Exception exx) { }
                }
                #endregion GEN Tab

                #region SUMM Tab


                All_Data = Get_SUMM_Textboxes();

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SUMM"];


                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_SUMM_", "");

                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #endregion SUP Tab

            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }

        private void Process_Design_BS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Sleel Girder Design in Limit State Method (BS).xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier Design in Limit State Method.xlsx");
            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\PIER_L_BS.xlsm");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Steel Girder BS\STEEL GIRDER LSM BS.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            iApp.Excel_Open_Message();

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["GEN"];


            List<TextBox> All_Data = Get_GEN_Textboxes();




            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);


            List<double> data = new List<double>();
            try
            {
                string kStr = "";

                #region GEN Tab

                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_GEN_", "");

                    try
                    {
                        //if (kStr == "G14")
                        //{
                        //    myExcelWorksheet.get_Range(kStr).Formula = item.Text + "%";
                        //}
                        //else
                        myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                    }
                    catch (Exception exx) { }
                }
                #endregion GEN Tab

                #region SUMM Tab


                All_Data = Get_SUMM_Textboxes();

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SUMM"];


                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_SUMM_", "");

                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #endregion SUP Tab

                //myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();

            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }
    }
}
