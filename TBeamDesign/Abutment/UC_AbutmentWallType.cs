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


namespace BridgeAnalysisDesign.Abutment
{
    public partial class UC_AbutmentWallType : UserControl
    {

        public event EventHandler OnButtonClick;
        public event EventHandler OnProcess;

        IApplication iApp = null;
        string user_path = "";
        public UC_AbutmentWallType()
        {
            InitializeComponent();

            Load_Default_Data();
        }
        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Abutment Design with Wall Type in LSM";
            }
        }


        public void Load_Default_Data()
        {
            List<string> list = new List<string>();
            #region Span Data
            //list.Add(string.Format("TYPICAL LEVELS$Actual level$$"));
            list.Add(string.Format("TYPICAL LEVELS$$"));
            list.Add(string.Format("FORMATION LEVEL$205.500$m"));
            list.Add(string.Format("BEARING LEVEL$203.600$m"));
            list.Add(string.Format("ABUTMENT CAP LEVEL$203.100$m"));
            list.Add(string.Format("FRONT GROUND LEVEL$200.573$m"));
            list.Add(string.Format("SCOUR LEVEL$200.573$m$"));
            list.Add(string.Format("FOUNDING LEVEL$197.626$m"));
            list.Add(string.Format("HIGHEST FLOOD LEVEL$202.700$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("ABUTMENT DIMENSIONS$$"));
            list.Add(string.Format("WIDTH OF ABT. CAP$1.890$m"));
            list.Add(string.Format("WIDTH OF ABUTMENT$12.50$m"));
            list.Add(string.Format("WIDTH OF FOOTING$5.90$m"));
            list.Add(string.Format("WIDTH OF HEEL $2.50$m"));
            list.Add(string.Format("MIN. HEEL & TOE THICKNESS$0.85$m"));
            list.Add(string.Format("HEEL THICKNESS$1.70$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("STEM TOP THICKNESS$0.90$m"));
            list.Add(string.Format("STEM BOTTOM THICKNESS$0.90$m"));
            list.Add(string.Format("DIRT WALL THICKNESS$0.50$m"));
            list.Add(string.Format("DEPTH OF ABT. CAP (CONSTANT PORTION)$0.30$m"));
            list.Add(string.Format("DEPTH OF ABT. CAP (VARYING PORTION)$0.30$m"));
            list.Add(string.Format("THICKNESS OF RETURN WALL (Top)$0.50$m"));
            list.Add(string.Format("THICKNESS OF RETURN WALL (Bottom)$0.50$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("LENGTH OF CANTILEVER IN RETURN WALL$3.600$m"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("SOIL PARAMETERS$$"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Angle of Shear resistance$30$deg"));
            list.Add(string.Format("Density of Dry Backfill $2.00$t/m3"));
            //list.Add(string.Format("$$"));
            list.Add(string.Format("Safe Bearing Capacity (Normal)$30$t/m2"));
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
            file_path = Path.Combine(file_path, "Wall Type Abutment in LSM.xlsm");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Slab Bridge\Wall Type Abutment.xlsm");

            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                file_path = Path.Combine(Application.StartupPath, @"DESIGN\Limit State Method\Slab Bridge\Wall Type Abutment BS.xlsm");
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
            Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["INPUT"];



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

                    for (int i = 9; i <= 45; i++)
                    {
                        if (i == 10
                            || i == 13
                            || i == 16
                            || i >= 19 && i <= 21
                            || i == 28
                            || i == 36
                            || i == 38
                            || i == 39
                            || i >= 42 && i <= 44
                            )
                        {
                            continue;
                        }

                        EXL_INP.get_Range("R" + (i)).Formula = data[rindx++].ToString();
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

            list.Add(txt_inp_C2);
            list.Add(txt_inp_D10);
            list.Add(txt_inp_D4);
            list.Add(txt_inp_D6);
            list.Add(txt_inp_D9);
            list.Add(txt_inp_I2);
            list.Add(txt_inp_I3);
            list.Add(txt_inp_I4);
            list.Add(txt_inp_I5);
            list.Add(txt_inp_I6);
            list.Add(txt_inp_I7);
            list.Add(txt_inp_I8);
            list.Add(txt_inp_I9);

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
