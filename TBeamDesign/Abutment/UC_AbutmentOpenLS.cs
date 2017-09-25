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


        #region Forces

        


//txt_xls_DL_H124

//txt_xls_DL_H127



//txt_xls_SIDL_H41
//txt_xls_SIDL_H44




//txt_xls_LL_H43
//txt_xls_LL_H46

//txt_xls_LL_J43
//txt_xls_LL_J46


        public string DL_MTT { get { return txt_xls_DL_H124.Text; } set { txt_xls_DL_H124.Text = value; } }
        public string DL_MLL { get { return txt_xls_DL_H127.Text; } set { txt_xls_DL_H127.Text = value; } }
        public string SIDL_MTT { get { return txt_xls_SIDL_H41.Text; } set { txt_xls_SIDL_H41.Text = value; } }
        public string SIDL_MLL { get { return txt_xls_SIDL_H44.Text; } set { txt_xls_SIDL_H44.Text = value; } }
        public string LL_MTT_Max { get { return txt_xls_LL_H43.Text; } set { txt_xls_LL_H43.Text = value; } }
        public string LL_MTT_Min { get { return txt_xls_LL_J43.Text; } set { txt_xls_LL_J43.Text = value; } }
        public string LL_MLL_Max { get { return txt_xls_LL_H46.Text; } set { txt_xls_LL_H46.Text = value; } }
        public string LL_MLL_Min { get { return txt_xls_LL_J46.Text; } set { txt_xls_LL_J46.Text = value; } }

        #endregion Forces
        public void SetIApplication(IApplication iApp)
        {
            this.iApp = iApp;
            if (iApp.DesignStandard == eDesignStandard.IndianStandard)
            {
                tc_content.TabPages.Remove(tabPage9);
                rbtn_value_worksheet.Checked = true;
            }
            else
            {
                Load_Live_Loads();
            }
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

        public void Load_Live_Loads()
        {
            if (iApp == null) return;
            //cmb_bs_ll_2
            iApp.LiveLoads.Fill_Combo_Without_Type(ref cmb_bs_ll_1);
            iApp.LiveLoads.Fill_Combo_Without_Type(ref cmb_bs_ll_2);
            if (iApp.DesignStandard == eDesignStandard.BritishStandard)
            {
                //cmb_bs_ll_1.SelectedIndex = cmb_bs_ll_1.Items.Count - 1;
                //cmb_bs_ll_2.SelectedIndex = 12;

                cmb_bs_ll_1.SelectedIndex = 8;
                cmb_bs_ll_2.SelectedIndex = 13;
            }
            else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
            {
                cmb_bs_ll_1.SelectedIndex = cmb_bs_ll_1.Items.Count - 1;
                cmb_bs_ll_2.SelectedIndex = 0;
            }
            else
            {
                cmb_bs_ll_1.SelectedIndex = 6;
                cmb_bs_ll_2.SelectedIndex = 0;

                lbl_6.Visible = false;
                pnl_6.Visible = false;
            }
        }
        private void cmb_bs_ll_1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmd = sender as ComboBox;
            DataGridView dgv = dgv_bs_ll_1;
            TextBox txtb = txt_bs_total_load1;
            TextBox txtd = txt_bs_load_dist1;

            if (cmd.Name == cmb_bs_ll_2.Name)
            {
                dgv = dgv_bs_ll_2;
                txtb = txt_bs_total_load2;
                txtd = txt_bs_load_dist2;
            }

            //string ss = cmd.Text;

            //MyList mlst = new MyList(ss, ':');
            string code = cmd.Text;

            LoadData ld = iApp.LiveLoads.Get_LoadData(code);


            dgv.Rows.Clear();
            //for (int i = 0; i < ld.Loads_In_KN.StringList.Count; i++)
            for (int i = 0; i < ld.Loads.StringList.Count; i++)
            {
                if (i == 0)
                {
                    dgv.Rows.Add((i + 1), ld.Loads.StringList[0], "");

                    dgv[2, 0].ReadOnly = true;
                    dgv[2, 0].Style.BackColor = Color.Gray;
                }
                else
                {
                    dgv.Rows.Add((i + 1), ld.Loads.StringList[i], ld.Distances.StringList[i - 1]);
                }
            }
            //txtb.Text = (ld.Total_Loads * 10).ToString("f3");
            txtb.Text = (ld.Total_Loads).ToString("f3");

            txtd.Text = (ld.LoadWidth).ToString("f3");

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
            file_path = Path.Combine(file_path, "Abutment with Open Foundation IS.xls");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Limit State\Abutment with Open Foundation IS.xls");

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
            Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["1.0 Input"];
            Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["1.3 SUP"];
            Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["1.4 SI"];
            Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["1.5 LL"];



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
                    else
                    {
                        if (rbtn_value_analysis.Checked)
                        {
                            if (item.Name.ToLower().StartsWith("txt_xls_dl_"))
                            {
                                kStr = item.Name.ToLower().Replace("txt_xls_dl_", "");

                                //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                                EXL_DL.get_Range(kStr).Formula = item.Text;
                            }
                            else if (item.Name.ToLower().StartsWith("txt_xls_sidl_"))
                            {
                                kStr = item.Name.ToLower().Replace("txt_xls_sidl_", "");

                                //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                                EXL_SIDL.get_Range(kStr).Formula = item.Text;
                            }
                            else if (item.Name.ToLower().StartsWith("txt_xls_ll_"))
                            {
                                kStr = item.Name.ToLower().Replace("txt_xls_ll_", "");

                                //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                                EXL_LL.get_Range(kStr).Formula = item.Text;
                            }
                        }
                    }
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
        private void Process_Design_BS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "Abutment with Open Foundation.xlsm");
            file_path = Path.Combine(file_path, "Abutment with Open Foundation BS.xls");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Abutment\Abutment Limit State\Abutment with Open Foundation BS.xls");

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
            Excel.Worksheet EXL_INP = (Excel.Worksheet)myExcelWorkbook.Sheets["1.0 Input"];
            Excel.Worksheet EXL_DL = (Excel.Worksheet)myExcelWorkbook.Sheets["1.3 SUP"];
            Excel.Worksheet EXL_SIDL = (Excel.Worksheet)myExcelWorkbook.Sheets["1.4 SI"];
            Excel.Worksheet EXL_LL = (Excel.Worksheet)myExcelWorkbook.Sheets["1.5 LL"];



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
                    else
                    {
                        if (rbtn_value_analysis.Checked)
                        {
                            if (item.Name.ToLower().StartsWith("txt_xls_dl_"))
                            {
                                kStr = item.Name.ToLower().Replace("txt_xls_dl_", "");

                                //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                                EXL_DL.get_Range(kStr).Formula = item.Text;
                            }
                            else if (item.Name.ToLower().StartsWith("txt_xls_sidl_"))
                            {
                                kStr = item.Name.ToLower().Replace("txt_xls_sidl_", "");

                                //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                                EXL_SIDL.get_Range(kStr).Formula = item.Text;
                            }
                            else if (item.Name.ToLower().StartsWith("txt_xls_ll_"))
                            {
                                kStr = item.Name.ToLower().Replace("txt_xls_ll_", "");

                                //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                                EXL_LL.get_Range(kStr).Formula = item.Text;
                            }
                        }
                    }
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

        public List<TextBox> Get_TextBoxes(Control ctrl)
        {
            List<TextBox> list = new List<TextBox>();

            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                var c = ctrl.Controls[i];
                if (c.Controls.Count > 0)
                {
                    list.AddRange(Get_TextBoxes(c));
                }
                //foreach (var item in ctrl.Controls)
                //{
                if (c is TextBox)
                {
                    list.Add(c as TextBox);
                }

                //}
            }
            return list;
        }
        public List<TextBox> Get_TextBoxes()
        {
            List<TextBox> list = new List<TextBox>();
            //list.Add(txt_des_G92);


            for (int i = 0; i < this.Controls.Count; i++)
            {
                var c = this.Controls[i];
                list.AddRange(Get_TextBoxes(c));
            }
            return list;


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
        private void rbtn_value_worksheet_CheckedChanged(object sender, EventArgs e)
        {
            grb_DL.Enabled = rbtn_value_analysis.Checked;
            grb_SIDL.Enabled = rbtn_value_analysis.Checked;
            grb_LL_Max.Enabled = rbtn_value_analysis.Checked;
            grb_LL_Min.Enabled = rbtn_value_analysis.Checked;
            if (Worksheet_Force_CheckedChanged != null) Worksheet_Force_CheckedChanged(sender, e);
        }
    }
}
