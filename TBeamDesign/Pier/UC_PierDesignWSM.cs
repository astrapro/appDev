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
    public partial class UC_PierDesignWSM : UserControl
    {
        public UC_PierDesignWSM()
        {
            InitializeComponent();
        }

        public IApplication iApp = null;

        string user_path = "";

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Pier Design in Working Stress Method";
            }
        }

        public bool Show_Title
        {
            get
            {
                return lbl_Title.Visible;
            }
            set
            {
                lbl_Title.Visible = value;
            }
        }
        public string Left_Span_Force
        {
            get
            {
                return txt_des_G92.Text;
            }
            set
            {
                txt_des_G92.Text = value;
                //txt_des_G92.ForeColor = Color.Red;
                //lbl_note.Visible = true;
            }
        }
        public string Right_Span_Force
        {
            get
            {
                return txt_des_I92.Text;
            }
            set
            {
                txt_des_I92.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }
        public bool Show_Note
        {
            get
            {

                if (lbl_note.Visible)
                {
                    txt_des_G92.ForeColor = Color.Red;
                    txt_des_I92.ForeColor = Color.Red;
                }
                else
                {
                    txt_des_G92.ForeColor = Color.Black;
                    txt_des_I92.ForeColor = Color.Black;

                }
                return lbl_note.Visible;
            }
            set
            {
                lbl_note.Visible = value;
                if (value)
                {
                    txt_des_G92.ForeColor = Color.Red;
                    txt_des_I92.ForeColor = Color.Red;
                }
                else
                {
                    txt_des_G92.ForeColor = Color.Black;
                    txt_des_I92.ForeColor = Color.Black;

                }

            }
        }
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
            else if (lbl == lbl_8) pnl = pnl_8;

            pnl.Visible = !pnl.Visible;

            if (pnl.Visible) lbl.ForeColor = Color.Black;
            else lbl.ForeColor = Color.Blue;

        }

        public event EventHandler OnProcess;
        private void btn_process_Click(object sender, EventArgs e)
        {
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                Pier_Process_Design_IS();
            }
            else
            {
                Pier_Process_Design_BS();
            }
            if (OnProcess != null) OnProcess(sender, e);
        }
        private void Pier_Process_Design_IS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if(iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Pier_Design_LS_IRC.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier with open foundation IS.xlsx");

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

        public List<TextBox> Get_TextBoxes()
        {
            List<TextBox> list = new List<TextBox>();
            list.Add(txt_des_G92);
            list.Add(txt_des_I92);
            list.Add(txt_des_G93);
            list.Add(txt_des_I93);


            list.Add(txt_des_F85);
            list.Add(txt_des_F87);

            list.Add(txt_des_G76);
            list.Add(txt_des_I76);
            list.Add(txt_des_G78);
            list.Add(txt_des_G79);
            list.Add(txt_des_G80);

            list.Add(txt_des_G68);
            list.Add(txt_des_I68);
            list.Add(txt_des_G69);
            list.Add(txt_des_I69);
            list.Add(txt_des_G70);
            list.Add(txt_des_I70);
            list.Add(txt_des_G71);
            list.Add(txt_des_I71);

            list.Add(txt_des_G57);
            list.Add(txt_des_G58);
            list.Add(txt_des_G60);
            list.Add(txt_des_I60);
            list.Add(txt_des_K60);
            list.Add(txt_des_G61);
            list.Add(txt_des_I61);
            list.Add(txt_des_K61);
            list.Add(txt_des_G62);
            list.Add(txt_des_I62);
            list.Add(txt_des_G63);
            list.Add(txt_des_I63);

            list.Add(txt_des_G51);
            list.Add(txt_des_I51);
            list.Add(txt_des_K51);
            list.Add(txt_des_G52);
            list.Add(txt_des_I52);
            list.Add(txt_des_K52);

            list.Add(txt_des_G39);
            list.Add(txt_des_G40);
            list.Add(txt_des_G41);
            list.Add(txt_des_G42);
            list.Add(txt_des_I42);
            list.Add(txt_des_G43);
            list.Add(txt_des_I43);
            list.Add(txt_des_G45);
            list.Add(txt_des_G46);
            list.Add(txt_des_I46);

            list.Add(txt_des_G16);
            list.Add(txt_des_I16);
            list.Add(txt_des_G17);
            list.Add(txt_des_I17);
            list.Add(txt_des_G18);
            list.Add(txt_des_G19);
            list.Add(txt_des_G20);
            list.Add(txt_des_G22);
            list.Add(txt_des_I22);
            list.Add(txt_des_G23);
            list.Add(txt_des_I23);
            list.Add(txt_des_G24);
            list.Add(txt_des_G26);
            list.Add(txt_des_G27);
            list.Add(txt_des_G28);
            list.Add(txt_des_G29);
            list.Add(txt_des_G30);
            list.Add(txt_des_G31);

            return list;
        }
        private void Pier_Process_Design_BS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Pier with open foundation.xlsx");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier with open foundation BS.xlsx");

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
