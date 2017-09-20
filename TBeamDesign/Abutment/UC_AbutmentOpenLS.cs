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
    public partial class UC_AbutmentOpenLS : UserControl
    {
        public UC_AbutmentOpenLS()
        {
            InitializeComponent();
        }


        IApplication iApp = null;
        string user_path = "";
        public event EventHandler OnProcess;
        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Abutment Design with Open Foundation in LSM";
            }
        }

        #region Structure Details

        public string Bridge_Type { get { return txt_xls_inp_C23.Text; } set { txt_xls_inp_C23.Text = value; } }
        public string Girder_Depth { get { return txt_xls_inp_E24.Text; } set { txt_xls_inp_E24.Text = value; } }
        public string Slab_Thickness { get { return txt_xls_inp_E25.Text; } set { txt_xls_inp_E25.Text = value; } }
        public string Wearing_Coat_Thickness { get { return txt_xls_inp_E26.Text; } set { txt_xls_inp_E26.Text = value; } }
        public string Girder_Nos { get { return txt_xls_inp_E29.Text; } set { txt_xls_inp_E29.Text = value; } }
        public string Girder_Spacing { get { return txt_xls_inp_E30.Text; } set { txt_xls_inp_E30.Text = value; } }

        #endregion Structure Details


        #region Super Structure Details
        public string Span { get { return txt_xls_inp_E9.Text; } set { txt_xls_inp_E9.Text = value; } }
        public string Exp_Gap { get { return txt_xls_inp_E11.Text; } set { txt_xls_inp_E11.Text = value; } }

        public string Carriageway_width { get { return txt_xls_inp_E14.Text; } set { txt_xls_inp_E14.Text = value; } }
        public string Cross_Camber { get { return txt_xls_inp_E16.Text; } set { txt_xls_inp_E16.Text = value; } }
        public string Railing { get { return txt_xls_inp_E17.Text; } set { txt_xls_inp_E17.Text = value; } }
        public string Crash_Barrier { get { return txt_xls_inp_E18.Text; } set { txt_xls_inp_E18.Text = value; } }
        public string Foot_path { get { return txt_xls_inp_E19.Text; } set { txt_xls_inp_E19.Text = value; } }

        #endregion Super Structure Details


        #region Material Details
        public string Concrete_Grade { get { return txt_xls_inp_H88.Text; } set { txt_xls_inp_H88.Text = value; } }
        public string Concrete_Reinforcement { get { return txt_xls_inp_H90.Text; } set { txt_xls_inp_H90.Text = value; } }
        public string RCC_Density { get { return txt_xls_inp_H92.Text; } set { txt_xls_inp_H92.Text = value; } }
        public string Crash_Barrier_weight { get { return txt_xls_inp_H93.Text; } set { txt_xls_inp_H93.Text = value; } }
        public string Wearing_coat_load { get { return txt_xls_inp_H94.Text; } set { txt_xls_inp_H94.Text = value; } }
        public string Foot_Path_Live_Load { get { return txt_xls_inp_H95.Text; } set { txt_xls_inp_H95.Text = value; } }
        public string Railing_weight { get { return txt_xls_inp_H96.Text; } set { txt_xls_inp_H96.Text = value; } }
        
        
        
        
        
        #endregion Super Structure Details

        public void SetIApplocation(IApplication iApp)
        {
            this.iApp = iApp;
        }


        //public bool Show_Title
        //{
        //    get
        //    {
        //        return lbl_Title.Visible;
        //    }
        //    set
        //    {
        //        lbl_Title.Visible = value;
        //    }
        //}

        private void lbl_1_DoubleClick(object sender, EventArgs e)
        {
            Label lbl = sender as Label;

            Panel pnl = pnl_1;


            if (lbl == lbl_1) pnl = pnl_1;
            else if (lbl == lbl_2) pnl = pnl_2;
            else if (lbl == lbl_3) pnl = pnl_3;
            else if (lbl == lbl_4) pnl = pnl_4;
            else if (lbl == lbl_5) pnl = pnl_5;
            else if (lbl == lbl_6) pnl = pnl_6;
            else if (lbl == lbl_7) pnl = pnl_7;
            //else if (lbl == lbl_8) pnl = pnl_8;

            pnl.Visible = !pnl.Visible;

            if (pnl.Visible) lbl.ForeColor = Color.Black;
            else lbl.ForeColor = Color.Blue;

        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {

            MessageBox.Show(this, "The Design in Excel Worksheet will take some time to complete. Please wait until the process is complete as shown at the bottom of the Excel Worksheet.", "ASTRA", MessageBoxButtons.OK);

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

            file_path = Path.Combine(file_path, "Abutment with Open Foundation.xlsm");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Limit State\Abutment with Open Foundation.xlsm");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["1.0 Input"];


            List<TextBox> All_Data = Get_TextBoxes();


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);


            List<double> data = new List<double>();
            try
            {
                string kStr = "";
                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_xls_inp_", "");

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #region Input 2

                //myExcelWorksheet.get_Range("K78").Formula = data[rindx++].ToString();
                //myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();

                #endregion Input 2


            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }

        public List<TextBox> Get_TextBoxes()
        {
            List<TextBox> list = new List<TextBox>();
            //list.Add(txt_des_G92);

            list.Add(txt_xls_inp_A103);
            list.Add(txt_xls_inp_B34);
            list.Add(txt_xls_inp_B40);
            list.Add(txt_xls_inp_B45);
            list.Add(txt_xls_inp_C102);
            list.Add(txt_xls_inp_C109);
            list.Add(txt_xls_inp_C23);
            list.Add(txt_xls_inp_C50);
            list.Add(txt_xls_inp_D101);
            list.Add(txt_xls_inp_D111);
            list.Add(txt_xls_inp_D121);
            list.Add(txt_xls_inp_D129);
            list.Add(txt_xls_inp_D130);
            list.Add(txt_xls_inp_D36);
            list.Add(txt_xls_inp_D37);
            list.Add(txt_xls_inp_D40);
            list.Add(txt_xls_inp_D42);
            list.Add(txt_xls_inp_D43);
            list.Add(txt_xls_inp_D60);
            list.Add(txt_xls_inp_E10);
            list.Add(txt_xls_inp_E11);
            list.Add(txt_xls_inp_E111);
            list.Add(txt_xls_inp_E125);
            list.Add(txt_xls_inp_E126);
            list.Add(txt_xls_inp_E132);
            list.Add(txt_xls_inp_E133);
            list.Add(txt_xls_inp_E134);
            list.Add(txt_xls_inp_E14);
            list.Add(txt_xls_inp_E16);
            list.Add(txt_xls_inp_E17);
            list.Add(txt_xls_inp_E18);
            list.Add(txt_xls_inp_E19);
            list.Add(txt_xls_inp_E2);
            list.Add(txt_xls_inp_E24);
            list.Add(txt_xls_inp_E25);
            list.Add(txt_xls_inp_E26);
            list.Add(txt_xls_inp_E27);
            list.Add(txt_xls_inp_E28);
            list.Add(txt_xls_inp_E29);
            list.Add(txt_xls_inp_E3);
            list.Add(txt_xls_inp_E30);
            list.Add(txt_xls_inp_E4);
            list.Add(txt_xls_inp_E5);
            list.Add(txt_xls_inp_E6);
            list.Add(txt_xls_inp_E7);
            list.Add(txt_xls_inp_E9);
            list.Add(txt_xls_inp_F104);
            list.Add(txt_xls_inp_F60);
            list.Add(txt_xls_inp_G125);
            list.Add(txt_xls_inp_G28);
            list.Add(txt_xls_inp_H106);
            list.Add(txt_xls_inp_H108);
            list.Add(txt_xls_inp_H120);
            list.Add(txt_xls_inp_H122);
            list.Add(txt_xls_inp_H88);
            list.Add(txt_xls_inp_H90);
            list.Add(txt_xls_inp_H92);
            list.Add(txt_xls_inp_H93);
            list.Add(txt_xls_inp_H94);
            list.Add(txt_xls_inp_H95);
            list.Add(txt_xls_inp_H96);
            list.Add(txt_xls_inp_I28);

            return list;
        }
        private void Process_Design_BS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Abutment with Open Foundation.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment with Open Foundation LS\Abutment with Open Foundation.xlsx");

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
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Design Data"];


            List<TextBox> All_Data = Get_TextBoxes();


            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);


            List<double> data = new List<double>();
            try
            {
                string kStr = "";
                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_des_", "");

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #region Input 2

                //myExcelWorksheet.get_Range("K78").Formula = data[rindx++].ToString();
                //myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();

                #endregion Input 2


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
