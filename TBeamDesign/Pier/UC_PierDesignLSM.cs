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
    public partial class UC_PierDesignLSM : UserControl
    {
        public UC_PierDesignLSM()
        {
            InitializeComponent();
        }
        public List<TextBox> Get_GEN_Textboxes()
        {

            List<TextBox> list = new List<TextBox>();
            list.Add(txt_GEN_G3);
            list.Add(txt_GEN_I3);
            list.Add(txt_GEN_G4);
            list.Add(txt_GEN_I4);
            list.Add(txt_GEN_G5);
            list.Add(txt_GEN_I5);
            list.Add(txt_GEN_G6);
            list.Add(txt_GEN_I6);
            list.Add(txt_GEN_G8);
            list.Add(txt_GEN_I8);
            list.Add(txt_GEN_G9);
            list.Add(txt_GEN_I9);
            list.Add(txt_GEN_G10);
            list.Add(txt_GEN_I10);
            list.Add(txt_GEN_G12);
            list.Add(txt_GEN_G13);
            list.Add(txt_GEN_G14);
            list.Add(txt_GEN_H19);
            list.Add(txt_GEN_H27);
            list.Add(txt_GEN_H30);
            list.Add(txt_GEN_H32);
            list.Add(txt_GEN_H33);
            list.Add(txt_GEN_H37);
            list.Add(txt_GEN_G45);
            list.Add(txt_GEN_G74);
            list.Add(txt_GEN_G75);
            list.Add(txt_GEN_I81);
            list.Add(txt_GEN_I82);
            list.Add(txt_GEN_I83);
            list.Add(txt_GEN_I84);
            list.Add(txt_GEN_F87);
            list.Add(txt_GEN_F88);
            list.Add(txt_GEN_F94);
            list.Add(txt_GEN_F97);
            list.Add(txt_GEN_F99);

            //list.Add(txt_SUP_G132);

            //list.Add(txt_SI_H50);
            //list.Add(txt_SI_J50);
            //list.Add(txt_SI_H51);
            //list.Add(txt_SI_J51);
            //list.Add(txt_SI_H52);
            //list.Add(txt_SI_J52);
            
            return list;
        }

        public List<TextBox> Get_SUP_Textboxes()
        {
            List<TextBox> list = new List<TextBox>();
            
            list.Add(txt_SUP_G132);


            return list;
        }
        public List<TextBox> Get_SI_Textboxes()
        {
            List<TextBox> list = new List<TextBox>();

            list.Add(txt_SI_H50);
            list.Add(txt_SI_J50);
            list.Add(txt_SI_H51);
            list.Add(txt_SI_J51);
            list.Add(txt_SI_H52);
            list.Add(txt_SI_J52);

            return list;
        }


        public IApplication iapp = null;

        public IApplication iApp
        {
            get
            {
                return iapp;
            }
            set
            {
                iapp  = value;

                Load_Live_Loads();

            }
        }


        string user_path = "";

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF RCC ABUTMENT CANTILEVER [BS]";
                return "Pier Design in Limit State Method";
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

        public string Left_Span
        {
            get
            {
                return txt_GEN_G3.Text;
            }
            set
            {
                txt_GEN_G3.Text = value;
                //txt_des_G92.ForeColor = Color.Red;
                //lbl_note.Visible = true;
            }
        }
        public string Right_Span
        {
            get
            {
                return txt_GEN_I3.Text;
            }
            set
            {
                txt_GEN_I3.Text = value;
                //txt_des_G92.ForeColor = Color.Red;
                //lbl_note.Visible = true;
            }
        }
        public string Total_weight_of_superstructure
        {
            get
            {
                return txt_SUP_G132.Text;
            }
            set
            {
                txt_SUP_G132.Text = value;
                //txt_des_G92.ForeColor = Color.Red;
                //lbl_note.Visible = true;
            }
        }
        public string Left_Span_Vertical_Load
        {
            get
            {
                return txt_SI_H50.Text;
            }
            set
            {
                txt_SI_H50.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }
        public string Right_Span_Vertical_Load
        {
            get
            {
                return txt_SI_J50.Text;
            }
            set
            {
                txt_SI_J50.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }

        //Moment [TT]
        public string Left_Span_Moment_Mz
        {
            get
            {
                return txt_SI_H51.Text;
            }
            set
            {
                txt_SI_H51.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }

        //Moment [TT]
        public string Right_Span_Moment_Mz
        {
            get
            {
                return txt_SI_J51.Text;
            }
            set
            {
                txt_SI_J51.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }


        //Moment [LL]
        public string Left_Span_Moment_Mx
        {
            get
            {
                return txt_SI_H52.Text;
            }
            set
            {
                txt_SI_H52.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }

        //Moment [LL]
        public string Right_Span_Moment_Mx
        {
            get
            {
                return txt_SI_J52.Text;
            }
            set
            {
                txt_SI_J52.Text = value;
                //txt_des_I92.ForeColor = Color.Red;
            }
        }



        public bool Show_Note
        {
            get
            {

                if (lbl_note.Visible)
                {
                    txt_SUP_G132.ForeColor = Color.Red;

                    txt_SI_H50.ForeColor = Color.Red;
                    txt_SI_J50.ForeColor = Color.Red;

                    txt_SI_H51.ForeColor = Color.Red;
                    txt_SI_J51.ForeColor = Color.Red;
                    txt_SI_H52.ForeColor = Color.Red;
                    txt_SI_J52.ForeColor = Color.Red;
                }
                else
                {
                    txt_SUP_G132.ForeColor = Color.Black;

                    txt_SI_H50.ForeColor = Color.Black;
                    txt_SI_J50.ForeColor = Color.Black;

                    txt_SI_H51.ForeColor = Color.Black;
                    txt_SI_J51.ForeColor = Color.Black;
                    txt_SI_H52.ForeColor = Color.Black;
                    txt_SI_J52.ForeColor = Color.Black;
                }
                return lbl_note.Visible;
            }
            set
            {
                lbl_note.Visible = value;
                if (value)
                {
                    txt_SUP_G132.ForeColor = Color.Red;

                    txt_SI_H50.ForeColor = Color.Red;
                    txt_SI_J50.ForeColor = Color.Red;

                    txt_SI_H51.ForeColor = Color.Red;
                    txt_SI_J51.ForeColor = Color.Red;
                    txt_SI_H52.ForeColor = Color.Red;
                    txt_SI_J52.ForeColor = Color.Red;
                }
                else
                {
                    txt_SUP_G132.ForeColor = Color.Black;

                    txt_SI_H50.ForeColor = Color.Black;
                    txt_SI_J50.ForeColor = Color.Black;

                    txt_SI_H51.ForeColor = Color.Black;
                    txt_SI_J51.ForeColor = Color.Black;
                    txt_SI_H52.ForeColor = Color.Black;
                    txt_SI_J52.ForeColor = Color.Black;
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

            pnl.Visible = !pnl.Visible;

            if (pnl.Visible) lbl.ForeColor = Color.Black;
            else lbl.ForeColor = Color.Blue;

        }

        public event EventHandler OnProcess;
        private void btn_process_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(this, "This Process will take few minuites, please wail until the Message Box comes...", "ASTRA", MessageBoxButtons.OK);


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

            file_path = Path.Combine(file_path, "Pier_Design_LS_IRC.xlsm");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier Design in Limit State Method.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\PIER_LS.xlsm");

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
                        //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                        if (kStr == "G14")
                        {
                            myExcelWorksheet.get_Range(kStr).Formula = item.Text + "%";
                        }
                        else
                            myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                    }
                    catch (Exception exx) { }
                }
                #endregion GEN Tab

                #region SUP Tab


                All_Data = Get_SUP_Textboxes();

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SUP"];


                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_SUP_", "");

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #endregion SUP Tab

                #region SI Tab

                All_Data = Get_SI_Textboxes();

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SI"];


                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_SI_", "");

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #endregion SI Tab

                //myExcelWorksheet.get_Range("K78").Formula = data[rindx++].ToString();
                //myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();



            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
        }

        private void Pier_Process_Design_BS()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (iApp.user_path != "")
                file_path = Path.Combine(iApp.user_path, Title);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            //file_path = Path.Combine(file_path, "RCC Cantilever Abutment Design");

            //if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, "Pier_Design_LS_BS.xlsm");

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            //file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\Pier Design in Limit State Method.xlsx");
            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Pier\Pier Design Limit State\PIER_L_BS.xlsm");

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

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    //myExcelWorksheet.get_Range(kStr).Formula = item.Text;

                    try
                    {
                        //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                        if (kStr == "G14")
                        {
                            myExcelWorksheet.get_Range(kStr).Formula = item.Text + "%";
                        }
                        else
                            myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                    }
                    catch (Exception exx) { }


                }
                #endregion GEN Tab

                #region SUP Tab


                All_Data = Get_SUP_Textboxes();

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SUP"];


                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_SUP_", "");

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #endregion SUP Tab

                #region SI Tab

                All_Data = Get_SI_Textboxes();

                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["SI"];


                foreach (var item in All_Data)
                {
                    kStr = item.Name.Replace("txt_SI_", "");

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();
                    myExcelWorksheet.get_Range(kStr).Formula = item.Text;
                }

                #endregion SI Tab




                #region LL Tab


                List<double> load1 = new List<double>();
                List<double> load2 = new List<double>();

                List<double> dist1 = new List<double>();
                List<double> dist2 = new List<double>();

                List<string> xls_cell = new List<string>();

                xls_cell.Add("C");
                xls_cell.Add("D");
                xls_cell.Add("E");
                xls_cell.Add("F");
                xls_cell.Add("G");
                xls_cell.Add("H");
                xls_cell.Add("I");
                xls_cell.Add("J");
                xls_cell.Add("K");

                int i = 0;

                for (i = 0; i < dgv_bs_ll_1.RowCount; i++)
                {
                    try
                    {
                        load1.Add(MyList.StringToDouble(dgv_bs_ll_1[1, i].Value.ToString(), 0.0));
                        if (i > 0)
                            dist1.Add(MyList.StringToDouble(dgv_bs_ll_1[2, i].Value.ToString(), 0.0));
                    }
                    catch (Exception exx1) { }
                }


                for (i = 0; i < dgv_bs_ll_2.RowCount; i++)
                {
                    try
                    {
                        load2.Add(MyList.StringToDouble(dgv_bs_ll_2[1, i].Value.ToString(), 0.0));
                        if (i > 0)
                            dist2.Add(MyList.StringToDouble(dgv_bs_ll_2[2, i].Value.ToString(), 0.0));
                    }
                    catch (Exception exx1) { }
                }



                myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["LL"];

                double HA_KEL = MyList.StringToDouble(txt_bs_left_bl.Text, 0.0);

                int cell = 11;
                #region HA LOad
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();

                    if(i == 0)
                        myExcelWorksheet.get_Range(kStr).Formula = HA_KEL.ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }
                cell = 12;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();

                    if (i == 0)
                        myExcelWorksheet.get_Range(kStr).Formula = "1.0";
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }


                cell = 13;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();

                    if (i == 0)
                        myExcelWorksheet.get_Range(kStr).Formula = HA_KEL.ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }
                cell = 14;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;

                    //myExcelWorksheet.get_Range("E53").Formula = data[rindx++].ToString();

                    if (i == 0)
                        myExcelWorksheet.get_Range(kStr).Formula = "1.0";
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }
                #endregion HA LOad


                #region HB LOad 1

                cell = 18;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;


                    if (i < load1.Count)
                        myExcelWorksheet.get_Range(kStr).Formula = load1[i].ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }
                cell = 19;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;


                    if (i < dist1.Count)
                        myExcelWorksheet.get_Range(kStr).Formula = dist1[i].ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }



                cell = 20;

                load1.Reverse();
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;


                    if (i < load1.Count)
                        myExcelWorksheet.get_Range(kStr).Formula = load1[i].ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }

                cell = 21;
                dist1.Reverse();
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;
                    if (i < dist1.Count)
                        myExcelWorksheet.get_Range(kStr).Formula = dist1[i].ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }


                #endregion HA LOad

                #region HB LOad 2

                cell = 25;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;


                    if (i < load2.Count)
                        myExcelWorksheet.get_Range(kStr).Formula = load2[i].ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }
                cell = 26;
                for (i = 0; i < xls_cell.Count; i++)
                {
                    kStr = xls_cell[i] + cell;


                    if (i < dist2.Count)
                        myExcelWorksheet.get_Range(kStr).Formula = dist2[i].ToString();
                    else
                        myExcelWorksheet.get_Range(kStr).Formula = "";
                }

                #endregion HA LOad





                #endregion LL Tab



                myExcelWorksheet.get_Range("A10").Formula = "HA KEL";
                myExcelWorksheet.get_Range("A17").Formula = cmb_bs_ll_1.Text;
                myExcelWorksheet.get_Range("A24").Formula = cmb_bs_ll_2.Text;
                //myExcelWorksheet.get_Range("K81").Formula = data[rindx++].ToString();

            }
            catch (Exception exx) { }

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            //iApp.Excel_Open_Message();
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

        private void txt_GEN_G3_TextChanged(object sender, EventArgs e)
        {
            double left_span = MyList.StringToDouble(Left_Span, 0.0);
            double right_span = MyList.StringToDouble(Right_Span, 0.0);


            double left_exp = MyList.StringToDouble(txt_GEN_G4.Text, 0.0);
            double right_exp = MyList.StringToDouble(txt_GEN_I4.Text, 0.0);

            double left_exp_gap = MyList.StringToDouble(txt_GEN_G5.Text, 0.0);
            double right_exp_gap = MyList.StringToDouble(txt_GEN_I5.Text, 0.0);

            txt_GEN_G6.Text = (left_span + left_exp + left_exp_gap / 1000.0).ToString("f3");
            txt_GEN_I6.Text = (right_span + right_exp + right_exp_gap / 1000.0).ToString("f3");

        }

    }
}
