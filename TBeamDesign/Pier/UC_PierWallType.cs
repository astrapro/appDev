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


namespace BridgeAnalysisDesign.Pier
{
    public partial class UC_PierWallType : UserControl
    {
        public UC_PierWallType()
        {
            InitializeComponent();
            Load_Default_Data();
        }

        public event EventHandler OnButtonClick;
        public event EventHandler OnProcess;

        IApplication iApp = null;
        string user_path = "";
       
        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Pier Design with Wall Type in LSM";
            }
        }


        public void Load_Default_Data()
        {
            List<string> list = new List<string>();
            #region Span Data
            list.Add(string.Format("Left Span$11.00$m"));
            list.Add(string.Format("Right Span$11.00$m"));
            list.Add(string.Format("Formation Level$97.200$m"));
            list.Add(string.Format("Founding level$83.015$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Super structure Details$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Wearing coat$65$mm"));
            list.Add(string.Format("Bearing to Bearing distance at pier$0.500$m"));
            list.Add(string.Format("Height of bearing+pedestal$0.006$m"));
            list.Add(string.Format("Overall Width of Superstructure$8.400$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Width of safety kerb/ footpath (L/S)$0.000$m"));
            list.Add(string.Format("Width of safety kerb/ footpath (R/S)$0.000$m"));
            list.Add(string.Format("Width of Railing/ Crash Barrier (L/S)$0.450$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Depth of Superstructure (at C/L)$0.903$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("No. of bearings$0$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("mu$0.050$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Type of Substructure$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Length of Pier (L-L)$0.800$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Type of Foundations$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of concrete of Pier$M35$"));
            list.Add(string.Format("Grade of concrete of Steel$Fe500$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Foundation Details$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Length of Footing (L-L)$4.500$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Depth of Footing at tip$0.300$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Depth of Footing at junction$1.200$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Properties of Soil$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Saturated Density$2.200$t/cu.m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format(" Dry Density$2.000$t/cu.m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Levels"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("HFL$94.677 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Ground Level$86.015 $m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Bearing Capacity$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Safe Bearing capacity  (Normal)$25.0$t/m2"));
            #endregion Span Data

            MyList.Fill_List_to_Grid(dgv_input_data, list, '$');
            MyList.Modified_Cell(dgv_input_data);
        }

        public void SetIApplication(IApplication iApp)
        {
            this.iApp = iApp;
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
            }
            else
            {
                //Load_Live_Loads();
            }
        }


        private void btn_proceed_Click(object sender, EventArgs e)
        {

            if (OnButtonClick != null)
            {
                OnButtonClick(sender, e);
            }

            if (iApp == null) return;
            //MessageBox.Show(this, "The Design in Excel Worksheet will take some time to complete. Please wait until the process is complete as shown at the bottom of the Excel Worksheet.", "ASTRA", MessageBoxButtons.OK);

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                Process_Design_IS();
            }
            else
            {
                Process_Design_IS();
                //Process_Design_BS();
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

            //file_path = Path.Combine(file_path, "Abutment with Open Foundation.xlsm");
            file_path = Path.Combine(file_path, "Pier_wall_Limit_State.xls");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Slab Bridge\Pier_wall_LSM_IS.xls");

            if(iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Slab Bridge\Pier_wall_LSM_BS.xls");
            }

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
            Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["Salient"];



            List<TextBox> All_Data = MyList.Get_TextBoxes(this);


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);


            List<double> data = new List<double>();
            try
            {
                string kStr = "";
                foreach (var item in All_Data)
                {
                    if (item.Name.ToLower().StartsWith("txt_inp_"))
                    {
                        kStr = item.Name.Replace("txt_inp_", "");
                        //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                        EXL_INP.get_Range(kStr).Formula = item.Text;
                    }
                    else
                    {
                         
                    }
                }

                #region Input 2

                DataGridView dgv = dgv_input_data;
                try
                {
                    double val = 0.0;
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        if (dgv[1, i].Value.ToString() != "")
                        {
                            data.Add(MyList.StringToDouble(dgv[1, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", ""), 0.0));
                        }
                    }

                    int rindx = 0;

                    for (int i = 5; i <= 81; i++)
                    {
                        if (i >= 9 && i <= 13
                            || i == 18
                            || i >= 22 && i <= 23
                            || i >= 25 && i <= 27
                            || i == 29
                            || i >= 31 && i <= 37
                            || i >= 39 && i <= 43
                            || i >= 46 && i <= 50
                            || i == 52
                            || i == 54
                            || i >= 56 && i <= 58
                            || i == 60
                            || i >= 56 && i <= 58
                            || i >= 62 && i <= 70
                            || i == 72
                            || i >= 74 && i <= 80
                            )
                        {
                            continue;
                        }

                        EXL_INP.get_Range("F" + (i)).Formula = data[rindx++].ToString();
                    }


                }
                catch (Exception exx) { }



                #endregion Input 2


            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Close_Message();
        }

        public List<TextBox> Get_TextBoxes()
        {
            List<TextBox> list = new List<TextBox>();

            //list.Add(txt_inp_C2);
            //list.Add(txt_inp_D10);
            //list.Add(txt_inp_D4);
            //list.Add(txt_inp_D6);
            //list.Add(txt_inp_D9);
            //list.Add(txt_inp_I2);
            //list.Add(txt_inp_I3);
            //list.Add(txt_inp_I4);
            //list.Add(txt_inp_I5);
            //list.Add(txt_inp_I6);
            //list.Add(txt_inp_I7);
            //list.Add(txt_inp_I8);
            //list.Add(txt_inp_I9);

            return list;
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

        public event EventHandler Worksheet_Force_CheckedChanged;
        
        private void btn_Excel_Notes_Click(object sender, EventArgs e)
        {
            if (iApp != null) iApp.Open_Excel_Macro_Notes();
        }

    }
}
