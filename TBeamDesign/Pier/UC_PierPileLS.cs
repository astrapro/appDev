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
    public partial class UC_PierPileLS : UserControl
    {
        public UC_PierPileLS()
        {
            InitializeComponent();
        }

        IApplication iApp = null;
        public event EventHandler OnProcess;
        string user_path = "";

        public void SetIApplication(IApplication iApp)
        {
            this.iApp = iApp;

            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                rbtn_value_worksheet.Checked = true;
            }
        }

        public bool Is_Force_From_Analysis
        {
            get
            {
                return rbtn_value_analysis.Checked;
            }
            set
            {
                rbtn_value_worksheet.Checked = !value;
                rbtn_value_analysis.Checked = value;
            }
        }
        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Pier Design with Pile Foundation in LSM";
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
                //lbl_Title.Visible = value;
            }
        }




        #region Span Details


        public string CC_Exp_Joint_Left_Skew { get { return txt_xls_inp_H7.Text; } set { txt_xls_inp_H7.Text = value; } }
        public string CC_Exp_Joint_Right_Skew { get { return txt_xls_inp_J7.Text; } set { txt_xls_inp_J7.Text = value; } }


        public string CC_Exp_Joint_CL_Brg_Left_Skew { get { return txt_xls_inp_H8.Text; } set { txt_xls_inp_H8.Text = value; } }
        public string CC_Exp_Joint_CL_Brg_Right_Skew { get { return txt_xls_inp_J8.Text; } set { txt_xls_inp_J8.Text = value; } }


        public string CC_Exp_Gap_Left { get { return txt_xls_inp_G9.Text; } set { txt_xls_inp_G9.Text = value; } }
        public string CC_Exp_Gap_Right { get { return txt_xls_inp_I9.Text; } set { txt_xls_inp_I9.Text = value; } }






        #endregion Span Details


        #region Super Structure Details

        public string SkewAngle { get { return txt_xls_inp_E4.Text; } set { txt_xls_inp_E4.Text = value; } }


        public string CarriageWidth_Left { get { return txt_xls_inp_G15.Text; } set { txt_xls_inp_G15.Text = value; } }
        public string CarriageWidth_Right { get { return txt_xls_inp_I15.Text; } set { txt_xls_inp_I15.Text = value; } }


        public string CrashBarierWidth_Nos { get { return txt_xls_inp_D16.Text; } set { txt_xls_inp_D16.Text = value; } }
        public string CrashBarierWidth_Left { get { return txt_xls_inp_G16.Text; } set { txt_xls_inp_G16.Text = value; } }
        public string CrashBarierWidth_Right { get { return txt_xls_inp_I16.Text; } set { txt_xls_inp_I16.Text = value; } }



        public string FootPathWidth_Nos { get { return txt_xls_inp_D17.Text; } set { txt_xls_inp_D17.Text = value; } }
        public string FootPathWidth_Left { get { return txt_xls_inp_G17.Text; } set { txt_xls_inp_G17.Text = value; } }
        public string FootPathWidth_Right { get { return txt_xls_inp_I17.Text; } set { txt_xls_inp_I17.Text = value; } }


        public string RailingWidth_Nos { get { return txt_xls_inp_D18.Text; } set { txt_xls_inp_D18.Text = value; } }
        public string RailingWidth_Left { get { return txt_xls_inp_G18.Text; } set { txt_xls_inp_G18.Text = value; } }
        public string RailingWidth_Right { get { return txt_xls_inp_I18.Text; } set { txt_xls_inp_I18.Text = value; } }


        public string CrashBarierHeight_Left { get { return txt_xls_inp_G19.Text; } set { txt_xls_inp_G19.Text = value; } }
        public string CrashBarierHeight_Right { get { return txt_xls_inp_I19.Text; } set { txt_xls_inp_I19.Text = value; } }



        public string WearingCoatThickness_Left { get { return txt_xls_inp_G22.Text; } set { txt_xls_inp_G22.Text = value; } }
        public string WearingCoatThickness_Right { get { return txt_xls_inp_I22.Text; } set { txt_xls_inp_I22.Text = value; } }


        public string GirderDepth_Left { get { return txt_xls_inp_G25.Text; } set { txt_xls_inp_G25.Text = value; } }
        public string GirderDepth_Right { get { return txt_xls_inp_I25.Text; } set { txt_xls_inp_I25.Text = value; } }



        public string SlabDepth_Left { get { return txt_xls_inp_G26.Text; } set { txt_xls_inp_G26.Text = value; } }
        public string SlabDepth_Right { get { return txt_xls_inp_I26.Text; } set { txt_xls_inp_I26.Text = value; } }


        public string TopFlangeWidth_Left { get { return txt_xls_inp_G28.Text; } set { txt_xls_inp_G28.Text = value; } }
        public string TopFlangeWidth_Right { get { return txt_xls_inp_I26.Text; } set { txt_xls_inp_I28.Text = value; } }


        public string CrossGirderWidth_Left { get { return txt_xls_inp_G30.Text; } set { txt_xls_inp_G30.Text = value; } }
        public string CrossGirderWidth_Right { get { return txt_xls_inp_I30.Text; } set { txt_xls_inp_I30.Text = value; } }


        public string CrossGirderNos_Left { get { return txt_xls_inp_G31.Text; } set { txt_xls_inp_G31.Text = value; } }

        public string CrossGirderNos_Right { get { return txt_xls_inp_I31.Text; } set { txt_xls_inp_I31.Text = value; } }

        #endregion Super Structure Details

        public void Calculated_Values()
        {
            double skewDeg = MyList.StringToDouble(SkewAngle, 0.0);

            double cval = 0.0;
            double skewRad = MyList.Convert_Degree_To_Radian(skewDeg);
            //=H7*COS(G4)

            cval = MyList.StringToDouble(txt_xls_inp_H7, 0.0) * Math.Cos(skewRad);
            txt_xls_inp_G7.Text = cval.ToString("f3");

            //=J7*COS(G4)
            cval = MyList.StringToDouble(txt_xls_inp_J7, 0.0) * Math.Cos(skewRad);
            txt_xls_inp_I7.Text = cval.ToString("f3");

            //
            cval = MyList.StringToDouble(txt_xls_inp_H8, 0.0) * Math.Cos(skewRad);
            txt_xls_inp_G8.Text = cval.ToString("f3");


            //
            cval = MyList.StringToDouble(txt_xls_inp_J8, 0.0) * Math.Cos(skewRad);
            txt_xls_inp_I8.Text = cval.ToString("f3");



            //=G9/COS($G$4)
            cval = MyList.StringToDouble(txt_xls_inp_G9, 0.0) / Math.Cos(skewRad);
            txt_xls_inp_H9.Text = cval.ToString("f3");


            //=I9/COS($G$4)
            cval = MyList.StringToDouble(txt_xls_inp_I9, 0.0) / Math.Cos(skewRad);
            txt_xls_inp_J9.Text = cval.ToString("f3");

        }
        public void Left_Equal_to_Right()
        {
            CC_Exp_Joint_Right_Skew = CC_Exp_Joint_Left_Skew;

            CC_Exp_Joint_CL_Brg_Right_Skew = CC_Exp_Joint_CL_Brg_Left_Skew;

            CC_Exp_Gap_Right = CC_Exp_Gap_Left;


            CarriageWidth_Right = CarriageWidth_Left;
            CrashBarierWidth_Right = CrashBarierWidth_Left;
            FootPathWidth_Right = FootPathWidth_Left;


            RailingWidth_Right = RailingWidth_Left;
            CrashBarierHeight_Right = CrashBarierHeight_Left;



            WearingCoatThickness_Right = WearingCoatThickness_Left;

            GirderDepth_Right = GirderDepth_Left;
            SlabDepth_Right = SlabDepth_Left;
            TopFlangeWidth_Right = TopFlangeWidth_Left;
            CrossGirderWidth_Right = CrossGirderWidth_Left;
            CrossGirderNos_Right = CrossGirderNos_Left;

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
            //else if (lbl == lbl_7) pnl = pnl_7;
            //else if (lbl == lbl_8) pnl = pnl_8;

            pnl.Visible = !pnl.Visible;

            if (pnl.Visible) lbl.ForeColor = Color.Black;
            else lbl.ForeColor = Color.Blue;

        }

        public event EventHandler OnButtonClick;

        private void btn_proceed_Click(object sender, EventArgs e)
        {

            MessageBox.Show(this, "The Design in Excel Worksheet will take some time to complete. Please wait until the process is complete as shown at the bottom of the Excel Worksheet.", "ASTRA", MessageBoxButtons.OK);

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
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Pier with Pile Foundation IS.xlsm");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier with Pile Foundation IS.xlsm");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }
            else
            {
                MessageBox.Show(file_path + " file not found.");
                return;
            }


            //iApp.Excel_Open_Message();

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
            Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["1. Lvl & Dim."];
            Excel.Worksheet EXL_DES = (Excel.Worksheet)myExcelWorkbook.Sheets["2.Design Paramters"];


            //List<TextBox> All_Data = Get_TextBoxes();

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
                    if (item.Name.ToLower().StartsWith("txt_xls_inp_"))
                    {
                        kStr = item.Name.Replace("txt_xls_inp_", "");

                        //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                        EXL_INP.get_Range(kStr).Formula = item.Text;
                    }

                    #region Input 2

                    else if (item.Name.ToLower().StartsWith("txt_xls_des_"))
                    {
                        kStr = item.Name.Replace("txt_xls_des_", "");
                        //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                        EXL_DES.get_Range(kStr).Formula = item.Text;
                    }

                    #endregion Input 2
                }



            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }

        public List<TextBox> Get_TextBoxes()
        {
            List<TextBox> list = new List<TextBox>();
            //list.Add(txt_xls_des_F61);
            //list.Add(txt_xls_des_F62);
            list.Add(txt_xls_des_G12);
            list.Add(txt_xls_des_G14);
            list.Add(txt_xls_des_G15);
            list.Add(txt_xls_des_G16);
            list.Add(txt_xls_des_G17);
            list.Add(txt_xls_des_G18);
            list.Add(txt_xls_des_G36);
            list.Add(txt_xls_des_G37);
            list.Add(txt_xls_des_G4);
            list.Add(txt_xls_des_G65);
            list.Add(txt_xls_des_G66);
            list.Add(txt_xls_des_G68);
            //list.Add(txt_xls_des_H61);
            //list.Add(txt_xls_des_H62);
            //list.Add(txt_xls_des_J61);
            //list.Add(txt_xls_des_J62);


            //list.Add(txt_xls_inp_A122);
            //list.Add(txt_xls_inp_A153);
            list.Add(txt_xls_inp_D16);
            list.Add(txt_xls_inp_D17);
            list.Add(txt_xls_inp_D18);
            list.Add(txt_xls_inp_E119_);
            list.Add(txt_xls_inp_E119);
            list.Add(txt_xls_inp_E132);
            list.Add(txt_xls_inp_E4);
            list.Add(txt_xls_inp_E92);
            list.Add(txt_xls_inp_G152);
            list.Add(txt_xls_inp_G15);
            list.Add(txt_xls_inp_G16);
            list.Add(txt_xls_inp_G17);
            list.Add(txt_xls_inp_G18);
            list.Add(txt_xls_inp_G19);
            list.Add(txt_xls_inp_G20);
            list.Add(txt_xls_inp_G21);
            list.Add(txt_xls_inp_G22);
            //list.Add(txt_xls_inp_G23);
            //list.Add(txt_xls_inp_G24);
            list.Add(txt_xls_inp_G25);
            list.Add(txt_xls_inp_G26);
            list.Add(txt_xls_inp_G28);
            list.Add(txt_xls_inp_G29);
            list.Add(txt_xls_inp_G30);
            list.Add(txt_xls_inp_G31);
            list.Add(txt_xls_inp_G7);
            list.Add(txt_xls_inp_G8);
            list.Add(txt_xls_inp_G9);
            list.Add(txt_xls_inp_I132);
            list.Add(txt_xls_inp_H7);
            list.Add(txt_xls_inp_H8);
            list.Add(txt_xls_inp_H86);
            list.Add(txt_xls_inp_H89);
            list.Add(txt_xls_inp_H9);
            list.Add(txt_xls_inp_I107);
            list.Add(txt_xls_inp_I113);
            list.Add(txt_xls_inp_B134);
            list.Add(txt_xls_inp_I15);
            list.Add(txt_xls_inp_I16);
            list.Add(txt_xls_inp_I17);
            list.Add(txt_xls_inp_I18);
            list.Add(txt_xls_inp_I19);
            list.Add(txt_xls_inp_I20);
            list.Add(txt_xls_inp_I21);
            list.Add(txt_xls_inp_I22);
            //list.Add(txt_xls_inp_I25);
            list.Add(txt_xls_inp_I26);
            list.Add(txt_xls_inp_I25);
            list.Add(txt_xls_inp_I26);
            list.Add(txt_xls_inp_I28);
            list.Add(txt_xls_inp_I29);
            list.Add(txt_xls_inp_I30);
            list.Add(txt_xls_inp_I31);
            list.Add(txt_xls_inp_I7);
            list.Add(txt_xls_inp_I8);
            list.Add(txt_xls_inp_I9);
            list.Add(txt_xls_inp_I101);
            list.Add(txt_xls_inp_J200);
            list.Add(txt_xls_inp_J7);
            list.Add(txt_xls_inp_J8);
            list.Add(txt_xls_inp_J9);
            list.Add(txt_xls_inp_K129);
            list.Add(txt_xls_inp_K74);
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

            file_path = Path.Combine(file_path, "Pier with open foundation BS.xlsm");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier with open foundation BS.xlsm");

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
                //foreach (var item in All_Data)
                //{
                //    if (item.Name.Contains("txt_xls_inp_", ""))
                //    {
                //        kStr = item.Name.Replace("txt_xls_inp_", "");

                //        //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                //        myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                //    }
                //}

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public event EventHandler Worksheet_Force_CheckedChanged;

        private void rbtn_value_analysis_CheckedChanged(object sender, EventArgs e)
        {

            txt_xls_des_G19.Enabled = rbtn_value_analysis.Checked;
            txt_xls_des_G21.Enabled = rbtn_value_analysis.Checked;
            txt_inp_DL.Enabled = rbtn_value_analysis.Checked;
            txt_inp_SIDL.Enabled = rbtn_value_analysis.Checked;
            txt_inp_Surfacing.Enabled = rbtn_value_analysis.Checked;
            txt_inp_FPLL.Enabled = rbtn_value_analysis.Checked;

            if (Worksheet_Force_CheckedChanged != null) Worksheet_Force_CheckedChanged(sender, e);

        }

        private void txt_xls_inp_H7_TextChanged(object sender, EventArgs e)
        {
            Calculated_Values();

        }

        private void btn_Excel_Notes_Click(object sender, EventArgs e)
        {
            if (iApp != null) iApp.Open_Excel_Macro_Notes();
        }
    }
}
